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
#if VS90
using WebDevPage = Microsoft.VisualWebDeveloper.Interop.WebDeveloperPage;
#endif
using System.Linq;
using MWizard.WCF;
using EFClientTools.Beans;
using System.Threading;
using EFClientTools.EFServerReference;

namespace MWizard
{
    public partial class fmEEPWCFWebWizard : Form
    {
        private TWCFWebClientData FClientData;
        private DTE2 FDTE2;
        private AddIn FAddIn;
        private DbConnection InternalConnection = null;
        private static string _serverPath;
        private InfoDataSet FInfoDataSet = null;
        public Boolean SDCall = false;
        private ListViewColumnSorter lvwColumnSorterViewSrc;
        private ListViewColumnSorter lvwColumnSorterViewDes;
        private ListViewColumnSorter lvwColumnSorterMasterSrc;
        private ListViewColumnSorter lvwColumnSorterMasterDes;
        private ListViewColumnSorter lvwColumnSorterDetail;

        public fmEEPWCFWebWizard()
        {
            InitializeComponent();
            FClientData = new TWCFWebClientData(this);
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

        public fmEEPWCFWebWizard(DTE2 aDTE2, AddIn aAddIn)
        {
            InitializeComponent();
            FClientData = new TWCFWebClientData(this);
            FDTE2 = aDTE2;
            FAddIn = aAddIn;
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
            tbCommandName.Text = "";
            tbEntityName.Text = "";
            tbRemoteName.Text = "";
            tbFormName.Text = "Form1";
            tbDetailTableName.Text = "";
            cbDetailEntityName.Items.Clear();
            cbDetailEntityName.Text = "";
            lvTemplate.Items[0].Selected = true;
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
            FInfoDataSet = new InfoDataSet();
            if (((FDTE2 != null) && (FDTE2.Solution.FileName != "")) && File.Exists(FDTE2.Solution.FileName))
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
            if ((fmEEPWCFWebWizard._serverPath == null) || (fmEEPWCFWebWizard._serverPath.Length == 0))
            {
                fmEEPWCFWebWizard._serverPath = EEPRegistry.Server + "\\";
            }
            return fmEEPWCFWebWizard._serverPath;
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

        public void ShowWebClientWizard()
        {
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
            TWCFWebClientGenerator CG = new TWCFWebClientGenerator(FClientData, FDTE2, FAddIn);
            CG.GenWebClientModule();
            SDCall = false;
        }

        private void SetFieldNames(String TableName, ListView LV)
        {
            try
            {
                Dictionary<string, object> htFields = WzdUtils.GetFieldsByEntityName(FClientData.AssemblyName, FClientData.CommandName, tbEntityName.Text);
                List<String> keyFields = WzdUtils.GetEntityPrimaryKeys(FClientData.AssemblyName, FClientData.CommandName, tbEntityName.Text);

                //Dictionary<string, object> htFields = WzdUtils.GetFieldsByEntityName(FClientData.AssemblyName, FClientData.CommandName, TableName);
                //List<string> keyFields = WzdUtils.GetEntityPrimaryKeys(FClientData.AssemblyName, FClientData.CommandName, TableName);

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

                    if (keyFields != null && keyFields.Count != 0)
                    {
                        if (keyFields.Contains(aBlockFieldItem.DataField))
                        {
                            aBlockFieldItem.IsKey = true;
                        }
                    }

                    aBlockFieldItem.DataType = (Type)field.Value;
                    lvi.Tag = aBlockFieldItem;
                    if (colDefObject != null)
                    {

                        lvi.SubItems.Add(colDefObject.CAPTION);

                        aBlockFieldItem.Description = colDefObject.CAPTION;
                        aBlockFieldItem.CheckNull = colDefObject.CHECK_NULL == null ? null : colDefObject.CHECK_NULL.ToUpper();
                        aBlockFieldItem.DefaultValue = colDefObject.DEFAULT_VALUE;
                        aBlockFieldItem.ControlType = colDefObject.NEEDBOX;
                        aBlockFieldItem.EditMask = colDefObject.EDITMASK;
                        if (string.Compare(colDefObject.IS_KEY, "Y", true) == 0)
                        {
                            aBlockFieldItem.IsKey = true;
                        }
                        if (aBlockFieldItem.DataType == typeof(DateTime))
                        {
                            if (aBlockFieldItem.ControlType == null || aBlockFieldItem.ControlType == "")
                                aBlockFieldItem.ControlType = "DateTimeBox";
                        }
                        aBlockFieldItem.QueryMode = colDefObject.QUERYMODE;
                        aBlockFieldItem.Length = colDefObject.FIELD_LENGTH;
                    }
                    LV.Items.Add(lvi);
                }
            }
            catch (Exception ex)
            {
                WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                return;
            }
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
                        aItem.SubItems.Add("");
                        aFieldItem.Description = "";
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

                    if (cbChooseLanguage.Text == "" || cbChooseLanguage.Text == "C#")
                        FClientData.Language = "cs";
                    else if (cbChooseLanguage.Text == "VB")
                        FClientData.Language = "vb";

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
                        tbRemoteName.Text = "";
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
                        FClientData.BaseFormName = lvTemplate.SelectedItems[0].SubItems[1].Text;
                        cbDetailEntityName.Visible = (FClientData.BaseFormName.CompareTo("WCFWMasterDetail") == 0);
                        label35.Visible = cbDetailEntityName.Visible;
                        DisplayPage(tpDataSource);
                    }
                }
                else if (tabControl.SelectedTab.Equals(tpDataSource))
                {
                    if (tbRemoteName.Text == "")
                    {
                        MessageBox.Show("Please input Provider Name !!");
                        if (tbRemoteName.CanFocus)
                        {
                            tbRemoteName.Focus();
                        }
                    }
                    else if (tbCommandName.Text == "")
                    {
                        MessageBox.Show("Please input Table Name !!");
                        if (tbCommandName.CanFocus)
                        {
                            tbCommandName.Focus();
                        }
                    }
                    else if (cbDetailEntityName.Visible && cbDetailEntityName.Text == "")
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
                        FClientData.DetailEntityName = cbDetailEntityName.Text;
                        FClientData.BaseFormName = lvTemplate.SelectedItems[0].SubItems[1].Text;

                        //2010-06-13 Sjj
                        lvMasterSrcField.Items.Clear();
                        lvMasterDesField.Items.Clear();

                        if (lvMasterSrcField.Items.Count == 0 && lvMasterDesField.Items.Count == 0)
                            SetFieldNames(tbEntitySetName.Text, lvMasterSrcField);
                        if (FClientData.BaseFormName == "WCFWSingle" || FClientData.BaseFormName == "WCFWMasterDetail")
                        {
                            //2010-06-13 Sjj
                            lvViewSrcField.Items.Clear();
                            if (lvViewSrcField.Items.Count == 0 && lvViewDesField.Items.Count == 0)
                            {
                                SetFieldNames(tbEntitySetName.Text, lvViewSrcField);
                            }
                            ShowTableRelations();
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
            catch (Exception ex)
            {
                WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                return;
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
            for (int I = 0; I < NodeCollection.Count; I++)
            {
                TWCFDetailItem DetailItem = (TWCFDetailItem)NodeCollection[I].Tag;
                TBlockItem BlockItem = new TBlockItem();
                BlockItem.Name = NodeCollection[I].Text;
                BlockItem.TableName = DetailItem.EntityName;
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
            FClientData.CommandName = tbCommandName.Text;
            FClientData.EntityName = tbEntityName.Text;
            FClientData.ViewProviderName = cbDetailEntityName.Text;
            TWCFWebClientGenerator Generator = new TWCFWebClientGenerator(FClientData, FDTE2, FAddIn);
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
                    if (FClientData.BaseFormName == "WCFWMasterDetail")
                        AddBlockItem("View", FClientData.RemoteName, FClientData.CommandName, lvViewDesField);
                    AddBlockItem("Master", FClientData.RemoteName, FClientData.CommandName, lvMasterDesField);
                    AddDetailBlockItem("Master", tvRelation.Nodes, lvSelectedFields);
                }
                else
                {
                    if (FClientData.BaseFormName == "WCFWSingle")
                        AddBlockItem("View", FClientData.RemoteName, FClientData.CommandName, lvViewDesField);
                    AddBlockItem("Main", FClientData.RemoteName, FClientData.CommandName, lvMasterDesField);
                }
                Hide();
                FDTE2.MainWindow.Activate();
                DoGenClient();
                FInfoDataSet.Dispose();
                FInfoDataSet = null;
            }
            catch (Exception ex)
            {
                WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                return;
            }
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
            TWCFWebClientGenerator G = new TWCFWebClientGenerator(FClientData, FDTE2, FAddIn);
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
                //SetNodeData(R, IBS, ChildNode);
                ShowChildRelation(R.ChildTable.ChildRelations, ChildNode);
            }
        }

        private void ShowTable()
        {
            System.Windows.Forms.TreeNode Node = new System.Windows.Forms.TreeNode();
            Node.Text = cbDetailEntityName.Text;
            Node.Name = cbDetailEntityName.Text;
            tvRelation.Nodes.Add(Node);
            SetNodeData(cbDetailEntityName.Text, Node);
            //ShowChildRelation(R1.ChildTable.ChildRelations, Node);
        }

        private void ShowTableRelations()
        {
            tvRelation.Nodes.Clear();

            try
            {
                ShowTable();
            }
            finally
            {

            }
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

        private void btnDeleteField_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.TreeNode Node = tvRelation.SelectedNode;
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
            UpdatelvSelectedFields((TWCFDetailItem)Node.Tag);
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
            EFAssembly.EFClientToolsAssemblyAdapt.RemoteNameEditorDialog remoteNameEditorDialog = new EFAssembly.EFClientToolsAssemblyAdapt.RemoteNameEditorDialog();

            if (remoteNameEditorDialog.RemoteNameEditorDialogForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbRemoteName.Text = remoteNameEditorDialog.ReturnValue;
                tbCommandName.Text = remoteNameEditorDialog.SelectedCommandName;
                tbEntityName.Text = remoteNameEditorDialog.ReturnClassName;
                tbEntitySetName.Text = remoteNameEditorDialog.EntitySetName;
            }
        }

        private void tbProviderName_TextChanged(object sender, EventArgs e)
        {
            //string ProviderName = tbProviderName.Text;
            //if (ProviderName.Trim() == "")
            //    return;
            //if (FInfoDataSet != null && FInfoDataSet.Active)
            //{
            //    FInfoDataSet.Active = false;
            //    FInfoDataSet.Dispose();
            //    FInfoDataSet = null;
            //    FInfoDataSet = new InfoDataSet();
            //    FInfoDataSet.SetWizardDesignMode(true);
            //}
            //FInfoDataSet.RemoteName = ProviderName;
            //FInfoDataSet.ClearWhere();
            //FInfoDataSet.SetWhere("1=0");
            //FInfoDataSet.Active = true;
            //tbTableName.Text = FInfoDataSet.RealDataSet.Tables[0].TableName;
            //String DataSetName = FInfoDataSet.RealDataSet.Tables[0].TableName;
            //String ModuleName = FInfoDataSet.RemoteName.Substring(0, FInfoDataSet.RemoteName.IndexOf('.'));
            //String SolutionName = System.IO.Path.GetFileNameWithoutExtension(FDTE2.Solution.FullName);
            //tbTableNameF.Text = CliUtils.GetTableName(ModuleName, DataSetName, SolutionName, "", true);
            //tbTableNameF.Text = WzdUtils.RemoveQuote(tbTableNameF.Text, FClientData.DatabaseType);
            //cbViewProviderName.Items.Clear();
            //string DllName = tbProviderName.Text;
            //int Index = DllName.IndexOf('.');
            //DllName = DllName.Substring(0, Index + 1);
            //for (int I = 0; I < FProviderNameList.Length; I++)
            //{
            //    if (FProviderNameList[I].ToString().IndexOf(DllName) > -1)
            //    {
            //        cbViewProviderName.Items.Add(FProviderNameList[I]);
            //    }
            //}
            //cbViewProviderName.SelectedIndex = GetProviderIndex();
            //FClientData.ViewProviderName = cbViewProviderName.Text;
            //ShowTableRelations();
        }

        private int GetProviderIndex()
        {
            String FindName = "";
            int Result = -1;
            //switch (FClientData.Language)
            //{
            //    case "cs":
            //        FindName = "View_";
            //        break;
            //    case "vb":
            //        FindName = "_View_";
            //        break;
            //}

            //for (int I = 0; I < cbViewProviderName.Items.Count; I++)
            //{
            //    if (cbViewProviderName.Items[I].ToString().IndexOf(FindName) > -1)
            //    {
            //        Result = I;
            //        break;
            //    }
            //}
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
            System.Windows.Forms.TreeNode Node = tvRelation.SelectedNode;
            if (Node != null)
            {
                TWCFDetailItem DetailItem = (TWCFDetailItem)Node.Tag;
                MWizard.fmSelWCFTableField F = new fmSelWCFTableField();
                //COLDEF
                List<COLDEFInfo> colDefObjects = null;

                colDefObjects = WzdUtils.GetColumnDefination(FClientData.AssemblyName, FClientData.CommandName, DetailItem.EntityName, cbEEPAlias.Text);

                if (F.ShowSelTableFieldForm(DetailItem, lvSelectedFields, RearrangeRefValButton, btnRefVal_Click, colDefObjects,FClientData.AssemblyName, FClientData.CommandName))
                {
                    btnDeleteField.Enabled = lvSelectedFields.Items.Count > 0;
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
            tbComboEntityName.Text = FSelectedBlockFieldItem.ComboEntityName;
            tbComboEntitySetName.Text = FSelectedBlockFieldItem.ComboEntitySetName;
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
            FSelectedBlockFieldItem.ComboEntitySetName = tbComboEntitySetName.Text;
            FSelectedBlockFieldItem.ComboTextField = cbDataTextField.Text;
            FSelectedBlockFieldItem.ComboValueField = cbDataValueField.Text;
            FSelectedBlockFieldItem.QueryMode = cbQueryMode.Text;
            FSelectedBlockFieldItem.EditMask = tbEditMask.Text;

            if (FSelectedBlockFieldItem.ControlType == "ComboBox")
            {
                String[] comboAssembly = FSelectedBlockFieldItem.ComboRemoteName.Split('.');
                List<COLDEFInfo> colDefObjects = null;
                colDefObjects = WzdUtils.GetColumnDefination(comboAssembly[0], comboAssembly[1], FSelectedBlockFieldItem.ComboEntitySetName, cbEEPAlias.Text);
                COLDEFInfo colDefObject = null;
                if (colDefObjects != null)
                {
                    colDefObject = colDefObjects.Find(c => c.FIELD_NAME == FSelectedBlockFieldItem.ComboTextField);
                    if (colDefObject != null)
                        FSelectedBlockFieldItem.ComboTextFieldCaption = colDefObject.CAPTION;
                    colDefObject = colDefObjects.Find(c => c.FIELD_NAME == FSelectedBlockFieldItem.ComboValueField);
                    if (colDefObject != null)
                        FSelectedBlockFieldItem.ComboValueFieldCaption = colDefObject.CAPTION;
                }

                SYS_REFVAL aSYS_REFVAL = new SYS_REFVAL();
                aSYS_REFVAL.REFVAL_NO = "Auto." + FSelectedBlockFieldItem.ComboEntityName;
                aSYS_REFVAL.TABLE_NAME = FSelectedBlockFieldItem.ComboEntityName;
                aSYS_REFVAL.SELECT_ALIAS = FSelectedBlockFieldItem.ComboRemoteName;
                aSYS_REFVAL.DISPLAY_MEMBER = FSelectedBlockFieldItem.ComboTextField;
                aSYS_REFVAL.VALUE_MEMBER = FSelectedBlockFieldItem.ComboValueField;
                //foreach (OtherField of in FSelectedBlockFieldItem.ComboOtherFields)
                //{
                //    aSYS_REFVAL.SELECT_COMMAND += of.FieldName + ";";
                //}
                List<object> lParams = new List<object>();
                lParams.Add(aSYS_REFVAL);
                WzdUtils.SaveDataToTable(lParams, "SYS_REFVAL");
            }
            //FSelectedListViewItem.SubItems[1].Text = FSelectedBlockFieldItem.Description;
            //FSelectedListViewItem.SubItems[2].Text = FSelectedBlockFieldItem.CheckNull;
            //FSelectedListViewItem.SubItems[3].Text = FSelectedBlockFieldItem.DefaultValue;
            //FSelectedListViewItem.SubItems[4].Text = FSelectedBlockFieldItem.RefValNo;
            //FSelectedListViewItem.SubItems[5].Text = FSelectedBlockFieldItem.QueryMode;
            //FSelectedListViewItem.SubItems[6].Text = FSelectedBlockFieldItem.EditMask;
        }

        private void tbTableName_TextChanged(object sender, EventArgs e)
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

        private void cbControlType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbControlType.Text == "ComboBox")
            {
                gbComboBox.Enabled = true;
                tbComboRemoteName.Text = FSelectedBlockFieldItem.ComboRemoteName;
                tbComboEntityName.Text = FSelectedBlockFieldItem.ComboEntityName;
                tbComboEntitySetName.Text = FSelectedBlockFieldItem.ComboEntitySetName;
                cbDataTextField.Text = FSelectedBlockFieldItem.ComboTextField;
                cbDataValueField.Text = FSelectedBlockFieldItem.ComboValueField;
            }
            else
            {
                tbComboRemoteName.Text = String.Empty;
                tbComboEntityName.Text = String.Empty;
                tbComboEntitySetName.Text = String.Empty;
                cbDataTextField.Text = String.Empty;
                cbDataValueField.Text = String.Empty;
                gbComboBox.Enabled = false;
            }
        }

        private void lvTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ListView).SelectedItems.Count > 0)
            {
                String templateName = (sender as ListView).SelectedItems[0].Text;
                switch (templateName)
                {
                    case "WCFWSingle":
                        this.tbDescription.Text = templateName + ": ExtGridView(View) + ExtFormView(Master)";
                        break;
                    case "WCFWMasterDetail":
                        this.tbDescription.Text = templateName + ": ExtGridView(View) + ExtFormView(Master) + ExtGridView(Detail)";
                        break;
                }
            }
        }

        private void btnComboRemoteName_Click(object sender, EventArgs e)
        {
            EFAssembly.EFClientToolsAssemblyAdapt.RemoteNameEditorDialog remoteNameEditorDialog = new EFAssembly.EFClientToolsAssemblyAdapt.RemoteNameEditorDialog();

            if (remoteNameEditorDialog.RemoteNameEditorDialogForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbComboRemoteName.Text = remoteNameEditorDialog.ReturnValue;
                tbComboEntityName.Text = remoteNameEditorDialog.ReturnClassName;
                tbComboEntitySetName.Text = remoteNameEditorDialog.EntitySetName;

                List<object> refvals = WzdUtils.GetAllDataByTableName("SYS_REFVAL");
                for (int i = 0; i < refvals.Count; i++)
                {
                    if ((refvals[i] as SYS_REFVAL).REFVAL_NO == "Auto." + tbComboEntityName.Text &&
                            lvMasterDesField.SelectedItems[0].Text == (refvals[i] as SYS_REFVAL).VALUE_MEMBER)
                    {
                        cbDataTextField.Text = (refvals[i] as SYS_REFVAL).DISPLAY_MEMBER;
                        cbDataValueField.Text = (refvals[i] as SYS_REFVAL).VALUE_MEMBER;
                        //tbOtherFields.Text = (refvals[i] as SYS_REFVAL).SELECT_COMMAND;
                        break;
                    }
                }
            }
        }

        private void tbEntityName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((sender as TextBox).Text != String.Empty)
                {
                    cbDetailEntityName.Items.Clear();
                    List<String> lDetailEntityNames = WzdUtils.GetDetailEntityNames((sender as TextBox).Text);
                    //List<String> lDetailEntityNames = WzdUtils.GetDetailEntityNames(this.tbRemoteName.Text.Split('.')[0], this.tbRemoteName.Text.Split('.')[1], (sender as TextBox).Text);
                    for (int i = 0; i < lDetailEntityNames.Count; i++)
                        cbDetailEntityName.Items.Add(lDetailEntityNames[i]);

                    if (cbDetailEntityName.Items.Count == 1)
                        cbDetailEntityName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
            }
        }

        private void cbControlType_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbControlType_D.Text == "ComboBox")
            {
                gbDetailCombo.Enabled = true;
                tbComboRemoteName_D.Text = FSelectedBlockFieldItem_D.ComboRemoteName;
                tbComboEntityName_D.Text = FSelectedBlockFieldItem_D.ComboEntityName;
                tbComboEntitySetName_D.Text = FSelectedBlockFieldItem_D.ComboEntitySetName;
                cbComboDisplayField_D.Text = FSelectedBlockFieldItem_D.ComboTextField;
                cbComboValueField_D.Text = FSelectedBlockFieldItem_D.ComboValueField;
            }
            else
            {
                tbComboRemoteName_D.Text = String.Empty;
                tbComboEntityName_D.Text = String.Empty;
                tbComboEntitySetName_D.Text = String.Empty;
                cbComboDisplayField_D.Text = String.Empty;
                cbComboValueField_D.Text = String.Empty;
                gbDetailCombo.Enabled = false;
            }
        }

        private void btnComboRemoteName_D_Click(object sender, EventArgs e)
        {
            EFAssembly.EFClientToolsAssemblyAdapt.RemoteNameEditorDialog remoteNameEditorDialog = new EFAssembly.EFClientToolsAssemblyAdapt.RemoteNameEditorDialog();

            if (remoteNameEditorDialog.RemoteNameEditorDialogForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbComboRemoteName_D.Text = remoteNameEditorDialog.ReturnValue;
                tbComboEntityName_D.Text = remoteNameEditorDialog.ReturnClassName;

                List<object> refvals = WzdUtils.GetAllDataByTableName("SYS_REFVAL");
                for (int i = 0; i < refvals.Count; i++)
                {
                    if ((refvals[i] as SYS_REFVAL).REFVAL_NO == "Auto." + tbComboEntityName_D.Text &&
                            lvSelectedFields.SelectedItems[0].Text == (refvals[i] as SYS_REFVAL).VALUE_MEMBER)
                    {
                        cbComboDisplayField_D.Text = (refvals[i] as SYS_REFVAL).DISPLAY_MEMBER;
                        cbComboValueField_D.Text = (refvals[i] as SYS_REFVAL).VALUE_MEMBER;
                        //tbOtherFields_D.Text = (refvals[i] as SYS_REFVAL).SELECT_COMMAND;
                        break;
                    }
                }
            }
        }

        private void tbComboEntityName_D_TextChanged(object sender, EventArgs e)
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
            tbComboEntitySetName_D.Text = FSelectedBlockFieldItem_D.ComboEntitySetName;
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
            FSelectedBlockFieldItem_D.ComboEntitySetName = tbComboEntitySetName_D.Text;
            FSelectedBlockFieldItem_D.ComboTextField = cbComboDisplayField_D.Text;
            FSelectedBlockFieldItem_D.ComboValueField = cbComboValueField_D.Text;
            FSelectedBlockFieldItem_D.QueryMode = cbQueryMode_D.Text;
            FSelectedBlockFieldItem_D.EditMask = tbEditMask_D.Text;

            if (FSelectedBlockFieldItem_D.ControlType == "ComboBox")
            {
                String[] comboAssembly = FSelectedBlockFieldItem_D.ComboRemoteName.Split('.');
                List<COLDEFInfo> colDefObjects = null;
                colDefObjects = WzdUtils.GetColumnDefination(comboAssembly[0], comboAssembly[1], FSelectedBlockFieldItem_D.ComboEntitySetName, cbEEPAlias.Text);
                COLDEFInfo colDefObject = null;
                if (colDefObjects != null)
                {
                    colDefObject = colDefObjects.Find(c => c.FIELD_NAME == FSelectedBlockFieldItem_D.ComboTextField);
                    if (colDefObject != null)
                        FSelectedBlockFieldItem_D.ComboTextFieldCaption = colDefObject.CAPTION;
                    colDefObject = colDefObjects.Find(c => c.FIELD_NAME == FSelectedBlockFieldItem_D.ComboValueField);
                    if (colDefObject != null)
                        FSelectedBlockFieldItem_D.ComboValueFieldCaption = colDefObject.CAPTION;
                }

                SYS_REFVAL aSYS_REFVAL = new SYS_REFVAL();
                aSYS_REFVAL.REFVAL_NO = "Auto." + FSelectedBlockFieldItem_D.ComboEntityName;
                aSYS_REFVAL.TABLE_NAME = FSelectedBlockFieldItem_D.ComboEntityName;
                aSYS_REFVAL.SELECT_ALIAS = FSelectedBlockFieldItem_D.ComboRemoteName;
                aSYS_REFVAL.DISPLAY_MEMBER = FSelectedBlockFieldItem_D.ComboTextField;
                aSYS_REFVAL.VALUE_MEMBER = FSelectedBlockFieldItem_D.ComboValueField;
                //foreach (OtherField of in FSelectedBlockFieldItem_D.ComboOtherFields)
                //{
                //    aSYS_REFVAL.SELECT_COMMAND += of.FieldName + ";";
                //}
                List<object> lParams = new List<object>();
                lParams.Add(aSYS_REFVAL);
                WzdUtils.SaveDataToTable(lParams, "SYS_REFVAL");
            }

            //FSelectedListViewItem.SubItems[1].Text = FSelectedBlockFieldItem.Description;
            //FSelectedListViewItem.SubItems[2].Text = FSelectedBlockFieldItem.CheckNull;
            //FSelectedListViewItem.SubItems[3].Text = FSelectedBlockFieldItem.DefaultValue;
            //FSelectedListViewItem.SubItems[4].Text = FSelectedBlockFieldItem.RefValNo;
            //FSelectedListViewItem.SubItems[5].Text = FSelectedBlockFieldItem.QueryMode;
            //FSelectedListViewItem.SubItems[6].Text = FSelectedBlockFieldItem.EditMask;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            fmSetServerPath afmSetServerPath = new fmSetServerPath(FAddIn);
            afmSetServerPath.ShowDialog();
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
        }
    }

    public class TWCFWebClientData : Object
    {
        private string FPackageName, FBaseFormName, FServerPackageName, FFolderName, FCommandName, FEntityName, FFormName, FRemoteName,
            FDatabaseName, FSolutionName, FViewProviderName, FWebSiteName, FFolderMode, FFormTitle, FDetailEntityName;
        private TBlockItems FBlocks;
        private MWizard.fmEEPWCFWebWizard FOwner;
        private bool FNewSolution = false;
        private string FCodeFolderName;
        private int FColumnCount;
        private ClientType FDatabaseType;
        private String FConnString;
        private String FLanguage = "cs";

        public TWCFWebClientData(MWizard.fmEEPWCFWebWizard Owner)
        {
            FOwner = Owner;
            FBlocks = new TBlockItems(this);
        }

        public ClientType DatabaseType
        {
            get { return FDatabaseType; }
            set { FDatabaseType = value; }
        }

        public fmEEPWCFWebWizard Owner
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
            if (string.Compare(FBaseFormName, "WCFWMasterDetail") == 0)
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

    partial class TWCFWebClientGenerator : System.ComponentModel.Component
    {
        private TWCFWebClientData FClientData;
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
        private List<EFClientTools.Web.EFDataSource> FEFDataSourceList;
        private List<WebRefVal> FWebRefValList;
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

        public TWCFWebClientGenerator(TWCFWebClientData ClientData, DTE2 dte2, AddIn aAddIn)
        {
            FClientData = ClientData;
            FDTE2 = dte2;
            FAddIn = aAddIn;
            FSYS_REFVAL = new DataSet();
            FEFDataSourceList = new List<EFClientTools.Web.EFDataSource>();
            FWebRefValList = new List<WebRefVal>();
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
            TemplatePath = WzdUtils.GetWebClientPath(FAddIn, true) + "\\Template";
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
                FPI.Name = FClientData.FormName + ".aspx";

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
#endif
        }

        private void GenViewBlockControl(TBlockItem BlockItem)
        {
#if VS90
            //object oView = FDesignerDocument.webControls.item("View", 0);
            //if (oView == null)
            //    oView = FDesignerDocument.webControls.item("Master", 0);

            //WebDevPage.IHTMLElement eView = null;
            //WebDevPage.IHTMLElement eWebView1 = null;

            //if (oView == null || !(oView is WebDevPage.IHTMLElement))
            //    return;
            //eView = (WebDevPage.IHTMLElement)oView;
            //BlockItem.wDataSource = new WebDataSource();
            //String viewDataMember = FClientData.ViewProviderName.Substring(FClientData.ViewProviderName.IndexOf('.') + 1, FClientData.ViewProviderName.Length -
            //                                        FClientData.ViewProviderName.IndexOf('.') - 1);
            //if (eView != null)
            //{
            //    eView.setAttribute("DataMember", viewDataMember, 0);
            //}

            //object oWebView1 = FDesignerDocument.webControls.item("WgView", 0);
            //if (oWebView1 != null)
            //{
            //    eWebView1 = (WebDevPage.IHTMLElement)oWebView1;
            //    //eWebView1.setAttribute("DataMember", viewDataMember, 0);
            //}

            //if (oWebView1 == null)
            //    oWebView1 = FDesignerDocument.webControls.item("WebGridView1", 0);

            //if (oWebView1 != null)
            //{
            //    eWebView1 = (WebDevPage.IHTMLElement)oWebView1;

            //    //这里本来想再往下找Columns节点的,可是找不到,只能先这样写了
            //    StringBuilder sb = new StringBuilder(eWebView1.innerHTML);
            //    int idx = eWebView1.innerHTML.IndexOf("</Columns>");
            //    List<string> KeyFields = new List<string>();
            //    foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
            //    {
            //        idx = sb.ToString().IndexOf("</Columns>");
            //        sb.Insert(idx, "\r            <asp:BoundField DataField=\"" + BFI.DataField + "\" HeaderText=\"" + (string.IsNullOrEmpty(BFI.Description) ? BFI.DataField : BFI.Description) + "\" SortExpression=\"" + BFI.DataField + "\" />\r\n            ");
            //    }
            //    eWebView1.innerHTML = sb.ToString();
            //}

            BlockItem.wDataSource = new WebDataSource();
            EFBase.EFCollection<ExtTools.ExtGridColumn> aExtGridColumnCollection = new EFBase.EFCollection<ExtTools.ExtGridColumn>(new ExtTools.ExtGridView());
            EFBase.EFCollection<ExtTools.ExtQueryField> aExtQueryFieldCollection = new EFBase.EFCollection<ExtTools.ExtQueryField>(new ExtTools.ExtGridView());
            bool flag = true;
            //DataTable srcTable = FWizardDataSet.RealDataSet.Tables[BlockItem.TableName];
            foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
            {
                ExtTools.ExtGridColumn extCol = new ExtTools.ExtGridColumn();
                if (BFI.CheckNull == "Y")
                    extCol.AllowNull = false;
                else
                    extCol.AllowNull = true;
                //extCol.AllowSort = false;
                extCol.ColumnName = string.Format("col{0}", BFI.DataField);
                extCol.DataField = BFI.DataField;
                extCol.ExpandColumn = true;
                if (BFI.Description != null && BFI.Description != String.Empty)
                    extCol.HeaderText = BFI.Description;
                else
                    extCol.HeaderText = BFI.DataField;
                extCol.IsKeyField = BFI.IsKey;
                //extCol.IsKeyField = IsKeyField(BFI.DataField, srcTable.PrimaryKey);
                extCol.NewLine = flag;
                //extCol.Resizable = true;
                //extCol.TextAlign = "left";
                extCol.Visible = true;
                extCol.Width = 120;
                extCol.DefaultValue = BFI.DefaultValue;
                extCol.Formatter = BFI.EditMask;
                extCol.FieldType = EFClientTools.Common.TypeHelper.ReturnDataType(BFI.DataType);
                if (BFI.QueryMode == "Normal")
                {
                    ExtTools.ExtQueryField aExtQueryField = new ExtTools.ExtQueryField();
                    aExtQueryField.Condition = "And";
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
                    ExtTools.ExtQueryField aExtQueryField = new ExtTools.ExtQueryField();
                    aExtQueryField.DataField = BFI.DataField;
                    aExtQueryField.Caption = BFI.Description;
                    aExtQueryField.Condition = "And";
                    aExtQueryField.Operator = ">=";
                    aExtQueryFieldCollection.Add(aExtQueryField);

                    ExtTools.ExtQueryField aExtQueryField2 = new ExtTools.ExtQueryField();
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

            WebDevPage.IHTMLElement ExtGVView = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("ExtGVView", 0);
            if (ExtGVView != null)
            {
                SetCollectionValue(ExtGVView, typeof(ExtTools.ExtGridView).GetProperty("Columns"), aExtGridColumnCollection);
            }
            if (ExtGVView != null && aExtQueryFieldCollection.Count > 0)
            {
                SetCollectionValue(ExtGVView, typeof(ExtTools.ExtGridView).GetProperty("QueryFields"), aExtQueryFieldCollection);
            }
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
            if (FClientData.BaseFormName == "WCFWMasterDetail")
            {
                bWebDataSet = FDesignerHost.CreateComponent(typeof(WebDataSet), "WView") as WebDataSet;
            }
            try
            {
                aWebDataSet.SetWizardDesignMode(true);
                aWebDataSet.RemoteName = FClientData.RemoteName;
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

        private String GenEFDataSource(TBlockFieldItem FieldItem, String TableName, String Kind, String ExtraName)
        {
            String Name = "EFDS" + TableName + FieldItem.DataField + ExtraName;

            bool isExist = false;
            foreach (EFClientTools.Web.EFDataSource bWebDataSource in FEFDataSourceList)
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
                if (eDataSoruce.tagName.ToLower() == "efclienttools:efdatasource")
                    return Name;
            }

            WebDevPage.IHTMLElement div = ((WebDevPage.IHTMLElementCollection)FDesignerDocument.pageContentElement.children).item("div1", 0) as WebDevPage.IHTMLElement;

            if (Kind == "ComboBox")
            {
                if (!isExist)
                {
                    EFClientTools.Web.EFDataSource aEFDataSource = new EFClientTools.Web.EFDataSource();
                    aEFDataSource.ID = Name;
                    aEFDataSource.RemoteName = FieldItem.ComboRemoteName;
                    aEFDataSource.DataMember = FieldItem.ComboEntityName;
                    aEFDataSource.Active = true;
                    FEFDataSourceList.Add(aEFDataSource);
                    WebDevPage.IHTMLElement Page = FDesignerDocument.pageContentElement;
                    InsertControl(Page, aEFDataSource);
                    //div.insertAdjacentHTML("afterBegin", "<EFClientTools:EFDataSource ID=\"" + Name + "\" runat=\"server\" RemoteName=\"" + aWebDataSource.RemoteName +
                    //                                        "\" DataMember=\"" + aWebDataSource.DataMember + "\" Active=\"True\"></EFClientTools:EFDataSource>");
                }
            }
            return Name;
#endif
        }

        private String GenExtComboBox(TBlockFieldItem FieldItem, String TableName, String Kind, String ExtraName, String DSID)
        {
            String Name = "ext" + TableName + FieldItem.DataField + ExtraName;

            bool isExist = false;
            foreach (ExtTools.ExtComboBox bExtComboBox in FExtComboBoxList)
            {
                if (String.Compare(bExtComboBox.ID, Name) == 0)
                {
                    isExist = true;
                    break;
                }
            }
            ExtTools.ExtComboBox aExtComboBox = new ExtTools.ExtComboBox();
            aExtComboBox.ID = Name;
            aExtComboBox.DataSourceID = DSID;
            aExtComboBox.AllowPage = true;
            aExtComboBox.AutoRender = false;
            
            if (Kind == "ExtComboBox")
            {
                aExtComboBox.DisplayField = FieldItem.ComboTextField;
                aExtComboBox.ValueField = FieldItem.ComboValueField;

                ExtTools.ExtSimpleColumn aExtSimpleColumn = new ExtTools.ExtSimpleColumn();
                aExtSimpleColumn.DataField = FieldItem.ComboTextField;
                if (FieldItem.ComboTextFieldCaption != null && FieldItem.ComboTextFieldCaption != String.Empty)
                    aExtSimpleColumn.HeaderText = FieldItem.ComboTextFieldCaption;
                else
                    aExtSimpleColumn.HeaderText = FieldItem.ComboTextField;
                aExtComboBox.Columns.Add(aExtSimpleColumn);
                ExtTools.ExtSimpleColumn bExtSimpleColumn = new ExtTools.ExtSimpleColumn();
                bExtSimpleColumn.DataField = FieldItem.ComboValueField;
                if (FieldItem.ComboValueFieldCaption != null && FieldItem.ComboValueFieldCaption != String.Empty)
                    bExtSimpleColumn.HeaderText = FieldItem.ComboValueFieldCaption;
                else
                    bExtSimpleColumn.HeaderText = FieldItem.ComboValueField;
                aExtComboBox.Columns.Add(bExtSimpleColumn);
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
                        String OWNER = String.Empty, SS = this.FClientData.EntityName, TableName = String.Empty;
                        if (SS.Contains("."))
                        {
                            OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                            TableName = SS;
                        }
                        aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME='" + TableName + "' OR TABLE_NAME='" + OWNER + "." + TableName + "'";
                        IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                        DataSet dsColdef = new DataSet();
                        WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsColdef, this.FClientData.CommandName);

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
                            RefColumns.Append("</Columns>");
                        }
                        else
                        {
                            RefColumns = new StringBuilder("");
                        }

                        String DataSourceID = GenEFDataSource(BFI, BlockItem.TableName, "RefVal", "");
                        String refvalHTML = String.Empty;
                        if (isAjaxPage)
                        {
                            refvalHTML = String.Format("<AjaxTools:AjaxRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"{1}\"{5}) %>' " +
                                        "DataSourceID=\"{2}\" " +
                                        "DataTextField=\"{3}\" DataValueField=\"{4}\" ResxDataSet=\"\">" +
                                         RefColumns.ToString() +
                                        "</AjaxTools:AjaxRefVal>",
                                        "arv" + BlockItem.TableName + BFI.DataField + "E",
                                        BFI.DataField,
                                        DataSourceID,
                                        FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                        FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                        FormatStyle
                                        );
                        }
                        else
                        {
                            refvalHTML = String.Format("<InfoLight:WebRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"{1}\"{5}) %>' " +
                                        "ButtonImageUrl=\"../Image/refval/RefVal.gif\" DataBindingField=\"{1}\" DataSourceID=\"{2}\" " +
                                        "DataTextField=\"{3}\" DataValueField=\"{4}\" ReadOnly=\"False\" ResxDataSet=\"\" " +
                                        "ResxFilePath=\"\" UseButtonImage=\"True\"> " +
                                         RefColumns.ToString() +
                                        "</InfoLight:WebRefVal>",
                                        "wrv" + BlockItem.TableName + BFI.DataField + "E",
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
                        String comboHTML = string.Format("<InfoLight:WebDropDownList runat=\"server\" ID=\"{0}\" DataSourceID=\"{1}\" DataTextField=\"{2}\" DataValueField=\"{3}\" DataMember=\"{4}\" SelectedValue='<%# Bind(\"{5}\"{6}) %>'></InfoLight:WebDropDownList>",
                            "wdd" + BlockItem.TableName + BFI.DataField + "E",
                            GenEFDataSource(BFI, BFI.ComboEntityName, "ComboBox", ""),
                            BFI.ComboTextField,
                            BFI.ComboValueField,
                            BFI.ComboEntityName,
                            BFI.DataField,
                            FormatStyle);
                        builder.AppendLine(comboHTML);
                        break;
                    case "ValidateBox":
                        String validateHTML = String.Format("<InfoLight:WebValidateBox ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{3}) %>' ValidateField=\"{1}\" WebValidateID=\"{2}\"></InfoLight:WebValidateBox>",
                                    "wvb" + BlockItem.TableName + BFI.DataField + "E",
                                    BFI.DataField,
                                    "wv" + BlockItem.TableName,
                                    FormatStyle);
                        builder.AppendLine(validateHTML);
                        break;
                    case "CheckBox":
                        String checkHTML = String.Format("<asp:CheckBox ID=\"{0}\" runat=\"server\" Checked='<%# Bind(\"{1}\"{2}) %>'></asp:CheckBox>",
                                    "cb" + BlockItem.TableName + BFI.DataField + "E",
                                    BFI.DataField,
                                    FormatStyle);
                        builder.AppendLine(checkHTML);
                        break;
                    case "DateTimeBox":
                        String dtHTML = String.Empty;
                        if (isAjaxPage)
                        {
                            dtHTML = String.Format("<AjaxTools:AjaxDateTimePicker runat=\"server\" DateFormat=\"{0}\" DateTimeType=\"{1}\" Localize=\"False\" MinYear=\"1950\" MaxYear=\"2050\" ToolTip=\"{2}\" Width=\"100px\" ID=\"{3}\" {4}='<%# Bind(\"{5}\"{6}) %>'></AjaxTools:AjaxDateTimePicker>",
                                "None",
                                (BFI.DataType == typeof(DateTime)) ? "DateTime" : "Varchar",
                                BFI.DataField,
                                "wdt" + BlockItem.TableName + BFI.DataField + "E",
                                (BFI.DataType == typeof(DateTime)) ? "Text" : "DateString",
                                BFI.DataField,
                                FormatStyle);
                        }
                        else
                        {
                            dtHTML = String.Format("<InfoLight:WebDateTimePicker runat=\"server\" UseButtonImage=\"True\" DateFormat=\"{0}\" DateTimeType=\"{1}\" Localize=\"False\" MinYear=\"1950\" MaxYear=\"2050\" ToolTip=\"{2}\" Width=\"100px\" ID=\"{3}\" {4}='<%# Bind(\"{5}\"{6}) %>'></InfoLight:WebDateTimePicker>",
                                "None",
                                (BFI.DataType == typeof(DateTime)) ? "DateTime" : "Varchar",
                                BFI.DataField,
                                "wdt" + BlockItem.TableName + BFI.DataField + "E",
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
                        String OWNER = String.Empty, SS = this.FClientData.EntityName, TableName = String.Empty;
                        if (SS.Contains("."))
                        {
                            OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                            TableName = SS;
                        }
                        aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME='" + TableName + "' OR TABLE_NAME='" + OWNER + "." + TableName + "'";
                        IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                        DataSet dsColdef = new DataSet();
                        WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsColdef, this.FClientData.CommandName);

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
                            RefColumns.Append("</Columns>");
                        }
                        else
                        {
                            RefColumns = new StringBuilder("");
                        }

                        String DataSourceID = GenEFDataSource(BFI, BlockItem.TableName, "RefVal", "");
                        String refvalHTML = String.Empty;
                        if (isAjaxPage)
                        {
                            refvalHTML = String.Format("<AjaxTools:AjaxRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"{1}\"{5}) %>' " +
                                        "DataSourceID=\"{2}\" " +
                                        "DataTextField=\"{3}\" DataValueField=\"{4}\" ResxDataSet=\"\">" +
                                         RefColumns.ToString() +
                                        "</AjaxTools:AjaxRefVal>",
                                        "arv" + BlockItem.TableName + BFI.DataField + "F",
                                        BFI.DataField,
                                        DataSourceID,
                                        FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                        FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                        FormatStyle
                                        );
                        }
                        else
                        {
                            refvalHTML = String.Format("<InfoLight:WebRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"{1}\"{5}) %>' " +
                                        "ButtonImageUrl=\"../Image/refval/RefVal.gif\" DataBindingField=\"{1}\" DataSourceID=\"{2}\" " +
                                        "DataTextField=\"{3}\" DataValueField=\"{4}\" ReadOnly=\"False\" ResxDataSet=\"\" " +
                                        "ResxFilePath=\"\" UseButtonImage=\"True\"> " +
                                         RefColumns.ToString() +
                                        "</InfoLight:WebRefVal>",
                                        "wrv" + BlockItem.TableName + BFI.DataField + "F",
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
                            "wdd" + BlockItem.TableName + BFI.DataField + "F",
                            GenEFDataSource(BFI, BFI.ComboEntityName, "ComboBox", ""),
                            BFI.ComboTextField,
                            BFI.ComboValueField,
                            BFI.ComboEntityName);
                        builder.AppendLine(comboHTML);
                        break;
                    case "ValidateBox":
                        String validateHTML = String.Format("<InfoLight:WebValidateBox ID=\"{0}\" runat=\"server\" Text='<%# Bind(\"{1}\"{3}) %>' ValidateField=\"{1}\" WebValidateID=\"{2}\"></InfoLight:WebValidateBox>",
                                    "wvb" + BlockItem.TableName + BFI.DataField + "F",
                                    BFI.DataField,
                                    "wv" + BlockItem.TableName,
                                    FormatStyle);
                        builder.AppendLine(validateHTML);
                        break;
                    case "CheckBox":
                        String checkHTML = String.Format("<asp:CheckBox ID=\"{0}\" runat=\"server\" Checked='<%# Bind(\"{1}\"{2}) %>'></asp:CheckBox>",
                                    "cb" + BlockItem.TableName + BFI.DataField + "F",
                                    BFI.DataField,
                                    FormatStyle);
                        builder.AppendLine(checkHTML);
                        break;
                    case "DateTimeBox":
                        String dtHTML = String.Empty;
                        if (isAjaxPage)
                        {
                            dtHTML = String.Format("<AjaxTools:AjaxDateTimePicker runat=\"server\" DateFormat=\"{0}\" DateTimeType=\"{1}\" Localize=\"False\" MinYear=\"1950\" MaxYear=\"2050\" ToolTip=\"{2}\" Width=\"100px\" ID=\"{3}\" {4}='<%# Bind(\"{5}\"{6}) %>'></AjaxTools:AjaxDateTimePicker>",
                                "None",
                                (BFI.DataType == typeof(DateTime)) ? "DateTime" : "Varchar",
                                BFI.DataField,
                                "wdt" + BlockItem.TableName + BFI.DataField + "F",
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
                                "wdt" + BlockItem.TableName + BFI.DataField + "F");
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
                        String OWNER = String.Empty, SS = this.FClientData.EntityName, TableName = String.Empty;
                        if (SS.Contains("."))
                        {
                            OWNER = WzdUtils.GetToken(ref SS, new char[] { '.' });
                            TableName = SS;
                        }
                        aInfoCommand.CommandText = "Select * from COLDEF where TABLE_NAME='" + TableName + "' OR TABLE_NAME='" + OWNER + "." + TableName + "'";
                        IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                        DataSet dsColdef = new DataSet();
                        WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, dsColdef, this.FClientData.CommandName);

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
                            RefColumns.Append("</Columns>");
                        }
                        else
                        {
                            RefColumns = new StringBuilder("");
                        }

                        String DataSourceID = GenEFDataSource(BFI, BlockItem.TableName, "RefVal", "");
                        String refvalHTML = String.Format("<InfoLight:WebRefVal ID=\"{0}\" runat=\"server\" BindingValue='<%# Bind(\"{1}\"{5}) %>' " +
                                    "ButtonImageUrl=\"../Image/refval/RefVal.gif\" DataBindingField=\"{1}\" DataSourceID=\"{2}\" " +
                                    "DataTextField=\"{3}\" DataValueField=\"{4}\" ReadOnly=\"True\" BorderStyle=\"None\" ResxDataSet=\"\" " +
                                    "ResxFilePath=\"\" UseButtonImage=\"True\" Width=\"100px\" BackColor=\"Transparent\"> " +
                                     RefColumns.ToString() +
                                    "</InfoLight:WebRefVal>",
                                    "wrv" + BlockItem.TableName + BFI.DataField + "E",
                                    BFI.DataField,
                                    DataSourceID,
                                    FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString(),
                                    FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString(),
                                    FormatStyle
                                    );
                        builder.AppendLine(refvalHTML);
                        break;
                    default:
                        string labelHTML = string.Format("<asp:Label runat=\"server\" ToolTip=\"{0}\" ID=\"{1}\" Text='<%# Bind(\"{2}\"{3}) %>'></asp:Label>",
                            BFI.DataField,
                            "l" + BlockItem.TableName + BFI.DataField,
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
            //object oMaster = FDesignerDocument.webControls.item("Master", 0);

            //WebDevPage.IHTMLElement eMaster = null;
            //WebDevPage.IHTMLElement eWebGridView1 = null;

            //if (oMaster == null || !(oMaster is WebDevPage.IHTMLElement))
            //    return;
            //eMaster = (WebDevPage.IHTMLElement)oMaster;

            //WebDefault Default = new WebDefault();
            //Default.ID = "wd" + BlockItem.TableName;
            //Default.DataSourceID = eMaster.getAttribute("ID", 0).ToString();
            //Default.DataMember = FClientData.TableName;

            //WebValidate Validate = new WebValidate();
            //Validate.ID = "wv" + BlockItem.TableName;
            //Validate.DataSourceID = eMaster.getAttribute("ID", 0).ToString();
            //Validate.DataMember = FClientData.TableName;

            //WebDevPage.IHTMLElement Page = FDesignerDocument.pageContentElement;
            //InsertControl(Page, Default);
            //InsertControl(Page, Validate);

            //WebQueryFiledsCollection QueryFields = new WebQueryFiledsCollection(null, typeof(QueryField));
            //WebQueryColumnsCollection QueryColumns = new WebQueryColumnsCollection(null, typeof(QueryColumns));
            //foreach (TBlockFieldItem fielditem in BlockItem.BlockFieldItems)
            //{
            //    GenDefault(fielditem, Default, Validate);
            //    GenQuery(fielditem, QueryFields, QueryColumns, BlockItem.TableName);
            //}

            //foreach (TBlockFieldItem fielditem in BlockItem.BlockFieldItems)
            //{
            //    foreach (WebQueryColumns wqc in QueryColumns)
            //    {
            //        if (wqc.ColumnType == "ClientQueryRefValColumn" && wqc.Column == fielditem.DataField && fielditem.RefValNo != String.Empty)
            //        {
            //            WebDataSource aWebDataSource = new WebDataSource();
            //            InfoCommand aInfoCommand = new InfoCommand(FClientData.DatabaseType);
            //            aInfoCommand.Connection = WzdUtils.AllocateConnection(FClientData.DatabaseName, FClientData.DatabaseType, true);
            //            //aInfoCommand.Connection = FClientData.Owner.GlobalConnection;
            //            IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            //            if (FSYS_REFVAL != null)
            //                FSYS_REFVAL.Dispose();
            //            FSYS_REFVAL = new DataSet();
            //            aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", fielditem.RefValNo);
            //            WzdUtils.FillDataAdapter(FClientData.DatabaseType, DA, FSYS_REFVAL, fielditem.RefValNo);

            //            WebRefVal aWebRefVal = new WebRefVal();
            //            aWebRefVal.ID = wqc.WebRefVal;
            //            aWebRefVal.DataTextField = FSYS_REFVAL.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
            //            aWebRefVal.DataValueField = FSYS_REFVAL.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
            //            aWebRefVal.DataSourceID = String.Format("wds{0}{1}", BlockItem.TableName, wqc.Column);
            //            aWebRefVal.Visible = false;
            //            InsertControl(Page, aWebRefVal);
            //            break;
            //        }
            //    }
            //}

            //WebDevPage.IHTMLElement Navigator = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("WebNavigator1", 0);
            //if (Navigator != null)
            //{
            //    SetCollectionValue(Navigator, typeof(WebNavigator).GetProperty("QueryFields"), QueryFields);
            //}
            //WebDevPage.IHTMLElement ClientQuery = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("WebClientQuery1", 0);
            //if (ClientQuery != null)
            //{
            //    SetCollectionValue(ClientQuery, typeof(WebClientQuery).GetProperty("Columns"), QueryColumns);
            //}

            //object oWebGridView1 = FDesignerDocument.webControls.item("wgvMaster", 0);
            //if (oWebGridView1 == null)
            //    oWebGridView1 = FDesignerDocument.webControls.item("WebGridView1", 0);
            //eWebGridView1 = (WebDevPage.IHTMLElement)oWebGridView1;
            ////eWebGridView1.setAttribute("DataMember", FClientData.TableName, 0);

            ////这里本来想再往下找Columns节点的,可是找不到,只能先这样写了
            //StringBuilder sb = new StringBuilder(eWebGridView1.innerHTML);
            //int idx = eWebGridView1.innerHTML.IndexOf("</Columns>");
            //if (idx == -1)
            //{
            //    idx = sb.ToString().IndexOf("<SelectedRowStyle");
            //    sb.Insert(idx, "<Columns>\r\n            </Columns>");
            //}
            //List<string> KeyFields = new List<string>();
            //foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
            //{
            //    idx = sb.ToString().IndexOf("</Columns>");
            //    if (!string.IsNullOrEmpty(BFI.RefValNo) || BFI.RefField != null)
            //    {
            //        sb.Insert(idx, GenTemplateFieldHTML(BFI.ControlType, BlockItem, BFI));
            //    }
            //    else if (BFI.ControlType == "ComboBox" || BFI.ControlType == "ValidateBox" || BFI.ControlType == "CheckBox")
            //    {
            //        sb.Insert(idx, GenTemplateFieldHTML(BFI.ControlType, BlockItem, BFI));
            //    }
            //    else
            //    {
            //        if (BFI.DataType == typeof(DateTime) || (BFI.ControlType != null && BFI.ControlType == "DateTimeBox"))
            //        {
            //            sb.Insert(idx, GenTemplateFieldHTML("DateTimeBox", BlockItem, BFI));
            //        }
            //        else
            //        {
            //            sb.Insert(idx, "\r            <asp:BoundField DataField=\"" + BFI.DataField + "\" HeaderText=\"" + (string.IsNullOrEmpty(BFI.Description) ? BFI.DataField : BFI.Description) + "\" SortExpression=\"" + BFI.DataField + "\" />\r\n            ");
            //        }
            //    }
            //}
            //eWebGridView1.innerHTML = sb.ToString();

            BlockItem.wDataSource = new WebDataSource();
            EFBase.EFCollection<ExtTools.ExtFormField> aClientCollection
                = new EFBase.EFCollection<ExtTools.ExtFormField>(new ExtTools.ExtFormView());
            //DataTable srcTable = FWizardDataSet.RealDataSet.Tables[BlockItem.TableName];
            bool flag = true;
            foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
            {
                ExtTools.ExtFormField extCol = new ExtTools.ExtFormField();
                if (BFI.CheckNull == "Y")
                    extCol.AllowNull = false;
                else
                    extCol.AllowNull = true;
                extCol.DataField = BFI.DataField;
                if (BFI.Description != null && BFI.Description != String.Empty)
                {
                    extCol.Caption = BFI.Description;
                }
                else
                {
                    extCol.Caption = BFI.DataField;
                }
                extCol.IsKeyField = BFI.IsKey;
                //extCol.IsKeyField = IsKeyField(BFI.DataField, srcTable.PrimaryKey);
                extCol.NewLine = flag;
                extCol.Width = 120;
                extCol.DefaultValue = BFI.DefaultValue;
                extCol.Formatter = BFI.EditMask;
                extCol.FieldType = EFClientTools.Common.TypeHelper.ReturnDataType(BFI.DataType);
                if (BFI.ControlType == "ComboBox")
                {
                    String DataSourceID = GenEFDataSource(BFI, BFI.ComboEntityName, "ComboBox", "");
                    String extComboBox = GenExtComboBox(BFI, BlockItem.TableName, "ExtComboBox", "", DataSourceID);
                    extCol.EditControlId = extComboBox;
                    extCol.Editor = ExtTools.ExtGridEditor.ComboBox;
                    extCol.Mapping = BFI.ComboEntitySetName + "." + BFI.ComboValueField;
                }
                this.FieldTypeSelector(BFI.DataType, extCol, BFI.ControlType);

                aClientCollection.Add(extCol);

                flag = !flag;
            }
            WebDevPage.IHTMLElement ExtFVMaster = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("ExtFVMaster", 0);
            if (ExtFVMaster != null)
            {
                SetCollectionValue(ExtFVMaster, typeof(ExtTools.ExtFormView).GetProperty("Fields"), aClientCollection);
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
                    strModuleName = FClientData.RemoteName;
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
                        String DataSourceID = GenWebDataSource(BFI, BFI.ComboTableName, "ComboBox", "");
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
                                    String DataSourceID = GenWebDataSource(aFieldItem, aFieldItem.ComboTableName, "ComboBox", "");
                                    String S5 = "";
                                    switch (tempDefin.Name)
                                    {
                                        case "EditItemTemplate":
                                            ExtraName = "E";
                                            S5 = String.Format("<InfoLight:WebDropDownList id=\"{0}\" runat=\"server\" DataMember=\"{1}\" DataSourceID=\"{2}\" __designer:wfdid=\"w3\" DataTextField=\"{3}\" Filter DataValueField=\"{4}\" AutoInsertEmptyData=\"False\" SelectedValue='<%# Bind(\"{5}\"{6})%>'  Width=\"130px\"></InfoLight:WebDropDownList>",
                                                "wdd" + BlockItem.TableName + aFieldItem.DataField + ExtraName,
                                                aFieldItem.ComboTableName,
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
                                                aFieldItem.ComboTableName,
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
                if (FClientData.BaseFormName == "WCFWSingle" || FClientData.BaseFormName == "WCFWMasterDetail")
                {
                    GenMainBlockControl(BlockItem);
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
#if VS90
            object oExtLayout = FDesignerDocument.webControls.item("ExtLayout1", 0);
            if (oExtLayout != null && oExtLayout is WebDevPage.IHTMLElement)
            {
                ((WebDevPage.IHTMLElement)oExtLayout).setAttribute("Title", FClientData.FormTitle, 0);
            }

            object oEFDSMaster = FDesignerDocument.webControls.item("EFDSMaster", 0);
            if (oEFDSMaster != null && oEFDSMaster is WebDevPage.IHTMLElement)
            {
                ((WebDevPage.IHTMLElement)oEFDSMaster).setAttribute("RemoteName", FClientData.RemoteName, 0);
                ((WebDevPage.IHTMLElement)oEFDSMaster).setAttribute("DataMember", FClientData.EntityName, 0);
                ((WebDevPage.IHTMLElement)oEFDSMaster).setAttribute("Active", "True", 0);
            }

            object oEFDSDetail = FDesignerDocument.webControls.item("EFDSDetail", 0);
            if (oEFDSDetail != null && oEFDSDetail is WebDevPage.IHTMLElement)
            {
                ((WebDevPage.IHTMLElement)oEFDSDetail).setAttribute("RemoteName", FClientData.RemoteName, 0);
                EFClientTools.Web.EFDataSource aEFDataSource = new EFClientTools.Web.EFDataSource();
                aEFDataSource.RemoteName = FClientData.RemoteName;
                aEFDataSource.DataMember = FClientData.EntityName;
                aEFDataSource.Active = true;
                ((WebDevPage.IHTMLElement)oEFDSDetail).setAttribute("DataMember", FClientData.DetailEntityName, 0);

                ((WebDevPage.IHTMLElement)oEFDSDetail).setAttribute("Active", "True", 0);
            }
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
                        FWizardDataSet.RemoteName = FClientData.RemoteName;


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

            EFBase.EFCollection<ExtTools.ExtGridColumn> aExtGridColumnCollection = new EFBase.EFCollection<ExtTools.ExtGridColumn>(new ExtTools.ExtGridView());
            //DataTable srcTable = FWizardDataSet.RealDataSet.Tables[BlockItem.TableName];
            bool flag = true;
            Dictionary<String, String> dMappings = EFAssembly.EFClientToolsAssemblyAdapt.DesignClientUtility.GetEntityPropertieMappings(FClientData.AssemblyName, FClientData.CommandName, FClientData.DetailEntityName);
            foreach (TBlockFieldItem BFI in BlockItem.BlockFieldItems)
            {
                ExtTools.ExtGridColumn extCol = new ExtTools.ExtGridColumn();
                if (BFI.CheckNull == "Y")
                    extCol.AllowNull = false;
                else
                    extCol.AllowNull = true;
                //extCol.AllowSort = false;
                extCol.ColumnName = string.Format("col{0}", BFI.DataField);
                extCol.DataField = BFI.DataField;
                extCol.DefaultValue = BFI.DefaultValue;
                extCol.ExpandColumn = true;
                if (BFI.Description != null && BFI.Description != String.Empty)
                    extCol.HeaderText = BFI.Description;
                else
                    extCol.HeaderText = BFI.DataField;
                extCol.IsKeyField = BFI.IsKey;
                if (extCol.IsKeyField)
                {
                    foreach (var mapping in dMappings)
                    {
                        if (mapping.Key == BFI.DataField)
                            extCol.Mapping = mapping.Value;
                    }
                }
                
                //extCol.IsKeyField = IsKeyField(BFI.DataField, srcTable.PrimaryKey);
                extCol.NewLine = flag;
                //extCol.Resizable = true;
                //extCol.TextAlign = "left";
                extCol.Visible = true;
                extCol.Width = 120;
                extCol.DefaultValue = BFI.DefaultValue;
                extCol.Formatter = BFI.EditMask;
                extCol.FieldType = EFClientTools.Common.TypeHelper.ReturnDataType(BFI.DataType);
                if ((BFI.RefValNo != null && BFI.RefValNo != "") || BFI.RefField != null)
                {
                    String DataSourceID = GenEFDataSource(BFI, BlockItem.TableName, "RefVal", "");
                    String extComboBox = GenExtComboBox(BFI, BlockItem.TableName, "ExtRefVal", "", DataSourceID);
                    extCol.EditControlId = extComboBox;
                    extCol.Editor = ExtTools.ExtGridEditor.ComboBox;
                    extCol.Mapping = BFI.ComboEntitySetName + "." + BFI.ComboValueField;
                }
                else if (BFI.ControlType == "ComboBox")
                {
                    String DataSourceID = GenEFDataSource(BFI, BFI.ComboEntityName, "ComboBox", "");
                    String extComboBox = GenExtComboBox(BFI, BlockItem.TableName, "ExtComboBox", "", DataSourceID);
                    extCol.EditControlId = extComboBox;
                    extCol.Editor = ExtTools.ExtGridEditor.ComboBox;
                    extCol.Mapping = BFI.ComboEntitySetName + "." + BFI.ComboValueField;
                }
                this.FieldTypeSelector(BFI.DataType, extCol, BFI.ControlType);

                aExtGridColumnCollection.Add(extCol);

                flag = !flag;
            }
            WebDevPage.IHTMLElement ExtGVDetail = (WebDevPage.IHTMLElement)FDesignerDocument.webControls.item("ExtGVDetail", 0);
            if (ExtGVDetail != null)
            {
                SetCollectionValue(ExtGVDetail, typeof(ExtTools.ExtGridView).GetProperty("Columns"), aExtGridColumnCollection);
            }
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
#endif
            //FDesignWindow = FPI.Open("{00000000-0000-0000-0000-000000000000}");
            //FDesignWindow.Activate();
            FDesignWindow = FPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
            FDesignWindow.Activate();
        }

        public void GenWebClientModule()
        {
            GenFolder();
            if (GetForm())
            {
                GetDesignerHost();
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
                        //GenDetailBlock_2();
                        GenDetailBlock(FClientData.BaseFormName);
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

    public class MyWCFDesingerLoader : System.ComponentModel.Design.Serialization.DesignerLoader, IResourceService
    {
        private String FResxFileName;

        public MyWCFDesingerLoader(String ResxFileName)
        {
            FResxFileName = ResxFileName;
            String WebPath = EEPRegistry.WebClient;
            FResxFileName = WebPath + @"\" + FResxFileName;
        }

        public override void BeginLoad(System.ComponentModel.Design.Serialization.IDesignerLoaderHost host)
        {
        }

        public override void Dispose()
        {
        }

        public IResourceReader GetResourceReader(CultureInfo info)
        {
            return new ResXResourceReader(FResxFileName);
        }

        public IResourceWriter GetResourceWriter(CultureInfo info)
        {
            return new ResXResourceWriter(FResxFileName);
        }
    }

    public class WCFWebControlTemplate : ITemplate
    {
        private TBlockFieldItem FFieldItem;
        private String FTableName;
        private String FDataSourceID;
        private DbConnection FConnection;
        private List<WebRefVal> aWebRefValList;
        private List<AjaxTools.AjaxRefVal> aAjaxRefValList;
        private List<MyWebDropDownList> aWebDropDownList;
        private List<WebDateTimePicker> aWebDateTimePickerList;
        private List<AjaxTools.AjaxDateTimePicker> aAjaxDateTimePickerList;
        private List<WebValidateBox> aWebValidateBoxList;
        private List<System.Web.UI.WebControls.CheckBox> aWebCheckBoxList;
        private List<System.Web.UI.WebControls.TextBox> aWebTextBoxList;
        private String FKind;
        private System.Web.UI.Control FContainer;
        private ClientType FDatabaseType;
        private WebGridView FWebGridView;
        private WebValidate aWebValidate;
        private List<System.Web.UI.WebControls.Label> aLabelList;

        public WCFWebControlTemplate(String Kind, TBlockFieldItem BlockFieldItem, String TableName, String DataSourceID, DbConnection GlobalConnection, List<WebRefVal> InWebRefValList, ClientType aDatabaseType, WebGridView aGridView)
        {
            FFieldItem = BlockFieldItem;
            FTableName = TableName;
            FDataSourceID = DataSourceID;
            FConnection = GlobalConnection;
            aWebRefValList = InWebRefValList;
            FKind = Kind;
            FDatabaseType = aDatabaseType;
            FWebGridView = aGridView;
        }

        public WCFWebControlTemplate(String Kind, TBlockFieldItem BlockFieldItem, String TableName, String DataSourceID, DbConnection GlobalConnection, List<AjaxTools.AjaxRefVal> InWebRefValList, ClientType aDatabaseType, WebGridView aGridView, List<System.Web.UI.WebControls.Label> InLabelList)
        {
            FFieldItem = BlockFieldItem;
            FTableName = TableName;
            FDataSourceID = DataSourceID;
            FConnection = GlobalConnection;
            aAjaxRefValList = InWebRefValList;
            FKind = Kind;
            FDatabaseType = aDatabaseType;
            FWebGridView = aGridView;
            aLabelList = InLabelList;
        }

        public WCFWebControlTemplate(String Kind, TBlockFieldItem BlockFieldItem, String TableName, String DataSourceID, List<MyWebDropDownList> InWebDropDownList, List<System.Web.UI.WebControls.Label> InLabelList)
        {
            FFieldItem = BlockFieldItem;
            FTableName = TableName;
            FDataSourceID = DataSourceID;
            FKind = Kind;
            aWebDropDownList = InWebDropDownList;
            FWebGridView = null;
            aLabelList = InLabelList;
        }

        public WCFWebControlTemplate(String Kind, TBlockFieldItem BlockFieldItem, String TableName, WebValidate InWebValidate, List<WebValidateBox> InWebValidateBoxList, List<System.Web.UI.WebControls.Label> InLabelList)
        {
            FFieldItem = BlockFieldItem;
            FTableName = TableName;
            aWebValidate = InWebValidate;
            FKind = Kind;
            aWebValidateBoxList = InWebValidateBoxList;
            FWebGridView = null;
            aLabelList = InLabelList;
        }

        public WCFWebControlTemplate(String Kind, TBlockFieldItem BlockFieldItem, String TableName, List<WebDateTimePicker> InWebDateTimePickerList, List<System.Web.UI.WebControls.Label> InLabelList, WebGridView aGridView)
        {
            FFieldItem = BlockFieldItem;
            FTableName = TableName;
            FKind = Kind;
            aWebDateTimePickerList = InWebDateTimePickerList;
            aLabelList = InLabelList;
            FWebGridView = aGridView;
        }

        public WCFWebControlTemplate(String Kind, TBlockFieldItem BlockFieldItem, String TableName, List<AjaxTools.AjaxDateTimePicker> InWebDateTimePickerList, List<System.Web.UI.WebControls.Label> InLabelList, WebGridView aGridView)
        {
            FFieldItem = BlockFieldItem;
            FTableName = TableName;
            FKind = Kind;
            aAjaxDateTimePickerList = InWebDateTimePickerList;
            aLabelList = InLabelList;
            FWebGridView = aGridView;
        }

        public WCFWebControlTemplate(String Kind, TBlockFieldItem BlockFieldItem, String TableName, List<WebDateTimePicker> InWebDateTimePickerList, List<System.Web.UI.WebControls.Label> InLabelList)
        {
            FFieldItem = BlockFieldItem;
            FTableName = TableName;
            FKind = Kind;
            aWebDateTimePickerList = InWebDateTimePickerList;
            aLabelList = InLabelList;
        }

        public WCFWebControlTemplate(String Kind, TBlockFieldItem BlockFieldItem, String TableName, List<System.Web.UI.WebControls.CheckBox> InWebCheckBoxList, List<System.Web.UI.WebControls.Label> InLabelList)
        {
            FFieldItem = BlockFieldItem;
            FTableName = TableName;
            FKind = Kind;
            aWebCheckBoxList = InWebCheckBoxList;
            aLabelList = InLabelList;
        }

        public WCFWebControlTemplate(String Kind, TBlockFieldItem BlockFieldItem, String TableName, List<System.Web.UI.WebControls.TextBox> InWebTextBoxList, List<System.Web.UI.WebControls.Label> InLabelList, WebGridView aGridView)
        {
            FFieldItem = BlockFieldItem;
            FTableName = TableName;
            FKind = Kind;
            aWebTextBoxList = InWebTextBoxList;
            aLabelList = InLabelList;
            FWebGridView = aGridView;
        }

        public ClientType DatabaseType
        {
            get { return FDatabaseType; }
            set { FDatabaseType = value; }
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            FContainer = container;
            switch (FKind)
            {
                case "WebGridViewRefValEditItemTemplate":
                    GenRefValTemplate("E");
                    break;
                case "WebGridViewRefValItemTemplate":
                    GenRefValTemplate("");
                    break;
                case "WebGridViewRefValFooterItemTemplate":
                    GenRefValTemplate("F");
                    break;
                case "DetailsViewRefValInsertItemTemplate":
                    GenRefValTemplate("I");
                    break;

                case "DetailsViewValidateBoxInsertItemTemplate":
                    GenValidateBoxTemplate("I");
                    break;

                case "DetailsViewComboBoxInsertItemTemplate":
                    GenComboBoxTemplate("I");
                    break;

                case "WebGridViewComboBoxEditItemTemplate":
                    GenComboBoxTemplate("E");
                    break;
                case "WebGridViewComboBoxItemTemplate":
                    GenComboBoxTemplate("");
                    break;
                case "WebGridViewComboBoxFooterItemTemplate":
                    GenComboBoxTemplate("F");
                    break;

                case "WebGridViewDateTimeItemTemplate":
                    GenDateTimeTemplate("");
                    break;
                case "WebGridViewDateTimeEditItemTemplate":
                    GenDateTimeTemplate("E");
                    break;
                case "WebGridViewDateTimeFooterItemTemplate":
                    GenDateTimeTemplate("F");
                    break;
                case "DetailsViewDateTimeInsertItemTemplate":
                    GenDateTimeTemplate("I");
                    break;

                case "WebGridViewValidateBoxEditItemTemplate":
                    GenValidateBoxTemplate("E");
                    break;
                case "WebGridViewValidateBoxItemTemplate":
                    GenValidateBoxTemplate("");
                    break;
                case "WebGridViewValidateBoxFooterItemTemplate":
                    GenValidateBoxTemplate("F");
                    break;

                case "WebGridViewCheckBoxEditItemTemplate":
                    GenCheckBoxTemplate("E");
                    break;
                case "WebGridViewCheckBoxItemTemplate":
                    GenCheckBoxTemplate("");
                    break;
                case "WebGridViewCheckBoxFooterItemTemplate":
                    GenCheckBoxTemplate("F");
                    break;
                case "DetailsViewCheckBoxInsertItemTemplate":
                    GenCheckBoxTemplate("I");
                    break;

                case "WebGridViewTextBoxEditItemTemplate":
                    GenTextBoxTemplate("E");
                    break;
                case "WebGridViewTextBoxItemTemplate":
                    GenTextBoxTemplate("");
                    break;
                case "WebGridViewTextBoxFooterItemTemplate":
                    GenTextBoxTemplate("F");
                    break;
                case "DetailsViewTextBoxInsertItemTemplate":
                    GenTextBoxTemplate("I");
                    break;

                case "WebGridViewAjaxRefValEditItemTemplate":
                    GenAjaxRefValTemplate("E");
                    break;
                case "WebGridViewAjaxRefValItemTemplate":
                    GenAjaxRefValTemplate("");
                    break;
                case "WebGridViewAjaxRefValFooterItemTemplate":
                    GenAjaxRefValTemplate("F");
                    break;
                case "DetailsViewAjaxRefValInsertItemTemplate":
                    GenAjaxRefValTemplate("I");
                    break;

                case "WebGridViewAjaxDateTimeItemTemplate":
                    GenAjaxDateTimeTemplate("");
                    break;
                case "WebGridViewAjaxDateTimeEditItemTemplate":
                    GenAjaxDateTimeTemplate("E");
                    break;
                case "WebGridViewAjaxDateTimeFooterItemTemplate":
                    GenAjaxDateTimeTemplate("F");
                    break;
                case "DetailsViewAjaxDateTimeInsertItemTemplate":
                    GenAjaxDateTimeTemplate("I");
                    break;
            }
        }

        private void GenRefValTemplate(String ExtraName)
        {
            //WebRefVal
            WebRefVal aWebRefVal = new WebRefVal();
            aWebRefVal.ID = "wrv" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
            aWebRefVal.DataSourceID = FDataSourceID;
            //if (ExtraName != "F")
            aWebRefVal.DataBindingField = FFieldItem.DataField;
            if (ExtraName == "")
            {
                aWebRefVal.ReadOnly = true;
                aWebRefVal.BackColor = Color.Transparent;
                aWebRefVal.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            }
            aWebRefVal.Width = 130;

            if (FFieldItem.RefField != null)
            {
                aWebRefVal.DataValueField = FFieldItem.RefField.ValueMember;
                aWebRefVal.DataTextField = FFieldItem.RefField.DisplayMember;
                foreach (RefColumns aColumn in FFieldItem.RefField.LookupColumns)
                {
                    WebRefColumn RC = new WebRefColumn();
                    RC.ColumnName = aColumn.Column;
                    RC.HeadText = aColumn.HeaderText;
                    aWebRefVal.Columns.Add(RC);
                }
            }
            else
            {
                InfoCommand aInfoCommand = new InfoCommand(DatabaseType);
                //aInfoCommand.Connection = WzdUtils.AllocateConnection(DatabaseName, DatabaseType, true);
                aInfoCommand.Connection = FConnection;
                IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                DataSet aDataSet = new DataSet();
                //SYS_REFVAL
                aDataSet.Clear();
                aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", FFieldItem.RefValNo);
                WzdUtils.FillDataAdapter(FDatabaseType, DA, aDataSet, FFieldItem.RefValNo);
                aWebRefVal.DataValueField = aDataSet.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
                aWebRefVal.DataTextField = aDataSet.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                //aWebRefVal.BindingValue = "'<%# + Bind(\"" + FFieldItem.DataField + "\") %>'";
                //SYS_REFVSL_D1 --> Columns
                aDataSet.Clear();
                aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL_D1 where REFVAL_NO = '{0}'", FFieldItem.RefValNo);
                WzdUtils.FillDataAdapter(FDatabaseType, DA, aDataSet, FFieldItem.RefValNo);
                foreach (DataRow DR in aDataSet.Tables[0].Rows)
                {
                    WebRefColumn RC = new WebRefColumn();
                    RC.ColumnName = DR["FIELD_NAME"].ToString();
                    RC.HeadText = DR["HEADER_TEXT"].ToString();
                    aWebRefVal.Columns.Add(RC);
                }
            }

            FContainer.Controls.Add(aWebRefVal);
            Boolean Found = false;
            foreach (WebRefVal bWebRefVal in aWebRefValList)
            {
                if (String.Compare(bWebRefVal.ID, aWebRefVal.ID) == 0)
                {
                    Found = true;
                    break;
                }
            }
            if (!Found)
                aWebRefValList.Add(aWebRefVal);

            //Add AddNewRowControlItem to WebGridView
            if (ExtraName == "F")
            {
                if (FWebGridView != null)
                {
                    Found = false;
                    foreach (AddNewRowControlItem aControlItem in FWebGridView.AddNewRowControls)
                    {
                        if (aControlItem.FieldName.CompareTo(FFieldItem.DataField) == 0)
                        {
                            Found = true;
                            break;
                        }
                    }
                    if (!Found)
                    {
                        AddNewRowControlItem aItem = new AddNewRowControlItem();
                        aItem.ControlID = "wrv" + FTableName + FFieldItem.DataField + ExtraName;
                        aItem.ControlType = WebGridView.AddNewRowControlType.RefVal;
                        aItem.FieldName = FFieldItem.DataField;
                        FWebGridView.AddNewRowControls.Add(aItem);
                    }
                }
            }
        }

        private void GenAjaxRefValTemplate(String ExtraName)
        {
            if (ExtraName == "")
            {
                System.Web.UI.WebControls.Label aLabel = new System.Web.UI.WebControls.Label();
                aLabel.ID = "l" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                aLabel.ToolTip = FFieldItem.DataField;
                FContainer.Controls.Add(aLabel);

                Boolean isFound = false;
                foreach (System.Web.UI.WebControls.Label bLabel in aLabelList)
                {
                    if (String.Compare(aLabel.ID, bLabel.ID) == 0)
                    {
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                {
                    aLabelList.Add(aLabel);
                }
            }
            else
            {
                //AjaxRefVal
                AjaxTools.AjaxRefVal aAjaxRefVal = new AjaxTools.AjaxRefVal();
                aAjaxRefVal.ID = "arv" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                aAjaxRefVal.DataSourceID = FDataSourceID;
                //if (ExtraName != "F")
                aAjaxRefVal.BindingValue = FFieldItem.DataField;
                aAjaxRefVal.Width = 130;

                if (FFieldItem.RefField != null)
                {
                    aAjaxRefVal.DataValueField = FFieldItem.RefField.ValueMember;
                    aAjaxRefVal.DataTextField = FFieldItem.RefField.DisplayMember;
                    foreach (RefColumns aColumn in FFieldItem.RefField.LookupColumns)
                    {
                        WebRefColumn RC = new WebRefColumn();
                        RC.ColumnName = aColumn.Column;
                        RC.HeadText = aColumn.HeaderText;
                        aAjaxRefVal.Columns.Add(RC);
                    }
                }
                else
                {
                    InfoCommand aInfoCommand = new InfoCommand(DatabaseType);
                    //aInfoCommand.Connection = WzdUtils.AllocateConnection(DatabaseName, DatabaseType, true);
                    aInfoCommand.Connection = FConnection;
                    IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                    DataSet aDataSet = new DataSet();
                    //SYS_REFVAL
                    aDataSet.Clear();
                    aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL where REFVAL_NO = '{0}'", FFieldItem.RefValNo);
                    WzdUtils.FillDataAdapter(FDatabaseType, DA, aDataSet, FFieldItem.RefValNo);
                    aAjaxRefVal.DataValueField = aDataSet.Tables[0].Rows[0]["VALUE_MEMBER"].ToString();
                    aAjaxRefVal.DataTextField = aDataSet.Tables[0].Rows[0]["DISPLAY_MEMBER"].ToString();
                    //aWebRefVal.BindingValue = "'<%# + Bind(\"" + FFieldItem.DataField + "\") %>'";
                    //SYS_REFVSL_D1 --> Columns
                    aDataSet.Clear();
                    aInfoCommand.CommandText = String.Format("Select * from SYS_REFVAL_D1 where REFVAL_NO = '{0}'", FFieldItem.RefValNo);
                    WzdUtils.FillDataAdapter(FDatabaseType, DA, aDataSet, FFieldItem.RefValNo);
                    foreach (DataRow DR in aDataSet.Tables[0].Rows)
                    {
                        WebRefColumn RC = new WebRefColumn();
                        RC.ColumnName = DR["FIELD_NAME"].ToString();
                        RC.HeadText = DR["HEADER_TEXT"].ToString();
                        aAjaxRefVal.Columns.Add(RC);
                    }
                }

                FContainer.Controls.Add(aAjaxRefVal);
                Boolean Found = false;
                foreach (AjaxTools.AjaxRefVal bWebRefVal in aAjaxRefValList)
                {
                    if (String.Compare(bWebRefVal.ID, aAjaxRefVal.ID) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                    aAjaxRefValList.Add(aAjaxRefVal);

                //Add AddNewRowControlItem to WebGridView
                if (ExtraName == "F")
                {
                    if (FWebGridView != null)
                    {
                        Found = false;
                        foreach (AddNewRowControlItem aControlItem in FWebGridView.AddNewRowControls)
                        {
                            if (aControlItem.FieldName.CompareTo(FFieldItem.DataField) == 0)
                            {
                                Found = true;
                                break;
                            }
                        }
                        if (!Found)
                        {
                            AddNewRowControlItem aItem = new AddNewRowControlItem();
                            aItem.ControlID = "arv" + FTableName + FFieldItem.DataField + ExtraName;
                            aItem.ControlType = WebGridView.AddNewRowControlType.RefVal;
                            aItem.FieldName = FFieldItem.DataField;
                            FWebGridView.AddNewRowControls.Add(aItem);
                        }
                    }
                }
            }
        }

        private void GenComboBoxTemplate(String ExtraName)
        {
            if (ExtraName == "")
            {
                System.Web.UI.WebControls.Label aLabel = new System.Web.UI.WebControls.Label();
                aLabel.ID = "l" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                aLabel.ToolTip = FFieldItem.DataField;
                FContainer.Controls.Add(aLabel);

                Boolean Found = false;
                foreach (System.Web.UI.WebControls.Label bLabel in aLabelList)
                {
                    if (String.Compare(aLabel.ID, bLabel.ID) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                {
                    aLabelList.Add(aLabel);
                }
            }
            else
            {
                WebDropDownList aComboBox = new WebDropDownList();
                aComboBox.ID = "wdd" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                aComboBox.DataSourceID = FDataSourceID;
                aComboBox.DataMember = FFieldItem.ComboEntityName;
                aComboBox.DataTextField = FFieldItem.ComboTextField;
                aComboBox.DataValueField = FFieldItem.ComboValueField;
                aComboBox.Width = 130;
                FContainer.Controls.Add(aComboBox);

                Boolean Found = false;
                foreach (MyWebDropDownList aDropDownList in aWebDropDownList)
                {
                    if (String.Compare(aComboBox.ID, aDropDownList.WebDropDownList.ID) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                {
                    MyWebDropDownList aDropDown = new MyWebDropDownList(aComboBox, FFieldItem.DataField);
                    aWebDropDownList.Add(aDropDown);


                    //Add AddNewRowControlItem to WebGridView
                    if (ExtraName == "F")
                    {
                        if (FWebGridView != null)
                        {
                            Found = false;
                            foreach (AddNewRowControlItem aControlItem in FWebGridView.AddNewRowControls)
                            {
                                if (aControlItem.FieldName.CompareTo(FFieldItem.DataField) == 0)
                                {
                                    Found = true;
                                    break;
                                }
                            }
                            if (!Found)
                            {
                                AddNewRowControlItem aItem = new AddNewRowControlItem();
                                aItem.ControlID = "wdd" + FTableName + FFieldItem.DataField + ExtraName;
                                aItem.ControlType = WebGridView.AddNewRowControlType.DropDownList;
                                aItem.FieldName = FFieldItem.DataField;
                                FWebGridView.AddNewRowControls.Add(aItem);
                            }
                        }
                    }
                }
            }
        }

        private void GenValidateBoxTemplate(String ExtraName)
        {
            if (ExtraName == "")
            {
                System.Web.UI.WebControls.Label aLabel = new System.Web.UI.WebControls.Label();
                aLabel.ID = "l" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                aLabel.ToolTip = FFieldItem.DataField;
                FContainer.Controls.Add(aLabel);

                Boolean Found = false;
                foreach (System.Web.UI.WebControls.Label bLabel in aLabelList)
                {
                    if (String.Compare(aLabel.ID, bLabel.ID) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                {
                    aLabelList.Add(aLabel);
                }
            }
            else
            {
                WebValidateBox aValidateBox = new WebValidateBox();
                aValidateBox.ID = "wvb" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                aValidateBox.WebValidateID = aWebValidate.ID;
                aValidateBox.ValidateField = FFieldItem.DataField;
                FContainer.Controls.Add(aValidateBox);

                Boolean Found = false;
                foreach (WebValidateBox aBox in aWebValidateBoxList)
                {
                    if (String.Compare(aBox.ID, aValidateBox.ID) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                {
                    aWebValidateBoxList.Add(aValidateBox);
                }
            }
        }

        private void GenDateTimeTemplate(String ExtraName)
        {
            if (ExtraName == "")
            {
                System.Web.UI.WebControls.Label aLabel = new System.Web.UI.WebControls.Label();
                aLabel.ID = "l" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                aLabel.ToolTip = FFieldItem.DataField;
                FContainer.Controls.Add(aLabel);

                Boolean Found = false;
                foreach (System.Web.UI.WebControls.Label bLabel in aLabelList)
                {
                    if (String.Compare(aLabel.ID, bLabel.ID) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                {
                    aLabelList.Add(aLabel);
                }
            }
            else
            {
                WebDateTimePicker aPicker = new WebDateTimePicker();
                aPicker.ID = "wdtp" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                if (FFieldItem.DataType == typeof(DateTime))
                    aPicker.DateTimeType = dateTimeType.DateTime;
                else if (FFieldItem.DataType == typeof(String))
                    aPicker.DateTimeType = dateTimeType.VarChar;
                aPicker.ToolTip = FFieldItem.DataField;
                //aPicker.Text = String.Format("'<%# Bind(\"{0}\") %>'", FFieldItem.DataField);
                FContainer.Controls.Add(aPicker);
                Boolean Found = false;
                foreach (WebDateTimePicker aWebPicker in aWebDateTimePickerList)
                {
                    if (String.Compare(aPicker.ID, aWebPicker.ID) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                {
                    aWebDateTimePickerList.Add(aPicker);

                    //Add AddNewRowControlItem to WebGridView
                    if (ExtraName == "F")
                    {
                        if (FWebGridView != null)
                        {
                            Found = false;
                            foreach (AddNewRowControlItem aControlItem in FWebGridView.AddNewRowControls)
                            {
                                if (aControlItem.FieldName.CompareTo(FFieldItem.DataField) == 0)
                                {
                                    Found = true;
                                    break;
                                }
                            }
                            if (!Found)
                            {
                                AddNewRowControlItem aItem = new AddNewRowControlItem();
                                aItem.ControlID = "wdtp" + FTableName + FFieldItem.DataField + ExtraName;
                                aItem.ControlType = WebGridView.AddNewRowControlType.DateTimePicker;
                                aItem.FieldName = FFieldItem.DataField;
                                FWebGridView.AddNewRowControls.Add(aItem);
                            }
                        }
                    }
                }
            }
        }

        private void GenAjaxDateTimeTemplate(String ExtraName)
        {
            if (ExtraName == "")
            {
                System.Web.UI.WebControls.Label aLabel = new System.Web.UI.WebControls.Label();
                aLabel.ID = "l" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                aLabel.ToolTip = FFieldItem.DataField;
                FContainer.Controls.Add(aLabel);

                Boolean Found = false;
                foreach (System.Web.UI.WebControls.Label bLabel in aLabelList)
                {
                    if (String.Compare(aLabel.ID, bLabel.ID) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                {
                    aLabelList.Add(aLabel);
                }
            }
            else
            {
                AjaxTools.AjaxDateTimePicker aAjaxPicker = new AjaxTools.AjaxDateTimePicker();
                aAjaxPicker.ID = "adtp" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                if (FFieldItem.DataType == typeof(DateTime))
                    aAjaxPicker.DateTimeType = dateTimeType.DateTime;
                else if (FFieldItem.DataType == typeof(String))
                    aAjaxPicker.DateTimeType = dateTimeType.VarChar;
                aAjaxPicker.ToolTip = FFieldItem.DataField;
                FContainer.Controls.Add(aAjaxPicker);
                Boolean Found = false;
                foreach (AjaxTools.AjaxDateTimePicker aPicker in aAjaxDateTimePickerList)
                {
                    if (String.Compare(aAjaxPicker.ID, aPicker.ID) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                {
                    aAjaxDateTimePickerList.Add(aAjaxPicker);

                    //Add AddNewRowControlItem to WebGridView
                    if (ExtraName == "F")
                    {
                        if (FWebGridView != null)
                        {
                            Found = false;
                            foreach (AddNewRowControlItem aControlItem in FWebGridView.AddNewRowControls)
                            {
                                if (aControlItem.FieldName.CompareTo(FFieldItem.DataField) == 0)
                                {
                                    Found = true;
                                    break;
                                }
                            }
                            if (!Found)
                            {
                                AddNewRowControlItem aItem = new AddNewRowControlItem();
                                aItem.ControlID = "adtp" + FTableName + FFieldItem.DataField + ExtraName;
                                aItem.ControlType = WebGridView.AddNewRowControlType.DateTimePicker;
                                aItem.FieldName = FFieldItem.DataField;
                                FWebGridView.AddNewRowControls.Add(aItem);
                            }
                        }
                    }
                }
            }
        }

        private void GenCheckBoxTemplate(String ExtraName)
        {
            if (ExtraName == "")
            {
                System.Web.UI.WebControls.Label aLabel = new System.Web.UI.WebControls.Label();
                aLabel.ID = "l" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                aLabel.ToolTip = FFieldItem.DataField;
                FContainer.Controls.Add(aLabel);

                Boolean Found = false;
                foreach (System.Web.UI.WebControls.Label bLabel in aLabelList)
                {
                    if (String.Compare(aLabel.ID, bLabel.ID) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                {
                    aLabelList.Add(aLabel);
                }
            }
            else
            {
                System.Web.UI.WebControls.CheckBox aCheckBox = new System.Web.UI.WebControls.CheckBox();
                aCheckBox.ID = "wdtp" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                aCheckBox.ToolTip = FFieldItem.DataField;
                //aPicker.Text = String.Format("'<%# Bind(\"{0}\") %>'", FFieldItem.DataField);
                FContainer.Controls.Add(aCheckBox);
                Boolean Found = false;
                foreach (System.Web.UI.WebControls.CheckBox bWebPicker in aWebCheckBoxList)
                {
                    if (String.Compare(aCheckBox.ID, bWebPicker.ID) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                {
                    aWebCheckBoxList.Add(aCheckBox);


                    //Add AddNewRowControlItem to WebGridView
                    if (ExtraName == "F")
                    {
                        if (FWebGridView != null)
                        {
                            Found = false;
                            foreach (AddNewRowControlItem aControlItem in FWebGridView.AddNewRowControls)
                            {
                                if (aControlItem.FieldName.CompareTo(FFieldItem.DataField) == 0)
                                {
                                    Found = true;
                                    break;
                                }
                            }
                            if (!Found)
                            {
                                AddNewRowControlItem aItem = new AddNewRowControlItem();
                                aItem.ControlID = "wdtp" + FTableName + FFieldItem.DataField + ExtraName;
                                aItem.ControlType = WebGridView.AddNewRowControlType.CheckBox;
                                aItem.FieldName = FFieldItem.DataField;
                                FWebGridView.AddNewRowControls.Add(aItem);
                            }
                        }
                    }
                }
            }
        }

        private void GenTextBoxTemplate(String ExtraName)
        {
            if (ExtraName == "")
            {
                System.Web.UI.WebControls.Label aLabel = new System.Web.UI.WebControls.Label();
                aLabel.ID = "l" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                aLabel.ToolTip = FFieldItem.DataField;
                FContainer.Controls.Add(aLabel);

                Boolean Found = false;
                foreach (System.Web.UI.WebControls.Label bLabel in aLabelList)
                {
                    if (String.Compare(aLabel.ID, bLabel.ID) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                {
                    aLabelList.Add(aLabel);
                }
            }
            else
            {
                System.Web.UI.WebControls.TextBox aTextBox = new System.Web.UI.WebControls.TextBox();
                aTextBox.ID = "wdtp" + FTableName + FFieldItem.DataField + ExtraName + "GridView";
                aTextBox.ToolTip = FFieldItem.DataField;
                //aPicker.Text = String.Format("'<%# Bind(\"{0}\") %>'", FFieldItem.DataField);
                FContainer.Controls.Add(aTextBox);
                Boolean Found = false;
                foreach (System.Web.UI.WebControls.TextBox bWebPicker in aWebTextBoxList)
                {
                    if (String.Compare(aTextBox.ID, bWebPicker.ID) == 0)
                    {
                        Found = true;
                        break;
                    }
                }
                if (!Found)
                {
                    aWebTextBoxList.Add(aTextBox);

                    //Add AddNewRowControlItem to WebGridView
                    if (ExtraName == "F")
                    {
                        if (FWebGridView != null)
                        {
                            Found = false;
                            foreach (AddNewRowControlItem aControlItem in FWebGridView.AddNewRowControls)
                            {
                                if (aControlItem.FieldName.CompareTo(FFieldItem.DataField) == 0)
                                {
                                    Found = true;
                                    break;
                                }
                            }
                            if (!Found)
                            {
                                AddNewRowControlItem aItem = new AddNewRowControlItem();
                                aItem.ControlID = "wdtp" + FTableName + FFieldItem.DataField + ExtraName;
                                aItem.ControlType = WebGridView.AddNewRowControlType.TextBox;
                                aItem.FieldName = FFieldItem.DataField;
                                FWebGridView.AddNewRowControls.Add(aItem);
                            }
                        }
                    }
                }
            }
        }
    }

    public class TWCFDetailItem : System.ComponentModel.Component
    {
        private TBlockFieldItems FBlockFieldItems;
        private String FCommandName;
        private String FEntityName;

        public TWCFDetailItem()
        {
            FBlockFieldItems = new TBlockFieldItems(this);
        }

        public TBlockFieldItems BlockFieldItems
        {
            get
            {
                return FBlockFieldItems;
            }
            set
            {
                FBlockFieldItems = value;
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

        public String EntityName
        {
            get { return FEntityName; }
            set { FEntityName = value; }
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
