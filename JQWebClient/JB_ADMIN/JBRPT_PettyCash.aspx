<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBRPT_PettyCash.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
            $('#lastestaccdate').hide();
   
            $('#SettleAccountDate_Query').closest("tr").hide();
            $('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });
            $('#PettyCashTotal').hide();
            $('#AccountAmt').hide();
        });
        function GetSettleAccountDate() {
            var rowdata = $("#JQDataGrid").datagrid('getRows')
            var row_length = rowdata.length;
            var row1 = rowdata[0];
            var dDate = row1.SettleAccountDate;
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel) {
                $(queryPanel).find('table:first()').prepend($('<tr>').append($('<td>', { colspan: '4' }).html(dDate)));
                $(queryPanel).panel('resize', { width: 430 });
            }
        }
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridView') {
                var filtstr = '';
                var result = [];
                var result1 = [];
                var aVal = '';
                var bVal = '';
                aVal = $('#APPLYDATES_Query').datebox('getValue');
                if (aVal != '')
                    result.push("APPLYDATE >= '" + aVal + "'");
                aVal = $('#APPLYDATEE_Query').datebox('getValue');
                if (aVal != '')
                    result.push("APPLYDATE <= '" + aVal + "'");
                aVal = $('#ApplyOrg_NO_Query').combobox("getValue");
                if (aVal != '')
                    result.push("ApplyOrg_NO = '" + aVal + "'");
                aVal = $('#CostCenterID_Query').combobox("getValue");
                if (aVal != '')
                    result.push("CostCenterID = '" + aVal + "'");
                aVal = $('#PayTypeID_Query').combobox("getValue");
                if (aVal != '')
                    result.push("PayTypeID = '" + aVal + "'");
                aVal = $('#APPLYEMPID_Query').refval('getValue');
                if (aVal != '')
                    result.push("APPLYEMPID = '" + aVal + "'");
                aVal = $('#IsSettleAccount_Query').combobox("getValue");
                if (aVal != '')
                    result.push("IsSettleAccount = '" + aVal + "'");
                aVal = $('#SettleAccountDateS_Query').combobox("getValue");
                if (aVal != '')
                    result.push("SettleAccountDate >= '" + aVal + "'");
                aVal = $('#SettleAccountDateE_Query').combobox("getValue");
                if (aVal != '')
                    result.push("SettleAccountDate <= '" + aVal + "'");
                var filtstr = result.join(' and ');
                $("#dataGridView").datagrid('setWhere', filtstr);
                $("#JQDataGrid1").datagrid('setWhere', filtstr);
                $("#DG_AccountAmt").datagrid('setWhere', filtstr);
                
            }
        }
        function dataGridViewOnLoadSuccess() {
            var count = $("#dataGridView").datagrid('getRows').length;
            if (count > 0) {
                $('#PettyCashTotal').show();
                $('#AccountAmt').show();
            }
        }
        function myPrint() {
            var WhereString = "";
            var queryParams = $("#dataGridView").datagrid("options").queryParams;
            if (queryParams.queryWord != "") {
                var queryWord = eval('(' + queryParams.queryWord + ')');
                if (queryWord != undefined && queryWord != null)
                    WhereString = queryWord.WhereString;
            }
            var repotype = $("input:radio[name='RadioButtonList1']:checked").val();
            switch (repotype) {
                case "1":
                    var rdlcstr = "~/JB_ADMIN/rPettyCashPay.rdlc";
                    exportReport("#dataGridView", "sPettyCashRepo.PettyCash", "PettyCash", rdlcstr, WhereString);
                    break;
                case "2":
                    var rdlcstr = "~/JB_ADMIN/rPettyCashAccount.rdlc";
                    exportReport("#dataGridView", "sPettyCashRepo.PettyCash", "PettyCash", rdlcstr, WhereString);
                    break;
                case "3":
                    var rdlcstr = "~/JB_ADMIN/rPettyCashCostCenter.rdlc";
                    exportReport("#dataGridView", "sPettyCashRepo.PettyCash", "PettyCash", rdlcstr, WhereString);
                    break;
                case "4":
                    //$('#PettyCashTotal').show();
                    break;
                default:
                    break;
            }
        }
        function dfOnLoadSuccess() {
            var CostCenterID = $('#dataFormMasterCostCenterID').combobox('getValue');
            var BudgetType = $('#dataFormMasterBudgetType').combobox('getValue');
            var filter = "CostCenterID = '" + CostCenterID + "' and BudgetType = " + BudgetType;
            $("#dataFormMasterAcSubno").combobox("setWhere", filter);
            
        }
        function OnSelectCostCenterID(rowData) {
            $("#dataFormMasterAcSubno").combobox("setValue", '');
            var BudgetType = $('#dataFormMasterBudgetType').combobox('getValue');
            var filter = "CostCenterID = '" + rowData.CostCenterID + "' and BudgetType = " + BudgetType;
            $("#dataFormMasterAcSubno").combobox("setWhere", filter);
        }
        function OnSelectBudgetType(rowData) {
            $("#dataFormMasterAcSubno").combobox("setValue", '');
            var CostCenterID = $('#dataFormMasterCostCenterID').combobox('getValue');
            var filter = "CostCenterID = '" + CostCenterID + "' and BudgetType = " + rowData.BudgetType;
            $("#dataFormMasterAcSubno").combobox("setWhere", filter);
        }
        function JQDataGrid1OnLoadSucess() {
        }
        function settleAccount() {
            var rows = $('#dataGridView').datagrid("getRows");
            if (rows.length <= 0) {
                alert('注意!!查詢後無資料,無法結帳');
                return false;
             }
             openForm('#JQDialog2', $('#dataGridView').datagrid('getSelected'), "update", 'dialog');
             return true;
            }
         function JQDataFrom1OnApply() {
                var PrePayDate = $("#JQDataForm1PrePayDate").datebox('getValue');
                if (PrePayDate == "" || PrePayDate == undefined) {
                    alert('注意!!支付日期,不得為空白!!')
                    return false;
                }
                var PayTypeID = $('#PayTypeID_Query').combobox("getValue");
                if (PayTypeID == "" || PayTypeID == undefined) {
                    alert('注意!!選擇結帳時,付款方式不得為空白!!')
                    return false;
                }
                var StartDate = $('#APPLYDATES_Query').datebox('getValue');
                var EndDate = $('#APPLYDATEE_Query').datebox('getValue');
                //var StartDate = $($("input[id='APPLYDATE_Query']")[0]).datebox('getBindingValue');
                //var EndDate = $($("input[id='APPLYDATE_Query']")[1]).datebox('getBindingValue');
                //alert(StartDate);
                //alert(EndDate);
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sPettyCashRepo.PettyCash',
                    data: "mode=method&method=" + "settleAccount" + " &parameters=" + StartDate + "," + EndDate + "," + PayTypeID + "," + PrePayDate, 
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data == "True") {
                            $.messager.confirm('提示訊息', '結帳成功,按下「確定」離開', function (r) {
                                if (r) {
                                    $("#dataGridView").datagrid('reload');
                                }
                            })
                        }
                        else {
                            alert("結帳失敗")
                        }

                    }

                });

            }
         function OnSelectAcSubno(rowData) {
             $('#dataFormMasterAcno').val(rowData.Acno_S);
             $('#dataFormMasterSubAcno').val(rowData.SubAcno_S);
            }
         function GetLastAccountDate() {
                var EndDate = $($("input[id='APPLYDATE1_Query']")[1]).datebox('getBindingValue');
                //alert(EndDate);
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sPettyCashRepo.PettyCash', //連接的Server端，command
                    data: "mode=method&method=" + "GetLastAccountDate" + "&parameters=" + EndDate, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        var rows = $.parseJSON(data);
                        if (rows.length > 0) {
                            $("#JQDateBox1").datebox('setValue', rows[0].LastAccountDate);
                            //Dt = new Date(rows[0].LastAccountDate);
                            //var dd = Dt.getFullYear() + "/" + (Dt.getMonth() + 1) + "/" + Dt.getDate()


                        }
                    }
                }
                );
            }
            function genCheckBox(val) {
                if (val)
                    return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
                else
                    return "<input  type='checkbox' onclick='return false;' />";
            }
            function BeforeSevenDays() {
                var dt = new Date();
                var aDate = new Date($.jbDateAdd('days', -7, dt));
                return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
            }
            function TodayDate() {
                var dt = new Date();
                var aDate = new Date($.jbDateAdd('days', 0, dt));
                return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
            }
            function IsSettleAccountOnSelect() {
                var SettleAccountType = $("#IsSettleAccount_Query").combobox('getValue')
                if (SettleAccountType == 1) {
                    $('#APPLYDATES_Query').datebox('setValue', '');
                    $('#APPLYDATEE_Query').datebox('setValue', '');
                    $('#SettleAccountDateS_Query').closest('tr').show();
                    $('#SettleAccountDateE_Query').closest('tr').show();
                    $('#SettleAccountDateS_Query').combobox('setValue', GetLstAccDate());
                    $('#SettleAccountDateE_Query').combobox('setValue', GetLstAccDate());
 
                }
                else {
                    $('#APPLYDATES_Query').datebox('setValue', BeforeSevenDays());
                    $('#APPLYDATEE_Query').datebox('setValue', TodayDate());
                    $('#SettleAccountDateS_Query').combobox('setValue', '');
                    $('#SettleAccountDateE_Query').combobox('setValue', '');
                    $('#SettleAccountDateS_Query').closest('tr').hide();
                    $('#SettleAccountDateE_Query').closest('tr').hide();
                }
            }
            function GetLstAccDate() {
                GetLastAccountDate();
                var Dt = $("#JQDateBox1").datebox('getValue');
                var DD = Dt.substr(0, 4) + '/' + Dt.substr(5, 2) + '/' + Dt.substr(8, 2)
                return DD;
            }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 110px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <table style="width: 75%;">
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="選擇輸出報表"></asp:Label></td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Size="Small" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">零用金簽收表</asp:ListItem>
                            <asp:ListItem Value="2">會計彙總表</asp:ListItem>
                            <asp:ListItem Value="3">成本中心報表</asp:ListItem>
                         </asp:RadioButtonList></td>
                </tr>
            </table>
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPettyCashRepo.PettyCash" runat="server" AutoApply="True"
                DataMember="PettyCash" Pagination="True" QueryTitle="輸出列印條件" EditDialogID="JQDialog1"
                Title="零用金報表列印" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40" PageSize="10" QueryAutoColumn="False" QueryLeft="50px" QueryMode="Window" QueryTop="50px" RecordLock="False" RecordLockMode="None" TotalCaption="合計:" UpdateCommandVisible="True" ViewCommandVisible="True" ReportFileName="~/JB_ADMIN/rPettyCashPay.rdlc" BufferView="False" NotInitGrid="False" RowNumbers="True" OnLoadSuccess="dataGridViewOnLoadSuccess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="單號" Editor="text" FieldName="PETTYCASHID" Format="" MaxLength="0" Width="65" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="已結案" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsSettleAccount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="45" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="OrgName" Format="" MaxLength="0" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請員工" Editor="text" FieldName="EmployeeName" Format="" MaxLength="0" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="APPLYDATE" Format="yyyy/mm/dd" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="會計科目" Editor="text" FieldName="AccountName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenterName" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="摘要" Editor="text" FieldName="AccountNotes" Format="" MaxLength="0" Width="210" />
                    <JQTools:JQGridColumn Alignment="left" Caption="憑證類型" Editor="text" FieldName="ProofTypeName" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="PettyCashAmt" Format="N0" Width="50" Total="sum" />
                    <JQTools:JQGridColumn Alignment="right" Caption="稅額" Editor="numberbox" FieldName="PettyCashTax" Format="N0" Width="50" Total="sum" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="APPLYEMPID" Editor="text" FieldName="APPLYEMPID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CostCenterID" Editor="text" FieldName="CostCenterID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ProofTypeID" Editor="numberbox" FieldName="ProofTypeID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ProofNO" Editor="text" FieldName="ProofNO" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AccountID" Editor="text" FieldName="AccountID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="PayTypeID" Editor="numberbox" FieldName="PayTypeID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PayTypeName" Editor="text" FieldName="PayTypeName" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="小計金額" Editor="numberbox" FieldName="SumAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Format="N0" Total="sum" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SettleAccountDate" Editor="text" FieldName="SettleAccountDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="會科主目" Editor="text" FieldName="Acno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="會科主目名稱" Editor="text" FieldName="AcnoName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="報表條件" />
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="myPrint" Text="列印輸出" />
                    <JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton" OnClick="settleAccount" Text="結帳" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="&gt;=" DataType="datetime" DefaultValue="" Editor="datebox" FieldName="APPLYDATES" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" Format="yyyy/MM/dd" DefaultMethod="BeforeSevenDays" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止日期" Condition="&lt;=" DataType="datetime" DefaultValue="_today" Editor="datebox" FieldName="APPLYDATEE" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" Format="yyyy/MM/dd" DefaultMethod="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="成本中心" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sPettyCashRepo.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="180" Span="2" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請部門" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'Org_NO',textField:'Org_Desc',remoteName:'sPettyCashRepo.Org',tableName:'Org',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyOrg_NO" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="付款方式" Condition="=" DataType="number" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sPettyCashRepo.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工" Condition="%%" DataType="string" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sPettyCashRepo.Employee',tableName:'Employee',columns:[{field:'EmployeeName',title:'員工姓名',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'EmployeeID',textField:'EmployeeName',valueFieldCaption:'EmployeeID',textFieldCaption:'員工姓名',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="APPLYEMPID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="結案狀態" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'0',text:'未結案',selected:'false'},{value:'1',text:'已結案',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,onSelect:IsSettleAccountOnSelect,panelHeight:200" FieldName="IsSettleAccount" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" DefaultValue="0" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始結帳日" Condition="&gt;=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AccountDate',textField:'AccountDate',remoteName:'sPettyCashRepo.AccDateList',tableName:'AccDateList',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SettleAccountDateS" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" DefaultMethod="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止結帳日" Condition="&lt;=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AccountDate',textField:'AccountDate',remoteName:'sPettyCashRepo.AccDateList',tableName:'AccDateList',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SettleAccountDateE" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" DefaultMethod="" DefaultValue="" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <div id="lastestaccdate">
                <JQTools:JQDataGrid ID="JQDataGrid" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="LastestAccDate" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sPettyCashRepo.LastestAccDate" Title="JQDataGrid" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnLoadSuccess="GetSettleAccountDate">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="SettleAccountDate" Editor="text" FieldName="Settleaccountdate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="Insert" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="True" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <JQTools:JQDateBox ID="JQDateBox1" runat="server" />
            </div>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="零用金報表列印" Width="776px" DialogLeft="80px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="PettyCash" HorizontalColumnsCount="4" RemoteName="sPettyCashRepo.PettyCash" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" Width="900px" ParentObjectID="dataGridView" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnLoadSuccess="dfOnLoadSuccess">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="申請員工" Editor="infocombobox" FieldName="APPLYEMPID" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sPettyCashRepo.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" Span="1" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'Org_NO',textField:'Org_Desc',remoteName:'sPettyCashRepo.OrgAll',tableName:'OrgAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Org_NOParent" Format="" Width="120" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="APPLYDATE" Format="yyyy/mm/dd" Width="120" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sPettyCashRepo.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InsGroupID" MaxLength="0" Span="1" Width="125" NewRow="False" ReadOnly="False" RowSpan="1" Visible="true" />
                        <JQTools:JQFormColumn Alignment="left" Caption="摘要" Editor="textarea" EditorOptions="height:45" FieldName="AccountNotes" Format="" Span="4" Width="650" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sPettyCashRepo.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectCostCenterID,panelHeight:200" FieldName="CostCenterID" Format="" MaxLength="0" Width="124" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="科目類別" Editor="infocombobox" EditorOptions="valueField:'BudgetType',textField:'BudgetTypeName',remoteName:'sPettyCashRepo.AccountType',tableName:'AccountType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectBudgetType,panelHeight:200" FieldName="BudgetType" Width="120" MaxLength="0" Span="1" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目" Editor="infocombobox" EditorOptions="valueField:'AcSubno',textField:'AcnoName',remoteName:'sPettyCashRepo.BudgetBase',tableName:'BudgetBase',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectAcSubno,panelHeight:200" FieldName="AcSubno" Format="" MaxLength="0" Width="310" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單據年月" Editor="infocombobox" EditorOptions="valueField:'InvoiceYM',textField:'InvoiceYM',remoteName:'sPettyCashRepo.YearMonth',tableName:'YearMonth',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InvoiceYM" Width="126" />
                        <JQTools:JQFormColumn Alignment="left" Caption="支付憑據" Editor="infocombobox" EditorOptions="valueField:'ProofTypeID',textField:'ProofTypeName',remoteName:'sPettyCashRepo.ProofType',tableName:'ProofType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ProofTypeID" Format="" MaxLength="0" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="憑據單號" Editor="text" FieldName="ProofNO" Format="" Width="120" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="未稅金額" Editor="numberbox" FieldName="PettyCashAmt" Format="" Width="120" MaxLength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="稅額" Editor="numberbox" FieldName="PettyCashTax" Format="" MaxLength="0" Width="120" ReadOnly="False" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="給付方式" Editor="infocombobox" FieldName="PayTypeID" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sPettyCashRepo.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PETTYCASHID" Editor="text" FieldName="PETTYCASHID" Format="" MaxLength="0" Width="180" Visible="False" ReadOnly="False" Span="1" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Acno" Editor="text" FieldName="Acno" MaxLength="0" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SubAcno" Editor="text" FieldName="SubAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
                 <JQTools:JQDialog ID="JQDialog2"  BindingObjectID="JQDataForm1" runat="server" DialogLeft="30px" DialogTop="65px" Title="支付日期" Width="472px" Closed="False" ShowSubmitDiv="True">
                  <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="PettyCash" HorizontalColumnsCount="2" RemoteName="sPettyCashRepo.PettyCash" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" ParentObjectID="dataGridView" OnApply="JQDataFrom1OnApply">
                        <Columns>
     
                            <JQTools:JQFormColumn Alignment="left" Caption="支付日期" Editor="datebox" FieldName="PrePayDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
     
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                        <Columns>
                        </Columns>
                    </JQTools:JQDefault>
            </JQTools:JQDialog>
            <div id="PettyCashTotal">
            <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="PettyCashTotal" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="5,10,20,40" PageSize="5" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sPettyCashRepo.PettyCashTotal" RowNumbers="True" Title="員工匯款明細" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="JQDataGrid1OnLoadSucess" Width="600px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="工號" Editor="text" FieldName="APPLYEMPID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銀行代號" Editor="text" FieldName="BANK_CODE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銀行名稱" Editor="text" FieldName="BANK_CNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="帳戶" Editor="text" FieldName="ACCOUNT_NO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="身分證號" Editor="text" FieldName="IDNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="匯款金額" Editor="text" FieldName="SumAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" Format="N0" Total="">
                    </JQTools:JQGridColumn>
                </Columns>
                 <TooItems>
                 <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出EXCEL" />
                 </TooItems>
            </JQTools:JQDataGrid>
            </div>
            <div id="AccountAmt">
                <JQTools:JQDataGrid ID="DG_AccountAmt" runat="server" AutoApply="False" DataMember="PettyCashAccountAmt" Pagination="True" ParentObjectID="" RemoteName="sPettyCashRepo.PettyCashAccountAmt" Title="會科彙總金額" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="5,10,20,40" PageSize="5" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="600px" CheckOnSelect="True" RecordLock="False" ColumnsHibeable="False" RecordLockMode="None" OnLoadSuccess="" BufferView="False" NotInitGrid="False" RowNumbers="True">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="科目代號" Editor="text" FieldName="ACNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="科目名稱" Editor="text" FieldName="AcnoName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="380">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="合計金額" Editor="numberbox" FieldName="SumAmt" Width="100" Visible="True" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Format="N0" />
                    </Columns>
                  <TooItems>
                 <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出EXCEL" />
                 </TooItems>
                </JQTools:JQDataGrid>
        </div>
        </div>

    </form>
</body>
</html>
