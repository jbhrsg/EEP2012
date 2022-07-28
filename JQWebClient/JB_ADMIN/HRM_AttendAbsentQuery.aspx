<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_AttendAbsentQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>員工出勤紀錄查詢</title>
    <script src="../js/jquery.jbjob.js"></script>
    <script>
        var flag = true; //定義一個全域變數，只有第一次執行
        var flagAnnualLeave = true; //定義一個全域變數，只有第一次執行
        $(document).ready(function () {
            initAbsentMinusDialog();
            initOvertimeDialog();
            initDialog_AttendAbsentPlus();

            //var Button1 = $("<button type='button'>").attr({ 'OnClick': 'OnClick1()' }).text("格式");//.linkbutton({ 'plain': false });
            var LinkAnnualLeave = $('<a>', { href: 'javascript:void(0)', name: 'AnnualLeave', onclick: 'LinkAnnualLeave.call(this)',}).linkbutton({ iconCls: 'icon-tip', plain: false, text: '開啟特休查詢視窗' })[0].outerHTML

            $('#SALARY_YYMM_Query').closest('td').append("&nbsp;&nbsp;&nbsp;&nbsp;");
            $('#SALARY_YYMM_Query').closest('td').append($('.infosysbutton-q', '#querydataGridMaster').closest('td').contents()).append("&nbsp;&nbsp;&nbsp;&nbsp;").append(LinkAnnualLeave);

            //$($('#querydataGrid_AttendAbsentPlus').find('input')[2]).closest('td').append($('.infosysbutton-q', '#querydataGrid_AttendAbsentPlus')).append($('.infosysbutton-cl', '#querydataGrid_AttendAbsentPlus'));
        });
        //dataGridMaster_OnLoad
        function employeeFilter() { //執行setwehre方法，過濾自己的條件
            if (flag) {
                var userid = getClientInfo("UserID");
                var WhereString = "";
                var WhereString1 = "";
                WhereString = WhereString + "V.USERID = '" + userid + "';";  //11 = 11 C.DEPT_MANAGER
                WhereString = WhereString + "B.USERID <> '" + userid + "';";   //12 = 12 M.DEPT_MANAGER
                WhereString = WhereString + "u.USERID = '" + userid + "';";  //13 = 13 A.EMPLOYEE_CODE
                
                var dt = new Date();
                var aDate = new Date($.jbDateAdd('days', 0, dt));//取得今天日期
                WhereString1 = WhereString + "u.USERID = '" + userid + "';"; //14=14
                WhereString1 = WhereString1 + "M.SALARY_YYMM ='" + $.jbjob.Date.DateFormat(aDate, 'yyyyMM') + "'"//15=15(最後面的where)
                
                setTimeout(function () {
                    $("#dataGridMaster").datagrid('setWhere', WhereString1);
                    $('#ORG_NO_Query').combobox('setWhere', WhereString);//不含14,15
                }, 800);

                $('#SALARY_YYMM_Query').combobox('setValue', $.jbjob.Date.DateFormat(aDate, 'yyyyMM'));
                flag = false;
            }
        }
        
        //特休 OnLoadSuccess(沒用到)
        function Onload_dataGrid_AttendAbsentPlus() { //執行setwehre方法，過濾自己的條件
            //if (flagAnnualLeave) {
                var userid = getClientInfo("UserID");
                var WhereString = "";
                //var WhereString1 = "";
                WhereString = WhereString + "C.DEPT_MANAGER = '" + userid + "';";  //11 = 11
                WhereString = WhereString + "M.DEPT_MANAGER <> '" + userid + "';";   //12 = 12
                WhereString = WhereString + "A.EMPLOYEE_CODE = '" + userid + "';";  //13 = 13
                //var dt = new Date();
                //var aDate = new Date($.jbDateAdd('days', 0, dt));//取得今天日期
                //WhereString1 = WhereString + "M.SALARY_YYMM ='" + $.jbjob.Date.DateFormat(aDate, 'yyyyMM') + "'"//14=14(最後面的where)
                
                $("#dataGrid_AttendAbsentPlus").datagrid('setWhere', WhereString);
                //setTimeout(function () {
                    $($('#querydataGrid_AttendAbsentPlus').find('input')[0]).combobox('setWhere', WhereString);
                //}, 800);

                //flagAnnualLeave = false;
            //}
        }
        //特休 Init dialog
        function initDialog_AttendAbsentPlus() {
            $("#Dialog_AttendAbsentPlus").dialog(
            {
                height: 480,
                width: 950,
                left: 20,
                top: 20,
                resizable: false,
                modal: true,
                title: "現在特休狀況",
                closed: true
            });
        };
        //特休 dialog開啟
        function LinkAnnualLeave() {
            var userid = getClientInfo("UserID");
            var WhereString = "";
            WhereString = WhereString + "(V.USERID = '" + userid + "');";  //11 = 11
            WhereString = WhereString + "(B.USERID <> '" + userid + "');";   //12 = 12
            WhereString = WhereString + "(u.DESCRIPTION='JB' and u.USERID = '" + userid + "');";  //13 = 13
            var WhereString1 = WhereString + "(u.DESCRIPTION='JB' and u.USERID = '" + userid + "');"; //14=14
            $("#dataGrid_AttendAbsentPlus").datagrid('setWhere', WhereString1);
            $($('#querydataGrid_AttendAbsentPlus').find('input')[0]).combobox('setWhere', WhereString);//部門 不含14

            $("#Dialog_AttendAbsentPlus").dialog("open");
        }

        // 加班紀錄 dialog
        function initOvertimeDialog() {
            $("#Dialog_Overtime").dialog(
            {
                height: 400,
                width: 1050,
                left: 20,
                top: 20,
                resizable: false,
                modal: true,
                title: "加班紀錄",
                closed: true
            });
        };

        //加班紀錄
        function OvertimeLink(value, row, index) {
            return $('<a>', { href: 'javascript:void(0)', name: 'AbsentMinus', onclick: 'LinkOvertime.call(this)', rowIndex: index }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '加班紀錄' })[0].outerHTML
        }

        // open加班紀錄畫面 dialog
        function LinkOvertime() {
            //alert(index);
            var index = $(this).attr('rowIndex');
            $("#dataGridMaster").datagrid('selectRow', index);
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var ID = rows.USERID;
            $("#dataGrid_Overtime").datagrid('setWhere', "HRM_BASE_BASE.EMPLOYEE_CODE = '" + ID + "' AND HRM_ATTEND_OVERTIME_DATA.SALARY_YYMM ='" + rows.SALARY_YYMM + "'");
            $("#Dialog_Overtime").dialog("open");
        }

        //過濾加班 && 請假紀錄查詢
        function queryGrid(dg) {
            var userid = getClientInfo("UserID");
            var where = $(dg).datagrid('getWhere');
            if ($(dg).attr('id') == 'dataGrid_Overtime') {
                var rows = $("#dataGridMaster").datagrid('getSelected');
                if (where.length > 0) {
                    where = where + " and HRM_BASE_BASE.EMPLOYEE_CODE = '" + rows.USERID+"'";
                    $(dg).datagrid('setWhere', where);
                }
            }
            else if ($(dg).attr('id') == 'dataGrid_AbsentMinus') {
                var rows = $("#dataGridMaster").datagrid('getSelected');
                if (where.length > 0) {
                    where = where + " and HRM_BASE_BASE.EMPLOYEE_CODE = '" + rows.USERID+"'";
                    $(dg).datagrid('setWhere', where);
                }
            }
            else if ($(dg).attr('id') == 'dataGrid_AttendAbsentPlus') {
                var where = $(dg).datagrid('getWhere');
                var WhereString = "";
                WhereString = WhereString + "(V.USERID = '" + userid + "');";  //11 = 11
                WhereString = WhereString + "(B.USERID <> '" + userid + "');";   //12 = 12
                WhereString = WhereString + "(u.DESCRIPTION='JB' and u.USERID = '" + userid + "');";  //13 = 13
                WhereString = WhereString + "(u.DESCRIPTION='JB' and u.USERID = '" + userid + "');"; //14=14
                $(dg).datagrid('setWhere', WhereString + where);//where就是15=15
            }
            else {
                var where = $(dg).datagrid('getWhere');
                var WhereString = "";
                WhereString = WhereString + "(V.USERID = '" + userid + "');";  //11 = 11
                WhereString = WhereString + "(B.USERID <> '" + userid + "');";   //12 = 12
                WhereString = WhereString + "(u.USERID = '" + userid + "');";  //13 = 13
                WhereString = WhereString + "(u.USERID = '" + userid + "');"; //14=14
                //14 = 14
                //WhereString = WhereString + "EMPLOYEE_ID = '" + userid + "' and HOLIDAY_ID='23' and GETDATE() between BEGIN_DATE and END_DATE" + ";";  
                $(dg).datagrid('setWhere', WhereString + where);//where就是15=15
            }
        }

        // 請假紀錄 dialog
        function initAbsentMinusDialog() {
            $("#Dialog_AbsentMinus").dialog(
            {
                height: 450,
                width: 950,
                left: 20,
                top: 20,
                resizable: false,
                modal: true,
                title: "請假紀錄",
                closed: true
            });
        };

        //請假紀錄
        function AbsentMinusLink(value, row, index) {
            return $('<a>', { href: 'javascript:void(0)', name: 'AbsentMinus', onclick: 'LinkAbsentMinus.call(this)', rowIndex: index }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '請假紀錄' })[0].outerHTML
        }

        // open請假紀錄畫面 dialog
        function LinkAbsentMinus() {
            var index = $(this).attr('rowIndex');
            $("#dataGridMaster").datagrid('selectRow', index);
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var ID = rows.USERID;
            $("#dataGrid_AbsentMinus").datagrid('setWhere', "HRM_BASE_BASE.EMPLOYEE_CODE = '" + ID + "' AND HRM_ATTEND_ABSENT_MINUS.SALARY_YYMM ='" + rows.SALARY_YYMM + "'");
            $("#Dialog_AbsentMinus").dialog("open");
        }

        function genCheckBox(val) {
            if (val == 'Y')
                return "<input  type='checkbox' checked='true' />";
            else
                return "<input  type='checkbox' />";
        }

        function DefaultMethod_SALARY_YYMM_Absent() {
            var rows = $("#dataGridMaster").datagrid('getSelected');
            $($('#querydataGrid_AbsentMinus').find('input')[6]).val(rows.SALARY_YYMM);
        }
        function DefaultMethod_SALARY_YYMM_Overtime() {
            var rows = $("#dataGridMaster").datagrid('getSelected');
            $($('#querydataGrid_Overtime').find('input')[6]).val(rows.SALARY_YYMM);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sHRMAttendAbsentQuery.HRM_BASE_BASE" runat="server" AutoApply="False"
                DataMember="HRM_BASE_BASE" Pagination="True" QueryTitle="查詢條件"
                Title="員工出勤紀錄查詢" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryMode="Panel" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="employeeFilter" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="部門代碼" Editor="text" FieldName="ORG_NO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="ORG_DESC" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="160" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="USERID" MaxLength="50" Width="120" Format="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="USERNAME" Format="" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption=" " Editor="text" FieldName="AbsentMinus" MaxLength="0" Width="120" FormatScript="AbsentMinusLink" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="計薪月已請假時數" Editor="text" FieldName="SumTotalMinusHours" Sortable="False" Width="100" ReadOnly="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption=" " Editor="text" FieldName="Overtme" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="120" FormatScript="OvertimeLink">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="計薪月已加班時數" Editor="text" FieldName="SumTotalOverTimeHours" Sortable="False" Width="100" ReadOnly="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="計薪年月" Editor="text" FieldName="SALARY_YYMM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="得假起始日" Editor="text" FieldName="BEGIN_DATE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" Format="yyyy-mm-dd">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="得假截止日" Editor="text" FieldName="END_DATE" Format="yyyy-mm-dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="特休總時數" Editor="text" FieldName="TOTAL_HOURS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="特休已請時數" Editor="text" FieldName="ABSENT_HOURS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="特休剩餘時數" Editor="text" FieldName="REST_HOURS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="部門" Condition="=" DataType="string" Editor="infocombobox" FieldName="ORG_NO" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="170" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sHRMAttendAbsentQuery.DEPT_ID',tableName:'DEPT_ID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工編號" Condition="=" DataType="string" Editor="text" FieldName="USERID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工姓名" Condition="%%" DataType="string" Editor="text" FieldName="USERNAME" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="計薪年月" Condition="=" DataType="string" Editor="infocombobox" FieldName="SALARY_YYMM" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'SALARY_YYMM',textField:'SALARY_YYMM',remoteName:'sHRMAttendAbsentQuery.SALARY_YYMM',tableName:'SALARY_YYMM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
        <%--請假紀錄--%>
        <div id="Dialog_AbsentMinus">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="dataGrid_AbsentMinus" data-options="pagination:true,view:commandview" RemoteName="sHRMAttendAbsentQuery.HRM_ATTEND_ABSENT_MINUS" runat="server" AutoApply="False"
                    DataMember="HRM_ATTEND_ABSENT_MINUS" Pagination="True" QueryTitle="查詢" EditDialogID=""
                    Title="" QueryLeft="300px" QueryTop="100px" AlwaysClose="True" AllowAdd="False" AllowDelete="False" AllowUpdate="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryMode="Window" RecordLock="False" RecordLockMode="None" TotalCaption="加總 : " UpdateCommandVisible="False" ViewCommandVisible="False" Width="1020px" BufferView="False" NotInitGrid="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="請假資料流水碼" Editor="numberbox" FieldName="ABSENT_MINUS_ID" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工流水碼" Editor="text" FieldName="EMPLOYEE_ID" Format="" MaxLength="50" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EMPLOYEE_CODE" Format="" MaxLength="0" Width="80" Frozen="True" Sortable="True" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" Format="" MaxLength="0" Width="80" Frozen="True" Sortable="True" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="請假起始日期" Editor="text" FieldName="BEGIN_DATE" Format="yyyy-mm-dd" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="請假截止日期" Editor="text" FieldName="END_DATE" MaxLength="0" Width="80" Sortable="False" Format="yyyy-mm-dd" />
                        <JQTools:JQGridColumn Alignment="left" Caption="請假截止時間" Editor="text" FieldName="END_TIME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="請假起始時間" Editor="text" FieldName="BEGIN_TIME" Format="" MaxLength="50" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期(含時間)" Editor="datebox" FieldName="ABSENT_DATE_TIME_BEGIN" Format="" Width="80" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="起始請假日期(含時間)" Editor="datebox" FieldName="ABSENT_DATE_TIME_END" Format="" Width="80" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="假別代碼流水號" Editor="numberbox" FieldName="HOLIDAY_ID" Format="" Width="80" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="假別代碼" Editor="text" FieldName="HOLIDAY_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="假別名稱" Editor="text" FieldName="HOLIDAY_CNAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="部門代碼" Editor="text" FieldName="DEPT_CODE" Format="" MaxLength="0" Width="80" Sortable="True" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="DEPT_CNAME" Format="" MaxLength="0" Width="120" Sortable="True" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="請假時數/天" Editor="numberbox" FieldName="TOTAL_HOURS" Format="" Width="80" Sortable="True" OnTotal="" Total="sum" />
                        <JQTools:JQGridColumn Alignment="left" Caption="計薪年月" Editor="text" FieldName="SALARY_YYMM" Format="" MaxLength="50" Width="60" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="備註(表單備註)" Editor="text" FieldName="MEMO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" Visible="True" />
                        <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="請假起始日期" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="BEGIN_DATE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="請假截止日期" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="END_DATE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="計薪年月" Condition="%" DataType="string" Editor="text" FieldName="SALARY_YYMM" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" DefaultMethod="DefaultMethod_SALARY_YYMM_Absent" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
            </div>
        </div>

        <%--加班紀錄--%>
        <div id="Dialog_Overtime">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="dataGrid_Overtime" data-options="pagination:true,view:commandview" RemoteName="sHRMAttendAbsentQuery.HRM_ATTEND_OVERTIME_DATA" runat="server" AutoApply="False"
                    DataMember="HRM_ATTEND_OVERTIME_DATA" Pagination="True" QueryTitle="查詢" EditDialogID=""
                    Title="" QueryLeft="300px" QueryTop="100px" AlwaysClose="True" AllowAdd="False" AllowDelete="False" AllowUpdate="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryMode="Window" RecordLock="False" RecordLockMode="None" TotalCaption="加總 : " UpdateCommandVisible="False" ViewCommandVisible="False" Width="1020px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                    <Columns>
                        <%--<JQTools:JQGridColumn Alignment="left" Caption="簽核狀態" Editor="text" FieldName="FLOWFLAG" Format="" MaxLength="1" Width="120" ReadOnly="True" Sortable="True" />--%>
                        <JQTools:JQGridColumn Alignment="right" Caption="加班資料流水碼" Editor="numberbox" FieldName="OVERTIME_ID" Format="" Width="80" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工流水碼" Editor="text" FieldName="EMPLOYEE_ID" Format="" MaxLength="50" Width="80" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EMPLOYEE_CODE" Format="" MaxLength="0" Width="80" Frozen="True" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" Format="" MaxLength="0" Width="80" Frozen="True" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班日期" Editor="text" FieldName="OVERTIME_DATE" Format="yyyy-mm-dd" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="刷卡上班時間" Editor="text" FieldName="ON_TIME_TRAN" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="刷卡下班時間" Editor="text" FieldName="OFF_TIME_TRAN" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班起始時間" Editor="text" FieldName="BEGIN_TIME" Format="" MaxLength="50" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班起始時間(DATETIME)" Editor="datebox" FieldName="OVERTIME_DATE_TIME_BEGIN" Format="" Width="80" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班截止時間" Editor="text" FieldName="END_TIME" Format="" MaxLength="50" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班截止時間(DATETIME)" Editor="datebox" FieldName="OVERTIME_DATE_TIME_END" Format="" Width="80" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="加班時數" Editor="numberbox" FieldName="OVERTIME_HOURS" Format="" Width="80" Sortable="True" EditorOptions="precision:1" Total="sum" />
                        <JQTools:JQGridColumn Alignment="right" Caption="補休時數" Editor="numberbox" FieldName="REST_HOURS" Format="" Width="80" Sortable="True" EditorOptions="precision:1" Total="sum" />
                        <JQTools:JQGridColumn Alignment="right" Caption="總時數" Editor="text" FieldName="TOTAL_HOURS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" Total="sum">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="加班原因流水號" Editor="numberbox" FieldName="OVERTIME_CAUSE_ID" Format="" Width="120" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班原因代碼" Editor="text" FieldName="OVERTIME_CAUSE_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班原因" Editor="text" FieldName="OVERTIME_CAUSE_CNAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="加班班別代碼" Editor="numberbox" FieldName="OVERTIME_ROTE_ID" Format="" Width="120" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班班別代碼" Editor="text" FieldName="ROTE_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班班別" Editor="text" FieldName="ROTE_CNAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="加班部門代碼" Editor="numberbox" FieldName="OVERTIME_DEPT_ID" Format="" Width="120" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班部門代碼" Editor="text" FieldName="DEPT_CODE" Format="" MaxLength="0" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班部門名稱" Editor="text" FieldName="DEPT_CNAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班有效日期" Editor="text" FieldName="OVERTIME_EFFECT_DATE" Format="yyyy-mm-dd" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班單單號" Editor="text" FieldName="OVERTIME_NO" Format="" MaxLength="50" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="加班比率流水號" Editor="numberbox" FieldName="OVERTIME_RATE_ID" Format="" Width="120" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="計薪年月" Editor="text" FieldName="SALARY_YYMM" Format="" MaxLength="50" Width="80" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="是否匯入" Editor="checkbox" EditorOptions="on:'Y',off:'N'" FieldName="IS_IMPORT" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="center" Caption="系統產生否" Editor="checkbox" FieldName="SYSTEM_CREATE" Format="" MaxLength="50" Width="80" FormatScript="genCheckBox" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="MEMO" Format="" MaxLength="250" Width="120" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Format="" MaxLength="50" Width="120" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:mm:SS" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Format="" MaxLength="50" Width="120" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:mm:SS" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Sortable="True" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" Visible="True" />
                        <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="起始加班日期" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="OVERTIME_DATE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="截止加班日期" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="OVERTIME_DATE" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="計薪年月" Condition="%" DataType="string" Editor="text" FieldName="SALARY_YYMM" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" DefaultMethod="DefaultMethod_SALARY_YYMM_Overtime" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
            </div>






        </div>


        <%--請假紀錄--%>
        <div id="Dialog_AttendAbsentPlus">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="dataGrid_AttendAbsentPlus" data-options="pagination:true,view:commandview" RemoteName="sHRMAttendAbsentQuery.HRM_ATTEND_ABSENT_PLUS" runat="server" AutoApply="False"
                    DataMember="HRM_ATTEND_ABSENT_PLUS" Pagination="True" QueryTitle="查詢(需重新選取部門)" EditDialogID=""
                    Title="" QueryLeft="300px" QueryTop="100px" AlwaysClose="True" AllowAdd="False" AllowDelete="False" AllowUpdate="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryMode="Panel" RecordLock="False" RecordLockMode="None" TotalCaption="加總 : " UpdateCommandVisible="False" ViewCommandVisible="False" Width="930px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="DEPT_ID" Editor="text" FieldName="ORG_NO" Width="90" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="ORG_DESC" MaxLength="0" Width="180" Frozen="False" Sortable="True" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="EMPLOYEE_ID" Editor="text" FieldName="USERID" Width="90" Sortable="False" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="USERNAME" MaxLength="0" Width="90" Sortable="True" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LEVEL" Editor="text" FieldName="LEVEL" Width="90" Visible="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="到職日" Editor="text" FieldName="EFFECT_DATE" Format="yyyy-mm-dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="得假起始日" Editor="text" FieldName="BEGIN_DATE" Width="90" Visible="True" Sortable="True" Format="yyyy-mm-dd" />
                        <JQTools:JQGridColumn Alignment="left" Caption="得假截止日" Editor="text" FieldName="END_DATE" Width="90" Visible="True" Sortable="True" Format="yyyy-mm-dd" />
                        <JQTools:JQGridColumn Alignment="left" Caption="特休總時數" Editor="text" FieldName="TOTAL_HOURS" MaxLength="0" Width="90" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="特休已請時數" Editor="text" FieldName="ABSENT_HOURS" MaxLength="0" Width="90" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="特休剩餘時數" Editor="text" FieldName="REST_HOURS" MaxLength="0" Width="90" Sortable="True" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="已休完成%" Editor="text" FieldName="AbsentRatio" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="Insert" Visible="True" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="True" />
                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="部門" Condition="=" DataType="string" Editor="infocombobox" FieldName="ORG_NO" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="170" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sHRMAttendAbsentQuery.DEPT_ID',tableName:'DEPT_ID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="員工姓名" Condition="%%" DataType="string" Editor="text" FieldName="USERNAME" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
            </div>
        </div>


    </form>
</body>
</html>
