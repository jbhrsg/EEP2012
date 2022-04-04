<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBREQ_ShortTermClose.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var EndReason = '';
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
            DefaultQuery();
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 410 });
            $('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });
        });


        function dataGridViewOnLoadSucess() {
                 var FiltStr = '1=2';
                 $("#JQDataGrid1").datagrid('setWhere', FiltStr);

        }
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox'  onclick='return false;'  />";
        }
        function dataGridViewOnSelect(rowIndex, rowData) {
            if (rowData != null && rowData != undefined) {
                var ShortTermNO = rowData.ShortTermNO;
                var FiltStr = 'ShortTermNO = ' + "'" + ShortTermNO + "'";
                $("#JQDataGrid1").datagrid('setWhere', FiltStr);
            }
            else {
                var FiltStr = '1=2';
                $("#JQDataGrid1").datagrid('setWhere', FiltStr);
            }

        }
        function ShortTermForceClose() {
            var UserID = getClientInfo("UserID");
            if (UserID != '121' && UserID != '028' && UserID != '301' && UserID !='009' && UserID != '467') {
                alert('注意!!,非會計人員無法執行結案功能');
                return false;
            }
            var RowData = $("#dataGridView").datagrid('getSelected');
            if (RowData.IsSettleAccount == true) {
                alert('注意!!,此暫借單已結案,請重新選取');
                return false;
            }
            if (RowData.ShortTermAmount > RowData.TotalMinus) {
                var ConfirmYN = confirm('注意!!暫借金額(' + RowData.ShortTermAmount + ') > 已沖銷金額(' + RowData.TotalMinus + '),差額(' + (RowData.ShortTermAmount - RowData.TotalMinus) + '),系統將自動以銀行匯款或支票沖銷?');
                if (ConfirmYN == false)
                    return false;
                else {
                    var RowData = $('#dataGridView').datagrid('getSelected');
                    var rowIndex = $("#dataGridView").datagrid('getRowIndex', RowData);
                    openForm('#JQDialog2', RowData, "updated", 'dialog');
                }
            }
            else {
                ShortTermEnd(RowData.ShortTermNO, RowData.ShortTermAmount - RowData.TotalMinus, EndReason,'');
            }
        }
        function ShortTermEnd(sShortTermNO,sDiffAmt,sEndReason,sCashType) {
            var flag = 0;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sShortTermClose.ShortTerm',
                data: "mode=method&method=" + "PutShortTermEnd" + " &parameters=" + sShortTermNO + ',' + sDiffAmt + ',' + sEndReason + ',' + sCashType,
                cache: false,
                async: false,
                success: function (data) {
                    if (data == 'True') {
                        flag = 1;
                        $("#dataGridView").datagrid('reload');
                    }
                    else {
                        alert("注意!! 結案失敗");
                    }
                }
            });
            return flag;
        }
        //預設查詢：未結案且已沖銷金額>=暫借金額
        function DefaultQuery() {
            var FiltStr1 = '';
            var UserID = getClientInfo("UserID");
            switch (UserID) {
                //
                case "121": 
                    FiltStr1 = '( CompanyID=2 OR CompanyID=4 ) AND ';
                    break;
                case "301":
                    FiltStr1 = '( CompanyID=1 OR CompanyID=3 ) AND ';
                    break;
            }
            //var FiltStr = FiltStr1 + 'IsSettleAccount = 0 and (ISNULL((SELECT SUM(Amount) FROM ShortTermMinusDetails WHERE ShortTermNO=SHORTTERM.ShortTermNO),0)+ISNULL((SELECT SUM(Amount) FROM [60.250.52.106,1433].FWCRM.dbo.ShortTermMinusDetails WHERE ShortTermNO=SHORTTERM.ShortTermNO),0)) >= ShortTermAmount';
            //var FiltStr = FiltStr1 + 'IsSettleAccount = 0 and (ISNULL((SELECT SUM(Amount) FROM ShortTermMinusDetails WHERE ShortTermNO=SHORTTERM.ShortTermNO),0)+ISNULL((SELECT SUM(Amount) FROM [60.250.52.106,1433].FWCRM.dbo.ShortTermMinusDetails WHERE ShortTermNO=SHORTTERM.ShortTermNO),0) + ISNULL((SELECT SUM(Amount) FROM ShortTermMinusDetails WHERE ShortTermNO=SHORTTERM.ShortTermNO),0)+ISNULL((SELECT SUM(Amount) FROM [60.250.52.106,1433].FWCRMJS.dbo.ShortTermMinusDetails WHERE ShortTermNO=SHORTTERM.ShortTermNO),0)) >= ShortTermAmount';
            var FiltStr = FiltStr1 + 'IsSettleAccount = 0 and (ISNULL((SELECT SUM(Amount) FROM ShortTermMinusDetails WHERE ShortTermNO = SHORTTERM.ShortTermNO),0)+ISNULL((SELECT SUM(Amount) FROM [60.250.52.106,1433].FWCRM.dbo.ShortTermMinusDetails WHERE ShortTermNO=SHORTTERM.ShortTermNO),0) + ISNULL((SELECT SUM(Amount) FROM [60.250.52.106,1433].FWCRMJS.dbo.ShortTermMinusDetails WHERE ShortTermNO=SHORTTERM.ShortTermNO),0)) >= ShortTermAmount';
            $("#dataGridView").datagrid('setWhere', FiltStr);
        }
        function queryGrid(dg) {
            $("#JQDataGrid1").datagrid('setWhere', '1=2');
            if ($(dg).attr('id') == 'dataGridView') {
                var result = [];
                var aVal = '';
                var bVal = '';
                aVal = $('#CompanyID_Query').combobox('getValue');
                if (aVal != '')
                    result.push("CompanyID = '" + aVal + "'");
                aVal = $('#ShortTermDateS_Query').datebox('getValue');
                bVal = $('#ShortTermDateE_Query').datebox('getValue');
                if (aVal != '' && bVal != '')
                    result.push("ShortTermDate between '" + aVal + "' and '" + bVal + "'");
                aVal = $('#ShortTermNO_Query').combobox('getValue');
                if (aVal != '')
                    result.push("ShortTermNO = '" + aVal + "'");
                aVal = $('#EmployeeID_Query').combobox('getValue');
                if (aVal != '')
                    result.push("EmployeeID = '" + aVal + "'");
                aVal = $('#ShortTermGist_Query').val();
                if (aVal != '')
                    result.push("ShortTermGist like '%" + aVal + "%'");
                aVal = $('#IsSettleAccount_Query').combobox('getValue');
                if (aVal != '')
                    result.push('IsSettleAccount = ' + aVal);
                    $(dg).datagrid('setWhere', result.join(' and '));
            }
        }
        function JQDataForm4OnLoadSucess() {
            var ShortTermAmount = $('#JQDataForm4ShortTermAmount').val();
            var TotalMinus = $('#JQDataForm4TotalMinus').val();
            $('#JQDataForm4DiffAmt').val(ShortTermAmount - TotalMinus);

        }
        function JQDataForm4OnApplied() {
            var ShortTermNO = $('#JQDataForm4ShortTermNO').val();
            var DiffAmt = $('#JQDataForm4DiffAmt').val();
            EndReason = $('#JQDataForm4Reason').val();
            var CashType = $('#JQDataForm4CashType').combobox('getValue');
            ShortTermEnd(ShortTermNO, DiffAmt, EndReason,CashType);
        }
    
     </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sShortTermClose.ShortTerm" runat="server" AutoApply="True"
                DataMember="ShortTerm" Pagination="True" QueryTitle="暫借單查詢" EditDialogID="JQDialog1"
                Title="暫借款帳務處理" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="20" QueryAutoColumn="False" QueryLeft="10px" QueryMode="Window" QueryTop="50px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="900px" OnSelect="dataGridViewOnSelect" OnLoadSuccess="dataGridViewOnLoadSucess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="已結案" Editor="checkbox" FieldName="IsSettleAccount" Format="" Visible="true" Width="43" EditorOptions="on:1,off:0" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="需求日期" Editor="text" FieldName="RequestDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70" Format="yyyy/mm/dd">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sShortTermClose.Company',tableName:'Company',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借單號" Editor="text" FieldName="ShortTermNO" Format="" MaxLength="20" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借主旨" Editor="text" FieldName="ShortTermDescr" Format="" MaxLength="50" Visible="true" Width="220" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請人員" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60"></JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="預計歸還日" Editor="datebox" FieldName="PlanPayDate" Format="" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="right" Caption="暫借金額" Editor="numberbox" FieldName="ShortTermAmount" Format="N" Visible="true" Width="65" />
                    <JQTools:JQGridColumn Alignment="right" Caption="已沖銷金額" Editor="numberbox" FieldName="TotalMinus" Format="N" Visible="true" Width="65" />
                    <JQTools:JQGridColumn Alignment="right" Caption="溢沖金額" Editor="text" FieldName="DIffAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ShortTermDate" Format="" Visible="False" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="工號" Editor="text" FieldName="EmployeeID" Format="" MaxLength="20" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SettleAccountDate" Editor="datebox" FieldName="SettleAccountDate" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="簽核旗標" Editor="text" FieldName="Flowflag" Format="" MaxLength="1" Visible="False" Width="120" />
                </Columns>
                <TooItems>
                    <%--<JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"   OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton"  OnClick="apply"  Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton"  OnClick="cancel" Text="取消"  />--%>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="DefaultQuery" Text="待結案" />
                    <JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton"   OnClick="ShortTermForceClose" Text="結案" />
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出EXCEL" />
  
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sShortTermClose.Company',tableName:'Company',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="ShortTermDateS" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止日期" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="ShortTermDateE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="暫借單號" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SHORTTERMNO',textField:'SHORTTERMNO',remoteName:'sShortTermClose.ShortTermNO',tableName:'ShortTermNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ShortTermNO" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請者" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sShortTermClose.Applyer',tableName:'Applyer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EmployeeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="暫借主旨" Condition="%%" DataType="string" Editor="text" FieldName="ShortTermGist" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="1" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="表單狀態" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'0',text:'未結案',selected:'false'},{value:'1',text:'已結案',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsSettleAccount" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="暫借款帳務處理" DialogLeft="10px" DialogTop="10px" Width="540px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ShortTerm" HorizontalColumnsCount="2" RemoteName="sShortTermClose.ShortTerm" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借單編號" Editor="text" FieldName="ShortTermNO" Format="" Width="144" ReadOnly="True" Visible="True" Span="1" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ShortTermDate" Format="yyyy/mm/dd" Width="145" Visible="True" Span="1" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="text" EditorOptions="" FieldName="EmployeeName" ReadOnly="False" Span="1" Width="144" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sShortTerm.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyOrg_NO" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="144" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借單類別" Editor="text" EditorOptions="" FieldName="ShortTypeName" MaxLength="0" ReadOnly="False" Span="1" Visible="True" Width="144" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="text" EditorOptions="" FieldName="CompanyName" Visible="True" Width="140" Span="1" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主" Editor="infocombobox" EditorOptions="valueField:'EmployerID',textField:'EmployerName',remoteName:'sShortTermClose.Employer',tableName:'Employer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EmployerID" MaxLength="6" ReadOnly="False" Span="1" Visible="True" Width="148" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本備註" Editor="text" FieldName="CostNotes" MaxLength="0" ReadOnly="False" Span="2" Visible="True" Width="360" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借事由" Editor="text" FieldName="ShortTermGist" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="360" />
                        <JQTools:JQFormColumn Alignment="left" Caption="事由說明" Editor="textarea" FieldName="ShortTermDescr" Format="" Width="357" EditorOptions="height:80" Span="2" Visible="True" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借金額" Editor="numberbox" FieldName="ShortTermAmount" Format="" Width="140" Visible="True" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="支付方式" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sShortTermClose.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" Format="" Width="145" Visible="True" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="受款人" Editor="infocombobox" EditorOptions="valueField:'VendID',textField:'VendShortName',remoteName:'sShortTermClose.Vendors',tableName:'Vendors',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="144" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求日期" Editor="datebox" FieldName="RequestDate" Format="yyyy/mm/dd" Width="143" Visible="True" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預計歸還日" Editor="datebox" FieldName="PlanPayDate" Format="yyyy/mm/dd" Visible="True" Width="145" ReadOnly="False" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsSettleAccount" MaxLength="0" NewRow="False" OnBlur="" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true" FieldName="SettleAccountDate" Format="yyyy/mm/dd hh:MM:ss" ReadOnly="True" Visible="False" Width="145" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="狀態" Editor="text" EditorOptions="" FieldName="ReqEnd" ReadOnly="False" Span="1" Visible="False" Width="80" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="RequisitionNO" Editor="text" FieldName="RequisitionNO" Format="" Visible="False" Width="180" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
        <br />
        <div>
            <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="ShortTermMinusDetails" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sShortTermClose.ShortTermMinusDetails" RowNumbers="True" Title="暫借款還款明細" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="900px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="對沖方式" Editor="text" FieldName="AgainBillName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="對沖單號" Editor="text" FieldName="AgainBillNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="對沖單號內容" Editor="text" FieldName="ShortTermGist" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="280">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="幣別" Editor="text" FieldName="CURRENCY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="text" FieldName="AMOUNT" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="sum" Visible="True" Width="60" Format="N0">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="現金/匯款" Editor="text" FieldName="CashType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="現金/匯款" Editor="infocombobox" EditorOptions="valueField:'AgainBillType',textField:'AgainBillName',remoteName:'sShortTermClose.CashAgainBillType',tableName:'CashAgainBillType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AgainBillType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="沖銷日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortTermNO" Editor="text" FieldName="ShortTermNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" Format="N">
                    </JQTools:JQGridColumn>
                </Columns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog2"  BindingObjectID="JQDataForm4" runat="server" DialogLeft="10px" DialogTop="65px" Title="匯款支票沖銷" Width="520px" Closed="False">
                  <JQTools:JQDataForm ID="JQDataForm4" runat="server" DataMember="ShortTerm" HorizontalColumnsCount="1" RemoteName="sShortTermClose.ShortTerm" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnLoadSuccess="JQDataForm4OnLoadSucess" OnApplied="JQDataForm4OnApplied">
                      <Columns>
                          <JQTools:JQFormColumn Alignment="left" Caption="暫借單號" Editor="text" FieldName="ShortTermNO" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="90" />
                          <JQTools:JQFormColumn Alignment="left" Caption="暫借主旨" Editor="text" FieldName="ShortTermGist" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="360" />
                          <JQTools:JQFormColumn Alignment="right" Caption="暫借金額" Editor="text" FieldName="ShortTermAmount" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="90" />
                          <JQTools:JQFormColumn Alignment="right" Caption="已沖銷金額" Editor="text" FieldName="TotalMinus" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="90" />
                          <JQTools:JQFormColumn Alignment="left" Caption="匯款支票" Editor="infocombobox" EditorOptions="items:[{value:'匯款',text:'匯款',selected:'false'},{value:'支票',text:'支票',selected:'false'},{value:'其它',text:'其它',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CashType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="95" />
                          <JQTools:JQFormColumn Alignment="right" Caption="沖銷金額" Editor="text" FieldName="DiffAmt" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                          <JQTools:JQFormColumn Alignment="left" Caption="沖銷原因" Editor="text" FieldName="Reason" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="360" />
                      </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="JQDefault4" runat="server" BindingObjectID="JQDataForm4" EnableTheming="True">
                    </JQTools:JQDefault>
                   <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm4" EnableTheming="True">
                       <Columns>
                           <JQTools:JQValidateColumn CheckNull="True" FieldName="DIffAmt" RemoteMethod="True" ValidateType="None" />
                           <JQTools:JQValidateColumn CheckNull="True" FieldName="Reason" RemoteMethod="True" ValidateType="None" />
                       </Columns>
                    </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
