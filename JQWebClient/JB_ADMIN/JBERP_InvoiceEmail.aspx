<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_InvoiceEmail.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
    var UserID = getClientInfo("UserID");
    $(document).ready(function () {
            //將Focus 欄位背景顏色改為黃色
        $(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "yellow");
            });
            $("input, select, textarea").blur(function () {
                $(this).css("background-color","white");
            });
        });
        setTimeout(function () {
           queryGrid();
        }, 500);
    });
    var DGVMailCount_FormatScript = function (value, row, index) {
        if (value >= 1) {
            var fieldName = this.field;
            return $('<a>', { href: 'javascript: void(0)', title: ' ', onclick: "DGVMailCount_CommandTrigger.call(this,'')" }).linkbutton({ iconCls: '', plain: true, text: value })[0].outerHTML;
        }
        return value;
    }
    var DGVMailCount_CommandTrigger = function (command) {
        
        var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
        var rowData = $("#dataGridView").datagrid('selectRow', rowIndex).datagrid('getSelected');
        //alert(rowData.InvoiceNO);
        GetGridDataEmailLogs(rowData.InvoiceNO);
        return true;
    }
    //取得eMailLogs明細
    function GetGridDataEmailLogs(InvoiceNO) {
        $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sERPInvoiceEmail.InvoiceDetails',
            data: "mode=method&method=" + "GetGridDataEmailLogs" + "&parameters=" + InvoiceNO,
            cache: false,
            async: false,
            success: function (data) {
                var rows = $.parseJSON(data);
                if (rows.length == 0) {
                    $('#JQDataGridEmailLogs').datagrid('loadData', []);//清空Grid資料
                } else {
                    if (rows.length > 0) {
                        $('#JQDataGridEmailLogs').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                        openForm('#JQDialog3', {}, "", 'dialog');
                    }
                }
            }
        }
      );
    }
    function queryGrid() {
        var StrSalesTypeID = GetUserSalesTypesStr(UserID);
        if (StrSalesTypeID == "") {
            alert('注意!!你無任何銷貨權限,請洽會計室或管理室');
            return false;
        }
        var Str = $.trim($("#STR_Query").val());
        var InsGroupID = $.trim($("#InsGroupID_Query").combobox('getValue'));
        var InvoiceYM = $.trim($("#InvoiceYM_Query").combobox('getValue'));
        var IsSend = $.trim($("#IsSend_Query").combobox('getValue'));
        var SDate = $.trim($("#SDate_Query").datebox('getValue'));
        var EDate = $.trim($("#EDate_Query").datebox('getValue'));
        var whereArr = [];
        if (Str != "") {
            whereArr.push("(InvoiceNO Like '%" + Str + "%'" + " OR " + "TelNO Like '%" + Str + "%'" + " OR " + "SalesNO Like '%" + Str + "%'" + " OR " + "InsGroupShortName Like '%" + Str + "%'" + " OR " + "SalesTypeName Like '%" + Str + "%'"
                + " OR " + "EmailAddress Like '%" + Str + "%'" + " OR " + "SalesName Like '%" + Str + "%'" + " OR " + "CustomerName Like '%" + Str + "%'" + " OR " + "AccountClerk Like '%" + Str + "%')");
        }
        if (InsGroupID != "") {
            whereArr.push("InsGroupID = '" + InsGroupID + "'");
        }
        if (InvoiceYM != "") {
            whereArr.push("InvoiceYM = '" + InvoiceYM + "'");
        }
        if (StrSalesTypeID != "") {
            whereArr.push("SalesTypeID IN (" + StrSalesTypeID + ")");
        }
        if (SDate != "" && EDate != "") {
            whereArr.push("(LastSendDate >='"+ SDate + "' and LastSendDate <='"+EDate+"')");
        }
        if (IsSend != 2) {
            if (IsSend == 0)
                whereArr.push("mailcount = 0");
            else
                whereArr.push("mailcount > 0");
        }
        if (whereArr.length != 0) {
            $("#dataGridView").datagrid('setWhere', whereArr.join(" and "));
        } else {
            $("#dataGridView").datagrid('setWhere', '1=1');
        }
    }
    function GetUserSalesTypesStr(UserID) {
        var UserSalesTypesStr = "";
        $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Report_OverdueUncollectedInvoiceDetails.InvoiceDetails',
            data: "mode=method&method=" + "GetUserSalesTypesStr" + "&parameters=" + UserID,
            cache: false,
            async: false,
            success: function (data) {
                var rows = $.parseJSON(data);
                if (rows.length > 0) {
                    UserSalesTypesStr = rows[0].UserSalesTypesStr;
                }
            }
        }
       );
        return UserSalesTypesStr;
    }
    function dataGridViewOnLoadSucess() {
        if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
            $(this).datagrid({
                singleSelect: true,
                selectOnCheck: false,
                checkOnSelect: false
            });
        }
    }
    //發送Mail
    function AddPDFMail() {
        //var where = $("#dataGridView").datagrid('getWhere');
        var rows = $('#dataGridView').datagrid("getChecked");
        if (rows.length <= 0) {
            alert('注意!!未選取發票記錄,請選取!!');
            return false;
        }
        for (var i = 0; i <= rows.length - 1; i++) {
            var url = "../JB_ADMIN/REPORT/ERP/JBERP_InvoiceEmail_RV.aspx?InvoiceNO=" + rows[i].InvoiceNO + "&UserID=" + UserID + "&ToMail=" + rows[i].EmailAddress + "&ToName=" + rows[i].AccountClerk + "&DCou=" + rows[i].DCou;
            var height = $(window).height() - 20;
            var height2 = $(window).height() - 90;
            var width = $(window).width() - 20;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                width: width,
                title: "Report",
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="98%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('close');
        }
        //$.messager.progress('close');
        //alert('...Email發送成功...');
        setTimeout(function () {
            queryGrid();
        }, 500);
    }
    function myPrint() {
        var WhereString = "";
        //var rows = $('#dataGridView').datagrid("getSelected");
        var rows = $('#dataGridView').datagrid("getChecked");
        if (rows.length <= 0) {
            alert('注意!!未選取發票記錄,請選取!!');
            return false;
        }
        var InvStr = "";
        for (var i = 0; i <= rows.length - 1; i++) {
            if (i == 0)
                InvStr = InvStr + "'" + rows[i].InvoiceNO + "'";
            else
                InvStr = InvStr + "," + "'" + rows[i].InvoiceNO + "'";
        }
        var WhereString = InvStr.split(',');
        WhereString = "InvoiceNO IN " + "(" + WhereString + ")";
        var rdlcstr = "~/JB_ADMIN/rInvoiceProofSheet1.rdlc";
        exportReport("#dataGridView", "sERPInvoiceEmail.InvoiceDetails", "InvoiceDetails", rdlcstr, WhereString);
    }
    function AddMailOthers() {
        var WhereString = "";
        var rows = $('#dataGridView').datagrid("getChecked");
        if (rows.length <= 0) {
            alert('注意!!未選取發票記錄,請選取!!');
            return false;
        }
        openForm('#JQDialog2', $('#dataGridView').datagrid('selectRow', 0).datagrid('getSelected'), 'inserted', 'dialog');
    }
    //提交Email與帳號
    function JQDialog2OnSubmited() {
        var EmailAddress = $("#JQDataForm1EmailAddress").val();
        if (EmailAddress == "") {
            alert('注意!!收件者Email不可空白!!');
            $("#JQDataForm1EmailAddress").focus();
            return false;
        }
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        var IsEmail = regex.test(EmailAddress);
        if (!IsEmail) {
            alert('注意!!Email格式有誤,請修正');
            $("#JQDataForm1EmailAddress").focus();
            return false;
        }
        var AccountClerk = $("#JQDataForm1AccountClerk").val();
        var rows = $('#dataGridView').datagrid("getChecked");
        for (var i = 0; i <= rows.length - 1; i++) {
            var url = "../JB_ADMIN/REPORT/ERP/JBERP_InvoiceEmail_RV.aspx?InvoiceNO=" + rows[i].InvoiceNO + "&UserID=" + UserID + "&ToMail=" + EmailAddress + "&ToName=" + AccountClerk + "&DCou=" + rows[i].DCou;
            var height = $(window).height() - 20;
            var height2 = $(window).height() - 90;
            var width = $(window).width() - 20;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                width: width,
                title: "Report",
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="98%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('close');
        }

        //$.messager.progress('close');
        //alert('...Email發送成功...');
        //queryGrid();
        setTimeout(function () {
            queryGrid();
        }, 10000);
        
        return true;
    }
    function dataGridViewOnUpdate() {

    }
 
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPInvoiceEmail.InvoiceDetails" runat="server" AutoApply="True"
                DataMember="InvoiceDetails" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="電子發票寄送" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" NotInitGrid="False" PageList="15,30,45,90" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" EnableTheming="True" OnUpdate="dataGridViewOnUpdate" MultiSelectGridID="" OnLoadSuccess="dataGridViewOnLoadSucess" Width="1120px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="text" FieldName="InsGroupShortName" Format="" MaxLength="0" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="發票號碼" Editor="text" FieldName="InvoiceNO" Format="" MaxLength="0" Width="77" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨客戶" Editor="text" FieldName="CustomerName" Format="" MaxLength="0" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨名稱" Editor="text" FieldName="SalesTypeName" Format="" MaxLength="0" Width="210" />
                    <JQTools:JQGridColumn Alignment="center" Caption="寄送次數" Editor="text" FieldName="MailCount" Format="" Width="55" FormatScript="DGVMailCount_FormatScript" IsNvarChar="False" MaxLength="0" />
                    <JQTools:JQGridColumn Alignment="left" Caption="收件者" Editor="text" FieldName="AccountClerk" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="收件者信箱" Editor="text" FieldName="EmailAddress" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="130">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="發票日期" Editor="datebox" FieldName="InvoiceDate" Format="yyyy/mm/dd" Width="65" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="發票金額" Editor="numberbox" FieldName="SalesAmount" Format="N0" Width="65" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="發票稅額" Editor="numberbox" FieldName="SalesTax" Format="N0" Width="65" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="發票總額" Editor="numberbox" FieldName="SalesTotal" Format="N0" Width="65" />
                    <JQTools:JQGridColumn Alignment="right" Caption="稅率" Editor="numberbox" FieldName="TaxRate" Format="N3" Width="45" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務人員" Editor="text" FieldName="SalesName" Format="" MaxLength="0" Width="70" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesNO" Editor="text" FieldName="SalesNO" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="InsGroupID" Editor="numberbox" FieldName="InsGroupID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="yyyy/MM/dd" Width="75" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustNO" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsSend" Editor="text" FieldName="IsSend" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="TelNO" Editor="text" FieldName="TelNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="DCou" Editor="text" FieldName="DCou" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-view" ItemType="easyui-linkbutton"  OnClick="myPrint" Text="預覽" />
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"   OnClick="AddPDFMail" Text="寄送電子發票郵件-PDF" />
                    <JQTools:JQToolItem Icon="icon-redo" ItemType="easyui-linkbutton"  OnClick="AddMailOthers" Text="改寄其他mail" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="搜尋關鍵字" Condition="%%" DataType="string" Editor="text" FieldName="STR" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sERPInvoiceEmail.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InsGroupID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="發票年月" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'InvoiceYM',textField:'InvoiceYM',remoteName:'sERPInvoiceEmail.InvoiceYM',tableName:'InvoiceYM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InvoiceYM" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="75" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="狀態" Condition="=" DataType="string" DefaultValue="0" Editor="infocombobox" EditorOptions="items:[{value:'0',text:'未寄送',selected:'false'},{value:'1',text:'已寄送',selected:'false'},{value:'2',text:'全部',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="IsSend" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="70" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起迄發送日" Condition="=" DataType="datetime" Editor="datebox" FieldName="SDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="-" Condition="=" DataType="datetime" Editor="datebox" FieldName="EDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="電子發票寄送資訊" DialogLeft="100px" DialogTop="110px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="InvoiceDetails" HorizontalColumnsCount="2" RemoteName="sERPInvoiceEmail.InvoiceDetails" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="發票號碼" Editor="text" FieldName="InvoiceNO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨單號" Editor="text" FieldName="SalesNO" Format="" Width="180" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="InsGroupID" Editor="numberbox" FieldName="InsGroupID" Format="" Width="180" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨公司" Editor="text" FieldName="InsGroupName" Format="" maxlength="0" Width="200" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨名稱" Editor="text" FieldName="SalesTypeName" Format="" maxlength="0" Width="200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="" maxlength="0" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票日期" Editor="datebox" FieldName="InvoiceDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="寄送郵件" Editor="text" FieldName="EmailAddress" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收件者" Editor="text" FieldName="AccountClerk" maxlength="0" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票金額" Editor="numberbox" FieldName="SalesAmount" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票稅額" Editor="numberbox" FieldName="SalesTax" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票總額" Editor="numberbox" FieldName="SalesTotal" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="稅率" Editor="text" FieldName="TaxRate" Format="" Width="180" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務人員" Editor="text" FieldName="SalesName" Format="" maxlength="0" Width="180" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨單備註" Editor="text" FieldName="SaleNotes" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="寄送次數" Editor="numberbox" FieldName="MailCount" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最近寄送日" Editor="text" FieldName="LastSendDate" Format="yyyy/mm/dd HH:MM:SS" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CustNO" Editor="text" FieldName="CustNO" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortName" Editor="text" FieldName="ShortName" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialog3" runat="server" DialogLeft="300px" DialogTop="60px" Title="發送明細列表" Width="455px" Closed="True" ShowSubmitDiv="False" DialogCenter="False" EditMode="Dialog" EnableTheming="True" ScrollBars="Vertical">
                 <JQTools:JQDataGrid ID="JQDataGridEmailLogs" runat="server" AlwaysClose="True" DataMember="EmailLogsList" RemoteName="sERPInvoiceEmail.EmailLogsList" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="120px" QueryMode="Window" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdate="" Height="350px" Width="380px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                     <Columns>
                         <JQTools:JQGridColumn Alignment="left" Caption="收信Email帳號" Editor="text" FieldName="ToMail" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="收信人" Editor="text" FieldName="ToName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="寄信者" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="寄出日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="113">
                         </JQTools:JQGridColumn>
                     </Columns>
                </JQTools:JQDataGrid>
         </JQTools:JQDialog>
        <JQTools:JQDialog ID="JQDialog2" runat="server" DialogLeft="300px" DialogTop="60px" Title="收件者Email帳號" Width="455px" Closed="True" ShowSubmitDiv="true" DialogCenter="False" EditMode="Dialog" EnableTheming="True" ScrollBars="Vertical" OnSubmited="JQDialog2OnSubmited">
              <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="InvoiceDetails" HorizontalColumnsCount="2" RemoteName="sERPInvoiceEmail.InvoiceDetails" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="收件者Email" Editor="text" FieldName="EmailAddress" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收件者" Editor="text" FieldName="AccountClerk" maxlength="0" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
               <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                   <Columns>
                       <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="''" FieldName="EmailAddress" RemoteMethod="True" />
                       <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="''" FieldName="AccountClerk" RemoteMethod="True" />
                   </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                </JQTools:JQValidate>
         </JQTools:JQDialog>
       
    </form>
</body>
</html>
