<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_REC_Report_NewRecruitsCount.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
                 $(queryPanel).panel('resize', { width: 630 });
             //Grid隱藏
             $('#dataGridMaster').datagrid('getPanel').hide();
             
             //---填表日期
             var SDate = $('#sDate_Query').closest('td');
             var EDate = $('#eDate_Query').closest('td').children();
             SDate.append("&nbsp;&nbsp;～&nbsp;&nbsp;").append(EDate);

             //查詢條件預設值
             var dt = new Date();
             var FirstDate = new Date($.jbGetFirstDate(dt));
             $("#sDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));
             var LastDate = new Date($.jbGetLastDate(dt));
             $("#eDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));
             

         });
         
         //區域,招募連動-查詢
         function SalesTeamOnSelectQ() {
             var ServiceSalesTeam = $("#ServiceSalesTeam_Query").combobox('getValue');
             $("#ServiceConsultants_Query").combobox('setWhere', "SalesTeamID = '" + ServiceSalesTeam + "'");
         }

         function queryGrid(dg) {//查詢後添加固定條件
            
             var sDate = $("#sDate_Query").datebox("getValue");//datebox("getBindingValue");//datebox("getValue");                
             var eDate = $("#eDate_Query").combo('textbox').val();
             var SalesTeam = $("#ServiceSalesTeam_Query").combobox('getValue');
             var ServiceConsultants = $('#ServiceConsultants_Query').combobox('getValue');//招募人員
             var ContactPeople = $('#ContactPeople_Query').combobox('getText');//面談修改人員

             var sFileName = "統計表";
             var url = "../JB_ADMIN/REPORT/RecUser/NewRecruitsCount.aspx?sDate=" + sDate + "&eDate=" + eDate + "&SalesTeam=" + SalesTeam + "&ServiceConsultants=" + ServiceConsultants + "&ContactPeople=" + ContactPeople + "&sFileName=" + sFileName;

                 var height = $(window).height() - 50;
                 var height2 = $(window).height() - 50;
                 var width = $(window).width() - 170;
                 var dialog = $('<div/>')
                 .dialog({
                     draggable: false,
                     modal: true,
                     height: height,
                     //top:0,
                     width: width,
                     title: "填表統計",
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


&nbsp;<div id="Dialog_dataGridMasterData">
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="_HRM_REC_Report_NewRecruitsCount.NewREC_User" runat="server" AutoApply="True"
                DataMember="NewREC_User" Pagination="True" QueryTitle="查詢條件"
                Title="" ReportFileName="~/JB_ADMIN/REPORT/Media/JBERP_R_SalesDetails2.rdlc" QueryMode="Panel" DeleteCommandVisible="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="True" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" >
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesName" Editor="numberbox" FieldName="SalesName" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesEmployeeID" Editor="text" FieldName="SalesEmployeeID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ViewAreaName" Editor="text" FieldName="ViewAreaName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="iCount" Editor="text" FieldName="iCount" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="salesQty" Editor="text" FieldName="salesQty" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustAmt" Editor="text" FieldName="CustAmt" Format="" MaxLength="0" Width="120" />
                </Columns>                 
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="填表/面談日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="sDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="=" DataType="datetime" Editor="datebox" FieldName="eDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="招募區域" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'_HRM_REC_User_Management.infoREC_SalesTeam',tableName:'infoREC_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:SalesTeamOnSelectQ,panelHeight:90" FieldName="ServiceSalesTeam" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="110" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="招募人員" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ConsultantName',remoteName:'_HRM_REC_User_Management.infoServiceConsultants2',tableName:'infoServiceConsultants2',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:90" FieldName="ServiceConsultants" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="110" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="面談修改人員" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ConsultantName',remoteName:'_HRM_REC_User_Management.infoServiceConsultants2',tableName:'infoServiceConsultants2',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:90" FieldName="ContactPeople" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="110" />
                    
                     </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
