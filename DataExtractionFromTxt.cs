using MotorTaskAcquisition;
//using static PreProcessing;
using System.Reflection;
using System;

public class DataExtractionFromTxt
{
    public static HeaderStruct loadHeaderStructureFromFile(string[] importedTxtData)
    {
        // Load header of txt data
        HeaderStruct header;
        string[] stringTemp = importedTxtData[0].Split('\t');
        header.date = stringTemp[1];
        //
        stringTemp = importedTxtData[1].Split('\t');
        header.time = stringTemp[1];
        //
        stringTemp = importedTxtData[2].Split('\t');
        header.patientCode = stringTemp[1];
        //
        stringTemp = importedTxtData[3].Split('\t');
        header.exerCode = stringTemp[1];
        //
        stringTemp = importedTxtData[4].Split('\t');
        header.side = stringTemp[1];
        //
        stringTemp = importedTxtData[5].Split('\t');
        header.rep = int.Parse(stringTemp[1]);
        //
        stringTemp = importedTxtData[6].Split('\t');
        header.nSamples = int.Parse(stringTemp[1]);
        //
        stringTemp = importedTxtData[7].Split('\t');
        header.vers = stringTemp[1];

        return header;
    }

    public static DataStruct loadDataFromTxtFile(string path, out HeaderStruct header)
    {

        // Import all data lines from selected path
        string[] importedData = System.IO.File.ReadAllLines(path);

        // Extract header information
        header = loadHeaderStructureFromFile(importedData);

        // Find the number header's size in terms of rows
        int headerSize = 0;
        if (header.vers.Contains("1.0") || header.vers.Contains("1.1"))
            headerSize = 8;

        int L = header.nSamples;

        // Define temporary time array
        DateTime[] T = new DateTime[L];
        TimeSpan[] T_ts = new TimeSpan[L];

        // Initialize the DataStruct structure and its components
        DataStruct dataLoaded = new DataStruct();
        FieldInfo[] mon = dataLoaded.GetType().GetFields();
        TypedReference tr = __makeref(dataLoaded);

        mon[0].SetValueDirect(tr, new double[L]); // Time axis

        // This for loops iterates for each sensor, imports data and pre-process them. Data are stored in
        // a temporary SensDataStruct variable, then cast into the final structure dataLoaded.
        for (int i = 1; i < mon.Length; i++)
        {
            // A temporary SENS 
            SensDataStruct sensTemp;
            sensTemp.acc = new double[3, L];
            sensTemp.gyr = new double[3, L];
            for (int j = 0; j < L; j++)
            {
                string[] temp = importedData[j + headerSize + 1].Split('\t');
                // Cast timestamps into initialized array and converts dateTime into milliseconds increments.
                if (i == 1)
                {
                    T[j] = DateTime.ParseExact(temp[0], "HH:mm:ss:fff", System.Globalization.CultureInfo.InvariantCulture);
                    T_ts[j] = T[j] - T[0];
                    dataLoaded.t[j] = T_ts[j].TotalMilliseconds / 1000;
                }

                for (int k = 0; k < temp.Length; k++)
                {
                    temp[k] = temp[k].Replace(',', '.');
                }

                // Cast values into initialized structures
                for (int k = 0; k < 3; k++)
                {
                    // Uso l'InvariantCulture per considerare correttamente il punto come separatore decimali
                    sensTemp.acc[k, j] = Double.Parse(temp[1 + 6 * (i - 1) + k], System.Globalization.CultureInfo.InvariantCulture);
                    sensTemp.gyr[k, j] = Double.Parse(temp[4 + 6 * (i - 1) + k], System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            // Finally cast data into the structure
            mon[i].SetValueDirect(tr, sensTemp);
        }
        return dataLoaded;
    }

    public static SensDataStruct[] extractSensorData(DataStruct allSensorsData, HeaderStruct header, string taskSelected)
    {
        SensDataStruct[] extractedSensorData = new SensDataStruct[2];

        switch (taskSelected)
        {
            case "THFv":
            case "THFa":
            case "FTAP":
            case "OPCL":
            case "KINT":
                if (header.side == "Dx" || header.side == "DX")
                    extractedSensorData[0] = allSensorsData.RIND;
                else
                    extractedSensorData[0] = allSensorsData.LIND;
                break;

            case "PSUP":
                if (header.side == "Dx" || header.side == "DX")
                    extractedSensorData[0] = allSensorsData.RWRS;
                else
                    extractedSensorData[0] = allSensorsData.LWRS;
                break;

            case "POST":
            case "HRST":
                extractedSensorData[0] = allSensorsData.LIND;
                extractedSensorData[1] = allSensorsData.RIND;
                break;

            case "TTHP":
            case "HEHE":
            case "HTTP":
            case "HETO":
            case "ROTA":
                if (header.side == "Dx" || header.side == "DX")
                    extractedSensorData[0] = allSensorsData.RFTT;
                else
                    extractedSensorData[0] = allSensorsData.LFTT;
                break;

            case "FRST":
                extractedSensorData[0] = allSensorsData.LFTT;
                extractedSensorData[1] = allSensorsData.RFTT;
                break;

            case "GTAS":
                // For GTAS this function is not called because it utilises all sensors data
                break;

            case "STUP":
                extractedSensorData[0] = allSensorsData.RFTT;
                break;
        }

        return extractedSensorData;
    }
}
