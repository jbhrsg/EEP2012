using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace JQOfficeTools.Design
{
    /// <summary>
    /// The class of frmDataMember, used to edit datamember
    /// </summary>
    public partial class frmDataMember : Form
    {
        /// <summary>
        /// Create a new instance of frmDataMember
        /// </summary>
        /// <param name="objmember">The list of datamember</param>
        /// <param name="value">The selected datamember</param>
        public frmDataMember(ArrayList objmember, string value)
        {
            InitializeComponent();
            for (int i = 0; i < objmember.Count; i++)
            {
                lbDataMember.Items.Add(objmember[i].ToString());
            }
           
            if (lbDataMember.Items.Contains(value))
            {
                lbDataMember.SelectedItem = value;
            }

        }

        private void frmDataMember_Load(object sender, EventArgs e)
        {
            #region setup language
            int lang = 0;
            this.Text = "DataMember" + JQOfficeTools.Properties.Resources.editor.Split(',')[lang];
            this.btnOK.Text = JQOfficeTools.Properties.Resources.btnOK.Split(',')[lang];
            this.btnCancel.Text = JQOfficeTools.Properties.Resources.btnCancel.Split(',')[lang]; 
            #endregion
        }
    }
}