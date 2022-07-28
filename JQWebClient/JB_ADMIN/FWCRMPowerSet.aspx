<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMPowerSet.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .datagrid-header-row td[field='fiCount'] .datagrid-cell span
        {
            color: red;
        }
        .datagrid-header-row td[field='fAmount'] .datagrid-cell span
        {
            color: red;
        }
  </style>


    <script>  

        $(document).ready(function () {                       
            //panel寬度調整
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 850 });
            
            //費用年月檢查
            $('#dataFormTitleYearMonth').change('change', function () {
                CheckYearMonth();
            });


        });

        function CheckYearMonth() {

            //檢查費用年月是否已結帳
            var YearMonth = $('#dataFormTitleYearMonth').val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPowerData.PowerMaster', //連接的Server端，command
                data: "mode=method&method=" + "CheckYearMonth" + "&parameters=" + YearMonth,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        IsClose = $.parseJSON(data);
                    }
                }
            });
            if ((IsClose == "True")) {
                alert(YearMonth + ":此費用年月已結帳！");
                var dataGrid = $('#dataGridMaster');
                dataGrid.datagrid('loadData', []);//清空Grid資料
                return false;
            }
        }

        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridView') {
                var result = [];
                var YearMonth = $('#YearMonth_Query').val();//費用年月
                if (YearMonth != '') result.push("p.YearMonth = '" + YearMonth + "'");

                var CreateDate = $('#CreateDate_Query').datebox('getValue');//建立日期
                if (CreateDate != '') result.push("convert(nvarchar(10),p.CreateDate,111) = '" + CreateDate + "'");

                var PowerDate = $('#PowerDate_Query').datebox('getValue');//操錶日期
                if (PowerDate != '') result.push("convert(nvarchar(10),p.PowerDate,111) = '" + PowerDate + "'");

                var CompanyID = $('#CompanyID_Query').combobox('getValue');//公司別
                if (CompanyID != '') result.push("p.CompanyID = " + CompanyID );

                var DormID = $('#DormID_Query').combobox('getValue');//居住宿舍
                if (DormID != '') result.push("p.DormID = '" + DormID + "'");           

                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }

        //---------------------------------------呼叫Method---------------------------------------
        var GetDataFromMethod = function (methodName, data) {
            var returnValue = null;
            $.ajax({
                url: '../handler/JqDataHandle.ashx?RemoteName=sPowerData',
                data: { mode: 'method', method: methodName, parameters: $.toJSONString(data) },
                type: 'POST',
                async: false,
                success: function (data) { returnValue = $.parseJSON(data); },
                error: function (xhr, ajaxOptions, thrownError) { returnValue = null; }
            });
            return returnValue;
        };

        //---------------------------------------dataGridView Query---------------------------------
        //---------------------------------------選公司別觸發---------------------------------
        var QCompanyID_OnSelect = function (rowdata) {
            QGetQDorm("");//居住宿舍
            $('#dataGridView').datagrid('loadData', []); //清空資料 
        }
        //得到居住宿舍
        var QGetQDorm = function (CompanyID) {
            //公司別
            var CompanyID = $("#CompanyID_Query").combobox('getValue');
            var CodeList = GetDataFromMethod('GetDorm', { Company_ID: CompanyID });
            if (CodeList != null) {
                $("#DormID_Query").combobox('loadData', CodeList);//宿舍
            }
        }
        //---------------------------------------DataForm---------------------------------
        //---------------------------------------選公司別觸發---------------------------------
        var CompanyID_OnSelect = function (rowdata) {           
            GetDorm("");//居住宿舍
            $('#dataGridMaster').datagrid('loadData', []); //清空資料 
        }
        //得到居住宿舍
        var GetDorm = function (CompanyID) {
            //公司別
            var CompanyID = $("#dataFormTitleCompanyID").combobox('getValue');
            var CodeList = GetDataFromMethod('GetDorm', { Company_ID: CompanyID });
            if (CodeList != null) {
                $("#dataFormTitleDormID").combobox('loadData', CodeList);//宿舍
            }
        }
        //---------------------------------------選宿舍觸發---------------------------------
        var DormID_OnSelect = function (rowdata) {
            OnSelectDorm();//帶出房間名單
        }
        //選擇居住宿舍
        function OnSelectDorm() {
            var CompID = $("#dataFormTitleCompanyID").combobox('getValue');//公司別 1傑報人力 ,2傑信管理
            var DormID = $("#dataFormTitleDormID").combobox('getValue');//宿舍ID	
            var YearMonth = $("#dataFormTitleYearMonth").val();//費用年月            
            var PowerDate = $("#dataFormTitlePowerDate").datebox('getValue');//操表日期            
            var dataGrid = $('#dataGridMaster');
            //顯示選擇宿舍之房間列表
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPowerData.PowerMaster',  //連接的Server端，command
                data: "mode=method&method=" + "getDormIDData" + "&parameters=" + CompID + "," + DormID,
                cache: false,
                async: true,
                success: function (data) {
                    var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示                            
                    //$('#dataGridDetail').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                    if (rows != null && rows.length > 0) {
                        dataGrid.datagrid('loadData', { "total": 0, "rows": [] });
                        data = eval('(' + data + ')');
                        var appandRows = [];
                        var UserID = getClientInfo("UserID");
                        var UserName = getClientInfo("UserName");
                        var today = getClientInfo('_today')
                        for (var j = 0; j < data.length; j++) {
                            appandRows.push({ PowerID: j, CompanyID: CompID, YearMonth: YearMonth, PowerDate: PowerDate, DormID: DormID, RoomID: data[j].RoomID, iEmpCount: data[j].iEmpCount, PowerQty: data[j].PowerQty, LastPowerMeter: data[j].LastPowerMeter, UserID: UserID, CreateBy: UserName, CreateDate: today, LastUpdateBy: UserName, LastUpdateDate: today });
                        }
                        for (var i = 0; i < appandRows.length; i++) {
                            dataGrid.datagrid("appendRow", appandRows[i]);
                        }
                        //griddetail的footer強制更新
                        setFooter(dataGrid);
                    } else dataGrid.datagrid('loadData', []);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }

        function OnLoadDT() {
            var dataGrid = $('#dataGridMaster');
            dataGrid.datagrid('loadData', []);//清空Grid資料
            //預設發票年月
            var sDate = new Date();
            var date1 = $.jbjob.Date.DateFormat(sDate, 'yyyyMMdd').substring(0, 6);
            $("#dataFormTitleYearMonth").val(date1);//銷貨年月
        }

        function LinkEmp(value, row, index) {
            if (value != 0)
                return "<a href='javascript: void(0)' onclick='OpenLinkEmp(" + index + ",1);'>" + value + "</a>";
            else return value;
        }
        // open 外勞 dialog
        function OpenLinkEmp(index, iType) {
            $("#dataGridMaster").datagrid('selectRow', index);
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var RoomID = rows.RoomID;
            var CompID = $("#dataFormTitleCompanyID").combobox('getValue');//公司別 1傑報人力 ,2傑信管理
            var DormID = $("#dataFormTitleDormID").combobox('getValue');//宿舍Name(因為會跨傑報&傑信)

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPowerData.PowerMaster',  //連接的Server端，command
                data: "mode=method&method=" + "getRoomIDData" + "&parameters=" + CompID + "," + DormID + "," + RoomID,
                cache: false,
                async: true,
                success: function (data) {
                    var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示

                    $('#dataGrid_RoomData').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                }
            });
            openForm('#Dialog_RoomData', {}, 'viewed', 'dialog');

        }
        //本次碼錶數檢查
        function CheckNowPowerMeter(val) {
            var row = $("#dataGridMaster").datagrid('getSelected');
            var index = $("#dataGridMaster").datagrid('getRowIndex', row);
            $("#dataGridMaster").datagrid('selectRow', index);
            a = parseInt(val);
            b = parseInt(row.LastPowerMeter);
            if (a > b) {
                return true;
            } else return false;
        }

        function AddItem() {
            openForm('#JQDialog1', {}, 'inserted', 'dialog');            
        }       
        //檢查字串是否符合發票年月
        function CheckStrWildWord(str) {
            var r = str.match(/^(\d{4})(0[1-9]|1[0-2])$/);
            if (r == null) return false;
            var d = new Date(r[1], (r[2] - 1), 1);
            return (d.getFullYear() == r[1] && d.getMonth() == (r[2] - 1) && d.getDate() == 1);
        }

      
        function OnInsertedGM() {
            //建立日期,居住宿舍 帶入查詢條件
            var dt = new Date();
            var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')
            $('#CreateDate_Query').datebox('setValue', today);//建立日期
            var DormID = $("#dataFormTitleDormID").combobox('getValue');//宿舍
            $('#DormID_Query').combobox('setValue', DormID);
            closeForm('#JQDialog1');
            alert('電費立帳完成～');
            queryGrid($('#dataGridView'));

        }
        //顯示計算完成的電費資訊
        //選擇居住宿舍
        function getPowereData() {
            var CompID = $("#dataFormTitleDormID").combobox('getValue');//公司別 1傑報人力 ,2傑信管理
            var DormName = $("#dataFormTitleDormID").combobox('getValue');//宿舍ID(會跨傑報&傑信)
            var YearMonth = $("#dataFormTitleYearMonth").val();//費用年月            
            var PowerDate = $("#dataFormTitlePowerDate").datebox('getValue');//操表日期            
            var dataGrid = $('#dataGridMaster');
            //顯示選擇宿舍之房間列表
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPowerData.PowerMaster',  //連接的Server端，command
                data: "mode=method&method=" + "getPowereData" + "&parameters=" + DormName,
                cache: false,
                async: true,
                success: function (data) {
                    var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示                            
                    //$('#dataGridDetail').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                    if (rows != null && rows.length > 0) {
                        dataGrid.datagrid('loadData', { "total": 0, "rows": [] });
                        data = eval('(' + data + ')');
                        var appandRows = [];
                        var UserID = getClientInfo("UserID");
                        var UserName = getClientInfo("UserName");
                        var today = getClientInfo('_today')
                        for (var j = 0; j < data.length; j++) {
                            appandRows.push({ PowerID: j, CompanyID: CompID, YearMonth: YearMonth, PowerDate: PowerDate, DormID: DormID, RoomID: data[j].RoomID, iEmpCount: data[j].iEmpCount, PowerQty: data[j].PowerQty, LastPowerMeter: data[j].LastPowerMeter, UserID: UserID, CreateBy: UserName, CreateDate: today, LastUpdateBy: UserName, LastUpdateDate: today });
                        }
                        for (var i = 0; i < appandRows.length; i++) {
                            dataGrid.datagrid("appendRow", appandRows[i]);
                        }
                        //griddetail的footer強制更新
                        setFooter(dataGrid);
                    } else dataGrid.datagrid('loadData', []);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }
        //如果立帳按鈕為顯示狀態=>刪除無效
        function OnDeleteGM() {
            if($("#JQButton1").is(":hidden")!=true)
            return false;
        }
      
        //立帳明細
        function fAmountLink(value, row, index) {
            if (value != 0)
                return "<a href='javascript: void(0)' onclick='LinkEmployeeFees(" + index + ",1);'>" + value + "</a>";
            else return value;

            //if (val < 0) {
            //    return "<div style='width: 30px; border: solid 1px red;'> " + val + "</div>";
            //} else {
            //    return "<div style='width: 30px; border: solid 1px blue;'> " + val + "</div>";
            //}

        }

        // open外勞費用立帳畫面 dialog
        function LinkEmployeeFees(index, iType) {
            $("#dataGridView").datagrid('selectRow', index);
            var rows = $("#dataGridView").datagrid('getSelected');
            var PowerID = rows.PowerID;         
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPowerData.PowerMaster',  //連接的Server端，command
                data: "mode=method&method=" + "EmployeeFeesData" + "&parameters=" + PowerID,  //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入搜尋字串
                cache: false,
                async: true,
                success: function (data) {
                    var row = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示

                    $('#dataGrid_EmployeeFees').datagrid('loadData', row);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                }
            });
            openForm('#Dialog_EmployeeFees', {}, 'viewed', 'dialog');

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <br />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPowerData.PowerQuery" runat="server" AutoApply="True"
                DataMember="PowerQuery" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="操錶資料" QueryMode="Panel" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="850px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="費用年月" Editor="text" FieldName="YearMonth" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="PowerID" Editor="numberbox" FieldName="PowerID" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="宿舍" Editor="text" FieldName="DormName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="操錶日期" Editor="datebox" FieldName="PowerDate" Format="" Visible="true" Width="65" />
                    <JQTools:JQGridColumn Alignment="right" Caption="單價" Editor="numberbox" FieldName="PowerQty" Format="" Visible="true" Width="50" />
                    <JQTools:JQGridColumn Alignment="right" Caption="上次碼錶數" Editor="numberbox" FieldName="LastPowerMeter" Format="" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="right" Caption="本次碼錶數" Editor="numberbox" FieldName="NowPowerMeter" Format="" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="right" Caption="合計" Editor="numberbox" FieldName="PowerAmount" Format="" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="right" Caption="差額" Editor="numberbox" FieldName="DiffAmount" Format="" Visible="true" Width="40" />
                    <JQTools:JQGridColumn Alignment="center" Caption="立帳人數" Editor="text" FieldName="fiCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" FormatScript="fAmountLink">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="立帳費用" Editor="text" FieldName="fAmount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="76">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" MaxLength="0" Visible="true" Width="65" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="AddItem" Text="新增" />                   
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="費用年月" Condition="=" DataType="string" Editor="text" FieldName="YearMonth" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="60" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="建立日期" Condition="%" DataType="string" Editor="datebox" FieldName="CreateDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="操錶日期" Condition="%" DataType="string" Editor="datebox" FieldName="PowerDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompName',remoteName:'sPowerData.infoCompID',tableName:'infoCompID',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:QCompanyID_OnSelect,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="88" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="居住宿舍" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:150" FieldName="DormID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="165" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="Dialog_EmployeeFees" runat="server" BindingObjectID="" Title="外勞立帳明細" ShowSubmitDiv="False" DialogLeft="100px" DialogTop="100px" Width="710px">
                        <JQTools:JQDataGrid ID="dataGrid_EmployeeFees" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" data-options="pagination:true,view:commandview" DataMember="infoEmployeeFeesPower" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="80px" QueryMode="Panel" QueryTitle="查詢條件" QueryTop="80px" RecordLock="False" RecordLockMode="None" RemoteName="sPowerData.infoEmployeeFeesPower" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="center" Caption="費用年月" Editor="text" FieldName="YearMonth" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="外勞姓名" Editor="text" FieldName="EmployeeTcName" Visible="True" Width="70" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="費用名稱" Editor="text" FieldName="FeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="right" Caption="費用金額" Editor="numberbox" FieldName="FeeAmount" Visible="True" Width="80" Total="sum">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="費用區間" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                                </JQTools:JQGridColumn>
                            </Columns>
                        </JQTools:JQDataGrid>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormTitle" Title="新增操錶資料" Width="700px" ShowSubmitDiv="False" DialogLeft="50px" DialogTop="20px">
                <JQTools:JQDataForm ID="dataFormTitle" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="infoPowerTitle" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sPowerData.infoPowerTitle" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoadDT" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="PowerID" Editor="numberbox" FieldName="PowerID" Format="" Visible="False" Width="180" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="費用年月" Editor="text" FieldName="YearMonth" Width="60" ReadOnly="False" Visible="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="操錶日期" Editor="datebox" FieldName="PowerDate" Format="" Width="93" ReadOnly="False" Visible="True" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompName',remoteName:'sPowerData.infoCompID',tableName:'infoCompID',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:CompanyID_OnSelect,panelHeight:200" FieldName="CompanyID" ReadOnly="False" Visible="True" Width="88" />
                        <JQTools:JQFormColumn Alignment="left" Caption="居住宿舍" Editor="infocombobox" FieldName="DormID" Format="" Width="170" ReadOnly="False" Visible="True" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:DormID_OnSelect,panelHeight:150" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDefault ID="defaultTitle" runat="server" BindingObjectID="dataFormTitle" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="PowerDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateTitle" runat="server" BindingObjectID="dataFormTitle" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="YearMonth" RemoteMethod="False" ValidateMessage="費用年月格式有誤！" ValidateType="None" CheckMethod="CheckStrWildWord" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDataGrid ID="dataGridMaster" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="PowerMaster" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" Height="400px" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" OnDelete="OnDeleteGM" OnInserted="OnInsertedGM" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sPowerData.PowerMaster" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="610px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="PowerID" Editor="numberbox" FieldName="PowerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="操錶日期" Editor="datebox" FieldName="PowerDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="DormID" Editor="text" FieldName="DormID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="PowerQty" Editor="text" FieldName="PowerQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="房號" Editor="text" FieldName="RoomID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="外勞人數" Editor="text" FieldName="iEmpCount" FormatScript="LinkEmp" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="75">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="上次碼錶數" Editor="numberbox" FieldName="LastPowerMeter" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="本次碼錶數" Editor="numberbox" FieldName="NowPowerMeter" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="YearMonth" Editor="text" FieldName="YearMonth" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" Visible="True" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataGridMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckMethod="CheckNowPowerMeter" CheckNull="True" FieldName="NowPowerMeter" RemoteMethod="False" ValidateMessage="本次碼錶數數值有誤！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="Dialog_RoomData" runat="server" BindingObjectID="" Title="房間外勞資料" ShowSubmitDiv="False" DialogLeft="180px" DialogTop="160px" Width="380px">
                        <JQTools:JQDataGrid ID="dataGrid_RoomData" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" data-options="pagination:true,view:commandview" DataMember="infoEmployeeFees" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="80px" QueryMode="Panel" QueryTitle="查詢條件" QueryTop="80px" RecordLock="False" RecordLockMode="None" RemoteName="sPowerData.infoEmployeeFees" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="外勞姓名" Editor="text" FieldName="EmployeeTcName" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                            </Columns>
                        </JQTools:JQDataGrid>
            </JQTools:JQDialog>

        </div>
    </form>
</body>
</html>
