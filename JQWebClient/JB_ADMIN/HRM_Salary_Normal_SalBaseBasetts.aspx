<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_Salary_Normal_SalBaseBasetts.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>基本薪資資料核定</title>

</head>
<body>
    <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
    <script type="text/javascript">
        var dataGridView_ID = "#dataGridView";
        var JQDialog1_ID = '#JQDialog1';

        var dataFormMaster_ID = '#dataFormMaster';
        var dataFormMasterEMPLOYEE_CODE_ID = dataFormMaster_ID + 'EMPLOYEE_CODE';
        var dataFormMasterEFFECT_DATE_ID = dataFormMaster_ID + 'EFFECT_DATE';

        var dataGridView_OldSetting_ID = '#dataGridView_OldSetting';

        var JQDialog1Log_ID = '#JQDialog1Log';
        var dataGridViewLog_ID = '#dataGridViewLog';

        var Dialog_Import_ID = '#Dialog_Import';
        var Dialog_ImportMain_ID = '#Dialog_ImportMain';
        var DataForm_ImportMain_ID = '#DataForm_ImportMain';
        //=======================================【Ready】=========================================
        $(function () {
            //-----------------------------------觸發(抓到以前設定)-------------------------------
            //$(dataFormMasterEMPLOYEE_CODE_ID).data('inforefval').refval.find('input.refval-text').blur(function () {
            //    GetOldSetByMan();
            //});
            //$(dataFormMasterEFFECT_DATE_ID).datebox({
            //    onSelect: function (date) {
            //        GetOldSetByMan();
            //    }
            //}).combo('textbox').blur(function () {
            //    GetOldSetByMan();
            //});
            //-------------------------------LogDialog整形------------------------------------
            (function () {
                var girdPanel = $(dataGridViewLog_ID).datagrid('getPanel').parent();
                girdPanel.parent().css('padding', 0).parent().css('padding', 0);
            })();
            //-----------------------------------讀取ExcelJquery---------------------------------            
            $(Dialog_Import_ID).jbExcelHandlerFileImport({
                OnGetTitleSuccess: function (dataArray, fileName) {

                    //開啟配對視窗                    
                    openForm(Dialog_ImportMain_ID, {}, 'inserted', 'Dialog');

                    //載入選項以及預設
                    $(DataForm_ImportMain_ID).find('.info-combobox').each(function () {
                        $(this).combobox('loadData', dataArray).combobox('clear');
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
                        $.messager.alert('匯入完成', json.Result);
                        $(dataGridView_ID).datagrid('reload');
                    }
                    closeForm(Dialog_ImportMain_ID);
                }
            });
            //-----------------------------------欄位配對視窗送出按鈕----------------------------
            $('#DialogSubmit', Dialog_ImportMain_ID).removeAttr('onclick').on('click', function () {
                if (!$(DataForm_ImportMain_ID).form('validateForm')) return;    //驗證                    
                var data = $(DataForm_ImportMain_ID).jbDataFormGetAFormData();  //取資料

                $(Dialog_Import_ID).jbExcelHandlerFileImport('importFile', {
                    remoteName: '_HRM_Salary_Normal_SalBaseBasetts',
                    method: 'ExcelFileImport',
                    handler: data
                });
            });
            //-------------------------------------------------------------------------------------
        });
        //=========================================================================================
        //---------------------------------------Grid載入完成-------------------------------------
        var dataGridView_OnLoadSuccess = function () {
            var dgid = $(this);
            //第一次載入Grid用
            if (!dgid.data('alreadyFirstLoad') && dgid.data('alreadyFirstLoad', true)) {
                //先載入預設條件然後先查
                var pnid = getInfolightOption($(dgid)).queryDialog;
                if (pnid != undefined) {
                    clearQuery(dgid);
                    setQueryDefault(pnid);
                    $(dgid).datagrid('setWhere', $(dgid).datagrid('getWhere'));
                    $.messager.show({
                        title: '提示',
                        msg: '當月份 基本薪資異動資料 載入',
                        showType: 'show'
                    });
                }
            }
        }
        //---------------------------------------Form載入完成-------------------------------------
        var dataFormMaster_OnLoadSuccess = function (data) {
            //GetOldSetByMan();
        }
        //---------------------------------------Form存檔之前-------------------------------------
        var dataFormMaster_OnApply = function () {
            var Ans = false;
            if ($(this).form('validateForm')) {
                var data = $(this).jbDataFormGetAFormData();
                $.ajaxSetup({ async: false });
                //檢查資料是否有重複
                $.post('../handler/JqDataHandle.ashx?RemoteName=_HRM_Salary_Normal_SalBaseBasetts', { mode: 'method', method: 'DataValidate', parameters: $.toJSONString(data) }
                      ).done(function (data) {
                          var Json = $.parseJSON(data);
                          if (Json.IsOK) Ans = true;
                          else alert(Json.ErrorMsg);
                      }).fail(function (xhr, textStatus, errorThrown) {
                          alert('error');
                      });
                $.ajaxSetup({ async: true });
            }
            return Ans;
        }
        //---------------------------------------Form存檔之後-------------------------------------
        var dataFormMaster_OnApplied = function () {
            $(this).jbDataFormReloadDataGrid();
        }
        //---------------------------------------異動資料Hyperlink--------------------------------
        var dataGridView_Hyperlink = function (value, row, index) {
            return "<a href='javascript: void(0)' onclick='dataGridView_Hyperlink_LinkLog(" + index + ");'>異動資料記錄</a>";
            return $('<a>', { href: 'javascript: void(0)', onclick: 'dataGridView_Hyperlink_LinkLog(' + index + ');' }).
                linkbutton({ iconCls: 'icon-search', plain: true })[0].outerHTML;
        }
        //---------------------------------------異動資料Hyperlink觸發----------------------------
        function dataGridView_Hyperlink_LinkLog(index) {
            var RowData = $(dataGridView_ID).datagrid('selectRow', index).datagrid('getSelected');
            $(dataGridViewLog_ID).datagrid("setWhere", String.format("BTS.SALBASE_BASETTS_ID='{0}'", RowData.SALBASE_BASETTS_ID));
            openForm(JQDialog1Log_ID, RowData, 'viewed', 'Dialog');
        }
        //---------------------------------------查詢舊的資料-------------------------------------
        var GetOldSetByMan = function () {
            var theGrid = $(dataGridView_OldSetting_ID);
            var data = $(dataFormMaster_ID).jbDataFormGetAFormData();
            theGrid.datagrid('loadData', []);
            $.post('../handler/JqDataHandle.ashx?RemoteName=_HRM_Salary_Normal_SalBaseBasetts',
                               { mode: 'method', method: 'GetOldSetting', parameters: $.toJSONString(data) },
                               function (data) {
                                   var Json = $.parseJSON(data);
                                   if (Json.IsOK) {
                                       var gData = { rows: Json.Result, total: Json.Result.length };
                                       //theGrid.datagrid('loadData', gData);
                                       theGrid.datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', gData);
                                   }
                                   //else $.messager.alert('錯誤', Json.ErrorMsg, 'error');
                               });
        }
        //---------------------------------------匯入Excel----------------------------------------
        var openImportExcel = function () {
            $(Dialog_Import_ID).dialog('open');
        }
        //-----------------------------------------------------------------------------------------
    </script>
    <form id="form1" runat="server">
        <JQTools:JQMultiLanguage ID="JQMultiLanguage1" runat="server" />
        <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="_HRM_Salary_Normal_SalBaseBasetts.HRM_SALARY_SALBASE_BASETTS" runat="server" AutoApply="True"
            DataMember="HRM_SALARY_SALBASE_BASETTS" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
            Title="基本薪資資料" QueryLeft="300px" QueryTop="100px" OnLoadSuccess="dataGridView_OnLoadSuccess" AlwaysClose="True" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryMode="Window" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
            <Columns>
                <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="DEPT_ID_NAME" Width="100" Sortable="True" Frozen="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EMPLOYEE_CODE" Format="" MaxLength="0" Width="80" Frozen="True" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" Format="" MaxLength="0" Width="80" Frozen="True" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="EFFECT_DATE" Format="" Width="80" Frozen="True" Sortable="True" />

                <JQTools:JQGridColumn Alignment="left" Caption="薪資項目代碼" Editor="text" FieldName="SALARY_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="薪資項目中文" Editor="text" FieldName="SALARY_CNAME" Format="" MaxLength="0" Width="80" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="薪資項目英文" Editor="text" FieldName="SALARY_ENAME" Format="" MaxLength="0" Width="80" Sortable="True" />
                <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="AMT_Decode" Format="N" Width="80" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="MEMO" Format="" MaxLength="0" Width="160" Sortable="True" />

                <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />

                <JQTools:JQGridColumn Alignment="left" Caption="異動資料記錄" Editor="text" FieldName="TRANSLOG" FormatScript="dataGridView_Hyperlink" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="True" Width="80" />
            </Columns>
            <TooItems>
                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                <JQTools:JQToolItem ID="Import" Icon="icon-importExcel" ItemType="easyui-linkbutton" OnClick="openImportExcel" Text="匯入EXCEL"  />
            </TooItems>
            <QueryColumns>
                <JQTools:JQQueryColumn AndOr="and" Caption="部門" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'DEPT_ID',textField:'DEPT_CNAME',remoteName:'_HRM_Salary_Normal_SalBaseBasetts.cb_HRM_DEPT',tableName:'cb_HRM_DEPT',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="DPT.DEPT_ID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="員工編號" Condition="%%" DataType="string" Editor="text" FieldName="EMPLOYEE_CODE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="員工姓名" Condition="%%" DataType="string" Editor="text" FieldName="NAME_C" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="生效日期" Condition=">=" DataType="datetime" Editor="datebox" FieldName="EFFECT_DATE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" DefaultValue="_firstday" />
                <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="<=" DataType="datetime" Editor="datebox" FieldName="EFFECT_DATE" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" DefaultValue="_lastday" />
            </QueryColumns>
        </JQTools:JQDataGrid>
        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="基本薪資資料">
            <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HRM_SALARY_SALBASE_BASETTS" HorizontalColumnsCount="2" RemoteName="_HRM_Salary_Normal_SalBaseBasetts.HRM_SALARY_BASETTS" OnApply="dataFormMaster_OnApply" OnApplied="dataFormMaster_OnApplied" OnLoadSuccess="dataFormMaster_OnLoadSuccess" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="ID" Editor="numberbox" FieldName="SALBASE_BASETTS_ID" Format="" ReadOnly="True" Visible="False" Width="180" />
                    <JQTools:JQFormColumn Alignment="left" Caption="員工代碼" Editor="text" FieldName="EMPLOYEE_ID" MaxLength="50" Width="140" ReadOnly="True" Visible="False" NewRow="False" />
                    <JQTools:JQFormColumn Alignment="left" Caption="薪資項目" Editor="text" FieldName="SALARY_ID" MaxLength="50" Width="140" ReadOnly="True" Visible="False" NewRow="False" />

                    <JQTools:JQFormColumn Alignment="left" Caption="員工編號" Editor="inforefval" EditorOptions="title:'員工資訊',panelWidth:500,remoteName:'_HRM_Salary_Normal_SalBaseBasetts.cb_HRM_BASE_BASE',tableName:'cb_HRM_BASE_BASE',columns:[{field:'EMPLOYEE_CODE',title:'員工編號',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'NAME_C',title:'員工姓名',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'NAME_E',title:'英文名字',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'EMPLOYEE_ID',value:'EMPLOYEE_ID'}],whereItems:[],valueField:'EMPLOYEE_CODE',textField:'NAME_C',valueFieldCaption:'員工代碼',textFieldCaption:'員工姓名',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="EMPLOYEE_CODE" Format="" MaxLength="0" Width="180" ReadOnly="False" />
                    <JQTools:JQFormColumn Alignment="left" Caption="薪資項目" Editor="inforefval" FieldName="SALARY_CODE" Format="" Width="180" EditorOptions="title:'薪資項目資訊',panelWidth:500,remoteName:'_HRM_Salary_Normal_SalBaseBasetts.cb_HRM_SALARY_SALCODE_SalaryApproval',tableName:'cb_HRM_SALARY_SALCODE_SalaryApproval',columns:[{field:'SALARY_CODE',title:'薪資代碼',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'SALARY_CNAME',title:'薪資中文名稱',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'SALARY_ENAME',title:'薪資英文名稱',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'SALARY_ID',value:'SALARY_ID'}],whereItems:[],valueField:'SALARY_CODE',textField:'SALARY_CNAME',valueFieldCaption:'薪資代碼',textFieldCaption:'薪資中文名稱',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" ReadOnly="False" />
                    <JQTools:JQFormColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="EFFECT_DATE" Format="yyyy/mm/dd" Width="180" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false" />
                    <JQTools:JQFormColumn Alignment="left" Caption="金額" Editor="numberbox" FieldName="AMT_Decode" Format="" Width="180" EditorOptions="groupSeparator:','" />
                    <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="MEMO" Format="" MaxLength="0" Width="400" EditorOptions="height:75" Span="2" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                <Columns>
                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="SALBASE_BASETTS_ID" RemoteMethod="False" />
                </Columns>
            </JQTools:JQDefault>
            <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EMPLOYEE_ID" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EMPLOYEE_CODE" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EFFECT_DATE" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="SALARY_ID" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="SALARY_CODE" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="AMT_Decode" RangeFrom="0" RangeTo="" RemoteMethod="True" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>

            <%--<JQTools:JQDataGrid ID="dataGridView_OldSetting" RemoteName="_HRM_Salary_Normal_SalBaseBasetts.HRM_SALARY_SALBASE_BASETTS" runat="server" AutoApply="False"
                DataMember="HRM_SALARY_SALBASE_BASETTS" Pagination="False" QueryTitle="查詢"
                Title="舊有薪資資料" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" PageList="" PageSize="2" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Caption="生效日期" Editor="datebox" FieldName="EFFECT_DATE" Format="yyyy/mm/dd" Width="80" />
                    <JQTools:JQGridColumn Caption="薪資代碼" Editor="text" FieldName="SALARY_CODE" Width="60" />
                    <JQTools:JQGridColumn Caption="中文名稱" Editor="text" FieldName="SALARY_CNAME" Width="100" />
                    <JQTools:JQGridColumn Caption="英文名稱" Editor="text" FieldName="SALARY_ENAME" Width="100" />
                    <JQTools:JQGridColumn Caption="金　　額" Editor="numberbox" FieldName="Num" Width="60" />
                    <JQTools:JQGridColumn Caption="備　　註" Editor="text" FieldName="MEMO" Width="200" />
                </Columns>
            </JQTools:JQDataGrid>--%>
        </JQTools:JQDialog>

        <JQTools:JQDialog ID="JQDialog1Log" runat="server" BindingObjectID="dataFormMasterLog" Title="基本薪資資料異動資料記錄" ShowModal="True" EditMode="Dialog" Width="650px">
            <JQTools:JQDataForm ID="dataFormMasterLog" runat="server" DataMember=" " RemoteName=" ">
                <Columns></Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDataGrid ID="dataGridViewLog" data-options="pagination:true,view:commandview" runat="server" RemoteName="_HRM_Salary_Normal_SalBaseBasetts.HRM_SALARY_SALBASE_BASETTS_LOG" DataMember="HRM_SALARY_SALBASE_BASETTS_LOG" AutoApply="False" Pagination="True" EditDialogID=" "
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ReportFileName="" EditMode="Dialog">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="異動日期" Editor="text" FieldName="LOG_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="True" Sortable="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="LOG_USER_NAME" Frozen="True" Sortable="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="LOG_STATE_NAME" Frozen="True" Sortable="True" Width="60" />

                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="DEPT_ID_NAME" Width="100" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EMPLOYEE_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="EFFECT_DATE" Format="" Width="80" Sortable="True" />

                    <JQTools:JQGridColumn Alignment="left" Caption="薪資項目代碼" Editor="text" FieldName="SALARY_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="薪資項目中文" Editor="text" FieldName="SALARY_CNAME" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="薪資項目英文" Editor="text" FieldName="SALARY_ENAME" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="AMT_Decode" Format="" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="MEMO" Format="" MaxLength="0" Width="160" Sortable="True" />

                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                </TooItems>
            </JQTools:JQDataGrid>
        </JQTools:JQDialog>1

        <!-- 匯入對話框內容的 DIV -->
        <div id="Dialog_Import"></div>
        <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog" DialogLeft="" DialogTop="">
            <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" DataMember="HRM_BASE_BASEIO" HorizontalColumnsCount="2" RemoteName="_HRM_Employee_Transfer_Leave.HRM_BASE_BASEIO" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="員工編號" Editor="infocombobox" FieldName="EMPLOYEE_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="生效日期" Editor="infocombobox" FieldName="EFFECT_DATE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="薪資項目" Editor="infocombobox" FieldName="SALARY_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="核定金額" Editor="infocombobox" FieldName="AMT" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="infocombobox" FieldName="MEMO" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />

                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EMPLOYEE_ID" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EFFECT_DATE" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ACTION_TYPE" />
                </Columns>
            </JQTools:JQValidate>
        </JQTools:JQDialog>
    </form>
</body>
</html>
