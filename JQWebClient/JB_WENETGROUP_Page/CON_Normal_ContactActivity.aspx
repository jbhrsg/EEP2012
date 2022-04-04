<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON_Normal_ContactActivity.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jquery.jbjob_wenetgroup.js"></script>
    <script src="../js/jquery.blockUI.js"></script>
    <link href="../css/WENETGROUP/Dialog.css" rel="stylesheet" />
    <title>活動記錄</title>

</head>
<body>
    <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
    <script type="text/javascript">
        var dataGridMaster_ID = "#dataGridMaster";
        var dataGridFile_ID = "#dataGrid_File";
        var dataGridGift_ID = "#dataGrid_Gift";
        var JQDialog1_ID = '#JQDialog1';

        var dataFormMaster_ID = '#dataFormMaster';
        var dataFormMasterCONTACT_ID = '#dataFormMasterCONTACT_ID';
        var dataFormMasterCONTACT_CELLPHONE = '#dataFormMasterCONTACT_CELLPHONE';

        var dataForm_File_ID = '#dataForm_File';
        var dataForm_FileCONTACT_ACTIVITY_ID = '#dataForm_FileCONTACT_ACTIVITY_ID';

        var dataForm_Gift_ID = '#dataForm_Gift';
        var dataForm_GiftCONTACT_ACTIVITY_ID = '#dataForm_GiftCONTACT_ACTIVITY_ID';

        var JQDialog1Log_ID = '#JQDialog1Log';
        var dataFormMasterLog_ID = '#dataFormMasterLog';
        var dataGridMasterLog_ID = '#dataGridMasterLog';

        var JQDialogFileLog_ID = '#JQDialogFileLog';
        var dataFormFileLog_ID = '#dataFormFileLog';
        var dataGridFileLog_ID = '#dataGrid_FileLog';

        var JQDialogGiftLog_ID = '#JQDialogGiftLog';
        var dataFormGiftLog_ID = '#dataFormGiftLog';
        var dataGridGiftLog_ID = '#dataGrid_GiftLog';

        var Dialog_Import_ID = '#Dialog_Import';
        var Dialog_ImportMain_ID = '#Dialog_ImportMain';
        var DataForm_ImportMain_ID = '#DataForm_ImportMain';

        //=======================================【Ready】=========================================
        $(function () {
            //-----------------------------------備註---------------------------------------------
            (function () {
                // 建立活動禮品 dialog
                initGiftDialog();
            })();
            //-----------------------------------LogDialog整形------------------------------------
            $(JQDialog1Log_ID).jbDialogPlugin();
            //-----------------------------------讀取ExcelJquery----------------------------------
            $(Dialog_Import_ID).wenetImportExcel({
                OnGetTitleSuccess: function (ArrayData, FilePathName) {
                    //開啟配對視窗
                    openForm(Dialog_ImportMain_ID, { FilePathName: FilePathName }, 'inserted', 'Dialog');
                    //載入選項以及預設
                    $(Dialog_ImportMain_ID).find('.info-combobox').each(function () {
                        $(this).combobox('loadData', ArrayData).combobox('clear');
                        $(this).combobox('selectExistsForText', $(this).closest('td').prev('td').html());
                    });
                }
            });
            //-----------------------------------欄位配對視窗送出按鈕-----------------------------
            $('#DialogSubmit', Dialog_ImportMain_ID).removeAttr('onclick').on('click', function () {
                if (!$(DataForm_ImportMain_ID).form('validateForm')) return;    //驗證                    
                var data = $(DataForm_ImportMain_ID).jbDataFormGetAFormData();  //取資料
                $.messager.progress({ msg: 'Loading...' });                     //進度條開始
                //送出上傳動作
                $.post('../handler/JqDataHandle.ashx?RemoteName=_CON_Normal_ContactActivity', {
                    mode: 'method', method: 'FileUpload', parameters: $.toJSONString(data)
                }).done(function (data) {
                    var Json = $.parseJSON(data);
                    if (Json.IsOK) {
                        $.messager.alert(' ', "匯入成功");
                        $(Dialog_Import_ID).dialog('close');
                        $(Dialog_ImportMain_ID).dialog('close');
                        $(dataGridMaster_ID).datagrid('reload');
                    }
                    else {
                        var html = Json.ErrorMsg;
                        if (Json.Result) {
                            var url = '../handler/JBHRISHandler.ashx?';
                            var querystrig = $.param({ mode: 'FileDownload', FilePathName: encodeURIComponent(Json.Result), DownloadName: encodeURIComponent('修正檔案') });
                            html = html + $('<a>', { href: url + querystrig, target: '_blank' }).html('檔案下載')[0].outerHTML;
                        }
                        $.messager.alert(' ', html);
                        $(Dialog_ImportMain_ID).dialog('close');
                    }
                }).fail(function (xhr, textStatus, errorThrown) {
                    alert('error');
                }).always(function () {
                    $.messager.progress('close');                           //進度條結束
                });
            });
        });

        // 活動禮品設定 dialog
        function initGiftDialog() {
            $("#Dialog_Gift").dialog(
            {
                height: 300,
                width: 900,
                left: 50,
                top:20,
                resizable: false,
                modal: true,
                title: "活動禮品設定",
                closed: true
             });

        };

        //=========================================================================================
        //---------------------------------------Form載入之後-------------------------------------
        var DataForm_OnLoadSuccess = function (RowData) {
            var defaultWhereStr = '';
            var theGrid = '';

            var thisDataForm = $(this);
            var form_ID = '#' + thisDataForm.attr('id');
            var ID = Request.getQueryStringByName("ID");

            switch (form_ID) {
                case dataFormMasterLog_ID:
                    defaultWhereStr = String.format("CONTACT_ACTIVITY_ID='{0}'", RowData.CONTACT_ACTIVITY_ID);
                    theGrid = dataGridMasterLog_ID;
                    break;
                case dataFormMaster_ID:
                    $(dataFormMasterCONTACT_CELLPHONE).refval('setWhere', "exists (select CENTER_ID from CON_CENTER_AUTHORITY where CENTER_ID = CENTER.CENTER_ID and USERID = '" + getClientInfo("UserID") + "' ) ");
                    if (ID) {
                        $(dataFormMasterCONTACT_CELLPHONE).refval('setValue', RowData.CONTACT_CELLPHONE);
                        $(dataFormMasterCONTACT_CELLPHONE).refval('disable');
                        $("#dataFormMasterCREATE_MAN").closest("tr").hide();
                        $("#dataFormMasterCREATE_DATE").closest("tr").hide();
                        $("#dataFormMasterUPDATE_MAN").closest("tr").hide();
                        $("#dataFormMasterUPDATE_DATE").closest("tr").hide();
                        $("#Div_dataGrid_File").hide();

                        if (getEditMode(thisDataForm) == "inserted") {
                            $.post(getRemoteUrl('_CON_SHARECODE.CON_CONTACT', 'CON_CONTACT'),
                               { queryWord: $.toJSONString({ whereString: String.format("CONTACT_ID='{0}'", ID) }) },
                               function (data) {
                                   var rowsData = $.parseJSON(data);
                                   $(dataFormMasterCONTACT_ID).val(ID);
                                   $(dataFormMasterCONTACT_CELLPHONE).refval('setValue', rowsData[0].CONTACT_CELLPHONE)
                               });
                        }
                    }
                    break;
                case dataFormFileLog_ID:
                    defaultWhereStr = String.format("ACTIVITY_FILE_ID='{0}'", RowData.ACTIVITY_FILE_ID);
                    theGrid = dataGridFileLog_ID;
                    break;
                case dataForm_File_ID:
                    var contactActivityID = $(dataGridMaster_ID).datagrid('getSelected').CONTACT_ACTIVITY_ID;
                    $(dataForm_FileCONTACT_ACTIVITY_ID).refval('setValue', contactActivityID);
                    $(dataForm_FileCONTACT_ACTIVITY_ID).refval('disable');
                case dataForm_Gift_ID:
                    var contactActivityID = $(dataGridMaster_ID).datagrid('getSelected').CONTACT_ACTIVITY_ID;
                    $(dataForm_GiftCONTACT_ACTIVITY_ID).refval('setValue', contactActivityID);
                    $(dataForm_GiftCONTACT_ACTIVITY_ID).refval('disable');
                case dataFormGiftLog_ID:
                    defaultWhereStr = String.format("ACTIVITY_GIFT_ID='{0}'", RowData.ACTIVITY_GIFT_ID);
                    theGrid = dataGridGiftLog_ID;
                    break;
                default:
                    break;
            }
            if (theGrid && defaultWhereStr) $(theGrid).data('defaultWhereStr', defaultWhereStr).datagrid("setWhere", defaultWhereStr);
        }
        //---------------------------------------Form存檔之後-------------------------------------
        var DataForm_OnApplied = function () {
            $(this).jbDataFormReloadDataGrid();
            $("#Div_dataGrid_File").show();
            $("#dataGrid_File").datagrid('reload');
        }

        //---------------------------------------Form取消之後-------------------------------------
        var DataForm_OnCancel = function () {
            $("#Div_dataGrid_File").show();
            $("#dataGrid_File").datagrid('reload');
        }
        //---------------------------------------Grid載入完成-------------------------------------
        var dataGridMaster_OnLoadSuccess = function () {
            var dgid = $(this);
           
            //第一次載入Grid用
            if (!dgid.data('alreadyFirstLoad') && dgid.data('alreadyFirstLoad', true)) {
                //首先判斷頁面
                var ID = Request.getQueryStringByName("ID");
                if (ID) {
                    var defaultWhereStr = String.format("CONTACT.CONTACT_ID='{0}'", ID);
                    $(this).data('defaultWhereStr', defaultWhereStr).datagrid("setWhere", defaultWhereStr);
                    $('#CONTACT_ID_Query').closest("tr").hide();
                }
                else {
                    //一般頁面則進行一般頁面之預先設定                    
                    //var pnid = getInfolightOption($(dgid)).queryDialog;
                    //if (pnid != undefined) {
                    //    clearQuery(dgid);
                    //    setQueryDefault(pnid);
                    //    $(dgid).datagrid('setWhere', $(dgid).datagrid('getWhere'));
                    //}
                    queryGrid(dgid);
                }

            }
        }
        //---------------------------------------聯絡人Grid 刪除前判斷parimay key 相對應之table 是否有資料-------------------------------------
        var dataGridMaster_OnDelete = function (row) {
            var dgid = $(this);
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_CON_System_Share.COLDEF', //連接的Server端，command
                data: "mode=method&method=" + "checkRowCount" + "&parameters=" + 'CON_CONTACT_ACTIVITY' + "," + row.CONTACT_ACTIVITY_ID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
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
        //---------------------------------------Grid欄位FormatScript-----------------------------
        var dataGridMaster_FormatScript = function (value, row, index) {
            var fieldName = this.field;
            switch (fieldName) {
                case 'TRANSLOG':
                    return $('<a>', { href: 'javascript: void(0)', onclick: "dataGridMaster_CommandTrigger.call(this,'" + fieldName + "')" }).
                            html('異動資料記錄')[0].outerHTML;
                    break;
                case 'TRANSLOG_FILE':
                    return $('<a>', { href: 'javascript: void(0)', onclick: "dataGridMaster_CommandTrigger.call(this,'" + fieldName + "')" }).
                            html('異動資料記錄')[0].outerHTML;
                    break;
                case 'TRANSLOG_GIFT':
                    return $('<a>', { href: 'javascript: void(0)', onclick: "dataGridMaster_CommandTrigger.call(this,'" + fieldName + "')" }).
                            html('異動資料記錄')[0].outerHTML;
                    break;
                default:
                    return '';
                    break;
            }
        }
        //---------------------------------------Grid欄位CommandTrigger---------------------------
        var dataGridMaster_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));

            switch (command) {
                case 'TRANSLOG':
                    var RowData = $(dataGridMaster_ID).datagrid('selectRow', rowIndex).datagrid('getSelected');
                    openForm(JQDialog1Log_ID, RowData, 'viewed', 'Dialog');
                    break;
                case 'TRANSLOG_FILE':
                    var RowData = $(dataGridFile_ID).datagrid('selectRow', rowIndex).datagrid('getSelected');
                    openForm(JQDialogFileLog_ID, RowData, 'viewed', 'Dialog');
                    break;
                case 'TRANSLOG_GIFT':
                    var RowData = $(dataGridGift_ID).datagrid('selectRow', rowIndex).datagrid('getSelected');
                    openForm(JQDialogGiftLog_ID, RowData, 'viewed', 'Dialog');
                    break;
                default:
                    break;
            }
        }
        //---------------------------------------改寫查詢-----------------------------------------
        function queryGrid(dg) {
            var where = $(dg).datagrid('getWhere');
            var userID = getClientInfo("UserID");
            if (where != "") where = where + "and ";
            where = where + "exists  (select CENTER_ID from CON_CENTER_AUTHORITY where CENTER_ID = CENTER.CENTER_ID and USERID = '" + userID + "' )";
            var defaultWhereStr = $(dg).data('defaultWhereStr');
            if (defaultWhereStr) where = where ? String.format(" {0} and {1} ", defaultWhereStr, where) : defaultWhereStr;
            $(dg).datagrid('setWhere', where);
        }
        //---------------------------------------改寫查詢-----------------------------------------
        function filterActivityFileData(rowIndex, rowData) {
            if (rowData != null && rowData != undefined) {
                var contactActivityID = rowData.CONTACT_ACTIVITY_ID;
                $(dataGrid_File).datagrid('setWhere', "A.CONTACT_ACTIVITY_ID = " + contactActivityID);
            }
        }

        //---------------------------------------匯入Excel----------------------------------------
        var openImportExcel = function () {
            $(Dialog_Import_ID).dialog('open');
        }
        //-----------------------------------------------------------------------------------------
        //判斷活動名稱(ACTIVITY_NAME)是否有重複
        function checkActivityName(val) {
            var o_ActivityName = ""
            var o_ContactID = ""
            var ActivityName = val;
            var ActivityName = $('#dataFormMasterACTIVITY_NAME').val();
            var ContactID = $('#dataFormMasterCONTACT_ID').refval('getValue');
            if ($("#dataGridMaster").datagrid('getSelected')) {
                o_ActivityName = $("#dataGridMaster").datagrid('getSelected').ACTIVITY_NAME;
                o_ContactID = $("#dataGridMaster").datagrid('getSelected').CONTACT_ID;
            }
            if (getEditMode($("#dataFormMaster")) == 'inserted' || o_ActivityName != ActivityName || o_ContactID != ContactID) {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_CON_Normal_ContactActivity.CON_CONTACT_ACTIVITY', //連接的Server端，command
                    data: "mode=method&method=" + "checkActivityName" + "&parameters=" + ContactID + "," + ActivityName, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
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
            //-----------------------------------------------------------------------------------------
        }

        //取得活動年度
        function getActivityYear() {
            var now = new Date();
            return now.getFullYear();
        }

        //check 活動年度格式如 : 2015 或 2016
        function checkYearFormat(val) {
            return $.jbIsYearStr(val);
        }

        //check 時間格式如 : 0800 或 0830
        function checkTimeFormat(val) {
            return $.jbIsTimeFormat_24(val);
        }

        //存檔前
        //1. 判斷活動起始日期不可大於截止日期
        //2. 判斷活動起始時間不可大於截止時間
        function checkActivityData() {
            var beginDate = $('#dataFormMasterBEGIN_DATE').datebox('getValue');
            var endDate = $('#dataFormMasterEND_DATE').datebox('getValue');
            var beginDateValidate = $.fn.datebox('parseDate', beginDate.replace(/\//g, '-'));
            var endDateValidate = $.fn.datebox('parseDate', endDate.replace(/\//g, '-'));
            var beginTime = $('#dataFormMasterBEGIN_TIME').val();
            var endTime = $('#dataFormMasterEND_TIME').val();

            //1. 判斷活動起始日期不可大於截止日期
            if (beginDateValidate == "Invalid Date" || !$.jbIsDateStr(beginDate)) {
                alert('活動起始日期:' + beginDate + '格式錯誤');
                $("#dataFormMasterBEGIN_DATE").datebox('textbox').focus();
                return false;
            }

            if (endDateValidate == "Invalid Date" || !$.jbIsDateStr(endDate)) {
                alert('活動截止日期:' + endDate + '格式錯誤');
                $("#dataFormMasterEND_DATE").datebox('textbox').focus();
                return false;
            }

            if (beginDate > endDate) {
                alert('活動起始日期 : ' + beginDate + ' 需小於活動截止日期 : ' + endDate);
                $("#dataFormMasterBEGIN_DATE").datebox('textbox').focus();
                return false;
            }

            //2. 判斷活動起始時間不可大於截止時間
            if (parseInt(beginTime) >= parseInt(endTime)) {
                alert('活動起始時間 : ' + beginTime + ' 需小於活動截止時間 : ' + endTime);
                return false;
            }
        }

        //判斷活動名稱(ACTIVITY_NAME)是否有重複
        function checkActivityFileName(val) {
            var o_ActivityFileName = ""
            var o_ContactActivityID = ""
            var ActivityFileName = val;
            var ContactActivityID = $('#dataForm_FileCONTACT_ACTIVITY_ID').refval('getValue');
            if ($("#dataGrid_File").datagrid('getSelected')) {
                o_ActivityFileName = $("#dataGrid_File").datagrid('getSelected').ACTIVIT_FILE;
                o_ContactActivityID = $("#dataGrid_File").datagrid('getSelected').CONTACT_ACTIVITY_ID;
            }
            if (getEditMode($("#dataForm_File")) == 'inserted' || o_ActivityFileName != ActivityFileName || o_ContactActivityID != ContactActivityID) {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_CON_Normal_ContactActivity.CON_CONTACT_ACTIVITY', //連接的Server端，command
                    data: "mode=method&method=" + "checkActivityFileName" + "&parameters=" + ContactActivityID + "," + ActivityFileName, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
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
            //-----------------------------------------------------------------------------------------
        }

        //活動禮品設定
        function GiftLink(value, row, index) {
            return $('<a>', { href: 'javascript:void(0)', name: 'GIFT', onclick: 'LinkGift.call(this)', rowIndex: index }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '活動禮品' })[0].outerHTML
        }

        // open活動禮品畫面 dialog
        function LinkGift() {
            //alert(index)
            var index = $(this).attr('rowIndex');
            $("#dataGridMaster").datagrid('selectRow', index);
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var ID = rows.CONTACT_ACTIVITY_ID;
            $(dataGrid_Gift).datagrid('setWhere', "CON_ACTIVITY_GIFT.CONTACT_ACTIVITY_ID = " + ID);
            $("#Dialog_Gift").dialog("open");
        }

        function dataGrid_Gift_Reload() {
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var ID = rows.CONTACT_ACTIVITY_ID;
            $(dataGrid_Gift).datagrid('setWhere', "CON_ACTIVITY_GIFT.CONTACT_ACTIVITY_ID = " + ID);
        }
 
    </script>
    <form id="form1" runat="server">
        <%--<JQTools:JQMultiLanguage ID="JQMultiLanguage1" runat="server" />--%>
        <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="_CON_Normal_ContactActivity.CON_CONTACT_ACTIVITY" runat="server" AutoApply="True"
            DataMember="CON_CONTACT_ACTIVITY" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
            Title="活動記錄" QueryLeft="100px" QueryTop="100px" OnLoadSuccess="dataGridMaster_OnLoadSuccess" AlwaysClose="True" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryMode="Window" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDelete="dataGridMaster_OnDelete" OnSelect="filterActivityFileData">
            <Columns>
                <JQTools:JQGridColumn Alignment="left" Caption=" " Editor="text" FieldName="CONTACT_GIFT" FormatScript="GiftLink" Sortable="False" Width="120" Frozen="True" ReadOnly="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動記錄流水號" Editor="text" FieldName="CONTACT_ACTIVITY_ID" Width="90" MaxLength="4" Visible="False" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="聯絡人流水號" Editor="text" FieldName="CONTACT_ID" Width="90" MaxLength="4" Visible="False" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="CONTACT_NAME" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90" MaxLength="0" />
                <JQTools:JQGridColumn Alignment="left" Caption="聯絡人電話" Editor="text" FieldName="CONTACT_CELLPHONE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動名稱" Editor="text" FieldName="ACTIVITY_NAME" Width="200" MaxLength="256" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動年度" Editor="text" FieldName="ACTIVITY_YEAR" Width="100" MaxLength="50" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動起始日期" Editor="datebox" FieldName="BEGIN_DATE" Width="90" MaxLength="8" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動截止日期" Editor="datebox" FieldName="END_DATE" Width="90" MaxLength="8" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動起始時間" Editor="text" FieldName="BEGIN_TIME" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90" MaxLength="8" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動截止時間" Editor="text" FieldName="END_TIME" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90" MaxLength="8" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動類型" Editor="text" FieldName="ACTIVITY_TYPE_ID" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="90" MaxLength="4" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動類型" Editor="text" FieldName="ACTIVITY_TYPE_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動身分別" Editor="text" FieldName="ACTIVITY_IDENTITY_ID" Frozen="False" ReadOnly="False" Sortable="True" Visible="False" Width="90" MaxLength="4" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動身分別" Editor="text" FieldName="ACTIVITY_IDENTITY_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動子類別" Editor="text" FieldName="ACTIVITY_CHILD_TYPE_ID" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="90" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動子類別" Editor="text" FieldName="ACTIVITY_CHILD_TYPE_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動級別" Editor="text" FieldName="ACTIVITY_LEVEL_ID" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="90" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動級別" Editor="text" FieldName="ACTIVITY_LEVEL_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動狀況" Editor="text" FieldName="ACTIVITY_STATUS_ID" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="90" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動狀況" Editor="text" FieldName="ACTIVITY_STATUS_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動評估" Editor="text" FieldName="ACTIVITY_EVALUATE_ID" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="90" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動評估" Editor="text" FieldName="ACTIVITY_EVALUATE_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動地點" Editor="text" FieldName="ACTIVITY_ADDR" Frozen="False" IsNvarChar="False" MaxLength="256" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="512" />
                <JQTools:JQGridColumn Alignment="left" Caption="活動經辦人" Editor="text" FieldName="ACTIVITY_PERSON" Frozen="False" IsNvarChar="False" MaxLength="50" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="100" />
                <JQTools:JQGridColumn Alignment="left" Caption="作品集" Editor="text" FieldName="ACTIVITY_WORKS" Frozen="False" IsNvarChar="False" MaxLength="2147483647" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="180" />
                <JQTools:JQGridColumn Alignment="left" Caption="其他說明" Editor="text" FieldName="ACTIVITY_DESCRIPTION" Frozen="False" IsNvarChar="False" MaxLength="2147483647" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="180" />
                <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                <JQTools:JQGridColumn Alignment="left" Caption="異動資料記錄" Editor="text" FieldName="TRANSLOG" FormatScript="dataGridMaster_FormatScript" Frozen="False" ReadOnly="True" Sortable="False" Visible="True" Width="80" />
            </Columns>
            <TooItems>
                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                <JQTools:JQToolItem Enabled="True" Icon="icon-importExcel" ItemType="easyui-linkbutton" OnClick="openImportExcel" Text="匯入EXCEL" Visible="True" />
            </TooItems>
            <QueryColumns>
                <JQTools:JQQueryColumn AndOr="and" Caption="聯絡人流水號" Condition="%%" DataType="string" Editor="inforefval" FieldName="CONTACT_ID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_CON_SHARECODE.CON_CONTACT',tableName:'CON_CONTACT',columns:[{field:'CENTER_CNAME',title:'中心名稱',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CONTACT_NAME',title:'聯絡人',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CONTACT_ID',textField:'CONTACT_NAME',valueFieldCaption:'聯絡人',textFieldCaption:'CONTACT_NAME',cacheRelationText:false,checkData:true,showValueAndText:false,selectOnly:false" TableName="A" />
                <JQTools:JQQueryColumn AndOr="and" Caption="活動名稱" Condition="%%" DataType="string" Editor="text" FieldName="ACTIVITY_NAME" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="活動年度" Condition="%%" DataType="string" Editor="text" FieldName="ACTIVITY_YEAR" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="活動起始日期" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="BEGIN_DATE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" DefaultValue="" Format="yyyy/MM/dd" />
                <JQTools:JQQueryColumn AndOr="and" Caption="活動截止日期" Condition="&lt;=" DataType="datetime" DefaultValue="" Editor="datebox" FieldName="END_DATE" Format="yyyy/MM/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="活動類型" Condition="%%" DataType="string" Editor="infocombobox" FieldName="ACTIVITY_TYPE_ID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" TableName="A" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.ACTIVITY_TYPE',tableName:'ACTIVITY_TYPE',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                <JQTools:JQQueryColumn AndOr="and" Caption="活動身分別" Condition="%%" DataType="string" Editor="infocombobox" FieldName="ACTIVITY_IDENTITY_ID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" TableName="A" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.ACTIVITY_IDENTITY',tableName:'ACTIVITY_IDENTITY',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                <JQTools:JQQueryColumn AndOr="and" Caption="活動子類別" Condition="%%" DataType="string" Editor="infocombobox" FieldName="ACTIVITY_CHILD_TYPE_ID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" TableName="A" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.ACTIVITY_CHILD_TYPE',tableName:'ACTIVITY_CHILD_TYPE',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                <JQTools:JQQueryColumn AndOr="and" Caption="活動級別" Condition="%%" DataType="string" Editor="infocombobox" FieldName="ACTIVITY_LEVEL_ID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" TableName="A" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.ACTIVITY_LEVEL',tableName:'ACTIVITY_LEVEL',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                <JQTools:JQQueryColumn AndOr="and" Caption="活動狀況" Condition="%%" DataType="string" Editor="infocombobox" FieldName="ACTIVITY_STATUS_ID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" TableName="A" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.ACTIVITY_STATUS',tableName:'ACTIVITY_STATUS',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                <JQTools:JQQueryColumn AndOr="and" Caption="活動評估" Condition="%%" DataType="string" Editor="infocombobox" FieldName="ACTIVITY_EVALUATE_ID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" TableName="A" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.ACTIVITY_EVALUATE',tableName:'ACTIVITY_EVALUATE',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                <JQTools:JQQueryColumn AndOr="and" Caption="活動經辦人" Condition="%%" DataType="string" Editor="text" FieldName="ACTIVITY_PERSON" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="作品集" Condition="%%" DataType="string" Editor="text" FieldName="ACTIVITY_WORKS" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
            </QueryColumns>
        </JQTools:JQDataGrid>
        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="活動記錄" DialogLeft="" DialogTop="" Width="1100px">
            <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CON_CONTACT_ACTIVITY" HorizontalColumnsCount="3" RemoteName="_CON_Normal_ContactActivity.CON_CONTACT_ACTIVITY" OnApplied="DataForm_OnApplied" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" OnLoadSuccess="DataForm_OnLoadSuccess" OnApply="checkActivityData" OnCancel="DataForm_OnCancel">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="活動記錄流水號" Editor="text" FieldName="CONTACT_ACTIVITY_ID" Visible="False" Width="180" MaxLength="4" />
                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="" FieldName="CONTACT_ID" Visible="False" Width="180" MaxLength="4" EditorOptions="" />
                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人電話" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:400,remoteName:'_CON_SHARECODE.CON_CONTACT',tableName:'CON_CONTACT',columns:[{field:'CENTER_CNAME',title:'中心名稱',width:150,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CONTACT_NAME',title:'聯絡人',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CONTACT_CELLPHONE',title:'手機',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'CONTACT_ID',value:'CONTACT_ID'}],whereItems:[],valueField:'CONTACT_CELLPHONE',textField:'CONTACT_NAME',valueFieldCaption:'手機',textFieldCaption:'聯絡人姓名',cacheRelationText:false,checkData:true,showValueAndText:false,selectOnly:false" FieldName="CONTACT_CELLPHONE" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動名稱" Editor="text" FieldName="ACTIVITY_NAME" Width="500" Visible="True" MaxLength="256" Span="2" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動年度" Editor="text" FieldName="ACTIVITY_YEAR" Width="180" MaxLength="50" Span="1" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動起始日期" Editor="datebox" FieldName="BEGIN_DATE" Width="180" NewRow="False" MaxLength="8" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動截止日期" Editor="datebox" FieldName="END_DATE" MaxLength="8" Width="180" ReadOnly="False" NewRow="False" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動起始時間" Editor="text" FieldName="BEGIN_TIME" Width="180" ReadOnly="False" MaxLength="8" NewRow="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動截止時間" Editor="text" FieldName="END_TIME" MaxLength="8" Width="180" ReadOnly="False" NewRow="False" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動類型" Editor="infocombobox" FieldName="ACTIVITY_TYPE_ID" Width="180" ReadOnly="False" MaxLength="4" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.ACTIVITY_TYPE',tableName:'ACTIVITY_TYPE',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動身分別" Editor="infocombobox" FieldName="ACTIVITY_IDENTITY_ID" MaxLength="4" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.ACTIVITY_IDENTITY',tableName:'ACTIVITY_IDENTITY',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動子類別" Editor="infocombobox" FieldName="ACTIVITY_CHILD_TYPE_ID" MaxLength="4" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.ACTIVITY_CHILD_TYPE',tableName:'ACTIVITY_CHILD_TYPE',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動級別" Editor="infocombobox" FieldName="ACTIVITY_LEVEL_ID" MaxLength="4" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.ACTIVITY_LEVEL',tableName:'ACTIVITY_LEVEL',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動狀況" Editor="infocombobox" FieldName="ACTIVITY_STATUS_ID" MaxLength="4" NewRow="False" ReadOnly="False" Width="180" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.ACTIVITY_STATUS',tableName:'ACTIVITY_STATUS',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動評估" Editor="infocombobox" FieldName="ACTIVITY_EVALUATE_ID" MaxLength="4" NewRow="False" ReadOnly="False" Width="180" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.ACTIVITY_EVALUATE',tableName:'ACTIVITY_EVALUATE',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動地點" Editor="text" FieldName="ACTIVITY_ADDR" MaxLength="256" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="480" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動經辦人" Editor="text" FieldName="ACTIVITY_PERSON" MaxLength="50" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                    <JQTools:JQFormColumn Alignment="left" Caption="作品集" Editor="textarea" FieldName="ACTIVITY_WORKS" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="820" />
                    <JQTools:JQFormColumn Alignment="left" Caption="其他說明" Editor="textarea" FieldName="ACTIVITY_DESCRIPTION" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="820" />
                    <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" MaxLength="50" Width="180" ReadOnly="True" NewRow="True" RowSpan="1" Span="1" Visible="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Width="180" ReadOnly="True" MaxLength="0" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" NewRow="False" Visible="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" MaxLength="50" Width="180" ReadOnly="True" NewRow="True" RowSpan="1" Span="1" Visible="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Width="180" ReadOnly="True" MaxLength="0" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                <Columns>
                    <JQTools:JQDefaultColumn FieldName="CONTACT_ACTIVITY_ID" DefaultValue="1" CarryOn="False" RemoteMethod="True" />
                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="BEGIN_DATE" RemoteMethod="True" />
                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="END_DATE" RemoteMethod="True" />
                    <JQTools:JQDefaultColumn DefaultMethod="getActivityYear" FieldName="ACTIVITY_YEAR" RemoteMethod="False" />
                </Columns>
            </JQTools:JQDefault>
            <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_ID" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckMethod="checkActivityName" CheckNull="True" RemoteMethod="False" ValidateMessage="此筆活動名稱資料已存在" ValidateType="None" FieldName="ACTIVITY_NAME" />
                    <JQTools:JQValidateColumn CheckMethod="checkYearFormat" CheckNull="True" FieldName="ACTIVITY_YEAR" RemoteMethod="False" ValidateMessage="請輸入正確的年度格式如 : 2015 或 2016" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="BEGIN_TIME" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="END_TIME" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>
        </JQTools:JQDialog>

        <JQTools:JQDialog ID="JQDialog1Log" runat="server" BindingObjectID="dataFormMasterLog" Title="一般註記異動資料記錄" ShowModal="True" EditMode="Dialog" Width="650px" DialogLeft="" DialogTop="">
            <div style="display: none;">
                <JQTools:JQDataForm ID="dataFormMasterLog" runat="server" DataMember="CON_CONTACT_ACTIVITY" RemoteName="_CON_Normal_ContactActivity.CON_CONTACT_ACTIVITY" OnLoadSuccess="DataForm_OnLoadSuccess">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="活動記錄ID" Editor="numberbox" FieldName="CONTACT_ACTIVITY_ID" Width="140" />
                    </Columns>
                </JQTools:JQDataForm>
            </div>
            <JQTools:JQDataGrid ID="dataGridMasterLog" data-options="pagination:true,view:commandview" runat="server" RemoteName="_CON_Normal_ContactActivity.CON_CONTACT_ACTIVITY_LOG" DataMember="CON_CONTACT_ACTIVITY_LOG" AutoApply="False" Pagination="True" EditDialogID=" "
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ReportFileName="" EditMode="Dialog">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="LOG_STATE_NAME" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="USERNAME" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="LOG_USER" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="False" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動日期" Editor="datebox" FieldName="LOG_DATE" Frozen="False" MaxLength="8" ReadOnly="False" Sortable="False" Visible="True" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動記錄流水號" Editor="text" FieldName="CONTACT_ACTIVITY_ID" Width="90" MaxLength="4" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人流水號" Editor="text" FieldName="CONTACT_ID" Width="90" MaxLength="4" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="CONTACT_NAME" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90" MaxLength="0" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動名稱" Editor="text" FieldName="ACTIVITY_NAME" Width="200" MaxLength="256" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動年度" Editor="text" FieldName="ACTIVITY_YEAR" Width="100" MaxLength="50" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動起始日期" Editor="datebox" FieldName="BEGIN_DATE" Width="90" MaxLength="8" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動截止日期" Editor="datebox" FieldName="END_DATE" Width="90" MaxLength="8" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動起始時間" Editor="text" FieldName="BEGIN_TIME" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90" MaxLength="8" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動截止時間" Editor="text" FieldName="END_TIME" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90" MaxLength="8" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動類型" Editor="text" FieldName="ACTIVITY_TYPE_ID" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="90" MaxLength="4" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動類型" Editor="text" FieldName="ACTIVITY_TYPE_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動身分別" Editor="text" FieldName="ACTIVITY_IDENTITY_ID" Frozen="False" ReadOnly="False" Sortable="True" Visible="False" Width="90" MaxLength="4" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動身分別" Editor="text" FieldName="ACTIVITY_IDENTITY_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動子類別" Editor="text" FieldName="ACTIVITY_CHILD_TYPE_ID" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動子類別" Editor="text" FieldName="ACTIVITY_CHILD_TYPE_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動級別" Editor="text" FieldName="ACTIVITY_LEVEL_ID" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動級別" Editor="text" FieldName="ACTIVITY_LEVEL_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動狀況" Editor="text" FieldName="ACTIVITY_STATUS_ID" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動狀況" Editor="text" FieldName="ACTIVITY_STATUS_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動評估" Editor="text" FieldName="ACTIVITY_EVALUATE_ID" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動評估" Editor="text" FieldName="ACTIVITY_EVALUATE_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動地點" Editor="text" FieldName="ACTIVITY_ADDR" Frozen="False" IsNvarChar="False" MaxLength="256" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="512" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動經辦人" Editor="text" FieldName="ACTIVITY_PERSON" Frozen="False" IsNvarChar="False" MaxLength="50" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="作品集" Editor="text" FieldName="ACTIVITY_WORKS" Frozen="False" IsNvarChar="False" MaxLength="2147483647" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="其他說明" Editor="text" FieldName="ACTIVITY_DESCRIPTION" Frozen="False" IsNvarChar="False" MaxLength="2147483647" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                </TooItems>
            </JQTools:JQDataGrid>
        </JQTools:JQDialog>

        <div id="Div_dataGrid_File">
            <JQTools:JQDataGrid ID="dataGrid_File" data-options="pagination:true,view:commandview" RemoteName="_CON_Normal_ContactActivity.CON_ACTIVITY_FILE" runat="server" AutoApply="True"
                DataMember="CON_ACTIVITY_FILE" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog_File"
                Title="上傳附件" QueryLeft="300px" QueryTop="100px" AlwaysClose="True" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryMode="Window" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="1020px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="上傳附件流水號" Editor="text" FieldName="ACTIVITY_FILE_ID" Width="90" MaxLength="4" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動流水號" Editor="text" FieldName="CONTACT_ACTIVITY_ID" Width="90" MaxLength="4" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動名稱" Editor="text" FieldName="ACTIVITY_NAME" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="上傳附件" Editor="text" FieldName="ACTIVIT_FILE" MaxLength="200" Width="180" Format="Download,Folder:Files/WENETGROUP/ACTIVITY" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動資料記錄" Editor="text" FieldName="TRANSLOG_FILE" FormatScript="dataGridMaster_FormatScript" Frozen="False" ReadOnly="True" Sortable="False" Visible="True" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="活動名稱" Condition="%%" DataType="string" Editor="inforefval" FieldName="CONTACT_ACTIVITY_ID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_CON_SHARECODE.CON_CONTACT_ACTIVITY',tableName:'CON_CONTACT_ACTIVITY',columns:[{field:'ACTIVITY_NAME',title:'活動名稱',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ACTIVITY_NAM',title:'活動名稱',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CONTACT_ACTIVITY_ID',textField:'ACTIVITY_NAME',valueFieldCaption:'活動名稱',textFieldCaption:'ACTIVITY_NAME',cacheRelationText:false,checkData:true,showValueAndText:false,selectOnly:false" TableName="A" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="上傳附件" Condition="%%" DataType="string" Editor="text" FieldName="ACTIVIT_FILE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
        <JQTools:JQDialog ID="JQDialog_File" runat="server" BindingObjectID="dataForm_File" Title="活動附件" DialogLeft="" DialogTop="">
            <JQTools:JQDataForm ID="dataForm_File" runat="server" DataMember="CON_ACTIVITY_FILE" HorizontalColumnsCount="2" RemoteName="_CON_Normal_ContactActivity.CON_ACTIVITY_FILE" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" OnLoadSuccess="DataForm_OnLoadSuccess">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="上傳附件流水號" Editor="text" FieldName="ACTIVITY_FILE_ID" Visible="False" Width="180" MaxLength="4" />
                    <JQTools:JQFormColumn Alignment="left" Caption="活動名稱" Editor="inforefval" FieldName="CONTACT_ACTIVITY_ID" Visible="True" Width="460" MaxLength="4" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_CON_SHARECODE.CON_CONTACT_ACTIVITY',tableName:'CON_CONTACT_ACTIVITY',columns:[{field:'CONTACT_NAME',title:'聯絡人',width:180,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ACTIVITY_NAME',title:'活動名稱',width:180,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CONTACT_ACTIVITY_ID',textField:'ACTIVITY_NAME',valueFieldCaption:'活動記錄流水號',textFieldCaption:'活動名稱',cacheRelationText:false,checkData:true,showValueAndText:false,selectOnly:false" Span="2" />
                    <JQTools:JQFormColumn Alignment="left" Caption="上傳附件" Editor="infofileupload" FieldName="ACTIVIT_FILE" MaxLength="200" Width="460" Span="2" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'Files/WENETGROUP/ACTIVITY',showButton:true,showLocalFile:false" />
                    <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" MaxLength="50" Width="180" ReadOnly="True" NewRow="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Width="180" ReadOnly="True" MaxLength="0" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                    <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" MaxLength="50" Width="180" ReadOnly="True" NewRow="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Width="180" ReadOnly="True" MaxLength="0" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDefault ID="defaultMasterFile" runat="server" BindingObjectID="dataForm_File" EnableTheming="True">
                <Columns>
                    <JQTools:JQDefaultColumn FieldName="ACTIVITY_FILE_ID" DefaultValue="1" />
                </Columns>
            </JQTools:JQDefault>
            <JQTools:JQValidate ID="validateMasterFile" runat="server" BindingObjectID="dataForm_File" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn FieldName="CONTACT_ACTIVITY_ID" CheckNull="true" />
                    <JQTools:JQValidateColumn CheckMethod="checkActivityFileName" CheckNull="True" RemoteMethod="False" ValidateMessage="此筆活動附加檔案名稱已存在" ValidateType="None" FieldName="ACTIVIT_FILE" />
                </Columns>
            </JQTools:JQValidate>
        </JQTools:JQDialog>


        <JQTools:JQDialog ID="JQDialogFileLog" runat="server" BindingObjectID="dataFormFileLog" Title="上傳附件異動資料記錄" ShowModal="True" EditMode="Dialog" Width="800px" DialogLeft="" DialogTop="">
            <div style="display: none;">
                <JQTools:JQDataForm ID="dataFormFileLog" runat="server" DataMember="CON_ACTIVITY_FILE" RemoteName="_CON_Normal_ContactActivity.CON_ACTIVITY_FILE" OnLoadSuccess="DataForm_OnLoadSuccess">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="上傳檔案ID" Editor="numberbox" FieldName="ACTIVITY_FILE_ID" Width="140" />
                    </Columns>
                </JQTools:JQDataForm>
            </div>
            <JQTools:JQDataGrid ID="dataGrid_FileLog" data-options="pagination:true,view:commandview" runat="server" RemoteName="_CON_Normal_ContactActivity.CON_ACTIVITY_FILE_LOG" DataMember="CON_ACTIVITY_FILE_LOG" AutoApply="False" Pagination="True" EditDialogID=" "
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ReportFileName="" EditMode="Dialog">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="LOG_STATE_NAME" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="USERNAME" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="LOG_USER" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="False" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動日期" Editor="datebox" FieldName="LOG_DATE" Frozen="False" MaxLength="8" ReadOnly="False" Sortable="False" Visible="True" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="ACTIVITY_NAME" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="上傳檔案" Editor="text" FieldName="ACTIVIT_FILE" MaxLength="200" Width="180" Format="Download,Folder:Files/WENETGROUP/CONTACT" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                </TooItems>
            </JQTools:JQDataGrid>
        </JQTools:JQDialog>

        <!-- 匯入對話框內容的 DIV -->
        <div id="Dialog_Import">

            <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog" DialogTop="50px">
                <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" DataMember=" " HorizontalColumnsCount="3" RemoteName=" " Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="檔案名稱" Editor="text" FieldName="FilePathName" Width="80" ReadOnly="true" Visible="false" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人手機" Editor="infocombobox" FieldName="CONTACT_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動名稱" Editor="infocombobox" FieldName="ACTIVITY_NAME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動年度" Editor="infocombobox" FieldName="ACTIVITY_YEAR" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動起始日期" Editor="infocombobox" FieldName="BEGIN_DATE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動截止日期" Editor="infocombobox" FieldName="END_DATE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動起始時間" Editor="infocombobox" FieldName="BEGIN_TIME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動截止時間" Editor="infocombobox" FieldName="END_TIME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動類型" Editor="infocombobox" FieldName="ACTIVITY_TYPE_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動身分別" Editor="infocombobox" FieldName="ACTIVITY_IDENTITY_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動子類別" Editor="infocombobox" FieldName="ACTIVITY_CHILD_TYPE_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動級別" Editor="infocombobox" FieldName="ACTIVITY_LEVEL_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動狀況" Editor="infocombobox" FieldName="ACTIVITY_STATUS_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動評估" Editor="infocombobox" FieldName="ACTIVITY_EVALUATE_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動地點" Editor="infocombobox" FieldName="ACTIVITY_ADDR" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動經辦人" Editor="infocombobox" FieldName="ACTIVITY_PERSON" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="作品集" Editor="infocombobox" FieldName="ACTIVITY_WORKS" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="其他說明" Editor="infocombobox" FieldName="ACTIVITY_DESCRIPTION" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_ID" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ACTIVITY_NAME" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>

        <!-- GIFT活動禮品 dialog對話框內容的 DIV -->
        <div id="Dialog_Gift">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="dataGrid_Gift" data-options="pagination:true,view:commandview" RemoteName="_CON_Normal_ContactActivity.CON_ACTIVITY_GIFT" runat="server" AutoApply="True"
                    DataMember="CON_ACTIVITY_GIFT" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog_Gift"
                    Title="" QueryLeft="300px" QueryTop="100px" AlwaysClose="True" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryMode="Window" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="1020px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="活動禮品紀錄流水號" Editor="text" FieldName="ACTIVITY_GIFT_ID" Width="90" MaxLength="0" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="活動記錄流水號" Editor="text" FieldName="CONTACT_ACTIVITY_ID" Width="90" MaxLength="0" Visible="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="活動名稱" Editor="text" FieldName="ACTIVITY_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                        <JQTools:JQGridColumn Alignment="left" Caption="禮品等級代碼" Editor="text" FieldName="GIFT_LEVEL_ID" Width="90" Sortable="False" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="禮品等級" Editor="text" FieldName="GIFT_LEVELE_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="品名" Editor="text" FieldName="GIFT_NAME" MaxLength="0" Width="180" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="容量價位" Editor="text" FieldName="GIFT_PRICE" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                        <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="GIFT_MEMO" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="異動資料記錄" Editor="text" FieldName="TRANSLOG_GIFT" FormatScript="dataGridMaster_FormatScript" Frozen="False" ReadOnly="True" Sortable="False" Visible="True" Width="80" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                        <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                        <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" Visible="False" />
                        <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                    </TooItems>
                    <QueryColumns>
                    </QueryColumns>
                </JQTools:JQDataGrid>
            </div>
            <JQTools:JQDialog ID="JQDialog_Gift" runat="server" BindingObjectID="dataForm_Gift" Title="" DialogLeft="" DialogTop="" Width="650px">
                <JQTools:JQDataForm ID="dataForm_Gift" runat="server" DataMember="CON_ACTIVITY_GIFT" HorizontalColumnsCount="2" RemoteName="_CON_Normal_ContactActivity.CON_ACTIVITY_GIFT" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" OnLoadSuccess="DataForm_OnLoadSuccess" OnApplied="dataGrid_Gift_Reload">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="活動禮品紀錄流水號" Editor="text" FieldName="ACTIVITY_GIFT_ID" MaxLength="0" Visible="False" Width="80" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動名稱" Editor="inforefval" FieldName="CONTACT_ACTIVITY_ID" MaxLength="0" Span="1" Visible="True" Width="180" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_CON_SHARECODE.CON_CONTACT_ACTIVITY',tableName:'CON_CONTACT_ACTIVITY',columns:[{field:'CONTACT_NAME',title:'聯絡人',width:180,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ACTIVITY_NAME',title:'活動名稱',width:180,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CONTACT_ACTIVITY_ID',textField:'ACTIVITY_NAME',valueFieldCaption:'活動記錄流水號',textFieldCaption:'活動名稱',cacheRelationText:false,checkData:true,showValueAndText:false,selectOnly:false" />
                        <JQTools:JQFormColumn Alignment="left" Caption="禮品代號" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_CON_SHARECODE.CON_GIFT',tableName:'CON_GIFT',columns:[],columnMatches:[{field:'GIFT_LEVEL_ID',value:'GIFT_LEVEL_ID'},{field:'GIFT_NAME',value:'GIFT_NAME'},{field:'GIFT_PRICE',value:'GIFT_PRICE'},{field:'GIFT_MEMO',value:'GIFT_MEMO'}],whereItems:[],valueField:'GIFT_CODE',textField:'GIFT_NAME',valueFieldCaption:'禮品代碼',textFieldCaption:'品名',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="GIFT_CODE" MaxLength="0" Visible="True" Width="180" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="禮品等級代碼" Editor="infocombobox" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.GIFT_LEVEL',tableName:'GIFT_LEVEL',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="GIFT_LEVEL_ID" MaxLength="0" Span="1" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="品名" Editor="text" FieldName="GIFT_NAME" MaxLength="0" NewRow="False" ReadOnly="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="容量價位" Editor="text" FieldName="GIFT_PRICE" MaxLength="0" ReadOnly="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="GIFT_MEMO" MaxLength="0" NewRow="False" ReadOnly="False" Span="2" Width="460" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" MaxLength="50" NewRow="True" ReadOnly="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:mm:SS" MaxLength="0" ReadOnly="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" MaxLength="50" NewRow="True" ReadOnly="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:mm:SS" MaxLength="0" ReadOnly="True" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="JQDefaultGift" runat="server" BindingObjectID="dataForm_Gift" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn FieldName="ACTIVITY_GIFT_ID" DefaultValue="1" CarryOn="False" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidateGift" runat="server" BindingObjectID="dataForm_Gift" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="GIFT_LEVEL_ID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="GIFT_NAME" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn FieldName="GIFT_PRICE" CheckNull="true" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="GIFT_CODE" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="JQDialogGiftLog" runat="server" BindingObjectID="dataFormGiftLog" Title="活動禮品異動資料記錄" ShowModal="True" EditMode="Dialog" Width="800px" DialogLeft="" DialogTop="">
            <div style="display: none;">
                <JQTools:JQDataForm ID="dataFormGiftLog" runat="server" DataMember="CON_ACTIVITY_GIFT" RemoteName="_CON_Normal_ContactActivity.CON_ACTIVITY_GIFT" OnLoadSuccess="DataForm_OnLoadSuccess">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="活動禮品ID" Editor="numberbox" FieldName="ACTIVITY_GIFT_ID" Width="140" />
                    </Columns>
                </JQTools:JQDataForm>
            </div>
            <JQTools:JQDataGrid ID="dataGrid_GiftLog" data-options="pagination:true,view:commandview" runat="server" RemoteName="_CON_Normal_ContactActivity.CON_ACTIVITY_GIFT_LOG" DataMember="CON_ACTIVITY_GIFT_LOG" AutoApply="False" Pagination="True" EditDialogID=" "
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ReportFileName="" EditMode="Dialog" QueryTitle="Query">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="LOG_STATE_NAME" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="USERNAME" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="LOG_USER" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="False" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動日期" Editor="datebox" FieldName="LOG_DATE" Frozen="False" MaxLength="8" ReadOnly="False" Sortable="False" Visible="True" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="ACTIVITY_NAME" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="品名" Editor="text" FieldName="GIFT_NAME" MaxLength="200" Width="180" Format="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="禮品等級" Editor="text" FieldName="GIFT_LEVEL_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="容量價位" Editor="text" FieldName="GIFT_PRICE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="GIFT_MEMO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                </TooItems>
            </JQTools:JQDataGrid>
        </JQTools:JQDialog>
        </div>

        <script type="text/javascript">
            var dataFormMasterADDR_ID = '#dataFormMasterACTIVITY_ADDR';

            var Dialog_AddrKeyIn_ID = '#Dialog_AddrKeyIn';

            var DataForm_AddrKeyIn_Combo_ID = '#DataForm_AddrKeyIn_Combo';
            var DataForm_AddrKeyIn_ComboCity_ID = DataForm_AddrKeyIn_Combo_ID + 'City';
            var DataForm_AddrKeyIn_ComboCountry_ID = DataForm_AddrKeyIn_Combo_ID + 'Country';
            var DataForm_AddrKeyIn_ComboRoad_ID = DataForm_AddrKeyIn_Combo_ID + 'Road';

            var DataForm_AddrKeyIn_Input_ID = '#DataForm_AddrKeyIn_Input';
            //===============================【Ready】=========================================
            $(function () {
                //-----------------------------------地址輸入畫面-------------------------------------
                (function () {

                    var aBtn = $('<a>', { href: 'javascript:void(0)' }).linkbutton({ iconCls: 'icon-edit', plain: true }).on('click', function () {
                        if (!$(dataFormMasterADDR_ID).prop('disabled')) openForm(Dialog_AddrKeyIn_ID, {}, "inserted", 'dialog');
                    });
                    $(dataFormMasterADDR_ID).after(aBtn);
                })();
                //-----------------------------------送出按鈕加工-------------------------------------
                $('#DialogSubmit', Dialog_AddrKeyIn_ID).removeAttr('onclick').on('click', function () {

                    var theReturnValue = '';
                    //取三個combobox
                    theReturnValue += $(DataForm_AddrKeyIn_ComboCity_ID).combobox('getText');
                    theReturnValue += $(DataForm_AddrKeyIn_ComboCountry_ID).combobox('getText');
                    theReturnValue += $(DataForm_AddrKeyIn_ComboRoad_ID).combobox('getText');

                    //取剩下手KEY值
                    $('table:first', DataForm_AddrKeyIn_Input_ID).find('tr:first > td').each(function () {
                        if ($('input', this).length == 0) return;
                        theReturnValue += twoTdString(this);
                    });

                    //回填
                    $(dataFormMasterADDR_ID).val(theReturnValue);

                    //關閉
                    $(Dialog_AddrKeyIn_ID).dialog('close');
                });

                //-----------------------------------地址輸入Combo連動--------------------------------
                (function () {
                    var cbCity = $(DataForm_AddrKeyIn_ComboCity_ID);
                    var cbCountry = $(DataForm_AddrKeyIn_ComboCountry_ID);
                    var cbRoad = $(DataForm_AddrKeyIn_ComboRoad_ID);

                    //City綁定選定後連動Country
                    cbCity.combobox({
                        valueField: 'Value', textField: 'Text',
                        onSelect: function (record) {
                            $.post('../handler/JqDataHandle.ashx?RemoteName=_CON_System_Share', { mode: 'method', method: 'getCountry', parameters: record['Value'] }).
                                done(function (data) {
                                    var Json = $.parseJSON(data);
                                    cbCountry.combobox({ data: Json.IsOK ? Json.Result : {} });
                                });
                        }
                    });

                    //Country綁定選定後連動Road,預設第一筆
                    cbCountry.combobox({
                        valueField: 'Value', textField: 'Text',
                        onLoadSuccess: function () {
                            var theCombobox = $(this);
                            var data = $(theCombobox).combobox('getData');
                            if ($(data).length > 0) $(theCombobox).combobox('select', data[0]['Value']);
                        },
                        onSelect: function (record) {
                            $.post('../handler/JqDataHandle.ashx?RemoteName=_CON_System_Share', { mode: 'method', method: 'getRoad', parameters: record['Value'] }).
                                done(function (data) {
                                    var Json = $.parseJSON(data);
                                    cbRoad.combobox({ data: Json.IsOK ? Json.Result : {} });
                                });
                        }
                    });

                    //預設第一筆
                    cbRoad.combobox({
                        valueField: 'Value', textField: 'Text',
                        onLoadSuccess: function () {
                            var theCombobox = $(this);
                            var data = $(theCombobox).combobox('getData');
                            if ($(data).length > 0) $(theCombobox).combobox('select', data[0]['Value']);
                        }
                    });

                    //City資料寫入
                    $.post('../handler/JqDataHandle.ashx?RemoteName=_CON_System_Share', { mode: 'method', method: 'getCity', parameters: '' }).
                       done(function (data) {
                           var Json = $.parseJSON(data);
                           cbCity.combobox({ data: Json.IsOK ? Json.Result : {} });
                       });
                })();
                //-----------------------------------DataForm_AddrKeyIn_Input整形---------------------
                (function () {
                    var aList = ['Lane', 'Alley', 'Number', 'Floor', 'Room'];
                    $.each(aList, function () {
                        var aTD = $(DataForm_AddrKeyIn_Input_ID + this).closest('td');
                        aTD.after(aTD.prev());
                    });

                })();
                //-------------------------------------------------------------------------------------
            });
            //=====================================================================================
            //-----------------------------------表單載入-----------------------------------------
            var twoTdString = function (thisTd) {
                var ans = '';
                var prevTd = $(thisTd).prev('td:first')[0];
                var nextTd = $(thisTd).next('td:first')[0];
                if (nextTd && $('input', nextTd).length == 0) {
                    var input = $('input:first', thisTd);
                    if (input.val()) ans = input.val() + $(nextTd).html();
                } else if (prevTd && $('input', prevTd).length == 0) {
                    var input = $('input:first', thisTd);
                    if (input.val()) ans = $(prevTd).html() + input.val();
                }
                return ans;
            }
        </script>
        <JQTools:JQDialog ID="Dialog_AddrKeyIn" runat="server" BindingObjectID="DataForm_AddrKeyIn_Combo" Title="地址輸入畫面" Width="650">
            <JQTools:JQDataForm ID="DataForm_AddrKeyIn_Combo" runat="server" DataMember=" " HorizontalColumnsCount="3" RemoteName=" " Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="縣市" FieldName="City" Width="80" Editor="text" />
                    <JQTools:JQFormColumn Alignment="left" Caption="鄉鎮市區" FieldName="Country" Width="80" Editor="text" />
                    <JQTools:JQFormColumn Alignment="left" Caption="道路或街名或村里名稱" FieldName="Road" Width="180" Editor="text" />
                </Columns>
            </JQTools:JQDataForm>
            <div>
                <JQTools:JQDataForm ID="DataForm_AddrKeyIn_Input" runat="server" DataMember=" " HorizontalColumnsCount="7" RemoteName=" " Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" Width="600px">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="巷" Editor="text" Visible="True" Width="50" FieldName="Lane" />
                        <JQTools:JQFormColumn Alignment="left" Caption="弄" Editor="text" Visible="True" Width="50" FieldName="Alley" />
                        <JQTools:JQFormColumn Alignment="left" Caption="號" Editor="text" Visible="True" Width="50" FieldName="Number" />
                        <JQTools:JQFormColumn Alignment="left" Caption="之" Editor="text" Visible="True" Width="50" FieldName="Number1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="樓" Editor="text" Visible="True" Width="50" FieldName="Floor" />
                        <JQTools:JQFormColumn Alignment="left" Caption="之" Editor="text" Visible="True" Width="50" FieldName="Floor1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="室" Editor="text" Visible="True" Width="50" FieldName="Room" />
                    </Columns>
                </JQTools:JQDataForm>
            </div>
        </JQTools:JQDialog>
    </form>
</body>
</html>
