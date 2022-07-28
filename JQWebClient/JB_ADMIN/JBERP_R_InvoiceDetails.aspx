<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_R_InvoiceDetails.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
             $("#InvoiceYM_Query").val(YM);//發票年月

         });
                 
         function queryGrid(dg) {//查詢後添加固定條件
             var InsGroupID = $("#InsGroupID_Query").combobox('getValue');
             if (InsGroupID == "" || InsGroupID == undefined) {
                 alert("請選擇公司別！");
                 return false;
             }
             var InvoiceYM = $("#InvoiceYM_Query").val();
             if (InvoiceYM == "" || InvoiceYM == undefined) {
                 alert("請選擇發票年月！");
                 return false;
             }
            
             var SalesTypeID = $("#SalesTypeID_Query").combobox('getValue');
             var url = "../JB_ADMIN/REPORT/JCS1/InvoiceDetailsReportView.aspx?InsGroupID=" + InsGroupID + "&SalesTypeID=" + SalesTypeID + "&InvoiceYM=" + InvoiceYM;
            
             var height = $(window).height() - 40;
             var height2 = $(window).height() - 80;
             var width = $(window).width() - 60;
             var dialog = $('<div/>')
             .dialog({
                 draggable: false,
                 modal: true,
                 height: height,
                 width: width,
                 title: "Report",
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
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERPInvoiceDetails.InvoiceDetails" runat="server" AutoApply="True"
                DataMember="InvoiceDetails" Pagination="True" QueryTitle="查詢條件"
                Title="" ReportFileName="~/JB_ADMIN/REPORT/Media/JBERP_R_SalesDetails2.rdlc" QueryMode="Panel" DeleteCommandVisible="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="False" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" BufferView="False" NotInitGrid="False" RowNumbers="True" >
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="QInvoiceType" Editor="text" FieldName="QInvoiceType" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CustomerID" Editor="numberbox" FieldName="CustomerID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortName" Editor="text" FieldName="ShortName" Format="" MaxLength="0" Width="120" />
                </Columns>                 
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="%" DataType="string" Editor="infocombobox" FieldName="InsGroupID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="120" EditorOptions="valueField:'InsGroupID',textField:'InsGroupShortName',remoteName:'sERPInvoiceDetails.infoInsGroupID',tableName:'infoInsGroupID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨類別" Condition="%" DataType="string" Editor="infocombobox" FieldName="SalesTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="130" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPInvoiceDetails.infoSalesType',tableName:'infoSalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="發票年月" Condition="=" DataType="string" Editor="text" FieldName="InvoiceYM" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="60" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
