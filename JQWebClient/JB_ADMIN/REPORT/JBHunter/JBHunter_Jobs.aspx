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
            var iTurnover = $('#dataFormMasteriTurnover').closest('td');
            var ratio = $('#dataFormMasterratio').closest('td').children();
            var Amount = $('#dataFormMasterAmount').closest('td').children();
            iTurnover.append(' * ').append(ratio).append(' = ').append(Amount);
            
            //加上(不可刊登)的欄位
            var HideFieldName = ['JobOpportunity', 'JobRisk', 'JobInterview', 'JobRecruit', 'JobNotes'];
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

            //實際營業額 合併顯示
            var iTurnoverReal = $('#DFJobAssignLogsiTurnoverReal').closest('td');
            var ratioReal = $('#DFJobAssignLogsratioReal').closest('td').children();
            var AmountReal = $('#DFJobAssignLogsAmountReal').closest('td').children();
            iTurnoverReal.append(' * ').append(ratioReal).append(' = ').append(AmountReal);

            //-------------職缺資料傳入客戶代號 => 查詢客戶的職缺---------------------------------------
            var parameter = Request.getQueryStringByName("CustID");
            if (parameter != "") {
                $("#CustID_Query").refval('setValue', parameter);
                queryGrid('#dataGridView');
            }

            $('#JobStatus_Query').combobox('setValue', "");



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

                if (CustID != '') result.push("CustID = '" + CustID + "'");
                if (JobName != '') result.push("JobName like '%" + JobName + "%'");
                if (HunterID != '') result.push("HunterID = " + HunterID);
                if (SalesTeamID != '') result.push("SalesTeamID = " + SalesTeamID);
                if (JobStatus != '') result.push("iDate = " + JobStatus);

                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }        

        //-----------------實際營業額-----------------------------------------------------------------------------------
        function OnBluriTurnoverReal() {
            //var iTurnover = $("#dataFormMaster1iTurnover").val();
            var iTurnover = $("#DFJobAssignLogsiTurnoverReal").numberbox('getValue');
            var ratio = $("#DFJobAssignLogsratioReal").val();
            var Amount = Math.round(iTurnover * ratio);
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
                return "<a href='javascript: void(0)' onclick='LinkDateLog(" + index + ");' style='color:red;'>" + value + "</a>";
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
        //日期編輯時=>開缺日期不能改
        function OnLoadSuccessDateLog() {
            if (getEditMode($("#DFDateLog")) == "inserted") {
                $('#DFDateLogJobDeclareDate').datebox('enable')
            } else {
                $('#DFDateLogJobDeclareDate').datebox('disable')                
            }
            if ($('#DFDateLogJobCloseReason').combobox('getValue') == "") {
                $('#DFDateLogJobCloseReason').combobox('setValue', "");
            }
        }

        function OnAppliedDateLog() {
            $("#dataGrid_DateLog").datagrid('reload');
            $("#dataGridView").datagrid('reload');
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
            var rowcount = $('#dataGridDetail').datagrid('getData').total;
            if (rowcount <= 0) {
                alert('注意!! 沒有可選取職缺資料,本功能無法使用');
                return false;
            }
            var row = $('#dataGridDetail').datagrid('getSelected');
            $('#dataGridDetail').datagrid('appendRow', row);
            row.JobID = 0;
            //新增且同時OPENDATAFORM一筆資料
            openForm('#JQDialog2', row, "inserted", 'dialog');
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
            var AssignID = row.AssignID;//1推薦,2面試,3錄取,4未錄取,5報到,6未報到
            if (AssignID == "1")//&& row.AssignFile=="")
                return "<a href='javascript: void(0)' onclick='LinkFile(" + index + ");'>上傳報告</a>";
            else return "";
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
            //將職缺執案顧問帶入推薦的執案顧問
            var HunterID = $('#dataFormMaster3HunterID').val();
            if (HunterID != "") {
                $("#DFJobAssignLogsHunterID").combobox('setValue', HunterID);
            }
        }
        function OnSelectAssignID(rowData) {
            var AssignID = rowData.AssignID;//1推薦,2面試,3錄取,4報到,5放棄
            var sTitle = "";
            if (AssignID == "1") {
                sTitle = "上傳推薦報告";
            } else if (AssignID == "2") {
                sTitle = "上傳徵信報告";
            } else if (AssignID == "3") {
                sTitle = "上傳錄取聘書";
            } else {
                sTitle = "上傳報告";
            }
            var AssignFile = $('#infoFileUploadDFJobAssignLogsAssignFile').closest('td').prev('td');//改變td前面文字顏色
            AssignFile.empty();
            AssignFile.append(sTitle);
        }
        //Grid 報告連結
        function RecommendLink(value, row, index) {
            if (row.AssignID == "1") {//推薦=>推薦報告
                return $('<a>', { href: 'javascript:void(0)', name: 'RecommendLink', onclick: 'OpenReport(' + index + ',1)' }).linkbutton({ plain: false, text: '產生報告' })[0].outerHTML;
            } else if (row.AssignID == "5") {//報到=>請款明細
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
            if (sAssignID == 1) {
                var FileName = rows.JobName + "-" + $('#dataFormMaster8NameC').val();
                var url = "../JB_ADMIN/REPORT/JBHunter/RecommendReport.aspx?FileName=" + FileName + "&UserID=" + UserID + "&JobID=" + JobID + "&AutoKey=" + AutoKey;
                stitle = "推薦報告";
            } else if (sAssignID == 5) {
                var FileName = rows.JobName + " 人才介紹請款明細表";
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
                            DataMember="HUT_Job" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                            Title="職缺資訊" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="15,30,45,60,65" PageSize="15" QueryAutoColumn="False" QueryLeft="10px" QueryMode="Panel" QueryTop="10px" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" ColumnsHibeable="False" RecordLockMode="None" Width="1050px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                            <Columns>
                                    <JQTools:JQGridColumn Alignment="right" Caption="職缺代號" Editor="numberbox" FieldName="JobID" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="False" Width="60" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="客戶" Editor="text" EditorOptions="" FieldName="CustID" Width="120" Visible="False" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="180" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="推薦人數" Editor="text" FieldName="iUser" FormatScript="AssignLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60"></JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="True" Visible="True" Width="150" FormatScript="OpenCustTab" />
                                    <JQTools:JQGridColumn Alignment="left" Editor="text" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="65" Caption="聯繫人員" FieldName="UpdateBy">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="執案顧問" Editor="text" EditorOptions="" FieldName="HunterName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="開/關缺日" Editor="text" EditorOptions="" FieldName="DateString" Visible="True" Width="78" Format="" FormatScript="DateLogLink" Sortable="True" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="執案天數" Editor="text" FieldName="sDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="" Visible="True" Width="55">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="right" Caption="預估營業額" Editor="numberbox" FieldName="Amount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="sum" Visible="True" Width="70" Format="N">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="需求數" Editor="numberbox" FieldName="JobNeedCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="48">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="上班地點" Editor="text" FieldName="JobWorkArea" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="更新日期" Editor="datebox" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="64" Format="yyyy/mm/dd">
                                    </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="更新人員" Editor="text" EditorOptions="" FieldName="LastUpdateBy" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="60" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增職缺" />
<%--                                <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢條件" />                                                                                                                            --%>
                                <JQTools:JQToolItem ID="JQToolItem2" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="OpenNAR" Text="NAR"  />
                            </TooItems>
                            <QueryColumns>
                                <JQTools:JQQueryColumn Caption="客戶查詢" Condition="=" DataType="string" Editor="inforefval" FieldName="CustID" NewLine="False" RemoteMethod="False" Width="200" AndOr="" EditorOptions="title:'選擇客戶',panelWidth:390,remoteName:'sCustomersJobs.View_HUT_Customer',tableName:'View_HUT_Customer',columns:[{field:'CustTaxNo',title:'統一編號',width:95,align:'center',table:'',isNvarChar:false,queryCondition:''},{field:'CustName',title:'客戶名稱',width:250,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustID',textField:'CustName',valueFieldCaption:'客戶編號',textFieldCaption:'客戶名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'true'" />
                                <JQTools:JQQueryColumn Caption="職缺名稱" Condition="%%" DataType="string" Editor="text" FieldName="JobName" NewLine="False" RemoteMethod="False" Width="90" AndOr="and" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="執案顧問" Condition="=" DataType="string" DefaultValue="" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="105" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="業務單位" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="職缺狀態" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'開',selected:'false'},{value:'2',text:'關',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobStatus" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="70" />
                            </QueryColumns>
                        </JQTools:JQDataGrid>

                        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="職缺資料" DialogLeft="2px" DialogTop="1px" Width="1050px" Wrap="False" EditMode="Dialog">
                            <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HUT_Job" HorizontalColumnsCount="6" RemoteName="sCustomersJobs.HUT_Job" Closed="False" ContinueAdd="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="MasterGridReload" disapply="False" IsRejectON="False"  IsAutoPause="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" ParentObjectID="" ChainDataFormID="" OnLoadSuccess="OnLoadDF">
                                <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="inforefval" EditorOptions="title:'選擇客戶',panelWidth:390,remoteName:'sCustomersJobs.View_HUT_Customer',tableName:'View_HUT_Customer',columns:[{field:'CustTaxNo',title:'統一編號',width:95,align:'center',table:'',isNvarChar:false,queryCondition:''},{field:'CustName',title:'客戶名稱',width:250,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'sCustID',value:'CustID'}],whereItems:[],valueField:'CustID',textField:'CustName',valueFieldCaption:'客戶編號',textFieldCaption:'客戶名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,onSelect:OnSelectInterview,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="CustID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="248" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺性質" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'JobTypeName',remoteName:'sJobType.HUT_JobType',tableName:'HUT_JobType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="140" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="執案顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="135" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="助理顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterIDAssist" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="135" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="248" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="機密性" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'keepType',remoteName:'sCustomersJobs.HUT_ZKeepType',tableName:'HUT_ZKeepType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobKeepType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="140" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="業務單位" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="135" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="急迫性" Editor="infocombobox" EditorOptions="items:[{value:'急',text:'急',selected:'false'},{value:'一般',text:'一般',selected:'false'},{value:'不急',text:'不急',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Urgency" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="135" />
                                        <JQTools:JQFormColumn Alignment="right" Caption="預估營業額" Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="iTurnover" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" OnBlur="OnBluriTurnover" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="上班地點" Editor="text" FieldName="JobWorkArea" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="102" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="需求人數" Editor="text" FieldName="JobNeedCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="需要協助" Editor="checkbox" FieldName="bHelp" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" EditorOptions="on:1,off:0" />
                                        <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" EditorOptions="precision:2,groupSeparator:',',prefix:''" FieldName="ratio" MaxLength="0" NewRow="False" OnBlur="OnBluriTurnover" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="38" />
                                        <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="Amount" Format="" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="60" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="所需條件" Editor="textarea" EditorOptions="height:150" FieldName="JobRequirement" MaxLength="2000" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="430" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職務說明" Editor="textarea" EditorOptions="height:150" FieldName="JobWorkContent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="400" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="薪資福利" Editor="textarea" EditorOptions="height:150" FieldName="JobFare" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="430" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺備註" Editor="textarea" EditorOptions="height:150" FieldName="JobNotes" MaxLength="0" ReadOnly="False" Span="3" Visible="True" Width="400" NewRow="False" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="inforefval" EditorOptions="title:'選擇職缺帶入面談內容',panelWidth:620,remoteName:'sCustomersJobs.infoSelectJobInterview',tableName:'infoSelectJobInterview',columns:[{field:'JobName',title:'職缺名稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'JobInterview',title:'面談事項',width:340,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'JobInterview',value:'JobInterview'}],whereItems:[],valueField:'CustID',textField:'JobName',valueFieldCaption:'客戶代號',textFieldCaption:'職務',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="JobInterviewRef" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="280" />
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
                                    <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                                </Columns>
                            </JQTools:JQDefault>
                            <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="JobName" RemoteMethod="True" ValidateMessage="職缺名稱不可空白！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CustID" RemoteMethod="True" ValidateMessage="請選擇客戶名稱！" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>                                                           
                                     
                            <JQTools:JQDataGrid ID="DGRequirementRecord" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_JobRequirementRecord" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogRequirementRecord" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnDelete="OnDeletRequirementRecord" OnUpdate="" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.HUT_Job" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                                <Columns>
                                    <JQTools:JQGridColumn Alignment="center" Caption="NAR建立日期" Editor="datebox" FieldName="RequirementDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="NAR內容" Editor="textarea" FieldName="Notes" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="660">
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
                                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增檔案(2000k)" />
                                </TooItems>
                            </JQTools:JQDataGrid>
                            <JQTools:JQDialog ID="JQDialogCustomerFile" runat="server" BindingObjectID="DFCustomerFile" DialogLeft="240px" DialogTop="120px" Title="職缺檔案維護" Width="550px">
                                <JQTools:JQDataForm ID="DFCustomerFile" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_JobFile" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedCustomerFile" ParentObjectID="DGCustomerFile" RemoteName="sCustomersJobs.HUT_Job" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApply="OnApplyCustomerFile">
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="上傳檔案" Editor="infofileupload" FieldName="JobFile" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="300" EditorOptions="filter:'docx|doc|xlsx|xls|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|image|tif|txt',isAutoNum:true,upLoadFolder:'Files/Hunter/Job',showButton:true,showLocalFile:true,fileSizeLimited:'2000'" />
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
                            <JQTools:JQDataForm ID="dataFormMaster2" runat="server" AlwaysReadOnly="False" ChainDataFormID="" Closed="False" ContinueAdd="False" DataMember="HUT_Job" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="6" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sCustomersJobs.HUT_Job" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="職缺編號" Editor="text" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="255" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                </Columns>
                            </JQTools:JQDataForm>
                            <JQTools:JQDataGrid ID="dataGrid_DateLog" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" data-options="pagination:true,view:commandview" DataMember="HUT_JobDateLog" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogDateLog" EditMode="Dialog" EditOnEnter="False" Height="335px" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnDeleted="OnAppliedDateLog" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster2" QueryAutoColumn="False" QueryLeft="80px" QueryMode="Panel" QueryTitle="查詢條件" QueryTop="80px" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.HUT_Job" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="100%">
                                <Columns>
                                    <JQTools:JQGridColumn Alignment="left" Caption="開缺日" Editor="datebox" FieldName="JobDeclareDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="88">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="關閉日" Editor="datebox" FieldName="JobCloseDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="88">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="關閉分類" Editor="text" FieldName="JobCloseReason" Frozen="False" IsNvarChar="True" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="250">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="關閉說明" Editor="text" FieldName="JobCloseDescription" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="365">
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

                <JQTools:JQDialog ID="JQDialogJobAssignLogs" runat="server" BindingObjectID="dataFormMaster3" Title="推薦作業" Width="940px" DialogLeft="66px" DialogTop="50px" Height="420px" EditMode="Dialog" ShowSubmitDiv="False">
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


                        <JQTools:JQDataGrid ID="DGJobAssignLogs" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_JobAssignLogs" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogAssignLogs" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnUpdate="" PageList="5,10,15,20" PageSize="5" Pagination="True" ParentObjectID="dataFormMaster3" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.HUT_Job" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="100%" OnDeleted="OnDeletedAssignLogs">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="人才姓名" Editor="infocombobox" FieldName="UserID" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="70" EditorOptions="valueField:'UserID',textField:'sName',remoteName:'sCustomersJobs.infoSelectUser',tableName:'infoSelectUser',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="人才姓名" Editor="text" FieldName="NameC" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="狀態" Editor="infocombobox" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'sHUTUser.HUT_ZAssignStep',tableName:'HUT_ZAssignStep',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssignID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="日期" Editor="datebox" EditorOptions="" FieldName="AssignTime" Format="yyyy/mm/dd" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="面試時間" Editor="text" FieldName="InterviewTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="推薦顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="推薦評估" Editor="text" FieldName="AssignContent" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="145">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption=" " Editor="text" FieldName="RecommendLink" FormatScript="RecommendLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="上傳推薦報告" Editor="text" FieldName="FileLink" FormatScript="FileLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="報告下載" Editor="infofileupload" EditorOptions="" FieldName="AssignFile" Format="download,folder:Files/Hunter/Assign" MaxLength="150"  ReadOnly="False" Width="165" Frozen="False" IsNvarChar="False" QueryCondition="" Sortable="False" Visible="True" />
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
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增推薦紀錄" />
                                <JQTools:JQToolItem Icon="icon-copy" ItemType="easyui-linkbutton" OnClick="OpenCopyAssign" Text="複製推薦紀錄" />
                            </TooItems>
                        </JQTools:JQDataGrid>
                        <JQTools:JQDialog ID="JQDialogAssignLogs" runat="server" BindingObjectID="DFJobAssignLogs" DialogLeft="120px" DialogTop="40px" Title="推薦紀錄維護" Width="860px" ShowSubmitDiv="True">
                            <JQTools:JQDataForm ID="DFJobAssignLogs" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_JobAssignLogs" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster3" RemoteName="sCustomersJobs.HUT_Job" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApplied="OnAppliedAssignLogs" OnLoadSuccess="OnLoadAssignLogs">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="選擇人才" Editor="inforefval" EditorOptions="title:'選擇人才',panelWidth:380,panelHeight:340,remoteName:'sCustomersJobs.infoSelectUser',tableName:'infoSelectUser',columns:[{field:'sName',title:'中/英文姓名',width:160,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'MobileNo1',title:'電話1',width:140,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'UserID',textField:'sName',valueFieldCaption:'UserID',textFieldCaption:'sName',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="270" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="日期" Editor="datebox" EditorOptions="" FieldName="AssignTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="面試時間" Editor="text" FieldName="InterviewTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="狀態" Editor="infocombobox" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'sHUTUser.HUT_ZAssignStep',tableName:'HUT_ZAssignStep',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnSelectAssignID,panelHeight:200" FieldName="AssignID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" Format="" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="原因" Editor="infocombobox" FieldName="AssignReason" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" EditorOptions="valueField:'AssignReason',textField:'AssignReason',remoteName:'sHUTUser.infoHUT_ZAssignStepReason',tableName:'infoHUT_ZAssignStepReason',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="推薦顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:180" FieldName="HunterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="130" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="上傳報告" Editor="infofileupload" EditorOptions="filter:'docx|doc|xlsx|xls|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|image|tif|txt',isAutoNum:true,upLoadFolder:'Files/Hunter/Assign',showButton:true,showLocalFile:true,fileSizeLimited:'2000'" FieldName="AssignFile" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="False" Width="200" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="推薦評估" Editor="textarea" FieldName="AssignContent" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="670" EditorOptions="height:200" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="實際營業額" Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="iTurnoverReal" MaxLength="0" NewRow="False" OnBlur="OnBluriTurnoverReal" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" EditorOptions="precision:2,groupSeparator:',',prefix:''" FieldName="ratioReal" MaxLength="0" NewRow="False" OnBlur="OnBluriTurnoverReal" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="38" />
                                    <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="AmountReal" MaxLength="0" NewRow="False" OnBlur="" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="60" />
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

                        <JQTools:JQDialog ID="JQDialogAssignLogs2" runat="server" BindingObjectID="DFJobAssignLogs2" DialogLeft="250px" DialogTop="100px" ShowSubmitDiv="True" Title="上傳報告" Width="500px">
                            <JQTools:JQDataForm ID="DFJobAssignLogs2" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_JobAssignLogs" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedAssignLogs" ParentObjectID="dataFormMaster8" RemoteName="sHUTUser.HUT_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="上傳報告" Editor="infofileupload" EditorOptions="filter:'docx|doc|xlsx|xls|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|image|tif|txt',isAutoNum:true,upLoadFolder:'Files/Hunter/Assign',showButton:true,showLocalFile:true,fileSizeLimited:'2000'" FieldName="AssignFile" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="250" />
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
