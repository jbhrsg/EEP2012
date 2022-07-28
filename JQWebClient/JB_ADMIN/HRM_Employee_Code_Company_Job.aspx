<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_Employee_Code_Company_Job.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="IE=edge" />
    <script src="../js/jquery.blockUI.js"></script>
    <title>職缺資料維護</title>
    <script>
        var DataForm_Master_ID = '#dataFormMaster';
        var Tab_Management_ID = '#Tab_Management';

        $(document).ready(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "#FFFFB5");
            });

            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });

            //-----------------------------------前台公告職缺-顯示日期改成 紫色 顯示----------------------------
            var HideFieldName = ['Front_ShowDate'];
            var FormName = '#dataFormFront';

            $.each(HideFieldName, function (index, fieldName) {
                $(FormName + fieldName).closest('td').prev('td').css("color", "rgb(138, 43, 226)");//改變td前面文字顏色
            });

            //-----------------------------------前台公告職缺-職缺薪資顯示----------------------------
            var Salary1 = $('#dataFormFrontFront_JOBSalary1').closest('td');
            var Salary2 = $('#dataFormFrontFront_JOBSalary2').closest('td').children();
            Salary1.append("&nbsp;～").append(Salary2);

            //-----------------------------------欄位配對視窗送出按鈕----------------------------
            $('#DialogSubmit', '#Dialog_ImportMain').removeAttr('onclick').on('click', function () {
                if (!$('#DataForm_ImportMain').form('validateForm')) return;            //驗證
                var titleObject = $('#DataForm_ImportMain').jbDataFormGetAFormData();   //取資料

                $('#Dialog_Import').jbExcelFileImport('importFile', {
                    remoteName: '_HRM_Employee_Code_Company_Job',
                    method: 'ExcelFileImport',
                    sheetIndex: $('#DataForm_SheetImportMainSHEET').combobox('getValue'),
                    titleObject: titleObject,
                    parameters: ''
                });
            });
            //-----------------------------------讀取ExcelJquery----------------------------------
            $('#Dialog_Import').jbExcelFileImport({
                OnFileUploadSuccess: function () {
                    //開啟配對視窗
                    openForm('#Dialog_ImportMain', {}, 'inserted', 'Dialog');
                    $(this).jbExcelFileImport('changeSheetByName', '加班單');
                },
                OnGetTitleSuccess: function (SheetArray, TitleArray) {

                    //SheetChangge
                    $('#DataForm_SheetImportMainSHEET').combobox('clear').combobox('loadData', SheetArray);

                    //載入選項以及預設
                    $('#DataForm_ImportMain').find('.info-combobox').each(function () {
                        $(this).combobox('loadData', TitleArray).combobox('clear');
                        $(this).combobox('selectExistsForText', $(this).closest('td').prev('td').html());
                    });
                },
                OnImportSuccess: function (jsonStr) {
                    var json = $.parseJSON(jsonStr);
                    if (!json.IsOK) {
                        var showMessage = json.ErrorMsg;
                        if (json.Result) {
                            showMessage += $('<a>', { href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: '錯誤資料.xls', FilePathName: json.Result }), target: '_blank' })
                                            .html('檔案下載')[0].outerHTML;
                        }
                        $.messager.alert('匯入失敗', showMessage, 'error');
                    }
                    else {
                        $.messager.alert(' ', "匯入成功");
                        $('#Dialog_Import').dialog('close');
                        $('#dataGridMaster').datagrid('reload');
                    }
                }
            });

            // 建立異動資料紀錄dialog
            $("#Dialog_TransLog").dialog(
                {
                    height: 400,
                    width: 550,
                    resizable: false,
                    modal: true,
                    title: "職缺資料代碼設定異動資料紀錄",
                    closed: true,
                    buttons: [{
                        text: '結束',
                        handler: function () { $("#Dialog_TransLog").dialog("close") }
                    }]
                });
        });

        //異動資料欄位超連結
        function HyperlinkLog(value, row, index) {
            return "<a href='javascript: void(0)' onclick='LinkLog(" + index + ");'>" + value + "</a>";
        }

        function LinkLog(index) {
            //alert(index)
            $("#dataGridMaster").datagrid('selectRow', index);
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var ID = rows.COMPANY_JOB_ID;
            $("#Dialog_TransLog").dialog("open");
            $("#DG_HRM_COMPANY_JOB_LOG").datagrid('setWhere', "HRM_COMPANY_JOB_LOG.COMPANY_JOB_ID = '" + ID + "'");
        }

        function openImportExcel() {
            $("#Dialog_Import").dialog("open");
        }

        //---------------------------------------匯入Excel Sheet切換------------------------------
        var DataForm_SheetImportMainSHEET_OnSelect = function (rowData) {
            $('#Dialog_Import').jbExcelFileImport('changeSheetByIndex', rowData.value);
        }
        //---------------------------------------Grid載入完成-------------------------------------
        var dataGridView_OnLoadSuccess = function () {
            var dgid = $(this);
            //第一次載入Grid用
            if (!dgid.data('alreadyFirstLoad') && dgid.data('alreadyFirstLoad', true)) {
                //首先判斷頁面
                var ID = Request.getQueryStringByName2("ID");
                if (ID) {
                    var defaultWhereStr = String.format("HRM_COMPANY_JOB.COMPANY_JOB_ID='{0}'", ID);
                    $(this).data('defaultWhereStr', defaultWhereStr).datagrid("setWhere", defaultWhereStr);
                }
                else {
                    //一般頁面則進行一般頁面之預先設定
                    var dgid = $(this);
                    var pnid = getInfolightOption($(dgid)).queryDialog;
                    if (pnid != undefined) {
                        clearQuery(dgid);
                        setQueryDefault(pnid);
                        $(dgid).datagrid('setWhere', $(dgid).datagrid('getWhere'));
                    }
                }
            }
        }

        //判斷資料是否有重複
        function checkParameterDate(val) {
            var EmployeeID = $('#dataFormMasterCOMPANY_JOB_ID').val();

            if ($("#dataGridMaster").datagrid('getSelected')) {
                o_EmployeeID = $("#dataGridMaster").datagrid('getSelected').COMPANY_JOB_ID;
            }
            if (getEditMode($("#dataFormMaster")) == 'inserted' || (o_EmployeeID != EmployeeID)) {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Employee_Code_Company_Job.HRM_COMPANY_JOB', //連接的Server端，command
                    data: "mode=method&method=" + "checkJobCode" + "&parameters=" + EmployeeID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });
                if ((cnt != "0" && cnt != "undefined")) {
                    alert('此職缺資料已存在');
                    return false;
                }
                else
                    return true;
            }
            else
                return true;
        }


        function gridReload() {
            $("#dataGridMaster").datagrid('reload');

            //---------------------------------------公告職缺地點處理----------------------------------
            UpdateRecReference();

        }


        //---------------------------------------Grid欄位FormatScript-----------------------------
        var DataGrid_BaseIO_FormatScript = function (value, row, index) {
            var fieldName = this.field;
            switch (fieldName) {
                case 'ToolBar':
                    return $("#dataGridMaster").jbDataGridMenuButton('createButton', index);
                    break;
                default:
                    return '';
                    break;
            }
        }

        //---------------------------------------Form載入之後-------------------------------------
        var DataForm_OnLoadSuccess = function (RowData) {
            var defaultWhereStr = '';
            var theGrid = '';

            var thisDataForm = $(this);
            var form_ID = '#' + thisDataForm.attr('id');
            switch (form_ID) {
                case DataForm_Master_ID:
                    var TabIndexList = [];
                    if (RowData.COMPANY_JOB_ID) {
                        //Tab方法
                        $(Tab_Management_ID).tabs({
                            onSelect: function (title, index) {
                                if ($.inArray(index, TabIndexList) == -1) {
                                    if (TabSelectedLoad.call($(this).tabs('getSelected'), RowData.COMPANY_JOB_ID)) TabIndexList.push(index);
                                }
                            }
                        }).tabs('select', 0);
                    }
                    break;
                default:
                    break;
            }

            //------公告職缺地點顯示處理--------------------------------------------------------
            var DutyAreasIDs = $("#dataFormFrontFront_DutyAreasIDs").options('getValue');
            var DutyAreaClassID = $("#dataFormFrontFront_DutyAreaClassIDs").options('getValue');
            if (DutyAreaClassID == "") {
                OnWhereClassID = " ClassID =111";
            } else {
                OnWhereClassID = " ClassID =" + DutyAreaClassID;
            }
            $('#dataFormFrontFront_DutyAreasIDs').options('initializePanel');
            $("#dataFormFrontFront_DutyAreasIDs").options('setValue', DutyAreasIDs);


        }
        //---------------------------------------Tab頁面載入方法----------------------------------
        var TabSelectedLoad = function (ID) {
            var theTab = $(this);
            var Iframe = theTab.find('iframe');
            if (Iframe) {
                var url = Iframe.data('src');
                if (url) {
                    url = url + '?' + $.param({ 'ID': ID });
                    theTab.block({ message: 'Loading.....', css: { border: 'none', padding: '15px', backgroundColor: '#fff', '-webkit-border-radius': '10px', '-moz-border-radius': '10px', opacity: .3, color: '#000' } });
                    Iframe.attr('src', url).load(function () { theTab.unblock(); });
                    return true;
                } else Iframe.attr('src', 'about:blank');
            }
            return false;
        }
        //---------------------------------------保險福利----------------------------------
        function getWelfareItemName() {
            var welfareItemID = $('#dataFormWelfareWELFARE_ITEM').options('getValue');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Employee_Code_Company_Job.HRM_COMPANY_JOB', //連接的Server端，command
                data: "mode=method&method=" + "getWelfareItemName" + "&parameters=" + welfareItemID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        rows = $.parseJSON(data);
                        if (rows.length > 0)
                            $('#dataFormWelfareWELFARE_ITEM_NAME').val(rows[0].WELFARE_ITEM_NAME);
                    }
                }
            });
        }
        function getWelfareJbItemName() {
            var welfareItemID = $('#dataFormWelfareWELFARE_JBITEM').options('getValue');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Employee_Code_Company_Job.HRM_COMPANY_JOB', //連接的Server端，command
                data: "mode=method&method=" + "getWelfareItemName" + "&parameters=" + welfareItemID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        rows = $.parseJSON(data);
                        if (rows.length > 0)
                            $('#dataFormWelfareWELFARE_JBITEM_NAME').val(rows[0].WELFARE_ITEM_NAME);
                    }
                }
            });
        }

        function getDemendDataName() {
            var demendDataID = $('#dataFormWelfareDEMEND_DATA_ID').options('getValue');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Employee_Code_Company_Job.HRM_COMPANY_JOB', //連接的Server端，command
                data: "mode=method&method=" + "getDemendDataName" + "&parameters=" + demendDataID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        rows = $.parseJSON(data);
                        if (rows.length > 0)
                            $('#dataFormWelfareDEMEND_DATA_NAME').val(rows[0].DEMEND_DATA_NAME);
                    }
                }
            });
        }

        //---------------------------------------公告職缺顯示處理----------------------------------
        
        //選擇客戶聯動到公告的客戶名稱
        function OnSelectCOMPANY() {
            var sCOMPANY_CODE = $("#dataFormMasterCOMPANY_CODE").refval('selectItem').text;
            if ($('#dataFormFrontFront_COMPANYName').val() == "") {
                $('#dataFormFrontFront_COMPANYName').val(sCOMPANY_CODE);
            }
        }
        //填完職缺名稱聯動到公告的職缺名稱
        function OnBlurCOMPANY_JOB_CNAME() {
            var sCOMPANY_JOB_CNAME = $("#dataFormMasterCOMPANY_JOB_CNAME").val();
            if ($('#dataFormFrontFront_COMPANY_JOBName').val() == "") {
                $('#dataFormFrontFront_COMPANY_JOBName').val(sCOMPANY_JOB_CNAME);
            }
        }

        //工作縣市,工作地點連動
        var OnWhereClassID;
        function OnSelectDutyAreaClass(rowdata) {
            var DutyAreasIDs = $("#dataFormFrontFront_DutyAreasIDs").options('getValue');
            //var DutyAreaClassID = $("#dataFormMaster3DutyAreaClassIDs").options('getValue');
            if (rowdata != "") {
                OnWhereClassID = " ClassID in (" + rowdata + ")";
            }
            $('#dataFormFrontFront_DutyAreasIDs').options('initializePanel');
            $("#dataFormFrontFront_DutyAreasIDs").options('setValue', DutyAreasIDs);

        }
        function OnWhereAreaClassID(param) {
            return OnWhereClassID;
        }
       
        //// 修改多選選項對應的文字
        function UpdateRecReference() {
            //var row = $('#dataGridMaster').datagrid('getSelected');
            var COMPANY_JOB_ID = $("#dataFormFrontCOMPANY_JOB_ID").val();//公告代號 
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_COMPANY_JOBFront.HRM_COMPANY_JOBFront', //連接的Server端，command
                data: "mode=method&method=" + "UpdateRecReference" + "&parameters=" + encodeURIComponent(COMPANY_JOB_ID), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });

        }

        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridMaster') {
                //查詢條件
                var result = [];
                var CustID = $('#COMPANY_ID_Query').refval('getValue');//客戶代號
                var JobName = $('#COMPANY_JOB_CNAME_Query').val();//職缺名稱
                if (CustID != '') result.push("[HRM_COMPANY_JOB].COMPANY_ID = '" + CustID + "'");
                if (JobName != '') result.push("[HRM_COMPANY_JOB].COMPANY_JOB_CNAME like '%" + JobName + "%'");

                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="_HRM_Employee_Code_Company_Job.HRM_COMPANY_JOB" runat="server" AutoApply="True"
                DataMember="HRM_COMPANY_JOB" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="" QueryTop="100px" QueryLeft="100px" OnLoadSuccess="dataGridView_OnLoadSuccess" AlwaysClose="False" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryMode="Panel" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="職缺流水號" Editor="numberbox" FieldName="COMPANY_JOB_ID" Format="" Visible="false" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶流水號" Editor="text" FieldName="COMPANY_ID" Format="" MaxLength="50" Visible="false" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="COMPANY_CODE" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="COMPANY_ABBR" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺代號" Editor="text" FieldName="COMPANY_JOB_CODE" Format="" MaxLength="50" Visible="true" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺中文名稱" Editor="text" FieldName="COMPANY_JOB_CNAME" Format="" MaxLength="50" Visible="true" Width="90" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺英文名稱" Editor="text" FieldName="COMPANY_JOB_ENAME" Format="" MaxLength="50" Visible="False" Width="90" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺類別" Editor="text" FieldName="JOB_TYPE_NAME" Format="" MaxLength="50" Visible="true" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="COMPANY_DEPT_CNAME" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="工作地點流水號" Editor="text" FieldName="WORK_ID" Format="" MaxLength="50" Visible="false" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="工作地點" Editor="text" FieldName="WORK_ADDR" Format="" MaxLength="50" Visible="true" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="負責招募人員" Editor="text" FieldName="RECRUIT_EMPLOYEE_NAME" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="負責關懷人員" Editor="text" FieldName="CARE_EMPLOYEE_NAME" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="負責業務人員" Editor="text" FieldName="SALES_EMPLOYEE" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺需求人數" Editor="text" FieldName="DEMAND_NUMBER" Format="" MaxLength="50" Visible="true" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="需求日期" Editor="text" FieldName="DEMAND_DATE" Format="yyyy/mm/dd" MaxLength="50" Visible="true" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主法定成本是否實報實銷" Editor="text" FieldName="IS_COST" Format="" MaxLength="50" Visible="true" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="投保勞保" Editor="text" FieldName="IS_LABOR" Format="" MaxLength="200" Visible="true" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="投保健保" Editor="text" FieldName="IS_HEALTH" Format="" MaxLength="50" Visible="true" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="投保勞退" Editor="text" FieldName="IS_RETIRE" Format="" MaxLength="50" Visible="true" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="勞保投保金額" Editor="text" FieldName="LABOR_AMT" Format="" MaxLength="50" Visible="true" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="健保投保金額" Editor="text" FieldName="HEALTH_AMT" Format="" MaxLength="50" Visible="true" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="勞退投保金額" Editor="text" FieldName="RETIRE_AMT" Format="" MaxLength="50" Visible="true" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="傑報團保方案" Editor="text" FieldName="INSURANCE_GROUP_TYPE_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶團保費用" Editor="text" FieldName="COMPANY_GROUP_AMT" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="福利項目" Editor="text" FieldName="WELFARE_ITEM_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="應繳資料" Editor="text" FieldName="DEMEND_DATA_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="MEMO" Format="" MaxLength="50" Visible="true" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Format="" MaxLength="50" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:mm:SS" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Format="" MaxLength="50" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:mm:SS" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動資料紀錄" Editor="text" FieldName="TRANSLOG" FormatScript="HyperlinkLog" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="True" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增職缺" />
<%--                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />--%>
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-importExcel" ItemType="easyui-linkbutton" OnClick="openImportExcel" Text="匯入EXCEL" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn Caption="選擇客戶" Condition="=" FieldName="COMPANY_ID" IsNvarChar="True" Editor="inforefval" EditorOptions="title:'選擇客戶',panelWidth:350,remoteName:'_HRM_Employee_Share.HRM_COMPANY',tableName:'HRM_COMPANY',columns:[{field:'COMPANY_ID',title:'客戶代碼',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'COMPANY_ABBR',title:'客戶名稱',width:170,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'COMPANY_ID',value:'COMPANY_ID'}],whereItems:[],valueField:'COMPANY_ID',textField:'COMPANY_ABBR',valueFieldCaption:'客戶流水號',textFieldCaption:'客戶名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" Width="165" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="職缺名稱" Condition="%%" DataType="string" Editor="text" FieldName="COMPANY_JOB_CNAME" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="職缺資料維護" Width="970px" DialogTop="20px" DialogLeft="20px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HRM_COMPANY_JOB" HorizontalColumnsCount="5" RemoteName="_HRM_Employee_Code_Company_Job.HRM_COMPANY_JOB" OnApply="checkParameterDate" OnApplied="gridReload" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="DataForm_OnLoadSuccess" ChainDataFormID="dataFormFront" ParentObjectID="">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="ID" Editor="text" FieldName="COMPANY_JOB_ID" MaxLength="50" Width="80" Visible="False" Span="1" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職缺代號" Editor="text" FieldName="COMPANY_JOB_CODE" MaxLength="50" Width="80" Span="1" Visible="False" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="inforefval" FieldName="COMPANY_CODE" MaxLength="0" Width="180" Span="1" Visible="True" EditorOptions="title:'選擇客戶',panelWidth:350,remoteName:'_HRM_Employee_Share.HRM_COMPANY',tableName:'HRM_COMPANY',columns:[{field:'COMPANY_CODE',title:'客戶代碼',width:110,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'COMPANY_ABBR',title:'客戶名稱',width:170,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'COMPANY_ID',value:'COMPANY_ID'}],whereItems:[],valueField:'COMPANY_CODE',textField:'COMPANY_ABBR',valueFieldCaption:'客戶代碼',textFieldCaption:'客戶名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,onSelect:OnSelectCOMPANY,selectOnly:false,capsLock:'none',fixTextbox:'false'" NewRow="True" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="COMPANY_JOB_CNAME" MaxLength="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="230" OnBlur="OnBlurCOMPANY_JOB_CNAME" />
                        <JQTools:JQFormColumn Alignment="left" Caption="英文名稱" Editor="text" FieldName="COMPANY_JOB_ENAME" MaxLength="50" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="False" Width="200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求人數" Editor="numberbox" FieldName="DEMAND_NUMBER" MaxLength="50" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="70" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求日期" Editor="datebox" FieldName="DEMAND_DATE" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶Key" Editor="text" FieldName="COMPANY_ID" MaxLength="0" Width="180" Visible="False" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />

                        <JQTools:JQFormColumn Alignment="left" Caption="客戶部門Key" Editor="text" FieldName="COMPANY_DEPT_ID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶部門" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_HRM_Employee_Share.HRM_COMPANY_DEPT',tableName:'HRM_COMPANY_DEPT',columns:[{field:'COMPANY_CODE',title:'客戶代碼',width:80,align:'left',table:''},{field:'COMPANY_ABBR',title:'客戶名稱',width:80,align:'left',table:''},{field:'COMPANY_DEPT_CODE',title:'客戶部門代碼',width:80,align:'left',table:''},{field:'COMPANY_DEPT_CNAME',title:'客戶部門名稱',width:80,align:'left',table:''}],columnMatches:[{field:'COMPANY_DEPT_ID',value:'COMPANY_DEPT_ID'}],whereItems:[],valueField:'COMPANY_DEPT_CODE',textField:'COMPANY_DEPT_CNAME',valueFieldCaption:'客戶部門代號',textFieldCaption:'客戶部門名稱',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="COMPANY_DEPT_CODE" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />

                        <JQTools:JQFormColumn Alignment="left" Caption="職缺類別" Editor="infocombobox" EditorOptions="valueField:'CODE',textField:'NAME',remoteName:'_HRM_Employee_Code_Company_Job.cb_JOB_TYPE_ID',tableName:'cb_JOB_TYPE_ID',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JOB_TYPE_ID" MaxLength="50" Width="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作地點Key" Editor="text" FieldName="WORK_ID" MaxLength="0" Visible="False" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作地點" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_HRM_Employee_Share.HRM_WORKPLACE',tableName:'HRM_WORKPLACE',columns:[{field:'WORK_CODE',title:'工作地點代號',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'WORK_ADDR',title:'工作地點名稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'WORK_ID',value:'WORK_ID'}],whereItems:[],valueField:'WORK_CODE',textField:'WORK_ADDR',valueFieldCaption:'工作地點代號',textFieldCaption:'工作地點',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none'" FieldName="WORK_CODE" MaxLength="50" Width="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />

                        <JQTools:JQFormColumn Alignment="left" Caption="休假類別" Editor="infocombobox" EditorOptions="valueField:'CODE',textField:'NAME',remoteName:'_HRM_Employee_Code_Company_Job.cb_HOLIDAY_TYPE',tableName:'cb_HOLIDAY_TYPE',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HOLIDAY_TYPE" MaxLength="50" Width="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />

                        <JQTools:JQFormColumn Alignment="left" Caption="計薪方式" Editor="infocombobox" EditorOptions="valueField:'CODE',textField:'NAME',remoteName:'_HRM_Employee_Code_Company_Job.cb_SALARY_TYPE',tableName:'cb_SALARY_TYPE',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SALARY_TYPE" MaxLength="50" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />

                        <JQTools:JQFormColumn Alignment="left" Caption="招募人員" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ConsultantName',remoteName:'_HRM_Employee_Code_Company_Job.cb_Consultants',tableName:'cb_Consultants',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="RECRUIT_EMPLOYEE_ID" MaxLength="50" Width="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="關懷人員" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ConsultantName',remoteName:'_HRM_Employee_Code_Company_Job.cb_Consultants',tableName:'cb_Consultants',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CARE_EMPLOYEE_ID" MaxLength="50" Width="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務人員" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ConsultantName',remoteName:'_HRM_Employee_Code_Company_Job.cb_Consultants',tableName:'cb_Consultants',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SALES_EMPLOYEE_ID" MaxLength="50" Width="100" Span="1" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="上班時間" Editor="text" FieldName="REPORT_ON_TIME" MaxLength="100" Width="90" Visible="True" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="下班時間" Editor="text" FieldName="REPORT_OFF_TIME" MaxLength="100" Width="90" Visible="True" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />

                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn DefaultValue="0" FieldName="COMPANY_JOB_ID" CarryOn="False" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="COMPANY_JOB_CODE" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="COMPANY_JOB_CODE" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="COMPANY_JOB_CNAME" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="COMPANY_CODE" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="JOB_TYPE_ID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="COMPANY_DEPT_CODE" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="WORK_CODE" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RECRUIT_COMPANY_JOB_ID" RemoteMethod="True" ValidateType="None" />

                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DEMAND_NUMBER" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DEMAND_DATE" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
             
                <div id="tabs1" class="easyui-tabs" style="width:900px">
                    <div style="padding:10px" title="前台公告資訊">
               
                        <JQTools:JQDataForm ID="dataFormFront" runat="server" AlwaysReadOnly="False" ChainDataFormID="dataFormWelfare" Closed="False" ContinueAdd="False" DataMember="HRM_COMPANY_JOB" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="6" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="" RemoteName="_HRM_Employee_Code_Company_Job.HRM_COMPANY_JOB" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                            <Columns>
                                <JQTools:JQFormColumn Alignment="left" Caption="ID" Editor="text" FieldName="COMPANY_JOB_ID" MaxLength="50" Span="1" Visible="False" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" />
                                <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="Front_COMPANYName" Width="220" maxlength="0" Span="2" NewRow="True" ReadOnly="False" RowSpan="1" Visible="True" />
                                <JQTools:JQFormColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="Front_COMPANY_JOBName" MaxLength="200" NewRow="False" Span="4" Width="280" Format="" />
                                <JQTools:JQFormColumn Alignment="left" Caption="職缺縣市" Editor="infooptions" EditorOptions="title:'職缺縣市',panelWidth:200,remoteName:'_HRM_REC_User_Management.infoREC_ZDutyAreasClass',tableName:'infoREC_ZDutyAreasClass',valueField:'ID',textField:'Contents',columnCount:2,multiSelect:true,openDialog:false,selectAll:false,onSelect:OnSelectDutyAreaClass,selectOnly:false,items:[]" FieldName="Front_DutyAreaClassIDs" Format="" MaxLength="0" NewRow="True" Span="1" Width="90" Visible="True" />
                                <JQTools:JQFormColumn Alignment="left" Caption="職缺地點" Editor="infooptions" EditorOptions="title:'職缺地點',panelWidth:590,remoteName:'_HRM_REC_User_Management.infoREC_ZDutyAreas',tableName:'infoREC_ZDutyAreas',valueField:'ID',textField:'Contents',columnCount:8,multiSelect:true,openDialog:false,selectAll:false,onWhere:OnWhereAreaClassID,selectOnly:false,items:[]" FieldName="Front_DutyAreasIDs" Format="" Span="5" Width="90" />
                                <JQTools:JQFormColumn Alignment="right" Caption="職缺薪資" Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="Front_JOBSalary1" maxlength="0" NewRow="False" Span="1" Width="60" />
                                <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="numberbox" EditorOptions="precision:0,groupSeparator:',',prefix:''" FieldName="Front_JOBSalary2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" />
                                <JQTools:JQFormColumn Alignment="left" Caption="職缺排序" Editor="numberbox" FieldName="Front_ShowOrder" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="45" />
                                <JQTools:JQFormColumn Alignment="left" Caption="顯示日期" Editor="datebox" FieldName="Front_ShowDate" Format="" MaxLength="0" NewRow="False" Span="1" Width="90" />
                                <JQTools:JQFormColumn Alignment="left" Caption="有效性" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="Front_IsActive" Format="" maxlength="0" NewRow="False" Span="1" Width="40" />
                                <JQTools:JQFormColumn Alignment="left" Caption="薪資福利" Editor="textarea" EditorOptions="height:100" FieldName="Front_JOBWelfare" Format="" MaxLength="500" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="360" />
                                <JQTools:JQFormColumn Alignment="left" Caption="工作內容" Editor="textarea" EditorOptions="height:100" FieldName="Front_JOBContent" Format="" MaxLength="500" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="360" />
                                <JQTools:JQFormColumn Alignment="left" Caption="備    註" Editor="textarea" FieldName="Front_JOBRemark" Format="" maxlength="500" Span="6" Width="820" EditorOptions="height:85" NewRow="True" />

                            </Columns>
                        </JQTools:JQDataForm>
               
                        <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="dataFormFront" EnableTheming="True">
                            <Columns>
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Front_ShowOrder" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="Front_IsActive" RemoteMethod="True" />
                            </Columns>
                        </JQTools:JQDefault>
               
                    </div>
                    <div style="padding:10px" title="保險福利">
                        <JQTools:JQDataForm ID="dataFormWelfare" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HRM_COMPANY_JOB" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="3" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="_HRM_Employee_Code_Company_Job.HRM_COMPANY_JOB" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                            <Columns>
                                <JQTools:JQFormColumn Alignment="left" Caption="COMPANY_JOB_ID" Editor="text" FieldName="COMPANY_JOB_ID" MaxLength="50" Span="1" Visible="False" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="雇主法定成本實支實付否" Editor="checkbox" EditorOptions="on:'Y',off:'N'" FieldName="IS_COST" NewRow="False" Width="20" />
                                <JQTools:JQFormColumn Alignment="left" Caption="投保勞保" Editor="checkbox" EditorOptions="on:'Y',off:'N'" FieldName="IS_LABOR" Format="" MaxLength="50" NewRow="True" Width="20" />
                                <JQTools:JQFormColumn Alignment="left" Caption="勞保投保金額" Editor="numberbox" FieldName="LABOR_AMT" MaxLength="50" NewRow="False" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="投保健保" Editor="checkbox" EditorOptions="on:'Y',off:'N'" FieldName="IS_HEALTH" Format="" MaxLength="50" NewRow="True" Width="20" />
                                <JQTools:JQFormColumn Alignment="left" Caption="健保投保金額" Editor="numberbox" FieldName="HEALTH_AMT" MaxLength="50" NewRow="False" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="投保勞退" Editor="checkbox" EditorOptions="on:'Y',off:'N'" FieldName="IS_RETIRE" Format="" MaxLength="50" NewRow="True" Width="20" />
                                <JQTools:JQFormColumn Alignment="left" Caption="勞退投保金額" Editor="numberbox" FieldName="RETIRE_AMT" MaxLength="50" NewRow="False" RowSpan="1" Span="1" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="傑報團保方案" Editor="infocombobox" EditorOptions="valueField:'INSURANCE_GROUP_TYPE_ID',textField:'INSURANCE_GROUP_TYPE_NAME',remoteName:'_HRM_Employee_Code_Company_Job.cb_HRM_INSURANCE_GROUP_TYPE',tableName:'cb_HRM_INSURANCE_GROUP_TYPE',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="INSURANCE_GROUP_TYPE_ID" MaxLength="50" NewRow="True" RowSpan="1" Span="1" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="客戶團保費用" Editor="text" FieldName="COMPANY_GROUP_AMT" MaxLength="50" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="客戶福利項目" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:300,remoteName:'_HRM_Employee_Code_Company_Job.cb_WELFARE_ITEM',tableName:'cb_WELFARE_ITEM',valueField:'CODE',textField:'NAME',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:getWelfareItemName,selectOnly:false,items:[]" FieldName="WELFARE_ITEM" MaxLength="50" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Width="500" />
                                <JQTools:JQFormColumn Alignment="left" Caption="客戶福利項目" Editor="text" FieldName="WELFARE_ITEM_NAME" MaxLength="0" Visible="False" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="傑報福利項目" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:300,remoteName:'_HRM_Employee_Code_Company_Job.cb_WELFARE_ITEM',tableName:'cb_WELFARE_ITEM',valueField:'CODE',textField:'NAME',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:getWelfareJbItemName,selectOnly:false,items:[]" FieldName="WELFARE_JBITEM" MaxLength="50" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Width="500" />
                                <JQTools:JQFormColumn Alignment="left" Caption="傑報福利項目" Editor="text" FieldName="WELFARE_JBITEM_NAME" MaxLength="0" Visible="False" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="應繳資料" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:400,remoteName:'_HRM_Employee_Code_Company_Job.cb_DEMEND_DATA_ID',tableName:'cb_DEMEND_DATA_ID',valueField:'CODE',textField:'NAME',columnCount:6,multiSelect:true,openDialog:false,selectAll:false,onSelect:getDemendDataName,selectOnly:false,items:[]" FieldName="DEMEND_DATA_ID" MaxLength="50" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="500" />
                                <JQTools:JQFormColumn Alignment="left" Caption="應繳資料" Editor="text" FieldName="DEMEND_DATA_NAME" MaxLength="0" Span="1" Visible="False" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="離職規定" Editor="textarea" EditorOptions="height:50" FieldName="LEAVE_MEMO" MaxLength="100" NewRow="False" ReadOnly="False" Span="3" Width="500" />
                                <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" EditorOptions="height:50" FieldName="MEMO" MaxLength="100" NewRow="False" ReadOnly="False" Span="3" Width="500" />
                            </Columns>
                        </JQTools:JQDataForm>
                    </div>
                </div>


            </JQTools:JQDialog>
        </div>
    </form>
    <!-- 匯入對話框內容的 DIV -->
    <div id="Dialog_Import">

        <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog">
            <JQTools:JQDataForm ID="DataForm_SheetImportMain" runat="server" RemoteName=" " DataMember=" " HorizontalColumnsCount="3" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="工作表" Editor="infocombobox" FieldName="SHEET" Width="120" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,onSelect:DataForm_SheetImportMainSHEET_OnSelect,panelHeight:200" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" RemoteName=" " DataMember=" " HorizontalColumnsCount="3" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="COMPANY_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="COMPANY_ID" RemoteMethod="True" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>
        </JQTools:JQDialog>
    </div>
    <!-- dialog對話框內容的 DIV -->
    <div id="Dialog_TransLog">
        <div class="div_RelativeLayout">
            <JQTools:JQDataGrid ID="DG_HRM_COMPANY_JOB_LOG" runat="server" RemoteName="_HRM_Employee_Code_Company_Job.HRM_COMPANY_JOB_LOG" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="HRM_COMPANY_JOB_LOG" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="LOG_STATE_NAME" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="USERNAME" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="LOG_USER" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="False" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動日期" Editor="datebox" FieldName="LOG_DATE" Frozen="False" MaxLength="8" ReadOnly="False" Sortable="False" Visible="True" Width="90" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                    <JQTools:JQGridColumn Alignment="right" Caption="職缺流水號" Editor="numberbox" FieldName="COMPANY_JOB_ID" Format="" Visible="false" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="text" FieldName="COMPANY_ID" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="雇用性質" Editor="text" FieldName="HrieName" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="支薪方式" Editor="text" FieldName="SalaryPayName" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="投保類型" Editor="text" FieldName="InsuranceName" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="動員月會組別" Editor="text" FieldName="MonthMeetingGroupName" Format="" MaxLength="200" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="可申請加班費" Editor="text" FieldName="IS_OVERTIME_HOUR" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="可申請加班補休" Editor="text" FieldName="IS_REST_HOUR" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="可申請補休超休" Editor="text" FieldName="IS_REST_OVER" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="誤餐津貼" Editor="text" FieldName="IS_DELAY_MEAL" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="誤餐津貼金額" Editor="text" FieldName="DELAY_MEAL_AMT" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班滿足時數(含)" Editor="text" FieldName="DELAY_MEAL_OVERTIME_INCLUSIVE" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="超過下班小時(含)" Editor="text" FieldName="DELAY_MEAL_OVERTIME_OVER" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="房務人員" Editor="text" FieldName="IS_HOUSEKEEPING" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="臨時工否" Editor="text" FieldName="IS_TEMPORARY_WORKER" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="不產生特休假" Editor="text" FieldName="IS_NOT_CREATE_YEAR_HOLIDAY" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="VD登入帳號" Editor="text" FieldName="VD_ACCOUNT" Format="" MaxLength="50" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Format="" MaxLength="50" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:mm:SS" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Format="" MaxLength="50" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:mm:SS" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Sortable="True" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" Visible="True" />
                </TooItems>
            </JQTools:JQDataGrid>
        </div>
    </div>
</body>
</html>
