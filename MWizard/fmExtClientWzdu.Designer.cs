namespace MWizard
{
	partial class fmExtClientWizard
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpConnection = new System.Windows.Forms.TabPage();
            this.cbChooseLanguage = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cbEEPAlias = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbDatabaseType = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnConnectionString = new System.Windows.Forms.Button();
            this.tbConnectionString = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tpOutputSetting = new System.Windows.Forms.TabPage();
            this.btnAssemblyOutputPath = new System.Windows.Forms.Button();
            this.tbAssemblyOutputPath = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.tbOutputPath = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCurrentSolution = new System.Windows.Forms.TextBox();
            this.rbAddToCurrent = new System.Windows.Forms.RadioButton();
            this.btnNewLocation = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tbNewLocation = new System.Windows.Forms.TextBox();
            this.btnSolutionName = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tbSolutionName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbNewSolutionName = new System.Windows.Forms.TextBox();
            this.rbAddToExistSln = new System.Windows.Forms.RadioButton();
            this.rbNewSolution = new System.Windows.Forms.RadioButton();
            this.tbPackageName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tpFormSetting = new System.Windows.Forms.TabPage();
            this.tbFormText = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbColumnCount = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbFormName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbBaseForm = new System.Windows.Forms.ComboBox();
            this.rbEEPBaseForm = new System.Windows.Forms.RadioButton();
            this.rbWindowsForm = new System.Windows.Forms.RadioButton();
            this.tbClientPackage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tpProvider = new System.Windows.Forms.TabPage();
            this.lvDataSet = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.label6 = new System.Windows.Forms.Label();
            this.tpBindingSource = new System.Windows.Forms.TabPage();
            this.lvBindingSource = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.label5 = new System.Windows.Forms.Label();
            this.tpLayout = new System.Windows.Forms.TabPage();
            this.cbViewBindingSource = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cbControlType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbBindingSource = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDeleteField = new System.Windows.Forms.Button();
            this.btnAddField = new System.Windows.Forms.Button();
            this.lvSelectedFields = new System.Windows.Forms.ListView();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.label22 = new System.Windows.Forms.Label();
            this.tvContainer = new System.Windows.Forms.TreeView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tpConnection.SuspendLayout();
            this.tpOutputSetting.SuspendLayout();
            this.tpFormSetting.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpProvider.SuspendLayout();
            this.tpBindingSource.SuspendLayout();
            this.tpLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpConnection);
            this.tabControl1.Controls.Add(this.tpOutputSetting);
            this.tabControl1.Controls.Add(this.tpFormSetting);
            this.tabControl1.Controls.Add(this.tpProvider);
            this.tabControl1.Controls.Add(this.tpBindingSource);
            this.tabControl1.Controls.Add(this.tpLayout);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(733, 345);
            this.tabControl1.TabIndex = 8;
            // 
            // tpConnection
            // 
            this.tpConnection.Controls.Add(this.cbChooseLanguage);
            this.tpConnection.Controls.Add(this.label21);
            this.tpConnection.Controls.Add(this.cbEEPAlias);
            this.tpConnection.Controls.Add(this.label15);
            this.tpConnection.Controls.Add(this.cbDatabaseType);
            this.tpConnection.Controls.Add(this.label13);
            this.tpConnection.Controls.Add(this.btnConnectionString);
            this.tpConnection.Controls.Add(this.tbConnectionString);
            this.tpConnection.Controls.Add(this.label12);
            this.tpConnection.Location = new System.Drawing.Point(4, 21);
            this.tpConnection.Name = "tpConnection";
            this.tpConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tpConnection.Size = new System.Drawing.Size(725, 320);
            this.tpConnection.TabIndex = 6;
            this.tpConnection.Text = "Connection";
            this.tpConnection.UseVisualStyleBackColor = true;
            // 
            // cbChooseLanguage
            // 
            this.cbChooseLanguage.FormattingEnabled = true;
            this.cbChooseLanguage.Items.AddRange(new object[] {
            "C#",
            "VB"});
            this.cbChooseLanguage.Location = new System.Drawing.Point(183, 227);
            this.cbChooseLanguage.Name = "cbChooseLanguage";
            this.cbChooseLanguage.Size = new System.Drawing.Size(184, 20);
            this.cbChooseLanguage.TabIndex = 15;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.SystemColors.Control;
            this.label21.Font = new System.Drawing.Font("Arial", 8F);
            this.label21.Location = new System.Drawing.Point(84, 229);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(95, 14);
            this.label21.TabIndex = 14;
            this.label21.Text = "Choose Language";
            // 
            // cbEEPAlias
            // 
            this.cbEEPAlias.FormattingEnabled = true;
            this.cbEEPAlias.Location = new System.Drawing.Point(183, 75);
            this.cbEEPAlias.Name = "cbEEPAlias";
            this.cbEEPAlias.Size = new System.Drawing.Size(419, 20);
            this.cbEEPAlias.TabIndex = 6;
            this.cbEEPAlias.SelectedIndexChanged += new System.EventHandler(this.cbEEPAlias_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label15.Font = new System.Drawing.Font("Arial", 8F);
            this.label15.Location = new System.Drawing.Point(127, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 14);
            this.label15.TabIndex = 5;
            this.label15.Text = "EEP Alias";
            // 
            // cbDatabaseType
            // 
            this.cbDatabaseType.FormattingEnabled = true;
            this.cbDatabaseType.Items.AddRange(new object[] {
            "None",
            "MSSQL",
            "OleDB",
            "Oracle",
            "ODBC",
            "MySql",
            "Informix",
            "Sybase"});
            this.cbDatabaseType.Location = new System.Drawing.Point(183, 178);
            this.cbDatabaseType.Name = "cbDatabaseType";
            this.cbDatabaseType.Size = new System.Drawing.Size(184, 20);
            this.cbDatabaseType.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label13.Font = new System.Drawing.Font("Arial", 8F);
            this.label13.Location = new System.Drawing.Point(99, 180);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 14);
            this.label13.TabIndex = 3;
            this.label13.Text = "Database Type";
            // 
            // btnConnectionString
            // 
            this.btnConnectionString.Location = new System.Drawing.Point(576, 126);
            this.btnConnectionString.Name = "btnConnectionString";
            this.btnConnectionString.Size = new System.Drawing.Size(26, 23);
            this.btnConnectionString.TabIndex = 2;
            this.btnConnectionString.Text = "...";
            this.btnConnectionString.UseVisualStyleBackColor = true;
            this.btnConnectionString.Click += new System.EventHandler(this.btnConnectionString_Click);
            // 
            // tbConnectionString
            // 
            this.tbConnectionString.Location = new System.Drawing.Point(183, 126);
            this.tbConnectionString.Name = "tbConnectionString";
            this.tbConnectionString.Size = new System.Drawing.Size(391, 21);
            this.tbConnectionString.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label12.Font = new System.Drawing.Font("Arial", 8F);
            this.label12.Location = new System.Drawing.Point(87, 130);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(92, 14);
            this.label12.TabIndex = 0;
            this.label12.Text = "Connection String";
            // 
            // tpOutputSetting
            // 
            this.tpOutputSetting.AutoScroll = true;
            this.tpOutputSetting.Controls.Add(this.btnAssemblyOutputPath);
            this.tpOutputSetting.Controls.Add(this.tbAssemblyOutputPath);
            this.tpOutputSetting.Controls.Add(this.label19);
            this.tpOutputSetting.Controls.Add(this.btnOutputPath);
            this.tpOutputSetting.Controls.Add(this.tbOutputPath);
            this.tpOutputSetting.Controls.Add(this.label16);
            this.tpOutputSetting.Controls.Add(this.label1);
            this.tpOutputSetting.Controls.Add(this.tbCurrentSolution);
            this.tpOutputSetting.Controls.Add(this.rbAddToCurrent);
            this.tpOutputSetting.Controls.Add(this.btnNewLocation);
            this.tpOutputSetting.Controls.Add(this.label9);
            this.tpOutputSetting.Controls.Add(this.tbNewLocation);
            this.tpOutputSetting.Controls.Add(this.btnSolutionName);
            this.tpOutputSetting.Controls.Add(this.label10);
            this.tpOutputSetting.Controls.Add(this.tbSolutionName);
            this.tpOutputSetting.Controls.Add(this.label11);
            this.tpOutputSetting.Controls.Add(this.tbNewSolutionName);
            this.tpOutputSetting.Controls.Add(this.rbAddToExistSln);
            this.tpOutputSetting.Controls.Add(this.rbNewSolution);
            this.tpOutputSetting.Controls.Add(this.tbPackageName);
            this.tpOutputSetting.Controls.Add(this.label2);
            this.tpOutputSetting.Location = new System.Drawing.Point(4, 21);
            this.tpOutputSetting.Name = "tpOutputSetting";
            this.tpOutputSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpOutputSetting.Size = new System.Drawing.Size(725, 320);
            this.tpOutputSetting.TabIndex = 0;
            this.tpOutputSetting.Text = "Output Setting";
            this.tpOutputSetting.UseVisualStyleBackColor = true;
            // 
            // btnAssemblyOutputPath
            // 
            this.btnAssemblyOutputPath.Location = new System.Drawing.Point(605, 263);
            this.btnAssemblyOutputPath.Name = "btnAssemblyOutputPath";
            this.btnAssemblyOutputPath.Size = new System.Drawing.Size(27, 23);
            this.btnAssemblyOutputPath.TabIndex = 33;
            this.btnAssemblyOutputPath.Text = "...";
            this.btnAssemblyOutputPath.Click += new System.EventHandler(this.btnAssemblyOutputPath_Click);
            // 
            // tbAssemblyOutputPath
            // 
            this.tbAssemblyOutputPath.Location = new System.Drawing.Point(173, 263);
            this.tbAssemblyOutputPath.Name = "tbAssemblyOutputPath";
            this.tbAssemblyOutputPath.Size = new System.Drawing.Size(427, 21);
            this.tbAssemblyOutputPath.TabIndex = 32;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Arial", 8F);
            this.label19.Location = new System.Drawing.Point(50, 265);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(117, 14);
            this.label19.TabIndex = 31;
            this.label19.Text = "Assembly Output Path:";
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Location = new System.Drawing.Point(605, 235);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(27, 23);
            this.btnOutputPath.TabIndex = 30;
            this.btnOutputPath.Text = "...";
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputPath_Click);
            // 
            // tbOutputPath
            // 
            this.tbOutputPath.Location = new System.Drawing.Point(173, 235);
            this.tbOutputPath.Name = "tbOutputPath";
            this.tbOutputPath.Size = new System.Drawing.Size(427, 21);
            this.tbOutputPath.TabIndex = 29;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial", 8F);
            this.label16.Location = new System.Drawing.Point(73, 239);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(94, 14);
            this.label16.TabIndex = 28;
            this.label16.Text = "Code Output Path:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8F);
            this.label1.Location = new System.Drawing.Point(146, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 14);
            this.label1.TabIndex = 27;
            this.label1.Text = "Solution Name:";
            // 
            // tbCurrentSolution
            // 
            this.tbCurrentSolution.Enabled = false;
            this.tbCurrentSolution.Location = new System.Drawing.Point(230, 46);
            this.tbCurrentSolution.Name = "tbCurrentSolution";
            this.tbCurrentSolution.ReadOnly = true;
            this.tbCurrentSolution.Size = new System.Drawing.Size(370, 21);
            this.tbCurrentSolution.TabIndex = 26;
            this.tbCurrentSolution.TextChanged += new System.EventHandler(this.tbCurrentSolution_TextChanged);
            // 
            // rbAddToCurrent
            // 
            this.rbAddToCurrent.AutoSize = true;
            this.rbAddToCurrent.Checked = true;
            this.rbAddToCurrent.Enabled = false;
            this.rbAddToCurrent.Font = new System.Drawing.Font("Arial", 8F);
            this.rbAddToCurrent.Location = new System.Drawing.Point(93, 21);
            this.rbAddToCurrent.Name = "rbAddToCurrent";
            this.rbAddToCurrent.Size = new System.Drawing.Size(140, 18);
            this.rbAddToCurrent.TabIndex = 25;
            this.rbAddToCurrent.TabStop = true;
            this.rbAddToCurrent.Text = "Add To Current Solution";
            this.rbAddToCurrent.UseVisualStyleBackColor = true;
            this.rbAddToCurrent.Click += new System.EventHandler(this.rbAddToCurrent_Click);
            // 
            // btnNewLocation
            // 
            this.btnNewLocation.Location = new System.Drawing.Point(557, 178);
            this.btnNewLocation.Name = "btnNewLocation";
            this.btnNewLocation.Size = new System.Drawing.Size(27, 23);
            this.btnNewLocation.TabIndex = 24;
            this.btnNewLocation.Text = "...";
            this.btnNewLocation.Visible = false;
            this.btnNewLocation.Click += new System.EventHandler(this.btnNewLocation_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 8F);
            this.label9.Location = new System.Drawing.Point(116, 181);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 14);
            this.label9.TabIndex = 23;
            this.label9.Text = "Location:";
            this.label9.Visible = false;
            // 
            // tbNewLocation
            // 
            this.tbNewLocation.Location = new System.Drawing.Point(173, 179);
            this.tbNewLocation.Name = "tbNewLocation";
            this.tbNewLocation.Size = new System.Drawing.Size(379, 21);
            this.tbNewLocation.TabIndex = 22;
            this.tbNewLocation.Visible = false;
            this.tbNewLocation.TextChanged += new System.EventHandler(this.tbCurrentSolution_TextChanged);
            // 
            // btnSolutionName
            // 
            this.btnSolutionName.Enabled = false;
            this.btnSolutionName.Location = new System.Drawing.Point(605, 113);
            this.btnSolutionName.Name = "btnSolutionName";
            this.btnSolutionName.Size = new System.Drawing.Size(27, 23);
            this.btnSolutionName.TabIndex = 21;
            this.btnSolutionName.Text = "...";
            this.btnSolutionName.Click += new System.EventHandler(this.btnSolutionName_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 8F);
            this.label10.Location = new System.Drawing.Point(146, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 14);
            this.label10.TabIndex = 20;
            this.label10.Text = "Solution Name:";
            // 
            // tbSolutionName
            // 
            this.tbSolutionName.Enabled = false;
            this.tbSolutionName.Location = new System.Drawing.Point(230, 114);
            this.tbSolutionName.Name = "tbSolutionName";
            this.tbSolutionName.Size = new System.Drawing.Size(370, 21);
            this.tbSolutionName.TabIndex = 19;
            this.tbSolutionName.TextChanged += new System.EventHandler(this.tbCurrentSolution_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 8F);
            this.label11.Location = new System.Drawing.Point(89, 156);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 14);
            this.label11.TabIndex = 18;
            this.label11.Text = "Solution Name:";
            this.label11.Visible = false;
            // 
            // tbNewSolutionName
            // 
            this.tbNewSolutionName.Location = new System.Drawing.Point(173, 151);
            this.tbNewSolutionName.Name = "tbNewSolutionName";
            this.tbNewSolutionName.Size = new System.Drawing.Size(379, 21);
            this.tbNewSolutionName.TabIndex = 17;
            this.tbNewSolutionName.Visible = false;
            this.tbNewSolutionName.TextChanged += new System.EventHandler(this.tbNewSolutionName_TextChanged);
            // 
            // rbAddToExistSln
            // 
            this.rbAddToExistSln.AutoSize = true;
            this.rbAddToExistSln.Font = new System.Drawing.Font("Arial", 8F);
            this.rbAddToExistSln.Location = new System.Drawing.Point(97, 86);
            this.rbAddToExistSln.Name = "rbAddToExistSln";
            this.rbAddToExistSln.Size = new System.Drawing.Size(127, 18);
            this.rbAddToExistSln.TabIndex = 16;
            this.rbAddToExistSln.Text = "Add To Exist Solution";
            this.rbAddToExistSln.Click += new System.EventHandler(this.rbAddToExistSln_Click);
            // 
            // rbNewSolution
            // 
            this.rbNewSolution.AutoSize = true;
            this.rbNewSolution.Font = new System.Drawing.Font("Arial", 8F);
            this.rbNewSolution.Location = new System.Drawing.Point(36, 135);
            this.rbNewSolution.Name = "rbNewSolution";
            this.rbNewSolution.Size = new System.Drawing.Size(124, 18);
            this.rbNewSolution.TabIndex = 15;
            this.rbNewSolution.Text = "Create New Solution";
            this.rbNewSolution.Visible = false;
            this.rbNewSolution.Click += new System.EventHandler(this.rbNewSolution_Click);
            // 
            // tbPackageName
            // 
            this.tbPackageName.Location = new System.Drawing.Point(173, 207);
            this.tbPackageName.Name = "tbPackageName";
            this.tbPackageName.Size = new System.Drawing.Size(367, 21);
            this.tbPackageName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8F);
            this.label2.Location = new System.Drawing.Point(86, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Package Name:";
            // 
            // tpFormSetting
            // 
            this.tpFormSetting.Controls.Add(this.tbFormText);
            this.tpFormSetting.Controls.Add(this.label20);
            this.tpFormSetting.Controls.Add(this.cbColumnCount);
            this.tpFormSetting.Controls.Add(this.label17);
            this.tpFormSetting.Controls.Add(this.tbFormName);
            this.tpFormSetting.Controls.Add(this.label4);
            this.tpFormSetting.Controls.Add(this.groupBox1);
            this.tpFormSetting.Controls.Add(this.tbClientPackage);
            this.tpFormSetting.Controls.Add(this.label3);
            this.tpFormSetting.Location = new System.Drawing.Point(4, 21);
            this.tpFormSetting.Name = "tpFormSetting";
            this.tpFormSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpFormSetting.Size = new System.Drawing.Size(725, 320);
            this.tpFormSetting.TabIndex = 1;
            this.tpFormSetting.Text = "Form Setting";
            this.tpFormSetting.UseVisualStyleBackColor = true;
            // 
            // tbFormText
            // 
            this.tbFormText.Location = new System.Drawing.Point(174, 240);
            this.tbFormText.Name = "tbFormText";
            this.tbFormText.Size = new System.Drawing.Size(347, 21);
            this.tbFormText.TabIndex = 8;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Arial", 8F);
            this.label20.Location = new System.Drawing.Point(171, 223);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(116, 14);
            this.label20.TabIndex = 7;
            this.label20.Text = "Please input Form Text";
            // 
            // cbColumnCount
            // 
            this.cbColumnCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumnCount.FormattingEnabled = true;
            this.cbColumnCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbColumnCount.Location = new System.Drawing.Point(172, 282);
            this.cbColumnCount.Name = "cbColumnCount";
            this.cbColumnCount.Size = new System.Drawing.Size(99, 20);
            this.cbColumnCount.TabIndex = 6;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Arial", 8F);
            this.label17.Location = new System.Drawing.Point(171, 265);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(122, 14);
            this.label17.TabIndex = 5;
            this.label17.Text = "TextBox\'s column count";
            // 
            // tbFormName
            // 
            this.tbFormName.Location = new System.Drawing.Point(173, 198);
            this.tbFormName.Name = "tbFormName";
            this.tbFormName.Size = new System.Drawing.Size(347, 21);
            this.tbFormName.TabIndex = 4;
            this.tbFormName.TextChanged += new System.EventHandler(this.tbFormName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 8F);
            this.label4.Location = new System.Drawing.Point(171, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "Please input Form Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbBaseForm);
            this.groupBox1.Controls.Add(this.rbEEPBaseForm);
            this.groupBox1.Controls.Add(this.rbWindowsForm);
            this.groupBox1.Location = new System.Drawing.Point(174, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 106);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inherit From";
            // 
            // cbBaseForm
            // 
            this.cbBaseForm.FormattingEnabled = true;
            this.cbBaseForm.Items.AddRange(new object[] {
            "CSingle",
            "CMasterDetail",
            "CQuery",
            "VBCSingle",
            "VBCMasterDetail",
            "VBCQuery"});
            this.cbBaseForm.Location = new System.Drawing.Point(44, 77);
            this.cbBaseForm.Name = "cbBaseForm";
            this.cbBaseForm.Size = new System.Drawing.Size(279, 20);
            this.cbBaseForm.TabIndex = 2;
            this.cbBaseForm.Text = "CSingle";
            // 
            // rbEEPBaseForm
            // 
            this.rbEEPBaseForm.AutoSize = true;
            this.rbEEPBaseForm.Checked = true;
            this.rbEEPBaseForm.Font = new System.Drawing.Font("Arial", 8F);
            this.rbEEPBaseForm.Location = new System.Drawing.Point(25, 52);
            this.rbEEPBaseForm.Name = "rbEEPBaseForm";
            this.rbEEPBaseForm.Size = new System.Drawing.Size(171, 18);
            this.rbEEPBaseForm.TabIndex = 1;
            this.rbEEPBaseForm.TabStop = true;
            this.rbEEPBaseForm.Text = "EEP Windows Template Forms";
            this.rbEEPBaseForm.CheckedChanged += new System.EventHandler(this.rbEEPBaseForm_CheckedChanged);
            // 
            // rbWindowsForm
            // 
            this.rbWindowsForm.AutoSize = true;
            this.rbWindowsForm.Enabled = false;
            this.rbWindowsForm.Font = new System.Drawing.Font("Arial", 8F);
            this.rbWindowsForm.Location = new System.Drawing.Point(25, 21);
            this.rbWindowsForm.Name = "rbWindowsForm";
            this.rbWindowsForm.Size = new System.Drawing.Size(98, 18);
            this.rbWindowsForm.TabIndex = 0;
            this.rbWindowsForm.Text = "Windows Form";
            this.rbWindowsForm.CheckedChanged += new System.EventHandler(this.rbWindowsForm_CheckedChanged);
            // 
            // tbClientPackage
            // 
            this.tbClientPackage.Enabled = false;
            this.tbClientPackage.Location = new System.Drawing.Point(174, 35);
            this.tbClientPackage.Name = "tbClientPackage";
            this.tbClientPackage.ReadOnly = true;
            this.tbClientPackage.Size = new System.Drawing.Size(347, 21);
            this.tbClientPackage.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 8F);
            this.label3.Location = new System.Drawing.Point(173, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "Target Client Package";
            // 
            // tpProvider
            // 
            this.tpProvider.Controls.Add(this.lvDataSet);
            this.tpProvider.Controls.Add(this.label6);
            this.tpProvider.Location = new System.Drawing.Point(4, 21);
            this.tpProvider.Name = "tpProvider";
            this.tpProvider.Padding = new System.Windows.Forms.Padding(3);
            this.tpProvider.Size = new System.Drawing.Size(725, 320);
            this.tpProvider.TabIndex = 2;
            this.tpProvider.Text = "Provider";
            this.tpProvider.UseVisualStyleBackColor = true;
            // 
            // lvDataSet
            // 
            this.lvDataSet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvDataSet.Location = new System.Drawing.Point(123, 43);
            this.lvDataSet.MultiSelect = false;
            this.lvDataSet.Name = "lvDataSet";
            this.lvDataSet.Size = new System.Drawing.Size(385, 230);
            this.lvDataSet.TabIndex = 40;
            this.lvDataSet.UseCompatibleStateImageBehavior = false;
            this.lvDataSet.View = System.Windows.Forms.View.Details;
            this.lvDataSet.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvSelectedFields_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "DataSet Name";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Provider Name";
            this.columnHeader2.Width = 180;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(121, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 39;
            this.label6.Text = "DataSet List";
            // 
            // tpBindingSource
            // 
            this.tpBindingSource.Controls.Add(this.lvBindingSource);
            this.tpBindingSource.Controls.Add(this.label5);
            this.tpBindingSource.Location = new System.Drawing.Point(4, 21);
            this.tpBindingSource.Name = "tpBindingSource";
            this.tpBindingSource.Padding = new System.Windows.Forms.Padding(3);
            this.tpBindingSource.Size = new System.Drawing.Size(725, 320);
            this.tpBindingSource.TabIndex = 8;
            this.tpBindingSource.Text = "BindingSource";
            this.tpBindingSource.UseVisualStyleBackColor = true;
            // 
            // lvBindingSource
            // 
            this.lvBindingSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvBindingSource.Location = new System.Drawing.Point(123, 48);
            this.lvBindingSource.MultiSelect = false;
            this.lvBindingSource.Name = "lvBindingSource";
            this.lvBindingSource.Size = new System.Drawing.Size(375, 230);
            this.lvBindingSource.TabIndex = 42;
            this.lvBindingSource.UseCompatibleStateImageBehavior = false;
            this.lvBindingSource.View = System.Windows.Forms.View.Details;
            this.lvBindingSource.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvSelectedFields_ColumnClick);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "BindingSource";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "DataMember";
            this.columnHeader5.Width = 180;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "";
            this.columnHeader6.Width = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(121, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "BindingSource List";
            // 
            // tpLayout
            // 
            this.tpLayout.Controls.Add(this.cbViewBindingSource);
            this.tpLayout.Controls.Add(this.label18);
            this.tpLayout.Controls.Add(this.label14);
            this.tpLayout.Controls.Add(this.cbControlType);
            this.tpLayout.Controls.Add(this.label8);
            this.tpLayout.Controls.Add(this.cbBindingSource);
            this.tpLayout.Controls.Add(this.label7);
            this.tpLayout.Controls.Add(this.btnDeleteField);
            this.tpLayout.Controls.Add(this.btnAddField);
            this.tpLayout.Controls.Add(this.lvSelectedFields);
            this.tpLayout.Controls.Add(this.label22);
            this.tpLayout.Controls.Add(this.tvContainer);
            this.tpLayout.Location = new System.Drawing.Point(4, 21);
            this.tpLayout.Name = "tpLayout";
            this.tpLayout.Padding = new System.Windows.Forms.Padding(3);
            this.tpLayout.Size = new System.Drawing.Size(725, 320);
            this.tpLayout.TabIndex = 7;
            this.tpLayout.Text = "Layout";
            this.tpLayout.UseVisualStyleBackColor = true;
            // 
            // cbViewBindingSource
            // 
            this.cbViewBindingSource.Enabled = false;
            this.cbViewBindingSource.FormattingEnabled = true;
            this.cbViewBindingSource.Items.AddRange(new object[] {
            "",
            "TextBox",
            "Grid"});
            this.cbViewBindingSource.Location = new System.Drawing.Point(414, 79);
            this.cbViewBindingSource.Name = "cbViewBindingSource";
            this.cbViewBindingSource.Size = new System.Drawing.Size(206, 20);
            this.cbViewBindingSource.TabIndex = 30;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(301, 82);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(107, 12);
            this.label18.TabIndex = 29;
            this.label18.Text = "ViewBindingSource";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(291, 110);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 28;
            this.label14.Text = "Field List";
            // 
            // cbControlType
            // 
            this.cbControlType.Enabled = false;
            this.cbControlType.FormattingEnabled = true;
            this.cbControlType.Items.AddRange(new object[] {
            "",
            "TextBox",
            "Grid"});
            this.cbControlType.Location = new System.Drawing.Point(414, 48);
            this.cbControlType.Name = "cbControlType";
            this.cbControlType.Size = new System.Drawing.Size(206, 20);
            this.cbControlType.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(301, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "Control Type";
            // 
            // cbBindingSource
            // 
            this.cbBindingSource.Enabled = false;
            this.cbBindingSource.FormattingEnabled = true;
            this.cbBindingSource.Location = new System.Drawing.Point(414, 19);
            this.cbBindingSource.Name = "cbBindingSource";
            this.cbBindingSource.Size = new System.Drawing.Size(206, 20);
            this.cbBindingSource.TabIndex = 25;
            this.cbBindingSource.SelectedIndexChanged += new System.EventHandler(this.cbBindingSource_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(301, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "BindingSource";
            // 
            // btnDeleteField
            // 
            this.btnDeleteField.Enabled = false;
            this.btnDeleteField.Location = new System.Drawing.Point(655, 167);
            this.btnDeleteField.Name = "btnDeleteField";
            this.btnDeleteField.Size = new System.Drawing.Size(56, 23);
            this.btnDeleteField.TabIndex = 23;
            this.btnDeleteField.Text = "Delete";
            this.btnDeleteField.UseVisualStyleBackColor = true;
            this.btnDeleteField.Click += new System.EventHandler(this.btnDeleteField_Click);
            // 
            // btnAddField
            // 
            this.btnAddField.Enabled = false;
            this.btnAddField.Location = new System.Drawing.Point(655, 138);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(56, 23);
            this.btnAddField.TabIndex = 22;
            this.btnAddField.Text = "Add";
            this.btnAddField.UseVisualStyleBackColor = true;
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
            // 
            // lvSelectedFields
            // 
            this.lvSelectedFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17});
            this.lvSelectedFields.Location = new System.Drawing.Point(291, 128);
            this.lvSelectedFields.Name = "lvSelectedFields";
            this.lvSelectedFields.Size = new System.Drawing.Size(358, 180);
            this.lvSelectedFields.TabIndex = 21;
            this.lvSelectedFields.UseCompatibleStateImageBehavior = false;
            this.lvSelectedFields.View = System.Windows.Forms.View.Details;
            this.lvSelectedFields.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvSelectedFields_ColumnClick);
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Field Name";
            this.columnHeader15.Width = 120;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Description";
            this.columnHeader16.Width = 130;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "";
            this.columnHeader17.Width = 30;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(19, 12);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(89, 12);
            this.label22.TabIndex = 20;
            this.label22.Text = "Container Tree";
            // 
            // tvContainer
            // 
            this.tvContainer.HideSelection = false;
            this.tvContainer.Location = new System.Drawing.Point(21, 27);
            this.tvContainer.Name = "tvContainer";
            this.tvContainer.Size = new System.Drawing.Size(234, 281);
            this.tvContainer.TabIndex = 19;
            this.tvContainer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvContainer_AfterSelect);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(627, 362);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(127, 362);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 21);
            this.btnNext.TabIndex = 12;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.SelectedPath = "folderBrowserDialog1";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(25, 362);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 21);
            this.btnPrevious.TabIndex = 14;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(535, 362);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 21);
            this.btnDone.TabIndex = 15;
            this.btnDone.Text = "Done";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "*.sln";
            this.openFileDialog.Filter = "Solution Files (*.sln)|*.sln";
            // 
            // fmExtClientWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 398);
            this.ControlBox = false;
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnNext);
            this.Name = "fmExtClientWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EEP Extensive Windows Form Client Package Wizard";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpConnection.ResumeLayout(false);
            this.tpConnection.PerformLayout();
            this.tpOutputSetting.ResumeLayout(false);
            this.tpOutputSetting.PerformLayout();
            this.tpFormSetting.ResumeLayout(false);
            this.tpFormSetting.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpProvider.ResumeLayout(false);
            this.tpProvider.PerformLayout();
            this.tpBindingSource.ResumeLayout(false);
            this.tpBindingSource.PerformLayout();
            this.tpLayout.ResumeLayout(false);
            this.tpLayout.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpOutputSetting;
		private System.Windows.Forms.TabPage tpFormSetting;
		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.TextBox tbPackageName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.Button btnPrevious;
		private System.Windows.Forms.TextBox tbClientPackage;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rbWindowsForm;
		private System.Windows.Forms.RadioButton rbEEPBaseForm;
		private System.Windows.Forms.ComboBox cbBaseForm;
		private System.Windows.Forms.TextBox tbFormName;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tpProvider;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnNewLocation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbNewLocation;
        private System.Windows.Forms.Button btnSolutionName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbSolutionName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbNewSolutionName;
        private System.Windows.Forms.RadioButton rbAddToExistSln;
        private System.Windows.Forms.RadioButton rbNewSolution;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCurrentSolution;
        private System.Windows.Forms.RadioButton rbAddToCurrent;
        private System.Windows.Forms.TabPage tpConnection;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnConnectionString;
        private System.Windows.Forms.TextBox tbConnectionString;
        private System.Windows.Forms.ComboBox cbDatabaseType;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbEEPAlias;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnOutputPath;
        private System.Windows.Forms.TextBox tbOutputPath;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbColumnCount;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnAssemblyOutputPath;
        private System.Windows.Forms.TextBox tbAssemblyOutputPath;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbFormText;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbChooseLanguage;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TabPage tpLayout;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TreeView tvContainer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lvDataSet;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TabPage tpBindingSource;
        private System.Windows.Forms.ListView lvBindingSource;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbControlType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbBindingSource;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDeleteField;
        private System.Windows.Forms.Button btnAddField;
        private System.Windows.Forms.ListView lvSelectedFields;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ComboBox cbViewBindingSource;
        private System.Windows.Forms.Label label18;


	}
}