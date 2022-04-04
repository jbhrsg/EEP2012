<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JCS_EmployeeJCS1.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
     <script type="text/javascript">
         $(document).ready(function () {
         });
         function genCheckBox(val) {
             if (val)
                 return "<input  type='checkbox' checked='true' />";
             else
                 return "<input  type='checkbox' />";
         };
         function dataFormMasterOnLoadSucess() {
             if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $('#dataFormMasterEmpID').focus();
             }
         }
         function CheckEmpID() {
             var EmpID = $("#dataFormMasterEmpID").val();
             if (getEditMode($("#dataFormMaster")) == 'inserted') {
                 var cnt;
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sJCS1EmpAttendance.Employee',
                     data: "mode=method&method=" + "CheckEmpID" + "&parameters=" + EmpID,
                     cache: false,
                     async: false,
                     success: function (data) {
                         if (data != false) {
                             cnt = $.parseJSON(data);
                         }
                     }
                 });
                 if ((cnt == "0") || (cnt == "undefined")) {
                     return true;
                 }
                 else {
                     alert('注意!!此代號已存在');
                     $('#dataFormMasterEmpID').val("");
                     $('#dataFormMasterEmpID').focus();
                     return false;
                 }
             }
             else return true;
         }
         function dataFormMasterOnApply() {
             var QDate = $("#dataFormMasterQuitDate").datebox('getValue');
             if (QDate != '2111/01/01') {
                $('#dataFormMasterIsActive').val(0);
             }
         }
         function IsActiveOnSelect() {
             var IsActive = $('#IsActive_Query').combobox('getValue');
             var FiltStr = 'IsActive = ' + IsActive;
             $("#EmpID_Query").combobox('setWhere', FiltStr);
         }
         function GetLastEmpID() {
             var UserID = getClientInfo("UserID");
             var EmpID = ''
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sJCS1EmpAttendance.Employee',
                 data: "mode=method&method=" + "GetLastEmpID" + "&parameters=" + UserID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     var rows = $.parseJSON(data);
                     if (rows.length > 0) {
                         EmpID = rows[0].EmpID;
                     }
                 }
             }
             );
             return EmpID;
         }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sJCS1EmpAttendance.Employee" runat="server" AutoApply="True"
                DataMember="Employee" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="幸福村員工出勤" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="20,40,60,120" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="員工代號" Editor="text" FieldName="EmpID" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="NameC" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="刷卡卡號" Editor="text" FieldName="CardNO" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="到職日期" Editor="datebox" FieldName="HireDate" Format="yyyy/mm/dd" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="離職日期" Editor="datebox" FieldName="QuitDate" Format="yyyy/mm/dd" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="有效紀錄" Editor="checkbox" FieldName="IsActive" Width="90" EditorOptions="on:1,off:0" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="出勤刷卡" Editor="text" FieldName="IsAtteAudit" Width="90" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" MaxLength="0" Width="90" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" Width="90" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem"
                        Text="刪除" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery"
                        Text="查詢" Visible="False"  />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="資料範圍" Condition="=" DataType="string" DefaultValue="1" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'在職',selected:'false'},{value:'0',text:'離職',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,onSelect:IsActiveOnSelect,panelHeight:200" FieldName="IsActive" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工姓名" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EMPID',textField:'NAMEC',remoteName:'sJCS1EmpAttendance.EMPList',tableName:'EMPList',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EmpID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="傑誠員工出勤" Width="480px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Employee" HorizontalColumnsCount="2" RemoteName="sJCS1EmpAttendance.Employee" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnLoadSuccess="dataFormMasterOnLoadSucess" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="dataFormMasterOnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="員工代號" Editor="text" FieldName="EmpID" Format="" maxlength="0" Width="120" OnBlur="CheckEmpID" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="text" FieldName="NameC" Format="" maxlength="0" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="刷卡卡號" Editor="text" FieldName="CardNO" Format="" maxlength="0" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="到職日期" Editor="datebox" FieldName="HireDate" Format="" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="離職日期" Editor="datebox" FieldName="QuitDate" Format="" Width="120" OnBlur="QuitDateOnBlur" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsActive" Editor="text" FieldName="IsActive" Format="" Width="120" Visible="False" />
                        <JQTools:JQFormColumn Alignment="center" Caption="出勤刷卡" Editor="checkbox" FieldName="IsAtteAudit" Format="" Width="120" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="120" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Width="120" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsAtteAudit" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="HireDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2111/01/01" FieldName="QuitDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsActive" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetLastEmpID" FieldName="EmpID" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="NameC" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
