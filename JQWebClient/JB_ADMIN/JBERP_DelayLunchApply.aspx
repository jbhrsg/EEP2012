<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_DelayLunchApply.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.calendar_jb.js"></script>
     <script>
         var flag = true; //定義一個全域變數，只有第一次執行        
         var parameter = "";
         $(document).ready(function () {

             initDateDialog();//扣除未訂餐日期對話框初始化
             initEatDialog();
             initAbsentDialog();

             //將Focus 欄位背景顏色改為黃色
             $(function () {
                 $("input, select, textarea").focus(function () {
                     $(this).css("background-color", "yellow");
                 });
                 $("input, select, textarea").blur(function () {
                     $(this).css("background-color", "white");
                 });
             });

             //扣除未訂餐日期註冊
             $("#dataFormMasterNotCheckDate").jbDateBoxMultiple({
                 onDataChange: function () {
                     //var dateArray = $(this).jbDateBoxMultiple('getData');
                     getLunchData();
                 }
             });
             //----------------------------------------------------------------------------------------------------
             ////取得流程狀態=>控制顯示項目
             parameter = Request.getQueryStringByName("D");
             //----------------------------------------------------------------------------------------------------


             //--------------------------------------★Need Update-----------------------------------------------
              //parameter = "Modify";//Need Update  ex: Modify
             //----------------------------------------------------------------------------------------------------             
             
             if (parameter == "")//新增申請
             {
                 //BeginDate
                 $("#dataFormMasterBeginDate").combo('textbox').blur(function () {
                     //設定請領年月,期間金額,訂餐扣款,請假扣款
                     getLunchData();
                 });
                 $("#dataFormMasterBeginDate").datebox({
                     onSelect: function (date) {                   
                         getLunchData();
                     }
                 });

                 //EndDate
                 $("#dataFormMasterEndDate").combo('textbox').blur(function () {
                     //設定請領年月,期間金額,訂餐扣款,請假扣款
                     getLunchData();
                 });
                 $("#dataFormMasterEndDate").datebox({
                     onSelect: function (date) {
                         getLunchData();
                     }
                 });
                 
             }
             if (parameter == "Modify" || parameter == "Notify")//呈辦審核Modify , 通知申請者Notify
             {
                 //在 修改訂餐扣款 加入查詢超連結
                 var CheckEatLink = $('<a>', { href: 'javascript:void(0)', name: 'EatLink', onclick: 'LinkEatLink.call(this)' }).linkbutton({ plain: false, text: '查看' })[0].outerHTML
                 var dfCheckEat = $('#dataFormMasterCheckEat').closest('td');
                 dfCheckEat.append(CheckEatLink);

                 //在 修改請假扣款 加入查詢超連結
                 var CheckAbsentLink = $('<a>', { href: 'javascript:void(0)', name: 'AbsentLink', onclick: 'LinkAbsentLink.call(this)' }).linkbutton({ plain: false, text: '查看' })[0].outerHTML
                 var dfAbsentLink = $('#dataFormMasterCheckAbsent').closest('td');
                 dfAbsentLink.append(CheckAbsentLink);

                 //在 扣除未訂餐金額	 加入查詢超連結
                var CheckDateLink = $('<a>', { href: 'javascript:void(0)', name: 'DateLink', onclick: 'LinkDateLink.call(this)' }).linkbutton({ plain: false, text: '查看' })[0].outerHTML
                var dfCheckDate = $('#dataFormMasterApplyDate').closest('td');
                dfCheckDate.append(CheckDateLink);
             }
         
         });

         //dataform頁面控制
         function ControlModify() {
             
             $("#dataFormMasterNotCheckDate").jbDateBoxMultiple('setData');
             var FormName = '#dataFormMaster';
             if (parameter == "") {
                 //申請 => 把欄位隱藏
                 //修改期間金額,修改訂餐扣款,修改請假扣款,其他扣款,最後誤餐金額	,其他扣款備註
                 var HideFieldName = ['CheckTotal', 'CheckEat', 'CheckAbsent', 'CheckOther', 'CheckAmt', 'CheckOtherMemo'];
                 $.each(HideFieldName, function (index, fieldName) {
                     $(FormName + fieldName).closest('td').prev('td').hide();
                     $(FormName + fieldName).closest('td').hide();
                 });

                 //把扣款欄位變藍色,扣除未訂餐金額,訂餐扣款,請假扣款
                 var BlueFieldName = ['ApplyDate', 'ApplyEat', 'ApplyAbsent'];
                 $.each(BlueFieldName, function (index, fieldName) {
                     $(FormName + fieldName).closest('td').prev('td').css("color", "blue");                    
                 });

             }
             if (parameter == "Modify")//呈辦審核Modify 
             {
                 
                 //承辦 => 起始日期不可選取
                 $("#dataFormMasterBeginDate").datebox("disable");
                 $("#dataFormMasterEndDate").datebox("disable");

                 getLunchDataCheck(); /* 承辦再一次取得 設定請領年月,期間金額,訂餐扣款,請假扣款 */
                 
             }
             if (parameter == "Modify" || parameter == "Notify")//呈辦審核Modify , 通知申請者Notify
             {
                 //扣除未訂餐日期隱藏
                 $("#dataFormMasterNotCheckDate").closest('td').prev('td').hide();
                 $("#dataFormMasterNotCheckDate").closest('td').hide();

                 //把扣款欄位變藍色,扣除未訂餐金額,修改訂餐扣款,修改請假扣款,其他扣款
                 var BlueFieldName = ['ApplyDate', 'CheckEat', 'CheckAbsent', 'CheckOther'];
                 $.each(BlueFieldName, function (index, fieldName) {
                     $(FormName + fieldName).closest('td').prev('td').css("color", "blue");
                 });
             }
         }

         // 扣除未訂餐日期 dialog
         function initDateDialog() {
             $("#Dialog_DateLink").dialog(
             {
                 height: 320,
                 width: 220,
                 left: 250,
                 top: 130,
                 resizable: false,
                 modal: true,
                 title: "扣除未訂餐日期",
                 closed: true
             });
         }

         // 訂餐紀錄 dialog
         function initEatDialog() {
             $("#Dialog_EatLink").dialog(
             {
                 height: 320,
                 width: 220,
                 left: 250,
                 top: 130,
                 resizable: false,
                 modal: true,
                 title: "訂餐紀錄",
                 closed: true
             });
         }

         // 請假扣款紀錄 dialog
         function initAbsentDialog() {
             $("#Dialog_AbsentLink").dialog(
             {
                 height: 320,
                 width: 220,
                 left: 250,
                 top: 130,
                 resizable: false,
                 modal: true,
                 title: "請假紀錄",
                 closed: true
             });
         }

         // open期間扣除未訂餐日期 dialog
         function LinkDateLink() {
             var DelayLunchID = $('#dataFormMasterDelayLunchID').val();

             $("#dataGrid_DateLink").datagrid('setWhere', " DelayLunchID = " + DelayLunchID );
             $("#Dialog_DateLink").dialog("open");
         }

         // open修改訂餐扣款 dialog
         function LinkEatLink() {
             var userid = $('#dataFormMasterUserID').val();
             var beginDate = $('#dataFormMasterBeginDate').datebox('getValue');
             var endDate = $('#dataFormMasterEndDate').datebox('getValue');

             $("#dataGrid_EatLink").datagrid('setWhere', " o.EmpNum = '" + userid+"' and o.Adate between '" + beginDate + "' and '" + endDate + "'");
             $("#Dialog_EatLink").dialog("open");
         }

         // open修改請假扣款 dialog
         function LinkAbsentLink() {
             var userid = $('#dataFormMasterUserID').val();
             var beginDate = $('#dataFormMasterBeginDate').datebox('getValue');
             var endDate = $('#dataFormMasterEndDate').datebox('getValue');

             $("#dataGrid_AbsentLink").datagrid('setWhere', " b.EMPLOYEE_CODE = '" + userid + "' and ABSENT_DATE between '" + beginDate + "' and '" + endDate + "'");
             $("#Dialog_AbsentLink").dialog("open");
         }

         /* 左邊補0 */
         function padLeft(str, len) {
             str = '' + str;
             if (str.length >= len) {
                 return str;
             } else {
                 return padLeft("0" + str, len);
             }
         }

         /* 取得 設定請領年月,期間金額,訂餐扣款,請假扣款 */
         function getLunchData() {             
             //申請時 , 審核時不用再次取得
             if (parameter == "") {
                 userid = getClientInfo("UserID");//EMPLOYEE_CODE
                 var beginDate = $('#dataFormMasterBeginDate').datebox('getValue');
                 var endDate = $('#dataFormMasterEndDate').datebox('getValue');
                 var d = new Date(beginDate);
                 var y = d.getFullYear().toString();
                 var m = d.getMonth() + 1;
                 $('#dataFormMasterYearMonth').val(y + padLeft(m.toString(), 2));//設定請領年月

                 var beginDateValidate = $.fn.datebox('parseDate', beginDate.replace(/\//g, '-'));
                 var endDateValidate = $.fn.datebox('parseDate', endDate.replace(/\//g, '-'));

                 //扣除未訂餐日期	
                 var sNotCheckDate = $("#dataFormMasterNotCheckDate").val();                 

                 if ($('#dataFormMaster').form('validateForm') && beginDateValidate != "Invalid Date" && endDateValidate != "Invalid Date" && $.jbIsDateStr(beginDate) && $.jbIsDateStr(endDate) && endDate >= beginDate) {
                     var dateLockCnt;
                     $.ajax({
                         type: "POST",
                         url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchApply.ERPDelayLunchApply', //連接的Server端，command
                         data: "mode=method&method=" + "getLunchData" + "&parameters=" + userid + "," + beginDate + "," + endDate + "," + sNotCheckDate + ",0", //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                         cache: false,
                         async: false,
                         success: function (data) {
                             var rows = $.parseJSON(data);
                             if (rows.length > 0) {
                                 $('#dataFormMasterApplyDate').val(rows[0].ApplyDate);
                                 $('#dataFormMasterApplyTotal').val(rows[0].ApplyTotal);
                                 $('#dataFormMasterApplyEat').val(rows[0].ApplyEat);
                                 $('#dataFormMasterApplyAbsent').val(rows[0].ApplyAbsent);
                                 $('#dataFormMasterApplyAmt').val(rows[0].ApplyTotal - rows[0].ApplyDate - rows[0].ApplyEat - rows[0].ApplyAbsent);
                             }
                         }
                     });
                 }
             }
             
         }

         /* 承辦再一次取得 設定請領年月,期間金額,訂餐扣款,請假扣款 =>DelayLunchID */
         function getLunchDataCheck() {
             if (parameter == "Modify") {
                 var userid = $('#dataFormMasterUserID').val();
                 var beginDate = $('#dataFormMasterBeginDate').datebox('getValue');
                 var endDate = $('#dataFormMasterEndDate').datebox('getValue');
                 var DelayLunchID = $('#dataFormMasterDelayLunchID').val();
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchApply.ERPDelayLunchApply', //連接的Server端，command
                     data: "mode=method&method=" + "getLunchData" + "&parameters=" + userid + "," + beginDate + "," + endDate + ",," + DelayLunchID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                     cache: false,
                     async: false,
                     success: function (data) {
                         var rows = $.parseJSON(data);
                         if (rows.length > 0) {
                             $('#dataFormMasterCheckTotal').val(rows[0].ApplyTotal);
                             $('#dataFormMasterCheckTotal').numberbox('setValue', rows[0].ApplyTotal);
                             $('#dataFormMasterCheckEat').val(rows[0].ApplyEat);
                             $('#dataFormMasterCheckEat').numberbox('setValue', rows[0].ApplyEat);
                             $('#dataFormMasterCheckAbsent').val(rows[0].ApplyAbsent);
                             $('#dataFormMasterCheckAbsent').numberbox('setValue', rows[0].ApplyAbsent);
                             $('#dataFormMasterCheckOther').numberbox('setValue', 0);//其他扣款預設為0
                             var Amt = rows[0].ApplyTotal - rows[0].ApplyDate - rows[0].ApplyEat - rows[0].ApplyAbsent;
                             $('#dataFormMasterCheckAmt').val(Amt);
                             $('#dataFormMasterCheckAmt').numberbox('setValue', Amt);
                             //$(FormName + fieldName).closest('td').css("color", "red");
                             $('#dataFormMasterCheckAmt').closest('td').prev('td').css("color", "red");

                         }
                     }
                 });
             }
         }

         
         //check 起始日期 => 當月誤餐費請於次月9號(含)前申請
         // 7/9  =>6/1~7/9 (6/1以前&7/9號以後不可申請)
         // 7/10 =>7/1~7/10 (7/1以前&7/10號以後不可申請)
         function checkTimeSDate(val) {             
             if (parameter == "") {//申請
                 var date = new Date();
                 var dNow = $.jbjob.Date.DateFormat(date, 'yyyy/MM/dd');//今天日期
                 var FirstDate = new Date($.jbGetFirstDate(date));
                 var dFirstDate = $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd');//今天的月初日期
                 var LastFirstDate = new Date($.jbDateAdd('months', -1, dFirstDate));
                 var dLastFirstDate = $.jbjob.Date.DateFormat(LastFirstDate, 'yyyy/MM/dd');//上月的月初日期
                 var NineDate = new Date($.jbDateAdd('days', 8, FirstDate));
                 var dNineDate = $.jbjob.Date.DateFormat(NineDate, 'yyyy/MM/dd');//當月9號
                 //求得開始的最小日期
                 var dSDate;
                 if (dNow > dNineDate) dSDate = dFirstDate;
                 else dSDate = dLastFirstDate;

                 if (val < dSDate) return false;
                 else return true;
             } else 
             return true;//審核不檢查
         }

         //起始結束日期須同一月份,結束日期須大於起始日期
         function checkTimeEDate(val) {             
             if (parameter == "") {//申請
                 var sDate = $("#dataFormMasterBeginDate").datebox('getValue');//起始日期
                 var date2 = new Date(sDate);
                 var LastDate = new Date($.jbGetLastDate(date2));
                 var dLastDate = $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd');//起始日期的月底日期
                 var date = new Date();
                 var eDate = $.jbjob.Date.DateFormat(date, 'yyyy/MM/dd');//今天日期
                 //起始日期的月底日期大於 今天日期=>今天日期為主
                 if (dLastDate > eDate) {
                     dLastDate = eDate;
                 }
                 if (val > dLastDate || val < sDate) return false;//不ok
                 else return true;
             } else return true;//審核不檢查
         }

         //計算最後誤餐金額
         function OnBlur_Check() {             
                 CheckTotal = $('#dataFormMasterCheckTotal').val();
                 ApplyDate = $('#dataFormMasterApplyDate').val();
                 CheckEat = $('#dataFormMasterCheckEat').val();
                 CheckAbsent = $('#dataFormMasterCheckAbsent').val();
                 CheckOther = $('#dataFormMasterCheckOther').val();
                 var Amt = CheckTotal - ApplyDate - CheckEat - CheckAbsent - CheckOther;//修改期間金額-扣除未訂餐金額-修改訂餐扣款-修改請假扣款-其他扣款
                 $('#dataFormMasterCheckAmt').numberbox('setValue', Amt);
         }
        
         //存檔前檢查
         function checkApplyData() {
             if (!$(this).form('validateForm')) return false;            

             //申請時檢查
             //1.檢查是否在設定的名單內 ERPDelayLunchEmp (500名單) , ERPDelayLunchEmpAuto (系統自動產生名單)
             //2.判斷誤餐費申請的時段內是否已有存在的誤餐費資料
             //3.檢查區間扣除未訂餐日期 是否在 起始日期 與 結束日期 之間
             //3.1.提醒區間扣除未訂餐日期 與 訂餐扣款 或 請假扣款 是否重複
             //4.最後申請金額不可<=0
             
             //申請檢查
             //1.簽核的年月是否正確
             //2.結算金額不可<= 0
             if (parameter == "") {
                 var userid = $('#dataFormMasterUserID').val();

                 if (!$('#dataFormMaster').form('validateForm')) return false;

                 var DelayLunchID;
                 if (getEditMode($("#dataFormMaster")) == 'updated')
                     DelayLunchID = $('#dataFormMasterDelayLunchID').val();
                 else
                     DelayLunchID = "0";

                 var beginDate = $('#dataFormMasterBeginDate').datebox('getValue');
                 var endDate = $('#dataFormMasterEndDate').datebox('getValue');

                 //1.檢查是否在設定的名單內 ERPDelayLunchEmp (500名單) , ERPDelayLunchEmpAuto (系統自動產生名單)
                 var cnt2;
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchApply.ERPDelayLunchApply', //連接的Server端，command
                     data: "mode=method&method=" + "checkLunchEmp" + "&parameters=" + userid, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                     cache: false,
                     async: false,
                     success: function (data) {
                         if (data != false) {
                             cnt2 = $.parseJSON(data);
                         }
                     }
                 });
                 if ((cnt2 != "0" && cnt2 != "undefined")) {
                     alert("已在設定名單內，不可申請誤餐資料！");
                     return false;
                 }

                 //2.判斷誤餐費申請的時段內是否已有存在的誤餐費資料               
                 var cnt;
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchApply.ERPDelayLunchApply', //連接的Server端，command
                     data: "mode=method&method=" + "checkLunchData" + "&parameters=" + DelayLunchID + "," + userid + "," + beginDate + "," + endDate, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                     cache: false,
                     async: false,
                     success: function (data) {
                         if (data != false) {
                             cnt = $.parseJSON(data);
                         }
                     }
                 });
                 if ((cnt != "0" && cnt != "undefined")) {
                     alert("申請的時段內已有存在的誤餐資料！");
                     return false;
                 }                                 

                 //3.檢查區間扣除未訂餐日期 是否在 起始日期 與 結束日期 之間        
                 var sNotCheckDate = $("#dataFormMasterNotCheckDate").val();
                 ////計算日期個數
                 if (sNotCheckDate != '') {
                     var cnt;
                     var iCount = 0;
                     var arr = sNotCheckDate.split("\n");
                     $.each(arr, function (i, val) {
                         if (val != "") {
                             iCount = iCount + 1;
                         }
                     });

                     $.ajax({
                         type: "POST",
                         url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchApply.ERPDelayLunchApply', //連接的Server端，command
                         data: "mode=method&method=" + "checkNotCheckDate" + "&parameters=" + userid + "," + beginDate + "," + endDate + "," + sNotCheckDate,
                         cache: false,
                         async: false,
                         success: function (data) {
                             if (data != false) {
                                 cnt = $.parseJSON(data);
                             }
                         }
                     });
                     if (cnt != iCount) {
                         alert("扣除未訂餐日期應在起始日期與結束日期之間！");
                         return false;
                     }
                     //3.1.提醒區間扣除未訂餐日期 與 訂餐扣款 或 請假扣款 是否重複
                     var Dates;
                     var Date2s;
                     $.ajax({
                         type: "POST",
                         url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchApply.ERPDelayLunchApply', //連接的Server端，command
                         data: "mode=method&method=" + "checkDateduplicate" + "&parameters=" + userid + "," + beginDate + "," + endDate + "," + sNotCheckDate,
                         cache: false,
                         async: false,
                         success: function (data) {
                             var rows = $.parseJSON(data);
                             if (rows.length > 0) {
                                 Dates = rows[0].Dates.trim();
                                 Date2s = rows[0].Date2s.trim();
                             }
                         }
                     });
                     if (Dates != "") {
                         alert("扣除未訂餐日期與訂餐扣款日重複:" + Dates);
                         return false;
                     }
                     if (Date2s != "") {
                         alert("扣除未訂餐日期與請假扣款日重複:" + Date2s);
                         return false;
                     }
                 }
                 //4.最後申請金額不可<=0
                 if (($('#dataFormMasterApplyAmt').val() <= 0)) {
                     alert("誤餐金額<=0，故不用申請！");
                     return false;
                 }

             } else if (parameter == "Modify") {
                 //檢查簽核的年月是否正確=>抓此筆簽核的 iType => 1 年月 2 上個年月  是否已結案   
                 var YearMonth = $('#dataFormMasterYearMonth').val();
                 var IsClose = 0;
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchApply.ERPDelayLunchApply', //連接的Server端，command
                     data: "mode=method&method=" + "checkLunchApplyClose" + "&parameters=" + YearMonth + ",2",
                     cache: false,
                     async: false,
                     success: function (data) {
                         if (data != false) {
                             IsClose = $.parseJSON(data);
                         }
                     }
                 });
                 if ((IsClose == "False")) {
                     alert("此筆資料請領年月尚不可以簽核！");
                     return false;
                 }

                 //把其他扣款補0
                 if ($('#dataFormMasterCheckOther').val() == "") {
                     $('#dataFormMasterCheckOther').val(0);
                     $('#dataFormMasterCheckOther').numberbox('setValue', 0);
                 }
                 //var ifinal = $('#dataFormMasterCheckTotal').val() - $('#dataFormMasterCheckEat').val() - $('#dataFormMasterCheckAbsent').val() - $('#dataFormMasterCheckOther').val();
                 //if ((ifinal <= 0)) {
                 //    alert("結算金額為 "+ifinal+"，資料有誤！");
                 //    return false;
                 //}                                 

             }
         }

        

     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPDelayLunchApply.ERPDelayLunchApply" runat="server" AutoApply="True"
                DataMember="ERPDelayLunchApply" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="誤餐費申請">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="DelayLunchID" Editor="numberbox" FieldName="DelayLunchID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="BeginDate" Editor="datebox" FieldName="BeginDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EndDate" Editor="datebox" FieldName="EndDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ApplyTotal" Editor="numberbox" FieldName="ApplyTotal" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ApplyEat" Editor="numberbox" FieldName="ApplyEat" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ApplyAbsent" Editor="numberbox" FieldName="ApplyAbsent" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CheckTotal" Editor="numberbox" FieldName="CheckTotal" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CheckEat" Editor="numberbox" FieldName="CheckEat" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CheckAbsent" Editor="numberbox" FieldName="CheckAbsent" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CheckOther" Editor="numberbox" FieldName="CheckOther" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckOtherMemo" Editor="text" FieldName="CheckOtherMemo" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsClose" Editor="text" FieldName="IsClose" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CloseDate" Editor="datebox" FieldName="CloseDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="誤餐費申請" Width="600px" DialogTop="70px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPDelayLunchApply" HorizontalColumnsCount="2" RemoteName="sERPDelayLunchApply.ERPDelayLunchApply" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="checkApplyData" OnLoadSuccess="ControlModify" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="DelayLunchID" Editor="numberbox" FieldName="DelayLunchID" Format="" Width="180" Visible="False" NewRow="False" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="BeginDate" Format="" Width="100" NewRow="True" Visible="True" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結束日期" Editor="datebox" FieldName="EndDate" Format="" Width="100" NewRow="False" ReadOnly="False" Visible="True" Span="1" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="扣除未訂餐日期" Editor="textarea" FieldName="NotCheckDate" NewRow="False" ReadOnly="False" Visible="True" Width="80" OnBlur="" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="期間金額" Editor="numberbox" FieldName="ApplyTotal" NewRow="True" ReadOnly="True" Visible="True" Width="80" Format="" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改期間金額" Editor="numberbox" FieldName="CheckTotal" NewRow="False" ReadOnly="False" Visible="True" Width="80" OnBlur="OnBlur_Check" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請領年月" Editor="text" FieldName="YearMonth" Width="80" NewRow="True" ReadOnly="True" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="扣除未訂餐金額" Editor="numberbox" FieldName="ApplyDate" NewRow="False" ReadOnly="True" Width="80" Visible="True" Span="1" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訂餐扣款" Editor="numberbox" FieldName="ApplyEat" Width="80" NewRow="True" ReadOnly="True" MaxLength="0" RowSpan="1" Span="1" Visible="True" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改訂餐扣款" Editor="numberbox" FieldName="CheckEat" NewRow="False" Width="80" Visible="True" ReadOnly="False" Span="1" MaxLength="0" RowSpan="1" OnBlur="OnBlur_Check" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假扣款" Editor="numberbox" FieldName="ApplyAbsent" Width="80" ReadOnly="True" Visible="True" NewRow="True" Span="1" MaxLength="0" RowSpan="1" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改請假扣款" Editor="numberbox" FieldName="CheckAbsent" NewRow="False" Width="80" Visible="True" Span="1" ReadOnly="False" MaxLength="0" RowSpan="1" OnBlur="OnBlur_Check" />
                        <JQTools:JQFormColumn Alignment="left" Caption="誤餐金額" Editor="numberbox" FieldName="ApplyAmt" NewRow="False" ReadOnly="True" Visible="True" Width="80" MaxLength="0" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="其他扣款" Editor="numberbox" FieldName="CheckOther" NewRow="False" Visible="True" Width="80" Span="1" ReadOnly="False" MaxLength="0" RowSpan="1" OnBlur="OnBlur_Check" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最後誤餐金額" Editor="numberbox" FieldName="CheckAmt" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="扣款備註" Editor="textarea" FieldName="CheckOtherMemo" Width="350" Visible="True" EditorOptions="height:80" NewRow="True" Span="2" ReadOnly="False" MaxLength="0" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" NewRow="False" ReadOnly="False" Visible="False" Width="180" MaxLength="0" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="BeginDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="DelayLunchID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="UserID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckMethod="checkTimeSDate" CheckNull="True" FieldName="BeginDate" RemoteMethod="False" ValidateType="None" ValidateMessage="起始日期不正確" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EndDate" RemoteMethod="False" ValidateType="None" CheckMethod="checkTimeEDate" ValidateMessage="結束日期不正確" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ApplyAmt" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
         <div id="Dialog_DateLink">
            <div class="div_RelativeLayout1">
                <JQTools:JQDataGrid ID="dataGrid_DateLink" data-options="pagination:true,view:commandview" RemoteName="sERPDelayLunchApply.infoDateRecord" runat="server" AutoApply="False"
                    DataMember="infoDateRecord" Pagination="False" QueryTitle="查詢" EditDialogID=""
                    Title="" QueryLeft="300px" QueryTop="100px" AlwaysClose="True" AllowAdd="False" AllowDelete="False" AllowUpdate="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryMode="Window" RecordLock="False" RecordLockMode="None" TotalCaption="加總 : " UpdateCommandVisible="False" ViewCommandVisible="False" Width="100%" BufferView="False" NotInitGrid="False" RowNumbers="True" Height="500px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="扣除未訂餐日期" Editor="datebox" FieldName="NotCheckDate" Width="160" Visible="True" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>
        <div id="Dialog_EatLink">
            <div class="div_RelativeLayout2">
                <JQTools:JQDataGrid ID="dataGrid_EatLink" data-options="pagination:true,view:commandview" RemoteName="sERPDelayLunchApply.infoEatRecord" runat="server" AutoApply="False"
                    DataMember="infoEatRecord" Pagination="False" QueryTitle="查詢" EditDialogID=""
                    Title="" QueryLeft="300px" QueryTop="100px" AlwaysClose="True" AllowAdd="False" AllowDelete="False" AllowUpdate="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryMode="Window" RecordLock="False" RecordLockMode="None" TotalCaption="加總 : " UpdateCommandVisible="False" ViewCommandVisible="False" Width="100%" BufferView="False" NotInitGrid="False" RowNumbers="True" Height="500px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="訂餐日期" Editor="datebox" FieldName="Adate" Width="160" Visible="True" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>
        <div id="Dialog_AbsentLink">
            <div class="div_RelativeLayout3">
                <JQTools:JQDataGrid ID="dataGrid_AbsentLink" data-options="pagination:true,view:commandview" RemoteName="sERPDelayLunchApply.infoAbsentRecord" runat="server" AutoApply="False"
                    DataMember="infoAbsentRecord" Pagination="False" QueryTitle="查詢" EditDialogID=""
                    Title="" QueryLeft="300px" QueryTop="100px" AlwaysClose="True" AllowAdd="False" AllowDelete="False" AllowUpdate="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryMode="Window" RecordLock="False" RecordLockMode="None" TotalCaption="加總 : " UpdateCommandVisible="False" ViewCommandVisible="False" Width="100%" BufferView="False" NotInitGrid="False" RowNumbers="True" Height="500px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="請假日期" Editor="datebox" FieldName="Adate" Width="160" Visible="True" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>
        </div>
    </form>
</body>
</html>
