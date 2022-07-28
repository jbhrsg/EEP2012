<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_COMPANY_JOBFront.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script type="text/javascript">
         $(function () {
             //$("#FrontName_Query").attr("placeholder", "輸入職缺名稱/代號");
             $("input, select, textarea").focus(function () {
                 $(this).css("background-color", "lightyellow");
             });
             $("input, select, textarea").blur(function () {
                 $(this).css("background-color", "white");
             });

             //需求條件加上(不可刊登)的欄位
             var HideFieldName = ['JOBConditionN'];
             var FormName = '#dataFormMaster';

             $.each(HideFieldName, function (index, fieldName) {
                 var Name = $(FormName + fieldName);
                 $('<br/><span id="t1" style="color: rgb(138, 43, 226);">*不可刊登</span>').insertAfter(Name);
                 //$(FormName + fieldName).closest('td').css("color", "rgb(138, 43, 226)");
                 $(FormName + fieldName).closest('td').prev('td').css("color", "rgb(138, 43, 226)");//改變td前面文字顏色
             });

         });

         $(document).ready(function () {

             //-----------------------------------前台公告職缺-職缺薪資顯示----------------------------
             var Salary1 = $('#dataFormMasterJOBSalary1').closest('td');
             var Salary2 = $('#dataFormMasterJOBSalary2').closest('td').children();
             Salary1.append("&nbsp;～").append(Salary2);

             var PhotoFile = $('#infoFileUploaddataFormMasterJobPhotoFile').closest('td');
             PhotoFile.append("&nbsp;(1:1，例1080*1080)");
             


             //-------------履歷的應徵職缺,職缺代號連結導入---------------------------------------
             setTimeout(function () {
                 var parameter = Request.getQueryStringByName("JobId");
                 if (parameter != "") {
                     $("#COMPANY_JOB_ID_Query").val(parameter);
                     UserQuery();
                 }
             }, 2000);


         });

         function OnLoadMaster() {
             if (getEditMode($("#dataFormMaster")) == 'inserted') {

                 if ($('#dataFormMasterServiceConsultants').combobox('getValue') == "") {
                     //招募人員=>(登入用戶編號=>預設招募代號)
                     var UserID = getClientInfo("UserID");
                     setTimeout(function () {
                         var data = $("#dataFormMasterServiceConsultants").combobox('getData');
                         for (var i = 0; i < data.length; i++) {
                             if (data[i].EmpID == UserID) {
                                 $("#dataFormMasterServiceConsultants").combobox('setValue', data[i].ID);
                             }
                         }
                     }, 200);

                 }

                 //職缺地點清空
                 var DutyAreasIDs = $("#dataFormMasterDutyAreasIDs").options('getValue');
                 var DutyAreaClassID = $("#dataFormMasterDutyAreaClassIDs").options('getValue');
                 OnWhereClassID = " ClassID =111";
                 $('#dataFormMasterDutyAreasIDs').options('initializePanel');
                 $("#dataFormMasterDutyAreasIDs").options('setValue', DutyAreasIDs);

             } else {

                 var DutyAreasIDs = $("#dataFormMasterDutyAreasIDs").options('getValue');
                 var DutyAreaClassID = $("#dataFormMasterDutyAreaClassIDs").options('getValue');
                 if (DutyAreaClassID != "") {
                     OnWhereClassID = " ClassID in (" + DutyAreaClassID + ")";
                 }
                 $('#dataFormMasterDutyAreasIDs').options('initializePanel');
                 var row = $('#dataGridView').datagrid('getSelected');
                 $("#dataFormMasterDutyAreasIDs").options('setValue', row.DutyAreasIDs);
             }

         }

         //-------------------CheckBox顯示---------------------------------------
         function genCheckBox(val) {
             if (val)
                 return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
             else
                 return "<input  type='checkbox'  onclick='return false;'  />";
         }

         //區域,招募連動-查詢
         function SalesTeamOnSelectQ() {
             var ServiceSalesTeam = $("#ServiceSalesTeam_Query").combobox('getValue');
             $("#ServiceConsultants_Query").combobox('setWhere', "SalesTeamID = '" + ServiceSalesTeam + "'");
         }

          //工作縣市,工作地點連動
          var OnWhereClassID ;
          function OnSelectDutyAreaClass(rowdata) {
              var DutyAreasIDs = $("#dataFormMasterDutyAreasIDs").options('getValue');
              //var DutyAreaClassID = $("#dataFormMaster3DutyAreaClassIDs").options('getValue');
              if (rowdata != "") {
                  OnWhereClassID = " ClassID in (" + rowdata + ")";
              }
              $('#dataFormMasterDutyAreasIDs').options('initializePanel');
              $("#dataFormMasterDutyAreasIDs").options('setValue', DutyAreasIDs);

          }
          function OnWhereAreaClassID(param) {
              return OnWhereClassID;
          }

         //更改完成
          function DFOnApplied() {
              //修改文字
              UpdateRecReference();
              UserQuery();
          }
          // 修改多選選項對應的文字
          function UpdateRecReference() {
              var row = $('#dataGridView').datagrid('getSelected');
              var COMPANY_JOB_ID = $("#dataFormMasterCOMPANY_JOB_ID").val();//公告代號 
              $.ajax({
                  type: "POST",
                  url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_COMPANY_JOBFront.HRM_COMPANY_JOBFront', //連接的Server端，command
                  data: "mode=method&method=" + "UpdateRecReference" + "&parameters=" + encodeURIComponent(COMPANY_JOB_ID), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
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
          function queryGrid(dg) {//查詢後添加固定條件
              if ($(dg).attr('id') == 'dataGridView') {
                  UserQuery();
              }
              if ($(dg).attr('id') == 'DGApplyJobLogs') {
                  ApplyJobQuery();
              }
          }
          function UserQuery() {
              var FrontName = $('#FrontName_Query').val();//職缺名稱
              var SalesTeam = $('#ServiceSalesTeam_Query').combobox('getValue');//招募地區	
              var ServiceConsultants = $('#ServiceConsultants_Query').combobox('getValue');//招募人員	
              var IsActiveDate = $("#IsActiveDate_Query").datebox("getValue");//有效日期
              var JobID = $('#COMPANY_JOB_ID_Query').val();//職缺代號

              var tString = $('#DutyAreas_Query').val();//搜尋字串
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

              var iOrderby = $("#Orderby_Query").options('getValue');
              if (iOrderby == "") {//是否選擇and=1,or=2
                  iOrderby = 0;
              }
              $.ajax({
                  type: "POST",
                  url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_COMPANY_JOBFront.HRM_COMPANY_JOBFront', //連接的Server端，command
                  data: "mode=method&method=" + "HRM_COMPANY_JOBFrontQuery" + "&parameters=" + encodeURIComponent(FrontName + "*" + SalesTeam + "*" + ServiceConsultants + "*" + IsActiveDate + "*" + JobID + "*" + tString + "*" + Mark + "*" + iOrderby), //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                  cache: false,
                  async: true,
                  success: function (data1) {
                      var rows = $.parseJSON(data1);//將JSon轉會到Object類型提供給Grid顯示
                      var data = new Object();
                      data.rows = rows;
                      if (rows == null) {
                          $('#dataGridView').datagrid('loadData', []); //Grid清空資料         
                          alert("目前無符合職缺！");
                      } else {
                          data.total = rows.length;
                          //$('#dataGridView').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                          $('#dataGridView').datagrid({ loadFilter: pagerFilter, url: "", pageNumber: 1 }).datagrid('loadData', rows);//第一頁
                      }
                  }
              });


          }
          //---------------------------複制職缺-------------------------------------
          function CopyJob() {

              var row = $('#dataGridView').datagrid('getSelected');
              openForm('#JQDialog1', row, "inserted", 'dialog');
              $("#dataFormMasterCOMPANY_JOB_FrontName").focus();
              //清空上傳檔案的文字框
              var infofileUploadvalue = $('.info-fileUpload-value', $('#dataFormMasterJobPhotoFile').next())  //取得選取文件後顯示的元件
              infofileUploadvalue.val("");

              var DutyAreasIDs = $("#dataFormMasterDutyAreasIDs").options('getValue');
              var DutyAreaClassID = $("#dataFormMasterDutyAreaClassIDs").options('getValue');
              if (DutyAreaClassID != "") {
                  OnWhereClassID = " ClassID in (" + DutyAreaClassID + ")";
              }
              $('#dataFormMasterDutyAreasIDs').options('initializePanel');
              var row = $('#dataGridView').datagrid('getSelected');
              $("#dataFormMasterDutyAreasIDs").options('setValue', row.DutyAreasIDs);
          }

          function FeaturedOnLoad() {
              //檢查是否有9筆資料=>把新增按鈕隱藏
              if (CheckFeaturedCount() == 9) {
                  $("#toolItemDGFeatured新增精選職缺").hide();
              } else $("#toolItemDGFeatured新增精選職缺").show();
          }

         //設定精選職缺
          function SetFeaturedJob() {
              openForm('#Dialog_Featured', {}, 'viewed', 'dialog');
          }

         //新增完精選職缺
          function DFFeaturedOnApplied() {
              UserQuery();
              //檢查是否有9筆資料=>把新增按鈕隱藏
              if (CheckFeaturedCount() == 9) {
                  $("#toolItemDGFeatured新增精選職缺").hide();
              } else $("#toolItemDGFeatured新增精選職缺").show();
          }
         //刪除精選職缺
          function FeaturedOnDeleted() {
              UserQuery();
              $("#DGFeatured").datagrid('reload');
              var data = $("#DGFeatured").datagrid("getRows");

              if (data.length >= 9) {
                  $("#toolItemDGFeatured新增精選職缺").hide();
              } else $("#toolItemDGFeatured新增精選職缺").show();
          }

          //檢查精選職缺為幾筆
          function CheckFeaturedCount() {
              var iCount;

              $.ajax({
                  type: "POST",
                  url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_COMPANY_JOBFront.HRM_COMPANY_JOBFront', //連接的Server端，command
                  data: "mode=method&method=" + "ReturnFeaturedCount" , //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                  cache: false,
                  async: false,
                  success: function (data) {
                      iCount = data
                  },
              });
              return iCount;
          }

         //--------------主動應徵(紀錄)-----------------------------------------------------------------------------------------------   
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
                  var sName = $('#sName_Query').val();//人才姓名

                  var JobId = $('#dataGridView').datagrid('getSelected').COMPANY_JOB_ID;
                  if (JobId != '') result2.push("j.JobId=" + JobId);
                  if (JobDate1 != '') result2.push("j.UpdateDate between '" + JobDate1 + "' and '" + JobDate2 + "'");
                  if (sName != '') result2.push("u.NameC like '%" + sName + "%'");
                  $("#DGApplyJobLogs").datagrid('setWhere', result2.join(' and '));

          }
          //---------------呼叫開啟人才 Tab--------------------------------------------------------------------------------
          function OpenUserTab(value, row, index) {
              if (value == undefined) ""
              else if (value != "0")
                  return "<a href='javascript: void(0)' onclick='LinkUserTab(" + index + ");' >" + value + "</a>";
              else return value;
          }
          function LinkUserTab(index) {
              $("#DGApplyJobLogs").datagrid('selectRow', index);
              var rows = $("#DGApplyJobLogs").datagrid('getSelected');
              var UserID = rows.RecUserID;

              parent.addTab('人才履歷維護', './JB_ADMIN/HRM_REC_User_Management.aspx?UserID=' + UserID);
          }

          //職缺縣市統計
          function DutyAreaClassCount() {
              openForm('#Dialog_DutyAreaClassCount', {}, 'viewed', 'dialog');
          }


     </script>




</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="_HRM_COMPANY_JOBFront.HRM_COMPANY_JOBFront" runat="server" AutoApply="True"
                DataMember="HRM_COMPANY_JOBFront" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="公告職缺維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="職缺流水號" Editor="numberbox" FieldName="COMPANY_JOB_ID" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="COMPANY_FrontName" Format="" MaxLength="0" Visible="true" Width="115" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="COMPANY_JOB_FrontName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="130">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="應徵人才" Editor="text" FieldName="iApplyJob" MaxLength="0" Width="55" FormatScript="ApplyJobLink" />
                    <JQTools:JQGridColumn Alignment="center" Caption="招募人員" Editor="text" FieldName="ConsultantName" Format="" Visible="true" Width="55" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺縣市" Editor="text" FieldName="DutyAreaClass" Format="" MaxLength="800" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺地點" Editor="text" FieldName="DutyAreas" Format="" MaxLength="2147483647" Visible="true" Width="100" />
                    <JQTools:JQGridColumn Alignment="center" Caption="精選" Editor="text" EditorOptions="" FieldName="isFeatured" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="圖片" Editor="text" FieldName="JobPhotoFile" Format="Image,Folder:../JQWebClient2015/Files/Files/Jobs,Height:30" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35" FormatScript="" />                    
                    <JQTools:JQGridColumn Alignment="center" Caption="有效性" Editor="checkbox" FieldName="IsActive" Format="" Visible="true" Width="45" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="center" Caption="排序" Editor="text" FieldName="ShowOrder" Format="" Visible="true" Width="50" />
                    <JQTools:JQGridColumn Alignment="center" Caption="顯示日期" Editor="datebox" FieldName="ShowDate" Format="" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="center" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="yyyy/mm/dd HH:MM" Visible="true" Width="95" />
                    <JQTools:JQGridColumn Alignment="center" Caption="修改者" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="20" Visible="true" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺福利" Editor="text" FieldName="JOBWelfare" Format="" MaxLength="500" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺條件" Editor="text" FieldName="JOBCondition" Format="" MaxLength="500" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺備註" Editor="text" FieldName="MEMO" Format="" MaxLength="500" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺縣市代號" Editor="text" FieldName="DutyAreaClassIDs" Format="" MaxLength="300" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺地點代號" Editor="text" FieldName="DutyAreasIDs" Format="" MaxLength="3000" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="text" FieldName="CreateBy" Format="" MaxLength="20" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增公告職缺" />
                    <JQTools:JQToolItem Icon="icon-copy" ItemType="easyui-linkbutton" OnClick="CopyJob" Text="複製職缺" />
                    <JQTools:JQToolItem ID="JQToolItem2" Icon="icon-tip" ItemType="easyui-linkbutton" OnClick="SetFeaturedJob" Text="設定精選職缺(9個)"  />    
                    <JQTools:JQToolItem ID="JQToolItem1" Icon="icon-view" ItemType="easyui-linkbutton" OnClick="DutyAreaClassCount" Text="職缺縣市統計"  />                                                   
                                               
<%--                        <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />--%>
<%--                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                  <JQTools:JQQueryColumn AndOr="and" Caption="職缺名稱" Condition="%" DataType="string" Editor="text" FieldName="FrontName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="155" DefaultValue="" />
                   <JQTools:JQQueryColumn AndOr="and" Caption="招募區域" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'_HRM_REC_User_Management.infoREC_SalesTeam',tableName:'infoREC_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:SalesTeamOnSelectQ,panelHeight:90" FieldName="ServiceSalesTeam" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                  <JQTools:JQQueryColumn AndOr="and" Caption="招募人員" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ConsultantName',remoteName:'_HRM_REC_User_Management.infoServiceConsultants',tableName:'infoServiceConsultants',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:180" FieldName="ServiceConsultants" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="115" />
                  <JQTools:JQQueryColumn AndOr="and" Caption="有效日期" Condition="%" DataType="string" Editor="datebox" FieldName="IsActiveDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="86" />               
                    <JQTools:JQQueryColumn AndOr="and" Caption="職缺代號" Condition="%" DataType="string" Editor="text" FieldName="COMPANY_JOB_ID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                  <JQTools:JQQueryColumn AndOr="and" Caption="職缺縣市/地點" Condition="%" DataType="string" Editor="text" FieldName="DutyAreas" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="2" Width="300" />
                  <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" DefaultValue="and" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:65,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'and',value:'and'},{text:'or',value:'or'}]" FieldName="AndOr" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="70" />

                    <JQTools:JQQueryColumn AndOr="and" Caption="顯示順序" Condition="%" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:300,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:4,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'修改日期',value:'1'},{text:'應徵數量',value:'2'},{text:'職缺排序',value:'3'},{text:'有效性',value:'4'}]" FieldName="Orderby" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />

                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="公告職缺維護" Width="1010px" DialogLeft="5px" DialogTop="5px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HRM_COMPANY_JOBFront" HorizontalColumnsCount="6" RemoteName="_HRM_COMPANY_JOBFront.HRM_COMPANY_JOBFront" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnLoadSuccess="OnLoadMaster" OnApplied="DFOnApplied" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="COMPANY_FrontName" Width="300" maxlength="0" Span="3" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="COMPANY_JOB_FrontName" MaxLength="200" Span="3" Width="280" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職缺圖片" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'../JQWebClient2015/Files/Files/Jobs',showButton:true,showLocalFile:false,fileSizeLimited:'1000'" FieldName="JobPhotoFile" MaxLength="0" Span="3" Width="280" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作班別" Editor="infooptions" EditorOptions="title:'工作班別',panelWidth:390,remoteName:'_HRM_REC_User_Management.infoREC_ZDutyClasses',tableName:'infoREC_ZDutyClasses',valueField:'ID',textField:'Contents',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="DutyClassesIDs" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職缺縣市" Editor="infooptions" EditorOptions="title:'職缺縣市',panelWidth:200,remoteName:'_HRM_REC_User_Management.infoREC_ZDutyAreasClass',tableName:'infoREC_ZDutyAreasClass',valueField:'ID',textField:'Contents',columnCount:2,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectDutyAreaClass,selectOnly:false,items:[]" FieldName="DutyAreaClassIDs" Format="" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職缺地點" Editor="infooptions" EditorOptions="title:'職缺地點',panelWidth:590,remoteName:'_HRM_REC_User_Management.infoREC_ZDutyAreas',tableName:'infoREC_ZDutyAreas',valueField:'ID',textField:'Contents',columnCount:8,multiSelect:true,openDialog:false,selectAll:false,onWhere:OnWhereAreaClassID,selectOnly:false,items:[]" FieldName="DutyAreasIDs" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="招募人員" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ConsultantName',remoteName:'_HRM_REC_User_Management.infoRecID',tableName:'infoRecID',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:180" FieldName="ServiceConsultants" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="110" />
                        <JQTools:JQFormColumn Alignment="right" Caption="職缺薪資" Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="JOBSalary1" maxlength="0" NewRow="False" Span="2" Width="60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職缺排序" Editor="numberbox" FieldName="ShowOrder" Format="" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="顯示日期" Editor="datebox" FieldName="ShowDate" Format="" Width="90" MaxLength="0" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="有效性" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Format="" maxlength="0" NewRow="False" Span="1" Width="40" />
                        <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="JOBSalary2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="薪資福利" Editor="textarea" FieldName="JOBWelfare" Format="" Width="410" EditorOptions="height:75" MaxLength="500" NewRow="True" Span="3" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作內容" Editor="textarea" FieldName="JOBContent" Format="" maxlength="500" Width="410" EditorOptions="height:75" Span="3" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求條件" Editor="textarea" EditorOptions="height:75" FieldName="JOBCondition" MaxLength="500" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="410" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求條件" Editor="textarea" FieldName="JOBConditionN" maxlength="500" Span="3" Width="410" EditorOptions="height:75" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備    註" Editor="textarea" EditorOptions="height:50" FieldName="MEMO" Format="" maxlength="500" NewRow="False" Span="6" Visible="True" Width="880" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立者" Editor="text" FieldName="CreateBy" Format="" maxlength="20" NewRow="True" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改者" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="20" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職缺流水號" Editor="numberbox" FieldName="COMPANY_JOB_ID" Format="" Visible="False" Width="180" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="COMPANY_JOB_ID" RemoteMethod="True" />
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ShowDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ShowOrder" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsActive" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="COMPANY_JOB_FrontName" RemoteMethod="True" ValidateMessage="職缺名稱不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ServiceConsultants" RemoteMethod="True" ValidateMessage="請選擇招募人員！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="JOBWelfare" RemoteMethod="True" ValidateMessage="薪資福利不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="JOBContent" RemoteMethod="True" ValidateMessage="工作內容不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ShowDate" RemoteMethod="True" ValidateMessage="職缺顯示日期不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ShowOrder" RemoteMethod="True" ValidateMessage="職缺排序不可空白！(預設為0)" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DutyAreasIDs" RemoteMethod="True" ValidateMessage="請選擇職缺地點！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DutyAreaClassIDs" RemoteMethod="True" ValidateMessage="請選擇職缺縣市！" ValidateType="None" />
 
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="JOBSalary1" RemoteMethod="True" ValidateMessage="職缺薪資(起)不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="JOBSalary2" RemoteMethod="True" ValidateMessage="職缺薪資(迄)不可空白！" ValidateType="None" />
 
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>


                        <JQTools:JQDialog ID="Dialog_Featured" runat="server" BindingObjectID="" Title="精選職缺設定" DialogLeft="60px" DialogTop="30px" Width="710px" Wrap="False" EditMode="Dialog" ShowSubmitDiv="False" Height="410px">
                            <JQTools:JQDataGrid ID="DGFeatured" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HRM_COMPANY_JOBFrontFeatured" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogFeatured" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="_HRM_COMPANY_JOBFront.HRM_COMPANY_JOBFrontFeatured" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="100%" OnDeleted="FeaturedOnDeleted" OnLoadSuccess="FeaturedOnLoad" Height="300px">
                                <Columns>
                                    <JQTools:JQGridColumn Alignment="left" Caption="客戶職缺" Editor="text" FieldName="sCOMPANY_FrontName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="340" EditorOptions="">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="新增日期" Editor="text" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="92">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="有效性" Editor="checkbox" FieldName="IsActive" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="新增者" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="52">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="Autokey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                                </RelationColumns>
                                <TooItems>
                                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增精選職缺" />
                                </TooItems>
                            </JQTools:JQDataGrid>
                            <JQTools:JQDialog ID="JQDialogFeatured" runat="server" BindingObjectID="DFFrontFeatured" DialogLeft="180px" DialogTop="100px" Title="精選職缺設定(9個)" Width="450px">
                                <JQTools:JQDataForm ID="DFFrontFeatured" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HRM_COMPANY_JOBFrontFeatured" disapply="False" DivFramed="False" DuplicateCheck="True" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="" RemoteName="_HRM_COMPANY_JOBFront.HRM_COMPANY_JOBFrontFeatured" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApplied="DFFeaturedOnApplied">
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="選擇職缺" Editor="inforefval" EditorOptions="title:'選擇職缺',panelWidth:550,panelHeight:200,remoteName:'_HRM_COMPANY_JOBFront.infoJOBFrontFeatured',tableName:'infoJOBFrontFeatured',columns:[{field:'COMPANY_FrontName',title:'客戶名稱',width:180,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'COMPANY_JOB_FrontName',title:'職缺名稱',width:180,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'LastUpdateDate',title:'修改日期',width:110,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'COMPANY_JOB_ID',textField:'COMPANY_JOB_FrontName',valueFieldCaption:'COMPANY_JOB_ID',textFieldCaption:'COMPANY_JOB_FrontName',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="COMPANY_JOB_ID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="Autokey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    </Columns>
                                    <RelationColumns>
                                        <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                                    </RelationColumns>
                                </JQTools:JQDataForm>
                                
                                <JQTools:JQValidate ID="JQValidate2" runat="server" BindingObjectID="DFFrontFeatured" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="COMPANY_JOB_ID" RemoteMethod="True" ValidateMessage="請選擇職缺！" ValidateType="None" />
                                    </Columns>
                                </JQTools:JQValidate>
                                <JQTools:JQDefault ID="JQDefault2" runat="server" BindingObjectID="DFFrontFeatured" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="CreateBy" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Autokey" RemoteMethod="True" />
                                    </Columns>
                                </JQTools:JQDefault>
                                
                            </JQTools:JQDialog>
                        </JQTools:JQDialog>


                <JQTools:JQDialog ID="JQDialogApplyJob" runat="server" BindingObjectID="dataFormMaster2" Title="應徵紀錄" Width="815px" DialogLeft="80px" DialogTop="10px" Height="460px" EditMode="Dialog" ShowSubmitDiv="False">
                    <JQTools:JQDataForm ID="dataFormMaster2" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HRM_COMPANY_JOBFront" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="6" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="_HRM_COMPANY_JOBFront.HRM_COMPANY_JOBFront" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" ParentObjectID="dataFormMaster">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="COMPANY_FrontName" MaxLength="0" Span="1" Width="180" NewRow="False" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="COMPANY_JOB_FrontName" MaxLength="200" Span="1" Width="180" Format="" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="職缺流水號" Editor="numberbox" FieldName="COMPANY_JOB_ID" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <fieldset>
                        <legend></legend>
                        <JQTools:JQDataGrid ID="DGApplyJobLogs" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="MyFavJobs" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" Height="270px" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="5,10,15,20" PageSize="5" Pagination="False" ParentObjectID="" QueryAutoColumn="False" QueryLeft="270px" QueryMode="Window" QueryTitle="人才搜尋" QueryTop="20px" RecordLock="False" RecordLockMode="None" RemoteName="_HRM_COMPANY_JOBFront.MyFavJobs" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="730px">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="center" Caption="應徵日期" Editor="datebox" FieldName="UpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="100">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="人才資訊" Editor="text" EditorOptions="" FieldName="sName" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="190" FormatScript="OpenUserTab">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="MobileNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="最高學歷" Editor="text" EditorOptions="" FieldName="EduName" Format="" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="希望工作地點" Editor="text" FieldName="DutyAreas" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="200">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="履歷編號" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                            </RelationColumns>
                             <TooItems>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                            <QueryColumns>
                            <JQTools:JQQueryColumn AndOr="and" Caption="應徵日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="JobDate1" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="95" RowSpan="0" Span="0" />
                            <JQTools:JQQueryColumn AndOr="and" Caption=" ～ " Condition="=" DataType="datetime" Editor="datebox" FieldName="JobDate2" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="95" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="姓名" Condition="%%" DataType="string" Editor="text" FieldName="sName" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="80" />
                            </QueryColumns>
                        </JQTools:JQDataGrid>
                    </fieldset>
                </JQTools:JQDialog>


                        <JQTools:JQDialog ID="Dialog_DutyAreaClassCount" runat="server" BindingObjectID="" Title="有效職缺縣市統計" DialogLeft="200px" DialogTop="60px" Width="300px" Wrap="False" EditMode="Dialog" ShowSubmitDiv="False">
                            <JQTools:JQDataGrid ID="DGDutyAreaClassCount" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="infoDutyAreaClassCount" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="Dialog_DutyAreaClassCount" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="_HRM_COMPANY_JOBFront.infoDutyAreaClassCount" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="100%">
                                <Columns>
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺縣市" Editor="text" FieldName="DutyAreaClass" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" EditorOptions="">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="數量" Editor="text" FieldName="iCount" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="80" Total="sum">
                                    </JQTools:JQGridColumn>
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                                </RelationColumns>
                            </JQTools:JQDataGrid>
                        </JQTools:JQDialog>


            <JQTools:JQImageContainer ID="JQImageContainer1" runat="server" AutoSize="False" Height="250px" Width="300px">
            </JQTools:JQImageContainer>
    </form>
</body>
</html>
