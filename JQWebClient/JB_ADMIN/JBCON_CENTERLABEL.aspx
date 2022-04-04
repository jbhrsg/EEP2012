<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBCON_CENTERLABEL.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            dataGridCenterFilt();
    
        });
        function dataGridCenterFilt() {
            var UserID = getClientInfo("UserID");
            var FiltStr = 'CENTER_ID  IN (SELECT DISTINCT CENTER_ID FROM CON_CENTER_AUTHORITY WHERE USERID=' + UserID + ')';
            $("#JQDataGrid1").datagrid('setWhere', FiltStr);
        }
        function JQDataGrid1OnSelect(rowIndex, rowData) {
            if (rowData != null && rowData != undefined) {
                var CenterName = rowData.CENTER_CNAME + ' 群組標簽列表';
                $("#dataGridMaster").datagrid('getPanel').panel('setTitle', CenterName);
                $("#dataGridMaster").datagrid('options').title = CenterName;
                var CenterID = rowData.CENTER_ID;
                var FiltStr = 'CENTER_ID =' + CenterID;
                $("#dataGridMaster").datagrid('setWhere', FiltStr);
            }
         }
        function GetCenterID() {
            var RowData = $("#JQDataGrid1").datagrid('getSelected');
            var CenterID = RowData.CENTER_ID;
            return CenterID
        }
      </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
         
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="GROUPTYPE" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sCON_CENTERLABEL.GROUPTYPE" RowNumbers="True" Title="人脈群組" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="550px" OnSelect="JQDataGrid1OnSelect">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="群組" Editor="text" EditorOptions="" FieldName="CENTER_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="擁有群組" Editor="text" FieldName="CENTER_CNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="420">
                    </JQTools:JQGridColumn>
                </Columns>
            </JQTools:JQDataGrid>
            <br />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sCON_CENTERLABEL.CON_CENTERLABEL" runat="server" AutoApply="True"
                DataMember="CON_CENTERLABEL" Pagination="True" QueryTitle="Query"
                Title="群組標簽" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="550px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="LABEL_ID" Editor="numberbox" FieldName="LABEL_ID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CENTER_ID" Editor="numberbox" FieldName="CENTER_ID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="群組標簽" Editor="text" FieldName="LABELNAME" Format="" MaxLength="0" Width="260" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CREATE_MAN" Format="" MaxLength="0" Width="70" Visible="True" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Width="110" Visible="True" ReadOnly="True" />
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
            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="LABEL_ID" RemoteMethod="True" />
            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="CENTER_ID" RemoteMethod="False" DefaultMethod="GetCenterID" />
        </Columns>
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" EnableTheming="True" ID="validateMaster" >
    <Columns>
        <JQTools:JQValidateColumn CheckNull="True" FieldName="LABELNAME" RemoteMethod="True" ValidateType="None" />
    </Columns>
</JQTools:JQValidate>
</form>
</body>
</html>
