<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMOrdersSet.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>
           
       
      </script> 
</head>
<body>
 <form id="form1" runat="server">
    <div id="cc" class="easyui-layout" style="width:845px;height:450px;">
       
         <div data-options="region:'west',title:'設定業務名單',split:true" style="width:430px;">  
            <div>
                        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
                        <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sFWCRMOrders.infoOrdersSalse" runat="server" AutoApply="True"
                            DataMember="infoOrdersSalse" Pagination="False" QueryTitle="Query"
                            Title="" AllowDelete="True" ViewCommandVisible="False" AllowAdd="True" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" Width="422px" Height="420px">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="center" Caption="員工姓名" Editor="inforefval" FieldName="EmpID" Format="" Width="171" EditorOptions="title:'請選擇員工',panelWidth:340,remoteName:'sFWCRMOrders.infoEmpID',tableName:'infoEmpID',columns:[],columnMatches:[],whereItems:[],valueField:'EMPLOYEE_CODE',textField:'NAME_C',valueFieldCaption:'員工工號',textFieldCaption:'員工姓名',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" />
                                <JQTools:JQGridColumn Alignment="center" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="100" ReadOnly="True" />
                                <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="100" ReadOnly="True" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                                <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                                <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                                <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                                <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                            </TooItems>
                            <QueryColumns>
                            </QueryColumns>
                        </JQTools:JQDataGrid>
                    </div>
            <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" EnableTheming="True" ID="defaultMaster" >
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                    </Columns>
            </JQTools:JQDefault>
            <JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" EnableTheming="True" ID="validateMaster" >
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EmpID" RemoteMethod="True" ValidateType="None" CheckMethod="" ValidateMessage="請選擇員工" />
                </Columns>
            </JQTools:JQValidate>

        </div>
        
        <div data-options="region:'center',title:'設定挑工進度全查詢人員'" >    
            <div>
                        <JQTools:JQDataGrid ID="dgERPDelayLunchEmp" data-options="pagination:true,view:commandview" RemoteName="sFWCRMOrders.infoSelectAdmin" runat="server" AutoApply="True"
                            DataMember="infoSelectAdmin" Pagination="False" QueryTitle="Query"
                            Title="" AllowDelete="True" ViewCommandVisible="False" AllowAdd="True" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" Width="405px" Height="420px">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="center" Caption="員工姓名" Editor="inforefval" FieldName="EmpID" Format="" Width="171" EditorOptions="title:'請選擇員工',panelWidth:340,remoteName:'sFWCRMOrders.infoEmpID',tableName:'infoEmpID',columns:[],columnMatches:[],whereItems:[],valueField:'EMPLOYEE_CODE',textField:'NAME_C',valueFieldCaption:'員工工號',textFieldCaption:'員工姓名',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" />
                                <JQTools:JQGridColumn Alignment="center" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="100" ReadOnly="True" />
                                <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="100" ReadOnly="True" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                                <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                                <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                                <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                                <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                            </TooItems>
                            <QueryColumns>
                            </QueryColumns>
                        </JQTools:JQDataGrid>
                    </div>
            <JQTools:JQDefault runat="server" BindingObjectID="dgERPDelayLunchEmp" EnableTheming="True" ID="JQDefault1" >
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                    </Columns>
            </JQTools:JQDefault>
            <JQTools:JQValidate runat="server" BindingObjectID="dgERPDelayLunchEmp" EnableTheming="True" ID="JQValidate1" >
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EmpID" RemoteMethod="True" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>

        </div>       
    </div>

</form>
</body>
</html>
