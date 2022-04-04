<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBCON_HOBBY.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        function dataGridMasterOnInserted() {
            $('#dataGridMaster').datagrid("reload");;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sCON_HOBBY.CON_SHARECODE" runat="server" AutoApply="True"
                DataMember="CON_SHARECODE" Pagination="True" QueryTitle="Query"
                Title="興趣設定" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="860px" OnInserted="dataGridMasterOnInserted">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="CODE_ID" Editor="numberbox" FieldName="CODE_ID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="FIELDNAME" Editor="text" FieldName="FIELDNAME" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="興趣名稱" Editor="text" FieldName="NAME" Format="" MaxLength="0" Width="230" />
                    <JQTools:JQGridColumn Alignment="center" Caption="排序" Editor="numberbox" FieldName="SORT" Format="" Width="55" />
                    <JQTools:JQGridColumn Alignment="center" Caption="顯示否" Editor="checkbox" FieldName="DISPLAY" Format="" MaxLength="0" Width="55" EditorOptions="on:'Y',off:'N'" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CREATE_MAN" Format="" MaxLength="0" Width="80" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Width="115" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Format="" MaxLength="0" Width="80" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Width="115" ReadOnly="True" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                    <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
    <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" EnableTheming="True" ID="defaultMaster" >
        <Columns>
            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="CODE_ID" RemoteMethod="True" />
            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="CONTACT_HOBBY" FieldName="FIELDNAME" RemoteMethod="True" />
            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="SORT" RemoteMethod="True" />
            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="Y" FieldName="DISPLAY" RemoteMethod="True" />
        </Columns>
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" EnableTheming="True" ID="validateMaster" >
    <Columns>
        <JQTools:JQValidateColumn CheckNull="True" FieldName="NAME" RemoteMethod="True" ValidateType="None" />
    </Columns>
</JQTools:JQValidate>
</form>
</body>
</html>
