<%@ Page Language="C#" AutoEventWireup="true" CodeFile="REC_User.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
      <script type="text/javascript">
          $(function () {
              $("#NameC_Query").attr("placeholder", "輸入姓名/身分證/履歷編號");
              $("input, select, textarea").focus(function () {
                  $(this).css("background-color", "lightyellow");
              });
              $("input, select, textarea").blur(function () {
                  $(this).css("background-color", "white");
              });
          });

          $(document).ready(function () {

              ////--------------查詢條件組合-----------------------------------------------------------------
              var Age1 = $('#Age1_Query').closest('td');
              var Age2 = $('#Age2_Query').closest('td').children();
              Age1.append("&nbsp;&nbsp;～&nbsp;&nbsp;").append(Age2);
              var FullSearch = $('#sFullSearch_Query').closest('td');
              FullSearch.append("&nbsp;(★請輸入關鍵字:以 , 區隔)");//.append(AndOr);


              var spi = "&nbsp;&nbsp;";
              ////--------------中文姓名+國籍....字串結合-----------------------------------------------------------------
              var NameC = $('#dataFormMasterNameC').closest('td');
              var Country = $('#dataFormMasterCountry').closest('td').children();
              NameC.append(spi + "國籍").append(Country);


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
              ////--------------家庭成員、同居成員...-----------------------------------------------------------------
              var FamilyCount = $('#dataFormMaster2FamilyCount').closest('td');
              FamilyCount.append("人");
              var CohabitCount = $('#dataFormMaster2CohabitCount').closest('td');
              CohabitCount.append("人");

              ////工作地點+文字
              //var DutyAreasIDs = $('#dataFormMaster3DutyAreasIDs').closest('td').find(".options-panel");
              ////var DutyAreasOther = $('#dataFormMaster3DutyAreasOther').closest('td');
              ////DutyAreasOther.insertAfter(DutyAreasIDs);//或者想加其他元件

              //$('<input id="ddd type="text">').insertAfter($(DutyAreasIDs[10]))

              $("#dataGridView").datagrid({
                  singleSelect: true,
                  selectOnCheck: false,
                  checkOnSelect: false
              });

          });

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

          function DFLoadSuccess() {
             
          }

          function queryGrid(dg) {//查詢後添加固定條件
              if ($(dg).attr('id') == 'dataGridView') {
                  UserQuery();
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
                  url: '../handler/jqDataHandle.ashx?RemoteName=sREC_User.HUT_User', //連接的Server端，command
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
              if (rowData.blacklist == true) {//黑名單=>灰色
                  return "<div style='background-color:#D3D3D3;'> " + val + "</div>";
              } else {
                  return val;
              }
          }
          function UserQuery() {
              var NameC = $('#NameC_Query').val();//人才姓名/身分證/履歷編號
              var Gender = $('#Gender_Query').combobox('getValue');//性別
              var Age1 = $('#Age1_Query').val();//年齡範圍
              var Age2 = $('#Age2_Query').val();
              var EduID = $('#EduID_Query').combobox('getValue');//最高學歷
              var DutyAreas = $('#DutyAreas_Query').val();//工作地點
              var ProLicenses = $('#ProLicenses_Query').val();//證照資格
              var JobCompany = $('#JobCompany1_Query').val();//公司名稱
              var CurAddress = $('#CurAddress_Query').val();//現居地址

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
                  url: '../handler/jqDataHandle.ashx?RemoteName=sREC_User.HUT_User', //連接的Server端，command
                  data: "mode=method&method=" + "RECUsersQuery" + "&parameters=" + encodeURIComponent(NameC + "*" + Gender + "*" + Age1 + "*" + Age2 + "*" + EduID + "*" + DutyAreas + "*" + CurAddress + "*" + ProLicenses + "*" + JobCompany + "*" + tString + "*" + Mark), //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
          //var waitA = false;
          function GVOnloadSuccess() {
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

          
          //--------------派任作業(紀錄)-----------------------------------------------------------------------------------------------   

          function AssignLink(value, row, index) {
              if (value != null)
                return "";//"<a href='javascript: void(0)' onclick='LinkAssign(" + index + ");' style='color:red;'>派任 / " + value + "</a>";
              else return "<a href='javascript: void(0)' onclick='LinkAssign(" + index + ");' style='color:blue;'>新增</a>";
          }
          // open推薦畫面
          function LinkAssign(index) {
              $("#dataGridView").datagrid('selectRow', index);
              openForm('#JQDialogJobAssignLogs', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
          }
          // 批次派任作業
          function AssignAdd() {
              //$("#dataGridView").datagrid('selectRow', index);
              //openForm('#JQDialogJobAssignLogs', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
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
          //---------------------------------------派任作業權限控制-----------------------------------------------------
          function AssignUpdateRow(rowData) {
          }

          //控制是否可以刪除 
          function AssignDeleteRow(rowData) {
            
          }

          //--------------------------------寫入招募系統--------------------------------
          function RecruitLink(value, row, index) {
              if (row.IsRec == true) {//已寫
                  return $('<a>', { href: "#", theData: row.UserID }).linkbutton({ text: "<img src=img/ok.png></a><b><div style=\"color:Red\"></div></b>", plain: true })[0].outerHTML;
              } else if (row.Country == "1" && (row.PID == "" || row.Birthday == null)) {//

                  return "<a style='color:red' title='身分證號 與 出生日期必填。'>資料不足</a>";

              } else if (row.Country != "1" && (row.ResidentID == "" || row.Birthday == null)) {//

                  return "<a style='color:red' title='居留證號 與 出生日期必填。'>資料不足</a>";
              }
              else {
                  return $('<a>', { href: 'javascript:void(0)', name: 'RecruitLink', onclick: 'InsertRecruit(' + index + ')' }).linkbutton({ plain: false, text: '寫入' })[0].outerHTML;
              }
          }
          function InsertRecruit(index) {
              $("#dataGridView").datagrid('selectRow', index);
              var row = $('#dataGridView').datagrid('getSelected'); //取得當前主檔中選中的那個Data

            
                    
              if (row.PID == "" || row.Birthday == "") {
                  alert('身分證號、出生日期等不可以空白！');
                  } else {
                      var pre = confirm("確定寫入招募系統?");
                      if (pre == true) {
                          if (row != null) {
                              var cnt;
                              $.ajax({
                                  type: "POST",
                                  url: '../handler/jqDataHandle.ashx?RemoteName=sREC_User.HUT_User', //連接的Server端，command
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
              var url = "../JB_ADMIN/REPORT/RecUser/RecommendReport.aspx?FileName=" + FileName + "&UserID=" + UserID + "&JobID=" + JobID + "&AutoKey=" + AutoKey + "&sDyItem=" + sDyItem;

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
         


      </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sREC_User.REC_User" runat="server" AutoApply="True"
                DataMember="REC_User" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title=" " AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="1055px" OnLoadSuccess="GVOnloadSuccess">
                <Columns>
<%--                                 <JQTools:JQGridColumn Alignment="center" Caption="照片" Editor="text" FieldName="PhotoFile" Format="Image,Folder:../JQWebClient/Files/REC/Users,Height:30" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35" FormatScript="" />--%>
                                <JQTools:JQGridColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="68">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="履歷表" Editor="text" FieldName="LinkResume" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="48" FormatScript="LinkResume"></JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="面談紀錄" Editor="text" FieldName="ContactDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" FormatScript=""></JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="身分證號" Editor="text" FieldName="PID" Frozen="False" IsNvarChar="False" MaxLength="20" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="71" EditorOptions="">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="中文姓名" Editor="text" FieldName="NameC" MaxLength="20" Width="55" EditorOptions="" Visible="True" Sortable="False" FormatScript="Formatblacklist" />
                                <JQTools:JQGridColumn Alignment="center" Caption="派任作業" Editor="" FieldName="sAssign" MaxLength="0" Width="55" FormatScript="AssignLink" />
                                <JQTools:JQGridColumn Alignment="center" Caption="轉招募系統" Editor="text" FieldName="IsRec" FormatScript="RecruitLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="性別" Editor="text" FieldName="GenderText" Frozen="False" IsNvarChar="False" MaxLength="30" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="37">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="年齡" Editor="text" FieldName="iAge" MaxLength="0" Width="37" Sortable="False" />
                                <JQTools:JQGridColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="MobileNo" MaxLength="0" Width="79" />
                                <JQTools:JQGridColumn Alignment="left" Caption="工作地點" Editor="text" FieldName="DutyAreas" MaxLength="0" Width="125" />
                                <JQTools:JQGridColumn Alignment="left" Caption="現居地址.." Editor="text" FieldName="sCurAddress" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="最高學歷" Editor="text" FieldName="EduName" MaxLength="20" Width="55" EditorOptions="" Visible="True" Sortable="False" />
                                <JQTools:JQGridColumn Alignment="center" Caption="更新人員" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55" EditorOptions="">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="更新日期" Editor="datebox" FieldName="LastUpdateDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" ItemType="easyui-linkbutton" Text="派任作業" Visible="True" OnClick="AssignAdd" Icon="icon-add" />
                </TooItems>
                <QueryColumns>
                               <JQTools:JQQueryColumn AndOr="and" Caption="人才搜尋" Condition="%" DataType="string" Editor="text" FieldName="NameC" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="155" DefaultValue="" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="性別" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'不拘',selected:'true'},{value:'0',text:'女',selected:'false'},{value:'1',text:'男',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:110" FieldName="Gender" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="年齡範圍" Condition="=" DataType="number" Editor="numberbox" FieldName="Age1" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="1" Width="50" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="=" DataType="number" Editor="numberbox" FieldName="Age2" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="公司名稱" Condition="%" DataType="string" Editor="text" FieldName="JobCompany1" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="140" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="專業證照" Condition="%" DataType="string" Editor="text" FieldName="ProLicenses" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="1" Width="155" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="最高學歷" Condition="%" DataType="string" Editor="infocombobox" FieldName="EduID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'sHUTUser.infoHUT_ZEduLevel',tableName:'infoHUT_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:110" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="工作地點" Condition="%" DataType="string" Editor="text" FieldName="DutyAreas" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="2" Width="140" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="現居地址" Condition="%" DataType="string" Editor="text" FieldName="CurAddress" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="140" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="全文檢索" Condition="%" DataType="string" Editor="text" FieldName="sFullSearch" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="5" Width="550" />
                                 <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" DefaultValue="and" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:65,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'and',value:'and'},{text:'or',value:'or'}]" FieldName="AndOr" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="2" Width="70" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="履歷維護" DialogLeft="9px" DialogTop="1px" Width="1050px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="REC_User" HorizontalColumnsCount="6" RemoteName="sREC_User.REC_User" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" ChainDataFormID="dataFormMaster2" OnApplied="DFOnApplied" OnLoadSuccess="DFLoadSuccess" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Format="" Width="180" Visible="False" NewRow="False" maxlength="0" Span="1" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="中文姓名" Editor="text" FieldName="NameC" Format="" Width="90" NewRow="True" maxlength="0" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="MobileNo" Format="" Width="110" maxlength="0" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="現居地址" Editor="infocombobox" FieldName="Addr_Country" Format="" maxlength="0" Width="80" EditorOptions="valueField:'Country',textField:'Country',remoteName:'sERP_Customer_Normal_Customer.Country',tableName:'Country',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:Addr_Country_OnSelect,panelHeight:200" Span="3" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="Addr_City" Format="" maxlength="0" Width="80" EditorOptions="valueField:'City',textField:'City',remoteName:'sERP_Customer_Normal_Customer.City',tableName:'City',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:Addr_City_OnSelect,panelHeight:200" Span="1" NewRow="False" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="Addr_Desc" Format="" Width="235" maxlength="0" Span="1" NewRow="False" ReadOnly="False" Visible="True" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'sREC_User.infoREC_ZCountry',tableName:'infoREC_ZCountry',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:120" FieldName="Country" MaxLength="0" NewRow="True" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="郵件信箱" Editor="text" FieldName="Email" MaxLength="0" NewRow="True" Span="2" Visible="True" Width="250" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話號碼" Editor="text" FieldName="CurTelNO" maxlength="0" Width="110" Span="1" NewRow="False" Visible="True" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="交通工具" Editor="infooptions" FieldName="TrafficIDs" Format="" maxlength="0" NewRow="False" Span="3" Visible="True" Width="180" EditorOptions="title:'交通工具',panelWidth:390,remoteName:'sREC_User.infoREC_ZTraffic',tableName:'infoREC_ZTraffic',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectTrafficIDs,selectOnly:false,items:[]" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="血型" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'sREC_User.infoREC_ZBloodType',tableName:'infoREC_ZBloodType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:120" FieldName="BloodType" MaxLength="0" NewRow="True" Span="1" Visible="True" Width="71" />
                         <JQTools:JQFormColumn Alignment="left" Caption="LineID" Editor="text" FieldName="LineID" NewRow="False" Visible="True" Width="140" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="人才照片" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'Files/Rec/Users',showButton:true,showLocalFile:false,fileSizeLimited:'1000'" FieldName="PhotoFile" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最高學校" Editor="text" FieldName="SchoolName1" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="250" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最高學歷" Editor="infocombobox" FieldName="EduID1" MaxLength="0" Span="1" Width="110" NewRow="False" ReadOnly="False" Visible="True" RowSpan="1" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'sHUTUser.infoHUT_ZEduLevel',tableName:'infoHUT_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:EduID1Select,panelHeight:120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最高科系" Editor="text" FieldName="Department1" MaxLength="0" NewRow="False" Span="1" Width="250" ReadOnly="False" Visible="True" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最高狀態" Editor="infocombobox" FieldName="GradeStatus1" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="80" RowSpan="1" EditorOptions="items:[{value:'畢業',text:'畢業',selected:'true'},{value:'肄業',text:'肄業',selected:'false'},{value:'就學中',text:'就學中',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="次高學校" Editor="text" FieldName="SchoolName2" MaxLength="0" Span="2" Width="250" NewRow="True" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="次高學歷" Editor="infocombobox" FieldName="EduID2" MaxLength="0" Span="1" Width="110" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'sHUTUser.infoHUT_ZEduLevel',tableName:'infoHUT_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:EduID2Select,panelHeight:120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="次高科系" Editor="text" FieldName="Department2" MaxLength="0" Span="1" Width="250" NewRow="False" ReadOnly="False" Visible="True" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="次高狀態" Editor="infocombobox" FieldName="GradStatus2" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="80" RowSpan="1" EditorOptions="items:[{value:'畢業',text:'畢業',selected:'true'},{value:'肄業',text:'肄業',selected:'false'},{value:'就學中',text:'就學中',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:130" />

                        <JQTools:JQFormColumn Alignment="left" Caption="EduName1" Editor="text" FieldName="EduName1" maxlength="0" NewRow="True" Span="1" Visible="False" Width="80" ReadOnly="False" RowSpan="1" />

                        <JQTools:JQFormColumn Alignment="left" Caption="EduName2" Editor="text" FieldName="EduName2" NewRow="False" Span="1" Visible="False" Width="80" MaxLength="0" ReadOnly="False" RowSpan="1" />

                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDataGrid ID="DGUserCareer" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="REC_UserCareer" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogUserCareer" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sREC_User.REC_User" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="940px">
                    <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="JobCompany" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="170">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="職務" Editor="text" FieldName="JobTitle" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="185" FormatScript="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="工作期間" Editor="infocombobox" FieldName="JobPeriodS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="52" EditorOptions="valueField:'sDate',textField:'sDate',remoteName:'sREC_User.infoJobPeriod',tableName:'infoJobPeriod',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption=" " Editor="infocombobox" FieldName="JobPeriodE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="52" EditorOptions="valueField:'sDate',textField:'sDate',remoteName:'sREC_User.infoJobPeriod2',tableName:'infoJobPeriod2',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200">
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
                <JQTools:JQDataForm ID="dataFormMaster3" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="REC_User" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="6" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sREC_User.REC_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApplied="DFOnApplied" ChainDataFormID="" OnLoadSuccess="DFLoadSuccess">
                    <Columns>

                        <JQTools:JQFormColumn Alignment="left" Caption="就業狀況" Editor="checkbox" FieldName="IsOnDuty" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="住宿需求" Editor="checkbox" FieldName="IsNeedDorm" MaxLength="0" NewRow="False" Span="1" Visible="True" Width="30" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="進修意願" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsEducation" maxlength="0" NewRow="False" Span="1" Visible="True" Width="30" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="right" Caption="期望薪資" Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="ExpectedSalary" NewRow="False" Span="1" Visible="True" Width="60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="專業證照" Editor="text" FieldName="ProLicenses" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="320" />


                        <JQTools:JQFormColumn Alignment="left" Caption="工作時間" Editor="infooptions" EditorOptions="title:'交通工具',panelWidth:210,remoteName:'sREC_User.infoREC_ZDutyTimes',tableName:'infoREC_ZDutyTimes',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="DutyTimesIDs" Format="" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作工具" Editor="infooptions" EditorOptions="title:'工作工具',panelWidth:310,remoteName:'sREC_User.infoREC_ZDutyTools',tableName:'infoREC_ZDutyTools',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectDutyTools,selectOnly:false,items:[]" FieldName="DutyToolsIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="無塵衣" Editor="infooptions" EditorOptions="title:'無塵衣',panelWidth:425,remoteName:'sREC_User.infoREC_ZCleanClothes',tableName:'infoREC_ZCleanClothes',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectCleanClothes,selectOnly:false,items:[]" FieldName="CleanClothesIDs" Format="" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作型態" Editor="infooptions" EditorOptions="title:'工作型態',panelWidth:400,remoteName:'sREC_User.infoREC_ZDutyActTypes',tableName:'infoREC_ZDutyActTypes',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectDutyActTypesIDs,selectOnly:false,items:[]" FieldName="DutyActTypesIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作班別" Editor="infooptions" EditorOptions="title:'工作班別',panelWidth:390,remoteName:'sREC_User.infoREC_ZDutyClasses',tableName:'infoREC_ZDutyClasses',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="DutyClassesIDs" Format="" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作加班" Editor="infooptions" EditorOptions="title:'加班意願',panelWidth:280,remoteName:'sREC_User.infoREC_ZOverTimes',tableName:'infoREC_ZOverTimes',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectOverTimesIDs,selectOnly:false,items:[]" FieldName="OverTimesIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作地點" Editor="infooptions" EditorOptions="title:'工作地點',panelWidth:850,remoteName:'sREC_User.infoREC_ZDutyAreas',tableName:'infoREC_ZDutyAreas',valueField:'ID',textField:'Contents',columnCount:10,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="DutyAreasIDs" Format="" NewRow="True" Span="6" Visible="True" Width="90" MaxLength="0" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="其它地點" Editor="text" FieldName="DutyAreasOther" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="360" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EduName1" Editor="text" FieldName="EduName1" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EduName2" Editor="text" FieldName="EduName2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />


                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDataForm ID="dataFormMaster2" runat="server" AlwaysReadOnly="False" ChainDataFormID="dataFormMaster3" Closed="False" ContinueAdd="False" DataMember="REC_User" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="7" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="DFOnApplied" RemoteName="sREC_User.REC_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Format="" maxlength="0" Span="1" Visible="False" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="身分證號" Editor="text" FieldName="PID" Format="" maxlength="0" Width="80" Span="1" Visible="True" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出生日期" Editor="datebox" FieldName="Birthday" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="85" />                        
                        <JQTools:JQFormColumn Alignment="left" Caption="居留證號" Editor="text" FieldName="ResidentID" Format="" maxlength="0" Width="80" Span="1" Visible="True" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="居留事由" Editor="text" FieldName="ResidenceDesc" MaxLength="0" Span="2" Width="180" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="有效日" Editor="datebox" FieldName="ResidenceSDate" maxlength="0" Width="85" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="datebox" FieldName="ResidenceEDate" MaxLength="0" NewRow="True" Span="1" Width="85" />
                        <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:120,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:true,items:[{text:'女',value:'false'},{text:'男',value:'true'}]" FieldName="Gender" MaxLength="20" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="55" />
                        <JQTools:JQFormColumn Alignment="left" Caption="身高" Editor="numberbox" FieldName="Tall" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="體重" Editor="numberbox" FieldName="Weight" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="婚姻狀況" Editor="infocombobox" EditorOptions="items:[{value:'未婚',text:'未婚',selected:'false'},{value:'已婚',text:'已婚',selected:'false'},{value:'分居',text:'分居',selected:'false'},{value:'離婚',text:'離婚',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="Marriage" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="85" />
                          <JQTools:JQFormColumn Alignment="left" Caption="現居地" Editor="infocombobox" EditorOptions="items:[{value:'自有',text:'自有',selected:'false'},{value:'租屋',text:'租屋',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="HouseOwnStatus" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="65" />                      
                        <JQTools:JQFormColumn Alignment="left" Caption="兵役狀況" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'sREC_User.infoREC_ZMilitaryService',tableName:'infoREC_ZMilitaryService',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:90" FieldName="MilitaryServiceIDs" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="85" />
                        <JQTools:JQFormColumn Alignment="left" Caption="退伍年月" Editor="text" FieldName="MilitaryYM" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="45" />
                        <JQTools:JQFormColumn Alignment="left" Caption="近視狀態" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'sREC_User.infoREC_ZShortsight',tableName:'infoREC_ZShortsight',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="ShortsightIDs" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="散光狀態" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'sREC_User.infoREC_ZAstigm',tableName:'infoREC_ZAstigm',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="AstigmIDs" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="辨色能力" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'sREC_User.infoREC_ZColorVision',tableName:'infoREC_ZColorVision',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="ColorVisionIDs" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        
                        <JQTools:JQFormColumn Alignment="left" Caption="家庭成員" Editor="infooptions" EditorOptions="title:'家庭成員',panelWidth:374,remoteName:'sREC_User.infoREC_ZFamily',tableName:'infoREC_ZFamily',valueField:'ID',textField:'Contents',columnCount:8,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="FamilyIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總共" Editor="numberbox" FieldName="FamilyCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="20" />
                        <JQTools:JQFormColumn Alignment="left" Caption="吸菸" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'sREC_User.infoREC_ZSmoking',tableName:'infoREC_ZSmoking',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="SmokingIDs" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="樁腳" Editor="checkbox" FieldName="IsPilefoot" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="45" />
                        <JQTools:JQFormColumn Alignment="left" Caption="推薦樁腳" Editor="infocombobox" EditorOptions="valueField:'MemberID',textField:'NameC',remoteName:'sREC_User.infoPFMemberID',tableName:'infoPFMemberID',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="PFMemberID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        
                        <JQTools:JQFormColumn Alignment="left" Caption="同居成員" Editor="infooptions" EditorOptions="title:'同居成員',panelWidth:374,remoteName:'sREC_User.infoREC_ZCohabits',tableName:'infoREC_ZCohabits',valueField:'ID',textField:'Contents',columnCount:8,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="CohabitIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總共" Editor="numberbox" FieldName="CohabitCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="20" />
                        <JQTools:JQFormColumn Alignment="left" Caption="身份別" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'sREC_User.infoREC_ZIdentity',tableName:'infoREC_ZIdentity',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:150" FieldName="IdentityID" Format="" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="面試來源" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'sREC_User.infoREC_ZInfoSource',tableName:'infoREC_ZInfoSource',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="InfoSource" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="190" />                        
                        <JQTools:JQFormColumn Alignment="left" Caption="懷孕" Editor="checkbox" FieldName="IsPregnancy" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="45" />
                        <JQTools:JQFormColumn Alignment="left" Caption="吃檳榔" Editor="checkbox" FieldName="IsBetelnut" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="45" />
                        <JQTools:JQFormColumn Alignment="left" Caption="黑名單" Editor="checkbox" FieldName="blacklist" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="45" />
                        <JQTools:JQFormColumn Alignment="left" Caption="健康評量" Editor="infooptions" EditorOptions="title:'同居成員',panelWidth:900,remoteName:'sREC_User.infoREC_ZHealthStatus',tableName:'infoREC_ZHealthStatus',valueField:'ID',textField:'Contents',columnCount:20,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectHealthStatusIDs,selectOnly:false,items:[]" FieldName="HealthStatusIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="8" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="經濟壓力" Editor="infooptions" EditorOptions="title:'經濟壓力',panelWidth:600,remoteName:'sREC_User.infoREC_ZEcoPressure',tableName:'infoREC_ZEcoPressure',valueField:'ID',textField:'Contents',columnCount:20,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectEcoPressureIDs,selectOnly:false,items:[]" FieldName="EcoPressureIDs" Format="" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="推薦職缺" Editor="text" FieldName="RecommendCust" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="480" />
                        <JQTools:JQFormColumn Alignment="left" Caption="推薦內容" Editor="textarea" EditorOptions="height:40" FieldName="RecommendContent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="7" Visible="True" Width="882" />
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
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="TrafficIDs" RemoteMethod="True" ValidateMessage="請選擇交通工具！" ValidateType="None" />
<%--                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SchoolName1" RemoteMethod="True" ValidateMessage="最高學校不可空白！" ValidateType="None" />--%>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EduID1" RemoteMethod="True" ValidateMessage="請選擇最高學歷！" ValidateType="None" />
<%--                        <JQTools:JQValidateColumn CheckNull="True" FieldName="GradeStatus1" RemoteMethod="True" ValidateMessage="請選擇最高狀態！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Department1" RemoteMethod="True" ValidateMessage="最高科系不可空白！" ValidateType="None" />--%>
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDataGrid ID="DGContactRecord" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="REC_UserContactRecord" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogContactRecord" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnDeleted="OnDeletedContactRecord" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sREC_User.REC_User" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="940px" OnUpdate="UserContactUpdateRow" OnDelete="UserContactDeleteRow">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="面談日期" Editor="datebox" FieldName="ContactDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="面談內容" Editor="text" FieldName="Notes" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="580">
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
                <JQTools:JQValidate ID="validateMaster2" runat="server" BindingObjectID="dataFormMaster2" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="False" FieldName="PID" RemoteMethod="True" ValidateMessage="身分證號格式不對！" ValidateType="IdCard" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQValidate ID="validateMaster3" runat="server" BindingObjectID="dataFormMaster3" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DutyTimesIDs" RemoteMethod="True" ValidateMessage="請選擇工作時間！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DutyClassesIDs" RemoteMethod="True" ValidateMessage="請選擇工作班別！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DutyAreasIDs" RemoteMethod="True" ValidateMessage="請選擇工作地點！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDialog ID="JQDialogUserCareer" runat="server" BindingObjectID="DFUserCareer" DialogLeft="100px" DialogTop="20px" Title="工作經驗維護" Width="900px">
                    <JQTools:JQDataForm ID="DFUserCareer" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="REC_UserCareer" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedCareer" OnApply="OnApplyCareer" OnLoadSuccess="OnLoadSuccessCareer" ParentObjectID="dataFormMaster" RemoteName="sREC_User.REC_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="JobCompany" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="210" />
                            <JQTools:JQFormColumn Alignment="left" Caption="職務 " Editor="text" FieldName="JobTitle" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="210" />
                            <JQTools:JQFormColumn Alignment="left" Caption="工作期間" Editor="infocombobox" EditorOptions="valueField:'sDate',textField:'sDate',remoteName:'sREC_User.infoJobPeriod',tableName:'infoJobPeriod',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectDutyDate,panelHeight:200" FieldName="JobPeriodS" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="right" Caption="薪資待遇" Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="JobSalary" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                            <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="JobPeriodE" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" EditorOptions="valueField:'sID',textField:'sDate',remoteName:'sREC_User.infoJobPeriod2',tableName:'infoJobPeriod2',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />                            
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
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="JobSalary" RemoteMethod="True" ValidateMessage="薪資待遇不可空白！" ValidateType="None" />
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
                    <JQTools:JQDataForm ID="DFContactRecord" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="REC_UserContactRecord" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnInsertedContactRecord" ParentObjectID="dataFormMaster" RemoteName="sREC_User.REC_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
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
            </JQTools:JQDialog>
        </div>

                <JQTools:JQDialog ID="JQDialogJobAssignLogs" runat="server" BindingObjectID="dataFormMaster8" Title="派任作業" Width="900px" DialogLeft="36px" DialogTop="20px" Height="440px" EditMode="Dialog" ShowSubmitDiv="False">
                    <JQTools:JQDataForm ID="dataFormMaster8" runat="server" AlwaysReadOnly="False" ChainDataFormID="" Closed="False" ContinueAdd="False" DataMember="REC_User" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="7" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sREC_User.REC_User" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="中文姓名" Editor="text" FieldName="NameC" MaxLength="20" Span="1" Width="70" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <fieldset>
                        <legend>派任紀錄</legend>


                        <JQTools:JQDataGrid ID="DGJobAssignLogs" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="Rec_JobAssignLogs" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogAssignLogs" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnUpdate="AssignUpdateRow" PageList="5,10,15,20" PageSize="5" Pagination="False" ParentObjectID="dataFormMaster8" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sREC_User.REC_User" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="100%" OnDeleted="OnDeletedAssignLogs" OnDelete="AssignDeleteRow" Height="310px">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱-職缺" Editor="infocombobox" FieldName="JobID" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="190" EditorOptions="valueField:'JobID',textField:'sJobName',remoteName:'sHUTUser.HUT_Job',tableName:'HUT_Job',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="派任狀態" Editor="infocombobox" FieldName="AssignID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'sREC_User.infoREC_ZAssignStep',tableName:'infoREC_ZAssignStep',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="派任日期" Editor="datebox" EditorOptions="" FieldName="AssignTime" Format="yyyy/mm/dd" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="派任人員" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="RecID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="派任備註" Editor="text" FieldName="AssignContent" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="300">
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
                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                            </RelationColumns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />

                            </TooItems>
                        </JQTools:JQDataGrid>
                        <JQTools:JQDialog ID="JQDialogAssignLogs" runat="server" BindingObjectID="DFJobAssignLogs" DialogLeft="120px" DialogTop="45px" Title="派任紀錄維護" Width="750px" ShowSubmitDiv="True">
                            <JQTools:JQDataForm ID="DFJobAssignLogs" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="Rec_JobAssignLogs" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster8" RemoteName="sREC_User.REC_User" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApplied="OnAppliedAssignLogs">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶-職缺" Editor="inforefval" EditorOptions="title:'請選擇',panelWidth:445,remoteName:'sHUTUser.HUT_Job',tableName:'HUT_Job',columns:[{field:'CustName',title:'客戶簡稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'JobName',title:'職缺名稱',width:280,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'HunterID',value:'HunterID'}],whereItems:[],valueField:'JobID',textField:'sJobName',valueFieldCaption:'JobID',textFieldCaption:'客戶-職缺',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="320" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="招募人員" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:180" FieldName="RecID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="派任狀態" Editor="infocombobox" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'sREC_User.infoREC_ZAssignStep',tableName:'infoREC_ZAssignStep',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="AssignID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" Format="" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="派任日期" Editor="datebox" EditorOptions="" FieldName="AssignTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="派任備註" Editor="textarea" EditorOptions="height:80" FieldName="AssignContent" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="570" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
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
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="AssignTime" RemoteMethod="True" ValidateMessage="請選擇日期！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="RecID" RemoteMethod="True" ValidateMessage="請選擇招募！" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>
                            <JQTools:JQAutoSeq ID="JQAutoAssignLogs" runat="server" BindingObjectID="DFJobAssignLogs" FieldName="AutoKey" NumDig="1" />
                        </JQTools:JQDialog>


                    </fieldset>
                </JQTools:JQDialog>

    </form>
                            
</body>
</html>
