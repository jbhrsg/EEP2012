<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JCS1_BillData.aspx.cs" Inherits="Template_JQuerySingle1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="EFClientTools" namespace="EFClientTools" tagprefix="cc1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>                                
         $(document).ready(function () {
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
                 $(queryPanel).panel('resize', { width: 500 });
             //Grid隱藏
             $('#dataGridMaster').datagrid('getPanel').hide();

             //查詢條件預設值
             var sDate = new Date();
             var vDate = new Date($.jbDateAdd('months', -1, sDate));
             var YM = $.jbjob.Date.DateFormat(vDate, 'yyyyMMdd').substring(0, 6);
             $("#YearMonth_Query").val(YM);//銷貨年月

         }

         function queryGrid(dg) {//查詢後添加固定條件
             var YearMonth = $("#YearMonth_Query").val();//datebox("getBindingValue");//datebox("getValue");                
             var CustomerID = $("#CustomerID_Query").combobox('getValue');

             var url = "../JB_ADMIN/REPORT/JCS1/BillDataReportView.aspx?YM=" + YearMonth + "&CustID=" + CustomerID;
            
            var height = $(window).height() - 20;
            var height2 = $(window).height()- 72;
             var width = $(window).width() - 80;
             var dialog = $('<div/>')
             .dialog({
                 draggable: false,
                 modal: true,
                 height: height,                
                 //top:0,
                 width: width,
                 title: " ",
                 //maximizable: true                              
             });
             $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="' + height2 + '"></iframe>').appendTo(dialog.find('.panel-body'));
             dialog.dialog('open');
        
         }
         
    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />


&nbsp;<div id="Dialog_dataGridMasterData">
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sJCS1.BillPayMaster" runat="server" AutoApply="True"
                DataMember="BillPayMaster" Pagination="True" QueryTitle="查詢條件"
                Title="" ReportFileName="~/JB_ADMIN/JBERP_R_SalesDetails.rdlc" QueryMode="Panel" DeleteCommandVisible="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="True" OnLoadSuccess="OnLoadSuccessGV" AllowAdd="True" AllowDelete="True" AllowUpdate="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" >
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨年月" Editor="text" FieldName="YearMonth" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerID" MaxLength="0" Width="80" />
                </Columns>                 
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨年月" Condition="%" DataType="string" Editor="text" FieldName="YearMonth" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶名稱" Condition="%" DataType="string" Editor="infocombobox" FieldName="CustomerID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" EditorOptions="valueField:'CustomerID',textField:'CustomerShortName',remoteName:'sJCS1.infoCustomers',tableName:'infoCustomers',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
