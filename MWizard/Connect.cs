namespace MWizard
{
    using System;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.CommandBars;
    using Extensibility;
    using EnvDTE;
    using EnvDTE80;
    using System.Reflection;
    using System.Threading;


    /// <summary>The object for implementing an Add-in.</summary>
    /// <seealso class='IDTExtensibility2' />
    public class Connect : Object, IDTExtensibility2, IDTCommandTarget
    {
        /// <summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>

        private EEPWizard FEEPWizard = null;

        public Connect()
        {
        }

        /// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
        /// <param term='application'>Root object of the host application.</param>
        /// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
        /// <param term='addInInst'>Object representing this Add-in.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            //EEP Wizard
            if (connectMode == ext_ConnectMode.ext_cm_Startup)
            {
                applicationObject = (DTE2)application;
                addInInstance = (AddIn)addInInst;
                FEEPWizard = new MWizard.EEPWizard(applicationObject, addInInstance);
                addInInstance.Object = FEEPWizard;
                GlobalObject.EEPWizardInstance = FEEPWizard;

                object[] contextGUIDS = new object[] { };
                Commands2 commands = (Commands2)applicationObject.Commands;
                try
                {
                    Microsoft.VisualStudio.CommandBars.CommandBar menuBarCommandBar;
                    CommandBarControl toolsControl;
                    CommandBarPopup toolsPopup;
                    CommandBarControl commandBarControl;

                    //Add a command to the Commands collection:
                    Command command = commands.AddNamedCommand2(
                        addInInstance,
                        "EEPWizard",
                        "EEP Wizard",
                        "Executes the command for EEP Wizard",
                        true,
                        59,
                        ref contextGUIDS,
                        (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled,
                        (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

                    String toolsMenuName = "Tools";
                    try
                    {
                        //If you would like to move the command to a different menu, change the word "Tools" to the 
                        //  English version of the menu. This code will take the culture, append on the name of the menu
                        //  then add the command to that menu. You can find a list of all the top-level menus in the file
                        //  CommandBar.resx.
                        System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("EEPServerWizard.CommandBar", System.Reflection.Assembly.GetExecutingAssembly());
                        System.Threading.Thread thread = System.Threading.Thread.CurrentThread;
                        System.Globalization.CultureInfo cultureInfo = thread.CurrentCulture;

                        if (applicationObject.LocaleID == 1028 || applicationObject.LocaleID == 2052)
                        {
                            toolsMenuName = "工具";
                            // toolsMenuName = resourceManager.GetString(String.Concat(cultureInfo.TwoLetterISOLanguageName, "工具"));
                        }
                        else
                        {
                            // toolsMenuName = resourceManager.GetString(String.Concat(cultureInfo.TwoLetterISOLanguageName, "Tools"));
                            toolsMenuName = "Tools";
                        }

                        // toolsMenuName = resourceManager.GetString(String.Concat(cultureInfo.TwoLetterISOLanguageName, "Tools"));//???
                        //toolsMenuName = resourceManager.GetString(String.Concat(cultureInfo.TwoLetterISOLanguageName, "工具"));//???
                    }
                    catch (Exception e)
                    {
                        //We tried to find a localized version of the word Tools, but one was not found.
                        //  Default to the en-US word, which may work for the current culture.
                        //if (applicationObject.LocaleID == 1028 || applicationObject.LocaleID == 2056)
                        //{
                        //    toolsMenuName = "工具";
                        //}
                        //else
                        //{
                        //    toolsMenuName = "tools";
                        //}
                        throw e;
                    }

                    //Place the command on the tools menu.
                    //Find the MenuBar command bar, which is the top-level command bar holding all the main menu items:
                    menuBarCommandBar = ((CommandBars)applicationObject.CommandBars)["MenuBar"];
                    //Find the Tools command bar on the MenuBar command bar:
                    toolsControl = menuBarCommandBar.Controls[toolsMenuName];
                    toolsPopup = (CommandBarPopup)toolsControl;
                    //Find the appropriate command bar on the MenuBar command bar:
                    commandBarControl = (CommandBarControl)command.AddControl(toolsPopup.CommandBar, 1);
                }
                catch
                {
                    //這邊的錯誤要忽略，不然會引起錯誤"A Command with that name already exists."
                    //MessageBox.Show(E.Message);
                    //throw;
                }
            }

        }

        /// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
        /// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
        }

        /// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />		
        public void OnAddInsUpdate(ref Array custom)
        {
        }

        /// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref Array custom)
        {
        }

        /// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref Array custom)
        {
        }

        /// <summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
        /// <param term='commandName'>The name of the command to determine state for.</param>
        /// <param term='neededText'>Text that is needed for the command.</param>
        /// <param term='status'>The state of the command in the user interface.</param>
        /// <param term='commandText'>Text requested by the neededText parameter.</param>
        /// <seealso class='Exec' />
        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                if (commandName == "MWizard.Connect.EEPWizard")
                {
                    status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                }
            }
        }

        /// <summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
        /// <param term='commandName'>The name of the command to execute.</param>
        /// <param term='executeOption'>Describes how the command should be run.</param>
        /// <param term='varIn'>Parameters passed from the caller to the command handler.</param>
        /// <param term='varOut'>Parameters passed from the command handler to the caller.</param>
        /// <param term='handled'>Informs the caller if the command was handled or not.</param>
        /// <seealso class='Exec' />
        public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            handled = false;
            if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                try
                {
                    if (commandName == "MWizard.Connect.EEPWizard")
                    {
                        fmWizardMain MainForm = new fmWizardMain();
                        String Result = MainForm.ShowEEPWizard();
                        switch (Result)
                        {
                            case "Server":
                                try
                                {
                                    FEEPWizard.ServerWizard.ShowServerWizard();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            case "Client":
                                try
                                {
                                    FEEPWizard.ClientWizard.ShowClientWizard();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            case "Web":
                                try
                                {
                                    FEEPWizard.WebWizard.ShowWebClientWizard();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            case "NewEmptySolution":
                                try
                                {
                                    FEEPWizard.NewEmptySolution.ShowDialog();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            case "ClientExt":
                                try
                                {
                                    FEEPWizard.ExtClientWizard.ShowClientWizard();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            case "WebExt":
                                try
                                {
                                    FEEPWizard.ExtWebWizard.ShowWebClientWizard();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            case "WebReport":
                            case "WinReport":
                                try
                                {
                                    fmReportTypeSelect frts = new fmReportTypeSelect(Result, applicationObject, addInInstance);
                                    frts.ShowDialog();
                                    //bool isWebRpt = (Result == "WebReport");
                                    //frmEEPReport bForm = new frmEEPReport(applicationObject, addInInstance, isWebRpt);
                                    //bForm.ShowDialog();
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            //case "WCFServer":
                            //    try
                            //    {
                            //        FEEPWizard.WCFServerWizard.ShowServerWizard();
                            //        handled = true;
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                            //    }
                            //    break;
                            //case "WCFWebForm":
                            //    try
                            //    {
                            //        FEEPWizard.WCFWebFormWizard.ShowWebClientWizard();
                            //        handled = true;
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                            //    }
                            //    break;
                            //case "SLClient":
                            //    try
                            //    {
                            //        FEEPWizard.SLClient.ShowSLClientWizard();
                            //        handled = true;
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                            //    }
                            //    break;
                            case "ServerPath":
                                try
                                {
                                    FEEPWizard.ServerPath.ShowDialog();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            case "ExtJS":
                                try
                                {
                                    FEEPWizard.ExtWebWizard.ShowWebClientWizard();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            case "DevExpress":
                                try
                                {
                                    FEEPWizard.DevExpress.ShowDevExpress();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            case "JQuery":
                                try
                                {
                                    FEEPWizard.JQueryWebFormWizard.ShowJQueryWebForm();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            case "JQMobile":
                                try
                                {
                                    FEEPWizard.JQMobileFormWizard.ShowJQMobileForm();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            case "JQueryToJQMobile":
                                try
                                {
                                    FEEPWizard.JQueryToJQMobileWizard.ShowJQueryToJQMobileWebForm();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            case "RDLC":
                                try
                                {
                                    FEEPWizard.RDLCWizard.ShowRDLCWizard();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                            case "Ionic":
                                try
                                {
                                    FEEPWizard.IonicWizard.ShowIonicWizard();
                                    handled = true;
                                }
                                catch (Exception ex)
                                {
                                    WzdUtils.Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
                                }
                                break;
                        }
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(string.Format("{0} causes exception, the error message: \n{1} \n\n{2}", commandName,
                        E.Message, "Please check the LoadBehavior value of MWizard.Addin is '1' !!!"));
                }
            }
        }
        private DTE2 applicationObject;
        private AddIn addInInstance;
    }
}