<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMOrders.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>   
        var flag = true; //定義一個全域變數，只有第一次執行

        $(document).ready(function () {
         
        });
        //-----------------------------------OnLoadSuccess   Orders-------------------------------------
        var dataFormOrders_OnLoadSuccess = function (data) {
            dataFormOrdersImg_onSuccess.call($(infoFileUploaddataFormOrdersWorkImg)); //照片處理      
            if (getEditMode($("#dataFormOrders")) == 'inserted')
            {
                //登入的工號帶入負責業務combobox
                var UserID = getClientInfo("UserID");          
                var data = $("#dataFormOrdersSalesID").combobox('getData');
                for (var i = 0; i < data.length; i++) {
                    if (data[i].EmpID == UserID) {
                        $("#dataFormOrdersSalesID").combobox('setValue', UserID);
                    }
                }
            }
        
        }
               
        var dataFormOrdersImg_onSuccess = function (data) {
            $(this).jbFileUploadWithPhoto();
        }

        //-----------------------------------OnLoadSuccess   OrdersDetails-------------------------------------
        var dataFormOrdersDetails_OnLoadSuccess = function (data) {
            if (flag) {
                //工期備註移至工期選項後面
                var WorkTimeReason = $('#dataFormDetailWorkTimeReason').closest('td').children();
                $("input[name='dataFormDetailWorkTime_0']:last").closest('td').append(' ').append(WorkTimeReason).append(' 年');
                flag = false;
            }
        }
        //-----------------------------------default-------------------------------------
        function DefaultPlanIndate() {
            var sDate = new Date();
            return $.jbjob.Date.DateFormat(sDate, 'yyyyMMdd').substring(0, 6);
        }

        //-----------------------------------validate-------------------------------------
        //檢查字串是否符合預定入境年月
        function CheckStrWildWord(str) {
            var r = str.match(/^(\d{4})(0[1-9]|1[0-2])$/);
            if (r == null) return false;
            var d = new Date(r[1], (r[2] - 1), 1);
            return (d.getFullYear() == r[1] && d.getMonth() == (r[2] - 1) && d.getDate() == 1);
        }
        //dataFormOrders聘工表號碼 過濾雇主
        function OnSelectEmployerID(rowData) {
            var EmployerID = rowData.EmployerID;//雇主
            $('#dataFormOrdersWorkNo').combobox('setWhere', "f.EmployerID ='" + EmployerID + "'")
        }
        //dataFormOrders函號 過濾雇主
        function OnFilterorg_okno() {
            var EmployerID = rowData.EmployerID;//雇主
            $('#dataFormDetailorg_okno').refval('setWhere', "cus_name ='" + EmployerID + "'")
        }

        //-----------------------------------訂單明細---------------------------------------------------------------------
        //刪除Details檢查
        function OnDeleteDetails() {
            var PersonQtyFinal = $("#dataGridDetail").datagrid('getSelected').PersonQtyFinal;;//取得當前主檔中選中的那個Data                
            if (PersonQtyFinal> 0){
                alert('目前人數>0,無法刪除!!');
                return false;
            } return true;
        }

        //dataFormDetail存檔前檢查
        function OnApplydataFormDetail() {
            //1.檢查訂單人數不可小於目前人數
            var QtyOriginal = $("#dataFormDetailPersonQtyOriginal").val();
            if (QtyOriginal != "") {
                var QtyFinal = $("#dataFormDetailPersonQtyFinal").val();
                if (QtyOriginal < QtyFinal) {
                    alert('訂單人數不可小於目前人數。');
                    return false;
                }
            }
            //2.檢查訂單明細 工期=>選擇遞補 2 選項需 填入? 年
            var value = $('#dataFormDetailWorkTime').options('getCheckedValue');
            if (value == "3") {
                var WorkTimeReason = $("#dataFormDetailWorkTimeReason").val();
                if (WorkTimeReason == "") {
                    alert('請填入遞補年期。');
                    return false;
                }
            }
           
        }        

        //選擇來源訂單=>帶出訂單資訊
        function OnSelectFromOrderNo(rowData) {
            var OrderNo = rowData.OrderNo;
            var dataGrid = $('#dataGridDetail');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sFWCRMOrders.FWCRMOrders', //連接的Server端，command
                data: "mode=method&method=" + "BindFWCRMOrders" + "&parameters=" + OrderNo, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示
                    //$('#dataGridDetail').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                    if (rows.length > 0) {
                        dataGrid.datagrid('loadData', { "total": 0, "rows": [] });
                        data = eval('(' + data + ')');
                        var appandRows = [];
                        var UserName = getClientInfo("UserName");
                        var today = getClientInfo('_today')
                        for (var j = 0; j < data.length; j++) {
                            appandRows.push({ OrderNo: '自動編號', Item: data[j].Item, PlanIndate: data[j].PlanIndate, PersonQtyOriginal: data[j].PersonQtyOriginal, PersonQtyFinal: data[j].PersonQtyFinal, Gender: data[j].Gender, org_okno: data[j].org_okno, Notes: data[j].Notes, CreateBy: UserName, CreateDate: today });
                        }
                        for (var i = 0; i < appandRows.length; i++) {
                            dataGrid.datagrid("appendRow", appandRows[i]);
                        }
                        //griddetail的footer強制更新
                        setFooter(dataGrid);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }
        //-----------------------------------★主檔存檔前檢查 dataFormOrders---------------------------------------------------------------------             
        function checkApplyData() {
            //1.檢查訂單明細
            var data = $("#dataGridDetail").datagrid("getRows");
            if (data.length == 0) {
                alert('無訂單明細。');
                return false;
            }
           

        }
      
    </script>  
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sFWCRMOrders.FWCRMOrders" runat="server" AutoApply="True"
                DataMember="FWCRMOrders" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="原訂單編號" Editor="text" FieldName="FromOrderNo" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聘工表號碼 " Editor="text" FieldName="WorkNo" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="引進國別" Editor="text" FieldName="NationalityID" Format="" Visible="true" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                   
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormOrders" Title="外籍勞工訂單" Width="680px" DialogTop="10px" DialogLeft="10px">
                <JQTools:JQDataForm ID="dataFormOrders" runat="server" DataMember="FWCRMOrders" HorizontalColumnsCount="2" RemoteName="sFWCRMOrders.FWCRMOrders" OnLoadSuccess="dataFormOrders_OnLoadSuccess" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApply="checkApplyData" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" maxlength="0" Width="120" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單類型" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:190,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'入境',value:'1'},{text:'承接',value:'2'},{text:'轉單',value:'3'}]" FieldName="OrderType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="220" />
                        <JQTools:JQFormColumn Alignment="left" Caption="負責業務" Editor="infocombobox" EditorOptions="valueField:'EmpID',textField:'NAME_C',remoteName:'sFWCRMOrders.infoSalesID',tableName:'infoSalesID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主名稱" Editor="infocombobox" EditorOptions="valueField:'EmployerID',textField:'EmployerName',remoteName:'sFWCRMOrders.infoEmployerID',tableName:'infoEmployerID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployerID,panelHeight:200" FieldName="EmployerID" Format="" ReadOnly="False" Width="180" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="來源訂單" Editor="inforefval" EditorOptions="title:'選擇來源訂單',panelWidth:590,remoteName:'sFWCRMOrders.infoOrderNo',tableName:'infoOrderNo',columns:[{field:'OrderNo',title:'訂單編號',width:92,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'EmployerName',title:'雇主名稱',width:180,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'PersonQtyOriginal',title:'訂單人數',width:92,align:'right',table:'',isNvarChar:false,queryCondition:''},{field:'PersonQtyFinal',title:'目前人數',width:92,align:'right',table:'',isNvarChar:false,queryCondition:''},{field:'CreateBy',title:'建立人員',width:92,align:'center',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'NationalityID',value:'NationalityID'}],whereItems:[],valueField:'OrderNo',textField:'OrderNo',valueFieldCaption:'OrderNo',textFieldCaption:'訂單編號',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,onSelect:OnSelectFromOrderNo,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="FromOrderNo" Format="" maxlength="0" NewRow="True" ReadOnly="False" Width="120" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聘工表號碼" Editor="infocombobox" FieldName="WorkNo" Format="" maxlength="0" Width="180" EditorOptions="valueField:'WorkNo',textField:'WorkNo',remoteName:'sFWCRMOrders.FWCRMWorkNo',tableName:'FWCRMWorkNo',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="False" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="引進國別" Editor="infocombobox" EditorOptions="valueField:'AutoKey',textField:'NationalityName',remoteName:'sFWCRMOrders.infoFWCRMNationality',tableName:'infoFWCRMNationality',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="NationalityID" Format="" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聘工表檔案" Editor="infofileupload" FieldName="WorkImg" MaxLength="100" Width="290" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf',isAutoNum:true,upLoadFolder:'Files/FWCRM/Orders',showButton:true,showLocalFile:false,onSuccess:dataFormOrdersImg_onSuccess,fileSizeLimited:'1024'" NewRow="False" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" NewRow="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="FWCRMOrdersDetails" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialog2" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" OnDelete="OnDeleteDetails" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormOrders" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sFWCRMOrders.FWCRMOrders" RowNumbers="True" Title="訂單明細" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="批次" Editor="text" FieldName="Item" Format="" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="預定年月" Editor="text" FieldName="PlanIndate" Format="" Width="55" />
                        <JQTools:JQGridColumn Alignment="right" Caption="訂單人數" Editor="text" FieldName="PersonQtyOriginal" Format="" Total="sum" Width="55" />
                        <JQTools:JQGridColumn Alignment="right" Caption="目前人數" Editor="text" FieldName="PersonQtyFinal" Total="" Width="55" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="性別" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'女',selected:'false'},{value:'2',text:'男',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Gender" Format="" Width="38" />
                        <JQTools:JQGridColumn Alignment="left" Caption="函號" Editor="text" FieldName="org_okno" Format="" Width="75" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="工期" Editor="infocombobox" FieldName="WorkTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sFWCRMOrders.infoWorkTime',tableName:'infoWorkTime',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="center" Caption="年期" Editor="text" FieldName="WorkTimeReason" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="32" />
                        <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Notes" Format="" Visible="True" Width="136" />
                        <JQTools:JQGridColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" Visible="False" Width="80" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="sgn_no" Editor="text" FieldName="sgn_no" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="sgn_type" Editor="text" FieldName="sgn_type" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="OrderNo" ParentFieldName="OrderNo" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" EditMode="Switch">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="FWCRMOrdersDetails" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApply="OnApplydataFormDetail" ParentObjectID="dataFormOrders" RemoteName="sFWCRMOrders.FWCRMOrders" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormOrdersDetails_OnLoadSuccess">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="批次" Editor="text" FieldName="Item" Format="" NewRow="True" ReadOnly="True" Width="40" />
                            <JQTools:JQFormColumn Alignment="left" Caption="預計入境年月" Editor="text" FieldName="PlanIndate" Format="" NewRow="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="訂單人數" Editor="numberbox" FieldName="PersonQtyOriginal" Format="" NewRow="True" OnBlur="OnApplydataFormDetail" ReadOnly="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="目前人數" Editor="numberbox" FieldName="PersonQtyFinal" NewRow="False" ReadOnly="True" Width="80" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:120,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'女',value:'1'},{text:'男',value:'2'}]" FieldName="Gender" Format="" NewRow="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="函號" Editor="inforefval" EditorOptions="title:'選擇函號',panelWidth:300,remoteName:'sFWCRMOrders.infoOrg_okno',tableName:'infoOrg_okno',columns:[{field:'cus_name',title:'雇主名稱',width:150,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'org_okno',title:'函號',width:110,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'sgn_no',value:'sgn_no'},{field:'sgn_type',value:'sgn_type'}],whereItems:[],valueField:'org_okno',textField:'cus_name',valueFieldCaption:'函號',textFieldCaption:'函號',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="org_okno" Format="" Span="1" Width="160" NewRow="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="工期" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:280,remoteName:'sFWCRMOrders.infoWorkTime',tableName:'infoWorkTime',valueField:'ID',textField:'Name',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="WorkTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="WorkTimeReason" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                            <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" EditorOptions="height:50" FieldName="Notes" Format="" NewRow="True" Span="2" Visible="True" Width="460" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" NewRow="True" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="sgn_no" Editor="text" FieldName="sgn_no" NewRow="False" ReadOnly="False" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="sgn_type" Editor="text" FieldName="sgn_type" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="OrderNo" ParentFieldName="OrderNo" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="Item" NumDig="0" />
                </JQTools:JQDialog>

                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormOrders" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="OrderNo" RemoteMethod="True" />
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" RemoteMethod="True" DefaultValue="_usercode" FieldName="UserID" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormOrders" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="WorkNo" RemoteMethod="True" ValidateMessage="請選擇聘工表" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="OrderType" RemoteMethod="True" ValidateMessage="請選擇訂單類型" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="NationalityID" RemoteMethod="True" ValidateMessage="請選擇引進國別	" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="WorkImg" RemoteMethod="True" ValidateMessage="請選擇聘工表檔案" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesID" RemoteMethod="True" ValidateMessage="請選擇業務" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EmployerID" RemoteMethod="True" ValidateMessage="請選擇雇主名稱	" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="WorkImg" RemoteMethod="True" ValidateMessage="請夾帶檔案" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="DefaultPlanIndate" FieldName="PlanIndate" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="Gender" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="WorkTime" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckMethod="CheckStrWildWord" CheckNull="True" FieldName="PlanIndate" RemoteMethod="False" ValidateMessage="預定入境年月格式不對！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PersonQtyOriginal" RemoteMethod="True" ValidateMessage="訂單人數不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="org_okno" RemoteMethod="True" ValidateMessage="請選擇函號！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
