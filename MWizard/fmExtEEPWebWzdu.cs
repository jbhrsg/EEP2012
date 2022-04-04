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
#endif


namespace MWizard
{
    public partial class fmExtEEPWebWzdu : Form
    {
        private TExtWebClientData FClientData;
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

        public fmExtEEPWebWzdu()
        {
            InitializeComponent();
            FClientData = new TExtWebClientData(this);
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

        public fmExtEEPWebWzdu(DTE2 aDTE2, AddIn aAddIn)
        {
            InitializeComponent();
            FClientData = new TExtWebClientData(this);
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
            cbWebForm.Text = "WSingle";
            lvViewSrcField.Items.Clear();
            lvViewDesField.Items.Clear();
            ClearRefValButton(lvMasterDesField);
            lvMasterDesField.Items.Clear();
            lvMasterSrcField.Items.Clear();
            FClientData.Blocks.Clear();
            tvRelation.Nodes.Clear();
            ClearRefValButton(lvSelectedFields);
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
            if ((fmExtEEPWebWzdu._serverPath == null) || (fmExtEEPWebWzdu._serverPath.Length == 0))
            {
                fmExtEEPWebWzdu._serverPath = EEPRegistry.Server + "\\";
            }
            return fmExtEEPWebWzdu._serverPath;
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

        public void ShowWebClientWizard()
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
            TExtWebClientGenerator CG = new TExtWebClientGenerator(FClientData, FDTE2, FAddIn);
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
                    aBlockFieldItem.QueryMode = DR["QUERYMODE"].ToString();
                    if (DR["FIELD_LENGTH"] != null && DR["FIELD_LENGTH"].ToString() != "")
                        aBlockFieldItem.Length = Convert.ToInt32(DR["FIELD_LENGTH"]);
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
                            this.cbWebForm.Items.Add("ExtSingle");
                            this.cbWebForm.Items.Add("ExtMasterDetail");
                            this.cbWebForm.Items.Add("ExtQuery");
                            this.cbWebForm.SelectedIndex = 0;
                            break;
                        case "VB":
                            this.cbWebForm.Items.Clear();
                            this.cbWebForm.Items.Add("VBWebSingle");
                            this.cbWebForm.Items.Add("VBWebSingle0");
                            this.cbWebForm.Items.Add("VBWebSingle1");
                            this.cbWebForm.Items.Add("VBWebSingle2");
                            this.cbWebForm.Items.Add("VBWebSingle3");
                            this.cbWebForm.Items.Add("VBWebSingle4");
                            this.cbWebForm.Items.Add("VBWebSingle5");
                            this.cbWebForm.Items.Add("VBWebCMasterDetail_FG");
                            this.cbWebForm.Items.Add("VBWebCMasterDetail_DG");
                            this.cbWebForm.Items.Add("VBWebCMasterDetail_VFG");
                            this.cbWebForm.Items.Add("VBWebCMasterDetail4");
                            this.cbWebForm.Items.Add("VBWebCMasterDetail5");
                            this.cbWebForm.Items.Add("VBWebMasterDetail6");
                            this.cbWebForm.Items.Add("VBWebMasterDetail7");
                            this.cbWebForm.Items.Add("VBWebCMasterDetail8");
                            this.cbWebForm.Items.Add("VBWebQuery");
                            this.cbWebForm.SelectedIndex = 0;
                            break;
                    }
                }
                else
                {
                    this.cbWebForm.Items.Clear();
                    this.cbWebForm.Items.Clear();
                    this.cbWebForm.Items.Add("ExtSingle");
                    this.cbWebForm.Items.Add("ExtMasterDetail");
                    this.cbWebForm.Items.Add("ExtQuery");
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
                    cbViewProviderName.Visible = (FClientData.BaseFormName.CompareTo("WMasterDetail3") == 0 || FClientData.BaseFormName.CompareTo("VBWebCMasterDetail_VFG") == 0 || FClientData.BaseFormName.CompareTo("ExtMasterDetail") == 0 || FClientData.BaseFormName.CompareTo("VBWebCMasterDetail8") == 0);
                    label18.Visible = cbViewProviderName.Visible;
                    DisplayPage(tpDataSource);
                }
            }
            else if (tabControl.SelectedTab.Equals(tpDataSource))
            {
                ClearAllControls();
                InitMasterAndDetail();
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
                    if (FClientData.BaseFormName == "WMasterDetail3" || FClientData.BaseFormName == "VBWebCMasterDetail_VFG" || FClientData.BaseFormName == "ExtMasterDetail"
                        || FClientData.BaseFormName == "WSingle2" || FClientData.BaseFormName == "WSingle3" || FClientData.BaseFormName == "WSingle4"
                        || FClientData.BaseFormName == "ExtSingle" || FClientData.BaseFormName == "VBWebCMasterDetail8" || FClientData.BaseFormName == "VBWebSingle5")
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
                        || FClientData.BaseFormName == "ExtMasterDetail" || FClientData.BaseFormName == "VBWebCMasterDetail8")
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

        private void AddDetailBlockItem(string MasterItemName, System.Windows.Forms.TreeNodeCollection NodeCollection)
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

        private void AddDetailBlockItem(string MasterItemName, System.Windows.Forms.TreeNodeCollection NodeCollection, ListView LV)
        {
            for (int I = 0; I < NodeCollection.Count; I++)
            {
                TDetailItem DetailItem = (TDetailItem)NodeCollection[I].Tag;
                TBlockItem BlockItem = new TBlockItem();
                BlockItem.Name = NodeCollection[I].Text;
                BlockItem.RelationName = DetailItem.Relation.RelationName;
                BlockItem.TableName = DetailItem.TableName;
                for (int J = 0; J < LV.Items.Count; J++)
                {
                    ListViewItem aItem = LV.Items[J];
                    TBlockFieldItem BlockFieldItem = new TBlockFieldItem();
                    if (aItem.Tag != null)
                    {
                        BlockFieldItem.DataField = ((TBlockFieldItem)aItem.Tag).DataField;
                        BlockFieldItem.CheckNull = ((TBlockFieldItem)aItem.Tag).CheckNull;
                        BlockFieldItem.DefaultValue = ((TBlockFieldItem)aItem.Tag).DefaultValue;
                        BlockFieldItem.Description = ((TBlockFieldItem)aItem.Tag).Description;
                        BlockFieldItem.RefValNo = ((TBlockFieldItem)aItem.Tag).RefValNo;
                        BlockFieldItem.ControlType = ((TBlockFieldItem)aItem.Tag).ControlType;
                        BlockFieldItem.ComboRemoteName = ((TBlockFieldItem)aItem.Tag).ComboRemoteName;
                        BlockFieldItem.ComboEntityName = ((TBlockFieldItem)aItem.Tag).ComboEntityName;
                        BlockFieldItem.ComboEntitySetName = ((TBlockFieldItem)aItem.Tag).ComboEntitySetName;
                        BlockFieldItem.ComboTextField = ((TBlockFieldItem)aItem.Tag).ComboTextField;
                        BlockFieldItem.ComboValueField = ((TBlockFieldItem)aItem.Tag).ComboValueField;
                        BlockFieldItem.DataType = ((TBlockFieldItem)aItem.Tag).DataType;
                        BlockFieldItem.QueryMode = ((TBlockFieldItem)aItem.Tag).QueryMode;
                        BlockFieldItem.EditMask = ((TBlockFieldItem)aItem.Tag).EditMask;
                        BlockFieldItem.Length = ((TBlockFieldItem)aItem.Tag).Length;
                        BlockFieldItem.IsKey = ((TBlockFieldItem)aItem.Tag).IsKey;
                        BlockFieldItem.ComboTextFieldCaption = ((TBlockFieldItem)aItem.Tag).ComboTextFieldCaption;
                        BlockFieldItem.ComboValueFieldCaption = ((TBlockFieldItem)aItem.Tag).ComboValueFieldCaption;
                    }
                    else
                    {
                        BlockFieldItem.DataField = aItem.Text;
                    }
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
            TExtWebClientGenerator Generator = new TExtWebClientGenerator(FClientData, FDTE2, FAddIn);
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
                if (FClientData.BaseFormName == "WMasterDetail3" || FClientData.BaseFormName == "VBWebCMasterDetail_VFG" || FClientData.BaseFormName == "ExtMasterDetail"
                    || FClientData.BaseFormName == "VBWebCMasterDetail8")
                    AddBlockItem("View", FClientData.ProviderName, FClientData.TableName, lvViewDesField);
                AddBlockItem("Master", FClientData.ProviderName, FClientData.TableName, lvMasterDesField);
                AddDetailBlockItem("Master", tvRelation.Nodes, lvSelectedFields);
            }
            else
            {
                if (FClientData.BaseFormName == "WSingle2" || FClientData.BaseFormName == "WSingle3" || FClientData.BaseFormName == "WSingle4"
                    || FClientData.BaseFormName == "ExtSingle" || FClientData.BaseFormName == "VBWebSingle5" || FClientData.BaseFormName == "VBWebSingle2"
                    || FClientData.BaseFormName == "VBWebSingle3" || FClientData.BaseFormName == "VBWebSingle4")
                    AddBlockItem("View", FClientData.ProviderName, FClientData.TableName, lvViewDesField);
                AddBlockItem("Main", FClientData.ProviderName, FClientData.TableName, lvMasterDesField);
            }
            Hide();
            FDTE2.MainWindow.Activate();
            DoGenClient();
            FInfoDataSet.Dispose();
            FInfoDataSet = null;
            ClearAllControls();

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
                    BlockFieldItem.ComboTextField = ((TBlockFieldItem)aItem.Tag).ComboTextField;
                    BlockFieldItem.ComboValueField = ((TBlockFieldItem)aItem.Tag).ComboValueField;
                    BlockFieldItem.DataType = ((TBlockFieldItem)aItem.Tag).DataType;
                    BlockFieldItem.QueryMode = ((TBlockFieldItem)aItem.Tag).QueryMode;
                    BlockFieldItem.EditMask = ((TBlockFieldItem)aItem.Tag).EditMask;
                    BlockFieldItem.Length = ((TBlockFieldItem)aItem.Tag).Length;
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
            TExtWebClientGenerator G = new TExtWebClientGenerator(FClientData, FDTE2, FAddIn);
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
                        DetailItem.BlockFieldItems.Remove(item3);
                        lvSelectedFields.Items.Remove(item2);
                    }
                }
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
            //    if (LVI.SubItems.Count > 2)
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
                case "WSingle":
                case "VBWebSingle":
                    this.label16.Text = templateName + ": WebGridView";
                    break;
                case "WSingle0":
                case "VBWebSingle0":
                    this.label16.Text = templateName + ": WebFormView";
                    break;
                case "WSingle1":
                case "VBWebSingle1":
                    this.label16.Text = templateName + ": WebFormView + WebTranslate";
                    break;
                case "WSingle2":
                case "VBWebSingle2":
                    this.label16.Text = templateName + ": MultiView + WebGridView(View) + WebFormView(Edit)";
                    break;
                case "WSingle3":
                case "VBWebSingle3":
                    this.label16.Text = templateName + ": AjaxModalPanel + WebGridView(View) + WebFormView(Edit)";
                    break;
                case "WSingle4":
                case "VBWebSingle4":
                    this.label16.Text = templateName + ": AjaxGridView(View) + WebFormView(Edit)";
                    break;
                case "ExtSingle":
                case "VBWebSingle5":
                    this.label16.Text = templateName + ": AjaxGridView(View) + AjaxFormView(Master)";
                    break;
                case "WMasterDetail1":
                case "VBWebCMasterDetail_FG":
                    this.label16.Text = templateName + ": WebFormView(Master) + WebGridView(Detail)";
                    break;
                case "WMasterDetail2":
                case "VBWebCMasterDetail_DG":
                    this.label16.Text = templateName + ": WebDetailView(Master) + WebGridView(Detail)";
                    break;
                case "WMasterDetail3":
                case "VBWebCMasterDetail_VFG":
                    this.label16.Text = templateName + ": WebGridView(View) + WebFormView(Master) + WebGridView(Detail)";
                    break;
                case "WMasterDetail4":
                case "VBWebCMasterDetail4":
                    this.label16.Text = templateName + ": WebFormView(Master) + MultiView(Detail) + WebGridView(View) + WebFormView(Edit)";
                    break;
                case "WMasterDetail6":
                case "VBWebMasterDetail6":
                    this.label16.Text = templateName + ": WebFormView(Master) + AjaxModalPanel(Detail) + WebGridView(View) + WebFormView(Edit)";
                    break;
                case "WMasterDetail7":
                case "VBWebMasterDetail7":
                    this.label16.Text = templateName + ": WebFormView(Master) + AjaxGridView(Detail)";
                    break;
                case "ExtMasterDetail":
                case "VBWebMasterDetail8":
                    this.label16.Text = templateName + ": AjaxGridView(View) + AjaxFormView(Master) + AjaxGridView(Detail)";
                    break;
                case "WQuery":
                case "VBWebQuery":
                    this.label16.Text = templateName + ": WebClientQuery + WebGridView";
                    break;
                case "WQuery1":
                    this.label16.Text = templateName + ": Timer + WebClientQuery + WebGridView";
                    break;
                case "ExtQuery":
                    this.label16.Text = templateName + ": WebClientQuery + AjaxGridView";
                    break;
            }
        }

        private TBlockFieldItem FSelectedBlockFieldItem;
        private ListViewItem FSelectedListViewItem;
        private Boolean FDisplayValue = false;
        private TWizardType FWizardType;
        private ListViewColumnSorter lvwColumnSorter;
        private InfoCommand FInfoCommand;
        private ClientType FDatabaseType;
        private DbConnection FConnection;
        private String DBAlias;

        private void InitMasterAndDetail()
        {
            FConnection = InternalConnection;
            FDatabaseType = FClientData.DatabaseType;
            DBAlias = FClientData.DatabaseName;
            FWizardType = TWizardType.wtWebPage;
            FInfoCommand = new InfoCommand(DBAlias);
            InitData();
            InitData_D();
        }

        private void ClearAllControls()
        {
            this.tbCaption.Text = String.Empty;
            this.tbCaption_D.Text = String.Empty;
            this.tbDefaultValue.Text = String.Empty;
            this.tbDefaultValue_D.Text = String.Empty;
            this.tbEditMask.Text = String.Empty;
            this.tbEditMask_D.Text = String.Empty;

            this.cbCheckNull.SelectedIndex = -1;
            this.cbCheckNull_D.SelectedIndex = -1;
            this.cbDataTextField_D.SelectedIndex = -1;
            this.cbDataValueField_D.SelectedIndex = -1;
            this.cbControlType.SelectedIndex = -1;
            this.cbControlType_D.SelectedIndex = -1;
            this.cbDataTextField.SelectedIndex = -1;
            this.cbDataValueField.SelectedIndex = -1;
            this.cbQueryMode.SelectedIndex = -1;
            this.cbQueryMode_D.SelectedIndex = -1;
            this.cbTableName.SelectedIndex = -1;
            this.cbTableName_D.SelectedIndex = -1;
            this.cbRefValNo.SelectedIndex = -1;
            this.cbRefValNo_D.SelectedIndex = -1;
            //this.cbRefValNo.Text = string.Empty;
            //this.cbRefValNo_D.Text = string.Empty;
        }

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

        private void DisplayValue()
        {
            if (FSelectedBlockFieldItem == null)
                return;
            cbControlType.Text = "TextBox";
            tbCaption.Text = FSelectedBlockFieldItem.Description;
            cbCheckNull.Text = FSelectedBlockFieldItem.CheckNull;
            tbDefaultValue.Text = FSelectedBlockFieldItem.DefaultValue;
            cbControlType.Text = FSelectedBlockFieldItem.ControlType;
            cbTableName.Text = FSelectedBlockFieldItem.ComboEntityName;
            cbDataTextField.Text = FSelectedBlockFieldItem.ComboTextField;
            cbDataValueField.Text = FSelectedBlockFieldItem.ComboValueField;
            cbRefValNo.Text = FSelectedBlockFieldItem.RefValNo;
            cbQueryMode.Text = FSelectedBlockFieldItem.QueryMode;
            tbEditMask.Text = FSelectedBlockFieldItem.EditMask;
            //if (cbCheckNull.Text == "" || cbCheckNull.Text == null)
            //    cbCheckNull.Text = "N";
            if (cbControlType.Text == "" || cbControlType.Text == null)
                cbControlType.Text = "TextBox";
            if (cbQueryMode.Text == null || cbQueryMode.Text == "")
                cbQueryMode.Text = "None";
        }

        private void SetValue()
        {
            if (FSelectedBlockFieldItem == null)
                return;
            FSelectedBlockFieldItem.Description = tbCaption.Text;
            FSelectedBlockFieldItem.CheckNull = cbCheckNull.Text;
            FSelectedBlockFieldItem.DefaultValue = tbDefaultValue.Text;
            FSelectedBlockFieldItem.ControlType = cbControlType.Text;
            FSelectedBlockFieldItem.RefValNo = cbRefValNo.Text;
            FSelectedBlockFieldItem.ComboEntityName = cbTableName.Text;
            FSelectedBlockFieldItem.ComboTextField = cbDataTextField.Text;
            FSelectedBlockFieldItem.ComboValueField = cbDataValueField.Text;
            FSelectedBlockFieldItem.QueryMode = cbQueryMode.Text;
            FSelectedBlockFieldItem.EditMask = tbEditMask.Text;

            FSelectedListViewItem.SubItems[1].Text = FSelectedBlockFieldItem.Description;
        }

        private void InitData()
        {
            FInfoCommand.Connection = FConnection;

            if (FDatabaseType != ClientType.ctInformix)
            {
                String[] Params = null;
                String ViewFieldName = "TABLE_NAME";
                if (FDatabaseType == ClientType.ctOracle)
                {
                    String UserID = WzdUtils.GetFieldParam(FConnection.ConnectionString.ToLower(), "user id");
                    Params = new String[] { UserID.ToUpper() };
                    ViewFieldName = "VIEW_NAME";
                }
                DataTable T = FConnection.GetSchema("Tables", Params);
                SortedList<String, String> sTable = new SortedList<String, String>();
                foreach (DataRow DR in T.Rows)
                {
                    sTable.Add(DR["TABLE_NAME"].ToString(), DR["TABLE_NAME"].ToString());
                }

                DataTable D1 = FConnection.GetSchema("Views", Params);
                foreach (DataRow DR in D1.Rows)
                {
                    if (!sTable.ContainsKey(DR[ViewFieldName].ToString()))
                        sTable.Add(DR[ViewFieldName].ToString(), DR[ViewFieldName].ToString());
                }

                foreach (var item in sTable)
                    cbTableName.Items.Add(item.Key);
            }
            else
            {
                List<String> allTables = WzdUtils.GetAllTablesList(FConnection, ClientType.ctInformix);
                allTables.Sort();
                foreach (String str in allTables)
                    cbTableName.Items.Add(str);
            }
            cbTableName.Items.Add("");
        }

        private void cbControlType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbControlType.Text == "ComboBox")
            {
                if (cbRefValNo.Items.Count == 0)
                {
                    FInfoCommand.Connection = WzdUtils.AllocateConnection(DBAlias, FDatabaseType, true);
                    FInfoCommand.CommandText = "Select REFVAL_NO from SYS_REFVAL";
                    IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(FInfoCommand);
                    DataSet aDataSet = new DataSet();
                    WzdUtils.FillDataAdapter(FDatabaseType, DA, aDataSet, "SYS_REFVAL");
                    DataTable aDataTable = aDataSet.Tables[0];
                    cbRefValNo.Items.Add("");
                    foreach (DataRow DR in aDataTable.Rows)
                    {
                        cbRefValNo.Items.Add(DR["REFVAL_NO"].ToString());
                    }

                }
                cbTableName.Text = "";
                cbDataTextField.Text = "";
                cbDataValueField.Text = "";
                cbTableName.Enabled = true;
                cbDataTextField.Enabled = true;
                cbDataValueField.Enabled = true;
                cbRefValNo.Enabled = true;
                //btnRefVal.Enabled = true;
            }
            else
            {
                cbTableName.Text = "";
                cbDataTextField.Text = "";
                cbDataValueField.Text = "";
                cbTableName.Enabled = false;
                cbDataTextField.Enabled = false;
                cbDataValueField.Enabled = false;
                cbRefValNo.Text = "";
                cbRefValNo.Enabled = false;
                //btnRefVal.Enabled = false;
            }
        }

        private void cbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTableName.Text == "")
                return;
            cbDataTextField.Items.Clear();
            cbDataValueField.Items.Clear();
            InfoCommand aInfoCommand = new InfoCommand(FDatabaseType);
            aInfoCommand.Connection = FConnection;
            aInfoCommand.CommandText = String.Format("Select * from {0} where 1=0", cbTableName.Text);
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet aDataSet = new DataSet();
            try
            {
                WzdUtils.FillDataAdapter(FDatabaseType, DA, aDataSet, cbTableName.Text);

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
                MessageBox.Show(cbTableName.Text + " is a illegal table.");
                cbTableName.Text = String.Empty;
            }
        }
        private void tbCaption_TextChanged(object sender, EventArgs e)
        {
            if (!FDisplayValue)
                SetValue();
        }

        private void btnRefVal_Click(object sender, EventArgs e)
        {
            fmRefVal aForm = new fmRefVal(FConnection, FDatabaseType, DBAlias);
            String RefValNo = aForm.ShowRefValForm();
            cbRefValNo.Text = RefValNo;
        }

        private void cbRefValNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRefValNo.SelectedItem != null && cbRefValNo.SelectedItem.ToString() != "")
            {
                string sName = cbRefValNo.SelectedItem.ToString();
                FInfoCommand.Connection = WzdUtils.AllocateConnection(DBAlias, FDatabaseType, true);
                FInfoCommand.CommandText = "Select * from SYS_REFVAL where REFVAL_NO = '" + sName + "'";
                IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(FInfoCommand);
                DataSet aDataSet = new DataSet();
                WzdUtils.FillDataAdapter(FDatabaseType, DA, aDataSet, "SYS_REFVAL");
                DataTable aDataTable = aDataSet.Tables[0];
                if (aDataTable.Rows.Count > 0 && aDataTable.Rows[0][0].ToString() != "")
                {
                    cbTableName.Text = aDataTable.Rows[0]["TABLE_NAME"].ToString();
                    cbDataTextField.Text = aDataTable.Rows[0]["DISPLAY_MEMBER"].ToString();
                    cbDataValueField.Text = aDataTable.Rows[0]["VALUE_MEMBER"].ToString();
                }
            }
            else
            {
                cbTableName.Text = "";
                cbDataTextField.Text = "";
                cbDataValueField.Text = "";
                cbRefValNo.Text = "";
            }
        }

        //detail
        private TBlockFieldItem FSelectedBlockFieldItem_D;
        private ListViewItem FSelectedListViewItem_D;
        private Boolean FDisplayValue_D = false;

        private void DisplayValue_D()
        {
            if (FSelectedBlockFieldItem_D == null)
                return;
            cbControlType_D.Text = "TextBox";
            tbCaption_D.Text = FSelectedBlockFieldItem_D.Description;
            cbCheckNull_D.Text = FSelectedBlockFieldItem_D.CheckNull;
            tbDefaultValue_D.Text = FSelectedBlockFieldItem_D.DefaultValue;
            cbControlType_D.Text = FSelectedBlockFieldItem_D.ControlType;
            cbTableName_D.Text = FSelectedBlockFieldItem_D.ComboEntityName;
            cbDataTextField_D.Text = FSelectedBlockFieldItem_D.ComboTextField;
            cbDataValueField_D.Text = FSelectedBlockFieldItem_D.ComboValueField;
            cbRefValNo_D.Text = FSelectedBlockFieldItem_D.RefValNo;
            cbQueryMode_D.Text = FSelectedBlockFieldItem_D.QueryMode;
            tbEditMask_D.Text = FSelectedBlockFieldItem_D.EditMask;
            //if (cbCheckNull.Text == "" || cbCheckNull.Text == null)
            //    cbCheckNull.Text = "N";
            if (cbControlType_D.Text == "" || cbControlType_D.Text == null)
                cbControlType_D.Text = "TextBox";
            if (cbQueryMode_D.Text == null || cbQueryMode_D.Text == "")
                cbQueryMode_D.Text = "None";
        }

        private void SetValue_D()
        {
            if (FSelectedBlockFieldItem_D == null)
                return;
            FSelectedBlockFieldItem_D.Description = tbCaption_D.Text;
            FSelectedBlockFieldItem_D.CheckNull = cbCheckNull_D.Text;
            FSelectedBlockFieldItem_D.DefaultValue = tbDefaultValue_D.Text;
            FSelectedBlockFieldItem_D.ControlType = cbControlType_D.Text;
            FSelectedBlockFieldItem_D.RefValNo = cbRefValNo_D.Text;
            FSelectedBlockFieldItem_D.ComboEntityName = cbTableName_D.Text;
            FSelectedBlockFieldItem_D.ComboTextField = cbDataTextField_D.Text;
            FSelectedBlockFieldItem_D.ComboValueField = cbDataValueField_D.Text;
            FSelectedBlockFieldItem_D.QueryMode = cbQueryMode_D.Text;
            FSelectedBlockFieldItem_D.EditMask = tbEditMask_D.Text;

            FSelectedListViewItem_D.SubItems[1].Text = FSelectedBlockFieldItem_D.Description;
        }

        private void InitData_D()
        {
            FInfoCommand.Connection = FConnection;

            if (FDatabaseType != ClientType.ctInformix)
            {
                String[] Params = null;
                String ViewFieldName = "TABLE_NAME";
                if (FDatabaseType == ClientType.ctOracle)
                {
                    String UserID = WzdUtils.GetFieldParam(FConnection.ConnectionString.ToLower(), "user id");
                    Params = new String[] { UserID.ToUpper() };
                    ViewFieldName = "VIEW_NAME";
                }
                DataTable T = FConnection.GetSchema("Tables", Params);
                SortedList<String, String> sTable = new SortedList<String, String>();
                foreach (DataRow DR in T.Rows)
                {
                    sTable.Add(DR["TABLE_NAME"].ToString(), DR["TABLE_NAME"].ToString());
                }

                DataTable D1 = FConnection.GetSchema("Views", Params);
                foreach (DataRow DR in D1.Rows)
                {
                    if (!sTable.ContainsKey(DR[ViewFieldName].ToString()))
                        sTable.Add(DR[ViewFieldName].ToString(), DR[ViewFieldName].ToString());
                }

                foreach (var item in sTable)
                    cbTableName_D.Items.Add(item.Key);
            }
            else
            {
                List<String> allTables = WzdUtils.GetAllTablesList(FConnection, ClientType.ctInformix);
                allTables.Sort();
                foreach (String str in allTables)
                    cbTableName_D.Items.Add(str);
            }
            cbTableName_D.Items.Add("");
        }

        private void cbControlType_D_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbControlType_D.Text == "ComboBox")
            {
                if (cbRefValNo_D.Items.Count == 0)
                {
                    FInfoCommand.Connection = WzdUtils.AllocateConnection(DBAlias, FDatabaseType, true);
                    FInfoCommand.CommandText = "Select REFVAL_NO from SYS_REFVAL";
                    IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(FInfoCommand);
                    DataSet aDataSet = new DataSet();
                    WzdUtils.FillDataAdapter(FDatabaseType, DA, aDataSet, "SYS_REFVAL");
                    DataTable aDataTable = aDataSet.Tables[0];
                    cbRefValNo_D.Items.Add("");
                    foreach (DataRow DR in aDataTable.Rows)
                    {
                        cbRefValNo_D.Items.Add(DR["REFVAL_NO"].ToString());
                    }

                }
                cbTableName_D.Text = "";
                cbDataTextField_D.Text = "";
                cbDataValueField_D.Text = "";
                cbTableName_D.Enabled = true;
                cbDataTextField_D.Enabled = true;
                cbDataValueField_D.Enabled = true;
                cbRefValNo_D.Text = "";
                cbRefValNo_D.Enabled = true;
            }
            else
            {
                cbTableName_D.Text = "";
                cbDataTextField_D.Text = "";
                cbDataValueField_D.Text = "";
                cbTableName_D.Enabled = false;
                cbDataTextField_D.Enabled = false;
                cbDataValueField_D.Enabled = false;
                cbRefValNo_D.Text = "";
                cbRefValNo_D.Enabled = false;
                //btnRefVal_D.Enabled = false;
            }
        }

        private void cbTableName_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTableName_D.Text == "")
                return;
            cbDataTextField_D.Items.Clear();
            cbDataValueField_D.Items.Clear();
            InfoCommand aInfoCommand = new InfoCommand(FDatabaseType);
            aInfoCommand.Connection = FConnection;
            aInfoCommand.CommandText = String.Format("Select * from {0} where 1=0", cbTableName_D.Text);
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet aDataSet = new DataSet();
            try
            {
                WzdUtils.FillDataAdapter(FDatabaseType, DA, aDataSet, cbTableName_D.Text);

                foreach (DataColumn DC in aDataSet.Tables[0].Columns)
                {
                    cbDataTextField_D.Items.Add(DC.ColumnName);
                    cbDataValueField_D.Items.Add(DC.ColumnName);
                }
                cbDataTextField_D.Items.Add("");
                cbDataValueField_D.Items.Add("");
            }
            catch
            {
                MessageBox.Show(cbTableName_D.Text + " is a illegal table.");
                cbTableName_D.Text = String.Empty;
            }
        }
        private void tbCaption_D_TextChanged(object sender, EventArgs e)
        {
            if (!FDisplayValue_D)
                SetValue_D();
        }

        private void btnRefVal_D_Click(object sender, EventArgs e)
        {
            fmRefVal aForm = new fmRefVal(FConnection, FDatabaseType, DBAlias);
            String RefValNo = aForm.ShowRefValForm();
            cbRefValNo_D.Text = RefValNo;
        }

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

        private void btnRefVal_D_Click_1(object sender, EventArgs e)
        {

        }

        private void cbRefValNo_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRefValNo_D.SelectedItem != null && cbRefValNo_D.SelectedItem.ToString() != "")
            {
                string sName = cbRefValNo_D.SelectedItem.ToString();
                FInfoCommand.Connection = WzdUtils.AllocateConnection(DBAlias, FDatabaseType, true);
                FInfoCommand.CommandText = "Select * from SYS_REFVAL where REFVAL_NO = '" + sName + "'";
                IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(FInfoCommand);
                DataSet aDataSet = new DataSet();
                WzdUtils.FillDataAdapter(FDatabaseType, DA, aDataSet, "SYS_REFVAL");
                DataTable aDataTable = aDataSet.Tables[0];
                if (aDataTable.Rows.Count > 0 && aDataTable.Rows[0][0].ToString() != "")
                {
                    cbTableName_D.Text = aDataTable.Rows[0]["TABLE_NAME"].ToString();
                    cbDataTextField_D.Text = aDataTable.Rows[0]["DISPLAY_MEMBER"].ToString();
                    cbDataValueField_D.Text = aDataTable.Rows[0]["VALUE_MEMBER"].ToString();
                }
            }
            else
            {
                cbTableName_D.Text = "";
                cbDataTextField_D.Text = "";
                cbDataValueField_D.Text = "";
                cbRefValNo_D.Text = "";
            }

        }

    }

    public class TExtWebClientData : Object
    {
        private string FPackageName, FBaseFormName, FServerPackageName, FFolderName, FTableName, FRealTableName, FFormName, FProviderName,
            FDatabaseName, FSolutionName, FViewProviderName, FWebSiteName,FWebSiteFullName, FFolderMode, FFormTitle;
        private TBlockItems FBlocks;
        private MWizard.fmExtEEPWebWzdu FOwner;
        private bool FNewSolution = false;
        private string FCodeFolderName;
        private int FColumnCount;
        private ClientType FDatabaseType;
        private String FConnString;
        private String FLanguage = "cs";

        public TExtWebClientData(MWizard.fmExtEEPWebWzdu Owner)
        {
            FOwner = Owner;
            FBlocks = new TBlockItems(this);
        }

        public ClientType DatabaseType
        {
            get { return FDatabaseType; }
            set { FDatabaseType = value; }
        }

        public fmExtEEPWebWzdu Owner
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
            if (string.Compare(FBaseFormName, "WMasterDetail1") == 0 ||
                string.Compare(FBaseFormName, "WMasterDetail2") == 0 ||
                string.Compare(FBaseFormName, "WMasterDetail3") == 0 ||
                string.Compare(FBaseFormName, "WMasterDetail4") == 0 ||
                string.Compare(FBaseFormName, "WMasterDetail6") == 0 ||
                string.Compare(FBaseFormName, "WMasterDetail7") == 0 ||
                string.Compare(FBaseFormName, "ExtMasterDetail") == 0 ||
                string.Compare(FBaseFormName, "VBWebCMasterDetail_FG") == 0 ||
                string.Compare(FBaseFormName, "VBWebCMasterDetail_DG") == 0 ||
                string.Compare(FBaseFormName, "VBWebCMasterDetail_VFG") == 0 ||
                string.Compare(FBaseFormName, "VBWebCMasterDetail4") == 0 ||
                string.Compare(FBaseFormName, "VBWebCMasterDetail8") == 0)
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

    partial class TExtWebClientGenerator : System.ComponentModel.Component
    {
        private TExtWebClientData FClientData;
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

        public TExtWebClientGenerator(TExtWebClientData ClientData, DTE2 dte2, AddIn aAddIn)
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
                            FPI = PI;
                            break;
                        }
                    }
                    break;
                case "NewFolder":
                    FPI = FProject.ProjectItems.AddFolder(FClientData.FolderName, "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}");
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
                TemplatePath = System.IO.Path.Combine(EEPRegistry.WebClient, "Template");
            else
                TemplatePath = System.IO.Path.Combine(FClientData.WebSiteFullName, "Template");
            if (TemplatePath == "")
            {
                MessageBox.Show("Cannot find WebTemplate path: {0}", TemplatePath);
                return false;
            }
            if (FPI != null)
            {
                foreach (ProjectItem aPI in FPI.ProjectItems)
                {
                    if (string.Compare(FClientData.FormName + ".aspx", aPI.Name) == 0 ||
                        string.Compare(FClientData.FormName + ".aspx.resx", aPI.Name) == 0 ||
                        string.Compare(FClientData.FormName + ".aspx.vi-VN.resx", aPI.Name) == 0)
                    {
                        DialogResult dr = MessageBox.Show("There is another File which name is " + FClientData.PackageName + " existed! Do you want to delete it first", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            string Path = aPI.get_FileNames(0);

                            aPI.Name = Guid.NewGuid().ToString();
                            //if (aPI.Document != null && aPI.Document.ActiveWindow != null)
                            //{
                            //    aPI.Document.ActiveWindow.Close(vsSaveChanges.vsSaveChangesYes);
                            //}

                            aPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");

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
                    if (string.Compare(FClientData.BaseFormName + ".aspx", aPI.Name) == 0 ||
                        string.Compare(FClientData.BaseFormName + ".aspx.resx", aPI.Name) == 0 ||
                        string.Compare(FClientData.BaseFormName + ".aspx.vi-VN.resx", aPI.Name) == 0)
                    {
                        string Path = aPI.get_FileNames(0);

                        aPI.Name = Guid.NewGuid().ToString();
                        //if (aPI.Document != null && aPI.Document.ActiveWindow != null)
                        //{
                        //    aPI.Document.ActiveWindow.Close(vsSaveChanges.vsSaveChangesYes);
                        //}

                        aPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");

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

                ProjectItem TempPI = FPI;
                FPI = FPI.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + FClientData.BaseFormName + ".aspx");
                //FPI.Name = Guid.NewGuid().ToString() + ".aspx";
                FPI.Name = FClientData.FormName + ".aspx";
                ProjectItem P1 = TempPI.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + FClientData.BaseFormName + ".aspx.resx");
                P1.Name = FClientData.FormName + ".aspx.resx";
                ProjectItem P2 = TempPI.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + FClientData.BaseFormName + ".aspx.vi-VN.resx");
                P2.Name = FClientData.FormName + ".aspx.vi-VN.resx";
                FResxFileName = P2.Name;

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
                ProjectItem P1 = FProject.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + FClientData.BaseFormName + ".aspx.resx");
                P1.Name = FClientData.FormName + ".aspx.resx";
                ProjectItem P2 = FProject.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + FClientData.BaseFormName + ".aspx.vi-VN.resx");
                P2.Name = FClientData.FormName + ".aspx.vi-VN.resx";
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
#if VS90
            object oView = FDesignerDocument.webControls.item("View", 0);
            if (oView == null)
                oView = FDesignerDocument.webControls.item("Master", 0);

            WebDevPage.IHTMLElement eView = null;
            WebDevPage.IHTMLElement eWebView1 = null;

            if (oView == null || !(oView is WebDevPage.IHTMLElement))
                return;
            eView = (WebDevPage.IHTMLElement)oView;
            BlockItem.wDataSource = new WebDataSource();
            String viewDataMember = FClientData.ViewProviderName.Substring(FClientData.ViewProviderName.IndexOf('.') + 1, FClientData.ViewProviderName.Length -
                                                    FClientData.ViewProviderName.IndexOf('.') - 1);
            if (eView != null)
            {
                eView.setAttribute("DataMember", viewDataMember, 0);
            }

            object oWebView1 = FDesignerDocument.webControls.item("WgView", 0);
            if (oWebView1 != null)
            {
                eWebView1 = (WebDevPage.IHTMLElement)oWebView1;
                //eWebView1.setAttribute("DataMember", viewDataMember, 0);
            }

            if (oWebView1 == null)
                oWebView1 = FDesignerDocument.webControls.item("WebGridView1", 0);

            if (oWebView1 != null)
            {
                eWebView1 = (WebDevPage.IHTMLElement)oWebView1;

                //这里本来想再往下找Columns节点的,可是找不到,只能先这样写了
                StringBuilder sb = new StringBuilder(eWebView1.innerHTML);
                int idx = eWebView1.innerHTML.IndexOf("</Columns>");
                List<string> KeyFields = new List<string>();
                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    idx = sb.ToString().IndexOf("</Columns>");
                    sb.Insert(idx, "\r            <asp:BoundField DataField=\"" + BFI.DataField + "\" HeaderText=\"" + (string.IsNullOrEmpty(BFI.Description) ? BFI.DataField : BFI.Description) + "\" SortExpression=\"" + BFI.DataField + "\" />\r\n            ");
                }
                eWebView1.innerHTML = sb.ToString();
            }

            AjaxTools.ExtGridColumnCollection aExtGridColumnCollection = new AjaxTools.ExtGridColumnCollection(new AjaxTools.AjaxGridView(), typeof(AjaxTools.ExtColumnMatch));
            AjaxTools.ExtQueryFieldCollection aExtQueryFieldCollection = new ExtQueryFieldCollection(new AjaxTools.AjaxGridView(), typeof(AjaxTools.ExtQueryField));
            bool flag = true;
            DataTable srcTable = FWizardDataSet.RealDataSet.Tables[BlockItem.TableName];
            foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
            {
                AjaxTools.ExtGridColumn extCol = new AjaxTools.ExtGridColumn();
                if (BFI.CheckNull == "Y")
                    extCol.AllowNull = false;
                else
                    extCol.AllowNull = true;
                extCol.AllowSort = false;
                extCol.ColumnName = string.Format("col{0}", "AjaxGridView1" + BFI.DataField);
                extCol.DataField = BFI.DataField;
                extCol.ExpandColumn = true;
                if (BFI.Description != null && BFI.Description != String.Empty)
                    extCol.HeaderText = BFI.Description;
                else
                    extCol.HeaderText = BFI.DataField;
                extCol.IsKeyField = BFI.IsKey;
                extCol.IsKeyField = IsKeyField(BFI.DataField, srcTable.PrimaryKey);
                extCol.NewLine = flag;
                extCol.Resizable = true;
                extCol.TextAlign = "left";
                extCol.Visible = true;
                extCol.Width = 75;
                extCol.ReadOnly = true;
                if (BFI.QueryMode == "Normal")
                {
                    AjaxTools.ExtQueryField aExtQueryField = new AjaxTools.ExtQueryField();
                    aExtQueryField.Condition = "And";
                    aExtQueryField.Id = BFI.DataField;
                    aExtQueryField.DataField = BFI.DataField;
                    aExtQueryField.Caption = BFI.Description;
                    if (BFI.DataType == typeof(int) || BFI.DataType == typeof(float) || BFI.DataType == typeof(double))
                    {
                        aExtQueryField.Operator = "=";
                    }
                    else
                    {
                        aExtQueryField.Operator = "%";
                    }
                    aExtQueryFieldCollection.Add(aExtQueryField);
                }
                else if (BFI.QueryMode == "Range")
                {
                    AjaxTools.ExtQueryField aExtQueryField = new AjaxTools.ExtQueryField();
                    aExtQueryField.Id = BFI.DataField;
                    aExtQueryField.DataField = BFI.DataField;
                    aExtQueryField.Caption = BFI.Description;
                    aExtQueryField.Condition = "And";
                    aExtQueryField.Operator = ">=";
                    aExtQueryFieldCollection.Add(aExtQueryField);

                    AjaxTools.ExtQueryField aExtQueryField2 = new AjaxTools.ExtQueryField();
                    aExtQueryField2.Id = BFI.DataField + "2";
                    aExtQueryField2.DataField = BFI.DataField;
                    aExtQueryField2.Caption = BFI.Description;
                    aExtQueryField2.Condition = "And";
                    aExtQueryField2.Operator = "<=";
                    aExtQueryFieldCollection.Add(aExtQueryField2);
                }
                this.FieldTypeSelector(BFI.DataType, extCol, BFI.ControlType);

                aExtGridColumnCollection.Add(extCol);
                flag = !flag;
            }

            WebDevPage.IHTMLElement AjaxGridView1 = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("AjaxGridView1", 0);
            if (AjaxGridView1 != null)
            {
                SetCollectionValue(AjaxGridView1, typeof(AjaxTools.AjaxGridView).GetProperty("Columns"), aExtGridColumnCollection);
            }
            if (AjaxGridView1 != null && aExtQueryFieldCollection.Count > 0)
            {
                SetCollectionValue(AjaxGridView1, typeof(AjaxTools.AjaxGridView).GetProperty("QueryFields"), aExtQueryFieldCollection);
            }
#else

            WebDataSource View = (WebDataSource)FPage.FindControl("View");
            if (View == null)
                View = (WebDataSource)FPage.FindControl("Master");
            BlockItem.wDataSource = View;
            View.DataMember = FClientData.ViewProviderName;
            if (View.DataMember == null || View.DataMember == "")
                View.DataMember = FClientData.ProviderName;
            View.DataMember = View.DataMember.Substring(View.DataMember.IndexOf('.') + 1, View.DataMember.Length -
                View.DataMember.IndexOf('.') - 1);
            IComponentChangeService FComponentChangeService = (IComponentChangeService)FDesignerHost.RootComponent.Site.GetService(typeof(IComponentChangeService));
            FComponentChangeService.OnComponentChanged(View, null, "", "M");
            WebGridView WgView = (WebGridView)FPage.FindControl("WgView");
            if (WgView == null)
                WgView = (WebGridView)FPage.FindControl("WebGridView1");
            //???WebGridView2.Columns.Clear();
            if (WgView != null)
            {
                System.Web.UI.WebControls.BoundField Field = null;
                List<string> KeyFields = new List<string>();
                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    Field = new System.Web.UI.WebControls.BoundField();
                    Field.DataField = BFI.DataField;
                    Field.SortExpression = BFI.DataField;
                    Field.HeaderText = BFI.Description;
                    //Field.HeaderStyle.Width = BFI.Length * ColumnWidthPixel;
                    if (Field.HeaderText == "")
                        Field.HeaderText = BFI.DataField;
                    if (BFI.IsKey)
                        KeyFields.Add(Field.DataField);
                    WgView.Columns.Add(Field);
                }
                FComponentChangeService = (IComponentChangeService)FDesignerHost.RootComponent.Site.GetService(typeof(IComponentChangeService));
                FComponentChangeService.OnComponentChanged(WgView, null, "", "M");
            }

            //AjaxTools.AjaxGridView aAjaxGridView = FPage.FindControl("AjaxGridView1") as AjaxTools.AjaxGridView;
            Object aAjaxGridView = FPage.FindControl("AjaxGridView1");
            if (aAjaxGridView != null)
            {
                bool flag = true;
                DataTable srcTable = FWizardDataSet.RealDataSet.Tables[BlockItem.TableName];
                IList iColumns = aAjaxGridView.GetType().GetProperty("Columns").GetValue(aAjaxGridView, null) as IList;
                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    Type columnsType = aAjaxGridView.GetType().GetProperty("Columns").PropertyType.GetProperties()[0].PropertyType;
                    object extCol = Activator.CreateInstance(columnsType);
                    if (BFI.CheckNull == "Y")
                        extCol.GetType().GetProperty("AllowNull").SetValue(extCol, false, null);
                    else
                        extCol.GetType().GetProperty("AllowNull").SetValue(extCol, true, null);
                    extCol.GetType().GetProperty("AllowSort").SetValue(extCol, false, null);
                    extCol.GetType().GetProperty("ColumnName").SetValue(extCol, string.Format("col{0}", BFI.DataField), null);
                    extCol.GetType().GetProperty("DataField").SetValue(extCol, BFI.DataField, null);
                    extCol.GetType().GetProperty("DefaultValue").SetValue(extCol, BFI.DefaultValue, null);
                    extCol.GetType().GetProperty("ExpandColumn").SetValue(extCol, true, null);
                    if (BFI.Description != null && BFI.Description != String.Empty)
                        extCol.GetType().GetProperty("HeaderText").SetValue(extCol, BFI.Description, null);
                    else
                        extCol.GetType().GetProperty("HeaderText").SetValue(extCol, BFI.DataField, null);
                    //extCol.GetType().GetProperty("IsKeyField").SetValue(extCol, BFI.IsKey, null);
                    extCol.GetType().GetProperty("IsKeyField").SetValue(extCol, IsKeyField(BFI.DataField, srcTable.PrimaryKey), null);
                    extCol.GetType().GetProperty("NewLine").SetValue(extCol, flag, null);
                    extCol.GetType().GetProperty("Resizable").SetValue(extCol, true, null);
                    extCol.GetType().GetProperty("TextAlign").SetValue(extCol, "left", null);
                    extCol.GetType().GetProperty("Visible").SetValue(extCol, true, null);
                    extCol.GetType().GetProperty("Width").SetValue(extCol, 75, null);
                    if (BFI.QueryMode == "Normal")
                    {
                        IList iQueryFields = aAjaxGridView.GetType().GetProperty("QueryFields").GetValue(aAjaxGridView, null) as IList;
                        Type queryFieldsType = aAjaxGridView.GetType().GetProperty("QueryFields").PropertyType.GetProperties()[0].PropertyType;
                        object aQueryField = Activator.CreateInstance(queryFieldsType);
                        aQueryField.GetType().GetProperty("Condition").SetValue(aQueryField, "And", null);
                        aQueryField.GetType().GetProperty("DataField").SetValue(aQueryField, BFI.DataField, null);
                        aQueryField.GetType().GetProperty("Caption").SetValue(aQueryField, BFI.Description, null);
                        if (BFI.DataType == typeof(int) || BFI.DataType == typeof(float) || BFI.DataType == typeof(double))
                        {
                            aQueryField.GetType().GetProperty("Operator").SetValue(aQueryField, "=", null);
                        }
                        else
                        {
                            aQueryField.GetType().GetProperty("Operator").SetValue(aQueryField, "%", null);
                        }
                        iQueryFields.Add(aQueryField);
                    }
                    else if (BFI.QueryMode == "Range")
                    {
                        IList iQueryFields = aAjaxGridView.GetType().GetProperty("QueryFields").GetValue(aAjaxGridView, null) as IList;
                        Type queryFieldsType = aAjaxGridView.GetType().GetProperty("QueryFields").PropertyType.GetProperties()[0].PropertyType;
                        object aQueryField = Activator.CreateInstance(queryFieldsType);
                        aQueryField.GetType().GetProperty("Condition").SetValue(aQueryField, "And", null);
                        aQueryField.GetType().GetProperty("DataField").SetValue(aQueryField, BFI.DataField, null);
                        aQueryField.GetType().GetProperty("Caption").SetValue(aQueryField, BFI.Description, null);
                        aQueryField.GetType().GetProperty("Condition").SetValue(aQueryField, "And", null);
                        aQueryField.GetType().GetProperty("Operator").SetValue(aQueryField, ">=", null);
                        iQueryFields.Add(aQueryField);

                        object aQueryField2 = Activator.CreateInstance(queryFieldsType);
                        aQueryField2.GetType().GetProperty("Condition").SetValue(aQueryField2, "And", null);
                        aQueryField2.GetType().GetProperty("DataField").SetValue(aQueryField2, BFI.DataField, null);
                        aQueryField2.GetType().GetProperty("Caption").SetValue(aQueryField2, BFI.Description, null);
                        aQueryField2.GetType().GetProperty("Condition").SetValue(aQueryField2, "And", null);
                        aQueryField2.GetType().GetProperty("Operator").SetValue(aQueryField2, "<=", null);
                        iQueryFields.Add(aQueryField2);

                    }
                    this.FieldTypeSelector(BFI.DataType, extCol, BFI.ControlType);
                    iColumns.Add(extCol);
                    //AjaxTools.ExtGridColumn extCol = new AjaxTools.ExtGridColumn();
                    //extCol.AllowSort = false;
                    //extCol.ColumnName = string.Format("col{0}", BFI.DataField);
                    //extCol.DataField = BFI.DataField;
                    //extCol.ExpandColumn = true;
                    //extCol.HeaderText = BFI.Description;
                    //extCol.IsKeyField = BFI.IsKey;
                    //extCol.IsKeyField = IsKeyField(BFI.DataField, srcTable.PrimaryKey);
                    //extCol.NewLine = flag;
                    //extCol.Resizable = true;
                    //extCol.TextAlign = "left";
                    //extCol.Visible = true;
                    //extCol.Width = 75;
                    //this.FieldTypeSelector(BFI.DataType, extCol);

                    //aAjaxGridView.Columns.Add(extCol);
                    flag = !flag;
                }
                NotifyRefresh(200);
                FComponentChangeService.OnComponentChanged(aAjaxGridView, null, "", "M");
            }

            /*
            WebGridView2.DataKeyNames = new string[KeyFields.Count];
            for (int I = 0; I < KeyFields.Count; I++)
                WebGridView2.DataKeyNames[I] = KeyFields[I];
             * 
             */


            /*
            if (FClientData.IsMasterDetailBaseForm())
            {
                InfoDataSet ViewDataSet = FDesignerHost.CreateComponent(typeof(InfoDataSet), "idView") as InfoDataSet;
                ViewDataSet.RemoteName = FClientData.ViewProviderName;
                if (ViewDataSet.RemoteName.Trim() == "")
                {
                    ViewDataSet.RemoteName = FClientData.ProviderName;
                }
                ViewDataSet.Active = true;
                InfoBindingSource ViewBindingSource = FDesignerHost.CreateComponent(typeof(InfoBindingSource),
                    "ibsView") as InfoBindingSource;
                ViewBindingSource.DataSource = ViewDataSet;
                ViewBindingSource.DataMember = ViewDataSet.RealDataSet.Tables[0].TableName;
                BlockItem.BindingSource = ViewBindingSource;
                InfoNavigator navigator1 = FRootForm.Controls["infoNavigator1"] as InfoNavigator;
                if (navigator1 != null)
                    navigator1.ViewBindingSource = ViewBindingSource;
                InfoRelation Relation = new InfoRelation();
                Relation.RelationDataSet = FDataSet;
                InfoKeyField KeyField;
                KeyField = new InfoKeyField();
                KeyField.FieldName = ViewDataSet.RealDataSet.Tables[0].Columns[0].ColumnName;
                Relation.SourceKeyFields.Add(KeyField);
                KeyField = new InfoKeyField();
                KeyField.FieldName = ViewDataSet.RealDataSet.Tables[0].Columns[0].ColumnName;
                Relation.TargetKeyFields.Add(KeyField);
                Relation.Active = true;
                ViewBindingSource.Relations.Add(Relation);
            }

            TBlockFieldItem FieldItem;
            System.Windows.Forms.SplitContainer scMaster = FRootForm.Controls["scMaster"] as System.Windows.Forms.SplitContainer;
            FViewGrid = FDesignerHost.CreateComponent(typeof(InfoDataGridView), "grdView") as InfoDataGridView;
            FViewGrid.Parent = scMaster.Panel1;
            FViewGrid.Dock = DockStyle.Fill;
            FViewGrid.TabIndex = 0;
            FViewGrid.DataSource = BlockItem.BindingSource;

            if (FClientData.IsMasterDetailBaseForm())
            {
                //???FViewGrid.Columns.Clear();
                DataGridViewColumn Column;
                int I, Index;
                for (I = 0; I < BlockItem.BlockFieldItems.Count; I++)
                {
                    FieldItem = BlockItem.BlockFieldItems[I] as TBlockFieldItem;
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
             */
#endif
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
            if (FClientData.BaseFormName == "WMasterDetail3" || FClientData.BaseFormName == "VBWebCMasterDetail_VFG" || FClientData.BaseFormName == "ExtMasterDetail"
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
            String Name = WzdUtils.RemoveSpecialCharacters("wds" + TableName + FieldItem.DataField + ExtraName);

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

            if (Kind == "RefVal")
            {
                WebDataSource aWebDataSource = new WebDataSource();
                InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
                aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                //aInfoCommand.Connection = FClientData.Owner.GlobalConnection;
                IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                if (FSYS_REFVAL != null)
                    FSYS_REFVAL.Dispose();
                FSYS_REFVAL = new DataSet();
                aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", FieldItem.RefValNo);
                WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, FSYS_REFVAL, FieldItem.RefValNo);
                aWebDataSource.SelectAlias = FSYS_REFVAL.Tables[0].Rows[0]["SELECT_ALIAS"].ToString();
                aWebDataSource.SelectCommand = FSYS_REFVAL.Tables[0].Rows[0]["SELECT_COMMAND"].ToString();

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
                    cmd = String.Format("Select * from [{0}]", FieldItem.ComboEntityName);
                else if (type == "2")
                    cmd = String.Format("Select * from [{0}]", FieldItem.ComboEntityName);
                else if (type == "3")
                    cmd = String.Format("Select * from {0}", FieldItem.ComboEntityName);
                else if (type == "4")
                    cmd = String.Format("Select * from {0}", FieldItem.ComboEntityName);
                else if (type == "5")
                    cmd = String.Format("Select * from {0}", FieldItem.ComboEntityName);
                else if (type == "6")
                    cmd = String.Format("Select * from {0}", FieldItem.ComboEntityName);
                else if (type == "7")
                    cmd = String.Format("Select * from {0}", FieldItem.ComboEntityName);

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
                InsertControl(Page, aExtComboBox);
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

        private void GenDefault(TBlockFieldItem aFieldItem, WebDefault aDefault, WebValidate aValidate)
        {
            if (aFieldItem.DefaultValue != "" && aFieldItem.DefaultValue != null)
            {
                DefaultFieldItem aDefaultItem = new DefaultFieldItem();
                aDefaultItem.FieldName = aFieldItem.DataField;
                aDefaultItem.DefaultValue = aFieldItem.DefaultValue;
                aDefault.Fields.Add(aDefaultItem);
            }
            if (aFieldItem.CheckNull != null && aFieldItem.CheckNull.ToUpper() == "Y")
            {
                ValidateFieldItem aValidateItem = new ValidateFieldItem();
                aValidateItem.FieldName = aFieldItem.DataField;
                aValidateItem.CheckNull = aFieldItem.CheckNull.ToUpper() == "Y";
                aValidateItem.ValidateLabelLink = "Caption" + aFieldItem.DataField;

                aValidate.Fields.Add(aValidateItem);
            }
        }

        private void CreateQueryField(TBlockFieldItem aFieldItem, String Range, InfoComboBox aComboBox, String TableName)
        {
            if (aFieldItem.QueryMode == null)
                return;
            WebNavigator navigator2 = FPage.FindControl("WebNavigator1") as WebNavigator;
            if (navigator2 != null)
            {
                if (aFieldItem.QueryMode.ToUpper() == "NORMAL" || aFieldItem.QueryMode.ToUpper() == "RANGE")
                {
                    WebQueryField qField = new WebQueryField();
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
                        if (Range == "")
                        {
                            qField.Condition = "<=";
                            CreateQueryField(aFieldItem, ">=", aComboBox, TableName);
                        }
                        else
                        {
                            qField.Condition = Range;
                        }
                        navigator2.QueryMode = WebNavigator.QueryModeType.ClientQuery;
                    }
                    switch (aFieldItem.ControlType.ToUpper())
                    {
                        case "TEXTBOX":
                            qField.Mode = "TextBox";
                            break;
                        case "COMBOBOX":
                            qField.Mode = "ComboBox";
                            qField.RefVal = "wrv" + TableName + aFieldItem.DataField + "QF";
                            break;
                        case "REFVALBOX":
                            qField.Mode = "RefVal";
                            qField.RefVal = "wrv" + TableName + aFieldItem.DataField + "QF";
                            break;
                        case "DATETIMEBOX":
                            qField.Mode = "Calendar";
                            break;
                    }
                    navigator2.QueryFields.Add(qField);
                }
                IComponentChangeService FComponentChangeService = (IComponentChangeService)FDesignerHost.RootComponent.Site.GetService(typeof(IComponentChangeService));
                NotifyRefresh(200);
                FComponentChangeService.OnComponentChanged(navigator2, null, "", "M");
            }

            WebClientQuery WebClientQuery1 = (WebClientQuery)FPage.FindControl("WebClientQuery1");
            if (WebClientQuery1 != null)
            {
                if (aFieldItem.QueryMode.ToUpper() == "NORMAL" || aFieldItem.QueryMode.ToUpper() == "RANGE")
                {
                    WebQueryColumns qColumns = new WebQueryColumns();
                    qColumns.Column = aFieldItem.DataField;
                    qColumns.Caption = aFieldItem.Description;
                    if (qColumns.Caption == "")
                        qColumns.Caption = aFieldItem.DataField;
                    qColumns.Condition = "And";
                    if (aFieldItem.QueryMode.ToUpper() == "NORMAL")
                    {
                        if (aFieldItem.DataType == typeof(DateTime))
                            qColumns.Operator = "=";
                        if (aFieldItem.DataType == typeof(int) || aFieldItem.DataType == typeof(float) ||
                            aFieldItem.DataType == typeof(double) || aFieldItem.DataType == typeof(Int16))
                            qColumns.Operator = "=";
                        if (aFieldItem.DataType == typeof(String))
                            qColumns.Operator = "%";
                    }
                    if (aFieldItem.QueryMode.ToUpper() == "RANGE")
                    {
                        qColumns.Condition = "And";
                        if (Range == "")
                        {
                            qColumns.Operator = "<=";
                            CreateQueryField(aFieldItem, ">=", aComboBox, TableName);
                        }
                        else
                        {
                            qColumns.Operator = Range;
                        }
                    }
                    switch (aFieldItem.ControlType.ToUpper())
                    {
                        case "TEXTBOX":
                            qColumns.ColumnType = "ClientQueryTextBoxColumn";
                            break;
                        case "COMBOBOX":
                            qColumns.ColumnType = "ClientQueryComboBoxColumn";
                            qColumns.WebRefVal = "wrv" + TableName + aFieldItem.DataField + "QF";
                            break;
                        case "REFVALBOX":
                            qColumns.ColumnType = "ClientQueryRefValColumn";
                            qColumns.WebRefVal = "wrv" + TableName + aFieldItem.DataField + "QF";

                            WebDataSource aWebDataSource = new WebDataSource();
                            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
                            aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                            //aInfoCommand.Connection = FClientData.Owner.GlobalConnection;
                            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                            if (FSYS_REFVAL != null)
                                FSYS_REFVAL.Dispose();
                            FSYS_REFVAL = new DataSet();
                            aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", aFieldItem.RefValNo);
                            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, FSYS_REFVAL, aFieldItem.RefValNo);

                            WebRefVal aWebRefVal = new WebRefVal();
                            aWebRefVal.ID = qColumns.WebRefVal;
                            aWebRefVal.DataTextField = FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                            aWebRefVal.DataValueField = FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
                            aWebRefVal.DataSourceID = String.Format("wds{0}{1}", TableName, aFieldItem.DataField);
                            aWebRefVal.Visible = false;
                            FWebRefValListPage.Add(aWebRefVal);
                            break;
                        case "DATETIMEBOX":
                            qColumns.ColumnType = "ClientQueryCalendarColumn";
                            break;
                        case "CHECKBOX":
                            qColumns.ColumnType = "ClientQueryCheckBoxColumn";
                            break;
                    }
                    WebClientQuery1.Columns.Add(qColumns);
                }
                IComponentChangeService FComponentChangeService = (IComponentChangeService)FDesignerHost.RootComponent.Site.GetService(typeof(IComponentChangeService));
                NotifyRefresh(200);
                FComponentChangeService.OnComponentChanged(WebClientQuery1, null, "", "M");
            }
        }

#if VS90
        private string GenTemplateFieldHTML(string controlType, TBlockItem BlockItem, TBlockFieldItem BFI)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<asp:TemplateField HeaderText=\"" + (string.IsNullOrEmpty(BFI.Description) ? BFI.DataField : BFI.Description) + "\" SortExpression=\"" + BFI.DataField + "\">");
            builder.AppendLine("<EditItemTemplate>");
            builder.AppendLine(GenTempateHTML("edit", controlType, BlockItem, BFI));
            builder.AppendLine("</EditItemTemplate>");
            builder.AppendLine("<FooterTemplate>");
            builder.AppendLine(GenTempateHTML("footer", controlType, BlockItem, BFI));
            builder.AppendLine("</FooterTemplate>");
            builder.AppendLine("<ItemTemplate>");
            builder.AppendLine(GenTempateHTML("item", controlType, BlockItem, BFI));
            builder.AppendLine("</ItemTemplate>");
            builder.AppendLine("</asp:TemplateField>");

            return builder.ToString();
        }

        private string GenDetailViewTemplateFieldHTML(string controlType, TBlockItem BlockItem, TBlockFieldItem BFI)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<asp:TemplateField HeaderText=\"" + (string.IsNullOrEmpty(BFI.Description) ? BFI.DataField : BFI.Description) + "\" SortExpression=\"" + BFI.DataField + "\">");
            builder.AppendLine("<EditItemTemplate>");
            builder.AppendLine(GenTempateHTML("edit", controlType, BlockItem, BFI));
            builder.AppendLine("</EditItemTemplate>");
            builder.AppendLine("<InsertItemTemplate>");
            builder.AppendLine(GenTempateHTML("footer", controlType, BlockItem, BFI));
            builder.AppendLine("</InsertItemTemplate>");
            builder.AppendLine("<ItemTemplate>");
            builder.AppendLine(GenTempateHTML("item", controlType, BlockItem, BFI));
            builder.AppendLine("</ItemTemplate>");
            builder.AppendLine("</asp:TemplateField>");

            return builder.ToString();
        }

        private string GenTempateHTML(string template, string controlType, TBlockItem BlockItem, TBlockFieldItem BFI)
        {
            bool isAjaxPage = false;
            object obj = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("AjaxScriptManager1", 0);
            if (obj != null)
                isAjaxPage = true;

            StringBuilder builder = new StringBuilder();
            String FormatStyle = this.FormatEditMask(BFI.EditMask);
            if (template == "edit")
            {
                switch (controlType)
                {
                    case "RefValBox":
                        IDbConnection conn = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, false);
                        InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
                        aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                        //aInfoCommand.Connection = conn;
                        String OWNER = String.Empty, SS = this.FClientData.RealTableName, TableName = String.Empty;
                        if (SS.Contains("."))
                        {
                            OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                            TableName = SS;
                        }
                        aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME='" + TableName + "' OR TABLE_NAME='" + OWNER + "." + TableName + "'";
                        IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                        DataSet dsColdef = new DataSet();
                        WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsColdef, this.FClientData.TableName);

                        DataSet aDataSet = new DataSet();
                        StringBuilder RefColumns = new StringBuilder("<Columns>");
                        aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL_D1 where REFVAL_NO = '{0}'", BFI.RefValNo);
                        WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, BFI.RefValNo);
                        if (aDataSet != null && aDataSet.Tables.Count > 0 && aDataSet.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow DR in aDataSet.Tables[0].Rows)
                            {
                                RefColumns.Append(Environment.NewLine);
                                RefColumns.Append("<InfoLight:WebRefColumn ColumnName=\"" + DR["FIELD_NAME"].ToString() + "\" HeadText=\"" + DR["HEADER_TEXT"].ToString() + "\" Width=\"100\" />");
                            }
                            RefColumns.Append(Environment.NewLine);
                            RefColumns.Append("</columns>");
                        }
                        else
                        {
                            RefColumns = new StringBuilder("");
                        }

                        String DataSourceID = GenWebDataSource(BFI, WzdUtils.RemoveSpecialCharacters(BlockItem.TableName), "RefVal", "");
                        String refvalHTML = String.Empty;
                        if (isAjaxPage)
                        {
                            refvalHTML = String.Format("<AjaxTools:AjaxRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"[{1}]\"{5}) %>' " +
                                        "DataSourceID=\"{2}\" " +
                                        "DataTextField=\"{3}\" DataValueField=\"{4}\" ResxDataSet=\"\">" +
                                         RefColumns.ToString() +
                                        "</AjaxTools:AjaxRefVal>",
                                        WzdUtils.RemoveSpecialCharacters("arv" + BlockItem.TableName + BFI.DataField + "E"),
                                        BFI.DataField,
                                        DataSourceID,
                                        FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                        FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                        FormatStyle
                                        );
                        }
                        else
                        {
                            refvalHTML = String.Format("<InfoLight:WebRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"[{1}]\"{5}) %>' " +
                                        "ButtonImageUrl=\"../Image/refval/RefVal.gif\" DataBindingField=\"{1}\" DataSourceID=\"{2}\" " +
                                        "DataTextField=\"{3}\" DataValueField=\"{4}\" ReadOnly=\"False\" ResxDataSet=\"\" " +
                                        "ResxFilePath=\"\" UseButtonImage=\"True\"> " +
                                         RefColumns.ToString() +
                                        "</InfoLight:WebRefVal>",
                                        WzdUtils.RemoveSpecialCharacters("wrv" + BlockItem.TableName + BFI.DataField + "E"),
                                        BFI.DataField,
                                        DataSourceID,
                                        FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                        FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                        FormatStyle
                                        );
                        }
                        builder.AppendLine(refvalHTML);
                        break;
                    case "ComboBox":
                        String comboHTML = string.Format("<InfoLight:WebDropDownList runat=\"server\" ID=\"{0}\" DataSourceID=\"{1}\" DataTextField=\"{2}\" DataValueField=\"{3}\" DataMember=\"{4}\" SelectedValue='<%# Bind(\"[{5}]\"{6}) %>'></InfoLight:WebDropDownList>",
                            WzdUtils.RemoveSpecialCharacters("wdd" + BlockItem.TableName + BFI.DataField + "E"),
                            GenWebDataSource(BFI, BFI.ComboEntityName, "ComboBox", ""),
                            BFI.ComboTextField,
                            BFI.ComboValueField,
                            BFI.ComboEntityName,
                            BFI.DataField,
                            FormatStyle);
                        builder.AppendLine(comboHTML);
                        break;
                    case "ValidateBox":
                        String validateHTML = String.Format("<InfoLight:WebValidateBox ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"[{1}]\"{3}) %>' ValidateField=\"{1}\" WebValidateID=\"{2}\"></InfoLight:WebValidateBox>",
                                    WzdUtils.RemoveSpecialCharacters("wvb" + BlockItem.TableName + BFI.DataField + "E"),
                                    BFI.DataField,
                                    "wv" + WzdUtils.RemoveSpecialCharacters(BlockItem.TableName),
                                    FormatStyle);
                        builder.AppendLine(validateHTML);
                        break;
                    case "CheckBox":
                        String checkHTML = String.Format("<asp:CheckBox ID=\"{0}\" runat=\"server\" Checked='<%# Bind(\"[{1}]\"{2}) %>'></asp:CheckBox>",
                                    WzdUtils.RemoveSpecialCharacters("cb" + BlockItem.TableName + BFI.DataField + "E"),
                                    BFI.DataField,
                                    FormatStyle);
                        builder.AppendLine(checkHTML);
                        break;
                    case "DateTimeBox":
                        String dtHTML = String.Empty;
                        if (isAjaxPage)
                        {
                            dtHTML = String.Format("<AjaxTools:AjaxDateTimePicker runat=\"server\" DateFormat=\"{0}\" DateTimeType=\"{1}\" Localize=\"False\" MinYear=\"1950\" MaxYear=\"2050\" ToolTip=\"{2}\" Width=\"100px\" ID=\"{3}\" {4}='<%# Bind(\"[{5}]\"{6}) %>'></AjaxTools:AjaxDateTimePicker>",
                                "None",
                                (BFI.DataType == typeof(DateTime)) ? "DateTime" : "Varchar",
                                BFI.DataField,
                                WzdUtils.RemoveSpecialCharacters("wdt" + BlockItem.TableName + BFI.DataField + "E"),
                                (BFI.DataType == typeof(DateTime)) ? "Text" : "DateString",
                                BFI.DataField,
                                FormatStyle);
                        }
                        else
                        {
                            dtHTML = String.Format("<InfoLight:WebDateTimePicker runat=\"server\" UseButtonImage=\"True\" DateFormat=\"{0}\" DateTimeType=\"{1}\" Localize=\"False\" MinYear=\"1950\" MaxYear=\"2050\" ToolTip=\"{2}\" Width=\"100px\" ID=\"{3}\" {4}='<%# Bind(\"[{5}]\"{6}) %>'></InfoLight:WebDateTimePicker>",
                                "None",
                                (BFI.DataType == typeof(DateTime)) ? "DateTime" : "Varchar",
                                BFI.DataField,
                                WzdUtils.RemoveSpecialCharacters("wdt" + BlockItem.TableName + BFI.DataField + "E"),
                                (BFI.DataType == typeof(DateTime)) ? "Text" : "DateString",
                                BFI.DataField,
                                FormatStyle);
                        }
                        builder.AppendLine(dtHTML);
                        break;
                }
            }
            else if (template == "footer")
            {
                switch (controlType)
                {
                    case "RefValBox":
                        IDbConnection conn = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, false);
                        InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
                        aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                        //aInfoCommand.Connection = conn;
                        String OWNER = String.Empty, SS = this.FClientData.RealTableName, TableName = String.Empty;
                        if (SS.Contains("."))
                        {
                            OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                            TableName = SS;
                        }
                        aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME='" + TableName + "' OR TABLE_NAME='" + OWNER + "." + TableName + "'";
                        IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                        DataSet dsColdef = new DataSet();
                        WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsColdef, this.FClientData.TableName);

                        DataSet aDataSet = new DataSet();
                        StringBuilder RefColumns = new StringBuilder("<Columns>");
                        aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL_D1 where REFVAL_NO = '{0}'", BFI.RefValNo);
                        WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, BFI.RefValNo);
                        if (aDataSet != null && aDataSet.Tables.Count > 0 && aDataSet.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow DR in aDataSet.Tables[0].Rows)
                            {
                                RefColumns.Append(Environment.NewLine);
                                RefColumns.Append("<InfoLight:WebRefColumn ColumnName=\"" + DR["FIELD_NAME"].ToString() + "\" HeadText=\"" + DR["HEADER_TEXT"].ToString() + "\" Width=\"100\" />");
                            }
                            RefColumns.Append(Environment.NewLine);
                            RefColumns.Append("</columns>");
                        }
                        else
                        {
                            RefColumns = new StringBuilder("");
                        }

                        String DataSourceID = GenWebDataSource(BFI, WzdUtils.RemoveSpecialCharacters(BlockItem.TableName), "RefVal", "");
                        String refvalHTML = String.Empty;
                        if (isAjaxPage)
                        {
                            refvalHTML = String.Format("<AjaxTools:AjaxRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"[{1}]\"{5}) %>' " +
                                        "DataSourceID=\"{2}\" " +
                                        "DataTextField=\"{3}\" DataValueField=\"{4}\" ResxDataSet=\"\">" +
                                         RefColumns.ToString() +
                                        "</AjaxTools:AjaxRefVal>",
                                        WzdUtils.RemoveSpecialCharacters("arv" + BlockItem.TableName + BFI.DataField + "F"),
                                        BFI.DataField,
                                        DataSourceID,
                                        FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                        FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                        FormatStyle
                                        );
                        }
                        else
                        {
                            refvalHTML = String.Format("<InfoLight:WebRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"[{1}]\"{5}) %>' " +
                                        "ButtonImageUrl=\"../Image/refval/RefVal.gif\" DataBindingField=\"{1}\" DataSourceID=\"{2}\" " +
                                        "DataTextField=\"{3}\" DataValueField=\"{4}\" ReadOnly=\"False\" ResxDataSet=\"\" " +
                                        "ResxFilePath=\"\" UseButtonImage=\"True\"> " +
                                         RefColumns.ToString() +
                                        "</InfoLight:WebRefVal>",
                                        WzdUtils.RemoveSpecialCharacters("wrv" + BlockItem.TableName + BFI.DataField + "F"),
                                        BFI.DataField,
                                        DataSourceID,
                                        FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                        FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                        FormatStyle
                                        );
                        }
                        builder.AppendLine(refvalHTML);
                        break;
                    case "ComboBox":
                        string comboHTML = string.Format("<InfoLight:WebDropDownList runat=\"server\" ID=\"{0}\" DataSourceID=\"{1}\" DataTextField=\"{2}\" DataValueField=\"{3}\" DataMember=\"{4}\"></InfoLight:WebDropDownList>",
                            WzdUtils.RemoveSpecialCharacters("wdd" + BlockItem.TableName + BFI.DataField + "F"),
                            GenWebDataSource(BFI, BFI.ComboEntityName, "ComboBox", ""),
                            BFI.ComboTextField,
                            BFI.ComboValueField,
                            BFI.ComboEntityName);
                        builder.AppendLine(comboHTML);
                        break;
                    case "ValidateBox":
                        String validateHTML = String.Format("<InfoLight:WebValidateBox ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"[{1}]\"{3}) %>' ValidateField=\"{1}\" WebValidateID=\"{2}\"></InfoLight:WebValidateBox>",
                                    WzdUtils.RemoveSpecialCharacters("wvb" + BlockItem.TableName + BFI.DataField + "F"),
                                    BFI.DataField,
                                    "wv" + WzdUtils.RemoveSpecialCharacters(BlockItem.TableName),
                                    FormatStyle);
                        builder.AppendLine(validateHTML);
                        break;
                    case "CheckBox":
                        String checkHTML = String.Format("<asp:CheckBox ID=\"{0}\" runat=\"server\" Checked='<%# Bind(\"[{1}]\"{2}) %>'></asp:CheckBox>",
                                    WzdUtils.RemoveSpecialCharacters("cb" + BlockItem.TableName + BFI.DataField + "F"),
                                    BFI.DataField,
                                    FormatStyle);
                        builder.AppendLine(checkHTML);
                        break;
                    case "DateTimeBox":
                        String dtHTML = String.Empty;
                        if (isAjaxPage)
                        {
                            dtHTML = String.Format("<AjaxTools:AjaxDateTimePicker runat=\"server\" DateFormat=\"{0}\" DateTimeType=\"{1}\" Localize=\"False\" MinYear=\"1950\" MaxYear=\"2050\" ToolTip=\"{2}\" Width=\"100px\" ID=\"{3}\" {4}='<%# Bind(\"[{5}]\"{6}) %>'></AjaxTools:AjaxDateTimePicker>",
                                "None",
                                (BFI.DataType == typeof(DateTime)) ? "DateTime" : "Varchar",
                                BFI.DataField,
                                WzdUtils.RemoveSpecialCharacters("wdt" + BlockItem.TableName + BFI.DataField + "F"),
                                (BFI.DataType == typeof(DateTime)) ? "Text" : "DateString",
                                BFI.DataField,
                                FormatStyle);
                        }
                        else
                        {
                            dtHTML = string.Format("<InfoLight:WebDateTimePicker runat=\"server\" UseButtonImage=\"True\" DateFormat=\"{0}\" DateTimeType=\"{1}\" Localize=\"False\" MinYear=\"1950\" MaxYear=\"2050\" ToolTip=\"{2}\" Width=\"100px\" ID=\"{3}\"></InfoLight:WebDateTimePicker>",
                                "None",
                                (BFI.DataType == typeof(DateTime)) ? "DateTime" : "Varchar",
                                BFI.DataField,
                                WzdUtils.RemoveSpecialCharacters("wdt" + BlockItem.TableName + BFI.DataField + "F"));
                        }
                        builder.AppendLine(dtHTML);
                        break;
                }
            }
            else if (template == "item")
            {
                switch (controlType)
                {
                    case "RefValBox":
                        IDbConnection conn = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, false);
                        InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
                        aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                        //aInfoCommand.Connection = conn;
                        String OWNER = String.Empty, SS = this.FClientData.RealTableName, TableName = String.Empty;
                        if (SS.Contains("."))
                        {
                            OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                            TableName = SS;
                        }
                        aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME='" + TableName + "' OR TABLE_NAME='" + OWNER + "." + TableName + "'";
                        IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                        DataSet dsColdef = new DataSet();
                        WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsColdef, this.FClientData.TableName);

                        DataSet aDataSet = new DataSet();
                        StringBuilder RefColumns = new StringBuilder("<Columns>");
                        aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL_D1 where REFVAL_NO = '{0}'", BFI.RefValNo);
                        WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, BFI.RefValNo);
                        if (aDataSet != null && aDataSet.Tables.Count > 0 && aDataSet.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow DR in aDataSet.Tables[0].Rows)
                            {
                                RefColumns.Append(Environment.NewLine);
                                RefColumns.Append("<InfoLight:WebRefColumn ColumnName=\"" + DR["FIELD_NAME"].ToString() + "\" HeadText=\"" + DR["HEADER_TEXT"].ToString() + "\" Width=\"100\" />");
                            }
                            RefColumns.Append(Environment.NewLine);
                            RefColumns.Append("</columns>");
                        }
                        else
                        {
                            RefColumns = new StringBuilder("");
                        }

                        String DataSourceID = GenWebDataSource(BFI, WzdUtils.RemoveSpecialCharacters(BlockItem.TableName), "RefVal", "");
                        String refvalHTML = String.Format("<InfoLight:WebRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"[{1}]\"{5}) %>' " +
                                    "ButtonImageUrl=\"../Image/refval/RefVal.gif\" DataBindingField=\"{1}\" DataSourceID=\"{2}\" " +
                                    "DataTextField=\"{3}\" DataValueField=\"{4}\" ReadOnly=\"True\" BorderStyle=\"None\" ResxDataSet=\"\" " +
                                    "ResxFilePath=\"\" UseButtonImage=\"True\" Width=\"100px\" BackColor=\"Transparent\"> " +
                                     RefColumns.ToString() +
                                    "</InfoLight:WebRefVal>",
                                    WzdUtils.RemoveSpecialCharacters("wrv" + BlockItem.TableName + BFI.DataField + "E"),
                                    BFI.DataField,
                                    DataSourceID,
                                    FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                    FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                    FormatStyle
                                    );
                        builder.AppendLine(refvalHTML);
                        break;
                    default:
                        string labelHTML = string.Format("<asp:Label runat=\"server\" ToolTip=\"{0}\" ID=\"{1}\" Text='<%# Bind(\"[{2}]\"{3}) %>'></asp:Label>",
                            BFI.DataField,
                            WzdUtils.RemoveSpecialCharacters("l" + BlockItem.TableName + BFI.DataField),
                            BFI.DataField,
                            FormatStyle);
                        builder.AppendLine(labelHTML);
                        break;
                }
            }
            return builder.ToString();
        }

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
            object oMaster = FDesignerDocument.webControls.item("Master", 0);

            WebDevPage.IHTMLElement eMaster = null;
            WebDevPage.IHTMLElement eWebGridView1 = null;

            if (oMaster == null || !(oMaster is WebDevPage.IHTMLElement))
                return;
            eMaster = (WebDevPage.IHTMLElement)oMaster;

            WebQueryFiledsCollection QueryFields = new WebQueryFiledsCollection(null, typeof(QueryField));
            WebQueryColumnsCollection QueryColumns = new WebQueryColumnsCollection(null, typeof(QueryColumns));
            foreach (TBlockFieldItem fielditem in BlockItem.BlockFieldItems)
            {
                GenQuery(fielditem, QueryFields, QueryColumns, BlockItem.TableName);
            }

            WebDevPage.IHTMLElement Page = FDesignerDocument.pageContentElement;

            foreach (TBlockFieldItem fielditem in BlockItem.BlockFieldItems)
            {
                foreach (WebQueryColumns wqc in QueryColumns)
                {
                    if (wqc.ColumnType == "ClientQueryRefValColumn" && wqc.Column == fielditem.DataField && fielditem.RefValNo != String.Empty)
                    {
                        WebDataSource aWebDataSource = new WebDataSource();
                        InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
                        aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                        //aInfoCommand.Connection = FClientData.Owner.GlobalConnection;
                        IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                        if (FSYS_REFVAL != null)
                            FSYS_REFVAL.Dispose();
                        FSYS_REFVAL = new DataSet();
                        aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", fielditem.RefValNo);
                        WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, FSYS_REFVAL, fielditem.RefValNo);

                        WebRefVal aWebRefVal = new WebRefVal();
                        aWebRefVal.ID = wqc.WebRefVal;
                        aWebRefVal.DataTextField = FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                        aWebRefVal.DataValueField = FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
                        aWebRefVal.DataSourceID = String.Format("wds{0}{1}", WzdUtils.RemoveSpecialCharacters(BlockItem.TableName), wqc.Column);
                        aWebRefVal.Visible = false;
                        InsertControl(Page, aWebRefVal);
                        break;
                    }
                    else if (wqc.ColumnType == "ClientQueryComboBoxColumn" && wqc.Column == fielditem.DataField
                        && !String.IsNullOrEmpty(fielditem.ComboTextField) && !String.IsNullOrEmpty(fielditem.ComboValueField))
                    {
                        WebRefVal aWebRefVal = new WebRefVal();
                        aWebRefVal.ID = wqc.WebRefVal;
                        aWebRefVal.DataTextField = fielditem.ComboTextField;
                        aWebRefVal.DataValueField = fielditem.ComboValueField;
                        aWebRefVal.DataSourceID = String.Format("wds{0}{1}", fielditem.ComboEntityName, wqc.Column);
                        aWebRefVal.Visible = false;
                        InsertControl(Page, aWebRefVal);
                        break;
                    }
                }
            }

            WebDevPage.IHTMLElement Navigator = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("WebNavigator1", 0);
            if (Navigator != null)
            {
                SetCollectionValue(Navigator, typeof(WebNavigator).GetProperty("QueryFields"), QueryFields);
            }
            WebDevPage.IHTMLElement ClientQuery = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("WebClientQuery1", 0);
            if (ClientQuery != null)
            {
                SetCollectionValue(ClientQuery, typeof(WebClientQuery).GetProperty("Columns"), QueryColumns);
            }

            WebDevPage.IHTMLElement AjaxGridView1 = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("AjaxGridView1", 0);
            if (AjaxGridView1 != null)
            {
                AjaxTools.ExtGridColumnCollection aExtGridColumnCollection = new AjaxTools.ExtGridColumnCollection(new AjaxTools.AjaxGridView(), typeof(AjaxTools.ExtColumnMatch));
                DataTable srcTable = FWizardDataSet.RealDataSet.Tables[BlockItem.TableName];
                bool flag = true;
                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    AjaxTools.ExtGridColumn extCol = new AjaxTools.ExtGridColumn();
                    if (BFI.CheckNull == "Y")
                        extCol.AllowNull = false;
                    else
                        extCol.AllowNull = true;
                    extCol.AllowSort = false;
                    extCol.ColumnName = string.Format("col{0}", BFI.DataField);
                    extCol.DataField = BFI.DataField;
                    extCol.ExpandColumn = true;
                    if (BFI.Description != null && BFI.Description != String.Empty)
                        extCol.HeaderText = BFI.Description;
                    else
                        extCol.HeaderText = BFI.DataField;
                    extCol.IsKeyField = BFI.IsKey;
                    extCol.IsKeyField = IsKeyField(BFI.DataField, srcTable.PrimaryKey);
                    extCol.NewLine = flag;
                    extCol.Resizable = true;
                    extCol.TextAlign = "left";
                    extCol.Visible = true;
                    extCol.Width = 75;
                    if ((BFI.RefValNo != null && BFI.RefValNo != "") || BFI.RefField != null)
                    {
                        String DataSourceID = GenWebDataSource(BFI, WzdUtils.RemoveSpecialCharacters(BlockItem.TableName), "RefVal", "", true);
                        String extComboBox = GenExtComboBox(BFI, WzdUtils.RemoveSpecialCharacters(BlockItem.TableName), "ExtRefVal", "", DataSourceID);
                        try
                        {
                            String str = AjaxGridView1.innerHTML;
                        }
                        catch
                        {
                            AjaxGridView1 = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("AjaxGridViewDetail", 0);
                        }
                        extCol.EditControlId = extComboBox;
                        extCol.Editor = AjaxTools.ExtGridEditor.ComboBox;
                    }
                    else if (BFI.ControlType == "ComboBox")
                    {
                        String DataSourceID = GenWebDataSource(BFI, WzdUtils.RemoveSpecialCharacters(BFI.ComboEntityName), "ComboBox", "", true);
                        String extComboBox = GenExtComboBox(BFI, WzdUtils.RemoveSpecialCharacters(BlockItem.TableName), "ExtComboBox", "", DataSourceID);
                        try
                        {
                            String str = AjaxGridView1.innerHTML;
                        }
                        catch
                        {
                            AjaxGridView1 = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("AjaxGridViewDetail", 0);
                        }
                        extCol.EditControlId = extComboBox;
                        extCol.Editor = AjaxTools.ExtGridEditor.ComboBox;
                    }
                    this.FieldTypeSelector(BFI.DataType, extCol, BFI.ControlType);

                    aExtGridColumnCollection.Add(extCol);

                    flag = !flag;
                }

                SetCollectionValue(AjaxGridView1, typeof(AjaxTools.AjaxGridView).GetProperty("Columns"), aExtGridColumnCollection);
            }
#else
            bool isAjaxPage = false;
            if (FPage.FindControl("AjaxScriptManager1") != null)
                isAjaxPage = true;

            WebDataSource Master = (WebDataSource)FPage.FindControl("Master");
            BlockItem.wDataSource = Master;
            WebGridView WebGridView1 = (WebGridView)FPage.FindControl("wgvMaster");
            if (WebGridView1 == null)
                WebGridView1 = (WebGridView)FPage.FindControl("WebGridView1");
            //Generate RESX
            //???GenResx(Master);

            //WebGridView1.DataMember = FClientData.TableName;
            //???WebGridView1.Columns.Clear();
            System.Web.UI.WebControls.BoundField aBoundField = null;
            System.Web.UI.WebControls.TemplateField aTemplateField = null;

            WebDefault aDefault = new WebDefault();
            aDefault.ID = "wd" + BlockItem.TableName;
            aDefault.DataSourceID = Master.ID;
            aDefault.DataMember = Master.DataMember;
            WebValidate aValidate = new WebValidate();
            aValidate.ID = "wv" + BlockItem.TableName;
            aValidate.DataSourceID = Master.ID;
            aValidate.DataMember = Master.DataMember;

            if (WebGridView1 != null)
            {
                List<string> KeyFields = new List<string>();
                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    if ((BFI.RefValNo != null && BFI.RefValNo != "") || BFI.RefField != null)
                    {
                        String DataSourceID = GenWebDataSource(BFI, BlockItem.TableName, "RefVal", "");
                        aTemplateField = new System.Web.UI.WebControls.TemplateField();
                        aTemplateField.HeaderText = BFI.Description;
                        aTemplateField.SortExpression = BFI.DataField;
                        if (aTemplateField.HeaderText == "")
                            aTemplateField.HeaderText = BFI.DataField;
                        if (isAjaxPage)
                        {
                            aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewAjaxRefValEditItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FAjaxRefValList, FClientData.DatabaseType, WebGridView1, FLabelList);
                            aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewAjaxRefValItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FAjaxRefValList, FClientData.DatabaseType, WebGridView1, FLabelList);
                            aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewAjaxRefValFooterItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FAjaxRefValList, FClientData.DatabaseType, WebGridView1, FLabelList);
                        }
                        else
                        {
                            aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewRefValEditItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FWebRefValList, FClientData.DatabaseType, WebGridView1);
                            aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewRefValItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FWebRefValList, FClientData.DatabaseType, WebGridView1);
                            aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewRefValFooterItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FWebRefValList, FClientData.DatabaseType, WebGridView1);
                        }
                        WebGridView1.Columns.Add(aTemplateField);
                    }
                    else if (BFI.ControlType == "ComboBox")
                    {
                        String DataSourceID = GenWebDataSource(BFI, BFI.ComboEntityName, "ComboBox", "");
                        aTemplateField = new System.Web.UI.WebControls.TemplateField();
                        aTemplateField.HeaderText = BFI.Description;
                        aTemplateField.SortExpression = BFI.DataField;
                        if (aTemplateField.HeaderText == "")
                            aTemplateField.HeaderText = BFI.DataField;
                        aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewComboBoxEditItemTemplate", BFI, BlockItem.TableName, DataSourceID, FMyWebDropDownList, FLabelList);
                        aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewComboBoxItemTemplate", BFI, BlockItem.TableName, DataSourceID, FMyWebDropDownList, FLabelList);
                        aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewComboBoxFooterItemTemplate", BFI, BlockItem.TableName, DataSourceID, FMyWebDropDownList, FLabelList);
                        WebGridView1.Columns.Add(aTemplateField);
                    }
                    else if (BFI.ControlType == "ValidateBox")
                    {
                        aTemplateField = new System.Web.UI.WebControls.TemplateField();
                        aTemplateField.HeaderText = BFI.Description;
                        aTemplateField.SortExpression = BFI.DataField;
                        if (aTemplateField.HeaderText == "")
                            aTemplateField.HeaderText = BFI.DataField;
                        aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewValidateBoxEditItemTemplate", BFI, BlockItem.TableName, aValidate, FWebValidateBoxList, FLabelList);
                        aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewValidateBoxItemTemplate", BFI, BlockItem.TableName, aValidate, FWebValidateBoxList, FLabelList);
                        aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewValidateBoxFooterItemTemplate", BFI, BlockItem.TableName, aValidate, FWebValidateBoxList, FLabelList);
                        WebGridView1.Columns.Add(aTemplateField);
                    }
                    else if (BFI.ControlType == "CheckBox")
                    {
                        aTemplateField = new System.Web.UI.WebControls.TemplateField();
                        aTemplateField.HeaderText = BFI.Description;
                        aTemplateField.SortExpression = BFI.DataField;
                        if (aTemplateField.HeaderText == "")
                            aTemplateField.HeaderText = BFI.DataField;
                        aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewCheckBoxEditItemTemplate", BFI, BlockItem.TableName, FWebCheckBoxList, FLabelList);
                        aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewCheckBoxItemTemplate", BFI, BlockItem.TableName, FWebCheckBoxList, FLabelList);
                        aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewCheckBoxFooterItemTemplate", BFI, BlockItem.TableName, FWebCheckBoxList, FLabelList);
                        WebGridView1.Columns.Add(aTemplateField);
                    }
                    else
                    {
                        if (BFI.DataType == typeof(DateTime) || (BFI.ControlType != null && BFI.ControlType.ToUpper() == "DATETIMEBOX"))
                        {
                            aTemplateField = new System.Web.UI.WebControls.TemplateField();
                            aTemplateField.HeaderText = BFI.Description;
                            aTemplateField.SortExpression = BFI.DataField;
                            if (aTemplateField.HeaderText == "")
                                aTemplateField.HeaderText = BFI.DataField;
                            if (isAjaxPage)
                            {
                                aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewAjaxDateTimeEditItemTemplate", BFI, BlockItem.TableName, FAjaxDateTimePickerList, FLabelList, WebGridView1);
                                aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewAjaxDateTimeItemTemplate", BFI, BlockItem.TableName, FAjaxDateTimePickerList, FLabelList, WebGridView1);
                                aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewAjaxDateTimeFooterItemTemplate", BFI, BlockItem.TableName, FAjaxDateTimePickerList, FLabelList, WebGridView1);
                            }
                            else
                            {
                                aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewDateTimeEditItemTemplate", BFI, BlockItem.TableName, FWebDateTimePickerList, FLabelList, WebGridView1);
                                aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewDateTimeItemTemplate", BFI, BlockItem.TableName, FWebDateTimePickerList, FLabelList, WebGridView1);
                                aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewDateTimeFooterItemTemplate", BFI, BlockItem.TableName, FWebDateTimePickerList, FLabelList, WebGridView1);
                            }
                            WebGridView1.Columns.Add(aTemplateField);
                        }
                        else
                        {
                            if (BFI.EditMask != null && BFI.EditMask != String.Empty)
                            {
                                aTemplateField = new System.Web.UI.WebControls.TemplateField();
                                aTemplateField.HeaderText = BFI.Description;
                                aTemplateField.SortExpression = BFI.DataField;
                                if (aTemplateField.HeaderText == "")
                                    aTemplateField.HeaderText = BFI.DataField;
                                aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewTextBoxEditItemTemplate", BFI, BlockItem.TableName, FWebTextBoxList, FLabelList, WebGridView1);
                                aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewTextBoxItemTemplate", BFI, BlockItem.TableName, FWebTextBoxList, FLabelList, WebGridView1);
                                aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewTextBoxFooterItemTemplate", BFI, BlockItem.TableName, FWebTextBoxList, FLabelList, WebGridView1);
                                WebGridView1.Columns.Add(aTemplateField);
                            }
                            else
                            {
                                aBoundField = new System.Web.UI.WebControls.BoundField();
                                aBoundField.DataField = BFI.DataField;
                                aBoundField.SortExpression = BFI.DataField;
                                aBoundField.HeaderText = BFI.Description;
                                //Field.HeaderStyle.Width = BFI.Length * ColumnWidthPixel;
                                if (aBoundField.HeaderText == "")
                                    aBoundField.HeaderText = BFI.DataField;
                                WebGridView1.Columns.Add(aBoundField);
                            }
                        }
                    }
                    if (BFI.IsKey)
                        KeyFields.Add(BFI.DataField);

                    CreateQueryField(BFI, "", null, BlockItem.TableName);

                    GenDefault(BFI, aDefault, aValidate);
                }

                DataTable DT = FWizardDataSet.RealDataSet.Tables[0];
                DataColumn[] PrimDc = DT.PrimaryKey;
                string[] AA = new string[PrimDc.Length];
                for (int J = 0; J < PrimDc.Length; J++)
                    AA[J] = PrimDc[J].ColumnName;

                /*
                WebGridView1.DataKeyNames = new string[AA.Length];
                for (int I = 0; I < AA.Length; I++)
                {
                    WebGridView1.DataKeyNames[I] = AA[I];
                }
                 */
                FWebDefaultList.Add(aDefault);
                FWebValidateList.Add(aValidate);
                IComponentChangeService FComponentChangeService = (IComponentChangeService)FDesignerHost.RootComponent.Site.GetService(typeof(IComponentChangeService));
                NotifyRefresh(200);
                FComponentChangeService.OnComponentChanged(WebGridView1, null, "", "M");
            }

            Object aAjaxGridView = FPage.FindControl("AjaxGridView1");
            if (aAjaxGridView != null)
            {
                bool flag = true;
                DataTable srcTable = FWizardDataSet.RealDataSet.Tables[BlockItem.TableName];
                IList iColumns = aAjaxGridView.GetType().GetProperty("Columns").GetValue(aAjaxGridView, null) as IList;
                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    Type columnsType = aAjaxGridView.GetType().GetProperty("Columns").PropertyType.GetProperties()[0].PropertyType;
                    object extCol = Activator.CreateInstance(columnsType);
                    if (BFI.CheckNull == "Y")
                        extCol.GetType().GetProperty("AllowNull").SetValue(extCol, false, null);
                    else
                        extCol.GetType().GetProperty("AllowNull").SetValue(extCol, true, null);
                    extCol.GetType().GetProperty("AllowSort").SetValue(extCol, false, null);
                    extCol.GetType().GetProperty("ColumnName").SetValue(extCol, string.Format("col{0}", BFI.DataField), null);
                    extCol.GetType().GetProperty("DataField").SetValue(extCol, BFI.DataField, null);
                    extCol.GetType().GetProperty("DefaultValue").SetValue(extCol, BFI.DefaultValue, null);
                    extCol.GetType().GetProperty("ExpandColumn").SetValue(extCol, true, null);
                    if (BFI.Description != null && BFI.Description != String.Empty)
                        extCol.GetType().GetProperty("HeaderText").SetValue(extCol, BFI.Description, null);
                    else
                        extCol.GetType().GetProperty("HeaderText").SetValue(extCol, BFI.DataField, null);
                    extCol.GetType().GetProperty("IsKeyField").SetValue(extCol, BFI.IsKey, null);
                    //extCol.GetType().GetProperty("IsKeyField").SetValue(extCol, IsKeyField(BFI.DataField, srcTable.PrimaryKey), null);
                    extCol.GetType().GetProperty("NewLine").SetValue(extCol, flag, null);
                    extCol.GetType().GetProperty("Resizable").SetValue(extCol, true, null);
                    extCol.GetType().GetProperty("TextAlign").SetValue(extCol, "left", null);
                    extCol.GetType().GetProperty("Visible").SetValue(extCol, true, null);
                    extCol.GetType().GetProperty("Width").SetValue(extCol, 75, null);
                    this.FieldTypeSelector(BFI.DataType, extCol, BFI.ControlType);
                    iColumns.Add(extCol);
                    flag = !flag;
                }
                IComponentChangeService FComponentChangeService = (IComponentChangeService)FDesignerHost.RootComponent.Site.GetService(typeof(IComponentChangeService));
                NotifyRefresh(200);
                FComponentChangeService.OnComponentChanged(aAjaxGridView, null, "", "M");
            }
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

        private void GenMainBlockControl_2(TBlockItem BlockItem)
        {
#if VS90
            object oMaster = FDesignerDocument.webControls.item("Master", 0);

            WebDevPage.IHTMLElement eMaster = null;
            WebDevPage.IHTMLElement eWebDetailView1 = null;

            if (oMaster == null || !(oMaster is WebDevPage.IHTMLElement))
                return;
            eMaster = (WebDevPage.IHTMLElement)oMaster;
            eMaster.setAttribute("AutoApply", "true", 0);

            BlockItem.wDataSource = new WebDataSource();
            string mastertablename = string.Empty;
            if (eMaster != null)
            {
                mastertablename = FClientData.ProviderName.Split('.')[1];
                eMaster.setAttribute("DataMember", mastertablename, 0);
            }
            WebDefault Default = new WebDefault();
            Default.ID = "wd" + WzdUtils.RemoveSpecialCharacters(BlockItem.TableName);
            Default.DataSourceID = eMaster.getAttribute("ID", 0).ToString();
            Default.DataMember = mastertablename;

            WebValidate Validate = new WebValidate();
            Validate.ID = "wv" + WzdUtils.RemoveSpecialCharacters(BlockItem.TableName);
            Validate.DataSourceID = eMaster.getAttribute("ID", 0).ToString();
            Validate.DataMember = mastertablename;

            WebQueryFiledsCollection QueryFields = new WebQueryFiledsCollection(null, typeof(QueryField));
            WebQueryColumnsCollection QueryColumns = new WebQueryColumnsCollection(null, typeof(QueryColumns));
            foreach (TBlockFieldItem fielditem in BlockItem.BlockFieldItems)
            {
                GenDefault(fielditem, Default, Validate);
                GenQuery(fielditem, QueryFields, QueryColumns, BlockItem.TableName);
            }

            WebDevPage.IHTMLElement Page = FDesignerDocument.pageContentElement;
            InsertControl(Page, Default);
            InsertControl(Page, Validate);

            foreach (TBlockFieldItem fielditem in BlockItem.BlockFieldItems)
            {
                foreach (WebQueryColumns wqc in QueryColumns)
                {
                    if (wqc.ColumnType == "ClientQueryRefValColumn" && wqc.Column == fielditem.DataField && fielditem.RefValNo != String.Empty)
                    {
                        WebDataSource aWebDataSource = new WebDataSource();
                        InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
                        aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                        //aInfoCommand.Connection = FClientData.Owner.GlobalConnection;
                        IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                        if (FSYS_REFVAL != null)
                            FSYS_REFVAL.Dispose();
                        FSYS_REFVAL = new DataSet();
                        aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", fielditem.RefValNo);
                        WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, FSYS_REFVAL, fielditem.RefValNo);

                        WebRefVal aWebRefVal = new WebRefVal();
                        aWebRefVal.ID = wqc.WebRefVal;
                        aWebRefVal.DataTextField = FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                        aWebRefVal.DataValueField = FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
                        aWebRefVal.DataSourceID = String.Format("wds{0}{1}", WzdUtils.RemoveSpecialCharacters(BlockItem.TableName), wqc.Column);
                        aWebRefVal.Visible = false;
                        InsertControl(Page, aWebRefVal);
                        break;
                    }
                    else if (wqc.ColumnType == "ClientQueryComboBoxColumn" && wqc.Column == fielditem.DataField
                            && !String.IsNullOrEmpty(fielditem.ComboTextField) && !String.IsNullOrEmpty(fielditem.ComboValueField))
                    {
                        WebRefVal aWebRefVal = new WebRefVal();
                        aWebRefVal.ID = wqc.WebRefVal;
                        aWebRefVal.DataTextField = fielditem.ComboTextField;
                        aWebRefVal.DataValueField = fielditem.ComboValueField;
                        aWebRefVal.DataSourceID = String.Format("wds{0}{1}", fielditem.ComboEntityName, wqc.Column);
                        aWebRefVal.Visible = false;
                        InsertControl(Page, aWebRefVal);
                        break;
                    }
                }
            }

            WebDevPage.IHTMLElement Navigator = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("WebNavigator1", 0);
            if (Navigator != null)
            {
                SetCollectionValue(Navigator, typeof(WebNavigator).GetProperty("QueryFields"), QueryFields);
            }
            WebDevPage.IHTMLElement ClientQuery = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("WebClientQuery1", 0);
            if (ClientQuery != null)
            {
                SetCollectionValue(ClientQuery, typeof(WebClientQuery).GetProperty("Columns"), QueryColumns);
            }

            object oWebDetailView1 = FDesignerDocument.webControls.item("wdvMaster", 0);
            eWebDetailView1 = (WebDevPage.IHTMLElement)oWebDetailView1;
            //eWebDetailView1.setAttribute("DataMember", FClientData.TableName, 0);
            //这里本来想再往下找Columns节点的,可是找不到,只能先这样写了
            StringBuilder sb = new StringBuilder(eWebDetailView1.innerHTML);
            int idx = eWebDetailView1.innerHTML.IndexOf("<EmptyDataRowStyle ForeColor=\"Black\" />");
            sb.Insert(idx, "<Fields>\r\n            </Fields>\r\n            ");
            eWebDetailView1.innerHTML = sb.ToString();
            idx = eWebDetailView1.innerHTML.IndexOf("</Fields>");
            List<string> KeyFields = new List<string>();
            foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
            {
                idx = sb.ToString().IndexOf("</Fields>");
                if (!string.IsNullOrEmpty(BFI.RefValNo) || BFI.RefField != null)
                {
                    sb.Insert(idx, GenDetailViewTemplateFieldHTML(BFI.ControlType, BlockItem, BFI));
                }
                else if (BFI.ControlType == "ComboBox" || BFI.ControlType == "ValidateBox" || BFI.ControlType == "CheckBox")
                {
                    sb.Insert(idx, GenDetailViewTemplateFieldHTML(BFI.ControlType, BlockItem, BFI));
                }
                else
                {
                    if (BFI.DataType == typeof(DateTime) || (BFI.ControlType != null && BFI.ControlType == "DateTimeBox"))
                    {
                        sb.Insert(idx, GenDetailViewTemplateFieldHTML("DateTimeBox", BlockItem, BFI));
                    }
                    else
                    {
                        sb.Insert(idx, "\r            <asp:BoundField DataField=\"" + BFI.DataField + "\" HeaderText=\"" + (string.IsNullOrEmpty(BFI.Description) ? BFI.DataField : BFI.Description) + "\" SortExpression=\"" + BFI.DataField + "\" />\r\n            ");
                    }
                }
            }
            eWebDetailView1.innerHTML = sb.ToString();
#else
            bool isAjaxPage = false;
            if (FPage.FindControl("AjaxScriptManager1") != null)
                isAjaxPage = true;

            WebDataSource Master = (WebDataSource)FPage.FindControl("Master");
            Master.AutoApply = true;
            BlockItem.wDataSource = Master;
            WebDetailsView wdvMaster = (WebDetailsView)FPage.FindControl("wdvMaster");

            //Generate RESX
            //???GenResx(Master);

            //wdvMaster.DataMember = FClientData.TableName;
            //???WebGridView1.Columns.Clear();
            System.Web.UI.WebControls.BoundField aBoundField = null;
            System.Web.UI.WebControls.TemplateField aTemplateField = null;
            WebDefault aDefault = new WebDefault();
            aDefault.ID = "wd" + BlockItem.TableName;
            aDefault.DataSourceID = Master.ID;
            aDefault.DataMember = Master.DataMember;
            WebValidate aValidate = new WebValidate();
            aValidate.ID = "wv" + BlockItem.TableName;
            aValidate.DataSourceID = Master.ID;
            aValidate.DataMember = Master.DataMember;
            List<string> KeyFields = new List<string>();
            foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
            {
                if ((BFI.RefValNo != null && BFI.RefValNo != "") || BFI.RefField != null)
                {
                    String DataSourceID = GenWebDataSource(BFI, BlockItem.TableName, "RefVal", "");
                    aTemplateField = new System.Web.UI.WebControls.TemplateField();
                    aTemplateField.HeaderText = BFI.Description;
                    aTemplateField.SortExpression = BFI.DataField;
                    if (aTemplateField.HeaderText == "")
                        aTemplateField.HeaderText = BFI.DataField;
                    aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewRefValEditItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FWebRefValList, FClientData.DatabaseType, null);
                    aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewRefValItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FWebRefValList, FClientData.DatabaseType, null);
                    aTemplateField.InsertItemTemplate = new WebControlTemplate("DetailsViewRefValInsertItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FWebRefValList, FClientData.DatabaseType, null);
                    wdvMaster.Fields.Add(aTemplateField);
                }
                else if (BFI.ControlType == "ComboBox")
                {
                    String DataSourceID = GenWebDataSource(BFI, BFI.ComboEntityName, "ComboBox", "");
                    aTemplateField = new System.Web.UI.WebControls.TemplateField();
                    aTemplateField.HeaderText = BFI.Description;
                    aTemplateField.SortExpression = BFI.DataField;
                    if (aTemplateField.HeaderText == "")
                        aTemplateField.HeaderText = BFI.DataField;
                    aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewComboBoxEditItemTemplate", BFI, BlockItem.TableName, DataSourceID, FMyWebDropDownList, FLabelList);
                    aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewComboBoxItemTemplate", BFI, BlockItem.TableName, DataSourceID, FMyWebDropDownList, FLabelList);
                    aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewComboBoxFooterItemTemplate", BFI, BlockItem.TableName, DataSourceID, FMyWebDropDownList, FLabelList);
                    aTemplateField.InsertItemTemplate = new WebControlTemplate("DetailsViewComboBoxInsertItemTemplate", BFI, BlockItem.TableName, DataSourceID, FMyWebDropDownList, FLabelList);
                    wdvMaster.Fields.Add(aTemplateField);
                }
                else if (BFI.ControlType == "ValidateBox")
                {
                    aTemplateField = new System.Web.UI.WebControls.TemplateField();
                    aTemplateField.HeaderText = BFI.Description;
                    aTemplateField.SortExpression = BFI.DataField;
                    if (aTemplateField.HeaderText == "")
                        aTemplateField.HeaderText = BFI.DataField;
                    aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewValidateBoxEditItemTemplate", BFI, BlockItem.TableName, aValidate, FWebValidateBoxList, FLabelList);
                    aTemplateField.InsertItemTemplate = new WebControlTemplate("DetailsViewValidateBoxInsertItemTemplate", BFI, BlockItem.TableName, aValidate, FWebValidateBoxList, FLabelList);
                    aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewValidateBoxItemTemplate", BFI, BlockItem.TableName, aValidate, FWebValidateBoxList, FLabelList);
                    aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewValidateBoxInsertItemTemplate", BFI, BlockItem.TableName, aValidate, FWebValidateBoxList, FLabelList);
                    wdvMaster.Fields.Add(aTemplateField);
                }
                else if (BFI.ControlType == "CheckBox")
                {
                    aTemplateField = new System.Web.UI.WebControls.TemplateField();
                    aTemplateField.HeaderText = BFI.Description;
                    aTemplateField.SortExpression = BFI.DataField;
                    if (aTemplateField.HeaderText == "")
                        aTemplateField.HeaderText = BFI.DataField;
                    aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewCheckBoxEditItemTemplate", BFI, BlockItem.TableName, FWebCheckBoxList, FLabelList);
                    aTemplateField.InsertItemTemplate = new WebControlTemplate("DetailsViewCheckBoxInsertItemTemplate", BFI, BlockItem.TableName, FWebCheckBoxList, FLabelList);
                    aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewCheckBoxItemTemplate", BFI, BlockItem.TableName, FWebCheckBoxList, FLabelList);
                    aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewCheckBoxInsertItemTemplate", BFI, BlockItem.TableName, FWebCheckBoxList, FLabelList);
                    wdvMaster.Fields.Add(aTemplateField);
                }
                else
                {
                    if (BFI.DataType == typeof(DateTime) || (BFI.ControlType != null && BFI.ControlType.ToUpper() == "DATETIMEBOX"))
                    {
                        aTemplateField = new System.Web.UI.WebControls.TemplateField();
                        aTemplateField.HeaderText = BFI.Description;
                        aTemplateField.SortExpression = BFI.DataField;
                        if (aTemplateField.HeaderText == "")
                            aTemplateField.HeaderText = BFI.DataField;
                        aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewDateTimeEditItemTemplate", BFI, BlockItem.TableName, FWebDateTimePickerList, FLabelList, null);
                        aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewDateTimeItemTemplate", BFI, BlockItem.TableName, FWebDateTimePickerList, FLabelList, null);
                        aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewDateTimeFooterItemTemplate", BFI, BlockItem.TableName, FWebDateTimePickerList, FLabelList, null);
                        aTemplateField.InsertItemTemplate = new WebControlTemplate("DetailsViewDateTimeInsertItemTemplate", BFI, BlockItem.TableName, FWebDateTimePickerList, FLabelList, null);
                        wdvMaster.Fields.Add(aTemplateField);
                    }
                    else
                    {
                        if (BFI.EditMask != null && BFI.EditMask != String.Empty)
                        {
                            aTemplateField = new System.Web.UI.WebControls.TemplateField();
                            aTemplateField.HeaderText = BFI.Description;
                            aTemplateField.SortExpression = BFI.DataField;
                            if (aTemplateField.HeaderText == "")
                                aTemplateField.HeaderText = BFI.DataField;
                            aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewTextBoxEditItemTemplate", BFI, BlockItem.TableName, FWebTextBoxList, FLabelList, null);
                            aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewTextBoxItemTemplate", BFI, BlockItem.TableName, FWebTextBoxList, FLabelList, null);
                            aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewTextBoxFooterItemTemplate", BFI, BlockItem.TableName, FWebTextBoxList, FLabelList, null);
                            aTemplateField.InsertItemTemplate = new WebControlTemplate("DetailsViewTextBoxInsertItemTemplate", BFI, BlockItem.TableName, FWebTextBoxList, FLabelList, null);
                            wdvMaster.Fields.Add(aTemplateField);
                        }
                        else
                        {
                            aBoundField = new System.Web.UI.WebControls.BoundField();
                            aBoundField.DataField = BFI.DataField;
                            aBoundField.SortExpression = BFI.DataField;
                            aBoundField.HeaderText = BFI.Description;
                            aBoundField.HeaderStyle.Width = 150;//BFI.Length * ColumnWidthPixel;
                            if (aBoundField.HeaderText == "")
                                aBoundField.HeaderText = BFI.DataField;
                            wdvMaster.Fields.Add(aBoundField);
                        }
                    }
                }

                if (BFI.IsKey)
                    KeyFields.Add(BFI.DataField);

                CreateQueryField(BFI, "", null, BlockItem.TableName);

                GenDefault(BFI, aDefault, aValidate);
            }

            DataTable DT = FWizardDataSet.RealDataSet.Tables[0];
            DataColumn[] PrimDc = DT.PrimaryKey;
            string[] AA = new string[PrimDc.Length];
            for (int J = 0; J < PrimDc.Length; J++)
                AA[J] = PrimDc[J].ColumnName;
            /*
            wdvMaster.DataKeyNames = new string[AA.Length];
            for (int I = 0; I < AA.Length; I++)
            {
                wdvMaster.DataKeyNames[I] = AA[I];
            }
             */
            FWebDefaultList.Add(aDefault);
            FWebValidateList.Add(aValidate);
            IComponentChangeService FComponentChangeService = (IComponentChangeService)FDesignerHost.RootComponent.Site.GetService(typeof(IComponentChangeService));
            NotifyRefresh(200);
            FComponentChangeService.OnComponentChanged(wdvMaster, null, "", "M");
#endif
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
                            WzdUtils.RemoveSpecialCharacters(ExtraName + FieldItem.DataField), Description);
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
        private void GenMainBlockControl_3(TBlockItem BlockItem, String FormViewName)
        {
            bool isAjaxPage = false;
            if (FPage.FindControl("AjaxScriptManager1") != null)
                isAjaxPage = true;

            WebDataSource Master = (WebDataSource)FPage.FindControl("Master");
            Master.DataMember = FClientData.ProviderName;
            Master.DataMember = Master.DataMember.Substring(Master.DataMember.IndexOf('.') + 1, Master.DataMember.Length -
                                Master.DataMember.IndexOf('.') - 1);

            if (FormViewName == "wfvMaster")
                Master.AutoApply = true;
            BlockItem.wDataSource = Master;
            WebFormView wfvMaster = (WebFormView)FPage.FindControl(FormViewName);
            WebDefault aDefault = new WebDefault();
            aDefault.ID = "wd" + BlockItem.TableName;
            aDefault.DataSourceID = Master.ID;
            aDefault.DataMember = Master.DataMember;

            WebValidate aValidate = new WebValidate();
            aValidate.ID = "wv" + BlockItem.TableName;
            aValidate.DataSourceID = Master.ID;
            aValidate.DataMember = Master.DataMember;
            Boolean Done = false;

            //Generate RESX
            WebGridView aGridView = (WebGridView)FPage.FindControl("WgView");
            if (aGridView == null)
                aGridView = (WebGridView)FPage.FindControl("WebGridView1");
            if (aGridView == null)
                aGridView = (WebGridView)FPage.FindControl("wgvMaster");
            if (aGridView != null)
                aGridView.WizardDesignMode = true;
            GenResx(Master);
            if (aGridView != null)
                aGridView.WizardDesignMode = false;
            if (FClientData.BaseFormName == "WSingle3" || FClientData.BaseFormName == "WSingle4" || FClientData.BaseFormName == "WMasterDetail3"
                || FClientData.BaseFormName == "WMasterDetail8" || FClientData.BaseFormName == "VBWebCMasterDetail8")
            {
                aGridView = null;
            }

            //DataSet Dset = new DataSet();
            //if (FPage.Site.DesignMode)
            //{
            //    Dset = GetDD(wfvMaster);
            //}

            IDbConnection conn = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, false);
            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
            //aInfoCommand.Connection = conn;
            String OWNER = String.Empty, SS = this.FClientData.RealTableName, TableName = String.Empty;
            if (SS.Contains("."))
            {
                OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                TableName = SS;
            }
            aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME='" + TableName + "' OR TABLE_NAME='" + OWNER + "." + TableName + "'";
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet dsColdef = new DataSet();
            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsColdef, this.FClientData.TableName);

            foreach (TBlockFieldItem aFieldItem in BlockItem.BlockFieldItems)
            {
                if (!Done)
                {
                    GenDefault(aFieldItem, aDefault, aValidate);
                    CreateQueryField(aFieldItem, "", null, BlockItem.TableName);
                }
            }
            Done = true;

            //GridView
            System.Web.UI.WebControls.BoundField aBoundField = null;
            System.Web.UI.WebControls.TemplateField aTemplateField = null;
            List<string> KeyFields = new List<string>();
            if (aGridView != null)
            {
                while (aGridView.Columns.Count > 1)
                    aGridView.Columns.RemoveAt(1);

                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    if ((BFI.RefValNo != null && BFI.RefValNo != "") || BFI.RefField != null)
                    {
                        String DataSourceID = GenWebDataSource(BFI, BlockItem.TableName, "RefVal", "");
                        aTemplateField = new System.Web.UI.WebControls.TemplateField();
                        aTemplateField.HeaderText = BFI.Description;
                        aTemplateField.SortExpression = BFI.DataField;
                        if (aTemplateField.HeaderText == "")
                            aTemplateField.HeaderText = BFI.DataField;
                        if (isAjaxPage)
                        {
                            aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewAjaxRefValEditItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FAjaxRefValList, FClientData.DatabaseType, aGridView, FLabelList);
                            aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewAjaxRefValItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FAjaxRefValList, FClientData.DatabaseType, aGridView, FLabelList);
                            aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewAjaxRefValFooterItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FAjaxRefValList, FClientData.DatabaseType, aGridView, FLabelList);
                        }
                        else
                        {
                            aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewRefValEditItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FWebRefValList, FClientData.DatabaseType, aGridView);
                            aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewRefValItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FWebRefValList, FClientData.DatabaseType, aGridView);
                            aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewRefValFooterItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FWebRefValList, FClientData.DatabaseType, aGridView);
                        }
                        aGridView.Columns.Add(aTemplateField);
                    }
                    else if (BFI.ControlType == "ComboBox")
                    {
                        String DataSourceID = GenWebDataSource(BFI, BFI.ComboEntityName, "ComboBox", "");
                        aTemplateField = new System.Web.UI.WebControls.TemplateField();
                        aTemplateField.HeaderText = BFI.Description;
                        aTemplateField.SortExpression = BFI.DataField;
                        if (aTemplateField.HeaderText == "")
                            aTemplateField.HeaderText = BFI.DataField;
                        aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewComboBoxEditItemTemplate", BFI, BlockItem.TableName, DataSourceID, FMyWebDropDownList, FLabelList);
                        aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewComboBoxItemTemplate", BFI, BlockItem.TableName, DataSourceID, FMyWebDropDownList, FLabelList);
                        aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewComboBoxFooterItemTemplate", BFI, BlockItem.TableName, DataSourceID, FMyWebDropDownList, FLabelList);
                        aGridView.Columns.Add(aTemplateField);
                    }
                    else if (BFI.ControlType == "ValidateBox")
                    {
                        aTemplateField = new System.Web.UI.WebControls.TemplateField();
                        aTemplateField.HeaderText = BFI.Description;
                        aTemplateField.SortExpression = BFI.DataField;
                        if (aTemplateField.HeaderText == "")
                            aTemplateField.HeaderText = BFI.DataField;
                        aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewValidateBoxEditItemTemplate", BFI, BlockItem.TableName, aValidate, FWebValidateBoxList, FLabelList);
                        aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewValidateBoxItemTemplate", BFI, BlockItem.TableName, aValidate, FWebValidateBoxList, FLabelList);
                        aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewValidateBoxFooterItemTemplate", BFI, BlockItem.TableName, aValidate, FWebValidateBoxList, FLabelList);
                        aGridView.Columns.Add(aTemplateField);
                    }
                    else if (BFI.ControlType == "CheckBox")
                    {
                        aTemplateField = new System.Web.UI.WebControls.TemplateField();
                        aTemplateField.HeaderText = BFI.Description;
                        aTemplateField.SortExpression = BFI.DataField;
                        if (aTemplateField.HeaderText == "")
                            aTemplateField.HeaderText = BFI.DataField;
                        aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewCheckBoxEditItemTemplate", BFI, BlockItem.TableName, FWebCheckBoxList, FLabelList);
                        aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewCheckBoxItemTemplate", BFI, BlockItem.TableName, FWebCheckBoxList, FLabelList);
                        aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewCheckBoxFooterItemTemplate", BFI, BlockItem.TableName, FWebCheckBoxList, FLabelList);
                        aGridView.Columns.Add(aTemplateField);
                    }
                    else
                    {
                        if (BFI.DataType == typeof(DateTime) || (BFI.ControlType != null && BFI.ControlType.ToUpper() == "DATETIMEBOX"))
                        {
                            aTemplateField = new System.Web.UI.WebControls.TemplateField();
                            aTemplateField.HeaderText = BFI.Description;
                            aTemplateField.SortExpression = BFI.DataField;
                            if (aTemplateField.HeaderText == "")
                                aTemplateField.HeaderText = BFI.DataField;
                            if (isAjaxPage)
                            {
                                aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewAjaxDateTimeEditItemTemplate", BFI, BlockItem.TableName, FAjaxDateTimePickerList, FLabelList, aGridView);
                                aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewAjaxDateTimeItemTemplate", BFI, BlockItem.TableName, FAjaxDateTimePickerList, FLabelList, aGridView);
                                aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewAjaxDateTimeFooterItemTemplate", BFI, BlockItem.TableName, FAjaxDateTimePickerList, FLabelList, aGridView);
                            }
                            else
                            {
                                aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewDateTimeEditItemTemplate", BFI, BlockItem.TableName, FWebDateTimePickerList, FLabelList, aGridView);
                                aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewDateTimeItemTemplate", BFI, BlockItem.TableName, FWebDateTimePickerList, FLabelList, aGridView);
                                aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewDateTimeFooterItemTemplate", BFI, BlockItem.TableName, FWebDateTimePickerList, FLabelList, aGridView);
                            }
                            aGridView.Columns.Add(aTemplateField);
                        }
                        else
                        {
                            if (BFI.EditMask != null && BFI.EditMask != String.Empty)
                            {
                                aTemplateField = new System.Web.UI.WebControls.TemplateField();
                                aTemplateField.HeaderText = BFI.Description;
                                aTemplateField.SortExpression = BFI.DataField;
                                if (aTemplateField.HeaderText == "")
                                    aTemplateField.HeaderText = BFI.DataField;
                                aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewTextBoxEditItemTemplate", BFI, BlockItem.TableName, FWebTextBoxList, FLabelList, aGridView);
                                aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewTextBoxItemTemplate", BFI, BlockItem.TableName, FWebTextBoxList, FLabelList, aGridView);
                                aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewTextBoxFooterItemTemplate", BFI, BlockItem.TableName, FWebTextBoxList, FLabelList, aGridView);
                                aGridView.Columns.Add(aTemplateField);
                            }
                            else
                            {
                                aBoundField = new System.Web.UI.WebControls.BoundField();
                                aBoundField.DataField = BFI.DataField;
                                aBoundField.SortExpression = BFI.DataField;
                                aBoundField.HeaderText = BFI.Description;
                                //Field.HeaderStyle.Width = BFI.Length * ColumnWidthPixel;
                                if (aBoundField.HeaderText == "")
                                    aBoundField.HeaderText = BFI.DataField;
                                aGridView.Columns.Add(aBoundField);
                            }
                        }
                    }
                }
            }
            IComponentChangeService FComponentChangeService = (IComponentChangeService)FDesignerHost.RootComponent.Site.GetService(typeof(IComponentChangeService));

            //AjaxTools.AjaxGridView aAjaxGridView = (AjaxTools.AjaxGridView)FPage.FindControl("AjaxGridView1");
            //if (aAjaxGridView != null)
            //{
            //    DataTable srcTable = GetDesignTable(Master);
            //    bool flag = true;
            //    foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
            //    {
            //        AjaxTools.ExtGridColumn extCol = new AjaxTools.ExtGridColumn();
            //        extCol.AllowSort = false;
            //        extCol.ColumnName = string.Format("col{0}", BFI.DataField);
            //        extCol.DataField = BFI.DataField;
            //        extCol.ExpandColumn = true;
            //        extCol.HeaderText = BFI.Description;
            //        extCol.IsKeyField = BFI.IsKey;
            //        extCol.IsKeyField = IsKeyField(BFI.DataField, srcTable.PrimaryKey);
            //        extCol.NewLine = flag;
            //        extCol.Resizable = true;
            //        extCol.TextAlign = "left";
            //        extCol.Visible = true;
            //        extCol.Width = 75;
            //        this.FieldTypeSelector(BFI.DataType, extCol);

            //        aAjaxGridView.Columns.Add(extCol);
            //        flag = !flag;
            //    }
            //    NotifyRefresh(200);
            //    FComponentChangeService.OnComponentChanged(aAjaxGridView, null, "", "M");
            //}

            if (wfvMaster != null)
            {

                //wfvMaster.EditItemTemplate = new MyTemplate("WebFormViewEditItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FWebRefValList);
                FormViewDesigner aDesigner = FDesignerHost.GetDesigner(wfvMaster) as FormViewDesigner;

                //FormView
                foreach (TemplateGroup tempGroup in aDesigner.TemplateGroups)
                {
                    foreach (TemplateDefinition tempDefin in tempGroup.Templates)
                    {
                        if (tempDefin.Name == "EditItemTemplate" || tempDefin.Name == "InsertItemTemplate" || tempDefin.Name == "ItemTemplate")
                        {
                            StringBuilder builder = new StringBuilder();
                            string content = tempDefin.Content;
                            if (content == null || content.Length == 0)
                                continue;

                            string[] ctrlTexts = content.Split("\r\n".ToCharArray());
                            //Control[] ctrls = ControlParser.ParseControls(host, content);
                            int i = 0;
                            int j = 0;
                            int m = wfvMaster.LayOutColNum * 2;

                            List<string> lists = new List<string>();
                            String ExtraName = "";

                            foreach (TBlockFieldItem aFieldItem in BlockItem.BlockFieldItems)
                            {
                                String FormatStyle = FormatEditMask(aFieldItem.EditMask);

                                if (!Done)
                                {
                                    GenDefault(aFieldItem, aDefault, aValidate);
                                    CreateQueryField(aFieldItem, "", null, BlockItem.TableName);
                                }

                                lists.Add(aFieldItem.DataField);
                                if ((aFieldItem.RefValNo != null && aFieldItem.RefValNo != "") || aFieldItem.RefField != null)
                                {
                                    String DataSourceID = GenWebDataSource(aFieldItem, BlockItem.TableName, "RefVal", "");
                                    switch (tempDefin.Name)
                                    {
                                        case "EditItemTemplate":
                                            ExtraName = "E";
                                            break;
                                        case "InsertItemTemplate":
                                            ExtraName = "I";
                                            if (aFieldItem.DefaultValue != null && aFieldItem.DefaultValue != "")
                                            {
                                                FormViewField aViewField = new FormViewField();
                                                aViewField.ControlID = "wrv" + BlockItem.TableName + aFieldItem.DataField + ExtraName;
                                                aViewField.FieldName = aFieldItem.DataField;
                                                wfvMaster.Fields.Add(aViewField);
                                            }
                                            break;
                                        case "ItemTemplate":
                                            ExtraName = "";
                                            break;
                                    }

                                    DataSet aDataSet = new DataSet();
                                    StringBuilder RefColumns = new StringBuilder("<Columns>");
                                    aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL_D1 where REFVAL_NO = '{0}'", aFieldItem.RefValNo);
                                    WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, aFieldItem.RefValNo);
                                    if (aDataSet != null && aDataSet.Tables.Count > 0 && aDataSet.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow DR in aDataSet.Tables[0].Rows)
                                        {
                                            RefColumns.Append(Environment.NewLine);
                                            RefColumns.Append("<InfoLight:WebRefColumn ColumnName=\"" + DR["FIELD_NAME"].ToString() + "\" HeadText=\"" + DR["HEADER_TEXT"].ToString() + "\" Width=\"100\" />");
                                        }
                                        RefColumns.Append(Environment.NewLine);
                                        RefColumns.Append("</Columns>");
                                    }
                                    else
                                    {
                                        RefColumns = new StringBuilder("");
                                    }

                                    if (tempDefin.Name == "ItemTemplate")
                                    {
                                        String S6 = String.Empty;
                                        if (isAjaxPage)
                                        {
                                            S6 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                        }
                                        else
                                        {
                                            S6 = String.Format("<InfoLight:WebRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"{1}\"{5}) %>' " +
                                                "ButtonImageUrl=\"../Image/refval/RefVal.gif\" DataBindingField=\"{1}\" DataSourceID=\"{2}\" " +
                                                "DataTextField=\"{3}\" DataValueField=\"{4}\" ReadOnly=\"True\" ResxDataSet=\"\" " +
                                                "ResxFilePath=\"\" UseButtonImage=\"True\" Width=\"130px\" BackColor=\"Transparent\" BorderStyle=\"None\"> " +
                                                RefColumns.ToString() +
                                                "</InfoLight:WebRefVal>",
                                                "wrv" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                aFieldItem.DataField,
                                                DataSourceID,
                                                FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                                FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                                FormatStyle
                                                );
                                        }
                                        lists.Add(S6);
                                    }
                                    else
                                    {
                                        String S1 = String.Empty;
                                        if (isAjaxPage)
                                        {
                                            S1 = String.Format("<AjaxTools:AjaxRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"{1}\"{5}) %>' " +
                                                                "DataSourceID=\"{2}\" " +
                                                                "DataTextField=\"{3}\" DataValueField=\"{4}\" ResxDataSet=\"\">" +
                                                                 RefColumns.ToString() +
                                                                "</AjaxTools:AjaxRefVal>",
                                                                "arv" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                                aFieldItem.DataField,
                                                                DataSourceID,
                                                                FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                                                FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                                                FormatStyle
                                                                );
                                        }
                                        else
                                        {
                                            S1 = String.Format("<InfoLight:WebRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"{1}\"{5}) %>' " +
                                                "ButtonImageUrl=\"../Image/refval/RefVal.gif\" DataBindingField=\"{1}\" DataSourceID=\"{2}\" " +
                                                "DataTextField=\"{3}\" DataValueField=\"{4}\" ReadOnly=\"False\" ResxDataSet=\"\" " +
                                                "ResxFilePath=\"\" UseButtonImage=\"True\"> " +
                                                 RefColumns.ToString() +
                                                "</InfoLight:WebRefVal>",
                                                "wrv" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                aFieldItem.DataField,
                                                DataSourceID,
                                                FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                                FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                                FormatStyle
                                                );
                                        }
                                        lists.Add(S1);
                                    }
                                }
                                else if (aFieldItem.ControlType == "ComboBox")
                                {
                                    String DataSourceID = GenWebDataSource(aFieldItem, aFieldItem.ComboEntityName, "ComboBox", "");
                                    String S5 = "";
                                    switch (tempDefin.Name)
                                    {
                                        case "EditItemTemplate":
                                            ExtraName = "E";
                                            S5 = String.Format("<InfoLight:WebDropDownList id=\"{0}\" runat=\"server\" DataMember=\"{1}\" DataSourceID=\"{2}\" __designer:wfdid=\"w3\" DataTextField=\"{3}\" Filter DataValueField=\"{4}\" AutoInsertEmptyData=\"False\" SelectedValue='<%# Bind(\"{5}\"{6})%>'  Width=\"130px\"></InfoLight:WebDropDownList>",
                                                "wdd" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                aFieldItem.ComboEntityName,
                                                DataSourceID,
                                                aFieldItem.ComboTextField,
                                                aFieldItem.ComboValueField,
                                                aFieldItem.DataField,
                                                FormatStyle);
                                            break;
                                        case "InsertItemTemplate":
                                            ExtraName = "I";
                                            S5 = String.Format("<InfoLight:WebDropDownList id=\"{0}\" runat=\"server\" DataMember=\"{1}\" DataSourceID=\"{2}\" __designer:wfdid=\"w3\" DataTextField=\"{3}\" Filter DataValueField=\"{4}\" AutoInsertEmptyData=\"False\" SelectedValue='<%# Bind(\"{5}\"{6}) %>'  Width=\"130px\"></InfoLight:WebDropDownList>",
                                                "wdd" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                aFieldItem.ComboEntityName,
                                                DataSourceID,
                                                aFieldItem.ComboTextField,
                                                aFieldItem.ComboValueField,
                                                aFieldItem.DataField,
                                                FormatStyle);
                                            if (aFieldItem.DefaultValue != null && aFieldItem.DefaultValue != "")
                                            {
                                                FormViewField aViewField = new FormViewField();
                                                aViewField.ControlID = "wdd" + BlockItem.TableName + aFieldItem.DataField + ExtraName;
                                                aViewField.FieldName = aFieldItem.DataField;
                                                wfvMaster.Fields.Add(aViewField);
                                            }
                                            break;
                                        case "ItemTemplate":
                                            ExtraName = "";
                                            S5 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                            break;
                                    }
                                    lists.Add(S5);
                                }
                                else if (aFieldItem.ControlType == "ValidateBox")
                                {
                                    String S6 = "";
                                    switch (tempDefin.Name)
                                    {
                                        case "EditItemTemplate":
                                            ExtraName = "E";
                                            S6 = String.Format("<InfoLight:WebValidateBox ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{3}) %>' ValidateField=\"{1}\" WebValidateID=\"{2}\" MaxLength=\"{4}\"></InfoLight:WebValidateBox></td>",
                                                "wvb" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                aFieldItem.DataField,
                                                aValidate.ID,
                                                FormatStyle,
                                                aFieldItem.Length);
                                            break;
                                        case "InsertItemTemplate":
                                            ExtraName = "I";
                                            if (aFieldItem.DefaultValue != null && aFieldItem.DefaultValue != "")
                                            {
                                                FormViewField aViewField = new FormViewField();
                                                aViewField.ControlID = "wdd" + BlockItem.TableName + aFieldItem.DataField + ExtraName;
                                                aViewField.FieldName = aFieldItem.DataField;
                                                wfvMaster.Fields.Add(aViewField);
                                            }
                                            S6 = String.Format("<InfoLight:WebValidateBox ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{3}) %>' ValidateField=\"{1}\" WebValidateID=\"{2}\" MaxLength=\"{4}\"></InfoLight:WebValidateBox></td>",
                                                "wvb" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                aFieldItem.DataField,
                                                aValidate.ID,
                                                FormatStyle,
                                                aFieldItem.Length);
                                            break;
                                        case "ItemTemplate":
                                            ExtraName = "";
                                            S6 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                            break;
                                    }
                                    lists.Add(S6);
                                }
                                else if (aFieldItem.ControlType == "CheckBox")
                                {
                                    String S6 = "";
                                    switch (tempDefin.Name)
                                    {
                                        case "EditItemTemplate":
                                            ExtraName = "E";
                                            S6 = String.Format("<asp:CheckBox ID=\"{0}\" runat=\"server\" Checked='<%# Bind(\"{1}\"{2}) %>'></asp:CheckBox></td>",
                                                "cb" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                aFieldItem.DataField,
                                                FormatStyle);
                                            break;
                                        case "InsertItemTemplate":
                                            ExtraName = "I";
                                            S6 = String.Format("<asp:CheckBox ID=\"{0}\" runat=\"server\" Checked='<%# Bind(\"{1}\"{2}) %>'></asp:CheckBox></td>",
                                                "wvb" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                aFieldItem.DataField,
                                                FormatStyle);
                                            break;
                                        case "ItemTemplate":
                                            ExtraName = "";
                                            S6 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                            break;
                                    }
                                    lists.Add(S6);
                                }
                                else
                                {
                                    if (aFieldItem.DataType == typeof(DateTime) || (aFieldItem.ControlType != null && aFieldItem.ControlType.ToUpper() == "DATETIMEBOX"))
                                    {
                                        String DataTimeType = "";
                                        if (aFieldItem.EditMask == "ShortDate")
                                            DataTimeType = "ShortDate";
                                        else if (aFieldItem.EditMask == "LongDate")
                                            DataTimeType = "ShortDate";
                                        else
                                            DataTimeType = "None";

                                        String S4 = "";
                                        if (isAjaxPage)
                                        {
                                            switch (tempDefin.Name)
                                            {
                                                case "EditItemTemplate":
                                                    ExtraName = "E";
                                                    if (aFieldItem.DataType == typeof(DateTime))
                                                        S4 = String.Format("<AjaxTools:AjaxDateTimePicker id=\"{0}\" runat=\"server\" Width=\"130px\" Text='<%# Bind(\"{1}\"{2}) %>' DateFormat=\"" + DataTimeType + "\" DateTimeType=\"DateTime\"></AjaxTools:AjaxDateTimePicker>",
                                                            "wdtp" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                            aFieldItem.DataField,
                                                            FormatStyle);
                                                    else if (aFieldItem.DataType == typeof(String) && (aFieldItem.ControlType != null && aFieldItem.ControlType.ToUpper() == "DATETIMEBOX"))
                                                        S4 = String.Format("<AjaxTools:AjaxDateTimePicker id=\"{0}\" runat=\"server\" Width=\"130px\" DateString='<%# Bind(\"{1}\"{2}) %>' DateFormat=\"" + DataTimeType + "\" DateTimeType=\"Varchar\"></AjaxTools:AjaxDateTimePicker>",
                                                            "wdtp" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                            aFieldItem.DataField,
                                                            FormatStyle);
                                                    break;
                                                case "InsertItemTemplate":
                                                    ExtraName = "I";
                                                    if (aFieldItem.DataType == typeof(DateTime))
                                                        S4 = String.Format("<AjaxTools:AjaxDateTimePicker id=\"{0}\" runat=\"server\" Width=\"130px\" Text='<%# Bind(\"{1}\"{2}) %>' DateFormat=\"" + DataTimeType + "\" DateTimeType=\"DateTime\"></AjaxTools:AjaxDateTimePicker>",
                                                            "wdtp" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                            aFieldItem.DataField,
                                                            FormatStyle);
                                                    else if (aFieldItem.DataType == typeof(String) && (aFieldItem.ControlType != null && aFieldItem.ControlType.ToUpper() == "DATETIMEBOX"))
                                                        S4 = String.Format("<AjaxTools:AjaxDateTimePicker id=\"{0}\" runat=\"server\" Width=\"130px\" DateString='<%# Bind(\"{1}\"{2}) %>' DateFormat=\"" + DataTimeType + "\" DateTimeType=\"Varchar\"></AjaxTools:AjaxDateTimePicker>",
                                                            "wdtp" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                            aFieldItem.DataField,
                                                            FormatStyle);
                                                    break;
                                                case "ItemTemplate":
                                                    ExtraName = "";
                                                    S4 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            switch (tempDefin.Name)
                                            {
                                                case "EditItemTemplate":
                                                    ExtraName = "E";
                                                    if (aFieldItem.DataType == typeof(DateTime))
                                                        S4 = String.Format("<InfoLight:WebDateTimePicker id=\"{0}\" runat=\"server\" Width=\"100px\" Text='<%# Bind(\"{1}\"{2}) %>' __designer:wfdid=\"w14\" UseButtonImage=\"True\" DateFormat=\"" + DataTimeType + "\" DateTimeType=\"DateTime\"></InfoLight:WebDateTimePicker>",
                                                            "wdtp" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                            aFieldItem.DataField,
                                                            FormatStyle);
                                                    else if (aFieldItem.DataType == typeof(String) && (aFieldItem.ControlType != null && aFieldItem.ControlType.ToUpper() == "DATETIMEBOX"))
                                                        S4 = String.Format("<InfoLight:WebDateTimePicker id=\"{0}\" runat=\"server\" Width=\"100px\" DateString='<%# Bind(\"{1}\"{2}) %>' __designer:wfdid=\"w14\" UseButtonImage=\"True\" DateFormat=\"" + DataTimeType + "\" DateTimeType=\"Varchar\"></InfoLight:WebDateTimePicker>",
                                                            "wdtp" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                            aFieldItem.DataField,
                                                            FormatStyle);
                                                    break;
                                                case "InsertItemTemplate":
                                                    ExtraName = "I";
                                                    if (aFieldItem.DataType == typeof(DateTime))
                                                        S4 = String.Format("<InfoLight:WebDateTimePicker id=\"{0}\" runat=\"server\" Width=\"100px\" Text='<%# Bind(\"{1}\"{2}) %>' __designer:wfdid=\"w14\" UseButtonImage=\"True\" DateFormat=\"" + DataTimeType + "\" DateTimeType=\"DateTime\"></InfoLight:WebDateTimePicker>",
                                                            "wdtp" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                            aFieldItem.DataField,
                                                            FormatStyle);
                                                    else if (aFieldItem.DataType == typeof(String) && (aFieldItem.ControlType != null && aFieldItem.ControlType.ToUpper() == "DATETIMEBOX"))
                                                        S4 = String.Format("<InfoLight:WebDateTimePicker id=\"{0}\" runat=\"server\" Width=\"100px\" DateString='<%# Bind(\"{1}\"{2}) %>' __designer:wfdid=\"w14\" UseButtonImage=\"True\" DateFormat=\"" + DataTimeType + "\" DateTimeType=\"Varchar\"></InfoLight:WebDateTimePicker>",
                                                            "wdtp" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                            aFieldItem.DataField,
                                                            FormatStyle);
                                                    break;
                                                case "ItemTemplate":
                                                    ExtraName = "";
                                                    S4 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                                    break;
                                            }
                                        }
                                        lists.Add(S4);
                                    }
                                    else
                                    {
                                        if (tempDefin.Name == "ItemTemplate")
                                        {
                                            String S3 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                            lists.Add(S3);
                                        }
                                        else
                                        {
                                            if (tempDefin.Name == "InsertItemTemplate")
                                            {
                                                if (aFieldItem.DefaultValue != null && aFieldItem.DefaultValue != "")
                                                {
                                                    FormViewField aViewField = new FormViewField();
                                                    aViewField.ControlID = "tb" + aFieldItem.DataField;
                                                    aViewField.FieldName = aFieldItem.DataField;
                                                    wfvMaster.Fields.Add(aViewField);
                                                }
                                            }
                                            String S4 = String.Format("<asp:TextBox ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>' MaxLength=\"{3}\"></asp:TextBox>", "tb" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle, aFieldItem.Length);
                                            lists.Add(S4);
                                        }
                                    }
                                }
                            }
                            Done = true;

                            j = j * 2;

                            if (m > 0)
                            {
                                builder.Append("<table>");
                            }

                            foreach (string ctrlText in lists.ToArray())
                            {
                                if (ctrlText == null || ctrlText.Length == 0)
                                    continue;

                                if (m > 0)
                                {
                                    if (i % m == 0)
                                    {
                                        builder.Append("<tr>");
                                    }

                                    builder.Append("<td>");
                                }
                                // add dd

                                string ddText = "";
                                if (tempDefin.Name != "ItemTemplate")
                                {
                                    ddText = GetDDText(ctrlText, BlockItem, tempDefin.Name);
                                }
                                else
                                {
                                    ddText = GetDDText(ctrlText, BlockItem, tempDefin.Name);
                                }
                                builder.Append(ddText);
                                builder.Append("\r\n");

                                if (m > 0)
                                {
                                    builder.Append("</td>");

                                    if (i % m == m - 1)
                                    {
                                        builder.Append("</tr>");
                                    }
                                }
                                i++;
                            }

                            if (m > 0)
                            {
                                if (i % m != 0)
                                {
                                    int n = m - (i % m);
                                    int q = 0;
                                    while (q < n)
                                    {
                                        builder.Append("<td></td>");
                                        q++;
                                    }
                                    builder.Append("</tr>");
                                }
                                builder.Append("</table>");
                            }

                            tempDefin.Content = builder.ToString();
                        }
                    }
                }
            }

            Object aAjaxLayout = FPage.FindControl("AjaxLayout1");
            if (aAjaxLayout != null)
            {
                aAjaxLayout.GetType().GetProperty("Title").SetValue(aAjaxLayout, FClientData.FormTitle, null);
                FComponentChangeService.OnComponentChanged(aAjaxLayout, null, "", "M");
            }
            Object aAjaxFormView = FPage.FindControl("AjaxFormView1");
            if (aAjaxFormView != null)
            {
                bool flag = true;
                DataTable srcTable = FWizardDataSet.RealDataSet.Tables[BlockItem.TableName];
                IList iFields = aAjaxFormView.GetType().GetProperty("Fields").GetValue(aAjaxFormView, null) as IList;
                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    Type fieldsType = aAjaxFormView.GetType().GetProperty("Fields").PropertyType.GetProperties()[0].PropertyType;
                    object extCol = Activator.CreateInstance(fieldsType);
                    if (BFI.CheckNull == "Y")
                        extCol.GetType().GetProperty("AllowNull").SetValue(extCol, false, null);
                    else
                        extCol.GetType().GetProperty("AllowNull").SetValue(extCol, true, null);
                    if (BFI.Description != null && BFI.Description != String.Empty)
                        extCol.GetType().GetProperty("Caption").SetValue(extCol, BFI.Description, null);
                    else
                        extCol.GetType().GetProperty("Caption").SetValue(extCol, BFI.DataField, null);
                    extCol.GetType().GetProperty("DataField").SetValue(extCol, BFI.DataField, null);
                    extCol.GetType().GetProperty("DefaultValue").SetValue(extCol, BFI.DefaultValue, null);
                    extCol.GetType().GetProperty("FieldControlId").SetValue(extCol, string.Format("ctrl{0}", BFI.DataField), null);
                    extCol.GetType().GetProperty("IsKeyField").SetValue(extCol, IsKeyField(BFI.DataField, srcTable.PrimaryKey), null);
                    extCol.GetType().GetProperty("NewLine").SetValue(extCol, flag, null);
                    //extCol.GetType().GetProperty("Resizable").SetValue(extCol, true, null);
                    //extCol.GetType().GetProperty("TextAlign").SetValue(extCol, "left", null);
                    //extCol.GetType().GetProperty("Visible").SetValue(extCol, true, null);
                    extCol.GetType().GetProperty("Width").SetValue(extCol, 140, null);
                    if ((BFI.RefValNo != null && BFI.RefValNo != "") || BFI.RefField != null)
                    {
                        String DataSourceID = GenWebDataSource(BFI, BlockItem.TableName, "RefVal", "", true);
                        String extComboBox = GenExtComboBox(BFI, BlockItem.TableName, "ExtRefVal", "", DataSourceID);
                        extCol.GetType().GetProperty("EditControlId").SetValue(extCol, extComboBox, null);
                        extCol.GetType().GetProperty("Editor").SetValue(extCol, extCol.GetType().GetProperty("Editor").PropertyType.GetField("ComboBox").GetValue(extCol), null);
                    }
                    else if (BFI.ControlType == "ComboBox")
                    {
                        String DataSourceID = GenWebDataSource(BFI, BlockItem.TableName, "ComboBox", "", true);
                        String extComboBox = GenExtComboBox(BFI, BlockItem.TableName, "ExtComboBox", "", DataSourceID);
                        extCol.GetType().GetProperty("EditControlId").SetValue(extCol, extComboBox, null);
                        extCol.GetType().GetProperty("Editor").SetValue(extCol, extCol.GetType().GetProperty("Editor").PropertyType.GetField("ComboBox").GetValue(extCol), null);
                    }
                    this.FieldTypeSelector(BFI.DataType, extCol, BFI.ControlType);
                    iFields.Add(extCol);
                    flag = !flag;
                }
                NotifyRefresh(200);
                FComponentChangeService.OnComponentChanged(aAjaxFormView, null, "", "M");
            }

            FWebDefaultList.Add(aDefault);
            FWebValidateList.Add(aValidate);
            NotifyRefresh(200);
            FComponentChangeService.OnComponentChanged(wfvMaster, null, "", "M");
            FComponentChangeService.OnComponentChanged(Master, null, "", "M");
            FComponentChangeService.OnComponentChanged(aGridView, null, "", "M");

        }

        //因为DD只有一个格式栏位，所以Web和Windows统一一种Format格式
        private String FormatEditMask(String editMask)
        {
            if (editMask != null && editMask != String.Empty)
                editMask = ",\"{0:" + editMask + "}\"";
            return editMask;
        }
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
                if (FClientData.BaseFormName == "WMasterDetail1" || FClientData.BaseFormName == "WMasterDetail3"
                    || FClientData.BaseFormName == "WMasterDetail4" || FClientData.BaseFormName == "WMasterDetail6"
                    || FClientData.BaseFormName == "WMasterDetail7" || FClientData.BaseFormName == "ExtMasterDetail")
                {
                    GenMainBlockControl_3(BlockItem, "wfvMaster");  //因為RefreshSchema要等Detail.DataMember全部設定完
                }
                if (FClientData.BaseFormName == "ExtSingle")
                {
                    GenMainBlockControl_3(BlockItem, "WebFormView1");  //因為RefreshSchema要等Detail.DataMember全部設定完
                }
                if (FClientData.BaseFormName == "WQuery" || FClientData.BaseFormName == "ExtQuery")
                {
                    GenMainBlockControl(BlockItem);
                }
                if (FClientData.BaseFormName == "VBWebSingle")
                {
                    GenMainBlockControl(BlockItem);
                }
                if (FClientData.BaseFormName == "VBWSingle1" || FClientData.BaseFormName == "VBWebSingle0")
                {
                    GenMainBlockControl_3(BlockItem, "WebFormView1");  //因為RefreshSchema要等Detail.DataMember全部設定完
                }
                if (FClientData.BaseFormName == "VBWebCMasterDetail_FG" || FClientData.BaseFormName == "VBWebCMasterDetail_VFG"
                    || FClientData.BaseFormName == "VBWebCMasterDetail4" || FClientData.BaseFormName == "VBWebCMasterDetail8"
                    || FClientData.BaseFormName == "VBWebMasterDetail6" || FClientData.BaseFormName == "VBWebMasterDetail7")
                {
                    GenMainBlockControl_3(BlockItem, "wfvMaster");
                }
                if (FClientData.BaseFormName == "VBWebCMasterDetail_DG")
                {
                    GenMainBlockControl_2(BlockItem);
                }
                if (FClientData.BaseFormName == "VBWebQuery")
                {
                    GenMainBlockControl(BlockItem);
                }
            }
        }

        private void SetBlockFieldDataComponent(TBlockItem BlockItem, string DataSetName)
        {
            InfoBindingSource aBindingSource = FDesignerHost.CreateComponent(typeof(InfoBindingSource), "ibs" + WzdUtils.RemoveSpecialCharacters(BlockItem.TableName)) as InfoBindingSource;
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
                Context = Context.Replace("this.WMaster.RemoteName = null;", "this.WMaster.RemoteName = \"" + FClientData.ProviderName + "\";");
                Context = Context.Replace("this.WMaster.Active = false;", "this.WMaster.Active = true;");
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

            SaveWebDataSet(FWizardDataSet);

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
                if (B.wDataSource == null)
                {
                    BlockItem = B;
                    break;
                }
            }

#if VS90
            object oDetail = FDesignerDocument.webControls.item("Detail", 0);

            WebDevPage.IHTMLElement eDetail = null;
            WebDevPage.IHTMLElement eWebGridView1 = null;

            if (oDetail == null || !(oDetail is WebDevPage.IHTMLElement))
                return;
            eDetail = (WebDevPage.IHTMLElement)oDetail;
            eDetail.setAttribute("DataMember", BlockItem.TableName, 0);

            WebDevPage.IHTMLElement AjaxGridView1 = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("AjaxGridViewDetail", 0);
            if (AjaxGridView1 != null)
            {
                AjaxTools.ExtGridColumnCollection aExtGridColumnCollection = new AjaxTools.ExtGridColumnCollection(new AjaxTools.AjaxGridView(), typeof(AjaxTools.ExtColumnMatch));
                DataTable srcTable = FWizardDataSet.RealDataSet.Tables[BlockItem.TableName];
                bool flag = true;
                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    AjaxTools.ExtGridColumn extCol = new AjaxTools.ExtGridColumn();
                    if (BFI.CheckNull == "Y")
                        extCol.AllowNull = false;
                    else
                        extCol.AllowNull = true;
                    extCol.AllowSort = false;
                    extCol.ColumnName = string.Format("col{0}", AjaxGridView1.id + BFI.DataField);
                    extCol.DataField = BFI.DataField;
                    extCol.DefaultValue = BFI.DefaultValue;
                    extCol.ExpandColumn = true;
                    if (BFI.Description != null && BFI.Description != String.Empty)
                        extCol.HeaderText = BFI.Description;
                    else
                        extCol.HeaderText = BFI.DataField;
                    //extCol.IsKeyField = BFI.IsKey;
                    extCol.IsKeyField = IsKeyField(BFI.DataField, srcTable.PrimaryKey);
                    extCol.NewLine = flag;
                    extCol.Resizable = true;
                    extCol.TextAlign = "left";
                    extCol.Visible = true;
                    extCol.Width = 75;
                    if ((BFI.RefValNo != null && BFI.RefValNo != "") || BFI.RefField != null)
                    {
                        String DataSourceID = GenWebDataSource(BFI, WzdUtils.RemoveSpecialCharacters(BlockItem.TableName), "RefVal", "", true);
                        String extComboBox = GenExtComboBox(BFI, WzdUtils.RemoveSpecialCharacters(BlockItem.TableName), "ExtRefVal", "", DataSourceID);
                        try
                        {
                            String str = AjaxGridView1.innerHTML;
                        }
                        catch
                        {
                            AjaxGridView1 = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("AjaxGridViewDetail", 0);
                        }
                        extCol.EditControlId = extComboBox;
                        extCol.Editor = AjaxTools.ExtGridEditor.ComboBox;
                    }
                    else if (BFI.ControlType == "ComboBox")
                    {
                        String DataSourceID = GenWebDataSource(BFI, WzdUtils.RemoveSpecialCharacters(BFI.ComboEntityName), "ComboBox", "", true);
                        String extComboBox = GenExtComboBox(BFI, WzdUtils.RemoveSpecialCharacters(BlockItem.TableName), "ExtComboBox", "", DataSourceID);
                        try
                        {
                            String str = AjaxGridView1.innerHTML;
                        }
                        catch
                        {
                            AjaxGridView1 = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("AjaxGridViewDetail", 0);
                        }
                        extCol.EditControlId = extComboBox;
                        extCol.Editor = AjaxTools.ExtGridEditor.ComboBox;
                    }
                    this.FieldTypeSelector(BFI.DataType, extCol, BFI.ControlType);

                    aExtGridColumnCollection.Add(extCol);

                    flag = !flag;

                }
                SetCollectionValue(AjaxGridView1, typeof(AjaxTools.AjaxGridView).GetProperty("Columns"), aExtGridColumnCollection);
            }

#else
            bool isAjaxPage = false;
            if (FPage.FindControl("AjaxScriptManager1") != null)
                isAjaxPage = true;

            WebDataSource Detail = (WebDataSource)FPage.FindControl("Detail");
            Detail.DataMember = BlockItem.TableName;
            IComponentChangeService FComponentChangeService = (IComponentChangeService)FDesignerHost.RootComponent.Site.GetService(typeof(IComponentChangeService));
            FComponentChangeService.OnComponentChanged(Detail, null, "", "M");
            WebGridView WebGridView2 = (WebGridView)FPage.FindControl("WebGridView2");
            if (WebGridView2 == null)
            {
                WebGridView2 = (WebGridView)FPage.FindControl("wgvDetail");
            }
            if (WebGridView2 != null)
            {
                int length = WebGridView2.Columns.Count;
                for (int i = 1; i < length; i++)
                {
                    WebGridView2.Columns.RemoveAt(1);
                }
                WebGridView2.DataSourceID = "Detail";
                //WebGridView2.DataMember = BlockItem.TableName;
                //???WebGridView2.Columns.Clear();
                System.Web.UI.WebControls.BoundField aBoundField = null;
                System.Web.UI.WebControls.TemplateField aTemplateField = null;
                WebDefault aDefault = new WebDefault();
                aDefault.ID = "wd" + BlockItem.TableName;
                aDefault.DataSourceID = Detail.ID;
                aDefault.DataMember = Detail.DataMember;
                WebValidate aValidate = new WebValidate();
                aValidate.ID = "wv" + BlockItem.TableName;
                aValidate.DataSourceID = Detail.ID;
                aValidate.DataMember = Detail.DataMember;
                List<string> KeyFields = new List<string>();
                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    //if (TemplateName == "WMasterDetail4")
                    //{
                    //    BFI.RefValNo = null;
                    //    BFI.ControlType = "TextBox";
                    //    BFI.DataType = null;
                    //}
                    if ((BFI.RefValNo != null && BFI.RefValNo != "") || BFI.RefField != null)
                    {
                        String DataSourceID = GenWebDataSource(BFI, BlockItem.TableName, "RefVal", "");
                        aTemplateField = new System.Web.UI.WebControls.TemplateField();
                        aTemplateField.HeaderText = BFI.Description;
                        aTemplateField.SortExpression = BFI.DataField;
                        if (aTemplateField.HeaderText == "")
                            aTemplateField.HeaderText = BFI.DataField;

                        if (isAjaxPage)
                        {
                            if (TemplateName != "WMasterDetail4" && TemplateName != "VBWebCMasterDetail4")
                            {
                                aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewAjaxRefValEditItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FAjaxRefValList, FClientData.DatabaseType, WebGridView2, FLabelList);
                                aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewAjaxRefValFooterItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FAjaxRefValList, FClientData.DatabaseType, WebGridView2, FLabelList);
                            }
                            aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewAjaxRefValItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FAjaxRefValList, FClientData.DatabaseType, WebGridView2, FLabelList);
                        }
                        else
                        {
                            if (TemplateName != "WMasterDetail4" && TemplateName != "VBWebCMasterDetail4")
                            {
                                aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewRefValEditItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FWebRefValList, FClientData.DatabaseType, WebGridView2);
                                aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewRefValFooterItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FWebRefValList, FClientData.DatabaseType, WebGridView2);
                            }
                            aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewRefValItemTemplate", BFI, BlockItem.TableName, DataSourceID, FClientData.Owner.GlobalConnection, FWebRefValList, FClientData.DatabaseType, WebGridView2);
                        }

                        WebGridView2.Columns.Add(aTemplateField);
                    }
                    else if (BFI.ControlType == "ComboBox")
                    {
                        String DataSourceID = GenWebDataSource(BFI, BFI.ComboEntityName, "ComboBox", "");
                        aTemplateField = new System.Web.UI.WebControls.TemplateField();
                        aTemplateField.HeaderText = BFI.Description;
                        aTemplateField.SortExpression = BFI.DataField;
                        if (aTemplateField.HeaderText == "")
                            aTemplateField.HeaderText = BFI.DataField;
                        if (TemplateName != "WMasterDetail4" && TemplateName != "VBWebCMasterDetail4")
                        {
                            aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewComboBoxEditItemTemplate", BFI, BlockItem.TableName, DataSourceID, FMyWebDropDownList, FLabelList);
                            aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewComboBoxFooterItemTemplate", BFI, BlockItem.TableName, DataSourceID, FMyWebDropDownList, FLabelList);
                        }
                        aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewComboBoxItemTemplate", BFI, BlockItem.TableName, DataSourceID, FMyWebDropDownList, FLabelList);
                        WebGridView2.Columns.Add(aTemplateField);
                    }
                    else if (BFI.ControlType == "ValidateBox")
                    {
                        aTemplateField = new System.Web.UI.WebControls.TemplateField();
                        aTemplateField.HeaderText = BFI.Description;
                        aTemplateField.SortExpression = BFI.DataField;
                        if (aTemplateField.HeaderText == "")
                            aTemplateField.HeaderText = BFI.DataField;
                        if (TemplateName != "WMasterDetail4" && TemplateName != "VBWebCMasterDetail4")
                        {
                            aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewValidateBoxEditItemTemplate", BFI, BlockItem.TableName, aValidate, FWebValidateBoxList, FLabelList);
                            aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewValidateBoxFooterItemTemplate", BFI, BlockItem.TableName, aValidate, FWebValidateBoxList, FLabelList);
                        }
                        aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewValidateBoxItemTemplate", BFI, BlockItem.TableName, aValidate, FWebValidateBoxList, FLabelList);
                        WebGridView2.Columns.Add(aTemplateField);
                    }
                    else if (BFI.ControlType == "CheckBox")
                    {
                        aTemplateField = new System.Web.UI.WebControls.TemplateField();
                        aTemplateField.HeaderText = BFI.Description;
                        aTemplateField.SortExpression = BFI.DataField;
                        if (aTemplateField.HeaderText == "")
                            aTemplateField.HeaderText = BFI.DataField;
                        if (TemplateName != "WMasterDetail4" && TemplateName != "VBWebCMasterDetail4")
                        {
                            aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewCheckBoxEditItemTemplate", BFI, BlockItem.TableName, FWebCheckBoxList, FLabelList);
                            aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewCheckBoxFooterItemTemplate", BFI, BlockItem.TableName, FWebCheckBoxList, FLabelList);
                        }
                        aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewCheckBoxItemTemplate", BFI, BlockItem.TableName, FWebCheckBoxList, FLabelList);
                        WebGridView2.Columns.Add(aTemplateField);
                    }
                    else
                    {
                        if (BFI.DataType == typeof(DateTime) || (BFI.ControlType != null && BFI.ControlType.ToUpper() == "DATETIMEBOX"))
                        {
                            aTemplateField = new System.Web.UI.WebControls.TemplateField();
                            aTemplateField.HeaderText = BFI.Description;
                            aTemplateField.SortExpression = BFI.DataField;
                            if (aTemplateField.HeaderText == "")
                                aTemplateField.HeaderText = BFI.DataField;

                            if (isAjaxPage)
                            {
                                if (TemplateName != "WMasterDetail4" && TemplateName != "VBWebCMasterDetail4")
                                {
                                    aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewAjaxDateTimeEditItemTemplate", BFI, BlockItem.TableName, FAjaxDateTimePickerList, FLabelList, WebGridView2);
                                    aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewAjaxDateTimeFooterItemTemplate", BFI, BlockItem.TableName, FAjaxDateTimePickerList, FLabelList, WebGridView2);
                                }
                                aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewAjaxDateTimeItemTemplate", BFI, BlockItem.TableName, FAjaxDateTimePickerList, FLabelList, WebGridView2);
                            }
                            else
                            {
                                if (TemplateName != "WMasterDetail4" && TemplateName != "VBWebCMasterDetail4")
                                {
                                    aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewDateTimeEditItemTemplate", BFI, BlockItem.TableName, FWebDateTimePickerList, FLabelList, WebGridView2);
                                    aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewDateTimeFooterItemTemplate", BFI, BlockItem.TableName, FWebDateTimePickerList, FLabelList, WebGridView2);
                                }
                                aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewDateTimeItemTemplate", BFI, BlockItem.TableName, FWebDateTimePickerList, FLabelList, WebGridView2);
                            }
                            WebGridView2.Columns.Add(aTemplateField);
                        }
                        else
                        {
                            if (BFI.EditMask != null && BFI.EditMask != String.Empty)
                            {
                                aTemplateField = new System.Web.UI.WebControls.TemplateField();
                                aTemplateField.HeaderText = BFI.Description;
                                aTemplateField.SortExpression = BFI.DataField;
                                if (aTemplateField.HeaderText == "")
                                    aTemplateField.HeaderText = BFI.DataField;
                                if (TemplateName != "WMasterDetail4" && TemplateName != "VBWebCMasterDetail4")
                                {
                                    aTemplateField.EditItemTemplate = new WebControlTemplate("WebGridViewTextBoxEditItemTemplate", BFI, BlockItem.TableName, FWebTextBoxList, FLabelList, WebGridView2);
                                    aTemplateField.FooterTemplate = new WebControlTemplate("WebGridViewTextBoxFooterItemTemplate", BFI, BlockItem.TableName, FWebTextBoxList, FLabelList, WebGridView2);
                                }
                                aTemplateField.ItemTemplate = new WebControlTemplate("WebGridViewTextBoxItemTemplate", BFI, BlockItem.TableName, FWebTextBoxList, FLabelList, WebGridView2);
                                WebGridView2.Columns.Add(aTemplateField);
                            }
                            else
                            {
                                aBoundField = new System.Web.UI.WebControls.BoundField();
                                aBoundField.DataField = BFI.DataField;
                                aBoundField.SortExpression = BFI.DataField;
                                aBoundField.HeaderText = BFI.Description;
                                //Field.HeaderStyle.Width = BFI.Length * ColumnWidthPixel;
                                if (aBoundField.HeaderText == "")
                                    aBoundField.HeaderText = BFI.DataField;
                                WebGridView2.Columns.Add(aBoundField);
                            }
                        }
                    }
                    if (BFI.IsKey)
                        KeyFields.Add(BFI.DataField);

                    GenDefault(BFI, aDefault, aValidate);
                }
                /*
                WebGridView2.DataKeyNames = new string[KeyFields.Count];
                for (int I = 0; I < KeyFields.Count; I++)
                    WebGridView2.DataKeyNames[I] = KeyFields[I];
                 */
                if (TemplateName != "WMasterDetail4" && TemplateName != "VBWebCMasterDetail4" && TemplateName != "WMasterDetail6")
                {
                    FWebDefaultList.Add(aDefault);
                    FWebValidateList.Add(aValidate);
                }
                FComponentChangeService = (IComponentChangeService)FDesignerHost.RootComponent.Site.GetService(typeof(IComponentChangeService));
                FComponentChangeService.OnComponentChanged(WebGridView2, null, "", "M");
            }

            Object aAjaxGridView = FPage.FindControl("AjaxGridViewDetail");
            if (aAjaxGridView != null)
            {
                bool flag = true;
                DataTable srcTable = FWizardDataSet.RealDataSet.Tables[BlockItem.TableName];
                IList iColumns = aAjaxGridView.GetType().GetProperty("Columns").GetValue(aAjaxGridView, null) as IList;
                foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
                {
                    Type columnsType = aAjaxGridView.GetType().GetProperty("Columns").PropertyType.GetProperties()[0].PropertyType;
                    object extCol = Activator.CreateInstance(columnsType);
                    if (BFI.CheckNull == "Y")
                        extCol.GetType().GetProperty("AllowNull").SetValue(extCol, false, null);
                    else
                        extCol.GetType().GetProperty("AllowNull").SetValue(extCol, true, null);
                    extCol.GetType().GetProperty("AllowSort").SetValue(extCol, false, null);
                    extCol.GetType().GetProperty("ColumnName").SetValue(extCol, string.Format("col{0}", BFI.DataField), null);
                    extCol.GetType().GetProperty("DataField").SetValue(extCol, BFI.DataField, null);
                    extCol.GetType().GetProperty("DefaultValue").SetValue(extCol, BFI.DefaultValue, null);
                    extCol.GetType().GetProperty("ExpandColumn").SetValue(extCol, true, null);
                    if (BFI.Description != null && BFI.Description != String.Empty)
                        extCol.GetType().GetProperty("HeaderText").SetValue(extCol, BFI.Description, null);
                    else
                        extCol.GetType().GetProperty("HeaderText").SetValue(extCol, BFI.DataField, null);
                    extCol.GetType().GetProperty("IsKeyField").SetValue(extCol, IsKeyField(BFI.DataField, srcTable.PrimaryKey), null);
                    extCol.GetType().GetProperty("NewLine").SetValue(extCol, flag, null);
                    extCol.GetType().GetProperty("Resizable").SetValue(extCol, true, null);
                    extCol.GetType().GetProperty("TextAlign").SetValue(extCol, "left", null);
                    extCol.GetType().GetProperty("Visible").SetValue(extCol, true, null);
                    extCol.GetType().GetProperty("Width").SetValue(extCol, 75, null);
                    if ((BFI.RefValNo != null && BFI.RefValNo != "") || BFI.RefField != null)
                    {
                        String DataSourceID = GenWebDataSource(BFI, BlockItem.TableName, "RefVal", "", true);
                        String extComboBox = GenExtComboBox(BFI, BlockItem.TableName, "ExtRefVal", "", DataSourceID);
                        extCol.GetType().GetProperty("EditControlId").SetValue(extCol, extComboBox, null);
                        extCol.GetType().GetProperty("Editor").SetValue(extCol, extCol.GetType().GetProperty("Editor").PropertyType.GetField("ComboBox").GetValue(extCol), null);
                    }
                    else if (BFI.ControlType == "ComboBox")
                    {
                        String DataSourceID = GenWebDataSource(BFI, BlockItem.TableName, "ComboBox", "", true);
                        String extComboBox = GenExtComboBox(BFI, BlockItem.TableName, "ExtComboBox", "", DataSourceID);
                        extCol.GetType().GetProperty("EditControlId").SetValue(extCol, extComboBox, null);
                        extCol.GetType().GetProperty("Editor").SetValue(extCol, extCol.GetType().GetProperty("Editor").PropertyType.GetField("ComboBox").GetValue(extCol), null);
                    }
                    this.FieldTypeSelector(BFI.DataType, extCol, BFI.ControlType);
                    iColumns.Add(extCol);
                    flag = !flag;
                }
                NotifyRefresh(200);
                FComponentChangeService.OnComponentChanged(aAjaxGridView, null, "", "M");
            }
#endif
        }

        private void GenDetailBlock_2()
        {
            MWizard.TBlockItem BlockItem = null;
            foreach (TBlockItem B in FClientData.Blocks)
            {
                if (B.wDataSource == null)
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
                Default.ID = "wd" + WzdUtils.RemoveSpecialCharacters(BlockItem.TableName);
                Default.DataSourceID = eDetail.getAttribute("ID", 0).ToString();
                Default.DataMember = BlockItem.TableName;

                WebValidate Validate = new WebValidate();
                Validate.ID = "wv" + WzdUtils.RemoveSpecialCharacters(BlockItem.TableName);
                Validate.DataSourceID = eDetail.getAttribute("ID", 0).ToString();
                Validate.DataMember = BlockItem.TableName;

                WebQueryFiledsCollection QueryFields = new WebQueryFiledsCollection(null, typeof(QueryField));
                WebQueryColumnsCollection QueryColumns = new WebQueryColumnsCollection(null, typeof(QueryColumns));
                foreach (TBlockFieldItem fielditem in BlockItem.BlockFieldItems)
                {
                    GenDefault(fielditem, Default, Validate);
                    GenQuery(fielditem, QueryFields, QueryColumns, BlockItem.TableName);
                }

                WebDevPage.IHTMLElement Page = FDesignerDocument.pageContentElement;
                InsertControl(Page, Default);
                InsertControl(Page, Validate);

                WebDevPage.IHTMLElement Navigator = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("WebNavigator1", 0);
                if (Navigator != null)
                {
                    SetCollectionValue(Navigator, typeof(WebNavigator).GetProperty("QueryFields"), QueryFields);
                }
                WebDevPage.IHTMLElement ClientQuery = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("WebClientQuery1", 0);
                if (ClientQuery != null)
                {
                    SetCollectionValue(ClientQuery, typeof(WebClientQuery).GetProperty("Columns"), QueryColumns);
                }

                FormView = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("wfvDetail", 0);
                RefreshFormView(FormView, BlockItem);
            }
#else
            bool isAjaxPage = false;
            if (FPage.FindControl("AjaxScriptManager1") != null)
                isAjaxPage = true;

            WebDataSource Detail = (WebDataSource)FPage.FindControl("Detail");
            Detail.DataMember = BlockItem.TableName;
            WebFormView WebFormView2 = (WebFormView)FPage.FindControl("wfvDetail");
            if (WebFormView2 == null)
            {
                return;
            }
            //WebFormView2.DataMember = BlockItem.TableName;

            WebDefault aDefault = new WebDefault();
            aDefault.ID = "wd" + BlockItem.TableName;
            aDefault.DataSourceID = Detail.ID;
            aDefault.DataMember = Detail.DataMember;
            WebValidate aValidate = (WebValidate)FPage.FindControl("WebDetailValidate");
            if (aValidate == null)
            {
                aValidate = new WebValidate();
                aValidate.ID = "wv" + BlockItem.TableName;
            }
            aValidate.DataSourceID = Detail.ID;
            aValidate.DataMember = Detail.DataMember;

            IDbConnection conn = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, false);
            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
            //aInfoCommand.Connection = conn;
            String OWNER = String.Empty, SS = this.FClientData.RealTableName, TableName = String.Empty;
            if (SS.Contains("."))
            {
                OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                TableName = SS;
            }
            aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME='" + TableName + "' OR TABLE_NAME='" + OWNER + "." + TableName + "'";
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet dsColdef = new DataSet();
            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsColdef, this.FClientData.TableName);
            FormViewDesigner aDesigner = FDesignerHost.GetDesigner(WebFormView2) as FormViewDesigner;
            Boolean Done = false;
            WebDataSourceDesigner aWebDataSourceDesigner = FDesignerHost.GetDesigner(Detail) as WebDataSourceDesigner;
            if (aWebDataSourceDesigner != null)
                aWebDataSourceDesigner.RefreshSchema(true);

            foreach (TemplateGroup tempGroup in aDesigner.TemplateGroups)
            {
                foreach (TemplateDefinition tempDefin in tempGroup.Templates)
                {
                    if (tempDefin.Name == "EditItemTemplate" || tempDefin.Name == "InsertItemTemplate" || tempDefin.Name == "ItemTemplate")
                    {
                        StringBuilder builder = new StringBuilder();
                        string content = tempDefin.Content;
                        if (content == null || content.Length == 0)
                            continue;

                        string[] ctrlTexts = content.Split("\r\n".ToCharArray());
                        int i = 0;
                        int j = 0;
                        int m = WebFormView2.LayOutColNum * 2;

                        List<string> lists = new List<string>();
                        String ExtraName = "";

                        foreach (TBlockFieldItem aFieldItem in BlockItem.BlockFieldItems)
                        {
                            if (!Done)
                            {
                                GenDefault(aFieldItem, aDefault, aValidate);
                            }

                            lists.Add(aFieldItem.DataField);
                            if ((aFieldItem.RefValNo != null && aFieldItem.RefValNo != "") || aFieldItem.RefField != null)
                            {
                                String DataSourceID = GenWebDataSource(aFieldItem, BlockItem.TableName, "RefVal", "");
                                switch (tempDefin.Name)
                                {
                                    case "EditItemTemplate":
                                        ExtraName = "E";
                                        break;
                                    case "InsertItemTemplate":
                                        ExtraName = "I";
                                        if (aFieldItem.DefaultValue != null && aFieldItem.DefaultValue != "")
                                        {
                                            FormViewField aViewField = new FormViewField();
                                            aViewField.ControlID = "wrv2" + BlockItem.TableName + aFieldItem.DataField + ExtraName;
                                            aViewField.FieldName = aFieldItem.DataField;
                                            WebFormView2.Fields.Add(aViewField);
                                        }
                                        break;
                                    case "ItemTemplate":
                                        ExtraName = "";
                                        break;
                                }

                                DataSet aDataSet = new DataSet();
                                StringBuilder RefColumns = new StringBuilder("<Columns>");
                                aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL_D1 where REFVAL_NO = '{0}'", aFieldItem.RefValNo);
                                WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, aFieldItem.RefValNo);
                                if (aDataSet != null && aDataSet.Tables.Count > 0 && aDataSet.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow DR in aDataSet.Tables[0].Rows)
                                    {
                                        RefColumns.Append(Environment.NewLine);
                                        RefColumns.Append("<InfoLight:WebRefColumn ColumnName=\"" + DR["FIELD_NAME"].ToString() + "\" HeadText=\"" + DR["HEADER_TEXT"].ToString() + "\" Width=\"100\" />");
                                    }
                                    RefColumns.Append(Environment.NewLine);
                                    RefColumns.Append("</Columns>");
                                }
                                else
                                {
                                    RefColumns = new StringBuilder("");
                                }

                                if (tempDefin.Name == "ItemTemplate")
                                {
                                    String FormatStyle = FormatEditMask(aFieldItem.EditMask);

                                    String S6 = String.Empty;
                                    if (isAjaxPage)
                                    {
                                        S6 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                    }
                                    else
                                    {
                                        S6 = String.Format("<InfoLight:WebRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"{1}\"{5}) %>' " +
                                            "ButtonImageUrl=\"../Image/refval/RefVal.gif\" DataBindingField=\"{1}\" DataSourceID=\"{2}\" " +
                                            "DataTextField=\"{3}\" DataValueField=\"{4}\" ReadOnly=\"True\" ResxDataSet=\"\" " +
                                            "ResxFilePath=\"\" UseButtonImage=\"True\" Width=\"130px\" BackColor=\"Transparent\" BorderStyle=\"None\"> " +
                                            RefColumns.ToString() +
                                            "</InfoLight:WebRefVal>",
                                            "wrv2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                            aFieldItem.DataField,
                                            DataSourceID,
                                            FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                            FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                            FormatStyle
                                            );
                                    }
                                    lists.Add(S6);
                                }
                                else
                                {
                                    String FormatStyle = FormatEditMask(aFieldItem.EditMask);
                                    String S1 = String.Empty;
                                    if (isAjaxPage)
                                    {
                                        S1 = String.Format("<AjaxTools:AjaxRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"{1}\"{5}) %>' " +
                                                            "DataSourceID=\"{2}\" " +
                                                            "DataTextField=\"{3}\" DataValueField=\"{4}\" ResxDataSet=\"\">" +
                                                             RefColumns.ToString() +
                                                            "</AjaxTools:AjaxRefVal>",
                                                            "arv2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                            aFieldItem.DataField,
                                                            DataSourceID,
                                                            FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                                            FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                                            FormatStyle
                                                            );
                                    }
                                    else
                                    {
                                        S1 = String.Format("<InfoLight:WebRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"{1}\"{5}) %>' " +
                                            "ButtonImageUrl=\"../Image/refval/RefVal.gif\" DataBindingField=\"{1}\" DataSourceID=\"{2}\" " +
                                            "DataTextField=\"{3}\" DataValueField=\"{4}\" ReadOnly=\"False\" ResxDataSet=\"\" " +
                                            "ResxFilePath=\"\" UseButtonImage=\"True\"> " +
                                             RefColumns.ToString() +
                                            "</InfoLight:WebRefVal>",
                                            "wrv2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                            aFieldItem.DataField,
                                            DataSourceID,
                                            FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                            FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                            FormatStyle
                                            );
                                    }
                                    lists.Add(S1);
                                }
                            }
                            else if (aFieldItem.ControlType == "ComboBox")
                            {
                                String FormatStyle = FormatEditMask(aFieldItem.EditMask);
                                String DataSourceID = GenWebDataSource(aFieldItem, aFieldItem.ComboEntityName, "ComboBox", "");
                                String S5 = "";
                                switch (tempDefin.Name)
                                {
                                    case "EditItemTemplate":
                                        ExtraName = "E";
                                        S5 = String.Format("<InfoLight:WebDropDownList id=\"{0}\" runat=\"server\" DataMember=\"{1}\" DataSourceID=\"{2}\" __designer:wfdid=\"w3\" DataTextField=\"{3}\" Filter DataValueField=\"{4}\" AutoInsertEmptyData=\"False\" SelectedValue='<%# Bind(\"{5}\"{6})%>'  Width=\"130px\"></InfoLight:WebDropDownList>",
                                            "wdd2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                            aFieldItem.ComboEntityName,
                                            DataSourceID,
                                            aFieldItem.ComboTextField,
                                            aFieldItem.ComboValueField,
                                            aFieldItem.DataField,
                                            FormatStyle);
                                        break;
                                    case "InsertItemTemplate":
                                        ExtraName = "I";
                                        S5 = String.Format("<InfoLight:WebDropDownList id=\"{0}\" runat=\"server\" DataMember=\"{1}\" DataSourceID=\"{2}\" __designer:wfdid=\"w3\" DataTextField=\"{3}\" Filter DataValueField=\"{4}\" AutoInsertEmptyData=\"False\" SelectedValue='<%# Bind(\"{5}\"{6}) %>'  Width=\"130px\"></InfoLight:WebDropDownList>",
                                            "wdd2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                            aFieldItem.ComboEntityName,
                                            DataSourceID,
                                            aFieldItem.ComboTextField,
                                            aFieldItem.ComboValueField,
                                            aFieldItem.DataField,
                                            FormatStyle);
                                        if (aFieldItem.DefaultValue != null && aFieldItem.DefaultValue != "")
                                        {
                                            FormViewField aViewField = new FormViewField();
                                            aViewField.ControlID = "wdd2" + BlockItem.TableName + aFieldItem.DataField + ExtraName;
                                            aViewField.FieldName = aFieldItem.DataField;
                                            WebFormView2.Fields.Add(aViewField);
                                        }
                                        break;
                                    case "ItemTemplate":
                                        ExtraName = "";
                                        S5 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l2" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                        break;
                                }
                                lists.Add(S5);
                            }
                            else if (aFieldItem.ControlType == "ValidateBox")
                            {
                                String FormatStyle = FormatEditMask(aFieldItem.EditMask);
                                String S6 = "";
                                switch (tempDefin.Name)
                                {
                                    case "EditItemTemplate":
                                        ExtraName = "E";
                                        S6 = String.Format("<InfoLight:WebValidateBox ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{3}) %>' ValidateField=\"{1}\" WebValidateID=\"{2}\" MaxLength=\"{4}\"></InfoLight:WebValidateBox></td>",
                                            "wvb2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                            aFieldItem.DataField,
                                            aValidate.ID,
                                            FormatStyle,
                                            aFieldItem.Length);
                                        break;
                                    case "InsertItemTemplate":
                                        ExtraName = "I";
                                        if (aFieldItem.DefaultValue != null && aFieldItem.DefaultValue != "")
                                        {
                                            FormViewField aViewField = new FormViewField();
                                            aViewField.ControlID = "wdd2" + BlockItem.TableName + aFieldItem.DataField + ExtraName;
                                            aViewField.FieldName = aFieldItem.DataField;
                                            WebFormView2.Fields.Add(aViewField);
                                        }
                                        S6 = String.Format("<InfoLight:WebValidateBox ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{3}) %>' ValidateField=\"{1}\" WebValidateID=\"{2}\" MaxLength=\"{4}\"></InfoLight:WebValidateBox></td>",
                                            "wvb2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                            aFieldItem.DataField,
                                            aValidate.ID,
                                            FormatStyle,
                                            aFieldItem.Length);
                                        break;
                                    case "ItemTemplate":
                                        ExtraName = "";
                                        S6 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l2" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                        break;
                                }
                                lists.Add(S6);
                            }
                            else if (aFieldItem.ControlType == "CheckBox")
                            {
                                String FormatStyle = FormatEditMask(aFieldItem.EditMask);
                                String S6 = "";
                                switch (tempDefin.Name)
                                {
                                    case "EditItemTemplate":
                                        ExtraName = "E";
                                        S6 = String.Format("<asp:CheckBox ID=\"{0}\" runat=\"server\" Checked='<%# Bind(\"{1}\"{2}) %>'></asp:CheckBox></td>",
                                            "cb2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                            aFieldItem.DataField,
                                            FormatStyle);
                                        break;
                                    case "InsertItemTemplate":
                                        ExtraName = "I";
                                        S6 = String.Format("<asp:CheckBox ID=\"{0}\" runat=\"server\" Checked='<%# Bind(\"{1}\"{2}) %>'></asp:CheckBox></td>",
                                            "wvb2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                            aFieldItem.DataField,
                                            FormatStyle);
                                        break;
                                    case "ItemTemplate":
                                        ExtraName = "";
                                        S6 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l2" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                        break;
                                }
                                lists.Add(S6);
                            }
                            else
                            {
                                if (aFieldItem.DataType == typeof(DateTime) || (aFieldItem.ControlType != null && aFieldItem.ControlType.ToUpper() == "DATETIMEBOX"))
                                {
                                    String DataTimeType = "";
                                    if (aFieldItem.EditMask == "ShortDate")
                                        DataTimeType = "ShortDate";
                                    else if (aFieldItem.EditMask == "LongDate")
                                        DataTimeType = "ShortDate";
                                    else
                                        DataTimeType = "None";

                                    String FormatStyle = FormatEditMask(aFieldItem.EditMask);
                                    String S4 = "";
                                    if (isAjaxPage)
                                    {
                                        switch (tempDefin.Name)
                                        {
                                            case "EditItemTemplate":
                                                ExtraName = "E";
                                                if (aFieldItem.DataType == typeof(DateTime))
                                                    S4 = String.Format("<AjaxTools:AjaxDateTimePicker id=\"{0}\" runat=\"server\" Width=\"130px\" Text='<%# Bind(\"{1}\"{2}) %>' DateFormat=\"" + DataTimeType + "\" DateTimeType=\"DateTime\"></AjaxTools:AjaxDateTimePicker>",
                                                        "wdtp2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                        aFieldItem.DataField,
                                                        FormatStyle);
                                                else if (aFieldItem.DataType == typeof(String) && (aFieldItem.ControlType != null && aFieldItem.ControlType.ToUpper() == "DATETIMEBOX"))
                                                    S4 = String.Format("<AjaxTools:AjaxDateTimePicker id=\"{0}\" runat=\"server\" Width=\"130px\" DateString='<%# Bind(\"{1}\"{2}) %>' DateFormat=\"" + DataTimeType + "\" DateTimeType=\"Varchar\"></AjaxTools:AjaxDateTimePicker>",
                                                        "wdtp2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                        aFieldItem.DataField,
                                                        FormatStyle);
                                                break;
                                            case "InsertItemTemplate":
                                                ExtraName = "I";
                                                if (aFieldItem.DataType == typeof(DateTime))
                                                    S4 = String.Format("<AjaxTools:AjaxDateTimePicker id=\"{0}\" runat=\"server\" Width=\"130px\" Text='<%# Bind(\"{1}\"{2}) %>' DateFormat=\"" + DataTimeType + "\" DateTimeType=\"DateTime\"></AjaxTools:AjaxDateTimePicker>",
                                                        "wdtp2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                        aFieldItem.DataField,
                                                        FormatStyle);
                                                else if (aFieldItem.DataType == typeof(String) && (aFieldItem.ControlType != null && aFieldItem.ControlType.ToUpper() == "DATETIMEBOX"))
                                                    S4 = String.Format("<AjaxTools:AjaxDateTimePicker id=\"{0}\" runat=\"server\" Width=\"130px\" DateString='<%# Bind(\"{1}\"{2}) %>' DateFormat=\"" + DataTimeType + "\" DateTimeType=\"Varchar\"></AjaxTools:AjaxDateTimePicker>",
                                                        "wdtp2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                        aFieldItem.DataField,
                                                        FormatStyle);
                                                break;
                                            case "ItemTemplate":
                                                ExtraName = "";
                                                S4 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (tempDefin.Name)
                                        {
                                            case "EditItemTemplate":
                                                ExtraName = "E";
                                                if (aFieldItem.DataType == typeof(DateTime))
                                                    S4 = String.Format("<InfoLight:WebDateTimePicker id=\"{0}\" runat=\"server\" Width=\"100px\" Text='<%# Bind(\"{1}\"{2}) %>' __designer:wfdid=\"w14\" UseButtonImage=\"True\" DateFormat=\"" + DataTimeType + "\" DateTimeType=\"DateTime\"></InfoLight:WebDateTimePicker>",
                                                        "wdtp2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                        aFieldItem.DataField,
                                                        FormatStyle);
                                                else if (aFieldItem.DataType == typeof(String) && (aFieldItem.ControlType != null && aFieldItem.ControlType.ToUpper() == "DATETIMEBOX"))
                                                    S4 = String.Format("<InfoLight:WebDateTimePicker id=\"{0}\" runat=\"server\" Width=\"100px\" DateString='<%# Bind(\"{1}\"{2}) %>' __designer:wfdid=\"w14\" UseButtonImage=\"True\" DateFormat=\"" + DataTimeType + "\" DateTimeType=\"Varchar\"></InfoLight:WebDateTimePicker>",
                                                        "wdtp2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                        aFieldItem.DataField,
                                                        FormatStyle);
                                                break;
                                            case "InsertItemTemplate":
                                                ExtraName = "I";
                                                if (aFieldItem.DataType == typeof(DateTime))
                                                    S4 = String.Format("<InfoLight:WebDateTimePicker id=\"{0}\" runat=\"server\" Width=\"100px\" Text='<%# Bind(\"{1}\"{2}) %>' __designer:wfdid=\"w14\" UseButtonImage=\"True\" DateFormat=\"" + DataTimeType + "\" DateTimeType=\"DateTime\"></InfoLight:WebDateTimePicker>",
                                                        "wdtp2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                        aFieldItem.DataField,
                                                        FormatStyle);
                                                else if (aFieldItem.DataType == typeof(String) && (aFieldItem.ControlType != null && aFieldItem.ControlType.ToUpper() == "DATETIMEBOX"))
                                                    S4 = String.Format("<InfoLight:WebDateTimePicker id=\"{0}\" runat=\"server\" Width=\"100px\" DateString='<%# Bind(\"{1}\"{2}) %>' __designer:wfdid=\"w14\" UseButtonImage=\"True\" DateFormat=\"" + DataTimeType + "\" DateTimeType=\"Varchar\"></InfoLight:WebDateTimePicker>",
                                                        "wdtp2" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                        aFieldItem.DataField,
                                                        FormatStyle);

                                                break;
                                            case "ItemTemplate":
                                                ExtraName = "";
                                                S4 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l2" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                                break;
                                        }
                                    }
                                    lists.Add(S4);
                                }
                                else
                                {
                                    String FormatStyle = FormatEditMask(aFieldItem.EditMask);

                                    if (tempDefin.Name == "ItemTemplate")
                                    {
                                        String S3 = String.Format("<asp:Label ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>'></asp:Label>", "l2" + aFieldItem.DataField, aFieldItem.DataField, FormatStyle);
                                        lists.Add(S3);
                                    }
                                    else
                                    {
                                        if (tempDefin.Name == "InsertItemTemplate")
                                        {
                                            //if (aFieldItem.DefaultValue != null && aFieldItem.DefaultValue != "")
                                            //{
                                            FormViewField aViewField = new FormViewField();
                                            aViewField.ControlID = "tb2" + aFieldItem.DataField;
                                            aViewField.FieldName = aFieldItem.DataField;
                                            WebFormView2.Fields.Add(aViewField);
                                            //}
                                        }
                                        String S4 = String.Format("<asp:TextBox ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{2}) %>' MaxLength=\"{3}\"></asp:TextBox>",
                                            "tb2" + aFieldItem.DataField,
                                            aFieldItem.DataField,
                                            FormatStyle,
                                            aFieldItem.Length);
                                        lists.Add(S4);
                                    }
                                }
                            }
                        }
                        Done = true;

                        j = j * 2;

                        if (m > 0)
                        {
                            builder.Append("<table>");
                        }

                        foreach (string ctrlText in lists.ToArray())
                        {
                            if (ctrlText == null || ctrlText.Length == 0)
                                continue;

                            if (m > 0)
                            {
                                if (i % m == 0)
                                {
                                    builder.Append("<tr>");
                                }

                                builder.Append("<td>");
                            }
                            // add dd

                            string ddText = "";
                            if (tempDefin.Name != "ItemTemplate")
                            {
                                ddText = GetDDText(ctrlText, BlockItem, tempDefin.Name);
                            }
                            else
                            {
                                ddText = GetDDText(ctrlText, BlockItem, tempDefin.Name);
                            }
                            builder.Append(ddText);
                            builder.Append("\r\n");

                            if (m > 0)
                            {
                                builder.Append("</td>");

                                if (i % m == m - 1)
                                {
                                    builder.Append("</tr>");
                                }
                            }
                            i++;
                        }

                        if (m > 0)
                        {
                            if (i % m != 0)
                            {
                                int n = m - (i % m);
                                int q = 0;
                                while (q < n)
                                {
                                    builder.Append("<td></td>");
                                    q++;
                                }
                                builder.Append("</tr>");
                            }
                            builder.Append("</table>");
                        }

                        tempDefin.Content = builder.ToString();
                    }
                }
            }

            if (!FWebDefaultList.Contains(aDefault))
                FWebDefaultList.Add(aDefault);
            //FWebValidateList.Add(aValidate);
            IComponentChangeService FComponentChangeService = (IComponentChangeService)FDesignerHost.RootComponent.Site.GetService(typeof(IComponentChangeService));
            NotifyRefresh(200);
            FComponentChangeService.OnComponentChanged(WebFormView2, null, "", "M");
            FComponentChangeService.OnComponentChanged(Detail, null, "", "M");
            FComponentChangeService.OnComponentChanged(aValidate, null, "", "M");
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
#if VS90
#else
            String UpdateHTML = "";
            //WebNavigator.QueryFields
            WebNavigator navigator1 = FPage.FindControl("WebNavigator1") as WebNavigator;
            TBlockItem aBlockItem = null;
            TBlockItem bBlockItem = null;
            if (navigator1 != null)
            {
                if (FClientData.IsMasterDetailBaseForm())
                {
                    aBlockItem = FClientData.Blocks.FindItem("Master");
                    foreach (MWizard.TBlockItem var in FClientData.Blocks)
                    {
                        if (var.ParentItemName == aBlockItem.Name)
                            bBlockItem = var;
                    }
                }
                else
                    aBlockItem = FClientData.Blocks.FindItem("Main");

                foreach (WebQueryField QF in navigator1.QueryFields)
                {
                    if (QF.RefVal != null && QF.RefVal != "")
                    {
                        TBlockFieldItem aFieldItem = aBlockItem.BlockFieldItems.FindItem(QF.FieldName);

                        if (QF.Mode == "RefVal")
                        {
                            String DataSourceID = GenWebDataSource(aFieldItem, aBlockItem.TableName, "RefVal", "QF");
                            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
                            aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
                            //aInfoCommand.Connection = FClientData.Owner.GlobalConnection;
                            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                            DataSet aDataSet = new DataSet();
                            aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", aFieldItem.RefValNo);
                            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, aDataSet, aFieldItem.RefValNo);

                            String S1 = String.Format("<InfoLight:WebRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"{1}\") %>' " +
                                "ButtonImageUrl=\"../Image/refval/RefVal.gif\" DataBindingField=\"{1}\" DataSourceID=\"{2}\" " +
                                "DataTextField=\"{3}\" DataValueField=\"{4}\" ReadOnly=\"False\" ResxDataSet=\"\" " +
                                "ResxFilePath=\"\" UseButtonImage=\"True\" Visible=\"False\">" +
                                "</InfoLight:WebRefVal>",
                                "wrv" + aBlockItem.TableName + aFieldItem.DataField + "QF",
                                aFieldItem.DataField,
                                DataSourceID,
                                aDataSet.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                aDataSet.Tables[0].Rows[0]["VALUE_MEMBER"].ToString());
                            UpdateHTML = UpdateHTML + S1;
                        }

                        if (QF.Mode == "ComboBox")
                        {
                            String DataSourceID = GenWebDataSource(aFieldItem, aBlockItem.TableName, "ComboBox", "QF");
                            String S2 = String.Format("<InfoLight:WebRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"{1}\") %>' " +
                                "ButtonImageUrl=\"../Image/refval/RefVal.gif\" DataBindingField=\"{1}\" DataSourceID=\"{2}\" " +
                                "DataTextField=\"{3}\" DataValueField=\"{4}\" ReadOnly=\"False\" ResxDataSet=\"\" " +
                                "ResxFilePath=\"\" UseButtonImage=\"True\" Visible=\"False\"> " +
                                "</InfoLight:WebRefVal>",
                                "wrv" + aBlockItem.TableName + aFieldItem.DataField + "QF",
                                aFieldItem.DataField,
                                DataSourceID,
                                aFieldItem.ComboTextField,
                                aFieldItem.ComboValueField);
                            UpdateHTML = UpdateHTML + S2;
                        }
                    }
                }
            }

            //WebDataSource
            foreach (WebDataSource aWebDataSource in FWebDataSourceList)
            {
                UpdateHTML = UpdateHTML + String.Format(
                    "<InfoLight:WebDataSource ID=\"{0}\" runat=\"server\" SelectAlias=\"{1}\" SelectCommand=\"{2}\" cachedataset=\"{3}\">\n",
                    aWebDataSource.ID, aWebDataSource.SelectAlias, aWebDataSource.SelectCommand, aWebDataSource.CacheDataSet);
                UpdateHTML = UpdateHTML + "</InfoLight:WebDataSource>\n";
            }

            //WebDefault
            if (!FClientData.BaseFormName.Contains("WQuery"))
            {
                foreach (WebDefault aDefault in FWebDefaultList)
                {
                    if (aDefault.Fields.Count > 0)
                    {
                        UpdateHTML = UpdateHTML + String.Format(
                            "<InfoLight:WebDefault ID=\"{0}\" runat=\"server\" DataMember=\"{1}\" DataSourceID=\"{2}\">\n",
                            aDefault.ID, aDefault.DataMember, aDefault.DataSourceID);
                        UpdateHTML = UpdateHTML + "   <Fields>\n";
                    }
                    foreach (DefaultFieldItem DFI in aDefault.Fields)
                    {
                        UpdateHTML = UpdateHTML + String.Format(
                            "      <InfoLight:DefaultFieldItem FieldName=\"{0}\" DefaultValue=\"{1}\" />\n",
                            DFI.FieldName, DFI.DefaultValue);
                    }
                    if (aDefault.Fields.Count > 0)
                    {
                        UpdateHTML = UpdateHTML + "   </Fields>\n";
                        UpdateHTML = UpdateHTML + "</InfoLight:WebDefault>\n";
                    }
                }
            }

            //WebRefVal
            foreach (WebRefVal aWebRefVal in FWebRefValListPage)
            {
                UpdateHTML = UpdateHTML + String.Format(
                    "<InfoLight:WebRefVal ID=\"{0}\" runat=\"server\" Visible=\"{1}\" DataSourceID=\"{2}\" DataTextField=\"{3}\" DataValueField=\"{4}\" Width=\"{5}\" >\n",
                    aWebRefVal.ID, aWebRefVal.Visible, aWebRefVal.DataSourceID, aWebRefVal.DataTextField, aWebRefVal.DataValueField, aWebRefVal.Width);
                UpdateHTML = UpdateHTML + "</InfoLight:WebRefVal>\n";
            }

            //ExtComboBox
            foreach (ExtComboBox aExtComboBox in FExtComboBoxList)
            {
                UpdateHTML = UpdateHTML + String.Format(
                    "<ajaxtools:extcombobox ID=\"{0}\" runat=\"server\" Visible=\"{1}\" DataSourceID=\"{2}\" DisplayField=\"{3}\" ValueField=\"{4}\" AutoRender=\"False\">\n",
                    aExtComboBox.ID, aExtComboBox.Visible, aExtComboBox.DataSourceID, aExtComboBox.DisplayField, aExtComboBox.ValueField);
                UpdateHTML = UpdateHTML + "</ajaxtools:extcombobox>\n";
            }

            //Start Update Process
            System.IO.StreamReader SR = new System.IO.StreamReader(FileName, Encoding.Default);
            String Context = SR.ReadToEnd();
            SR.Close();

            String ReplaceTag = "<InfoLight:WebNavigator ID=";
            if (FClientData.BaseFormName.CompareTo("WSingle1") == 0 || FClientData.BaseFormName.CompareTo("WQuery") == 0 || FClientData.BaseFormName.CompareTo("WSingle0") == 0)
                ReplaceTag = "<InfoLight:WebDataSource ID=\"Master\"";
            if (Context.Contains("<flTools:FLWebNavigator ID="))
                ReplaceTag = "<flTools:FLWebNavigator ID=";
            if (FClientData.BaseFormName.CompareTo("WSingle3") == 0)
                ReplaceTag = "<asp:Button ID=\"ButtonOK\"";
            if (FClientData.BaseFormName.CompareTo("WSingle4") == 0)
                ReplaceTag = "<InfoLight:WebFormView ID=\"WebFormView1\"";
            if (FClientData.BaseFormName.CompareTo("WSingle5") == 0 || FClientData.BaseFormName.CompareTo("WMasterDetail8") == 0)
                ReplaceTag = "<InfoLight:WebStatusStrip ID=\"WebStatusStrip1\"";

            UpdateHTML = UpdateHTML + ReplaceTag;

            Context = Context.Replace(ReplaceTag, UpdateHTML);

            UpdateHTML = String.Empty;
            //WebValidate
            if (!FClientData.BaseFormName.Contains("WQuery"))
            {
                foreach (WebValidate aValidate in FWebValidateList)
                {
                    if (aValidate.Fields.Count > 0)
                    {
                        UpdateHTML = UpdateHTML + String.Format(
                            "<InfoLight:WebValidate ID=\"{0}\" runat=\"server\" DataMember=\"{1}\" DataSourceID=\"{2}\" " +
                            "DuplicateCheck=\"False\" DuplicateCheckMode=\"ByLocal\" ForeColor=\"Red\" ValidateChar=\"*\" " +
                            "ValidateColor=\"Red\" ValidateStyle=\"ShowLable\">\n", aValidate.ID, aValidate.DataMember,
                            aValidate.DataSourceID);
                        UpdateHTML = UpdateHTML + "   <Fields>\n";
                    }
                    foreach (ValidateFieldItem VFI in aValidate.Fields)
                    {
                        String ValidateLabelLink = "";
                        if (FClientData.BaseFormName == "WSingle0" || FClientData.BaseFormName == "WSingle1" || FClientData.BaseFormName == "WSingle2" || FClientData.BaseFormName == "WSingle3" || FClientData.BaseFormName == "WSingle4"
                            || FClientData.BaseFormName == "WSingle5" || FClientData.BaseFormName == "WMasterDetail1" || FClientData.BaseFormName == "WMasterDetail3" || FClientData.BaseFormName == "WMasterDetail4"
                            || FClientData.BaseFormName == "VBWebSingle5" || FClientData.BaseFormName == "WMasterDetail6" || FClientData.BaseFormName == "VBWebCMasterDetail_FG" || FClientData.BaseFormName == "VBWebCMasterDetail4"
                            || FClientData.BaseFormName == "WMasterDetail7" || FClientData.BaseFormName == "WMasterDetail8" || FClientData.BaseFormName == "VBWebCMasterDetail8")
                        {
                            ValidateLabelLink = " ValidateLabelLink=\"Caption" + VFI.FieldName + "\"";
                        }
                        UpdateHTML = UpdateHTML + String.Format(
                            "<InfoLight:ValidateFieldItem FieldName=\"{0}\" CheckNull=\"{1}\"{2}></InfoLight:ValidateFieldItem>\n",
                            VFI.FieldName, VFI.CheckNull.ToString(), ValidateLabelLink);
                    }
                    if (aValidate.Fields.Count > 0)
                    {
                        UpdateHTML = UpdateHTML + "   </Fields>\n";
                        UpdateHTML = UpdateHTML + "</InfoLight:WebValidate>\n<br />";
                    }
                }
            }

            ReplaceTag = "<InfoLight:WebNavigator ID=";
            if (FClientData.BaseFormName.CompareTo("WSingle1") == 0 || FClientData.BaseFormName.CompareTo("WQuery") == 0 || FClientData.BaseFormName.CompareTo("WSingle0") == 0)
                ReplaceTag = "<InfoLight:WebDataSource ID=\"Master\"";
            if (Context.Contains("<flTools:FLWebNavigator ID="))
                ReplaceTag = "<flTools:FLWebNavigator ID=";
            if (FClientData.BaseFormName.CompareTo("WSingle3") == 0)
                ReplaceTag = "<asp:Button ID=\"ButtonOK\"";
            if (FClientData.BaseFormName.CompareTo("WSingle4") == 0)
                ReplaceTag = "<InfoLight:WebFormView ID=\"WebFormView1\"";

            UpdateHTML = UpdateHTML + ReplaceTag;

            Context = Context.Replace(ReplaceTag, UpdateHTML);

            //WebRefVal

            ////Start Update Process
            //String ReplaceTag = "<InfoLight:WebNavigator ID=";
            //if (FClientData.BaseFormName.CompareTo("WSingle1") == 0 || FClientData.BaseFormName.CompareTo("WQuery") == 0)
            //    ReplaceTag = "<InfoLight:WebDataSource ID=";

            ////String ReplaceTag = "<InfoLight:WebNavigator";
            ////if (FClientData.BaseFormName == "WSingle1")
            ////    ReplaceTag = "<InfoLight:WebDataSource";
            //UpdateHTML = UpdateHTML + ReplaceTag;
            //System.IO.StreamReader SR = new System.IO.StreamReader(FileName, Encoding.Default);
            //String Context = SR.ReadToEnd();
            //SR.Close();
            //Context = Context.Replace(ReplaceTag, UpdateHTML);

            ////WebRefVal
            foreach (WebRefVal aWebRefVal in FWebRefValList)
            {
                String EditMask = String.Empty;
                if (aBlockItem != null && aBlockItem.BlockFieldItems.FindItem(aWebRefVal.DataBindingField) != null)
                    EditMask = FormatEditMask(aBlockItem.BlockFieldItems.FindItem(aWebRefVal.DataBindingField).EditMask);
                if (bBlockItem != null && bBlockItem.BlockFieldItems.FindItem(aWebRefVal.DataBindingField) != null)
                    EditMask = FormatEditMask(bBlockItem.BlockFieldItems.FindItem(aWebRefVal.DataBindingField).EditMask);

                String newWebRefValID = aWebRefVal.ID.Remove(aWebRefVal.ID.LastIndexOf("GridView"));
                Context = Context.Replace(" ID=\"" + aWebRefVal.ID + "\"", String.Format("{0}\" BindingValue='<%# Bind(\"{1}\"{2}) %>'",
                    " ID=\"" + newWebRefValID, aWebRefVal.DataBindingField, EditMask));
            }

            ////AjaxRefVal
            foreach (AjaxTools.AjaxRefVal aAjaxRefVal in FAjaxRefValList)
            {
                String EditMask = String.Empty;
                if (aBlockItem != null && aBlockItem.BlockFieldItems.FindItem(aAjaxRefVal.BindingValue) != null)
                    EditMask = FormatEditMask(aBlockItem.BlockFieldItems.FindItem(aAjaxRefVal.BindingValue).EditMask);
                if (bBlockItem != null && bBlockItem.BlockFieldItems.FindItem(aAjaxRefVal.BindingValue) != null)
                    EditMask = FormatEditMask(bBlockItem.BlockFieldItems.FindItem(aAjaxRefVal.BindingValue).EditMask);

                String newWebRefValID = aAjaxRefVal.ID.Remove(aAjaxRefVal.ID.LastIndexOf("GridView"));
                Context = Context.Replace(" ID=\"" + aAjaxRefVal.ID + "\"", String.Format("{0}\" BindingValue='<%# Bind(\"{1}\"{2}) %>'",
                    " ID=\"" + newWebRefValID, aAjaxRefVal.BindingValue, EditMask));
            }

            //WebDropDownList
            foreach (MyWebDropDownList aWebDropDownList in FMyWebDropDownList)
            {
                String EditMask = String.Empty;
                if (aBlockItem != null && aBlockItem.BlockFieldItems.FindItem(aWebDropDownList.BindingField) != null)
                    EditMask = FormatEditMask(aBlockItem.BlockFieldItems.FindItem(aWebDropDownList.BindingField).EditMask);
                if (bBlockItem != null && bBlockItem.BlockFieldItems.FindItem(aWebDropDownList.BindingField) != null)
                    EditMask = FormatEditMask(bBlockItem.BlockFieldItems.FindItem(aWebDropDownList.BindingField).EditMask);

                String newWebDropDownListID = aWebDropDownList.WebDropDownList.ID.Remove(aWebDropDownList.WebDropDownList.ID.LastIndexOf("GridView"));
                Context = Context.Replace(aWebDropDownList.WebDropDownList.ID + "\"", String.Format("{0}\" SelectedValue='<%# Bind(\"{1}\"{2}) %>'",
                    newWebDropDownListID, aWebDropDownList.BindingField, EditMask));
            }

            //WebDateTimePicker
            foreach (WebDateTimePicker aDateTimePicker in FWebDateTimePickerList)
            {
                String SS = "";
                String EditMask = String.Empty;
                if (aBlockItem != null && aBlockItem.BlockFieldItems.FindItem(aDateTimePicker.ToolTip) != null)
                    EditMask = FormatEditMask(aBlockItem.BlockFieldItems.FindItem(aDateTimePicker.ToolTip).EditMask);
                if (bBlockItem != null && bBlockItem.BlockFieldItems.FindItem(aDateTimePicker.ToolTip) != null)
                    EditMask = FormatEditMask(bBlockItem.BlockFieldItems.FindItem(aDateTimePicker.ToolTip).EditMask);

                String newDateTimePickerID = aDateTimePicker.ID.Remove(aDateTimePicker.ID.LastIndexOf("GridView"));
                if (aDateTimePicker.DateTimeType == dateTimeType.DateTime)
                    SS = String.Format(" ID=\"{0}\" Text='<%# Bind(\"{1}\"{2}) %>'",
                                        newDateTimePickerID, aDateTimePicker.ToolTip, EditMask);
                else if (aDateTimePicker.DateTimeType == dateTimeType.VarChar)
                    SS = String.Format(" ID=\"{0}\" DateString='<%# Bind(\"{1}\"{2}) %>'",
                                        newDateTimePickerID, aDateTimePicker.ToolTip, EditMask);
                Context = Context.Replace(" ID=\"" + aDateTimePicker.ID + "\"", SS);
            }

            //WebAjaxDateTimePicker
            foreach (AjaxTools.AjaxDateTimePicker aDateTimePicker in FAjaxDateTimePickerList)
            {
                String SS = "";
                String EditMask = String.Empty;
                if (aBlockItem != null && aBlockItem.BlockFieldItems.FindItem(aDateTimePicker.ToolTip) != null)
                    EditMask = FormatEditMask(aBlockItem.BlockFieldItems.FindItem(aDateTimePicker.ToolTip).EditMask);
                if (bBlockItem != null && bBlockItem.BlockFieldItems.FindItem(aDateTimePicker.ToolTip) != null)
                    EditMask = FormatEditMask(bBlockItem.BlockFieldItems.FindItem(aDateTimePicker.ToolTip).EditMask);

                String newDateTimePickerID = aDateTimePicker.ID.Remove(aDateTimePicker.ID.LastIndexOf("GridView"));
                if (aDateTimePicker.DateTimeType == dateTimeType.DateTime)
                    SS = String.Format(" ID=\"{0}\" Text='<%# Bind(\"{1}\"{2}) %>'",
                                        newDateTimePickerID, aDateTimePicker.ToolTip, EditMask);
                else if (aDateTimePicker.DateTimeType == dateTimeType.VarChar)
                    SS = String.Format(" ID=\"{0}\" DateString='<%# Bind(\"{1}\"{2}) %>'",
                                        newDateTimePickerID, aDateTimePicker.ToolTip, EditMask);
                Context = Context.Replace("Text=\"\" Localize=\"False\" LocalizeForROC=\"False\" ToolTip=\"" + aDateTimePicker.ToolTip + "\" ID=\"" + aDateTimePicker.ID + "\""
                                            , "Localize=\"False\" LocalizeForROC=\"False\" ToolTip=\"" + aDateTimePicker.ToolTip + "\" ID=\"" + aDateTimePicker.ID + "\"");
                Context = Context.Replace(" ID=\"" + aDateTimePicker.ID + "\"", SS);
            }

            //WebValidateBox
            foreach (WebValidateBox aValidateBox in FWebValidateBoxList)
            {
                String EditMask = String.Empty;
                if (aBlockItem != null && aBlockItem.BlockFieldItems.FindItem(aValidateBox.ValidateField) != null)
                    EditMask = FormatEditMask(aBlockItem.BlockFieldItems.FindItem(aValidateBox.ValidateField).EditMask);
                if (bBlockItem != null && bBlockItem.BlockFieldItems.FindItem(aValidateBox.ValidateField) != null)
                    EditMask = FormatEditMask(bBlockItem.BlockFieldItems.FindItem(aValidateBox.ValidateField).EditMask);

                String newValidateBoxID = aValidateBox.ID.Remove(aValidateBox.ID.LastIndexOf("GridView"));
                String SSS = String.Format(" ID=\"{0}\" Text='<%# Bind(\"{1}\"{2}) %>'",
                    newValidateBoxID, aValidateBox.ValidateField, EditMask);
                Context = Context.Replace(" ID=\"" + aValidateBox.ID + "\"", SSS);
            }

            //Label
            foreach (System.Web.UI.WebControls.Label aLabel in FLabelList)
            {
                String EditMask = String.Empty;
                if (aBlockItem != null && aBlockItem.BlockFieldItems.FindItem(aLabel.ToolTip) != null)
                    EditMask = FormatEditMask(aBlockItem.BlockFieldItems.FindItem(aLabel.ToolTip).EditMask);
                if (bBlockItem != null && bBlockItem.BlockFieldItems.FindItem(aLabel.ToolTip) != null)
                    EditMask = FormatEditMask(bBlockItem.BlockFieldItems.FindItem(aLabel.ToolTip).EditMask);

                String newLabelID = aLabel.ID.Remove(aLabel.ID.LastIndexOf("GridView"));
                String S4 = String.Format("{0}\" Text='<%# Bind(\"{1}\"{2}) %>'",
                    newLabelID, aLabel.ToolTip, EditMask);
                Context = Context.Replace(aLabel.ID + "\"", S4);
            }

            //CheckBox
            foreach (System.Web.UI.WebControls.CheckBox aCheckBox in FWebCheckBoxList)
            {
                String newCheckBoxID = aCheckBox.ID.Remove(aCheckBox.ID.LastIndexOf("GridView"));
                String SSS = String.Format(" ID=\"{0}\" Text='<%# Bind(\"{1}\") %>'",
                    newCheckBoxID, aCheckBox.ToolTip);
                Context = Context.Replace(" ID=\"" + aCheckBox.ID + "\"", SSS);
            }

            //TextBox
            foreach (System.Web.UI.WebControls.TextBox aTextBox in FWebTextBoxList)
            {
                String EditMask = String.Empty;
                if (aBlockItem != null && aBlockItem.BlockFieldItems.FindItem(aTextBox.ToolTip) != null)
                    EditMask = FormatEditMask(aBlockItem.BlockFieldItems.FindItem(aTextBox.ToolTip).EditMask);
                if (bBlockItem != null && bBlockItem.BlockFieldItems.FindItem(aTextBox.ToolTip) != null)
                    EditMask = FormatEditMask(bBlockItem.BlockFieldItems.FindItem(aTextBox.ToolTip).EditMask);

                String newTextBoxID = aTextBox.ID.Remove(aTextBox.ID.LastIndexOf("GridView"));
                String SSS = String.Format(" ID=\"{0}\" Text='<%# Bind(\"{1}\"{2}) %>'",
                    newTextBoxID, aTextBox.ToolTip, EditMask);
                Context = Context.Replace(" ID=\"" + aTextBox.ID + "\"", SSS);
            }

            //Page Title
            Context = Context.Replace("<title>Untitled Page</title>", "<title>" + FClientData.FormTitle + "</title>");

            System.IO.FileStream Filefs = new System.IO.FileStream(FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            System.IO.StreamWriter SW = new System.IO.StreamWriter(Filefs, Encoding.Default);
            SW.Write(Context);
            SW.Close();
            Filefs.Close();
#endif
            System.IO.StreamReader SR = new System.IO.StreamReader(FileName, Encoding.Default);
            String Context = SR.ReadToEnd();
            SR.Close();
            Context = Context.Replace("<title>Untitled Page</title>", "<title>" + FClientData.FormTitle + "</title>");
            System.IO.FileStream Filefs = new System.IO.FileStream(FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            System.IO.StreamWriter SW = new System.IO.StreamWriter(Filefs, Encoding.Default);
            SW.Write(Context);
            SW.Close();
            Filefs.Close();

            //FDesignWindow = FPI.Open("{00000000-0000-0000-0000-000000000000}");
            //FDesignWindow.Activate();
            FPI.Name = FClientData.FormName + ".aspx";
            FDesignWindow = FPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
            FDesignWindow.Activate();
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
                    GenDataSet();
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
                        GenDetailBlock_2();
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
                    FDesignWindow.Activate();



                    bool b = FDesignerDocument.execCommand("Refresh", true, "");
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
