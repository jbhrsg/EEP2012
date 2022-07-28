<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JOBPublishingRecord.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        ///=============================================  ready  ===============================================================================================
        $(document).ready(function () {
            //初始化查詢條件
            var dt = new Date();
            var FirstDate = new Date($.jbGetFirstDate(dt));                          
            var LastDate = new Date($.jbGetLastDate(dt));          
            $("#SDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));//開始日期
            $("#EDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));//結束日期
            $("#SDate2_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));//開始日期
            $("#EDate2_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));//結束日期
        });
        
        function queryGrid(dg) {//查詢後添加固定條件 
            //點擊統計
            if ($(dg).attr('id') == 'dataGridMaster') {                   
                SetqueryGrid();
            } else if ($(dg).attr('id') == 'dataGridMaster2') {
                //廣告統計
                SetqueryGrid2();
            }
           
        }
        //點擊統計
        function SetqueryGrid() {
            var SDate = $("#SDate_Query").datebox("getValue");        
            var EDate = $("#EDate_Query").datebox("getValue");          
            var sCust = $("#TypeText_Query").val();//客戶關鍵字           
            var aAccount = $("#iCount_Query").val();//帳號
        
            var dataGrid = $('#dataGridMaster');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sJOB0800.PublishingRecord', //連接的Server端，command
                data: "mode=method&method=" + "GePublishingRecordInfo" + "&parameters=" + SDate + "," + EDate + "," + sCust + "," + aAccount, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示
                    //$('#dataGridDetail').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                    if (rows.length > 0) {
                        dataGrid.datagrid('loadData', { "total": 0, "rows": [] });
                        data = eval('(' + data + ')');
                        var appandRows = [];

                        for (var j = 0; j < data.length; j++) {
                            appandRows.push({ Type: data[j].Type, TypeText: data[j].TypeText, iCount: data[j].iCount });
                        }
                        for (var i = 0; i < appandRows.length; i++) {
                            dataGrid.datagrid("appendRow", appandRows[i]);
                        }

                    }else {
                        dataGrid.datagrid('loadData', []); //清空資料
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }
        //點擊統計人數
        function iCountLink(value, row, index) {
            if (value != 0)
                return "<a href='javascript: void(0)' onclick='LinkiCount(" + index + ");'>" + value + "</a>";
            else return value;
        }

        // open點擊統計紀錄畫面 dialog
        function LinkiCount(index) {
            var SDate = $("#SDate_Query").datebox("getValue");
            var EDate = $("#EDate_Query").datebox("getValue");
            var sCust = $("#TypeText_Query").val();//客戶關鍵字           
            var aAccount = $("#iCount_Query").val();//帳號

            $("#dataGridMaster").datagrid('selectRow', index);
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var Type = rows.Type;
            var dataGrid = $('#dataGrid_iCount');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sJOB0800.PublishingRecord', //連接的Server端，command
                data: "mode=method&method=" + "GePublishingRecordInfoData" + "&parameters=" + SDate + "," + EDate + "," + sCust + "," + aAccount + "," + Type, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示
                    //$('#dataGridDetail').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                    if (rows.length > 0) {
                        dataGrid.datagrid('loadData', { "total": 0, "rows": [] });
                        data = eval('(' + data + ')');
                        var appandRows = [];

                        for (var j = 0; j < data.length; j++) {
                            appandRows.push({ DisplayName: data[j].DisplayName, Email: data[j].Email, CreateTime: data[j].CreateTime });
                        }
                        for (var i = 0; i < appandRows.length; i++) {
                            dataGrid.datagrid("appendRow", appandRows[i]);
                        }

                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });

            //$("#dataGrid_iCount").datagrid('setWhere', "a.YearMonth = " + YearMonth + " and a.UserID=" + UserID);
            $("#Dialog_iCount").dialog("open");
        }
        //廣告統計
        function SetqueryGrid2() {
            var SDate = $("#SDate2_Query").datebox("getValue");
            var EDate = $("#EDate2_Query").datebox("getValue");
            var IndustryId = $("#IndustryId_Query").combobox('getValue');//產業別           
            var CityId = $("#CityId_Query").combobox('getValue');//縣市    
            var TownId = $("#TownId_Query").combobox('getValue');//區鎮鄉    

            var dataGrid = $('#dataGridMaster2');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sJOB0800.PublishingRecord', //連接的Server端，command
                data: "mode=method&method=" + "GePublishingCount" + "&parameters=" + SDate + "," + EDate + "," + IndustryId + "," + CityId + "," + TownId, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示
                    //$('#dataGridDetail').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                    if (rows.length > 0) {
                            dataGrid.datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                    } else {
                        dataGrid.datagrid('loadData', []); //清空資料
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }
        function pagerFilter(data) {
            if (typeof data.length == 'number' && typeof data.splice == 'function') {    // is array
                data = {
                    total: data.length,
                    rows: data
                }
            }
            var dg = $(this);
            var opts = dg.datagrid('options');
            var pager = dg.datagrid('getPager');
            pager.pagination({
                onSelectPage: function (pageNum, pageSize) {
                    opts.pageNumber = pageNum;
                    opts.pageSize = pageSize;
                    pager.pagination('refresh', {
                        pageNumber: pageNum,
                        pageSize: pageSize
                    });
                    dg.datagrid('loadData', data);
                }
            });
            if (!data.originalRows) {
                data.originalRows = (data.rows);
            }
            var start = (opts.pageNumber - 1) * parseInt(opts.pageSize);
            var end = start + parseInt(opts.pageSize);
            data.rows = (data.originalRows.slice(start, end));
            return data;
        }

        //點擊廣告統計人數
        function iCountLink2(value, row, index) {
            if (value != 0)
                return "<a href='javascript: void(0)' onclick='LinkiCount2(" + value + ","+index+");'>" + value + "</a>";
            else return value;
        }
        // open點擊廣告統計畫面 dialog
        function LinkiCount2(value, index) {
            var SDate = $("#SDate2_Query").datebox("getValue");
            var EDate = $("#EDate2_Query").datebox("getValue");
            var IndustryId = $("#IndustryId_Query").combobox('getValue');//產業別           
            var CityId = $("#CityId_Query").combobox('getValue');//縣市    
            var TownId = $("#TownId_Query").combobox('getValue');//區鎮鄉    

            $("#dataGridMaster2").datagrid('selectRow', index);
            var rows = $("#dataGridMaster2").datagrid('getSelected');
            var iYear = rows.iYear;
            var iMonth = rows.iMonth;
            var SalesID = rows.SalesID;

            footerRows = $("#dataGridMaster2").datagrid('getFooterRows');
            //表尾(總計)
            if (value == footerRows[0]["iCount"]) {
                iYear = "";
                iMonth = "";
                SalesID = "";
            }
            var dataGrid = $('#dataGrid_iCount2');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sJOB0800.PublishingRecord', //連接的Server端，command
                data: "mode=method&method=" + "GePublishingCountData" + "&parameters=" + SDate + "," + EDate + "," + IndustryId + "," + CityId + "," + TownId + "," + iYear + "," + iMonth + "," + SalesID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示
                    //$('#dataGridDetail').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                    if (rows.length > 0) {
                        dataGrid.datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                    } else {
                        dataGrid.datagrid('loadData', []); //清空資料
                    }
                        //dataGrid.datagrid('loadData', { "total": 0, "rows": [] });
                        //data = eval('(' + data + ')');
                        //var appandRows = [];

                        //for (var j = 0; j < data.length; j++) {
                        //    appandRows.push({ TeamName: data[j].TeamName, Code: data[j].Code, DisplayName: data[j].DisplayName, StartDate: data[j].StartDate, EndTime: data[j].EndTime, IndustryName: data[j].IndustryName, CityName: data[j].CityName, TownData: data[j].TownData });
                        //}
                        //for (var i = 0; i < appandRows.length; i++) {
                        //    dataGrid.datagrid("appendRow", appandRows[i]);
                        //}                       
                    //}
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });

            //$("#dataGrid_iCount").datagrid('setWhere', "a.YearMonth = " + YearMonth + " and a.UserID=" + UserID);
            $("#Dialog_iCount2").dialog("open");
        }

        //匯出Excel
        function AutoExcel() {
            //得出grid data
            var SDate = $("#SDate2_Query").datebox("getValue");
            var EDate = $("#EDate2_Query").datebox("getValue");
            var IndustryId = $("#IndustryId_Query").combobox('getValue');//產業別           
            var CityId = $("#CityId_Query").combobox('getValue');//縣市    
            var TownId = $("#TownId_Query").combobox('getValue');//區鎮鄉    

            $.ajax({
                url: '../handler/JBHRISUseCaseHandler.ashx?' + $.param({ mode: 'CallServerMethodReturnFile', remoteName: 'sJOB0800', method: 'PublishingAutoExcel' }),
                //data: { parameters: $.toJSONString(data) },

                data: "&parameters=" + SDate + "," + EDate + "," + IndustryId + "," + CityId + "," + TownId,

                type: 'POST',
                async: true,
                success: function (data) {
                    //Json.FileName
                    var Json = $.parseJSON(data);
                    if (Json.IsOK) {
                        var Url = $('<a>', {
                            href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: '廣告統計.xls', FilePathName: Json.FileStreamOrFileName }),
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



        //業務報表
        function lbSalesInfo() {
            var SDate = $("#SDate2_Query").datebox("getValue");
            var EDate = $("#EDate2_Query").datebox("getValue");
            var IndustryId = $("#IndustryId_Query").combobox('getValue');//產業別           
            var CityId = $("#CityId_Query").combobox('getValue');//縣市    
            var TownId = $("#TownId_Query").combobox('getValue');//區鎮鄉   

            var url = "../JB_ADMIN/REPORT/Media/0800PublishingReportView.aspx?SDate=" + SDate + "&EDate=" + EDate + "&IndustryId=" + IndustryId + "&CityId=" + CityId + "&TownId=" + TownId;

            var height = $(window).height() - 20;
            var height2 = $(window).height() + 150;
            var width = $(window).width() - 20;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                //top:0,
                width: width,
                title: "統計表",
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

            <div class="easyui-tabs" style="width:780px;height100%" id="tabs1">
            <div title="點擊統計" style="padding:10px">
                <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sJOB0800.PublishingRecord" runat="server" AutoApply="False"
                DataMember="PublishingRecord" Pagination="False" QueryTitle=""
                Title="" QueryMode="Panel" UpdateCommandVisible="False" ViewCommandVisible="False" DeleteCommandVisible="False" AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" Width="420px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="分類" Editor="text" FieldName="TypeText" Format="" MaxLength="0" Width="190" />
                    <JQTools:JQGridColumn Alignment="right" Caption="次數" Editor="numberbox" FieldName="iCount" Format="" Width="170" Total="" FormatScript="iCountLink" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Type" Editor="text" FieldName="Type" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
               
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Condition="=" DataType="datetime" Editor="datebox" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" Caption="起始日期" FieldName="SDate" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="結束日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="EDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶關鍵字" Condition="%" DataType="string" Editor="text" FieldName="TypeText" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶代碼" Condition="%" DataType="string" Editor="text" FieldName="iCount" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="Dialog_iCount" runat="server" BindingObjectID="" Title="點擊明細" ShowSubmitDiv="False" DialogTop="80px" Height="450px" Width="700px">
                        <JQTools:JQDataGrid ID="dataGrid_iCount" data-options="pagination:true,view:commandview" RemoteName="sJOB0800.PublishingRecord" runat="server" AutoApply="True"
                DataMember="PublishingRecord" Pagination="False" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="80px" QueryMode="Panel" QueryTop="80px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="600px">
                            <Columns>
                               <JQTools:JQGridColumn Alignment="left" Caption="點擊客戶" Editor="text" FieldName="DisplayName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="300">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="客戶代碼" Editor="text" FieldName="Email" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="點擊時間" Editor="text" FieldName="CreateTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150" Format="yyyy/mm/dd HH:MM:SS">
                                </JQTools:JQGridColumn>
                            </Columns>
            </JQTools:JQDataGrid>
            </JQTools:JQDialog>
            </div>
            <div title="廣告統計" style="padding:10px">
                 <JQTools:JQDataGrid ID="dataGridMaster2" data-options="pagination:true,view:commandview" RemoteName="sJOB0800.PublishingCount" runat="server" AutoApply="False"
                DataMember="PublishingCount" Pagination="False" QueryTitle=""
                Title="" QueryMode="Panel" UpdateCommandVisible="False" ViewCommandVisible="False" DeleteCommandVisible="False" AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" Width="620px" Height="400px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="年度" Editor="text" FieldName="iYear" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="center" Caption="月份" Editor="text" FieldName="iMonth" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="業務代號" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="right" Caption="廣告則數" Editor="numberbox" FieldName="iCount" Format="" Width="140" Total="sum" FormatScript="iCountLink2" />
                    <JQTools:JQGridColumn Alignment="right" Caption=" 職缺數" Editor="text" FieldName="JobCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140" Total="sum">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="TeamID" Editor="text" FieldName="TeamID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                 <TooItems>
<%--                    <JQTools:JQToolItem ID="JQToolItem2" Icon="icon-tip" ItemType="easyui-linkbutton" OnClick="lbSalesInfo" Text="業務報表"  />--%>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="AutoExcel" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Condition="=" DataType="datetime" Editor="datebox" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" Caption="起始日期" FieldName="SDate2" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="結束日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="EDate2" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="產業別" Condition="=" DataType="string" Editor="infocombobox" FieldName="IndustryId" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'Id',textField:'Name',remoteName:'sJOB0800.cmdIndustry',tableName:'cmdIndustry',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="縣市" Condition="=" DataType="string" Editor="infocombobox" FieldName="CityId" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'Id',textField:'Name',remoteName:'sJOB0800.infoCity',tableName:'infoCity',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="區鎮鄉" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'Id',textField:'Name',remoteName:'sJOB0800.infoTown',tableName:'infoTown',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="TownId" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="Dialog_iCount2" runat="server" BindingObjectID="" Title="廣告明細" ShowSubmitDiv="False" DialogTop="80px" Height="450px" Width="930px">
                        <JQTools:JQDataGrid ID="dataGrid_iCount2" data-options="pagination:true,view:commandview" RemoteName="sJOB0800.PublishingRecord" runat="server" AutoApply="True"
                DataMember="PublishingRecord" Pagination="False" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="80px" QueryMode="Panel" QueryTop="80px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="850px">
                            <Columns>
                                  <JQTools:JQGridColumn Alignment="center" Caption="業務代號" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Width="70" />
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶代碼" Editor="text" FieldName="Code" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="DisplayName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" Format="">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="刊登起始日" Editor="datebox" FieldName="StartDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="刊登結束日" Editor="datebox" FieldName="EndTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="產業別" Editor="text" FieldName="IndustryName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="縣市" Editor="text" FieldName="CityName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="區鎮鄉" Editor="text" FieldName="TownData" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="265">
                                </JQTools:JQGridColumn>
                            </Columns>
            </JQTools:JQDataGrid>
            </JQTools:JQDialog>
            </div>
        </div>
</form>
</body>
</html>
