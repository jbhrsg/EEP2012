<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Setting_SalesKindSalesType.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
                    $(this).css("background-color", "lightyellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
        });
        function dataGridView_OnLoadSuccess() {
            //$("#dataGridView").datagrid('selectRow', -1);
        }
        //銷貨主類別資料dataGrid的連動
        function dataGridView_OnSelect(index, rowdata) {
            if (rowdata != null && rowdata != undefined) {

                $("#dataGrid1").datagrid('setWhere', "s.SalesKindID='" + rowdata.SalesKindID + "'");
            }
            else
                $("#dataGrid1").datagrid('setWhere', "s.SalesKindID='XXXXXXXXXX'");
        }
        //銷貨類別dataForm載入
        function dataForm1_OnLoadSuccess() {
            if (getEditMode($("#dataForm1")) == "inserted") {
                var row = $("#dataGridView").datagrid('getSelected');
                if (row != null) {
                    //設定銷貨主類別ID
                    $("#dataForm1SalesKindID").combobox('reload');
                    $("#dataForm1SalesKindID").combobox('setValue', row.SalesKindID);
                    //重整銷貨類別ID
                    $("#dataForm1SalesTypeID").combobox('setWhere', '');

                } else {
                    //銷貨主類別資料沒選就關掉
                    closeForm($("#JQDialog2"));
                }
            }
        }
        function dataFormMasterOnLoadSuccess() {
            if (getEditMode($("#dataFormMaster")) == 'updated') {
                $("#dataFormMasterSalesKindID").attr('disabled', true);
            }
            else {
                $("#dataFormMasterSalesKindID").attr('disabled', false);
            }
        }
        //加入銷貨類別
        function AddSalesType() {
            var rows = $("#dataGridView").datagrid('getRows');
            if (rows.length <= 0) {
                alert('須請先選取一筆銷貨主類');
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
            var KindSaleTypeIDs = row.KindSaleTypeIDS;
            JQSalesTypeSetChecked(KindSaleTypeIDs);
            openForm('#JQDialog3', {}, "", 'dialog');
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
        function dataFormMasterOnApplied() {
            $("#dataGridView").datagrid('reload');
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
            SaveKindSaleType(SalesTypeIDs);
            $("#dataGrid1").datagrid('reload');
            return true;
        }
        //儲存業務銷貨類別
        function SaveKindSaleType(SalesTypeIDs) {
            var UserID = getClientInfo("UserName");
            var row = $("#dataGridView").datagrid('getSelected');
            var SalesKindID = row.SalesKindID;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Setting_SalesKindSalesType.SalesKind',
                data: "mode=method&method=" + "SaveKindSaleType" + " &parameters=" + SalesKindID + "*" + SalesTypeIDs + "*" + UserID,
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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERP_Setting_SalesKindSalesType.SalesKind" runat="server" AutoApply="True"
                DataMember="SalesKind" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="業務類別" DuplicateCheck="True" OnSelect="dataGridView_OnSelect" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnLoadSuccess="dataGridView_OnLoadSuccess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務類別" Editor="text" FieldName="SalesKindID" Format="" Visible="true" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務類別名稱" Editor="text" FieldName="SalesKindName" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶系統URL" Editor="text" FieldName="CRUDUrl" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="300">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" Format="yyyy/mm/dd HH:MM:SS" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="KindSaleTypeIDS" Editor="text" FieldName="KindSaleTypeIDS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton"  OnClick="AddSalesType" Text="設定銷貨類別"/>
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="業務類別">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SalesKind" HorizontalColumnsCount="1" RemoteName="sERP_Setting_SalesKindSalesType.SalesKind" DuplicateCheck="True" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormMasterOnLoadSuccess" OnApplied="dataFormMasterOnApplied" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務類別" Editor="text" FieldName="SalesKindID" Format="" Width="60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務類別名稱" Editor="text" FieldName="SalesKindName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶系統URL" Editor="text" FieldName="CRUDUrl" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="320" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <JQTools:JQDataGrid ID="dataGrid1" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="SalesKindSalesType" DeleteCommandVisible="True" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERP_Setting_SalesKindSalesType.SalesKindSalesType" RowNumbers="True" Title="業務類別的銷貨類別" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" EditDialogID="JQDialog2">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="業務類別代號" Editor="text" FieldName="SalesKindID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="180">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="業務類別名稱" Editor="text" FieldName="SalesKindName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別代號" Editor="text" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別名稱" Editor="text" FieldName="SalesTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" Format="yyyy/mm/dd HH:MM:SS">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" Visible="False" />
                </TooItems>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataForm1" Title="業務類別的銷貨類別">
                <JQTools:JQDataForm ID="dataForm1" runat="server" DataMember="SalesKindSalesType" HorizontalColumnsCount="2" RemoteName="sERP_Setting_SalesKindSalesType.SalesKindSalesType" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="True" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataForm1_OnLoadSuccess" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務類別" Editor="infocombobox" EditorOptions="valueField:'SalesKindID',textField:'SalesKindName',remoteName:'sERP_Setting_SalesKindSalesType.SalesKind',tableName:'SalesKind',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesKindID" maxlength="0" Width="80" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" Width="120" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Setting_SalesKindSalesType.SalesType_dataForm1',tableName:'SalesType_dataForm1',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="default1" runat="server" BindingObjectID="dataForm1" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validate1" runat="server" BindingObjectID="dataForm1" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialog3" runat="server" DialogLeft="260px" DialogTop="20px" Title="選取銷貨類別" Width="360px" OnSubmited="SalesTypeOnSubmited">
                  <div class="easyui-layout" style="height: 360px;">
                    <div data-options="region:'center',title:'',border:false" title="">
                 <JQTools:JQTreeView ID="JQSalesType" runat="server" DataMember="SalesTypeTree" idField="ID" parentField="ParentID" RemoteName="sERP_Setting_SalesKindSalesType.SalesTypeTree" textField="Name" Checkbox="True" Height="360px"></JQTools:JQTreeView>
                    </div>
                    </div>
       </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
