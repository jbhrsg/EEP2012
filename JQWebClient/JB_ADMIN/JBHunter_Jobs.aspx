<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_Jobs.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">

        

        $(document).ready(function () {

            //設定 Grid QueryColunm panel寬度調整
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 1050 });

            //預估營業額 合併顯示
            var SalesTeamID = $('#dataFormMasterSalesTeamID').closest('td');
            var JobGrade = $('#dataFormMasterJobGrade').closest('td').children();//等級
            var JobNeedCount = $('#dataFormMasterJobNeedCount').closest('td').children();//需求人數
            var IsActive = $('#dataFormMasterIsActive').closest('td').children();//有效

            SalesTeamID.append('&nbsp;&nbsp;&nbsp;等級').append(JobGrade).append('&nbsp;&nbsp;&nbsp;&nbsp;需求人數').append(JobNeedCount).append('&nbsp;&nbsp;&nbsp;&nbsp;有效?').append(IsActive);
            
            //業務單位 合併顯示
            var iTurnover = $('#dataFormMasteriTurnover').closest('td');
            var ratio = $('#dataFormMasterratio').closest('td').children();
            var Amount = $('#dataFormMasterAmount').closest('td').children();
            iTurnover.append(' * ').append(ratio).append(' = ').append(Amount);

            //加上(不可刊登)的欄位
            var HideFieldName = ['JobOpportunity', 'JobRisk', 'JobInterview', 'JobRecruit', 'JobNotes', 'JobRequirementN', 'JobFareN'];
            var FormName = '#dataFormMaster';

            $.each(HideFieldName, function (index, fieldName) {
                var Name = $(FormName + fieldName);
                $('<br/><span id="t1" style="color: rgb(138, 43, 226);">*不可刊登</span>').insertAfter(Name);
                //$(FormName + fieldName).closest('td').css("color", "rgb(138, 43, 226)");
                $(FormName + fieldName).closest('td').prev('td').css("color", "rgb(138, 43, 226)");//改變td前面文字顏色
            });

            var JobInterviewRef = $('#dataFormMasterJobInterviewRef').closest('td');
            JobInterviewRef.append('&nbsp;(選擇帶入其他職缺面談事項)');
            $('#dataFormMasterJobInterviewRef').closest('td').css("color", "red");//改變td後面文字顏色

            //職缺檔案組織圖 顯示文字提醒
            var bOrg = $('#DFCustomerFilebOrg').closest('td');           
            bOrg.append(' (請上傳圖檔格式) ');

            //-------------推薦作業---------------------------------------

            //實際營業額 合併顯示
            var iTurnoverReal = $('#DFJobAssignLogsiTurnoverReal').closest('td');
            var ratioReal = $('#DFJobAssignLogsratioReal').closest('td').children();
            var AmountReal = $('#DFJobAssignLogsAmountReal').closest('td').children();
            //部門單位 合併顯示
            var sDeptName = $('#DFJobAssignLogssDeptName').closest('td');
            var sJobName = $('#DFJobAssignLogssJobName').closest('td').children();
            var MonthSalary = $('#DFJobAssignLogsMonthSalary').closest('td').children();

            iTurnoverReal.append(' * ').append(ratioReal).append(' = ').append(AmountReal);//.append("&nbsp;&nbsp;&nbsp;&nbsp;部門單位").append(sDeptName).append("&nbsp;&nbsp;&nbsp;&nbsp;職稱").append(sJobName).append("&nbsp;&nbsp;&nbsp;&nbsp;月薪").append(MonthSalary);
            sDeptName.append("&nbsp;&nbsp;&nbsp;&nbsp;職稱").append(sJobName).append("&nbsp;&nbsp;&nbsp;&nbsp;月薪").append(MonthSalary);

            //成案機率、月份加文字 顯示
            var Draft = $('#DFJobAssignLogsDraft').closest('td');
            Draft.append('&nbsp;%');
            var DraftMonth = $('#DFJobAssignLogsDraftMonth').closest('td');
            DraftMonth.append('&nbsp;月ex:202208');

            //推薦作業中退費欄位勾掉時候清除退回營業額欄位
            $("#DFJobAssignLogsbRefund").click(function () {
                if ($(this).is(":checked") == false) {  //讀取是選中還是非選中，返回true、false
                    $("#DFJobAssignLogsratioReal").numberbox('setValue', 0);//四捨五入      
                    $("#DFJobAssignLogsratioReal").val(0);
                    $("#DFJobAssignLogsiTurnoverReal").numberbox('setValue', 0);//四捨五入      
                    $("#DFJobAssignLogsiTurnoverReal").val(0);
                    $("#DFJobAssignLogsAmountReal").numberbox('setValue', 0);//四捨五入      
                    $("#DFJobAssignLogsAmountReal").val(0);
                }
            });

            $('#JobGrade_Query').combobox('setValue', "");//等級清空

            //-------------職缺資料傳入客戶代號 => 查詢客戶的職缺---------------------------------------
            setTimeout(function () {
                var parameter = Request.getQueryStringByName("CustID");
                if (parameter != "") {

                    if (parameter == "0") {//登入頁的即時訊息連結導入
                        parameter = "";
                    }

                    $("#CustID_Query").refval('setValue', parameter);

                    var parameter2 = Request.getQueryStringByName("iType");
                    if (parameter2 != "") {
                        $("#JobStatus_Query").combobox('setValue', parameter2);//1開,2關
                    }
                    queryGrid('#dataGridView');

                } else {

                    $('#JobStatus_Query').combobox('setValue', "");
                }
            }, 2000);
           


        });        
       
        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridView') {
                //查詢條件
                var result = [];
                var CustID = $('#CustID_Query').refval('getValue');//客戶代號
                var JobName = $('#JobName_Query').val();//職缺名稱
                var HunterID = $('#HunterID_Query').combobox('getValue');//執案顧問                             
                var SalesTeamID = $('#SalesTeamID_Query').combobox('getValue');//業務單位  
                var JobStatus = $('#JobStatus_Query').combobox('getValue');//職缺狀態  
                var JobWorkArea = $('#JobWorkArea_Query').val();//上班地點
                var JobGrade = $('#JobGrade_Query').combobox('getValue');//等級  
                var JobKeepType = $("#JobKeepType_Query").checkbox('getValue');//保密

                if (CustID != '') result.push("CustID = '" + CustID + "'");
                if (JobName != '') result.push("JobName like '%" + JobName + "%'");
                if (HunterID != '') result.push("HunterID = " + HunterID);
                if (SalesTeamID != '') result.push("SalesTeamID = " + SalesTeamID);
                if (JobStatus != '') result.push("iDate = " + JobStatus);
                if (JobWorkArea != '') result.push("JobWorkArea like '%" + JobWorkArea + "%'");
                if (JobGrade != '') result.push("JobGrade = '" + JobGrade+"'");
                if (JobKeepType == 1) result.push("JobKeepType != 3");

                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }        
        //---------------------------------------職缺權限控制-----------------------------------------------------
        //控制是否可以修改 
        function JobUpdateRow(rowData) {
            var username = getClientInfo("username");

            if (GetSalesTeamID() != rowData.SalesTeamID && rowData.LastUpdateBy != username) {
                alert('無編輯權限！');
                return false; //取消編輯的動作 
            }
        }

        //控制是否可以刪除 
        function JobDeleteRow(rowData) {
            var username = getClientInfo("username");
            if (GetSalesTeamID() != rowData.SalesTeamID && rowData.LastUpdateBy != username) {
                alert('無刪除權限！');
                return false; //取消編輯的動作 
            } else if (GetJobUnion(rowData.JobID)>0){
                alert('有推薦或開/關缺日存在，不可刪除！');
                return false; //取消編輯的動作 
            }
        }
        //取得登入者業務單位
        function GetSalesTeamID() {
            var SalesTeamID;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCustomersJobs.HUT_Customer', //連接的Server端，command
                data: "mode=method&method=" + "ReturnSalesTeamID" + "&parameters=",  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    SalesTeamID = data
                },
            });
            return SalesTeamID;
        }
        //取得職缺的關聯筆數----刪除用
        function GetJobUnion(JobID) {
            var JobUnion;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCustomersJobs.HUT_Customer', //連接的Server端，command
                data: "mode=method&method=" + "ReturnJobUnion" + "&parameters=" + JobID,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    JobUnion = data
                },
            });
            return JobUnion;
        }
        //-----------------實際營業額-----------------------------------------------------------------------------------
        function OnBluriTurnoverReal() {
            //var iTurnover = $("#dataFormMaster1iTurnover").val();
            var iTurnover = $("#DFJobAssignLogsiTurnoverReal").val();
            var ratio = $("#DFJobAssignLogsratioReal").val();
            var Amount = Math.round(parseInt(iTurnover) * ratio);
            $("#DFJobAssignLogsAmountReal").numberbox('setValue', Amount);//四捨五入      
            $("#DFJobAssignLogsAmountReal").val(Amount);
        }
        function OnBluriTurnoverReal2() {
            //var iTurnover = $("#dataFormMaster1iTurnover").val();
            var iTurnover = $("#DFJobAssignLogsiTurnoverReal").numberbox('getValue');
            var ratio = $("#DFJobAssignLogsratioReal").val();
            var Amount = Math.round(parseInt(iTurnover) * ratio);
            $("#DFJobAssignLogsAmountReal").numberbox('setValue', Amount);//四捨五入      
            $("#DFJobAssignLogsAmountReal").val(Amount);
        }

        //DataForm Onload
        //控制refval 屬性
        function OnLoadDF() {

            if (getEditMode($("#dataFormMaster")) == "inserted") {
                $("#dataFormMasterCustID").data("inforefval").refval.find("span.icon-view").show();//顯示refval放大鏡
                //$("#dataFormMasterCustID").data("inforefval").refval.find("span.icon-view").focus();
                $('#dataFormMasterCustID').focus();
                $('#dataFormMasterCustID').refval('enable')
            } else {
                $("#dataFormMasterCustID").data("inforefval").refval.find("span.icon-view").hide();//隱藏refval放大鏡
                $('#dataFormMasterJobName').focus();
                $('#dataFormMasterCustID').refval('disable')
                //面談事項過濾客戶
                var CustID = $("#dataFormMasterCustID").refval('getValue');
                $('#dataFormMasterJobInterviewRef').refval('setWhere', "CustID='" + CustID + "'");
            }

        
        }
        function OnSelectInterview() {

            //面談事項過濾客戶
            var CustID = $("#dataFormMasterCustID").refval('getValue');
            $('#dataFormMasterJobInterviewRef').refval('setWhere', "CustID='" + CustID + "'");
        }
        //////------------------------------------//聯繫維護紀錄---------------------------------------------------------
        //function ContactDateLink(value, row, index) {
        //    if (value == undefined) {
        //        return "";
        //    }
        //    else if (value != null )
        //        return "<a href='javascript: void(0)' onclick='LinkContactDate(" + index + ");' style='color:red;'>" + value + "</a>";
        //    else
        //        return "<a href='javascript: void(0)' onclick='LinkContactDate(" + index + ");' style='color:red;'>新增</a>";
        //}

        //// open聯繫維護紀錄畫面 dialog
        //function LinkContactDate(index) {
        //    $("#dataGridView").datagrid('selectRow', index);
        //    openForm('#Dialog_ContactRecord', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');

        //}

       

        //完整顯示Grid聯繫紀錄
        function ShowAllGrid(value) {
            return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
        }
       

        //------------------------------------//開關缺紀錄---------------------------------------------------------    
        function DateLogLink(value, row, index) {
            if (value != null) {
                if (row.iDate == "1") {//----1開,2關,3開發中,0無(待新增)                    
                    return "<a href='javascript: void(0)' onclick='LinkDateLog(" + index + ");' style='color:red;'>" + value + "</a>&nbsp;&nbsp;"; 
                        //+"<a><img src=img/Record.png ></a><b><div style=\"color:Red\"></div></b>";
                    }
                    else {                
                    return "<a href='javascript: void(0)' onclick='LinkDateLog(" + index + ");' style='color:blue;'>" + value + "</a>";
                
                    }
            }
            else return "";

        }
        //顯示Grid日期的顏色
        //function FormatDateString(val, rowData) {
        //    if (rowData.iDate == "1") {
        //        return "<div style='font-weight:bold;color:red;'> " + val + "</div>";
        //    } else if (rowData.iDate == "2") {
        //        return "<div style='color:black;'> " + val + "</div>";
        //    }
        //}

        // open開關缺畫面 dialog
        function LinkDateLog(index) {
            $("#dataGridView").datagrid('selectRow', index);
            openForm('#Dialog_DateLog', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');

        }             

        function OnLoadSuccessDateLog() {
            //日期編輯時=>開缺日期不能改
            if (getEditMode($("#DFDateLog")) == "inserted") {                
                $('#DFDateLogJobDeclareDate').datebox('enable');

            } else {
                $('#DFDateLogJobDeclareDate').datebox('disable');
            }
            if ($('#DFDateLogJobCloseReason').combobox('getValue') == "") {
                $('#DFDateLogJobCloseReason').combobox('setValue', "");
            }
            
        }
        function OnLoadSuccessMDate() {
            //狀態為開缺時: 1 =>不能新增開關缺
            var rows = $("#dataGridView").datagrid('getSelected');
            if (rows.iDate == "1") {

                $('#toolItemdataGrid_DateLog開關缺維護').hide();
            } else $('#toolItemdataGrid_DateLog開關缺維護').show();
        }

        function OnAppliedDateLog() {
            $("#dataGridView").datagrid('reload');

            //判斷是否要顯示開關缺維護----------------------------------
            $("#dataGrid_DateLog").datagrid("selectRow", 0);
            var rows = $("#dataGrid_DateLog").datagrid('getSelected');
            //alert(rows.JobCloseDate);
            if (rows == null) {//刪除時
                $('#toolItemdataGrid_DateLog開關缺維護').show();
            }else if (rows.JobCloseDate == "") {

                $('#toolItemdataGrid_DateLog開關缺維護').hide();
            } else $('#toolItemdataGrid_DateLog開關缺維護').show();
        }
        //開關缺紀錄新增
        function OnInsertDateLog() {
            var rows = $("#dataGrid_DateLog").datagrid('getSelected');
            if (rows != null) {//刪除時
                if (rows.JobCloseDate == null) {
                    alert('上一筆資料尚未關閉');
                    return false;
                }
            } 
        }

        //------------------------------------------------------------------------------------------------------
        function GetCustID() {
            var rows = $("#dataGridView").datagrid('getSelected');
            return rows.CustID;
        }
        function GetJobID() {
            var rows = $("#dataGridView").datagrid('getSelected');
            return rows.JobID;
        }
      
       
        //焦點欄位變顏色
        $(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "yellow");
            });

            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });
        });
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox'  onclick='return false;'  />";
        }        

        ///
        function MasterGridReload() {
            //再打開一次網頁---------------------------------------------------------------------------------------
            //if (getEditMode($("#dataFormMaster")) == 'updated') {
            //    openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
            //} else {
            //    //reload
                queryGrid('#dataGridView');
            //}
        }
        
        //--------------------------聯絡人紀錄-----------------------------------
        function OnLoadContactPerson() {
            //過濾帶入選擇            
            var CustID = $("#dataFormMasterCustID").refval('getValue');
            $('#DFContactPersonSelectAutoKey').refval('setWhere', "CustID='" + CustID+"'");
            $('#DFContactPersonContactStatus').combobox('setValue', "1");//狀態       

            //清空選擇
            if ($('#DFContactPersonContactMobile1Area').combobox('getValue') == "") {
                $('#DFContactPersonContactMobile1Area').combobox('setValue', "");
            }
            if ($('#DFContactPersonContactMobile2Area').combobox('getValue') == "") {
                $('#DFContactPersonContactMobile2Area').combobox('setValue', "");
            }
        }
        //電訪維護紀錄有變更時重整
        function OnAppliedContactPerson() {
            //$("#dataGridView").datagrid('reload');
            $("#DGContactPerson").datagrid('reload');

        }
        //預設值- 電話
        function ContactTelAreaDefault() {
            return $("#dataFormMasterCustomerTelArea").val();
        }
        function ContactTelDefault() {
            return $("#dataFormMasterCustomerTel").val();
        }
        function CheckContactMobile1(phone) {
            var regex = /^09\d{2}-\d{6}$/;
            if (!regex.test(phone)) {
                $("#DFContactPersonContactMobile1").focus();
                return false;
            } else {
                return true;
            }
        }
        function CheckContactMobile2(phone) {
            var regex = /^09\d{2}-\d{6}$/;
            if (!regex.test(phone)) {
                $("#DFContactPersonContactMobile2").focus();
                return false;
            } else {
                return true;
            }
        }
        //---------------------------複制職缺-------------------------------------
        function CopyJob() {
            
            var row = $('#dataGridView').datagrid('getSelected');           
            openForm('#JQDialog1', row, "inserted", 'dialog');
            $("#dataFormMasterJobName").focus();
        }
        
        //---------------呼叫開啟Cust Tab--------------------------------------------------------------------------------
        function OpenCustTab(value, row, index) {
            if (value == undefined) ""
            else if (value != "0")
                return "<a href='javascript: void(0)' onclick='LinkCustTab(" + index + ");' >" + value + "</a>";
            else return value;
        }
        function LinkCustTab(index) {
            $("#dataGridView").datagrid('selectRow', index);
            var rows = $("#dataGridView").datagrid('getSelected');
            var CustID = rows.CustID;
            parent.addTab('客戶資料維護', './JB_ADMIN/JBHunter_Customers.aspx?CustID=' + CustID);
        }
        //
        function OnAppliedRequirementRecord() {
            $("#DGContactRecord").datagrid('reload');
            //$("#dataGridView").datagrid('reload');
        }
        //-----------------預估營業額-----------------------------------------------------------------------------------
        function OnBluriTurnover() {
            //var iTurnover = $("#dataFormMaster1iTurnover").val();
            var iTurnover = $("#dataFormMasteriTurnover").numberbox('getValue');
            var ratio = $("#dataFormMasterratio").val();
            var Amount = Math.round(iTurnover * ratio);
            $("#dataFormMasterAmount").numberbox('setValue', Amount);//四捨五入      
            $("#dataFormMasterAmount").val(Amount);
        }
        
        //-------------------------NAR紀錄管理-----------------------------------
        function OnDeletRequirementRecord() {
            var pre = confirm("確認失效?");
            if (pre == true) {
                //callServerMethod
                RequirementRecordIsActive();
                return false; //取消刪除的動作
            }
            else {
                return false;
            }
        }
        //--------------------------------NAR紀錄失效----------------------------------------
        function RequirementRecordIsActive() {
            var row = $('#DGRequirementRecord').datagrid('getSelected'); //取得當前主檔中選中的那個Data
            if (row != null) {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sCustomersJobs.HUT_Customer', //連接的Server端，command
                    data: "mode=method&method=" + "RequirementRecordIsActive" + "&parameters=" + row.AutoKey, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        $('#DGRequirementRecord').datagrid('reload');
                    }
                });
            }
        }
        //-------------------------檔案管理-----------------------------------
        function sCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        function OnApplyCustomerFile() {
            if ($("#infoFileUploadDFCustomerFileJobFile").val() == "") {                    
                alert('請選擇檔案！');
                return false;
            }
            ////組織圖=>檢查格式
            //if ($("#DFCustomerFilebOrg").checkbox('getValue') == "1") {
            //    alert('請選擇檔案！');
            //    return false;
            //}

        }
        function OnAppliedCustomerFile() {
            //alert('請選擇檔案！');
            $("#DGCustomerFile").datagrid('reload');
        }
        //刪除檔案
        function DeleteCustFile() {
            var row = $('#DGCustomerFile').datagrid('getSelected'); //取得當前主檔中選中的那個Data
            if (row != null) {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sCustomersJobs.HUT_Customer', //連接的Server端，command
                    data: "mode=method&method=" + "DelJobFile" + "&parameters=" + row.AutoKey + "," + row.JobFile, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });
                if (cnt == "0") {
                    $("#DGCustomerFile").datagrid('reload');
                }
                else {
                    alert('此職缺檔案不存在!');
                    return false;
                }
            }
        }
        function OnDeletFile() {
            var pre = confirm("確認刪除?");
            if (pre == true) {
                //callServerMethod
                DeleteCustFile();
                return false; //取消刪除的動作
            }
            else {
                return false;
            }

        }
        //--------------NAR join-----------------------------------------------------------------------------------------------       
        //呼叫NAR join
        function lbNARJoin() {
            openForm('#JQDialogNARJoin', {}, 'viewed', 'dialog');
            var rows = $("#dataGridView").datagrid('getSelected');
            var JobID = rows.JobID;
            $("#DGRequirementRecordJoin").datagrid('setWhere', "h.JobID=" + JobID);
        }
        //顯示NAR串聯
        function ShowJoin(value) {
            return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
        }

        //--------------NAR 報表-----------------------------------------------------------------------------------------------       
        function OpenNAR() {
            var rows = $("#dataGridView").datagrid('getSelected');
            var JobID = rows.JobID;
            var JobName = rows.JobName;
            var url = "../JB_ADMIN/REPORT/JBHunter/JobNARReport.aspx?JobID=" + JobID + "&JobName=" + JobName;

            var height = $(window).height() - 50;
            var height2 = $(window).height() - 90;
            var width = $(window).width() - 290;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                //top:0,
                width: width,
                title: "NAR-Report",
                //maximizable: true                              
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="98%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('open');

        }

        //--------------推薦作業(紀錄)-----------------------------------------------------------------------------------------------                        
        function AssignLink(value, row, index) {
            if (value != undefined)
                return "<a href='javascript: void(0)' onclick='LinkAssign(" + index + ");' style='color:red;'>推薦 / " + value + "</a>";
            else "";
        }
        // open推薦畫面
        function LinkAssign(index) {
            $("#dataGridView").datagrid('selectRow', index);
            //var rows = $("#dataGridView").datagrid('getSelected');
            //var EmpNum = rows.EmpNum;
            //var BeginDate = $('#BeginDate_Query').datebox('getValue');
            //var EndDate = $('#EndDate_Query').datebox('getValue');
            //$.ajax({
            //    type: "POST",
            //    url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchQuery.ERPDelayLunchApply',  //連接的Server端，command
            //    data: "mode=method&method=" + "JBePortalEmpOrderList" + "&parameters=" + BeginDate + "," + EndDate + "," + EmpNum,  //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入搜尋字串
            //    cache: false,
            //    async: true,
            //    success: function (data) {
            //        var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示

            //        if (rows.length > 0) {
            //            //通過loadData方法清除掉原有Grid中的舊有資料並填補分頁資料
            //            $('#dataGrid_DelayDate').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
            //        } else {
            //            $('#dataGrid_DelayDate').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
            //        }

            //    }
            //});
            openForm('#JQDialogJobAssignLogs', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
        }

        //呼叫上傳報告視窗
        function FileLink(value, row, index) {
            //var AssignID = row.AssignID;//1推薦,2面試,3錄取,4未錄取,5報到,6未報到
            //if (AssignID == "1")//&& row.AssignFile=="")
            return "<a href='javascript: void(0)' onclick='LinkFile(" + index + ");'>檔案上傳</a>";
            //else return "";
        }
        function LinkFile(index) {
            $("#DGJobAssignLogs").datagrid('selectRow', index);
            var rows = $("#DGJobAssignLogs").datagrid('getSelected');
            openForm('#JQDialogAssignLogs2', rows, "updated", 'dialog');
        }
        //複製推薦紀錄
        function OpenCopyAssign() {
            //選取的那筆進行複製
            var row = $('#DGJobAssignLogs').datagrid('getSelected');
            openForm('#JQDialogAssignLogs', row, "inserted", 'dialog');
            ControlAssign("");
        }

        function OnAppliedAssignLogs() {
            $('#DGJobAssignLogs').datagrid("reload");
            queryGrid('#dataGridView');
        }
        function OnDeletedAssignLogs() {
            $('#DGJobAssignLogs').datagrid("reload");
            queryGrid('#dataGridView');
        }
        function OnLoadAssignLogs() {
            if (getEditMode($("#DFJobAssignLogs")) == "inserted") {
                //將職缺執案顧問帶入推薦的執案顧問
                var HunterID = $('#dataFormMaster3HunterID').val();
                if (HunterID != "") {
                    $("#DFJobAssignLogsHunterID").combobox('setValue', HunterID);
                }
            }
            //推薦記錄預設            
            var AssignID = $("#DFJobAssignLogsAssignID").combobox('getValue');
            ControlAssign(AssignID);
        }
        //推薦記錄狀態控管
        function OnSelectAssignID(rowData) {
            var AssignID = rowData.AssignID;//1推薦,2面試,3錄取,4未錄取,5報到,6未報到
            ControlAssign(AssignID);
            var sTitle = "";            
            //var AssignFile = $('#infoFileUploadDFJobAssignLogsAssignFile').closest('td').prev('td');//改變td前面文字顏色
            //AssignFile.empty();
            //AssignFile.append(sTitle);
        }
        function ControlAssign(AssignID) {//1推薦,2面試,3錄取,4未錄取,5報到,6未報到
            //預設
            $('#DFJobAssignLogsAssignContent').closest('td').prev('td').hide();//推薦評估隱藏
            $('#DFJobAssignLogsAssignContent').closest('td').hide();//推薦評估隱藏
            $('#DFJobAssignLogsInterviewTime').closest('td').prev('td').hide();//面試時間隱藏
            $('#DFJobAssignLogsInterviewTime').closest('td').hide();//面試時間隱藏
            $('#DFJobAssignLogsAssignReason').closest('td').prev('td').hide();//原因隱藏
            $('#DFJobAssignLogsAssignReason').closest('td').hide();//原因隱藏
            $('#DFJobAssignLogsiTurnoverReal').closest('td').prev('td').hide();//實際營業額...等 隱藏
            $('#DFJobAssignLogsiTurnoverReal').closest('td').hide();
            $('#DFJobAssignLogssDeptName').closest('td').prev('td').hide();//部門單位...等 隱藏
            $('#DFJobAssignLogssDeptName').closest('td').hide();
            $('#DFJobAssignLogsbHandOver').closest('td').prev('td').hide();//有無遞補隱藏
            $('#DFJobAssignLogsbHandOver').closest('td').hide();
            $('#DFJobAssignLogsbRefund').closest('td').prev('td').hide();//是否退費隱藏
            $('#DFJobAssignLogsbRefund').closest('td').hide();
            $('#DFJobAssignLogsPayableDate').closest('td').prev('td').hide();//應付款日隱藏
            $('#DFJobAssignLogsPayableDate').closest('td').hide();
            $('#DFJobAssignLogsAssureDay').closest('td').prev('td').hide();//保證天數隱藏
            $('#DFJobAssignLogsAssureDay').closest('td').hide();
            $('#DFJobAssignLogsDeadline').closest('td').prev('td').hide();//保証截止日隱藏
            $('#DFJobAssignLogsDeadline').closest('td').hide();
            $('#DFJobAssignLogsDraft').closest('td').prev('td').hide();//成案機率隱藏
            $('#DFJobAssignLogsDraft').closest('td').hide();
            $('#DFJobAssignLogsDraftMonth').closest('td').prev('td').hide();//成案月份隱藏
            $('#DFJobAssignLogsDraftMonth').closest('td').hide();

            //1推薦,2面試,3錄取,4未錄取,5報到,6未報到,7離職
            if (AssignID == "1") {
                $('#DFJobAssignLogsAssignContent').closest('td').prev('td').show();//推薦評估顯示
                $('#DFJobAssignLogsAssignContent').closest('td').show();//推薦評估顯示   
                $('#DFJobAssignLogsDraft').closest('td').prev('td').show();//成案機率顯示
                $('#DFJobAssignLogsDraft').closest('td').show();
                $('#DFJobAssignLogsDraftMonth').closest('td').prev('td').show();//成案月份顯示
                $('#DFJobAssignLogsDraftMonth').closest('td').show();

            }
            if (AssignID == "2") {
                $('#DFJobAssignLogsInterviewTime').closest('td').prev('td').show();//面試時間顯示
                $('#DFJobAssignLogsInterviewTime').closest('td').show();//面試時間顯示                  
            }
            if (AssignID == "4" || AssignID == "6") {
                $('#DFJobAssignLogsAssignReason').closest('td').prev('td').show();//原因顯示
                $('#DFJobAssignLogsAssignReason').closest('td').show();//原因顯示                  
            }
            if (AssignID == "5") {//5報到
                $('#DFJobAssignLogssDeptName').closest('td').prev('td').show();//部門單位...等 額顯示
                $('#DFJobAssignLogssDeptName').closest('td').show();
                $('#DFJobAssignLogsiTurnoverReal').closest('td').prev('td').show();//實際營業額...等 顯示
                $('#DFJobAssignLogsiTurnoverReal').closest('td').show();
                $('#DFJobAssignLogsiTurnoverReal').closest('td').prev('td').html('實際營業額');
                $('#DFJobAssignLogsPayableDate').closest('td').prev('td').show();//應付款日顯示
                $('#DFJobAssignLogsPayableDate').closest('td').show();
                $('#DFJobAssignLogsAssureDay').closest('td').prev('td').show();//保證天數顯示
                $('#DFJobAssignLogsAssureDay').closest('td').show();
                $('#DFJobAssignLogsDeadline').closest('td').prev('td').show();//保証截止日顯示
                $('#DFJobAssignLogsDeadline').closest('td').show();

            }
            if (AssignID == "7") {//7離職
                $('#DFJobAssignLogsbHandOver').closest('td').prev('td').show();//有無遞補顯示
                $('#DFJobAssignLogsbHandOver').closest('td').show();
                $('#DFJobAssignLogsbRefund').closest('td').prev('td').show();//是否退費顯示
                $('#DFJobAssignLogsbRefund').closest('td').show();
                $('#DFJobAssignLogsiTurnoverReal').closest('td').prev('td').show();//實際營業額顯示
                $('#DFJobAssignLogsiTurnoverReal').closest('td').show();//實際營業額顯示  
                $('#DFJobAssignLogsiTurnoverReal').closest('td').prev('td').html('退回營業額');

            }
            //
            if (AssignID == "") {
                $("#DFJobAssignLogsAssignID").combobox('setValue', "");
                $("#DFJobAssignLogsAssignContent").val("");
                $("#DFJobAssignLogsAssignExplain").val("");
                $('#DFJobAssignLogsInterviewTime').val("");
                $("#DFJobAssignLogsAssignReason").combobox('setValue', "");
                $("#DFJobAssignLogsratioReal").numberbox('setValue', 0);//四捨五入      
                $("#DFJobAssignLogsratioReal").val(0);
                $("#DFJobAssignLogsAmountReal").numberbox('setValue', 0);//四捨五入      
                $("#DFJobAssignLogsAmountReal").val(0);
                $("#DFJobAssignLogsAssignFile").val("");
                $("#DFJobAssignLogsbHandOver").checkbox('setValue', false);//有無遞補
                $("#DFJobAssignLogsbRefund").checkbox('setValue', false);//是否退費
                $("#DFJobAssignLogsPayableDate").datebox('setValue', "");//應付款日
                $("#DFJobAssignLogsAssureDay").numberbox('setValue', "");//保證天數
                $("#DFJobAssignLogsAssureDay").val("");
                $("#DFJobAssignLogsDeadline").val("");//保証截止日
                $("#DFJobAssignLogsDraft").numberbox("");
                $("#DFJobAssignLogsDraftMonth").numberbox("");


            }
            
        }
        //推薦作業檢查
        function OnAppyAssignLogs() {
            var AssignID = $('#DFJobAssignLogsAssignID').combobox('getValue');
            //檢查第一筆要為推薦狀態
            //by人才去看
            if (CheckJobAssignLogsCount() == "0") {
                if (AssignID != "1")//---1推薦狀態
                {
                    alert('請先新增狀態為推薦的紀錄！');
                    return false;
                }
            }
           
            if (AssignID == "5")//---5報到狀態
            {
                //實際營業額檢查
                if ($('#DFJobAssignLogsiTurnoverReal').val() == "") {
                    alert('請填寫實際營業額！');
                    return false;
                }
                //求<=報到日期是否有錄取的紀錄
                if (ReturnJobAssignLogsAdmitCount() == "0") {
                    alert('請先新增狀態為錄取的紀錄！');
                    return false;
                }
            }
            //4未錄取、6未報到=>原因檢查
            if (AssignID == "4" || AssignID == "6") {
                if ($('#DFJobAssignLogsAssignReason').combobox('getValue') == "") {
                    alert('請選擇原因！');
                    return false;
                }
            }
            //7離職
            if (AssignID == "7")//---7離職狀態
            {
                var bRefund = $("#DFJobAssignLogsbRefund").checkbox('getValue')//退費勾選
                if (bRefund == 1) {
                    //退費勾選後要檢查需填寫退回營業額
                    var iTurnover = $("#DFJobAssignLogsiTurnoverReal").numberbox('getValue');//退回營業額	                    
                    if (iTurnover == "0") {
                        alert('請填寫退回營業額！');
                        return false;
                    }
                    //離職日期若大於報到日期+保證天數-1，要檢查不能勾選退費欄位
                    if (ReturnJobAssignLogsAssureDayCount() == "1") {
                        alert('已超過保證天數，不可勾選退費！');
                        return false;
                    }
                }
            }

        }
        //檢查第一筆要為推薦狀態
        function CheckJobAssignLogsCount() {
            var iCount;
            var JobID = $('#dataFormMaster3JobID').val();
            var UserID = $('#DFJobAssignLogsUserID').refval('getValue');//選擇人才
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sHUTUser.HUT_JobAssignLogs', //連接的Server端，command
                data: "mode=method&method=" + "ReturnJobAssignLogsCount" + "&parameters=" + JobID + "," + UserID,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    iCount = data
                },
            });
            return iCount;
        }
        //求<=報到日期是否有錄取的紀錄
        function ReturnJobAssignLogsAdmitCount() {
            var iCount;
            var JobID = $('#dataFormMaster3JobID').val();
            var UserID = $('#DFJobAssignLogsUserID').refval('getValue');//選擇人才
            var dDate = $('#DFJobAssignLogsAssignTime').datebox('getValue');;//報到日期

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sHUTUser.HUT_JobAssignLogs', //連接的Server端，command
                data: "mode=method&method=" + "ReturnJobAssignLogsAdmitCount" + "&parameters=" + JobID + "," + UserID + "," + dDate,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    iCount = data
                },
            });
            return iCount;
        }
        //離職日期若大於報到日期+保證天數-1，要檢查不能勾選退費欄位
        function ReturnJobAssignLogsAssureDayCount() {
            var iCount;
            var JobID = $('#dataFormMaster3JobID').val();
            var UserID = $('#DFJobAssignLogsUserID').refval('getValue');//選擇人才
            var dDate = $('#DFJobAssignLogsAssignTime').datebox('getValue');;//離職日期

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sHUTUser.HUT_JobAssignLogs', //連接的Server端，command
                data: "mode=method&method=" + "ReturnJobAssignLogsAssureDayCount" + "&parameters=" + JobID + "," + UserID + "," + dDate,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    iCount = data
                },
            });
            return iCount;
        }
        //---------------------------------------推薦作業權限控制-----------------------------------------------------
        //控制是否可以修改 (推薦顧問、執案顧問、建立者)
        function AssignUpdateRow(rowData) {
            var username = getClientInfo("username");
            //alert(rowData.AssignHunterName + rowData.JobHunterName + rowData.LastUpdateBy)
            //if (rowData.AssignHunterName != username && rowData.JobHunterName != username && rowData.CreateBy != username && rowData.JBERPSalseName != username) {
            //    alert('無編輯權限！');
            //    return false; //取消編輯的動作 
            //}
            if (GetSalesTeamID() != rowData.HunterSalesTeamID) {
                alert('無編輯權限！');
                return false; //取消編輯的動作 
            }
        }

        //控制是否可以刪除 
        function AssignDeleteRow(rowData) {
            //var username = getClientInfo("username");
            //if (rowData.AssignHunterName != username && rowData.JobHunterName != username && rowData.CreateBy != username) {
            if (GetSalesTeamID() != rowData.HunterSalesTeamID) {
                alert('無刪除權限！');
                return false; //取消編輯的動作 
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------
        //Grid 報告連結
        function RecommendLink(value, row, index) {
            if (row.AssignID == "1") {//推薦=>推薦報告
                return $('<a>', { href: 'javascript:void(0)', name: 'RecommendLink', onclick: 'OpenReport(' + index + ',1)' }).linkbutton({ plain: false, text: '產生報告' })[0].outerHTML;
            } else if (row.AssignID == "5" && row.SalesID == null) {//5報到狀態下=>請款明細,銷貨申請
                return $('<a>', { href: 'javascript:void(0)', name: 'PleasePayLink', onclick: 'OpenReport(' + index + ',5)' }).linkbutton({ plain: false, text: '請款明細' })[0].outerHTML +
                    $('<a>', { href: 'javascript:void(0)', name: 'SalesLink', onclick: 'LinkSales(' + index + ',5)' }).linkbutton({ plain: false, text: '銷貨申請' })[0].outerHTML;
            } else if (row.AssignID == "5") {//5報到狀態下=>請款明細
                return $('<a>', { href: 'javascript:void(0)', name: 'PleasePayLink', onclick: 'OpenReport(' + index + ',5)' }).linkbutton({ plain: false, text: '請款明細' })[0].outerHTML;
            } else return "";
        }
        //推薦報告
        function OpenReport(index, sAssignID) {
            $("#DGJobAssignLogs").datagrid('selectRow', index);
            var rows = $("#DGJobAssignLogs").datagrid('getSelected');

            var UserID = rows.UserID;
            var JobID = rows.JobID;
            var AutoKey = rows.AutoKey;
            var stitle = "";
            var FileName = "";
            if (sAssignID == 1) {
                FileName = $('#dataFormMaster3JobName').val() + "-" + rows.NameC;
                var url = "../JB_ADMIN/REPORT/JBHunter/RecommendReport.aspx?FileName=" + FileName + "&UserID=" + UserID + "&JobID=" + JobID + "&AutoKey=" + AutoKey;
                stitle = "推薦報告";
            } else if (sAssignID == 5) {//5報到狀態下顯示
                FileName = $('#dataFormMaster3JobName').val() + " 人才介紹請款明細表";
                var url = "../JB_ADMIN/REPORT/JBHunter/PleasePayReport.aspx?FileName=" + FileName + "&UserID=" + UserID + "&JobID=" + JobID + "&AutoKey=" + AutoKey;
                stitle = "請款明細";
            }

            var height = $(window).height() - 50;
            var height2 = $(window).height() - 90;
            var width = $(window).width() - 230;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                //top:0,
                width: width,
                title: stitle,
                //maximizable: true                              
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="98%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('open');
        }
        //--------------------------銷貨收入申請---------------------------------------------------------------------
        //呼叫銷貨收入申請視窗
        function LinkSales(index) {
            $("#DGJobAssignLogs").datagrid('selectRow', index);
            var rows = $("#DGJobAssignLogs").datagrid('getSelected');
            var iTurnoverReal = rows.iTurnoverReal;
            if (iTurnoverReal == "0") {
                alert('請先填寫實際營業額！');
            } else {
                openForm('#JQDialogAssignLogs3', rows, "updated", 'dialog');
            }
        }
        //檢查字串是否符合發票年月
        function CheckStrWildWord(str) {
            var r = str.match(/^(\d{4})(\/)(0[1-9]|1[0-2])$/);
            if (r == null) return false;
            var d = new Date(r[1], (r[3] - 1), 1);
            return (d.getFullYear() == r[1] && d.getMonth() == (r[3] - 1) && d.getDate() == 1);
        }
        //預設發票年月
        function OnLoadDFJobAssignLogs3() {
            var rows = $("#DGJobAssignLogs").datagrid('getSelected');
            //預設發票年月=>報到日期
            var sDate = new Date(rows.AssignTime);
            var date1 = $.jbjob.Date.DateFormat(sDate, 'yyyy/MM/dd').substring(0, 7);
            $("#DFJobAssignLogs3InvoiceYM").val(date1);//發票年月
            //預設業務人員
            var sUserID = getClientInfo("UserID");
            var data = $("#DFJobAssignLogs3SalesID").combobox('getData');
            for (var i = 0; i < data.length; i++) {
                if (data[i].EmpID == sUserID) {
                    $("#DFJobAssignLogs3SalesID").combobox('setValue', data[i].EmpID);
                }
            }
        }
        //銷貨收入申請
        function InsertSalesMaster() {
            var pre = confirm("確定申請?");
            if (pre == true) {
                var iAutoKey = $("#DFJobAssignLogs3AutoKey").val();
                //業務人員工號
                var SalesID = $("#DFJobAssignLogs3SalesID").combobox('getValue');
                //發票年月
                var InvoiceYM = $("#DFJobAssignLogs3InvoiceYM").val();
                //夾檔(取得上傳檔案的文件名稱)
                var infofileUpload = $('#DFJobAssignLogs3Attach');
                var Attach = $('.info-fileUpload-value', infofileUpload.next()).val();
                if (SalesID == "" || InvoiceYM == "") {
                    alert('業務人員或發票年月不可以空白！');
                } else {
                    closeForm('#JQDialogAssignLogs3');

                    $.messager.progress({ title: 'Please waiting', msg: '資料處理中...' });//進度條開始

                    setTimeout(function () {
                        $.ajax({
                            type: "POST",
                            url: '../handler/jqDataHandle.ashx?RemoteName=sHUTUser.HUT_User', //連接的Server端，command
                            data: "mode=method&method=" + "InsertSalesMaster" + "&parameters=" + iAutoKey + "," + SalesID + "," + InvoiceYM + "," + Attach,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                            cache: false,
                            async: true,//設定為非同步
                            success: function (data) {
                                closeForm('#JQDialogAssignLogs3');
                                $('#DGJobAssignLogs').datagrid("reload");

                                $.messager.progress('close'); //進度條結束

                            }
                        });
                    }, 1000);


                }
            }

        }
        //關閉職缺
        function CloseJob(val, row, index) {
            var JobID = row.JobID;
            var CustID = row.CustID;
            return $('<a>', { href: "#", onclick: "OpenResume('" + CustID + "'," + JobID + ")", theData: row.JobID }).linkbutton({ text: "<img src=img/Record.png></a><b><div style=\"color:Red\"></div></b>", plain: true })[0].outerHTML;
        }



    </script>
   
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td class="auto-style1">
                        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
                        <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCustomersJobs.HUT_Job" runat="server" AutoApply="True" 
                            DataMember="HUT_Job" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                            Title="職缺資訊" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,40,60,80,100" PageSize="10" QueryAutoColumn="False" QueryLeft="10px" QueryMode="Panel" QueryTop="10px" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" ColumnsHibeable="False" RecordLockMode="None" Width="1050px" BufferView="False" NotInitGrid="False" RowNumbers="True" OnUpdate="JobUpdateRow" OnDelete="JobDeleteRow" Height="400px">
                            <Columns>
                                    <JQTools:JQGridColumn Alignment="right" Caption="職缺代號" Editor="numberbox" FieldName="JobID" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="False" Width="60" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="保密" Editor="text" FieldName="sJobKeepType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="等級" Editor="text" FieldName="JobGrade" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="33">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="客戶" Editor="text" EditorOptions="" FieldName="CustID" Width="120" Visible="False" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="172" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="完成度" Editor="text" FieldName="CreateStatus" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="45">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="推薦人數" Editor="text" FieldName="iUser" FormatScript="AssignLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60"></JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="ShortCustName" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="True" Visible="True" Width="60" FormatScript="OpenCustTab" />
                                    <JQTools:JQGridColumn Alignment="left" Editor="text" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="65" Caption="聯繫人員" FieldName="UpdateBy">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="執案顧問" Editor="text" EditorOptions="" FieldName="HunterName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="開/關缺日" Editor="text" EditorOptions="" FieldName="DateString" Visible="True" Width="78" Format="" FormatScript="DateLogLink" Sortable="True" />
<%--                                <JQTools:JQGridColumn Alignment="center" Caption="失效" Editor="text" FieldName="sJobStatus" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" FormatScript="CloseJob"></JQTools:JQGridColumn>--%>
                                   
                                 <JQTools:JQGridColumn Alignment="center" Caption="執案天數" Editor="text" FieldName="sDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="" Visible="True" Width="55">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="right" Caption="預估營業額" Editor="numberbox" FieldName="Amount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="sum" Visible="True" Width="70" Format="N" OnTotal="sum">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="需求數" Editor="numberbox" FieldName="JobNeedCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="48">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="上班地點" Editor="text" FieldName="JobWorkArea" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="更新日期" Editor="datebox" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="64" Format="yyyy/mm/dd">
                                    </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="更新人員" Editor="text" EditorOptions="" FieldName="LastUpdateBy" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="55" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增職缺" />
                                <JQTools:JQToolItem Icon="icon-copy" ItemType="easyui-linkbutton" OnClick="CopyJob" Text="複製職缺" />
<%--                                <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢條件" />                                                                                                                            --%>
                                <JQTools:JQToolItem ID="JQToolItem2" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="OpenNAR" Text="NAR"  />
                                <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                            </TooItems>
                            <QueryColumns>
                                <JQTools:JQQueryColumn Caption="客戶查詢" Condition="=" DataType="string" Editor="inforefval" FieldName="CustID" NewLine="False" RemoteMethod="False" Width="200" AndOr="" EditorOptions="title:'選擇客戶',panelWidth:390,remoteName:'sCustomersJobs.View_HUT_Customer',tableName:'View_HUT_Customer',columns:[{field:'CustTaxNo',title:'統一編號',width:95,align:'center',table:'',isNvarChar:false,queryCondition:''},{field:'CustName',title:'客戶名稱',width:250,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustID',textField:'CustName',valueFieldCaption:'客戶編號',textFieldCaption:'客戶名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'true'" Span="0" />
                                <JQTools:JQQueryColumn Caption="職缺名稱" Condition="%%" DataType="string" Editor="text" FieldName="JobName" NewLine="False" RemoteMethod="False" Width="90" AndOr="and" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="執案顧問" Condition="=" DataType="string" DefaultValue="" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="105" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="業務單位" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="上班地點" Condition="%" DataType="string" Editor="text" FieldName="JobWorkArea" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="110" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="職缺狀態" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'開',selected:'false'},{value:'2',text:'關',selected:'false'},{value:'3',text:'開發中',selected:'false'},{value:'0',text:'待新增',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobStatus" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="等級" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'A',text:'A',selected:'false'},{value:'B',text:'B',selected:'false'},{value:'C',text:'C',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:100" FieldName="JobGrade" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="105" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="保密" Condition="%" DataType="string" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="JobKeepType" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="35" />
                            </QueryColumns>
                        </JQTools:JQDataGrid>

                        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="職缺資料" DialogLeft="2px" DialogTop="1px" Width="1050px" Wrap="False" EditMode="Dialog">
                            <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HUT_Job" HorizontalColumnsCount="7" RemoteName="sCustomersJobs.HUT_Job" Closed="False" ContinueAdd="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" OnApplied="MasterGridReload" disapply="False" IsRejectON="False"  IsAutoPause="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" ParentObjectID="" ChainDataFormID="" OnLoadSuccess="OnLoadDF">
                                <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="inforefval" EditorOptions="title:'選擇客戶',panelWidth:390,remoteName:'sCustomersJobs.View_HUT_Customer',tableName:'View_HUT_Customer',columns:[{field:'CustTaxNo',title:'統一編號',width:95,align:'center',table:'',isNvarChar:false,queryCondition:''},{field:'CustName',title:'客戶名稱',width:250,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'sCustID',value:'CustID'}],whereItems:[],valueField:'CustID',textField:'CustName',valueFieldCaption:'客戶編號',textFieldCaption:'客戶名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,onSelect:OnSelectInterview,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="CustID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="248" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺性質" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'JobTypeName',remoteName:'sJobType.HUT_JobType',tableName:'HUT_JobType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="140" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="保密" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'keepType',remoteName:'sCustomersJobs.HUT_ZKeepType',tableName:'HUT_ZKeepType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobKeepType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="140" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="急迫性" Editor="infocombobox" EditorOptions="items:[{value:'急',text:'急',selected:'false'},{value:'一般',text:'一般',selected:'false'},{value:'不急',text:'不急',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Urgency" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="95" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="開發中" Editor="checkbox" FieldName="bDevelopment" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" EditorOptions="on:1,off:0" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="248" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="執案顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="140" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="協助顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterIDHelp" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="140" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="助理顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterIDAssist" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="95" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="需要協助" Editor="checkbox" FieldName="bHelp" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" EditorOptions="on:1,off:0" />
                                        <JQTools:JQFormColumn Alignment="right" Caption="預估營業額" Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="iTurnover" MaxLength="0" NewRow="True" OnBlur="OnBluriTurnover" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="上班地點" Editor="text" FieldName="JobWorkArea" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="業務單位" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="100" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="items:[{value:'A',text:'A',selected:'false'},{value:'B',text:'B',selected:'false'},{value:'C',text:'C',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="JobGrade" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="JobNeedCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="35" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                                        <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" EditorOptions="precision:2,groupSeparator:',',prefix:''" FieldName="ratio" MaxLength="0" NewRow="False" OnBlur="OnBluriTurnover" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="38" />
                                        <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="Amount" Format="" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="60" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="所需條件" Editor="textarea" EditorOptions="height:100" FieldName="JobRequirement" MaxLength="2000" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="430" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="薪資福利" Editor="textarea" EditorOptions="height:100" FieldName="JobFare" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="400" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="所需條件" Editor="textarea" EditorOptions="height:100" FieldName="JobRequirementN" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="430" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="薪資福利" Editor="textarea" EditorOptions="height:100" FieldName="JobFareN" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="400" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職務說明" Editor="textarea" EditorOptions="height:150" FieldName="JobWorkContent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="430" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺備註" Editor="textarea" EditorOptions="height:150" FieldName="JobNotes" MaxLength="0" ReadOnly="False" Span="3" Visible="True" Width="400" NewRow="False" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="inforefval" EditorOptions="title:'選擇其他職缺帶入面談事項內容(無面談事項不顯示)',panelWidth:620,remoteName:'sCustomersJobs.infoSelectJobInterview',tableName:'infoSelectJobInterview',columns:[{field:'JobName',title:'職缺名稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'JobInterview',title:'面談事項',width:340,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'JobInterview',value:'JobInterview'}],whereItems:[],valueField:'CustID',textField:'JobName',valueFieldCaption:'客戶代號',textFieldCaption:'職務',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="JobInterviewRef" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="280" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="面談事項" Editor="textarea" EditorOptions="height:150" FieldName="JobInterview" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="430" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="招募策略" Editor="textarea" EditorOptions="height:150" FieldName="JobRecruit" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="400" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺機會" Editor="textarea" EditorOptions="height:150" FieldName="JobOpportunity" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="430" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺風險" Editor="textarea" EditorOptions="height:150" FieldName="JobRisk" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="400" />

                                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" MaxLength="20" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" Format="" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" MaxLength="20" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="修正日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" FieldName="LastUpdateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="240" EditorOptions="" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="text" FieldName="sCustID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="CustomerTelArea" Editor="text" FieldName="CustomerTelArea" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="CustomerTel" Editor="text" FieldName="CustomerTel" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                </Columns>

                            </JQTools:JQDataForm>

                            <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQDefaultColumn DefaultValue="0" FieldName="JobID" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" CarryOn="False" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="1" FieldName="IsActive" RemoteMethod="True" />
                                </Columns>
                            </JQTools:JQDefault>
                            <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="JobName" RemoteMethod="True" ValidateMessage="職缺名稱不可空白！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CustID" RemoteMethod="True" ValidateMessage="請選擇客戶名稱！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesTeamID" RemoteMethod="True" ValidateMessage="請選擇業務單位！" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>                                                           
                                     
                            <JQTools:JQDataGrid ID="DGRequirementRecord" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_JobRequirementRecord" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogRequirementRecord" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnDelete="OnDeletRequirementRecord" OnUpdate="" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.HUT_Job" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                                <Columns>
                                    <JQTools:JQGridColumn Alignment="center" Caption="NAR建立日期" Editor="datebox" FieldName="RequirementDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="NAR內容" Editor="textarea" FieldName="Notes" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="645">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="更新日期" Editor="text" FieldName="UpdateDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="更新人員" Editor="text" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="JobID" ParentFieldName="JobID" />
                                    <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                </RelationColumns>
                                <TooItems>
                                    <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增NAR(Needs Analysis Report)" Visible="True" />
                                </TooItems>
                            </JQTools:JQDataGrid>
                            <JQTools:JQDialog ID="JQDialogRequirementRecord" runat="server" BindingObjectID="DFRequirementRecord" DialogLeft="100px" DialogTop="70px" Title="NAR紀錄維護" Width="850px">
                                <JQTools:JQDataForm ID="DFRequirementRecord" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_JobRequirementRecord" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedRequirementRecord" ParentObjectID="dataFormMaster" RemoteName="sCustomersJobs.HUT_Job" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="日期" Editor="datebox" FieldName="RequirementDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="NAR內容" Editor="textarea" EditorOptions="height:190" FieldName="Notes" MaxLength="5000" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="720" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="UpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateDate" Editor="text" FieldName="UpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    </Columns>
                                    <RelationColumns>
                                        <JQTools:JQRelationColumn FieldName="JobID" ParentFieldName="JobID" />
                                        <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                    </RelationColumns>
                                </JQTools:JQDataForm>
                                <JQTools:JQDefault ID="JQDefault3" runat="server" BindingObjectID="DFRequirementRecord" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="RequirementDate" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCODE" FieldName="UserID" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="UpdateBy" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="UpdateDate" RemoteMethod="True" />
                                    </Columns>
                                </JQTools:JQDefault>
                                <JQTools:JQValidate ID="JQValidate3" runat="server" BindingObjectID="DFRequirementRecord" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RequirementDate" RemoteMethod="True" ValidateMessage="請選擇日期！" ValidateType="None" />
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Notes" RemoteMethod="True" ValidateMessage="內容不可空白！" ValidateType="None" />
                                    </Columns>
                                </JQTools:JQValidate>
                                <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="DFRequirementRecord" FieldName="AutoKey" NumDig="1" />
                            </JQTools:JQDialog>
                            <%--<JQTools:JQDialog ID="JQDialogNARJoin" runat="server" BindingObjectID="" DialogLeft="80px" DialogTop="50px" ShowSubmitDiv="False" Title="NAR匯總表" Width="820px">
                                <JQTools:JQDataGrid ID="DGRequirementRecordJoin" runat="server" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="infoRequirementRecordJoin" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnUpdate="" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.infoRequirementRecordJoin" RowNumbers="False" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="91%">
                                    <Columns>
                                        <JQTools:JQGridColumn Alignment="center" Caption="日期" Editor="text" FieldName="RequirementDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="70">
                                        </JQTools:JQGridColumn>
                                        <JQTools:JQGridColumn Alignment="left" Caption="內容" Editor="textarea" EditorOptions="height:300" FieldName="Notes" FormatScript="ShowJoin" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="690">
                                        </JQTools:JQGridColumn>
                                        <JQTools:JQGridColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                        </JQTools:JQGridColumn>
                                    </Columns>                                                                                        
                                </JQTools:JQDataGrid>
                            </JQTools:JQDialog>--%>
                                 
                            <JQTools:JQDataGrid ID="DGCustomerFile" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_JobFile" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogCustomerFile" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnUpdate="" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.HUT_Job" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="550px" OnDelete="OnDeletFile">
                                <Columns>
                                    <JQTools:JQGridColumn Alignment="left" Caption="下載檔案" Editor="text" EditorOptions="" FieldName="JobFile" Format="download,folder:Files/Hunter/Job" MaxLength="150"  ReadOnly="False" Width="276" Visible="True" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="組織圖" Editor="checkbox" FieldName="bOrg" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="73" FormatScript="sCheckBox">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="text" FieldName="UpdateDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="建立者" Editor="text" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                    <JQTools:JQRelationColumn FieldName="JobID" ParentFieldName="JobID" />
                                </RelationColumns>
                                <TooItems>
                                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增檔案(10MB)" />
                                </TooItems>
                            </JQTools:JQDataGrid>
                            <JQTools:JQDialog ID="JQDialogCustomerFile" runat="server" BindingObjectID="DFCustomerFile" DialogLeft="240px" DialogTop="120px" Title="職缺檔案維護" Width="550px">
                                <JQTools:JQDataForm ID="DFCustomerFile" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_JobFile" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedCustomerFile" ParentObjectID="DGCustomerFile" RemoteName="sCustomersJobs.HUT_Job" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApply="OnApplyCustomerFile">
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="上傳檔案" Editor="infofileupload" FieldName="JobFile" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="300" EditorOptions="filter:'docx|doc|xlsx|xls|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|image|tif|txt',isAutoNum:true,upLoadFolder:'Files/Hunter/Job',showButton:true,showLocalFile:true,fileSizeLimited:'10000'" />
                                        <JQTools:JQFormColumn Alignment="center" Caption="組織圖" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="bOrg" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="45" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="UpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateDate" Editor="text" FieldName="UpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    </Columns>
                                    <RelationColumns>
                                        <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                        <JQTools:JQRelationColumn FieldName="JobID" ParentFieldName="JobID" />
                                    </RelationColumns>
                                </JQTools:JQDataForm>
                            </JQTools:JQDialog>
                            <JQTools:JQDefault ID="JQDefaultCustomerFile" runat="server" BindingObjectID="DFCustomerFile" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCODE" FieldName="UserID" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="UpdateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="UpdateDate" RemoteMethod="True" />
                                </Columns>
                            </JQTools:JQDefault>
                            <JQTools:JQAutoSeq ID="JQAutoSeq5" runat="server" BindingObjectID="DFCustomerFile" FieldName="AutoKey" NumDig="1" />
                            <JQTools:JQValidate ID="JQValidateCustomerFile" runat="server" BindingObjectID="DFCustomerFile" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="JobFile" RemoteMethod="True" ValidateMessage="請選擇檔案！" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>
                            <br />                                                         

                        </JQTools:JQDialog>

                        <JQTools:JQDialog ID="Dialog_DateLog" runat="server" BindingObjectID="dataFormMaster2" Title="開/關缺紀錄" DialogLeft="40px" DialogTop="30px" Width="950px" Wrap="False" EditMode="Dialog" ShowSubmitDiv="False">
                            <JQTools:JQDataForm ID="dataFormMaster2" runat="server" AlwaysReadOnly="False" ChainDataFormID="" Closed="False" ContinueAdd="False" DataMember="HUT_Job" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="6" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sCustomersJobs.HUT_Job" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoadSuccessMDate">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="職缺編號" Editor="text" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="255" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="iDate" Editor="text" FieldName="iDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                </Columns>
                            </JQTools:JQDataForm>
                            <JQTools:JQDataGrid ID="dataGrid_DateLog" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" data-options="pagination:true,view:commandview" DataMember="HUT_JobDateLog" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogDateLog" EditMode="Dialog" EditOnEnter="False" Height="335px" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnDeleted="OnAppliedDateLog" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster2" QueryAutoColumn="False" QueryLeft="80px" QueryMode="Panel" QueryTitle="查詢條件" QueryTop="80px" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.HUT_Job" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="100%" OnInsert="OnInsertDateLog">
                                <Columns>
                                    <JQTools:JQGridColumn Alignment="left" Caption="開缺日" Editor="datebox" FieldName="JobDeclareDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="88">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="關閉日" Editor="datebox" FieldName="JobCloseDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="88">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="關閉分類" Editor="text" FieldName="JobCloseReason" Frozen="False" IsNvarChar="True" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="210">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="關閉說明" Editor="text" FieldName="JobCloseDescription" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="375">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="UpdateBy" MaxLength="0" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="UpateDate" Editor="text" FieldName="UpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="JobID" ParentFieldName="JobID" />
                                    <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                </RelationColumns>
                                <TooItems>
                                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="開關缺維護" />
                                </TooItems>
                            </JQTools:JQDataGrid>
                            <JQTools:JQDialog ID="JQDialogDateLog" runat="server" BindingObjectID="DFDateLog" DialogLeft="165px" DialogTop="90px" Title="開關缺維護" Width="550px">
                                <JQTools:JQDataForm ID="DFDateLog" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_JobDateLog" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedDateLog" OnLoadSuccess="OnLoadSuccessDateLog" ParentObjectID="dataFormMaster2" RemoteName="sCustomersJobs.HUT_Job" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" RowSpan="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="開缺日" Editor="datebox" FieldName="JobDeclareDate" MaxLength="20" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="關閉日 " Editor="datebox" FieldName="JobCloseDate" MaxLength="100" NewRow="False" Span="1" Visible="True" Width="130" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="關閉分類" Editor="infocombobox" EditorOptions="valueField:'JobCloseReason',textField:'JobCloseReason',remoteName:'sCustomersJobs.infoJobCloseReason',tableName:'infoJobCloseReason',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobCloseReason" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="400" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="關閉說明" Editor="textarea" EditorOptions="height:90" FieldName="JobCloseDescription" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="400" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="UpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateDate" Editor="text" FieldName="UpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    </Columns>
                                    <RelationColumns>
                                        <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                        <JQTools:JQRelationColumn FieldName="JobID" ParentFieldName="JobID" />
                                    </RelationColumns>
                                </JQTools:JQDataForm>
                                <JQTools:JQDefault ID="defaultDateLog" runat="server" BindingObjectID="DFDateLog" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="UpdateBy" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="UpdateDate" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn DefaultValue="_USERCODE" FieldName="UserID" RemoteMethod="True" />
                                    </Columns>
                                </JQTools:JQDefault>
                                <JQTools:JQValidate ID="validateDateLog" runat="server" BindingObjectID="DFDateLog" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="JobDeclareDate" RemoteMethod="True" ValidateMessage="" ValidateType="None" />
                                    </Columns>
                                </JQTools:JQValidate>
                                <JQTools:JQAutoSeq ID="JQAutoSeq4" runat="server" BindingObjectID="DFDateLog" FieldName="AutoKey" NumDig="1" />
                            </JQTools:JQDialog>
                        </JQTools:JQDialog>

                <JQTools:JQDialog ID="JQDialogJobAssignLogs" runat="server" BindingObjectID="dataFormMaster3" Title="推薦作業" Width="940px" DialogLeft="66px" DialogTop="30px" Height="440px" EditMode="Dialog" ShowSubmitDiv="False">
                    <JQTools:JQDataForm ID="dataFormMaster3" runat="server" AlwaysReadOnly="False" ChainDataFormID="" Closed="False" ContinueAdd="False" DataMember="HUT_Job" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="6" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sCustomersJobs.HUT_Job" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="職缺編號" Editor="text" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="70" />
                            <JQTools:JQFormColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="275" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="HunterID" Editor="text" FieldName="HunterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <fieldset>
                        <legend>推薦紀錄 </legend>


                        <JQTools:JQDataGrid ID="DGJobAssignLogs" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_JobAssignLogs" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogAssignLogs" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnUpdate="AssignUpdateRow" PageList="5,10,15,20" PageSize="5" Pagination="False" ParentObjectID="dataFormMaster3" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.HUT_Job" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="100%" OnDeleted="OnDeletedAssignLogs" OnDelete="AssignDeleteRow" Height="315px">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="人才姓名" Editor="infocombobox" FieldName="UserID" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="70" EditorOptions="valueField:'UserID',textField:'sName',remoteName:'sCustomersJobs.infoSelectUser',tableName:'infoSelectUser',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="人才姓名" Editor="text" FieldName="NameC" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="狀態" Editor="infocombobox" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'sHUTUser.HUT_ZAssignStep',tableName:'HUT_ZAssignStep',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssignID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="日期" Editor="datebox" EditorOptions="" FieldName="AssignTime" Format="yyyy/mm/dd" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="面試時間" Editor="text" FieldName="InterviewTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="推薦顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="推薦評估" Editor="text" FieldName="AssignContent" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="145">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption=" " Editor="text" FieldName="FileLink" FormatScript="FileLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="檔案下載" Editor="infofileupload" EditorOptions="" FieldName="AssignFile" Format="download,folder:Files/Hunter/Assign" Frozen="False" IsNvarChar="False" MaxLength="150" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85" />
                                <JQTools:JQGridColumn Alignment="center" Caption=" " Editor="text" FieldName="RecommendLink" FormatScript="RecommendLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="170">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="JobID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateby" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="JobID" ParentFieldName="JobID" />
                            </RelationColumns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增推薦紀錄" Enabled="True" Visible="True" />
                                <JQTools:JQToolItem Icon="icon-copy" ItemType="easyui-linkbutton" OnClick="OpenCopyAssign" Text="新增狀態" Enabled="True" Visible="True" />
                            </TooItems>
                        </JQTools:JQDataGrid>
                        <JQTools:JQDialog ID="JQDialogAssignLogs" runat="server" BindingObjectID="DFJobAssignLogs" DialogLeft="120px" DialogTop="40px" Title="推薦紀錄維護" Width="860px" ShowSubmitDiv="True">
                            <JQTools:JQDataForm ID="DFJobAssignLogs" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_JobAssignLogs" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="10" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster3" RemoteName="sCustomersJobs.HUT_Job" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApplied="OnAppliedAssignLogs" OnLoadSuccess="OnLoadAssignLogs" OnApply="OnAppyAssignLogs">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="選擇人才" Editor="inforefval" EditorOptions="title:'選擇人才',panelWidth:380,panelHeight:340,remoteName:'sCustomersJobs.infoSelectUser',tableName:'infoSelectUser',columns:[{field:'sName',title:'中/英文姓名',width:160,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'MobileNo1',title:'電話1',width:140,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'UserID',textField:'sName',valueFieldCaption:'UserID',textFieldCaption:'sName',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="270" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="推薦顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:180" FieldName="HunterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="狀態日期" Editor="datebox" EditorOptions="" FieldName="AssignTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="狀態" Editor="infocombobox" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'sHUTUser.HUT_ZAssignStep',tableName:'HUT_ZAssignStep',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnSelectAssignID,panelHeight:200" FieldName="AssignID" Format="" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="應付款日" Editor="datebox" FieldName="PayableDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="95" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="保證天數" Editor="numberbox" FieldName="AssureDay" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="面試時間" Editor="text" FieldName="InterviewTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="原因" Editor="infocombobox" EditorOptions="valueField:'AssignReason',textField:'AssignReason',remoteName:'sHUTUser.infoHUT_ZAssignStepReason',tableName:'infoHUT_ZAssignStepReason',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssignReason" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="遞補" Editor="checkbox" FieldName="bHandOver" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="退費" Editor="checkbox" FieldName="bRefund" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="保証截止日" Editor="text" FieldName="Deadline" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="95" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="成案機率" Editor="numberbox" FieldName="Draft" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="預計成案年月" Editor="numberbox" FieldName="DraftMonth" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />                                  
                                    <JQTools:JQFormColumn Alignment="left" Caption="上傳報告" Editor="infofileupload" FieldName="AssignFile" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="False" Width="200" EditorOptions="filter:'docx|doc|xlsx|xls|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|image|tif|txt',isAutoNum:true,upLoadFolder:'Files/Hunter/Assign',showButton:true,showLocalFile:true,fileSizeLimited:'2000'" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="推薦評估" Editor="textarea" EditorOptions="height:120" FieldName="AssignContent" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="10" Visible="True" Width="670" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="說明" Editor="textarea" FieldName="AssignExplain" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="10" Visible="True" Width="670" EditorOptions="height:120" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="部門單位" Editor="text" FieldName="sDeptName" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="170" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="sJobName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" FieldName="MonthSalary" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" EditorOptions="precision:0,groupSeparator:',',prefix:''" />
                                    <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="iTurnoverReal" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="80" OnBlur="OnBluriTurnoverReal" />
                                    <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" FieldName="AmountReal" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="60" EditorOptions="precision:0,groupSeparator:',',prefix:''" OnBlur="" />
                                    <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" FieldName="ratioReal" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="38" EditorOptions="precision:2,groupSeparator:',',prefix:''" OnBlur="OnBluriTurnoverReal2" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="JobID" ParentFieldName="JobID" />
                                </RelationColumns>
                            </JQTools:JQDataForm>
                            <JQTools:JQDefault ID="JQDefaultAssignLogs" runat="server" BindingObjectID="DFJobAssignLogs" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="AssignTime" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="LastUpdateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                                </Columns>
                            </JQTools:JQDefault>
                            <JQTools:JQValidate ID="JQValidateAssignLogs" runat="server" BindingObjectID="DFJobAssignLogs" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="UserID" RemoteMethod="True" ValidateMessage="請選擇人才！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="AssignID" RemoteMethod="True" ValidateMessage="請選擇狀態！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="HunterID" RemoteMethod="True" ValidateMessage="請選擇顧問！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="AssignTime" RemoteMethod="True" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>
                            <JQTools:JQAutoSeq ID="JQAutoAssignLogs" runat="server" BindingObjectID="DFJobAssignLogs" FieldName="AutoKey" NumDig="1" />
                        </JQTools:JQDialog>

                        <JQTools:JQDialog ID="JQDialogAssignLogs2" runat="server" BindingObjectID="DFJobAssignLogs2" DialogLeft="250px" DialogTop="100px" ShowSubmitDiv="True" Title="檔案上傳" Width="500px">
                            <JQTools:JQDataForm ID="DFJobAssignLogs2" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_JobAssignLogs" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedAssignLogs" ParentObjectID="dataFormMaster8" RemoteName="sHUTUser.HUT_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="檔案上傳" Editor="infofileupload" EditorOptions="filter:'docx|doc|xlsx|xls|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|image|tif|txt',isAutoNum:true,upLoadFolder:'Files/Hunter/Assign',showButton:true,showLocalFile:true,fileSizeLimited:'2000'" FieldName="AssignFile" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="250" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                                </RelationColumns>
                            </JQTools:JQDataForm>
                            <JQTools:JQValidate ID="JQValidateAssignLogs2" runat="server" BindingObjectID="DFJobAssignLogs2" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="AssignFile" RemoteMethod="True" ValidateMessage="請選擇檔案！" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>
                        </JQTools:JQDialog>

                        <JQTools:JQDialog ID="JQDialogAssignLogs3" runat="server" BindingObjectID="DFJobAssignLogs3" DialogLeft="300px" DialogTop="100px" ShowSubmitDiv="False" Title="銷貨收入申請" Width="500px">
                            <table style="width:100%;">
                                <tr>
                                    <td style="vertical-align: bottom;">
                                        <JQTools:JQDataForm ID="DFJobAssignLogs3" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_JobAssignLogs" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedAssignLogs" OnLoadSuccess="OnLoadDFJobAssignLogs3" ParentObjectID="dataFormMaster8" RemoteName="sHUTUser.HUT_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                                            <Columns>
                                            <JQTools:JQFormColumn Alignment="left" Caption="客戶統編" Editor="text" FieldName="CustTaxNo" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                                            <JQTools:JQFormColumn Alignment="right" Caption="實際營業額" Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="AmountReal" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="業務人員" Editor="infocombobox" EditorOptions="valueField:'EmpID',textField:'HunterName',remoteName:'sHUTUser.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:180" FieldName="SalesID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="發票年月" Editor="text" FieldName="InvoiceYM" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="上傳夾檔" Editor="infofileupload" EditorOptions="filter:'docx|doc|xlsx|xls|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|image|tif|txt',isAutoNum:true,upLoadFolder:'WorkflowFiles',showButton:true,showLocalFile:true,fileSizeLimited:'10000'" FieldName="Attach" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="300" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                            </Columns>
                                            <RelationColumns>
                                                <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                                            </RelationColumns>
                                        </JQTools:JQDataForm>
                                        <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="DFJobAssignLogs3" EnableTheming="True">
                                            <Columns>
                                                <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesID" RemoteMethod="True" ValidateType="None" />
                                                <JQTools:JQValidateColumn CheckMethod="CheckStrWildWord" CheckNull="True" FieldName="InvoiceYM" RemoteMethod="False" ValidateMessage="發票年月格式錯誤!" ValidateType="None" />
                                            </Columns>
                                        </JQTools:JQValidate>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: bottom; text-align: center;"><a class="easyui-linkbutton" data-options="" href="#" onclick="InsertSalesMaster()">銷貨收入申請</a> </td>
                                </tr>
                            </table>
                        </JQTools:JQDialog>

                    </fieldset>
                </JQTools:JQDialog>

                    </td>

                </tr>
                <tr>
                    <td class="auto-style1">
                        &nbsp;</td>

                </tr>
            </table>
        </div>
       
       

    </form>
</body>
</html>
