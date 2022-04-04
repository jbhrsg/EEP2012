<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON_Code_Center.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jquery.blockUI.js"></script>
    <script src="../js/jquery.jbjob_wenetgroup.js"></script>
    <link href="../css/WENETGROUP/Dialog.css" rel="stylesheet" />
    <title>中心資料設定</title>
    <script>

        var CenterCname = "";
        var index;
        var insertFlag = false;
        var o_CenterCname = "";

        $(document).ready(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "#FFFFB5");
            });

            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });

            // 建立權限設定 dialog
            initAuthorityDialog();

              //-------------------------------欄位配對視窗送出按鈕------------------------------
            $('#DialogSubmit', '#Dialog_ImportMain').removeAttr('onclick').on('click', function () {
                if (!$('#DataForm_ImportMain').form('validateForm')) return;    //驗證                    
                var data = $('#DataForm_ImportMain').jbDataFormGetAFormData();  //取資料
                $.messager.progress({ msg: 'Loading...' });                 //進度條開始
                //送出上傳動作
                $.ajaxSetup({ async: false });
                $.post('../handler/JqDataHandle.ashx?RemoteName=_CON_Code_Center', {
                    mode: 'method', method: 'FileUpload', parameters: $.toJSONString(data)
                }).done(function (data) {
                    var Json = $.parseJSON(data);
                    if (Json.IsOK) {
                        $.messager.alert(' ', "匯入成功");
                        $('#Dialog_Import').dialog('close');
                        $('#Dialog_ImportMain').dialog('close');
                        $('#dataGridMaster').datagrid('reload');
                    }
                    else {
                        var html = Json.ErrorMsg;
                        if (Json.Result) {
                            var url = '../handler/JBHRISHandler.ashx?';
                            var querystrig = $.param({ mode: 'FileDownload', FilePathName: encodeURIComponent(Json.Result), DownloadName: encodeURIComponent('修正檔案') });
                            html = html + $('<a>', { href: url + querystrig, target: '_blank' }).html('檔案下載')[0].outerHTML;
                        }
                        $.messager.alert(' ', html);
                        $('#Dialog_ImportMain').dialog('close');
                    }
                }).fail(function (xhr, textStatus, errorThrown) {
                    alert('error');
                }).always(function () {
                    $.messager.progress('close');                           //進度條結束
                });
            });
            //-------------------------------讀取ExcelJquery----------------------------------
            $('#Dialog_Import').wenetImportExcel({
                OnGetTitleSuccess: function (ArrayData, FilePathName) {
                    //開啟配對視窗
                    openForm('#Dialog_ImportMain', { FilePathName: FilePathName }, 'inserted', 'Dialog');
                    //載入選項以及預設
                    $('#DataForm_ImportMain').find('.info-combobox').each(function () {
                        $(this).combobox('loadData', ArrayData).combobox('clear');
                        $(this).combobox('selectExistsForText', $(this).closest('td').prev('td').html());
                    });
                }
            });


            // 建立 dialog
            $("#Dialog_TransLog").dialog(
                {
                    height: 400,
                    width: 650,
                    resizable: false,
                    modal: true,
                    title: "中心異動資料紀錄",
                    closed: true,
                    buttons: [{
                        text: '結束',
                        handler: function () { $("#Dialog_TransLog").dialog("close") }
                    }]
                });
        })

        function checkInsert(rows) {
            insertFlag = true;
        }

        function checkUpdate(rows) {
            insertFlag = false;
            o_CenterCname = rows.CENTER_CNAME;
        }

        //datagrid inserted/updated
        function gridReload(rows) {
            if (rows) {
                CenterCname = rows.CENTER_CNAME;
                var total = $("#dataGridMaster").datagrid('getData').total;  //取回資料總筆數
                var pageSize = $("#dataGridMaster").datagrid('options').pageSize;  //取回pagesize
                var pages = Math.ceil(total / pageSize);    //計算總頁數
                var gridRows = $('#dataGridMaster').datagrid('getRows');
                if (gridRows.length > pageSize) {
                    $('#dataGridMaster').datagrid("getPager").pagination('select', pages);
                }
                $("#dataGridMaster").datagrid('reload');
            }
        }

        //datagrid onloadsuccess
        function getGridIndex() {
            if (CenterCname) {
                var gridRows = $('#dataGridMaster').datagrid('getRows');
                for (var i = gridRows.length - 1; i >= 0; i--) {
                    if (CenterCname == gridRows[i].CENTER_CNAME) {
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
            var ID = rows.CENTER_ID;
            $("#Dialog_TransLog").dialog("open");
            $("#DG_CON_CENTER_LOG").datagrid('setWhere', "CON_CENTER_LOG.CENTER_ID = '" + ID + "'");
        }

        //中心權限設定
        function AuthorityLink(value, row, index) {
            return $('<a>', { href: 'javascript:void(0)', name: 'AUTHORITY', onclick: 'LinkAuthority.call(this)', rowIndex: index }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '權限設定' })[0].outerHTML
        }

        function LinkAuthority() {
            //alert(index)
            var index = $(this).attr('rowIndex');
            $("#dataGridMaster").datagrid('selectRow', index);
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var ID = rows.CENTER_ID;
            openAuthorityDialog(ID);
           
            //$("#HRM_ATTEND_HOLIDAY_SALARY").datagrid('setWhere', "HRM_ATTEND_HOLIDAY_SALARY.HOLIDAY_ID = " + holidayID);
        }

        // 建立權限設定 dialog
        function initAuthorityDialog() {
            $("#Dialog_Authority").dialog(
            {
                height: 400,
                width: 400,
                resizable: false,
                modal: true,
                title: "權限設定",
                closed: true,
                buttons: [{
                    text: '取消',
                    handler: function () { $("#Dialog_Authority").dialog("close") }
                },
                {
                    text: "確認",
                    handler: function () {
                        var rows = $("#dataGridMaster").datagrid('getSelected');
                        var centerID = rows.CENTER_ID;
                        if (centerID) {
                            var userID = "";

                            var authorityRows = $("#DG_CenterAuthority").datagrid("getRows");
                            var checkedItems = $('#DG_CenterAuthority').datagrid('getChecked');
                            var flag;

                            for (var k = 0; k < authorityRows.length; k++) {
                                //判斷有勾選的 update 為 "Y"
                                flag = "N"
                                $.each(checkedItems, function (index, item) {
                                    if (authorityRows[k].USERID == item.USERID) {
                                        authorityRows[k].IS_SELECTED = "Y";
                                        flag = "Y";
                                        userID = userID + authorityRows[k].USERID + ",";
                                    }
                                });
                                if (flag != "Y")
                                    authorityRows[k].IS_SELECTED = "N";
                            }
                            $.ajax({
                                type: "POST",
                                url: '../handler/jqDataHandle.ashx?RemoteName=_CON_Code_Center.CON_CENTER', //連接的Server端，command
                                data: "mode=method&method=" + "updateCetnerAuthorityData" + "&parameters=" + centerID + "," + userID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                                cache: false,
                                async: false,
                                success: function (data) {
                                    if (data == "True") {
                                        $("#Dialog_Authority").dialog("close");
                                    }
                                    else {
                                        alert("權限設定失敗");
                                    }
                                }
                            }); //ajax
                        }   //if (centerID)
                    }
                }]
            });
        };

        // open權限查詢畫面 dialog
        function openAuthorityDialog(ID) {
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_CON_Code_Center.CON_CENTER', //連接的Server端，command
                data: "mode=method&method=" + "getAuthorityDialogData" + "&parameters=" + ID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                    if (rows.length > 0) {
                        //$('#DG_EMPLOYEE').datagrid('loadData', { "total": 0, "rows": [] });
                        $('#DG_CenterAuthority').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                        $('#DG_CenterAuthority').datagrid('uncheckAll');
                        for (var j = 0; j < rows.length; j++) {
                            if (rows[j].IS_SELECTED == "Y")
                                $('#DG_CenterAuthority').datagrid('checkRow', j);
                        }
                    }
                }
            });
            $("#Dialog_Authority").dialog("open");
        };

        //判斷中心資料名稱(CENTER_CNAME)是否有重複
        function checkCenterCname(val) {
            if (insertFlag == true || o_CenterCname != val) {
                var CenterCname = val;
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_CON_Code_Center.CON_CENTER', //連接的Server端，command
                    data: "mode=method&method=" + "checkCenterCname" + "&parameters=" + CenterCname, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
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
        function checkRowCount(row) {
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_CON_System_Share.COLDEF', //連接的Server端，command
                data: "mode=method&method=" + "checkRowCount" + "&parameters=" + 'CON_CENTER' + "," + row.CENTER_ID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
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


        function openImportExcel() {
            $("#Dialog_Import").dialog("open");
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
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="_CON_Code_Center.CON_CENTER" runat="server" AutoApply="True"
                DataMember="CON_CENTER" Pagination="True" QueryTitle="查詢"
                Title="中心資料設定" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnInserted="gridReload" OnLoadSuccess="getGridIndex" OnUpdated="gridReload" OnInsert="checkInsert" OnUpdate="checkUpdate" OnDelete="checkRowCount">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption=" " Editor="text" FieldName="CENTER_AUTHORITY" FormatScript="AuthorityLink" Sortable="False" Width="120" Frozen="True" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CENTER_ID" Editor="numberbox" FieldName="CENTER_ID" Width="80" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="中心中文名稱" Editor="text" FieldName="CENTER_CNAME" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="中心英文名稱" Editor="text" FieldName="CENTER_ENAME" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="200" />
                    <JQTools:JQGridColumn Alignment="center" Caption="排序" Editor="text" FieldName="CENTER_SEQ" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
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
                    <JQTools:JQToolItem Enabled="True" Icon="icon-importExcel" ItemType="easyui-linkbutton" OnClick="openImportExcel" Text="匯入EXCEL" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn Caption="中心中文名稱" Condition="%%" DataType="string" Editor="text" FieldName="CENTER_CNAME" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn Caption="中心英文名稱" Condition="%%" DataType="string" Editor="text" FieldName="CENTERK_ENAME" NewLine="True" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
        <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="defaultMaster" EnableViewState="True" ViewStateMode="Inherit">
            <Columns>
                <JQTools:JQDefaultColumn DefaultValue="1" FieldName="CENTER_ID" RemoteMethod="True" />
                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CREATE_MAN" RemoteMethod="True" />
                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_sysdate" FieldName="CREATE_DATE" RemoteMethod="True" />
                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="UPDATE_MAN" RemoteMethod="True" />
                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_sysdate" FieldName="UPDATE_DATE" RemoteMethod="True" />
            </Columns>
        </JQTools:JQDefault>
        <JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" EnableTheming="True" ID="validateMaster">
            <Columns>
                <JQTools:JQValidateColumn CheckMethod="checkCenterCname" CheckNull="True" RemoteMethod="False" ValidateMessage="此筆中心資料已存在" ValidateType="None" FieldName="CENTER_CNAME" />
            </Columns>
        </JQTools:JQValidate>
        <!-- dialog對話框內容的 DIV -->
        <div id="Dialog_TransLog">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="DG_CON_CENTER_LOG" runat="server" RemoteName="_CON_Code_Center.CON_CENTER_LOG" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="CON_CENTER_LOG" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="LOG_STATE_NAME" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="USERNAME" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                        <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="LOG_USER" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="False" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="異動日期" Editor="datebox" FieldName="LOG_DATE" Frozen="False" MaxLength="8" ReadOnly="False" Sortable="False" Visible="True" Width="90" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                        <JQTools:JQGridColumn Alignment="left" Caption="中心中文名稱" Editor="text" FieldName="CENTER_CNAME" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="中心英文名稱" Editor="text" FieldName="CENTER_ENAME" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="200" />
                        <JQTools:JQGridColumn Alignment="center" Caption="排序" Editor="text" FieldName="CENTER_SEQ" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" FormatScript="" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" Visible="True" />
                    </TooItems>
                </JQTools:JQDataGrid>
            </div>
        </div>

        <!-- 匯入對話框內容的 DIV -->
        <div id="Dialog_Import">

            <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog">
                <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" RemoteName=" " DataMember=" " HorizontalColumnsCount="3" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="檔案名稱" Editor="text" FieldName="FilePathName" Width="80" ReadOnly="true" Visible="false" />
                        <JQTools:JQFormColumn Alignment="left" Caption="中心中文名稱" Editor="infocombobox" FieldName="CENTER_CNAME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="中心英文名稱" Editor="infocombobox" FieldName="CENTER_ENAME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="排序" Editor="infocombobox" FieldName="CENTER_SEQ" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CENTER_CNAME" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>

         <!-- AUTHORITY權限 dialog對話框內容的 DIV -->
        <div id="Dialog_Authority">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="DG_CenterAuthority" runat="server" AutoApply="False" DataMember="USERS" Pagination="False" ParentObjectID="" RemoteName="_CON_Code_Center.USERS" Title="權限設定" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,30,40,50" PageSize="50" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="360px" CheckOnSelect="True" RecordLock="False" ColumnsHibeable="False" RecordLockMode="None" OnLoadSuccess="">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="USERID" Editor="text" FieldName="USERID" Width="90" Visible="False" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="使用者" Editor="text" FieldName="USERNAME" Width="150" ReadOnly="True" Frozen="False" MaxLength="4" QueryCondition="" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="已選擇" Editor="checkbox" FieldName="IS_SELECTED" Width="40" Frozen="False" MaxLength="60" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" EditorOptions="on:'Y',off:'N'" FormatScript="genCheckBox" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>
    </form>
</body>
</html>
