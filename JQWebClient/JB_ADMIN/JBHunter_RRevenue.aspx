<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_RRevenue.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
                $(queryPanel).panel('resize', { width: 1100 });

            //推薦區間
            var SAssignDate_Query = $('#SAssignDate_Query').closest('td');
            var EAssignDate_Query = $('#EAssignDate_Query').closest('td').children();
            SAssignDate_Query.append("&nbsp;〜&nbsp;").append(EAssignDate_Query);

            //成案機率
            var DraftS = $('#DraftS_Query').closest('td');
            var DraftE = $('#DraftE_Query').closest('td').children();
            DraftS.append("%&nbsp;〜&nbsp;").append(DraftE).append("%");
          
            //$('#Type_Query').options('setValue', 1);

            //設定 推薦說明紀錄 dialog
            initAssignExplainDialog();

        });        
       
        function OnLoadSuccessGV() {           
            //Grid隱藏
            //$('#dataGridView').datagrid('getPanel').hide();

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
     
        function RevenueReport() {
            //查詢條件
            var result = [];
            var CustID = $('#CustID_Query').refval('getValue');//客戶查詢
            var JobName = $('#JobName_Query').val();//職缺名稱
            var HunterID = $("#HunterID_Query").combobox('getValue');//執案顧問
            var SalesTeamID = $("#SalesTeamID_Query").combobox('getValue');//業務單位	
            var AssignHunterID = $("#AssignHunterID_Query").combobox('getValue');//推薦顧問
            var SADate = $("#SAssignDate_Query").datebox("getValue");//推薦區間
            var EADate = $("#EAssignDate_Query").datebox("getValue");
            var DraftS = $("#DraftS_Query").val();//成案機率
            var DraftE = $("#DraftE_Query").val();

            //if (SADate != "") {
            //    sType = sType + " " + SADate + "～" + EADate;
            //}
            //if (Type == "") {
            //    sType = "1";
            //}

            //if (CustID != '') result.push("j.CustID = '" + CustID + "'");
            //if (JobName != '') result.push("j.JobName like '%" + JobName + "%'");
            //if (HunterID != '') result.push("j.HunterID = " + HunterID);
            //if (SalesTeamID != '') result.push("j.SalesTeamID = " + SalesTeamID);

            //if (SADate != '') result.push("Convert(nvarchar(10),a.AssignTime,111) between '" + SADate + "' and '" + EADate+"'");
            //if (AssignHunterID != '') result.push("a.HunterID = " + HunterID);
            //if (DraftS != '') result.push("a.Draft between " + DraftS + " and " + DraftE);

            //$('#dataGridView').datagrid('setWhere', result.join(' and '));


            //var Type = $('#Type_Query').options('getCheckedValue');
            //var sType = "";
            //switch (Type) {
            //    case "1":
            //        sType = "執案預估營收";
            //        break;
            //    case "2":
            //        sType = "營收目標進度";
            //        break;

            //}


            var url = "../JB_ADMIN/REPORT/JBHunter/RevenueReport.aspx?Type=1&CustID=" + CustID + "&JobName=" + JobName + "&HunterID=" + HunterID + "&SalesTeamID=" + SalesTeamID +
                "&AssignHunterID=" + AssignHunterID + "&SADate=" + SADate + "&EADate=" + EADate + "&DraftS=" + DraftS + "&DraftE=" + DraftE ;

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
                title: "報表",
                //maximizable: true                              
            });
                $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="' + height2 + '"></iframe>').appendTo(dialog.find('.panel-body'));
                dialog.dialog('open');

        }

        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridView') {
                RevenueQuery();
            }
        }

        function RevenueQuery() {
            var result = [];
            var CustID = $('#CustID_Query').refval('getValue');//客戶查詢
            var JobName = $('#JobName_Query').val();//職缺名稱
            var HunterID = $("#HunterID_Query").combobox('getValue');//執案顧問
            var SalesTeamID = $("#SalesTeamID_Query").combobox('getValue');//業務單位	
            var AssignHunterID = $("#AssignHunterID_Query").combobox('getValue');//推薦顧問
            var SADate = $("#SAssignDate_Query").datebox("getValue");//推薦區間
            var EADate = $("#EAssignDate_Query").datebox("getValue");
            var DraftS = $("#DraftS_Query").val();//成案機率
            var DraftE = $("#DraftE_Query").val();
            var Type = 1;
            //var sType = "";
            //switch (Type) {
            //    case "1":
            //        sType = "執案預估營收";
            //        break;
            //    case "2":
            //        sType = "營收目標進度";
            //        break;

            //}

            var sType = "1"
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sHUTUser.HUT_User', //連接的Server端，command
                data: "mode=method&method=" + "procDisplayRevenue" + "&parameters=" + encodeURIComponent(Type + "*" + CustID + "*" + JobName + "*" + HunterID + "*" + SalesTeamID + "*" + SADate + "*" + EADate +
                     "*" + DraftS + "*" + DraftE + "*" + AssignHunterID + "*1"), //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
                        //$('#dataGridView').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                        $('#dataGridView').datagrid({ loadFilter: pagerFilter, url: "", pageNumber: 1 }).datagrid('loadData', rows);

                    }
                }
            });

        }

        //---------------------------------------查詢結果匯出Excel---------------------------------------
        function AutoExcel() {
            //查詢條件
            var CustID = $('#CustID_Query').refval('getValue');//客戶查詢
            var JobName = $('#JobName_Query').val();//職缺名稱
            var HunterID = $("#HunterID_Query").combobox('getValue');//執案顧問
            var SalesTeamID = $("#SalesTeamID_Query").combobox('getValue');//業務單位	
            var AssignHunterID = $("#AssignHunterID_Query").combobox('getValue');//推薦顧問
            var SADate = $("#SAssignDate_Query").datebox("getValue");//推薦區間
            var EADate = $("#EAssignDate_Query").datebox("getValue");
            var DraftS = $("#DraftS_Query").val();//成案機率
            var DraftE = $("#DraftE_Query").val();
            var Type = 1;                 

            $.ajax({
                url: '../handler/JBHRISUseCaseHandler.ashx?' + $.param({ mode: 'CallServerMethodReturnFile', remoteName: 'sHUTUser', method: 'procExcelRevenue' }),
                //data: { parameters: $.toJSONString(data) },

                data: "&parameters=" + Type + "*" + CustID + "*" + JobName + "*" + HunterID + "*" + SalesTeamID + "*" + SADate + "*" + EADate +
                     "*" + DraftS + "*" + DraftE + "*" + AssignHunterID + "*2",

                type: 'POST',
                async: true,
                success: function (data) {
                    //Json.FileName
                    var Json = $.parseJSON(data);
                    if (Json.IsOK) {
                        var Url = $('<a>', {
                            href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: '執案預估營收.xls', FilePathName: Json.FileStreamOrFileName }),
                            target: '_blank'

                        }).html('檔案下載')[0].outerHTML;

                        $.messager.alert('下載', Url, '');

                    }

                    else $.messager.alert('錯誤', Json.Msg, 'error');

                },

                beforeSend: function () { $.messager.progress({ msg: '執行中...' }); },

                complete: function () { $.messager.progress('close'); },

                error: function (xhr, ajaxOptions, thrownError) { alert('error'); }

            });
        }

        // 推薦說明紀錄 dialog
        function initAssignExplainDialog() {
            $("#Dialog_Explain").dialog(
            {
                height: 400,
                width: 850,
                left: 100,
                top: 80,
                resizable: false,
                modal: true,
                title: "推薦說明紀錄",
                closed: true
            });
        };
        //推薦說明紀錄
        function AssignExplainLink(value, row, index) {
            return $('<a>', { href: 'javascript:void(0)', name: 'AssignExplainLink', onclick: 'LinkAssignExplain.call(this)', rowIndex: index }).linkbutton({ plain: false, text: '推薦說明' })[0].outerHTML
        }

        // open推薦說明紀錄 dialog
        function LinkAssignExplain() {
            //alert(index)
            var index = $(this).attr('rowIndex');
            $("#dataGridView").datagrid('selectRow', index);
            var rows = $("#dataGridView").datagrid('getSelected');
            var UserID = rows.UserID;
            var JobID = rows.JobID;
            var AssignTime = rows.AssignTime;

            $("#dataGrid_Explain").datagrid('setWhere', "UserID = '" + UserID + "' and JobID='" + JobID + "' and AssignTime>'" + AssignTime + "'");
            $("#Dialog_Explain").dialog("open");
        }

        //完整顯示Grid聯繫紀錄
        function ShowAllGrid(value) {
            return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
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
                        <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sHUTUser.infoReportRevenue" runat="server" AutoApply="True" 
                            DataMember="infoReportRevenue" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                            Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="15,30,45,60,65" PageSize="15" QueryAutoColumn="False" QueryLeft="10px" QueryMode="Panel" QueryTop="10px" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ColumnsHibeable="False" RecordLockMode="None" Width="1100px" BufferView="False" NotInitGrid="False" RowNumbers="True" OnLoadSuccess="OnLoadSuccessGV" Height="400px">
                            <Columns>
                                    <JQTools:JQGridColumn Alignment="center" Caption="執案顧問" Editor="text" EditorOptions="" FieldName="HunterName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="70" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="人才姓名" Editor="text" FieldName="NameC" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="True" Width="75" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" EditorOptions="" FieldName="ShortCustName" Width="90" Visible="True" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="250" />
                                <JQTools:JQGridColumn Alignment="center" Caption="推薦日期" Editor="datebox" FieldName="AssignTime" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" FormatScript="" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="推薦顧問" Editor="text" FieldName="AssignHunter" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="預計成案年月" Editor="text" EditorOptions="" FieldName="DraftMonth" Visible="True" Width="85" Format="" FormatScript="" Sortable="True" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="成案機率％" Editor="numberbox" FieldName="Draft" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="" Visible="True" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="right" Caption="預估營業額" Editor="numberbox" FieldName="EstimateTurnover" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="" Visible="True" Width="75" Format="N">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="說明" Editor="text" FieldName="AssignExplain" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="180">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption=" " Editor="text" FieldName="AssignExplainLink" FormatScript="AssignExplainLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                                    </JQTools:JQGridColumn>
                            </Columns>
                           <TooItems>
                                <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="AutoExcel" Text="匯出資料" Visible="True" />
                                <JQTools:JQToolItem ID="JQToolItem2" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="RevenueReport" Text="匯出報表"  />
                            </TooItems>
                            <QueryColumns>
                                    <JQTools:JQQueryColumn AndOr="and" Caption="業務單位" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />                            
                                <JQTools:JQQueryColumn Caption="客戶查詢" Condition="=" DataType="string" Editor="inforefval" FieldName="CustID" NewLine="False" RemoteMethod="False" Width="220" AndOr="" EditorOptions="title:'選擇客戶',panelWidth:390,remoteName:'sCustomersJobs.View_HUT_Customer',tableName:'View_HUT_Customer',columns:[{field:'CustTaxNo',title:'統一編號',width:95,align:'center',table:'',isNvarChar:false,queryCondition:''},{field:'CustName',title:'客戶名稱',width:250,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustID',textField:'CustName',valueFieldCaption:'客戶編號',textFieldCaption:'客戶名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'true'" RowSpan="0" Span="2" />
                                <JQTools:JQQueryColumn Caption="職缺名稱" Condition="%%" DataType="string" Editor="text" FieldName="JobName" NewLine="False" RemoteMethod="False" Width="105" AndOr="and" Span="0" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="執案顧問" Condition="=" DataType="string" DefaultValue="" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" Span="0" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="推薦區間" Condition="%" DataType="string" Editor="datebox" FieldName="SAssignDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" Editor="datebox" FieldName="EAssignDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />                                
                                <JQTools:JQQueryColumn AndOr="and" Caption="推薦顧問" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssignHunterID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                                    <JQTools:JQQueryColumn AndOr="and" Caption="成案機率" Condition="=" DataType="number" Editor="text" FieldName="DraftS" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
                                    <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="=" DataType="number" Editor="text" FieldName="DraftE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
<%--                                <JQTools:JQQueryColumn AndOr="and" Caption="報表種類" Condition="%" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:210,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:4,multiSelect:false,openDialog:false,selectAll:false,selectOnly:true,items:[{text:'執案預估營收',value:'1'}]" FieldName="Type" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="2" Width="125" />--%>
                                
                            </QueryColumns>
                        </JQTools:JQDataGrid>

                    </td>

                </tr>
            </table>
        </div>
        <JQTools:JQDialog ID="Dialog_Explain" runat="server" BindingObjectID="" Title="推薦記錄說明" DialogLeft="80px" DialogTop="50px" Width="750px" Wrap="False" EditMode="Dialog" ShowSubmitDiv="False">
              <JQTools:JQDataGrid ID="dataGrid_Explain" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" data-options="pagination:true,view:commandview" DataMember="infoReportRevenueExplain" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="Dialog_Explain" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnLoadSuccess="OnLoadSuccessGV" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="80px" QueryMode="Panel" QueryTitle="查詢條件" QueryTop="80px" RecordLock="False" RecordLockMode="None" RemoteName="sHUTUser.infoReportRevenueExplain" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="100%">
                  <Columns>
                      <JQTools:JQGridColumn Alignment="center" Caption="推薦日期" Editor="datebox" FieldName="AssignTime" Format="" Visible="True" Width="120" ReadOnly="False" />
                      <JQTools:JQGridColumn Alignment="left" Caption="推薦說明" Editor="text" FieldName="AssignExplain" Visible="True" Width="600" MaxLength="0" FormatScript="ShowAllGrid" />
                  </Columns>
              </JQTools:JQDataGrid>

         </JQTools:JQDialog>
       
       

    </form>
</body>
</html>
