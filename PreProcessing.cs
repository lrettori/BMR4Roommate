using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MathNet.Numerics.IntegralTransforms;
using MathNet.Numerics.Statistics;
using System.Numerics;
using Filtering;
using System.Xml;

namespace MotorTaskAcquisition
{
    class PreProcessing
    {
        public static double offsetCalculation(double[] gyrDataVector, double[] timeVector, double timeStart, double timeStop)
        {
            /// This function finds the offset of a gyroscope input signal, by calculating its average value (filtering possible spikes or false starts) in the time interval between input values timeStart and timeStop
            double offset = 0;

            // Finding the indices of time interval start and stop
            int indexTimeStart = Matlab.FindIndexOfClosest(timeVector, timeStart);
            int indexTimeStop = Matlab.FindIndexOfClosest(timeVector, timeStop);

            // Extraction of gyroscope data in the considered time interval
            double[] gyrDataVectorIntervalExtraction = new double[indexTimeStop - indexTimeStart + 1];
            Array.Copy(gyrDataVector, indexTimeStart, gyrDataVectorIntervalExtraction, 0, indexTimeStop - indexTimeStart + 1);

            Array.Sort(gyrDataVectorIntervalExtraction);
            int startIndex = gyrDataVectorIntervalExtraction.Length / 2 - gyrDataVectorIntervalExtraction.Length / 4;
            int endIndex = gyrDataVectorIntervalExtraction.Length / 2 + gyrDataVectorIntervalExtraction.Length / 4;

            //offset = gyrDataVectorIntervalExtraction[startIndex..(endIndex + 1)].Mean();
            offset = gyrDataVectorIntervalExtraction.Skip(startIndex).Take(endIndex - startIndex + 1).Mean();

            return offset;
        }

        public static double[] preProcessing_repMovementTasks(HeaderStruct header, out double fundFreq, ref double[] gyrDataVector, out double fSampling, double[] timeVector)
        {
            /// Offset removal, fundamental frequency extraction and low-pass filtering
            /// 

            int nSamples = header.nSamples;

            // Offset calculation, as the mean between 1s and 2s, after removing possible spikes or false starts occurred in that interval
            double offsetGyr = offsetCalculation(gyrDataVector, timeVector, 1, 2);

            int nextPow2nSamples = Matlab.Nextpow2(nSamples);
            int NFFT = Matlab.IntPow(2, nextPow2nSamples);
            Complex[] freqData = new Complex[NFFT]; // Preparation of input data for FFT in place (zero-padding to reach size NFFT)
            double[] spectDensity = new double[NFFT];
            double[] timeDiff = new double[nSamples - 1];

            for (int j = 0; j < nSamples; j++)
            {
                gyrDataVector[j] = gyrDataVector[j] - offsetGyr;
                freqData[j] = gyrDataVector[j]; // Last (NFFT-nSamples) elements are left to 0
                if (j != nSamples - 1)
                    timeDiff[j] = timeVector[j + 1] - timeVector[j];
            }

            /// Find the fundamental frequency of the input signal, in order to define the cut-off frequency of the zero-phase filter
            /// 

            // Find sampling frequency, as the inverse of the median of (t[i+1]-t[i])
            //fSampling = 1 / ((timeDiff).Median());
            fSampling = 1 / ((timeDiff).Mean());

            // Perform FFT of data
            Fourier.Forward(freqData);
            // Normalization factor to adapt to Matlab method
            double normFactor = Math.Sqrt(NFFT);

            for (int j = 0; j < NFFT; j++)
            {
                freqData[j] = freqData[j] * normFactor;
                spectDensity[j] = Math.Pow((freqData[j].Magnitude), 2) / (nSamples * fSampling);
            }

            // Create fVector using the sampling frequency calculated
            double[] fVector = Matlab.Linspace(0, 1, NFFT / 2 + 1);
            for (int j = 0; j < NFFT / 2 + 1; j++)
            {
                fVector[j] = (fSampling / 2) * fVector[j];
            }

            // Find the fundamental frequency
            double maxValue = spectDensity.Skip(0).Take(NFFT / 2 + 1).Max();
            int maxIndex = spectDensity.ToList().IndexOf(maxValue);
            fundFreq = fVector[maxIndex];

            // Define the Butterworth filter parameters
            int orderFilter = 4;
            double fCut = fundFreq + 1;
            double wn_cut = 2 * fCut / fSampling;
            double[][] filterParams = new double[2][];
            IIR_Butterworth_Interface IBI = new IIR_Butterworth_Implementation();
            filterParams = IBI.Lp2lp(wn_cut, orderFilter);
            double[] b = filterParams[0];
            double[] a = filterParams[1];

            // Filter the data
            double[] filteredData = FilteringTools.FiltFilt(gyrDataVector, a, b);

            return filteredData;
        }

        public static double[,] preProcessing_TremorTasks(HeaderStruct header, SensDataStruct sensData, ref double[] accIAVtot, ref int[] tStartAndStopIndices, out double fSampling, string taskSelected, double[] timeVector)
        {
            int nSamples = header.nSamples;
            double[,] accGyrDataPreProcessed = new double[4, nSamples];

            // Find the fSampling from data
            double[] timeDiff = new double[nSamples - 1];
            for (int j = 0; j < nSamples - 1; j++)
            {
                timeDiff[j] = timeVector[j + 1] - timeVector[j];
            }
            fSampling = 1 / ((timeDiff).Mean());

            // Extract the data from sensData
            double[] dataAccX = new double[nSamples];
            double[] dataAccY = new double[nSamples];
            double[] dataAccZ = new double[nSamples];
            double[] dataGyrX = new double[nSamples];
            double[] dataGyrY = new double[nSamples];
            double[] dataGyrZ = new double[nSamples];

            for (int i = 0; i < nSamples; i++)
            {
                dataAccX[i] = sensData.acc[0, i];
                dataAccY[i] = sensData.acc[1, i];
                dataAccZ[i] = sensData.acc[2, i];
                dataGyrX[i] = sensData.gyr[0, i];
                dataGyrY[i] = sensData.gyr[1, i];
                dataGyrZ[i] = sensData.gyr[2, i];
            }

            // Low-pass filtering, with cut-off frequency equal to 20 Hz
            int orderFilter = 4;
            double fCut = 20;
            double wn_cut = 2 * fCut / fSampling;
            double[][] filterParams = new double[2][];
            IIR_Butterworth_Interface IBI = new IIR_Butterworth_Implementation();
            filterParams = IBI.Lp2lp(wn_cut, orderFilter);
            double[] b = filterParams[0];
            double[] a = filterParams[1];

            double[] filteredDataAccX = FilteringTools.FiltFilt(dataAccX, a, b);
            double[] filteredDataAccY = FilteringTools.FiltFilt(dataAccY, a, b);
            double[] filteredDataAccZ = FilteringTools.FiltFilt(dataAccZ, a, b);
            double[] filteredDataGyrX = FilteringTools.FiltFilt(dataGyrX, a, b);
            double[] filteredDataGyrY = FilteringTools.FiltFilt(dataGyrY, a, b);
            double[] filteredDataGyrZ = FilteringTools.FiltFilt(dataGyrZ, a, b);

            /// Offset removal from gyroscope data
            double offsetGyrX = offsetCalculation(filteredDataGyrX, timeVector, 1, 2);
            double offsetGyrY = offsetCalculation(filteredDataGyrY, timeVector, 1, 2);
            double offsetGyrZ = offsetCalculation(filteredDataGyrZ, timeVector, 1, 2);

            double[] dataGyrXOffsetRemov = new double[nSamples];
            double[] dataGyrYOffsetRemov = new double[nSamples];
            double[] dataGyrZOffsetRemov = new double[nSamples];

            // Remove the offset from the gyroscope data vectors
            for (int i = 0; i < nSamples; i++)
            {
                dataGyrXOffsetRemov[i] = filteredDataGyrX[i] - offsetGyrX;
                dataGyrYOffsetRemov[i] = filteredDataGyrY[i] - offsetGyrY;
                dataGyrZOffsetRemov[i] = filteredDataGyrZ[i] - offsetGyrZ;
            }

            // Find start and end indices for the exercise
            int indexStart = Matlab.FindIndexOfClosest(timeVector, 3);
            int indexStop = 0;
            List<int> indexStopList = new List<int>();

            switch (taskSelected)
            {
                case "HRST":
                case "FRST":
                    indexStop = Matlab.FindIndexOfClosest(timeVector, 35);
                    break;

                case "POST":
                    indexStop = Matlab.FindIndexOfClosest(timeVector, 13);
                    break;

                case "KINT":
                    int k = Matlab.FindIndexOfClosest(timeVector, 5);
                    int p = nSamples - 1;
                    double THRE = 100;
                    double TH_t = 9;
                    int step = 0;
                    int app;
                    bool flag = false;
                    bool flagTendNotFound = true;

                    double[] gyrXSquared = dataGyrXOffsetRemov.Select(x => x * x).ToArray();
                    double[] gyrYSquared = dataGyrYOffsetRemov.Select(x => x * x).ToArray();
                    double[] gyrZSquared = dataGyrZOffsetRemov.Select(x => x * x).ToArray();
                    double[] gyrModule = new double[nSamples];

                    for (int j = 0; j < nSamples; j++)
                    {
                        gyrModule[j] = Math.Sqrt(gyrXSquared[j] + gyrYSquared[j] + gyrZSquared[j]);
                    }

                    while (p >= k && flagTendNotFound)
                    {
                        if (gyrModule[p] > THRE)
                        {
                            app = p;
                            //step++;
                            flag = true;
                            while (flag && app < nSamples - 1)
                            {
                                app++;
                                if (gyrModule[app] < TH_t)
                                {
                                    //step++;
                                    indexStop = app;
                                    //indexStopList.Add(app);
                                    flag = false;
                                    flagTendNotFound = false;
                                    break;
                                }
                            }
                        }
                        p = p - 1;
                    }
                    //indexStop = indexStopList[0];
                    break;
            }
            tStartAndStopIndices[0] = indexStart;
            tStartAndStopIndices[1] = indexStop;

            // High-pass filtering, with cut-off frequency equal to 0.5 Hz
            orderFilter = 4;
            double fCutHigh = 0.5;
            if (String.Equals(taskSelected, "KINT"))
                fCutHigh = 1.5;

            double wn_cutHigh = 2 * fCutHigh / fSampling;
            double[][] filterParamsHigh = new double[2][];
            IIR_Butterworth_Interface IBI2 = new IIR_Butterworth_Implementation();
            filterParamsHigh = IBI2.Lp2hp(wn_cutHigh, orderFilter);
            double[] d = filterParamsHigh[0];
            double[] c = filterParamsHigh[1];

            double[] accTot = new double[nSamples];

            for (int i = 0; i < nSamples; i++)
            {
                accTot[i] = Math.Sqrt(Math.Pow(filteredDataAccX[i], 2) + Math.Pow(filteredDataAccY[i], 2) + Math.Pow(filteredDataAccZ[i], 2));
            }

            // acceleration vector for IAV analysis (not HP filtered)
            accIAVtot = accTot;

            double[] accFilteredTot = FilteringTools.FiltFilt(accTot, c, d);
            double[] gyrXFilteredTot = FilteringTools.FiltFilt(dataGyrXOffsetRemov, c, d);
            double[] gyrYFilteredTot = FilteringTools.FiltFilt(dataGyrYOffsetRemov, c, d);
            double[] gyrZFilteredTot = FilteringTools.FiltFilt(dataGyrZOffsetRemov, c, d);

            // Fill the output structure (first row acc data, last three rows gyroscope data)
            for (int i = 0; i < nSamples; i++)
            {
                accGyrDataPreProcessed[0, i] = accFilteredTot[i];
                accGyrDataPreProcessed[1, i] = gyrXFilteredTot[i];
                accGyrDataPreProcessed[2, i] = gyrYFilteredTot[i];
                accGyrDataPreProcessed[3, i] = gyrZFilteredTot[i];
            }

            return accGyrDataPreProcessed;
        }

        public static double[] preProcessing_STUP(HeaderStruct header, SensDataStruct sensData, out double fSampling, double[] timeVector)
        {
            int nSamples = header.nSamples;

            // Find the fSampling from data
            double[] timeDiff = new double[nSamples - 1];
            for (int j = 0; j < nSamples - 1; j++)
            {
                timeDiff[j] = timeVector[j + 1] - timeVector[j];
            }
            fSampling = 1 / ((timeDiff).Mean());

            // Extract the data from sensData
            //double[] dataAccX = new double[nSamples];
            //double[] dataAccY = new double[nSamples];
            //double[] dataAccZ = new double[nSamples];
            double[] dataGyrX = new double[nSamples];
            double[] dataGyrY = new double[nSamples];
            double[] dataGyrZ = new double[nSamples];

            for (int i = 0; i < nSamples; i++)
            {
                //dataAccX[i] = sensData.acc[0, i];
                //dataAccY[i] = sensData.acc[1, i];
                //dataAccZ[i] = sensData.acc[2, i];
                dataGyrX[i] = sensData.gyr[0, i];
                dataGyrY[i] = sensData.gyr[1, i];
                dataGyrZ[i] = sensData.gyr[2, i];
            }

            /// Offset removal from gyroscope data
            double offsetGyrX = PreProcessing.offsetCalculation(dataGyrX, timeVector, 1, 2);
            double offsetGyrY = PreProcessing.offsetCalculation(dataGyrY, timeVector, 1, 2);
            double offsetGyrZ = PreProcessing.offsetCalculation(dataGyrZ, timeVector, 1, 2);

            double[] dataGyrXOffsetRemov = new double[nSamples];
            double[] dataGyrYOffsetRemov = new double[nSamples];
            double[] dataGyrZOffsetRemov = new double[nSamples];

            // Remove the offset from the gyroscope data vectors
            for (int i = 0; i < nSamples; i++)
            {
                dataGyrXOffsetRemov[i] = dataGyrX[i] - offsetGyrX;
                dataGyrYOffsetRemov[i] = dataGyrY[i] - offsetGyrY;
                dataGyrZOffsetRemov[i] = dataGyrZ[i] - offsetGyrZ;
            }

            // Low-pass filtering, with cut-off frequency equal to 5 Hz
            int orderFilter = 4;
            double fCut = 5;
            double wn_cut = 2 * fCut / fSampling;
            double[][] filterParams = new double[2][];
            IIR_Butterworth_Interface IBI = new IIR_Butterworth_Implementation();
            filterParams = IBI.Lp2lp(wn_cut, orderFilter);
            double[] b = filterParams[0];
            double[] a = filterParams[1];

            //double[] filteredDataAccX = FilteringTools.FiltFilt(dataAccX, a, b);
            //double[] filteredDataAccY = FilteringTools.FiltFilt(dataAccY, a, b);
            //double[] filteredDataAccZ = FilteringTools.FiltFilt(dataAccZ, a, b);
            double[] filteredDataGyrX = FilteringTools.FiltFilt(dataGyrXOffsetRemov, a, b);
            double[] filteredDataGyrY = FilteringTools.FiltFilt(dataGyrYOffsetRemov, a, b);
            double[] filteredDataGyrZ = FilteringTools.FiltFilt(dataGyrZOffsetRemov, a, b);

            // Gyroscope magnitude calculation, and offset removal
            double[] gyrMod = new double[nSamples];
            for (int i = 0; i < nSamples; i++)
            {
                gyrMod[i] = Math.Sqrt(Math.Pow(filteredDataGyrX[i], 2) + Math.Pow(filteredDataGyrY[i], 2) + Math.Pow(filteredDataGyrZ[i], 2));
            }

            double[] gyrModOffsetRemov = new double[nSamples];
            double offsetGyrMod = PreProcessing.offsetCalculation(gyrMod, timeVector, 1, 2);
            for (int i = 0; i < nSamples; i++)
            {
                gyrModOffsetRemov[i] = gyrMod[i] - offsetGyrMod;
            }

            return gyrModOffsetRemov;
        }

        public static double[] preProcessing_ROTA(HeaderStruct header, SensDataStruct sensData, out double fSampling, double[] timeVector)
        {
            int nSamples = header.nSamples;

            // Find the fSampling from data
            double[] timeDiff = new double[nSamples - 1];
            for (int j = 0; j < nSamples - 1; j++)
            {
                timeDiff[j] = timeVector[j + 1] - timeVector[j];
            }
            fSampling = 1 / ((timeDiff).Mean());

            // Extract the data from sensData
            double[] dataGyrZ = new double[nSamples];

            for (int i = 0; i < nSamples; i++)
            {
                dataGyrZ[i] = sensData.gyr[2, i];
            }

            // Low-pass filtering, with cut-off frequency equal to 5 Hz
            int orderFilter = 4;
            double fCut = 5;
            double wn_cut = 2 * fCut / fSampling;
            double[][] filterParams = new double[2][];
            IIR_Butterworth_Interface IBI = new IIR_Butterworth_Implementation();
            filterParams = IBI.Lp2lp(wn_cut, orderFilter);
            double[] b = filterParams[0];
            double[] a = filterParams[1];

            double[] filteredDataGyrZ = FilteringTools.FiltFilt(dataGyrZ, a, b);

            /// Offset removal from gyroscope data
            double offsetGyrZ = offsetCalculation(filteredDataGyrZ, timeVector, 1, 2);

            double[] dataGyrZOffsetRemov = new double[nSamples];

            // Remove the offset from the gyroscope data vectors
            for (int i = 0; i < nSamples; i++)
            {
                dataGyrZOffsetRemov[i] = filteredDataGyrZ[i] - offsetGyrZ;
            }

            return dataGyrZOffsetRemov;
        }
    }
}