using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing.Design;
using System.ComponentModel.Design;
using System.Web.UI.Design;
using System.IO;
using System.Windows.Forms.Design;
using EFClientTools.Editor;

namespace JQOfficeTools
{
    [Designer(typeof(DataSourceDesigner), typeof(IDesigner))]
    public abstract class WebOfficePlate: WebControl, IOfficePlate
    {
        public WebOfficePlate()
        {
            _DataSource = new JQCollection<DataSourceItem>(this);
            _Tags = new JQCollection<TagItem>(this);
            _WebDataSource = new JQCollection<WebDataSourceItem>(this);

            _EmailAddress = string.Empty;
            _EmailTitle = string.Empty;
            _OfficeFile = string.Empty;
            _OutputFileName = string.Empty;
            _OutputPath = string.Empty;
            _PlateMode = PlateModeType.Xml;
            _FilePath = string.Empty;
        }

        private string _OfficeFile;
        /// <summary>
        /// The file used as a template
        /// </summary>
        [Category("Infolight"),
        Description("The template office file")]
        [Browsable(false)]
        public string OfficeFile
        {
            get { return _OfficeFile; }
            set { _OfficeFile = value; }
        }
        private string _OutputPath;
        /// <summary>
        /// The path of file to output
        /// </summary>
        [Category("Infolight"),
        Description("The path of file to output")]
        [Editor(typeof(URLPathEditor), typeof(UITypeEditor))]
        public string OutputPath
        {
            get { return _OutputPath; }
            set { _OutputPath = value; }
        }

        private string _OutputFileName;
        /// <summary>
        /// The name of file to output
        /// </summary>
        [Category("Infolight"),
        Description("The name of file to output")]
        public string OutputFileName
        {
            get { return _OutputFileName; }
            set { _OutputFileName = value; }
        }

        private PlateModeType _PlateMode;
        /// <summary>
        /// The reference mode to plate
        /// </summary>
        [Category("Infolight"),
        Description("The reference mode to plate")]
        public PlateModeType PlateMode
        {
            get { return _PlateMode; }
            set { _PlateMode = value; }
        }


        private string _EmailAddress;
        /// <summary>
        /// The address to email
        /// </summary>
        [Category("Infolight"),
        Description("The address to email")]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        private string _EmailTitle;
        /// <summary>
        /// The title of email
        /// </summary>
        [Category("Infolight"),
        Description("The title of email")]
        public string EmailTitle
        {
            get { return _EmailTitle; }
            set { _EmailTitle = value; }
        }

        private bool _ShowAction;
        /// <summary>
        /// The flag indicates whether show infomation durning the plate process
        /// </summary>
        [Category("Infolight"),
        Description("Specifies whether show infomation durning the plate process")]
        [Browsable(false)]
        public bool ShowAction
        {
            get { return _ShowAction; }
            set { _ShowAction = value; }
        }

        private bool _MarkException;
        /// <summary>
        /// The flag indicates whether mark a flag in the output file when encounter error
        /// </summary>
        [Category("Infolight"),
        Description("Specifies whether mark a flag in the output file when encounter error")]
        public bool MarkException
        {
            get { return _MarkException; }
            set { _MarkException = value; }
        }

        private string _FilePath;
        /// <summary>
        /// The really name of output file, while outputfilename is not set, system will create a file named by string of time
        /// </summary>
        [Browsable(false)]
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }

        private JQCollection<WebDataSourceItem> _WebDataSource;
        /// <summary>
        /// The datasource collection used to output
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        TypeConverter(typeof(CollectionConverter)),
        NotifyParentProperty(true)]
        [Category("Infolight"),
        Description("The datasources which plate will use to output")]
        public JQCollection<WebDataSourceItem> WebDataSource
        {
            get
            {
                return _WebDataSource;
            }
        }

        #region IOfficePlate Members

        /// <summary>
        /// The basic function of WordPlate, used to output
        /// </summary>
        /// <param name="Mode"></param>
        /// <returns>The flag indicates whether output is successful</returns>
        public virtual bool Output(int Mode)
        {
            string sfile = Path.GetDirectoryName(this.Page.Request.PhysicalPath) + "\\" + OfficeFile;
            string psypath = this.Page.Server.MapPath(this.OutputPath);
            if (!Directory.Exists(psypath))
            {
                Directory.CreateDirectory(psypath);
            }
            if (OutputFileName.Trim().Length > 0)
            {
                if (this.OutputPath.EndsWith("\\"))
                {
                    this.FilePath = this.OutputPath + this.OutputFileName;
                }
                else
                {
                    this.FilePath = this.OutputPath + "\\" + this.OutputFileName;
                }
                File.Copy(sfile, this.Page.Server.MapPath(FilePath), true);
            }
            else
            {
                DateTime dt = DateTime.Now;
                string strext = Path.GetExtension(sfile);
                if (this.OutputPath.EndsWith("\\"))
                {
                    this.FilePath = this.OutputPath + dt.ToString("yyyyMMddHHmmss") + strext;
                }
                else
                {
                    this.FilePath = this.OutputPath + "\\" + dt.ToString("yyyyMMddHHmmss") + strext;
                }
                File.Copy(sfile, this.Page.Server.MapPath(FilePath));
            }
            return true;
        }

        private static object EventOnAfterOutput = new object();
        /// <summary>
        /// The event ocured when the the process of output accomplished
        /// </summary>
        [Category("Infolight"),
        Description("The event ocured when the the process of output accomplished")]
        public event EventHandler AfterOutput
        {
            add { this.Events.AddHandler(EventOnAfterOutput, value); }
            remove { this.Events.RemoveHandler(EventOnAfterOutput, value); }
        }

        /// <summary>
        /// Trigger the afteroutput event
        /// </summary>
        /// <param name="value">The arguments of event</param>
        public void OnAfterOutput(EventArgs value)
        {
            EventHandler handler = this.Events[EventOnAfterOutput] as EventHandler;
            if (handler != null)
            {
                handler(this, value);
            }
        }

        private JQCollection<DataSourceItem> _DataSource;
        [PersistenceMode(PersistenceMode.InnerProperty),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        TypeConverter(typeof(CollectionConverter)),
        NotifyParentProperty(true),
        Browsable(false)]
        public JQCollection<DataSourceItem> DataSource
        {
            get
            {
                return _DataSource;
            }
        }
        private JQCollection<TagItem> _Tags;
        /// <summary>
        /// The user-defined tag used to output
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        TypeConverter(typeof(CollectionConverter)),
        NotifyParentProperty(true)]
        [Browsable(false)]
        public JQCollection<TagItem> Tags
        {
            get
            {
                return _Tags;
            }
        }

        #endregion

        /// <summary>
        /// The event ocured when the control is load
        /// </summary>
        /// <param name="e">The arguments of event</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitialDataSourceItem();
        }

        internal void InitialDataSourceItem()
        {
            DataSource.Clear();
            foreach (WebDataSourceItem wdi in WebDataSource)
            {
                DataSourceItem di = new DataSourceItem();
                Object datasource = wdi.DataSource;
                if (datasource != null)
                {
                    di.Caption = wdi.Caption;
                    di.DataSource = datasource as System.Data.DataSet;
                    di.DataMember = "Table";
                    foreach (DataSourceImageColumnItem item in wdi.ImageColumns)
                    {
                        di.ImageColumns.Add(item);
                    }
                    DataSource.Add(di);
                }
            }
        }

        internal Control FindControl(string ControlID, Control ParentControl)
        {
            if (string.Compare(ParentControl.ID, ControlID, false) == 0)
            {
                return ParentControl;
            }
            else
            {
                foreach (Control ct in ParentControl.Controls)
                {
                    Control ctrtn = FindControl(ControlID, ct);
                    if (ctrtn != null)
                    {
                        return ctrtn;
                    }
                }
                return null;
            }
        }
    }

    /// <summary>
    /// The class of WebDataSourceItem
    /// </summary>
    public class WebDataSourceItem : DataSourceItem, IJQDataSourceProvider
    {
        /// <summary>
        /// Create a new instance of WebDataSourceItem
        /// </summary>
        public WebDataSourceItem()
        {
        }

        public string Name
        {
            get
            {
                return Caption;
            }
        }
    }

    /// <summary>
    /// The editor of file path in design time
    /// </summary>
    public class URLPathEditor : System.Web.UI.Design.UrlEditor
    {
        public URLPathEditor()
            : base()
        { }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            string path = base.EditValue(context, provider, value).ToString();
            path = path.Substring(0, path.LastIndexOf("/") + 1);
            return path;
        }
    }

    public class URLNameEditor : System.Web.UI.Design.UrlEditor
    {
        public URLNameEditor()
            : base()
        { }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            string path = base.EditValue(context, provider, value).ToString();
            return path;
        }

    }
}
