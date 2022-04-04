﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_AttendAbsentMinusFlow.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jquery.jbjob.js"></script>
    <title>請假單</title>
    <script>
        $(document).ready(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "#FFFFB5");
            });

            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });


            $("#dataFormMasterHolidayID").data("inforefval").refval.find("input.refval-text").blur(function () {
                timeSet();
            });
            $("#dataFormMasterHolidayID").data("inforefval").refval.find("input.refval-text").blur(function () {
                checkAbsentHours();
            });

            $("#dataFormMasterBeginDate").combo('textbox').blur(function () {               
                timeSet();
                checkAbsentHours();              
            });

            $("#dataFormMasterBeginDate").datebox({
                onSelect: function (date) {                   
                    timeSet();
                    checkAbsentHours();
                }
            });

            $("#dataFormMasterEndDate").combo('textbox').blur(function () {               
                timeSet();
                checkAbsentHours();               
            });

            $("#dataFormMasterEndDate").datebox({
                onSelect: function (date) {                   
                    timeSet();
                    checkAbsentHours();                   
                }
            });
        })

        function getEmployeeID() {
            //由登入的UserID 到 HRM_BASE_BASE 取得 EMPLOYEE_ID
            var EmployeeID = getClientInfo("UserID");
            var EMPLOYEE_ID;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster', //連接的Server端，command
                data: "mode=method&method=" + "getEmployeeID" + "&parameters=" + EmployeeID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        EMPLOYEE_ID = $.parseJSON(data);
                    }
                }
            });
            if (EMPLOYEE_ID != "0" && EMPLOYEE_ID != "undefined") {
                return EMPLOYEE_ID;
            }
        }

        function OnLoadFormMaster() {
            var employeeID = $("#dataFormMasterEmployeeID").refval('getValue');
            var dt = new Date();
            var sDate = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')

            if (employeeID != "") {

                //取得申請時的部門名稱,班別
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendAbsent.HRMAttendAbsentApply', //連接的Server端，command
                    data: "mode=method&method=" + "getDeptInfo" + "&parameters=" + employeeID + "," + sDate,
                    cache: false,
                    async: false,
                    success: function (data) {
                        var rows = $.parseJSON(data);
                        if (rows.length > 0) {                           
                            $('#dataFormMasterDEPT_Name').val(rows[0].DEPT_CNAME);                            
                            $('#dataFormMasterROTE_Name').val(rows[0].ROTE_CNAME);
                        }
                    }
                });
            }
            //請起時間(ON_TIME) && 請迄時間(OFF_TIME)
            timeSet();
            // GetHolidayData();
        }
        //得到假別資訊(已請、剩餘)
        var flag = true; //定義一個全域變數，只有第一次執行        
        function OnLoadHolidayData() {
            if (flag) {
                var employeeID = $("#dataFormMasterEmployeeID").refval('getValue');
                var dt = new Date();
                var sDate = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')

                if (employeeID != "") {

                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendAbsent.infoABSENT_PLUS',  //連接的Server端，command
                        data: "mode=method&method=" + "GetHolidayData" + "&parameters=" + employeeID + "," + sDate,
                        cache: false,
                        async: true,
                        success: function (data) {
                            var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示                            
                            $('#GridHolidayData').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                            ////拿掉項次
                            //$('#GridHolidayData').datagrid({ rownumbers: false });
                        }
                    });
                }
                flag = false;
            }
        }
        //請起時間(ON_TIME) && 請迄時間(OFF_TIME)
        function timeSet() {
            var beginDate = $('#dataFormMasterBeginDate').datebox('getValue');
            var endDate = $('#dataFormMasterEndDate').datebox('getValue');
            var employeeID = $("#dataFormMasterEmployeeID").refval('getValue');
            var beginDateValidate = $.fn.datebox('parseDate', beginDate.replace(/\//g, '-'));
            var endDateValidate = $.fn.datebox('parseDate', endDate.replace(/\//g, '-'));

            if (employeeID != "") {
                if (beginDateValidate != "Invalid Date" && endDateValidate != "Invalid Date" && $.jbIsDateStr(beginDate) && $.jbIsDateStr(endDate) && endDate >= beginDate) {
                    //計薪年月(SALARY_YYMM) && 請起時間(ON_TIME) && 請迄時間(OFF_TIME)
                    var dateLockCnt;
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendAbsent.HRMAttendAbsentApply', //連接的Server端，command
                        data: "mode=method&method=" + "getSalaryYYMM" + "&parameters=" + employeeID + "," + beginDate, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            var rows = $.parseJSON(data);
                            if (rows.length > 0) {                              
                                if ($('#dataFormMasterBeginTime').val() == "")
                                    $('#dataFormMasterBeginTime').val(rows[0].ON_TIME);
                                if ($('#dataFormMasterEndTime').val() == "")
                                    $('#dataFormMasterEndTime').val(rows[0].OFF_TIME);
                            }
                        }
                    });
                }
            }
        }

        //計算請假時數/天
        function checkAbsentHours() {
            var employeeID = $("#dataFormMasterEmployeeID").refval('getValue');
            var beginDate = $('#dataFormMasterBeginDate').datebox('getValue');
            var endDate = $('#dataFormMasterEndDate').datebox('getValue');
            var beginDateValidate = $.fn.datebox('parseDate', beginDate.replace(/\//g, '-'));
            var endDateValidate = $.fn.datebox('parseDate', endDate.replace(/\//g, '-'));
            var beginTime = $('#dataFormMasterBeginTime').val();
            var endTime = $('#dataFormMasterEndTime').val();
            var holidayID = $('#dataFormMasterHolidayID').refval('getValue');

            var absentMinusID;

            if (getEditMode($("#dataFormMaster")) == 'updated')
                absentMinusID = $('#dataFormMasterAbsentMinusID').val();
            else
                absentMinusID = "0";

            //判斷起迄時間是否正確
            beginTimeValidate = $.jbIsTimeFormat(beginTime);
            endTimeValidate = $.jbIsTimeFormat(endTime);

            if (employeeID != "" && holidayID != "" && beginDate != "" && endDate != "" && beginTime != "" && endTime != "" && beginDateValidate != "Invalid Date" && endDateValidate != "Invalid Date" && $.jbIsDateStr(beginDate) && $.jbIsDateStr(endDate) && beginTimeValidate && endTimeValidate) {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendAbsent.HRMAttendAbsentApply', //連接的Server端，command
                    data: "mode=method&method=" + "checkAbsentHours" + "&parameters=" + absentMinusID + "," + employeeID + "," + beginDate + "," + endDate + "," + beginTime + "," + endTime + "," + holidayID,
                    cache: false,
                    async: false,
                    success: function (data) {
                        var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                        if (rows.length > 0) {
                            if (rows[0].totalHours == "0") {
                                //alert("此筆請假日期無對應的出勤，請先產生出勤資料");
                                $('#dataFormMasterTotalHours').numberbox('setValue', "");
                                return false;
                            }
                            else
                            $('#dataFormMasterTotalHours').numberbox('setValue', rows[0].totalHours);
                        }
                    }
                });
            }
            else {
                $('#dataFormMasterTotalHours').numberbox('setValue', "");
            }
        }

        //存檔前
        //1. 檢查combox 必要欄位
        //2. 判斷請假起始日期不可大於截止日期
        //3. 判斷請假起始時間不可大於截止時間
        //3.1 判斷生理假起始日期須等於截止日期   
        //3.2 判斷假別性別限制
        //4. 判斷請假剩餘時數(順便產生得假資料)
        //5. 判斷請假時數
        //6. 判斷申請的時段內是否已有存在的請假資料
        //7. 判斷請假資料(在途)
        function checkAbsentData() {
            if (!$(this).form('validateForm')) return false;                
            var employeeID = $("#dataFormMasterEmployeeID").refval('getValue');
            var AgentEmployeeID = $("#dataFormMasterAgentEmployeeID").refval('getValue');
            var beginDate = $('#dataFormMasterBeginDate').datebox('getValue');
            var endDate = $('#dataFormMasterEndDate').datebox('getValue');
            var beginDateValidate = $.fn.datebox('parseDate', beginDate.replace(/\//g, '-'));
            var endDateValidate = $.fn.datebox('parseDate', endDate.replace(/\//g, '-'));
            var beginTime = $('#dataFormMasterBeginTime').val();
            var endTime = $('#dataFormMasterEndTime').val();
            var holidayID = $("#dataFormMasterHolidayID").refval('getValue');
            var totalHours = $('#dataFormMasterTotalHours').numberbox('getValue');
            var absentMinusID;
         
            ////代理人
            //if (AgentEmployeeID == "" || AgentEmployeeID == undefined) {
            //    alert("請選擇代理人");
            //    $("#AgentEmployeeID").data("inforefval").refval.find("input.refval-text").focus();
            //    return false;
            //}
            ////假別代碼
            //if (holidayID == "" || holidayID == undefined) {
            //    alert("請選擇假別代碼");
            //    $("#dataFormMasterHolidayID").data("inforefval").refval.find("input.refval-text").focus();
            //    return false;
            //}

            //2. 判斷請假起始日期不可大於截止日期
            if (beginDateValidate == "Invalid Date" || !$.jbIsDateStr(beginDate)) {
                alert('請假起始日期:' + beginDate + '格式錯誤');
                $("#dataFormMasterBeginDate").datebox('textbox').focus();
                return false;
            }

            if (endDateValidate == "Invalid Date" || !$.jbIsDateStr(endDate)) {
                alert('請假截止日期:' + endDate + '格式錯誤');
                $("#dataFormMasterEndDate").datebox('textbox').focus();
                return false;
            }

            if (beginDate > endDate) {
                alert('請假起始日期 : ' + beginDate + ' 需小於請假截止日期 : ' + endDate);
                $("#dataFormMasterBeginDate").datebox('textbox').focus();
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
                url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendAbsent.HRMAttendAbsentApply', //連接的Server端，command
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
                url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendAbsent.HRMAttendAbsentApply', //連接的Server端，command
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

            if (getEditMode($("#dataFormMaster")) == 'updated')
                absentMinusID = $('#dataFormMasterAbsentMinusID').val();
            else
                absentMinusID = "0";

            if ($("#dataGridMaster").datagrid('getSelected')) {
                var o_employeeID = $("#dataGridMaster").datagrid('getSelected').EmployeeID;
                var o_beginDate = $("#dataGridMaster").datagrid('getSelected').BeginDate.replace(/\-/g, '/').substr(0, 10);
                var o_endDate = $("#dataGridMaster").datagrid('getSelected').EndDate.replace(/\-/g, '/').substr(0, 10);
                var o_beginTime = $("#dataGridMaster").datagrid('getSelected').BeginTime;
                var o_endTime = $("#dataGridMaster").datagrid('getSelected').EndTime;
                var o_totalHours = $("#dataGridMaster").datagrid('getSelected').TotalHours;
            }
            else
                var o_totalHours = 0;

            //4. 判斷請假剩餘時數(順便產生得假資料)
            var rows;

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendAbsent.HRMAttendAbsentApply', //連接的Server端，command
                data: "mode=method&method=" + "checkAbsentRestHours" + "&parameters=" + absentMinusID + "," + employeeID + "," + beginDate + "," + endDate + "," + beginTime + "," + endTime + "," + holidayID + "," + totalHours,
                cache: false,
                async: false,
                success: function (data) {
                    var result = $.parseJSON(data);
                    if (result != false) rows = result;
                }
            });

            if (rows!=null && rows.length > 0) {
                if (rows[0].CHECK_REST_HOUR == 'Y') {
                    if (absentMinusID == "0" && rows[0].REST_HOURS < parseFloat($('#dataFormMasterTotalHours').numberbox('getValue'))) {
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
                    if (absentMinusID == "0" && rows[0].FAMILY_HOURS != "0.00" && parseFloat(rows[0].FAMILY_MAX_HOURS) - parseFloat(rows[0].FAMILY_HOURS) < parseFloat($('#dataFormMasterTotalHours').numberbox('getValue'))) {
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
            }

            //5. 判斷請假時數
            var rows;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendAbsent.HRMAttendAbsentApply', //連接的Server端，command
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
                if (rows[0].totalHours != parseFloat($('#dataFormMasterTotalHours').numberbox('getValue'))) {
                    alert("請假時數不正確(請假時數 : " + rows[0].hours + "小時)");
                    $('#dataFormMasterTotalHours').numberbox('setValue', rows[0].hours);
                    return false;
                }
            }
           
            //6. 判斷請假資料申請的時段內是否已有存在的請假資料 
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var cnt;
                absentMinusID = "0";

                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendAbsent.HRMAttendAbsentApply', //連接的Server端，command
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

                //7. 判斷請假資料(在途)

                var sd = new Date(beginDate);
                var bDate = $.jbjob.Date.DateFormat(sd, 'yyyy/MM/dd');

                var ed = new Date(endDate);
                var eDate = $.jbjob.Date.DateFormat(ed, 'yyyy/MM/dd');
                var beginDateTime = bDate + " " + beginTime.substr(0, 2) + ":" + beginTime.substr(2, 2);
                var endDateTime = eDate + " " + endTime.substr(0, 2) + ":" + endTime.substr(2, 2)

                var cnt2;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendAbsent.HRMAttendAbsentApply', //連接的Server端，command
                    data: "mode=method&method=" + "checkOnData" + "&parameters=" + absentMinusID + "," + employeeID + "," + beginDateTime + "," + endDateTime, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt2 = $.parseJSON(data);
                        }
                    }
                });
                if ((cnt2 != "0" && cnt2 != "undefined")) {
                    alert("申請的時段內已有在途的請假資料");
                    return false;
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

        function gridReload() {
            $("#dataGridMaster").datagrid('reload');
        }
       

    </script>

    </head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sHRMAttendAbsent.HRMAttendAbsentApply" runat="server" AutoApply="True"
                DataMember="HRMAttendAbsentApply" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="請假單" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" BufferView="False" NotInitGrid="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="請假資料流水碼" Editor="numberbox" FieldName="AbsentMinusID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工流水碼" Editor="text" FieldName="EmployeeID" Format="" MaxLength="50" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期" Editor="datebox" FieldName="BeginDate" Format="" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期" Editor="datebox" FieldName="EndDate" Format="" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="請假起始時間" Editor="text" FieldName="BeginTime" Format="" MaxLength="50" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="請假截止時間" Editor="text" FieldName="EndTime" Format="" MaxLength="50" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期(含時間)" Editor="datebox" FieldName="AbsentDateTimeBegin" Format="" Width="80" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期(含時間)" Editor="datebox" FieldName="AbsentDateTimeEnd" Format="" Width="80" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="假別代碼流水號" Editor="numberbox" FieldName="HolidayID" Format="" Width="80" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="請假時數/天" Editor="numberbox" FieldName="TotalHours" Format="" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Memo" Format="" MaxLength="200" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="50" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd HH:mm:SS" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Sortable="True" />
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
                    <JQTools:JQQueryColumn Caption="員工編號" Condition="%%" FieldName="EMPLOYEE_CODE" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工姓名" Condition="%%" DataType="string" Editor="text" FieldName="NAME_C" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="編制部門" Condition="%%" DataType="string" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_HRM_Employee_Share.HRM_DEPT',tableName:'HRM_DEPT',columns:[{field:'DEPT_CODE',title:'編制部門代碼',width:80,align:'left',table:'',queryCondition:''},{field:'DEPT_CNAME',title:'編制部門中文名稱',width:80,align:'left',table:'',queryCondition:''},{field:'DEPT_ENAME',title:'編制部門英文名稱',width:80,align:'left',table:'',queryCondition:''}],columnMatches:[],whereItems:[],valueField:'DEPT_CODE',textField:'DEPT_CNAME',valueFieldCaption:'編制部門代碼',textFieldCaption:'編制部門中文名稱',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="DEPT_CODE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="假別代碼" Condition="%%" DataType="string" Editor="text" FieldName="HOLIDAY_CODE" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="假別名稱" Condition="%%" DataType="string" Editor="text" FieldName="HOLIDAY_CNAME" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="請假單" Width="550px" DialogLeft="50px" DialogTop="30px" Closed="True">
         
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HRMAttendAbsentApply" HorizontalColumnsCount="2" RemoteName="sHRMAttendAbsent.HRMAttendAbsentApply" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="True" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" OnApply="checkAbsentData" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Dialog" OnApplied="gridReload" IsAutoPause="False" OnLoadSuccess="OnLoadFormMaster" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="請假單號" Editor="numberbox" FieldName="AbsentMinusID" Format="" Width="150" Visible="False" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請姓名" Editor="inforefval" FieldName="EmployeeID" Format="" Width="90" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sHRMAttendAbsent.infoHRM_BASE_BASE',tableName:'infoHRM_BASE_BASE',columns:[],columnMatches:[{field:'EmployeeText',value:'NAME_C'}],whereItems:[],valueField:'EMPLOYEE_ID',textField:'NAME_C',valueFieldCaption:'EMPLOYEE_ID',textFieldCaption:'NAME_C',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none'" OnBlur="" NewRow="True" ReadOnly="True" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="代理人" Editor="inforefval" EditorOptions="title:'選擇代理人',panelWidth:350,remoteName:'sHRMAttendAbsent.infoAgentEmployeeID',tableName:'infoAgentEmployeeID',columns:[],columnMatches:[],whereItems:[],valueField:'EMPLOYEE_ID',textField:'NAME_C',valueFieldCaption:'工號',textFieldCaption:'姓名',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="AgentEmployeeID" MaxLength="0" Visible="True" Width="110" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="DEPT_Name" NewRow="True" ReadOnly="True" Span="1" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="班別名稱" Editor="text" FieldName="ROTE_Name" Width="130" ReadOnly="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="BeginDate" Format="" Width="90" OnBlur="checkAbsentHours" Span="1" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止日期" Editor="datebox" FieldName="EndDate" Format="" Width="90" OnBlur="checkAbsentHours" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始時間" Editor="text" FieldName="BeginTime" Format="" MaxLength="50" Width="90" OnBlur="checkAbsentHours" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止時間" Editor="text" FieldName="EndTime" Format="" MaxLength="50" Width="90" OnBlur="checkAbsentHours" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始請假日期(含時間)" Editor="datebox" FieldName="AbsentDateTimeBegin" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始請假日期(含時間)" Editor="datebox" FieldName="AbsentDateTimeEnd" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="假別選擇" Editor="inforefval" FieldName="HolidayID" Format="" Width="140" Visible="True" MaxLength="0" EditorOptions="title:'假別選擇',panelWidth:350,remoteName:'sHRMAttendAbsent.infoHRM_ATTEND_HOLIDAY',tableName:'infoHRM_ATTEND_HOLIDAY',columns:[],columnMatches:[{field:'HolidayText',value:'HOLIDAY_CNAME'}],whereItems:[],valueField:'HOLIDAY_ID',textField:'HOLIDAY_CNAME',valueFieldCaption:'假別代號',textFieldCaption:'假別名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,onSelect:checkAbsentHours,selectOnly:false,capsLock:'none'" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="HolidayText" Editor="text" FieldName="HolidayText" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假時數/天" Editor="numberbox" FieldName="TotalHours" Format="" MaxLength="0" Width="90" Visible="True" EditorOptions="precision:1" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假事由" Editor="textarea" FieldName="Memo" Format="" Visible="True" Width="380" ReadOnly="False" MaxLength="200" NewRow="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" Width="180" ReadOnly="False" Visible="False" MaxLength="1" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="50" Width="180" NewRow="False" Span="1" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CreateDate" Format="" MaxLength="0" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AbsentMinusID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="EmployeeID" RemoteMethod="False" DefaultMethod="getEmployeeID" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="BeginDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="EndDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="BeginDate" RemoteMethod="True" ValidateMessage="請輸入起始請假日期" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EndDate" RemoteMethod="True" ValidateMessage="請輸入截止請假日期" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="BeginTime" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="EndTime" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="HOLIDAY_CODE" RemoteMethod="True" ValidateMessage="請選擇請假假別" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="TotalHours" RemoteMethod="True" ValidateMessage="請輸入請假時數/天" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AgentEmployeeID" RemoteMethod="True" ValidateMessage="請選擇代理人" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="HolidayID" RemoteMethod="True" ValidateMessage="請選擇假別" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDataGrid ID="GridHolidayData" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="infoABSENT_PLUS" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sHRMAttendAbsent.infoABSENT_PLUS" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="227px" OnLoadSuccess="OnLoadHolidayData">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="假別/小時" Editor="text" FieldName="HOLIDAY_CNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="已請" Editor="numberbox" EditorOptions="precision:1" FieldName="ABSENT_HOURS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="未核" Editor="numberbox" EditorOptions="precision:1" FieldName="WAIT_HOURS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="剩餘" Editor="numberbox" EditorOptions="precision:1" FieldName="REST_HOURS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                        </JQTools:JQGridColumn>
                    </Columns>
                </JQTools:JQDataGrid>
               
            </JQTools:JQDialog>
        </div>
    </form>
    </body>
</html>
