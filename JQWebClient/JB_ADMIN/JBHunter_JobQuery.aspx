<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_JobQuery.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>   
     <script src="../js/datagrid-filter.js"></script>
    <style>
		.icon-filter{
			background:url('../js/filter.png') no-repeat center center;
		}
	    .auto-style1
        {
            height: 20px;
        }
    </style>

        <script>                                
            $(document).ready(function () {               
                //全文檢索隱藏
                $("#divFullSearch").hide();
                //預設為簡易搜尋
                $('input:radio[name=JQOptions1_0][value=1]').attr('checked', true);
                //預設為and
                $('input:radio[name=JQOptions2_0][value=1]').attr('checked', true);
                //刪除dialog下面存檔,關閉按鈕
                $("#dataGridDetail-SubmitDiv").remove();                
                //隱藏搜尋結果Grid
                //$("#divResult").hide();                
                //根據職缺的條件來搜尋該範圍內的資料
                $("#bnSelect").click(function () {               
                    UpdateGrid($("#JQDataResult"));                    
                });                
                //加上ToolTip
                $('#dataFormMasterDutyDept').attr('title', '請輸入「工作部門」關鍵字範例「品保,研發,稽核....」');
                $('#dataFormMasterDutyTitle').attr('title', '請輸入「工作職稱」關鍵字範例「消防,製程,業務副總....」');
                //新增推薦紀錄--給事件
                $("#bnAssign").click(function () {
                    AddAssign($("#JQDataAssignNew"));
                });
                //編輯圖案加上提示
                //$(".datagrid-row datagrid-row-selected").attr('title', '點此可做人才搜尋&推薦');
                //dataForm
                $("#dataFormMaster").form({
                    
                    onLoadSuccess: function () {
                        if (getEditMode($(this)) == "updated") {
                            //清空搜尋Grid
                            $('#JQDataResult').datagrid('setWhere', '1=0');                           
                            //load時更新HUT_JobLangTemp
                            UpdateJobLangTemp();
                            //過濾語文搜尋條件(JobID+IPAddress)
                            //var UserName = getClientInfo("UserName");
                            var IPAddress = getClientInfo("IPAddress");                            
                            var JobID = $("#dataFormMasterJobID").val();                           
                            $("#dataGridDetail").datagrid('setWhere', " JobID=" + JobID + " and IPAddress='" + IPAddress + "'");
                            //推薦列表上方資訊(客戶+職缺)                            
                            var CustShortName = $("#dataFormMasterCustShortName").val();
                            var JobName = $("#dataFormMasterJobName").val();
                            $("#lbCustomer").html(CustShortName);
                            $("#lbJobName").html(JobName);
                            //Job做成combobox
                            //$("#ComboJob").combobox('setValue', JobID);
                            //職缺餐盤條件過濾
                            $("#JQDataAssignNew").datagrid('setWhere', " n.JobID=" + JobID);
                            
                            //若無產業別=>預設為產業別不拘
                            //if ($("#dataFormMasterIndustryType").is(":visible") ) {
                                var IndustryType = $("#dataFormMasterIndustryType").combobox('getValue');
                                if (IndustryType == 0) {
                                    $("#dataFormMasterIndustryType").combobox('setValue', 0);
                                }
                            //}
                            //若無最高學類=>預設為最高學類不拘
                            var EduSubject = $("#dataFormMasterEduSubject").combobox('getValue');
                            //var EduSubject = $("#dataFormMasterEduSubject+.combo :hidden[name=EduSubject]").val();
                            if (EduSubject == 0) {
                                $("#dataFormMasterEduSubject").combobox('setValue', 0);
                            }
                           
                            //預設推薦時間
                            var date = new Date();
                            var year = date.getYear() + 1900;
                            var month = date.getMonth() + 1;
                            var day = date.getDate();
                            //var hour = date.getHours();
                            //var minute = date.getMinutes();
                            //var second = date.getSeconds();
                            var now = year + "/" + month + "/" + day;// + " " + hour + ":" + minute + ":" + second;
                            $('#JQDateBox1').datebox('setValue', now);                            
                            //var gg = $("#OptionsStep").data("infooptions").panel;
                            //$("input:radio", gg).attr("checked", 2);//填充內容
                            //$(".combo datebox").css("width", "80px");
                            //夾檔預設隱藏
                            $('span[class="info-fileUpload-span"]').hide();
                            //清空夾檔的檔名
                            $('input[class="info-fileUpload-value"]').val("");
                            //$('a[class="info-fileUpload-button href= l-btn l-btn-plain"]').hide();

                            //************grid Filter功能************
                            //var dg = $('#JQDataAssignNew').datagrid({
                            //    filterBtnIconCls: 'icon-filter'
                            //});
                            //dg.datagrid('enableFilter', [{
                            //    field: 'AssignName',
                            //    type: 'combobox',
                            //    options: {
                            //        panelHeight: 'auto',
                            //        data: [{ value: '', text: 'All' }, { value: 'P', text: 'P' }, { value: 'N', text: 'N' }],
                            //        onChange: function (value) {
                            //            if (value == '') {
                            //                dg.datagrid('removeFilterRule', 'status');
                            //            } else {
                            //                dg.datagrid('addFilterRule', {
                            //                    field: 'AssignName',
                            //                    op: 'equal',
                            //                    value: value
                            //                });
                            //            }
                            //            dg.datagrid('doFilter');
                            //        }
                            //    }
                            //}]);
                            //************grid Filter功能************


                        }                        
                    }
                });
                //以下為combotree----產業別   
                $('#dataFormMasterIndustryType').jbCombobox2tree({
                    parentField: 'ParentID'
                });               
                //以下為combotree----學類                             
                $('#dataFormMasterEduSubject').jbCombobox2tree({
                    parentField: 'ParentID'
                });
            });
            //兜查詢條件-----------------------------------------------------------
            function queryGrid(dg) {
                if ($(dg).attr('id') == 'dataGridMaster') {
                    var where = $(dg).datagrid('getWhere');
                    //查詢字串取代
                    if (where.length > 0) {
                        //職缺效期JobCloseDate
                        where = where.replace("JobCloseDate>=", " ( JobCloseDate is null or JobCloseDate>=");
                        where = where+" )"
                    }
                    $(dg).datagrid('setWhere', where);
                }

            }
            //刪除HUT_JobLangTemp資料,新增HUT_JobLangTemp與HUT_JobLang同JobID資料
            function UpdateJobLangTemp() {
                var JobID = $("#dataFormMasterJobID").val();
                var UserName = getClientInfo("UserName");
                var IPAddress = getClientInfo("IPAddress");
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sJobLang.HUT_JobLangTemp',  //連接的Server端，command
                        data: "mode=method&method=" + "UpdateJobLangTemp" + "&parameters=" + JobID + "," + UserName + "," + IPAddress,  //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入搜尋字串
                        cache: false,
                        async: true,
                        success: function (data) {                           
                        }
                    });            
            }
            //-------------------------------------------------Grid Search--------------------------------------------------------------
            //defaultMaster=>求得JobLang 的JobID
            function SetJobID() {
                return $("#dataFormMasterJobID").val();
            }
            // 根據輸入的條件更新 Grid
            function UpdateGrid(dg) {               
                //$("#divResult").show();
                if ($(dg).attr('id') == 'JQDataResult') {
                    var JobID = $("#dataFormMasterJobID").val();
                    var where = $(dg).datagrid('getWhere');
                    if (where.length == 0) {//--過濾已在職缺名單餐盤內人員--排除黑名單
                        where = where + " u.UserID not in (Select UserID from HUT_JobAssignNew where AssignID!=6 and JobID=" + JobID + ")";
                    }
                    //起始年齡
                    var Age1 = $("#dataFormMasterJobAge1").val();
                    if (Age1!='') {
                        where = where + " and DateDiff(Year,u.BirthDay,GetDate())>=" + Age1;
                    }
                    //終止年齡
                    var Age2 = $("#dataFormMasterJobAge2").val();
                    if (Age2 != '') {
                        where = where + " and DateDiff(Year,u.BirthDay,GetDate())<=" + Age2;
                    }
                    //var Gender = $("#dataFormMasterJobGender").combobox('getValue', ID);//性別   
                    //性別 
                    //var Gender = $("#dataFormMasterJobGender+.combo :hidden[name=JobGender]").val();
                    var Gender = $("#dataFormMasterJobGender").combobox('getValue');
                    if (Gender != '2') {//不拘
                        where = where + " and ( u.Gender=" + Gender + ")";
                    }
                    //最高教育程度
                    var EduID = $("#dataFormMasterEduLevelID").combobox('getValue');
                    if (EduID != '') {
                        where = where + " and ( u.EduID1>=" + EduID + ")";
                    }
                    //最高需求學類(包含以下階層)
                    var EduSubject = $("#dataFormMasterEduSubject").combobox('getValue');
                    if (EduSubject != '0' && EduSubject != null) {
                        where = where + " and ( u.EduSubject1 in ( Select ID from dbo.funReturnEduSubjectChildNodes(" + EduSubject + ")))"; //+" or u.EduSubject2 =" + EduSubject + " or u.EduSubject3 =" + EduSubject + ")";
                    }
                    //產業別(包含以下階層)
                    var IndustryType = $("#dataFormMasterIndustryType").combobox('getValue');                   
                    if (IndustryType != '0' && IndustryType != null) {
                        where = where + " and ( c.IndustryID in ( Select ID from dbo.funReturnIndCategoryChildNodes(" + IndustryType + ")))";
                    }
                    //最高需求科系
                    var EduDepart = $("#dataFormMasterEduDepart").val();
                    if (EduDepart != '') {
                        where = where + " and ( u.Department1 like '%" + EduDepart + "%')"; //+ "%' or u.Department2 like '%" + EduDepart + "%' or u.Department3 like '%" + EduDepart + "%')";
                    }
                    //最後更新天數
                    var LastDay = $("#dataFormMasterLastUpdateDate").val();
                    if (LastDay != '') {
                        where = where + " and ( DateDiff(DAY,u.LastUpdateDate,GetDate())<= " + LastDay + ")";
                    }                    
                    //工作部門(以逗號分割)
                    var DutyDept = $("#dataFormMasterDutyDept").val();
                    if (DutyDept != '') {
                        var arrD = DutyDept.split(",");
                        var arr="";
                        $.each(arrD, function (i, val) {
                            arr = "or c.DutyDept like '%" + val + "%' " + arr;
                        });
                        where = where + " and (" + arr.substr(2, arr.length) + ")";
                    }
                    //工作職稱(以逗號分割)
                    var DutyTitle = $("#dataFormMasterDutyTitle").val();
                    if (DutyTitle != '') {
                        var arrT = DutyTitle.split(",");
                        var arr2 = "";
                        $.each(arrT, function (i, val) {
                            arr2 = "or c.DutyTitle like '%" + val + "%' " + arr2;
                        });
                        where = where + " and (" + arr2.substr(2, arr2.length) + ")";
                    }
                    
                    //語文種類grid
                    var rows = $('#dataGridDetail').datagrid('getRows');  // Return the current page rows
                    //語文條件 => 1 and,2 or
                    var value = $("input:radio[name='dataFormMasterJobLangNeedType_0']:checked").val();
                    //語文種類or and判斷=>or icount=1,and=rows.length       
                    var icount = rows.length;
                    if (value == 2)//or
                    {
                        icount = 1;
                    }
                    ////兜語文字串
                    //var swhere = "and";
                    //if (value == 2)//or
                    //{
                    //    swhere = " or";
                    //}
                    //var Langwhere="";
                    //if (rows.length > 0) {                      
                    //    for (var i = 0; i < rows.length; i++) {
                    //        var LangID = rows[i].LangID;
                    //        var ListenLevel = rows[i].ListenLevel;
                    //        var SayLevel = rows[i].SayLevel;
                    //        var ReadLevel = rows[i].ReadLevel;
                    //        var WriteLevel = rows[i].WriteLevel;
                    //        var LangLicence = rows[i].LangLicence;//證照
                    //        var LicenceScore = rows[i].LicenceScore;//證照分數
                    //        Langwhere = Langwhere+ " (u.LangID=" + LangID + " and u.ListenLevel<=" + ListenLevel + " and u.SayLevel<=" + SayLevel + " and u.ReadLevel<=" + ReadLevel + " and u.WriteLevel<=" + WriteLevel +
                    //             " and ISNULL(" + LangLicence + ",ISNULL(u.LangLicence,''))=ISNULL(" + LangLicence + ",'')" +
                    //             " and ISNULL(" + LicenceScore + ",0)<=ISNULL(u.LicenceScore,0) ) " + swhere;
                    //    }
                    //    Langwhere=Langwhere.substr(0, Langwhere.length - 3);
                    //    where = where + " and u.UserID in (select u.UserID from HUT_UserLang u where" + Langwhere;
                    //}
                    //由資料庫HUT_JobLangTemp中關聯
                    if (rows.length > 0) {
                        var JobID = $("#dataFormMasterJobID").val();
                        where = where + " and u.UserID in (select u.UserID from HUT_JobLangTemp l" +
                            " inner join HUT_UserLang u on l.LangID=u.LangID " +
                            " where l.JobID=" + JobID + " and l.ListenLevel>=u.ListenLevel and l.SayLevel>=u.SayLevel and l.ReadLevel>=u.ReadLevel and l.WriteLevel>=u.WriteLevel " +
                            " and ISNULL(l.LangLicence,ISNULL(u.LangLicence,''))=ISNULL(u.LangLicence,'')"+//證照
                            " and ISNULL(l.LicenceScore,0)<=ISNULL(u.LicenceScore,0)"+//證照分數
                            " group by u.UserID having count(u.UserID)>=" + icount + ")";
                    }

                    $(dg).datagrid('setWhere', where);                   
                }                               
            }
            //-------------------------------------------------Grid Result--------------------------------------------------------------
            //Grid開啟履歷連結
            function OpenUser(val, row) {                
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
            //control顯示加入餐盤按鈕
            function ShowAddMenu() {
                //取消Grid預設的第一筆勾選
                $("#JQDataResult").datagrid("unselectAll");                             
            }            
            function GridResultReload() {
                $('#JQDataResult').datagrid('reload');
            }
            function GridMenuReload() {
                $('#JQDataAssignNew').datagrid('reload');
            }

            // 加入餐盤 Call Method
            function AddMenu() {
                if ($("#JQDataResult").datagrid('getSelections').length == 0) {
                    alert('請勾選人才。');
                } else {
                    var pre = confirm("確定加入餐盤?");
                    if (pre == true) {
                        var rows = $('#JQDataResult').datagrid('getSelections');
                        var aUserID = [];
                        for(var i=0; i<rows.length; i++){
                            aUserID.push(rows[i].UserID);
                        }
                        var sUserID=aUserID.join(',');                        
                        var JobID = $("#dataFormMasterJobID").val();                       
                        $.ajax({
                            type: "POST",
                            url: '../handler/jqDataHandle.ashx?RemoteName=sJobQuery.QueryResult', //連接的Server端，command
                            data: "mode=method&method=" + "AddMenu" + "&parameters=1," + JobID + ",1," + rows.length + "," + sUserID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                            cache: false,
                            async: false,
                            success: function (data) {                                
                                GridResultReload();
                                GridMenuReload();
                                alert("已加入職缺餐盤！");                              
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                alert(xhr.status);
                                alert(thrownError);
                            }
                        });                        
                    }
                }
            }
            //-------------------------------------------------上傳檔案功能顯示控制(徵信報告)-------------------------------------------------------------- 
            function AssignStepFile(val, rowData) {
                var Step = rowData.AssignID;
                if (Step == 4) {
                    $('span[class="info-fileUpload-span"]').show();                    
                    var t = "徵信報告";                   
                    //$('span[class="l-btn-text icon-upload l-btn-icon-left"]').val('CCCCVVVV');
                    //取得span的值和把值填入span                   

                    $('span[class="l-btn-text icon-upload l-btn-icon-left"]', $("#JQFileUpload1").next()).css({ color: 'red' }).html("上傳" + t);
                    //$('span[class="l-btn-text icon-upload l-btn-icon-left"]"]').css('color','red');
                } else {
                    $('span[class="info-fileUpload-span"]').hide();
                }
            }            
            //上傳檔案路徑設定
            function beforeUpload(options) {
                var rowData = $("#JQDataAssignNew").datagrid('getSelected');
                options.upLoadFolder = '\\JB_Hunter\\file\\' + rowData.UserID;
                return true;
            }
            //寫路徑到DB
            function writeDBurl() {
                //夾檔(徵信報告)                               
                //var infofileUploadfile = $('.info-fileUpload-file', infofileUpload.next());//上傳路徑               
                //上傳檔案
                //infoFileUploadMethod($('#FileUpload1'));
                var rowData = $("#JQDataAssignNew").datagrid('getSelected');
                var infofileUpload = $('#JQFileUpload1');
                var infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next());//取得文件名稱
                var infofile = infofileUploadvalue.val();
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sJobQuery.JobAssignNew',
                    data: "mode=method&method=" + "writeDBurl" + "&parameters=" + rowData.UserID + "," + rowData.JobID + "," + rowData.AssignID + "," + infofile,
                    cache: false,
                    async: true,
                    success: function (data) {
                        GridMenuReload();
                        var t = "徵信報告";                      
                        alert(rowData.Name+" - "+t+"上傳成功！");
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
            }
            //下載上傳檔案
            function downloadScript(val, rowData) {
                if (val != null) {
                    return '<a href="../handler/HunterFileHandler.ashx?File=' + val + '&UserID=' + rowData.UserID + '">' + val + '</a>';
                }
            }
            //------------------------------------------------新增推薦紀錄------------------------------------------------
            function AddAssign(dg) {
                var UserID = dg.datagrid('getSelected').UserID;
                
                //新增推薦紀錄
                var AssignID = "";
                if (dg.datagrid('getSelections').length != 0) {
                    var AssignID = $("#OptionsStep").options('getValue');
                    //var AssignID = $("select#OptionsStep").val();
                    if(AssignID!="")
                    {                                           
                        var pre = confirm("確定新增推薦紀錄?");
                        if (pre == true) {

                            var JobID = $("#dataFormMasterJobID").val();
                            var AssignTime = $("#JQDateBox1").datebox("getValue");
                            $.ajax({
                                type: "POST",
                                url: '../handler/jqDataHandle.ashx?RemoteName=sJobQuery.QueryResult', //連接的Server端，command
                                data: "mode=method&method=" + "AddMenu" + "&parameters=2," + JobID + "," + AssignID + ",1," + UserID + "," + AssignTime, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                                cache: false,
                                async: false,
                                success: function (data) {
                                    GridMenuReload();
                                    alert("推薦紀錄新增成功！");
                                    //取消狀態選取
                                    var gg = $("#OptionsStep").data("infooptions").panel;
                                    $("input:radio", gg).attr("checked", false);
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    alert(xhr.status);
                                    alert(thrownError);
                                }
                            });
                        }
                        
                    }else alert("請選擇要推薦的狀態~");
                } else alert("餐盤目前無資料~");
            }
            //-------------------------------------------------推薦紀錄歷程-------------------------------------------------------------
            function aBtnOnRow(val, row) {
                return $('<a>', { href: '#', onclick: 'aBtnOnRowFunction(this)', theData: row.JobID, theData2: row.UserID }).linkbutton({ text: "<b><div style=\"color:Red\">" + val + "</div></b>", plain: true })[0].outerHTML;
            }
            var aBtnOnRowFunction = function (Target) {                            
                $("#JQDialog4").dialog("open");
                var JobID = $(Target).attr('theData');
                var UserID = $(Target).attr('theData2');
                $("#JQJobAssignLogs").datagrid('setWhere', "l.JobID='" + JobID + "' and l.UserID='"+UserID+"'");
            }
            function ControlButton2() {
                //推薦紀錄歷程-----刪除dialog下面存檔,關閉按鈕              
                $("#JQJobAssignLogs-SubmitDiv").remove();
            }
            function AddAssignNotes() {
                //參數依次是dialog的名字，dataform開啟的row，dialog的狀態,viewed，dialog方式。 
                openForm('#JQDialog2', $('#JQDataAssignNew').datagrid('getSelected'), "", 'dialog');
            }
            //推薦紀錄刪除控制=>刪除最新一筆(取消刪除動作並執行新指令)        
            function DeleteRow(rowData) {
                var pre = confirm("確定移除最新推薦紀錄?");
                if (pre == true) {
                    //callServerMethod
                    var row = $('#JQDataAssignNew').datagrid('getSelected');
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sJobQuery.JobAssignNew', 
                        data: "mode=method&method=" + "userDEL" + "&parameters=" + rowData.AssignNO + "," + rowData.UserID + "," + rowData.JobID,
                        cache: false,
                        async: true,
                    });
                    GridMenuReload();
                    return false; //取消刪除的動作 
                }
                else {
                    return false;
                }
            }
            //-------------------------------------------------推薦列表Grid連結-------------------------------------------------------------
            //推薦列表Grid 姓名 => 開啟履歷連結       
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
            //推薦列表Grid 原始履歷 => 開啟面談履歷預覽連結       
            function OpenResume(val, row) {
                var sUserID = row.UserID;
                // return $('<input type="submit" onclick="myclick()" value="view">')[0].outerHTML;
                return $('<input type="image" img  src="img/Resume.png" onclick="myclick(' + sUserID + ')" value="view">')[0].outerHTML;
            }
            function myclick(sUserID) {
                var LoginID = getClientInfo("UserID");
                window.open('../../NewHunter/Rec_Resume.html?JobID=0&UserID=' + sUserID + '&HunterID=' + LoginID, '面談履歷預覽');
            }
            //推薦列表Grid 推薦履歷 => 開啟推薦履歷連結       
            function OpenResume2(val, row) {
                var sUserID = row.UserID;
                var sJobID = row.JobID;
                return $('<input type="image" img  src="img/Resume2.png" onclick="myclick2(' + sUserID + ',' + sJobID + ')" value="view">')[0].outerHTML;
            }
            function myclick2(sUserID, sJobID) {
                var LoginID = getClientInfo("UserID");
                window.open('../../NewHunter/Rec_Resume.html?JobID=' + sJobID + '&UserID=' + sUserID + '&HunterID=' + LoginID, '推薦履歷');
            }
            //下載推薦履歷連結(判斷HUT_RUser =>downloadpdf是否有值,若無職則導向轉出頁面,否則打開PDF)
            function downloadpdf(val, row) {
                var JobResumeFileNameC = row.JobResumeFileNameC + ".pdf";//職缺對應的檔名
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
            //推薦列表Grid 推薦函 => 開啟推薦函連結       
            function OpenLetter(val, row) {
                var EnUserID = EnCodeMethod(row.UserID);
                var LoginID = getClientInfo("UserID");
                //return $('<input type="submit" onclick="myclick2()" value="view">')[0].outerHTML;
                return $('<input type="image" img  src="img/Letter.png" onclick="myclick3()" value="view">')[0].outerHTML;
            }
            function myclick3() {
                var LoginID = getClientInfo("UserID");
                window.open('../../NewHunter/index.html?HunterID=' + LoginID, '推薦函');
            }
            //推薦列表Grid 面談紀錄維護 
            function OpenInterview(val, row) {         
                return $('<a>', { href: '#', onclick: 'InterviewFunction(this)', theData: row.UserID }).linkbutton({ text: "<b><div style=\"color:Red\">" + val + "</div></b>", plain: true })[0].outerHTML;
            }
            var InterviewFunction = function (Target) {
                $("#JQDialog6").dialog("open");
                var UserID = $(Target).attr('theData');                
                $("#dataGridInterview").datagrid('setWhere', "UserID='" + UserID + "'");
            }
            //面談紀錄維護設預設值defaultInterview
            function SetUserID() {
                var row = $('#JQDataAssignNew').datagrid('getSelected');
                return row.UserID;
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

            //控制搜尋條件(簡易搜尋,全文檢索)
            function ControlShow(val) {
                //清空搜尋Grid
                $('#dataGridView').datagrid('setWhere', '1=0');
                if (val == 1) {
                    $("#querydataGridMaster").show();
                    $("#divFullSearch").hide();
                } else {
                    $("#querydataGridMaster").hide();
                    $("#divFullSearch").show();
                    //全文檢索加上ToolTip
                    $('#txtString').attr('title', '請輸入關鍵字:以 , 區隔');
                }
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
                    } else Mark = " or ";

                    var arrT = tString.split(",");
                    var arr2 = "";
                    $.each(arrT, function (i, val) {
                        arr2 = arr2 + Mark + val;
                    });
                    arr2 = arr2.substr(4, arr2.length);
                    arr2 = "'" + arr2 + "'";

                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sJobQuery.JobQuery',  //連接的Server端，command
                        data: "mode=method&method=" + "SearchJobFullIndex" + "&parameters=" + arr2,  //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入搜尋字串
                        cache: false,
                        async: true,
                        success: function (data) {
                            var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示

                            if (rows.length > 10) {
                                //通過loadData方法清除掉原有Grid中的舊有資料並填補分頁資料
                                $('#dataGridMaster').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                            } else {
                                $('#dataGridMaster').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
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
        </script>      
   
</head>
<body>

    <form id="form1" runat="server">
       <div class="panel">
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />               
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 170px">

                                
                                <JQTools:JQOptions ID="JQOptions1"  runat="server" OpenDialog="False" Width="80px" DialogWidth="180" OnSelect="ControlShow" >
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
                                            <td style="width: 126px"><JQTools:JQOptions ID="JQOptions2" runat="server" OpenDialog="False" DialogWidth="105" Width="110px">
                                    <Items>
                                        <JQTools:JQComboItem Selected="True" Text=" and " Value="1" />
                                        <JQTools:JQComboItem Selected="False" Text=" or " Value="2" />
                                    </Items>
                                </JQTools:JQOptions></td>
                                            <td>
                                    <input id="bnSelect2" type="button" value="搜尋" onclick="serverMethod()" />&nbsp;&nbsp; </td>
                                        </tr>
                                    </table>
                                
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="auto-style1">                                
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sJobQuery.JobQuery" runat="server" AutoApply="True"
                DataMember="JobQuery" Pagination="True" QueryTitle=""
                Title="職缺搜尋明細" AllowDelete="False" AllowInsert="False" AllowUpdate="True" QueryMode="Panel" AlwaysClose="True" EditDialogID="JQDialog1" ViewCommandVisible="False" AllowAdd="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" OnLoadSuccess="queryGrid">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="獵才顧問" Editor="text" FieldName="HunterName" MaxLength="50" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" MaxLength="128" Width="250" />
                    <JQTools:JQGridColumn Alignment="center" Caption="需求人數" Editor="text" FieldName="JobNeedCount" Width="100" />
                    <JQTools:JQGridColumn Alignment="center" Caption="職務類別" Editor="text" FieldName="FunctionName" Width="110" Format="" />
                    <JQTools:JQGridColumn Alignment="center" Caption="職類性質" Editor="text" FieldName="JobTypeName" MaxLength="0" Width="100" />
                </Columns>              
                <QueryColumns>
                    <JQTools:JQQueryColumn Caption="獵才顧問" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sHunter.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="j.HunterID" NewLine="True" RemoteMethod="False" Width="180" DefaultValue="" />
                    <JQTools:JQQueryColumn Caption="客戶簡稱" Condition="%%" DataType="string" Editor="text" EditorOptions="" FieldName="CustShortName" NewLine="True" RemoteMethod="False" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="職缺效期" Condition=">=" DataType="datetime" DefaultValue="_today" Editor="datebox" FieldName="JobCloseDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" />
                </QueryColumns>
            </JQTools:JQDataGrid>                            

                            </td>
                        </tr>
                        </table>
        </div>

     <JQTools:JQDialog ID="JQDialog1" runat="server" Title="職缺推薦作業" BindingObjectID="dataFormMaster" EditMode="Dialog" Width="990px" DialogLeft="10px" DialogTop="10px" Closed="False" Height="490px" >         
        <div id="tt" class="easyui-tabs">
            <div id="tab1" title="職缺條件搜尋人才">  
            <fieldset>
            <legend>職缺條件</legend>
    
                  <table style="width:100%;">
                      <tr>
                          <td class="auto-style1">
                              <JQTools:JQDataForm ID="dataFormMaster"  runat="server" Closed="False" ContinueAdd="False" DataMember="JobQuery" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="6" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" RemoteName="sJobQuery.JobQuery" ShowApplyButton="False" title="Tab1" ValidateStyle="Hint" ParentObjectID="" IsRejectON="False">
                                  <Columns>
                                      <JQTools:JQFormColumn Alignment="left" Caption="職缺代號" Editor="numberbox" FieldName="JobID" Visible="False" Width="120" ReadOnly="False" Span="1" RowSpan="1" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" ReadOnly="True" Span="1" Width="115" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" Span="1" Width="170" ReadOnly="True" Visible="True" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="性別需求" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Gender',remoteName:'sReferences.HUT_ZGENType',tableName:'HUT_ZGENType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobGender" Width="100" NewRow="False" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="起始年齡" Editor="numberbox" FieldName="JobAge1" Width="50" Span="1" Visible="True" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="終止年齡" Editor="numberbox" FieldName="JobAge2" Span="2" Width="50" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="教育程度" Editor="infocombobox" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'sReferences.HUT_ZEduLevel',tableName:'HUT_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EduLevelID" Span="1" Width="120" NewRow="False" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="最高學類" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SubjectName',remoteName:'sSearchFunction.HUT_EduSubject',tableName:'HUT_EduSubject',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EduSubject" Span="1" Width="180" NewRow="False" MaxLength="0" ReadOnly="False" RowSpan="1" Visible="True" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="最高科系" Editor="text" FieldName="EduDepart" Width="100" ReadOnly="False" Span="1" Visible="True" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="產業別" Editor="infocombobox" FieldName="IndustryType" Span="2" Width="190" EditorOptions="valueField:'ID',textField:'IndCategory',remoteName:'sSearchFunction.HUT_IndCategory',tableName:'HUT_IndCategory',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" MaxLength="0" NewRow="False" ReadOnly="False" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="工作部門" Editor="text" FieldName="DutyDept" MaxLength="0" ReadOnly="False" Span="2" Visible="True" Width="300" NewRow="True" RowSpan="1" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="工作職稱" Editor="text" FieldName="DutyTitle" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="300" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="語文種類" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:250,remoteName:'sJobLang.HUT_ZLangNeedType',tableName:'HUT_ZLangNeedType',valueField:'ID',textField:'NeedTypeName',columnCount:2,multiSelect:false,openDialog:false,selectOnly:false,items:[]" FieldName="JobLangNeedType" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="160" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="履歷有效天數" Editor="numberbox" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                  </Columns>
                                   
                              </JQTools:JQDataForm>    
                              <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" CheckOnSelect="True" DataMember="HUT_JobLangTemp" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RemoteName="sJobLang.HUT_JobLangTemp" Title="語文種類" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" ColumnsHibeable="False" RecordLockMode="None" Width="96%">
                                  <Columns>
                                      <JQTools:JQGridColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" Visible="False" Width="90" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="語文種類" Editor="infocombobox" FieldName="LangID" Width="90" EditorOptions="valueField:'LangID',textField:'LangName',remoteName:'sJobLang.HUT_ZLangType',tableName:'HUT_ZLangType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="聽" Editor="infocombobox" FieldName="ListenLevel" Width="70" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sJobLang.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="說" Editor="infocombobox" FieldName="SayLevel" Width="70" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sJobLang.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="讀" Editor="infocombobox" FieldName="ReadLevel" Width="70" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sJobLang.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="寫" Editor="infocombobox" FieldName="WriteLevel" Width="70" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sJobLang.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="90" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="90" QueryCondition="" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="要求證照" Editor="infocombobox" EditorOptions="valueField:'LangLicenceID',textField:'LangLicenceName',remoteName:'sJobLang.HUT_LangLicence',tableName:'HUT_LangLicence',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LangLicence" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="330" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="證照分數" Editor="numberbox" FieldName="LicenceScore" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="IPAddress" Editor="text" FieldName="IPAddress" Visible="False" Width="80" />
                                  </Columns>
                                  <TooItems>
                                      <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                                      <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="False" />
                                      <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="False" />
                                      <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="儲存" Visible="True" />
                                      <JQTools:JQToolItem Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" Visible="True" />
                                      <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                                      <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="False" />
                                  </TooItems>
                              </JQTools:JQDataGrid>
                              <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataGridDetail" EnableTheming="True">
                                  <Columns>
                                      <JQTools:JQDefaultColumn DefaultValue="" FieldName="JobID" RemoteMethod="False" DefaultMethod="SetJobID" CarryOn="False" />
                                      <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                      <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                      <JQTools:JQDefaultColumn DefaultValue="_ipaddress" FieldName="IPAddress" RemoteMethod="True" />
                                  </Columns>
                              </JQTools:JQDefault>
                              <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataGridDetail" EnableTheming="True">
                                  <Columns>
                                      <JQTools:JQValidateColumn CheckNull="True" FieldName="LangID" RemoteMethod="True" ValidateMessage="請選擇語文種類" ValidateType="None" />
                                      <JQTools:JQValidateColumn CheckNull="True" FieldName="ListenLevel" RemoteMethod="True" ValidateMessage="聽程度?" ValidateType="None" />
                                      <JQTools:JQValidateColumn CheckNull="True" FieldName="SayLevel" RemoteMethod="True" ValidateMessage="說程度?" ValidateType="None" />
                                      <JQTools:JQValidateColumn CheckNull="True" FieldName="ReadLevel" RemoteMethod="True" ValidateMessage="讀程度?" ValidateType="None" />
                                      <JQTools:JQValidateColumn CheckNull="True" FieldName="WriteLevel" RemoteMethod="True" ValidateMessage="寫程度?" ValidateType="None" />
                                  </Columns>
                              </JQTools:JQValidate>
                          </td>
                          <td valign="bottom">
                              <input id="bnSelect" type="button" value="搜尋" />
                          </td>
                      </tr>
                </table>
                
                 </fieldset><div id="divResult">
                    <JQTools:JQDataGrid ID="JQDataResult" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DataMember="QueryResult" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sJobQuery.QueryResult" Title="搜尋結果" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="100%" OnLoadSuccess="ShowAddMenu" EditDialogID="JQDialog5">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="Name" MaxLength="0" ReadOnly="False" Visible="True" Width="110" FormatScript="OpenUser" />
                            <JQTools:JQGridColumn Alignment="left" Caption="最高學歷" Editor="text" FieldName="sEdu" maxlength="0" ReadOnly="False" Visible="True" Width="280"/>
                            <JQTools:JQGridColumn Alignment="left" Caption="最近經歷" Editor="text" FieldName="sCareer" MaxLength="0" ReadOnly="True" Visible="True" Width="360" Frozen="False" QueryCondition="" Sortable="False"/>
                            <JQTools:JQGridColumn Alignment="center" Caption="黑名單" Editor="checkbox" FieldName="NotMatch" FormatScript="" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="True" Width="45" EditorOptions="on:1,off:0" Format="L-checkbox" />
                            <JQTools:JQGridColumn Alignment="center" Caption="照片" Editor="text" FieldName="PhotoFile" Format="Image,Folder:../NewHunter,Height:30" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="45" FormatScript="" QueryCondition="" />
                        </Columns>
                        <TooItems>
                            <JQTools:JQToolItem ItemType="easyui-linkbutton" OnClick="AddMenu" Text="加入推薦" Visible="True" Icon="icon-ok" />
                        </TooItems>
                    </JQTools:JQDataGrid>
                    <JQTools:JQDialog ID="JQDialog5" runat="server" BindingObjectID="dataFormResult" EditMode="Expand" Title="人才資訊" ShowSubmitDiv="False" Width="800px">
                        <JQTools:JQDataForm ID="dataFormResult" runat="server" Closed="False" ContinueAdd="False" DataMember="QueryResult" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="3" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="JQDataResult" RemoteName="sJobQuery.QueryResult" ShowApplyButton="False" ValidateStyle="Hint" Width="100%">
                            <Columns>
                                <JQTools:JQFormColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" maxlength="0" ReadOnly="True" Width="100" />
                                <JQTools:JQFormColumn Alignment="left" Caption="居住地址" Editor="text" FieldName="Address1" MaxLength="20" ReadOnly="True" Visible="True" Width="350" />
                                <JQTools:JQFormColumn Alignment="left" Caption="最後更新日" Editor="text" FieldName="LastUpdateDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="70" />
                                <JQTools:JQFormColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="MobileNo1" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="100" />
                                <JQTools:JQFormColumn Alignment="left" Caption="eMail " Editor="text" FieldName="eMail1" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="350" />
                                <JQTools:JQFormColumn Alignment="left" Caption="最新履歷狀態" Editor="text" FieldName="AssignName" MaxLength="0" ReadOnly="True" Visible="True" Width="100" NewRow="True" />
                                <JQTools:JQFormColumn Alignment="left" Caption="最新推薦資訊" Editor="text" FieldName="LastStatus" MaxLength="0" ReadOnly="True" Visible="True" Width="350" />

                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                            </RelationColumns>
                        </JQTools:JQDataForm>
                    </JQTools:JQDialog>
                </div>
</div>
            <div id="tab2" title="推薦列表" style="padding:20px;" >		        
                <table>
                    <tr>
                        <td colspan="3" style="width: 980px">
                            <asp:Label ID="Label1" runat="server" Text="客戶簡稱:"></asp:Label>
                            <asp:Label ID="lbCustomer" runat="server" ForeColor="#6600FF" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label2" runat="server" Text="職缺名稱:"></asp:Label>
                            <asp:Label ID="lbJobName" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#6600FF"></asp:Label>
                            <JQTools:JQComboBox ID="ComboJob" runat="server" DisplayMember="JobName" RemoteName="sCustomersJobs.HUT_Job" ValueMember="JobID" EnableTheming="True" Visible="False">
                            </JQTools:JQComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 80px; text-align: right;">
                            推薦狀態：</td>
                        <td colspan="2">
                            <JQTools:JQOptions ID="OptionsStep" runat="server" ColumnCount="0" DialogWidth="770" DisplayMember="AssignName" EnableTheming="True" OpenDialog="False" RemoteName="sJobQuery.AssignStep" ValueMember="AssignID">
                            </JQTools:JQOptions>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">推薦日期： </td>
                        <td style="width: 700px">
                            <JQTools:JQDateBox ID="JQDateBox1" runat="server" ShowSeconds="False" ShowTimeSpinner="False" Width="500px" />
                            <input id="bnAssign" type="button" value="新增推薦紀錄" />
                        </td>
                        <td>
                            <JQTools:JQFileUpload ID="JQFileUpload1" runat="server" onBeforeUpload="beforeUpload" onSuccess="writeDBurl" ShowLocalFile="False" SizeFieldName="" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 980px">
                            

                            <JQTools:JQDataGrid ID="JQDataAssignNew" data-options="pagination:true,view:commandview" RemoteName="sJobQuery.JobAssignNew" runat="server" AutoApply="True"
                DataMember="JobAssignNew" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog2" Title="" DeleteCommandVisible="True" ViewCommandVisible="False" OnDeleted="GridMenuReload" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" OnDelete="DeleteRow" OnSelect="AssignStepFile">
                <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="推薦狀態" Editor="text" FieldName="AssignName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="70" FormatScript="aBtnOnRow" />
                        <JQTools:JQGridColumn Alignment="center" Caption="推薦日期" Editor="text" FieldName="AssignTime" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="70" Format="yyyy/mm/dd" />
                        <JQTools:JQGridColumn Alignment="center" Caption="姓名" Editor="text" FieldName="Name" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="110" QueryCondition="" FormatScript="OpenUser" />
                        <JQTools:JQGridColumn Alignment="center" Caption="面談紀錄" Editor="text" FieldName="iInterview" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="65" FormatScript="OpenInterview" />
                        <JQTools:JQGridColumn Alignment="center" Caption="獵才顧問" Editor="text" FieldName="HunterName" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" />
                        <JQTools:JQGridColumn Alignment="right" Caption="行動電話" Editor="text" FieldName="MobileNo1" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="85" QueryCondition="" />
                        <JQTools:JQGridColumn Alignment="center" Caption="面談履歷預覽" Editor="text" FieldName="OpenResume" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="75" FormatScript="OpenResume" QueryCondition="" />
                        <JQTools:JQGridColumn Alignment="center" Caption="編修推薦履歷" Editor="text" FieldName="OpenResume2" FormatScript="OpenResume2" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75" />
                        <JQTools:JQGridColumn Alignment="center" Caption="推薦履歷" Editor="text" FieldName="downloadpdf" FormatScript="downloadpdf" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="center" Caption="推薦函" Editor="text" FieldName="OpenLetter" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="50" QueryCondition="" Format="" FormatScript="OpenLetter" />
                        <JQTools:JQGridColumn Alignment="right" Caption="徵信報告" Editor="text" FieldName="FileUrl2" Format="" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="100" FormatScript="downloadScript" />
                </Columns>
            </JQTools:JQDataGrid>

                            <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormMaster1" Title="推薦備註維護" DialogLeft="150px" DialogTop="150px" Width="600px" Height="300px">
                                <JQTools:JQDataForm ID="dataFormMaster1" runat="server" DataMember="JobAssignNew" HorizontalColumnsCount="2" RemoteName="sJobQuery.JobAssignNew" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="AssignNO" Editor="text" FieldName="AssignNO" maxlength="0" Width="180" Visible="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Width="180" Visible="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="numberbox" FieldName="JobID" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                                    </Columns>
                                </JQTools:JQDataForm>
                                <JQTools:JQDataGrid ID="dataGridDetail1" runat="server" AutoApply="False" DataMember="AssignNotes" EditDialogID="" Pagination="False" ParentObjectID="dataFormMaster1" RemoteName="sJobQuery.JobAssignNew" Title="" ViewCommandVisible="False" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" Height="200px" Width="520px" ColumnsHibeable="False" RecordLockMode="None">
                                    <Columns>
                                        <JQTools:JQGridColumn Alignment="left" Caption="推薦編號" Editor="text" FieldName="AssignNO" Width="120" Visible="False" />
                                        <JQTools:JQGridColumn Alignment="right" Caption="職缺代號" Editor="numberbox" FieldName="JobID" Width="120" Visible="False" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Visible="False" Width="80" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="推薦狀態" Editor="infocombobox" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'sJobQuery.AssignStep',tableName:'AssignStep',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:150" FieldName="AssignID" Visible="True" Width="100" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="推薦備註" Editor="textarea" FieldName="AssignNotes" Width="400" Visible="True" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Width="90" Visible="False" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Width="90" Format="yyyy/mm/dd" Visible="False" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                                    </Columns>
                                    <RelationColumns>
                                        <JQTools:JQRelationColumn FieldName="AssignNO" ParentFieldName="AssignNO" />
                                    </RelationColumns>
                                    <TooItems>
                                      <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />                                     
                                      <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="儲存" Visible="False" />
                                      <JQTools:JQToolItem Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" Visible="True" />
                                    </TooItems>
                                </JQTools:JQDataGrid>
                                <JQTools:JQDialog ID="JQDialog3" runat="server" BindingObjectID="dataFormDetail" EditMode="Switch">
                                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" Closed="False" ContinueAdd="False" DataMember="AssignNotes" DuplicateCheck="False" HorizontalColumnsCount="2" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sJobQuery.JobAssignNew" ShowApplyButton="False" ValidateStyle="Dialog" disapply="False" IsRejectON="False">
                                        <Columns>
                                            <JQTools:JQFormColumn Alignment="left" Caption="推薦編號" Editor="text" FieldName="AssignNO" Visible="False" Width="120" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="職缺代號" Editor="numberbox" FieldName="JobID" Visible="False" Width="120" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="推薦狀態" Editor="infocombobox" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'sJobQuery.AssignStep',tableName:'AssignStep',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssignID" Visible="True" Width="120" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="推薦備註" Editor="text" FieldName="AssignNotes" Width="280" Visible="True" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Visible="False" Width="120" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Visible="False" Width="120" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                                        </Columns>
                                        <RelationColumns>
                                            <JQTools:JQRelationColumn FieldName="AssignNO" ParentFieldName="AssignNO" />
                                            <JQTools:JQRelationColumn FieldName="JobID" ParentFieldName="JobID" />
                                        </RelationColumns>
                                    </JQTools:JQDataForm>
                                </JQTools:JQDialog>
                                <JQTools:JQDefault ID="defaultMaster1" runat="server" BindingObjectID="dataFormMaster1" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                                </JQTools:JQDefault>
                                <JQTools:JQValidate ID="validateMaster1" runat="server" BindingObjectID="dataFormMaster1" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                                </JQTools:JQValidate>
                                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataGridDetail1" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQDefaultColumn DefaultMethod="SetJobID" FieldName="JobID" RemoteMethod="False" />
                                        <JQTools:JQDefaultColumn FieldName="CreateBy" RemoteMethod="True" DefaultValue="_username" />
                                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn DefaultMethod="" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                                    </Columns>
                                </JQTools:JQDefault>
                                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AssignID" RemoteMethod="True" ValidateMessage="請選擇推薦狀態" ValidateType="None" />
                                    </Columns>
                                </JQTools:JQValidate>
                              <%--  <div id="JQDialog4">--%>
                                    <JQTools:JQDialog ID="JQDialog4" runat="server" BindingObjectID="" EditMode="Dialog" Title="推薦紀錄歷程" HorizontalAlign="Justify" DialogLeft="10px" DialogTop="10px" Width="400px" Height="330px">
                                    <JQTools:JQDataGrid ID="JQJobAssignLogs" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="AssignLogs" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="7,10,20,30,40,50" PageSize="7" Pagination="True" ParentObjectID="" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sJobQuery.AssignLogs" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="ControlButton2">
                                        <Columns>
                                            <JQTools:JQGridColumn Alignment="left" Caption="AssignNO" Editor="text" FieldName="AssignNO" Visible="False" Width="90" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="JobName" Editor="text" FieldName="JobName" Visible="False" Width="90" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="推薦時間" Editor="text" FieldName="AssignTime" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="177" Format="yyyy/mm/dd HH:MM:SS" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="推薦狀態" Editor="text" FieldName="AssignName" Width="120" />
                                        </Columns>
                                    </JQTools:JQDataGrid>
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
                              <%--  </div>--%>

                            </JQTools:JQDialog>




























                        </td>
                    </tr>
                </table>               
            </div>               
        </div>
    </JQTools:JQDialog>        
        <JQTools:JQImageContainer ID="JQImageContainer1" runat="server" AutoSize="False" Height="450px" HorizontalAlign="Center" Width="350px">

        </JQTools:JQImageContainer>

</form>
   
</body>
</html>
