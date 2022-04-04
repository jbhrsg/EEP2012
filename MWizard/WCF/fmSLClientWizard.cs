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
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;
using System.Globalization;
using System.Resources;
using System.Text.RegularExpressions;
using mshtml;
using InfoRemoteModule;
using System.Linq;
using MWizard.WCF;
using EFClientTools.EFServerReference;
using EFClientTools.Beans;
using System.Threading;

namespace MWizard
{
    public partial class fmSLClientWizard : Form
    {
        private TSLClientData FClientData;
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

        public fmSLClientWizard()
        {
            InitializeComponent();
            FClientData = new TSLClientData(this);
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

        public fmSLClientWizard(DTE2 aDTE2, AddIn aAddIn)
        {
            InitializeComponent();
            FClientData = new TSLClientData(this);
            FDTE2 = aDTE2;
            FAddIn = aAddIn;
            //PrepareWizardService();
            WzdUtils.FAddIn = FAddIn;
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

        private void PrepareWizardService()
        {
            Show();
            Hide();
        }

        private void ClearValues()
        {
            SDCall = false;
            cbWebSite.Items.Clear();
            cbWebSite.Text = String.Empty;
            tbCurrentSolution.Text = FDTE2.Solution.FileName;
            if (tbCurrentSolution.Text != String.Empty)
            {
                rbCurrentSolution.Enabled = true;
                rbCurrentSolution.Checked = true;
                rbAddToExistSolution.Checked = false;
                tbSolutionName.Text = String.Empty;
                GetWebSite();
            }
            else
            {
                rbCurrentSolution.Enabled = false;
                rbAddToExistSolution.Checked = true;
            }
            tbSolutionName.Text = String.Empty;
            cbAddToExistFolder.Items.Clear();
            cbAddToExistFolder.Text = String.Empty;
            cbTextBoxColumnCount.SelectedIndex = 0;
            tbAddToNewFolder.Text = String.Empty;
            rbAddToRootFolder_CheckedChanged(rbAddToRootFolder, null);
            tbCommandName.Text = String.Empty;
            tbEntityName.Text = String.Empty;
            tbEntitySetName.Text = String.Empty;
            tbRemoteName.Text = String.Empty;
            tbProviderName.Text = String.Empty;
            tbTableName.Text = String.Empty;
            tbTableNameF.Text = String.Empty;
            cbViewProviderName.Text = String.Empty;
            cbViewEntityName.Text = String.Empty;
            tbFormName.Text = "Form1";
            tbDetailTableName.Text = String.Empty;
            cbDetailEntityName.Items.Clear();
            cbDetailEntityName.Text = String.Empty;
            lvViewSrcField.Items.Clear();
            lvViewDesField.Items.Clear();
            lvMasterDesField.Items.Clear();
            lvMasterSrcField.Items.Clear();
            FClientData.Blocks.Clear();
            tvRelation.Nodes.Clear();
            lvSelectedFields.Items.Clear();
        }

        private void Init()
        {
            ClearValues();
            LoadDBString();
            MWizard.WCF.EFAssembly.EFClientToolsAssemblyAdapt.EFClientToolsAssembly = MWizard.WCF.EFAssembly.EFClientToolsAssemblyAdapt.LoadEFClientTools();
            FInfoDataSet = new InfoDataSet();
            if (((FDTE2 != null) && (FDTE2.Solution.FileName != String.Empty)) && File.Exists(FDTE2.Solution.FileName))
            {
                EnabledOutputControls();
            }
            cbEEPAlias.Text = EEPRegistry.WizardConnectionString;
            cbDatabaseType.Text = EEPRegistry.DataBaseType;
            FInfoDataSet.SetWizardDesignMode(true);
            DisplayPage(tbConnection);
        }

        private void LoadDBString()
        {
            try
            {
                cbEEPAlias.Items.Clear();
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
                }

                String strEEPAlias = WzdUtils.GetEEPAlias(FAddIn, false);
                if (String.IsNullOrEmpty(strEEPAlias))
                    cbEEPAlias.SelectedIndex = 0;
                else
                    cbEEPAlias.SelectedText = strEEPAlias;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please setup <DB Manager> of EEPNetServer at first !");
            }
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
            btnPrevious.Enabled = tabControl.SelectedTab != tbConnection;
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
            if ((fmSLClientWizard._serverPath == null) || (fmSLClientWizard._serverPath.Length == 0))
            {
                fmSLClientWizard._serverPath = EEPRegistry.Server + "\\";
            }
            return fmSLClientWizard._serverPath;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'flow7_testDataSet.COLDEF' table. You can move, or remove it, as needed.
            cbAddToExistFolder.Items.Clear();
            foreach (Project P in FDTE2.Solution.Projects)
            {
                if (string.Compare(P.Name, cbWebSite.Text) == 0)
                {
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

        public void ShowSLClientWizard()
        {
            //Show();
            Init();
            ShowDialog();
        }

        public void SDGenWebForm(string XML)
        {
            SDCall = true;
            if (XML != String.Empty)
            {
                FClientData.Blocks.Clear();
                FClientData.LoadFromXML(XML);
            }
            TSLClientGenerator CG = new TSLClientGenerator(FClientData, FDTE2, FAddIn);
            CG.GenWebClientModule();
            SDCall = false;
        }

        private void SetFieldNames(String TableName, ListView LV)
        {
            Dictionary<string, object> htFields = WzdUtils.GetFieldsByEntityName(FClientData.AssemblyName, FClientData.CommandName, tbEntityName.Text);
            List<String> primaryKeys = WzdUtils.GetEntityPrimaryKeys(FClientData.AssemblyName, FClientData.CommandName, tbEntityName.Text);
            //List<string> keyFields = EFClientTools.DesignClientUtility

            //COLDEF
            List<COLDEFInfo> colDefObjects = null;

            colDefObjects = WzdUtils.GetColumnDefination(FClientData.AssemblyName, FClientData.CommandName, TableName, cbEEPAlias.Text);

            foreach (var field in htFields)
            {
                ListViewItem lvi = new ListViewItem();
                COLDEFInfo colDefObject = null;

                lvi.Text = field.Key.ToString();

                if (colDefObjects != null)
                {
                    colDefObject = colDefObjects.Find(c => c.FIELD_NAME == lvi.Text);
                }

                TBlockFieldItem aBlockFieldItem = new TBlockFieldItem();
                aBlockFieldItem.DataField = lvi.Text;
                aBlockFieldItem.DataType = (Type)field.Value;

                if (colDefObject != null)
                {

                    lvi.SubItems.Add(colDefObject.CAPTION);

                    aBlockFieldItem.Description = colDefObject.CAPTION;
                    aBlockFieldItem.CheckNull = colDefObject.CHECK_NULL == null ? null : colDefObject.CHECK_NULL.ToUpper();
                    aBlockFieldItem.DefaultValue = colDefObject.DEFAULT_VALUE;
                    aBlockFieldItem.ControlType = colDefObject.NEEDBOX;
                    aBlockFieldItem.EditMask = colDefObject.EDITMASK;

                    aBlockFieldItem.IsKey = IsPrimaryKey(field.Key.ToString(), primaryKeys);
                    if (aBlockFieldItem.DataType == typeof(DateTime))
                    {
                        if (aBlockFieldItem.ControlType == null || aBlockFieldItem.ControlType == String.Empty)
                            aBlockFieldItem.ControlType = "DateTimeBox";
                    }
                    aBlockFieldItem.QueryMode = colDefObject.QUERYMODE;
                    if (colDefObject.FIELD_LENGTH != null)
                        aBlockFieldItem.Length = colDefObject.FIELD_LENGTH;
                }

                lvi.Tag = aBlockFieldItem;
                LV.Items.Add(lvi);
            }
        }

        private bool IsPrimaryKey(String filedName, List<String> keys)
        {
            foreach (String key in keys)
            {
                if (key == filedName)
                    return true;
            }
            return false;
        }

        private void SetFieldNamesByProvider(String TableName, ListView aListView)
        {
            aListView.Items.Clear();
            try
            {
                Dictionary<string, object> htFields = WzdUtils.GetFieldsByEntityName(FClientData.AssemblyName, FClientData.CommandName, tbEntityName.Text);

                foreach (var field in htFields)
                {
                    ListViewItem aItem = new ListViewItem(field.Key.ToString());
                    DataRow[] DRS = new DataRow[0];// dsColdef.Tables[0].Select("FIELD_NAME='" + Column.ColumnName + "'");
                    TBlockFieldItem aFieldItem = new TBlockFieldItem();
                    if (DRS.Length > 0)
                    {
                        aItem.SubItems.Add(DRS[0]["CAPTION"].ToString());
                        aFieldItem.Description = DRS[0]["CAPTION"].ToString();
                        aFieldItem.QueryMode = DRS[0]["QUERYMODE"].ToString();
                    }
                    else
                    {
                        aItem.SubItems.Add(String.Empty);
                        aFieldItem.Description = String.Empty;
                    }
                    aListView.Items.Add(aItem);
                    aFieldItem.DataField = field.Key.ToString();
                    aFieldItem.DataType = (Type)field.Value;
                    if (DRS.Length > 0 && DRS[0]["CAPTION"] != null)
                        aItem.Tag = aFieldItem;
                }
            }
            finally
            {
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedTab.Equals(tbConnection))
                {
                    WzdUtils.SetRegistryValueByKey("WizardConnectionString", cbEEPAlias.Text);
                    WzdUtils.SetRegistryValueByKey("DatabaseType", cbDatabaseType.Text);
                    DisplayPage(tpOutputSetting);
                }
                else if (tabControl.SelectedTab.Equals(tpOutputSetting))
                {
                    WzdUtils.SetListViewSelect(this.lvTemplate, true, -1);

                    if (cbChooseLanguage.Text == String.Empty || cbChooseLanguage.Text == "C#")
                        FClientData.Language = "cs";
                    else if (cbChooseLanguage.Text == "VB")
                        FClientData.Language = "vb";

                    FClientData.FolderName = String.Empty;
                    if (rbAddToExistSolution.Checked && tbSolutionName.Text == String.Empty)
                    {
                        tbSolutionName.Focus();
                        MessageBox.Show("Please input SolutionName");
                    }
                    else if (cbWebSite.Text == String.Empty)
                    {
                        cbWebSite.Focus();
                        MessageBox.Show("Please select a WebSite");
                    }
                    else if (rbAddToExistFolder.Checked && (cbAddToExistFolder.Text == String.Empty))
                    {
                        cbAddToExistFolder.Focus();
                        MessageBox.Show("Please select a exist folder");
                    }
                    else if (rbCurrentSolution.Checked && (tbCurrentSolution.Text == String.Empty))
                    {
                        MessageBox.Show("The IDE's Solution is empty");
                    }
                    else if (rbAddToNewFolder.Checked && (tbAddToNewFolder.Text == String.Empty))
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
                        tbRemoteName.Text = String.Empty;
                        DisplayPage(tpFormSetting);
                    }
                }
                else if (tabControl.SelectedTab.Equals(tpFormSetting))
                {
                    if (lvTemplate.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Please select EEP Web Templates Form !!");
                        if (lvTemplate.CanFocus)
                        {
                            lvTemplate.Focus();
                        }
                    }
                    else if (tbFormName.Text == String.Empty)
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
                        FClientData.BaseFormName = lvTemplate.SelectedItems[0].SubItems[1].Text;

                        bool detailEntityNameVisible = (FClientData.BaseFormName.CompareTo("SLMasterDetail") == 0) ||
                            (FClientData.BaseFormName.CompareTo("SLMasterDetail2") == 0) ||
                            (FClientData.BaseFormName.CompareTo("SLMasterDetail3") == 0) ||
                            (FClientData.BaseFormName.CompareTo("SL3DCardMasterDetail") == 0) ||
                            (FClientData.BaseFormName.CompareTo("SLFormListMasterDetail") == 0);
                        cbDetailEntityName.Visible = detailEntityNameVisible;
                        lDetailEntityName.Visible = detailEntityNameVisible;

                        bool viewEntityNameVisible = FClientData.BaseFormName.CompareTo("SLMasterDetail3") == 0;
                        cbViewEntityName.Visible = viewEntityNameVisible;
                        lViewEntityName.Visible = viewEntityNameVisible;

                        DisplayPage(tpDataSource);
                    }
                }
                else if (tabControl.SelectedTab.Equals(tpDataSource))
                {
                    ClearAllControls();

                    if (radioButtonEntity.Checked)
                    {
                        if (tbRemoteName.Text == String.Empty)
                        {
                            MessageBox.Show("Please input Provider Name !!");
                            if (tbRemoteName.CanFocus)
                            {
                                tbRemoteName.Focus();
                            }
                        }
                        else if (tbCommandName.Text == String.Empty)
                        {
                            MessageBox.Show("Please input Table Name !!");
                            if (tbCommandName.CanFocus)
                            {
                                tbCommandName.Focus();
                            }
                        }
                        else if (cbDetailEntityName.Visible && cbDetailEntityName.Text == String.Empty)
                        {
                            MessageBox.Show("Please input View Provider Name !!");
                            if (cbDetailEntityName.CanFocus)
                            {
                                cbDetailEntityName.Focus();
                            }
                        }
                        else
                        {
                            FClientData.RemoteName = tbRemoteName.Text;
                            FClientData.CommandName = tbCommandName.Text;
                            FClientData.EntityName = tbEntityName.Text;
                            if (!String.IsNullOrEmpty(cbDetailEntityName.Text) && cbDetailEntityName.Text.Contains(":"))
                            {
                                FClientData.DetailEntityName = cbDetailEntityName.Text.Split(':')[0];
                                FClientData.DetailEntitySetName = cbDetailEntityName.Text.Split(':')[1];
                            }
                            else
                            {
                                FClientData.DetailEntityName = cbDetailEntityName.Text;
                            }
                            FClientData.BaseFormName = lvTemplate.SelectedItems[0].SubItems[1].Text;
                            if (!String.IsNullOrEmpty(cbViewEntityName.Text))
                            {
                                FClientData.ViewProviderName = FClientData.RemoteName.Split('.')[0] + "." + cbViewEntityName.Text;
                            }

                            //2010-06-13 Sjj
                            lvMasterSrcField.Items.Clear();
                            lvMasterDesField.Items.Clear();

                            if (lvMasterSrcField.Items.Count == 0 && lvMasterDesField.Items.Count == 0)
                                SetFieldNames(tbEntitySetName.Text, lvMasterSrcField);
                            if (FClientData.BaseFormName == "SLSingle"
                                || FClientData.BaseFormName == "SL3DCard" || FClientData.BaseFormName == "SL3DCardMasterDetail"
                                || FClientData.BaseFormName == "SLFormList" || FClientData.BaseFormName == "SLFormListMasterDetail"
                                || FClientData.BaseFormName == "SLMasterDetail2" || FClientData.BaseFormName == "SLMasterDetail3")
                            {
                                //2010-06-13 Sjj
                                lvViewSrcField.Items.Clear();

                                if (lvViewSrcField.Items.Count == 0 && lvViewDesField.Items.Count == 0)
                                {
                                    SetFieldNames(tbEntitySetName.Text, lvViewSrcField);
                                }
                                DisplayPage(tpViewFields);
                            }
                            else if (FClientData.BaseFormName == "SLMasterDetail")
                            {
                                ShowTableRelations();
                                DisplayPage(tpMasterFields);
                            }
                            else
                            {
                                DisplayPage(tpMasterFields);
                            }
                        }
                    }
                    else if (radioButtonInfoCommand.Checked)
                    {
                        if (tbProviderName.Text == String.Empty)
                        {
                            MessageBox.Show("Please input Provider Name !!");
                            if (tbProviderName.CanFocus)
                            {
                                tbProviderName.Focus();
                            }
                        }
                        else if (tbTableName.Text == String.Empty)
                        {
                            MessageBox.Show("Please input Table Name !!");
                            if (tbTableName.CanFocus)
                            {
                                tbTableName.Focus();
                            }
                        }
                        else if (cbViewProviderName.Visible && cbViewProviderName.Text == String.Empty)
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
                            FClientData.ViewProviderName = cbViewProviderName.Text;

                            fmEEPWebWizard webWizard = new fmEEPWebWizard();
                            FClientData.BaseFormName = lvTemplate.SelectedItems[0].SubItems[1].Text;
                            if (lvMasterSrcField.Items.Count == 0 && lvMasterDesField.Items.Count == 0)
                                SetFieldNamesInfoCommand(FClientData.RealTableName, lvMasterSrcField);
                            if (FClientData.BaseFormName == "SLSingle"
                                 || FClientData.BaseFormName == "SL3DCard" || FClientData.BaseFormName == "SL3DCardMasterDetail"
                                 || FClientData.BaseFormName == "SLFormList" || FClientData.BaseFormName == "SLFormListMasterDetail"
                                 || FClientData.BaseFormName == "SLMasterDetail2" || FClientData.BaseFormName == "SLMasterDetail3")
                            {
                                lvViewSrcField.Items.Clear();

                                if (lvViewSrcField.Items.Count == 0 && lvViewDesField.Items.Count == 0)
                                {
                                    if (cbViewProviderName.Visible)
                                        SetFieldNamesByProviderInfoCommand(FClientData.RealTableName, FClientData.ViewProviderName, lvViewSrcField);
                                    else
                                        SetFieldNamesInfoCommand(FClientData.RealTableName, lvViewSrcField);
                                }
                                DisplayPage(tpViewFields);
                            }
                            else
                            {
                                DisplayPage(tpMasterFields);
                            }
                        }
                    }
                }
                else if (tabControl.SelectedTab.Equals(tpViewFields))
                {
                    if (FClientData.BaseFormName == "SLMasterDetail2" || FClientData.BaseFormName == "SLMasterDetail3" ||
                        FClientData.BaseFormName == "SL3DCardMasterDetail" || FClientData.BaseFormName == "SLFormListMasterDetail")
                    {
                        ShowTableRelations();
                    }
                    DisplayPage(tpMasterFields);
                }
                else if (tabControl.SelectedTab.Equals(tpMasterFields) && FClientData.IsMasterDetailBaseForm())
                {
                    DisplayPage(tpDetailFields);
                }
                BringToFront();
            }
            catch (Exception ex)
            {
                WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
            }
        }

        private void SetFieldNamesInfoCommand(String TableName, ListView LV)
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
                        if (aBlockFieldItem.ControlType == null || aBlockFieldItem.ControlType == String.Empty)
                            aBlockFieldItem.ControlType = "DateTimeBox";
                    }
                    aBlockFieldItem.QueryMode = DR["QUERYMODE"].ToString();
                    if (DR["FIELD_LENGTH"] != null && DR["FIELD_LENGTH"].ToString() != String.Empty)
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

        private void SetFieldNamesByProviderInfoCommand(String TableName, String ProviderName, ListView aListView)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Equals(tpOutputSetting))
            {
                DisplayPage(tbConnection);
            }
            else if (tabControl.SelectedTab.Equals(tpFormSetting))
            {
                DisplayPage(tpOutputSetting);
            }
            else if (tabControl.SelectedTab.Equals(tpDataSource))
            {
                WzdUtils.SetListViewSelect(this.lvTemplate, true, -1);
                DisplayPage(tpFormSetting);
            }
            else if (tabControl.SelectedTab.Equals(tpViewFields))
            {
                DisplayPage(tpDataSource);
            }
            else if (tabControl.SelectedTab.Equals(tpMasterFields))
            {
                if (FClientData.BaseFormName == "WCFWMasterDetail")
                    DisplayPage(tpViewFields);
                else
                    DisplayPage(tpDataSource);
            }
            else if (tabControl.SelectedTab.Equals(tpDetailFields))
            {
                DisplayPage(tpMasterFields);
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

        private void AddDetailBlockItem(string MasterItemName, System.Windows.Forms.TreeNodeCollection NodeCollection, ListView LV)
        {
            bool isInfoCommand = false;
            for (int I = 0; I < NodeCollection.Count; I++)
            {
                TBlockItem BlockItem = new TBlockItem();
                BlockItem.Name = NodeCollection[I].Text;
                if (radioButtonEntity.Checked)
                {
                    BlockItem.TableName = ((TWCFDetailItem)NodeCollection[I].Tag).EntityName;
                    isInfoCommand = false;
                }
                else if (radioButtonInfoCommand.Checked)
                {
                    BlockItem.TableName = ((TDetailItem)NodeCollection[I].Tag).TableName;
                    BlockItem.RelationName = ((TDetailItem)NodeCollection[I].Tag).Relation.RelationName;
                    isInfoCommand = true;
                }
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
                        BlockFieldItem.IsInfoCommand = isInfoCommand;
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
            FClientData.CommandName = tbCommandName.Text;
            FClientData.EntityName = tbEntityName.Text;
            //FClientData.ViewProviderName = cbDetailEntityName.Text;
            FClientData.ColumnCount = cbTextBoxColumnCount.SelectedIndex;
            TSLClientGenerator Generator = new TSLClientGenerator(FClientData, FDTE2, FAddIn);
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
            try
            {
                ClearAllListViewSort();

                SetValue();
                SetValue_D();

                if (FClientData.IsMasterDetailBaseForm())
                {
                    if (FClientData.BaseFormName == "SL3DCardMasterDetail" || FClientData.BaseFormName == "SLFormListMasterDetail"
                        || FClientData.BaseFormName == "SLMasterDetail2" || FClientData.BaseFormName == "SLMasterDetail3")
                        AddBlockItem("View", FClientData.RemoteName, FClientData.CommandName, lvViewDesField);
                    AddBlockItem("Master", FClientData.RemoteName, FClientData.CommandName, lvMasterDesField);
                    AddDetailBlockItem("Master", tvRelation.Nodes, lvSelectedFields);
                }
                else
                {
                    if (FClientData.BaseFormName == "SLSingle" || FClientData.BaseFormName == "SL3DCard"
                        || FClientData.BaseFormName == "SLFormList")
                        AddBlockItem("View", FClientData.RemoteName, FClientData.CommandName, lvViewDesField);
                    AddBlockItem("Main", FClientData.RemoteName, FClientData.CommandName, lvMasterDesField);
                }
                Hide();
                FDTE2.MainWindow.Activate();
                DoGenClient();
                FInfoDataSet.Dispose();
                FInfoDataSet = null;

                ClearAllControls();
            }
            catch (Exception ex)
            {
                WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
            }
        }

        private void ClearAllControls()
        {
            this.tbCaption.Text = String.Empty;
            this.tbCaption_D.Text = String.Empty;
            this.tbComboEntityName.Text = String.Empty;
            this.tbComboEntityName_D.Text = String.Empty;
            this.tbComboRemoteName.Text = String.Empty;
            this.tbComboRemoteName_D.Text = String.Empty;
            this.tbDefaultValue.Text = String.Empty;
            this.tbDefaultValue_D.Text = String.Empty;
            this.tbEditMask.Text = String.Empty;
            this.tbEditMask_D.Text = String.Empty;
            this.tbOtherFields.Text = String.Empty;
            this.tbOtherFields_D.Text = String.Empty;

            this.cbCheckNull.SelectedIndex = -1;
            this.cbCheckNull_D.SelectedIndex = -1;
            this.cbComboDisplayField_D.SelectedIndex = -1;
            this.cbComboValueField_D.SelectedIndex = -1;
            this.cbControlType.SelectedIndex = -1;
            this.cbControlType_D.SelectedIndex = -1;
            this.cbDataTextField.SelectedIndex = -1;
            this.cbDataValueField.SelectedIndex = -1;
            this.cbQueryMode.SelectedIndex = -1;
            this.cbQueryMode_D.SelectedIndex = -1;
        }

        private void AddBlockItem(string BlockName, string ProviderName, string TableName, ListView LV)
        {
            int I;
            TBlockItem BlockItem = new TBlockItem();
            TBlockFieldItem BlockFieldItem;

            DataSet DS = new DataSet();
            BlockItem.Name = BlockName;
            BlockItem.ProviderName = ProviderName;
            BlockItem.TableName = TableName;

            Boolean isInfoCommand = false;
            if (radioButtonEntity.Checked)
                isInfoCommand = false;
            else if (radioButtonInfoCommand.Checked)
                isInfoCommand = true;
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
                    BlockFieldItem.ComboRemoteName = ((TBlockFieldItem)aItem.Tag).ComboRemoteName;
                    BlockFieldItem.ComboEntityName = ((TBlockFieldItem)aItem.Tag).ComboEntityName;
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
                    BlockFieldItem.IsInfoCommand = isInfoCommand;
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
            FClientData.CommandName = tbCommandName.Text;
            FClientData.EntityName = tbEntityName.Text;
            TSLClientGenerator G = new TSLClientGenerator(FClientData, FDTE2, FAddIn);
            G.GenWebClientModule();
            Close();
        }

        private void GetWebSite()
        {
            cbWebSite.Items.Clear();

            SortedList<String, String> slProjectItems = new SortedList<String, String>();

            foreach (Project P in FDTE2.Solution.Projects)
            {
                if (string.Compare(P.Kind, "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") == 0)
                {
                    slProjectItems.Add(P.Name, P.Name);
                    //cbWebSite.Items.Add(P.Name);
                }
            }

            for (int i = 0; i < slProjectItems.Count; i++ )
            {
                cbWebSite.Items.Add(slProjectItems.Values[i]);
            }

            if (cbWebSite.Items.Count == 1)
            {
                cbWebSite.SelectedIndex = 0;
                cbWebSite_SelectedIndexChanged(new object(), new EventArgs());
            }
            else if (cbWebSite.Items.Count > 0)
            {
                cbWebSite.Text = "SLClient";
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

                if ((Node == null) || (Node.Level == 0))
                {
                    IBS.DataSource = FInfoDataSet;
                    IBS.DataMember = FInfoDataSet.RealDataSet.Tables[0].TableName;
                }
                else
                {
                    TDetailItem item1 = (TDetailItem)Node.Parent.Tag;
                    IBS.DataSource = item1.BindingSource;
                    IBS.DataMember = item1.Relation.RelationName;
                }
                ChildNode = new System.Windows.Forms.TreeNode();
                ChildNode.Text = R.ChildTable.TableName;
                ChildNode.Name = R.ChildTable.TableName;
                Node.Nodes.Add(ChildNode);
                //SetNodeData(R, IBS, ChildNode);
                ShowChildRelation(R.ChildTable.ChildRelations, ChildNode);
            }
        }

        private void ShowTable()
        {
            System.Windows.Forms.TreeNode Node = new System.Windows.Forms.TreeNode();
            Node.Text = FClientData.DetailEntityName;
            Node.Name = FClientData.DetailEntityName;
            tvRelation.Nodes.Add(Node);
            SetNodeData(FClientData.DetailEntityName, Node);
            //ShowChildRelation(R1.ChildTable.ChildRelations, Node);
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
            if (radioButtonEntity.Checked)
            {
                ShowTable();
            }
            else if (radioButtonInfoCommand.Checked)
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

        private void SetNodeData(String tableName, System.Windows.Forms.TreeNode Node)
        {
            TWCFDetailItem DetailItem = new TWCFDetailItem();
            DetailItem.CommandName = tableName;
            DetailItem.EntityName = tableName;
            Node.Tag = DetailItem;
            tvRelation.SelectedNode = Node;
        }

        private void SetNodeData(DataRelation Relation, InfoBindingSource BindingSource, System.Windows.Forms.TreeNode Node)
        {
            TDetailItem DetailItem = new TDetailItem();
            DetailItem.BindingSource = BindingSource;
            DetailItem.Relation = Relation;
            DetailItem.TableName = Relation.ChildTable.TableName;
            FClientData.DetailEntityName = DetailItem.TableName;
            FClientData.DetailEntitySetName = DetailItem.TableName;
            String ModuleName = tbProviderName.Text;
            ModuleName = ModuleName.Substring(0, ModuleName.IndexOf('.'));
            String SolutionName = System.IO.Path.GetFileNameWithoutExtension(FClientData.SolutionName);
            DetailItem.RealTableName = CliUtils.GetTableName(ModuleName, DetailItem.TableName, SolutionName, "", true);
            Node.Tag = DetailItem;
            tvRelation.SelectedNode = Node;
        }

        private void UpdatelvSelectedFields(TWCFDetailItem DetailItem)
        {
            lvSelectedFields.BeginUpdate();
            lvSelectedFields.Items.Clear();
            try
            {
                tbDetailTableName.Text = DetailItem.EntityName;
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

        private void UpdatelvSelectedFields(TDetailItem DetailItem)
        {
            lvSelectedFields.BeginUpdate();
            lvSelectedFields.Items.Clear();
            try
            {
                tbDetailTableName.Text = DetailItem.TableName;
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

        public delegate void GetFieldNamesFunc(string DatabaseName, string TableName, String DataSetName, ListView SrcListView, ListView DestListView);



        public String FindDBType(String aliasName)
        {
            String xmlName = SystemFile.DBFile;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlName);

            XmlNode node = xmlDoc.FirstChild.FirstChild.SelectSingleNode(aliasName);

            string DbType = node.Attributes["Type"].Value.Trim();
            return DbType;
        }

        private void btnDeleteField_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.TreeNode Node = tvRelation.SelectedNode;
                if (radioButtonEntity.Checked)
                {
                    TWCFDetailItem DetailItem = null;
                    if (Node != null)
                    {
                        DetailItem = (TWCFDetailItem)Node.Tag;
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
                else if (radioButtonInfoCommand.Checked)
                {
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

            if (radioButtonEntity.Checked)
                UpdatelvSelectedFields((TWCFDetailItem)Node.Tag);
            else if (radioButtonInfoCommand.Checked)
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

        private void btnRemoteName_Click(object sender, EventArgs e)
        {
            try
            {
                EFAssembly.EFClientToolsAssemblyAdapt.RemoteNameEditorDialog remoteNameEditorDialog = new EFAssembly.EFClientToolsAssemblyAdapt.RemoteNameEditorDialog();

                if (remoteNameEditorDialog.RemoteNameEditorDialogForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tbRemoteName.Text = remoteNameEditorDialog.ReturnValue;
                    tbCommandName.Text = remoteNameEditorDialog.SelectedCommandName;
                    tbEntityName.Text = remoteNameEditorDialog.ReturnClassName;
                    tbEntitySetName.Text = remoteNameEditorDialog.EntitySetName;
                }
            }
            catch (Exception ex)
            {
                WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
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
            cbViewProviderName.SelectedIndex = GetProviderIndex(tbTableNameF.Text);
            FClientData.ViewProviderName = cbViewProviderName.Text;
            ShowTableRelations();
        }

        private int GetProviderIndex(String tableName)
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

            bool flag = false;
            for (int I = 0; I < cbViewProviderName.Items.Count; I++)
            {
                if (cbViewProviderName.Items[I].ToString().IndexOf(FindName + tableName) > -1)
                {
                    Result = I;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                for (int I = 0; I < cbViewProviderName.Items.Count; I++)
                {
                    if (cbViewProviderName.Items[I].ToString().IndexOf(FindName) > -1)
                    {
                        Result = I;
                        flag = true;
                        break;
                    }
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
            foreach (ListViewItem LVI in LV.Items)
            {
                if (LVI.SubItems.Count > 1)
                {
                    ListViewItem.ListViewSubItem LVSI = LVI.SubItems[2];
                    if (LVSI != null)
                    {
                        if (LVSI.Tag != null)
                        {
                            ((System.Windows.Forms.Button)LVSI.Tag).Dispose();
                        }
                    }
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

            fmWCFFieldSetting aForm = new fmWCFFieldSetting(aViewItem.ListView, TWizardType.wtWebPage);
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

        private void tbFormName_TextChanged(object sender, EventArgs e)
        {
            tbFormTitle.Text = tbFormName.Text;
        }

        private void cbViewProviderName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FClientData.DetailEntityName = cbDetailEntityName.Text;
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

        private void btnNewField_Click(object sender, EventArgs e)
        {
            if (radioButtonEntity.Checked)
            {
                System.Windows.Forms.TreeNode Node = tvRelation.SelectedNode;
                if (Node != null)
                {
                    TWCFDetailItem DetailItem = (TWCFDetailItem)Node.Tag;
                    MWizard.fmSelWCFTableField F = new fmSelWCFTableField();
                    //COLDEF
                    List<COLDEFInfo> colDefObjects = null;

                    colDefObjects = WzdUtils.GetColumnDefination(FClientData.AssemblyName, FClientData.CommandName, DetailItem.EntityName, cbEEPAlias.Text);

                    if (F.ShowSelTableFieldForm(DetailItem, lvSelectedFields, RearrangeRefValButton, btnRefVal_Click, colDefObjects, FClientData.AssemblyName, FClientData.CommandName))
                    {
                        btnDeleteField.Enabled = lvSelectedFields.Items.Count > 0;
                    }
                }
            }
            else if (radioButtonInfoCommand.Checked)
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
        }

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

        private void DisplayValue()
        {
            if (FSelectedBlockFieldItem == null)
                return;
            cbControlType.Text = "TextBox";
            tbCaption.Text = FSelectedBlockFieldItem.Description;
            cbCheckNull.Text = FSelectedBlockFieldItem.CheckNull;
            tbDefaultValue.Text = FSelectedBlockFieldItem.DefaultValue;
            cbControlType.Text = FSelectedBlockFieldItem.ControlType;
            tbCommandName.Text = FSelectedBlockFieldItem.ComboEntityName;
            cbDataTextField.Text = FSelectedBlockFieldItem.ComboTextField;
            cbDataValueField.Text = FSelectedBlockFieldItem.ComboValueField;
            cbQueryMode.Text = FSelectedBlockFieldItem.QueryMode;
            tbEditMask.Text = FSelectedBlockFieldItem.EditMask;
            //if (cbCheckNull.Text == "" || cbCheckNull.Text == null)
            //    cbCheckNull.Text = "N";
            if (cbControlType.Text == "" || cbControlType.Text == null)
                cbControlType.Text = "TextBox";
            if (cbQueryMode.Text == null || cbQueryMode.Text == "")
                cbQueryMode.Text = "None";
        }

        private void tbCaption_TextChanged(object sender, EventArgs e)
        {

        }

        private void SetValue()
        {
            if (FSelectedBlockFieldItem == null)
                return;
            FSelectedBlockFieldItem.Description = tbCaption.Text;
            FSelectedBlockFieldItem.CheckNull = cbCheckNull.Text;
            FSelectedBlockFieldItem.DefaultValue = tbDefaultValue.Text;
            FSelectedBlockFieldItem.ControlType = cbControlType.Text;
            FSelectedBlockFieldItem.ComboRemoteName = tbComboRemoteName.Text;
            FSelectedBlockFieldItem.ComboEntityName = tbComboEntityName.Text;
            FSelectedBlockFieldItem.ComboTextField = cbDataTextField.Text;
            FSelectedBlockFieldItem.ComboValueField = cbDataValueField.Text;

            if (!String.IsNullOrEmpty(tbComboRemoteName.Text) && tbComboRemoteName.Text.Contains("."))
            {
                if (radioButtonEntity.Checked)
                {
                    String[] comboRemoteNames = tbComboRemoteName.Text.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    List<COLDEFInfo> colDefObjects = WzdUtils.GetColumnDefination(comboRemoteNames[0], comboRemoteNames[1], tbComboEntityName.Text, cbEEPAlias.Text);
                    if (colDefObjects != null)
                    {
                        COLDEFInfo colDefObject = colDefObjects.Find(c => c.FIELD_NAME == cbDataTextField.Text);
                        if (colDefObject != null)
                            FSelectedBlockFieldItem.ComboTextFieldCaption = colDefObject.CAPTION;

                        colDefObject = colDefObjects.Find(c => c.FIELD_NAME == cbDataValueField.Text);
                        if (colDefObject != null)
                            FSelectedBlockFieldItem.ComboValueFieldCaption = colDefObject.CAPTION;
                    }

                    if (tbOtherFields.Text != null)
                    {
                        String[] otherFields = tbOtherFields.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        FSelectedBlockFieldItem.ComboOtherFields = new List<OtherField>();
                        foreach (String item in otherFields)
                        {
                            if (item != FSelectedBlockFieldItem.ComboTextField && item != FSelectedBlockFieldItem.ComboValueField)
                            {
                                OtherField of = new OtherField();
                                of.FieldName = item;
                                if (colDefObjects != null)
                                {
                                    COLDEFInfo colDefObject = colDefObjects.Find(c => c.FIELD_NAME == item);
                                    if (colDefObject != null)
                                        of.FieldCaption = colDefObject.CAPTION;
                                }
                                FSelectedBlockFieldItem.ComboOtherFields.Add(of);
                            }
                        }
                    }
                    else
                    {

                    }

                    SYS_REFVAL aSYS_REFVAL = new SYS_REFVAL();
                    aSYS_REFVAL.REFVAL_NO = "SL." + FSelectedBlockFieldItem.ComboEntityName;
                    aSYS_REFVAL.TABLE_NAME = FSelectedBlockFieldItem.ComboEntityName;
                    aSYS_REFVAL.SELECT_ALIAS = FSelectedBlockFieldItem.ComboRemoteName;
                    aSYS_REFVAL.DISPLAY_MEMBER = FSelectedBlockFieldItem.ComboTextField;
                    aSYS_REFVAL.VALUE_MEMBER = FSelectedBlockFieldItem.ComboValueField;
                    if (FSelectedBlockFieldItem.ComboOtherFields != null)
                    {
                        foreach (OtherField of in FSelectedBlockFieldItem.ComboOtherFields)
                        {
                            aSYS_REFVAL.SELECT_COMMAND += of.FieldName + ";";
                        }
                    }
                    List<object> lParams = new List<object>();
                    lParams.Add(aSYS_REFVAL);
                    WzdUtils.SaveDataToTable(lParams, "SYS_REFVAL");
                }
                else if (radioButtonInfoCommand.Checked)
                {
                    DataSet dsCOLDEF = GetDataFromCOLDEF(tbComboEntityName.Text);
                    if (dsCOLDEF.Tables.Count > 0 && dsCOLDEF.Tables[0].Rows.Count > 0)
                    {
                        DataRow[] drsDisplay = dsCOLDEF.Tables[0].Select(String.Format("FIELD_NAME='{0}'", cbDataTextField.Text));
                        if (drsDisplay.Count() > 0)
                            FSelectedBlockFieldItem.ComboTextFieldCaption = drsDisplay[0]["CAPTION"].ToString();

                        DataRow[] drsValue = dsCOLDEF.Tables[0].Select(String.Format("FIELD_NAME='{0}'", cbDataValueField.Text));
                        if (drsValue != null)
                            FSelectedBlockFieldItem.ComboValueFieldCaption = drsValue[0]["CAPTION"].ToString();
                    }

                    if (tbOtherFields.Text != null)
                    {
                        String[] otherFields = tbOtherFields.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        FSelectedBlockFieldItem.ComboOtherFields = new List<OtherField>();
                        foreach (String item in otherFields)
                        {
                            if (item != FSelectedBlockFieldItem.ComboTextField && item != FSelectedBlockFieldItem.ComboValueField)
                            {
                                OtherField of = new OtherField();
                                of.FieldName = item;
                                if (dsCOLDEF.Tables.Count > 0 && dsCOLDEF.Tables[0].Rows.Count > 0)
                                {
                                    DataRow[] drs = dsCOLDEF.Tables[0].Select(String.Format("FIELD_NAME='{0}'", item));
                                    if (drs.Count() >0)
                                        of.FieldCaption = drs[0]["CAPTION"].ToString();
                                }
                                FSelectedBlockFieldItem.ComboOtherFields.Add(of);
                            }
                        }
                    }
                    else
                    {

                    }

                    String selectCommand = String.Empty;
                    if (FSelectedBlockFieldItem.ComboOtherFields != null)
                    {
                        foreach (OtherField of in FSelectedBlockFieldItem.ComboOtherFields)
                        {
                            selectCommand += of.FieldName + ";";
                        }
                    }
                    SetDataToSYS_REFVAL(FSelectedBlockFieldItem.ComboEntityName,
                                        FSelectedBlockFieldItem.ComboTextField,
                                        FSelectedBlockFieldItem.ComboValueField,
                                        FSelectedBlockFieldItem.ComboRemoteName,
                                        selectCommand);
                }
            }

            FSelectedBlockFieldItem.QueryMode = cbQueryMode.Text;
            FSelectedBlockFieldItem.EditMask = tbEditMask.Text;

            //FSelectedListViewItem.SubItems[1].Text = FSelectedBlockFieldItem.Description;
            //FSelectedListViewItem.SubItems[2].Text = FSelectedBlockFieldItem.CheckNull;
            //FSelectedListViewItem.SubItems[3].Text = FSelectedBlockFieldItem.DefaultValue;
            //FSelectedListViewItem.SubItems[4].Text = FSelectedBlockFieldItem.RefValNo;
            //FSelectedListViewItem.SubItems[5].Text = FSelectedBlockFieldItem.QueryMode;
            //FSelectedListViewItem.SubItems[6].Text = FSelectedBlockFieldItem.EditMask;
        }

        private void tbTableName_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonEntity.Checked)
            {
                if (tbComboEntityName.Text != String.Empty)
                {
                    Dictionary<string, object> htFields = WzdUtils.GetFieldsByEntityName(FClientData.AssemblyName, FClientData.CommandName, this.tbComboEntityName.Text);
                    cbDataTextField.Items.Clear();
                    cbDataValueField.Items.Clear();
                    foreach (var field in htFields)
                    {
                        cbDataTextField.Items.Add(field.Key.ToString());
                        cbDataValueField.Items.Add(field.Key.ToString());
                    }
                }
                else
                {
                    cbDataTextField.Items.Clear();
                    cbDataValueField.Items.Clear();
                }
            }
            else if (radioButtonInfoCommand.Checked)
            {
                if (tbComboEntityName.Text != String.Empty)
                {
                    InfoDataSet ds = new InfoDataSet();
                    ds.SetWizardDesignMode(true);
                    ds.RemoteName = tbComboRemoteName.Text;
                    ds.ClearWhere();
                    ds.SetWhere("1=0");
                    ds.Active = true;
                    cbDataTextField.Items.Clear();
                    cbDataValueField.Items.Clear();
                    foreach (DataColumn field in ds.GetRealDataSet().Tables[0].Columns)
                    {
                        cbDataTextField.Items.Add(field.ColumnName);
                        cbDataValueField.Items.Add(field.ColumnName);
                    }
                }
                else
                {
                    cbDataTextField.Items.Clear();
                    cbDataValueField.Items.Clear();
                }
            }
        }

        private void cbControlType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbControlType.Text == "ComboBox" || cbControlType.Text == "RefValBox")
                {
                    gbComboBox.Enabled = true;
                    tbComboRemoteName.Text = FSelectedBlockFieldItem.ComboRemoteName;
                    tbComboEntityName.Text = FSelectedBlockFieldItem.ComboEntityName;
                    cbDataTextField.Text = FSelectedBlockFieldItem.ComboTextField;
                    cbDataValueField.Text = FSelectedBlockFieldItem.ComboValueField;
                    if (FSelectedBlockFieldItem.ComboOtherFields != null)
                    {
                        foreach (OtherField of in FSelectedBlockFieldItem.ComboOtherFields)
                        {
                            tbOtherFields.Text += of.FieldName + ";";
                        }
                    }
                }
                else
                {
                    tbComboRemoteName.Text = String.Empty;
                    tbComboEntityName.Text = String.Empty;
                    cbDataTextField.Text = String.Empty;
                    cbDataValueField.Text = String.Empty;
                    tbOtherFields.Text = String.Empty;
                    gbComboBox.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
            }
        }

        private void lvTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ListView).SelectedItems.Count > 0)
            {
                String templateName = (sender as ListView).SelectedItems[0].ToolTipText;
                switch (templateName)
                {
                    case "SLSingle":
                        this.tbDescription.Text = templateName + ": SLDataGrid(View) + SLDataForm(Master)";
                        break;
                    case "SL3DCard":
                        this.tbDescription.Text = templateName + ": SL3DCard(View) + SLDataForm(Master)";
                        break;
                    case "SL3DCardMasterDetail":
                        this.tbDescription.Text = templateName + ": SL3DCard(View) + SLDataForm(Master) + SLDataGrid(Detail)";
                        break;
                    case "SLMasterDetail":
                        this.tbDescription.Text = templateName + ": SLDataForm(Master) + SLDataGrid(Detail)";
                        break;
                    case "SLMasterDetail2":
                        this.tbDescription.Text = templateName + ": SLDataGrid(View) + SLDataForm(Master) + SLDataGrid(Detail)";
                        break;
                    case "SLMasterDetail3":
                        this.tbDescription.Text = templateName + ": SLDataGrid(View) + SLDataForm(Master) + SLDataGrid(Detail)";
                        break;
                    case "SLQuery":
                        this.tbDescription.Text = templateName + ": SLClientQuery(Query) + SLDataGrid(Result)";
                        break;
                    case "SLFormList":
                        this.tbDescription.Text = templateName + ": SLFormList";
                        break;
                    case "SLFormListMasterDetail":
                        this.tbDescription.Text = templateName + ": SLFormListMasterDetail";
                        break;

                }
            }
        }

        private void btnComboRemoteName_Click(object sender, EventArgs e)
        {
            if (radioButtonEntity.Checked)
            {
                EFAssembly.EFClientToolsAssemblyAdapt.RemoteNameEditorDialog remoteNameEditorDialog = new EFAssembly.EFClientToolsAssemblyAdapt.RemoteNameEditorDialog();

                if (remoteNameEditorDialog.RemoteNameEditorDialogForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tbComboRemoteName.Text = remoteNameEditorDialog.ReturnValue;
                    tbComboEntityName.Text = remoteNameEditorDialog.ReturnClassName;

                    List<object> refvals = WzdUtils.GetAllDataByTableName("SYS_REFVAL");
                    for (int i = 0; i < refvals.Count; i++)
                    {
                        if ((refvals[i] as SYS_REFVAL).REFVAL_NO == "SL." + tbComboEntityName.Text &&
                                lvMasterDesField.SelectedItems[0].Text == (refvals[i] as SYS_REFVAL).VALUE_MEMBER)
                        {
                            cbDataTextField.Text = (refvals[i] as SYS_REFVAL).DISPLAY_MEMBER;
                            cbDataValueField.Text = (refvals[i] as SYS_REFVAL).VALUE_MEMBER;
                            tbOtherFields.Text = (refvals[i] as SYS_REFVAL).SELECT_COMMAND;
                            break;
                        }
                    }
                }
            }
            else if (radioButtonInfoCommand.Checked)
            {
                string[] fSelectedList = new string[10];
                string strSelected = "";
                IGetValues aItem = (IGetValues)FInfoDataSet;
                FProviderNameList = aItem.GetValues("RemoteName");
                PERemoteName form = new PERemoteName(FProviderNameList, strSelected);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tbComboRemoteName.Text = form.RemoteName;

                    InfoDataSet ds = new InfoDataSet();
                    ds.SetWizardDesignMode(true);
                    ds.RemoteName = tbComboRemoteName.Text;
                    ds.ClearWhere();
                    ds.SetWhere("1=0");
                    ds.Active = true;
                    String DataSetName = ds.RealDataSet.Tables[0].TableName;
                    String ModuleName = ds.RemoteName.Substring(0, ds.RemoteName.IndexOf('.'));
                    String SolutionName = System.IO.Path.GetFileNameWithoutExtension(FDTE2.Solution.FullName);
                    tbComboEntityName.Text = CliUtils.GetTableName(ModuleName, DataSetName, SolutionName, "", true);


                    DataSet dsSYS_REFVAL = GetDataFromSYS_REFVAL(tbComboEntityName.Text, lvMasterDesField.SelectedItems[0].Text);
                    if (dsSYS_REFVAL.Tables.Count > 0 && dsSYS_REFVAL.Tables[0].Rows.Count > 0)
                    {
                        cbDataTextField.Text = dsSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                        cbDataValueField.Text = dsSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
                        tbOtherFields.Text = dsSYS_REFVAL.Tables[0].Rows[0]["SELECT_COMMAND"].ToString();
                    }
                }
            }
        }

        private DataSet GetDataFromSYS_REFVAL(String tableName, String valueMember)
        {
            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = InternalConnection;
            aInfoCommand.CommandText = String.Format("SELECT * FROM SYS_REFVAL WHERE REFVAL_NO='{0}' AND VALUE_MEMBER='{1}'", "SL." + tableName, valueMember);
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet dsSYS_REFVAL = new DataSet();
            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsSYS_REFVAL, tableName);

            return dsSYS_REFVAL;
        }

        private DataSet GetDataFromCOLDEF(String tableName)
        {
            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = InternalConnection;
            aInfoCommand.CommandText = String.Format("SELECT * FROM COLDEF WHERE TABLE_NAME='{0}'", tableName);
            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            DataSet dsCOLDEF = new DataSet();
            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsCOLDEF, tableName);

            return dsCOLDEF;
        }

        private void SetDataToSYS_REFVAL(String tableName, String displayMember, String valueMember, String selectAlias, String selectCommand)
        {
            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            aInfoCommand.Connection = InternalConnection;
            aInfoCommand.CommandText = String.Format("DELETE FROM SYS_REFVAL WHERE REFVAL_NO='{0}'", "SL." + tableName);
            if (InternalConnection.State != ConnectionState.Open)
                InternalConnection.Open();
            aInfoCommand.ExecuteNonQuery();

            aInfoCommand.CommandText = String.Format("INSERT INTO SYS_REFVAL (REFVAL_NO,TABLE_NAME,DISPLAY_MEMBER,SELECT_ALIAS,SELECT_COMMAND,VALUE_MEMBER) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')",
                                                        "SL." + tableName, tableName, displayMember, selectAlias, selectCommand, valueMember);
            aInfoCommand.ExecuteNonQuery();
        }

        private void tbEntityName_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text != String.Empty)
            {
                cbDetailEntityName.Items.Clear();
                Dictionary<String, String> lDetailEntityNames = WzdUtils.GetDetailEntityClassNameAndEntitySetName((sender as TextBox).Text);

                foreach (var item in lDetailEntityNames)
                {
                    cbDetailEntityName.Items.Add(item.Key + ":" + item.Value);
                }

                if (cbDetailEntityName.Items.Count == 1)
                {
                    cbDetailEntityName.SelectedIndex = 0;
                    cbDetailEntityName.Enabled = false;
                }
                else
                {
                    cbDetailEntityName.Enabled = true;
                }

                cbViewEntityName.Items.Clear();
                if (!String.IsNullOrEmpty(this.tbRemoteName.Text))
                {
                    List<String> lViewCommands = WzdUtils.GetCommandNamesByDataModuleName(this.tbRemoteName.Text.Split('.')[0]);
                    foreach (var item in lViewCommands)
                    {
                        cbViewEntityName.Items.Add(item);
                    }
                    if (cbViewEntityName.Items.Count == 1)
                    {
                        cbViewEntityName.SelectedIndex = 0;
                        cbViewEntityName.Enabled = false;
                    }
                    else
                    {
                        cbViewEntityName.Enabled = true;
                    }
                }
            }
        }

        private void cbControlType_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioButtonEntity.Checked)
            {
                if (cbControlType_D.Text == "ComboBox" || cbControlType_D.Text == "RefValBox")
                {
                    gbDetailCombo.Enabled = true;
                    tbComboRemoteName_D.Text = FSelectedBlockFieldItem_D.ComboRemoteName;
                    tbComboEntityName_D.Text = FSelectedBlockFieldItem_D.ComboEntityName;
                    cbComboDisplayField_D.Text = FSelectedBlockFieldItem_D.ComboTextField;
                    cbComboValueField_D.Text = FSelectedBlockFieldItem_D.ComboValueField;
                    if (FSelectedBlockFieldItem_D.ComboOtherFields != null)
                    {
                        foreach (OtherField of in FSelectedBlockFieldItem_D.ComboOtherFields)
                        {
                            tbOtherFields_D.Text += of.FieldName + ";";
                        }
                    }
                }
                else
                {
                    tbComboRemoteName_D.Text = String.Empty;
                    tbComboEntityName_D.Text = String.Empty;
                    cbComboDisplayField_D.Text = String.Empty;
                    cbComboValueField_D.Text = String.Empty;
                    tbOtherFields_D.Text = String.Empty;
                    gbDetailCombo.Enabled = false;
                }
            }
            else if (radioButtonInfoCommand.Checked)
            {
                if (cbControlType_D.Text == "ComboBox" || cbControlType_D.Text == "RefValBox")
                {
                    gbDetailCombo.Enabled = true;
                }
                else
                {
                    tbComboRemoteName_D.Text = String.Empty;
                    tbComboEntityName_D.Text = String.Empty;
                    cbComboDisplayField_D.Text = String.Empty;
                    cbComboValueField_D.Text = String.Empty;
                    tbOtherFields_D.Text = String.Empty;
                    gbDetailCombo.Enabled = false;
                }
            }
        }

        private void btnComboRemoteName_D_Click(object sender, EventArgs e)
        {
            if (radioButtonEntity.Checked)
            {
                EFAssembly.EFClientToolsAssemblyAdapt.RemoteNameEditorDialog remoteNameEditorDialog = new EFAssembly.EFClientToolsAssemblyAdapt.RemoteNameEditorDialog();

                if (remoteNameEditorDialog.RemoteNameEditorDialogForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tbComboRemoteName_D.Text = remoteNameEditorDialog.ReturnValue;
                    tbComboEntityName_D.Text = remoteNameEditorDialog.ReturnClassName;

                    List<object> refvals = WzdUtils.GetAllDataByTableName("SYS_REFVAL");
                    for (int i = 0; i < refvals.Count; i++)
                    {
                        if ((refvals[i] as SYS_REFVAL).REFVAL_NO == "SL." + tbComboEntityName_D.Text &&
                                lvSelectedFields.SelectedItems[0].Text == (refvals[i] as SYS_REFVAL).VALUE_MEMBER)
                        {
                            cbComboDisplayField_D.Text = (refvals[i] as SYS_REFVAL).DISPLAY_MEMBER;
                            cbComboValueField_D.Text = (refvals[i] as SYS_REFVAL).VALUE_MEMBER;
                            tbOtherFields_D.Text = (refvals[i] as SYS_REFVAL).SELECT_COMMAND;
                            break;
                        }
                    }
                }
            }
            else if (radioButtonInfoCommand.Checked)
            {
                string[] fSelectedList = new string[10];
                string strSelected = "";
                IGetValues aItem = (IGetValues)FInfoDataSet;
                FProviderNameList = aItem.GetValues("RemoteName");
                PERemoteName form = new PERemoteName(FProviderNameList, strSelected);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tbComboRemoteName_D.Text = form.RemoteName;

                    InfoDataSet ds = new InfoDataSet();
                    ds.SetWizardDesignMode(true);
                    ds.RemoteName = tbComboRemoteName_D.Text;
                    ds.ClearWhere();
                    ds.SetWhere("1=0");
                    ds.Active = true;
                    String DataSetName = ds.RealDataSet.Tables[0].TableName;
                    String ModuleName = ds.RemoteName.Substring(0, ds.RemoteName.IndexOf('.'));
                    String SolutionName = System.IO.Path.GetFileNameWithoutExtension(FDTE2.Solution.FullName);
                    tbComboEntityName_D.Text = CliUtils.GetTableName(ModuleName, DataSetName, SolutionName, "", true);

                    DataSet dsSYS_REFVAL = GetDataFromSYS_REFVAL(tbComboEntityName_D.Text, lvSelectedFields.SelectedItems[0].Text);
                    if (dsSYS_REFVAL.Tables.Count > 0 && dsSYS_REFVAL.Tables[0].Rows.Count > 0)
                    {
                        cbComboDisplayField_D.Text = dsSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                        cbComboValueField_D.Text = dsSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
                        tbOtherFields_D.Text = dsSYS_REFVAL.Tables[0].Rows[0]["SELECT_COMMAND"].ToString();
                    }
                }
            }
        }

        private void tbComboEntityName_D_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonEntity.Checked)
            {
                if (tbComboEntityName_D.Text != String.Empty)
                {
                    Dictionary<string, object> htFields = WzdUtils.GetFieldsByEntityName(FClientData.AssemblyName, FClientData.CommandName, this.tbComboEntityName_D.Text);
                    cbComboDisplayField_D.Items.Clear();
                    cbComboValueField_D.Items.Clear();
                    foreach (var field in htFields)
                    {
                        cbComboDisplayField_D.Items.Add(field.Key.ToString());
                        cbComboValueField_D.Items.Add(field.Key.ToString());
                    }
                }
                else
                {
                    cbComboDisplayField_D.Items.Clear();
                    cbComboValueField_D.Items.Clear();
                }
            }
            else if (radioButtonInfoCommand.Checked)
            {
                if (tbComboEntityName_D.Text != String.Empty)
                {
                    InfoDataSet ds = new InfoDataSet();
                    ds.SetWizardDesignMode(true);
                    ds.RemoteName = tbComboRemoteName_D.Text;
                    ds.ClearWhere();
                    ds.SetWhere("1=0");
                    ds.Active = true;
                    cbComboDisplayField_D.Items.Clear();
                    cbComboValueField_D.Items.Clear();
                    foreach (DataColumn field in ds.GetRealDataSet().Tables[0].Columns)
                    {
                        cbComboDisplayField_D.Items.Add(field.ColumnName);
                        cbComboValueField_D.Items.Add(field.ColumnName);
                    }
                }
                else
                {
                    cbComboDisplayField_D.Items.Clear();
                    cbComboValueField_D.Items.Clear();
                }
            }
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
            cbControlType_D.Text = "TextBox";
            tbCaption_D.Text = FSelectedBlockFieldItem_D.Description;
            cbCheckNull_D.Text = FSelectedBlockFieldItem_D.CheckNull;
            tbDefaultValue_D.Text = FSelectedBlockFieldItem_D.DefaultValue;
            cbControlType_D.Text = FSelectedBlockFieldItem_D.ControlType;
            tbComboEntityName_D.Text = FSelectedBlockFieldItem_D.ComboEntityName;
            cbComboDisplayField_D.Text = FSelectedBlockFieldItem_D.ComboTextField;
            cbComboValueField_D.Text = FSelectedBlockFieldItem_D.ComboValueField;
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
            FSelectedBlockFieldItem_D.ComboRemoteName = tbComboRemoteName_D.Text;
            FSelectedBlockFieldItem_D.ComboEntityName = tbComboEntityName_D.Text;
            FSelectedBlockFieldItem_D.ComboTextField = cbComboDisplayField_D.Text;
            FSelectedBlockFieldItem_D.ComboValueField = cbComboValueField_D.Text;

            if (!String.IsNullOrEmpty(tbComboRemoteName_D.Text) && tbComboRemoteName_D.Text.Contains("."))
            {
                if (radioButtonEntity.Checked)
                {
                    String[] combo_DRemoteNames = tbComboRemoteName_D.Text.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    List<COLDEFInfo> colDefObjects = WzdUtils.GetColumnDefination(combo_DRemoteNames[0], combo_DRemoteNames[1], this.tbComboEntityName_D.Text, cbEEPAlias.Text);
                    if (colDefObjects != null)
                    {
                        COLDEFInfo colDefObject = colDefObjects.Find(c => c.FIELD_NAME == cbComboDisplayField_D.Text);
                        if (colDefObject != null)
                            FSelectedBlockFieldItem_D.ComboTextFieldCaption = colDefObject.CAPTION;

                        colDefObject = colDefObjects.Find(c => c.FIELD_NAME == cbComboValueField_D.Text);
                        if (colDefObject != null)
                            FSelectedBlockFieldItem_D.ComboValueFieldCaption = colDefObject.CAPTION;
                    }

                    if (tbOtherFields_D.Text != null)
                    {
                        String[] otherFields = tbOtherFields_D.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        FSelectedBlockFieldItem_D.ComboOtherFields = new List<OtherField>();
                        foreach (String item in otherFields)
                        {
                            if (item != FSelectedBlockFieldItem_D.ComboTextField && item != FSelectedBlockFieldItem_D.ComboValueField)
                            {
                                OtherField of = new OtherField();
                                of.FieldName = item;
                                if (colDefObjects != null)
                                {
                                    COLDEFInfo colDefObject = colDefObjects.Find(c => c.FIELD_NAME == item);
                                    if (colDefObject != null)
                                        of.FieldCaption = colDefObject.CAPTION;
                                }
                                FSelectedBlockFieldItem_D.ComboOtherFields.Add(of);
                            }
                        }
                    }

                    SYS_REFVAL aSYS_REFVAL = new SYS_REFVAL();
                    aSYS_REFVAL.REFVAL_NO = "SL." + FSelectedBlockFieldItem_D.ComboEntityName;
                    aSYS_REFVAL.TABLE_NAME = FSelectedBlockFieldItem_D.ComboEntityName;
                    aSYS_REFVAL.SELECT_ALIAS = FSelectedBlockFieldItem_D.ComboRemoteName;
                    aSYS_REFVAL.DISPLAY_MEMBER = FSelectedBlockFieldItem_D.ComboTextField;
                    aSYS_REFVAL.VALUE_MEMBER = FSelectedBlockFieldItem_D.ComboValueField;
                    if (FSelectedBlockFieldItem_D.ComboOtherFields != null)
                    {
                        foreach (OtherField of in FSelectedBlockFieldItem_D.ComboOtherFields)
                        {
                            aSYS_REFVAL.SELECT_COMMAND += of.FieldName + ";";
                        }
                    }
                    List<object> lParams = new List<object>();
                    lParams.Add(aSYS_REFVAL);
                    WzdUtils.SaveDataToTable(lParams, "SYS_REFVAL");
                }
                else if (radioButtonInfoCommand.Checked)
                {
                    DataSet dsCOLDEF = GetDataFromCOLDEF(tbComboEntityName_D.Text);
                    if (dsCOLDEF.Tables.Count > 0 && dsCOLDEF.Tables[0].Rows.Count > 0)
                    {
                        DataRow[] drsDisplay = dsCOLDEF.Tables[0].Select(String.Format("FIELD_NAME='{0}'", cbComboDisplayField_D.Text));
                        if (drsDisplay.Count() > 0)
                            FSelectedBlockFieldItem_D.ComboTextFieldCaption = drsDisplay[0]["CAPTION"].ToString();

                        DataRow[] drsValue = dsCOLDEF.Tables[0].Select(String.Format("FIELD_NAME='{0}'", cbComboDisplayField_D.Text));
                        if (drsValue != null)
                            FSelectedBlockFieldItem_D.ComboValueFieldCaption = drsValue[0]["CAPTION"].ToString();
                    }

                    if (tbOtherFields_D.Text != null)
                    {
                        String[] otherFields = tbOtherFields_D.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        FSelectedBlockFieldItem_D.ComboOtherFields = new List<OtherField>();
                        foreach (String item in otherFields)
                        {
                            if (item != FSelectedBlockFieldItem_D.ComboTextField && item != FSelectedBlockFieldItem_D.ComboValueField)
                            {
                                OtherField of = new OtherField();
                                of.FieldName = item;
                                if (dsCOLDEF.Tables.Count > 0 && dsCOLDEF.Tables[0].Rows.Count > 0)
                                {
                                    DataRow[] drs = dsCOLDEF.Tables[0].Select(String.Format("FIELD_NAME='{0}'", item));
                                    if (drs.Count() > 0)
                                        of.FieldCaption = drs[0]["CAPTION"].ToString();
                                }
                                FSelectedBlockFieldItem_D.ComboOtherFields.Add(of);
                            }
                        }
                    }
                    else
                    {

                    }

                    String selectCommand = String.Empty;
                    if (FSelectedBlockFieldItem_D.ComboOtherFields != null)
                    {
                        foreach (OtherField of in FSelectedBlockFieldItem_D.ComboOtherFields)
                        {
                            selectCommand += of.FieldName + ";";
                        }
                    }
                    SetDataToSYS_REFVAL(FSelectedBlockFieldItem_D.ComboEntityName,
                                        FSelectedBlockFieldItem_D.ComboTextField,
                                        FSelectedBlockFieldItem_D.ComboValueField,
                                        FSelectedBlockFieldItem_D.ComboRemoteName,
                                        selectCommand);
                }
            }

            FSelectedBlockFieldItem_D.QueryMode = cbQueryMode_D.Text;
            FSelectedBlockFieldItem_D.EditMask = tbEditMask_D.Text;

            //FSelectedListViewItem.SubItems[1].Text = FSelectedBlockFieldItem.Description;
            //FSelectedListViewItem.SubItems[2].Text = FSelectedBlockFieldItem.CheckNull;
            //FSelectedListViewItem.SubItems[3].Text = FSelectedBlockFieldItem.DefaultValue;
            //FSelectedListViewItem.SubItems[4].Text = FSelectedBlockFieldItem.RefValNo;
            //FSelectedListViewItem.SubItems[5].Text = FSelectedBlockFieldItem.QueryMode;
            //FSelectedListViewItem.SubItems[6].Text = FSelectedBlockFieldItem.EditMask;
        }

        private void cbDataTextField_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOtherFields_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.tbComboRemoteName.Text))
            {
                fmOtherFields afmOtherFields = new fmOtherFields(this.cbDataValueField.Items, this.tbOtherFields.Text);
                if (afmOtherFields.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.tbOtherFields.Text = afmOtherFields.strCheckedItems;
                }
            }
            else
            {
                MessageBox.Show("Please select ComboRemoteName first!");
            }
        }

        private void btnOtherFields_D_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.tbComboRemoteName_D.Text))
            {
                fmOtherFields afmOtherFields = new fmOtherFields(this.cbComboValueField_D.Items, this.tbOtherFields_D.Text);
                if (afmOtherFields.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.tbOtherFields_D.Text = afmOtherFields.strCheckedItems;
                }
            }
            else
            {
                MessageBox.Show("Please select ComboRemoteName first!");
            }
        }

        private void cbEEPAlias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = FindDBType(cbEEPAlias.Text);
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
                    InternalConnection.Open();
                }
                catch (Exception E)
                {
                    MessageBox.Show(string.Format("Database ConnnectionString information error, please reset ConnectionString.\nThe error message:\n{0}", E.Message));
                }
            }
        }

        private void radioButtonInfoCommand_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                this.panelInfoCommand.Visible = true;
            }
        }

        private void radioButtonEntity_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                this.panelInfoCommand.Visible = false;
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
    }

    public class TSLClientData : Object
    {
        private string FPackageName, FBaseFormName, FServerPackageName, FFolderName, FCommandName, FEntityName, FFormName, FRemoteName,
                       FDatabaseName, FSolutionName, FViewProviderName, FWebSiteName, FFolderMode, FFormTitle, FDetailEntityName, FDetailEntitySetName,
                       FFullXamlName, FProviderName, FTableName, FRealTableName;
        private TBlockItems FBlocks;
        private MWizard.fmSLClientWizard FOwner;
        private bool FNewSolution = false;
        private string FCodeFolderName;
        private int FColumnCount;
        private ClientType FDatabaseType;
        private String FConnString;
        private String FLanguage = "cs";

        public TSLClientData(MWizard.fmSLClientWizard Owner)
        {
            FOwner = Owner;
            FBlocks = new TBlockItems(this);
        }

        public ClientType DatabaseType
        {
            get { return FDatabaseType; }
            set { FDatabaseType = value; }
        }

        public fmSLClientWizard Owner
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

        public String FullXamlName
        {
            get { return FFullXamlName; }
            set { FFullXamlName = value; }
        }

        public String ProviderName
        {
            get { return FProviderName; }
            set { FProviderName = value; }
        }

        public String TableName
        {
            get { return FTableName; }
            set { FTableName = value; }
        }

        public String RealTableName
        {
            get { return FRealTableName; }
            set { FRealTableName = value; }
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
            CommandName = Node.Attributes["TableName"].Value;
            FormTitle = Node.Attributes["FormName"].Value;
            RemoteName = Node.Attributes["ProviderName"].Value;
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
            if (string.Compare(FBaseFormName, "SLMasterDetail") == 0 ||
                string.Compare(FBaseFormName, "SLMasterDetail2") == 0 ||
                string.Compare(FBaseFormName, "SLMasterDetail3") == 0 ||
                string.Compare(FBaseFormName, "SL3DCardMasterDetail") == 0 ||
                string.Compare(FBaseFormName, "SLFormListMasterDetail") == 0)
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

        public string DetailEntityName
        {
            get
            {
                return FDetailEntityName;
            }
            set
            {
                FDetailEntityName = value;
            }
        }

        public string DetailEntitySetName
        {
            get
            {
                return FDetailEntitySetName;
            }
            set
            {
                FDetailEntitySetName = value;
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

        public string CommandName
        {
            get
            {
                return FCommandName;
            }
            set
            {
                FCommandName = value;
            }
        }

        public string EntityName
        {
            get
            {
                return FEntityName;
            }
            set
            {
                FEntityName = value;
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

        public string RemoteName
        {
            get
            {
                return FRemoteName;
            }
            set
            {
                FRemoteName = value;
            }
        }

        public string AssemblyName
        {
            get { return RemoteName.Split('.')[0]; }
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

    partial class TSLClientGenerator : System.ComponentModel.Component
    {
        private TSLClientData FClientData;
        private DTE2 FDTE2;
        private AddIn FAddIn;
        private System.Windows.Forms.Form FRootForm = null;
        private System.ComponentModel.Design.IDesignerHost FDesignerHost;
        private InfoDataSet FDataSet = null;
        private ProjectItem FPI;
        private Project FProject = null;
        private InfoDataGridView FViewGrid = null;
        private InfoDataSet FWizardDataSet = null;
        private DataSet FSYS_REFVAL;
        private List<EFClientTools.Web.EFDataSource> FEFDataSourceList;
        private List<String> FSLComboDataSource;
        private List<AjaxTools.AjaxRefVal> FAjaxRefValList;
        private List<WebRefVal> FWebRefValListPage;
        private List<WebDefault> FWebDefaultList;
        private List<WebValidate> FWebValidateList;
        private List<ExtTools.ExtComboBox> FExtComboBoxList;
        private List<MyWebDropDownList> FMyWebDropDownList;
        private List<WebDateTimePicker> FWebDateTimePickerList;
        private List<AjaxTools.AjaxDateTimePicker> FAjaxDateTimePickerList;
        private List<WebValidateBox> FWebValidateBoxList;
        private List<System.Web.UI.WebControls.CheckBox> FWebCheckBoxList;
        private List<System.Web.UI.WebControls.TextBox> FWebTextBoxList;
        private List<System.Web.UI.WebControls.Label> FLabelList;

        public TSLClientGenerator(TSLClientData ClientData, DTE2 dte2, AddIn aAddIn)
        {
            FClientData = ClientData;
            FDTE2 = dte2;
            FAddIn = aAddIn;
            FSYS_REFVAL = new DataSet();
            FEFDataSourceList = new List<EFClientTools.Web.EFDataSource>();
            FSLComboDataSource = new List<String>();
            FAjaxRefValList = new List<AjaxTools.AjaxRefVal>();
            FWebRefValListPage = new List<WebRefVal>();
            FWebDefaultList = new List<WebDefault>();
            FWebValidateList = new List<WebValidate>();
            FExtComboBoxList = new List<ExtTools.ExtComboBox>();
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
            //Context = Context.Replace("TAB_BASEFORMNAME", FClientData.BaseFormName);
            Context = Context.Replace("TAB_FOLDERNAME", FClientData.FolderName);
            Context = Context.Replace("TAB_CLASSNAME_" + FClientData.BaseFormName, FClientData.FormName);
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
                if (String.Compare(P.Name, "SLClient") == 0)
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
            TemplatePath = FProject.FullName.Remove(FProject.FullName.LastIndexOf('\\')) + "\\Template";
            if (TemplatePath == "")
            {
                MessageBox.Show("Cannot find WebTemplate path: {0}", TemplatePath);
                return false;
            }
            if (FPI != null)
            {
                foreach (ProjectItem aPI in FPI.ProjectItems)
                {
                    if (string.Compare(FClientData.FormName + ".xaml", aPI.Name) == 0
                        || string.Compare(FClientData.FormName + ".xaml.cs", aPI.Name) == 0)
                    {
                        DialogResult dr = MessageBox.Show("There is another File which name is " + FClientData.PackageName + " existed! Do you want to delete it first", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            string Path = aPI.get_FileNames(0);
                            aPI.Name = Guid.NewGuid().ToString();
                            aPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
                            aPI.Delete();
                            File.Delete(Path);
                        }
                        else if (File.Exists(aPI.get_FileNames(0)))
                        {
                            File.Delete(aPI.get_FileNames(0));
                        }
                        else
                        {
                            return false;
                        }

                        continue;
                    }
                    if (string.Compare(FClientData.BaseFormName + ".xaml", aPI.Name) == 0)
                    {
                        string Path = aPI.get_FileNames(0);

                        aPI.Name = Guid.NewGuid().ToString();
                        aPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
                        aPI.Delete();
                        File.Delete(Path);
                    }
                }

                FPI = FPI.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + FClientData.BaseFormName + ".xaml");
                FPI.Name = FClientData.FormName + ".xaml";
                FClientData.FullXamlName = FPI.Properties.Item(18).Value.ToString();
                RenameNameSpace(FPI.Properties.Item(18).Value.ToString());
                ProjectItem P1 = FPI.ProjectItems.AddFromFileCopy(TemplatePath + "\\" + FClientData.BaseFormName + ".xaml.cs");
                P1.Name = FClientData.FormName + ".xaml.cs";
                RenameNameSpace(P1.Properties.Item(18).Value.ToString());
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
            }

            return true;
        }

        private void GetDesignerHost()
        {
            //FDesignWindow = FPI.Open("{00000000-0000-0000-0000-000000000000}");
            //FDesignWindow.Activate();

            //FDesignWindow = FPI.Open("{7651A703-06E5-11D1-8EBD-00A0C90F26EA}");
            //FDesignWindow.Activate();

            //HTMLWindow W = (HTMLWindow)FDesignWindow.Object;

            //W.CurrentTab = vsHTMLTabs.vsHTMLTabsDesign;
            //if (W.CurrentTabObject is WebDevPage.DesignerDocument)
            //{
            //    FDesignerDocument = W.CurrentTabObject as WebDevPage.DesignerDocument;
            //}
        }

        private void GenViewBlockControl(TBlockItem BlockItem, int columnCount)
        {
            String strGridViewColumns = ComposeGridViewXML(BlockItem.BlockFieldItems);
            WriteToXaml(FClientData.FullXamlName,
                        "<SLTools:SLDataGrid.Columns><data:DataGridTextColumn Header=\"gvViewDataGridColumns\" /></SLTools:SLDataGrid.Columns>",
                        strGridViewColumns);

            BlockItem.wDataSource = new WebDataSource();
            String strReadOnlyTemplate = ComposeViewFormViewXML(BlockItem.BlockFieldItems, "ReadOnlyTemplate", columnCount);
            WriteToXaml(FClientData.FullXamlName, "<Grid><toolkit:DataField x:Name=\"viewMasterReadOnlyTemplate\" /></Grid>", strReadOnlyTemplate);

            String strSLCardToolTip = ComposeSLCardToolTipXML(BlockItem.BlockFieldItems);
            WriteToXaml(FClientData.FullXamlName, "TooltipField=\"\" TooltipFormat=\"\"", strSLCardToolTip);
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

        }

        //因为DD只有一个格式栏位，所以Web和Windows统一一种Format格式
        private String FormatEditMask(String editMask)
        {
            if (editMask != null && editMask != String.Empty && !editMask.StartsWith(","))
                editMask = ",\"{0:" + editMask + "}\"";
            return editMask;
        }

        private void GenMainBlockControl(TBlockItem BlockItem, int columnCount)
        {
            BlockItem.wDataSource = new WebDataSource();

            String strReadOnlyTemplate = ComposeFormViewXML(BlockItem.BlockFieldItems, "ReadOnlyTemplate", columnCount);
            WriteToXaml(FClientData.FullXamlName, "<Grid><toolkit:DataField x:Name=\"fvMasterReadOnlyTemplate\" /></Grid>", strReadOnlyTemplate);
            String strNewItemTemplate = ComposeFormViewXML(BlockItem.BlockFieldItems, "NewItemTemplate", columnCount);
            WriteToXaml(FClientData.FullXamlName, "<Grid><toolkit:DataField x:Name=\"fvMasterNewItemTemplate\" /></Grid>", strNewItemTemplate);
            String strEditTemplate = ComposeFormViewXML(BlockItem.BlockFieldItems, "EditTemplate", columnCount);
            WriteToXaml(FClientData.FullXamlName, "<Grid><toolkit:DataField x:Name=\"fvMasterEditTemplate\" /></Grid>", strEditTemplate);

            String strGridViewColumns = ComposeGridViewXML(BlockItem.BlockFieldItems);
            WriteToXaml(FClientData.FullXamlName,
                        "<SLTools:SLDataGrid.Columns><data:DataGridTextColumn Header=\"gvMasterDataGridColumns\" /></SLTools:SLDataGrid.Columns>",
                        strGridViewColumns);
        }

        //private void GenMainBlockControlFor3DCard(TBlockItem BlockItem, int columnCount)
        //{
        //    BlockItem.wDataSource = new WebDataSource();

        //    String strReadOnlyTemplate = ComposeFormViewXML(BlockItem.BlockFieldItems, "ReadOnlyTemplate", columnCount);
        //    WriteToXaml(FClientData.FullXamlName, "<Grid><toolkit:DataField x:Name=\"fvMasterReadOnlyTemplate\" /></Grid>", strReadOnlyTemplate);
        //    String strNewItemTemplate = ComposeFormViewXML(BlockItem.BlockFieldItems, "NewItemTemplate", columnCount);
        //    WriteToXaml(FClientData.FullXamlName, "<Grid><toolkit:DataField x:Name=\"fvMasterNewItemTemplate\" /></Grid>", strNewItemTemplate);
        //    String strEditTemplate = ComposeFormViewXML(BlockItem.BlockFieldItems, "EditTemplate", columnCount);
        //    WriteToXaml(FClientData.FullXamlName, "<Grid><toolkit:DataField x:Name=\"fvMasterEditTemplate\" /></Grid>", strEditTemplate);

        //    String strGridViewColumns = ComposeGridViewXML(BlockItem.BlockFieldItems);
        //    WriteToXaml(FClientData.FullXamlName,
        //                "<SLTools:SLDataGrid.Columns><data:DataGridTextColumn Header=\"gvMasterDataGridColumns\" /></SLTools:SLDataGrid.Columns>",
        //                strGridViewColumns);
        //}

        private String ComposeViewFormViewXML(TBlockFieldItems BlockFieldItems, String mode, int columnCount)
        {
            XmlDocument xmlTemplate = new XmlDocument();
            XmlNode xnTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "Grid", "");
            XmlAttribute xaMargin = xmlTemplate.CreateAttribute("Margin");
            xaMargin.Value = "10,5,10,5";
            xnTemplate.Attributes.Append(xaMargin);
            XmlNode xdGridRowDefinitions = xmlTemplate.CreateNode(XmlNodeType.Element, "Grid.RowDefinitions", "");
            double temp = Convert.ToDouble(BlockFieldItems.Count) / Convert.ToDouble(columnCount);
            for (int i = 0; i < System.Math.Ceiling(temp); i++)
            {
                XmlNode xmlRowDefinition = xmlTemplate.CreateNode(XmlNodeType.Element, "RowDefinition", "");
                XmlAttribute xaHeight = xmlTemplate.CreateAttribute("Height");
                xaHeight.Value = "30";
                xmlRowDefinition.Attributes.Append(xaHeight);
                xdGridRowDefinitions.AppendChild(xmlRowDefinition);
            }
            xnTemplate.AppendChild(xdGridRowDefinitions);

            XmlNode xdGridColumnDefinitions = xmlTemplate.CreateNode(XmlNodeType.Element, "Grid.ColumnDefinitions", "");
            for (int i = 0; i < columnCount; i++)
            {
                XmlNode xmlColumnDefinition = xmlTemplate.CreateNode(XmlNodeType.Element, "ColumnDefinition", "");
                xdGridColumnDefinitions.AppendChild(xmlColumnDefinition);
            }
            xnTemplate.AppendChild(xdGridColumnDefinitions);

            int X = 0;
            foreach (TBlockFieldItem BFI in BlockFieldItems)
            {
                XmlNode xnFiels = xmlTemplate.CreateNode(XmlNodeType.Element, "toolkit", "DataField", "infolight");
                XmlAttribute xaGridRow = xmlTemplate.CreateAttribute("Grid.Row");
                xaGridRow.Value = (X / columnCount).ToString();
                xnFiels.Attributes.Append(xaGridRow);

                XmlAttribute xaGridColumn = xmlTemplate.CreateAttribute("Grid.Column");
                xaGridColumn.Value = (X % columnCount).ToString();
                xnFiels.Attributes.Append(xaGridColumn);

                XmlAttribute xaLabelStyle = xmlTemplate.CreateAttribute("LabelStyle");
                xaLabelStyle.Value = "{StaticResource FieldCaptionStyle}";
                xnFiels.Attributes.Append(xaLabelStyle);

                XmlAttribute xaForegroundStyle = xmlTemplate.CreateAttribute("Style");
                xaForegroundStyle.Value = "{StaticResource FieldForegroundStyle}";
                xnFiels.Attributes.Append(xaForegroundStyle);

                XmlAttribute xaLabel = xmlTemplate.CreateAttribute("Label");
                if (BFI.Description == null || BFI.Description == String.Empty || BFI.Description == " ")
                    xaLabel.Value = BFI.DataField;
                else
                    xaLabel.Value = BFI.Description;
                xnFiels.Attributes.Append(xaLabel);

                XmlNode xnFieldControl = null;
                if (BFI.ControlType == "TextBox")
                {
                    if (mode == "ReadOnlyTemplate")
                    {
                        xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLTextBox", "infolight");
                        XmlAttribute xaBindingField = xmlTemplate.CreateAttribute("BindingField");
                        xaBindingField.Value = BFI.DataField;
                        xnFieldControl.Attributes.Append(xaBindingField);

                        //XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                        //xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "OneWay");
                        //xaText.Value = "{" + xaText.Value + "}";
                        //xnFieldControl.Attributes.Append(xaText);

                        XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                        xaWidth.Value = "180";
                        xnFieldControl.Attributes.Append(xaWidth);

                        XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                        xaHorizontalAlignment.Value = "Left";
                        xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                        XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                        xaName.Value = "TextBox" + BFI.DataField;
                        xnFieldControl.Attributes.Append(xaName);

                        //给FormView的ItemTemplate加上只读
                        if (FClientData.BaseFormName == "SL3DCard" || FClientData.BaseFormName == "SL3DCardMasterDetail"
                            || FClientData.BaseFormName == "SLFormList" || FClientData.BaseFormName == "SLFormListMasterDetail")
                        {
                            XmlAttribute xaIsEnabled = xmlTemplate.CreateAttribute("IsEnabled");
                            xaIsEnabled.Value = "False";
                            xnFieldControl.Attributes.Append(xaIsEnabled);
                        }
                    }
                    else if (mode == "NewItemTemplate" || mode == "EditTemplate")
                    {
                        xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLTextBox", "infolight");
                        XmlAttribute xaBindingField = xmlTemplate.CreateAttribute("BindingField");
                        xaBindingField.Value = BFI.DataField;
                        xnFieldControl.Attributes.Append(xaBindingField);

                        //xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "TextBox", "");
                        //XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                        //xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "TwoWay");
                        //xaText.Value = "{" + xaText.Value + "}";
                        //xnFieldControl.Attributes.Append(xaText);

                        XmlAttribute xaStyle = xmlTemplate.CreateAttribute("Style");
                        xaStyle.Value = "{StaticResource FieldEditor}";
                        xnFieldControl.Attributes.Append(xaStyle);

                        XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                        xaWidth.Value = "180";
                        xnFieldControl.Attributes.Append(xaWidth);

                        XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                        xaHorizontalAlignment.Value = "Left";
                        xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                        XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                        xaName.Value = "TextBox" + BFI.DataField;
                        xnFieldControl.Attributes.Append(xaName);
                    }
                }
                else if (BFI.ControlType == "DateTimeBox")
                {
                    if (mode == "ReadOnlyTemplate")
                    {
                        xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLTextBox", "infolight");
                        XmlAttribute xaBindingField = xmlTemplate.CreateAttribute("BindingField");
                        xaBindingField.Value = BFI.DataField;
                        xnFieldControl.Attributes.Append(xaBindingField);

                        //xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "TextBox", "");
                        //XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                        //xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "OneWay");
                        //xaText.Value = "{" + xaText.Value + "}";
                        //xnFieldControl.Attributes.Append(xaText);

                        XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                        xaWidth.Value = "180";
                        xnFieldControl.Attributes.Append(xaWidth);

                        XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                        xaHorizontalAlignment.Value = "Left";
                        xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                        XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                        xaName.Value = "DateTime" + BFI.DataField;
                        xnFieldControl.Attributes.Append(xaName);

                        //给FormView的ItemTemplate加上只读
                        if (FClientData.BaseFormName == "SL3DCard" || FClientData.BaseFormName == "SL3DCardMasterDetail"
                            || FClientData.BaseFormName == "SLFormList" || FClientData.BaseFormName == "SLFormListMasterDetail")
                        {
                            XmlAttribute xaIsEnabled = xmlTemplate.CreateAttribute("IsEnabled");
                            xaIsEnabled.Value = "False";
                            xnFieldControl.Attributes.Append(xaIsEnabled);
                        }
                    }
                    else if (mode == "NewItemTemplate" || mode == "EditTemplate")
                    {
                        xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "controls", "DatePicker", "infolight");
                        XmlAttribute xaSelectedDate = xmlTemplate.CreateAttribute("SelectedDate");
                        xaSelectedDate.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "TwoWay");
                        xaSelectedDate.Value = "{" + xaSelectedDate.Value + "}";
                        xnFieldControl.Attributes.Append(xaSelectedDate);

                        XmlAttribute xaStyle = xmlTemplate.CreateAttribute("Style");
                        xaStyle.Value = "{StaticResource FieldEditor}";
                        xnFieldControl.Attributes.Append(xaStyle);

                        XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                        xaWidth.Value = "180";
                        xnFieldControl.Attributes.Append(xaWidth);

                        XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                        xaHorizontalAlignment.Value = "Left";
                        xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                        XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                        xaName.Value = "DateTime" + BFI.DataField;
                        xnFieldControl.Attributes.Append(xaName);
                    }
                }
                else
                {
                    if (mode == "ReadOnlyTemplate")
                    {
                        xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLTextBox", "infolight");
                        XmlAttribute xaBindingField = xmlTemplate.CreateAttribute("BindingField");
                        xaBindingField.Value = BFI.DataField;
                        xnFieldControl.Attributes.Append(xaBindingField);

                        //xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "TextBox", "");
                        //XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                        //xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "OneWay");
                        //xaText.Value = "{" + xaText.Value + "}";
                        //xnFieldControl.Attributes.Append(xaText);

                        XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                        xaWidth.Value = "180";
                        xnFieldControl.Attributes.Append(xaWidth);

                        XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                        xaHorizontalAlignment.Value = "Left";
                        xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                        XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                        xaName.Value = "TextBox" + BFI.DataField;
                        xnFieldControl.Attributes.Append(xaName);

                        //给FormView的ItemTemplate加上只读
                        if (FClientData.BaseFormName == "SL3DCard" || FClientData.BaseFormName == "SL3DCardMasterDetail"
                            || FClientData.BaseFormName == "SLFormList" || FClientData.BaseFormName == "SLFormListMasterDetail")
                        {
                            XmlAttribute xaIsEnabled = xmlTemplate.CreateAttribute("IsEnabled");
                            xaIsEnabled.Value = "False";
                            xnFieldControl.Attributes.Append(xaIsEnabled);
                        }
                    }
                    else if (mode == "NewItemTemplate" || mode == "EditTemplate")
                    {
                        xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLTextBox", "infolight");
                        XmlAttribute xaBindingField = xmlTemplate.CreateAttribute("BindingField");
                        xaBindingField.Value = BFI.DataField;
                        xnFieldControl.Attributes.Append(xaBindingField);

                        //xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "TextBox", "");
                        //XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                        //xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "TwoWay");
                        //xaText.Value = "{" + xaText.Value + "}";
                        //xnFieldControl.Attributes.Append(xaText);

                        XmlAttribute xaStyle = xmlTemplate.CreateAttribute("Style");
                        xaStyle.Value = "{StaticResource FieldEditor}";
                        xnFieldControl.Attributes.Append(xaStyle);

                        XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                        xaWidth.Value = "180";
                        xnFieldControl.Attributes.Append(xaWidth);

                        XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                        xaHorizontalAlignment.Value = "Left";
                        xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                        XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                        xaName.Value = "TextBox" + BFI.DataField;
                        xnFieldControl.Attributes.Append(xaName);
                    }
                }
                xnFiels.AppendChild(xnFieldControl);
                xnTemplate.AppendChild(xnFiels);
                X++;
            }
            return xnTemplate.OuterXml.Replace("xmlns:toolkit=\"infolight\"", String.Empty).Replace("xmlns:controls=\"infolight\"", String.Empty).Replace("xmlns:SLTools=\"infolight\"", String.Empty);
        }

        private String ComposeFormViewXML(TBlockFieldItems BlockFieldItems, String mode, int columnCount)
        {
            XmlDocument xmlTemplate = new XmlDocument();
            XmlNode xnTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "Grid", "");
            XmlNode xdGridRowDefinitions = xmlTemplate.CreateNode(XmlNodeType.Element, "Grid.RowDefinitions", "");
            for (int i = 0; i < (BlockFieldItems.Count + 1) / columnCount; i++)
            {
                XmlNode xmlRowDefinition = xmlTemplate.CreateNode(XmlNodeType.Element, "RowDefinition", "");
                xdGridRowDefinitions.AppendChild(xmlRowDefinition);
            }
            xnTemplate.AppendChild(xdGridRowDefinitions);

            XmlNode xdGridColumnDefinitions = xmlTemplate.CreateNode(XmlNodeType.Element, "Grid.ColumnDefinitions", "");
            for (int i = 0; i < columnCount; i++)
            {
                XmlNode xmlColumnDefinition = xmlTemplate.CreateNode(XmlNodeType.Element, "ColumnDefinition", "");
                xdGridColumnDefinitions.AppendChild(xmlColumnDefinition);
            }
            xnTemplate.AppendChild(xdGridColumnDefinitions);

            int X = 0;
            foreach (TBlockFieldItem BFI in BlockFieldItems)
            {
                if (String.IsNullOrEmpty(BFI.ControlType))
                    BFI.ControlType = "TextBox";

                XmlNode xnFiels = xmlTemplate.CreateNode(XmlNodeType.Element, "toolkit", "DataField", "infolight");
                XmlAttribute xaGridRow = xmlTemplate.CreateAttribute("Grid.Row");
                xaGridRow.Value = (X / columnCount).ToString();
                xnFiels.Attributes.Append(xaGridRow);

                XmlAttribute xaGridColumn = xmlTemplate.CreateAttribute("Grid.Column");
                xaGridColumn.Value = (X % columnCount).ToString();
                xnFiels.Attributes.Append(xaGridColumn);

                XmlAttribute xaLabelStyle = xmlTemplate.CreateAttribute("LabelStyle");
                xaLabelStyle.Value = "{StaticResource FieldCaptionStyle}";
                xnFiels.Attributes.Append(xaLabelStyle);

                XmlAttribute xaForegroundStyle = xmlTemplate.CreateAttribute("Style");
                xaForegroundStyle.Value = "{StaticResource FieldForegroundStyle}";
                xnFiels.Attributes.Append(xaForegroundStyle);

                XmlAttribute xaLabel = xmlTemplate.CreateAttribute("Label");
                if (BFI.Description == null || BFI.Description == String.Empty || BFI.Description == " ")
                    xaLabel.Value = BFI.DataField;
                else
                    xaLabel.Value = BFI.Description;
                xnFiels.Attributes.Append(xaLabel);

                XmlAttribute xaLabelName = xmlTemplate.CreateAttribute("Name");
                xaLabelName.Value = BFI.DataField;
                xnFiels.Attributes.Append(xaLabelName);

                XmlNode xnFieldControl = null;

                if (BFI.ControlType == "CheckBox")
                {
                    if (mode == "ReadOnlyTemplate")
                    {
                        xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLCheckBox", "infolight");
                        XmlAttribute xaBindingField = xmlTemplate.CreateAttribute("BindingField");
                        xaBindingField.Value = BFI.DataField;
                        xnFieldControl.Attributes.Append(xaBindingField);

                        //XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                        //xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "OneWay");
                        //xaText.Value = "{" + xaText.Value + "}";
                        //xnFieldControl.Attributes.Append(xaText);

                        XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                        xaWidth.Value = "180";
                        xnFieldControl.Attributes.Append(xaWidth);

                        XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                        xaHorizontalAlignment.Value = "Left";
                        xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                        XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                        xaName.Value = "CheckBox" + BFI.DataField;
                        xnFieldControl.Attributes.Append(xaName);
                    }
                    else if (mode == "NewItemTemplate" || mode == "EditTemplate")
                    {
                        xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLCheckBox", "infolight");
                        XmlAttribute xaBindingField = xmlTemplate.CreateAttribute("BindingField");
                        xaBindingField.Value = BFI.DataField;
                        xnFieldControl.Attributes.Append(xaBindingField);

                        //xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "TextBox", "");
                        //XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                        //xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "TwoWay");
                        //xaText.Value = "{" + xaText.Value + "}";
                        //xnFieldControl.Attributes.Append(xaText);

                        XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                        xaWidth.Value = "180";
                        xnFieldControl.Attributes.Append(xaWidth);

                        XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                        xaHorizontalAlignment.Value = "Left";
                        xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                        XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                        xaName.Value = "CheckBox" + BFI.DataField;
                        xnFieldControl.Attributes.Append(xaName);
                    }
                }
                else if (BFI.ControlType == "DateTimeBox")
                {
                    if (mode == "ReadOnlyTemplate")
                    {
                        xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLTextBox", "infolight");
                        XmlAttribute xaBindingField = xmlTemplate.CreateAttribute("BindingField");
                        xaBindingField.Value = BFI.DataField;
                        xnFieldControl.Attributes.Append(xaBindingField);

                        //xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "TextBox", "");
                        //XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                        //xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "OneWay");
                        //xaText.Value = "{" + xaText.Value + "}";
                        //xnFieldControl.Attributes.Append(xaText);

                        XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                        xaWidth.Value = "180";
                        xnFieldControl.Attributes.Append(xaWidth);

                        XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                        xaHorizontalAlignment.Value = "Left";
                        xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                        XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                        xaName.Value = "DateTime" + BFI.DataField;
                        xnFieldControl.Attributes.Append(xaName);
                    }
                    else if (mode == "NewItemTemplate" || mode == "EditTemplate")
                    {
                        xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "controls", "DatePicker", "infolight");
                        XmlAttribute xaSelectedDate = xmlTemplate.CreateAttribute("SelectedDate");
                        xaSelectedDate.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "TwoWay");
                        xaSelectedDate.Value = "{" + xaSelectedDate.Value + "}";
                        xnFieldControl.Attributes.Append(xaSelectedDate);

                        XmlAttribute xaStyle = xmlTemplate.CreateAttribute("Style");
                        xaStyle.Value = "{StaticResource FieldEditor}";
                        xnFieldControl.Attributes.Append(xaStyle);

                        XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                        xaWidth.Value = "180";
                        xnFieldControl.Attributes.Append(xaWidth);

                        XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                        xaHorizontalAlignment.Value = "Left";
                        xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                        XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                        xaName.Value = "DateTime" + BFI.DataField;
                        xnFieldControl.Attributes.Append(xaName);
                    }
                }
                else if (BFI.ControlType == "ComboBox")
                {
                    if (String.IsNullOrEmpty(BFI.ComboRemoteName) || String.IsNullOrEmpty(BFI.ComboEntityName))
                        BFI.ControlType = "TextBox";
                    else
                    {
                        if (mode == "ReadOnlyTemplate")
                        {
                            String serviceDataSourceName = "combo" + BFI.ComboEntityName;
                            String collectionViewSourceName = "category" + serviceDataSourceName + "Source";

                            if (!FSLComboDataSource.Contains(serviceDataSourceName))
                            {
                                String xmlServiceDataSource = ComposeServiceDataSourceXML(serviceDataSourceName, BFI.ComboRemoteName, BFI.IsInfoCommand);
                                WriteToXaml(FClientData.FullXamlName, "</Grid.Resources>", "</Grid.Resources>" + xmlServiceDataSource);

                                String xmlComboCollectionSource = ComposeCollectionSourceXML(collectionViewSourceName, serviceDataSourceName);
                                WriteToXaml(FClientData.FullXamlName, "</Grid.Resources>", xmlComboCollectionSource + "</Grid.Resources>");
                            }

                            xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLComboBox", "infolight");
                            XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                            xaName.Value = "Combo" + BFI.DataField;
                            xnFieldControl.Attributes.Append(xaName);

                            //XmlAttribute xaSelectedValue = xmlTemplate.CreateAttribute("SelectedValue");
                            //xaSelectedValue.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "OneWay");
                            //xaSelectedValue.Value = "{" + xaSelectedValue.Value + "}";
                            //xnFieldControl.Attributes.Append(xaSelectedValue);

                            XmlAttribute xaSelectedValuePath = xmlTemplate.CreateAttribute("SelectedValuePath");
                            xaSelectedValuePath.Value = BFI.ComboValueField;
                            xnFieldControl.Attributes.Append(xaSelectedValuePath);

                            XmlAttribute xaDisplayMemberPath = xmlTemplate.CreateAttribute("DisplayMemberPath");
                            xaDisplayMemberPath.Value = BFI.ComboTextField;
                            xnFieldControl.Attributes.Append(xaDisplayMemberPath);

                            XmlAttribute xaItemsSource = xmlTemplate.CreateAttribute("ItemsSource");
                            xaItemsSource.Value = "{Binding Source={StaticResource " + collectionViewSourceName + "}}";
                            xnFieldControl.Attributes.Append(xaItemsSource);

                            XmlAttribute xaDataSourceID = xmlTemplate.CreateAttribute("DataSourceID");
                            xaDataSourceID.Value = serviceDataSourceName;
                            xnFieldControl.Attributes.Append(xaDataSourceID);

                            XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                            xaWidth.Value = "180";
                            xnFieldControl.Attributes.Append(xaWidth);

                            XmlAttribute xaBindingField = xmlTemplate.CreateAttribute("BindingField");
                            xaBindingField.Value = BFI.DataField;
                            xnFieldControl.Attributes.Append(xaBindingField);

                            XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                            xaHorizontalAlignment.Value = "Left";
                            xnFieldControl.Attributes.Append(xaHorizontalAlignment);
                            //xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "TextBox", "");
                            //XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                            //xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "OneWay");
                            //xaText.Value = "{" + xaText.Value + "}";
                            //xnFieldControl.Attributes.Append(xaText);

                            //XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                            //xaWidth.Value = "180";
                            //xnFieldControl.Attributes.Append(xaWidth);

                            //XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                            //xaHorizontalAlignment.Value = "Left";
                            //xnFieldControl.Attributes.Append(xaHorizontalAlignment);
                        }
                        else if (mode == "NewItemTemplate" || mode == "EditTemplate")
                        {
                            String serviceDataSourceName = "combo" + BFI.ComboEntityName;
                            String collectionViewSourceName = "category" + serviceDataSourceName + "Source";

                            if (!FSLComboDataSource.Contains(serviceDataSourceName))
                            {
                                String xmlServiceDataSource = ComposeServiceDataSourceXML(serviceDataSourceName, BFI.ComboRemoteName, BFI.IsInfoCommand);
                                WriteToXaml(FClientData.FullXamlName, "</Grid.Resources>", "</Grid.Resources>" + xmlServiceDataSource);

                                String xmlComboCollectionSource = ComposeCollectionSourceXML(collectionViewSourceName, serviceDataSourceName);
                                WriteToXaml(FClientData.FullXamlName, "</Grid.Resources>", xmlComboCollectionSource + "</Grid.Resources>");
                            }

                            xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLComboBox", "infolight");
                            XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                            xaName.Value = "Combo" + BFI.DataField;
                            xnFieldControl.Attributes.Append(xaName);

                            //XmlAttribute xaSelectedValue = xmlTemplate.CreateAttribute("SelectedValue");
                            //xaSelectedValue.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "TwoWay");
                            //xaSelectedValue.Value = "{" + xaSelectedValue.Value + "}";
                            //xnFieldControl.Attributes.Append(xaSelectedValue);

                            XmlAttribute xaSelectedValuePath = xmlTemplate.CreateAttribute("SelectedValuePath");
                            xaSelectedValuePath.Value = BFI.ComboValueField;
                            xnFieldControl.Attributes.Append(xaSelectedValuePath);

                            XmlAttribute xaDisplayMemberPath = xmlTemplate.CreateAttribute("DisplayMemberPath");
                            xaDisplayMemberPath.Value = BFI.ComboTextField;
                            xnFieldControl.Attributes.Append(xaDisplayMemberPath);

                            XmlAttribute xaItemsSource = xmlTemplate.CreateAttribute("ItemsSource");
                            xaItemsSource.Value = "{Binding Source={StaticResource " + collectionViewSourceName + "}}";
                            xnFieldControl.Attributes.Append(xaItemsSource);

                            XmlAttribute xaDataSourceID = xmlTemplate.CreateAttribute("DataSourceID");
                            xaDataSourceID.Value = serviceDataSourceName;
                            xnFieldControl.Attributes.Append(xaDataSourceID);

                            XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                            xaWidth.Value = "180";
                            xnFieldControl.Attributes.Append(xaWidth);

                            XmlAttribute xaBindingField = xmlTemplate.CreateAttribute("BindingField");
                            xaBindingField.Value = BFI.DataField;
                            xnFieldControl.Attributes.Append(xaBindingField);

                            XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                            xaHorizontalAlignment.Value = "Left";
                            xnFieldControl.Attributes.Append(xaHorizontalAlignment);
                        }
                    }
                }
                else if (BFI.ControlType == "RefValBox")
                {
                    if (String.IsNullOrEmpty(BFI.ComboRemoteName) || String.IsNullOrEmpty(BFI.ComboEntityName))
                        BFI.ControlType = "TextBox";
                    else
                    {
                        if (mode == "ReadOnlyTemplate")
                        {
                            //xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "TextBox", "");
                            //XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                            //xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "OneWay");
                            //xaText.Value = "{" + xaText.Value + "}";
                            //xnFieldControl.Attributes.Append(xaText);

                            //XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                            //xaWidth.Value = "180";
                            //xnFieldControl.Attributes.Append(xaWidth);

                            //XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                            //xaHorizontalAlignment.Value = "Left";
                            //xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                            String serviceDataSourceName = "refval" + BFI.ComboEntityName;
                            String collectionViewSourceName = "category" + serviceDataSourceName + "Source";

                            if (!FSLComboDataSource.Contains(serviceDataSourceName))
                            {
                                String xmlServiceDataSource = ComposeServiceDataSourceXML(serviceDataSourceName, BFI.ComboRemoteName, BFI.IsInfoCommand);
                                WriteToXaml(FClientData.FullXamlName, "</Grid.Resources>", "</Grid.Resources>" + xmlServiceDataSource);

                                String xmlComboCollectionSource = ComposeCollectionSourceXML(collectionViewSourceName, serviceDataSourceName);
                                WriteToXaml(FClientData.FullXamlName, "</Grid.Resources>", xmlComboCollectionSource + "</Grid.Resources>");
                            }

                            xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLRefval", "infolight");
                            XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                            xaName.Value = "Refval" + BFI.DataField;
                            xnFieldControl.Attributes.Append(xaName);

                            XmlAttribute xaSelectedValue = xmlTemplate.CreateAttribute("SelectedValue");
                            xaSelectedValue.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "OneWay");
                            xaSelectedValue.Value = "{" + xaSelectedValue.Value + "}";
                            xnFieldControl.Attributes.Append(xaSelectedValue);

                            XmlAttribute xaValueMemberPath = xmlTemplate.CreateAttribute("ValueMemberPath");
                            xaValueMemberPath.Value = BFI.ComboValueField;
                            xnFieldControl.Attributes.Append(xaValueMemberPath);

                            XmlAttribute xaDisplayMemberPath = xmlTemplate.CreateAttribute("DisplayMemberPath");
                            xaDisplayMemberPath.Value = BFI.ComboTextField;
                            xnFieldControl.Attributes.Append(xaDisplayMemberPath);

                            XmlAttribute xaItemsSource = xmlTemplate.CreateAttribute("ItemsSource");
                            xaItemsSource.Value = "{Binding Source={StaticResource " + collectionViewSourceName + "}}";
                            xnFieldControl.Attributes.Append(xaItemsSource);

                            XmlAttribute xaDataSourceID = xmlTemplate.CreateAttribute("DataSourceID");
                            xaDataSourceID.Value = serviceDataSourceName;
                            xnFieldControl.Attributes.Append(xaDataSourceID);

                            XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                            xaWidth.Value = "180";
                            xnFieldControl.Attributes.Append(xaWidth);

                            XmlAttribute xaOpenSize = xmlTemplate.CreateAttribute("OpenSize");
                            xaOpenSize.Value = "500,400";
                            xnFieldControl.Attributes.Append(xaOpenSize);

                            XmlNode xnSLRefvalColumns = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLRefval.Columns", "infolight");
                            //ValueMember
                            XmlNode xnRefvalColumnValueMember = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "RefvalColumn", "infolight");
                            XmlAttribute xaValueMemberFieldName = xmlTemplate.CreateAttribute("FieldName");
                            xaValueMemberFieldName.Value = BFI.ComboValueField;
                            xnRefvalColumnValueMember.Attributes.Append(xaValueMemberFieldName);

                            XmlAttribute xaValueMemberHeadText = xmlTemplate.CreateAttribute("HeadText");
                            xaValueMemberHeadText.Value = String.IsNullOrEmpty(BFI.ComboValueFieldCaption) ? BFI.ComboValueField : BFI.ComboValueFieldCaption;
                            xnRefvalColumnValueMember.Attributes.Append(xaValueMemberHeadText);

                            XmlAttribute xaValueMemberWidth = xmlTemplate.CreateAttribute("Width");
                            xaValueMemberWidth.Value = "100";
                            xnRefvalColumnValueMember.Attributes.Append(xaValueMemberWidth);
                            xnSLRefvalColumns.AppendChild(xnRefvalColumnValueMember);
                            xnFieldControl.AppendChild(xnSLRefvalColumns);

                            if (BFI.ComboValueField != BFI.ComboTextField)
                            {
                                //DisplayMember
                                XmlNode xnRefvalColumnDisplayMember = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "RefvalColumn", "infolight");
                                XmlAttribute xaDisplayMemberFieldName = xmlTemplate.CreateAttribute("FieldName");
                                xaDisplayMemberFieldName.Value = BFI.ComboTextField;
                                xnRefvalColumnDisplayMember.Attributes.Append(xaDisplayMemberFieldName);

                                XmlAttribute xaDisplayMemberHeadText = xmlTemplate.CreateAttribute("HeadText");
                                xaDisplayMemberHeadText.Value = String.IsNullOrEmpty(BFI.ComboTextFieldCaption) ? BFI.ComboTextField : BFI.ComboTextFieldCaption;
                                xnRefvalColumnDisplayMember.Attributes.Append(xaDisplayMemberHeadText);

                                XmlAttribute xaDisplayMemberWidth = xmlTemplate.CreateAttribute("Width");
                                xaDisplayMemberWidth.Value = "100";
                                xnRefvalColumnDisplayMember.Attributes.Append(xaDisplayMemberWidth);
                                xnSLRefvalColumns.AppendChild(xnRefvalColumnDisplayMember);
                            }

                            //OtherFields
                            if (BFI.ComboOtherFields != null)
                            {
                                foreach (OtherField item in BFI.ComboOtherFields)
                                {
                                    XmlNode xnRefvalColumnOtherField = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "RefvalColumn", "infolight");
                                    XmlAttribute xaDisplayMemberFieldName = xmlTemplate.CreateAttribute("FieldName");
                                    xaDisplayMemberFieldName.Value = item.FieldName;
                                    xnRefvalColumnOtherField.Attributes.Append(xaDisplayMemberFieldName);

                                    XmlAttribute xaDisplayMemberHeadText = xmlTemplate.CreateAttribute("HeadText");
                                    xaDisplayMemberHeadText.Value = String.IsNullOrEmpty(item.FieldCaption) ? item.FieldName : item.FieldCaption;
                                    xnRefvalColumnOtherField.Attributes.Append(xaDisplayMemberHeadText);

                                    XmlAttribute xaDisplayMemberWidth = xmlTemplate.CreateAttribute("Width");
                                    xaDisplayMemberWidth.Value = "100";
                                    xnRefvalColumnOtherField.Attributes.Append(xaDisplayMemberWidth);
                                    xnSLRefvalColumns.AppendChild(xnRefvalColumnOtherField);
                                }
                            }

                            XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                            xaHorizontalAlignment.Value = "Left";
                            xnFieldControl.Attributes.Append(xaHorizontalAlignment);
                        }
                        else if (mode == "NewItemTemplate" || mode == "EditTemplate")
                        {
                            String serviceDataSourceName = "refval" + BFI.ComboEntityName;
                            String collectionViewSourceName = "category" + serviceDataSourceName + "Source";

                            if (!FSLComboDataSource.Contains(serviceDataSourceName))
                            {
                                String xmlServiceDataSource = ComposeServiceDataSourceXML(serviceDataSourceName, BFI.ComboRemoteName, BFI.IsInfoCommand);
                                WriteToXaml(FClientData.FullXamlName, "</Grid.Resources>", "</Grid.Resources>" + xmlServiceDataSource);

                                String xmlComboCollectionSource = ComposeCollectionSourceXML(collectionViewSourceName, serviceDataSourceName);
                                WriteToXaml(FClientData.FullXamlName, "</Grid.Resources>", xmlComboCollectionSource + "</Grid.Resources>");
                            }

                            xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLRefval", "infolight");
                            XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                            xaName.Value = "Refval" + BFI.DataField;
                            xnFieldControl.Attributes.Append(xaName);

                            XmlAttribute xaSelectedValue = xmlTemplate.CreateAttribute("SelectedValue");
                            xaSelectedValue.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "TwoWay");
                            xaSelectedValue.Value = "{" + xaSelectedValue.Value + "}";
                            xnFieldControl.Attributes.Append(xaSelectedValue);

                            XmlAttribute xaValueMemberPath = xmlTemplate.CreateAttribute("ValueMemberPath");
                            xaValueMemberPath.Value = BFI.ComboValueField;
                            xnFieldControl.Attributes.Append(xaValueMemberPath);

                            XmlAttribute xaDisplayMemberPath = xmlTemplate.CreateAttribute("DisplayMemberPath");
                            xaDisplayMemberPath.Value = BFI.ComboTextField;
                            xnFieldControl.Attributes.Append(xaDisplayMemberPath);

                            XmlAttribute xaItemsSource = xmlTemplate.CreateAttribute("ItemsSource");
                            xaItemsSource.Value = "{Binding Source={StaticResource " + collectionViewSourceName + "}}";
                            xnFieldControl.Attributes.Append(xaItemsSource);

                            XmlAttribute xaDataSourceID = xmlTemplate.CreateAttribute("DataSourceID");
                            xaDataSourceID.Value = serviceDataSourceName;
                            xnFieldControl.Attributes.Append(xaDataSourceID);

                            XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                            xaWidth.Value = "180";
                            xnFieldControl.Attributes.Append(xaWidth);

                            XmlAttribute xaOpenSize = xmlTemplate.CreateAttribute("OpenSize");
                            xaOpenSize.Value = "500,400";
                            xnFieldControl.Attributes.Append(xaOpenSize);

                            XmlNode xnSLRefvalColumns = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLRefval.Columns", "infolight");
                            //ValueMember
                            XmlNode xnRefvalColumnValueMember = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "RefvalColumn", "infolight");
                            XmlAttribute xaValueMemberFieldName = xmlTemplate.CreateAttribute("FieldName");
                            xaValueMemberFieldName.Value = BFI.ComboValueField;
                            xnRefvalColumnValueMember.Attributes.Append(xaValueMemberFieldName);

                            XmlAttribute xaValueMemberHeadText = xmlTemplate.CreateAttribute("HeadText");
                            xaValueMemberHeadText.Value = String.IsNullOrEmpty(BFI.ComboValueFieldCaption) ? BFI.ComboValueField : BFI.ComboValueFieldCaption;
                            xnRefvalColumnValueMember.Attributes.Append(xaValueMemberHeadText);

                            XmlAttribute xaValueMemberWidth = xmlTemplate.CreateAttribute("Width");
                            xaValueMemberWidth.Value = "100";
                            xnRefvalColumnValueMember.Attributes.Append(xaValueMemberWidth);
                            xnSLRefvalColumns.AppendChild(xnRefvalColumnValueMember);
                            xnFieldControl.AppendChild(xnSLRefvalColumns);

                            if (BFI.ComboValueField != BFI.ComboTextField)
                            {
                                //DisplayMember
                                XmlNode xnRefvalColumnDisplayMember = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "RefvalColumn", "infolight");
                                XmlAttribute xaDisplayMemberFieldName = xmlTemplate.CreateAttribute("FieldName");
                                xaDisplayMemberFieldName.Value = BFI.ComboTextField;
                                xnRefvalColumnDisplayMember.Attributes.Append(xaDisplayMemberFieldName);

                                XmlAttribute xaDisplayMemberHeadText = xmlTemplate.CreateAttribute("HeadText");
                                xaDisplayMemberHeadText.Value = String.IsNullOrEmpty(BFI.ComboTextFieldCaption) ? BFI.ComboTextField : BFI.ComboTextFieldCaption;
                                xnRefvalColumnDisplayMember.Attributes.Append(xaDisplayMemberHeadText);

                                XmlAttribute xaDisplayMemberWidth = xmlTemplate.CreateAttribute("Width");
                                xaDisplayMemberWidth.Value = "100";
                                xnRefvalColumnDisplayMember.Attributes.Append(xaDisplayMemberWidth);
                                xnSLRefvalColumns.AppendChild(xnRefvalColumnDisplayMember);
                            }

                            //OtherFields
                            if (BFI.ComboOtherFields != null)
                            {
                                foreach (OtherField item in BFI.ComboOtherFields)
                                {
                                    XmlNode xnRefvalColumnOtherField = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "RefvalColumn", "infolight");
                                    XmlAttribute xaDisplayMemberFieldName = xmlTemplate.CreateAttribute("FieldName");
                                    xaDisplayMemberFieldName.Value = item.FieldName;
                                    xnRefvalColumnOtherField.Attributes.Append(xaDisplayMemberFieldName);

                                    XmlAttribute xaDisplayMemberHeadText = xmlTemplate.CreateAttribute("HeadText");
                                    xaDisplayMemberHeadText.Value = String.IsNullOrEmpty(item.FieldCaption) ? item.FieldName : item.FieldCaption;
                                    xnRefvalColumnOtherField.Attributes.Append(xaDisplayMemberHeadText);

                                    XmlAttribute xaDisplayMemberWidth = xmlTemplate.CreateAttribute("Width");
                                    xaDisplayMemberWidth.Value = "100";
                                    xnRefvalColumnOtherField.Attributes.Append(xaDisplayMemberWidth);
                                    xnSLRefvalColumns.AppendChild(xnRefvalColumnOtherField);
                                }
                            }

                            XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                            xaHorizontalAlignment.Value = "Left";
                            xnFieldControl.Attributes.Append(xaHorizontalAlignment);
                        }
                    }
                }

                if (BFI.ControlType == "TextBox")
                {
                    if (mode == "ReadOnlyTemplate")
                    {
                        xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLTextBox", "infolight");
                        XmlAttribute xaBindingField = xmlTemplate.CreateAttribute("BindingField");
                        xaBindingField.Value = BFI.DataField;
                        xnFieldControl.Attributes.Append(xaBindingField);

                        //XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                        //xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "OneWay");
                        //xaText.Value = "{" + xaText.Value + "}";
                        //xnFieldControl.Attributes.Append(xaText);

                        XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                        xaWidth.Value = "180";
                        xnFieldControl.Attributes.Append(xaWidth);

                        XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                        xaHorizontalAlignment.Value = "Left";
                        xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                        XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                        xaName.Value = "TextBox" + BFI.DataField;
                        xnFieldControl.Attributes.Append(xaName);
                    }
                    else if (mode == "NewItemTemplate" || mode == "EditTemplate")
                    {
                        xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLTextBox", "infolight");
                        XmlAttribute xaBindingField = xmlTemplate.CreateAttribute("BindingField");
                        xaBindingField.Value = BFI.DataField;
                        xnFieldControl.Attributes.Append(xaBindingField);

                        //xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "TextBox", "");
                        //XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                        //xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "TwoWay");
                        //xaText.Value = "{" + xaText.Value + "}";
                        //xnFieldControl.Attributes.Append(xaText);

                        XmlAttribute xaStyle = xmlTemplate.CreateAttribute("Style");
                        xaStyle.Value = "{StaticResource FieldEditor}";
                        xnFieldControl.Attributes.Append(xaStyle);

                        XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                        xaWidth.Value = "180";
                        xnFieldControl.Attributes.Append(xaWidth);

                        XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                        xaHorizontalAlignment.Value = "Left";
                        xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                        XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                        xaName.Value = "TextBox" + BFI.DataField;
                        xnFieldControl.Attributes.Append(xaName);
                    }
                }

                xnFiels.AppendChild(xnFieldControl);
                xnTemplate.AppendChild(xnFiels);
                X++;
            }
            return xnTemplate.OuterXml.Replace("xmlns:toolkit=\"infolight\"", String.Empty).Replace("xmlns:controls=\"infolight\"", String.Empty).Replace("xmlns:SLTools=\"infolight\"", String.Empty);
        }

        private String ComposeGridViewXML(TBlockFieldItems BlockFieldItems)
        {
            XmlDocument xmlTemplate = new XmlDocument();
            XmlNode xnTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLDataGrid.Columns", "infolight");

            foreach (TBlockFieldItem BFI in BlockFieldItems)
            {
                String ControlTypeOldValue = BFI.ControlType;
                if (String.IsNullOrEmpty(BFI.ControlType) || String.IsNullOrEmpty(BFI.ComboRemoteName))
                    BFI.ControlType = "TextBox";

                if (FClientData.BaseFormName != "SL3DCardMasterDetail" || FClientData.BaseFormName == "SLFormListMasterDetail")
                {
                    BFI.ControlType = "TextBox";
                }

                XmlNode xnFiels = null;

                if (BFI.ControlType == "ComboBox")
                {
                    xnFiels = xmlTemplate.CreateNode(XmlNodeType.Element, "data", "DataGridTemplateColumn", "infolight");
                    XmlNode xnDataGridTemplateColumnCellTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "data", "DataGridTemplateColumn.CellTemplate", "infolight");
                    XmlNode xnDataTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "DataTemplate", "");
                    XmlNode xnGrid = xmlTemplate.CreateNode(XmlNodeType.Element, "Grid", "");
                    XmlNode xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "TextBlock", "");
                    XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                    xaName.Value = "TextBlock" + BFI.DataField;
                    xnFieldControl.Attributes.Append(xaName);

                    XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                    xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "TwoWay");
                    xaText.Value = "{" + xaText.Value + "}";
                    xnFieldControl.Attributes.Append(xaText);

                    XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                    xaWidth.Value = "180";
                    xnFieldControl.Attributes.Append(xaWidth);

                    xnGrid.AppendChild(xnFieldControl);
                    xnDataTemplate.AppendChild(xnGrid);
                    xnDataGridTemplateColumnCellTemplate.AppendChild(xnDataTemplate);
                    xnFiels.AppendChild(xnDataGridTemplateColumnCellTemplate);

                    XmlNode xnDataGridTemplateColumnCellEditingTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "data", "DataGridTemplateColumn.CellEditingTemplate", "infolight");
                    xnDataTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "DataTemplate", "");
                    xnGrid = xmlTemplate.CreateNode(XmlNodeType.Element, "Grid", "");
                    String serviceDataSourceName = "combo" + BFI.ComboEntityName;
                    String collectionViewSourceName = "category" + serviceDataSourceName + "Source";

                    if (!FSLComboDataSource.Contains(serviceDataSourceName))
                    {
                        String xmlServiceDataSource = ComposeServiceDataSourceXML(serviceDataSourceName, BFI.ComboRemoteName, BFI.IsInfoCommand);
                        WriteToXaml(FClientData.FullXamlName, "</Grid.Resources>", "</Grid.Resources>" + xmlServiceDataSource);

                        String xmlComboCollectionSource = ComposeCollectionSourceXML(collectionViewSourceName, serviceDataSourceName);
                        WriteToXaml(FClientData.FullXamlName, "</Grid.Resources>", xmlComboCollectionSource + "</Grid.Resources>");
                    }

                    xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLComboBox", "infolight");
                    xaName = xmlTemplate.CreateAttribute("Name");
                    xaName.Value = "Combo" + BFI.DataField;
                    xnFieldControl.Attributes.Append(xaName);

                    XmlAttribute xaSelectedValuePath = xmlTemplate.CreateAttribute("SelectedValuePath");
                    xaSelectedValuePath.Value = BFI.ComboValueField;
                    xnFieldControl.Attributes.Append(xaSelectedValuePath);

                    XmlAttribute xaDisplayMemberPath = xmlTemplate.CreateAttribute("DisplayMemberPath");
                    xaDisplayMemberPath.Value = BFI.ComboTextField;
                    xnFieldControl.Attributes.Append(xaDisplayMemberPath);

                    XmlAttribute xaDataSourceID = xmlTemplate.CreateAttribute("DataSourceID");
                    xaDataSourceID.Value = serviceDataSourceName;
                    xnFieldControl.Attributes.Append(xaDataSourceID);

                    XmlAttribute xaItemsSource = xmlTemplate.CreateAttribute("ItemsSource");
                    xaItemsSource.Value = "{Binding Source={StaticResource " + collectionViewSourceName + "}}";
                    xnFieldControl.Attributes.Append(xaItemsSource);

                    xaWidth = xmlTemplate.CreateAttribute("Width");
                    xaWidth.Value = "180";
                    xnFieldControl.Attributes.Append(xaWidth);

                    //XmlAttribute xaBindingField = xmlTemplate.CreateAttribute("BindingField");
                    //xaBindingField.Value = BFI.DataField;
                    //xnFieldControl.Attributes.Append(xaBindingField);

                    XmlAttribute xaSelectedValue = xmlTemplate.CreateAttribute("SelectedValue");
                    xaSelectedValue.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "TwoWay");
                    xaSelectedValue.Value = "{" + xaSelectedValue.Value + "}";
                    xnFieldControl.Attributes.Append(xaSelectedValue);

                    XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                    xaHorizontalAlignment.Value = "Left";
                    xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                    xnGrid.AppendChild(xnFieldControl);
                    xnDataTemplate.AppendChild(xnGrid);
                    xnDataGridTemplateColumnCellEditingTemplate.AppendChild(xnDataTemplate);
                    xnFiels.AppendChild(xnDataGridTemplateColumnCellEditingTemplate);
                }
                else if (BFI.ControlType == "RefValBox")
                {
                    xnFiels = xmlTemplate.CreateNode(XmlNodeType.Element, "data", "DataGridTemplateColumn", "infolight");
                    XmlNode xnDataGridTemplateColumnCellTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "data", "DataGridTemplateColumn.CellTemplate", "infolight");
                    XmlNode xnDataTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "DataTemplate", "");
                    XmlNode xnGrid = xmlTemplate.CreateNode(XmlNodeType.Element, "Grid", "");
                    XmlNode xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "TextBlock", "");
                    XmlAttribute xaName = xmlTemplate.CreateAttribute("Name");
                    xaName.Value = "TextBlock" + BFI.DataField;
                    xnFieldControl.Attributes.Append(xaName);

                    XmlAttribute xaText = xmlTemplate.CreateAttribute("Text");
                    xaText.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "OneWay");
                    xaText.Value = "{" + xaText.Value + "}";
                    xnFieldControl.Attributes.Append(xaText);

                    XmlAttribute xaWidth = xmlTemplate.CreateAttribute("Width");
                    xaWidth.Value = "180";
                    xnFieldControl.Attributes.Append(xaWidth);

                    xnGrid.AppendChild(xnFieldControl);
                    xnDataTemplate.AppendChild(xnGrid);
                    xnDataGridTemplateColumnCellTemplate.AppendChild(xnDataTemplate);
                    xnFiels.AppendChild(xnDataGridTemplateColumnCellTemplate);


                    XmlNode xnDataGridTemplateColumnCellEditingTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "data", "DataGridTemplateColumn.CellEditingTemplate", "infolight");
                    xnDataTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "DataTemplate", "");
                    xnGrid = xmlTemplate.CreateNode(XmlNodeType.Element, "Grid", "");

                    String serviceDataSourceName = "refval" + BFI.ComboEntityName;
                    String collectionViewSourceName = "category" + serviceDataSourceName + "Source";

                    if (!FSLComboDataSource.Contains(serviceDataSourceName))
                    {
                        String xmlServiceDataSource = ComposeServiceDataSourceXML(serviceDataSourceName, BFI.ComboRemoteName, BFI.IsInfoCommand);
                        WriteToXaml(FClientData.FullXamlName, "</Grid.Resources>", "</Grid.Resources>" + xmlServiceDataSource);

                        String xmlComboCollectionSource = ComposeCollectionSourceXML(collectionViewSourceName, serviceDataSourceName);
                        WriteToXaml(FClientData.FullXamlName, "</Grid.Resources>", xmlComboCollectionSource + "</Grid.Resources>");
                    }

                    xnFieldControl = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLRefval", "infolight");
                    xaName = xmlTemplate.CreateAttribute("Name");
                    xaName.Value = "Refval" + BFI.DataField;
                    xnFieldControl.Attributes.Append(xaName);

                    XmlAttribute xaMode = xmlTemplate.CreateAttribute("Mode");
                    xaMode.Value = "PopupWindow";
                    xnFieldControl.Attributes.Append(xaMode);

                    xaWidth = xmlTemplate.CreateAttribute("Width");
                    xaWidth.Value = "180";
                    xnFieldControl.Attributes.Append(xaWidth);

                    XmlAttribute xaSelectedValue = xmlTemplate.CreateAttribute("SelectedValue");
                    xaSelectedValue.Value = String.Format("Binding {0}, Mode={1}", BFI.DataField, "TwoWay");
                    xaSelectedValue.Value = "{" + xaSelectedValue.Value + "}";
                    xnFieldControl.Attributes.Append(xaSelectedValue);

                    XmlAttribute xaValueMemberPath = xmlTemplate.CreateAttribute("ValueMemberPath");
                    xaValueMemberPath.Value = BFI.ComboValueField;
                    xnFieldControl.Attributes.Append(xaValueMemberPath);

                    XmlAttribute xaDisplayMemberPath = xmlTemplate.CreateAttribute("DisplayMemberPath");
                    xaDisplayMemberPath.Value = BFI.ComboTextField;
                    xnFieldControl.Attributes.Append(xaDisplayMemberPath);

                    XmlAttribute xaItemsSource = xmlTemplate.CreateAttribute("ItemsSource");
                    xaItemsSource.Value = "{Binding Source={StaticResource " + collectionViewSourceName + "}}";
                    xnFieldControl.Attributes.Append(xaItemsSource);

                    XmlAttribute xaDataSourceID = xmlTemplate.CreateAttribute("DataSourceID");
                    xaDataSourceID.Value = serviceDataSourceName;
                    xnFieldControl.Attributes.Append(xaDataSourceID);

                    XmlAttribute xaOpenSize = xmlTemplate.CreateAttribute("OpenSize");
                    xaOpenSize.Value = "500,400";
                    xnFieldControl.Attributes.Append(xaOpenSize);

                    XmlNode xnSLRefvalColumns = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLRefval.Columns", "infolight");
                    //ValueMember
                    XmlNode xnRefvalColumnValueMember = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "RefvalColumn", "infolight");
                    XmlAttribute xaValueMemberFieldName = xmlTemplate.CreateAttribute("FieldName");
                    xaValueMemberFieldName.Value = BFI.ComboValueField;
                    xnRefvalColumnValueMember.Attributes.Append(xaValueMemberFieldName);

                    XmlAttribute xaValueMemberHeadText = xmlTemplate.CreateAttribute("HeadText");
                    xaValueMemberHeadText.Value = String.IsNullOrEmpty(BFI.ComboValueFieldCaption) ? BFI.ComboValueField : BFI.ComboValueFieldCaption;
                    xnRefvalColumnValueMember.Attributes.Append(xaValueMemberHeadText);

                    XmlAttribute xaValueMemberWidth = xmlTemplate.CreateAttribute("Width");
                    xaValueMemberWidth.Value = "100";
                    xnRefvalColumnValueMember.Attributes.Append(xaValueMemberWidth);
                    xnSLRefvalColumns.AppendChild(xnRefvalColumnValueMember);
                    xnFieldControl.AppendChild(xnSLRefvalColumns);

                    if (BFI.ComboValueField != BFI.ComboTextField)
                    {
                        //DisplayMember
                        XmlNode xnRefvalColumnDisplayMember = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "RefvalColumn", "infolight");
                        XmlAttribute xaDisplayMemberFieldName = xmlTemplate.CreateAttribute("FieldName");
                        xaDisplayMemberFieldName.Value = BFI.ComboTextField;
                        xnRefvalColumnDisplayMember.Attributes.Append(xaDisplayMemberFieldName);

                        XmlAttribute xaDisplayMemberHeadText = xmlTemplate.CreateAttribute("HeadText");
                        xaDisplayMemberHeadText.Value = String.IsNullOrEmpty(BFI.ComboTextFieldCaption) ? BFI.ComboTextField : BFI.ComboTextFieldCaption;
                        xnRefvalColumnDisplayMember.Attributes.Append(xaDisplayMemberHeadText);

                        XmlAttribute xaDisplayMemberWidth = xmlTemplate.CreateAttribute("Width");
                        xaDisplayMemberWidth.Value = "100";
                        xnRefvalColumnDisplayMember.Attributes.Append(xaDisplayMemberWidth);
                        xnSLRefvalColumns.AppendChild(xnRefvalColumnDisplayMember);
                    }

                    //OtherFields
                    if (BFI.ComboOtherFields != null)
                    {
                        foreach (OtherField item in BFI.ComboOtherFields)
                        {
                            XmlNode xnRefvalColumnOtherField = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "RefvalColumn", "infolight");
                            XmlAttribute xaDisplayMemberFieldName = xmlTemplate.CreateAttribute("FieldName");
                            xaDisplayMemberFieldName.Value = item.FieldName;
                            xnRefvalColumnOtherField.Attributes.Append(xaDisplayMemberFieldName);

                            XmlAttribute xaDisplayMemberHeadText = xmlTemplate.CreateAttribute("HeadText");
                            xaDisplayMemberHeadText.Value = String.IsNullOrEmpty(item.FieldCaption) ? item.FieldName : item.FieldCaption;
                            xnRefvalColumnOtherField.Attributes.Append(xaDisplayMemberHeadText);

                            XmlAttribute xaDisplayMemberWidth = xmlTemplate.CreateAttribute("Width");
                            xaDisplayMemberWidth.Value = "100";
                            xnRefvalColumnOtherField.Attributes.Append(xaDisplayMemberWidth);
                            xnSLRefvalColumns.AppendChild(xnRefvalColumnOtherField);
                        }
                    }

                    XmlAttribute xaHorizontalAlignment = xmlTemplate.CreateAttribute("HorizontalAlignment");
                    xaHorizontalAlignment.Value = "Left";
                    xnFieldControl.Attributes.Append(xaHorizontalAlignment);

                    xnGrid.AppendChild(xnFieldControl);
                    xnDataTemplate.AppendChild(xnGrid);
                    xnDataGridTemplateColumnCellEditingTemplate.AppendChild(xnDataTemplate);
                    xnFiels.AppendChild(xnDataGridTemplateColumnCellEditingTemplate);
                }
                else
                {
                    xnFiels = xmlTemplate.CreateNode(XmlNodeType.Element, "data", "DataGridTextColumn", "infolight");

                    XmlAttribute xaBinding = xmlTemplate.CreateAttribute("Binding");
                    xaBinding.Value = "{Binding " + BFI.DataField + "}";
                    xnFiels.Attributes.Append(xaBinding);
                }

                XmlAttribute xaCanUserReorder = xmlTemplate.CreateAttribute("CanUserReorder");
                xaCanUserReorder.Value = "True";
                xnFiels.Attributes.Append(xaCanUserReorder);

                XmlAttribute xaCanUserResize = xmlTemplate.CreateAttribute("CanUserResize");
                xaCanUserResize.Value = "True";
                xnFiels.Attributes.Append(xaCanUserResize);

                XmlAttribute xaCanUserSort = xmlTemplate.CreateAttribute("CanUserSort");
                xaCanUserSort.Value = "True";
                xnFiels.Attributes.Append(xaCanUserSort);

                XmlAttribute xaHeader = xmlTemplate.CreateAttribute("Header");
                if (BFI.Description == null || BFI.Description == String.Empty || BFI.Description == " ")
                    xaHeader.Value = BFI.DataField;
                else
                    xaHeader.Value = BFI.Description;
                xnFiels.Attributes.Append(xaHeader);

                XmlAttribute xaWidth2 = xmlTemplate.CreateAttribute("Width");
                xaWidth2.Value = "Auto";
                xnFiels.Attributes.Append(xaWidth2);

                if (FClientData.BaseFormName == "SLSingle" || FClientData.BaseFormName == "SLMasterDetail2"
                    || FClientData.BaseFormName == "SLMasterDetail3")
                {
                    XmlAttribute xaIsReadOnly = xmlTemplate.CreateAttribute("IsReadOnly");
                    xaIsReadOnly.Value = "True";
                    xnFiels.Attributes.Append(xaIsReadOnly);
                }
                xnTemplate.AppendChild(xnFiels);

                BFI.ControlType = ControlTypeOldValue;
            }
            String returnValue = xnTemplate.OuterXml.Replace("xmlns:data=\"infolight\"", String.Empty).Replace("xmlns:SLTools=\"infolight\"", String.Empty);
            return returnValue;
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

        private String ComposeCollectionSourceXML(String collectionViewSourceName, String serviceDataSourceName)
        {
            XmlDocument xmlTemplate = new XmlDocument();
            XmlNode xnTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "CollectionViewSource", "");
            XmlAttribute xaKey = xmlTemplate.CreateAttribute("x", "Key", "infolight");
            xaKey.Value = collectionViewSourceName;
            xnTemplate.Attributes.Append(xaKey);

            XmlAttribute xaSource = xmlTemplate.CreateAttribute("Source");
            xaSource.Value = "{Binding Path=Data, ElementName=" + serviceDataSourceName + "}";
            xnTemplate.Attributes.Append(xaSource);

            String returnValue = xnTemplate.OuterXml;
            return returnValue.Replace("xmlns:x=\"infolight\"", String.Empty);
        }

        private String ComposeServiceDataSourceXML(String dataSourcrName, String remoteName, Boolean isInfoCommand)
        {
            XmlDocument xmlTemplate = new XmlDocument();
            XmlNode xnTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "ServiceDataSource", "infolight");
            XmlAttribute xaName = xmlTemplate.CreateAttribute("x", "Name", "infolight");
            xaName.Value = dataSourcrName;
            xnTemplate.Attributes.Append(xaName);

            XmlAttribute xaRemoteName = xmlTemplate.CreateAttribute("RemoteName");
            xaRemoteName.Value = remoteName;
            xnTemplate.Attributes.Append(xaRemoteName);

            XmlAttribute xaUseDataSet = xmlTemplate.CreateAttribute("UseDataSet");
            xaUseDataSet.Value = isInfoCommand.ToString();
            xnTemplate.Attributes.Append(xaUseDataSet);

            XmlAttribute xaGridUse = xmlTemplate.CreateAttribute("GridUse");
            xaGridUse.Value = "True";
            xnTemplate.Attributes.Append(xaGridUse);

            String returnValue = xnTemplate.OuterXml;

            FSLComboDataSource.Add(dataSourcrName);
            return returnValue.Replace("xmlns:SLTools=\"infolight\"", String.Empty).Replace("xmlns:x=\"infolight\"", String.Empty);
        }

        private void SetBlockFieldControls(TBlockItem BlockItem)
        {
            int columnCount = 0;
            if (BlockItem.Name == "View")
            {
                if (FClientData.BaseFormName == "SL3DCard" || FClientData.BaseFormName == "SL3DCardMasterDetail"
                    || FClientData.BaseFormName == "SLFormList" || FClientData.BaseFormName == "SLFormListMasterDetail")
                    columnCount = 1;
                else
                    columnCount = 2;
                if (FClientData.ColumnCount != 0)
                    columnCount = FClientData.ColumnCount;
                GenViewBlockControl(BlockItem, columnCount);

                if (FClientData.BaseFormName == "SLFormList" || FClientData.BaseFormName == "SLFormListMasterDetail")
                {
                    int itemHeight = 46 + BlockItem.BlockFieldItems.Count * 30 + 34;
                    WriteToXaml(FClientData.FullXamlName,
                                "Name=\"formlist1\" ItemHeight=\"150\"",
                                String.Format("Name=\"formlist1\" ItemHeight=\"{0}\" FormListItemTitle=\"{1}\"",
                                itemHeight.ToString(),
                                FClientData.FormTitle));
                }
                else if (FClientData.BaseFormName == "SL3DCard" || FClientData.BaseFormName == "SL3DCardMasterDetail")
                {
                    WriteToXaml(FClientData.FullXamlName,
                                "Name=\"slcardMaster\"",
                                String.Format("Name=\"slcardMaster\" CardItemTitle=\"{0}\"",
                                FClientData.FormTitle));
                }
            }
            else if (BlockItem.Name == "Main" || BlockItem.Name == "Master")
            {
                if (FClientData.BaseFormName == "SLSingle" || FClientData.BaseFormName == "SLQuery"
                    || FClientData.BaseFormName == "SLMasterDetail" || FClientData.BaseFormName == "SLMasterDetail2"
                    || FClientData.BaseFormName == "SLMasterDetail3"
                    || FClientData.BaseFormName == "SLFormList" || FClientData.BaseFormName == "SLFormListMasterDetail")
                {
                    columnCount = 2;

                }
                else if (FClientData.BaseFormName == "SL3DCard" || FClientData.BaseFormName == "SL3DCardMasterDetail")
                {
                    columnCount = 2;
                    //GenMainBlockControlFor3DCard(BlockItem, columnCount);
                }
                if (FClientData.ColumnCount != 0)
                    columnCount = FClientData.ColumnCount;
                GenMainBlockControl(BlockItem, columnCount);
            }
        }

        private void GenBlock(TBlockItem BlockItem, string DataSetName, bool GenField)
        {
            SetBlockFieldControls(BlockItem);
        }

        private void WriteToXaml(String FullName, String OldString, String NewString)
        {
            if (!File.Exists(FullName))
                return;
            System.IO.StreamReader SR = new System.IO.StreamReader(FullName);
            String Context = SR.ReadToEnd();
            SR.Close();
            Context = Context.Replace(OldString, NewString);
            System.IO.FileStream Filefs = new System.IO.FileStream(FullName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            System.IO.StreamWriter SW = new System.IO.StreamWriter(Filefs);
            SW.Write(Context);
            SW.Close();
            Filefs.Close();
        }

        private void GenDataSource()
        {
            if (FClientData.BaseFormName == "SLSingle" || FClientData.BaseFormName == "SLQuery"
                || FClientData.BaseFormName == "SLMasterDetail" || FClientData.BaseFormName == "SLMasterDetail2"
                || FClientData.BaseFormName == "SLMasterDetail3"
                || FClientData.BaseFormName == "SL3DCard" || FClientData.BaseFormName == "SL3DCardMasterDetail"
                || FClientData.BaseFormName == "SLFormList" || FClientData.BaseFormName == "SLFormListMasterDetail")
            {
                String oldString = "ServiceDataSource x:Name=\"SDSMaster\"";
                String newString = String.Empty;
                if (!String.IsNullOrEmpty(FClientData.RemoteName))
                    newString = String.Format("ServiceDataSource x:Name=\"SDSMaster\" RemoteName=\"{0}\"", FClientData.RemoteName);
                else if (!String.IsNullOrEmpty(FClientData.ProviderName))
                    newString = String.Format("ServiceDataSource x:Name=\"SDSMaster\" RemoteName=\"{0}\" UseDataSet=\"True\"", FClientData.ProviderName);

                WriteToXaml(FClientData.FullXamlName, oldString, newString);
            }

            if (FClientData.BaseFormName == "SLMasterDetail3")
            {
                String oldString = "ServiceDataSource x:Name=\"SDSView\"";
                String newString = String.Empty;
                if (!String.IsNullOrEmpty(FClientData.RemoteName))
                    newString = String.Format("ServiceDataSource x:Name=\"SDSView\" RemoteName=\"{0}\"", FClientData.ViewProviderName);
                else if (!String.IsNullOrEmpty(FClientData.ProviderName))
                    newString = String.Format("ServiceDataSource x:Name=\"SDSView\" RemoteName=\"{0}\" UseDataSet=\"True\"", FClientData.ViewProviderName);

                WriteToXaml(FClientData.FullXamlName, oldString, newString);
            }

            if (FClientData.BaseFormName == "SLMasterDetail" || FClientData.BaseFormName == "SLMasterDetail2"
                || FClientData.BaseFormName == "SLMasterDetail3"
                || FClientData.BaseFormName == "SL3DCardMasterDetail"
                || FClientData.BaseFormName == "SLFormListMasterDetail")
            {
                String oldString = "Binding Path=Data.DetailTableName, ElementName=SDSMaster";
                //String detailEntityName = WzdUtils.GetServerEntityClassName(FClientData.AssemblyName, FClientData.DetailEntityName);
                try
                {
                    
                    String newString = String.Format("Binding Path=Data.{0}, ElementName=SDSMaster", FClientData.DetailEntitySetName);
                    WriteToXaml(FClientData.FullXamlName, oldString, newString);
                }
                catch (Exception ex)
                {
                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                }
            }
        }

        private void GenDetailBlock(TBlockItem BlockItem, int columnCount)
        {
            String strGridViewColumns = ComposeGridViewXML(BlockItem.BlockFieldItems);
            WriteToXaml(FClientData.FullXamlName,
                        "<SLTools:SLDataGrid.Columns><data:DataGridTextColumn Header=\"gvDetailDataGridColumns\" /></SLTools:SLDataGrid.Columns>",
                        strGridViewColumns);

            String strReadOnlyTemplate = ComposeFormViewXML(BlockItem.BlockFieldItems, "ReadOnlyTemplate", columnCount);
            WriteToXaml(FClientData.FullXamlName, "<Grid><toolkit:DataField x:Name=\"fvDetailReadOnlyTemplate\" /></Grid>", strReadOnlyTemplate);
            String strNewItemTemplate = ComposeFormViewXML(BlockItem.BlockFieldItems, "NewItemTemplate", columnCount);
            WriteToXaml(FClientData.FullXamlName, "<Grid><toolkit:DataField x:Name=\"fvDetailNewItemTemplate\" /></Grid>", strNewItemTemplate);
            String strEditTemplate = ComposeFormViewXML(BlockItem.BlockFieldItems, "EditTemplate", columnCount);
            WriteToXaml(FClientData.FullXamlName, "<Grid><toolkit:DataField x:Name=\"fvDetailEditTemplate\" /></Grid>", strEditTemplate);
        }

        private void GenDefault(TBlockItem BlockItem, String defaultName, String dataObjectID)
        {
            String strOldValue = "<SLTools:SLDefault x:Name=\"" + defaultName + "\" DataObjectID=\"" + dataObjectID + "\"></SLTools:SLDefault>";
            String strDefault = ComposeDefaultXML(BlockItem.BlockFieldItems, defaultName, dataObjectID);
            WriteToXaml(FClientData.FullXamlName,
                        strOldValue,
                        strDefault);
        }

        private void GenClientQuery(TBlockItem BlockItem, String clientQueryName, String dataControlID)
        {
            //String strOldValue = "<SLTools:SLClientQuery x:Name=\"" + clientQueryName + "\" DataControlID=\"" + dataControlID + "\"></SLTools:SLClientQuery>";
            String strOldValue = "<SLTools:SLClientQuery.Columns><SLTools:QueryColumns ColumnName=\"" + clientQueryName + "Columns\"/></SLTools:SLClientQuery.Columns>";
            String strClientQuery = ComposeClientQueryXML(BlockItem.BlockFieldItems, clientQueryName, dataControlID);
            WriteToXaml(FClientData.FullXamlName,
                        strOldValue,
                        strClientQuery);
        }

        private String ComposeDefaultXML(TBlockFieldItems BlockItems, String defaultName, String dataObjectID)
        {
            XmlDocument xmlTemplate = new XmlDocument();
            XmlNode xnTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLDefault", "infolight");
            XmlAttribute xaName = xmlTemplate.CreateAttribute("x", "Name", "infolight");
            xaName.Value = defaultName;
            xnTemplate.Attributes.Append(xaName);

            XmlAttribute xaDataObjectID = xmlTemplate.CreateAttribute("DataObjectID");
            xaDataObjectID.Value = dataObjectID;
            xnTemplate.Attributes.Append(xaDataObjectID);

            if (BlockItems.Count > 0)
            {
                XmlNode xnDefaultValues = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLDefault.DefaultValues", "infolight");
                foreach (TBlockFieldItem BlockItem in BlockItems)
                {
                    if (!String.IsNullOrEmpty(BlockItem.DefaultValue))
                    {
                        XmlNode xnSLDefaultItem = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLDefaultItem", "infolight");
                        XmlAttribute xaDefaultValue = xmlTemplate.CreateAttribute("DefaultValue", "");
                        xaDefaultValue.Value = BlockItem.DefaultValue;
                        xnSLDefaultItem.Attributes.Append(xaDefaultValue);

                        XmlAttribute xaFieldName = xmlTemplate.CreateAttribute("FieldName");
                        xaFieldName.Value = BlockItem.DataField;
                        xnSLDefaultItem.Attributes.Append(xaFieldName);

                        xnDefaultValues.AppendChild(xnSLDefaultItem);
                    }
                }
                if (xnDefaultValues.ChildNodes.Count > 0)
                    xnTemplate.AppendChild(xnDefaultValues);
            }

            String returnValue = xnTemplate.OuterXml;

            //FSLComboDataSource.Add(dataSourcrName);
            return returnValue.Replace("xmlns:SLTools=\"infolight\"", String.Empty).Replace("xmlns:x=\"infolight\"", String.Empty);
        }

        private String ComposeSLCardToolTipXML(TBlockFieldItems BlockItems)
        {
            String tooltipField = String.Empty;
            String tooltipFormat = String.Empty;
            foreach (TBlockFieldItem BlockItem in BlockItems)
            {
                if (BlockItem.IsKey)
                {
                    tooltipField = String.Format("TooltipField=\"{0}\" ", BlockItem.DataField);
                    tooltipFormat = "TooltipFormat=\"此筆記錄為'{0}'的" + BlockItem.Description + "\"";
                    break;
                }
            }

            return tooltipField + tooltipFormat;
        }

        private String ComposeClientQueryXML(TBlockFieldItems BlockItems, String clientQueryName, String dataControlID)
        {
            String returnValue = String.Empty;

            XmlDocument xmlTemplate = new XmlDocument();
            //XmlNode xnTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLClientQuery", "infolight");
            //XmlAttribute xaName = xmlTemplate.CreateAttribute("x", "Name", "infolight");
            //xaName.Value = clientQueryName;
            //xnTemplate.Attributes.Append(xaName);

            //XmlAttribute xaDataObjectID = xmlTemplate.CreateAttribute("DataControlID");
            //xaDataObjectID.Value = dataControlID;
            //xnTemplate.Attributes.Append(xaDataObjectID);

            if (BlockItems.Count > 0)
            {
                XmlNode xnClientQueryColumns = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLClientQuery.Columns", "infolight");
                foreach (TBlockFieldItem BlockItem in BlockItems)
                {
                    if (!String.IsNullOrEmpty(BlockItem.QueryMode) && BlockItem.QueryMode != "None")
                    {
                        if (BlockItem.QueryMode == "Normal")
                        {
                            XmlNode xnSLQueryColumns = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "QueryColumns", "infolight");
                            XmlAttribute xaFName = xmlTemplate.CreateAttribute("ColumnName", "");
                            xaFName.Value = BlockItem.DataField;
                            xnSLQueryColumns.Attributes.Append(xaFName);

                            XmlAttribute xaFieldName = xmlTemplate.CreateAttribute("FieldName", "");
                            xaFieldName.Value = BlockItem.DataField;
                            xnSLQueryColumns.Attributes.Append(xaFieldName);

                            XmlAttribute xaCondition = xmlTemplate.CreateAttribute("Condition");
                            if (BlockItem.DataType == typeof(String))
                                xaCondition.Value = "%";
                            else
                                xaCondition.Value = "=";
                            xnSLQueryColumns.Attributes.Append(xaCondition);

                            XmlAttribute xaCaption = xmlTemplate.CreateAttribute("Caption", "");
                            xaCaption.Value = BlockItem.Description;
                            xnSLQueryColumns.Attributes.Append(xaCaption);

                            if (FClientData.BaseFormName == "SLQuery")
                            {
                                XmlAttribute xaDefaultValue = xmlTemplate.CreateAttribute("DefaultValue", "");
                                xaDefaultValue.Value = BlockItem.DefaultValue;
                                xnSLQueryColumns.Attributes.Append(xaDefaultValue);
                            }

                            XmlAttribute xaColumnType = xmlTemplate.CreateAttribute("ColumnType", "");
                            if (!String.IsNullOrEmpty(BlockItem.ControlType) && BlockItem.ControlType != "ValidateBox")
                                xaColumnType.Value = BlockItem.ControlType;
                            else
                                xaColumnType.Value = "TextBox";

                            xnSLQueryColumns.Attributes.Append(xaColumnType);

                            xnClientQueryColumns.AppendChild(xnSLQueryColumns);

                            if (BlockItem.ControlType == "ComboBox")
                            {
                                XmlAttribute xaComboItemSource = xmlTemplate.CreateAttribute("ComboItemSource", "");
                                xaComboItemSource.Value = "combo" + BlockItem.ComboEntityName;
                                xnSLQueryColumns.Attributes.Append(xaComboItemSource);

                                XmlAttribute xaComboDisplayMember = xmlTemplate.CreateAttribute("ComboDisplayMember", "");
                                xaComboDisplayMember.Value = BlockItem.ComboTextField;
                                xnSLQueryColumns.Attributes.Append(xaComboDisplayMember);

                                XmlAttribute xaComboValueMember = xmlTemplate.CreateAttribute("ComboValueMember", "");
                                xaComboValueMember.Value = BlockItem.ComboValueField;
                                xnSLQueryColumns.Attributes.Append(xaComboValueMember);
                            }
                            else if (BlockItem.ControlType == "RefValBox")
                            {
                                XmlAttribute xaComboItemSource = xmlTemplate.CreateAttribute("ComboItemSource", "");
                                xaComboItemSource.Value = "refval" + BlockItem.ComboEntityName;
                                xnSLQueryColumns.Attributes.Append(xaComboItemSource);

                                XmlAttribute xaComboDisplayMember = xmlTemplate.CreateAttribute("ComboDisplayMember", "");
                                xaComboDisplayMember.Value = BlockItem.ComboTextField;
                                xnSLQueryColumns.Attributes.Append(xaComboDisplayMember);

                                XmlAttribute xaComboDisplayMemberCaption = xmlTemplate.CreateAttribute("ComboDisplayMemberCaption", "");
                                xaComboDisplayMemberCaption.Value = String.IsNullOrEmpty(BlockItem.ComboTextFieldCaption) ? BlockItem.ComboTextField : BlockItem.ComboTextFieldCaption;
                                xnSLQueryColumns.Attributes.Append(xaComboDisplayMemberCaption);

                                XmlAttribute xaComboValueMember = xmlTemplate.CreateAttribute("ComboValueMember", "");
                                xaComboValueMember.Value = BlockItem.ComboValueField;
                                xnSLQueryColumns.Attributes.Append(xaComboValueMember);

                                XmlAttribute xaComboValueMemberCaption = xmlTemplate.CreateAttribute("ComboValueMemberCaption", "");
                                xaComboValueMemberCaption.Value = String.IsNullOrEmpty(BlockItem.ComboValueFieldCaption) ? BlockItem.ComboValueField : BlockItem.ComboValueFieldCaption;
                                xnSLQueryColumns.Attributes.Append(xaComboValueMemberCaption);

                                if (BlockItem.ComboOtherFields != null)
                                {
                                    String strOtherFields = String.Empty;
                                    String strOtherFieldsCaption = String.Empty;
                                    foreach (OtherField of in BlockItem.ComboOtherFields)
                                    {
                                        strOtherFields += of.FieldName + ";";
                                        strOtherFieldsCaption += of.FieldCaption + ";";
                                    }
                                    XmlAttribute xaComboOtherFields = xmlTemplate.CreateAttribute("ComboOtherFields", "");
                                    xaComboOtherFields.Value = strOtherFields;
                                    xnSLQueryColumns.Attributes.Append(xaComboOtherFields);

                                    XmlAttribute xaComboOtherFieldsCaption = xmlTemplate.CreateAttribute("ComboOtherFieldsCaption", "");
                                    xaComboOtherFieldsCaption.Value = strOtherFieldsCaption;
                                    xnSLQueryColumns.Attributes.Append(xaComboOtherFieldsCaption);
                                }
                            }
                        }
                        else if (BlockItem.QueryMode == "Range")
                        {
                            XmlNode xnQueryColumnsG = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "QueryColumns", "infolight");
                            XmlAttribute xaFNameG = xmlTemplate.CreateAttribute("ColumnName", "");
                            xaFNameG.Value = BlockItem.DataField + "G";
                            xnQueryColumnsG.Attributes.Append(xaFNameG);

                            XmlAttribute xaFieldNameG = xmlTemplate.CreateAttribute("FieldName", "");
                            xaFieldNameG.Value = BlockItem.DataField;
                            xnQueryColumnsG.Attributes.Append(xaFieldNameG);

                            XmlAttribute xaConditionG = xmlTemplate.CreateAttribute("Condition");
                            xaConditionG.Value = ">=";
                            xnQueryColumnsG.Attributes.Append(xaConditionG);

                            XmlAttribute xaCaption = xmlTemplate.CreateAttribute("Caption", "");
                            xaCaption.Value = BlockItem.Description;
                            xnQueryColumnsG.Attributes.Append(xaCaption);

                            XmlAttribute xaDefaultValueG = xmlTemplate.CreateAttribute("DefaultValue", "");
                            xaDefaultValueG.Value = BlockItem.DefaultValue;
                            xnQueryColumnsG.Attributes.Append(xaDefaultValueG);

                            XmlAttribute xaColumnTypeG = xmlTemplate.CreateAttribute("ColumnType", "");
                            if (!String.IsNullOrEmpty(BlockItem.ControlType) && BlockItem.ControlType != "ValidateBox")
                                xaColumnTypeG.Value = BlockItem.ControlType;
                            else
                                xaColumnTypeG.Value = "TextBox";
                            xnQueryColumnsG.Attributes.Append(xaColumnTypeG);

                            xnClientQueryColumns.AppendChild(xnQueryColumnsG);

                            if (BlockItem.ControlType == "ComboBox")
                            {
                                XmlAttribute xaComboItemSource = xmlTemplate.CreateAttribute("ComboItemSource", "");
                                xaComboItemSource.Value = "combo" + BlockItem.ComboEntityName;
                                xnQueryColumnsG.Attributes.Append(xaComboItemSource);

                                XmlAttribute xaComboDisplayMember = xmlTemplate.CreateAttribute("ComboDisplayMember", "");
                                xaComboDisplayMember.Value = BlockItem.ComboTextField;
                                xnQueryColumnsG.Attributes.Append(xaComboDisplayMember);

                                XmlAttribute xaComboValueMember = xmlTemplate.CreateAttribute("ComboValueMember", "");
                                xaComboValueMember.Value = BlockItem.ComboValueField;
                                xnQueryColumnsG.Attributes.Append(xaComboValueMember);
                            }
                            else if (BlockItem.ControlType == "RefValBox")
                            {
                                XmlAttribute xaComboItemSource = xmlTemplate.CreateAttribute("ComboItemSource", "");
                                xaComboItemSource.Value = "refval" + BlockItem.ComboEntityName;
                                xnQueryColumnsG.Attributes.Append(xaComboItemSource);

                                XmlAttribute xaComboDisplayMember = xmlTemplate.CreateAttribute("ComboDisplayMember", "");
                                xaComboDisplayMember.Value = BlockItem.ComboTextField;
                                xnQueryColumnsG.Attributes.Append(xaComboDisplayMember);

                                XmlAttribute xaComboDisplayMemberCaption = xmlTemplate.CreateAttribute("ComboDisplayMemberCaption", "");
                                xaComboDisplayMemberCaption.Value = String.IsNullOrEmpty(BlockItem.ComboTextFieldCaption) ? BlockItem.ComboTextField : BlockItem.ComboTextFieldCaption;
                                xnQueryColumnsG.Attributes.Append(xaComboDisplayMemberCaption);

                                XmlAttribute xaComboValueMember = xmlTemplate.CreateAttribute("ComboValueMember", "");
                                xaComboValueMember.Value = BlockItem.ComboValueField;
                                xnQueryColumnsG.Attributes.Append(xaComboValueMember);

                                XmlAttribute xaComboValueMemberCaption = xmlTemplate.CreateAttribute("ComboValueMemberCaption", "");
                                xaComboValueMemberCaption.Value = String.IsNullOrEmpty(BlockItem.ComboValueFieldCaption) ? BlockItem.ComboValueField : BlockItem.ComboValueFieldCaption;
                                xnQueryColumnsG.Attributes.Append(xaComboValueMemberCaption);

                                if (BlockItem.ComboOtherFields != null)
                                {
                                    String strOtherFields = String.Empty;
                                    String strOtherFieldsCaption = String.Empty;
                                    foreach (OtherField of in BlockItem.ComboOtherFields)
                                    {
                                        strOtherFields += of.FieldName + ";";
                                        strOtherFieldsCaption += of.FieldCaption + ";";
                                    }
                                    XmlAttribute xaComboOtherFieldsG = xmlTemplate.CreateAttribute("ComboOtherFields", "");
                                    xaComboOtherFieldsG.Value = strOtherFields;
                                    xnQueryColumnsG.Attributes.Append(xaComboOtherFieldsG);

                                    XmlAttribute xaComboOtherFieldsCaption = xmlTemplate.CreateAttribute("ComboOtherFieldsCaption", "");
                                    xaComboOtherFieldsCaption.Value = strOtherFields;
                                    xnQueryColumnsG.Attributes.Append(xaComboOtherFieldsCaption);
                                }
                            }

                            XmlNode xnQueryColumnsL = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "QueryColumns", "infolight");
                            XmlAttribute xaFNameL = xmlTemplate.CreateAttribute("ColumnName", "");
                            xaFNameL.Value = BlockItem.DataField + "L";
                            xnQueryColumnsL.Attributes.Append(xaFNameL);

                            XmlAttribute xaFieldNameL = xmlTemplate.CreateAttribute("FieldName", "");
                            xaFieldNameL.Value = BlockItem.DataField;
                            xnQueryColumnsL.Attributes.Append(xaFieldNameL);

                            XmlAttribute xaConditionL = xmlTemplate.CreateAttribute("Condition");
                            xaConditionL.Value = "<=";
                            xnQueryColumnsL.Attributes.Append(xaConditionL);

                            XmlAttribute xaCaptionL = xmlTemplate.CreateAttribute("Caption", "");
                            xaCaptionL.Value = BlockItem.Description;
                            xnQueryColumnsL.Attributes.Append(xaCaptionL);

                            XmlAttribute xaDefaultValueL = xmlTemplate.CreateAttribute("DefaultValue", "");
                            xaDefaultValueL.Value = BlockItem.DefaultValue;
                            xnQueryColumnsL.Attributes.Append(xaDefaultValueL);

                            XmlAttribute xaColumnTypeL = xmlTemplate.CreateAttribute("ColumnType", "");
                            if (!String.IsNullOrEmpty(BlockItem.ControlType) && BlockItem.ControlType != "ValidateBox")
                                xaColumnTypeL.Value = BlockItem.ControlType;
                            else
                                xaColumnTypeL.Value = "TextBox";
                            xnQueryColumnsL.Attributes.Append(xaColumnTypeL);

                            XmlAttribute xaNewLineL = xmlTemplate.CreateAttribute("NewLine", "");
                            xaNewLineL.Value = "False";
                            xnQueryColumnsL.Attributes.Append(xaNewLineL);

                            xnClientQueryColumns.AppendChild(xnQueryColumnsL);

                            if (BlockItem.ControlType == "ComboBox")
                            {
                                XmlAttribute xaComboItemSource = xmlTemplate.CreateAttribute("ComboItemSource", "");
                                xaComboItemSource.Value = "combo" + BlockItem.ComboEntityName;
                                xnQueryColumnsL.Attributes.Append(xaComboItemSource);

                                XmlAttribute xaComboDisplayMember = xmlTemplate.CreateAttribute("ComboDisplayMember", "");
                                xaComboDisplayMember.Value = BlockItem.ComboTextField;
                                xnQueryColumnsL.Attributes.Append(xaComboDisplayMember);

                                XmlAttribute xaComboValueMember = xmlTemplate.CreateAttribute("ComboValueMember", "");
                                xaComboValueMember.Value = BlockItem.ComboValueField;
                                xnQueryColumnsL.Attributes.Append(xaComboValueMember);
                            }
                            else if (BlockItem.ControlType == "RefValBox")
                            {
                                XmlAttribute xaComboItemSource = xmlTemplate.CreateAttribute("ComboItemSource", "");
                                xaComboItemSource.Value = "refval" + BlockItem.ComboEntityName;
                                xnQueryColumnsL.Attributes.Append(xaComboItemSource);

                                XmlAttribute xaComboDisplayMember = xmlTemplate.CreateAttribute("ComboDisplayMember", "");
                                xaComboDisplayMember.Value = BlockItem.ComboTextField;
                                xnQueryColumnsL.Attributes.Append(xaComboDisplayMember);

                                XmlAttribute xaComboDisplayMemberCaption = xmlTemplate.CreateAttribute("ComboDisplayMemberCaption", "");
                                xaComboDisplayMemberCaption.Value = String.IsNullOrEmpty(BlockItem.ComboTextFieldCaption) ? BlockItem.ComboTextField : BlockItem.ComboTextFieldCaption;
                                xnQueryColumnsL.Attributes.Append(xaComboDisplayMemberCaption);

                                XmlAttribute xaComboValueMember = xmlTemplate.CreateAttribute("ComboValueMember", "");
                                xaComboValueMember.Value = BlockItem.ComboValueField;
                                xnQueryColumnsL.Attributes.Append(xaComboValueMember);

                                XmlAttribute xaComboValueMemberCaption = xmlTemplate.CreateAttribute("ComboValueMemberCaption", "");
                                xaComboValueMemberCaption.Value = String.IsNullOrEmpty(BlockItem.ComboValueFieldCaption) ? BlockItem.ComboValueField : BlockItem.ComboValueFieldCaption;
                                xnQueryColumnsL.Attributes.Append(xaComboValueMemberCaption);

                                if (BlockItem.ComboOtherFields != null)
                                {
                                    String strOtherFields = String.Empty;
                                    String strOtherFieldsCaption = String.Empty;
                                    foreach (OtherField of in BlockItem.ComboOtherFields)
                                    {
                                        strOtherFields += of.FieldName + ";";
                                        strOtherFieldsCaption += of.FieldCaption + ";";
                                    }
                                    XmlAttribute xaComboOtherFieldsL = xmlTemplate.CreateAttribute("ComboOtherFields", "");
                                    xaComboOtherFieldsL.Value = strOtherFields;
                                    xnQueryColumnsL.Attributes.Append(xaComboOtherFieldsL);

                                    XmlAttribute xaComboOtherFieldsCaption = xmlTemplate.CreateAttribute("ComboOtherFieldsCaption", "");
                                    xaComboOtherFieldsCaption.Value = strOtherFieldsCaption;
                                    xnQueryColumnsL.Attributes.Append(xaComboOtherFieldsCaption);
                                }
                            }
                        }
                    }
                }

                if (xnClientQueryColumns.OuterXml != "<SLTools:SLClientQuery.Columns xmlns:SLTools=\"infolight\" />")
                    returnValue = xnClientQueryColumns.OuterXml;
                //if (xnClientQueryColumns.ChildNodes.Count > 0)
                //    xnTemplate.AppendChild(xnClientQueryColumns);
            }
            //String returnValue = xnTemplate.OuterXml;

            //FSLComboDataSource.Add(dataSourcrName);
            return returnValue.Replace("xmlns:SLTools=\"infolight\"", String.Empty).Replace("xmlns:x=\"infolight\"", String.Empty);
        }

        private void GenValidator(TBlockItem BlockItem, String validatorName, String dataObjectID)
        {
            String strOldValue = "<SLTools:SLValidator x:Name=\"" + validatorName + "\" DataObjectID=\"" + dataObjectID + "\"></SLTools:SLValidator>";
            String strDefault = ComposeValidatorXML(BlockItem.BlockFieldItems, validatorName, dataObjectID);
            WriteToXaml(FClientData.FullXamlName,
                        strOldValue,
                        strDefault);
        }

        private String ComposeValidatorXML(TBlockFieldItems BlockItems, String validatorName, String dataObjectID)
        {
            XmlDocument xmlTemplate = new XmlDocument();
            XmlNode xnTemplate = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLValidator", "infolight");
            XmlAttribute xaName = xmlTemplate.CreateAttribute("x", "Name", "infolight");
            xaName.Value = validatorName;
            xnTemplate.Attributes.Append(xaName);

            XmlAttribute xaDataObjectID = xmlTemplate.CreateAttribute("DataObjectID");
            xaDataObjectID.Value = dataObjectID;
            xnTemplate.Attributes.Append(xaDataObjectID);

            if (BlockItems.Count > 0)
            {
                XmlNode xnDefaultValues = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLValidator.Validates", "infolight");
                foreach (TBlockFieldItem BlockItem in BlockItems)
                {
                    if (BlockItem.CheckNull != null && BlockItem.CheckNull != String.Empty && BlockItem.CheckNull != "N")
                    {
                        XmlNode xnSLDefaultItem = xmlTemplate.CreateNode(XmlNodeType.Element, "SLTools", "SLValidateItem", "infolight");
                        XmlAttribute xaCheckNull = xmlTemplate.CreateAttribute("CheckNull", "");
                        if (BlockItem.CheckNull.ToUpper() == "Y")
                            xaCheckNull.Value = "True";
                        else
                            xaCheckNull.Value = "False";
                        xnSLDefaultItem.Attributes.Append(xaCheckNull);

                        XmlAttribute xaFieldName = xmlTemplate.CreateAttribute("FieldName");
                        xaFieldName.Value = BlockItem.DataField;
                        xnSLDefaultItem.Attributes.Append(xaFieldName);

                        XmlAttribute xaCaptionControlName = xmlTemplate.CreateAttribute("CaptionControlName");
                        xaCaptionControlName.Value = BlockItem.DataField;
                        xnSLDefaultItem.Attributes.Append(xaCaptionControlName);

                        xnDefaultValues.AppendChild(xnSLDefaultItem);
                    }
                }
                if (xnDefaultValues.ChildNodes.Count > 0)
                    xnTemplate.AppendChild(xnDefaultValues);
            }
            String returnValue = xnTemplate.OuterXml;

            //FSLComboDataSource.Add(dataSourcrName);
            return returnValue.Replace("xmlns:SLTools=\"infolight\"", String.Empty).Replace("xmlns:x=\"infolight\"", String.Empty);
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

        [DllImport("kernel32.dll")]
        public extern static void Sleep(uint msec);

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
                    GenDataSource(); //???
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
                            GenDefault(BlockItem, "defMaster", "fvMaster");
                            GenValidator(BlockItem, "valMaster", "fvMaster");
                            GenClientQuery(BlockItem, "cqMaster", "SDSMaster");
                        }

                        BlockItem = FClientData.Blocks.FindItem(FClientData.DetailEntityName);
                        if (BlockItem != null)
                        {
                            int columnCount = 2;
                            GenDetailBlock(BlockItem, columnCount);
                            GenDefault(BlockItem, "defDetail", "fvDetail");
                            GenValidator(BlockItem, "valDetail", "fvDetail");
                        }
                    }
                    else
                    {
                        BlockItem = FClientData.Blocks.FindItem("View");
                        if (BlockItem != null)
                        {
                            GenBlock(BlockItem, "View", false);
                        }
                        TBlockItem MainBlockItem = FClientData.Blocks.FindItem("Main");
                        if (MainBlockItem != null)
                        {
                            GenBlock(MainBlockItem, "Main", false);
                            GenDefault(MainBlockItem, "defMaster", "fvMaster");
                            GenValidator(MainBlockItem, "valMaster", "fvMaster");
                            GenClientQuery(MainBlockItem, "cqMaster", "SDSMaster");
                            //UpdateDataSource(MainBlockItem, BlockItem);
                        }
                    }
                }
                catch (Exception exception2)
                {
                    MessageBox.Show(exception2.Message);
                    return;
                }
                finally
                {
                }

                Window W = FPI.Open("{00000000-0000-0000-0000-000000000000}");
                W.Activate();

                FProject.Save(FProject.FullName);
                FDTE2.Solution.SolutionBuild.BuildProject(FDTE2.Solution.SolutionBuild.ActiveConfiguration.Name,
                    FProject.FullName, true);
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
