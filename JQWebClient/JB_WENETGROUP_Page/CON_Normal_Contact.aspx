<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON_Normal_Contact.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jquery.jbjob_wenetgroup.js"></script>
    <script src="../js/jquery.blockUI.js"></script>
    <link href="../css/WENETGROUP/Dialog.css" rel="stylesheet" />
    <title>聯絡資料</title>

</head>
<body>
    <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "#FFFFB5");
            });

            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });

            $('<a>', { id: 'BT_SKILL', class: 'easyui-linkbutton', 'data-options': "iconCls:'icon-search',plain:true" }).appendTo($('#dataFormMasterCONTACT_SKILL_NAME').closest("td")).linkbutton();
            $('<a>', { id: 'BT_HOBBY', class: 'easyui-linkbutton', 'data-options': "iconCls:'icon-search',plain:true" }).appendTo($('#dataFormMasterCONTACT_HOBBY_NAME').closest("td")).linkbutton();

            //新增查詢圖示
            //$('<a>', { id: 'BT_SKILL_QUERY', class: 'easyui-linkbutton', 'data-options': "iconCls:'icon-search',plain:true" }).appendTo($('#CONTACT_SKILL_NAME_Query').closest("td")).linkbutton();
            //$('<a>', { id: 'BT_HOBBY_QUERY', class: 'easyui-linkbutton', 'data-options': "iconCls:'icon-search',plain:true" }).appendTo($('#CONTACT_HOBBY_NAME_Query').closest("td")).linkbutton();
            //$('#CONTACT_SKILL_NAME_Query').closest("td").attr('colspan', 3);
            //$('#CONTACT_HOBBY_NAME_Query').closest("td").attr('colspan', 3);

            //將原先 JQDialog1 的 dialShowSubmitDiv="False" 
            //並在 dataFormMaster 新增 存檔即關閉 button
            var btnSave = $('<a>', { id: 'BT_SAVE', class: "easyui-linkbutton infosysbutton-s l-btn", onclick: "submitForm('#JQDialog1')", href: "javascript:void(0)", isalsoreadonly: "true" }).html('存檔');
            var btnClose = $('<a>', { id: 'BT_ClOSE', class: "easyui-linkbutton infosysbutton-c l-btn", onclick: "closeForm('#JQDialog1')", href: "javascript:void(0)", isalsoreadonly: "true" }).html('關閉');

            $('#dataFormMasterSaveButton').after(btnClose).after(btnSave).remove();
            btnSave.linkbutton();
            btnClose.linkbutton();

            // 建立專長 dialog
            initQuerySkillDialog();

            // 建立興趣 dialog
            initQueryHobbyDialog();

            //open 專長 dialog --使用 jQuery 绑定 easyui-linkbutton
            $(function () {
                $('#BT_SKILL').bind('click', function () {
                    openQuerySkillDialog();
                });
            });

            //open 興趣 dialog --使用 jQuery 绑定 easyui-linkbutton
            $(function () {
                $('#BT_HOBBY').bind('click', function () {
                    openQueryHobbyDialog();
                });
            });

            //open 專長 dialog --使用 jQuery 绑定 easyui-linkbutton
            $(function () {
                $('#BT_SKILL_QUERY').bind('click', function () {
                    openQuerySkillDialog();
                });
            });

            //open 興趣 dialog --使用 jQuery 绑定 easyui-linkbutton
            $(function () {
                $('#BT_HOBBY_QUERY').bind('click', function () {
                    openQueryHobbyDialog();
                });
            });
        });

        var dataGridMaster_ID = "#dataGridMaster";
        var JQDialog1_ID = '#JQDialog1';

        var Tab_Management_ID = '#Tab_Management';

        var dataFormMaster_ID = '#dataFormMaster';
        var dataFormMasterCENTER_ID = '#dataFormMasterCENTER_ID';
        var dataFormMasterPHOTO_ID = '#dataFormMasterCONTACT_PHOTO';

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
            //-------------------------------Form整形-----------------------------------------
            $(dataFormMaster_ID).parent().css('padding', 0).parent().css('padding', 0);
            $('table', dataFormMaster_ID).css('margin', '0 auto');
            $(JQDialog1_ID).width('100%');
            //---------------------------------------------------------------------------------

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
                $.post('../handler/JqDataHandle.ashx?RemoteName=_CON_Normal_Contact', {
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

        //-----------------------------------照片顯示(上傳後+載入都要用到)---------------------
        var dataFormCONTACT_PHOTO_onSuccess = function (data) {
            $(this).jbFileUploadWithPhoto();
        }
        //-----------------------------------照片上傳前---------------------------------------
        var dataFormCONTACT_PHOTO_onBeforeUpload = function () {
            //$(this) == 'DF_BasePHOTO'
            //var InputFile = $('#infoFileUploadDF_BasePHOTO')[0];
            //alert(InputFile.size);
            //檢查區域?!
            return true;
        }
        //=========================================================================================
        //---------------------------------------Form載入之後-------------------------------------
        var DataForm_OnLoadSuccess = function (RowData) {
            var defaultWhereStr = '';
            var theGrid = '';

            var thisDataForm = $(this);
            var form_ID = '#' + thisDataForm.attr('id');
            switch (form_ID) {
                case dataFormMasterLog_ID:
                    defaultWhereStr = String.format("CONTACT_ID='{0}'", RowData.CONTACT_ID);
                    theGrid = dataGridMasterLog_ID;
                    break;
                case dataFormMaster_ID:
                    $(dataFormMasterCENTER_ID).combobox('setWhere', "CENTER_ID in (select CENTER_ID from CON_CENTER_AUTHORITY where USERID = '" + getClientInfo("UserID") + "' ) ");
                    $(dataFormMasterPHOTO_ID).jbFileUploadWithPhoto();   //照片處理
                    ShowImage($('#dataFormMasterCONTACT_PHOTO_Img'), 150, 150);
                    //判斷新增狀態隱藏tabitem
                    if (getEditMode(thisDataForm) == "inserted") {
                        var length = $(Tab_Management_ID).tabs('tabs').length;
                        for (var i = 1; i < length; i++) {
                            var onetab = $(Tab_Management_ID).tabs("tabs")[i];
                            onetab.panel('options').tab.hide();
                        }
                        $(Tab_Management_ID).tabs('select', 0)
                        //設定中心ID
                        if (RowData.CENTER_ID) {
                            $.post(getRemoteUrl('_CON_SHARECODE.CON_CENTER', 'CON_CENTER'),
                               { queryWord: $.toJSONString({ whereString: String.format("CENTER_ID='{0}'", RowData.CENTER_ID) }) },
                               function (data) {
                                   var rowsData = $.parseJSON(data);
                                   $(dataFormMasterCENTER_ID).combobox('setValue', rowsData[0].CENTER_ID);
                               });
                        }
                    }
                    else {
                        var TabIndexList = [];
                        if (RowData.CONTACT_ID) {
                            //Tab方法
                            var length = $(Tab_Management_ID).tabs('tabs').length;
                            for (var i = 1; i < length; i++) {
                                var onetab = $(Tab_Management_ID).tabs("tabs")[i];
                                onetab.panel('options').tab.show();
                            }
                            $(Tab_Management_ID).tabs({
                                onSelect: function (title, index) {
                                    if ($.inArray(index, TabIndexList) == -1) {
                                        if (TabSelectedLoad.call($(this).tabs('getSelected'), RowData.CONTACT_ID)) TabIndexList.push(index);
                                    }
                                }
                            }).tabs('select', 0);
                        }
                    }
                    break;
                default:
                    break;
            }
            if (theGrid && defaultWhereStr) $(theGrid).data('defaultWhereStr', defaultWhereStr).datagrid("setWhere", defaultWhereStr);
        }
        //---------------------------------------Tab頁面載入方法----------------------------------
        var TabSelectedLoad = function (ID) {
            var theTab = $(this);
            var Iframe = theTab.find('iframe');
            if (Iframe) {
                var url = Iframe.data('src');
                if (url) {
                    url = url + '?' + $.param({ 'ID': ID });
                    theTab.block({ message: 'Loading.....', css: { border: 'none', padding: '15px', backgroundColor: '#fff', '-webkit-border-radius': '10px', '-moz-border-radius': '10px', opacity: .3, color: '#000' } });
                    Iframe.attr('src', url).load(function () { theTab.unblock(); });
                    return true;
                } else Iframe.attr('src', 'about:blank');
            }
            return false;
        }
        //---------------------------------------Form存檔之後-------------------------------------
        var DataForm_OnApplied = function () {
            $(this).jbDataFormReloadDataGrid();
        }
        //---------------------------------------Grid載入完成-------------------------------------
        var dataGridMaster_OnLoadSuccess = function () {
            var dg = $(this);
            ////第一次載入Grid用
            if (!dg.data('alreadyFirstLoad') && dg.data('alreadyFirstLoad', true)) {
                queryGrid(dg);
            }
        }
        //---------------------------------------聯絡人Grid 刪除前判斷parimay key 相對應之table 是否有資料-------------------------------------
        var dataGridMaster_OnDelete = function (row) {
            var dgid = $(this);
            var cnt;
            //僅管理員能看到全部，並擁有刪除的功能，對非管理員身分的使用者無法刪除
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_CON_Normal_Contact.COLDEF', //連接的Server端，command
                data: "mode=method&method=" + "checkUser" + "&parameters=" + 'CON_CONTACT' + "," + row.CONTACT_ID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        cnt = $.parseJSON(data);
                    }
                    else {
                        alert("沒有刪除的權限, 此筆資料無法刪除");
                        return false;
                    }
                }
            });
            if ((cnt == "0" && cnt != "undefined")) {
                alert("沒有刪除的權限, 此筆資料無法刪除");
                return false;
            }

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_CON_System_Share.COLDEF', //連接的Server端，command
                data: "mode=method&method=" + "checkRowCount" + "&parameters=" + 'CON_CONTACT' + "," + row.CONTACT_ID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
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
            where = where + "exists  (select CENTER_ID from CON_CENTER_AUTHORITY where CENTER_ID = A.CENTER_ID and USERID = '" + userID + "' )";
            var defaultWhereStr = $(dg).data('defaultWhereStr');
            if (defaultWhereStr) where = where ? String.format(" {0} and {1} ", defaultWhereStr, where) : defaultWhereStr;
            $(dg).datagrid('setWhere', where);
        }
        //---------------------------------------匯入Excel----------------------------------------
        var openImportExcel = function () {
            $(Dialog_Import_ID).dialog('open');
        }
        //-----------------------------------------------------------------------------------------
        //判斷手機號碼(CONTACT_CELLPHONE)是否有重複
        function checkContactCellphone(val) {
            var o_ContactCellphone = ""
            var o_CenterID = ""
            
            var ContactCellphone = $('#dataFormMasterCONTACT_CELLPHONE').val();
            var CenterID = $('#dataFormMasterCENTER_ID').combobox('getValue');
            if ($("#dataGridMaster").datagrid('getSelected')) {
                o_ContactCellphone = $("#dataGridMaster").datagrid('getSelected').CONTACT_CELLPHONE;
                o_CenterID = $("#dataGridMaster").datagrid('getSelected').CENTER_ID;
            }
            if (getEditMode($("#dataFormMaster")) == 'inserted' || o_ContactCellphone != ContactCellphone || o_CenterID != CenterID) {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_CON_Normal_Contact.CON_CONTACT', //連接的Server端，command
                    data: "mode=method&method=" + "checkContactCellphone" + "&parameters=" + CenterID + "," + ContactCellphone, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });
                if ((cnt != "0" && cnt != "undefined")) {
                    alert("此筆手機號碼資料已存在")
                    return false;
                }
                else
                    return true;
            }
            else
                return true;
        }

        // 建立專長 dialog
        function initQuerySkillDialog() {
            $("#Dialog_Skill").dialog(
            {
                height: 400,
                width: 400,
                resizable: false,
                modal: true,
                title: "專長選項",
                closed: true,
                buttons: [{
                    text: '取消',
                    handler: function () { $("#Dialog_Skill").dialog("close") }
                },
                {
                    text: "確認",
                    handler: function () {
                        var skillName = "";
                        var skillID = "";
                        var skillRows = $("#DG_SKILL").datagrid("getRows");
                        var checkedItems = $('#DG_SKILL').datagrid('getChecked');
                        var flag;

                        for (var k = 0; k < skillRows.length; k++) {
                            //判斷有勾選的 update 為 "Y"
                            flag = "N"
                            $.each(checkedItems, function (index, item) {
                                if (skillRows[k].CODE_ID == item.CODE_ID) {
                                    skillRows[k].IS_SELECTED = "Y";
                                    flag = "Y";
                                    skillName = skillName + skillRows[k].NAME + ",";
                                    skillID = skillID + skillRows[k].CODE_ID + ",";
                                }
                            });
                            if (flag != "Y")
                                skillRows[k].IS_SELECTED = "N";
                        }

                        $("#dataFormMasterCONTACT_SKILL_NAME").val(skillName);
                        $("#dataFormMasterCONTACT_SKILL_ID").val(skillID);
                        $("#Dialog_Skill").dialog("close");
                    }
                }]
            });
        };

        // 建立興趣 dialog
        function initQueryHobbyDialog() {
            $("#Dialog_Hobby").dialog(
            {
                height: 400,
                width: 400,
                resizable: false,
                modal: true,
                title: "興趣選項",
                closed: true,
                buttons: [{
                    text: '取消',
                    handler: function () { $("#Dialog_Hobby").dialog("close") }
                },
                {
                    text: "確認",
                    handler: function () {
                        var hobbyName = "";
                        var hobbyID = "";
                        var hobbyRows = $("#DG_HOBBY").datagrid("getRows");
                        var checkedItems = $('#DG_HOBBY').datagrid('getChecked');
                        var flag;

                        for (var k = 0; k < hobbyRows.length; k++) {
                            //判斷有勾選的 update 為 "Y"
                            flag = "N"
                            $.each(checkedItems, function (index, item) {
                                if (hobbyRows[k].CODE_ID == item.CODE_ID) {
                                    hobbyRows[k].IS_SELECTED = "Y";
                                    flag = "Y";
                                    hobbyName = hobbyName + hobbyRows[k].NAME + ",";
                                    hobbyID = hobbyID + hobbyRows[k].CODE_ID + ",";
                                }
                            });
                            if (flag != "Y")
                                hobbyRows[k].IS_SELECTED = "N";
                        }

                        $("#dataFormMasterCONTACT_HOBBY_NAME").val(hobbyName);
                        $("#dataFormMasterCONTACT_HOBBY_ID").val(hobbyID);
                        $("#Dialog_Hobby").dialog("close");
                    }
                }]
            });
        };

        // open專長查詢畫面 dialog
        function openQuerySkillDialog() {
            var skillList = $("#dataFormMasterCONTACT_SKILL_ID").val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_CON_SHARECODE.CONTACT_SKILL', //連接的Server端，command
                data: "mode=method&method=" + "getDialogData" + "&parameters=" + "SKILL" + "," + skillList, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                    if (rows.length > 0) {
                        //$('#DG_EMPLOYEE').datagrid('loadData', { "total": 0, "rows": [] });
                        $('#DG_SKILL').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                        $('#DG_SKILL').datagrid('uncheckAll');
                        for (var j = 0; j < rows.length; j++) {
                            if (rows[j].IS_SELECTED == "Y")
                                $('#DG_SKILL').datagrid('checkRow', j);
                        }
                    }
                }
            });
            $("#Dialog_Skill").dialog("open");
        };

        // open興趣查詢畫面 dialog
        function openQueryHobbyDialog() {
            var hobbyList = $("#dataFormMasterCONTACT_HOBBY_ID").val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_CON_SHARECODE.CONTACT_HOBBY', //連接的Server端，command
                data: "mode=method&method=" + "getDialogData" + "&parameters=" + "HOBBY" + "," + hobbyList, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                    if (rows.length > 0) {
                        //$('#DG_EMPLOYEE').datagrid('loadData', { "total": 0, "rows": [] });
                        $('#DG_HOBBY').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                        $('#DG_HOBBY').datagrid('uncheckAll');
                        for (var j = 0; j < rows.length; j++) {
                            if (rows[j].IS_SELECTED == "Y")
                                $('#DG_HOBBY').datagrid('checkRow', j);
                        }
                    }
                }
            });
            $("#Dialog_Hobby").dialog("open");
        };

        function genCheckBox(val) {
            if (val == "Y")
                return "<input  type='checkbox' checked='true' />";
            else
                return "<input  type='checkbox' />";
        }

        //關閉mouseover時把圖放大的功能
        //覆蓋原本infolight.js的功能
        function infoimageonmouseover() { return; };
        function infogridimageformatterset(target, val) { return; };

    </script>

    <form id="form1" runat="server">
        <%--<JQTools:JQMultiLanguage ID="JQMultiLanguage1" runat="server" />--%>

        <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="_CON_Normal_Contact.CON_CONTACT" runat="server" AutoApply="True"
            DataMember="CON_CONTACT" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
            Title="聯絡資料" QueryLeft="100px" QueryTop="100px" OnLoadSuccess="dataGridMaster_OnLoadSuccess" AlwaysClose="True" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryMode="Window" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDelete="dataGridMaster_OnDelete">
            <Columns>
                <JQTools:JQGridColumn Alignment="left" Caption="聯絡人流水號" Editor="text" FieldName="CONTACT_ID" Width="90" MaxLength="4" Visible="False" />
                <JQTools:JQGridColumn Alignment="left" Caption="中心名稱" Editor="text" FieldName="CENTER_CNAME" Width="90" MaxLength="50" />
                <JQTools:JQGridColumn Alignment="left" Caption="名字" Editor="text" FieldName="CONTACT_NAME" Width="90" MaxLength="50" />
                <JQTools:JQGridColumn Alignment="left" Caption="職稱" Editor="text" FieldName="CONTACT_JOB" Width="100" MaxLength="50" />
                <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="CONTACT_TEL" Width="100" MaxLength="50" />
                <JQTools:JQGridColumn Alignment="left" Caption="手機" Editor="text" FieldName="CONTACT_CELLPHONE" Width="100" MaxLength="50" />
                <JQTools:JQGridColumn Alignment="left" Caption="E-Mail" Editor="text" FieldName="CONTACT_EMAIL" Width="100" MaxLength="50" />
                <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="CONTACT_ADDR" Width="100" MaxLength="50" />
                <JQTools:JQGridColumn Alignment="left" Caption="相片" Editor="text" FieldName="CONTACT_PHOTO" Width="100" MaxLength="50" Format="image,folder:files/UploadFile/WENETGROUP/CON_CONTACT/PHOTO,height:40,,Stretch" />
                <JQTools:JQGridColumn Alignment="left" Caption="區域" Editor="text" FieldName="CONTACT_AREA_NAME" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" MaxLength="50" />
                <JQTools:JQGridColumn Alignment="left" Caption="類型" Editor="text" FieldName="CONTACT_TYPE_NAME" Frozen="False" ReadOnly="False" Sortable="False" Visible="True" Width="90" MaxLength="50" />
                <JQTools:JQGridColumn Alignment="left" Caption="領域別" Editor="text" FieldName="CONTACT_TERRITORY_NAME" Frozen="False" IsNvarChar="False" MaxLength="50" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                <JQTools:JQGridColumn Alignment="left" Caption="專長" Editor="text" FieldName="CONTACT_SKILL_NAME" Frozen="False" IsNvarChar="False" MaxLength="50" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                <JQTools:JQGridColumn Alignment="left" Caption="興趣" Editor="text" FieldName="CONTACT_HOBBY_NAME" Frozen="False" IsNvarChar="False" MaxLength="50" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="CONTACT_MEMO" Width="100" MaxLength="50" />
                <JQTools:JQGridColumn Alignment="left" Caption="備註一" Editor="text" FieldName="CONTACT_MEMO1" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" MaxLength="50" />
                <JQTools:JQGridColumn Alignment="left" Caption="備註二" Editor="text" FieldName="CONTACT_MEMO2" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" MaxLength="50" />
                <JQTools:JQGridColumn Alignment="left" Caption="備註三" Editor="text" FieldName="CONTACT_MEMO3" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" MaxLength="50" />
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
                <JQTools:JQQueryColumn AndOr="and" Caption="中心" Condition="=" DataType="string" Editor="infocombobox" FieldName="CENTER_ID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" EditorOptions="valueField:'CENTER_ID',textField:'CENTER_CNAME',remoteName:'_CON_SHARECODE.CON_CENTER',tableName:'CON_CENTER',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" TableName="CENTER" />
                <JQTools:JQQueryColumn AndOr="and" Caption="名字" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_NAME" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="職稱" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_JOB" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="電話" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_TEL" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="手機" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_CELLPHONE" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="E-Mail" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_EMAIL" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="地址" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_ADDR" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="區域流水碼" Condition="%%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.CONTACT_AREA',tableName:'CONTACT_AREA',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_AREA_ID" IsNvarChar="False" NewLine="True" RemoteMethod="False" TableName="A" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="類型流水碼" Condition="%%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.CONTACT_TYPE',tableName:'CONTACT_TYPE',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_TYPE_ID" IsNvarChar="False" NewLine="False" RemoteMethod="False" TableName="A" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="領域別流水碼" Condition="%%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.CONTACT_TERRITORY',tableName:'CONTACT_TERRITORY',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_TERRITORY_ID" IsNvarChar="False" NewLine="False" RemoteMethod="False" TableName="A" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="專長" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_SKILL_NAME" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="興趣" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_HOBBY_NAME" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="備註" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_MEMO" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="備註一" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_MEMO1" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="備註二" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_MEMO2" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
            </QueryColumns>
        </JQTools:JQDataGrid>
        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="聯絡人" DialogLeft="0px" DialogTop="0px" Width="1100px" EditMode="Dialog" ShowSubmitDiv="False">
            <JQTools:JQTab ID="Tab_Management" runat="server" Height="400">
                <JQTools:JQTabItem ID="JQTabItem0" runat="server" Title="聯絡人">
                    <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CON_CONTACT" HorizontalColumnsCount="3" RemoteName="_CON_Normal_Contact.CON_CONTACT" OnApply="checkContactCellphone" OnApplied="DataForm_OnApplied" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" OnLoadSuccess="DataForm_OnLoadSuccess">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="聯絡人流水號" Editor="text" FieldName="CONTACT_ID" Visible="False" Width="180" MaxLength="4" />
                            <JQTools:JQFormColumn Alignment="left" Caption="中心" Editor="infocombobox" EditorOptions="valueField:'CENTER_ID',textField:'CENTER_CNAME',remoteName:'_CON_SHARECODE.CON_CENTER',tableName:'CON_CENTER',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CENTER_ID" MaxLength="4" Width="180" NewRow="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="text" FieldName="CONTACT_NAME" MaxLength="50" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="CONTACT_JOB" MaxLength="50" Width="180" NewRow="False" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="CONTACT_CELLPHONE" MaxLength="50" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="CONTACT_TEL" MaxLength="50" Width="180" NewRow="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="E-Mail" Editor="text" FieldName="CONTACT_EMAIL" MaxLength="50" RowSpan="1" Span="2" Width="450" Visible="True" />
                            <JQTools:JQFormColumn Alignment="center" Caption="照片" Editor="infofileupload" EditorOptions="filter:'jpg|jpeg|png|bmp|gif',isAutoNum:true,upLoadFolder:'Files/UploadFile/WENETGROUP/CON_CONTACT/PHOTO',showButton:true,showLocalFile:false,onSuccess:dataFormCONTACT_PHOTO_onSuccess,onBeforeUpload:dataFormCONTACT_PHOTO_onBeforeUpload" FieldName="CONTACT_PHOTO" MaxLength="50" RowSpan="6" Visible="True" Width="120" NewRow="False" ReadOnly="False" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="區域" Editor="infocombobox" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.CONTACT_AREA',tableName:'CONTACT_AREA',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_AREA_ID" MaxLength="4" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="類型" Editor="infocombobox" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.CONTACT_TYPE',tableName:'CONTACT_TYPE',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_TYPE_ID" MaxLength="4" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="領域別" Editor="infocombobox" EditorOptions="valueField:'CODE_ID',textField:'NAME',remoteName:'_CON_SHARECODE.CONTACT_TERRITORY',tableName:'CONTACT_TERRITORY',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_TERRITORY_ID" MaxLength="4" NewRow="False" Span="1" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="地址" Editor="text" FieldName="CONTACT_ADDR" MaxLength="50" NewRow="True" Span="2" Width="450" ReadOnly="False" RowSpan="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="專長流水碼" Editor="textarea" FieldName="CONTACT_SKILL_ID" MaxLength="4" Span="1" Visible="False" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="專長" Editor="textarea" FieldName="CONTACT_SKILL_NAME" MaxLength="0" Span="3" Visible="True" Width="450" NewRow="False" ReadOnly="False" RowSpan="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="興趣流水碼" Editor="textarea" FieldName="CONTACT_HOBBY_ID" MaxLength="4" Span="1" Visible="False" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="興趣" Editor="textarea" FieldName="CONTACT_HOBBY_NAME" MaxLength="0" Span="3" Visible="True" Width="450" NewRow="False" ReadOnly="False" RowSpan="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="CONTACT_MEMO" MaxLength="50" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="450" />
                            <JQTools:JQFormColumn Alignment="left" Caption="備註1" Editor="textarea" FieldName="CONTACT_MEMO1" MaxLength="50" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="450" />
                            <JQTools:JQFormColumn Alignment="left" Caption="備註2" Editor="textarea" FieldName="CONTACT_MEMO2" MaxLength="50" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="450" />
                            <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="SaveButton" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="備註3" Editor="textarea" FieldName="CONTACT_MEMO3" MaxLength="50" NewRow="True" ReadOnly="False" Span="2" Visible="False" Width="450" />
                            <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" MaxLength="50" Width="180" ReadOnly="True" NewRow="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Width="180" ReadOnly="True" MaxLength="0" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" NewRow="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" MaxLength="50" Width="180" ReadOnly="True" NewRow="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Width="180" ReadOnly="True" MaxLength="0" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                        <Columns>
                            <JQTools:JQDefaultColumn FieldName="CONTACT_ID" DefaultValue="1" RemoteMethod="True" />
                        </Columns>
                    </JQTools:JQDefault>
                    <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                        <Columns>
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="CENTER_ID" RemoteMethod="True" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckMethod="" CheckNull="True" RemoteMethod="False" ValidateMessage="" ValidateType="None" FieldName="CONTACT_NAME" />
                            <JQTools:JQValidateColumn CheckNull="False" FieldName="CONTACT_EMAIL" ValidateType="EMail" RemoteMethod="True" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_CELLPHONE" RemoteMethod="False" ValidateMessage="此筆手機號碼資料已存在" ValidateType="None" CheckMethod="checkContactCellphone" />
                        </Columns>
                    </JQTools:JQValidate>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem1" runat="server" Title="檔案上傳">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="CON_Normal_ContactFile.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem2" runat="server" Title="一般註紀">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="CON_Normal_ContactMemo.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem3" runat="server" Title="活動記錄">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="CON_Normal_ContactActivity.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem4" runat="server" Title="聯絡窗口"><iframe style="width: 100%; height: 98%; border: 0" data-src="CON_Normal_ContactPerson.aspx"></iframe></JQTools:JQTabItem>
                <%--<JQTools:JQTabItem ID="JQTabItem4" runat="server" Title="其他">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src=""></iframe>
                </JQTools:JQTabItem>--%>
            </JQTools:JQTab>
        </JQTools:JQDialog>

        <JQTools:JQDialog ID="JQDialog1Log" runat="server" BindingObjectID="dataFormMasterLog" Title="轉帳帳戶異動資料記錄" ShowModal="True" EditMode="Dialog" Width="650px" DialogLeft="" DialogTop="">
            <div style="display: none;">
                <JQTools:JQDataForm ID="dataFormMasterLog" runat="server" DataMember="CON_CONTACT" RemoteName="_CON_Normal_Contact.CON_CONTACT" OnLoadSuccess="DataForm_OnLoadSuccess">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人ID" Editor="numberbox" FieldName="CONTACT_ID" Width="140" />
                    </Columns>
                </JQTools:JQDataForm>
            </div>
            <JQTools:JQDataGrid ID="dataGridMasterLog" data-options="pagination:true,view:commandview" runat="server" RemoteName="_CON_Normal_Contact.CON_CONTACT_LOG" DataMember="CON_CONTACT_LOG" AutoApply="False" Pagination="True" EditDialogID=" "
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ReportFileName="" EditMode="Dialog">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="LOG_STATE_NAME" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="USERNAME" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="LOG_USER" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="False" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動日期" Editor="datebox" FieldName="LOG_DATE" Frozen="False" MaxLength="8" ReadOnly="False" Sortable="False" Visible="True" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人流水號" Editor="text" FieldName="CONTACT_ID" Width="90" MaxLength="4" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="中心名稱" Editor="text" FieldName="CENTER_CNAME" Width="90" MaxLength="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="名字" Editor="text" FieldName="CONTACT_NAME" Width="90" MaxLength="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職稱" Editor="text" FieldName="CONTACT_JOB" Width="100" MaxLength="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="CONTACT_TEL" Width="100" MaxLength="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="手機" Editor="text" FieldName="CONTACT_CELLPHONE" Width="100" MaxLength="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="E-Mail" Editor="text" FieldName="CONTACT_EMAIL" Width="100" MaxLength="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="CONTACT_ADDR" Width="100" MaxLength="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="相片" Editor="text" FieldName="CONTACT_PHOTO" Width="100" MaxLength="50" Format="image,folder:files/UploadFile/WENETGROUP/CON_CONTACT/PHOTO,height:40,,Stretch" />
                    <JQTools:JQGridColumn Alignment="left" Caption="區域" Editor="text" FieldName="CONTACT_AREA_NAME" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" MaxLength="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="類型" Editor="text" FieldName="CONTACT_TYPE_NAME" Frozen="False" ReadOnly="False" Sortable="False" Visible="True" Width="90" MaxLength="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="領域別" Editor="text" FieldName="CONTACT_TERRITORY_NAME" Frozen="False" IsNvarChar="False" MaxLength="50" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="專長" Editor="text" FieldName="CONTACT_SKILL_NAME" Frozen="False" IsNvarChar="False" MaxLength="50" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="興趣" Editor="text" FieldName="CONTACT_HOBBY_NAME" Frozen="False" IsNvarChar="False" MaxLength="50" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="CONTACT_MEMO" Width="100" MaxLength="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註一" Editor="text" FieldName="CONTACT_MEMO1" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" MaxLength="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註二" Editor="text" FieldName="CONTACT_MEMO2" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" MaxLength="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註三" Editor="text" FieldName="CONTACT_MEMO3" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" MaxLength="50" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                </TooItems>
            </JQTools:JQDataGrid>
        </JQTools:JQDialog>

        <!-- 匯入對話框內容的 DIV -->
        <div id="Dialog_Import">

            <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog">
                <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" RemoteName=" " DataMember=" " HorizontalColumnsCount="3" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApply="">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="檔案名稱" Editor="text" FieldName="FilePathName" Width="80" ReadOnly="true" Visible="false" />
                        <JQTools:JQFormColumn Alignment="left" Caption="中心名稱" Editor="infocombobox" FieldName="CENTER_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="infocombobox" FieldName="CONTACT_NAME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="infocombobox" FieldName="CONTACT_JOB" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="infocombobox" FieldName="CONTACT_TEL" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="infocombobox" FieldName="CONTACT_CELLPHONE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="E-Mail" Editor="infocombobox" FieldName="CONTACT_EMAIL" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="地址" Editor="infocombobox" FieldName="CONTACT_ADDR" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="區域" Editor="infocombobox" FieldName="CONTACT_AREA_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="類型" Editor="infocombobox" FieldName="CONTACT_TYPE_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="領域別" Editor="infocombobox" FieldName="CONTACT_TERRITORY_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="專長" Editor="infocombobox" FieldName="CONTACT_SKILL_NAME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="興趣" Editor="infocombobox" FieldName="CONTACT_HOBBY_NAME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="infocombobox" FieldName="CONTACT_MEMO" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註一" Editor="infocombobox" FieldName="CONTACT_MEMO1" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註二" Editor="infocombobox" FieldName="CONTACT_MEMO2" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CENTER_ID" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_NAME" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_CELLPHONE" />
                     </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
        <!-- SKILL專長 dialog對話框內容的 DIV -->
        <div id="Dialog_Skill">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="DG_SKILL" runat="server" AutoApply="False" DataMember="CONTACT_SKILL" Pagination="False" ParentObjectID="" RemoteName="_CON_SHARECODE.CONTACT_SKILL" Title="專長選項" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,30,40,50" PageSize="50" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="360px" CheckOnSelect="True" RecordLock="False" ColumnsHibeable="False" RecordLockMode="None" OnLoadSuccess="">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="CODE_ID" Editor="text" FieldName="CODE_ID" Width="90" Visible="False" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="專長名稱" Editor="text" FieldName="NAME" Width="150" ReadOnly="True" Frozen="False" MaxLength="4" QueryCondition="" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="已選擇" Editor="checkbox" FieldName="IS_SELECTED" Width="40" Frozen="False" MaxLength="60" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" EditorOptions="on:'Y',off:'N'" FormatScript="genCheckBox" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>

        <!-- HOBBY興趣 dialog對話框內容的 DIV -->
        <div id="Dialog_Hobby">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="DG_HOBBY" runat="server" AutoApply="False" DataMember="CONTACT_HOBBY" Pagination="False" ParentObjectID="" RemoteName="_CON_SHARECODE.CONTACT_HOBBY" Title="興趣選項" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,30,40,50" PageSize="50" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="360px" CheckOnSelect="True" RecordLock="False" ColumnsHibeable="False" RecordLockMode="None" OnLoadSuccess="">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="CODE_ID" Editor="text" FieldName="CODE_ID" Width="90" Visible="False" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="興趣名稱" Editor="text" FieldName="NAME" Width="150" ReadOnly="True" Frozen="False" MaxLength="4" QueryCondition="" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="已選擇" Editor="checkbox" FieldName="IS_SELECTED" Width="40" Frozen="False" MaxLength="60" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" EditorOptions="on:'Y',off:'N'" FormatScript="genCheckBox" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>

        <script type="text/javascript">
            var dataFormMasterADDR_ID = '#dataFormMasterCONTACT_ADDR';

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
