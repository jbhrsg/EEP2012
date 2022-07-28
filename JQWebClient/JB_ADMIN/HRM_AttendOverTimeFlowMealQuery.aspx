<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_AttendOverTimeFlowMealQuery.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        //呼叫Report視窗
        function OpenexportGrid() {

            var YearMonth = $('#LastOneUpdateYM_Query').combobox('getValue');
            if (YearMonth == "") { alert("請選擇特定的「人事審核年月」"); return false;}
            var dt = new Date();
            var ExportDate = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd');
            //var ExportDate = $("#JQDate").combo('textbox').val();
            var url = "../JB_ADMIN/REPORT/OvertimeMeal/BankRemitReportView1.aspx?YearMonth=" + YearMonth + "&ExportDate=" + ExportDate;

            var height = $(window).height() - 350;
            var width = $(window).width() - 200;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                width: width,
                title: "Report",
                top:0,
                //maximizable: true                              
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="100%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('open');
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sHRMAttendOverTimeMealQuery.View_HRMAttendOverTimeApplyMaster" runat="server" AutoApply="True"
                DataMember="View_HRMAttendOverTimeApplyMaster" Pagination="True" QueryTitle="查詢條件"
                Title="加班報餐費明細" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" AllowAdd="False" ViewCommandVisible="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計:" UpdateCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="加班單號" Editor="text" FieldName="OverTimeNO" MaxLength="10" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="工號" Editor="text" FieldName="EmployeeID" MaxLength="50" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OverTimeDeptID" Editor="text" FieldName="OverTimeDeptID" MaxLength="10" Width="50" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OverTimeRoteID" Editor="text" FieldName="OverTimeRoteID" MaxLength="10" Width="50" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="MasterTotalHours" Editor="text" FieldName="MasterTotalHours" MaxLength="17" Width="50" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班事由" Editor="text" FieldName="Memo" MaxLength="250" Width="50" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" MaxLength="1" Width="50" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Width="50" MaxLength="20" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" MaxLength="4" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加班人" Editor="text" FieldName="EmployeeText" Width="50" MaxLength="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OverTimeNO1" Editor="text" FieldName="OverTimeNO1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="加班日期" Editor="text" FieldName="OverTimeDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="起始時間" Editor="text" FieldName="BeginTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="OverTimeDateTimeBegin" Editor="text" FieldName="OverTimeDateTimeBegin" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="終止時間" Editor="text" FieldName="EndTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="OverTimeDateTimeEnd" Editor="text" FieldName="OverTimeDateTimeEnd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="OverTimeHours" Editor="text" FieldName="OverTimeHours" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="RestHours" Editor="text" FieldName="RestHours" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="加班費時數" Editor="text" FieldName="TotalHours" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="OverTimeCauseID" Editor="text" FieldName="OverTimeCauseID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy1" Editor="text" FieldName="CreateBy1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate1" Editor="text" FieldName="CreateDate1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="申請類型" Editor="text" FieldName="ApplyOvertimeType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="餐別" Editor="text" FieldName="MealType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="補助金額" Editor="text" FieldName="MealTotalNTD" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="LastOneUpdateYM" Editor="text" FieldName="LastOneUpdateYM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="Insert" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="OpenexportGrid" Text="匯款明細" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="人事審核年月" Condition="%" DataType="string" Editor="infocombobox" FieldName="LastOneUpdateYM" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'UpdateDateYM',textField:'UpdateDateYM',remoteName:'sHRMAttendOverTimeMealQuery.UpdateDateYM',tableName:'UpdateDateYM',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工工號" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeID',remoteName:'sHRMAttendOverTimeMealQuery.EmployeeID',tableName:'EmployeeID',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EmployeeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工姓名" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EmployeeText',textField:'EmployeeText',remoteName:'sHRMAttendOverTimeMealQuery.EmployeeText',tableName:'EmployeeText',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EmployeeText" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
