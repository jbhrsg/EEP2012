<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_MediaPostQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var dd = new Date();
            var DT = dd.getFullYear().toString() + '/' + (dd.getMonth() + 1).toString() + '/' + dd.getDate().toString();
            sqlstr = ("Published between '" + DT + "' and '" + DT + "'");
            $('#dataGridMaster').datagrid('setWhere', sqlstr);
           
        });
        function dataGridMasterLoadSucess() {
         
        
        }
        function queryGrid(dg) {
            var aVal = '';
            var bVal = '';
            var sqlstr = '';
            aVal = $('#DATES_Query').datebox('getValue');
            bVal = $('#DATEE_Query').datebox('getValue');
            if (aVal != '' && bVal != '') {
                sqlstr = ("Published between '" + aVal + "' and '" + bVal + "'");
            }
            $(dg).datagrid('setWhere', sqlstr);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sMediaPostCount.PeriodArea" runat="server" AutoApply="True"
                DataMember="PeriodArea" Pagination="True" QueryTitle=""
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="480px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="區域" Editor="text" FieldName="AreaName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="數量" Editor="numberbox" FieldName="QTY" Width="80" Total="sum" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                    <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="&gt;=" DataType="string" DefaultValue="_today" Editor="datebox" FieldName="DATES" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="1" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止日期" Condition="%" DataType="string" DefaultValue="_today" Editor="datebox" FieldName="DATEE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="1" Width="125" />
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
