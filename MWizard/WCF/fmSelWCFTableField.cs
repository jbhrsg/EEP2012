using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlClient;
using System.Data.Common;
using Srvtools;
using System.Linq;
using System.Collections;
using EFClientTools.Beans;
using EFClientTools.EFServerReference;

namespace MWizard
{
    public partial class fmSelWCFTableField : Form
    {
        private string FTableName;
        private TWCFDatasetItem FDatasetItem;
        private ListView FDestListView;
        private bool FClientField = false;
        private TBlockItem FBlockItem = null;
        private ListViewColumnSorter lvwFieldSorter;
        private ListViewColumnSorter lvwTableSorter;
        private String FProjectPath;
        private String FProjectName;
        private TWCFDetailItem FDetailItem = null;
        private Type FClassType;
        private MWizard.fmClientWzard.RearrangeRefValButtonFunc FRearrangeRefValButtonFunc = null;
        private EventHandler FRefValClickEvent = null;
        private List<COLDEFInfo> ColDefObjects = null;
        private string _AssemblyName;
        private string _CommandName;


        public fmSelWCFTableField()
        {
            InitializeComponent();
            lvwFieldSorter = new ListViewColumnSorter();
            lvwTableSorter = new ListViewColumnSorter();
            this.lvFields.ListViewItemSorter = lvwFieldSorter;
            this.lvTable.ListViewItemSorter = lvwTableSorter;
        }

        public bool ShowSelTableFieldForm(String pPath, String pName, ref string TableName)
        {
            FProjectPath = pPath;
            FProjectName = pName;
            FTableName = TableName;
            Init();
            DialogResult R = ShowDialog();
            if (R == DialogResult.OK)
                TableName = FTableName;
            return R == DialogResult.OK;
        }

        public bool ShowSelTableFieldForm(String pPath, String pName, String sName, ListView DestListView, TWCFDatasetItem DatasetItem)
        {
            FDatasetItem = DatasetItem;
            FDatasetItem.AddAll = false;
            FProjectPath = pPath;
            FProjectName = pName;
            FTableName = sName; 
            FDestListView = DestListView;
            Init();
            return ShowDialog() == DialogResult.OK;
        }

        public bool ShowSelTableFieldForm(TWCFDetailItem DetailItem, ListView DestListView, MWizard.fmClientWzard.RearrangeRefValButtonFunc RearrangeRefValButton, EventHandler RefValButtonEvent,  List<COLDEFInfo> colDefObjects, string assemblyName, string commandName)
        {
            FDetailItem = DetailItem;
            FTableName = FDetailItem.EntityName;
            FDestListView = DestListView;
            FRearrangeRefValButtonFunc = RearrangeRefValButton;
            FRefValClickEvent = RefValButtonEvent;
            FClientField = true;
            ColDefObjects = colDefObjects;
            _AssemblyName = assemblyName;
            _CommandName = commandName;
            Init();
            return ShowDialog() == DialogResult.OK;
        }

        private bool Init()
        {
            int I;

            /*
            for (I = 0; I < tabControl.TabCount; I++)
            {
                tabControl.TabPages[I].Hide();
            }
            */

            tabControl.TabPages.Clear();

            if (FDestListView == null)
            {
                tabControl.TabPages.Add(tpSelectTable);
                tabControl.SelectedTab = tpSelectTable;
                I = (panel1.ClientSize.Width - btnOK.Width - btnCancel.Width) / 3;
                btnOK.Left = I;
                btnCancel.Left = btnOK.Bounds.Width + I + 50;
                GetTableNames(lvTable);
                if (lvTable.Items.Count > 0 && lvTable.SelectedItems.Count == 0)
                    lvTable.Items[0].Selected = true;
            }
            else
            {
                tabControl.TabPages.Add(tpSelectFields);
                tabControl.SelectedTab = tpSelectFields;
                btnSelectAll.Visible = true;
                if (FTableName == String.Empty)
                {
                    MessageBox.Show("DataMember is null. Please select it first.");
                    return false;
                }
                GetFieldNames(FTableName, lvFields, FDestListView);
            }
            return true;
        }

        private void GetTableNames(ListView LV)
        {
            EFServerTools.Design.MetadataProvider aMetadataProvider = new EFServerTools.Design.MetadataProvider(FProjectPath, FProjectName);
            List<String> lsContainerNames = aMetadataProvider.GetEntityContainerNames();
            if (lsContainerNames.Count > 0)
            {
                List<String> lsSetNames = aMetadataProvider.GetEntitySetNames(lsContainerNames[0]);

                ListViewItem lvi;

                LV.Items.Clear();

                if (lsSetNames.Count > 0)
                {
                    LV.BeginUpdate();
                    for (int I = 0; I < lsSetNames.Count; I++)
                    {
                        lvi = new ListViewItem();
                        lvi.Text = lsSetNames[I];
                        LV.Items.Add(lvi);
                        lvi.SubItems.Add(lvi.Text);
                        lvi.Selected = lvi.Text.CompareTo(FTableName) == 0;

                        //aMetadataProvider.SetEntityType(lsContainerNames[0], lsSetNames[I], lsSetNames[I] + "test");
                        //aMetadataProvider.SetEntityType(lsContainerNames[0], lvi.Text + "test", lvi.Text);

                    }
                    LV.EndUpdate();
                }

                LV.Sort();
            }
        }

        private void AddDestFieldItem(ListViewItem lvi)
        {
            TFieldAttrItem Item = new TFieldAttrItem();
            FDatasetItem.FieldAttrItems.Add(Item);
            Item.DataField = lvi.Text;
            if (lvi.SubItems.Count > 1)
                Item.Description = lvi.SubItems[1].Text;
            lvi.Tag = Item;
        }

        private void AddDestBlockFieldItem(ListViewItem ViewItem, TBlockFieldItem SourceItem)
        {
            TBlockFieldItem BlockFieldItem = new TBlockFieldItem();
            BlockFieldItem.DataField = SourceItem.DataField;
            BlockFieldItem.Description = SourceItem.Description;
            BlockFieldItem.DataType = SourceItem.DataType;
            BlockFieldItem.IsKey = SourceItem.IsKey;
            BlockFieldItem.CheckNull = SourceItem.CheckNull;
            BlockFieldItem.DefaultValue = SourceItem.DefaultValue;
            BlockFieldItem.Length = SourceItem.Length;
            BlockFieldItem.ControlType = SourceItem.ControlType;
            BlockFieldItem.QueryMode = SourceItem.QueryMode;
            BlockFieldItem.EditMask = SourceItem.EditMask;
            /*
            InfoCommand command1 = new InfoCommand(FDatabaseType);
            command1.Connection = FConnection;
            string[] textArray1 = new string[] { "Select TABLE_NAME, FIELD_NAME, IS_KEY, FIELD_LENGTH, CHECK_NULL, DEFAULT_VALUE from COLDEF where TABLE_NAME = '", FTableName, "' and FIELD_NAME = '", BlockFieldItem.DataField, "'" };
            command1.CommandText = string.Concat(textArray1);
            IDbDataAdapter adapter1 = WzdUtils.AllocateDataAdapter(FDatabaseType);
            adapter1.SelectCommand = command1.GetInternalCommand();
            DataSet set1 = new DataSet();
            WzdUtils.FillDataAdapter(FDatabaseType, adapter1, set1, "COLDEF");
            DataRow[] rowArray1 = set1.Tables[0].Select("FIELD_NAME = '" + BlockFieldItem.DataField + "'");
            if (rowArray1.Length == 1)
            {
                BlockFieldItem.IsKey = rowArray1[0].ItemArray[2].ToString().ToUpper() == "Y";
                if (rowArray1[0].ItemArray[3] != null)
                    BlockFieldItem.Length = int.Parse(rowArray1[0].ItemArray[3].ToString());
                BlockFieldItem.CheckNull = rowArray1[0].ItemArray[4].ToString().ToUpper();
                BlockFieldItem.DefaultValue = rowArray1[0].ItemArray[5].ToString();
            }
             */

           

            
            if (FDestListView.Columns.Count == 3)
            {
                ListViewItem.ListViewSubItem LVSI = ViewItem.SubItems.Add("");
                Button B = new Button();
                B.Parent = FDestListView;
                if (FRearrangeRefValButtonFunc != null)
                    FRearrangeRefValButtonFunc(B, LVSI.Bounds);
                B.BackColor = Color.Silver;
                B.BringToFront();
                LVSI.Tag = B;
                B.Tag = ViewItem;
                ViewItem.Tag = BlockFieldItem;
                B.Click += new EventHandler(FRefValClickEvent);
                B.Text = "...";
            }

            if (FDetailItem != null)
                FDetailItem.BlockFieldItems.Add(BlockFieldItem);
        }

        private bool DoOK()
        {
            bool Result = false;
            ListViewItem lvi;
            if (FDestListView == null)
            {
                lvi = lvTable.SelectedItems[0];
                FTableName = lvi.Text;
                Result = true;
            }
            else
            {
                int I;
                for (I = 0; I < lvFields.Items.Count; I++)
                {
                    if (lvFields.Items[I].Selected)
                    {
                        Result = true;
                        lvi = new ListViewItem();
                        lvi.Text = lvFields.Items[I].Text;
                        lvi.Tag = lvFields.Items[I].Tag;
                        lvi.SubItems.Add(lvFields.Items[I].SubItems[1]);
                        FDestListView.Items.Add(lvi);
                        if (FClientField)
                            AddDestBlockFieldItem(lvi, (TBlockFieldItem)lvFields.Items[I].Tag);
                        else
                            AddDestFieldItem(lvi);
                    }
                }
                if (FDestListView.Items.Count > 0)
                    FDestListView.Items[0].Selected = true;
            }
            return Result;
        }

        private void GetFieldNames(string EntityName, ListView SrcListView, ListView DestListView)
        {
            if (FClientField)
            {
                Dictionary<string, object> htFields = WzdUtils.GetFieldsByEntityName(_AssemblyName, _CommandName, FTableName);
                List<string> keyFields = EFClientTools.DesignClientUtility.GetEntityPrimaryKeys(_AssemblyName, _CommandName, FTableName);
                ListViewItem lvi;
                ListViewItem ViewItem;
                for (int I = 0; I < DestListView.Items.Count; I++)
                {
                    ViewItem = DestListView.Items[I];
                    if (!htFields.ContainsKey(ViewItem.Text))
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
                SrcListView.BeginUpdate();

                foreach (var field in htFields)
                {
                    bool Found = false;
                    for (int J = 0; J < DestListView.Items.Count; J++)
                    {
                        lvi = DestListView.Items[J];
                        if (string.Compare(field.Key.ToString(), lvi.Text, false) == 0)
                        {
                            Found = true;
                            break;
                        }
                    }
                    if (Found == false)
                    {
                        TBlockFieldItem FieldItem = new TBlockFieldItem();
                        FieldItem.DataField = field.Key.ToString();

                        if (keyFields != null && keyFields.Count != 0)
                        {
                            if (keyFields.Contains(FieldItem.DataField))
                            {
                                FieldItem.IsKey = true;
                            }
                        }

                        FieldItem.DataType = (Type)field.Value;
                        ViewItem = SrcListView.Items.Add(FieldItem.DataField);
                        ViewItem.Tag = FieldItem;

                        COLDEFInfo colDefObject = null;

                        if (ColDefObjects != null)
                        {
                            colDefObject = ColDefObjects.Find(c => c.FIELD_NAME == FieldItem.DataField);
                        }

                        if (colDefObject != null)
                        {
                            FieldItem.Description = colDefObject.CAPTION;
                            FieldItem.CheckNull = colDefObject.CHECK_NULL == null ? null : colDefObject.CHECK_NULL.ToUpper();
                            FieldItem.DefaultValue = colDefObject.DEFAULT_VALUE;
                            if (string.Compare(colDefObject.IS_KEY, "Y", true) == 0)
                            {
                                FieldItem.IsKey = true;
                            }
                            FieldItem.ControlType = colDefObject.NEEDBOX;
                            FieldItem.EditMask = colDefObject.EDITMASK;
                            FieldItem.Length = colDefObject.FIELD_LENGTH;
                        }

                        ViewItem.SubItems.Add(FieldItem.Description);

                        //lvi = SrcListView.Items.Add(lsPropertyNames[I].ToString());
                        //lvi.SubItems.Add(lvi.Text);
                    }

                }

                if (SrcListView.Items.Count > 0)
                    SrcListView.Items[0].Selected = true;

                SrcListView.EndUpdate();

                SrcListView.Sort();
            }
            else
            {
                ListViewItem lvi;
                SrcListView.Items.Clear();
                SrcListView.BeginUpdate();

                EFServerTools.Design.MetadataProvider aMetadataProvider = new EFServerTools.Design.MetadataProvider(FProjectPath, FProjectName);
                String strEntityTypeName = aMetadataProvider.GetEntitySetType(FDatasetItem.ContainerName, FDatasetItem.TableName);
                List<string> lPropertyNames = aMetadataProvider.GetPropertyNames(FDatasetItem.ContainerName, strEntityTypeName);
                for (int i = 0; i < lPropertyNames.Count; i++)
                {
                    bool Found = false;
                    for (int J = 0; J < DestListView.Items.Count; J++)
                    {
                        lvi = DestListView.Items[J];
                        if (string.Compare(lPropertyNames[i], lvi.Text, false) == 0)
                        {
                            Found = true;
                            break;
                        }
                    }
                    if (Found == false)
                    {
                        lvi = SrcListView.Items.Add(lPropertyNames[i]);
                        lvi.SubItems.Add(lvi.Text);
                    }
                }

                if (SrcListView.Items.Count > 0)
                    SrcListView.Items[0].Selected = true;

                SrcListView.EndUpdate();
                SrcListView.Sort();
            }
        }

        private void GetTableCaptionFromCOLDEF(TStringList aTableNameCaptionList)
        {
            //int I;
            //DataRow DR;
            //InfoCommand aInfoCommand = new InfoCommand(FDatabaseType);
            //aInfoCommand.Connection = FConnection;
            //aInfoCommand.CommandText = "Select TABLE_NAME, CAPTION from COLDEF where FIELD_NAME = '' or FIELD_NAME is null order by TABLE_NAME";
            //IDbDataAdapter DA = DBUtils.CreateDbDataAdapter(aInfoCommand);
            //DataSet D = new DataSet();
            //WzdUtils.FillDataAdapter(FDatabaseType, DA, D, "COLDEF");
            //aTableNameCaptionList.Clear();
            //for (I = 0; I < D.Tables[0].Rows.Count; I++)
            //{
            //    DR = D.Tables[0].Rows[I];
            //    if (DR["TABLE_NAME"].ToString().Trim() != "" && DR["CAPTION"].ToString().Trim() != "")
            //        aTableNameCaptionList.Add(DR["TABLE_NAME"].ToString().Trim() + "=" + DR["CAPTION"].ToString().Trim());
            //}
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (DoOK())
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private string Test1(FUNC f)
        {
            MessageBox.Show(f());
            return "";
        }

        public delegate string FUNC();

        public string Test2()
        {
            return "";
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (FDatasetItem != null)
                FDatasetItem.AddAll = true;
            if (FDestListView != null)
            {
                int I;
                for (I = 0; I < lvFields.Items.Count; I++)
                    lvFields.Items[I].Selected = true;
            }
            if (DoOK())
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void lvFields_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // 检查点击的列是不是现在的排序列.
            if (e.Column == lvwFieldSorter.SortColumn)
            {
                // 重新设置此列的排序方法.
                if (lvwFieldSorter.OrderOfSort == System.Windows.Forms.SortOrder.Ascending)
                {
                    lvwFieldSorter.OrderOfSort = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    lvwFieldSorter.OrderOfSort = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // 设置排序列，默认为正向排序
                lvwFieldSorter.SortColumn = e.Column;
                lvwFieldSorter.OrderOfSort = System.Windows.Forms.SortOrder.Ascending;
            }

            // 用新的排序方法对ListView排序
            (sender as ListView).Sort();
        }

        private void lvTable_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // 检查点击的列是不是现在的排序列.
            if (e.Column == lvwTableSorter.SortColumn)
            {
                // 重新设置此列的排序方法.
                if (lvwTableSorter.OrderOfSort == System.Windows.Forms.SortOrder.Ascending)
                {
                    lvwTableSorter.OrderOfSort = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    lvwTableSorter.OrderOfSort = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // 设置排序列，默认为正向排序
                lvwTableSorter.SortColumn = e.Column;
                lvwTableSorter.OrderOfSort = System.Windows.Forms.SortOrder.Ascending;
            }

            // 用新的排序方法对ListView排序
            (sender as ListView).Sort();
        }
    }
}