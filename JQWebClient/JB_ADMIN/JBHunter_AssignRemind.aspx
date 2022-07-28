<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_AssignRemind.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //設定 Grid QueryColunm panel寬度調整
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 940 });          
            //提醒區間
            var SDate = $('#SDate_Query').closest('td');
            var EDate = $('#EDate_Query').closest('td').children();
            SDate.append("&nbsp;〜&nbsp;").append(EDate);
            //查詢條件預設值
            var dt = new Date();
            var FirstDate = new Date($.jbGetFirstDate(dt));
            var sDate2 = new Date($.jbDateAdd('days', 30, dt));
            var Date2 = $.jbjob.Date.DateFormat(sDate2, 'yyyy/MM/dd');
            $("#SDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd'));
            $("#EDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(sDate2, 'yyyy/MM/dd'));

            var aEDate = $('#EDate_Query').closest('td');
            aEDate.append(" (日期區間之待辦事項)(若無日期條件預設為今天)");

            ////預設推薦顧問,用戶編號帶入
            //var sUserID = getClientInfo("UserID");
            //setTimeout(function () {
            //    var data = $("#AssignHunterID_Query").combobox('getData');
            //    for (var i = 0; i < data.length; i++) {
            //        if (data[i].EmpID == sUserID) {
            //            if (data[i].IsSales == false) {//是否是業務
            //                $("#AssignHunterID_Query").combobox('setValue', data[i].ID);
            //            } else $("#SalesID_Query").combobox('setValue', data[i].ID);
            //        }
            //    }
                
            //}, 300);
           


        });        
       
        function OnLoadSuccessGV() {           

        }
        function queryGrid(dg) {//查詢後添加固定條件
            
            var CustID = $('#CustID_Query').refval('getValue');//客戶查詢
            var AssignID = $("#AssignID_Query").combobox('getValue');//推薦狀態	
            var SDate = $("#SDate_Query").datebox("getValue");//推薦區間
            var EDate = $("#EDate_Query").datebox("getValue");
            var SalesTeamID = $("#SalesTeamID_Query").combobox('getValue');//業務單位	
            var SalesID = $("#SalesID_Query").combobox('getValue');//客戶業務	
            var AssignHunterID = $("#AssignHunterID_Query").combobox('getValue');//推薦顧問

            //var url = "../JB_ADMIN/REPORT/JBHunter/AssignRemindReport.aspx?CustID=" + CustID + "&AssignID=" + AssignID + "&SDate=" + SDate + "&EDate=" + EDate + "&SalesTeamID=" + SalesTeamID
            //    + "&SalesID=" + SalesID + "&AssignHunterID=" + AssignHunterID;

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sHUTUser.HUT_User', //連接的Server端，command
                data: "mode=method&method=" + "JobAssignLogsRemind" + "&parameters=" + CustID + "*" + AssignID + "*" + SalesTeamID + "*" + AssignHunterID + "*" + SalesID + "*" + SDate + "*" + EDate , //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: true,
                success: function (data1) {
                    var rows = $.parseJSON(data1);//將JSon轉會到Object類型提供給Grid顯示
                    var data = new Object();
                    data.rows = rows;
                    if (rows == null) {
                        $('#dataGridView').datagrid('loadData', []); //Grid清空資料         
                        alert("目前無資料！");
                    } else {
                        data.total = rows.length;
                        $('#dataGridView').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                    }
                }
            });


        }
        //提醒說明
        function FormatScriptsAlertTime(val, rowData) {
            return "<div style='font-weight:bold;color:red;'> " + val + "</div>";
        }
        function openDesc() {
            var sDesc = '待辦作業查詢日期規則(以工作天計算)：<br>' +
            '<br>1.推薦：推薦後1天無任何狀態紀錄(假日往後延)；' +
            '<br>2.面試：推薦後1天無任何狀態紀錄(假日往後延)；' +
            '<br>3.錄取：推薦後1天無任何狀態紀錄(假日往後延)；' +
            '<br>4.報到：關聯會計是否開發票,之後1天無任何狀態紀錄(假日往後延)；' +
            '<br>5.離職：往前抓前一筆報到的日期,二者相減<=保證天數,可(1)遞補 或 (2)退費 處理；'+
            '<br>&nbsp;&nbsp;&nbsp;(1)遞補：需於遞補者報到狀態記錄，選擇被遞補的人才; '+
            '<br>&nbsp;&nbsp;&nbsp;(2)退費：需於離職狀態記錄，選擇異常申請單單據'
            //$.messager.alert('提醒日期說明', sDesc, '');

            var light = "<span style='font-family:Microsoft JhengHei;font-weight: bold;'>";
            var right = "</span>";
            $.messager.show({
                title: '待辦作業說明',
                msg: sDesc,
                style: { left: 220, top: 125 },     //設置彈框的位置
                width: 480,                           //設置彈框的寬度和高度
                height: 190
            });

        }
        //---------------呼叫開啟人才 Tab--------------------------------------------------------------------------------
        function OpenUserTab(value, row, index) {
            if (value == undefined) ""
            else if (value != "0")
                return "<a href='javascript: void(0)' onclick='LinkUserTab(" + index + ");' >" + value + "</a>";
            else return value;
        }
        function LinkUserTab(index) {
            $("#dataGridView").datagrid('selectRow', index);
            var rows = $("#dataGridView").datagrid('getSelected');
            var NameC = rows.NameC;
            parent.addTab('履歷資料維護', './JB_ADMIN/JBHunter_Users.aspx?NameC=' + NameC);
        }


    </script>
   
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td class="auto-style1">
                        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
                        <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCustomersJobs.HUT_Job" runat="server" AutoApply="True" 
                            DataMember="HUT_Job" Pagination="True" QueryTitle="查詢條件" EditDialogID=""
                            Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="15,30,45,60" PageSize="15" QueryAutoColumn="False" QueryLeft="10px" QueryMode="Panel" QueryTop="10px" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ColumnsHibeable="False" RecordLockMode="None" Width="940px" BufferView="False" NotInitGrid="False" RowNumbers="True" OnLoadSuccess="OnLoadSuccessGV" Height="415px">
                            <Columns>
                                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="True" Visible="True" Width="200" FormatScript="" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="220">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Editor="text" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Caption="人才姓名" FieldName="NameC" FormatScript="OpenUserTab">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="推薦顧問" Editor="text" EditorOptions="" FieldName="HunterName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="業務" Editor="text" EditorOptions="" FieldName="SalesName" Visible="True" Width="80" Format="" FormatScript="" Sortable="True" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="推薦狀態" Editor="text" FieldName="AssignName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="狀態日期" Editor="text" FieldName="AssignTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="" Visible="True" Width="80" FormatScript="FormatScriptsAlertTime">
                                    </JQTools:JQGridColumn>
                            </Columns>
                            <TooItems>
                            </TooItems>
                            <QueryColumns>
                                
<%--                                <JQTools:JQQueryColumn AndOr="and" Caption="執案區間" Condition="%" DataType="string" Editor="numberbox" FieldName="iDay1" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="40" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" Editor="numberbox" FieldName="iDay2" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="40" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="開缺區間" Condition="%" DataType="string" Editor="datebox" FieldName="SDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="〜" Condition="%" DataType="string" Editor="datebox" FieldName="EDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="執案顧問" Condition="=" DataType="string" DefaultValue="" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="105" Span="2" />
                                --%>
                                <JQTools:JQQueryColumn Caption="客戶查詢" Condition="=" DataType="string" Editor="inforefval" FieldName="CustID" NewLine="False" RemoteMethod="False" Width="220" AndOr="" EditorOptions="title:'選擇客戶',panelWidth:390,remoteName:'sCustomersJobs.View_HUT_Customer',tableName:'View_HUT_Customer',columns:[{field:'CustTaxNo',title:'統一編號',width:95,align:'center',table:'',isNvarChar:false,queryCondition:''},{field:'CustName',title:'客戶名稱',width:250,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustID',textField:'CustName',valueFieldCaption:'客戶編號',textFieldCaption:'客戶名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'true'" RowSpan="0" Span="2" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="查詢區間" Condition="%" DataType="string" Editor="datebox" FieldName="SDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="2" Width="90" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" Editor="datebox" FieldName="EDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="業務單位" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="%" DataType="string" Editor="infocombobox" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                 <JQTools:JQQueryColumn AndOr="and" Caption="推薦顧問" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssignHunterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                 <JQTools:JQQueryColumn AndOr="and" Caption="推薦狀態" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AssignID',textField:'AssignName',remoteName:'sHUTUser.HUT_ZAssignStep',tableName:'HUT_ZAssignStep',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssignID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="75" />
                            </QueryColumns>
                        </JQTools:JQDataGrid>

                    </td>
                     <td style="vertical-align: top"><a href="#" class="easyui-linkbutton" data-options="" onclick="openDesc()">說明</a></td>
                     

                </tr>
            </table>
        </div>
       
       

    </form>
</body>
</html>
