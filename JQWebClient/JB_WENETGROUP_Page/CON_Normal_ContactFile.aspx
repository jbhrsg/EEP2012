<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON_Normal_ContactFile.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>檔案上傳</title>

</head>
<body>
    <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
    <script type="text/javascript">
        var dataGridMaster_ID = "#dataGridMaster";
        var JQDialog1_ID = '#JQDialog1';

        var dataFormMaster_ID = '#dataFormMaster';
        var dataFormMasterCONTACT_ID = '#dataFormMasterCONTACT_ID';
        var dataFormMasterCONTACT_CELLPHONE = '#dataFormMasterCONTACT_CELLPHONE';

        var JQDialog1Log_ID = '#JQDialog1Log';
        var dataFormMasterLog_ID = '#dataFormMasterLog';
        var dataGridMasterLog_ID = '#dataGridMasterLog';
        //=======================================【Ready】=========================================
        $(function () {
            //-----------------------------------備註---------------------------------------------
            (function () {

            })();
            //-----------------------------------LogDialog整形------------------------------------
            $(JQDialog1Log_ID).jbDialogPlugin();
        });
        //=========================================================================================
        //---------------------------------------Form載入之後-------------------------------------
        var DataForm_OnLoadSuccess = function (RowData) {
            var defaultWhereStr = '';
            var theGrid = '';

            var thisDataForm = $(this);
            var form_ID = '#' + thisDataForm.attr('id');
            var ID = Request.getQueryStringByName("ID");

            switch (form_ID) {
                case dataFormMasterLog_ID:
                    defaultWhereStr = String.format("CONTACT_FILE_ID='{0}'", RowData.CONTACT_FILE_ID);
                    theGrid = dataGridMasterLog_ID;
                    break;
                case dataFormMaster_ID:
                    $(dataFormMasterCONTACT_CELLPHONE).refval('setWhere', "exists (select CENTER_ID from CON_CENTER_AUTHORITY where CENTER_ID = CENTER.CENTER_ID and USERID = '" + getClientInfo("UserID") + "' ) ");
                    if (ID) {
                        $(dataFormMasterCONTACT_CELLPHONE).refval('setValue', RowData.CONTACT_CELLPHONE);
                        $(dataFormMasterCONTACT_CELLPHONE).refval('disable');
                        if (getEditMode(thisDataForm) == "inserted") {
                            $.post(getRemoteUrl('_CON_SHARECODE.CON_CONTACT', 'CON_CONTACT'),
                               { queryWord: $.toJSONString({ whereString: String.format("CONTACT_ID='{0}'", ID) }) },
                               function (data) {
                                   var rowsData = $.parseJSON(data);
                                   $(dataFormMasterCONTACT_ID).val(ID);
                                   $(dataFormMasterCONTACT_CELLPHONE).refval('setValue', rowsData[0].CONTACT_CELLPHONE)
                               });
                        }
                    }
                    break;
                default:
                    break;
            }
            if (theGrid && defaultWhereStr) $(theGrid).data('defaultWhereStr', defaultWhereStr).datagrid("setWhere", defaultWhereStr);
        }
        //---------------------------------------Form存檔之前-------------------------------------
        var DataForm_OnApply = function () {
            var Ans = false;
            if ($(this).form('validateForm')) {
                var validateFun = '';
                switch ('#' + $(this).attr('id')) {
                    case dataFormMaster_ID: validateFun = 'DataValidate'; break;
                }
                if (validateFun) {
                    var data = $(this).jbDataFormGetAFormData();
                    $.ajaxSetup({ async: false });
                    //檢查資料是否有重複
                    $.post('../handler/JqDataHandle.ashx?RemoteName=_CON_Normal_ContactFile', { mode: 'method', method: validateFun, parameters: $.toJSONString(data) }
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
        //---------------------------------------Form存檔之後-------------------------------------
        var DataForm_OnApplied = function () {
            $(this).jbDataFormReloadDataGrid();
        }
        //---------------------------------------Grid載入完成-------------------------------------
        var dataGridMaster_OnLoadSuccess = function () {
            var dgid = $(this);
            //第一次載入Grid用
            if (!dgid.data('alreadyFirstLoad') && dgid.data('alreadyFirstLoad', true)) {
                //首先判斷頁面
                var ID = Request.getQueryStringByName("ID");
                if (ID) {
                    var defaultWhereStr = String.format("CONTACT.CONTACT_ID='{0}'", ID);
                    $(this).data('defaultWhereStr', defaultWhereStr).datagrid("setWhere", defaultWhereStr);
                    $('#CONTACT_ID_Query').closest("tr").hide();
                }
                else {
                    //一般頁面則進行一般頁面之預先設定                    
                    //var pnid = getInfolightOption($(dgid)).queryDialog;
                    //if (pnid != undefined) {
                    //    clearQuery(dgid);
                    //    setQueryDefault(pnid);
                    //    $(dgid).datagrid('setWhere', $(dgid).datagrid('getWhere'));
                    //}
                    queryGrid(dgid);
                }

            }
        }
        //---------------------------------------Grid欄位FormatScript-----------------------------
        var dataGridMaster_FormatScript = function (value, row, index) {
            var fieldName = this.field;
            switch (fieldName) {
                case 'TRANSLOG':
                    return $('<a>', { href: 'javascript: void(0)', onclick: "dataGridMaster_CommandTrigger.call(this,'" + fieldName + "')" }).
                            html('異動資料記錄')[0].outerHTML;
                    break;
                default:
                    return '';
                    break;
            }
        }
        //---------------------------------------Grid欄位CommandTrigger---------------------------
        var dataGridMaster_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
            var RowData = $(dataGridMaster_ID).datagrid('selectRow', rowIndex).datagrid('getSelected');
            switch (command) {
                case 'TRANSLOG':
                    openForm(JQDialog1Log_ID, RowData, 'viewed', 'Dialog');
                    break;
                default:
                    break;
            }
        }
        //---------------------------------------改寫查詢-----------------------------------------
        function queryGrid(dg) {
            var where = $(dg).datagrid('getWhere');
            var userID = getClientInfo("UserID");
            if (where != "") where = where + "and ";
            where = where + "exists  (select CENTER_ID from CON_CENTER_AUTHORITY where CENTER_ID = CENTER.CENTER_ID and USERID = '" + userID + "' )";
            var defaultWhereStr = $(dg).data('defaultWhereStr');
            if (defaultWhereStr) where = where ? String.format(" {0} and {1} ", defaultWhereStr, where) : defaultWhereStr;
            $(dg).datagrid('setWhere', where);
        }
    </script>
    <form id="form1" runat="server">
        <%--<JQTools:JQMultiLanguage ID="JQMultiLanguage1" runat="server" />--%>
        <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="_CON_Normal_ContactFile.CON_CONTACT_FILE" runat="server" AutoApply="True"
            DataMember="CON_CONTACT_FILE" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
            Title="檔案上傳" QueryLeft="300px" QueryTop="100px" OnLoadSuccess="dataGridMaster_OnLoadSuccess" AlwaysClose="True" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryMode="Window" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
            <Columns>
                <JQTools:JQGridColumn Alignment="left" Caption="上傳檔案流水號" Editor="text" FieldName="CONTACT_FILE_ID" Width="90" MaxLength="4" Visible="False" />
                <JQTools:JQGridColumn Alignment="left" Caption="聯絡人流水號" Editor="text" FieldName="CONTACT_ID" Width="90" MaxLength="4" Visible="False" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="CONTACT_NAME" Width="120" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="聯絡人手機" Editor="text" FieldName="CONTACT_CELLPHONE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                <JQTools:JQGridColumn Alignment="left" Caption="上傳檔案" Editor="text" FieldName="CONTACT_FILE" MaxLength="200" Width="180" Format="Download,Folder:Files/WENETGROUP/CONTACT" Sortable="True" />
                <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                <JQTools:JQGridColumn Alignment="left" Caption="異動資料記錄" Editor="text" FieldName="TRANSLOG" FormatScript="dataGridMaster_FormatScript" Frozen="False" ReadOnly="True" Sortable="False" Visible="True" Width="80" />
            </Columns>
            <TooItems>
                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
            </TooItems>
            <QueryColumns>
                <JQTools:JQQueryColumn AndOr="and" Caption="聯絡人流水號" Condition="%%" DataType="string" Editor="inforefval" FieldName="CONTACT_ID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'_CON_SHARECODE.CON_CONTACT',tableName:'CON_CONTACT',columns:[{field:'CENTER_CNAME',title:'中心名稱',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CONTACT_NAME',title:'聯絡人',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CONTACT_ID',textField:'CONTACT_NAME',valueFieldCaption:'聯絡人',textFieldCaption:'CONTACT_NAME',cacheRelationText:false,checkData:true,showValueAndText:false,selectOnly:false" TableName="A" />
                <JQTools:JQQueryColumn AndOr="and" Caption="檔案" Condition="%%" DataType="string" Editor="text" FieldName="CONTACT_FILE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
            </QueryColumns>
        </JQTools:JQDataGrid>
        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="聯絡檔案" DialogLeft="" DialogTop="">
            <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CON_CONTACT_FILE" HorizontalColumnsCount="2" RemoteName="_CON_Normal_ContactFile.CON_CONTACT_FILE" OnApply="DataForm_OnApply" OnApplied="DataForm_OnApplied" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" OnLoadSuccess="DataForm_OnLoadSuccess">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人檔案流水號" Editor="text" FieldName="CONTACT_FILE_ID" Visible="False" Width="180" MaxLength="4" />
                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="CONTACT_ID" Visible="False" Width="180" MaxLength="4" EditorOptions="" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人手機" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:400,remoteName:'_CON_SHARECODE.CON_CONTACT',tableName:'CON_CONTACT',columns:[{field:'CENTER_CNAME',title:'中心名稱',width:150,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CONTACT_NAME',title:'聯絡人',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CONTACT_CELLPHONE',title:'手機',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'CONTACT_ID',value:'CONTACT_ID'}],whereItems:[],valueField:'CONTACT_CELLPHONE',textField:'CONTACT_NAME',valueFieldCaption:'手機',textFieldCaption:'聯絡人姓名',cacheRelationText:false,checkData:true,showValueAndText:false,selectOnly:false" FieldName="CONTACT_CELLPHONE" MaxLength="0" Visible="True" Width="180" />
                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人檔案" Editor="infofileupload" FieldName="CONTACT_FILE" MaxLength="200" Width="460" Span="2" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'Files/WENETGROUP/CONTACT',showButton:true,showLocalFile:false"  />
                    <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" MaxLength="50" Width="180" ReadOnly="True" NewRow="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Width="180" ReadOnly="True" MaxLength="0" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                    <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" MaxLength="50" Width="180" ReadOnly="True" NewRow="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Width="180" ReadOnly="True" MaxLength="0" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                <Columns>
                    <JQTools:JQDefaultColumn FieldName="CONTACT_FILE_ID" DefaultValue="1" />
                </Columns>
            </JQTools:JQDefault>
            <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn FieldName="CONTACT_ID" CheckNull="true" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_FILE" RemoteMethod="True" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>
        </JQTools:JQDialog>

        <JQTools:JQDialog ID="JQDialog1Log" runat="server" BindingObjectID="dataFormMasterLog" Title="檔案上傳異動資料記錄" ShowModal="True" EditMode="Dialog" Width="650px" DialogLeft="" DialogTop="">
            <div style="display: none;">
                <JQTools:JQDataForm ID="dataFormMasterLog" runat="server" DataMember="CON_CONTACT_FILE" RemoteName="_CON_Normal_ContactFile.CON_CONTACT_FILE" OnLoadSuccess="DataForm_OnLoadSuccess">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="上傳檔案ID" Editor="numberbox" FieldName="CONTACT_FILE_ID" Width="140" />
                    </Columns>
                </JQTools:JQDataForm>
            </div>
            <JQTools:JQDataGrid ID="dataGridMasterLog" data-options="pagination:true,view:commandview" runat="server" RemoteName="_CON_Normal_ContactFile.CON_CONTACT_FILE_LOG" DataMember="CON_CONTACT_FILE_LOG" AutoApply="False" Pagination="True" EditDialogID=" "
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ReportFileName="" EditMode="Dialog">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="LOG_STATE_NAME" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="USERNAME" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="LOG_USER" Frozen="False" MaxLength="50" ReadOnly="False" Sortable="False" Visible="False" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動日期" Editor="datebox" FieldName="LOG_DATE" Frozen="False" MaxLength="8" ReadOnly="False" Sortable="False" Visible="True" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="CONTACT_NAME" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="上傳檔案" Editor="text" FieldName="CONTACT_FILE" MaxLength="200" Width="180" Format="Download,Folder:Files/WENETGROUP/CONTACT" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                </TooItems>
            </JQTools:JQDataGrid>
        </JQTools:JQDialog>
    </form>
</body>
</html>
