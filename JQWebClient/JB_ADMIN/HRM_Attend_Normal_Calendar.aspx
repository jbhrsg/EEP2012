<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_Attend_Normal_Calendar.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="IE=edge" />
    <link href="../css/JBHRIS/Dialog.css" rel="stylesheet" />
    <link href="../css/JBHRIS/ReportPage.css" rel="stylesheet" />
    <title>行事曆排定</title>
</head>
<body>
    <form id="form1" runat="server">
        <JQTools:JQMultiLanguage ID="JQMultiLanguage1" runat="server" />
        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
        <script type="text/javascript">
            var Schedule_Main_ID = '#Schedule_Main';
            var Dialog_Main_ID = '#Dialog_Main';
            var Dialog_openCreatHoliday_ID = '#Dialog_openCreatHoliday';
            var Dialog_copyCreatHoliday = '#Dialog_copyCreatHoliday';
            //=======================================【Ready】=========================================
            $(function () {
                var date = new Date();
                var beginYear = (date.getFullYear() + 1).toString();
                
                //---------------------------------------自動產生假日----------------------------------------
                $("#openCreatHoliday").click(function () {
                    $(Dialog_openCreatHoliday_ID).dialog("open");
                    $("#CalendarType").combobox('setValue', $('#DataForm_CalendarType').combobox('getValue'));
                    $('#Input_CreateYear').val(beginYear);
                    $('#JQAttendDateBegin').datebox('setValue', beginYear + '/01/01');
                    $('#JQAttendDateEnd').datebox('setValue', beginYear + '/12/31');
                });

                $("#Dialog_openCreatHoliday").dialog(
             {
                 height: 350,
                 width: 600,
                 resizable: false,
                 modal: true,
                 title: "自動產生假日",
                 closed: true,
                 buttons: [{
                     text: '離開',
                     handler: function () { $("#Dialog_openCreatHoliday").dialog("close") }
                 },
                 {
                     text: '產生',
                     handler: function () {
                         var flag = false;
                         var createYear = $('#Input_CreateYear').val();
                         var calendarType = $('#CalendarType').combobox('getValue');
                         var weekType = $('#JQWeekType').combobox('getValue');
                         var holidayType = $('#JQHolidayType').combobox('getValue');
                         var attendDateBegin = $('#JQAttendDateBegin').datebox('getValue');
                         var attendDateEnd = $('#JQAttendDateEnd').datebox('getValue');

                         if (createYear == "")
                             alert("請輸入年度");
                         else if ($('#CalendarType').combobox('getValue') == "")
                             alert("請選擇行事曆");
                         else if ($('#JQWeekType').combobox('getValue') == "" && $('#JQHolidayType').combobox('getValue') != "")
                             alert("請選擇星期");
                         //else if ($('#JQHolidayType').combobox('getValue') == "" && $('#JQWeekType').combobox('getValue') != "")
                         //    alert("請選擇假日類別");
                         else
                             flag = true;

                         if (flag) {
                             $.ajax({
                                 type: "POST",
                                 url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_Calendar.HRM_ATTEND_CALENDAR_HOLIDAY', //連接的Server端，command
                                 data: "mode=method&method=" + "CreatHoliday" + "&parameters=" + createYear + "," + calendarType + "," + weekType + "," + holidayType + "," + attendDateBegin + "," + attendDateEnd, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                                 cache: false,
                                 async: false,
                                 success: function (data) {
                                     alert(data);
                                     //alert("複製完成");

                                 },
                                 beforeSend: function () { $.messager.progress({ msg: '執行中...' }); },
                                 complete: function () { $.messager.progress('close'); },
                                 error: function (xhr, ajaxOptions, thrownError) { alert('error'); }
                             });
                         }
                     }
                 }]
             });

                $("#copyCreatHoliday").click(function () {
                    $(Dialog_copyCreatHoliday).dialog("open");
                    $("#fromCalendarType").combobox('setValue', $('#DataForm_CalendarType').combobox('getValue'));
                    $('#Input_CopyYear').val(beginYear);
                });


                $("#Dialog_copyCreatHoliday").dialog(
             {
                 height: 350,
                 width: 600,
                 resizable: false,
                 modal: true,
                 title: "複製行事曆",
                 closed: true,
                 buttons: [{
                     text: '離開',
                     handler: function () { $("#Dialog_copyCreatHoliday").dialog("close") }
                 },
                 {
                     text: '產生',
                     handler: function () {
                         var flag = false;
                         var CopyYear = $('#Input_CopyYear').val();
                         var fromCalendarType = $('#fromCalendarType').combobox('getValue');
                         var toCalendarType = $('#toCalendarType').combobox('getValue');

                         if (CopyYear == "")
                             alert("請輸入年度");
                         else if ($('#fromCalendarType').combobox('getValue') == "")
                             alert("請選擇來源行事曆");
                         else if ($('#toCalendarType').combobox('getValue') == "")
                             alert("請選擇目的行事曆");
                         else if (fromCalendarType == toCalendarType)
                             alert("來源行事曆和目的行事曆一樣，請再次選擇");
                         else
                             flag = true;

                         if (flag) {
                             $.ajax({
                                 type: "POST",
                                 url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_Calendar.HRM_ATTEND_CALENDAR_HOLIDAY', //連接的Server端，command
                                 data: "mode=method&method=" + "CopyHoliday" + "&parameters=" + CopyYear + "," + fromCalendarType + "," + toCalendarType, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                                 cache: false,
                                 async: false,
                                 success: function (data) {
                                     alert(data);
                                     //alert("複製完成");

                                 },
                                 beforeSend: function () { $.messager.progress({ msg: '執行中...' }); },
                                 complete: function () { $.messager.progress('close'); },
                                 error: function (xhr, ajaxOptions, thrownError) { alert('error'); }
                             });
                         }
                     }
                 }]
             });


            });
            //=========================================================================================
            //--------------------------------------行事曆類別選擇刷新-------------------------------
            var DataForm_CalendarType_OnSelect = function (rowData) {
                //行事曆篩選條件弄進去，然後重新Load
                $(Schedule_Main_ID).data('theQueryKey', rowData.CALENDAR_ID).schedule('load');
            }
            //--------------------------------------行事曆項目Css修改--------------------------------
            var Schedule_Main_OnItemFormating = function (e) {
                //var color = '#000000';
                //if (e && e.item && e.item.HOLIDAY_TYPE_ID_COLOR) color = e.item.HOLIDAY_TYPE_ID_COLOR;
                //e.itemClass = 'schedule-period-middle';                             //加上這個讓他不能拖曳
                //e.itemCss = { 'background-color': color, 'border-radius': 15 };     //css修正
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



        </script>

        <JQTools:JQDataForm ID="DataForm_Calendar" runat="server" DataMember=" " HorizontalColumnsCount="1" RemoteName=" " ValidateStyle="Dialog" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" IsAutoPause="False">
            <Columns>
                <JQTools:JQFormColumn Alignment="left" Caption="行事曆" Editor="infocombobox" FieldName="Type" Width="180" EditorOptions="valueField:'CALENDAR_ID',textField:'CALENDAR_CNAME',remoteName:'_HRM_Attend_Normal_Calendar.cb_HRM_ATTEND_CALENDAR',tableName:'cb_HRM_ATTEND_CALENDAR',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:DataForm_CalendarType_OnSelect,panelHeight:200" />
            </Columns>
        </JQTools:JQDataForm>
        <a id="openCreatHoliday" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'">自動產生假日</a>

<%--        <a id="copyCreatHoliday" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'">複製行事曆</a>--%>

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
        <!-- 檢核加班刷卡資料對話框內容的 DIV -->
        <div id="Dialog_openCreatHoliday">
            <div class="div_RelativeLayout">
                <div id="Div_Area">
                    <table class="TableClass">
                        <tr>
                            <td class="TableFieldTitle">行事曆</td>
                            <td>
                                <input id="CalendarType" name="Type" type="text" class="info-combobox" data-options="" infolight-options="field:&#39;Type&#39;,form:&#39;DataForm_Calendar&#39;,valueField:&#39;CALENDAR_ID&#39;,textField:&#39;CALENDAR_CNAME&#39;,remoteName:&#39;_HRM_Attend_Normal_Calendar.cb_HRM_ATTEND_CALENDAR&#39;,tableName:&#39;cb_HRM_ATTEND_CALENDAR&#39;,pageSize:&#39;-1&#39;,checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" style="width: 180px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TableFieldTitle">年度</td>
                            <td>
                                <input id="Input_CreateYear" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TableFieldTitle">出勤日期</td>
                            <td>
                                <JQTools:JQDateBox ID="JQAttendDateBegin" Format="DateTime" runat="server" Width="200px" ShowSeconds="False" />
                            </td>
                            <td class="TableFieldTitle">至</td>
                            <td>
                                <JQTools:JQDateBox ID="JQAttendDateEnd" Format="DateTime" runat="server" Width="200px" ShowSeconds="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TableFieldTitle">星期</td>
                            <td>
                                <JQTools:JQComboBox ID="JQWeekType" runat="server" CheckData="True" DisplayMember="" RemoteName="" ValueMember="" PanelHeight="200" OnSelect="">
                                    <Items>
                                        <JQTools:JQComboItem Selected="False" Text="星期一" Value="1" />
                                        <JQTools:JQComboItem Selected="False" Text="星期二" Value="2" />
                                        <JQTools:JQComboItem Selected="False" Text="星期三" Value="3" />
                                        <JQTools:JQComboItem Selected="False" Text="星期四" Value="4" />
                                        <JQTools:JQComboItem Selected="False" Text="星期五" Value="5" />
                                        <JQTools:JQComboItem Selected="False" Text="星期六" Value="6" />
                                        <JQTools:JQComboItem Selected="False" Text="星期日" Value="0" />
                                    </Items>
                                </JQTools:JQComboBox>
                            </td>
                            <td class="TableFieldTitle">假日類別</td>
                            <td>
                                <JQTools:JQComboBox ID="JQHolidayType" runat="server" CheckData="True" DisplayMember="HOLIDAY_TYPE_CNAME" RemoteName="_HRM_Attend_Normal_Calendar.HRM_ATTEND_HOLIDAY_TYPE" ValueMember="HOLIDAY_TYPE_ID" PanelHeight="200">
                                </JQTools:JQComboBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="Dialog_copyCreatHoliday">
            <div class="div_RelativeLayout">
                <div id="Div2">
                    <table class="TableClass">
                        <tr>
                            <td class="TableFieldTitle">由行事曆</td>
                            <td>
                                <input id="fromCalendarType" name="Type" type="text" class="info-combobox" data-options="" infolight-options="field:&#39;Type&#39;,form:&#39;DataForm_Calendar&#39;,valueField:&#39;CALENDAR_ID&#39;,textField:&#39;CALENDAR_CNAME&#39;,remoteName:&#39;_HRM_Attend_Normal_Calendar.cb_HRM_ATTEND_CALENDAR&#39;,tableName:&#39;cb_HRM_ATTEND_CALENDAR&#39;,pageSize:&#39;-1&#39;,checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" style="width: 180px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TableFieldTitle">複製到行事曆</td>
                            <td>
                                <input id="toCalendarType" name="Type" type="text" class="info-combobox" data-options="" infolight-options="field:&#39;Type&#39;,form:&#39;DataForm_Calendar&#39;,valueField:&#39;CALENDAR_ID&#39;,textField:&#39;CALENDAR_CNAME&#39;,remoteName:&#39;_HRM_Attend_Normal_Calendar.cb_HRM_ATTEND_CALENDAR&#39;,tableName:&#39;cb_HRM_ATTEND_CALENDAR&#39;,pageSize:&#39;-1&#39;,checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" style="width: 180px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TableFieldTitle">年度</td>
                            <td>
                                <input id="Input_CopyYear" type="text" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
