<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TRN_Course.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>課程資料維護</title>
     <style>
        .tree-node-clicked {
            background: #ff7f50;            
        }
    </style>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var Btn = $('<a>', { href: "javascript:void(0)" }).bind('click', function () {
                var nodes = $('#JQTreeViewTeacher').tree('getChecked');
                $.each(nodes, function (index, node) {
                        $('#JQTreeViewTeacher').tree('uncheck', node.target);
                        $(node.target).removeClass('.tree-node-clicked');
                 });
                var root = $('#JQTreeViewTeacher').tree('getRoot');
                $('#JQTreeViewTeacher').tree('uncheck', root.target);
                var TeacherIDS = $('#dataFormMasterTeacherIDs').val();
                JQTreeViewTeacherSetChecked(TeacherIDS);
                openForm('#JQDialog3', {}, "", 'dialog');
            }).linkbutton({ text: '選取講師' });
            $('#dataFormMasterTeachers').after(Btn);
            //設定職能樹狀結構
            var Btn1 = $('<a>', { href: "javascript:void(0)" }).bind('click', function () {
                var nodes = $('#JQTreeViewJob').tree('getChecked');
                $.each(nodes, function (index, node) {
                    $('#JQTreeViewJob').tree('uncheck', node.target);
                    $(node.target).removeClass('.tree-node-clicked');
                });
                //取得此課程職能代號,並從樹狀節點移除
                var root = $('#JQTreeViewJob').tree('getRoot');
                $('#JQTreeViewJob').tree('uncheck', root.target);
                var CourseID = GetCourseID();
                var JobIDsStr = GetCourseJobIDStr(CourseID);
                if (JobIDsStr != '') {
                    $.each(JobIDsStr.split(","), function (i, id) {
                        var node = $('#JQTreeViewJob').tree('find', id);
                        if (node != null) {
                           $('#JQTreeViewJob').tree('remove', node.target);
                        }
                    });
                }
                //var root = $('#JQTreeViewJob').tree('getRoot');
                //$('#JQTreeViewJob').tree('uncheck', root.target);
                openForm('#JQDialog4', {}, "", 'dialog');
            }).linkbutton({ text: '選取職能' });
            $('#JQDataForm1COURSENAME').after(Btn1);
            $('#JQTreeViewJob').tree({
                onlyLeafCheck: true,
                onCheck: function (node, checked) {
                    if (checked) $(node.target).addClass('tree-node-clicked');
                    else $(node.target).removeClass('tree-node-clicked');
                },
                onSelect: function (node) {
                },
                onLoadSuccess: function () {
                    //$(this).tree('collapseAll');
                }
            });
            $('#TreeView_Course').tree({
                onSelect: function (node) {
                    var SetWhereStr = String.format("CourseParentID='{0}'", node.id);
                    var SetWhereStr = SetWhereStr + ' and isClass=0';
                    $('#dataGridView').datagrid('setWhere', SetWhereStr);
                  },
                 onLoadSuccess: function () {
                 $(this).tree('collapseAll');
                }
            });
            $('#JQTreeViewTeacher').tree({
                onlyLeafCheck: true,
                onCheck: function (node, checked) {
                    if (checked) $(node.target).addClass('tree-node-clicked');
                    else $(node.target).removeClass('tree-node-clicked');
                },
                onSelect: function (node) {
                },
                onLoadSuccess: function () {
                    //$(this).tree('collapseAll');
                }
            });
            //設定欄位Caption 變顏色
            var flagIDs = ['#dataFormMasterDispatchAreaID', '#dataFormMasterDispatchAreaManager'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: '#8A2BE2' });
            });
            //設定 Grid QueryColunm Windows width=480px
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel) {
               $(queryPanel).panel('resize', { width: 480 });
            }
            //將QUERY PANEL 按鈕置中
            $('.infosysbutton-q', '#dataGridView').closest('td').attr('align', 'middle');
          
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
           
            //
            var CanExamTime = $('#dataFormMasterCanExamTime').closest('td');
            CanExamTime.append(' 次')
            var CourseMinCount = $('#dataFormMasterCourseMinCount').closest('td');
            CourseMinCount.append(' 人')
            var CourseMaxCount = $('#dataFormMasterCourseMaxCount').closest('td');
            CourseMaxCount.append(' 人')
            var CourseFinishDays1 = $('#dataFormMasterCourseFinishDays1').closest('td');
            CourseFinishDays1.append(' 天')
            var CourseFinishDays2 = $('#dataFormMasterCourseFinishDays2').closest('td');
            CourseFinishDays2.append(' 天')
            var AdviseHours = $('#dataFormMasterAdviseHours').closest('td');
            AdviseHours.append(' 小時')
            var ReTrainHours = $('#dataFormMasterReTrainHours').closest('td');
            ReTrainHours.append(' 小時')
            var CourseFinishDays1 = $('#JQDataForm1CourseFinishDays1').closest('td');
            CourseFinishDays1.append(' 天')
            var CourseFinishDays2 = $('#JQDataForm1CourseFinishDays2').closest('td');
            CourseFinishDays2.append(' 天')
        })
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;'  />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        function dataFormMasterOnLoadSucess() {
            var node = $('#TreeView_Course').tree('getSelected');
            var CourseID = node.id;
            var CoursePath = GetCoursePath(CourseID);
            $('#dataFormMasterCoursePath').val(CoursePath);
            }
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridView') {
                var node = $('#TreeView_Course').tree('getSelected');
                var selectednodeid = node.id;
                var wherestr = [];
                var val = '';
                val = $('#CourseName_Query').val();
                if (val != '') {
                    wherestr.push("CourseName like '%" + val + "%'");
                }
                val = $('#CourseID_Query').val();
                if (val != '') {
                    wherestr.push("CourseID like '%" + val + "%'");
                }
                val = $('#SpreadType_Query').combobox("getValue");
                if (val != '') {
                    wherestr.push("SpreadType = " + val);
                }
                val = $('#CourseTestMethod_Query').combobox("getValue");
                if (val != '') {
                    wherestr.push("CourseTestMethod = " + val);
                }
                if (selectednodeid != '0') {
                    wherestr.push("CourseParentID=" + selectednodeid);
                }
                $(dg).datagrid('setWhere', wherestr.join(' and '));
            }
        }
        //將選擇的講師代號存入StudentsCourses
        function SetStudentsCourses(TeacherIDs) {
            var UserID = getClientInfo("UserID");
            var CourseID = $('#dataFormMasterCourseID').val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCourse.Course', //連接的Server端，command
                data: "mode=method&method=" + "SetStudentsCourses" + " &parameters=" + CourseID + "*" + TeacherIDs + "*" + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data == "True") {

                    }
                    else {
                        alert("存入失敗")
                    }
                }
            });
        }
        function TreeViewTeacherOnSubmited() {
            var nodes = $('#JQTreeViewTeacher').tree('getChecked');
            var TeacherIDs = '';
            var TeacherNames = '';
            var i = 1;
            $.each(nodes, function (index, node) {
                if (i > 1) {
                    TeacherIDs = TeacherIDs + ',' + node.id;
                    TeacherNames = TeacherNames + ',' + node.text;
                }
                else {
                    TeacherIDs = TeacherIDs + node.id;
                    TeacherNames = TeacherNames + node.text;
                }
                i = i + 1;
            });
            SetStudentsCourses(TeacherIDs);
            $('#dataFormMasterTeacherIDs').val(TeacherIDs);
            $('#dataFormMasterTeachers').val(TeacherNames);
          return true;
        }
        function TreeViewJobOnSubmited() {
            var nodes = $('#JQTreeViewJob').tree('getChecked');
            var JobIDs = '';
            var i = 1;
            $.each(nodes, function (index, node) {
                if (i > 1) { JobIDs = JobIDs + ',' + node.id;}
                else { JobIDs = JobIDs + node.id;}
                i = i + 1;
            });
            $('#JQDataForm1VirtureIDs').val(JobIDs);
            return true;
        }
        function JQTreeViewTeacherSetChecked(IDstr) {
            if (IDstr != '') {
                $.each(IDstr.split(","), function (i, id) {
                    var node = $('#JQTreeViewTeacher').tree('find', id);
                    if (node != null) {
                       $(node.target).addClass('tree-node-clicked');
                       $('#JQTreeViewTeacher').tree('check', node.target);
                    }
                });
            }
        }
    
        //檢查課程是否可刪除
        function dataGridViewOnDelete(data) {
            var today = new Date();
            var CourseID = data.CourseID;
            var Dt = $.jbjob.Date.DateFormat(today, 'yyyy/MM/dd');
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCourse.Course', //連接的Server端，command
                data: "mode=method&method=" + "CheckDelCourseID" + "&parameters=" + CourseID + "," + Dt, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        cnt = $.parseJSON(data);
                    }
                }
            });
            if ((cnt == "0") || (cnt == "undefined")) {
                return true;
            }
            else {
                alert('注意!!,此課程已被其他資料參照使用,無法刪除！！');
                return false;
            }
        }
        function dataGridViewOnSelect(rowindex, rowdata) {
            if (rowdata != null && rowdata != undefined) {
                var CourseID = rowdata.CourseID;
                $("#JQDataGrid1").datagrid('setWhere', "JOBNEEDCOURSE.CourseID="+"'"+ CourseID+"'");
            }
        }
        function GetCourseID() {
            var RowData = $("#dataGridView").datagrid('getSelected');
            var CourseID = RowData.CourseID;
            return CourseID;
        }
        function GetCourseName() {
            var RowData = $("#dataGridView").datagrid('getSelected');
            var CourseName = RowData.CourseName;
            return CourseName;
        }
        //取得傳入課程適用職能代號字串
        function GetCourseJobIDStr(CourseID) {
            var UserID = getClientInfo("UserID");
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCourse.Course', //連接的Server端，command
                data: "mode=method&method=" + "GetCourseJobIDStr" + "&parameters=" + CourseID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        ReturnStr = data;
                    }
                }
            });
            return ReturnStr;
        }
        //取取課程所在路徑字串
        function GetCoursePath(CourseID) {
            var UserID = getClientInfo("UserID");
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCourse.Course', //連接的Server端，command
                data: "mode=method&method=" + "GetCoursePath" + "&parameters=" + CourseID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        ReturnStr = data;
                    }
                }
            });
            return ReturnStr;
        }
        function JQDataForm1OnLoadSucess() {
            var dd = new Date();
            var CourseID = GetCourseID();
            var CourseName = GetCourseName();
            if (getEditMode($("#JQDataForm1")) != 'updated') {
                var FormName = '#JQDataForm1';
                var HideFieldName = ['JobID', 'JOBNAME'];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').hide();
                    $(FormName + fieldName).closest('td').hide();
                });
                $('#JQDataForm1CourseID').val(CourseID);
                $('#JQDataForm1COURSENAME').val(CourseName);
                $('#JQDataForm1StartDate').datebox('setValue', $.jbjob.Date.DateFormat(dd, 'yyyy/MM/dd'));
                $('#JQDataForm1EndDate').datebox('setValue','2079/01/01');
            }
            else {
                var FormName = '#JQDataForm1';
                var HideFieldName = ['JobID', 'JOBNAME'];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').show();
                    $(FormName + fieldName).closest('td').show();
                });
            }
        }
        function AddCourseJobID() {
            openForm('#JQDialog2', {}, "inserted", 'dialog');
        }
        function JQDataForm1OnApply() {
            if (getEditMode($("#JQDataForm1")) != 'updated') {
                var CourseID = $('#JQDataForm1CourseID').val();
                var JobIDs   = $('#JQDataForm1VirtureIDs').val();
                if ((JobIDs == null) || (JobIDs == '')) {
                    alert('注意!!,未選取職能,請選取');
                    return false;
                }
                var CourseFinishDays1 = $('#JQDataForm1CourseFinishDays1').val();
                if ((CourseFinishDays1 == null) || (CourseFinishDays1 == 0)) {
                    alert('注意!!,初訓完課不得為0,請輸入');
                    return false;
                }
                var CourseFrequency = $('#JQDataForm1CourseFrequency').combobox('getValue');
                var StartDate = $('#JQDataForm1StartDate').datebox('getValue');
                var EndDate = $('#JQDataForm1EndDate').datebox('getValue');
                var UserID = getClientInfo("UserID");
      
                //$.ajax({
                //    type: "POST",
                //    url: '../handler/jqDataHandle.ashx?RemoteName=sCourse.Course', //連接的Server端，command
                //    data: "mode=method&method=" + "procAddJobNeedCourseByJob" + " &parameters=" + CourseID + "*" + CourseFrequency + "*" + CourseFinishDays1 + "*" + CourseFinishDays2 + '*' + StartDate + '*' + EndDate + '*' + JobIDs + '*' + UserID, 
                //    cache: false,
                //    async: false,
                //    success: function (data) {
                //        if (data == "True") {
                //            $("#JQDataGrid1").datagrid('reload');
                //        }
                //        else {
                //            alert("加入職能失敗");
                //        }
                //    }
                //});
            };
        }
        function JQDataForm1OnApplied() {
            var CourseID = $('#JQDataForm1CourseID').val();
            var JobIDs = $('#JQDataForm1VirtureIDs').val();
            var CourseFinishDays1 = $('#JQDataForm1CourseFinishDays1').val();
            var CourseFinishDays2 = $('#JQDataForm1CourseFinishDays2').val();
            var CourseFrequency = $('#JQDataForm1CourseFrequency').combobox('getValue');
            var StartDate = $('#JQDataForm1StartDate').datebox('getValue');
            var EndDate = $('#JQDataForm1EndDate').datebox('getValue');
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCourse.Course', //連接的Server端，command
                data: "mode=method&method=" + "procAddJobNeedCourseByJob" + " &parameters=" + CourseID + "*" + CourseFrequency + "*" + CourseFinishDays1 + "*" + CourseFinishDays2 + '*' + StartDate + '*' + EndDate + '*' + JobIDs + '*' + UserID, 
                cache: false,
                async: false,
                success: function (data) {
                    if (data == "True") {
                        $("#JQDataGrid1").datagrid('reload');
                    }
                    else {
                        alert("加入職能失敗");
                    }
                }
            });
        }
        function GetCourseParentID() {
            var node = $('#TreeView_Course').tree('getSelected');
            return node.id;
        }
        function GetCourseFrequency() {
            var RowData = $("#dataGridView").datagrid('getSelected');
            var CourseFrequency = RowData.CourseFrequency;
            return CourseFrequency;
        }
        function GetCourseFinishDays1() {
            var RowData = $("#dataGridView").datagrid('getSelected');
            var CourseFinishDays1 = RowData.CourseFinishDays1;
            return CourseFinishDays1;
        }
        function GetCourseFinishDays2() {
            var RowData = $("#dataGridView").datagrid('getSelected');
            var CourseFinishDays2 = RowData.CourseFinishDays2;
            return CourseFinishDays2;
        }
        function dataFormMasterOnApplied() {
            if (getEditMode($("#dataFormMaster")) == 'updated') {
                var UserID = getClientInfo("UserID");
                var CourseID = $('#dataFormMasterCourseID').val();
                if (UpdateJobNeedCourse(CourseID,UserID) != 1) {
                    alert('注意!! 更新課程職能資訊失敗');
                    return false;
                }
                $("#JQDataGrid1").datagrid('reload');
            }
            return true;
        }
        //更新JobNeedCourse資訊
        function UpdateJobNeedCourse(CourseID, UserID) {
            var flag = 0;
            var CourseOpenStatus = 3; //結案
            var IsSendMail = 0;  //是否發送eMail 否
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCourse.Course',
                data: "mode=method&method=" + "procUpdateJobNeedCourse" + " &parameters=" + CourseID + "," + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data == 'True') {
                        flag = 1;
                    }
                    else {
                        alert("注意!! 更新課程適用職能失敗");
                    }
                }
            });
            return flag;
        }
  
    </script>
</head>
<body>
 <form id="form1" runat="server">
 <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
       <div id="Layout_Base" class="easyui-layout" style="height: 980px;">
            <div data-options="region:'west',split:true,border:false" title=" " style="width: 210px;">
                <div class="easyui-layout" style="height: 980px;">
                    <div data-options="region:'center',title:'',border:false" title="課程類別">
                        <JQTools:JQTreeView ID="TreeView_Course" runat="server" DataMember="CourseTree" idField="CourseID" parentField="CourseParentID" RemoteName="sCourse.CourseTree" textField="CourseName" Checkbox="False" DialogTitle="Dialog">
                            <Columns>
                                <JQTools:JQTreeViewColumn Caption="CourseID" FieldName="CourseID" NewLine="True" />
                            </Columns>
                            <Menutems>
                                <JQTools:JQTreeViewContextItem Class="infosysbutton-i" Icon="icon-add" OnClick="insertTreeNode" Text="Insert" />
                                <JQTools:JQTreeViewContextItem Class="infosysbutton-u" Icon="icon-edit" OnClick="updateTreeNode" Text="Update" />
                                <JQTools:JQTreeViewContextItem Class="infosysbutton-d" Icon="icon-remove" OnClick="removeTreeNode" Text="Delete" />
                            </Menutems>
                        </JQTools:JQTreeView>
                    </div>

                </div>
            </div>
            <div data-options="region:'center',title:'',border:false" title="">

                <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCourse.Course" runat="server" AutoApply="True"
                    DataMember="Course" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
                    Title="課程維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="220px" QueryMode="Window" QueryTop="50px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="930px" OnDelete="dataGridViewOnDelete" OnSelect="dataGridViewOnSelect">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="課程代號" Editor="text" FieldName="CourseID" Format="" MaxLength="0" Width="60" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="CourseName" Format="" MaxLength="0" Width="280" />
                        <JQTools:JQGridColumn Alignment="center" Caption="初訓期限(天)" Editor="numberbox" FieldName="CourseFinishDays1" Format="" Width="75" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="授課(hr)" Editor="numberbox" FieldName="AdviseHours" Format="" Width="50" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="授課方式" Editor="infocombobox" FieldName="CourseMethod" Format="" Width="110" Visible="True" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourse.ListCourseMethod',tableName:'ListCourseMethod',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="測驗方式" Editor="infocombobox" FieldName="CourseTestMethod" Format="" Width="80" Visible="True" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourse.ListTestMethod',tableName:'ListTestMethod',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="center" Caption="複訓頻率" Editor="infocombobox" FieldName="CourseFrequency" Format="" Width="55" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourse.CourseFrequency',tableName:'CourseFrequency',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="center" Caption="複訓期限(天)" Editor="numberbox" FieldName="CourseFinishDays2" Format="" Width="75" Visible="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="複訓(hr)" Editor="numberbox" FieldName="ReTrainHours" Format="" Width="50" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="開課季別" Editor="text" FieldName="OpenSeason" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="52" />
                        <JQTools:JQGridColumn Alignment="center" Caption="起始年度" Editor="text" FieldName="StartYear" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="52" />
                        <JQTools:JQGridColumn Alignment="center" Caption="有效課程" Editor="checkbox" FieldName="IsActive" Format="" Width="52" Visible="False" EditorOptions="on:1,off:0" FormatScript="genCheckBox" />
                        <JQTools:JQGridColumn Alignment="left" Caption="課程大綱" Editor="text" FieldName="CourseOutline" Format="" MaxLength="0" Width="300" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="適用對象" Editor="text" FieldName="CourseApplyTo" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseParentID" Editor="text" FieldName="CourseParentID" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="IsClass" Editor="text" FieldName="IsClass" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="IsMayOutTraining" Editor="text" FieldName="IsMayOutTraining" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="CourseCategory1" Editor="numberbox" FieldName="CourseCategory1" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="CourseCategory2" Editor="numberbox" FieldName="CourseCategory2" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="CanExamTime" Editor="numberbox" FieldName="CanExamTime" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="CourseMinCount" Editor="numberbox" FieldName="CourseMinCount" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="CourseMaxCount" Editor="numberbox" FieldName="CourseMaxCount" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="LicenceID" Editor="numberbox" FieldName="LicenceID" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="Description" Editor="text" FieldName="Description" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="IsProfileCourse" Editor="text" FieldName="IsProfileCourse" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="ckCourseMethod" Editor="text" FieldName="ckCourseMethod" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="ckCourseTestMethod" Editor="text" FieldName="ckCourseTestMethod" Format="" Width="120" Visible="False" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                            OnClick="insertItem" Text="新增課程" />
                        <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                            OnClick="openQuery" Text="查詢" />
                        <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="課程名稱" Condition="%%" DataType="string" Editor="text" FieldName="CourseName" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="120" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="課程代號" Condition="=" DataType="string" Editor="text" FieldName="CourseID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="計畫展開" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourse.SpreadType',tableName:'SpreadType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SpreadType" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="測驗方式" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourse.ListTestMethod',tableName:'ListTestMethod',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseTestMethod" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="180" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="課程維護" DialogLeft="220px" DialogTop="40px" Width="775px">
                    <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Course" HorizontalColumnsCount="1" RemoteName="sCourse.Course" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnLoadSuccess="dataFormMasterOnLoadSucess" OnApplied="dataFormMasterOnApplied">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="目前路徑" Editor="text" FieldName="CoursePath" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="500" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課程代號" Editor="text" FieldName="CourseID" Format="" MaxLength="0" Width="100" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="CourseName" Format="" MaxLength="0" Width="500" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課程大綱" Editor="textarea" FieldName="CourseOutline" Format="" Width="500" EditorOptions="height:45" MaxLength="0" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="適用對象" Editor="textarea" EditorOptions="height:30" FieldName="CourseApplyTo" Format="" MaxLength="0" Width="500" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="授課老師" Editor="textarea" EditorOptions="height:30" FieldName="Teachers" MaxLength="0" Width="500" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課程主類" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sCourse.CourseCategory1',tableName:'CourseCategory1',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseCategory1" Format="" Width="240" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課程次類" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sCourse.CourseCategory2',tableName:'CourseCategory2',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseCategory2" Format="" Width="240" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="上課方式" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourse.ListCourseMethod',tableName:'ListCourseMethod',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseMethod" Format="" Width="240" />
                            <JQTools:JQFormColumn Alignment="left" Caption="測驗方式" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourse.ListTestMethod',tableName:'ListTestMethod',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseTestMethod" Format="" Width="240" />
                            <JQTools:JQFormColumn Alignment="left" Caption="測驗次數" Editor="numberbox" FieldName="CanExamTime" Format="" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="人數下限" Editor="numberbox" FieldName="CourseMinCount" Format="" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="人數上限" Editor="numberbox" FieldName="CourseMaxCount" Format="" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="初訓完訓" Editor="numberbox" FieldName="CourseFinishDays1" Format="" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="複訓完訓" Editor="numberbox" FieldName="CourseFinishDays2" Format="" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="標準時數" Editor="numberbox" FieldName="AdviseHours" Format="" Width="80" EditorOptions="precision:1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="複訓時數" Editor="numberbox" FieldName="ReTrainHours" Format="" Width="80" EditorOptions="precision:1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="複訓頻率" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourse.CourseFrequency',tableName:'CourseFrequency',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseFrequency" Format="" Width="83" MaxLength="0" />
                            <JQTools:JQFormColumn Alignment="left" Caption="開課季別" Editor="infocombobox" EditorOptions="valueField:'Season',textField:'Season',remoteName:'sCourse.OpenSeason',tableName:'OpenSeason',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="OpenSeason" Width="83" MaxLength="0" />
                            <JQTools:JQFormColumn Alignment="left" Caption="起始年度" Editor="infocombobox" FieldName="StartYear" Width="83" EditorOptions="valueField:'YEARNO',textField:'YEARNO',remoteName:'sCourse.YearNO',tableName:'YearNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課程認證" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'LicenceName',remoteName:'sCourse.Licence',tableName:'Licence',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LicenceID" Format="" Width="300" />
                            <JQTools:JQFormColumn Alignment="left" Caption="外訓申請" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsMayOutTraining" Format="" Width="100" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ProFile課程" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsProfileCourse" Format="" Width="100" />
                            <JQTools:JQFormColumn Alignment="left" Caption="有效課程" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Format="" Width="100" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="計畫展開" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourse.SpreadType',tableName:'SpreadType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SpreadType" Width="300" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="相關備註" Editor="text" FieldName="Description" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="500" />
                            <JQTools:JQFormColumn Alignment="left" Caption="IsClass" Editor="text" FieldName="IsClass" Format="" Visible="False" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CourseParentID" Editor="text" FieldName="CourseParentID" Format="" Visible="False" Width="180" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ckCourseMethod" Editor="text" FieldName="ckCourseMethod" Format="" Width="180" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ckCourseTestMethod" Editor="text" FieldName="ckCourseTestMethod" Format="" Width="180" Visible="False" MaxLength="0" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Visible="False" Width="180" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Visible="False" Width="180" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="TeacherIDs" Editor="text" FieldName="TeacherIDs" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                        <Columns>
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCourseParentID" FieldName="CourseParentID" RemoteMethod="False" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsActive" RemoteMethod="True" />
                        </Columns>
                    </JQTools:JQDefault>
                    <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                    </JQTools:JQValidate>
                 </JQTools:JQDialog>
                 <JQTools:JQDialog ID="JQDialog3" runat="server" DialogLeft="30px" DialogTop="210px" Title="選取講師" Width="450px" OnSubmited="TreeViewTeacherOnSubmited">
                  <div class="easyui-layout" style="height: 480px;">
                    <div data-options="region:'center',title:'',border:false" title="">
                 <JQTools:JQTreeView ID="JQTreeViewTeacher" runat="server" DataMember="TeacherTree" idField="ID" parentField="ParentID" RemoteName="sCourse.TeacherTree" textField="Name" Checkbox="True"></JQTools:JQTreeView>
                    </div>
                    </div>
                 </JQTools:JQDialog>
                 <JQTools:JQDialog ID="JQDialog4" runat="server" DialogLeft="30px" DialogTop="50px" Title="選取職能" Width="450px" OnSubmited="TreeViewJobOnSubmited">
                  <div class="easyui-layout" style="height: 480px;">
                    <div data-options="region:'center',title:'',border:false" title="">
                 <JQTools:JQTreeView ID="JQTreeViewJob" runat="server" DataMember="JobTree" idField="JobID" parentField="JobParentID" RemoteName="sCourse.JobTree" textField="JobName" Checkbox="True"></JQTools:JQTreeView>
                    </div>
                    </div>
                 </JQTools:JQDialog>
                <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" AutoApply="True" 
                CheckOnSelect="True" ColumnsHibeable="False" DataMember="JobNeedCourse" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" 
                EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="20" Pagination="True" QueryAutoColumn="False" 
                QueryLeft="30px" QueryMode="Window" QueryTitle="查詢" QueryTop="150px" RecordLock="False" RecordLockMode="None" 
                RemoteName="sCourse.JobNeedCourse" Title="課程適用職能" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" EditDialogID="JQDialog2" Width="930px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="職能代號" Editor="text" FieldName="JobID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職能名稱" Editor="text" FieldName="JOBNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180" />
                    <JQTools:JQGridColumn Alignment="center" Caption="複訓頻率" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourse.CourseFrequency',tableName:'CourseFrequency',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseFrequency" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="初訓完課" Editor="text" FieldName="CourseFinishDays1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="複訓完課" Editor="text" FieldName="CourseFinishDays2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="StartDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="失效日期" Editor="datebox" FieldName="EndDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="AddCourseJobID" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
 
            </JQTools:JQDataGrid>
             <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="JQDataForm1" Title="課程適用職能維護" Width="600px" DialogLeft="220px" DialogTop="40px">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="JobNeedCourse" HorizontalColumnsCount="1" RemoteName="sCourse.JobNeedCourse" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApply="JQDataForm1OnApply" OnLoadSuccess="JQDataForm1OnLoadSucess" OnApplied="JQDataForm1OnApplied" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="課程代號" Editor="text" FieldName="CourseID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="COURSENAME" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職能代號" Editor="text" FieldName="JobID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職能名稱" Editor="text" FieldName="JOBNAME" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="複訓頻率" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourse.CourseFrequency',tableName:'CourseFrequency',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseFrequency" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="85" />
                        <JQTools:JQFormColumn Alignment="left" Caption="初訓完課" Editor="text" FieldName="CourseFinishDays1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="複訓完課" Editor="text" FieldName="CourseFinishDays2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="StartDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="失效日期" Editor="datebox" FieldName="EndDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="VirtureIDs" Editor="text" FieldName="VirtureIDs" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="300" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCourseFrequency" FieldName="CourseFrequency" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCourseFinishDays1" FieldName="CourseFinishDays1" RemoteMethod="False"  />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCourseFinishDays2" FieldName="CourseFinishDays2" RemoteMethod="False"  />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="StartDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2079/01/01" FieldName="EndDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="CourseID" RemoteMethod="False" DefaultMethod="GetCourseID" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCourseName" FieldName="COURSENAME" RemoteMethod="False" />
                    
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    <Columns>

                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
    
                 
            </div>
   
           
        </div>  
 
  

   
    </form>
</body>
</html>
