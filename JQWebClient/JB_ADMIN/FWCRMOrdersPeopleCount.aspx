<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMOrdersPeopleCount.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            
             //var Month1 = $("#iMonth_Query").closest('td');//月份1
             //var Month2 = $("#PersonQty_Query").closest('td').children();//月份2
             //Month1.append('〜').append(Month2);

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
             var dt = new Date();   
             $("#iYear_Query").val(dt.getFullYear() );             

         }

         function queryGrid(dg) {//查詢後添加固定條件
             //var where = $(dg).datagrid('getWhere');
             //if (where.length > 0) {
             var iYear = $("#iYear_Query").val();//datebox("getBindingValue");//datebox("getValue");                
             var SalesID = $("#SalesID_Query").combobox('getValue');
             var WorkTime = $("#ID_Query").combobox('getValue');//工期
             var NationalityID = $("#NationalityID_Query").combobox('getValue');//國籍	
             var EmployerName = $("#EmployerName_Query").val();//雇主
             //var Month1 = $("#iMonth_Query").val();
             //var Month2 = $("#PersonQty_Query").val();

             var url = "../JB_ADMIN/REPORT/FWCRM/OrdersPeopleCount.aspx?iYear=" + iYear + "&SalesID=" + SalesID + "&WorkTime=" + WorkTime + "&NationalityID=" + NationalityID + "&EmployerName=" + EmployerName;
            
                 var height = $(window).height() - 20;
                 var height2 = $(window).height() - 90;
             var width = $(window).width() - 20;
             var dialog = $('<div/>')
             .dialog({
                 draggable: false,
                 modal: true,
                 height: height,
                 //top:0,
                 width: width,
                 title: "引進明細表",
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
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sFWCRMOrdersPeopleCount.FWCRMOrders" runat="server" AutoApply="True"
                DataMember="FWCRMOrders" Pagination="True" QueryTitle="查詢條件"
                Title="" ReportFileName="~/JB_ADMIN/JBERP_R_SalesDetails.rdlc" QueryMode="Panel" DeleteCommandVisible="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="True" OnLoadSuccess="OnLoadSuccessGV" AllowAdd="True" AllowDelete="True" AllowUpdate="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" >
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="iYear" Editor="numberbox" FieldName="iYear" Format="" Width="120" />
                </Columns>                 
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="年度" Condition="=" DataType="string" Editor="text" FieldName="iYear" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" EditorOptions="valueField:'EmpID',textField:'NAME_C',remoteName:'sFWCRMStickStatus.infoSalesID',tableName:'infoSalesID',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="工期" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sFWCRMOrders.infoWorkTime',tableName:'infoWorkTime',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="國籍" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AutoKey',textField:'NationalityName',remoteName:'sFWCRMOrders.infoFWCRMNationality',tableName:'infoFWCRMNationality',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="NationalityID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="雇主" Condition="%%" DataType="string" Editor="text" FieldName="EmployerName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
