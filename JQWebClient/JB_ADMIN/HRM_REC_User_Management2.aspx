<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_REC_User_Management2.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
      <style type="text/css">

        /*.validate-label:before {
            color: red;
            content: "*";
        }*/

    </style>

      <script type="text/javascript">
          $(function () {
              $("#NameC_Query").attr("placeholder", "輸入姓名/身份證/履歷編號");
              $("input, select, textarea").focus(function () {
                  $(this).css("background-color", "lightyellow");
              });
              $("input, select, textarea").blur(function () {
                  $(this).css("background-color", "white");
              });
          });

          $(document).ready(function () {

              //-------------前台公告職缺的人才連結導入---------------------------------------
              setTimeout(function () {
                  var parameter = Request.getQueryStringByName("UserID");
                  if (parameter != "") {
                      $("#NameC_Query").val(parameter);
                      UserQuery();
                  }
              }, 2000);


              /////查詢條件=>(求職人員=>預設招募代號)
              //var UserID = getClientInfo("UserID");
              //setTimeout(function () {
              //    var data = $("#ServiceConsultants_Query").combobox('getData');
              //    for (var i = 0; i < data.length; i++) {
              //        if (data[i].EmpID == UserID) {
              //            $("#ServiceConsultants_Query").combobox('setValue', data[i].ID);
              //        }
              //    }
              //}, 200);

              ////--------------查詢條件組合-----------------------------------------------------------------
              //---年齡範圍
              var Age1 = $('#Age1_Query').closest('td');
              var Age2 = $('#Age2_Query').closest('td').children();
              Age1.append("&nbsp;&nbsp;～&nbsp;&nbsp;").append(Age2);
              //---修改日期
              var SDate = $('#SDate_Query').closest('td');
              var EDate = $('#EDate_Query').closest('td').children();
              SDate.append("&nbsp;&nbsp;～&nbsp;&nbsp;").append(EDate);
              //---填表日期
              var SCDate = $('#SCDate_Query').closest('td');
              var ECDate = $('#ECDate_Query').closest('td').children();
              SCDate.append("&nbsp;&nbsp;～&nbsp;&nbsp;").append(ECDate);

              var FullSearch = $('#sFullSearch_Query').closest('td');
              FullSearch.append("&nbsp;(★請輸入關鍵字:以 , 區隔)");//.append(AndOr);


              var spi = "&nbsp;&nbsp;";
              ////--------------中文姓名+國籍+有效性....字串結合-----------------------------------------------------------------
              var NameC = $('#dataFormMasterNameC').closest('td');
              var Country = $('#dataFormMasterCountry').closest('td').children();
              var RecIsActive = $('#dataFormMasterRecIsActive').closest('td').children();

              NameC.append(spi + "國籍").append(Country).append(spi + "有效").append(RecIsActive);


              ////--------------縣市+鄉鎮區....字串結合-----------------------------------------------------------------
              var Country = $('#dataFormMasterAddr_Country').closest('td');
              var Addr_City = $('#dataFormMasterAddr_City').closest('td').children();
              var Addr_Desc = $('#dataFormMasterAddr_Desc').closest('td').children();
              Country.append("").append(Addr_City).append("").append(Addr_Desc);


              ////--------------經歷....字串結合-----------------------------------------------------------------
              var JobPeriodS = $('#DFUserCareerJobPeriodS').closest('td');
              var JobPeriodE = $('#DFUserCareerJobPeriodE').closest('td').children();
              JobPeriodS.append("～").append(JobPeriodE);

              ////--------------居留有效日起訖...-----------------------------------------------------------------
              var ResidenceSDate = $('#dataFormMaster2ResidenceSDate').closest('td');
              var ResidenceEDate = $('#dataFormMaster2ResidenceEDate').closest('td').children();
              ResidenceSDate.append("～").append(ResidenceEDate);

              //////--------------身高、體重、婚姻狀況...-----------------------------------------------------------------
              var Tall = $('#dataFormMaster2Tall').closest('td');
              var Weight = $('#dataFormMaster2Weight').closest('td').children();
              var Marriage = $('#dataFormMaster2Marriage').closest('td').children();
              var HouseOwnStatus = $('#dataFormMaster2HouseOwnStatus').closest('td').children();
              var MilitaryServiceIDs = $('#dataFormMaster2MilitaryServiceIDs').closest('td').children();
              var MilitaryReason = $('#dataFormMaster2MilitaryReason').closest('td').children();
              var MilitaryYM = $('#dataFormMaster2MilitaryYM').closest('td').children();
              Tall.append("&nbsp;&nbsp;體重").append(Weight).append("&nbsp;&nbsp;婚姻狀況").append(Marriage).append("&nbsp;&nbsp;現居地").append(HouseOwnStatus).append("&nbsp;&nbsp;兵役狀況").append(MilitaryServiceIDs).append("&nbsp;&nbsp;免役原因").append(MilitaryReason).append("&nbsp;&nbsp;退伍年月").append(MilitaryYM);

              ////--------------家庭成員、同居成員...-----------------------------------------------------------------
              var FamilyCount = $('#dataFormMaster2FamilyCount').closest('td');
              FamilyCount.append("人");
              var CohabitCount = $('#dataFormMaster2CohabitCount').closest('td');
              CohabitCount.append("人");

              //出生日期 
              var Birthday = $('#dataFormMaster2Birthday').closest('td');
              Birthday.append("(1988/08/08)");

              //////--------------緊急聯絡人...-----------------------------------------------------------------
              var CONTACT_NAME = $('#dataFormMaster2CONTACT_NAME').closest('td');
              var CONTACT_PHONE = $('#dataFormMaster2CONTACT_PHONE').closest('td').children();
              var CONTACT_RELATION_ID = $('#dataFormMaster2CONTACT_RELATION_ID').closest('td').children();
              var Same = $('#dataFormMaster2Same').closest('td').children();
              var DomicileCountry = $('#dataFormMaster2Domicile_Addr_Country').closest('td').children();
              var DomicileCity = $('#dataFormMaster2Domicile_Addr_City').closest('td').children();
              var DomicileDesc = $('#dataFormMaster2Domicile_Addr_Desc').closest('td').children();

              CONTACT_NAME.append("&nbsp;&nbsp;<span style='color: rgb(138, 43, 226);'>聯絡人電話</span>").append(CONTACT_PHONE).append("&nbsp;&nbsp;<span style='color: rgb(138, 43, 226);'>聯絡人關係</span>").append(CONTACT_RELATION_ID)
              .append("&nbsp;&nbsp;<span style='color: rgb(138, 43, 226);'>戶籍地址</span>").append(Same).append("同現居地&nbsp;&nbsp;&nbsp;").append(DomicileCountry).append("").append(DomicileCity).append("").append(DomicileDesc);

              //-------------其它地點---------------------------
              //var DutyAreasOther = $('#dataFormMaster3DutyAreasOther').closest('td');
              //DutyAreasOther.append("<br><br>&nbsp;&nbsp;<span style='color: rgb(138, 43, 226);'>★紫色為招募系統派任必需欄位</span>");
              $('<span id="t1" style="color: rgb(138, 43, 226); background-color:#FFFF99;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(★紫色為招募系統"派任"必需欄位，轉入前建議填寫)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>').insertAfter($('#dataFormMaster3DutyAreasOther'));

              //////--------------招募區域+匯款帳號...-----------------------------------------------------------------
              var AccountNo = $('#dataFormMaster2AccountNo').closest('td');
              var AccountName = $('#dataFormMaster2AccountName').closest('td').children();
              var AccountID = $('#dataFormMaster2AccountID').closest('td').children();
              var BankID = $('#dataFormMaster2BankID').closest('td').children();
              var BankBranchID = $('#dataFormMaster2BankBranchID').closest('td').children();
              AccountNo.append("&nbsp;&nbsp;匯款戶名").append(AccountName).append("&nbsp;&nbsp;身份證號").append(AccountID).append("&nbsp;&nbsp;行庫名稱").append(BankID).append("&nbsp;&nbsp;分行名稱").append(BankBranchID);

              //顯示筆數預設
              var i = 2000;
              $("#ShowCount_Query").numberbox('setValue', i);//四捨五入      
              $("#ShowCount_Query").val(i);


              //Grid選取單選,checkbox多選
              $("#dataGridView").datagrid({
                  singleSelect: true,
                  selectOnCheck: false,
                  checkOnSelect: false
              });

              //加上(紫色)的欄位
              var HideFieldName = ['2PID', '2ResidentID', '2Birthday', 'CurTelNO', '2CONTACT_NAME'];
              var FormName = '#dataFormMaster';

              $.each(HideFieldName, function (index, fieldName) {
                  var Name = $(FormName + fieldName);
                  //$('<br/><span id="t1" style="color: rgb(138, 43, 226);">*不可刊登</span>').insertAfter(Name);
                  $(FormName + fieldName).closest('td').prev('td').css("color", "rgb(138, 43, 226)");//改變td前面文字顏色
              });

              //ShowToDoCount();//今日新增筆數呈現
              GoToShowList();//今日新增筆數呈現

              //同步戶籍地址
              $("#dataFormMaster2Same").click(function () {
                  if ($(this).is(":checked") == true) {  //讀取是選中還是非選中，返回true、false       
                      var addr_City = $("#dataFormMasterAddr_Country").combobox('getValue');
                      $('#dataFormMaster2Domicile_Addr_Country').combobox('setValue', addr_City);
                      setTimeout(function () {
                          $("#dataFormMasterAddr_City").combobox('setWhere', "Country = '" + addr_City + "'");
                          $("#dataFormMasterAddr_City").combobox('enable');

                          $('#dataFormMaster2Domicile_Addr_City').combobox('setValue', $("#dataFormMasterAddr_City").combobox('getValue'));
                          $('#dataFormMaster2Domicile_Addr_Desc').val($("#dataFormMasterAddr_Desc").val());
                      }, 200);
                  }
              });

              //派任作業 => 發信內文加上註解
              var bAssignMail = $('#DFJobAssignLogsbAssignMail').closest('td');
              bAssignMail.append("&nbsp;&nbsp;<span style='color: rgb(138, 43, 226);'>發信內文已包含( 抬頭 </span>").append("<span style='color: red;'>Hello，###您好：</span>").append("<span style='color: rgb(138, 43, 226);'>，結尾 </span>").append("<span style='color: red;'>系統的連結 與 發送時間</span>").append("<span style='color: rgb(138, 43, 226);'> )</span>");

              //派任作業 => 發信內文加上註解(批次)
              var bAssignMailMore = $('#DFJobAssignLogsMorebAssignMail').closest('td');
              bAssignMailMore.append("&nbsp;&nbsp;<span style='color: rgb(138, 43, 226);'>發信內文已包含( 抬頭 </span>").append("<span style='color: red;'>Hello，###您好：</span>").append("<span style='color: rgb(138, 43, 226);'>，結尾 </span>").append("<span style='color: red;'>系統的連結 與 發送時間</span>").append("<span style='color: rgb(138, 43, 226);'> )</span>");

              
              //健保眷屬
              //眷屬生日 + 所得稅扶養親屬否	
              var FamilyBirthday = $('#DFUserFamilyUserFamilyBirthday').closest('td');
              var FamilyIsSupport = $('#DFUserFamilyIsSupport').closest('td').children();

              FamilyBirthday.append("&nbsp;&nbsp;<span style='color: rgb(138, 43, 226);'>(1988/08/08)</span>").append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;所得稅扶養親屬否").append(FamilyIsSupport);;



          });

          //check 出生日期,需大於15歲
          function checkBirthday(val) {
              var dt = new Date();
              var aDate = $.jbjob.Date.DateFormat(dt, 'yyyy');
              var dt2 = new Date(val);
              var bDate = $.jbjob.Date.DateFormat(dt2, 'yyyy');
              if (aDate - bDate > 15) return true;
              else return false;

          }
          //-------------------CheckBox顯示---------------------------------------
          function genCheckBox(val) {
              if (val)
                  return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
              else
                  return "<input  type='checkbox'  onclick='return false;'  />";
          }
          //=============================================今日新增筆數呈現=========================================================================================
          function GoToShowList() {
              var sCountInfo;
              $.ajax({
                  type: "POST",
                  url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_User_Management2.REC_User', //連接的Server端，command
                  data: "mode=method&method=" + "ReturnREC_UserAddNowCount", //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                  cache: false,
                  async: false,
                  success: function (data) {
                      if (data != false) {
                          sCountInfo = data;
                      }
                  },
                  error: function (xhr, ajaxOptions, thrownError) {
                      alert(xhr.status);
                      alert(thrownError);
                  }
              });
              //呈現
              if (sCountInfo != undefined) {
                  $('#LinkShowList').html(sCountInfo);//.css("background-color", "pink");
              }
              else {
                  $('#LinkShowList').html('');
              }
              //if (sCountInfo != undefined && sCountInfo != "") {
              //    //$('#dataGridView').datagrid('getPanel').panel('setTitle', '★今日填寫履歷人數：' + sCountInfo);
              //    $("#Label1").text(sCountInfo);

                  //var light = "<span style='font-family:Microsoft JhengHei;font-weight: bold;'>";
                  //var right = "</span>";

                  //if (sCountInfo != "") {
                  //    sMsg = "★新開職缺資訊 ";
                  //    if (iData != 0) {
                  //        sDataMore = "<a href='javascript: void(0)' onclick='LinkJobTab(1);' style='color:red;text-decoration: underline'>  (共  " + iData + " 筆 ) </a>";
                  //        sMsg = sMsg + sDataMore;
                  //    }
                  //    sMsg = sMsg + "<br>" + sData;
                  //    sMsg = light + sMsg + right;
                  //}

              //}
          }
          function ShowCountList() {
              //alert('YA');
          }
          //function ShowToDoCount() {
          //    var sCountInfo;
          //    $.ajax({
          //        type: "POST",
          //        url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_User_Management2.REC_User', //連接的Server端，command
          //        data: "mode=method&method=" + "ReturnREC_UserAddNowCount", //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
          //        cache: false,
          //        async: false,
          //        success: function (data) {
          //            if (data != false) {
          //                sCountInfo = data;
          //            }
          //        },
          //        error: function (xhr, ajaxOptions, thrownError) {
          //            alert(xhr.status);
          //            alert(thrownError);
          //        }
          //    });
          //    //呈現
          //    if (sCountInfo != undefined && sCountInfo != "") {
          //        //$('#dataGridView').datagrid('getPanel').panel('setTitle', '★今日填寫履歷人數：' + sCountInfo);
          //        $("#Label1").text(sCountInfo);

          //    }
          //}

          function EduID1Select(val) {
              $('#dataFormMasterEduName1').val($('#dataFormMasterEduID1').combobox('getText'));
          }
          function EduID2Select(val) {
              $('#dataFormMasterEduName2').val($('#dataFormMasterEduID2').combobox('getText'));
          }
          //縣市連動
          function Addr_Country_OnSelect() {
              var addr_City = $("#dataFormMasterAddr_Country").combobox('getValue');
              $("#dataFormMasterAddr_City").combobox('setWhere', "Country = '" + addr_City + "'");
              $("#dataFormMasterAddr_City").combobox('enable');
          }
          //鄉鎮區連動
          function Addr_City_OnSelect(rowdata) {
              var country = $("#dataFormMasterAddr_Country").combobox('getValue');
              var city = $("#dataFormMasterAddr_City").combobox('getValue');
              $("#dataFormMasterAddr_Desc").val(country + city);
          }
          //戶籍縣市連動
          function Domicile_Addr_Country_OnSelect() {
              var addr_City = $("#dataFormMaster2Domicile_Addr_Country").combobox('getValue');
              $("#dataFormMaster2Domicile_Addr_City").combobox('setWhere', "Country = '" + addr_City + "'");
              $("#dataFormMaster2Domicile_Addr_City").combobox('enable');
          }
          //戶籍籍鄉鎮區連動
          function Domicile_Addr_City_OnSelect(rowdata) {
              var country = $("#dataFormMaster2Domicile_Addr_Country").combobox('getValue');
              var city = $("#dataFormMaster2Domicile_Addr_City").combobox('getValue');
              $("#dataFormMaster2Domicile_Addr_Desc").val(country + city);
          }
          //區域,招募連動
          function SalesTeamOnSelect() {
              $('#dataFormMaster2ServiceConsultants').combobox('setValue', "");
              var ServiceSalesTeam = $("#dataFormMaster2ServiceSalesTeam").combobox('getValue');
              $("#dataFormMaster2ServiceConsultants").combobox('setWhere', "SalesTeamID = '" + ServiceSalesTeam + "'");
          }
          //銀行,分行連動
          function BANK_IDOnSelect() {
              $('#dataFormMaster2BankBranchID').combobox('setValue', "");
              var BankID = $("#dataFormMaster2BankID").combobox('getValue');
              $("#dataFormMaster2BankBranchID").combobox('setWhere', "BANK_ID = '" + BankID + "'");
          }
          //區域,招募連動-查詢
          function SalesTeamOnSelectQ() {
              var ServiceSalesTeam = $("#ServiceSalesTeam_Query").combobox('getValue');
              $("#ServiceConsultants_Query").combobox('setWhere', "SalesTeamID = '" + ServiceSalesTeam + "'");
          }
          //工作縣市,工作地點連動
          var OnWhereClassID ;
          function OnSelectDutyAreaClass(rowdata) {
              var DutyAreasIDs = $("#dataFormMaster3DutyAreasIDs").options('getValue');
              //var DutyAreaClassID = $("#dataFormMaster3DutyAreaClassIDs").options('getValue');
              if (rowdata != "") {
                  OnWhereClassID = " ClassID in (" + rowdata + ")";
              }
              $('#dataFormMaster3DutyAreasIDs').options('initializePanel');
              $("#dataFormMaster3DutyAreasIDs").options('setValue', DutyAreasIDs);

          }
          function OnWhereAreaClassID(param) {
              return OnWhereClassID;
          }

          function DFLoadSuccess() {
              Addr_Country_OnSelect();

              var DutyAreasIDs = $("#dataFormMaster3DutyAreasIDs").options('getValue');
              var DutyAreaClassID = $("#dataFormMaster3DutyAreaClassIDs").options('getValue');
              if (DutyAreaClassID != "") {
                  OnWhereClassID = " ClassID in (" + DutyAreaClassID + ")";
              }
              $('#dataFormMaster3DutyAreasIDs').options('initializePanel');
              $("#dataFormMaster3DutyAreasIDs").options('setValue', DutyAreasIDs);
          }

          function queryGrid(dg) {//查詢後添加固定條件
              if ($(dg).attr('id') == 'dataGridView') {
                  UserQuery();
              }
              //應徵紀錄查詢
              if ($(dg).attr('id') == 'DGApplyJobLogs') {
                  ApplyJobQuery();
              }
          }
          
          function DFOnApplied() {
              //修改文字
              UpdateRecReference();

              UserQuery();
          }
          // 修改多選選項對應的文字
          function UpdateRecReference() {
              var row = $('#dataGridView').datagrid('getSelected');
              var UserID = $("#dataFormMasterUserID").val();//履歷編號            
              $.ajax({
                  type: "POST",
                  url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_User_Management2.REC_User', //連接的Server端，command
                  data: "mode=method&method=" + "UpdateRecReference" + "&parameters=" + encodeURIComponent(UserID), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                  cache: false,
                  async: false,
                  success: function (data) {
                  },
                  error: function (xhr, ajaxOptions, thrownError) {
                      alert(xhr.status);
                      alert(thrownError);
                  }
              });

          }
          function Formatblacklist(val, rowData) {
              if (rowData.blacklist == true ) {//黑名單=>灰色
                  return "<div style='background-color:#D3D3D3' title='黑名單'> " + val + "</div>";
              } else if (rowData.RecIsActive == false) {//無效=>刪除線效果
                  return "<span style='text-decoration:line-through' title='無效履歷'> " + val + "</span>";
              }
              else {
                  return val;
              }
          }
          function UserQuery() {
              var NameC = $('#NameC_Query').val();//人才姓名/身份證/履歷編號
              var Gender = $('#Gender_Query').combobox('getValue');//性別
              var Age1 = $('#Age1_Query').val();//年齡範圍
              var Age2 = $('#Age2_Query').val();
              var EduID = $('#EduID_Query').combobox('getValue');//最高學歷
              var DutyAreas = $('#DutyAreas_Query').val();//工作地點
              var ProLicenses = $('#ProLicenses_Query').val();//證照資格
              var JobCompany = $('#JobCompany1_Query').val();//公司名稱
              var CurAddress = $('#CurAddress_Query').val();//現居地址
              var AssignJob = $('#AssignJob_Query').val();//派任職缺
              var SalesTeam = $('#ServiceSalesTeam_Query').combobox('getValue');//求職地區	
              var ServiceConsultants = $('#ServiceConsultants_Query').combobox('getValue');//求職人員	
              var Status = $('#sStatus_Query').combobox('getValue');//處理狀態
              var SDate = $("#SDate_Query").datebox("getValue");//修改日期
              var EDate = $("#EDate_Query").datebox("getValue");//修改日期
              var SCDate = $("#SCDate_Query").datebox("getValue");//建立日期
              var ECDate = $("#ECDate_Query").datebox("getValue");

              var PFMemberID = $('#PFMemberID_Query').combobox('getValue');//推薦樁腳
              var IsPilefoot = $('#IsPilefoot_Query').combobox('getValue');//是否樁腳

              var tString = $('#sFullSearch_Query').val();//搜尋字串
              var ShowCount = $('#ShowCount_Query').val();//顯示筆數
              
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
                  url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_User_Management2.REC_User', //連接的Server端，command
                  data: "mode=method&method=" + "RECUsersQuery" + "&parameters=" + encodeURIComponent(NameC + "*" + Gender + "*" + Age1 + "*" + Age2 + "*" + EduID + "*" + DutyAreas + "*" + CurAddress + "*" + AssignJob + "*" + ProLicenses + "*" + JobCompany + "*" + SalesTeam + "*" + ServiceConsultants + "*" + Status + "*" + SDate + "*" + EDate + "*" + SCDate + "*" + ECDate + "*" + PFMemberID + "*" + IsPilefoot + "*" + ShowCount + "*" + tString + "*" + Mark), //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
                          //$('#dataGridView').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                          $('#dataGridView').datagrid({ loadFilter: pagerFilter, url: "", pageNumber: 1 }).datagrid('loadData', rows);//第一頁
                      }
                  }
              });

              //ShowToDoCount();//今日新增筆數呈現

          }

          //var waitA = false;
          function GVOnloadSuccess(param) {
              //if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
              //    //1=0做完
              //    waitA = true;

              //    //明細Grid選取單選,checkbox多選
              //    $(this).datagrid({
              //        singleSelect: true,
              //        selectOnCheck: false,
              //        checkOnSelect: false
              //    });
              //} 
          }

          //-------------------------------------------------------------------刪除作業-------------------------------------------------------------------
          //取得登入者是否是主管
          function GetIsManager() {
              var UserID = getClientInfo("UserID");
              var IsManager;
              $.ajax({
                  type: "POST",
                  url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_User_Management2.REC_User', //連接的Server端，command
                  data: "mode=method&method=" + "ReturnIsManager" + "&parameters=" + UserID,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                  cache: false,
                  async: false,
                  success: function (data) {
                      IsManager = data
                  },
              });
              return IsManager;
          }
          //求得招募系統人才的派任紀錄筆術(若有派任紀錄=>不可刪除)
          function ReturnAssignLogsCount(iType, AutoKey) {
              var cnt;
              var UserID = $('#dataFormMaster8UserID').val();//人才
              $.ajax({
                  type: "POST",
                  url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_User_Management2.REC_User',
                  data: "mode=method&method=" + "ReturnAssignLogsCount" + "&parameters=" + UserID ,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                  cache: false,
                  async: false,
                  success: function (data) {
                      var rows = $.parseJSON(data);
                      if (rows.length > 0) {
                              cnt = rows[0].cnt;

                      }
                  },
              });
              return cnt;
          }
          //控制是否可以刪除=>主管可刪除
          function OnDeleteRECUser(rowData) {
              //if (GetIsManager() == 0) {//不是主管
              //    if (rowData.ContactDate != null || ReturnAssignLogsCount() != "0") {
              //        alert('有面談紀錄存在/招募系統有派任紀錄，不可刪除！');
              //        return false; //取消編輯的動作 
              //    }
              //}
              if (GetIsManager() == 0) {//不是主管
                  alert('無刪除權限！');
                  return false; //取消刪除的動作 
              }
              if (rowData.ContactDate != null || ReturnAssignLogsCount() != "0") {
                  alert('有面談紀錄存在/招募系統有派任紀錄，不可刪除！');
                  return false; //取消刪除的動作 
              }
              var pre = confirm("確定刪除此筆紀錄?");
              if (pre == true) {
                  //callServerMethod
                  var row = $('#dataGridView').datagrid('getSelected');
                  $.ajax({
                      type: "POST",
                      url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_User_Management2.REC_User',
                      data: "mode=method&method=" + "DeleteREC_UserAbount" + "&parameters=" + rowData.UserID,
                      cache: false,
                      async: true,
                  });
                  $('#dataGridView').datagrid("reload");
                  UserQuery();
                  return false; //取消刪除的動作 
              }
              else {
                  return false;
              }
          }
          function OnDeletedRECUser() {
              UserQuery();
          }
          //--------------------控制多選的選項-------------------------------
          function ControlOPT(opt, optID) {
              var ilength = $('#' + optID).parent().find('[type="checkbox"]').length;
              var i;
              if (opt.substring(0, 1) == "0") {
                  for (i = 1; i < ilength; i++) {
                      $('input[name^=' + optID + '_' + i + '][type=checkbox]').prop("checked", false);
                      $('input[name^=' + optID + '_' + i + '][type=checkbox]').attr("disabled", "disabled");
                  };
              } else {
                  for (i = 1; i < ilength; i++) {
                      $('input[name^=' + optID + '_' + i + '][type=checkbox]').removeAttr("disabled");//disable属性删除
                  };
              }
          }

          //--------------------選取交通工具,選無	時--------------------
          function OnSelectTrafficIDs(opt) {
              var optID = 'dataFormMasterTrafficIDs';
              ControlOPT(opt, optID);
          }
          //--------------------選取無塵衣,選都不接受	時--------------------
          function OnSelectCleanClothes(opt) {
              var optID = 'dataFormMaster3CleanClothesIDs';
              ControlOPT(opt, optID);
          }
          //--------------------選取工作工具,選都不接受	時--------------------
          function OnSelectDutyTools(opt) {
              var optID = 'dataFormMaster3DutyToolsIDs';
              ControlOPT(opt, optID);
          }
          //--------------------選取工作型態,選都不接受	時--------------------
          function OnSelectDutyActTypesIDs(opt) {
              var optID = 'dataFormMaster3DutyActTypesIDs';
              ControlOPT(opt, optID);
          }
          //--------------------選取工作環境,選都不接受	時--------------------
          function OnSelectDutyEnvironmentIDs(opt) {
              var optID = 'dataFormMaster3DutyEnvironmentIDs';
              ControlOPT(opt, optID);
          }
          //--------------------選取加班意願,選無法加班	時--------------------
          function OnSelectOverTimesIDs(opt) {
              var optID = 'dataFormMaster3OverTimesIDs';
              ControlOPT(opt, optID);
          }
          //--------------------選取經濟壓力,選無	時--------------------
          function OnSelectEcoPressureIDs(opt) {
              var optID = 'dataFormMaster2EcoPressureIDs';
              ControlOPT(opt, optID);
          }
          //--------------------選取健康評量,選正常	時--------------------
          function OnSelectHealthStatusIDs(opt) {
              var optID = 'dataFormMaster2HealthStatusIDs';
              ControlOPT(opt, optID);
          }
          //--------------------------工作經驗-----------------------------------
          
          function OnLoadSuccessCareer() {
              //清空選擇=> 在職期間
              if ($('#DFUserCareerJobPeriodS').combobox('getValue') == "") {
                  $('#DFUserCareerJobPeriodS').combobox('setValue', "");
              }
              if ($('#DFUserCareerJobPeriodE').combobox('getValue') == "") {
                  $('#DFUserCareerJobPeriodE').combobox('setValue', "");
              }
          }
          function OnApplyCareer() {
              //檢查起訖在職期間	
              var DutyDate = $('#DFUserCareerJobPeriodS').combobox('getValue');
              var DutyDate2 = $('#DFUserCareerJobPeriodE').combobox('getValue').replace(/\s*/g, "");

              if (DutyDate2 != "" && DutyDate2 < DutyDate) {
                  alert('工作期間區間有誤！');
                  return false;
              }
          }
          function OnAppliedCareer() {
              $("#DGUserCareer").datagrid('reload');
          }
          //預設選了起始期間,自動帶入結束時間
          function OnSelectDutyDate(rowData) {
              if ($('#DFUserCareerJobPeriodE').combobox('getValue') == "") {
                  $("#DFUserCareerJobPeriodE").combobox('setValue', rowData.sDate);
              }
          }
          //檢查字串是否符合在職期間年月
          function CheckStrWildWord(str) {
              if (str.replace(/\s*/g, "") != "") {
                  var r = str.match(/^(\d{4})(\/)(0[1-9]|1[0-2])$/);
                  if (r == null) return false;
                  var d = new Date(r[1], (r[3] - 1), 1);
                  return (d.getFullYear() == r[1] && d.getMonth() == (r[3] - 1) && d.getDate() == 1);
              } else {
                  return true;
              }
          }
          //客戶資料dataForm的縣市連動
          function dataFormMasterAddr_Country_OnSelect() {
              var addr_City = $("#dataFormMasterAddr_Country").combobox('getValue');
              $("#dataFormMasterAddr_City").combobox('setWhere', "Country = '" + addr_City + "'");
              $("#dataFormMasterAddr_City").combobox('enable');
          }
          //--------------------------面談紀錄-----------------------------------

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

          //---------------------------------------面談紀錄權限控制-----------------------------------------------------
          //控制是否可以修改 (當天&&建立者)
          function UserContactUpdateRow(rowData) {
              var username = getClientInfo("username");
              if (rowData.diffday!=0 || rowData.UpdateBy != username) {
                  alert('無編輯權限！');
                  return false; //取消編輯的動作 
              }
          }

          //控制是否可以刪除 (當天&&建立者)
          function UserContactDeleteRow(rowData) {
              var username = getClientInfo("username");
              if (rowData.diffday != 0 || rowData.UpdateBy != username) {
                  alert('無刪除權限！');
                  return false; //取消編輯的動作 
              }
          }
          
          //--------------------------履歷眷屬資料檔-----------------------------------

          //眷屬資料有變更時重整
          function OnInsertedUserFamily() {
              $("#DGREC_UserFamily").datagrid('reload');
              UserQuery();
          }
          function OnDeletedUserFamily() {
              $("#DGREC_UserFamily").datagrid('reload');
              UserQuery();
          }

          //---------------------------------------履歷眷屬資料權限控制-----------------------------------------------------
          //控制是否可以修改 (當天&&建立者)
          function UserFamilyUpdateRow(rowData) {
              var username = getClientInfo("username");
              if (rowData.diffday != 0 || rowData.CreateBy != username) {
                  alert('無編輯權限！');
                  return false; //取消編輯的動作 
              }
          }
          //控制是否可以刪除 (當天&&建立者)
          function UserFamilyDeleteRow(rowData) {
              var username = getClientInfo("username");
              if (rowData.diffday != 0 || rowData.CreateBy != username) {
                  alert('無刪除權限！');
                  return false; //取消編輯的動作 
              }
          }

        //---------------------------------------推薦紀錄權限控制-----------------------------------------------------
        //function OnLoadJobRecommendLogs() {
        //    ////清空選擇 ----履歷來源
        //    if ($('#DFJobRecommendLogsRecID').combobox('getValue') == "") {
        //        //派任作業=>(登入用戶編號=>預設招募代號)
        //        var UserID = getClientInfo("UserID");
        //        setTimeout(function () {
        //            var data = $("#DFJobRecommendLogsRecID").combobox('getData');
        //            for (var i = 0; i < data.length; i++) {
        //                if (data[i].EmpID == UserID) {
        //                    $("#DFJobRecommendLogsRecID").combobox('setValue', data[i].ID);
        //                }
        //            }
        //        }, 200);

        //    }

        //}
        ////聯繫維護紀錄有變更時重整
        //function OnInsertedRecommendRecord() {
        //    $("#DGJobRecommendLogs").datagrid('reload');
        //    UserQuery();
        //}

        //function OnDeletedRecommendRecord() {
        //    $("#DGJobRecommendLogs").datagrid('reload');
        //    UserQuery();
        //}

        ////控制是否可以修改 (當天&&建立者)
        //function UserRecommendUpdateRow(rowData) {
        //    var username = getClientInfo("username");
        //    if (rowData.diffday != 0 || rowData.LastUpdateBy != username) {
        //        alert('無編輯權限！');
        //        return false; //取消編輯的動作 
        //    }
        //}

        ////控制是否可以刪除 (當天&&建立者)
        //function UserRecommendDeleteRow(rowData) {
        //    var username = getClientInfo("username");
        //    if (rowData.diffday != 0 || rowData.LastUpdateBy != username) {
        //        alert('無刪除權限！');
        //        return false; //取消編輯的動作 
        //    }
        //}
        ////
          
          //--------------派任作業(紀錄)-----------------------------------------------------------------------------------------------   
         
          function AssignLink(value, row, index) {
              var svalue="新增"
              if (value != null) {
                  svalue = value;
              }
                  return "<a href='javascript: void(0)' onclick='LinkAssign(" + index + ");' style='color:blue;'>" + svalue + "</a>";
          }
          // open推薦畫面
          function LinkAssign(index) {
              $("#dataGridView").datagrid('selectRow', index);
              openForm('#JQDialogJobAssignLogs', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
          }
          function OnLoadAssignLogs() {
              //新增時=>是否發信預設勾取
              if (getEditMode($("#DFJobAssignLogs")) == 'inserted') {
                  ////清空選擇 ----履歷來源
                  if ($('#DFJobAssignLogsRecID').combobox('getValue') == "") {
                      //派任作業=>(登入用戶編號=>預設招募代號)
                      var UserID = getClientInfo("UserID");
                      setTimeout(function () {
                          var data = $("#DFJobAssignLogsRecID").combobox('getData');
                          for (var i = 0; i < data.length; i++) {
                              if (data[i].EmpID == UserID) {
                                  $("#DFJobAssignLogsRecID").combobox('setValue', data[i].ID);
                              }
                          }
                      }, 200);

                  }

                  $("#DFJobAssignLogsbAssignMail").checkbox('setValue', true);
              }

              //推薦記錄預設            
              var AssignID = $("#DFJobAssignLogsAssignID").combobox('getValue');
              ControlAssign(AssignID);
              
          }
         
          //複製推薦紀錄
          function OpenCopyAssign() {
              //選取的那筆進行複製
              var row = $('#DGJobAssignLogs').datagrid('getSelected');
              openForm('#JQDialogAssignLogs', row, "inserted", 'dialog');
              ControlAssign("");

          }
          //派任記錄狀態控管
          function OnSelectAssignID(rowData) {
              var AssignID = rowData.AssignID;//1推薦中,2待報到,3待加保
              ControlAssign(AssignID);
              
          }
          function ControlAssign(AssignID) {
              $('#DFJobAssignLogsAssignMail').closest('td').prev('td').hide();//發信內文隱藏
              $('#DFJobAssignLogsAssignMail').closest('td').hide();
              $('#DFJobAssignLogsbAssignMail').closest('td').prev('td').hide();//是否發信隱藏
              $('#DFJobAssignLogsbAssignMail').closest('td').hide();
              if (AssignID == "2") {
                  $('#DFJobAssignLogsAssignMail').closest('td').prev('td').show();//推薦評估顯示
                  $('#DFJobAssignLogsAssignMail').closest('td').show();    
                  $('#DFJobAssignLogsbAssignMail').closest('td').prev('td').show();//是否發信顯示
                  $('#DFJobAssignLogsbAssignMail').closest('td').show();
                  if (getEditMode($("#DFJobAssignLogs")) == 'inserted') {
                      $('#DFJobAssignLogsAssignMail').val("  恭喜您錄取了，請登入下方連結填寫報到前所需要的資料與上傳報到時需要的文件。\n  網站中也有報到時需攜帶的文件可以下載使用。\n  若有任何問題請再來電詢問。\n  謝謝。");
                  }
              }
          }
          //求得最新一筆的紀錄(派任狀態、派任日期)
          function ReturnNewJobAssignLogs(iType, AutoKey) {
              var sNew;
              var JobID = $('#DFJobAssignLogsJobID').refval('getValue');//客戶-職缺
              var UserID = $('#dataFormMaster8UserID').val();//人才
              $.ajax({
                  type: "POST",
                  url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_User_Management2.REC_User',
                  data: "mode=method&method=" + "ReturnNewJobAssignLogs" + "&parameters=" + JobID + "," + UserID + "," + AutoKey,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                  cache: false,
                  async: false,
                  success: function (data) {
                      var rows = $.parseJSON(data);
                      if (rows.length > 0) {
                          if (iType == 2) {
                              sNew = rows[0].AssignID;//派任狀態
                          } else if (iType == 3) {
                              sNew = rows[0].AssignTime;//派任日期
                          }

                      }
                  },
              });
              return sNew;
          }
          function OnApplyAssignLogs() {
              //派任狀態為待報到時檢查
              if ($("#DFJobAssignLogsAssignID").combobox('getValue')=="2" && $("#DFJobAssignLogsbAssignMail").checkbox('getValue') == true) {
                  if ($("#DFJobAssignLogsAssignMail").val().trim() == "") {
                      alert('請填寫發信內文！');
                      return false;
                  }
                  if ($("#dataFormMaster8Email").val().trim() == "") {
                      alert('郵件信箱為空值，無法寄送！');
                      return false;
                  }
              }

              //檢查檢查客戶-職缺必選
              if ($("#DFJobAssignLogsAssignID").combobox('getValue') == "1") {//推薦
                  if ($('#DFJobAssignLogsRecommendCust').val() == "" && $('#DFJobAssignLogsJobID').refval('getValue') == "") {
                      alert('請選擇客戶-職缺或填寫推薦職缺！');
                      return false;
                  }
              } else {
                  if ($('#DFJobAssignLogsJobID').refval('getValue') == "") {
                      alert('請選擇客戶-職缺！');
                      return false;
                  }
              }

              //新增時檢查
              if (getEditMode($("#DFJobAssignLogs")) == 'inserted' && $("#DFJobAssignLogsAssignID").combobox('getValue')=="2") {

                  //檢查是否最新一筆紀錄的派任狀態
                  if (ReturnNewJobAssignLogs(2,"1900/01/01") == $("#DFJobAssignLogsAssignID").combobox('getValue')) {
                      alert('已有' + $("#DFJobAssignLogsAssignID").combobox('getText') + '紀錄！');
                      return false;
                  }
                  //檢查是否最新一筆紀錄的派任日期
                  if (ReturnNewJobAssignLogs(3,"1900/01/01") >= $("#DFJobAssignLogsAssignTime").datebox('getValue')) {
                      alert('派任日期有誤！');
                      return false;
                  }
              }

              //編輯時檢查
              if (getEditMode($("#DFJobAssignLogs")) == 'updated' && $("#DFJobAssignLogsAssignID").combobox('getValue') == "2") {

                  var row = $('#DGJobAssignLogs').datagrid('getSelected');
                  
                  //檢查是否最新一筆紀錄的派任狀態
                  if (ReturnNewJobAssignLogs(2, row.AssignTime) == $("#DFJobAssignLogsAssignID").combobox('getValue')) {
                      alert('已有' + $("#DFJobAssignLogsAssignID").combobox('getText') + '紀錄！');
                      return false;
                  }
                  //檢查是否最新一筆紀錄的派任日期
                  if (ReturnNewJobAssignLogs(3, row.AssignTime) >= $("#DFJobAssignLogsAssignTime").datebox('getValue')) {
                      alert('派任日期有誤！');
                      return false;
                  }
              }

          }

          function OnAppliedAssignLogs() {
              $('#DGJobAssignLogs').datagrid("reload");
              UserQuery();

              //發報到通知信(新增時)
              if (getEditMode($("#DFJobAssignLogs")) == 'inserted') {
                  if ($("#DFJobAssignLogsAssignID").combobox('getValue') == "2" && $("#DFJobAssignLogsbAssignMail").checkbox('getValue') == true) {
                      AssignLogsToMail();
                  }
              }


          }

          function OnDeletedAssignLogs() {
              $('#DGJobAssignLogs').datagrid("reload");
              UserQuery();
          }
          //完整顯示Grid聯繫紀錄
          function ShowAllGrid(value) {
              return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
          }
          //---------------------------------------派任作業權限控制-----------------------------------------------------
          function AssignUpdateRow(rowData) {
              var IsDone = rowData.IsDone;//已寫入員工資料不可編輯
              if (IsDone == true) {
                  alert('作業完成，無法編輯！');
                  return false; //取消編輯的動作 
              }
          }

          //派任紀錄刪除=>刪除最新一筆(取消刪除動作並執行新指令)   
          function AssignDeleteRow(rowData) {
              var IsDone = rowData.IsDone;//已寫入員工資料不可編輯
              if (IsDone == true) {
                  alert('作業完成，無法編輯！');
                  return false; //取消編輯的動作 
              }
              var pre = confirm("確定失效此筆紀錄?");
              if (pre == true) {
                  //callServerMethod
                  var row = $('#DGJobAssignLogs').datagrid('getSelected');
                  $.ajax({
                      type: "POST",
                      url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_User_Management2.REC_User',
                      data: "mode=method&method=" + "JobAssignLogsIsActive" + "&parameters=" + rowData.AutoKey,
                      cache: false,
                      async: true,
                  });
                  $('#DGJobAssignLogs').datagrid("reload");
                  UserQuery();
                  return false; //取消刪除的動作 
              }
              else {
                  return false;
              }
          }

          //---------------------------------------批次派任作業-----------------------------------------------------
          function AssignAddMore() {
              if ($("#dataGridView").datagrid('getChecked').length == 0) {
                  alert('請勾選人才。');
              } else {
                  openForm('#JQDialogAssignLogsMore', $('#dataGridView').datagrid('getSelected'), "update", 'dialog');
              }
          }
          function OnLoadAssignLogsMore() {
              ////清空選擇 ----履歷來源
              if ($('#DFJobAssignLogsMoreRecID').combobox('getValue') == "") {
                  //派任作業=>(登入用戶編號=>預設招募代號)
                  var UserID = getClientInfo("UserID");
                  setTimeout(function () {
                      var data = $("#DFJobAssignLogsMoreRecID").combobox('getData');
                      for (var i = 0; i < data.length; i++) {
                          if (data[i].EmpID == UserID) {
                              $("#DFJobAssignLogsMoreRecID").combobox('setValue', data[i].ID);
                          }
                      }
                  }, 200);

              }
              //推薦記錄預設            
              $("#DFJobAssignLogsMoreAutoKey").val(0);
              $("#SDate_Query").datebox("getValue");
              var AssignID = $("#DFJobAssignLogsMoreAssignID").combobox('getValue');
              ControlAssignMore(AssignID);
          }
          //派任記錄狀態控管
          function OnSelectAssignIDMore(rowData) {
              var AssignID = rowData.AssignID;//1推薦中,2待報到,3待加保
              ControlAssignMore(AssignID);

          }
          function ControlAssignMore(AssignID) {
              $('#DFJobAssignLogsMoreAssignMail').closest('td').prev('td').hide();//發信內文隱藏
              $('#DFJobAssignLogsMoreAssignMail').closest('td').hide();
              $('#DFJobAssignLogsMorebAssignMail').closest('td').prev('td').hide();//是否發信隱藏
              $('#DFJobAssignLogsMorebAssignMail').closest('td').hide();
              if (AssignID == "2") {
                  $('#DFJobAssignLogsMoreAssignMail').closest('td').prev('td').show();//推薦評估顯示
                  $('#DFJobAssignLogsMoreAssignMail').closest('td').show();
                  $('#DFJobAssignLogsMorebAssignMail').closest('td').prev('td').show();//是否發信顯示
                  $('#DFJobAssignLogsMorebAssignMail').closest('td').show();
                  $('#DFJobAssignLogsMoreAssignMail').val("  恭喜您錄取了，請登入下方連結填寫報到前所需要的資料與上傳報到時需要的文件。\n  網站中也有報到時需攜帶的文件可以下載使用。\n  若有任何問題請再來電詢問。\n  謝謝。");
              }
          }
          function OnApplyAssignLogsMore() {

              if ($("#DFJobAssignLogsMoreAssignID").combobox('getValue') == "2" && $("#DFJobAssignLogsMorebAssignMail").checkbox('getValue') == true) {
                  if ($("#DFJobAssignLogsMoreAssignMail").val().trim() == "") {
                      alert('請填寫發信內文！');
                      return false;
                  }
              }
              //檢查客戶-職缺必選
              if ($("#DFJobAssignLogsMoreAssignID").combobox('getValue') == "1") {//推薦
                  if ($('#DFJobAssignLogsMoreRecommendCust').val() == "" && $('#DFJobAssignLogsMoreJobID').refval('getValue') == "") {
                      alert('請選擇客戶-職缺或填寫推薦職缺！');
                      return false;
                  }
              } else {
                  if ($('#DFJobAssignLogsMoreJobID').refval('getValue') == "") {
                      alert('請選擇客戶-職缺！');
                      return false;
                  }
              }

          }

          function AssignLogsToMail() {
              var UserID = $('#dataFormMaster8UserID').val();
              var NameC = $('#dataFormMaster8NameC').val();
              var Email = $('#dataFormMaster8Email').val();
              var AssignMail = $('#DFJobAssignLogsAssignMail').val();
              $.ajax({
                  type: "POST",
                  url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_User_Management2.REC_User',
                  data: "mode=method&method=" + "AssignLogsToMail" + " &parameters=" + UserID + "*" + NameC + "*" + Email + "*" + AssignMail,
                  cache: false,
                  async: false,
                  success: function (data) {
                  },
                  error: function (xhr, ajaxOptions, thrownError) {
                      alert(xhr.status);
                      alert(thrownError);
                  }
              });
          }

          function OnAppliedAssignLogsMore() {

              var rows = $('#dataGridView').datagrid("getChecked");
              var UserIDStr = [];//人才ID群
              var NameCStr = [];//人才中文群
              var EmailStr = [];//人才email群

              for (var i = 0; i < rows.length; i++) {
                  UserIDStr.push(rows[i].UserID);
                  NameCStr.push(rows[i].NameC);
                  EmailStr.push(rows[i].Email);
              }
             
              var sUserID = UserIDStr.join(',');
              var sNameC = NameCStr.join(',');
              var sEmail = EmailStr.join(',');

              var CustID = $('#DFJobAssignLogsMoreCustID').val();
              var JobID = $('#DFJobAssignLogsMoreJobID').refval('getValue');//客戶-職缺
              var RecommendCust = $('#DFJobAssignLogsMoreRecommendCust').val();//推薦職缺
              var RecID = $('#DFJobAssignLogsMoreRecID').combobox('getValue');//招募人員
              var AssignID = $('#DFJobAssignLogsMoreAssignID').combobox('getValue');//派任狀態
              var AssignTime = $('#DFJobAssignLogsMoreAssignTime').datebox('getValue');//派任日期
              var AssignContent = $('#DFJobAssignLogsMoreAssignContent').val();//派任備註
              var bAssignMail = $("#DFJobAssignLogsMorebAssignMail").checkbox('getValue');//是否發信
              var AssignMail = $('#DFJobAssignLogsMoreAssignMail').val();//發信內文

              if (JobID == "") {
                  JobID = 0;
              }

              $.ajax({
                  type: "POST",
                  url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_User_Management2.REC_User',
                  data: "mode=method&method=" + "AddJobAssignLogsMore" + " &parameters=" + sUserID + "*" + sNameC + "*" + sEmail + "*" + CustID + "*" + JobID + "*" + RecommendCust + "*" + RecID + "*" + AssignID + "*" + AssignTime + "*" + AssignContent + "*" + bAssignMail + "*" + AssignMail,
                  cache: false,
                  async: false,
                  success: function (data) {
                      UserQuery();
                  },
                  error: function (xhr, ajaxOptions, thrownError) {
                      alert(xhr.status);
                      alert(thrownError);
                  }
              });

          }
          //--------------------------------寫入招募系統--------------------------------
          function RecruitLink(value, row, index) {
              if (row.IsRec == true) {//已寫
                  return $('<a>', { href: "#", theData: row.UserID }).linkbutton({ text: "<img src=img/ok.png></a><b><div style=\"color:Red\"></div></b>", plain: true })[0].outerHTML;
              } else if (row.Country == "1" && (row.PID == "" || row.CurTelNO == "" || row.Birthday == null)) {//

                  return "<a style='color:red' title='身份證號、出生日期、聯絡電話必填。'>資料不足</a>";

              } else if (row.Country != "1" && (row.ResidentID == "" || row.CurTelNO == "" || row.Birthday == null)) {//

                  return "<a style='color:red' title='居留證號 、出生日期、聯絡電話必填。'>資料不足</a>";
              }
              else {
                  return $('<a>', { href: 'javascript:void(0)', name: 'RecruitLink', onclick: 'InsertRecruit(' + index + ')' }).linkbutton({ plain: false, text: '寫入' })[0].outerHTML;
              }
          }
          //檢查身份證字號不可以重複
          function CheckUserCount(PID) {
              var iCount;

              $.ajax({
                  type: "POST",
                  url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_User_Management2.REC_User', //連接的Server端，command
                  data: "mode=method&method=" + "ReturnUserCount" + "&parameters=" + PID,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                  cache: false,
                  async: false,
                  success: function (data) {
                      iCount = data
                  },
              });
              return iCount;
          }
          function InsertRecruit(index) {
              $("#dataGridView").datagrid('selectRow', index);
              var row = $('#dataGridView').datagrid('getSelected'); //取得當前主檔中選中的那個Data
                    
              //if (row.PID == "" || row.Birthday == "") {
              //    alert('身份證號、出生日期等不可以空白！');
              //} else {
              if (row.Country == "1") {
                  if (CheckUserCount(row.PID) != "0") {
                      alert('此身份證號於招募系統已經存在！');
                      return false;
                  }
              } else {
                  if (CheckUserCount(row.ResidentID) != "0") {
                      alert('此居留證號於招募系統已經存在！');
                      return false;
                  }
              }
                      var pre = confirm("確定寫入招募系統?");
                      if (pre == true) {
                          if (row != null) {
                              var cnt;
                              $.ajax({
                                  type: "POST",
                                  url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_User_Management2.REC_User', //連接的Server端，command
                                  data: "mode=method&method=" + "InsertUserbyREC_User" + "&parameters=" + row.UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                                  cache: false,
                                  async: true,
                                  success: function (data) {
                                      UserQuery();
                                  }
                              });
                          }
                      }
                  

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
          function OpenResume(FileName, UserID, JobID) {
              var AutoKey = 0;
              var sDyItem = "";
              var url = "../JB_ADMIN/REPORT/RecUser/RecommendReport2.aspx?FileName=" + FileName + "&UserID=" + UserID + "&JobID=" + JobID + "&AutoKey=" + AutoKey + "&sDyItem=" + sDyItem;

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
                  title: "推薦履歷",
                  //maximizable: true                              
              });
              $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="98%"></iframe>').appendTo(dialog.find('.panel-body'));
              dialog.dialog('open');

          }
         
          //--------------應徵職缺(紀錄)-----------------------------------------------------------------------------------------------   
          function ApplyJobLink(value, row, index) {
              var svalue = "0";
              if (value != "0") {
                  return "<a href='javascript: void(0)' onclick='LinkApplyJob(" + index + ");' style='color:red;'>" + value + "</a>";
              } else return "0";
          }
          // open主動應徵
          function LinkApplyJob(index) {
              $("#dataGridView").datagrid('selectRow', index);
              openForm('#JQDialogApplyJob', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
              ApplyJobQuery();
          }
          //應徵紀錄查詢
          function ApplyJobQuery() {
              var result2 = [];
              var JobDate1 = $('#JobDate1_Query').datebox('getValue');//應徵日期1
              var JobDate2 = $('#JobDate2_Query').datebox('getValue');//應徵日期2    
              var Consultants = $('#JobConsultants_Query').combobox('getValue');//招募人員
              var sCust = $('#sCust_Query').val();//客戶名稱
              var sName = $('#sName_Query').val();//職缺名稱

              var UserID = $('#dataGridView').datagrid('getSelected').UserID;
              if (UserID != '') result2.push("j.UserIdBack='" + UserID + "'");
              if (JobDate1 != '') result2.push("j.UpdateDate between '" + JobDate1 + "' and '" + JobDate2 + "'");
              if (Consultants != '') result2.push("u.ServiceConsultants=" + Consultants);
              if (sCust != '') result2.push("u.COMPANY_FrontName like '%" + sCust + "%'");
              if (sName != '') result2.push("u.COMPANY_JOB_FrontName like '%" + sName + "%'");

              $("#DGApplyJobLogs").datagrid('setWhere', result2.join(' and '));

          }

          //---------------呼叫開啟公告職缺 Tab--------------------------------------------------------------------------------
          function OpenJobTab(value, row, index) {
              if (value == undefined) ""
              else if (value != "0")
                  return "<a href='javascript: void(0)' onclick='LinkJobTab(" + index + ");' >" + value + "</a>";
              else return value;
          }
          function LinkJobTab(index) {
              $("#DGApplyJobLogs").datagrid('selectRow', index);
              var rows = $("#DGApplyJobLogs").datagrid('getSelected');
              var JobId = rows.JobId;

              parent.addTab('公告職缺維護', './JB_ADMIN/HRM_COMPANY_JOBFront.aspx?JobId=' + JobId);
          }

      </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <a href="#" id="LinkShowList" onclick="ShowCountList()"></a>
<%--            <asp:Label ID="Label1" runat="server" ForeColor="#6600FF" BackColor="#FFFF99" Font-Size="Medium"></asp:Label>--%>
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="_HRM_REC_User_Management2.REC_User" runat="server" AutoApply="True"
                DataMember="REC_User" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title=" " AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="7,20,30,40,50" PageSize="7" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="1055px" OnLoadSuccess="GVOnloadSuccess" OnDelete="OnDeleteRECUser" OnDeleted="OnDeletedRECUser">
                <Columns>
<%--                                 <JQTools:JQGridColumn Alignment="center" Caption="照片" Editor="text" FieldName="PhotoFile" Format="Image,Folder:../JQWebClient/Files/REC/Users,Height:30" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35" FormatScript="" />--%>
                                <JQTools:JQGridColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="68">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="推薦履歷" Editor="text" FieldName="LinkResume" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="54" FormatScript="LinkResume"></JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="面談紀錄" Editor="text" FieldName="ContactDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="59" FormatScript=""></JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="姓名" Editor="text" FieldName="NameC" MaxLength="20" Width="50" EditorOptions="" Visible="True" Sortable="False" FormatScript="Formatblacklist" />
                                <JQTools:JQGridColumn Alignment="center" Caption="應徵職缺" Editor="text" FieldName="iApplyJob" MaxLength="0" Width="55" FormatScript="ApplyJobLink" />                               
                                <JQTools:JQGridColumn Alignment="left" Caption="派任紀錄" Editor="" FieldName="sCustJob" MaxLength="0" Width="110" FormatScript="AssignLink" />
                                <JQTools:JQGridColumn Alignment="center" Caption="轉招募系統" Editor="text" FieldName="IsRec" FormatScript="RecruitLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="66">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="性別" Editor="text" FieldName="GenderText" Frozen="False" IsNvarChar="False" MaxLength="30" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="37">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="年齡" Editor="text" FieldName="iAge" MaxLength="0" Width="37" Sortable="False" />
                                <JQTools:JQGridColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="MobileNo" MaxLength="0" Width="70" />
                                <JQTools:JQGridColumn Alignment="left" Caption="工作地點" Editor="text" FieldName="DutyAreas" MaxLength="0" Width="110" />
                                <JQTools:JQGridColumn Alignment="left" Caption="現居地址.." Editor="text" FieldName="sCurAddress" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="最高學歷" Editor="text" FieldName="EduName" MaxLength="20" Width="55" EditorOptions="" Visible="True" Sortable="False" />
                                <JQTools:JQGridColumn Alignment="left" Caption="招募人員" Editor="text" FieldName="ConsultantName" Frozen="False" IsNvarChar="False" MaxLength="20" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="53" EditorOptions="">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="更新人員" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="53" EditorOptions="">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="更新日期" Editor="datebox" FieldName="LastUpdateDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70" />
                </Columns>
                <TooItems>
<%--                       <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增人才履歷" />        --%>

                    <JQTools:JQToolItem Enabled="True" ItemType="easyui-linkbutton" Text="派任作業" Visible="True" OnClick="AssignAddMore" Icon="icon-add" />
                </TooItems>
                <QueryColumns>
                               <JQTools:JQQueryColumn AndOr="and" Caption="人才搜尋" Condition="%" DataType="string" Editor="text" FieldName="NameC" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="195" DefaultValue="" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="性別" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'不拘',selected:'true'},{value:'0',text:'女',selected:'false'},{value:'1',text:'男',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:110" FieldName="Gender" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="年齡範圍" Condition="=" DataType="number" Editor="numberbox" FieldName="Age1" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="1" Width="50" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="=" DataType="number" Editor="numberbox" FieldName="Age2" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="公司名稱" Condition="%" DataType="string" Editor="text" FieldName="JobCompany1" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="95" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="專業證照" Condition="%" DataType="string" Editor="text" FieldName="ProLicenses" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="95" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="派任職缺" Condition="%" DataType="string" Editor="text" FieldName="AssignJob" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="195" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="最高學歷" Condition="%" DataType="string" Editor="infocombobox" FieldName="EduID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'_HRM_REC_User_Management2.infoREC_ZEduLevel',tableName:'infoREC_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:110" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="工作地點" Condition="%" DataType="string" Editor="text" FieldName="DutyAreas" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="2" Width="130" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="現居地址" Condition="%" DataType="string" Editor="text" FieldName="CurAddress" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="95" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="顯示筆數" Condition="=" DataType="number" DefaultValue="" Editor="numberbox" FieldName="ShowCount" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="招募區域" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'_HRM_REC_User_Management2.infoREC_SalesTeam',tableName:'infoREC_SalesTeam',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:SalesTeamOnSelectQ,panelHeight:90" FieldName="ServiceSalesTeam" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="70" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="招募人員" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ConsultantName',remoteName:'_HRM_REC_User_Management2.infoServiceConsultants',tableName:'infoServiceConsultants',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:150" FieldName="ServiceConsultants" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="處理狀態" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'不拘',selected:'true'},{value:'0',text:'未處理',selected:'false'},{value:'1',text:'已處理',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:110" FieldName="sStatus" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="修改日期" Condition="%" DataType="string" Editor="datebox" FieldName="SDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="1" Width="86" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" Editor="datebox" FieldName="EDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="86" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="填表日期" Condition="%" DataType="string" Editor="datebox" FieldName="SCDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="2" Width="86" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" Editor="datebox" FieldName="ECDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="86" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="是否樁腳" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'不拘',selected:'true'},{value:'1',text:'是',selected:'false'},{value:'0',text:'否',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:110" FieldName="IsPilefoot" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="70" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="推薦樁腳" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'MemberID',textField:'NameC',remoteName:'_HRM_REC_User_Management2.infoPFMemberID',tableName:'infoPFMemberID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:150" FieldName="PFMemberID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />                                
                                <JQTools:JQQueryColumn AndOr="and" Caption="全文檢索" Condition="%" DataType="string" Editor="text" FieldName="sFullSearch" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="4" Width="380" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" DefaultValue="and" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:65,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'and',value:'and'},{text:'or',value:'or'}]" FieldName="AndOr" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="70" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="履歷維護" DialogLeft="9px" DialogTop="1px" Width="1050px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="REC_User" HorizontalColumnsCount="6" RemoteName="_HRM_REC_User_Management2.REC_User" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" ChainDataFormID="dataFormMaster2" OnApplied="DFOnApplied" OnLoadSuccess="DFLoadSuccess" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Format="" Width="180" Visible="False" NewRow="False" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="中文姓名" Editor="text" FieldName="NameC" Format="" Width="90" NewRow="True" maxlength="0" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="MobileNo" Format="" Width="110" maxlength="0" Span="1" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="現居地址" Editor="infocombobox" FieldName="Addr_Country" Format="" maxlength="0" Width="80" EditorOptions="valueField:'Country',textField:'Country',remoteName:'_HRM_REC_User_Management2.Country',tableName:'Country',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:Addr_Country_OnSelect,panelHeight:200" Span="3" NewRow="False" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="Addr_City" Format="" maxlength="0" Width="80" EditorOptions="valueField:'City',textField:'City',remoteName:'_HRM_REC_User_Management2.City',tableName:'City',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:Addr_City_OnSelect,panelHeight:200" Span="1" NewRow="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="Addr_Desc" Format="" Width="235" maxlength="0" Span="1" NewRow="False" ReadOnly="False" Visible="True" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'COUNTRY_ID',textField:'COUNTRY_CNAME',remoteName:'_HRM_REC_User_Management2.infoHRM_COUNTRY',tableName:'infoHRM_COUNTRY',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:120" FieldName="Country" MaxLength="0" NewRow="True" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="郵件信箱" Editor="text" FieldName="Email" MaxLength="0" NewRow="True" Span="2" Visible="True" Width="250" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡電話" Editor="text" FieldName="CurTelNO" maxlength="0" Width="110" Span="1" NewRow="False" Visible="True" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="上班工具" Editor="infooptions" FieldName="TrafficIDs" Format="" maxlength="0" NewRow="False" Span="3" Visible="True" Width="180" EditorOptions="title:'上班工具',panelWidth:390,remoteName:'_HRM_REC_User_Management2.infoREC_ZTraffic',tableName:'infoREC_ZTraffic',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectTrafficIDs,selectOnly:false,items:[]" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="登入帳號" Editor="text" FieldName="MemberID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="85" />
                        <JQTools:JQFormColumn Alignment="left" Caption="血型" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'_HRM_REC_User_Management2.infoREC_ZBloodType',tableName:'infoREC_ZBloodType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:120" FieldName="BloodType" NewRow="False" Span="1" Visible="True" Width="71" />
                         <JQTools:JQFormColumn Alignment="left" Caption="LineID" Editor="text" FieldName="LineID" NewRow="False" Visible="True" Width="110" Span="1" MaxLength="0" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="人才照片" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'../JQWeb2015/Files/Files/Rec/Users',showButton:true,showLocalFile:false,fileSizeLimited:'1000'" FieldName="PhotoFile" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最高學校" Editor="text" FieldName="SchoolName1" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="250" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最高學歷" Editor="infocombobox" FieldName="EduID1" MaxLength="0" Span="1" Width="110" NewRow="False" ReadOnly="False" Visible="True" RowSpan="1" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'_HRM_REC_User_Management2.infoREC_ZEduLevel',tableName:'infoREC_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:EduID1Select,panelHeight:120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最高科系" Editor="text" FieldName="Department1" MaxLength="0" NewRow="False" Span="1" Width="250" ReadOnly="False" Visible="True" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最高狀態" Editor="infocombobox" FieldName="GradeStatus1" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="80" RowSpan="1" EditorOptions="items:[{value:'畢業',text:'畢業',selected:'true'},{value:'肄業',text:'肄業',selected:'false'},{value:'就學中',text:'就學中',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="次高學校" Editor="text" FieldName="SchoolName2" MaxLength="0" Span="2" Width="250" NewRow="True" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="次高學歷" Editor="infocombobox" FieldName="EduID2" MaxLength="0" Span="1" Width="110" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'_HRM_REC_User_Management2.infoREC_ZEduLevel',tableName:'infoREC_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:EduID2Select,panelHeight:120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="次高科系" Editor="text" FieldName="Department2" MaxLength="0" Span="1" Width="250" NewRow="False" ReadOnly="False" Visible="True" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="次高狀態" Editor="infocombobox" FieldName="GradStatus2" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="80" RowSpan="1" EditorOptions="items:[{value:'畢業',text:'畢業',selected:'true'},{value:'肄業',text:'肄業',selected:'false'},{value:'就學中',text:'就學中',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:130" />

                        <JQTools:JQFormColumn Alignment="left" Caption="EduName1" Editor="text" FieldName="EduName1" maxlength="0" NewRow="True" Span="1" Visible="False" Width="80" ReadOnly="False" RowSpan="1" />

                        <JQTools:JQFormColumn Alignment="left" Caption="EduName2" Editor="text" FieldName="EduName2" NewRow="False" Span="1" Visible="False" Width="80" MaxLength="0" ReadOnly="False" RowSpan="1" />

                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" FieldName="RecIsActive" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" />

                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDataGrid ID="DGUserCareer" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="REC_UserCareer" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogUserCareer" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="_HRM_REC_User_Management2.REC_User" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="940px">
                    <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="JobCompany" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="170">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="職務" Editor="text" FieldName="JobTitle" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="185" FormatScript="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="工作期間" Editor="infocombobox" FieldName="JobPeriodS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="52" EditorOptions="valueField:'sDate',textField:'sDate',remoteName:'_HRM_REC_User_Management2.infoJobPeriod',tableName:'infoJobPeriod',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption=" " Editor="infocombobox" FieldName="JobPeriodE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="52" EditorOptions="valueField:'sDate',textField:'sDate',remoteName:'_HRM_REC_User_Management2.infoJobPeriod2',tableName:'infoJobPeriod2',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="薪資待遇" Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="JobSalary" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="53" DrillObjectID="" Format="N">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="離職原因" Editor="text" FieldName="JobQuitDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                    </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="工作內容" Editor="textarea" FieldName="JobDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="230">
                        </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="修改時間" Editor="text" FieldName="LastUpdateDate" Format="yyyy/mm/dd HH:MM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="92">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="52">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增工作經歷" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <br />
                <JQTools:JQDataForm ID="dataFormMaster3" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="REC_User" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="6" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="_HRM_REC_User_Management2.REC_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApplied="DFOnApplied" ChainDataFormID="" OnLoadSuccess="DFLoadSuccess">
                    <Columns>

                        <JQTools:JQFormColumn Alignment="left" Caption="就業狀況" Editor="checkbox" FieldName="IsOnDuty" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="住宿需求" Editor="checkbox" FieldName="IsNeedDorm" MaxLength="0" NewRow="False" Span="1" Visible="True" Width="30" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="進修意願" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsEducation" maxlength="0" NewRow="False" Span="1" Visible="True" Width="30" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="right" Caption="期望薪資" Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="ExpectedSalary" NewRow="False" Span="1" Visible="True" Width="60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="專業證照" Editor="text" FieldName="ProLicenses" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="320" />


                        <JQTools:JQFormColumn Alignment="left" Caption="工作時間" Editor="infooptions" EditorOptions="title:'上班工具',panelWidth:210,remoteName:'_HRM_REC_User_Management2.infoREC_ZDutyTimes',tableName:'infoREC_ZDutyTimes',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="DutyTimesIDs" Format="" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作班別" Editor="infooptions" EditorOptions="title:'工作班別',panelWidth:390,remoteName:'_HRM_REC_User_Management2.infoREC_ZDutyClasses',tableName:'infoREC_ZDutyClasses',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="DutyClassesIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班意願" Editor="infooptions" EditorOptions="title:'加班意願',panelWidth:280,remoteName:'_HRM_REC_User_Management2.infoREC_ZOverTimes',tableName:'infoREC_ZOverTimes',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectOverTimesIDs,selectOnly:false,items:[]" FieldName="OverTimesIDs" Format="" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="無塵衣" Editor="infooptions" EditorOptions="title:'無塵衣',panelWidth:425,remoteName:'_HRM_REC_User_Management2.infoREC_ZCleanClothes',tableName:'infoREC_ZCleanClothes',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectCleanClothes,selectOnly:false,items:[]" FieldName="CleanClothesIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作工具" Editor="infooptions" EditorOptions="title:'工作工具',panelWidth:310,remoteName:'_HRM_REC_User_Management2.infoREC_ZDutyTools',tableName:'infoREC_ZDutyTools',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectDutyTools,selectOnly:false,items:[]" FieldName="DutyToolsIDs" Format="" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作型態" Editor="infooptions" EditorOptions="title:'工作型態',panelWidth:400,remoteName:'_HRM_REC_User_Management2.infoREC_ZDutyActTypes',tableName:'infoREC_ZDutyActTypes',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectDutyActTypesIDs,selectOnly:false,items:[]" FieldName="DutyActTypesIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作環境" Editor="infooptions" EditorOptions="title:'工作環境',panelWidth:400,remoteName:'_HRM_REC_User_Management2.infoREC_ZDutyEnvironment',tableName:'infoREC_ZDutyEnvironment',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectDutyEnvironmentIDs,selectOnly:false,items:[]" FieldName="DutyEnvironmentIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />                        
                        <JQTools:JQFormColumn Alignment="left" Caption="工作縣市" Editor="infooptions" EditorOptions="title:'工作縣市',panelWidth:330,remoteName:'_HRM_REC_User_Management2.infoREC_ZDutyAreasClass',tableName:'infoREC_ZDutyAreasClass',valueField:'ID',textField:'Contents',columnCount:3,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectDutyAreaClass,selectOnly:false,items:[]" FieldName="DutyAreaClassIDs" Format="" NewRow="True" Span="2" Visible="True" Width="90" MaxLength="0" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作地點" Editor="infooptions" EditorOptions="title:'工作地點',panelWidth:570,remoteName:'_HRM_REC_User_Management2.infoREC_ZDutyAreas',tableName:'infoREC_ZDutyAreas',valueField:'ID',textField:'Contents',columnCount:8,multiSelect:true,openDialog:false,selectAll:false,onWhere:OnWhereAreaClassID,selectOnly:false,items:[]" FieldName="DutyAreasIDs" Format="" NewRow="False" Span="4" Visible="True" Width="90" MaxLength="0" ReadOnly="False" RowSpan="1" />
            
                         <JQTools:JQFormColumn Alignment="left" Caption="其它地點" Editor="text" FieldName="DutyAreasOther" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="6" Visible="True" Width="360" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EduName1" Editor="text" FieldName="EduName1" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EduName2" Editor="text" FieldName="EduName2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />


                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDataForm ID="dataFormMaster2" runat="server" AlwaysReadOnly="False" ChainDataFormID="dataFormMaster3" Closed="False" ContinueAdd="False" DataMember="REC_User" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="7" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="DFOnApplied" RemoteName="_HRM_REC_User_Management2.REC_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Format="" maxlength="0" Span="1" Visible="False" Width="180" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="身份證號" Editor="text" FieldName="PID" Format="" maxlength="0" Width="80" Span="1" NewRow="True" Visible="True" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出生日期" Editor="datebox" FieldName="Birthday" maxlength="0" Span="1" Width="85" Visible="True" />                        
                        <JQTools:JQFormColumn Alignment="left" Caption="居留證" Editor="text" FieldName="ResidentID" Format="" maxlength="0" Width="80" Span="1" Visible="True" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="居留事由" Editor="text" FieldName="ResidenceDesc" MaxLength="0" Span="2" Width="180" NewRow="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="有效日" Editor="datebox" FieldName="ResidenceSDate" maxlength="0" Width="85" Span="2" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="datebox" FieldName="ResidenceEDate" MaxLength="0" NewRow="True" Span="1" Width="85" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="緊急聯絡人" Editor="text" FieldName="CONTACT_NAME" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="7" Visible="True" Width="64" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CONTACT_PHONE" MaxLength="0" Span="1" Visible="True" Width="85" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="CONTACT_RELATION_ID" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" EditorOptions="valueField:'RELATION_ID',textField:'RELATION_NAME',remoteName:'_HRM_REC_User_Management2.infoCONTACT_RELATION_ID',tableName:'infoCONTACT_RELATION_ID',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:90" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" EditorOptions="on:1,off:0" FieldName="Same" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="25" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="Domicile_Addr_Country" maxlength="0" NewRow="False" Span="1" Visible="True" Width="75" EditorOptions="valueField:'Country',textField:'Country',remoteName:'_HRM_REC_User_Management2.Country',tableName:'Country',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:Domicile_Addr_Country_OnSelect,panelHeight:200" Format="" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'City',textField:'City',remoteName:'_HRM_REC_User_Management2.City',tableName:'City',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:Domicile_Addr_City_OnSelect,panelHeight:200" FieldName="Domicile_Addr_City" maxlength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="75" Format="" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="Domicile_Addr_Desc" Format="" maxlength="0" Width="232" Span="1" NewRow="False" Visible="True" />                        
                        <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:120,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:true,items:[{text:'女',value:'false'},{text:'男',value:'true'}]" FieldName="Gender" MaxLength="20" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="55" />
                        <JQTools:JQFormColumn Alignment="left" Caption="身高" Editor="numberbox" FieldName="Tall" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="6" Visible="True" Width="40" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" FieldName="Weight" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="35" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="items:[{value:'未婚',text:'未婚',selected:'false'},{value:'已婚',text:'已婚',selected:'false'},{value:'分居',text:'分居',selected:'false'},{value:'離婚',text:'離婚',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="Marriage" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="85" />
                          <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="items:[{value:'自有',text:'自有',selected:'false'},{value:'租屋',text:'租屋',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="HouseOwnStatus" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="72" />                      
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'_HRM_REC_User_Management2.infoREC_ZMilitaryService',tableName:'infoREC_ZMilitaryService',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:90" FieldName="MilitaryServiceIDs" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="85" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="MilitaryReason" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="MilitaryYM" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="45" />
                        <JQTools:JQFormColumn Alignment="left" Caption="近視狀態" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'_HRM_REC_User_Management2.infoREC_ZShortsight',tableName:'infoREC_ZShortsight',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="ShortsightIDs" MaxLength="128" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="散光狀態" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'_HRM_REC_User_Management2.infoREC_ZAstigm',tableName:'infoREC_ZAstigm',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="AstigmIDs" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="辨色能力" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'_HRM_REC_User_Management2.infoREC_ZColorVision',tableName:'infoREC_ZColorVision',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="ColorVisionIDs" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        
                        <JQTools:JQFormColumn Alignment="left" Caption="家庭成員" Editor="infooptions" EditorOptions="title:'家庭成員',panelWidth:374,remoteName:'_HRM_REC_User_Management2.infoREC_ZFamily',tableName:'infoREC_ZFamily',valueField:'ID',textField:'Contents',columnCount:8,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="FamilyIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總共" Editor="numberbox" FieldName="FamilyCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="20" />
                        <JQTools:JQFormColumn Alignment="left" Caption="吸菸" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'_HRM_REC_User_Management2.infoREC_ZSmoking',tableName:'infoREC_ZSmoking',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="SmokingIDs" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="樁腳" Editor="checkbox" FieldName="IsPilefoot" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="45" />
                        <JQTools:JQFormColumn Alignment="left" Caption="推薦樁腳" Editor="infocombobox" EditorOptions="valueField:'MemberID',textField:'NameC',remoteName:'_HRM_REC_User_Management2.infoPFMemberID',tableName:'infoPFMemberID',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="PFMemberID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        
                        <JQTools:JQFormColumn Alignment="left" Caption="同居成員" Editor="infooptions" EditorOptions="title:'同居成員',panelWidth:374,remoteName:'_HRM_REC_User_Management2.infoREC_ZCohabits',tableName:'infoREC_ZCohabits',valueField:'ID',textField:'Contents',columnCount:8,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="CohabitIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總共" Editor="numberbox" FieldName="CohabitCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="20" />
                        <JQTools:JQFormColumn Alignment="left" Caption="身份別" Editor="infocombobox" EditorOptions="valueField:'EMPLOYEE_IDENTITY_ID',textField:'EMPLOYEE_IDENTITY_CNAME',remoteName:'_HRM_REC_User_Management2.infoHRM_EMPLOYEE_IDENTITY',tableName:'infoHRM_EMPLOYEE_IDENTITY',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:150" FieldName="IdentityID" Format="" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="面試來源" Editor="infocombobox" EditorOptions="valueField:'InfoSource',textField:'InfoSource',remoteName:'_HRM_REC_User_Management2.infoREC_ZInfoSource',tableName:'infoREC_ZInfoSource',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:150" FieldName="InfoSource" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="290" />                        
                        <JQTools:JQFormColumn Alignment="left" Caption="懷孕" Editor="checkbox" FieldName="IsPregnancy" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="45" />
                        <JQTools:JQFormColumn Alignment="left" Caption="吃檳榔" Editor="checkbox" FieldName="IsBetelnut" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="45" />
                        <JQTools:JQFormColumn Alignment="left" Caption="黑名單" Editor="checkbox" FieldName="blacklist" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="45" />
                        <JQTools:JQFormColumn Alignment="left" Caption="健康評量" Editor="infooptions" EditorOptions="title:'健康評量',panelWidth:900,remoteName:'_HRM_REC_User_Management2.infoREC_ZHealthStatus',tableName:'infoREC_ZHealthStatus',valueField:'ID',textField:'Contents',columnCount:20,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectHealthStatusIDs,selectOnly:false,items:[]" FieldName="HealthStatusIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="7" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="健康狀況" Editor="text" FieldName="HealthOther" MaxLength="0" NewRow="False" Span="2" Visible="True" Width="220" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="經濟壓力" Editor="infooptions" EditorOptions="title:'經濟壓力',panelWidth:590,remoteName:'_HRM_REC_User_Management2.infoREC_ZEcoPressure',tableName:'infoREC_ZEcoPressure',valueField:'ID',textField:'Contents',columnCount:20,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectEcoPressureIDs,selectOnly:false,items:[]" FieldName="EcoPressureIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="招募區域" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'_HRM_REC_User_Management2.infoREC_SalesTeam',tableName:'infoREC_SalesTeam',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:SalesTeamOnSelect,panelHeight:90" FieldName="ServiceSalesTeam" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="85" />
                        <JQTools:JQFormColumn Alignment="left" Caption="招募人員" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ConsultantName',remoteName:'_HRM_REC_User_Management2.infoServiceConsultants',tableName:'infoServiceConsultants',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:90" FieldName="ServiceConsultants" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款帳號" Editor="text" FieldName="AccountNo" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="6" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="AccountName" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="AccountID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="85" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'BANK_ID',textField:'BANK_CName',remoteName:'_HRM_REC_User_Management2.infoHRM_BANK',tableName:'infoHRM_BANK',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:BANK_IDOnSelect,panelHeight:90" FieldName="BankID" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="110" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="BankBranchID" maxlength="0" Width="110" Span="1" NewRow="False" ReadOnly="False" Visible="True" RowSpan="1" EditorOptions="valueField:'BANKBRANCH_CODE',textField:'BANKBRANCH_CNAME',remoteName:'_HRM_REC_User_Management2.infoHRM_BANKBRANCH',tableName:'infoHRM_BANKBRANCH',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:90" />

                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="UserID" RemoteMethod="True" ValidateMessage="中文姓名不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="NameC" RemoteMethod="True" ValidateMessage="姓名不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Addr_Country" RemoteMethod="True" ValidateMessage="現居地不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Addr_City" RemoteMethod="True" ValidateMessage="現居地不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Addr_Desc" RemoteMethod="True" ValidateMessage="現居地不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Email" RemoteMethod="True" ValidateMessage="信箱格式不正確！" ValidateType="EMail" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="MobileNo" RemoteMethod="True" ValidateMessage="行動電話不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Country" RemoteMethod="True" ValidateMessage="請選擇國籍！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="TrafficIDs" RemoteMethod="True" ValidateMessage="請選擇上班工具！" ValidateType="None" />
<%--                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SchoolName1" RemoteMethod="True" ValidateMessage="最高學校不可空白！" ValidateType="None" />--%>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EduID1" RemoteMethod="True" ValidateMessage="請選擇最高學歷！" ValidateType="None" />
<%--                        <JQTools:JQValidateColumn CheckNull="True" FieldName="GradeStatus1" RemoteMethod="True" ValidateMessage="請選擇最高狀態！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Department1" RemoteMethod="True" ValidateMessage="最高科系不可空白！" ValidateType="None" />--%>
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDataGrid ID="DGContactRecord" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="REC_UserContactRecord" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogContactRecord" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnDelete="UserContactDeleteRow" OnDeleted="OnDeletedContactRecord" OnUpdate="UserContactUpdateRow" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="_HRM_REC_User_Management2.REC_User" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="940px">
                    <Columns>
              
                        <JQTools:JQGridColumn Alignment="center" Caption="面談日期" Editor="datebox" FieldName="ContactDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="面談內容" Editor="text" FieldName="Notes" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="580" FormatScript="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="修改時間" Editor="text" FieldName="UpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="92" Format="yyyy/mm/dd HH:MM">
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
                <JQTools:JQDataGrid ID="DGREC_UserFamily" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="REC_UserFamily" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogUserFamily" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnDelete="UserFamilyDeleteRow" OnDeleted="OnDeletedUserFamily" OnUpdate="UserFamilyUpdateRow" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="_HRM_REC_User_Management2.REC_User" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="940px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="眷屬姓名" Editor="text" FieldName="UserFamilyName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
<%--                        <JQTools:JQGridColumn Alignment="center" Caption="眷屬性別" Editor="infocombobox" EditorOptions="items:[{value:'F',text:'女性',selected:'true'},{value:'M',text:'男性',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:130" FieldName="UserFamilySex" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>--%>
                        <JQTools:JQGridColumn Alignment="center" Caption="身分證號" Editor="text" FieldName="UserFamilyIdno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="眷屬生日" Editor="datebox" FieldName="UserFamilyBirthday" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="所得稅扶養親屬否" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsSupport" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" FormatScript="genCheckBox">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="關係" Editor="infocombobox" EditorOptions="valueField:'RELATION_ID',textField:'RELATION_NAME',remoteName:'_HRM_REC_User_Management2.infoRELATION',tableName:'infoRELATION',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:130" FieldName="Relation_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="修改時間" Editor="text" FieldName="LastUpdateDate" Format="yyyy/mm/dd HH:MM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="110">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="修改者" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
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
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增健保眷屬" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <JQTools:JQValidate ID="validateMaster2" runat="server" BindingObjectID="dataFormMaster2" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="False" FieldName="PID" RemoteMethod="True" ValidateMessage="身份證號格式不對！" ValidateType="IdCard" CheckMethod="" />
                        <JQTools:JQValidateColumn CheckMethod="checkBirthday" CheckNull="False" FieldName="Birthday" RemoteMethod="False" ValidateMessage="需大於15歲！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQValidate ID="validateMaster3" runat="server" BindingObjectID="dataFormMaster3" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DutyTimesIDs" RemoteMethod="True" ValidateMessage="請選擇工作時間！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DutyClassesIDs" RemoteMethod="True" ValidateMessage="請選擇工作班別！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DutyAreasIDs" RemoteMethod="True" ValidateMessage="請選擇工作地點！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DutyAreaClassIDs" RemoteMethod="True" ValidateMessage="請選擇工作縣市！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDialog ID="JQDialogUserCareer" runat="server" BindingObjectID="DFUserCareer" DialogLeft="100px" DialogTop="20px" Title="工作經驗維護" Width="900px">
                    <JQTools:JQDataForm ID="DFUserCareer" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="REC_UserCareer" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedCareer" OnApply="OnApplyCareer" OnLoadSuccess="OnLoadSuccessCareer" ParentObjectID="dataFormMaster" RemoteName="_HRM_REC_User_Management2.REC_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="JobCompany" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="210" />
                            <JQTools:JQFormColumn Alignment="left" Caption="職務 " Editor="text" FieldName="JobTitle" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="210" />
                            <JQTools:JQFormColumn Alignment="left" Caption="工作期間" Editor="infocombobox" EditorOptions="valueField:'sDate',textField:'sDate',remoteName:'_HRM_REC_User_Management2.infoJobPeriod',tableName:'infoJobPeriod',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectDutyDate,panelHeight:200" FieldName="JobPeriodS" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="right" Caption="薪資待遇" Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="JobSalary" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                            <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="JobPeriodE" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" EditorOptions="valueField:'sID',textField:'sDate',remoteName:'_HRM_REC_User_Management2.infoJobPeriod2',tableName:'infoJobPeriod2',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />                            
                            <JQTools:JQFormColumn Alignment="left" Caption="離職原因" Editor="textarea" EditorOptions="height:25" FieldName="JobQuitDescr" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="750" />
                            <JQTools:JQFormColumn Alignment="left" Caption="工作內容" Editor="textarea" FieldName="JobDescr" MaxLength="3000" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="750" EditorOptions="height:110" />
                            <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateby" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQAutoSeq ID="JQAutoSeqCareer" runat="server" BindingObjectID="DFUserCareer" FieldName="AutoKey" NumDig="1" />
                    <JQTools:JQValidate ID="JQValidateCareer" runat="server" BindingObjectID="DFUserCareer" EnableTheming="True">
                        <Columns>
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="JobCompany" RemoteMethod="True" ValidateMessage="公司名稱不可空白！" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="JobTitle" RemoteMethod="True" ValidateMessage="職務不可空白！" ValidateType="None" />
<%--                            <JQTools:JQValidateColumn CheckNull="True" FieldName="JobSalary" RemoteMethod="True" ValidateMessage="薪資待遇不可空白！" ValidateType="None" />--%>
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="JobDescr" RemoteMethod="True" ValidateMessage="工作內容不可空白！" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckMethod="CheckStrWildWord" CheckNull="True" FieldName="JobPeriodS" RemoteMethod="False" ValidateMessage="起始工作期間格式錯誤！" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckMethod="CheckStrWildWord" CheckNull="False" FieldName="JobPeriodE" RemoteMethod="False" ValidateMessage="終止工作期間格式錯誤！" ValidateType="None" />
                        </Columns>
                    </JQTools:JQValidate>
                    <JQTools:JQDefault ID="JQDefaultCareer" runat="server" BindingObjectID="DFUserCareer" EnableTheming="True">
                        <Columns>
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCODE" FieldName="UserID" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="LastUpdateBy" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="CreateBy" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        </Columns>
                    </JQTools:JQDefault>
                </JQTools:JQDialog>
                                <JQTools:JQDialog ID="JQDialogContactRecord" runat="server" BindingObjectID="DFContactRecord" DialogLeft="120px" DialogTop="50px" Title="面談紀錄維護" Width="650px">
                    <JQTools:JQDataForm ID="DFContactRecord" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="REC_UserContactRecord" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="_HRM_REC_User_Management2.REC_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApplied="OnInsertedContactRecord">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="面談日期" Editor="datebox" FieldName="ContactDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="面談內容" Editor="textarea" EditorOptions="height:250" FieldName="Notes" MaxLength="3000" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="520" />
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
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                        </Columns>
                    </JQTools:JQDefault>
                    <JQTools:JQValidate ID="JQValidate2" runat="server" BindingObjectID="DFContactRecord" EnableTheming="True">
                        <Columns>
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactDate" RemoteMethod="True" ValidateMessage="請選擇面談日期！" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="Notes" RemoteMethod="True" ValidateMessage="面談內容不可空白！" ValidateType="None" />
                        </Columns>
                    </JQTools:JQValidate>
                    <JQTools:JQAutoSeq ID="JQAutoSeq2" runat="server" BindingObjectID="DFContactRecord" FieldName="AutoKey" NumDig="1" />
                </JQTools:JQDialog>

                <JQTools:JQDialog ID="JQDialogUserFamily" runat="server" BindingObjectID="DFUserFamily" DialogLeft="130px" DialogTop="130px" Title="健保眷屬維護" Width="650px">
                    <JQTools:JQDataForm ID="DFUserFamily" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="REC_UserFamily" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="3" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnInsertedUserFamily" ParentObjectID="dataFormMaster" RemoteName="_HRM_REC_User_Management2.REC_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="眷屬姓名" Editor="text" FieldName="UserFamilyName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
<%--                            <JQTools:JQFormColumn Alignment="left" Caption="眷屬性別" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:150,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:true,items:[{text:'女性',value:'F'},{text:'男性',value:'M'}]" FieldName="UserFamilySex" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />--%>
                            <JQTools:JQFormColumn Alignment="left" Caption="關係" Editor="infocombobox" EditorOptions="valueField:'RELATION_ID',textField:'RELATION_NAME',remoteName:'_HRM_REC_User_Management2.infoRELATION',tableName:'infoRELATION',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:130" FieldName="Relation_ID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="身分證號" Editor="text" FieldName="UserFamilyIdno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="110" />
                            <JQTools:JQFormColumn Alignment="left" Caption="眷屬生日" Editor="datebox" EditorOptions="" FieldName="UserFamilyBirthday" MaxLength="3000" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="100" />
                            <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsSupport" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" />
                            <JQTools:JQFormColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="UpdateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="JQDefault3" runat="server" BindingObjectID="DFUserFamily" EnableTheming="True">
                        <Columns>
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="CreateBy" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="LastUpdateBy" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                        </Columns>
                    </JQTools:JQDefault>
                    <JQTools:JQValidate ID="JQValidate3" runat="server" BindingObjectID="DFUserFamily" EnableTheming="True">
                        <Columns>
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="UserFamilyName" RemoteMethod="True" ValidateMessage="眷屬姓名不可空白！" ValidateType="None" />
<%--                            <JQTools:JQValidateColumn CheckNull="True" FieldName="UserFamilySex" RemoteMethod="True" ValidateMessage="請選擇眷屬性別！" ValidateType="None" />--%>
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="UserFamilyIdno" RemoteMethod="True" ValidateMessage="身分證字號不可空白！" ValidateType="IdCard" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="UserFamilyBirthday" RemoteMethod="True" ValidateMessage="眷屬生日不可空白！" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="Relation_ID" RemoteMethod="True" ValidateMessage="請選擇眷屬關係！" ValidateType="None" />
                        </Columns>
                    </JQTools:JQValidate>
                    <JQTools:JQAutoSeq ID="JQAutoSeq3" runat="server" BindingObjectID="DFUserFamily" FieldName="AutoKey" NumDig="1" />
                </JQTools:JQDialog>

            </JQTools:JQDialog>
        </div>

                <JQTools:JQDialog ID="JQDialogJobAssignLogs" runat="server" BindingObjectID="dataFormMaster8" Title="派任紀錄" Width="900px" DialogLeft="36px" DialogTop="20px" Height="440px" EditMode="Dialog" ShowSubmitDiv="False">
                    <JQTools:JQDataForm ID="dataFormMaster8" runat="server" AlwaysReadOnly="False" ChainDataFormID="" Closed="False" ContinueAdd="False" DataMember="REC_User" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="7" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="_HRM_REC_User_Management2.REC_User" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="中文姓名" Editor="text" FieldName="NameC" MaxLength="20" Span="1" Width="70" ReadOnly="True" NewRow="False" RowSpan="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="郵件信箱" Editor="text" FieldName="Email" MaxLength="0" ReadOnly="True" Span="1" Visible="True" Width="250" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <fieldset>
                        <legend></legend>


                        <JQTools:JQDataGrid ID="DGJobAssignLogs" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="Rec_JobAssignLogs" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogAssignLogs" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnUpdate="AssignUpdateRow" PageList="5,10,15,20" PageSize="5" Pagination="False" ParentObjectID="dataFormMaster8" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="_HRM_REC_User_Management2.REC_User" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="100%" OnDeleted="OnDeletedAssignLogs" OnDelete="AssignDeleteRow" Height="310px">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱-職缺" Editor="text" FieldName="sCustJob" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="190" EditorOptions="">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="派任狀態" Editor="infocombobox" FieldName="AssignID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'_HRM_REC_User_Management2.infoREC_ZAssignStep',tableName:'infoREC_ZAssignStep',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="派任日期" Editor="datebox" EditorOptions="" FieldName="AssignTime" Format="yyyy/mm/dd" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="派任人員" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ConsultantName',remoteName:'_HRM_REC_User_Management2.infoRecID',tableName:'infoRecID',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="RecID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="派任備註" Editor="text" FieldName="AssignContent" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="260">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="有效?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" FormatScript="genCheckBox">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="履歷編號" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateby" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="JobName" Editor="text" FieldName="JobName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="AssignMail" Editor="text" FieldName="AssignMail" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                            </RelationColumns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增派任" />

                            </TooItems>
                        </JQTools:JQDataGrid>
                        <JQTools:JQDialog ID="JQDialogAssignLogs" runat="server" BindingObjectID="DFJobAssignLogs" DialogLeft="120px" DialogTop="45px" Title="派任紀錄維護" Width="750px" ShowSubmitDiv="True">
                            <JQTools:JQDataForm ID="DFJobAssignLogs" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="Rec_JobAssignLogs" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster8" RemoteName="_HRM_REC_User_Management2.REC_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApplied="OnAppliedAssignLogs" OnLoadSuccess="OnLoadAssignLogs" OnApply="OnApplyAssignLogs">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="派任狀態" Editor="infocombobox" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'_HRM_REC_User_Management2.infoREC_ZAssignStep',tableName:'infoREC_ZAssignStep',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnSelectAssignID,panelHeight:200" FieldName="AssignID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" Format="" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="派任日期" Editor="datebox" EditorOptions="" FieldName="AssignTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="招募人員" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ConsultantName',remoteName:'_HRM_REC_User_Management2.infoRecID',tableName:'infoRecID',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="RecID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="當日到離" Editor="checkbox" FieldName="IsSamDay" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />                                    
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶-職缺" Editor="inforefval" EditorOptions="title:'請選擇',panelWidth:445,remoteName:'_HRM_REC_User_Management2.infoHRM_COMPANY_JOB',tableName:'infoHRM_COMPANY_JOB',columns:[{field:'COMPANY_ABBR',title:'客戶簡稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'COMPANY_JOB_CNAME',title:'職缺名稱',width:280,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'CustID',value:'COMPANY_ID'}],whereItems:[],valueField:'COMPANY_JOB_ID',textField:'COMPANY_JOB_CNAME',valueFieldCaption:'職缺流水號',textFieldCaption:'職缺名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="320" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="推薦職缺" Editor="text" FieldName="RecommendCust" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="190" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="派任備註" Editor="textarea" EditorOptions="height:80" FieldName="AssignContent" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="600" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="是否發信" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="bAssignMail" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="35" />                                    
                                    <JQTools:JQFormColumn Alignment="left" Caption="發信內文" Editor="textarea" EditorOptions="height:120" FieldName="AssignMail" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="570" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
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
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                                </Columns>
                            </JQTools:JQDefault>
                            <JQTools:JQValidate ID="JQValidateAssignLogs" runat="server" BindingObjectID="DFJobAssignLogs" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="AssignTime" RemoteMethod="True" ValidateMessage="請選擇日期！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="RecID" RemoteMethod="True" ValidateMessage="請選擇招募！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="AssignID" RemoteMethod="True" ValidateMessage="請選擇派任狀態！" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>
                        </JQTools:JQDialog>


                    </fieldset>
                </JQTools:JQDialog>

                        <JQTools:JQDialog ID="JQDialogAssignLogsMore" runat="server" BindingObjectID="DFJobAssignLogsMore" DialogLeft="120px" DialogTop="45px" Title="批次派任作業" Width="750px" ShowSubmitDiv="True">
                            <JQTools:JQDataForm ID="DFJobAssignLogsMore" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="Rec_JobAssignLogs" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="" RemoteName="_HRM_REC_User_Management2.REC_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnLoadSuccess="OnLoadAssignLogsMore" OnApplied="OnAppliedAssignLogsMore" OnApply="OnApplyAssignLogsMore">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="派任狀態" Editor="infocombobox" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'_HRM_REC_User_Management2.infoREC_ZAssignStep',tableName:'infoREC_ZAssignStep',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnSelectAssignIDMore,panelHeight:200" FieldName="AssignID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" Format="" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="派任日期" Editor="datebox" EditorOptions="" FieldName="AssignTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="招募人員" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ConsultantName',remoteName:'_HRM_REC_User_Management2.infoRecID',tableName:'infoRecID',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="RecID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="當日到離" Editor="checkbox" FieldName="IsSamDay" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶-職缺" Editor="inforefval" EditorOptions="title:'請選擇',panelWidth:445,remoteName:'_HRM_REC_User_Management2.infoHRM_COMPANY_JOB',tableName:'infoHRM_COMPANY_JOB',columns:[{field:'COMPANY_ABBR',title:'客戶簡稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'COMPANY_JOB_CNAME',title:'職缺名稱',width:280,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'CustID',value:'COMPANY_ID'}],whereItems:[],valueField:'COMPANY_JOB_ID',textField:'COMPANY_JOB_CNAME',valueFieldCaption:'職缺流水號',textFieldCaption:'職缺名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="320" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="推薦職缺" Editor="text" FieldName="RecommendCust" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="190" />                                    
                                    <JQTools:JQFormColumn Alignment="left" Caption="派任備註" Editor="textarea" EditorOptions="height:80" FieldName="AssignContent" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="600" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="是否發信" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="bAssignMail" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="35" />                               
                                     <JQTools:JQFormColumn Alignment="left" Caption="發信內文" Editor="textarea" EditorOptions="height:120" FieldName="AssignMail" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="570" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                </Columns>
                            </JQTools:JQDataForm>
                            <JQTools:JQDefault ID="JQDefaultAssignLogsMore" runat="server" BindingObjectID="DFJobAssignLogsMore" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="AssignTime" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="LastUpdateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                                </Columns>
                            </JQTools:JQDefault>
                            <JQTools:JQValidate ID="JQValidateAssignLogsMore" runat="server" BindingObjectID="DFJobAssignLogsMore" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="AssignTime" RemoteMethod="True" ValidateMessage="請選擇日期！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="RecID" RemoteMethod="True" ValidateMessage="請選擇招募！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="AssignID" RemoteMethod="True" ValidateMessage="請選擇派任狀態！" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>
                        </JQTools:JQDialog>


                <JQTools:JQDialog ID="JQDialogApplyJob" runat="server" BindingObjectID="dataFormMaster9" Title="應徵紀錄" Width="835px" DialogLeft="80px" DialogTop="10px" Height="460px" EditMode="Dialog" ShowSubmitDiv="False">
                    <JQTools:JQDataForm ID="dataFormMaster9" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="REC_User" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="7" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="_HRM_REC_User_Management2.REC_User" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" ParentObjectID="dataFormMaster" ChainDataFormID="">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="中文姓名" Editor="text" FieldName="NameC" MaxLength="20" Span="1" Width="70" NewRow="False" ReadOnly="True" RowSpan="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="MobileNo" MaxLength="0" Span="1" Width="90" ReadOnly="True" NewRow="False" RowSpan="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="郵件信箱" Editor="text" FieldName="Email" MaxLength="0" ReadOnly="True" Span="1" Visible="True" Width="250" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <fieldset>
                        <legend></legend>
                        <JQTools:JQDataGrid ID="DGApplyJobLogs" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="MyFavJobs" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" Height="270px" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="5,10,15,20" PageSize="5" Pagination="False" ParentObjectID="" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="_HRM_REC_User_Management2.MyFavJobs" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="750px">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="center" Caption="應徵日期" Editor="datebox" FieldName="UpdateDate" MaxLength="0" Sortable="True" Visible="true" Width="85">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" EditorOptions="" FieldName="COMPANY_FrontName" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="150" FormatScript="">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="COMPANY_JOB_FrontName" MaxLength="0" Sortable="True" Visible="true" Width="150" FormatScript="OpenJobTab">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="招募人員" Editor="text" EditorOptions="" FieldName="ConsultantName" Format="" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="職缺縣市" Editor="text" FieldName="DutyAreaClass" Format="" MaxLength="800" Visible="true" Width="90" />
                                <JQTools:JQGridColumn Alignment="left" Caption="職缺地點" Editor="text" FieldName="DutyAreas" Format="" MaxLength="2147483647" Visible="True" Width="90" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                                <JQTools:JQGridColumn Alignment="center" Caption="有效性" Editor="checkbox" FieldName="IsActive" Format="" Visible="True" Width="45" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                                <JQTools:JQGridColumn Alignment="left" Caption="JobId" Editor="text" FieldName="JobId" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                            </RelationColumns>
                            <QueryColumns>
                            <JQTools:JQQueryColumn AndOr="and" Caption="應徵日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="JobDate1" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="85" RowSpan="0" Span="0" />
                            <JQTools:JQQueryColumn AndOr="and" Caption=" ～ " Condition="=" DataType="datetime" Editor="datebox" FieldName="JobDate2" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="85" RowSpan="0" Span="0" />
                           <JQTools:JQQueryColumn AndOr="and" Caption="招募人員" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ConsultantName',remoteName:'_HRM_REC_User_Management2.infoServiceConsultants',tableName:'infoServiceConsultants',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:150" FieldName="JobConsultants" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />    
                                <JQTools:JQQueryColumn AndOr="and" Caption="客戶名稱" Condition="%" DataType="string" Editor="text" FieldName="sCust" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="85" RowSpan="0" Span="0" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="職缺名稱" Condition="%%" DataType="string" Editor="text" FieldName="sName" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="85" />
                            </QueryColumns>
                        </JQTools:JQDataGrid>
                    </fieldset>
                </JQTools:JQDialog>


    </form>
                            
</body>
</html>
