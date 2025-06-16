using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;

using System.Globalization;
using Microsoft.WindowsAPICodePack.Dialogs;
using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;


namespace MotorTaskAcquisition
{
    public partial class ChildForm1 : Form
    {
        string headerVersion = "1.0";
        int headerSize = 8;

        static SerialPort _serialport1;
        static int dataReceived = 0;
        bool flagPortOpened = false;

        int samplingFreq;
        public int sizeData = 98;
        byte[] serialbuf = new byte[98];

        int d = 0;
        int l = 0;
        int er = 0;
        int pack = 0;

        static double Acc_Sens = 0.244;
        static double Gyr_Sens = 70;

        //static string specifier = "D";
        //static string specifier; = "F";
        static CultureInfo culture = CultureInfo.CreateSpecificCulture("en-CA");
        static float s = 1;

        static bool recordData = false;
        int counter = 0;
        int counterPacketSaved = 0;

        //static string appendText;
        private StringBuilder _dataBuffer = new StringBuilder();
        private const int PacketThreshold = 25; // Numero di pacchetti prima di scrivere su file
        private int _packetCounter = 0;

        byte lastByte = 0;
        byte[] lastWord = new byte[42];
        //int indexLastWord = 0;

        string outputType = "Float";

        Byte[] acc_x_0 = new Byte[2]; Byte[] acc_y_0 = new Byte[2]; Byte[] acc_z_0 = new Byte[2];
        Byte[] gyr_x_0 = new Byte[2]; Byte[] gyr_y_0 = new Byte[2]; Byte[] gyr_z_0 = new Byte[2];

        Byte[] acc_x_1 = new Byte[2]; Byte[] acc_y_1 = new Byte[2]; Byte[] acc_z_1 = new Byte[2];
        Byte[] gyr_x_1 = new Byte[2]; Byte[] gyr_y_1 = new Byte[2]; Byte[] gyr_z_1 = new Byte[2];

        Byte[] acc_x_2 = new Byte[2]; Byte[] acc_y_2 = new Byte[2]; Byte[] acc_z_2 = new Byte[2];
        Byte[] gyr_x_2 = new Byte[2]; Byte[] gyr_y_2 = new Byte[2]; Byte[] gyr_z_2 = new Byte[2];

        Byte[] acc_x_3 = new Byte[2]; Byte[] acc_y_3 = new Byte[2]; Byte[] acc_z_3 = new Byte[2];
        Byte[] gyr_x_3 = new Byte[2]; Byte[] gyr_y_3 = new Byte[2]; Byte[] gyr_z_3 = new Byte[2];

        Byte[] acc_x_4 = new Byte[2]; Byte[] acc_y_4 = new Byte[2]; Byte[] acc_z_4 = new Byte[2];
        Byte[] gyr_x_4 = new Byte[2]; Byte[] gyr_y_4 = new Byte[2]; Byte[] gyr_z_4 = new Byte[2];

        Byte[] acc_x_5 = new Byte[2]; Byte[] acc_y_5 = new Byte[2]; Byte[] acc_z_5 = new Byte[2];
        Byte[] gyr_x_5 = new Byte[2]; Byte[] gyr_y_5 = new Byte[2]; Byte[] gyr_z_5 = new Byte[2];

        Byte[] acc_x_6 = new Byte[2]; Byte[] acc_y_6 = new Byte[2]; Byte[] acc_z_6 = new Byte[2];
        Byte[] gyr_x_6 = new Byte[2]; Byte[] gyr_y_6 = new Byte[2]; Byte[] gyr_z_6 = new Byte[2];

        Byte[] acc_x_7 = new Byte[2]; Byte[] acc_y_7 = new Byte[2]; Byte[] acc_z_7 = new Byte[2];
        Byte[] gyr_x_7 = new Byte[2]; Byte[] gyr_y_7 = new Byte[2]; Byte[] gyr_z_7 = new Byte[2];

        string sensor0;
        string sensor1;
        string sensor2;
        string sensor3;
        string sensor4;
        string sensor5;
        string sensor6;
        string sensor7;

        bool flagBatteryTest = false;
        string sensorNull = "0\t0\t0\t0\t0\t0";
        static string sensorNullFloat = "0.000\t0.000\t0.000\t0.000\t0.000\t0.000";
        int counterLHConnected = 0;
        int counterRHConnected = 0;
        int counterLFConnected = 0;
        int counterRFConnected = 0;
        int dataCountedForBatteryTest = 0;

        // Per il meccanismo di recupero del flusso dati in arrivo dal dongle, nel caso di perdita di qualche byte
        bool lastMessageReceivedCorrectly = true;
        int lastEndBytePosition = 0;

        TimeSpan timespan;
        DateTime timestamp;

        string[] taskDescriptionImported;
        List<string> tasksWithoutSide = new List<string> { "FRST", "GTAS", "HRST", "POST", "STUP" };
        string userType = "User";

        static int sensVers = 1;

        bool[] isSensorConnected = new bool[8];

        int counterTimer2 = 0;

        static int numbOfTasks = 17;
        int[] timing3Beeps = new int[3] { 3, 13, 16 };
        int[] timing2Beeps = new int[2] { 3, 35 };
        static string[] shortExercise = new string[12] { "THFv", "THFa", "FTAP", "OPCv", "OPCa", "OPCL", "PSUP", "TTHP", "HEHE", "HTTP", "HETO", "POST" };
        static string[] longExercise = new string[2] { "HRST", "FRST" };
        static string[] manualStop = new string[4] { "KINT", "GTAS", "ROTA", "STUP" };

        // String arrays for checking the connection of required sensors
        static string[] upperLimbSingleSideEx = new string[8] { "THFv", "THFa", "FTAP", "OPCv", "OPCa", "OPCL", "PSUP", "KINT" };
        static string[] upperLimbDualSideEx = new string[2] { "POST", "HRST" };
        static string[] lowerLimbSingleSideEx = new string[5] { "TTHP", "HEHE", "HTTP", "HETO", "STUP" };
        static string[] lowerLimbDualSideEx = new string[1] { "FRST" };
        static string[] totalBodyExercise = new string[2] { "GTAS", "ROTA" };

        string createText = "Timestamps" + "\t" + "\t" +
                    "Acc_x_0" + "\t" + "Acc_y_0" + "\t" + "Acc_z_0" + "\t" + "Gyr_x_0" + "\t" + "Gyr_y_0" + "\t" + "Gyr_z_0" + "\t" +
                    "Acc_x_1" + "\t" + "Acc_y_1" + "\t" + "Acc_z_1" + "\t" + "Gyr_x_1" + "\t" + "Gyr_y_1" + "\t" + "Gyr_z_1" + "\t" +
                    "Acc_x_2" + "\t" + "Acc_y_2" + "\t" + "Acc_z_2" + "\t" + "Gyr_x_2" + "\t" + "Gyr_y_2" + "\t" + "Gyr_z_2" + "\t" +
                    "Acc_x_3" + "\t" + "Acc_y_3" + "\t" + "Acc_z_3" + "\t" + "Gyr_x_3" + "\t" + "Gyr_y_3" + "\t" + "Gyr_z_3" + "\t" +
                    "Acc_x_4" + "\t" + "Acc_y_4" + "\t" + "Acc_z_4" + "\t" + "Gyr_x_4" + "\t" + "Gyr_y_4" + "\t" + "Gyr_z_4" + "\t" +
                    "Acc_x_5" + "\t" + "Acc_y_5" + "\t" + "Acc_z_5" + "\t" + "Gyr_x_5" + "\t" + "Gyr_y_5" + "\t" + "Gyr_z_5" + "\t" +
                    "Acc_x_6" + "\t" + "Acc_y_6" + "\t" + "Acc_z_6" + "\t" + "Gyr_x_6" + "\t" + "Gyr_y_6" + "\t" + "Gyr_z_6" + "\t" +
                    "Acc_x_7" + "\t" + "Acc_y_7" + "\t" + "Acc_z_7" + "\t" + "Gyr_x_7" + "\t" + "Gyr_y_7" + "\t" + "Gyr_z_7" + "\t" + Environment.NewLine;

        string filepath = "";
        string filename = "";

        string[] DATA = new string[0];

        FeaturesRepMovStruct features;

        /////// Gestione multi-thread per lettura dati da seriale e scrittura su file txt
        //private readonly ConcurrentQueue<string> _dataQueue = new ConcurrentQueue<string>();
        //private readonly AutoResetEvent _dataAvailable = new AutoResetEvent(false);
        //private volatile bool _isWriting = false; // Flag per indicare se la scrittura è attiva
        //private volatile bool _isRunning = false; // Per fermare il thread dedicato
        //private string _currentFilePath = null; // Percorso del file attivo
        ///////

        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);

        public ChildForm1()
        {
            InitializeComponent();
            comboBoxSerialPorts.DropDown += ComboBoxSerialPorts_DropDown;

            // Aggiungi il gestore per la chiusura del form
            this.FormClosing += ChildForm1_FormClosing;
        }
        
        private void ChildForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnProgramExit(); // Esegui la pulizia prima di chiudere
        }

        private void OnProgramExit()
        {
            //_isRunning = false; // Ferma il thread
            //_dataAvailable.Set(); // Sblocca eventuali attese del thread di scrittura

            //StopWriting().ConfigureAwait(false); ; // Ferma la scrittura e svuota i dati residui

            //// Chiudi la connessione seriale (se aperta)
            //if (_serialport1 != null && _serialport1.IsOpen)
            //{
            //    _serialport1.Close();  // Chiudi la seriale
            //}
        }

        private void ChildForm1_Load(object sender, EventArgs e)
        {
            string path = "motor_task_description.txt";
            taskDescriptionImported = System.IO.File.ReadAllLines(path);
            //string[] DATA = new string[0];
            //COM_init();
            initColors();

        }

        private void ComboBoxSerialPorts_DropDown(object sender, EventArgs e)
        {
            COM_init(); // Aggiorna la lista delle porte COM
        }

        private void COM_init()
        {
            comboBoxSerialPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBoxSerialPorts.Items.Add(port);
            }

            if (_serialport1 == null)
            {
                _serialport1 = new SerialPort();
                _serialport1.Parity = Parity.None;
                _serialport1.DataBits = 8;
                _serialport1.StopBits = StopBits.One;
                _serialport1.ReceivedBytesThreshold = sizeData;
                //_serialport1.ReceivedBytesThreshold = 1;
                _serialport1.ReadBufferSize = 4096;
                _serialport1.WriteBufferSize = 4096;
                _serialport1.WriteTimeout = 500;
                _serialport1.ReadTimeout = 3000;
                _serialport1.BaudRate = 460800;
                //_serialport1.BaudRate = 115200;
                _serialport1.DtrEnable = true;
                //_serialport1.PortName = "COM13";
            }
                
            
        }

        // Append new data line to existing string array, expanding it. 
        string[] Add(string[] array, string newValue)
        {
            int newLength = array.Length + 1;
            string[] result = new string[newLength];
            for (int i = 0; i < array.Length; i++)
                result[i] = array[i];
            result[newLength - 1] = newValue;
            return result;
        }

        public delegate void UpdateTextCallback(string text);

        public void _serialport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                samplingFreq = samplingFreq + 1;
                counter++;
                dataReceived++;

                byte buf1;
                int bytesRead = _serialport1.BytesToRead;

                if (lastMessageReceivedCorrectly)
                {
                    for (int w = 0; w < sizeData; w++)
                    {
                        buf1 = (byte)_serialport1.ReadByte();
                        if (buf1 == 68) d++;
                        if (buf1 == 76) l++;
                        serialbuf[w] = buf1;
                    }
                }
                else
                {
                    // Last packet had some missing bytes so it was discarded, and now realignment is necessary
                    for (int w = (sizeData - (lastEndBytePosition + 1)); w < sizeData; w++)
                    {
                        buf1 = (byte)_serialport1.ReadByte();
                        serialbuf[w] = buf1;
                    }

                    lastMessageReceivedCorrectly = true;
                }

                string rawBytes = Encoding.UTF8.GetString(serialbuf);

                if ((serialbuf[0] == 68) & (serialbuf[97] == 76))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        // Left wrist
                        acc_x_0[i] = serialbuf[1 + i]; acc_y_0[i] = serialbuf[3 + i]; acc_z_0[i] = serialbuf[5 + i];
                        gyr_x_0[i] = serialbuf[7 + i]; gyr_y_0[i] = serialbuf[9 + i]; gyr_z_0[i] = serialbuf[11 + i];
                        // Left thumb
                        acc_x_1[i] = serialbuf[13 + i]; acc_y_1[i] = serialbuf[15 + i]; acc_z_1[i] = serialbuf[17 + i];
                        gyr_x_1[i] = serialbuf[19 + i]; gyr_y_1[i] = serialbuf[21 + i]; gyr_z_1[i] = serialbuf[23 + i];
                        // Left forefinger
                        acc_x_2[i] = serialbuf[25 + i]; acc_y_2[i] = serialbuf[27 + i]; acc_z_2[i] = serialbuf[29 + i];
                        gyr_x_2[i] = serialbuf[31 + i]; gyr_y_2[i] = serialbuf[33 + i]; gyr_z_2[i] = serialbuf[35 + i];
                        // Right wrist
                        acc_x_3[i] = serialbuf[37 + i]; acc_y_3[i] = serialbuf[39 + i]; acc_z_3[i] = serialbuf[41 + i];
                        gyr_x_3[i] = serialbuf[43 + i]; gyr_y_3[i] = serialbuf[45 + i]; gyr_z_3[i] = serialbuf[47 + i];
                        // Right thumb
                        acc_x_4[i] = serialbuf[49 + i]; acc_y_4[i] = serialbuf[51 + i]; acc_z_4[i] = serialbuf[53 + i];
                        gyr_x_4[i] = serialbuf[55 + i]; gyr_y_4[i] = serialbuf[57 + i]; gyr_z_4[i] = serialbuf[59 + i];
                        // Right forefinger
                        acc_x_5[i] = serialbuf[61 + i]; acc_y_5[i] = serialbuf[63 + i]; acc_z_5[i] = serialbuf[65 + i];
                        gyr_x_5[i] = serialbuf[67 + i]; gyr_y_5[i] = serialbuf[69 + i]; gyr_z_5[i] = serialbuf[71 + i];
                        // Left foot
                        acc_x_6[i] = serialbuf[73 + i]; acc_y_6[i] = serialbuf[75 + i]; acc_z_6[i] = serialbuf[77 + i];
                        gyr_x_6[i] = serialbuf[79 + i]; gyr_y_6[i] = serialbuf[81 + i]; gyr_z_6[i] = serialbuf[83 + i];
                        // Right foot
                        acc_x_7[i] = serialbuf[85 + i]; acc_y_7[i] = serialbuf[87 + i]; acc_z_7[i] = serialbuf[89 + i];
                        gyr_x_7[i] = serialbuf[91 + i]; gyr_y_7[i] = serialbuf[93 + i]; gyr_z_7[i] = serialbuf[95 + i];
                    }

                    sensor0 = generate_String(acc_x_0, acc_y_0, acc_z_0, gyr_x_0, gyr_y_0, gyr_z_0, outputType);
                    sensor1 = generate_String(acc_x_1, acc_y_1, acc_z_1, gyr_x_1, gyr_y_1, gyr_z_1, outputType);
                    sensor2 = generate_String(acc_x_2, acc_y_2, acc_z_2, gyr_x_2, gyr_y_2, gyr_z_2, outputType);
                    sensor3 = generate_String(acc_x_3, acc_y_3, acc_z_3, gyr_x_3, gyr_y_3, gyr_z_3, outputType);
                    sensor4 = generate_String(acc_x_4, acc_y_4, acc_z_4, gyr_x_4, gyr_y_4, gyr_z_4, outputType);
                    sensor5 = generate_String(acc_x_5, acc_y_5, acc_z_5, gyr_x_5, gyr_y_5, gyr_z_5, outputType);
                    sensor6 = generate_String(acc_x_6, acc_y_6, acc_z_6, gyr_x_6, gyr_y_6, gyr_z_6, outputType);
                    sensor7 = generate_String(acc_x_7, acc_y_7, acc_z_7, gyr_x_7, gyr_y_7, gyr_z_7, outputType);

                    if (recordData)
                    {
                        string data = timestamp.ToString("HH:mm:ss:fff") + "\t" + sensor0 + "\t" + sensor1 + "\t" + sensor2 + "\t" + sensor3 + "\t" + sensor4 + "\t" + sensor5 + "\t" + sensor6 + "\t" + sensor7 + "\t";
                        //string data = timestamp.ToString("HH:mm:ss:fff") + "\t" + sensor0 + "\t" + sensor1 + "\t" + sensor2 + "\t" + sensor3 + "\t" + sensor4 + "\t" + sensor5 + "\t" + sensor6 + "\t" + sensor7;
                        timestamp = timestamp.AddMilliseconds(20);

                        _packetCounter++;

                        if (recordData)
                        {
                            DATA = Add(DATA, data);
                            counterPacketSaved++;
                            //_packetCounter++;
                        }

                    }
                }
                else
                {
                    er++;
                    lastMessageReceivedCorrectly = false;
                    timestamp = timestamp.AddMilliseconds(20);
                    // Look for the last 'L' in the received message, which is probably in the last positions of the array (in case we missed some bytes on the serial communication)
                    int index = sizeData;
                    bool flagStopByteFound = false;
                    while (index > 0 && flagStopByteFound == false)
                    {
                        index--;
                        if (serialbuf[index] == 76)
                        {
                            flagStopByteFound = true;
                            lastEndBytePosition = index;
                        }
                    }

                    if (flagStopByteFound)
                    {
                        for (int i = 0; i < (sizeData - (lastEndBytePosition + 1)); i++)
                        {
                            serialbuf[i] = serialbuf[lastEndBytePosition + 1 + i];
                        }
                    }
                    else
                        lastEndBytePosition = sizeData - 1;
                }
            }
            catch (TimeoutException)
            {
                // Reached serial port timeout
                MessageBox.Show("Timeout porta seriale", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _serialport1.Close();
                Invoke(new Action(() => HandleTimeout()));
            }

            catch (Exception ex)
            {
                MessageBox.Show("Si è verificato un errore di comunicazione con il dispositivo: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _serialport1.Close();
                buttonOpen.Text = "Open";
                buttonOpen.BackColor = ColorManagement.dangerColor;
                flagPortOpened = false;
                initRedPanelSensors();
                initRedPanelSensors();
            }
        }

        private static string generate_String(Byte[] a_x, Byte[] a_y, Byte[] a_z, Byte[] g_x, Byte[] g_y, Byte[] g_z, string outputType)
        {
            string data_str;

            if (outputType == "Float")
            {
                string specifier = "F";
                culture.NumberFormat.NumberDecimalDigits = 3;
                data_str = ((float)((BitConverter.ToInt16(a_x, 0) * s * 9.81 * (-1)) / 1000) * Acc_Sens).ToString(specifier, culture) + "\t" +
                              ((float)((BitConverter.ToInt16(a_y, 0) * s * 9.81 * (-1)) / 1000) * Acc_Sens).ToString(specifier, culture) + "\t" +
                              ((float)((BitConverter.ToInt16(a_z, 0) * s * 9.81 * (-1)) / 1000) * Acc_Sens).ToString(specifier, culture) + "\t" +
                              ((float)((BitConverter.ToInt16(g_x, 0) * s * (-1)) / 1000) * Gyr_Sens).ToString(specifier, culture) + "\t" +
                              ((float)((BitConverter.ToInt16(g_y, 0) * s * (-1)) / 1000) * Gyr_Sens).ToString(specifier, culture) + "\t" +
                              ((float)((BitConverter.ToInt16(g_z, 0) * s * (-1)) / 1000) * Gyr_Sens).ToString(specifier, culture)/* + "\t" + pack.ToString()*/;

            }
            else
            {
                string specifier = "D";
                data_str = (BitConverter.ToUInt16(a_x, 0)).ToString(specifier, culture) + "\t" +
                                  (BitConverter.ToUInt16(a_y, 0)).ToString(specifier, culture) + "\t" +
                                  (BitConverter.ToUInt16(a_z, 0)).ToString(specifier, culture) + "\t" +
                                  (BitConverter.ToUInt16(g_x, 0)).ToString(specifier, culture) + "\t" +
                                  (BitConverter.ToUInt16(g_y, 0)).ToString(specifier, culture) + "\t" +
                                  (BitConverter.ToUInt16(g_z, 0)).ToString(specifier, culture);
            }


            return data_str;
        }

        private string headerDefiner()
        {
            // Version 1.0, the header is defined by 8 fields: date, time, codice paziente, codice esercizio, lato, ripetizione, N. Samples, versione
            string date = "Date" + "\t" + DateTime.UtcNow.ToString("dd/MM/yyyy");
            string time = "Time" + "\t" + DateTime.UtcNow.ToString("HH:mm:ss");
            string codicePaziente = "Codice Paziente" + "\t" + textBoxID.Text;

            string codiceEsercizio;
            string lato;
            string ripetizione;

            if (checkBoxFreeAcquisition.Checked == false)
            {
                codiceEsercizio = "Codice esercizio" + "\t" + comboBoxTasks.Text;
                lato = "Lato" + "\t";
                if (comboBoxTasks.Text == "POST" || comboBoxTasks.Text == "HRST" || comboBoxTasks.Text == "GTAS" || comboBoxTasks.Text == "STUP")
                    lato = lato + "N/A";
                else
                    lato = lato + comboBoxSide.Text;
                ripetizione = "Ripetizione" + "\t" + comboBoxRep.Text;
            }
            else
            {
                codiceEsercizio = "Codice esercizio" + "\t" + "N/A";
                lato = "Lato" + "\t" + "N/A";
                ripetizione = "Ripetizione" + "\t" + "N/A";
            }

            //string nSamples = "N. Samples"; // Da aggiornare alla chiusura del file
            string nSamples = "N. Samples" + "\t" + DATA.Length.ToString();
            string versione = "Versione" + "\t" + headerVersion;

            string header = date + "\n" + time + "\n" + codicePaziente + "\n" + codiceEsercizio + "\n" + lato + "\n" + ripetizione + "\n" + nSamples + "\n" + versione;
            return header;
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            labelFreq.Text = samplingFreq.ToString();
            samplingFreq = 0;

            // Check sensor connection
            checkSensorConnection();

            // Update color of the boxes indicating which sensor is connected
            updatePanelColors();

            // Developer mode, update of textboxes giving real time feedback on data acquired by the sensors
            if (userType == "Developer")
            {
                updateSensorsTextBoxes();
            }
        }

        private void comboBoxSerialPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            _serialport1.PortName = comboBoxSerialPorts.SelectedItem.ToString();
        }

        private void buttonConnectAll_Click(object sender, EventArgs e)
        {
            if (flagPortOpened)
                _serialport1.Write("at$cntn\r\n");
            else
                MessageBox.Show("Stabilire la connessione con il dongle");
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (buttonOpen.Text == "Open")
            {
                try
                {
                    _serialport1.DataReceived += new SerialDataReceivedEventHandler(_serialport_DataReceived);
                    _serialport1.Open();
                    buttonOpen.Text = "Close";
                    buttonOpen.BackColor = ColorManagement.successColor;
                    flagPortOpened = true;
                    timer1.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore durante l'apertura della porta seriale: {ex.Message}");
                }
            }
            else
            {
                buttonOpen.Text = "Open";
                buttonOpen.BackColor = ColorManagement.dangerColor;
                _serialport1.Close();
                flagPortOpened = false;
                timer1.Enabled = false;
            }
        }

        private void comboBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Change the task description in richTextBoxTaskDescription

            if (userType != "Developer")
            {
                bool found = false;
                richTextBoxTaskDescription.Text = "";
                int i = 0;
                while (!found && i < taskDescriptionImported.Length)
                {
                    if (comboBoxTasks.Text == taskDescriptionImported[i])
                    {
                        found = true;
                        break;
                    }
                    i++;
                }
                if (found)
                {
                    int numbOfRows = 6;
                    for (int j = 0; j < numbOfRows; j++)
                    {
                        string[] taskDescriptionDivided = taskDescriptionImported[i + j + 1].Split(':');

                        if (j == 0)
                        {
                            richTextBoxTaskDescription.SelectionFont = new Font(richTextBoxTaskDescription.Font.FontFamily, 10, FontStyle.Bold);
                        }
                        else
                        {
                            richTextBoxTaskDescription.SelectionFont = new Font(richTextBoxTaskDescription.Font, FontStyle.Bold);
                        }

                        string taskDescription = taskDescriptionDivided[0];
                        richTextBoxTaskDescription.AppendText(taskDescription);
                        if (taskDescriptionDivided.Length > 1)
                        {
                            richTextBoxTaskDescription.SelectionFont = new Font(richTextBoxTaskDescription.Font, FontStyle.Regular);
                            taskDescription = taskDescriptionDivided[1];
                            richTextBoxTaskDescription.AppendText(":" + taskDescription);
                        }
                        richTextBoxTaskDescription.AppendText(Environment.NewLine);
                        richTextBoxTaskDescription.AppendText(Environment.NewLine);
                    }
                }

            }

            // For certain tasks (contained in the list tasksWithoutSide) the definition of a side is not required, hence the side selection is disabled
            if (tasksWithoutSide.Contains(comboBoxTasks.Text))
                comboBoxSide.Enabled = false;
            else
                comboBoxSide.Enabled = true;

            changeFileName();
        }

        private void comboBoxSide_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeFileName();
        }

        private void comboBoxRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeFileName();
        }

        private void checkBoxAutomaticName_CheckedChanged(object sender, EventArgs e)
        {
            changeFileName();
        }

        private void changeFileName()
        {
            // Change file name if automatic and all other fields have been selected
            if (checkBoxAutomaticName.Checked && !String.IsNullOrEmpty(comboBoxSide.Text) && comboBoxSide.Enabled == true && !String.IsNullOrEmpty(comboBoxRep.Text))
            {
                textBoxFileName.Text = comboBoxTasks.Text + "_" + comboBoxSide.Text + "_Ex" + comboBoxRep.Text + ".txt";
            }
            else if ((checkBoxAutomaticName.Checked && comboBoxSide.Enabled == false && !String.IsNullOrEmpty(comboBoxRep.Text)))
            {
                textBoxFileName.Text = comboBoxTasks.Text + "_Ex" + comboBoxRep.Text + ".txt";
            }
        }

        private void buttonSendCommand_Click(object sender, EventArgs e)
        {
            if (flagPortOpened)
            {
                switch (comboBoxBTCommand.Text)
                {
                    case "Scan":
                        _serialport1.Write("at$scan\r\n");
                        break;

                    case "Connect to all":
                        _serialport1.Write("at$cntn\r\n");
                        break;

                    case "Connect to 0":
                        _serialport1.Write("at$cntn_0\r\n");
                        break;

                    case "Connect to 1":
                        _serialport1.Write("at$cntn_1\r\n");
                        break;

                    case "Connect to 2":
                        _serialport1.Write("at$cntn_2\r\n");
                        break;

                    case "Connect to 3":
                        _serialport1.Write("at$cntn_3\r\n");
                        break;

                    case "Disconnect from all":
                        _serialport1.Write("at$dntn\r\n");
                        initSensors("all");
                        break;

                    case "Disconnect from 0":
                        _serialport1.Write("at$dntn_0\r\n");
                        initSensors("left hand");
                        break;

                    case "Disconnect from 1":
                        _serialport1.Write("at$dntn_1\r\n");
                        initSensors("right hand");
                        break;

                    case "Disconnect from 2":
                        _serialport1.Write("at$dntn_2\r\n");
                        initSensors("left foot");
                        break;

                    case "Disconnect from 3":
                        _serialport1.Write("at$dntn_3\r\n");
                        initSensors("right foot");
                        break;
                }
            }
            else
                MessageBox.Show("Stabilire la connessione con il dongle");
        }

        private void comboBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUser.Text == "Developer")
            {
                userType = "Developer";
                textBoxSens0.Visible = true;
                textBoxSens1.Visible = true;
                textBoxSens2.Visible = true;
                textBoxSens3.Visible = true;
                textBoxSens4.Visible = true;
                textBoxSens5.Visible = true;
                textBoxSens6.Visible = true;
                textBoxSens7.Visible = true;
                labelLW.Visible = true;
                labelLT.Visible = true;
                labelLI.Visible = true;
                labelRW.Visible = true;
                labelRT.Visible = true;
                labelRI.Visible = true;
                labelLF.Visible = true;
                labelRF.Visible = true;
                label_ax.Visible = true;
                label_ay.Visible = true;
                label_az.Visible = true;
                label_wx.Visible = true;
                label_wy.Visible = true;
                label_wz.Visible = true;
                richTextBoxTaskDescription.Text = "";
            }
            else
            {
                userType = "User";
                textBoxSens0.Visible = false;
                textBoxSens1.Visible = false;
                textBoxSens2.Visible = false;
                textBoxSens3.Visible = false;
                textBoxSens4.Visible = false;
                textBoxSens5.Visible = false;
                textBoxSens6.Visible = false;
                textBoxSens7.Visible = false;
                labelLW.Visible = false;
                labelLT.Visible = false;
                labelLI.Visible = false;
                labelRW.Visible = false;
                labelRT.Visible = false;
                labelRI.Visible = false;
                labelLF.Visible = false;
                labelRF.Visible = false;
                label_ax.Visible = false;
                label_ay.Visible = false;
                label_az.Visible = false;
                label_wx.Visible = false;
                label_wy.Visible = false;
                label_wz.Visible = false;
            }
        }

        private void initRedPanelSensors()
        {
            panelLH.BackColor = ColorManagement.dangerColor;
            panelRH.BackColor = ColorManagement.dangerColor;
            panelLF.BackColor = ColorManagement.dangerColor;
            panelLF.BackColor = ColorManagement.dangerColor;
        }

        private void HandleTimeout()
        {
            // Timeout serial port
            buttonOpen.Text = "Open";
            buttonOpen.BackColor = ColorManagement.dangerColor;
            initRedPanelSensors();
            initSensors("all");
            flagPortOpened = false;
        }

        private void initSensors(string sensors)
        {
            // Puts zeros in sensors string, depending on input
            switch (sensors)
            {
                case "left hand":
                    sensor0 = sensorNull;
                    sensor1 = sensorNull;
                    sensor2 = sensorNull;
                    break;

                case "right hand":
                    sensor3 = sensorNull;
                    sensor4 = sensorNull;
                    sensor5 = sensorNull;
                    break;

                case "left foot":
                    sensor6 = sensorNull;
                    break;

                case "right foot":
                    sensor7 = sensorNull;
                    break;

                case "all":
                    sensor0 = sensorNull;
                    sensor1 = sensorNull;
                    sensor2 = sensorNull;
                    sensor3 = sensorNull;
                    sensor4 = sensorNull;
                    sensor5 = sensorNull;
                    sensor6 = sensorNull;
                    sensor7 = sensorNull;
                    break;
            }
        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            if (buttonStartStop.Text == "START")
            {
                filepath = textBoxPath.Text;
                if (filepath == "")
                    MessageBox.Show("Indicare un percorso per il salvataggio dei dati");
                else if (textBoxID.Text == "")
                    MessageBox.Show("Indicare l'ID del soggetto");
                else if (checkBoxFreeAcquisition.Checked == false && comboBoxTasks.Text == "")
                    MessageBox.Show("Selezionare un task motorio");
                else if (checkBoxFreeAcquisition.Checked == false && comboBoxSide.Text == "" && !(comboBoxTasks.Text == "POST" || comboBoxTasks.Text == "HRST" || comboBoxTasks.Text == "GTAS" || comboBoxTasks.Text == "STUP" || comboBoxTasks.Text == "FRST"))
                    MessageBox.Show("Selezionare un lato");
                else if (checkBoxFreeAcquisition.Checked == false && comboBoxRep.Text == "" && comboBoxRep.Text == "")
                    MessageBox.Show("Selezionare il numero della ripetizione corrente");

                else
                {
                    // Create folder if it does not exist yet
                    bool pathExists = System.IO.Directory.Exists(filepath);
                    if (!pathExists)
                        System.IO.Directory.CreateDirectory(filepath);

                    // Check the filename
                    filename = textBoxFileName.Text;
                    if (filename == "")
                        MessageBox.Show("Indicare un nome per il file di salvataggio");
                    else
                    {
                        // Create txt file, write the header and start the acquisition
                        if (!filename.Contains(".txt"))
                            filename = filename + ".txt";

                        // Check if the file already exists, if yes, ask permission to overwrite it
                        bool fileExistsCheck = checkExistingFileOverwrite(filepath + '\\' + filename);

                        // Check if the sensors required for the selected motor task are connected
                        bool requiredSensorsConnected = checkRequiredSensorsConnected("Start");

                        // If both conditions are satisfied, start acquisition
                        bool startAcquisition = fileExistsCheck && requiredSensorsConnected;
                        if (startAcquisition)
                        {
                            //// Write the file header
                            //string header = headerDefiner();
                            //header = header + "\n" + createText;

                            //await WriteHeaderToFileAsync(filepath + '\\' + filename, header);

                            // Define first timestamp
                            timestamp = DateTime.UtcNow;

                            // Enable the data straming into txt file
                            counterPacketSaved = 0;
                            _serialport1.Write("Start\r\n"); // Empty the dongle buffers
                            recordData = true;
                            startTimer();
                            //StartWriting(filepath + '\\' + filename); ///// Avvia la scrittura
                            //StartRecording(filepath + '\\' + filename); ///// Avvia la scrittura

                            // Change the button text
                            buttonStartStop.Text = "STOP";
                        }
                    }
                }
            }
            else
            {
                // If the button is "STOP"
                recordData = false;
                Console.Beep();
                stopTimer();
                saveData();
                buttonStartStop.Text = "START";
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {

            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    textBoxPath.Text = dialog.FileName;
                }
            }
        }

        void initColors()
        {
            ColorManagement.StyleButton(buttonOpen, ColorManagement.secondaryColor, Color.White, ColorManagement.secondaryHoverColor, false);
            ColorManagement.StyleButton(buttonSendCommand, ColorManagement.secondaryColor, Color.White, ColorManagement.secondaryHoverColor, true);
            ColorManagement.StyleButton(buttonConnectAll, ColorManagement.secondaryColor, Color.White, ColorManagement.secondaryHoverColor, true);
            ColorManagement.StyleButton(buttonBrowse, ColorManagement.secondaryColor, Color.White, ColorManagement.secondaryHoverColor, true);
            ColorManagement.StyleButton(buttonStartStop, ColorManagement.primaryColor, Color.White, ColorManagement.primaryHoverColor, true);
            ColorManagement.StyleButton(buttonProcess, ColorManagement.primaryColor, Color.White, ColorManagement.primaryHoverColor, true);
        }

        private void panelRH_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            comboBoxUser.Visible = true;
            labelMode.Visible = true;
        }

        private void updatePanelColors()
        {
            if (sensor0 != sensorNull && sensor0 != sensorNullFloat && sensor0 != null)
                panelLH.BackColor = Color.LimeGreen;
            else
                panelLH.BackColor = ColorManagement.dangerColor;
            if (sensor3 != sensorNull && sensor3 != sensorNullFloat && sensor3 != null)
                panelRH.BackColor = Color.LimeGreen;
            else
                panelRH.BackColor = ColorManagement.dangerColor;
            if (sensor6 != sensorNull && sensor6 != sensorNullFloat && sensor6 != null)
                panelLF.BackColor = Color.LimeGreen;
            else
                panelLF.BackColor = ColorManagement.dangerColor;
            if (sensor7 != sensorNull && sensor7 != sensorNullFloat && sensor7 != null)
                panelRF.BackColor = Color.LimeGreen;
            else
                panelRF.BackColor = ColorManagement.dangerColor;
        }

        private void updateSensorsTextBoxes()
        {
            textBoxSens0.Text = sensor0;
            textBoxSens1.Text = sensor1;
            textBoxSens2.Text = sensor2;
            textBoxSens3.Text = sensor3;
            textBoxSens4.Text = sensor4;
            textBoxSens5.Text = sensor5;
            textBoxSens6.Text = sensor6;
            textBoxSens7.Text = sensor7;
        }
        private void startTimer()
        {
            this.counterTimer2 = 0;
            timer2.Enabled = true;
            timer2.Interval = 1000;
            timer2.Start();
        }

        private void stopTimer()
        {
            this.counterTimer2 = 0;
            timer2.Enabled = false;
            timer2.Stop();
            labelTimer.Text = this.counterTimer2.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                this.counterTimer2++;
                labelTimer.Text = this.counterTimer2.ToString();

                if (manualStop.Contains(comboBoxTasks.Text) || checkBoxFreeAcquisition.Checked == true)
                {
                    // Beeps at 3 and when the STOP button is pressed
                    if (counterTimer2 == 3)
                        Console.Beep();
                }

                else if (shortExercise.Contains(comboBoxTasks.Text))
                {
                    // Beeps at 3, 13, 16 seconds
                    if (counterTimer2 == 3 || counterTimer2 == 13)
                        Console.Beep();

                    //await BeepAsync(800, 300);

                    if (counterTimer2 == 16)
                    {
                        recordData = false;
                        Console.Beep();
                        stopTimer();

                        saveData();
                        buttonStartStop.Text = "START";
                    }
                }

                else if (longExercise.Contains(comboBoxTasks.Text))
                {
                    // Beeps at 3, and 35 seconds
                    if (counterTimer2 == 3)
                        Console.Beep();

                    if (counterTimer2 == 35)
                    {
                        recordData = false;
                        Console.Beep();
                        stopTimer();

                        saveData();
                        buttonStartStop.Text = "START";
                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Errore durante l'esecuzione di timer2", "Error");
            }
        }

        private bool checkExistingFileOverwrite(string filePathName)
        {
            bool checkResult = true;

            if (File.Exists(filePathName))
            {
                // If the file already exists, ask if the user wants to delete it and create a new one. If not, don't start the data acquisition
                var selectedOption = MessageBox.Show("Il file è già esistente, vuoi sovrascriverlo?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (selectedOption == DialogResult.Yes)
                    File.Delete(filePathName);
                else
                    checkResult = false;
            }

            return checkResult;
        }

        private bool checkRequiredSensorsConnected(string startOrStop)
        {
            bool checkResult = true;

                if (upperLimbSingleSideEx.Contains(comboBoxTasks.Text))
                {
                    // Upper limb single side exercises require the connection of sensor 0 OR sensor 3, depending on the side selected
                    if (comboBoxSide.Text == "DX")
                        checkResult = isSensorConnected[3];
                    else
                        checkResult = isSensorConnected[0];
                }

                else if (upperLimbDualSideEx.Contains(comboBoxTasks.Text))
                {
                    // Upper limb dual side exercises require the connection of sensor 0 AND sensor 3
                    checkResult = isSensorConnected[0] && isSensorConnected[3];
                }

                else if (lowerLimbSingleSideEx.Contains(comboBoxTasks.Text))
                {
                    // Lower limb single side exercises require the connection of sensor 6 OR sensor 7, depending on the side selected
                    if (comboBoxSide.Text == "DX" || comboBoxTasks.Text == "STUP")
                        checkResult = isSensorConnected[7];
                    else
                        checkResult = isSensorConnected[6];
                }

                else if (lowerLimbDualSideEx.Contains(comboBoxTasks.Text))
                {
                    // Lower limb dual side exercises require the connection of sensor 6 AND sensor 7
                    checkResult = isSensorConnected[6] && isSensorConnected[7];
                }

                else
                {
                    // Total body exercises require the connection of ALL sensors
                    checkResult = isSensorConnected[0] && isSensorConnected[3] && isSensorConnected[6] && isSensorConnected[7];
                }

            if (!checkResult)
            {
                if (startOrStop == "Start")
                    MessageBox.Show("Attenzione, impossibile iniziare l'acquisizione. Stabilire la connessione con tutti i sensori richiesti per questo task motorio", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Attenzione, Uno dei sensori richiesti si è disconnesso durante l'acquisizione. Riconnetterlo e ripetere l'acquisizione corrente", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            return checkResult;
        }

        private void saveData()
        {
            // Check if all the required sensors stayed connected until the end of the acquisition
            bool requiredSensorsConnected = checkRequiredSensorsConnected("Stop");

            if (requiredSensorsConnected)
            {
                // Check if the data acquired by the fingers have repetitions, meaning that they have been accidentally
                // disconnected. In this case the user is informed and data is not saved. Only for sensor version 1
                bool[] areFingersConnected = new bool[2] { true, true };
                if (sensVers == 1 && (isSensorConnected[0] || isSensorConnected[3]))
                    areFingersConnected = checkFingers(DATA, isSensorConnected[0], isSensorConnected[3]);

                if ((areFingersConnected[0]) && (areFingersConnected[1]))
                {
                    string header = headerDefiner();
                    header = header + "\n" + createText;

                    File.WriteAllText(filepath + '\\' + filename, header);
                    File.AppendAllLines(filepath + '\\' + filename, DATA, Encoding.UTF8);

                    DATA = new string[0];
                }
                else
                {
                    DATA = new string[0];

                    string hands = "";
                    if (!areFingersConnected[0] && !areFingersConnected[1])
                        hands = "SX e DX ";
                    else if (!areFingersConnected[0])
                        hands = "SX ";
                    else
                        hands = "DX ";

                    MessageBox.Show("Sensori dita mano " + hands + "disconnessi\nRicollegare il sensore mano e ripetere l'acquisizione corrente", "Error");
                }
            }

            else
            {
                // One or more sensors disconnected during the acquisition
                DATA = new string[0];
            }
        }

        bool[] checkFingers(string[] DATA, bool isLeftHandConnected, bool isRightHandConnected)
        {
            bool[] areFingersConnected = { true, true };
            double[] fingersLeftHandLastMessage = new double[12];
            double[] fingersRightHandLastMessage = new double[12];
            int[] currentRepeatStreak = new int[2]; // Conta quante volte lo stesso messaggio è stato ripetuto di fila

            for (int i = 0; i < DATA.Length; i++)
            {
                string[] dataTemp = DATA[i].Split('\t');
                double[] fingersLeftHandCurrentMessage = new double[12];
                double[] fingersRightHandCurrentMessage = new double[12];
                for (int j = 0; j < 12; j++)
                {
                    fingersLeftHandCurrentMessage[j] = double.Parse(dataTemp[j + 7]);
                    fingersRightHandCurrentMessage[j] = double.Parse(dataTemp[j + 25]);
                }

                // LEFT HAND
                if (isLeftHandConnected)
                {
                    if (fingersLeftHandCurrentMessage.SequenceEqual(fingersLeftHandLastMessage))
                    {
                        currentRepeatStreak[0]++;
                        if (currentRepeatStreak[0] >= 50)
                            areFingersConnected[0] = false;
                    }
                    else
                    {
                        currentRepeatStreak[0] = 1; // Include il messaggio attuale come inizio nuovo conteggio
                        fingersLeftHandLastMessage = fingersLeftHandCurrentMessage;
                    }
                }

                if (isRightHandConnected)
                {
                    // RIGHT HAND
                    if (fingersRightHandCurrentMessage.SequenceEqual(fingersRightHandLastMessage))
                    {
                        currentRepeatStreak[1]++;
                        if (currentRepeatStreak[1] >= 50)
                            areFingersConnected[1] = false;
                    }
                    else
                    {
                        currentRepeatStreak[1] = 1;
                        fingersRightHandLastMessage = fingersRightHandCurrentMessage;
                    }
                }


                // Early exit: se entrambi sono disconnessi, non serve continuare
                if (!areFingersConnected[0] && !areFingersConnected[1])
                    break;
            }

            return areFingersConnected;
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            string filepath = textBoxPath.Text;
            string filename = textBoxFileName.Text;

            if (!File.Exists(filepath + '\\' + filename))
                MessageBox.Show("File non trovato", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {

                try
                {
                    // Process features
                    features = DataProcessing.processSingleTask(filepath + '\\' + filename);
                    updateTextBoxFeatures(features);

                }
                catch
                {
                    MessageBox.Show("Errore di processing, ripetere l'esercizio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void checkSensorConnection()
        {
            //
            if (sensor0 != sensorNull && sensor0 != sensorNullFloat && sensor0 != null)
                isSensorConnected[0] = true;
            else
                isSensorConnected[0] = false;
            //
            if (sensor1 != sensorNull && sensor1 != sensorNullFloat && sensor1 != null)
                isSensorConnected[1] = true;
            else
                isSensorConnected[1] = false;
            //
            if (sensor2 != sensorNull && sensor2 != sensorNullFloat && sensor2 != null)
                isSensorConnected[2] = true;
            else
                isSensorConnected[2] = false;
            //
            if (sensor3 != sensorNull && sensor3 != sensorNullFloat && sensor3 != null)
                isSensorConnected[3] = true;
            else
                isSensorConnected[3] = false;
            //
            if (sensor4 != sensorNull && sensor4 != sensorNullFloat && sensor4 != null)
                isSensorConnected[4] = true;
            else
                isSensorConnected[4] = false;
            //
            if (sensor5 != sensorNull && sensor5 != sensorNullFloat && sensor5 != null)
                isSensorConnected[5] = true;
            else
                isSensorConnected[5] = false;
            //
            if (sensor6 != sensorNull && sensor6 != sensorNullFloat && sensor6 != null)
                isSensorConnected[6] = true;
            else
                isSensorConnected[6] = false;
            //
            if (sensor7 != sensorNull && sensor7 != sensorNullFloat && sensor7 != null)
                isSensorConnected[7] = true;
            else
                isSensorConnected[7] = false;
        }


        private void updateTextBoxFeatures(FeaturesRepMovStruct features)
        {
            textBoxTaps.Text = features.taps.ToString();
            textBoxExc.Text = Math.Round(features.exc,2).ToString() + " deg";
            textBoxVelOpen.Text = Math.Round(features.wo,2).ToString() + " deg/s";
            textBoxVelClose.Text = Math.Round(features.wc,2).ToString() + " deg/s";
        }
    }

}
