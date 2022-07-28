<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Normal_CheckDetailsAction.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(function () {
            $('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });
            $("#ActionCode_Query").combobox('setValue', '');
        });
        function dataGridView_OnLoadSuccess() {
            $("#dataGridView").datagrid('clearSelections');
        }
        //openForm"託收"
        function ToolItemTrust_OnClick() {
            //if ($("#ActionCode_Query").combobox('getText') != '託收') {
            //    alert("「動作別」請先選取'託收'並「查詢」，再選取票據");
            //} else if ($("#ActionCode_Query").combobox('getText') == '託收') {
                var rows = $('#dataGridView').datagrid("getChecked");
                if (rows.length != 0)
                    openForm('#dialogTrust', '', "update", 'dialog');//$('#dataGridView').datagrid('getChecked')
                else { alert("您沒選取任一票據"); return false; }
            //}
        }
        function dataFormTrust_OnLoadSuccess() {
            var rows = $('#dataGridView').datagrid("getChecked");
            var WarrantNOs=[], ItemNOs=[];
            for (var i = 0; i < rows.length; i++) {
                WarrantNOs.push(rows[i].WarrantNO);
                ItemNOs.push(rows[i].ItemNO);
                if (rows[i].ActionCode != '0') { closeForm("#dialogTrust"); alert("票據"+rows[i].CheckNO + "的動作別不是'可託收'"); return false; }
            }
            $('#dataFormTrustWarrantNO').val(WarrantNOs.join('*'));
            $('#dataFormTrustItemNO').val(ItemNOs.join('*'));
            $('#dataFormTrustActionCode').val('0');
        }
        function submitFormTrust() {
            var WarrantNOs = $("#dataFormTrustWarrantNO").val().trim();
            var ItemNOs = $("#dataFormTrustItemNO").val().trim();
            var TrustDate = $("#dataFormTrustTrustDate").datebox('getValue');
            var TrustAccountID = $("#dataFormTrustTrustAccountID").combobox('getValue');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Normal_CheckDetailsAction.CheckDetails', //連接的Server端，command
                data: "mode=method&method=" + "UpdateCheckDetails_Trust" + "&parameters=" + WarrantNOs + "," + ItemNOs + "," + TrustDate + "," + TrustAccountID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "False") {
                        closeForm("#dialogTrust");
                        alert("已修改" + data + "筆")
                        $("#dataGridView").datagrid("reload");
                    } else {
                        alert(data);
                    }
                }
                , error: function () {
                }
            });
        }

        //openForm"兌現"
        function ToolItemCash_OnClick() {
            var rows = $('#dataGridView').datagrid("getChecked");
            if (rows.length != 0)
                openForm('#dialogCash', '', "update", 'dialog');
            else { alert("您沒選取任一票據"); return false; }
        }
        function dataFormCash_OnLoadSuccess() {
            var rows = $('#dataGridView').datagrid("getChecked");
            var WarrantNOs = [], ItemNOs = [];
            for (var i = 0; i < rows.length; i++) {
                WarrantNOs.push(rows[i].WarrantNO);
                ItemNOs.push(rows[i].ItemNO);
                if (rows[i].ActionCode != '1') { closeForm("#dialogCash"); alert("票據" + rows[i].CheckNO + "的動作別不是'可兌現'"); return false; }
            }
            $('#dataFormCashWarrantNO').val(WarrantNOs.join('*'));
            $('#dataFormCashItemNO').val(ItemNOs.join('*'));
            $('#dataFormCashActionCode').val('1');
        }
        function submitFormCash() {
            var WarrantNOs = $("#dataFormCashWarrantNO").val().trim();
            var ItemNOs = $("#dataFormCashItemNO").val().trim();
            var CashDate = $("#dataFormCashCashDate").datebox('getValue');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Normal_CheckDetailsAction.CheckDetails', //連接的Server端，command
                data: "mode=method&method=" + "UpdateCheckDetails_Cash" + "&parameters=" + WarrantNOs + "," + ItemNOs + "," + CashDate,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "False") {
                        closeForm("#dialogCash");
                        alert("已修改" + data + "筆")
                        $("#dataGridView").datagrid("reload");
                    } else {
                        alert(data);
                    }
                }
                , error: function () {
                }
            });
        }


        //openForm"退票"
        function ToolItemReturn_OnClick() {
            var rows = $('#dataGridView').datagrid("getChecked");
            if (rows.length != 0)
                openForm('#dialogReturn', '', "update", 'dialog');
            else { alert("您沒選取任一票據"); return false; }
        }
        function dataFormReturn_OnLoadSuccess() {
            var rows = $('#dataGridView').datagrid("getChecked");
            var WarrantNOs = [], ItemNOs = [];
            for (var i = 0; i < rows.length; i++) {
                WarrantNOs.push(rows[i].WarrantNO);
                ItemNOs.push(rows[i].ItemNO);
                if (rows[i].ActionCode != '2') { closeForm("#dialogReturn"); alert("票據" + rows[i].CheckNO + "的動作別不是'可退票'"); return false; }
            }
            $('#dataFormReturnWarrantNO').val(WarrantNOs.join('*'));
            $('#dataFormReturnItemNO').val(ItemNOs.join('*'));
            $('#dataFormReturnActionCode').val('2');
        }
        function submitFormReturn() {
            var WarrantNOs = $("#dataFormReturnWarrantNO").val().trim();
            var ItemNOs = $("#dataFormReturnItemNO").val().trim();
            var ReturnDate = $("#dataFormReturnReturnDate").datebox('getValue');
            var ReturnNotes = $("#dataFormReturnReturnNotes").val().trim();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Normal_CheckDetailsAction.CheckDetails', //連接的Server端，command
                data: "mode=method&method=" + "UpdateCheckDetails_Return" + "&parameters=" + WarrantNOs + "," + ItemNOs + "," + ReturnDate + "," + ReturnNotes,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "False") {
                        closeForm("#dialogReturn");
                        alert("已修改" + data + "筆")
                        $("#dataGridView").datagrid("reload");
                    } else {
                        alert(data);
                    }
                }
                , error: function () {
                }
            });
        }



        function submitForm(dlgID) {
            if (dlgID=="#dialogTrust") submitFormTrust();
            else if (dlgID == "#dialogCash") submitFormCash();
            else if (dlgID == "#dialogReturn") submitFormReturn();
            else if (dlgID == "#JQDialog1") {
                applyForm("#dataFormMaster");
                closeForm(dlgID)
            }
        }
        

        function clearQuery(dgid) {
            var isQueryAutoColumn = getInfolightOption($(dgid)).queryAutoColumn;
            if (isQueryAutoColumn == true) {
                var pnid = getInfolightOption($(dgid)).queryDialog;
                var queryTr = $('#queryTr_' + $(dgid)[0].id);
                var queryParams = $(dgid).datagrid('options').queryParams;
                var queryWord = new Object();

                var where = '';
                $(":text,select", queryTr).each(function () {
                    var text = $(this);
                    text.val('');
                });
            }

            var pnid = getInfolightOption($(dgid)).queryDialog;
            if (pnid != undefined) {
                var queryParams = $(dgid).datagrid('options').queryParams;
                var queryWord = new Object();

                var where = '';
                //jbjob edit by serlina 增加textarea
                $(":text,select,textarea", pnid).each(function () {
                    var text = $(this);
                    text.val('');
                    var controlClass = $(this).attr('class');
                    if (controlClass != undefined) {
                        if (controlClass.indexOf('easyui-datebox') == 0) {
                            text.datebox('setValue', '');
                        }
                        else if (controlClass.indexOf('easyui-datetimebox') == 0) {
                            text.datetimebox('setValue', '');
                        }
                        else if (controlClass.indexOf('info-combobox') == 0) {
                            text.combobox('setValue', '');
                            text.combobox('clear');
                            text.combobox('setWhere', '');
                        }
                        else if (controlClass.indexOf('info-combogrid') == 0) {
                            text.combogrid('setValue', '');
                            text.combogrid('clear');
                            text.combogrid('setWhere', '');
                        }
                        else if (controlClass.indexOf('combo-text') == 0) {
                            value = '';
                        }
                        else if (controlClass.indexOf('info-refval') == 0) {
                            text.refval('setValue', '');
                        }
                        else if (controlClass.indexOf('info-autocomplete') == 0) {
                            text.combobox('setValue', '');
                        }
                    }
                });
                $(":radio,:checkbox", pnid).each(function () {
                    $(this).prop("checked", false);
                });
            }
        }
        
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERP_Normal_CheckDetailsAction.CheckDetails" runat="server" AutoApply="True"
                DataMember="CheckDetails" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="票據託收兌現退票" UpdateCommandVisible="True" DeleteCommandVisible="False" MultiSelect="True" OnLoadSuccess="dataGridView_OnLoadSuccess" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="WarrantNO" Editor="text" FieldName="WarrantNO" Format="" MaxLength="0" Visible="False" Width="70" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ItemNO" Editor="numberbox" FieldName="ItemNO" Format="" Visible="False" Width="40" />
                    <JQTools:JQGridColumn Alignment="right" Caption="InsGroupID" Editor="text" FieldName="InsGroupID" Format="" Visible="False" Width="120" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="WarrantDate" Editor="datebox" FieldName="WarrantDate" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="支票號碼" Editor="text" FieldName="CheckNO" Format="" MaxLength="0" Visible="true" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="動作別" Editor="text" FieldName="ActionCode" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="30" EditorOptions="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="動作別" Editor="text" FieldName="ActionName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="到期日" Editor="text" FieldName="CheckDueDate" Format="yyyy/mm/dd" Visible="true" Width="60" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="Amount" Format="" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銀行主行" Editor="text" FieldName="BankRootName" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銀行分行" Editor="text" FieldName="BankBranchName" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="交換碼" Editor="text" FieldName="Bourse" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="45">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶" Editor="infocombobox" FieldName="CustomerID" Format="" MaxLength="0" Visible="True" Width="80" EditorOptions="valueField:'CustomerID',textField:'ShortName',remoteName:'sERP_Normal_CheckDetailsAction.Customers',tableName:'Customers',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶支票帳號" Editor="text" FieldName="CheckAccount" Format="" MaxLength="0" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AccountID" Editor="text" FieldName="AccountID" Format="" MaxLength="0" Visible="False" Width="120" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="託收日" Editor="datebox" FieldName="TrustDate" Format="yyyy/mm/dd" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="託收銀行帳戶" Editor="infocombobox" FieldName="TrustAccountID" Format="" MaxLength="0" Visible="true" Width="120" EditorOptions="valueField:'CheckAccountID',textField:'CheckAccountName',remoteName:'sERP_Normal_CheckDetailsAction.CheckAccount',tableName:'CheckAccount',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="兌現日" Editor="datebox" FieldName="CashDate" Format="yyyy/mm/dd" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="退票日" Editor="datebox" FieldName="ReturnDate" Format="yyyy/mm/dd" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="退票原因" Editor="text" FieldName="ReturnNotes" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" Format="yyyy/mm/dd" Visible="true" Width="60" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" Visible="False"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="ToolItemTrust_OnClick" Text="託收" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="ToolItemCash_OnClick" Text="兌現" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="ToolItemReturn_OnClick" Text="退票" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" FieldName="InsGroupID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_Normal_CheckDetailsAction.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="動作別" Condition="=" DataType="string" Editor="infocombobox" FieldName="ActionCode" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="items:[{value:'0',text:'可託收',selected:'false'},{value:'1',text:'可兌現',selected:'false'},{value:'2',text:'可退票',selected:'false'},{value:'3',text:'已退票',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" DefaultValue="" TableName="d" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="到期日起迄" Condition="&gt;=" DataType="string" Editor="datebox" FieldName="CheckDueDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="&lt;=" DataType="string" Editor="datebox" FieldName="CheckDueDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="託收日起迄" Condition="&gt;=" DataType="string" Editor="datebox" FieldName="TrustDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="&lt;=" DataType="string" Editor="datebox" FieldName="TrustDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="=" DataType="string" Editor="infocombobox" FieldName="CustomerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'CustomerID',textField:'ShortName',remoteName:'sERP_Normal_CheckDetailsAction.Customer',tableName:'Customer',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="支票號碼" Condition="=" DataType="string" Editor="text" FieldName="CheckNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="支票銀行帳戶" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CheckAccountID',textField:'CheckAccountName',remoteName:'sERP_Normal_CheckDetailsAction.CheckAccount',tableName:'CheckAccount',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="票據託收兌現退票">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CheckDetails" HorizontalColumnsCount="2" RemoteName="sERP_Normal_CheckDetailsAction.CheckDetails" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="WarrantNO" Editor="text" FieldName="WarrantNO" Format="" maxlength="0" Width="180" Visible="False" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ItemNO" Editor="numberbox" FieldName="ItemNO" Format="" Width="180" Visible="False" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別代號" Editor="infocombobox" FieldName="InsGroupID" Format="" Width="180" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_Normal_CheckDetailsAction.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收款日" Editor="datebox" FieldName="WarrantDate" Format="" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="支票號碼" Editor="text" FieldName="CheckNO" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="到期日" Editor="datebox" FieldName="CheckDueDate" Format="" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="支票金額" Editor="numberbox" FieldName="Amount" Format="" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銀行主行" Editor="text" FieldName="BankRootName" Format="" maxlength="0" Width="180" Visible="False" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銀行分行" Editor="text" FieldName="BankBranchName" Format="" maxlength="0" Width="180" Visible="False" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="交換碼" Editor="text" FieldName="Bourse" Format="" maxlength="0" Width="177" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="BankID" Editor="text" FieldName="BankID" Format="" maxlength="0" Width="180" Visible="False" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶支票帳號" Editor="text" FieldName="CheckAccount" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="支票銀行帳戶" Editor="infocombobox" FieldName="AccountID" Format="" maxlength="0" Width="180" EditorOptions="valueField:'CheckAccountID',textField:'CheckAccountName',remoteName:'sERP_Normal_CheckDetailsAction.CheckAccount',tableName:'CheckAccount',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶" Editor="infocombobox" FieldName="CustomerID" Format="" maxlength="0" Width="180" EditorOptions="valueField:'CustomerID',textField:'ShortName',remoteName:'sERP_Normal_CheckDetailsAction.Customer',tableName:'Customer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="託收日" Editor="datebox" FieldName="TrustDate" Format="" Width="180" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="託收銀行帳戶" Editor="infocombobox" FieldName="TrustAccountID" Format="" maxlength="0" Width="180" EditorOptions="valueField:'CheckAccountID',textField:'CheckAccountName',remoteName:'sERP_Normal_CheckDetailsAction.CheckAccount',tableName:'CheckAccount',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="兌現日" Editor="datebox" FieldName="CashDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="退票日" Editor="datebox" FieldName="ReturnDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="退票備註" Editor="textarea" FieldName="ReturnNotes" Format="" maxlength="0" Width="435" EditorOptions="height:60" NewRow="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" NewRow="True" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" ReadOnly="True" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="dialogReturn" runat="server" BindingObjectID="dataFormReturn" Title="票據退票" Width="600px">
                <JQTools:JQDataForm ID="dataFormReturn" runat="server" DataMember="CheckDetails" HorizontalColumnsCount="2" RemoteName="sERP_Normal_CheckDetailsAction.CheckDetails" OnLoadSuccess="dataFormReturn_OnLoadSuccess" ParentObjectID="" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="WarrantNO" Editor="text" FieldName="WarrantNO" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ItemNO" Editor="numberbox" FieldName="ItemNO" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="TrustDate" Editor="datebox" FieldName="TrustDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="TrustAccountID" Editor="text" FieldName="TrustAccountID" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CashDate" Editor="datebox" FieldName="CashDate" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="退票日" Editor="datebox" FieldName="ReturnDate" Format="" Width="180" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="退票原因" Editor="textarea" FieldName="ReturnNotes" Format="" Width="410" Visible="True" EditorOptions="height:60" NewRow="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="動作別" Editor="text" FieldName="ActionCode" maxlength="0" Width="80" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster2" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster2" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="dialogCash" runat="server" BindingObjectID="dataFormCash" Title="票據兌現">
                <JQTools:JQDataForm ID="dataFormCash" runat="server" DataMember="CheckDetails" HorizontalColumnsCount="2" RemoteName="sERP_Normal_CheckDetailsAction.CheckDetails" OnLoadSuccess="dataFormCash_OnLoadSuccess" ParentObjectID="" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="WarrantNO" Editor="text" FieldName="WarrantNO" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ItemNO" Editor="numberbox" FieldName="ItemNO" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="TrustDate" Editor="datebox" FieldName="TrustDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="TrustAccountID" Editor="text" FieldName="TrustAccountID" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="兌現日" Editor="datebox" FieldName="CashDate" Format="" maxlength="0" Width="180" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ReturnDate" Editor="datebox" FieldName="ReturnDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ReturnNotes" Editor="text" FieldName="ReturnNotes" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="動作別" Editor="text" FieldName="ActionCode" maxlength="0" Width="80" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster1" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster1" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="dialogTrust" runat="server" BindingObjectID="dataFormTrust" Title="票據託收">
                <JQTools:JQDataForm ID="dataFormTrust" runat="server" DataMember="CheckDetails" HorizontalColumnsCount="2" RemoteName="sERP_Normal_CheckDetailsAction.CheckDetails" OnLoadSuccess="dataFormTrust_OnLoadSuccess" ParentObjectID="" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="WarrantNO" Editor="text" FieldName="WarrantNO" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ItemNO" Editor="numberbox" FieldName="ItemNO" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="託收日" Editor="datebox" FieldName="TrustDate" Format="" Width="180" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="託收銀行帳戶" Editor="infocombobox" FieldName="TrustAccountID" Format="" Width="180" Visible="True" EditorOptions="valueField:'CheckAccountID',textField:'CheckAccountName',remoteName:'sERP_Normal_CheckDetailsAction.CheckAccount',tableName:'CheckAccount',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CashDate" Editor="datebox" FieldName="CashDate" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ReturnDate" Editor="datebox" FieldName="ReturnDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ReturnNotes" Editor="text" FieldName="ReturnNotes" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="動作別" Editor="text" FieldName="ActionCode" maxlength="0" Width="80" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster0" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster0" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
