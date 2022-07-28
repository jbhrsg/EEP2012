<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_ReportJobRequire.aspx.cs" Inherits="Template_JQueryQuery1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script> 
    <script src="../js/jquery.url.js"></script> 
     <script>   
         $(document).ready(function () {
             
         });
         
         //兜查詢條件-----------------------------------------------------------
         function queryGrid(dg) {
             if ($(dg).attr('id') == 'dataGridMaster') {
                 //查詢條件
                 var result = [];
                 var CustID = $('#CustID_Query').refval('getValue');//客戶名稱
                 var JobName = $('#JobName_Query').val();//職缺名稱
                 var HunterID = $('#HunterID_Query').combobox('getValue');//執案顧問                             
                 var SalesTeamID = $('#SalesTeamID_Query').combobox('getValue');//業務單位  

                 if (CustID != '') result.push("c.CustID = '" + CustID + "'");
                 if (JobName != '') result.push("j.JobName like '%" + JobName + "%'");
                 if (HunterID != '') result.push("j.HunterID = " + HunterID);
                 if (SalesTeamID != '') result.push("j.SalesTeamID = " + SalesTeamID);

                 $(dg).datagrid('setWhere', result.join(' and '));
             }
         }

         //---------------呼叫開啟Job Tab--------------------------------------------------------------------------------
         function OpenJobTab(value, row, index) {
             return "<a href='javascript: void(0)' onclick='LinkJobTab(" + index + ");' style='color:blue;'>" + value + "</a>";
         }
         function LinkJobTab(index) {
             $("#dataGridMaster").datagrid('selectRow', index);
             var rows = $("#dataGridMaster").datagrid('getSelected');
             var CustID = rows.CustID;
             parent.addTab('職缺資料維護', './JB_ADMIN/JBHunter_Jobs.aspx?CustID=' + CustID);
         }

         //--------------職缺通報-----------------------------------------------------------------------------------------------       
         function OpenJobRequire() {
             var CustID = $('#CustID_Query').refval('getValue');//客戶名稱
             var JobName = $('#JobName_Query').val();//職缺名稱
             var HunterID = $('#HunterID_Query').combobox('getValue');//執案顧問                             
             var SalesTeamID = $('#SalesTeamID_Query').combobox('getValue');//業務單位  
             var SalesTeamText = $('#SalesTeamID_Query').combobox('getText') + "職缺通報";//業務單位文字  

             var url = "../JB_ADMIN/REPORT/JBHunter/JobRequireReport.aspx?CustID=" + CustID + "&JobName=" + JobName + "&HunterID=" + HunterID + "&SalesTeamID=" + SalesTeamID + "&SalesTeamText=" + SalesTeamText;

             var height = $(window).height() - 20;//Dialog 高
             var height2 = $(window).height() - 90;
             var width = $(window).width() - 100; //Dialog 寬
             var dialog = $('<div/>')
             .dialog({
                 draggable: false,
                 modal: true,
                 height: height,
                 //top:0,
                 width: width,
                 title: "職缺通報",
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
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sReportJobRequire.ReportJobRequire" runat="server" AutoApply="True"
                DataMember="ReportJobRequire" Pagination="True" QueryTitle="條件查詢"
                Title="內部職缺通報" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" ViewCommandVisible="False" AllowAdd="True" CheckOnSelect="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="False" ColumnsHibeable="False" RecordLockMode="None" OnLoadSuccess="queryGrid" ReportFileName="~/JB_Hunter/rJobRequire.rdlc" BufferView="False" NotInitGrid="False" RowNumbers="True" Width="1050px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="保密" Editor="text" FieldName="JobKeepType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="急迫性" Editor="text" FieldName="Urgency" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="需協助" Editor="text" FieldName="bHelp" Visible="True" Width="40" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="開缺日期" Editor="text" EditorOptions="" FieldName="JobDeclareDate" Format="" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="65">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="執案顧問" Editor="text" EditorOptions="" FieldName="HunterName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="85" FormatScript="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="產業別/產品" Editor="text" FieldName="CategoryName" Width="150" Visible="True" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="center" Caption="缺額" Editor="text" FieldName="JobNeedCount" MaxLength="0" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="所需條件" Editor="text" FieldName="JobRequirement" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職務說明" Editor="text" FieldName="JobWorkContent" MaxLength="0" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="薪資福利" Editor="text" FieldName="JobFare" MaxLength="0" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="工作地點" Editor="text" FieldName="JobWorkArea" MaxLength="0" Width="110" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="JobNotes" MaxLength="0" Width="80" />
                </Columns>
                <TooItems>
                  
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" Visible="False" />
                  
                    <JQTools:JQToolItem Enabled="True" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="OpenJobRequire" Text="報表" Visible="True" />
                  
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn Caption="客戶查詢" Condition="=" DataType="string" Editor="inforefval" FieldName="CustID" NewLine="False" RemoteMethod="False" Width="150" EditorOptions="title:'選擇客戶',panelWidth:390,remoteName:'sCustomersJobs.View_HUT_Customer',tableName:'View_HUT_Customer',columns:[{field:'CustTaxNo',title:'統一編號',width:95,align:'center',table:'',isNvarChar:false,queryCondition:''},{field:'CustName',title:'客戶名稱',width:250,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustID',textField:'CustName',valueFieldCaption:'客戶編號',textFieldCaption:'客戶名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'true'" />
                    <JQTools:JQQueryColumn Caption="職缺名稱" Condition="%" DataType="string" Editor="text" FieldName="JobName" NewLine="False" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="執案顧問" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sHunter.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="110" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務單位" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" />
<%--                    <JQTools:JQQueryColumn AndOr="and" Caption="開缺日期" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="JobSDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="85" DefaultMethod="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="關閉日期" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="JobEDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="85" DefaultMethod="" DefaultValue="" />--%>
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
