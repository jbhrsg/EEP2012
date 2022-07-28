<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_Register_Users.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script type="text/javascript">
         $(function () {
            
         });

         $(document).ready(function () {

         
            
         });

        

         //-------------------CheckBox顯示---------------------------------------
         function genCheckBox(val) {
             if (val)
                 return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
             else
                 return "<input  type='checkbox'  onclick='return false;'  />";
         }

          function queryGrid(dg) {//查詢後添加固定條件
              if ($(dg).attr('id') == 'dataGridView') {
                  var result = [];
                  var MobilePhoneNumber = $('#MobilePhoneNumber_Query').val();//手機	   
                  var Email = $('#Email_Query').val();//Email
                  var LoginName = $('#LoginName_Query').val();//註冊名稱
                  var Name = $('#Name_Query').val();//姓名
                  var userValidate = $('#userValidate_Query').combobox('getValue');//有效
                  var hasFinValidate = $('#hasFin_Query').combobox('getValue');//有填

                  if (MobilePhoneNumber != '') result.push("MobilePhoneNumber like '%" + MobilePhoneNumber + "%'");
                  if (Email != '') result.push("Email like '%" + Email + "%'");
                  if (LoginName != '') result.push("LoginName like '%" + LoginName + "%'");
                  if (Name != '') result.push("Name like '%" + Name + "%'");
                  if (userValidate != '') result.push("Isnull(userValidate,0) = " + userValidate);
                  if (hasFinValidate != '') result.push("(select Isnull(hasFin,0) from userResumeTempStatus where userId=[User].Id) = " + hasFinValidate);

                  $(dg).datagrid('setWhere', result.join(' and '));
              }
          }

        

          function OnDeletedUser() {
              $("#dataGridView").datagrid('reload');
          }

     </script>




</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="_HRM_COMPANY_JOBFront.infoUsers" runat="server" AutoApply="True"
                DataMember="infoUsers" Pagination="True" QueryTitle="" EditDialogID="JQDialogUser"
                Title="會員帳號維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="950px" OnDeleted="OnDeletedUser">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="流水號" Editor="text" FieldName="Id" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="Name" Format="" MaxLength="250" Visible="true" Width="90" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="手機" Editor="text" FieldName="MobilePhoneNumber" Format="" Visible="true" Width="90" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Email" Editor="text" FieldName="Email" Format="" Visible="true" Width="220" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="註冊帳號" Editor="text" FieldName="LoginName" Format="" MaxLength="800" Visible="true" Width="90" ReadOnly="True" />
<%--                    <JQTools:JQGridColumn Alignment="left" Caption="密碼" Editor="text" FieldName="Password" Format="" MaxLength="2147483647" Visible="true" Width="130" ReadOnly="True" />--%>
                    <JQTools:JQGridColumn Alignment="center" Caption="履歷填寫" Editor="checkbox" FieldName="hasFin" Format="" Visible="true" Width="70" FormatScript="genCheckBox" />               
                     <JQTools:JQGridColumn Alignment="center" Caption="有效性" Editor="checkbox" FieldName="userValidate" Format="" Visible="true" Width="70" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="center" Caption="email認證" Editor="checkbox" FieldName="emailConfirmed" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="簡訊認證" Editor="checkbox" FieldName="smsConfirmed" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                  <%--  <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />--%>
<%--                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                  <JQTools:JQQueryColumn AndOr="and" Caption="手機" Condition="%" DataType="string" Editor="text" FieldName="MobilePhoneNumber" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="90" DefaultValue="" />
                  <JQTools:JQQueryColumn AndOr="and" Caption="Email" Condition="%" DataType="string" Editor="text" FieldName="Email" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="210" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="註冊帳號" Condition="%" DataType="string" Editor="text" FieldName="LoginName" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                  <JQTools:JQQueryColumn AndOr="and" Caption="姓名" Condition="%" DataType="string" Editor="text" FieldName="Name" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />               
                  <JQTools:JQQueryColumn AndOr="and" Caption="履歷填寫" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'不拘',selected:'false'},{value:'1',text:'有',selected:'false'},{value:'0',text:'無',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:100" FieldName="hasFin" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="60" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="狀態" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'不拘',selected:'false'},{value:'1',text:'有效',selected:'false'},{value:'0',text:'無效',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:100" FieldName="userValidate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="70" />

                </QueryColumns>
            </JQTools:JQDataGrid>

                        <JQTools:JQDialog ID="JQDialogUser" runat="server" BindingObjectID="DFUsers" DialogLeft="120px" DialogTop="80px" Title="資訊維護" Width="600px" ShowSubmitDiv="True">
                            <JQTools:JQDataForm ID="DFUsers" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="infoUsers" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="10" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="" RemoteName="_HRM_COMPANY_JOBFront.infoUsers" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="姓名 " Editor="text" FieldName="Name" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="註冊帳號" Editor="text" FieldName="LoginName" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="MobilePhoneNumber" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="Email" Editor="text" FieldName="Email" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="3" Visible="True" Width="420" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="有效性" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="userValidate" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="email認證" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="emailConfirmed" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="70" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="簡訊認證" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="smsConfirmed" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="70" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="Id" Editor="text" FieldName="Id" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                                </RelationColumns>
                            </JQTools:JQDataForm>
                            <JQTools:JQValidate ID="JQValidateUser" runat="server" BindingObjectID="DFUsers" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="Name" RemoteMethod="True" ValidateMessage="姓名不可空白！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="LoginName" RemoteMethod="True" ValidateMessage="註冊帳號不可空白！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="MobilePhoneNumber" RemoteMethod="True" ValidateMessage="手機不可空白！" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>
                        </JQTools:JQDialog>

        </div>
    </form>
</body>
</html>
