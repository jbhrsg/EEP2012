using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERPSalesApply
{
    public partial class Component : DataModule
    {
        private InfoCommand infoCommand1;
        private IContainer components;
    
        public Component()
        {
            InitializeComponent();
        }

        public Component(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.infoCommand1 = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.infoCommand1)).BeginInit();
            // 
            // infoCommand1
            // 
            this.infoCommand1.CacheConnection = false;
            this.infoCommand1.CommandText = null;
            this.infoCommand1.CommandTimeout = 30;
            this.infoCommand1.CommandType = System.Data.CommandType.Text;
            this.infoCommand1.DynamicTableName = false;
            this.infoCommand1.EEPAlias = null;
            this.infoCommand1.EncodingAfter = null;
            this.infoCommand1.EncodingBefore = "Windows-1252";
            this.infoCommand1.EncodingConvert = null;
            this.infoCommand1.InfoConnection = null;
            this.infoCommand1.MultiSetWhere = false;
            this.infoCommand1.Name = "infoCommand1";
            this.infoCommand1.NotificationAutoEnlist = false;
            this.infoCommand1.SecExcept = null;
            this.infoCommand1.SecFieldName = null;
            this.infoCommand1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCommand1.SelectPaging = false;
            this.infoCommand1.SelectTop = 0;
            this.infoCommand1.SiteControl = false;
            this.infoCommand1.SiteFieldName = null;
            this.infoCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.infoCommand1)).EndInit();

        }
    }
}
