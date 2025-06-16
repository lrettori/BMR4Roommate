using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTaskAcquisition
{
    public struct SensDataStruct
    {
        public double[,] acc;
        public double[,] gyr;
    }

    public struct DataStruct
    {
        //
        public double[] t;
        //
        public SensDataStruct LWRS;
        public SensDataStruct LTMB;
        public SensDataStruct LIND;
        public SensDataStruct RWRS;
        public SensDataStruct RTMB;
        public SensDataStruct RIND;
        public SensDataStruct LFTT;
        public SensDataStruct RFTT;
    }

    public struct HeaderStruct
    {
        public string date;
        public string time;
        public string patientCode;
        public string exerCode;
        public string side;
        public int rep;
        public int nSamples;
        public string vers;
    }

    public struct FeaturesRepMovStruct
    {
        public string exerCode;
        public bool isFeatureCalculated;
        public double vel10;
        public double vel10SD;
        public double exc10;
        public double exc10SD;
        public double dec10B;
        public double dec10M;
        public double dec10E;
        //public int hes10;
        public int interr10;
        //public int frz10;
        public int taps;
        public double exc;
        public double excSD;
        public double wo;
        public double woSD;
        public double wc;
        public double wcSD;
        public double IAV;
        //public int hes;
        public int interr;
        //public int frz;
        public double decB;
        public double decM;
        public double decE;
    }

    public struct HistogramStruct
    {
        public double[] values;
        public int[] occurr;
    }
}
