<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Customer_Normal_Customer.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        //目前使用者的銷貨類別
        var custSalesTypeIDs = ''; //客戶已擁有的銷貨類別
        var userSalesTypeIDs = ''; //業務已擁有的銷貨類別
        var userSalesKindIDs = ''; //業務已擁有的業務類別
        var userCustomerTypeID = 1;    //預設客戶屬性代號
        var firstload = 0;
        var UserID = getClientInfo("UserID");
        var UserName = getClientInfo("_username");
        $(function () {
            $("#CustomerName_Query").attr("placeholder", "客戶名稱,電話,統編,匯款碼...");
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "lightyellow");
            });
            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });
            var ViewContactUsers = $('#JQDataForm1IsShade').closest('td');
            ViewContactUsers.append($('<a>', { href: 'javascript:void(0)' }).on('click', function () {
                var ContactDescr = $("#JQDataForm1ContactDescr").val();
                if (ContactDescr == "" || ContactDescr == undefined) {
                    alert('注意!!,請先輸入聯絡內容,再設定分享');
                    $("#JQDataForm1ContactDescr").focus();
                    return false;
                }
                var IsShade = $("#JQDataForm1IsShade").checkbox('getValue');
                if (IsShade == 0) {
                    alert('注意!!,要設訂分享聯絡內容時,需先選取遮蔽紀錄');
                    $("#JQDataForm1IsShade").checkbox('setValue', 1);
                    return false;
                }
                var SalesKindID = $("#JQDataForm1SalesKindID").val();
                var FiltStr = "B.SalesKindID = '" + SalesKindID + "'";
                $("#JQDataGrid1").datagrid('setWhere', FiltStr);
                openForm('#JQDialogClu', {}, "", 'dialog');
                return true;
            }).linkbutton({ text: '分享給' }));
        });
        $(document).ready(function () {
            userSalesTypeIDs = GetSalesTypeID();
            userSalesKindIDs = GetSalesKindID();
            userCustomerTypeID = GetUserCustomerTypeID(UserID);
            $('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });
            //$("#CustomerName_Query").focus(function () {
            //    var field = $(this);
            //    if (field.val() == '客戶代號/名稱/簡稱/電話/統編' ) {
            //        field.val('');
            //    }
            //});
            var where = "(Customer.CustomerID IN (SELECT CUSTOMERID FROM ContactLogs  WHERE CREATEBY= '" + UserID + "' AND DATEDIFF(DAY,CreateDate,GETDATE())<=15  GROUP BY CUSTOMERID))" 
                      + " OR (Customer.CustomerID IN (SELECT CUSTOMERID FROM Customer WHERE LastUpdateBy= '"+ UserName +"' AND DATEDIFF(DAY,LastUpdateDate,GETDATE())<=15  GROUP BY CUSTOMERID))";
            $("#dataGridView").datagrid('setWhere', where);
            $("#dataGridView").datagrid('selectRow', 0).datagrid('getSelected');
        });
        var DGVSalesTypeCount_FormatScript = function (value, row, index) {
            if (value >= 1) {
                var fieldName = this.field;
                return $('<a>', { href: 'javascript: void(0)', title: '', onclick: "DGVSalesTypeCount_CommandTrigger.call(this,'')" }).linkbutton({ iconCls: '', plain: false, text: value })[0].outerHTML;
            }
        }
        //DataGrid 中SalesTypeCount 點按觸發
        var DGVSalesTypeCount_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
            var rowData = $("#dataGridView").datagrid('selectRow', rowIndex).datagrid('getSelected');
            GetGridDataSalesTypeDate(rowData.CustomerID);
            return true;
        }
      function RefreshGrid() {
            var where = $("#dataGridView").datagrid('getWhere');
            if (customerID != undefined && customerID != "") {
                where = where + "Customer.CustomerID='" + customerID + "'";
            } else { where = where + "Customer.CustomerID='XXXXXXXXX'"; }
            $("#dataGridView").datagrid('setWhere', where);
        }
      function dataGridView_OnLoadSuccess() {
    }
    //銷貨類別dataForm 載入
      function dataForm1_OnLoadSuccess() {
        var row = $("#dataGridView").datagrid('getSelected');
        var CustID = row.CustomerID;
        userSalesTypeIDs = GetSalesTypeID();
        custSalesTypeIDs = GetCustSalesTypeID(CustID);
        var UserID = getClientInfo("UserID");
        if (getEditMode($("#dataForm1")) == "inserted") {
            $("#dataForm1SalesID").combobox('disable');//新增時，業務員預設停用
            var row = $("#dataGridView").datagrid('getSelected');
            if (row != null) {
                //代入選取的客戶資料的客戶代號
                $("#dataForm1CustomerID").val(row.CustomerID);
                if ((custSalesTypeIDs) != "" && (custSalesTypeIDs) != undefined) {
                    var FiltStr = "SalesID = " + "'" + UserID + "'" + " AND (SalesTypeID Not In (" + custSalesTypeIDs + ")  AND (SalesTypeID IN (" + userSalesTypeIDs + ")))";
                }
                else {
                    var FiltStr = "SalesID = " + "'" + UserID + "'" + " AND (SalesTypeID IN (" + userSalesTypeIDs + "))";
                }
                $("#dataForm1SalesTypeID").combobox('setWhere', FiltStr);
             }
            else
                closeForm($("#JQDialog2"));//客戶資料沒選取就關掉

        } else {
               //業務員combobox篩選
                $("#dataForm1SalesID").combobox('enable');//修改時，業務員預設啟用
                $("#dataForm1SalesID").combobox('setWhere', "SalesTypeID = '" + $("#dataForm1SalesTypeID").combobox('getValue') + "'");//業務員combobox篩選
                //var FiltStr = "SalesID = " + "'" + UserID + "'" + " AND SalesTypeID in (" + CustomerSaleTypes + ")";
                $("#dataForm1SalesTypeID").combobox('setWhere', '1=1');
         }
    }
    //業務客戶資料dataForm 載入
    function dataFormMaster_OnLoadSuccess(row) {
          if (getEditMode($("#dataFormMaster")) == 'inserted') {
              $("#divSalesType").hide(); //隱藏銷貨類別GRID
              $("#dataFormMasterCustomerTypeID").combobox('setValue', userCustomerTypeID);
              if (userSalesKindIDs.length < 43) {
                  $("#dataFormMasterUserSalesKind").options('setValue', userSalesKindIDs);
              }
              else {
                  $("#dataFormMasterUserSalesKind").options('setValue', '');
              }
            $("#dataFormMasterSalesID").combobox('setWhere', 'IsSalesRole = 1');
            $('#dataFormMasterUserSalesKind').closest('td').prev('td').show();
            $('#dataFormMasterUserSalesKind').closest('td').show();
            var wherestr = "CustomerID='XXXXX'"
            $("#dataGrid1").datagrid('setWhere', wherestr);
            $('#dataFormMasterShortCode').closest('td').prev('td').hide();
            $('#dataFormMasterShortCode').closest('td').hide();
            ShowDetailsField(1);
          }

          else {
            $("#divSalesType").show();
            $("#dataFormMasterUserSalesKind").options('setValue', '');
            //隱藏更新客戶欄位UserSalesKind
            //$('#dataFormMasterUserSalesKind').closest('td').prev('td').hide();
              //$('#dataFormMasterUserSalesKind').closest('td').hide();
            $('#dataFormMasterShortCode').closest('td').prev('td').show();
            $('#dataFormMasterShortCode').closest('td').show();
            ShowDetailsField(0);
        }
        if ($("#dataFormMasterCustomerTypeID").combobox('getValue') == 1) {
            ShowPersonalField(0);
        }
        else {
            ShowPersonalField(1);
        }
        $("#dataFormMasterAddr_City").combobox('disable');
    }
    //開發客戶資料dataForm 載入
    function JQDataForm3_OnLoadSuccess(row) {
        if (getEditMode($("#JQDataForm3")) == 'inserted') {
            $("#JQDataForm3CustomerTypeID").combobox('setValue', userCustomerTypeID);
            $("#JQDataForm3SalesID").combobox('setWhere', 'IsSalesRole = 1');
        }
        if ($("#JQDataForm3CustomerTypeID").combobox('getValue') == 1) {
            ShowPersonalFieldSale(0);
        }
        else {
            ShowPersonalFieldSale(1);
        }
        $("#JQDataForm3Addr_City").combobox('disable');
    }
    //開發客戶資料存檔
    function JQDataForm3_OnApply() {
        if ($("#JQDataForm3CustomerTypeID").combobox('getValue') == '') {
            alert("客戶屬性 必填");
            return false;
        }
        if ($("#JQDataForm3CustomerTypeID").combobox('getValue') == 1) {
            if ($("#JQDataForm3TelNO").val().trim() == '') {
                alert("當「客戶屬性」為公司，「電話號碼」為必填");
 
                return false;
            }
        }
        if (getEditMode($('#JQDataForm3')) == 'inserted') {
            var vSalesID = $("#JQDataForm3SalesID").combobox('getValue');
            if (vSalesID == '' || vSalesID == undefined) {
                alert('注意!!請選填業務人員!!');
                $("#JQDataForm3SalesID").combobox('textbox').focus();
                return false;
            }
            var CustID = GetCustomerID();
            $("#JQDataForm3CustomerID").val(CustID);
            var flag1 = true;
            var TaxNO = $.trim($("#JQDataForm3TaxNO").val());
            var CustomerName = $.trim($("#JQDataForm3CustomerName").val());
            var TelNO = $.trim($("#JQDataForm3TelNO").val());
            if (TaxNO != '') {//檢查統編有無重複
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer',
                    data: "mode=method&method=" + "CheckDuplicate_TaxNO" + "&parameters=" + TaxNO,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != 'False') {
                            var rows = $.parseJSON(data);
                            if (rows.length != 0) {
                                alert("此統編的客戶已存在");
                                flag1 = false;
                            }
                        } else {
                            alert("檢查統編錯誤");
                            flag1 = false;
                        }
                    }
                });
            } else if (CustomerName != '' && TelNO != '') {//檢查客戶名稱、電話有無重複
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer',
                    data: "mode=method&method=" + "CheckDuplicate_CustomerNameAndTelNO" + "&parameters=" + CustomerName + "," + TelNO,
                    async: false,
                    success: function (data) {
                        if (data != 'False') {
                            var rows = $.parseJSON(data);
                            if (rows.length != 0) {
                                alert("此客戶名稱、電話的客戶已存在");
                                flag1 = false;
                            }
                        } else {
                            alert("檢查客戶名稱、電話錯誤");
                            flag1 = false;
                        }
                    }
                });
            }
            if (flag1 == false) { return false }
        }

    }
    //開發客戶資料存檔後更新資料
    function JQDataForm3_OnApplied() {
        if (getEditMode($("#JQDataForm3")) == 'inserted') {
            var USK = $("#dataFormMasterUserSalesKind").options('getValue');
            setTimeout(function () {
                ExecAddCustomerSaleTypeBySales(1,USK,1,0,1,0);
            }, 500);
            var CustomerID = $("#JQDataForm3CustomerID").val();
            var FiltStr = 'Customer.CustomerID = ' + "'" + CustomerID + "'";
            $("#dataGridView").datagrid('setWhere', FiltStr);
            $('#dataGridView').datagrid('reload');
            //setTimeout(function () {
            //    openForm('#JQDialog7', $('#dataGridView').datagrid('selectRow', 0).datagrid('getSelected'), 'updated', 'dialog');
            //}, 600);
        } else {
        }
    }
    //顯示或隱藏業務客戶個人客戶欄位
    function ShowPersonalField(flag) {
        var FormName = '#dataFormMaster';
        var HideFieldName = ['ARCNO', 'CellPhone'];
        $.each(HideFieldName, function (index, fieldName) {
            if (flag == 0) {
                $(FormName + fieldName).closest('td').prev('td').hide();
                $(FormName + fieldName).closest('td').hide();
            }
            else {
                $(FormName + fieldName).closest('td').prev('td').show();
                $(FormName + fieldName).closest('td').show();
            }
        });
    }
    //顯示或隱藏 開發客戶個人客戶欄位
    function ShowPersonalFieldSale(flag) {
        var FormName = '#JQDataForm3';
        var HideFieldName = ['ARCNO', 'CellPhone'];
        $.each(HideFieldName, function (index, fieldName) {
            if (flag == 0) {
                $(FormName + fieldName).closest('td').prev('td').hide();
                $(FormName + fieldName).closest('td').hide();
            }
            else {
                $(FormName + fieldName).closest('td').prev('td').show();
                $(FormName + fieldName).closest('td').show();
            }
        });
    }
    //顯示或隱藏欄位
    function ShowDetailsField(flag) {
        var FormName = '#dataFormMaster';
        var HideFieldName = ['AccountClerk', 'EmailAddress', 'SalesID', 'BalanceDate', 'DebtorDays', 'TaxType', 'PayWay', 'QInvoiceType'];
        $.each(HideFieldName, function (index, fieldName) {
            if (flag == 0) {
                $(FormName + fieldName).closest('td').prev('td').hide();
                $(FormName + fieldName).closest('td').hide();
            }
            else {
                $(FormName + fieldName).closest('td').prev('td').show();
                $(FormName + fieldName).closest('td').show();
            }
         });
    }
    //取客戶代號
    function GetCustomerID() {
            var CID = '';
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer', 
                data: "mode=method&method=" + "GetCustomerID", 
                cache: false,
                async: false,
                success: function (data) {
                    if (data != 'False') {
                        CID = $.parseJSON(data);
                     } else {
                        alert("客戶代號取號錯誤");
                    }
                }
            });
            return CID;
    }
    //客戶資料dataForm的縣市連動
    function dataFormMasterAddr_Country_OnSelect() {
        var addr_City = $("#dataFormMasterAddr_Country").combobox('getValue');
        $("#dataFormMasterAddr_City").combobox('setWhere', "Country = '" + addr_City + "'");
        $("#dataFormMasterAddr_City").combobox('enable');
    }
    //客戶資料JQDataForm3的縣市連動
    function JQDataForm3Addr_Country_OnSelect() {
        var addr_City = $("#JQDataForm3Addr_Country").combobox('getValue');
        $("#JQDataForm3Addr_City").combobox('setWhere', "Country = '" + addr_City + "'");
        $("#JQDataForm3Addr_City").combobox('enable');
    }
    //dataFormMaster的鄉鎮市連動
    function dataFormMasterAddr_City_OnSelect(rowdata) {
        $("#dataFormMasterZIPCode").val(rowdata.ZIPCode);
        var country=$("#dataFormMasterAddr_Country").combobox('getValue');
        var city=$("#dataFormMasterAddr_City").combobox('getValue');
        $("#dataFormMasterAddr_Desc").val(country+city);
    }
    //JQDataForm3的鄉鎮市連動
    function JQDataForm3Addr_City_OnSelect(rowdata) {
        $("#JQDataForm3ZIPCode").val(rowdata.ZIPCode);
        var country = $("#JQDataForm3Addr_Country").combobox('getValue');
        var city = $("#JQDataForm3Addr_City").combobox('getValue');
        $("#JQDataForm3Addr_Desc").val(country + city);
    }
    function dataForm1SalesTypeID_OnSelect(rowdata) {
        $("#dataForm1SalesTypeName").val(rowdata.SalesTypeName);
        //業務員combobox篩選
        $("#dataForm1SalesID").combobox('enable');
        $("#dataForm1SalesID").combobox('setWhere', "SalesTypeID = '" + rowdata.SalesTypeID + "'");
        $("#dataForm1SalesID").combobox('setValue', '');
        $("#dataForm1SalesKindID").val(rowdata.SalesKindID); //帶入銷貨類別的業務類別
    }
    //客戶資料dataGrid連動
    function dataGridView_OnSelect(index, rowdata) {
        if (rowdata != null && rowdata != undefined) {
            $("#JQDataGrid2").datagrid('setWhere', "CustomerID='" + rowdata.CustomerID+"'");
            if (userSalesTypeIDs != "") {
                $("#dataGrid1").datagrid('setWhere', "C.CustomerID = '" + rowdata.CustomerID + "'  and c.SalesTypeID in (" + userSalesTypeIDs + ")");
            } else {
                $("#dataGrid1").datagrid('setWhere', "C.CustomerID = '" + rowdata.CustomerID + "'  and c.SalesTypeID = 'XXXXXX'");
            }
        }
        else
            $("#JQDataGrid2").datagrid('setWhere', "CustomerID='XXXXXXX'");
    }
    //客戶資料dataForm_OnApply檢查
    function dataFormMaster_OnApply() {
        if ($("#dataFormMasterCustomerTypeID").combobox('getValue') == '') {
                alert("注意!! 客戶屬性 必填");
                return false;
        }
        if ($("#dataFormMasterCustomerTypeID").combobox('getValue') == 1) {
            if ($("#dataFormMasterTelNO").val().trim() == '') {
                alert("注意!! 當「客戶屬性」為公司，「電話號碼」為必填");
                return false;
            }
        }
        var taxId = $("#dataFormMasterTaxNO").val().trim();
        if (taxId != '') {
            if (!IsGuiNumberValid(taxId)) {
                alert('注意!!統一編號格式錯誤,請修正');
                $("#dataFormMasterTaxNO").focus();
                return false;
            }
        }

        //if ($("#dataFormMasterCustomerTypeID").combobox('getValue') == '1') {
        //    if ($("#dataFormMasterTaxNO").val().trim() == '') {
        //        alert("您的「客戶屬性」為公司，「統一編號」為必填");
        //        return false;
        //    }
        //}
     
        if (getEditMode($('#dataFormMaster')) == 'inserted') {
            var vSalesID = $("#dataFormMasterSalesID").combobox('getValue');
            if (vSalesID == '' || vSalesID == undefined) {
                alert('注意!!請選填業務人員!!');
                $("#dataFormMasterSalesID").combobox('textbox').focus();
                //$("#dataFormMasterSalesID").focus();
                return false;
            }
            var CustID = GetCustomerID();
            $("#dataFormMasterCustomerID").val(CustID);
            var flag1 = true;
            var TaxNO = $.trim($("#dataFormMasterTaxNO").val());
            var CustomerName = $.trim($("#dataFormMasterCustomerName").val());
            var TelNO = $.trim($("#dataFormMasterTelNO").val());
            if (TaxNO != '') {//檢查統編有無重複
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer', 
                    data: "mode=method&method=" + "CheckDuplicate_TaxNO" + "&parameters=" + TaxNO, 
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != 'False') {
                            var rows = $.parseJSON(data);
                            if (rows.length != 0) {
                                alert("此統一編號客戶已存在,請查明!!");
                                flag1 = false;
                            }
                        } else {
                            alert("檢查統編錯誤");
                            flag1 = false;
                        }
                    }
                });
            } else if (CustomerName != '' && TelNO != '') {//檢查客戶名稱、電話有無重複
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer', 
                    data: "mode=method&method=" + "CheckDuplicate_CustomerNameAndTelNO" + "&parameters=" + CustomerName + "," + TelNO, 
                    async: false,
                    success: function (data) {
                        if (data != 'False') {
                            var rows = $.parseJSON(data);
                            if (rows.length != 0) {
                                alert("注意!! 此電話號碼客戶已存在,請查明");
                                flag1 = false;
                            }
                        } else {
                            alert("檢查客戶名稱、電話錯誤");
                            flag1 = false;
                        }
                    }
                });
            }
            if (flag1 == false) { return false }
        }
        var STCount = $("#dataFormMasterSTCount").val();
        var USK = $("#dataFormMasterUserSalesKind").options('getValue');
        if ((USK.length) <=0  && STCount > 0) {
            var YN = confirm("提醒!!未選取[同步客戶],要[同步客戶]資料嗎?按[確定]後選取[同步客戶],不更新按[取消]");
            if (YN == true) {
                return false;
            }
        }
    }
    //銷貨類別OnApply檢查
    function dataForm1_OnApply() {
        if ($("#dataForm1CustomerID").val().trim() == '') {
            alert("請先選取客戶資料一筆");
            return false;
        }
        if ($("#dataForm1SalesTypeID").combobox('getValue').trim() == '') {
            alert("請先選取「銷貨類別」");
            return false;
        }
        if ($("#dataForm1SalesID").combobox('getValue').trim() == '') {
            alert("請先選取「業務員」");
            return false;
        }
    }
    //銷貨類別新增存檔後檢查
    function dataForm1_OnApplied() {
        //傳入新增或修改的銷貨類別
        var SalesTypeID = $("#dataForm1SalesTypeID").combobox('getValue');
        ExecAddCustomerSaleTypeBySales(2,SalesTypeID,1,1,1,1); //
        return true;
    }
    //執行新增客戶資料,
    function ExecAddCustomerSaleTypeBySales(Ty,Para2,FormType,SyncSalesType,SyncSalesKind,SyncCustomer) {
        //Ty=1:客戶資料新增,Ty=2:銷貨類別新增
        //Para2:使用者選取要同步的客戶資料
        //FormType=1:開發客戶  FormType=2:業務客戶
        if (Ty == 1) {
            if (FormType == 2) {
                var CustomerID = $("#dataFormMasterCustomerID").val();
                var customerName = $("#dataFormMasterCustomerName").val();
                var SalesID = $("#dataFormMasterSalesID").combobox('getValue');
                var TaxType = $("#dataFormMasterTaxType").combobox('getValue');
                var PayWay = $("#dataFormMasterPayWay").combobox('getValue');
                var BalanceDate = $("#dataFormMasterBalanceDate").val();
                var DebtorDays = $("#dataFormMasterDebtorDays").val();
                var AccountClerk = $("#dataFormMasterAccountClerk").val();
                var EmailAddress = $("#dataFormMasterEmailAddress").val();
                var QInvoiceType = $("#dataFormMasterQInvoiceType").combobox('getValue');
                var telNO = $("#dataFormMasterTelNO").val();
            }
            else {
                var CustomerID = $("#JQDataForm3CustomerID").val();
                var customerName = $("#JQDataForm3CustomerName").val();
                var SalesID = $("#JQDataForm3SalesID").combobox('getValue');
                var TaxType = '';
                var PayWay = '';
                var BalanceDate = 0;
                var DebtorDays = 0;
                var AccountClerk = '';
                var EmailAddress = '';
                var QInvoiceType = '';
                var telNO = $("#JQDataForm3TelNO").val();
            }
            AddCustomerSaleTypeBySales(CustomerID, SalesID, TaxType, PayWay, BalanceDate, DebtorDays, AccountClerk, EmailAddress, QInvoiceType, Para2, SyncSalesType,SyncSalesKind,SyncCustomer,getClientInfo("userid"));
        }
        else {
            var CustomerID = $("#dataFormMasterCustomerID").val();
            var SalesID = $("#dataForm1SalesID").combobox('getValue');
            AddCustomerSaleTypeBySalesType(CustomerID,SalesID,Para2,getClientInfo("userid"));
        }
     }
    //新增後能看到新增的紀錄且方便新增付款方式
    function dataFormMaster_OnApplied() {
        var USK = $("#dataFormMasterUserSalesKind").options('getValue');
            setTimeout(function () {
               ExecAddCustomerSaleTypeBySales(1,USK,2,1,1,1);
               }, 500);
        if (getEditMode($("#dataFormMaster")) == 'inserted') {
            var CustomerID = $("#dataFormMasterCustomerID").val();
            var FiltStr = 'Customer.CustomerID = ' + "'" + CustomerID + "'";
            $("#dataGridView").datagrid('setWhere', FiltStr);
            $('#dataGridView').datagrid('reload');
            setTimeout(function () {
               openForm('#JQDialog1',$('#dataGridView').datagrid('selectRow',0).datagrid('getSelected'),'updated', 'dialog');
            }, 600);
            //存完檔,顯示SalesTypeGrid
            $("#divSalesType").show();
        } else {
            $('#dataGridView').datagrid('reload'); //queryGrid有時搜不出來 且 修改後 客戶資料dataGrie不會reload(猜是datagrid.alwaysclose=true)
        }
    }
   //依業務人員銷貨類別權限=>新增銷貨類別/業務類別/依銷貨類別SyncDB新增相關資料
    function AddCustomerSaleTypeBySales(CustomerID, SalesID, TaxType, PayWay, BalanceDate, DebtorDays, AccountClerk, EmailAddress, QInvoiceType, sUSK, SyncSalesType,SyncSalesKind,SyncCustomer,UserID) {
             $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer', 
                data: "mode=method&method=" + "procAddCustomerSaleTypeBySales" + "&parameters=" + encodeURIComponent(CustomerID + "*" + SalesID + "*" + TaxType + "*" + PayWay + "*" + BalanceDate + "*" + DebtorDays + "*" + AccountClerk + "*" + EmailAddress + "*" + QInvoiceType + "*" + sUSK + "*" + SyncSalesType + "*" + SyncSalesKind + "*" + SyncCustomer + "*" + UserID),
                cache: false,
                async: false,
                success: function (data) {
                    $('#JQDataGrid1').datagrid('reload');
                    $('#JQDataGrid2').datagrid('reload');
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
    }
    //依新增銷貨類別=>新增銷貨類別/業務類別/依銷貨類別SyncDB新增相關資料
    function AddCustomerSaleTypeBySalesType(CustomerID, SalesID, SalesTypeID,UserID) {
         $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer',
            data: "mode=method&method=" + "procAddCustomerSaleTypeBySalesType" + "&parameters=" + encodeURIComponent(CustomerID + "," + SalesID + "," + SalesTypeID + "," + UserID),
            cache: false,
            async: false,
            success: function (data) {
                $('#JQDataGrid1').datagrid('reload');
                $('#JQDataGrid2').datagrid('reload');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }
    //取目前客戶已設定銷貨類別
    function GetUserCustomerTypeID(UserID) {
        var ID = 1;
        $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer',
            data: "mode=method&method=" + "GetUserCustomerTypeID" + "&parameters=" + UserID,
            cache: false,
            async: false,
            success: function (data) {
                if (data != 'False') {
                    ID = data;
                } else {
                }
            }
        }
       );
       return ID;
    }
    //取目前客戶已設定銷貨類別
    function GetCustSalesTypeID(CustID) {
        var IDs = ''
        $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer',
            data: "mode=method&method=" + "GetCustSalesTypeID" + "&parameters=" + CustID,
            cache: false,
            async: false,
            success: function (data) {
                if (data != 'False') {
                    IDs = data;
                } else {
                }
            }
        }
       );
        return IDs;
    }
    //取目前使用者擁有的銷貨類別
    function GetSalesTypeID() {
            var IDs=''
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer', 
                data: "mode=method&method=" + "GetSalesTypeID" + "&parameters=" + getClientInfo("userid"), 
                cache: false,
                async: false,
                success: function (data) {
                    if (data != 'False') {
                        IDs=data;
                    } else {
                   }
                }
             }
           );
          return IDs;
    }
    function GetSalesKindID() {
        var IDs = ''
        $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer',
            data: "mode=method&method=" + "GetSalesKindID" + "&parameters=" + getClientInfo("userid"),
            cache: false,
            async: false,
            success: function (data) {
                if (data != 'False') {
                    IDs = data;
                } else {
                }
            }
        }
       );
      return IDs;
    }
    //查詢
    function queryGrid(dg) {
        var queryArr = [];
        var isQueryFlag = 0;
        var CustStr = $('#CustomerName_Query').val();
        if (CustStr != '' || CustStr == undefined) {
            queryArr.push("(CustomerName LIKE '%" + CustStr + "%')" + " OR (ShortName Like '%" + CustStr + "%')  OR  (TelNO Like '%" + CustStr + "%') OR (TaxNO Like '%" + CustStr + "%') OR (Customer.CustomerID  Like '%" + CustStr + "%') OR (Customer.ShortCode Like '%"+ CustStr + "%')");
            isQueryFlag = 1;
        }
        if ($("#SalesKindID_Query").combobox('getValue') != '' && $("#DevelopLevelID_Query").combobox('getValue') == '') {
            queryArr.push("Customer.CustomerID in (SELECT CustomerID FROM dbo.CustomerSaleKind where SalesKindID='" + $("#SalesKindID_Query").combobox('getValue') + "')");
            isQueryFlag = 1;
        }
        if ($("#SalesKindID_Query").combobox('getValue') != '' && $("#DevelopLevelID_Query").combobox('getValue') != '') {
            queryArr.push("Customer.CustomerID in (SELECT CustomerID FROM dbo.CustomerSaleKind where SalesKindID='" + $("#SalesKindID_Query").combobox('getValue') + "' and DevelopLevelID = '" + $("#DevelopLevelID_Query").combobox('getValue') + "')");
            isQueryFlag = 1;
        }
        if ($("#SalesTypeID_Query").combobox('getValue') != '') {
            queryArr.push("Customer.CustomerID in (SELECT CustomerID FROM dbo.CustomerSaleType where SalesTypeID='" + $("#SalesTypeID_Query").combobox('getValue') + "')");
            isQueryFlag = 1;
        }
        if ($("#CustomerTypeID_Query").combobox('getValue') != '') {
            var CustomerTypeID = $("#CustomerTypeID_Query").combobox('getValue');
            queryArr.push("Customer.CustomerTypeID = '" + CustomerTypeID + "'");
            isQueryFlag = 1;
        }
        if (isQueryFlag == 1) {//有查詢條件
            $("#dataGrid1").datagrid('setWhere',"c.SalesTypeID='xxxxxx'");//清空銷貨類別
            $("#dataGridView").datagrid('setWhere', queryArr.join(" and "));
        } else {//無查詢條件
            $("#dataGrid1").datagrid('setWhere', "c.SalesTypeID='xxxxxx'");
            $("#dataGridView").datagrid('setWhere','');
        }
        var rows = $('#dataGridView').datagrid("getRows");
        if (rows.length <= 0) {
            $("#JQDataGrid2").datagrid('setWhere', '1=2');
            $("#JQDataGrid2").datagrid('reload');
        }
    }
    //刪除客戶檢查有無存在於銷貨主檔裡
    function dataGridView_OnDelete(rowdata) {
        alert('提醒!!刪除功能修正中...,暫停使用');
        return false;
        var stopFlag = false;
        if (rowdata.CustomerID != '') {
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer',
                data: "mode=method&method=" + "SelectSalesMasterWhereCustomerID" + "&parameters=" + rowdata.CustomerID, 
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "False") {
                        var rows = $.parseJSON(data);
                        if (rows.length > 0) {
                            alert("注意!!此客戶已有銷貨紀錄，無法刪除!!");
                            stopFlag = true;
                        } else {
                            stopFlag = false;
                        }
                    } else {
                        alert("檢查過程錯誤，請洽管理室");
                        stopFlag = true;
                    }
                }
            });
        } else {
            alert("注意!!客戶代號空白");
            stopFlag = true;
        }
        if (stopFlag == true) {
            return false;
        } else {
            return true;
        }
    }
    function dataGridView_OnInsert() {
        var CNStr = $('#CustomerName_Query').val();
        if (CNStr == '') {
            alert('注意!!新增客戶資料前,請先以客戶關鍵字查詢客戶是否已存在!!')
            $('#CustomerName_Query').focus();
            return false;
        }
        //if (CNStr == '' || CNStr == '客戶代號/名稱/簡稱/電話/統編') {
        //    alert('注意!!新增客戶資料前,請先以客戶關鍵字查詢客戶是否已存在!!')
        //    $('#CustomerName_Query').focus();
        //    return false;
        //}
        var wherestr = "CustomerID='XXXXX'"
        $("#dataGrid1").datagrid('setWhere', wherestr);
    }
    function dataGrid1_OnInsert() {
        if (getEditMode($("#dataFormMaster")) == 'inserted') {
            alert('注意!!在新增客戶狀態時,系統會依您的權限新增銷貨類別.在修改狀態時,再新增銷貨類別')
            return false;
        }
        var rowdata = $("#dataGridView").datagrid('getSelected');
        if (rowdata == null) {
            alert("請先選取指定客戶");
            return false;
        }
    }
    function JQDataGrid2_OnInsert() {
        var rowdata = $("#dataGridView").datagrid('getSelected');
        if (rowdata == null) {
            alert("請先選取指定客戶");
            return false;
        }
    }
    function dataGrid1_OnLoadSucess() {
        //var row = $("#dataGridView").datagrid('getSelected');
        //$("#JQDataGrid2").datagrid('setWhere', "A.CustomerID='" + row.CustomerID + "'");
    }
    function GetGridDataSalesTypeDate(CustomerID) {
        $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer',
            data: "mode=method&method=" + "GetGridDataSalesTypeDate" + "&parameters=" + CustomerID,
            cache: false,
            async: false,
            success: function (data) {
                var rows = $.parseJSON(data);
                if (rows.length == 0) {
                    $('#JQDataGridSalesType').datagrid('loadData',[]);//清空Grid資料
                } else {
                    if (rows.length > 0) {
                        $('#JQDataGridSalesType').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                        openForm('#JQDialog3', {}, "", 'dialog');
                    }
                }
            }
        }
      );
    }
    //新增開發客戶
    function insertItemSale() {
        var CNStr = $('#CustomerName_Query').val();
        if (CNStr == '' || CNStr == '客戶代號/名稱/簡稱/電話/統編') {
            alert('注意!!新增客戶資料前,請先以客戶關鍵字查詢客戶是否已存在!!')
            $('#CustomerName_Query').focus();
            return false;
        }
        openForm('#JQDialog7', $('#dataGridView').datagrid('selectRow'), 'inserted', 'dialog');
    }
    //GRID 呼叫開啟聯絡紀錄List
    function OpenContactLogsLink(value, row, index) {
        if (value > 0) {
            return $('<a>', { href: 'javascript:void(0)', name: 'OpenContactLogsLink', onclick: 'OpenContactLogs.call(this)', rowIndex: index }).linkbutton({ plain: false, text: value })[0].outerHTML
        }
    }
    function OpenContactLogs() {
        //關閉開啟檢視業務類別檢查功能 CJS 2020/10/21
        //var rows = $("#JQDataGrid2").datagrid('getSelected');
        //if (IsEditableContactLogs(rows.SalesKindID) == 0) {
        //    alert('注意!!你無權限檢視此業務類別聯絡紀錄...');
        //    return false;
        //}
        var custrows = $("#dataGridView").datagrid('getSelected');
        var index = $(this).attr('rowIndex');
        $("#JQDataGrid2").datagrid('selectRow', index);
        var rows = $("#JQDataGrid2").datagrid('getSelected');
        DialogTitle = custrows.ShortName + '_' + rows.SalesKindName + '_聯絡記錄';
        $("#JQDataGridContact").datagrid('getPanel').panel('setTitle', DialogTitle);
        $("#JQDataGridContact").datagrid('options').title = DialogTitle;
        $("#JQDataGridContact").datagrid('setWhere', "CustomerID = '" + rows.CustomerID + "' and SalesKindID = '" + rows.SalesKindID + "'");
        openForm('#JQDialog4', {}, "", 'dialog');
    }
    //GRID 呼叫按鈕
    function OpenEditCustomerLink(value, row, index) {
        if (value == undefined) ""
        else if (value != "0")
            return "<a href='javascript: void(0)' onclick='LinkEditCustomer(" + index + ");' >" + value + "</a>";
        else
            return value;
    }
    //open客戶編輯畫面 dialog
    function LinkEditCustomer(index) {
        $("#JQDataGrid2").datagrid('selectRow', index);
        var rows = $("#JQDataGrid2").datagrid('getSelected');
        if (IsEditableContactLogs(rows.SalesKindID) == 0) {
            alert('注意!!你無權限編輯此銷貨類別客戶資料...');
            return false;
        }
        if (rows.CRUDUrl != '' || rows.CRUDUrl == undefined) {
            parent.addTab(rows.SalesKindName + '_' + '客戶資料維護', rows.CRUDUrl + rows.CustomerID);
        }
        else {
           alert('注意!!此業務類別無客戶資料維護功能!!');
        }
        return true;
    }
    //新增聯絡紀錄
    function AddContactLogs() {
        var rows = $("#JQDataGrid2").datagrid('getSelected');
        if (IsEditableContactLogs(rows.SalesKindID) == 0) {
            alert('注意!!你無權限新增此業務類別聯絡資料...');
            return false;
        }
        openForm('#JQDialog5', {}, "inserted", 'dialog');
    }
    //更新業務開發狀態
    function UpdateDevelopLevel()
    {
        var rows = $("#JQDataGrid2").datagrid('getSelected');
        var CustomerID = rows.CustomerID;
        $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_Normal_Customer.Customer',
            data: "mode=method&method=" + "procUpdateDevelopLevel" + "&parameters=" + encodeURIComponent(CustomerID),
            cache: false,
            async: false,
            success: function (data) {
               $('#JQDataGrid2').datagrid('reload');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }
    //選取業務客戶類別,顯示或引藏個人欄位
    function CustomerTypeIDOnSelect(row) {
        if (row.CustomerTypeID == 2) {
            ShowPersonalField(1);
        }
        else {
            ShowPersonalField(0);
        }
    }
    //選取開發客戶類別,顯示或引藏個人欄位
    function CustomerTypeIDOnSelectSale(row) {
        if (row.CustomerTypeID == 2) {
            ShowPersonalFieldSale(1);
        }
        else {
            ShowPersonalFieldSale(0);
        }
    }
    function JQDataGridContactOnLoadSucess() {
         
    }
    function JQDataFormOnLoadSucess(row) {
       if (getEditMode($("#JQDataForm1")) == 'viewed') {
            var str = row.ContactDescr;
            var slen = (row.ContactDescr).trim().length;
            if (slen > 0) {
               if (row.IsShade == true && ((row.CreateBy != UserID) && (row.ShareTo.indexOf(UserID) == -1))) {
                    str = ".........聯絡內容遮蔽...........";
                }
            }
            $("#JQDataForm1ContactDescr").val(str);
        }
        if (getEditMode($("#JQDataForm1")) == 'inserted') {
            var dt = new Date();
            $("#JQDataForm1ContactDate").datebox('setValue', $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd'));
        }
        var rows1 = $("#JQDataGrid2").datagrid('getSelected');
        $("#JQDataForm1CustomerID").val(rows1.CustomerID);
        $("#JQDataForm1SalesKindID").val(rows1.SalesKindID);
        $("#JQDataForm1Dialogue").combobox('setWhere', "CustomerID = '" + rows1.CustomerID + "' AND SalesKindID = '" + rows1.SalesKindID + "'");
        if (getEditMode($("#JQDataForm1")) == 'updated') {
            var rows = $("#JQDataGridContact").datagrid('getSelected');
            var fstr = rows.ContactDescr.substr(0,rows.ContactDescr.length);
            $("#JQDataForm1ContactDescr").val(fstr);
        };
    }
    function JQDataFormOnApply() {

        var IsShade=$("#JQDataForm1IsShade").checkbox('getValue');
    }
    function JQDataFormOnApplied() {
        $('#JQDataGrid2').datagrid('reload');
    }
    //在聯絡紀錄列表新增聯絡紀錄
    function JQDataGridContactOnInsert() {
        var rows = $("#JQDataGrid2").datagrid('getSelected');
        if (IsEditableContactLogs(rows.SalesKindID) == 0) {
            alert('注意!!你無權限新增['+rows.SalesKindName+']聯絡紀錄...');
            return false;
        }
    }
    function JQDataGridContactOnDelete(rowData) {
        var rows = $("#JQDataGrid2").datagrid('getSelected');
        var MaxAutoKey = (rows.MaxAutoKey);
        if (rowData.AutoKey < MaxAutoKey) {
            alert('注意!!不是最後一筆資料,不能刪除')
            return false;
        }
        if (rowData.CreateBy != UserID) {
            alert('注意!!,你無權限刪除此筆聯絡紀錄');
            return false;
        }
        return true;
    }
    //聯絡紀錄刪除完成,執行聯絡紀錄Grid刷新
    function JQDataGridContactOnDeleted(rowData) {
        $('#dataGridView').datagrid('reload');
    }
    function JQDataGridContactUpDate(rowData) {
        var rows = $("#JQDataGrid2").datagrid('getSelected');
        var MaxAutoKey = (rows.MaxAutoKey);
        if (rowData.AutoKey < MaxAutoKey) {
            alert('注意!!不是最後一筆資料,不能修改')
            return false;
        }
        if (rowData.CreateBy != UserID) {
            alert('注意!!,你無權限修改此筆聯絡紀錄');
            return false;
        }
        return true;
    }
    //在JQDataGrid2中,顯示ContactDescr文字內容
    function ShowContactJDG2(value, row) {
        var str = '';
        var ShareTo = '';
        if (row.ShareTo != null) {
            var ShareTo = row.ShareTo;
        }
        var slen = (value).trim().length;
        if (slen > 0) {
            if (row.IsShade == true && ((row.LastCreateBy != UserID) && (ShareTo.indexOf(UserID) == -1))) {
                str = ".........聯絡內容遮蔽...........";
            }
            else {
                str = value
            }
        }
        return str ;
        //return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + str + "</p>";
        }
    //在JQDataGridContact中,顯示ContactDescr文字內容
    function ShowContactJDGC(value, row) {
        var str = '';
        var ShareTo = '';
        if (row.ShareTo != null) {
            var ShareTo = row.ShareTo;
        }
        var slen = (value).trim().length;
        if (slen > 0) {
            if (row.IsShade == true && ((row.CreateBy != UserID) && (ShareTo.indexOf(UserID) == -1))) {
                str = ".........聯絡內容遮蔽...........";
            }
             else {
                 str = value;
            }
        }
        return str;
        //return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + str + "</p>";
    }
    function ShowAllGrid(value, row) {
        return value ;
        //return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
    }
   //檢查有無權限修改銷類別
    function dataGrid1_OnUpdate(rows) {
        var result = true;
        if (IsEditable(rows.SalesTypeID) == 0) {
            alert('注意!!你無權編輯此銷貨類別資料...');
            result = false;
        }
        return result;
    }
   //檢查有無權限刪除銷貨類別
    function dataGrid1_OnDelete(rows) {
        var result = true;
        if (IsEditable(rows.SalesTypeID) == 0) {
            alert('注意!!你無權刪除此銷貨類別資料...');
            result = false;
        }
        if ((rows.DealCount) > 0) {
            alert('注意!!此客戶與交易類別已有銷貨資料,無法刪除')
            result = false;
        }
        return result;
    }
    //是否可編輯或檢視業務類別聯絡紀錄 
    function IsEditable(SType) {
        var vSType = "'" + SType.trim() + "'";
        var result = 1;
        if (userSalesTypeIDs.indexOf(vSType) == -1) {
            result = 0;
        }
        return result;
    }
    function IsEditableContactLogs(SType) {
        //var vSType = "'" + SType.trim() + "'";
        var vSType =  SType.trim();
        var result = 1;
        if (userSalesKindIDs.indexOf(vSType) == -1) {
            result = 0;
        }
        return result;
    }
    function OnBlurCustName() {
        if ($("#dataFormMasterShortName").val() == '') {
            var CustName = $("#dataFormMasterCustomerName").val().toString().substr(0, 6);
            $("#dataFormMasterShortName").val(CustName);
        }
    }
    function OnBlurTaxNO() {
        var TaxNO = $("#dataFormMasterTaxNO").val().trim();
        $("#dataFormMasterTaxNO").val(TaxNO);
    }
    //離開客戶名稱_開發客戶
    function OnBlurCustNameSale() {
        if ($("#JQDataForm3ShortName").val() == '') {
            var CustName = $("#JQDataForm3CustomerName").val().toString().substr(0, 6);
            $("#JQDataForm3ShortName").val(CustName);
        }
    }
    function SalesKindQueryOnSelect(rowdata) {
        $('#CustomerName_Query').val('');
        $("#SalesTypeID_Query").combobox('setWhere', "SalesKindID = '" + rowdata.SalesKindID + "'");
    }
   
    function JQDataForm2_OnApply() {
    }
    function JQDataForm2_OnLoadSucess(row) {
        //if (getEditMode($("#JQDataForm2")) == 'viewed') {
        //    var str = row.LastCall;
        //    alert(str);
        //    var slen = row.LastCall.trim().length;
        //    if (slen > 0) {
        //        alert('in');
        //        alert(row.IsShade);
        //        if (row.IsShade == true && ((row.LastCreateBy != UserID) && (row.ShareTo.indexOf(UserID) == -1))) {
        //            str = ".........聯絡內容遮蔽...........";
        //        }
        //    }
        //    $("#JQDataForm2LastCall").val(str);
        //}
        if (getEditMode($("#JQDataForm2")) == 'inserted') {
            var rows = $("#dataGrid1").datagrid('getSelected');
            $("#JQDataForm2CustomerID").val(rows.CustomerID);
        }
        $("#JQDataForm2LastCall").attr('disabled', true);
        $("#JQDataForm2LastCreateBy").attr('disabled', true);
        $("#JQDataForm2LastCallDate").datebox({'disabled':true});
    }
    //
    function JQDialogCluOnSubmited() {
        var rows = $('#JQDataGrid1').datagrid("getChecked");
        var count = rows.length;
        if (count == 0) {
            alert('注意!!未選取任何業務人員,請選取');
            return false;
        }
        var ShareTo = '';
        var ShareToName = '';
        for (var i = 0; i <= rows.length - 1; i++) {
            if (i > 0) {
                ShareTo = ShareTo + ',' + rows[i].SalesID;
                ShareToName = ShareToName + ',' + rows[i].SalesName;
            }
            else {
                ShareTo = ShareTo + rows[0].SalesID;
                ShareToName = ShareToName + ',' + rows[i].SalesName;
            }
        }
        $("#JQDataForm1ShareTo").val(ShareTo);
        $("#JQDataForm1ShareToName").val(ShareToName);
        return true;
    }
    function JQDataGrid1OnLoadSuccess() {
        var ShareTo = $("#JQDataForm1ShareTo").val();
        if (ShareTo.length > 0) {
            var rows = $("#JQDataGrid1").datagrid("getRows");
            for (var k = 0; k < rows.length; k++) {
                if (ShareTo.indexOf(rows[k].SalesID) != -1) {
                    $('#JQDataGrid1').datagrid("checkRow",k);
                }
            }
        }
    }
    //過濾
    function UserSalesKindOnWhere() {
        return "A.SalesID = "+"'"+UserID+"'";
    }
    //檢查統編是否正確
    function IsGuiNumberValid(input) {
        var GUI_NUMBER_COEFFICIENTS = [1, 2, 1, 2, 1, 2, 4, 1];
        try {
            var n_1 = input.toString();
            var regex = /^\d{8}$/;
            if (!regex.test(n_1)) {
                throw new Error('統一編號為8位數字');
            }
            var checksum = GUI_NUMBER_COEFFICIENTS.reduce(function (sum, c, index) {
                var product = c * parseInt(n_1.charAt(index), 10); // Step 1
                return sum + (product % 10) + Math.floor(product / 10); // Step 2
            }, 0);
            if (
                // Step 3 & Step 4
            checksum % 10 === 0 ||
                (parseInt(n_1.charAt(6), 10) === 7 && (checksum + 1) % 10 === 0)) {
                return true;
            }
            return false;
        }
        catch (e) {
            console.error(e.message);
            return false;
        }
    }
   </script>
  
</head>
   
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERP_Customer_Normal_Customer.Customer" runat="server" AutoApply="True"
                DataMember="Customer" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="" AlwaysClose="True" OnSelect="dataGridView_OnSelect" QueryMode="Panel" AllowAdd="True" AllowDelete="True" AllowUpdate="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnLoadSuccess="dataGridView_OnLoadSuccess" OnDelete="dataGridView_OnDelete" OnInsert="dataGridView_OnInsert" Width="1120px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustomerID" Format="" MaxLength="0" Visible="true" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerName" Format="" MaxLength="0" Visible="true" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="銷類數" Editor="text" FieldName="STCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="TelNO" Format="" MaxLength="0" Visible="true" Width="90" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNO" Format="" MaxLength="0" Visible="true" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="最早交易日" Editor="text" FieldName="FirstDealDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="最早銷類" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Customer_Normal_Customer.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="FirstSaleType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="最近交易日" Editor="text" FieldName="LastestDealDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="最近銷類" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Customer_Normal_Customer.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LastestSaleType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="交易類別筆數" Editor="text" FieldName="SalesTypeCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" FormatScript="DGVSalesTypeCount_FormatScript">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="總交易筆數" Editor="text" FieldName="SalesCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶屬性" Editor="text" FieldName="CustomerTypeName" MaxLength="0" Visible="true" Width="70" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="傳真" Editor="text" FieldName="FaxNO" MaxLength="0" Visible="False" Width="85" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="縣市" Editor="text" FieldName="Addr_Country" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50" Format="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="鄉鎮區" Editor="text" FieldName="Addr_City" Format="" MaxLength="0" Visible="False" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="郵遞區號" Editor="text" FieldName="ZIPCode" Format="" MaxLength="0" Visible="False" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr_Desc" Format="" MaxLength="0" Visible="False" Width="240" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="捐贈註記" Editor="numberbox" FieldName="DonateMark" Format="" MaxLength="0" Visible="False" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="捐贈註記" Editor="text" FieldName="DonateMarkName" Visible="False" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="社福愛心碼" Editor="text" FieldName="NPOBAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="80" Format="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="60" Format="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CreateDate" Format="" MaxLength="0" Visible="False" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" Format="" Visible="False" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" Format="" MaxLength="0" Visible="False" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustomerSaleTypes" Editor="text" FieldName="CustomerSaleTypes" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortName" Editor="text" FieldName="ShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="DevelopLevelID" Editor="text" FieldName="DevelopLevelID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                  <%--      <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />--%>
                  <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItemSale" Text="新增開發客戶" />
                  <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增業務客戶" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶搜尋" Condition="%%" DataType="string" Editor="text" FieldName="CustomerName" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="180" EditorOptions="" DefaultValue="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務類別" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesKindID',textField:'SalesKindName',remoteName:'sERP_Customer_Normal_Customer.SalesKind',tableName:'SalesKind',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:SalesKindQueryOnSelect,panelHeight:200" FieldName="SalesKindID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="狀態" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'DevelopLevelID',textField:'DevelopLevelName',remoteName:'sERP_Customer_Normal_Customer.DevelopLevel',tableName:'DevelopLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="DevelopLevelID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨類別" Condition="=" DataType="string" Editor="infocombobox" FieldName="SalesTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="True" RowSpan="0" Span="0" Width="120" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Customer_Normal_Customer.SalesKindSalesType',tableName:'SalesKindSalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶屬性" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustomerTypeID',textField:'CustomerTypeName',remoteName:'sERP_Customer_Normal_Customer.CustomerType',tableName:'CustomerType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustomerTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="業務客戶" Width="1080px" DialogLeft="10px" DialogTop="60px" ShowSubmitDiv="True">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Customer" HorizontalColumnsCount="5" RemoteName="sERP_Customer_Normal_Customer.Customer" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormMaster_OnLoadSuccess" OnApply="dataFormMaster_OnApply" OnApplied="dataFormMaster_OnApplied" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustomerID" Format=""  ReadOnly="True" Span="1" Width="80" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerName" Format="" maxlength="0" Width="210" Span="2" OnBlur="OnBlurCustName" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簡稱" Editor="text" FieldName="ShortName" Format="" maxlength="0" Width="90" ReadOnly="False" Span="1" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="網站" Editor="text" FieldName="SiteUrl" maxlength="0" Width="200" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="產業別" Editor="inforefval" EditorOptions="title:'請輸入關鍵字後滑鼠離開焦點可做搜尋',panelWidth:500,remoteName:'sERP_Customer_Normal_Customer.Industry',tableName:'Industry',columns:[{field:'CategoryName',title:'產業類別',width:485,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CategoryID',textField:'CategoryName',valueFieldCaption:'CategoryID',textFieldCaption:'CategoryName',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="Industry" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="514" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話國碼" Editor="infocombobox" EditorOptions="valueField:'ContryAreaID',textField:'IDName',remoteName:'sERP_Customer_Normal_Customer.CountryArea',tableName:'CountryArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="TelCountryArea" Width="88" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="區碼" Editor="text" FieldName="TelArea" Width="30" Span="1" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" - " Editor="text" FieldName="TelNO" Format="" Width="80" EditorOptions="" Span="1" maxlength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳真" Editor="text" FieldName="FaxNO" Width="90" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNO" Format="" Width="80" maxlength="8" NewRow="False" Span="1" OnBlur="OnBlurTaxNO" />
                        <JQTools:JQFormColumn Alignment="left" Caption="縣市" Editor="infocombobox" FieldName="Addr_Country" Format="" maxlength="0" Width="87" EditorOptions="valueField:'Country',textField:'Country',remoteName:'sERP_Customer_Normal_Customer.Country',tableName:'Country',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:dataFormMasterAddr_Country_OnSelect,panelHeight:200" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="鄉鎮區" Editor="infocombobox" FieldName="Addr_City" Format="" maxlength="0" Width="80" EditorOptions="valueField:'City',textField:'City',remoteName:'sERP_Customer_Normal_Customer.City',tableName:'City',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:dataFormMasterAddr_City_OnSelect,panelHeight:200" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" - " Editor="text" FieldName="ZIPCode" Format="" Width="80" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr_Desc" Format="" Width="360" Span="2" Visible="True" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="捐贈註記" Editor="infocombobox" FieldName="DonateMark" Format="" Width="80" EditorOptions="valueField:'DonateMarkID',textField:'DonateMark',remoteName:'sERP_Customer_Normal_Customer.DonateMark',tableName:'DonateMark',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="False" maxlength="0" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="社福愛心碼" Editor="text" FieldName="NPOBAN" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶屬性" Editor="infocombobox" FieldName="CustomerTypeID" maxlength="0" Width="86" Visible="True" EditorOptions="valueField:'CustomerTypeID',textField:'CustomerTypeName',remoteName:'sERP_Customer_Normal_Customer.CustomerType',tableName:'CustomerType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:CustomerTypeIDOnSelect,panelHeight:200" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="居留證號" Editor="text" FieldName="ARCNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="CellPhone" MaxLength="0" ReadOnly="False" Span="1" Width="80" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款碼" Editor="textarea" FieldName="ShortCode" MaxLength="300" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="230" EditorOptions="height:80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳務人員" Editor="text" FieldName="AccountClerk" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電子信箱" Editor="text" FieldName="EmailAddress" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="245" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務人員" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERP_Customer_Normal_Customer.SalesPerson',tableName:'SalesPerson',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結帳日" Editor="text" FieldName="BalanceDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳款天數" Editor="text" FieldName="DebtorDays" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="稅別" Editor="infocombobox" EditorOptions="valueField:'TaxTypeID',textField:'TaxTypeName',remoteName:'sERP_Customer_Normal_Customer.TaxType',tableName:'TaxType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="TaxType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="255" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款方式" Editor="infocombobox" EditorOptions="valueField:'PayWayID',textField:'PayWayName',remoteName:'sERP_Customer_Normal_Customer.PayWay',tableName:'PayWay',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayWay" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單據類別" Editor="infocombobox" EditorOptions="valueField:'InvoiceTypeID',textField:'InvoiceTypeName',remoteName:'sERP_Customer_Normal_Customer.InvoiceType',tableName:'InvoiceType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="QInvoiceType" Visible="True" Width="86" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CreateDate" Format="" Width="185" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="185" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CustomerSaleTypes" Editor="text" FieldName="CustomerSaleTypes" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="STCount" Editor="text" FieldName="STCount" ReadOnly="False" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="同步客戶" Editor="infooptions" FieldName="UserSalesKind" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="80" EditorOptions="title:'JQOptions',panelWidth:980,remoteName:'sERP_Customer_Normal_Customer.UserSalesKind',tableName:'UserSalesKind',valueField:'SalesKindID',textField:'SalesKindName',columnCount:10,multiSelect:true,openDialog:false,selectAll:true,onWhere:UserSalesKindOnWhere,selectOnly:false,items:[]" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="CustomerID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="886" FieldName="TelCountryArea" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2" FieldName="DonateMark" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="TaxType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="PayWay" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="25" FieldName="BalanceDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="30" FieldName="DebtorDays" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="98" FieldName="QInvoiceType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="03" FieldName="TelArea" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="Addr_Country" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="Addr_City" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustomerName" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ShortName" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustomerTypeID" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <div ID="divSalesType">
                <JQTools:JQDataGrid ID="dataGrid1" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="CustomerSaleType" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40" PageSize="5" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERP_Customer_Normal_Customer.CustomerSaleType" RowNumbers="True" Title="銷貨類別" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" EditDialogID="JQDialog2" OnInsert="dataGrid1_OnInsert" OnUpdate="dataGrid1_OnUpdate" OnDelete="dataGrid1_OnDelete" MultiSelectGridID="" OnLoadSuccess="dataGrid1_OnLoadSucess" Width="1020px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustomerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Customer_Normal_Customer.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="text" FieldName="ShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="業務員" Editor="infocombobox" FieldName="SalesID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERP_Customer_Normal_Customer.SalesPerson',tableName:'SalesPerson',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="業務員" Editor="text" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="稅別" Editor="text" FieldName="TaxType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50" EditorOptions="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="稅別" Editor="text" FieldName="TaxTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="付款方式" Editor="infocombobox" FieldName="PayWay" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" EditorOptions="valueField:'PayWayID',textField:'PayWayName',remoteName:'sERP_Customer_Normal_Customer.PayWay',tableName:'PayWay',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="付款方式" Editor="text" FieldName="PayWayName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="結帳日" Editor="text" FieldName="BalanceDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="帳款天數" Editor="text" FieldName="DebtorDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="帳務人員" Editor="text" FieldName="AccountClerk" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="電子信箱" Editor="text" FieldName="EmailAddress" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="單據類別" Editor="infocombobox" FieldName="QInvoiceType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="60" EditorOptions="valueField:'InvoiceTypeID',textField:'InvoiceTypeName',remoteName:'sERP_Customer_Normal_Customer.InvoiceType',tableName:'InvoiceType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="以交易筆數" Editor="text" FieldName="DealCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="text" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="LastUpdateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-add"  ItemType ="easyui-linkbutton"  OnClick="insertItem"    Text="新增" Visible="True" />
                </TooItems>
            </JQTools:JQDataGrid>
            </div>
            <JQTools:JQDialog ID="JQDialog2" runat="server" Title="銷貨類別" BindingObjectID="dataForm1" DialogLeft="20px" Width="1030px" ShowSubmitDiv="True">
                <JQTools:JQDataForm ID="dataForm1" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="CustomerSaleType" disapply="False" DivFramed="False" DuplicateCheck="True" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataGrid1" RemoteName="sERP_Customer_Normal_Customer.CustomerSaleType" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataForm1_OnLoadSuccess" OnApply="dataForm1_OnApply" OnApplied="dataForm1_OnApplied">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustomerID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Customer_Normal_Customer.SalesSalesType',tableName:'SalesSalesType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:dataForm1SalesTypeID_OnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesTypeName" Editor="text" FieldName="SalesTypeName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" EditorOptions="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務員" Editor="infocombobox" FieldName="SalesID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERP_Customer_Normal_Customer.SalesSalesType',tableName:'SalesSalesType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="稅別" Editor="infocombobox" FieldName="TaxType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" EditorOptions="valueField:'TaxTypeID',textField:'TaxTypeName',remoteName:'sERP_Customer_Normal_Customer.TaxType',tableName:'TaxType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款方式" Editor="infocombobox" FieldName="PayWay" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" EditorOptions="valueField:'PayWayID',textField:'PayWayName',remoteName:'sERP_Customer_Normal_Customer.PayWay',tableName:'PayWay',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收款結帳日" Editor="numberbox" FieldName="BalanceDate" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="122" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳款天數" Editor="numberbox" FieldName="DebtorDays" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="115" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳務人員" Editor="text" FieldName="AccountClerk" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="170" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電子信箱" Editor="text" FieldName="EmailAddress" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="220" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單據類別" Editor="infocombobox" EditorOptions="valueField:'InvoiceTypeID',textField:'InvoiceTypeName',remoteName:'sERP_Customer_Normal_Customer.InvoiceType',tableName:'InvoiceType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="QInvoiceType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正日期" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesKindID" Editor="text" FieldName="SalesKindID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="default1" runat="server" BindingObjectID="dataForm1">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="TaxType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2" FieldName="PayWay" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="25" FieldName="BalanceDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="60" FieldName="DebtorDays" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="98" FieldName="QInvoiceType" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validate1" runat="server" BindingObjectID="dataForm1">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesTypeID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DebtorDays" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialog7" runat="server" BindingObjectID="JQDataForm3" Title="開發客戶" Width="1060px" DialogLeft="10px" DialogTop="60px">
                <JQTools:JQDataForm ID="JQDataForm3" runat="server" DataMember="Customer" HorizontalColumnsCount="5" RemoteName="sERP_Customer_Normal_Customer.Customer" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="JQDataForm3_OnLoadSuccess" OnApply="JQDataForm3_OnApply" OnApplied="JQDataForm3_OnApplied" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustomerID" Format="" maxlength="0" Width="80" ReadOnly="True" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerName" Format="" maxlength="0" Width="210" Span="2" OnBlur="OnBlurCustNameSale" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簡稱" Editor="text" FieldName="ShortName" Format="" maxlength="0" Width="90" ReadOnly="False" Span="1" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="網站" Editor="text" FieldName="SiteUrl" maxlength="0" Width="200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="產業別" Editor="inforefval" EditorOptions="title:'請輸入關鍵字後滑鼠離開焦點可做搜尋',panelWidth:500,remoteName:'sERP_Customer_Normal_Customer.Industry',tableName:'Industry',columns:[{field:'CategoryName',title:'產業類別',width:485,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CategoryID',textField:'CategoryName',valueFieldCaption:'CategoryID',textFieldCaption:'CategoryName',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="Industry" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="514" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話國碼" Editor="infocombobox" EditorOptions="valueField:'ContryAreaID',textField:'IDName',remoteName:'sERP_Customer_Normal_Customer.CountryArea',tableName:'CountryArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="TelCountryArea" maxlength="0" Width="88" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="區碼" Editor="text" FieldName="TelArea" Width="30" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" - " Editor="text" FieldName="TelNO" Format="" Width="80" EditorOptions="" Span="1" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳真" Editor="text" FieldName="FaxNO" Width="90" Span="1" maxlength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNO" Format="" Width="80" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="縣市" Editor="infocombobox" FieldName="Addr_Country" Format="" maxlength="0" Width="87" EditorOptions="valueField:'Country',textField:'Country',remoteName:'sERP_Customer_Normal_Customer.Country',tableName:'Country',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:JQDataForm3Addr_Country_OnSelect,panelHeight:200" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="鄉鎮區" Editor="infocombobox" FieldName="Addr_City" Format="" maxlength="0" Width="80" EditorOptions="valueField:'City',textField:'City',remoteName:'sERP_Customer_Normal_Customer.City',tableName:'City',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:JQDataForm3Addr_City_OnSelect,panelHeight:200" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" - " Editor="text" FieldName="ZIPCode" Format="" maxlength="0" Width="80" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr_Desc" Format="" Width="360" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="捐贈註記" Editor="infocombobox" FieldName="DonateMark" Format="" Width="80" EditorOptions="valueField:'DonateMarkID',textField:'DonateMark',remoteName:'sERP_Customer_Normal_Customer.DonateMark',tableName:'DonateMark',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="社福愛心碼" Editor="text" FieldName="NPOBAN" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶屬性" Editor="infocombobox" FieldName="CustomerTypeID" maxlength="0" Width="86" Visible="True" EditorOptions="valueField:'CustomerTypeID',textField:'CustomerTypeName',remoteName:'sERP_Customer_Normal_Customer.CustomerType',tableName:'CustomerType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:CustomerTypeIDOnSelectSale,panelHeight:200" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="居留證號" Editor="text" FieldName="ARCNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="CellPhone" MaxLength="0" ReadOnly="False" Span="1" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務人員" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERP_Customer_Normal_Customer.SalesPerson',tableName:'SalesPerson',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CreateDate" Visible="False" Width="185" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="185" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CustomerSaleTypes" Editor="text" FieldName="CustomerSaleTypes" Visible="False" Width="80" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="同步客戶" Editor="infooptions" FieldName="UserSalesKind" Width="80" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" EditorOptions="title:'JQOptions',panelWidth:980,remoteName:'sERP_Customer_Normal_Customer.UserSalesKind',tableName:'UserSalesKind',valueField:'SalesKindID',textField:'SalesKindName',columnCount:10,multiSelect:true,openDialog:false,selectAll:true,onWhere:UserSalesKindOnWhere,selectOnly:false,items:[]" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="JQDefault2" runat="server" BindingObjectID="JQDataForm3" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="CustomerID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="886" FieldName="TelCountryArea" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="03" FieldName="TelArea" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="Addr_Country" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="Addr_City" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate3" runat="server" BindingObjectID="JQDataForm3" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustomerName" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ShortName" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustomerTypeID" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>

        </JQTools:JQDialog>
        <JQTools:JQDialog ID="JQDialog3" runat="server" DialogLeft="320px" DialogTop="90px" Title="銷售類別列表" Width="430px" Closed="True" ShowSubmitDiv="False" DialogCenter="False" EditMode="Dialog" EnableTheming="True" ScrollBars="Vertical">
                 <JQTools:JQDataGrid ID="JQDataGridSalesType" runat="server" AlwaysClose="True" DataMember="SalesTypeDateList" RemoteName="sERP_Customer_Normal_Customer.SalesTypeDateList" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="20,40,60,80,100" PageSize="20" Pagination="True" QueryAutoColumn="False" QueryLeft="120px" QueryMode="Window" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdate="" Height="450px" Width="355px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                     <Columns>
                         <JQTools:JQGridColumn Alignment="left" Caption="交易類別" Editor="infocombobox" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="140" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sCustomerDealQuery.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="center" Caption="最近交易日期" Editor="datebox" FieldName="SalesDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="center" Caption="類別交易筆數" Editor="text" FieldName="SalesCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                         </JQTools:JQGridColumn>
                     </Columns>
                </JQTools:JQDataGrid>
         </JQTools:JQDialog>
         <JQTools:JQDialog ID="JQDialog4" runat="server" DialogLeft="120px" DialogTop="80px" Title="聯絡紀錄" Width="960px" Closed="True" ShowSubmitDiv="False" DialogCenter="False" EditMode="Dialog" EnableTheming="True" ScrollBars="Vertical">
                 <JQTools:JQDataGrid ID="JQDataGridContact" runat="server" EditDialogID="JQDialog5" AlwaysClose="True" DataMember="ContactLogs" RemoteName="sERP_Customer_Normal_Customer.ContactLogs" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,40,60,80" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="120px" QueryMode="Window" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="列表" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnUpdate="JQDataGridContactUpDate" Height="370px" Width="890px" BufferView="False" NotInitGrid="False" RowNumbers="True" OnDelete="JQDataGridContactOnDelete" OnDeleted="JQDataGridContactOnDeleted" OnLoadSuccess="JQDataGridContactOnLoadSucess" OnInsert="JQDataGridContactOnInsert">
                     <Columns>
                         <JQTools:JQGridColumn Alignment="left" Caption="連絡日期" Editor="datebox" EditorOptions="" FieldName="ContactDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Format="yyyy/mm/dd">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="聯絡對象" Editor="text" FieldName="Dialogue" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="聯絡內容" Editor="text" FieldName="ContactDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="460" FormatScript="">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateByName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="建立時間" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="ShareTo" Editor="text" FieldName="ShareTo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                         </JQTools:JQGridColumn>
                     </Columns>
                     <TooItems>
                         <JQTools:JQToolItem Enabled="True" Icon="icon-add"  ItemType ="easyui-linkbutton"  OnClick="insertItem"    Text="新增" Visible="True" />
                     </TooItems>
                </JQTools:JQDataGrid>
          </JQTools:JQDialog>
              <JQTools:JQDialog ID="JQDialog5" runat="server" BindingObjectID="JQDataForm1" Title="聯絡紀錄" Width="680px" DialogLeft="220px" DialogTop="180px">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="ContactLogs" HorizontalColumnsCount="3" RemoteName="sERP_Customer_Normal_Customer.ContactLogs" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="JQDataFormOnLoadSucess" OnApplied="JQDataFormOnApplied" OnApply="JQDataFormOnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" ReadOnly="False" Span="1" Visible="False" Width="80" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" MaxLength="0" ReadOnly="False" Span="1" Visible="False" Width="80" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" maxlength="0" Width="80" ReadOnly="False" Visible="False" Span="1" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡日期" Editor="datebox" FieldName="ContactDate" Format="yyyy/mm/dd" MaxLength="0" ReadOnly="False" Visible="True" Width="100" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="infocombobox" EditorOptions="valueField:'Dialogue',textField:'Dialogue',remoteName:'sERP_Customer_Normal_Customer.Dialogue',tableName:'Dialogue',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Dialogue" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡內容" Editor="textarea" EditorOptions="height:120" FieldName="ContactDescr" MaxLength="512" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="520" />
                        <JQTools:JQFormColumn Alignment="left" Caption="遮蔽紀錄" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsShade" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                        <JQTools:JQFormColumn Alignment="left" Caption="檢視者" Editor="text" FieldName="ShareToName" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesKindID" Editor="text" FieldName="SalesKindID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShareTo" Editor="text" FieldName="ShareTo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                 <JQTools:JQDefault ID="default2" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                     <Columns>
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsShade" RemoteMethod="True" />
                     </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactDescr" RemoteMethod="True" ValidateMessage="不可空白" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
          <JQTools:JQDataGrid ID="JQDataGrid2" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="CustomerSaleKind" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERP_Customer_Normal_Customer.CustomerSaleKind" RowNumbers="True" Title="業務類別" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" EditDialogID="JQDialog6" MultiSelectGridID="" OnInsert="JQDataGrid2_OnInsert" Width="1120px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="動作" Editor="text" FieldName="EditLink" FormatScript="OpenEditCustomerLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="業務類別" Editor="infocombobox" FieldName="SalesKindID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" EditorOptions="valueField:'SalesKindID',textField:'SalesKindName',remoteName:'sERP_Customer_Normal_Customer.SalesKind',tableName:'SalesKind',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="狀態" Editor="infocombobox" FieldName="DevelopLevelID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" EditorOptions="valueField:'DevelopLevelID',textField:'DevelopLevelName',remoteName:'sERP_Customer_Normal_Customer.DevelopLevel',tableName:'DevelopLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="則數" Editor="text" FieldName="ConCount" FormatScript="OpenContactLogsLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="最新聯絡內容" Editor="text" FieldName="LastCall" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="455" Format="" FormatScript="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="最近聯絡時間" Editor="datebox" FieldName="LastCallDate" Format="yyyy/mm/dd HH:MM:SS" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="LastCreateByName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="LastCreateBy" Editor="text" FieldName="LastCreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="120" Format="yyyy/mm/dd HH:MM:SS">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="MaxAutoKey" Editor="text" FieldName="MaxAutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesKindName" Editor="text" FieldName="SalesKindName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CRUDUrl" Editor="text" FieldName="CRUDUrl" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="ShareTo" Editor="text" FieldName="ShareTo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="IsShade" Editor="text" FieldName="IsShade" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-add"  ItemType ="easyui-linkbutton"  OnClick="insertItem"       Text="新增" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton"   OnClick="AddContactLogs"   Text ="聯絡紀錄" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-sum"  ItemType="easyui-linkbutton"   OnClick="UpdateDevelopLevel"    Text ="更新狀態" Visible="True" />
                </TooItems>
            </JQTools:JQDataGrid>
             <JQTools:JQDialog ID="JQDialog6" runat="server" Title="業務類別" BindingObjectID="JQDataForm2" DialogLeft="20px" Width="705px" DialogTop="100px">
                <JQTools:JQDataForm ID="JQDataForm2" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="CustomerSaleKind" disapply="False" DivFramed="False" DuplicateCheck="True" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="JQDataGrid2" RemoteName="sERP_Customer_Normal_Customer.CustomerSaleKind" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="JQDataForm2_OnLoadSucess" OnApply="JQDataForm2_OnApply">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustomerID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務類別" Editor="infocombobox" EditorOptions="valueField:'SalesKindID',textField:'SalesKindName',remoteName:'sERP_Customer_Normal_Customer.SalesKind',tableName:'SalesKind',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesKindID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="狀態" Editor="infocombobox" EditorOptions="valueField:'DevelopLevelID',textField:'DevelopLevelName',remoteName:'sERP_Customer_Normal_Customer.DevelopLevel',tableName:'DevelopLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="DevelopLevelID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最新聯絡內容" Editor="textarea" EditorOptions="height:100" FieldName="LastCall" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="430" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="LastCreateByName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最近聯絡時間" Editor="datebox" FieldName="LastCallDate" Format="yyyy/mm/dd HH:MM:SS" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="140" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastCreateBy" Editor="text" FieldName="LastCreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="dataForm1">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate2" runat="server" BindingObjectID="dataForm1">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        
        <JQTools:JQDialog ID="JQDialogClu" runat="server" DialogLeft="430px" DialogTop="280px" Title="業務人員" Width="190px" Closed="True" ShowSubmitDiv="True" DialogCenter="False"  EnableTheming="False" ScrollBars="Vertical" OnSubmited="JQDialogCluOnSubmited" Height="370px">
                 <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" EditDialogID="" AlwaysClose="True" DataMember="SalesKindUser" RemoteName="sERP_Customer_Normal_Customer.SalesKindUser" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,40,60,80" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="120px" QueryMode="Window" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Height="270px" Width="120px" BufferView="False" NotInitGrid="False" RowNumbers="False" EnableTheming="False" OnLoadSuccess="JQDataGrid1OnLoadSuccess">
                     <Columns>
                         <JQTools:JQGridColumn Alignment="left" Caption="業務人員" Editor="text" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                         </JQTools:JQGridColumn>
                     </Columns>
                 </JQTools:JQDataGrid>
          </JQTools:JQDialog>
    </form>
    </body>
</html>
