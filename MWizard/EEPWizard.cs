using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using EEPLibrary;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using System.Threading;
using Srvtools;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Net;
using System.IO;
using System.Collections;
using System.Data;
using Microsoft.Win32;
using System.Xml;
using Microsoft.VisualStudio.Designer.Interfaces;
using System.ComponentModel.Design;
using System.Drawing;

namespace MWizard
{
    public partial class EEPWizard : Object, EEPLibrary.IEEPWizard
    {
        private DTE2 FDTE2;
        private AddIn FAddIn;
        private fmServerWzd fmServerWizard;
        //private fmWCFServerWzd fmWCFServerWizard;
        private fmClientWzard fmClientWizard;
        private fmExtClientWizard fmExtClientWizard;
        private fmEEPWebWizard fmWebWizard;
        //private fmEEPWCFWebWizard fmWCFWebFormWizard;
        private fmExtEEPWebWzdu fmExtWebWizard;
        //private fmSLClientWizard fmSLClient;
        private EEPRemoteModule FRemoteModule = null;
        private IDesignerHost FDesignerHost = null;
        private fmSetServerPath fmServerPath = null;
        private fmNewEmptySolution fmNewEmptySolution = null;
        private fmDevExpress fmDevExpress = null;
        private fmJQueryWebForm fmJQueryWebForm = null;
        private fmJQMobileForm fmJQMobileForm = null;
        private fmRDLCWizard fmRDLCWizard = null;
        private fmIonicWizard fmIonicWizard = null;
        private fmJQueryToJQMobile fmJQueryToJQMobileForm = null;

        public EEPWizard(DTE2 aDTE2, AddIn aAddIn)
        {
            FDTE2 = aDTE2;
            FAddIn = aAddIn;
            fmServerWizard = new fmServerWzd(FDTE2, FAddIn, this);
            //fmWCFServerWizard = new fmWCFServerWzd(FDTE2, FAddIn, this);
            fmClientWizard = new fmClientWzard(FDTE2, FAddIn);
            fmExtClientWizard = new fmExtClientWizard(FDTE2, FAddIn);
            fmWebWizard = new fmEEPWebWizard(FDTE2, FAddIn);
            fmExtWebWizard = new fmExtEEPWebWzdu(FDTE2, FAddIn);
            //fmWCFWebFormWizard = new fmEEPWCFWebWizard(FDTE2, FAddIn);
            //fmSLClient = new fmSLClientWizard(FDTE2, FAddIn);
            fmServerPath = new fmSetServerPath(FAddIn);
            fmNewEmptySolution = new fmNewEmptySolution(FDTE2, FAddIn);
            fmDevExpress = new fmDevExpress(FDTE2, FAddIn);
            fmJQueryWebForm = new fmJQueryWebForm(FDTE2, FAddIn);
            fmJQMobileForm = new fmJQMobileForm(FDTE2, FAddIn);
            fmRDLCWizard = new fmRDLCWizard(FDTE2, FAddIn);
            fmIonicWizard = new fmIonicWizard(FDTE2, FAddIn);
            fmJQueryToJQMobileForm = new fmJQueryToJQMobile(FDTE2, FAddIn);

            try
            {
                String s = Path.GetDirectoryName(FAddIn.Object.GetType().Assembly.Location) + @"\";
                //RemotingConfiguration.Configure(s + "MWizard.dll.config", true);
                CliUtils.LoadLoginServiceConfig(s + "MWizard.dll.config");
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                //throw;
            }
        }

        object EEPLibrary.IEEPWizard.CallMethod(string MethodName, object Params)
        {
            object O = null;
            try
            {
                if (string.Compare(MethodName, "GenServerModule") == 0)
                    O = GenServerModule(Params);
                if (string.Compare(MethodName, "GenClientModule") == 0)
                    O = GenClientModule(Params);
                if (string.Compare(MethodName, "GenWebForm") == 0)
                    O = GenWebForm(Params);
                if (string.Compare(MethodName, "OpenFile") == 0)
                    O = OpenFile(Params);
                if (string.Compare(MethodName, "OpenPage") == 0)
                    O = OpenPage(Params);
                if (string.Compare(MethodName, "GetFormImage") == 0)
                    O = GetFormImage(Params);
                if (string.Compare(MethodName, "TestVisualStudio2005") == 0)
                    O = TestVisualStudio2005(Params);
                if (string.Compare(MethodName, "GetProvider") == 0)
                    O = GetProvider(Params);
                if (string.Compare(MethodName, "GetTableRelationByProvider") == 0)
                    O = GetTableRelationByProvider(Params);
                if (string.Compare(MethodName, "GetPageInfo") == 0)
                    O = GetPageInfo(Params);
                if (string.Compare(MethodName, "DeletePackage") == 0)
                    O = DeletePackage(Params);
                object[] RetValue = new object[2];
                RetValue[0] = ((Object[])O)[0];
                RetValue[1] = ((Object[])O)[1];
                return RetValue;
            }
            catch (Exception E)
            {
                object[] RetValue = new object[2];
                RetValue[0] = -1;
                RetValue[1] = E.Message;
                return RetValue;
            }
        }

        void EEPLibrary.IEEPWizard.ShowMsg()
        {
            MessageBox.Show("ShowMsg");
        }

        void EEPLibrary.IEEPWizard.HookDTE(object DTE)
        {
        }

        public fmServerWzd ServerWizard
        {
            get
            {
                return fmServerWizard;
            }
        }

        //public fmWCFServerWzd WCFServerWizard
        //{
        //    get
        //    {
        //        return fmWCFServerWizard;
        //    }
        //}

        public fmClientWzard ClientWizard
        {
            get
            {
                return fmClientWizard;
            }
        }

        public fmExtClientWizard ExtClientWizard
        {
            get
            {
                return fmExtClientWizard;
            }
        }

        public fmEEPWebWizard WebWizard
        {
            get
            {
                return fmWebWizard;
            }
        }

        //public fmEEPWCFWebWizard WCFWebFormWizard
        //{
        //    get
        //    {
        //        return fmWCFWebFormWizard;
        //    }
        //}

        public fmExtEEPWebWzdu ExtWebWizard
        {
            get
            {
                return fmExtWebWizard;
            }
        }

        //public fmSLClientWizard SLClient
        //{
        //    get
        //    {
        //        return fmSLClient;
        //    }
        //}

        public fmSetServerPath ServerPath
        {
            get
            {
                return fmServerPath;
            }
        }

        public fmNewEmptySolution NewEmptySolution
        {
            get
            {
                return fmNewEmptySolution;
            }
        }

        public fmDevExpress DevExpress
        {
            get
            {
                return fmDevExpress;
            }
        }

        public fmJQueryWebForm JQueryWebFormWizard
        {
            get
            {
                return fmJQueryWebForm;
            }
        }

        public fmJQMobileForm JQMobileFormWizard
        {
            get
            {
                return fmJQMobileForm;
            }
        }

        public fmJQueryToJQMobile JQueryToJQMobileWizard
        {
            get
            {
                return fmJQueryToJQMobileForm;
            }
        }

        public fmRDLCWizard RDLCWizard
        {
            get
            {
                return fmRDLCWizard;
            }
        }

        public fmIonicWizard IonicWizard
        {
            get
            {
                return fmIonicWizard;
            }
        }

        public object GenServerModule(object Params)
        {
            object[] RealParams = (object[])Params;
            TGenCode GenCode = new TGenCode(fmServerWizard.SDGenServerModule);
            try
            {
                fmServerWizard.Invoke(GenCode, RealParams);
                return new Object[] { 1, "Success" };
            }
            catch (Exception E)
            {
                return new Object[] { -1, E.Message };
            }
        }

        public object GenClientModule(object Params)
        {
            object[] RealParams = (object[])Params;
            TGenCode GenCode = new TGenCode(fmClientWizard.SDGenClientModule);
            try
            {
                fmClientWizard.Invoke(GenCode, RealParams);
                return new Object[] { 1, "Success" };
            }
            catch (Exception E)
            {
                return new Object[] { -1, E.Message };
            }
        }

        public object GenWebForm(object Params)
        {
            object[] RealParams = (object[])Params;
            TGenCode GenCode = new TGenCode(fmWebWizard.SDGenWebForm);
            try
            {
                fmWebWizard.Invoke(GenCode, RealParams);
                return new Object[] { 1, "Success" };
            }
            catch (Exception E)
            {
                return new Object[] { -1, E.Message };
            }
        }

        public object OpenFile(object Params)
        {
            object[] RealParams = (object[])Params;
            string SolutionName = RealParams[0].ToString();
            string ProjectName = RealParams[1].ToString();
            string FileName = RealParams[2].ToString();
            string UnitName = RealParams[3].ToString();
            Project P;
            ProjectItem PI;
            int I, J;
            try
            {
                if (SolutionName == "")
                    return null;
                if (FDTE2.Solution == null || FDTE2.Solution.FileName.CompareTo(SolutionName) != 0)
                    FDTE2.Solution.Open(SolutionName);
                for (I = 1; I <= FDTE2.Solution.Projects.Count; I++)
                {
                    P = FDTE2.Solution.Projects.Item(I);
                    if (ProjectName == "")
                        break;
                    if (string.Compare(P.Name, ProjectName) == 0)
                    {
                        if (FileName == "")
                            break;
                        for (J = 1; J <= P.ProjectItems.Count; J++)
                        {
                            PI = P.ProjectItems.Item(J);
                            if (string.Compare(PI.Name, UnitName) == 0)
                            {
                                Window W = PI.Open("{00000000-0000-0000-0000-000000000000}");
                                W.Activate();
                            }
                        }
                    }
                }
                return new Object[] { 1, "Success" };
            }
            catch (Exception E)
            {
                return new Object[] { -1, E.Message };
            }
        }

        public Object OpenPage(Object Params)
        {
            Object[] RealParams = (object[])Params;
            String SolutionName = RealParams[0].ToString();
            String WebSiteName = RealParams[1].ToString();
            String FolderName = RealParams[2].ToString();
            String PageName = RealParams[3].ToString();
            String OpenKind = RealParams[4].ToString();
            try
            {
                if (FDTE2.Solution == null || FDTE2.Solution.FileName.CompareTo(SolutionName) != 0)
                    FDTE2.Solution.Open(SolutionName);
                foreach (Project P in FDTE2.Solution)
                {
                    if (P.Kind.CompareTo("{E24C65DC-7377-472b-9ABA-BC803B73C61A}") == 0)
                    {
                        String VSName = WzdUtils.FixupToVSWebSiteName(P.Name);
                        if (VSName.CompareTo(WebSiteName) == 0)
                        {
                            foreach (ProjectItem PI in P.ProjectItems)
                            {
                                if (PI.Name.CompareTo(FolderName) == 0)
                                {
                                    foreach (ProjectItem aPI in PI.ProjectItems)
                                    {
                                        if (aPI.Name.CompareTo(PageName) == 0)
                                        {
                                            if (OpenKind.CompareTo("Open") == 0)
                                            {
                                                Window aWindow = aPI.Open("{00000000-0000-0000-0000-000000000000}");
                                                aWindow.Activate();
                                                aWindow = aPI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
                                                aWindow.Activate();
                                                FDTE2.MainWindow.Activate();
                                            }
                                            if (OpenKind.CompareTo("Delete") == 0)
                                            {
                                                aPI.Delete();
                                                return null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return new Object[] { 1, "Success" };
            }
            catch (Exception E)
            {
                return new Object[] { -1, E.Message };
            }
        }

        private void CheckRemotingObject(string UserID, string Password, string DBName, string SiteCode,
            string CurrentProject)
        {
            try
            {
                FRemoteModule.ToString();
            }
            catch
            {
                CliUtils.fLoginUser = UserID;
                CliUtils.fLoginPassword = Password;
                CliUtils.fLoginDB = DBName;
                CliUtils.fSiteCode = SiteCode;//涴跺sitecode猁蚕蚚誧懂扢...
                //CliUtils.fClientLang = SYS_LANGUAGE.ENG;
                CliUtils.fComputerName = Dns.GetHostName();
                //CliUtils.fComputerIp = Dns.GetHostByName(CliUtils.fComputerName).AddressList[0].ToString();
                CliUtils.fComputerIp = Dns.GetHostEntry(CliUtils.fComputerName).AddressList[0].ToString();
                CliUtils.fCurrentProject = CurrentProject;
                try
                {
                    string message = "";
                    bool rtn = CliUtils.Register(ref message);
                    if (rtn)
                    {
                        CliUtils.GetSysXml(SystemFile.SysMsgFile);
                    }
                    else
                    {
                        MessageBox.Show(message);
                    }
                    //LoginService loginService = new LoginService(); // Remoting object

                    //BeginObtainService:
                    //// Obtain service from the master server
                    //string serverIP = "";
                    //try
                    //{
                    //    serverIP = loginService.GetServerIP();
                    //}
                    //catch (Exception err)
                    //{
                    //    if ((string.Compare(err.Message.ToLower(), "unable to connect to the remote server") == 0) &&
                    //        (CliUtils.PassByEEPListener()))
                    //    {
                    //        try
                    //        {
                    //            CliUtils.EEPListenerService.StartupEEPNetServer();
                    //        }
                    //        catch (Exception E1)
                    //        {
                    //            MessageBox.Show(E1.Message);
                    //            return;
                    //        }
                    //        goto BeginObtainService;
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show(err.Message);
                    //        return;
                    //    }
                    //}

                    //if (serverIP == null || serverIP.Trim() == "")
                    //{
                    //    MessageBox.Show("Can not login due to busy service");
                    //    return;
                    //}

                    //// Try to connect to server, reobtain service from the master server if failed
                    //try
                    //{
                    //    FRemoteModule = Activator.GetObject(typeof(EEPRemoteModule),
                    //        string.Format("http://{0}:8989/InfoRemoteModule.rem", serverIP)) as EEPRemoteModule;
                    //    FRemoteModule.ToString();
                    //}
                    //catch
                    //{
                    //    loginService.DeRegisterRemoteServer(serverIP);
                    //    goto BeginObtainService;
                    //}

                    //// Register EEPRemoteModule on the server
                    //WellKnownClientTypeEntry clientEntry = new WellKnownClientTypeEntry(typeof(EEPRemoteModule),
                    //    string.Format("http://{0}:8989/InfoRemoteModule.rem", serverIP));
                    //RemotingConfiguration.RegisterWellKnownClientType(clientEntry);

                    //// End Add

                    //// 检查SysMsg.xml
                    //String path = Path.GetDirectoryName(FAddIn.Object.GetType().Assembly.Location) + @"\sysmsg.xml";
                    //DateTime t = new DateTime(1900, 1, 1, 0, 0, 0);
                    //if (File.Exists(path))
                    //{
                    //    FileInfo fileInfo = new FileInfo(path);
                    //    t = fileInfo.LastWriteTime;
                    //}
                    //object[] ret = CliUtils.CallMethod("GLModule", "GetSysMsgXml", new object[] { t });
                    //if (ret != null)
                    //{
                    //    if (ret[0].ToString() == "0" && ret[1].ToString() != "0")
                    //    {
                    //        XmlDocument xmlDoc = new XmlDocument();
                    //        xmlDoc.LoadXml(ret[1].ToString());
                    //        xmlDoc.Save(path);
                    //    }
                    //    else if (ret[0].ToString() == "1")
                    //    {
                    //        MessageBox.Show(ret[1].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    }
                    //}

                    CliUtils.fLoginUser = UserID;
                    CliUtils.fLoginPassword = Password;
                    CliUtils.fLoginDB = DBName;
                    CliUtils.fCurrentProject = CurrentProject;
                    string sParam = CliUtils.fLoginUser + ':' + CliUtils.fLoginPassword + ':' + CliUtils.fLoginDB + ":0";
                    object[] myRet = CliUtils.CallMethod("GLModule", "CheckUser", new object[] { (object)sParam });
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void NotifyRefresh(uint SleepTime)
        {
            fmVirDialog aForm = new fmVirDialog(true, SleepTime);
            aForm.Dispose();
        }

        private void GetChildRelation(DataRelationCollection Relations, ArrayList Tables)
        {
            ArrayList Children = new ArrayList();
            foreach (DataRelation R in Relations)
            {
                Children.Add(R.ChildTable.TableName);
                GetChildRelation(R.ChildTable.ChildRelations, Tables);
            }
            if (Children.Count > 0)
            {
                Tables.Add(Children);
            }
        }

        private Object GetTableRelation(InfoDataSet Master)
        {
            ArrayList Tables = new ArrayList();
            GetChildRelation(Master.RealDataSet.Tables[0].ChildRelations, Tables);
            Object[] Result = new Object[Tables.Count + 1];
            Result[0] = new Object[1] { Master.RealDataSet.Tables[0].TableName };
            for (int I = 0; I < Tables.Count; I++)
            {
                ArrayList AL = (ArrayList)Tables[I];
                Object[] ChildTable = new Object[AL.Count];
                for (int J = 0; J < AL.Count; J++)
                {
                    ChildTable[J] = AL[J];
                }
                Result[I + 1] = ChildTable;
            }
            return Result;
        }

        private void DoGetTextBoxList(Control aForm, ArrayList List, String SolutionName)
        {
            foreach (Control C in aForm.Controls)
            {
                if (C.GetType().Equals(typeof(InfoTextBox)) || C.GetType().Equals(typeof(TextBox)))
                {
                    TextBox TB = (TextBox)C;
                    if (TB.DataBindings.Count > 0)
                    {
                        Binding B = TB.DataBindings[0];
                        string TableName = GetTableNameByBindingSource((InfoBindingSource)B.DataSource, SolutionName);
                        List.Add(TableName + "." + B.BindingMemberInfo.BindingField);
                    }
                }
                DoGetTextBoxList(C, List, SolutionName);
            }
        }

        private Object GetTextBoxList(InfoForm aForm, String SolutionName)
        {
            ArrayList List = new ArrayList();
            DoGetTextBoxList(aForm, List, SolutionName);
            Object[] Result = new Object[List.Count];
            for (int I = 0; I < List.Count; I++)
                Result[I] = List[I];
            return Result;
        }

        private void DoGetDefaultValidateList(Control aForm, ArrayList List, String SolutionName)
        {
            InfoForm aInfoForm = (InfoForm)aForm;
            ArrayList aList = aInfoForm.GetIntfObjects(typeof(IFindContainer));
            foreach (Object C in aList)
            {
                if (C.GetType().Equals(typeof(DefaultValidate)))
                {
                    DefaultValidate DV = (DefaultValidate)C;
                    foreach (FieldItem FI in DV.FieldItems)
                    {
                        Object[] aItem = new Object[] { DV.BindingSource.text, FI.FieldName, FI.CheckNull.ToString(),
                            FI.Validate, FI.CarryOn, FI.DefaultValue};
                        List.Add(aItem);
                    }
                }
            }
        }

        private Object GetDefaultValidateList(InfoForm aForm, String SolutionName)
        {
            ArrayList List = new ArrayList();
            DoGetDefaultValidateList(aForm, List, SolutionName);
            Object[] Result = new Object[List.Count];
            for (int I = 0; I < List.Count; I++)
                Result[I] = List[I];
            return Result;
        }

        private String GetTableNameByRelationName(DataRelationCollection Relations, String RelationName, String ModuleName, String SolutionName)
        {
            foreach (DataRelation R in Relations)
            {
                if (String.Compare(R.RelationName, RelationName) == 0)
                {
                    return CliUtils.GetTableName(ModuleName, R.ChildTable.TableName, SolutionName);
                }
                else
                {
                    return GetTableNameByRelationName(R.ChildTable.ChildRelations, RelationName, ModuleName, SolutionName);
                }
            }
            return RelationName;
        }

        private String GetTableNameByBindingSource(InfoBindingSource aBindingSource, String SolutionName)
        {
            String RelationName = aBindingSource.DataMember;
            while (aBindingSource.DataSource.GetType().ToString() != typeof(InfoDataSet).ToString())
                aBindingSource = (InfoBindingSource)aBindingSource.DataSource;
            InfoDataSet aDataSet = (InfoDataSet)aBindingSource.DataSource;
            String ModuleName = aDataSet.RemoteName;
            ModuleName = ModuleName.Substring(0, ModuleName.IndexOf('.'));
            if (aDataSet.RealDataSet.Tables[0].TableName == RelationName)
            {
                return CliUtils.GetTableName(ModuleName, RelationName, SolutionName);
            }
            else
            {
                return GetTableNameByRelationName(aDataSet.RealDataSet.Tables[0].ChildRelations, RelationName, ModuleName, SolutionName);
            }
        }

        private void DoGetGridColumnList(Control aForm, ArrayList List, String SolutionName)
        {
            ArrayList ColumnList = new ArrayList();
            ArrayList GridData = new ArrayList();
            foreach (Control C in aForm.Controls)
            {
                if (C.GetType().Equals(typeof(InfoDataGridView)) || C.GetType().Equals(typeof(DataGridView)))
                {
                    DataGridView Grid = (DataGridView)C;
                    foreach (DataGridViewColumn Column in Grid.Columns)
                    {
                        if (Column.DataPropertyName != null && Column.DataPropertyName != "")
                            ColumnList.Add(Column.DataPropertyName);
                    }
                    if (Grid.DataSource != null && Grid.DataSource.ToString() != "")
                    {
                        string TableName = GetTableNameByBindingSource((InfoBindingSource)Grid.DataSource, SolutionName);
                        GridData.Add(Grid.Name + "." + TableName);
                        GridData.Add(ColumnList);
                        List.Add(GridData);
                    }
                }
                DoGetGridColumnList(C, List, SolutionName);
            }
        }

        private Object GetGridColumnList(InfoForm aForm, String SolutionName)
        {
            ArrayList List = new ArrayList();
            DoGetGridColumnList(aForm, List, SolutionName);
            Object[] Result = new Object[List.Count];
            for (int I = 0; I < List.Count; I++)
            {
                ArrayList GridData = (ArrayList)List[I];
                ArrayList ColumnList = (ArrayList)GridData[1];
                Object[] ColumnObject = new Object[ColumnList.Count];
                for (int J = 0; J < ColumnList.Count; J++)
                    ColumnObject[J] = ColumnList[J];
                Result[I] = new Object[2] { GridData[0], ColumnObject };
            }
            return Result;
        }

        private void GetDataModuleImage(ProjectItem aItem, List<String> FileList)
        {
            Window W = aItem.Open("{00000000-0000-0000-0000-000000000000}");
            Sleep(1000);
            W.Activate();

            System.ComponentModel.Design.IDesignerHost FDesignerHost =
                (System.ComponentModel.Design.IDesignerHost)W.Object;
            Component aDataModule = (Component)FDesignerHost.RootComponent;

            Srvtools.ScreenCapture B = new ScreenCapture();
            StringBuilder SB = new StringBuilder(200);
            GetTempPath(200, SB);
            string TempPath = SB.ToString();
            GetTempFileName(TempPath, "", 0, SB);
            String FileName = SB.ToString();
            FileName = System.IO.Path.ChangeExtension(FileName, "bmp");
            FDTE2.MainWindow.Activate();
            Application.DoEvents();
            IntPtr aHandle = new IntPtr(FDTE2.MainWindow.HWnd);
            B.CaptureWindowToFile(aHandle, FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            FileList.Add(FileName);

            foreach (Component C in aDataModule.Container.Components)
            {
                if (C.GetType().ToString().IndexOf("InfoTransaction") > 0)
                {
                    InfoTransaction IT = (InfoTransaction)C;
                    InfoTransactionEditor ITE = (InfoTransactionEditor)FDesignerHost.GetDesigner(IT);
                    Form aForm = ITE.ShowForm();
                    Srvtools.ScreenCapture SC = new ScreenCapture();
                    GetTempPath(200, SB);
                    TempPath = SB.ToString();
                    GetTempFileName(TempPath, "", 0, SB);
                    FileName = SB.ToString();
                    FileName = System.IO.Path.ChangeExtension(FileName, "bmp");
                    FDTE2.MainWindow.Activate();
                    Application.DoEvents();
                    aForm.Activate();
                    Application.DoEvents();
                    IntPtr bHandle = new IntPtr(aForm.Handle.ToInt32());
                    SC.CaptureWindowToFile(bHandle, FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    FileList.Add(FileName);
                }
            }
        }

        private ProjectItem FindProjectItem(ref Project aProject, String ModuleName, String ItemName)
        {
            if (aProject.Name.CompareTo(ModuleName) == 0)
            {
                foreach (ProjectItem PI in aProject.ProjectItems)
                {
                    if (PI.Name.CompareTo(ItemName) == 0)
                        return PI;
                }
                return null;
            }
            else if (aProject.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
            {
                foreach (ProjectItem PI in aProject.ProjectItems)
                {
                    if (PI.SubProject != null)
                    {
                        Project SubProject = PI.SubProject;
                        ProjectItem aItem = FindProjectItem(ref SubProject, ModuleName, ItemName);
                        if (aItem != null)
                        {
                            aProject = SubProject;
                            return aItem;
                        }
                    }
                }
                return null;
            }
            else
                return null;
        }

        private String GetDataModuleSourceFile(String SolutionFileName, String ModuleName)
        {
            if (FDTE2.Solution == null || FDTE2.Solution.FileName.CompareTo(SolutionFileName) != 0)
                FDTE2.Solution.Open(SolutionFileName);
            ModuleName = WzdUtils.GetToken(ref ModuleName, new char[] { '.' });
            List<String> FileList = new List<string>();
            foreach (Project P in FDTE2.Solution.Projects)
            {
                Project aProject = P;
                String CodeName = "Component.cs";
                ProjectItem PI = FindProjectItem(ref aProject, ModuleName, CodeName);
                if (PI == null)
                {
                    CodeName = "Component.vb";
                    PI = FindProjectItem(ref aProject, ModuleName, CodeName);
                }
                if (PI != null)
                {
                    return System.IO.Path.GetDirectoryName(aProject.FileName) + @"\" + CodeName;
                }
            }

            return "";
        }

        private Object[] CaptureDataModule(String SolutionFileName, String ModuleName,
            ArrayList InfoCommandList, ArrayList UpdateComponentList)
        {
            //Capture DataModule
            if (FDTE2.Solution == null || FDTE2.Solution.FileName.CompareTo(SolutionFileName) != 0)
                FDTE2.Solution.Open(SolutionFileName);
            ModuleName = WzdUtils.GetToken(ref ModuleName, new char[] { '.' });
            List<String> FileList = new List<string>();
            foreach (Project P in FDTE2.Solution.Projects)
            {
                Project aProject = P;
                ProjectItem PI = FindProjectItem(ref aProject, ModuleName, "Component.cs");
                if (PI == null)
                    PI = FindProjectItem(ref aProject, ModuleName, "Component.vb");
                if (PI != null)
                {
                    //???
                    Window W = null;
                    if (InfoCommandList != null)
                    {
                        W = PI.Open("{00000000-0000-0000-0000-000000000000}");
                        W.Activate();
                        IDesignerHost FDesignerHost = (IDesignerHost)W.Object;
                        DataModule FRootForm = (DataModule)FDesignerHost.RootComponent;
                        foreach (Component C in FRootForm.Container.Components)
                        {
                            if (C.GetType().Equals(typeof(InfoCommand)))
                            {
                                InfoCommand aCommand = C as InfoCommand;
                                InfoCommandList.Add(new Object[] { aCommand.Name, aCommand.CommandText });
                            }
                            if (C.GetType().Equals(typeof(UpdateComponent)))
                            {
                                UpdateComponent aUpdate = C as UpdateComponent;
                                int Index2 = 0;
                                Object[] FieldAttrs = new Object[aUpdate.FieldAttrs.Count];
                                foreach (FieldAttr aAttr in aUpdate.FieldAttrs)
                                {
                                    FieldAttrs[Index2] = new Object[] { aAttr.DataField, aAttr.DefaultMode.ToString(), aAttr.DefaultValue, aAttr.CheckNull.ToString() };
                                    Index2++;
                                }
                                UpdateComponentList.Add(new Object[] { aUpdate.Name, FieldAttrs });
                            }
                        }
                    }

                    GetDataModuleImage(PI, FileList);
                    if (W != null)
                        W.Close(vsSaveChanges.vsSaveChangesNo);
                    break;
                }

                /*
                if (String.Compare(P.Name, ModuleName) == 0)
                {
                    foreach (ProjectItem PI in P.ProjectItems)
                    {
                        if (String.Compare(PI.Name, "Component.cs") == 0)
                        {
                            Window W = PI.Open("{00000000-0000-0000-0000-000000000000}");
                            W.Activate();

                            System.ComponentModel.Design.IDesignerHost FDesignerHost = 
                                (System.ComponentModel.Design.IDesignerHost)W.Object;
                            Component aDataModule = (Component)FDesignerHost.RootComponent;

                            Srvtools.ScreenCapture B = new ScreenCapture();
                            StringBuilder SB = new StringBuilder(200);
                            GetTempPath(200, SB);
                            string TempPath = SB.ToString();
                            GetTempFileName(TempPath, "", 0, SB);
                            String FileName = SB.ToString();
                            FileName = System.IO.Path.ChangeExtension(FileName, "bmp");
                            FDTE2.MainWindow.Activate();
                            Application.DoEvents();
                            IntPtr aHandle = new IntPtr(FDTE2.MainWindow.HWnd);
                            B.CaptureWindowToFile(aHandle, FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            FileList.Add(FileName);

                            foreach (Component C in aDataModule.Container.Components)
                            {
                                if (C.GetType().ToString().IndexOf("InfoTransaction") > 0)
                                {
                                    InfoTransaction IT = (InfoTransaction)C;
                                    InfoTransactionEditor ITE = (InfoTransactionEditor)FDesignerHost.GetDesigner(IT);
                                    Form aForm = ITE.ShowForm();
                                    Srvtools.ScreenCapture SC = new ScreenCapture();
                                    GetTempPath(200, SB);
                                    TempPath = SB.ToString();
                                    GetTempFileName(TempPath, "", 0, SB);
                                    FileName = SB.ToString();
                                    FileName = System.IO.Path.ChangeExtension(FileName, "bmp");
                                    FDTE2.MainWindow.Activate();
                                    Application.DoEvents();
                                    aForm.Activate();
                                    Application.DoEvents();
                                    IntPtr bHandle = new IntPtr(aForm.Handle.ToInt32());
                                    SC.CaptureWindowToFile(bHandle, FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    FileList.Add(FileName);
                                }
                            }
                        }
                    }
                }
                */
            }

            Object[] Result = new Object[FileList.Count];
            int Index = 0;
            foreach (String S in FileList)
                Result[Index++] = S;

            return Result;
        }

        [DllImport("kernel32.dll")]
        static extern uint GetTempFileName(string lpPathName, string lpPrefixString,
           uint uUnique, [Out] StringBuilder lpTempFileName);
        [DllImport("kernel32.dll")]
        static extern uint GetTempPath(uint nBufferLength,
           [Out] StringBuilder lpBuffer);
        [DllImport("kernel32.dll")]
        static extern void Sleep(uint msec);
        [DllImport("user32.dll")]
        static extern IntPtr SetActiveWindow(IntPtr wHandle);
        public Object GetFormImage(object Params)
        {
            FDTE2.MainWindow.WindowState = vsWindowState.vsWindowStateMinimize;
            FDTE2.MainWindow.WindowState = vsWindowState.vsWindowStateMaximize;
            FDTE2.MainWindow.Activate();
            FDTE2.MainWindow.SetFocus();
            object[] RealParams = (object[])Params;
            string DllFileName = RealParams[0].ToString();
            string FormName = RealParams[1].ToString();
            string DllName = DllFileName;
            string UserID = RealParams[2].ToString();
            string Password = RealParams[3].ToString();
            string DBName = RealParams[4].ToString();
            string SiteCode = RealParams[5].ToString();
            string CurrentProject = RealParams[6].ToString();
            String SolutionFileName = RealParams[7].ToString();
            CheckRemotingObject(UserID, Password, DBName, SiteCode, CurrentProject);
            Assembly A;
            InfoForm aForm = null;
            Object TextBoxList = null;
            Object GridColumnList = null;
            Object DefaultValidateList = null;
            ArrayList InfoCommandList = new ArrayList();
            ArrayList UpdateComponentList = new ArrayList();
            Object[] Tables = null;
            Object[] DataModuleFileList = null;
            try
            {
                DllName = System.IO.Path.GetFileName(DllFileName);
                DllName = DllName.Substring(0, DllName.IndexOf('.', 1));
                //if (System.IO.File.Exists(DllFileName))
                {
                    A = Assembly.LoadFrom(DllFileName);
                    Type myType = A.GetType(DllName + "." + FormName);
                    if (myType != null)
                    {
                        try
                        {
                            object obj = Activator.CreateInstance(myType);
                            aForm = (InfoForm)obj;
                            InfoDataSet Master = aForm.GetIntfObject(typeof(IInfoDataSet)) as InfoDataSet;
                            //TableRelation = GetTableRelation(Master);
                            String ModuleName = Master.RemoteName;
                            if (ModuleName != "")
                            {
                                ModuleName = ModuleName.Substring(0, ModuleName.IndexOf('.'));
                                String DataSetName = Master.RemoteName;
                                DataSetName = DataSetName.Substring(DataSetName.IndexOf('.') + 1, DataSetName.Length - DataSetName.IndexOf('.') - 1);
                                DataSet aDataSet = CliUtils.GetSqlCommand(ModuleName, DataSetName, Master, "", CurrentProject, "");
                                String TableName = GetTableNameByProvider(CurrentProject, ModuleName, DataSetName);
                                Tables = new Object[2];
                                Tables[0] = TableName;
                                Object[] ChildRelation = new Object[aDataSet.Tables[0].ChildRelations.Count];
                                Tables[1] = ChildRelation;
                                GetRealChildTable(CurrentProject, ModuleName, aDataSet.Tables[0].ChildRelations, ref ChildRelation);
                                //Capture Form
                                TextBoxList = GetTextBoxList(aForm, CurrentProject);
                                GridColumnList = GetGridColumnList(aForm, CurrentProject);
                                DefaultValidateList = GetDefaultValidateList(aForm, CurrentProject);
                            }
                            aForm.Show();
                            FDTE2.MainWindow.WindowState = vsWindowState.vsWindowStateMinimize;
                            Application.DoEvents();
                            FDTE2.MainWindow.WindowState = vsWindowState.vsWindowStateMaximize;
                            IntPtr MHandle = new IntPtr(FDTE2.MainWindow.HWnd);
                            SetActiveWindow(MHandle);
                            Application.DoEvents();
                            FDTE2.MainWindow.Visible = false;
                            Application.DoEvents();
                            FDTE2.MainWindow.Visible = true;
                            Application.DoEvents();
                            FDTE2.MainWindow.Activate();
                            Application.DoEvents();
                            FDTE2.MainWindow.Activate();
                            Application.DoEvents();
                            aForm.Activate();
                            Application.DoEvents();
                            Srvtools.ScreenCapture B = new ScreenCapture();
                            StringBuilder SB = new StringBuilder(200);
                            GetTempPath(200, SB);
                            string TempPath = SB.ToString();
                            GetTempFileName(TempPath, "", 0, SB);
                            string FileName = SB.ToString();
                            FileName = System.IO.Path.ChangeExtension(FileName, "bmp");
                            B.CaptureWindowToFile(aForm.Handle, FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            aForm.Dispose();
                            if (ModuleName != "")
                                DataModuleFileList = CaptureDataModule(SolutionFileName, ModuleName, InfoCommandList, UpdateComponentList);
                            Object[] aCommandList = new Object[InfoCommandList.Count];
                            for (int I = 0; I < aCommandList.Length; I++)
                                aCommandList[I] = InfoCommandList[I];
                            Object[] aUpdateList = new Object[UpdateComponentList.Count];
                            for (int I = 0; I < aUpdateList.Length; I++)
                                aUpdateList[I] = UpdateComponentList[I];
                            return new object[] { 1, new Object[] { FileName, Tables, TextBoxList, GridColumnList, 
                                DataModuleFileList, DefaultValidateList, aCommandList, aUpdateList } };
                        }
                        finally
                        {
                            aForm.Dispose();
                        }
                    }
                    else
                    {
                        return new Object[] { -1, String.Format("Cannot get form[%1] from dll[%2]", FormName, DllName) };
                    }
                }
            }
            catch (Exception E)
            {
                return new Object[] { -1, E.Message };
            }
        }

        private Window FDesignWindow = null;

        public System.Web.UI.Page GetWebPage(ProjectItems PIs, String PageName, Object[] FolderOffset)
        {
            if (PIs == null)
                return null;
            if (FolderOffset.Length > 0)
            {
                String TargetFolder = (String)FolderOffset[0];
                foreach (ProjectItem PI in PIs)
                {
                    if (PI.Name.CompareTo(TargetFolder) == 0)
                    {
                        if (FolderOffset.Length == 1)
                        {
                            Object[] NewFolderOffset = new Object[0];
                            return GetWebPage(PI.ProjectItems, PageName, NewFolderOffset);
                        }
                        else
                        {
                            Object[] NewFolderOffset = new Object[FolderOffset.Length - 1];
                            for (int I = 0; I < FolderOffset.Length - 1; I++)
                                NewFolderOffset[I] = FolderOffset[I + 1];
                            return GetWebPage(PI.ProjectItems, PageName, NewFolderOffset);
                        }
                    }
                }
            }
            else
            {
                foreach (ProjectItem PI in PIs)
                {
                    if (PI.Name.CompareTo(PageName) == 0)
                    {
                        //PI.DTE.ExecuteCommand("View.ViewDesigner", String.Empty);
                        FDesignWindow = PI.Open("{00000000-0000-0000-0000-000000000000}");
                        FDesignWindow.Activate();
                        FDesignWindow = PI.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
                        FDesignWindow.Activate();
                        //FHtmlDocument = (HtmlDocument)aW.Document;
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
                                return (System.Web.UI.Page)FDesignerHost.RootComponent;
                            }
                            finally
                            {
                                Marshal.Release(pObject);
                            }
                        }
                    }
                }
            }
            return null;
        }

        private void GetDDInfo(ref String TableName, ref String FieldName, WebDataSource DataSource)
        {
            foreach (DataSouceDDInfomationsItem DDItem in DataSource.DDInfomations)
            {
                if (DDItem.ColumnName.CompareTo(FieldName) == 0)
                {
                    if (!String.IsNullOrEmpty(DDItem.RealTableName))
                    {
                        TableName = DDItem.RealTableName;
                        if (TableName.IndexOf('.') > -1)
                        {
                            WzdUtils.GetToken(ref TableName, new Char[] { '.' });
                        }
                    }
                    if (!String.IsNullOrEmpty(DDItem.RealColumnName))
                        FieldName = DDItem.RealColumnName;
                }
            }
            return;

            //DataSouceDDInfomationsItem DDItem = DataSource.DDInfomations.GetItem(FieldName) as DataSouceDDInfomationsItem;
            //if (DDItem != null)
            //{
            //    if (!String.IsNullOrEmpty(DDItem.RealTableName))
            //        TableName = DDItem.RealTableName;
            //    if (!String.IsNullOrEmpty(DDItem.RealColumnName))
            //        FieldName = DDItem.RealColumnName;
            //}
        }

        private WebDataSource GetDataSource(System.Web.UI.Page aPage, String ID)
        {
            foreach (System.Web.UI.Control C in aPage.Controls)
                if (!String.IsNullOrEmpty(C.ID))
                    if (C.ID.CompareTo(ID) == 0)
                        return C as WebDataSource;
            return null;
        }

        private void GetWebGridView(WebGridView aGridView, ArrayList aGridViewList, String SolutionName, System.Web.UI.Page aPage)
        {
            ArrayList FieldList = new ArrayList();
            WebDataSource DataSource = GetDataSource(aPage, aGridView.DataSourceID);
            String TableName = "";
            TableName = aGridView.DataMember;
            if (TableName == null || TableName == "")
            {
                foreach (System.Web.UI.Control C in aPage.Controls)
                    if (C.ID == aGridView.DataSourceID)
                        TableName = ((WebDataSource)C).DataMember;
            }
            foreach (System.Web.UI.WebControls.DataControlField DCF in aGridView.Columns)
            {
                if (DCF.GetType().Equals(typeof(System.Web.UI.WebControls.BoundField)))
                {
                    String FieldName = DCF.SortExpression;
                    TableName = DataSource.DataMember;
                    GetDDInfo(ref TableName, ref FieldName, DataSource);
                    FieldList.Add(TableName + "." + FieldName + "." + DCF.HeaderText);
                }
            }
            Object[] Result = new Object[FieldList.Count];
            for (int I = 0; I < FieldList.Count; I++)
                Result[I] = FieldList[I];
            aGridViewList.Add(new Object[] { aGridView.ID + "." + TableName, Result, "WebGridView" });
        }

        public void GetWebFormView(WebFormView aFormView, ArrayList aGridViewList, System.Web.UI.Page aPage)
        {
            ArrayList FieldList = new ArrayList();
            String TableName = aFormView.DataMember;

            foreach (System.Web.UI.Control C in aPage.Controls)
                if (C.ID == aFormView.DataSourceID)
                    TableName = ((WebDataSource)C).DataMember;

            WebFormViewDesigner aDesigner = (WebFormViewDesigner)FDesignerHost.GetDesigner(aFormView);
            if (aDesigner == null)
                return;

            WebDataSource DataSource = GetDataSource(aPage, aFormView.DataSourceID);
            foreach (System.Web.UI.Design.TemplateGroup T in aDesigner.TemplateGroups)
            {
                foreach (System.Web.UI.Design.TemplateDefinition TD in T.Templates)
                {
                    if (TD.Name == "ItemTemplate")
                    {
                        StringBuilder builder = new StringBuilder();
                        string content = TD.Content;
                        if (content == null || content.Length == 0)
                            continue;
                        string[] ctrlTexts = content.Split("\r\n".ToCharArray());
                        List<string> lists = new List<string>();
                        foreach (string ctrlText in ctrlTexts)
                        {
                            if (ctrlText != null && ctrlText.Length != 0)
                            {
                                System.Web.UI.Control ctrl = null;
                                try
                                {
                                    ctrl = System.Web.UI.Design.ControlParser.ParseControl(FDesignerHost, ctrlText);
                                }
                                catch
                                {
                                    continue;
                                }
                                if (ctrl == null || ctrl.ID == null || ctrl.ID == "\"\"")
                                    continue;
                                if (ctrl is System.Web.UI.WebControls.Label)
                                {
                                    System.Web.UI.WebControls.Label aLabel = ctrl as System.Web.UI.WebControls.Label;
                                    if (!String.IsNullOrEmpty(aLabel.Text))
                                    {
                                        String ID = aLabel.ID;
                                        if (ID.Length >= 7)
                                        {
                                            if (ID.Substring(0, 7).CompareTo("Caption") == 0)
                                                ID = ID.Substring(7);
                                        }
                                        String Text = aLabel.Text;
                                        if (Text[Text.Length - 1] == ':')
                                            Text = Text.Substring(0, Text.Length - 1);
                                        TableName = DataSource.DataMember;
                                        String FieldName = ID;
                                        GetDDInfo(ref TableName, ref FieldName, DataSource);
                                        FieldList.Add(TableName + "." + FieldName + "." + Text);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Object[] Result = new Object[FieldList.Count];
            for (int I = 0; I < FieldList.Count; I++)
                Result[I] = FieldList[I];
            aGridViewList.Add(new Object[] { aFormView.ID + "." + TableName, Result, "WebFormView" });
        }

        public void GetWebDetailsView(WebDetailsView aGridView, ArrayList aDataGridViewList, String SolutionName, System.Web.UI.Page aPage)
        {
            ArrayList FieldList = new ArrayList();
            WebDataSource DataSource = GetDataSource(aPage, aGridView.DataSourceID);
            String TableName = "";
            foreach (System.Web.UI.WebControls.DataControlField DCF in aGridView.Fields)
            {
                if (DCF.GetType().Equals(typeof(System.Web.UI.WebControls.BoundField)))
                {
                    TableName = DataSource.DataMember;
                    String FieldName = DCF.SortExpression;
                    GetDDInfo(ref TableName, ref FieldName, DataSource);
                    FieldList.Add(TableName + "." + FieldName + "." + DCF.HeaderText);
                }
            }
            Object[] Result = new Object[FieldList.Count];
            for (int I = 0; I < FieldList.Count; I++)
                Result[I] = FieldList[I];
            aDataGridViewList.Add(new Object[] { aGridView.ID + "." + TableName, Result, "WebDetailsView" });
        }

        private int FindValidateItem(String DataSourceID, String FieldName, ArrayList ValidateList)
        {
            for (int I = 0; I < ValidateList.Count; I++)
            {
                Object[] Item = ValidateList[I] as Object[];
                if (Item[0].ToString().CompareTo(DataSourceID) == 0)
                    if (Item[1].ToString().CompareTo(FieldName) == 0)
                        return I;
            }
            return -1;
        }

        private void GetWebDefault(WebDefault aDefault, ArrayList ValidateList)
        {
            foreach (DefaultFieldItem DefItem in aDefault.Fields)
            {
                int Index = FindValidateItem(aDefault.DataSourceID, DefItem.FieldName, ValidateList);
                if (Index == -1)
                {
                    Object[] Item = new Object[] {aDefault.DataSourceID, DefItem.FieldName, "", "",
                        DefItem.CarryOn.ToString(), DefItem.DefaultValue};
                    ValidateList.Add(Item);
                }
                else
                {
                    Object[] Item = ValidateList[Index] as Object[];
                    Item[4] = DefItem.CarryOn.ToString();
                    Item[5] = DefItem.DefaultValue;
                }
            }
        }

        private void GetWebValidate(WebValidate aValidate, ArrayList ValidateList)
        {
            foreach (ValidateFieldItem ValItem in aValidate.Fields)
            {
                int Index = FindValidateItem(aValidate.DataSourceID, ValItem.FieldName, ValidateList);
                if (Index == -1)
                {
                    Object[] Item = new Object[] { aValidate.DataSourceID, ValItem.FieldName, ValItem.CheckNull.ToString(),
                        ValItem.Validate, "", ""};
                    ValidateList.Add(Item);
                }
                else
                {
                    Object[] Item = ValidateList[Index] as Object[];
                    Item[2] = ValItem.CheckNull.ToString();
                    Item[3] = ValItem.Validate;
                }
            }
        }

        private void RenameNoneLoginTag(String PageName, Object[] FolderOffset, String UserID, String Password,
            String DBName, String SolutionName)
        {
            String FileName = EEPRegistry.WebClient;

            String BackupFileName = FileName + @"\NoneLogin.aspx.cs.txt";
            FileName = FileName + @"\NoneLogin.aspx.cs";
            if (!File.Exists(BackupFileName))
                return;
            System.IO.File.Copy(BackupFileName, FileName, true);
            System.IO.StreamReader SR = new System.IO.StreamReader(FileName);
            String Context = SR.ReadToEnd();
            SR.Close();
            Context = Context.Replace("TAG_USERID", UserID);
            Context = Context.Replace("TAG_PASSWORD", Password);
            Context = Context.Replace("TAG_DBNAME", DBName);
            Context = Context.Replace("TAG_SOLUTION", SolutionName);
            String FixupPageName = "";
            foreach (Object O in FolderOffset)
                FixupPageName = FixupPageName + (String)O + @"\";
            FixupPageName = FixupPageName + PageName;
            Context = Context.Replace("TAG_PAGENAME", FixupPageName);
            System.IO.FileStream Filefs = new System.IO.FileStream(FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            System.IO.StreamWriter SW = new System.IO.StreamWriter(Filefs);
            SW.Write(Context);
            SW.Close();
            Filefs.Close();
        }

        public String GetPageImage(String WebSiteName, String PageName, Object[] FolderOffset)
        {
            String FileName = "";
            FDTE2.Windows.Item(Constants.vsWindowKindSolutionExplorer).Activate();
            UIHierarchy A = (UIHierarchy)FDTE2.ActiveWindow.Object;

            foreach (UIHierarchyItem aItem in A.UIHierarchyItems)
            {
                foreach (UIHierarchyItem B in aItem.UIHierarchyItems)
                {
                    if (B.Name.CompareTo(WebSiteName) == 0)
                    {
                        foreach (UIHierarchyItem C in B.UIHierarchyItems)
                        {
                            if (C.Name.CompareTo(PageName) == 0)
                            {
                                C.Select(vsUISelectionType.vsUISelectionTypeSelect);
                                try
                                {
                                    FDTE2.MainWindow.Activate();
                                    FDTE2.ActiveWindow.Activate();
                                    C.DTE.ExecuteCommand("File.ViewinBrowser", String.Empty);
                                    Sleep(15000);

                                    Srvtools.ScreenCapture SC = new ScreenCapture();
                                    StringBuilder SB = new StringBuilder(200);
                                    GetTempPath(200, SB);
                                    string TempPath = SB.ToString();
                                    GetTempFileName(TempPath, "", 0, SB);
                                    FileName = SB.ToString();
                                    FileName = System.IO.Path.ChangeExtension(FileName, "bmp");
                                    IntPtr aHandle = new IntPtr(FDTE2.MainWindow.HWnd);
                                    //B.CaptureWindowToFile(aHandle, FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    SC.CaptureScreenToFile(FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
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
            return FileName;
        }

        public String GetPageImage2(String WebSiteName, String PageName, Object[] FolderOffset, String UserID,
            String Password, String DBName, String SolutionName, String PrintWaitingTime, String PageTitle)
        {
            String FileName = "";
            FDTE2.Windows.Item(Constants.vsWindowKindSolutionExplorer).Activate();
            UIHierarchy A = (UIHierarchy)FDTE2.ActiveWindow.Object;
            RenameNoneLoginTag(PageName, FolderOffset, UserID, Password, DBName, SolutionName);
            if (PrintWaitingTime == "")
                PrintWaitingTime = "5";

            foreach (UIHierarchyItem aItem in A.UIHierarchyItems)
            {
                foreach (UIHierarchyItem B in aItem.UIHierarchyItems)
                {
                    if (B.Name.CompareTo(WebSiteName) == 0)
                    {
                        foreach (UIHierarchyItem C in B.UIHierarchyItems)
                        {
                            if (C.Name.CompareTo("NoneLogin.aspx") == 0)
                            {
                                C.Select(vsUISelectionType.vsUISelectionTypeSelect);
                                try
                                {
                                    FDTE2.MainWindow.Activate();
                                    FDTE2.ActiveWindow.Activate();
                                    C.DTE.ExecuteCommand("File.ViewinBrowser", String.Empty);
                                    //Sleep((uint)int.Parse(PrintWaitingTime) * 1000);
                                    Srvtools.ScreenCapture SC = new ScreenCapture();
                                    StringBuilder SB = new StringBuilder(200);
                                    GetTempPath(200, SB);
                                    string TempPath = SB.ToString();
                                    GetTempFileName(TempPath, "", 0, SB);
                                    FileName = SB.ToString();
                                    FileName = System.IO.Path.ChangeExtension(FileName, "bmp");
                                    IntPtr aHandle = new IntPtr(FDTE2.MainWindow.HWnd);
                                    //B.CaptureWindowToFile(aHandle, FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    String WindowName = String.Format(PageTitle + " - Microsoft Internet Explorer", WebSiteName, FolderOffset[0], PageName); //String WindowName = String.Format(@"http://localhost:1130/EEPWebClient/{1}/{2} - Microsoft Internet Explorer", WebSiteName, FolderOffset[0], PageName);
                                    IntPtr IEHandle = FindWindow(null, WindowName);
                                    int WaitCount = 0;
                                    while (IEHandle.ToInt32() == 0)
                                    {
                                        Sleep(50);
                                        IEHandle = FindWindow(null, WindowName);
                                        WaitCount++;
                                        if (WaitCount > 400) //20秒
                                            break;
                                    }
                                    SetActiveWindow(IEHandle);
                                    Sleep(uint.Parse(PrintWaitingTime) * 1000); //開完IE的等待時間
                                    //SC.CaptureScreenToFile(FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    //IntPtr IEHandle = FindWindow(null, "Untitled Page - Microsoft Internet Explorer");
                                    SC.CaptureWindowToFile(IEHandle, FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    PostMessage(IEHandle, 16, 0, 0);
                                    break;
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
            return FileName;
        }

        private System.Web.UI.UpdatePanel FUpdatePanel;
        private void GetPageControls(System.Web.UI.ControlCollection ParentControl, ArrayList ControlList,
            ArrayList ValidateList, ArrayList RemoteNameList, String SolutionFileName, System.Web.UI.Page RootPage, ArrayList InfoCommandList, ArrayList UpdateComponentList, ArrayList DataModuleList)
        {
            foreach (System.Web.UI.Control ChildControl in ParentControl)
            {
                if (ChildControl is WebDataSource)
                {
                    if (RemoteNameList.IndexOf((ChildControl as WebDataSource).RemoteName) < 0)
                    {
                        if (!String.IsNullOrEmpty((ChildControl as WebDataSource).RemoteName))
                        {
                            RemoteNameList.Add((ChildControl as WebDataSource).RemoteName);
                            String S = (ChildControl as WebDataSource).RemoteName;
                            Object[] TempList = CaptureDataModule(SolutionFileName, S, InfoCommandList, UpdateComponentList);
                            foreach (Object O in TempList)
                                DataModuleList.Add(O);
                        }
                    }
                }
                if (ChildControl is WebGridView)
                {
                    GetWebGridView((WebGridView)ChildControl, ControlList, SolutionFileName, RootPage);
                }
                if (ChildControl is WebFormView)
                {
                    GetWebFormView((WebFormView)ChildControl, ControlList, RootPage);
                }
                if (ChildControl is WebDetailsView)
                {
                    GetWebDetailsView((WebDetailsView)ChildControl, ControlList, SolutionFileName, RootPage);
                }
                if (ChildControl is WebDefault)
                {
                    GetWebDefault((WebDefault)ChildControl, ValidateList);
                }
                if (ChildControl is WebValidate)
                {
                    GetWebValidate((WebValidate)ChildControl, ValidateList);
                }
                if (ChildControl is System.Web.UI.UpdatePanel)
                {
                    FUpdatePanel = ChildControl as System.Web.UI.UpdatePanel;
                    GetPageControls((ChildControl as System.Web.UI.UpdatePanel).ContentTemplateContainer.Controls, ControlList, ValidateList, RemoteNameList, SolutionFileName, RootPage, InfoCommandList, UpdateComponentList, DataModuleList);
                }
                if (ChildControl is System.Web.UI.WebControls.Panel || ChildControl is System.Web.UI.WebControls.MultiView
                    || ChildControl is System.Web.UI.WebControls.View)
                {
                    GetPageControls(ChildControl.Controls, ControlList, ValidateList, RemoteNameList, SolutionFileName, RootPage, InfoCommandList, UpdateComponentList, DataModuleList);
                }
            }
        }

        public Object GetPageInfo(Object Params)
        {
            //SrvUtils.IsDesignMode = false;
            try
            {
                FDTE2.MainWindow.WindowState = vsWindowState.vsWindowStateMinimize;
                FDTE2.MainWindow.WindowState = vsWindowState.vsWindowStateMaximize;
                FDTE2.MainWindow.Activate();
                FDTE2.MainWindow.SetFocus();
                Object[] RealParams = (object[])Params;
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
                String PageTitle = RealParams[10].ToString();

                if (FDTE2.Solution == null || FDTE2.Solution.FileName.CompareTo(SolutionFileName) != 0)
                    FDTE2.Solution.Open(SolutionFileName);

                FDTE2.MainWindow.Activate();

                //Get Page Controls  

                ArrayList WebControlList = new ArrayList();
                ArrayList ValidateList = new ArrayList();
                ArrayList RemoteNameList = new ArrayList();
                ArrayList InfoCommandList = new ArrayList();
                ArrayList UpdateComponentList = new ArrayList();
                ArrayList DataModuleFileList = new ArrayList();
                FDesignWindow = null;
                foreach (Project P in FDTE2.Solution.Projects)
                {
                    if (P.Name.CompareTo(WebSitePath) == 0)
                    {
                        System.Web.UI.Page aPage = GetWebPage(P.ProjectItems, PageName, FolderOffset); //???
                        GetPageControls(aPage.Controls, WebControlList, ValidateList, RemoteNameList, SolutionFileName,
                            aPage, InfoCommandList, UpdateComponentList, DataModuleFileList);
                    }
                }

                //Capture Page Image
                String FileName = GetPageImage2(WebSiteName, PageName, FolderOffset, UserID, Password, DBName,
                    Solutionname, PrintWaitingTime, PageTitle);

                if (FDesignWindow != null)
                    FDesignWindow.Close(vsSaveChanges.vsSaveChangesNo);

                Object[] WebControlListObject = new Object[WebControlList.Count];
                for (int I = 0; I < WebControlList.Count; I++)
                    WebControlListObject[I] = WebControlList[I];
                Object[] ValidateListObject = new Object[ValidateList.Count];
                for (int I = 0; I < ValidateList.Count; I++)
                    ValidateListObject[I] = ValidateList[I];
                Object[] RemoteNameListObject = new Object[RemoteNameList.Count];
                for (int I = 0; I < RemoteNameList.Count; I++)
                    RemoteNameListObject[I] = (String)RemoteNameList[I] + ";" + GetDataModuleSourceFile(SolutionFileName, (String)RemoteNameList[I]);
                Object[] InfoCommandObject = new Object[InfoCommandList.Count];
                for (int I = 0; I < InfoCommandList.Count; I++)
                    InfoCommandObject[I] = InfoCommandList[I];
                Object[] UpdateComponentObject = new Object[UpdateComponentList.Count];
                for (int I = 0; I < UpdateComponentList.Count; I++)
                    UpdateComponentObject[I] = UpdateComponentList[I];
                Object[] DataModuleFileObject = new Object[DataModuleFileList.Count];
                for (int I = 0; I < DataModuleFileList.Count; I++)
                    DataModuleFileObject[I] = DataModuleFileList[I];

                //SrvUtils.IsDesignMode = true;
                return new Object[] { 1, new object[] { FileName, WebControlListObject, ValidateListObject, 
                    RemoteNameListObject, InfoCommandObject, UpdateComponentObject, DataModuleFileObject } };
            }
            catch (Exception E)
            {
                return new Object[] { -1, E.Message };
            }
        }

        public Object DeletePackage(Object Params)
        {
            object[] RealParams = (object[])Params;
            string SolutionFileName = RealParams[0].ToString();
            string ProjectName = RealParams[1].ToString();
            if (SolutionFileName == "" || ProjectName == "")
                return null;
            try
            {
                FDTE2.Solution.Open(SolutionFileName);
                foreach (Project P in FDTE2.Solution.Projects)
                {
                    if (string.Compare(P.Name, ProjectName) == 0)
                    {
                        FDTE2.Solution.Remove(P);
                        FDTE2.Solution.SaveAs(FDTE2.Solution.FileName);
                        break;
                    }
                }
                return new Object[] { 1, "Success" };
            }
            catch (Exception E)
            {
                return new Object[] { -1, E.Message };
            }
        }

        public Object TestVisualStudio2005(object Params)
        {
            return new Object[] { 1, "Success" };
        }

        public Object GetProvider(object Params)
        {
            Object[] RealParams = (Object[])Params;
            string SolutionName = RealParams[0].ToString();
            EEPRemoteModule remoteObject = new EEPRemoteModule();
            object[] myRet = remoteObject.GetSqlCommandList(new object[] { (object)"", (object)"", (object)"", (object)"", (object)"", (object)"", (object)SolutionName });
            if ((null != myRet))
            {
                if (0 == (int)(myRet[0]))
                {
                    string[] sList = (string[])(myRet[1]);
                    Object[] Result = new Object[sList.Length];
                    for (int I = 0; I < sList.Length; I++)
                    {
                        Result[I] = sList[I];
                    }
                    return new Object[] { 1, Result };
                }
                else
                {
                    return new Object[] { -1, "Get provider fail" };
                }
            }
            else
                return new Object[] { -1, "Get provider fail" };
        }

        private String GetTableNameByProvider(String SolutionName, String ModuleName, String DataSetName)
        {
            EEPRemoteModule remoteObject = new EEPRemoteModule();
            Object[] CallParams = new object[] { (object)"", (object)"", (object)"", (object)"", (object)"", (object)"", (object)SolutionName, (Object)"" };
            return CliUtils.GetTableName(ModuleName, DataSetName, SolutionName);
            //if (null != aryRet)
            //{
            //    if (0 == (int)(aryRet[0]))
            //        return (string)(aryRet[1]);
            //    else
            //        return "";
            //}
            //else 
            //    return "";
        }

        private void GetRealChildTable(String SolutionName, String ModuleName, DataRelationCollection Relations, ref Object[] Tables)
        {
            int Index = 0;
            foreach (DataRelation R in Relations)
            {
                Object[] Child = new Object[2];
                String DataSetName = R.ChildTable.TableName;
                String TableName = CliUtils.GetTableName(ModuleName, DataSetName, SolutionName); //GetTableNameByProvider(SolutionName, ModuleName, DataSetName);
                Child[0] = TableName;
                Object[] ChildRelation = new Object[R.ChildTable.ChildRelations.Count];
                Child[1] = ChildRelation;
                Tables[Index++] = Child;
                GetRealChildTable(SolutionName, ModuleName, R.ChildTable.ChildRelations, ref ChildRelation);
            }
        }

        public Object GetTableRelationByProvider(object Params)
        {
            try
            {
                Object[] RealParams = (Object[])Params;
                String SolutionName = RealParams[0].ToString();
                String RemoteName = RealParams[1].ToString();
                String DataSetName = RemoteName.Substring(RemoteName.IndexOf('.') + 1, RemoteName.Length - RemoteName.IndexOf('.') - 1);
                String ModuleName = RemoteName.Substring(0, RemoteName.IndexOf('.'));

                InfoDataSet aInfoDataSet = new InfoDataSet();
                aInfoDataSet.PacketRecords = -1;

                DataSet aDataSet = CliUtils.GetSqlCommand(ModuleName, DataSetName, aInfoDataSet, "", SolutionName, "");

                //String TableName = GetTableNameByProvider(SolutionName, ModuleName, DataSetName);
                String TableName = CliUtils.GetTableName(ModuleName, DataSetName, SolutionName);
                Object[] Tables = new Object[2];
                Tables[0] = TableName;
                Object[] ChildRelation = new Object[aDataSet.Tables[0].ChildRelations.Count];
                Tables[1] = ChildRelation;
                GetRealChildTable(SolutionName, ModuleName, aDataSet.Tables[0].ChildRelations, ref ChildRelation);
                return new Object[] { 1, Tables };
            }
            catch (Exception E)
            {
                return new Object[] { -1, E.Message };
            }
        }

        public delegate void TGenCode(string XML);

        [DllImport("User32.DLL")]
        public static extern IntPtr GetActiveWindow();

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int
            nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, System.Int32 dwRop);

        [DllImport("User32.dll")]
        public extern static System.IntPtr GetDC(System.IntPtr hWnd);

        [DllImport("User32.dll")]
        public extern static int ReleaseDC(System.IntPtr hWnd, System.IntPtr hDC);
        //modified to include hWnd

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //   a := FindWindow(nil, PChar('Untitled Page - Microsoft Internet Explorer'));
        //   a := FindWindow(PChar('IEFrame'), nil);
        //   PostMessage(a, WM_CLOSE, 0, 0);

        [DllImport("User32.Dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private void Capture(String FileName)
        {
            IntPtr hWnd = GetActiveWindow();

            MessageBox.Show("1=" + hWnd.ToString());
            Form frm = (Form)Form.FromHandle(hWnd);
            if (frm != null)
            {
                MessageBox.Show("2");
                Control c = frm.ActiveControl;
                if (c != null)
                {
                    MessageBox.Show("3");
                    System.IntPtr srcDC = GetDC(c.Handle);
                    Bitmap bm = new Bitmap(c.Width, c.Height);
                    Graphics g = Graphics.FromImage(bm);
                    System.IntPtr bmDC = g.GetHdc();
                    BitBlt(bmDC, 0, 0, bm.Width, bm.Height, srcDC, 0, 0, 0x00C/*, C0020*/ /*SRCCOPY*/);
                    ReleaseDC(c.Handle, srcDC);
                    g.ReleaseHdc(bmDC);
                    g.Dispose();
                    bm.Save(FileName);
                }
            }
        }
    }

    #region WorkThread
    public class WorkThread
    {
        private System.Threading.Thread FThread;
        private EEPWizard FEEPWizard;
        private TWorkItems FWorkItems;
        private ManualResetEvent FWorkEvent;

        public WorkThread(EEPWizard aEEPWizard)
        {
            FEEPWizard = aEEPWizard;
            FWorkItems = new TWorkItems();
            FWorkEvent = new ManualResetEvent(false);
            FThread = new System.Threading.Thread(new ThreadStart(ThreadRun));
            FThread.Start();
        }

        public void AddWorkItem(string MethodName, object Params)
        {
            System.Threading.Monitor.Enter(FWorkItems);
            try
            {
                TWorkItem WorkItem = new TWorkItem();
                WorkItem.MethodName = MethodName;
                WorkItem.Params = Params;
                FWorkItems.Add(WorkItem);
                FWorkEvent.Set();
            }
            finally
            {
                System.Threading.Monitor.Exit(FWorkItems);
            }
        }

        public void RemoveWorkItem(TWorkItem WorkItem)
        {
            System.Threading.Monitor.Enter(FWorkItems);
            try
            {
                FWorkItems.Remove(WorkItem);
            }
            finally
            {
                System.Threading.Monitor.Exit(FWorkItems);
            }
        }

        public void NotifyWorkFinished()
        {
            if (FWorkItems.Count > 0)
            {
                FWorkEvent.Set();
            }
        }

        public void ThreadRun()
        {
            while (FThread.IsAlive)
            {
                FWorkEvent.WaitOne();
                if (FWorkItems.Count > 0)
                {
                    TWorkItem WorkItem = FWorkItems[0] as TWorkItem;
                    if (string.Compare(WorkItem.MethodName, "GenServerModule") == 0)
                    {
                        FEEPWizard.GenServerModule(WorkItem.Params);
                    }
                    RemoveWorkItem(WorkItem);
                }
                FWorkEvent.Reset();
            }
        }
    }

    public class TWorkItem : TCollectionItem
    {
        private string FMethodName;
        private object FParams;

        public TWorkItem()
        {
        }

        public string MethodName
        {
            get
            {
                return FMethodName;
            }
            set
            {
                FMethodName = value;
            }
        }

        public object Params
        {
            get
            {
                return FParams;
            }
            set
            {
                FParams = value;
            }
        }
    }

    public class TWorkItems : TCollection
    {
        public TWorkItems()
        {
        }
    }
    #endregion WorkThread

    public static class GlobalObject
    {
        private static EEPWizard FEEPWizard = null;

        public static EEPWizard EEPWizardInstance
        {
            get { return FEEPWizard; }
            set { FEEPWizard = value; }
        }
    }
}

//[DllImport("User32.DLL")]

//public static extern IntPtr GetActiveWindow ( );

//[DllImport("gdi32.dll")]

//private static extern bool BitBlt(IntPtr hdcDest,int nXDest,int nYDest,int
//nWidth,int nHeight,IntPtr hdcSrc,int nXSrc,int nYSrc,System.Int32 dwRop);

//[DllImport("User32.dll")]

//public extern static System.IntPtr GetDC(System.IntPtr hWnd);

//[DllImport("User32.dll")]

//public extern static int ReleaseDC(System.IntPtr hWnd, System.IntPtr hDC);
////modified to include hWnd

//private void Capture()

//{

//IntPtr hWnd = GetActiveWindow();

//Form frm = (Form)Form.FromHandle(hWnd);

//if(frm != null)

//{

//Control c = frm.ActiveControl;

//if(c != null)

//{


//System.IntPtr srcDC = GetDC(c.Handle);

//Bitmap bm = new Bitmap(c.Width,c.Height);

//Graphics g = Graphics.FromImage(bm);

//System.IntPtr bmDC = g.GetHdc();


//BitBlt(bmDC,0,0,bm.Width,bm.Height,srcDC,0,0,0x00C C0020 /*SRCCOPY*/);



//ReleaseDC(c.Handle, srcDC);

//g.ReleaseHdc(bmDC);

//g.Dispose();

//bm.Save([FILENAME]);

//}

//}

//}
