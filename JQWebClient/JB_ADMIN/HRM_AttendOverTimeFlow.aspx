<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_AttendOverTimeFlow.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jquery.jbjob.js"></script>
    <title></title>
     <script>
         $(document).ready(function () {
             $("input, select, textarea").focus(function () {
                 $(this).css("background-color", "#FFFFB5");
             });

             $("input, select, textarea").blur(function () {
                 $(this).css("background-color", "white");
             });
             //明細Hours加總至Grid欄位 => 計算合計時數
             $("#dataGridDetail").datagrid({
                 onAfterEdit: function (rowIndex, rowData, changes) {
                     rowData.TotalHours = parseFloat(rowData.OverTimeHours) + parseFloat(rowData.RestHours);
                     $(this).datagrid('refreshRow', rowIndex);
                 }
             });
             //就醫服務紀錄必填欄位
             $('#dataFormHospitalEmployer').closest('td').prev('td').css({ 'color': 'red' });
             $('#dataFormHospitalForeignLaborer').closest('td').prev('td').css({ 'color': 'red' });

             //加班時數後面加備註
             var DinnerHours = $('#dataFormDetailDinnerHours').closest('td');
             DinnerHours.append(' (中午休息時間系統會扣除，不需填寫)');
             
         });
         
         function getEmployeeID() {
             //取得HRM_BASE_BASE => EMPLOYEE_ID
             var EmployeeID = getClientInfo("UserID");
             var EMPLOYEE_ID;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster', //連接的Server端，command
                 data: "mode=method&method=" + "getEmployeeID" + "&parameters=" + EmployeeID ,
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
             var EmployeeID = $("#dataFormMasterEmployeeID").refval('getValue');
             var CreateDate = $('#dataFormMasterCreateDate').datebox('getValue');
             if (EmployeeID != "") {
                 //取得申請時的部門名稱,班別
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster', //連接的Server端，command
                     data: "mode=method&method=" + "getDeptInfo" + "&parameters=" + EmployeeID + "," + CreateDate,
                     cache: false,
                     async: false,
                     success: function (data) {
                         var rows = $.parseJSON(data);
                         if (rows.length > 0) {
                             $('#dataFormMasterOverTimeDeptID').val(rows[0].DEPT_ID);
                             $('#dataFormMasterDEPT_Name').val(rows[0].DEPT_CNAME);
                             $('#dataFormMasterOverTimeRoteID').val(rows[0].ROTE_ID);
                             //$('#dataFormMasterROTE_Name').val(rows[0].ROTE_CNAME);
                         }
                     }
                 });

                 JQDataGrid1LoadData();//載入本月及上月加班總時數
                 setTimeout(function () {
                     ShowdataGridHospital();//是否顯示就醫紀錄
                 }, 3000);
             }             
         }
         //check 時間格式如 : 0800 或 0830
         function checkTimeFormat(val) {
             if ($('#dataFormMasterOverTimeRoteID').val() == "17") {//0750上班
                 return true;
             } else return $.jbIsTimeFormat(val);
         }
         //function checkTimeDate(val) { return true; }
         //check 加班日期=>卡7天
         function checkTimeDate(val) {
             //var dt = new Date();
             //var aDate = new Date($.jbDateAdd('days', -7, dt));//小一個月
             //var bDate = $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
             //if (val <= bDate) return false;
             //else return true;

             //加班日期不可小於? //加班日期不可小於?日前申請=>看 sSYS_Variable設定之天數 OverTimeDays
             var AbsentDays;
             var AttendSetDaysrows;
             var result;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sSYS_Variable.HRMAttendSetDays', //連接的Server端，command
                 data: "mode=method&method=" + "GetHRM_AttendSetDays",
                 cache: false,
                 async: false,
                 success: function (data) {
                     result = $.parseJSON(data);
                 }
             });

             if (result.IsOK == undefined) {
                 AttendSetDaysrows = result;
                 OverTimeDays = AttendSetDaysrows[0].OverTimeDays;
                 var dt = new Date();
                 var aDate = new Date($.jbDateAdd('days', -OverTimeDays, dt));
                 var bDate = $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
                 if (val < bDate) return false;
                 else return true;
             } else {
                 return false;
             }
             return true;
         }
         function OnLoadFormDetail() {

             ////清空加班原因
             //$("#dataFormDetailOverTimeCauseID").combobox('setValue', "");
             //$("#dataFormDetailApplyOvertimeType").combobox('setValue', "xxxx");
             
             if (getEditMode($("#dataFormDetail")) == 'inserted') {
                 //申請類型上紅色
                 $("#dataFormDetailApplyOvertimeType").closest('td').prev('td').css({ 'color': 'red' });

                 var EmployeeID = $("#dataFormMasterEmployeeID").refval('getValue');
                 var CreateDate = $('#dataFormMasterCreateDate').datebox('getValue');
                 if (EmployeeID != "") {
                     //取得加班預設結束時間
                     $.ajax({
                         type: "POST",
                         url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster', //連接的Server端，command
                         data: "mode=method&method=" + "getOFF_TIME" + "&parameters=" + EmployeeID + "," + CreateDate,
                         cache: false,
                         async: false,
                         success: function (data) {
                             var rows = $.parseJSON(data);
                             if (rows.length > 0) {
                                 $('#dataFormDetailBeginTime').val(rows[0].OFF_TIME);
                             }
                         }
                     });
                 }
             }
         }
       

         function dataFormMasterOnApply() {
             var data = $("#dataGridDetail").datagrid('getData');
             if (data.total == 0) {             
                 alert('注意!!,未新增加班紀錄,無法存檔!');
                 return false;
             }
             //明細Hours加總至Grid欄位
             endEdit($('#dataGridDetail'));


             //檢查判斷formview資料是否重複
             var datagrid = $('#dataGridDetail');
             var rows = datagrid.datagrid('getRows');
             var aOverTimeDate = [];//日期
             var aDateTimeBegin = [];//日期起始時間
             var aDateTimeEnd = [];//日期終止時間
             
             for (var i = 0; i < rows.length; i++) {
                 var dt = new Date(rows[i].OverTimeDate);
                 var sDate = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd');
                 var d1 = sDate + " " + rows[i].BeginTime.substr(0, 2) + ":" + rows[i].BeginTime.substr(2, 2);
                 var d2 = sDate + " " + rows[i].EndTime.substr(0, 2) + ":" + rows[i].EndTime.substr(2, 2)
                 aDateTimeBegin.push(d1);
                 aDateTimeEnd.push(d2);
             }
             for (var i = 0; i < aDateTimeBegin.length; i++) {
                 var nary2 = aDateTimeBegin.sort();
                 var nary3 = aDateTimeEnd.sort();                   
                    if (nary2[i] < nary3[i + 1] && nary3[i] > nary2[i + 1]) {//指時間重複
                        alert('加班資料重複');
                            return false;
                    }                    
             }

             
         }
         //DataGridDetail 合計時數加總
         //欄值,row,index
         function ScriptHoursTotal(a, r, x) {
             if ( r.TotalHours != undefined) {//表示最後一筆加總的row
                 return r.TotalHours;
             }else{
                 var RestHours = r.RestHours;
                 var OverTimeHours = r.OverTimeHours;
                 if (RestHours != undefined && OverTimeHours != undefined && RestHours != "" && OverTimeHours != "") {
                     value = parseFloat(RestHours) + parseFloat(OverTimeHours);
                     return value;
                 }
             }
         }
         //加總至Master欄位。DataGridDetail Amount欄位Total屬性設定sum、OnTotal屬性定義此方法
         function OnTotalHours() {
             //Grid最後一行加總時數
             //新增模式使用執行             
             if (getEditMode($("#dataFormMaster")) == "inserted" || getEditMode($("#dataFormDetail")) != "viewed") {
                 var datagrid = $('#dataGridDetail');
                 var rows = datagrid.datagrid('getRows');
                 var result = 0;
                 for (var i = 0; i < rows.length ; i++) {
                     var RestHours = rows[i].RestHours;
                     var TotalHours = rows[i].TotalHours;
                     value = parseFloat(TotalHours);
                         if (!isNaN(value)) {
                             result = eval(result + value);

                         }
                 }
                 $('#dataFormMasterMasterTotalHours').numberbox('setValue', result);
                 return result;
             } 
       
         }
         //計算加班時數 預設 => 終止時間-起始時間(起始時間、終止時間OnBlur)
         function OnBlurOverTimeHours() {
             CalOvertimeHoursAndMealTypeAndMealNTD();
         }

         //左填補字串
         function padLeft(str, lenght) {
             if (str > 10)
                 return str;
             else
                 return "0" + str;
         }
         //右填補字串
         function padRight(str, lenght) {
             if (str>10)
                 return str;
             else
                 return str + "0";
         }
         function CalOvertimeHoursAndMealTypeAndMealNTD() {
             var BeginTime = $("#dataFormDetailBeginTime").val();
             
             var EndTime = $("#dataFormDetailEndTime").val();
             var ApplyOvertimeType = $("#dataFormDetailApplyOvertimeType").combobox('getValue');
             var dt = new Date();
             var sDate = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd');

             var d1 = "";
             var d2 = "";
             var s =parseInt(BeginTime.substr(0, 2));
             var e =parseInt(EndTime.substr(0, 2));
             if (s > 24) {//超過24小時
                 d1 =(s - 24).toString();

                 if(s - 24 < 10){//左邊補0
                     d1 = '0' + d1;
                 } 

                 if (e > 24) {//超過24小時
                     d2 = (e - 24).toString();
                     if (e - 24 < 10) {
                         d2 = '0' + d2;
                     }
                 }
                 d1 = sDate + " " + d1 + ":" + BeginTime.substr(2, 2);
                 d2 = sDate + " " + d2 + ":" + EndTime.substr(2, 2);

             } else {
                 d1 = sDate + " " + BeginTime.substr(0, 2) + ":" + BeginTime.substr(2, 2);
                 d2 = sDate + " " + EndTime.substr(0, 2) + ":" + EndTime.substr(2, 2);
             }

             if (BeginTime != '' && EndTime != '') {
                 //計算加班時數
                 if (ApplyOvertimeType != '' && ApplyOvertimeType != '3') {

                     var OverTimeHours = $.jbDateDiff("minutes", d1, d2) / 60.0;
                     var DiffTimeHours = OverTimeHours - parseFloat($('#dataFormDetailDinnerHours').val());//扣掉休息時間=>用.numberbox 取值來扣會有問題
                     $("#dataFormDetailTotalHours").numberbox('setValue', DiffTimeHours);
                     $("#dataFormDetailTotalHours").val(DiffTimeHours);
                     if (ApplyOvertimeType == '1' || ApplyOvertimeType == '2') {//1報加班費與報餐費 , 2報加班費
                         $("#dataFormDetailOverTimeHours").numberbox('setValue', DiffTimeHours);
                         $("#dataFormDetailOverTimeHours").val(DiffTimeHours);
                         $("#dataFormDetailRestHours").numberbox('setValue', '0.0');
                         $("#dataFormDetailRestHours").val('0.0');

                     } else {
                         $("#dataFormDetailRestHours").numberbox('setValue', DiffTimeHours);
                         $("#dataFormDetailRestHours").val(DiffTimeHours);
                         $("#dataFormDetailOverTimeHours").numberbox('setValue', '0.0');
                         $("#dataFormDetailOverTimeHours").val('0.0');
                     }

                     //推得扣除休息時間的時間格式 => 放在暫存 EndTimeTemp 欄位
                     if (parseFloat($('#dataFormDetailDinnerHours').val()) == "0") {
                         $("#dataFormDetailEndTimeTemp").val(EndTime);//休息時間=0 ,暫存時間為終止時間
                     } else {
                         var aDate = new Date($.jbDateAdd('minutes', DiffTimeHours * 60.0, d1));
                         $("#dataFormDetailEndTimeTemp").val(padLeft(aDate.getHours(), 2) + "" + padRight(aDate.getMinutes(),2));
                     }

                 } else if (ApplyOvertimeType == '3') {//只報餐費
                     $("#dataFormDetailTotalHours").numberbox('setValue', '0.0');
                     $("#dataFormDetailDinnerHours").numberbox('setValue', '0.0');
                     $("#dataFormDetailOverTimeHours").numberbox('setValue', '0.0');
                     $("#dataFormDetailRestHours").numberbox('setValue', '0.0');

                 }
                 //計算餐別、餐費總金額
                 if (ApplyOvertimeType == '1' || ApplyOvertimeType == '3' || ApplyOvertimeType == '4') {
                     var MealTotalNTD = 0;
                     var MealType = [];
                     //var grid = $("#dataFormDetailCombo").combogrid('grid');
                     //var rows = grid.datagrid('getRows');

                     $.ajax({
                         type: "POST",
                         url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster',
                         data: "mode=method&method=SelectHRMAttendOverTimeMeal&parameters=",
                         cache: false,
                         async: false,
                         success: function (data) {
                             var rows = $.parseJSON(data);
                             if (rows.length > 0) {
                                 for (var i = 0; i < rows.length; i++) {
                                     //var l1 = sDate + " " + rows[i].Time0.substr(0, 2) + ":" + rows[i].Time0.substr(2, 2);
                                     var l2 = sDate + " " + rows[i].Time1.substr(0, 2) + ":" + rows[i].Time1.substr(2, 2);
                                     //if ((d1 < l1 || d1 == l1) && d1 < l2 && (l2 < d2 || l2 == d2)) {
                                     if ((l2 < d2 || l2 == d2) && (d1 < l2 || d1 == l2)) {
                                         MealTotalNTD = MealTotalNTD + rows[i].NTD;
                                         MealType.push(rows[i].MealType);
                                     }
                                 }
                                 var sMealType = MealType.join(",");
                                 $("#dataFormDetailMealTotalNTD").val(MealTotalNTD);
                                 $("#dataFormDetailMealType").val(sMealType);
                             }
                         }
                     });
                     //var n1 = sDate + " " + '1730'.substr(0, 2) + ":" + '1730'.substr(2, 2);
                     //var n2 = sDate + " " + '1930'.substr(0, 2) + ":" + '1930'.substr(2, 2);
                     //if ((d1 < n1 || d1 == n1) && d1 < n2 && (n2 < d2 || n2 == d2)) {
                     //    MealTotalNTD = MealTotalNTD + 60;
                     //    MealType.push("晚餐");
                     //}

                     
                 } else if (ApplyOvertimeType == '2' || ApplyOvertimeType == '5') {
                     $("#dataFormDetailMealTotalNTD").val('');
                     $("#dataFormDetailMealType").val('');
                 }
             }
         }
         //改變休息時間=>減少時數 時數
         function OnBlurDinnerHours() {
             CalOvertimeHoursAndMealTypeAndMealNTD();
         }

         //dataFormDetail(加班明細)存檔(OnApply)前        
         //1. 判斷加班起始日期不可大於截止日期
         //2. 判斷加班時數        
         //3. 判斷加班資料(EMPLOYEE_ID/OVERTIME_DATE_TIME_BEGIN/OVERTIME_DATE_TIME_END)申請的時段內是否已有存在的加班資料
         //4. 判斷加班資料(在途)
         function checkOvertimeData() {
             //0.檢查申請類型是否有填//allen
             if ($("#dataFormDetailApplyOvertimeType").combobox('getValue') == '') {
                 alert('請選擇"申請類型"');
                 return false;
             }
           
             //1.判斷加班起始時間不可大於截止時間
             var beginTime = $("#dataFormDetailBeginTime").val();
             var endTime = $("#dataFormDetailEndTime").val();
             if (parseInt(beginTime) >= parseInt(endTime)) {
                 alert('加班起始時間 : ' + beginTime + ' 需小於加班截止時間 : ' + endTime);
                 return false;
             }

             if (getEditMode($("#dataFormMaster")) == 'updated')
                 OverTimeNO = $('#dataFormMasterOverTimeNO').val();
             else
                 OverTimeNO = "0";

             var EmployeeID = $("#dataFormMasterEmployeeID").refval('getValue');
             var overtimeDate = $('#dataFormDetailOverTimeDate').datebox('getValue');
             var beginTime = $('#dataFormDetailBeginTime').val();
             var endTime = $('#dataFormDetailEndTime').val();
             var restHours = $('#dataFormDetailRestHours').val();

             //2. 判斷加班時數
             var rows;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster', //連接的Server端，command
                 data: "mode=method&method=" + "checkOvertimeHours" + "&parameters=" + OverTimeNO + "," + EmployeeID + "," + overtimeDate + "," + beginTime + "," + endTime,
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
                         case "4": alert("申請日期查無出勤資料"); break;
                         case "5": alert("加班起始時間未在合理時間範圍內"); $('#dataFormDetailBeginTime').focus(); break;
                         case "6": alert("加班截止時間未在合理時間範圍內"); $('#dataFormDetailEndTime').focus(); break;
                     }
                     return false;
                 }
                 else {
                     ////把取得的中午休息時間set 回欄位
                     if ($('#dataFormDetailDinnerHours').numberbox('getValue') == "0.0") {
                         $('#dataFormDetailDinnerHours').numberbox('setValue', rows[0].restHours);
                         $('#dataFormDetailDinnerHours').val(rows[0].restHours);
                         ////減掉休息時間的時數
                         $('#dataFormDetailTotalHours').numberbox('setValue', rows[0].hours);
                         $('#dataFormDetailTotalHours').val(rows[0].hours);
                         CalOvertimeHoursAndMealTypeAndMealNTD();
                     }

                     if (rows[0].hours == 0) {
                         alert("申請的時段為上班時間");
                         $('#dataFormDetailTotalHours').numberbox('setValue', rows[0].hours);
                         return false;
                     }
                     if (parseFloat($('#dataFormDetailTotalHours').numberbox('getValue')) <= 0)
                     {
                         alert("加班時數不正確(加班時數需>0小時)");
                         $('#dataFormDetailTotalHours').focus();
                         return false;
                     }

                     if (rows[0].hours < parseFloat($('#dataFormDetailOverTimeHours').numberbox('getValue')) + parseFloat($('#dataFormDetailRestHours').numberbox('getValue'))) {
                         alert("加班總時數不正確(加班時數需<=" + rows[0].hours + " 小時)");
                         $('#dataFormDetailOverTimeHours').focus();
                         //$('#dataFormMasterOVERTIME_HOURS').numberbox('setValue', rows[0].hours);
                         return false;
                     }

                 }
             }
             //3. 判斷加班資料(EMPLOYEE_ID/OVERTIME_DATE_TIME_BEGIN/OVERTIME_DATE_TIME_END)申請的時段內是否已有存在的加班資料
                 var cnt;
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster', //連接的Server端，command
                     data: "mode=method&method=" + "checkOvertimeData" + "&parameters=" + OverTimeNO + "," + EmployeeID + "," + overtimeDate + "," + beginTime + "," + endTime, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                     cache: false,
                     async: false,
                     success: function (data) {
                         if (data != false) {
                             cnt = $.parseJSON(data);
                         }
                     }
                 });
                 if (cnt != "0" && cnt != "undefined") {
                     alert("申請的時段內已有存在加班資料！");
                     return false;
                 }
                 //4. 判斷加班資料(在途)
                 var inow;
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster', //連接的Server端，command
                     data: "mode=method&method=" + "checkOnData" + "&parameters=" + OverTimeNO + "," + EmployeeID + "," + overtimeDate + "," + beginTime + "," + endTime, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                     cache: false,
                     async: false,
                     success: function (data) {
                         if (data != false) {
                             inow = $.parseJSON(data);
                         }
                     }
                 });
                 if (inow != "0" && inow != "undefined") {
                     alert("申請的時段內已有在途加班資料！");
                     return false;
                 }
             
             //4. 加班時數及補休時數只能擇一申請
             //var overtimeHours = $("#dataFormDetailOverTimeHours").numberbox('getValue');
             //var restHours = $("#dataFormDetailRestHours").numberbox('getValue');
             //if (parseInt(overtimeHours) > 0 && parseInt(restHours) > 0) {
             //    alert("加班時數及補休時數只能擇一申請");
             //    return false;
                 //}       
         }

         
         function OnApplydataFormHospital() {
             if ($('#dataFormHospitalEmployer').combobox('getValue') == "") {
                 alert("注意!請選「雇主」!!"); return false;
             }
             if ($('#dataFormHospitalForeignLaborer').combobox('getValue') == "") {
                 alert("注意!請選取「外勞姓名」!!"); return false;
             }
             //return true;
         }
         //取得某外勞的就醫次數與欠款金額
         function OnSelectForeignLaborer(row) {
             var EmployeeID = $('#dataFormHospitalForeignLaborer').combobox('getValue');
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster',
                 data: "mode=method&method=GetForeignLaborCountsDebt&parameters=" + EmployeeID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     var rows = $.parseJSON(data);
                     $('#dataFormHospitalcounts1').val(rows[0].counts1);
                     $('#dataFormHospitaldebt').val(rows[0].debt);
                 }
             });
         }
         //本月與上月加班總時數
         function JQDataGrid1LoadData() {
             var EmployeeID = $("#dataFormMasterEmployeeID").refval('getValue');//此ID是JBHR_EEP的HRM_BASA_BASE人事系統的工號
             var dt = new Date();
             var dt1 = new Date();
             dt1.setMonth(dt1.getMonth(), -1, 1);
             var aDate = $.jbjob.Date.DateFormat(dt, 'yyyy-MM');
             var bDate = $.jbjob.Date.DateFormat(dt1, 'yyyy-MM');

             if (EmployeeID != "" && aDate != "" && bDate != "") {
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster',
                     data: "mode=method&method=SumOvertime&parameters=" + EmployeeID + "," + aDate + "," + bDate,
                     cache: false,
                     async: false,
                     success: function (data) {
                         var rows = $.parseJSON(data);
                         if (rows.length > 0) {
                             if (rows.length == 2) {
                                 $('#dataFormMasterLMOvertimeHrs').val(rows[0].yymmss);
                                 $('#dataFormMasterTMOvertimeHrs').val(rows[1].yymmss);
                             } else if (rows[0].OverTimeYYMM == bDate) {
                                 $('#dataFormMasterLMOvertimeHrs').val(rows[0].yymmss);
                             } else if (rows[0].OverTimeYYMM == aDate) {
                                 $('#dataFormMasterTMOvertimeHrs').val(rows[0].yymmss);
                             }
                         }
                     }
                 });
             }
         }
         //申請時，外勞部的使用者能看見就醫服務紀錄dataGrid
         function ShowdataGridHospital() {
             var param = Request.getQueryStringByName("p1");
             if (param == '') { param = Request.getQueryStringByName2("p1"); }
             if(param == 'apply'){
                 var userid = getClientInfo("userid");
                 var counts;
                 if (userid != "") {
                     $.ajax({
                         type: "POST",
                         url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster',
                         data: "mode=method&method=IsForeignDept&parameters=" + userid,
                         cache: false,
                         async: false,
                         success: function (data) {
                             counts = $.parseJSON(data);
                             if (counts > 0) {
                                 $('#DivJQDataGrid2').show();
                             } else {
                                 $('#DivJQDataGrid2').hide();
                             }
                         }
                     });
                 }
             } else {
                 var rows = $('#dataGridHospital').datagrid('getRows');
                 if (rows.length > 0) {
                     $('#DivJQDataGrid2').show();
                 } else {
                     $('#DivJQDataGrid2').hide();
                 }
             }
         }

         function OnLoadSuccessdataFormHospital() {
             var mode = getEditMode($('#dataFormHospital'));
             if (mode == 'inserted') {
                 var EmployeeID = $("#dataFormMasterEmployeeID").refval('getValue');
                 $("#dataFormHospitalCreateBy").val(EmployeeID);
                 $("#dataFormHospitalcounts").hide();//隱藏Table存入欄位
                 $("#dataFormHospitalcounts").closest('td').prev('td').text('');
                 $("#dataFormHospitalForeignLaborer").combobox('disable');
             } else if (mode == 'updated') {
             } else {
                 $("#dataFormHospitalcounts1").hide();//隱藏空欄位
                 $("#dataFormHospitalcounts1").closest('td').prev('td').text('');
                 GetForeignLabors();
             }
         }
         function OnSelectEmployer() {
             //$('#dataFormHospitalForeignLaborer').combobox('setValue', '');
             //var where = "er.EmployerID ='" + $('#dataFormHospitalEmployer').combobox('getValue') + "'";
             $('#dataFormHospitalForeignLaborer').combobox("clear");
             $('#dataFormHospitalForeignLaborer').combobox('enable');
             GetForeignLabors();
         }
         //取得某公司的外勞名單，並塞到combobox
         function GetForeignLabors() {
             var EmployerID = $('#dataFormHospitalEmployer').combobox('getValue');
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster',
                 data: "mode=method&method=GetForeignLabors&parameters=" + EmployerID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     var rows = $.parseJSON(data);
                     //if (rows.length > 0) {
                     var json = "[]";
                     for (var i = 0; i < rows.length; i++) {
                         if (i == 0) { json = "["; }
                         if (i == 0) {
                             json = json + '{"value":"' + rows[i].EmployeeID + '","text":"' + rows[i].EmployeeCEName + '"}';
                         } else { json = json + ',{"value":"' + rows[i].EmployeeID + '","text":"' + rows[i].EmployeeCEName + '"}'; }
                         if (i == rows.length - 1) { json = json + "]"; }
                     }
                     var orows = $.parseJSON(json);
                     $('#dataFormHospitalForeignLaborer').combobox('loadData', orows);
                     //}
                 }
             });
         }

         function FormatScript_DGHospital(value,row,index) {
             if (value != 0)
                 return "<a href='javascript: void(0)' onclick='LinkHospitalCounts(" + index + ");'>" + value + "</a>";
             else return value;
         }
         function LinkHospitalCounts(index) {
             $("#dataGridHospital").datagrid('selectRow', index);
             var row = $("#dataGridHospital").datagrid('getSelected');
             var ForeignLaborer = row.ForeignLaborer;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster',  //連接的Server端，command
                 data: "mode=method&method=" + "HospitalDrillDown" + "&parameters=" + ForeignLaborer,  //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入搜尋字串
                 cache: false,
                 async: true,
                 success: function (data) {
                     var rows = $.parseJSON(data);
                     $("#dataGridHospitalDrillDown").datagrid('loadData', rows);
                 }
             });
             openForm("#JQDialog4", {}, "viewed", "dialog");
         
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sHRMAttendOverTime.HRMAttendOverTimeApplyMaster" runat="server" AutoApply="True"
                DataMember="HRMAttendOverTimeApplyMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="加班申請" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="OverTimeNO" Editor="numberbox" FieldName="OverTimeNO" Format="" Visible="true" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="OverTimeDeptID" Editor="numberbox" FieldName="OverTimeDeptID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="OverTimeRoteID" Editor="numberbox" FieldName="OverTimeRoteID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="TotalHours" Editor="numberbox" FieldName="TotalHours" Format="" Visible="true" Width="120" EditorOptions="precision:1" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" Sortable="True" />
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
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="加班單" DialogLeft="50px" DialogTop="30px" Width="780px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HRMAttendOverTimeApplyMaster" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" OnApply="dataFormMasterOnApply" OnLoadSuccess="OnLoadFormMaster" RemoteName="sHRMAttendOverTime.HRMAttendOverTimeApplyMaster" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="加班單號" Editor="text" FieldName="OverTimeNO" Format="" NewRow="False" ReadOnly="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="上月加班總時數" Editor="text" FieldName="LMOvertimeHrs" NewRow="True" ReadOnly="True" Width="450" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="本月加班總時數" Editor="text" FieldName="TMOvertimeHrs" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="2" Visible="True" Width="450" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請姓名" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sHRMAttendOverTime.infoHRM_BASE_BASE',tableName:'infoHRM_BASE_BASE',columns:[],columnMatches:[],whereItems:[],valueField:'EMPLOYEE_ID',textField:'NAME_C',valueFieldCaption:'EMPLOYEE_ID',textFieldCaption:'NAME_C',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none'" FieldName="EmployeeID" Format="" NewRow="True" OnBlur="" ReadOnly="True" Width="100" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false,editable:true" FieldName="CreateDate" Format="yyyy/mm/dd" ReadOnly="True" Width="95" Visible="True" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="DEPT_Name" NewRow="True" ReadOnly="True" Span="1" Visible="True" Width="150" MaxLength="0" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OverTimeDeptID" Editor="numberbox" FieldName="OverTimeDeptID" MaxLength="0" ReadOnly="True" Visible="False" Width="130" NewRow="True" RowSpan="1" Span="1" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OverTimeRoteID" Editor="numberbox" FieldName="OverTimeRoteID" Format="" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總時數" Editor="numberbox" EditorOptions="precision:1" FieldName="MasterTotalHours" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班備註" Editor="textarea" FieldName="Memo" NewRow="True" ReadOnly="False" Visible="True" Width="450" EditorOptions="height:50" MaxLength="125" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" Format="" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="HRMAttendOverTimeApplyDetails" Pagination="False" RemoteName="sHRMAttendOverTime.HRMAttendOverTimeApplyMaster" Title="加班紀錄" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="時數合計:" UpdateCommandVisible="True" ViewCommandVisible="True" RowNumbers="True" EditDialogID="JQDialog2" ParentObjectID="dataFormMaster" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" Width="120" Visible="False" Format="" />
                        <JQTools:JQGridColumn Alignment="right" Caption="OverTimeNO" Editor="text" FieldName="OverTimeNO" Width="120" Visible="False" Format="" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班日期" Editor="datebox" FieldName="OverTimeDate" Width="60" Format="" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="申請類型" Editor="infocombobox" EditorOptions="valueField:'ApplyNO',textField:'ApplyType',remoteName:'sHRMAttendOverTime.HRMAttendOverTimeMealApplyType',tableName:'HRMAttendOverTimeMealApplyType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyOvertimeType" Visible="True" Width="110" />
                        <JQTools:JQGridColumn Alignment="center" Caption="起始時間" Editor="text" FieldName="BeginTime" Format="" Width="52" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="終止時間" Editor="text" FieldName="EndTime" Format="" Width="52" Visible="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="加班時數" Editor="numberbox" EditorOptions="precision:1" FieldName="OverTimeHours" Format="" Width="52" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="補休時數" Editor="numberbox" EditorOptions="precision:1" FieldName="RestHours" Format="" Visible="False" Width="52" />
                        <JQTools:JQGridColumn Alignment="right" Caption="加班時數" Editor="numberbox" EditorOptions="precision:1" FieldName="TotalHours" FormatScript="" OnTotal="OnTotalHours" PlaceHolder="" Total="sum" Visible="True" Width="52">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="休息時數" Editor="numberbox" EditorOptions="precision:1" FieldName="DinnerHours" Total="sum" Visible="True" Width="52" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班原因" Editor="text" EditorOptions="" FieldName="OverTimeCauseID" Format="" Width="143" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="OverTimeDateTimeBegin" Editor="datebox" FieldName="OverTimeDateTimeBegin" Format="" Visible="False" Width="120" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="OverTimeDateTimeEnd" Editor="datebox" FieldName="OverTimeDateTimeEnd" Format="" Visible="False" Width="120" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="120" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="餐別" Editor="text" FieldName="MealType" Width="40" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="餐費" Editor="text" FieldName="MealTotalNTD" Visible="True" Width="30" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="EndTimeTemp" Editor="text" FieldName="EndTimeTemp" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="OverTimeDateTimeEndTemp" Editor="text" FieldName="OverTimeDateTimeEndTemp" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="OverTimeNO" ParentFieldName="OverTimeNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <div id="DivJQDataGrid2">
                <JQTools:JQDataGrid ID="dataGridHospital" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="HRMAttendOverTimeApplyHospital" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sHRMAttendOverTime.HRMAttendOverTimeApplyMaster" RowNumbers="True" Title="就醫服務紀錄" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" EditDialogID="JQDialog3" ParentObjectID="dataFormMaster">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="OverTimeNO" Editor="text" FieldName="OverTimeNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主" Editor="text" FieldName="Employer" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" RelationOptions="valueField:'EmployerID',textField:'EmployerShortName',remoteName:'sHRMAttendOverTime.EmployerName',tableName:'EmployerName'" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>                        
                        <JQTools:JQGridColumn Alignment="left" Caption="外勞姓名" Editor="infocombobox" EditorOptions="valueField:'e.EmployeeID',textField:'EmployeeCEName',remoteName:'sHRMAttendOverTime.EmployeeCEName',tableName:'EmployeeCEName',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ForeignLaborer" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" RelationOptions="" Sortable="False" Visible="True" Width="100" TableName="" DrillObjectID="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="infocombobox" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" EditorOptions="valueField:'EMPLOYEE_ID',textField:'NAME_C',remoteName:'sHRMAttendOverTime.infoHRM_BASE_BASE',tableName:'infoHRM_BASE_BASE',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Format="yyyy/mm/dd">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="累計次數" Editor="text" FieldName="counts" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="54" DrillObjectID="" FormatScript="FormatScript_DGHospital">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="counts1" Editor="text" FieldName="counts1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="欠款" Editor="text" FieldName="debt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="OverTimeNO" ParentFieldName="OverTimeNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" Visible="True" />
                    </TooItems>
                </JQTools:JQDataGrid>
                </div>
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Title="新增加班資料">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="HRMAttendOverTimeApplyDetails" HorizontalColumnsCount="2" RemoteName="sHRMAttendOverTime.HRMAttendOverTimeApplyMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="checkOvertimeData" OnLoadSuccess="OnLoadFormDetail" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" Format="" Width="120" Visible="False" NewRow="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="OverTimeNO" Editor="text" FieldName="OverTimeNO" Format="" Width="120" Visible="False" NewRow="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="加班日期" Editor="datebox" FieldName="OverTimeDate" Format="" Width="100" NewRow="True" Visible="True" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="申請類型" Editor="infocombobox" EditorOptions="valueField:'ApplyNO',textField:'ApplyType',remoteName:'sHRMAttendOverTime.HRMAttendOverTimeMealApplyType',tableName:'HRMAttendOverTimeMealApplyType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:CalOvertimeHoursAndMealTypeAndMealNTD,panelHeight:200" FieldName="ApplyOvertimeType" NewRow="True" Visible="True" Width="123" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="起始時間" Editor="text" FieldName="BeginTime" Format="" Width="120" NewRow="True" OnBlur="OnBlurOverTimeHours" Visible="True" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="終止時間" Editor="text" FieldName="EndTime" Format="" Width="120" OnBlur="OnBlurOverTimeHours" Visible="True" NewRow="True" Span="1" ReadOnly="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="總時數" Editor="numberbox" EditorOptions="precision:1" FieldName="TotalHours" NewRow="True" OnBlur="" ReadOnly="True" Span="1" Visible="True" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="休息時數" Editor="numberbox" EditorOptions="precision:1" FieldName="DinnerHours" NewRow="False" OnBlur="OnBlurDinnerHours" Span="1" Visible="True" Width="60" MaxLength="0" ReadOnly="False" RowSpan="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="加班時數" Editor="numberbox" FieldName="OverTimeHours" Format="" Width="120" EditorOptions="precision:1" Visible="False" NewRow="True" ReadOnly="True" Span="1" MaxLength="0" RowSpan="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="補休時數" Editor="numberbox" FieldName="RestHours" Format="" Width="120" EditorOptions="precision:1" Visible="False" NewRow="True" ReadOnly="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="加班原因" Editor="textarea" FieldName="OverTimeCauseID" Format="" Width="430" EditorOptions="height:50" Span="2" Visible="True" NewRow="False" ReadOnly="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="OverTimeDateTimeBegin" Editor="datebox" FieldName="OverTimeDateTimeBegin" Format="" Width="120" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="OverTimeDateTimeEnd" Editor="datebox" FieldName="OverTimeDateTimeEnd" Format="" Width="120" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" NewRow="False" maxlength="0" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" NewRow="False" ReadOnly="False" MaxLength="0" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="補助餐別" Editor="text" FieldName="MealType" Visible="True" Width="80" NewRow="False" ReadOnly="True" MaxLength="0" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="餐費補助金額" Editor="text" FieldName="MealTotalNTD" NewRow="False" Width="80" ReadOnly="True" Visible="True" MaxLength="0" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="EndTimeTemp" Editor="text" FieldName="EndTimeTemp" NewRow="False" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="OverTimeDateTimeEndTemp" Editor="text" FieldName="OverTimeDateTimeEndTemp" MaxLength="0" NewRow="False" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="OverTimeNO" ParentFieldName="OverTimeNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDialog ID="JQDialog3" runat="server" BindingObjectID="dataFormHospital" Title="新增就醫服務紀錄">


                    <JQTools:JQDataForm ID="dataFormHospital" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HRMAttendOverTimeApplyHospital" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sHRMAttendOverTime.HRMAttendOverTimeApplyMaster" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" ParentObjectID="dataFormMaster" OnLoadSuccess="OnLoadSuccessdataFormHospital" OnApply="OnApplydataFormHospital">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="OverTimeNO" Editor="text" FieldName="OverTimeNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="雇主" Editor="infocombobox" FieldName="Employer" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="200" EditorOptions="valueField:'EmployerID',textField:'EmployerName',remoteName:'sHRMAttendOverTime.EmployerName',tableName:'EmployerName',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployer,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="外勞姓名" Editor="infocombobox" FieldName="ForeignLaborer" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" EditorOptions="items:[{value:' ',text:' ',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectForeignLaborer,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="建立者" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" EditorOptions="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" Format="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="已就醫服務次數" Editor="text" EditorOptions="" FieldName="counts1" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="已就醫服務次數" Editor="text" FieldName="counts" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="尚欠款金額" Editor="text" EditorOptions="valueField:'debt',textField:'debt',remoteName:'sHRMAttendOverTime.EmployeeDebt',tableName:'EmployeeDebt',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="debt" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="OverTimeNO" ParentFieldName="OverTimeNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>


                </JQTools:JQDialog>
                <JQTools:JQDialog ID="JQDialog4" runat="server" BindingObjectID="" ShowSubmitDiv="False" Title="次數明細" Width="458px">
                    <JQTools:JQDataGrid ID="dataGridHospitalDrillDown" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="HRMAttendOverTimeApplyHospitalDrilldown" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sHRMAttendOverTime.HRMAttendOverTimeApplyHospitalDrilldown" RowNumbers="True" Title=" " TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" EditDialogID="">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="雇主" Editor="text" FieldName="EmployerShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="外勞姓名" Editor="text" FieldName="EmployeeCEName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="160">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="text" FieldName="USERNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Format="yyyy-mm-dd">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <TooItems>
                            <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="Insert" Visible="False" />
                            <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="False" />
                            <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="False" />
                            <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="False" />
                            <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="False" />
                            <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                            <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="False" />
                        </TooItems>
                    </JQTools:JQDataGrid>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="" DefaultValue="自動編號" FieldName="OverTimeNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="EmployeeID" RemoteMethod="False" DefaultMethod="getEmployeeID" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="OverTimeDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="OverTimeHours" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="RestHours" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="DinnerHours" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="OverTimeDate" RemoteMethod="False" ValidateMessage="加班日期請於4天內申請" ValidateType="None" CheckMethod="checkTimeDate" />
                        <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="BeginTime" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="EndTime" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="OverTimeCauseID" RemoteMethod="True" ValidateMessage="請選擇加班原因" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultHospital" runat="server" BindingObjectID="dataFormHospital">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="ItemSeq" NumDig="2" />
                <JQTools:JQAutoSeq ID="JQAutoSeq2" runat="server" BindingObjectID="dataFormHospital" FieldName="AutoKey" NumDig="2" />
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
