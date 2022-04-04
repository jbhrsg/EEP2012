<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TRN_CoursePlan.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>年度計畫展開</title>
     <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var Today = new Date();
            var yyyy = Today.getFullYear();
            $("#cbYEAR").combobox('resize', '80');
            $("#cbYEAR").combobox('setValue', yyyy);
            $("#cbSpreadType").combobox('setValue', 1);
            //CoursePlanFilt();
            var dgid = $('#JQDataGrid1');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 480 });
            $('.infosysbutton-q', '#JQDataGrid1').closest('td').attr('align', 'middle');
            CoursePlanFilt();
            $('#JQDialog3').dialog({
                onClose: function () {
                    var UserID = getClientInfo("UserID");
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sCoursePlan.CoursePlanRecord',
                        data: "mode=method&method=" + "DeleteCoursePlanStudentList" + "&parameters=" + UserID,
                        cache: false,
                        async: false,
                        success: function (data) {
                            if (data == "True") {
                            }
                        }
                    }
                    );
                }
            });
        });
        var DataGridView_FormatScript = function (value, row, index) {
            var fieldName = this.field;
            return $('<a>', { href: 'javascript: void(0)', title: '',onclick: "DataGridView_CommandTrigger.call(this,'')" }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '展開計畫'})[0].outerHTML;
        }
        var DataGridView_CommandTrigger = function (command) {
            var RowData = $("#dataGridView").datagrid('getSelected');
            var CourseNum = RowData.CourseNum;
            if (CourseNum > 0) {
                alert('注意!!,此計畫已展開,無法重複展開');
                return false;
            }
            var Year = RowData.Year;
            var PlanID = RowData.PlanID;
            var SpreadType = RowData.SpreadType;
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCoursePlan.CoursePlanRecord', 
                data: "mode=method&method=" + "procSetUpCoursePlan" + " &parameters=" + Year + "," + PlanID + "," + SpreadType+"," + UserID, 
                cache: false,
                async: false,
                success: function (data) {
                    if (data == "True") {
                        $.messager.confirm('提示訊息', '計畫產生成功,按下「確定」離開', function (r) {
                            if (r) {
                                $("#dataGridView").datagrid('reload');
                            }
                        })
                    }
                    else {
                        alert("產生計畫失敗")
                    }
                }
            });
        }
        function JQDataForm1LoadSuccess() {
            if (getEditMode($("#JQDataForm1")) == 'inserted') {
                var RowData = $("#dataGridView").datagrid('getSelected');
                $("#JQDataForm1PlanID").val(RowData.PlanID);
                //var rowData = $("#JQDataGrid1").datagrid('getSelected');
                //var PlanID = rowData.PlanID;
                //var wherestr = "CourseID not in (Select CourseID From CoursePlanDetails Where PlanID =" + "'" + PlanID + "')";
                //alert(wherestr);
                //$("#JQDataForm1CourseID").refVal('setWhere', wherestr);
                //var Type = 'insert';
                //GetCourseListCoursePlan(PlanID, Type);
            }
        }
        function JQDataForm1OnApply() {
            if (getEditMode($("#JQDataForm1")) == 'inserted') {
                var CourseID = $("#JQDataForm1CourseID").refval('getValue');
                if (CourseID == '' || CourseID == null) {
                    alert('注意!!課程未選取,請選取');
                    return false;
                }
             }
        }
        function CourseIDOnSelect() {
        }
        function OpenSeasonOnSelect(rowData) {
            var Year = $("#cbYEAR").combobox("getValue");
            var StdDate = Year + '/' + rowData.StartDate;
            var EndDate = Year + '/' + rowData.EndDate;
            $("#JQDataForm1StartDate1").datebox('setValue', StdDate);
            $("#JQDataForm1StartDate2").datebox('setValue', EndDate);
        }
        function dataGridViewOnSelect(rowindex, rowdata) {
            var PlanName = rowdata.PlanName + ' 課程計畫列表';
            $("#JQDataGrid1").datagrid('getPanel').panel('setTitle', PlanName);
            $("#JQDataGrid1").datagrid('options').title = PlanName;
            if (rowdata != null && rowdata != undefined) {
                var PlanID = rowdata.PlanID;
                $("#JQDataGrid1").datagrid('setWhere', "PlanID=" + PlanID);
            }
            else
                $("#JQDataGrid1").datagrid('setWhere', "1=2");
        }
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;'  />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        //
        function OnSelectYEAR() {
            CoursePlanFilt();
        }
        function CoursePlanFilt() {
            var Year = $("#cbYEAR").combobox("getValue");
            var SpreadType = $("#cbSpreadType").combobox("getValue");
            var FiltStr = "Year=" + "'" + Year + "'" + " and SpreadType=" + "'" + SpreadType + "'";
            $("#dataGridView").datagrid('setWhere', FiltStr);
            $("#JQDataGrid1").datagrid('setWhere', "1=2");
        }
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'JQDataGrid1') {
                var rowData = $("#JQDataGrid1").datagrid('getSelected');
                var PlanID = rowData.PlanID;
                var wherestr = [];
                var val = '';
                val = $('#ClassName_Query').val();
                if (val != '') {
                    wherestr.push("ClassName like '%" + val + "%'");
                }
                wherestr.push("PlanID=" + PlanID);
                $(dg).datagrid('setWhere', wherestr.join(' and '));
            }
        }
        //檢查計畫是否可刪除
        function dataGridViewOnDelete(data) {
            var PlanID = data.PlanID;
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCoursePlan.CoursePlanRecord', 
                data: "mode=method&method=" + "CheckDelCoursePlan" + "&parameters=" + PlanID, 
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        cnt = $.parseJSON(data);

                    }
                }
            });
         
            if ((cnt == 0) || (cnt == "undefined")) {
        
                dataGridViewOnDeleted(PlanID);
                return true;
            }
            else {
                alert('注意!!,此課程計畫已有課程開課,無法刪除！！');
                return false;
            }
        }
        function dataGridViewOnDeleted(PlanID) {
             $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCoursePlan.CoursePlanRecord', 
                data: "mode=method&method=" + "ProcDeleteCoursePlan" + " &parameters=" + PlanID, 
                cache: false,
                async: false,
                success: function (data) {
                    if (data == "True") {
                      //  $.messager.confirm('提示訊息', '課程計畫刪除成功,按下「確定」離開', function (r) {
                      //      if (r) {
                     //           $("#dataGridView").datagrid('reload');
                     //       }
                     //   })
                    }
                 }
            });
        }
        //檢視課程需上課名單
        function ViewCourseStudentBill() {
            var RowData = $("#JQDataGrid1").datagrid('getSelected');
            var ClassName = RowData.ClassName+' 職能應上課名單';
            $("#JQDataGrid2").datagrid('getPanel').panel('setTitle', ClassName);
            $("#JQDataGrid2").datagrid('options').title = ClassName;
            var CourseID = RowData.CourseID;
            var StartDate = RowData.StartDate1;
            var EndDate = RowData.StartDate2;
            GetCoursePlanStudentList(CourseID, StartDate, EndDate);
            openForm('#JQDialog3', {}, "", 'dialog');
        }
        //取得課程對應職能應上課名單
        function GetCoursePlanStudentList(CourseID, StartDate, EndDate) {
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCoursePlan.CoursePlanRecord', 
                data: "mode=method&method=" + "GetCoursePlanStudentList" + "&parameters=" + CourseID + "," + StartDate + "," + EndDate+","+UserID,
                cache: false,
                async: false,
                success: function (data) {
                    //var rows = $.parseJSON(data);
                    if (data == "True") {
                        $("#JQDataGrid2").datagrid('reload');
                    }
                }
            }
            );
        }
        function JQDataGrid2OnLoadSucess() {
        }
        function JQDialog3OnSubmited() {
         
            return true
        }
        function GetSpreadType() {
            return $("#cbSpreadType").combobox("getValue");
        }
        function JQDataForm1OnAppied() {
            $("#JQDataGrid1").datagrid('reload');
        }
        //取得課程計畫課程資料
        function GetCourseListCoursePlan(PlanID, Type) {
             $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCoursePlan.CoursePlanRecord', 
                data: "mode=method&method=" + "GetCourseListCoursePlan" + "&parameters=" + PlanID + "," + Type,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $('#JQDataForm1CourseID').combobox('loadData', rows);
                    }
                }
            }
            );
        }
        function YearOnSelect() {
            var year = $("#cbYEAR").combobox("getValue");
            if (year != '    ') {
               $("#dataFormMasterPlanRange").val(year + '/01/01 - ' + year + '/12/31');
            }
        }
    </script>
</head>
<body>

    <form id="form1" runat="server">
         <br />
         <div>
            <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="年度:"></asp:Label>
            <JQTools:JQComboBox ID="cbYEAR" runat="server" DisplayMember="YEAR" PanelHeight="150" RemoteName="sCoursePlan.YearNO" ValueMember="YEAR" Width="30px" OnSelect="OnSelectYEAR">
            </JQTools:JQComboBox>
            &nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Font-Size="Small" Text="展開方式:"></asp:Label>
            <JQTools:JQComboBox ID="cbSpreadType" runat="server" DisplayMember="NAME" PanelHeight="150" RemoteName="sCoursePlan.SpreadType" ValueMember="ID" Width="30px" OnSelect="OnSelectYEAR">
            </JQTools:JQComboBox>
        </div>
        <br />
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCoursePlan.CoursePlanRecord" runat="server" AutoApply="True"
                DataMember="CoursePlanRecord" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="計畫展開" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="980px" OnSelect="dataGridViewOnSelect" OnDelete="dataGridViewOnDelete">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="年度" Editor="text" FieldName="Year" Format="" MaxLength="0" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="計畫名稱" Editor="text" FieldName="PlanName" Format="" MaxLength="0" Width="160" />
                    <JQTools:JQGridColumn Alignment="left" Caption="展開方式" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCoursePlan.SpreadType',tableName:'SpreadType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SpreadType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="計畫期間" Editor="text" FieldName="PlanRange" Format="" MaxLength="0" Width="130" />
                    <JQTools:JQGridColumn Alignment="left" Caption="展開計畫" Editor="text" FieldName="SetPlan" FormatScript="DataGridView_FormatScript" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="center" Caption="開啟" Editor="checkbox" FieldName="IsOpen" Format="" Width="40" EditorOptions="on:1,off:0" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="infocombobox" FieldName="CreateBy" Format="" MaxLength="0" Width="110" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sCoursePlan.UserInfo',tableName:'UserInfo',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Width="110" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="right" Caption="PlanID" Editor="numberbox" FieldName="PlanID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="RequestID" Editor="numberbox" FieldName="RequestID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PlanType" Editor="text" FieldName="PlanType" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsMain" Editor="text" FieldName="IsMain" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CourseNum" Editor="text" FieldName="CourseNum" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增計畫" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="課程計畫展開" DialogLeft="15px" DialogTop="50px" Width="480px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CoursePlanRecord" HorizontalColumnsCount="1" RemoteName="sCoursePlan.CoursePlanRecord" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="計畫代號" Editor="numberbox" FieldName="PlanID" Format="" Width="66" Visible="True" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="RequestID" Editor="numberbox" FieldName="RequestID" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="年度" Editor="infocombobox" FieldName="Year" Format="" maxlength="0" Width="70" EditorOptions="valueField:'YEAR',textField:'YEAR',remoteName:'sCoursePlan.YearNO',tableName:'YearNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:YearOnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="計畫名稱" Editor="text" FieldName="PlanName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="展開方式" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCoursePlan.SpreadType',tableName:'SpreadType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SpreadType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="期間範圍" Editor="text" FieldName="PlanRange" Format="" Width="180" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="計畫開啟" Editor="checkbox" FieldName="IsOpen" Format="" Width="180" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PlanType" Editor="text" FieldName="PlanType" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsMain" Editor="text" FieldName="IsMain" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Visible="False" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SpreadType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="PlanID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="RequestID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
              </div>
            <br /> 
            <div>
             <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" AutoApply="True" 
                CheckOnSelect="True" ColumnsHibeable="False" DataMember="CoursePlanDetails" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" 
                EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="20" Pagination="True" QueryAutoColumn="False" 
                QueryLeft="10px" QueryMode="Window" QueryTitle="課程計劃查詢" QueryTop="250px" RecordLock="False" RecordLockMode="None" 
                RemoteName="sCoursePlan.CoursePlanDetails" Title="課程計畫" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" EditDialogID="JQDialog2" Width="980px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="計畫類別" Editor="text" FieldName="PlanTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="課程代號" Editor="text" FieldName="CourseID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="ClassName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="300" />
                    <JQTools:JQGridColumn Alignment="center" Caption="場次" Editor="text" FieldName="CourseNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="達成率(%)" Editor="text" FieldName="CloseRate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" />
                    <JQTools:JQGridColumn Alignment="center" Caption="上課中" Editor="datebox" FieldName="NumInClass" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" />
                    <JQTools:JQGridColumn Alignment="center" Caption="測驗中" Editor="numberbox" FieldName="NumInExam" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" />
                    <JQTools:JQGridColumn Alignment="center" Caption="已結案" Editor="numberbox" FieldName="NumInClose" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" />
                    <JQTools:JQGridColumn Alignment="center" Caption="季別" Editor="text" FieldName="OpenSeason" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="StartDate1" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="67" />
                    <JQTools:JQGridColumn Alignment="left" Caption="終止日期" Editor="datebox" EditorOptions="" FieldName="StartDate2" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="67" />
                    <JQTools:JQGridColumn Alignment="center" Caption="開啟計畫" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PlanID" Editor="text" FieldName="PlanID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="ViewCourseStudentBill" Text="檢視需上課學員" Visible="True" />
                </TooItems>
                 <QueryColumns>
                     <JQTools:JQQueryColumn AndOr="and" Caption="課程名稱" Condition="%%" DataType="string" Editor="text" FieldName="ClassName" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                 </QueryColumns>
            </JQTools:JQDataGrid>
             <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="JQDataForm1" Title="課程計畫維護" Width="650px" DialogLeft="15px">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="CoursePlanDetails" HorizontalColumnsCount="1" RemoteName="sCoursePlan.CoursePlanDetails" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnLoadSuccess="JQDataForm1LoadSuccess" OnApply="JQDataForm1OnApply" OnApplied="JQDataForm1OnAppied" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="課程" Editor="inforefval" FieldName="CourseID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="480" EditorOptions="title:'課程搜尋',panelWidth:460,remoteName:'sCoursePlan.CourseList',tableName:'CourseList',columns:[{field:'CourseName',title:'課程名稱',width:260,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CourseID',title:'課程代號',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'ClassName',value:'CourseName'}],whereItems:[],valueField:'CourseID',textField:'CourseName',valueFieldCaption:'CourseID',textFieldCaption:'CourseName',cacheRelationText:false,checkData:false,showValueAndText:false,onSelect:CourseIDOnSelect,selectOnly:true" />
                        <JQTools:JQFormColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="ClassName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="360" />
                        <JQTools:JQFormColumn Alignment="left" Caption="場次" Editor="text" FieldName="CourseNO" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="計畫類別" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCoursePlan.PlanType',tableName:'PlanType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PlanType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開課期別" Editor="infocombobox" EditorOptions="valueField:'Season',textField:'Season',remoteName:'sCoursePlan.OpenSeason',tableName:'OpenSeason',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OpenSeasonOnSelect,panelHeight:200" FieldName="OpenSeason" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="StartDate1" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止日期" Editor="datebox" FieldName="StartDate2" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開啟" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ParentKey" Editor="text" FieldName="ParentKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PlanID" Editor="text" FieldName="PlanID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsClass" Editor="text" FieldName="IsClass" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SpreadType" Editor="text" FieldName="SpreadType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="PlanType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="CourseNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                    
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="StartDate1" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="StartDate2" RemoteMethod="True" />
                    
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsClass" RemoteMethod="True" />
                    
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetSpreadType" FieldName="SpreadType" RemoteMethod="False" />
                    
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    <Columns>

                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
     
      </div>
      <JQTools:JQDialog ID="JQDialog3" runat="server" DialogLeft="15px" DialogTop="60px" Title="" Width="820px" OnSubmited="JQDialog3OnSubmited" Closed="True">
                 <JQTools:JQDataGrid ID="JQDataGrid2" runat="server" AlwaysClose="False" DataMember="CourseStudentList" RemoteName="sCoursePlan.CourseStudentList" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Window" QueryTitle="設備查詢" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="課程職能應上課名單test" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" OnUpdate="" Height="450px" Width="810px" OnLoadSuccess="JQDataGrid2OnLoadSucess">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="部門代號" Editor="text" FieldName="DeptID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70" />
                        <JQTools:JQGridColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="DEPTNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="學員代號" Editor="text" FieldName="STUDENTID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" />
                        <JQTools:JQGridColumn Alignment="left" Caption="學員名稱" Editor="text" FieldName="STUDENTNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                        <JQTools:JQGridColumn Alignment="center" Caption="職別代號" Editor="text" FieldName="JOBTYPE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="職別名稱" Editor="text" FieldName="JOBNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="職能代號" Editor="text" FieldName="JOBID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="職能名稱" Editor="text" FieldName="JOBNAME1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180" />
               
                    </Columns>
                     <TooItems>
                     <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                     <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
                
    </form>

</body>
</html>
