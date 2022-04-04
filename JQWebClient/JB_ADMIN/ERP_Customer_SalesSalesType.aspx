<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Customer_SalesSalesType.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
   function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;'  />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
    function dataGridView_OnLoadSuccess() {
            $("#dataGridView").datagrid('selectRow', -1);
    }
    //業務資料dataForm的業務員ID連動
    function dataFormMasterSalesID_OnSelect(rowdata) {
        $("#dataFormMasterSalesName").val(rowdata.USERNAME);
    }
    //業務資料dataGrid的連動
    function dataGridView_OnSelect(index,rowdata) {
        if (rowdata != null && rowdata != undefined) {
        
            $("#dataGrid1").datagrid('setWhere', "SalesID='" + rowdata.SalesID + "'");
        }
        else
            $("#dataGrid1").datagrid('setWhere', "SalesID='XXXXXXXXXX'");
    }
    //銷貨類別dataForm載入
    function dataForm1_OnLoadSuccess() {
        if (getEditMode($("#dataForm1")) == "inserted") {
            var row = $("#dataGridView").datagrid('getSelected');
            if (row != null) {
                //設定業務ID
                $("#dataForm1SalesID").combobox('setValue', row.SalesID);
                $("#dataForm1SalesTypeID").combobox('setValue', '');
            } else {
                //業務資料沒選就關掉
                closeForm($("#JQDialog2"));
            }
        }
    }
    function dataFormMaster_OnLoadSuccess() {
        if (getEditMode($("#dataFormMaster")) == "inserted") {
            $("#dataFormMasterSalesID").combobox('setValue', '');
        }
    }
    function queryGrid() {
        var SalesID = $.trim($("#SalesID_Query").combobox('getValue'));
        var SalesTypeID = $.trim($("#SalesTypeID_Query").combobox('getValue'));
        var IsActive = $('#IsActive_Query').checkbox('getValue');
        var whereArr = [];
        if (SalesTypeID != "") {
            whereArr.push("SalesID in(select SalesID from SalesSalesType where SalesTypeID = '" + SalesTypeID + "')")
        }
        if (SalesID != "") {
            whereArr.push("SalesName like '%" + SalesID + "%'" + " or " + "SalesID = '" + SalesID + "'");
        }
        if (IsActive == 1) {
            whereArr.push("IsActive = " + "'" + IsActive + "'");
        }
        if (whereArr.length != 0) {
            $("#dataGridView").datagrid('setWhere', whereArr.join(" and "));
        } else {
            $("#dataGridView").datagrid('setWhere', '1=1');
        }

        return true;
    }
    function dataGrid1_OnApply() {
        apply("#dataGrid1");
        $("#dataGrid1").datagrid('reload');
    }
    //加入銷貨類別
    function AddSalesType() {
        var rows = $("#dataGridView").datagrid('getRows');
        if (rows.length <= 0) {
            alert('須請先選取一筆業務人員');
            return false;
        }
        var nodes = $('#JQSalesType').tree('getChecked');
        $.each(nodes, function (index, node) {
            $('#JQSalesType').tree('uncheck', node.target);
            $(node.target).removeClass('.tree-node-clicked');
        });
        var root = $('#JQSalesType').tree('getRoot');
        $('#JQSalesType').tree('uncheck', root.target);
        var row = $('#dataGridView').datagrid('getSelected');
        var SalesSaleTypeIDs = row.SalesSaleTypeIDS;
        JQSalesTypeSetChecked(SalesSaleTypeIDs);
        openForm('#JQDialog4', {}, "", 'dialog');
    }
    function JQSalesTypeSetChecked(IDstr) {
        if (IDstr != '') {
            $.each(IDstr.split(","), function (i, id) {
                var node = $('#JQSalesType').tree('find', id);
                if (node != null) {
                    $(node.target).addClass('tree-node-clicked');
                    $('#JQSalesType').tree('check', node.target);
                }
            });
        }
    }
    function SalesTypeOnSubmited() {
        var nodes = $('#JQSalesType').tree('getChecked');
        var SalesTypeIDs = '';
        var i = 1;
        $.each(nodes, function (index, node) {
            if (i > 1) {
                SalesTypeIDs = SalesTypeIDs + ',' + node.id;
            }
            else {
                SalesTypeIDs = SalesTypeIDs + node.id;
            }
            i = i + 1;
        });
        SaveSalesSaleType(SalesTypeIDs);
        $("#dataGrid1").datagrid('reload');
        return true;
    }
    //儲存業務銷貨類別
    function SaveSalesSaleType(SalesTypeIDs) {
        var UserID = getClientInfo("UserName");
        var row = $("#dataGridView").datagrid('getSelected');
        var Sales = row.SalesID;
        $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Customer_SalesSalesType.SalesPerson',
            data: "mode=method&method=" + "SaveSalesSaleType" + " &parameters=" + Sales + "*" + SalesTypeIDs + "*" + UserID,
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
    };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERP_Customer_SalesSalesType.SalesPerson" runat="server" AutoApply="True"
                DataMember="SalesPerson" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="業務員" OnSelect="dataGridView_OnSelect" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnLoadSuccess="dataGridView_OnLoadSuccess" Width="750px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="代號" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Visible="False" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務員" Editor="text" FieldName="SalesName" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務屬性" Editor="infocombobox" EditorOptions="valueField:'CustomerTypeID',textField:'CustomerTypeName',remoteName:'sERP_Customer_Normal_Customer.CustomerType',tableName:'CustomerType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustomerTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="業務角色" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsSalesRole" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="40" FormatScript="genCheckBox">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd" Visible="true" Width="85" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" Format="yyyy/mm/dd" Visible="true" Width="85" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesSaleTypeIDS" Editor="text" FieldName="SalesSaleTypeIDS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton"   OnClick="AddSalesType" Text="銷貨類別權限設定" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務員" Condition="%%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERP_Customer_SalesSalesType.QSalesPerson',tableName:'QSalesPerson',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨類別" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Customer_SalesSalesType.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="150" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="有效" Condition="=" DataType="string" DefaultValue="1" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="30" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="業務員">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SalesPerson" HorizontalColumnsCount="2" RemoteName="sERP_Customer_SalesSalesType.SalesPerson" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="True" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormMaster_OnLoadSuccess" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="180" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="text" FieldName="SalesID" Format="" maxlength="0" Width="60" EditorOptions="" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務員姓名" Editor="text" FieldName="SalesName" Format="" maxlength="0" Width="180" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶屬性" Editor="infocombobox" EditorOptions="valueField:'CustomerTypeID',textField:'CustomerTypeName',remoteName:'sERP_Customer_Normal_Customer.CustomerType',tableName:'CustomerType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustomerTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="70" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務角色" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsSalesRole" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="35" />
                        <JQTools:JQFormColumn Alignment="left" Caption="有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Visible="True" Width="35" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsSalesRole" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsSalesRole" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <JQTools:JQDataGrid ID="dataGrid1" runat="server" AlwaysClose="True" DataMember="SalesSalesType" EditDialogID="JQDialog2" RemoteName="sERP_Customer_SalesSalesType.SalesSalesType" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" Title="業務員銷貨類別權現清單" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="750px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="業務員" Editor="infocombobox" FieldName="SalesID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERP_Customer_SalesSalesType.SalesPerson',tableName:'SalesPerson',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="160" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Customer_SalesSalesType.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="text" FieldName="ShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="115" Format="yyyy/mm/dd HH:MM:SS">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="" Text="多筆新增" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="dataGrid1_OnApply" Text="存檔" Visible="False" />
                </TooItems>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataForm1" Title="業務員銷貨類別" Closed="True">
                <JQTools:JQDataForm ID="dataForm1" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="SalesSalesType" disapply="False" DivFramed="False" DuplicateCheck="True" HorizontalColumnsCount="3" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sERP_Customer_SalesSalesType.SalesSalesType" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataForm1_OnLoadSuccess">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="業務員" Editor="infocombobox" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERP_Customer_SalesSalesType.Users',tableName:'Users',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Customer_SalesSalesType.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="default1" runat="server" BindingObjectID="dataForm1">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validate1" runat="server" BindingObjectID="dataForm1">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
        <JQTools:JQDialog ID="JQDialog3" runat="server" BindingObjectID="" OnSubmited="" DialogTop="-80px">
            <JQTools:JQDataGrid ID="dataGrid2" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="dataGrid2" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" NotInitGrid="False" PageList="50,100,150,200,250" PageSize="50" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERP_Customer_SalesSalesType.dataGrid2" RowNumbers="True" Title="JQDataGrid" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="infocombobox" FieldName="SalesTypeID" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Customer_SalesSalesType.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="Insert" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="False" />
                </TooItems>
            </JQTools:JQDataGrid>
        </JQTools:JQDialog>
        <JQTools:JQDialog ID="JQDialog4" runat="server" DialogLeft="260px" DialogTop="20px" Title="選取銷貨類別" Width="450px" OnSubmited="SalesTypeOnSubmited">
                  <div class="easyui-layout" style="height: 480px;">
                    <div data-options="region:'center',title:'',border:false" title="">
                 <JQTools:JQTreeView ID="JQSalesType" runat="server" DataMember="SalesTypeTree" idField="ID" parentField="ParentID" RemoteName="sERP_Customer_SalesSalesType.SalesTypeTree" textField="Name" Checkbox="True"></JQTools:JQTreeView>
                    </div>
                    </div>
       </JQTools:JQDialog>

      <%--  <JQTools:JQBatchMove ID="batchMove1" runat="server" DesDataGrid="dataGrid1" SrcDataGrid="dataGrid2">
            <MatchColumns>
                <JQTools:JQBatchMoveColumns DesColumn="SalesTypeID" SrcColumn="SalesTypeID" />
                <JQTools:JQBatchMoveColumns DesColumn="SalesID" SrcColumn="SalesID" />
            </MatchColumns>
        </JQTools:JQBatchMove>--%>
    </form>
</body>
</html>
