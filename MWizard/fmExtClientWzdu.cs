using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using System.Collections;
using System.Data.SqlClient;
using System.ComponentModel.Design;
using System.IO;
using Srvtools;
using Microsoft.Win32;
using System.Data.Common;
using System.Reflection;
using System.Xml;
using System.Windows.Forms.Design;
using System.Threading;

namespace MWizard
{
	public partial class fmExtClientWizard : Form
	{
		private TExtClientData FClientData;
		private DTE2 FDTE2;
		private AddIn FAddIn;
        private DbConnection InternalConnection = null;
        private TStringList FAlias;
        private static string _serverPath;
        private InfoForm FTemplateForm;
        private Container FContainer = null;
        private ListViewColumnSorter lvwColumnSorterDataSet;
        private ListViewColumnSorter lvwColumnSorterBindingSource;
        private ListViewColumnSorter lvwColumnSorterDataSetFields;

		public fmExtClientWizard()
		{
			InitializeComponent();
            FClientData = new TExtClientData(this);
            //PrepareWizardService();

            lvwColumnSorterDataSet = new ListViewColumnSorter();
            lvwColumnSorterBindingSource = new ListViewColumnSorter();
            lvwColumnSorterDataSetFields = new ListViewColumnSorter();
            this.lvDataSet.ListViewItemSorter = lvwColumnSorterDataSet;
            this.lvBindingSource.ListViewItemSorter = lvwColumnSorterBindingSource;
            this.lvSelectedFields.ListViewItemSorter = lvwColumnSorterDataSetFields;
        }

        public fmExtClientWizard(DTE2 aDTE2, AddIn aAddIn)
        {
            InitializeComponent();
            FClientData = new TExtClientData(this);
            FDTE2 = aDTE2;
            FAddIn = aAddIn;
            //PrepareWizardService();

            lvwColumnSorterDataSet = new ListViewColumnSorter();
            lvwColumnSorterBindingSource = new ListViewColumnSorter();
            lvwColumnSorterDataSetFields = new ListViewColumnSorter();
            this.lvDataSet.ListViewItemSorter = lvwColumnSorterDataSet;
            this.lvBindingSource.ListViewItemSorter = lvwColumnSorterBindingSource;
            this.lvSelectedFields.ListViewItemSorter = lvwColumnSorterDataSetFields;
        }

        private void PrepareWizardService()
        {
            Show();
            Hide();
        }

        private void ClearValues()
        {
            tbCurrentSolution.Text = "";
            tbNewLocation.Text = "";
            tbNewSolutionName.Text = "";
            tbPackageName.Text = "ClientPackage";
            tbSolutionName.Text = "";
            cbBaseForm.Text = "CSingle";
            tbFormName.Text = "Form1";
            tbAssemblyOutputPath.Text = "";
            cbColumnCount.SelectedIndex = 0;
            rbAddToExistSln_Click(rbAddToExistSln, null);
            tvContainer.Nodes.Clear();
            rbEEPBaseForm.Checked = false;
            rbEEPBaseForm.Checked = true;
            cbBindingSource.Enabled = false;
            cbViewBindingSource.Enabled = false;
            btnAddField.Enabled = false;
            btnDeleteField.Enabled = false;
            FTemplateForm = null;
            ClearFormData();
        }

        private void ClearFormData()
        {
            cbBindingSource.Items.Clear();
            cbBindingSource.Items.Add("");
            cbViewBindingSource.Items.Clear();
            cbViewBindingSource.Items.Add("");
            ClearRefValButton(lvSelectedFields);
            ClearRefValButton(lvDataSet);
            ClearRefValButton(lvBindingSource);
            lvSelectedFields.Items.Clear();
            lvDataSet.Items.Clear();
            lvBindingSource.Items.Clear();
            tvContainer.Nodes.Clear();

            foreach (InfoDataSet aDataSet in FClientData.DataSetList)
                aDataSet.Dispose();
            FClientData.DataSetList.Clear();
            foreach (InfoBindingSource aBindingSource in FClientData.BindingSourceList)
                aBindingSource.Dispose();
            FClientData.BindingSourceList.Clear();
            foreach (Component C in FContainer.Components)
                C.Dispose();
            FClientData.Blocks.Clear();
        }

        private void Init()
        {
            FContainer = new Container();
            ClearValues();
            LoadDBString();
            if (((FDTE2 != null) && (FDTE2.Solution.FileName != "")) && File.Exists(FDTE2.Solution.FileName))
            {
                rbAddToCurrent.Enabled = true;
                rbAddToCurrent.Checked = true;
                tbCurrentSolution.Text = FDTE2.Solution.FileName;
                EnabledOutputControls();
            }
            try
            {

                cbEEPAlias.Text = EEPRegistry.WizardConnectionString;
                cbDatabaseType.Text = EEPRegistry.DataBaseType;
            }
            catch { }
            DisplayPage(tpConnection);
        }

        public DbConnection GlobalConnection
        {
            get { return InternalConnection; }
        }

        public String SelectedAlias
        {
            get { return cbEEPAlias.Text; }
        }

        public void SDGenClientModule(string XML)
        {
            if (XML != "")
            {
                FClientData.Blocks.Clear();
                FClientData.LoadFromXML(XML);
            }
            TExtClientGenerator CG = new TExtClientGenerator(FClientData, FDTE2, FAddIn);
            CG.GenClientModule();
        }

        private void DisplayPage(TabPage aPage)
        {
            tabControl1.TabPages.Clear();
            tabControl1.TabPages.Add(aPage);
            tabControl1.SelectedTab = aPage;
            EnableButton();
        }

        private void EnableButton()
        {
            btnPrevious.Enabled = tabControl1.SelectedTab != tpConnection;
            btnNext.Enabled = tabControl1.SelectedTab != tpLayout;
            btnDone.Enabled = tabControl1.SelectedTab == tpLayout;
            btnCancel.Enabled = true;
        }

        private void LoadDBString()
        {
            cbEEPAlias.Items.Clear();
            FAlias = new TStringList();
            List<string> list1 = new List<string>();
            string text3 = SystemFile.DBFile;
            XmlDocument document1 = new XmlDocument();
            document1.Load(text3);
            foreach (XmlNode node1 in document1.FirstChild.FirstChild.ChildNodes)
            {
                list1.Add((string)node1.Name);
                cbEEPAlias.Items.Add(node1.Name);
                string text1 = node1.Attributes["String"].Value.Trim();
                string text2 = WzdUtils.GetPwdString(node1.Attributes["Password"].Value.Trim());
                if ((text1.Length > 0) && (text2.Length > 0) && text2 != String.Empty)
                {
                    if (text1[text1.Length - 1] != ';')
                    {
                        text1 = text1 + ";Password=" + text2;
                    }
                    else
                    {
                        text1 = text1 + "Password=" + text2;
                    }
                }
                FAlias.AddObject(node1.Name, text1);
            }
        }

		private void Form1_Load(object sender, EventArgs e)
		{
			// TODO: This line of code loads data into the 'flow7_testDataSet.COLDEF' table. You can move, or remove it, as needed.
            //LoadDBString();
		}

        public void ShowClientWizard()
        {
            //Show();
            Init();
            ShowDialog();
        }

        private InfoDataSet DoGetRoot(InfoDataSet RootDataSet, DataRelationCollection Relations, String DataSetName)
        {
            foreach (DataRelation Relation in Relations)
            {
                if (Relation.ChildTable.TableName.CompareTo(DataSetName) == 0)
                {
                    return RootDataSet;
                }
                DoGetRoot(RootDataSet, Relation.ChildTable.ChildRelations, DataSetName);
            }
            return null;
        }

        private InfoDataSet GetRootInfoDataSet(String DataSetName)
        {
            foreach (InfoDataSet aDataSet in FClientData.DataSetList)
            {
                if (aDataSet.RealDataSet.Tables[0].TableName.CompareTo(DataSetName) == 0)
                    return aDataSet;
                InfoDataSet bDataSet = DoGetRoot(aDataSet, aDataSet.RealDataSet.Relations, DataSetName);
                if (bDataSet != null)
                    return bDataSet;
            }
            return null;
        }

		private void SetFieldNames(string TableName, ListView LV, String DataSetName)
	    {
			int I;
			DataRow[] DRs;
			DataRow DR;
            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = InternalConnection;
            TableName = WzdUtils.RemoveQuote(TableName, FClientData.DatabaseType);
            aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME = '" + TableName + "'";
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet dsColdef = new DataSet();
            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsColdef, TableName);

            InfoDataSet aDataSet = GetRootInfoDataSet(DataSetName);
            DataTable dtTableSchema = aDataSet.RealDataSet.Tables[0];
            for (I = 0; I < dtTableSchema.Columns.Count; I++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = dtTableSchema.Columns[I].ColumnName;
                DRs = dsColdef.Tables[0].Select("FIELD_NAME='" + lvi.Text + "'");
                TBlockFieldItem aBlockFieldItem = new TBlockFieldItem();
                aBlockFieldItem.DataField = lvi.Text;
                aBlockFieldItem.DataType = dtTableSchema.Columns[I].DataType;
                lvi.Tag = aBlockFieldItem;
                if (DRs.Length == 1)
                {
                    DR = DRs[0];
                    lvi.SubItems.Add(DR["CAPTION"].ToString());

                    aBlockFieldItem.Description = DR["CAPTION"].ToString();
                    aBlockFieldItem.CheckNull = DR["CHECK_NULL"].ToString().ToUpper();
                    aBlockFieldItem.DefaultValue = DR["DEFAULT_VALUE"].ToString();
                    aBlockFieldItem.ControlType = DR["NEEDBOX"].ToString();
                    aBlockFieldItem.EditMask = DR["EDITMASK"].ToString();
                    if (aBlockFieldItem.DataType == typeof(DateTime))
                    {
                        if (aBlockFieldItem.ControlType == null || aBlockFieldItem.ControlType == "")
                            aBlockFieldItem.ControlType = "DateTimeBox";
                    }
                    aBlockFieldItem.QueryMode = DR["QUERYMODE"].ToString();
                }
                LV.Items.Add(lvi);
            }
            /*
    		string[] S = new string[4];
			S[2] = TableName;
            DataTable dtTableSchema = InternalConnection.GetSchema("Columns", S);
            DataRow[] DRs1 = dtTableSchema.Select("", "ORDINAL_POSITION ASC");
            for (I = 0; I < DRs1.Length; I++)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = DRs1[I][3].ToString();
				DRs = dsColdef.Tables[0].Select("FIELD_NAME='" + lvi.Text + "'");
				if (DRs.Length == 1)
				{
					DR = DRs[0];
					lvi.SubItems.Add(DR["CAPTION"].ToString());
				}
				LV.Items.Add(lvi);
			}
             */
	    }

        private void SetFieldNamesByProvider(String TableName, String ProviderName, ListView aListView)
        {
            if (ProviderName == null || ProviderName.Trim() == "")
                return;

            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = InternalConnection;
            TableName = WzdUtils.RemoveQuote(TableName, FClientData.DatabaseType);
            aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME = '" + TableName + "'";
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet dsColdef = new DataSet();
            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsColdef, TableName);

            aListView.Items.Clear();
            InfoDataSet aDataSet = new InfoDataSet();
            try
            {
                aDataSet.SetWizardDesignMode(true);
                aDataSet.RemoteName = ProviderName;
                aDataSet.AlwaysClose = true;
                aDataSet.Active = true;
                DataTable Table = aDataSet.RealDataSet.Tables[0];
                foreach (DataColumn Column in Table.Columns)
                {
                    ListViewItem aItem = new ListViewItem(Column.ColumnName);
                    DataRow[] DRS = dsColdef.Tables[0].Select("FIELD_NAME='" + Column.ColumnName + "'");
                    if (DRS.Length == 1)
                        aItem.SubItems.Add(DRS[0]["CAPTION"].ToString());
                    else
                        aItem.SubItems.Add("");
                    aListView.Items.Add(aItem);
                    TBlockFieldItem aFieldItem = new TBlockFieldItem();
                    aFieldItem.DataField = Column.ColumnName;
                    aItem.Tag = aFieldItem;
                }
            }
            finally
            {
                aDataSet.Dispose();
            }
        }

        private void GetTemplateForms()
        {
            cbBaseForm.Items.Clear();
            String Language = "{B5E9BD34-6D3E-4B5D-925E-8A43B79820B4}";
            if (FClientData.Language == "vb")
                Language = "{B5E9BD33-6D3E-4B5D-925E-8A43B79820B4}";
            foreach (Project P in FDTE2.Solution.Projects)
            {
                if (FClientData.TemplatePath == null || FClientData.TemplatePath == "")
                    FClientData.TemplatePath = Path.GetDirectoryName(FDTE2.Solution.FullName) + "\\Template\\";

                if (P.Name.CompareTo("Template") == 0 && P.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
                {
                    foreach (ProjectItem PI in P.ProjectItems)
                    {
                        if (PI.SubProject != null && PI.SubProject.CodeModel.Language.ToString().CompareTo(Language) == 0)
                        {
                            cbBaseForm.Items.Add(PI.Name);
                        }
                    }
                }
            }
            if (cbBaseForm.Items.Count > 0)
                this.cbBaseForm.SelectedIndex = 0;
        }

        private void btnNext_Click(object sender, EventArgs e)
		{
            if (tabControl1.SelectedTab.Equals(tpConnection))
            {
                WzdUtils.SetRegistryValueByKey("WizardConnectionString", cbEEPAlias.Text);
                WzdUtils.SetRegistryValueByKey("DatabaseType", cbDatabaseType.Text);
                if (FDTE2.Solution.FullName == null || FDTE2.Solution.FullName == "")
                {
                    rbAddToExistSln.Checked = true;
                    rbAddToExistSln_Click(rbAddToExistSln, null);
                }
                else
                    rbAddToCurrent.Checked = true;
                string type = FindDBType(cbEEPAlias.Text);
                switch (type)
                {
                    case "1":
                        FClientData.DatabaseType = ClientType.ctMsSql; break;
                    case "2":
                        FClientData.DatabaseType = ClientType.ctOleDB; break;
                    case "3":
                        FClientData.DatabaseType = ClientType.ctOracle; break;
                    case "4":
                        FClientData.DatabaseType = ClientType.ctODBC; break;
                    case "5":
                        FClientData.DatabaseType = ClientType.ctMySql; break;
                    case "6":
                        FClientData.DatabaseType = ClientType.ctInformix; break;
                    case "7":
                        FClientData.DatabaseType = ClientType.ctSybase; break;
                }
                DisplayPage(tpOutputSetting);
                if (cbChooseLanguage.Text == "" || cbChooseLanguage.Text == "C#")
                    FClientData.Language = "cs";
                else if (cbChooseLanguage.Text == "VB")
                    FClientData.Language = "vb";

            }
            else if (tabControl1.SelectedTab.Equals(tpOutputSetting))
            {
                if (rbAddToCurrent.Checked)
                {
                    FClientData.SolutionName = tbCurrentSolution.Text;
                }
                if (rbNewSolution.Checked)
                {
                    if (tbNewSolutionName.Text == "")
                    {
                        MessageBox.Show("Please input Solution Name !!");
                        if (tbNewSolutionName.CanFocus)
                        {
                            tbNewSolutionName.Focus();
                        }
                        return;
                    }
                    if (tbNewLocation.Text == "")
                    {
                        MessageBox.Show("Please input Location !!");
                        if (tbNewLocation.CanFocus)
                        {
                            tbNewLocation.Focus();
                        }
                        return;
                    }
                    FClientData.SolutionName = tbNewSolutionName.Text;
                    FClientData.OutputPath = tbNewLocation.Text;
                    FClientData.CodeOutputPath = tbOutputPath.Text;
                    FDTE2.Solution.Create(FClientData.OutputPath, FClientData.SolutionName);
                    FDTE2.Solution.SaveAs(FClientData.OutputPath + "\\" + FClientData.SolutionName + ".sln");
                    FDTE2.Solution.Open(FClientData.OutputPath + "\\" + FClientData.SolutionName + ".sln");
                }
                if (rbAddToExistSln.Checked)
                {
                    if (tbSolutionName.Text == "")
                    {
                        MessageBox.Show("Please input Location !!");
                        if (tbNewLocation.CanFocus)
                        {
                            tbNewLocation.Focus();
                        }
                        return;
                    }
                    FClientData.SolutionName = tbSolutionName.Text;
                    FClientData.CodeOutputPath = tbOutputPath.Text;
                    if (string.Compare(tbSolutionName.Text, FDTE2.Solution.FullName) != 0)
                        FDTE2.Solution.Open(FClientData.SolutionName);
                }
                if (tbPackageName.Text == "")
                {
                    MessageBox.Show("Please input Package Name !!");
                    if (tbPackageName.CanFocus)
                    {
                        tbPackageName.Focus();
                    }
                }
                else
                {
                    FClientData.PackageName = tbPackageName.Text;
                    tbClientPackage.Text = tbPackageName.Text;
                    FClientData.NewSolution = rbNewSolution.Checked;
                    FClientData.CodeOutputPath = tbOutputPath.Text;
                    GetTemplateForms();
                    DisplayPage(tpFormSetting);
                }
                FClientData.AssemblyOutputPath = tbAssemblyOutputPath.Text;
            }
            else if (tabControl1.SelectedTab.Equals(tpFormSetting))
            {
                if (rbEEPBaseForm.Checked && (cbBaseForm.Text == ""))
                {
                    MessageBox.Show("Please select EEP Windows Templates Form !!");
                    if (cbBaseForm.CanFocus)
                    {
                        cbBaseForm.Focus();
                    }
                }
                else if (tbFormName.Text == "")
                {
                    MessageBox.Show("Please input Form Name !!");
                    if (tbFormName.CanFocus)
                    {
                        tbFormName.Focus();
                    }
                }
                else
                {
                    if (FTemplateForm == null || FClientData.BaseFormName != cbBaseForm.Text)
                    {
                        ClearFormData();
                        OpenTemplateForm();
                        DisplayPage(tpProvider);
                        InitComponentList(typeof(InfoDataSet), lvDataSet, btnSelectProvider_Click, null);
                        DisplayPage(tpBindingSource);
                        InitComponentList(typeof(InfoBindingSource), lvBindingSource, btnSelectDataMember_Click, cbBindingSource);
                        InitTemplateFormContainer();
                    }
                    FClientData.FormName = tbFormName.Text;
                    FClientData.FormText = tbFormText.Text;
                    if (rbWindowsForm.Checked)
                    {
                        FClientData.BaseFormName = "System.Windows.Form";
                    }
                    else 
                    {
                        FClientData.BaseFormName = cbBaseForm.Text;
                    }
                    DisplayPage(tpProvider);
                }
            }
            else if (tabControl1.SelectedTab.Equals(tpProvider))
            {
                DisplayPage(tpBindingSource);
            }
            else if (tabControl1.SelectedTab.Equals(tpBindingSource))
            {
                DisplayPage(tpLayout);
            }
            BringToFront();
        }

        private void OpenTemplateForm()
        {
            FTemplateForm = null;
            foreach (Project P in FDTE2.Solution.Projects)
            {
                if (P.Name.CompareTo("Template") == 0 && P.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
                {
                    foreach (ProjectItem PI in P.ProjectItems)
                    {
                        if (PI.Name.CompareTo(cbBaseForm.Text) == 0)
                        {
                            if (PI.SubProject != null)
                            {
                                foreach (ProjectItem FormPI in PI.SubProject.ProjectItems)
                                {
                                    if (FormPI.Name.CompareTo("Form1." + FClientData.Language) == 0)
                                    {
                                        Window W = FormPI.Open("{00000000-0000-0000-0000-000000000000}");
                                        IDesignerHost aDesignerHost = (IDesignerHost)W.Object;
                                        FTemplateForm = (System.Windows.Forms.Form)aDesignerHost.RootComponent as InfoForm;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
            if (FTemplateForm == null)
            {
                MessageBox.Show(String.Format("Cannot open template form [Form1] of project [{0}] !", cbBaseForm.Text));
                throw new Exception("");
            }
        }

        private void InitComponentList(Type ObjectType, ListView aListView, EventHandler aClick, ComboBox aComboBox)
        {
            aListView.BeginUpdate();
            foreach (Object Obj in FTemplateForm.Container.Components)
            {
                if (Obj is Component)
                {
                    Component Comp = Obj as Component;
                    if (Comp.GetType().Equals(ObjectType))
                    {
                        ListViewItem Item = aListView.Items.Add(Comp.Site.Name);
                        Item.Tag = Comp;
                        if (aComboBox != null)
                        {
                            aComboBox.Items.Add(Comp.Site.Name);
                            cbViewBindingSource.Items.Add(Comp.Site.Name);
                        }
                        if (ObjectType.Equals(typeof(InfoBindingSource)))
                        {
                            InfoBindingSource bBindingSource = FClientData.FindBindingSource(Comp.Site.Name);
                            if (bBindingSource == null)
                            {
                                bBindingSource = new InfoBindingSource(FContainer);
                                FClientData.BindingSourceList.Add(bBindingSource);
                            }
                            bBindingSource.Site.Name = Comp.Site.Name;
                            if (((InfoBindingSource)Comp).DataSource is InfoDataSet)
                                bBindingSource.text = ((InfoDataSet)((InfoBindingSource)Comp).DataSource).Site.Name;
                            if (((InfoBindingSource)Comp).DataSource is InfoBindingSource)
                                bBindingSource.text = ((InfoBindingSource)((InfoBindingSource)Comp).DataSource).Site.Name;
                        }
                        if (ObjectType.Equals(typeof(InfoDataSet)))
                        {
                            InfoDataSet aDataSet = FClientData.FindDataSet(Comp.Site.Name);
                            if (aDataSet == null)
                            {
                                aDataSet = new InfoDataSet(FContainer);
                                FClientData.DataSetList.Add(aDataSet);
                            }
                            aDataSet.SetWizardDesignMode(true);
                            aDataSet.Site.Name = Item.Text;
                        }
                        Item.SubItems.Add("");
                        ListViewItem.ListViewSubItem LVSI = Item.SubItems.Add("");
                        CreateRefValButton(Item, LVSI, aClick);
                    }
                }
            }
            FixupBindingSourceReference();
            aListView.EndUpdate();
        }

        private void FixupBindingSourceReference()
        {
            ArrayList NewList = new ArrayList();
            foreach (InfoBindingSource aSource in FClientData.BindingSourceList)
            {
                Component C = FClientData.FindDataSet(aSource.text);
                if (C != null && C.GetType().Equals(typeof(InfoDataSet)))
                {   
                    aSource.DataSource = C;
                    NewList.Add(aSource);
                    TraceTree(aSource, NewList);
                }
            }
            FClientData.BindingSourceList.Clear();
            foreach (InfoBindingSource bSource in NewList)
                FClientData.BindingSourceList.Add(bSource);
        }

        private void TraceTree(InfoBindingSource aBindingSource, ArrayList NewList)
        {
            foreach (InfoBindingSource aSource in FClientData.BindingSourceList)
            {
                Component C = FClientData.FindBindingSource(aSource.text);
                if (C == aBindingSource)
                {
                    aSource.DataSource = aBindingSource;
                    NewList.Add(aSource);
                    TraceTree(aSource, NewList);
                }
            }
        }

        private void CreateRefValButton(ListViewItem Item, ListViewItem.ListViewSubItem SubItem,
            EventHandler ClickEvent)
        {
            System.Windows.Forms.Button B = new System.Windows.Forms.Button();
            B.Parent = Item.ListView;
            RearrangeRefValButton(B, SubItem.Bounds);
            B.BackColor = Color.Silver;
            B.BringToFront();
            SubItem.Tag = B;
            B.Tag = Item;
            B.Click += new EventHandler(ClickEvent);
            B.Text = "...";
        }

        private void InitTemplateFormContainer()
        {
            TreeNode FormNode = tvContainer.Nodes.Add(tbFormName.Text);
            TNodeObject NodeObject = new TNodeObject(FormNode, FClientData, FTemplateForm);
            NodeObject.BlockItem.ContainerName = tbFormName.Text;
            BuildContainerTree(FTemplateForm, FormNode);
            FormNode.ExpandAll();
        }

        private void BuildContainerTree(Control ParentControl, TreeNode ParentNode)
        {
            foreach (Control ChildControl in ParentControl.Controls)
            {
                if (ChildControl is Panel || ChildControl is DataGridView || ChildControl is DataGrid ||
                    ChildControl is TabPage || ChildControl is InfoNavigator || ChildControl is TabControl ||
                    ChildControl is SplitContainer)
                {
                    TreeNode ChildNode = new TreeNode();
                    ChildNode.Text = ChildControl.Name;
                    ParentNode.Nodes.Add(ChildNode);
                    TNodeObject NodeObject = new TNodeObject(ChildNode, FClientData, ChildControl);
                    if (!(ChildControl is InfoNavigator))
                        BuildContainerTree(ChildControl, ChildNode);
                    if (ChildControl is DataGrid)
                    {
                        if ((InfoBindingSource)((DataGrid)ChildControl).DataSource != null)
                        {
                            InfoBindingSource aBindingSource = FClientData.FindBindingSource(((InfoBindingSource)((DataGrid)ChildControl).DataSource).Site.Name);
                            NodeObject.BlockItem.BindingSource = aBindingSource;
                        }
                    }
                    if (ChildControl is DataGridView)
                    {
                        if ((InfoBindingSource)((DataGridView)ChildControl).DataSource != null)
                        {
                            InfoBindingSource aBindingSource = FClientData.FindBindingSource(((InfoBindingSource)((DataGridView)ChildControl).DataSource).Site.Name);
                            NodeObject.BlockItem.BindingSource = aBindingSource;
                        }
                    }
                    if (ChildControl is InfoNavigator)
                    {
                        NodeObject.BlockItem.Name = ((InfoNavigator)ChildControl).Name;
                        if ((InfoBindingSource)((InfoNavigator)ChildControl).BindingSource != null)
                        {
                            InfoBindingSource aBindingSource = FClientData.FindBindingSource(((InfoBindingSource)((InfoNavigator)ChildControl).BindingSource).Site.Name);
                            NodeObject.BlockItem.BindingSource = aBindingSource;
                        }
                        if ((InfoBindingSource)((InfoNavigator)ChildControl).ViewBindingSource != null)
                        {
                            InfoBindingSource aBindingSource = FClientData.FindBindingSource(((InfoBindingSource)((InfoNavigator)ChildControl).ViewBindingSource).Site.Name);
                            NodeObject.BlockItem.ViewBindingSource = aBindingSource;
                        }
                    }
                }
            }
        }

		private void button1_Click(object sender, EventArgs e)
		{
            if (tabControl1.SelectedTab.Equals(tpOutputSetting))
            {
                DisplayPage(tpConnection);
            }
            else
            {
                if (tabControl1.SelectedTab.Equals(tpFormSetting))
                {
                    DisplayPage(tpOutputSetting);
                }
                if (tabControl1.SelectedTab.Equals(tpProvider))
                {
                    DisplayPage(tpFormSetting);
                }
                if (tabControl1.SelectedTab.Equals(tpBindingSource))
                {
                    DisplayPage(tpProvider);
                }
                if (tabControl1.SelectedTab.Equals(tpLayout))
                {
                    DisplayPage(tpBindingSource);
                }
            }
		}

		private void rbWindowsForm_CheckedChanged(object sender, EventArgs e)
		{
			if (rbWindowsForm.Checked)
			{
				cbBaseForm.Enabled = false;
			}
		}

		private void rbEEPBaseForm_CheckedChanged(object sender, EventArgs e)
		{
			if (rbEEPBaseForm.Checked)
			{
				cbBaseForm.Enabled = true;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
            Hide();
		}

        private void ClearRefValButton(ListView LV)
        {
            //foreach (ListViewItem LVI in LV.Items)
            //{
            //    if (LVI.SubItems.Count > 1)
            //    {
            //        ListViewItem.ListViewSubItem LVSI = LVI.SubItems[2];
            //        if (LVSI != null)
            //        {
            //            if (LVSI.Tag != null)
            //            {
            //                ((Button)LVSI.Tag).Dispose();
            //            }
            //        }
            //    }
            //}
        }

		private void SelectFields(ListView lvSrc, ListView lvDes, Boolean All)
	    {
		    int I;
			ListViewItem Item;
            ListViewItem.ListViewSubItem LVSI;

			for (I = 0; I < lvSrc.Items.Count; I++)
			{
				if (lvSrc.Items[I].Selected || All)
				{
					Item = new ListViewItem(lvSrc.Items[I].Text);
                    Item.Tag = lvSrc.Items[I].Tag;
                    if (lvSrc.Items[I].SubItems.Count > 1)
						Item.SubItems.Add(lvSrc.Items[I].SubItems[1]);
                    else
                        Item.SubItems.Add("");
                    lvDes.Items.Add(Item);
                    if (lvDes.Columns.Count == 3)
                    {
                        LVSI = Item.SubItems.Add("");
                        CreateRefValButton(Item, LVSI, btnRefVal_Click);
                    }
                }
			}

			for (I = lvSrc.Items.Count - 1; I >= 0; I--)
			{
				if (lvSrc.Items[I].Selected || All)
				{
                    if (lvSrc.Columns.Count == 3)
                    {
                        LVSI = lvSrc.Items[I].SubItems[2];
                        if (LVSI != null)
                        { 
                            if (LVSI.Tag != null)
                            {
                                ((Button)LVSI.Tag).Dispose();
                            }
                        }
                    }
					lvSrc.Items[I].Remove();
				}
			}

            if (lvSrc.Columns.Count == 3)
            {
                foreach (ListViewItem LVI in lvSrc.Items)
                {
                    LVSI = LVI.SubItems[2];
                    if (LVSI.Tag != null)
                        RearrangeRefValButton((Button)LVSI.Tag, LVSI.Bounds);
                }
            }
	    }

        private void DisplayValue(ListView aListView)
        {
            foreach (ListViewItem aViewItem in aListView.Items)
            {
                TBlockFieldItem BlockFieldItem = (TBlockFieldItem)aViewItem.Tag;
                aViewItem.SubItems[1].Text = BlockFieldItem.Description;
            }
        }

        private void btnRefVal_Click(object sender, EventArgs e)
        {
            Object aObject = ((System.Windows.Forms.Button)sender).Tag;
            ListViewItem.ListViewSubItem LVSI = null;
            ListViewItem aViewItem = null;
            TBlockFieldItem BlockFieldItem = null;

            if (aObject.GetType().Equals(typeof(ListViewItem.ListViewSubItem)))
            {
                MessageBox.Show("This is wrong way");
                throw new Exception("");
                //LVSI = (ListViewItem.ListViewSubItem)((System.Windows.Forms.Button)sender).Tag;
                //BlockFieldItem = (TBlockFieldItem)LVSI.Tag;
            }
            if (aObject.GetType().Equals(typeof(ListViewItem)))
            {
                aViewItem = (ListViewItem)((System.Windows.Forms.Button)sender).Tag;
                BlockFieldItem = (TBlockFieldItem)aViewItem.Tag;
                LVSI = ((ListViewItem)aObject).SubItems[2];
            }

            fmFieldSetting aForm = new fmFieldSetting(InternalConnection, FClientData.DatabaseType, aViewItem.ListView, TWizardType.wtWinForm, FClientData.DatabaseName);
            try
            {
                String[] Params = new String[] { BlockFieldItem.Description, BlockFieldItem.CheckNull,
                    BlockFieldItem.DefaultValue, BlockFieldItem.ControlType, BlockFieldItem.RefValNo,
                    BlockFieldItem.ComboEntityName, BlockFieldItem.ComboTextField, BlockFieldItem.ComboValueField, BlockFieldItem.EditMask};
                if (aForm.ShowRefValForm(Params))
                {
                    //BlockFieldItem.Description = Params[0];
                    //BlockFieldItem.CheckNull = Params[1].ToUpper();
                    //BlockFieldItem.DefaultValue = Params[2];
                    //BlockFieldItem.ControlType = Params[3];
                    //BlockFieldItem.RefValNo = Params[4];
                    //BlockFieldItem.ComboTableName = Params[5];
                    //BlockFieldItem.ComboTextField = Params[6];
                    //BlockFieldItem.ComboValueField = Params[7];
                }
                //LVSI.Text = Params[4];
                //aViewItem.SubItems[1].Text = BlockFieldItem.Description;
                DisplayValue(aViewItem.ListView);
            }
            finally
            {
                aForm.Dispose();
            }
        }

        private void AddDetailBlockItem(string MasterItemName, TreeNodeCollection NodeCollection)
        {
            for (int I = 0; I < NodeCollection.Count; I++)
            {
                TDetailItem DetailItem = (TDetailItem)NodeCollection[I].Tag;
                TBlockItem BlockItem = new TBlockItem();
                BlockItem.Name = NodeCollection[I].Text;
                BlockItem.RelationName = DetailItem.Relation.RelationName;
                BlockItem.TableName = DetailItem.TableName;
                if (NodeCollection[I].Parent != null)
                {
                    BlockItem.ParentItemName = NodeCollection[I].Parent.Text;
                }
                else
                {
                    BlockItem.ParentItemName = MasterItemName;
                }
                FClientData.Blocks.Add(BlockItem);
                BlockItem.BlockFieldItems = DetailItem.BlockFieldItems;
                AddDetailBlockItem(MasterItemName, NodeCollection[I].Nodes);
            }
        }

        private void DoGenClient()
        {
            FClientData.OutputPath = tbNewLocation.Text;
            FClientData.CodeOutputPath = tbOutputPath.Text;
            FClientData.PackageName = tbPackageName.Text;
            FClientData.FormName = tbFormName.Text;
            FClientData.ColumnCount = cbColumnCount.SelectedIndex + 1;
            TExtClientGenerator Generator = new TExtClientGenerator(FClientData, FDTE2, FAddIn);
            if (rbEEPBaseForm.Checked)
            {
                Generator.GenClientModule();
            }
        }

        private bool bAbort = false;
        private void ShowProgressBar()
        {
            bAbort = false;
            ProgressForm aForm = new ProgressForm();
            aForm.Show();
            while (!bAbort)
            {
                aForm.progressBar1.Value += 3;
                if (aForm.progressBar1.Value >= 100)
                    aForm.progressBar1.Value = 1;
                System.Threading.Thread.Sleep(100);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
		{
            FClientData.DatabaseType = FClientData.DatabaseType;
            Hide();
            FDTE2.MainWindow.Activate();
            DoGenClient();
            foreach (InfoDataSet aDataSet in FClientData.DataSetList)
                aDataSet.Dispose();
            FClientData.DataSetList.Clear();
            foreach (InfoBindingSource aBindingSource in FClientData.BindingSourceList)
                aBindingSource.Dispose();
            FClientData.BindingSourceList.Clear();
        }

		private void button2_Click(object sender, EventArgs e)
		{
            FClientData.OutputPath = tbNewLocation.Text;
            FClientData.PackageName = tbPackageName.Text;
            FClientData.FormName = tbFormName.Text;
            TExtClientGenerator G = new TExtClientGenerator(FClientData, FDTE2, FAddIn);
            G.GenClientModule();
            Close();
		}

        private void btnNewLocation_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbNewLocation.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnSolutionName_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbSolutionName.Text = openFileDialog.FileName;
            }
        }

        private void EnabledOutputControls()
        {
            tbNewSolutionName.Enabled = rbNewSolution.Checked;
            tbNewLocation.Enabled = rbNewSolution.Checked;
            btnNewLocation.Enabled = rbNewSolution.Checked;
            tbSolutionName.Enabled = rbAddToExistSln.Checked;
            btnSolutionName.Enabled = rbAddToExistSln.Checked;
        }

        private void SetCodeOutputPath()
        {
            if (rbAddToCurrent.Checked)
            {
                string S = tbCurrentSolution.Text;
                if (S != "")
                {
                    S = System.IO.Path.GetDirectoryName(S);
                    String SolutionName = Path.GetFileNameWithoutExtension(tbCurrentSolution.Text);
                    tbOutputPath.Text = S+ @"\" + SolutionName;
                    tbAssemblyOutputPath.Text = String.Format(@"..\..\EEPNetClient\{0}", SolutionName);
                }
            }
            if (rbNewSolution.Checked)
            {
                tbOutputPath.Text = tbNewLocation.Text;
            }
            if (rbAddToExistSln.Checked)
            {
                string S = tbSolutionName.Text;
                if (S != "")
                {
                    S = System.IO.Path.GetDirectoryName(S);
                    String SolutionName = Path.GetFileNameWithoutExtension(tbSolutionName.Text);
                    tbOutputPath.Text = S + @"\" + SolutionName;                    
                    tbAssemblyOutputPath.Text = String.Format(@"..\..\EEPNetClient\{0}", SolutionName);
                }
            }
        }

        private void rbNewSolution_Click(object sender, EventArgs e)
        {
            EnabledOutputControls();
            SetCodeOutputPath();
        }

        private void rbAddToExistSln_Click(object sender, EventArgs e)
        {
            EnabledOutputControls();
            SetCodeOutputPath();
        }

        private void rbAddToCurrent_Click(object sender, EventArgs e)
        {
            EnabledOutputControls();
            SetCodeOutputPath();
        }

        private void btnConnectionString_Click(object sender, EventArgs e)
        {
            ADODB.Connection Conn = null;
            String ConnectionString = tbConnectionString.Text;
            MSDASC.DataLinks dataLinks = new MSDASC.DataLinksClass();
            if (ConnectionString == string.Empty)
            {
                Conn = (ADODB.Connection)dataLinks.PromptNew();
            }
            else
            {
                Conn = new ADODB.Connection();
                Conn.ConnectionString = tbConnectionString.Text;
                object TempConn = Conn;
                if (dataLinks.PromptEdit(ref TempConn))
                    ConnectionString = Conn.ConnectionString;
            }
            tbConnectionString.Text = Conn.ConnectionString;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = "C:\\Program Files\\Infolight\\EEP2006\\EEPNetServer\\WindowsApplication1\\SSingle.dll";
            string sDll = "SSingle";
            FileStream fs = null;
            fs = new FileStream(s, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] b = new byte[fs.Length];
            fs.Read(b, 0, (int)fs.Length);

            // Add By Chenjian, Can not delete dll file if FileStream is not closed
            fs.Close();
            // End Add

            Assembly a = Assembly.Load(b);
            try
            {
                Type myType = a.GetType(sDll + ".Component", true,  true);
                if (myType != null)
                    MessageBox.Show("get");
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void UpdatelvSelectedFields(TDetailItem DetailItem)
        {
            lvSelectedFields.BeginUpdate();
            lvSelectedFields.Items.Clear();
            try
            {
                    int I;
                    TBlockFieldItem BlockFieldItem;
                    ListViewItem ViewItem;
                    for (I = 0; I < DetailItem.BlockFieldItems.Count; I++)
                    {
                        BlockFieldItem = (TBlockFieldItem)DetailItem.BlockFieldItems[I];
                        ViewItem = lvSelectedFields.Items.Add(BlockFieldItem.DataField);
                        ViewItem.SubItems.Add(BlockFieldItem.Description);
                        ViewItem.Tag = BlockFieldItem;
                    }
            }
            finally
            {
                lvSelectedFields.EndUpdate();
            }
        }

        public void GetFieldNames(string DatabaseName, string TableName, String DataSetName, ListView SrcListView, ListView DestListView)
        {
            TreeNode Node = tvContainer.SelectedNode;
            if (Node == null)
                return;
            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = InternalConnection;
            TableName = WzdUtils.RemoveQuote(TableName, FClientData.DatabaseType);
            aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME = '" + TableName + "'";
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet dsColdef = new DataSet();
            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsColdef, "COLDEF");

            InfoDataSet aDataSet = GetRootInfoDataSet(DataSetName);
            int Index = aDataSet.RealDataSet.Tables.IndexOf(WzdUtils.RemoveSpace(DataSetName));
            DataTable Table = aDataSet.RealDataSet.Tables[Index];

            int I;
            ListViewItem ViewItem;
            for (I = 0; I < DestListView.Items.Count; I++)
            {
                ViewItem = DestListView.Items[I];
                if (Table.Columns.IndexOf(ViewItem.Text) < 0)
                { 
                    if (ViewItem.Tag != null)
                        if (ViewItem.Tag.GetType().Equals(typeof(TBlockFieldItem)))
                        {
                            TBlockFieldItem B = (TBlockFieldItem)ViewItem.Tag;
                            B.Collection.Remove(B);
                        }
                }
            }

            SrcListView.Items.Clear();
            bool Found;
            int J;
            DataRow[] DRs = null;
            for (I = 0; I < Table.Columns.Count; I++)
            {
                Found = false;
                for (J = 0; J < DestListView.Items.Count; J++)
                {
                    ViewItem = DestListView.Items[J];
                    if (string.Compare(Table.Columns[I].ColumnName, ViewItem.Text, false) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (Found == false)
                {
                    TBlockFieldItem FieldItem = new TBlockFieldItem();
                    FieldItem.DataField = Table.Columns[I].ColumnName;
                    FieldItem.DataType = Table.Columns[I].DataType;
                    ViewItem = SrcListView.Items.Add(Table.Columns[I].ColumnName);
                    ViewItem.Tag = FieldItem;
                    DRs = dsColdef.Tables[0].Select("TABLE_NAME = '" + TableName + "' and FIELD_NAME = '" + Table.Columns[I].ColumnName + "'");
                    if (DRs.Length == 1)
                    {
                        FieldItem.Description = DRs[0]["CAPTION"].ToString();
                        FieldItem.CheckNull = DRs[0]["CHECK_NULL"].ToString();
                        FieldItem.DefaultValue = DRs[0]["DEFAULT_VALUE"].ToString();
                        FieldItem.IsKey = DRs[0]["IS_KEY"].ToString().ToUpper() == "Y";
                        FieldItem.ControlType = DRs[0]["NEEDBOX"].ToString().ToUpper();
                        FieldItem.EditMask = DRs[0]["EDITMASK"].ToString().ToUpper();
                        if (DRs[0]["FIELD_LENGTH"] != null && DRs[0]["FIELD_LENGTH"].ToString() != String.Empty)
                            FieldItem.Length = int.Parse(DRs[0]["FIELD_LENGTH"].ToString());
                    }
                    ViewItem.SubItems.Add(FieldItem.Description);
                }
            }
        }

        public String FindDBType(String aliasName)
        {
            String xmlName = SystemFile.DBFile;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlName);

            XmlNode node = xmlDoc.FirstChild.FirstChild.SelectSingleNode(aliasName);

            string DbType = node.Attributes["Type"].Value.Trim();
            return DbType;
        }

        private void cbEEPAlias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = FindDBType(cbEEPAlias.Text);
            int num1 = FAlias.IndexOf(cbEEPAlias.Text);
            tbConnectionString.Text = (string)FAlias.Objects(num1);
            FClientData.DatabaseName = this.cbEEPAlias.Text;
            switch (type)
            {
                case "1":
                    FClientData.DatabaseType = ClientType.ctMsSql; break;
                case "2":
                    FClientData.DatabaseType = ClientType.ctOleDB; break;
                case "3":
                    FClientData.DatabaseType = ClientType.ctOracle; break;
                case "4":
                    FClientData.DatabaseType = ClientType.ctODBC; break;
                case "5":
                    FClientData.DatabaseType = ClientType.ctMySql; break;
                case "6":
                    FClientData.DatabaseType = ClientType.ctInformix; break;
                case "7":
                    FClientData.DatabaseType = ClientType.ctSybase; break;
            }
            cbDatabaseType.SelectedIndex = (int)FClientData.DatabaseType;

            if (InternalConnection == null)
            {
                InternalConnection = WzdUtils.AllocateConnection(this.cbEEPAlias.Text, FClientData.DatabaseType, false);
                FClientData.ConnString = InternalConnection.ConnectionString;
            }
            else
            {
                if (InternalConnection.State == ConnectionState.Open)
                    InternalConnection.Close();
                InternalConnection = WzdUtils.AllocateConnection(this.cbEEPAlias.Text, FClientData.DatabaseType, false);
                //InternalConnection.ConnectionString = tbConnectionString.Text;
            }

            if (InternalConnection.ConnectionString.Trim() != "")
            {
                try
                {
                    if (InternalConnection.State != ConnectionState.Open)
                        InternalConnection.Open();
                }
                catch (Exception E)
                {
                    MessageBox.Show(string.Format("Database ConnnectionString information error, please reset ConnectionString.\nThe error message:\n{0}", E.Message));
                }
            }
            FClientData.ResetDatabaseConnection();
        }

        private void tbCurrentSolution_TextChanged(object sender, EventArgs e)
        {
            SetCodeOutputPath();
        }

        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbOutputPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnSelectProvider_Click(object sender, EventArgs e)
        {
            InfoDataSet aDataSet = new InfoDataSet(FContainer);
            IGetValues aItem = (IGetValues)aDataSet;
            String[] FProviderNameList = aItem.GetValues("RemoteName");
            String strSelected = "";
            PERemoteName form = new PERemoteName(FProviderNameList, strSelected);
            if (form.ShowDialog() == DialogResult.OK)
            {
                ListViewItem ViewItem = (ListViewItem)((Button)sender).Tag;
                ViewItem.SubItems[1].Text = form.RemoteName;
                InfoDataSet bDataSet = FClientData.FindDataSet(ViewItem.Text);

                bDataSet.RealDataSet.Tables.Clear();
                bDataSet.RemoteName = form.RemoteName;
                bDataSet.SetWizardDesignMode(true);
                bDataSet.Active = true;
            }
        }

        private String GetTableNameByCommandName(String PackageName, String CommandName)
        {
            if ((fmExtClientWizard._serverPath == null) || (fmExtClientWizard._serverPath.Length == 0))
            {
                string text1 = WzdUtils.GetRegistryValueByKey("Server Path");
                if (text1[text1.Length - 1] != '\\')
                {
                    text1 = text1 + "\\";
                }
                fmExtClientWizard._serverPath = text1;
            }
            return "";
        }

        private void lvMasterDesField_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lvMasterDesField_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
            e.DrawFocusRectangle();
            ListViewItem.ListViewSubItem LVSI = e.Item.SubItems[3];

            if (LVSI != null)
            {
                Rectangle cellBounds = LVSI.Bounds;
                Rectangle rect = cellBounds;
                Point buttonLocation = new Point();
                buttonLocation.Y = cellBounds.Y + 1;
                buttonLocation.X = cellBounds.X + 1;
                Rectangle ButtonBounds = new Rectangle();

                ButtonBounds.X = cellBounds.X + 1;
                ButtonBounds.Y = cellBounds.Y + 1;
                ButtonBounds.Width = cellBounds.Width - 2;
                ButtonBounds.Height = cellBounds.Height - 3;

                Pen pen1 = new Pen(Brushes.White, 1);
                Pen pen2 = new Pen(Brushes.Black, 1);
                Pen pen3 = new Pen(Brushes.DimGray, 1);

                SolidBrush myBrush = new SolidBrush(SystemColors.Control);

                e.Graphics.FillRectangle(myBrush, ButtonBounds.X, ButtonBounds.Y, ButtonBounds.Width, ButtonBounds.Height);
                e.Graphics.DrawLine(pen2, ButtonBounds.X, ButtonBounds.Y + ButtonBounds.Height, ButtonBounds.X + ButtonBounds.Width, ButtonBounds.Y + ButtonBounds.Height);
                e.Graphics.DrawLine(pen2, ButtonBounds.X + ButtonBounds.Width, ButtonBounds.Y, ButtonBounds.X + ButtonBounds.Width, ButtonBounds.Y + ButtonBounds.Height);

                StringFormat sf1 = new StringFormat();
                sf1.Alignment = StringAlignment.Center;
                sf1.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString("...", new Font("SimSun", 5), Brushes.Black, ButtonBounds.X + 10, ButtonBounds.Y + 5, sf1);
            }
        }

        public delegate void RearrangeRefValButtonFunc(Button B, Rectangle Bounds);

        private void RearrangeRefValButton(Button B, Rectangle Bounds)
        {
            Rectangle NewBounds = new Rectangle();
            NewBounds.X = Bounds.X;
            NewBounds.Width = Bounds.Width;
            NewBounds.Y = Bounds.Y - 1;
            NewBounds.Height = Bounds.Height - 2;
            B.Bounds = NewBounds;
        }
        
        private void lvMasterDesField_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            //ListView LV = (ListView)sender;
            //foreach (ListViewItem LVI in LV.Items)
            //{
            //    ListViewItem.ListViewSubItem LVSI = LVI.SubItems[2];
            //    if (LVSI.Tag != null)
            //    {
            //        Button B = (Button)LVSI.Tag;
            //        RearrangeRefValButton(B, LVSI.Bounds);
            //    }
            //}
        }

        private void btnAssemblyOutputPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbAssemblyOutputPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void tbNewSolutionName_TextChanged(object sender, EventArgs e)
        {
            tbAssemblyOutputPath.Text = String.Format(@"..\..\EEPNetClient\{0}", tbNewSolutionName.Text);
        }

        private void tbFormName_TextChanged(object sender, EventArgs e)
        {
            tbFormText.Text = tbFormName.Text;
        }

        private void EnableBindingInformation(TBlockItem BlockItem)
        {
            cbBindingSource.Enabled = false;
            cbControlType.Enabled = false;
            cbViewBindingSource.Enabled = false;
            ClearRefValButton(lvSelectedFields);
            lvSelectedFields.Items.Clear();
            btnAddField.Enabled = false;
            btnDeleteField.Enabled = false;
            if (BlockItem.ContainerControl is Form || BlockItem.ContainerControl is Panel ||
                BlockItem.ContainerControl is TabPage)
            {
                cbBindingSource.Enabled = true;
                cbControlType.Enabled = true;
                btnAddField.Enabled = true;
                btnDeleteField.Enabled = true;
            }
            if (BlockItem.ContainerControl is DataGridView || BlockItem.ContainerControl is DataGrid)
            {
                cbBindingSource.Enabled = true;
                btnAddField.Enabled = true;
                btnDeleteField.Enabled = true;
            }
            if (BlockItem.ContainerControl is InfoNavigator)
            {
                cbBindingSource.Enabled = true;
                cbViewBindingSource.Enabled = true;
            }
        }

        private void DisplaySelection(TBlockItem BlockItem)
        {
            cbBindingSource.SelectedIndexChanged -= cbBindingSource_SelectedIndexChanged;
            cbViewBindingSource.SelectedIndexChanged -= cbViewBindingSource_SelectedIndexChanged;
            cbControlType.SelectedIndexChanged -= cbControlType_SelectedIndexChanged;
            cbBindingSource.Text = "";
            cbViewBindingSource.Text = "";
            cbControlType.Text = "";
            EnableBindingInformation(BlockItem);
            if (BlockItem.BindingSource != null)
                cbBindingSource.Text = BlockItem.BindingSource.Site.Name;
            if (BlockItem.ViewBindingSource != null)
                cbViewBindingSource.Text = BlockItem.ViewBindingSource.Site.Name;
            if (BlockItem.LayoutKind == TLayoutType.ltTextBox)
                cbControlType.Text = "TextBox";
            if (BlockItem.LayoutKind == TLayoutType.ltDataGridView)
                cbControlType.Text = "Grid";

            //Columns
            foreach (TBlockFieldItem BlockFieldItem in BlockItem.BlockFieldItems)
            {
                ListViewItem ListItem = lvSelectedFields.Items.Add(BlockFieldItem.DataField);
                ListItem.SubItems.Add(BlockFieldItem.Description);
                ListViewItem.ListViewSubItem LVSI = ListItem.SubItems.Add("");
                CreateRefValButton(ListItem, LVSI, btnRefVal_Click);
                ListItem.Tag = BlockFieldItem;
            }
            cbBindingSource.SelectedIndexChanged += cbBindingSource_SelectedIndexChanged;
            cbViewBindingSource.SelectedIndexChanged += cbViewBindingSource_SelectedIndexChanged;
            cbControlType.SelectedIndexChanged += cbControlType_SelectedIndexChanged;
        }

        private void tvContainer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag == null)
                return;
            TNodeObject NodeObject = (TNodeObject)e.Node.Tag;
            DisplaySelection(NodeObject.BlockItem);
        }

        private void SetNavigatorProvider(TreeNode Node, String ProviderName)
        {
            foreach (TreeNode ChildNode in Node.Nodes)
            {
                TNodeObject NodeObject = (TNodeObject)ChildNode.Tag;
                if (NodeObject.BlockItem.ContainerControl.GetType().Equals(typeof(InfoNavigator)) ||
                    NodeObject.BlockItem.ContainerControl.GetType().IsSubclassOf(typeof(InfoNavigator)))
                {
                    NodeObject.BlockItem.ProviderName = ProviderName;
                    return;
                }
                SetNavigatorProvider(ChildNode, ProviderName);
            }
        }

        private void cbControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNode Node = tvContainer.SelectedNode;
            if (Node != null)
            {
                TNodeObject NodeObject = (TNodeObject)Node.Tag;
                NodeObject.BlockItem.LayoutKind = (TLayoutType)((ComboBox)sender).SelectedIndex;
            }
        }

        private String GetRelationName(InfoDataSet aDataSet, DataRelationCollection Relations, String DataSetName)
        {
            String Result = "";
            if (aDataSet != null)
            {
                if (aDataSet.RealDataSet.Tables[0].TableName.CompareTo(DataSetName) == 0)
                    Result = DataSetName;
                if (Result == "")
                    Result = GetRelationName(null, aDataSet.RealDataSet.Relations, DataSetName);
            }
            else
            {
                foreach (DataRelation Relation in Relations)
                {
                    if (Relation.ChildTable.TableName.CompareTo(DataSetName) == 0)
                        return Relation.RelationName;
                    Result = GetRelationName(null, Relation.ChildTable.ChildRelations, DataSetName);
                    if (Result != "")
                        return Result;
                }
            }
            return Result;
        }

        private InfoDataSet GetRootDataSetByBindingSource(InfoBindingSource aBindingSource)
        {
            if (aBindingSource.DataSource == null)
                return null;
            if (aBindingSource.DataSource is InfoDataSet)
                return aBindingSource.DataSource as InfoDataSet;
            return GetRootDataSetByBindingSource(aBindingSource.DataSource as InfoBindingSource);
        }

        private void btnSelectDataMember_Click(object sender, EventArgs e)
        {
            fmSelectProvider aForm = new fmSelectProvider();
            String ProviderName = "";
            if (aForm.ShowSelectProviderForm(FClientData.DataSetList, ref ProviderName))
            {
                ListViewItem ViewItem = (ListViewItem)((Button)sender).Tag;
                InfoBindingSource aBindingSource = FClientData.FindBindingSource(ViewItem.Text);
                InfoDataSet aDataSet = GetRootDataSetByBindingSource(aBindingSource);

                String DataSetName = ProviderName;
                String ModuleName = aDataSet.RemoteName.Substring(0, aDataSet.RemoteName.IndexOf('.'));
                String SolutionName = System.IO.Path.GetFileNameWithoutExtension(FDTE2.Solution.FullName);
                String TableName = CliUtils.GetTableName(ModuleName, DataSetName, SolutionName);
                TableName = WzdUtils.GetToken(ref TableName, new char[] { '.' });

                try
                {
                    aBindingSource.DataMember = GetRelationName(aDataSet, null, ProviderName);
                    aBindingSource.Tablename = TableName;
                    List<TBlockItem> aList = FClientData.FindBlockItemByDataSource(aBindingSource);
                    foreach (TBlockItem BlockItem in aList)
                    {
                        BlockItem.TableName = TableName;
                        BlockItem.ProviderName = ProviderName;
                    }
                }
                catch
                {
                    MessageBox.Show("While Setting DataMember occurs error, please check master BindingSource had been setup correctly. ");
                    return;
                }
                ViewItem.SubItems[1].Text = ProviderName;
            }
        }

        private void btnDeleteField_Click(object sender, EventArgs e)
        {
            TBlockItem BlockItem = null;
            if (tvContainer.SelectedNode == null)
                return;
            TNodeObject NodeObject = (TNodeObject)tvContainer.SelectedNode.Tag;
            BlockItem = NodeObject.BlockItem;
            foreach (ListViewItem aViewItem in lvSelectedFields.SelectedItems)
            {
                TBlockFieldItem FieldItem = BlockItem.BlockFieldItems.FindItem(aViewItem.Text);
                BlockItem.BlockFieldItems.Remove(FieldItem);
                if (aViewItem.SubItems.Count > 1 && aViewItem.SubItems[2].Tag != null)
                    ((Button)aViewItem.SubItems[2].Tag).Dispose();
                lvSelectedFields.Items.Remove(aViewItem);
            }
        }

        private void btnAddField_Click(object sender, EventArgs e)
        {
            TreeNode Node = tvContainer.SelectedNode;
            if (Node != null)
            {
                TNodeObject NodeObject = (TNodeObject)Node.Tag;
                fmSelTableField F = new fmSelTableField();
                if (F.ShowSelTableFieldForm(NodeObject.BlockItem, GetFieldNames, lvSelectedFields, InternalConnection, RearrangeRefValButton, btnRefVal_Click, FClientData.DatabaseType))
                {
                    btnDeleteField.Enabled = lvSelectedFields.Items.Count > 0;
                }
            }
        }

        private String GetTableNameByRelationName(InfoDataSet aDataSet, DataRelationCollection Relations, String RelationName)
        {
            String Result = "";
            if (aDataSet != null)
            {
                if (aDataSet.RealDataSet.Tables[0].TableName.CompareTo(RelationName) == 0)
                    Result = RelationName;
                if (Result == "")
                    Result = GetTableNameByRelationName(null, aDataSet.RealDataSet.Relations, RelationName);
            }
            else
            {
                foreach (DataRelation Relation in Relations)
                {
                    if (Relation.RelationName.CompareTo(RelationName) == 0)
                        return Relation.ChildTable.TableName;
                    Result = GetTableNameByRelationName(null, Relation.ChildTable.ChildRelations, RelationName);
                    if (Result != "")
                        return Result;
                }
            }
            return Result;

        }

        private String GetTableNameByBindingSource(InfoBindingSource aBindingSource)
        {
            if (aBindingSource.DataSource is InfoDataSet)
                return aBindingSource.DataMember;
            if (aBindingSource.DataSource is InfoBindingSource)
            {
                InfoDataSet aDataSet = GetRootDataSetByBindingSource(aBindingSource);
                String TableName = GetTableNameByRelationName(aDataSet, null, aBindingSource.DataMember);
                return TableName;
            }
            return "";
        }

        private void cbBindingSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tvContainer.SelectedNode == null || tvContainer.SelectedNode.Tag == null)
                return;
            TNodeObject NodeObject = (TNodeObject)tvContainer.SelectedNode.Tag;
            InfoBindingSource aInfoBindingSource = FClientData.FindBindingSource(cbBindingSource.Text);
            List<TBlockItem> aList = FClientData.FindBlockItemByDataSource(aInfoBindingSource);
            //NodeObject.BlockItem.BindingSource = FClientData.FindBindingSource(cbBindingSource.Text);
            if (aInfoBindingSource != null)
            {
                NodeObject.BlockItem.TableName = aInfoBindingSource.Tablename;
                if (aList.Count > 0)
                    NodeObject.BlockItem.ProviderName = aList[0].ProviderName;
                NodeObject.BlockItem.BindingSource = aInfoBindingSource;
            }
            else
            {
                //NodeObject.BlockItem.TableName = null;
                //NodeObject.BlockItem.ProviderName = null;
            }
        }

        private void cbViewBindingSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tvContainer.SelectedNode == null || tvContainer.SelectedNode.Tag == null)
                return;
            TNodeObject NodeObject = (TNodeObject)tvContainer.SelectedNode.Tag;
            NodeObject.BlockItem.ViewBindingSource = FClientData.FindBindingSource(cbViewBindingSource.Text);
        }

        private void lvSelectedFields_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewColumnSorter lvwColumnSorter = (sender as ListView).ListViewItemSorter as ListViewColumnSorter;

            // 检查点击的列是不是现在的排序列.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // 重新设置此列的排序方法.
                if (lvwColumnSorter.OrderOfSort == System.Windows.Forms.SortOrder.Ascending)
                {
                    lvwColumnSorter.OrderOfSort = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.OrderOfSort = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // 设置排序列，默认为正向排序
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.OrderOfSort = System.Windows.Forms.SortOrder.Ascending;
            }

            // 用新的排序方法对ListView排序
            (sender as ListView).Sort();
        }
    }

  	public class TExtClientData : Object
	{
        private string FPackageName, FBaseFormName, FServerPackageName, FOutputPath, FTableName, FFormName, FProviderName, 
            FDatabaseName, FSolutionName, FViewProviderName, FAssemblyOutputPath, FFormText;
        private TBlockItems FBlocks;
        private MWizard.fmExtClientWizard FOwner;
        private bool FNewSolution = false;
        private string FCodeOutputPath;
        private int FColumnCount;
        private ClientType FDatabaseType;
        private String FConnString;
        private String FLanguage = "cs";
        private String FTemplatePath;
        private List<InfoDataSet> FDataSetList = null;
        private List<InfoBindingSource> FBindingSourceList = null;

		public TExtClientData(MWizard.fmExtClientWizard Owner)
		{
			FOwner = Owner;
			FBlocks = new TBlockItems(this);
            FDataSetList = new List<InfoDataSet>();
            FBindingSourceList = new List<InfoBindingSource>();
		}

        public MWizard.fmExtClientWizard Owner
        {
            get { return FOwner; }
        }

        public ClientType DatabaseType
        {
            get { return FDatabaseType; }
            set { FDatabaseType = value; }
        }

        public String ConnString
        {
            get { return FConnString; }
            set { FConnString = value; }
        }

        public String Language
        {
            get { return FLanguage; }
            set { FLanguage = value; }
        }

        public String TemplatePath
        {
            get { return FTemplatePath; }
            set { FTemplatePath = value; }
        }

        public List<InfoDataSet> DataSetList
        {
            get { return FDataSetList; }
        }

        public List<InfoBindingSource> BindingSourceList
        {
            get { return FBindingSourceList; }
        }

        public InfoBindingSource FindBindingSource(String Name)
        {
            foreach (InfoBindingSource aBindingSource in FBindingSourceList)
                if (aBindingSource.Site.Name.CompareTo(Name) == 0)
                    return aBindingSource;
            return null;
        }

        public InfoDataSet FindDataSet(String Name)
        {
            foreach (InfoDataSet aDataSet in FDataSetList)
                if (aDataSet.Site.Name.CompareTo(Name) == 0)
                    return aDataSet;
            return null;
        }

        public List<TBlockItem> FindBlockItemByDataSource(InfoBindingSource DataSource)
        {
            List<TBlockItem> Result = new List<TBlockItem>();
            foreach (TBlockItem BlockItem in Blocks)
            {
                if (BlockItem.BindingSource == DataSource)
                    Result.Add(BlockItem);
            }
            return Result;
        }

        private void LoadBlockFieldItems(XmlNode Node, TBlockFieldItems BlockFieldItems)
        {
            TBlockFieldItem BFI;
            int I;
            XmlNode BlockFieldItemNode;
            for (I = 0; I < Node.ChildNodes.Count; I++)
            {
                BlockFieldItemNode = Node.ChildNodes[I];
                BFI = new TBlockFieldItem();
                BFI.DataField = BlockFieldItemNode.Attributes["DataField"].Value;
                BFI.Description = BlockFieldItemNode.Attributes["Description"].Value;
                BFI.Length = int.Parse(BlockFieldItemNode.Attributes["Length"].Value.ToString());
                BFI.EditMask = BlockFieldItemNode.Attributes["EditMaskType"].Value;
                foreach (XmlNode RefNode in BlockFieldItemNode.ChildNodes)
                { 
                    BFI.RefField = new TRefField();
                    BFI.RefField.SelectCommand = RefNode.Attributes["SelectCommand"].Value;
                    BFI.RefField.ValueMember = RefNode.Attributes["ValueMember"].Value;
                    BFI.RefField.DisplayMember = RefNode.Attributes["DisplayMember"].Value;
                    foreach (XmlNode ColumnNode in RefNode.ChildNodes)
                    {
                        RefColumns aColumn = new RefColumns();
                        aColumn.Column = ColumnNode.Attributes["Column"].Value;
                        aColumn.HeaderText = ColumnNode.Attributes["HeaderText"].Value;
                        aColumn.Width = int.Parse(ColumnNode.Attributes["Width"].Value);
                        BFI.RefField.LookupColumns.Add(aColumn);
                    }
                }
                //IPC保留缺口
                //BlockFieldItem.CheckNull = DR["CHECK_NULL"].ToString();
                //BlockFieldItem.DefaultValue = DR["DEFAULT_VALUE"].ToString();
                BlockFieldItems.Add(BFI);
            }
        }

        private void LoadBlocks(XmlNode Node)
        {
            int I;
            TBlockItem BI;
            XmlNode BlockNode, BlockFieldItemsNode;
            for (I = 0; I < Node.ChildNodes.Count; I++)
            {
                BlockNode = Node.ChildNodes[I];
                BI = new TBlockItem();
                BI.Name = BlockNode.Attributes["Name"].Value;
                BI.ProviderName = BlockNode.Attributes["ProviderName"].Value;
                BI.TableName = BlockNode.Attributes["TableName"].Value;
                BI.ParentItemName = BlockNode.Attributes["ParentItemName"].Value;
                BlockFieldItemsNode = WzdUtils.FindNode(null, BlockNode, "BlockFieldItems");
                LoadBlockFieldItems(BlockFieldItemsNode, BI.BlockFieldItems);
                Blocks.Add(BI);
            }
        }

        public void ResetDatabaseConnection()
        {
        }

        public object LoadFromXML(string XML)
        {
            System.Xml.XmlNode Node = null;
            System.Xml.XmlDocument Doc = new System.Xml.XmlDocument();
            Doc.LoadXml(XML);
            Node = Doc.SelectSingleNode("ClientData");
            SolutionName = Node.Attributes["SolutionName"].Value;
            OutputPath = Node.Attributes["OutputPath"].Value;
            CodeOutputPath = Node.Attributes["CodeOutputPath"].Value;
            NewSolution = Node.Attributes["NewSolution"].Value == "1";
            PackageName = Node.Attributes["PackageName"].Value;
            BaseFormName = Node.Attributes["BaseFormName"].Value;
            OutputPath = Node.Attributes["OutputPath"].Value;
            FormName = Node.Attributes["FormName"].Value;
            TableName = Node.Attributes["TableName"].Value;
            ProviderName = Node.Attributes["ProviderName"].Value;
            ColumnCount = Convert.ToInt16(Node.Attributes["ColumnCount"].Value);
            ViewProviderName = Node.Attributes["ViewProviderName"].Value;
            if (Node.Attributes["Language"].Value.ToString().CompareTo("C#") == 0)
                this.Language = "cs";
            else
                this.Language = "vb";
            Node = WzdUtils.FindNode(Doc, Node, "Blocks");
            LoadBlocks(Node);
            return null;
        }

		public bool IsMasterDetailBaseForm()
		{ 
			bool Result;
            Result = false;
			if (string.Compare(FBaseFormName, "CMasterDetail") == 0 ||
				string.Compare(FBaseFormName, "WorkFlowBase2") == 0 ||
                string.Compare(FBaseFormName, "VBCMasterDetail") == 0)
			{
				Result = true;
			}
			return Result;
   		}

        public bool NewSolution
        {
            get
            {
                return FNewSolution;
            }
            set
            {
                FNewSolution = value;
            }
        }

        public string SolutionName
        {
            get
            {
                return FSolutionName;
            }
            set
            {
                FSolutionName = value;
            }
        }

        public string ViewProviderName
        {
            get
            {
                return FViewProviderName;
            }
            set
            {
                FViewProviderName = value;
            }
        }

		public string DatabaseName
		{
			get
			{
				return FDatabaseName;
			}
			set
			{
				FDatabaseName = value;
			}
		}

        public string PackageName
		{
			get
			{
				return FPackageName;
			}
			set
			{
				FPackageName = value;
			}

		}

        public String AssemblyOutputPath
        {
            get { return FAssemblyOutputPath; }
            set { FAssemblyOutputPath = value; }
        }

        public String FormText
        {
            get { return FFormText; }
            set { FFormText = value; }
        }

		public string BaseFormName
		{
			get
			{
				return FBaseFormName;
			}
			set
			{
				FBaseFormName = value;
			}
		}

		public string ServerPackageName
		{
			get
			{
				return FServerPackageName;
			}
			set
			{
				FServerPackageName = value;
			}
		}

		public string OutputPath
		{
			get
			{
				return FOutputPath;
			}
			set
			{
				FOutputPath = value;
			}

		}

        public string CodeOutputPath
        {
            get
            {
                return FCodeOutputPath;
            }
            set
            {
                string S = value;
                if (S != "")
                    if (S[S.Length - 1] == '\\')
                        S = S.Substring(0, S.Length - 1);
                FCodeOutputPath = S;
            }
        }

        public int ColumnCount
        {
            get
            {
                return FColumnCount;
            }
            set
            {
                FColumnCount = value;
            }
        }

        public string TableName
		{
			get
			{
				return FTableName;
			}
			set
			{
				FTableName = value;
			}
		}

		public string FormName
		{
			get
			{
				return FFormName;
			}
			set
			{
				FFormName = value;
			}
		}

		public string ProviderName
		{
			get
			{
				return FProviderName;
			}
			set
			{
				FProviderName = value;
			}
		}

		public TBlockItems Blocks
		{
			get
			{
				return FBlocks;
			}
			set
			{
				FBlocks = value;
			}
		}
	}


	public class TExtClientGenerator : System.ComponentModel.Component
	{
		private TExtClientData FClientData;
		private DTE2 FDTE2;
		private System.Windows.Forms.Form FRootForm;
		private System.ComponentModel.Design.IDesignerHost FDesignerHost;
        private AddIn FAddIn;
        private ProjectItem GlobalPI;
        private Project GlobalProject;
        private Window GlobalWindow;
        private List<InfoDataSet> FRootInfoDataSetList = new List<InfoDataSet>();
        private List<InfoBindingSource> FBindingSourceList = new List<InfoBindingSource>();

		public TExtClientGenerator(TExtClientData ClientData, DTE2 dte2, AddIn aAddIn)
		{
			FClientData = ClientData;
			FDTE2 = dte2;
            FAddIn = aAddIn;
        }

        private void GenSolution()
		{
            Solution sln = FDTE2.Solution;
            string BaseFormProj = FClientData.BaseFormName + "\\" + FClientData.BaseFormName + "." + FClientData.Language + "proj";
            if (FClientData.NewSolution)
            {
                if (System.IO.Directory.Exists(FClientData.OutputPath))
                {
                    if (FClientData.OutputPath == "\\")
                        throw new Exception("Unknown Output Path: " + "\\");
                    System.IO.Directory.Delete(FClientData.OutputPath, true);
                }
                sln.Create(FClientData.OutputPath, FClientData.SolutionName);
                ProjectLoader.AddDefaultProject(FDTE2);
                Project P = sln.AddFromTemplate(FClientData.TemplatePath + BaseFormProj,
                    FClientData.CodeOutputPath + "\\" + FClientData.PackageName, FClientData.PackageName, false);
                    //FClientData.OutputPath + "\\" + FClientData.PackageName, FClientData.PackageName, true);
                P.Name = FClientData.PackageName;
                string FileName = FClientData.OutputPath + "\\" + FClientData.SolutionName + ".sln";
                sln.SaveAs(FileName);
                //sln.Open(FileName);
                sln.SolutionBuild.StartupProjects = P;
                sln.SolutionBuild.BuildProject(sln.SolutionBuild.ActiveConfiguration.Name, P.FullName, true);
                GlobalProject = P;
            }
            else
            {
                string FilePath = FClientData.CodeOutputPath + "\\" + FClientData.PackageName;
                //string FilePath = Path.GetDirectoryName(FClientData.SolutionName) + "\\" + FClientData.PackageName;
                if (System.IO.Directory.Exists(FilePath))
                {
                    if (FilePath == "\\")
                        throw new Exception("Unknown Output Path: " + "\\");
                    try
                    {
                        System.IO.Directory.Delete(FilePath, true);
                    }
                    catch
                    {
                        System.IO.Directory.Delete(FilePath, true);
                    }
                }
                Project P = sln.AddFromTemplate(FClientData.TemplatePath + BaseFormProj,
                    FilePath, FClientData.PackageName, true); 
                P.Name = FClientData.PackageName;
                string FileName = FilePath + "\\" + FClientData.PackageName + ". " + FClientData.Language + "proj";
                P.Save(FileName);
                sln.Open(FClientData.SolutionName);
                int I;
                P = null;
                for (I = 1; I <= sln.Projects.Count; I++)
                {
                    P = sln.Projects.Item(I);
                    if (string.Compare(P.Name, FClientData.PackageName) == 0)
                        break;
                    else
                        P = null;
                }
                if (P != null)
                    sln.Remove(P);
                P = sln.AddFromFile(FilePath + "\\" + FClientData.PackageName + "." + FClientData.Language + "proj", false);
                P.Properties.Item("RootNamespace").Value = FClientData.PackageName;
                sln.SaveAs(sln.FileName);
                sln.SolutionBuild.StartupProjects = P;
                sln.SolutionBuild.BuildProject(sln.SolutionBuild.ActiveConfiguration.Name, P.FullName, true);
                GlobalProject = P;
            }
            if (FClientData.AssemblyOutputPath != null && FClientData.AssemblyOutputPath != "")
                GlobalProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value = FClientData.AssemblyOutputPath;
		}

        private void RenameNameSpace(string FileName)
        {
            if (!File.Exists(FileName))
                return;
            System.IO.StreamReader SR = new System.IO.StreamReader(FileName);
            string Context = SR.ReadToEnd();
            SR.Close();
            Context = Context.Replace("TAG_NAMESPACE", FClientData.PackageName);
            Context = Context.Replace("TAG_FORMNAME", FClientData.FormName);
            System.IO.FileStream Filefs = new System.IO.FileStream(FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            System.IO.StreamWriter SW = new System.IO.StreamWriter(Filefs);
            SW.Write(Context);
            SW.Close();
            Filefs.Close();
        }

        private void GetForm()
		{
            Solution Sln = FDTE2.Solution;
            Project P = null;
            int I;
            for (I = 1; I <= Sln.Projects.Count; I++)
            {
                P = Sln.Projects.Item(I);
                if (string.Compare(FClientData.PackageName, P.Name) == 0)
                    break;
                P = null;
            }
            if (P == null)
                throw new Exception("Can not find project " + FClientData.PackageName + " in solution");
            ProjectItem PI;
            for (I = P.ProjectItems.Count; I >= 1; I--)
            {
                PI = P.ProjectItems.Item(I);
                if (string.Compare(PI.Name, "Form1." + FClientData.Language) == 0)
                {
                    string Path = PI.get_FileNames(0);
                    Path = System.IO.Path.GetDirectoryName(Path);
                    RenameNameSpace(Path + "\\Form1." + FClientData.Language);
                    RenameNameSpace(Path + "\\Form1.Designer." + FClientData.Language);
                    Window W = PI.Open("{00000000-0000-0000-0000-000000000000}");
                    W.Activate();
                    GlobalPI = PI;
                    GlobalWindow = W;
                    if (string.Compare(FClientData.FormName, "Form1") != 0)
                    {
                        PI.Name = FClientData.FormName + "." + FClientData.Language;
                        W.Close(vsSaveChanges.vsSaveChangesYes);
                        W = PI.Open("{00000000-0000-0000-0000-000000000000}");
                        W.Activate();
                    }
                    FDesignerHost = (IDesignerHost)W.Object;
                    FRootForm = (System.Windows.Forms.Form)FDesignerHost.RootComponent;
                    FRootForm.Name = FClientData.FormName;
                    FRootForm.Text = FClientData.FormText;
                    IComponentChangeService FComponentChangeService = (IComponentChangeService)FDesignerHost.RootComponent.Site.GetService(typeof(IComponentChangeService));
                }
                if (string.Compare(PI.Name, "Program." + FClientData.Language) == 0)
                {
                    RenameNameSpace(PI.get_FileNames(0));
                }
            }
		}

        private void AdjectLabelEditPos(TStringList EditList, TStringList LabelList)
		{
			int MaxLabelWidth = 0;
			Label label = null;
			Control textbox = null;
			for (int I = 0; I < LabelList.Count; I++)
			{
				label = (Label)LabelList[I];
				if (label.Width > MaxLabelWidth)
					MaxLabelWidth = label.Width;
			}
            if (MaxLabelWidth >= 105)
            {
                int EditOffSet = MaxLabelWidth - 105 + 5;

                for (int I = 0; I < EditList.Count; I++)
                {
                    textbox = (Control)EditList[I];
                    textbox.Left = 110 + EditOffSet;
                }

                for (int I = 0; I < LabelList.Count; I++)
                {
                    label = (Label)LabelList[I];
                    label.Left = 110 - label.Width - 5 + EditOffSet;
                }
            }
            if (EditList.Count == 0)
                return;
            int ColumnIndex = 0;
            int ColumnControlCount = (EditList.Count + (FClientData.ColumnCount - 1)) / FClientData.ColumnCount;
            int ColumnWidth = ((Control)EditList[0]).Left + ((Control)EditList[0]).Width;
            int TopOffset = 10;
            int ColumnControlIndex = 0;

            for (int I = 0; I < EditList.Count; I++)
            {
                if (I % ColumnControlCount == 0)
                {
                    if (I+1 >= ColumnControlCount)
                    {
                        ColumnControlIndex = 0;
                        ColumnIndex++;
                    }
                }
                label = (Label)LabelList[I];
                textbox = (Control)EditList[I];
                textbox.Left = textbox.Left + ColumnWidth * ColumnIndex;
                textbox.Top = TopOffset + (textbox.Height + 5) * ColumnControlIndex;
                label.Left = label.Left + ColumnWidth * ColumnIndex;
                label.Top = textbox.Top + (textbox.Height - label.Height) / 2;
                ColumnControlIndex++;
            }
		}

        private InfoRefVal GenRefVal(TBlockFieldItem FieldItem, string TableName)
        {
            String Name = "rv" + TableName + FieldItem.DataField;
            InfoRefVal Result = FDesignerHost.CreateComponent(typeof(InfoRefVal), Name) as InfoRefVal;
            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
            //aInfoCommand.Connection = FClientData.Owner.GlobalConnection;
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet aDataSet = new DataSet();
            //SYS_REFVAL
            aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", FieldItem.RefValNo);
            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, FieldItem.RefValNo);
            if (aDataSet.Tables[0].Rows.Count != 1)
                throw new Exception(String.Format("Unknown REFVAL_NO in SYS_REFVAL: {0}", FieldItem.RefValNo));
            Result.Caption = aDataSet.Tables[0].Rows[0]["CAPTION"].ToString();
            Result.DisplayMember = aDataSet.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
            Result.ValueMember = aDataSet.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
            Result.SelectAlias = aDataSet.Tables[0].Rows[0]["SELECT_ALIAS"].ToString();
            Result.SelectCommand = aDataSet.Tables[0].Rows[0]["SELECT_COMMAND"].ToString();
            //SYS_REFVSL_D1 --> Columns
            aDataSet.Clear();
            aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL_D1 where REFVAL_NO = '{0}'", FieldItem.RefValNo);
            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, FieldItem.RefValNo);
            foreach (DataRow DR in aDataSet.Tables[0].Rows)
            {
                RefColumns RC = new RefColumns();
                RC.Column = DR["FIELD_NAME"].ToString();
                RC.HeaderText = DR["HEADER_TEXT"].ToString();
                Result.Columns.Add(RC);
            }
            return Result;
        }

        private void CreateQueryField(TBlockFieldItem aFieldItem, String Range, InfoRefVal aRefVal, InfoBindingSource ibs)
        {
            if (aFieldItem.QueryMode == null)
                return;
            InfoNavigator navigator1 = FRootForm.Controls["infoNavigator1"] as InfoNavigator;
            if (navigator1 != null)
            {
                if (aFieldItem.QueryMode.ToUpper() == "NORMAL" || aFieldItem.QueryMode.ToUpper() == "RANGE")
                {
                    QueryField qField = new QueryField();
                    qField.FieldName = aFieldItem.DataField;
                    qField.Caption = aFieldItem.Description;
                    if (qField.Caption == "")
                        qField.Caption = aFieldItem.DataField;
                    if (aFieldItem.QueryMode.ToUpper() == "NORMAL")
                    {
                        if (aFieldItem.DataType == typeof(DateTime))
                            qField.Condition = "=";
                        if (aFieldItem.DataType == typeof(int) || aFieldItem.DataType == typeof(float) ||
                            aFieldItem.DataType == typeof(double) || aFieldItem.DataType == typeof(Int16))
                            qField.Condition = "=";
                        if (aFieldItem.DataType == typeof(String))
                            qField.Condition = "%";
                    }
                    if (aFieldItem.QueryMode.ToUpper() == "RANGE")
                    {
                        qField.Condition = "And";
                        if (Range == "")
                        {
                            qField.Condition = "<=";
                            CreateQueryField(aFieldItem, ">=", aRefVal, null);
                        }
                        else
                        {
                            qField.Condition = Range;
                        }
                    }
                    switch (aFieldItem.ControlType.ToUpper())
                    {
                        case "TEXTBOX":
                            qField.Mode = "TextBox";
                            break;
                        case "COMBOBOX":
                            qField.Mode = "ComboBox";
                            qField.RefVal = aRefVal;
                            break;
                        case "REFVALBOX":
                            qField.Mode = "RefVal";
                            qField.RefVal = aRefVal;
                            break;
                        case "DATETIMEBOX":
                            qField.Mode = "Calendar";
                            break;
                    }
                    navigator1.QueryFields.Add(qField);
                }
            }

            ClientQuery aClientQuery = FRootForm.Container.Components["clientQuery1"] as ClientQuery;
            if (aClientQuery != null)
            {
                if (ibs != null && aClientQuery != null)
                    aClientQuery.BindingSource = ibs;
                if (aFieldItem.QueryMode.ToUpper() == "NORMAL" || aFieldItem.QueryMode.ToUpper() == "RANGE")
                {
                    QueryColumns qColumn = new QueryColumns();
                    qColumn.Column = aFieldItem.DataField;
                    qColumn.Caption = aFieldItem.Description;
                    if (qColumn.Caption == "")
                        qColumn.Caption = aFieldItem.DataField;
                    if (aFieldItem.QueryMode.ToUpper() == "NORMAL")
                    {
                        if (aFieldItem.DataType == typeof(DateTime))
                            qColumn.Operator = "=";
                        if (aFieldItem.DataType == typeof(int) || aFieldItem.DataType == typeof(float) ||
                            aFieldItem.DataType == typeof(double) || aFieldItem.DataType == typeof(Int16))
                            qColumn.Operator = "=";
                        if (aFieldItem.DataType == typeof(String))
                            qColumn.Operator = "%";
                    }
                    if (aFieldItem.QueryMode.ToUpper() == "RANGE")
                    {
                        qColumn.Condition = "And";
                        if (Range == "")
                        {
                            qColumn.Operator = "<=";
                            CreateQueryField(aFieldItem, ">=", aRefVal, null);
                        }
                        else
                        {
                            qColumn.Operator = Range;
                            //qColumn.Condition = Range;
                        }
                    }
                    switch (aFieldItem.ControlType.ToUpper())
                    {
                        case "TEXTBOX":
                            qColumn.ColumnType = "ClientQueryTextBoxColumn";
                            break;
                        case "COMBOBOX":
                            qColumn.ColumnType = "ClientQueryComboBoxColumn";
                            qColumn.InfoRefVal = aRefVal;
                            break;
                        case "REFVALBOX":
                            qColumn.ColumnType = "ClientQueryRefValColumn";
                            qColumn.InfoRefVal = aRefVal;
                            break;
                        case "DATETIMEBOX":
                            qColumn.ColumnType = "ClientQueryCalendarColumn";
                            break;
                        case "CHECKBOX":
                            qColumn.ColumnType = "ClientQueryCheckBoxColumn";
                            break;
                    }
                    aClientQuery.Columns.Add(qColumn);
                }
            }
        }

        private InfoDataSet GetRootDataSet(Object DataSource)
        { 
            if (DataSource.GetType().Equals(typeof(InfoDataSet)))
                return (InfoDataSet)DataSource;
            if (DataSource.GetType().Equals(typeof(InfoBindingSource)))
                return GetRootDataSet(((InfoBindingSource)DataSource).DataSource);
            return null;
        }

        private DataTable FindDataTable(InfoBindingSource aBindingSource, String innerDataSetName)
        {
            InfoDataSet aDataSet = GetRootDataSet(aBindingSource);
            foreach (DataTable aTable in aDataSet.RealDataSet.Tables)
            {
                if (aTable.TableName.CompareTo(innerDataSetName) == 0)
                    return aTable;
                //if (aTable.TableName.CompareTo(aBindingSource.Tablename) == 0)
                //    return aTable;
            }
            return null;
        }

        private Control FindContainer(Control ParentControl, String ControlName)
        {
            int Index = ControlName.IndexOf(".");
            if (Index > -1)
            { 
                String ParentName = ControlName.Substring(0, Index);
                ControlName = ControlName.Substring(Index + 1, ControlName.Length - Index - 1);
                ParentControl = FindComponent(FRootForm, ParentName, typeof(SplitContainer)) as Control;
            }
            if (ParentControl.Name.CompareTo(ControlName) == 0)
                return ParentControl;
            Control Result = null;
            Result = ParentControl.Controls[ControlName];
            if (Result == null)
            {
                foreach (Control ChildControl in ParentControl.Controls)
                {
                    Result = FindContainer(ChildControl, ControlName);
                    if (Result != null)
                        return Result;
                }
            }
            return Result;
        }

        private void GenTextBoxControl(TBlockItem BlockItem)
		{
            DataTable aTable = FindDataTable(BlockItem.BindingSource, BlockItem.ProviderName);

			TStringList aLabelList = new TStringList();
			TStringList aEditList = new TStringList();
			TBlockFieldItem aFieldItem;
			System.Windows.Forms.Label l = null;
            int TopOffset = 10;
			int LeftOffst = 100;
            InfoTextBox aInfoTextBox = null;
            InfoRefvalBox aInfoRefValBox = null;
            InfoDateTimePicker aInfoDateTimePicker = null;
            InfoRefVal aRefVal = null;
            InfoComboBox aComboBox = null;
            CheckBox aCheckBox = null;

            for (int I = 0; I < BlockItem.BlockFieldItems.Count; I++)
			{
				aFieldItem = BlockItem.BlockFieldItems[I] as TBlockFieldItem;
                aInfoTextBox = null;
                aInfoRefValBox = null;
                aInfoDateTimePicker = null;
                aRefVal = null;
                aComboBox = null;
                aCheckBox = null;
                
                if ((aFieldItem.RefValNo != null && aFieldItem.RefValNo != "") || aFieldItem.RefField != null)
                {
                    aRefVal = GenRefVal(aFieldItem, BlockItem.ProviderName);
                    aInfoRefValBox = FDesignerHost.CreateComponent(typeof(InfoRefvalBox)) as InfoRefvalBox;
                    aInfoRefValBox.Parent = FindContainer(FRootForm, BlockItem.ContainerName);
                    aInfoRefValBox.Top = TopOffset + (aInfoRefValBox.Height + 5) * I;
                    aInfoRefValBox.Left = LeftOffst;
                    aInfoRefValBox.Width = 150;
                    aInfoRefValBox.Text = aFieldItem.DataField;
                    //aInfoRefValBox.Name = "tb" + aFieldItem.DataField;
                    aInfoRefValBox.Name = aFieldItem.DataField + "InfoRefValBox";
                    aInfoRefValBox.DataBindings.Add(new Binding("Text", BlockItem.BindingSource, aFieldItem.DataField, true));
                    aInfoRefValBox.DataBindings[0].FormatString = aFieldItem.EditMask;
                    aInfoRefValBox.RefVal = aRefVal;
                    aInfoRefValBox.TextBoxBindingSource = BlockItem.BindingSource;
                    aInfoRefValBox.TextBoxBindingMember = aFieldItem.DataField;
                    //aInfoRefValBox.MaxLength = FieldItem.Length;
                    aEditList.Add(aInfoRefValBox);
                }
                else if (aFieldItem.ControlType == "ComboBox")
                {
                    string type = FindSystemDBType("SystemDB");

                    aComboBox = FDesignerHost.CreateComponent(typeof(InfoComboBox)) as InfoComboBox;
                    //aComboBox.Name = "icb" + aFieldItem.DataField;
                    aComboBox.Name = aFieldItem.DataField + "ComboBox";
                    aComboBox.Parent = FindContainer(FRootForm, BlockItem.ContainerName);
                    aComboBox.Top = TopOffset + (aComboBox.Height + 5) * I;
                    aComboBox.Left = LeftOffst;
                    aComboBox.Width = 150;
                    aComboBox.SelectAlias = FClientData.Owner.SelectedAlias;
                    if (aFieldItem.ComboEntityName != null && aFieldItem.ComboTextField != null && aFieldItem.ComboValueField != null)
                    {
                        if (type == "1")
                            aComboBox.SelectCommand = String.Format("Select [{0}].[{1}], [{0}].[{2}] from [{0}]", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);
                        else if (type == "2")
                            aComboBox.SelectCommand = String.Format("Select [{0}].[{1}], [{0}].[{2}] from [{0}]", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);
                        else if (type == "3")
                            aComboBox.SelectCommand = String.Format("Select {0}.{1}, {0}.{2} from {0}", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);
                        else if (type == "4")
                            aComboBox.SelectCommand = String.Format("Select {0}.{1}, {0}.{2} from {0}", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);
                        else if (type == "5")
                            aComboBox.SelectCommand = String.Format("Select {0}.{1}, {0}.{2} from {0}", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);
                        else if (type == "6")
                            aComboBox.SelectCommand = String.Format("Select {0}.{1}, {0}.{2} from {0}", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);
                        else if (type == "7")
                            aComboBox.SelectCommand = String.Format("Select {0}.{1}, {0}.{2} from {0}", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);
                    }
                    aComboBox.DisplayMember = aFieldItem.ComboTextField;
                    aComboBox.ValueMember = aFieldItem.ComboValueField;
                    aComboBox.DataBindings.Add(new Binding("Text", BlockItem.BindingSource, aFieldItem.DataField, true));
                    //if (dsColdef.Tables[0].Rows.Count > 0)
                    //    for (int j = 0; j < dsColdef.Tables[0].Rows.Count; j++)
                    //        if (dsColdef.Tables[0].Rows[j]["FIELD_NAME"].ToString() == aFieldItem.DataField && dsColdef.Tables[0].Rows[j]["EDITMASK"] != null)
                    //            aComboBox.DataBindings[0].FormatString = dsColdef.Tables[0].Rows[j]["EDITMASK"].ToString();
                    aComboBox.DataBindings[0].FormatString = aFieldItem.EditMask;
                    aEditList.Add(aComboBox);
                }
                else if (aFieldItem.ControlType == "CheckBox")
                {
                    aCheckBox = FDesignerHost.CreateComponent(typeof(CheckBox)) as CheckBox;
                    aCheckBox.Name = aFieldItem.DataField + "CheckBox";
                    aCheckBox.Site.Name = aFieldItem.DataField + "CheckBox";
                    aCheckBox.Parent = FindContainer(FRootForm, BlockItem.ContainerName);
                    aCheckBox.Height = 22;
                    aCheckBox.Top = TopOffset + (aCheckBox.Height + 5) * I;
                    aCheckBox.Left = LeftOffst;
                    aCheckBox.Width = 150;
                    aCheckBox.DataBindings.Add(new Binding("Checked", BlockItem.BindingSource, aFieldItem.DataField, true));
                    aEditList.Add(aCheckBox);
                }
                else
                {
                    if (aTable.Columns[aFieldItem.DataField].DataType == typeof(DateTime) || (aFieldItem.ControlType != null && aFieldItem.ControlType.ToUpper() == "DATETIMEBOX"))
                    {
                        aInfoDateTimePicker = FDesignerHost.CreateComponent(typeof(InfoDateTimePicker)) as InfoDateTimePicker;
                        aInfoDateTimePicker.Parent = FindContainer(FRootForm, BlockItem.ContainerName);
                        aInfoDateTimePicker.Top = TopOffset + (aInfoDateTimePicker.Height + 5) * I;
                        aInfoDateTimePicker.Left = LeftOffst;
                        aInfoDateTimePicker.Width = 150;
                        //aInfoDateTimePicker.Name = "dtp" + aFieldItem.DataField;
                        aInfoDateTimePicker.Site.Name = aFieldItem.DataField + "InfoDateTimePicker";
                        aInfoDateTimePicker.DataBindings.Add(new Binding("Text", BlockItem.BindingSource, aFieldItem.DataField, true));
                        //if (dsColdef.Tables[0].Rows.Count > 0)
                        //    for (int j = 0; j < dsColdef.Tables[0].Rows.Count; j++)
                        //        if (dsColdef.Tables[0].Rows[j]["FIELD_NAME"].ToString() == aFieldItem.DataField && dsColdef.Tables[0].Rows[j]["EDITMASK"] != null)
                        //            aInfoDateTimePicker.DataBindings[0].FormatString = dsColdef.Tables[0].Rows[j]["EDITMASK"].ToString();
                        aInfoDateTimePicker.DataBindings[0].FormatString = aFieldItem.EditMask;
                        aEditList.Add(aInfoDateTimePicker);
                    }
                    else
                    {
                        aInfoTextBox = FDesignerHost.CreateComponent(typeof(InfoTextBox)) as InfoTextBox;
                        aInfoTextBox.Parent = FindContainer(FRootForm, BlockItem.ContainerName);
                        aInfoTextBox.Top = TopOffset + (aInfoTextBox.Height + 5) * I;
                        aInfoTextBox.Left = LeftOffst;
                        aInfoTextBox.Width = 150;
                        aInfoTextBox.Text = aFieldItem.DataField;
                        //aInfoTextBox.Name = "tb" + aFieldItem.DataField;
                        aInfoTextBox.Site.Name = aFieldItem.DataField + "InfoTextBox";
                        aInfoTextBox.DataBindings.Add(new Binding("Text", BlockItem.BindingSource, aFieldItem.DataField, true));
                        //if (dsColdef.Tables[0].Rows.Count > 0)
                        //    for (int j = 0; j < dsColdef.Tables[0].Rows.Count; j++)
                        //        if (dsColdef.Tables[0].Rows[j]["FIELD_NAME"].ToString() == aFieldItem.DataField && dsColdef.Tables[0].Rows[j]["EDITMASK"] != null)
                        //            aInfoTextBox.DataBindings[0].FormatString = dsColdef.Tables[0].Rows[j]["EDITMASK"].ToString();
                        aInfoTextBox.DataBindings[0].FormatString = aFieldItem.EditMask;
                        aInfoTextBox.MaxLength = aFieldItem.Length;
                        aEditList.Add(aInfoTextBox);
                    }
                }

                CreateQueryField(aFieldItem, "", aRefVal, BlockItem.BindingSource);

				l = FDesignerHost.CreateComponent(typeof(System.Windows.Forms.Label)) as Label;
                l.Parent = FindContainer(FRootForm, BlockItem.ContainerName);
				l.AutoSize = true;
				l.Text = aFieldItem.Description;
                if (l.Text == "")
                    l.Text = aFieldItem.DataField;
                l.Left = LeftOffst - l.Width - 5;
                if (aInfoTextBox != null)
                {
                    l.Top = aInfoTextBox.Top + (aInfoTextBox.Height - l.Height) / 2;
                }
                if (aInfoDateTimePicker != null)
                {
                    l.Top = aInfoDateTimePicker.Top + (aInfoDateTimePicker.Height - l.Height) / 2;
                }
				aLabelList.Add(l);
			}
            AdjectLabelEditPos(aEditList, aLabelList);
        }

        public String FindSystemDBType(String aliasName)
        {
            String xmlName = SystemFile.DBFile;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlName);

            XmlNode node = xmlDoc.FirstChild.SelectSingleNode(aliasName);

            string DbString = node.FirstChild.Value;
            string systemDBType = FindDBType(DbString);

            return systemDBType;
        }

        public String FindDBType(String aliasName)
        {
            String xmlName = SystemFile.DBFile;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlName);

            XmlNode node = xmlDoc.FirstChild.FirstChild.SelectSingleNode(aliasName);

            string DbType = node.Attributes["Type"].Value.Trim();
            return DbType;
        }

		private void GenPanelBlock(TBlockItem BlockItem)
		{
            switch (BlockItem.LayoutKind)
            {
                case TLayoutType.ltTextBox:
                    {
                        GenTextBoxControl(BlockItem);
                        break;
                    }
                case TLayoutType.ltDataGridView:
                    {
                        GenDataGridViewControl(BlockItem);
                        break;
                    }
            }
        }

        private Object FindComponent(Form OwnerForm, String Name, Type ComponentType)
        {
            if (ComponentType == typeof(Control))
            {
                return FindContainer(OwnerForm, Name);
            }
            else
            {
                foreach (Object Obj in OwnerForm.Container.Components)
                    if (Obj is Component)
                    {
                        Component C = Obj as Component;
                        if (C.Site != null)
                            if (C.Site.Name.CompareTo(Name) == 0)
                                return C;
                    }
            }
            return null;
        }

        private void GenDataSet()
        {
            foreach (InfoDataSet aDataSet in FClientData.DataSetList)
            {
                InfoDataSet bDataSet = FindComponent(FRootForm, aDataSet.Site.Name, typeof(InfoDataSet)) as InfoDataSet;
                if (bDataSet != null)
                {
                    bDataSet.RemoteName = aDataSet.RemoteName;
                    bDataSet.Active = true;
                }
            }
            foreach (InfoBindingSource aBindingSource in FClientData.BindingSourceList)
            {
                InfoBindingSource bBindingSource = FindComponent(FRootForm, aBindingSource.Site.Name, typeof(InfoBindingSource)) as InfoBindingSource;
                if (bBindingSource != null)
                {
                    try
                    {
                        bBindingSource.DataMember = aBindingSource.DataMember;
                    }
                    catch { }
                    bBindingSource.Tablename = aBindingSource.Tablename;
                    if (bBindingSource.Relations.Count > 0)
                    {
                        foreach (DataColumn dc in bBindingSource.Relations[0].RelationDataSet.RealDataSet.Tables[0].PrimaryKey)
                        {
                            InfoKeyField ikfSourceKeyFields = new InfoKeyField();
                            ikfSourceKeyFields.FieldName = dc.ColumnName;
                            InfoKeyField ikfTargetKeyFields = new InfoKeyField();
                            ikfTargetKeyFields.FieldName = dc.ColumnName;

                            bBindingSource.Relations[0].SourceKeyFields.Add(ikfSourceKeyFields);
                            bBindingSource.Relations[0].TargetKeyFields.Add(ikfTargetKeyFields);
                        }
                    }
                    foreach (TBlockItem BlockItem in FClientData.Blocks)
                    {
                        if (BlockItem.BindingSource == aBindingSource)
                        {
                            BlockItem.BindingSource = bBindingSource;
                        }
                        if (BlockItem.ViewBindingSource == aBindingSource)
                        {
                            BlockItem.ViewBindingSource = bBindingSource;
                        }
                    }
                }
            }
        }

        private void GenRelationBindingSource(DataRelationCollection Relations, Object DataSource)
        {
            foreach (DataRelation Relation in Relations)
            {
                InfoBindingSource aBindingSource = FDesignerHost.CreateComponent(typeof(InfoBindingSource),
                    "ibs" + Relation.ChildTable.TableName) as InfoBindingSource;
                aBindingSource.DataSource = DataSource;
                aBindingSource.DataMember = Relation.RelationName;
                aBindingSource.text = "ibs" + Relation.ChildTable.TableName;
                aBindingSource.Tablename = Relation.ChildTable.TableName;
                FBindingSourceList.Add(aBindingSource);
                GenRelationBindingSource(Relation.ChildTable.ChildRelations, aBindingSource);
            }
        }

        private void GenDataGridViewControl(TBlockItem BlockItem)
        {
            InfoDataGridView Grid = null;
            Control aControl = FindContainer(FRootForm, BlockItem.ContainerName);
            if (!(aControl is InfoDataGridView))
            {
                Grid = FDesignerHost.CreateComponent(typeof(InfoDataGridView), "grd" + BlockItem.ProviderName) as InfoDataGridView;
                Grid.Parent = aControl;
                Grid.Dock = DockStyle.Fill;
            }
            else
                Grid = (InfoDataGridView)aControl;
            Grid.DataSource = BlockItem.BindingSource;
            Grid.Columns.Clear();
            TBlockFieldItem aFieldItem;
            DataGridViewTextBoxColumn Column;
            InfoDataGridViewComboBoxColumn ComboBoxColumn;
            InfoDataGridViewRefValColumn RefValColumn;
            int I;
            for (I = 0; I < BlockItem.BlockFieldItems.Count; I++)
            {
                aFieldItem = BlockItem.BlockFieldItems[I] as TBlockFieldItem;
                if ((aFieldItem.RefValNo != null && aFieldItem.RefValNo != "") || aFieldItem.RefField != null)
                {
                    InfoRefVal aInfoRefVal = GenRefVal(aFieldItem, BlockItem.ProviderName);
                    RefValColumn = FDesignerHost.CreateComponent(typeof(InfoDataGridViewRefValColumn), "dgc" + BlockItem.ProviderName + aFieldItem.DataField) as InfoDataGridViewRefValColumn;
                    RefValColumn.DataPropertyName = aFieldItem.DataField;
                    RefValColumn.DefaultCellStyle.Format = aFieldItem.EditMask;
                    RefValColumn.HeaderText = aFieldItem.Description;
                    RefValColumn.MaxInputLength = aFieldItem.Length;
                    if (RefValColumn.HeaderText.Trim() == "")
                        RefValColumn.HeaderText = aFieldItem.DataField;
                    RefValColumn.RefValue = aInfoRefVal;
                    Grid.Columns.Add(RefValColumn);
                }
                else if (aFieldItem.ControlType == "ComboBox")
                {
                    string type = FindSystemDBType("SystemDB");
                    
                    InfoRefVal bInfoRefVal = FDesignerHost.CreateComponent(typeof(InfoRefVal), "irv" + BlockItem.ProviderName + aFieldItem.DataField) as InfoRefVal;
                    bInfoRefVal.SelectAlias = FClientData.Owner.SelectedAlias;
                    if (type == "1")
                        bInfoRefVal.SelectCommand = String.Format("Select [{0}].[{1}], [{0}].[{2}] from [{0}]", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);
                    else if (type == "2")
                        bInfoRefVal.SelectCommand = String.Format("Select [{0}].[{1}], [{0}].[{2}] from [{0}]", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);
                    else if (type == "3")
                        bInfoRefVal.SelectCommand = String.Format("Select {0}.{1}, {0}.{2} from {0}", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);
                    else if (type == "4")
                        bInfoRefVal.SelectCommand = String.Format("Select {0}.{1}, {0}.{2} from {0}", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);
                    else if (type == "5")
                        bInfoRefVal.SelectCommand = String.Format("Select {0}.{1}, {0}.{2} from {0}", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);
                    else if (type == "6")
                        bInfoRefVal.SelectCommand = String.Format("Select {0}.{1}, {0}.{2} from {0}", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);
                    else if (type == "7")
                        bInfoRefVal.SelectCommand = String.Format("Select {0}.{1}, {0}.{2} from {0}", aFieldItem.ComboEntityName, aFieldItem.ComboTextField, aFieldItem.ComboValueField);

                    bInfoRefVal.ValueMember = aFieldItem.ComboValueField;
                    bInfoRefVal.DisplayMember = aFieldItem.ComboTextField;
                    ComboBoxColumn = FDesignerHost.CreateComponent(typeof(InfoDataGridViewComboBoxColumn), "dgcc" + BlockItem.ProviderName + aFieldItem.DataField) as InfoDataGridViewComboBoxColumn;
                    ComboBoxColumn.RefValue = bInfoRefVal;
                    ComboBoxColumn.DataPropertyName = aFieldItem.DataField;
                    ComboBoxColumn.DefaultCellStyle.Format = aFieldItem.EditMask;
                    ComboBoxColumn.HeaderText = aFieldItem.Description;
                    if (ComboBoxColumn.HeaderText.Trim() == "")
                        ComboBoxColumn.HeaderText = aFieldItem.DataField;
                    ComboBoxColumn.DisplayMember = aFieldItem.ComboTextField;
                    ComboBoxColumn.ValueMember = aFieldItem.ComboValueField;
                    Grid.Columns.Add(ComboBoxColumn);
                }
                else
                {
                    Column = FDesignerHost.CreateComponent(typeof(DataGridViewTextBoxColumn), "dgc" + BlockItem.ProviderName + aFieldItem.DataField) as DataGridViewTextBoxColumn;
                    Column.DataPropertyName = aFieldItem.DataField;
                    Column.DefaultCellStyle.Format = aFieldItem.EditMask;
                    Column.HeaderText = aFieldItem.Description;
                    Column.MaxInputLength = aFieldItem.Length;
                    if (Column.HeaderText.Trim() == "")
                        Column.HeaderText = aFieldItem.DataField;
                    Grid.Columns.Add(Column);
                }
            }
        }

        private void RenameForm()
        {
            string Path = GlobalPI.get_FileNames(0);
            Path = System.IO.Path.GetDirectoryName(Path);
            string NewName = FClientData.FormName + ".cs";
            string FileName = Path + @"\" + NewName;
            GlobalPI.SaveAs(FileName);
            System.IO.File.Delete(Path + @"\Form1.cs");
            System.IO.File.Delete(Path + @"\Form1.Designer.cs");
            System.IO.File.Delete(Path + @"\Form1.resx");
        }

        public void GenDefaultValidate()
        {
            foreach (TBlockItem BlockItem in FClientData.Blocks)
            {
                if (String.Compare(BlockItem.Name, "View") == 0)
                    continue;

                DefaultValidate aValidate = null;
                int count = 1;
                foreach (TBlockFieldItem aFieldItem in BlockItem.BlockFieldItems)
                {
                    if (aFieldItem.CheckNull != null && aFieldItem.CheckNull.ToUpper() == "Y" || (aFieldItem.DefaultValue != "" && aFieldItem.DefaultValue != null))
                    {
                        if (aValidate == null)
                        {
                            aValidate = FDesignerHost.CreateComponent(typeof(DefaultValidate), "dv" + BlockItem.ProviderName) as DefaultValidate;
                            aValidate.BindingSource = BlockItem.BindingSource;
                        }
                        FieldItem FI = new FieldItem();
                        FI.FieldName = aFieldItem.DataField;
                        FI.CheckNull = aFieldItem.CheckNull.ToUpper() == "Y";
                        FI.DefaultValue = aFieldItem.DefaultValue;
                        if (!aValidate.BindingSource.Site.Name.EndsWith("Details"))
                            FI.ValidateLabelLink = "label" + count;
                        aValidate.FieldItems.Add(FI);
                    }
                    count++;
                }
            }
        }

        private InfoBindingSource FindBindingSource(TBlockItem BlockItem)
        {
            foreach (InfoBindingSource BindingSource in FBindingSourceList)
            {
                if (BindingSource.text.CompareTo("ibs" + BlockItem.ProviderName) == 0)
                {
                    BlockItem.BindingSource = BindingSource;
                    return BindingSource;
                }
            }
            return null;
        }

        private InfoBindingSource FindViewBindingSource(TBlockItem BlockItem)
        {
            foreach (InfoBindingSource ViewBindingSource in FBindingSourceList)
            {
                if (ViewBindingSource.text.CompareTo("ibsView" + BlockItem.ProviderName) == 0)
                {
                    BlockItem.ViewBindingSource = ViewBindingSource;
                    return ViewBindingSource;
                }
            }
            return null;
        }

        private void SetNavigatorBindingSource(TBlockItem BlockItem)
        {
            //InfoNavigator Navigator = (InfoNavigator)BlockItem.ContainerControl;
            InfoNavigator Navigator = FindComponent(FRootForm, (BlockItem.ContainerControl as InfoNavigator).AnyQueryID, typeof(InfoNavigator)) as InfoNavigator;
            Navigator.BindingSource = FindBindingSource(BlockItem);
            if (Navigator.BindingSource == null)
                Navigator.BindingSource = BlockItem.BindingSource;
            Navigator.ViewBindingSource = FindBindingSource(BlockItem);
            if(Navigator.ViewBindingSource == null)
                Navigator.ViewBindingSource = BlockItem.ViewBindingSource;
        }

        //private void GenTabPageControl(TBlockItem BlockItem)
        //{
        //    TabControl control1 = (TabControl)FindContainer(FRootForm, BlockItem.ContainerName);
        //    TabPage page1 = FDesignerHost.CreateComponent(typeof(TabPage)) as TabPage;
        //    page1.Text = BlockItem.TableName;
        //    page1.Name = "tp" + BlockItem.ProviderName;
        //    BlockItem.ContainerName = page1.Name;
        //    control1.TabPages.Add(page1);
        //    GenPanelBlock(BlockItem);
        //}

        private void GenBlockControl()
        {
            foreach (TBlockItem BlockItem in FClientData.Blocks)
            {
                if (BlockItem.ContainerControl != null && BlockItem.ProviderName != null && BlockItem.ProviderName != "")
                {
                    if (BlockItem.ContainerControl is InfoNavigator)
                    {
                        SetNavigatorBindingSource(BlockItem);
                    }
                    if (BlockItem.ContainerControl is Panel || BlockItem.ContainerControl is TabPage ||
                        BlockItem.ContainerControl is Form)
                    {
                        GenPanelBlock(BlockItem);
                    }
                    if (BlockItem.ContainerControl is InfoDataGridView ||
                        BlockItem.ContainerControl is DataGridView ||
                        BlockItem.ContainerControl is DataGrid)
                    {
                        GenDataGridViewControl(BlockItem);
                    }
                    //if (BlockItem.ContainerControl.GetType().Equals(typeof(TabControl)) ||
                    //    BlockItem.ContainerControl.GetType().IsSubclassOf(typeof(TabControl)))
                    //{
                    //    GenTabPageControl(BlockItem);
                    //}
                }
            }
        }

        public void GenClientModule()
		{
            GenSolution();
            GetForm();
            DesignerTransaction transaction1 = FDesignerHost.CreateTransaction();
            try
            {
                GenDataSet();
                GenBlockControl();
                GenDefaultValidate();
                GlobalProject.Save(GlobalProject.FullName);
                FDTE2.Solution.SolutionBuild.BuildProject(FDTE2.Solution.SolutionBuild.ActiveConfiguration.Name,
                    GlobalProject.FullName, true);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message);
                return;
            }
            finally
            {
                transaction1.Commit();
            }
        }
    }

    public class TNodeObject
    {
        public TNodeObject(TreeNode aTreeNode, TExtWebClientData ClientData)
        {
            Node = aTreeNode;
            aTreeNode.Tag = this;
            FBlockItem = new TBlockItem();
            FBlockItem.Name = aTreeNode.Text;
            ClientData.Blocks.Add(FBlockItem);
        }

        public TNodeObject(TreeNode aTreeNode, TExtClientData ClientData, Control Container)
        {
            Node = aTreeNode;
            aTreeNode.Tag = this;
            FBlockItem = new TBlockItem();
            FBlockItem.Name = Container.Name;
            FBlockItem.ContainerControl = Container;
            ClientData.Blocks.Add(FBlockItem);
        }

        public TNodeObject(TreeNode aTreeNode, TExtWebClientData ClientData, Object Container)
        {
            Node = aTreeNode;
            aTreeNode.Tag = this;
            FBlockItem = new TBlockItem();
            PropertyInfo aInfo = Container.GetType().GetProperty("ID");
            if (aInfo != null)
            {
                FBlockItem.Name = aInfo.GetValue(Container, null) as String;
            }
            FBlockItem.WebContainerControl = Container;
            ClientData.Blocks.Add(FBlockItem);
        }

        private TreeNode FNode;
        public TreeNode Node
        {
            get { return FNode; }
            set { FNode = value; }
        }

        private TBlockItem FBlockItem;
        public TBlockItem BlockItem
        {
            get { return FBlockItem; }
            set { FBlockItem = value; }
        }
    }
} 