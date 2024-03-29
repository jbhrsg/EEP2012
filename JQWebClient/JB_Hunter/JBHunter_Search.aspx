﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_Search.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script src="../js/jquery.url.js"></script>
    <script>
        //控制搜尋條件(簡易搜尋,全文檢索)
        function ControlShow(val) {
            //清空搜尋Grid
            $('#dataGridView').datagrid('setWhere', '1=0');
            if (val == 1) {
                $("#querydataGridView").show();
                $("#divFullSearch").hide();
            } else {
                $("#querydataGridView").hide();
                $("#divFullSearch").show();
                //全文檢索加上ToolTip
                $('#txtString').attr('title', '請輸入關鍵字:以 , 區隔');
            }
        }
        //給Query Column預設值(履歷有效天數)=>從首頁過來帶參數=>3天
        function QueryDefault() {
            //url分析
            if ($.url.param("day") == null) {
                return "30";
            } else return "3";
        }
        $(document).ready(function () {
            //工作部門,職稱加上ToolTip
            $('#DutyDept_Query').attr('title', '請輸入「工作部門」關鍵字範例「品保,研發,稽核....」');
            $('#DutyTitle_Query').attr('title', '請輸入「工作職稱」關鍵字範例「消防,製程,業務副總....」');
            //全文檢索隱藏
            $("#divFullSearch").hide();
            //var gg = $("#JQOptions1").data("infooptions").panel;
            //$("input:radio", gg).attr("checked", "1");
            //預設為簡易搜尋
            $('input:radio[name=JQOptions1_0][value=1]').attr('checked', true);
            //預設為and
            $('input:radio[name=JQOptions2_0][value=1]').attr('checked', true);
            //dataFormMaster
            $("#dataFormMaster").form({
                onLoadSuccess: function () {
                    if (getEditMode($(this)) == "updated") {
                        var UserID = $("#dataFormMasterUserID").val();
                        //職缺餐盤條件過濾
                        $("#dataGridDetail").datagrid('setWhere', " n.UserID=" + UserID);
                    }
                }
            });
            //刪除dialog下面存檔,關閉按鈕
            $("#dataGridDetail-SubmitDiv").remove();
            //以下為combotree----產業別    
            $('#IndustryID_Query').jbCombobox2tree({
                parentField: 'ParentID'
            });
            //以下為combotree----學類
            $('#EduSubject1_Query').jbCombobox2tree({
                parentField: 'ParentID'
            });
            //產業別預設不拘
            $("#IndustryID_Query").combobox('setValue', 0);
            //最高學類預設不拘
            $("#EduSubject1_Query").combobox('setValue', 0);
            //從首頁過來帶參數=>3天=>自動查詢
            if ($.url.param("day") != null) {
                queryGrid($("#dataGridView"));
            }
        });
        //-------------------------------簡易查詢條件-----------------------------------------------------------
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridView') {
                var where = $(dg).datagrid('getWhere');
                //查詢字串取代
                if (where.length > 0) {
                    //起始年齡	
                    where = where.replace("u.CreateDate", "DateDiff(Year,u.BirthDay,GetDate())");
                    //終止年齡
                    where = where.replace("AssignName", "DateDiff(Year,u.BirthDay,GetDate())");
                    //履歷有效天數
                    var dt = new Date();
                    var edate = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd');
                    //往前推?天(看前端輸入值)        
                    var LastUpdateDate = $("#LastUpdateDate_Query").val();
                    var aDate = new Date($.jbDateAdd('days', -LastUpdateDate, dt));
                    var adate = $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
                    where = where.replace("LastUpdateDate<='" + LastUpdateDate + "'", "Convert(nvarchar(10),u.CreateDate,111) between '" + adate + "' and '" + edate + "'");

                    //最高需求學類(包含以下階層)
                    var EduSubject = $("#EduSubject1_Query").combobox('getValue');
                    //學類不拘過濾                   
                    where = where.replace(" and EduSubject1=" + EduSubject, "");//前面有條件時
                    where = where.replace("EduSubject1=" + EduSubject, "");//前面無條件時
                    if (EduSubject != '0' && EduSubject != null) {
                        where = where + " and ( EduSubject1 in ( Select ID from dbo.funReturnEduSubjectChildNodes(" + EduSubject + ")))"; //+" or u.EduSubject2 =" + EduSubject + " or u.EduSubject3 =" + EduSubject + ")";
                    }

                    //產業別(包含以下階層)
                    var IndustryType = $("#IndustryID_Query").combobox('getValue');
                    //產業別不拘過濾
                    where = where.replace(" and IndustryID=" + IndustryType, "");//前面有條件時
                    where = where.replace("IndustryID=" + IndustryType, "");//前面無條件時
                    if (IndustryType != '0' && IndustryType != null) {
                        where = where + " and ( c.IndustryID in ( Select ID from dbo.funReturnIndCategoryChildNodes(" + IndustryType + ")))";
                    }

                    //工作部門(以逗號分割)
                    var DutyDept = $("#DutyDept_Query").val();
                    if (DutyDept != '') {
                        var arrD = DutyDept.split(",");
                        var arr = "";
                        $.each(arrD, function (i, val) {
                            arr = "DutyDept like '%" + val + "%' or " + arr;
                        });
                        where = where.replace("DutyDept='" + DutyDept + "'", '(' + arr.substr(0, arr.length - 3) + ')');
                    }
                    //工作職稱(以逗號分割)
                    var DutyTitle = $("#DutyTitle_Query").val();
                    if (DutyTitle != '') {
                        var arrT = DutyTitle.split(",");
                        var arr2 = "";
                        $.each(arrT, function (i, val) {
                            arr2 = "DutyTitle like '%" + val + "%' or " + arr2;
                        });
                        where = where.replace("DutyTitle='" + DutyTitle + "'", '(' + arr2.substr(0, arr2.length - 3) + ')');
                    }
                }
                if (where.substr(0, 5) == ' and ') {
                    where = where.substr(5, where.length);
                }
                $(dg).datagrid('setWhere', where);
                //$("#JQDialog31").datagrid('getPanel').panel('expand'); //展開
                // $(dg).datagrid('getPanel').panel('collapse');将grid的panel缩回
                //$("#datagrid-row-r1-1-0").find('td').eq(2).text().trim();
                //alert($("#payment_currency_text tr.find(.formdata)").val());
            }
        }
        //Grid開啟履歷連結       
        function OpenUser(val, row) {
            //參數依次是dialog的名字，dataform開啟的row，dialog的狀態,viewed，dialog方式。 
            //openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "", 'dialog');   
            //return $('<a>', { href: '#', onclick: 'self.parent.addTab("履歷資料維護", "JB_Hunter/JBHunter_UserCareer.aspx");'}).linkbutton({ text: "<b><div style=\"color:Blue\">" + val + "</div></b>", plain: true })[0].outerHTML;
            //self.parent.addTab('履歷資料', 'JB_Hunter/JBHunter_UserCareer.aspx');
            //return $('<a>', { href: '#', onclick: 'self.parent.addTab("履歷資料維護", "JB_Hunter/JBHunter_UserCareer.aspx");', theData: row.UserID }).linkbutton({ text: "<b><div style=\"color:Blue\">" + val + "</div></b>", plain: true })[0].outerHTML;
            var EnUserID = EnCodeMethod(row.UserID);
            var LoginID = getClientInfo("UserID");
            return $('<a>', { href: '#', onclick: 'window.open("../../NewHunter/index.html?POS=' + EnUserID + '&HunterID=' + LoginID + '","履歷資料維護");', theData: row.UserID }).linkbutton({ text: "<b><div style=\"color:Blue\">" + val + "</div></b>", plain: true })[0].outerHTML;
        }
        //加密
        var s;
        function EnCodeMethod(UserID) {
            $.ajax({
                type: "POST",
                url: '../handler/UserPaw.ashx',
                data: { UserID: UserID },
                async: false,//非同步
                success: function (data) {
                    s = data;
                }
            });
            return s;
        }
        //得日期YYYMMDD
        function GetDate() {
            var date = new Date();
            ///date.format("dd/MM/yyyy");            
            var year = date.getYear() + 1900;
            var month = date.getMonth() + 1;
            var day = date.getDate();
            var now = year + "" + FullString(month) + "" + FullString(day);
            return now;
        }
        //判斷日期月,日是否為2位數,否則填滿
        function FullString(s) {
            if (s > 9) {
                return s
            } else return '0' + '' + s
        }
        // 推薦紀錄維護 Call Method
        function AddMenu() {
            if ($("#dataGridView").datagrid('getSelections').length == 0) {
                alert('請先選擇人才。');
            } else {
                //$('#dataFormMaster').attr('dialogGrid', '#dataGridView');
                var row = $('#dataGridView').datagrid('getSelected');
                openForm('#JQDialog1', row, "", 'dialog');
                $("#dataGridDetail").datagrid('setWhere', "n.UserID='" + row.UserID + "'");
                //return $('<a>', { href: '#', onclick: 'AssignLogs(this)', theData: row.Name, theData2: row.UserID }).linkbutton({ text: "<b><div style=\"color:Red\">" + val + "</div></b>", plain: true })[0].outerHTML;
            }
        }
        ////新增人才履歷 Call Method
        //function OpenResume() {
        //    var LoginID = getClientInfo("UserID");
        //    window.open('../../NewHunter/index.html?HunterID=' + LoginID, '新增履歷資料');
        //}
        //搜尋結果Grid面談履歷 => 開啟面談履歷連結       
        function OpenResume0(val, row) {
            var sUserID = row.UserID;    
            var sJobID = 0;
            return $('<a>', { href: "#", onclick: "myclick0(" + sUserID + "," + sJobID +")", theData: row.UserID }).linkbutton({ text: "<img src=img/Resume.png></a><b><div style=\"color:Red\">" + val + "</div></b>", plain: true })[0].outerHTML;
        }
        function myclick0(sUserID,sJobID) {
            var LoginID = getClientInfo("UserID");
            window.open('../../NewHunter/Rec_Resume.html?JobID=' + sJobID + '&UserID=' + sUserID + '&HunterID=' + LoginID, '面談履歷');            
        }
        //搜尋結果Grid 獵才顧問  => 新增獵才顧問連結       
        function OpenHunter(val, row) {
            return val;
        }
        
        function OpenResume(val, row) {
            var sUserID = row.UserID;
            var sJobID = row.JobID;
            return $('<a>', { href: "#", onclick: "myclick(" + sUserID + "," + sJobID + ")", theData: row.UserID }).linkbutton({ text: "<img src=img/Resume.png></a><b><div style=\"color:Red\">" + val + "</div></b>", plain: true })[0].outerHTML;
        }
        function myclick(sUserID, sJobID) {
            var LoginID = getClientInfo("UserID");
            window.open('../../NewHunter/Rec_Resume.html?Type=50&JobID=' + sJobID + '&UserID=' + sUserID + '&HunterID=' + LoginID, '面談履歷');
        }
        //推薦作業Grid 修改推薦履歷 => 開啟推薦履歷連結       
        function OpenResume2(val, row) {
            var sUserID = row.UserID;
            var sJobID = row.JobID;
            return $('<input type="image" img  src="img/Resume2.png" onclick="myclick2(' + sUserID + ',' + sJobID + ')" value="view">')[0].outerHTML;
        }
        function myclick2(sUserID, sJobID) {
            var LoginID = getClientInfo("UserID");
            window.open('../../NewHunter/Rec_Resume.html?JobID=' + sJobID + '&UserID=' + sUserID + '&HunterID=' + LoginID, '修改推薦履歷');
        }
        //下載推薦履歷連結(判斷HUT_RUser =>downloadpdf是否有值,若無職則導向轉出頁面,否則打開PDF)
        function downloadpdf(val, row) {
            var JobResumeFileNameC = row.JobResumeFileNameC+".pdf";//職缺對應的檔名
            var sdownloadpdf = row.downloadpdf;//有轉檔才有資料
            if (sdownloadpdf != '') {
                sdownloadpdf = "'../../NewHunter/files/" + JobResumeFileNameC + "'";
            } else {
                var sUserID = row.UserID;
                var sJobID = row.JobID;
                var LoginID = getClientInfo("UserID");
                var sdownloadpdf = "http://www.jbjob.com.tw/NewHunter/Rec_Resume.html?JobID=" + sJobID + "&UserID=" + sUserID + "&HunterID=" + LoginID;
                sdownloadpdf = "'" + sdownloadpdf + "'";
            }
            return $('<input type="image" img  src="img/pdf.png" onclick="window.open(' + sdownloadpdf + ');" value="view">')[0].outerHTML;
        }
        //搜尋結果Grid 面談紀錄維護 
        function OpenInterview(val, row) {
            return $('<a>', { href: '#', onclick: 'InterviewFunction(this)', theData: row.UserID }).linkbutton({ text: "<b><div style=\"color:Red\">" + val + "</div></b>", plain: true })[0].outerHTML;
        }
        var InterviewFunction = function (Target) {
            $("#JQDialog6").dialog("open");
            var UserID = $(Target).attr('theData');
            $("#dataGridInterview").datagrid('setWhere', "UserID='" + UserID + "'");
        }
        
        //-------------------------------人才推薦紀錄-Dialog--------------------------------------------------------------------
        //求得UserID 
        function GetUserID() {
            return $("#dataFormMasterUserID").val();
        }
        //function GetJobID() {
        //    return $("#dataFormMasterJobID").val();
        //}
        //求得CreateDate
        function GetCreateDate() {
            var date = new Date();
            var year = date.getYear() + 1900;
            var month = date.getMonth() + 1;
            var day = date.getDate();
            var hour = date.getHours();
            var minute = date.getMinutes();
            var second = date.getSeconds();
            var now = year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second;
            return now;
        }
        //新增推薦紀錄後更新2個Grid
        function RefreshGrid() {
            $('#dataGridDetail').datagrid('reload');
            //$('#dataGridView').datagrid('reload');
            //queryGrid($("#dataGridView"));
        }

        //刪除控制=>刪除最新一筆        
        function DeleteRow(rowData) {
            if (rowData.ROW_NUMBER == 1) {
                var pre = confirm("確定移除最新推薦紀錄?");
                if (pre == true) {
                    //callServerMethod
                    var row = $('#dataGridDetail').datagrid('getSelected');
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sSearchFunction.JobAssignLogs',
                        data: "mode=method&method=" + "userDEL" + "&parameters=" + rowData.AssignNO + "," + rowData.UserID + "," + rowData.JobID,
                        cache: false,
                        async: true,
                    });
                    RefreshGrid();
                    return false; //取消刪除的動作                     
                }
                else {
                    return false;
                }
            }
            alert('不是最後一筆資料,不可刪除。')
            return false;
        }

        //-------------------------------全文索引--------------------------------------------------------------------
        function serverMethod() {
            var tString = $('#txtString').val();//搜尋字串
            //分割字串(以逗號分割)
            if (tString != '') {
                //判斷 and or               
                var value = $("#JQOptions2").data("infooptions").panel;
                //$("input:radio", value).attr("checked", 2);
                var Chk = $("input:radio", value).get(0).checked;//是否選擇and ( and 1=>index 0,or 2=>index 1 )
                var arrT = "";
                var Mark = "";
                if (Chk == true) {//是否選擇and
                    Mark = " and ";                   
                } else Mark=" or ";
               
                var arrT = tString.split(",");
                var arr2 = "";
                $.each(arrT, function (i, val) {
                    arr2 = arr2 + Mark + val;
                });
                arr2 = arr2.substr(4, arr2.length);
                arr2 = "'" + arr2 + "'";

                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sSearchFunction.HUT_User',  //連接的Server端，command
                    data: "mode=method&method=" + "SearchFullIndex" + "&parameters=" + arr2,  //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入搜尋字串
                    cache: false,
                    async: true,
                    success: function (data) {
                        var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示

                        if (rows.length > 10) {
                            //通過loadData方法清除掉原有Grid中的舊有資料並填補分頁資料
                            $('#dataGridView').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                        } else {
                            $('#dataGridView').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                        }

                        if (rows.length == 0) {
                            alert("目前無符合人才！");
                        } else {
                            alert("搜尋完成！");
                        }
                    }
                });
            } else {
                alert("請輸入全文檢索文字！");
            }
        }
        //-------------------------------人才推薦作業 步驟控制--------------------------------------------------------------------            
        //dataform load
        function AssignControl() {
            //if (getEditMode($('#dataFormDetail')) == "viewed") {//瀏覽
            //若為履歷指定0的狀況=>再指定一次
            var AssignID = $("#dataFormDetailAssignID").combobox('getValue');
            if (AssignID == 0) {
                $("#dataFormDetailAssignID").combobox('setValue', 0);
                $("#dataFormDetailJobID").refval('setValue', 443);
            }
            //若為內部面試的狀況=>顯示面談紀錄
            var Table = $('table:first', '#dataFormDetail');//dataForm div 底下的第一個table
            var hideTable = $('#tbData');//隱藏的table
            if (AssignID == 2) {
                $("#dataFormDetailInterviewContent").closest('tr').appendTo(Table);
            } else {
                $("#dataFormDetailInterviewContent").closest('tr').appendTo(hideTable);
            }            
        }
        //推薦狀態Select change
        function AssignSelect(row) {
            Control(row.AssignID);                             
        }
        function Control(AssignID) {
            var Table = $('table:first', '#dataFormDetail');//dataForm div 底下的第一個table
            var hideTable = $('#tbData');//隱藏的table

            //2 內部面試=>顯示面談紀錄            
            if (AssignID == 2) {               
                $("#dataFormDetailInterviewContent").closest('tr').appendTo(Table);                
            } else {               
                $("#dataFormDetailInterviewContent").closest('tr').appendTo(hideTable);                
            }
            //0 履歷指定,1 聯絡中,2 內部面試=>指定職缺傑報JobID=443
            var JobID = $("#dataFormDetailJobID").refval("getValue");
            if (JobID == "") {
                if (AssignID == 0 || AssignID == 1 || AssignID == 2) {
                    $("#dataFormDetailJobID").refval('setValue', 443);
                }
            }
            //推薦日期為空值=>則選擇時預設今天
            var AssignTime = $("#dataFormDetailAssignTime").datebox("getValue");
            if (AssignTime == "")
            {
                $("#dataFormDetailAssignTime").datebox("setValue", "2015/02/11");
            }
            
        }
        //複製推薦紀錄
        function CopyAssign() {
            var rowcount = $('#dataGridDetail').datagrid('getData').total;
            if (rowcount <= 0) {
                openForm('#JQDialog2', 0, "inserted", 'dialog');
            } else {
                var row = $('#dataGridDetail').datagrid('getSelected');
                //$('#dataGridDetail').datagrid('appendRow', row);//grid新增一筆資料
                row.AssignNO = 0;
                //OPENDATAFORM一筆資料
                openForm('#JQDialog2', row, "inserted", 'dialog');
            }
        }
        //
        function AssignLoad() {
            var rowcount = $('#dataGridDetail').datagrid('getData').total;
            if (rowcount <= 0) {
                $("#toolItemdataGridDetail新增").show();
                $("#toolItemdataGridDetail新增推薦").hide();
            } else {
                $("#toolItemdataGridDetail新增").hide();
                $("#toolItemdataGridDetail新增推薦").show();
            }
        }
        //-------------------------------------------------上傳檔案功能--------------------------------------------------------------         
        //上傳檔案路徑設定
        function beforeUpload(options) {
            var rowData = $("#dataGridDetail").datagrid('getSelected');
            options.upLoadFolder = '\\JB_Hunter\\file\\' + rowData.UserID;
            return true;
        }
        //寫路徑到DB
        function writeDBurl() {
            //夾檔                             
            //var infofileUploadfile = $('.info-fileUpload-file', infofileUpload.next());//上傳路徑               
            //上傳檔案
            //infoFileUploadMethod($('#FileUpload1'));
            var rowData = $("#dataGridDetail").datagrid('getSelected');
            var infofileUpload = $('#JQFileUpload1');
            var infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next());//取得文件名稱
            var infofile = infofileUploadvalue.val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sSearchFunction.JobAssignLogs',
                data: "mode=method&method=" + "writeDBurl" + "&parameters=" + rowData.UserID + "," + rowData.JobID + "," + rowData.AssignID + "," + infofile,
                cache: false,
                async: true,
                success: function (data) {
                    GridMenuReload();                 
                    alert("上傳成功！");
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }
        function GridMenuReload() {
            $('#dataGridDetail').datagrid('reload');
        }
        //下載上傳檔案
        function downloadScript(val, rowData) {
            if (val != null) {
                return '<a href="../handler/HunterFileHandler.ashx?File=' + val + '&UserID=' + rowData.UserID + '">' + val + '</a>';
            }
        }

    </script>

    <style type="text/css">
        .auto-style1
        {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
        <div id="tab1" class="easyui-tabs">
            <div id="aa" title="一般簡易搜尋">
                <fieldset>
                    <legend>搜尋條件</legend>

                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 170px">
                                <JQTools:JQOptions ID="JQOptions1" runat="server" OpenDialog="False" Width="80px" DialogWidth="180" OnSelect="ControlShow">
                                    <Items>
                                        <JQTools:JQComboItem Selected="True" Text="簡易搜尋" Value="1" />
                                        <JQTools:JQComboItem Selected="False" Text="全文檢索" Value="2" />
                                    </Items>
                                </JQTools:JQOptions>
                            </td>
                            <td>
                                <div id="divFullSearch">
                                    <table style="width:100%;">
                                        <tr>
                                            <td style="width: 305px">
                                    &nbsp;(★請輸入關鍵字:以 , 區隔)<asp:TextBox ID="txtString" runat="server" Width="300px"></asp:TextBox>
                                            </td>
                                            <td style="width: 106px"><JQTools:JQOptions ID="JQOptions2" runat="server" OpenDialog="False" DialogWidth="105" Width="110px">
                                    <Items>
                                        <JQTools:JQComboItem Selected="True" Text=" and " Value="1" />
                                        <JQTools:JQComboItem Selected="False" Text=" or " Value="2" />
                                    </Items>
                                </JQTools:JQOptions></td>
                                            <td>
                                    <input id="bnSelect" type="button" value="搜尋" onclick="serverMethod()" />&nbsp;&nbsp; </td>
                                        </tr>
                                    </table>
                                
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="auto-style1">                                
                                <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sSearchFunction.HUT_User" runat="server" AutoApply="True"
                                    DataMember="HUT_User" Pagination="True" QueryTitle="" EditDialogID="JQDialog3"
                                    Title="搜尋結果" AlwaysClose="True" OnLoadSuccess="queryGrid" QueryMode="Panel" DeleteCommandVisible="False" ViewCommandVisible="False" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True">
                                    <Columns>
                                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="Name" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="110" FormatScript="OpenUser" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="面談履歷預覽" Editor="text" FieldName="OpenResume" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="75" FormatScript="OpenResume0" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="面談紀錄" Editor="text" FieldName="iInterview" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="60" FormatScript="OpenInterview" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="更新日期" Editor="text" FieldName="LastUpdateDate" Format="yyyy/mm/dd" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="獵才顧問" Editor="text" FieldName="HunterName" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="75" FormatScript="OpenHunter" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="參考顧問" Editor="text" FieldName="SelectHunter" FormatScript="" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="最高學歷" Editor="text" FieldName="sEdu" MaxLength="0" ReadOnly="False" Visible="True" Width="210" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="最近經歷" Editor="text" FieldName="sCareer" MaxLength="0" ReadOnly="True" Visible="True" Width="280" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="黑名單" Editor="text" EditorOptions="on:1,off:0" FieldName="NotMatch" Format="L-checkbox" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="照片" Editor="text" FieldName="PhotoFile" Format="Image,Folder:../NewHunter,Height:30" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" FormatScript="" />

                                    </Columns>
                                    <TooItems>
                                        <JQTools:JQToolItem ItemType="easyui-linkbutton" OnClick="AddMenu" Text="人才推薦作業" Visible="True" Icon="icon-ok" />
<%--                                        <JQTools:JQToolItem Enabled="True" ItemType="easyui-linkbutton" OnClick="OpenResume" Text="新增人才履歷" Visible="True" Icon="icon-redo" />--%>
                                    </TooItems>
                                    <QueryColumns>
                                        <%--<JQTools:JQQueryColumn Caption="科系名稱" Condition="%%" DataType="string" Editor="text" FieldName="Department1" NewLine="False" RemoteMethod="False" Width="125" />--%>

                                        <JQTools:JQQueryColumn AndOr="and" Caption="人才姓名" Condition="%%" DataType="string" Editor="text" FieldName="NameC" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="130" />
                                        <JQTools:JQQueryColumn Caption="性別" Condition="=" DataType="number" Editor="infocombobox" EditorOptions="items:[{value:'',text:'不拘',selected:'true'},{value:'1',text:'男',selected:'false'},{value:'0',text:'女',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="u.Gender" NewLine="False" RemoteMethod="False" Width="125" />
                                        <JQTools:JQQueryColumn AndOr="and" Caption="起始年齡" Condition="&gt;=" DataType="number" Editor="numberbox" FieldName="u.CreateDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="80" />

                                        <JQTools:JQQueryColumn AndOr="and" Caption="終止年齡" Condition="&lt;=" DataType="number" Editor="numberbox" FieldName="AssignName" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="80" />

                                        <JQTools:JQQueryColumn Caption="教育程度" Condition="&gt;=" DataType="number" Editor="infocombobox" FieldName="EduID1" NewLine="True" RemoteMethod="False" Width="130" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'sReferences.HUT_ZEduLevel',tableName:'HUT_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQQueryColumn Caption="最高學類" Condition="=" DataType="number" Editor="infocombobox" FieldName="EduSubject1" NewLine="False" RemoteMethod="False" Width="170" EditorOptions="valueField:'ID',textField:'SubjectName',remoteName:'sSearchFunction.HUT_EduSubject',tableName:'HUT_EduSubject',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQQueryColumn AndOr="and" Caption="最高科系" Condition="%%" DataType="string" Editor="text" FieldName="u.Department1" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="170" />
                                        <JQTools:JQQueryColumn AndOr="and" Caption="產業別" Condition="=" DataType="number" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'IndCategory',remoteName:'sSearchFunction.HUT_IndCategory',tableName:'HUT_IndCategory',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IndustryID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="170" />

                                        <JQTools:JQQueryColumn AndOr="and" Caption="工作部門" Condition="=" DataType="string" Editor="text" FieldName="DutyDept" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="170" />
                                        <JQTools:JQQueryColumn AndOr="and" Caption="工作職稱" Condition="=" DataType="string" Editor="text" FieldName="DutyTitle" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="170" />

                                        <JQTools:JQQueryColumn AndOr="and" Caption="履歷有效天數" Condition="&lt;=" DataType="string" Editor="text" FieldName="LastUpdateDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="85" DefaultValue="" DefaultMethod="QueryDefault" />

                                        <JQTools:JQQueryColumn AndOr="and" Caption="獵才顧問" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sSearchFunction.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ServicePerson" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />

                                        <JQTools:JQQueryColumn AndOr="and" Caption="履歷效期" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'有效',selected:'true'},{value:'0',text:'無效',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsActive" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />

                                    </QueryColumns>
                                    <QueryColumns>
                                    </QueryColumns>
                                </JQTools:JQDataGrid>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <JQTools:JQDialog ID="JQDialog3" runat="server" BindingObjectID="dataFormResult" Closed="True" EditMode="Expand" ShowModal="False" Title="人才資訊" ShowSubmitDiv="False" Width="800px">
                                    <JQTools:JQDataForm ID="dataFormResult" runat="server" Closed="False" ContinueAdd="False" DataMember="QueryResult" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="2" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataGridView" RemoteName="sSearchFunction.HUT_User" ShowApplyButton="False" ValidateStyle="Hint" Width="100%">
                                        <Columns>
                                            <JQTools:JQFormColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" MaxLength="0" ReadOnly="True" Width="100" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="居住地址" Editor="text" FieldName="Address1" MaxLength="20" ReadOnly="True" Visible="True" Width="350" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="MobileNo1" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="100" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="eMail " Editor="text" FieldName="eMail1" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="350" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="最新履歷狀態" Editor="text" FieldName="AssignName" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="100" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="最新推薦資訊" Editor="text" FieldName="LastStatus" MaxLength="0" ReadOnly="True" Visible="True" Width="350" NewRow="False" />
                                        </Columns>
                                        <RelationColumns>
                                            <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                                        </RelationColumns>
                                    </JQTools:JQDataForm>
                                </JQTools:JQDialog>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>

                </fieldset>

                <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="推薦作業" Width="800px" DialogLeft="50px" DialogTop="50px" Height="420px" EditMode="Dialog">
                    <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HUT_User" HorizontalColumnsCount="2" RemoteName="sSearchFunction.HUT_User" ParentObjectID="" ContinueAdd="False">

                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" MaxLength="20" Width="180" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="人才資料" Editor="text" FieldName="Name" MaxLength="50" Width="180" ReadOnly="True" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" MaxLength="0" ReadOnly="False" Visible="False" Width="80" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <fieldset>
                        <legend>推薦紀錄</legend>


                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: right">上傳檔案：<JQTools:JQFileUpload ID="JQFileUpload2" runat="server" onBeforeUpload="beforeUpload" onSuccess="writeDBurl" ShowLocalFile="False" SizeFieldName="" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="JobAssignLogs" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialog2" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" OnDelete="DeleteRow" OnInserted="RefreshGrid" OnLoadSuccess="AssignLoad" PageList="7,10,20,30,40,50" PageSize="7" Pagination="True" ParentObjectID="" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sSearchFunction.JobAssignLogs" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                                        <Columns>
                                            <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱 - 職缺名稱" Editor="text" EditorOptions="" FieldName="JobName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="200" />
                                            <JQTools:JQGridColumn Alignment="center" Caption="面談履歷" Editor="text" FieldName="OpenResume" FormatScript="OpenResume" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="55" />
                                            <JQTools:JQGridColumn Alignment="center" Caption="編修推薦履歷" Editor="text" FieldName="OpenResume2" FormatScript="OpenResume2" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75" />
                                            <JQTools:JQGridColumn Alignment="center" Caption="推薦履歷" Editor="text" FieldName="downloadpdf" FormatScript="downloadpdf" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55" />
                                            <JQTools:JQGridColumn Alignment="center" Caption="上傳檔案下載" Editor="text" FieldName="FileUrl" FormatScript="downloadScript" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                                            <JQTools:JQGridColumn Alignment="center" Caption="獵才顧問" Editor="text" FieldName="HunterName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="75" />
                                            <JQTools:JQGridColumn Alignment="center" Caption="推薦狀態" Editor="text" EditorOptions="" FieldName="AssignName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="65" />
                                            <JQTools:JQGridColumn Alignment="center" Caption="推薦日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" FieldName="AssignTime" Format="yyyy/mm/dd" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="70" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="輸入人員" Editor="text" FieldName="CreateBy" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="True" Width="65" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="AssignNO" Editor="text" FieldName="AssignNO" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="ROW_NUMBER" Editor="text" FieldName="ROW_NUMBER" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                        </Columns>
                                        <TooItems>
                                            <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" Visible="True" />
                                            <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="CopyAssign" Text="新增推薦" Visible="True" />
                                        </TooItems>
                                    </JQTools:JQDataGrid>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Closed="True" EditMode="Dialog" ShowModal="True" ShowSubmitDiv="True" Title="新增推薦紀錄" Width="650px">
                                        <JQTools:JQDataForm ID="dataFormDetail" runat="server" Closed="False" ContinueAdd="False" DataMember="JobAssignLogs" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="1" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="RefreshGrid" OnLoadSuccess="AssignControl" ParentObjectID="" RemoteName="sSearchFunction.JobAssignLogs" ShowApplyButton="False" ValidateStyle="Dialog">
                                            <Columns>
                                                <JQTools:JQFormColumn Alignment="left" Caption="推薦狀態" Editor="infocombobox" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'sJobQuery.AssignStep',tableName:'AssignStep',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:AssignSelect,panelHeight:150" FieldName="AssignID" MaxLength="0" Span="1" Width="100" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="推薦編號" Editor="text" FieldName="AssignNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="120" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="職缺名稱" Editor="inforefval" EditorOptions="title:'選擇職缺',panelWidth:450,remoteName:'sSearchFunction.infoCusJob',tableName:'infoCusJob',columns:[{field:'CustShortName',title:'客戶簡稱',width:120,align:'left',table:'',queryCondition:''},{field:'JobName',title:'職缺名稱',width:290,align:'left',table:'',queryCondition:''}],columnMatches:[],whereItems:[],valueField:'JobID',textField:'JobName',valueFieldCaption:'JobID',textFieldCaption:'JobName',cacheRelationText:false,checkData:false,showValueAndText:false,selectOnly:true" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="推薦日期" Editor="datebox" FieldName="AssignTime" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="120" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="120" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="獵才顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sHunter.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ServicePerson" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="面談紀錄" Editor="textarea" EditorOptions="height:200" FieldName="InterviewContent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="450" />
                                            </Columns>
                                        </JQTools:JQDataForm>
                                        <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                                            <Columns>
                                                <JQTools:JQDefaultColumn DefaultMethod="" DefaultValue="_today" FieldName="AssignTime" RemoteMethod="True" />
                                                <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                                <JQTools:JQDefaultColumn DefaultMethod="" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                                <JQTools:JQDefaultColumn DefaultValue="0" FieldName="AssignNO" RemoteMethod="True" />
                                                <JQTools:JQDefaultColumn DefaultMethod="GetUserID" FieldName="UserID" RemoteMethod="False" />
                                            </Columns>
                                        </JQTools:JQDefault>
                                        <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                                            <Columns>
                                                <JQTools:JQValidateColumn CheckNull="True" FieldName="AssignID" RemoteMethod="True" ValidateMessage="請選擇推薦狀態" ValidateType="None" />
                                                <JQTools:JQValidateColumn CheckNull="True" FieldName="AssignTime" RemoteMethod="True" ValidateType="None" />
                                                <JQTools:JQValidateColumn CheckMethod="" CheckNull="True" FieldName="ServicePerson" RemoteMethod="True" ValidateMessage="請選擇獵才顧問" ValidateType="None" />
                                                <JQTools:JQValidateColumn CheckNull="True" FieldName="JobID" RemoteMethod="True" ValidateMessage="請選擇職缺" ValidateType="None" />
                                            </Columns>
                                        </JQTools:JQValidate>
                                        <table id="tbData" style="width: 100%; display:none;">
                                        </table>
                                    </JQTools:JQDialog>
                                </td>
                            </tr>
                        </table>

                    </fieldset>
                </JQTools:JQDialog>
                                <JQTools:JQDialog ID="JQDialog6" runat="server" BindingObjectID="" DialogLeft="10px" DialogTop="10px" EditMode="Dialog" Height="330px" HorizontalAlign="Justify" Title="面談紀錄" Width="620px" ShowSubmitDiv="False">
                                    <JQTools:JQDataGrid ID="dataGridInterview" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="False" CheckOnSelect="True" ColumnsHibeable="False" data-options="pagination:true,view:commandview" DataMember="HUT_UserInterview" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="DialogInterview" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sJobQuery.HUT_UserInterview" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                                        <Columns>
                                            <JQTools:JQGridColumn Alignment="left" Caption="面談紀錄內容" Editor="textarea" FieldName="InterviewContent" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="500" Format="" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                        </Columns>
                                        <TooItems>
                                            <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                                        </TooItems>
                                    </JQTools:JQDataGrid>
                                    <JQTools:JQDialog ID="DialogInterview" runat="server" BindingObjectID="dataFormInterview" EditMode="Switch" Title="">
                                        <JQTools:JQDataForm ID="dataFormInterview" runat="server" Closed="False" ContinueAdd="False" DataMember="HUT_UserInterview" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="2" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sJobQuery.HUT_UserInterview" ShowApplyButton="False" ValidateStyle="Hint">
                                            <Columns>
                                                <JQTools:JQFormColumn Alignment="left" Caption="面談紀錄" Editor="textarea" FieldName="InterviewContent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="5" Span="1" Visible="True" Width="500" EditorOptions="height:200" Format="" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                            </Columns>
                                        </JQTools:JQDataForm>
                                    </JQTools:JQDialog>
                                </JQTools:JQDialog>
                <JQTools:JQImageContainer ID="JQImageContainer1" runat="server" AutoSize="False" Height="450px" HorizontalAlign="Center" Width="350px">
                    </JQTools:JQImageContainer>    
            </div>
            <div id="bb" title="進階搜尋">
                <fieldset>
                    <legend>搜尋條件</legend>
                </fieldset>
            </div>
        </div>

    </form>
</body>
</html>
