using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace FLDesigner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                string xoml = Path.GetTempPath() + "\\" + args[0];
                if (File.Exists(xoml))
                {
                    try
                    {
                        FLDesignerCore.FLDesigner designer = new FLDesignerCore.FLDesigner();
                        designer.BorderStyle = BorderStyle.FixedSingle;
                        designer.Size = new System.Drawing.Size(800, 600);
                        designer.LoadWorkflow(xoml);
                        designer.WorkflowView.SaveWorkflowImage(xoml + ".bmp", ImageFormat.Bmp);
                    }
                    catch{ }
                   
                }
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}