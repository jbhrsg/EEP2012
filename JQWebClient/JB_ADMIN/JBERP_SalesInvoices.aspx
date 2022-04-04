<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesInvoices.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        //目前有依使用者篩選公司別，公司別再撈出該公司別的客戶，客戶再撈出該客戶的銷貨類別
        //宣告公共變數
        var P_APIWebCode = ''; //網站代號
        var P_APIPassword = '';//網站密碼
        var P_RentID = '';     //公司統編
        //var backcolor = "#E8FFE8"

        $(document).ready(function () {
            $(function () {
                
                //銷貨日期起迄合併
                $('#SalesDateS_Query').closest('td').append($('#SalesDateE_Query').closest('td').prev('td').contents()).append($('#SalesDateE_Query').closest('td').children());
                $('#SalesDateS_Query').closest('td').next('td').remove();
                $('#SalesDateS_Query').closest('td').next('td').remove();

                $("#JQDialog5").find(".infosysbutton-s").hide();

                $('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });

                //關閉，清資料
                $('#JQDialog5').dialog('options').onBeforeClose = function () {
                    $("#dataGridDetail1").datagrid('setWhere', '1=0');//清資料
                }

                var Link = $("<a>").attr({'href':'#', 'onclick': 'openCustomerTab()' }).text("修改");
                $('#dataFormMasterCustomerID').closest('td').append('&nbsp;&nbsp;&nbsp;').append(Link).append('&nbsp;&nbsp;');

                $('#dataFormMasterIsOutPutDetails').closest('td').append("&nbsp;<font color='blue'>若勾，紙本發票明細會顯示「銷貨品名」。若沒勾，會顯示一筆「銷貨類別」</font>");

                //銷貨狀態 發票年月合併
                $('#IsActive_Query').closest('td').append($('#InvoiceYM_Query').closest('td').prev('td').contents()).append($('#InvoiceYM_Query').closest('td').children());
                $('#IsActive_Query').closest('td').next('td').remove();
                $('#IsActive_Query').closest('td').next('td').remove();
            });
        });

        function openCustomerTab() {
            var customerID = $("#dataFormMasterCustomerID").refval('getValue');
            if (customerID == '' || customerID == undefined) {
                alert("請先選取客戶簡稱");
                return false;
            } else {
                self.parent.addTab('客戶資料維護', './JB_ADMIN/ERP_Customer_Normal_Customer.aspx?CustomerID=' + customerID);
            }
        }

        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' />";
            else
                return "";
        }

        function dataFormMasterOnLoadSuccess() {
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                //取號
                var InsGroupID = $('#InsGroupID_Query').combobox('getValue');
                $("#dataFormMasterSalesNO").val(GetSalesNO());
                $("#dataFormMasterInsGroupID").val($("#InsGroupID_Query").combobox('getValue'));
                //過濾客戶、雇主
                $("#dataFormMasterCustomerID").refval('setWhere', "CustomerID IN (Select Distinct CustomerID From CustomerSaleType A,SalesType B Where A.SalesTypeID = B.SalesTypeID AND B.InsGroupID = " + "'" + InsGroupID + "'" + ")");
                $("#dataFormMasterEmployer").refval('setWhere', "CustomerID IN (Select Distinct CustomerID From CustomerSaleType A,SalesType B Where A.SalesTypeID = B.SalesTypeID AND B.InsGroupID = " + "'" + InsGroupID + "'" + ")");

                $('#dataFormMasterSalesTypeID').combobox('disable');
            } else if (getEditMode($("#dataFormMaster")) == 'updated') {//公司別，客戶有值 才須過濾
                //過濾客戶、雇主
                var InsGroupID = $("#dataFormMasterInsGroupID").val();
                $("#dataFormMasterCustomerID").refval('setWhere', "CustomerID IN (Select Distinct CustomerID From CustomerSaleType A,SalesType B Where A.SalesTypeID = B.SalesTypeID AND B.InsGroupID = " + "'" + InsGroupID + "'" + ")");
                $("#dataFormMasterEmployer").refval('setWhere', "CustomerID IN (Select Distinct CustomerID From CustomerSaleType A,SalesType B Where A.SalesTypeID = B.SalesTypeID AND B.InsGroupID = " + "'" + InsGroupID + "'" + ")");
                //過濾銷貨類別
                var UserID = getClientInfo("UserID");
                var CustomerID = $("#dataFormMasterCustomerID").refval('getValue');
                $("#dataFormMasterSalesTypeID").combobox('setWhere', "InsGroupID='" + InsGroupID + "' and CustomerID='" + CustomerID + "' and SalesID='" + UserID + "'");
                $("#dataFormMasterSalesTypeID").combobox('enable');
            }
        }

        //設定有多選條件時,焦點背後顏色維持系統預設
        function dataGridViewOnLoadSucess() {

            var UserID = getClientInfo("UserID");
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                $(this).datagrid({
                    singleSelect: true,
                    selectOnCheck: false,
                    checkOnSelect: false
                });
            }
            //依使用者篩選公司別(此使用者有的銷貨類別，銷貨類別對應的公司別)
            $('#InsGroupID_Query').combobox('setWhere', "InsGroupID IN (Select Distinct InsGroupID From SalesSalesType X,SalesType Y Where X.SalesTypeID=Y.SalesTypeID AND X.SalesID = " +"'"+  UserID+"'"+")" );
            //$('#IsActive_Query').combobox('setValue', '');
        }

        //取得銷售單號
        function GetSalesNO() {
            var UserID = getClientInfo("UserID");
            var _return;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster',
                data: "mode=method&method=" + "GetSalesNO" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                     _return = rows[0].SalesNO
                    }
                }
            });
            return _return;
        }

        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridView') {
                var result = [];
                var aVal = '';

                aVal = $('#InsGroupID_Query').combobox('getValue');
                if (aVal != '') {
                    result.push("InsGroupID = '" + aVal + "'");
                }
                else {
                    alert('注意!!,請選擇公司別...');
                    return false;
                }

                aVal = $("#SalesDateS_Query").datebox('getValue');
                if (aVal != '') result.push("SalesDate >= '" + aVal + "'");

                aVal = $("#SalesDateE_Query").datebox('getValue');
                if (aVal != '') result.push("SalesDate <= '" + aVal + "'");

                aVal = $("#CustomerID_Query").combobox('getValue');
                if (aVal != '') result.push("CustomerID = '" + aVal + "'");

                aVal = $("#SalesTypeID_Query").combobox('getValue');
                if (aVal != '') result.push("SalesTypeID = '" + aVal + "'");

                aVal = $("#IsActive_Query").combobox('getValue');
                if (aVal != '') result.push("IsActive = '" + aVal + "'");

                aVal = $("#InvoiceType_Query").combobox('getValue');
                if (aVal != '') { result.push("QInvoiceType = '" + aVal + "'"); }

                aVal = $("#Employer_Query").combobox('getValue');
                if (aVal != '') { result.push("Employer = '" + aVal + "'"); }

                var bVal = "('IS','RS','HS')"; //開單據產生成功碼
                var aVal = $("#IsNotputBill_Query").combobox('getValue');
                if (aVal == 1) {//未開單據(不含作廢單據)
                    result.push("((UploadCode NOT IN  " + bVal + " and UploadCode NOT IN  ('C0','C00')) or UploadCode is null) ");
                } else if (aVal == 2) { //已開單據
                    result.push("UploadCode IN  " + bVal);
                } else if (aVal == 3) {//已作廢單據
                    result.push("UploadCode IN  ('C0','C00')");
                }

                aVal = $("#SalesNO_Query").val();
                if (aVal != '') { result.push("SalesNO = '" + aVal + "'"); }

                aVal = $("#SalesID_Query").combobox('getValue');
                if (aVal != '') { result.push("SalesID = '" + aVal + "'"); }

                aVal = $("#InvoiceYM_Query").combobox('getValue');
                if (aVal != '') { result.push("InvoiceYM = '" + aVal + "'"); }

                var filtstr = result.join(' and ');
                $(dg).datagrid('setWhere', filtstr);
            }
        }

        function getFirstDate() {
            var date = new Date();
            var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
            return convertDate(firstDay);
        }

        function convertDate(date) {
            var yyyy = date.getFullYear().toString();
            var mm = (date.getMonth() + 1).toString();
            var dd = date.getDate().toString();
            var mmChars = mm.split('');
            var ddChars = dd.split('');
            return yyyy + '/' + (mmChars[1] ? mm : "0" + mmChars[0]) + '/' + (ddChars[1] ? dd : "0" + ddChars[0]);
        }

        //查詢選取公司別時,設定API要用的參數，並篩選銷貨客戶,雇主,銷貨類別
        function QInsGroupOnSelect(rowData) {
            var InsGroupID = $('#InsGroupID_Query').combobox('getValue');
            P_APIWebCode = (rowData.APIWebCode);
            P_APIPassword = (rowData.APIPassword);
            P_RentID = (rowData.TaxNO);
            $("#CustomerID_Query").combobox('setValue', '');//客戶
            $("#CustomerID_Query").combobox('setWhere', "CustomerID IN (Select Distinct a.CustomerID From CustomerSaleType A inner join SalesType B on A.SalesTypeID = B.SalesTypeID inner join Customer C on C.CustomerID=A.CustomerID Where  B.InsGroupID = '" + InsGroupID + "')");

            $("#Employer_Query").combobox('setWhere', '');//雇主
            $("#Employer_Query").combobox('setWhere', "CustomerID IN (Select Distinct a.CustomerID From CustomerSaleType A inner join SalesType B on A.SalesTypeID = B.SalesTypeID inner join Customer C on C.CustomerID=A.CustomerID Where  B.InsGroupID = '" + InsGroupID + "')");
            $("#SalesTypeID_Query").combobox('setValue', '');//銷貨類別
            $("#SalesTypeID_Query").combobox('setWhere', "InsGroupID ='" + InsGroupID + "'");
        }

        //選擇送出開立發票
        function CreateInvoice() {
            if (P_RentID == '' || P_APIWebCode == '') {
                alert('注意!!上傳錯誤...\n[' + $('#InsGroupID_Query').combobox('getText') + ']統編與串接碼設定錯誤,請洽資訊人員');
                return false;
            }

            var rows = $('#dataGridView').datagrid("getChecked");
            var amt = 0;
            var before1 = '';
            var before2 = '';
            var after = '';
            var scount = 0;
            var samt = 0;
            var fcount = 0;
            var famt = 0;
            var count = rows.length;

            if (count == 0) {
                alert('注意!!未選取任何銷貨單號,請選取');
                return false;
            }

            //#region 選取銷貨單號的加總金額，檢查每筆資料
            var CustomerIDIsEmpty = 0;
            var arrNullCustomerID = [];
            var TaxNONot8 = 0;
            var arrTaxNONot8 = [];
            var QInvoiceNot9899 = 0;
            var arrQInvoiceNot9899 = [];
            var UploadCodeSS = 0;
            var arrUploadCodeSS = [];
            var SalesMasterIsActive = 0;
            var arrSalesMasterIsActive = [];
            for (var i = 0; i <= rows.length - 1; i++) {
                amt = amt + (rows[i].SalesAmount);//各銷貨單的金額加總
                //客戶ID不能為空字串
                if ($.trim(rows[i].CustomerID) == '') {
                    CustomerIDIsEmpty = 1;
                    arrNullCustomerID.push(rows[i].SalesNO);
                }
                //三聯的銷貨，統編一定要8碼
                if ($.trim(rows[i].QInvoiceType)=="98" && $.trim(rows[i].TaxNO).length != 8) {
                    TaxNONot8 = 1;
                    arrTaxNONot8.push(rows[i].SalesNO);
                }
                //單據類別只能是二聯或三聯
                if ($.trim(rows[i].QInvoiceType) != "98" && $.trim(rows[i].QInvoiceType) != "99") {
                    QInvoiceNot9899 = 1;
                    arrQInvoiceNot9899.push(rows[i].SalesNO);
                }
                //單據無法再開發票
                if ($.trim(rows[i].UploadCode) == "IS" || $.trim(rows[i].UploadCode) == "RS" || $.trim(rows[i].UploadCode) == "HS" ) {
                    UploadCodeSS = 1;
                    arrUploadCodeSS.push(rows[i].SalesNO);
                }
                //無效的銷貨無法開發票
                if (rows[i].IsActive == false) {
                    SalesMasterIsActive = 1;
                    arrSalesMasterIsActive.push(rows[i].SalesNO);
                }
            }
                //客戶ID不能為空字串
            if (CustomerIDIsEmpty == 1) {
                alert("注意，銷貨單號:" + arrNullCustomerID.join('，') + "的客戶為空白");
                return false;
            }
                //三聯的銷貨，統編一定要八碼
            else if (TaxNONot8 == 1) {
                alert("注意，銷貨單號(三聯):" + arrTaxNONot8.join('，') + "的統編不為8碼");
                return false;
            }
                //單據類別只能是2,3聯
            else if (QInvoiceNot9899 == 1) {
                alert("注意，銷貨單號:" + arrQInvoiceNot9899.join('，') + "的單據類別不為發票");
                return false;
            }
                //單據無法再開發票
            else if (UploadCodeSS == 1) {
                alert("注意，銷貨單號:" + arrUploadCodeSS.join('，') + "已為單據");
                return false;
            }
                //無效的銷貨無法再開發票
            else if (SalesMasterIsActive == 1) {
                alert("注意，銷貨單號:" + arrSalesMasterIsActive.join('，') + "為無效的銷貨");
                return false;
            }
            
            //#endregion 選取銷貨單號的加總金額，檢查每筆資料

            //上傳前提醒
            before1 = '注意!!您已選取要上傳開立發票的資訊如下:';
            before2 = '筆數:' + rows.length.toString() + '\n' + '銷貨金額:' + toCurrency(amt);
            var yn = confirm(before1 + '\n' + before2);
            if (yn == false) {
                return
            }

            //CallAPI_Create
            for (var i = 0; i <= rows.length - 1; i++) 
            {
                var Rstr = CallAPI_Create(rows[i].InsGroupID, rows[i].SalesNO, rows[i].IsHasTax, P_RentID, P_APIWebCode + P_APIPassword, rows[i].IsOutPutDetails, rows[i].SalesTypeID, rows[i].tmpInvoiceNO);

               if (Rstr.length == 15) {
                   scount = scount + 1;
                   samt = samt + rows[i].SalesAmount;
               }
               else {
                   fcount = fcount + 1;
                   famt = famt + rows[i].SalesAmount;
               }
            }

            //上傳後提醒
            after = '銷貨上傳結果資訊如下:' + '\n' + '執行前...' + '\n' + before2 + '\n\n\n' + '執行後...' + '\n' + '成功筆數:' + scount.toString() + '      ' + '成功金額:' + toCurrency(samt) + '\n' + '失敗筆數:' + fcount.toString() + '      ' + '失敗金額:' + famt.toString();
            alert(after);

            $('#dataGridView').datagrid('reload');
        }

        //按下作廢發票(電開、手開)
        function CancelInvoice() {
            var rows = $('#dataGridView').datagrid("getChecked");
            var amount = 0;
            var before1 = '';
            var before2 = '';
            var after = '';
            var scount = 0;
            var samt = 0;
            var fcount = 0;
            var famt = 0;
            var count = rows.length;

            if (count == 0) {
                alert('注意!!未選取任何發票,請選取');
                return false;
            }

            //#region
            var QInvoiceNot9899 = 0;
            var arrQInvoiceNot9899 = [];
            var UploadCodeNotIS = 0;
            var arrUploadCodeNotIS = [];
            var IsInWarrantDetails = 0;
            var arrIsInWarrantDetails = [];
            for (var i = 0; i <= rows.length - 1; i++) {
                //單據類別只能是二聯或三聯
                if ($.trim(rows[i].QInvoiceType) != "98" && $.trim(rows[i].QInvoiceType) != "99") {
                    QInvoiceNot9899 = 1;
                    arrQInvoiceNot9899.push(rows[i].SalesNO);
                }
                //有效的電開發票或手開發票才能作廢
                if ($.trim(rows[i].UploadCode) != "IS" && $.trim(rows[i].UploadCode) != "HS") {
                    UploadCodeNotIS = 1;
                    arrUploadCodeNotIS.push(rows[i].SalesNO);
                }
                //檢查發票是否已在收款明細裡
                if (CheckInvoiceNOIsInWarrantDetails($.trim(rows[i].InvoiceNO))==true) {
                    IsInWarrantDetails = 1;
                    arrIsInWarrantDetails.push(rows[i].SalesNO);
                }
            }
            
            //單據類別只能是2,3聯
            if (QInvoiceNot9899 == 1) {
                alert("銷貨單號:" + arrQInvoiceNot9899.join('，') + "的單據類別不為發票");
                return false;
            }
            //上傳成功才能作廢
            else if (UploadCodeNotIS == 1) {
                alert("銷貨單號:" + arrUploadCodeNotIS.join('，') + "不是有效的發票");
                return false;
            }
            //發票號碼已在收款明細裡
            else if (IsInWarrantDetails == 1) {
                alert("銷貨單號:" + arrIsInWarrantDetails.join('，') + "的發票已被沖款，無法作廢");
                return false;
            }
            //#endregion

            //作廢事由必填
            var ReturnTaxNumber = ''; //作廢文號
            var ReturnRemark = '';//'作廢備註';
            var voidtxt = prompt("請輸入作廢事由:(限定20個字以內)", "");
            if (voidtxt == null || voidtxt == "") {
                alert("請輸入作廢事由");
                return false;
            } else if (voidtxt.length>20) {
                alert("限定20個字以內");
            } else {
                ReturnRemark = voidtxt
            }

            

            for (var i = 0; i <= rows.length - 1; i++) {
                amount = amount + rows[i].SalesAmount;
            }

            //作廢前提醒
            before1 = '注意!!您已選取要上傳取消發票的資訊如下:';
            before2 = '筆數:' + rows.length.toString() + '\n' + '銷貨金額:' + toCurrency(amount);
            var yn = confirm(before1 + '\n' + before2);
            if (yn == false) {
                return
            }
            
            for (var i = 0; i <= rows.length - 1; i++) {
                var Rstr;
                if (rows[i].QInvoiceType == '98' || rows[i].QInvoiceType == '99') {
                    if (rows[i].CreateInvoiceType != "2") {//電開
                        if (rows[i].SalesTypeID == '01' || rows[i].SalesTypeID == '02' || rows[i].SalesTypeID == '47' || rows[i].SalesTypeID == '48') {
                            Rstr = CallAPI_Cancel(rows[i].InsGroupID, rows[i].SalesNO, rows[i].InvoiceNO_01024748, P_RentID, P_APIWebCode + P_APIPassword, ReturnTaxNumber, ReturnRemark, rows[i].InvoiceDate_01024748);
                        } else {
                            Rstr = CallAPI_Cancel(rows[i].InsGroupID, rows[i].SalesNO, rows[i].InvoiceNO, P_RentID, P_APIWebCode + P_APIPassword, ReturnTaxNumber, ReturnRemark, rows[i].InvoiceDate);
                        }
                    } else if (rows[i].CreateInvoiceType == "2") {//手開
                        Rstr = HandWriteInvoice_Cancel(rows[i].SalesNO, rows[i].InvoiceNO);
                    }
                }
                if (Rstr == 'C0' || Rstr == 'C000') {
                    scount = scount + 1;
                    samt = samt + rows[i].SalesAmount;
                }
                else {
                    fcount = fcount + 1;
                    famt = famt + rows[i].SalesAmount;
                }
            }
            //作廢後提醒
            after = '發票取消資訊如下:' + '\n' + '執行前...' + '\n' + before2 + '\n\n\n' + '執行後...' + '\n' + '成功筆數:' + scount.toString() + '      ' + '成功金額:' + toCurrency(samt) + '\n' + '失敗筆數:' + fcount.toString() + '      ' + '失敗金額:' + famt.toString();
            alert(after);
            $('#dataGridView').datagrid('reload');
            return true;
        }

        //選擇送出作廢收據
        function CancelReceipt() {
            var rows = $('#dataGridView').datagrid("getChecked");
            var amount = 0;
            var before1 = '';
            var before2 = '';
            var after = '';
            var scount = 0;
            var samt = 0;
            var fcount = 0;
            var famt = 0;
            var count = rows.length;

            if (count == 0) {
                alert('注意!!未選取任何收據,請選取');
                return false;
            }

            //#region
            var QInvoiceNot97 = 0;
            var arrQInvoiceNot97 = [];
            var UploadCodeNotRS = 0;
            var arrUploadCodeNotRS = [];
            var IsInWarrantDetails = 0;
            var arrIsInWarrantDetails = [];
            for (var i = 0; i <= rows.length - 1; i++) {
                //單據類別只能是收據
                if ($.trim(rows[i].QInvoiceType) != "97") {
                    QInvoiceNot97 = 1;
                    arrQInvoiceNot97.push(rows[i].SalesNO);
                }
                //有效的收據才能作廢
                if ($.trim(rows[i].UploadCode) != "RS") {
                    UploadCodeNotRS = 1;
                    arrUploadCodeNotRS.push(rows[i].SalesNO);
                }
                //檢查發票是否已在收款明細裡
                if (CheckInvoiceNOIsInWarrantDetails($.trim(rows[i].InvoiceNO)) == true) {
                    IsInWarrantDetails = 1;
                    arrIsInWarrantDetails.push(rows[i].SalesNO);
                }
            }
            //單據類別只能是收據
            if (QInvoiceNot97 == 1) {
                alert("銷貨單號:" + arrQInvoiceNot97.join('，') + "的單據類別不為收據");
                return false;
            }
                //有效的收據才能作廢
            else if (UploadCodeNotRS == 1) {
                alert("銷貨單號:" + arrUploadCodeNotRS.join('，') + "不是有效的收據");
                return false;
            }
                //發票號碼已在收款明細裡
            else if (IsInWarrantDetails == 1) {
                alert("銷貨單號:" + arrIsInWarrantDetails.join('，') + "的收據已被沖款，無法作廢");
                return false;
            }
            //#endregion

            var ReturnTaxNumber = ''; //作廢文號
            var ReturnRemark = '作廢備註'

            for (var i = 0; i <= rows.length - 1; i++) {
                amount = amount + rows[i].SalesAmount;
            }

            //作廢前提醒
            before1 = '注意!!您已選取要取消的收據資訊如下:';
            before2 = '筆數:' + rows.length.toString() + '\n' + '銷貨金額:' + toCurrency(amount);
            var yn = confirm(before1 + '\n' + before2);
            if (yn == false) {
                return
            }

            for (var i = 0; i <= rows.length - 1; i++) {
                var Rstr;
                
                if (rows[i].QInvoiceType == '97') {
                    Rstr = Receipt_Cancel(rows[i].SalesNO, rows[i].InvoiceNO);
                }
                if (Rstr == 'C00') {
                    scount = scount + 1;
                    samt = samt + rows[i].SalesAmount;
                }
                else {
                    fcount = fcount + 1;
                    famt = famt + rows[i].SalesAmount;
                }
            }
            //作廢後提醒
            after = '收據取消資訊如下:' + '\n' + '執行前...' + '\n' + before2 + '\n\n\n' + '執行後...' + '\n' + '成功筆數:' + scount.toString() + '      ' + '成功金額:' + toCurrency(samt) + '\n' + '失敗筆數:' + fcount.toString() + '      ' + '失敗金額:' + famt.toString();
            alert(after);
            $('#dataGridView').datagrid('reload');
            return true;
        }

        function toCurrency(num) {
            var parts = num.toString().split('.');
            parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
            return parts.join('.');
        }

        function JQDataForm1OnApply() {//沒用
            var _CancelInvoice = CancelInvoice();
        }

        //呼叫鯨耀開立發票API 開立發票
        //傳入參數:@Para1:公司代號 @SalesNO:銷貨單號 @HasTax:是否含稅 @RentID:公司統編 @Source:網站代號與密碼 @UserID:使用者代號 @IsOutPutDetails:是否明細全顯示
        function CallAPI_Create(InsGroupID, SalesNO, HasTax, RentID, Source, IsOutPutDetails, SalesTypeID, tmpInvoiceNO) {
            var ReturnStr = '';
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster',
                data: "mode=method&method=" + "procCallApi_Create" + " &parameters=" + InsGroupID + "," + SalesNO + "," + HasTax + "," + RentID + "," + Source + "," + UserID + "," + IsOutPutDetails + "," + SalesTypeID + "," + tmpInvoiceNO,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        ReturnStr = data;
                    }
                }
            });
            return ReturnStr;
        }

        //呼叫鯨耀開立發票API 作廢發票
        //傳入參數:@Para1:公司代號 @SalesNO:銷貨單號 @InvoiceNO:發票號碼 @RentID:公司統編 @Source:網站代號與密碼 @UserID:使用者代號 @ReturnTaxNumber:發票作廢文號 @ReturnRemark:作廢原因 @UserID:使用者代號
        function CallAPI_Cancel(InsGroupID, SalesNO, InvoiceNO, RentID, Source, ReturnTaxNumber, ReturnRemark, InvoiceDate) {
            var ReturnStr = '';
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster',
                data: "mode=method&method=" + "procCallApi_Cancel" + " &parameters=" + InsGroupID + "*" + SalesNO + "*" + InvoiceNO + "*" + RentID + "*" + Source + "*" + ReturnTaxNumber + "*" + ReturnRemark + "*" + UserID + "*" + InvoiceDate,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        ReturnStr = data;
                    }
                }
            });
            return ReturnStr;
        }

        //作廢一筆收據
        function Receipt_Cancel(SalesNO, InvoiceNO) {
            var ReturnStr = '';
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster',
                data: "mode=method&method=" + "Receipt_Cancel" + " &parameters="  + SalesNO + "," + InvoiceNO + "," + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        ReturnStr = data;
                    }
                }
            });
            return ReturnStr;
        }

        //作廢一筆手開發票
        function HandWriteInvoice_Cancel(SalesNO, InvoiceNO) {
            var ReturnStr = '';
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster',
                data: "mode=method&method=" + "HandWriteInvoice_Cancel" + " &parameters=" + SalesNO + "," + InvoiceNO + "," + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "False") {
                        ReturnStr = data;
                    }
                }
            });
            return ReturnStr;
        }

        //選擇開立收據
        function SelectCreateReceipt() {
            var rows = $('#dataGridView').datagrid("getChecked");
            if (rows.length <= 0) {
                alert('注意!!未選取銷貨紀錄,請選取');
                return false;
            }
            var rows = $('#dataGridView').datagrid("getChecked");
            var amount = 0;
            var before1 = '';
            var before2 = '';
            var after = '';
            var after1 = '';
            var scount = 0;
            var samt = 0;
            var fcount = 0;
            var famt = 0;
            
            var InsGroupID = $('#InsGroupID_Query').combobox('getValue');

            var CustomerIDIsEmpty = 0;
            var arrNullCustomerID = [];
            var QInvoiceNot97 = 0;
            var arrQInvoiceNot97 = [];
            var UploadCodeSS = 0;
            var arrUploadCodeSS = [];
            var SalesMasterIsActive = 0;
            var arrSalesMasterIsActive = [];
            for (var i = 0; i <= rows.length - 1; i++) {
                amount = amount + rows[i].SalesAmount;//各銷貨單的金額加總
                //客戶ID不能為空字串
                if ($.trim(rows[i].CustomerID) == '') {
                    CustomerIDIsEmpty = 1;
                    arrNullCustomerID.push(rows[i].SalesNO);
                }
                //單據類別只能是收據
                if ($.trim(rows[i].QInvoiceType) != "97") {
                    QInvoiceNot97 = 1;
                    arrQInvoiceNot97.push(rows[i].SalesNO);
                }
                //單據無法再開收據
                if ($.trim(rows[i].UploadCode) == "IS" || $.trim(rows[i].UploadCode) == "RS" || $.trim(rows[i].UploadCode) == "HS") {
                    UploadCodeSS = 1;
                    arrUploadCodeSS.push(rows[i].SalesNO);
                }
                //無效的銷貨無法開發票
                if (rows[i].IsActive == false) {
                    SalesMasterIsActive = 1;
                    arrSalesMasterIsActive.push(rows[i].SalesNO);
                }
            }

            //客戶ID不能為空字串
            if (CustomerIDIsEmpty == 1) {
                alert("注意，銷貨單號:" + arrNullCustomerID.join('，') + "的客戶為空白");
                return false;
            }
            //單據類別只能是收據
            else if (QInvoiceNot97 == 1) {
                alert("注意，銷貨單號:" + arrQInvoiceNot97.join('，') + "的單據類別不為收據");
                return false;
            }
                //單據無法再開收據
            else if (UploadCodeSS == 1) {
                alert("注意，銷貨單號:" + arrUploadCodeSS.join('，') + "已為單據");
                return false;
            }
                //無效的銷貨無法再開收據
            else if (SalesMasterIsActive == 1) {
                alert("注意，銷貨單號:" + arrSalesMasterIsActive.join('，') + "為無效的銷貨");
                return false;
            }

            before1 = '注意!!您已選取要開立收據的資訊如下:';
            before2 = '筆數:' + i.toString() + '\n' + '銷貨金額:' + toCurrency(amount);
            var yn = confirm(before1 + '\n' + before2);
            if (yn == false) {
                return
            }
            //CreateReceipt
            for (var i = 0; i <= rows.length - 1; i++) {
                var Rstr = CreateReceipt(InsGroupID, rows[i].SalesNO, rows[i].SalesTypeID);
                var arrRstr = [];
                arrRstr = Rstr.split("*");

                if (arrRstr[0]== "RS") {
                    scount = scount + 1;
                    samt = samt + rows[i].SalesAmount;
                }
                else {
                    fcount = fcount + 1;
                    famt = famt + rows[i].SalesAmount;
                }

                if (arrRstr[1] != arrRstr[2]) {
                    if(after1==''){
                        after1 = '收據金額與銷貨金額不一致!!!銷貨單號:' + rows[i].SalesNO;
                    }else{
                        after1 = after1 + ',' + rows[i].SalesNO;
                    }
                }
            }

            after = '產生收據結果資訊如下:' + '\n' + '執行前...' + '\n' + before2 + '\n\n\n' + '執行後...' + '\n' + '成功筆數:' + scount.toString() + '      ' + '成功金額:' + toCurrency(samt) + '\n' + '失敗筆數:' + fcount.toString() + '      ' + '失敗金額:' + famt.toString();
            alert(after + '\n' + after1);
            $('#dataGridView').datagrid('reload');
        }
        
        //產生收據
        function CreateReceipt(InsGroupID, SalesNO,SalesTypeID) {
            var ReturnStr = '';
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster',
                data: "mode=method&method=" + "procCreateReceipt" + " &parameters=" + InsGroupID + "," + SalesNO + "," + UserID + "," + SalesTypeID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        ReturnStr = data;
                    }
                }
            });
            return ReturnStr;
        }

        //作廢銷貨
        function SelectDelete() {
            var rows = $('#dataGridView').datagrid("getChecked");
            if (rows.length <= 0) {
                alert('注意!!未選取銷貨紀錄,請選取');
                return false;
            }
            var cou = 0;
            var amount = 0;
            var before1 = '';
            var before2 = '';
            for (var i = 0; i <= rows.length - 1; i++) {
                if (rows[i].UploadCode == 'IS' || rows[i].UploadCode == 'RS' || rows[i].UploadCode == 'HS') {
                    cou = cou + 1;
                }
            }
            if (cou > 0) {
               alert( '注意!!有' + cou.toString() + '筆銷貨已開立發票或收據,無法刪除,請重新選取.');
               return false;
            }
            for (var i = 0; i <= rows.length - 1; i++) {
                amount = amount + rows[i].SalesAmount;
            }
            before1 = '注意!!您已選取要整批刪除的銷貨資訊如下:';
            before2 = '筆數:' + i.toString() + '\n' + '銷貨金額:' + toCurrency(amount);
            var yn = confirm(before1 + '\n' + before2);
            if (yn == false) {
                return
            }
            for (var i = 0; i <= rows.length - 1; i++) {
                 var Rstr = DeleteSales(rows[i].SalesNO);//傳回"OK"代表成功
            }
            $('#dataGridView').datagrid('reload');
            return true;
        }

        //單筆後端作廢銷貨
        function DeleteSales(SalesNO) {
            var ReturnStr = '';
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster',
                data: "mode=method&method=" + "procDeleteSales" + " &parameters=" + SalesNO + "," + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "Fail") {
                        ReturnStr = data;
                    }
                }
            });
            return ReturnStr;
        }

        //dataFormMaster 客戶代號_OnSelect
        function CustomerOnSelect(rowData) {
            var UserID = getClientInfo("UserID");
            var InsGroupID = $("#dataFormMasterInsGroupID").val();
            $("#dataFormMasterSalesTypeID").combobox('setValue','');
            $("#dataFormMasterSalesTypeID").combobox('setWhere', "");
            $("#dataFormMasterSalesTypeID").combobox('setWhere', "InsGroupID='" + InsGroupID + "' and CustomerID='" + rowData.CustomerID + "' and SalesID='" + UserID + "'");
            $("#dataFormMasterSalesTypeID").combobox('enable');
            $("#dataFormMasterCustomerTypeID").val(rowData.CustomerTypeID);

            //清空
            $("#dataFormMasterSalesID").combobox('setValue', '');
            $("#dataFormMasterBalanceDate").val('');
            $("#dataFormMasterDebtorDays").val('');
            $("#dataFormMasterEmailAddress").val('');
            $("#dataFormMasterEmployer").refval('setValue', '');
            $("#dataFormMasterQInvoiceType").combobox('setValue', '');

            $("#dataFormMasterRemark").val('');
            $("#dataFormMasterTaxNO").val('');

            $("#dataFormMasterTempTaxNO").val('');
            $("#dataFormMasterTempTaxNO").val(rowData.TaxNO);
        }
        //dataGridView按筆時
        function dataGridViewOnUpdate(RowData) {
            var UploadCode = RowData.UploadCode;
            //if (UploadCode != '00' && UploadCode != 'C0' && UploadCode != 'C00') {//00新增，C0發票作廢,C00收據作廢
            //    openForm('#JQDialog1', RowData, "viewed", 'dialog');
            //    return false;
            //}
            if (UploadCode == 'IS' || UploadCode == 'RS' || UploadCode == 'HS') {//開成功，就無法修改
                openForm('#JQDialog1', RowData, "viewed", 'dialog');
                return false;
            }
        }

        function dataFormMasterOnApply() {
            var InvoiceType = $("#dataFormMasterQInvoiceType").combobox('getValue');
            var TaxNO = $.trim($("#dataFormMasterTaxNO").val());
            var IsOutPutDetails = $.trim($("#dataFormMasterIsOutPutDetails").val());

            //發票類型卡統編
            if (InvoiceType == '98' && TaxNO == '') {
                alert('注意!!當單據是三聯式發票時,統一編號不可空白..');
                return false;
            }
            if (InvoiceType == '99' && TaxNO != '') {
                alert('注意!!當單據是二聯式發票時,統一編號不可填..');
                return false;
            }

            //備註(發票)，必填
            var remark = $.trim($("#dataFormMasterRemark").val());
            if (remark == '') {
                alert('此銷貨的客戶名稱為空白，請至客戶資料管理填寫客戶名稱(客戶簡稱旁有修改連結)');
                return false;
            }

            //客戶屬性為個人卡雇主
            var customerTypeID = $.trim($("#dataFormMasterCustomerTypeID").val());
            var employer = $.trim($("#dataFormMasterEmployer").refval('getValue'));
            if (customerTypeID == '2' && employer=='') {
            alert('「雇主」必填');
            return false;
            }

            //Mail傳送有勾，郵件信箱檢查
            var mailSend = $.trim($("#dataFormMasterMailSend").checkbox('getValue'));
            var emailAddress = $.trim($("#dataFormMasterEmailAddress").val());
            if (mailSend == '1') {
                if (emailAddress == '') {//沒填
                    alert('「郵件信箱」必填');
                    return false;
                } else {//有填
                    //「郵件信箱」格式檢查
                    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    if (!regex.test(emailAddress)) {
                        alert('「郵件信箱」不符合格式');
                        return false;
                    }
                }
            }

            //卡明細
            var rows = $('#dataGridDetail').datagrid("getRows");
            var IsOutPutDetails = $("#dataFormMasterIsOutPutDetails").checkbox('getValue');
            if (rows.length <= 0) {
                alert('注意!!未建立銷貨明細,無法存檔');
                return false;
            }
            else if (rows.length > 0) {
                for (var i = 0; i < rows.length; i++) {
                    if (IsOutPutDetails == '1' && $.trim(rows[i]["SalesTypeName"]) == "") {
                        alert("你已勾取「明細全顯示」，請填「銷貨品名」");
                        return false;
                    }
                }
            }

            return true;
        }
        
        //dataFormMaster銷貨類別_OnSelect ，裡頭會呼叫單據類別_OnSelect
        function SalesTypeID_OnSelect()
        {
            //清空
            $("#dataFormMasterSalesID").combobox('setValue', '');
            $("#dataFormMasterBalanceDate").val('');
            $("#dataFormMasterDebtorDays").val('');
            $("#dataFormMasterEmailAddress").val('');
            $("#dataFormMasterEmployer").refval('setValue', '');
            $("#dataFormMasterQInvoiceType").combobox('setValue', '');
            $("#dataFormMasterTaxType").combobox('setValue', '');
            $("#dataFormMasterSalesKindID").val('');
            //抓值
            var CustomerID=$("#dataFormMasterCustomerID").refval('getValue');
            var SalesTypeID = $("#dataFormMasterSalesTypeID").combobox('getValue');
            var InvoiceType = $("#dataFormMasterQInvoiceType").combobox('getValue');
            //取資料
            if (CustomerID != '' && SalesTypeID != '') {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster',
                    data: "mode=method&method=" + "selectCustomerSaleType" + " &parameters=" + CustomerID + "," + SalesTypeID + "," + InvoiceType,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False") {
                            var rows = $.parseJSON(data)
                            if (rows.length > 0) {
                                $("#dataFormMasterSalesID").combobox('setValue', rows[0].SalesID);
                                $("#dataFormMasterBalanceDate").val(rows[0].BalanceDate);
                                $("#dataFormMasterDebtorDays").val(rows[0].DebtorDays);
                                $("#dataFormMasterEmailAddress").val(rows[0].EmailAddress);
                                $("#dataFormMasterEmployer").refval('setValue', rows[0].Employer);
                                $("#dataFormMasterQInvoiceType").combobox('setValue', rows[0].QInvoiceType);
                                $("#dataFormMasterTaxType").combobox('select', rows[0].TaxType);
                                $("#dataFormMasterSalesKindID").val(rows[0].SalesKindID);
                                QInvoiceType_OnSelect();
                                if ($.trim(rows[0].DebtorDays) == '') {
                                    alert("付款天數沒填，請點'客戶簡稱'旁的'修改'連結，來進行修改");
                                }
                            }
                        } else {
                            alert("有錯誤，請洽管理室");
                        }
                    }
                });
            }
        }
        //dataFormMaster單據類別_OnSelect
        function QInvoiceType_OnSelect() {
            //清空
            $("#dataFormMasterRemark").val('');
            $("#dataFormMasterTaxNO").val('');
            //抓值
            var CustomerID = $("#dataFormMasterCustomerID").refval('getValue');
            var InvoiceType = $("#dataFormMasterQInvoiceType").combobox('getValue');
            //取資料
            if (CustomerID != ''  && InvoiceType != '') {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster',
                    data: "mode=method&method=" + "selectRemarkAndTaxNO" + " &parameters=" + CustomerID + "," + InvoiceType,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False") {
                            var rows = $.parseJSON(data)
                            if (rows.length > 0) {
                                //設備註
                                $("#dataFormMasterRemark").val(rows[0].Remark);
                                //設統編
                                if ($("#dataFormMasterQInvoiceType").combobox('getValue') == '98') {//三聯
                                    $("#dataFormMasterTaxNO").val(rows[0].TaxNO);
                                } else {
                                    $("#dataFormMasterTaxNO").val('');
                                }
                            }
                        } else {//dll有錯
                            alert("有錯誤，請洽管理室");
                        }
                    }
                });
            }
            //單據選二聯，停用mail寄送
            if (InvoiceType == '99') {
                $("#dataFormMasterMailSend").checkbox('setValue', 0);
                $("#dataFormMasterMailSend").attr('disabled', false);
            } else {
                $("#dataFormMasterMailSend").attr('disabled', false);
            }

        }

        //銷貨明細_合計
        function dataGridDetailAmount_OnTotal(footerRow) {
            $("#dataFormMasterSalesAmount").val(footerRow.Amount)
        }

        //新增銷貨
        function dataGridView_Add_OnClick(dg) {
            if ($.trim($("#InsGroupID_Query").combobox('getValue')) == '') {
                alert('「公司別」請先選取');
                return false;
            } else if ($.trim($("#InsGroupID_Query").combobox('getValue')) != '') {
                insertItem(dg);
            }
        }

        //銷貨明細Form_OnLoadSuccess
        function dataFormDetail_OnLoadSuccess() {
            if (getEditMode($('#dataFormDetail')) == 'inserted') {
                var salesTypeID = $.trim($("#dataFormMasterSalesTypeID").combobox('getValue'));
                var salesTypeName = $.trim($("#dataFormMasterSalesTypeID").combobox('getText'));
                if (salesTypeID != '') {
                    //主檔銷貨類別帶入
                    $("#dataFormDetailSalesTypeID").combobox('setValue', salesTypeID);
                    $("#dataFormDetailFeeItemID").combobox('setWhere', "SalesTypeID='" + salesTypeID + "'");
                    $("#dataFormDetailSalesTypeName").combobox('setWhere', "SalesTypeID='" + salesTypeID + "'");
                    setTimeout(
                    function () { $("#dataFormDetailSalesTypeName").combobox('setValue', '') }, 800);
                } else {
                    alert('主檔請先選取「銷貨類別」');
                    closeForm("#JQDialog2");
                    return false;
                }
            } else if (getEditMode($('#dataFormDetail')) == 'updated' || getEditMode($('#dataFormDetail')) == 'viewed') {
                var salesTypeID = $.trim($("#dataFormMasterSalesTypeID").combobox('getValue'));
                $("#dataFormDetailFeeItemID").combobox('setWhere', "SalesTypeID='" + salesTypeID + "'");
            }
        }

        //dataGridView列印發票明細
        function dataGridView_IsOutPutDetails_FormatScript(val) {
            if (val == true)
                return "<input  type='checkbox' checked='true' onclick='return  false;'/>";
            else
                return "<input  type='checkbox' onclick='return false;'/>";
        }

        function dataFormDetail_Quantity_OnBlur() {
            var quantity = $("#dataFormDetailQuantity").val();
            var unitPrice = $("#dataFormDetailUnitPrice").val();
            if (quantity != '' && unitPrice != '') {
                $("#dataFormDetailAmount").val(quantity * unitPrice);
            }
        }

        //銷貨明細.費用項目
        function FeeItemID_OnSelect(rowdata) {
            $("#dataFormDetailSalesTypeName").combobox('setValue', rowdata.FeeItemName);
        }

        //-----第一種-----批次新增(直接寫入)

        //批次新增二聯銷貨(宿舍專用)(沒用)
        function BatchAddForDorm_OnClick() {
            var insGroupID = $.trim($("#InsGroupID_Query").combobox('getValue'));
            var customerID = $.trim($("#Employer_Query").combobox('getValue'));
            var customerName = $.trim($("#Employer_Query").combobox('getText'));
            var userName = getClientInfo('_username');
            if (insGroupID != '' && customerID != '' && userName != '') {
                var yn = confirm("按下「確定」就會為「" + customerName + "」的宿舍房客，寫入銷貨");
                if (yn == false) {
                    return
                }
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster',
                    data: "mode=method&method=" + "BatchAddForDorm" + " &parameters=" + insGroupID + "," + customerID + "," + userName,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False") {
                            var rows = $.parseJSON(data);
                            if (rows[0].Error == '0') {//@Error=0代表跑procedure沒出錯
                                alert('批次新增成功' + '，' + '已新增銷貨單數:' + rows[0].JBERPRoomers + '\n'
                                    + '公司別:' + $("#InsGroupID_Query").combobox('getText') + '\n'
                                    + '宿舍:' + rows[0].DormName + '\n'
                                    + '合約編號:' + rows[0].ContractID + '\n'
                                    + '合約房客數:' + rows[0].JCSRoomers + '\n'
                                    + '合約費用項目數:' + rows[0].FeelItems + '\n'
                                    + '請再填寫各房客的銷貨明細金額');
                                $("#dataGridView").datagrid('setWhere', '');
                                //$("#dataGridView").datagrid('setWhere', "SalesMaster.InsGroupID='" + insGroupID + "' and SalesMaster.Employer='" + customerID + "' and SalesMaster.QInvoiceType='99' and SalesMaster.SalesDate=CONVERT(date, getdate()) and SalesMaster.IsActive=1");// and SalesDate='" + $.now() + "'
                                $("#dataGridView").datagrid('setWhere', "InsGroupID='" + insGroupID + "' and Employer='" + customerID + "' and QInvoiceType='99' and SalesDate=CONVERT(date, getdate()) and IsActive=1");// and SalesDate='" + $.now() + "'
                                $('#dataGridView').datagrid('reload');
                                return true;
                            }
                        }
                    }
                });
            } else {
                alert('請選取「公司別」、「雇主」，再批次新增');
            }
        }

        //---第二種---批次新增(使用暫存資料表)------

        //按鈕開啟JQDialog4
        function BatchAddForDorm1_OnClick() {
            //清Temp
            //DeleteSalesDetailsMasterTemp();
            //開啟JQDialog4
            openForm('#JQDialog4', '', 'updated', 'dialog');//viewed
            $('#dataGridTemp').datagrid('reload');
        }
        function DeleteSalesDetailsMasterTemp() {
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster',
                data: "mode=method&method=" + "DeleteSalesDetailsMasterTemp",// + " &parameters=" + insGroupID + "," + employer + "," + userName,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "False") {


                    }
                }
            });
        }
        //轉入Temp
        function dlg4InsertTempBtn_OnClick() {
            var insGroupID = $("#dlg4InsGroupID").combobox('getValue');
            var employer = $("#dlg4Employer").combobox('getValue');
            var userName = getClientInfo('_username');

            if (insGroupID != '' && employer != '' && userName != '') {
                //清Temp
                //DeleteSalesDetailsMasterTemp();

                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster',
                    data: "mode=method&method=" + "BatchInsertTemp" + " &parameters=" + insGroupID + "," + employer + "," + userName,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False") {
                            var rows = $.parseJSON(data);
                            var jBERPRoomers, feeItems, totalCount, pageCount;
                            if (rows[0].Error == '0') {//@Error=0代表跑procedure沒出錯
                                alert('查詢資料來自:「' + rows[0].DormName + '」系統' + '\n'
                                    + '合約編號:' + rows[0].ContractID + '\n'
                                    + '合約房客數:' + rows[0].JCSRoomers + '\n'
                                    + '合約費用項目數:' + rows[0].FeeItems + '\n'
                                + 'EEP客戶數:' + rows[0].JBERPRoomers);
                                //+ '公司別:' + $("#InsGroupID_Query").combobox('getText') + '\n'
                                // + 

                                //設定pageSize
                                jBERPRoomers = rows[0].JBERPRoomers;
                                feeItems = rows[0].FeeItems;
                                totalCount = jBERPRoomers * feeItems;//feeItem
                                pageCount = (Math.ceil(totalCount / 10)) * 10
                                $("#dataGridTemp").datagrid({ pageSize: pageCount, pageList: [pageCount, pageCount * 2, pageCount * 3], });

                                $('#dataGridTemp').datagrid('reload');
                                return true;
                            }
                        }
                    }
                });
            } else {
                alert('請選取「公司別」、「雇主」，再批次新增');
            }
        }
        function dlg4InsGroupID_OnSelect(rowData) {
            $("#dlg4Employer").combobox('setValue', '');
            $("#dlg4Employer").combobox('setWhere', "CustomerID IN (Select Distinct CustomerID From CustomerSaleType A,SalesType B Where A.SalesTypeID = B.SalesTypeID AND B.InsGroupID = " + "'" + rowData.InsGroupID + "'" + ")");
        }
        //轉入銷貨
        function dlg4InsertSalesBtn_OnClick() {
            var rows = $("#dataGridTemp").datagrid('getRows');
            var dataString = JSON.stringify(rows);
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster',
                data: "mode=method&method=" + "BatchInsertSales" + " &parameters=" + dataString,// + "," + employer + "," + userName,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "False") {
                        var rows = $.parseJSON(data);
                        if (rows[0].Error == '0') {//@Error=0代表跑procedure沒出錯
                            DeleteSalesDetailsMasterTemp();
                            alert("轉入銷貨成功");
                            $('#dataGridTemp').datagrid('reload');

                            //alert('批次新增成功' + '，' + '已新增銷貨單數:' + rows[0].JBERPRoomers + '\n'
                            //    + '公司別:' + $("#InsGroupID_Query").combobox('getText') + '\n'
                            //    + '宿舍:' + rows[0].DormName + '\n'
                            //    + '合約編號:' + rows[0].ContractID + '\n'
                            //    + '合約房客數:' + rows[0].JCSRoomers + '\n'
                            //    + '合約費用項目數:' + rows[0].FeelItems + '\n'
                            //    + '請再填寫各房客的銷貨明細金額');
                            ////$("#dataGridTemp").datagrid('setWhere', '');
                            ////$("#dataGridView").datagrid('setWhere', "SalesMaster.InsGroupID='" + insGroupID + "' and SalesMaster.Employer='" + customerID + "' and SalesMaster.QInvoiceType='99' and SalesMaster.SalesDate=CONVERT(date, getdate()) and SalesMaster.IsActive=1");// and SalesDate='" + $.now() + "'
                            //$('#dataGridTemp').datagrid('reload');
                            return true;
                        }
                    }
                }
            });
        }
        //checkbox設定
        function OnLoadSuccess_dataGridTemp() {
            //單select，多check
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                $("#dataGridTemp").datagrid({
                    singleSelect: true,
                    selectOnCheck: false,
                    checkOnSelect: false
                });
            }

            //為了取消預設第一列勾選
            setTimeout(function () {
                $("#dataGridTemp").datagrid("unselectAll");
            }, 600);
        }
        //Temp多筆刪除
        function dataGridTemp_DeleteBtn_OnClick() {
            var rows = $("#dataGridTemp").datagrid('getChecked');
            for (var i = rows.length - 1 ; i >= 0 ; i--) {
                var index = $('#dataGridTemp').datagrid('getRowIndex', rows[i]);
                $('#dataGridTemp').datagrid('deleteRow', index);
            }
            apply($('#dataGridTemp'));
        }
        //Temp多筆修改dialog開啟
        function dataGridTempUpdateBtn_OnClick() {
            openForm("#dialogUpdateTemp", '', '', 'dialog');
            $("#dialogUpdateTemp").find(".infosysbutton-s").hide();
            $("#dialogUpdateTemp").find(".infosysbutton-c").hide();
        }
        //Temp多筆修改
        function submitFormUpdateTemp() {
            var rows = $("#dataGridTemp").datagrid('getChecked');
            var unitPrice = $.trim($("#dataFormUpdateTempUnitPrice").val());
            var quantity = $.trim($("#dataFormUpdateTempQuantity").val());
            //var feeItemID = $("#dataFormUpdateTempFeeItemID").combobox('getValue');
            //var salesTypeName = $.trim($("#dataFormUpdateTempSalesTypeName").val());
            var feeItemID = "";
            var salesTypeName = "";


            //if (rows.length != 0 && (unitPrice != '' || quantity != '' || feeItemID != '' || salesTypeName != '')) {
            if (rows.length != 0 && (unitPrice != '' || quantity != '')) {
                var salesNOs = [];
                var itemNOs = [];
                for (var i = 0 ; i < rows.length ; i++) {
                    salesNOs.push(rows[i].SalesNO);
                    itemNOs.push(rows[i].ItemNO);
                }
                var strSalesNOs = salesNOs.join("*");
                var strItemNOs = itemNOs.join("*");

                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster', //連接的Server端，command
                    data: "mode=method&method=" + "UpdateSalesDetailsMasterTemp" + "&parameters=" + strSalesNOs + "," + strItemNOs + "," + unitPrice + "," + quantity,// + "," + feeItemID + "," + salesTypeName,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False") {
                            closeForm("#dialogUpdateTemp");
                            //alert("已修改" + data + "筆")
                            $("#dataGridTemp").datagrid("reload");
                        } else {
                            alert(data);
                        }
                    }
                    , error: function () {
                    }
                });
            } else if (rows.length == 0) {
                alert('未勾選房客費用項目');
            } else if (unitPrice == '' && quantity == '') {// && feeItemID == '' && salesTypeName == ''
                alert('未修改任一欄位');
            }
        }
        //Temp多筆修改的費用項目_OnSelect
        function dataFormUpdateTempFeeItemID_OnSelect(rowdata) {
            $("#dataFormUpdateTempSalesTypeName").val(rowdata.FeeItemName);
        }

        function dataGridTempFeeItem_OnSelect() {
            var text = $(this).combobox("getText");
            var row = $('#dataGridTemp').datagrid('getSelected');
            var rowIndex = $('#dataGridTemp').datagrid('getRowIndex', row);
            var editor = $('#dataGridTemp').datagrid('getEditor', { index: rowIndex, field: 'SalesTypeName' });
            var combo = editor.target;
            editor.target.val(text);
        }
        function CloseTempBtn_OnClick() {
            closeForm('#dialogUpdateTemp');
        }
        function dataGridTemp_TrueFalse_FormatScript(val) {
            if (val == true)
                return "<input  type='checkbox' checked='true' onclick='return  false;'/>";
            else
                return "<input  type='checkbox' onclick='return false;'/>";
        }
        function dataFormUpdateTemp_SalesTypeID_OnSelect(rowdata) {
            $("#dataFormUpdateTempFeeItemID").combobox('setValue', '');
            $("#dataFormUpdateTempFeeItemID").combobox('setWhere', "SalesTypeID='" + rowdata.SalesTypeID + "'");
            //var salesTypeID = $.trim($("#dataFormUpdateTempSalesTypeID").combobox('getValue'));
            //$("#dataFormUpdateTempFeeItemID").combobox('setWhere', "SalesTypeID='" + salesTypeID + "'");
        }
        
        //---第三種-----批次新增-----------------------
        //dataGridViwe_ToolItem_批次新增
        function BatchAdd_OnClick(){
            openForm("#JQDialog5", '', '', 'dialog');
        }
        //批次新增銷貨dlg_匯入房客費用項目
        function dlg5SelectBtn_OnClick() {
            //轉入
            var insGroupID = $("#dlg5InsGroupID").combobox('getValue');
            var employer = $("#dlg5Employer").combobox('getValue');

            if (insGroupID == '' || employer == '') { alert('請先選取「公司別」和「雇主」'); return false; }

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster', //連接的Server端，command
                data: "mode=method&method=" + "SelectRoomerFeeItems" + "&parameters=" + insGroupID + "," + employer,
                cache: false,
                async: true,
                beforeSend:function(){
                    var win=$.messager.progress({
                        title: '轉入',
                        msg: '轉入明細資料,請稍後...',
                        interval: '500'
                    });
                },
                success: function (data) {
                    $.messager.progress('close');
                    if (data != false) {
                        var rows = $.parseJSON(data);
                        for (var i = 0; i < rows.length; i++) {
                            var rowData = new Object();
                            rowData['SalesNO'] = rows[i].SalesNO;
                            rowData['ItemNO'] = rows[i].ItemNO;
                            rowData['SalesTypeID'] = rows[i].SalesTypeID;
                            rowData['FeeItemID'] = rows[i].FeeItemID;
                            rowData['SalesTypeName'] = rows[i].SalesTypeName;
                            rowData['Quantity'] = rows[i].Quantity;
                            rowData['Unit'] = rows[i].Unit;
                            rowData['UnitPrice'] = rows[i].UnitPrice;
                            rowData['Amount'] = rows[i].Amount;
                            rowData['FeeItemNameShow'] = rows[i].FeeItemNameShow;

                            rowData['CustomerID'] = rows[i].CustomerID;
                            rowData['SalesDate'] = rows[i].SalesDate;
                            rowData['SalesID'] = rows[i].SalesID;
                            rowData['TaxType'] = rows[i].TaxType;
                            rowData['PayWayID'] = rows[i].PayWayID;
                            rowData['Remark'] = rows[i].Remark;
                            rowData['InsGroupID'] = rows[i].InsGroupID;
                            rowData['SalesKindID'] = rows[i].SalesKindID;
                            rowData['Employer'] = rows[i].Employer;
                            rowData['BalanceDate'] = rows[i].BalanceDate;
                            rowData['DebtorDays'] = rows[i].DebtorDays;
                            rowData['EmailAddress'] = rows[i].EmailAddress;
                            $("#dataGridDetail1").datagrid("appendRow", rowData);
                        }
                    }
                }
                , error: function () { $.messager.progress('close'); }
            });
        }
        //存檔
        function SubmitJQDialog5() {
            var yn = confirm("按下「確定」就會寫入銷貨");
            if (yn == false) {
                return
            }

            apply("#dataGridDetail1");
            $("#dataGridDetail1").datagrid('setWhere', '1=0');//清資料
            closeForm('#JQDialog5');

            var insGroupID = $("#dlg5InsGroupID").combobox('getValue');
            var employer = $("#dlg5Employer").combobox('getValue');

            var d = new Date();
            var month = d.getMonth() + 1;
            var day = d.getDate();
            var salesDate = d.getFullYear() + '/' +
                (month < 10 ? '0' : '') + month + '/' +
                (day < 10 ? '0' : '') + day;

            $("#dataGridView").datagrid('setWhere', "InsGroupID='" + insGroupID + "' and Employer='" + employer + "' and SalesDate='" + salesDate + "' and (UploadCode NOT IN  ('IS','RS','HS') or UploadCode is null) and IsActive = '1'");
        }
        //關閉
        function CloseJQDialog5() {
            $("#dataGridDetail1").datagrid('setWhere', '1=0');//清資料
            closeForm('#JQDialog5');
        }

        function dataGridDetail_OnLoadSuccess() {
            //單select，多check
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                $("#dataGridDetail1").datagrid({
                    singleSelect: true,
                    selectOnCheck: false,
                    checkOnSelect: false
                });
            }
        }

        //多筆刪除
        function MultiDelete() {
            var rows = $("#dataGridDetail1").datagrid('getChecked');
            var index;
            for (var i = 0; i < rows.length; i++) {
                index = $("#dataGridDetail1").datagrid('getRowIndex', rows[i]);//只能對一個row取
                $("#dataGridDetail1").datagrid('deleteRow', index);//刪除後，index就會改變
            }
        }
        //多筆存檔
        function MultiUpdate_OpenForm() {
            openForm('#JQDialog6', '', '', 'dialog');
            $("#JQDialog6").find(".infosysbutton-s").hide();
            $("#JQDialog6").find(".infosysbutton-c").hide();
        }
        //多筆修改dlg_存檔
        function MultiUpdate() {
            var unitPrice=$.trim($("#dataGridDetail1FormUnitPrice").val());
            var quantity=$.trim($("#dataGridDetail1FormQuantity").val());
            var checkRows = $("#dataGridDetail1").datagrid('getChecked');
            var dataRows = $("#dataGridDetail1").datagrid('getRows');

            //先把全部資料暫存起來，給forloop用
            var dataRowsTemp = [];
            for (var i = 0; i < dataRows.length; i++) {
                dataRowsTemp.push(dataRows[i]);
            }

            //打勾的Index，給forloop用
            var checkRowIndexes = [];
            var Index;
            for (var i = 0; i < checkRows.length; i++) {
                index = $("#dataGridDetail1").datagrid('getRowIndex', checkRows[i]);
                checkRowIndexes.push(index)
            }

            //刪除全部資料
            var index;
            for (var i = 0; i < dataRowsTemp.length; i++) {
                index = $("#dataGridDetail1").datagrid('getRowIndex', dataRowsTemp[i]);
                $("#dataGridDetail1").datagrid('deleteRow', index);
            }

            //新增資料
            for (var i = 0; i < dataRowsTemp.length; i++) {
                var rowData = new Object();
                if (checkRowIndexes.includes(i)) {//等於打勾的
                    rowData['SalesNO'] = dataRowsTemp[i].SalesNO;
                    rowData['ItemNO'] = dataRowsTemp[i].ItemNO;
                    rowData['SalesTypeID'] = dataRowsTemp[i].SalesTypeID;
                    rowData['FeeItemID'] = dataRowsTemp[i].FeeItemID;
                    rowData['SalesTypeName'] = dataRowsTemp[i].SalesTypeName;
                    if (quantity != '') {rowData['Quantity'] = quantity;} else {rowData['Quantity'] = dataRowsTemp[i].Quantity;}
                    if (unitPrice != '') {rowData['UnitPrice'] = unitPrice;} else {rowData['UnitPrice'] = dataRowsTemp[i].UnitPrice;}
                    rowData['Unit'] = dataRowsTemp[i].Unit;
                    rowData['FeeItemNameShow'] = dataRowsTemp[i].FeeItemNameShow;

                    rowData['CustomerID'] = dataRowsTemp[i].CustomerID;
                    rowData['SalesDate'] = dataRowsTemp[i].SalesDate;
                    rowData['SalesID'] = dataRowsTemp[i].SalesID;
                    rowData['TaxType'] = dataRowsTemp[i].TaxType;
                    rowData['PayWayID'] = dataRowsTemp[i].PayWayID;
                    rowData['Remark'] = dataRowsTemp[i].Remark;
                    rowData['InsGroupID'] = dataRowsTemp[i].InsGroupID;
                    rowData['SalesKindID'] = dataRowsTemp[i].SalesKindID;
                    rowData['Employer'] = dataRowsTemp[i].Employer;
                    rowData['BalanceDate'] = dataRowsTemp[i].BalanceDate;
                    rowData['DebtorDays'] = dataRowsTemp[i].DebtorDays;
                    rowData['EmailAddress'] = dataRowsTemp[i].EmailAddress;
                    $("#dataGridDetail1").datagrid("appendRow", rowData);
                } else {//不等於打勾的
                    rowData = dataRowsTemp[i];
                    $("#dataGridDetail1").datagrid("appendRow", rowData);
                }
            }
            closeForm('#JQDialog6');
        }
        //多筆存檔dlg_關閉
        function CloseMultiUpdateForm() {
            closeForm('#JQDialog6');
        }
        
        //批次新增銷貨_公司別_OnSelect
        function dlg5InsGroupID_OnSelect(rowData) {
            $("#dlg5Employer").combobox('setValue', '');
            $("#dlg5Employer").combobox('setWhere', "CustomerID IN (Select Distinct CustomerID From CustomerSaleType A,SalesType B Where A.SalesTypeID = B.SalesTypeID AND B.InsGroupID = " + "'" + rowData.InsGroupID + "'" + ")");
        }

        function dataFormDetail_OnApply() {
            var salesTypeName = $("#dataFormDetailSalesTypeName").combobox('getValue');
            if (salesTypeName == '') {
                alert("「銷貨品名」必填");
                return false;
            }
            return true;
        }

        function dfm_TaxType_OnSelect(rowdata) {
            $("#dataFormMasterTaxRate").val(rowdata.TaxRate);
        }
        //發票號碼是否在沖款明細資料裡
        function CheckInvoiceNOIsInWarrantDetails(InvoiceNO) {
            var ReturnStr = false;
            if (InvoiceNO != "") {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster', //連接的Server端，command
                    data: "mode=method&method=" + "CheckInvoiceNOIsInWarrantDetails" + "&parameters=" + InvoiceNO,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            var rows = $.parseJSON(data);
                            if (rows[0]["Counts"] > 0) {
                                ReturnStr = true;
                            }
                        }
                    }
                , error: function () { }
                });
            }
            return ReturnStr;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPSalesInvoices.SalesMaster" runat="server" AutoApply="True"
                DataMember="SalesMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="20,40,80,160" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計:" UpdateCommandVisible="True" ViewCommandVisible="True" OnLoadSuccess="dataGridViewOnLoadSucess" OnUpdate="dataGridViewOnUpdate">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨單號" Editor="text" FieldName="SalesNO" Format="" MaxLength="0" Width="90" Sortable="True" TableName="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶" Editor="text" FieldName="ShortName" Format="" MaxLength="0" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨日期" Editor="text" FieldName="SalesDate" Format="yyyy/mm/dd" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="單據種類" Editor="text" FieldName="BillTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷售類別" Editor="text" FieldName="SalesTypeName" MaxLength="0" Width="100" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" Width="100" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesInvoices.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="明細數" Editor="text" FieldName="SalesNum" MaxLength="0" Width="40" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="銷貨金額" Editor="text" FieldName="SalesAmount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="75" Format="N" Total="sum">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="捐贈註記" Editor="text" FieldName="DonateMark" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="載具類別" Editor="text" FieldName="CarrierTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="愛心碼" Editor="text" FieldName="NPOBAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="載具顯碼" Editor="text" FieldName="CarrierID1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="載具隱碼" Editor="text" FieldName="CarrierID2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="業務人員" Editor="text" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="稅別" Editor="infocombobox" FieldName="TaxType" Format="" Width="50" EditorOptions="valueField:'TaxTypeID',textField:'TaxTypeName',remoteName:'sERPSalesInvoices.TaxType',tableName:'TaxType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="稅率" Editor="numberbox" FieldName="TaxRate" Format="" MaxLength="0" Width="40" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務員" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Width="80" Visible="False" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceType" Editor="text" FieldName="InvoiceType" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="InsGroupID" Editor="numberbox" FieldName="InsGroupID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="120" Format="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="稅內含" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsHasTax" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="40" FormatScript="genCheckBox">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="發票欲開年月" Editor="text" FieldName="WantedInvoiceYM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="75">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="狀態" Editor="text" FieldName="UploadDesc" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" EditorOptions="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="收據發票號碼" Editor="text" FieldName="InvoiceNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="發票年月" Editor="text" FieldName="InvoiceYM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="發票日期" Editor="text" FieldName="InvoiceDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="明細全顯" Editor="text" EditorOptions="" FieldName="IsOutPutDetails" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="tmpInvoiceNO" Editor="text" FieldName="tmpInvoiceNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨品名清單" Editor="text" FieldName="SalesTypeName1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateInvoiceType" Editor="text" FieldName="CreateInvoiceType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨狀態" Editor="text" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="備註(內部)" Editor="text" FieldName="RemarkForInner" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceNO_01024748" Editor="text" FieldName="InvoiceNO_01024748" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceDate_01024748" Editor="datebox" FieldName="InvoiceDate_01024748" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"     OnClick="dataGridView_Add_OnClick" Text="新增銷貨" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-upload" ItemType="easyui-linkbutton"  OnClick="CreateInvoice" Text="開立發票" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton"    OnClick="CancelInvoice" Text="作廢發票"/>
                    <JQTools:JQToolItem Icon="icon-upload" ItemType="easyui-linkbutton"  OnClick="SelectCreateReceipt" Text="開立收據" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton"    OnClick="CancelReceipt" Text="作廢收據"/>
                    <JQTools:JQToolItem Icon="icon-cut" ItemType="easyui-linkbutton"     OnClick="SelectDelete" Text="作廢銷貨" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" ItemType="easyui-linkbutton" OnClick="BatchAdd_OnClick" Text="批次新增二聯(宿舍)" Visible="True" Icon="icon-add" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sERPSalesInvoices.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:QInsGroupOnSelect,panelHeight:200" FieldName="InsGroupID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="130" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務員" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalesInvoices.QSalesPerson',tableName:'QSalesPerson',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨起迄日期" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="SalesDateS" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" DefaultMethod="getFirstDate" />
                    <JQTools:JQQueryColumn AndOr="and" Caption=" ~ " Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="SalesDateE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" DefaultValue="_today" Format="yyyy/mm/dd" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="單據種類" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'INVOICETYPEID',textField:'INVOICETYPENAME',remoteName:'sERPSalesInvoices.QInvoiceType',tableName:'QInvoiceType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InvoiceType" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="120" />
      
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨單號" Condition="%" DataType="string" Editor="text" FieldName="SalesNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
      
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨客戶" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustomerID',textField:'ShortName',remoteName:'sERPSalesInvoices.QCustomer',tableName:'QCustomer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustomerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="130" />
      
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨類別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesInvoices.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="130" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="單據狀態" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'0',text:'全部',selected:'false'},{value:'1',text:'未開單據',selected:'true'},{value:'2',text:'已開單據',selected:'false'},{value:'3',text:'已作廢單據',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsNotputBill" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="120" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="雇主" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CUSTOMERID',textField:'SHORTNAME',remoteName:'sERPSalesInvoices.QEmployer',tableName:'QEmployer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Employer" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="120" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨狀態" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'有效',selected:'true'},{value:'0',text:'無效',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsActive" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
      
                    <JQTools:JQQueryColumn AndOr="and" Caption="發票年月" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'InvoiceYM',textField:'InvoiceYM',remoteName:'sERPSalesInvoices.QInvoiceYM',tableName:'QInvoiceYM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InvoiceYM" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
      
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="銷貨資料維護" Width="880px" DialogLeft="0px" DialogTop="50px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SalesMaster" HorizontalColumnsCount="4" RemoteName="sERPSalesInvoices.SalesMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormMasterOnLoadSuccess" OnApply="dataFormMasterOnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶簡稱" Editor="inforefval" EditorOptions="title:'客戶簡稱',panelWidth:690,panelHeight:400,remoteName:'sERPSalesInvoices.Customer',tableName:'Customer',columns:[{field:'TaxNO',title:'統編',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'TelNO',title:'電話',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ShortName',title:'簡稱',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustomerName',title:'名稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'WebHostName',title:'來源',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustomerID',title:'代號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustomerID',textField:'ShortName',valueFieldCaption:'客戶ID',textFieldCaption:'客戶簡稱',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,onSelect:CustomerOnSelect,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CustomerID" Format="" maxlength="0" ReadOnly="False" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="" MaxLength="0" ReadOnly="False" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨單號" Editor="text" FieldName="SalesNO" Format="" Width="120" ReadOnly="True" Visible="True" PlaceHolder="" />
                        <JQTools:JQFormColumn Alignment="right" Caption="銷貨金額" Editor="text" FieldName="SalesAmount" Format="N3" maxlength="0" ReadOnly="True" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesInvoices.CustomerSaleType',tableName:'CustomerSaleType',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:SalesTypeID_OnSelect,panelHeight:200" FieldName="SalesTypeID" Format="" PlaceHolder="" ReadOnly="True" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單據類別" Editor="infocombobox" EditorOptions="valueField:'INVOICETYPEID',textField:'INVOICETYPENAME',remoteName:'sERPSalesInvoices.QInvoiceType',tableName:'QInvoiceType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:QInvoiceType_OnSelect,panelHeight:200" FieldName="QInvoiceType" ReadOnly="False" Width="120" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNO" maxlength="0" ReadOnly="True" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務員" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalesInvoices.SalesPerson',tableName:'SalesPerson',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" Format="" Width="120" MaxLength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收款結帳日" Editor="text" FieldName="BalanceDate" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款天數" Editor="text" FieldName="DebtorDays" MaxLength="0" ReadOnly="True" Width="120" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主" Editor="inforefval" FieldName="Employer" Visible="True" Width="120" MaxLength="0" ReadOnly="False" EditorOptions="title:'雇主',panelWidth:350,panelHeight:400,remoteName:'sERPSalesInvoices.Employer',tableName:'Employer',columns:[],columnMatches:[],whereItems:[],valueField:'CUSTOMERID',textField:'SHORTNAME',valueFieldCaption:'客戶ID',textFieldCaption:'客戶簡稱',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" Format="" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="已含稅" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsHasTax" maxlength="0" Visible="True" Width="30" ReadOnly="False" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="稅別" Editor="infocombobox" FieldName="TaxType" Format="" maxlength="0" Visible="True" Width="120" Span="1" EditorOptions="valueField:'TaxTypeID',textField:'TaxTypeName',remoteName:'sERPSalesInvoices.TaxType',tableName:'TaxType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:dfm_TaxType_OnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="稅率" Editor="text" FieldName="TaxRate" Format="" Width="120" maxlength="0" ReadOnly="False" Span="1" Visible="True" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Mail傳送" Editor="checkbox" FieldName="MailSend" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" EditorOptions="on:1,off:0" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="郵件信箱" Editor="text" FieldName="EmailAddress" NewRow="False" Visible="True" Width="295" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="捐贈註記" Editor="infocombobox" EditorOptions="valueField:'DonateMarkID',textField:'DonateMark',remoteName:'sERPSalesInvoices.DonateMark',tableName:'DonateMark',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="DonateMarkID" Format="" Visible="True" Width="120" maxlength="0" ReadOnly="False" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="載具類別" Editor="infocombobox" FieldName="CarrierType" Format="" maxlength="0" Visible="True" Width="120" EditorOptions="valueField:'CarrierType',textField:'CarrierTypeName',remoteName:'sERPSalesInvoices.CarrierType',tableName:'CarrierType',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="愛心碼" Editor="text" FieldName="NPOBAN" Format="" maxlength="0" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="載具顯碼" Editor="text" FieldName="CarrierID1" maxlength="0" Visible="True" Width="120" ReadOnly="False" Span="1" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註(發票)" Editor="textarea" EditorOptions="height:18" FieldName="Remark" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="680" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註(內部)" Editor="textarea" EditorOptions="height:60" FieldName="RemarkForInner" MaxLength="0" ReadOnly="False" Span="4" Visible="True" Width="680" NewRow="True" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="明細全顯示" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsOutPutDetails" Width="30" Visible="True" maxlength="0" ReadOnly="False" NewRow="False" RowSpan="1" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsActive" Editor="checkbox" FieldName="IsActive" Width="80" Visible="False" EditorOptions="on:1,off:0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票種類" Editor="infocombobox" FieldName="InvoiceType" Format="" Visible="False" Width="130" EditorOptions="valueField:'INVOICETYPEID',textField:'INVOICETYPENAME',remoteName:'sERPSalesInvoices.InvoiceType',tableName:'InvoiceType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CarrierID2" Editor="text" FieldName="CarrierID2" Format="" Width="180" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayWayID" Editor="text" FieldName="PayWayID" Format="" Width="180" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票號碼" Editor="text" FieldName="InvoiceNO" Width="80" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="InsGroupID" Editor="numberbox" FieldName="InsGroupID" Format="" Width="180" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="180" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" maxlength="0" Width="180" Visible="False" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesNOTemp" Editor="text" FieldName="SalesNOTemp" maxlength="0" ReadOnly="False" Visible="False" Width="180" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="網站代碼" Editor="text" FieldName="APIWebCode" maxlength="0" ReadOnly="False" Visible="False" Width="80" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="網站密碼" Editor="text" FieldName="APIPassword" MaxLength="0" ReadOnly="False" Visible="False" Width="80" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="text" EditorOptions="valueField:'SalesKindID',textField:'SalesKindName',remoteName:'sERPSalesInvoices.SalesKind',tableName:'SalesKind',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesKindID" MaxLength="0" ReadOnly="False" Visible="False" Width="130" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UploadCode" Editor="text" FieldName="UploadCode" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CustomerTypeID" Editor="text" FieldName="CustomerTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="TempTaxNO" Editor="text" FieldName="TempTaxNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <br />
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="SalesDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sERPSalesInvoices.SalesMaster" Title="明細資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計" UpdateCommandVisible="True" ViewCommandVisible="True"  >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="SalesNO" Editor="text" FieldName="SalesNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="130" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesInvoices.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="費用項目" Editor="infocombobox" EditorOptions="valueField:'FeeItemID',textField:'FeeItemName',remoteName:'sERPSalesInvoices.FeeItem',tableName:'FeeItem',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="FeeItemID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="150">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="費用項目" Editor="text" FieldName="FeeItemNameShow" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="銷貨品名" Editor="text" FieldName="SalesTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="160">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="單位" Editor="text" FieldName="Unit" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="數量" Editor="text" FieldName="Quantity" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="單價" Editor="text" FieldName="UnitPrice" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" Format="N">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="銷貨金額" Editor="text" FieldName="Amount" Format="N" Frozen="False" IsNvarChar="False" MaxLength="0" OnTotal="dataGridDetailAmount_OnTotal" QueryCondition="" ReadOnly="False" Sortable="False" Total="sum" Visible="True" Width="120">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="SalesNO" ParentFieldName="SalesNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" Visible="False" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" Visible="False" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="SalesNO" RemoteMethod="True" DefaultMethod="" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="SalesDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="TaxType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0.05" FieldName="TaxRate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="07" FieldName="InvoiceType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsActive" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsOutPutDetails" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2" FieldName="DonateMarkID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="Z" FieldName="FlowFlag" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="00" FieldName="UploadCode" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="MailSend" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustomerID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesDate" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="QInvoiceType" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesTypeID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DonateMarkID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DebtorDays" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="TaxType" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Width="900px" DialogTop="150px" Title="銷貨明細" DialogLeft="50px">
                <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="SalesDetails" HorizontalColumnsCount="4" RemoteName="sERPSalesInvoices.SalesMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormDetail_OnLoadSuccess" OnApply="dataFormDetail_OnApply" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="SalesNO" Editor="text" FieldName="SalesNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="center" Caption="項次" Editor="text" FieldName="ItemNO" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="30" />
                            <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesInvoices.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="費用項目" Editor="infocombobox" EditorOptions="valueField:'FeeItemID',textField:'FeeItemName',remoteName:'sERPSalesInvoices.FeeItem',tableName:'FeeItem',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:FeeItemID_OnSelect,panelHeight:200" FieldName="FeeItemID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                            <JQTools:JQFormColumn Alignment="left" Caption="銷貨品名" Editor="infocombobox" FieldName="SalesTypeName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" EditorOptions="valueField:'SalesItemName',textField:'SalesItemName',remoteName:'sERPSalesInvoices.SalesItem',tableName:'SalesItem',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="數量" Editor="numberbox" FieldName="Quantity" MaxLength="0" NewRow="True" OnBlur="dataFormDetail_Quantity_OnBlur" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="單位" Editor="text" FieldName="Unit" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" />
                            <JQTools:JQFormColumn Alignment="right" Caption="單價" Editor="numberbox" FieldName="UnitPrice" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" OnBlur="dataFormDetail_Quantity_OnBlur" />
                            <JQTools:JQFormColumn Alignment="left" Caption="項目金額" Editor="numberbox" FieldName="Amount" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="SalesNO" ParentFieldName="SalesNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="' '" FieldName="DType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="Quantity" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="UnitPrice" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Amount" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesTypeName" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Quantity" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="UnitPrice" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="ItemNO" NumDig="3" />
            </JQTools:JQDialog>

        <%--<JQTools:JQDialog ID="JQDialog3"  BindingObjectID="JQDataForm1" runat="server" DialogLeft="10px" DialogTop="65px" Title="產生收據" Width="520px" Closed="False" ShowSubmitDiv="True">
                  <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="SalesMaster" HorizontalColumnsCount="2" RemoteName="sERPSalesInvoices.SalesMaster" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" ParentObjectID="dataGridView">
                        <Columns>
                             <JQTools:JQFormColumn Alignment="left" Caption="發票作廢文號" Editor="text" FieldName="ReturnTaxNumber" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                             <JQTools:JQFormColumn Alignment="left" Caption="作廢原因" Editor="text" FieldName="ReturnRemark" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    </JQTools:JQDefault>
                  <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm1">
                      <Columns>
                          <JQTools:JQValidateColumn CheckNull="True" FieldName="ReturnRemark" RemoteMethod="True" ValidateType="None" />
                      </Columns>
                  </JQTools:JQValidate>
            </JQTools:JQDialog>--%>

        <%--第二種批次新增--%>
        <%--<JQTools:JQDialog ID="JQDialog4" runat="server" Width="1000px" Title="批次新增二聯銷貨(宿舍)" DialogLeft="30px" DialogTop="30px">
                      <JQTools:JQLabel ID="JQLabel1" runat="server" Text="公司別" />
                      <JQTools:JQComboBox ID="dlg4InsGroupID" runat="server" DisplayMember="ShortName" RemoteName="sERPSalesInvoices.InsGroup" ValueMember="InsGroupID" OnSelect="dlg4InsGroupID_OnSelect">
                      </JQTools:JQComboBox>
                      <JQTools:JQLabel ID="JQLabel2" runat="server" Text="雇主" />
                      <JQTools:JQComboBox ID="dlg4Employer" runat="server" DisplayMember="SHORTNAME" RemoteName="sERPSalesInvoices.Employer" ValueMember="CUSTOMERID">
                      </JQTools:JQComboBox>
                      <JQTools:JQButton ID="dlg4InsertTempBtn" runat="server" OnClick="dlg4InsertTempBtn_OnClick" Text="匯入房客費用項目" />
                      <JQTools:JQButton ID="dlg4InsertSalesBtn" runat="server" OnClick="dlg4InsertSalesBtn_OnClick" Text="轉入銷貨" />
                      <JQTools:JQDataGrid ID="dataGridTemp" runat="server" DataMember="SalesDetailsMasterTemp" RemoteName="sERPSalesInvoices.SalesDetailsMasterTemp" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" Title="房客費用項目" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnLoadSuccess="OnLoadSuccess_dataGridTemp">
                          <Columns>
                              <JQTools:JQGridColumn Alignment="left" Caption="SalesNO" Editor="text" FieldName="SalesNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="客戶編號" Editor="text" FieldName="CustomerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Remark" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="150">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="單價" Editor="numberbox" FieldName="UnitPrice" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="數量" Editor="numberbox" FieldName="Quantity" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="費用項目" Editor="infocombobox" FieldName="FeeItemID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90" EditorOptions="valueField:'FeeItemID',textField:'FeeItemName',remoteName:'sERPSalesInvoices.FeeItem',tableName:'FeeItem',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:dataGridTempFeeItem_OnSelect,panelHeight:200">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="費用項目" Editor="text" FieldName="FeeITemNameShow" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="80">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="銷貨內容" Editor="text" FieldName="SalesTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="單位" Editor="text" FieldName="Unit" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesInvoices.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="Amount" Editor="text" FieldName="Amount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="銷貨日期" Editor="text" FieldName="SalesDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90" Format="yyyy/mm/dd">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="業務" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalesInvoices.SalesPerson',tableName:'SalesPerson',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="捐贈註記" Editor="infocombobox" EditorOptions="valueField:'DonateMarkID',textField:'DonateMark',remoteName:'sERPSalesInvoices.DonateMark',tableName:'DonateMark',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="DonateMarkID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="InvoiceType" Editor="infocombobox" EditorOptions="valueField:'INVOICETYPEID',textField:'INVOICETYPENAME',remoteName:'sERPSalesInvoices.InvoiceType',tableName:'InvoiceType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InvoiceType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="單據類別" Editor="infocombobox" FieldName="QInvoiceType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90" EditorOptions="valueField:'INVOICETYPEID',textField:'INVOICETYPENAME',remoteName:'sERPSalesInvoices.QInvoiceType',tableName:'QInvoiceType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="統編" Editor="text" FieldName="TaxNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="稅別" Editor="infocombobox" FieldName="TaxType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="50" EditorOptions="valueField:'TaxTypeID',textField:'TaxTypeName',remoteName:'sERPSalesInvoices.TaxType',tableName:'TaxType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="稅率" Editor="text" FieldName="TaxRate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="付款方式" Editor="infocombobox" FieldName="PayWayID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="50" EditorOptions="valueField:'PayWayID',textField:'PayWayName',remoteName:'sERPSalesInvoices.PayWay',tableName:'PayWay',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="Mail傳送" Editor="text" FieldName="MailSend" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sERPSalesInvoices.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InsGroupID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="銷貨主類別" Editor="infocombobox" EditorOptions="valueField:'SalesKindID',textField:'SalesKindName',remoteName:'sERPSalesInvoices.SalesKind',tableName:'SalesKind',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesKindID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="雇主" Editor="infocombobox" FieldName="Employer" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90" EditorOptions="valueField:'CUSTOMERID',textField:'SHORTNAME',remoteName:'sERPSalesInvoices.Employer',tableName:'Employer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="已含稅" Editor="checkbox" FieldName="IsHasTax" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90" EditorOptions="on:1,off:0" FormatScript="dataGridTemp_TrueFalse_FormatScript">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="UploadCode" Editor="text" FieldName="UploadCode" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="收款結帳日	" Editor="text" FieldName="BalanceDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="付款天數" Editor="text" FieldName="DebtorDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="郵件信箱" Editor="text" FieldName="EmailAddress" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="IsActive" Editor="text" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="明細全顯示" Editor="checkbox" FieldName="IsOutPutDetails" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90" EditorOptions="on:1,off:0" FormatScript="dataGridTemp_TrueFalse_FormatScript">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="FlowFlag" Editor="text" FieldName="FlowFlag" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                              <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                              </JQTools:JQGridColumn>
                          </Columns>
                          <TooItems>
                              <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="Insert" Visible="False" />
                              <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="dataGridTempUpdateBtn_OnClick" Text="修改" Visible="True" />
                              <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="dataGridTemp_DeleteBtn_OnClick" Text="刪除" Visible="True" />
                              <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="False" />
                              <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" Visible="False" />
                              <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                              <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="False" />
                          </TooItems>
                      </JQTools:JQDataGrid>
         </JQTools:JQDialog>
            <JQTools:JQDialog ID="dialogUpdateTemp" runat="server" BindingObjectID="dataFormUpdateTemp" Title="批次修改" Width="800px">
                <JQTools:JQDataForm ID="dataFormUpdateTemp" runat="server" DataMember="SalesDetailsMasterTemp" HorizontalColumnsCount="2" RemoteName="sERPSalesInvoices.SalesDetailsMasterTemp" ParentObjectID="" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="單價" Editor="numberbox" FieldName="UnitPrice" maxlength="0" Width="80" Visible="True" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="數量" Editor="numberbox" FieldName="Quantity" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="150" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesInvoices.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:dataFormUpdateTemp_SalesTypeID_OnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="費用項目" Editor="infocombobox" FieldName="FeeItemID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="150" EditorOptions="valueField:'FeeItemID',textField:'FeeItemName',remoteName:'sERPSalesInvoices.FeeItem',tableName:'FeeItem',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:dataFormUpdateTempFeeItemID_OnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨內容" Editor="text" FieldName="SalesTypeName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="150" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster0" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster0" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
                <br />
                <div style="text-align:center">
                <JQTools:JQButton ID="UpdateTempBtn" runat="server" OnClick="submitFormUpdateTemp" Text="存檔" />
                <JQTools:JQButton ID="CloseTempBtn" runat="server" OnClick="CloseTempBtn_OnClick" Text="關閉" />
                </div>
            </JQTools:JQDialog>--%>

        <%--第三種批次新增--%>
        <JQTools:JQDialog ID="JQDialog5" runat="server" Width="1000px" BindingObjectID="" Title="批次新增銷貨" DialogLeft="30px" DialogTop="50px">
            <JQTools:JQLabel ID="JQLabel3" runat="server" Text="公司別" />
            <JQTools:JQComboBox ID="dlg5InsGroupID" runat="server" DisplayMember="ShortName" OnSelect="dlg5InsGroupID_OnSelect" RemoteName="sERPSalesInvoices.InsGroup" ValueMember="InsGroupID">
            </JQTools:JQComboBox>
            <JQTools:JQLabel ID="JQLabel4" runat="server" Text="雇主" />
            <JQTools:JQComboBox ID="dlg5Employer" runat="server" DisplayMember="SHORTNAME" RemoteName="sERPSalesInvoices.QEmployer" ValueMember="CUSTOMERID">
            </JQTools:JQComboBox>
            <JQTools:JQButton ID="dlg5SelectBtn" runat="server" OnClick="dlg5SelectBtn_OnClick" Text="匯入房客費用項目" />
            <JQTools:JQDataGrid ID="dataGridDetail1" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="SalesDetails1" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPSalesInvoices.SalesDetails1" RowNumbers="True" Title="銷貨明細" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="dataGridDetail_OnLoadSuccess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesNO" Editor="text" FieldName="SalesNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Remark" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="130">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="100" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesInvoices.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="FeeItemID" Editor="text" FieldName="FeeItemID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="費用項目" Editor="text" FieldName="FeeItemNameShow" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨內容" Editor="text" FieldName="SalesTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="數量" Editor="text" FieldName="Quantity" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="單位" Editor="text" FieldName="Unit" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="單價" Editor="text" FieldName="UnitPrice" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="Amount" Editor="text" FieldName="Amount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶" Editor="infocombobox" FieldName="CustomerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90" EditorOptions="valueField:'CustomerID',textField:'ShortName',remoteName:'sERPSalesInvoices.Customer',tableName:'Customer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="70" Format="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="業務" Editor="infocombobox" FieldName="SalesID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="50" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalesInvoices.SalesPerson',tableName:'SalesPerson',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="DonateMarkID" Editor="text" FieldName="DonateMarkID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceType" Editor="text" FieldName="InvoiceType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="QInvoiceType" Editor="text" FieldName="QInvoiceType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="TaxNO" Editor="text" FieldName="TaxNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="TaxType" Editor="text" FieldName="TaxType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="TaxRate" Editor="text" FieldName="TaxRate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="PayWayID" Editor="text" FieldName="PayWayID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="MailSend" Editor="text" FieldName="MailSend" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="InsGroupID" Editor="text" FieldName="InsGroupID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesKindID" Editor="text" FieldName="SalesKindID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="Employer" Editor="text" FieldName="Employer" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="IsHasTax" Editor="text" FieldName="IsHasTax" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="UploadCode" Editor="text" FieldName="UploadCode" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="BalanceDate" Editor="text" FieldName="BalanceDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="DebtorDays" Editor="text" FieldName="DebtorDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="EmailAddress" Editor="text" FieldName="EmailAddress" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="IsActive" Editor="text" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="IsOutPutDetails" Editor="text" FieldName="IsOutPutDetails" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="FlowFlag" Editor="text" FieldName="FlowFlag" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="Insert" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="MultiUpdate_OpenForm" Text="多筆修改" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="MultiDelete" Text="多筆刪除" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="False" />
                </TooItems>
            </JQTools:JQDataGrid>
            <div style="text-align:center">
            <JQTools:JQButton ID="UpdateTempBtn0" runat="server" OnClick="SubmitJQDialog5" Text="存檔" />
            <JQTools:JQButton ID="CloseTempBtn0" runat="server" OnClick="CloseJQDialog5" Text="關閉" />
            </div>
        </JQTools:JQDialog>

        <JQTools:JQDialog ID="JQDialog6" runat="server" BindingObjectID="dataGridDetail1Form" Title="批次修改" Width="300px">
                <JQTools:JQDataForm ID="dataGridDetail1Form" runat="server" DataMember="SalesDetailsMasterTemp" HorizontalColumnsCount="2" RemoteName="sERPSalesInvoices.SalesDetailsMasterTemp" ParentObjectID="" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="單價" Editor="numberbox" FieldName="UnitPrice" maxlength="0" Width="80" Visible="True" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="數量" Editor="numberbox" FieldName="Quantity" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="150" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesInvoices.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:dataFormUpdateTemp_SalesTypeID_OnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="費用項目" Editor="infocombobox" FieldName="FeeItemID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="150" EditorOptions="valueField:'FeeItemID',textField:'FeeItemName',remoteName:'sERPSalesInvoices.FeeItem',tableName:'FeeItem',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:dataFormUpdateTempFeeItemID_OnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨內容" Editor="text" FieldName="SalesTypeName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="150" />
                    </Columns>
                </JQTools:JQDataForm>
                
                <br />
                <div style="text-align:center">
                <JQTools:JQButton ID="JQButton1" runat="server" OnClick="MultiUpdate" Text="存檔" />
                <JQTools:JQButton ID="JQButton2" runat="server" OnClick="CloseMultiUpdateForm" Text="關閉" />
                </div>
        </JQTools:JQDialog>
        
        </div>
    </form>
</body>
</html>
