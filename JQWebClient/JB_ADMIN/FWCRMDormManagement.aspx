<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMDormManagement.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>

        //========================================= ready ====================================================================================

        $(document).ready(function () {
            $('#IsActive_Query').checkbox('setValue', 1);
            $('#IsActiveJS_Query').checkbox('setValue', 1);

        });

        //--------------------查詢條件的標籤Grid--------------------------
        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridView') {
                //查詢條件
                var result = [];

                var ListContent = $('#ListContent_Query').val();//宿舍名稱
                    if (ListContent != '') result.push("ListContent like '%" + ListContent + "%'");

                    var ckIsActive = $('#IsActive_Query').checkbox('getValue');//有效
                    if (ckIsActive != '') result.push("IsActive=" + ckIsActive );

                    $(dg).datagrid('setWhere', result.join(' and '));

            }
            else if ($(dg).attr('id') == 'dataGridViewJS') {
                //查詢條件
                var result = [];

                var ListContent = $('#ListContentJS_Query').val();//宿舍名稱
                if (ListContent != '') result.push("ListContent like '%" + ListContent + "%'");

                var ckIsActive = $('#IsActiveJS_Query').checkbox('getValue');//有效
                if (ckIsActive != '') result.push("IsActive=" + ckIsActive);

                $(dg).datagrid('setWhere', result.join(' and '));

            }
        }
        
        function genCheckBox(val) {
            if (val != "0")
                return "<input  type='checkbox' checked='true' onclick='return false;'/>";
            else
                return "<input  type='checkbox' onclick='return false;'/>";
        }

     
            
           
        

        


    </script>



</head>
<body>
    <form id="form1" runat="server">
        <div>
         
            
            <div id="cc" class="easyui-layout" style="width:940px;height:450px;">

            <div data-options="region:'west',title:'傑報宿舍管理',split:true" style="width:470px;">
                <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />

                <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoDorm" runat="server" AutoApply="True"
                    DataMember="infoDorm" Pagination="True" QueryTitle="" EditDialogID=""
                    Title=" " AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="460px">
                    <Columns>
    <%--                    <JQTools:JQGridColumn Alignment="center" Caption="出生年" Editor="numberbox" FieldName="BirthYear" MaxLength="0" Visible="true" Width="60" Sortable="False" />--%>
                        <JQTools:JQGridColumn Alignment="left" Caption="宿舍名稱" Editor="text" FieldName="ListContent" Format="" Visible="True" Width="240" />
                        <JQTools:JQGridColumn Alignment="center" Caption="電費單價" Editor="numberbox" FieldName="Nums" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" EditorOptions="precision:2,groupSeparator:'',prefix:''">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="區域" Editor="text" FieldName="DormArea" Format="" MaxLength="0" Visible="False" Width="80" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="預估人數" Editor="numberbox" FieldName="EstimatedPeople" Format="" MaxLength="0" Visible="False" Width="60" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="實際人數" Editor="numberbox" FieldName="ActualPeople" Format="" MaxLength="0" Visible="False" Width="60" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="ListCategory" Editor="text" FieldName="ListCategory" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="ListID" Editor="text" FieldName="ListID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="Listcontentunicode" Editor="text" FieldName="Listcontentunicode" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="有效?" Editor="checkbox" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" EditorOptions="on:1,off:0" FormatScript="genCheckBox">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
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
                        <JQTools:JQQueryColumn AndOr="and" Caption="有效" Condition="%" DataType="string" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="30" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
          
                <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="Form1">
                    <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="infoDorm" HorizontalColumnsCount="2" RemoteName="sPowerData.infoDorm" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="宿舍名稱" Editor="text" FieldName="ListCategory" Format="" maxlength="0" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ListID" Editor="numberbox" FieldName="ListID" Format="" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ListContent" Editor="text" FieldName="ListContent" Format="" maxlength="0" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="Listcontentunicode" Editor="text" FieldName="Listcontentunicode" Format="" maxlength="0" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="電費單價" Editor="numberbox" FieldName="Nums" Format="" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="預估人數" Editor="numberbox" FieldName="EstimatedPeople" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="實際人數" Editor="numberbox" FieldName="ActualPeople" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="IsActive" Editor="text" FieldName="IsActive" Format="" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="numberbox" FieldName="CompanyID" Format="" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="區域" Editor="text" FieldName="DormArea" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                    </JQTools:JQDefault>
                    <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                    </JQTools:JQValidate>
                </JQTools:JQDialog>
          
            </div>
            <div data-options="region:'center',title:'傑信宿舍管理',split:true" style="width:470px;">

                <JQTools:JQDataGrid ID="dataGridViewJS" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoDormJS" runat="server" AutoApply="True"
                    DataMember="infoDormJS" Pagination="True" QueryTitle="" EditDialogID=""
                    Title=" " AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="460px">
                    <Columns>
    <%--                    <JQTools:JQGridColumn Alignment="center" Caption="出生年" Editor="numberbox" FieldName="BirthYear" MaxLength="0" Visible="true" Width="60" Sortable="False" />--%>
                        <JQTools:JQGridColumn Alignment="left" Caption="宿舍名稱" Editor="text" FieldName="ListContent" Format="" Visible="True" Width="240" />
                        <JQTools:JQGridColumn Alignment="center" Caption="電費單價" Editor="numberbox" FieldName="Nums" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" EditorOptions="precision:2,groupSeparator:'',prefix:''">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="區域" Editor="text" FieldName="DormArea" Format="" MaxLength="0" Visible="False" Width="60" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="預估人數" Editor="numberbox" FieldName="EstimatedPeople" Format="" MaxLength="0" Visible="False" Width="60" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="實際人數" Editor="numberbox" FieldName="ActualPeople" Format="" MaxLength="0" Visible="False" Width="60" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="ListCategory" Editor="text" FieldName="ListCategory" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="ListID" Editor="text" FieldName="ListID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="Listcontentunicode" Editor="text" FieldName="Listcontentunicode" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="有效?" Editor="checkbox" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" EditorOptions="on:1,off:0" FormatScript="genCheckBox">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
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
                        <JQTools:JQQueryColumn AndOr="and" Caption="宿舍名稱" Condition="%" DataType="string" Editor="text" FieldName="ListContentJS" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="有效" Condition="%" DataType="string" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActiveJS" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="30" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
          


            </div>

            </div>
        </div>
    </form>
</body>
</html>
