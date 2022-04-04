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
using System.ComponentModel.Design;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using System.Data.Common;
using Microsoft.Win32;
using System.Reflection;
using Srvtools;
using EFServerTools;
using System.Threading;
using System.Xml.Linq;
using System.Linq;

namespace MWizard
{

    public partial class fmWCFServerWzd : Form
    {
#if VS90
        const string REGISTRYNAME = "infolight\\eep.net2008";
#else
        const string REGISTRYNAME = "infolight\\eep.net";
#endif

        private TWCFServerData FServerData;
        private DTE2 FDTE2 = null;
        private AddIn FAddIn = null;
        private DbConnection InternalConnection = null;
        private TStringList FAlias;
        private static string _serverPath;
        private EEPWizard FEEPWizard;
        private ListViewColumnSorter lvwColumnSorter;
        private String FTemplateName;
        private Project GlobalProject;

        public fmWCFServerWzd()
        {
            InitializeComponent();
            FServerData = new TWCFServerData(this);
            //PrepareWizardService();
            lvwColumnSorter = new ListViewColumnSorter();
            this.lvSelectedFields.ListViewItemSorter = lvwColumnSorter;
        }

        public fmWCFServerWzd(DTE2 aDTE2, AddIn aAddIn, EEPWizard aEEPWizard)
        {
            InitializeComponent();
            FServerData = new TWCFServerData(this);
            FDTE2 = aDTE2;
            FAddIn = aAddIn;
            FEEPWizard = aEEPWizard;
            //PrepareWizardService();
            lvwColumnSorter = new ListViewColumnSorter();
            this.lvSelectedFields.ListViewItemSorter = lvwColumnSorter;
        }

        private void PrepareWizardService()
        {
            Show();
            Hide();
        }

        public TStringList AliasList
        {
            get { return FAlias; }
        }

        private void ClearValues()
        {
            tbCurrentSolution.Text = "";
            tbNewLocation.Text = "";
            tbNewSolutionName.Text = "";
            tbPackageName.Text = "WCFServerPackage";
            tbSolutionName.Text = "";
            tbOutputPath.Text = "";
            tbAssemblyOutputPath.Text = "";
            tvTables.Nodes.Clear();
            lvSelectedFields.Items.Clear();
            if (FServerData.Datasets != null)
                FServerData.Datasets.Clear();
        }

        public void ShowServerWizard()
        {
            //Show();
            Init();
            ShowDialog();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 10001)
            {
                SDGenServerModule("");
            }
        }

        public void SDGenServerModule(string XML)
        {
            if (XML != "")
            {
                FServerData.Datasets.Clear();
                FServerData.LoadFromXML(XML);
            }
            TWCFServerGenerator SG = new TWCFServerGenerator(FServerData, FDTE2, FAddIn, GlobalProject);
            SG.GenServerModule();
        }

        private void Init()
        {
            ClearValues();
            tabControl.SelectedTab = tpOutputSetting;
            btnNewSubDataset.Enabled = false;
            btnDeleteDataset.Enabled = false;
            btnNewField.Enabled = false;
            btnDeleteField.Enabled = false;
            if (((FDTE2 != null) && (FDTE2.Solution.FileName != "")) && File.Exists(FDTE2.Solution.FileName))
            {
                rbAddToCurrent.Enabled = true;
                rbAddToCurrent.Checked = true;
                tbCurrentSolution.Text = FDTE2.Solution.FileName;
                EnabledOutputControls();
            }

            DisplayPage(tpOutputSetting);
        }

        public TWCFServerData ServerData
        {
            get
            {
                return FServerData;
            }
        }

        public void CheckCurrentSolution()
        {
        }

        private void DisplayPage(TabPage aPage)
        {
            while (tabControl.TabPages.Count > 0)
                tabControl.TabPages.Remove(tabControl.TabPages[0]);
            tabControl.TabPages.Add(aPage);
            tabControl.SelectedTab = aPage;
            EnableButton();
        }

        private void EnableButton()
        {
            btnPrevious.Enabled = tabControl.SelectedTab != tpOutputSetting;
            btnNext.Enabled = tabControl.SelectedTab != tpTables;
            btnDone.Enabled = tabControl.SelectedTab == tpTables;
            btnCancel.Enabled = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedTab.Equals(tpOutputSetting))
                {
                    this.cbGenerateEntity.SelectedIndex = 0;

                    if (cbChooseLanguage.Text == "" || cbChooseLanguage.Text == "C#")
                        FServerData.Language = "cs";
                    else if (cbChooseLanguage.Text == "VB")
                        FServerData.Language = "vb";

                    if (rbAddToCurrent.Checked)
                    {
                        FServerData.SolutionName = tbCurrentSolution.Text;
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
                        FServerData.SolutionName = tbNewSolutionName.Text;
                        FServerData.OutputPath = tbNewLocation.Text;
                        FServerData.CodeOutputPath = tbOutputPath.Text;
                    }
                    if (rbAddToExistSln.Checked)
                    {
                        if (tbSolutionName.Text == "")
                        {
                            MessageBox.Show("Please input Location !!");
                            if (tbSolutionName.CanFocus)
                            {
                                tbSolutionName.Focus();
                            }
                            return;
                        }
                        FServerData.SolutionName = tbSolutionName.Text;
                        FServerData.CodeOutputPath = tbOutputPath.Text;
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
                        FServerData.PackageName = tbPackageName.Text;
                        FServerData.NewSolution = rbNewSolution.Checked;
                        FServerData.CodeOutputPath = tbOutputPath.Text;
                        this.Hide();
                        if (GenSolution())
                        {
                            DisplayPage(tpTables);
                        }
                        this.Show();
                    }
                    FServerData.AssemblyOutputPath = tbAssemblyOutputPath.Text;
                }
                BringToFront();
            }
            catch (Exception ex)
            {
                WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                return;
            }
        }

        private bool GenSolution()
        {
            Solution sln = /*(Solution2)*/FDTE2.Solution;
            
            switch (FServerData.Language)
            {
                case "cs":
                    FTemplateName = Path.GetDirectoryName(FAddIn.Object.GetType().Assembly.Location) + "\\Templates\\WCFServerPackage\\WCFServerPackage.csproj";
                    break;
                case "vb":
                    FTemplateName = Path.GetDirectoryName(FAddIn.Object.GetType().Assembly.Location) + "\\Templates\\VBServerPackage\\VBServerPackage.vbproj";
                    break;
                default:
                    FTemplateName = Path.GetDirectoryName(FAddIn.Object.GetType().Assembly.Location) + "\\Templates\\WCFServerPackage\\WCFServerPackage.csproj";
                    break;
            }

            if (FServerData.NewSolution)
            {
                if (System.IO.Directory.Exists(FServerData.OutputPath))
                {
                    if (FServerData.OutputPath == "\\")
                        throw new Exception("Unknown Output Path: " + "\\");
                    System.IO.Directory.Delete(FServerData.OutputPath, true);
                }
                sln.Create(FServerData.OutputPath, FServerData.SolutionName);
                ProjectLoader.AddDefaultProject(FDTE2);
                Project P = sln.AddFromTemplate(FTemplateName,
                    FServerData.CodeOutputPath + "\\" + FServerData.PackageName, FServerData.PackageName, false);
                P.Name = FServerData.PackageName;
                string FileName = FServerData.OutputPath + "\\" + FServerData.SolutionName + ".sln";
                sln.SaveAs(FileName);
                //sln.Open(FileName);
                sln.SolutionBuild.StartupProjects = P;
                sln.SolutionBuild.BuildProject(sln.SolutionBuild.ActiveConfiguration.Name, P.FullName, true);
                GlobalProject = P;
                ////sln.SolutionBuild.Clean(true);
            }
            else
            {
                string FilePath = FServerData.CodeOutputPath + "\\" + FServerData.PackageName;
                //string FilePath = Path.GetDirectoryName(FServerData.SolutionName) + "\\" + FServerData.PackageName;
                if (System.IO.Directory.Exists(FilePath))
                {
                    if (FilePath == "\\")
                        throw new Exception("Unknown Output Path: " + "\\");

                    DialogResult dr = MessageBox.Show("There is another File which name is " + FServerData.PackageName + " existed! Do you want to delete it first", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        System.IO.Directory.Delete(FilePath, true);
                    }
                    else
                    {
                        return false;
                    }

                }
                Project P = sln.AddFromTemplate(FTemplateName, FilePath, FServerData.PackageName, true);//sln.Projects.Item(16); 
                P.Name = FServerData.PackageName;
                string FileName = FilePath + "\\" + FServerData.PackageName + "." + FServerData.Language + "proj";
                P.Save(FileName);
                sln.Open(FServerData.SolutionName);
                int I;
                P = null;
                for (I = 1; I <= sln.Projects.Count; I++)
                {
                    P = sln.Projects.Item(I);
                    if (string.Compare(P.Name, FServerData.PackageName, true) == 0) //大小写重命问题
                        break;
                    else
                        P = null;
                }
                if (P != null)
                    sln.Remove(P);
                P = sln.AddFromFile(FilePath + "\\" + FServerData.PackageName + "." + FServerData.Language + "proj", false);
                P.Properties.Item("RootNamespace").Value = FServerData.PackageName;
                P.Properties.Item("AssemblyName").Value = FServerData.PackageName;
                sln.SaveAs(FServerData.SolutionName);
                sln.SolutionBuild.StartupProjects = P;
                sln.SolutionBuild.BuildProject(sln.SolutionBuild.ActiveConfiguration.Name, P.FullName, true);
                GlobalProject = P;
                //sln.SolutionBuild.Clean(true);
                if (FServerData.AssemblyOutputPath != null && FServerData.AssemblyOutputPath != "")
                    GlobalProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value = FServerData.AssemblyOutputPath;

                try
                {
                    EnvDTE.DTE dte = System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.11.0") as DTE;
                    EnvDTE80.DTE2 dte2 = FDTE2;//Activator.CreateInstance(Type.GetTypeFromProgID("VisualStudio.DTE.10.0")) as EnvDTE80.DTE2;

                    string vsWizardAddItem = "{0F90E1D1-4999-11D1-B6D1-00A0C90F2744}";//WizardType Guid
                    bool silent = false;

                    int commonIndex = dte2.Application.FileName.IndexOf(@"\Common7");
                    string vsInstallPath = dte2.Application.FileName.Substring(0, commonIndex);
                    Project project = P;

                    //object[] obj = dte.ActiveSolutionProjects as object[];
                    //if (obj.Length > 0)
                    //    project = obj[0] as Project;
                    //Project project = (Project)(((object[])dte.ActiveSolutionProjects)[0]);

                    string itemName = project.Name + ".edmx";
                    string localDir = System.IO.Path.GetDirectoryName(project.FullName);

                    object[] prams = {vsWizardAddItem,project.Name,project.ProjectItems,
                             localDir, itemName,vsInstallPath, silent};

                    Solution2 soln = (Solution2)dte2.Solution;
                    //string templatePath = soln.GetProjectItemTemplate("Form.zip", "CSharp");
                    string templatePath = soln.GetProjectItemTemplate("AdoNetEntityDataModelCSharp.zip", "CSharp");
                    dte.LaunchWizard(templatePath, ref prams);

                    project.Save(FileName);
                    //a.GetEntitySetNames();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            foreach (ProjectItem pi in GlobalProject.ProjectItems)
            {
                if (pi.Name.EndsWith("edmx"))
                {
                    String strOldName = pi.Name;
                    pi.Name = GlobalProject.Name + "123.edmx";
                    pi.Name = strOldName;

                    String fileName = FServerData.CodeOutputPath + "\\" + FServerData.PackageName + "\\" + strOldName;
                    String extraName = FServerData.PackageName + "_";
                    XDocument xml = new XDocument();
                    xml = XDocument.Load(fileName);
                    if (xml.Elements().Count() > 0)
                    {
                        //ConceptualModels
                        IEnumerable<XElement> xConceptualModels = GetXElementByLocalName(xml.Elements(), "ConceptualModels");
                        IEnumerable<XElement> xEntitySets = GetXElementByLocalName(xConceptualModels, "EntitySet");
                        if (xEntitySets != null)
                        {
                            foreach (XElement element in xEntitySets)
                            {
                                XAttribute xa = element.Attributes().FirstOrDefault(x => x.Name == "EntityType");
                                xa.Value = xa.Value.Insert(xa.Value.LastIndexOf(".") + 1, extraName);
                            }
                        }

                        IEnumerable<XElement> xEntityTypes = GetXElementByLocalName(xConceptualModels, "EntityType");
                        if (xEntityTypes != null)
                        {
                            foreach (XElement element in xEntityTypes)
                            {
                                XAttribute xa = element.Attributes().FirstOrDefault(x => x.Name == "Name");
                                xa.Value = xa.Value.Insert(xa.Value.LastIndexOf(".") + 1, extraName);
                            }
                        }

                        IEnumerable<XElement> xAssociations = GetXElementByLocalName(xConceptualModels, "Association");
                        IEnumerable<XElement> xEnds = GetXElementByLocalName(xAssociations, "End");
                        if (xEnds != null)
                        {
                            foreach (XElement element in xEnds)
                            {
                                XAttribute xa = element.Attributes().FirstOrDefault(x => x.Name == "Type");
                                xa.Value = xa.Value.Insert(xa.Value.LastIndexOf(".") + 1, extraName);
                            }
                        }

                        IEnumerable<XElement> xEntityTypeMappings = GetXElementByLocalName(xml.Elements(), "EntityTypeMapping");
                        if (xEntityTypeMappings != null)
                        {
                            foreach (XElement element in xEntityTypeMappings)
                            {
                                XAttribute xa = element.Attributes().FirstOrDefault(x => x.Name == "TypeName");
                                xa.Value = xa.Value.Insert(xa.Value.LastIndexOf(".") + 1, extraName);
                            }
                        }

                        IEnumerable<XElement> xEntityTypeShapes = GetXElementByLocalName(xml.Elements(), "EntityTypeShape");
                        if (xEntityTypeShapes != null)
                        {
                            foreach (XElement element in xEntityTypeShapes)
                            {
                                XAttribute xa = element.Attributes().FirstOrDefault(x => x.Name == "EntityType");
                                xa.Value = xa.Value.Insert(xa.Value.LastIndexOf(".") + 1, extraName);
                            }
                        }
                    }
                    xml.Save(fileName);
                }
            }
            return true;
        }

        private IEnumerable<XElement> GetXElementByLocalName(IEnumerable<XElement> xElements, String eName)
        {
            if (xElements != null)
            {
                IEnumerable<XElement> returnValues = xElements.Where(x => x.Name.LocalName == eName);
                if (returnValues.Count() > 0)
                    return returnValues;
                else if (xElements.Elements().Count() > 0)
                    return GetXElementByLocalName(xElements.Elements(), eName);
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Equals(tpTables))
            {
                DisplayPage(tpOutputSetting);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //foreach (ProjectItem pi in this.GlobalProject.ProjectItems)
            //{
            //    if (pi.Name.EndsWith("edmx"))
            //    {
            //        Window FDesignWindow = pi.Open("{7651A703-06E5-11D1-8EBD-00A0C90F26EA}");
            //        FDesignWindow.Activate();
            //        HTMLWindow W = (HTMLWindow)FDesignWindow.Object;
            //    }
            //}

            TreeNode Node = tvTables.SelectedNode;
            string TableName = "";
            if (Node != null)
                TableName = Node.Text;

            MWizard.fmSelWCFTableField F = new fmSelWCFTableField();
            if (F.ShowSelTableFieldForm(System.IO.Path.GetDirectoryName(GlobalProject.FullName),
                                                                    GlobalProject.Name, ref TableName))
            {
                Node = tvTables.Nodes.Add(TableName);
                Node.Name = TableName;
                AddDatasetNode(Node);
                tvTables.SelectedNode = Node;
                GetFieldNames(TableName, lvSelectedFields);
            }
        }

        private void GetFieldNames(string EntityName, ListView SrcListView)
        {
            ListViewItem lvi;
            SrcListView.Items.Clear();
            SrcListView.BeginUpdate();

            String FProjectPath = System.IO.Path.GetDirectoryName(GlobalProject.FullName);
            String FProjectName = GlobalProject.Name;
            TreeNode Node = tvTables.SelectedNode;
            TWCFDatasetItem FDatasetItem = (TWCFDatasetItem)Node.Tag;

            EFServerTools.Design.MetadataProvider aMetadataProvider = new EFServerTools.Design.MetadataProvider(FProjectPath, FProjectName);
            String strEntityTypeName = aMetadataProvider.GetEntitySetType(FDatasetItem.ContainerName, FDatasetItem.TableName);
            List<string> lPropertyNames = aMetadataProvider.GetPropertyNames(FDatasetItem.ContainerName, strEntityTypeName);
            for (int i = 0; i < lPropertyNames.Count; i++)
            {
                lvi = SrcListView.Items.Add(lPropertyNames[i]);
                lvi.SubItems.Add(lvi.Text);

                TFieldAttrItem Item = new TFieldAttrItem();
                FDatasetItem.FieldAttrItems.Add(Item);
                Item.DataField = lvi.Text;
                if (lvi.SubItems.Count > 1)
                    Item.Description = lvi.SubItems[1].Text;
                lvi.Tag = Item;
            }

            if (SrcListView.Items.Count > 0)
                SrcListView.Items[0].Selected = true;

            SrcListView.EndUpdate();
            SrcListView.Sort();

            btnDeleteField.Enabled = SrcListView.Items.Count > 0;
        }

        private void AddDatasetNode(TreeNode Node)
        {
            TWCFDatasetItem Item = new TWCFDatasetItem();
            Item.GenerateEntity = cbGenerateEntity.Text;
            FServerData.Datasets.Add(Item);
            Item.DatabaseName = FServerData.DatabaseName;
            Item.TableName = Node.Text;
            EFServerTools.Design.MetadataProvider aMetadataProvider = new EFServerTools.Design.MetadataProvider(System.IO.Path.GetDirectoryName(GlobalProject.FullName), GlobalProject.Name);
            List<String> lsContainerNames = aMetadataProvider.GetEntityContainerNames();
            if (lsContainerNames.Count > 0)
            {
                Item.ContainerName = lsContainerNames[0];
            }
            Node.Tag = Item;
            if (Node.Parent != null)
            {
                TWCFDatasetItem ParentItem = (TWCFDatasetItem)Node.Parent.Tag;
                ParentItem.ChildItem = Item;
                Item.ParentItem = ParentItem;
            }
        }

        private void GetFieldList(string DatabaseName, string TableName, TStringList FieldNameList)
        {
            String Owner = "";
            if (TableName.IndexOf('.') > -1)
            {
                Owner = WzdUtils.GetToken(ref TableName, new char[] { '.' });
            }

            string[] S = new string[4];
            S[1] = Owner;
            S[2] = TableName;
            String SortName = "ORDINAL_POSITION";
            if (FServerData.DatabaseType == ClientType.ctOracle)
            {
                String UserID = WzdUtils.GetFieldParam(FServerData.ConnectionString, "User ID");
                S = new String[] { UserID.ToUpper(), TableName };
                SortName = "ID";
            }
            DataTable D = FServerData.Owner.GlobalConnection.GetSchema("Columns", S);
            DataRow[] DRs = D.Select("", SortName + " ASC");

            foreach (DataRow DR in DRs)
                FieldNameList.Add(DR["COLUMN_NAME"].ToString());
        }

        //private void GetCaptionFromCOLDEF(string DatabaseName, string TableName, TStringList FieldCaptionList)
        //{
        //    InfoCommand aInfoCommand = new InfoCommand(FServerData.DatabaseType);
        //    aInfoCommand.Connection = InternalConnection;
        //    TableName = WzdUtils.RemoveQuote(TableName, FServerData.DatabaseType);
        //    aInfoCommand.CommandText = "Select FIELD_NAME,CAPTION from COLDEF where TABLE_NAME = '" + TableName + "'";
        //    IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
        //    DataSet D = new DataSet();
        //    WzdUtils.FillDataAdapter(FServerData.DatabaseType, DA, D, "COLDEF");
        //    FieldCaptionList.Clear();
        //    int I;
        //    DataRow DR;
        //    for (I = 0; I < D.Tables[0].Rows.Count; I++)
        //    {
        //        DR = D.Tables[0].Rows[I];
        //        if (DR["FIELD_NAME"].ToString() != "")
        //            FieldCaptionList.Add(DR["FIELD_NAME"] + "=" + DR["CAPTION"]);
        //    }
        //}

        private void btnAddField_Click(object sender, EventArgs e)
        {
            TreeNode Node = tvTables.SelectedNode;
            TWCFDatasetItem DatasetItem = (TWCFDatasetItem)Node.Tag;
            if (Node != null)
            {
                MWizard.fmSelWCFTableField F = new fmSelWCFTableField();
                if (F.ShowSelTableFieldForm(System.IO.Path.GetDirectoryName(GlobalProject.FullName),
                                            GlobalProject.Name, Node.Text, lvSelectedFields, DatasetItem))
                {
                    btnDeleteField.Enabled = lvSelectedFields.Items.Count > 0;
                }
            }
        }

        private void btnAddNext_Click(object sender, EventArgs e)
        {
            TreeNode node1 = tvTables.SelectedNode;
            if (node1 != null)
            {
                String TableName = ""; 
                MWizard.fmSelWCFTableField F = new fmSelWCFTableField();
                if (F.ShowSelTableFieldForm(System.IO.Path.GetDirectoryName(GlobalProject.FullName),
                                                                        GlobalProject.Name, ref TableName))
                {
                    TreeNode node2 = new TreeNode();
                    node2.Text = TableName;
                    node2.Name = TableName;
                    node1.Nodes.Add(node2);
                    AddDatasetNode(node2);
                    tvTables.SelectedNode = node2;
                    GetFieldNames(TableName, lvSelectedFields);
                }
            }

        }

        private void UpdatelvSelectedFields(TWCFDatasetItem DatasetItem)
        {
            //cbIsRelationKey.Checked = false;
            lvSelectedFields.Items.Clear();
            if (DatasetItem != null)
            {
                lvSelectedFields.BeginUpdate();
                for (int num1 = 0; num1 < DatasetItem.FieldAttrItems.Count; num1++)
                {
                    TFieldAttrItem item1 = DatasetItem.FieldAttrItems[num1] as TFieldAttrItem;
                    ListViewItem item2 = lvSelectedFields.Items.Add(item1.DataField);
                    item2.SubItems.Add(item1.Description);
                    item2.Tag = item1;
                }
                lvSelectedFields.EndUpdate();
                btnDeleteField.Enabled = lvSelectedFields.Items.Count > 0;
                cbGenerateEntity.Text = DatasetItem.GenerateEntity;
            }

        }

        private void tvTables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode Node = tvTables.SelectedNode;
            btnNewSubDataset.Enabled = Node != null;
            btnDeleteDataset.Enabled = btnNewSubDataset.Enabled;
            btnNewField.Enabled = btnNewSubDataset.Enabled;
            TWCFDatasetItem aItem = (TWCFDatasetItem)Node.Tag;
            UpdatelvSelectedFields(aItem);
            if (aItem.ParentItem != null)
                cbGenerateEntity.Enabled = false;
            else
                cbGenerateEntity.Enabled = true;
        }

        private void lvSelectedFields_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //cbIsRelationKey.Enabled = lvSelectedFields.SelectedItems.Count > 0;
            if (lvSelectedFields.SelectedItems.Count == 1)
            {
                ListViewItem Item = lvSelectedFields.SelectedItems[0];
                TFieldAttrItem FieldItem = (TFieldAttrItem)Item.Tag;
                //cbIsRelationKey.Checked = FieldItem.IsRelationKey;
            }
            else
            {
                //cbIsRelationKey.Checked = false;
            }
        }

        private void SetFieldAttrOption(string OptionName)
        {
            int I;
            ListViewItem Item;
            TFieldAttrItem FieldItem;
            for (I = 0; I < lvSelectedFields.Items.Count; I++)
            {
                if (lvSelectedFields.Items[I].Selected)
                {
                    Item = lvSelectedFields.Items[I];
                    FieldItem = (TFieldAttrItem)Item.Tag;

                    //if (string.Compare(OptionName, "IsRelationKey") == 0)
                    //	   FieldItem.IsRelationKey = cbIsRelationKey.Checked;
                }
            }
        }

        private void cbIsKey_Click(object sender, EventArgs e)
        {
            SetFieldAttrOption("IsKey");
        }

        private void cbCheckNull_Click(object sender, EventArgs e)
        {
            SetFieldAttrOption("CheckNull");
        }

        private void cbIsRelationKey_Click(object sender, EventArgs e)
        {
            SetFieldAttrOption("IsRelationKey");
        }

        private void btnDeleteField_Click(object sender, EventArgs e)
        {
            int I;
            ListViewItem Item;
            TFieldAttrItem FieldItem;
            Boolean HaveDelete = false;
            for (I = lvSelectedFields.Items.Count - 1; I >= 0; I--)
            {
                if (lvSelectedFields.Items[I].Selected)
                {
                    Item = lvSelectedFields.Items[I];
                    FieldItem = (TFieldAttrItem)Item.Tag;
                    FieldItem.Collection.Remove(FieldItem);
                    lvSelectedFields.Items.Remove(Item);
                    HaveDelete = true;
                }
            }

            if (HaveDelete)
            {
                TreeNode Node = tvTables.SelectedNode;
                ((TWCFDatasetItem)Node.Tag).AddAll = false;
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                EFServerTools.Design.MetadataProvider aMetadataProvider = new EFServerTools.Design.MetadataProvider(System.IO.Path.GetDirectoryName(GlobalProject.FullName), GlobalProject.Name);
                for (int i = 0; i < FServerData.Datasets.Count; i++)
                {
                    TWCFDatasetItem DatasetItem = FServerData.Datasets[i] as TWCFDatasetItem;
                    String strEntityTypeName = aMetadataProvider.GetEntitySetType(DatasetItem.ContainerName, DatasetItem.TableName);
                    List<string> lPropertyNames = aMetadataProvider.GetPropertyNames(DatasetItem.ContainerName, strEntityTypeName);
                    for (int l = 0; l < lPropertyNames.Count; l++)
                    {
                        bool flag = false;
                        for (int j = 0; j < DatasetItem.FieldAttrItems.Count; j++)
                        {
                            if ((DatasetItem.FieldAttrItems[j] as TFieldAttrItem).DataField == lPropertyNames[l])
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                            aMetadataProvider.RemoveProperty(DatasetItem.ContainerName, strEntityTypeName, lPropertyNames[l]);
                    }

                }
                aMetadataProvider.Save();

                String strOldName = String.Empty;
                foreach (ProjectItem pi in GlobalProject.ProjectItems)
                {
                    if (pi.Name.EndsWith("edmx"))
                    {
                        strOldName = pi.Name;
                        File.Copy(FServerData.CodeOutputPath + "\\" + FServerData.PackageName + "\\" + strOldName,
                            FServerData.CodeOutputPath + "\\" + FServerData.PackageName + "\\" + GlobalProject.Name + "_infolightTemp.edmx");
                        pi.Delete();
                        GlobalProject.Save();
                        GlobalProject.ProjectItems.AddFromTemplate(FServerData.CodeOutputPath + "\\" + FServerData.PackageName + "\\" + GlobalProject.Name + "_infolightTemp.edmx",
                            FServerData.CodeOutputPath + "\\" + FServerData.PackageName + "\\" + strOldName);
                        GlobalProject.Save();
                        File.Delete(FServerData.CodeOutputPath + "\\" + FServerData.PackageName + "\\" + GlobalProject.Name + "_infolightTemp.edmx");
                        break;
                    }
                }

                if (FServerData.AssemblyOutputPath != null && FServerData.AssemblyOutputPath != "")
                    GlobalProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value = FServerData.AssemblyOutputPath;

                //foreach (ProjectItem pi in GlobalProject.ProjectItems)
                //{
                //    if (pi.Name.EndsWith("edmx"))
                //    {
                //        pi.Open("{00000000-0000-0000-0000-000000000000}");
                //        break;
                //    }
                //}

                Hide();
                FDTE2.MainWindow.Activate();
                TWCFServerGenerator SG = new TWCFServerGenerator(FServerData, FDTE2, FAddIn, GlobalProject);
                SG.GenServerModule();
            }
            catch (Exception ex)
            {
                WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                return;
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void btnNewSolution_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbNewLocation.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void tbNewSolutionName_TextChanged(object sender, EventArgs e)
        {
            tbAssemblyOutputPath.Text = String.Format(@"..\..\..WCFServer\{0}", tbNewSolutionName.Text);
        }



        private void rbAddToCurrent_Click(object sender, EventArgs e)
        {
            EnabledOutputControls();
            SetCodeOutputPath();
        }

        private static string GetServerPath()
        {
            if ((fmWCFServerWzd._serverPath == null) || (fmWCFServerWzd._serverPath.Length == 0))
            {
                fmWCFServerWzd._serverPath = EEPRegistry.Server + "\\";
            }
            return fmWCFServerWzd._serverPath;
        }

        private void fmServerWzd_Load(object sender, EventArgs e)
        {
            //???LoadDBString();
        }

        private void SetCodeOutputPath()
        {
            // ..\..\EEPNetServer\Solution1
            if (rbAddToCurrent.Checked)
            {
                string S = tbCurrentSolution.Text;
                if (S != "")
                {
                    S = System.IO.Path.GetDirectoryName(S);
                    String SolutionName = Path.GetFileNameWithoutExtension(tbCurrentSolution.Text);
                    tbOutputPath.Text = S + @"\" + SolutionName + "\\Server";
                    tbAssemblyOutputPath.Text = String.Format(@"..\..\..\EEPNetServer\{0}", SolutionName);
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
                    tbAssemblyOutputPath.Text = String.Format(@"..\..\EEPNetServer\{0}", SolutionName);
                }
            }
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

        private void button1_Click_2(object sender, EventArgs e)
        {
            //Object[] Params = new Object[] { "Solution1", "S002.GROUPS_1" };
            //FEEPWizard.GetTableRelationByProvider(Params);

            Type aInterface = FEEPWizard.GetType().GetInterface("IEEPWizard");
            if (aInterface != null)
            {
                Object Params = new Object[] { 
                   @"C:\Program Files\InfoLight\EEP2006\EEPNetClient\Solution1\C001.dll", 
                    "Form1", 
                    "001", 
                    "", 
                    "ERPS", 
                    "", 
                    "Solution1",
                    @"C:\Program Files\InfoLight\EEP2006\Solution1.sln"};
                FEEPWizard.GetFormImage(Params);
                //IEEPWizard A = (IEEPWizard)aInterface;
                //A.CallMethod("GetFormImage", null);
            }
        }

        private void btnAssemblyOutputPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbAssemblyOutputPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            //Project.SetasStartUpProject

            //MessageBox.Show("1");
            FDTE2.Solution.Open(@"C:\Program Files\InfoLight\EEP2006\Solution1.sln");
            //MessageBox.Show("2");

            FDTE2.Windows.Item(Constants.vsWindowKindSolutionExplorer).Activate();
            //MessageBox.Show("3");
            UIHierarchy A = (UIHierarchy)FDTE2.ActiveWindow.Object;

            foreach (UIHierarchyItem aItem in A.UIHierarchyItems)
            {
                foreach (UIHierarchyItem B in aItem.UIHierarchyItems)
                {
                    if (B.Name.CompareTo("C:\\...\\EEPWebClient\\") == 0)
                    {
                        foreach (UIHierarchyItem C in B.UIHierarchyItems)
                        {
                            if (C.Name.CompareTo("InfoLogin.aspx") == 0)
                            {
                                C.Select(vsUISelectionType.vsUISelectionTypeSelect);
                                try
                                {
                                    FDTE2.MainWindow.Activate();
                                    FDTE2.ActiveWindow.Activate();
                                    MessageBox.Show("1");
                                    C.DTE.ExecuteCommand("File.ViewinBrowser", String.Empty);
                                    MessageBox.Show("2");
                                    //MessageBox.Show("OK");
                                }
                                catch (Exception E)
                                {
                                    MessageBox.Show(E.Message);
                                }
                            }
                        }
                    }
                }
            }

            //return;
            //MessageBox.Show("4");
            try
            {
                String S = "Solution1\\C:\\...\\EEPWebClient\\";
                A.GetItem(S);
                MessageBox.Show("OK");
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            //MessageBox.Show("4.5");
            A.GetItem(@"EEPWebClient").Select(vsUISelectionType.vsUISelectionTypeSelect);
            //MessageBox.Show("5");
            FDTE2.ExecuteCommand("Project.SetasStartUpProject", null);
            //MessageBox.Show("6");
            A.GetItem(@"Solution1\C:\...\EEPWebClient\").UIHierarchyItems.Expanded = true;
            //MessageBox.Show("7");
            A.GetItem(@"Solution1\C:\...\EEPWebClient\\InfoLogin.aspx").Select(vsUISelectionType.vsUISelectionTypeSelect);
            //MessageBox.Show("8");
            FDTE2.ExecuteCommand("Project.SetAsStartPage", null);
            //MessageBox.Show("9");

            A.GetItem(@"Solution1\C:\...\EEPWebClient\\InfoLogin.aspx").Select(vsUISelectionType.vsUISelectionTypeSelect);
            //MessageBox.Show("10");
            FDTE2.ExecuteCommand("File.ViewinBrowser", null);
            //MessageBox.Show("11");

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            /*
            //OpenPage
            Type bInterface = FEEPWizard.GetType().GetInterface("IEEPWizard");
            if (bInterface != null)
            {
                Object Params = new Object[] { 
                    @"C:\Program Files\InfoLight\EEP2006\Solution1.sln", 
                    @"C:\...\EEPWebClient\",
                    "AP",
                    "Form1.aspx" };
                FEEPWizard.OpenPage(Params);
            }
             */

            /*
            //GetTableRelation
            Type bInterface = FEEPWizard.GetType().GetInterface("IEEPWizard");
            if (bInterface != null)
            {
                Object Params = new Object[] { "Solution1", "S002.GROUPS" };
                FEEPWizard.GetTableRelationByProvider(Params);
                //IEEPWizard A = (IEEPWizard)aInterface;
                //A.CallMethod("GetFormImage", null);
            }
             */

            //GenClientModule
            Type bInterface = FEEPWizard.GetType().GetInterface("IEEPWizard");
            if (bInterface != null)
            {
                System.IO.FileStream file = new System.IO.FileStream("d:\\a.xml", System.IO.FileMode.Open);
                try
                {
                    XmlDocument X = new XmlDocument();
                    X.Load(file);
                    String S = X.OuterXml;
                    Object Params = new Object[] { S }; //{ "<?xml version=\"1.0\" encoding=\"UTF-8\"?><ClientData PackageName=\"c9\" BaseFormName=\"CMasterDetail\" ServerPackageName=\"S002.GROUPS\" OutputPath=\"C:\\Program Files\\InfoLight\\EEP2006\\\" CodeOutputPath=\"C:\\Program Files\\InfoLight\\EEP2006\\Solution1\" TableName=\"GROUPS\" FormName=\"fmc9\" ProviderName=\"S002.GROUPS\" DatabaseName=\"ERPS\" SolutionName=\"C:\\Program Files\\InfoLight\\EEP2006\\Solution1.sln\" NewSolution=\"0\" ColumnCount=\"1\" ViewProviderName=\"S002.View_Provider\"><Blocks><Block0 ProviderName=\"GROUPS\" TableName=\"GROUPS\" Name=\"View\" ParentItemName=\"\"><BlockFieldItems><BlockFieldItem0 DataField=\"GROUPID\" Description=\"\"/><BlockFieldItem1 DataField=\"GROUPNAME\" Description=\"\"/><BlockFieldItem2 DataField=\"DESCRIPTION\" Description=\"\"/></BlockFieldItems></Block0><Block1 ProviderName=\"GROUPS\" TableName=\"GROUPS\" Name=\"Master\" ParentItemName=\"\"><BlockFieldItems><BlockFieldItem0 DataField=\"GROUPID\" Description=\"\"/><BlockFieldItem1 DataField=\"GROUPNAME\" Description=\"\"/><BlockFieldItem2 DataField=\"DESCRIPTION\" Description=\"\"/></BlockFieldItems></Block1><Block2 ProviderName=\"GROUPMENUS\" TableName=\"GROUPMENUS\" Name=\"GROUPMENUS\" ParentItemName=\"Master\"><BlockFieldItems><BlockFieldItem0 DataField=\"GROUPID\" Description=\"\"/><BlockFieldItem1 DataField=\"MENUID\" Description=\"\"/></BlockFieldItems></Block2></Blocks></ClientData>" };
                    FEEPWizard.GenClientModule(Params);
                }
                finally
                {
                    file.Dispose();
                }
            }

            //GenServerModule
            //Type bInterface = FEEPWizard.GetType().GetInterface("IEEPWizard");
            //if (bInterface != null)
            //{
            //    //System.Xml.Serialization.XmlSerializer xs;
            //    //xs = new System.Xml.Serialization.XmlSerializer(typeof(ClassTestData));
            //    System.IO.FileStream file = new System.IO.FileStream("d:\\a.xml", System.IO.FileMode.Open);
            //    try
            //    {
            //        XmlDocument X = new XmlDocument();
            //        X.Load(file);
            //        String S = X.OuterXml;
            //        Object Params = new Object[] { S }; //{ "<?xml version=\"1.0\" encoding=\"UTF-8\"?><ClientData PackageName=\"c9\" BaseFormName=\"CMasterDetail\" ServerPackageName=\"S002.GROUPS\" OutputPath=\"C:\\Program Files\\InfoLight\\EEP2006\\\" CodeOutputPath=\"C:\\Program Files\\InfoLight\\EEP2006\\Solution1\" TableName=\"GROUPS\" FormName=\"fmc9\" ProviderName=\"S002.GROUPS\" DatabaseName=\"ERPS\" SolutionName=\"C:\\Program Files\\InfoLight\\EEP2006\\Solution1.sln\" NewSolution=\"0\" ColumnCount=\"1\" ViewProviderName=\"S002.View_Provider\"><Blocks><Block0 ProviderName=\"GROUPS\" TableName=\"GROUPS\" Name=\"View\" ParentItemName=\"\"><BlockFieldItems><BlockFieldItem0 DataField=\"GROUPID\" Description=\"\"/><BlockFieldItem1 DataField=\"GROUPNAME\" Description=\"\"/><BlockFieldItem2 DataField=\"DESCRIPTION\" Description=\"\"/></BlockFieldItems></Block0><Block1 ProviderName=\"GROUPS\" TableName=\"GROUPS\" Name=\"Master\" ParentItemName=\"\"><BlockFieldItems><BlockFieldItem0 DataField=\"GROUPID\" Description=\"\"/><BlockFieldItem1 DataField=\"GROUPNAME\" Description=\"\"/><BlockFieldItem2 DataField=\"DESCRIPTION\" Description=\"\"/></BlockFieldItems></Block1><Block2 ProviderName=\"GROUPMENUS\" TableName=\"GROUPMENUS\" Name=\"GROUPMENUS\" ParentItemName=\"Master\"><BlockFieldItems><BlockFieldItem0 DataField=\"GROUPID\" Description=\"\"/><BlockFieldItem1 DataField=\"MENUID\" Description=\"\"/></BlockFieldItems></Block2></Blocks></ClientData>" };
            //        FEEPWizard.GenServerModule(Params);
            //    }
            //    finally
            //    {
            //        file.Dispose();
            //    }
            //}

            /*
            //GetPageInfo
            Type aInterface = FEEPWizard.GetType().GetInterface("IEEPWizard");
            if (aInterface != null)
            {
                Object Params = new Object[] { 
                   @"C:\Program Files\InfoLight\EEP2006\Solution1.sln",
                   @"C:\...\EEPWebClient\", 
                   @"C:\Program Files\InfoLight\EEP2006\EEPWebClient\",
                    new Object[] { "Ap" },
                    "Form1.aspx",
                    "001",
                    "",
                    "ERPS",
                    "Solution1",
                    "0"
                };
                FEEPWizard.GetPageInfo(Params);
                //IEEPWizard A = (IEEPWizard)aInterface;
                //A.CallMethod("GetFormImage", null);
            }
             */


            /*
            String SolutionFileName = RealParams[0].ToString();
            String WebSiteName = RealParams[1].ToString();
            String WebSitePath = RealParams[2].ToString();
            Object[] FolderOffset = (Object[])RealParams[3];
            String PageName = RealParams[4].ToString();
            String UserID = RealParams[5].ToString();
            String Password = RealParams[6].ToString();
            String DBName = RealParams[7].ToString();
            String Solutionname = RealParams[8].ToString();
            String PrintWaitingTime = RealParams[9].ToString();
            */

            /*
            //GenServerModule
            Type bInterface = FEEPWizard.GetType().GetInterface("IEEPWizard");
            if (bInterface != null)
            {
                Object Params = new Object[] { "<?xml version=\"1.0\" encoding=\"UTF-8\"?><ServerData DatabaseName=\"ERPS\" PackageName=\"s1\" SolutionName=\"C:\\Program Files\\InfoLight\\EEP2006\\Solution1.sln\" OutputPath=\"C:\\Program Files\\InfoLight\\EEP2006\\\" CodeOutputPath=\"C:\\Program Files\\InfoLight\\EEP2006\\\" NewSolution=\"0\"><Datasets><Dataset0 DatabaseName=\"ERPS\" TableName=\"Customers\" Name=\"Customers\" RelFields=\"\" KeyFields=\"CustomerID\" ParentItem=\"\"><FieldAttrItems><FieldAttrItem0 DataField=\"CustomerID\" Description=\"め絪腹\" IsKey=\"1\" CheckNull=\"0\" IsRelationKey=\"0\" ParentRelationField=\"\"/><FieldAttrItem1 DataField=\"CompanyName\" Description=\"CompanyName\" IsKey=\"0\" CheckNull=\"0\" IsRelationKey=\"0\" ParentRelationField=\"\"/><FieldAttrItem2 DataField=\"ContactName\" Description=\"ContactName\" IsKey=\"0\" CheckNull=\"0\" IsRelationKey=\"0\" ParentRelationField=\"\"/></FieldAttrItems></Dataset0></Datasets></ServerData>" };
                FEEPWizard.GenServerModule(Params);
                //IEEPWizard A = (IEEPWizard)aInterface;
                //A.CallMethod("GetFormImage", null);
            }
             */

            //GenClientModule
            bInterface = FEEPWizard.GetType().GetInterface("IEEPWizard");
            if (bInterface != null)
            {
                System.IO.FileStream file = new System.IO.FileStream("d:\\a.xml", System.IO.FileMode.Open);
                try
                {
                    XmlDocument X = new XmlDocument();
                    X.Load(file);
                    String S = X.OuterXml;
                    Object Params = new Object[] { S }; //{ "<?xml version=\"1.0\" encoding=\"UTF-8\"?><ClientData PackageName=\"c9\" BaseFormName=\"CMasterDetail\" ServerPackageName=\"S002.GROUPS\" OutputPath=\"C:\\Program Files\\InfoLight\\EEP2006\\\" CodeOutputPath=\"C:\\Program Files\\InfoLight\\EEP2006\\Solution1\" TableName=\"GROUPS\" FormName=\"fmc9\" ProviderName=\"S002.GROUPS\" DatabaseName=\"ERPS\" SolutionName=\"C:\\Program Files\\InfoLight\\EEP2006\\Solution1.sln\" NewSolution=\"0\" ColumnCount=\"1\" ViewProviderName=\"S002.View_Provider\"><Blocks><Block0 ProviderName=\"GROUPS\" TableName=\"GROUPS\" Name=\"View\" ParentItemName=\"\"><BlockFieldItems><BlockFieldItem0 DataField=\"GROUPID\" Description=\"\"/><BlockFieldItem1 DataField=\"GROUPNAME\" Description=\"\"/><BlockFieldItem2 DataField=\"DESCRIPTION\" Description=\"\"/></BlockFieldItems></Block0><Block1 ProviderName=\"GROUPS\" TableName=\"GROUPS\" Name=\"Master\" ParentItemName=\"\"><BlockFieldItems><BlockFieldItem0 DataField=\"GROUPID\" Description=\"\"/><BlockFieldItem1 DataField=\"GROUPNAME\" Description=\"\"/><BlockFieldItem2 DataField=\"DESCRIPTION\" Description=\"\"/></BlockFieldItems></Block1><Block2 ProviderName=\"GROUPMENUS\" TableName=\"GROUPMENUS\" Name=\"GROUPMENUS\" ParentItemName=\"Master\"><BlockFieldItems><BlockFieldItem0 DataField=\"GROUPID\" Description=\"\"/><BlockFieldItem1 DataField=\"MENUID\" Description=\"\"/></BlockFieldItems></Block2></Blocks></ClientData>" };
                    FEEPWizard.GenWebForm(Params);
                }
                finally
                {
                    file.Dispose();
                }
            }
        }

        private void RemoveChildItem(TreeNode aNode)
        {
            while (aNode.Nodes.Count > 0)
            {
                RemoveChildItem(aNode.Nodes[0]);
            }

            if (aNode.Tag != null)
            {
                TWCFDatasetItem bItem = (TWCFDatasetItem)aNode.Tag;
                FServerData.Datasets.Remove(bItem);
            }
            aNode.Nodes.Remove(aNode);
        }

        private void btnDeleteDataset_Click(object sender, EventArgs e)
        {
            if (tvTables.SelectedNode != null)
            {
                RemoveChildItem(tvTables.SelectedNode);
                //if (tvTables.SelectedNode.Tag != null)
                //{
                //    TDatasetItem aItem = (TDatasetItem)tvTables.SelectedNode.Tag;
                //    aItem.ChildItem.Clear();
                //    FServerData.Datasets.Remove(aItem);
                //}
                //tvTables.Nodes.Remove(tvTables.SelectedNode);
            }
        }

        private void btnRelation_Click(object sender, EventArgs e)
        {
            TreeNode Node = tvTables.SelectedNode;
            if (Node == null)
                return;
            TDatasetItem DetailItem = (TDatasetItem)Node.Tag;
            fmSelKeyField aForm = new fmSelKeyField();
            aForm.ShowSelKeyField(DetailItem.ParentItem, DetailItem);
        }

        private void lvSelectedFields_ColumnClick(object sender, ColumnClickEventArgs e)
        {
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

        private void tvTables_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (tvTables.SelectedNode != null)
            {
                TreeNode tn = tvTables.SelectedNode;
                TWCFDatasetItem aTWCFDatasetItem = tn.Tag as TWCFDatasetItem;
                aTWCFDatasetItem.GenerateEntity = cbGenerateEntity.Text;
            }
        }

        private void cbGenerateEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tvTables.SelectedNode != null)
            {
                TreeNode tn = tvTables.SelectedNode;
                TWCFDatasetItem aTWCFDatasetItem = tn.Tag as TWCFDatasetItem;
                aTWCFDatasetItem.GenerateEntity = cbGenerateEntity.Text;
            }
        }
    }

    public class TWCFServerGenerator : Object
    {
        private TWCFServerData FServerData;
        private DTE2 FDTE2;
        private Component FDataModule;
        private System.ComponentModel.Design.IDesignerHost FDesignerHost;
        private AddIn FAddIn;
        private string FTemplateName;
        private Project GlobalProject;
        private ProjectItem GlobalPI;
        private Window GlobalWindow;

        public TWCFServerGenerator(TWCFServerData ServerData, DTE2 aDTE, AddIn aAddIn, Project P)
        {
            FServerData = ServerData;
            FDTE2 = aDTE;
            FAddIn = aAddIn;
            GlobalProject = P;
        }

        public void GenServerModule()
        {
            if (GlobalProject != null)
            {
                GetDataModule();
                DesignerTransaction T = FDesignerHost.CreateTransaction();
                try
                {
                    try
                    {
                        GenDatasets();
                        GenViewDataSet();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                }
                finally
                {
                    T.Commit();
                }
                //GlobalPI.Save(GlobalPI.get_FileNames(0));
                GlobalWindow.Close(vsSaveChanges.vsSaveChangesYes);
                Window W = GlobalPI.Open("{00000000-0000-0000-0000-000000000000}");
                W.Activate();
                GlobalProject.Save(GlobalProject.FullName);
                FDTE2.Solution.SolutionBuild.BuildProject(FDTE2.Solution.SolutionBuild.ActiveConfiguration.Name,
                    GlobalProject.FullName, true);
            }
        }

        private bool GenSolution()
        {
            Solution sln = /*(Solution2)*/FDTE2.Solution;

            if (FServerData.NewSolution)
            {
                if (System.IO.Directory.Exists(FServerData.OutputPath))
                {
                    if (FServerData.OutputPath == "\\")
                        throw new Exception("Unknown Output Path: " + "\\");
                    System.IO.Directory.Delete(FServerData.OutputPath, true);
                }
                sln.Create(FServerData.OutputPath, FServerData.SolutionName);
                ProjectLoader.AddDefaultProject(FDTE2);
                Project P = sln.AddFromTemplate(FTemplateName,
                    FServerData.CodeOutputPath + "\\" + FServerData.PackageName, FServerData.PackageName, false);
                P.Name = FServerData.PackageName;
                string FileName = FServerData.OutputPath + "\\" + FServerData.SolutionName + ".sln";
                sln.SaveAs(FileName);
                //sln.Open(FileName);
                sln.SolutionBuild.StartupProjects = P;
                sln.SolutionBuild.BuildProject(sln.SolutionBuild.ActiveConfiguration.Name, P.FullName, true);
                GlobalProject = P;
                ////sln.SolutionBuild.Clean(true);
            }
            else
            {
                string FilePath = FServerData.CodeOutputPath + "\\" + FServerData.PackageName;
                //string FilePath = Path.GetDirectoryName(FServerData.SolutionName) + "\\" + FServerData.PackageName;
                if (System.IO.Directory.Exists(FilePath))
                {
                    if (FilePath == "\\")
                        throw new Exception("Unknown Output Path: " + "\\");

                    DialogResult dr = MessageBox.Show("There is another File which name is " + FServerData.PackageName + " existed! Do you want to delete it first", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        System.IO.Directory.Delete(FilePath, true);
                    }
                    else
                    {
                        return false;
                    }

                }
                Project P = sln.AddFromTemplate(FTemplateName, FilePath, FServerData.PackageName, true);
                P.Name = FServerData.PackageName;
                string FileName = FilePath + "\\" + FServerData.PackageName + "." + FServerData.Language + "proj";
                P.Save(FileName);
                sln.Open(FServerData.SolutionName);
                int I;
                P = null;
                for (I = 1; I <= sln.Projects.Count; I++)
                {
                    P = sln.Projects.Item(I);
                    if (string.Compare(P.Name, FServerData.PackageName, true) == 0) //大小写重命问题
                        break;
                    else
                        P = null;
                }
                if (P != null)
                    sln.Remove(P);
                P = sln.AddFromFile(FilePath + "\\" + FServerData.PackageName + "." + FServerData.Language + "proj", false);
                P.Properties.Item("RootNamespace").Value = FServerData.PackageName;
                P.Properties.Item("AssemblyName").Value = FServerData.PackageName;
                sln.SaveAs(FServerData.SolutionName);
                sln.SolutionBuild.StartupProjects = P;
                sln.SolutionBuild.BuildProject(sln.SolutionBuild.ActiveConfiguration.Name, P.FullName, true);
                GlobalProject = P;
                //sln.SolutionBuild.Clean(true);

                try
                {
                    EnvDTE.DTE dte = System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.11.0") as DTE;
                    EnvDTE80.DTE2 dte2 = dte as EnvDTE80.DTE2;// Activator.CreateInstance(Type.GetTypeFromProgID("VisualStudio.DTE.10.0")) as EnvDTE80.DTE2;

                    string vsWizardAddItem = "{0F90E1D1-4999-11D1-B6D1-00A0C90F2744}";//WizardType Guid
                    bool silent = false;

                    int commonIndex = dte2.Application.FileName.IndexOf(@"\Common7");
                    string vsInstallPath = dte2.Application.FileName.Substring(0, commonIndex);
                    //object[] obj = dte.ActiveSolutionProjects as object[];
                    Project project = P;
                    //if (obj.Length > 0)
                    //    project = obj[0] as Project;
                    //Project project = (Project)(((object[])dte.ActiveSolutionProjects)[0]);

                    string itemName = project.Name + ".edmx";
                    string localDir = System.IO.Path.GetDirectoryName(project.FullName);

                    object[] prams = {vsWizardAddItem,project.Name,project.ProjectItems,
                             localDir, itemName,vsInstallPath, silent};

                    Solution2 soln = (Solution2)dte2.Solution;
                    string templatePath = soln.GetProjectItemTemplate("AdoNetEntityDataModelCSharp.zip", "CSharp");
                    dte.LaunchWizard(templatePath, ref prams);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (FServerData.AssemblyOutputPath != null && FServerData.AssemblyOutputPath != "")
                GlobalProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value = FServerData.AssemblyOutputPath;

            return true;
        }

        private void RenameNameSpace(string FileName)
        {
            if (!File.Exists(FileName))
                return;
            System.IO.StreamReader SR = new System.IO.StreamReader(FileName);
            string Context = SR.ReadToEnd();
            SR.Close();
            Context = Context.Replace("TAG_NAMESPACE", FServerData.PackageName);
            System.IO.FileStream Filefs = new System.IO.FileStream(FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            System.IO.StreamWriter SW = new System.IO.StreamWriter(Filefs);
            SW.Write(Context);
            SW.Close();
            Filefs.Close();
        }

        private void GetDataModule()
        {
            Project P = GlobalProject;

            ProjectItem PI;
            for (int I = 1; I <= P.ProjectItems.Count; I++)
            {
                PI = P.ProjectItems.Item(I);
                if (string.Compare(PI.Name, "Component." + FServerData.Language) == 0)
                {
                    string Path = PI.get_FileNames(0);
                    Path = System.IO.Path.GetDirectoryName(Path);
                    RenameNameSpace(Path + "\\Component." + FServerData.Language);
                    Window W = PI.Open("{00000000-0000-0000-0000-000000000000}");
                    W.Activate();
                    GlobalWindow = W;
                    GlobalPI = PI;
                    //return;//???
                    FDesignerHost = (IDesignerHost)W.Object;
                    FDataModule = (Component)FDesignerHost.RootComponent;
                    break;
                }
            }
        }

        private string CreateUniqueComponentName(Component aOwner, string Name)
        {
            int I, J = 1;
            Component C;
            Type T;
            System.Reflection.PropertyInfo P;
            string Result = Name;
            for (I = 0; I < aOwner.Container.Components.Count - 1; I++)
            {
                C = (Component)aOwner.Container.Components[I];
                T = C.GetType();
                P = T.GetProperty("Name");
                if (P != null)
                {
                    while (string.Compare((string)P.GetValue(C, null), Name) == 0)
                    {
                        Result = Name + J.ToString();
                    }
                }
            }
            return Result;
        }

        private Component CreateDataset(TWCFDatasetItem DatasetItem, int Index)
        {
            string TempName = WzdUtils.RemoveSpace(DatasetItem.TableName);
            string ComponentName = CreateUniqueComponentName(FDataModule, TempName);
            if (ComponentName.Contains("."))
                ComponentName = ComponentName.Remove(0, ComponentName.IndexOf('.') + 1);
            EFServerTools.EFCommand IC = FDesignerHost.CreateComponent(typeof(EFServerTools.EFCommand), ComponentName) as EFServerTools.EFCommand;

            EFServerTools.Design.MetadataProvider aMetadataProvider = new EFServerTools.Design.MetadataProvider(System.IO.Path.GetDirectoryName(GlobalProject.FullName), GlobalProject.Name);
            String strEntityTypeName = aMetadataProvider.GetEntitySetType(DatasetItem.ContainerName, DatasetItem.TableName);
            if (DatasetItem.ChildItem != null)
            {
                List<string> lNavgationPropertyNames = aMetadataProvider.GetNavgationPropertyNames(DatasetItem.ContainerName, strEntityTypeName);
                for (int i = 0; i < lNavgationPropertyNames.Count; i++)
                {
                    IncludeObject aFKey = new IncludeObject();
                    aFKey.ObjectName = lNavgationPropertyNames[i];
                    IC.ForeignKeyRelations.Add(aFKey);
                }
            }
            IC.CommandText = String.Format("Select value {0} from {1}.{2} as {0}",
                                            DatasetItem.TableName[0].ToString().ToLower(),
                                            DatasetItem.ContainerName,
                                            DatasetItem.TableName);
            IC.MetadataFile = GlobalProject.Name;
            DatasetItem.Command = IC;
            return IC;
        }

        private void CreateUpdateComponent(EFServerTools.EFCommand EFC, TWCFDatasetItem DatasetItem, int Index)
        {
            string TempName = DatasetItem.TableName;
            string ComponentName = CreateUniqueComponentName(FDataModule, "uc" + TempName);
            if (ComponentName.Contains("."))
            {
                ComponentName = ComponentName.Remove(0, ComponentName.IndexOf('.') + 1);
                ComponentName = "uc" + ComponentName;
            }
            EFServerTools.EFUpdateComponent UC = FDesignerHost.CreateComponent(typeof(EFServerTools.EFUpdateComponent), ComponentName) as EFServerTools.EFUpdateComponent;
            UC.Command = EFC;
            UC.Site.Name = "AA";
            int I;

            TFieldAttrItem FAI;
            EFServerTools.FieldItem FI;
            for (I = 0; I < DatasetItem.FieldAttrItems.Count; I++)
            {
                FAI = (TFieldAttrItem)DatasetItem.FieldAttrItems[I];
                FI = new EFServerTools.FieldItem();
                FI.FieldName = FAI.DataField;
                FI.CheckNull = FAI.CheckNull;
                UC.Fields.Add(FI);
            }
            UC.Site.Name = ComponentName;
        }

        private void SetColumns(ColumnItems aColumnItems, TFieldAttrItems FieldAttrItems, Boolean ParentField)
        {
            aColumnItems.Clear();
            for (int num1 = 0; num1 < FieldAttrItems.Count; num1++)
            {
                TFieldAttrItem item2 = FieldAttrItems[num1] as TFieldAttrItem;
                if (item2.ParentRelationField != null && item2.ParentRelationField != "")
                {
                    ColumnItem item1 = new ColumnItem();
                    if (ParentField)
                    {
                        item1.FieldName = item2.ParentRelationField;
                        item1.Name = item2.ParentRelationField;
                    }
                    else
                    {
                        item1.FieldName = item2.DataField;
                        item1.Name = item2.DataField;
                    }
                    aColumnItems.Add(item1);
                }
            }
        }

        /*
        private void SetColumns(ColumnItems aColumnItems, TFieldAttrItems FieldAttrItems, Boolean ParentField)
        {
            aColumnItems.Clear();
            for (int num1 = 0; num1 < FieldAttrItems.Count; num1++)
            {
                TFieldAttrItem item2 = FieldAttrItems[num1] as TFieldAttrItem;
                if (item2.IsRelationKey)
                {
                    ColumnItem item1 = new ColumnItem();
                    item1.FieldName = item2.DataField;
                    item1.Name = item2.DataField;
                    aColumnItems.Add(item1);
                }
            }
        }

         */

        private void FixupMasterDetail()
        {
            for (int num1 = 0; num1 < FServerData.Datasets.Count; num1++)
            {
                TWCFDatasetItem item1 = (TWCFDatasetItem)FServerData.Datasets[num1];
                if (item1.Relation == null)
                {
                    EFServerTools.EFRelation relation1;
                    string text1;
                    if (item1.ParentItem == null)
                    {
                        TWCFDatasetItem item2 = item1.ChildItem;
                    }
                    if ((item1.ParentItem != null) && (item1.ChildItem == null))
                    {
                        if (item1.ParentItem.Relation != null && item1.ParentItem.Relation.DetailCommand == null)
                        {
                            item1.Relation = item1.ParentItem.Relation;
                            item1.Relation.DetailCommand = item1.Command;
                        }
                        else
                        {
                            String strMaster = item1.ParentItem.TableName;
                            if (strMaster.Contains("."))
                                strMaster = strMaster.Remove(0, strMaster.IndexOf(".") + 1);
                            String strDetail = item1.TableName;
                            if (strDetail.Contains("."))
                                strDetail = strDetail.Remove(0, strDetail.IndexOf(".") + 1);
                            text1 = "id" + WzdUtils.RemoveSpace(strMaster) + "_" + WzdUtils.RemoveSpace(strDetail);
                            relation1 = FDesignerHost.CreateComponent(typeof(EFServerTools.EFRelation), text1) as EFServerTools.EFRelation;
                            item1.Relation = relation1;
                            item1.ParentItem.Relation = relation1;
                            relation1.MasterCommand = item1.ParentItem.Command;
                            relation1.DetailCommand = item1.Command;
                        }
                    }
                    if ((item1.ParentItem != null) && (item1.ChildItem != null))
                    {
                        if (item1.ParentItem.Relation != null && item1.ParentItem.Relation.DetailCommand == null)
                        {
                            item1.Relation = item1.ParentItem.Relation;
                            item1.Relation.DetailCommand = item1.Command;
                        }
                        else
                        {
                            String strMaster = item1.ParentItem.TableName;
                            if (strMaster.Contains("."))
                                strMaster = strMaster.Remove(0, strMaster.IndexOf(".") + 1);
                            String strDetail = item1.TableName;
                            if (strDetail.Contains("."))
                                strDetail = strDetail.Remove(0, strDetail.IndexOf(".") + 1);
                            text1 = "id" + WzdUtils.RemoveSpace(strMaster) + "_" + WzdUtils.RemoveSpace(strDetail);
                            relation1 = FDesignerHost.CreateComponent(typeof(EFServerTools.EFRelation), text1) as EFServerTools.EFRelation;
                            item1.Relation = relation1;
                            item1.ParentItem.Relation = relation1;
                            relation1.MasterCommand = item1.ParentItem.Command;
                            relation1.DetailCommand = item1.Command;
                        }
                    }
                }
            }
        }

        private void GenDatasets()
        {
            int I;
            TWCFDatasetItem DatasetItem;
            EFCommand EFC;
            for (I = 0; I < FServerData.Datasets.Count; I++)
            {
                DatasetItem = (TWCFDatasetItem)FServerData.Datasets[I];
                if (DatasetItem.GenerateEntity != "View Only")
                {
                    EFC = CreateDataset(DatasetItem, I + 1) as EFServerTools.EFCommand;
                    CreateUpdateComponent(EFC, DatasetItem, I + 1);
                }
            }
            FixupMasterDetail();
        }

        private void GenViewDataSet()
        {
            foreach (TWCFDatasetItem DatasetItem in FServerData.Datasets)
            {
                if (DatasetItem.ParentItem == null)
                {
                    if (DatasetItem.GenerateEntity != "Master")
                    {
                        string ComponentName = CreateUniqueComponentName(FDataModule, "View_" + DatasetItem.TableName);
                        if (ComponentName.Contains("."))
                        {
                            ComponentName = ComponentName.Remove(0, ComponentName.IndexOf('.') + 1);
                            ComponentName = "View_" + ComponentName;
                        }
                        if (ComponentName.Contains(" "))
                        {
                            String[] temp = ComponentName.Split(' ');
                            ComponentName = "";
                            foreach (String str in temp)
                                ComponentName += str.Trim();
                        }
                        EFServerTools.EFCommand IC = FDesignerHost.CreateComponent(typeof(EFServerTools.EFCommand), ComponentName) as EFServerTools.EFCommand;
                        IC.CommandText = String.Format("Select value {0} from {1}.{2} as {0}",
                                                        DatasetItem.TableName[0].ToString().ToLower(),
                                                        DatasetItem.ContainerName,
                                                        DatasetItem.TableName);
                        IC.MetadataFile = GlobalProject.Name;
                        DatasetItem.Command = IC;
                    }
                }
            }
        }
    }

    public class TWCFDatasetItem : TCollectionItem
    {
        private string FName, FTableName, FDatabaseName, FContainerName;
        private TFieldAttrItems FFieldAttrItems;
        private TWCFDatasetItem FParentItem, FChildItem;
        private TStringList FFieldList = null;
        private TStringList FFieldCaptionList = null;
        private EFServerTools.EFCommand FInfoCommand;
        private bool FAddAll;
        private EFServerTools.EFRelation FRelation;
        private String FGenerateEntity;

        public TWCFDatasetItem()
        {
            FFieldAttrItems = new TFieldAttrItems(this);
        }

        public string Name
        {
            get
            {
                return FName;
            }
            set
            {
                FName = value;
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

        public string ContainerName
        {
            get
            {
                return FContainerName;
            }
            set
            {
                FContainerName = value;
            }
        }

        public EFServerTools.EFCommand Command
        {
            get
            {
                return FInfoCommand;
            }
            set
            {
                FInfoCommand = value;
            }
        }

        public EFServerTools.EFRelation Relation
        {
            get
            {
                return FRelation;
            }
            set
            {
                FRelation = value;
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

        public bool AddAll
        {
            get { return FAddAll; }
            set { FAddAll = value; }
        }

        public TFieldAttrItems FieldAttrItems
        {
            get
            {
                return FFieldAttrItems;
            }
            set
            {
                FFieldAttrItems = value;
            }
        }

        public TWCFDatasetItem ParentItem
        {
            get
            {
                return FParentItem;
            }
            set
            {
                FParentItem = value;
            }
        }

        public TWCFDatasetItem ChildItem
        {
            get
            {
                return FChildItem;
            }
            set
            {
                FChildItem = value;
            }
        }

        public String GenerateEntity
        {
            get { return FGenerateEntity; }
            set { FGenerateEntity = value; }
        }

        public TStringList FieldList
        {
            get
            {
                if (FFieldList == null)
                {
                    FFieldList = new TStringList();
                }
                if (FFieldList.Count == 0)
                {
                    String OldTableName = TableName;
                    String Owner = String.Empty;
                    String SS = TableName;
                    if (TableName.IndexOf('.') > -1)
                    {
                        Owner = WzdUtils.GetToken(ref SS, new char[] { '.' });
                        TableName = SS;
                    }

                    string[] S = new string[4];
                    //S[1] = Owner;
                    S[2] = TableName;

                    String SortName = "ORDINAL_POSITION";
                    TDatasetCollection dc = (TDatasetCollection)Collection;
                    TServerData sd = (TServerData)dc.Owner;
                    if (sd.DatabaseType != ClientType.ctInformix)
                    {
                        if (sd.DatabaseType == ClientType.ctOracle)
                        {
                            String UserID = WzdUtils.GetFieldParam(sd.ConnectionString.ToLower(), "user id") == "" ? WzdUtils.GetFieldParam(sd.ConnectionString.ToLower(), "uid") : WzdUtils.GetFieldParam(sd.ConnectionString.ToLower(), "user id");
                            if (Owner != null && Owner != "") UserID = Owner;
                            S = new String[] { UserID, TableName };
                            SortName = "ID";
                        }
                        DataTable D = null;
                        D = sd.Owner.GlobalConnection.GetSchema("Columns", S);
                        if (D.Rows.Count == 0 && sd.DatabaseType == ClientType.ctOracle)
                        {
                            S = new String[] { Owner, TableName };
                            D = sd.Owner.GlobalConnection.GetSchema("Columns", S);
                        }
                        DataRow[] DRs = D.Select("", SortName + " ASC");

                        foreach (DataRow DR in DRs)
                            FFieldList.Add(DR["COLUMN_NAME"].ToString());

                        TableName = OldTableName;
                    }
                    else
                    {
                        String sQL = "select * from " + TableName + " where 1=0";
                        IDbCommand cmd = sd.Owner.GlobalConnection.CreateCommand();
                        cmd.CommandText = sQL;
                        if (sd.Owner.GlobalConnection.State == ConnectionState.Closed)
                        { sd.Owner.GlobalConnection.Open(); }

                        IDataReader reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly);
                        DataTable schemaTable = reader.GetSchemaTable();

                        foreach (DataRow DR in schemaTable.Rows)
                            FFieldList.Add(DR["COLUMNNAME"].ToString());
                    }
                }
                return FFieldList;
            }
        }

        public TStringList FieldCaptionList
        {
            get
            {
                if (FFieldCaptionList == null)
                {
                    FFieldCaptionList = new TStringList();
                }
                if (FFieldCaptionList.Count == 0)
                {
                    TDatasetCollection dc = (TDatasetCollection)Collection;
                    TServerData sd = (TServerData)dc.Owner;
                    InfoCommand aInfoCommand = new InfoCommand();
                    aInfoCommand.Connection = sd.Owner.GlobalConnection;
                    TableName = "";// WzdUtils.RemoveQuote(TableName);
                    String Owner = String.Empty, SS = TableName;
                    if (TableName.IndexOf('.') > -1)
                    {
                        Owner = WzdUtils.GetToken(ref SS, new char[] { '.' });
                        TableName = SS;
                    }
                    aInfoCommand.CommandText = "Select FIELD_NAME,CAPTION from COLDEF where TABLE_NAME='" + TableName + "' OR TABLE_NAME='" + Owner + "." + TableName + "'";
                    IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                    DataSet D = new DataSet();

                    FFieldCaptionList.Clear();
                    int I;
                    DataRow DR;
                    for (I = 0; I < D.Tables[0].Rows.Count; I++)
                    {
                        DR = D.Tables[0].Rows[I];
                        if (DR["FIELD_NAME"].ToString() != "")
                            FFieldCaptionList.Add(DR["FIELD_NAME"] + "=" + DR["CAPTION"]);
                    }
                }
                return FFieldCaptionList;
            }
        }
    }

    public class TWCFDatasetCollection : TCollection
    {
        public TWCFDatasetCollection(object Owner)
        {
            base.Owner = Owner;
        }

        public TWCFDatasetItem FindItem(string AName)
        {
            foreach (TWCFDatasetItem aItem in InnerList)
            {
                if (String.Compare(aItem.Name, AName) == 0)
                    return aItem;
            }
            return null;
        }
    }

    public class TWCFServerData : Object
    {
        private string FDatabaseName, FPackageName, FOutputPath, FSolutionName, FAssemblyOutputPath, FEEPAlias;
        private TWCFDatasetCollection FDatasetCollection;
        private TStringList FTableNameList = null;
        private TStringList FTableNameCaptionList = null;
        private MWizard.fmServerWzd FOwner;
        private bool FNewSolution = false;
        private string FConnectionString;
        private string FCodeOutputPath;
        private ClientType FDatabaseType;
        private String FLanguage = "cs";
        private fmWCFServerWzd FWCFOwner;

        public TWCFServerData(MWizard.fmServerWzd Owner)
        {
            FDatasetCollection = new TWCFDatasetCollection(this);
            FOwner = Owner;
        }

        public TWCFServerData(fmWCFServerWzd Owner)
        {
            // TODO: Complete member initialization
            FDatasetCollection = new TWCFDatasetCollection(this);
            FWCFOwner = Owner;
        }

        public void ResetDatabaseConnection()
        {
            TableNameList.Clear();
            TableNameCaptionList.Clear();
        }

        public String EEPAlias
        {
            get { return FEEPAlias; }
            set { FEEPAlias = value; }
        }

        public String Language
        {
            get { return FLanguage; }
            set { FLanguage = value; }
        }

        private Boolean IsKeyField(String KeyFields, String aField)
        {
            String Temp = KeyFields;
            String S = WzdUtils.GetToken(ref Temp, new char[] { ';' });
            while (S != "")
            {
                if (String.Compare(S, aField) == 0)
                    return true;
                S = WzdUtils.GetToken(ref Temp, new char[] { ';' });
            }
            return false;
        }

        private void LoadFieldAttrs(XmlNode Node, TFieldAttrItems AttrItems, String KeyFields)
        {
            TFieldAttrItem FAI;
            int I;
            XmlNode AttrNode;
            for (I = 0; I < Node.ChildNodes.Count; I++)
            {
                AttrNode = Node.ChildNodes[I];
                FAI = new TFieldAttrItem();
                FAI.DataField = AttrNode.Attributes["DataField"].Value;
                FAI.Description = AttrNode.Attributes["Description"].Value;
                FAI.IsKey = AttrNode.Attributes["IsKey"].Value == "1";
                FAI.CheckNull = AttrNode.Attributes["CheckNull"].Value == "1";
                FAI.IsRelationKey = AttrNode.Attributes["IsRelationKey"].Value == "1"; //IsKeyField(KeyFields, FAI.DataField);
                FAI.ParentRelationField = AttrNode.Attributes["ParentRelationField"].Value;
                AttrItems.Add(FAI);
            }
        }

        private void LoadDatasets(XmlNode Node)
        {
            int I;
            TDatasetItem DI;
            XmlNode DatasetNode, FieldAttrsNode;
            for (I = 0; I < Node.ChildNodes.Count; I++)
            {
                DatasetNode = Node.ChildNodes[I];
                DI = new TDatasetItem();
                DI.Name = DatasetNode.Attributes["Name"].Value;
                DI.DatabaseType = FDatabaseType;
                DI.DatabaseName = DatasetNode.Attributes["DatabaseName"].Value;
                DI.TableName = DatasetNode.Attributes["TableName"].Value;
                FieldAttrsNode = WzdUtils.FindNode(null, DatasetNode, "FieldAttrItems");
                LoadFieldAttrs(FieldAttrsNode, DI.FieldAttrItems, DatasetNode.Attributes["RelFields"].Value);
                Datasets.Add(DI);
            }

            for (I = 0; I < Node.ChildNodes.Count; I++)
            {
                DatasetNode = Node.ChildNodes[I];
                if (DatasetNode.Attributes["ParentItem"].Value != "")
                {
                    TWCFDatasetItem ChildItem = Datasets.FindItem(DatasetNode.Attributes["Name"].Value);
                    TWCFDatasetItem ParentItem = Datasets.FindItem(DatasetNode.Attributes["ParentItem"].Value);
                    ChildItem.ParentItem = ParentItem;
                }
            }
        }

        public ClientType DatabaseType
        {
            get { return FDatabaseType; }
            set { FDatabaseType = value; }
        }

        private String GetConnectionString(String DatabaseName)
        {
            String ServerPath = EEPRegistry.Server + "\\";
            string text3 = SystemFile.DBFile;
            XmlDocument document1 = new XmlDocument();
            document1.Load(text3);
            foreach (XmlNode node1 in document1.FirstChild.FirstChild.ChildNodes)
            {
                String aName = node1.Name;
                if (String.Compare(aName, DatabaseName) == 0)
                {
                    DatabaseType = (ClientType)int.Parse(node1.Attributes["Type"].Value);
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
                    return text1;
                }
            }
            return "";
        }

        public object LoadFromXML(string XML)
        {
            System.Xml.XmlNode Node = null;
            System.Xml.XmlDocument Doc = new System.Xml.XmlDocument();
            Doc.LoadXml(XML);
            Node = Doc.SelectSingleNode("ServerData");
            EEPAlias = Node.Attributes["DatabaseName"].Value;
            ConnectionString = GetConnectionString(EEPAlias);
            SolutionName = Node.Attributes["SolutionName"].Value;
            OutputPath = Node.Attributes["OutputPath"].Value;
            CodeOutputPath = Node.Attributes["CodeOutputPath"].Value;
            NewSolution = Node.Attributes["NewSolution"].Value == "1";
            PackageName = Node.Attributes["PackageName"].Value;
            if (Node.Attributes["Language"].Value.ToString().CompareTo("C#") == 0)
                this.Language = "cs";
            else
                this.Language = "vb";
            Node = WzdUtils.FindNode(Doc, Node, "Datasets");
            LoadDatasets(Node);
            return null;
        }

        public MWizard.fmServerWzd Owner
        {
            get
            {
                return FOwner;
            }
        }

        public MWizard.fmWCFServerWzd WCFOwner
        {
            get
            {
                return FWCFOwner;
            }
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

        public string ConnectionString
        {
            get
            {
                return FConnectionString;
            }
            set
            {
                FConnectionString = value;
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

        public String AssemblyOutputPath
        {
            get { return FAssemblyOutputPath; }
            set { FAssemblyOutputPath = value; }
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

        public TWCFDatasetCollection Datasets
        {
            get
            {
                return FDatasetCollection;
            }
            set
            {
                FDatasetCollection = value;
            }
        }

        public TStringList TableNameList
        {
            get
            {
                if (FTableNameList == null)
                {
                    FTableNameList = new TStringList();
                }
                if (FTableNameList.Count == 0)
                {
                    if (FOwner != null)
                    {
                        if (FOwner.ServerData.DatabaseType != ClientType.ctInformix)
                        {
                            String[] Params = null;
                            if (FOwner.GlobalConnection.ConnectionString != null && FOwner.ServerData.DatabaseType == ClientType.ctOracle)
                            {
                                String UserID = WzdUtils.GetFieldParam(FOwner.GlobalConnection.ConnectionString.ToLower(), "user id");
                                Params = new String[] { UserID.ToUpper() };
                            }
                            DataTable T = FOwner.GlobalConnection.GetSchema("Tables", Params);
                            DataRow[] dr = T.Select("", "TABLE_NAME ASC");
                            bool flag = false;
                            foreach (DataColumn DC in T.Columns)
                            {
                                if (DC.Caption.ToLower() == "owner")
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            foreach (DataRow DR in dr)
                            {
                                String S = "";
                                if (flag && !String.IsNullOrEmpty(DR["OWNER"].ToString()))
                                    S = DR["OWNER"].ToString() + '.';
                                FTableNameList.Add(S + DR["TABLE_NAME"].ToString());
                            }

                            T = FOwner.GlobalConnection.GetSchema("Views", Params);
                            if (T.Rows.Count > 0)
                            {
                                if (FOwner.ServerData.DatabaseType != ClientType.ctOracle)
                                    dr = T.Select("", "TABLE_NAME ASC");
                                else
                                    dr = T.Select("", "VIEW_NAME ASC");
                                flag = false;
                                foreach (DataColumn DC in T.Columns)
                                {
                                    if (DC.Caption.ToLower() == "owner")
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                                foreach (DataRow DR in dr)
                                {
                                    String S = "";
                                    if (flag && !String.IsNullOrEmpty(DR["OWNER"].ToString()))
                                        S = DR["OWNER"].ToString() + '.';
                                    if (FOwner.ServerData.DatabaseType != ClientType.ctOracle)
                                        FTableNameList.Add(DR["TABLE_NAME"].ToString());
                                    else
                                        FTableNameList.Add(S + DR["VIEW_NAME"].ToString());

                                }
                            }
                        }
                        else
                        {
                            List<String> allTables = WzdUtils.GetAllTablesList(FOwner.GlobalConnection, ClientType.ctInformix);
                            allTables.Sort();
                            foreach (String str in allTables)
                                FTableNameList.Add(str);
                        }
                    }
                }

                return FTableNameList;
            }
        }

        public TStringList TableNameCaptionList
        {
            get
            {
                if (FTableNameCaptionList == null)
                {
                    FTableNameCaptionList = new TStringList();
                }
                if (FTableNameCaptionList.Count == 0)
                {
                    int I;
                    DataRow DR;
                    InfoCommand aInfoCommand = new InfoCommand(DatabaseType);
                    aInfoCommand.Connection = FOwner.GlobalConnection;

                    aInfoCommand.CommandText = "Select TABLE_NAME, CAPTION from COLDEF where FIELD_NAME = '' or FIELD_NAME is null order by TABLE_NAME";
                    IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
                    DataSet D = new DataSet();
                    WzdUtils.FillDataAdapter(DatabaseType, DA, D, "COLDEF");


                    FTableNameCaptionList.Clear();
                    for (I = 0; I < D.Tables[0].Rows.Count; I++)
                    {
                        DR = D.Tables[0].Rows[I];
                        if (DR["TABLE_NAME"].ToString().Trim() != "" && DR["CAPTION"].ToString().Trim() != "")
                            FTableNameCaptionList.Add(DR["TABLE_NAME"].ToString().Trim() + "=" + DR["CAPTION"].ToString().Trim());
                    }
                }
                return FTableNameCaptionList;
            }
        }
    }

}