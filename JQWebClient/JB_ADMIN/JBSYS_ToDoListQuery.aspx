<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBSYS_ToDoListQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
             var dgid = $('#dataGridView');
             var queryPanel = getInfolightOption(dgid).queryDialog;
             if (queryPanel)
                 $(queryPanel).panel('resize', { width: 430 });
             $('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });
         });
         function dataGridViewOnLoadSucess() {
             $('#D_STEP_ID_Query').combobox('setWhere', "FLOW_DESC=''");
             $('#APPLICANT_Query').combobox('setWhere', "EmployeeID=''");
             $('#AUDITOR_Query').combobox('setWhere', "AUDITOR=''");
         }
         function onSelectFLOW_DESC(rowData) {
             $('#D_STEP_ID_Query').combobox('setWhere', "FLOW_DESC='" + rowData.FLOW_DESC + "'");
             $('#APPLICANT_Query').combobox('setWhere', "FLOW_DESC='" + rowData.FLOW_DESC + "'");
             $('#AUDITOR_Query').combobox('setWhere', "FLOW_DESC='" + rowData.FLOW_DESC + "'");
         }

         
 </script>
</head>
<body style="margin-bottom: 19px">
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sSYS_TodoList.View_SYS_TODOLIST" runat="server" AutoApply="False"
                DataMember="View_SYS_TODOLIST" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="待辦事項查詢" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="20,30,40,50,60" PageSize="10" QueryAutoColumn="False" QueryLeft="50px" QueryMode="Window" QueryTop="50px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="1020px" OnLoadSuccess="dataGridViewOnLoadSucess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="流程名稱" Editor="text" FieldName="FLOW_DESC" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請單號" Editor="text" FieldName="BILLNO" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者" Editor="text" FieldName="APPLICANT" Format="" MaxLength="0" Width="50" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者" Editor="text" FieldName="EMPLOYEENAME" MaxLength="0" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請事由" Editor="text" FieldName="APPLYDESCR" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="350" />
                    <JQTools:JQGridColumn Alignment="left" Caption="S_STEP_ID" Editor="text" FieldName="S_STEP_ID" Format="" MaxLength="0" Width="90" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="目前活動" Editor="text" FieldName="D_STEP_ID" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="待辦者" Editor="text" FieldName="AUDITOR" Format="" MaxLength="0" Width="50" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="遞送者" Editor="text" FieldName="USERNAME" Format="" MaxLength="0" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="遞入時間" Editor="datebox" FieldName="UPDATEDATE" Format="yyyy/mm/dd HH:MM:SS" MaxLength="0" Width="120" Sortable="True" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="right" Caption="停留小時" Editor="numberbox" FieldName="HOURS" Format="" Width="55" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="STATUS" Editor="text" EditorOptions="" FieldName="STATUS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢條件" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="表單名稱" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'FLOW_DESC',textField:'FLOW_DESC',remoteName:'sSYS_TodoList.Flow_Desc',tableName:'Flow_Desc',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:onSelectFLOW_DESC,panelHeight:200" FieldName="FLOW_DESC" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="活動名稱" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'D_STEP_ID',textField:'D_STEP_ID',remoteName:'sSYS_TodoList.FlowStep',tableName:'FlowStep',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="D_STEP_ID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請者" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sSYS_TodoList.Applicant',tableName:'Applicant',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="APPLICANT" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="待辦者" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AUDITOR',textField:'AUDITOR',remoteName:'sSYS_TodoList.Auditor',tableName:'Auditor',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AUDITOR" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="表單狀態" Condition="=" DataType="string" DefaultValue="N" Editor="infocombobox" EditorOptions="items:[{value:'N',text:'流程中',selected:'false'},{value:'F',text:'已結案',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="STATUS" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="停留小時大於" Condition="&gt;=" DataType="number" Editor="text" FieldName="HOURS" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="121" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="待辦事項查詢" Width="720px" DialogLeft="50px" DialogTop="50px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="View_SYS_TODOLIST" HorizontalColumnsCount="3" RemoteName="sSYS_TodoList.View_SYS_TODOLIST" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="流程名稱" Editor="text" FieldName="FLOW_DESC" Format="" maxlength="0" Width="120" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請單號" Editor="text" FieldName="BILLNO" Format="" maxlength="0" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="text" FieldName="EMPLOYEENAME" maxlength="0" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請事由" Editor="textarea" EditorOptions="height:180" FieldName="APPLYDESCR" Format="" maxlength="0" Span="3" Width="544" />
                        <JQTools:JQFormColumn Alignment="left" Caption="目前活動" Editor="text" FieldName="D_STEP_ID" Format="" maxlength="0" Width="150" />
                        <JQTools:JQFormColumn Alignment="left" Caption="待辦者" Editor="text" FieldName="AUDITOR" Format="" maxlength="0" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="遞送者" Editor="text" FieldName="USERNAME" Format="" maxlength="0" Width="80" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="遞入時間" Editor="text" FieldName="UPDATEDATE" Format="" maxlength="0" Width="150" />
                        <JQTools:JQFormColumn Alignment="left" Caption="停留小時" Editor="numberbox" FieldName="HOURS" Format="" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="text" FieldName="APPLICANT" Format="" maxlength="0" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="S_STEP_ID" Editor="text" FieldName="S_STEP_ID" Format="" maxlength="0" Visible="False" Width="180" />
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
