<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON_Query_Contact.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>全文檢索</title>
</head>
<body>
    <script type="text/javascript">
        //---------------------------------------改寫查詢-----------------------------------------
        function queryGrid(dg) {
            var where = ""
            var userID = getClientInfo("UserID");
            var fulltext = $(dg).datagrid('getWhere');
            if (fulltext != "") {
                fulltext = fulltext.replace("CENTER_CNAME like ", "");
                where = where + "(CENTER_CNAME like " + fulltext + "\r\n";
                where = where + "or CENTER_ENAME like " + fulltext + "\r\n";
                where = where + "or CONTACT_NAME like " + fulltext + "\r\n";
                where = where + "or CONTACT_JOB like " + fulltext + "\r\n";
                where = where + "or CONTACT_TEL like " + fulltext + "\r\n";
                where = where + "or CONTACT_CELLPHONE like " + fulltext + "\r\n";
                where = where + "or CONTACT_EMAIL like " + fulltext + "\r\n";
                where = where + "or CONTACT_ADDR like " + fulltext + "\r\n";
                where = where + "or CONTACT_MEMO like " + fulltext + "\r\n";
                where = where + "or CONTACT_MEMO1 like " + fulltext + "\r\n";
                where = where + "or CONTACT_MEMO2 like " + fulltext + "\r\n";
                where = where + "or CONTACT_AREA_NAME like " + fulltext + "\r\n";
                where = where + "or CONTACT_TYPE_NAME like " + fulltext + "\r\n";
                where = where + "or CONTACT_TERRITORY_NAME like " + fulltext + "\r\n";
                where = where + "or CONTACT_MEMO3 like " + fulltext + "\r\n";
                where = where + "or CONTACT_SKILL_NAME like " + fulltext + "\r\n";
                where = where + "or CONTACT_HOBBY_NAME like " + fulltext + "\r\n";
                where = where + "or ACTIVITY_NAME like " + fulltext + "\r\n";
                where = where + "or ACTIVITY_YEAR like " + fulltext + "\r\n";
                where = where + "or ACTIVITY_ADDR like " + fulltext + "\r\n";
                where = where + "or ACTIVITY_PERSON like " + fulltext + "\r\n";
                where = where + "or ACTIVITY_WORKS like " + fulltext + "\r\n";
                where = where + "or ACTIVITY_DESCRIPTION like " + fulltext + "\r\n";
                where = where + "or ACTIVITY_TYPE_NAME like " + fulltext + "\r\n";
                where = where + "or ACTIVITY_STATUS_NAME like " + fulltext + "\r\n";
                where = where + "or ACTIVITY_LEVEL_NAME like " + fulltext + "\r\n";
                where = where + "or ACTIVITY_IDENTITY_NAME like " + fulltext + "\r\n";
                where = where + "or ACTIVITY_EVALUATE_NAME like " + fulltext + "\r\n";
                where = where + "or ACTIVITY_CHILD_TYPE_NAME like " + fulltext + " )" + "\r\n";
                //where = where + "or MEMO_CONTENT like " + fulltext + "\r\n";
                //where = where + "or MEMO_USER like " + fulltext + " )" + "\r\n";
            }
            if (where != "") where = where + "and ";
            where = where + "exists  (select CENTER_ID from CON_CENTER_AUTHORITY where CENTER_ID = CONTACT_VIEW.CENTER_ID and USERID = '" + userID + "' )";
            $(dg).datagrid('setWhere', where);
        }
    </script>    
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="_CON_System_Share.CONTACT_VIEW" runat="server" AutoApply="True"
                DataMember="CONTACT_VIEW" Pagination="True" QueryTitle=""
                Title="人脈全文檢索資料" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" AllowAdd="False" ViewCommandVisible="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="序號" Editor="numberbox" FieldName="ROWID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CENTER_ID" Editor="numberbox" FieldName="CENTER_ID" Format="" Width="120" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="中心中文名稱" Editor="text" FieldName="CENTER_CNAME" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="中心英文名稱" Editor="text" FieldName="CENTER_ENAME" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CONTACT_ID" Editor="numberbox" FieldName="CONTACT_ID" Format="" Width="120" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人姓名" Editor="text" FieldName="CONTACT_NAME" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人職稱" Editor="text" FieldName="CONTACT_JOB" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人電話" Editor="text" FieldName="CONTACT_TEL" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人手機" Editor="text" FieldName="CONTACT_CELLPHONE" Format="" MaxLength="0" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人EMAIL" Editor="text" FieldName="CONTACT_EMAIL" Format="" MaxLength="0" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人地址" Editor="text" FieldName="CONTACT_ADDR" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人備註" Editor="text" FieldName="CONTACT_MEMO" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人備註1" Editor="text" FieldName="CONTACT_MEMO1" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人備註2" Editor="text" FieldName="CONTACT_MEMO2" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="區域" Editor="text" FieldName="CONTACT_AREA_NAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="類型" Editor="text" FieldName="CONTACT_TYPE_NAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="領域別" Editor="text" FieldName="CONTACT_TERRITORY_NAME" Format="" MaxLength="0" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="專長" Editor="text" FieldName="CONTACT_SKILL_NAME" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="興趣" Editor="text" FieldName="CONTACT_HOBBY_NAME" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CONTACT_ACTIVITY_ID" Editor="numberbox" FieldName="CONTACT_ACTIVITY_ID" Format="" Width="120" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動名稱" Editor="text" FieldName="ACTIVITY_NAME" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動年度" Editor="text" FieldName="ACTIVITY_YEAR" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動地點" Editor="text" FieldName="ACTIVITY_ADDR" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動經辦人" Editor="text" FieldName="ACTIVITY_PERSON" Format="" MaxLength="0" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="作品集" Editor="text" FieldName="ACTIVITY_WORKS" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動其他說明" Editor="text" FieldName="ACTIVITY_DESCRIPTION" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動類型" Editor="text" FieldName="ACTIVITY_TYPE_NAME" Format="" MaxLength="0" Width="100" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動狀況" Editor="text" FieldName="ACTIVITY_STATUS_NAME" Format="" MaxLength="0" Width="100" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動級別" Editor="text" FieldName="ACTIVITY_LEVEL_NAME" Format="" MaxLength="0" Width="100" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動身分別" Editor="text" FieldName="ACTIVITY_IDENTITY_NAME" Format="" MaxLength="0" Width="100" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動評估" Editor="text" FieldName="ACTIVITY_EVALUATE_NAME" Format="" MaxLength="0" Width="100" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動子類別" Editor="text" FieldName="ACTIVITY_CHILD_TYPE_NAME" Format="" MaxLength="0" Width="100" Sortable="True" />
                    <%--<JQTools:JQGridColumn Alignment="right" Caption="CONTACT_MEMO_ID" Editor="numberbox" FieldName="CONTACT_MEMO_ID" Format="" Width="120" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="註記內容" Editor="text" FieldName="MEMO_CONTENT" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="註記人員" Editor="text" FieldName="MEMO_USER" Format="" MaxLength="0" Width="80" Sortable="True" />--%>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="人脈全文檢索" Condition="%%" DataType="string" Editor="text" FieldName="CENTER_CNAME" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
