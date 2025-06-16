
using Filtering;
using MathNet.Numerics.IntegralTransforms;
using MathNet.Numerics.Statistics;
using MotorTaskAcquisition;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System;


/// --------------------------------------  Feature extraction for repMovement tasks (THFF, FTAP, OPCL, PSUP, TTHP, HTTP) --------------------------------------

namespace MotorTaskAcquisition
{
    class FeatureExtraction_repMovementTasks
    {
        public static FeaturesRepMovStruct featureExtraction_repMovementTasks(string taskSelected, HeaderStruct header, SensDataStruct sensData, double[] timeVector)
        {
            FeaturesRepMovStruct features = new FeaturesRepMovStruct();
            features.exerCode = taskSelected;
            features.isFeatureCalculated = true;
            double fundFreq;
            double fSampling;

            // Extract correct gyr axis data based on the specific task
            double[] gyrDataVector = new double[header.nSamples];
            int gyrAxis = new int();

            switch (taskSelected)
            {
                case "THFv":
                case "THFa":
                case "FTAP":
                case "OPCv":
                case "OPCa":
                case "OPCL":
                case "TTHP":
                case "HTTP":
                    // y axis
                    gyrAxis = 1;
                    break;

                case "PSUP":
                    // x axis
                    gyrAxis = 0;
                    break;
            }

            for (int j = 0; j < header.nSamples; j++)
            {
                gyrDataVector[j] = sensData.gyr[gyrAxis, j];
            }


            // Pre-processing of raw data (offset removal and low-pass filtering)
            double[] gyrDataVectorFiltered1 = PreProcessing.preProcessing_repMovementTasks(header, out fundFreq, ref gyrDataVector, out fSampling, timeVector);
            //processedData = gyrDataVectorFiltered1;

            // Signal segmentation
            List<int> tStartIndices = new List<int>();
            List<int> tMaxIndices = new List<int>();
            List<int> tEndIndices = new List<int>();
            int nSteps = signalSegmentation_repMovementTasks(taskSelected, header.side, timeVector, gyrDataVectorFiltered1, out tStartIndices, out tMaxIndices, out tEndIndices);

            // If the number of repetitions is under 3, leave all the output parameters to 0
            if (nSteps > 2)
            {
                // Extract acceleration data
                int nSamples = header.nSamples;
                // Extract data from index finger of the selected hand
                double[] accXData = new double[nSamples];
                double[] accYData = new double[nSamples];
                double[] accZData = new double[nSamples];

                for (int j = 0; j < nSamples; j++)
                {
                    accXData[j] = sensData.acc[0, j];
                    accYData[j] = sensData.acc[1, j];
                    accZData[j] = sensData.acc[2, j];
                }

                // Define filtering settings for features extraction
                double fCut;
                if (fundFreq < 3.5)
                    fCut = 5;
                else
                    fCut = fundFreq + 1.5;
                int orderFilter = 4;
                double wn_cut = 2 * fCut / fSampling;
                double[][] filterParams = new double[2][];
                IIR_Butterworth_Interface IBI = new IIR_Butterworth_Implementation();
                filterParams = IBI.Lp2lp(wn_cut, orderFilter);
                double[] b = filterParams[0];
                double[] a = filterParams[1];

                // Data filtering
                double[] accXDataFiltered = FilteringTools.FiltFilt(accXData, a, b);
                double[] accYDataFiltered = FilteringTools.FiltFilt(accYData, a, b);
                double[] accZDataFiltered = FilteringTools.FiltFilt(accZData, a, b);
                //
                double[] gyrDataVectorFiltered = FilteringTools.FiltFilt(gyrDataVector, a, b);

                /// Feature extraction
                /// 

                if (tEndIndices.Count < tStartIndices.Count & tStartIndices.Count == tMaxIndices.Count)
                {
                    // Append last element of tEndIndices, and do not modify nSteps
                    int durLastRep = tEndIndices.Last() - tStartIndices[tStartIndices.Count - 2];
                    tEndIndices.Add(tStartIndices.Last() + durLastRep);
                }
                else if (tStartIndices.Count > tMaxIndices.Count)
                {
                    // Remove last element of tStartIndices and reduce nSteps
                    tStartIndices.RemoveAt(tStartIndices.Count - 1);
                    nSteps = nSteps - 1;
                }
                features.taps = nSteps;

                if (nSteps < 11)
                {
                    features.vel10 = 0;
                    features.vel10SD = 0;
                }
                else
                {
                    double[] tTap = new double[10];
                    double[] vel = new double[10];
                    for (int j = 0; j < 10; j++)
                    {
                        tTap[j] = timeVector[tEndIndices[j + 1]] - timeVector[tStartIndices[j + 1]];
                        vel[j] = 1 / tTap[j];

                    }
                    features.vel10 = vel.Average();
                    features.vel10SD = Matlab.Std(vel);
                }

                // Calculation of IAV parameter
                int startIndex = tStartIndices[0];
                int endIndex = tEndIndices[tEndIndices.Count - 1];
                double[] accX = accXDataFiltered.Skip(startIndex).Take(endIndex - startIndex + 1).ToArray();
                double[] accY = accYDataFiltered.Skip(startIndex).Take(endIndex - startIndex + 1).ToArray();
                double[] accZ = accZDataFiltered.Skip(startIndex).Take(endIndex - startIndex + 1).ToArray();

                double[] accXSquared = accX.Select(x => x * x).ToArray();
                double[] accYSquared = accY.Select(x => x * x).ToArray();
                double[] accZSquared = accZ.Select(x => x * x).ToArray();
                double[] acc = new double[tEndIndices[tEndIndices.Count - 1] - tStartIndices[0] + 1];

                for (int j = 0; j < acc.Length; j++)
                {
                    acc[j] = Math.Sqrt(accXSquared[j] + accYSquared[j] + accZSquared[j]);
                }
                features.IAV = Matlab.Trapz(timeVector.Skip(startIndex).Take(endIndex - startIndex + 1).ToArray(), acc);

                // Angular amplitude of the movement
                calculateAmplitudeFeatures_repMovementTasks(ref features, gyrDataVectorFiltered, tStartIndices, tMaxIndices, tEndIndices, timeVector);

                // Interruptions
                calculateInterruptionFeatures_repMovementTasks(ref features, gyrDataVectorFiltered, tEndIndices, timeVector);

            }
            return features;
        }

        public static int signalSegmentation_repMovementTasks(string taskSelected, string side, double[] ts, double[] filteredData, out List<int> tStartIndices, out List<int> tMaxIndices, out List<int> tEndIndices)
        {
            /// Performs segmentation of the input signal filteredData. It returns the number of repetitions performed (nSteps), together with the indices at which each repetition starts (tStartIndices), 
            /// reaches its maximum (tMaxIndices), and ends (tEndIndices)
            /// 

            // Initialization of parameters
            int nSteps = 0;
            //int k = 300;
            int k = Matlab.FindIndexOfClosest(ts, 3);
            int p = filteredData.Length;
            int state = 1;
            int app;
            bool flag = false;
            double THRE = 0;
            double TH_t = 0;

            // Definition of thresholds
            switch (taskSelected)
            {
                case "FTAP":
                case "THFv":
                case "THFa":
                    THRE = 20;
                    TH_t = 3;
                    break;

                case "OPCv":
                case "OPCa":
                case "OPCL":
                    THRE = 50;
                    TH_t = 3;
                    break;

                case "PSUP":
                    THRE = 50;
                    TH_t = 5;
                    if (side == "Sx" || side == "SX")
                        filteredData = filteredData.Select(x => -x).ToArray();
                    break;

                case "TTHP":
                    THRE = 20;
                    TH_t = 3;
                    break;

                case "HTTP":
                    THRE = 15;
                    TH_t = 2;
                    filteredData = filteredData.Select(x => -x).ToArray();
                    break;

            }

            tStartIndices = new List<int>();
            tMaxIndices = new List<int>();
            tEndIndices = new List<int>();

            while (k < p)
            {
                switch (state)
                {
                    // Looking for the start of next repetition
                    case 1:
                        // First repetition of the sequence
                        if (nSteps == 0)
                        {
                            if (filteredData[k] > THRE)
                            {
                                app = k;
                                flag = true;
                                while (flag)
                                {
                                    app = app - 1;
                                    if (filteredData[app] < TH_t)
                                    {
                                        nSteps++;
                                        tStartIndices.Add(app);
                                        flag = false;
                                        state = 2;
                                    }
                                }
                            }
                        }
                        // From the second repetition on
                        else if (filteredData[k] > THRE & (ts[k] < (ts[tStartIndices[0]] + 10))) // 10 s after tStartIndices(0) [MODIFICARE] mettere questa condizione dopo l'incremento di k, e uscire dalla segmentazione
                        {
                            app = k;
                            flag = true;
                            while (flag)
                            {
                                app = app - 1;
                                if (filteredData[app] < TH_t)
                                {
                                    nSteps++;
                                    tStartIndices.Add(app);

                                    if (tStartIndices[nSteps - 1] < tEndIndices[nSteps - 2])
                                        tStartIndices[nSteps - 1] = tEndIndices[nSteps - 2];
                                    flag = false;
                                    state = 2;
                                }
                            }
                        }
                        break;

                    case 2:
                        // Looking for the inversion of the movement in the current repetition
                        if (filteredData[k] < TH_t)
                        {
                            tMaxIndices.Add(k);
                            state = 3;
                        }
                        break;

                    case 3:
                        // Looking for the end of the repetition
                        if (filteredData[k] > -TH_t & filteredData[k] > filteredData[k - 1])
                        {
                            tEndIndices.Add(k);
                            state = 1;
                        }
                        break;
                }
                k++;
            }
            return nSteps;
        }

        public static void calculateAmplitudeFeatures_repMovementTasks(ref FeaturesRepMovStruct features, double[] gyrDataVectorFiltered, List<int> tStartIndices, List<int> tMaxIndices, List<int> tEndIndices, double[] ts)
        {
            double[] wopen_2 = new double[features.taps];
            double[] wclose_2 = new double[features.taps];
            double[] yang_tap2 = new double[features.taps];

            for (int i = 0; i < features.taps; i++)
            {
                double[] wyang = gyrDataVectorFiltered.Skip(tStartIndices[i]).Take(tMaxIndices[i] - tStartIndices[i] + 1).ToArray();
                double[] tapp = ts.Skip(tStartIndices[i]).Take(tMaxIndices[i] - tStartIndices[i] + 1).ToArray();
                double[] yang = Matlab.Cumtrapz(tapp, wyang);

                int finalIndex;
                if (i < features.taps - 1)
                    finalIndex = tStartIndices[i + 1];
                else
                    // Last repetition
                    finalIndex = tEndIndices[i];

                double[] wyang2 = gyrDataVectorFiltered.Skip(tMaxIndices[i]).Take(finalIndex - tMaxIndices[i] + 1).ToArray();
                double[] tapp2 = ts.Skip(tMaxIndices[i]).Take(finalIndex - tMaxIndices[i] + 1).ToArray();
                double[] yang2 = Matlab.Cumtrapz(tapp2, wyang2);
                double[] yang_r = yang2.Select(x => x + yang.Last()).ToArray();
                double myang = (yang_r.Last() - yang[0]) / (tapp2.Last() - tapp[0]);
                double qyang = yang[0] - myang * tapp[0];

                double[] yang_e = ts.Skip(tStartIndices[i]).Take(finalIndex - tStartIndices[i] + 1).ToArray();
                yang_e = yang_e.Select(x => x * myang + qyang).ToArray();
                double[] yang_tot = yang.Concat(yang_r.Skip(1).Take(yang_r.Length - 1)).ToArray();
                double[] yang_m = Matlab.Subtract(yang_tot, yang_e);

                wopen_2[i] = wyang.Average();
                wclose_2[i] = wyang2.Average();
                yang_tap2[i] = (Matlab.AbsOfArray(yang_m)).Max();
            }

            // Extraction of amplitude features
            int startIndex;
            int endIndex = features.taps - 1;
            if (features.taps == 3)
                // Few repetitions available, the first one is not discarded
                startIndex = 0;
            else
                // Discard the first repetition
                startIndex = 1;

            features.wo = Math.Abs(wopen_2.Skip(startIndex).Take(endIndex - startIndex).Average());
            features.woSD = Matlab.Std(Matlab.AbsOfArray(wopen_2.Skip(startIndex).Take(endIndex - startIndex).ToArray()));
            features.wc = Math.Abs(wclose_2.Skip(startIndex).Take(endIndex - startIndex).Average());
            features.wcSD = Matlab.Std(Matlab.AbsOfArray(wclose_2.Skip(startIndex).Take(endIndex - startIndex).ToArray()));
            features.exc = yang_tap2.Skip(startIndex).Take(endIndex - startIndex).Average();
            features.excSD = Matlab.Std(yang_tap2.Skip(startIndex).Take(endIndex - startIndex).ToArray());

            if (features.taps >= 11)
            {
                features.exc10 = yang_tap2.Skip(1).Take(10).Sum() / 10;
                features.exc10SD = Matlab.Std(yang_tap2.Skip(1).Take(10).ToArray());

                // Amplitude reduction calculation (first 10 reps)
                features.dec10B = 100 * (((yang_tap2.Skip(2).Take(4).Average()) / yang_tap2[1]) - 1);
                features.dec10M = 100 * (((yang_tap2.Skip(5).Take(3).Average()) / yang_tap2[1]) - 1);
                features.dec10E = 100 * (((yang_tap2.Skip(8).Take(3).Average()) / yang_tap2[1]) - 1);
            }
            else
            {
                features.exc10 = 0;
                features.exc10SD = 0;
            }

            // Amplitude reduction calculation (all acquisition)
            if (features.taps > 5)
            {
                double[] timeFromFirstTap = new double[features.taps];
                for (int i = 0; i < features.taps; i++)
                {
                    timeFromFirstTap[i] = ts[tStartIndices[i]] - ts[tStartIndices[0]];
                }

                // Looking for indices of taps in each of the three sub-intervals considered ([1, 4], [4, 7] and [7, 10] seconds)
                List<int> indicesBegInterval = Matlab.FindIndicesBetweenTwoValues(timeFromFirstTap, 1, 4);
                List<int> indicesMedInterval = Matlab.FindIndicesBetweenTwoValues(timeFromFirstTap, 4, 7);
                List<int> indicesFinInterval = Matlab.FindIndicesBetweenTwoValues(timeFromFirstTap, 7, 10);

                double ampInit = 0;
                double ampMid = 0;
                double ampFin = 0;

                if (indicesBegInterval.Count > 0)
                {
                    ampInit = yang_tap2.Skip(indicesBegInterval.First()).Take(indicesBegInterval.Last() - indicesBegInterval.First() + 1).Average();
                    features.decB = 100 * ((ampInit / yang_tap2[1]) - 1);
                }

                if (indicesMedInterval.Count > 0)
                {
                    ampMid = yang_tap2.Skip(indicesMedInterval.First()).Take(indicesMedInterval.Last() - indicesMedInterval.First() + 1).Average();
                    features.decM = 100 * ((ampMid / yang_tap2[1]) - 1);
                }

                if (indicesFinInterval.Count > 0)
                {
                    ampFin = yang_tap2.Skip(indicesFinInterval.First()).Take(indicesFinInterval.Last() - indicesFinInterval.First() + 1).Average();
                    features.decE = 100 * ((ampFin / yang_tap2[1]) - 1);
                }
            }
        }

        public static void calculateInterruptionFeatures_repMovementTasks(ref FeaturesRepMovStruct features, double[] gyrDataVectorFiltered, List<int> tEndIndices, double[] ts)
        {
            features.interr = 0;
            features.interr10 = 0;

            // Extraction of the number of interruptions only if more than 10 taps
            if (features.taps >= 11)
            {
                int nSteps;
                if (features.taps == 11)
                    nSteps = features.taps;
                else
                    nSteps = features.taps - 1;


                double[] timesEndReps = new double[nSteps];

                for (int i = 0; i < nSteps; i++)
                {
                    timesEndReps[i] = ts[tEndIndices[i]];
                }
                double[] rhythm = Matlab.Diff(timesEndReps);
                double[] rhythmSorted = new double[rhythm.Length];
                Array.Copy(rhythm, rhythmSorted, rhythm.Length);

                Array.Sort(rhythmSorted);
                int L = rhythm.Length;
                double averageRhythm = 0;

                if (L % 2 == 0)
                {
                    int[] indexes = { L / 2 - 2, L / 2 - 1, L / 2, L / 2 + 1 };
                    for (int i = 0; i < indexes.Length; i++)
                    {
                        averageRhythm = averageRhythm + rhythmSorted[indexes[i]];
                    }
                    averageRhythm = averageRhythm / indexes.Length;
                }
                else
                {
                    int[] indexes = { L / 2 - 2, L / 2 - 1, L / 2, L / 2 + 1, L / 2 + 2 };
                    for (int i = 0; i < indexes.Length; i++)
                    {
                        averageRhythm = averageRhythm + rhythmSorted[indexes[i]];
                    }
                    averageRhythm = averageRhythm / indexes.Length;
                }

                double rhythmSD = Matlab.Std(rhythm);

                // Count the number of repetitions whose time period (rhythm) is outside the interval (average ± SD)
                for (int i = 0; i < L; i++)
                {
                    if (rhythm[i] < averageRhythm - 2 * rhythmSD | rhythm[i] > averageRhythm + 2 * rhythmSD)
                    {
                        features.interr = features.interr + 1;
                        if (i < 10)
                            features.interr10 = features.interr10 + 1;
                    }
                }
            }
        }

    }
}