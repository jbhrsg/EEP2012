namespace MWizard
{
    partial class fmWCFServerWzd
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
            this.btnDone = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tpTables = new System.Windows.Forms.TabPage();
            this.cbGenerateEntity = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeleteField = new System.Windows.Forms.Button();
            this.btnNewField = new System.Windows.Forms.Button();
            this.lvSelectedFields = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteDataset = new System.Windows.Forms.Button();
            this.btnNewDataset = new System.Windows.Forms.Button();
            this.btnNewSubDataset = new System.Windows.Forms.Button();
            this.tvTables = new System.Windows.Forms.TreeView();
            this.tpOutputSetting = new System.Windows.Forms.TabPage();
            this.cbChooseLanguage = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAssemblyOutputPath = new System.Windows.Forms.Button();
            this.tbAssemblyOutputPath = new System.Windows.Forms.TextBox();
            this.tbOutputPath = new System.Windows.Forms.TextBox();
            this.tbCurrentSolution = new System.Windows.Forms.TextBox();
            this.tbNewLocation = new System.Windows.Forms.TextBox();
            this.tbSolutionName = new System.Windows.Forms.TextBox();
            this.tbNewSolutionName = new System.Windows.Forms.TextBox();
            this.tbPackageName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbAddToCurrent = new System.Windows.Forms.RadioButton();
            this.btnNewLocation = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSolutionName = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rbAddToExistSln = new System.Windows.Forms.RadioButton();
            this.rbNewSolution = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpTables.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpOutputSetting.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(481, 372);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 26);
            this.btnDone.TabIndex = 20;
            this.btnDone.Text = "Done";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(33, 372);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 26);
            this.btnPrevious.TabIndex = 19;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(568, 372);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(126, 372);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 26);
            this.btnNext.TabIndex = 17;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.SelectedPath = "folderBrowserDialog1";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "*.sln";
            this.openFileDialog.Filter = "Solution Files (*.sln)|*.sln";
            // 
            // tpTables
            // 
            this.tpTables.Controls.Add(this.cbGenerateEntity);
            this.tpTables.Controls.Add(this.label9);
            this.tpTables.Controls.Add(this.groupBox2);
            this.tpTables.Controls.Add(this.groupBox1);
            this.tpTables.Location = new System.Drawing.Point(4, 22);
            this.tpTables.Name = "tpTables";
            this.tpTables.Padding = new System.Windows.Forms.Padding(3);
            this.tpTables.Size = new System.Drawing.Size(664, 327);
            this.tpTables.TabIndex = 2;
            this.tpTables.Text = "Select Tables";
            this.tpTables.UseVisualStyleBackColor = true;
            // 
            // cbGenerateEntity
            // 
            this.cbGenerateEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGenerateEntity.FormattingEnabled = true;
            this.cbGenerateEntity.Items.AddRange(new object[] {
            "Master + View",
            "Master",
            "View Only"});
            this.cbGenerateEntity.Location = new System.Drawing.Point(397, 10);
            this.cbGenerateEntity.Name = "cbGenerateEntity";
            this.cbGenerateEntity.Size = new System.Drawing.Size(259, 20);
            this.cbGenerateEntity.TabIndex = 3;
            this.cbGenerateEntity.SelectedIndexChanged += new System.EventHandler(this.cbGenerateEntity_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(287, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "Generate Entity";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDeleteField);
            this.groupBox2.Controls.Add(this.btnNewField);
            this.groupBox2.Controls.Add(this.lvSelectedFields);
            this.groupBox2.Location = new System.Drawing.Point(283, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 283);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fields";
            // 
            // btnDeleteField
            // 
            this.btnDeleteField.Location = new System.Drawing.Point(292, 254);
            this.btnDeleteField.Name = "btnDeleteField";
            this.btnDeleteField.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteField.TabIndex = 5;
            this.btnDeleteField.Text = "Delete";
            this.btnDeleteField.Click += new System.EventHandler(this.btnDeleteField_Click);
            // 
            // btnNewField
            // 
            this.btnNewField.Location = new System.Drawing.Point(292, 223);
            this.btnNewField.Name = "btnNewField";
            this.btnNewField.Size = new System.Drawing.Size(75, 23);
            this.btnNewField.TabIndex = 4;
            this.btnNewField.Text = "Add";
            this.btnNewField.Click += new System.EventHandler(this.btnAddField_Click);
            // 
            // lvSelectedFields
            // 
            this.lvSelectedFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvSelectedFields.HideSelection = false;
            this.lvSelectedFields.Location = new System.Drawing.Point(6, 21);
            this.lvSelectedFields.Name = "lvSelectedFields";
            this.lvSelectedFields.Size = new System.Drawing.Size(280, 256);
            this.lvSelectedFields.TabIndex = 0;
            this.lvSelectedFields.UseCompatibleStateImageBehavior = false;
            this.lvSelectedFields.View = System.Windows.Forms.View.Details;
            this.lvSelectedFields.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvSelectedFields_ColumnClick);
            this.lvSelectedFields.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvSelectedFields_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Field Name";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 120;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeleteDataset);
            this.groupBox1.Controls.Add(this.btnNewDataset);
            this.groupBox1.Controls.Add(this.btnNewSubDataset);
            this.groupBox1.Controls.Add(this.tvTables);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 313);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tables";
            // 
            // btnDeleteDataset
            // 
            this.btnDeleteDataset.Location = new System.Drawing.Point(212, 284);
            this.btnDeleteDataset.Name = "btnDeleteDataset";
            this.btnDeleteDataset.Size = new System.Drawing.Size(53, 23);
            this.btnDeleteDataset.TabIndex = 3;
            this.btnDeleteDataset.Text = "Delete";
            this.btnDeleteDataset.Click += new System.EventHandler(this.btnDeleteDataset_Click);
            // 
            // btnNewDataset
            // 
            this.btnNewDataset.Location = new System.Drawing.Point(6, 284);
            this.btnNewDataset.Name = "btnNewDataset";
            this.btnNewDataset.Size = new System.Drawing.Size(53, 23);
            this.btnNewDataset.TabIndex = 2;
            this.btnNewDataset.Text = "Add";
            this.btnNewDataset.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnNewSubDataset
            // 
            this.btnNewSubDataset.Location = new System.Drawing.Point(74, 284);
            this.btnNewSubDataset.Name = "btnNewSubDataset";
            this.btnNewSubDataset.Size = new System.Drawing.Size(79, 23);
            this.btnNewSubDataset.TabIndex = 1;
            this.btnNewSubDataset.Text = "Add Child";
            this.btnNewSubDataset.Click += new System.EventHandler(this.btnAddNext_Click);
            // 
            // tvTables
            // 
            this.tvTables.HideSelection = false;
            this.tvTables.Location = new System.Drawing.Point(6, 21);
            this.tvTables.Name = "tvTables";
            this.tvTables.Size = new System.Drawing.Size(259, 255);
            this.tvTables.TabIndex = 0;
            this.tvTables.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvTables_BeforeSelect);
            this.tvTables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTables_AfterSelect);
            // 
            // tpOutputSetting
            // 
            this.tpOutputSetting.AutoScroll = true;
            this.tpOutputSetting.Controls.Add(this.cbChooseLanguage);
            this.tpOutputSetting.Controls.Add(this.label8);
            this.tpOutputSetting.Controls.Add(this.btnAssemblyOutputPath);
            this.tpOutputSetting.Controls.Add(this.tbAssemblyOutputPath);
            this.tpOutputSetting.Controls.Add(this.tbOutputPath);
            this.tpOutputSetting.Controls.Add(this.tbCurrentSolution);
            this.tpOutputSetting.Controls.Add(this.tbNewLocation);
            this.tpOutputSetting.Controls.Add(this.tbSolutionName);
            this.tpOutputSetting.Controls.Add(this.tbNewSolutionName);
            this.tpOutputSetting.Controls.Add(this.tbPackageName);
            this.tpOutputSetting.Controls.Add(this.label7);
            this.tpOutputSetting.Controls.Add(this.btnOutputPath);
            this.tpOutputSetting.Controls.Add(this.label6);
            this.tpOutputSetting.Controls.Add(this.label1);
            this.tpOutputSetting.Controls.Add(this.rbAddToCurrent);
            this.tpOutputSetting.Controls.Add(this.btnNewLocation);
            this.tpOutputSetting.Controls.Add(this.label5);
            this.tpOutputSetting.Controls.Add(this.btnSolutionName);
            this.tpOutputSetting.Controls.Add(this.label4);
            this.tpOutputSetting.Controls.Add(this.label3);
            this.tpOutputSetting.Controls.Add(this.rbAddToExistSln);
            this.tpOutputSetting.Controls.Add(this.rbNewSolution);
            this.tpOutputSetting.Controls.Add(this.label2);
            this.tpOutputSetting.Location = new System.Drawing.Point(4, 22);
            this.tpOutputSetting.Name = "tpOutputSetting";
            this.tpOutputSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpOutputSetting.Size = new System.Drawing.Size(664, 327);
            this.tpOutputSetting.TabIndex = 0;
            this.tpOutputSetting.Text = "Output Setting";
            this.tpOutputSetting.UseVisualStyleBackColor = true;
            // 
            // cbChooseLanguage
            // 
            this.cbChooseLanguage.FormattingEnabled = true;
            this.cbChooseLanguage.Items.AddRange(new object[] {
            "C#",
            "VB"});
            this.cbChooseLanguage.Location = new System.Drawing.Point(149, 284);
            this.cbChooseLanguage.Name = "cbChooseLanguage";
            this.cbChooseLanguage.Size = new System.Drawing.Size(184, 20);
            this.cbChooseLanguage.TabIndex = 25;
            this.cbChooseLanguage.Text = "C#";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 8F);
            this.label8.Location = new System.Drawing.Point(48, 286);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 14);
            this.label8.TabIndex = 24;
            this.label8.Text = "Choose Language";
            // 
            // btnAssemblyOutputPath
            // 
            this.btnAssemblyOutputPath.Location = new System.Drawing.Point(586, 253);
            this.btnAssemblyOutputPath.Name = "btnAssemblyOutputPath";
            this.btnAssemblyOutputPath.Size = new System.Drawing.Size(27, 23);
            this.btnAssemblyOutputPath.TabIndex = 23;
            this.btnAssemblyOutputPath.Text = "...";
            this.btnAssemblyOutputPath.Click += new System.EventHandler(this.btnAssemblyOutputPath_Click);
            // 
            // tbAssemblyOutputPath
            // 
            this.tbAssemblyOutputPath.Location = new System.Drawing.Point(149, 254);
            this.tbAssemblyOutputPath.Name = "tbAssemblyOutputPath";
            this.tbAssemblyOutputPath.Size = new System.Drawing.Size(431, 21);
            this.tbAssemblyOutputPath.TabIndex = 22;
            // 
            // tbOutputPath
            // 
            this.tbOutputPath.Location = new System.Drawing.Point(149, 226);
            this.tbOutputPath.Name = "tbOutputPath";
            this.tbOutputPath.Size = new System.Drawing.Size(431, 21);
            this.tbOutputPath.TabIndex = 19;
            // 
            // tbCurrentSolution
            // 
            this.tbCurrentSolution.Enabled = false;
            this.tbCurrentSolution.Location = new System.Drawing.Point(187, 40);
            this.tbCurrentSolution.Name = "tbCurrentSolution";
            this.tbCurrentSolution.ReadOnly = true;
            this.tbCurrentSolution.Size = new System.Drawing.Size(393, 21);
            this.tbCurrentSolution.TabIndex = 16;
            this.tbCurrentSolution.TextChanged += new System.EventHandler(this.tbCurrentSolution_TextChanged);
            // 
            // tbNewLocation
            // 
            this.tbNewLocation.Location = new System.Drawing.Point(186, 170);
            this.tbNewLocation.Name = "tbNewLocation";
            this.tbNewLocation.Size = new System.Drawing.Size(393, 21);
            this.tbNewLocation.TabIndex = 12;
            this.tbNewLocation.Text = "D:\\VS2005\\SrvTestSolution";
            this.tbNewLocation.Visible = false;
            this.tbNewLocation.TextChanged += new System.EventHandler(this.tbCurrentSolution_TextChanged);
            // 
            // tbSolutionName
            // 
            this.tbSolutionName.Enabled = false;
            this.tbSolutionName.Location = new System.Drawing.Point(187, 100);
            this.tbSolutionName.Name = "tbSolutionName";
            this.tbSolutionName.Size = new System.Drawing.Size(393, 21);
            this.tbSolutionName.TabIndex = 9;
            this.tbSolutionName.Text = "D:\\VS2005\\SrvTestSolution\\SrvTestSolution.sln";
            this.tbSolutionName.TextChanged += new System.EventHandler(this.tbCurrentSolution_TextChanged);
            // 
            // tbNewSolutionName
            // 
            this.tbNewSolutionName.Location = new System.Drawing.Point(186, 143);
            this.tbNewSolutionName.Name = "tbNewSolutionName";
            this.tbNewSolutionName.Size = new System.Drawing.Size(393, 21);
            this.tbNewSolutionName.TabIndex = 7;
            this.tbNewSolutionName.Text = "SrvTestSolution";
            this.tbNewSolutionName.Visible = false;
            this.tbNewSolutionName.TextChanged += new System.EventHandler(this.tbNewSolutionName_TextChanged);
            // 
            // tbPackageName
            // 
            this.tbPackageName.Location = new System.Drawing.Point(149, 198);
            this.tbPackageName.Name = "tbPackageName";
            this.tbPackageName.Size = new System.Drawing.Size(309, 21);
            this.tbPackageName.TabIndex = 4;
            this.tbPackageName.Text = "SrvTestSolution";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 8F);
            this.label7.Location = new System.Drawing.Point(26, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 14);
            this.label7.TabIndex = 21;
            this.label7.Text = "Assembly Output Path:";
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Location = new System.Drawing.Point(586, 225);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(27, 23);
            this.btnOutputPath.TabIndex = 20;
            this.btnOutputPath.Text = "...";
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputPath_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 8F);
            this.label6.Location = new System.Drawing.Point(49, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 14);
            this.label6.TabIndex = 18;
            this.label6.Text = "Code Output Path:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8F);
            this.label1.Location = new System.Drawing.Point(104, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "Solution Name:";
            // 
            // rbAddToCurrent
            // 
            this.rbAddToCurrent.AutoSize = true;
            this.rbAddToCurrent.Checked = true;
            this.rbAddToCurrent.Enabled = false;
            this.rbAddToCurrent.Font = new System.Drawing.Font("Arial", 8F);
            this.rbAddToCurrent.Location = new System.Drawing.Point(69, 18);
            this.rbAddToCurrent.Name = "rbAddToCurrent";
            this.rbAddToCurrent.Size = new System.Drawing.Size(139, 18);
            this.rbAddToCurrent.TabIndex = 15;
            this.rbAddToCurrent.TabStop = true;
            this.rbAddToCurrent.Text = "Add To Current Solution";
            this.rbAddToCurrent.UseVisualStyleBackColor = true;
            this.rbAddToCurrent.Click += new System.EventHandler(this.rbAddToCurrent_Click);
            // 
            // btnNewLocation
            // 
            this.btnNewLocation.Location = new System.Drawing.Point(585, 170);
            this.btnNewLocation.Name = "btnNewLocation";
            this.btnNewLocation.Size = new System.Drawing.Size(27, 23);
            this.btnNewLocation.TabIndex = 14;
            this.btnNewLocation.Text = "...";
            this.btnNewLocation.Visible = false;
            this.btnNewLocation.Click += new System.EventHandler(this.btnNewSolution_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 8F);
            this.label5.Location = new System.Drawing.Point(133, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 14);
            this.label5.TabIndex = 13;
            this.label5.Text = "Location:";
            this.label5.Visible = false;
            // 
            // btnSolutionName
            // 
            this.btnSolutionName.Enabled = false;
            this.btnSolutionName.Location = new System.Drawing.Point(585, 99);
            this.btnSolutionName.Name = "btnSolutionName";
            this.btnSolutionName.Size = new System.Drawing.Size(27, 23);
            this.btnSolutionName.TabIndex = 11;
            this.btnSolutionName.Text = "...";
            this.btnSolutionName.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 8F);
            this.label4.Location = new System.Drawing.Point(104, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "Solution Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 8F);
            this.label3.Location = new System.Drawing.Point(105, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 14);
            this.label3.TabIndex = 8;
            this.label3.Text = "Solution Name:";
            this.label3.Visible = false;
            // 
            // rbAddToExistSln
            // 
            this.rbAddToExistSln.AutoSize = true;
            this.rbAddToExistSln.Font = new System.Drawing.Font("Arial", 8F);
            this.rbAddToExistSln.Location = new System.Drawing.Point(69, 78);
            this.rbAddToExistSln.Name = "rbAddToExistSln";
            this.rbAddToExistSln.Size = new System.Drawing.Size(126, 18);
            this.rbAddToExistSln.TabIndex = 6;
            this.rbAddToExistSln.Text = "Add To Exist Solution";
            this.rbAddToExistSln.Click += new System.EventHandler(this.rbAddToExistSln_Click);
            // 
            // rbNewSolution
            // 
            this.rbNewSolution.AutoSize = true;
            this.rbNewSolution.Font = new System.Drawing.Font("Arial", 8F);
            this.rbNewSolution.Location = new System.Drawing.Point(68, 121);
            this.rbNewSolution.Name = "rbNewSolution";
            this.rbNewSolution.Size = new System.Drawing.Size(124, 18);
            this.rbNewSolution.TabIndex = 5;
            this.rbNewSolution.Text = "Create New Solution";
            this.rbNewSolution.Visible = false;
            this.rbNewSolution.Click += new System.EventHandler(this.rbNewSolution_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8F);
            this.label2.Location = new System.Drawing.Point(62, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Package Name:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpOutputSetting);
            this.tabControl.Controls.Add(this.tpTables);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.RightToLeftLayout = true;
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(672, 353);
            this.tabControl.TabIndex = 9;
            // 
            // fmWCFServerWzd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(672, 429);
            this.ControlBox = false;
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.tabControl);
            this.Name = "fmWCFServerWzd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EEP Server Package Wizard";
            this.Load += new System.EventHandler(this.fmServerWzd_Load);
            this.tpTables.ResumeLayout(false);
            this.tpTables.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tpOutputSetting.ResumeLayout(false);
            this.tpOutputSetting.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Button btnDone;
		private System.Windows.Forms.Button btnPrevious;
		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabPage tpTables;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeleteField;
        private System.Windows.Forms.Button btnNewField;
        private System.Windows.Forms.ListView lvSelectedFields;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeleteDataset;
        private System.Windows.Forms.Button btnNewDataset;
        private System.Windows.Forms.Button btnNewSubDataset;
        private System.Windows.Forms.TreeView tvTables;
        private System.Windows.Forms.TabPage tpOutputSetting;
        private System.Windows.Forms.Button btnAssemblyOutputPath;
        private System.Windows.Forms.TextBox tbAssemblyOutputPath;
        private System.Windows.Forms.TextBox tbOutputPath;
        private System.Windows.Forms.TextBox tbCurrentSolution;
        private System.Windows.Forms.TextBox tbNewLocation;
        private System.Windows.Forms.TextBox tbSolutionName;
        private System.Windows.Forms.TextBox tbNewSolutionName;
        private System.Windows.Forms.TextBox tbPackageName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnOutputPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbAddToCurrent;
        private System.Windows.Forms.Button btnNewLocation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSolutionName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbAddToExistSln;
        private System.Windows.Forms.RadioButton rbNewSolution;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ComboBox cbChooseLanguage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbGenerateEntity;
        private System.Windows.Forms.Label label9;
	}
}