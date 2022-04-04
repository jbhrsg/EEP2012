namespace MWizard
{
	partial class fmEEPWCFWebWizard
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Single", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("MasterDetail", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Report", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, ""),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "WCFWSingle", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134))))}, "3.png");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, ""),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "WCFWMasterDetail", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134))))}, "5.png");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmEEPWCFWebWizard));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tbConnection = new System.Windows.Forms.TabPage();
            this.cbChooseLanguage = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbEEPAlias = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cbDatabaseType = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.tpOutputSetting = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbCurrentSolution = new System.Windows.Forms.RadioButton();
            this.rbAddToExistSolution = new System.Windows.Forms.RadioButton();
            this.tbSolutionName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCurrentSolution = new System.Windows.Forms.TextBox();
            this.btnSolutionName = new System.Windows.Forms.Button();
            this.cbWebSite = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbAddToRootFolder = new System.Windows.Forms.RadioButton();
            this.tbAddToNewFolder = new System.Windows.Forms.TextBox();
            this.rbAddToNewFolder = new System.Windows.Forms.RadioButton();
            this.cbAddToExistFolder = new System.Windows.Forms.ComboBox();
            this.rbAddToExistFolder = new System.Windows.Forms.RadioButton();
            this.tpFormSetting = new System.Windows.Forms.TabPage();
            this.btnConfig = new System.Windows.Forms.Button();
            this.tbFormTitle = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbFormName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.lvTemplate = new System.Windows.Forms.ListView();
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ilTemplate = new System.Windows.Forms.ImageList(this.components);
            this.tpDataSource = new System.Windows.Forms.TabPage();
            this.label37 = new System.Windows.Forms.Label();
            this.tbEntitySetName = new System.Windows.Forms.TextBox();
            this.cbDetailEntityName = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbEntityName = new System.Windows.Forms.TextBox();
            this.tbRemoteName = new System.Windows.Forms.TextBox();
            this.btnRemoteName = new System.Windows.Forms.Button();
            this.tbCommandName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tpViewFields = new System.Windows.Forms.TabPage();
            this.btnViewDown = new System.Windows.Forms.Button();
            this.btnViewUp = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnViewRemoveAll = new System.Windows.Forms.Button();
            this.btnViewAddAll = new System.Windows.Forms.Button();
            this.btnViewRemove = new System.Windows.Forms.Button();
            this.lvViewDesField = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnViewAdd = new System.Windows.Forms.Button();
            this.lvViewSrcField = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpMasterFields = new System.Windows.Forms.TabPage();
            this.tbCaption = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tbEditMask = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbQueryMode = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cbCheckNull = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tbDefaultValue = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cbControlType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.gbComboBox = new System.Windows.Forms.GroupBox();
            this.tbComboEntityName = new System.Windows.Forms.TextBox();
            this.tbComboEntitySetName = new System.Windows.Forms.TextBox();
            this.btnComboRemoteName = new System.Windows.Forms.Button();
            this.tbComboRemoteName = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cbDataTextField = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cbDataValueField = new System.Windows.Forms.ComboBox();
            this.btnMasterDown = new System.Windows.Forms.Button();
            this.btnMasterUp = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnMasterRemoveAll = new System.Windows.Forms.Button();
            this.btnMasterAddAll = new System.Windows.Forms.Button();
            this.btnMasterRemove = new System.Windows.Forms.Button();
            this.lvMasterDesField = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnMasterAll = new System.Windows.Forms.Button();
            this.lvMasterSrcField = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpDetailFields = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbCaption_D = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbEditMask_D = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cbQueryMode_D = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.cbCheckNull_D = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.tbDefaultValue_D = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cbControlType_D = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.gbDetailCombo = new System.Windows.Forms.GroupBox();
            this.btnComboRemoteName_D = new System.Windows.Forms.Button();
            this.tbComboRemoteName_D = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.cbComboDisplayField_D = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.cbComboValueField_D = new System.Windows.Forms.ComboBox();
            this.btnDetailDown = new System.Windows.Forms.Button();
            this.btnDetailUp = new System.Windows.Forms.Button();
            this.btnDeleteField = new System.Windows.Forms.Button();
            this.btnNewField = new System.Windows.Forms.Button();
            this.lvSelectedFields = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbDetailTableName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tvRelation = new System.Windows.Forms.TreeView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tbComboEntitySetName_D = new System.Windows.Forms.TextBox();
            this.tbComboEntityName_D = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tbConnection.SuspendLayout();
            this.tpOutputSetting.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tpFormSetting.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpDataSource.SuspendLayout();
            this.tpViewFields.SuspendLayout();
            this.tpMasterFields.SuspendLayout();
            this.gbComboBox.SuspendLayout();
            this.tpDetailFields.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbDetailCombo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tbConnection);
            this.tabControl.Controls.Add(this.tpOutputSetting);
            this.tabControl.Controls.Add(this.tpFormSetting);
            this.tabControl.Controls.Add(this.tpDataSource);
            this.tabControl.Controls.Add(this.tpViewFields);
            this.tabControl.Controls.Add(this.tpMasterFields);
            this.tabControl.Controls.Add(this.tpDetailFields);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.RightToLeftLayout = true;
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(733, 384);
            this.tabControl.TabIndex = 8;
            // 
            // tbConnection
            // 
            this.tbConnection.Controls.Add(this.cbChooseLanguage);
            this.tbConnection.Controls.Add(this.label11);
            this.tbConnection.Controls.Add(this.cbEEPAlias);
            this.tbConnection.Controls.Add(this.label18);
            this.tbConnection.Controls.Add(this.cbDatabaseType);
            this.tbConnection.Controls.Add(this.label36);
            this.tbConnection.Location = new System.Drawing.Point(4, 22);
            this.tbConnection.Name = "tbConnection";
            this.tbConnection.Size = new System.Drawing.Size(725, 358);
            this.tbConnection.TabIndex = 8;
            this.tbConnection.Text = "Connection";
            this.tbConnection.UseVisualStyleBackColor = true;
            // 
            // cbChooseLanguage
            // 
            this.cbChooseLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChooseLanguage.FormattingEnabled = true;
            this.cbChooseLanguage.Items.AddRange(new object[] {
            "C#",
            "VB"});
            this.cbChooseLanguage.Location = new System.Drawing.Point(185, 234);
            this.cbChooseLanguage.Name = "cbChooseLanguage";
            this.cbChooseLanguage.Size = new System.Drawing.Size(184, 20);
            this.cbChooseLanguage.TabIndex = 54;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 8F);
            this.label11.Location = new System.Drawing.Point(83, 236);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 14);
            this.label11.TabIndex = 53;
            this.label11.Text = "Choose Language";
            // 
            // cbEEPAlias
            // 
            this.cbEEPAlias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEEPAlias.FormattingEnabled = true;
            this.cbEEPAlias.Location = new System.Drawing.Point(185, 72);
            this.cbEEPAlias.Name = "cbEEPAlias";
            this.cbEEPAlias.Size = new System.Drawing.Size(419, 20);
            this.cbEEPAlias.TabIndex = 13;
            this.cbEEPAlias.SelectedIndexChanged += new System.EventHandler(this.cbEEPAlias_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial", 8F);
            this.label18.Location = new System.Drawing.Point(127, 74);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(51, 14);
            this.label18.TabIndex = 12;
            this.label18.Text = "EEP Alias";
            // 
            // cbDatabaseType
            // 
            this.cbDatabaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabaseType.FormattingEnabled = true;
            this.cbDatabaseType.Items.AddRange(new object[] {
            "None",
            "MSSQL",
            "OleDB",
            "Oracle",
            "ODBC",
            "MySql",
            "Informix"});
            this.cbDatabaseType.Location = new System.Drawing.Point(185, 152);
            this.cbDatabaseType.Name = "cbDatabaseType";
            this.cbDatabaseType.Size = new System.Drawing.Size(184, 20);
            this.cbDatabaseType.TabIndex = 11;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Arial", 8F);
            this.label36.Location = new System.Drawing.Point(99, 154);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(79, 14);
            this.label36.TabIndex = 10;
            this.label36.Text = "Database Type";
            // 
            // tpOutputSetting
            // 
            this.tpOutputSetting.AutoScroll = true;
            this.tpOutputSetting.Controls.Add(this.panel1);
            this.tpOutputSetting.Controls.Add(this.cbWebSite);
            this.tpOutputSetting.Controls.Add(this.label2);
            this.tpOutputSetting.Controls.Add(this.rbAddToRootFolder);
            this.tpOutputSetting.Controls.Add(this.tbAddToNewFolder);
            this.tpOutputSetting.Controls.Add(this.rbAddToNewFolder);
            this.tpOutputSetting.Controls.Add(this.cbAddToExistFolder);
            this.tpOutputSetting.Controls.Add(this.rbAddToExistFolder);
            this.tpOutputSetting.Location = new System.Drawing.Point(4, 22);
            this.tpOutputSetting.Name = "tpOutputSetting";
            this.tpOutputSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpOutputSetting.Size = new System.Drawing.Size(725, 358);
            this.tpOutputSetting.TabIndex = 0;
            this.tpOutputSetting.Text = "Output Setting";
            this.tpOutputSetting.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbCurrentSolution);
            this.panel1.Controls.Add(this.rbAddToExistSolution);
            this.panel1.Controls.Add(this.tbSolutionName);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbCurrentSolution);
            this.panel1.Controls.Add(this.btnSolutionName);
            this.panel1.Location = new System.Drawing.Point(52, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 142);
            this.panel1.TabIndex = 50;
            // 
            // rbCurrentSolution
            // 
            this.rbCurrentSolution.AutoSize = true;
            this.rbCurrentSolution.Font = new System.Drawing.Font("Arial", 8F);
            this.rbCurrentSolution.Location = new System.Drawing.Point(18, 19);
            this.rbCurrentSolution.Name = "rbCurrentSolution";
            this.rbCurrentSolution.Size = new System.Drawing.Size(139, 18);
            this.rbCurrentSolution.TabIndex = 46;
            this.rbCurrentSolution.TabStop = true;
            this.rbCurrentSolution.Text = "Add To Current Solution";
            this.rbCurrentSolution.UseVisualStyleBackColor = true;
            this.rbCurrentSolution.CheckedChanged += new System.EventHandler(this.rbCurrentSolution_CheckedChanged);
            // 
            // rbAddToExistSolution
            // 
            this.rbAddToExistSolution.AutoSize = true;
            this.rbAddToExistSolution.Font = new System.Drawing.Font("Arial", 8F);
            this.rbAddToExistSolution.Location = new System.Drawing.Point(18, 82);
            this.rbAddToExistSolution.Name = "rbAddToExistSolution";
            this.rbAddToExistSolution.Size = new System.Drawing.Size(126, 18);
            this.rbAddToExistSolution.TabIndex = 49;
            this.rbAddToExistSolution.TabStop = true;
            this.rbAddToExistSolution.Text = "Add To Exist Solution";
            this.rbAddToExistSolution.UseVisualStyleBackColor = true;
            this.rbAddToExistSolution.CheckedChanged += new System.EventHandler(this.rbAddToExistSolution_CheckedChanged);
            // 
            // tbSolutionName
            // 
            this.tbSolutionName.Location = new System.Drawing.Point(119, 104);
            this.tbSolutionName.Name = "tbSolutionName";
            this.tbSolutionName.Size = new System.Drawing.Size(400, 21);
            this.tbSolutionName.TabIndex = 36;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 8F);
            this.label7.Location = new System.Drawing.Point(36, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 14);
            this.label7.TabIndex = 48;
            this.label7.Text = "Solution Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8F);
            this.label1.Location = new System.Drawing.Point(36, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 14);
            this.label1.TabIndex = 37;
            this.label1.Text = "Solution Name:";
            // 
            // tbCurrentSolution
            // 
            this.tbCurrentSolution.BackColor = System.Drawing.SystemColors.Menu;
            this.tbCurrentSolution.Enabled = false;
            this.tbCurrentSolution.Location = new System.Drawing.Point(119, 41);
            this.tbCurrentSolution.Name = "tbCurrentSolution";
            this.tbCurrentSolution.Size = new System.Drawing.Size(400, 21);
            this.tbCurrentSolution.TabIndex = 47;
            // 
            // btnSolutionName
            // 
            this.btnSolutionName.Location = new System.Drawing.Point(520, 103);
            this.btnSolutionName.Name = "btnSolutionName";
            this.btnSolutionName.Size = new System.Drawing.Size(27, 23);
            this.btnSolutionName.TabIndex = 38;
            this.btnSolutionName.Text = "...";
            this.btnSolutionName.Click += new System.EventHandler(this.btnSolutionName_Click);
            // 
            // cbWebSite
            // 
            this.cbWebSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWebSite.FormattingEnabled = true;
            this.cbWebSite.Location = new System.Drawing.Point(171, 168);
            this.cbWebSite.Name = "cbWebSite";
            this.cbWebSite.Size = new System.Drawing.Size(400, 20);
            this.cbWebSite.TabIndex = 45;
            this.cbWebSite.SelectedIndexChanged += new System.EventHandler(this.cbWebSite_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8F);
            this.label2.Location = new System.Drawing.Point(113, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 14);
            this.label2.TabIndex = 44;
            this.label2.Text = "WebSite: ";
            // 
            // rbAddToRootFolder
            // 
            this.rbAddToRootFolder.AutoSize = true;
            this.rbAddToRootFolder.Font = new System.Drawing.Font("Arial", 8F);
            this.rbAddToRootFolder.Location = new System.Drawing.Point(73, 310);
            this.rbAddToRootFolder.Name = "rbAddToRootFolder";
            this.rbAddToRootFolder.Size = new System.Drawing.Size(117, 18);
            this.rbAddToRootFolder.TabIndex = 43;
            this.rbAddToRootFolder.Text = "Add To Root Folder";
            this.rbAddToRootFolder.UseVisualStyleBackColor = true;
            this.rbAddToRootFolder.CheckedChanged += new System.EventHandler(this.rbAddToRootFolder_CheckedChanged);
            // 
            // tbAddToNewFolder
            // 
            this.tbAddToNewFolder.Location = new System.Drawing.Point(195, 265);
            this.tbAddToNewFolder.Name = "tbAddToNewFolder";
            this.tbAddToNewFolder.Size = new System.Drawing.Size(376, 21);
            this.tbAddToNewFolder.TabIndex = 42;
            // 
            // rbAddToNewFolder
            // 
            this.rbAddToNewFolder.AutoSize = true;
            this.rbAddToNewFolder.Font = new System.Drawing.Font("Arial", 8F);
            this.rbAddToNewFolder.Location = new System.Drawing.Point(73, 266);
            this.rbAddToNewFolder.Name = "rbAddToNewFolder";
            this.rbAddToNewFolder.Size = new System.Drawing.Size(118, 18);
            this.rbAddToNewFolder.TabIndex = 41;
            this.rbAddToNewFolder.Text = "Add To New Folder";
            this.rbAddToNewFolder.UseVisualStyleBackColor = true;
            this.rbAddToNewFolder.CheckedChanged += new System.EventHandler(this.rbAddToNewFolder_CheckedChanged);
            // 
            // cbAddToExistFolder
            // 
            this.cbAddToExistFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAddToExistFolder.FormattingEnabled = true;
            this.cbAddToExistFolder.Location = new System.Drawing.Point(195, 218);
            this.cbAddToExistFolder.Name = "cbAddToExistFolder";
            this.cbAddToExistFolder.Size = new System.Drawing.Size(376, 20);
            this.cbAddToExistFolder.TabIndex = 40;
            // 
            // rbAddToExistFolder
            // 
            this.rbAddToExistFolder.AutoSize = true;
            this.rbAddToExistFolder.Checked = true;
            this.rbAddToExistFolder.Font = new System.Drawing.Font("Arial", 8F);
            this.rbAddToExistFolder.Location = new System.Drawing.Point(73, 219);
            this.rbAddToExistFolder.Name = "rbAddToExistFolder";
            this.rbAddToExistFolder.Size = new System.Drawing.Size(118, 18);
            this.rbAddToExistFolder.TabIndex = 39;
            this.rbAddToExistFolder.TabStop = true;
            this.rbAddToExistFolder.Text = "Add To Exist Folder";
            this.rbAddToExistFolder.UseVisualStyleBackColor = true;
            this.rbAddToExistFolder.CheckedChanged += new System.EventHandler(this.rbAddToExistFolder_CheckedChanged);
            // 
            // tpFormSetting
            // 
            this.tpFormSetting.Controls.Add(this.btnConfig);
            this.tpFormSetting.Controls.Add(this.tbFormTitle);
            this.tpFormSetting.Controls.Add(this.label10);
            this.tpFormSetting.Controls.Add(this.tbFormName);
            this.tpFormSetting.Controls.Add(this.label4);
            this.tpFormSetting.Controls.Add(this.groupBox1);
            this.tpFormSetting.Location = new System.Drawing.Point(4, 22);
            this.tpFormSetting.Name = "tpFormSetting";
            this.tpFormSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpFormSetting.Size = new System.Drawing.Size(725, 358);
            this.tpFormSetting.TabIndex = 1;
            this.tpFormSetting.Text = "Form Setting";
            this.tpFormSetting.UseVisualStyleBackColor = true;
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(626, 26);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(75, 23);
            this.btnConfig.TabIndex = 7;
            this.btnConfig.Text = "Config";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // tbFormTitle
            // 
            this.tbFormTitle.Location = new System.Drawing.Point(61, 325);
            this.tbFormTitle.Name = "tbFormTitle";
            this.tbFormTitle.Size = new System.Drawing.Size(450, 21);
            this.tbFormTitle.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(59, 310);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(143, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "Please input Form Title";
            // 
            // tbFormName
            // 
            this.tbFormName.Location = new System.Drawing.Point(61, 284);
            this.tbFormName.Name = "tbFormName";
            this.tbFormName.Size = new System.Drawing.Size(450, 21);
            this.tbFormName.TabIndex = 4;
            this.tbFormName.TextChanged += new System.EventHandler(this.tbFormName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Please input Form Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.tbDescription);
            this.groupBox1.Controls.Add(this.lvTemplate);
            this.groupBox1.Location = new System.Drawing.Point(61, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(538, 251);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inherit From";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(328, 22);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(71, 12);
            this.label25.TabIndex = 18;
            this.label25.Text = "Description";
            // 
            // tbDescription
            // 
            this.tbDescription.BackColor = System.Drawing.SystemColors.Window;
            this.tbDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbDescription.Location = new System.Drawing.Point(328, 41);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            this.tbDescription.Size = new System.Drawing.Size(195, 142);
            this.tbDescription.TabIndex = 17;
            // 
            // lvTemplate
            // 
            this.lvTemplate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader11});
            listViewGroup1.Header = "Single";
            listViewGroup1.Name = "lvgSingle";
            listViewGroup2.Header = "MasterDetail";
            listViewGroup2.Name = "lvgMasterDetail";
            listViewGroup3.Header = "Report";
            listViewGroup3.Name = "lvgReport";
            this.lvTemplate.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            listViewItem1.Group = listViewGroup1;
            listViewItem1.ToolTipText = "WCFWSingle";
            listViewItem2.Group = listViewGroup2;
            listViewItem2.ToolTipText = "WCFWMasterDetail";
            this.lvTemplate.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvTemplate.LargeImageList = this.ilTemplate;
            this.lvTemplate.Location = new System.Drawing.Point(18, 20);
            this.lvTemplate.MultiSelect = false;
            this.lvTemplate.Name = "lvTemplate";
            this.lvTemplate.Size = new System.Drawing.Size(304, 225);
            this.lvTemplate.SmallImageList = this.ilTemplate;
            this.lvTemplate.TabIndex = 16;
            this.lvTemplate.UseCompatibleStateImageBehavior = false;
            this.lvTemplate.View = System.Windows.Forms.View.Details;
            this.lvTemplate.SelectedIndexChanged += new System.EventHandler(this.lvTemplate_SelectedIndexChanged);
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Image";
            this.columnHeader13.Width = 128;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "TemplateName";
            this.columnHeader11.Width = 120;
            // 
            // ilTemplate
            // 
            this.ilTemplate.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTemplate.ImageStream")));
            this.ilTemplate.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTemplate.Images.SetKeyName(0, "5.png");
            this.ilTemplate.Images.SetKeyName(1, "3.png");
            // 
            // tpDataSource
            // 
            this.tpDataSource.Controls.Add(this.label37);
            this.tpDataSource.Controls.Add(this.tbEntitySetName);
            this.tpDataSource.Controls.Add(this.cbDetailEntityName);
            this.tpDataSource.Controls.Add(this.label35);
            this.tpDataSource.Controls.Add(this.label3);
            this.tpDataSource.Controls.Add(this.tbEntityName);
            this.tpDataSource.Controls.Add(this.tbRemoteName);
            this.tpDataSource.Controls.Add(this.btnRemoteName);
            this.tpDataSource.Controls.Add(this.tbCommandName);
            this.tpDataSource.Controls.Add(this.label6);
            this.tpDataSource.Controls.Add(this.label5);
            this.tpDataSource.Location = new System.Drawing.Point(4, 22);
            this.tpDataSource.Name = "tpDataSource";
            this.tpDataSource.Padding = new System.Windows.Forms.Padding(3);
            this.tpDataSource.Size = new System.Drawing.Size(725, 358);
            this.tpDataSource.TabIndex = 2;
            this.tpDataSource.Text = "DataSource";
            this.tpDataSource.UseVisualStyleBackColor = true;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Arial", 8F);
            this.label37.Location = new System.Drawing.Point(53, 173);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(82, 14);
            this.label37.TabIndex = 19;
            this.label37.Text = "Entity Set Name";
            // 
            // tbEntitySetName
            // 
            this.tbEntitySetName.Location = new System.Drawing.Point(56, 190);
            this.tbEntitySetName.Name = "tbEntitySetName";
            this.tbEntitySetName.Size = new System.Drawing.Size(412, 21);
            this.tbEntitySetName.TabIndex = 18;
            // 
            // cbDetailEntityName
            // 
            this.cbDetailEntityName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDetailEntityName.FormattingEnabled = true;
            this.cbDetailEntityName.Location = new System.Drawing.Point(56, 252);
            this.cbDetailEntityName.Name = "cbDetailEntityName";
            this.cbDetailEntityName.Size = new System.Drawing.Size(412, 20);
            this.cbDetailEntityName.TabIndex = 17;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(54, 235);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(113, 12);
            this.label35.TabIndex = 16;
            this.label35.Text = "Detail Entity Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 8F);
            this.label3.Location = new System.Drawing.Point(53, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 13;
            this.label3.Text = "Entity Name";
            // 
            // tbEntityName
            // 
            this.tbEntityName.Location = new System.Drawing.Point(56, 138);
            this.tbEntityName.Name = "tbEntityName";
            this.tbEntityName.Size = new System.Drawing.Size(412, 21);
            this.tbEntityName.TabIndex = 12;
            this.tbEntityName.TextChanged += new System.EventHandler(this.tbEntityName_TextChanged);
            // 
            // tbRemoteName
            // 
            this.tbRemoteName.Location = new System.Drawing.Point(55, 41);
            this.tbRemoteName.Name = "tbRemoteName";
            this.tbRemoteName.Size = new System.Drawing.Size(413, 21);
            this.tbRemoteName.TabIndex = 11;
            this.tbRemoteName.TextChanged += new System.EventHandler(this.tbProviderName_TextChanged);
            // 
            // btnRemoteName
            // 
            this.btnRemoteName.Location = new System.Drawing.Point(474, 41);
            this.btnRemoteName.Name = "btnRemoteName";
            this.btnRemoteName.Size = new System.Drawing.Size(24, 23);
            this.btnRemoteName.TabIndex = 10;
            this.btnRemoteName.Text = "...";
            this.btnRemoteName.UseVisualStyleBackColor = true;
            this.btnRemoteName.Click += new System.EventHandler(this.btnRemoteName_Click);
            // 
            // tbCommandName
            // 
            this.tbCommandName.Location = new System.Drawing.Point(55, 89);
            this.tbCommandName.Name = "tbCommandName";
            this.tbCommandName.Size = new System.Drawing.Size(413, 21);
            this.tbCommandName.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 8F);
            this.label6.Location = new System.Drawing.Point(53, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 14);
            this.label6.TabIndex = 2;
            this.label6.Text = "Command Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 8F);
            this.label5.Location = new System.Drawing.Point(53, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "Remote Name";
            // 
            // tpViewFields
            // 
            this.tpViewFields.Controls.Add(this.btnViewDown);
            this.tpViewFields.Controls.Add(this.btnViewUp);
            this.tpViewFields.Controls.Add(this.label9);
            this.tpViewFields.Controls.Add(this.btnViewRemoveAll);
            this.tpViewFields.Controls.Add(this.btnViewAddAll);
            this.tpViewFields.Controls.Add(this.btnViewRemove);
            this.tpViewFields.Controls.Add(this.lvViewDesField);
            this.tpViewFields.Controls.Add(this.btnViewAdd);
            this.tpViewFields.Controls.Add(this.lvViewSrcField);
            this.tpViewFields.Location = new System.Drawing.Point(4, 22);
            this.tpViewFields.Name = "tpViewFields";
            this.tpViewFields.Padding = new System.Windows.Forms.Padding(3);
            this.tpViewFields.Size = new System.Drawing.Size(725, 358);
            this.tpViewFields.TabIndex = 7;
            this.tpViewFields.Text = "ViewFields";
            this.tpViewFields.UseVisualStyleBackColor = true;
            // 
            // btnViewDown
            // 
            this.btnViewDown.Location = new System.Drawing.Point(688, 92);
            this.btnViewDown.Name = "btnViewDown";
            this.btnViewDown.Size = new System.Drawing.Size(29, 23);
            this.btnViewDown.TabIndex = 19;
            this.btnViewDown.Text = "↓";
            this.btnViewDown.UseVisualStyleBackColor = true;
            this.btnViewDown.Click += new System.EventHandler(this.btnViewDown_Click);
            // 
            // btnViewUp
            // 
            this.btnViewUp.Location = new System.Drawing.Point(688, 54);
            this.btnViewUp.Name = "btnViewUp";
            this.btnViewUp.Size = new System.Drawing.Size(29, 23);
            this.btnViewUp.TabIndex = 18;
            this.btnViewUp.Text = "↑";
            this.btnViewUp.UseVisualStyleBackColor = true;
            this.btnViewUp.Click += new System.EventHandler(this.btnViewUp_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 8F);
            this.label9.Location = new System.Drawing.Point(305, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 14);
            this.label9.TabIndex = 13;
            this.label9.Text = "Selected Field";
            // 
            // btnViewRemoveAll
            // 
            this.btnViewRemoveAll.Location = new System.Drawing.Point(270, 211);
            this.btnViewRemoveAll.Name = "btnViewRemoveAll";
            this.btnViewRemoveAll.Size = new System.Drawing.Size(29, 23);
            this.btnViewRemoveAll.TabIndex = 12;
            this.btnViewRemoveAll.Text = "<<";
            this.btnViewRemoveAll.Click += new System.EventHandler(this.btnViewRemoveAll_Click);
            // 
            // btnViewAddAll
            // 
            this.btnViewAddAll.Location = new System.Drawing.Point(270, 171);
            this.btnViewAddAll.Name = "btnViewAddAll";
            this.btnViewAddAll.Size = new System.Drawing.Size(29, 23);
            this.btnViewAddAll.TabIndex = 11;
            this.btnViewAddAll.Text = ">>";
            this.btnViewAddAll.Click += new System.EventHandler(this.btnViewAddAll_Click);
            // 
            // btnViewRemove
            // 
            this.btnViewRemove.Location = new System.Drawing.Point(270, 128);
            this.btnViewRemove.Name = "btnViewRemove";
            this.btnViewRemove.Size = new System.Drawing.Size(29, 23);
            this.btnViewRemove.TabIndex = 10;
            this.btnViewRemove.Text = "<";
            this.btnViewRemove.Click += new System.EventHandler(this.btnViewRemove_Click);
            // 
            // lvViewDesField
            // 
            this.lvViewDesField.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lvViewDesField.Location = new System.Drawing.Point(307, 30);
            this.lvViewDesField.Name = "lvViewDesField";
            this.lvViewDesField.Size = new System.Drawing.Size(375, 285);
            this.lvViewDesField.TabIndex = 9;
            this.lvViewDesField.UseCompatibleStateImageBehavior = false;
            this.lvViewDesField.View = System.Windows.Forms.View.Details;
            this.lvViewDesField.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvViewSrcField_ColumnClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Field Name";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Description";
            this.columnHeader4.Width = 130;
            // 
            // btnViewAdd
            // 
            this.btnViewAdd.Location = new System.Drawing.Point(270, 87);
            this.btnViewAdd.Name = "btnViewAdd";
            this.btnViewAdd.Size = new System.Drawing.Size(29, 23);
            this.btnViewAdd.TabIndex = 8;
            this.btnViewAdd.Text = ">";
            this.btnViewAdd.Click += new System.EventHandler(this.btnViewAdd_Click);
            // 
            // lvViewSrcField
            // 
            this.lvViewSrcField.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
            this.lvViewSrcField.Location = new System.Drawing.Point(7, 30);
            this.lvViewSrcField.Name = "lvViewSrcField";
            this.lvViewSrcField.Size = new System.Drawing.Size(257, 285);
            this.lvViewSrcField.TabIndex = 7;
            this.lvViewSrcField.UseCompatibleStateImageBehavior = false;
            this.lvViewSrcField.View = System.Windows.Forms.View.Details;
            this.lvViewSrcField.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvViewSrcField_ColumnClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Field Name";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Description";
            this.columnHeader1.Width = 130;
            // 
            // tpMasterFields
            // 
            this.tpMasterFields.Controls.Add(this.tbCaption);
            this.tpMasterFields.Controls.Add(this.label24);
            this.tpMasterFields.Controls.Add(this.tbEditMask);
            this.tpMasterFields.Controls.Add(this.label20);
            this.tpMasterFields.Controls.Add(this.cbQueryMode);
            this.tpMasterFields.Controls.Add(this.label21);
            this.tpMasterFields.Controls.Add(this.cbCheckNull);
            this.tpMasterFields.Controls.Add(this.label22);
            this.tpMasterFields.Controls.Add(this.tbDefaultValue);
            this.tpMasterFields.Controls.Add(this.label23);
            this.tpMasterFields.Controls.Add(this.cbControlType);
            this.tpMasterFields.Controls.Add(this.label12);
            this.tpMasterFields.Controls.Add(this.gbComboBox);
            this.tpMasterFields.Controls.Add(this.btnMasterDown);
            this.tpMasterFields.Controls.Add(this.btnMasterUp);
            this.tpMasterFields.Controls.Add(this.label8);
            this.tpMasterFields.Controls.Add(this.btnMasterRemoveAll);
            this.tpMasterFields.Controls.Add(this.btnMasterAddAll);
            this.tpMasterFields.Controls.Add(this.btnMasterRemove);
            this.tpMasterFields.Controls.Add(this.lvMasterDesField);
            this.tpMasterFields.Controls.Add(this.btnMasterAll);
            this.tpMasterFields.Controls.Add(this.lvMasterSrcField);
            this.tpMasterFields.Location = new System.Drawing.Point(4, 22);
            this.tpMasterFields.Name = "tpMasterFields";
            this.tpMasterFields.Padding = new System.Windows.Forms.Padding(3);
            this.tpMasterFields.Size = new System.Drawing.Size(725, 358);
            this.tpMasterFields.TabIndex = 4;
            this.tpMasterFields.Text = "MasterFields";
            this.tpMasterFields.UseVisualStyleBackColor = true;
            // 
            // tbCaption
            // 
            this.tbCaption.Location = new System.Drawing.Point(569, 41);
            this.tbCaption.Name = "tbCaption";
            this.tbCaption.Size = new System.Drawing.Size(142, 21);
            this.tbCaption.TabIndex = 39;
            this.tbCaption.TextChanged += new System.EventHandler(this.tbCaption_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(516, 44);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(47, 12);
            this.label24.TabIndex = 38;
            this.label24.Text = "Caption";
            // 
            // tbEditMask
            // 
            this.tbEditMask.Location = new System.Drawing.Point(569, 141);
            this.tbEditMask.Name = "tbEditMask";
            this.tbEditMask.Size = new System.Drawing.Size(142, 21);
            this.tbEditMask.TabIndex = 37;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(504, 144);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(59, 12);
            this.label20.TabIndex = 36;
            this.label20.Text = "Edit Mask";
            // 
            // cbQueryMode
            // 
            this.cbQueryMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQueryMode.FormattingEnabled = true;
            this.cbQueryMode.Items.AddRange(new object[] {
            "",
            "None",
            "Normal",
            "Range"});
            this.cbQueryMode.Location = new System.Drawing.Point(569, 117);
            this.cbQueryMode.Name = "cbQueryMode";
            this.cbQueryMode.Size = new System.Drawing.Size(142, 20);
            this.cbQueryMode.TabIndex = 35;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(498, 120);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 12);
            this.label21.TabIndex = 34;
            this.label21.Text = "Query Mode";
            // 
            // cbCheckNull
            // 
            this.cbCheckNull.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCheckNull.FormattingEnabled = true;
            this.cbCheckNull.Items.AddRange(new object[] {
            "",
            "N",
            "Y"});
            this.cbCheckNull.Location = new System.Drawing.Point(569, 67);
            this.cbCheckNull.Name = "cbCheckNull";
            this.cbCheckNull.Size = new System.Drawing.Size(142, 20);
            this.cbCheckNull.TabIndex = 33;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(498, 70);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(65, 12);
            this.label22.TabIndex = 32;
            this.label22.Text = "Check Null";
            // 
            // tbDefaultValue
            // 
            this.tbDefaultValue.Location = new System.Drawing.Point(569, 91);
            this.tbDefaultValue.Name = "tbDefaultValue";
            this.tbDefaultValue.Size = new System.Drawing.Size(142, 21);
            this.tbDefaultValue.TabIndex = 31;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(480, 94);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(83, 12);
            this.label23.TabIndex = 30;
            this.label23.Text = "Default Value";
            // 
            // cbControlType
            // 
            this.cbControlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbControlType.FormattingEnabled = true;
            this.cbControlType.Items.AddRange(new object[] {
            "TextBox",
            "ComboBox",
            "RefValBox",
            "ValidateBox",
            "DateTimeBox",
            "CheckBox"});
            this.cbControlType.Location = new System.Drawing.Point(569, 185);
            this.cbControlType.Name = "cbControlType";
            this.cbControlType.Size = new System.Drawing.Size(142, 20);
            this.cbControlType.TabIndex = 29;
            this.cbControlType.SelectedIndexChanged += new System.EventHandler(this.cbControlType_SelectedValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(486, 188);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 28;
            this.label12.Text = "Control Type";
            // 
            // gbComboBox
            // 
            this.gbComboBox.Controls.Add(this.tbComboEntityName);
            this.gbComboBox.Controls.Add(this.tbComboEntitySetName);
            this.gbComboBox.Controls.Add(this.btnComboRemoteName);
            this.gbComboBox.Controls.Add(this.tbComboRemoteName);
            this.gbComboBox.Controls.Add(this.label16);
            this.gbComboBox.Controls.Add(this.label13);
            this.gbComboBox.Controls.Add(this.label15);
            this.gbComboBox.Controls.Add(this.cbDataTextField);
            this.gbComboBox.Controls.Add(this.label19);
            this.gbComboBox.Controls.Add(this.cbDataValueField);
            this.gbComboBox.Enabled = false;
            this.gbComboBox.Location = new System.Drawing.Point(476, 211);
            this.gbComboBox.Name = "gbComboBox";
            this.gbComboBox.Size = new System.Drawing.Size(241, 131);
            this.gbComboBox.TabIndex = 27;
            this.gbComboBox.TabStop = false;
            this.gbComboBox.Text = "ComboBox Setting";
            // 
            // tbComboEntityName
            // 
            this.tbComboEntityName.Location = new System.Drawing.Point(93, 44);
            this.tbComboEntityName.Name = "tbComboEntityName";
            this.tbComboEntityName.Size = new System.Drawing.Size(142, 21);
            this.tbComboEntityName.TabIndex = 40;
            this.tbComboEntityName.TextChanged += new System.EventHandler(this.tbTableName_TextChanged);
            // 
            // tbComboEntitySetName
            // 
            this.tbComboEntitySetName.Location = new System.Drawing.Point(93, 44);
            this.tbComboEntitySetName.Name = "tbComboEntitySetName";
            this.tbComboEntitySetName.Size = new System.Drawing.Size(142, 21);
            this.tbComboEntitySetName.TabIndex = 43;
            // 
            // btnComboRemoteName
            // 
            this.btnComboRemoteName.Location = new System.Drawing.Point(213, 18);
            this.btnComboRemoteName.Name = "btnComboRemoteName";
            this.btnComboRemoteName.Size = new System.Drawing.Size(22, 21);
            this.btnComboRemoteName.TabIndex = 42;
            this.btnComboRemoteName.Text = "...";
            this.btnComboRemoteName.UseVisualStyleBackColor = true;
            this.btnComboRemoteName.Click += new System.EventHandler(this.btnComboRemoteName_Click);
            // 
            // tbComboRemoteName
            // 
            this.tbComboRemoteName.Location = new System.Drawing.Point(92, 18);
            this.tbComboRemoteName.Name = "tbComboRemoteName";
            this.tbComboRemoteName.Size = new System.Drawing.Size(115, 21);
            this.tbComboRemoteName.TabIndex = 41;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(22, 21);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 40;
            this.label16.Text = "RemoteName";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(22, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 12);
            this.label13.TabIndex = 9;
            this.label13.Text = "Entity Name";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 76);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(83, 12);
            this.label15.TabIndex = 11;
            this.label15.Text = "Display Field";
            // 
            // cbDataTextField
            // 
            this.cbDataTextField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataTextField.FormattingEnabled = true;
            this.cbDataTextField.Location = new System.Drawing.Point(93, 73);
            this.cbDataTextField.Name = "cbDataTextField";
            this.cbDataTextField.Size = new System.Drawing.Size(142, 20);
            this.cbDataTextField.TabIndex = 12;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(16, 105);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(71, 12);
            this.label19.TabIndex = 13;
            this.label19.Text = "Value Field";
            // 
            // cbDataValueField
            // 
            this.cbDataValueField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataValueField.FormattingEnabled = true;
            this.cbDataValueField.Location = new System.Drawing.Point(93, 102);
            this.cbDataValueField.Name = "cbDataValueField";
            this.cbDataValueField.Size = new System.Drawing.Size(142, 20);
            this.cbDataValueField.TabIndex = 14;
            // 
            // btnMasterDown
            // 
            this.btnMasterDown.Location = new System.Drawing.Point(470, 64);
            this.btnMasterDown.Name = "btnMasterDown";
            this.btnMasterDown.Size = new System.Drawing.Size(23, 23);
            this.btnMasterDown.TabIndex = 21;
            this.btnMasterDown.Text = "↓";
            this.btnMasterDown.UseVisualStyleBackColor = true;
            this.btnMasterDown.Click += new System.EventHandler(this.btnMasterDown_Click);
            // 
            // btnMasterUp
            // 
            this.btnMasterUp.Location = new System.Drawing.Point(470, 30);
            this.btnMasterUp.Name = "btnMasterUp";
            this.btnMasterUp.Size = new System.Drawing.Size(23, 23);
            this.btnMasterUp.TabIndex = 20;
            this.btnMasterUp.Text = "↑";
            this.btnMasterUp.UseVisualStyleBackColor = true;
            this.btnMasterUp.Click += new System.EventHandler(this.btnMasterUp_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 8F);
            this.label8.Location = new System.Drawing.Point(255, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 14);
            this.label8.TabIndex = 13;
            this.label8.Text = "Selected Field";
            // 
            // btnMasterRemoveAll
            // 
            this.btnMasterRemoveAll.Location = new System.Drawing.Point(219, 238);
            this.btnMasterRemoveAll.Name = "btnMasterRemoveAll";
            this.btnMasterRemoveAll.Size = new System.Drawing.Size(29, 23);
            this.btnMasterRemoveAll.TabIndex = 12;
            this.btnMasterRemoveAll.Text = "<<";
            this.btnMasterRemoveAll.Click += new System.EventHandler(this.btnMasterRemoveAll_Click);
            // 
            // btnMasterAddAll
            // 
            this.btnMasterAddAll.Location = new System.Drawing.Point(219, 198);
            this.btnMasterAddAll.Name = "btnMasterAddAll";
            this.btnMasterAddAll.Size = new System.Drawing.Size(29, 23);
            this.btnMasterAddAll.TabIndex = 11;
            this.btnMasterAddAll.Text = ">>";
            this.btnMasterAddAll.Click += new System.EventHandler(this.btnMasterAddAll_Click);
            // 
            // btnMasterRemove
            // 
            this.btnMasterRemove.Location = new System.Drawing.Point(219, 155);
            this.btnMasterRemove.Name = "btnMasterRemove";
            this.btnMasterRemove.Size = new System.Drawing.Size(29, 23);
            this.btnMasterRemove.TabIndex = 10;
            this.btnMasterRemove.Text = "<";
            this.btnMasterRemove.Click += new System.EventHandler(this.btnMasterRemove_Click);
            // 
            // lvMasterDesField
            // 
            this.lvMasterDesField.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.lvMasterDesField.Location = new System.Drawing.Point(254, 30);
            this.lvMasterDesField.Name = "lvMasterDesField";
            this.lvMasterDesField.Size = new System.Drawing.Size(210, 312);
            this.lvMasterDesField.TabIndex = 9;
            this.lvMasterDesField.UseCompatibleStateImageBehavior = false;
            this.lvMasterDesField.View = System.Windows.Forms.View.Details;
            this.lvMasterDesField.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvViewSrcField_ColumnClick);
            this.lvMasterDesField.SelectedIndexChanged += new System.EventHandler(this.lvMasterDesField_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Field Name";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Description";
            this.columnHeader6.Width = 100;
            // 
            // btnMasterAll
            // 
            this.btnMasterAll.Location = new System.Drawing.Point(219, 114);
            this.btnMasterAll.Name = "btnMasterAll";
            this.btnMasterAll.Size = new System.Drawing.Size(29, 23);
            this.btnMasterAll.TabIndex = 8;
            this.btnMasterAll.Text = ">";
            this.btnMasterAll.Click += new System.EventHandler(this.btnMasterAll_Click);
            // 
            // lvMasterSrcField
            // 
            this.lvMasterSrcField.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.lvMasterSrcField.Location = new System.Drawing.Point(4, 30);
            this.lvMasterSrcField.Name = "lvMasterSrcField";
            this.lvMasterSrcField.Size = new System.Drawing.Size(210, 312);
            this.lvMasterSrcField.TabIndex = 7;
            this.lvMasterSrcField.UseCompatibleStateImageBehavior = false;
            this.lvMasterSrcField.View = System.Windows.Forms.View.Details;
            this.lvMasterSrcField.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvViewSrcField_ColumnClick);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Field Name";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Description";
            this.columnHeader8.Width = 100;
            // 
            // tpDetailFields
            // 
            this.tpDetailFields.Controls.Add(this.groupBox3);
            this.tpDetailFields.Controls.Add(this.groupBox2);
            this.tpDetailFields.Location = new System.Drawing.Point(4, 22);
            this.tpDetailFields.Name = "tpDetailFields";
            this.tpDetailFields.Padding = new System.Windows.Forms.Padding(3);
            this.tpDetailFields.Size = new System.Drawing.Size(725, 358);
            this.tpDetailFields.TabIndex = 5;
            this.tpDetailFields.Text = "DetailFields";
            this.tpDetailFields.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbCaption_D);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.tbEditMask_D);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.cbQueryMode_D);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.cbCheckNull_D);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.tbDefaultValue_D);
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Controls.Add(this.cbControlType_D);
            this.groupBox3.Controls.Add(this.label30);
            this.groupBox3.Controls.Add(this.gbDetailCombo);
            this.groupBox3.Controls.Add(this.btnDetailDown);
            this.groupBox3.Controls.Add(this.btnDetailUp);
            this.groupBox3.Controls.Add(this.btnDeleteField);
            this.groupBox3.Controls.Add(this.btnNewField);
            this.groupBox3.Controls.Add(this.lvSelectedFields);
            this.groupBox3.Controls.Add(this.tbDetailTableName);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Location = new System.Drawing.Point(179, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(543, 330);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fields";
            // 
            // tbCaption_D
            // 
            this.tbCaption_D.Location = new System.Drawing.Point(388, 18);
            this.tbCaption_D.Name = "tbCaption_D";
            this.tbCaption_D.Size = new System.Drawing.Size(142, 21);
            this.tbCaption_D.TabIndex = 52;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(335, 21);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 12);
            this.label17.TabIndex = 51;
            this.label17.Text = "Caption";
            // 
            // tbEditMask_D
            // 
            this.tbEditMask_D.Location = new System.Drawing.Point(388, 118);
            this.tbEditMask_D.Name = "tbEditMask_D";
            this.tbEditMask_D.Size = new System.Drawing.Size(142, 21);
            this.tbEditMask_D.TabIndex = 50;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(323, 121);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(59, 12);
            this.label26.TabIndex = 49;
            this.label26.Text = "Edit Mask";
            // 
            // cbQueryMode_D
            // 
            this.cbQueryMode_D.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQueryMode_D.FormattingEnabled = true;
            this.cbQueryMode_D.Items.AddRange(new object[] {
            "",
            "None",
            "Normal",
            "Range"});
            this.cbQueryMode_D.Location = new System.Drawing.Point(388, 94);
            this.cbQueryMode_D.Name = "cbQueryMode_D";
            this.cbQueryMode_D.Size = new System.Drawing.Size(142, 20);
            this.cbQueryMode_D.TabIndex = 48;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(317, 97);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 12);
            this.label27.TabIndex = 47;
            this.label27.Text = "Query Mode";
            // 
            // cbCheckNull_D
            // 
            this.cbCheckNull_D.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCheckNull_D.FormattingEnabled = true;
            this.cbCheckNull_D.Items.AddRange(new object[] {
            "",
            "N",
            "Y"});
            this.cbCheckNull_D.Location = new System.Drawing.Point(388, 44);
            this.cbCheckNull_D.Name = "cbCheckNull_D";
            this.cbCheckNull_D.Size = new System.Drawing.Size(142, 20);
            this.cbCheckNull_D.TabIndex = 46;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(317, 47);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(65, 12);
            this.label28.TabIndex = 45;
            this.label28.Text = "Check Null";
            // 
            // tbDefaultValue_D
            // 
            this.tbDefaultValue_D.Location = new System.Drawing.Point(388, 68);
            this.tbDefaultValue_D.Name = "tbDefaultValue_D";
            this.tbDefaultValue_D.Size = new System.Drawing.Size(142, 21);
            this.tbDefaultValue_D.TabIndex = 44;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(299, 71);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(83, 12);
            this.label29.TabIndex = 43;
            this.label29.Text = "Default Value";
            // 
            // cbControlType_D
            // 
            this.cbControlType_D.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbControlType_D.FormattingEnabled = true;
            this.cbControlType_D.Items.AddRange(new object[] {
            "TextBox",
            "ComboBox",
            "RefValBox",
            "ValidateBox",
            "DateTimeBox",
            "CheckBox"});
            this.cbControlType_D.Location = new System.Drawing.Point(388, 167);
            this.cbControlType_D.Name = "cbControlType_D";
            this.cbControlType_D.Size = new System.Drawing.Size(142, 20);
            this.cbControlType_D.TabIndex = 42;
            this.cbControlType_D.SelectedIndexChanged += new System.EventHandler(this.cbControlType_D_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(305, 170);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(77, 12);
            this.label30.TabIndex = 41;
            this.label30.Text = "Control Type";
            // 
            // gbDetailCombo
            // 
            this.gbDetailCombo.Controls.Add(this.tbComboEntityName_D);
            this.gbDetailCombo.Controls.Add(this.tbComboEntitySetName_D);
            this.gbDetailCombo.Controls.Add(this.btnComboRemoteName_D);
            this.gbDetailCombo.Controls.Add(this.tbComboRemoteName_D);
            this.gbDetailCombo.Controls.Add(this.label31);
            this.gbDetailCombo.Controls.Add(this.label32);
            this.gbDetailCombo.Controls.Add(this.label33);
            this.gbDetailCombo.Controls.Add(this.cbComboDisplayField_D);
            this.gbDetailCombo.Controls.Add(this.label34);
            this.gbDetailCombo.Controls.Add(this.cbComboValueField_D);
            this.gbDetailCombo.Enabled = false;
            this.gbDetailCombo.Location = new System.Drawing.Point(295, 193);
            this.gbDetailCombo.Name = "gbDetailCombo";
            this.gbDetailCombo.Size = new System.Drawing.Size(241, 131);
            this.gbDetailCombo.TabIndex = 40;
            this.gbDetailCombo.TabStop = false;
            this.gbDetailCombo.Text = "ComboBox Setting";
            // 
            // btnComboRemoteName_D
            // 
            this.btnComboRemoteName_D.Location = new System.Drawing.Point(213, 18);
            this.btnComboRemoteName_D.Name = "btnComboRemoteName_D";
            this.btnComboRemoteName_D.Size = new System.Drawing.Size(22, 21);
            this.btnComboRemoteName_D.TabIndex = 42;
            this.btnComboRemoteName_D.Text = "...";
            this.btnComboRemoteName_D.UseVisualStyleBackColor = true;
            this.btnComboRemoteName_D.Click += new System.EventHandler(this.btnComboRemoteName_D_Click);
            // 
            // tbComboRemoteName_D
            // 
            this.tbComboRemoteName_D.Location = new System.Drawing.Point(92, 18);
            this.tbComboRemoteName_D.Name = "tbComboRemoteName_D";
            this.tbComboRemoteName_D.Size = new System.Drawing.Size(115, 21);
            this.tbComboRemoteName_D.TabIndex = 41;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(22, 21);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(65, 12);
            this.label31.TabIndex = 40;
            this.label31.Text = "RemoteName";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(22, 47);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(71, 12);
            this.label32.TabIndex = 9;
            this.label32.Text = "Entity Name";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(4, 76);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(83, 12);
            this.label33.TabIndex = 11;
            this.label33.Text = "Display Field";
            // 
            // cbComboDisplayField_D
            // 
            this.cbComboDisplayField_D.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComboDisplayField_D.FormattingEnabled = true;
            this.cbComboDisplayField_D.Location = new System.Drawing.Point(93, 73);
            this.cbComboDisplayField_D.Name = "cbComboDisplayField_D";
            this.cbComboDisplayField_D.Size = new System.Drawing.Size(142, 20);
            this.cbComboDisplayField_D.TabIndex = 12;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(16, 105);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(71, 12);
            this.label34.TabIndex = 13;
            this.label34.Text = "Value Field";
            // 
            // cbComboValueField_D
            // 
            this.cbComboValueField_D.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComboValueField_D.FormattingEnabled = true;
            this.cbComboValueField_D.Location = new System.Drawing.Point(93, 102);
            this.cbComboValueField_D.Name = "cbComboValueField_D";
            this.cbComboValueField_D.Size = new System.Drawing.Size(142, 20);
            this.cbComboValueField_D.TabIndex = 14;
            // 
            // btnDetailDown
            // 
            this.btnDetailDown.Location = new System.Drawing.Point(242, 72);
            this.btnDetailDown.Name = "btnDetailDown";
            this.btnDetailDown.Size = new System.Drawing.Size(29, 23);
            this.btnDetailDown.TabIndex = 23;
            this.btnDetailDown.Text = "↓";
            this.btnDetailDown.UseVisualStyleBackColor = true;
            this.btnDetailDown.Click += new System.EventHandler(this.btnDetailDown_Click);
            // 
            // btnDetailUp
            // 
            this.btnDetailUp.Location = new System.Drawing.Point(242, 43);
            this.btnDetailUp.Name = "btnDetailUp";
            this.btnDetailUp.Size = new System.Drawing.Size(29, 23);
            this.btnDetailUp.TabIndex = 22;
            this.btnDetailUp.Text = "↑";
            this.btnDetailUp.UseVisualStyleBackColor = true;
            this.btnDetailUp.Click += new System.EventHandler(this.btnDetailUp_Click);
            // 
            // btnDeleteField
            // 
            this.btnDeleteField.Location = new System.Drawing.Point(242, 301);
            this.btnDeleteField.Name = "btnDeleteField";
            this.btnDeleteField.Size = new System.Drawing.Size(34, 23);
            this.btnDeleteField.TabIndex = 10;
            this.btnDeleteField.Text = "Delete";
            this.btnDeleteField.UseVisualStyleBackColor = true;
            this.btnDeleteField.Click += new System.EventHandler(this.btnDeleteField_Click);
            // 
            // btnNewField
            // 
            this.btnNewField.Location = new System.Drawing.Point(242, 272);
            this.btnNewField.Name = "btnNewField";
            this.btnNewField.Size = new System.Drawing.Size(34, 23);
            this.btnNewField.TabIndex = 9;
            this.btnNewField.Text = "Add";
            this.btnNewField.UseVisualStyleBackColor = true;
            this.btnNewField.Click += new System.EventHandler(this.btnNewField_Click);
            // 
            // lvSelectedFields
            // 
            this.lvSelectedFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10});
            this.lvSelectedFields.Location = new System.Drawing.Point(6, 43);
            this.lvSelectedFields.Name = "lvSelectedFields";
            this.lvSelectedFields.Size = new System.Drawing.Size(230, 281);
            this.lvSelectedFields.TabIndex = 8;
            this.lvSelectedFields.UseCompatibleStateImageBehavior = false;
            this.lvSelectedFields.View = System.Windows.Forms.View.Details;
            this.lvSelectedFields.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvViewSrcField_ColumnClick);
            this.lvSelectedFields.SelectedIndexChanged += new System.EventHandler(this.lvSelectedFields_SelectedIndexChanged);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Field Name";
            this.columnHeader9.Width = 100;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Description";
            this.columnHeader10.Width = 110;
            // 
            // tbDetailTableName
            // 
            this.tbDetailTableName.Location = new System.Drawing.Point(86, 15);
            this.tbDetailTableName.Name = "tbDetailTableName";
            this.tbDetailTableName.Size = new System.Drawing.Size(190, 21);
            this.tbDetailTableName.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 8F);
            this.label14.Location = new System.Drawing.Point(16, 18);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 14);
            this.label14.TabIndex = 0;
            this.label14.Text = "Table Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tvRelation);
            this.groupBox2.Location = new System.Drawing.Point(8, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(165, 330);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tables";
            // 
            // tvRelation
            // 
            this.tvRelation.HideSelection = false;
            this.tvRelation.Location = new System.Drawing.Point(6, 21);
            this.tvRelation.Name = "tvRelation";
            this.tvRelation.Size = new System.Drawing.Size(153, 303);
            this.tvRelation.TabIndex = 1;
            this.tvRelation.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvRelation_AfterSelect);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(572, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(158, 400);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
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
            this.btnPrevious.Location = new System.Drawing.Point(56, 400);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 14;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(480, 400);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 15;
            this.btnDone.Text = "Done";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "*.sln";
            this.openFileDialog.Filter = "Solution Files (*.sln)|*.sln";
            // 
            // tbComboEntitySetName_D
            // 
            this.tbComboEntitySetName_D.Location = new System.Drawing.Point(93, 44);
            this.tbComboEntitySetName_D.Name = "tbComboEntitySetName_D";
            this.tbComboEntitySetName_D.Size = new System.Drawing.Size(142, 21);
            this.tbComboEntitySetName_D.TabIndex = 53;
            // 
            // tbComboEntityName_D
            // 
            this.tbComboEntityName_D.Location = new System.Drawing.Point(92, 44);
            this.tbComboEntityName_D.Name = "tbComboEntityName_D";
            this.tbComboEntityName_D.Size = new System.Drawing.Size(142, 21);
            this.tbComboEntityName_D.TabIndex = 53;
            this.tbComboEntityName_D.TextChanged += new System.EventHandler(this.tbComboEntityName_D_TextChanged);
            // 
            // fmEEPWCFWebWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 435);
            this.ControlBox = false;
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnNext);
            this.Name = "fmEEPWCFWebWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EEP Ext Web Form Wizard";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.tbConnection.ResumeLayout(false);
            this.tbConnection.PerformLayout();
            this.tpOutputSetting.ResumeLayout(false);
            this.tpOutputSetting.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tpFormSetting.ResumeLayout(false);
            this.tpFormSetting.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpDataSource.ResumeLayout(false);
            this.tpDataSource.PerformLayout();
            this.tpViewFields.ResumeLayout(false);
            this.tpViewFields.PerformLayout();
            this.tpMasterFields.ResumeLayout(false);
            this.tpMasterFields.PerformLayout();
            this.gbComboBox.ResumeLayout(false);
            this.gbComboBox.PerformLayout();
            this.tpDetailFields.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbDetailCombo.ResumeLayout(false);
            this.gbDetailCombo.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tpOutputSetting;
		private System.Windows.Forms.TabPage tpFormSetting;
		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox tbFormName;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tpDataSource;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbCommandName;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TabPage tpMasterFields;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btnMasterRemoveAll;
		private System.Windows.Forms.Button btnMasterAddAll;
		private System.Windows.Forms.Button btnMasterRemove;
		private System.Windows.Forms.ListView lvMasterDesField;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button btnMasterAll;
		private System.Windows.Forms.ListView lvMasterSrcField;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.TabPage tpDetailFields;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvSelectedFields;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.TextBox tbDetailTableName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TreeView tvRelation;
        private System.Windows.Forms.Button btnDeleteField;
        private System.Windows.Forms.Button btnNewField;
        private System.Windows.Forms.RadioButton rbAddToRootFolder;
        private System.Windows.Forms.TextBox tbAddToNewFolder;
        private System.Windows.Forms.RadioButton rbAddToNewFolder;
        private System.Windows.Forms.ComboBox cbAddToExistFolder;
        private System.Windows.Forms.RadioButton rbAddToExistFolder;
        private System.Windows.Forms.Button btnSolutionName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSolutionName;
        private System.Windows.Forms.ComboBox cbWebSite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRemoteName;
        private System.Windows.Forms.Button btnRemoteName;
        private System.Windows.Forms.RadioButton rbAddToExistSolution;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbCurrentSolution;
        private System.Windows.Forms.RadioButton rbCurrentSolution;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tpViewFields;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnViewRemoveAll;
        private System.Windows.Forms.Button btnViewAddAll;
        private System.Windows.Forms.Button btnViewRemove;
        private System.Windows.Forms.ListView lvViewDesField;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnViewAdd;
        private System.Windows.Forms.ListView lvViewSrcField;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox tbFormTitle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbEntityName;
        private System.Windows.Forms.Button btnViewDown;
        private System.Windows.Forms.Button btnViewUp;
        private System.Windows.Forms.Button btnMasterDown;
        private System.Windows.Forms.Button btnMasterUp;
        private System.Windows.Forms.Button btnDetailDown;
        private System.Windows.Forms.Button btnDetailUp;
        private System.Windows.Forms.GroupBox gbComboBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbDataTextField;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cbDataValueField;
        private System.Windows.Forms.ComboBox cbControlType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbEditMask;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbQueryMode;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbCheckNull;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tbDefaultValue;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tbCaption;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ListView lvTemplate;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ImageList ilTemplate;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnComboRemoteName;
        private System.Windows.Forms.TextBox tbComboRemoteName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbCaption_D;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbEditMask_D;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cbQueryMode_D;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cbCheckNull_D;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox tbDefaultValue_D;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cbControlType_D;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.GroupBox gbDetailCombo;
        private System.Windows.Forms.Button btnComboRemoteName_D;
        private System.Windows.Forms.TextBox tbComboRemoteName_D;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox cbComboDisplayField_D;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox cbComboValueField_D;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ComboBox cbDetailEntityName;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.TabPage tbConnection;
        private System.Windows.Forms.ComboBox cbChooseLanguage;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbEEPAlias;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cbDatabaseType;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox tbEntitySetName;
        private System.Windows.Forms.TextBox tbComboEntityName;
        private System.Windows.Forms.TextBox tbComboEntitySetName;
        private System.Windows.Forms.TextBox tbComboEntityName_D;
        private System.Windows.Forms.TextBox tbComboEntitySetName_D;


	}
}