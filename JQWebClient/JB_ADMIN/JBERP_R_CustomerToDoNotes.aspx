<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_R_CustomerToDoNotes.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
             var dt = new Date();
             var FirstDate = new Date($.jbGetFirstDate(dt));
             $("#NextCallDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));
             $("#CreateDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));

             var LastDate = new Date($.jbGetLastDate(dt));
             $("#NotesCreateDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));
             $("#CreateDate2_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));

         });
                 
         function queryGrid(dg) {//查詢後添加固定條件
             //var where = $(dg).datagrid('getWhere');
             //if (where.length > 0) {
             //預計日期
             var JQDate1 = $("#NextCallDate_Query").datebox("getValue");//datebox("getBindingValue");//datebox("getValue");                
             var JQDate11 = $("#NotesCreateDate_Query").combo('textbox').val();
             //建立日期
             var JQDate2 = $("#CreateDate_Query").datebox("getValue");//datebox("getBindingValue");//datebox("getValue");                
             var JQDate22 = $("#CreateDate2_Query").combo('textbox').val();

             var CreateBy = $("#CreateBy_Query").combobox('getValue');//提出者
             var SalesID = $("#SalesID_Query").combobox('getValue');

             var url = "../JB_ADMIN/REPORT/Media/JBERP_R_SalesDetailsReportView4.aspx?SDate=" + JQDate1 + "&EDate=" + JQDate11 + "&SDate2=" + JQDate2 + "&EDate2=" + JQDate22 + "&CreateBy=" + CreateBy + "&SalesID=" + SalesID;
            
             var height = $(window).height() - 20;
             var height2 = $(window).height() - 72;
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
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sR_SalesDetails.ERPCustomerToDoNotes" runat="server" AutoApply="True"
                DataMember="ERPCustomerToDoNotes" Pagination="True" QueryTitle="查詢條件"
                Title="" ReportFileName="~/JB_ADMIN/REPORT/Media/CustomerToDoNotes.rdlc" QueryMode="Panel" DeleteCommandVisible="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="True" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" BufferView="False" NotInitGrid="False" RowNumbers="True" >
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="CustNO" Editor="numberbox" FieldName="CustNO" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustName" Editor="text" FieldName="CustName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ListContent" Editor="text" FieldName="ListContent" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="NextCallDate" Editor="text" FieldName="NextCallDate" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="NotesCreateDate" Editor="text" FieldName="NotesCreateDate" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="HrBankUrl" Editor="text" FieldName="HrBankUrl" Format="" MaxLength="0" Width="120" />
                </Columns>                 
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="提出者" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CreateBy',textField:'CreateBy',remoteName:'sR_SalesDetails.infoCreateBy',tableName:'infoCreateBy',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CreateBy" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sR_SalesDetails.infoSalesMan',tableName:'infoSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="預計覆訪起始日" Condition="=" DataType="datetime" Editor="datebox" FieldName="NextCallDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="預計覆訪終止日" Condition="=" DataType="datetime" Editor="datebox" FieldName="NotesCreateDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="維保建立起始日" Condition="%" DataType="string" Editor="datebox" FieldName="CreateDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="維保建立終止日" Condition="=" DataType="datetime" Editor="datebox" FieldName="CreateDate2" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
