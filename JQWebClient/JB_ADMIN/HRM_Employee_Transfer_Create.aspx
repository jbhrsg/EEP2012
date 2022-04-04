<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_Employee_Transfer_Create.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>員工新進作業</title>
</head>
<body>
    <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
    <script type="text/javascript">
        var DataGrid_BaseIO_ID = '#DataGrid_BaseIO';
        var Dialog_BaseIO_ID = '#Dialog_BaseIO';

        var DataForm_BaseIO_ID = '#DataForm_BaseIO';
        var DataForm_BaseIOEMPLOYEE_CODE_ID = DataForm_BaseIO_ID + 'EMPLOYEE_CODE';
        var DataForm_BaseIOEMPLOYEE_ID_ID = DataForm_BaseIO_ID + 'EMPLOYEE_ID';
        var DataGrid_BaseHistory_ID = '#DataGrid_BaseHistory';

        var Dialog_BaseIOLog_ID = '#Dialog_BaseIOLog';
        var DataForm_BaseIOLog_ID = '#DataForm_BaseIOLog';
        var DataGrid_BaseIOLog_ID = '#DataGrid_BaseIOLog';

        var Dialog_Management_ID = '#Dialog_Management';
        var DataForm_Management_ID = '#DataForm_Management';
        var Iframe_Management_ID = '#Iframe_Management';

        var Dialog_BaseCreate_ID = '#Dialog_BaseCreate';
        //=======================================【Ready】=========================================
        $(function () {
            //-------------------------------重新刷新-----------------------------------------
            $(Dialog_BaseCreate_ID).dialog({ onClose: function () { $(DataGrid_BaseIO_ID).datagrid('reload'); } });
            //-----------------------------------打字觸發GV---------------------------------------
            $(DataForm_BaseIOEMPLOYEE_CODE_ID).data('inforefval').refval.find('input.refval-text').blur(function () {
                EmployeeDataRp();
            });
            //-----------------------------------LogDialog整形------------------------------------
            $(Dialog_BaseIOLog_ID).jbDialogPlugin();
            $(Dialog_Management_ID).jbDialogPlugin();
            $(Dialog_BaseCreate_ID).jbDialogPlugin();
            //-------------------------------------------------------------------------------------
        });
        //=========================================================================================
        //---------------------------------------Grid載入完成-------------------------------------
        var DataGrid_BaseIO_OnLoadSuccess = function () {
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
                        msg: '載入本月份到職人員',
                        showType: 'show'
                    });
                }
            }
        }
        //---------------------------------------Grid欄位FormatScript-----------------------------
        var DataGrid_BaseIO_FormatScript = function (value, row, index) {
            var fieldName = this.field;
            switch (fieldName) {
                case 'TRANSLOG':
                    return $('<a>', { href: 'javascript: void(0)', onclick: "DataGrid_BaseIO_CommandTrigger.call(this,'DataLog')" }).html('異動資料記錄')[0].outerHTML;
                    break;
                case 'ToolBar':
                    return $('<a>', { href: 'javascript:void(0)', title: '個人資料維護', onclick: "DataGrid_BaseIO_CommandTrigger.call(this,'Base')" }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '個人資料維護' })[0].outerHTML
                    + $('<a>', { href: 'javascript:void(0)', title: '出勤班表設定', onclick: "DataGrid_BaseIO_CommandTrigger.call(this,'Attend')" }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '出勤班表設定' })[0].outerHTML
                    + $('<a>', { href: 'javascript:void(0)', title: '薪資群組設定', onclick: "DataGrid_BaseIO_CommandTrigger.call(this,'SalaryGroup')" }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '薪資群組設定' })[0].outerHTML
                    + $('<a>', { href: 'javascript:void(0)', title: '核定薪資作業', onclick: "DataGrid_BaseIO_CommandTrigger.call(this,'SalaryBase')" }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '核定薪資作業' })[0].outerHTML
                    + $('<a>', { href: 'javascript:void(0)', title: '勞健保投保', onclick: "DataGrid_BaseIO_CommandTrigger.call(this,'Insurance')" }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '勞健保投保' })[0].outerHTML;
                    break;
                default:
                    return '';
                    break;
            }
        }
        //---------------------------------------Grid欄位CommandTrigger---------------------------
        var DataGrid_BaseIO_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
            var RowData = $(DataGrid_BaseIO_ID).datagrid('selectRow', rowIndex).datagrid('getSelected');
            switch (command) {
                case 'DataLog':
                    openForm(Dialog_BaseIOLog_ID, RowData, 'viewed', 'Dialog');
                    break;
                case 'Base':
                case 'Attend':
                case 'SalaryGroup':
                case 'SalaryBase':
                case 'Insurance':
                    $(Dialog_Management_ID).dialog({ title: $(this).attr('title') });  //設定Title                    
                    openForm(Dialog_Management_ID, { EMPLOYEE_ID: RowData.EMPLOYEE_ID, PageName: command }, 'viewed', 'Dialog');
                    break;
                default:
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
                case DataForm_BaseIOLog_ID:
                    defaultWhereStr = String.format("BIO.BASEIO_ID='{0}'", RowData.BASEIO_ID);
                    theGrid = DataGrid_BaseIOLog_ID;
                    break;
                case DataForm_BaseIO_ID:
                    EmployeeDataRp();//下方資料
                    break;
                case DataForm_Management_ID:
                    var src = '';
                    switch (RowData.PageName) {
                        case 'Base': src = 'HRM_Employee_Management.aspx?' + $.param({ 'ID': RowData.EMPLOYEE_ID }); break;
                        case 'Attend': src = 'HRM_Attend_Normal_Basetts.aspx?' + $.param({ 'ID': RowData.EMPLOYEE_ID }); break;
                        case 'SalaryGroup': src = 'HRM_Salary_Normal_SalaryBasetts.aspx?' + $.param({ 'ID': RowData.EMPLOYEE_ID }); break;
                        case 'SalaryBase': src = 'HRM_Salary_Normal_SalBaseAudit.aspx?' + $.param({ 'ID': RowData.EMPLOYEE_ID }); break;
                        case 'Insurance': src = 'HRM_Insurance_Normal_EmployeeInsurance.aspx?' + $.param({ 'ID': RowData.EMPLOYEE_ID }); break;
                        default: src = 'about:blank'; break;
                    }
                    $(Iframe_Management_ID).attr('src', src);
                    break;
                default:
                    break;
            }
            if (theGrid && defaultWhereStr) $(theGrid).data('defaultWhereStr', defaultWhereStr).datagrid("setWhere", defaultWhereStr);
        }
        //---------------------------------------Form存檔之前-------------------------------------
        var DataForm_OnApply = function () {
            var Ans = false;
            var thisDataForm = $(this);
            if (thisDataForm.form('validateForm')) {
                var validateFun = '';
                switch ('#' + thisDataForm.attr('id')) {
                    case DataForm_BaseIO_ID:
                        if (getEditMode(thisDataForm) == "inserted") validateFun = 'DataValidate_Create';
                        else if (getEditMode(thisDataForm) == "updated") validateFun = 'DataValidate_Update';
                        break;
                }
                if (validateFun) {
                    var data = thisDataForm.jbDataFormGetAFormData();
                    $.ajaxSetup({ async: false });
                    //檢查資料是否有重複
                    $.post('../handler/JqDataHandle.ashx?RemoteName=_HRM_Employee_Transfer_Create', { mode: 'method', method: validateFun, parameters: $.toJSONString(data) }
                          ).done(function (data) {
                              var Json = $.parseJSON(data);
                              if (Json.IsOK) Ans = true;
                              else alert(Json.ErrorMsg);
                          }).fail(function (xhr, textStatus, errorThrown) {
                              alert('error');
                          });
                    $.ajaxSetup({ async: true });
                }
            }
            return Ans;
        }
        //---------------------------------------DataForm存檔後-----------------------------------
        var DataForm_OnApplied = function () {
            $(this).jbDataFormReloadDataGrid(); //DataGrid刷新
        }
        //---------------------------------------DataGrid刪除前-----------------------------------
        var DataGrid_OnDelete = function (rowData) {
            var data = { BASEIO_ID: rowData.BASEIO_ID }
            $.ajaxSetup({ async: false });
            var Ans = false;
            //●刪除的時候去檢查有沒有資料了
            $.post('../handler/JqDataHandle.ashx?RemoteName=_HRM_Employee_Transfer_Create', { mode: 'method', method: 'DataValidate_Delete', parameters: $.toJSONString(data) }
                  ).done(function (data) {
                      var Json = $.parseJSON(data);
                      if (Json.IsOK) Ans = true;
                      else alert(Json.ErrorMsg);
                  }).fail(function (xhr, textStatus, errorThrown) {
                      alert('error');
                  });
            return Ans;
        }
        //---------------------------------------DataGrid刪除後-----------------------------------
        var DataGrid_OnDeleted = function (rowData) {
            $(this).datagrid('reload');
            $(DataGrid_BaseIO_ID).datagrid('reload');
        }
        //---------------------------------------選人觸發GV---------------------------------------
        var EmployeeCodeOnSelect = function (rowdata) {
            EmployeeDataRp();
        }
        //---------------------------------------選人帶入下方資料---------------------------------
        var EmployeeDataRp = function () {
            var EmployeeID = $(DataForm_BaseIOEMPLOYEE_ID_ID).val()
            if (EmployeeID) $(DataGrid_BaseHistory_ID).datagrid("setWhere", String.format("EMPLOYEE_ID='{0}'", EmployeeID));
            else $(DataGrid_BaseHistory_ID).datagrid("setWhere", '1=0');
        }
        //---------------------------------------員工新增-----------------------------------------
        var DataGrid_BaseIO_OnInsert = function () {
            openForm(Dialog_BaseCreate_ID, {}, 'viewed', 'dialog');
        }
        //-----------------------------------------------------------------------------------------
    </script>
    <form id="form1" runat="server">
        <%--<JQTools:JQMultiLanguage ID="JQMultiLanguage1" runat="server" />--%>

        <JQTools:JQDataGrid ID="DataGrid_BaseIO" data-options="pagination:true,view:commandview" RemoteName="_HRM_Employee_Transfer_Create.HRM_BASE_BASEIO" runat="server" AutoApply="True"
            DataMember="HRM_BASE_BASEIO" Pagination="True" QueryTitle="查詢" EditDialogID="Dialog_BaseIO"
            Title="員工到職資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDelete="DataGrid_OnDelete" OnLoadSuccess="DataGrid_BaseIO_OnLoadSuccess">
            <Columns>
                <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EMPLOYEE_CODE" MaxLength="0" Width="80" Frozen="True" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" Frozen="True" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                <JQTools:JQGridColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="EFFECT_DATE" Width="80" Format="yyyy/mm/dd" Frozen="True" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="快捷功能" Editor="text" FormatScript="DataGrid_BaseIO_FormatScript" FieldName="ToolBar" Width="700" />
                <JQTools:JQGridColumn Alignment="left" Caption="錄取管道" Editor="text" FieldName="HIRE_WAY_ID_NAME" MaxLength="50" Width="160" EditorOptions="" ReadOnly="True" Visible="True" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />

                <JQTools:JQGridColumn Alignment="left" Caption="異動資料記錄" Editor="text" FieldName="TRANSLOG" FormatScript="DataGrid_BaseIO_FormatScript" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="True" Width="80" />
            </Columns>
            <TooItems>
                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="DataGrid_BaseIO_OnInsert" Text="員工新進" />
                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增到職資料" />
                <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
            </TooItems>
            <QueryColumns>
                <JQTools:JQQueryColumn AndOr="and" Caption="員工編號" Condition="%%" DataType="string" Editor="text" FieldName="EMPLOYEE_CODE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="員工姓名" Condition="%%" DataType="string" Editor="text" FieldName="NAME_C" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                <JQTools:JQQueryColumn AndOr="and" Caption="生效日期" Condition=">=" DataType="datetime" Editor="datebox" FieldName="EFFECT_DATE" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" DefaultValue="_firstday" />
                <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="<=" DataType="datetime" Editor="datebox" FieldName="EFFECT_DATE" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" DefaultValue="_lastday" />
            </QueryColumns>
        </JQTools:JQDataGrid>

        <JQTools:JQDialog ID="Dialog_BaseIO" runat="server" BindingObjectID="DataForm_BaseIO" Title="到職資料" ShowModal="True" DialogTop="" DialogLeft="">
            <JQTools:JQDataForm ID="DataForm_BaseIO" runat="server" DataMember="HRM_BASE_BASEIO" HorizontalColumnsCount="2" RemoteName="_HRM_Employee_Transfer_Create.HRM_BASE_BASEIO" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" OnLoadSuccess="DataForm_OnLoadSuccess" OnApply="DataForm_OnApply" OnApplied="DataForm_OnApplied" IsAutoPause="False">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="BASEIO_ID" Editor="text" FieldName="BASEIO_ID" Width="180" ReadOnly="True" Visible="False" />
                    <JQTools:JQFormColumn Alignment="left" Caption="類別" Editor="text" FieldName="ACTION_TYPE" Width="180" ReadOnly="True" Visible="False" />
                    <JQTools:JQFormColumn Alignment="left" Caption="員工代碼" Editor="text" FieldName="EMPLOYEE_ID" MaxLength="50" Width="180" ReadOnly="True" Visible="False" />
                    <JQTools:JQFormColumn Alignment="left" Caption="員工編號" Editor="inforefval" FieldName="EMPLOYEE_CODE" MaxLength="0" Width="180" EditorOptions="title:'人員資料',panelWidth:500,remoteName:'_HRM_Employee_Transfer_Create.cb_HRM_BASE_BASE',tableName:'cb_HRM_BASE_BASE',columns:[{field:'EMPLOYEE_CODE',title:'員工編號',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'NAME_C',title:'員工姓名',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'EMPLOYEE_ID',value:'EMPLOYEE_ID'}],whereItems:[],valueField:'EMPLOYEE_CODE',textField:'NAME_C',valueFieldCaption:'員工編號',textFieldCaption:'員工姓名',cacheRelationText:true,checkData:true,showValueAndText:false,onSelect:EmployeeCodeOnSelect,selectOnly:false" />
                    <JQTools:JQFormColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="EFFECT_DATE" Format="yyyy/mm/dd" Width="180" />
                    <JQTools:JQFormColumn Alignment="left" Caption="錄取管道" Editor="infocombobox" FieldName="HIRE_WAY_ID" EditorOptions="valueField:'HIRE_WAY_ID',textField:'HIRE_WAY_CNAME',remoteName:'_HRM_Employee_Transfer_Create.cb_HRM_HIRE_WAY',tableName:'cb_HRM_HIRE_WAY',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" MaxLength="50" Width="400" NewRow="True" Span="2" />
                    <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" EditorOptions="height:50" FieldName="MEMO" MaxLength="100" Width="400" NewRow="True" RowSpan="1" Span="2" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDefault ID="Default_BaseIO" runat="server" BindingObjectID="DataForm_BaseIO" EnableTheming="True">
                <Columns>
                    <JQTools:JQDefaultColumn DefaultValue="0" FieldName="BASEIO_ID" RemoteMethod="True" />
                    <JQTools:JQDefaultColumn DefaultValue="1" FieldName="ACTION_TYPE" RemoteMethod="True" />
                    <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="EFFECT_DATE" />
                </Columns>
            </JQTools:JQDefault>
            <JQTools:JQValidate ID="Validate_BaseIO" runat="server" BindingObjectID="DataForm_BaseIO" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="BASEIO_ID" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EMPLOYEE_CODE" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EFFECT_DATE" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ACTION_TYPE" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>

            <JQTools:JQDataGrid ID="DataGrid_BaseHistory" data-options="pagination:true,view:commandview" RemoteName="_HRM_Employee_Transfer_Create.HRM_BASE_BASE_History" runat="server" AutoApply="True"
                DataMember="HRM_BASE_BASE_History" Pagination="True" QueryTitle="查詢" EditDialogID=""
                Title="員工進出異動" AllowAdd="False" AllowDelete="True" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="True" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Height="200" OnDelete="DataGrid_OnDelete" OnDeleted="DataGrid_OnDeleted">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="起始時間" Editor="datebox" FieldName="SDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="迄時時間" Editor="datebox" FieldName="EDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="TypeActionName" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                </Columns>
            </JQTools:JQDataGrid>
        </JQTools:JQDialog>

        <JQTools:JQDialog ID="Dialog_BaseIOLog" runat="server" BindingObjectID="DataForm_BaseIOLog" Title="到職異動資料記錄" ShowModal="True" Width="600px" DialogTop="" DialogLeft="">
            <div style="display: none;">
                <JQTools:JQDataForm ID="DataForm_BaseIOLog" runat="server" DataMember="HRM_BASE_BASEIO" RemoteName="_HRM_Employee_Transfer_Create.HRM_BASE_BASEIO" OnLoadSuccess="DataForm_OnLoadSuccess">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="BASEIO_ID" Editor="text" FieldName="BASEIO_ID" Width="180" ReadOnly="True" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
            </div>
            <JQTools:JQDataGrid ID="DataGrid_BaseIOLog" data-options="pagination:true,view:commandview" RemoteName="_HRM_Employee_Transfer_Create.HRM_BASE_BASEIO_LOG" runat="server" AutoApply="False"
                DataMember="HRM_BASE_BASEIO_LOG" Pagination="True" QueryTitle="查詢" EditDialogID="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Title="">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="異動日期" Editor="text" FieldName="LOG_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="True" Sortable="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="LOG_USER_NAME" Frozen="True" Sortable="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="LOG_STATE_NAME" Frozen="True" Sortable="True" Width="60" />

                    <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EMPLOYEE_CODE" MaxLength="0" Width="80" Frozen="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="EFFECT_DATE" Width="80" Format="yyyy/mm/dd" Frozen="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="錄取管道" Editor="text" FieldName="HIRE_WAY_ID_NAME" MaxLength="50" Width="160" EditorOptions="" ReadOnly="True" Visible="True" Sortable="True" />

                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />

                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                </TooItems>
            </JQTools:JQDataGrid>
        </JQTools:JQDialog>

        <JQTools:JQDialog ID="Dialog_Management" runat="server" BindingObjectID="DataForm_Management" Title="" ShowModal="True" EditMode="Dialog" ShowSubmitDiv="false" DialogLeft="" DialogTop="0" Width="1060">
            <div style="display: none;">
                <JQTools:JQDataForm ID="DataForm_Management" runat="server" DataMember="HRM_BASE_BASEIO" RemoteName="_HRM_Employee_Transfer_Create.HRM_BASE_BASEIO" OnLoadSuccess="DataForm_OnLoadSuccess">
                    <Columns>
                        <JQTools:JQFormColumn Caption="EMPLOYEE_ID" Editor="text" FieldName="EMPLOYEE_ID" Width="180" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Caption="PageName" Editor="text" FieldName="PageName" Width="180" ReadOnly="True" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
            </div>
            <iframe id="Iframe_Management" src="about:blank" width="1050" height="500" style="border: 0;"></iframe>
        </JQTools:JQDialog>

        <JQTools:JQDialog ID="Dialog_BaseCreate" runat="server" Title="員工基本資料" DialogTop="" ShowModal="True" EditMode="Dialog" DialogLeft="" ShowSubmitDiv="False" Width="720px">
            <iframe src="HRM_Employee_Edit.aspx" width="700" height="350" style="border: 0;"></iframe>
        </JQTools:JQDialog>
    </form>
</body>
</html>
