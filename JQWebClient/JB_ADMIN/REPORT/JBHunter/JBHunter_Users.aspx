<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_Users.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <script type="text/javascript">
            $(document).ready(function () {

                ////--------------查詢條件組合-----------------------------------------------------------------
                var Age1 = $('#Age1_Query').closest('td');
                var Age2 = $('#Age2_Query').closest('td').children();
                Age1.append("&nbsp;&nbsp;～&nbsp;&nbsp;").append(Age2);

                var LangID = $('#LangID_Query').closest('td');
                var LangLevel = $('#LangLevel_Query').closest('td').children();
                LangID.append("&nbsp;&nbsp;程度&nbsp;&nbsp;").append(LangLevel);

                var FullSearch = $('#sFullSearch_Query').closest('td');
               // var AndOr = $('#AndOr_Query').closest('td').children();
                FullSearch.append("&nbsp;(★請輸入關鍵字:以 , 區隔)");//.append(AndOr);


                //設定 Grid QueryColunm panel寬度調整
                var dgid = $('#dataGridView');
                var queryPanel = getInfolightOption(dgid).queryDialog;
                if (queryPanel)
                    $(queryPanel).panel('resize', { width: 1050 });

                var spi2 = "&nbsp;&nbsp;&nbsp;&nbsp;"

                //出生日期 
                var Birthday = $('#dataFormMasterBirthday').closest('td')
                Birthday.append("&nbsp;例:1999/08/08");
                
                //原始面談顧問+履歷來源+照片   合併為同TD顯示   
                var UserHunterID = $('#dataFormMasterUserHunterID').closest('td');
                var PhotoFile = $('#dataFormMasterPhotoFile').closest('td').children()
                var UserSourse = $('#dataFormMasterUserSourse').closest('td').children();

                UserHunterID.append(spi2 + spi2 + "履歷來源&nbsp;").append(UserSourse).append(spi2 + spi2 + "照片&nbsp;").append(PhotoFile);

                //連絡電話1 + 連絡電話2 + 履歷來源  合併為同TD顯示
                var Mobile1Area = $('#dataFormMasterMobileNo1Area').closest('td');
                var Mobile1 = $('#dataFormMasterMobileNo1').closest('td').children();
                var Mobile2 = $('#dataFormMasterMobileNo2').closest('td').children();
                var Mobile2Area = $('#dataFormMasterMobileNo2Area').closest('td').children();
                var TelContryArea = $('#dataFormMasterTelContryArea').closest('td').children();
                var TelArea = $('#dataFormMasterTelArea').closest('td').children();
                var Tel = $('#dataFormMasterTel').closest('td').children();

                // 電話1
                Mobile1Area.append('&nbsp;電話1').append(Mobile1).append('&nbsp;例:0933-123456').append(spi2 + "國碼&nbsp;").append(Mobile2Area).append("&nbsp;電話2&nbsp;").append(Mobile2).append(spi2 + "國碼&nbsp;").append(TelContryArea).append("&nbsp;市話&nbsp;").append(TelArea).append("-").append(Tel);

                //即時通類型1 即時通類型+帳號合併縣顯示
                var imtype1 = $('#dataFormMasterContIMType1').closest('td');
                var imno1 = $('#dataFormMasterContIMNO1').closest('td').children();
                //即時通類型2 即時通類型+帳號合併縣顯示
                var imtype2 = $('#dataFormMasterContIMType2').closest('td').children();
                var imno2 = $('#dataFormMasterContIMNO2').closest('td').children();
                var DrivingLicense = $('#dataFormMasterDrivingLicense').closest('td').children();//駕駛執照
                var Traffic = $('#dataFormMasterTraffic').closest('td').children();//交通工具

                imtype1.append('&nbsp;&nbsp;&nbsp;帳號&nbsp;').append(imno1).append(spi2 + spi2 + '即時通2&nbsp;').append(imtype2).append('&nbsp;&nbsp;&nbsp;帳號&nbsp;').append(imno2).append('&nbsp;&nbsp;&nbsp;駕駛執照&nbsp;').append(DrivingLicense).append('&nbsp;&nbsp;&nbsp;交通工具&nbsp;').append(Traffic);

                //履歷薪資合併顯示
                var ExpPayType = $('#dataFormMasterExpPayType').closest('td');
                var ExpPay = $('#dataFormMasterExpPay').closest('td').children();
                var ExpPay2 = $('#dataFormMasterExpPay2').closest('td').children();
                var ExpPayDesc = $('#dataFormMasterExpPayDesc').closest('td').children();
                ExpPayType.append(ExpPay).append("～").append(ExpPay2).append("&nbsp;元").append(spi2 + "說明").append(ExpPayDesc);


                ////--------------學歷....字串結合-----------------------------------------------------------------
                var SchoolName1 = $('#dataFormMasterSchoolName1').closest('td');
                var EduID1 = $('#dataFormMasterEduID1').closest('td').children();
                var Department1 = $('#dataFormMasterDepartment1').closest('td').children();
                var GradStatus1 = $('#dataFormMasterGradStatus1').closest('td').children();
                var GraduateYear1 = $('#dataFormMasterGraduateYear1').closest('td').children();
                var SchoolArea1 = $('#dataFormMasterSchoolArea1').closest('td').children();
                SchoolName1.append(spi2 + "學歷").append(EduID1).append(spi2 + "科系").append(Department1).append(GradStatus1).append(spi2 + "就學期間").append(GraduateYear1).append(spi2 + "地點").append(SchoolArea1);
                //
                var SchoolName2 = $('#dataFormMasterSchoolName2').closest('td');
                var EduID2 = $('#dataFormMasterEduID2').closest('td').children();
                var Department2 = $('#dataFormMasterDepartment2').closest('td').children();
                var GradStatus2 = $('#dataFormMasterGradStatus2').closest('td').children();
                var GraduateYear2 = $('#dataFormMasterGraduateYear2').closest('td').children();
                var SchoolArea2 = $('#dataFormMasterSchoolArea2').closest('td').children();
                SchoolName2.append(spi2 + "學歷").append(EduID2).append(spi2 + "科系").append(Department2).append(GradStatus2).append(spi2 + "就學期間").append(GraduateYear2).append(spi2 + "地點").append(SchoolArea2);
              
                ////--------------推薦人....字串結合-----------------------------------------------------------------
                var ReferrerTel1 = $('#dataFormMaster4ReferrerTel1').closest('td');
                ReferrerTel1.append('例:0933-123456');
                var ReferrerTel2 = $('#dataFormMaster5ReferrerTel2').closest('td');
                ReferrerTel2.append('例:0933-123456');

                ////--------------工作經驗-----------------------------------------------------------------
                ////--------------工作經驗 薪資待遇等....字串結合-----------------------------------------------------------------

                var SalaryType = $('#DFUserCareerDutySalaryType').closest('td');
                var Salary = $('#DFUserCareerDutySalary').closest('td').children();
                var SalaryDesc = $('#DFUserCareerDutySalaryDesc').closest('td').children();
                SalaryType.append(Salary).append("&nbsp;元").append(spi2 + "說明").append(SalaryDesc);

                ////--------------工作經驗 公司規模-----------------------------------------------------------------
                var ComScale = $('#DFUserCareerComScale').closest('td');
                ComScale.append('人');
                ////--------------工作經驗 主管職稱,管理責任,部屬人數....字串結合-----------------------------------------------------------------
                var AdvisorTitle = $('#DFUserCareerAdvisorTitle').closest('td');
                var bManagement = $('#DFUserCareerbManagement').closest('td').children();
                var SubCount = $('#DFUserCareerSubCount').closest('td').children();
                AdvisorTitle.append(spi2 + spi2 + spi2).append("管理責任&nbsp;").append(bManagement).append("&nbsp;&nbsp;管理人數&nbsp;").append(SubCount);

                //-------在職期間------------------------------------------------------             
                var DutyDate1 = $('#DFUserCareerDutyDate').closest('td');
                var DutyDate2 = $('#DFUserCareerDutyDate2').closest('td').children();
                DutyDate1.append("&nbsp;～").append(DutyDate2);

                var CategoryID = $('#DFUserCareerCategoryID').closest('td')
                var OpenIndustry = $('#DFUserCareerOpenIndustry').closest('td').children();

                CategoryID.append("&nbsp;").append(OpenIndustry);

                //---------查詢產業類別=>呼叫104網頁-----------------------------------                
                //$("#DFUserCareer").form({
                //    onLoadSuccess: function (data) {
                //        $("td", "#DFUserCareer").each(function (index) {
                //            if ($(this).children().length == 0) {
                //                if ($(this).html() == "呼叫類別") {
                //                    $(this).html("");                                   
                //                    $('<input type="image" img  src="img/clock_red.png" onclick="OpenIndustry()">').attr({
                //                    }).appendTo(this);

                //                    $("#DFUserCareerOpenIndustry").closest('td').hide();
                //                }

                //            }
                //        });
                //    }
                //});

                var Link = $('<a>', { href: 'javascript:void(0)', name: 'OpenIndustry', onclick: 'OpenIndustry()' }).linkbutton({ plain: false, text: '產業類別參考' })[0].outerHTML
                var tdIndustry = $('#DFUserCareerOpenIndustry').closest('td');
                tdIndustry.append("&nbsp;&nbsp;&nbsp;&nbsp;" + Link);

                $("#DFUserCareerOpenIndustry").hide();
                //個人資料頁籤文字加紅色
                OnLoadMaster1();
                //setTimeout(function () {
                //    UserQuery();
                //}, 500);
                //Tooltip , title
                $('#dataFormMasterExpDutyArea').attr('title', '多工作地，請以 , 區隔');

                //實際營業額 合併顯示
                var iTurnoverReal = $('#DFJobAssignLogsiTurnoverReal').closest('td');
                var ratioReal = $('#DFJobAssignLogsratioReal').closest('td').children();
                var AmountReal = $('#DFJobAssignLogsAmountReal').closest('td').children();
                iTurnoverReal.append(' * ').append(ratioReal).append(' = ').append(AmountReal);


            });

            //-----------------實際營業額-----------------------------------------------------------------------------------
            function OnBluriTurnoverReal() {
                //var iTurnover = $("#dataFormMaster1iTurnover").val();
                var iTurnover = $("#DFJobAssignLogsiTurnoverReal").numberbox('getValue');
                var ratio = $("#DFJobAssignLogsratioReal").val();
                var Amount = Math.round(iTurnover * ratio);
                $("#DFJobAssignLogsAmountReal").numberbox('setValue', Amount);//四捨五入      
                $("#DFJobAssignLogsAmountReal").val(Amount);
            }

            //查詢產業類別=>呼叫104網頁
            function OpenIndustry(index) {                
                var sName = $("#DFUserCareerComName").val();

                window.open('https://www.104.com.tw/jobs/search/?ro=0&keyword=' + sName, '104連結參考');
            }
            function OnLoadMaster1() {
                //個人資料頁籤文字加紅色
                var HideFieldName = [ 'EduID1', 'Department1', 'GradStatus1', 'GraduateYear1', 'SchoolArea1'];
                var FormName = '#dataFormMaster1';

                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').css("color", "red");
                    $(FormName + fieldName).closest('td').prev('td').css("color", "red");
                    $(FormName + fieldName).closest('td').prev('td').show();
                    $(FormName + fieldName).closest('td').show();
                });

                //推薦人Email改成黑色
                var HideFieldName = ['ReferrerEmail1', 'ReferrerEmail2'];
                var FormName = '#dataFormMaster3';

                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').css("color", "black");
                });
                ////個人資料頁籤文字加黑色
                //var HideFieldName2 = ['MobileNo2'];
                //var FormName2 = '#dataFormMaster1';

                //$.each(HideFieldName2, function (index, fieldName2) {
                //    $(FormName2 + fieldName2).closest('td').css("color", "black");
                //    //$(FormName2 + fieldName2).closest('td').prev('td').css("color", "black");
                //    //$(FormName2 + fieldName2).closest('td').prev('td').show();
                //    $(FormName2 + fieldName2).closest('td').show();
                //});
            }
          
            //手機1=> 前4 - 後9
            function CheckMobileNo1(phone) {
                var regex = /^\d{2,4}-\d{6,9}$/;
                if (!regex.test(phone)) {
                    $("#dataFormMasterMobileNo1").focus();
                    return false;
                } else {
                    return true;
                }
            }
            //手機2=> 前4 - 後9
            function CheckMobileNo2(phone) {
                var regex = /^\d{2,4}-\d{6,9}$/;
                if (!regex.test(phone)) {
                    $("#dataFormMasterMobileNo2").focus();
                    return false;
                } else {
                    return true;
                }
            }
            //出生日期
            function CheckBirthday(Birthday) {
                var regex = /^\d{4}[\-/\\.](0?[1-9]|1[012])[\-/\\.](0?[1-9]|[12][0-9]|3[01])$/;
                if (!regex.test(Birthday)) {
                    $("#dataFormMasterBirthday").focus();
                    return false;
                } else {
                    return true;
                }
            }


            //以下是分頁方法，不需做任何修改，複製就可以
            function pagerFilter(data) {
                if (typeof data.length == 'number' && typeof data.splice == 'function') {    // is array
                    data = {
                        total: data.length,
                        rows: data
                    }
                }
                var dg = $(this);
                var opts = dg.datagrid('options');
                var pager = dg.datagrid('getPager');
                pager.pagination({
                    onSelectPage: function (pageNum, pageSize) {
                        opts.pageNumber = pageNum;
                        opts.pageSize = pageSize;
                        pager.pagination('refresh', {
                            pageNumber: pageNum,
                            pageSize: pageSize
                        });
                        dg.datagrid('loadData', data);
                    }
                });
                if (!data.originalRows) {
                    data.originalRows = (data.rows);
                }
                var start = (opts.pageNumber - 1) * parseInt(opts.pageSize);
                var end = start + parseInt(opts.pageSize);
                data.rows = (data.originalRows.slice(start, end));
                return data;
            }


            //以下是callServerMethod的方法，有元件id，server的dll和方法等，請做修改
            function UserQuery() {
                var Age1 = $('#Age1_Query').val();//年齡範圍
                var Age2 = $('#Age2_Query').val();
                var EduID = $('#EduID_Query').combobox('getValue');//最高學歷
                var LangID = $('#LangID_Query').combobox('getValue');//語文能力
                var LangLevel = $('#LangLevel_Query').combobox('getValue');//程度
                var GoodTools = $('#GoodTools_Query').val();//電腦技能
                var LicenseQA = $('#LicenseQA_Query').val();//證照資格
                var ExpDutyArea = $('#ExpDutyArea_Query').val();//期望工作地
                var ExpCategory = $('#ExpCategory_Query').val();//期望產業
                var ExpJobType = $('#ExpJobType_Query').val();//期望職務
                var ComName = $('#ComName_Query').val();//公司名稱
                var DutyTitle = $('#DutyTitle_Query').val();//公司職稱
                var CategoryID = $('#CategoryID_Query').refval('getValue');//產業類別
                var DutyContent = $('#DutyContent_Query').val();//工作內容

                var tString = $('#sFullSearch_Query').val();//搜尋字串
                //分割字串(以逗號分割)
                    //判斷 and or               
                    var value = $("#AndOr_Query").data("infooptions").panel;
                    //$("input:radio", value).attr("checked", 2);
                    var Chk = $("input:radio", value).get(0).checked;//是否選擇and ( and 1=>index 0,or 2=>index 1 )
                    var arrT = "";
                    var Mark = "";
                    if (Chk == true) {//是否選擇and=1,or=2
                        Mark = "1";
                    } else Mark = "2";

                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sHUTUser.HUT_User', //連接的Server端，command
                        data: "mode=method&method=" + "UsersQuery" + "&parameters=" + Age1 + "*" + Age2 + "*" + EduID + "*" + LangID + "*" + LangLevel + "*" + GoodTools + "*" + LicenseQA +
                             "*" + ExpDutyArea + "*" + ExpCategory + "*" + ExpJobType + "*" + ComName + "*" + DutyTitle + "*" + CategoryID + "*" + DutyContent + "*" + tString + "*" + Mark, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: true,
                        success: function (data1) {
                            var rows = $.parseJSON(data1);//將JSon轉會到Object類型提供給Grid顯示
                            var data = new Object();
                            data.rows = rows;
                            if (rows == null) {
                                $('#dataGridView').datagrid('loadData', []); //Grid清空資料         
                                alert("目前無符合人才！");
                            } else {
                                data.total = rows.length;
                                $('#dataGridView').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                            }
                        }
                    });
             
            }

            function queryGrid(dg) {//查詢後添加固定條件
                if ($(dg).attr('id') == 'dataGridView') {
                    UserQuery();
                }
            }

            function OnLoadDF() {
                //清空選擇 ----履歷來源
                if ($('#dataFormMasterUserSourse').combobox('getValue') == "") {
                    $('#dataFormMasterUserSourse').combobox('setValue', "");
                }
            }

            function MasterGridReload() {
                //再打開一次網頁---------------------------------------------------------------------------------------
                //if (getEditMode($("#dataFormMaster")) == 'updated') {
                //    openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
                //} else {
                    //reload
                    UserQuery();
                //}

            }

            //檢查手機1 ,2 格式
            function CheckContactMobile1(phone) {
                var regex = /^09\d{2}-\d{6}$/;
                if (!regex.test(phone)) {
                    $("#dataFormMasterMobileNo1").focus();
                    return false;
                } else {
                    return true;
                }
            }
            function CheckContactMobile2(phone) {
                var regex = /^09\d{2}-\d{6}$/;
                if (!regex.test(phone)) {
                    $("#dataFormMasterMobileNo2").focus();
                    return false;
                } else {
                    return true;
                }
            }           
            //--------------------------工作經驗-----------------------------------
            //JQDataGrid checkbox勾選
            function genCheckBox(val) {
                if (val)
                    return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
                else
                    return "<input  type='checkbox' onclick='return false;' />";
            }
            function OnLoadSuccessCareer() {
                //清空選擇=> 在職期間
                if ($('#DFUserCareerDutyDate').combobox('getValue') == "") {
                    $('#DFUserCareerDutyDate').combobox('setValue', "");
                }
                if ($('#DFUserCareerDutyDate2').combobox('getValue') == "") {
                    $('#DFUserCareerDutyDate2').combobox('setValue', "");
                }
            }
            function OnApplyCareer() {
                //檢查起訖在職期間	
                var DutyDate=$('#DFUserCareerDutyDate').combobox('getValue');
                var DutyDate2 = $('#DFUserCareerDutyDate2').combobox('getValue');
                if (DutyDate2 != " 迄今" && DutyDate2 < DutyDate) {
                    alert('在職期間區間有誤！');
                    return false;
                }
            }
            function OnAppliedCareer() {
                $("#DGUserCareer").datagrid('reload');
            }
            //預設選了起始期間,自動帶入結束時間
            function OnSelectDutyDate(rowData) {
                if ($('#DFUserCareerDutyDate2').combobox('getValue') == "") {
                    $("#DFUserCareerDutyDate2").combobox('setValue', rowData.sDate);
                }
            }

            //--------------------------面談紀錄-----------------------------------
            function LinkContactRecord(value, row, index) {
                if (value != null) {
                    return "<a href='javascript: void(0)' onclick='OpenContactRecord(" + index + ");' style='color:red;'>" + value + "</a>";
                }
                else  return "<a href='javascript: void(0)' onclick='OpenContactRecord(" + index + ");' style='color:red;'>新增</a>";
            }
            // open面談紀錄 dialog
            function OpenContactRecord(index) {
                $("#dataGridView").datagrid('selectRow', index);
                openForm('#Dialog_ContactRecord', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');

            }
            //完整顯示Grid聯繫紀錄
            function ShowAllGrid(value) {
                return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
            }
            //聯繫維護紀錄有變更時重整
            function OnInsertedContactRecord() {
                $("#DGContactRecord").datagrid('reload');
                UserQuery();
            }
            function OnDeletedContactRecord() {                
                $("#DGContactRecord").datagrid('reload');
                UserQuery();
            }

            //--------------推薦作業(紀錄)-----------------------------------------------------------------------------------------------   
         
            function AssignLink(value, row, index) {
                //if (value != null)
                return "<a href='javascript: void(0)' onclick='LinkAssign(" + index + ");' style='color:red;'>推薦 / " + value + "</a>";
                //else return "<a href='javascript: void(0)' onclick='LinkAssign(" + index + ");' style='color:red;'>新增</a>";
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
                if (AssignID == "1" )//&& row.AssignFile=="")
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
                UserQuery();
            }            
            function OnDeletedAssignLogs() {
                $('#DGJobAssignLogs').datagrid("reload");
                UserQuery();
            }
            //完整顯示Grid聯繫紀錄
            function ShowAllGrid(value) {
                return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
            }
            function OnSelectAssignID(rowData) {
                var AssignID = rowData.AssignID;//1推薦,2面試,3錄取,4未錄取,5報到,6未報到
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

            //-------------Report ---------------------------------------------------------------------------------------------------       
            //搜尋結果Grid => 開啟推薦函連結       
            function LinkResume(val, row, index) {
                var FileName = row.NameC;
                var UserID = row.UserID;
                var JobID = row.JobID;
                if (JobID == null) {
                    JobID = "";
                }
                return $('<a>', { href: "#", onclick: "OpenResume('" + FileName + "','" + UserID + "'," + JobID + ")", theData: row.UserID }).linkbutton({ text: "<img src=img/Record.png></a><b><div style=\"color:Red\"></div></b>", plain: true })[0].outerHTML;
            }
            function OpenResume(FileName,UserID, JobID) {
                var AutoKey = 0;
                var url = "../JB_ADMIN/REPORT/JBHunter/RecommendReport.aspx?FileName=" + FileName + "&UserID=" + UserID + "&JobID=" + JobID + "&AutoKey=" + AutoKey;

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
                    title: "履歷表",
                    //maximizable: true                              
                });
                $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="98%"></iframe>').appendTo(dialog.find('.panel-body'));
                dialog.dialog('open');

            }
            //Grid 報告連結
            function RecommendLink(value, row, index) {
                if (row.AssignID == "1") {//推薦=>推薦報告
                    return $('<a>', { href: 'javascript:void(0)', name: 'RecommendLink', onclick: 'OpenReport(' + index + ',1)'}).linkbutton({ plain: false, text: '產生報告' })[0].outerHTML;
                } else if (row.AssignID == "5") {//報到=>請款明細
                    return $('<a>', { href: 'javascript:void(0)', name: 'PleasePayLink', onclick: 'OpenReport(' + index + ',5)'}).linkbutton({ plain: false, text: '請款明細' })[0].outerHTML;
                } else return "";
            }
            //推薦報告
            function OpenReport(index,sAssignID) {                
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
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />

            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sHUTUser.HUT_User" runat="server" AutoApply="True" 
                            DataMember="HUT_User" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                            Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" ColumnsHibeable="False" RecordLockMode="None" Width="1050px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="中文姓名" Editor="text" FieldName="NameC" MaxLength="20" Width="55" EditorOptions="" Visible="True" Sortable="False" />
                                <JQTools:JQGridColumn Alignment="left" Caption="英文姓名" Editor="text" FieldName="NameE" MaxLength="20" Width="66" EditorOptions="" Visible="True" Sortable="False" />
                                <JQTools:JQGridColumn Alignment="center" Caption="原始面談顧問" Editor="text" FieldName="HunterName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="面談紀錄" Editor="text" FieldName="ContactDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" FormatScript="LinkContactRecord"></JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="推薦職缺" Editor="text" FieldName="iJob" FormatScript="AssignLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="履歷表" Editor="text" FieldName="LinkResume" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" FormatScript="LinkResume"></JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="性別" Editor="text" FieldName="GenderText" MaxLength="30" Width="38" Sortable="True" />
                                <JQTools:JQGridColumn Alignment="center" Caption="年齡" Editor="text" FieldName="iAge" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="38">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="連絡電話1" Editor="text" FieldName="MobileNo1" MaxLength="0" Width="79" />
                                <JQTools:JQGridColumn Alignment="left" Caption="期望工作地" Editor="text" FieldName="ExpDutyArea" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="94" Format="" FormatScript="" />
                                <JQTools:JQGridColumn Alignment="left" Caption="期望產業" Editor="text" FieldName="ExpCategory" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="94">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="期望職務" Editor="text" FieldName="ExpJobType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="94">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="更新日期" Editor="datebox" FieldName="LastUpdateDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" />
                                <JQTools:JQGridColumn Alignment="center" Caption="更新人員" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" EditorOptions="">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="73">
                                </JQTools:JQGridColumn>
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增人才履歷" />                                                           
                            </TooItems>
                            <QueryColumns>
                                <JQTools:JQQueryColumn AndOr="and" Caption="年齡範圍" Condition="=" DataType="number" Editor="numberbox" FieldName="Age1" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="1" Width="50" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="=" DataType="number" Editor="numberbox" FieldName="Age2" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="最高學歷" Condition="%" DataType="string" Editor="infocombobox" FieldName="EduID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'sHUTUser.infoHUT_ZEduLevel',tableName:'infoHUT_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:120" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="語文能力" Condition="%" DataType="string" Editor="infocombobox" FieldName="LangID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="1" Width="95" EditorOptions="valueField:'LangID',textField:'LangName',remoteName:'sHUTUser.infoHUT_ZLangType',tableName:'infoHUT_ZLangType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" Editor="infocombobox" FieldName="LangLevel" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="70" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sHUTUser.infoHUT_ZLangLevel',tableName:'infoHUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="電腦技能" Condition="%" DataType="string" Editor="text" FieldName="GoodTools" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="150" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="證照資格" Condition="%" DataType="string" Editor="text" FieldName="LicenseQA" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="150" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="期望工作地" Condition="%" DataType="string" Editor="text" FieldName="ExpDutyArea" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="150" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="期望產業" Condition="%" DataType="string" Editor="text" FieldName="ExpCategory" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="2" Width="150" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="期望職務" Condition="%" DataType="string" Editor="text" FieldName="ExpJobType" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="150" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="公司名稱" Condition="%" DataType="string" Editor="text" FieldName="ComName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="150" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="公司職稱" Condition="%" DataType="string" Editor="text" FieldName="DutyTitle" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="150" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="產業類別" Condition="%" DataType="string" Editor="inforefval" EditorOptions="title:'請選擇',panelWidth:500,panelHeight:310,remoteName:'sHUTUser.infoUserCareerCategory',tableName:'infoUserCareerCategory',columns:[{field:'CategoryName',title:'類別',width:455,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CategoryID',textField:'CategoryName',valueFieldCaption:'CategoryID',textFieldCaption:'CategoryName',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="CategoryID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="2" Width="230" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="工作內容" Condition="%" DataType="string" Editor="text" FieldName="DutyContent" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="150" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="全文檢索" Condition="%" DataType="string" Editor="text" FieldName="sFullSearch" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="3" Width="300" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" DefaultValue="and" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:65,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'and',value:'and'},{text:'or',value:'or'}]" FieldName="AndOr" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="2" Width="70" />
                            </QueryColumns>
                        </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="履歷維護" DialogLeft="9px" DialogTop="1px" Width="1050px" Height="100%">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" AlwaysReadOnly="False" ChainDataFormID="dataFormMaster2" Closed="False" ContinueAdd="False" DataMember="HUT_User" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="9" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="MasterGridReload" OnLoadSuccess="OnLoadDF" ParentObjectID="" RemoteName="sHUTUser.HUT_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="中文姓名" Editor="text" FieldName="NameC" MaxLength="20" Span="1" Width="65" NewRow="False" Visible="True" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="英文姓名" Editor="text" FieldName="NameE" MaxLength="128" Span="2" Width="90" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出生日期" Editor="datebox" FieldName="Birthday" MaxLength="0" NewRow="False" Span="2" Width="90" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="虛擬日期" Editor="checkbox" FieldName="bBirthday" MaxLength="0" NewRow="False" Span="1" Visible="True" Width="23" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="婚姻狀況" Editor="infocombobox" FieldName="MarriageStatus" MaxLength="128" Span="1" Width="105" NewRow="False" EditorOptions="valueField:'ID',textField:'Marriage',remoteName:'sHUTUser.infoHUT_ZMarType',tableName:'infoHUT_ZMarType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:120,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:true,items:[{text:'女',value:'0'},{text:'男',value:'1'}]" FieldName="Gender" MaxLength="20" NewRow="False" Span="2" Width="80" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="國碼1" Editor="infocombobox" EditorOptions="valueField:'ContryAreaID',textField:'sAreaID',remoteName:'sCustomersJobs.HUT_ContryArea',tableName:'HUT_ContryArea',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="MobileNo1Area" MaxLength="0" Span="9" Width="102" NewRow="True" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="MobileNo1" MaxLength="0" NewRow="True" ReadOnly="False" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'ContryAreaID',textField:'sAreaID',remoteName:'sCustomersJobs.HUT_ContryArea',tableName:'HUT_ContryArea',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="MobileNo2Area" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="102" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="MobileNo2" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="通訊地址" Editor="text" FieldName="Address1" MaxLength="0" NewRow="True" Span="3" Visible="True" Width="260" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" 原始顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="UserHunterID" MaxLength="0" NewRow="False" Span="6" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'UserSourse',textField:'UserSourse',remoteName:'sHUTUser.infoUserSourse',tableName:'infoUserSourse',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="UserSourse" MaxLength="0" NewRow="False" Span="1" Width="105" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infofileupload" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'Files/Hunter/Users',showButton:true,showLocalFile:false,fileSizeLimited:'1000'" FieldName="PhotoFile" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="即時通1" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'IMNAME',remoteName:'sCustomersJobs.HUT_ZIMType',tableName:'HUT_ZIMType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="ContIMType1" MaxLength="20" NewRow="True" Span="8" Width="78" Visible="True" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContIMNO1" MaxLength="0" NewRow="True" Span="1" Width="120" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'ID',textField:'IMNAME',remoteName:'sCustomersJobs.HUT_ZIMType',tableName:'HUT_ZIMType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="ContIMType2" MaxLength="20" NewRow="False" Span="1" Width="78" ReadOnly="False" Visible="True" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContIMNO2" MaxLength="0" NewRow="False" Span="1" Visible="True" Width="120" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" EditorOptions="" FieldName="DrivingLicense" MaxLength="0" NewRow="False" Span="1" Width="118" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" EditorOptions="" FieldName="Traffic" MaxLength="0" Span="1" Width="118" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最高學歷" Editor="text" FieldName="SchoolName1" MaxLength="0" Span="9" Width="160" NewRow="True" ReadOnly="False" Visible="True" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="EduID1" MaxLength="0" NewRow="False" Span="1" Width="70" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'sHUTUser.infoHUT_ZEduLevel',tableName:'infoHUT_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:120" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="Department1" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="150" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="GradStatus1" MaxLength="0" Span="1" Width="65" EditorOptions="items:[{value:'1',text:'畢業',selected:'true'},{value:'2',text:'肄業',selected:'false'},{value:'3',text:'就學中',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:130" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="GraduateYear1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="135" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="SchoolArea1" MaxLength="0" Span="1" Width="82" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="次高學歷" Editor="text" FieldName="SchoolName2" MaxLength="0" Span="9" Width="160" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="EduID2" MaxLength="0" Span="1" Width="70" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'sHUTUser.infoHUT_ZEduLevel',tableName:'infoHUT_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:120" NewRow="False" ReadOnly="False" Visible="True" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="Department2" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="150" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="GradStatus2" MaxLength="0" Span="1" Width="65" EditorOptions="items:[{value:'1',text:'畢業',selected:'true'},{value:'2',text:'肄業',selected:'false'},{value:'3',text:'就學中',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:130" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="GraduateYear2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="135" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="SchoolArea2" MaxLength="0" Span="1" Width="82" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="人才Email" Editor="text" FieldName="eMail1" MaxLength="300" Span="3" Width="260" NewRow="True" ReadOnly="False" Visible="True" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="期望薪資" Editor="infocombobox" FieldName="ExpPayType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="6" Visible="True" Width="60" EditorOptions="items:[{value:'2',text:'月薪',selected:'true'},{value:'1',text:'年薪',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="ExpPay" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="63" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="ExpPay2" MaxLength="0" NewRow="False" Span="1" Width="63" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ExpPayDesc" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="335" />
                        <JQTools:JQFormColumn Alignment="left" Caption="期望工作地" Editor="textarea" FieldName="ExpDutyArea" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="260" EditorOptions="height:30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="期望產業" Editor="textarea" FieldName="ExpCategory" MaxLength="0" Span="3" Width="260" EditorOptions="height:30" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="期望職務" Editor="textarea" FieldName="ExpJobType" MaxLength="0" Span="3" Width="280" EditorOptions="height:30" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作專長" Editor="textarea" EditorOptions="height:80" FieldName="WorkSkill" MaxLength="0" NewRow="True" ReadOnly="False" Span="3" Visible="True" Width="260" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電腦技能" Editor="textarea" FieldName="GoodTools" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="260" EditorOptions="height:80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="證照資格" Editor="textarea" EditorOptions="height:80" FieldName="LicenseQA" MaxLength="0" NewRow="False" Span="3" Visible="True" Width="280" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" MaxLength="20" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />

                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" MaxLength="20" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" MaxLength="0" Visible="False" Width="180" NewRow="False" ReadOnly="False" Span="1" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" MaxLength="0" Visible="False" Width="80" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'ContryAreaID',textField:'sAreaID',remoteName:'sCustomersJobs.HUT_ContryArea',tableName:'HUT_ContryArea',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="TelContryArea" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="105" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" FieldName="TelArea" MaxLength="0" NewRow="False" Span="1" Width="45" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" FieldName="Tel" MaxLength="0" NewRow="False" Span="1" Width="66" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn DefaultValue="自動編號" FieldName="UserID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" CarryOn="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="NameC" RemoteMethod="True" ValidateMessage="" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Birthday" RemoteMethod="False" ValidateType="None" CheckMethod="CheckBirthday" ValidateMessage="出生日期格式不對！" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Gender" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="eMail1" RemoteMethod="True" ValidateType="EMail" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="MobileNo1" RemoteMethod="False" ValidateType="None" CheckMethod="CheckMobileNo1" ValidateMessage="電話1格式不對！" />
                        <JQTools:JQValidateColumn CheckMethod="CheckMobileNo2" CheckNull="False" FieldName="MobileNo2" RemoteMethod="False" ValidateMessage="電話2格式不對！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Address1" RemoteMethod="True" ValidateMessage="" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ExpDutyArea" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ExpPayType" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ExpPay" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EduID1" RemoteMethod="True" ValidateMessage="請選擇學歷！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SchoolName1" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SchoolArea1" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Department1" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="GraduateYear1" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="GradStatus1" RemoteMethod="True" ValidateMessage="請選擇狀態！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="UserHunterID" RemoteMethod="True" ValidateMessage="請選擇原始面談顧問！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>                    
            <JQTools:JQDataGrid ID="DGUserCareer" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_UserCareer" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogUserCareer" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnUpdate="" PageList="5,10,15,20" PageSize="5" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sHUTUser.HUT_User" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="100%">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="ComName" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="150">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="產業類別" Editor="infocombobox" FieldName="CategoryID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="200" EditorOptions="valueField:'CategoryID',textField:'LastCategoryName',remoteName:'sHUTUser.infoUserCareerCategory',tableName:'infoUserCareerCategory',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="職稱" Editor="text" FieldName="DutyTitle" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="在職期間" Editor="infocombobox" FieldName="DutyDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="52" EditorOptions="valueField:'sDate',textField:'sDate',remoteName:'sHUTUser.infoDutyDate',tableName:'infoDutyDate',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption=" " Editor="infocombobox" EditorOptions="valueField:'sDate',textField:'sDate',remoteName:'sHUTUser.infoDutyDate2',tableName:'infoDutyDate2',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="DutyDate2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="52">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="地點" Editor="text" FieldName="ComArea" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption=" " Editor="infocombobox" EditorOptions="items:[{value:'1',text:'年薪',selected:'true'},{value:'2',text:'月薪',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="DutySalaryType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="34">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="薪資待遇" Editor="text" FieldName="DutySalary" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="53">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="管理責任" Editor="checkbox" FieldName="bManagement" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="55" EditorOptions="on:1,off:0" FormatScript="genCheckBox">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="部屬數" Editor="text" FieldName="SubCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="45">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="主管職稱" Editor="text" FieldName="AdvisorTitle" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="67">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="工作內容" Editor="textarea" FieldName="DutyContent" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="230">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="ComScale" Editor="text" FieldName="ComScale" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="DutyPerform" Editor="textarea" FieldName="DutyPerform" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="DutyPromo" Editor="textarea" FieldName="DutyPromo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="更新日期" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Format="yyyy/mm/dd">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="更新人員" Editor="text" FieldName="LastUpdateby" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="OpenIndustry" Editor="text" FieldName="OpenIndustry" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="DutySalaryDesc" Editor="text" FieldName="DutySalaryDesc" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="LeaveReason" Editor="textarea" FieldName="LeaveReason" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <RelationColumns>
                    <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                </RelationColumns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增工作經驗" />
                </TooItems>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialogUserCareer" runat="server" BindingObjectID="DFUserCareer" DialogLeft="100px" DialogTop="40px" Title="工作經驗維護" Width="900px">
                <JQTools:JQDataForm ID="DFUserCareer" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_UserCareer" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="7" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sHUTUser.HUT_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnLoadSuccess="OnLoadSuccessCareer" OnApplied="OnAppliedCareer" OnApply="OnApplyCareer">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="ComName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="210" />
                        <JQTools:JQFormColumn Alignment="left" Caption="在職期間" Editor="infocombobox" FieldName="DutyDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="90" EditorOptions="valueField:'sDate',textField:'sDate',remoteName:'sHUTUser.infoDutyDate',tableName:'infoDutyDate',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnSelectDutyDate,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'sDate',textField:'sDate',remoteName:'sHUTUser.infoDutyDate2',tableName:'infoDutyDate2',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="DutyDate2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="產業類別" Editor="inforefval" EditorOptions="title:'請輸入關鍵字後滑鼠離開焦點可做搜尋',panelWidth:650,panelHeight:310,remoteName:'sHUTUser.infoUserCareerCategory',tableName:'infoUserCareerCategory',columns:[{field:'CategoryName',title:'產業類別',width:615,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CategoryID',textField:'CategoryName',valueFieldCaption:'CategoryID',textFieldCaption:'CategoryName',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CategoryID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="6" Visible="True" Width="600" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="OpenIndustry" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="0" EditorOptions="" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="DutyTitle" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="210" />
                        <JQTools:JQFormColumn Alignment="left" Caption="薪資待遇" Editor="infocombobox" EditorOptions="items:[{value:'2',text:'月薪',selected:'true'},{value:'1',text:'年薪',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="DutySalaryType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="60" />
                        <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" FieldName="DutySalary" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" EditorOptions="precision:0,groupSeparator:',',prefix:''" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="DutySalaryDesc" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="267" />
                        <JQTools:JQFormColumn Alignment="right" Caption="公司規模" Editor="numberbox" EditorOptions="" FieldName="ComScale" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="地點" Editor="text" FieldName="ComArea" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="89" />
                        <JQTools:JQFormColumn Alignment="left" Caption="主管職稱" Editor="text" FieldName="AdvisorTitle" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="215" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" FieldName="bManagement" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" FieldName="SubCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="70" EditorOptions="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作內容" Editor="textarea" EditorOptions="height:90" FieldName="DutyContent" MaxLength="3000" NewRow="True" ReadOnly="False" RowSpan="1" Span="7" Visible="True" Width="750" />
                        <JQTools:JQFormColumn Alignment="left" Caption="績效貢獻" Editor="textarea" EditorOptions="height:50" FieldName="DutyPerform" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="7" Visible="True" Width="750" />
                        <JQTools:JQFormColumn Alignment="left" Caption="晉升歷程" Editor="textarea" EditorOptions="height:25" FieldName="DutyPromo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="7" Visible="True" Width="750" />
                        <JQTools:JQFormColumn Alignment="left" Caption="離職原因" Editor="textarea" FieldName="LeaveReason" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="7" Visible="True" Width="750" EditorOptions="height:25" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateby" Editor="text" FieldName="LastUpdateby" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                    </RelationColumns>
                </JQTools:JQDataForm>
            </JQTools:JQDialog>
            <JQTools:JQDefault ID="JQDefaultCareer" runat="server" BindingObjectID="DFUserCareer" EnableTheming="True">
                <Columns>
                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCODE" FieldName="UserID" RemoteMethod="True" />
                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="LastUpdateby" RemoteMethod="True" />
                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="CreateBy" RemoteMethod="True" />
                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2" FieldName="DutySalaryType" RemoteMethod="True" />
                </Columns>
            </JQTools:JQDefault>
            <JQTools:JQValidate ID="JQValidateCareer" runat="server" BindingObjectID="DFUserCareer" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ComName" RemoteMethod="True" ValidateMessage="公司名稱不可空白！" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ComScale" RemoteMethod="True" ValidateMessage="請選擇公司規模！" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="DutyTitle" RemoteMethod="True" ValidateMessage="職稱不可空白！" ValidateType="None" />
<%--                                <JQTools:JQValidateColumn CheckNull="True" FieldName="DutyDate" RemoteMethod="True" ValidateMessage="在職期間不可空白！" ValidateType="None" />--%>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ComArea" RemoteMethod="True" ValidateMessage="地點不可空白！" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="DutySalaryType" RemoteMethod="True" ValidateMessage="請選擇月薪/年薪！" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="DutySalary" RemoteMethod="True" ValidateMessage="薪資待遇不可空白！" ValidateType="None" />
<%--                                <JQTools:JQValidateColumn CheckNull="True" FieldName="AdvisorTitle" RemoteMethod="True" ValidateMessage="直屬主管職稱不可空白！" ValidateType="None" />--%>
<%--                                <JQTools:JQValidateColumn CheckNull="True" FieldName="bManagement" RemoteMethod="True" ValidateMessage="管理責任不可空白！" ValidateType="None" />--%>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="DutyContent" RemoteMethod="True" ValidateMessage="工作內容不可空白！" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>
            <JQTools:JQAutoSeq ID="JQAutoSeqCareer" runat="server" BindingObjectID="DFUserCareer" FieldName="AutoKey" NumDig="1" />                                       
                <%-- <JQTools:JQValidate ID="validateMaster3" runat="server" BindingObjectID="dataFormMaster3" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ComputerSkill" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                    </JQTools:JQValidate>   --%>                   
                <JQTools:JQDataGrid ID="DGUserLang" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_UserLang" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogUserLang" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnUpdate="" PageList="5,10,15,20" PageSize="5" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sHUTUser.HUT_User" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="805px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="語文種類" Editor="infocombobox" EditorOptions="valueField:'LangID',textField:'LangName',remoteName:'sHUTUser.infoHUT_ZLangType',tableName:'infoHUT_ZLangType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="LangID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="程度" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sHUTUser.infoHUT_ZLangLevel',tableName:'infoHUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LangLevel" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="說明" Editor="text" FieldName="LangDesc" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="265">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="證照" Editor="text" FieldName="LangLicense" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="265">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateby" Editor="text" FieldName="LastUpdateby" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增語文能力" Visible="True" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="JQDialogUserLang" runat="server" BindingObjectID="DFUserLang" DialogLeft="160px" DialogTop="100px" Title="語文能力維護" Width="500px">
                    <JQTools:JQDataForm ID="DFUserLang" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_UserLang" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sHUTUser.HUT_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="語文種類" Editor="infocombobox" EditorOptions="valueField:'LangID',textField:'LangName',remoteName:'sHUTUser.infoHUT_ZLangType',tableName:'infoHUT_ZLangType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="LangID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                            <JQTools:JQFormColumn Alignment="left" Caption="語文程度" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sHUTUser.infoHUT_ZLangLevel',tableName:'infoHUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LangLevel" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                            <JQTools:JQFormColumn Alignment="left" Caption="說明" Editor="textarea" EditorOptions="height:50" FieldName="LangDesc" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="350" />
                            <JQTools:JQFormColumn Alignment="left" Caption="證照" Editor="textarea" EditorOptions="height:50" FieldName="LangLicense" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="350" />
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateby" Editor="text" FieldName="LastUpdateby" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="JQDefaultLang" runat="server" BindingObjectID="DFUserLang" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="LastUpdateby" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQAutoSeq ID="JQAutoSeqLang" runat="server" BindingObjectID="DFUserLang" FieldName="AutoKey" NumDig="1" />
                <JQTools:JQValidate ID="JQValidateLang" runat="server" BindingObjectID="DFUserLang" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="LangID" RemoteMethod="True" ValidateMessage="請選擇語文種類！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="LangLevel" RemoteMethod="True" ValidateMessage="請選擇程度！" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>                   
                   
                <JQTools:JQDataForm ID="dataFormMaster2" runat="server" AlwaysReadOnly="False" ChainDataFormID="dataFormMaster3" Closed="False" ContinueAdd="False" DataMember="HUT_User" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="True" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="" RemoteName="sHUTUser.HUT_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="中文自傳" Editor="textarea" FieldName="ChnBio" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="930" EditorOptions="height:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="英文自傳" Editor="textarea" FieldName="EngBio" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="930" EditorOptions="height:200" />
                    </Columns>
                </JQTools:JQDataForm>                                                                                                                                     
                <JQTools:JQDataForm ID="dataFormMaster3" runat="server" AlwaysReadOnly="False" ChainDataFormID="" Closed="False" ContinueAdd="False" DataMember="HUT_User" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="5" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="True" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="" RemoteName="sHUTUser.HUT_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="推薦人1姓名" Editor="text" FieldName="ReferrerName1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="服務單位" Editor="text" FieldName="ReferrerComp1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="ReferrerTitle1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="146" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="ReferrerTel1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="E-mail" Editor="text" FieldName="ReferrerEmail1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="250" />
                        <JQTools:JQFormColumn Alignment="left" Caption="推薦人2姓名" Editor="text" FieldName="ReferrerName2" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="服務單位" Editor="text" FieldName="ReferrerComp2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="ReferrerTitle2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="146" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="ReferrerTel2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="E-mail" Editor="text" FieldName="ReferrerEmail2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="250" />
                        <JQTools:JQFormColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQValidate ID="validateMaster3" runat="server" BindingObjectID="dataFormMaster3" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="False" FieldName="ReferrerEmail1" RemoteMethod="True" ValidateType="EMail" />
                        <JQTools:JQValidateColumn CheckNull="False" FieldName="ReferrerEmail2" RemoteMethod="True" ValidateType="EMail" />
                    </Columns>
                </JQTools:JQValidate>                                                                        
                             
            </JQTools:JQDialog>

                <JQTools:JQDialog ID="JQDialogJobAssignLogs" runat="server" BindingObjectID="dataFormMaster8" Title="推薦作業" Width="990px" DialogLeft="36px" DialogTop="30px" Height="450px" EditMode="Dialog" ShowSubmitDiv="False">
                    <JQTools:JQDataForm ID="dataFormMaster8" runat="server" AlwaysReadOnly="False" ChainDataFormID="" Closed="False" ContinueAdd="False" DataMember="HUT_User" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="7" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sHUTUser.HUT_User" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="中文姓名" Editor="text" FieldName="NameC" MaxLength="20" Span="1" Width="70" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="英文姓名" Editor="text" FieldName="NameE" MaxLength="128" Span="1" Width="90" ReadOnly="True" NewRow="False" RowSpan="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="原始面談顧問" Editor="text" FieldName="HunterName" MaxLength="0" ReadOnly="True" Span="1" Width="110" NewRow="False" RowSpan="1" Visible="True" EditorOptions="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="履歷來源" Editor="text" FieldName="UserSourse" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <fieldset>
                        <legend>推薦紀錄</legend>


                        <JQTools:JQDataGrid ID="DGJobAssignLogs" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_JobAssignLogs" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogAssignLogs" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnUpdate="" PageList="5,10,15,20" PageSize="5" Pagination="True" ParentObjectID="dataFormMaster8" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sHUTUser.HUT_User" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="100%" OnDeleted="OnDeletedAssignLogs">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱-職缺" Editor="infocombobox" FieldName="JobID" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="210" EditorOptions="valueField:'JobID',textField:'sJobName',remoteName:'sHUTUser.HUT_Job',tableName:'HUT_Job',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="狀態" Editor="infocombobox" FieldName="AssignID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'sHUTUser.HUT_ZAssignStep',tableName:'HUT_ZAssignStep',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="日期" Editor="datebox" EditorOptions="" FieldName="AssignTime" Format="yyyy/mm/dd" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="面試時間" Editor="text" FieldName="InterviewTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="推薦顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="推薦評估" Editor="text" FieldName="AssignContent" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption=" " Editor="text" FieldName="RecommendLink" FormatScript="RecommendLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="95">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="上傳推薦報告" Editor="text" FieldName="FileLink" FormatScript="FileLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="下載推薦報告" Editor="infofileupload" EditorOptions="" FieldName="AssignFile" Format="download,folder:Files/Hunter/Assign" MaxLength="150"  ReadOnly="False" Width="110" Frozen="False" IsNvarChar="False" QueryCondition="" Sortable="False" Visible="True" />
                                <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateby" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="JobName" Editor="text" FieldName="JobName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                            </RelationColumns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增推薦紀錄" />
                                <JQTools:JQToolItem Icon="icon-copy" ItemType="easyui-linkbutton" OnClick="OpenCopyAssign" Text="複製推薦紀錄" />
                            </TooItems>
                        </JQTools:JQDataGrid>
                        <JQTools:JQDialog ID="JQDialogAssignLogs" runat="server" BindingObjectID="DFJobAssignLogs" DialogLeft="120px" DialogTop="45px" Title="推薦紀錄維護" Width="860px" ShowSubmitDiv="True">
                            <JQTools:JQDataForm ID="DFJobAssignLogs" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_JobAssignLogs" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster8" RemoteName="sHUTUser.HUT_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApplied="OnAppliedAssignLogs">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶-職缺" Editor="inforefval" EditorOptions="title:'請選擇',panelWidth:445,remoteName:'sHUTUser.HUT_Job',tableName:'HUT_Job',columns:[{field:'CustName',title:'客戶簡稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'JobName',title:'職缺名稱',width:280,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'HunterID',value:'HunterID'}],whereItems:[],valueField:'JobID',textField:'sJobName',valueFieldCaption:'JobID',textFieldCaption:'客戶-職缺',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="290" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="日期" Editor="datebox" EditorOptions="" FieldName="AssignTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="面試時間" Editor="text" FieldName="InterviewTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="狀態" Editor="infocombobox" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'sHUTUser.HUT_ZAssignStep',tableName:'HUT_ZAssignStep',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnSelectAssignID,panelHeight:200" FieldName="AssignID" Format="" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="原因" Editor="infocombobox" EditorOptions="valueField:'AssignReason',textField:'AssignReason',remoteName:'sHUTUser.infoHUT_ZAssignStepReason',tableName:'infoHUT_ZAssignStepReason',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssignReason" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="推薦顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:180" FieldName="HunterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="130" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="上傳報告" Editor="infofileupload" EditorOptions="filter:'docx|doc|xlsx|xls|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|image|tif|txt',isAutoNum:true,upLoadFolder:'Files/Hunter/Assign',showButton:true,showLocalFile:true,fileSizeLimited:'2000'" FieldName="AssignFile" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="False" Width="200" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="推薦評估" Editor="textarea" FieldName="AssignContent" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="670" EditorOptions="height:200" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="實際營業額" Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="iTurnoverReal" MaxLength="0" NewRow="False" OnBlur="OnBluriTurnoverReal" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" EditorOptions="precision:2,groupSeparator:',',prefix:''" FieldName="ratioReal" MaxLength="0" NewRow="False" OnBlur="OnBluriTurnoverReal" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="38" />
                                    <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="AmountReal" MaxLength="0" NewRow="False" OnBlur="" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="60" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
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
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="JobID" RemoteMethod="True" ValidateMessage="請選擇客戶-職缺！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="AssignID" RemoteMethod="True" ValidateMessage="請選擇狀態！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="HunterID" RemoteMethod="True" ValidateMessage="請選擇顧問！" ValidateType="None" />
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

                        <JQTools:JQDialog ID="Dialog_ContactRecord" runat="server" BindingObjectID="dataFormMaster4" Title="面談紀錄" DialogLeft="60px" DialogTop="30px" Width="950px" Wrap="False" EditMode="Dialog" ShowSubmitDiv="False" Height="450px">
                            <JQTools:JQDataForm ID="dataFormMaster4" runat="server" AlwaysReadOnly="False" ChainDataFormID="" Closed="False" ContinueAdd="False" DataMember="HUT_User" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="5" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="True" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="" RemoteName="sHUTUser.HUT_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="中文姓名" Editor="text" FieldName="NameC" MaxLength="20" Span="1" Width="70" ReadOnly="True" NewRow="False" Visible="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="英文姓名" Editor="text" FieldName="NameE" MaxLength="128" Span="1" Width="90" ReadOnly="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="年齡" Editor="text" FieldName="iAge" MaxLength="0" ReadOnly="True" Span="1" Width="50" NewRow="False" RowSpan="1" Visible="True" />
                                </Columns>
                            </JQTools:JQDataForm>
                            <JQTools:JQDataGrid ID="DGContactRecord" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_UserContactRecord" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogContactRecord" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnDeleted="OnDeletedContactRecord" OnUpdate="" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster4" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sHUTUser.HUT_User" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="100%">
                                <Columns>
                                    <JQTools:JQGridColumn Alignment="center" Caption="面談日期" Editor="datebox" FieldName="ContactDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="面談顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="面談內容" Editor="text" FieldName="Notes" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="480">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="修改時間" Editor="text" FieldName="UpdateDate" Format="yyyy/mm/dd HH:MM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="92">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="text" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="52">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                                </RelationColumns>
                                <TooItems>
                                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增面談紀錄" />
                                </TooItems>
                            </JQTools:JQDataGrid>
                            <JQTools:JQDialog ID="JQDialogContactRecord" runat="server" BindingObjectID="DFContactRecord" DialogLeft="120px" DialogTop="70px" Title="面談紀錄維護" Width="850px">
                                <JQTools:JQDataForm ID="DFContactRecord" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_UserContactRecord" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster4" RemoteName="sHUTUser.HUT_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApplied="OnInsertedContactRecord">
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="面談日期" Editor="datebox" FieldName="ContactDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="面談顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="110" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="面談內容" Editor="textarea" EditorOptions="height:250" FieldName="Notes" MaxLength="3000" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="720" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="UpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateDate" Editor="text" FieldName="UpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    </Columns>
                                    <RelationColumns>
                                        <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                                    </RelationColumns>
                                </JQTools:JQDataForm>
                                <JQTools:JQDefault ID="JQDefault2" runat="server" BindingObjectID="DFContactRecord" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ContactDate" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="UpdateBy" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="UpdateDate" RemoteMethod="True" />
                                    </Columns>
                                </JQTools:JQDefault>
                                <JQTools:JQValidate ID="JQValidate2" runat="server" BindingObjectID="DFContactRecord" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactDate" RemoteMethod="True" ValidateMessage="請選擇面談日期！" ValidateType="None" />
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Notes" RemoteMethod="True" ValidateMessage="面談內容不可空白！" ValidateType="None" />
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="HunterID" RemoteMethod="True" ValidateMessage="請選擇面談顧問！" ValidateType="None" />
                                    </Columns>
                                </JQTools:JQValidate>
                                <JQTools:JQAutoSeq ID="JQAutoSeq2" runat="server" BindingObjectID="DFContactRecord" FieldName="AutoKey" NumDig="1" />
                            </JQTools:JQDialog>
                        </JQTools:JQDialog>
            <JQTools:JQImageContainer ID="JQImageContainer1" runat="server" AutoSize="False" Height="250px" Width="350px">
            </JQTools:JQImageContainer>
        </div>
    </form>
</body>
</html>
