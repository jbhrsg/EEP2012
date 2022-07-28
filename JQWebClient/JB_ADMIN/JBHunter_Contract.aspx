<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_Contract.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 合約起迄日期合併
            var ConStdDate = $('#dataFormMasterConStdDate').closest('td');
            var ConEndDate = $('#dataFormMasterConEndDate').closest('td').children();
            ConStdDate.append(' - ').append(ConEndDate);
            // 查詢合約起迄日期合併
            var QryConStdDate = $('#ConStdDate_Query').closest('td');
            var QryConEndDate = $('#ConEndDate_Query').closest('td').children();
            QryConStdDate.append(' - ').append(QryConEndDate);
            // 合約起迄日期合併
            var ContCloseDate = $('#dataFormMasterContCloseDate').closest('td');
            var IsCloseJobs = $('#dataFormMasterIsCloseJobs').closest('td').children();
            ContCloseDate.append('  關閉所有職缺').append(IsCloseJobs);
            //建立 dialog
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 320 });
            // 合約起迄日期合併
            var CreateBy = $('#dataFormMasterCreateBy').closest('td');
            var CreateDate = $('#dataFormMasterCreateDate').closest('td').children();
            CreateBy.append('     建立日期').append(CreateDate);

            $('#dataFormMasterConStdDate').datebox({
                onSelect: function (date) {
                    FunDate();
                }
            }).combo('textbox').blur(function () {
                FunDate();
            });
            $('#dataFormMasterConEndDate').datebox({
                onSelect: function (date) {
                    FunDate();
                }
            }).combo('textbox').blur(function () {
                FunDate();
            });
        });
   function checkCombo() {
       var dataFormMasterCustID = $("#dataFormMasterCustID").refval('getValue');
        if (dataFormMasterCustID == "" || dataFormMasterCustID == undefined) {
            alert('注意!!,未選取客戶,請選取');
            $("#dataFormMasterCustID").focus();
            return false;
        }
   };
   //焦點欄位變顏色
   $(function () {
       $("input, select, textarea").focus(function () {
           $(this).css("background-color", "yellow");
       });

       $("input, select, textarea").blur(function () {
           $(this).css("background-color", "white");
       });
   });
    var FunDate = function () {
        var S = $('#dataFormMasterConStdDate').combo('textbox').val();
        var E = $('#dataFormMasterConEndDate').combo('textbox').val();
        var ans = $.jbDateDiff('months', S, E);
        $('#dataFormMasterContEffYear').val(ans);
    }
    function genCheckBox(val) {
        if (val)
            return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
        else
            return "<input  type='checkbox' onclick='return false;' />";
    }
    function MasterGridReload() {
        $("#dataGridView").datagrid('reload');
    }
    function CheckDataGridViewDelete() {
        var row = $('#dataGridView').datagrid('getSelected');//取得當前主檔中選中的那個Data
        var cnt;
        $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sContract.HUT_Contract', //連接的Server端，command
            data: "mode=method&method=" + "CheckDelContract" + "&parameters=" + row.ContractNO, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
            cache: false,
            async: false,
            success: function (data) {
                if (data != false) {
                    cnt = $.parseJSON(data);
                }
            }
        });
        if ((cnt == "0") || (cnt == "undefined")) {

            return true;
        }
        else {
            alert('此合約已有職缺參考使用,無法刪除!!');
            return false;
        }
    }
    //檢查合約代號是否重複
    function CheckContractNODul() {
        var dataFormMasterCustID = $("#dataFormMasterCustID").refval('getValue');
        if (dataFormMasterCustID == "" || dataFormMasterCustID == undefined) {
            alert('注意!!,未選取客戶,請選取');
            $("#dataFormMasterCustID").focus();
            return false;
        }
        if (getEditMode($("#dataFormMaster")) == 'inserted') {
            var ConstractNO = $('#dataFormMasterContractNO').val();
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sContract.HUT_Contract', //連接的Server端，command
                data: "mode=method&method=" + "CheckContractNODul" + "&parameters=" + ConstractNO, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        cnt = $.parseJSON(data);
                    }
                }
            });
            if ((cnt == "0") || (cnt == "undefined")) {
                return true;
            }
            else {
                alert('注意!!此合約編號已存在');
                $('#dataFormMasterContractNO').focus;
                return false;
            }
        }
    }
    function AfterMonths(){
        var dt = new Date();
        var aDate = new Date($.jbDateAdd('days',30, dt));
        return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
    }
    //取得3個月後日期
    function AfterYears() {
        var dt = new Date();
        var aDate = new Date($.jbDateAdd('months',3, dt));
        return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
    }
    //取得3年前日期
    function BeforeYears() {
        var dt = new Date();
        var aDate = new Date($.jbDateAdd('years',-3, dt));
        return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sContract.HUT_Contract" runat="server" AutoApply="True"
                DataMember="HUT_Contract" Pagination="True" QueryTitle="快速查詢" EditDialogID="JQDialog1"
                Title="獵才合約" AlwaysClose="False" QueryMode="Window" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="90px" QueryTop="120px" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDelete="CheckDataGridViewDelete" ColumnsHibeable="False" RecordLockMode="None" Width="1120px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustID" MaxLength="0" Width="75" EditorOptions="" Sortable="True" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶電話" Editor="text" FieldName="CustomerTel" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" MaxLength="0" Width="90" EditorOptions="valueField:'CustID',textField:'CustShortName',remoteName:'sContract.HUT_Customer',tableName:'HUT_Customer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="合約編號" Editor="text" FieldName="ContractNO" MaxLength="0" Width="90" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="合約性質" Editor="text" FieldName="ContContent" MaxLength="0" Width="140" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起始日期" Editor="text" FieldName="ConStdDate" Width="70" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="終止日期" Editor="text" FieldName="ConEndDate" Width="70" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="center" Caption="有效月數" Editor="text" FieldName="ContEffYear" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="簽約日期" Editor="text" FieldName="ContDate" Width="70" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務單位" Editor="text" EditorOptions="" FieldName="SalesTeamName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="合約關閉日" Editor="text" FieldName="ContCloseDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="合約自動延展" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsContinue" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="True" Width="85" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="付款方式" Editor="textarea" FieldName="PayDesc" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務人員" Editor="text" FieldName="HunterName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="快速查詢"  Visible="True" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn Caption="客戶電話" Condition="%%" DataType="string" Editor="text" FieldName="CustomerTel" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="180" />
                    <JQTools:JQQueryColumn Caption="客戶簡稱" Condition="%%" DataType="string" Editor="text" EditorOptions="" FieldName="CustShortName" NewLine="True" RemoteMethod="False" Width="180" />
                    <JQTools:JQQueryColumn Caption="合約編號" Condition="%%" DataType="string" Editor="text" FieldName="ContractNO" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="180" />
                    <JQTools:JQQueryColumn Caption="業務單位" Condition="=" DataType="string" Editor="infocombobox" FieldName="HUT_Contract.SalesTeamID" NewLine="True" RemoteMethod="False" Width="184"  EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200"/>
                    <JQTools:JQQueryColumn AndOr="And" Caption="業務人員" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sContract.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="184" />
                    <JQTools:JQQueryColumn AndOr="" Caption="合約期間" Condition="&gt;=" DataType="datetime" DefaultMethod="BeforeYears" DefaultValue="" Editor="datebox" FieldName="ConStdDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" Format="yyyy/mm/dd" />
                    <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="&gt;=" DataType="datetime" DefaultMethod="" DefaultValue="_today" Editor="datebox" FieldName="ConEndDate" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" />
                
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="獵才合約" EditMode="Dialog" DialogLeft="45px" DialogTop="45px" Width="660px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HUT_Contract" HorizontalColumnsCount="2" RemoteName="sContract.HUT_Contract" Closed="False" ContinueAdd="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApply="CheckContractNODul" OnApplied="MasterGridReload" disapply="False" IsRejectON="False">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶簡稱" Editor="inforefval" EditorOptions="title:'選擇客戶',panelWidth:350,remoteName:'sContract.HUT_Customer',tableName:'HUT_Customer',columns:[],columnMatches:[],whereItems:[],valueField:'CustID',textField:'CustShortName',valueFieldCaption:'客戶代號',textFieldCaption:'客戶名稱',cacheRelationText:false,checkData:false,showValueAndText:false,selectOnly:true" FieldName="CustID" maxlength="0" Span="2" Width="123" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約編號" Editor="text" FieldName="ContractNO" maxlength="20"   Width ="120" ReadOnly="False" Span="1" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約性質" Editor="textarea" FieldName="ContContent" maxlength="256" Width="310" EditorOptions="height:30"  Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起迄日期" Editor="datebox" FieldName="ConStdDate" Width="123" Format="yyyy/mm/dd" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="datebox" FieldName="ConEndDate" Width="123" Format="yyyy/mm/dd" Span="2" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="有效月數" Editor="text" FieldName="ContEffYear" maxlength="30" Width="120" Span="2"/> 
                        <JQTools:JQFormColumn Alignment="left" Caption="簽約日期" Editor="datebox" FieldName="ContDate" Width="123" Span="2" Format="yyyy/mm/dd" maxlength="0" ReadOnly="False"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="自動延展" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsContinue" MaxLength="0" ReadOnly="False" Span="2" Visible="True" Width="15" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務單位" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" maxlength="0" ReadOnly="False" Span="2" Width="123" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約備註" Editor="textarea" EditorOptions="height:40" FieldName="ContNotes" Span="2" Width="310" Visible="True" MaxLength="1024" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約關閉日" Editor="datebox" FieldName="ContCloseDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="123" />
                        <JQTools:JQFormColumn Alignment="right" Caption=" " Editor="checkbox" FieldName="IsCloseJobs" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款方式" Editor="textarea" EditorOptions="height:80" FieldName="PayDesc" MaxLength="1024" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="480" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ContClosedBy" Editor="text" FieldName="ContClosedBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy"  Width="123" Visible="True" ReadOnly="True" EditorOptions="" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="datebox" FieldName="CreateDate"  Width="138"  Visible="True" Format="" ReadOnly="True" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="LastUpdateBy"    Width="150" Visible="False" MaxLength="0" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="text" FieldName="LastUpdateDate"  Width="150" Visible="False" MaxLength="0" ReadOnly="False" Span="1" />
                       </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="ContDate" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn DefaultValue="36" FieldName="ContEffYear" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn DefaultValue="_usercode" FieldName="LastUpdateBy" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn DefaultValue="中高階獵才合約" FieldName="ContContent" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="2" FieldName="SalesTeamID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="ConStdDate" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn DefaultMethod="AfterMonths" DefaultValue="" FieldName="ConEndDate" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContractNO" RemoteMethod="True" ValidateType="None" CheckMethod="" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
