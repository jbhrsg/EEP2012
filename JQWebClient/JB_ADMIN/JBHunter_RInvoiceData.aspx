<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_RInvoiceData.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {
            //設定 Grid QueryColunm panel寬度調整
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 800 });          
            //發票區間
            var SDate = $('#SDate_Query').closest('td');
            var EDate = $('#EDate_Query').closest('td').children();
            SDate.append("&nbsp;〜&nbsp;").append(EDate);
           
        });        
       
        function OnLoadSuccessGV() {           
            //Grid隱藏
            $('#dataGridView').datagrid('getPanel').hide();

        }
        function queryGrid(dg) {//查詢後添加固定條件
            
            var CustID = $('#CustID_Query').refval('getValue');//客戶查詢
            var SDate = $("#SDate_Query").datebox("getValue");//推薦區間
            var EDate = $("#EDate_Query").datebox("getValue");
            var SalesTeamID = $("#SalesTeamID_Query").combobox('getValue');//業務單位	
            var HunterID = $("#HunterID_Query").combobox('getValue');//執案顧問	
            var AssignHunterID = $("#AssignHunterID_Query").combobox('getValue');//推薦顧問

            var url = "../JB_ADMIN/REPORT/JBHunter/InvoiceDataReport.aspx?CustID=" + CustID + "&SDate=" + SDate + "&EDate=" + EDate + "&SalesTeamID=" + SalesTeamID
                + "&HunterID=" + HunterID + "&AssignHunterID=" + AssignHunterID;

            var height = $(window).height() - 40;
            var height2 = $(window).height() - 80;
            var width = $(window).width() - 90;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                //top:0,
                width: width,
                title: "銷貨發票總表",
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
            <table>
                <tr>
                    <td colspan="2" class="auto-style1">
                        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
                        <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCustomersJobs.HUT_Job" runat="server" AutoApply="True" 
                            DataMember="HUT_Job" Pagination="True" QueryTitle="查詢條件" EditDialogID=""
                            Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="15,30,45,60,65" PageSize="15" QueryAutoColumn="False" QueryLeft="10px" QueryMode="Panel" QueryTop="10px" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" ColumnsHibeable="False" RecordLockMode="None" Width="750px" BufferView="False" NotInitGrid="False" RowNumbers="True" OnLoadSuccess="OnLoadSuccessGV">
                            <Columns>
                                    <JQTools:JQGridColumn Alignment="right" Caption="職缺代號" Editor="numberbox" FieldName="JobID" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="False" Width="60" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="客戶" Editor="text" EditorOptions="" FieldName="CustID" Width="120" Visible="False" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="180" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="推薦人數" Editor="text" FieldName="iUser" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60"></JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="True" Visible="True" Width="150" FormatScript="" />
                                    <JQTools:JQGridColumn Alignment="left" Editor="text" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="65" Caption="聯繫人員" FieldName="UpdateBy">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="執案顧問" Editor="text" EditorOptions="" FieldName="HunterName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="開/關缺日" Editor="text" EditorOptions="" FieldName="DateString" Visible="True" Width="78" Format="" FormatScript="" Sortable="True" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="執案天數" Editor="text" FieldName="sDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="" Visible="True" Width="55">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="right" Caption="預估營業額" Editor="numberbox" FieldName="Amount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="sum" Visible="True" Width="70" Format="N">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="需求數" Editor="numberbox" FieldName="JobNeedCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="48">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="上班地點" Editor="text" FieldName="JobWorkArea" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="更新日期" Editor="datebox" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="64" Format="yyyy/mm/dd">
                                    </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="更新人員" Editor="text" EditorOptions="" FieldName="LastUpdateBy" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="60" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增職缺" />
<%--                                <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢條件" />                                                                                                                            --%>
                                <JQTools:JQToolItem ID="JQToolItem2" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="OpenNAR" Text="NAR"  />
                            </TooItems>
                            <QueryColumns>
                                
<%--                                <JQTools:JQQueryColumn AndOr="and" Caption="執案區間" Condition="%" DataType="string" Editor="numberbox" FieldName="iDay1" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="40" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" Editor="numberbox" FieldName="iDay2" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="40" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="開缺區間" Condition="%" DataType="string" Editor="datebox" FieldName="SDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="〜" Condition="%" DataType="string" Editor="datebox" FieldName="EDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="執案顧問" Condition="=" DataType="string" DefaultValue="" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="105" Span="2" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="助理顧問" Condition="%" DataType="string" Editor="infocombobox" FieldName="HunterIDAssist" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="2" Width="105" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />--%>
                                <JQTools:JQQueryColumn Caption="客戶查詢" Condition="=" DataType="string" Editor="inforefval" FieldName="CustID" NewLine="False" RemoteMethod="False" Width="200" AndOr="" EditorOptions="title:'選擇客戶',panelWidth:390,remoteName:'sCustomersJobs.View_HUT_Customer',tableName:'View_HUT_Customer',columns:[{field:'CustTaxNo',title:'統一編號',width:95,align:'center',table:'',isNvarChar:false,queryCondition:''},{field:'CustName',title:'客戶名稱',width:250,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustID',textField:'CustName',valueFieldCaption:'客戶編號',textFieldCaption:'客戶名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'true'" RowSpan="0" Span="2" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="發票區間" Condition="%" DataType="string" Editor="datebox" FieldName="SDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" Editor="datebox" FieldName="EDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="業務單位" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="108" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="執案顧問" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="推薦顧問" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssignHunterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                                
                            </QueryColumns>
                        </JQTools:JQDataGrid>

                    </td>

                </tr>
            </table>
        </div>
       
       

    </form>
</body>
</html>
