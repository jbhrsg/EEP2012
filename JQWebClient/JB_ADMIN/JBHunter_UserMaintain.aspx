﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_UserMaintain.aspx.cs" Inherits="Template_JQueryQuery1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script src="../js/jquery.url.js"></script>
    <script>
        
        $(document).ready(function () {
            var LoginID = getClientInfo("UserID");
            $("[id$='hyAddResume']").attr("href", "../../NewHunter/index.html?HunterID=" + LoginID);
        });
        
        $(document).ready(function () {
            //工作部門,職稱加上ToolTip
            $('#DutyDept_Query').attr('title', '請輸入「工作部門」關鍵字範例「品保,研發,稽核....」');
            $('#DutyTitle_Query').attr('title', '請輸入「工作職稱」關鍵字範例「消防,製程,業務副總....」');                
            
          
            
            //以下為combotree----產業別    
            $('#IndustryID_Query').jbCombobox2tree({
                parentField: 'ParentID'
            });
            //以下為combotree----學類
            $('#EduSubject1_Query').jbCombobox2tree({
                parentField: 'ParentID'
            });
            //產業別預設不拘
            $("#IndustryID_Query").combobox('setValue', 0);
            //最高學類預設不拘
            $("#EduSubject1_Query").combobox('setValue', 0);
           
        });
        //-------------------------------簡易查詢條件-----------------------------------------------------------
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridView') {
                var where = $(dg).datagrid('getWhere');
                //查詢字串取代
                if (where.length > 0) {
                    //起始年齡	
                    where = where.replace("u.CreateDate", "DateDiff(Year,u.BirthDay,GetDate())");
                    //終止年齡
                    where = where.replace("AssignName", "DateDiff(Year,u.BirthDay,GetDate())");
                    //履歷有效天數
                    var dt = new Date();
                    var edate = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd');
                    //往前推?天(看前端輸入值)        
                    var LastUpdateDate = $("#LastUpdateDate_Query").val();
                    var aDate = new Date($.jbDateAdd('days', -LastUpdateDate, dt));
                    var adate = $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
                    where = where.replace("LastUpdateDate<='" + LastUpdateDate + "'", "Convert(nvarchar(10),u.CreateDate,111) between '" + adate + "' and '" + edate + "'");

                    //最高需求學類(包含以下階層)
                    var EduSubject = $("#EduSubject1_Query").combobox('getValue');
                    //學類不拘過濾                   
                    where = where.replace(" and EduSubject1=" + EduSubject, "");//前面有條件時
                    where = where.replace("EduSubject1=" + EduSubject, "");//前面無條件時
                    if (EduSubject != '0' && EduSubject != null) {
                        where = where + " and ( EduSubject1 in ( Select ID from dbo.funReturnEduSubjectChildNodes(" + EduSubject + ")))"; //+" or u.EduSubject2 =" + EduSubject + " or u.EduSubject3 =" + EduSubject + ")";
                    }

                    //產業別(包含以下階層)
                    var IndustryType = $("#IndustryID_Query").combobox('getValue');
                    //產業別不拘過濾
                    where = where.replace(" and IndustryID=" + IndustryType, "");//前面有條件時
                    where = where.replace("IndustryID=" + IndustryType, "");//前面無條件時
                    if (IndustryType != '0' && IndustryType != null) {
                        where = where + " and ( c.IndustryID in ( Select ID from dbo.funReturnIndCategoryChildNodes(" + IndustryType + ")))";
                    }

                    //工作部門(以逗號分割)
                    var DutyDept = $("#DutyDept_Query").val();
                    if (DutyDept != '') {
                        var arrD = DutyDept.split(",");
                        var arr = "";
                        $.each(arrD, function (i, val) {
                            arr = "DutyDept like '%" + val + "%' or " + arr;
                        });
                        where = where.replace("DutyDept='" + DutyDept + "'", '(' + arr.substr(0, arr.length - 3) + ')');
                    }
                    //工作職稱(以逗號分割)
                    var DutyTitle = $("#DutyTitle_Query").val();
                    if (DutyTitle != '') {
                        var arrT = DutyTitle.split(",");
                        var arr2 = "";
                        $.each(arrT, function (i, val) {
                            arr2 = "DutyTitle like '%" + val + "%' or " + arr2;
                        });
                        where = where.replace("DutyTitle='" + DutyTitle + "'", '(' + arr2.substr(0, arr2.length - 3) + ')');
                    }
                }
                if (where.substr(0, 5) == ' and ') {
                    where = where.substr(5, where.length);
                }
                $(dg).datagrid('setWhere', where);
                //$("#JQDialog31").datagrid('getPanel').panel('expand'); //展開
                // $(dg).datagrid('getPanel').panel('collapse');将grid的panel缩回
                //$("#datagrid-row-r1-1-0").find('td').eq(2).text().trim();
                //alert($("#payment_currency_text tr.find(.formdata)").val());
            }
        }
        //Grid開啟履歷連結       
        function OpenUser(val, row) {
            var EnUserID = EnCodeMethod(row.UserID);
            var LoginID = getClientInfo("UserID");
            return $('<a>', { href: '#', onclick: 'window.open("../../NewHunter/index.html?POS=' + EnUserID + '&HunterID=' + LoginID + '","履歷資料維護");', theData: row.UserID }).linkbutton({ text: "<b><div style=\"color:Blue\">" + val + "</div></b>", plain: true })[0].outerHTML;
        }
        //加密
        var s;
        function EnCodeMethod(UserID) {
            $.ajax({
                type: "POST",
                url: '../handler/UserPaw.ashx',
                data: { UserID: UserID },
                async: false,//非同步
                success: function (data) {
                    s = data;
                }
            });
            return s;
        }
        //得日期YYYMMDD
        function GetDate() {
            var date = new Date();
            ///date.format("dd/MM/yyyy");            
            var year = date.getYear() + 1900;
            var month = date.getMonth() + 1;
            var day = date.getDate();
            var now = year + "" + FullString(month) + "" + FullString(day);
            return now;
        }
        //判斷日期月,日是否為2位數,否則填滿
        function FullString(s) {
            if (s > 9) {
                return s
            } else return '0' + '' + s
        }
        // 推薦紀錄維護 Call Method
        function AddMenu() {
            if ($("#dataGridView").datagrid('getSelections').length == 0) {
                alert('請先選擇人才。');
            } else {
                //$('#dataFormMaster').attr('dialogGrid', '#dataGridView');
                var row = $('#dataGridView').datagrid('getSelected');
                openForm('#JQDialog1', row, "", 'dialog');
                $("#dataGridDetail").datagrid('setWhere', "n.UserID='" + row.UserID + "'");
                //return $('<a>', { href: '#', onclick: 'AssignLogs(this)', theData: row.Name, theData2: row.UserID }).linkbutton({ text: "<b><div style=\"color:Red\">" + val + "</div></b>", plain: true })[0].outerHTML;
            }
        }
        ////新增人才履歷 Call Method
        //function OpenResume() {
        //    var LoginID = getClientInfo("UserID");
        //    window.open('../../NewHunter/index.html?HunterID=' + LoginID, '新增履歷資料');
        //}
        //搜尋結果Grid 推薦履歷 => 開啟推薦履歷連結       
        function OpenResume(val, row) {
            var EnUserID = EnCodeMethod(row.UserID);
            var LoginID = getClientInfo("UserID");
            return $('<a>', { href: "#", onclick: "myclick(" + sUserID + ")", theData: row.UserID }).linkbutton({ text: "<img src=img/Resume.png></a><b><div style=\"color:Red\">" + val + "</div></b>", plain: true })[0].outerHTML;

        }
        function myclick() {
            var LoginID = getClientInfo("UserID");
            window.open('../../NewHunter/index.html?HunterID=' + LoginID, '推薦履歷');
        }
        //搜尋結果Grid 面談紀錄維護 
        function OpenInterview(val, row) {
            return $('<a>', { href: '#', onclick: 'InterviewFunction(this)', theData: row.UserID }).linkbutton({ text: "<b><div style=\"color:Red\">" + val + "</div></b>", plain: true })[0].outerHTML;
        }
        var InterviewFunction = function (Target) {
            $("#JQDialog6").dialog("open");
            var UserID = $(Target).attr('theData');
            $("#dataGridInterview").datagrid('setWhere', "UserID='" + UserID + "'");
        }
        
        //-------------------------------人才推薦紀錄-Dialog--------------------------------------------------------------------
        //求得UserID 
        function GetUserID() {
            return $("#dataFormMasterUserID").val();
        }
        //function GetJobID() {
        //    return $("#dataFormMasterJobID").val();
        //}
        //求得CreateDate
        function GetCreateDate() {
            var date = new Date();
            var year = date.getYear() + 1900;
            var month = date.getMonth() + 1;
            var day = date.getDate();
            var hour = date.getHours();
            var minute = date.getMinutes();
            var second = date.getSeconds();
            var now = year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second;
            return now;
        }
        //新增推薦紀錄後更新2個Grid
        function RefreshGrid() {
            $('#dataGridDetail').datagrid('reload');
            //$('#dataGridView').datagrid('reload');
            //queryGrid($("#dataGridView"));
        }

        //刪除控制=>刪除最新一筆        
        function DeleteRow(rowData) {
            if (rowData.ROW_NUMBER == 1) {
                var pre = confirm("確定移除最新推薦紀錄?");
                if (pre == true) {
                    //callServerMethod
                    var row = $('#dataGridDetail').datagrid('getSelected');
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sSearchFunction.JobAssignLogs',
                        data: "mode=method&method=" + "userDEL" + "&parameters=" + rowData.AssignNO + "," + rowData.UserID + "," + rowData.JobID,
                        cache: false,
                        async: true,
                    });
                    RefreshGrid();
                    return false; //取消刪除的動作                     
                }
                else {
                    return false;
                }
            }
            alert('不是最後一筆資料,不可刪除。')
            return false;
        }

      
      
    </script>

    <style type="text/css">
        .auto-style1
        {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
        
                    <table style="width: 100%;">                       
                        <tr>
                            <td colspan="3" class="auto-style1">                                
                                &nbsp;&nbsp;
                                <asp:HyperLink ID="hyAddResume" runat="server" ImageUrl="~/JB_Hunter/img/AddResume.png" Target="_blank" ToolTip="新增人才履歷" ></asp:HyperLink>

                                <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="新增人才履歷"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="auto-style1">                                
                                <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sSearchFunction.HUT_User" runat="server" AutoApply="True"
                                    DataMember="HUT_User" Pagination="True" QueryTitle="人才搜尋條件" EditDialogID="JQDialog3"
                                    Title="搜尋結果" AlwaysClose="True" OnLoadSuccess="queryGrid" QueryMode="Panel" DeleteCommandVisible="False" ViewCommandVisible="False" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True">
                                    <Columns>
                                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="Name" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="110" FormatScript="OpenUser" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="面談履歷預覽" Editor="text" FieldName="OpenResume" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="75" FormatScript="OpenResume" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="面談紀錄" Editor="text" FieldName="iInterview" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="60" FormatScript="OpenInterview" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="更新日期" Editor="text" FieldName="LastUpdateDate" Format="yyyy/mm/dd" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="獵才顧問" Editor="text" FieldName="HunterName" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="最高學歷" Editor="text" FieldName="sEdu" MaxLength="0" ReadOnly="False" Visible="True" Width="210" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="最近經歷" Editor="text" FieldName="sCareer" MaxLength="0" ReadOnly="True" Visible="True" Width="280" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="黑名單" Editor="text" EditorOptions="on:1,off:0" FieldName="NotMatch" Format="L-checkbox" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40" />
                                        <JQTools:JQGridColumn Alignment="center" Caption="照片" Editor="text" FieldName="PhotoFile" Format="Image,Folder:../NewHunter,Height:30" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" FormatScript="" />

                                    </Columns>
                                    <QueryColumns>
                                        <%--<JQTools:JQQueryColumn Caption="科系名稱" Condition="%%" DataType="string" Editor="text" FieldName="Department1" NewLine="False" RemoteMethod="False" Width="125" />--%>

                                        <JQTools:JQQueryColumn AndOr="and" Caption="人才姓名" Condition="%%" DataType="string" Editor="text" FieldName="NameC" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="130" />
                                        <JQTools:JQQueryColumn Caption="性別" Condition="=" DataType="number" Editor="infocombobox" EditorOptions="items:[{value:'',text:'不拘',selected:'true'},{value:'1',text:'男',selected:'false'},{value:'0',text:'女',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="u.Gender" NewLine="False" RemoteMethod="False" Width="125" />
                                        <JQTools:JQQueryColumn AndOr="and" Caption="起始年齡" Condition="&gt;=" DataType="number" Editor="numberbox" FieldName="u.CreateDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="80" />

                                        <JQTools:JQQueryColumn AndOr="and" Caption="終止年齡" Condition="&lt;=" DataType="number" Editor="numberbox" FieldName="AssignName" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="80" />

                                        <JQTools:JQQueryColumn Caption="教育程度" Condition="&gt;=" DataType="number" Editor="infocombobox" FieldName="EduID1" NewLine="True" RemoteMethod="False" Width="130" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'sReferences.HUT_ZEduLevel',tableName:'HUT_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQQueryColumn Caption="最高學類" Condition="=" DataType="number" Editor="infocombobox" FieldName="EduSubject1" NewLine="False" RemoteMethod="False" Width="170" EditorOptions="valueField:'ID',textField:'SubjectName',remoteName:'sSearchFunction.HUT_EduSubject',tableName:'HUT_EduSubject',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQQueryColumn AndOr="and" Caption="最高科系" Condition="%%" DataType="string" Editor="text" FieldName="u.Department1" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="170" />
                                        <JQTools:JQQueryColumn AndOr="and" Caption="產業別" Condition="=" DataType="number" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'IndCategory',remoteName:'sSearchFunction.HUT_IndCategory',tableName:'HUT_IndCategory',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IndustryID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="170" />

                                        <JQTools:JQQueryColumn AndOr="and" Caption="工作部門" Condition="=" DataType="string" Editor="text" FieldName="DutyDept" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="170" />
                                        <JQTools:JQQueryColumn AndOr="and" Caption="工作職稱" Condition="=" DataType="string" Editor="text" FieldName="DutyTitle" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="170" />

                                        <JQTools:JQQueryColumn AndOr="and" Caption="履歷有效天數" Condition="&lt;=" DataType="string" Editor="text" FieldName="LastUpdateDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="85" DefaultValue="" DefaultMethod="QueryDefault" />

                                        <JQTools:JQQueryColumn AndOr="and" Caption="獵才顧問" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sSearchFunction.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ServicePerson" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />

                                        <JQTools:JQQueryColumn AndOr="and" Caption="履歷效期" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'有效',selected:'true'},{value:'0',text:'無效',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsActive" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                                    </QueryColumns>
                                    <TooItems>                    
<%--                                        <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="快速查詢"  Visible="True" />                   --%>
                                    </TooItems>
                                    <QueryColumns>
                                    </QueryColumns>
                                </JQTools:JQDataGrid>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <JQTools:JQDialog ID="JQDialog3" runat="server" BindingObjectID="dataFormResult" Closed="True" EditMode="Expand" ShowModal="False" Title="人才資訊" ShowSubmitDiv="False" Width="800px">
                                    <JQTools:JQDataForm ID="dataFormResult" runat="server" Closed="False" ContinueAdd="False" DataMember="QueryResult" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="2" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataGridView" RemoteName="sSearchFunction.HUT_User" ShowApplyButton="False" ValidateStyle="Hint" Width="100%">
                                        <Columns>
                                            <JQTools:JQFormColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" MaxLength="0" ReadOnly="True" Width="100" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="居住地址" Editor="text" FieldName="Address1" MaxLength="20" ReadOnly="True" Visible="True" Width="350" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="MobileNo1" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="100" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="eMail " Editor="text" FieldName="eMail1" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="350" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="最新履歷狀態" Editor="text" FieldName="AssignName" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="100" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="最新推薦資訊" Editor="text" FieldName="LastStatus" MaxLength="0" ReadOnly="True" Visible="True" Width="350" NewRow="False" />
                                        </Columns>
                                        <RelationColumns>
                                            <JQTools:JQRelationColumn FieldName="UserID" ParentFieldName="UserID" />
                                        </RelationColumns>
                                    </JQTools:JQDataForm>
                                </JQTools:JQDialog>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>              

                                <JQTools:JQDialog ID="JQDialog6" runat="server" BindingObjectID="" DialogLeft="10px" DialogTop="10px" EditMode="Dialog" Height="330px" HorizontalAlign="Justify" Title="面談紀錄" Width="620px" ShowSubmitDiv="False">
                                    <JQTools:JQDataGrid ID="dataGridInterview" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="False" CheckOnSelect="True" ColumnsHibeable="False" data-options="pagination:true,view:commandview" DataMember="HUT_UserInterview" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="DialogInterview" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sJobQuery.HUT_UserInterview" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                                        <Columns>
                                            <JQTools:JQGridColumn Alignment="left" Caption="面談紀錄內容" Editor="textarea" FieldName="InterviewContent" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="500" Format="" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                        </Columns>
                                        <TooItems>
                                            <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                                        </TooItems>
                                    </JQTools:JQDataGrid>
                                    <JQTools:JQDialog ID="DialogInterview" runat="server" BindingObjectID="dataFormInterview" EditMode="Switch" Title="">
                                        <JQTools:JQDataForm ID="dataFormInterview" runat="server" Closed="False" ContinueAdd="False" DataMember="HUT_UserInterview" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="2" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sJobQuery.HUT_UserInterview" ShowApplyButton="False" ValidateStyle="Hint">
                                            <Columns>
                                                <JQTools:JQFormColumn Alignment="left" Caption="面談紀錄" Editor="textarea" FieldName="InterviewContent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="5" Span="1" Visible="True" Width="500" EditorOptions="height:200" Format="" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                            </Columns>
                                        </JQTools:JQDataForm>
                                    </JQTools:JQDialog>
                                </JQTools:JQDialog>
        <%--<JQTools:JQQueryColumn Caption="科系名稱" Condition="%%" DataType="string" Editor="text" FieldName="Department1" NewLine="False" RemoteMethod="False" Width="125" />--%>           

    </form>
</body>
</html>
