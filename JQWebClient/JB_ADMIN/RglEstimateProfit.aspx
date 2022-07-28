<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RglEstimateProfit.aspx.cs" Inherits="Template_JQuerySingle1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="EFClientTools" namespace="EFClientTools" tagprefix="cc1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>     
         //========================================= ready ====================================================================================

         var sCompanyID = "";
         var sVoucherID = "";

         $(document).ready(function () {

            

            //變色        
             $("input, select, textarea").focus(function () {
                 $(this).css("background-color", "#FFFFB5");
             });

             $("input, select, textarea").blur(function () {
                 $(this).css("background-color", "white");
             });
          

         });
         
         

         function OnLoadSuccessGV() {
             //panel寬度調整
             var dgid = $('#dataGridMaster');
             var queryPanel = getInfolightOption(dgid).queryDialog;
             if (queryPanel)
                 $(queryPanel).panel('resize', { width: 850 });
             //Grid隱藏
             $('#dataGridMaster').datagrid('getPanel').hide();

             //查詢條件預設值
             var dt = new Date();
             var FirstDate = new Date($.jbGetFirstDate(dt));
             $("#SDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));
             var LastDate = new Date($.jbGetLastDate(dt));
             $("#EDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));


           

         }

         function queryGrid(dg) {//查詢後添加固定條件
             //報表用參數

             var JQDate1 = $("#SDate_Query").datebox("getValue");              
             var JQDate2 = $("#EDate_Query").datebox("getValue");
             //---------------------日期檢查---------------------------------------------------------------
             var beginDateValidate = $.fn.datebox('parseDate', JQDate1.replace(/\//g, '-'));
             var endDateValidate = $.fn.datebox('parseDate', JQDate2.replace(/\//g, '-'));
             //判斷起始日期不可大於結束日期
             if (beginDateValidate == "Invalid Date" || !$.jbIsDateStr(JQDate1)) {
                 alert('起始日期:' + JQDate1 + '格式錯誤');
                 $("#dataFormMasterBeginDate").datebox('textbox').focus();
                 return false;
             }

             if (endDateValidate == "Invalid Date" || !$.jbIsDateStr(JQDate2)) {
                 alert('結束日期:' + JQDate2 + '格式錯誤');
                 $("#dataFormMasterEndDate").datebox('textbox').focus();
                 return false;
             }

             if (JQDate1 > JQDate2) {
                 alert('起始日期 : ' + JQDate1 + ' 需小於結束日期 : ' + JQDate2);
                 $("#VoucherDate_Query").datebox('textbox').focus();
                 return false;
             }

             var CompanyID = "1";//資訊+人力+傑信
             var VoucherID = "A";
             var CostCenterID = $("#CostCenterID_Query").combobox('getValue');
             var iType = $("#iType_Query").combobox('getValue');//1 各部門預估損益  ,2預估淨利  ,3  ,4

                 var CostCenterText = "";
                 if (CostCenterID != "") {
                     CostCenterText = $("#CostCenterID_Query").combobox('getText');
                 }
                 var url = "../JB_ADMIN/REPORT/JBGL/RglEstimateProfit.aspx?JQDate1=" + JQDate1 + "&JQDate2=" + JQDate2 + "&CompanyID=" + CompanyID + "&VoucherID=" + VoucherID + "&CostCenterID=" + CostCenterID + "&iType=" + iType +
                      "&CostCenterText=" + CostCenterText ;

                 var height = $(window).height() - 40;
                 var height2 = $(window).height() - 110;
                 var width = $(window).width() - 60;
                 var dialog = $('<div/>')
                 .dialog({
                     draggable: false,
                     modal: true,
                     height: height,
                     //top:0,
                     width: width,
                     title: "Report",
                     maximizable: true
                 });
                 $('<iframe style="border: 0px;" src="' + url + '" width="99%" height="98%" ></iframe>').appendTo(dialog.find('.panel-body'));
                 dialog.dialog('open');
            
         }
         
    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />


&nbsp;<div id="Dialog_dataGridMasterData">
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sRglVoucherList.glVoucherList" runat="server" AutoApply="True"
                DataMember="glVoucherList" Pagination="True" QueryTitle="查詢條件"
                Title="" ReportFileName="~/JB_ADMIN/REPORT/JBGL/glVoucherList.rdlc" QueryMode="Panel" DeleteCommandVisible="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="True" OnLoadSuccess="OnLoadSuccessGV" AllowAdd="True" AllowDelete="True" AllowUpdate="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" >
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="預估損益日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="SDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="95" RowSpan="0" Span="2" />
                    <JQTools:JQQueryColumn AndOr="and" Caption=" ～" Condition="=" DataType="datetime" Editor="datebox" FieldName="EDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="95" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="成本中心" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sEstimateProfit.infoglCostCenter',tableName:'infoglCostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="160" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="報表種類" Condition="%" DataType="string" Editor="infocombobox" FieldName="iType" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="3" Width="200" EditorOptions="items:[{value:'1',text:'各部門預估損益',selected:'true'},{value:'2',text:'損益預估表',selected:'false'},{value:'3',text:'損益累積差異分析',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
