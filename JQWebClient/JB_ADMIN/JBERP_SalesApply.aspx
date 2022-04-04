<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesApply.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        //小元件,js script,xoml皆用sERPSalesInvoices(主要用這個，不得已才用sERPSalesApply)
        //dataGridMaster,dataGridDetail,dataFormMaster,dataFormDetail皆用sERPSalesApply

        //宣告公共變數
        var P_backcolor = "#cbf1de";
        $(document).ready(function () {
            $(function () {
                var Link = $("<a>").attr({ 'href': '#', 'onclick': 'openCustomerTab()' }).text("修改");
                $('#dataFormMasterCustomerID').closest('td').append('&nbsp;&nbsp;&nbsp;').append(Link);

                //var remark = $("<label>").text("&nbsp;格式:yyyy/MM");
                $('#dataFormMasterWantedInvoiceYM').closest('td').append("&nbsp;<font color='blue'>格式:yyyy/MM</font>");

                $('#dataFormMasterIsOutPutDetails').closest('td').append("&nbsp;<font color='blue'>若勾，紙本發票明細會顯示「銷貨品名」。若沒勾，會顯示一筆「銷貨類別」</font>");

                $('#dataFormMasterSaleNotes').closest('td').append("&nbsp;<font color='blue'>內容將顯示在發票證明聯備註欄內</font>");
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
        
        function dataFormMasterOnLoadSuccess() {

            var param = Request.getQueryStringByName("P1");
            if (param == '') { param = Request.getQueryStringByName2("P1"); }

            if (getEditMode($("#dataFormMaster")) == 'inserted' || getEditMode($("#dataFormMaster")) == 'updated') {
                //依使用者過濾公司別
                var UserID = getClientInfo("UserID");
                //此使用者有的銷貨類別，銷貨類別對應的公司別
                $('#dataFormMasterInsGroupID').combobox('setWhere', "InsGroupID IN (Select Distinct InsGroupID From SalesSalesType X,SalesType Y Where X.SalesTypeID=Y.SalesTypeID AND X.SalesID = '" + UserID + "')");

                if (getEditMode($("#dataFormMaster")) == 'inserted') {
                    //取號
                    $("#dataFormMasterSalesNO").val(GetSalesNO());
                    $('#dataFormMasterSalesTypeID').combobox('disable');
                    //設定使用者組織編號
                    GetUserOrgNOs();

                } else if (getEditMode($("#dataFormMaster")) == 'updated') {//公司別，客戶有值 才須過濾
                    
                    //過濾客戶，雇主
                    var insGroupID = $("#dataFormMasterInsGroupID").combobox('getValue');
                    $("#dataFormMasterCustomerID").refval('setWhere', "CustomerID IN (Select Distinct CustomerID From CustomerSaleType A,SalesType B Where A.SalesTypeID = B.SalesTypeID AND B.InsGroupID = " + "'" + insGroupID + "'" + ")");
                    $("#dataFormMasterEmployer").refval('setWhere', "CustomerID IN (Select Distinct CustomerID From CustomerSaleType A,SalesType B Where A.SalesTypeID = B.SalesTypeID AND B.InsGroupID = " + "'" + insGroupID + "'" + ")");
                    //過濾銷貨類別
                    var CustomerID=$("#dataFormMasterCustomerID").refval('getValue');
                    $("#dataFormMasterSalesTypeID").combobox('setWhere', "InsGroupID='" + InsGroupID + "' and CustomerID='" + CustomerID + "' and SalesID='" + UserID + "'");
                    $("#dataFormMasterSalesTypeID").combobox('enable');
                }
            }

            //欄位隱藏顯示控制
            if (param != "Account") {
                $("#dataFormMasterCreateInvoiceType").closest('td').prev('td').hide();
                $("#dataFormMasterCreateInvoiceType").closest('td').hide();
                $("#dataFormMasterHandWriteInvoiceNO").closest('td').prev('td').hide();
                $("#dataFormMasterHandWriteInvoiceNO").closest('td').hide();
                $("#dataFormMasterRemark").closest('td').prev('td').hide();
                $("#dataFormMasterRemark").closest('td').hide();
            } else if (param == "Account") {//會計關卡
                $("#dataFormMasterCreateInvoiceType").closest('td').prev('td').show();
                $("#dataFormMasterCreateInvoiceType").closest('td').show();
                $("#dataFormMasterHandWriteInvoiceNO").closest('td').prev('td').show();
                $("#dataFormMasterHandWriteInvoiceNO").closest('td').show();
                $("#dataFormMasterRemark").closest('td').prev('td').show();
                $("#dataFormMasterRemark").closest('td').show();
            }
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
            }
            );
            return _return;
        }

        //銷貨主檔.客戶代號_OnSelect
        function CustomerOnSelect(rowData) {
            var UserID = getClientInfo("UserID");
            var InsGroupID = $("#dataFormMasterInsGroupID").combobox('getValue');
            $("#dataFormMasterTaxNO").val(rowData.TaxNO);
            $("#dataFormMasterSalesTypeID").combobox('setValue','');
            $("#dataFormMasterSalesTypeID").combobox('setWhere', "");
            //alert("InsGroupID='" + InsGroupID + "' and CustomerID='" + rowData.CustomerID + "' and SalesID='" + UserID + "'");
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

        function dataFormMasterOnApply() {

            //三聯發票，統編必填
            var InvoiceType = $("#dataFormMasterQInvoiceType").combobox('getValue');
            var TaxNO = $("#dataFormMasterTaxNO").val();
            if (InvoiceType == '98' && TaxNO == '') {
                alert('注意!!當單據是三聯式發票時,統一編號不可空白..');
                return false;
            }

            //客戶為個人，雇主必填
            var customerTypeID = $.trim($("#dataFormMasterCustomerTypeID").val());
            var employer = $.trim($("#dataFormMasterEmployer").refval('getValue'));
            if (customerTypeID == '2' && employer == '') {//客戶為個人
                alert('「雇主」必填');
                return false;
            }

            //備註(發票)，必填
            var remark = $.trim($("#dataFormMasterRemark").val());
            if (remark == '') {
                alert('此銷貨的客戶名稱為空白，請至客戶資料管理填寫客戶名稱(客戶簡稱旁有修改連結)');
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

            //單據類別選發票(二聯,三聯)，發票欲開年月為必填
            var wantedInvoiceNO = $.trim($("#dataFormMasterWantedInvoiceYM").val());
            if (InvoiceType == '98' || InvoiceType == '99') {
                if (wantedInvoiceNO == '') {
                    alert('您選的單據類別為發票，「發票欲開年月」為必填');
                    return false;
                } else if (wantedInvoiceNO != '') {
                    //格式檢查
                    var regex = /[0-9]{4}[/][0-9]{2}/;
                    if (!regex.test(wantedInvoiceNO)) {
                        alert('「發票欲開年月」格式須為yyyy/MM');
                        return false;
                    }
                }
            } else if (InvoiceType == '97') {
                if (wantedInvoiceNO != '') {
                    $("#dataFormMasterWantedInvoiceYM").val('');//清除發票欲開年月
                }
            }


            //銷貨明細
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

            //2.會計關卡檢查
            var param = Request.getQueryStringByName("P1");
            if (param == '') { param = Request.getQueryStringByName2("P1"); }
            if (param == "Account") {

                //開發票類型必填
                var createInvoiceType = $("#dataFormMasterCreateInvoiceType").combobox('getValue');
                if (createInvoiceType == "") {//沒選
                    alert("「開發票類型」必填");
                    return false;
                }

                if (InvoiceType == '97' && createInvoiceType != '1') {//單據類別選收據
                    alert("單據類別選收據，只能選電開");
                    $("#dataFormMasterCreateInvoiceType").combobox('setValue', '1');
                    return false;
                } else if (InvoiceType == '97' || InvoiceType == '98') {//單據類別選發票(可以手開，可以電開)
                    //選手開，必填填發票號碼
                    var handWriteInvoiceNO = $("#dataFormMasterHandWriteInvoiceNO").val();
                    if (createInvoiceType == '2') {//選擇手開
                        if (handWriteInvoiceNO == "") {//沒發票號碼
                            alert("「手開發票號碼」必填");
                            return false;
                        } else {//有發票號碼
                            if (handWriteInvoiceNO.length != 10) {
                                alert("「手開發票號碼」須為10碼");
                                return false;
                            }
                        }
                    }

                    //如發票欲開年月 < 目前年月，必填手開
                    var WantedInvoiceYM = $("#dataFormMasterWantedInvoiceYM").val();
                    var d = new Date(WantedInvoiceYM);
                    var today = new Date();
                    var mm = today.getMonth() + 1;
                    var yyyy = today.getFullYear();
                    var d1 = new Date(yyyy + '/' + mm);
                    if (d < d1 && createInvoiceType != '2') {//發票欲開年月 < 目前年月 且 不選手開
                        alert("「開發票類型」請選擇手開，因「發票欲開年月」小於目前年月");
                        return false;
                    }
                }
            }


            return true;
        }
        
        //銷貨主檔.銷貨類別_OnSelect
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
            var SalesTypeID=$("#dataFormMasterSalesTypeID").combobox('getValue');
            var InvoiceType = $("#dataFormMasterQInvoiceType").combobox('getValue');
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
                        } else {//dll有錯
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
            if (CustomerID != '' && InvoiceType != '') {

                //設定發票欲開年月
                if (InvoiceType == '98' || InvoiceType == '99') {
                    $('#dataFormMasterWantedInvoiceYM').val(WantedInvoiceYM_DefaultMethod());
                } else {
                    $('#dataFormMasterWantedInvoiceYM').val('');
                }

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
                $("#dataFormMasterMailSend").attr('disabled', true);
            } else {
                $("#dataFormMasterMailSend").attr('disabled', false);
            }
        }
        //銷貨明細_合計
        function dataGridDetailAmount_OnTotal(footerRow) {
            $("#dataFormMasterSalesAmount").val(footerRow.Amount)
        }
        //銷貨明細Form_OnLoadSuccess
        function dataFormDetail_OnLoadSuccess() {
            
            if (getEditMode($("#dataFormDetail")) == 'inserted') {
                var salesTypeID = $.trim($("#dataFormMasterSalesTypeID").combobox('getValue'));
                if (salesTypeID != '') {
                    //主檔銷貨類別帶入
                    $("#dataFormDetailSalesTypeID").combobox('setValue', salesTypeID);
                    $("#dataFormDetailSalesTypeName").combobox('setWhere', "SalesTypeID='" + salesTypeID + "'");
                    setTimeout(
                    function () { $("#dataFormDetailSalesTypeName").combobox('setValue', '') }, 800);
                } else {
                    alert('主檔請先選取「銷貨類別」');
                    closeForm("#JQDialog2");
                    return false;
                }
            } else if (getEditMode($("#dataFormDetail")) == 'updated') {
                var salesTypeID = $.trim($("#dataFormDetailSalesTypeID").combobox('getValue'));
                $("#dataFormDetailSalesTypeName").combobox('setWhere', "SalesTypeID='" + salesTypeID + "'");
            }
        }

        //銷貨主檔.公司別(for Flow)
        function dataFormMasterInsGroupID_OnSelect(rowData) {
            //依公司別篩選客戶
            $("#dataFormMasterCustomerID").refval('setValue', '');
			$("#dataFormMasterCustomerID").refval('setWhere', "CustomerID IN (Select Distinct CustomerID From CustomerSaleType A,SalesType B Where A.SalesTypeID = B.SalesTypeID AND B.InsGroupID = " + "'" + rowData.InsGroupID + "'" + ")");

            //篩選
			$("#dataFormMasterEmployer").refval('setWhere', "CustomerID IN (Select Distinct CustomerID From CustomerSaleType A,SalesType B Where A.SalesTypeID = B.SalesTypeID AND B.InsGroupID = " + "'" + rowData.InsGroupID + "'" + ")");
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
            var salesTypeID = $("#dataFormDetailSalesTypeID").combobox('getValue');
            $("#dataFormDetailSalesTypeName").combobox("setWhere", "SalesTypeID='" + salesTypeID + "' and FeeItemID='" + rowdata.FeeItemID + "'");
            $("#dataFormDetailSalesTypeName").combobox('setValue', rowdata.FeeItemName);
        }
        //發票欲開年月
        function WantedInvoiceYM_DefaultMethod() {
            var today = new Date();
            var mth = today.getMonth() + 1;
            var yr = today.getFullYear();
            var ym = yr + '/' + (mth < 10 ? '0' : '') + mth;
            return ym;
        }
        function dataFormDetail_OnApply() {
            var salesTypeName = $("#dataFormDetailSalesTypeName").combobox('getValue');
            if (salesTypeName == '') {
                alert("「銷貨品名」必填");
                return false;
            }
            return true;
        }
        //取得USER的部門代號
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesInvoices.SalesMaster', //連接的Server端，command
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $("#dataFormMasterApplyOrg_NO").val(rows[0].OrgNO);
                        $("#dataFormMasterOrg_NOParent").val(rows[0].OrgNOParent);
                    }
                }
            }
            );
        }
        function dfm_TaxType_OnSelect(rowdata) {
            $("#dataFormMasterTaxRate").val(rowdata.TaxRate);
        }
        function dataGridDetailOnInsert() {
            var kk = $("#dataGridDetail").datagrid('getRows');
            if (kk.length > 9) {
                alert('注意!!因發票證明聯格式限制,最多10筆明細');
                return false;
            }
        }
    </script>   

</head>
<body>
    
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPSalesApply.SalesMaster" runat="server" AutoApply="True"
                DataMember="SalesMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="30,60,90,120,240,480" PageSize="30" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨單號" Editor="text" FieldName="SalesNO" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶" Editor="text" FieldName="ShortName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="單據種類" Editor="text" FieldName="BillTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷售類別" Editor="text" FieldName="SalesTypeName" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="明細數" Editor="text" FieldName="SalesNum" Width="40" />
                    <JQTools:JQGridColumn Alignment="right" Caption="銷貨金額" Editor="text" FieldName="SalesAmount" MaxLength="0" Width="80" Format="N" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務人員" Editor="text" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="稅別" Editor="infocombobox" EditorOptions="valueField:'TaxTypeID',textField:'TaxTypeName',remoteName:'sERPSalesInvoices.TaxType',tableName:'TaxType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="TaxType" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="稅率" Editor="numberbox" FieldName="TaxRate" Format="" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務員" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Width="80" EditorOptions="" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceType" Editor="text" FieldName="InvoiceType" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="InsGroupID" Editor="numberbox" FieldName="InsGroupID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="稅內含" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsHasTax" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="狀態" Editor="text" EditorOptions="" FieldName="UploadDesc" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="收據發票號碼" Editor="text" FieldName="InvoiceNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="列印發票明細" Editor="text" EditorOptions="" FieldName="IsOutPutDetails" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="銷貨收入申請" Width="880px" DialogLeft="0px" DialogTop="20px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SalesMaster" HorizontalColumnsCount="4" RemoteName="sERPSalesApply.SalesMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormMasterOnLoadSuccess" OnApply="dataFormMasterOnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="InsGroupID" Format="" Width="120" Visible="True" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sERPSalesInvoices.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:dataFormMasterInsGroupID_OnSelect,panelHeight:200" />
						<JQTools:JQFormColumn Alignment="left" Caption="客戶簡稱" Editor="inforefval" EditorOptions="title:'客戶簡稱',panelWidth:690,panelHeight:400,remoteName:'sERPSalesInvoices.Customer',tableName:'Customer',columns:[{field:'TaxNO',title:'統編',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'TelNO',title:'電話',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ShortName',title:'簡稱',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustomerName',title:'名稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'WebHostName',title:'來源',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustomerID',title:'代號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustomerID',textField:'ShortName',valueFieldCaption:'客戶ID',textFieldCaption:'客戶簡稱',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,onSelect:CustomerOnSelect,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CustomerID" Format="" MaxLength="0" ReadOnly="False" Visible="True" Width="120" />
						<JQTools:JQFormColumn Alignment="left" Caption="銷貨單號" Editor="text" FieldName="SalesNO" Format="" maxlength="0" ReadOnly="True" Visible="True" Width="120" NewRow="False" PlaceHolder="" />
						<JQTools:JQFormColumn Alignment="right" Caption="銷貨金額" Editor="text" FieldName="SalesAmount" ReadOnly="True" Width="120" Format="N" MaxLength="0" Visible="True" />																																							
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Width="120" ReadOnly="False" Format="" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" maxlength="0" ReadOnly="False" Visible="True" Width="120" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesInvoices.CustomerSaleTypeForApply',tableName:'CustomerSaleTypeForApply',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:SalesTypeID_OnSelect,panelHeight:200" Format="" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單據類別" Editor="infocombobox" FieldName="QInvoiceType" Width="120" Visible="True" EditorOptions="valueField:'INVOICETYPEID',textField:'INVOICETYPENAME',remoteName:'sERPSalesInvoices.QInvoiceType',tableName:'QInvoiceType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:QInvoiceType_OnSelect,panelHeight:200" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNO" Width="120" Visible="True" ReadOnly="True" />
						<JQTools:JQFormColumn Alignment="left" Caption="業務員" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalesInvoices.SalesPerson',tableName:'SalesPerson',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收款結帳日" Editor="text" FieldName="BalanceDate" MaxLength="0" ReadOnly="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款天數" Editor="text" FieldName="DebtorDays" Visible="True" Width="120" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主" Editor="inforefval" FieldName="Employer" maxlength="0" Visible="True" Width="120" ReadOnly="False" EditorOptions="title:'雇主',panelWidth:350,panelHeight:400,remoteName:'sERPSalesInvoices.Employer',tableName:'Employer',columns:[],columnMatches:[],whereItems:[],valueField:'CUSTOMERID',textField:'SHORTNAME',valueFieldCaption:'客戶ID',textFieldCaption:'客戶簡稱',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="已含稅" Editor="checkbox" FieldName="IsHasTax" maxlength="0" Visible="False" Width="30" EditorOptions="on:1,off:0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="稅別" Editor="infocombobox" FieldName="TaxType" Format="" Visible="True" Width="120" maxlength="0" Span="1" EditorOptions="valueField:'TaxTypeID',textField:'TaxTypeName',remoteName:'sERPSalesInvoices.TaxType',tableName:'TaxType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:dfm_TaxType_OnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="稅率" Editor="text" FieldName="TaxRate" MaxLength="0" ReadOnly="False" Span="1" Width="120" Format="" Visible="True" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Mail傳送" Editor="checkbox" FieldName="MailSend" Width="30" Visible="True" maxlength="0" ReadOnly="False" NewRow="True" RowSpan="1" Span="1" EditorOptions="on:1,off:0" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="郵件信箱" Editor="text" FieldName="EmailAddress" Width="350" Visible="True" maxlength="0" ReadOnly="False" Span="3" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註(發票)" Editor="textarea" EditorOptions="height:18" FieldName="Remark" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="4" Visible="True" Width="700" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註(內部)" Editor="textarea" FieldName="RemarkForInner" Width="700" Visible="True" EditorOptions="height:60" maxlength="0" NewRow="True" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨單備註" Editor="text" FieldName="SaleNotes" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="510" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票種類" Editor="infocombobox" FieldName="InvoiceType" Format="" ReadOnly="False" Visible="False" Width="130" EditorOptions="valueField:'INVOICETYPEID',textField:'INVOICETYPENAME',remoteName:'sERPSalesInvoices.InvoiceType',tableName:'InvoiceType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="DonateMarkID" Editor="numberbox" FieldName="DonateMarkID" Format="" Width="180" Visible="False" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="NPOBAN" Editor="text" FieldName="NPOBAN" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CarrierType" Editor="text" FieldName="CarrierType" Format="" Width="180" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CarrierID1" Editor="text" FieldName="CarrierID1" Format="" Width="180" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CarrierID2" Editor="text" FieldName="CarrierID2" Format="" Width="180" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayWayID" Editor="text" FieldName="PayWayID" Visible="False" Width="180" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票號碼" Editor="text" FieldName="InvoiceNO" Width="80" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="180" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" maxlength="0" Width="180" Visible="False" ReadOnly="False" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesNOTemp" Editor="text" FieldName="SalesNOTemp" maxlength="0" ReadOnly="False" Visible="False" Width="180" Format="" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="網站代碼" Editor="text" FieldName="APIWebCode" maxlength="0" ReadOnly="False" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="網站密碼" Editor="text" FieldName="APIPassword" MaxLength="0" ReadOnly="False" Visible="False" Width="80" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨主類別" Editor="text" EditorOptions="valueField:'SalesKindID',textField:'SalesKindName',remoteName:'sERPSalesInvoices.SalesKind',tableName:'SalesKind',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesKindID" MaxLength="0" ReadOnly="False" Visible="False" Width="130" />
						<JQTools:JQFormColumn Alignment="left" Caption="UploadCode" Editor="text" FieldName="UploadCode" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CustomerTypeID" Editor="text" FieldName="CustomerTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />																																																 
                        <JQTools:JQFormColumn Alignment="left" Caption="TempTaxNO" Editor="text" FieldName="TempTaxNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="明細全顯示" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsOutPutDetails" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票欲開年月" Editor="text" FieldName="WantedInvoiceYM" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開發票類型" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'電開',selected:'true'},{value:'2',text:'手開',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="CreateInvoiceType" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手開發票號碼" Editor="text" FieldName="HandWriteInvoiceNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="SalesDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sERPSalesApply.SalesMaster" Title="明細資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計" UpdateCommandVisible="True" ViewCommandVisible="True" OnInsert="dataGridDetailOnInsert" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="SalesNO" Editor="text" FieldName="SalesNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="130" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesInvoices.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
						<JQTools:JQGridColumn Alignment="left" Caption="費用項目" Editor="infocombobox" EditorOptions="valueField:'FeeItemID',textField:'FeeItemName',remoteName:'sERPSalesInvoices.FeeItem',tableName:'FeeItem',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="FeeItemID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>																																																																																																														  		   
                        <JQTools:JQGridColumn Alignment="left" Caption="銷貨品名" Editor="text" FieldName="SalesTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="單位" Editor="text" FieldName="Unit" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="數量" Editor="text" FieldName="Quantity" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="未稅價" Editor="text" FieldName="UnitPrice" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" Format="N">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="銷貨金額" Editor="text" FieldName="Amount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" Format="N" OnTotal="dataGridDetailAmount_OnTotal" Total="sum">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="SalesNO" ParentFieldName="SalesNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Width="900px" DialogTop="150px" Title="銷貨明細">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="SalesDetails" HorizontalColumnsCount="4" RemoteName="sERPSalesApply.SalesMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormDetail_OnLoadSuccess" OnApply="dataFormDetail_OnApply" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="SalesNO" Editor="text" FieldName="SalesNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="center" Caption="項次" Editor="text" FieldName="ItemNO" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="30" />
                            <JQTools:JQFormColumn Alignment="left" Caption="銷售項目" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesInvoices.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="費用項目" Editor="text" EditorOptions="valueField:'FeeItemID',textField:'FeeItemName',remoteName:'sERPSalesInvoices.FeeItem',tableName:'FeeItem',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:FeeItemID_OnSelect,panelHeight:200" FieldName="FeeItemID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="130" />
                            <JQTools:JQFormColumn Alignment="left" Caption="銷貨品名" Editor="infocombobox" FieldName="SalesTypeName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="300" EditorOptions="valueField:'SalesItemName',textField:'SalesItemName',remoteName:'sERPSalesInvoices.SalesItem',tableName:'SalesItem',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="數量" Editor="text" FieldName="Quantity" MaxLength="0" NewRow="True" OnBlur="dataFormDetail_Quantity_OnBlur" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="單位" Editor="text" FieldName="Unit" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" />
                            <JQTools:JQFormColumn Alignment="right" Caption="未稅價" Editor="text" FieldName="UnitPrice" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" OnBlur="dataFormDetail_Quantity_OnBlur" />
                            <JQTools:JQFormColumn Alignment="left" Caption="項目金額" Editor="text" FieldName="Amount" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="160" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="SalesNO" ParentFieldName="SalesNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="Quantity" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Amount" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="UnitPrice" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="ZZ" FieldName="FeeItemID" RemoteMethod="False" />
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

                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="SalesNO" RemoteMethod="True" DefaultMethod="" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="TaxType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0.05" FieldName="TaxRate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="07" FieldName="InvoiceType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsOutPutDetails" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="00" FieldName="UploadCode" RemoteMethod="True" />
						<JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2" FieldName="DonateMarkID" RemoteMethod="False" />																									  
						<JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="MailSend" RemoteMethod="True" />																											
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="WantedInvoiceYM" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="CreateInvoiceType" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustomerID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesDate" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="QInvoiceType" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="InsGroupID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesTypeID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DebtorDays" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
             
    </form>
</body>
</html>
