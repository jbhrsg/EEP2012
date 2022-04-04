<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERQ_ShortTermLists.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script> 
    <script type="text/javascript">
        function BeforeOneMonth() {
            var dt = new Date();
            var aDate = new Date($.jbDateAdd('days', -31, dt));
            return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
        }
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridView') {
                var FiltStr = '';
                var PlanPayDateS = $('#PlanPayDateS_Query').datebox('getValue');
                if (PlanPayDateS != '' && PlanPayDateS != undefined) {
                    FiltStr = "RequestDate >= " + "'" + PlanPayDateS + "'";
                }
                var PlanPayDateE = $('#PlanPayDateE_Query').datebox('getValue');
                if (PlanPayDateE != '' && PlanPayDateE != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + "RequestDate <= " + "'" + PlanPayDateE + "'";
                }
                var EmployeeID = $('#EmployeeID_Query').combobox('getValue');
                if (EmployeeID != '' && EmployeeID != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + "EmployeeID = " + "'" + EmployeeID + "'";
                }
                var CompanyID = $('#CompanyID_Query').combobox('getValue');
                if (CompanyID != '' && CompanyID != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + "CompanyID = " + "'" + CompanyID + "'";
                }
                var PayTo = $('#PayTo_Query').combobox('getValue');
                if (PayTo != '' && PayTo != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + "PayTo = " + "'" + PayTo + "'";
                }
                var PayTypeID = $('#PayTypeID_Query').combobox('getValue');
                if (PayTypeID != '' && PayTypeID != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + "PayTypeID = " + "'" + PayTypeID + "'";
                }

                var Status = $('#Status_Query').combobox('getValue');
                if (Status != '' && Status != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    if (Status == 1) {
                        FiltStr = FiltStr + " Flowflag <> 'Z'";
                    }
                    else {
                        FiltStr = FiltStr + " Flowflag = 'Z'";
                    }
                }
                $(dg).datagrid('setWhere', FiltStr);
            }
        }
        function dataFormMasterOnApply() {
            var AssetGetType = $('#dataFormMasterAssetGetType').combobox('getValue');
            if (AssetGetType == "" || AssetGetType == undefined) {
                alert('注意!!,未選取得方式,請選取!!');
                $("#dataFormMasterAssetGetType").focus();
                return false;
            }
            var AssetUnit = $('#dataFormMasterAssetUnit').combobox('getValue');
            if (AssetUnit == "" || AssetUnit == undefined) {
                alert('注意!!,未選取或輸入單位,請輸入或選取!!');
                $("#dataFormMasterAssetUnit").focus();
                return false;
            }
            if (getEditMode($("#dataFormMaster")) == 'Inserted') {
                var AssetCompID = $('#dataFormMasterAssetCompID').combobox('getValue');
                if (AssetCompID == "" || AssetCompID == undefined) {
                    alert('注意!!,未選取帳務歸屬,請選取!!');
                    $("#dataFormMasterAssetCompID").focus();
                    return false;
                }
                var AssetOwnerID = $('#dataFormMasterAssetOwnerID').combobox('getValue');
                if (AssetOwnerID == "" || AssetOwnerID == undefined) {
                    alert('注意!!,未選取保管人員,請選取!!');
                    $("#dataFormMasterAssetOwnerID").focus();
                    return false;
                }
                var AssetLocaID = $('#dataFormMasterAssetLocaID').combobox('getValue');
                if (AssetLocaID == "" || AssetLocaID == undefined) {
                    alert('注意!!,未選取區域位置,請選取!!');
                    $("#dataFormMasterAssetLocaID").focus();
                    return false;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sShortTermQuery.ShowTermQuery" runat="server" AutoApply="True"
                DataMember="ShowTermQuery" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="暫借款單明細" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="15,30,45,60,120" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借單號" Editor="text" FieldName="ShortTermNO" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ShortTermDate" Format="" Width="70" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者" Editor="infocombobox" FieldName="EmployeeID" Format="" MaxLength="0" Width="70" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sShortTermQuery.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="CompanyID" Format="" Width="75" EditorOptions="valueField:'COMPANYID',textField:'COMPANYNAME',remoteName:'sShortTermQuery.Company',tableName:'Company',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借事由" Editor="text" FieldName="ShortTermGist" Format="" MaxLength="0" Width="320" />
                    <JQTools:JQGridColumn Alignment="left" Caption="事由說明" Editor="text" FieldName="ShortTermDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="暫借金額" Editor="numberbox" FieldName="ShortTermAmount" Format="N0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="支付方式" Editor="infocombobox" FieldName="PayTypeID" Format="" Width="60" EditorOptions="valueField:'PAYTYPEID',textField:'PAYTYPENAME',remoteName:'sShortTermQuery.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="受款人" Editor="infocombobox" FieldName="PayTo" Format="" MaxLength="0" Width="90" EditorOptions="valueField:'VENDID',textField:'VENDNAME',remoteName:'sShortTermQuery.Vendor',tableName:'Vendor',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="需求日期" Editor="datebox" FieldName="RequestDate" Format="yyyy/mm/dd" Width="70" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="預計歸還日" Editor="datebox" FieldName="PlanPayDate" Format="yyyy/mm/dd" Width="70" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="單據類別" Editor="infocombobox" FieldName="ShortTermTypeID" Format="" Width="120" EditorOptions="valueField:'ShortTermTypeID',textField:'ShortTermTypeName',remoteName:'sShortTermQuery.ShortTermType',tableName:'ShortTermType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="狀態" Editor="infocombobox" FieldName="Flowflag" Format="" MaxLength="0" Width="60" EditorOptions="items:[{value:'N',text:'流程中',selected:'false'},{value:'P',text:'流程中',selected:'false'},{value:'Z',text:'已結案',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銀行代碼" Editor="text" FieldName="BankNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="帳戶" Editor="text" FieldName="BankName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                <%--    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="需求起迄日" Condition="=" DataType="datetime" Editor="datebox" FieldName="PlanPayDateS" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" DefaultMethod="BeforeOneMonth" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="  -  " Condition="=" DataType="datetime" DefaultValue="_today" Editor="datebox" FieldName="PlanPayDateE" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請者" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sShortTermQuery.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EmployeeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'COMPANYID',textField:'COMPANYNAME',remoteName:'sShortTermQuery.Company',tableName:'Company',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="受款人" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'VENDID',textField:'VENDNAME',remoteName:'sShortTermQuery.Vendor',tableName:'Vendor',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTo" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="支付方式" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'PAYTYPEID',textField:'PAYTYPENAME',remoteName:'sShortTermQuery.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="狀態" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'STATUS',remoteName:'sShortTermQuery.Status',tableName:'Status',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Status" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="暫借款單明細">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ShowTermQuery" HorizontalColumnsCount="2" RemoteName="sShortTermQuery.ShowTermQuery" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermNO" Editor="text" FieldName="ShortTermNO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermDate" Editor="datebox" FieldName="ShortTermDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="numberbox" FieldName="CompanyID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermTypeID" Editor="numberbox" FieldName="ShortTermTypeID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermGist" Editor="text" FieldName="ShortTermGist" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermAmount" Editor="numberbox" FieldName="ShortTermAmount" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayTypeID" Editor="numberbox" FieldName="PayTypeID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayTo" Editor="text" FieldName="PayTo" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="RequestDate" Editor="datebox" FieldName="RequestDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PlanPayDate" Editor="datebox" FieldName="PlanPayDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" maxlength="0" Width="180" />
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
