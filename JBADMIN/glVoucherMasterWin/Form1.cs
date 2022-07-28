using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Srvtools;

namespace glVoucherMasterWin
{
    public partial class Form1 : InfoForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //infoNavigator1.AddNewItem.PerformClick();//一load就新增模式   
            //mVoucherDate.Focus();//焦點

            //grdglVoucherDetails.Focus();
            //grdglVoucherDetails.CurrentCell = grdglVoucherDetails.Rows[0].Cells[0];
            //(ibsOrderDetails.Current as DataRowView).IsNew
            //mVoucherDate.SelectAll();
            mVoucherDate.Enabled = false;
            infoCompanyID.Enabled = false;
            infoVoucherID.Enabled = false;

            //gViewBind();
            
        }
        public void gViewBind()
        {
            //Grid View 過濾顯示條件
            string UserID = CliUtils.fLoginUser.ToString();
            string d = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Day.ToString().PadLeft(2, '0');
            idView.SetWhere("UserID='" + UserID + "'");
            idView.SetOrder("CreateDate desc,CompanyID");

        }
    
        ////Master 欄位檢核
        // public bool VVoucherDate(object value)
        // {
        //     if (value.ToString() == "")
        //     {
        //         return false;
        //     }
        //     return true;
        // }
        //==============================VoucherDetails處理=====================================================
        //VoucherDetails中科目項目取得=> 參數=> CompanyID
        public int GetCompanyID()
        {
            try
            {
                int CompanyID = int.Parse(infoCompanyID.TextBoxSelectedValue.Trim());
                return CompanyID;
            }
            catch
            {
                return 0;
            }
           // return CompanyID;
        }
        //VoucherDetails中科目項目取得=> 參數=> Acno(取四碼)
        public string GetAcno()
        {            
            String AcnoSubAcno = ((DataGridViewRow)grdglVoucherDetails.CurrentRow).Cells[2].Value.ToString().Trim();//科目明細
            if (AcnoSubAcno.Length > 3)
            {
                return AcnoSubAcno.Substring(0, 4);
            }else
                return "";

        }
        //資料列檢查
        private void grdglVoucherDetails_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (ibsglVoucherDetails.Count > 0)
            {
                //是否需要成本中心
                int CompanyID = int.Parse(infoCompanyID.TextBoxSelectedValue.Trim());
                if (((DataGridViewRow)grdglVoucherDetails.CurrentRow).Cells[2].Value != null)
                {
                    String AcnoSubAcno = ((DataGridViewRow)grdglVoucherDetails.CurrentRow).Cells[2].Value.ToString().Trim();//科目明細
                    String CostCenterID = ((DataGridViewRow)grdglVoucherDetails.CurrentRow).Cells[4].Value.ToString().Trim();//成本中心

                    if (AcnoSubAcno != "")
                    {
                        DataSet dataSet = dsTemp.Execute("select bCostCenterID from glAccountItem where CompanyID = " + CompanyID + " and Acno+SubAcno= \'" + AcnoSubAcno + "\' ");
                        if (dataSet.Tables[0].Rows.Count == 0)
                        {
                            e.Cancel = true;
                            MessageBox.Show(AcnoSubAcno + " 此科目明細不存在");
                            grdglVoucherDetails.ClearSelection();
                        }
                        else if (dataSet.Tables[0].Rows[0]["bCostCenterID"].ToString() == "True" && CostCenterID == "")  // 如果需要 又 沒有資料的話
                        {
                            e.Cancel = true;
                            MessageBox.Show(AcnoSubAcno + " 需要成本中心");
                            grdglVoucherDetails.ClearSelection();
                        }
                        else
                        {
                            e.Cancel = false;
                        }
                    }
                }
                else
                {
                    e.Cancel = true;
                    grdglVoucherDetails.ClearSelection();
                }
            }
            
        }
  
        //存檔檢查
        private void infoNavigator1_BeforeItemClick(object sender, BeforeItemClickEventArgs e)
        {
            if (e.ItemName == "Query")//查詢時=>不可改 傳票日期,公司別, 傳票類別
            {
                mVoucherDate.Enabled = false;
                infoCompanyID.Enabled = false;
                infoVoucherID.Enabled = false;
            }
            if (e.ItemName == "Add")//選按新增時
            {
                mVoucherDate.Enabled = true;
                infoCompanyID.Enabled = true;
                infoVoucherID.Enabled = true;

            }
            //gridview 要有明細資料
            if (e.ItemName == "Apply" || e.ItemName == "Editing")
            {
                //if (ibsglVoucherDetails.isEdited)
                //{
                //    ibsglVoucherDetails.EndEdit();
                //}
                //ibsglVoucherDetails.BeginEdit();

                //傳票日期檢查
                if (mVoucherDate.IsEmpty)
                {
                    e.Cancel = true;
                    MessageBox.Show("請選擇傳票日期。");
                    mVoucherDate.Focus();//焦點
                }
                else
                {

                    if (ibsglVoucherDetails.Count == 0)
                    {
                        e.Cancel = true;
                        MessageBox.Show("無項目。");
                    }
                    else
                    {
                        mVoucherDate.Focus();//傳票日期焦點=>幫助計算金額

                        (ibsglVoucherDetails.Current as DataRowView).Row.BeginEdit();

                        //gridview借貸需要平衡 -----------------------------------------------------------------            
                        int i = ibsglVoucherDetails.List.Count;
                        int qty1 = 0;
                        int qty2 = 0;
                        string sType = "";
                        string qty = "";

                        for (int j = 0; j < i; j++)
                        {
                            sType = ((DataRowView)ibsglVoucherDetails.List[j])["BorrowLendType"].ToString();
                            qty = ((DataRowView)ibsglVoucherDetails.List[j])["AmtShow"].ToString();
                            if (qty != "")
                            {
                                if (sType == "1")//借
                                {
                                    qty1 = qty1 + Convert.ToInt32(qty);
                                }
                                else qty2 = qty2 + Convert.ToInt32(qty);//貸          
                            }
                        }

                        //ibsglVoucherDetails.EndEdit();
                        (ibsglVoucherDetails.Current as DataRowView).Row.EndEdit();
                        //此方法的作用是為了當Master沒有修改的狀態下可以將這行Row的狀態變為Modifed
                        if (qty1 != qty2)
                        {
                            e.Cancel = true;
                            MessageBox.Show("借:" + qty1 + ",貸:" + qty2 + " 總金額不平衡！");
                        }

                    }


                }


            }
           

        }

        private void infoNavigator1_AfterItemClick(object sender, AfterItemClickEventArgs e)
        {
            
            if (e.ItemName == "Add")//選按新增時
            {
                // 公司別、傳票類別預設=> 抓設定
                string UserID = CliUtils.fLoginUser.ToString();
                string CompanyID = "1";
                string VoucherID = "2";//2 => 稅務帳

                DataSet dataSet = dsTemp.Execute("Select top 1 * from glVoucherSet where UserID= \'" + UserID + "\'");
                if (dataSet.Tables[0].Rows.Count > 0)  // 如果存在的話
                {
                    CompanyID = dataSet.Tables[0].Rows[0]["CompanyID"].ToString().Trim();
                    VoucherID = dataSet.Tables[0].Rows[0]["VoucherID"].ToString().Trim();
                }

                //infoCompanyID.TextBoxSelectedValue = CompanyID;
                //infoVoucherID.TextBoxSelectedValue = VoucherID;
                infoCompanyID.WriteValue(CompanyID);
                infoVoucherID.WriteValue(VoucherID);

                //gridview新增一列
                grdglVoucherDetails.Focus();
                grdglVoucherDetails.CurrentCell = grdglVoucherDetails.Rows[0].Cells[0];
              


            }
            //------------------------------------------------------------------------------------------------------------------------
            if (e.ItemName == "Apply")//存檔
            {
                
                infoCompanyID.WriteValue(infoCompanyID.TextBoxSelectedValue);
                infoVoucherID.WriteValue(infoVoucherID.TextBoxSelectedValue);

                //ibsglVoucherDetails.RemoveCurrent();//清空明細
                //gViewBind();
                //infoNavigator1.AddNewItem.PerformClick();//新增完一筆資料後=>再導入新增模式

            }
        }

 



      
       
       

       

        

       

       

       

       

       

      

       
       

    }
}

