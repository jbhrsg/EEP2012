using EFBase.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace JQOfficeTools
{
    public class JQOfficePlate : WebControl
    {
        public JQOfficePlate()
        {
            _JQDataSourceCollection = new JQCollection<JQDataSource>(this);

            _PlateMode = PlateModeType.Xml;
            _PlateType = PlateTypeType.Excel;
        }

        private JQCollection<JQDataSource> _JQDataSourceCollection;
        /// <summary>
        /// The datasource collection used to output
        /// </summary>
        [Category("Infolight"),
        Description("The datasources which plate will use to output")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        public JQCollection<JQDataSource> JQDataSourceCollection
        {
            get { return _JQDataSourceCollection; }
        }

        private string _TemplateFile;
        /// <summary>
        /// The template excel file
        /// </summary>
        [Category("Infolight"),
        Description("The template excel file")]
        [Editor(typeof(System.Web.UI.Design.UrlEditor),typeof(UITypeEditor))]
        public string TemplateFile
        {
            get { return _TemplateFile; }
            set { _TemplateFile = value; }
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

        private PlateTypeType _PlateType;
        /// <summary>
        /// The reference mode to plate
        /// </summary>
        [Category("Infolight"),
        Description("The reference mode to plate")]
        public PlateTypeType PlateType
        {
            get { return _PlateType; }
            set { _PlateType = value; }
        }
        /// <summary>
        /// The enum of reference mode of officeplate
        /// </summary>
        public enum PlateTypeType
        {
            Excel,
            Word
        }
        private string _EmailAddress;
        /// <summary>
        /// The address to email
        /// </summary>
        [Category("Infolight"),
        Description("The address to email")]
        [Browsable(false)]
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
        [Browsable(false)]
        public string EmailTitle
        {
            get { return _EmailTitle; }
            set { _EmailTitle = value; }
        }

        public bool _MarkException;
        /// <summary>
        /// The flag indicates whether mark a flag in the output file when encounter error
        /// </summary>
        [Category("Infolight"),
        Description("Specifies whether mark a flag in the output file when encounter error")]
        [Browsable(false)]
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

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            if (!this.DesignMode)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, JQControl.OfficePlate);
                writer.AddAttribute(JQProperty.DataOptions, DataOptions);
                writer.AddAttribute(JQProperty.InfolightOptions, InfolightOptions);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
            }
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            if (!this.DesignMode)
            {
                writer.RenderEndTag();
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.DesignMode)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, JQControl.OfficePlate);
                writer.AddAttribute(JQProperty.DataOptions, DataOptions);
                writer.AddAttribute(JQProperty.InfolightOptions, InfolightOptions);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                RenderChildren(writer);
                writer.RenderEndTag();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private string DataOptions
        {
            get
            {
                var options = new List<string>();
                if (this.Width.Type == UnitType.Pixel && Width.Value > double.Epsilon)
                {
                    options.Add(string.Format("width:{0}", Width.Value));
                }
                if (this.Height.Type == UnitType.Pixel && Height.Value > double.Epsilon)
                {
                    options.Add(string.Format("height:{0}", Height.Value));
                }
                return string.Join(",", options);
            }
        }


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private string InfolightOptions
        {
            get
            {
                var options = new List<string>();
                options.Add(string.Format("templateFile:'{0}'", TemplateFile));
                options.Add(string.Format("outputPath:'{0}'", OutputPath));
                options.Add(string.Format("outputFileName:'{0}'", OutputFileName));
                options.Add(string.Format("plateMode:'{0}'", PlateMode.ToString()));
                options.Add(string.Format("plateType:'{0}'", PlateType.ToString()));
                options.Add(string.Format("markException:{0}", MarkException.ToString().ToLower()));
                if (!string.IsNullOrEmpty(EmailAddress))
                {
                    options.Add(string.Format("emailAddress:'{0}'", EmailAddress));
                }
                if (!string.IsNullOrEmpty(EmailTitle))
                {
                    options.Add(string.Format("emailTitle:'{0}'", EmailTitle));
                }
                var dataSourceCollection = new List<string>();
                foreach (var datasource in JQDataSourceCollection)
                {
                    dataSourceCollection.Add("{" + datasource.Value + "}");
                }
                options.Add(string.Format("dataSourceCollection:[{0}]", string.Join(",", dataSourceCollection)));
                return string.Join(",", options);
            }
        }
    }
    public class JQDataSource : JQCollectionItem, IJQDataSourceProvider
    {

        public JQDataSource()
        {
            whereItems = new JQCollection<JQDataSourceWhereItem>((this) as IJQProperty);
        }
        private string _Caption;
        /// <summary>
        /// The Id of the datatable
        /// </summary>
        [Category("Infolight"),
        Description("The Id of the datatable")]
        public string Caption
        {
            get { return _Caption; }
            set { _Caption = value; }
        }

        private string _RemoteName;
        /// <summary>
        /// 数据源
        /// </summary>
        [Category("Infolight")]
        [Editor(typeof(RemoteNameEditor), typeof(UITypeEditor))]
        public string RemoteName
        {
            get
            {
                return _RemoteName;
            }
            set
            {
                _RemoteName = value;
            }
        }

        private string _DataMember;
        /// <summary>
        /// 表名
        /// </summary>
        [Category("Infolight")]
        [Editor(typeof(DataMemberEditor), typeof(UITypeEditor))]
        public string DataMember
        {
            get
            {
                return _DataMember;
            }
            set
            {
                _DataMember = value;
            }
        }
        private JQCollection<JQDataSourceWhereItem> whereItems;
        [Category("Infolight")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        public JQCollection<JQDataSourceWhereItem> WhereItems
        {
            get
            {
                return whereItems;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private string DataOptions
        {
            get
            {
                var options = new List<string>();
                options.Add(string.Format("remoteName:'{0}'", RemoteName));
                options.Add(string.Format("tableName:'{0}'", DataMember));
                options.Add(string.Format("caption:'{0}'", Caption));
                var whereIterms = new List<string>();
                foreach (var whereItem in WhereItems)
                {
                    whereIterms.Add(string.Format("{{field:'{0}',value:'{1}',condition:'{2}'}}", whereItem.FieldName, whereItem.Value, whereItem.Condition));
                }
                options.Add(string.Format("whereItems:[{0}]", string.Join(",", whereIterms)));

                return string.Join(",", options);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private string InfolightOptions
        {
            get
            {
                var options = new List<string>();
                options.Add(string.Format("remoteName:'{0}'", RemoteName));
                options.Add(string.Format("tableName:'{0}'", DataMember));
                options.Add(string.Format("caption:'{0}'", Caption));
                var whereIterms = new List<string>();
                foreach (var whereItem in WhereItems)
                {
                    whereIterms.Add(string.Format("{{field:'{0}',value:'{1}',condition:'{2}'}}", whereItem.FieldName, whereItem.Value, whereItem.Condition));
                }
                options.Add(string.Format("whereItems:[{0}]", string.Join(",", whereIterms)));
                return string.Join(",", options);
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Value
        {
            get
            {
                return this.InfolightOptions;
            }
        }


        public IColumnCaptions Component
        {
            get
            {
                if ((this as IJQProperty).ParentProperty != null && (this as IJQProperty).ParentProperty.Component != null)
                {
                    return (this as IJQProperty).ParentProperty.Component as IColumnCaptions;
                }
                return null;
            }
        }
    }

    public class JQDataSourceWhereItem : JQCollectionItem, IJQDataSourceProvider
    {
        public JQDataSourceWhereItem()
        { 
        
        }

        [Category("Infolight")]
        [Editor(typeof(FieldEditor), typeof(UITypeEditor))]
        public string FieldName { get; set; }

        [Category("Infolight")]
        public string WhereValue { get; set; }

        [Category("Infolight")]
        public string WhereMethod { get; set; }

        [Category("Infolight")]
        public bool RemoteMethod { get; set; }

        [Category("Infolight")]
        [Editor(typeof(ConditionEditor), typeof(UITypeEditor))]
        public string Condition { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Value
        {
            get
            {
                var values = new List<string>();
                if (!string.IsNullOrEmpty(this.WhereValue))
                {
                    if (this.WhereValue.StartsWith("_"))
                    {
                        values.Add(string.Format("remote[{0}]", WhereValue));
                    }
                    else
                    {
                        values.Add(WhereValue.Replace("'", "\'"));
                    }
                }
                else if (!string.IsNullOrEmpty(WhereMethod))
                {
                    if (RemoteMethod)
                    {
                        values.Add(string.Format("remote[{0}]", WhereMethod));
                    }
                    else
                    {
                        values.Add(string.Format("client[{0}]", WhereMethod));
                    }
                }
                return string.Join(",", values);
            }
        }

        public IColumnCaptions Component
        {
            get
            {
                if ((this as IJQProperty).ParentProperty != null && (this as IJQProperty).ParentProperty.Component != null)
                {
                    return (this as IJQProperty).ParentProperty.Component as IColumnCaptions;
                }
                return null;
            }
        }
        #region IJQDataSourceProvider Members

        string IJQDataSourceProvider.RemoteName
        {
            get
            {
                return ((this as IJQProperty).ParentProperty.ParentProperty as IJQDataSourceProvider).RemoteName;
            }
            set { }
        }

        string IJQDataSourceProvider.DataMember
        {
            get
            {
                return ((this as IJQProperty).ParentProperty.ParentProperty as IJQDataSourceProvider).DataMember;
            }
            set { }
        }

        #endregion
    }

}
