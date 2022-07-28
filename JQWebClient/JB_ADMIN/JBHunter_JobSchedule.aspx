<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_JobSchedule.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
                $(queryPanel).panel('resize', { width: 900 });          
            ////執案區間
            //var iDay1 = $('#iDay1_Query').closest('td');
            //var iDay2 = $('#iDay2_Query').closest('td').children();
            ////var JobStatus = $('#JobStatus_Query').closest('td').children();
            //iDay1.append("&nbsp;〜&nbsp;").append(iDay2).append("&nbsp;天");//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;職缺狀態&nbsp;").append(JobStatus);
            //推薦區間
            var SAssignDate_Query = $('#SAssignDate_Query').closest('td');
            var EAssignDate_Query = $('#EAssignDate_Query').closest('td').children();
            SAssignDate_Query.append("&nbsp;〜&nbsp;").append(EAssignDate_Query);
            //面談/推薦顧問
            var AssignHunterID_Query = $('#AssignHunterID_Query').closest('td');
            var AssignID_Query = $('#AssignID_Query').closest('td').children();
            AssignHunterID_Query.append("&nbsp;&nbsp;&nbsp;&nbsp;推薦狀態").append(AssignID_Query);

            $('#Type_Query').options('setValue', 1);
        });        
       
        function OnLoadSuccessGV() {           
            //Grid隱藏
            $('#dataGridView').datagrid('getPanel').hide();

            //查詢條件預設值
            //var dt = new Date();
            //var FirstDate = new Date($.jbGetFirstDate(dt));
            //$("#CreateDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));
            //var LastDate = new Date($.jbGetLastDate(dt));
            //$("#AcceptDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));
            //$('#iday_Query').options('setValue', 1);
            //$('#ViewAreaName_Query').options('setValue', 1);
            ////$('input:radio[name=iday_Query_0][value=1]').attr('checked', true);      
            //$('#SalesTypeID_Query').combobox('setValue', 1);
            $("#JobStatus_Query").combobox('setValue', "");

        }
        function queryGrid(dg) {//查詢後添加固定條件
            
            var Type = $('#Type_Query').options('getCheckedValue');
            var sType = ""; 
            switch (Type)
            {
                case "1":
                    sType = "執案作業進度表";
                    break;
                case "2":
                    sType = "推薦複試報到總表";
                    break;
                case "3":
                    sType = "執案人次表";
                    break;
                case "4":
                    sType = "執案分析表";
                    break;
                default:
                    Type = "1";
                    sType = "執案作業進度表";
                    break;
            }
            
            var HunterID = "";//$("#HunterID_Query").combobox('getValue');//執案顧問
            var HunterIDAssist = "";//$("#HunterIDAssist_Query").combobox('getValue');//助理顧問
            var SDate = "";//$("#SDate_Query").datebox("getValue");//開缺區間
            var EDate = "";//$("#EDate_Query").datebox("getValue");
            var iDay1 = "";//$('#iDay1_Query').val();//執案區間
            var iDay2 = "";//$('#iDay2_Query').val();

            var CustID = $('#CustID_Query').refval('getValue');//客戶查詢
            var JobName = $('#JobName_Query').val();//職缺名稱
            var SalesTeamID = $("#SalesTeamID_Query").combobox('getValue');//業務單位	
            var JobStatus = $("#JobStatus_Query").combobox('getValue');//職缺狀態
            var SADate = $("#SAssignDate_Query").datebox("getValue");//推薦區間
            var EADate = $("#EAssignDate_Query").datebox("getValue");
            var AssignID = $("#AssignID_Query").combobox('getValue');//推薦狀態	
            var AssignHunterID = $("#AssignHunterID_Query").combobox('getValue');//推薦顧問

            var url = "../JB_ADMIN/REPORT/JBHunter/JobScheduleReport.aspx?Type=" + Type + "&CustID=" + CustID + "&JobName=" + JobName + "&SalesTeamID=" + SalesTeamID + "&JobStatus=" + JobStatus + "&HunterID=" + HunterID + "&HunterIDAssist=" + HunterIDAssist +
                "&SDate=" + SDate + "&EDate=" + EDate + "&iDay1=" + iDay1 + "&iDay2=" + iDay2 + "&sType=" + sType + "&SADate=" + SADate + "&EADate=" + EADate + "&AssignID=" + AssignID + "&AssignHunterID=" + AssignHunterID;

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
                title: "執案報表",
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
                            Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="15,30,45,60,65" PageSize="15" QueryAutoColumn="False" QueryLeft="10px" QueryMode="Panel" QueryTop="10px" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" ColumnsHibeable="False" RecordLockMode="None" Width="1050px" BufferView="False" NotInitGrid="False" RowNumbers="True" OnLoadSuccess="OnLoadSuccessGV">
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
                                <JQTools:JQQueryColumn Caption="職缺名稱" Condition="%%" DataType="string" Editor="text" FieldName="JobName" NewLine="False" RemoteMethod="False" Width="105" AndOr="and" Span="0" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="業務單位" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="108" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="職缺狀態" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'開',selected:'false'},{value:'2',text:'關',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:100" FieldName="JobStatus" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="105" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="面談/推薦顧問" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssignHunterID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="105" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'sHUTUser.HUT_ZAssignStep',tableName:'HUT_ZAssignStep',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssignID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="85" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="區間" Condition="%" DataType="string" Editor="datebox" FieldName="SAssignDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" Editor="datebox" FieldName="EAssignDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="報表種類" Condition="%" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:300,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:4,multiSelect:false,openDialog:false,selectAll:false,selectOnly:true,items:[{text:'執案作業進度表',value:'1'},{text:'推薦狀態總表',value:'2'},{text:'執案人次表',value:'3'},{text:'執案分析表',value:'4'}]" FieldName="Type" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="4" Width="125" />
                                
                            </QueryColumns>
                        </JQTools:JQDataGrid>

                    </td>

                </tr>
            </table>
        </div>
       
       

    </form>
</body>
</html>
