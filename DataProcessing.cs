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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;
using System.Data;


namespace MotorTaskAcquisition
{
    class DataProcessing
    {

        //int[] numbOfFeatures = { 20, 20, 20, 20, 20, 20, 9, 45, 9, 20, 5, 20, 13, 45, 39, 5, 1 };

        //public static string[] handTapsFeatureExpl = { "Velocità media - prime 10 rip.", "Dev. standard su velocità - prime 10 rip.", "Amp. media - prime 10 rip.", "Dev. standard su amp. media - prime 10 rip.", "Rid. amp. fase iniziale mov.- prime 10 rip.",
        //        "Rid. amp. fase centrale mov.- prime 10 rip.", "Rid. amp. fase finale mov.- prime 10 rip.", "Numero di interruzioni - prime 10 rip.", "Numero di ripetizioni",  "Amp. media - 10 sec.", "Dev. standard su amp. media - 10 sec.",
        //        "Vel. ang. media di apertura - 10 sec.", "Dev. standard della vel. ang. media di apertura - 10 sec.", "Vel. ang. media di chiusura - 10 sec.", "Dev. standard della vel. ang. media di chiusura - 10 sec.",
        //        "Stima consumo energetico - 10 sec.", "Numero di interruzioni - 10 sec.", "Rid. amp. fase iniziale mov.- 10 sec.", "Rid. amp. fase centrale mov. - 10 sec.", "Rid. amp. fase finale mov.- 10 sec." };
        //public static string[] footTapsFeatureExpl = { "Velocità media - prime 10 rip.", "Dev. standard su velocità - prime 10 rip.", "Amp. media angolo di sollevamento - prime 10 rip.", "Dev. standard su amp. media - prime 10 rip.", "Rid. amp. fase iniziale mov.- prime 10 rip.",
        //        "Rid. amp. fase centrale mov.- prime 10 rip.", "Rid. amp. fase finale mov.- prime 10 rip.", "Numero di interruzioni - prime 10 rip.", "Numero di ripetizioni",  "Amp. media angolo di sollevamento - 10 sec.", "Dev. standard su amp. media - 10 sec.",
        //        "Vel. ang. media di sollevamento - 10 sec.", "Dev. standard della vel. ang. media di sollevamento - 10 sec.", "Vel. ang. media di abbassamento - 10 sec.", "Dev. standard della vel. ang. media di abbassamento - 10 sec.",
        //        "Stima consumo energetico - 10 sec.", "Numero di interruzioni - 10 sec.", "Rid. amp. fase iniziale mov.- 10 sec.", "Rid. amp. fase centrale mov. - 10 sec.", "Rid. amp. fase finale mov.- 10 sec." };

        //public static string[] HETOFeatureExpl = { "Numero di tapping completi tacco/punta - 10 sec.", "Amp. media angolo sollevamento avampiede - 10 sec.", "Dev. standard su amp. media movimento avampiede - 10 sec.", "Amp. media angolo sollevamento tallone - 10 sec.",
        //        "Dev. standard su amp. media movimento tallone - 10 sec.", "Vel. ang. media sollevamento avampiede - 10 sec.", "Dev. standard della vel. ang. media sollevamento avampiede - 10 sec.", "Vel. ang. media sollevamento tallone - 10 sec.",
        //        "Dev. standard della vel. ang. media sollevamento tallone - 10 sec.", "Stima consumo energetico - 10 sec.", "Frequenza di tapping dell'avampiede - 10 sec.", "Frequenza di tapping del tallone - 10 sec.", "Numero di interruzioni - 10 sec." };

        //public static string[] HEHEFeatureExpl = { "Numero di tapping completi - 10 sec.", "Frequenza di tapping del piede", "Potenza media del segnale dell'accelerometro - 10 sec.", "Picco di potenza del segnale dell'accelerometro - 10 sec.", "Stima consumo energetico" };

        //public static string[] HRSTFeatureExpl = { "Potenza % accelerometro - banda [3.5-7.5] Hz - 32 sec." , "Potenza % giroscopio - banda [3.5-7.5] Hz - 32 sec." , "Potenza % accelerometro - banda [8-12] Hz - 32 sec." , "Potenza % giroscopio - banda [8-12] Hz - 32 sec." , "Potenza media accelerometro - 32 sec." ,
        //        "Potenza media giroscopio - 32 sec." , "Frequenza fondamentale dell’accelerometro - 32 sec." , "Frequenza fondamentale del giroscopio - 32 sec." , "Stima consumo energetico - 32 sec." , "Potenza % accelerometro - banda [3.5-7.5] Hz - intervallo 1-8 sec." ,
        //        "Potenza % giroscopio - banda [3.5-7.5] Hz - intervallo 1-8 sec." , "Potenza % accelerometro - banda [8-12] Hz - intervallo 1-8 sec." , "Potenza % giroscopio - banda [8-12] Hz - intervallo 1-8 sec." , "Potenza media accelerometro - intervallo 1-8 sec." , "Potenza media giroscopio - intervallo 1-8 sec." ,
        //        "Frequenza fondamentale dell’accelerometro - intervallo 1-8 sec." , "Frequenza fondamentale del giroscopio - intervallo 1-8 sec." , "Stima consumo energetico - intervallo 1-8 sec." , "Potenza % accelerometro - banda [3.5-7.5] Hz - intervallo 8-16 sec." , "Potenza % giroscopio - banda [3.5-7.5] Hz - intervallo 8-16 sec." ,
        //        "Potenza % accelerometro - banda [8-12] Hz - intervallo 8-16 sec." , "Potenza % giroscopio - banda [8-12] Hz - intervallo 8-16 sec." , "Potenza media accelerometro - intervallo 8-16 sec." , "Potenza media giroscopio - intervallo 8-16 sec." , "Frequenza fondamentale dell’accelerometro - intervallo 8-16 sec." ,
        //        "Frequenza fondamentale del giroscopio - intervallo 8-16 sec." , "Stima consumo energetico - intervallo 8-16 sec." , "Potenza % accelerometro - banda [3.5-7.5] Hz - intervallo 16-24 sec." , "Potenza % giroscopio - banda [3.5-7.5] Hz - intervallo 16-24 sec." , "Potenza % accelerometro - banda [8-12] Hz - intervallo 16-24 sec." ,
        //        "Potenza % giroscopio - banda [8-12] Hz - intervallo 16-24 sec." , "Potenza media accelerometro - intervallo 16-24 sec." , "Potenza media giroscopio - intervallo 16-24 sec." , "Frequenza fondamentale dell’accelerometro - intervallo 16-24 sec." , "Frequenza fondamentale del giroscopio - intervallo 16-24 sec." ,
        //        "Stima consumo energetico - intervallo 16-24 sec." , "Potenza % accelerometro - banda [3.5-7.5] Hz - intervallo 24-32 sec." , "Potenza % giroscopio - banda [3.5-7.5] Hz - intervallo 24-32 sec." , "Potenza % accelerometro - banda [8-12] Hz - intervallo 24-32 sec." , "Potenza % giroscopio - banda [8-12] Hz - intervallo 24-32 sec." ,
        //        "Potenza media accelerometro - intervallo 24-32 sec." , "Potenza media giroscopio - intervallo 24-32 sec." , "Frequenza fondamentale dell’accelerometro - intervallo 24-32 sec." , "Frequenza fondamentale del giroscopio - intervallo 24-32 sec." , "Stima consumo energetico - intervallo 24-32 sec." };

        //public static string[] FRSTFeatureExpl = HRSTFeatureExpl;

        //public static string[] POSTFeatureExpl = { "Potenza % accelerometro - banda [3.5-7.5] Hz - 10 sec." , "Potenza % giroscopio - banda [3.5-7.5] Hz - 10 sec." , "Potenza % accelerometro - banda [8-12] Hz - 10 sec." , "Potenza % giroscopio - banda [8-12] Hz - 10 sec." , "Potenza media accelerometro - 10 sec." ,
        //        "Potenza media giroscopio - 10 sec." , "Frequenza fondamentale dell’accelerometro - 10 sec." , "Frequenza fondamentale del giroscopio - 10 sec." , "Stima consumo energetico - 10 sec." };

        //public static string[] KINTFeatureExpl = { "Potenza % accelerometro - banda [3.5-7.5] Hz" , "Potenza % giroscopio - banda [3.5-7.5] Hz" , "Potenza % accelerometro - banda [8-12] Hz" , "Potenza % giroscopio - banda [8-12] Hz" , "Potenza media accelerometro" ,
        //        "Potenza media giroscopio" , "Frequenza fondamentale dell’accelerometro" , "Frequenza fondamentale del giroscopio" , "Stima consumo energetico" };

        //public static string[] STUPFeatureExpl = { "Tempo di alzata (s)" };

        //public static string[] ROTAFeatureExpl = { "Tempo necessario per effettuare la rotazione (s)", "Numero di passi", "Frequenza di rotazione (steps/s)", "Tempo totale di stazionamento del piede a terra (s)", "T. di stazionamento perc. rispetto al tempo totale di rotazione" };

        //public static string[] GTASFeatureExpl = { "Tempo necessario per effettuare la camminata (s)","Numero di passi", "Velocità media di camminata (m/s)", "Ampiezza media della falcata - piede destro (m)", "Ampiezza media della falcata - piede sinistro (m)",
        //        "Tempo medio di stride - piede destro (s)","Dev. standard tempo medio stride - piede destro (s)","Tempo medio di stride - piede sinistro (s)","Dev. standard tempo medio stride - piede sinistro (s)","Tempo medio di volo - piede destro (s)",
        //        "Dev. standard tempo medio volo - piede destro (s)","Tempo medio di volo - piede sinistro (s)","Dev. standard tempo medio volo - piede sinistro (s)","Tempo medio di stazionamento - piede destro (s)","Dev. standard tempo di stazionamento - piede destro (s)",
        //        "Tempo medio di stazionamento - piede sinistro (s)","Dev. standard tempo di stazionamento - piede sinistro (s)","T. di stazionamento perc. rispetto al t. totale - piede destro","T. di stazionamento perc. rispetto al t. totale - piede sinistro",
        //        "Escursione angolare del movimento dorso/plantare - piede destro [gradi]","Dev. standard esc. angolare movimento dorso/plantare - piede destro [gradi]","Escursione angolare del movimento dorso/plantare - piede sinistro [gradi]","Dev. standard esc. angolare movimento dorso/plantare - piede sinistro [gradi]",
        //        "Numero di oscillazioni - braccio destro","Numero di oscillazioni - braccio sinistro","Ampiezza angolare media dell'oscillazione del braccio destro (gradi/s)","Dev. standard amp. angolare oscillazione braccio destro (gradi/s)","Ampiezza angolare media dell'oscillazione del braccio sinistro (gradi/s)",
        //        "Dev. standard amp. angolare oscillazione braccio sinistro (gradi/s)","Vel. angolare media movimento in avanti - braccio destro (gradi/s)","Dev. standard vel. ang. media movimento in avanti - braccio destro (gradi/s)","Vel. angolare media movimento in avanti - braccio sinistro (gradi/s)",
        //        "Dev. standard vel. ang. media movimento in avanti - braccio sinistro (gradi/s)","Vel. angolare media movimento indietro - braccio destro (gradi/s)","Dev. standard vel. ang. media movimento indietro - braccio destro (gradi/s)","Vel. angolare media movimento indietro - braccio sinistro (gradi/s)",
        //        "Dev. standard vel. ang. media movimento indietro - braccio sinistro (gradi/s)","Stima consumo energetico - braccio destro","Stima consumo energetico - braccio sinistro"};


        public static FeaturesRepMovStruct processSingleTask(string filePath)
        {
            FeaturesRepMovStruct featuresRepMov;

            //// Processed data for graph plot
            //double[] features = new double[20];

            // Structure used for raw extraction of sensorData (it can be from one sensor or two, as in case of FRST, HRST and POST tasks)
            SensDataStruct[] extractedSensorData = new SensDataStruct[2];

            //// Files only for HRST, FRST and POST tasks
            //SensDataStruct sensorDataRight = new SensDataStruct();
            //SensDataStruct sensorDataLeft = new SensDataStruct();

            // Files for other tasks
            SensDataStruct sensorData = new SensDataStruct();

            // Extract raw data from txt file
            HeaderStruct header;
            DataStruct allSensorsData = DataExtractionFromTxt.loadDataFromTxtFile(filePath, out header);



            //// ESEMPIO, GIUSTO PER PROVARE LA STAMPA DEL GRAFICO
            //processedSignal[0] = allSensorsData.t;
            //double[] processedData = new double[allSensorsData.t.Length];



            // Extract the task name from the header
            string taskSelected = header.exerCode;

            // Selection of useful data for the selected task
            //if (taskSelected != "GTAS")
            //{
            extractedSensorData = DataExtractionFromTxt.extractSensorData(allSensorsData, header, taskSelected);

            sensorData = extractedSensorData[0];
            //switch (taskSelected)
            //{
            //    case "FRST":
            //    case "HRST":
            //    case "POST":
            //        sensorDataLeft = extractedSensorData[0];
            //        sensorDataRight = extractedSensorData[1];
            //        break;

            //    default:
            //        // For other tasks the useful sensor data is only in the first element of extractedSensorData
            //        sensorData = extractedSensorData[0];
            //        break;
            //}
            //}

            // Definition of all the possible features structs
            //FeaturesRepMovStruct featuresRepMov;
            //FeaturesTremorStruct[] featuresTremor = new FeaturesTremorStruct[2];
            //FeaturesHEHEStruct featuresHEHE;
            //FeaturesHETOStruct featuresHETO;
            //FeaturesGTASStruct featuresGTAS;
            //FeaturesROTAStruct featuresROTA;
            //FeaturesSTUPStruct featuresSTUP;

            //// Definition of the datatables
            //DataColumn[] columns1 = { new DataColumn("Feature codes"), new DataColumn("Description"), new DataColumn("Features") };
            //DataColumn[] columns2 = { new DataColumn("Feature codes"), new DataColumn("Description"), new DataColumn("Dx"), new DataColumn("Sx") };
            //DataTable dataTable = new DataTable();

            // Feature extraction, depending on the selected motor task, and dataTable creation

            featuresRepMov = FeatureExtraction_repMovementTasks.featureExtraction_repMovementTasks(taskSelected, header, sensorData, allSensorsData.t);


            //switch (taskSelected)
            //{

            //    case "OPCL":
            //        featuresRepMov = FeatureExtraction_repMovementTasks.featureExtraction_repMovementTasks(taskSelected, header, sensorData, allSensorsData.t, ref processedData);
            //        break;

            //    case "PSUP":
            //        featuresRepMov = FeatureExtraction_repMovementTasks.featureExtraction_repMovementTasks(taskSelected, header, sensorData, allSensorsData.t, ref processedData);
            //        dataTable.Columns.AddRange(columns1);
            //        DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRepMov, handTapsFeatureExpl, "PSUP", false);
            //        break;


            //case "THFv":
            //    featuresRepMov = FeatureExtraction_repMovementTasks.featureExtraction_repMovementTasks(taskSelected, header, sensorData, allSensorsData.t, ref processedData);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRepMov, handTapsFeatureExpl, "THFv", false);



            //    processedSignal[1] = processedData;




            //    break;

            //case "THFa":
            //    featuresRepMov = FeatureExtraction_repMovementTasks.featureExtraction_repMovementTasks(taskSelected, header, sensorData, allSensorsData.t, ref processedData);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRepMov, handTapsFeatureExpl, "THFa", false);

            //    processedSignal[1] = processedData;
            //    break;

            //case "FTAP":
            //    featuresRepMov = FeatureExtraction_repMovementTasks.featureExtraction_repMovementTasks(taskSelected, header, sensorData, allSensorsData.t, ref processedData);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRepMov, handTapsFeatureExpl, "FTAP", false);

            //    processedSignal[1] = processedData;
            //    break;

            //case "OPCv":
            //    featuresRepMov = FeatureExtraction_repMovementTasks.featureExtraction_repMovementTasks(taskSelected, header, sensorData, allSensorsData.t, ref processedData);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRepMov, handTapsFeatureExpl, "OPCv", false);
            //    break;

            //case "OPCa":
            //    featuresRepMov = FeatureExtraction_repMovementTasks.featureExtraction_repMovementTasks(taskSelected, header, sensorData, allSensorsData.t, ref processedData);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRepMov, handTapsFeatureExpl, "OPCa", false);
            //    break;

            //case "PSUP":
            //    featuresRepMov = FeatureExtraction_repMovementTasks.featureExtraction_repMovementTasks(taskSelected, header, sensorData, allSensorsData.t, ref processedData);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRepMov, handTapsFeatureExpl, "PSUP", false);
            //    break;

            //case "POST":
            //    featuresTremor[0] = FeatureExtraction_TremorTasks.featureExtraction_Tremors(taskSelected, header, sensorDataRight, allSensorsData.t);
            //    featuresTremor[1] = FeatureExtraction_TremorTasks.featureExtraction_Tremors(taskSelected, header, sensorDataLeft, allSensorsData.t);

            //    FeaturesTremor9Struct featuresRedefinedRightPOST = DataTableManager.redefine9TremorFeaturesStruct(featuresTremor[0]);
            //    FeaturesTremor9Struct featuresRedefinedLeftPOST = DataTableManager.redefine9TremorFeaturesStruct(featuresTremor[1]);
            //    dataTable.Columns.AddRange(columns2);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRedefinedRightPOST, POSTFeatureExpl, "POST", false);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRedefinedLeftPOST, POSTFeatureExpl, "POST", true);
            //    break;

            //case "HRST":
            //    featuresTremor[0] = FeatureExtraction_TremorTasks.featureExtraction_Tremors(taskSelected, header, sensorDataRight, allSensorsData.t);
            //    featuresTremor[1] = FeatureExtraction_TremorTasks.featureExtraction_Tremors(taskSelected, header, sensorDataLeft, allSensorsData.t);

            //    FeaturesTremor45Struct featuresRedefinedRightHRST = DataTableManager.redefine45TremorFeaturesStruct(featuresTremor[0]);
            //    FeaturesTremor45Struct featuresRedefinedLeftHRST = DataTableManager.redefine45TremorFeaturesStruct(featuresTremor[1]);
            //    dataTable.Columns.AddRange(columns2);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRedefinedRightHRST, HRSTFeatureExpl, "HRST", false);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRedefinedLeftHRST, HRSTFeatureExpl, "HRST", true);
            //    break;

            //case "KINT":
            //    featuresTremor[0] = FeatureExtraction_TremorTasks.featureExtraction_Tremors(taskSelected, header, sensorData, allSensorsData.t);

            //    FeaturesTremor9Struct featuresRedefined = DataTableManager.redefine9TremorFeaturesStruct(featuresTremor[0]);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRedefined, KINTFeatureExpl, "KINT", false);
            //    break;

            //case "TTHP":
            //    featuresRepMov = FeatureExtraction_repMovementTasks.featureExtraction_repMovementTasks(taskSelected, header, sensorData, allSensorsData.t, ref processedData);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRepMov, footTapsFeatureExpl, "TTHP", false);
            //    break;

            //case "HEHE":
            //    featuresHEHE = FeatureExtraction_HEHE.featureExtraction_HEHE(taskSelected, header, sensorData, allSensorsData.t);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresHEHE, HEHEFeatureExpl, "HEHE", false);
            //    break;

            //case "HTTP":
            //    featuresRepMov = FeatureExtraction_repMovementTasks.featureExtraction_repMovementTasks(taskSelected, header, sensorData, allSensorsData.t, ref processedData);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRepMov, footTapsFeatureExpl, "HTTP", false);
            //    break;

            //case "HETO":
            //    featuresHETO = FeatureExtraction_HETO.featureExtraction_HETO(taskSelected, header, sensorData, allSensorsData.t);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresHETO, HETOFeatureExpl, "HETO", false);
            //    break;

            //case "FRST":
            //    featuresTremor[0] = FeatureExtraction_TremorTasks.featureExtraction_Tremors(taskSelected, header, sensorDataRight, allSensorsData.t);
            //    featuresTremor[1] = FeatureExtraction_TremorTasks.featureExtraction_Tremors(taskSelected, header, sensorDataLeft, allSensorsData.t);

            //    FeaturesTremor45Struct featuresRedefinedRightFRST = DataTableManager.redefine45TremorFeaturesStruct(featuresTremor[0]);
            //    FeaturesTremor45Struct featuresRedefinedLeftFRST = DataTableManager.redefine45TremorFeaturesStruct(featuresTremor[1]);
            //    dataTable.Columns.AddRange(columns2);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRedefinedRightFRST, FRSTFeatureExpl, "FRST", false);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresRedefinedLeftFRST, FRSTFeatureExpl, "FRST", true);
            //    break;

            //case "GTAS":
            //    featuresGTAS = FeatureExtraction_GTAS.featureExtraction_GTAS(taskSelected, header, allSensorsData, _sharedData.gaitDistance);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresGTAS, GTASFeatureExpl, "GTAS", false);
            //    break;

            //case "ROTA":
            //    featuresROTA = FeatureExtraction_ROTA.featureExtraction_ROTA(taskSelected, header, sensorData, allSensorsData.t);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresROTA, ROTAFeatureExpl, "ROTA", false);
            //    break;

            //case "STUP":
            //    featuresSTUP = FeatureExtraction_STUP.featureExtraction_STUP(taskSelected, header, sensorData, allSensorsData.t);
            //    dataTable.Columns.AddRange(columns1);
            //    DataTableManager.convertFeaturesToDataTable(ref dataTable, featuresSTUP, STUPFeatureExpl, "STUP", false);
            //    break;
            //}

            //// Fill the dataGridView on the GUI
            //_dataGridView.DataSource = dataTable;
            //_dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ////_dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            return featuresRepMov;
        }



    }

}