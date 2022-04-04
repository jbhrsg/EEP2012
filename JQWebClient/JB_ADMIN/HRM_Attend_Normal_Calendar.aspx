<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_Attend_Normal_Calendar.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>行事曆排定</title>
</head>
<body>
    <form id="form1" runat="server">
        <JQTools:JQMultiLanguage ID="JQMultiLanguage1" runat="server" />
        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
        <script type="text/javascript">
            var Schedule_Main_ID = '#Schedule_Main';
            var Dialog_Main_ID = '#Dialog_Main';

            //=======================================【Ready】=========================================
            $(function () {
            });
            //=========================================================================================
            //--------------------------------------行事曆類別選擇刷新-------------------------------
            var DataForm_CalendarType_OnSelect = function (rowData) {
                //行事曆篩選條件弄進去，然後重新Load
                $(Schedule_Main_ID).data('theQueryKey', rowData.CALENDAR_ID).schedule('load');
            }
            //--------------------------------------行事曆項目Css修改--------------------------------
            var Schedule_Main_OnItemFormating = function (e) {
                var color = '#000000';
                if (e && e.item && e.item.HOLIDAY_TYPE_ID_COLOR) color = e.item.HOLIDAY_TYPE_ID_COLOR;
                e.itemClass = 'schedule-period-middle';                             //加上這個讓他不能拖曳
                e.itemCss = { 'background-color': color, 'border-radius': 15 };     //css修正
            }
            //--------------------------------------行事曆載入前-------------------------------------
            var Schedule_Main_OnBeforeLoad = function () {
                var schedule = $(this);
                $('.schedule-menu .icon-month', schedule).closest('a').remove();    //移除不要選項
                $('.schedule-menu .icon-week', schedule).closest('a').remove();     //移除不要選項

                var theQueryKey = schedule.data('theQueryKey');                     //行事曆篩選條件載入
                theQueryKey = theQueryKey ? theQueryKey : '';

                var options = schedule.data('options');                             //加入QueryString
                options.queryWord.whereString += String.format("and ACH.[CALENDAR_ID]='{0}'", theQueryKey);
                schedule.data('options', options);

                if (!theQueryKey) return;

                $('.schedule-date-row>.schedule-cell', schedule).each(function () { //加入新增按鈕
                    var cell = $(this);//cell.data('date')

                    var text = cell.html();
                    var btnCreate = $('<a>').
                                    linkbutton({ iconCls: 'icon-add', plain: true, }).
                                    on('click', function () {
                                        ScheduleCellCreate.call(cell, theQueryKey);
                                    });

                    cell.empty().append(btnCreate).append($('<span>').html(text));
                });

                $('.schedule-row>.schedule-cell', schedule).droppable('disable');   //關閉事件
            }
            //--------------------------------------新增按鈕事件-------------------------------------
            var ScheduleCellCreate = function (theQueryKey) {
                var cell = $(this);
                if (cell.data('date') == undefined) return;

                //準備新增之預設值
                var aData = {
                    CALENDAR_ID: theQueryKey,
                    CALENDAR_HOLIDAY_DATE: $.jbjob.Date.DateFormat(cell.data('date'), 'yyyy-MM-dd')
                };

                //開啟
                openForm(Dialog_Main_ID, aData, "inserted", 'dialog');
            }
            //--------------------------------------新增後續動作-------------------------------------
            var DataForm_Main_OnApplied = function (rows) {
                $(Schedule_Main_ID).schedule('load');//刷新
            }
            //--------------------------------------輸入檢查-----------------------------------------
            var DataForm_Main_OnApply = function () {
                var Ans = false;
                if ($(this).form('validateForm')) {
                    var data = $(this).jbDataFormGetAFormData();
                    $.ajaxSetup({ async: false });
                    //●檢查是不是有重複輸入相同的
                    $.post('../handler/JqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_Calendar', { mode: 'method', method: 'DataValidate', parameters: $.toJSONString(data) }
                      ).done(function (data) {
                          var Json = $.parseJSON(data);
                          if (Json.IsOK) Ans = true;
                          else alert(Json.ErrorMsg);
                      }).fail(function (xhr, textStatus, errorThrown) {
                          alert('error');
                      });
                }
                return Ans;
            }
            //-----------------------------------------------------------------------------------------
        </script>

        <JQTools:JQDataForm ID="DataForm_Calendar" runat="server" DataMember=" " HorizontalColumnsCount="1" RemoteName=" " ValidateStyle="Dialog" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" IsAutoPause="False">
            <Columns>
                <JQTools:JQFormColumn Alignment="left" Caption="行事曆" Editor="infocombobox" FieldName="Type" Width="180" EditorOptions="valueField:'CALENDAR_ID',textField:'CALENDAR_CNAME',remoteName:'_HRM_Attend_Normal_Calendar.cb_HRM_ATTEND_CALENDAR',tableName:'cb_HRM_ATTEND_CALENDAR',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:DataForm_CalendarType_OnSelect,panelHeight:200" />                
            </Columns>
        </JQTools:JQDataForm>

        <JQTools:JQSchedule ID="Schedule_Main" runat="server" DateField="CALENDAR_HOLIDAY_DATE" DateToField="" RemoteName="_HRM_Attend_Normal_Calendar.HRM_ATTEND_CALENDAR_HOLIDAY" TimeFromField="" TimeToField="" TipField="" TitleField="HOLIDAY_TYPE_ID_NAME" OnItemFormating="Schedule_Main_OnItemFormating" AllowUpdate="True" OnBeforeLoad="Schedule_Main_OnBeforeLoad"></JQTools:JQSchedule>


        <div style="display: none;">
            <JQTools:JQDataGrid ID="DataGrid_Main" data-options="pagination:true,view:commandview" RemoteName="_HRM_Attend_Normal_Calendar.HRM_ATTEND_CALENDAR_HOLIDAY" runat="server" AutoApply="True"
                DataMember="HRM_ATTEND_CALENDAR_HOLIDAY" Pagination="False" QueryTitle="Query" EditDialogID="Dialog_Main"
                Title="行事曆" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" ViewCommandVisible="False" ColumnsHibeable="True" Height="200" AllowAdd="True">
            </JQTools:JQDataGrid>
        </div>


        <JQTools:JQDialog ID="Dialog_Main" runat="server" BindingObjectID="DataForm_Main" Title="行事曆" EditMode="Dialog" ShowModal="true" Width="300px">
            <JQTools:JQDataForm ID="DataForm_Main" runat="server" DataMember="HRM_ATTEND_CALENDAR_HOLIDAY" HorizontalColumnsCount="1" RemoteName="_HRM_Attend_Normal_Calendar.HRM_ATTEND_CALENDAR_HOLIDAY" ValidateStyle="Dialog" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" OnApplied="DataForm_Main_OnApplied" OnApply="DataForm_Main_OnApply">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="行事曆流水號" Editor="numberbox" FieldName="CALENDAR_HOLIDAY_ID" Format="" Width="180" ReadOnly="true" Visible="false" />
                    <JQTools:JQFormColumn Alignment="left" Caption="行事曆" Editor="infocombobox" FieldName="CALENDAR_ID" Format="" Width="180" EditorOptions="valueField:'CALENDAR_ID',textField:'CALENDAR_CNAME',remoteName:'_HRM_Attend_Normal_Calendar.cb_HRM_ATTEND_CALENDAR',tableName:'cb_HRM_ATTEND_CALENDAR',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" ReadOnly="true" />
                    <JQTools:JQFormColumn Alignment="left" Caption="日期" Editor="datebox" FieldName="CALENDAR_HOLIDAY_DATE" Format="yyyy/mm/dd" Width="180" ReadOnly="true" />
                    <JQTools:JQFormColumn Alignment="left" Caption="假日類別" Editor="infocombobox" FieldName="HOLIDAY_TYPE_ID" Format="" Width="180" EditorOptions="valueField:'HOLIDAY_TYPE_ID',textField:'HOLIDAY_TYPE_CNAME',remoteName:'_HRM_Attend_Normal_Calendar.cb_HRM_ATTEND_HOLIDAY_TYPE',tableName:'cb_HRM_ATTEND_HOLIDAY_TYPE',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDefault ID="Default_Main" runat="server" BindingObjectID="DataForm_Main" EnableTheming="True">
                <Columns>
                    <JQTools:JQDefaultColumn CarryOn="False" FieldName="CALENDAR_HOLIDAY_ID" DefaultValue="0" />
                </Columns>
            </JQTools:JQDefault>
            <JQTools:JQValidate ID="Validate_Main" runat="server" BindingObjectID="DataForm_Main" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="HOLIDAY_TYPE_ID" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CALENDAR_ID" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CALENDAR_HOLIDAY_DATE" RemoteMethod="True" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>
        </JQTools:JQDialog>

    </form>
</body>


</html>
