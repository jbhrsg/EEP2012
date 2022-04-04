<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_Attend_Normal_Overtime.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>加班單</title>
    RERE
    <script>
        $(document).ready(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "#FFFFB5");
            });

            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });

            $("#dataFormMasterOVERTIME_DATE").closest('td').append('  (請輸入日期格式 : YYYY/MM/DD)').css("color", "blue");

            $("#dataFormMasterEMPLOYEE_CODE").data("inforefval").refval.find("input.refval-text").blur(function () {
                getEffectDate();
            });

            $("#dataFormMasterOVERTIME_DATE").combo('textbox').blur(function () {
                var employeeCode = $('#dataFormMasterEMPLOYEE_ID').val();
                if (employeeCode == "") {
                    alert("請先點選員工工號");
                    $("#dataFormMasterEMPLOYEE_CODE").data("inforefval").refval.find("input.refval-text").focus();
                }
                else
                    getEffectDate();
            });

            $("#dataFormMasterOVERTIME_DATE").datebox({
                onSelect: function (date) {
                    var employeeCode = $('#dataFormMasterEMPLOYEE_ID').val();
                    if (employeeCode == "") {
                        alert("請先點選員工工號");
                        $("#dataFormMasterEMPLOYEE_CODE").data("inforefval").refval.find("input.refval-text").focus();
                    }
                    else
                        getEffectDate();
                }
            });

            //-------------------------------欄位配對視窗送出按鈕------------------------------
            $('#DialogSubmit', '#Dialog_ImportMain').removeAttr('onclick').on('click', function () {
                if (!$('#DataForm_ImportMain').form('validateForm')) return;    //驗證                    
                var data = $('#DataForm_ImportMain').jbDataFormGetAFormData();  //取資料
                $.messager.progress({ msg: 'Loading...' });                 //進度條開始
                //送出上傳動作
                $.ajaxSetup({ async: false });
                $.post('../handler/JqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_Overtime', {
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
        });

        //刪除前判斷是否已有出勤鎖檔紀錄
        function checkLockData(rowData) {
            if (rowData != null && rowData != undefined) {
                var overtimeID = rowData.OVERTIME_ID;
                var employeeID = rowData.EMPLOYEE_ID;
                var beginDate = rowData.OVERTIME_DATE.replace(/\-/g, '/').substr(0, 10);
                var endDate = rowData.OVERTIME_DATE.replace(/\-/g, '/').substr(0, 10);
                var restHours = rowData.REST_HOURS;
               
                var dateLockCnt;
                var flag;

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
                    alert("此筆加班日期已有鎖檔紀錄，無法刪除");
                    return false;
                }
                else //判斷補休可沖銷的剩餘時數
                {
                    if (restHours == 0)
                        return true;
                    else {
                        $.ajax({
                            type: "POST",
                            url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_Overtime.HRM_ATTEND_OVERTIME_DATA', //連接的Server端，command
                            data: "mode=method&method=" + "checkCompensatoryData" + "&parameters=" + overtimeID + "," + employeeID + "," + beginDate + "," + endDate + "," + restHours + "," + restHours + "," + "deleted", //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                            cache: false,
                            async: false,
                            success: function (data) {
                                if (data != "False")
                                    flag = true;
                                else 
                                    flag = false;
                            },
                        });
                        if (!flag) {
                            alert("此筆資料已有補休休假紀錄且無剩餘補修時數可供沖銷, 無法刪除");
                            return false;
                        }
                    }
                }
            }
        }

        //存檔前
        //1. 檢查combox 必要欄位
        //2. 判斷加班起始日期不可大於截止日期
        //3. 判斷加班時數
        //4. 判斷加班日期是否已有鎖檔紀錄
        //5. 判斷加班資料(EMPLOYEE_ID/OVERTIME_DATE_TIME_BEGIN/OVERTIME_DATE_TIME_END)申請的時段內是否已有存在的加班資料
        //6. 判斷補休時數
        
      
        function checkOvertimeData() {
            //1. 檢查combox 必要欄位
            var overtimeRoteCode = $("#dataFormMasterROTE_CODE").refval('getValue');
            var overtimeDeptCode = $("#dataFormMasterDEPT_CODE").refval('getValue');
            var overtimeCauseCode = $("#dataFormMasterCAUSE_CODE").refval('getValue');

            //班別代碼
            if (overtimeRoteCode == "" || overtimeRoteCode == undefined) {
                alert("請選擇班別代碼");
                $("#dataFormMasterROTE_CODE").data("inforefval").refval.find("input.refval-text").focus();
                return false;
            }

            //班別代碼
            if (overtimeRoteCode == "00") {
                alert("加班班別不可以選擇假日班，請選擇適用該加班時段津貼的班別");
                $("#dataFormMasterROTE_CODE").data("inforefval").refval.find("input.refval-text").focus();
                return false;
            }

            //2. 判斷加班起始時間不可大於截止時間
            var beginTime = $("#dataFormMasterBEGIN_TIME").val();
            var endTime = $("#dataFormMasterEND_TIME").val();
            if (parseInt(beginTime) >= parseInt(endTime)) {
                alert('加班起始時間 : ' + beginTime + ' 需小於加班截止時間 : ' + endTime);
                return false;
            }

            //3. 加班時數及補休時數只能擇一申請
            //var overtimeHours = $("#dataFormMasterOVERTIME_HOURS").numberbox('getValue');
            //var restHours = $("#dataFormMasterREST_HOURS").numberbox('getValue');
            //if (parseInt(overtimeHours) > 0 && parseInt(restHours) > 0) {
            //    alert("加班時數及補休時數只能擇一申請");
            //    return false;
            //}


            var employeeID = $('#dataFormMasterEMPLOYEE_ID').val();
            var overtimeDate = $('#dataFormMasterOVERTIME_DATE').datebox('getValue');
            var beginTime = $('#dataFormMasterBEGIN_TIME').val();
            var endTime = $('#dataFormMasterEND_TIME').val();
            var restHours = $('#dataFormMasterREST_HOURS').val();
            //var overtimeDateBegin = $('#dataFormMasterOVERTIME_DATE_TIME_BEGIN').datebox('getValue');
            //var overtimeDateEnd = $('#dataFormMasterOVERTIME_DATE_TIME_END').datebox('getValue');

            if (getEditMode($("#dataFormMaster")) == 'updated')
                overtimeID = $('#dataFormMasterOVERTIME_ID').val();
            else
                overtimeID = "0";

            if ($("#dataGridMaster").datagrid('getSelected')) {
                var o_employeeID = $("#dataGridMaster").datagrid('getSelected').EMPLOYEE_ID;
                var o_overtimeDate = $("#dataGridMaster").datagrid('getSelected').OVERTIME_DATE.replace(/\-/g, '/').substr(0, 10);
                var o_beginTime = $("#dataGridMaster").datagrid('getSelected').BEGIN_TIME;
                var o_endTime = $("#dataGridMaster").datagrid('getSelected').END_TIME;
                var o_restHours = $("#dataGridMaster").datagrid('getSelected').REST_HOURS;
                //var o_overtimeDateBegin = $("#dataGridMaster").datagrid('getSelected').OVERTIME_DATE_TIME_BEGIN.replace(/\-/g, '/').substr(0, 10);
                //var o_overtimeDateEnd = $("#dataGridMaster").datagrid('getSelected').OVERTIME_DATE_TIME_END.replace(/\-/g, '/').substr(0, 10);
            }

            //3. 判斷加班時數
            var rows;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_Overtime.HRM_ATTEND_OVERTIME_DATA', //連接的Server端，command
                data: "mode=method&method=" + "checkOvertimeHours" + "&parameters=" + overtimeID + "," + employeeID + "," + overtimeDate + "," + beginTime + "," + endTime,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false)
                        rows = $.parseJSON(data);
                }
            });

            if (rows.length > 0) {
                if (rows[0].rejectCode != "") {
                    switch (rows[0].rejectCode) {
                        case "1": alert("申請日期查無出勤刷卡資料"); break;
                        case "2": alert("申請日期出勤刷卡資料不完整"); break;
                        case "3": alert("申請時間未在刷卡時段內"); break;
                        case "4": alert("申請日期查無出勤資料"); break;
                        case "5": alert("加班起始時間未在合理時間範圍內"); $('#dataFormMasterBEGIN_TIME').focus(); break;
                        case "6": alert("加班截止時間未在合理時間範圍內"); $('#dataFormMasterEND_TIME').focus(); break;
                    }
                    return false;
                }
                else {
                    if (rows[0].hours == 0) {
                        alert("申請的時段為上班時間");
                        $('#dataFormMasterOVERTIME_HOURS').numberbox('setValue', rows[0].hours);
                        return false;
                    }

                    if (rows[0].hours != parseFloat($('#dataFormMasterOVERTIME_HOURS').numberbox('getValue')) + parseFloat($('#dataFormMasterREST_HOURS').numberbox('getValue'))) {
                        alert("加班總時數不正確(加班總時數" + rows[0].hours + "小時)");
                        $('#dataFormMasterOVERTIME_HOURS').focus();
                        //$('#dataFormMasterOVERTIME_HOURS').numberbox('setValue', rows[0].hours);
                        return false;
                    }
                }
            }

            //4. 修改判斷加班日期是否已有鎖檔紀錄
            if (getEditMode($("#dataFormMaster")) == 'updated') {
                var dateLockCnt = 0;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Share.HRM_ATTEND_DATA_LOCK', //連接的Server端，command
                    data: "mode=method&method=" + "checkAttendDataLock" + "&parameters=" + employeeID + "," + overtimeDate + "," + overtimeDate, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            dateLockCnt = $.parseJSON(data);
                        }
                    }
                });
                if (dateLockCnt != "0" && dateLockCnt != "undefined") {
                    alert("此區間加班日期已有鎖檔紀錄");
                    return false;
                }
                //5. 判斷加班資料(EMPLOYEE_ID/OVERTIME_DATE_TIME_BEGIN/OVERTIME_DATE_TIME_END)申請的時段內是否已有存在的加班資料        
                else {
                    if (o_employeeID != employeeID || o_beginTime != beginTime || o_endTime != endTime) {
                        var cnt;
                        $.ajax({
                            type: "POST",
                            url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_Overtime.HRM_ATTEND_OVERTIME_DATA', //連接的Server端，command
                            data: "mode=method&method=" + "checkOvertimeData" + "&parameters=" + overtimeID + "," + employeeID + "," + overtimeDate + "," + beginTime + "," + endTime, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                            cache: false,
                            async: false,
                            success: function (data) {
                                if (data != false) {
                                    cnt = $.parseJSON(data);
                                }
                            }
                        });
                        if ((cnt != "0" && cnt != "undefined")) {
                            alert("申請的時段內已有存在的加班資料");
                            return false;
                        }
                    }
                }
            }
            else {
                if (getEditMode($("#dataFormMaster")) == 'inserted') {
                    var cnt;
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_Overtime.HRM_ATTEND_OVERTIME_DATA', //連接的Server端，command
                        data: "mode=method&method=" + "checkOvertimeData" + "&parameters=" + overtimeID + "," + employeeID + "," + overtimeDate + "," + beginTime + "," + endTime, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            if (data != false) {
                                cnt = $.parseJSON(data);
                            }
                        }
                    });
                    if (cnt != "0" && cnt != "undefined") {
                        alert("申請的時段內已有存在的加班資料");
                        return false;
                    }
                }
            }

            //6. 判斷補休時數
            if (getEditMode($("#dataFormMaster")) == 'updated' && restHours < o_restHours) {
                var flag = true;

                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_Overtime.HRM_ATTEND_OVERTIME_DATA', //連接的Server端，command
                    data: "mode=method&method=" + "checkCompensatoryData" + "&parameters=" + overtimeID + "," + employeeID + "," + overtimeDate + "," + o_overtimeDate + "," + restHours + "," + o_restHours + "," + "updated", //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False")
                            flag = true;
                        else
                            flag = false;
                    },
                });
                if (!flag) {
                    alert("此筆資料已有補休休假紀錄, 無法修改");
                    return false;
                }
                else
                    return true;
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

        //check 時間格式如 : 0800 或 0830
        function checkTimeFormat(val) {
            return $.jbIsTimeFormat(val);
        }

        //check 計薪年月格式如 : 201401 或 201412
        function checkYearMonthFormat(val) {
            return $.jbIsYearMonthStr(val)
        }

        //計算加班時數
        function checkOvertimeHours() {
            var employeeID = $('#dataFormMasterEMPLOYEE_ID').val();
            var overtimeDate = $('#dataFormMasterOVERTIME_DATE').datebox('getValue');
            var beginTime = $('#dataFormMasterBEGIN_TIME').val();
            var endTime = $('#dataFormMasterEND_TIME').val();
            if (getEditMode($("#dataFormMaster")) == 'updated')
                overtimeID = $('#dataFormMasterOVERTIME_ID').val();
            else
                overtimeID = "0";

            //判斷起迄時間是否正確
            beginTimeValidate = $.jbIsTimeFormat(beginTime);
            endTimeValidate = $.jbIsTimeFormat(endTime);

            if (employeeID != "" && overtimeDate != "" && beginTime != "" && endTime != "" && beginTimeValidate && endTimeValidate) {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_Overtime.HRM_ATTEND_OVERTIME_DATA', //連接的Server端，command
                    data: "mode=method&method=" + "checkOvertimeHours" + "&parameters=" + overtimeID + "," + employeeID + "," + overtimeDate + "," + beginTime + "," + endTime,
                    cache: false,
                    async: false,
                    success: function (data) {
                        var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                        if (rows.length > 0) {
                            if (rows[0].rejectCode != "") {
                                switch (rows[0].rejectCode) {
                                    case "1": alert("申請日期查無出勤刷卡資料"); break;
                                    case "2": alert("申請日期出勤刷卡資料不完整"); break;
                                    case "3": alert("申請時間未在刷卡時段內"); break;
                                    case "4": alert("申請日期查無出勤資料"); break;
                                    case "5": alert("加班起始時間未在合理時間範圍內"); $('#dataFormMasterBEGIN_TIME').focus(); break;
                                    case "6": alert("加班截止時間未在合理時間範圍內"); $('#dataFormMasterEND_TIME').focus(); break;
                                }
                            }
                            $('#dataFormMasterOVERTIME_HOURS').numberbox('setValue', rows[0].hours);
                            $('#dataFormMasterTOTAL_HOURS').val(rows[0].totalHours);
                        }
                    }
                });
            }
            else {
                $('#dataFormMasterOVERTIME_HOURS').numberbox('setValue', "");
                $('#dataFormMasterTOTAL_HOURS').val("");
            }
        }

        //加班日期 OnBlur 設定有效日期 && 計薪年月 && 加班班別 && 刷卡轉出勤上下班時間
        function getEffectDate() {
            //有效日期
            var overtimeDate = $('#dataFormMasterOVERTIME_DATE').datebox('getValue');
            var employeeID = $('#dataFormMasterEMPLOYEE_ID').val();
            var overtimeDateValidate = $.fn.datebox('parseDate', overtimeDate.replace(/\//g, '-'));

            if (employeeID != "") {
                if (overtimeDateValidate != "Invalid Date") {
                    if (overtimeDate) {
                        $('#dataFormMasterOVERTIME_EFFECT_DATE').datebox('setValue', overtimeDate.substr(0, 5) + "12/31")
                    }

                    //計薪年月
                    //if (getEditMode($("#dataFormMaster")) == 'inserted') {
                    //var overtimeDate = $('#dataFormMasterOVERTIME_DATE').datebox('getValue');
                    var dateLockCnt;
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_Overtime.HRM_ATTEND_OVERTIME_DATA', //連接的Server端，command
                        data: "mode=method&method=" + "getSalaryYYMM" + "&parameters=" + employeeID + "," + overtimeDate, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            var rows = $.parseJSON(data);
                            if (rows.length > 0) {
                                $('#dataFormMasterSALARY_YYMM').val(rows[0].SALARY_YYMM);
                            }
                        }
                    });
                    //}
                }
                //加班班別 && 刷卡轉出勤上下班時間
                //if (getEditMode($("#dataFormMaster")) == 'inserted') {
                //var overtimeDate = $('#dataFormMasterOVERTIME_DATE').datebox('getValue');
                var dateLockCnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_Overtime.HRM_ATTEND_OVERTIME_DATA', //連接的Server端，command
                    data: "mode=method&method=" + "getAttendCard" + "&parameters=" + employeeID + "," + overtimeDate, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        var rows = $.parseJSON(data);
                        if (rows.length > 0) {
                            $('#dataFormMasterROTE_ID').val(rows[0].ROTE_ID);
                            //判斷出勤當日是否為假日班("00")
                            if (rows[0].ROTE_CODE == "00") {
                                $('#dataFormMasterOVERTIME_ROTE_ID').val(rows[0].UPPER_ROTE_ID);
                                $('#dataFormMasterROTE_CODE').refval("setValue", rows[0].UPPER_ROTE_CODE);
                            }
                            else {
                                $('#dataFormMasterOVERTIME_ROTE_ID').val(rows[0].ROTE_ID);
                                $('#dataFormMasterROTE_CODE').refval("setValue", rows[0].ROTE_CODE);
                            }
                            $('#dataFormMasterON_TIME_TRAN').val(rows[0].ON_TIME_TRAN);
                            $('#dataFormMasterOFF_TIME_TRAN').val(rows[0].OFF_TIME_TRAN);
                        }
                    }
                });
            }
        }

        function gridReload() {
            $("#dataGridMaster").datagrid('reload');
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
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="_HRM_Attend_Normal_Overtime.HRM_ATTEND_OVERTIME_DATA" runat="server" AutoApply="True"
                DataMember="HRM_ATTEND_OVERTIME_DATA" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="加班單" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDelete="checkLockData">
                <Columns>
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="異動資料紀錄" Editor="text" FieldName="TRANSLOG" FormatScript="HyperlinkLog" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="True" Width="80" />--%>
                    <JQTools:JQGridColumn Alignment="right" Caption="加班資料流水碼" Editor="numberbox" FieldName="OVERTIME_ID" Format="" Width="80" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工流水碼" Editor="text" FieldName="EMPLOYEE_ID" Format="" MaxLength="50" Width="80" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EMPLOYEE_CODE" Format="" MaxLength="0" Width="80" Frozen="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" Format="" MaxLength="0" Width="80" Frozen="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班日期" Editor="datebox" FieldName="OVERTIME_DATE" Format="" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="刷卡上班時間" Editor="text" FieldName="ON_TIME_TRAN" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="刷卡下班時間" Editor="text" FieldName="OFF_TIME_TRAN" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班起始時間" Editor="text" FieldName="BEGIN_TIME" Format="" MaxLength="50" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班起始時間(DATETIME)" Editor="datebox" FieldName="OVERTIME_DATE_TIME_BEGIN" Format="" Width="80" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班截止時間" Editor="text" FieldName="END_TIME" Format="" MaxLength="50" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班截止時間(DATETIME)" Editor="datebox" FieldName="OVERTIME_DATE_TIME_END" Format="" Width="80" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="加班時數" Editor="numberbox" FieldName="OVERTIME_HOURS" Format="" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="補休時數" Editor="numberbox" FieldName="REST_HOURS" Format="" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="加班原因流水號" Editor="numberbox" FieldName="OVERTIME_CAUSE_ID" Format="" Width="120" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班原因代碼" Editor="text" FieldName="OVERTIME_CAUSE_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班原因" Editor="text" FieldName="OVERTIME_CAUSE_CNAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="加班班別代碼" Editor="numberbox" FieldName="OVERTIME_ROTE_ID" Format="" Width="120" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班班別代碼" Editor="text" FieldName="ROTE_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班班別" Editor="text" FieldName="ROTE_CNAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="加班部門代碼" Editor="numberbox" FieldName="OVERTIME_DEPT_ID" Format="" Width="120" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班部門代碼" Editor="text" FieldName="DEPT_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班部門名稱" Editor="text" FieldName="DEPT_CNAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班有效日期" Editor="datebox" FieldName="OVERTIME_EFFECT_DATE" Format="" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班單單號" Editor="text" FieldName="OVERTIME_NO" Format="" MaxLength="50" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="加班比率流水號" Editor="numberbox" FieldName="OVERTIME_RATE_ID" Format="" Width="120" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="計薪年月" Editor="text" FieldName="SALARY_YYMM" Format="" MaxLength="50" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="是否匯入" Editor="checkbox" EditorOptions="on:'Y',off:'N'" FieldName="IS_IMPORT" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="MEMO" Format="" MaxLength="250" Width="120" Sortable="True" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="簽核狀態" Editor="text" FieldName="FLOWFLAG" Format="" MaxLength="1" Width="120" ReadOnly="True" Sortable="True" />--%>
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
                    <JQTools:JQToolItem Enabled="True" Icon="icon-importExcel" ItemType="easyui-linkbutton" OnClick="openImportExcel" Text="匯入EXCEL" Visible="True" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="加班單" Width="700px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HRM_ATTEND_OVERTIME_DATA" HorizontalColumnsCount="2" RemoteName="_HRM_Attend_Normal_Overtime.HRM_ATTEND_OVERTIME_DATA" Closed="False" ContinueAdd="True" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" OnApply="checkOvertimeData" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="gridReload" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" IsAutoPause="False" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="加班資料流水碼" Editor="numberbox" FieldName="OVERTIME_ID" Format="" Width="180" Visible="True" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="員工流水碼" Editor="text" FieldName="EMPLOYEE_ID" Format="" MaxLength="50" Width="180" Visible="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="員工編號" Editor="inforefval" FieldName="EMPLOYEE_CODE" Format="" MaxLength="0" Width="180" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_HRM_Attend_Share.dtHRM_BaseAndBasetts_Employed',tableName:'dtHRM_BaseAndBasetts_Employed',columns:[{field:'EMPLOYEE_CODE',title:'員工工號',width:80,align:'left',table:'',queryCondition:''},{field:'NAME_C',title:'員工姓名',width:80,align:'left',table:'',queryCondition:''}],columnMatches:[{field:'EMPLOYEE_ID',value:'EMPLOYEE_ID'},{field:'OVERTIME_DEPT_ID',value:'DEPT'},{field:'DEPT_CODE',value:'DEPT_CODE'}],whereItems:[],valueField:'EMPLOYEE_CODE',textField:'NAME_C',valueFieldCaption:'EMPLOYEE_CODE',textFieldCaption:'NAME_C',cacheRelationText:true,checkData:true,showValueAndText:false,onSelect:getEffectDate,selectOnly:false" OnBlur="getEffectDate" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班日期" Editor="datebox" FieldName="OVERTIME_DATE" Format="" Width="180" OnBlur="getEffectDate" Span="2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="刷卡上班時間" Editor="text" FieldName="ON_TIME_TRAN" ReadOnly="True" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="刷卡下班時間" Editor="text" FieldName="OFF_TIME_TRAN" MaxLength="0" ReadOnly="True" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班起始時間" Editor="text" FieldName="BEGIN_TIME" Format="" MaxLength="50" Width="180" OnBlur="checkOvertimeHours" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班起始時間" Editor="datebox" FieldName="OVERTIME_DATE_TIME_BEGIN" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班截止時間" Editor="text" FieldName="END_TIME" Format="" MaxLength="50" Width="180" OnBlur="checkOvertimeHours" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班截止時間" Editor="datebox" FieldName="OVERTIME_DATE_TIME_END" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班時數" Editor="numberbox" FieldName="OVERTIME_HOURS" Format="N1" Width="180" EditorOptions="precision:1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="補休時數" Editor="numberbox" FieldName="REST_HOURS" Format="" Width="180" Visible="True" EditorOptions="precision:1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班班別代碼" Editor="numberbox" FieldName="OVERTIME_ROTE_ID" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班班別" Editor="inforefval" FieldName="ROTE_CODE" Format="" MaxLength="0" Width="180" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_HRM_Attend_Share.HRM_ATTEND_ROTE',tableName:'HRM_ATTEND_ROTE',columns:[{field:'ROTE_CODE',title:'班別代碼',width:80,align:'left',table:'',queryCondition:''},{field:'ROTE_CNAME',title:'班別中文名稱',width:80,align:'left',table:'',queryCondition:''}],columnMatches:[{field:'OVERTIME_ROTE_ID',value:'ROTE_ID'}],whereItems:[],valueField:'ROTE_CODE',textField:'ROTE_CNAME',valueFieldCaption:'班別代碼',textFieldCaption:'班別中文名稱',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班原因流水號" Editor="numberbox" FieldName="OVERTIME_CAUSE_ID" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班原因" Editor="inforefval" FieldName="OVERTIME_CAUSE_CODE" Format="" MaxLength="0" Width="180" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_HRM_Attend_Share.HRM_ATTEND_OVERTIME_CAUSE',tableName:'HRM_ATTEND_OVERTIME_CAUSE',columns:[{field:'OVERTIME_CAUSE_CODE',title:'加班原因代碼',width:80,align:'left',table:'',queryCondition:''},{field:'OVERTIME_CAUSE_CNAME',title:'加班原因中文',width:80,align:'left',table:'',queryCondition:''}],columnMatches:[{field:'OVERTIME_CAUSE_ID',value:'OVERTIME_CAUSE_ID'}],whereItems:[],valueField:'OVERTIME_CAUSE_CODE',textField:'OVERTIME_CAUSE_CNAME',valueFieldCaption:'加班原因代碼',textFieldCaption:'加班原因中文',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班部門代碼" Editor="numberbox" FieldName="OVERTIME_DEPT_ID" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班部門" Editor="inforefval" FieldName="DEPT_CODE" Format="" MaxLength="0" Width="180" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_HRM_Employee_Share.HRM_DEPTC',tableName:'HRM_DEPTC',columns:[{field:'DEPTC_CODE',title:'成本部門代碼',width:80,align:'left',table:'',queryCondition:''},{field:'DEPTC_CNAME',title:'成本部門中文名稱',width:80,align:'left',table:'',queryCondition:''},{field:'DEPTC_ENAME',title:'成本部門英文名稱',width:80,align:'left',table:'',queryCondition:''}],columnMatches:[{field:'OVERTIME_DEPT_ID',value:'DEPTC_ID'}],whereItems:[],valueField:'DEPTC_CODE',textField:'DEPTC_CNAME',valueFieldCaption:'成本部門代碼',textFieldCaption:'成本部門中文名稱',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班有效日期" Editor="datebox" FieldName="OVERTIME_EFFECT_DATE" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班單單號" Editor="text" FieldName="OVERTIME_NO" Format="" MaxLength="50" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="計薪年月" Editor="text" FieldName="SALARY_YYMM" Format="" MaxLength="50" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="是否匯入" Editor="checkbox" EditorOptions="on:'Y',off:'N'" FieldName="IS_IMPORT" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="25" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="MEMO" Format="" MaxLength="250" Width="480" Span="2" />
                        <%--<JQTools:JQFormColumn Alignment="left" Caption="FLOWFLAG" Editor="text" FieldName="FLOWFLAG" Format="" MaxLength="1" Width="180" Visible="False" />--%>
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Format="" MaxLength="50" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="" Width="180" ReadOnly="True" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Format="" MaxLength="50" Width="180" ReadOnly="True" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="" Width="180" ReadOnly="True" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Visible="True" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="OVERTIME_ID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="OVERTIME_DATE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="" DefaultValue="getEffectDate" FieldName="OVERTIME_EFFECT_DATE" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="OVERTIME_HOURS" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="REST_HOURS" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="TOTAL_HOURS" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="N" FieldName="SYSTEM_HOLIDAY" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="N" FieldName="SYSTEM_CREATE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="N" FieldName="NOT_ALLOW_MODIFY" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="N" FieldName="IS_IMPORT" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EMPLOYEE_CODE" RemoteMethod="True" ValidateMessage="請輸入員工工號" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="OVERTIME_DATE" RemoteMethod="True" ValidateMessage="請輸入加班日期" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="BEGIN_TIME" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="END_TIME" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ROTE_CODE" RemoteMethod="True" ValidateMessage="請選擇加班班別" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="checkYearMonthFormat" CheckNull="True" FieldName="SALARY_YYMM" RemoteMethod="False" ValidateMessage="請輸入正確計薪年月格式如 : 201401 或 201412" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="OVERTIME_EFFECT_DATE" RemoteMethod="True" ValidateMessage="請輸入有效日期" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DEPT_CODE" RemoteMethod="True" ValidateMessage="請輸入加班部門" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
        <!-- 匯入對話框內容的 DIV -->
        <div id="Dialog_Import"></div>
        
        <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog">
            <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" RemoteName=" " DataMember=" " HorizontalColumnsCount="3" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                <Columns>                    
                    <JQTools:JQFormColumn Alignment="left" Caption="檔案名稱" Editor="text" FieldName="FilePathName" Width="80" ReadOnly="true" Visible="false" />
                    <JQTools:JQFormColumn Alignment="left" Caption="員工編號" Editor="infocombobox" FieldName="EMPLOYEE_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="加班日期" Editor="infocombobox" FieldName="OVERTIME_DATE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="加班起始時間" Editor="infocombobox" FieldName="BEGIN_TIME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="加班截止時間" Editor="infocombobox" FieldName="END_TIME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="加班時數" Editor="infocombobox" FieldName="OVERTIME_HOURS" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="補休時數" Editor="infocombobox" FieldName="REST_HOURS" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="加班總時數" Editor="infocombobox" FieldName="TOTAL_HOURS" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="加班原因代碼" Editor="infocombobox" FieldName="OVERTIME_CAUSE_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" Visible="False" />
                    <JQTools:JQFormColumn Alignment="left" Caption="加班班別代碼" Editor="infocombobox" FieldName="OVERTIME_ROTE_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" Visible="False" />
                    <JQTools:JQFormColumn Alignment="left" Caption="加班部門代碼" Editor="infocombobox" FieldName="OVERTIME_DEPT_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="加班有效日期" Editor="infocombobox" FieldName="OVERTIME_EFFECT_DATE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="計薪年月" Editor="infocombobox" FieldName="SALARY_YYMM" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="加班單單號" Editor="infocombobox" FieldName="OVERTIME_NO" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="infocombobox" FieldName="MEMO" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EMPLOYEE_ID" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="OVERTIME_DATE" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="BEGIN_TIME" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="END_TIME" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="OVERTIME_HOURS" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="REST_HOURS" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="TOTAL_HOURS" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="OVERTIME_EFFECT_DATE" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="SALARY_YYMM" RemoteMethod="True" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>
        </JQTools:JQDialog>
    </form>
</body>
</html>
