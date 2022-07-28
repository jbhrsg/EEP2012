<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_JBePortalEmp.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">

        $(document).ready(function () {
          
            ////查詢條件=>預設是否有效
            //Grid設定寬度
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 400 });
        });

        function OnLoadSuccessDF() {
            //帳號編輯模式不可修改
            if (getEditMode($("#dataFormMaster")) == 'updated') {
                $("#dataFormMasterAccount").attr("disabled", "disabled");
            }else
                $("#dataFormMasterAccount").removeAttr("disabled");


        }
        //存檔前檢查
        function OnApplyDF() {
            var EmpNum = $("#dataFormMasterEmpNum").val();
            var Account = $("#dataFormMasterAccount").val();
            var DeptCode = $("#dataFormMasterDeptCode").combobox('getValue');
            var inDate = $("#dataFormMasterinDate").datebox('getValue');
            var outDate = $("#dataFormMasteroutDate").datebox('getValue');

            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                //新增時工號不可空白
                if (EmpNum == "" || EmpNum == undefined) {
                    alert('工號不可空白！');
                    $("#dataFormMasterEmpNum").focus();
                    return false;
                }
                //新增時工號不可重複
                var iCount;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sJBePortalEmp.EmpBase', //連接的Server端，command
                    data: "mode=method&method=" + "checkEmpNumCount" + "&parameters=" + EmpNum,
                    cache: false,
                    async: false,
                    success: function (data) {
                        iCount = $.parseJSON(data);
                    }
                });
                if (iCount > 0) {
                    alert("此工號已存在！");
                    return false;
                }
                //新增時帳號不可重複
                var iCount2;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sJBePortalEmp.EmpBase', //連接的Server端，command
                    data: "mode=method&method=" + "checkAccountCount" + "&parameters=" + Account,
                    cache: false,
                    async: false,
                    success: function (data) {
                        iCount2 = $.parseJSON(data);
                    }
                });
                if (iCount2 > 0 || iCount2 == undefined) {
                    alert("此帳號已存在！");
                    return false;
                }
            } else if (getEditMode($("#dataFormMaster")) == 'updated') {               
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sJBePortalEmp.EmpBase', //連接的Server端，command
                    data: "mode=method&method=" + "EmpBaseModify" + "&parameters=" + EmpNum + "," + DeptCode + "," + inDate + "," + outDate,
                    cache: false,
                    async: false,
                    success: function (data) {
                    }
                });
            }
        }
       

        function OnInsertedGV() {
            $("#dataGridView").datagrid("reload");
        }


    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sJBePortalEmp.EmpBase" runat="server" AutoApply="True"
                DataMember="EmpBase" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="資料列表" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="250px" QueryMode="Window" QueryTop="150px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="工號" Editor="text" FieldName="EmpNum" Format="" MaxLength="0" Visible="true" Width="90" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="姓名" Editor="text" FieldName="Name" Format="" MaxLength="0" Visible="true" Width="100" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="帳號" Editor="text" FieldName="Account" Format="" MaxLength="0" Visible="true" Width="100" />
                    <JQTools:JQGridColumn Alignment="center" Caption="密碼" Editor="text" FieldName="PassWord" Format="" MaxLength="0" Visible="true" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="infocombobox" FieldName="DeptCode" Format="" MaxLength="0" Visible="true" Width="125" EditorOptions="valueField:'DeptCode',textField:'DeptName',remoteName:'sJBePortalEmp.infoDept',tableName:'infoDept',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="餘款" Editor="numberbox" FieldName="Money" Format="" Visible="true" Width="110" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="到職日期" Editor="datebox" FieldName="inDate" Format="" MaxLength="0" Visible="true" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="離職日期" Editor="datebox" FieldName="outDate" Format="" MaxLength="0" Visible="true" Width="120" Sortable="True" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />                    
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="工號" Condition="%" DataType="string" Editor="text" FieldName="e.EmpNum" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="姓名" Condition="%" DataType="string" Editor="text" FieldName="Name" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="資料維護">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="EmpBase" HorizontalColumnsCount="2" RemoteName="sJBePortalEmp.EmpBase" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnLoadSuccess="OnLoadSuccessDF" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApply="OnApplyDF" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="工號" Editor="text" FieldName="EmpNum" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="text" FieldName="Name" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳號" Editor="text" FieldName="Account" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="密碼" Editor="text" FieldName="PassWord" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門" Editor="infocombobox" EditorOptions="valueField:'DeptCode',textField:'DeptName',remoteName:'sJBePortalEmp.infoDept',tableName:'infoDept',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="DeptCode" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="140" />
                        <JQTools:JQFormColumn Alignment="right" Caption="餘款" Editor="numberbox" FieldName="Money" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="center" Caption="到職日期" Editor="datebox" FieldName="inDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="95" />
                        <JQTools:JQFormColumn Alignment="center" Caption="離職日期 " Editor="datebox" FieldName="outDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="95" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="inDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="9999/12/31" FieldName="outDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EmpNum" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Name" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PassWord" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Account" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DeptCode" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Money" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="inDate" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="outDate" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
