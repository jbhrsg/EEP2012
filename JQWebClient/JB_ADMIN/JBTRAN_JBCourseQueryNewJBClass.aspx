<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBTRAN_JBCourseQueryNewJBClass.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sJBCourseQuery.JBClassStudentList1" runat="server" AutoApply="True"
                DataMember="JBClassStudentList1" Pagination="True" QueryTitle="公開班課程查詢"
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Fuzzy" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="1200px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="descr" Format="" MaxLength="0" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="company" Format="" MaxLength="0" Width="140" />
                    <JQTools:JQGridColumn Alignment="left" Caption="參加員" Editor="text" FieldName="name" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="department" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職稱" Editor="text" FieldName="occupation" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="telephone" Format="" MaxLength="0" Width="105" />
                    <JQTools:JQGridColumn Alignment="left" Caption="傳真" Editor="text" FieldName="fax" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="信箱" Editor="text" FieldName="mail" Format="" MaxLength="0" Width="150" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="統編" Editor="text" FieldName="education" Format="" MaxLength="0" Width="70" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="報名日期" Editor="datebox" FieldName="key_date" Format="yyyy/mm/dd" Width="70" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="繳費方式" Editor="text" FieldName="method" Format="" MaxLength="0" Width="60" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="通知方式" Editor="text" FieldName="inform" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="會員" Editor="text" FieldName="member" Format="" MaxLength="0" Width="50" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="address" Format="" MaxLength="0" Width="100" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="日期" Editor="datebox" FieldName="cosdate" Format="yyyy/mm/dd" Width="70" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="sex" Editor="text" FieldName="sex" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="class" Editor="text" FieldName="class" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="number" Editor="text" FieldName="number" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="id" Editor="numberbox" FieldName="id" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="coscode" Editor="text" FieldName="coscode" Format="" MaxLength="0" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                    <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出EXCEL" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="課程名稱" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'COSCODE',textField:'COURSENAME',remoteName:'sJBCourseQuery.JBClassNewClass',tableName:'JBClassNewClass',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="coscode" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="300" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
    <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="defaultMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="validateMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQValidate>
</form>
</body>
</html>
