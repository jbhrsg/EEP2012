<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesDetails.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
<%--    <script src="../js/jquery.jbjob.js"></script>--%>
     <script>
         //=========================================控制3個Grid1=0都做完才顯示Grid=========================================================================================
         var waitA = false;
         var waitB = false;
         var waitC = false;
         var myVar = setInterval(function () { myTimer(); }, 100);
         setTimeout(function () { clearTimeout(myVar); }, 6000);//最多6秒結束
         
         function myTimer() {                          
             if (waitA == true && waitB == true ) {
                 RefreshGrid();//更新主檔                 
                 RefreshSalesInfo();//更新資訊統計
                 clearTimeout(myVar);
             }
         }        
         //=============================================取得登入者工號(是否有最高權限)=========================================================================================
         function GetLoginID() {
             var bAdmin = false;
             //var cookies = document.cookie.split(';');
             //var curruser = "";
             //for (var i = 0; i < cookies.length; i++) {
             //    var cookie = cookies[i];
             //    var temp = cookie.split('=');
             //    if (temp[0] == "username") {
             //        curruser = temp[1];
             //    }
             //}
             var sUserID = getClientInfo("UserID");
             if (sUserID == "011" || sUserID == "020" || sUserID == "060") {
                 var bAdmin = true;
             }
             return bAdmin;
         }
         ///=============================================  ready  ===============================================================================================
         $(document).ready(function () {             
             //寬度調整
             $("#cbSalesEmployeeID").combobox('resize','105');
             $("#cbCustNO").combobox('resize', '200');
             $("#cbSalesType").combobox('resize', '80');
             //跳登日期註冊
             $("#dataFormMasterJumpDate").jbDateBoxMultiple({});
             //週報日期註冊
             $("#dataFormMasterWeekendDate").jbDateBoxMultiple({});
             //查詢條件預設值  
             //setTimeout(function () { $("#cbSalesEmployeeID").combobox('select', UserID); }, 5000);//自動篩選
             var UserID = getClientInfo("UserID");
             //用戶編號=>業務代號
             var UserID = getClientInfo("UserID");
             setTimeout(function () {
                 var data = $("#cbSalesEmployeeID").combobox('getData');
                 for (var i = 0; i < data.length; i++) {
                     if (data[i].SalesEmployeeID == UserID) {
                         $("#cbSalesEmployeeID").combobox('setValue', data[i].SalesID);
                     }
                 }
             }, 200);

             $("#cbCustNO").combobox('setValue', "");
             //月初,月底
             //var FirstDate = new Date($.jbGetFirstDate(dt));                          
             //var LastDate = new Date($.jbGetLastDate(dt));             
             var dt = new Date();
             var aDate = new Date($.jbDateAdd('days', 1, dt));//開始日期明天
             var aDate2 = new Date($.jbDateAdd('days', 6, dt));//開始日期+5天
             $("#JQDate1").datebox('setValue', $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd'));
             $("#JQDate2").datebox('setValue', $.jbjob.Date.DateFormat(aDate2, 'yyyy/MM/dd'));
             
             //查詢欄位更新主檔
             $('#JQDate1').datebox({
                 width: 90,
                 onSelect: function (date) {
                     RefreshGrid();
                     RefreshSalesInfo();                
                 }
             }).combo('textbox').blur(function () {
                 setTimeout(function () {
                     RefreshGrid();
                     RefreshSalesInfo();
                 }, 500);
             });
             $('#JQDate2').datebox({
                 width: 90,
                 onSelect: function (date) {
                     RefreshGrid();
                     RefreshSalesInfo();
                 }
             }).combo('textbox').blur(function () {
                 setTimeout(function () {
                     RefreshGrid();
                     RefreshSalesInfo();
                 }, 500);
             });
             //新增主檔銷貨排版
             AddSalesMasterLoad();            
             //代辦事項筆數
             ShowToDoCount();
             ////新增銷貨明細排版and 註冊日曆
             AddSalesDetailsLoad();
             //預收查詢=>查詢條件預設值
             var dt = new Date();
             var FirstDate = new Date($.jbGetFirstDate(dt));
             $("#SalesDescrDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));
             var LastDate = new Date($.jbGetLastDate(dt));
             $("#SysDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));

             //主檔訂單修改加字串(發票已開，贈期不轉下月)
             var IsConvertNexMonth = $("#dataFormMasterUpdateIsConvertNexMonth").closest('td');
             IsConvertNexMonth.append('發票已開，贈期不轉下月   ');
             $("#dataFormMasterUpdateIsConvertNexMonth").closest('td').css("color", "blue");
             ///主檔新增銷貨加字串(發票已開，贈期不轉下月)
             var IsConvertNexMonth = $("#dataFormMasterIsConvertNexMonth").closest('td');
             IsConvertNexMonth.append('發票已開，贈期不轉下月   ');
             $("#dataFormMasterIsConvertNexMonth").closest('td').css("color", "blue");
         });        
         //=============================================代辦事項筆數呈現=========================================================================================
         function ShowToDoCount(SalesEmployeeID) {
             var cnt;
             var SalesEmployeeID = $("#cbSalesEmployeeID").combobox('getValue');
             //var UserID = getClientInfo("UserID");                        
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetails.ERPSalesMaster', //連接的Server端，command
                 data: "mode=method&method=" + "ShowToDoCount" + "&parameters=" + SalesEmployeeID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data != false) {
                         cnt = data;
                     }                    
                 },
                 error: function (xhr, ajaxOptions, thrownError) {
                     alert(xhr.status);
                     alert(thrownError);
                 }
             });
             //代辦事項呈現
             if (cnt != undefined) {                               
                 $('#LinkToDoList').html('提醒您！目前有 ' + cnt + ' 筆待辦事項。').css("background-color", "pink");                 
               } 
             else {
                 $('#LinkToDoList').html('');
             }
         }
         function GoToDoList() {             
            parent.addTab("待辦事項提醒", "JB_ADMIN/JBERP_SalesToDoList.aspx");             
         }
         //呼叫預收查詢頁面
         function OpenAcceptDateList() {             
             openForm('#JQDialogAcceptDate', {}, 'viewed', 'dialog')
         }
         //=============================================預收查詢頁面 OnLoad=========================================================================================
         function queryGrid(dg) {//查詢後添加固定條件
             if ($(dg).attr('id') == 'JQGridAcceptDate') {
                 //查詢條件
                 var result = [];
                 var SalesID = $('#SalesID_Query').combobox('getValue');//業務代號
                 var CustNO = $('#CustNO_Query').combobox('getValue');//客戶代號
                 var MinSalesDate = $('#SalesDescrDate_Query').datebox('getValue');//開始日期
                 var MaxSalesDate = $('#SysDate_Query').datebox('getValue');//結束日期                           
                 if (SalesID != '') result.push("d.SalesID = '" + SalesID + "'");
                 if (CustNO != '') result.push("m.CustNO = '" + CustNO + "'");
                 if (MinSalesDate != '') result.push("m.AcceptDate >= '" + MinSalesDate + "'");
                 if (MaxSalesDate != '') result.push("m.AcceptDate <= '" + MaxSalesDate + "'");
                 $(dg).datagrid('setWhere', result.join(' and '));
             }
         }
         //=============================================上面查詢條件=========================================================================================
         //業務人員combo更新時的事件
         function cbSalesEmployeeIDRefresh() {                        
             ShowToDoCount();//代辦事項筆數呈現
             RefreshGrid();//更新主檔
             RefreshSalesInfo();//更新資訊統計
         }
         //客戶代號combo更新時的事件
         function cbCustNORefresh(rowData) {
             //選完客戶時直接帶入選擇客戶的業務   
             var setvalue = rowData.SalesID;
            var data = $("#cbSalesEmployeeID").combobox('getData');            
            if (data.length > 0) {
                $("#cbSalesEmployeeID").combobox('setValue', setvalue);
            }

            RefreshGrid();
            RefreshSalesInfo();
         }
         //交易別combo更新時的事件
         function cbSalesTypeRefresh() {
             RefreshGrid();
             RefreshSalesInfo();
         }
         //=============================================新增主檔Dialog=========================================================================================
         //呼叫視窗銷貨明細新增
         function OpenInsertSalesDetails() {
             openForm('#JQDialog1', {}, "inserted", 'dialog');//
         }
         //新增銷貨
         function OnAppliedSalesDetails() {
             RefreshGrid();
             RefreshSalesInfo();//更新資訊統計
         }
         function AddSalesMasterLoad() {                        
             //新增銷貨畫面選單預設
             ControlShowItem();
             ControlSalesType();            
         }
         //新增銷貨時取得SalesEmployeeID
         function GetSalesEmployeeID() {
             return $("#cbSalesEmployeeID").combobox('getValue');
         }
         //左填補字串
         function padLeft(str, lenght) {
             if (str.length >= lenght)
                 return str;
             else
                 return padLeft("0" + str, lenght);
         }
         //新增銷貨時取得系統變數InvoiceYMPoint結帳日
         function GetInvoiceYMPoint() {
             var InvoiceYMPoint;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetails.ERPSalesMaster', //連接的Server端，command
                 data: "mode=method&method=" + "GetInvoiceYMPoint", //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data != false) {
                         InvoiceYMPoint = data;
                     }
                 },
             });
             //var dt = new Date();
             //var dt2 = $.jbjob.Date.DateFormat(InvoiceYMPoint, 'yyyy/MM/dd')             
             return InvoiceYMPoint;
         }
         
         //依據交易別顯示隱藏 求才或報紙 選項
         function ControlShowItem() {
             var SalesTypeID = $("#dataFormMasterSalesTypeID").combobox('getValue');
             //報,版,發,段,社單價,社行,客行,繳社價,客單價//客總價
             var HideFieldName = ['NewsTypeID', 'NewsAreaID', 'NewsPublishID', 'Sections', 'OfficePrice', 'OfficeLines', 'CustLines', 'OfficeAmt', 'CustPrice'];//, 'CustAmt'
             var FormName = '#dataFormMaster';

             //版別,版別之區域,贈週報,單位數,見刊數,總單位數,保留天數 
             var HideFieldName2 = ['DMTypeID', 'ViewAreaID', 'PresentWNewsCount', 'SalesQty', 'SalesQtyView', 'TotalSalesQty', 'KeepDays'];
             var FormName2 = '#dataFormMaster';

             //報紙6=>顯示
             if (SalesTypeID.trim() == "6") {
                 //求才之版別,區域清空
                 $("#dataFormMasterDMTypeID").combobox('setValue', "");
                 $("#dataFormMasterViewAreaID").combobox('setValue', "");
                 $.each(HideFieldName, function (index, fieldName) {
                     $(FormName + fieldName).closest('td').css("color", "red");
                     $(FormName + fieldName).closest('td').prev('td').css("color", "red");
                     $(FormName + fieldName).closest('td').prev('td').show();                     
                     $(FormName + fieldName).closest('td').show();
                 });
                 //版別,版別之區域,贈週報,單位數,總單位數,保留天數 =>隱藏
                 $.each(HideFieldName2, function (index, fieldName) {                    
                     $(FormName2 + fieldName).closest('td').prev('td').hide();
                     $(FormName2 + fieldName).closest('td').hide();
                 });                
             } else {//求才1=>隱藏
                 $.each(HideFieldName, function (index, fieldName) {
                     $(FormName + fieldName).closest('td').prev('td').hide();
                     $(FormName + fieldName).closest('td').hide();
                 });
                 $("#dataFormMasterDMTypeID").closest('td').prev('td').css("color", "red");
                 $("#dataFormMasterViewAreaID").closest('td').prev('td').css("color", "red");

                 //版別,版別之區域,贈週報,單位數,總單位數,保留天數 =>顯示
                 $.each(HideFieldName2, function (index, fieldName) {
                     $(FormName2 + fieldName).closest('td').prev('td').show();
                     $(FormName2 + fieldName).closest('td').show();
                 });                
             }
             //PDF檔名清空(非 1 : 求才,31 : 便利報 選項)
             var SalesTypeID = $("#dataFormMasterSalesTypeID").combobox('getValue');
             if (SalesTypeID != "1" && SalesTypeID != "31") {
                 $("#dataFormMasterRemark1").val("");
             } else {
                 var SalesQty = $("#dataFormMasterSalesQty").val();
                 var CustNO = $("#dataFormMasterCustNO").combobox('getValue');
                 var setvalue = CustNO.trim() + "-" + SalesQty.trim();
                 $("#dataFormMasterRemark1").val(setvalue);
             }

         }
         
         //主檔OnLoadSuccess
         function MasterOnLoadSuccess() {
            
             //刊登方式顯示隱藏與否 (連登或跳登選項)
             ControlSalesType();
             //主檔版面控制預設求才選項
             ControlShowItem();
             //日曆元件清空(連登日期)                          
             $("#dataFormMasterJumpDate").jbDateBoxMultiple("setData");
             //日曆元件清空(週報日期),預設隱藏                         
             $("#dataFormMasterWeekendDate").jbDateBoxMultiple("setData");
             $("#dataFormMasterWeekendDate").closest('td').prev('td').hide();
             $("#dataFormMasterWeekendDate").closest('td').hide();
             //客戶代號,版別,區域,報別,版別,發刊單位 ---請選擇--- 拿掉=>預設空白            
             var HideFieldName = ['CustNO', 'DMTypeID', 'ViewAreaID', 'NewsTypeID', 'NewsAreaID', 'NewsPublishID'];
             var FormName = '#dataFormMaster';
             $.each(HideFieldName, function (index, fieldName) {
                 $(FormName + fieldName).combobox('setValue', "");
             });
             //PDF檔名清空
             $("#dataFormMasterRemark1").val("");
         }
         //刊登方式顯示隱藏與否 (連登或跳登選項)
         function ControlSalesType() {
                 var PublishType = $("#dataFormMasterPublishType").options('getValue');
                 //連登1,跳登2
                 if (PublishType.trim() == "1") {
                     //連登日期預設明天
                     var dt = new Date();
                     var TDate = new Date($.jbDateAdd('days', 1, dt));
                     $("#dataFormMasterContinueDate").datebox('setValue', $.jbjob.Date.DateFormat(TDate, 'yyyy/MM/dd'));
                     $("#dataFormMasterContinueDate").closest('td').prev('td').css("color", "red");
                     $("#dataFormMasterContinueDate").closest('td').prev('td').show();
                     $("#dataFormMasterContinueDate").closest('td').show();
                     $("#dataFormMasterJumpDate").closest('td').prev('td').hide();
                     $("#dataFormMasterJumpDate").closest('td').hide();
                 } else {//求才1=>隱藏                
                     $("#dataFormMasterContinueDate").closest('td').prev('td').hide();
                     $("#dataFormMasterContinueDate").closest('td').hide();
                     $("#dataFormMasterJumpDate").closest('td').prev('td').css("color", "red");
                     $("#dataFormMasterJumpDate").closest('td').prev('td').show();
                     $("#dataFormMasterJumpDate").closest('td').show();
                 }
         }
         //贈週報控制是否顯示日歷元件
         function OnBlur_PresentWNewsCount() {
             var PresentWNewsCount = parseInt($("#dataFormMasterPresentWNewsCount").val());
             if (PresentWNewsCount > 0) {
                 $("#dataFormMasterWeekendDate").closest('td').prev('td').css("color", "red");
                 $("#dataFormMasterWeekendDate").closest('td').prev('td').show();
                 $("#dataFormMasterWeekendDate").closest('td').show();
             } else {
                 $("#dataFormMasterWeekendDate").closest('td').prev('td').hide();
                 $("#dataFormMasterWeekendDate").closest('td').hide();
             }
         }
         //新增銷貨時選完客戶時直接帶入PDF檔名
         function OnSelectCustNO(rowData) {
             //單位數
             var SalesQty = $("#dataFormMasterSalesQty").val();
             var setvalue = rowData.CustNO.trim() + "-" + SalesQty.trim();
             $("#dataFormMasterRemark1").val(setvalue);
         }
         //新增銷貨時檢查是否有此客戶
         function CheckNullCustNO() {
             var CustShortName;
             var CustNO = $("#dataFormMasterCustNO").combobox('getValue');
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetails.ERPSalesMaster', //連接的Server端，command
                 data: "mode=method&method=" + "CheckNullCustNO" + "&parameters=" + CustNO,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                 cache: false,
                 async: false,  
                 success: function (data) {
                     CustShortName = data
                 },
             });
             return CustShortName;
         }
         //新增銷貨時檢查必填
         function checkItemNull() {
             //客戶資料不完整檢查(缺少對應業務,收款方式)
             if ($("#dataFormMasterCustNO").combobox('getText').lastIndexOf('?') != -1)//沒有找到
             {
                 alert('此客戶資料不完整(無對應業務或沒有收款方式)！');
                 return false;
             }
             //客戶代號自行key in檢查
             var CustShortName=CheckNullCustNO();
             if (CustShortName == "") {
                 alert('此客戶代號不存在！');
                 return false;
             } else {
                 $("#dataFormMasterCustShortName").val(CustShortName);
             }
             var dt = new Date();
             var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')
             //連登1,跳登2
             var PublishType = $("#dataFormMasterPublishType").options('getValue');
             //種類
             var SalesTypeID = $("#dataFormMasterSalesTypeID").combobox('getValue');

             if (PublishType.trim() == "1") {                

                 //連登日期
                 var Check = $("#dataFormMasterContinueDate").datebox('getValue');
                 if (Check == "" || Check == undefined) {
                     alert('連登日期未選擇！');
                     return false;
                 }
                 //日期需大於今天日期
                 if (Check < today) {
                     alert('連登日期不可小於今天！');
                     return false;
                 }
                 
             } else {
                 //跳登日期
                 var Check = $("#dataFormMasterJumpDate").val();
                 if (Check == "" || Check == undefined) {
                     alert('跳登日期未選擇！');
                     return false;
                 }
                 //跳登起始日期需大於今天日期                 
                 var sJumpDate = $("#dataFormMasterJumpDate").val();
                 var arr = sJumpDate.split("\n");
                
                 //便利報(跳登)檢查是否為一、四
                 if (SalesTypeID.trim() == "31") {
                     var sErrorDate = "";
                     $.each(arr, function (index, value) {
                         if (value != "") {
                             var d = new Date(value);
                             //getDay()傳回值是一個0到6的整數值，0是星期日，1是星期一
                             if (d.getDay() != 1 && d.getDay() != 4) {
                                 sErrorDate = sErrorDate + value + ",";
                             }
                         }
                     });
                     if (sErrorDate != "") {
                         alert(sErrorDate + '日期錯誤=> 便利報的日期須為禮拜一或四！');
                         return false;
                     }
                 }
                 
                 if (arr[0] < today) {
                     alert('跳登起始日期不可小於今天！');
                     return false;
                 }
                 var arrCount = arr.length-1;//去掉\n空白
                 var Sum = parseInt($("#dataFormMasterPublishCount").val(), 0) + parseInt($("#dataFormMasterPresentCount").val(), 0);
                 if (arrCount != Sum) {
                     alert('跳登日期個數錯誤！');//刊期+贈期
                     return false;
                 }
             }             

             ////報紙6
             if (SalesTypeID.trim() == "6") {
                 //客行,社單價,社行,段
                 var sCheck = ['CustLines', 'OfficePrice', 'OfficeLines', 'Sections'];
                 var sError = "";
                 $.each(sCheck, function (index, fieldName) {
                     var Check = $("#dataFormMaster" + fieldName).val();
                     if (Check == "" || Check == undefined) {
                         sError = "尚有未填寫項目！";
                     }
                 });
                 //報,版,發
                 var sCheck2 = ['NewsTypeID', 'NewsAreaID', 'NewsPublishID'];
                 $.each(sCheck2, function (index, fieldName) {
                     var sCheck2 = $("#dataFormMaster" + fieldName).combobox('getValue');
                     if (sCheck2 == "" || sCheck2 == undefined) {
                         sError = "尚有未選擇項目！";
                     }
                 });
                 if (sError != "") {
                     alert(sError);
                     return false;
                 } else return true;
             } else {
                 //贈週報檢查
                 var PresentWNewsCount = $("#dataFormMasterPresentWNewsCount").val();
                 if (PresentWNewsCount != '') {
                     //贈週報日期需大於今天日期                 
                     var sWeekendDate = $("#dataFormMasterWeekendDate").val();
                     var arr = sWeekendDate.split("\n");
                     if (arr[0] < today) {
                         alert('贈週報日期不可小於今天！');
                         return false;
                     }
                     var arrCount = arr.length - 1;//去掉\n空白
                     if (arrCount != PresentWNewsCount) {
                         alert('贈週報日期個數錯誤！');
                         return false;
                     }
                 }
                 //總單位數檢查
                 var TotalSalesQty = parseInt($("#dataFormMasterTotalSalesQty").val(), 0);
                 var Sum = parseInt($("#dataFormMasterPublishCount").val(), 0) + parseInt($("#dataFormMasterPresentCount").val(), 0);
                 if (TotalSalesQty < Sum) {
                     alert('總單位數錯誤！');
                     return false;
                 }
                 //版別
                 var Check = $("#dataFormMasterDMTypeID").combobox('getValue');
                 if (Check == "" || Check == undefined) {
                     alert('版別未選擇！');
                     return false;
                 }
                 //版別區域
                 var Check2 = $("#dataFormMasterViewAreaID").combobox('getValue');
                 if (Check2 == "" || Check2 == undefined) {
                     alert('版別區域未選擇！');
                     return false;
                 }
                 return confirm("提醒您,請檢查總單位數。");
                 //alert('提醒您,請檢查總單位數。');
             }            
         }
         //************************************************繳社*****************************************************************************
         //=================================單價=>推總價========================================
         //發稿行數*繳社單價=>繳社總價
         function OnBlur_ChangeOfficePrice() {
             var OfficeLines = $("#dataFormMasterOfficeLines").val();
             var OfficePrice = $("#dataFormMasterOfficePrice").val();
             var OfficeAmt = Math.round(OfficeLines * OfficePrice);
             $("#dataFormMasterOfficeAmt").numberbox('setValue', OfficeAmt);//四捨五入      
             $("#dataFormMasterOfficeAmt").val(OfficeAmt);
         }
         //=================================總價=>推單價========================================
         //繳社總價/發稿行數=>繳社單價,若繳社單價已有數字=>則不用換算
         function OnBlur_ChangeOfficeAmt() {
             if ($("#dataFormMasterOfficePrice").val()=="") {
                 var OfficeLines = $("#dataFormMasterOfficeLines").val();
                 var OfficeAmt = $("#dataFormMasterOfficeAmt").val();
                 var OfficePrice = formatFloat(OfficeAmt / OfficeLines,3);//小數點第3位四捨五入
                 $("#dataFormMasterOfficePrice").numberbox('setValue', OfficePrice);     
                 $("#dataFormMasterOfficePrice").val(OfficePrice);
             }
         }
         //第一個參數num是帶有小數的變數，第二個參數pos是要取小數後的幾位數。
         //Math.pow(x,y);x -- 為number型態 y -- 為number型態返回值為x的y次方。如果pow的參數過大而引起浮點溢出，返回Infinity
         function formatFloat(num, pos) {
             var size = Math.pow(10, pos);
             return Math.round(num * size) / size;
         }
         //************************************************客戶*****************************************************************************
         //=================================單價=>推總價========================================
         //客行*客單價=>客總價
         function OnBlur_ChangeCustPrice() {
             var CustLines = $("#dataFormMasterCustLines").val();
             var CustPrice = $("#dataFormMasterCustPrice").val();
             var CustAmt = Math.round(CustLines * CustPrice);
             $("#dataFormMasterCustAmt").numberbox('setValue', CustAmt);
             $("#dataFormMasterCustAmt").val(CustAmt);
         }
         //客總價/客行=客單價,若客單價已有數字=>則不用換算
         function OnBlur_ChangeCustAmt() {
             if ($("#dataFormMasterCustPrice").val() == "") {
                 var SalesTypeID = $('#dataFormMasterSalesTypeID').combobox('getValue');// 交易別
                 if (SalesTypeID.trim() == "6") {
                     var CustLines = $("#dataFormMasterCustLines").val();
                     var CustAmt = $("#dataFormMasterCustAmt").val();
                     var CustPrice = formatFloat(CustAmt / CustLines, 3);//
                     $("#dataFormMasterCustPrice").numberbox('setValue', CustPrice);
                     $("#dataFormMasterCustPrice").val(CustPrice);
                 }
             }
         }
         //(刊期+贈期)*單位數=總單位數
         function OnBlur_TotalSalesQty() {
             //(刊期+贈期)*單位數=總單位數
             var SalesTypeID = $('#dataFormMasterSalesTypeID').combobox('getValue');// 交易別
                 var PublishCount = parseInt($("#dataFormMasterPublishCount").val());
                 var PresentCount = parseInt($("#dataFormMasterPresentCount").val());
                 var SalesQty = parseInt($("#dataFormMasterSalesQty").val());
                 var sum = (PublishCount + PresentCount) * SalesQty;
                 $("#dataFormMasterTotalSalesQty").numberbox('setValue', sum);
             //單位數填完帶入見刊
                 $("#dataFormMasterSalesQtyView").numberbox('setValue', SalesQty);
         }         
         //天數提醒(主檔),是否失效(明細)CheckBox=>不可以編輯
         function genCheckBox(val) {
             if (val != "0")
                 return "<input  type='checkbox' checked='true' onclick='return false;'/>";
             else
                 return "<input  type='checkbox' onclick='return false;'/>";
         }         
         //當選取版別時,重新設定區域(連動)         
         function GetViewAreaID(rowData) {
             //$("#dataFormMasterViewAreaID").combobox('setValue', "");
             $("#dataFormMasterViewAreaID").combobox('setWhere', "DMTypeID='" + rowData.DMTypeID + "'");
             setTimeout(function () {
                 var data = $("#dataFormMasterViewAreaID").combobox('getData');
                 var setvalue = data[0].ViewAreaID;
                 if (data.length > 0) {
                     $("#dataFormMasterViewAreaID").combobox('setValue', setvalue);
                 }
             }, 500);             
         }        
         //=============================================Master Grid近期銷貨清單=========================================================================================         
         //更新近期客戶清單
         function RefreshGrid() {             
            $('#dataGridDetail').datagrid('loadData', []); //銷貨明細清空資料
            var SalesEmployeeID = $("#cbSalesEmployeeID").combobox('getValue');
            var CustNO = $("#cbCustNO").combobox('getValue');
            var SalesType = $("#cbSalesType").combobox('getValue');
            var JQDate1 = $("#JQDate1").combo('textbox').val();//datebox("getBindingValue");//datebox("getValue");                
            var JQDate2 = $("#JQDate2").combo('textbox').val();//datebox("getBindingValue");
            var where = $("#dataGridView").datagrid('getWhere');
            where = where + " d.SalesID='" + SalesEmployeeID + "' and d.SalesDate between '" + JQDate1 + "' and '" + JQDate2 + "'";
            if (CustNO != "==不拘==" && CustNO != "") {
                where = where + " and m.CustNO='" + CustNO + "'";
            }
            if (SalesType != "") {
                where = where + " and m.SalesTypeID='" + SalesType + "'";
            }
            $("#dataGridView").datagrid('setWhere', where);          
         }          
         //主檔過濾條件
         function MastersetWhere() {
             if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {                 
                 //1=0做完
                 waitA = true;
             }
             ////銷貨明細維護後把索引指向修改客戶
             //if ($("div").data("dataGridViewIndex") != undefined) {
             //    var index = $("div").data("dataGridViewIndex");                 
             //    var rowData = $("div").data("dataGridViewData");
             //    $("#dataGridView").datagrid("selectRow", index, rowData.SalesTypeID);
             //    $("#dataGridView").datagrid('setCurrentRow', rowData);
             //}
             //控制複製銷貨,銷貨修改顯示
             var data = $("#dataGridView").datagrid('getData');
             if (data.total > 0) {
                 $("#toolItemdataGridView複製銷貨").show();
                 $("#toolItemdataGridView訂單修改").show();                
             } else {
                 $("#toolItemdataGridView複製銷貨").hide();
                 $("#toolItemdataGridView訂單修改").hide();
             }                                             
         }
         //依據選取的客戶更新Detail明細,顯示或隱藏DataGrid Item
         function dataGridViewSelect(rowindex, rowdata) {                                      
             RefreshDetail(rowdata);
             var SalesTypeID = rowdata.SalesTypeID;//交易別
             ControlGridItem(SalesTypeID);//顯示或隱藏DataGrid Item               
         }        
         //顯示或隱藏DataGrid Item
         function ControlGridItem(SalesTypeID) {
             //版別,區域,贈期,單位數,見刊         
             var HideFieldName = ['DMTypeID', 'ViewAreaID', 'GrantTypeID', 'SalesQty', 'SalesQtyView'];
             //報,版,發,段,社單價,社行,繳社價,客行
             var HideFieldName2 = ['NewsTypeID', 'NewsAreaID', 'NewsPublishID', 'Sections', 'OfficePrice', 'OfficeLines', 'OfficeAmt', 'CustLines'];
            
             if (SalesTypeID == "6")//報紙
             {
                 $.each(HideFieldName, function (index, fieldName) {
                     $("#dataGridDetail").datagrid('hideColumn', fieldName);
                 });
                 $.each(HideFieldName2, function (index, fieldName2) {
                     $("#dataGridDetail").datagrid('showColumn', fieldName2);
                 });                               
             } else {
                 $.each(HideFieldName, function (index, fieldName) {
                     $("#dataGridDetail").datagrid('showColumn', fieldName);
                 });
                 $.each(HideFieldName2, function (index, fieldName2) {
                     $("#dataGridDetail").datagrid('hideColumn', fieldName2);
                 });                 
             }
         }
         //Grid近期銷貨清單=>剩餘數提醒
         function FormatScriptUseQty(val, rowData) {
             if (val < 0) {
                 return "<div style='width: 30px; border: solid 1px red;'> " + val + "</div>";
             } else {
                 return "<div style='width: 30px; border: solid 1px blue;'> " + val + "</div>";
             }
         }
         //Grid近期銷貨清單=>已開發票提醒
         function FormatScriptIsInvoice(val, rowData) {             
             if (rowData.IsInvoice >0) {
                 return "<div style='font-weight:bold;color:red;'> " + val + "</div>";
             } else {
                 return "<div style='color:black;'> " + val + "</div>";
             }
         }
         //主檔銷貨修改
         function UpdateSalesMaster() {
             //呼叫視窗修改
             openForm('#JQDialog2', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
         }                
         //=============================================新增銷貨明細=========================================================================================
         //新增明細時取得CustNO
         function GetCustNO() {
             var row = $('#dataGridView').datagrid('getSelected');
             return row.CustNO;
         }
        //新增明細時取得SalesMasterNO
        function GetSalesMasterNO() {
            var row = $('#dataGridView').datagrid('getSelected');
            return row.SalesMasterNO;
        }        
         //************************************************繳社*****************************************************************************
         //=================================單價=>推總價========================================
         //發稿行數*繳社單價=>繳社總價
        function OnBlur_SalesDescrOfficePrice() {
            var OfficeLines = $("#dataFormSalesDescrOfficeLines").val();//numberbox('getValue');
            var OfficePrice = $("#dataFormSalesDescrOfficePrice").val(); //numberbox('getValue');
            var OfficeAmt=Math.round(OfficeLines * OfficePrice);
            $("#dataFormSalesDescrOfficeAmt").numberbox('setValue', OfficeAmt);//四捨五入
            $("#dataFormSalesDescrOfficeAmt").val(OfficeAmt);//.numberbox('setValue', Math.round(OfficeLines * OfficePrice));//四捨五入
        }
         //=================================總價=>推單價========================================
         //繳社總價/發稿行數=>繳社單價
        function OnBlur_SalesDescrOfficeAmt() {
            var OfficeLines = $("#dataFormSalesDescrOfficeLines").val();
            var OfficeAmt = $("#dataFormSalesDescrOfficeAmt").val();
            var OfficePrice = Math.round(OfficeAmt / OfficeLines);
            $("#dataFormSalesDescrOfficePrice").numberbox('setValue', OfficePrice);
            $("#dataFormSalesDescrOfficePrice").val(OfficePrice);
        }

        //************************************************客戶*****************************************************************************
        //=================================單價=>推總價========================================
        //求才=>單位數*客單價=>客總價
        //報紙=>客戶行數*客戶單價=>客總價
        function OnBlur_SalesDescrCustPrice() {
            var SalesTypeID = $('#dataFormSalesDescrSalesTypeID').combobox('getValue');// 交易別
            if (SalesTypeID.trim() == "1") {
                var SalesQty = $("#dataFormSalesDescrSalesQty").val();
                var CustPrice = $("#dataFormSalesDescrCustPrice").val();
                var CustAmt = Math.round(SalesQty * CustPrice);//四捨五入
            } else {//報紙
                var CustLines = $("#dataFormSalesDescrCustLines").val();
                var CustPrice = $("#dataFormSalesDescrCustPrice").val();
                var CustAmt = Math.round(CustLines * CustPrice);
            }
            $("#dataFormSalesDescrCustAmt").numberbox('setValue', CustAmt);
            $("#dataFormSalesDescrCustAmt").val(CustAmt);
        }
        //=================================總價=>推單價(報紙)========================================
        function OnBlur_SalesDescrCustAmt() {
            var SalesTypeID = $('#dataFormSalesDescrSalesTypeID').combobox('getValue');// 交易別
            if (SalesTypeID.trim() == "6") {
                //報紙
                var CustLines = $("#dataFormSalesDescrCustLines").val();
                var CustAmt = $("#dataFormSalesDescrCustAmt").val();
                var CustPrice = Math.round(CustAmt / CustLines);
                $("#dataFormSalesDescrCustPrice").numberbox('setValue', CustPrice);
                $("#dataFormSalesDescrCustPrice").val(CustPrice);
            }
        }
         //更新銷貨明細
        function RefreshDetail(row) {            
            if (row != null) {
                var where = $("#dataGridDetail").datagrid('getWhere');
                where = where + "d.SalesMasterNO=" + row.SalesMasterNO;//"SalesMasterNO=" + row.SalesMasterNO + "and
                $("#dataGridDetail").datagrid('setWhere', where);
            } 
            else {
                $('#dataGridDetail').datagrid('loadData', []); //銷貨明細清空資料                
            }
        }
                
        //明細過濾條件
        function DetailsetWhere() {
            //明細Grid選取單選,checkbox多選
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                $(this).datagrid({
                    singleSelect: true,
                    selectOnCheck: false,
                    checkOnSelect: false
                });
    
                //1=0做完
                waitB = true;                                                
            }
            //補銷貨明細,銷貨明細維護 顯示
            var row = $('#dataGridView').datagrid('getSelected');
            if (row != null) {
                $("#toolItemdataGridDetail銷貨明細修改").show();
                $("#toolItemdataGridDetail銷貨失效").show();
                var rowdata = $('#dataGridView').datagrid('getSelected');
                if (rowdata.UseQty <= 0) {
                    $("#toolItemdataGridDetail補銷貨明細").hide();
                } else $("#toolItemdataGridDetail補銷貨明細").show();

                //有權限才可以刪除或匯入銷貨
                if (GetLoginID()) {
                    $("#toolItemdataGridDetail銷貨匯入").show();
                } else {
                    $("#toolItemdataGridDetail銷貨匯入").hide();
                }
            } else {
                $("#toolItemdataGridDetail補銷貨明細").hide();
                $("#toolItemdataGridDetail銷貨明細修改").hide();
                $("#toolItemdataGridDetail銷貨失效").hide();
                $("#toolItemdataGridDetail銷貨匯入").hide();
            }          
        }
         //銷貨明細修改後更新
        function DetailSaveRefresh() {            
            //RefreshGrid();//更新主檔  
            $('#dataGridDetail').datagrid("reload");
            ShowToDoCount();//代辦事項筆數
            RefreshSalesInfo();//更新資訊統計
        }
        
         //=============================================修改銷貨明細=========================================================================================     
        function UpdateSalesDescr() {
            //呼叫視窗修改
            openForm('#Dialog_SalesDescr', $('#dataGridDetail').datagrid('getSelected'), "updated", 'dialog');
        }
         //修改銷貨明細SalesDate檢查=>原本日期>今天的才要檢查=>新的日期不可以<=今天(有權限者都可以修改)
        function validateDetailSalesDate(SalesDate) {//新日期
            var row = $("#dataGridDetail").datagrid('getSelected');
            var date = new Date();
            var now = $.jbjob.Date.DateFormat(date, 'yyyy/MM/dd');//現在日期
            var date2 = new Date(row.SalesDate);
            var oldSalesDate = $.jbjob.Date.DateFormat(date2, 'yyyy/MM/dd');//原本日期
            if (oldSalesDate > now && SalesDate <= now) return false;
            else
                return true;
        }
        //銷貨明細維護新增完成
        function OnAppliedDFSalesDescr() {                     
            //更新明細
            $('#dataGridDetail').datagrid("reload");
            //代辦事項筆數
            ShowToDoCount();
            //更新資訊統計
            $('#dataGridSalesInfo').datagrid("reload");
            //得到主檔選取的資料列
            var rowData = $('#dataGridView').datagrid('getSelected');
            $("div").data("dataGridViewData", rowData);//把主檔選取的索引添加到div元素
                       
            var index = $('#dataGridView').datagrid('getRowIndex', rowData);
            $("div").data("dataGridViewIndex", index);//把主檔選取的索引添加到div元素
            //更新主檔            
            //$('#dataGridView').datagrid("reload");            
        }
        //銷貨明細Grid勾選失效
        function GridCheckAllFalse() {
            if ($("#dataGridDetail").datagrid('getChecked').length == 0) {
                alert('請勾選銷貨項目。');
            } else {
                var pre = confirm("確定失效?");
                if (pre == true) {
                    var rows = $('#dataGridDetail').datagrid('getChecked');
                    var aItemSeq = [];//銷貨序號
                    var adepositSeq = [];//匯入序號
                    var SalesMasterNO = "";
                    var CustNO = "";
                    for (var i = 0; i < rows.length; i++) {
                        if (i == 0) {
                            SalesMasterNO = rows[0].SalesMasterNO;
                            CustNO = rows[0].CustNO;
                        }
                        aItemSeq.push(rows[i].ItemSeq);
                        adepositSeq.push(rows[i].depositSeq);
                    }
                    var sItemSeq = aItemSeq.join('*');
                    var sdepositSeq = adepositSeq.join('*');
                    //有權限才可以失效匯入的銷貨
                    var sType = "";
                    if (GetLoginID()) { sType = "Admin" };
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetails.ERPSalesDetails', //連接的Server端，command
                        data: "mode=method&method=" + "UpdateSalseDetailsIsActive" + "&parameters=" + SalesMasterNO + "," + sItemSeq + "," + CustNO + "," + sdepositSeq + "," + sType, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            alert('銷貨失效成功！');
                            $('#dataGridDetail').datagrid('loadData', []); //銷貨明細清空資料 
                            //更新主檔            
                            $('#dataGridView').datagrid("reload");
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            alert(xhr.status);
                            alert(thrownError);
                        }
                    });
                                                        
                }
            }
        }
        //銷貨明細選擇性匯入行政系統
        function ImportSalesDetails() {
            if ($("#dataGridDetail").datagrid('getChecked').length == 0) {
                alert('請勾選銷貨項目。');
            } else {
                var pre = confirm("確定匯入行政系統?");
                if (pre == true) {
                    var rows = $('#dataGridDetail').datagrid('getChecked');
                    var aSalesDetails = [];
                    var SalesMasterNO = "";
                    for (var i = 0; i < rows.length; i++) {
                        if (i == 0) {
                            SalesMasterNO = rows[0].SalesMasterNO;
                        }
                        aSalesDetails.push(rows[i].ItemSeq);
                    }
                    var sItemSeq = aSalesDetails.join('*');
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetails.ERPSalesDetails', //連接的Server端，command
                        data: "mode=method&method=" + "ImportSalesDetails" + "&parameters=" + SalesMasterNO + "," + sItemSeq, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            alert('銷貨匯入行政系統成功！');
                            $('#dataGridDetail').datagrid("reload");
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            alert(xhr.status);
                            alert(thrownError);
                        }
                    });
                }
            }
        }
        //EditOnEnter銷貨明細檢查
        function DetailUpdateCheck() {
            var row = $("#dataGridDetail").datagrid('getSelected');          
            var IsTransSys = row.IsTransSys;
            var dt = new Date();
            var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd');            
            var aDate = new Date(row.SalesDate);
            var SalesDate = $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
            //未匯入且銷貨日期大於今天的可修改,  已匯入=>有權限者改
            //if (GetLoginID() || (IsTransSys == "0" &&  SalesDate > today)) {
            //未匯入的可修改,  已匯入=>有權限者改
            if (GetLoginID() || (IsTransSys==false &&  SalesDate > today)) {
                return true;
            } else {//未轉入且銷貨日期小於今天=>可修改 有效,發票年月,銷貨日期=>翠玲改
                var index = $("#dataGridDetail").datagrid('getRowIndex', row);
                if (index != undefined) {
                    $("#dataGridDetail").datagrid('selectRow', index).datagrid('beginEdit', index);
                    //var cellEdit = $("#dataGridDetail").datagrid('getEditor', { index: index, field: 'SalesQtyView' });
                    var cells = $("#dataGridDetail").datagrid('getEditors', index);
                    $.each(cells, function (index, obj) {
                        if (obj.field != "ViewAreaID" && obj.field != "SalesDescr" && obj.field != "ContractDescr" && obj.field != "Commission" && obj.field != "SalesDescrDate") {
                            switch (obj.type) {
                                case "text": $(obj.target).attr("disabled", "disabled");
                                    break;
                                case "validatebox": $(obj.target).attr("disabled", "disabled");
                                    break;
                                case "datebox": $(obj.target).datebox("disable");
                                    break;
                                case "infocombogrid": $(obj.target).combogrid('disable');
                                    break;
                                case "numberbox": $(obj.target).numberbox('disable');
                                    break;
                                case "checkbox": $(obj.target).attr('disabled', "disabled");
                                    break;
                                default:
                                    break;
                            }
                        }
                    });
                }
            }
        }
        //檢查字串是否符合發票年月
        function CheckStrWildWord(str) {
            var r = str.match(/^(\d{4})(\/)(0[1-9]|1[0-2])$/);
            if (r == null) return false;
            var d = new Date(r[1], (r[3] - 1), 1);
            return (d.getFullYear() == r[1] && d.getMonth() == (r[3] - 1) && d.getDate() == 1);
        }
         //顯示或隱藏DataForm Item
        //依據交易別顯示隱藏 求才或報紙 選項
        function ControlDataFormItem(SalesTypeID) {
            var FormName = '#dataFormSalesDescr';
            //顯示項目=>報,版,發,段,社單價,社行,客行,繳社價//,客總價
            var HideFieldName = ['NewsTypeID', 'NewsAreaID', 'NewsPublishID', 'Sections', 'OfficePrice', 'OfficeLines', 'CustLines', 'OfficeAmt'];//, 'CustAmt'
            //隱藏項目=>單位數,見刊,版別,區域
            var HideFieldName2 = ['SalesQty', 'SalesQtyView', 'DMTypeID', 'ViewAreaID'];
            //報紙6=>顯示
            if (SalesTypeID.trim() == "6") {
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').css("color", "red");
                    $(FormName + fieldName).closest('td').prev('td').css("color", "red");
                    $(FormName + fieldName).closest('td').prev('td').show();
                    $(FormName + fieldName).closest('td').show();
                });
                $.each(HideFieldName2, function (index, fieldName2) {
                    $(FormName + fieldName2).closest('td').prev('td').hide();
                    $(FormName + fieldName2).closest('td').hide();
                });               
            } else {//求才1=>隱藏
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').hide();
                    $(FormName + fieldName).closest('td').hide();                    
                });
                $.each(HideFieldName2, function (index, fieldName2) {
                    $(FormName + fieldName2).closest('td').css("color", "red");
                    $(FormName + fieldName2).closest('td').prev('td').css("color", "red");
                    $(FormName + fieldName2).closest('td').prev('td').show();
                    $(FormName + fieldName2).closest('td').show();
                });               
                //依據預設的版別=>區域做連動
                var DMTypeID = $('#dataFormSalesDescrDMTypeID').combobox('getValue');// 版別
                var ViewAreaID = $('#dataFormSalesDescrViewAreaID').combobox('getValue');// 版別
                $("#dataFormSalesDescrViewAreaID").combobox('setValue', "");
                $("#dataFormSalesDescrViewAreaID").combobox('setWhere', "DMTypeID='" + DMTypeID + "'");
                $("#dataFormSalesDescrViewAreaID").combobox('setValue', ViewAreaID);
            }           
        }
         //控制DataForm Item的可否編輯
         //銷貨日期大於等於今天的可修改,  銷貨日期小於今天=>翠玲改
        function ControlDataFormItem2(SalesTypeID) {                      
            var dt = new Date();
            var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd');            
            var aDate = new Date($('#dataFormSalesDescrSalesDate').datebox('getValue'));
            var SalesDate = $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');

            var FormName = '#dataFormSalesDescr';
            //項目=>版別,版別區域,贈期,報,版,發
            var HideFieldName = ['DMTypeID', 'ViewAreaID', 'GrantTypeID', 'NewsTypeID', 'NewsAreaID', 'GrantTypeID'];

            var IsTransSys = $("#dataFormSalesDescrIsTransSys").checkbox('getValue');

            //銷貨日期大於今天的可修改,  銷貨日期小於今天=>翠玲改,其他隱藏
            //if ((SalesDate <= today && GetLoginID()) || SalesDate > today) {
            //未匯入且銷貨日期大於今天的可修改,  已匯入=>有權限者改
            if (GetLoginID() || (IsTransSys == "0" &&  SalesDate > today)) {

                //解鎖textarea型態欄位
                $('input,select', "#dataFormSalesDescr").each(function () {//input,select,
                    this.disabled = '';
                });
                //combobox解鎖 
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).combobox("enable");
                });
                //銷貨日期可用
                $('#dataFormSalesDescrSalesDate').datebox('enable');   

                if (IsTransSys == 1) {
                    alert('提醒您，此筆銷貨已匯入行政系統\n請記得到 "行政系統" 修改銷貨資料。');
                }
            } else {
                //鎖textarea型態欄位
                $('input,select', "#dataFormSalesDescr").each(function () {//input,select,
                    this.disabled = 'disabled';
                });
                //combobox鎖定           
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).combobox("disable");             
                });

                //銷貨日期不可用
                $('#dataFormSalesDescrSalesDate').datebox('disable');
                
                //$('#dataFormSalesDescrCommission').disabled = '';//佣金可用
                $('#dataFormSalesDescrSalesDescrDate').datebox('enable');//提醒日期可用
                $('#dataFormSalesDescrViewAreaID').combobox('enable');//區域可用(無對應到行政系統)                   
            }
            //客戶代號,客戶簡稱,匯入鎖定           
            $("#dataFormSalesDescrCustNO").prop('readonly', true);
            $("#dataFormSalesDescrCustShortName").prop('readonly', true);
            $("#dataFormSalesDescrIsTransSys").attr("disabled", true);
        }
         //銷貨明細維護OnLoadSuccess
        function OnLoadSuccessDFSalesDescr() {
            var SalesTypeID = $('#dataFormSalesDescrSalesTypeID').combobox('getValue');// 交易別
            ControlDataFormItem(SalesTypeID.trim());
            ControlDataFormItem2(SalesTypeID.trim());
        }
         //當選取版別時,重新設定區域(連動)
        function GetSalesDescrViewAreaID(rowData) {
            $("#dataFormSalesDescrViewAreaID").combobox('setValue', "");
            $("#dataFormSalesDescrViewAreaID").combobox('setWhere', "DMTypeID='" + rowData.DMTypeID + "'");
        }
       
         //=============================================(補單)銷貨明細新增=========================================================================================
        //呼叫視窗新增
        function OpenInsertSalesDetailsLast() {
            openForm('#Dialog_SalesDetails', {}, "inserted", 'dialog');//
        }       
         //Load
        function AddSalesDetailsLoad() {
            //刊登日期跳登註冊            
            $("#dataFormSalesDetailJumpDate").jbDateBoxMultiple({});
            //週報日期跳登註冊            
            $("#dataFormSalesDetailJumpDate2").jbDateBoxMultiple({});
        }
        function OnLoadSuccessSalesDetail() {            
            //清空                              
            $("#dataFormSalesDetailJumpDate").jbDateBoxMultiple("setData");
            $("#dataFormSalesDetailJumpDate2").jbDateBoxMultiple("setData");
        }       
        //當選取版別時,重新設定區域(連動)
        function GetSalseDetailsViewAreaID(rowData) {
            $("#dataFormSalesDetailViewAreaID").combobox('setWhere', "DMTypeID='" + rowData.DMTypeID + "'");
            setTimeout(function () {
                var data = $("#dataFormSalesDetailViewAreaID").combobox('getData');
                var setvalue = data[0].ViewAreaID;
                if (data.length > 0) {
                    $("#dataFormSalesDetailViewAreaID").combobox('setValue', setvalue);
                }
            }, 500);
        }
         //單位數填完帶入見刊
        function OnBlur_SalesQty() {
            var SalesQty = parseInt($("#dataFormSalesDetailSalesQty").val());
            $("#dataFormSalesDetailSalesQtyView").numberbox('setValue', SalesQty);
        }
         //新增銷貨明細(補單)     
        function InsertSalesDetailsLast() {

            if ($('#dataFormSalesDetail').form('validateForm')) {
                //結帳日期
                var YMPoint = $("#dataFormSalesDetailInvoiceYMPoint").datebox("getValue");
                //版別
                var DMTypeID = $("#dataFormSalesDetailDMTypeID").combobox('getValue');
                //區域
                var ViewAreaID = $("#dataFormSalesDetailViewAreaID").combobox('getValue');
                //單位數	
                var SalesQty = $("#dataFormSalesDetailSalesQty").val();
                //見刊	
                var SalesQtyView = $("#dataFormSalesDetailSalesQtyView").val();
                //備註	
                var SalesDescr = $("#dataFormSalesDetailSalesDescr").val();
                //PDF檔名	
                var Remark1 = $("#dataFormSalesDetailRemark1").val();
                //跳登起始日期需大於今天日期                 
                var sJumpDate = $("#dataFormSalesDetailJumpDate").val();
                var dt = new Date();
                var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')
                var arr = sJumpDate.split("\n");
                var sJumpDate2 = $("#dataFormSalesDetailJumpDate2").val();
                var arr2 = sJumpDate2.split("\n");

                if (GetLoginID() == false && (arr != "" && arr[0] <= today || (arr2 != "" && arr2[0] <= today))) {//有權限者才可以補之前銷貨
                    alert('日期需大於今天！');
                } else {
                    var SalesMasterNO = $('#dataGridView').datagrid('getSelected').SalesMasterNO;
                    var CustNO = $('#dataGridView').datagrid('getSelected').CustNO;
                    //檢查新增銷貨明細(補單)
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetails.ERPSalesDetails', //連接的Server端，command
                        data: "mode=method&method=" + "CheckERPSalseDetailsLast" + "&parameters=" + SalesMasterNO + "," + sJumpDate,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                            if (rows.length > 0) {
                                if (rows[0].checkCount < 0) {
                                    alert('日期個數錯誤！');
                                } else {
                                    if (rows[0].sAlertDate != "") {
                                        alert('提醒您,銷貨日期:' + rows[0].sAlertDate + "目前已存在！");
                                    }
                                    //新增銷貨明細(補單)
                                    $.ajax({
                                        type: "POST",
                                        url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetails.ERPSalesDetails', //連接的Server端，command
                                        data: "mode=method&method=" + "InsertERPSalseDetailsLast" + "&parameters=" + SalesMasterNO + "," + CustNO + "," + DMTypeID + "," + ViewAreaID + "," + SalesQty + "," + SalesQtyView + "," + SalesDescr + "," + Remark1 + "," + YMPoint + "," + sJumpDate + "," + sJumpDate2,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                                        cache: false,
                                        async: false,
                                        success: function (data) {
                                            RefreshGrid();
                                            RefreshSalesInfo();//更新資訊統計
                                            closeForm('#Dialog_SalesDetails');
                                        }
                                    });
                                }
                            }
                        }
                    });

                }
            }
        }
        function EditCheckBox(ckbox) {
            UpdateSalseDetailsIsActive(ckbox);
        }
        //天數提醒(主檔),是否失效(明細)CheckBox=>可編輯
        function genCheckBoxEdit(val) {
            if (val != "0")
                return "<input id='GG' type='checkbox' checked='true' onclick='EditCheckBox(this);'/>";
            else
                return "<input  type='checkbox' />";
        }
        //銷貨明細失效Details檢查
        function UpdateSalseDetailsIsActive(ckbox) {
            var SalesDate = $('#dataGridDetail').datagrid('getSelected').SalesDate;           
            var dt = new Date();
            var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd');            
            var aDate = new Date($('#dataGridDetail').datagrid('getSelected').SalesDate);
            var SalesDate = $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');            

            //銷貨日期大於等於今天的可修改,  銷貨日期小於今天=>翠玲改
            if ((SalesDate < today && GetLoginID()) || SalesDate >= today ) {
                var pre = confirm("確認失效此筆銷貨?");
                if (pre == true) {
                    var SalesMasterNO = $('#dataGridDetail').datagrid('getSelected').SalesMasterNO;
                    var ItemSeq = $('#dataGridDetail').datagrid('getSelected').ItemSeq;
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetails.ERPSalesDetails', //連接的Server端，command
                        data: "mode=method&method=" + "UpdateSalseDetailsIsActive" + "&parameters=" + SalesMasterNO + "," + ItemSeq , //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: true,
                    });
                    RefreshGrid();
                    if ($('#dataGridDetail').datagrid('getSelected').IsTransSys == true) {
                        alert('提醒您，此筆銷貨已匯入行政系統\n請記得到 "行政系統" 修改銷貨資料。');
                    }
                    return false; //取消失效的動作
                }
                else {                    
                    $(ckbox).attr("checked", true);                    
                    return false;
                }
            } else {
                alert('銷貨日期已過期故無法失效');
                return false; //取消失效的動作
            }
        }           
         //=============================================更新資訊統計=========================================================================================
         //setwhere
        function InfoLoadSuccess() {
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                //1=0做完
                waitC = true;
            }
        }
        function RefreshSalesInfo() {            
            var SalesEmployeeID = $("#cbSalesEmployeeID").combobox('getValue');
            var where = $("#dataGridSalesInfo").datagrid('getWhere');
            var Date1 = $("#JQDate1").combo('textbox').val();               
            var Date2 = $("#JQDate2").combo('textbox').val();
            var CustNO = $("#cbCustNO").combobox('getValue');
            var SalesType = $("#cbSalesType").combobox('getValue');
            if (where.length > 0) {
                where = where + " and d.SalesID='" + SalesEmployeeID + "' and d.SalesDate between '" + Date1 + "' and '" + Date2 + "'";
            }
            else {
                where = "d.SalesID='" + SalesEmployeeID + "' and d.SalesDate between '" + Date1 + "' and '" + Date2+"'";
            }
            if (CustNO != "==不拘==" && CustNO != "") {
                where = where + " and d.CustNO='" + CustNO + "'";
            }
            if (SalesType != "") {
                where = where + " and SalesTypeID='" + SalesType + "'";
            }
            $("#dataGridSalesInfo").datagrid('setWhere', where);
            //拿掉項次
            $('#dataGridSalesInfo').datagrid({ rownumbers: false });
        }                    
         //=============================================複製主檔Dialog=========================================================================================
        //呼叫視窗銷貨明細新增
        function OpenCopySalesDetails() {
            //客戶代號	
            var CustNO = $('#dataGridView').datagrid('getSelected').CustNO;
            //交易別
            var SalesTypeID = $('#dataGridView').datagrid('getSelected').SalesTypeID;
            //當選取版別時,重新設定區域(連動)
            var DMTypeID = $('#dataGridView').datagrid('getSelected').DMTypeID;
            var ViewAreaID = $('#dataGridView').datagrid('getSelected').ViewAreaID;
            //總單位數
            var TotalSalesQty = $('#dataGridView').datagrid('getSelected').TotalSalesQty;
            //選取的那筆進行複製
            var row = $('#dataGridDetail').datagrid('getSelected');
            openForm('#JQDialog1', row, "inserted", 'dialog');

            //openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "inserted", 'dialog');
            //設定客戶代號,交易別
            $("#dataFormMasterCustNO").combobox('setValue', CustNO);
            $("#dataFormMasterSalesTypeID").combobox('setValue', SalesTypeID);
            ControlShowItem();
            //設定版別,交易別
            $("#dataFormMasterDMTypeID").combobox('setValue', DMTypeID);
            $("#dataFormMasterViewAreaID").combobox('setValue', "");
            $("#dataFormMasterViewAreaID").combobox('setWhere', "DMTypeID='" + DMTypeID + "'")
            $("#dataFormMasterViewAreaID").combobox('setValue', ViewAreaID);
            //報別,版別,發刊單位
            $("#dataFormMasterNewsTypeID").combobox('setValue', row.NewsTypeID);
            $("#dataFormMasterNewsAreaID").combobox('setValue', row.NewsAreaID);
            $("#dataFormMasterNewsPublishID").combobox('setValue', row.NewsPublishID);
            //報紙時=>總單位數預設為0
            if (SalesTypeID == "6") {
                $("#dataFormMasterTotalSalesQty").val(TotalSalesQty);
            }
        }
      
     </script> 
    </head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="客戶代號:"></asp:Label>
            <JQTools:JQComboBox ID="cbCustNO" runat="server" DisplayMember="CustShortName" RemoteName="sERPSalseDetails.infoCustomersAll" ValueMember="CustNO" OnSelect="cbCustNORefresh">
            </JQTools:JQComboBox>           
            &nbsp; <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="業務:"></asp:Label>
            <JQTools:JQComboBox ID="cbSalesEmployeeID" runat="server" DisplayMember="SalesName" PanelHeight="150" RemoteName="sERPSalseDetails.infoSalesMan" ValueMember="SalesID" Width="50px" OnSelect="cbSalesEmployeeIDRefresh">
            </JQTools:JQComboBox>
&nbsp;<asp:Label ID="Label4" runat="server" Font-Size="Small" Text="交易別:"></asp:Label>
            <JQTools:JQComboBox ID="cbSalesType" runat="server" DisplayMember="SalesTypeName" RemoteName="sERPSalseDetails.infoERPSalesType" ValueMember="SalesTypeID" OnSelect="cbSalesTypeRefresh">
            </JQTools:JQComboBox>
            <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="起訖日期:"></asp:Label>
            <JQTools:JQDateBox ID="JQDate1" runat="server" Width="100px" />
            〜<JQTools:JQDateBox ID="JQDate2" runat="server" />
            &nbsp;&nbsp;<a href="#" id="LinkToDoList" onclick="GoToDoList()"></a>
            &nbsp;<a href="#" class="easyui-linkbutton" OnClick="OpenAcceptDateList()">預收查詢</a>
            <br />
        </div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />

            <script src="../js/jquery.calendar_jb.js"></script>
           

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="新增銷貨" EditMode="Dialog" DialogLeft="30px" DialogTop="10px" Width="850px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPSalesMaster" HorizontalColumnsCount="4" RemoteName="sERPSalseDetails.ERPSalesMaster" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" OnLoadSuccess="MasterOnLoadSuccess" OnApply="checkItemNull" OnApplied="OnAppliedSalesDetails" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesMasterNO" Editor="text" FieldName="SalesMasterNO" Width="80" Visible="False" Span="1" NewRow="False" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsConvertNexMonth" NewRow="True" Span="4" Visible="True" Width="40" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="infocombobox" FieldName="CustNO" Width="190" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sERPSalseDetails.infoCustomers',tableName:'infoCustomers',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectCustNO,panelHeight:200" Span="1" NewRow="True" Visible="True" OnBlur="" MaxLength="0" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="交易別" Editor="infocombobox" FieldName="SalesTypeID" Width="90" Visible="True" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalseDetails.infoERPSalesType',tableName:'infoERPSalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:ControlShowItem,panelHeight:200" NewRow="False" Span="1" ReadOnly="False" MaxLength="0" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="版別" Editor="infocombobox" FieldName="DMTypeID" Width="90" Visible="True" EditorOptions="valueField:'DMTypeID',textField:'DMTypeName',remoteName:'sERPSalseDetails.infoERPDMType',tableName:'infoERPDMType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetViewAreaID,panelHeight:200" NewRow="False" ReadOnly="False" MaxLength="0" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" 區域" Editor="infocombobox" EditorOptions="valueField:'ViewAreaID',textField:'ViewAreaName',remoteName:'sERPSalseDetails.infoERPViewArea',tableName:'infoERPViewArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ViewAreaID" NewRow="False" Span="1" Visible="True" Width="80" MaxLength="0" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報別" Editor="infocombobox" FieldName="NewsTypeID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="125" EditorOptions="valueField:'NewsTypeID',textField:'NewsTypeName',remoteName:'sERPSalseDetails.infoERPNewsType',tableName:'infoERPNewsType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="版別" Editor="infocombobox" FieldName="NewsAreaID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" EditorOptions="valueField:'NewsAreaID',textField:'NewsAreaName',remoteName:'sERPSalseDetails.infoERPNewsArea',tableName:'infoERPNewsArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發刊單位" Editor="infocombobox" FieldName="NewsPublishID" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" Width="90" EditorOptions="valueField:'NewsPublishID',textField:'NewsPublishName',remoteName:'sERPSalseDetails.infoERPNewsPublish',tableName:'infoERPNewsPublish',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發稿段數" Editor="text" FieldName="Sections" NewRow="True" Span="1" Visible="True" Width="50" MaxLength="0" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發稿行數" Editor="numberbox" FieldName="OfficeLines" MaxLength="0" RowSpan="1" Span="1" Visible="True" Width="50" OnBlur="OnBlur_ChangeOfficePrice" NewRow="False" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="繳社單價" Editor="numberbox" FieldName="OfficePrice" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" EditorOptions="precision:3" OnBlur="OnBlur_ChangeOfficePrice" />
                        <JQTools:JQFormColumn Alignment="left" Caption="繳社總價" Editor="numberbox" FieldName="OfficeAmt" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" EditorOptions="" OnBlur="OnBlur_ChangeOfficeAmt" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="False" Width="80" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="佣金" Editor="numberbox" FieldName="Commission" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="50" EditorOptions="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶行數" Editor="numberbox" FieldName="CustLines" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" OnBlur="OnBlur_ChangeCustPrice" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶單價" Editor="numberbox" EditorOptions="precision:3" FieldName="CustPrice" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" OnBlur="OnBlur_ChangeCustPrice" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶總價" Editor="numberbox" FieldName="CustAmt" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" Width="60" EditorOptions="" ReadOnly="False" OnBlur="OnBlur_ChangeCustAmt" />
                        <JQTools:JQFormColumn Alignment="left" Caption="刊期" Editor="text" FieldName="PublishCount" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="贈期" Editor="text" FieldName="PresentCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" OnBlur="OnBlur_TotalSalesQty" />
                        <JQTools:JQFormColumn Alignment="left" Caption="贈週報" Editor="text" FieldName="PresentWNewsCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" OnBlur="OnBlur_PresentWNewsCount" />
                        <JQTools:JQFormColumn Alignment="left" Caption="週報日期" Editor="textarea" FieldName="WeekendDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="6" Span="1" Visible="True" Width="85" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單位數" Editor="numberbox" FieldName="SalesQty" MaxLength="0" NewRow="True" RowSpan="1" Span="1" Visible="True" Width="50" OnBlur="OnBlur_TotalSalesQty" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="見刊" Editor="numberbox" FieldName="SalesQtyView" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總單位數" Editor="numberbox" FieldName="TotalSalesQty" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結帳日期" Editor="datebox" FieldName="InvoiceYMPoint" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="85" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保留天數" Editor="numberbox" FieldName="KeepDays" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="刊登方式" Editor="infooptions" FieldName="PublishType" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="100" EditorOptions="title:'JQOptions',panelWidth:150,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,onSelect:ControlSalesType,selectOnly:false,items:[{text:'連登',value:'1'},{text:'跳登',value:'2'}]" />
                        <JQTools:JQFormColumn Alignment="left" Caption="連登日期" Editor="datebox" FieldName="ContinueDate" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="跳登日期" Editor="textarea" FieldName="JumpDate" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="85" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出刊備註" Editor="textarea" FieldName="SalesDescr" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="500" EditorOptions="height:60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PDF檔名" Editor="textarea" EditorOptions="height:40" FieldName="Remark1" MaxLength="50" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="500" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesEmployeeID" Editor="text" FieldName="SalesEmployeeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="天數提醒" Editor="text" FieldName="KeepDaysAlert" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="30" EditorOptions="" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SalesMasterNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SalesTypeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetSalesEmployeeID" DefaultValue="" FieldName="SalesEmployeeID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="SalesDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="PublishType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SalesQty" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SalesQtyView" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Commission" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="KeepDaysAlert" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="KeepDays" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetInvoiceYMPoint" FieldName="InvoiceYMPoint" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustNO" RemoteMethod="True" ValidateMessage="請選擇客戶" ValidateType="None" CheckMethod="" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesTypeID" RemoteMethod="True" ValidateMessage="請選擇交易別" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="InvoiceYMPoint" RemoteMethod="True" ValidateMessage="請填寫結帳日" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PublishCount" RemoteMethod="True" ValidateMessage="請填寫刊登天數" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PresentCount" RemoteMethod="True" ValidateMessage="請填寫贈送天數" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustAmt" RemoteMethod="True" ValidateMessage="客戶總價	不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesQty" RemoteMethod="True" ValidateMessage="單位數不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesQtyView" RemoteMethod="True" ValidateMessage="見刊數不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="TotalSalesQty" RemoteMethod="True" ValidateMessage="總單位數不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="KeepDays" RemoteMethod="True" ValidateMessage="保留天數不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Commission" RemoteMethod="True" ValidateMessage="佣金不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PublishType" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>

            </JQTools:JQDialog>


            <JQTools:JQDialog ID="JQDialogAcceptDate" runat="server" DialogLeft="60px" DialogTop="20px" Title="預收查詢" ShowSubmitDiv="False" Width="620px">
                <JQTools:JQDataGrid ID="JQGridAcceptDate" runat="server" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="infoAcceptDateData" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sR_SalesDetails.infoAcceptDateData" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="550px" BufferView="False" NotInitGrid="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="98" />
                        <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="110" />
                        <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="text" FieldName="CustAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" />
                        <JQTools:JQGridColumn Alignment="center" Caption="開發票?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="depositOV" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="55" FormatScript="genCheckBox" />
                        <JQTools:JQGridColumn Alignment="center" Caption="預收日期" Editor="datebox" FieldName="AcceptDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="78" />
                        <JQTools:JQGridColumn Alignment="center" Editor="text" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="61" Caption="業務代號" FieldName="SalesID" />
                    </Columns>
                    <TooItems>                  
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                    <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="SalesDescrDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="SysDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sR_SalesDetails.infoSalesMan',tableName:'infoSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶代號" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sR_SalesDetails.infoCustomersAll',tableName:'infoCustomersAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="200" />
                </QueryColumns>
                </JQTools:JQDataGrid>
        </JQTools:JQDialog>


            <div class="easyui-layout" style="height: 450px;">
                <div data-options="region:'west',split:true" style="width: 320px; height: 16px;">
                     <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPSalseDetails.ERPSalesMaster" runat="server" AutoApply="True"
                DataMember="ERPSalesMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="近期銷貨清單" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Height="448px" OnLoadSuccess="MastersetWhere" OnSelect="dataGridViewSelect" ParentObjectID="">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="67" ReadOnly="True" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="60" ReadOnly="True" FormatScript="FormatScriptIsInvoice" />
                    <JQTools:JQGridColumn Alignment="center" Caption="交易別" Editor="text" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="38" />
                    <JQTools:JQGridColumn Alignment="center" Caption="總數" Editor="text" FieldName="TotalSalesQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="35" />
                    <JQTools:JQGridColumn Alignment="center" Caption="剩餘數" Editor="text" FieldName="UseQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" FormatScript="FormatScriptUseQty" />
                    <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="text" FieldName="SalesAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="43" />
                    <JQTools:JQGridColumn Alignment="left" Caption="預收日期" Editor="text" FieldName="AcceptDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" />
                    <JQTools:JQGridColumn Alignment="center" Caption="保留天數" Editor="text" FieldName="KeepDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="53" />
                    <JQTools:JQGridColumn Alignment="center" Caption="天數提醒" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="KeepDaysAlert" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="53" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="text" FieldName="CreateDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Width="120" Visible="False" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="序號" Editor="text" FieldName="SalesMasterNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" />
                </Columns>
                <TooItems>                  
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="OpenInsertSalesDetails" Text="新增銷貨" />
                    <JQTools:JQToolItem Icon="icon-cut" ItemType="easyui-linkbutton" OnClick="OpenCopySalesDetails" Text="複製銷貨" />
                    <JQTools:JQToolItem Enabled="True" ItemType="easyui-linkbutton" OnClick="UpdateSalesMaster" Text="訂單修改" Visible="True" Icon="icon-ok" />
                </TooItems>
            </JQTools:JQDataGrid>
                </div>
                                                                                                 
                    <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormMasterUpdate" Title="銷貨修改" EditMode="Dialog" DialogLeft="30px" DialogTop="10px" Width="500px">
                <JQTools:JQDataForm ID="dataFormMasterUpdate" runat="server" DataMember="ERPSalesMaster" HorizontalColumnsCount="4" RemoteName="sERPSalseDetails.ERPSalesMaster" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" OnApplied="OnAppliedSalesDetails" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsConvertNexMonth" NewRow="True" ReadOnly="False" Span="2" Visible="True" Width="40" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesMasterNO" Editor="text" FieldName="SalesMasterNO" Width="80" Visible="False" Span="1" NewRow="False" ReadOnly="False" MaxLength="0" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="infocombobox" FieldName="CustNO" Width="200" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sERPSalseDetails.infoCustomers',tableName:'infoCustomers',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2" NewRow="True" Visible="True" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總單位數" Editor="numberbox" FieldName="TotalSalesQty" NewRow="True" ReadOnly="False" Visible="True" Width="50" MaxLength="0" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預收日期" Editor="datebox" FieldName="AcceptDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保留天數" Editor="numberbox" FieldName="KeepDays" MaxLength="0" NewRow="True" Span="1" Visible="True" Width="60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="天數提醒" Editor="checkbox" FieldName="KeepDaysAlert" maxlength="0" NewRow="False" Span="1" Visible="True" Width="50" ReadOnly="False" RowSpan="1" />
                    </Columns>
                </JQTools:JQDataForm>

            </JQTools:JQDialog>

                <div data-options="region:'center'" height: 450px;">
                    <JQTools:JQDataGrid ID="dataGridDetail" data-options="pagination:true,view:commandview" RemoteName="sERPSalseDetails.ERPSalesDetails" runat="server" AutoApply="True"
                DataMember="ERPSalesDetails" Pagination="False" QueryTitle="Query"
                Title="" AllowDelete="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="True" AllowAdd="True" AllowUpdate="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" OnUpdated="DetailSaveRefresh" PageList="10,20,30,40,50" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" Height="447px" EditDialogID="" ParentObjectID="" OnUpdate="DetailUpdateCheck" OnDelete="UpdateSalseDetailsIsActive" OnLoadSuccess="DetailsetWhere" BufferView="False" NotInitGrid="False" RowNumbers="True">
                                    <Columns>
                                        <JQTools:JQGridColumn Alignment="right" Caption="SalesMasterNO" Editor="numberbox" FieldName="SalesMasterNO" Format="" Width="50" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="" FieldName="CustNO" Format="" Width="58" Visible="True" Frozen="True" ReadOnly="True" Sortable="True" IsNvarChar="False" MaxLength="0" QueryCondition="" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="項次" Editor="text" FieldName="ItemSeq" Format="" Width="59" ReadOnly="True" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" />                        
                                        <JQTools:JQGridColumn Alignment="center" Caption="交易別" Editor="" FieldName="SalesTypeID" Format="" Width="40" EditorOptions="" Frozen="True" Visible="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="版別" Editor="infocombogrid" FieldName="DMTypeID" Format="" Width="35" EditorOptions="panelWidth:165,valueField:'DMTypeID',textField:'DMTypeID',remoteName:'sERPSalseDetails.infoERPDMType',tableName:'infoERPDMType',valueFieldCaption:'DMTypeID',textFieldCaption:'DMTypeID',selectOnly:false,checkData:false,columns:[{field:'DMTypeID',title:'代號',width:40,align:'left',sortable:false},{field:'DMTypeName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />                   
                                        <JQTools:JQGridColumn Alignment="center" Caption="區域" Editor="infocombogrid" FieldName="ViewAreaID" Format="" Width="40" EditorOptions="panelWidth:120,valueField:'ViewAreaID',textField:'ViewAreaName',remoteName:'sERPSalseDetails.infoERPViewArea',tableName:'infoERPViewArea',valueFieldCaption:'ViewAreaID',textFieldCaption:'ViewAreaName',selectOnly:false,checkData:false,columns:[{field:'ViewAreaID',title:'代號',width:40,align:'left',sortable:false},{field:'ViewAreaName',title:'名稱',width:55,align:'left',sortable:false}],cacheRelationText:false" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />                  
                                        <JQTools:JQGridColumn Alignment="center" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="yyyy-mm-dd" Width="65" FormatScript="" Frozen="True" Sortable="True" ReadOnly="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Visible="True" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="星期" Editor="" FieldName="dWeekday" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="30" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="匯入" Editor="checkbox" FieldName="IsTransSys" Format="" Width="30" Visible="True" EditorOptions="on:1,off:0" FormatScript="genCheckBox" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="匯入編號" Editor="" FieldName="depositSeq" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="55" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="需發票" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsInvoice" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="發票年月" Editor="text" FieldName="InvoiceYM" Format="" Width="50" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="報" Editor="infocombogrid" FieldName="NewsTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" EditorOptions="panelWidth:165,valueField:'NewsTypeID',textField:'NewsTypeID',remoteName:'sERPSalseDetails.infoERPNewsType',tableName:'infoERPNewsType',valueFieldCaption:'NewsTypeID',textFieldCaption:'NewsTypeID',selectOnly:false,checkData:false,columns:[{field:'NewsTypeID',title:'代號',width:40,align:'left',sortable:false},{field:'NewsTypeName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="版" Editor="infocombogrid" FieldName="NewsAreaID" Format="" Width="30" Visible="True" EditorOptions="panelWidth:165,valueField:'NewsAreaID',textField:'NewsAreaID',remoteName:'sERPSalseDetails.infoERPNewsArea',tableName:'infoERPNewsArea',valueFieldCaption:'NewsAreaID',textFieldCaption:'NewsAreaID',selectOnly:false,checkData:false,columns:[{field:'NewsAreaID',title:'代號',width:40,align:'left',sortable:false},{field:'NewsAreaName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="發" Editor="infocombogrid" FieldName="NewsPublishID" Format="" Width="30" Visible="True" EditorOptions="panelWidth:165,valueField:'NewsPublishID',textField:'NewsPublishID',remoteName:'sERPSalseDetails.infoERPNewsPublish',tableName:'infoERPNewsPublish',valueFieldCaption:'NewsPublishID',textFieldCaption:'NewsPublishID',selectOnly:false,checkData:false,columns:[{field:'NewsPublishID',title:'代號',width:40,align:'left',sortable:false},{field:'NewsPublishName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="段" Editor="numberbox" FieldName="Sections" Format="" Width="20" Visible="True" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" />
                                        <JQTools:JQGridColumn Alignment="right" Caption="行數" Editor="numberbox" FieldName="OfficeLines" Format="" Width="28" Visible="True" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" />
                                        <JQTools:JQGridColumn Alignment="right" Caption="社單價" Editor="numberbox" FieldName="OfficePrice" Format="" Width="40" Visible="True" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" />
                                        <JQTools:JQGridColumn Alignment="right" Caption="社總價" Editor="numberbox" FieldName="OfficeAmt" Format="" Width="40" Visible="True" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False"  />
                                        <JQTools:JQGridColumn Alignment="center" Caption="客行數" Editor="numberbox" FieldName="CustLines" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="28" />
                                        <JQTools:JQGridColumn Alignment="right" Caption="客單價" Editor="numberbox" FieldName="CustPrice" Format="" Width="40" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" OnTotal="" />                        
                                        <JQTools:JQGridColumn Alignment="right" Caption="客總額" Editor="numberbox" FieldName="CustAmt" Format="" Width="40" FormatScript="" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" />  
                                        <JQTools:JQGridColumn Alignment="center" Caption="贈期" Editor="" FieldName="GrantTypeID" Format="" Width="30" EditorOptions="" Sortable="True" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Visible="True" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="單位數" Editor="text" FieldName="SalesQty" Format="" Width="40" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />                        
                                        <JQTools:JQGridColumn Alignment="center" Caption="見刊" Editor="text" FieldName="SalesQtyView" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="30" FormatScript="genCheckBox" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="出刊備註" Editor="text" FieldName="SalesDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="PDF檔名" Editor="text" FieldName="Remark1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                                        </JQTools:JQGridColumn>
                                        <JQTools:JQGridColumn Alignment="left" Caption="聯絡備註" Editor="text" FieldName="ContractDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                                        <JQTools:JQGridColumn Alignment="right" Caption="佣金" Editor="text" FieldName="Commission" Format="" Width="30" Visible="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />   
                                        <JQTools:JQGridColumn Alignment="left" Caption="業務代碼" Editor="text" FieldName="SalesEmployeeID" Format="" Width="60" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />                        
                                        <JQTools:JQGridColumn Alignment="center" Caption="提醒日期" Editor="datebox" FieldName="SalesDescrDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" FormatScript="" Format="yyyy-mm-dd" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="建立者" Editor="" FieldName="CreateBy" Format="" Width="40" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy-mm-dd HH:MM" Width="94" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" FormatScript="" />
                                    </Columns>                                   
                                    <RelationColumns>
                                        <JQTools:JQRelationColumn FieldName="SalesMasterNO" ParentFieldName="SalesMasterNO" />
                                    </RelationColumns>
                                    <TooItems>
                                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="OpenInsertSalesDetailsLast" Text="補銷貨明細" Enabled="True" Visible="True" />
                                        <JQTools:JQToolItem Enabled="True" ItemType="easyui-linkbutton" OnClick="UpdateSalesDescr" Text="銷貨明細修改" Visible="True" Icon="icon-ok" />
                                        <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" Enabled="True" Visible="True" />
                                        <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" Enabled="True" Visible="True"  />
                                        <JQTools:JQToolItem Enabled="True" Icon="icon-ok" ItemType="easyui-linkbutton" OnClick="GridCheckAllFalse" Text="銷貨失效" Visible="True" />
                                        <JQTools:JQToolItem Enabled="True" ItemType="easyui-linkbutton" OnClick="ImportSalesDetails" Text="銷貨匯入" Visible="True" Icon="icon-back"/>
                                    </TooItems>
                                </JQTools:JQDataGrid>
                    <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataGridDetail" EnableTheming="True">
                        <Columns>
<%--                            <JQTools:JQValidateColumn CheckNull="True" FieldName="DMTypeID" RemoteMethod="True" ValidateMessage="請選擇版別" ValidateType="None" />--%>
<%--                            <JQTools:JQValidateColumn CheckNull="True" FieldName="GrantTypeID" RemoteMethod="True" ValidateMessage="請選擇贈期" ValidateType="None" />--%>
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="CustPrice" RemoteMethod="True" ValidateMessage="請填寫單價" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="CustAmt" RemoteMethod="True" ValidateMessage="請填寫總額" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesQty" RemoteMethod="True" ValidateMessage="請填寫單位數" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesQtyView" RemoteMethod="True" ValidateMessage="請填寫見刊" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="Commission" RemoteMethod="True" ValidateMessage="請填寫佣金" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckMethod="CheckStrWildWord" FieldName="InvoiceYM" CheckNull="True" RemoteMethod="False" ValidateMessage="發票年月格式錯誤！" ValidateType="None" />
                        </Columns>
                    </JQTools:JQValidate>
                    <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataGridDetail" EnableTheming="True">
                        <Columns>
<JQTools:JQDefaultColumn FieldName="SalesMasterNO" DefaultValue="" RemoteMethod="False" CarryOn="False" DefaultMethod="GetSalesMasterNO"></JQTools:JQDefaultColumn>
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCustNO" FieldName="CustNO" RemoteMethod="False" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SalesQty" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SalesQtyView" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Commission" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsActive" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetSalesEmployeeID" FieldName="SalesEmployeeID" RemoteMethod="False" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="CustLines" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="OfficeLines" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Sections" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="OfficeAmt" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="OfficePrice" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsSetInvoice" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsImport" RemoteMethod="True" />
                        </Columns>
                    </JQTools:JQDefault>
                    <JQTools:JQDialog ID="Dialog_SalesDescr" runat="server" BindingObjectID="dataFormSalesDescr" EditMode="Dialog" Title="銷貨明細修改" DialogLeft="50px" DialogTop="20px" Width="750px">
                                <JQTools:JQDataForm runat="server" ID="dataFormSalesDescr" RemoteName="sERPSalseDetails.ERPSalesDetails" DataMember="ERPSalesDetails" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="4" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="OnAppliedDFSalesDescr" OnLoadSuccess="OnLoadSuccessDFSalesDescr" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="" FieldName="CustNO" ReadOnly="False" Visible="True" Width="80" NewRow="False" MaxLength="0" RowSpan="1" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="客戶簡稱" Editor="" FieldName="CustShortName" ReadOnly="False" Visible="True" Width="120" NewRow="False" MaxLength="0" RowSpan="1" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" ReadOnly="True" Visible="False" Width="120" NewRow="False" MaxLength="0" RowSpan="1" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="SalesMasterNO" Editor="text" FieldName="SalesMasterNO" Visible="False" Width="80" ReadOnly="False" NewRow="False" MaxLength="0" RowSpan="1" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="交易別" Editor="infocombobox" FieldName="SalesTypeID" Width="125" Visible="True" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalseDetails.infoERPSalesType',tableName:'infoERPSalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:ControlShowItem,panelHeight:200" NewRow="True" ReadOnly="True" MaxLength="0" RowSpan="1" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="版別" Editor="infocombobox" FieldName="DMTypeID" Width="100" Visible="True" EditorOptions="valueField:'DMTypeID',textField:'DMTypeName',remoteName:'sERPSalseDetails.infoERPDMType',tableName:'infoERPDMType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetSalesDescrViewAreaID,panelHeight:200" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="區域" Editor="infocombobox" EditorOptions="valueField:'ViewAreaID',textField:'ViewAreaName',remoteName:'sERPSalseDetails.infoERPViewArea',tableName:'infoERPViewArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ViewAreaID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="發票年月" Editor="text" FieldName="InvoiceYM" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" OnBlur="" />
                                        <JQTools:JQFormColumn Alignment="center" Caption="贈期" Editor="infocombogrid" FieldName="GrantTypeID" Format="" Width="35" EditorOptions="panelWidth:165,valueField:'GrantTypeID',textField:'GrantTypeID',remoteName:'sERPSalseDetails.infoERPGrantType',tableName:'infoERPGrantType',valueFieldCaption:'GrantTypeID',textFieldCaption:'GrantTypeID',selectOnly:false,checkData:false,columns:[{field:'GrantTypeID',title:'代號',width:40,align:'left',sortable:false},{field:'GrantTypeName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="報別" Editor="infocombobox" FieldName="NewsTypeID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="125" EditorOptions="valueField:'NewsTypeID',textField:'NewsTypeName',remoteName:'sERPSalseDetails.infoERPNewsType',tableName:'infoERPNewsType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="版別" Editor="infocombobox" FieldName="NewsAreaID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="125" EditorOptions="valueField:'NewsAreaID',textField:'NewsAreaName',remoteName:'sERPSalseDetails.infoERPNewsArea',tableName:'infoERPNewsArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="發刊單位" Editor="infocombobox" FieldName="NewsPublishID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="125" EditorOptions="valueField:'NewsPublishID',textField:'NewsPublishName',remoteName:'sERPSalseDetails.infoERPNewsPublish',tableName:'infoERPNewsPublish',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="發稿段數" Editor="text" FieldName="Sections" NewRow="True" Visible="True" Width="50" ReadOnly="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="發稿行數" Editor="numberbox" FieldName="OfficeLines" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" OnBlur="OnBlur_SalesDescrOfficePrice" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="繳社單價" Editor="numberbox" FieldName="OfficePrice" MaxLength="0" RowSpan="1" Span="1" Visible="True" Width="60" EditorOptions="precision:3" OnBlur="OnBlur_SalesDescrOfficePrice" NewRow="False" ReadOnly="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="繳社總價" Editor="numberbox" FieldName="OfficeAmt" MaxLength="0" RowSpan="1" Span="1" Visible="True" Width="60" OnBlur="OnBlur_SalesDescrOfficeAmt" EditorOptions="" NewRow="False" ReadOnly="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="佣金" Editor="numberbox" FieldName="Commission" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" EditorOptions="" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="客戶行數" Editor="numberbox" FieldName="CustLines" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" OnBlur="OnBlur_SalesDescrCustPrice" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="客戶單價" Editor="numberbox" FieldName="CustPrice" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" EditorOptions="precision:3" OnBlur="OnBlur_SalesDescrCustPrice" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="客戶總價" Editor="numberbox" FieldName="CustAmt" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" EditorOptions="" OnBlur="OnBlur_SalesDescrCustAmt" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="單位數" Editor="numberbox" FieldName="SalesQty" Visible="True" Width="50" OnBlur="OnBlur_SalesDescrCustPrice" ReadOnly="False" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="見刊" Editor="numberbox" FieldName="SalesQtyView" Visible="True" Width="50" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="提醒日期" Editor="datebox" FieldName="SalesDescrDate" NewRow="False" ReadOnly="False" Visible="True" Width="90" MaxLength="0" RowSpan="1" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="匯入" Editor="checkbox" FieldName="IsTransSys" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="50" EditorOptions="on:1,off:0" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="需開發票" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsInvoice" NewRow="False" ReadOnly="False" Visible="True" Width="50" MaxLength="0" RowSpan="1" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="出刊備註" Editor="textarea" EditorOptions="height:60" FieldName="SalesDescr" ReadOnly="False" Visible="True" Width="500" MaxLength="0" NewRow="True" RowSpan="1" Span="4" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡備註" Editor="textarea" EditorOptions="height:60" FieldName="ContractDescr" ReadOnly="False" Span="4" Visible="True" Width="500" MaxLength="0" NewRow="False" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="PDF檔名" Editor="textarea" EditorOptions="height:40" FieldName="Remark1" NewRow="False" ReadOnly="False" Span="4" Visible="True" Width="500" />
                                    </Columns>
                                </JQTools:JQDataForm>                               
                                <JQTools:JQValidate ID="validateMaster0" runat="server" BindingObjectID="dataFormSalesDescr" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustPrice" RemoteMethod="True" ValidateMessage="單價不可空白" ValidateType="None" CheckMethod="" />
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesQty" RemoteMethod="True" ValidateMessage="單位數不可空白" ValidateType="None" />
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Commission" RemoteMethod="True" ValidateMessage="佣金不可空白" ValidateType="None" />
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesDate" RemoteMethod="False" ValidateMessage="銷貨日期不可小於等於今天！" ValidateType="None" CheckMethod="validateDetailSalesDate" />
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustAmt" RemoteMethod="True" ValidateMessage="客總價不可空白" ValidateType="None" />
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="InvoiceYM" RemoteMethod="False" ValidateMessage="發票年月格式錯誤！" ValidateType="None" CheckMethod="CheckStrWildWord"/>
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="GrantTypeID" RemoteMethod="True" ValidateMessage="贈期不可空白" ValidateType="None" />
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesQtyView" RemoteMethod="True" ValidateMessage="見刊不可空白" ValidateType="None" />
                                    </Columns>
                                </JQTools:JQValidate>
                             </JQTools:JQDialog>
                    <JQTools:JQDialog ID="Dialog_SalesDetails" runat="server" BindingObjectID="dataFormSalesDetail" EditMode="Dialog" Title="新增銷貨明細" DialogLeft="50px" DialogTop="30px" Width="740px" ShowSubmitDiv="False">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <JQTools:JQDataForm ID="dataFormSalesDetail" runat="server" Closed="False" ContinueAdd="False" DataMember="ERPSalesDetails" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="4" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedDFSalesDescr" OnLoadSuccess="OnLoadSuccessSalesDetail" RemoteName="sERPSalseDetails.ERPSalesDetails" ShowApplyButton="False" ValidateStyle="Dialog" ParentObjectID="" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0">
                                                <Columns>
                                                    <JQTools:JQFormColumn Alignment="left" Caption="結帳日期" Editor="datebox" FieldName="InvoiceYMPoint" NewRow="True" ReadOnly="False" Span="1" Visible="True" Width="85" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="版別" Editor="infocombobox" EditorOptions="valueField:'DMTypeID',textField:'DMTypeName',remoteName:'sERPSalseDetails.infoERPDMType',tableName:'infoERPDMType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetSalseDetailsViewAreaID,panelHeight:200" FieldName="DMTypeID" NewRow="True" ReadOnly="False" Span="1" Visible="True" Width="140" MaxLength="0" RowSpan="1" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="區域" Editor="infocombobox" EditorOptions="valueField:'ViewAreaID',textField:'ViewAreaName',remoteName:'sERPSalseDetails.infoERPViewArea',tableName:'infoERPViewArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ViewAreaID" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="140" MaxLength="0" RowSpan="1" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="單位數" Editor="numberbox" FieldName="SalesQty" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" OnBlur="OnBlur_SalesQty" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="見刊" Editor="numberbox" FieldName="SalesQtyView" MaxLength="0" NewRow="False" OnBlur="" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="刊登日期" Editor="textarea" FieldName="JumpDate" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="週報日期" Editor="textarea" FieldName="JumpDate2" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" Width="100" ReadOnly="False" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="出刊備註" Editor="textarea" FieldName="SalesDescr" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="500" EditorOptions="height:70" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="PDF檔名" Editor="textarea" EditorOptions="height:40" FieldName="Remark1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="500" />
                                                </Columns>
                                            </JQTools:JQDataForm>
                                            <JQTools:JQValidate ID="validatedataFormSalesDetail" runat="server" BindingObjectID="dataFormSalesDetail" EnableTheming="True">
                                                <Columns>
                                                    <JQTools:JQValidateColumn CheckMethod="" CheckNull="True" FieldName="SalesQty" RemoteMethod="True" ValidateMessage="請填寫單位數" ValidateType="None" />
                                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesQtyView" RemoteMethod="True" ValidateMessage="請填寫見刊" ValidateType="None" />
                                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="DMTypeID" RemoteMethod="True" ValidateMessage="請選擇版別" ValidateType="None" />
                                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ViewAreaID" RemoteMethod="True" ValidateMessage="請選擇區域" ValidateType="None" />
                                                </Columns>
                                            </JQTools:JQValidate>
                                            <JQTools:JQDefault ID="defaultdataFormSalesDetail" runat="server" BindingObjectID="dataFormSalesDetail" EnableTheming="True">
                                                <Columns>
                                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetInvoiceYMPoint" FieldName="InvoiceYMPoint" RemoteMethod="False" />
                                                </Columns>
                                            </JQTools:JQDefault>
                                        </td>
                                        <td style="vertical-align: bottom">
                                            <a href="#" class="easyui-linkbutton" data-options="" onclick="InsertSalesDetailsLast()">新增銷貨</a>
                                        </td>
                                    </tr>
                                </table>
                             </JQTools:JQDialog>                           
                </div>
                <div data-options="region:'east'" style="width: 85px; height: 450px;">
                     <JQTools:JQDataGrid ID="dataGridSalesInfo" data-options="pagination:true,view:commandview" RemoteName="sERPSalseDetails.infoSalesInfo" runat="server" AutoApply="True"
                DataMember="infoSalesInfo" Pagination="False" QueryTitle="" EditDialogID=""
                Title="資訊統計" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Height="448px" ParentObjectID="" OnLoadSuccess="InfoLoadSuccess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="區域" Editor="text" FieldName="ViewAreaName" Format="" MaxLength="0" Width="43" ReadOnly="True" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="總額" Editor="text" FieldName="CustAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40" Total="" />
                    <JQTools:JQGridColumn Alignment="center" Caption="家數" Editor="text" FieldName="iCount" Format="" MaxLength="0" Width="30" ReadOnly="True" Total="" />
                    <JQTools:JQGridColumn Alignment="center" Caption="單位數" Editor="text" FieldName="salesQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="38" Total="" />
                </Columns>
            </JQTools:JQDataGrid>
                </div>
             </div>
               
           
    </form>
</body>
</html>
