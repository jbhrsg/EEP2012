<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoomCustReport.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
             var DormID = Request.getQueryStringByName("DormID");
             //選擇宿舍不可更改
             //$("#DormID_Query").combobox("disable");
             $("#DormID_Query").combobox('setValue', 'JCS1');
            
             //$("#RoomID_Query").options('setValue', 0);
         }

         function queryGrid(dg) {//查詢後添加固定條件
             var DormID = $("#DormID_Query").combobox('getValue');//選擇宿舍	               
             var CustomerID = $("#CustomerID_Query").combobox('getValue');//客戶名稱
             var RoomID = $("#RoomID_Query").numberbox('getValue');//房號	

             var url = "../JB_JCS/REPORT/JCS/RoomCustReportView.aspx?DormID=" + DormID + "&CustID=" + CustomerID + "&RoomID=" + RoomID;
            
            var height = $(window).height() - 50;
            var height2 = $(window).height()- 92;
             var width = $(window).width() - 250;
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

         function OnSelectDormID() {
             var DormID = $("#DormID_Query").combobox('getValue');//選擇宿舍	               
             $('#CustomerID_Query').combobox('setWhere', "sDorm='" + DormID+"'");
         }


    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" AgentDatabase="JBADMIN" AgentSolution="JBADMIN" AgentUser="A0123" />


&nbsp;<div id="Dialog_dataGridMasterData">
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sJCS.infoRoomCustList" runat="server" AutoApply="True"
                DataMember="infoRoomCustList" Pagination="True" QueryTitle="查詢條件"
                Title="" ReportFileName="~/JB_JCS/REPORT/JCS/RoomCustList.rdlc" QueryMode="Panel" DeleteCommandVisible="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="True" OnLoadSuccess="OnLoadSuccessGV" AllowAdd="True" AllowDelete="True" AllowUpdate="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" >
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨年月" Editor="text" FieldName="YearMonth" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerID" MaxLength="0" Width="80" />
                </Columns>                 
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="選擇宿舍" Condition="%" DataType="string" Editor="infocombobox" FieldName="DormID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'DormID',textField:'DormName',remoteName:'sJCS.infoDorm',tableName:'infoDorm',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectDormID,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶名稱" Condition="%" DataType="string" Editor="infocombobox" FieldName="CustomerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" EditorOptions="valueField:'CustomerID',textField:'CustomerShortName',remoteName:'sJCS.infoCustomers',tableName:'infoCustomers',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="房號" Condition="%%" DataType="string" Editor="numberbox" FieldName="RoomID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="60" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
