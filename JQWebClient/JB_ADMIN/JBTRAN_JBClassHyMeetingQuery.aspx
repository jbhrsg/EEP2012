<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBTRAN_JBClassHyMeetingQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
     <script type="text/javascript">
         $(document).ready(function () {
             //將Focus 欄位背景顏色改為黃色
             $(function () {
                 $("input, select, textarea").focus(function () {
                     $(this).css("background-color", "yellow");
                 });
                 $("input, select, textarea").blur(function () {
                     $(this).css("background-color", "white");
                 });
             });

         });
         </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sJBCourseQuery.JBClassStudentHyMeeting" runat="server" AutoApply="True"
                DataMember="JBClassStudentHyMeeting" Pagination="True" QueryTitle="人資活動課程查詢" EditDialogID="JQDialog1"
                Title="" AlwaysClose="True" QueryMode="Fuzzy" Width="1300px" PageSize="20" AllowAdd="True" AllowDelete="False" AllowUpdate="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="descr" Format="" MaxLength="0" Width="160" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="company" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="參加員" Editor="text" FieldName="name" Format="" MaxLength="0" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="性別" Editor="text" FieldName="sex" Format="" MaxLength="0" Width="30" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="department" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職稱" Editor="text" FieldName="occupation" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="通知方式" Editor="text" FieldName="inform" Format="" MaxLength="0" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="telephone" Format="" MaxLength="0" Width="105" />
                    <JQTools:JQGridColumn Alignment="left" Caption="手機" Editor="text" FieldName="fax" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="address" Format="" MaxLength="0" Width="100" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="信箱" Editor="text" FieldName="mail" Format="" MaxLength="0" Width="150" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="報名日期" Editor="datebox" FieldName="key_date" Format="yyyy/mm/dd hh:MM:ss" Width="110" Visible="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="日期" Editor="datebox" FieldName="cosdate" Format="yyyy/mm/dd" Width="70" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="學歷" Editor="text" FieldName="education" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="報名場次" Editor="text" FieldName="class" Format="" MaxLength="0" Width="60" Visible="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="會員" Editor="text" FieldName="member" Format="" MaxLength="0" Width="50" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="繳費方式" Editor="text" FieldName="method" Format="" MaxLength="0" Width="70" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="會員編號" Editor="text" FieldName="number" Format="" MaxLength="0" Width="70" Visible="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="id" Editor="numberbox" FieldName="id" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="coscode" Editor="text" FieldName="coscode" Format="" MaxLength="0" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                  <%--  <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />--%>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出EXCEL" />
                </TooItems>
                <QueryColumns>
                     <JQTools:JQQueryColumn AndOr="and" Caption="課程名稱" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'COSCODE',textField:'COURSENAME',remoteName:'sJBCourseQuery.JBClass',tableName:'JBClass',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="coscode" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="200" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="人資活動課程" DialogLeft="200px" DialogTop="110px" Width="450px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="JBClassStudentHyMeeting" HorizontalColumnsCount="2" RemoteName="sJBCourseQuery.JBClassStudentHyMeeting" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="descr" Format="" Width="280" ReadOnly="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="text" FieldName="name" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infocombobox" EditorOptions="items:[{value:'男',text:'男',selected:'false'},{value:'女',text:'女',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="sex" Format="" maxlength="0" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會員" Editor="text" FieldName="member" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會員編號" Editor="text" FieldName="number" Format="" maxlength="0" Width="125" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電子郵件" Editor="text" FieldName="mail" Format="" maxlength="0" Span="2" Width="280" />
                        <JQTools:JQFormColumn Alignment="left" Caption="通訊地址" Editor="text" FieldName="address" Format="" maxlength="0" Span="2" Width="280" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡電話" Editor="text" FieldName="telephone" Format="" maxlength="0" Span="2" Width="280" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳真" Editor="text" FieldName="fax" Format="" maxlength="0" Span="2" Width="280" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司" Editor="text" FieldName="company" Format="" maxlength="0" Span="2" Width="280" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="occupation" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門" Editor="text" FieldName="department" Format="" maxlength="0" Width="125" />
                        <JQTools:JQFormColumn Alignment="left" Caption="學歷" Editor="text" FieldName="education" Format="" maxlength="0" Span="2" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報名場次" Editor="text" FieldName="class" Format="" maxlength="0" Span="2" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="繳費方式" Editor="text" FieldName="method" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="通知方式" Editor="text" FieldName="inform" Format="" maxlength="0" Span="2" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="id" Editor="numberbox" FieldName="id" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="coscode" Editor="text" FieldName="coscode" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="cosdate" Editor="datebox" FieldName="cosdate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="key_date" Editor="datebox" FieldName="key_date" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
