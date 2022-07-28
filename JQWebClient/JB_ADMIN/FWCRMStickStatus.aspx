<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMStickStatus.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>  
        var flag = true; //定義一個全域變數，只有第一次執行

        $(document).ready(function () {            
            //建立日期	串聯
            var Date1 = $('#flowflagText_Query').closest('td');
            var Date2 = $('#QtyResult_Query').closest('td').children();

            Date1.append(' - ').append(Date2);

            //是否駐廠 + 天數
            var IsOnSite = $('#dataFormOrdersIsOnSite').closest('td');
            var SiteDays = $('#dataFormOrdersOnSiteDays').closest('td').children();
            var IsHotel = $('#dataFormOrdersIsHotel').closest('td').children();
            IsOnSite.append("&nbsp;&nbsp;駐廠天數&nbsp;&nbsp;").append(SiteDays).append("&nbsp;天").append('&nbsp;&nbsp;&nbsp;&nbsp;防疫旅館需求').append(IsHotel);

            //付費模式 + 雇主支付費用分攤連結
            var HotelType = $('#dataFormOrdersHotelType').closest('td');
            var HotelEmployer = $('#dataFormOrdersHotelEmployer').closest('td').children();
            var HotelAgent = $('#dataFormOrdersHotelAgent').closest('td').children();
            var HotelEmployee = $('#dataFormOrdersHotelEmployee').closest('td').children();
            HotelType.append("&nbsp;").append(HotelEmployer).append("&nbsp;元、仲介支付").append(HotelAgent).append("&nbsp;元、移工支付").append(HotelEmployee).append("&nbsp;元");

            //接工方式連接
            var TakeTypeOne = $('#dataFormOrdersTakeTypeOne').closest('td');
            var TypeOneNo = $('#dataFormOrdersTypeOneNo').closest('td').children();
            var TakeTypeTree = $('#dataFormOrdersTakeTypeTree').closest('td').children();
            TakeTypeOne.append("第一順位，核函號碼：").append(TypeOneNo).append("&nbsp;&nbsp;&nbsp;").append(TakeTypeTree).append("第三順位");


        });

        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridMaster') {

                //得出grid data
                var OrderYear = $('#OrderYear_Query').val();
                var SalesID = $("#SalesID_Query").combobox('getValue');
                var EmployerName = $('#EmployerName_Query').val();
                var OrderNo = $('#OrderNo_Query').val();
                var org_okno = $('#sup_cname_Query').val();
                var NationalityID = $("#NationalityID_Query").combobox('getValue');//國籍
                var OrderDate1 = $('#flowflagText_Query').datebox('getValue');//建立日期1                       
                var OrderDate2 = $('#QtyResult_Query').datebox('getValue');//建立日期2                           
                var NationalityID = $("#NationalityID_Query").combobox('getValue');//國籍
                var D_STEP_ID = $("#OrderStatus_Query").combobox('getValue');//流程狀態	

                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sFWCRMStickStatus.FWCRMStickStatus',  //連接的Server端，command
                    data: "mode=method&method=" + "getOrderData" + "&parameters=" + SalesID + "," + EmployerName + "," + OrderNo + "," + org_okno + "," + OrderDate1 + "," + OrderDate2 + "," + NationalityID + "," + D_STEP_ID + "," + OrderYear,
                    cache: false,
                    async: true,
                    success: function (data) {
                        var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示                            
                       
                        if (rows.length > 10) {
                            //通過loadData方法清除掉原有Grid中的舊有資料並填補分頁資料
                            $('#dataGridMaster').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                        } else {
                            $('#dataGridMaster').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                        }
                    }
                });







            }
        }

        var IsUpdate = 0;
        var OnLoadSuccess = function (data) {
            if (flag) {               

                //登入的工號得出查詢權限
                var UserID = getClientInfo("UserID");
                
                var Status = "";//0 無查詢權限,1 業務=>查詢自己 ,2 SelectAdmin =>全部查詢
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sFWCRMStickStatus.FWCRMStickStatus', //連接的Server端，command
                    data: "mode=method&method=" + "getSelectRange" + "&parameters=" + UserID,
                    cache: false,
                    async: false,
                    success: function (data) {
                        var rows = $.parseJSON(data);
                        if (rows.length > 0) {
                            Status =rows[0].Status;
                            IsUpdate =rows[0].IsUpdate;
                        }
                    }
                });
                var obj = $($('#querydataGridMaster').find('input')[0]);
                if (Status != 2) {
                    obj.combobox('setValue', UserID);
                    obj.combobox("disable");
                }
                //求得系統設定 => 設定訂單進度全查詢人員
                //var WhereString = "";
                //WhereString = WhereString + "A.EMPLOYEE_CODE = '" + userid + "';";  //11 = 11
                //WhereString = WhereString + "M.EMPLOYEE_CODE <> '" + userid + "';";   //12 = 12
                //$("#dataGridMaster").datagrid('setWhere', WhereString);

                flag = false;
            }
        }
        // 訂單進度查詢 dialog
        function initStickStatus() {
            $("#JQDialog_StickStatus").dialog(
            {
                height: 400,
                width: 850,
                left: 100,
                top: 80,
                resizable: false,
                modal: true,
                title: "訂單進度查詢",
                closed: true
            });
        };
        //訂單進度查詢
        function StickStatusLink(value, row, index) {
            //return $('<a>', { href: 'javascript:void(0)', name: 'StickStatusLink', onclick: 'LinkStickStatus.call(this)', rowIndex: index }).linkbutton({ plain: false, text: value })[0].outerHTML
            if (value != 0)
                return "<a href='javascript: void(0)' onclick='LinkStickStatus(" + index + ");'>" + value + "</a>";
            else return "";
        }
       
        // open訂單進度查詢畫面 dialog
        function LinkStickStatus(index) {
            //var index = $(this).attr('rowIndex');
            $("#dataGridMaster").datagrid('selectRow', index);
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var OrderNo = rows.OrderNo;
            $("#dataGridStickStatus").datagrid('setWhere', "o.OrderNo = '" + OrderNo + "'");
            openForm('#JQDialog_StickStatus', {}, 'viewed', 'dialog');
        }

        //Grid下載檔案 => 聘工表檔案 
        //欄值,row,index
        function downloadScript(val, rowData, index) {
            return '<a href="../handler/JqFileHandler.ashx?File=/FWCRM/Orders/' + val + '">' + val + '</a>';
        }
        //剩餘人數 QtyResult =>變紅色
        function QtyResultScript(val, rowData, index) {
            return '<span style="color:red"> ' + val + '</span>';
        }

        function PersonQtyFinalLink(value, row, index) {
            if (row.Gender != undefined )//表示最後一筆加總的row 
                return "<a href='javascript: void(0)' onclick='LinkIndateCheck(" + index + ");'> <div style='color:Red;font-weight:bolder;font-size: 16px'>" + value + "</div></a>";
            else return value;
        }

        // open入境確認單畫面 dialog
        function LinkIndateCheck(index, iType) {
            $("#dataGridDetail").datagrid('selectRow', index); //按連結時返回Grid焦點  
            openForm('#Dialog_IndateCheck', $('#dataGridDetail').datagrid('getSelected'), "viewed", 'dialog');
        }

        //過濾入境確認維護Grid setWhere
        function OnLoadSuccessDFIndateCheck() {
            var OrderNo = $("#dataFormDetailOrderNo").val();//訂單編號
            var Item = $("#dataFormDetailItem").val();//批次
            $("#dataGrid_IndateCheck").datagrid('setWhere', "OrderNo = '" + OrderNo + "' and Item=" + Item);

        }
        //Grid下載檔案 
        //欄值,row,index
        function downloadScript(val, rowData, index) {
            if (rowData.IndateImg != undefined) {//表示不是最後一筆加總的row
                return '<a href="../handler/JqFileHandler.ashx?File=/FWCRM/Orders/' + val + '">' + val + '</a>';
            }
        }

        function OrderNoLink(value, row, index) {
            if (value != 0)
                return "<a href='javascript: void(0)' onclick='LinkOrderNo(" + index + ",1);'>" + value + "</a>";
            else return value;
        }
        // open order dialog
        function LinkOrderNo(index, iType) {
            $("#dataGridMaster").datagrid('selectRow', index);
            // 是否有權限可以修改訂單 => FWCRMOrdersSelectAdmin => IsUpdate=1
            if (IsUpdate == true) {
                openForm('#JQDialog1', $('#dataGridMaster').datagrid('getSelected'), "updated", 'dialog');
            } else openForm('#JQDialog1', $('#dataGridMaster').datagrid('getSelected'), "viewed", 'dialog');
        }
        function OnAppliedDFOrders() {
            queryGrid($('#dataGridMaster'));
            //$('#dataGridMaster').datagrid('setWhere', "1=1");
        }

        function AutoExcel() {
            //得出grid data
            var OrderYear = $('#OrderYear_Query').val();
            var SalesID = $("#SalesID_Query").combobox('getValue');
            var EmployerName = $('#EmployerName_Query').val();
            var OrderNo = $('#OrderNo_Query').val();
            var org_okno = $('#sup_cname_Query').val();
            var NationalityID = $("#NationalityID_Query").combobox('getValue');//國籍
            var OrderDate1 = $('#flowflagText_Query').datebox('getValue');//建立日期1                       
            var OrderDate2 = $('#QtyResult_Query').datebox('getValue');//建立日期2                           
            var NationalityID = $("#NationalityID_Query").combobox('getValue');//國籍
            var D_STEP_ID = $("#OrderStatus_Query").combobox('getValue');//流程狀態	

            //var data = {
            //    SalesID: sSalesID,
            //    EmployerName: sEmployerName
            //};

            $.ajax({
                url: '../handler/JBHRISUseCaseHandler.ashx?' + $.param({ mode: 'CallServerMethodReturnFile', remoteName: 'sFWCRMStickStatus', method: 'OrderDataAutoExcel' }),
                //data: { parameters: $.toJSONString(data) },

                data: "&parameters=" + SalesID + "," + EmployerName + "," + OrderNo + "," + org_okno + "," + OrderDate1 + "," + OrderDate2 + "," + NationalityID + "," + D_STEP_ID + "," + OrderYear,

                type: 'POST',
                async: true,
                success: function (data) {
                    //Json.FileName
                    var Json = $.parseJSON(data);
                    if (Json.IsOK) {
                        var Url = $('<a>', {
                            href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: '訂單查詢.xls', FilePathName: Json.FileStreamOrFileName }),
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

    </script>  
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sFWCRMStickStatus.FWCRMOrderList" runat="server" AutoApply="True"
                DataMember="FWCRMOrderList" Pagination="True" QueryTitle=""
                Title="訂單進度查詢" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="OnLoadSuccess" EditDialogID="JQDialog1">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="訂單年度" Editor="text" FieldName="OrderYear" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" MaxLength="0" Width="90" Visible="True" Sortable="True" FormatScript="OrderNoLink" />
                    <JQTools:JQGridColumn Alignment="left" Caption="流程狀態" Editor="text" FieldName="D_STEP_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="110">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="挑工紀錄" Editor="text" FieldName="iStickStatus" FormatScript="StickStatusLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="挑工進度" Editor="text" FieldName="StatusName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Format="" MaxLength="0" Width="180" Visible="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="訂單人數" Editor="text" FieldName="PersonQtyOriginal" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="已結案人數" Editor="text" FieldName="PersonQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="未結案人數" Editor="text" FieldName="QtyResult" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70" FormatScript="QtyResultScript">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="引進國別" Editor="text" FieldName="NationalityName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="75">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="負責業務" Editor="text" FieldName="NAME_C" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="75">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="製單日期" Editor="datebox" FieldName="OrderDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="聘工表號碼" Editor="text" FieldName="WorkNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                </Columns> 
                 <TooItems>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="AutoExcel" Text="匯出Excel" Visible="True" />
                </TooItems>              
                <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="訂單年度" Condition="%" DataType="string" Editor="numberbox" FieldName="OrderYear" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="55" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="負責業務" Condition="=" DataType="string" Editor="infocombobox" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" EditorOptions="valueField:'EmpID',textField:'NAME_C',remoteName:'sFWCRMStickStatus.infoSalesID',tableName:'infoSalesID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="雇主名稱" Condition="%" DataType="string" Editor="text" FieldName="EmployerName" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="國籍" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AutoKey',textField:'NationalityName',remoteName:'sFWCRMOrders.infoFWCRMNationality',tableName:'infoFWCRMNationality',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:120" FieldName="NationalityID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="流程狀態" Condition="=" DataType="string" Editor="infocombobox" FieldName="OrderStatus" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'D_STEP_ID',textField:'D_STEP_ID',remoteName:'sFWCRMStickStatus.infoD_STEP_ID',tableName:'infoD_STEP_ID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="訂單編號" Condition="=" DataType="string" Editor="text" EditorOptions="" FieldName="OrderNo" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="函號" Condition="%" DataType="string" Editor="text" FieldName="sup_cname" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="製單日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="flowflagText" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="2" Width="90" />
                        <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="=" DataType="datetime" Editor="datebox" FieldName="QtyResult" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="1" Span="0" Width="90" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormOrders" Title="外籍勞工訂單" Width="830px" DialogTop="50px" ShowSubmitDiv="True">
                <JQTools:JQDataForm ID="dataFormOrders" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="FWCRMOrders" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" RemoteName="sFWCRMStickStatus.FWCRMOrders" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApplied="OnAppliedDFOrders">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" MaxLength="0" NewRow="False" ReadOnly="True" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單類型" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:340,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:5,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'入境',value:'1'},{text:'承接',value:'2'},{text:'轉單',value:'3'},{text:'轉單續聘',value:'4'},{text:'代招',value:'5'}]" FieldName="OrderType" NewRow="True" ReadOnly="False" Visible="True" Width="220" />
                        <JQTools:JQFormColumn Alignment="left" Caption="負責業務" Editor="text" FieldName="NAME_C" MaxLength="0" NewRow="False" ReadOnly="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單狀態" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:270,remoteName:'sFWCRMOrders.infoOrderStatus',tableName:'infoOrderStatus',valueField:'ID',textField:'Name',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="OrderStatus" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="150" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主名稱" Editor="text" EditorOptions="" FieldName="EmployerName" Format="" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聘工表號碼" Editor="text" EditorOptions="" FieldName="WorkNo" Format="" MaxLength="0" NewRow="False" ReadOnly="True" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="引進國別" Editor="infocombobox" EditorOptions="valueField:'AutoKey',textField:'NationalityName',remoteName:'sFWCRMOrders.infoFWCRMNationality',tableName:'infoFWCRMNationality',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="NationalityID" Format="" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聘工表檔案" Editor="text" EditorOptions="" FieldName="WorkImg" Format="download,folder:Files/FWCRM/Orders" MaxLength="100" NewRow="False" ReadOnly="False" Width="190" />
                        <JQTools:JQFormColumn Alignment="left" Caption="國外仲介" Editor="text" FieldName="sup_cname" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="是否駐廠" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsOnSite" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" FieldName="OnSiteDays" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="2" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsHotel" MaxLength="0" ReadOnly="True" Visible="True" Width="30" NewRow="False" Span="1" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付費模式" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'雇主全額支付',selected:'false'},{value:'2',text:'移工全額支付',selected:'false'},{value:'3',text:'雇主支付',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:130" FieldName="HotelType" NewRow="False" Span="4" Visible="True" Width="180" MaxLength="0" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" FieldName="HotelEmployer" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" FieldName="HotelAgent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" FieldName="HotelEmployee" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="50" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="接工方式" Editor="checkbox" FieldName="TakeTypeOne" NewRow="True" ReadOnly="False" Span="4" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="TypeOneNo" MaxLength="0" NewRow="True" ReadOnly="False" Span="1" Width="135" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" FieldName="TakeTypeTree" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案日期" Editor="datebox" FieldName="CloseDate" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案原因" Editor="infooptions" FieldName="CloseType" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" EditorOptions="title:'JQOptions',panelWidth:190,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'訂單作廢',value:'1'},{text:'不再引進',value:'2'}]" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="FWCRMOrdersDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormOrders" RemoteName="sFWCRMOrders.FWCRMOrders" Title="" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="批次" Editor="text" FieldName="Item" Format="" Width="30" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="預計入境年月" Editor="text" FieldName="PlanIndate" Format="" Width="75" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="訂單人數" Editor="text" FieldName="PersonQtyOriginal" Format="" Width="56" Total="sum" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="目前人數" Editor="text" FieldName="PersonQtyFinal" Width="56" Total="sum" FormatScript="PersonQtyFinalLink" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="性別" Editor="infocombobox" FieldName="Gender" Format="" Width="38" EditorOptions="items:[{value:'1',text:'女',selected:'false'},{value:'2',text:'男',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="函號" Editor="text" FieldName="org_okno" Format="" Width="88" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="工期" Editor="text" FieldName="WorkTimeText" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Notes" Format="" Width="250" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" Width="80" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="WorkTime" Editor="text" FieldName="WorkTime" ReadOnly="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="WorkTimeReason" Editor="text" FieldName="WorkTimeReason" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="sgn_no" Editor="text" FieldName="sgn_no" ReadOnly="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="sgn_type" Editor="text" FieldName="sgn_type" ReadOnly="False" Visible="False" Width="80" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="OrderNo" ParentFieldName="OrderNo" />
                    </RelationColumns>
                     
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail0" EditMode="Dialog" Title="訂單明細" Width="650px">
                    <JQTools:JQDataForm ID="dataFormDetail0" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="FWCRMOrdersDetails" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataGridDetail" RemoteName="sFWCRMOrders.FWCRMOrders" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="批次" Editor="text" FieldName="Item" Format="" NewRow="True" ReadOnly="True" Width="40" />
                            <JQTools:JQFormColumn Alignment="left" Caption="預計入境年月" Editor="text" FieldName="PlanIndate" Format="" NewRow="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="訂單人數" Editor="numberbox" FieldName="PersonQtyOriginal" Format="" NewRow="True" OnBlur="OnApplydataFormDetail" ReadOnly="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="目前人數" Editor="numberbox" FieldName="PersonQtyFinal" NewRow="False" ReadOnly="True" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:120,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'女',value:'1'},{text:'男',value:'2'}]" FieldName="Gender" Format="" NewRow="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="函號" Editor="inforefval" EditorOptions="title:'選擇函號',panelWidth:300,remoteName:'sFWCRMOrders.infoOrg_okno',tableName:'infoOrg_okno',columns:[{field:'cus_name',title:'雇主名稱',width:150,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'org_okno',title:'函號',width:110,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'sgn_no',value:'sgn_no'},{field:'sgn_type',value:'sgn_type'}],whereItems:[],valueField:'org_okno',textField:'cus_name',valueFieldCaption:'函號',textFieldCaption:'函號',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="org_okno" Format="" NewRow="True" Span="1" Width="160" />
                            <JQTools:JQFormColumn Alignment="left" Caption="工期" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:500,remoteName:'sFWCRMOrders.infoWorkTime',tableName:'infoWorkTime',valueField:'ID',textField:'Name',columnCount:6,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="WorkTime" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="WorkTimeReason" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" EditorOptions="height:50" FieldName="Notes" Format="" NewRow="True" Span="2" Visible="True" Width="460" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" NewRow="True" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="sgn_no" Editor="text" FieldName="sgn_no" NewRow="False" ReadOnly="False" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="sgn_type" Editor="text" FieldName="sgn_type" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="OrderNo" ParentFieldName="OrderNo" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>

                <JQTools:JQDialog ID="Dialog_IndateCheck" runat="server" BindingObjectID="dataFormDetail" EditMode="Dialog" Title="入境確認維護" Width="590px" DialogLeft="170px" ShowSubmitDiv="False">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="FWCRMOrdersDetails" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormOrders" RemoteName="sFWCRMStickStatus.FWCRMOrders" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoadSuccessDFIndateCheck">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" NewRow="False" ReadOnly="True" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="批次" Editor="numberbox" FieldName="Item" Format="" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="40" />
                            <JQTools:JQFormColumn Alignment="left" Caption="項目訂單人數" Editor="text" FieldName="PersonQtyOriginal" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="OrderNo" ParentFieldName="OrderNo" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDataGrid ID="dataGrid_IndateCheck" runat="server" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" data-options="pagination:true,view:commandview" DataMember="FWCRMIndateCheck" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sFWCRMStickStatus.FWCRMIndateCheck" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ParentObjectID="">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="入境單號碼" Editor="text" EditorOptions="" FieldName="IndateNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="right" Caption="人數" Editor="numberbox" FieldName="PersonQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="sum" Visible="True" Width="60">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="入境單檔案" Editor="text" FieldName="IndateImg" Format="" FormatScript="downloadScript" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="250" />
                            <JQTools:JQGridColumn Alignment="center" Caption="預定入境日" Editor="text" FieldName="PlanIndate" Format="yyyy-mm-dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="Item" Editor="text" FieldName="Item" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="iAutokey" Editor="text" FieldName="iAutokey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <TooItems>
                            <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        </TooItems>
                    </JQTools:JQDataGrid>
                </JQTools:JQDialog>

            </JQTools:JQDialog>

        </div>

            <JQTools:JQDialog ID="JQDialog_StickStatus" runat="server" BindingObjectID="" Title="訂單進度查詢" ShowSubmitDiv="False" Width="850px" DialogLeft="10px" DialogTop="10px">
                <JQTools:JQDataGrid ID="dataGridStickStatus" data-options="pagination:true,view:commandview" RemoteName="sFWCRMStickStatus.FWCRMStickStatus" runat="server" AutoApply="True"
                DataMember="FWCRMStickStatus" Pagination="True" QueryTitle=""
                Title="" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="OnLoadSuccess">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" MaxLength="0" Width="90" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Format="" MaxLength="0" Width="155" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="WorkNo" Editor="text" FieldName="WorkNo" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="日期" Editor="datebox" FieldName="StatusDate" Format="" Sortable="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="狀態" Editor="text" FieldName="StatusName" Format="" MaxLength="0" Width="125" />
                        <JQTools:JQGridColumn Alignment="center" Caption="人數" Editor="numberbox" FieldName="PersonQty" Format="" Width="50" />
                        <JQTools:JQGridColumn Alignment="left" Caption="進度/結果" Editor="text" FieldName="StatusResult" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Notes" Format="" MaxLength="0" Width="346" />
                        <JQTools:JQGridColumn Alignment="center" Caption="負責業務" Editor="text" FieldName="NAME_C" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="65">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="iAutoKey" Editor="numberbox" FieldName="iAutoKey" Format="" Width="120" Visible="False" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                    </TooItems>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
</form>
</body>
</html>
