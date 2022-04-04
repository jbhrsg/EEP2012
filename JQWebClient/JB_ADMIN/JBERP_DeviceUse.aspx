<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_DeviceUse.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
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
            var StaTime = $('#dataFormMasterStaTime').closest('td');
            var EndTime = $('#dataFormMasterEndTime').closest('td').children();
            StaTime.append(' - ').append(EndTime)

            setTimeout("setWhere()", 10);

            //查詢條件=>預設是否有效
            $('input:radio[name=IsActive_Query_0][value=1]').attr('checked', true);

            //Grid設定寬度
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 480 });
            
            //查詢條件加驗證判斷
            $('#StaTime_Query,#EndTime_Query').validatebox({
                //required: true,
                validType: "client['IsTimeFormat', '輸入格式24小時制:時分(1010)']"
            });
        });

        function setWhere() {
            var date = new Date();
            var now = $.jbjob.Date.DateFormat(date, 'yyyy/MM/dd');
            $("#dataGridView").datagrid('setWhere', "IsActive=1 and ApplyDate='" + now + "'");// 
        }
        //查詢后添加固定條件
        function queryGrid(dg) {
                     
            if ($(dg).attr('id') == 'dataGridView') {
                var Stime = $('#StaTime_Query').val();
                var Etime = $('#EndTime_Query').val();
                if (!IsTimeFormat(Stime) || !IsTimeFormat(Etime)) {
                    alert("起迄時間格式錯誤！");
                } else {
                    var where = $(dg).datagrid('getWhere');
                    var value = $("input:radio[name='IsActive_Query_0']:checked").val();
                    if (where.length > 0) {
                        where = where + " and IsActive=" + value;
                    } else {
                        where = "IsActive = " + value;
                    }
                    $(dg).datagrid('setWhere', where);
                }
            }
        }
        //當選取工作歸屬時,重新設定工作需求(連動)
        function GetDeviceMasterID(rowData) {
            $("#dataFormMasterDeviceItemsID").combobox('setValue', "");
            $("#dataFormMasterDeviceItemsID").combobox('setWhere', "DeviceMasterID=" + rowData.DeviceMasterID);
        }
        //借用日期限制
        function IsApplyDateFormat(ApplyDate) {
            var date = new Date();
            var now = $.jbjob.Date.DateFormat(date, 'yyyy/MM/dd');            
            if (ApplyDate < now) return false;
            else
                return true;
        }
        //起迄時間格式限制
        function IsTimeFormat(Str) {
            if (Str != "") {
                var r = Str.match(/(?!0000)^(?:([01]\d|2[0-3])([0-5]\d)|2400)$/);
                if (r == null) return false;
                else
                    return true;
            }
            else
                return true;
        }
       
        //檢查Combo要選擇
        function checkCombo() {
            var DeviceMasterID = $("#dataFormMasterDeviceMasterID").combobox('getValue');
            if (DeviceMasterID == "" || DeviceMasterID == undefined) {
                alert('請選取設備類別！');
                $("#dataFormMasterDeviceMasterID").focus();
                return false;
            }
            var DeviceItemsID = $("#dataFormMasterDeviceItemsID").combobox('getValue');
            if (DeviceItemsID == "" || DeviceItemsID == undefined) {
                alert('請選取設備項目！');
                $("#dataFormMasterDeviceItemsID").focus();
                return false;
            }           
            var cnt=CheckTimeOver();
            if (cnt != "N") {               
                alert('此設備起迄時間已被 ' + cnt + ' 所佔用');
                return false;
            }
        }
        //檢查借用設備日期時間是否重疊
        function CheckTimeOver() {
            //新增時UseNO帶空值,修改時帶UseNO
            var UseNO = "";
            if (getEditMode($('#dataFormMaster')) == "updated") {//修改狀態
                UseNO = $('#dataFormMasterUseNO').val();               
            }
            var DeviceItemID = $("#dataFormMasterDeviceItemsID").combobox('getValue');//設備項目
            var Date = $("#dataFormMasterApplyDate").datebox('getValue');//借用日期
            var Stime = $('#dataFormMasterStaTime').val();
            var Etime = $('#dataFormMasterEndTime').val();
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sDeviceUse.DeviceUse', //連接的Server端，command
                data: "mode=method&method=" + "CheckTimeOver" + "&parameters=" + UseNO + "," + DeviceItemID + "," + Date + "," + Stime + "," + Etime, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    cnt = data;
                }
            });
            return cnt;
        }
        //JQDataGrid checkbox勾選
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        //控制是否可以修改 
        function UpdateRow(rowData) {
            var userid = getClientInfo("userid");            
            if (rowData.ApplyEmpID != userid ) {
                alert('申請者才可編輯！');
                return false; //取消編輯的動作 
            }
            var dt = new Date();
            var dtAdd = new Date(rowData.ApplyDate);
            if ($.jbjob.Date.DateFormat(dtAdd, 'yyyy/MM/dd') < $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')) {
                alert('舊有資料不可編輯！');
                return false; //取消編輯的動作 
            }
        }
        //設定查詢日期
        function SetApplyDate() {
            var dt = new Date();
            return $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd');
        }
        //Grid重整
        function GridReload() {
            $('#dataGridView').datagrid('reload');
        }

        var oldQuery = query;
        query = function (dgid) {
            if (dgid == '#dataGridView') {
                var queryDialog = getInfolightOption($(dgid)).queryDialog;
                if (queryDialog && $(queryDialog).form('validate')) return oldQuery.call(this, dgid);
            }
            else return oldQuery.call(this, dgid);
        };
      
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sDeviceUse.DeviceUse" runat="server" AutoApply="True"
                DataMember="DeviceUse" Pagination="True" QueryTitle="設備查詢" EditDialogID="JQDialog1"
                Title="設備借用一覽表" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="50px" QueryMode="Window" QueryTop="50px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnUpdate="UpdateRow" OnLoadSuccess="queryGrid" Width="920px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="借用編號" Editor="text" FieldName="UseNO" Format="" MaxLength="0" Width="90" />                    
                    <JQTools:JQGridColumn Alignment="left" Caption="設備項目" Editor="infocombobox" FieldName="DeviceItemsID" Format="" Width="120" EditorOptions="valueField:'DeviceItemsID',textField:'DeviceItemsName',remoteName:'sDeviceUse.infoDeviceItems',tableName:'infoDeviceItems',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="借用日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="起迄時間" Editor="text" FieldName="timerange" Format="" MaxLength="0" Width="80" Sortable="True" />
                   
                    <JQTools:JQGridColumn Alignment="left" Caption="用途說明" Editor="textarea" FieldName="OutLine" Format="" MaxLength="0" Width="150" />
                    <JQTools:JQGridColumn Alignment="center" Caption="確認借用" Editor="checkbox" FieldName="IsActive" Format="" Width="70" EditorOptions="on:1,off:0" FormatScript="genCheckBox" />
                   
                     <JQTools:JQGridColumn Alignment="center" Caption="申請人" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="申請日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Width="115" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="&gt;=" DataType="datetime" DefaultMethod="SetApplyDate" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止日期" Condition="&lt;=" DataType="datetime" DefaultMethod="SetApplyDate" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始時間" Condition="&gt;=" DataType="string" DefaultMethod="" Editor="text" FieldName="StaTime" Format="" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="80" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止時間" Condition="&lt;=" DataType="string" DefaultMethod="" Editor="text" FieldName="EndTime" Format="" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="80" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="設備項目" Condition="=" DataType="number" Editor="infocombobox" EditorOptions="valueField:'DeviceItemsID',textField:'DeviceItemsName',remoteName:'sDeviceUse.infoDeviceItems',tableName:'infoDeviceItems',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="DeviceItemsID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="是否有效" Condition="=" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:90,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'是',value:'1'},{text:'否',value:'0'}]" FieldName="IsActive" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="設備借用申請">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="DeviceUse" HorizontalColumnsCount="3" RemoteName="sDeviceUse.DeviceUse" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApply="checkCombo" OnApplied="GridReload" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="借用編號" Editor="text" FieldName="UseNO" Format="" maxlength="0" Width="130" ReadOnly="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="借用日期" Editor="datebox" FieldName="ApplyDate" Format="" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="設備類別" Editor="infocombobox" EditorOptions="valueField:'DeviceMasterID',textField:'DeviceMasterName',remoteName:'sDeviceUse.infoDeviceMaster',tableName:'infoDeviceMaster',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetDeviceMasterID,panelHeight:200" FieldName="DeviceMasterID" Width="130" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="設備項目" Editor="infocombobox" EditorOptions="valueField:'DeviceItemsID',textField:'DeviceItemsName',remoteName:'sDeviceUse.infoDeviceItems',tableName:'infoDeviceItems',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="DeviceItemsID" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起迄時間" Editor="text" FieldName="StaTime" Format="" MaxLength="0" Width="41" Span="1" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="EndTime" Format="" MaxLength="0" Span="2" Width="41" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="用途說明" Editor="textarea" FieldName="OutLine" Format="" Span="3" Width="365" />
                        <JQTools:JQFormColumn Alignment="left" Caption="確認借用" Editor="checkbox" FieldName="IsActive" Format="" maxlength="0" Width="50" EditorOptions="on:1,off:0" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="UseNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsActive" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0830" FieldName="StaTime" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ApplyDate" RemoteMethod="False" ValidateType="None" CheckMethod="IsApplyDateFormat" ValidateMessage="借用日期不可小於今天！" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DeviceMasterID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DeviceItemsID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="IsTimeFormat" CheckNull="True" FieldName="StaTime" RemoteMethod="False" ValidateType="None" ValidateMessage="輸入格式24小時制:時分(1010)" />
                        <JQTools:JQValidateColumn CheckMethod="IsTimeFormat" CheckNull="True" FieldName="EndTime" RemoteMethod="False" ValidateType="None" ValidateMessage="輸入格式24小時制:時分(1010)" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="OutLine" RemoteMethod="True" ValidateType="None" />                                              
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
