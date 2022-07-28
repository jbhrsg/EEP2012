<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBCON_ContactManagement.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var GridFlag = 0;
        var Dialog_Import_ID = '#Dialog_Import';
        var Dialog_ImportMain_ID = '#Dialog_ImportMain'; //EXCEL轉入的DialogID
        var DataForm_ImportMain_ID = '#DataForm_ImportMain';
        var UserID = getClientInfo("UserID");
        var UserName = getClientInfo("USERNAME");
        var Labelfilt1 = '';
        var Labelfilt2 = '';
        var Labelfilt3 = '';
        var backcolor = "#cbf1de";
        var IsExportExcelUser = -1;
        $(document).ready(function () {
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", backcolor);
                });
            });
            $("#dataGridView").datagrid('options').rowStyler = function (index, row) {
                var _ErrorTrue = false;
                if ((row.CONTACT_JOB == '' || row.CONTACT_JOB == undefined) && (!_ErrorTrue)) {
                    _ErrorTrue = true;
                }
                if ((row.CONTACT_AREA == '' || row.CONTACT_AREA == undefined) && (!_ErrorTrue)) {
                    _ErrorTrue = true;
                }
                if ((row.CONTACT_ADDR == '' || row.CONTACT_ADDR == undefined) && (!_ErrorTrue)) {
                    _ErrorTrue = true;
                }
                if ((row.CONTACT_TRADE == '' || row.CONTACT_TRADE == undefined) && (!_ErrorTrue)) {
                    _ErrorTrue = true;
                }
                if (_ErrorTrue == true) {
                    return 'background-color:pink;color:blue;font-weight:bold;';
                }
            };
            $('<a>', { id: 'BT_SKILL', class: 'easyui-linkbutton', 'data-options': "iconCls:'icon-search',plain:true" }).appendTo($('#dataFormMasterCONTACT_SKILL_NAME').closest("td")).linkbutton();
            $('<a>', { id: 'BT_HOBBY', class: 'easyui-linkbutton', 'data-options': "iconCls:'icon-search',plain:true" }).appendTo($('#dataFormMasterCONTACT_HOBBY_NAME').closest("td")).linkbutton();
            // 建立專長 dialog
            initQuerySkillDialog();
            initQueryHobbyDialog();
            var CONTACT_NAME = $('#dataFormMasterCONTACT_NAME').closest('td');
            var CONTACT_ENAME = $('#dataFormMasterCONTACT_ENAME').closest('td').children();
            CONTACT_NAME.append(' 英文名').append(CONTACT_ENAME);
            var CONTACT_TRADE = $('#dataFormMasterCONTACT_TRADE').closest('td');
            var CONTACT_TRADENOTES = $('#dataFormMasterCONTACT_TRADENOTES').closest('td').children();
            CONTACT_TRADE.append(' 行業備註').append(CONTACT_TRADENOTES);
            var CONTACT_TEL_TYPE = $('#dataFormMasterCONTACT_TEL_TYPE').closest('td');
            var CONTACT_TEL = $('#dataFormMasterCONTACT_TEL').closest('td').children();
            var CONTACT_TEL_EXT = $('#dataFormMasterCONTACT_TEL_EXT').closest('td').children();
            CONTACT_TEL_TYPE.append(' ').append(CONTACT_TEL).append(' 分機 ').append(CONTACT_TEL_EXT);
            var CONTACT_TEL1_TYPE = $('#dataFormMasterCONTACT_TEL1_TYPE').closest('td');
            var CONTACT_TEL1 = $('#dataFormMasterCONTACT_TEL1').closest('td').children();
            var CONTACT_TEL1_EXT = $('#dataFormMasterCONTACT_TEL1_EXT').closest('td').children();
            CONTACT_TEL1_TYPE.append(' ').append(CONTACT_TEL1).append(' 分機 ').append(CONTACT_TEL1_EXT);
            //縮排地址一類別+地址一
            var CONTACT_ADDR_TYPE = $('#dataFormMasterCONTACT_ADDR_TYPE').closest('td');
            var CONTACT_AREA = $('#dataFormMasterCONTACT_AREA').closest('td').children();
            var CONTACT_ADDR = $('#dataFormMasterCONTACT_ADDR').closest('td').children();
            CONTACT_ADDR_TYPE.append(' - ').append(CONTACT_AREA).append(CONTACT_ADDR);
            //縮排地址二類別+地址二
            var CONTACT_ADDR_TYPE1 = $('#dataFormMasterCONTACT_ADDR1_TYPE').closest('td');
            var CONTACT_ADDR1 = $('#dataFormMasterCONTACT_ADDR1').closest('td').children();
            CONTACT_ADDR_TYPE1.append(' - ').append(CONTACT_ADDR1);
            //縮排電子郵件+訂閱電子報
            var CONTACT_EMAIL_TYPE = $('#dataFormMasterCONTACT_EMAIL_TYPE').closest('td');
            var CONTACT_EMAIL = $('#dataFormMasterCONTACT_EMAIL').closest('td').children();
            var CONTACT_ISEDM = $('#dataFormMasterCONTACT_ISEDM').closest('td').children();
            CONTACT_EMAIL_TYPE.append(' - ').append(CONTACT_EMAIL).append(' 訂閱電子報 ').append(CONTACT_ISEDM);
            //縮排電子郵件1+訂閱電子報1
            var CONTACT_EMAIL1_TYPE = $('#dataFormMasterCONTACT_EMAIL1_TYPE').closest('td');
            var CONTACT_EMAIL1 = $('#dataFormMasterCONTACT_EMAIL1').closest('td').children();
            var CONTACT_ISEDM1 = $('#dataFormMasterCONTACT_ISEDM1').closest('td').children();
            CONTACT_EMAIL1_TYPE.append(' - ').append(CONTACT_EMAIL1).append(' 訂閱電子報 ').append(CONTACT_ISEDM1);
            //CONTACT_EMAIL1.append(' 訂閱電子報').append(CONTACT_ISEDM1);
            var CONTACT_CELLPHONE = $('#dataFormMasterCONTACT_CELLPHONE').closest('td');
            CONTACT_CELLPHONE.append($('<lable>').css({ color: '#8A2BE2' }).html(' 格式:9999999999  例:0933123456'));
            var CONTACT_TEL_EXT = $('#dataFormMasterCONTACT_TEL_EXT').closest('td');
            CONTACT_TEL_EXT.append($('<lable>').css({ color: '#8A2BE2' }).html(' 格式:999-999999  例:03-3554436'));
            var CONTACT_TEL1_EXT = $('#dataFormMasterCONTACT_TEL1_EXT').closest('td');
            CONTACT_TEL1_EXT.append($('<lable>').css({ color: '#8A2BE2' }).html(' 格式:999-999999  例:03-3554436'));
            //var CONTACT_AREA = $('#dataFormMasterCONTACT_AREA').closest('td');
            //CONTACT_AREA.append($('<lable>').css({ color: '#8A2BE2' }).html(' 請輸入縣市,例:桃園市,台北市,新北市,新竹縣....'));
            var CONTACT_FAX = $('#dataFormMasterCONTACT_FAX').closest('td');
            CONTACT_FAX.append($('<lable>').css({ color: '#8A2BE2' }).html(' 傳真格式:999-999999  例:03-3554436'));
            var CONTACT_TYPE = $('#dataFormMasterCONTACT_TYPE').closest('td');
            CONTACT_TYPE.append($('<lable>').css({ color: '#8A2BE2' }).html(' 請輸入聯絡人職業屬性,例:專業經理人,專業講師,講師,教授....'));
            $('#DialogSubmit', JQDialog4).linkbutton({ text: '開始加入' });
            setTimeout(function () {
                dataGridCenter1Filt();
            }, 100);
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 570 });
            $('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });
            //-----------------------------------讀取ExcelJquery---------------------------------      
            $(Dialog_Import_ID).jbExcelHandlerFileImport({
                OnGetTitleSuccess: function (dataArray, fileName) {
                    //開啟配對視窗                    
                    openForm(Dialog_ImportMain_ID, {}, 'inserted', 'Dialog');
                    //載入選項以及預設
                    $(DataForm_ImportMain_ID).find('.info-combobox').each(function () {
                        $(this).combobox('loadData', dataArray).combobox('clear');
                        $(this).combobox('selectExistsForText', $(this).closest('td').prev('td').html());
                    });
                },
                OnImportSuccess: function (jsonStr) {
                    var json = $.parseJSON(jsonStr);
                    if (!json.IsOK) {
                        var showMessage = json.ErrorMsg;
                        if (json.Result) {
                            showMessage += $('<a>', { href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: '錯誤資料.xls', FilePathName: json.Result }), target: '_blank' })
                                            .html('檔案下載')[0].outerHTML;
                        }
                        $.messager.alert('匯入失敗', showMessage, 'error');
                    }
                    else {
                        $.messager.alert('匯入完成', json.Result);
                        $(dataGridView_ID).datagrid('reload');
                    }
                    closeForm(Dialog_ImportMain_ID);
                }
            });
            //-----------------------------------欄位配對視窗送出按鈕----------------------------
            $('#DialogSubmit', Dialog_ImportMain_ID).removeAttr('onclick').on('click', function () {
                if (!$(DataForm_ImportMain_ID).form('validateForm')) return;    //驗證                    
                var data = $(DataForm_ImportMain_ID).jbDataFormGetAFormData();  //取資料
                $(Dialog_Import_ID).jbExcelHandlerFileImport('importFile', {
                    remoteName: 'sCON_ContactManagement',
                    method: 'ExcelFileImport',
                    handler: data
                });
            });
        });
        var DGVHEADCOUNT_FormatScript = function (value, row, index) {
            if (value > 1) {
                var fieldName = this.field;
                return $('<a>', { href: 'javascript: void(0)', title: '', onclick: "DGVHEADCOUNT_CommandTrigger.call(this,'')" }).linkbutton({ iconCls: '', plain: false, text: value })[0].outerHTML;
            }
        }
        var DGVHEADCOUNT_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));           
            var rowData = $("#dataGridView").datagrid('selectRow', rowIndex).datagrid('getSelected');
            var FiltStr = "CONTACT_NAME = " + "'" + rowData.CONTACT_NAME + "'";
            $("#JQDataGridContactDul").datagrid('setWhere', FiltStr);
            openForm('#JQDialog3', {}, "", 'dialog');
            return true;
        }
        var DGVLABELNUM_FormatScript = function (value, row, index) {
            if (value > 0 ) {
                var fieldName = this.field;
                return $('<a>', { href: 'javascript: void(0)', title: '', onclick: "DGVLABELNUM_CommandTrigger.call(this,'')" }).linkbutton({ iconCls: '', plain: true, text: value })[0].outerHTML;
            }
        }
        var DGVLABELNUM_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
            var rowData = $("#dataGridView").datagrid('selectRow', rowIndex).datagrid('getSelected');
            var CenterName = rowData.CONTACT_NAME+' 標簽列表';
            $("#JQDataGridContactLabel").datagrid('getPanel').panel('setTitle', CenterName);
            $("#JQDataGridContactLabel").datagrid('options').title = CenterName;
            //var FiltStr = "CENTER_ID = " + "'" + rowData.CENTER_ID + "' AND CONTACT_ID = " + "'" + rowData.CONTACT_ID + "'";
            var FiltStr = "CONTACT_ID = " + "'" + rowData.CONTACT_ID + "'";
            $("#JQDataGridContactLabel").datagrid('setWhere', FiltStr);
            openForm('#JQDialog5', {}, "", 'dialog');
            return true;
        }
        //匯入EXCEL
        var openImportExcel = function () {
            alert('此功能測試中,暫時無法使用');
            return false;
            if (GridFlag == 2) {
                alert('注意!!目前選取的是分享群組,無法匯入');
                return false;
            } 
            $(Dialog_Import_ID).dialog('open');
        }
        $(function () {
            //-----------------------------------讀取ExcelJquery----------------------------------
            $(Dialog_Import_ID).jbExcelHandlerFileImport({
                OnGetTitleSuccess: function (ArrayData, FilePathName) {
                    //開啟配對視窗
                    openForm(Dialog_ImportMain_ID, { FilePathName: FilePathName }, 'inserted', 'Dialog');
                    //載入選項以及預設
                    $(Dialog_ImportMain_ID).find('.info-combobox').each(function () {
                        $(this).combobox('loadData', ArrayData).combobox('clear');
                        $(this).combobox('selectExistsForText', $(this).closest('td').prev('td').html());
                    });
                },
                OnImportSuccess: function (jsonStr) {
                    var json = $.parseJSON(jsonStr);
                    if (!json.IsOK) {
                        var showMessage = json.ErrorMsg;
                        if (json.Result) {
                            showMessage += $('<a>', { href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: '錯誤資料.xls', FilePathName: json.Result }), target: '_blank' })
                                            .html('檔案下載')[0].outerHTML;
                        }
                        $.messager.alert('匯入失敗', showMessage, 'error');
                    }
                    else {
                        $.messager.alert('匯入完成', json.Result);
                        $("dataGridView").datagrid('reload');
                    }
                    closeForm(Dialog_ImportMain_ID);
                }
            });
        });
        $(function () {
            $('#BT_SKILL').bind('click', function () {
                openQuerySkillDialog();
            });
        });
        $(function () {
            $('#BT_HOBBY').bind('click', function () {
                openQueryHobbyDialog();
            });
        });
        function dataGridCenter1Filt() {
            var FiltStr = 'CENTER_ID  IN (SELECT DISTINCT CENTER_ID FROM CON_CENTER_AUTHORITY WHERE USERID='+ UserID+')';
            $("#dataGridCenter1").datagrid('setWhere', FiltStr);
            return true;
        }
        function dataGridCenter2Filt() {
            var FiltStr = 'CENTER_ID NOT IN (SELECT DISTINCT CENTER_ID FROM CON_CENTER_AUTHORITY WHERE USERID=' + UserID + ')';
            $("#dataGridCenter2").datagrid('setWhere', FiltStr);
        }
        function dataGridCenter1OnSelect(rowIndex, rowData) {
            if (rowData != null && rowData != undefined) {
                GridFlag = 1;
                var CenterName = '擁有群組_' + rowData.CENTER_CNAME + '_聯絡人列表';
                $("#dataGridView").datagrid('getPanel').panel('setTitle', CenterName);
                $("#dataGridView").datagrid('options').title = CenterName;
                var CenterID = rowData.CENTER_ID;
                var FiltStr = 'B.CENTER_ID =' + CenterID;
                $("#dataGridView").datagrid('setWhere', FiltStr);
                var FiltStr = 'CENTER_ID=' + CenterID;
                $("#CenterLevelGrid").datagrid('setWhere', FiltStr);
            }
        }
        function dataGridCenter2OnSelect(rowIndex, rowData) {
            if (rowData != null && rowData != undefined) {
                GridFlag = 2;
                var Row1_CENTER_ID = GETCENTER1ID();
                var CenterName = '分享群組_'+rowData.CENTER_CNAME + '_聯絡人列表';
                $("#dataGridView").datagrid('getPanel').panel('setTitle', CenterName);
                $("#dataGridView").datagrid('options').title = CenterName;
                var CenterID = rowData.CENTER_ID;
                var FiltStr = 'B.CENTER_ID =' + CenterID;
                FiltStr = FiltStr + ' AND CONTACT_ISPUBLIC=1 AND A.CONTACT_ID NOT IN (SELECT CONTACT_ID FROM CON_CONTACTOWNERS WHERE CENTER_ID=' + Row1_CENTER_ID + ' AND ISACTIVE=1 GROUP BY  CONTACT_ID)';
                $("#dataGridView").datagrid('setWhere', FiltStr);
                var FiltStr = 'CENTER_ID=' + CenterID;
                $("#CenterLevelGrid").datagrid('setWhere', FiltStr);
            }
        }
        //取得擁有群組群組代號
        function GETCENTER1ID() {
            var RowData = $("#dataGridCenter1").datagrid('getSelected');
            var CENTER_ID = RowData.CENTER_ID;
            return CENTER_ID
        }
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' />";
            else
                return "<input  type='checkbox' />";
        }
        function dataFormMasterOnLoadSucess() {
            //var rowData = $("#dataGridView").datagrid('getSelected');
            var rowData = $("#dataGridCenter1").datagrid('getSelected');
            var Center_ID = rowData.CENTER_ID;
            var ContactID = $("#dataFormMasterCONTACT_ID").val();
            var FiltStr = 'CENTER_ID=' + Center_ID;
            $("#dataFormMasterCONTACT_SALES").combobox('setWhere', FiltStr);
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $("#dataFormMasterCONTACT_SALES").combobox('setValue', UserName);
                ContactID = -1;
            }
            var FiltStr = 'CONTACT_ID=' + ContactID;
            $("#JQDataGrid1").datagrid('setWhere', FiltStr);
            var FiltStr = 'CENTER_ID=' + Center_ID;
            $("#dataFormMasterCONTACT_JOB").combobox('setWhere', FiltStr);
            $('#JQDataGrid1').datagrid("reload");
        }
        //加群組
        function AddToGroup() {
            var rows = $('#dataGridView').datagrid("getChecked");
            var count = rows.length;
            if (count == 0) {
                alert('注意!!未選取任何聯絡人,請選取');
                return false;
            }
            var ContactIDStr = '';
             for (var i = 0; i <= rows.length-1; i++) {
                 if (i > 0)
                    ContactIDStr = ContactIDStr + ',' + rows[i].CONTACT_ID; 
                  else 
                    ContactIDStr = ContactIDStr + rows[0].CONTACT_ID;
             }
            //加入聯絡人到群組中,傳入轉入群組代號,聯絡人清單
             var rows = $('#dataGridCenter1').datagrid("getSelected");
             var CENTER_ID = rows.CENTER_ID;
             AddContactToGroup(CENTER_ID, ContactIDStr);
             var Row1_CENTER_ID = GETCENTER1ID();
             var rows = $('#dataGridCenter2').datagrid("getSelected");
             var CenterID = rows.CENTER_ID;
             var FiltStr = 'B.CENTER_ID =' + CenterID;
             FiltStr = FiltStr + ' AND CONTACT_ISPUBLIC=1 AND A.CONTACT_ID NOT IN (SELECT CONTACT_ID FROM CON_CONTACTOWNERS WHERE CENTER_ID=' + Row1_CENTER_ID + ' GROUP BY  CONTACT_ID)';
             $("#dataGridView").datagrid('setWhere', FiltStr);
        }
        //移除群組
        function RemoveFromGroup() {
            if (GridFlag == 2 ) {
                alert('注意!!目前選取資料在分享群組,無需移出群組  !!');
                return false;
            }
            var rows = $('#dataGridView').datagrid("getChecked");
            var count = rows.length;
            if (count == 0) {
                alert('注意!!未選取任何聯絡人,請選取');
                return false;
            }
            var ContactIDStr = '';
            var ContactNameStr = '';
            for (var i = 0; i <= rows.length - 1; i++) {
                if (i > 0) {
                    if (rows[i].AUTHORITY_ID == 2) {
                        ContactIDStr = ContactIDStr + ',' + rows[i].CONTACT_ID;
                        ContactNameStr = ContactNameStr + ',' + rows[i].CONTACT_NAME;
                    }
                }
                else {
                    if (rows[i].AUTHORITY_ID == 2) {
                        ContactIDStr = ContactIDStr + rows[0].CONTACT_ID;
                        ContactNameStr = ContactNameStr + rows[0].CONTACT_NAME;
                    }
                }
            }
            if (ContactIDStr == '') {
                alert('注意!!您未選取任何來源為加入的聯絡人');
                return false;
            }
            var ConfirmYN = confirm("確定要將(" + ContactNameStr + ')移出群組?');
            if (ConfirmYN == true) {
                //加入聯絡人到群組中,傳入轉入群組代號,聯絡人清單
                var rows = $('#dataGridCenter1').datagrid("getSelected");
                var CENTER_ID = rows.CENTER_ID;
                RemoveContactFromGroup(CENTER_ID, ContactIDStr);
                var Row1_CENTER_ID = GETCENTER1ID();
                var rows = $('#dataGridCenter1').datagrid("getSelected");
                var CenterID = rows.CENTER_ID;
                var FiltStr = 'B.CENTER_ID =' + CenterID;
                $("#dataGridView").datagrid('setWhere', FiltStr);
                $('#dataGridView').datagrid('reload');
            }
        }
        //加入群組
        function AddContactToGroup(CENTER_ID, ContactIDStr) {
            //var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCON_ContactManagement.CON_CONTACT',
                data: "mode=method&method=" + "procAddContactToGroup" + " &parameters=" + CENTER_ID + "*" + ContactIDStr + "*" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data == "True") {
                        $('#dataGridCenter1').datagrid("reload");
                    }
                    else {
                        alert("注意!! 聯絡人加入群組失敗")
                    }
                }
            });
        }
        //移除群組
        function RemoveContactFromGroup(CENTER_ID, ContactIDStr) {
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCON_ContactManagement.CON_CONTACT',
                data: "mode=method&method=" + "procRemoveContactFromGroup" + " &parameters=" + CENTER_ID + "*" + ContactIDStr + "*" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data == "True") {
                        $('#dataGridCenter1').datagrid("reload");
                    }
                    else {
                        alert("注意!! 聯絡人移除群組失敗")
                    }
                }
            });
        }
        function AddLabel() {
            if (GridFlag == 2) {
                alert('注意!!需在擁有群組才可貼上標籤!!');
                return false;
            }
            var rows = $('#CenterLevelGrid').datagrid("getRows");
            if (rows.length <= 0) {
                alert('注意!!未建立群組標簽,請建立');
                return false;
            }
            var rows = $('#dataGridView').datagrid("getChecked");
            if (rows.length <= 0) {
                alert('注意!!未選取聯絡人,請選取');
                return false;
            }
            openForm('#JQDialog2', $('#dataGridView').datagrid('getSelected'), "update", 'dialog');
        }
        function JQDataForm1OnApply() {
            var lv = $("#JQDataForm1LABELVALUE").combobox('getValue');
            // 2019/12/31 取消必填
            //if (lv == "" || lv == "undefined") {
            //    alert('注意!!請選取或輸入標籤值');
            //    $('#JQDataForm1LABELVALUE').combobox().next('span').find('input').focus();
            //    return false;
            //}
            if (getEditMode($("#JQDataForm1")) == 'update') {
                var rows = $('#dataGridView').datagrid("getChecked");
                var count = rows.length;
                var ContactIDStr = '';
                for (var i = 0; i <= rows.length - 1; i++) {
                    if (i > 0)
                        ContactIDStr = ContactIDStr + ',' + rows[i].CONTACT_ID;
                    else
                        ContactIDStr = ContactIDStr + rows[0].CONTACT_ID;
                }
                var rows = $('#dataGridCenter1').datagrid("getSelected");
                var CENTER_ID = rows.CENTER_ID;
                var LABEL_ID = $('#JQDataForm1LABEL_ID').combobox('getValue');
                var LABELVALUE = $('#JQDataForm1LABELVALUE').combobox('getValue');
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sCON_ContactManagement.CON_CONTACT',
                    data: "mode=method&method=" + "procAddContactsLabel" + " &parameters=" + CENTER_ID + "*" + LABEL_ID + "*" + LABELVALUE +"*"+ ContactIDStr + "*" + UserID,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data == "True") {
                        }
                        else {
                            alert("注意!! 聯絡人加入標籤失敗")
                        }
                        $('#dataGridView').datagrid('reload');
                    }
                });
               
            };
        }
        function JQDataForm1OnLoadSucess() {
            var rows = $('#CenterLevelGrid').datagrid("getSelected");
            $("#JQDataForm1LABEL_ID").combobox('setWhere', "CENTER_ID=" + rows.CENTER_ID);
            $("#JQDataForm1LABEL_ID").combobox('setValue', rows.LABEL_ID);
            var FiltStr = "CENTER_ID=" + rows.CENTER_ID + ' AND LABEL_ID=' + rows.LABEL_ID;
            $("#JQDataForm1LABELVALUE").combobox('setWhere', FiltStr);
        }
        function dataGridViewOnInsert() {
            if (GridFlag == 2) {
                alert('注意!!需在擁有群組才可新增聯絡人!!');
                return false;
            }
            var rowData = $("#dataGridCenter1").datagrid('getSelected');
            var Center_ID = rowData.CENTER_ID;
        }
        function dataGridViewOnUpdate(RowData) {
            if (GridFlag == 2) {
                //alert('注意!!需在擁有群組修改基本資料!!');
                openForm('#JQDialog1', RowData, "viewed", 'dialog');
                return false;
            }
            var AUTHORITY_ID = RowData.AUTHORITY_ID;
            if (GridFlag == 1 && AUTHORITY_ID != 1) {
                //alert('注意!!需是擁有者才可修改基本資料!!');
                openForm('#JQDialog1', RowData, "viewed", 'dialog');
                return false;
            }
        }
        function dataFormMasterOnApply() {
             if (CheckContactIsExist() == false) {
                alert('注意!!聯絡人姓名與手機號碼已存在')
                return false;
            }
        }
        function CheckContactIsExist() {
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var CONTACT_NAME = $("#dataFormMasterCONTACT_NAME").val();
                var CONTACT_CELLPHONE = $('#dataFormMasterCONTACT_CELLPHONE').val();
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sCON_ContactManagement.CON_CONTACT', 
                    data: "mode=method&method=" + "CheckContactIsExist" + "&parameters=" + CONTACT_NAME + "," + CONTACT_CELLPHONE, 
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });
                if ((cnt == "0") || (cnt == "undefined")) {
                    return true;
                }
                else {
                    alert('提示!!聯絡人姓名與手機號碼已存在');
                    return true;
                }
            }
            return true;
        }
        function dataFormMasterOnApplied() {
             //$('#dataGridCenter1').datagrid("reload")
        }
        function queryGrid(dg) {
             if ($(dg).attr('id') == 'dataGridView') {
                var result = [];
                var result1 = [];
                var aVal = '';
                var bVal = '';
                aVal = $('#CENTER_ID_Query').combobox('getValue');
                if (aVal != '')
                    result.push("B.CENTER_ID = '" + aVal + "'");
                aVal = $('#CONTACT_JOB_Query').combobox('getValue');
                if (aVal != '')
                    result.push("CONTACT_JOB = '" + aVal + "'");
                aVal = $('#CONTACT_NAME_Query').val();
                if (aVal != '')
                    result.push("CONTACT_NAME LIKE '%" + aVal + "%'");
                aVal = $('#CONTACT_COMPANY_Query').val();
                if (aVal != '')
                    result.push("CONTACT_COMPANY LIKE '%" + aVal + "%'");
                aVal = $('#CONTACT_AREA_Query').combobox('getValue');
                if (aVal != '')
                    result.push("CONTACT_AREA = '" + aVal + "'");
                aVal = $('#CONTACT_TRADE_Query').combobox('getValue');
                if (aVal != '')
                    result.push("CONTACT_TRADE = '" + aVal + "'");
                aVal = $('#CONTACT_SALES_Query').combobox('getValue');
                if (aVal != '')
                    result.push("CONTACT_SALES = '" + aVal + "'");
                aVal = $('#CONTACT_DEPT_Query').combobox('getValue');
                if (aVal != '')
                    result.push("CONTACT_DEPT = '" + aVal + "'");
                var filtstr1 = result.join(' and ');
                var filtstr2_1 = '';
                var filtstr2_2 = '';
                var filtstr2_3 = '';
                var filtstr2_4 = '';
                var filtstr2_5 = '';
                var filtstr3_1 = '';
                var filtstrisnotok = '';
                var filtstrAddLabel = '';
                var label1f = $("#LABELFILTER1_Query").combobox('getValue');
                var label1v = $("#LABELVALUE1_Query").combobox('getText');
                var label2f = $("#LABELFILTER2_Query").combobox('getValue');
                var label2v = $("#LABELVALUE2_Query").combobox('getText');
                var label3f = $("#LABELFILTER3_Query").combobox('getValue');
                var label3v = $("#LABELVALUE3_Query").combobox('getText');
                var label4f = $("#LABELFILTER4_Query").combobox('getValue');
                var label4v = $("#LABELVALUE4_Query").combobox('getText');
                var label5f = $("#LABELFILTER5_Query").combobox('getValue');
                var label5v = $("#LABELVALUE5_Query").combobox('getText');
                var isnotok = $("#ISNOTOK_Query").checkbox('getValue');
                var isaddlabel = $("#ISNOTLABEL_Query").checkbox('getValue');
                var isactivity = $("#ISACTIVITY_Query").checkbox('getValue');
                var isnotactivity = $("#ISNOTACTIVITY_Query").checkbox('getValue');
                if ((label1f != '') && (label1v != '')) {
                    filtstr2_1 = "(CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label1f + "' AND LABELVALUE='" + label1v + "')"
                }
                if ((label2f != '') && (label2v != '')) {
                    if (label2f == label1f)
                        filtstr2_1 = filtstr2_1 + " OR (CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label2f + "' AND LABELVALUE='" + label2v + "')"
                      else
                        filtstr2_2 = "(CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label2f + "' AND LABELVALUE='" + label2v + "')"
 
                }
                if ((label3f != '') && (label3v != '')) {
                    if (label3f == label1f)
                        filtstr2_1 = filtstr2_1 + " OR (CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label3f + "' AND LABELVALUE='" + label3v + "')"
                    else
                        if (label3f == label2f)
                            filtstr2_2 = filtstr2_2 + " OR (CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label3f + "' AND LABELVALUE='" + label3v + "')"
                           else
                              filtstr2_3 = "(CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label3f + "' AND LABELVALUE='" + label3v + "')"
                }
                if ((label4f != '') && (label4v != '')) {
                    if (label4f == label1f)
                        filtstr2_1 = filtstr2_1 + " OR (CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label4f + "' AND LABELVALUE='" + label4v + "')"
                    else
                        if (label4f == label2f)
                            filtstr2_2 = filtstr2_2 + " OR (CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label4f + "' AND LABELVALUE='" + label4v + "')"
                        else
                            if (label4f == label3f)
                                filtstr2_3 = filtstr2_3 + " OR (CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label4f + "' AND LABELVALUE='" + label4v + "')"
                            else
                                filtstr2_4 = "(CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label4f + "' AND LABELVALUE='" + label4v + "')"
                }
                if ((label5f != '') && (label5v != '')) {
                    if (label5f == label1f)
                        filtstr2_1 = filtstr2_1 + " OR (CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label5f + "' AND LABELVALUE='" + label5v + "')"
                    else
                        if (label5f == label2f)
                            filtstr2_2 = filtstr2_2 + " OR (CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label5f + "' AND LABELVALUE='" + label5v + "')"
                        else
                            if (label5f == label3f)
                                filtstr2_3 = filtstr2_3 + " OR (CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label5f + "' AND LABELVALUE='" + label5v + "')"
                            else
                              if (label5f == label4f)
                                  filtstr2_4 = filtstr2_4 + "(CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label5f + "' AND LABELVALUE='" + label5v + "')"
                                 else
                                  filtstr2_5 = "(CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + label5f + "' AND LABELVALUE='" + label5v + "')"
                }
                if (filtstr2_1 != '') {
                    filtstr2_1 = ' AND ( A.CONTACT_ID IN (SELECT CONTACT_ID FROM CON_CONTACTLABEL WHERE (' + filtstr2_1 + '))';
                }
           
                if (filtstr2_2 != '') {
                    if (filtstr2_1 != '') {

                        filtstr2_2 = ' OR  A.CONTACT_ID IN (SELECT CONTACT_ID FROM CON_CONTACTLABEL WHERE (' + filtstr2_2 + '))';
                    }
                    else {
                        filtstr2_2 = '  OR (A.CONTACT_ID IN (SELECT CONTACT_ID FROM CON_CONTACTLABEL WHERE (' + filtstr2_2 + '))';
                    }
                }
                if (filtstr2_3 != '') {
                    if (filtstr2_1 != '' || filtstr2_2 != '') {
                        filtstr2_3 = ' OR  A.CONTACT_ID IN (SELECT CONTACT_ID FROM CON_CONTACTLABEL WHERE (' + filtstr2_3 + '))';
                    }
                    else {
                        filtstr2_3 = ' OR (A.CONTACT_ID IN (SELECT CONTACT_ID FROM CON_CONTACTLABEL WHERE (' + filtstr2_3 + '))';
                    }
                }
                if (filtstr2_4 != '') {
                    if (filtstr2_1 != '' || filtstr2_2 != '' || filtstr2_3 != '') {
                        filtstr2_4 = ' OR  A.CONTACT_ID IN (SELECT CONTACT_ID FROM CON_CONTACTLABEL WHERE (' + filtstr2_4 + '))';
                    }
                    else {
                        filtstr2_4 = ' OR  (A.CONTACT_ID IN (SELECT CONTACT_ID FROM CON_CONTACTLABEL WHERE (' + filtstr2_4 + '))';
                    }
                }
                if (filtstr2_5 != '') {
                    if (filtstr2_1 != '' || filtstr2_2 != '' || filtstr2_3 != '' || filtstr2_4 != '') {
                        filtstr2_5 = ' OR  A.CONTACT_ID IN (SELECT CONTACT_ID FROM CON_CONTACTLABEL WHERE (' + filtstr2_5 + '))';
                    }
                    else {
                        filtstr2_5 = ' OR (A.CONTACT_ID IN (SELECT CONTACT_ID FROM CON_CONTACTLABEL WHERE (' + filtstr2_5 + '))';
                    }
                }
                var _sp=''; 
                if (isnotok == 1) {
                    if ((filtstr1 != '') || (filtstr2_1 != '') || (filtstr2_2 != '') || (filtstr2_3 != '') || (filtstr2_4 != '') || (filtstr2_5 != '')) {
                        filtstrisnotok = " and (A.Contact_JOB='" + _sp + "' OR A.Contact_Area='" + _sp + "' OR A.Contact_ADDR='" + _sp + "' OR A.Contact_Trade='" + _sp + "' )";
                    }
                    else {
                        filtstrisnotok = " (A.Contact_JOB='" + _sp + "' OR A.Contact_Area='" + _sp + "' OR A.Contact_ADDR='" + _sp + "' OR A.Contact_Trade='" + _sp + "' )";
                    }
                }
                var _sp1 = ''
                if (isaddlabel == 1) {
                    filtstrAddLabel = " and ( A.CONTACT_LABELQTY <=0 )";
                }
                var _filtstrend = ''
                if (filtstr2_1 + filtstr2_2 + filtstr2_3 + filtstr2_4 + filtstr2_5 != '') {
                    var _filtstrend = ')'
                }
                var aVal = '';
                var bVal = '';
                aVal = $('#CENTER_ID_Query').combobox('getValue');
                bVal = $('#CONTACT_SALES_Query').combobox('getValue');
                var activitystr = ''
                if (isactivity == 1) {
                    if (aVal != '') {
                        activitystr = " AND A.CONTACT_ID IN (SELECT DISTINCT CONTACT_ID FROM CON_ACTIVITYDETAILS WHERE CENTER_ID_FROM = '" + aVal + "' AND CONTACT_ISACTIVE =1)"
                    }
                    if ((aVal != '') && (bVal != '')) {
                        activitystr = " AND A.CONTACT_ID IN (SELECT DISTINCT CONTACT_ID FROM CON_ACTIVITYDETAILS WHERE CENTER_ID_FROM = '" + aVal + "' AND CREATE_MAN ='"+ bVal +"'  AND  Contact_ISACTIVE =1)"
                    }
                }
                //var aVal = '';
                //var bVal = '';
                var notactivitystr = ''
                if (isnotactivity == 1) {
                   if (aVal != '') {
                        notactivitystr = " AND A.CONTACT_ID NOT IN (SELECT DISTINCT CONTACT_ID FROM CON_ACTIVITYDETAILS WHERE CENTER_ID_FROM = '" + aVal + "' AND CONTACT_ISACTIVE =1)"
                    }
                    if ((aVal != '') && (bVal != '')) {
                        notactivitystr = " AND A.CONTACT_ID NOT IN (SELECT DISTINCT CONTACT_ID FROM CON_ACTIVITYDETAILS WHERE CENTER_ID_FROM = '" + aVal + "' AND CREATE_MAN ='" + bVal + "'  AND  Contact_ISACTIVE =1)"
                    }
                }
                var sdate = $('#SDate_Query').datebox('getValue');
                var edate = $('#EDate_Query').datebox('getValue');
                if (sdate != '' && edate != '') {
                    filtstr3_1 = " AND A.Create_Man='"+ UserName +"' AND A.Create_Date between '" + sdate + "'" + " AND '" + edate + "'";
                    }
                var filtstr = filtstr1 + filtstr2_1 + filtstr2_2 + filtstr2_3 + filtstr2_4 + filtstr2_5 + filtstr3_1 + _filtstrend + filtstrisnotok + filtstrAddLabel + activitystr + notactivitystr;
                $(dg).datagrid('setWhere', filtstr);
                GridFlag=1
             }
             if ($(dg).attr('id') == 'JQDataGridActivityBills') {
                aVal = $('#ACTIVITY_ID_Query').combobox('getValue');
                if (aVal == '') {
                    alert('注意!!請選擇活動');
                    return false;
                }
                var CreateMan = $('#CREATE_MAN_Query').combobox('getValue');
                var rowData = $("#dataGridCenter1").datagrid('getSelected');
                var CENTER_ID = rowData.CENTER_ID;
                var ACTIVITY_ID = $("#ACTIVITY_ID_Query").combobox('getValue');
                var filtstr = "";
                    filtstr = 'CENTER_ID_FROM=' + CENTER_ID;
                    filtstr = filtstr + ' AND ACTIVITY_ID = ' + ACTIVITY_ID;
                if (CreateMan != "") {
                    filtstr = filtstr + " AND ACT.CREATE_MAN = '" + CreateMan + "'";
                }
                $(dg).datagrid('setWhere', filtstr);
                   
            }
        }
        function initQuerySkillDialog() {
            $("#Dialog_Skill").dialog(
            {
                height:400,
                width: 400,
                resizable: false,
                modal: true,
                title: "專長選項",
                closed: true,
                buttons: [
                {
                    text: "確認",
                    handler: function () {
                        var skillName = "";
                        var skillID = "";
                        var skillRows = $("#DG_SKILL").datagrid("getRows");
                        var checkedItems = $('#DG_SKILL').datagrid('getChecked');
                        var flag = "N";
                        for (var k = 0; k < skillRows.length; k++) {
                            //判斷有勾選的 update 為 "Y"
                            flag = "N"
                            $.each(checkedItems, function (index, item) {
                                if (skillRows[k].CODE_ID == item.CODE_ID) {
                                    if (skillRows[k].CODE_ID == item.CODE_ID) {
                                        skillRows[k].IS_SELECTED = "Y";
                                        flag = "Y";
                                        skillName = skillName + skillRows[k].NAME + ",";
                                        skillID = skillID + skillRows[k].CODE_ID + ",";
                                    }
                                }
                            });
                            if (flag != "Y")
                                skillRows[k].IS_SELECTED = "N";
                        }
                        skillName = skillName.substr(0, skillName.length - 1);
                        $("#dataFormMasterCONTACT_SKILL_NAME").val(skillName);
                        $("#dataFormMasterCONTACT_SKILL_ID").val(skillID);
                        $("#Dialog_Skill").dialog("close");
                    }
                },
                {
                    text: '取消',
                    handler: function () { $("#Dialog_Skill").dialog("close") }
                }

                ]
            });
        };
        function initQueryHobbyDialog() {
            $("#Dialog_Hobby").dialog(
            {
                height: 400,
                width: 400,
                resizable: false,
                modal: true,
                title: "興趣選項",
                closed: true,
                buttons: [
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
                        hobbyName = hobbyName.substr(0, hobbyName.length - 1);
                        $("#dataFormMasterCONTACT_HOBBY_NAME").val(hobbyName);
                        $("#dataFormMasterCONTACT_HOBBY_ID").val(hobbyID);
                        $("#Dialog_Hobby").dialog("close");
                    }
                },
                {
                    text: '取消',
                    handler: function () { $("#Dialog_Hobby").dialog("close") }
                }]
            });
        };
        function openQuerySkillDialog() {
            var skillList = $("#dataFormMasterCONTACT_SKILL_ID").val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCON_ContactManagement.CON_CONTACT', //連接的Server端，command
                data: "mode=method&method=" + "GetSkillHobbyData" + "&parameters=" + "SKILL" + "," + skillList, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                    if (rows.length > 0) {
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
        function openQueryHobbyDialog() {
            var hobbyList = $("#dataFormMasterCONTACT_HOBBY_ID").val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCON_ContactManagement.CON_CONTACT', //連接的Server端，command
                data: "mode=method&method=" + "GetSkillHobbyData" + "&parameters=" + "HOBBY" + "," + hobbyList, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                    if (rows.length > 0) {
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
        function dataGridCenter2OnLoadSuccess() {
            if (!$(this).data('tag') && $(this).data('tag', true)) {
                dataGridCenter2Filt();
            }
        }
        function DeleContact() {
            var flag = 0;
            var rowData = $("#dataGridView").datagrid('getSelected');
            var CONTACT_ID = rowData.CONTACT_ID;
            if (GridFlag == 2) {
                alert('注意!!需在擁有群組才可刪除聯絡人!!');
                return false;
            }
            if (rowData.AUTHORITY_ID == 2) {
                alert('注意!!人脈來源需為擁有才可刪除聯絡人!!');
                return false;
            }
            var rows = $('#dataGridView').datagrid("getChecked");
            var ConfirmYN = confirm("確定要刪除已選取的(" + rows.length + ')位聯絡人?');
            if (ConfirmYN == false){
                return false;
            }
            var ConIDStr = '';
            var rows = $('#dataGridView').datagrid("getChecked");
            for (var i = 0; i <= rows.length - 1 ; i++) {
                 if (i > 0)
                    ConIDStr = ConIDStr + ',' + rows[i].CONTACT_ID;
                  else
                    ConIDStr = ConIDStr + rows[i].CONTACT_ID;
            }
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCON_ContactManagement.CON_CONTACT',
                data: "mode=method&method=" + "DeleteContact" + " &parameters=" + ConIDStr + "*" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data == 'True') {
                        flag = 1;
                    }
                    else {
                        alert("注意!! 刪除聯絡人失敗");
                    }
                }
            });
            $('#dataGridView').datagrid("reload");
            //$('#dataGridCenter1').datagrid("reload");
            //$('#dataGridCenter2').datagrid("reload");
            return flag;
        }
        function JQDataGrid1OnDeleted() {
            var CONTACT_ID = $("#dataFormMasterCONTACT_ID").val();
            var flag = 0;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCON_ContactManagement.CON_CONTACT', //連接的Server端，command
                data: "mode=method&method=" + "UpdateContactLabelQty" + " &parameters=" + CONTACT_ID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data == 'True') {
                        flag = 1;
                    }
                    else {
                        //alert("注意!! 刪除聯絡人失敗");
                    }
                }
            });
            $('#dataGridView').datagrid('reload');
            return true;
        }
        function GetCenterID() {
            var rowData = $("#dataGridCenter1").datagrid('getSelected');
            return rowData.CENTER_ID;
        }
        function GetDefaultSales() {
            //var rowData = $("#dataGridCenter1").datagrid('getSelected');
            return UserName;
        }
        function LABELFILTER1OnSelect() {
            var CLID = $('#LABELFILTER1_Query').combobox('getValue');
            $("#LABELVALUE1_Query").combobox('setWhere', "CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + CLID + "'");
        }
        function LABELFILTER2OnSelect() {
            var CLID = $('#LABELFILTER2_Query').combobox('getValue');
            $("#LABELVALUE2_Query").combobox('setWhere', "CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + CLID + "'");
        }
        function LABELFILTER3OnSelect() {
            var CLID = $('#LABELFILTER3_Query').combobox('getValue');
            $("#LABELVALUE3_Query").combobox('setWhere', "CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + CLID + "'");
        }
        function LABELFILTER4OnSelect() {
            var CLID = $('#LABELFILTER4_Query').combobox('getValue');
            $("#LABELVALUE4_Query").combobox('setWhere', "CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + CLID + "'");
        }
        function LABELFILTER5OnSelect() {
            var CLID = $('#LABELFILTER5_Query').combobox('getValue');
            $("#LABELVALUE5_Query").combobox('setWhere', "CONVERT(NVARCHAR(10),CENTER_ID)+CONVERT(NVARCHAR(10),LABEL_ID)='" + CLID + "'");
        }
        function dataGridViewOnLoadSucess() {
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                $(this).datagrid({
                    singleSelect: true,
                    selectOnCheck: false,
                    checkOnSelect: false
                });
            }
            var rowData = $("#dataGridCenter1").datagrid('getSelected');
            $("#LABELFILTER1_Query").combobox('setWhere', "CENTER_ID=" + rowData.CENTER_ID);
            $("#LABELFILTER2_Query").combobox('setWhere', "CENTER_ID=" + rowData.CENTER_ID);
            $("#LABELFILTER3_Query").combobox('setWhere', "CENTER_ID=" + rowData.CENTER_ID);
            $("#LABELFILTER4_Query").combobox('setWhere', "CENTER_ID=" + rowData.CENTER_ID);
            $("#LABELFILTER5_Query").combobox('setWhere', "CENTER_ID=" + rowData.CENTER_ID);
            $("#CONTACT_SALES_Query").combobox('setWhere', "CENTER_ID=" + rowData.CENTER_ID);
        }
        //取得人脈群組的職稱資料,在CONTACT_JOB QUERY DEFAULTMETHOD 中觸發
        function GetCenterJOB() {
            var rowData = $("#dataGridCenter1").datagrid('getSelected');
            var FiltStr = 'CENTER_ID=' + rowData.CENTER_ID;
            $('#CONTACT_JOB_Query').combobox('setWhere', "1=1");
            return '';
        }
        //整合聯絡人
        function ConcordContact() {
             if (GridFlag == 2) {
                alert('注意!!目前選取資料在分享群組,無法整合聯絡人!!');
                return false;
             }
             var Checkedrows = $('#JQDataGridContactDul').datagrid("getChecked");
             if (Checkedrows.length < 2) {
                 alert('注意!!選取必須2位聯絡人以上');
                 return false;
             }
             var rowData = $('#dataGridView').datagrid('getSelected');
             var ContactName = rowData.CONTACT_NAME;
             var rowData1 = $('#JQDataGridContactDul').datagrid("getSelected");
             var ContactIDStr = rowData1.CONTACT_ID;
             var ConIDStr = '';
             var ConfirmYN = confirm("確定整合聯絡人到(" + rowData1.CENTER_NAME_SOU + '-' + rowData.CONTACT_NAME + ')?');
             if  (ConfirmYN == true) {
                 var rows = $('#JQDataGridContactDul').datagrid("getChecked");
                 for (var i = 0; i<= rows.length-1 ; i++) {
                     if (i > 0)
                            ConIDStr = ConIDStr + ',' + rows[i].CONTACT_ID;
                          else 
                            ConIDStr = ConIDStr +'('+ rows[i].CONTACT_ID;
                }
                if (ConIDStr != '') {
                    ConIDStr = ConIDStr + ')';
                }
                var UserID = getClientInfo("UserID");
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sCON_ContactManagement.CON_CONTACT',
                    data: "mode=method&method=" + "procConcordContact" + " &parameters=" + ContactIDStr + "*" + ContactName + "*" + ConIDStr + "*" + UserID,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data == "True") {
                            $("#JQDataGridContactDul").datagrid('reload');
                            $("#dataGridView").datagrid('reload');
                        }
                        else {
                            alert("注意!!整合聯絡人失敗")
                        }
                    }
                })
            };
        }
        //加入活動
        function AddToActivity() {

            if (GridFlag != 1) {
                alert("注意!!需在擁有群組的狀態下,才可加入活動");
                return false;
            }
            var rowData = $("#dataGridCenter1").datagrid('getSelected');
            $("#CREATE_MAN_Query").combobox('setWhere', "CENTER_ID_FROM = " + rowData.CENTER_ID);
            var CenterName = rowData.CENTER_CNAME + '_推薦參加活動列表';
            $("#JQDataGridActivityBills").datagrid('getPanel').panel('setTitle', CenterName);
            $("#JQDataGridActivityBills").datagrid('options').title = CenterName;

            openForm('#JQDialog4', {}, "", 'dialog');
        }
        function JQDialog3OnSubmited() {
        }
        function JQDialog5OnSubmited() {
        }
        function exportGridCustom() {
            if (GridFlag != 1) {
                alert("注意!!需在擁有群組的狀態下,才可匯出Excel");
                return false;
            }
            var kk = returnISExportUser();
            if (returnISExportUser() == -1) {
                alert('注意!!,你無權限匯出Excel,請洽企劃室');
                return false;
            }
            var kkk = exportGrid("#dataGridView");
        }
        //加入活動確認
        function JQDialog4OnSubmited() {
            var rows = $('#dataGridView').datagrid("getChecked");
            if (rows.length == 0) {
                alert('注意!!未選擇任何連絡人,請選取');
                return false;
            }
            var Activity_ID = $("#ACTIVITY_ID_Query").combobox('getValue');
            if ((Activity_ID == "") || (Activity_ID == "undefined")) {
                alert('注意,請選取要加入的活動');
                return false;
            }
            var Activity_Name = $("#ACTIVITY_ID_Query").combobox('getText');
            var ConfirmYN = confirm("確定要選取人員加入(" + Activity_Name + ')活動中?');
            if (ConfirmYN == true) {
                var ConIDStr = '';
                var rows = $('#dataGridView').datagrid("getChecked");
                for (var i = 0; i <= rows.length - 1 ; i++) {
                    if (i > 0)
                        ConIDStr = ConIDStr + ',' + rows[i].CONTACT_ID;
                    else
                        ConIDStr = ConIDStr + rows[i].CONTACT_ID;
                }
                var Center1row = $('#dataGridCenter1').datagrid("getSelected");
                var Center_ID_From = Center1row.CENTER_ID;
                var UserID = getClientInfo("UserID");
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sCON_ContactManagement.CON_CONTACT',
                    data: "mode=method&method=" + "procAddContactToActivity" + " &parameters=" + Activity_ID + "*" + ConIDStr + "*" + Center_ID_From + "*" + UserID,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data == "True") {
                            $("#JQDataGridActivityBills").datagrid('reload');
                            $('#dataGridView').datagrid('uncheckAll');
                         }
                        else {
                            alert("注意!!加入活動失敗")
                        }
                    }
                })
            }
            return true;
        }
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox'  onclick='return false;'  />";
        }
        function JQDataGridContactDulOnLoadSucess() {
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                $(this).datagrid({
                    singleSelect: true,
                    selectOnCheck: false,
                    checkOnSelect: false
                });
            }
            //預設全部選取
            $('#JQDataGridContactDul').datagrid('checkAll');
        }
        //移出活動
        function RemoveFromActivity() {
            var dataRow = $('#dataGridCenter1').datagrid("getSelected");
            var CENTER_ID = dataRow.CENTER_ID;
            var rows = $("#JQDataGridActivityBills").datagrid("getChecked");
            if (rows.length == 0) {
                alert('注意!!未選取任何聯絡人,請選取');
                return false;
            }
            var ContactIDStr = '';
            var ContactNameStr = '';
            for (var i = 0; i <= rows.length - 1; i++) {
                if (i > 0) {
                    ContactIDStr = ContactIDStr + ',' + rows[i].CONTACT_ID;
                    ContactNameStr = ContactNameStr + ',' + rows[i].CONTACT_NAME;
                }
                else {
                    ContactIDStr = ContactIDStr + rows[0].CONTACT_ID;
                    ContactNameStr = ContactNameStr + rows[0].CONTACT_NAME;
                }
            }
            var ConfirmYN = confirm("確定要將(" + ContactNameStr + ')移出活動?');
            if (ConfirmYN == true) {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sCON_ContactManagement.CON_CONTACT',
                    data: "mode=method&method=" + "procRemoveContactFromActivity" + " &parameters=" + CENTER_ID + "*" + ContactIDStr + "*" + UserID,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data == "True") {
                            $('#JQDataGridActivityBills').datagrid('reload');
                        }
                        else {
                            alert("注意!! 聯絡人移除活動失敗")
                        }
                    }
                });
            }
        }
        function returnISExportUser() {
            var UserID = getClientInfo("UserID");
            var row = $('#JQDataGridVal').datagrid('getSelected');
            var ViewList = row.ExportExcelUser;
            var UserRight = ViewList.indexOf(UserID);
            return UserRight;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
          <div id="Layout_Base" class="easyui-layout" style="height: 655px;">
            <div data-options="region:'west',split:true,border:false" title="" style="width: 170px; margin-right: 101px;">
                <JQTools:JQDataGrid ID="dataGridCenter1" runat="server" AutoApply="False" DataMember="GROUPTYPE" Pagination="False" ParentObjectID="" RemoteName="sCON_ContactManagement.GROUPTYPE" Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="20" QueryAutoColumn="False" QueryLeft="100px" QueryMode="Window" QueryTitle="查詢" QueryTop="100px" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" CheckOnSelect="True" RecordLock="False" ColumnsHibeable="False" RecordLockMode="None" BufferView="False" NotInitGrid="False" RowNumbers="True" Width="185px" OnSelect="dataGridCenter1OnSelect">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="擁有群組" Editor="infocombobox" FieldName="CENTER_ID" Width="110" Visible="False" EditorOptions="valueField:'CENTER_ID',textField:'CENTER_CNAME',remoteName:'sCON_ContactManagement.CENTER',tableName:'CENTER',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="擁有群組" Editor="text" FieldName="CENTER_CNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="人數" Editor="text" FieldName="HeadCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="63">
                        </JQTools:JQGridColumn>
                    </Columns>
                 </JQTools:JQDataGrid>
                 <JQTools:JQDataGrid ID="dataGridCenter2" runat="server" AutoApply="False" DataMember="GROUPTYPE1" Pagination="False" ParentObjectID="" RemoteName="sCON_ContactManagement.GROUPTYPE1" Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="50" QueryAutoColumn="False" QueryLeft="100px" QueryMode="Window" QueryTitle="查詢" QueryTop="100px" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" CheckOnSelect="True" RecordLock="False" ColumnsHibeable="False" RecordLockMode="None" BufferView="False" NotInitGrid="False" RowNumbers="True" Width="185px" OnSelect="dataGridCenter2OnSelect" OnLoadSuccess="dataGridCenter2OnLoadSuccess">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="分享群組" Editor="infocombobox" FieldName="CENTER_ID" Width="120" Visible="False" EditorOptions="valueField:'CENTER_ID',textField:'CENTER_CNAME',remoteName:'sCON_ContactManagement.CENTER',tableName:'CENTER',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="分享群組" Editor="text" FieldName="CENTER_CNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="人數" Editor="text" FieldName="HeadCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                        </JQTools:JQGridColumn>
                    </Columns>
                </JQTools:JQDataGrid>
                <JQTools:JQDataGrid ID="CenterLevelGrid" runat="server" AutoApply="False" DataMember="CENTERLEVEL" Pagination="False" ParentObjectID="" RemoteName="sCON_ContactManagement.CENTERLEVEL" Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="100px" QueryMode="Window" QueryTitle="查詢" QueryTop="100px" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" CheckOnSelect="True" RecordLock="False" ColumnsHibeable="False" RecordLockMode="None" BufferView="False" NotInitGrid="False" RowNumbers="True" Width="185px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="LABEL_ID" Editor="text" FieldName="LABEL_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CENTER_ID" Editor="text" FieldName="CENTER_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="群組共用標簽" Editor="text" FieldName="LABELNAME" Width="120" Visible="True" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>

            <div data-options="region:'center',title:'',border:false" title="">
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCON_ContactManagement.CON_CONTACT" runat="server" AutoApply="True"
                DataMember="CON_CONTACT" Pagination="True" QueryTitle="聯絡人查詢" EditDialogID="JQDialog1"
                Title="聯絡人維護" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="15,30,45,90" PageSize="15" QueryAutoColumn="False" QueryLeft="180px" QueryMode="Window" QueryTop="40px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" EnableTheming="True" OnUpdate="dataGridViewOnUpdate" OnInsert="dataGridViewOnInsert" OnLoadSuccess="dataGridViewOnLoadSucess">
                <Columns>
                   <%-- <JQTools:JQGridColumn Alignment="left" Caption="CENTER_ID" Editor="text" FieldName="B.CENTER_ID" MaxLength="0" Visible="False" Width="80" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="人脈群組-來源" Editor="text" FieldName="CENTER_NAME_SOU" Format="" Visible="true" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="來源" Editor="text" FieldName="AUTHORITY_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="性別" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'男',selected:'false'},{value:'0',text:'女',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_GENDER" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="來源" Editor="text" EditorOptions="" FieldName="AUTHORITY_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="35">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="CONTACT_ID" Editor="numberbox" FieldName="CONTACT_ID" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="CONTACT_NAME" Format="" Visible="True" Width="70" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="標簽" Editor="text" FieldName="CONTACT_LABELQTY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" FormatScript="DGVLABELNUM_FormatScript">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="重複" Editor="text" FieldName="HEADCOUNT" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="48" FormatScript="DGVHEADCOUNT_FormatScript">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人姓名" Editor="text" FieldName="CONTACT_ENAME" Format="" MaxLength="0" Visible="False" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="任職公司" Editor="text" FieldName="CONTACT_COMPANY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="160">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="職稱" Editor="text" FieldName="CONTACT_JOB" Format="" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="CONTACT_TEL" Format="" MaxLength="0" Visible="true" Width="75" />
                    <JQTools:JQGridColumn Alignment="left" Caption="分機" Editor="text" FieldName="CONTACT_TEL_EXT" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="手機" Editor="text" FieldName="CONTACT_CELLPHONE" Format="" MaxLength="0" Visible="true" Width="75" />
                    <JQTools:JQGridColumn Alignment="left" Caption="郵件信箱" Editor="text" FieldName="CONTACT_EMAIL" Format="" MaxLength="0" Visible="true" Width="160" />
                    <JQTools:JQGridColumn Alignment="left" Caption="傳真" Editor="text" FieldName="CONTACT_FAX" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="任職部門" Editor="text" FieldName="CONTACT_DEPT" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="區域" Editor="text" FieldName="CONTACT_AREA" Format="" MaxLength="0" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職業屬性" Editor="text" FieldName="CONTACT_TYPE" Format="" MaxLength="0" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="相片" Editor="text" FieldName="CONTACT_PHOTO" Format="Image,Folder:JB_ADMIN/Images,Height:35" MaxLength="0" Visible="true" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CONTACT_SKILL_NAME" Editor="text" FieldName="CONTACT_SKILL_NAME" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CONTACT_HOBBY_NAME" Editor="text" FieldName="CONTACT_HOBBY_NAME" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="所屬業務" Editor="text" FieldName="CONTACT_SALES" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="公開" Editor="checkbox" FieldName="CONTACT_ISPUBLIC" Format="" MaxLength="0" Visible="True" Width="30" EditorOptions="on:1,off:0" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="ADDRESS" Format="" MaxLength="0" Visible="True" Width="240" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CREATE_MAN" Editor="text" FieldName="CREATE_MAN" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd" MaxLength="0" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="UPDATE_MAN" Editor="text" FieldName="UPDATE_MAN" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="UPDATE_DATE" Editor="datebox" FieldName="UPDATE_DATE" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LABEL_ID" Editor="text" FieldName="LABEL_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="LABELVALUE" Editor="text" FieldName="LABELVALUE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CENTER_ID" Editor="text" FieldName="CENTER_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="DeleContact" Text="刪除" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGridCustom" Text="匯出EXCEL"/>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="openImportExcel" Text="匯入EXCEL" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"   OnClick="AddToGroup" Text="加入群組"/>
                    <JQTools:JQToolItem Icon="icon-cut" ItemType="easyui-linkbutton"   OnClick="RemoveFromGroup" Text="移出群組"/>
                    <JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton"   OnClick="AddLabel" Text="新增標簽" />
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"   OnClick="AddToActivity" Text="加入活動"/>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="群組" Condition="=" DataType="string" Editor="infocombobox" FieldName="CENTER_ID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="180" EditorOptions="valueField:'CENTER_ID',textField:'CENTER_CNAME',remoteName:'sCON_ContactManagement.GROUPTYPE',tableName:'GROUPTYPE',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" DefaultMethod="GetCenterID" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="職稱" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CONTACT_JOB',textField:'CONTACT_JOB',remoteName:'sCON_ContactManagement.CONTACT_JOB',tableName:'CONTACT_JOB',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_JOB" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="180" DefaultMethod="GetCenterJOB" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="區域" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CONTACT_AREA',textField:'CONTACT_AREA',remoteName:'sCON_ContactManagement.CONTACT_AREA',tableName:'CONTACT_AREA',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_AREA" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="行業別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'JB_TYPE',textField:'JB_NAME',remoteName:'sCON_ContactManagement.CONTACT_TRADE',tableName:'CONTACT_TRADE',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_TRADE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="部門" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CONTACT_DEPT',textField:'CONTACT_DEPT',remoteName:'sCON_ContactManagement.CONTACT_DEPT',tableName:'CONTACT_DEPT',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_DEPT" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="姓名" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_NAME" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="415" DefaultMethod="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="任職公司" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_COMPANY" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="415" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="專長" Condition="=" DataType="string" Editor="text" FieldName="CONTACT_SKILL_NAME" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="415" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="興趣" Condition="=" DataType="string" Editor="text" FieldName="CONTACT_HOBBY_NAME" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="415" />
                    <JQTools:JQQueryColumn AndOr="=" Caption="電子報1" Condition="=" DataType="string" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="CONTACT_ISEDM" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="電子報2" Condition="=" DataType="string" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="CONTACT_ISEDM1" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="標簽1" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CENTERLABELID',textField:'LABELNAME',remoteName:'sCON_ContactManagement.CFILTLABEL1',tableName:'CFILTLABEL1',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:LABELFILTER1OnSelect,panelHeight:200" FieldName="LABELFILTER1" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="標簽值1" Condition="=" DataType="string" Editor="infocombobox" FieldName="LABELVALUE1" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="180" EditorOptions="valueField:'LABELVALUE',textField:'LABELVALUE',remoteName:'sCON_ContactManagement.CONTACTLABEL',tableName:'CONTACTLABEL',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="標簽2" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CENTERLABELID',textField:'LABELNAME',remoteName:'sCON_ContactManagement.CFILTLABEL2',tableName:'CFILTLABEL2',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:LABELFILTER2OnSelect,panelHeight:200" FieldName="LABELFILTER2" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="標簽值2" Condition="=" DataType="string" Editor="infocombobox" FieldName="LABELVALUE2" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="180" EditorOptions="valueField:'LABELVALUE',textField:'LABELVALUE',remoteName:'sCON_ContactManagement.CONTACTLABEL',tableName:'CONTACTLABEL',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="標簽3" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CENTERLABELID',textField:'LABELNAME',remoteName:'sCON_ContactManagement.CFILTLABEL3',tableName:'CFILTLABEL3',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:LABELFILTER3OnSelect,panelHeight:200" FieldName="LABELFILTER3" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="標簽值3" Condition="=" DataType="string" Editor="infocombobox" FieldName="LABELVALUE3" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="180" EditorOptions="valueField:'LABELVALUE',textField:'LABELVALUE',remoteName:'sCON_ContactManagement.CONTACTLABEL',tableName:'CONTACTLABEL',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="標簽4" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CENTERLABELID',textField:'LABELNAME',remoteName:'sCON_ContactManagement.CFILTLABEL4',tableName:'CFILTLABEL4',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:LABELFILTER4OnSelect,panelHeight:200" FieldName="LABELFILTER4" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="標簽值4" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'LABELVALUE',textField:'LABELVALUE',remoteName:'sCON_ContactManagement.CONTACTLABEL',tableName:'CONTACTLABEL',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LABELVALUE4" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="標簽5" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CENTERLABELID',textField:'LABELNAME',remoteName:'sCON_ContactManagement.CFILTLABEL5',tableName:'CFILTLABEL5',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:LABELFILTER5OnSelect,panelHeight:200" FieldName="LABELFILTER5" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="標簽值5" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'LABELVALUE',textField:'LABELVALUE',remoteName:'sCON_ContactManagement.CONTACTLABEL',tableName:'CONTACTLABEL',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LABELVALUE5" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="所屬業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'USERNAME',textField:'USERNAME',remoteName:'sCON_ContactManagement.CONTACT_TSALES',tableName:'CONTACT_TSALES',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_SALES" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="180" DefaultMethod="GetDefaultSales" DefaultValue="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="不完整紀錄" Condition="=" DataType="string" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="ISNOTOK" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="135" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="未設標簽" Condition="=" DataType="string" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="ISNOTLABEL" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="已參加活動" Condition="=" DataType="string" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="ISACTIVITY" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="135" DefaultValue="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="未參加活動" Condition="=" DataType="string" DefaultValue="" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="ISNOTACTIVITY" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="=" DataType="string" Editor="datebox" FieldName="SDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止日期" Condition="=" DataType="string" DefaultValue="" Editor="datebox" FieldName="EDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
            </div>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="聯絡人維護" Width="840px" DialogLeft="30px" DialogTop="10px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CON_CONTACTUPDATE" HorizontalColumnsCount="1" RemoteName="sCON_ContactManagement.CON_CONTACTUPDATE" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnLoadSuccess="dataFormMasterOnLoadSucess" OnApplied="dataFormMasterOnApplied" OnApply="dataFormMasterOnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="CONTACT_ID" Editor="numberbox" FieldName="CONTACT_ID" Format="" Width="180" Visible="False" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="人脈群組" Editor="infocombobox" FieldName="CENTER_ID" Format="" Width="170" EditorOptions="valueField:'CENTER_ID',textField:'CENTER_CNAME',remoteName:'sCON_ContactManagement.CENTER',tableName:'CENTER',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="text" FieldName="CONTACT_NAME" Format="" Width="166" maxlength="0" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CONTACT_ENAME" Format="" Width="167" maxlength="0" Span="1" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:300,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'男',value:'1'},{text:'女',value:'0'}]" FieldName="CONTACT_GENDER" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行業別" Editor="infocombobox" EditorOptions="valueField:'JB_TYPE',textField:'JB_NAME',remoteName:'sCON_ContactManagement.CONTACT_TRADE',tableName:'CONTACT_TRADE',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_TRADE" MaxLength="0" ReadOnly="False" Visible="True" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CONTACT_TRADENOTES" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="任職公司" Editor="infocombobox" FieldName="CONTACT_COMPANY" Width="240" EditorOptions="valueField:'CONTACT_COMPANY',textField:'CONTACT_COMPANY',remoteName:'sCON_ContactManagement.COMPANY',tableName:'COMPANY',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" maxlength="0" Span="1" ReadOnly="False" Visible="True" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="任職部門" Editor="infocombobox" FieldName="CONTACT_DEPT" maxlength="0" Width="240" Visible="True" EditorOptions="valueField:'CONTACT_DEPT',textField:'CONTACT_DEPT',remoteName:'sCON_ContactManagement.CONTACT_DEPT',tableName:'CONTACT_DEPT',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="1" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="infocombobox" EditorOptions="valueField:'CONTACT_JOB',textField:'CONTACT_JOB',remoteName:'sCON_ContactManagement.CONTACT_JOB',tableName:'CONTACT_JOB',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_JOB" Format="" maxlength="0" ReadOnly="False" Visible="True" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="CONTACT_CELLPHONE" Format="" maxlength="0" OnBlur="CheckContactIsExist" ReadOnly="False" Visible="True" Width="240" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話1" Editor="infocombobox" FieldName="CONTACT_TEL_TYPE" maxlength="0" Width="100" EditorOptions="valueField:'CONTACT_TEL_TYPE',textField:'CONTACT_TEL_TYPE',remoteName:'sCON_ContactManagement.TEL_TYPE',tableName:'TEL_TYPE',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CONTACT_TEL" Format="" Span="1" Width="100" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CONTACT_TEL_EXT" Span="1" Width="45" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話2" Editor="infocombobox" EditorOptions="valueField:'CONTACT_TEL1_TYPE',textField:'CONTACT_TEL1_TYPE',remoteName:'sCON_ContactManagement.TEL1_TYPE',tableName:'TEL1_TYPE',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_TEL1_TYPE" Span="1" Width="100" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CONTACT_TEL1" ReadOnly="False" Span="1" Width="100" MaxLength="0" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CONTACT_TEL1_EXT" Span="1" Width="45" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳真" Editor="text" FieldName="CONTACT_FAX" MaxLength="0" Span="1" Width="98" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="地址一" Editor="infocombobox" EditorOptions="items:[{value:'私人',text:'私人',selected:'false'},{value:'公司',text:'公司',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_ADDR_TYPE" maxlength="0" ReadOnly="False" Span="1" Visible="True" Width="100" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'CONTACT_AREA',textField:'CONTACT_AREA',remoteName:'sCON_ContactManagement.CONTACT_AREA',tableName:'CONTACT_AREA',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_AREA" Format="" maxlength="0" Span="1" Visible="True" Width="90" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="CONTACT_ADDR" Format="" maxlength="0" Width="280" Span="1" EditorOptions="valueField:'CONTACT_ADDR',textField:'CONTACT_ADDR',remoteName:'sCON_ContactManagement.CONTACT_ADDR',tableName:'CONTACT_ADDR',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="地址二" Editor="infocombobox" EditorOptions="items:[{value:'公司',text:'公司',selected:'false'},{value:'私人',text:'私人',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_ADDR1_TYPE" maxlength="0" Span="1" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CONTACT_ADDR1" Format="" maxlength="0" Width="280" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="郵件信箱一" Editor="infocombobox" EditorOptions="valueField:'CONTACT_EMAIL_TYPE',textField:'CONTACT_EMAIL_TYPE',remoteName:'sCON_ContactManagement.EMAIL1_TYPE',tableName:'EMAIL1_TYPE',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_EMAIL_TYPE" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CONTACT_EMAIL" Format="" maxlength="0" Width="280" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" EditorOptions="on:1,off:0" FieldName="CONTACT_ISEDM" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="郵件信箱二" Editor="infocombobox" FieldName="CONTACT_EMAIL1_TYPE" maxlength="0" Width="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" EditorOptions="valueField:'CONTACT_EMAIL_TYPE',textField:'CONTACT_EMAIL_TYPE',remoteName:'sCON_ContactManagement.EMAIL2_TYPE',tableName:'EMAIL2_TYPE',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CONTACT_EMAIL1" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="280" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" EditorOptions="on:1,off:0" FieldName="CONTACT_ISEDM1" maxlength="0" Span="2" Width="30" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="專長" Editor="text" FieldName="CONTACT_SKILL_NAME" Format="" maxlength="0" Width="350" Span="3" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="興趣" Editor="text" FieldName="CONTACT_HOBBY_NAME" Format="" maxlength="0" Width="350" Span="3" Visible="True" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職業屬性" Editor="infocombobox" FieldName="CONTACT_TYPE" Format="" maxlength="0" Width="170" Span="2" EditorOptions="valueField:'CONTACT_TYPE',textField:'CONTACT_TYPE',remoteName:'sCON_ContactManagement.CONTACT_TYPE',tableName:'CONTACT_TYPE',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="center" Caption="相片" Editor="infofileupload" FieldName="CONTACT_PHOTO" Format="" maxlength="50" Width="120" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'jb_admin/images',showButton:true,showLocalFile:false,fileSizeLimited:'500'" Span="3" Visible="True" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" EditorOptions="height:45" FieldName="CONTACT_MEMO" MaxLength="0" ReadOnly="False" Span="1" Visible="True" Width="480" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公開" Editor="checkbox" FieldName="CONTACT_ISPUBLIC" Format="" Width="60" EditorOptions="on:1,off:0" Span="3" Visible="True" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CREATE_MAN" Editor="text" FieldName="CREATE_MAN" Format="" Width="180" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CREATE_DATE" Editor="datebox" FieldName="CREATE_DATE" Format="" Width="180" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UPDATE_MAN" Editor="text" FieldName="UPDATE_MAN" Format="" Width="180" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UPDATE_DATE" Editor="datebox" FieldName="UPDATE_DATE" Format="" Width="180" Visible="False" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CENTER_NAME" Editor="text" FieldName="CENTER_NAME" Format="" maxlength="0" Width="180" Visible="False" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CONTACT_SKILL_ID" Editor="text" FieldName="CONTACT_SKILL_ID" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CONTACT_HOBBY_ID" Editor="text" FieldName="CONTACT_HOBBY_ID" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CONTACT_ISACTIVE" Editor="text" FieldName="CONTACT_ISACTIVE" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="所屬業務" Editor="infocombobox" EditorOptions="valueField:'USERNAME',textField:'USERNAME',remoteName:'sCON_ContactManagement.CONTACT_SALES',tableName:'CONTACT_SALES',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_SALES" MaxLength="0" ReadOnly="False" Span="1" Width="170" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CONTACT_LABELQTY" Editor="text" FieldName="CONTACT_LABELQTY" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="CONTACT_ID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GETCENTER1ID" FieldName="CENTER_ID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="公司" FieldName="CONTACT_TEL_TYPE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="公司" FieldName="CONTACT_ADDR_TYPE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="公司" FieldName="CONTACT_ADDR1_TYPE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="true" FieldName="CONTACT_ISACTIVE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="CONTACT_GENDER" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="CONTACT_ISPUBLIC" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="CONTACT_JOB" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="CONTACT_AREA" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="CONTACT_ADDR" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="CONTACT_LABELQTY" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_NAME" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_ADDR" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_AREA" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_JOB" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="CON_CENTERLABEL" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sCON_ContactManagement.CON_CENTERLABEL" RowNumbers="True" Title="標簽列表" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" OnDeleted="JQDataGrid1OnDeleted">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="群組" Editor="text" FieldName="CENTER_CNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="標籤欄名" Editor="text" FieldName="LABELNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="標籤內容" Editor="text" FieldName="LABELVALUE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="280">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="AUTOKEY" Editor="text" FieldName="AUTOKEY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CENTER_ID" Editor="text" FieldName="CENTER_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CONTACT_ID" Editor="text" FieldName="CONTACT_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LABEL_ID" Editor="text" FieldName="LABEL_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                        </JQTools:JQGridColumn>
                    </Columns>
                 </JQTools:JQDataGrid>
             </JQTools:JQDialog>
                  <JQTools:JQDialog ID="JQDialog2"  BindingObjectID="JQDataForm1" runat="server" DialogLeft="10px" DialogTop="65px" Title="新增標簽" Width="520px" Closed="False" ShowSubmitDiv="True">
                  <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="CON_CONTACT" HorizontalColumnsCount="2" RemoteName="sCON_ContactManagement.CON_CONTACT" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnLoadSuccess="JQDataForm1OnLoadSucess" OnApply="JQDataForm1OnApply" ParentObjectID="dataGridView">
                        <Columns>
                             <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="LABEL_ID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" EditorOptions="valueField:'LABEL_ID',textField:'LABELNAME',remoteName:'sCON_ContactManagement.CENTERLABEL',tableName:'CENTERLABEL',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                             <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="LABELVALUE" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" EditorOptions="valueField:'LABELVALUE',textField:'LABELVALUE',remoteName:'sCON_ContactManagement.CENTERLABEL1',tableName:'CENTERLABEL1',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                        <Columns>
                        </Columns>
                    </JQTools:JQDefault>
            </JQTools:JQDialog>
            <div id="Dialog_Import">
            <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog" DialogLeft="400px" DialogTop="200px">
                <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" RemoteName=" " DataMember=" " HorizontalColumnsCount="1" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApply="">
                    <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="檔案名稱" Editor="text" FieldName="FilePathName" Width="80" ReadOnly="true" Visible="false" />
                        <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="infocombobox" FieldName="CONTACT_NAME" Width="180" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="英文姓名" Editor="infocombobox" FieldName="CONTACT_ENAME" Width="180" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="infocombobox" FieldName="CONTACT_JOB" Width="180" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="infocombobox" FieldName="CONTACT_TEL" Width="180" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="infocombobox" FieldName="CONTACT_CELLPHONE" Width="180" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="通訊地址" Editor="infocombobox" FieldName="CONTACT_ADDR" Width="180" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="郵件信箱" Editor="infocombobox" FieldName="CONTACT_EMAIL" Width="180" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_NAME" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_CELLPHONE" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_EMAIL" />
                     </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
        <div id="Dialog_Skill">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="DG_SKILL" runat="server" AutoApply="False" DataMember="CONTACT_SKILL" Pagination="False" ParentObjectID="" RemoteName="sCON_ContactManagement.CONTACT_SKILL" Title="專長選項" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,30,40,50" PageSize="50" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="360px" CheckOnSelect="True" RecordLock="False" ColumnsHibeable="False" RecordLockMode="None" OnLoadSuccess="">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="CODE_ID" Editor="text" FieldName="CODE_ID" Width="90" Visible="False" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="專長名稱" Editor="text" FieldName="NAME" Width="150" ReadOnly="True" Frozen="False" MaxLength="4" QueryCondition="" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="已選擇" Editor="checkbox" FieldName="IS_SELECTED" Width="40" Frozen="False" MaxLength="60" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" EditorOptions="on:'Y',off:'N'" FormatScript="genCheckBox" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>
         <div id="Dialog_Hobby">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="DG_HOBBY" runat="server" AutoApply="False" DataMember="CONTACT_HOBBY" Pagination="False" ParentObjectID="" RemoteName="sCON_ContactManagement.CONTACT_HOBBY" Title="興趣選項" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,30,40,50" PageSize="50" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="360px" CheckOnSelect="True" RecordLock="False" ColumnsHibeable="False" RecordLockMode="None" OnLoadSuccess="">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="CODE_ID" Editor="text" FieldName="CODE_ID" Width="90" Visible="False" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="興趣名稱" Editor="text" FieldName="NAME" Width="150" ReadOnly="True" Frozen="False" MaxLength="4" QueryCondition="" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="已選擇" Editor="checkbox" FieldName="IS_SELECTED" Width="40" Frozen="False" MaxLength="60" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" EditorOptions="on:'Y',off:'N'" FormatScript="genCheckBox" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>
       <JQTools:JQDialog ID="JQDialog3" runat="server" DialogLeft="15px" DialogTop="60px" Title="聯絡人重複列表" Width="820px" OnSubmited="JQDialog3OnSubmited" Closed="True" ShowSubmitDiv="False">
                 <JQTools:JQDataGrid ID="JQDataGridContactDul" runat="server" AlwaysClose="True" DataMember="CON_CONTACTTEMP" RemoteName="sCON_ContactManagement.CON_CONTACTTEMP" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Window" QueryTitle="設備查詢" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="聯絡人重複列表" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" OnUpdate="" Height="450px" Width="760px" OnLoadSuccess="JQDataGridContactDulOnLoadSucess" BufferView="False" NotInitGrid="False" RowNumbers="True">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="CONTACT_ID" Editor="text" FieldName="CONTACT_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CENTER_ID" Editor="text" FieldName="CENTER_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="來源群組" Editor="text" FieldName="CENTER_NAME_SOU" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="聯絡人姓名" Editor="text" FieldName="CONTACT_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="CONTACT_TEL" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="CONTACT_CELLPHONE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="任職公司" Editor="text" FieldName="CONTACT_COMPANY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="160">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CREATE_DATE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" Format="yyyy/mm/dd HH:MM:SS">
                        </JQTools:JQGridColumn>
                     </Columns>
                     <TooItems>
                     <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                     <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                     <%--<JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton"   OnClick="ConcordContact" Text="整合聯絡人" />--%>
                </TooItems>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialog4" runat="server" DialogLeft="15px" DialogTop="15px" Title=" " Width="960px" OnSubmited="JQDialog4OnSubmited" Closed="True" ShowSubmitDiv="True">
                 <JQTools:JQDataGrid ID="JQDataGridActivityBills" runat="server" AlwaysClose="True" DataMember="ACTIVITYBILLS" RemoteName="sCON_ContactManagement.ACTIVITYBILLS" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="15px" RecordLock="False" RecordLockMode="None" Title=" " TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdate="" Height="400px" Width="890px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="ACTIVITY_ID" Editor="text" FieldName="ACTIVITY_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CONTACT_ID" Editor="text" FieldName="CONTACT_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="任職公司" Editor="text" FieldName="CONTACT_COMPANY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="參加人員" Editor="text" FieldName="CONTACT_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="性別" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'男',selected:'false'},{value:'0',text:'女',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_GENDER" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="CONTACT_DEPT" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="CONTACT_TEL" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="CONTACT_CELLPHONE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="電子郵件" Editor="text" FieldName="CONTACT_EMAIL" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="加入人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                     </Columns>
                     <TooItems>
                     <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                     <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                     <JQTools:JQToolItem Icon="icon-cut" ItemType="easyui-linkbutton"   OnClick="RemoveFromActivity" Text="移出活動1"/>
                </TooItems>
                     <QueryColumns>
                         <JQTools:JQQueryColumn AndOr="and" Caption="活動" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ACTIVITY_ID',textField:'ACTIVITY_NAME',remoteName:'sCON_ContactManagement.ACTIVITYMASTER',tableName:'ACTIVITYMASTER',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ACTIVITY_ID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="450" />
                         <JQTools:JQQueryColumn AndOr="and" Caption="加入人員" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CREATE_MAN',textField:'CREATE_MAN',remoteName:'sCON_ContactManagement.AddUser',tableName:'AddUser',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CREATE_MAN" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                     </QueryColumns>
                </JQTools:JQDataGrid>
           </JQTools:JQDialog>
           <JQTools:JQDialog ID="JQDialog5" runat="server" DialogLeft="175px" DialogTop="92px" Title=" " Width="640px" OnSubmited="JQDialog5OnSubmited" Closed="True" ShowSubmitDiv="False">
                 <JQTools:JQDataGrid ID="JQDataGridContactLabel" runat="server" AlwaysClose="True" DataMember="CONTACTLABELQUERY" RemoteName="sCON_ContactManagement.CONTACTLABELQUERY" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Window" QueryTitle="標簽查詢" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="聯絡人重複列表" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdate="" Height="450px" Width="580px" OnLoadSuccess="JQDataGridContactDulOnLoadSucess" BufferView="False" NotInitGrid="False" RowNumbers="True">
                     <Columns>
                         <JQTools:JQGridColumn Alignment="left" Caption="日期" Editor="text" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="標籤內容" Editor="text" FieldName="CONLABELNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="320">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                         </JQTools:JQGridColumn>
                     </Columns>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
            <div style="display:none;">
            <JQTools:JQDataGrid ID="JQDataGridVal" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="SYSVar" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sCON_ContactManagement.SYSVar" Title="JQDataGrid" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="ExportExcelUser" Editor="text" FieldName="ExportExcelUser" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                </Columns>
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="CategoryValue" Editor="text" FieldName="AssetAuditor" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                </Columns>
            </JQTools:JQDataGrid>
            </div>
                 <JQTools:JQDialog ID="JQDialog6"  BindingObjectID="JQDataForm1" runat="server" DialogLeft="10px" DialogTop="65px" Title="" Width="520px" Closed="False" ShowSubmitDiv="True">
                  <JQTools:JQDataForm ID="JQDataForm2" runat="server" DataMember="CON_CONTACT" HorizontalColumnsCount="2" RemoteName="sCON_ContactManagement.CON_CONTACT" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnLoadSuccess="JQDataForm1OnLoadSucess" OnApply="JQDataForm1OnApply" ParentObjectID="dataGridView">
                        <Columns>
                             <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="LABEL_ID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" EditorOptions="valueField:'LABEL_ID',textField:'LABELNAME',remoteName:'sCON_ContactManagement.CENTERLABEL',tableName:'CENTERLABEL',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                             <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="LABELVALUE" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" EditorOptions="valueField:'LABELVALUE',textField:'LABELVALUE',remoteName:'sCON_ContactManagement.CENTERLABEL1',tableName:'CENTERLABEL1',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="JQDefault2" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                        <Columns>
                        </Columns>
                    </JQTools:JQDefault>
            </JQTools:JQDialog>
         <script type="text/javascript">
                 $(":input").css("background", backcolor);
         </script>
    
    </form>
</body>
</html>
