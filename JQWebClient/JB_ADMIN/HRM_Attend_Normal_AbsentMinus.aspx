<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_Attend_Normal_AbsentMinus.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>請假單</title>
    <script>
        $(document).ready(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "#FFFFB5");
            });

            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });

            $("#dataFormMasterBEGIN_DATE").closest('td').append('  (請輸入日期格式 : YYYY/MM/DD)').css("color", "blue");
            $("#dataFormMasterEND_DATE").closest('td').append('  (請輸入日期格式 : YYYY/MM/DD)').css("color", "blue");

            $("#dataFormMasterEMPLOYEE_CODE").data("inforefval").refval.find("input.refval-text").blur(function () {
                timeSet();
            });

            $("#dataFormMasterHOLIDAY_CODE").data("inforefval").refval.find("input.refval-text").blur(function () {
                checkAbsentHours();
            });

            $("#dataFormMasterBEGIN_DATE").combo('textbox').blur(function () {
                var employeeCode = $('#dataFormMasterEMPLOYEE_ID').val();
                if (employeeCode == "") {
                    alert("請先點選員工工號");
                    $("#dataFormMasterEMPLOYEE_CODE").data("inforefval").refval.find("input.refval-text").focus();
                }
                else {
                    timeSet();
                    checkAbsentHours();
                }
            });

            $("#dataFormMasterBEGIN_DATE").datebox({
                onSelect: function (date) {
                    var employeeCode = $('#dataFormMasterEMPLOYEE_ID').val();
                    if (employeeCode == "") {
                        alert("請先點選員工工號");
                        $("#dataFormMasterEMPLOYEE_CODE").data("inforefval").refval.find("input.refval-text").focus();
                    }
                    else {
                        timeSet();
                        checkAbsentHours();
                    }
                }
            });

            $("#dataFormMasterEND_DATE").combo('textbox').blur(function () {
                var employeeCode = $('#dataFormMasterEMPLOYEE_ID').val();
                if (employeeCode == "") {
                    alert("請先點選員工工號");
                    $("#dataFormMasterEMPLOYEE_CODE").data("inforefval").refval.find("input.refval-text").focus();
                }
                else {
                    timeSet();
                    checkAbsentHours();
                }

            });

            $("#dataFormMasterEND_DATE").datebox({
                onSelect: function (date) {
                    var employeeCode = $('#dataFormMasterEMPLOYEE_ID').val();
                    if (employeeCode == "") {
                        alert("請先點選員工工號");
                        $("#dataFormMasterEMPLOYEE_CODE").data("inforefval").refval.find("input.refval-text").focus();
                    }
                    else {
                        timeSet();
                        checkAbsentHours();
                    }
                }
            });

            //-------------------------------欄位配對視窗送出按鈕------------------------------
            $('#DialogSubmit', '#Dialog_ImportMain').removeAttr('onclick').on('click', function () {
                if (!$('#DataForm_ImportMain').form('validateForm')) return;    //驗證                    
                var data = $('#DataForm_ImportMain').jbDataFormGetAFormData();  //取資料
                $.messager.progress({ msg: 'Loading...' });                 //進度條開始
                //送出上傳動作
                $.ajaxSetup({ async: false });
                $.post('../handler/JqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_AbsentMinus', {
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
            $('#Dialog_Import').jbImportExcel({
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
                    width: 550,
                    resizable: false,
                    modal: true,
                    title: "請假異動資料紀錄",
                    closed: true,
                    buttons: [{
                        text: '結束',
                        handler: function () { $("#Dialog_TransLog").dialog("close") }
                    }]
                });
        })  //document ready

        //異動資料欄位超連結
        function HyperlinkLog(value, row, index) {
            return "<a href='javascript: void(0)' onclick='LinkLog(" + index + ");'>" + value + "</a>";
        }

        function LinkLog(index) {
            //alert(index)
            $("#dataGridMaster").datagrid('selectRow', index);
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var absentMinusID = rows.ABSENT_MINUS_ID

            var sql = "HRM_ATTEND_ABSENT_MINUS_LOG.ABSENT_MINUS_ID = " + absentMinusID;

            $("#Dialog_TransLog").dialog("open");
            $("#DG_HRM_ATTEND_ABSENT_MINUS_LOG").datagrid('setWhere', sql);
        }

        //刪除前判斷是否已有出勤鎖檔紀錄
        function checkLockData(rowData) {
            if (rowData != null && rowData != undefined) {
                var employeeID = rowData.EMPLOYEE_ID;
                var beginDate = rowData.BEGIN_DATE.replace(/\-/g, '/').substr(0, 10);
                var endDate = rowData.END_DATE.replace(/\-/g, '/').substr(0, 10);
                var restHours = rowData.REST_HOURS;

                var dateLockCnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Share.HRM_ATTEND_DATA_LOCK', //連接的Server端，command
                    data: "mode=method&method=" + "checkAttendDataLock" + "&parameters=" + employeeID + "," + beginDate + "," + endDate, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            dateLockCnt = $.parseJSON(data);
                        }
                    },
                });
                if ((dateLockCnt != "0" && dateLockCnt != "undefined")) {
                    alert("此筆請假日期已有鎖檔紀錄，無法執行修改或刪除");
                    return false;
                }
                else
                    return true;
            }
        }

        //存檔前
        //1. 檢查combox 必要欄位
        //2. 判斷請假起始日期不可大於截止日期
        //3. 判斷請假起始時間不可大於截止時間
        //3.1 判斷生理假起始日期須等於截止日期
        //4. 判斷請假日期是否已有鎖檔紀錄
        //5. 判斷請假時數
        //6. 判斷請假資料(EMPLOYEE_ID/ABSENT_DATE_TIME_BEGIN/ABSENT_DATE_TIME_END)申請的時段內是否已有存在的請假資料
        function checkAbsentData() {
            var employeeID = $('#dataFormMasterEMPLOYEE_ID').val();
            var employeeCode = $("#dataFormMasterEMPLOYEE_CODE").refval('getValue');
            var beginDate = $('#dataFormMasterBEGIN_DATE').datebox('getValue');
            var endDate = $('#dataFormMasterEND_DATE').datebox('getValue');
            var beginDateValidate = $.fn.datebox('parseDate', beginDate.replace(/\//g, '-'));
            var endDateValidate = $.fn.datebox('parseDate', endDate.replace(/\//g, '-'));
            var beginTime = $('#dataFormMasterBEGIN_TIME').val();
            var endTime = $('#dataFormMasterEND_TIME').val();
            var holidayID = $('#dataFormMasterHOLIDAY_ID').val();
            var absentHolidayCode = $("#dataFormMasterHOLIDAY_CODE").refval('getValue');
            var absentDeptCode = $("#dataFormMasterDEPT_CODE").refval('getValue');
            var totalHours = $('#dataFormMasterTOTAL_HOURS').numberbox('getValue');
            var absentMinusID;
            var cnt = 0;

            //員工工號
            if (employeeCode == "" || employeeCode == undefined) {
                alert("請選擇員工工號");
                $("#dataFormMasterEMPLOYEE_CODE").data("inforefval").refval.find("input.refval-text").focus();
                return false;
            }

            //假別代碼
            if (absentHolidayCode == "" || absentHolidayCode == undefined) {
                alert("請選擇假別代碼");
                $("#dataFormMasterHOLIDAY_CODE").data("inforefval").refval.find("input.refval-text").focus();
                return false;
            }

            //2. 判斷請假起始日期不可大於截止日期
            if (beginDateValidate == "Invalid Date" || !$.jbIsDateStr(beginDate)) {
                alert('請假起始日期:' + beginDate + '格式錯誤');
                $("#dataFormMasterBEGIN_DATE").datebox('textbox').focus();
                return false;
            }

            if (endDateValidate == "Invalid Date" || !$.jbIsDateStr(endDate)) {
                alert('請假截止日期:' + endDate + '格式錯誤');
                $("#dataFormMasterEND_DATE").datebox('textbox').focus();
                return false;
            }

            if (beginDate > endDate) {
                alert('請假起始日期 : ' + beginDate + ' 需小於請假截止日期 : ' + endDate);
                $("#dataFormMasterBEGIN_DATE").datebox('textbox').focus();
                return false;
            }

            //3. 判斷請假起始時間不可大於截止時間
            if (parseInt(beginTime) >= parseInt(endTime)) {
                alert('請假起始時間 : ' + beginTime + ' 需小於請假截止時間 : ' + endTime);
                return false;
            }

            //3.1  判斷生理假起始日期須等於截止日期
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS', //連接的Server端，command
                data: "mode=method&method=" + "checkPhysiologyleavesID" + "&parameters=" + holidayID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        cnt = $.parseJSON(data);
                    }
                }
            });
            if ((cnt == "1")) {
                if (beginDate != endDate) {
                    alert("生理假請假起始時間需等於請假截止時間");
                    return false;
                }
            }

            //3.2 判斷假別性別限制
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS', //連接的Server端，command
                data: "mode=method&method=" + "checkHolidaySex" + "&parameters=" + holidayID + "," + employeeID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        cnt = $.parseJSON(data);
                    }
                }
            });
            if ((cnt == "0")) {
                alert("此假別有性別限制");
                return false;
            }


            //var overtimeDateBegin = $('#dataFormMasterOVERTIME_DATE_TIME_BEGIN').datebox('getValue');
            //var overtimeDateEnd = $('#dataFormMasterOVERTIME_DATE_TIME_END').datebox('getValue');

            if (getEditMode($("#dataFormMaster")) == 'updated')
                absentMinusID = $('#dataFormMasterABSENT_MINUS_ID').val();
            else
                absentMinusID = "0";

            if ($("#dataGridMaster").datagrid('getSelected')) {
                var o_employeeID = $("#dataGridMaster").datagrid('getSelected').EMPLOYEE_ID;
                var o_beginDate = $("#dataGridMaster").datagrid('getSelected').BEGIN_DATE.replace(/\-/g, '/').substr(0, 10);
                var o_endDate = $("#dataGridMaster").datagrid('getSelected').END_DATE.replace(/\-/g, '/').substr(0, 10);
                var o_beginTime = $("#dataGridMaster").datagrid('getSelected').BEGIN_TIME;
                var o_endTime = $("#dataGridMaster").datagrid('getSelected').END_TIME;
                var o_totalHours = $("#dataGridMaster").datagrid('getSelected').TOTAL_HOURS;

                //var o_overtimeDateBegin = $("#dataGridMaster").datagrid('getSelected').OVERTIME_DATE_TIME_BEGIN.replace(/\-/g, '/').substr(0, 10);
                //var o_overtimeDateEnd = $("#dataGridMaster").datagrid('getSelected').OVERTIME_DATE_TIME_END.replace(/\-/g, '/').substr(0, 10);
            }
            else
                var o_totalHours = 0;

            //5. 判斷請假時數
            var rows;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS', //連接的Server端，command
                data: "mode=method&method=" + "checkAbsentHours" + "&parameters=" + absentMinusID + "," + employeeID + "," + beginDate + "," + endDate + "," + beginTime + "," + endTime + "," + holidayID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false)
                        rows = $.parseJSON(data);
                }
            });

            if (rows.length > 0) {
                if (rows[0].totalHours == 0) {
                    alert("此請假區間尚未產生考勤資料");
                    return false;
                }
                if (rows[0].totalHours != parseFloat($('#dataFormMasterTOTAL_HOURS').numberbox('getValue'))) {
                    alert("請假時數不正確(請假時數 : " + rows[0].hours + "小時)");
                    $('#dataFormMasterTOTAL_HOURS').numberbox('setValue', rows[0].hours);
                    return false;
                }
            }


            //6. 判斷請假剩餘時數
            var rows;
            //if (getEditMode($("#dataFormMaster")) == 'updated')
            //    absentMinusID = $('#dataFormMasterABSENT_MINUS_ID').val();
            //else
            //    absentMinusID = "0";

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS', //連接的Server端，command
                data: "mode=method&method=" + "checkAbsentRestHours" + "&parameters=" + absentMinusID + "," + employeeID + "," + beginDate + "," + endDate + "," + beginTime + "," + endTime + "," + holidayID + "," + totalHours,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false)
                        rows = $.parseJSON(data);
                }
            });

            if (rows.length > 0) {
                if (rows[0].CHECK_REST_HOUR == 'Y') {
                    if (absentMinusID == "0" && rows[0].REST_HOURS < parseFloat($('#dataFormMasterTOTAL_HOURS').numberbox('getValue'))) {
                        alert("剩餘時數不足(剩餘時數 : " + rows[0].REST_HOURS + "小時)");
                        return false;
                    }
                    if (absentMinusID != "0" && o_employeeID == employeeID && rows[0].REST_HOURS - totalHours + o_totalHours < 0) {
                        alert("剩餘時數不足(剩餘時數 : " + rows[0].REST_HOURS + "小時)");
                        return false;
                    }
                    if (absentMinusID != "0" && o_employeeID != employeeID && rows[0].REST_HOURS - totalHours < 0) {
                        alert("剩餘時數不足(剩餘時數 : " + rows[0].REST_HOURS + "小時)");
                        return false;
                    }
                    if (rows[0].PHYSIOLOGY_CNT != "0") {
                        alert("本月已請生理假(生理假次數 : " + rows[0].PHYSIOLOGY_CNT + "次)");
                        return false;
                    }
                    if (absentMinusID == "0" && rows[0].FAMILY_HOURS != "0.00" && parseFloat(rows[0].FAMILY_MAX_HOURS) - parseFloat(rows[0].FAMILY_HOURS) < parseFloat($('#dataFormMasterTOTAL_HOURS').numberbox('getValue'))) {
                        alert("家庭照顧假剩餘時數不足(剩餘時數 : " + (parseFloat(rows[0].FAMILY_MAX_HOURS) - parseFloat(rows[0].FAMILY_HOURS)).toString() + "小時)");
                        return false;
                    }
                    if (absentMinusID != "0" && o_employeeID == employeeID && parseFloat(rows[0].FAMILY_MAX_HOURS) - parseFloat(rows[0].FAMILY_HOURS) - totalHours + o_totalHours < 0) {
                        alert("家庭照顧假剩餘時數不足(剩餘時數 : " + (parseFloat(rows[0].FAMILY_MAX_HOURS) - parseFloat(rows[0].FAMILY_HOURS)).toString() + "小時)");
                        return false;
                    }
                    if (absentMinusID != "0" && o_employeeID != employeeID && parseFloat(rows[0].FAMILY_MAX_HOURS) - parseFloat(rows[0].FAMILY_HOURS) - totalHours < 0) {
                        alert("家庭照顧假剩餘時數不足(剩餘時數 : " + (parseFloat(rows[0].FAMILY_MAX_HOURS) - parseFloat(rows[0].FAMILY_HOURS)).toString() + "小時)");
                        return false;
                    }
                }

                //和大補休以4小時為單位, 效期當日起算次月月底休完, 剩餘時數(4的餘數)提供遞延1個月BY 部門補休剩餘時數遞延功能
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS', //連接的Server端，command
                    data: "mode=method&method=" + "checkOvertimeRestHours" + "&parameters=" + employeeID + "," + holidayID + "," + rows[0].REST_HOURS + "," + endDate,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    },
                });
                if ((cnt != "0" && cnt != "undefined")) {
                    if ((rows[0].REST_HOURS - totalHours + o_totalHours) > 0 && totalHours % 4 > 0) {
                        alert("補休需以4小時為請假單位");
                        $("#dataFormMasterBEGIN_TIME").focus();
                        return false;
                    }
                }
            }


            //4. 修改判斷請假日期是否已有鎖檔紀錄
            if (getEditMode($("#dataFormMaster")) == 'updated') {
                var dateLockCnt = 0;
                absentMinusID = $('#dataFormMasterABSENT_MINUS_ID').val();

                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Share.HRM_ATTEND_DATA_LOCK', //連接的Server端，command
                    data: "mode=method&method=" + "checkAttendDataLock" + "&parameters=" + employeeID + "," + beginDate + "," + endDate, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            dateLockCnt = $.parseJSON(data);
                        }
                    }
                });
                if ((dateLockCnt != "0" && dateLockCnt != "undefined")) {
                    alert("此區間請假日期已有鎖檔紀錄");
                    return false;
                }
                    //6. 判斷請假資料申請的時段內是否已有存在的請假資料        
                else {
                    if (o_employeeID != employeeID || o_beginDate != beginDate || o_endDate != endDate || o_beginTime != beginTime || o_endTime != endTime) {
                        var cnt;
                        $.ajax({
                            type: "POST",
                            url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS', //連接的Server端，command
                            data: "mode=method&method=" + "checkAbsentData" + "&parameters=" + absentMinusID + "," + employeeID + "," + beginDate + "," + endDate + "," + beginTime + "," + endTime + "," + holidayID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                            cache: false,
                            async: false,
                            success: function (data) {
                                if (data != false) {
                                    cnt = $.parseJSON(data);
                                }
                            }
                        });
                        if ((cnt != "0" && cnt != "undefined")) {
                            alert("申請的時段內已有存在的請假或公出資料");
                            return false;
                        }
                        else
                            return true;
                    }
                    else
                        return true;
                }
            }
            else {
                if (getEditMode($("#dataFormMaster")) == 'inserted') {
                    var cnt;
                    absentMinusID = "0";

                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS', //連接的Server端，command
                        data: "mode=method&method=" + "checkAbsentData" + "&parameters=" + absentMinusID + "," + employeeID + "," + beginDate + "," + endDate + "," + beginTime + "," + endTime + "," + holidayID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            if (data != false) {
                                cnt = $.parseJSON(data);
                            }
                        }
                    });
                    if ((cnt != "0" && cnt != "undefined")) {
                        alert("申請的時段內已有存在的請假資料");
                        return false;
                    }
                    else
                        return true;
                }
                else
                    return true;
            }
        }



        function genCheckBox(val) {
            if (val == 'Y')
                return "<input  type='checkbox' checked='true' />";
            else
                return "<input  type='checkbox' />";
        }

        //check 時間格式如 : 0800 或 0830
        function checkTimeFormat(val) {
            return $.jbIsTimeFormat(val);
        }

        //check 計薪年月格式如 : 201401 或 201412
        function checkYearMonthFormat(val) {
            return $.jbIsYearMonthStr(val)
        }

        //計算請假時數
        function checkAbsentHours() {
            var employeeID = $('#dataFormMasterEMPLOYEE_ID').val();
            var beginDate = $('#dataFormMasterBEGIN_DATE').datebox('getValue');
            var endDate = $('#dataFormMasterEND_DATE').datebox('getValue');
            var beginDateValidate = $.fn.datebox('parseDate', beginDate.replace(/\//g, '-'));
            var endDateValidate = $.fn.datebox('parseDate', endDate.replace(/\//g, '-'));
            var beginTime = $('#dataFormMasterBEGIN_TIME').val();
            var endTime = $('#dataFormMasterEND_TIME').val();
            var holidayID = $('#dataFormMasterHOLIDAY_ID').val();

            var absentMinusID;

            if (getEditMode($("#dataFormMaster")) == 'updated')
                absentMinusID = $('#dataFormMasterABSENT_MINUS_ID').val();
            else
                absentMinusID = "0";

            //判斷起迄時間是否正確
            beginTimeValidate = $.jbIsTimeFormat(beginTime);
            endTimeValidate = $.jbIsTimeFormat(endTime);

            if (employeeID != "" && holidayID != "" && beginDate != "" && endDate != "" && beginTime != "" && endTime != "" && beginDateValidate != "Invalid Date" && endDateValidate != "Invalid Date" && $.jbIsDateStr(beginDate) && $.jbIsDateStr(endDate) && beginTimeValidate && endTimeValidate) {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS', //連接的Server端，command
                    data: "mode=method&method=" + "checkAbsentHours" + "&parameters=" + absentMinusID + "," + employeeID + "," + beginDate + "," + endDate + "," + beginTime + "," + endTime + "," + holidayID,
                    cache: false,
                    async: false,
                    success: function (data) {
                        var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                        if (rows.length > 0) {
                            $('#dataFormMasterTOTAL_HOURS').numberbox('setValue', rows[0].totalHours);
                        }
                    }
                });
            }
            else {
                $('#dataFormMasterTOTAL_HOURS').numberbox('setValue', "");
            }
        }


        function timeSet() {
            var beginDate = $('#dataFormMasterBEGIN_DATE').datebox('getValue');
            var endDate = $('#dataFormMasterEND_DATE').datebox('getValue');
            var employeeID = $('#dataFormMasterEMPLOYEE_ID').val();
            var beginDateValidate = $.fn.datebox('parseDate', beginDate.replace(/\//g, '-'));
            var endDateValidate = $.fn.datebox('parseDate', endDate.replace(/\//g, '-'));


            if (employeeID != "") {
                if (beginDateValidate != "Invalid Date" && endDateValidate != "Invalid Date" && $.jbIsDateStr(beginDate) && $.jbIsDateStr(endDate) && endDate >= beginDate) {
                    //計薪年月(SALARY_YYMM) && 請起時間(ON_TIME) && 請迄時間(OFF_TIME)
                    var dateLockCnt;
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS', //連接的Server端，command
                        data: "mode=method&method=" + "getSalaryYYMM" + "&parameters=" + employeeID + "," + beginDate, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            var rows = $.parseJSON(data);
                            if (rows.length > 0) {
                                $('#dataFormMasterSALARY_YYMM').val(rows[0].SALARY_YYMM);
                                if ($('#dataFormMasterBEGIN_TIME').val() == "")
                                    $('#dataFormMasterBEGIN_TIME').val(rows[0].ON_TIME);
                                if ($('#dataFormMasterEND_TIME').val() == "")
                                    $('#dataFormMasterEND_TIME').val(rows[0].OFF_TIME);
                            }
                        }
                    });
                }
            }
        }

        //function updateAttend(employeeID, beginDate, endDate) {
        //    var rowData = { "EmployeeID": employeeID, "DateFrom": beginDate, DateTo: endDate };
        //    $.ajax({
        //        url: '../handler/JqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_AttendCalculate',
        //        data: { mode: 'method', method: 'AttendCalculate_Single', parameters: $.toJSONString(rowData) },
        //        type: 'POST',
        //        success: function (data) {
        //            var Json = $.parseJSON(data);
        //            if (!Json.IsOK) $.messager.alert(' ', Json.ErrorMsg.replace(/\n/g, "<br/>"), 'error');
        //            else {
        //                var arrayJoin = [];
        //                $.each(Json.Result, function (index, object) {
        //                    var ShowMsg = '';
        //                    if (object.IsOK) ShowMsg += $('<a>', { href: 'javascript:void(0)' }).linkbutton({ plain: true, iconCls: 'icon-ok' })[0].outerHTML;
        //                    else ShowMsg += $('<a>', { href: 'javascript:void(0)' }).linkbutton({ plain: true, iconCls: 'icon-no' })[0].outerHTML;
        //                    ShowMsg += object.Result.replace(/\n/g, "<br/>");
        //                    arrayJoin.push(ShowMsg);
        //                });
        //                $.messager.alert(' ', arrayJoin.join('<br/>')).window({ width: 500 }).window('center');
        //            }
        //        },
        //        beforeSend: function () { $.messager.progress({ msg: '執行中...' }); },
        //        complete: function () { $.messager.progress('close'); },
        //        error: function (xhr, ajaxOptions, thrownError) { alert('error'); }
        //    });
        //}

        function gridReload() {
             $("#dataGridMaster").datagrid('reload');
        }

        function openImportExcel() {
            $("#Dialog_Import").dialog("open");
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS" runat="server" AutoApply="True"
                DataMember="HRM_ATTEND_ABSENT_MINUS" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
                Title="請假單" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDelete="checkLockData" OnUpdate="checkLockData" BufferView="False" NotInitGrid="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="請假資料流水碼" Editor="numberbox" FieldName="ABSENT_MINUS_ID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工流水碼" Editor="text" FieldName="EMPLOYEE_ID" Format="" MaxLength="50" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EMPLOYEE_CODE" Format="" MaxLength="0" Width="80" Frozen="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" Format="" MaxLength="0" Width="80" Frozen="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期" Editor="datebox" FieldName="BEGIN_DATE" Format="yyyy/mm/dd" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期" Editor="datebox" FieldName="END_DATE" Format="yyyy/mm/dd" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="請假起始時間" Editor="text" FieldName="BEGIN_TIME" Format="" MaxLength="50" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="請假截止時間" Editor="text" FieldName="END_TIME" Format="" MaxLength="50" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期(含時間)" Editor="datebox" FieldName="ABSENT_DATE_TIME_BEGIN" Format="" Width="80" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期(含時間)" Editor="datebox" FieldName="ABSENT_DATE_TIME_END" Format="" Width="80" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="假別代碼流水號" Editor="numberbox" FieldName="HOLIDAY_ID" Format="" Width="80" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="假別代碼" Editor="text" FieldName="HOLIDAY_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="假別名稱" Editor="text" FieldName="HOLIDAY_CNAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門代碼" Editor="text" FieldName="DEPT_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="DEPT_CNAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="請假時數/天" Editor="numberbox" FieldName="TOTAL_HOURS" Format="" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="請假天數" Editor="numberbox" FieldName="TOTAL_DAY" Format="" Width="80" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="計薪年月" Editor="text" FieldName="SALARY_YYMM" Format="" MaxLength="50" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="MEMO" Format="" MaxLength="200" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="請假單號" Editor="text" FieldName="ABSENT_NO" Format="" MaxLength="50" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="不允許修改" Editor="checkbox" FieldName="NOT_ALLOW_MODIFY" Format="" MaxLength="50" Width="80" FormatScript="genCheckBox" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="不計算" Editor="checkbox" FieldName="NOT_CALCULATE" Format="" MaxLength="50" Width="80" FormatScript="genCheckBox" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="系統產生否" Editor="checkbox" FieldName="SYSCREATE" Format="" MaxLength="50" Width="80" FormatScript="genCheckBox" Sortable="True" EditorOptions="on:'Y',off:'N'" />
                    <JQTools:JQGridColumn Alignment="left" Caption="是否匯入" Editor="checkbox" EditorOptions="on:'Y',off:'N'" FieldName="IS_IMPORT" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Format="" MaxLength="50" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:mm:SS" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Format="" MaxLength="50" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:mm:SS" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動資料紀錄" Editor="text" FieldName="TRANSLOG" FormatScript="HyperlinkLog" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="True" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-importExcel" ItemType="easyui-linkbutton" OnClick="openImportExcel" Text="匯入EXCEL" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn Caption="員工編號" Condition="%%" FieldName="EMPLOYEE_CODE" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工姓名" Condition="%%" DataType="string" Editor="text" FieldName="NAME_C" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="編制部門" Condition="%%" DataType="string" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_HRM_Employee_Share.HRM_DEPT',tableName:'HRM_DEPT',columns:[{field:'DEPT_CODE',title:'編制部門代碼',width:80,align:'left',table:'',queryCondition:''},{field:'DEPT_CNAME',title:'編制部門中文名稱',width:80,align:'left',table:'',queryCondition:''},{field:'DEPT_ENAME',title:'編制部門英文名稱',width:80,align:'left',table:'',queryCondition:''}],columnMatches:[],whereItems:[],valueField:'DEPT_CODE',textField:'DEPT_CNAME',valueFieldCaption:'編制部門代碼',textFieldCaption:'編制部門中文名稱',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="DEPT_CODE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="假別代碼" Condition="%%" DataType="string" Editor="text" FieldName="HOLIDAY_CODE" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="假別名稱" Condition="%%" DataType="string" Editor="text" FieldName="HOLIDAY_CNAME" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始請假日期" Condition="&gt;=" DataType="string" Editor="datebox" FieldName="BEGIN_DATE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="截止請假日期" Condition="&lt;=" DataType="string" Editor="datebox" FieldName="BEGIN_DATE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="請假單" Width="700px" DialogLeft="50px" DialogTop="20px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HRM_ATTEND_ABSENT_MINUS" HorizontalColumnsCount="2" RemoteName="_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS" Closed="False" ContinueAdd="True" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" OnApply="checkAbsentData" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="gridReload" IsAutoPause="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="請假資料流水碼" Editor="numberbox" FieldName="ABSENT_MINUS_ID" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="員工流水碼" Editor="text" FieldName="EMPLOYEE_ID" Format="" MaxLength="50" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="員工編號" Editor="inforefval" FieldName="EMPLOYEE_CODE" Format="" MaxLength="0" Width="180" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_HRM_Attend_Share.dtHRM_BaseAndBasetts_Employed',tableName:'dtHRM_BaseAndBasetts_Employed',columns:[{field:'EMPLOYEE_CODE',title:'員工工號',width:80,align:'left',table:'',queryCondition:''},{field:'NAME_C',title:'員工姓名',width:80,align:'left',table:'',queryCondition:''}],columnMatches:[{field:'EMPLOYEE_ID',value:'EMPLOYEE_ID'},{field:'OVERTIME_DEPT_ID',value:'DEPT'},{field:'DEPT_CODE',value:'DEPT_CODE'}],whereItems:[],valueField:'EMPLOYEE_CODE',textField:'NAME_C',valueFieldCaption:'EMPLOYEE_CODE',textFieldCaption:'NAME_C',cacheRelationText:true,checkData:true,showValueAndText:false,onSelect:timeSet,selectOnly:false" OnBlur="timeSet" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始請假日期" Editor="datebox" FieldName="BEGIN_DATE" Format="" Width="180" OnBlur="checkAbsentHours" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="截止請假日期" Editor="datebox" FieldName="END_DATE" Format="" Width="180" OnBlur="checkAbsentHours" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假起始時間" Editor="text" FieldName="BEGIN_TIME" Format="" MaxLength="50" Width="180" OnBlur="checkAbsentHours" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假截止時間" Editor="text" FieldName="END_TIME" Format="" MaxLength="50" Width="180" OnBlur="checkAbsentHours" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始請假日期(含時間)" Editor="datebox" FieldName="ABSENT_DATE_TIME_BEGIN" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始請假日期(含時間)" Editor="datebox" FieldName="ABSENT_DATE_TIME_END" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="假別代碼流水號" Editor="numberbox" FieldName="HOLIDAY_ID" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="假別代碼" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_HRM_Attend_Share.HRM_ATTEND_HOLIDAY_MINUS',tableName:'HRM_ATTEND_HOLIDAY_MINUS',columns:[],columnMatches:[{field:'HOLIDAY_ID',value:'HOLIDAY_ID'}],whereItems:[],valueField:'HOLIDAY_CODE',textField:'HOLIDAY_CNAME',valueFieldCaption:'假別代碼',textFieldCaption:'假別名稱',cacheRelationText:false,checkData:true,showValueAndText:false,onSelect:checkAbsentHours,selectOnly:false" FieldName="HOLIDAY_CODE" Format="" MaxLength="0" Width="180" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="假別名稱" Editor="text" FieldName="HOLIDAY_CNAME" Format="" MaxLength="0" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假時數/天" Editor="numberbox" FieldName="TOTAL_HOURS" Format="" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假天數" Editor="numberbox" FieldName="TOTAL_DAY" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="計薪年月" Editor="text" FieldName="SALARY_YYMM" Format="" MaxLength="50" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假單號" Editor="text" FieldName="ABSENT_NO" Format="" MaxLength="50" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假事由" Editor="textarea" FieldName="MEMO" Format="" MaxLength="200" NewRow="True" Span="2" Width="480" />
                        <JQTools:JQFormColumn Alignment="left" Caption="不允許修改" Editor="checkbox" FieldName="NOT_ALLOW_MODIFY" Format="" MaxLength="50" Width="25" EditorOptions="on:'Y',off:'N'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="不計算" Editor="checkbox" FieldName="NOT_CALCULATE" Format="" MaxLength="50" Width="25" EditorOptions="on:'Y',off:'N'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="系統產生否" Editor="checkbox" FieldName="SYSCREATE" Format="" MaxLength="50" Width="25" ReadOnly="True" EditorOptions="on:'Y',off:'N'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="是否匯入" Editor="checkbox" EditorOptions="on:'Y',off:'N'" FieldName="IS_IMPORT" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="25" />
                        <JQTools:JQFormColumn Alignment="left" Caption="FLOWFLAG" Editor="text" FieldName="FLOWFLAG" Format="" MaxLength="1" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門代碼" Editor="text" FieldName="DEPT_CODE" Format="" MaxLength="0" ReadOnly="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="DEPT_CNAME" Format="" MaxLength="0" ReadOnly="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" MaxLength="50" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Width="180" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" MaxLength="50" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Width="180" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" ReadOnly="True" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="ABSENT_MINUS_ID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="BEGIN_DATE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="END_DATE" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EMPLOYEE_CODE" RemoteMethod="True" ValidateMessage="請輸入員工工號" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="BEGIN_DATE" RemoteMethod="True" ValidateMessage="請輸入起始請假日期" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="END_DATE" RemoteMethod="True" ValidateMessage="請輸入截止請假日期" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="BEGIN_TIME" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="END_TIME" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="HOLIDAY_CODE" RemoteMethod="True" ValidateMessage="請選擇請假假別" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="checkYearMonthFormat" CheckNull="True" FieldName="SALARY_YYMM" RemoteMethod="False" ValidateMessage="請輸入正確計薪年月格式如 : 201401 或 201412" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="TOTAL_HOURS" RemoteMethod="True" ValidateMessage="請輸入請假時數/天" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
        <!-- 匯入對話框內容的 DIV -->
        <div id="Dialog_Import">

            <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog">
                <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" RemoteName=" " DataMember=" " HorizontalColumnsCount="3" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="檔案名稱" Editor="text" FieldName="FilePathName" Width="80" ReadOnly="true" Visible="false" />
                        <JQTools:JQFormColumn Alignment="left" Caption="員工編號" Editor="infocombobox" FieldName="EMPLOYEE_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始請假日期" Editor="infocombobox" FieldName="BEGIN_DATE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="截止請假日期" Editor="infocombobox" FieldName="END_DATE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假起始時間" Editor="infocombobox" FieldName="BEGIN_TIME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假截止時間" Editor="infocombobox" FieldName="END_TIME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="假別代碼" Editor="infocombobox" FieldName="HOLIDAY_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假時數" Editor="infocombobox" FieldName="TOTAL_HOURS" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假天數" Editor="infocombobox" FieldName="TOTAL_DAY" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="計薪年月" Editor="infocombobox" FieldName="SALARY_YYMM" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假事由" Editor="infocombobox" FieldName="MEMO" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假單號" Editor="infocombobox" FieldName="ABSENT_NO" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EMPLOYEE_ID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="BEGIN_DATE" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="END_DATE" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="BEGIN_TIME" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="END_TIME" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="HOLIDAY_ID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="TOTAL_HOURS" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SALARY_YYMM" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
        <!-- dialog對話框內容的 DIV -->
        <div id="Dialog_TransLog">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="DG_HRM_ATTEND_ABSENT_MINUS_LOG" runat="server" RemoteName="_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS_LOG" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="HRM_ATTEND_ABSENT_MINUS_LOG" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="加班資料流水碼" Editor="numberbox" FieldName="OVERTIME_ID" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工流水碼" Editor="text" FieldName="EMPLOYEE_ID" Format="" MaxLength="50" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EMPLOYEE_CODE" Format="" MaxLength="0" Width="80" Frozen="True" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" Format="" MaxLength="0" Width="80" Frozen="True" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期" Editor="datebox" FieldName="BEGIN_DATE" Format="" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期" Editor="datebox" FieldName="END_DATE" Format="" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="請假起始時間" Editor="text" FieldName="BEGIN_TIME" Format="" MaxLength="50" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="請假截止時間" Editor="text" FieldName="END_TIME" Format="" MaxLength="50" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期(含時間)" Editor="datebox" FieldName="ABSENT_DATE_TIME_BEGIN" Format="" Width="80" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期(含時間)" Editor="datebox" FieldName="ABSENT_DATE_TIME_END" Format="" Width="80" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="假別代碼流水號" Editor="numberbox" FieldName="HOLIDAY_ID" Format="" Width="80" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="假別代碼" Editor="text" FieldName="HOLIDAY_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="假別名稱" Editor="text" FieldName="HOLIDAY_CNAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="部門代碼" Editor="text" FieldName="DEPT_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="DEPT_CNAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="請假時數/天" Editor="numberbox" FieldName="TOTAL_HOURS" Format="" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="請假天數" Editor="numberbox" FieldName="TOTAL_DAY" Format="" Width="80" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="計薪年月" Editor="text" FieldName="SALARY_YYMM" Format="" MaxLength="50" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="MEMO" Format="" MaxLength="200" Width="120" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="請假單號" Editor="text" FieldName="ABSENT_NO" Format="" MaxLength="50" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="不允許修改" Editor="checkbox" FieldName="NOT_ALLOW_MODIFY" Format="" MaxLength="50" Width="80" FormatScript="genCheckBox" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="不計算" Editor="checkbox" FieldName="NOT_CALCULATE" Format="" MaxLength="50" Width="80" FormatScript="genCheckBox" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="系統產生否" Editor="checkbox" FieldName="SYSCREATE" Format="" MaxLength="50" Width="80" FormatScript="genCheckBox" Sortable="True" EditorOptions="on:'Y',off:'N'" />
                        <JQTools:JQGridColumn Alignment="left" Caption="是否匯入" Editor="checkbox" EditorOptions="on:'Y',off:'N'" FieldName="IS_IMPORT" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" MaxLength="50" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Frozen="False" MaxLength="8" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
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
