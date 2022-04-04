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
using Microsoft.VisualStudio.Designer.Interfaces;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;
using System.Globalization;
using System.Resources;
using System.Text.RegularExpressions;
using mshtml;
using InfoRemoteModule;
using AjaxTools;
#if VS90
using WebDevPage = Microsoft.VisualWebDeveloper.Interop.WebDeveloperPage;
using JQClientTools;
#endif


namespace MWizard
{
    public partial class fmIonicWizard : Form
    {
        private TIonicWizardData FClientData;
        private DTE2 FDTE2;
        private AddIn FAddIn;
        private DbConnection InternalConnection = null;
        private TStringList FAlias;
        private static string _serverPath;
        private InfoDataSet FInfoDataSet = null;
        private string[] FProviderNameList;
        public Boolean SDCall = false;
        private ListViewColumnSorter lvwColumnSorterViewSrc;
        private ListViewColumnSorter lvwColumnSorterViewDes;
        private ListViewColumnSorter lvwColumnSorterMasterSrc;
        private ListViewColumnSorter lvwColumnSorterMasterDes;
        private ListViewColumnSorter lvwColumnSorterDetail;

        public fmIonicWizard()
        {
            InitializeComponent();
            FClientData = new TIonicWizardData(this);
            //PrepareWizardService();

            lvwColumnSorterViewSrc = new ListViewColumnSorter();
            lvwColumnSorterViewDes = new ListViewColumnSorter();
            lvwColumnSorterMasterSrc = new ListViewColumnSorter();
            lvwColumnSorterMasterDes = new ListViewColumnSorter();
            lvwColumnSorterDetail = new ListViewColumnSorter();
            this.lvViewSrcField.ListViewItemSorter = lvwColumnSorterViewSrc;
            this.lvViewDesField.ListViewItemSorter = lvwColumnSorterViewDes;
            this.lvMasterSrcField.ListViewItemSorter = lvwColumnSorterMasterSrc;
            this.lvMasterDesField.ListViewItemSorter = lvwColumnSorterMasterDes;
            this.lvSelectedFields.ListViewItemSorter = lvwColumnSorterDetail;
        }

        public fmIonicWizard(DTE2 aDTE2, AddIn aAddIn)
        {
            InitializeComponent();
            FClientData = new TIonicWizardData(this);
            FDTE2 = aDTE2;
            FAddIn = aAddIn;
            //PrepareWizardService();

            lvwColumnSorterViewSrc = new ListViewColumnSorter();
            lvwColumnSorterViewDes = new ListViewColumnSorter();
            lvwColumnSorterMasterSrc = new ListViewColumnSorter();
            lvwColumnSorterMasterDes = new ListViewColumnSorter();
            lvwColumnSorterDetail = new ListViewColumnSorter();
            this.lvViewSrcField.ListViewItemSorter = lvwColumnSorterViewSrc;
            this.lvViewDesField.ListViewItemSorter = lvwColumnSorterViewDes;
            this.lvMasterSrcField.ListViewItemSorter = lvwColumnSorterMasterSrc;
            this.lvMasterDesField.ListViewItemSorter = lvwColumnSorterMasterDes;
            this.lvSelectedFields.ListViewItemSorter = lvwColumnSorterDetail;
        }

        public DbConnection GlobalConnection
        {
            get { return InternalConnection; }
        }

        public String SelectedAlias
        {
            get { return cbEEPAlias.Text; }
        }

        private void PrepareWizardService()
        {
            Show();
            Hide();
        }

        private void ClearValues()
        {
            SDCall = false;
            cbWebSite.Items.Clear();
            cbWebSite.Text = "";
            tbCurrentSolution.Text = FDTE2.Solution.FileName;
            if (tbCurrentSolution.Text != "")
            {
                rbCurrentSolution.Enabled = true;
                rbCurrentSolution.Checked = true;
                rbAddToExistSolution.Checked = false;
                tbSolutionName.Text = "";
                GetWebSite();
            }
            else
            {
                rbCurrentSolution.Enabled = false;
                rbAddToExistSolution.Checked = true;
            }
            tbSolutionName.Text = "";
            cbAddToExistFolder.Items.Clear();
            cbAddToExistFolder.Text = "";
            //cbIonicFolder.Items.Clear();
            //cbIonicFolder.Text = "";
            tbAddToNewFolder.Text = "";
            rbAddToRootFolder_CheckedChanged(rbAddToRootFolder, null);
            tbTableName.Text = "";
            tbTableNameF.Text = "";
            tbProviderName.Text = "";
            tbFormName.Text = "Form1";
            tbDetailTableName.Text = "";
            cbViewProviderName.Items.Clear();
            cbViewProviderName.Text = "";
            cbWebForm.Text = "IonicSingle1";
            ClearAll();
        }

        private void ClearAll()
        {
            lvViewSrcField.Items.Clear();
            lvViewDesField.Items.Clear();
            tbCaption.Text = "";
            cbControlType.Text = "";
            lvMasterDesField.Items.Clear();
            lvMasterSrcField.Items.Clear();
            FClientData.Blocks.Clear();
            tvRelation.Nodes.Clear();
            tbCaption_D.Text = "";
            cbControlType_D.Text = "";
            lvSelectedFields.Items.Clear();

            tbComboRemoteName.Text = String.Empty;
            tbComboTableName.Text = String.Empty;
            tbComboTableName_F.Text = String.Empty;
            cbDataValueField.Items.Clear();
            cbDataTextField.Items.Clear();
            cbDataValueField.Text = String.Empty;
            cbDataTextField.Text = String.Empty;
            tbComboRemoteName_D.Text = String.Empty;
            tbComboTableName_D.Text = String.Empty;
            tbComboTableName_D_F.Text = String.Empty;
            cbComboValueField_D.Items.Clear();
            cbComboDisplayField_D.Items.Clear();
            cbComboValueField_D.Text = String.Empty;
            cbComboDisplayField_D.Text = String.Empty;
        }

        private void Init()
        {
            ClearValues();
            LoadDBString();
            FInfoDataSet = new InfoDataSet();
            if (((FDTE2 != null) && (FDTE2.Solution.FileName != "")) && File.Exists(FDTE2.Solution.FileName))
            {
                EnabledOutputControls();
            }
            FInfoDataSet.SetWizardDesignMode(true);
            try
            {
                cbEEPAlias.Text = EEPRegistry.WizardConnectionString;
                cbDatabaseType.Text = EEPRegistry.DataBaseType;
            }
            catch { }
            DisplayPage(tpConnection);
        }

        private void DisplayPage(TabPage aPage)
        {
            tabControl.TabPages.Clear();
            tabControl.TabPages.Add(aPage);
            tabControl.SelectedTab = aPage;
            EnableButton();
        }

        private void EnableButton()
        {
            btnPrevious.Enabled = tabControl.SelectedTab != tpConnection;
            if (FClientData.IsMasterDetailBaseForm())
            {
                btnNext.Enabled = tabControl.SelectedTab != tpDetailFields;
                btnDone.Enabled = tabControl.SelectedTab == tpDetailFields;
            }
            else
            {
                btnNext.Enabled = tabControl.SelectedTab != tpMasterFields;
                btnDone.Enabled = tabControl.SelectedTab == tpMasterFields;
            }
            btnCancel.Enabled = true;
        }

        private static string GetServerPath()
        {
            if ((fmIonicWizard._serverPath == null) || (fmIonicWizard._serverPath.Length == 0))
            {
                fmIonicWizard._serverPath = EEPRegistry.Server + "\\";
            }
            return fmIonicWizard._serverPath;
        }

        private void LoadDBString()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Please setup <DB Manager> of EEPNetServer at first !");
            }
        }

        public void ShowIonicWizard()
        {
            //Show();
            Init();
            ShowDialog();
        }

        public void SDGenWebForm(string XML)
        {
            SDCall = true;
            if (XML != "")
            {
                FClientData.Blocks.Clear();
                FClientData.LoadFromXML(XML);
            }
            TIonicWizardGenerator CG = new TIonicWizardGenerator(FClientData, FDTE2, FAddIn);
            CG.GenWebClientModule();
            SDCall = false;
        }

        private void SetFieldNames(String TableName, ListView LV)
        {
            int I;
            DataRow[] DRs;
            DataRow DR;
            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = InternalConnection;
            String OWNER = String.Empty, SS = TableName;
            if (SS.Contains("."))
            {
                OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                TableName = SS;
            }
            aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME='" + TableName + "' OR TABLE_NAME='" + OWNER + "." + TableName + "'";
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet dsColdef = new DataSet();
            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsColdef, TableName);


            DataTable dtTableSchema = FInfoDataSet.RealDataSet.Tables[0];
            for (I = 0; I < dtTableSchema.Columns.Count; I++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = dtTableSchema.Columns[I].ColumnName;
                DRs = dsColdef.Tables[0].Select("FIELD_NAME='" + lvi.Text + "'");
                TBlockFieldItem aBlockFieldItem = new TBlockFieldItem();
                aBlockFieldItem.DataField = lvi.Text;
                aBlockFieldItem.DataType = dtTableSchema.Columns[I].DataType;
                lvi.Tag = aBlockFieldItem;
                if (DRs.Length > 0)
                {
                    DR = DRs[0];
                    lvi.SubItems.Add(DR["CAPTION"].ToString());

                    aBlockFieldItem.Description = DR["CAPTION"].ToString();
                    aBlockFieldItem.CheckNull = DR["CHECK_NULL"].ToString().ToUpper();
                    aBlockFieldItem.DefaultValue = DR["DEFAULT_VALUE"].ToString();
                    aBlockFieldItem.ControlType = DR["NEEDBOX"].ToString();
                    //aBlockFieldItem.EditMask = DR["EDITMASK"].ToString();
                    if (aBlockFieldItem.DataType == typeof(DateTime))
                    {
                        if (aBlockFieldItem.ControlType == null || aBlockFieldItem.ControlType == "")
                            aBlockFieldItem.ControlType = "DateTimeBox";
                    }
                    else if (aBlockFieldItem.DataType == typeof(Int16) || aBlockFieldItem.DataType == typeof(Int32)
                            || aBlockFieldItem.DataType == typeof(Int64) || aBlockFieldItem.DataType == typeof(float)
                            || aBlockFieldItem.DataType == typeof(double) || aBlockFieldItem.DataType == typeof(decimal))
                    {
                        if (aBlockFieldItem.ControlType == null || aBlockFieldItem.ControlType == "")
                            aBlockFieldItem.ControlType = "NumberBox";
                    }
                    aBlockFieldItem.QueryMode = DR["QUERYMODE"].ToString();
                    if (DR["FIELD_LENGTH"] != null && DR["FIELD_LENGTH"].ToString() != "")
                        aBlockFieldItem.Length = Convert.ToInt32(DR["FIELD_LENGTH"]);
                    if (DR["IS_KEY"] != null && DR["IS_KEY"].ToString() == "Y")
                        aBlockFieldItem.IsKey = true;
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
            String OWNER = String.Empty, SS = TableName;
            if (SS.Contains("."))
            {
                OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                TableName = SS;
            }
            aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME='" + TableName + "' OR TABLE_NAME='" + OWNER + "." + TableName + "'";
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
                    TBlockFieldItem aFieldItem = new TBlockFieldItem();
                    if (DRS.Length > 0)
                    {
                        aItem.SubItems.Add(DRS[0]["CAPTION"].ToString());
                        aFieldItem.Description = DRS[0]["CAPTION"].ToString();
                        aFieldItem.QueryMode = DRS[0]["QUERYMODE"].ToString();
                        if (DRS[0]["IS_KEY"] != null && DRS[0]["IS_KEY"].ToString() == "Y")
                            aFieldItem.IsKey = true;
                    }
                    else
                    {
                        aItem.SubItems.Add("");
                        aFieldItem.Description = "";
                    }
                    aListView.Items.Add(aItem);
                    aFieldItem.DataField = Column.ColumnName;
                    aFieldItem.DataType = Column.DataType;
                    if (DRS.Length > 0 && DRS[0]["CAPTION"] != null)
                        aItem.Tag = aFieldItem;

                }
            }
            finally
            {
                aDataSet.Dispose();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Equals(tpConnection))
            {
                WzdUtils.SetRegistryValueByKey("WizardConnectionString", cbEEPAlias.Text);
                WzdUtils.SetRegistryValueByKey("DatabaseType", cbDatabaseType.Text);
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

                if (cbChooseLanguage.Text == "" || cbChooseLanguage.Text == "C#")
                    FClientData.Language = "cs";
                else if (cbChooseLanguage.Text == "VB")
                    FClientData.Language = "vb";

                if (cbWebSite.Items.Count == 1)
                {
                    cbWebSite.SelectedIndex = 0;
                    cbWebSite_SelectedIndexChanged(new object(), new EventArgs());
                }

                DisplayPage(tpOutputSetting);
            }
            else if (tabControl.SelectedTab.Equals(tpOutputSetting))
            {
                FClientData.FolderName = "";
                if (rbAddToExistSolution.Checked && tbSolutionName.Text == "")
                {
                    tbSolutionName.Focus();
                    MessageBox.Show("Please input SolutionName");
                }
                else if (cbWebSite.Text == "")
                {
                    cbWebSite.Focus();
                    MessageBox.Show("Please select a WebSite");
                }
                else if (rbAddToExistFolder.Checked && (cbAddToExistFolder.Text == ""))
                {
                    cbAddToExistFolder.Focus();
                    MessageBox.Show("Please select a exist folder");
                }
                else if (rbCurrentSolution.Checked && (tbCurrentSolution.Text == ""))
                {
                    MessageBox.Show("The IDE's Solution is empty");
                }
                else if (rbAddToNewFolder.Checked && (tbAddToNewFolder.Text == ""))
                {
                    tbAddToNewFolder.Focus();
                    MessageBox.Show("Please input new folder");
                }
                else
                {
                    if (rbAddToExistFolder.Checked)
                    {
                        FClientData.FolderName = cbAddToExistFolder.Text;
                        FClientData.FolderMode = "ExistFolder";
                    }
                    else if (rbAddToNewFolder.Checked)
                    {
                        if (cbAddToExistFolder.Items.Contains(tbAddToNewFolder.Text))
                        {
                            MessageBox.Show("The folder name you typed has already existed.");
                            return;
                        }
                        FClientData.FolderName = tbAddToNewFolder.Text;
                        FClientData.FolderMode = "NewFolder";
                    }
                    if (rbCurrentSolution.Checked)
                    {
                        FClientData.SolutionName = tbCurrentSolution.Text;
                    }
                    if (rbAddToExistSolution.Checked)
                    {
                        FClientData.SolutionName = tbSolutionName.Text;
                    }
                    FClientData.WebSiteName = cbWebSite.Text;
                    FClientData.WebSiteFullName = cbWebSite.Tag != null ? cbWebSite.Tag.ToString() : cbWebSite.Text;
                    tbProviderName.Text = "";
                    DisplayPage(tpFormSetting);
                }

                if (this.cbChooseLanguage.SelectedItem != null)
                {
                    switch (this.cbChooseLanguage.SelectedItem.ToString())
                    {
                        case "C#":
                            this.cbWebForm.Items.Clear();
                            this.cbWebForm.Items.Add("IonicSingle1");
                            this.cbWebForm.Items.Add("IonicMasterDetail1");
                            this.cbWebForm.SelectedIndex = 0;
                            break;
                    }
                }
                else
                {
                    this.cbWebForm.Items.Clear();
                    this.cbWebForm.Items.Add("IonicSingle1");
                    this.cbWebForm.Items.Add("IonicMasterDetail1");
                    this.cbWebForm.SelectedIndex = 0;
                }
            }
            else if (tabControl.SelectedTab.Equals(tpFormSetting))
            {
                if (cbWebForm.Text == "")
                {
                    MessageBox.Show("Please select EEP Web Templates Form !!");
                    if (cbWebForm.CanFocus)
                    {
                        cbWebForm.Focus();
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
                    FClientData.FormName = tbFormName.Text;
                    FClientData.FormTitle = tbFormTitle.Text;
                    FClientData.BaseFormName = cbWebForm.Text;
                    cbViewProviderName.Visible = (FClientData.BaseFormName.CompareTo("WMasterDetail3") == 0 || FClientData.BaseFormName.CompareTo("VBWebCMasterDetail_VFG") == 0 || FClientData.BaseFormName.CompareTo("WMasterDetail8") == 0 || FClientData.BaseFormName.CompareTo("VBWebCMasterDetail8") == 0);
                    label18.Visible = cbViewProviderName.Visible;
                    DisplayPage(tpDataSource);
                }
            }
            else if (tabControl.SelectedTab.Equals(tpDataSource))
            {
                if (tbProviderName.Text == "")
                {
                    MessageBox.Show("Please input Provider Name !!");
                    if (tbProviderName.CanFocus)
                    {
                        tbProviderName.Focus();
                    }
                }
                else if (tbTableName.Text == "")
                {
                    MessageBox.Show("Please input Table Name !!");
                    if (tbTableName.CanFocus)
                    {
                        tbTableName.Focus();
                    }
                }
                else if (cbViewProviderName.Visible && cbViewProviderName.Text == "")
                {
                    MessageBox.Show("Please input View Provider Name !!");
                    if (cbViewProviderName.CanFocus)
                    {
                        cbViewProviderName.Focus();
                    }
                }
                else
                {
                    FClientData.ProviderName = tbProviderName.Text;
                    FClientData.TableName = tbTableName.Text;
                    FClientData.RealTableName = tbTableNameF.Text;
                    FClientData.BaseFormName = cbWebForm.Text;
                    if (lvMasterSrcField.Items.Count == 0 && lvMasterDesField.Items.Count == 0)
                        SetFieldNames(FClientData.RealTableName, lvMasterSrcField);
                    if (FClientData.BaseFormName == "WMasterDetail3" || FClientData.BaseFormName == "VBWebCMasterDetail_VFG" || FClientData.BaseFormName == "WMasterDetail8"
                        || FClientData.BaseFormName == "WSingle2" || FClientData.BaseFormName == "WSingle3" || FClientData.BaseFormName == "WSingle4"
                        || FClientData.BaseFormName == "WSingle5" || FClientData.BaseFormName == "VBWebCMasterDetail8" || FClientData.BaseFormName == "VBWebSingle5")
                    {
                        if (lvViewSrcField.Items.Count == 0 && lvViewDesField.Items.Count == 0)
                        {
                            if (cbViewProviderName.Visible)
                                SetFieldNamesByProvider(FClientData.RealTableName, FClientData.ViewProviderName, lvViewSrcField);
                            else
                                SetFieldNames(FClientData.RealTableName, lvViewSrcField);
                        }
                        DisplayPage(tpViewFields);
                    }
                    else
                    {
                        DisplayPage(tpMasterFields);
                    }
                }
            }
            else if (tabControl.SelectedTab.Equals(tpViewFields))
            {
                DisplayPage(tpMasterFields);
            }
            else if (tabControl.SelectedTab.Equals(tpMasterFields) && FClientData.IsMasterDetailBaseForm())
            {
                DisplayPage(tpDetailFields);
            }
            BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Equals(tpOutputSetting))
            {
                DisplayPage(tpConnection);

            }
            else
            {
                if (tabControl.SelectedTab.Equals(tpFormSetting))
                {
                    DisplayPage(tpOutputSetting);
                }
                if (tabControl.SelectedTab.Equals(tpDataSource))
                {
                    DisplayPage(tpFormSetting);
                }
                if (tabControl.SelectedTab.Equals(tpViewFields))
                {
                    DisplayPage(tpDataSource);
                }
                if (tabControl.SelectedTab.Equals(tpMasterFields))
                {
                    if (FClientData.BaseFormName == "WMasterDetail3" || FClientData.BaseFormName == "VBWebCMasterDetail_VFG"
                        || FClientData.BaseFormName == "WMasterDetail8" || FClientData.BaseFormName == "VBWebCMasterDetail8")
                        DisplayPage(tpViewFields);
                    else
                        DisplayPage(tpDataSource);
                }
                if (tabControl.SelectedTab.Equals(tpDetailFields))
                {
                    DisplayPage(tpMasterFields);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAllListViewSort();

            FInfoDataSet.Dispose();
            FInfoDataSet = null;
            Hide();
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
                        System.Windows.Forms.Button B = new System.Windows.Forms.Button();
                        B.Parent = lvDes;
                        RearrangeRefValButton(B, LVSI.Bounds);
                        B.BackColor = Color.Silver;
                        B.BringToFront();
                        LVSI.Tag = B;
                        B.Tag = Item;
                        B.Click += new EventHandler(btnRefVal_Click);
                        B.Text = "...";
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
                                ((System.Windows.Forms.Button)LVSI.Tag).Dispose();
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
                        RearrangeRefValButton((System.Windows.Forms.Button)LVSI.Tag, LVSI.Bounds);
                }
            }
        }

        public delegate void RearrangeRefValButtonFunc(System.Windows.Forms.Button B, Rectangle Bounds);

        private void RearrangeRefValButton(System.Windows.Forms.Button B, Rectangle Bounds)
        {
            Rectangle NewBounds = new Rectangle();
            if (Bounds.Width > 20)
            {
                NewBounds.X = Bounds.X + Bounds.Width - 20;
                NewBounds.Width = 20;
            }
            else
            {
                NewBounds.X = Bounds.X;
                NewBounds.Width = Bounds.Width;
            }
            NewBounds.Y = Bounds.Y - 1;
            NewBounds.Height = Bounds.Height - 2;
            B.Bounds = NewBounds;
        }

        private void btnMasterAll_Click(object sender, EventArgs e)
        {
            SelectFields(lvMasterSrcField, lvMasterDesField, false);
        }

        private void btnMasterRemove_Click(object sender, EventArgs e)
        {
            SelectFields(lvMasterDesField, lvMasterSrcField, false);
        }

        private void btnMasterAddAll_Click(object sender, EventArgs e)
        {
            SelectFields(lvMasterSrcField, lvMasterDesField, true);
        }

        private void btnMasterRemoveAll_Click(object sender, EventArgs e)
        {
            SelectFields(lvMasterDesField, lvMasterSrcField, true);
        }

        //private void AddDetailBlockItem(string MasterItemName, System.Windows.Forms.TreeNodeCollection NodeCollection)
        //{
        //    for (int I = 0; I < NodeCollection.Count; I++)
        //    {
        //        TDetailItem DetailItem = (TDetailItem)NodeCollection[I].Tag;
        //        TBlockItem BlockItem = new TBlockItem();
        //        BlockItem.Name = NodeCollection[I].Text;
        //        BlockItem.RelationName = DetailItem.Relation.RelationName;
        //        BlockItem.TableName = DetailItem.TableName;
        //        if (NodeCollection[I].Parent != null)
        //        {
        //            BlockItem.ParentItemName = NodeCollection[I].Parent.Text;
        //        }
        //        else
        //        {
        //            BlockItem.ParentItemName = MasterItemName;
        //        }
        //        FClientData.Blocks.Add(BlockItem);
        //        BlockItem.BlockFieldItems = DetailItem.BlockFieldItems;
        //        AddDetailBlockItem(MasterItemName, NodeCollection[I].Nodes);
        //    }
        //}

        private void AddDetailBlockItem(string MasterItemName, System.Windows.Forms.TreeNodeCollection NodeCollection, ListView LV)
        {
            for (int I = 0; I < NodeCollection.Count; I++)
            {
                TBlockItem BlockItem = new TBlockItem();
                BlockItem.Name = NodeCollection[I].Text;
                BlockItem.TableName = ((TDetailItem)NodeCollection[I].Tag).TableName;
                BlockItem.Relation = ((TDetailItem)NodeCollection[I].Tag).Relation;
                BlockItem.RelationName = ((TDetailItem)NodeCollection[I].Tag).Relation.RelationName;
                for (int J = 0; J < ((TDetailItem)NodeCollection[I].Tag).BlockFieldItems.Count; J++)
                {
                    TBlockFieldItem aItem = ((TDetailItem)NodeCollection[I].Tag).BlockFieldItems[J] as TBlockFieldItem;
                    TBlockFieldItem BlockFieldItem = new TBlockFieldItem();
                    if (aItem != null)
                    {
                        BlockFieldItem.DataField = aItem.DataField;
                        BlockFieldItem.CheckNull = aItem.CheckNull;
                        BlockFieldItem.DefaultValue = aItem.DefaultValue;
                        BlockFieldItem.Description = aItem.Description;
                        BlockFieldItem.RefValNo = aItem.RefValNo;
                        BlockFieldItem.ControlType = aItem.ControlType;
                        BlockFieldItem.ComboRemoteName = aItem.ComboRemoteName;
                        BlockFieldItem.ComboEntityName = aItem.ComboEntityName;
                        BlockFieldItem.ComboTextField = aItem.ComboTextField;
                        BlockFieldItem.ComboValueField = aItem.ComboValueField;
                        BlockFieldItem.ComboTextFieldCaption = aItem.ComboTextFieldCaption;
                        BlockFieldItem.ComboValueFieldCaption = aItem.ComboValueFieldCaption;
                        BlockFieldItem.DataType = aItem.DataType;
                        BlockFieldItem.QueryMode = aItem.QueryMode;
                        BlockFieldItem.EditMask = aItem.EditMask;
                        BlockFieldItem.Length = aItem.Length;
                        BlockFieldItem.ComboOtherFields = aItem.ComboOtherFields;
                        BlockFieldItem.IsKey = aItem.IsKey;
                    }
                    //else
                    //{
                    //    BlockFieldItem.DataField = aItem.Text;
                    //}
                    BlockItem.BlockFieldItems.Add(BlockFieldItem);

                }
                if (NodeCollection[I].Parent != null)
                {
                    BlockItem.ParentItemName = NodeCollection[I].Parent.Text;
                }
                else
                {
                    BlockItem.ParentItemName = MasterItemName;
                }
                FClientData.Blocks.Add(BlockItem);
                AddDetailBlockItem(MasterItemName, NodeCollection[I].Nodes, LV);
            }
        }

        private void DoGenClient()
        {
            FClientData.FormName = tbFormName.Text;
            FClientData.TableName = tbTableName.Text;
            FClientData.RealTableName = tbTableNameF.Text;
            FClientData.ViewProviderName = cbViewProviderName.Text;
            TIonicWizardGenerator Generator = new TIonicWizardGenerator(FClientData, FDTE2, FAddIn);
            Generator.GenWebClientModule();
        }

        private void ClearAllListViewSort()
        {
            (this.lvViewSrcField.ListViewItemSorter as ListViewColumnSorter).OrderOfSort = System.Windows.Forms.SortOrder.None;
            (this.lvViewDesField.ListViewItemSorter as ListViewColumnSorter).OrderOfSort = System.Windows.Forms.SortOrder.None;
            (this.lvMasterSrcField.ListViewItemSorter as ListViewColumnSorter).OrderOfSort = System.Windows.Forms.SortOrder.None;
            (this.lvMasterDesField.ListViewItemSorter as ListViewColumnSorter).OrderOfSort = System.Windows.Forms.SortOrder.None;
            (this.lvSelectedFields.ListViewItemSorter as ListViewColumnSorter).OrderOfSort = System.Windows.Forms.SortOrder.None;

            btnViewUp.Enabled = true;
            btnViewDown.Enabled = true;
            btnMasterUp.Enabled = true;
            btnMasterDown.Enabled = true;
            btnDetailUp.Enabled = true;
            btnDetailDown.Enabled = true;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            ClearAllListViewSort();
            SetValue();
            SetValue_D();

            if (FClientData.IsMasterDetailBaseForm())
            {
                if (FClientData.BaseFormName == "DEMasterDetail2" || FClientData.BaseFormName == "DEMasterDetail2_VB")
                    AddBlockItem("View", FClientData.ProviderName, FClientData.TableName, lvViewDesField);
                AddBlockItem("Master", FClientData.ProviderName, FClientData.TableName, lvMasterDesField);
                AddDetailBlockItem("Master", tvRelation.Nodes, lvSelectedFields);
            }
            else
            {
                if (FClientData.BaseFormName == "DESingle2" || FClientData.BaseFormName == "DESingle2_VB")
                    AddBlockItem("View", FClientData.ProviderName, FClientData.TableName, lvViewDesField);
                AddBlockItem("Main", FClientData.ProviderName, FClientData.TableName, lvMasterDesField);
            }
            Hide();
            FDTE2.MainWindow.Activate();
            DoGenClient();
            FInfoDataSet.Dispose();
            FInfoDataSet = null;
        }

        private void AddBlockItem(string BlockName, string ProviderName, string TableName, ListView LV)
        {
            int I;
            TBlockItem BlockItem = new TBlockItem();
            TBlockFieldItem BlockFieldItem;

            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = InternalConnection;
            String OWNER = String.Empty, SS = TableName;
            if (SS.Contains("."))
            {
                OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                TableName = SS;
            }
            aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME='" + TableName + "' OR TABLE_NAME='" + OWNER + "." + TableName + "'";

            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet DS = new DataSet();
            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, DS, TableName);
            BlockItem.Name = BlockName;
            BlockItem.ProviderName = ProviderName;
            BlockItem.TableName = TableName;
            for (I = 0; I < LV.Items.Count; I++)
            {
                ListViewItem aItem = LV.Items[I];
                BlockFieldItem = new TBlockFieldItem();
                if (aItem.Tag != null)
                {
                    BlockFieldItem.DataField = ((TBlockFieldItem)aItem.Tag).DataField;
                    BlockFieldItem.CheckNull = ((TBlockFieldItem)aItem.Tag).CheckNull;
                    BlockFieldItem.DefaultValue = ((TBlockFieldItem)aItem.Tag).DefaultValue;
                    BlockFieldItem.Description = ((TBlockFieldItem)aItem.Tag).Description;
                    BlockFieldItem.RefValNo = ((TBlockFieldItem)aItem.Tag).RefValNo;
                    BlockFieldItem.ControlType = ((TBlockFieldItem)aItem.Tag).ControlType;
                    BlockFieldItem.ComboEntityName = ((TBlockFieldItem)aItem.Tag).ComboEntityName;
                    BlockFieldItem.ComboRemoteName = ((TBlockFieldItem)aItem.Tag).ComboRemoteName;
                    BlockFieldItem.ComboTextField = ((TBlockFieldItem)aItem.Tag).ComboTextField;
                    BlockFieldItem.ComboValueField = ((TBlockFieldItem)aItem.Tag).ComboValueField;
                    BlockFieldItem.ComboTextFieldCaption = ((TBlockFieldItem)aItem.Tag).ComboTextFieldCaption;
                    BlockFieldItem.ComboValueFieldCaption = ((TBlockFieldItem)aItem.Tag).ComboValueFieldCaption;
                    BlockFieldItem.DataType = ((TBlockFieldItem)aItem.Tag).DataType;
                    BlockFieldItem.QueryMode = ((TBlockFieldItem)aItem.Tag).QueryMode;
                    BlockFieldItem.EditMask = ((TBlockFieldItem)aItem.Tag).EditMask;
                    BlockFieldItem.Length = ((TBlockFieldItem)aItem.Tag).Length;
                    BlockFieldItem.ComboOtherFields = ((TBlockFieldItem)aItem.Tag).ComboOtherFields;
                    BlockFieldItem.IsKey = ((TBlockFieldItem)aItem.Tag).IsKey;
                }
                else
                {
                    BlockFieldItem.DataField = aItem.Text;
                }
                /*
                BlockFieldItem.DataField = LV.Items[I].Text;
				DRs = DS.Tables[0].Select("FIELD_NAME='" + BlockFieldItem.DataField + "'");
                if (DRs.Length == 1)
				{ 
					DR = DRs[0];
                    if (!DR.IsNull("FIELD_LENGTH"))
                       BlockFieldItem.Length = int.Parse(DR["FIELD_LENGTH"].ToString());
                    if (DR["IS_KEY"].ToString() == "Y")
					{
                        BlockFieldItem.IsKey = true;
					}
					else
					{
						BlockFieldItem.IsKey = false;
					}
                    BlockFieldItem.Description = DR["CAPTION"].ToString();
                    if (LV.Items[I].SubItems.Count == 3)
                    {
                        BlockFieldItem.RefValNo = aItem.SubItems[2].Text;
                    }
                    if (BlockFieldItem.Description == "")
					{
						BlockFieldItem.Description = BlockFieldItem.DataField;
					}

                    BlockFieldItem.CheckNull = DR["CHECK_NULL"].ToString().ToUpper();
                    BlockFieldItem.DefaultValue = DR["DEFAULT_VALUE"].ToString();
				}
                 */
                BlockItem.BlockFieldItems.Add(BlockFieldItem);

            }
            FClientData.Blocks.Add(BlockItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FClientData.FormName = tbFormName.Text;
            FClientData.TableName = tbTableName.Text;
            FClientData.RealTableName = tbTableNameF.Text;
            TIonicWizardGenerator G = new TIonicWizardGenerator(FClientData, FDTE2, FAddIn);
            G.GenWebClientModule();
            Close();
        }

        private void GetWebSite()
        {
            cbWebSite.Items.Clear();
            foreach (Project P in FDTE2.Solution.Projects)
            {
                if (string.Compare(P.Kind, "{E24C65DC-7377-472b-9ABA-BC803B73C61A}") == 0)
                {
                    cbWebSite.Items.Add(P.Name);
                }
            }

            if (cbWebSite.Items.Count == 1)
            {
                cbWebSite.SelectedIndex = 0;
                cbWebSite_SelectedIndexChanged(new object(), new EventArgs());
            }
        }

        private void btnSolutionName_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbSolutionName.Text = openFileDialog.FileName;
                if (string.Compare(tbSolutionName.Text, FDTE2.Solution.FullName) != 0)
                {
                    FDTE2.Solution.Open(tbSolutionName.Text);
                }
                GetWebSite();
            }
        }

        private void EnabledOutputControls()
        {
        }

        private void rbNewSolution_Click(object sender, EventArgs e)
        {
            EnabledOutputControls();
        }

        private void rbAddToExistSln_Click(object sender, EventArgs e)
        {
            EnabledOutputControls();
        }

        private void rbAddToCurrent_Click(object sender, EventArgs e)
        {
            EnabledOutputControls();
        }

        private void ShowChildRelation(DataRelationCollection Relations, System.Windows.Forms.TreeNode Node)
        {
            System.Windows.Forms.TreeNode ChildNode;
            foreach (DataRelation R in Relations)
            {
                InfoBindingSource IBS = new InfoBindingSource();

                //if ((Node == null) || (Node.Level == 0))
                //{
                IBS.DataSource = FInfoDataSet;
                IBS.DataMember = FInfoDataSet.RealDataSet.Tables[0].TableName;
                //}
                //else
                //{
                //    TDetailItem item1 = (TDetailItem)Node.Parent.Tag;
                //    IBS.DataSource = item1.BindingSource;
                //    IBS.DataMember = item1.Relation.RelationName;
                //}
                ChildNode = new System.Windows.Forms.TreeNode();
                ChildNode.Text = R.ChildTable.TableName;
                ChildNode.Name = R.ChildTable.TableName;
                Node.Nodes.Add(ChildNode);
                SetNodeData(R, IBS, ChildNode);
                ShowChildRelation(R.ChildTable.ChildRelations, ChildNode);
            }
        }

        private void ShowTable(InfoBindingSource aBindingSource, DataRelation Relation)
        {
            DataRelation R1;
            System.Windows.Forms.TreeNode Node;
            InfoBindingSource IBS;
            if (aBindingSource.DataSource.GetType().Equals(typeof(InfoDataSet)))
            {
                InfoDataSet set1 = (InfoDataSet)aBindingSource.DataSource;
                for (int I = 0; I < set1.RealDataSet.Tables[0].ChildRelations.Count; I++)
                {
                    R1 = set1.RealDataSet.Tables[0].ChildRelations[I];
                    Node = new System.Windows.Forms.TreeNode();
                    Node.Text = R1.ChildTable.TableName;
                    Node.Name = R1.ChildTable.TableName;
                    tvRelation.Nodes.Add(Node);
                    IBS = new InfoBindingSource();
                    IBS.DataSource = aBindingSource;
                    IBS.DataMember = R1.RelationName;
                    SetNodeData(R1, IBS, Node);
                    ShowChildRelation(R1.ChildTable.ChildRelations, Node);
                }
            }
            if (aBindingSource.DataSource.GetType().Equals(typeof(InfoBindingSource)))
            {
                while (!aBindingSource.DataSource.GetType().Equals(typeof(InfoDataSet)))
                {
                    aBindingSource = (InfoBindingSource)aBindingSource.DataSource;
                }
                InfoDataSet set2 = (InfoDataSet)aBindingSource.DataSource;
                for (int num2 = 0; num2 < set2.RealDataSet.Tables.Count; num2++)
                {
                    if (set2.RealDataSet.Tables[num2].TableName.Equals(Relation.ChildTable.TableName))
                    {
                        for (int num3 = 0; num3 < set2.RealDataSet.Tables[num2].ChildRelations.Count; num3++)
                        {
                            R1 = set2.RealDataSet.Tables[num2].ChildRelations[num3];
                            Node = new System.Windows.Forms.TreeNode();
                            Node.Text = R1.ChildTable.TableName;
                            Node.Name = R1.ChildTable.TableName;
                            tvRelation.Nodes.Add(Node);
                            IBS = new InfoBindingSource();
                            IBS.DataSource = aBindingSource;
                            IBS.DataMember = R1.RelationName;
                            SetNodeData(R1, IBS, Node);
                            ShowChildRelation(R1.ChildTable.ChildRelations, Node);
                        }
                    }
                }
            }
        }

        private void ShowTableRelations()
        {
            tvRelation.Nodes.Clear();
            InfoBindingSource IBS = new InfoBindingSource();
            DataRelation R = null;
            try
            {
                IBS.DataSource = FInfoDataSet;
                IBS.DataMember = FInfoDataSet.RealDataSet.Tables[0].TableName;
                ShowTable(IBS, R);
            }
            finally
            {
                IBS.Dispose();
            }
        }

        private void btnConnectionString_Click(object sender, EventArgs e)
        {
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
                Type myType = a.GetType(sDll + ".Component", true, true);
                if (myType != null)
                    MessageBox.Show("get");
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void SetNodeData(DataRelation Relation, InfoBindingSource BindingSource, System.Windows.Forms.TreeNode Node)
        {
            TDetailItem DetailItem = new TDetailItem();
            DetailItem.BindingSource = BindingSource;
            DetailItem.Relation = Relation;
            DetailItem.TableName = Relation.ChildTable.TableName;
            String ModuleName = tbProviderName.Text;
            ModuleName = ModuleName.Substring(0, ModuleName.IndexOf('.'));
            String SolutionName = System.IO.Path.GetFileNameWithoutExtension(FClientData.SolutionName);
            DetailItem.RealTableName = CliUtils.GetTableName(ModuleName, DetailItem.TableName, SolutionName, "", true);
            Node.Tag = DetailItem;
            tvRelation.SelectedNode = Node;
        }

        private void btnNewDataset_Click(object sender, EventArgs e)
        {
            InfoBindingSource IBS = new InfoBindingSource();
            System.Windows.Forms.TreeNode node1 = tvRelation.SelectedNode;
            if ((node1 == null) || (node1.Level == 0))
            {
                if (FInfoDataSet.RemoteName == "")
                {
                    FInfoDataSet.RemoteName = tbProviderName.Text;
                }
                IBS.DataSource = FInfoDataSet;
                IBS.DataMember = tbTableName.Text;
            }
            else
            {
                TDetailItem item1 = (TDetailItem)node1.Parent.Tag;
                IBS.DataSource = item1.BindingSource;
                IBS.DataMember = item1.Relation.RelationName;
            }
            fmSelDetail detail1 = new fmSelDetail();
            DataRelation R = null;
            if (detail1.ShowSelDetail(IBS, ref R))
            {
                System.Windows.Forms.TreeNode Node = tvRelation.Nodes.Add(R.ChildTable.TableName);
                SetNodeData(R, IBS, Node);
                UpdatelvSelectedFields((TDetailItem)Node.Tag);
            }
        }

        private void UpdatelvSelectedFields(TDetailItem DetailItem)
        {
            lvSelectedFields.BeginUpdate();
            lvSelectedFields.Items.Clear();
            try
            {
                tbDetailTableName.Text = DetailItem.RealTableName;
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

        private void btnNewField_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.TreeNode Node = tvRelation.SelectedNode;
            if (Node != null)
            {
                TDetailItem DetailItem = (TDetailItem)Node.Tag;
                MWizard.fmSelTableField F = new fmSelTableField();
                if (F.ShowSelTableFieldForm(DetailItem, GetFieldNames, lvSelectedFields, InternalConnection, RearrangeRefValButton, btnRefVal_Click, FClientData.DatabaseType))
                {
                    btnDeleteField.Enabled = lvSelectedFields.Items.Count > 0;
                }
            }
        }

        public delegate void GetFieldNamesFunc(string DatabaseName, string TableName, String DataSetName, ListView SrcListView, ListView DestListView);

        public void GetFieldNames(string DatabaseName, string TableName, String DataSetName, ListView SrcListView, ListView DestListView)
        {
            System.Windows.Forms.TreeNode Node = tvRelation.SelectedNode;
            if (Node == null)
                return;
            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = InternalConnection;
            String OWNER = String.Empty, SS = TableName;
            if (SS.Contains("."))
            {
                OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                TableName = SS;
            }
            aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME='" + TableName + "' OR TABLE_NAME='" + OWNER + "." + TableName + "'";

            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet dsColdef = new DataSet();
            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsColdef, "COLDEF");

            int Index = FInfoDataSet.RealDataSet.Tables.IndexOf(WzdUtils.RemoveSpace(DataSetName));
            DataTable Table = FInfoDataSet.RealDataSet.Tables[Index];

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
                    DRs = dsColdef.Tables[0].Select("TABLE_NAME = '" + WzdUtils.RemoveQuote(TableName, FClientData.DatabaseType) + "' and FIELD_NAME = '" + Table.Columns[I].ColumnName + "'");
                    if (DRs.Length == 1)
                    {
                        FieldItem.Description = DRs[0]["CAPTION"].ToString();
                        FieldItem.CheckNull = DRs[0]["CHECK_NULL"].ToString();
                        FieldItem.DefaultValue = DRs[0]["DEFAULT_VALUE"].ToString();
                        FieldItem.IsKey = DRs[0]["IS_KEY"].ToString().ToUpper() == "Y";
                        FieldItem.ControlType = DRs[0]["NEEDBOX"].ToString();
                        FieldItem.EditMask = DRs[0]["EDITMASK"].ToString();
                        if (DRs[0]["FIELD_LENGTH"] != null && DRs[0]["FIELD_LENGTH"].ToString() != String.Empty)
                            FieldItem.Length = int.Parse(DRs[0]["FIELD_LENGTH"].ToString());
                        if (DRs[0]["IS_KEY"] != null && DRs[0]["IS_KEY"].ToString() == "Y")
                            FieldItem.IsKey = true;
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
                InternalConnection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, false);
                this.FClientData.ConnString = InternalConnection.ConnectionString;
            }
            else
            {
                if (InternalConnection.State == ConnectionState.Open)
                    InternalConnection.Close();
                InternalConnection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, false);
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
        }

        private void btnNewSubDataset_Click(object sender, EventArgs e)
        {
            InfoBindingSource IBS = new InfoBindingSource();
            System.Windows.Forms.TreeNode Node = tvRelation.SelectedNode;
            if (Node != null)
            {
                TDetailItem DetailItem = (TDetailItem)Node.Tag;
                IBS.DataSource = DetailItem.BindingSource;
                fmSelDetail detail1 = new fmSelDetail();
                DataRelation R = DetailItem.Relation;
                if (detail1.ShowSelDetail(IBS, ref R))
                {
                    System.Windows.Forms.TreeNode Node1 = Node.Nodes.Add(R.ChildTable.TableName);
                    SetNodeData(R, IBS, Node1);
                    UpdatelvSelectedFields((TDetailItem)Node1.Tag);
                }
            }
        }

        private void btnDeleteField_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.TreeNode Node = tvRelation.SelectedNode;
                TDetailItem DetailItem = null;
                if (Node != null)
                {
                    DetailItem = (TDetailItem)Node.Tag;
                }
                for (int I = lvSelectedFields.Items.Count - 1; I >= 0; I--)
                {
                    if (lvSelectedFields.Items[I].Selected)
                    {
                        ListViewItem item2 = lvSelectedFields.Items[I];
                        TBlockFieldItem item3 = (TBlockFieldItem)item2.Tag;
                        ListViewItem.ListViewSubItem LVSI = item2.SubItems[2];
                        if (LVSI.Tag != null)
                            ((System.Windows.Forms.Button)LVSI.Tag).Dispose();
                        DetailItem.BlockFieldItems.Remove(item3);
                        lvSelectedFields.Items.Remove(item2);
                    }
                }

                //foreach (ListViewItem LVI in lvSelectedFields.Items)
                //{
                //    ListViewItem.ListViewSubItem LVSI = LVI.SubItems[2];
                //    if (LVSI.Tag != null)
                //        RearrangeRefValButton((System.Windows.Forms.Button)LVSI.Tag, LVSI.Bounds);
                //}
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message);
            }
        }

        private void tvRelation_AfterSelect(object sender, TreeViewEventArgs e)
        {
            System.Windows.Forms.TreeNode Node = tvRelation.SelectedNode;
            //btnNewSubDataset.Enabled = Node != null;
            //btnDeleteDataset.Enabled = btnNewSubDataset.Enabled;
            //btnNewField.Enabled = btnNewSubDataset.Enabled;
            UpdatelvSelectedFields((TDetailItem)Node.Tag);
        }

        private void EnableFolderControl()
        {
            cbAddToExistFolder.Enabled = rbAddToExistFolder.Checked;
            tbAddToNewFolder.Enabled = rbAddToNewFolder.Checked;
            //cbIonicFolder.Enabled = rbAddToNewFolder.Checked;
        }

        private void rbAddToRootFolder_CheckedChanged(object sender, EventArgs e)
        {
            EnableFolderControl();
        }

        private void rbAddToExistFolder_CheckedChanged(object sender, EventArgs e)
        {
            EnableFolderControl();
        }

        private void rbAddToNewFolder_CheckedChanged(object sender, EventArgs e)
        {
            EnableFolderControl();
        }

        private void cbWebSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAddToExistFolder.Items.Clear();
            foreach (Project P in FDTE2.Solution.Projects)
            {
                if (string.Compare(P.Name, cbWebSite.Text) == 0)
                {
                    cbWebSite.Tag = P.FullName;
                    foreach (ProjectItem PI in P.ProjectItems)
                    {
                        if (string.Compare(PI.Kind, "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}") == 0)
                        {
                            cbAddToExistFolder.Items.Add(PI.Name);
                            //cbIonicFolder.Items.Add(PI.Name);
                        }
                    }
                }
            }
            if (cbAddToExistFolder.Items.Count > 0)
            {
                rbAddToExistFolder.Checked = true;
                rbAddToExistFolder_CheckedChanged(rbAddToExistFolder, null);
            }
            else
            {
                rbAddToNewFolder.Checked = true;
                rbAddToNewFolder_CheckedChanged(rbAddToNewFolder, null);
            }
        }

        private void btnProviderName_Click(object sender, EventArgs e)
        {
            string[] fSelectedList = new string[10];
            string strSelected = "";
            IGetValues aItem = (IGetValues)FInfoDataSet;
            FProviderNameList = aItem.GetValues("RemoteName");
            PERemoteName form = new PERemoteName(FProviderNameList, strSelected);
            if (form.ShowDialog() == DialogResult.OK)
            {
                tbProviderName.Text = form.RemoteName;
            }
        }

        private void tbProviderName_TextChanged(object sender, EventArgs e)
        {
            string ProviderName = tbProviderName.Text;
            if (ProviderName.Trim() == "")
                return;

            ClearAll();

            if (FInfoDataSet != null && FInfoDataSet.Active)
            {
                FInfoDataSet.Active = false;
                FInfoDataSet.Dispose();
                FInfoDataSet = null;
                FInfoDataSet = new InfoDataSet();
                FInfoDataSet.SetWizardDesignMode(true);
            }
            FInfoDataSet.RemoteName = ProviderName;
            FInfoDataSet.ClearWhere();
            FInfoDataSet.SetWhere("1=0");
            FInfoDataSet.Active = true;
            tbTableName.Text = FInfoDataSet.RealDataSet.Tables[0].TableName;
            String DataSetName = FInfoDataSet.RealDataSet.Tables[0].TableName;
            String ModuleName = FInfoDataSet.RemoteName.Substring(0, FInfoDataSet.RemoteName.IndexOf('.'));
            String SolutionName = System.IO.Path.GetFileNameWithoutExtension(FDTE2.Solution.FullName);
            tbTableNameF.Text = CliUtils.GetTableName(ModuleName, DataSetName, SolutionName, "", true);
            tbTableNameF.Text = WzdUtils.RemoveQuote(tbTableNameF.Text, FClientData.DatabaseType);
            cbViewProviderName.Items.Clear();
            string DllName = tbProviderName.Text;
            int Index = DllName.IndexOf('.');
            DllName = DllName.Substring(0, Index + 1);
            for (int I = 0; I < FProviderNameList.Length; I++)
            {
                if (FProviderNameList[I].ToString().IndexOf(DllName) > -1)
                {
                    cbViewProviderName.Items.Add(FProviderNameList[I]);
                }
            }
            cbViewProviderName.SelectedIndex = GetProviderIndex();
            FClientData.ViewProviderName = cbViewProviderName.Text;
            ShowTableRelations();
        }

        private int GetProviderIndex()
        {
            String FindName = "";
            int Result = -1;
            switch (FClientData.Language)
            {
                case "cs":
                    FindName = "View_";
                    break;
                case "vb":
                    FindName = "_View_";
                    break;
            }

            for (int I = 0; I < cbViewProviderName.Items.Count; I++)
            {
                if (cbViewProviderName.Items[I].ToString().IndexOf(FindName) > -1)
                {
                    Result = I;
                    break;
                }
            }
            return Result;
        }

        private void SolutionCheckedChange()
        {
            if (rbCurrentSolution.Checked)
            {
                tbSolutionName.Enabled = false;
                btnSolutionName.Enabled = false;
            }
            if (rbAddToExistSolution.Checked)
            {
                tbSolutionName.Enabled = true;
                btnSolutionName.Enabled = true;
            }
        }

        private void rbCurrentSolution_CheckedChanged(object sender, EventArgs e)
        {
            SolutionCheckedChange();
        }

        private void rbAddToExistSolution_CheckedChanged(object sender, EventArgs e)
        {
            SolutionCheckedChange();
        }

        private void btnViewAdd_Click(object sender, EventArgs e)
        {
            SelectFields(lvViewSrcField, lvViewDesField, false);
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
            //                ((System.Windows.Forms.Button)LVSI.Tag).Dispose();
            //            }
            //        }
            //    }
            //}
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

            fmFieldSetting aForm = new fmFieldSetting(InternalConnection, FClientData.DatabaseType, aViewItem.ListView, TWizardType.wtWebPage, FClientData.DatabaseName);
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
                    //BlockFieldItem.ComboEntityName = Params[5];
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

        private void btnViewRemove_Click(object sender, EventArgs e)
        {
            SelectFields(lvViewDesField, lvViewSrcField, false);
        }

        private void btnViewAddAll_Click(object sender, EventArgs e)
        {
            SelectFields(lvViewSrcField, lvViewDesField, true);
        }

        private void btnViewRemoveAll_Click(object sender, EventArgs e)
        {
            SelectFields(lvViewDesField, lvViewSrcField, true);
        }

        private void lvMasterDesField_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            //ListView LV = (ListView)sender;
            //foreach (ListViewItem LVI in LV.Items)
            //{
            //    ListViewItem.ListViewSubItem LVSI = LVI.SubItems[2];
            //    if (LVSI.Tag != null)
            //    {
            //        System.Windows.Forms.Button B = (System.Windows.Forms.Button)LVSI.Tag;
            //        RearrangeRefValButton(B, LVSI.Bounds);
            //    }
            //}
        }

        private void cbDatabaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FClientData.DatabaseType = (ClientType)cbDatabaseType.SelectedIndex;
        }

        private void tbFormName_TextChanged(object sender, EventArgs e)
        {
            tbFormTitle.Text = tbFormName.Text;
        }

        private void cbViewProviderName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FClientData.ViewProviderName = cbViewProviderName.Text;
        }

        private void lvViewSrcField_ColumnClick(object sender, ColumnClickEventArgs e)
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

            switch ((sender as ListView).Name)
            {
                case "lvViewDesField":
                    btnViewUp.Enabled = false;
                    btnViewDown.Enabled = false;
                    break;
                case "lvMasterDesField":
                    btnMasterUp.Enabled = false;
                    btnMasterDown.Enabled = false;
                    break;
                case "lvSelectedFields":
                    btnDetailUp.Enabled = false;
                    btnDetailDown.Enabled = false;
                    break;
            }
        }

        private void btnViewUp_Click(object sender, EventArgs e)
        {
            WzdUtils.SelectedListViewItemUp(lvViewDesField);
        }

        private void btnViewDown_Click(object sender, EventArgs e)
        {
            WzdUtils.SelectedListViewItemDown(lvViewDesField);
        }

        private void btnMasterUp_Click(object sender, EventArgs e)
        {
            WzdUtils.SelectedListViewItemUp(lvMasterDesField);
        }

        private void btnMasterDown_Click(object sender, EventArgs e)
        {
            WzdUtils.SelectedListViewItemDown(lvMasterDesField);
        }

        private void btnDetailUp_Click(object sender, EventArgs e)
        {
            WzdUtils.SelectedListViewItemUp(lvSelectedFields);
        }

        private void btnDetailDown_Click(object sender, EventArgs e)
        {
            WzdUtils.SelectedListViewItemDown(lvSelectedFields);
        }

        private void cbWebForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            String templateName = (sender as ComboBox).Text;
            switch (templateName)
            {
                case "IonicSingle1":
                    this.label16.Text = templateName + ": ";
                    break;
                case "IonicMasterDetail1":
                    this.label16.Text = templateName + ": ";
                    break;
            }
        }

        private ListView FListView;
        private TBlockFieldItem FSelectedBlockFieldItem;
        private ListViewItem FSelectedListViewItem;
        private Boolean FDisplayValue = false;
        private TWizardType FWizardType;
        private ListViewColumnSorter lvwColumnSorter;
        private void lvMasterDesField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMasterDesField.SelectedItems.Count == 1)
            {
                if (!FDisplayValue)
                    SetValue();

                ListViewItem aViewItem = lvMasterDesField.SelectedItems[0];
                FSelectedListViewItem = aViewItem;
                FSelectedBlockFieldItem = (TBlockFieldItem)aViewItem.Tag;
                FDisplayValue = true;
                DisplayValue();
                FDisplayValue = false;
            }
        }

        private void SetValue()
        {
            if (FSelectedBlockFieldItem == null)
                return;

            FSelectedBlockFieldItem.Description = tbCaption.Text;
            //FSelectedBlockFieldItem.CheckNull = cbCheckNull.Text;
            //FSelectedBlockFieldItem.DefaultValue = tbDefaultValue.Text;
            //FSelectedBlockFieldItem.QueryMode = cbQueryMode.Text;
            //FSelectedBlockFieldItem.EditMask = tbEditMask.Text;
            FSelectedBlockFieldItem.ControlType = cbControlType.Text;
            FSelectedBlockFieldItem.ComboRemoteName = tbComboRemoteName.Text;// cbComboTableName.Text;
            FSelectedBlockFieldItem.ComboEntityName = tbComboTableName.Text;
            FSelectedBlockFieldItem.ComboTextField = cbDataTextField.Text;
            FSelectedBlockFieldItem.ComboValueField = cbDataValueField.Text;
            FSelectedBlockFieldItem.RefValNo = cbRefValNo.Text;

            if (!String.IsNullOrEmpty(tbComboRemoteName.Text))
            {
                if (InternalConnection.State != ConnectionState.Open)
                    InternalConnection.Open();
                //InfoCommand comm = new InfoCommand(FClientData.DatabaseType);
                //comm.Connection = InternalConnection;
                DbCommand comm = InternalConnection.CreateCommand();
                try
                {
                    comm.CommandText = "DELETE FROM SYS_REFVAL WHERE REFVAL_NO='Auto." + tbComboTableName_F.Text + "'";
                    comm.ExecuteNonQuery();
                    comm.CommandText = string.Format("INSERT INTO SYS_REFVAL (REFVAL_NO, TABLE_NAME, DESCRIPTION, DISPLAY_MEMBER, VALUE_MEMBER) VALUES('{0}', '{1}', '{2}', '{3}', '{4}')",
                                                    "Auto." + tbComboTableName_F.Text, FSelectedBlockFieldItem.ComboEntityName, FSelectedBlockFieldItem.ComboRemoteName, FSelectedBlockFieldItem.ComboTextField, FSelectedBlockFieldItem.ComboValueField);
                    comm.ExecuteNonQuery();
                }
                catch (SqlException sex)
                {
                    if (sex.Number == 208)
                    {
                        string sSysDBAlias = WzdUtils.GetSystemDBName();
                        ClientType sSysDBType = WzdUtils.GetSystemDBType();
                        DbConnection sysConn = WzdUtils.AllocateConnection(sSysDBAlias, sSysDBType, false);
                        if (sysConn.State != ConnectionState.Open) sysConn.Open();
                        comm = sysConn.CreateCommand();
                        comm.CommandText = "DELETE FROM SYS_REFVAL WHERE REFVAL_NO='Auto." + tbComboTableName_F.Text + "'";
                        comm.ExecuteNonQuery();
                        comm.CommandText = string.Format("INSERT INTO SYS_REFVAL (REFVAL_NO, TABLE_NAME, DESCRIPTION, DISPLAY_MEMBER, VALUE_MEMBER) VALUES('{0}', '{1}', '{2}', '{3}', '{4}')",
                                                        "Auto." + tbComboTableName_F.Text, FSelectedBlockFieldItem.ComboEntityName, FSelectedBlockFieldItem.ComboRemoteName, FSelectedBlockFieldItem.ComboTextField, FSelectedBlockFieldItem.ComboValueField);
                        comm.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    String tableName = tbComboTableName_F.Text;
                    String OWNER = String.Empty, SS = tableName;
                    if (SS.Contains("."))
                    {
                        OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                        tableName = SS;
                    }
                    comm.CommandText = "SELECT * FROM COLDEF WHERE TABLE_NAME='" + tableName + "' OR TABLE_NAME='" + OWNER + "." + tableName + "'";
                    IDbDataAdapter da = WzdUtils.AllocateDataAdapter(FClientData.DatabaseType);
                    da.SelectCommand = comm;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        FSelectedBlockFieldItem.ComboValueFieldCaption = cbDataTextField.Text;
                        DataRow[] drValueField = ds.Tables[0].Select("FIELD_NAME='" + cbDataValueField.Text + "'");
                        if (drValueField.Length > 0)
                        {
                            FSelectedBlockFieldItem.ComboValueFieldCaption = drValueField[0]["CAPTION"].ToString();
                        }

                        FSelectedBlockFieldItem.ComboTextFieldCaption = cbDataTextField.Text;
                        DataRow[] drTextField = ds.Tables[0].Select("FIELD_NAME='" + cbDataTextField.Text + "'");
                        if (drTextField.Length > 0)
                        {
                            FSelectedBlockFieldItem.ComboTextFieldCaption = drTextField[0]["CAPTION"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DisplayValue()
        {
            if (FSelectedBlockFieldItem == null)
                return;
            tbCaption.Text = FSelectedBlockFieldItem.Description;
            //cbCheckNull.Text = FSelectedBlockFieldItem.CheckNull;
            //tbDefaultValue.Text = FSelectedBlockFieldItem.DefaultValue;
            //if (FSelectedBlockFieldItem.QueryMode == null || FSelectedBlockFieldItem.QueryMode == "")
            //    cbQueryMode.Text = "None";
            //else
            //    cbQueryMode.Text = FSelectedBlockFieldItem.QueryMode;
            //tbEditMask.Text = FSelectedBlockFieldItem.EditMask;
            if (FSelectedBlockFieldItem.ControlType == "" || FSelectedBlockFieldItem.ControlType == null)
                cbControlType.Text = "text";
            else
                cbControlType.Text = FSelectedBlockFieldItem.ControlType;
            tbComboRemoteName.Text = FSelectedBlockFieldItem.ComboRemoteName;//cbComboTableName.Text = FSelectedBlockFieldItem.ComboRemoteName;
            tbComboTableName.Text = FSelectedBlockFieldItem.ComboEntityName;
            cbDataTextField.Text = FSelectedBlockFieldItem.ComboTextField;
            cbDataValueField.Text = FSelectedBlockFieldItem.ComboValueField;
            if (FSelectedBlockFieldItem.RefValNo == "" || FSelectedBlockFieldItem.RefValNo == null)
                cbRefValNo.SelectedIndex = -1;
            else
                cbRefValNo.Text = FSelectedBlockFieldItem.RefValNo;
        }

        private TBlockFieldItem FSelectedBlockFieldItem_D;
        private ListViewItem FSelectedListViewItem_D;
        private Boolean FDisplayValue_D = false;
        private void lvSelectedFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSelectedFields.SelectedItems.Count == 1)
            {
                if (!FDisplayValue_D)
                    SetValue_D();

                ListViewItem aViewItem = lvSelectedFields.SelectedItems[0];
                FSelectedListViewItem_D = aViewItem;
                FSelectedBlockFieldItem_D = (TBlockFieldItem)aViewItem.Tag;
                if (FClientData.BaseFormName == "SingleLabel")
                {
                    if (String.IsNullOrEmpty(FSelectedBlockFieldItem_D.EditMask))
                        FSelectedBlockFieldItem_D.EditMask = "Left";
                }
                else if (FClientData.BaseFormName == "SingleTable")
                {
                    if (String.IsNullOrEmpty(FSelectedBlockFieldItem_D.EditMask))
                    {
                        if (FSelectedBlockFieldItem_D.DataType == typeof(int)
                            || FSelectedBlockFieldItem_D.DataType == typeof(float)
                            || FSelectedBlockFieldItem_D.DataType == typeof(double)
                            || FSelectedBlockFieldItem_D.DataType == typeof(decimal))
                            FSelectedBlockFieldItem_D.EditMask = "Right";
                        else
                            FSelectedBlockFieldItem_D.EditMask = "Left";
                    }
                }
                else if (FClientData.BaseFormName == "MasterDetail")
                {
                    if (String.IsNullOrEmpty(FSelectedBlockFieldItem_D.EditMask))
                    {
                        if (FSelectedBlockFieldItem_D.DataType == typeof(int)
                            || FSelectedBlockFieldItem_D.DataType == typeof(float)
                            || FSelectedBlockFieldItem_D.DataType == typeof(double)
                            || FSelectedBlockFieldItem_D.DataType == typeof(decimal))
                            FSelectedBlockFieldItem_D.EditMask = "Right";
                        else
                            FSelectedBlockFieldItem_D.EditMask = "Left";
                    }
                }
                FDisplayValue_D = true;
                DisplayValue_D();
                FDisplayValue_D = false;
            }
        }

        private void DisplayValue_D()
        {
            if (FSelectedBlockFieldItem_D == null)
                return;
            tbCaption_D.Text = FSelectedBlockFieldItem_D.Description;
            //cbCheckNull_D.Text = FSelectedBlockFieldItem_D.CheckNull;
            //tbDefaultValue_D.Text = FSelectedBlockFieldItem_D.DefaultValue;
            //if (FSelectedBlockFieldItem_D.QueryMode == null || FSelectedBlockFieldItem_D.QueryMode == "")
            //    cbQueryMode_D.Text = "None";
            //else
            //    cbQueryMode_D.Text = FSelectedBlockFieldItem_D.QueryMode;
            //tbEditMask_D.Text = FSelectedBlockFieldItem_D.EditMask;
            if (FSelectedBlockFieldItem_D.ControlType == "" || FSelectedBlockFieldItem_D.ControlType == null)
                cbControlType_D.Text = "text";
            else
                cbControlType_D.Text = FSelectedBlockFieldItem_D.ControlType;
            tbComboRemoteName_D.Text = FSelectedBlockFieldItem_D.ComboRemoteName;
            tbComboTableName_D.Text = FSelectedBlockFieldItem_D.ComboEntityName;
            //cbComboTableName_D.Text = FSelectedBlockFieldItem_D.ComboRemoteName;
            cbComboDisplayField_D.Text = FSelectedBlockFieldItem_D.ComboTextField;
            cbComboValueField_D.Text = FSelectedBlockFieldItem_D.ComboValueField;
            if (FSelectedBlockFieldItem_D.RefValNo == "" || FSelectedBlockFieldItem_D.RefValNo == null)
                cbRefValNo_D.SelectedIndex = -1;
            else
                cbRefValNo_D.Text = FSelectedBlockFieldItem_D.RefValNo;
        }

        private void SetValue_D()
        {
            if (FSelectedBlockFieldItem_D == null)
                return;
            FSelectedBlockFieldItem_D.Description = tbCaption_D.Text;
            //FSelectedBlockFieldItem_D.CheckNull = cbCheckNull_D.Text;
            //FSelectedBlockFieldItem_D.DefaultValue = tbDefaultValue_D.Text;
            FSelectedBlockFieldItem_D.ControlType = cbControlType_D.Text;
            //FSelectedBlockFieldItem_D.EditMask = tbEditMask_D.Text;
            FSelectedBlockFieldItem_D.ComboRemoteName = tbComboRemoteName_D.Text;//cbComboTableName_D.Text;
            FSelectedBlockFieldItem_D.ComboEntityName = tbComboTableName_D.Text;
            FSelectedBlockFieldItem_D.ComboTextField = cbComboDisplayField_D.Text;
            FSelectedBlockFieldItem_D.ComboValueField = cbComboValueField_D.Text;
            FSelectedBlockFieldItem_D.RefValNo = cbRefValNo_D.Text;

            if (!String.IsNullOrEmpty(tbComboRemoteName_D.Text))
            {
                if (InternalConnection.State != ConnectionState.Open)
                    InternalConnection.Open();
                //InfoCommand comm = new InfoCommand(FClientData.DatabaseType);
                //comm.Connection = InternalConnection;
                DbCommand comm = InternalConnection.CreateCommand();
                try
                {
                    comm.Transaction = InternalConnection.BeginTransaction();
                    comm.CommandText = "DELETE FROM SYS_REFVAL WHERE REFVAL_NO='Auto." + tbComboTableName_D_F.Text + "'";
                    comm.ExecuteNonQuery();
                    comm.CommandText = string.Format("INSERT INTO SYS_REFVAL (REFVAL_NO, TABLE_NAME, DESCRIPTION, DISPLAY_MEMBER, VALUE_MEMBER) VALUES('{0}', '{1}', '{2}', '{3}', '{4}')",
                                                    "Auto." + tbComboTableName_D_F.Text, FSelectedBlockFieldItem_D.ComboEntityName, FSelectedBlockFieldItem_D.ComboRemoteName, FSelectedBlockFieldItem_D.ComboTextField, FSelectedBlockFieldItem_D.ComboValueField);
                    comm.ExecuteNonQuery();
                    comm.Transaction.Commit();
                }
                catch (SqlException sex)
                {
                    if (sex.Number == 208)
                    {
                        string sSysDBAlias = WzdUtils.GetSystemDBName();
                        ClientType sSysDBType = WzdUtils.GetSystemDBType();
                        DbConnection sysConn = WzdUtils.AllocateConnection(sSysDBAlias, sSysDBType, false);
                        if (sysConn.State != ConnectionState.Open) sysConn.Open();
                        comm = sysConn.CreateCommand();
                        comm.Transaction = InternalConnection.BeginTransaction();
                        comm.CommandText = "DELETE FROM SYS_REFVAL WHERE REFVAL_NO='Auto." + tbComboTableName_D_F.Text + "'";
                        comm.ExecuteNonQuery();
                        comm.CommandText = string.Format("INSERT INTO SYS_REFVAL (REFVAL_NO, TABLE_NAME, DESCRIPTION, DISPLAY_MEMBER, VALUE_MEMBER) VALUES('{0}', '{1}', '{2}', '{3}', '{4}')",
                                                        "Auto." + tbComboTableName_D_F.Text, FSelectedBlockFieldItem_D.ComboEntityName, FSelectedBlockFieldItem_D.ComboRemoteName, FSelectedBlockFieldItem_D.ComboTextField, FSelectedBlockFieldItem_D.ComboValueField);
                        comm.ExecuteNonQuery();
                        comm.Transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    comm.Transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    String tableName = tbComboTableName_D_F.Text;
                    String OWNER = String.Empty, SS = tableName;
                    if (SS.Contains("."))
                    {
                        OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                        tableName = SS;
                    }
                    comm.CommandText = "SELECT * FROM COLDEF WHERE TABLE_NAME='" + tableName + "' OR TABLE_NAME='" + OWNER + "." + tableName + "'";
                    IDbDataAdapter da = WzdUtils.AllocateDataAdapter(FClientData.DatabaseType);
                    da.SelectCommand = comm;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        FSelectedBlockFieldItem_D.ComboValueFieldCaption = cbComboValueField_D.Text;
                        DataRow[] drValueField = ds.Tables[0].Select("FIELD_NAME='" + cbComboValueField_D.Text + "'");
                        if (drValueField.Length > 0)
                        {
                            FSelectedBlockFieldItem_D.ComboValueFieldCaption = drValueField[0]["CAPTION"].ToString();
                        }

                        FSelectedBlockFieldItem_D.ComboTextFieldCaption = cbComboDisplayField_D.Text;
                        DataRow[] drTextField = ds.Tables[0].Select("FIELD_NAME='" + cbComboDisplayField_D.Text + "'");
                        if (drTextField.Length > 0)
                        {
                            FSelectedBlockFieldItem_D.ComboTextFieldCaption = drTextField[0]["CAPTION"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cbTypeMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).Text == "checkbox" || (sender as ComboBox).Text == "combobox" ||
                (sender as ComboBox).Text == "radiobox" || (sender as ComboBox).Text == "refval")
            {
                gbComboBox.Enabled = true;
            }
            else
            {
                gbComboBox.Enabled = false;
            }

            tbComboRemoteName.Text = String.Empty;
            tbComboTableName.Text = String.Empty;
            tbComboTableName_F.Text = String.Empty;
            cbDataValueField.Items.Clear();
            cbDataTextField.Items.Clear();
            cbDataValueField.Text = String.Empty;
            cbDataTextField.Text = String.Empty;
        }

        private void cbTypeDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).Text == "checkbox" || (sender as ComboBox).Text == "combobox" ||
                (sender as ComboBox).Text == "radiobox" || (sender as ComboBox).Text == "refval")
            {
                gbDetailCombo.Enabled = true;
            }
            else
            {
                gbDetailCombo.Enabled = false;
            }

            tbComboRemoteName_D.Text = String.Empty;
            tbComboTableName_D.Text = String.Empty;
            tbComboTableName_D_F.Text = String.Empty;
            cbComboValueField_D.Items.Clear();
            cbComboDisplayField_D.Items.Clear();
            cbComboValueField_D.Text = String.Empty;
            cbComboDisplayField_D.Text = String.Empty;
        }

        private void btnRefValNo_Click(object sender, EventArgs e)
        {
            IGetValues aItem = (IGetValues)FInfoDataSet;
            FProviderNameList = aItem.GetValues("RemoteName");
            PERemoteName form = new PERemoteName(FProviderNameList, tbComboRemoteName.Text);
            if (form.ShowDialog() == DialogResult.OK)
            {
                tbComboRemoteName.Text = form.RemoteName;
                //if (!String.IsNullOrEmpty(tbComboRemoteName.Text))
                //{
                //    if (InternalConnection.State != ConnectionState.Open)
                //        InternalConnection.Open();
                //    InfoCommand comm = new InfoCommand(FClientData.DatabaseType);
                //    comm.Connection = InternalConnection;
                //    DbCommand comm = InternalConnection.CreateCommand();
                //    try
                //    {
                //        comm.CommandText = "SELECT * FROM SYS_REFVAL WHERE REFVAL_NO='Auto." + form.RemoteName + "'";
                //        IDataAdapter ida = WzdUtils.AllocateDataAdapter(FClientData.DatabaseType);
                //        DbDataReader dr = comm.ExecuteReader();
                //        while (dr.NextResult())
                //        {
                //            MessageBox.Show(dr["VALUE_MEMBER"].ToString());
                //            cbDataValueField.Text = dr["VALUE_MEMBER"] == null ? String.Empty : dr["VALUE_MEMBER"].ToString();
                //            cbDataTextField.Text = dr["DISPLAY_MEMBER"] == null ? String.Empty : dr["DISPLAY_MEMBER"].ToString();
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message);
                //    }
                //}
            }
            //fmRefVal aForm = new fmRefVal(InternalConnection, FClientData.DatabaseType, FClientData.DatabaseName);
            //String RefValNo = aForm.ShowRefValForm();
            //cbRefValNo.Text = RefValNo;
        }

        private void tbComboRemoteName_TextChanged(object sender, EventArgs e)
        {
            string ProviderName = tbComboRemoteName.Text;
            if (ProviderName.Trim() == "")
                return;

            InfoDataSet temp = new InfoDataSet();
            temp.SetWizardDesignMode(true);
            temp.RemoteName = ProviderName;
            temp.SetWhere("1=0");
            temp.Active = true;
            tbComboTableName.Text = temp.RealDataSet.Tables[0].TableName;
            String DataSetName = temp.RealDataSet.Tables[0].TableName;
            String ModuleName = temp.RemoteName.Substring(0, temp.RemoteName.IndexOf('.'));
            String SolutionName = System.IO.Path.GetFileNameWithoutExtension(FDTE2.Solution.FullName);
            tbComboTableName_F.Text = CliUtils.GetTableName(ModuleName, DataSetName, SolutionName, "", true);
            tbComboTableName_F.Text = WzdUtils.RemoveQuote(tbComboTableName_F.Text, FClientData.DatabaseType);

            cbDataValueField.Items.Clear();
            cbDataTextField.Items.Clear();
            DataTable dtTableSchema = temp.RealDataSet.Tables[0];
            foreach (DataColumn item in dtTableSchema.Columns)
            {
                cbDataValueField.Items.Add(item.ColumnName);
                cbDataTextField.Items.Add(item.ColumnName);
            }

            if (InternalConnection.State != ConnectionState.Open)
                InternalConnection.Open();
            DbCommand comm = InternalConnection.CreateCommand();
            try
            {
                comm.CommandText = "SELECT * FROM SYS_REFVAL WHERE REFVAL_NO='Auto." + tbComboTableName_F.Text + "'";
                IDbDataAdapter da = WzdUtils.AllocateDataAdapter(FClientData.DatabaseType);
                da.SelectCommand = comm;
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbDataValueField.Text = ds.Tables[0].Rows[0]["VALUE_MEMBER"] == null ? String.Empty : ds.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
                    cbDataTextField.Text = ds.Tables[0].Rows[0]["DISPLAY_MEMBER"] == null ? String.Empty : ds.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                }
            }
            catch (SqlException sex)
            {
                if (sex.Number == 208)
                {
                    string sSysDBAlias = WzdUtils.GetSystemDBName();
                    ClientType sSysDBType = WzdUtils.GetSystemDBType();
                    DbConnection sysConn = WzdUtils.AllocateConnection(sSysDBAlias, sSysDBType, false);
                    if (sysConn.State != ConnectionState.Open) sysConn.Open();
                    comm = sysConn.CreateCommand();
                    comm.CommandText = "SELECT * FROM SYS_REFVAL WHERE REFVAL_NO='Auto." + tbComboTableName_F.Text + "'";
                    IDbDataAdapter da = WzdUtils.AllocateDataAdapter(sSysDBType);
                    da.SelectCommand = comm;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        cbDataValueField.Text = ds.Tables[0].Rows[0]["VALUE_MEMBER"] == null ? String.Empty : ds.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
                        cbDataTextField.Text = ds.Tables[0].Rows[0]["DISPLAY_MEMBER"] == null ? String.Empty : ds.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefValNo_D_Click(object sender, EventArgs e)
        {
            IGetValues aItem = (IGetValues)FInfoDataSet;
            FProviderNameList = aItem.GetValues("RemoteName");
            PERemoteName form = new PERemoteName(FProviderNameList, tbComboRemoteName.Text);
            if (form.ShowDialog() == DialogResult.OK)
            {
                tbComboRemoteName_D.Text = form.RemoteName;
            }
            //fmRefVal aForm = new fmRefVal(InternalConnection, FClientData.DatabaseType, FClientData.DatabaseName);
            //String RefValNo = aForm.ShowRefValForm();
            //cbRefValNo_D.Text = RefValNo;
        }

        private void tbComboRemoteName_D_TextChanged(object sender, EventArgs e)
        {
            string ProviderName = tbComboRemoteName_D.Text;
            if (ProviderName.Trim() == "")
                return;

            InfoDataSet temp = new InfoDataSet();
            temp.SetWizardDesignMode(true);
            temp.RemoteName = ProviderName;
            temp.SetWhere("1=0");
            temp.Active = true;
            tbComboTableName_D.Text = temp.RealDataSet.Tables[0].TableName;
            String DataSetName = temp.RealDataSet.Tables[0].TableName;
            String ModuleName = temp.RemoteName.Substring(0, temp.RemoteName.IndexOf('.'));
            String SolutionName = System.IO.Path.GetFileNameWithoutExtension(FDTE2.Solution.FullName);
            tbComboTableName_D_F.Text = CliUtils.GetTableName(ModuleName, DataSetName, SolutionName, "", true);
            tbComboTableName_D_F.Text = WzdUtils.RemoveQuote(tbComboTableName_D_F.Text, FClientData.DatabaseType);

            cbComboValueField_D.Items.Clear();
            cbComboDisplayField_D.Items.Clear();
            DataTable dtTableSchema = temp.RealDataSet.Tables[0];
            foreach (DataColumn item in dtTableSchema.Columns)
            {
                cbComboValueField_D.Items.Add(item.ColumnName);
                cbComboDisplayField_D.Items.Add(item.ColumnName);
            }

            if (InternalConnection.State != ConnectionState.Open)
                InternalConnection.Open();
            DbCommand comm = InternalConnection.CreateCommand();
            try
            {
                comm.CommandText = "SELECT * FROM SYS_REFVAL WHERE REFVAL_NO='Auto." + tbComboTableName_D_F.Text + "'";
                IDbDataAdapter da = WzdUtils.AllocateDataAdapter(FClientData.DatabaseType);
                da.SelectCommand = comm;
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbComboValueField_D.Text = ds.Tables[0].Rows[0]["VALUE_MEMBER"] == null ? String.Empty : ds.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
                    cbComboDisplayField_D.Text = ds.Tables[0].Rows[0]["DISPLAY_MEMBER"] == null ? String.Empty : ds.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                }
            }
            catch (SqlException sex)
            {
                if (sex.Number == 208)
                {
                    string sSysDBAlias = WzdUtils.GetSystemDBName();
                    ClientType sSysDBType = WzdUtils.GetSystemDBType();
                    DbConnection sysConn = WzdUtils.AllocateConnection(sSysDBAlias, sSysDBType, false);
                    if (sysConn.State != ConnectionState.Open) sysConn.Open();
                    comm = sysConn.CreateCommand();
                    comm.CommandText = "SELECT * FROM SYS_REFVAL WHERE REFVAL_NO='Auto." + tbComboTableName_D_F.Text + "'";
                    IDbDataAdapter da = WzdUtils.AllocateDataAdapter(sSysDBType);
                    da.SelectCommand = comm;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        cbComboValueField_D.Text = ds.Tables[0].Rows[0]["VALUE_MEMBER"] == null ? String.Empty : ds.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
                        cbComboDisplayField_D.Text = ds.Tables[0].Rows[0]["DISPLAY_MEMBER"] == null ? String.Empty : ds.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class TIonicWizardData : Object
    {
        private string FPackageName, FBaseFormName, FServerPackageName, FFolderName, FTableName, FRealTableName, FFormName, FProviderName,
            FDatabaseName, FSolutionName, FViewProviderName, FWebSiteName, FWebSiteFullName, FFolderMode, FFormTitle;
        private TBlockItems FBlocks;
        private MWizard.fmIonicWizard FOwner;
        private bool FNewSolution = false;
        private string FCodeFolderName;
        private int FColumnCount;
        private ClientType FDatabaseType;
        private String FConnString;
        private String FLanguage = "cs";

        public TIonicWizardData(MWizard.fmIonicWizard Owner)
        {
            FOwner = Owner;
            FBlocks = new TBlockItems(this);
        }

        public ClientType DatabaseType
        {
            get { return FDatabaseType; }
            set { FDatabaseType = value; }
        }

        public fmIonicWizard Owner
        {
            get { return FOwner; }
            set { FOwner = value; }
        }

        public String FormTitle
        {
            get { return FFormTitle; }
            set { FFormTitle = value; }
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
                BlockFieldItemsNode = WzdUtils.FindNode(null, BlockNode, "BlockFieldItems");
                LoadBlockFieldItems(BlockFieldItemsNode, BI.BlockFieldItems);
                Blocks.Add(BI);
            }
        }

        public object LoadFromXML(string XML)
        {
            System.Xml.XmlNode Node = null;
            System.Xml.XmlDocument Doc = new System.Xml.XmlDocument();
            Doc.LoadXml(XML);
            Node = Doc.SelectSingleNode("ClientData");
            SolutionName = Node.Attributes["SolutionName"].Value;
            NewSolution = Node.Attributes["NewSolution"].Value == "1";
            WebSiteName = Node.Attributes["PackageName"].Value;
            BaseFormName = Node.Attributes["BaseFormName"].Value;
            FolderName = Node.Attributes["FolderName"].Value;
            FolderMode = "ExistFolder";
            FormName = Node.Attributes["FormName"].Value;
            TableName = Node.Attributes["TableName"].Value;
            FormTitle = Node.Attributes["FormName"].Value;
            ProviderName = Node.Attributes["ProviderName"].Value;
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
            if (string.Compare(FBaseFormName, "IonicMasterDetail1") == 0)
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

        public string WebSiteName
        {
            get
            {
                return FWebSiteName;
            }
            set
            {
                FWebSiteName = value;
            }
        }

        public string WebSiteFullName
        {
            get
            {
                return FWebSiteFullName;
            }
            set
            {
                FWebSiteFullName = value;
            }
        }

        public string FolderMode
        {
            get
            {
                return FFolderMode;
            }
            set
            {
                FFolderMode = value;
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

        public string FolderName
        {
            get
            {
                return FFolderName;
            }
            set
            {
                FFolderName = value;
            }

        }

        public string CodeFolderName
        {
            get
            {
                return FCodeFolderName;
            }
            set
            {
                string S = value;
                if (S != "")
                    if (S[S.Length - 1] == '\\')
                        S = S.Substring(0, S.Length - 1);
                FCodeFolderName = S;
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

        public string RealTableName
        {
            get
            {
                return FRealTableName;
            }
            set
            {
                FRealTableName = value;
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

    partial class TIonicWizardGenerator : System.ComponentModel.Component
    {
        private TIonicWizardData FClientData;
        private DTE2 FDTE2;
        private AddIn FAddIn;
        private System.Windows.Forms.Form FRootForm = null;
        private System.ComponentModel.Design.IDesignerHost FDesignerHost;
#if VS90
        private WebDevPage.DesignerDocument FDesignerDocument;
#endif
        private TextWindow FTextWindow;
        private InfoDataSet FDataSet = null;
        private ProjectItem FPI;
        private ProjectItem FPIFolder;
        private Project FProject = null;
        private InfoDataGridView FViewGrid = null;
        private System.Web.UI.Page FPage;
        private InfoDataSet FWizardDataSet = null;
        private String FResxFileName;
        private DataSet FSYS_REFVAL;
        private Window FDesignWindow;
        private List<WebDataSource> FWebDataSourceList;
        private List<WebRefVal> FWebRefValList;
        private List<AjaxTools.AjaxRefVal> FAjaxRefValList;
        private List<WebRefVal> FWebRefValListPage;
        private List<WebDefault> FWebDefaultList;
        private List<WebValidate> FWebValidateList;
        private List<ExtComboBox> FExtComboBoxList;
        private List<MyWebDropDownList> FMyWebDropDownList;
        private List<WebDateTimePicker> FWebDateTimePickerList;
        private List<AjaxTools.AjaxDateTimePicker> FAjaxDateTimePickerList;
        private List<WebValidateBox> FWebValidateBoxList;
        private List<System.Web.UI.WebControls.CheckBox> FWebCheckBoxList;
        private List<System.Web.UI.WebControls.TextBox> FWebTextBoxList;
        private List<System.Web.UI.WebControls.Label> FLabelList;

        public TIonicWizardGenerator(TIonicWizardData ClientData, DTE2 dte2, AddIn aAddIn)
        {
            FClientData = ClientData;
            FDTE2 = dte2;
            FAddIn = aAddIn;
            FSYS_REFVAL = new DataSet();
            FWebDataSourceList = new List<WebDataSource>();
            FWebRefValList = new List<WebRefVal>();
            FAjaxRefValList = new List<AjaxTools.AjaxRefVal>();
            FWebRefValListPage = new List<WebRefVal>();
            FWebDefaultList = new List<WebDefault>();
            FWebValidateList = new List<WebValidate>();
            FExtComboBoxList = new List<ExtComboBox>();
            FMyWebDropDownList = new List<MyWebDropDownList>();
            FWebDateTimePickerList = new List<WebDateTimePicker>();
            FAjaxDateTimePickerList = new List<AjaxTools.AjaxDateTimePicker>();
            FWebValidateBoxList = new List<WebValidateBox>();
            FWebCheckBoxList = new List<System.Web.UI.WebControls.CheckBox>();
            FWebTextBoxList = new List<System.Web.UI.WebControls.TextBox>();
            FLabelList = new List<System.Web.UI.WebControls.Label>();
            //???FTemplatePath = Path.GetDirectoryName(FAddIn.Object.GetType().Assembly.Location) + "\\Templates\\";
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

        private void GenFolder()
        {
            Solution2 sln = (Solution2)FDTE2.Solution;

            if (string.Compare(sln.FullName, FClientData.SolutionName) != 0)
            {
                sln.Open(FClientData.SolutionName);
            }
            foreach (Project P in sln.Projects)
            {
                if (String.Compare(P.Kind, "{E24C65DC-7377-472b-9ABA-BC803B73C61A}") == 0)
                {
                    String VSName = P.Name;
                    if (FClientData.Owner.SDCall)
                    {
                        VSName = WzdUtils.FixupToVSWebSiteName(VSName);
                    }
                    if (string.Compare(VSName, FClientData.WebSiteName) == 0)
                    {
                        FProject = P;
                        break;
                    }
                }
            }
            switch (FClientData.FolderMode)
            {
                case "ExistFolder":
                    foreach (ProjectItem PI in FProject.ProjectItems)
                    {
                        if (string.Compare(PI.Name, FClientData.FolderName) == 0)
                        {
                            FPIFolder = PI;
                            break;
                        }
                    }
                    break;
                case "NewFolder":
                    FPIFolder = FProject.ProjectItems.AddFolder(FClientData.FolderName, "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}");
                    break;
                default:
                    break;
            }
        }

        private bool IsFileExisted(ProjectItems ownerProjs, string filename)
        {
            //判断有无已存在的同名文件
            foreach (ProjectItem item in ownerProjs)
            {
                if (item.Name == filename)
                {
                    if (MessageBox.Show("There is another File which name is " + filename + " existed! Do you want to delete it first", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        string Path = item.get_FileNames(0);
                        Path = System.IO.Path.GetDirectoryName(Path);
                        item.Delete();
                    }
                    else
                        return false;
                    break;
                }
            }
            return true;
        }

        private bool GetForm()
        {
            bool flag = true;
            DialogResult dr = DialogResult.No;
            String TemplatePath = String.Empty;
            //TemplatePath = FClientData.WebSiteName + "Template";//EEPRegistry.WebClient + "\\Template";
            if (FClientData.WebSiteFullName.Contains("localhost"))
            {
                String[] webSiteNames = FClientData.WebSiteName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                String[] webClients = EEPRegistry.WebClient.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                String newPath = String.Empty;
                for (int i = 0; i < webClients.Length - 1; i++)
                {
                    newPath += webClients[i] + "\\";
                }
                newPath = System.IO.Path.Combine(newPath, webSiteNames[webSiteNames.Length - 1]);
                TemplatePath = System.IO.Path.Combine(newPath, "Template");
                FClientData.WebSiteFullName = newPath;
            }
            else
            {
                TemplatePath = System.IO.Path.Combine(FClientData.WebSiteFullName, "Template");
            }
            if (TemplatePath == "")
            {
                MessageBox.Show("Cannot find WebTemplate path: {0}", TemplatePath);
                return false;
            }
            if (FPIFolder != null)
            {
                foreach (ProjectItem aPI in FPIFolder.ProjectItems)
                {
                    if (string.Compare(FClientData.FormName + ".html", aPI.Name) == 0)
                    {
                        dr = MessageBox.Show("There is another File which name is " + FClientData.PackageName + " existed! Do you want to delete it first", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            flag = true;
                            break;
                        }
                        else
                        {
                            flag = false;
                            break;
                        }
                    }
                }

                if (flag)
                {
                    if (dr == DialogResult.Yes)
                    {
                        foreach (ProjectItem aPI in FPIFolder.ProjectItems)
                        {
                            if (string.Compare(FClientData.FormName + ".html", aPI.Name) == 0 ||
                                string.Compare(FClientData.FormName + ".js", aPI.Name) == 0 ||
                                string.Compare(FClientData.FormName + "Edit.html", aPI.Name) == 0 ||
                                string.Compare(FClientData.FormName + "Details.html", aPI.Name) == 0 ||
                                string.Compare(FClientData.FormName + "Details.js", aPI.Name) == 0 ||
                                string.Compare(FClientData.FormName + "DetailsEdit.html", aPI.Name) == 0)
                            {
                                string Path = aPI.get_FileNames(0);
                                aPI.Name = Guid.NewGuid().ToString();
                                aPI.Delete();
                                File.Delete(Path);
                            }
                        }
                    }

                    String templateName = "IonicForm";
                    FPI = null;
                    FPI = FPIFolder.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + templateName + ".html");
                    FPI.Name = FClientData.FormName + ".html";
                    FPI = FPIFolder.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + templateName + ".js");
                    FPI.Name = FClientData.FormName + ".js";
                    FPI = FPIFolder.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + templateName + "Edit.html");
                    FPI.Name = FClientData.FormName + "Edit.html";
                    if (FClientData.IsMasterDetailBaseForm())
                    {
                        FPI = FPIFolder.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + templateName + ".html");
                        FPI.Name = FClientData.FormName + "Details.html";
                        FPI = FPIFolder.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + templateName + ".js");
                        FPI.Name = FClientData.FormName + "Details.js";
                        FPI = FPIFolder.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + templateName + "Edit.html");
                        FPI.Name = FClientData.FormName + "DetailsEdit.html";
                    }
                }
                else
                {

                }
            }
            else
            {
                foreach (ProjectItem aPI in FProject.ProjectItems)
                {
                    if (string.Compare(FClientData.FormName + ".html", aPI.Name) == 0)
                    {
                        string Path = aPI.get_FileNames(0);
                        Path = System.IO.Path.GetDirectoryName(Path);
                        aPI.Delete();
                        break;
                    }
                }

                String templateName = "IonicForm";
                FPI = null;
                FPI = FProject.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + templateName + ".html");
                FPI.Name = FClientData.FormName + ".html";
                FPI = FProject.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + templateName + ".js");
                FPI.Name = FClientData.FormName + ".js";
                FPI = FProject.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + templateName + "Edit.html");
                FPI.Name = FClientData.FormName + "Edit.html";
                if (FClientData.IsMasterDetailBaseForm())
                {
                    FPI = FProject.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + templateName + ".html");
                    FPI.Name = FClientData.FormName + "Details.html";
                    FPI = FProject.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + templateName + ".js");
                    FPI.Name = FClientData.FormName + "Details.js";
                    FPI = FProject.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + templateName + "Edit.html");
                    FPI.Name = FClientData.FormName + "DetailsEdit.html";
                }
            }

            return flag;
        }

        public static DataTable GetDDTable(ClientType databaseType, string dbName, string tableName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(SystemFile.DBFile);
            XmlNode nDataBase = doc.SelectSingleNode("InfolightDB/DataBase");
            foreach (XmlNode node in nDataBase.ChildNodes)
            {
                if (node.Name == dbName)
                {
                    string conString = node.Attributes["String"].Value;
                    string password = WzdUtils.GetPwdString(node.Attributes["Password"].Value);
                    if ((conString.Length > 0) && (password.Length > 0) && password != String.Empty)
                    {
                        if (conString[conString.Length - 1] != ';')
                        {
                            conString = conString + ";Password=" + password;
                        }
                        else
                        {
                            conString = conString + "Password=" + password;
                        }
                    }
                    //Connection
                    DbConnection con = WzdUtils.AllocateConnection(dbName, databaseType, false);
                    //Command
                    InfoCommand cmd = new InfoCommand(databaseType);
                    cmd.Connection = con;
                    tableName = WzdUtils.RemoveQuote(tableName, databaseType);
                    String OWNER = String.Empty, SS = tableName;
                    if (SS.Contains("."))
                    {
                        OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                        tableName = SS;
                    }
                    cmd.CommandText = "Select * from COLDEF where TABLE_NAME = '" + tableName + "' OR TABLE_NAME='" + OWNER + "." + tableName + "'";
                    //Adapter
                    IDbDataAdapter adapter = WzdUtils.AllocateDataAdapter(databaseType);
                    adapter.SelectCommand = cmd.GetInternalCommand();
                    DataTable tab = new DataTable();
                    WzdUtils.FillDataAdapter(databaseType, adapter, tab);
                    return tab;
                }
            }

            return null;
        }

        public static string GetFieldCaption(DataTable tabDataDic, string fieldName)
        {
            if (tabDataDic != null && tabDataDic.Rows.Count > 0)
            {
                foreach (DataRow row in tabDataDic.Rows)
                {
                    if (row["FIELD_NAME"] != null && row["CAPTION"] != null
                        && row["FIELD_NAME"].ToString().ToLower() == fieldName.ToLower()
                        && !string.IsNullOrEmpty(row["CAPTION"].ToString()))
                    {
                        return row["CAPTION"].ToString();
                    }
                }
            }
            return fieldName;
        }

        DataTable GetDesignTable(WebDataSource wds)
        {
            if (wds != null)
            {
                WebDataSet ds = null;
                if (wds.DesignDataSet == null)
                {
                    ds = WebDataSet.CreateWebDataSet(wds.WebDataSetID);
                }
                if (wds.DesignDataSet != null && wds.DesignDataSet.Tables.Contains(wds.DataMember))
                {
                    return wds.DesignDataSet.Tables[wds.DataMember];
                }
                else
                {
                    return ds.RealDataSet.Tables[wds.DataMember];
                }
            }
            return null;
        }

        private void SetBlockFieldControls(TBlockItem BlockItem)
        {
            if (BlockItem.Name == "Main" || BlockItem.Name == "Master")
            {
                if (FClientData.BaseFormName == "IonicSingle1")
                {
                    GenIonicSingle(BlockItem, new TBlockItem());
                }
                else if (FClientData.BaseFormName == "IonicMasterDetail1")
                {
                    TBlockItem BlockItemMaster = BlockItem;
                    BlockItemMaster.wDataSource = new WebDataSource();
                    TBlockItem BlockItemDetail = null;
                    foreach (TBlockItem B in FClientData.Blocks)
                    {
                        if (B.wDataSource == null && B.BlockFieldItems.Count > 0)
                        {
                            BlockItemDetail = B;
                            break;
                        }
                    }
                    GenIonicSingle(BlockItem, BlockItemDetail);
                    GenIonicSingle(BlockItemDetail, null);
                }
            }
        }

        private void GenIonicSingle(TBlockItem BlockItem, TBlockItem detailBlockItem)
        {
            String htmlName, jsName, editName;
            String mainPath = String.Empty;
            if (FClientData.WebSiteFullName.Contains("localhost"))
            {
                mainPath = EEPRegistry.WebClient;
            }
            else
            {
                mainPath = FClientData.WebSiteFullName;
            }
            String detailFixed = "";
            if (detailBlockItem == null) detailFixed = "Details";
            htmlName = System.IO.Path.Combine(mainPath, FClientData.FolderName, FClientData.FormName + detailFixed + ".html");
            jsName = System.IO.Path.Combine(mainPath, FClientData.FolderName, FClientData.FormName + detailFixed + ".js");
            editName = System.IO.Path.Combine(mainPath, FClientData.FolderName, FClientData.FormName + detailFixed + "Edit.html");

            String content = String.Empty;
            String dataMember = FClientData.ProviderName.Split('.')[1];
            //htmlName
            using (FileStream fs = File.Open(htmlName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                String columns = "[\n";
                //{'fieldName':'CustomerID','caption':'客戶編號'},
                foreach (TBlockFieldItem item in BlockItem.BlockFieldItems)
                {
                    columns += String.Format("{{'fieldName':'{0}', 'caption':'{1}'}},\n", item.DataField, item.Description);
                }
                columns = columns.Remove(columns.LastIndexOf(","));
                columns += "]";

                StreamReader reader = new StreamReader(fs);
                content = reader.ReadToEnd();
                content = content.Replace("[]", columns);
                reader.Close();
                fs.Close();
            }
            using (StreamWriter writer = new StreamWriter(htmlName, false, Encoding.UTF8))
            {
                writer.Write(content);
                writer.Close();
            }
            //jsName
            using (FileStream fs = File.Open(jsName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                String providerName = BlockItem.ProviderName;
                if (String.IsNullOrEmpty(providerName))
                {
                    providerName = FClientData.ProviderName.Split('.')[0] + "." + BlockItem.TableName;
                }
                StreamReader reader = new StreamReader(fs);
                content = reader.ReadToEnd();
                content = content.Replace("IonicSingle", FClientData.FormName + detailFixed);
                content = content.Replace("FolderName", FClientData.FolderName);
                content = content.Replace("realRemoteName", "'" + providerName + "'");
                content = content.Replace("realDataMember", "'" + BlockItem.TableName + "'");
                content = content.Replace("FormEdit", FClientData.FormName + detailFixed + "Edit");

                
                if (FClientData.IsMasterDetailBaseForm())
                {
                    if (detailBlockItem != null)
                    {
                        String keyStr = String.Empty;
                        for (int i = 0; i < detailBlockItem.Relation.ParentColumns.Length; i++)
                        {
                            keyStr += String.Format("{0}='\" + $scope.pageData.{0} + \"' AND ", detailBlockItem.Relation.ParentColumns[i].ColumnName);
                        }
                        keyStr = keyStr.Remove(keyStr.LastIndexOf(" AND "));

                        String details = "//Details\n"
                                         + "$scope.gotoDetails = function () {\n"
                                         + "      $scope.modal.hide();\n"
                                         + "      $rootScope.detailUrl = \"" + FClientData.FolderName + "/" + FClientData.FormName + "Details.html\";\n"
                                         + "      $state.go(\"detailPage\", { CAPTION: \"" + FClientData.FormName + "Details\", PACKAGE: \"" + FClientData.FolderName + "\", FORM: \"" + FClientData.FormName + "Details\", WHERESTRING: \"" + keyStr + "\" });\n"
                                         + "  }"
                                         + "//Details";
                        content = content.Replace("//Details", details);
                    }
                }
                reader.Close();
                fs.Close();
            }
            using (StreamWriter writer = new StreamWriter(jsName, false, Encoding.UTF8))
            {
                writer.Write(content);
                writer.Close();
            }
            //editName
            using (FileStream fs = File.Open(editName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                String columns = "[\n";
                //{'fieldName':'CustomerID','caption':'客戶編號'},
                foreach (TBlockFieldItem item in BlockItem.BlockFieldItems)
                {
                    //columns += String.Format("{{'fieldName':'{0}', 'caption':'{1}'}},\n", item.DataField, item.Description);
                    columns += InitEditColumn(item);
                }
                columns = columns.Remove(columns.LastIndexOf(","));
                columns += "]";

                StreamReader reader = new StreamReader(fs);
                content = reader.ReadToEnd();
                content = content.Replace("[]", columns);
                if (FClientData.IsMasterDetailBaseForm() && detailBlockItem != null)
                {
                    content = content.Replace("<!--Detail-->", "<button class=\"button button-positive\" ng-click=\"gotoDetails()\">Details</button>");
                }
                reader.Close();
                fs.Close();
            }
            using (StreamWriter writer = new StreamWriter(editName, false, Encoding.UTF8))
            {
                writer.Write(content);
                writer.Close();
            }
        }

        private string InitEditColumn(TBlockFieldItem column)
        {
            String type = "text";
            if (!String.IsNullOrEmpty(column.ControlType))
                type = column.ControlType;


            String str = "{";
            str += String.Format("'fieldName':'{0}', 'caption':'{1}','type':'{2}'", column.DataField, column.Description, type);

            String extraAttrs = String.Empty;
            if (type == "checkbox" || type == "combobox" || type == "radiobox" || type == "refval")
            {
                extraAttrs = String.Format(",\n'remoteName':'{0}','dataMember':'{1}','valueMember':'{2}','displayMember':'{3}'",
                                column.ComboRemoteName, column.ComboEntityName, column.ComboValueField, column.ComboTextField);
                str += extraAttrs;
                if (type == "combobox" || type == "refval")
                {
                    String extraColumns = String.Empty;
                    extraColumns = String.Format(",\n'columns':[{{'fieldName':'{0}','caption':'{1}'}},{{'fieldName':'{2}','caption':'{3}'}}]",
                                    column.ComboValueField, column.ComboValueFieldCaption, column.ComboTextField, column.ComboTextFieldCaption);
                    str += extraColumns;
                }
            }

            str += "},\n";
            return str;
        }

        [DllImport("kernel32.dll")]
        public extern static void Sleep(uint msec);

        public void GenWebClientModule()
        {
            GenFolder();
            if (GetForm())
            {
                try
                {
                    TBlockItem BlockItem;
                    if (FClientData.IsMasterDetailBaseForm())
                    {
                        BlockItem = FClientData.Blocks.FindItem("Master");
                        SetBlockFieldControls(BlockItem);
                    }
                    else
                    {
                        BlockItem = FClientData.Blocks.FindItem("Main");
                        SetBlockFieldControls(BlockItem);
                    }
                    //WriteWebDataSourceHTML();
                }
                catch (Exception exception2)
                {
                    MessageBox.Show(exception2.Message);
                    return;
                }
                finally
                {
#if VS90
                    //FPI.Name = FClientData.FormName + ".js";
                    //FDesignWindow = FPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
                    //FDesignWindow.Activate();
                    //bool b = FDesignerDocument.execCommand("Refresh", true, "");
#else
                    transaction1.Commit();
#endif
                }
                //RenameForm();
                //FPI.Save(FPI.get_FileNames(0));
                //GlobalWindow.Close(vsSaveChanges.vsSaveChangesYes);
                FProject.Save(FProject.FullName);
            }
        }
    }

}

//常数 值 说明 
//vsViewKindAny {FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF} 任何视图。确定是否在任何上下文中打开项。仅适用于 IsOpen 属性。 
//vsViewKindCode {7651A701-06E5-11D1-8EBD-00A0C90F26EA} “代码”视图。 
//vsViewKindDebugging {7651A700-06E5-11D1-8EBD-00A0C90F26EA} “调试器”视图。 
//vsViewKindDesigner {7651A702-06E5-11D1-8EBD-00A0C90F26EA} “设计器”视图。 
//vsViewKindPrimary {00000000-0000-0000-0000-000000000000} “主”视图。即，项的默认视图。 
//vsViewKindTextView {7651A703-06E5-11D1-8EBD-00A0C90F26EA} “正文”视图。 
