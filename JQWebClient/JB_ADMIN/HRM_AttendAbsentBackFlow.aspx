<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_AttendAbsentBackFlow.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">

        var flag = true; //定義一個全域變數，只有第一次執行
        $(document).ready(function () {
           
        });

        //過濾請假紀錄執行setwehre方法，過濾自己的條件
        function employeeFilter() { 
            if (flag) {
                userid = getClientInfo("UserID");
                var WhereString = "";
                WhereString = WhereString + "b.EMPLOYEE_CODE = '" + userid + "'";
                $("#dataFormMasterAbsentMinusID").refval('setWhere', WhereString);
                flag = false;
            }
        }
    </script>   
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sHRMAttendAbsentBack.HRMAttendAbsentBackApply" runat="server" AutoApply="True"
                DataMember="HRMAttendAbsentBackApply" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="銷假申請">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="AbsentBackID" Editor="numberbox" FieldName="AbsentBackID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="AbsentMinusID" Editor="numberbox" FieldName="AbsentMinusID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="HolidayText" Editor="text" FieldName="HolidayText" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AbsentDateTimeBegin" Editor="datebox" FieldName="AbsentDateTimeBegin" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AbsentDateTimeEnd" Editor="datebox" FieldName="AbsentDateTimeEnd" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="TotalHours" Editor="numberbox" FieldName="TotalHours" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="BackMemo" Editor="text" FieldName="BackMemo" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="銷假申請" DialogLeft="50px" DialogTop="20px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HRMAttendAbsentBackApply" HorizontalColumnsCount="2" RemoteName="sHRMAttendAbsentBack.HRMAttendAbsentBackApply" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnLoadSuccess="employeeFilter" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AbsentBackID" Editor="numberbox" FieldName="AbsentBackID" Format="" Width="80" NewRow="False" ReadOnly="False" Span="1" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假單號" Editor="inforefval" EditorOptions="title:'請假資料',panelWidth:536,panelHeight:318,remoteName:'sHRMAttendAbsentBack.infoHRM_ATTEND_ABSENT_MINUS',tableName:'infoHRM_ATTEND_ABSENT_MINUS',columns:[{field:'ABSENT_MINUS_ID',title:'請假單號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'HOLIDAY_CNAME',title:'假別',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ABSENT_DATE_TIME_BEGIN',title:'起始請假日期',width:115,align:'left',table:'',isNvarChar:false,queryCondition:'',formatter:formatValue,format:'yyyy/mm/dd HH:MM '},{field:'ABSENT_DATE_TIME_END',title:'截止請假日期',width:115,align:'left',table:'',isNvarChar:false,queryCondition:'',formatter:formatValue,format:'yyyy/mm/dd HH:MM '},{field:'TOTAL_HOURS',title:'總時數',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'HolidayText',value:'HOLIDAY_CNAME'},{field:'AbsentDateTimeBegin',value:'ABSENT_DATE_TIME_BEGIN'},{field:'AbsentDateTimeEnd',value:'ABSENT_DATE_TIME_END'},{field:'TotalHours',value:'TOTAL_HOURS'}],whereItems:[],valueField:'ABSENT_MINUS_ID',textField:'ABSENT_MINUS_ID',valueFieldCaption:'請假單號',textFieldCaption:'請假單號',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="AbsentMinusID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="假別" Editor="text" FieldName="HolidayText" Width="80" NewRow="False" ReadOnly="True" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始日期" Editor="text" FieldName="AbsentDateTimeBegin" Format="yyyy/mm/dd HH:MM " Width="120" NewRow="False" ReadOnly="True" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止日期" Editor="text" FieldName="AbsentDateTimeEnd" Width="120" NewRow="False" ReadOnly="True" Span="1" Visible="True" Format="yyyy/mm/dd HH:MM " />
                        <JQTools:JQFormColumn Alignment="left" Caption="總時數" Editor="text" FieldName="TotalHours" maxlength="0" Width="80" NewRow="False" ReadOnly="True" Span="1" Visible="True" />
                         <JQTools:JQFormColumn Alignment="left" Caption="銷假原因" Editor="textarea" FieldName="BackMemo" Width="300" EditorOptions="height:50" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="80" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="80" maxlength="0" NewRow="False" ReadOnly="False" Visible="False" />

                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AbsentBackID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckMethod="" CheckNull="True" FieldName="AbsentMinusID" RemoteMethod="True" ValidateMessage="請選擇請假單號" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="BackMemo" RemoteMethod="True" ValidateMessage="請輸入銷假原因" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
