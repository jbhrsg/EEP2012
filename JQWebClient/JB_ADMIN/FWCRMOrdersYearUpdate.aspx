<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMOrdersYearUpdate.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
     <script> 
         $(document).ready(function () {
             
         });
         function OnSelectOrderNo(rowData) {
             var OrderType = rowData.OrderType;
             var OrderStatus = rowData.OrderStatus;
             $("#dataFormMasterOrderType").options('setValue', OrderType);
             $("#dataFormMasterOrderStatus").options('setValue', OrderStatus);
         }
         function SetOrderYearNew() {
             var d = new Date();
             return d.getFullYear();
         }
       
         //修改過濾
         function OnLoadSuccessMaster() {
             var UserID = getClientInfo("UserID");
             $('#dataFormMasterOrderNo').refval('setWhere', "f.SalesID ='" + UserID + "'")
         }

         function checkApplyData() {            	
             var OrderYear = $('#dataFormMasterOrderYear').val();
             var OrderYearNew = $('#dataFormMasterOrderYearNew').val();

             if (OrderYear == OrderYearNew) {                
                alert('原始年度 與 轉換年度 相同。');
                return false;                 
             }

         }
     </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sFWCRMOrdersYearUpdate.FWCRMOrdersYearUpdate" runat="server" AutoApply="True"
                DataMember="FWCRMOrdersYearUpdate" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="0" Visible="true" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="訂單年度修改" DialogTop="10px" DialogLeft="10px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="FWCRMOrdersYearUpdate" HorizontalColumnsCount="2" RemoteName="sFWCRMOrdersYearUpdate.FWCRMOrdersYearUpdate" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="checkApplyData"  >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單編號" Editor="inforefval" FieldName="OrderNo" Format="" maxlength="0" Width="130" EditorOptions="title:'選擇訂單',panelWidth:450,remoteName:'sFWCRMOrdersYearUpdate.infoOrderNo',tableName:'infoOrderNo',columns:[{field:'OrderNo',title:'訂單編號',width:95,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'EmployerName',title:'雇主名稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'WorkNo',title:'聘工表號碼',width:95,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'OrderYear',value:'OrderYear'},{field:'SalesID',value:'SalesID'},{field:'EmployerID',value:'EmployerID'},{field:'NationalityID',value:'NationalityID'}],whereItems:[],valueField:'OrderNo',textField:'OrderNo',valueFieldCaption:'OrderNo',textFieldCaption:'訂單編號',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,onSelect:OnSelectOrderNo,selectOnly:false,capsLock:'none',fixTextbox:'false'" NewRow="False" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="原始年度" Editor="text" FieldName="OrderYear" Format="" Width="80" ReadOnly="True" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="轉換年度" Editor="text" FieldName="OrderYearNew" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單類型" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:310,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:5,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'入境',value:'1'},{text:'承接',value:'2'},{text:'轉單',value:'3'},{text:'轉單續聘',value:'4'},{text:'代招',value:'5'}]" FieldName="OrderType" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="220" />
                        <JQTools:JQFormColumn Alignment="left" Caption="負責業務" Editor="infocombobox" EditorOptions="valueField:'EmpID',textField:'NAME_C',remoteName:'sFWCRMOrders.infoSalesID',tableName:'infoSalesID',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單狀態" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:270,remoteName:'sFWCRMOrders.infoOrderStatus',tableName:'infoOrderStatus',valueField:'ID',textField:'Name',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="OrderStatus" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="2" Visible="True" Width="150" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主名稱" Editor="infocombobox" EditorOptions="valueField:'EmployerID',textField:'EmployerName',remoteName:'sFWCRMOrders.infoEmployerID',tableName:'infoEmployerID',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EmployerID" Format="" ReadOnly="True" Width="180" NewRow="True" MaxLength="0" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="引進國別" Editor="infocombobox" EditorOptions="valueField:'AutoKey',textField:'NationalityName',remoteName:'sFWCRMOrders.infoFWCRMNationality',tableName:'infoFWCRMNationality',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="NationalityID" Format="" MaxLength="0" NewRow="True" ReadOnly="True" Width="180" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="iAutokey" Editor="text" FieldName="iAutokey" maxlength="0" NewRow="False" ReadOnly="False" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>                
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                     <Columns>
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="SetOrderYearNew" FieldName="OrderYearNew" RemoteMethod="False" />
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="iAutokey" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                     <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="OrderNo" RemoteMethod="True" ValidateMessage="請選擇訂單" ValidateType="None" />
                         <JQTools:JQValidateColumn CheckNull="True" FieldName="OrderYearNew" RemoteMethod="True" ValidateMessage="轉換年度不可空白！'" ValidateType="None" CheckMethod="" />
                    </Columns>

                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
