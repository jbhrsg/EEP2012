<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMDormFeeManagement.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>

        //========================================= ready ====================================================================================

        $(document).ready(function () {

        });

        //--------------------查詢條件的標籤Grid--------------------------
        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridView') {
                //查詢條件
                var result = [];

                var ListContent = $('#ListContent_Query').val();//宿舍名稱
                if (ListContent != '') result.push("DormName like '%" + ListContent + "%'");

                $(dg).datagrid('setWhere', result.join(' and '));

            }
            
        }
        
       

     
            
           
        

        


    </script>



</head>
<body>
    <form id="form1" runat="server">
        <div>
         
            
  
                <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />

                <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoglDorm" runat="server" AutoApply="True"
                    DataMember="infoglDorm" Pagination="True" QueryTitle="" EditDialogID=""
                    Title=" " AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="460px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="公司別" Editor="text" FieldName="CompanyName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="110" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="宿舍名稱" Editor="text" FieldName="DormName" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="center" Caption="區域" Editor="text" FieldName="DormArea" Format="" MaxLength="0" Visible="true" Width="60" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="預估人數" Editor="numberbox" FieldName="EstimatedPeople" Format="" MaxLength="0" Visible="true" Width="60" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="實際人數" Editor="numberbox" FieldName="ActualPeople" Format="" MaxLength="0" Visible="true" Width="60" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                        <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                        <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                        <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                        </TooItems>

                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="宿舍名稱" Condition="%" DataType="string" Editor="text" FieldName="ListContent" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
          


          
        </div>
    </form>
</body>
</html>
