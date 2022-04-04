<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON_Normal_ContactPerson.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>【標題】</title>

</head>
<body>
    <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
    <script type="text/javascript">
        var dataGridView_ID = "#dataGridView";
        var JQDialog1_ID = '#JQDialog1';

        var dataFormMaster_ID = '#dataFormMaster';
        var dataFormMasterCONTACT_ID_ID = '#dataFormMasterCONTACT_ID';

        var JQDialog1Log_ID = '#JQDialog1Log';
        var dataFormMasterLog_ID = '#dataFormMasterLog';
        var dataGridViewLog_ID = '#dataGridViewLog';
        //=======================================【Ready】=========================================
        $(function () {
            //-----------------------------------備註---------------------------------------------
            (function () {

            })();
            //-------------------------------LogDialog整形------------------------------------
            $(JQDialog1Log_ID).jbDialogPlugin();
            //-------------------------------------------------------------------------------------
        });
        //=========================================================================================
        //---------------------------------------Grid載入完成-------------------------------------
        var dataGridView_OnLoadSuccess = function () {
            var dgid = $(this);
            //第一次載入Grid用
            if (!dgid.data('alreadyFirstLoad') && dgid.data('alreadyFirstLoad', true)) {
                //首先判斷頁面
                var ID = Request.getQueryStringByName("ID");                
                if (ID) $(dgid).data('defaultWhereStr', String.format("CPR.CONTACT_ID='{0}'", ID));               
                else $(dgid).data('defaultWhereStr', String.format("Exists(Select 1 From [dbo].[CON_CENTER_AUTHORITY]Where[CENTER_ID]=[CON].[CENTER_ID]and[USERID]='{0}')", getClientInfo("UserID")));
                queryGrid(dgid);
            }
        }
        //---------------------------------------Grid欄位FormatScript-----------------------------
        var dataGridView_FormatScript = function (value, row, index) {
            var fieldName = this.field;
            switch (fieldName) {
                case 'TRANSLOG':
                    return $('<a>', { href: 'javascript: void(0)', onclick: "dataGridView_CommandTrigger.call(this,'" + fieldName + "')" }).
                            html('異動資料記錄')[0].outerHTML;
                    break;
                default:
                    return '';
                    break;
            }
        }
        //---------------------------------------Grid欄位CommandTrigger---------------------------
        var dataGridView_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
            var RowData = $(dataGridView_ID).datagrid('selectRow', rowIndex).datagrid('getSelected');
            switch (command) {
                case 'TRANSLOG':
                    openForm(JQDialog1Log_ID, RowData, 'viewed', 'Dialog');
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
                case dataFormMasterLog_ID:
                    defaultWhereStr = $.map(['CONTACT_PERSON_ID'], function (obj) {
                        return String.format("CPR.{0}='{1}'", obj, RowData[obj]);
                    }).join(" and ");
                    theGrid = dataGridViewLog_ID;
                    break;
                case dataFormMaster_ID:
                    //單頁模式
                    if (RowData.CONTACT_ID) {
                        if (getEditMode(thisDataForm) == "inserted") {
                            var info = getInfolightOption($(dataFormMasterCONTACT_ID_ID));
                            $.post(getRemoteUrl(info.remoteName, info.tableName), { queryWord: $.toJSONString({ whereString: String.format("CONTACT_ID='{0}'", RowData.CONTACT_ID) }) },
                               function (data) {
                                   var rowsData = $.parseJSON(data);
                                   if (rowsData.length > 0) {
                                       $(dataFormMasterCONTACT_ID_ID).refval('setValue', rowsData[0].CONTACT_ID);
                                       $(dataFormMasterCONTACT_ID_ID).refval('doColumnMatch', rowsData[0]);
                                   } else $(dataFormMasterCONTACT_ID_ID).refval('setValue','');
                               });
                        }
                    }
                    else {
                        //聯絡人的權限綁定
                        $(dataFormMasterCONTACT_ID_ID).refval('setWhere', String.format("Exists(Select 1 From [dbo].[CON_CENTER_AUTHORITY]Where[CENTER_ID]=[CON].[CENTER_ID]and[USERID]='{0}')", getClientInfo("UserID")));
                    }
                    break;
                default:
                    break;
            }
            if (theGrid && defaultWhereStr) $(theGrid).data('defaultWhereStr', defaultWhereStr).datagrid("setWhere", defaultWhereStr);
        }        
        //---------------------------------------Form存檔之後-------------------------------------
        var DataForm_OnApplied = function () {
            $(this).jbDataFormReloadDataGrid();
        }
        //---------------------------------------改寫查詢-----------------------------------------
        function queryGrid(dg) {
            var where = $(dg).datagrid('getWhere');
            var defaultWhereStr = $(dg).data('defaultWhereStr');
            if (defaultWhereStr) where = where ? String.format(" {0} and {1} ", defaultWhereStr, where) : defaultWhereStr;
            $(dg).datagrid('setWhere', where);
        }
        //-----------------------------------------------------------------------------------------
    </script>
    <form id="form1" runat="server">
        <%--<JQTools:JQMultiLanguage ID="JQMultiLanguage1" runat="server" />--%>
        <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="_CON_Normal_ContactPerson.CON_CONTACT_PERSON" runat="server" AutoApply="True"
            DataMember="CON_CONTACT_PERSON" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
            Title="聯絡窗口資訊" QueryLeft="300" QueryTop="100" OnLoadSuccess="dataGridView_OnLoadSuccess" AlwaysClose="True">
            <Columns>
                <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="CONTACT_NAME" Width="100" />
                <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="NAME" Width="100" />                
                <JQTools:JQGridColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="BEGIN_DATE" Width="100" />
                <JQTools:JQGridColumn Alignment="left" Caption="截止日期" Editor="datebox" FieldName="END_DATE" Width="100" />
                <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="MEMO" Width="200" />

                <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Width="80" />
                <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Width="120" />
                <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Width="80" />
                <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Width="120" />

                <JQTools:JQGridColumn Alignment="left" Caption="異動資料記錄" Editor="text" FieldName="TRANSLOG" FormatScript="dataGridView_FormatScript" Width="80" />
            </Columns>
            <TooItems>
                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
            </TooItems>
            <QueryColumns>
                <JQTools:JQQueryColumn FieldName="NAME" Condition="%%" Caption="聯絡窗口姓名" />
            </QueryColumns>
        </JQTools:JQDataGrid>
        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="聯絡窗口資訊">
            <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CON_CONTACT_PERSON" HorizontalColumnsCount="2" RemoteName="_CON_Normal_ContactPerson.CON_CONTACT_PERSON" OnApplied="DataForm_OnApplied" OnLoadSuccess="DataForm_OnLoadSuccess" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡窗口流水號" Editor="numberbox" FieldName="CONTACT_PERSON_ID" Width="180" Visible="False" />

                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="inforefval" FieldName="CONTACT_ID" Width="180" EditorOptions="title:'聯絡人',panelWidth:350,remoteName:'_CON_Normal_ContactPerson.cb_CON_CONTACT',tableName:'cb_CON_CONTACT',columns:[{field:'CENTER_CNAME',title:'中心名稱',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CONTACT_NAME',title:'聯絡人',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CONTACT_ID',textField:'CONTACT_NAME',valueFieldCaption:'聯絡人流水號',textFieldCaption:'姓名',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" />
                    <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="text" FieldName="NAME"  Width="180" />                    
                    <JQTools:JQFormColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="BEGIN_DATE" Width="180" NewRow="true" />
                    <JQTools:JQFormColumn Alignment="left" Caption="截止日期" Editor="datebox" FieldName="END_DATE" Width="180" />
                    <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="MEMO" Width="400" EditorOptions="height:50" Span="2" />

                    <JQTools:JQFormColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Width="180" ReadOnly="True" NewRow="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Width="180" ReadOnly="True" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />
                    <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Width="180" ReadOnly="True" NewRow="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Width="180" ReadOnly="True" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="yyyy/mm/dd HH:mm:SS" />                    
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                <Columns>
                    <JQTools:JQDefaultColumn FieldName="CONTACT_PERSON_ID" DefaultValue="0" />
                    <JQTools:JQDefaultColumn FieldName="CONTACT_ID" DefaultValue="" />
                </Columns>
            </JQTools:JQDefault>
            <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                <Columns>
                    <JQTools:JQValidateColumn FieldName="CONTACT_ID" CheckNull="true" />
                </Columns>
            </JQTools:JQValidate>
        </JQTools:JQDialog>

        <JQTools:JQDialog ID="JQDialog1Log" runat="server" BindingObjectID="dataFormMasterLog" Title="聯絡窗口資訊異動資料記錄" ShowModal="True" EditMode="Dialog" Width="650px">
            <div style="display: none;">
                <JQTools:JQDataForm ID="dataFormMasterLog" runat="server" DataMember="CON_CONTACT_PERSON" RemoteName="_CON_Normal_ContactPerson.CON_CONTACT_PERSON" OnLoadSuccess="DataForm_OnLoadSuccess">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡窗口流水號" Editor="numberbox" FieldName="CONTACT_PERSON_ID" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
            </div>
            <JQTools:JQDataGrid ID="dataGridViewLog" data-options="pagination:true,view:commandview" runat="server" RemoteName="_CON_Normal_ContactPerson.CON_CONTACT_PERSON_LOG" DataMember="CON_CONTACT_PERSON_LOG" AutoApply="False" Pagination="True" EditDialogID=" "
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Window" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ReportFileName="" EditMode="Dialog">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="異動日期" Editor="text" FieldName="LOG_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="True" Sortable="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="LOG_USER_NAME" Frozen="True" Sortable="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="LOG_STATE_NAME" Frozen="True" Sortable="True" Width="60" />

                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="CONTACT_NAME" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="NAME" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="BEGIN_DATE" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="截止日期" Editor="datebox" FieldName="END_DATE" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="MEMO" Width="200" />

                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                </TooItems>
            </JQTools:JQDataGrid>
        </JQTools:JQDialog>
    </form>
</body>
</html>
