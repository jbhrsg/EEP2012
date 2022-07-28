<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_ContractAlter.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        function ContractNOOnSelect(rowdata) {
            $("#dataFormMasterBeginDate").datebox('setValue', rowdata.BeginDate.substring(0,rowdata.BeginDate.indexOf('T')));
            $("#dataFormMasterEndDate").datebox('setValue', rowdata.EndDate.substring(0, rowdata.EndDate.indexOf('T')));
            $("#dataFormMasterPhysicalContractNO").val(rowdata.PhysicalContractNO);
            $("#dataFormMasterRemindDays").val(rowdata.RemindDays);
            
            //$("#dataFormMasterIsForeignDept").val(rowdata.IsForeignDept);
            $("#dataFormMasterAssignChecker").val(rowdata.AssignChecker);
            $("#dataFormMasterKeeper").val(rowdata.Keeper);

            if (rowdata.GuarantyEndDate != null && rowdata.GuarantyEndDate != '') {
                $("#dataFormMasterGuarantyEndDate").datebox('setValue', rowdata.GuarantyEndDate.substring(0, rowdata.GuarantyEndDate.indexOf('T')));
            }
        }
        function dataFormMaster_OnLoad() {
            var parameter = Request.getQueryStringByName2("p1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("p1");//有加密
            }
            if (getEditMode($("#dataFormMaster")) == 'inserted' || parameter == "apply") {
                $("#tabs1").hide();
                $("#dataFormMasterIsForeignDept").val(IsForeignDept());
            } else {
                for (i = 1; i <= 5; i++) {
                    $("#download" + i).remove();
                    var file = $('.info-fileUpload-value', $("#dataFormMaster1Attachment" + i).next()).val();
                    if (file != '') {
                        var link = $("<a download>").attr({ 'id': 'download' + i, 'href': '../JB_ADMIN/Contract/Attachment' + i + '/' + file }).html('下載');
                        $('#dataFormMaster1Attachment' + i).closest('td').append(link);
                    }
                }
            }
        }
        
        //工具---------------------------------------------------------------------------
        function IsForeignDept() {
            var userid = getClientInfo("userid");
            var counts = 0;
            if (userid != "") {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPContract.ERPContract',
                    data: "mode=method&method=IsForeignDept&parameters=" + userid,
                    cache: false,
                    async: false,
                    success: function (data) {
                        counts = $.parseJSON(data);
                    }
                });
            }
            return counts;
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPContractAlter.ERPContractAlter" runat="server" AutoApply="True"
                DataMember="ERPContractAlter" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="ContractAlterNO" Editor="text" FieldName="ContractAlterNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ContractNO" Editor="text" FieldName="ContractNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="BeginDate" Editor="datebox" FieldName="BeginDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EndDate" Editor="datebox" FieldName="EndDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PhysicalContractNO" Editor="text" FieldName="PhysicalContractNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RemindDays" Editor="text" FieldName="RemindDays" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="履約保證到期日" Editor="datebox" FieldName="GuarantyEndDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="IsForeignDept" Editor="text" FieldName="IsForeignDept" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AssignChecker" Editor="text" FieldName="AssignChecker" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Keeper" Editor="text" FieldName="Keeper" Format="" MaxLength="0" Visible="true" Width="120" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="合約異動" Width="650px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPContractAlter" HorizontalColumnsCount="2" RemoteName="sERPContractAlter.ERPContractAlter" IsAutoPageClose="True" IsAutoSubmit="True" IsShowFlowIcon="True" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPause="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" ChainDataFormID="dataFormMaster1" OnLoadSuccess="dataFormMaster_OnLoad">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="合約異動編號" Editor="text" FieldName="ContractAlterNO" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約編號" Editor="inforefval" FieldName="ContractNO" Format="" maxlength="0" Width="180" EditorOptions="title:'',panelWidth:400,remoteName:'sERPContractAlter.ContractNO',tableName:'ContractNO',columns:[{field:'ContractNO',title:'合約編號',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'PhysicalContractNO',title:'紙本合約編號',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContractName',title:'合約名稱',width:180,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'ContractNO',textField:'ContractNO',valueFieldCaption:'ContractNO',textFieldCaption:'ContractNO',cacheRelationText:true,checkData:false,showValueAndText:false,dialogCenter:false,onSelect:ContractNOOnSelect,selectOnly:true,capsLock:'none',fixTextbox:'false'" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起日" Editor="datebox" FieldName="BeginDate" Format="" Width="180" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false,editable:true" />
                        <JQTools:JQFormColumn Alignment="left" Caption="迄日" Editor="datebox" FieldName="EndDate" Format="" Width="180" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false,editable:true" />
                        <JQTools:JQFormColumn Alignment="left" Caption="紙本合約編號" Editor="text" FieldName="PhysicalContractNO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="到期幾日前提醒" Editor="text" FieldName="RemindDays" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="履約保證到期日" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false,editable:true" FieldName="GuarantyEndDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsForeignDept" Editor="text" FieldName="IsForeignDept" Format="" maxlength="0" Width="180" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AssignChecker" Editor="text" FieldName="AssignChecker" Format="" maxlength="0" Width="180" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Keeper" Editor="text" FieldName="Keeper" Format="" maxlength="0" Width="180" ReadOnly="True" Visible="False" />

                        
                    </Columns>
                    
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="ContractAlterNO" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContractNO" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>

                <div id="tabs1" class="easyui-tabs" style="width:580px">
                   <div style="padding:10px" title="原合約資料">
                       <JQTools:JQDataForm ID="dataFormMaster1" runat="server" AlwaysReadOnly="False" ChainDataFormID="" Closed="False" ContinueAdd="False" DataMember="ERPContractAlter" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="True" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="" RemoteName="sERPContractAlter.ERPContractAlter" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                            <Columns>
                              

                                <JQTools:JQFormColumn Alignment="left" Caption="合約異動編號" Editor="text" FieldName="ContractAlterNO" Format="" maxlength="0" ReadOnly="True" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="紙本合約編號" Editor="text" FieldName="PhysicalContractNO1" Width="180" maxlength="0" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約名稱" Editor="text" FieldName="ContractName" Format="" Width="180" maxlength="0" ReadOnly="True" Visible="True" />
                       <JQTools:JQFormColumn Alignment="left" Caption="客戶/廠商" Editor="inforefval" FieldName="ContractB" Format="" Width="184" maxlength="0" ReadOnly="False" EditorOptions="title:'客戶/廠商',panelWidth:350,remoteName:'sERPContract.VenderCustomer',tableName:'VenderCustomer',columns:[{field:'name',title:'客戶/廠商',width:260,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'name',textField:'name',valueFieldCaption:'名稱',textFieldCaption:'名稱',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                         <JQTools:JQFormColumn Alignment="left" Caption="合約類別" Editor="infocombobox" FieldName="ContractClass" Format="" Width="184" EditorOptions="valueField:'ContractClassID',textField:'ContractClass',remoteName:'sERPContract.ContractClass',tableName:'ContractClass',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" ReadOnly="True" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="權責部門" Editor="infocombobox" EditorOptions="valueField:'CENTER_CNAME',textField:'CENTER_ENAME',remoteName:'sERPContractGroup.ERPContractGroup',tableName:'ERPContractGroup',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="ResponsibleDepart" ReadOnly="True" Visible="True" Width="184" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保管部門" Editor="infocombobox" FieldName="KeepDepart" ReadOnly="True" Visible="True" Width="180" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sERPContract.SYS_ORG',tableName:'SYS_ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保管人" Editor="infocombobox" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERPContract.GroupUsers',tableName:'GroupUsers',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Keeper1" Format="" ReadOnly="True" Width="184" Visible="True" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起日" Editor="datebox" FieldName="BeginDate1" Format="" Width="184" maxlength="0" NewRow="False" RowSpan="1" Span="1" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true,editable:false" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="迄日" Editor="datebox" FieldName="EndDate1" Format="" Width="184" ReadOnly="True" Visible="True" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true,editable:false" NewRow="False" />
                                <JQTools:JQFormColumn Alignment="left" Caption="簽約日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true,editable:false" FieldName="SignDate" Format="" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="184" />
                        <JQTools:JQFormColumn Alignment="left" Caption="金額" Editor="numberbox" FieldName="Amount" ReadOnly="True" Visible="True" Width="180" maxlength="0" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="到期前幾天提醒" Editor="numberbox" FieldName="RemindDays1" maxlength="0" Width="180" NewRow="False" Span="1" ReadOnly="True" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="履約保證" Editor="infocombobox" FieldName="IsGuaranty" ReadOnly="True" Width="184" Span="1" EditorOptions="items:[{value:'是',text:'是',selected:'false'},{value:'否',text:'否',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="履約保證票號" Editor="text" FieldName="GuarantyNO" maxlength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="履約保證金額" Editor="numberbox" FieldName="GuarantyAmount" Width="180" Span="1" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="履約保證到期日" Editor="datebox" FieldName="GuarantyEndDate1" ReadOnly="True" Width="184" maxlength="0" Visible="True" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true,editable:false" />
                        <JQTools:JQFormColumn Alignment="left" Caption="流程狀態" Editor="text" FieldName="FlowFlag" Format="" maxlength="0" Width="180" ReadOnly="True" Visible="False" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註1" Editor="textarea" FieldName="Remarks" Format="" Width="453" EditorOptions="height:60" ReadOnly="True" MaxLength="0" Visible="True" NewRow="False" RowSpan="1" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註2" Editor="textarea" FieldName="Remarks2" Width="453" EditorOptions="height:60" ReadOnly="True" MaxLength="0" Visible="True" NewRow="False" RowSpan="1" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件1" Editor="infofileupload" FieldName="Attachment1" Format="" Width="430" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment1',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" ReadOnly="True" maxlength="0" NewRow="True" RowSpan="1" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件2" Editor="infofileupload" FieldName="Attachment2" Format="" Width="430" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment2',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" ReadOnly="True" maxlength="0" Visible="True" NewRow="True" RowSpan="1" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件3" Editor="infofileupload" FieldName="Attachment3" Format="" ReadOnly="True" Width="430" maxlength="0" NewRow="True" RowSpan="1" Span="2" Visible="True" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment3',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件4" Editor="infofileupload" FieldName="Attachment4" Format="" maxlength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="2" Visible="True" Width="430" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment4',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件5" Editor="infofileupload" FieldName="Attachment5" Format="" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="2" Visible="True" Width="430" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment5',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="特定保管部門主管" Editor="text" FieldName="AssignChecker1" maxlength="0" ReadOnly="True" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="是外勞部" Editor="text" FieldName="IsForeignDept1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            </Columns>
                       </JQTools:JQDataForm>
                   </div>  
                </div>
                
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
