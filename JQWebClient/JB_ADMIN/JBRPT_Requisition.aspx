<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBRPT_Requisition.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            //設定 Grid QueryColunm Windows width=320px
            //var dgid = $('#dataGridView');
            //var queryPanel = getInfolightOption(dgid).queryDialog;
            //if (queryPanel)
            //    $(queryPanel).panel('resize', { width: 480 });
            //$('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });
    });
    function queryGrid(dg) {
        if ($(dg).attr('id') == 'dataGridView') {
            var result = [];
            var aVal = '';
            var bVal = '';
            aVal = $('#InvoiceYM_Query').combobox('getValue');
            if (aVal != '')
                result.push("A.InvoiceYM = '" + aVal + "'");
            aVal = $('#DateS_Query').datebox('getValue');
            bVal = $('#DateE_Query').datebox('getValue');
            if (aVal != '' && bVal != '')
                result.push("A.PlanPayDate between '" + aVal + "' and '" + bVal + "'");
            aVal = $('#CompanyID_Query').combobox('getValue');
            if (aVal != '')
                result.push("A.CompanyID = '" + aVal + "'");
            aVal = $('#PayTypeID_Query').combobox('getValue');
            if (aVal != '')
                result.push("A.PayTypeID = '" + aVal + "'");
            aVal = $('#FlowStatus_Query').combobox('getValue');
            if (aVal == '1')
                result.push("A.Flowflag = 'Z'");
            else if (aVal == '2')
                result.push("A.Flowflag in ('P','N')");
            else if (aVal == '3')
                result.push("1=1");
            $(dg).datagrid('setWhere', result.join(' and '));
        }
    }
    function BeforeOneMonth() {
        var dt = new Date();
        var aDate = new Date($.jbDateAdd('days', -31, dt));
        return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
    }
    function TodayDate() {
        var dt = new Date();
        var aDate = new Date($.jbDateAdd('days', 0, dt));
        return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
    }
    function OnSelectInvoiceYM() {
        $('#DateS_Query').datebox('setValue', null);
        $('#DateE_Query').datebox('setValue', null);
    }
   </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sRequisitionRepo.Requisition" runat="server" AutoApply="True"
                DataMember="Requisition" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="請款單報表輸出" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="15,30,45,60" PageSize="15" QueryAutoColumn="False" QueryLeft="30px" QueryMode="Panel" QueryTop="30px" RecordLock="False" RecordLockMode="None" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="1320px" ReportFileName="~/JB_ADMIN/rRequisitionSummary.rdlc" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="申請單號" Editor="text" FieldName="RequisitionNO" Format="" MaxLength="20" Width="75" Visible="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="狀態" Editor="text" FieldName="FlowStatus" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="支付日期" Editor="datebox" FieldName="PlanPayDate" Format="yyyy/mm/dd" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="VendAccountName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="190" />
                    <JQTools:JQGridColumn Alignment="left" Caption="身分證號" Editor="text" FieldName="IDNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="請款內容" Editor="text" FieldName="RequisitionDescr" Format="" MaxLength="50" Width="280" />
                    <JQTools:JQGridColumn Alignment="left" Caption="單據號碼" Editor="text" FieldName="ProofNO" Format="" MaxLength="0" Width="80" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="發票年月" Editor="text" FieldName="InvoiceYM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="RequisitAmt" Format="N" Width="65" Total="sum" />
                    <JQTools:JQGridColumn Alignment="right" Caption="手續費" Editor="numberbox" FieldName="Remit" Format="N" Width="50" Total="sum" />
                    <JQTools:JQGridColumn Alignment="right" Caption="給付金額" Editor="text" FieldName="PayAmount" Format="N" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="sum" Visible="True" Width="65">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="CompanyMame" Format="" MaxLength="0" Width="75" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="DeptName" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" Width="65" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="付款條件" Editor="text" FieldName="PayTypeTerm" MaxLength="0" Width="70" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="受款人" Editor="text" FieldName="PayToName" Format="" MaxLength="0" Width="150" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="要匯費" Editor="text" FieldName="IsRemit" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="匯費方式" Editor="text" FieldName="RemitType" Format="" MaxLength="0" Width="60" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="匯款銀行" Editor="text" FieldName="BankName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="付款備註" Editor="text" FieldName="PayToNotes" Format="" MaxLength="100" Width="100" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="公司" Editor="numberbox" FieldName="CompanyID" Format="" Width="20" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ProofTypeName" Editor="text" FieldName="ProofTypeName" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請部門" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="8" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenterID" Format="" MaxLength="10" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CostCenterName" Editor="text" FieldName="CostCenterName" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="RequisitionTypeID" Editor="numberbox" FieldName="RequisitionTypeID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ProofTypeID" Editor="numberbox" FieldName="ProofTypeID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="PayTermID" Editor="numberbox" FieldName="PayTermID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="付款方式" Editor="infocombobox" FieldName="PayTypeID" Format="" Width="80" Visible="True" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sRequisitionRepo.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RequisitionNotes" Editor="text" FieldName="RequisitionNotes" Format="" MaxLength="100" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PayTo" Editor="text" FieldName="PayTo" Format="" MaxLength="20" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsUrgentPay" Editor="text" FieldName="IsUrgentPay" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsNotPayDate" Editor="text" FieldName="IsNotPayDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortTermNO" Editor="text" FieldName="ShortTermNO" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="RequistKindID" Editor="numberbox" FieldName="RequistKindID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銀行代碼" Editor="text" FieldName="BankNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="帳戶" Editor="text" FieldName="VendAccount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="會科主目" Editor="text" FieldName="Acno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="會科子目" Editor="text" FieldName="SubAcno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
 <%--               <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                       OnClick="openQuery" Text="報表條件" />--%>
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportReport"  Text="列印輸出"  />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="發票年月" Condition="" DataType="string" Editor="infocombobox" EditorOptions="valueField:'InvoiceYM',textField:'InvoiceYM',remoteName:'sRequisitionRepo.InvoiceYM',tableName:'InvoiceYM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectInvoiceYM,panelHeight:200" FieldName="InvoiceYM" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="70" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="付款起始日期" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="DateS" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" DefaultMethod="BeforeOneMonth" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="付款終止日期" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="DateE" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" DefaultMethod="TodayDate" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="number" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sRequisitionRepo.Company',tableName:'Company',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="130" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="付款條件" Condition="=" DataType="number" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sRequisitionRepo.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="流程狀態" Condition="" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'已結案',selected:'false'},{value:'2',text:'流程中',selected:'false'},{value:'3',text:'不拘',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="FlowStatus" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="70" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="請款單報表輸出" DialogLeft="70px" DialogTop="50px" Width="790px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Requisition" HorizontalColumnsCount="3" RemoteName="sRequisitionRepo.Requisition" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >
                    <Columns>
                     <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="text" FieldName="RequisitionNO" Format="" maxlength="0" Width="127"  ReadOnly="True" Span="1"/>
                     <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sRequisitionRepo.Company',tableName:'Company',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:250" FieldName="CompanyID" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                     <JQTools:JQFormColumn Alignment="left" Caption="請款性質" Editor="infocombobox" EditorOptions="valueField:'RequisitKindID',textField:'RequistKindName',remoteName:'sRequisition.RequistKind',tableName:'RequistKind',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="RequistKindID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                     <JQTools:JQFormColumn Alignment="left" Caption="申請員工" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sRequisitionRepo.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyEmpID" MaxLength="0" ReadOnly="False" Width="133" />
                     <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sRequisitionQuery.Organization',tableName:'Organization',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:250" FieldName="ApplyOrg_NO" Format="" ReadOnly="False" Width="130" Span="1" />
                     <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" ReadOnly="True" Width="90" Span="1" />
                     <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sRequisitionRepo.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:250" FieldName="CostCenterID" Format="" Span="1" Width="133" MaxLength="0" ReadOnly="True" />
                     <JQTools:JQFormColumn Alignment="left" Caption="科目類別" Editor="infocombobox" EditorOptions="valueField:'BudgetType',textField:'BudgetTypeName',remoteName:'sRequisitionRepo.BudgetType',tableName:'BudgetType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="BudgetType" MaxLength="0" ReadOnly="False" Span="1" Width="130" NewRow="False" RowSpan="1" Visible="True" />
                     <JQTools:JQFormColumn Alignment="left" Caption="會計科目" Editor="infocombobox" EditorOptions="valueField:'AcSubno',textField:'AcnoName',remoteName:'sRequisitionRepo.BudgetBase',tableName:'BudgetBase',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountID" Span="1" Width="163" />
                     <JQTools:JQFormColumn Alignment="left" Caption="請款事由" Editor="textarea" EditorOptions="height:40" FieldName="RequisitionDescr" Format="" Span="3" Width="580" maxlength="254" />
                     <JQTools:JQFormColumn Alignment="left" Caption="請款依據" Editor="infocombobox" EditorOptions="valueField:'RequisitionTypeID',textField:'RequisitionTypeName',remoteName:'sRequisition.RequisitionType',tableName:'RequisitionType',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="RequisitionTypeID" Format="" Width="133" maxlength="0" ReadOnly="False" />
                     <JQTools:JQFormColumn Alignment="left" Caption="檢附憑證" Editor="infocombobox" EditorOptions="valueField:'ProofTypeID',textField:'ProofTypeName',remoteName:'sRequisition.ProofType',tableName:'ProofType',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="ProofTypeID" Format="" Width="130" OnBlur="" />
                     <JQTools:JQFormColumn Alignment="left" Caption="憑據號碼" Editor="text" FieldName="ProofNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="155" OnBlur="" />
                     <JQTools:JQFormColumn Alignment="left" Caption="請款金額" Editor="numberbox" FieldName="RequisitAmt" Format="" Width="130" NewRow="False" OnBlur="" />
                     <JQTools:JQFormColumn Alignment="left" Caption="未稅金額" Editor="numberbox" FieldName="RequisitAmtNoTax" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="125" />
                     <JQTools:JQFormColumn Alignment="left" Caption="稅額" Editor="numberbox" FieldName="RequisitAmtTax" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="130" />
                     <JQTools:JQFormColumn Alignment="left" Caption="暫借款單" Editor="infocombobox" EditorOptions="valueField:'ShortTermNO',textField:'ShortTermDescr',remoteName:'sRequisitionQuery.ShortTerm',tableName:'ShortTerm',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ShortTermNO" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="395" />
                     <JQTools:JQFormColumn Alignment="left" Caption="會辦總務" Editor="checkbox" FieldName="NeedGeneralAffairs" Width="80" NewRow="True" OnBlur="" maxlength="0" Span="1" />
                     <JQTools:JQFormColumn Alignment="left" Caption="請款備註" Editor="textarea" EditorOptions="height:40" FieldName="RequisitionNotes" Format="" Width="580" maxlength="512" NewRow="False" ReadOnly="False" RowSpan="1" span="3" Visible="True" />
                     <JQTools:JQFormColumn Alignment="left" Caption="受款人" Editor="inforefval" EditorOptions="title:'廠商與員工搜尋',panelWidth:540,panelHeight:240,remoteName:'sRequisitionQuery.Vendor',tableName:'Vendor',columns:[{field:'Employee_ID',title:'員工代號',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'VendName',title:'廠商/員工姓名',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'VendShortName',title:'廠商簡稱',width:160,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendShortName',valueFieldCaption:'VendID',textFieldCaption:'VendShortName',cacheRelationText:true,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="PayTo" Format="" Width="130" maxlength="0" Visible="True" />
                     <JQTools:JQFormColumn Alignment="left" Caption="付款方式" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sRequisitionQuery.PayType',tableName:'PayType',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" Format="" Width="133" Visible="True" Span="1" />
                     <JQTools:JQFormColumn Alignment="left" Caption="付款條件" Editor="infocombobox" FieldName="PayTermID" Width="165" Format="" EditorOptions="valueField:'PayTermID',textField:'PayTermName',remoteName:'sRequisitionQuery.PayTerm',tableName:'PayTerm',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" Visible="True" Span="1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1"/>
                     <JQTools:JQFormColumn Alignment="left" Caption="預付日期" Editor="datebox" FieldName="PlanPayDate" ReadOnly="False" Span="1" Visible="True" Width="133" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false" Format="yyyy/mm/dd" OnBlur="PlanPayDateOnBlur" />
                     <JQTools:JQFormColumn Alignment="left" Caption="緊急付款" Editor="checkbox" FieldName="IsUrgentPay" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                     <JQTools:JQFormColumn Alignment="left" Caption="非付款日付款" Editor="checkbox" FieldName="IsNotPayDate" Span="1" Width="80" Visible="True" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" />
                     <JQTools:JQFormColumn Alignment="left" Caption="付款備註" Editor="textarea" FieldName="PayToNotes" Format="" Visible="True" Width="580" ReadOnly="False" Span="3" EditorOptions="height:40" MaxLength="0" NewRow="False" RowSpan="1" />

                      <%--  <JQTools:JQFormColumn Alignment="left" Caption="請款單號" Editor="text" FieldName="RequisitionNO" Format="" maxlength="20" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="numberbox" FieldName="CompanyID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CompanyMame" Editor="text" FieldName="CompanyMame" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="text" FieldName="ApplyOrg_NO" Format="" maxlength="8" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="DeptName" Editor="text" FieldName="DeptName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenterID" Format="" maxlength="10" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CostCenterName" Editor="text" FieldName="CostCenterName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款事由" Editor="text" FieldName="RequisitionDescr" Format="" maxlength="50" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="RequisitionTypeID" Editor="numberbox" FieldName="RequisitionTypeID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ProofTypeID" Editor="numberbox" FieldName="ProofTypeID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ProofTypeName" Editor="text" FieldName="ProofTypeName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ProofNO" Editor="text" FieldName="ProofNO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayTermID" Editor="numberbox" FieldName="PayTermID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayTermName" Editor="text" FieldName="PayTermName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayTypeID" Editor="numberbox" FieldName="PayTypeID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayTypeName" Editor="text" FieldName="PayTypeName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="RequisitionNotes" Editor="text" FieldName="RequisitionNotes" Format="" maxlength="100" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayTo" Editor="text" FieldName="PayTo" Format="" maxlength="20" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayToName" Editor="text" FieldName="PayToName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayToNotes" Editor="text" FieldName="PayToNotes" Format="" maxlength="100" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="RequisitAmt" Editor="numberbox" FieldName="RequisitAmt" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsUrgentPay" Editor="text" FieldName="IsUrgentPay" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsNotPayDate" Editor="text" FieldName="IsNotPayDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermNO" Editor="text" FieldName="ShortTermNO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PlanPayDate" Editor="datebox" FieldName="PlanPayDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="RequistKindID" Editor="numberbox" FieldName="RequistKindID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsRemit" Editor="text" FieldName="IsRemit" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="RemitType" Editor="text" FieldName="RemitType" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Remit" Editor="numberbox" FieldName="Remit" Format="" Width="180" />--%>
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
