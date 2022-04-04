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
using JQMobileTools;
//using JQClientTools;
#endif


namespace MWizard
{
    public partial class fmJQMobileForm : Form
    {
        private TJQMobileFormData FClientData;
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

        public fmJQMobileForm()
        {
            InitializeComponent();
            FClientData = new TJQMobileFormData(this);
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

        public fmJQMobileForm(DTE2 aDTE2, AddIn aAddIn)
        {
            InitializeComponent();
            FClientData = new TJQMobileFormData(this);
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
            tbAddToNewFolder.Text = "";
            rbAddToRootFolder_CheckedChanged(rbAddToRootFolder, null);
            tbTableName.Text = "";
            tbTableNameF.Text = "";
            tbProviderName.Text = "";
            tbFormName.Text = "Form1";
            tbDetailTableName.Text = "";
            cbViewProviderName.Items.Clear();
            cbViewProviderName.Text = "";
            cbWebForm.Text = "JQMobileSingle1";
            ClearAll();
        }

        private void ClearAll()
        {
            lvViewSrcField.Items.Clear();
            lvViewDesField.Items.Clear();
            tbCaption.Text = "";
            cbCheckNull.Text = "";
            tbDefaultValue.Text = "";
            cbQueryMode.Text = "";
            tbEditMask.Text = "";
            cbControlType.Text = "";
            cbComboTableName.Text = "";
            cbComboTableName.Items.Clear();
            cbDataTextField.Text = "";
            cbDataValueField.Text = "";
            cbRefValNo.Text = "";
            cbRefValNo.Items.Clear();
            //ClearRefValButton(lvMasterDesField);
            lvMasterDesField.Items.Clear();
            lvMasterSrcField.Items.Clear();
            FClientData.Blocks.Clear();
            tvRelation.Nodes.Clear();
            tbCaption_D.Text = "";
            cbCheckNull_D.Text = "";
            tbDefaultValue_D.Text = "";
            cbQueryMode_D.Text = "";
            tbEditMask_D.Text = "";
            cbControlType_D.Text = "";
            cbComboTableName_D.Text = "";
            cbComboTableName_D.Items.Clear();
            cbComboDisplayField_D.Text = "";
            cbComboValueField_D.Text = "";
            cbRefValNo_D.Text = "";
            cbRefValNo_D.Items.Clear();
            //ClearRefValButton(lvSelectedFields);
            lvSelectedFields.Items.Clear();
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
            if ((fmJQMobileForm._serverPath == null) || (fmJQMobileForm._serverPath.Length == 0))
            {
                fmJQMobileForm._serverPath = EEPRegistry.Server + "\\";
            }
            return fmJQMobileForm._serverPath;
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

        public void ShowJQMobileForm()
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
            TJQMobileFormGenerator CG = new TJQMobileFormGenerator(FClientData, FDTE2, FAddIn);
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
                    aBlockFieldItem.EditMask = DR["EDITMASK"].ToString();
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
                            this.cbWebForm.Items.Add("JQMobileSingle1");
                            //this.cbWebForm.Items.Add("JQMobileSingle2");
                            this.cbWebForm.Items.Add("JQMobileMasterDetail1");
                            this.cbWebForm.Items.Add("JQMobileQuery");
                            this.cbWebForm.SelectedIndex = 0;
                            break;
                        case "VB":
                            this.cbWebForm.Items.Clear();
                            this.cbWebForm.Items.Add("VBJQMobileSingle1");
                            //this.cbWebForm.Items.Add("VBJQMobileSingle2");
                            this.cbWebForm.Items.Add("VBJQMobileMasterDetail1");
                            this.cbWebForm.Items.Add("VBJQMobileQuery");
                            this.cbWebForm.SelectedIndex = 0;
                            break;
                    }
                }
                else
                {
                    this.cbWebForm.Items.Clear();
                    this.cbWebForm.Items.Add("JQMobileSingle1");
                    //this.cbWebForm.Items.Add("JQMobileSingle2");
                    this.cbWebForm.Items.Add("JQMobileMasterDetail1");
                    this.cbWebForm.Items.Add("JQMobileQuery");
                    this.cbWebForm.Items.Add("VBJQMobileSingle1");
                    //this.cbWebForm.Items.Add("VBJQMobileSingle2");
                    this.cbWebForm.Items.Add("VBJQMobileMasterDetail1");
                    this.cbWebForm.Items.Add("VBJQMobileQuery");
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
                    if (FClientData.BaseFormName == "JQMobileSingle1" || FClientData.BaseFormName == "JQMobileMasterDetail1"
                        || FClientData.BaseFormName == "VBJQMobileSingle1" || FClientData.BaseFormName == "VBJQMobileMasterDetail1")
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
            TJQMobileFormGenerator Generator = new TJQMobileFormGenerator(FClientData, FDTE2, FAddIn);
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
                if (FClientData.BaseFormName == "JQMobileMasterDetail1" || FClientData.BaseFormName == "VBJQMobileMasterDetail1")
                    AddBlockItem("View", FClientData.ProviderName, FClientData.TableName, lvViewDesField);
                AddBlockItem("Master", FClientData.ProviderName, FClientData.TableName, lvMasterDesField);
                AddDetailBlockItem("Master", tvRelation.Nodes, lvSelectedFields);
            }
            else
            {
                if (FClientData.BaseFormName == "JQMobileSingle1" || FClientData.BaseFormName == "VBJQMobileSingle1")
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
            TJQMobileFormGenerator G = new TJQMobileFormGenerator(FClientData, FDTE2, FAddIn);
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
                case "JQMobileSingle1":
                case "VBJQMobileSingle1":
                    this.label16.Text = templateName + ": JQueryGridview";
                    break;
                //case "JQMobileSingle2":
                //case "VBJQMobileSingle2":
                //    this.label16.Text = templateName + ": JQueryGridview + JQueryFormview";
                //    break;
                case "JQMobileMasterDetail1":
                case "VBJQMobileMasterDetail1":
                    this.label16.Text = templateName + ": JQueryGridview(Master) + JQueryFormview(Master) + JQueryGridview(Detail)";
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

            //if (cbControlType.Text == "ComboBox" || cbControlType.Text == "ComboGrid" || cbControlType.Text == "RefValBox")
            //{
            //    if (String.IsNullOrEmpty(tbComboRemoteName.Text) || String.IsNullOrEmpty(tbComboTableName.Text)
            //        || String.IsNullOrEmpty(cbDataTextField.Text) || String.IsNullOrEmpty(cbDataValueField.Text))
            //    {
            //        MessageBox.Show(cbControlType.Text + " Set is not complete!");
            //        return;
            //    }
            //}

            FSelectedBlockFieldItem.Description = tbCaption.Text;
            FSelectedBlockFieldItem.CheckNull = cbCheckNull.Text;
            FSelectedBlockFieldItem.DefaultValue = tbDefaultValue.Text;
            FSelectedBlockFieldItem.QueryMode = cbQueryMode.Text;
            FSelectedBlockFieldItem.EditMask = tbEditMask.Text;
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
            cbCheckNull.Text = FSelectedBlockFieldItem.CheckNull;
            tbDefaultValue.Text = FSelectedBlockFieldItem.DefaultValue;
            if (FSelectedBlockFieldItem.QueryMode == null || FSelectedBlockFieldItem.QueryMode == "")
                cbQueryMode.Text = "None";
            else
                cbQueryMode.Text = FSelectedBlockFieldItem.QueryMode;
            tbEditMask.Text = FSelectedBlockFieldItem.EditMask;
            if (FSelectedBlockFieldItem.ControlType == "" || FSelectedBlockFieldItem.ControlType == null)
                cbControlType.Text = "TextBox";
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

        private void cbControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbComboRemoteName.Text = String.Empty;
            tbComboTableName.Text = String.Empty;
            tbComboTableName_F.Text = String.Empty;
            cbDataValueField.Items.Clear();
            cbDataTextField.Items.Clear();
            cbDataValueField.Text = String.Empty;
            cbDataTextField.Text = String.Empty;

            if ((sender as ComboBox).SelectedItem.ToString() == "ComboBox")
            {
                cbComboTableName.Enabled = true;
                cbDataValueField.Enabled = true;
                cbDataTextField.Enabled = true;
                cbRefValNo.Enabled = true;
                btnRefValNo.Enabled = true;
                tbComboRemoteName.Enabled = true;
                tbComboTableName.Enabled = true;
            }

            else if ((sender as ComboBox).SelectedItem.ToString() == "RefValBox")
            {
                cbComboTableName.Enabled = true;
                cbDataValueField.Enabled = true;
                cbDataTextField.Enabled = true;
                cbRefValNo.Enabled = true;
                btnRefValNo.Enabled = true;
                tbComboRemoteName.Enabled = true;
                tbComboTableName.Enabled = true;
            }
            else if ((sender as ComboBox).SelectedItem.ToString() == "RadioButton")
            {
                cbComboTableName.Enabled = true;
                cbDataValueField.Enabled = true;
                cbDataTextField.Enabled = true;
                cbRefValNo.Enabled = true;
                btnRefValNo.Enabled = true;
                tbComboRemoteName.Enabled = true;
                tbComboTableName.Enabled = true;
            }
            else if ((sender as ComboBox).SelectedItem.ToString() == "CheckBox")
            {
                cbComboTableName.Enabled = true;
                cbDataValueField.Enabled = true;
                cbDataTextField.Enabled = true;
                cbRefValNo.Enabled = true;
                btnRefValNo.Enabled = true;
                tbComboRemoteName.Enabled = true;
                tbComboTableName.Enabled = true;
            }
            else
            {
                cbComboTableName.Enabled = false;
                cbDataValueField.Enabled = false;
                cbDataTextField.Enabled = false;
                cbRefValNo.Enabled = false;
                btnRefValNo.Enabled = false;
                btnRefValNo.Text = String.Empty;
                tbComboRemoteName.Enabled = false;
                tbComboTableName.Enabled = false;
            }
        }

        private void cbComboTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strTabelName = cbComboTableName.Text;
            if (strTabelName == "")
                return;
            else
            {
                if (strTabelName.Contains(" "))
                {
                    if (FClientData.DatabaseType == ClientType.ctMsSql)
                        strTabelName = String.Format("[{0}]", strTabelName);
                }
            }

            cbDataTextField.Items.Clear();
            cbDataValueField.Items.Clear();
            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = InternalConnection;
            aInfoCommand.CommandText = String.Format("Select * from {0} where 1=0", strTabelName);
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet aDataSet = new DataSet();
            try
            {
                WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, strTabelName);

                foreach (DataColumn DC in aDataSet.Tables[0].Columns)
                {
                    cbDataTextField.Items.Add(DC.ColumnName);
                    cbDataValueField.Items.Add(DC.ColumnName);
                }
                cbDataTextField.Items.Add("");
                cbDataValueField.Items.Add("");
            }
            catch
            {
                MessageBox.Show(cbComboTableName.Text + " is a illegal table.");
                strTabelName = String.Empty;
            }
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

        private ListView FListView_D;
        private TBlockFieldItem FSelectedBlockFieldItem_D;
        private ListViewItem FSelectedListViewItem_D;
        private Boolean FDisplayValue_D = false;
        private TWizardType FWizardType_D;
        private void lvSelectedFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSelectedFields.SelectedItems.Count == 1)
            {
                if (!FDisplayValue_D)
                    SetValue_D();

                ListViewItem aViewItem = lvSelectedFields.SelectedItems[0];
                FSelectedListViewItem_D = aViewItem;
                FSelectedBlockFieldItem_D = (TBlockFieldItem)aViewItem.Tag;
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
            cbCheckNull_D.Text = FSelectedBlockFieldItem_D.CheckNull;
            tbDefaultValue_D.Text = FSelectedBlockFieldItem_D.DefaultValue;
            if (FSelectedBlockFieldItem_D.QueryMode == null || FSelectedBlockFieldItem_D.QueryMode == "")
                cbQueryMode_D.Text = "None";
            else
                cbQueryMode_D.Text = FSelectedBlockFieldItem_D.QueryMode;
            tbEditMask_D.Text = FSelectedBlockFieldItem_D.EditMask;
            if (FSelectedBlockFieldItem_D.ControlType == "" || FSelectedBlockFieldItem_D.ControlType == null)
                cbControlType_D.Text = "TextBox";
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
            FSelectedBlockFieldItem_D.CheckNull = cbCheckNull_D.Text;
            FSelectedBlockFieldItem_D.DefaultValue = tbDefaultValue_D.Text;
            FSelectedBlockFieldItem_D.ControlType = cbControlType_D.Text;
            FSelectedBlockFieldItem_D.EditMask = tbEditMask_D.Text;
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

        private void cbControlType_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbComboRemoteName_D.Text = String.Empty;
            tbComboTableName_D.Text = String.Empty;
            tbComboTableName_D_F.Text = String.Empty;
            cbComboValueField_D.Items.Clear();
            cbComboDisplayField_D.Items.Clear();
            cbComboValueField_D.Text = String.Empty;
            cbComboDisplayField_D.Text = String.Empty;

            if ((sender as ComboBox).SelectedItem.ToString() == "ComboBox"
                || (sender as ComboBox).SelectedItem.ToString() == "RefValBox" || (sender as ComboBox).SelectedItem.ToString() == "RadioButton" ||
                (sender as ComboBox).SelectedItem.ToString() == "CheckBox")
            {
                cbComboTableName_D.Enabled = true;
                cbComboValueField_D.Enabled = true;
                cbComboDisplayField_D.Enabled = true;
                cbRefValNo_D.Enabled = false;
                btnRefValNo_D.Enabled = true;
                tbComboRemoteName_D.Enabled = true;
                tbComboTableName_D.Enabled = true;

                if (cbComboTableName_D.Items.Count == 0)
                {
                    cbComboTableName_D.Items.Add("");

                    if (FClientData.DatabaseType != ClientType.ctInformix)
                    {
                        String[] Params = null;
                        String ViewFieldName = "TABLE_NAME";
                        if (FClientData.DatabaseType == ClientType.ctOracle)
                        {
                            String UserID = WzdUtils.GetFieldParam(InternalConnection.ConnectionString.ToLower(), "user id");
                            Params = new String[] { UserID.ToUpper() };
                            ViewFieldName = "VIEW_NAME";
                        }
                        DataTable T = InternalConnection.GetSchema("Tables", Params);
                        SortedList<String, String> sTable = new SortedList<String, String>();
                        foreach (DataRow DR in T.Rows)
                        {
                            sTable.Add(DR["TABLE_NAME"].ToString(), DR["TABLE_NAME"].ToString());
                        }

                        DataTable D1 = InternalConnection.GetSchema("Views", Params);
                        foreach (DataRow DR in D1.Rows)
                        {
                            if (!sTable.ContainsKey(DR[ViewFieldName].ToString()))
                                sTable.Add(DR[ViewFieldName].ToString(), DR[ViewFieldName].ToString());
                        }

                        foreach (var item in sTable)
                            cbComboTableName_D.Items.Add(item.Key);
                    }
                    else
                    {
                        List<String> allTables = WzdUtils.GetAllTablesList(InternalConnection, ClientType.ctInformix);
                        allTables.Sort();
                        foreach (String str in allTables)
                            cbComboTableName_D.Items.Add(str);
                    }
                }

                if (cbRefValNo_D.Items.Count == 0)
                {
                    try
                    {
                        InfoCommand FInfoCommand = new InfoCommand(FClientData.DatabaseType);
                        FInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                        FInfoCommand.CommandText = "Select REFVAL_NO from SYS_REFVAL";
                        IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(FInfoCommand);
                        DataSet aDataSet = new DataSet();
                        WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, "SYS_REFVAL");
                        DataTable aDataTable = aDataSet.Tables[0];
                        foreach (DataRow DR in aDataTable.Rows)
                        {
                            cbRefValNo_D.Items.Add(DR["REFVAL_NO"].ToString());
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
                            DbCommand comm = sysConn.CreateCommand();
                            comm.CommandText = "Select REFVAL_NO from SYS_REFVAL";
                            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(comm);
                            DataSet aDataSet = new DataSet();
                            WzdUtils.FillDataAdapter(sSysDBType, DA, aDataSet, "SYS_REFVAL");
                            DataTable aDataTable = aDataSet.Tables[0];
                            foreach (DataRow DR in aDataTable.Rows)
                            {
                                cbRefValNo_D.Items.Add(DR["REFVAL_NO"].ToString());
                            }
                        }
                    }
                }
            }
            else if ((sender as ComboBox).SelectedItem.ToString() == "RefValBox")
            {
                cbComboTableName_D.Enabled = false;
                cbComboValueField_D.Enabled = false;
                cbComboDisplayField_D.Enabled = false;
                cbRefValNo_D.Enabled = true;
                btnRefValNo_D.Enabled = true;
                tbComboRemoteName_D.Enabled = true;
                tbComboTableName_D.Enabled = true;
                if (cbRefValNo_D.Items.Count == 0)
                {
                    try
                    {
                        InfoCommand FInfoCommand = new InfoCommand(FClientData.DatabaseType);
                        FInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                        FInfoCommand.CommandText = "Select REFVAL_NO from SYS_REFVAL";
                        IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(FInfoCommand);
                        DataSet aDataSet = new DataSet();
                        WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, "SYS_REFVAL");
                        DataTable aDataTable = aDataSet.Tables[0];
                        foreach (DataRow DR in aDataTable.Rows)
                        {
                            cbRefValNo_D.Items.Add(DR["REFVAL_NO"].ToString());
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
                            DbCommand comm = sysConn.CreateCommand();
                            comm.CommandText = "Select REFVAL_NO from SYS_REFVAL";
                            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(comm);
                            DataSet aDataSet = new DataSet();
                            WzdUtils.FillDataAdapter(sSysDBType, DA, aDataSet, "SYS_REFVAL");
                            DataTable aDataTable = aDataSet.Tables[0];
                            foreach (DataRow DR in aDataTable.Rows)
                            {
                                cbRefValNo_D.Items.Add(DR["REFVAL_NO"].ToString());
                            }
                        }
                    }
                }
            }
            else
            {
                cbComboTableName_D.Enabled = false;
                cbComboValueField_D.Enabled = false;
                cbComboDisplayField_D.Enabled = false;
                cbRefValNo_D.Enabled = false;
                btnRefValNo_D.Enabled = false;
                btnRefValNo_D.Text = String.Empty;
                tbComboRemoteName_D.Enabled = false;
                tbComboTableName_D.Enabled = false;
            }
        }

        private void cbComboTableName_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbComboTableName_D.Text == "")
                return;
            cbComboDisplayField_D.Items.Clear();
            cbComboValueField_D.Items.Clear();
            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = InternalConnection;
            aInfoCommand.CommandText = String.Format("Select * from {0} where 1=0", cbComboTableName_D.Text);
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet aDataSet = new DataSet();
            try
            {
                WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, cbComboTableName_D.Text);

                foreach (DataColumn DC in aDataSet.Tables[0].Columns)
                {
                    cbComboDisplayField_D.Items.Add(DC.ColumnName);
                    cbComboValueField_D.Items.Add(DC.ColumnName);
                }
                cbComboDisplayField_D.Items.Add("");
                cbComboValueField_D.Items.Add("");
            }
            catch
            {
                MessageBox.Show(cbComboTableName_D.Text + " is a illegal table.");
                cbComboTableName_D.Text = String.Empty;
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

        private void cbRefValNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String sRefValNo = (sender as ComboBox).Text;
            if (cbControlType.Text == "ComboBox" && !String.IsNullOrEmpty(sRefValNo))
            {
                try
                {
                    InfoCommand FInfoCommand = new InfoCommand(FClientData.DatabaseType);
                    FInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                    FInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL WHERE REFVAL_NO='{0}'", sRefValNo);
                    IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(FInfoCommand);
                    DataSet aDataSet = new DataSet();
                    WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, "SYS_REFVAL");
                    DataTable aDataTable = aDataSet.Tables[0];
                    foreach (DataRow DR in aDataTable.Rows)
                    {
                        cbComboTableName.Text = DR["TABLE_NAME"].ToString();
                        cbDataValueField.Text = DR["VALUE_MEMBER"].ToString();
                        cbDataTextField.Text = DR["DISPLAY_MEMBER"].ToString();
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
                        DbCommand comm = sysConn.CreateCommand();
                        comm.CommandText = String.Format("Select * from SYS_REFVAL WHERE REFVAL_NO='{0}'", sRefValNo);
                        IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(comm);
                        DataSet aDataSet = new DataSet();
                        WzdUtils.FillDataAdapter(sSysDBType, DA, aDataSet, "SYS_REFVAL");
                        DataTable aDataTable = aDataSet.Tables[0];
                        foreach (DataRow DR in aDataTable.Rows)
                        {
                            cbComboTableName.Text = DR["TABLE_NAME"].ToString();
                            cbDataValueField.Text = DR["VALUE_MEMBER"].ToString();
                            cbDataTextField.Text = DR["DISPLAY_MEMBER"].ToString();
                        }
                    }
                }
            }
        }

        private void cbRefValNo_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            String sRefValNo = (sender as ComboBox).Text;
            if (cbControlType_D.Text == "ComboBox" && !String.IsNullOrEmpty(sRefValNo))
            {
                try
                {
                    InfoCommand FInfoCommand = new InfoCommand(FClientData.DatabaseType);
                    FInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                    FInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL WHERE REFVAL_NO='{0}'", sRefValNo);
                    IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(FInfoCommand);
                    DataSet aDataSet = new DataSet();
                    WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, "SYS_REFVAL");
                    DataTable aDataTable = aDataSet.Tables[0];
                    foreach (DataRow DR in aDataTable.Rows)
                    {
                        cbComboTableName_D.Text = DR["TABLE_NAME"].ToString();
                        cbComboValueField_D.Text = DR["VALUE_MEMBER"].ToString();
                        cbComboDisplayField_D.Text = DR["DISPLAY_MEMBER"].ToString();
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
                        DbCommand comm = sysConn.CreateCommand();
                        comm.CommandText = String.Format("Select * from SYS_REFVAL WHERE REFVAL_NO='{0}'", sRefValNo);
                        IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(comm);
                        DataSet aDataSet = new DataSet();
                        WzdUtils.FillDataAdapter(sSysDBType, DA, aDataSet, "SYS_REFVAL");
                        DataTable aDataTable = aDataSet.Tables[0];
                        foreach (DataRow DR in aDataTable.Rows)
                        {
                            cbComboTableName_D.Text = DR["TABLE_NAME"].ToString();
                            cbComboValueField_D.Text = DR["VALUE_MEMBER"].ToString();
                            cbComboDisplayField_D.Text = DR["DISPLAY_MEMBER"].ToString();
                        }
                    }
                }
            }
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

    public class TJQMobileFormData : Object
    {
        private string FPackageName, FBaseFormName, FServerPackageName, FFolderName, FTableName, FRealTableName, FFormName, FProviderName,
            FDatabaseName, FSolutionName, FViewProviderName, FWebSiteName, FWebSiteFullName, FFolderMode, FFormTitle;
        private TBlockItems FBlocks;
        private MWizard.fmJQMobileForm FOwner;
        private bool FNewSolution = false;
        private string FCodeFolderName;
        private int FColumnCount;
        private ClientType FDatabaseType;
        private String FConnString;
        private String FLanguage = "cs";

        public TJQMobileFormData(MWizard.fmJQMobileForm Owner)
        {
            FOwner = Owner;
            FBlocks = new TBlockItems(this);
        }

        public ClientType DatabaseType
        {
            get { return FDatabaseType; }
            set { FDatabaseType = value; }
        }

        public fmJQMobileForm Owner
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
            if (string.Compare(FBaseFormName, "JQMobileMasterDetail1") == 0 ||
                string.Compare(FBaseFormName, "VBJQMobileMasterDetail1") == 0 ||
                string.Compare(FBaseFormName, "JQMobileMasterDetail2") == 0 ||
                string.Compare(FBaseFormName, "VBJQMobileMasterDetail2") == 0)
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

    partial class TJQMobileFormGenerator : System.ComponentModel.Component
    {
        private TJQMobileFormData FClientData;
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

        public TJQMobileFormGenerator(TJQMobileFormData ClientData, DTE2 dte2, AddIn aAddIn)
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

        private bool GetForm()
        {
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
                bool flag = false;
                foreach (ProjectItem aPI in FPIFolder.ProjectItems)
                {
                    if (string.Compare(FClientData.FormName + ".aspx", aPI.Name) == 0)
                    {
                        DialogResult dr = DialogResult.No;
                        if (!flag)
                            dr = MessageBox.Show("There is another File which name is " + FClientData.PackageName + " existed! Do you want to delete it first", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        else
                            dr = DialogResult.Yes;
                        if (dr == DialogResult.Yes)
                        {
                            flag = true;
                            string Path = aPI.get_FileNames(0);
                            aPI.Name = Guid.NewGuid().ToString();
                            //aPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
                            //aPI.Save();
                            //if (FPI.Document != null && FPI.Document.ActiveWindow != null)
                            //{
                            //    FPI.Document.ActiveWindow.Close(vsSaveChanges.vsSaveChangesYes);
                            //}
                            aPI.Delete();
                            File.Delete(Path);
                        }
                        else
                        {
                            return false;
                        }
                        //if (System.IO.File.Exists(Path + "\\" + SearchName + ".aspx.resx"))
                        //    System.IO.File.Delete(Path + "\\" + SearchName + ".aspx.resx");
                        //if (System.IO.File.Exists(Path + "\\" + SearchName + ".aspx.vi-VN.resx"))
                        //    System.IO.File.Delete(Path + "\\" + SearchName + ".aspx.vi-VN.resx");
                        //break;
                        continue;
                    }
                    if (string.Compare(FClientData.BaseFormName + ".aspx", aPI.Name) == 0)
                    {
                        string Path = aPI.get_FileNames(0);

                        aPI.Name = Guid.NewGuid().ToString();
                        //aPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
                        //var w = aPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
                        //w.Activate();
                        //if (aPI.Document != null && aPI.Document.ActiveWindow != null)
                        //{
                        //    aPI.Document.ActiveWindow.Close(vsSaveChanges.vsSaveChangesYes);
                        //}
                        //aPI.Save();
                        aPI.Delete();
                        File.Delete(Path);

                        //if (System.IO.File.Exists(Path + "\\" + SearchName + ".aspx.resx"))
                        //    System.IO.File.Delete(Path + "\\" + SearchName + ".aspx.resx");
                        //if (System.IO.File.Exists(Path + "\\" + SearchName + ".aspx.vi-VN.resx"))
                        //    System.IO.File.Delete(Path + "\\" + SearchName + ".aspx.vi-VN.resx");
                        //break;
                    }
                }

                //OpenTemp(TemplatePath);
                FPI = null;
                FPI = FPIFolder.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + FClientData.BaseFormName + ".aspx");
                FPI.Name = Guid.NewGuid().ToString() + ".aspx";
                //FPI.Name = FClientData.FormName + ".aspx";
                FPIFolder = null;
                //ProjectItem P1 = TempPI.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + FClientData.BaseFormName + ".aspx.resx");
                //P1.Name = FClientData.FormName + ".aspx.resx";
                //ProjectItem P2 = TempPI.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + FClientData.BaseFormName + ".aspx.vi-VN.resx");
                //P2.Name = FClientData.FormName + ".aspx.vi-VN.resx";
                //FResxFileName = P2.Name;

                //FPI.ProjectItems.AddFromTemplate(TemplatePath + "\\" + SearchName + ".aspx", FClientData.FormName + ".aspx");
            }
            else
            {
                foreach (ProjectItem aPI in FProject.ProjectItems)
                {
                    if (string.Compare(FClientData.FormName + ".aspx", aPI.Name) == 0)
                    {
                        string Path = aPI.get_FileNames(0);
                        Path = System.IO.Path.GetDirectoryName(Path);
                        aPI.Delete();
                        if (System.IO.File.Exists(Path + "\\" + FClientData.FormName + ".aspx.resx"))
                            System.IO.File.Delete(Path + "\\" + FClientData.FormName + ".aspx.resx");
                        if (System.IO.File.Exists(Path + "\\" + FClientData.FormName + ".aspx.vi-VN.resx"))
                            System.IO.File.Delete(Path + "\\" + FClientData.FormName + ".aspx.vi-VN.resx");
                        break;
                    }
                }

                FPI = FProject.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + FClientData.BaseFormName + ".aspx");
                FPI.Name = FClientData.FormName + ".aspx";
                //ProjectItem P1 = FProject.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + FClientData.BaseFormName + ".aspx.resx");
                //P1.Name = FClientData.FormName + ".aspx.resx";
                //ProjectItem P2 = FProject.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + FClientData.BaseFormName + ".aspx.vi-VN.resx");
                //P2.Name = FClientData.FormName + ".aspx.vi-VN.resx";
                //FPI = FProject.ProjectItems.AddFromTemplate(TemplateFile, FClientData.FormName + ".aspx");
            }

            return true;
        }

        //private void OpenTemp(string templatePath)
        //{
        //    ProjectItem TempPI = FPI;

        //    FPI = FPI.ProjectItems.AddFromFileCopy(templatePath + "\\Temp.aspx");
        //    Window w = FPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
        //    w.Close(vsSaveChanges.vsSaveChangesNo);

        //    string p = FPI.get_FileNames(0);
        //    FPI.Delete();
        //    File.Delete(p);

        //    FPI = TempPI;
        //}

        private void GetDesignerHost()
        {
#if VS90
            //FDesignWindow = FPI.Open("{00000000-0000-0000-0000-000000000000}");
            //FDesignWindow.Activate();
            FDesignWindow = FPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
            FDesignWindow.Activate();
            HTMLWindow W = (HTMLWindow)FDesignWindow.Object;

            //W.CurrentTab = vsHTMLTabs.vsHTMLTabsSource;
            //if (W.CurrentTabObject is TextWindow)
            //    FTextWindow = W.CurrentTabObject as TextWindow;
            W.CurrentTab = vsHTMLTabs.vsHTMLTabsDesign;
            if (W.CurrentTabObject is WebDevPage.DesignerDocument)
            {
                FDesignerDocument = W.CurrentTabObject as WebDevPage.DesignerDocument;
            }
#else
            FDesignWindow = FPI.Open("{00000000-0000-0000-0000-000000000000}");
            FDesignWindow.Activate();
            FDesignWindow = FPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
            FDesignWindow.Activate();
            HTMLWindow W = (HTMLWindow)FDesignWindow.Object;
            object o = W.CurrentTabObject;
            IntPtr pObject;
            Microsoft.VisualStudio.OLE.Interop.IServiceProvider oleSP = (Microsoft.VisualStudio.OLE.Interop.IServiceProvider)o;
            Guid sid = typeof(IVSMDDesigner).GUID;
            Guid iid = typeof(IVSMDDesigner).GUID;
            int hr = oleSP.QueryService(ref sid, ref iid, out pObject);
            System.Runtime.InteropServices.Marshal.ThrowExceptionForHR(hr);
            if (pObject != IntPtr.Zero)
            {
                try
                {
                    Object TempObj = Marshal.GetObjectForIUnknown(pObject);
                    if (TempObj is IDesignerHost)
                    {
                        FDesignerHost = (IDesignerHost)TempObj;
                    }
                    else
                    {
                        Object ObjContainer = TempObj.GetType().InvokeMember("ComponentContainer",
                            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public |
                            System.Reflection.BindingFlags.GetProperty, null, TempObj, null);
                        if (ObjContainer is IDesignerHost)
                        {
                            FDesignerHost = (IDesignerHost)ObjContainer;
                        }
                    }
                    FPage = (System.Web.UI.Page)FDesignerHost.RootComponent;
                    NotifyRefresh(200);
                    Application.DoEvents();
                    //FPage.Form.ID = FClientData.FormName;
                }
                finally
                {
                    Marshal.Release(pObject);
                }
            }
#endif
        }

        private void GenViewBlockControl(TBlockItem BlockItem)
        {
            var masterBlockItem = FClientData.Blocks.FindItem("Master");
            BlockItem.wDataSource = new WebDataSource();

            WebQueryFiledsCollection QueryFields = new WebQueryFiledsCollection(null, typeof(QueryField));
            WebQueryColumnsCollection QueryColumns = new WebQueryColumnsCollection(null, typeof(QueryColumns));
            foreach (TBlockFieldItem fielditem in BlockItem.BlockFieldItems)
            {
                TBlockFieldItem masterField = null;
                if (masterBlockItem != null)
                {
                    foreach (TBlockFieldItem item in masterBlockItem.BlockFieldItems)
                    {
                        if (item.DataField == fielditem.DataField)
                        {
                            masterField = item;
                        }
                    }
                }
                if (masterField != null)
                    GenQuery(masterField, QueryFields, QueryColumns, BlockItem.TableName);
                else
                    GenQuery(fielditem, QueryFields, QueryColumns, BlockItem.TableName);
            }

            object oJQGridViewMaster = FDesignerDocument.webControls.item("dataGridView", 0);
            WebDevPage.IHTMLElement eJQGridViewMaster = (WebDevPage.IHTMLElement)oJQGridViewMaster;
            //FClientData.ProviderName
            if (eJQGridViewMaster != null)
            {
                StringBuilder sb = new StringBuilder(eJQGridViewMaster.innerHTML);
                ((WebDevPage.IHTMLElement)oJQGridViewMaster).setAttribute("RemoteName", FClientData.ProviderName, 0);
                ((WebDevPage.IHTMLElement)oJQGridViewMaster).setAttribute("DataMember", FClientData.TableName, 0);
                ((WebDevPage.IHTMLElement)oJQGridViewMaster).setAttribute("Title", FClientData.FormTitle, 0);
                //((WebDevPage.IHTMLElement)oJQGridViewMaster).setAttribute("Title", FClientData.FormTitle, 0);

                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    int idx = sb.ToString().LastIndexOf("</Columns>");
                    if (idx == -1)
                    {
                        sb.AppendLine("<Columns></Columns>");
                        idx = sb.ToString().LastIndexOf("</Columns>");
                    }
                    String sHeaderText = String.IsNullOrEmpty(BFI.Description) ? BFI.DataField : BFI.Description;
                    var relationOptions = string.Empty;
                    if ((BFI.ControlType == "RefValBox" || BFI.ControlType == "ComboBox") && !string.IsNullOrEmpty(BFI.ComboValueField) && !string.IsNullOrEmpty(BFI.ComboTextField)
                        && !string.IsNullOrEmpty(BFI.ComboRemoteName) && !string.IsNullOrEmpty(BFI.ComboEntityName))
                    {
                        relationOptions = string.Format("RelationOptions=\"{{RemoteName:'{0}',DisplayMember:'{1}',ValueMember:'{2}'}}\""
                            , BFI.ComboRemoteName, BFI.ComboTextField, BFI.ComboValueField);
                    }


                    sb.Insert(idx, String.Format("<JQMobileTools:JQGridColumn Alignment=\"left\" FieldName=\"{0}\" Caption=\"{1}\" Width=\"120\" {2} Format=\"{3}\"/>", BFI.DataField, sHeaderText, relationOptions, BFI.EditMask));
                }

                var toolItems = new List<JQMobileTools.JQToolItem>();
                toolItems.Add(JQMobileTools.JQToolItem.InsertItem);
                toolItems.Add(JQMobileTools.JQToolItem.PreviousPageItem);
                toolItems.Add(JQMobileTools.JQToolItem.NextPageItem);
                toolItems.Add(JQMobileTools.JQToolItem.QueryItem);
                toolItems.Add(JQMobileTools.JQToolItem.RefreshItem);
                toolItems.Add(JQMobileTools.JQToolItem.BackItem);

                int idxTool = sb.ToString().LastIndexOf("</ToolItems>");
                foreach (var item in toolItems)
                {
                    idxTool = sb.ToString().LastIndexOf("</ToolItems>");
                    sb.Insert(idxTool, String.Format("<JQMobileTools:JQToolItem Icon=\"{0}\" Name=\"{1}\" Text=\"{2}\" Visible=\"True\" />", item.Icon, item.Name, item.Text));
                }

                int idxQuery = sb.ToString().LastIndexOf("</QueryColumns>");
                foreach (WebQueryColumns item in QueryColumns)
                {
                    idxQuery = sb.ToString().LastIndexOf("</QueryColumns>");
                    //sb.Insert(idxQuery, item.Mode);
                    sb.Insert(idxQuery, String.Format("<JQMobileTools:JQQueryColumn Caption=\"{0}\" Condition=\"{1}\" Editor=\"text\" FieldName=\"{2}\" />", item.Caption, item.Operator, item.Column));
                }
                eJQGridViewMaster.innerHTML = sb.ToString();

            }
        }

        private void AdjectLabelEditPos(TStringList EditList, TStringList LabelList)
        {
            int MaxLabelWidth = 0;
            System.Windows.Forms.Label label = null;
            System.Windows.Forms.TextBox textbox = null;
            for (int I = 0; I < LabelList.Count; I++)
            {
                label = (System.Windows.Forms.Label)LabelList[I];
                if (label.Width > MaxLabelWidth)
                    MaxLabelWidth = label.Width;
            }
            if (MaxLabelWidth >= 105)
            {
                int EditOffSet = MaxLabelWidth - 105 + 5;

                for (int I = 0; I < EditList.Count; I++)
                {
                    textbox = (System.Windows.Forms.TextBox)EditList[I];
                    textbox.Left = 110 + EditOffSet;
                }

                for (int I = 0; I < LabelList.Count; I++)
                {
                    label = (System.Windows.Forms.Label)LabelList[I];
                    label.Left = 110 - label.Width - 5 + EditOffSet;
                }
            }
            if (EditList.Count == 0)
                return;
            int ColumnIndex = 0;
            int ColumnControlCount = EditList.Count / FClientData.ColumnCount;
            int ColumnWidth = ((System.Windows.Forms.TextBox)EditList[0]).Left + ((System.Windows.Forms.TextBox)EditList[0]).Width;
            int TopOffset = 10;
            int ColumnControlIndex = 0;

            for (int I = 0; I < EditList.Count; I++)
            {
                if (I % ColumnControlCount == 0)
                {
                    if (I + 1 >= ColumnControlCount)
                    {
                        ColumnControlIndex = 0;
                        ColumnIndex++;
                    }
                }
                label = (System.Windows.Forms.Label)LabelList[I];
                textbox = (System.Windows.Forms.TextBox)EditList[I];
                textbox.Left = textbox.Left + ColumnWidth * ColumnIndex;
                textbox.Top = TopOffset + (textbox.Height + 5) * ColumnControlIndex;
                label.Left = label.Left + ColumnWidth * ColumnIndex;
                label.Top = textbox.Top + (textbox.Height - label.Height) / 2;
                ColumnControlIndex++;
            }
        }

        private void GenResx(WebDataSource aWebDataSource)
        {
            WebDataSet aWebDataSet = FDesignerHost.CreateComponent(typeof(WebDataSet), "WMaster") as WebDataSet;
            WebDataSet bWebDataSet = null;
            if (FClientData.BaseFormName == "WMasterDetail3" || FClientData.BaseFormName == "VBWebCMasterDetail_VFG" || FClientData.BaseFormName == "WMasterDetail8"
                || FClientData.BaseFormName == "VBWebCMasterDetail8")
            {
                bWebDataSet = FDesignerHost.CreateComponent(typeof(WebDataSet), "WView") as WebDataSet;
            }
            try
            {
                aWebDataSet.SetWizardDesignMode(true);
                aWebDataSet.RemoteName = FClientData.ProviderName;
                aWebDataSet.PacketRecords = 100;
                aWebDataSet.Active = true;

                if (bWebDataSet != null)
                {
                    bWebDataSet.RemoteName = FClientData.ViewProviderName;
                    bWebDataSet.PacketRecords = 0;
                    bWebDataSet.Active = true;
                }

                MyDesingerLoader ML = new MyDesingerLoader(FClientData.FolderName + @"\" + FResxFileName);
                FDesignerHost.AddService(typeof(IResourceService), ML);
                WebDataSetDesigner aDesigner = (WebDataSetDesigner)FDesignerHost.GetDesigner(aWebDataSet);

                aDesigner.Initialize(aWebDataSet);
                if (aDesigner != null)
                {
                    aDesigner.OnSave(FDesignWindow.Document, null);
                }

                WebDataSourceDesigner aWebDataSourceDesigner = FDesignerHost.GetDesigner(aWebDataSource) as WebDataSourceDesigner;
                if (aWebDataSourceDesigner != null)
                    aWebDataSourceDesigner.RefreshSchema(true);
            }
            finally
            {
                aWebDataSet.Dispose();
                if (bWebDataSet != null)
                    bWebDataSet.Dispose();
            }
        }

        private String GenWebDataSource(TBlockFieldItem FieldItem, String TableName, String Kind, String ExtraName)
        {
            return GenWebDataSource(FieldItem, TableName, Kind, ExtraName, false);
        }

        private String GenWebDataSource(TBlockFieldItem FieldItem, String TableName, String Kind, String ExtraName, bool isAjaxControl)
        {
            String Name = "wds" + TableName + FieldItem.DataField + ExtraName;

            bool isExist = false;
            foreach (WebDataSource bWebDataSource in FWebDataSourceList)
            {
                if (String.Compare(bWebDataSource.ID, Name) == 0)
                {
                    isExist = true;
                    break;
                }
            }
#if VS90
            object oDataSource = FDesignerDocument.webControls.item(Name, 0);
            if (oDataSource != null && oDataSource is WebDevPage.IHTMLElement)
            {
                WebDevPage.IHTMLElement eDataSoruce = oDataSource as WebDevPage.IHTMLElement;
                if (eDataSoruce.tagName.ToLower() == "infolight:webdatasource")
                    return Name;
            }

            WebDevPage.IHTMLElement form = ((WebDevPage.IHTMLElementCollection)FDesignerDocument.pageContentElement.children).item("form1", 0) as WebDevPage.IHTMLElement;

            if (Kind == "RefValBox")
            {
                WebDataSource aWebDataSource = new WebDataSource();
                InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
                aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                //aInfoCommand.Connection = FClientData.Owner.GlobalConnection;
                IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                if (FSYS_REFVAL != null)
                    FSYS_REFVAL.Dispose();
                FSYS_REFVAL = new DataSet();
                try
                {
                    aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", FieldItem.RefValNo);
                    WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, FSYS_REFVAL, FieldItem.RefValNo);
                    aWebDataSource.SelectAlias = FSYS_REFVAL.Tables[0].Rows[0]["SELECT_ALIAS"].ToString();
                    aWebDataSource.SelectCommand = FSYS_REFVAL.Tables[0].Rows[0]["SELECT_COMMAND"].ToString();
                }
                catch (SqlException sex)
                {
                    if (sex.Number == 208)
                    {
                        string sSysDBAlias = WzdUtils.GetSystemDBName();
                        ClientType sSysDBType = WzdUtils.GetSystemDBType();
                        DbConnection sysConn = WzdUtils.AllocateConnection(sSysDBAlias, sSysDBType, false);
                        if (sysConn.State != ConnectionState.Open) sysConn.Open();
                        DbCommand comm = sysConn.CreateCommand();
                        comm.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", FieldItem.RefValNo);
                        WzdUtils.FillDataAdapter(sSysDBType, DA, FSYS_REFVAL, FieldItem.RefValNo);
                        aWebDataSource.SelectAlias = FSYS_REFVAL.Tables[0].Rows[0]["SELECT_ALIAS"].ToString();
                        aWebDataSource.SelectCommand = FSYS_REFVAL.Tables[0].Rows[0]["SELECT_COMMAND"].ToString();
                    }
                }

                if (!isExist)
                {
                    aWebDataSource.ID = Name;
                    FWebDataSourceList.Add(aWebDataSource);
                    if (isAjaxControl)
                        form.insertAdjacentHTML("afterBegin", "<InfoLight:WebDataSource ID=\"" + Name + "\" runat=\"server\" SelectAlias=\"" + aWebDataSource.SelectAlias + "\" SelectCommand=\"" + aWebDataSource.SelectCommand + "\" CacheDataSet=\"True\"></InfoLight:WebDataSource>");
                    else
                        form.insertAdjacentHTML("afterBegin", "<InfoLight:WebDataSource ID=\"" + Name + "\" runat=\"server\" SelectAlias=\"" + aWebDataSource.SelectAlias + "\" SelectCommand=\"" + aWebDataSource.SelectCommand + "\"></InfoLight:WebDataSource>");
                }
            }
            else if (Kind == "ComboBox")
            {
                string type = FindSystemDBType("SystemDB");
                string cmd = "";
                if (type == "1")
                    cmd = String.Format("Select * from [{0}]", TableName);
                else if (type == "2")
                    cmd = String.Format("Select * from [{0}]", TableName);
                else if (type == "3")
                    cmd = String.Format("Select * from {0}", TableName);
                else if (type == "4")
                    cmd = String.Format("Select * from {0}", TableName);
                else if (type == "5")
                    cmd = String.Format("Select * from {0}", TableName);
                else if (type == "6")
                    cmd = String.Format("Select * from {0}", TableName);
                else if (type == "7")
                    cmd = String.Format("Select * from {0}", TableName);

                if (!isExist)
                {
                    WebDataSource aWebDataSource = new WebDataSource();
                    aWebDataSource.ID = Name;
                    FWebDataSourceList.Add(aWebDataSource);
                    if (isAjaxControl)
                        form.insertAdjacentHTML("afterBegin", "<InfoLight:WebDataSource ID=\"" + Name + "\" runat=\"server\" SelectAlias=\"" + FClientData.Owner.SelectedAlias + "\" SelectCommand=\"" + cmd + "\" CacheDataSet=\"True\"></InfoLight:WebDataSource>");
                    else
                        form.insertAdjacentHTML("afterBegin", "<InfoLight:WebDataSource ID=\"" + Name + "\" runat=\"server\" SelectAlias=\"" + FClientData.Owner.SelectedAlias + "\" SelectCommand=\"" + cmd + "\"></InfoLight:WebDataSource>");
                }
            }
            return Name;
#else
            WebDataSource aWebDataSource = new WebDataSource();
            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
            //aInfoCommand.Connection = FClientData.Owner.GlobalConnection;
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            //SYS_REFVAL
            if (Kind == "RefVal")
            {
                if (FSYS_REFVAL != null)
                    FSYS_REFVAL.Dispose();
                FSYS_REFVAL = new DataSet();
                aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", FieldItem.RefValNo);
                WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, FSYS_REFVAL, FieldItem.RefValNo);
                aWebDataSource.SelectAlias = FSYS_REFVAL.Tables[0].Rows[0]["SELECT_ALIAS"].ToString();
                aWebDataSource.SelectCommand = FSYS_REFVAL.Tables[0].Rows[0]["SELECT_COMMAND"].ToString();
            }
            //WebDropDownList
            else if (Kind == "ComboBox")
            {
                string type = FindSystemDBType("SystemDB");

                aWebDataSource.SelectAlias = FClientData.Owner.SelectedAlias;
                if (type == "1")
                    aWebDataSource.SelectCommand = String.Format("Select * from [{0}]", FieldItem.ComboEntityName);
                else if (type == "2")
                    aWebDataSource.SelectCommand = String.Format("Select * from [{0}]", FieldItem.ComboEntityName);
                else if (type == "3")
                    aWebDataSource.SelectCommand = String.Format("Select * from {0}", FieldItem.ComboEntityName);
                else if (type == "4")
                    aWebDataSource.SelectCommand = String.Format("Select * from {0}", FieldItem.ComboEntityName);
                else if (type == "5")
                    aWebDataSource.SelectCommand = String.Format("Select * from {0}", FieldItem.ComboEntityName);
            }
            aWebDataSource.ID = Name;
            if (isAjaxControl)
            {
                aWebDataSource.CacheDataSet = true;
            }
            if (!isExist)
                FWebDataSourceList.Add(aWebDataSource);
            return aWebDataSource.ID;
#endif
        }

        private String GenExtComboBox(TBlockFieldItem FieldItem, String TableName, String Kind, String ExtraName, String DSID)
        {
            String Name = "ext" + TableName + FieldItem.DataField + ExtraName;

            bool isExist = false;
            foreach (ExtComboBox bExtComboBox in FExtComboBoxList)
            {
                if (String.Compare(bExtComboBox.ID, Name) == 0)
                {
                    isExist = true;
                    break;
                }
            }
            ExtComboBox aExtComboBox = new ExtComboBox();
            aExtComboBox.ID = Name;
            aExtComboBox.DataSourceID = DSID;
            aExtComboBox.AutoRender = false;
            if (Kind == "ExtRefVal")
            {
                DataSet aDataSet = new DataSet();
                try
                {
                    InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
                    aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                    if (aInfoCommand.Connection.State != ConnectionState.Open) aInfoCommand.Connection.Open();
                    aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", FieldItem.RefValNo);
                    IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                    WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, FieldItem.RefValNo);
                    aExtComboBox.DisplayField = aDataSet.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                    aExtComboBox.ValueField = aDataSet.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();

                    ExtSimpleColumn aExtSimpleColumn = new ExtSimpleColumn();
                    aExtSimpleColumn.DataField = aDataSet.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                    aExtComboBox.Columns.Add(aExtSimpleColumn);
                    ExtSimpleColumn bExtSimpleColumn = new ExtSimpleColumn();
                    bExtSimpleColumn.DataField = aDataSet.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
                    aExtComboBox.Columns.Add(bExtSimpleColumn);
                }
                catch (SqlException sex)
                {
                    if (sex.Number == 208)
                    {
                        string sSysDBAlias = WzdUtils.GetSystemDBName();
                        ClientType sSysDBType = WzdUtils.GetSystemDBType();
                        DbConnection sysConn = WzdUtils.AllocateConnection(sSysDBAlias, sSysDBType, false);
                        if (sysConn.State != ConnectionState.Open) sysConn.Open();
                        DbCommand comm = sysConn.CreateCommand();
                        comm.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", FieldItem.RefValNo);
                        IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(comm);
                        WzdUtils.FillDataAdapter(sSysDBType, DA, aDataSet, FieldItem.RefValNo);
                        aExtComboBox.DisplayField = aDataSet.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                        aExtComboBox.ValueField = aDataSet.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();

                        ExtSimpleColumn aExtSimpleColumn = new ExtSimpleColumn();
                        aExtSimpleColumn.DataField = aDataSet.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                        aExtComboBox.Columns.Add(aExtSimpleColumn);
                        ExtSimpleColumn bExtSimpleColumn = new ExtSimpleColumn();
                        bExtSimpleColumn.DataField = aDataSet.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
                        aExtComboBox.Columns.Add(bExtSimpleColumn);
                    }
                }
            }
            else if (Kind == "ExtComboBox")
            {
                aExtComboBox.DisplayField = FieldItem.ComboTextField;
                aExtComboBox.ValueField = FieldItem.ComboValueField;

                ExtSimpleColumn aExtSimpleColumn = new ExtSimpleColumn();
                aExtSimpleColumn.DataField = FieldItem.ComboTextField;
                aExtComboBox.Columns.Add(aExtSimpleColumn);
                ExtSimpleColumn bExtSimpleColumn = new ExtSimpleColumn();
                bExtSimpleColumn.DataField = FieldItem.ComboValueField;
                aExtComboBox.Columns.Add(bExtSimpleColumn);

                aExtComboBox.AutoRender = false;

            }
            if (!isExist)
                FExtComboBoxList.Add(aExtComboBox);
#if VS90
            if (!isExist)
            {
                WebDevPage.IHTMLElement Page = FDesignerDocument.pageContentElement;
                ////InsertControl(Page, aExtComboBox);
            }
#endif
            return aExtComboBox.ID;
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

        private static string GetServerPath()
        {
            String _serverPath = "";
            if (_serverPath.Length == 0)
            {
                String s = EEPRegistry.Server + "\\";
                _serverPath = s;
            }
            return _serverPath;
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

        private void TestCreateSomething()
        {
            //IWebFormReferenceManager aManager = FDesignerHost.GetService(typeof(IWebFormReferenceManager)) as IWebFormReferenceManager;

            //System.Web.UI.WebControls.Button TB = FDesignerHost.CreateComponent(typeof(System.Web.UI.WebControls.Button), "TextBBB") as System.Web.UI.WebControls.Button;
            //FPage.Controls.Add(TB);
            //ToolboxDataAttribute attribute = TypeDescriptor.GetAttributes(typeof(System.Web.UI.WebControls.Button))[typeof(ToolboxDataAttribute)] as ToolboxDataAttribute;

            //ControlDesigner A = FDesignerHost.GetDesigner(TB) as ControlDesigner;

            //if (attribute != null)
            //{
            //    ((IHTMLElement)A.Behavior.DesignTimeElement).insertAdjacentHTML(
            //    "beforeEnd", String.Format(attribute.Data, aManager.GetTagPrefix(typeof(
            //    Content))));
            //}
            //else
            //{
            //    ((IHTMLElement)A.Behavior.DesignTimeElement).insertAdjacentHTML(
            //    "beforeEnd", String.Format("<{0}:Button runat='server'></{0}:Button>",
            //    aManager.GetTagPrefix(typeof(Content))));
            //}
        }

        private void GenDefault(TBlockFieldItem aFieldItem, JQDefault aDefault, JQValidate aValidate)
        {
            if (aFieldItem.DefaultValue != "" && aFieldItem.DefaultValue != null)
            {
                JQDefaultColumn aDefaultItem = new JQDefaultColumn();
                aDefaultItem.FieldName = aFieldItem.DataField;
                aDefaultItem.DefaultValue = aFieldItem.DefaultValue;
                aDefault.Columns.Add(aDefaultItem);
            }
            if (aFieldItem.CheckNull != null && aFieldItem.CheckNull.ToUpper() == "Y")
            {
                JQValidateColumn aValidateItem = new JQValidateColumn();
                aValidateItem.FieldName = aFieldItem.DataField;
                aValidateItem.CheckNull = aFieldItem.CheckNull.ToUpper() == "Y";
                //aValidateItem.ValidateLabelLink = "Caption" + aFieldItem.DataField;

                aValidate.Columns.Add(aValidateItem);
            }
        }

#if VS90
        //因为DD只有一个格式栏位，所以Web和Windows统一一种Format格式
        private String FormatEditMask(String editMask)
        {
            if (editMask != null && editMask != String.Empty && !editMask.StartsWith(","))
                editMask = ",\"{0:" + editMask + "}\"";
            return editMask;
        }
#endif

        private void GenMainBlockControl(TBlockItem BlockItem)
        {
#if VS90
            //object oMaster = FDesignerDocument.webControls.item("Master", 0);

            //WebDevPage.IHTMLElement eMaster = null;

            //if (oMaster == null || !(oMaster is WebDevPage.IHTMLElement))
            //    return;
            //eMaster = (WebDevPage.IHTMLElement)oMaster;
            BlockItem.wDataSource = new WebDataSource();

            JQDefault Default = new JQDefault();
            Default.ID = "defaultMaster"; // +BlockItem.TableName;
            Default.BindingObjectID = "dataGridMaster";

            JQValidate Validate = new JQValidate();
            Validate.ID = "validateMaster"; // +BlockItem.TableName;
            Validate.BindingObjectID = "dataGridMaster";

            WebQueryFiledsCollection QueryFields = new WebQueryFiledsCollection(null, typeof(QueryField));
            WebQueryColumnsCollection QueryColumns = new WebQueryColumnsCollection(null, typeof(QueryColumns));
            foreach (TBlockFieldItem fielditem in BlockItem.BlockFieldItems)
            {
                GenDefault(fielditem, Default, Validate);
                GenQuery(fielditem, QueryFields, QueryColumns, BlockItem.TableName);
            }

            object oJQGridViewMaster = FDesignerDocument.webControls.item("dataGridMaster", 0);
            WebDevPage.IHTMLElement eJQGridViewMaster = (WebDevPage.IHTMLElement)oJQGridViewMaster;
            //FClientData.ProviderName
            if (eJQGridViewMaster != null)
            {
                StringBuilder sb = new StringBuilder(eJQGridViewMaster.innerHTML);
                ((WebDevPage.IHTMLElement)oJQGridViewMaster).setAttribute("RemoteName", FClientData.ProviderName, 0);
                ((WebDevPage.IHTMLElement)oJQGridViewMaster).setAttribute("DataMember", FClientData.TableName, 0);
                ((WebDevPage.IHTMLElement)oJQGridViewMaster).setAttribute("Title", FClientData.FormTitle, 0);
                //((WebDevPage.IHTMLElement)oJQGridViewMaster).setAttribute("Title", FClientData.FormTitle, 0);

                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    int idx = sb.ToString().LastIndexOf("</Columns>");
                    if (idx == -1)
                    {
                        sb.AppendLine("<Columns></Columns>");
                        idx = sb.ToString().LastIndexOf("</Columns>");
                    }
                    String sHeaderText = String.IsNullOrEmpty(BFI.Description) ? BFI.DataField : BFI.Description;
                    var relationOptions = string.Empty;
                    if ((BFI.ControlType == "RefValBox" || BFI.ControlType == "ComboBox") && !string.IsNullOrEmpty(BFI.ComboValueField) && !string.IsNullOrEmpty(BFI.ComboTextField)
                        && !string.IsNullOrEmpty(BFI.ComboRemoteName) && !string.IsNullOrEmpty(BFI.ComboEntityName))
                    {
                        relationOptions = string.Format("RelationOptions=\"{{RemoteName:'{0}',DisplayMember:'{1}',ValueMember:'{2}'}}\""
                            , BFI.ComboRemoteName, BFI.ComboTextField, BFI.ComboValueField);
                    }


                    sb.Insert(idx, String.Format("<JQMobileTools:JQGridColumn Alignment=\"left\" FieldName=\"{0}\" Caption=\"{1}\" Width=\"120\" {2} Format=\"{3}\"/>", BFI.DataField, sHeaderText, relationOptions, BFI.EditMask));
                }

                var toolItems = new List<JQMobileTools.JQToolItem>();
                if (FClientData.BaseFormName == "JQMobileQuery" || FClientData.BaseFormName == "VBJQMobileQuery")
                {
                    toolItems.Add(JQMobileTools.JQToolItem.PreviousPageItem);
                    toolItems.Add(JQMobileTools.JQToolItem.NextPageItem);
                    toolItems.Add(JQMobileTools.JQToolItem.QueryItem);
                    toolItems.Add(JQMobileTools.JQToolItem.RefreshItem);
                }
                else
                {
                    toolItems.Add(JQMobileTools.JQToolItem.InsertItem);
                    toolItems.Add(JQMobileTools.JQToolItem.PreviousPageItem);
                    toolItems.Add(JQMobileTools.JQToolItem.NextPageItem);
                    toolItems.Add(JQMobileTools.JQToolItem.QueryItem);
                    toolItems.Add(JQMobileTools.JQToolItem.RefreshItem);
                    toolItems.Add(JQMobileTools.JQToolItem.BackItem);
                }

                int idxTool = sb.ToString().LastIndexOf("</ToolItems>");
                if (idxTool != -1)
                {
                    foreach (var item in toolItems)
                    {
                        idxTool = sb.ToString().LastIndexOf("</ToolItems>");
                        sb.Insert(idxTool, String.Format("<JQMobileTools:JQToolItem Icon=\"{0}\" Name=\"{1}\" Text=\"{2}\" Visible=\"True\" />", item.Icon, item.Name, item.Text));
                    }
                }

                int idxQuery = sb.ToString().LastIndexOf("</QueryColumns>");
                if (idxQuery != -1)
                {
                    foreach (WebQueryField item in QueryFields)
                    {
                        idxQuery = sb.ToString().LastIndexOf("</QueryColumns>");
                        sb.Insert(idxQuery, item.Mode);
                        //sb.Insert(idxQuery, String.Format("<JQTools:JQQueryColumn Caption=\"{0}\" Condition=\"{1}\" Editor=\"text\" FieldName=\"{2}\" />", item.Caption, item.Operator, item.Column));
                    }
                    //foreach (WebQueryColumns item in QueryColumns)
                    //{
                    //    idxQuery = sb.ToString().LastIndexOf("</QueryColumns>");
                    //    //sb.Insert(idxQuery, item.Mode);
                    //    sb.Insert(idxQuery, String.Format("<JQMobileTools:JQQueryColumn Caption=\"{0}\" Condition=\"{1}\" Editor=\"text\" FieldName=\"{2}\" />", item.Caption, item.Operator, item.Column));
                    //}
                }
                eJQGridViewMaster.innerHTML = sb.ToString();

            }

            //object oJQDialog1 = FDesignerDocument.webControls.item("JQDialog1", 0);
            //WebDevPage.IHTMLElement eJQDialog1 = (WebDevPage.IHTMLElement)oJQDialog1;
            //if (oJQDialog1 != null)
            //{
            //((WebDevPage.IHTMLElement)oJQDialog1).setAttribute("Title", FClientData.FormTitle, 0);

            object oJQFormViewMaster = FDesignerDocument.webControls.item("dataFormMaster", 0);
            WebDevPage.IHTMLElement eJQFormViewMaster = (WebDevPage.IHTMLElement)oJQFormViewMaster;
            if (eJQFormViewMaster != null)
            {
                StringBuilder sb = new StringBuilder();
                ((WebDevPage.IHTMLElement)oJQFormViewMaster).setAttribute("RemoteName", FClientData.ProviderName, 0);
                ((WebDevPage.IHTMLElement)oJQFormViewMaster).setAttribute("DataMember", FClientData.TableName, 0);
                ((WebDevPage.IHTMLElement)oJQFormViewMaster).setAttribute("Title", FClientData.FormTitle, 0);
                sb.Append("<Columns>");
                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    JQControl control = null;
                    string editor = "text";
                    String sHeaderText = String.IsNullOrEmpty(BFI.Description) ? BFI.DataField : BFI.Description;
                    if ((BFI.ControlType == "ComboBox" || BFI.ControlType == "ComboGrid" || BFI.ControlType == "RefValBox")
                        && (string.IsNullOrEmpty(BFI.ComboValueField) || string.IsNullOrEmpty(BFI.ComboTextField)
                        || string.IsNullOrEmpty(BFI.ComboRemoteName) || string.IsNullOrEmpty(BFI.ComboEntityName)))
                    {
                        BFI.ControlType = "TextBox";
                    }


                    if (BFI.ControlType == "ComboBox")
                    {
                        control = new JQSelects() { RemoteName = BFI.ComboRemoteName, DisplayMember = BFI.ComboTextField, ValueMember = BFI.ComboValueField };
                        editor = "selects";

                    }
                    else if (BFI.ControlType == "RefValBox")
                    {
                        control = new JQRefval() { RemoteName = BFI.ComboRemoteName, DisplayMember = BFI.ComboTextField, ValueMember = BFI.ComboValueField };
                        //(control as JQRefval).Columns.Add(new JQRefValColumn() { FieldName = BFI.ComboValueField, Caption = BFI.ComboValueFieldCaption });
                        editor = "refval";
                    }
                    else if (BFI.ControlType == "RadioButton")
                    {
                        control = new JQRadioButtons() { RemoteName = BFI.ComboRemoteName, DisplayMember = BFI.ComboTextField, ValueMember = BFI.ComboValueField };
                        editor = "radiobuttons";
                    }
                    else if (BFI.ControlType == "CheckBox")
                    {
                        JQCollection<JQDataItem> items = new JQCollection<JQDataItem>(control);
                        JQDataItem item1 = new JQDataItem();
                        item1.Text = "Yes"; item1.Value = "Yes";
                        JQDataItem item2 = new JQDataItem();
                        item2.Text = "No"; item2.Value = "No";
                        items.Add(item1);
                        items.Add(item2);
                        control = new JQCheckBoxes() { RemoteName = BFI.ComboRemoteName, DisplayMember = BFI.ComboTextField, ValueMember = BFI.ComboValueField, Items = items };
                        editor = "checkboxes";
                    }
                    else if (BFI.ControlType == "DateTimeBox")
                    {
                        control = new JQDate();
                        editor = "date";
                    }
                    else
                    {
                        control = new JQTextBox();

                    }
                    sb.AppendFormat("<{0}:{1} Caption=\"{2}\" FieldName=\"{3}\" Editor=\"{4}\" Width=\"120\" EditorOptions=\"{5}\"></{0}:{1}>"
                        , "JQMobileTools", "JQFormColumn", sHeaderText, BFI.DataField, editor, control.Options);
                }
                sb.Append("</Columns>");

                eJQFormViewMaster.innerHTML = sb.ToString();
                WebDevPage.IHTMLElement Page = FDesignerDocument.pageContentElement;
                Default.BindingObjectID = "dataFormMaster";
                InsertControl(Page, Default);
                Validate.BindingObjectID = "dataFormMaster";
                InsertControl(Page, Validate);
            }
            //}
            //else
            //{
            //    if (FClientData.BaseFormName != "JQueryQuery1")
            //    {
            //        WebDevPage.IHTMLElement Page = FDesignerDocument.pageContentElement;
            //        InsertControl(Page, Default);
            //        InsertControl(Page, Validate);
            //    }
            //}

#else
#endif
        }

        private void FieldTypeSelector(Type fieldType, object extCol, String type)
        {
            if (fieldType == typeof(uint) || fieldType == typeof(UInt16) || fieldType == typeof(UInt32) || fieldType == typeof(UInt64) || fieldType == typeof(int) || fieldType == typeof(Int16) || fieldType == typeof(Int32) || fieldType == typeof(Int64))
            {
                extCol.GetType().GetProperty("FieldType").SetValue(extCol, "int", null);
                if (type != "ComboBox" && type != "RefValBox")
                    extCol.GetType().GetProperty("ValidType").SetValue(extCol, extCol.GetType().GetProperty("ValidType").PropertyType.GetField("Int").GetValue(extCol), null);
                //extCol.FieldType = "int";
            }
            else if (fieldType == typeof(Single) || fieldType == typeof(double) || fieldType == typeof(decimal))
            {
                extCol.GetType().GetProperty("FieldType").SetValue(extCol, "float", null);
                //if(type != "ComboBox" && type != "Refval")
                //    extCol.GetType().GetProperty("ValidType").SetValue(extCol, extCol.GetType().GetProperty("ValidType").PropertyType.GetField("Float").GetValue(extCol), null);
                //extCol.FieldType = "float";
            }
            else if (fieldType == typeof(string))
            {
                extCol.GetType().GetProperty("FieldType").SetValue(extCol, "string", null);
                //extCol.FieldType = "string";
            }
            else if (fieldType == typeof(bool))
            {
                extCol.GetType().GetProperty("FieldType").SetValue(extCol, "boolean", null);
                extCol.GetType().GetProperty("Editor").SetValue(extCol, extCol.GetType().GetProperty("Editor").PropertyType.GetField("CheckBox").GetValue(extCol), null);
                //extCol.FieldType = "boolean";
            }
            else if (fieldType == typeof(DateTime))
            {
                extCol.GetType().GetProperty("FieldType").SetValue(extCol, "date", null);
                extCol.GetType().GetProperty("Formatter").SetValue(extCol, "Y/m/d", null);
                extCol.GetType().GetProperty("Editor").SetValue(extCol, extCol.GetType().GetProperty("Editor").PropertyType.GetField("DateTimePicker").GetValue(extCol), null);
                //extCol.FieldType = "date";
                //extCol.Formatter = "Y/m/d";//"Ext.util.Format.dateRenderer('Y/m/d')";
                //extCol.Editor = AjaxTools.ExtGridEditor.DateTimePicker;
            }
        }

        private void InsertIntoGridView(StringBuilder sb, String content)
        {
            //<SettingsDetail ShowDetailRow="true" />
            int idx = sb.ToString().IndexOf("<SettingsDetail ShowDetailRow=\"true\" />");
            sb.Insert(idx, content);
        }

        private string GetValidateText(string ControlText)
        {
            string fieldName = ControlText;

            int Index = ControlText.IndexOf("Bind(\"");
            if (Index > -1)
            {
                fieldName = ControlText.Substring(Index + 6);
                fieldName = fieldName.Substring(0, fieldName.IndexOf("\""));
            }

            return fieldName;
        }

        private DataSet GetDD(WebFormView formView)
        {
            DataSet dsDD = new DataSet();
            DTE2 myDTE2 = FDTE2; //(DTE2)Marshal.GetActiveObject("VisualStudio.DTE.8.0");
            string strModuleName = "", strTableName = "", sCurProject = "", tabName = "", dbAlias = "";
            object obj = formView.GetObjByID(formView.DataSourceID);
            if (obj == null)
                return null;

            if (myDTE2 != null)
            {
                Solution2 sol = (Solution2)myDTE2.Solution;
                sCurProject = Path.ChangeExtension(Path.GetFileName(sol.FileName), "");

                if ((sCurProject.Length >= 1) && (sCurProject[sCurProject.Length - 1] == '.'))
                    sCurProject = sCurProject.Substring(0, sCurProject.Length - 1);
            }
            if (obj is WebDataSource)
            {
                WebDataSource wds = (WebDataSource)obj;
                if (wds.SelectAlias != null && wds.SelectAlias != "" && wds.SelectCommand != null && wds.SelectCommand != "")
                {
                    MatchCollection mc = Regex.Matches(wds.SelectCommand.ToLower(), @"(\s+)\bfrom\b(\s+)(\S+)");
                    string s = mc[0].Value;
                    strModuleName = "GLModule";
                    strTableName = "cmdDDUse";
                    tabName = s.Substring(s.LastIndexOf(' ') + 1);
                    dbAlias = wds.SelectAlias;
                }
                else
                {
                    strModuleName = FClientData.ProviderName;
                    strModuleName = strModuleName.Substring(0, strModuleName.IndexOf('.'));
                    strTableName = wds.DataMember;

                    if (strTableName == null || strTableName.Length == 0)
                    {
                        System.Windows.Forms.MessageBox.Show(formView.DataSourceID + "'s DataMember property is null.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return null;
                    }
                    tabName = CliUtils.GetTableName(strModuleName, strTableName, sCurProject);
                }
            }
            String OWNER = String.Empty, SS = tabName;
            if (SS.Contains("."))
            {
                OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                tabName = SS;
            }
            string strSql = "Select * from COLDEF where TABLE_NAME='" + tabName + "' OR TABLE_NAME='" + OWNER + "." + tabName + "'";
            if (dbAlias != "")
            {
                dsDD = CliUtils.ExecuteSql(strModuleName, strTableName, strSql, dbAlias, true, sCurProject);
            }
            else
            {
                dsDD = CliUtils.ExecuteSql(strModuleName, strTableName, strSql, true, sCurProject);
            }
            return dsDD;
        }

        private string GetDDText(string ControlText, TBlockItem BlockItem, String TemplateName)
        {
            int Index1 = ControlText.IndexOf("Bind(\"");
            if (Index1 < 0)
            {
                foreach (TBlockFieldItem FieldItem in BlockItem.BlockFieldItems)
                {
                    if (String.Compare(ControlText, FieldItem.DataField) == 0)
                    {
                        // <asp:Label ID="CaptionGROUPID" runat="server" Text="群組編號:"></asp:Label>
                        String Description = FieldItem.Description;
                        if (Description == null || Description == "")
                            Description = FieldItem.DataField;
                        String ExtraName = "";
                        if (TemplateName == "ItemTemplate")
                            ExtraName = "I";
                        return String.Format("<asp:Label ID=\"Caption{0}\" runat=\"server\" Text=\"{1}:\"></asp:Label>",
                            ExtraName + FieldItem.DataField, Description);
                    }
                }
                return ControlText + ":";
            }
            else
            {
                return ControlText;
            }
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

#if !VS90

#endif

        private bool IsKeyField(string columnName, DataColumn[] primaryKeys)
        {
            bool isPrimaryKey = false;
            foreach (DataColumn keyCol in primaryKeys)
            {
                if (keyCol.ColumnName == columnName)
                {
                    isPrimaryKey = true;
                    break;
                }
            }
            return isPrimaryKey;
        }

        private void NotifyRefresh(uint SleepTime)
        {
            return;
            //fmVirDialog aForm = new fmVirDialog(true, SleepTime);
            //aForm.Dispose();
        }

        private void SetBlockFieldControls(TBlockItem BlockItem)
        {
            if (BlockItem.Name == "View")
            {
                GenViewBlockControl(BlockItem);
            }
            else if (BlockItem.Name == "Main" || BlockItem.Name == "Master")
            {
                if (FClientData.BaseFormName == "JQMobileSingle1" || FClientData.BaseFormName == "JQMobileSingle2"
                    || FClientData.BaseFormName == "VBJQMobileSingle1" || FClientData.BaseFormName == "VBJQMobileSingle2"
                    || FClientData.BaseFormName == "JQMobileQuery" || FClientData.BaseFormName == "VBJQMobileQuery")
                {
                    GenMainBlockControl(BlockItem);
                }
                else if (FClientData.BaseFormName == "JQMobileMasterDetail1" || FClientData.BaseFormName == "VBJQMobileMasterDetail1")
                {
                    GenMainBlockControl(BlockItem);
                    ////GenMainBlockControl_3(BlockItem, "wfvMaster");
                }
            }
        }

        private void SetBlockFieldDataComponent(TBlockItem BlockItem, string DataSetName)
        {
            InfoBindingSource aBindingSource = FDesignerHost.CreateComponent(typeof(InfoBindingSource), "ibs" + BlockItem.TableName) as InfoBindingSource;
            if (BlockItem.ParentItemName == "")
            {
                aBindingSource.DataSource = FDataSet;
                aBindingSource.DataMember = BlockItem.TableName;
            }
            else
            {
                TBlockItem ParentItem = ((TBlockItems)BlockItem.Collection).FindItem(BlockItem.ParentItemName);
                aBindingSource.DataSource = ParentItem.BindingSource;
                aBindingSource.DataMember = BlockItem.RelationName;
            }
            BlockItem.BindingSource = aBindingSource;
        }

        private void GenBlock(TBlockItem BlockItem, string DataSetName, bool GenField)
        {
            SetBlockFieldControls(BlockItem);
        }

        private void GenDataSet()
        {
            //NotifyRefresh(1000);
            NotifyRefresh(1000);
            //WebDataSet
            string Path = FPI.get_FileNames(0);
            string FileName = System.IO.Path.GetFileNameWithoutExtension(Path) + ".aspx." + FClientData.Language;
            Path = System.IO.Path.GetDirectoryName(Path);
            FileName = Path + "\\" + FileName;
            if (!File.Exists(FileName))
                return;
            System.IO.StreamReader SR = new System.IO.StreamReader(FileName);
            string Context = SR.ReadToEnd();
            SR.Close();
            if (FClientData.Language == "cs")
            {
                Context = Context.Replace("ID=\"dataGridMaster\" RemoteName=\"\"", "ID=\"dataGridMaster\" RemoteName=\"" + FClientData.ProviderName + "\";");
                //Context = Context.Replace("this.WMaster.Active = false;", "this.WMaster.Active = true;");
            }
            else if (FClientData.Language == "vb")
            {
                Context = Context.Replace("Me.WMaster.RemoteName = Nothing", "Me.WMaster.RemoteName = \"" + FClientData.ProviderName + "\"");
                Context = Context.Replace("Me.WMaster.Active = False", "Me.WMaster.Active = True");
            }
            if (FClientData.BaseFormName == "WMasterDetail3")
            {
                Context = Context.Replace("this.WView.RemoteName = null;", "this.WView.RemoteName = \"" + FClientData.ViewProviderName + "\";");
                Context = Context.Replace("this.WView.Active = false;", "this.WView.Active = true;");
            }
            else if (FClientData.BaseFormName == "VBWebCMasterDetail_VFG")
            {
                Context = Context.Replace("Me.WView.RemoteName = Nothing", "Me.WView.RemoteName = \"" + FClientData.ViewProviderName + "\"");
                Context = Context.Replace("Me.WView.Active = False", "Me.WView.Active = True");
            }
            System.IO.FileStream Filefs = new System.IO.FileStream(FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            System.IO.StreamWriter SW = new System.IO.StreamWriter(Filefs);
            SW.Write(Context);
            SW.Close();
            Filefs.Close();
            //WebDataSource Master
            NotifyRefresh(1000);

            //GetSchema
            if (FWizardDataSet != null)
                FWizardDataSet.Dispose();
            FWizardDataSet = new InfoDataSet(true);
            FWizardDataSet.RemoteName = FClientData.ProviderName;
            FWizardDataSet.Active = true;
            FWizardDataSet.ServerModify = false;
            FWizardDataSet.PacketRecords = 100;
            FWizardDataSet.AlwaysClose = false;

#if VS90
            object oMaster = FDesignerDocument.webControls.item("Master", 0);
            if (oMaster != null && oMaster is WebDevPage.IHTMLElement)
            {
                ((WebDevPage.IHTMLElement)oMaster).setAttribute("DataMember", FClientData.TableName, 0);
            }

            //SaveWebDataSet(FWizardDataSet);

            //EditPoint ep1 = FTextWindow.ActivePane.StartPoint.CreateEditPoint();
            //EditPoint ep2 = null;

            //TextRanges textRanges = FTextWindow.Selection.TextRanges;
            //if (ep1.FindPattern("InfoLight:WebDataSource ID=\"Master\"", (int)vsFindOptions.vsFindOptionsMatchWholeWord, ref ep2, ref textRanges))
            //{
            //    ep1.WordRight(7);
            //    ep1.Insert("DataMember=\"" + FClientData.TableName + "\" ");
            //}
#else
            WebDataSource Master = (WebDataSource)FPage.FindControl("Master");
            Master.DataMember = FClientData.TableName;
            IComponentChangeService FComponentChangeService = (IComponentChangeService)FDesignerHost.RootComponent.Site.GetService(typeof(IComponentChangeService));
            FComponentChangeService.OnComponentChanged(Master, null, "", "M");
#endif

        }

        public void SaveWebDataSet(InfoDataSet ds)
        {
            string Path = FPI.get_FileNames(0);
            string FileName = Path + ".vi-VN.resx";

            string keyName = "WebDataSets";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FileName);
            XmlNode aNode = xDoc.DocumentElement.FirstChild;
            while (aNode != null)
            {
                if (aNode.InnerText.Contains(keyName))
                {
                    if (aNode.InnerText.Contains("WView"))
                        FWizardDataSet.RemoteName = FClientData.ViewProviderName;
                    else
                        FWizardDataSet.RemoteName = FClientData.ProviderName;


                    int x, y;
                    String temp;
                    x = aNode.InnerText.IndexOf("<Active>");
                    y = aNode.InnerText.IndexOf("</Active>");
                    temp = aNode.InnerText.Substring(x, (y - x) + 9);
                    aNode.InnerText = aNode.InnerText.Replace(temp, "<Active>" + FWizardDataSet.Active + "</Active>");

                    x = aNode.InnerText.IndexOf("<PacketRecords>");
                    y = aNode.InnerText.IndexOf("</PacketRecords>");
                    temp = aNode.InnerText.Substring(x, (y - x) + 16);
                    aNode.InnerText = aNode.InnerText.Replace(temp, "<PacketRecords>" + FWizardDataSet.PacketRecords + "</PacketRecords>");

                    x = aNode.InnerText.IndexOf("<RemoteName>");
                    y = aNode.InnerText.IndexOf("</RemoteName>");
                    temp = aNode.InnerText.Substring(x, (y - x) + 13);
                    aNode.InnerText = aNode.InnerText.Replace(temp, "<RemoteName>" + FWizardDataSet.RemoteName + "</RemoteName>");

                    x = aNode.InnerText.IndexOf("<ServerModify>");
                    y = aNode.InnerText.IndexOf("</ServerModify>");
                    temp = aNode.InnerText.Substring(x, (y - x) + 15);
                    aNode.InnerText = aNode.InnerText.Replace(temp, "<ServerModify>" + FWizardDataSet.ServerModify + "</ServerModify>");
                    break;
                }
                aNode = aNode.NextSibling;
            }
            xDoc.Save(FileName);
        }

        private void GenDetailBlock(String TemplateName)
        {
            MWizard.TBlockItem BlockItem = null;
            foreach (TBlockItem B in FClientData.Blocks)
            {
                if (B.wDataSource == null && B.BlockFieldItems.Count > 0)
                {
                    BlockItem = B;
                    break;
                }
            }

#if VS90

            JQDefault Default = new JQDefault();
            Default.ID = "defaultDetail"; // +BlockItem.TableName;
            Default.BindingObjectID = "dataGridDetail";

            JQValidate Validate = new JQValidate();
            Validate.ID = "validateDetail"; // +BlockItem.TableName;
            Validate.BindingObjectID = "dataGridDetail";

            WebQueryFiledsCollection QueryFields = new WebQueryFiledsCollection(null, typeof(QueryField));
            WebQueryColumnsCollection QueryColumns = new WebQueryColumnsCollection(null, typeof(QueryColumns));
            foreach (TBlockFieldItem fielditem in BlockItem.BlockFieldItems)
            {
                GenDefault(fielditem, Default, Validate);
                //GenQuery(fielditem, QueryFields, QueryColumns, BlockItem.TableName);
            }

            WebDevPage.IHTMLElement eJQDataGridDetail = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("dataGridDetail", 0);
            if (eJQDataGridDetail != null)
            {
                ((WebDevPage.IHTMLElement)eJQDataGridDetail).setAttribute("RemoteName", FClientData.ProviderName, 0);
                ((WebDevPage.IHTMLElement)eJQDataGridDetail).setAttribute("DataMember", BlockItem.TableName, 0);
                StringBuilder sb = new StringBuilder(eJQDataGridDetail.innerHTML);
                //sb.Append("<Columns>");
                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    int idx = sb.ToString().LastIndexOf("</Columns>");
                    if (idx == -1)
                    {
                        sb.AppendLine("<Columns></Columns>");
                        idx = sb.ToString().LastIndexOf("</Columns>");
                    }
                    String sHeaderText = String.IsNullOrEmpty(BFI.Description) ? BFI.DataField : BFI.Description;

                    var relationOptions = string.Empty;
                    if ((BFI.ControlType == "RefValBox" || BFI.ControlType == "ComboBox") && !string.IsNullOrEmpty(BFI.ComboValueField) && !string.IsNullOrEmpty(BFI.ComboTextField)
                        && !string.IsNullOrEmpty(BFI.ComboRemoteName) && !string.IsNullOrEmpty(BFI.ComboEntityName))
                    {
                        relationOptions = string.Format("RelationOptions=\"{{RemoteName:'{0}',DisplayMember:'{1}',ValueMember:'{2}'}}\""
                            , BFI.ComboRemoteName, BFI.ComboTextField, BFI.ComboValueField);
                    }


                    sb.Insert(idx, String.Format("<JQMobileTools:JQGridColumn Alignment=\"left\" FieldName=\"{0}\" Caption=\"{1}\" Width=\"120\" {2} Format=\"{3}\"/>", BFI.DataField, sHeaderText, relationOptions, BFI.EditMask));
                }
                //sb.Append("</Columns>");
                eJQDataGridDetail.innerHTML = sb.ToString();
            }

            WebDevPage.IHTMLElement eJQDataFormDetail = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("dataFormDetail", 0);
            if (eJQDataFormDetail != null)
            {
                StringBuilder sb = new StringBuilder();
                ((WebDevPage.IHTMLElement)eJQDataFormDetail).setAttribute("RemoteName", FClientData.ProviderName, 0);
                ((WebDevPage.IHTMLElement)eJQDataFormDetail).setAttribute("DataMember", BlockItem.TableName, 0);

                sb.Append("<Columns>");
                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    JQControl control = null;
                    string editor = "text";
                    String sHeaderText = String.IsNullOrEmpty(BFI.Description) ? BFI.DataField : BFI.Description;
                    if ((BFI.ControlType == "ComboBox" || BFI.ControlType == "ComboGrid" || BFI.ControlType == "RefValBox")
                        && (string.IsNullOrEmpty(BFI.ComboValueField) || string.IsNullOrEmpty(BFI.ComboTextField)
                        || string.IsNullOrEmpty(BFI.ComboRemoteName) || string.IsNullOrEmpty(BFI.ComboEntityName)))
                    {
                        BFI.ControlType = "TextBox";
                    }


                    if (BFI.ControlType == "ComboBox")
                    {
                        control = new JQSelects() { RemoteName = BFI.ComboRemoteName, DisplayMember = BFI.ComboTextField, ValueMember = BFI.ComboValueField };
                        editor = "selects";

                    }
                    else if (BFI.ControlType == "RefValBox")
                    {
                        control = new JQRefval() { RemoteName = BFI.ComboRemoteName, DisplayMember = BFI.ComboTextField, ValueMember = BFI.ComboValueField };
                        //(control as JQRefval).Columns.Add(new JQRefValColumn() { FieldName = BFI.ComboValueField, Caption = BFI.ComboValueFieldCaption });
                        editor = "refval";
                    }
                    else if (BFI.ControlType == "RadioButton")
                    {
                        control = new JQRadioButtons() { RemoteName = BFI.ComboRemoteName, DisplayMember = BFI.ComboTextField, ValueMember = BFI.ComboValueField };
                        editor = "radiobuttons";
                    }
                    else if (BFI.ControlType == "CheckBox")
                    {
                        JQCollection<JQDataItem> items = new JQCollection<JQDataItem>(control);
                        JQDataItem item1 = new JQDataItem();
                        item1.Text = "Yes"; item1.Value = "Yes";
                        JQDataItem item2 = new JQDataItem();
                        item2.Text = "No"; item2.Value = "No";
                        items.Add(item1);
                        items.Add(item2);
                        control = new JQCheckBoxes() { RemoteName = BFI.ComboRemoteName, DisplayMember = BFI.ComboTextField, ValueMember = BFI.ComboValueField, Items = items };
                        editor = "checkboxes";
                    }
                    else if (BFI.ControlType == "DateTimeBox")
                    {
                        control = new JQDate();
                        editor = "date";
                    }
                    else
                    {
                        control = new JQTextBox();

                    }
                    sb.AppendFormat("<{0}:{1} Caption=\"{2}\" FieldName=\"{3}\" Editor=\"{4}\" Width=\"120\" EditorOptions=\"{5}\"></{0}:{1}>"
                        , "JQMobileTools", "JQFormColumn", sHeaderText, BFI.DataField, editor, control.Options);
                }
                sb.Append("</Columns>");

                if (BlockItem.Relation != null && BlockItem.Relation.ChildColumns.Length > 0)
                {
                    sb.Append("<RelationColumns>");
                    for (int i = 0; i < BlockItem.Relation.ChildColumns.Length; i++)
                    {
                        sb.AppendFormat("<JQMobileTools:JQRelationColumn FieldName=\"{0}\" ParentFieldName=\"{1}\" />", BlockItem.Relation.ChildColumns[i].ColumnName, BlockItem.Relation.ParentColumns[i].ColumnName);
                    }
                    sb.Append("</RelationColumns>");
                }
                eJQDataFormDetail.innerHTML = sb.ToString();

                WebDevPage.IHTMLElement Page = FDesignerDocument.pageContentElement;
                Default.BindingObjectID = "dataFormDetail";
                InsertControl(Page, Default);
                Validate.BindingObjectID = "dataFormDetail";
                InsertControl(Page, Validate);
            }
#else
                
#endif
        }

        private void GenDetailBlock_2()
        {
            MWizard.TBlockItem BlockItem = null;
            foreach (TBlockItem B in FClientData.Blocks)
            {
                if (B.wDataSource == null && B.BlockFieldItems.Count > 0)
                {
                    BlockItem = B;
                    break;
                }
            }

#if VS90
            object oDetail = FDesignerDocument.webControls.item("Detail", 0);

            WebDevPage.IHTMLElement eDetail = null;

            if (oDetail == null || !(oDetail is WebDevPage.IHTMLElement))
                return;
            eDetail = (WebDevPage.IHTMLElement)oDetail;
            eDetail.setAttribute("DataMember", BlockItem.TableName, 0);

            WebDevPage.IHTMLElement FormView = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("wfvDetail", 0);
            if (FormView != null)
            {
                WebDefault Default = new WebDefault();
                Default.ID = "wd" + BlockItem.TableName;
                Default.DataSourceID = eDetail.getAttribute("ID", 0).ToString();
                Default.DataMember = BlockItem.TableName;

                WebValidate Validate = new WebValidate();
                Validate.ID = "wv" + BlockItem.TableName;
                Validate.DataSourceID = eDetail.getAttribute("ID", 0).ToString();
                Validate.DataMember = BlockItem.TableName;

                WebQueryFiledsCollection QueryFields = new WebQueryFiledsCollection(null, typeof(QueryField));
                WebQueryColumnsCollection QueryColumns = new WebQueryColumnsCollection(null, typeof(QueryColumns));
                foreach (TBlockFieldItem fielditem in BlockItem.BlockFieldItems)
                {
                    //GenDefault(fielditem, Default, Validate);
                    ////GenQuery(fielditem, QueryFields, QueryColumns, BlockItem.TableName);
                }

                WebDevPage.IHTMLElement Page = FDesignerDocument.pageContentElement;
                ////InsertControl(Page, Default);
                ////InsertControl(Page, Validate);

                WebDevPage.IHTMLElement Navigator = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("WebNavigator1", 0);
                if (Navigator != null)
                {
                    ////SetCollectionValue(Navigator, typeof(WebNavigator).GetProperty("QueryFields"), QueryFields);
                }
                WebDevPage.IHTMLElement ClientQuery = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("WebClientQuery1", 0);
                if (ClientQuery != null)
                {
                    ////SetCollectionValue(ClientQuery, typeof(WebClientQuery).GetProperty("Columns"), QueryColumns);
                }

                FormView = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("wfvDetail", 0);
                ////RefreshFormView(FormView, BlockItem);
            }
#else
#endif
        }

        private void RenameForm()
        {
            string Path = FPI.get_FileNames(0);
            Path = System.IO.Path.GetDirectoryName(Path);
            string NewName = FClientData.FormName + ".cs";
            string FileName = Path + "\\" + NewName;
            FPI.SaveAs(FileName);
            System.IO.File.Delete(Path + "\\Form1.cs");
            System.IO.File.Delete(Path + "\\Form1.Designer.cs");
            System.IO.File.Delete(Path + "\\Form1.resx");
        }

        private void UpdateDataSource(TBlockItem MainBlockItem, TBlockItem ViewBlockItem)
        {
            InfoNavigator navigator3 = FRootForm.Controls["infoNavigator1"] as InfoNavigator;
            if (navigator3 != null)
                navigator3.ViewBindingSource = MainBlockItem.BindingSource;

            FViewGrid.DataSource = MainBlockItem.BindingSource;
            TBlockFieldItem FieldItem;
            //???FViewGrid.Columns.Clear();
            DataGridViewColumn Column;
            int I, Index;
            for (I = 0; I < ViewBlockItem.BlockFieldItems.Count; I++)
            {
                FieldItem = ViewBlockItem.BlockFieldItems[I] as TBlockFieldItem;
                if (FieldItem.Description == "")
                    Index = FViewGrid.Columns.Add(FieldItem.DataField, FieldItem.DataField);
                else
                    Index = FViewGrid.Columns.Add(FieldItem.DataField, FieldItem.Description);
                Column = FViewGrid.Columns[Index];
                Column.DataPropertyName = FieldItem.DataField;
                if (Column.HeaderText.Trim() == "")
                    Column.HeaderText = FieldItem.DataField;
            }
        }

        [DllImport("kernel32.dll")]
        public extern static void Sleep(uint msec);

        private void WriteWebDataSourceHTML()
        {
            //if (FWebDataSourceList.Count == 0)
            //    return;

            String FileName = FDesignWindow.Document.FullName;
            FDesignWindow.Close(vsSaveChanges.vsSaveChangesYes);
            System.IO.StreamReader SR = new System.IO.StreamReader(FileName, Encoding.Default);
            String Context = SR.ReadToEnd();
            SR.Close();
            Context = Context.Replace("<title>Untitled Page</title>", "<title>" + FClientData.FormTitle + "</title>");
            System.IO.FileStream Filefs = new System.IO.FileStream(FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            System.IO.StreamWriter SW = new System.IO.StreamWriter(Filefs, Encoding.UTF8);
            SW.Write(Context);
            SW.Close();
            Filefs.Close();

            //FDesignWindow = FPI.Open("{00000000-0000-0000-0000-000000000000}");
            //FDesignWindow.Activate();
        }

        public void GenWebClientModule()
        {
            GenFolder();
            if (GetForm())
            {
                GetDesignerHost();
#if VS90
#else
                DesignerTransaction transaction1 = FDesignerHost.CreateTransaction();
#endif
                try
                {
                    TBlockItem BlockItem;
                    GenDataSet(); //???
                    if (FClientData.IsMasterDetailBaseForm())
                    {
                        BlockItem = FClientData.Blocks.FindItem("View");
                        if (BlockItem != null)
                        {
                            GenBlock(BlockItem, "View", false);
                        }
                        BlockItem = FClientData.Blocks.FindItem("Master");
                        if (BlockItem != null)
                        {
                            GenBlock(BlockItem, "Master", false);
                        }
                        GenDetailBlock(FClientData.BaseFormName);
                    }
                    else
                    {
                        BlockItem = FClientData.Blocks.FindItem("View");
                        if (BlockItem != null)
                        {
                            GenBlock(BlockItem, "Main", false);
                        }
                        TBlockItem MainBlockItem = FClientData.Blocks.FindItem("Main");
                        if (MainBlockItem != null)
                        {
                            GenBlock(MainBlockItem, "Main", false);
                            //UpdateDataSource(MainBlockItem, BlockItem);
                        }
                    }
                    WriteWebDataSourceHTML();
                }
                catch (Exception exception2)
                {
                    MessageBox.Show(exception2.Message);
                    return;
                }
                finally
                {
#if VS90
                    FPI.Name = FClientData.FormName + ".aspx";
                    FDesignWindow = FPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
                    FDesignWindow.Activate();
                    //bool b = FDesignerDocument.execCommand("Refresh", true, "");
#else
                    transaction1.Commit();
#endif
                }
                //RenameForm();
                //FPI.Save(FPI.get_FileNames(0));
                //GlobalWindow.Close(vsSaveChanges.vsSaveChangesYes);
                FProject.Save(FProject.FullName);
                //FDTE2.Solution.SolutionBuild.BuildProject(FDTE2.Solution.SolutionBuild.ActiveConfiguration.Name,
                //    FProject.FullName, true);
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
