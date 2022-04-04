<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON_Normal_Gift.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jquery.jbjob_wenetgroup.js"></script>
    <script src="../js/jquery.blockUI.js"></script>
    <link href="../css/WENETGROUP/Dialog.css" rel="stylesheet" />
    <title>禮品設定</title>
    <script type="text/javascript">
        var dataGridMaster_ID = "#dataGridMaster";
        var JQDialog1_ID = '#JQDialog1';

        var dataFormMaster_ID = '#dataFormMaster';
        var dataFormMasterPHOTO_ID = '#dataFormMasterGIFT_PHOTO';

        var JQDialog1Log_ID = '#JQDialog1Log';
        var dataFormMasterLog_ID = '#dataFormMasterLog';
        var dataGridMasterLog_ID = '#dataGridMasterLog';

        var Dialog_Import_ID = '#Dialog_Import';
        var Dialog_ImportMain_ID = '#Dialog_ImportMain';
        var DataForm_ImportMain_ID = '#DataForm_ImportMain';
        //=======================================【Ready】=========================================
        $(function () {
            //-------------------------------Form插入圖片-------------------------------------
            (function () {
                var filePhoto = $(dataFormMasterPHOTO_ID);
                var TD = filePhoto.closest('td');
                TD.append($('<div>').append(TD.children()).before($('<div>', { 'class': 'photoLayout' }).append($('<img>', { id: filePhoto.attr('id') + '_Img' }))));
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
                $.post('../handler/JqDataHandle.ashx?RemoteName=_CON_Normal_Gift', {
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
            //-------------------------------------------------------------------------------------
        });
        //=========================================================================================
        //-----------------------------------照片顯示(上傳後+載入都要用到)---------------------
        var dataFormGIFT_PHOTO_onSuccess = function (data) {
            $(this).jbFileUploadWithPhoto();
        }
        //-----------------------------------照片上傳前---------------------------------------
        var dataFormGIFT_PHOTO_onBeforeUpload = function () {
            //$(this) == 'DF_BasePHOTO'
            //var InputFile = $('#infoFileUploadDF_BasePHOTO')[0];
            //alert(InputFile.size);
            //檢查區域?!
            return true;
        }
        //=========================================================================================
        //---------------------------------------Form載入之後-------------------------------------
        var DataForm_OnLoadSuccess = function (RowData) {
            $(dataFormMasterPHOTO_ID).jbFileUploadWithPhoto();   //照片處理
            ShowImage($('#dataFormMasterGIFT_PHOTO_Img'), 150, 150);
            //var defaultWhereStr = '';
            //var theGrid = '';

            //var thisDataForm = $(this);
            //var form_ID = '#' + thisDataForm.attr('id');
            //var ID = Request.getQueryStringByName("ID");

            //switch (form_ID) {
            //    case dataFormMasterLog_ID:
            //        defaultWhereStr = String.format("GIFT_ID='{0}'", RowData.GIFT_ID);
            //        theGrid = dataGridMasterLog_ID;
            //        break;
            //    case dataFormMaster_ID:
            //        if (ID) {
            //            $(dataFormMasterCONTACT_ID).refval('setValue', ID);
            //            $(dataFormMasterCONTACT_ID).refval('disable');
            //            if (getEditMode(thisDataForm) == "inserted") {
            //                $.post(getRemoteUrl('_CON_SHARECODE.CON_CONTACT', 'CON_CONTACT'),
            //                   { queryWord: $.toJSONString({ whereString: String.format("CONTACT_ID='{0}'", ID) }) },
            //                   function (data) {
            //                       var rowsData = $.parseJSON(data);
            //                   });
            //            }
            //        }
            //        break;
            //    default:
            //        break;
            //}
            //if (theGrid && defaultWhereStr) $(theGrid).data('defaultWhereStr', defaultWhereStr).datagrid("setWhere", defaultWhereStr);
        }
        //---------------------------------------Form存檔之前-------------------------------------
        var DataForm_OnApply = function () {
            var Ans = false;
            if ($(this).form('validateForm')) {
                var validateFun = '';
                switch ('#' + $(this).attr('id')) {
                    case dataFormMaster_ID: validateFun = 'DataValidate'; break;
                }
                if (validateFun) {
                    var data = $(this).jbDataFormGetAFormData();
                    $.ajaxSetup({ async: false });
                    //檢查資料是否有重複
                    $.post('../handler/JqDataHandle.ashx?RemoteName=_CON_Normal_GIFT', { mode: 'method', method: validateFun, parameters: $.toJSONString(data) }
                          ).done(function (data) {
                              var Json = $.parseJSON(data);
                              if (Json.IsOK) Ans = true;
                              else alert(Json.ErrorMsg);
                          }).fail(function (xhr, textStatus, errorThrown) {
                              alert('error');
                          });
                    $.ajaxSetup({ async: true });
                }
            }
            return Ans;
        }
        //---------------------------------------Form存檔之後-------------------------------------
        var DataForm_OnApplied = function () {
            $(this).jbDataFormReloadDataGrid();
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
        //---------------------------------------Grid欄位FormatScript-----------------------------
        var dataGridMaster_FormatScript = function (value, row, index) {
            var fieldName = this.field;
            switch (fieldName) {
                case 'TRANSLOG':
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
            var RowData = $(dataGridMaster_ID).datagrid('selectRow', rowIndex).datagrid('getSelected');
            switch (command) {
                case 'TRANSLOG':
                    openForm(JQDialog1Log_ID, RowData, 'viewed', 'Dialog');
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
        //---------------------------------------匯入Excel----------------------------------------
        var openImportExcel = function () {
            $(Dialog_Import_ID).dialog('open');
        }
        //-----------------------------------------------------------------------------------------
        //判斷禮品代碼(GIFT_CODE)是否有重複
        function checkGiftCode(val) {
            var o_GiftCode = ""
            var GiftCode = val;
            if ($("#dataGridMaster").datagrid('getSelected')) 
                o_GiftCode = $("#dataGridMaster").datagrid('getSelected').GIFT_CODE;
 
            if (getEditMode($("#dataFormMaster")) == 'inserted' || o_GiftCode != GiftCode) {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_CON_Normal_GIFT.CON_GIFT', //連接的Server端，command
                    data: "mode=method&method=" + "checkGiftCode" + "&parameters=" + giftCode + "," + GiftName, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
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
        //-----------------------------------------------------------------------------------------
        //網址欄位超連結
        function HyperlinkGiftURL(value, row, index) {
             return "<a href='javascript: void(0)' onclick='LinkGiftURL(" + index + ");'>" + value + "</a>";
        }

        function LinkGiftURL(index) {
            //alert(index)
            $("#dataGridMaster").datagrid('selectRow', index);
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var giftURL = rows.GIFT_URL
            window.open(giftURL, "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=20, left=50, width=900, height=600");
        }

        function dataGridMaster_Reload() {
            $("#dataGridMaster").datagrid('reload');
        }

     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="_CON_Normal_Gift.CON_GIFT" runat="server" AutoApply="True"
                DataMember="CON_GIFT" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="禮品設定" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="100px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="禮品設定流水碼" Editor="numberbox" FieldName="GIFT_ID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="禮品代碼" Editor="text" FieldName="GIFT_CODE" Format="" MaxLength="50" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="禮品級別" Editor="text" FieldName="GIFT_LEVELE_NAME" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="GIFT_LEVEL_ID" Editor="numberbox" FieldName="GIFT_LEVEL_ID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="品名" Editor="text" FieldName="GIFT_NAME" Format="" MaxLength="50" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="參考圖片" Editor="text" FieldName="GIFT_PHOTO" Format="image,folder:files/UploadFile/WENETGROUP/CON_GIFT/PHOTO,height:30,,Stretch" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="網址" Editor="text" FieldName="GIFT_URL" Format="" MaxLength="50" Width="180" FormatScript="HyperlinkGiftURL" />
                    <JQTools:JQGridColumn Alignment="right" Caption="容量價位" Editor="numberbox" FieldName="GIFT_PRICE" Format="" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動年度" Editor="text" FieldName="GIFT_YEAR" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="GIFT_MEMO" Format="" MaxLength="2147483647" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建議" Editor="text" FieldName="GIFT_MEMO1" Format="" MaxLength="2147483647" Width="180" />
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
                    <JQTools:JQQueryColumn AndOr="and" Caption="禮品等級" Condition="%%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_Normal_Gift.CON_GIFT_LEVEL',tableName:'CON_GIFT_LEVEL',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="GIFT_LEVELE_NAME" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="禮品代碼" Condition="%%" DataType="string" Editor="text" FieldName="GIFT_CODE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="品名" Condition="%%" DataType="string" Editor="text" FieldName="GIFT_NAME" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="備註" Condition="%%" DataType="string" Editor="text" FieldName="GIFT_MEMO" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="建議" Condition="%%" DataType="string" Editor="text" FieldName="GIFT_MEMO1" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="聯絡資料" Width="850px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CON_GIFT" HorizontalColumnsCount="3" RemoteName="_CON_Normal_Gift.CON_GIFT" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" OnLoadSuccess="DataForm_OnLoadSuccess" OnApplied="dataGridMaster_Reload">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="禮品設定流水碼" Editor="numberbox" FieldName="GIFT_ID" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="禮品代碼" Editor="text" FieldName="GIFT_CODE" Format="" MaxLength="50" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="禮品級別" Editor="infocombobox" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_Normal_Gift.CON_GIFT_LEVEL',tableName:'CON_GIFT_LEVEL',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="GIFT_LEVEL_ID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="參考圖片" Editor="infofileupload" EditorOptions="filter:'jpg|jpeg|png|bmp|gif',isAutoNum:true,upLoadFolder:'Files/UploadFile/WENETGROUP/CON_GIFT/PHOTO',showButton:true,showLocalFile:false,onSuccess:dataFormGIFT_PHOTO_onSuccess,onBeforeUpload:dataFormGIFT_PHOTO_onBeforeUpload" FieldName="GIFT_PHOTO" Format="" MaxLength="50" NewRow="False" RowSpan="6" Span="1" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="品名" Editor="text" FieldName="GIFT_NAME" Format="" MaxLength="50" Width="450" Span="2" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="容量價位" Editor="numberbox" FieldName="GIFT_PRICE" Format="" MaxLength="0" NewRow="True" RowSpan="1" Span="1" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動年度" Editor="text" FieldName="GIFT_YEAR" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="網址" Editor="text" FieldName="GIFT_URL" Format="" MaxLength="200" Width="450" RowSpan="1" NewRow="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="GIFT_MEMO" Format="" MaxLength="2147483647" Width="450" NewRow="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建議" Editor="textarea" FieldName="GIFT_MEMO1" Format="" MaxLength="2147483647" Width="450" NewRow="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" MaxLength="50" Width="180" ReadOnly="True" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Width="180" ReadOnly="True" MaxLength="0" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" MaxLength="50" Width="180" ReadOnly="True" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Width="180" ReadOnly="True" MaxLength="0" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="GIFT_ID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckMethod="checkGiftCode" CheckNull="True" RemoteMethod="False" ValidateMessage="此筆禮品代碼已存在" ValidateType="None" FieldName="GIFT_CODE" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="GIFT_NAME" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="GIFT_LEVEL_ID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="GIFT_PRICE" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="GIFT_YEAR" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="JQDialog1Log" runat="server" BindingObjectID="dataFormMasterLog" Title="一般註記異動資料記錄" ShowModal="True" EditMode="Dialog" Width="650px" DialogLeft="" DialogTop="">
                <div style="display: none;">
                    <JQTools:JQDataForm ID="dataFormMasterLog" runat="server" DataMember="CON_GIFT" RemoteName="_CON_Normal_GIFT.CON_GIFT" OnLoadSuccess="DataForm_OnLoadSuccess">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="禮品ID" Editor="numberbox" FieldName="GIFT_ID" Width="140" />
                        </Columns>
                    </JQTools:JQDataForm>
                </div>
                <JQTools:JQDataGrid ID="dataGridMasterLog" data-options="pagination:true,view:commandview" runat="server" RemoteName="_CON_Normal_Gift.CON_GIFT_LOG" DataMember="CON_GIFT_LOG" AutoApply="False" Pagination="True" EditDialogID=" "
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ReportFileName="" EditMode="Dialog" QueryTitle="Query">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="LOG_STATE_NAME" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="USERNAME" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="LOG_USER" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="False" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="異動日期" Editor="datebox" FieldName="LOG_DATE" Frozen="False" MaxLength="8" ReadOnly="False" Sortable="False" Visible="True" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                        <JQTools:JQGridColumn Alignment="left" Caption="禮品代碼" Editor="text" FieldName="GIFT_CODE" Format="" MaxLength="50" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="禮品級別" Editor="text" FieldName="GIFT_LEVELE_NAME" Format="" MaxLength="0" Width="120" />
                        <JQTools:JQGridColumn Alignment="right" Caption="GIFT_LEVEL_ID" Editor="numberbox" FieldName="GIFT_LEVEL_ID" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="品名" Editor="text" FieldName="GIFT_NAME" Format="" MaxLength="50" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="參考圖片" Editor="text" FieldName="GIFT_PHOTO" Format="" MaxLength="50" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="網址" Editor="text" FieldName="GIFT_URL" Format="" MaxLength="50" Width="120" />
                        <JQTools:JQGridColumn Alignment="right" Caption="容量價位" Editor="numberbox" FieldName="GIFT_PRICE" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="GIFT_MEMO" Format="" MaxLength="2147483647" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建議" Editor="text" FieldName="GIFT_MEMO1" Format="" MaxLength="2147483647" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                    </TooItems>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>

            <!-- 匯入對話框內容的 DIV -->
            <div id="Dialog_Import">

                <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog">
                    <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" DataMember="CON_GIFT" HorizontalColumnsCount="3" RemoteName="_CON_Normal_Gift.CON_GIFT" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="檔案名稱" Editor="text" FieldName="FilePathName" Width="80" ReadOnly="true" Visible="false" />
                            <JQTools:JQFormColumn Alignment="left" Caption="禮品代碼" Editor="infocombobox" FieldName="GIFT_CODE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="禮品級別" Editor="infocombobox" FieldName="GIFT_LEVEL_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="品名" Editor="infocombobox" FieldName="GIFT_NAME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="網址" Editor="infocombobox" FieldName="GIFT_URL" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="容量價位" Editor="infocombobox" FieldName="GIFT_PRICE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="infocombobox" FieldName="GIFT_MEMO" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="建議" Editor="infocombobox" FieldName="GIFT_MEMO1" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
                        <Columns>
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="GIFT_CODE" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="GIFT_NAME" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="GIFT_PRICE" RemoteMethod="True" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="GIFT_YEAR" RemoteMethod="True" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="GIFT_LEVEL_ID" RemoteMethod="True" ValidateType="None" />
                        </Columns>
                    </JQTools:JQValidate>
                </JQTools:JQDialog>
            </div>
        </div>
    </form>
</body>
</html>
