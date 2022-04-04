<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_Attend_Normal_AbsentMinusFlow.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
        })

        //刪除前判斷是否已有出勤鎖檔紀錄
        function checkLockData(rowData) {
            if (rowData != null && rowData != undefined) {
                var employeeID = rowData.EMPLOYEE_ID;
                var beginDate = rowData.BEGIN_DATE.replace(/\-/g, '/').substr(0, 10);
                var endDate = rowData.END_DATE.replace(/\-/g, '/').substr(0, 10);

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
                data: "mode=method&method=" + "checkAbsentRestHours" + "&parameters=" + absentMinusID + "," + employeeID + "," + beginDate + "," + endDate + "," + beginTime + "," + endTime + "," + holidayID + "," + totalHours + "," + o_totalHours,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false)
                        rows = $.parseJSON(data);
                }
            });

            if (rows.length > 0) {
                if (rows[0].CHECK_REST_HOUR == 'Y') {
                    if (absentMinusID = "0" && rows[0].REST_HOURS < parseFloat($('#dataFormMasterTOTAL_HOURS').numberbox('getValue'))) {
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

        function gridReload() {
            $("#dataGridMaster").datagrid('reload');
        }


    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS" runat="server" AutoApply="True"
                DataMember="HRM_ATTEND_ABSENT_MINUS" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="請假單" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDelete="checkLockData" OnUpdate="checkLockData">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="請假資料流水碼" Editor="numberbox" FieldName="ABSENT_MINUS_ID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工流水碼" Editor="text" FieldName="EMPLOYEE_ID" Format="" MaxLength="50" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EMPLOYEE_CODE" Format="" MaxLength="0" Width="80" Frozen="True" />
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
                    <JQTools:JQGridColumn Alignment="center" Caption="系統產生否" Editor="checkbox" FieldName="SYSCREATE" Format="" MaxLength="50" Width="80" FormatScript="genCheckBox" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Format="" MaxLength="50" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:mm:SS" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Format="" MaxLength="50" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:mm:SS" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Sortable="True" />
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
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="請假單" Width="650px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HRM_ATTEND_ABSENT_MINUS" HorizontalColumnsCount="2" RemoteName="_HRM_Attend_Normal_AbsentMinus.HRM_ATTEND_ABSENT_MINUS" Closed="False" ContinueAdd="True" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" OnApply="checkAbsentData" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="gridReload" IsAutoPause="False">
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
                        <JQTools:JQFormColumn Alignment="left" Caption="假別代碼" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_HRM_Attend_Share.HRM_ATTEND_HOLIDAY_MINUS',tableName:'HRM_ATTEND_HOLIDAY_MINUS',columns:[],columnMatches:[{field:'HOLIDAY_ID',value:'HOLIDAY_ID'}],whereItems:[],valueField:'HOLIDAY_CODE',textField:'HOLIDAY_CNAME',valueFieldCaption:'假別代碼',textFieldCaption:'假別名稱',cacheRelationText:false,checkData:true,showValueAndText:false,onSelect:checkAbsentHours,selectOnly:false" FieldName="HOLIDAY_CODE" Format="" MaxLength="0" Width="180" OnBlur="checkAbsentHours" />
                        <JQTools:JQFormColumn Alignment="left" Caption="假別名稱" Editor="text" FieldName="HOLIDAY_CNAME" Format="" MaxLength="0" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假時數/天" Editor="numberbox" FieldName="TOTAL_HOURS" Format="" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假天數" Editor="numberbox" FieldName="TOTAL_DAY" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="計薪年月" Editor="text" FieldName="SALARY_YYMM" Format="" MaxLength="50" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假事由" Editor="textarea" FieldName="MEMO" Format="" MaxLength="200" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假單號" Editor="text" FieldName="ABSENT_NO" Format="" MaxLength="50" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="不允許修改" Editor="checkbox" FieldName="NOT_ALLOW_MODIFY" Format="" MaxLength="50" Width="25" EditorOptions="on:'Y',off:'N'" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="不計算" Editor="checkbox" FieldName="NOT_CALCULATE" Format="" MaxLength="50" Width="25" EditorOptions="on:'Y',off:'N'" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="系統產生否" Editor="checkbox" FieldName="SYSCREATE" Format="" MaxLength="50" Width="25" ReadOnly="True" EditorOptions="on:'Y',off:'N'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="FLOWFLAG" Editor="text" FieldName="FLOWFLAG" Format="" MaxLength="1" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門代碼" Editor="text" FieldName="DEPT_CODE" Format="" MaxLength="0" ReadOnly="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="DEPT_CNAME" Format="" MaxLength="0" ReadOnly="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Format="" MaxLength="50" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Format="" MaxLength="50" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="ABSENT_MINUS_ID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="BEGIN_DATE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="END_DATE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="EMPLOYEE_CODE" RemoteMethod="True" />
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
    </form>
</body>
</html>
