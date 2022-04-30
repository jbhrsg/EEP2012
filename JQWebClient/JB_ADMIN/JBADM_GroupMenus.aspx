<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBADM_GroupMenus.aspx.cs" Inherits="Template_JQuerySingle1" %>
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
        });
        var FunCount_FormatScript = function (value, row, index) {
            if (value > 0) {
                var fieldName = this.field;
                return $('<a>', { href: 'javascript: void(0)', title: '', onclick: "FunCount_CommandTrigger.call(this,'')" }).linkbutton({ iconCls: '', plain: false, text: value })[0].outerHTML;
            }
        }
        var FunCount_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
            var rowData = $("#dataGridView").datagrid('selectRow', rowIndex).datagrid('getSelected');
            var GROUPID = rowData.GROUPID;
            var FiltStr = 'A.GROUPID =' + "'" + GROUPID + "'";
            $("#JQDataGrid1").datagrid('setWhere', FiltStr);
            openForm('#JQDialog3', {}, "", 'dialog');
            return true;
        }
        function dataGridViewOnSelect(rowIndex, rowData) {
            //if (rowData != null && rowData != undefined) {
            //    var CenterName = rowData.MENUNAME + ' 角色群組列表';
            //    $("#JQDataGrid1").datagrid('getPanel').panel('setTitle', CenterName);
            //    $("#JQDataGrid1").datagrid('options').title = CenterName;
            //    var MENUID = rowData.MENUID;
            //    var FiltStr = 'MENUID =' + "'"+MENUID+"'";
            //    $("#JQDataGrid1").datagrid('setWhere', FiltStr);
            //}
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sSYSMenusGroups.GROUPS" runat="server" AutoApply="True"
                DataMember="GROUPS" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="角色群組-&gt;系統功能權限" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="20,40,60,80" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="455px" OnSelect="dataGridViewOnSelect">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="GROUPID" Editor="text" FieldName="GROUPID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="角色群組名稱" Editor="text" FieldName="GROUPNAME" Format="" MaxLength="0" Width="300" Visible="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="功能數" Editor="numberbox" FieldName="FUNCount" Format="" MaxLength="0" Width="55" Sortable="True" FormatScript="FunCount_FormatScript" />
                </Columns>
                <TooItems>
                   <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                 </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="系統功能對應角色功能權限">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="GROUPS" HorizontalColumnsCount="2" RemoteName="sSYSMenusGroups.GROUPS" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="GROUPID" Editor="text" FieldName="GROUPID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="角色群組名稱" Editor="text" FieldName="GROUPNAME" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="功能數" Editor="numberbox" FieldName="FUNCount" Format="" maxlength="0" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <br />
            <JQTools:JQDialog ID="JQDialog3" runat="server" DialogLeft="465px" DialogTop="8px" Title="系統功能列表" Width="460px" Closed="True" ShowSubmitDiv="False" DialogCenter="False" EditMode="Dialog" EnableTheming="True" ScrollBars="Vertical">
            <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="GROUPMENUS" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="20,40,60" PageSize="20" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sSYSMenusGroups.GROUPMENUS" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="390px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="MENUID" Editor="text" FieldName="A.MENUID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="功能選單名稱" Editor="text" FieldName="FunctionName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="360">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <TooItems>
                   <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                 </TooItems>
                </JQTools:JQDataGrid>
             </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
