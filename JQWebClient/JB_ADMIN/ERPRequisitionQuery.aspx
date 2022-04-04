<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERPRequisitionQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        function dataGridViewOnLoadSucess() {
            var userid = getClientInfo("userid");
            $("#ApplyOrg_NO_Query").combobox('setWhere', "ApplyEmpID ='" + userid + "'");
            var DeptNO = $("#ApplyOrg_NO_Query").combobox('getValue');
            $("#ApplyEmpID_Query").combobox('setWhere', "ApplyOrg_NO ='" + DeptNO + "'");
            $("#AccountID_Query").combobox('setWhere', "ApplyOrg_NO ='" + DeptNO + "'");
        }
        function OnSelectApplyDept() {
            var DeptNO = $("#ApplyOrg_NO_Query").combobox('getValue');
            $("#ApplyEmpID_Query").combobox('setWhere', "1=2");
            $("#ApplyEmpID_Query").combobox('setWhere', "ApplyOrg_NO = '" + DeptNO + "'");
            $("#ApplyEmpID_Query").combobox('setWhere', "1=2");
            $("#AccountID_Query").combobox('setWhere', "ApplyOrg_NO = '" + DeptNO + "'");
        }
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            var ReturnStr = '';
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sRequisitionQuery.Requisition',
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID, 
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        ReturnStr = rows[0].OrgNO;
 
                    }
                }
            }
            );
            return ReturnStr
        }
      
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sRequisitionQuery.Requisition" runat="server" AutoApply="True"
                DataMember="Requisition" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="20,40,60,80,100" PageSize="20" QueryAutoColumn="False" QueryLeft="20px" QueryMode="Panel" QueryTop="30px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="1080px" OnLoadSuccess="dataGridViewOnLoadSucess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="請款單號" Editor="text" FieldName="RequisitionNO" Format="" MaxLength="20" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請人員" Editor="text" FieldName="ApplyName" MaxLength="0" Visible="true" Width="70" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="請款人員" Editor="text" EditorOptions="" FieldName="ApplyEmpID" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Visible="true" Width="70" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="請款事由" Editor="text" FieldName="RequisitionDescr" Format="" MaxLength="50" Visible="true" Width="400" />
                    <JQTools:JQGridColumn Alignment="left" Caption="會計科目" Editor="infocombobox" EditorOptions="valueField:'AcSubno',textField:'AcnoName',remoteName:'sRequisitionQuery.BudgetBase',tableName:'BudgetBase',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="請款金額" Editor="numberbox" FieldName="RequisitAmt" Format="N" Visible="true" Width="90" Total="sum" />
                    <JQTools:JQGridColumn Alignment="left" Caption="狀態" Editor="text" FieldName="FlowStatus" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                 <%--   <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />--%>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出EXCEL" />
                    <%--<JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="請款單號" Condition="%%" DataType="string" Editor="text" FieldName="RequisitionNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="&gt;=" DataType="datetime" DefaultValue="" Editor="datebox" FieldName="ApplyDate" Format="yyyy/MM/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="85" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止日期" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="ApplyDate" Format="yyyy/MM/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="85" DefaultValue="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請部門" Condition="=" DataType="string" DefaultValue="" Editor="infocombobox" EditorOptions="valueField:'Applyorg_no',textField:'DEPTNAME',remoteName:'sRequisitionQuery.ApplyDept',tableName:'ApplyDept',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectApplyDept,panelHeight:200" FieldName="ApplyOrg_NO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" DefaultMethod="GetUserOrgNOs" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請員工" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ApplyEmpID',textField:'ApplyerName',remoteName:'sRequisitionQuery.Applyer',tableName:'Applyer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyEmpID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="65" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="表單狀態" Condition="=" DataType="string" DefaultValue="Z" Editor="infocombobox" EditorOptions="valueField:'FLOWFLAG',textField:'FLOWFLAGNAME',remoteName:'sRequisitionQuery.ERPFlowFlag',tableName:'ERPFlowFlag',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Flowflag" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="70" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="請款資料" Width="790px" DialogLeft="70px" DialogTop="65px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Requisition" HorizontalColumnsCount="3" RemoteName="sRequisitionQuery.Requisition" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                     <Columns>
                     <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="text" FieldName="RequisitionNO" Format="" maxlength="0" Width="127"  ReadOnly="True" Span="1"/>
                     <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sRequisition.Company',tableName:'Company',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:250" FieldName="CompanyID" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                     <JQTools:JQFormColumn Alignment="left" Caption="請款性質" Editor="infocombobox" EditorOptions="valueField:'RequisitKindID',textField:'RequistKindName',remoteName:'sRequisition.RequistKind',tableName:'RequistKind',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="RequistKindID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                     <JQTools:JQFormColumn Alignment="left" Caption="申請員工" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sRequisitionQuery.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyEmpID" MaxLength="0" ReadOnly="False" Width="133" />
                     <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sRequisitionQuery.Organization',tableName:'Organization',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:250" FieldName="ApplyOrg_NO" Format="" ReadOnly="False" Width="130" Span="1" />
                     <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" ReadOnly="True" Width="90" Span="1" />
                     <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sRequisitionQuery.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:250" FieldName="CostCenterID" Format="" Span="1" Width="133" MaxLength="0" ReadOnly="True" />
                     <JQTools:JQFormColumn Alignment="left" Caption="科目類別" Editor="infocombobox" EditorOptions="valueField:'AccountType',textField:'AccountType',remoteName:'sRequisitionQuery.AccountType',tableName:'AccountType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountType" MaxLength="0" ReadOnly="False" Span="1" Width="130" NewRow="False" RowSpan="1" Visible="True" />
                     <JQTools:JQFormColumn Alignment="left" Caption="會計科目" Editor="infocombobox" EditorOptions="valueField:'AcSubno',textField:'AcnoName',remoteName:'sRequisitionQuery.BudgetBase',tableName:'BudgetBase',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountID" Span="1" Width="163" Visible="True" />
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
                     <JQTools:JQFormColumn Alignment="left" Caption="預付日期" Editor="datebox" FieldName="PlanPayDate" ReadOnly="False" Span="1" Visible="True" Width="133" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false" Format="yyyy/mm/dd" />
                     <JQTools:JQFormColumn Alignment="left" Caption="緊急付款" Editor="checkbox" FieldName="IsUrgentPay" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                     <JQTools:JQFormColumn Alignment="left" Caption="非付款日付款" Editor="checkbox" FieldName="IsNotPayDate" Span="1" Width="80" Visible="True" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" />
                     <JQTools:JQFormColumn Alignment="left" Caption="付款備註" Editor="textarea" FieldName="PayToNotes" Format="" Visible="True" Width="580" ReadOnly="False" Span="3" EditorOptions="height:40" MaxLength="0" NewRow="False" RowSpan="1" />
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
