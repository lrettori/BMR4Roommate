namespace MotorTaskAcquisition
{
    partial class ChildForm1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChildForm1));
            this.comboBoxSerialPorts = new System.Windows.Forms.ComboBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonConnectAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelFreq = new System.Windows.Forms.Label();
            this.textBoxSens0 = new System.Windows.Forms.TextBox();
            this.textBoxSens1 = new System.Windows.Forms.TextBox();
            this.textBoxSens2 = new System.Windows.Forms.TextBox();
            this.textBoxSens4 = new System.Windows.Forms.TextBox();
            this.textBoxSens6 = new System.Windows.Forms.TextBox();
            this.textBoxSens3 = new System.Windows.Forms.TextBox();
            this.textBoxSens5 = new System.Windows.Forms.TextBox();
            this.textBoxSens7 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelLH = new System.Windows.Forms.Panel();
            this.panelRH = new System.Windows.Forms.Panel();
            this.panelLF = new System.Windows.Forms.Panel();
            this.panelRF = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelMode = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBoxUser = new System.Windows.Forms.ComboBox();
            this.comboBoxBTCommand = new System.Windows.Forms.ComboBox();
            this.buttonSendCommand = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.checkBoxFreeAcquisition = new System.Windows.Forms.CheckBox();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.labelTimer = new System.Windows.Forms.Label();
            this.labelFileName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelRep = new System.Windows.Forms.Label();
            this.labelSide = new System.Windows.Forms.Label();
            this.labelTaskSelection = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.comboBoxRep = new System.Windows.Forms.ComboBox();
            this.comboBoxSide = new System.Windows.Forms.ComboBox();
            this.comboBoxTasks = new System.Windows.Forms.ComboBox();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.checkBoxAutomaticName = new System.Windows.Forms.CheckBox();
            this.labelLW = new System.Windows.Forms.Label();
            this.labelLT = new System.Windows.Forms.Label();
            this.labelLI = new System.Windows.Forms.Label();
            this.labelRW = new System.Windows.Forms.Label();
            this.labelRT = new System.Windows.Forms.Label();
            this.labelRI = new System.Windows.Forms.Label();
            this.labelLF = new System.Windows.Forms.Label();
            this.labelRF = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label_ax = new System.Windows.Forms.Label();
            this.label_ay = new System.Windows.Forms.Label();
            this.label_az = new System.Windows.Forms.Label();
            this.label_wx = new System.Windows.Forms.Label();
            this.label_wy = new System.Windows.Forms.Label();
            this.label_wz = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxTaskDescription = new System.Windows.Forms.RichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonProcess = new System.Windows.Forms.Button();
            this.textBoxTaps = new System.Windows.Forms.TextBox();
            this.textBoxExc = new System.Windows.Forms.TextBox();
            this.textBoxVelOpen = new System.Windows.Forms.TextBox();
            this.textBoxVelClose = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxSerialPorts
            // 
            this.comboBoxSerialPorts.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSerialPorts.FormattingEnabled = true;
            this.comboBoxSerialPorts.Location = new System.Drawing.Point(16, 42);
            this.comboBoxSerialPorts.Name = "comboBoxSerialPorts";
            this.comboBoxSerialPorts.Size = new System.Drawing.Size(123, 21);
            this.comboBoxSerialPorts.TabIndex = 0;
            this.comboBoxSerialPorts.Text = "Select Serial Port";
            this.comboBoxSerialPorts.SelectedIndexChanged += new System.EventHandler(this.comboBoxSerialPorts_SelectedIndexChanged);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpen.Location = new System.Drawing.Point(151, 27);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(80, 40);
            this.buttonOpen.TabIndex = 1;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonConnectAll
            // 
            this.buttonConnectAll.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnectAll.Location = new System.Drawing.Point(236, 79);
            this.buttonConnectAll.Name = "buttonConnectAll";
            this.buttonConnectAll.Size = new System.Drawing.Size(80, 40);
            this.buttonConnectAll.TabIndex = 2;
            this.buttonConnectAll.Text = "Connect\r\nAll";
            this.buttonConnectAll.UseVisualStyleBackColor = true;
            this.buttonConnectAll.Click += new System.EventHandler(this.buttonConnectAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(364, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Frequency";
            // 
            // labelFreq
            // 
            this.labelFreq.AutoSize = true;
            this.labelFreq.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFreq.Location = new System.Drawing.Point(384, 102);
            this.labelFreq.Name = "labelFreq";
            this.labelFreq.Size = new System.Drawing.Size(18, 20);
            this.labelFreq.TabIndex = 4;
            this.labelFreq.Text = "0";
            // 
            // textBoxSens0
            // 
            this.textBoxSens0.Location = new System.Drawing.Point(86, 445);
            this.textBoxSens0.Name = "textBoxSens0";
            this.textBoxSens0.Size = new System.Drawing.Size(295, 20);
            this.textBoxSens0.TabIndex = 5;
            this.textBoxSens0.TabStop = false;
            this.textBoxSens0.Visible = false;
            // 
            // textBoxSens1
            // 
            this.textBoxSens1.Location = new System.Drawing.Point(86, 470);
            this.textBoxSens1.Name = "textBoxSens1";
            this.textBoxSens1.Size = new System.Drawing.Size(295, 20);
            this.textBoxSens1.TabIndex = 6;
            this.textBoxSens1.Visible = false;
            // 
            // textBoxSens2
            // 
            this.textBoxSens2.Location = new System.Drawing.Point(86, 496);
            this.textBoxSens2.Name = "textBoxSens2";
            this.textBoxSens2.Size = new System.Drawing.Size(295, 20);
            this.textBoxSens2.TabIndex = 7;
            this.textBoxSens2.Visible = false;
            // 
            // textBoxSens4
            // 
            this.textBoxSens4.Location = new System.Drawing.Point(86, 548);
            this.textBoxSens4.Name = "textBoxSens4";
            this.textBoxSens4.Size = new System.Drawing.Size(295, 20);
            this.textBoxSens4.TabIndex = 8;
            this.textBoxSens4.Visible = false;
            // 
            // textBoxSens6
            // 
            this.textBoxSens6.Location = new System.Drawing.Point(86, 600);
            this.textBoxSens6.Name = "textBoxSens6";
            this.textBoxSens6.Size = new System.Drawing.Size(295, 20);
            this.textBoxSens6.TabIndex = 9;
            this.textBoxSens6.Visible = false;
            // 
            // textBoxSens3
            // 
            this.textBoxSens3.Location = new System.Drawing.Point(86, 522);
            this.textBoxSens3.Name = "textBoxSens3";
            this.textBoxSens3.Size = new System.Drawing.Size(295, 20);
            this.textBoxSens3.TabIndex = 10;
            this.textBoxSens3.Visible = false;
            // 
            // textBoxSens5
            // 
            this.textBoxSens5.Location = new System.Drawing.Point(86, 574);
            this.textBoxSens5.Name = "textBoxSens5";
            this.textBoxSens5.Size = new System.Drawing.Size(295, 20);
            this.textBoxSens5.TabIndex = 11;
            this.textBoxSens5.Visible = false;
            // 
            // textBoxSens7
            // 
            this.textBoxSens7.Location = new System.Drawing.Point(86, 626);
            this.textBoxSens7.Name = "textBoxSens7";
            this.textBoxSens7.Size = new System.Drawing.Size(295, 20);
            this.textBoxSens7.TabIndex = 12;
            this.textBoxSens7.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelLH
            // 
            this.panelLH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLH.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelLH.Location = new System.Drawing.Point(521, 64);
            this.panelLH.Name = "panelLH";
            this.panelLH.Size = new System.Drawing.Size(12, 12);
            this.panelLH.TabIndex = 13;
            // 
            // panelRH
            // 
            this.panelRH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRH.Location = new System.Drawing.Point(472, 64);
            this.panelRH.Name = "panelRH";
            this.panelRH.Size = new System.Drawing.Size(12, 12);
            this.panelRH.TabIndex = 14;
            this.panelRH.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelRH_MouseDoubleClick);
            // 
            // panelLF
            // 
            this.panelLF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLF.Location = new System.Drawing.Point(511, 127);
            this.panelLF.Name = "panelLF";
            this.panelLF.Size = new System.Drawing.Size(12, 12);
            this.panelLF.TabIndex = 14;
            // 
            // panelRF
            // 
            this.panelRF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRF.Location = new System.Drawing.Point(485, 127);
            this.panelRF.Name = "panelRF";
            this.panelRF.Size = new System.Drawing.Size(12, 12);
            this.panelRF.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Connessione dongle";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelMode);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.panelLF);
            this.panel1.Controls.Add(this.panelRF);
            this.panel1.Controls.Add(this.panelRH);
            this.panel1.Controls.Add(this.panelLH);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.comboBoxUser);
            this.panel1.Controls.Add(this.comboBoxBTCommand);
            this.panel1.Controls.Add(this.buttonSendCommand);
            this.panel1.Controls.Add(this.comboBoxSerialPorts);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.buttonOpen);
            this.panel1.Controls.Add(this.buttonConnectAll);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelFreq);
            this.panel1.Location = new System.Drawing.Point(12, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 151);
            this.panel1.TabIndex = 16;
            // 
            // labelMode
            // 
            this.labelMode.AutoSize = true;
            this.labelMode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMode.Location = new System.Drawing.Point(347, 8);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(62, 16);
            this.labelMode.TabIndex = 24;
            this.labelMode.Text = "Modalità";
            this.labelMode.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(523, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 14);
            this.label9.TabIndex = 23;
            this.label9.Text = "LF";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(464, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 14);
            this.label8.TabIndex = 22;
            this.label8.Text = "RF";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(530, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 14);
            this.label6.TabIndex = 21;
            this.label6.Text = "LH";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(457, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 14);
            this.label5.TabIndex = 20;
            this.label5.Text = "RH";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(470, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(68, 139);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // comboBoxUser
            // 
            this.comboBoxUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUser.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxUser.FormattingEnabled = true;
            this.comboBoxUser.Items.AddRange(new object[] {
            "User",
            "Developer"});
            this.comboBoxUser.Location = new System.Drawing.Point(347, 27);
            this.comboBoxUser.Name = "comboBoxUser";
            this.comboBoxUser.Size = new System.Drawing.Size(81, 23);
            this.comboBoxUser.TabIndex = 18;
            this.comboBoxUser.Visible = false;
            this.comboBoxUser.SelectedIndexChanged += new System.EventHandler(this.comboBoxUser_SelectedIndexChanged);
            // 
            // comboBoxBTCommand
            // 
            this.comboBoxBTCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBTCommand.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxBTCommand.FormattingEnabled = true;
            this.comboBoxBTCommand.Items.AddRange(new object[] {
            "Scan",
            "Connect to all",
            "Connect to 0",
            "Connect to 1",
            "Connect to 2",
            "Connect to 3",
            "Disconnect from all",
            "Disconnect from 0",
            "Disconnect from 1",
            "Disconnect from 2",
            "Disconnect from 3"});
            this.comboBoxBTCommand.Location = new System.Drawing.Point(16, 87);
            this.comboBoxBTCommand.Name = "comboBoxBTCommand";
            this.comboBoxBTCommand.Size = new System.Drawing.Size(123, 23);
            this.comboBoxBTCommand.TabIndex = 17;
            // 
            // buttonSendCommand
            // 
            this.buttonSendCommand.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSendCommand.Location = new System.Drawing.Point(151, 79);
            this.buttonSendCommand.Name = "buttonSendCommand";
            this.buttonSendCommand.Size = new System.Drawing.Size(80, 40);
            this.buttonSendCommand.TabIndex = 16;
            this.buttonSendCommand.Text = "Send\r\nCommand";
            this.buttonSendCommand.UseVisualStyleBackColor = true;
            this.buttonSendCommand.Click += new System.EventHandler(this.buttonSendCommand_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.textBoxID);
            this.panel2.Controls.Add(this.checkBoxFreeAcquisition);
            this.panel2.Controls.Add(this.buttonStartStop);
            this.panel2.Controls.Add(this.labelTimer);
            this.panel2.Controls.Add(this.labelFileName);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.labelRep);
            this.panel2.Controls.Add(this.labelSide);
            this.panel2.Controls.Add(this.labelTaskSelection);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.textBoxFileName);
            this.panel2.Controls.Add(this.comboBoxRep);
            this.panel2.Controls.Add(this.comboBoxSide);
            this.panel2.Controls.Add(this.comboBoxTasks);
            this.panel2.Controls.Add(this.textBoxPath);
            this.panel2.Controls.Add(this.buttonBrowse);
            this.panel2.Controls.Add(this.checkBoxAutomaticName);
            this.panel2.Location = new System.Drawing.Point(12, 194);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(590, 188);
            this.panel2.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(34, 75);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 15);
            this.label13.TabIndex = 33;
            this.label13.Text = "ID soggetto";
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(118, 71);
            this.textBoxID.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(76, 20);
            this.textBoxID.TabIndex = 32;
            this.textBoxID.Text = "Subj_1";
            // 
            // checkBoxFreeAcquisition
            // 
            this.checkBoxFreeAcquisition.AutoSize = true;
            this.checkBoxFreeAcquisition.Location = new System.Drawing.Point(434, 67);
            this.checkBoxFreeAcquisition.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxFreeAcquisition.Name = "checkBoxFreeAcquisition";
            this.checkBoxFreeAcquisition.Size = new System.Drawing.Size(129, 17);
            this.checkBoxFreeAcquisition.TabIndex = 31;
            this.checkBoxFreeAcquisition.Text = "Free acquisition mode";
            this.checkBoxFreeAcquisition.UseVisualStyleBackColor = true;
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartStop.Location = new System.Drawing.Point(438, 91);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(100, 50);
            this.buttonStartStop.TabIndex = 20;
            this.buttonStartStop.Text = "START";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // labelTimer
            // 
            this.labelTimer.AutoSize = true;
            this.labelTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimer.Location = new System.Drawing.Point(506, 154);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(18, 20);
            this.labelTimer.TabIndex = 30;
            this.labelTimer.Text = "0";
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFileName.Location = new System.Drawing.Point(247, 80);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(60, 15);
            this.labelFileName.TabIndex = 24;
            this.labelFileName.Text = "Nome file";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(454, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 15);
            this.label7.TabIndex = 29;
            this.label7.Text = "Timer";
            // 
            // labelRep
            // 
            this.labelRep.AutoSize = true;
            this.labelRep.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRep.Location = new System.Drawing.Point(34, 158);
            this.labelRep.Name = "labelRep";
            this.labelRep.Size = new System.Drawing.Size(69, 15);
            this.labelRep.TabIndex = 23;
            this.labelRep.Text = "Ripetizione";
            // 
            // labelSide
            // 
            this.labelSide.AutoSize = true;
            this.labelSide.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSide.Location = new System.Drawing.Point(71, 130);
            this.labelSide.Name = "labelSide";
            this.labelSide.Size = new System.Drawing.Size(32, 15);
            this.labelSide.TabIndex = 22;
            this.labelSide.Text = "Lato";
            // 
            // labelTaskSelection
            // 
            this.labelTaskSelection.AutoSize = true;
            this.labelTaskSelection.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTaskSelection.Location = new System.Drawing.Point(13, 102);
            this.labelTaskSelection.Name = "labelTaskSelection";
            this.labelTaskSelection.Size = new System.Drawing.Size(90, 15);
            this.labelTaskSelection.TabIndex = 21;
            this.labelTaskSelection.Text = "Selezione task";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "Percorso salvataggio dati";
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFileName.Location = new System.Drawing.Point(247, 105);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(142, 21);
            this.textBoxFileName.TabIndex = 6;
            // 
            // comboBoxRep
            // 
            this.comboBoxRep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRep.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxRep.FormattingEnabled = true;
            this.comboBoxRep.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBoxRep.Location = new System.Drawing.Point(118, 154);
            this.comboBoxRep.Name = "comboBoxRep";
            this.comboBoxRep.Size = new System.Drawing.Size(61, 23);
            this.comboBoxRep.TabIndex = 5;
            this.comboBoxRep.SelectedIndexChanged += new System.EventHandler(this.comboBoxRep_SelectedIndexChanged);
            // 
            // comboBoxSide
            // 
            this.comboBoxSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSide.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSide.FormattingEnabled = true;
            this.comboBoxSide.Items.AddRange(new object[] {
            "DX",
            "SX"});
            this.comboBoxSide.Location = new System.Drawing.Point(118, 126);
            this.comboBoxSide.Name = "comboBoxSide";
            this.comboBoxSide.Size = new System.Drawing.Size(61, 23);
            this.comboBoxSide.TabIndex = 4;
            this.comboBoxSide.SelectedIndexChanged += new System.EventHandler(this.comboBoxSide_SelectedIndexChanged);
            // 
            // comboBoxTasks
            // 
            this.comboBoxTasks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTasks.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTasks.FormattingEnabled = true;
            this.comboBoxTasks.Items.AddRange(new object[] {
            "OPCL",
            "PSUP"});
            this.comboBoxTasks.Location = new System.Drawing.Point(118, 98);
            this.comboBoxTasks.Name = "comboBoxTasks";
            this.comboBoxTasks.Size = new System.Drawing.Size(63, 23);
            this.comboBoxTasks.TabIndex = 3;
            this.comboBoxTasks.SelectedIndexChanged += new System.EventHandler(this.comboBoxTasks_SelectedIndexChanged);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(15, 34);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(291, 20);
            this.textBoxPath.TabIndex = 2;
            this.textBoxPath.Text = "D:\\";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowse.Location = new System.Drawing.Point(317, 29);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(80, 30);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // checkBoxAutomaticName
            // 
            this.checkBoxAutomaticName.AutoSize = true;
            this.checkBoxAutomaticName.Checked = true;
            this.checkBoxAutomaticName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutomaticName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAutomaticName.Location = new System.Drawing.Point(250, 140);
            this.checkBoxAutomaticName.Name = "checkBoxAutomaticName";
            this.checkBoxAutomaticName.Size = new System.Drawing.Size(125, 18);
            this.checkBoxAutomaticName.TabIndex = 0;
            this.checkBoxAutomaticName.Text = "Nome file automatico";
            this.checkBoxAutomaticName.UseVisualStyleBackColor = true;
            this.checkBoxAutomaticName.CheckedChanged += new System.EventHandler(this.checkBoxAutomaticName_CheckedChanged);
            // 
            // labelLW
            // 
            this.labelLW.AutoSize = true;
            this.labelLW.BackColor = System.Drawing.Color.White;
            this.labelLW.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLW.Location = new System.Drawing.Point(25, 447);
            this.labelLW.Name = "labelLW";
            this.labelLW.Size = new System.Drawing.Size(56, 15);
            this.labelLW.TabIndex = 21;
            this.labelLW.Text = "Left wrist";
            this.labelLW.Visible = false;
            // 
            // labelLT
            // 
            this.labelLT.AutoSize = true;
            this.labelLT.BackColor = System.Drawing.Color.White;
            this.labelLT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLT.Location = new System.Drawing.Point(16, 473);
            this.labelLT.Name = "labelLT";
            this.labelLT.Size = new System.Drawing.Size(65, 15);
            this.labelLT.TabIndex = 22;
            this.labelLT.Text = "Left thumb";
            this.labelLT.Visible = false;
            // 
            // labelLI
            // 
            this.labelLI.AutoSize = true;
            this.labelLI.BackColor = System.Drawing.Color.White;
            this.labelLI.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLI.Location = new System.Drawing.Point(21, 499);
            this.labelLI.Name = "labelLI";
            this.labelLI.Size = new System.Drawing.Size(59, 15);
            this.labelLI.TabIndex = 23;
            this.labelLI.Text = "Left index";
            this.labelLI.Visible = false;
            // 
            // labelRW
            // 
            this.labelRW.AutoSize = true;
            this.labelRW.BackColor = System.Drawing.Color.White;
            this.labelRW.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRW.Location = new System.Drawing.Point(18, 525);
            this.labelRW.Name = "labelRW";
            this.labelRW.Size = new System.Drawing.Size(65, 15);
            this.labelRW.TabIndex = 24;
            this.labelRW.Text = "Right wrist";
            this.labelRW.Visible = false;
            // 
            // labelRT
            // 
            this.labelRT.AutoSize = true;
            this.labelRT.BackColor = System.Drawing.Color.White;
            this.labelRT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRT.Location = new System.Drawing.Point(10, 551);
            this.labelRT.Name = "labelRT";
            this.labelRT.Size = new System.Drawing.Size(74, 15);
            this.labelRT.TabIndex = 25;
            this.labelRT.Text = "Right thumb";
            this.labelRT.Visible = false;
            // 
            // labelRI
            // 
            this.labelRI.AutoSize = true;
            this.labelRI.BackColor = System.Drawing.Color.White;
            this.labelRI.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRI.Location = new System.Drawing.Point(14, 577);
            this.labelRI.Name = "labelRI";
            this.labelRI.Size = new System.Drawing.Size(68, 15);
            this.labelRI.TabIndex = 26;
            this.labelRI.Text = "Right index";
            this.labelRI.Visible = false;
            // 
            // labelLF
            // 
            this.labelLF.AutoSize = true;
            this.labelLF.BackColor = System.Drawing.Color.White;
            this.labelLF.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLF.Location = new System.Drawing.Point(28, 603);
            this.labelLF.Name = "labelLF";
            this.labelLF.Size = new System.Drawing.Size(50, 15);
            this.labelLF.TabIndex = 27;
            this.labelLF.Text = "Left foot";
            this.labelLF.Visible = false;
            // 
            // labelRF
            // 
            this.labelRF.AutoSize = true;
            this.labelRF.BackColor = System.Drawing.Color.White;
            this.labelRF.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRF.Location = new System.Drawing.Point(21, 629);
            this.labelRF.Name = "labelRF";
            this.labelRF.Size = new System.Drawing.Size(59, 15);
            this.labelRF.TabIndex = 28;
            this.labelRF.Text = "Right foot";
            this.labelRF.Visible = false;
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label_ax
            // 
            this.label_ax.AutoSize = true;
            this.label_ax.Location = new System.Drawing.Point(88, 425);
            this.label_ax.Name = "label_ax";
            this.label_ax.Size = new System.Drawing.Size(24, 13);
            this.label_ax.TabIndex = 37;
            this.label_ax.Text = "a_x";
            this.label_ax.Visible = false;
            // 
            // label_ay
            // 
            this.label_ay.AutoSize = true;
            this.label_ay.Location = new System.Drawing.Point(138, 425);
            this.label_ay.Name = "label_ay";
            this.label_ay.Size = new System.Drawing.Size(24, 13);
            this.label_ay.TabIndex = 38;
            this.label_ay.Text = "a_y";
            this.label_ay.Visible = false;
            // 
            // label_az
            // 
            this.label_az.AutoSize = true;
            this.label_az.Location = new System.Drawing.Point(187, 425);
            this.label_az.Name = "label_az";
            this.label_az.Size = new System.Drawing.Size(24, 13);
            this.label_az.TabIndex = 39;
            this.label_az.Text = "a_z";
            this.label_az.Visible = false;
            // 
            // label_wx
            // 
            this.label_wx.AutoSize = true;
            this.label_wx.Location = new System.Drawing.Point(235, 425);
            this.label_wx.Name = "label_wx";
            this.label_wx.Size = new System.Drawing.Size(26, 13);
            this.label_wx.TabIndex = 40;
            this.label_wx.Text = "w_x";
            this.label_wx.Visible = false;
            // 
            // label_wy
            // 
            this.label_wy.AutoSize = true;
            this.label_wy.Location = new System.Drawing.Point(283, 425);
            this.label_wy.Name = "label_wy";
            this.label_wy.Size = new System.Drawing.Size(26, 13);
            this.label_wy.TabIndex = 41;
            this.label_wy.Text = "w_y";
            this.label_wy.Visible = false;
            // 
            // label_wz
            // 
            this.label_wz.AutoSize = true;
            this.label_wz.Location = new System.Drawing.Point(331, 425);
            this.label_wz.Name = "label_wz";
            this.label_wz.Size = new System.Drawing.Size(26, 13);
            this.label_wz.TabIndex = 42;
            this.label_wz.Text = "w_z";
            this.label_wz.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(186, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 19);
            this.label3.TabIndex = 43;
            this.label3.Text = "BMR Wearable Sensor System";
            // 
            // richTextBoxTaskDescription
            // 
            this.richTextBoxTaskDescription.Location = new System.Drawing.Point(12, 407);
            this.richTextBoxTaskDescription.Name = "richTextBoxTaskDescription";
            this.richTextBoxTaskDescription.Size = new System.Drawing.Size(389, 247);
            this.richTextBoxTaskDescription.TabIndex = 44;
            this.richTextBoxTaskDescription.Text = "";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 385);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 16);
            this.label10.TabIndex = 45;
            this.label10.Text = "Descrizione task";
            // 
            // buttonProcess
            // 
            this.buttonProcess.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonProcess.Location = new System.Drawing.Point(422, 407);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(140, 36);
            this.buttonProcess.TabIndex = 46;
            this.buttonProcess.Text = "PROCESS DATA";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // textBoxTaps
            // 
            this.textBoxTaps.Location = new System.Drawing.Point(435, 473);
            this.textBoxTaps.Name = "textBoxTaps";
            this.textBoxTaps.Size = new System.Drawing.Size(140, 20);
            this.textBoxTaps.TabIndex = 47;
            // 
            // textBoxExc
            // 
            this.textBoxExc.Location = new System.Drawing.Point(435, 520);
            this.textBoxExc.Name = "textBoxExc";
            this.textBoxExc.Size = new System.Drawing.Size(140, 20);
            this.textBoxExc.TabIndex = 48;
            // 
            // textBoxVelOpen
            // 
            this.textBoxVelOpen.Location = new System.Drawing.Point(435, 572);
            this.textBoxVelOpen.Name = "textBoxVelOpen";
            this.textBoxVelOpen.Size = new System.Drawing.Size(140, 20);
            this.textBoxVelOpen.TabIndex = 49;
            // 
            // textBoxVelClose
            // 
            this.textBoxVelClose.Location = new System.Drawing.Point(435, 624);
            this.textBoxVelClose.Name = "textBoxVelClose";
            this.textBoxVelClose.Size = new System.Drawing.Size(140, 20);
            this.textBoxVelClose.TabIndex = 50;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(434, 455);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 15);
            this.label11.TabIndex = 51;
            this.label11.Text = "Ripetizioni";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(434, 502);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 15);
            this.label12.TabIndex = 52;
            this.label12.Text = "Ampiezza media";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.White;
            this.label14.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(432, 553);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 15);
            this.label14.TabIndex = 53;
            this.label14.Text = "Vel. media apertura";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(432, 606);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(115, 15);
            this.label15.TabIndex = 54;
            this.label15.Text = "Vel. media chiusura";
            // 
            // ChildForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(614, 669);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxVelClose);
            this.Controls.Add(this.textBoxVelOpen);
            this.Controls.Add(this.textBoxExc);
            this.Controls.Add(this.textBoxTaps);
            this.Controls.Add(this.buttonProcess);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_wz);
            this.Controls.Add(this.label_wy);
            this.Controls.Add(this.label_wx);
            this.Controls.Add(this.label_az);
            this.Controls.Add(this.label_ay);
            this.Controls.Add(this.label_ax);
            this.Controls.Add(this.labelRF);
            this.Controls.Add(this.labelLF);
            this.Controls.Add(this.labelRI);
            this.Controls.Add(this.labelRT);
            this.Controls.Add(this.labelRW);
            this.Controls.Add(this.labelLI);
            this.Controls.Add(this.labelLT);
            this.Controls.Add(this.labelLW);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBoxSens7);
            this.Controls.Add(this.textBoxSens5);
            this.Controls.Add(this.textBoxSens3);
            this.Controls.Add(this.textBoxSens6);
            this.Controls.Add(this.textBoxSens4);
            this.Controls.Add(this.textBoxSens2);
            this.Controls.Add(this.textBoxSens1);
            this.Controls.Add(this.textBoxSens0);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.richTextBoxTaskDescription);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(12, 177);
            this.Name = "ChildForm1";
            this.Text = "BMR4Roommate";
            this.Load += new System.EventHandler(this.ChildForm1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSerialPorts;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonConnectAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFreq;
        private System.Windows.Forms.TextBox textBoxSens0;
        private System.Windows.Forms.TextBox textBoxSens1;
        private System.Windows.Forms.TextBox textBoxSens2;
        private System.Windows.Forms.TextBox textBoxSens4;
        private System.Windows.Forms.TextBox textBoxSens6;
        private System.Windows.Forms.TextBox textBoxSens3;
        private System.Windows.Forms.TextBox textBoxSens5;
        private System.Windows.Forms.TextBox textBoxSens7;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panelLH;
        private System.Windows.Forms.Panel panelRH;
        private System.Windows.Forms.Panel panelLF;
        private System.Windows.Forms.Panel panelRF;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonSendCommand;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBoxUser;
        private System.Windows.Forms.ComboBox comboBoxBTCommand;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.ComboBox comboBoxRep;
        private System.Windows.Forms.ComboBox comboBoxSide;
        private System.Windows.Forms.ComboBox comboBoxTasks;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.CheckBox checkBoxAutomaticName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.Label labelRep;
        private System.Windows.Forms.Label labelSide;
        private System.Windows.Forms.Label labelTaskSelection;
        private System.Windows.Forms.Label labelLW;
        private System.Windows.Forms.Label labelLT;
        private System.Windows.Forms.Label labelLI;
        private System.Windows.Forms.Label labelRW;
        private System.Windows.Forms.Label labelRT;
        private System.Windows.Forms.Label labelRI;
        private System.Windows.Forms.Label labelLF;
        private System.Windows.Forms.Label labelRF;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label labelMode;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label_ax;
        private System.Windows.Forms.Label label_ay;
        private System.Windows.Forms.Label label_az;
        private System.Windows.Forms.Label label_wx;
        private System.Windows.Forms.Label label_wy;
        private System.Windows.Forms.Label label_wz;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxFreeAcquisition;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBoxTaskDescription;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.TextBox textBoxTaps;
        private System.Windows.Forms.TextBox textBoxExc;
        private System.Windows.Forms.TextBox textBoxVelOpen;
        private System.Windows.Forms.TextBox textBoxVelClose;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}

