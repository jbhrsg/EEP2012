<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON_Code_ActivityLevel.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>活動級別設定</title>
    <script>
        var name = "";
        var index;
        var insertFlag = false;
        var o_name = "";
        var flag = true;

        $(document).ready(function () {
            // 建立 dialog
            $("#Dialog_TransLog").dialog(
                {
                    height: 400,
                    width: 650,
                    resizable: false,
                    modal: true,
                    title: "活動級別異動資料紀錄",
                    closed: true,
                    buttons: [{
                        text: '結束',
                        handler: function () { $("#Dialog_TransLog").dialog("close") }
                    }]
                });
        });

        function checkInsert(rows) {
            insertFlag = true;
        }

        function checkUpdate(rows) {
            insertFlag = false;
            o_name = rows.NAME;
        }

        //datagrid inserted/updated
        function gridReload(rows) {
            if (rows) {
                name = rows.NAME;
                $("#dataGridMaster").datagrid('setWhere', "FIELDNAME = 'ACTIVITY_LEVEL'");
                var total = $("#dataGridMaster").datagrid('getData').total;  //取回資料總筆數
                var pageSize = $("#dataGridMaster").datagrid('options').pageSize;  //取回pagesize
                var pages = Math.ceil(total / pageSize);    //計算總頁數
                var gridRows = $('#dataGridMaster').datagrid('getRows');
                if (gridRows.length > pageSize) {
                    $('#dataGridMaster').datagrid("getPager").pagination('select', pages);
                    $("#dataGridMaster").datagrid('reload');
                }
            }
        }

        //datagrid onloadsuccess
        function getGridIndex() {
            if (flag) {
                $("#dataGridMaster").datagrid('setWhere', "FIELDNAME ='ACTIVITY_LEVEL'");
                flag = false;
            }
            if (name) {
                var gridRows = $('#dataGridMaster').datagrid('getRows');
                for (var i = gridRows.length - 1; i >= 0; i--) {
                    if (name == gridRows[i].NAME) {
                        index = $('#dataGridMaster').datagrid('getRowIndex', gridRows[i]);  // get the row index
                        break;
                    }
                }
                $("#dataGridMaster").datagrid('selectRow', index);
            }
        }

        //異動資料欄位超連結
        function HyperlinkLog(value, row, index) {
            return "<a href='javascript: void(0)' onclick='LinkLog(" + index + ");'>" + value + "</a>";
        }

        function LinkLog(index) {
            //alert(index)
            $("#dataGridMaster").datagrid('selectRow', index);
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var ID = rows.CODE_ID;
            $("#Dialog_TransLog").dialog("open");
            $("#DG_CON_SHARECODE_LOG").datagrid('setWhere', "CON_SHARECODE_LOG.CODE_ID = '" + ID + "'");
        }

        //判斷名稱(NAME)是否有重複
        function checkName(val) {
            if (insertFlag == true || o_name != val) {
                var name = val;
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_CON_SHARECODE.CON_SHARECODE', //連接的Server端，command
                    data: "mode=method&method=" + "checkName" + "&parameters=" + "ACTIVITY_LEVEL" + "," + name, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });
                if ((cnt != "0" && cnt != "undefined")) {
                    return false;
                }
                else
                    return true;
            }
            else
                return true;
        }

        //刪除前判斷parimay key 相對應之table 是否有資料
        function checkShareCodeRowCount(row) {
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_CON_System_Share.COLDEF', //連接的Server端，command
                data: "mode=method&method=" + "checkShareCodeRowCount" + "&parameters=" + 'ACTIVITY_EVALUATE' + "," + row.CODE_ID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        cnt = $.parseJSON(data);
                    }
                    else {
                        alert("此筆資料無法刪除");
                        return false;
                    }
                }
            });
            if ((cnt != "0" && cnt != "undefined")) {
                alert("此筆資料已有相關聯的使用資料, 無法刪除");
                return false;
            }
            else
                return true;
        }

        function genCheckBox(val) {
            if (val == 'Y')
                return "<input  type='checkbox' checked='true' />";
            else
                return "<input  type='checkbox' />";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="_CON_SHARECODE.CON_SHARECODE" runat="server" AutoApply="True"
                DataMember="CON_SHARECODE" Pagination="True" QueryTitle="查詢"
                Title="活動級別設定" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnInserted="gridReload" OnLoadSuccess="getGridIndex" OnUpdated="gridReload" OnInsert="checkInsert" OnUpdate="checkUpdate"  OnDelete="checkShareCodeRowCount">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="系統代碼流水號" Editor="numberbox" FieldName="CODE_ID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="系統代碼欄位名稱" Editor="text" FieldName="FIELDNAME" Format="" MaxLength="50" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動級別名稱" Editor="text" FieldName="NAME" Format="" MaxLength="200" Width="250" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption=" 排序" Editor="numberbox" FieldName="SORT" Format="" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="顯示否" Editor="checkbox" FieldName="DISPLAY" Format="" MaxLength="50" Width="60" EditorOptions="on:'Y',off:'N'" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" MaxLength="0" Width="60" ReadOnly="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Width="120" FormatScript="" ReadOnly="True" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" MaxLength="0" Width="60" ReadOnly="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Width="120" FormatScript="" ReadOnly="True" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動資料紀錄" Editor="text" FieldName="TRANSLOG" FormatScript="HyperlinkLog" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="True" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                    <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn Caption="活動身分別名稱" Condition="%%" DataType="string" Editor="text" FieldName="NAME" NewLine="True" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
        <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" EnableTheming="True" ID="defaultMaster">
            <Columns>
                <JQTools:JQDefaultColumn DefaultValue="1" FieldName="CODE_ID" RemoteMethod="True" />
                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CREATE_MAN" RemoteMethod="True" />
                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_sysdate" FieldName="CREATE_DATE" RemoteMethod="True" />
                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="UPDATE_MAN" RemoteMethod="True" />
                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_sysdate" FieldName="UPDATE_DATE" RemoteMethod="True" />
                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="Y" FieldName="DISPLAY" RemoteMethod="True" />
                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="ACTIVITY_LEVEL" FieldName="FIELDNAME" RemoteMethod="True" />
            </Columns>
        </JQTools:JQDefault>
        <JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="validateMaster" EnableViewState="True" ViewStateMode="Inherit">
            <Columns>
                <JQTools:JQValidateColumn CheckMethod="checkName" CheckNull="True" FieldName="NAME" RemoteMethod="False" ValidateType="None" ValidateMessage="此活動子類別名稱已存在" />
            </Columns>
        </JQTools:JQValidate>
        <!-- dialog對話框內容的 DIV -->
        <div id="Dialog_TransLog">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="DG_CON_SHARECODE_LOG" runat="server" RemoteName="_CON_SHARECODE.CON_SHARECODE_LOG" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="CON_SHARECODE_LOG" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="LOG_STATE_NAME" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="USERNAME" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="LOG_USER" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="False" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="異動日期" Editor="datebox" FieldName="LOG_DATE" Frozen="False" MaxLength="8" ReadOnly="False" Sortable="False" Visible="True" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                        <JQTools:JQGridColumn Alignment="left" Caption="活動級別名稱" Editor="text" FieldName="NAME" Format="" MaxLength="200" Width="180" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="排序" Editor="numberbox" FieldName="SORT" Format="" Width="40" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="顯示否" Editor="checkbox" FieldName="DISPLAY" Format="" MaxLength="50" Width="40" EditorOptions="on:'Y',off:'N'" FormatScript="genCheckBox"/>
                        <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" FormatScript="" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" Visible="True" />
                    </TooItems>
                </JQTools:JQDataGrid>
            </div>
        </div>
    </form>
</body>
</html>
