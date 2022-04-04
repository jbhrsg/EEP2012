<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBRPT_CashTakeBackQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
     <script type="text/javascript">
         $(document).ready(function () {
             //設定欄位Caption 變顏色
             var flagIDs = ['#dataFormMasterIsUrgentPay', '#dataFormMasterIsNotPayDate'];
             $(flagIDs.toString()).each(function () {
                 var captionTd = $(this).closest('td').prev('td');
                 captionTd.css({ color: '#8A2BE2' });
             });
             //將Focus 欄位背景顏色改為黃色
             $(function () {
                 $("input, select, textarea").focus(function () {
                     $(this).css("background-color", "yellow");
                 });
                 $("input, select, textarea").blur(function () {
                     $(this).css("background-color", "white");
                 });
             });
             //var dgid = $('#dataGridView');
             //var queryPanel = getInfolightOption(dgid).queryDialog;
             //if (queryPanel)
             //    $(queryPanel).panel('resize', { width: 440});
             //$('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });
         })
         function BeforeOneMonth() {
             var dt = new Date();
             var aDate = new Date($.jbDateAdd('days', -31, dt));
             return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
         }
         function queryGrid(dg) {
             if ($(dg).attr('id') == 'dataGridView') {
                 var result = [];
                 var aVal = '';
                 var bVal = '';
                 aVal = $('#ApplyDateS_Query').datebox('getValue');
                 bVal = $('#ApplyDateE_Query').datebox('getValue');
                 if (aVal != '' && bVal != '')
                     result.push("ApplyDate between '" + aVal + "' and '" + bVal + "'");
                 aVal = $('#SetAccountDateS_Query').datebox('getValue');
                 bVal = $('#SetAccountDateE_Query').datebox('getValue');
                 if (aVal != '' && bVal != '')
                     result.push("SetAccountDate between '" + aVal + "' and '" + bVal + "'");
                 aVal = $('#AgainBillType_Query').combobox('getValue');
                 if (aVal != '')
                     result.push("AgainBillType = '" + aVal + "'");
                 aVal = $('#ApplyOrg_NO_Query').combobox('getValue');
                 if (aVal != '')
                     result.push("ApplyOrg_NO = '" + aVal + "'");
                 aVal = $('#ApplyEmpID_Query').combobox('getValue');
                 if (aVal != '')
                     result.push("ApplyEmpID = '" + aVal + "'");
                 aVal = $('#Status_Query').combobox('getValue');
                 if (aVal != '') {
                     if (aVal == 1) {
                         result.push("Flowflag <> 'Z'");
                     }
                     else {
                         result.push("Flowflag = 'Z'");
                     }
                 }
                 $(dg).datagrid('setWhere', result.join(' and '));
             }
         }
  </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCashTakeBackQuery.CashTakeBackDetails" runat="server" AutoApply="True"
                DataMember="CashTakeBackDetails" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="還款明細查詢" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="15,30,45,60,90" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="單據號碼" Editor="text" FieldName="CashTakeBackNO" Format="" MaxLength="0" Visible="true" Width="90" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="沖銷表單" Editor="infocombobox" FieldName="AgainBillType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" EditorOptions="valueField:'AgainBillType',textField:'AgainBillName',remoteName:'sCashTakeBackQuery.CashAgainBillType',tableName:'CashAgainBillType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="工號" Editor="text" FieldName="ApplyEmpID" MaxLength="0" Visible="true" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者姓名" Editor="text" FieldName="ApplyEmpName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ApplyOrg_NO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sCashTakeBackQuery.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="text" FieldName="ApplyDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70" Format="yyyy/mm/dd">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借單號" Editor="text" FieldName="ShortTermNO" Format="" MaxLength="0" Visible="true" Width="70" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借事由" Editor="text" FieldName="ShortTermGist" Format="" MaxLength="0" Visible="true" Width="240" />
                    <JQTools:JQGridColumn Alignment="left" Caption="幣別" Editor="text" FieldName="Currency" Format="" MaxLength="0" Visible="true" Width="50" />
                    <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="Amount" Format="N0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="現金來源" Editor="infocombobox" EditorOptions="valueField:'CashTakeBackType',textField:'CashTakeBackName',remoteName:'sCashTakeBackQuery.CashTakeBackType',tableName:'CashTakeBackType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CashTakeBackType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="現金匯款" Editor="text" FieldName="CashType" Format="" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="繳回入帳日" Editor="text" FieldName="SetAccountDate" Format="yyyy/mm/dd MM:HH:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="狀態" Editor="text" EditorOptions="" FieldName="FlowStatus" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn FieldName="CreateBy" Caption="CreateBy" IsNvarChar="False" Alignment="left" Width="120" Editor="text" MaxLength="0" Format="" Sortable="False" Frozen="False" ReadOnly="False" Visible="False" QueryCondition=""></JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="120" />
                </Columns>
                <TooItems>
                  <%--  <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請起迄日" Condition="＝" DataType="string" DefaultMethod="" Editor="datebox" FieldName="ApplyDateS" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="&lt;=" DataType="string" Editor="datebox" FieldName="ApplyDateE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" DefaultValue="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="歸還起迄日" Condition="%" DataType="string" Editor="datebox" FieldName="SetAccountDateS" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" DefaultMethod="BeforeOneMonth" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="%" DataType="string" DefaultValue="_today" Editor="datebox" FieldName="SetAccountDateE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="沖銷表單" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AgainBillType',textField:'AgainBillName',remoteName:'sCashTakeBackQuery.CashAgainBillType',tableName:'CashAgainBillType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AgainBillType" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請部門" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sCashTakeBackQuery.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyOrg_NO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請者" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sCashTakeBackQuery.Applyer',tableName:'Applyer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyEmpID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="狀態" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'STATUS',remoteName:'sCashTakeBackQuery.Status',tableName:'Status',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Status" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="65" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="還款明細內容" DialogLeft="10px" DialogTop="40px" Width="520px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CashTakeBackDetails" HorizontalColumnsCount="2" RemoteName="sCashTakeBackQuery.CashTakeBackDetails" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="單據號碼" Editor="text" FieldName="CashTakeBackNO" Format="" maxlength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemNO" Format="" maxlength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="text" FieldName="ApplyEmpID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者姓名" Editor="text" FieldName="ApplyEmpName" maxlength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sCashTakeBackQuery.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyOrg_NO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="93" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" maxlength="0" Width="93" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借單號" Editor="text" FieldName="ShortTermNO" Format="" maxlength="0" Width="90" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借事由" Editor="text" FieldName="ShortTermGist" Format="" maxlength="0" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption="幣別" Editor="text" FieldName="Currency" Format="" Width="90" />
                        <JQTools:JQFormColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="Amount" Format="" Width="90" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="現金匯款" Editor="text" FieldName="CashType" Format="" maxlength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="繳回入帳日" Editor="datebox" FieldName="SetAccountDate" Format="yyyy/mm/dd HH:MM:SS" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="140" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
