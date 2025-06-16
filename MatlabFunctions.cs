using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using System.Net.Http.Headers;
using System.Linq;
using MathNet.Numerics;
using Combinatorics;
using Combinatorics.Collections;
using MathNet.Numerics.LinearAlgebra.Factorization;
using MotorTaskAcquisition;

namespace MotorTaskAcquisition
{
    class Matlab
    {
        /// Matlab functions useful for this application

        public static int FindIndexOfClosest(double[] array, double number)
        {
            int index = 0;
            double closest = array[0];
            double difference = Math.Abs(number - closest);
            for (int i = 1; i < array.Length; i++)
            {
                double currentDifference = Math.Abs(number - array[i]);
                if (currentDifference < difference)
                {
                    closest = array[i];
                    difference = currentDifference;
                    index = i;
                }
            }

            return index;
        }

        public static List<int> FindIndicesBetweenTwoValues(double[] array, double number1, double number2)
        {
            // Extract indices of values if >= number1 and < number2
            List<int> indices = new List<int>();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] >= number1 & array[i] < number2)
                    indices.Add(i);
            }

            return indices;
        }

        public static int Nextpow2(double inValue)
        {
            int nextPow = 1;
            int result = 2;

            while (result < inValue)
            {
                nextPow++;
                result = result * 2;
            }

            return nextPow;
        }

        public static double[] Linspace(double startval, double endval, int steps)
        {
            //double interval = (endval / Math.Abs(endval)) * Math.Abs(endval - startval) / (steps - 1);
            double interval = Math.Abs(endval - startval) / (steps - 1);
            double[] result = (from val in Enumerable.Range(0, steps) select startval + (val * interval)).ToArray();

            return result;
        }

        public static double[] Diff(double[] inputArray)
        {
            int length = inputArray.Length;
            double[] diffArray = new double[length - 1];
            for (int i = 0; i < length - 1; i++)
            {
                diffArray[i] = inputArray[i + 1] - inputArray[i];
            }
            return diffArray;
        }

        public static double[] Sum(double[] a, double[] b)
        {
            if (a.Length == b.Length & a.Length != 0)
            {
                double[] result = new double[a.Length];
                for (int i = 0; i < a.Length; i++)
                {
                    result[i] = a[i] + b[i];
                }
                return result;
            }
            else
            {
                // return 0
                double[] result = new double[1];

                return result;
            }
        }

        public static double[] Subtract(double[] a, double[] b)
        {
            if (a.Length == b.Length & a.Length != 0)
            {
                double[] result = new double[a.Length];
                for (int i = 0; i < a.Length; i++)
                {
                    result[i] = a[i] - b[i];
                }
                return result;
            }
            else
            {
                // return 0
                double[] result = new double[1];

                return result;
            }
        }

        public static double[] Cumtrapz(double[] x, double[] y)
        {
            // Area calculation of function y vs x axis using trapezoid rule
            double[] cumArea = new double[y.Length];
            for (int i = 0; i < y.Length - 1; i++)
            {
                cumArea[i + 1] = cumArea[i] + (y[i] + y[i + 1]) * (x[i + 1] - x[i]) / 2;
            }

            return cumArea;
        }

        public static double Trapz(double[] x, double[] y)
        {
            double area = new double();
            double[] cumArea = Cumtrapz(x, y);
            area = cumArea.Last();

            return area;
        }

        public static double[] AbsOfArray(double[] x)
        {
            double[] result = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                result[i] = Math.Abs(x[i]);
            }

            return result;
        }

        public static double Std(double[] x)
        {
            double mean = x.Average();
            double sumOfSquaresOfDifferences = x.Select(val => (val - mean) * (val - mean)).Sum();
            double result = Math.Sqrt(sumOfSquaresOfDifferences / (x.Length - 1));

            return result;
        }

        public static HistogramStruct Hist(double[] inputValues, int n)
        {
            HistogramStruct histStruct;
            histStruct.values = new double[n];
            histStruct.occurr = new int[n];
            Array.Sort(inputValues);

            // Creation of temporary values vector
            double[] valuesTemp = Matlab.Linspace(inputValues.First(), inputValues.Last(), n + 1);

            int i = 0;
            int j = 0;

            while (i < inputValues.Length & j < n)
            {
                if (inputValues[i] <= valuesTemp[j + 1])
                {
                    histStruct.occurr[j] = histStruct.occurr[j] + 1;
                    i++;
                }
                else
                {
                    histStruct.values[j] = (valuesTemp[j] + valuesTemp[j + 1]) / 2;
                    j++;
                }
            }
            // Last element
            histStruct.values[n - 1] = (valuesTemp[n - 1] + valuesTemp[n]) / 2;

            if (histStruct.occurr.Sum() < inputValues.Length)

                histStruct.occurr[n - 1] = histStruct.occurr[n - 1] + 1;

            return histStruct;

        }

        public static int IntPow(int x, int pow)
        {
            int ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }
    }
}
