<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_CustomersB.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {                     
            　
            //設定 Grid QueryColunm panel寬度調整
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 1050 });
                                                        
            //客戶電話 區號+電話 合併為同TD顯示
            var Custarea1 = $('#dataFormMasterCustomerTelArea').closest('td');
            var Custcode1 = $('#dataFormMasterCustomerTel').closest('td').children();
            Custarea1.append('-').append(Custcode1);

            //獵才業務+獵才顧問+網址 連結顯示
            var SalesID = $('#dataFormMasterSalesID').closest('td');
            var HunterID = $('#dataFormMasterHunterID').closest('td').children();
            var Url = $('#dataFormMasterCustomerUrl').closest('td').children();
            SalesID.append('&nbsp;&nbsp;&nbsp;獵才顧問').append(HunterID).append('&nbsp;&nbsp;&nbsp;網址').append(Url);

            //在客戶名稱後加入經濟部商業司超連結
            var EconmicLink = $("<a>").attr({ 'href': 'https://findbiz.nat.gov.tw/fts/query/QueryBar/queryInit.do' }).attr({ 'target': '_blank' }).text("    經濟部商業司");
            var CustName = $('#dataFormMasterCustName').closest('td');
            CustName.append(EconmicLink);

            //--------------聯絡人頁籤-----------------------------------------------------------------
            //客戶聯絡人區號+電話+分機+Mail 合併為同TD顯示
            var area1 = $('#DFContactPersonContactTelArea').closest('td');
            var code1 = $('#DFContactPersonContactTel').closest('td').children();
            var ext1 = $('#DFContactPersonContactTelExt').closest('td').children();
            var Mail = $('#DFContactPersonContacteMail').closest('td').children();
            area1.append('-').append(code1).append(' 分機').append(ext1).append('&nbsp;&nbsp;eMail').append(Mail);
            //手機 =>國碼+手機合併
            var Mobile1Area = $('#DFContactPersonContactMobile1Area').closest('td');
            var Mobile1 = $('#DFContactPersonContactMobile1').closest('td').children();
            var Mobile2Area = $('#DFContactPersonContactMobile2Area').closest('td').children();
            var Mobile2 = $('#DFContactPersonContactMobile2').closest('td').children();
            Mobile1Area.append('(國碼)&nbsp;&nbsp;').append(Mobile1).append('例:0933-123456&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;手機2&nbsp;').append(Mobile2Area).append('(國碼)&nbsp;&nbsp;').append(Mobile2).append('例:0933-123456');

            //即時通類型1 即時通類型+帳號合併縣顯示
            var imtype1 = $('#DFContactPersonContIMType1').closest('td');
            var imno1 = $('#DFContactPersonContIMNO1').closest('td').children();
            //即時通類型2 即時通類型+帳號合併縣顯示
            var imtype2 = $('#DFContactPersonContIMType2').closest('td').children();
            var imno2 = $('#DFContactPersonContIMNO2').closest('td').children();
            imtype1.append('&nbsp;&nbsp;&nbsp;帳號').append(imno1).append('&nbsp;&nbsp;&nbsp;即時通2').append(imtype2).append('&nbsp;&nbsp;&nbsp;帳號').append(imno2);

            //--------------客戶職缺傳入客戶代號 => 查詢客戶---------------------------------------------------
            var parameter = Request.getQueryStringByName("CustID");
            if (parameter != "") {
                $("#CustID_Query").val(parameter); 
                queryGrid('#dataGridView');
            }
                    
        });
        
        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridView') {
                //查詢條件
                var result = [];
                var CustName = $('#CustName_Query').val();//客戶名稱
                var CustTaxNo = $('#CustTaxNo_Query').val();//統一編號
                var CustomerTel = $('#CustomerTel_Query').val();//客戶電話
                var HunterID = $('#HunterID_Query').combobox('getValue');//獵才顧問
                var CustID = $('#CustID_Query').val();//客戶編號	                

                if (CustName != '') result.push("c.CustName like '%" + CustName + "%'");
                if (CustTaxNo != '') result.push("c.CustTaxNo like '%" + CustTaxNo + "%'");
                if (CustomerTel != '') result.push("c.CustomerTel like '%" + CustomerTel + "%'");
                if (HunterID != '') result.push("c.HunterID = " + HunterID );
                if (CustID != '') result.push("c.CustID like '%" + CustID + "%'");
               
                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }

        function OnLoadDF() {
            
            if (getEditMode($("#dataFormMaster")) == "inserted") {//唯讀
                $("#dataFormMasterCustName").prop('readonly', false);//客戶名稱
                $("#dataFormMasterCustomerTelArea").prop('readonly', false);//電話
                $("#dataFormMasterCustomerTel").prop('readonly', false);//電話
                $("#dataFormMasterCustTaxNo").prop('readonly', false);//統一編號	
                $("#dataFormMasterCustomerAddress").prop('readonly', false);//地址
                $("#dataFormMasterSalesTeamID").combobox("enable");//業務單位
                $("#dataFormMasterSalesID").combobox("enable");//獵才業務
                $("#dataFormMasterHunterID").combobox("enable");//獵才顧問    

            } else {//取消唯讀
                $('#dataFormMasterCustName').prop('readonly', true);
                $('#dataFormMasterCustomerTelArea').prop('readonly', true);
                $('#dataFormMasterCustomerTel').prop('readonly', true);
                $('#dataFormMasterCustTaxNo').prop('readonly', true);
                $('#dataFormMasterCustomerAddress').prop('readonly', true);
                $("#dataFormMasterSalesTeamID").combobox("disable");
                $("#dataFormMasterSalesID").combobox("disable");
                $("#dataFormMasterHunterID").combobox("disable");
            }
        }
              
        //--------------------------聯繫紀錄-----------------------------------
        function ContactDateLink(value, row, index) {
            if (value != null)
                return "<a href='javascript: void(0)' onclick='LinkContactDate(" + index + ");' style='color:red;'>" + value + "</a>";
            else
                return "<a href='javascript: void(0)' onclick='LinkContactDate(" + index + ");' style='color:red;'>新增</a>";
        }

        // open聯繫維護紀錄畫面 dialog
        function LinkContactDate(index) {
            $("#dataGridView").datagrid('selectRow', index);        
            openForm('#Dialog_ContactRecord', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
        }

        //完整顯示Grid聯繫紀錄
        function ShowAllGrid(value) {
            return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
        }
        //聯繫維護紀錄有變更時重整
        function OnAppliedContactRecord() {
            $("#dataGridView").datagrid('reload');
            $("#DGContactRecord").datagrid('reload');

        }
        function OnDeletedContactRecord() {
            $("#dataGridView").datagrid('reload');
            $("#DGContactRecord").datagrid('reload');
        }
        //--------------------------聯絡人紀錄-----------------------------------    
        //清空選擇
        function OnLoadSuccessContactPerson() {            
            if ($('#DFContactPersonContactMobile1Area').combobox('getValue') == "") {
                $('#DFContactPersonContactMobile1Area').combobox('setValue', "");
            }
            if ($('#DFContactPersonContactMobile2Area').combobox('getValue') == "") {
                $('#DFContactPersonContactMobile2Area').combobox('setValue', "");
            }
        }

        //聯繫維護紀錄有變更時重整
        function OnAppliedContactPerson() {
            //$("#dataGridView").datagrid('reload');
            $("#DGContactPerson").datagrid('reload');

        }
        //預設值- 電話
        function ContactTelAreaDefault() {
            return $("#dataFormMasterCustomerTelArea").val();
        }
        function ContactTelDefault() {
            return $("#dataFormMasterCustomerTel").val();
        }
       
        function CheckContactMobile1(phone) {
            var regex = /^09\d{2}-\d{6}$/;
            if (!regex.test(phone)) {
                $("#DFContactPersonContactMobile1").focus();
                return false;
            } else {
                return true;
            }
        }
        function CheckContactMobile2(phone) {
            var regex = /^09\d{2}-\d{6}$/;
            if (!regex.test(phone)) {
                $("#DFContactPersonContactMobile2").focus();
                return false;
            } else {
                return true;
            }
        }
        //-------------------------檔案管理-----------------------------------
        function OnApplyCustomerFile() {            
            if ($("#infoFileUploadDFCustomerFileCustFile").val() == "") {
                 alert('請選擇檔案！');
                 return false;
             }            
        }
        function OnAppliedCustomerFile() {
            //alert('請選擇檔案！');
            $("#DGCustomerFile").datagrid('reload');
        }
        //刪除檔案
        function DeleteCustFile() {
            var row = $('#DGCustomerFile').datagrid('getSelected'); //取得當前主檔中選中的那個Data
            if (row != null) {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sCustomersJobs.HUT_Customer', //連接的Server端，command
                    data: "mode=method&method=" + "DelCustomerFile" + "&parameters=" + row.AutoKey + "," + row.CustFile, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });
                if (cnt == "0") {
                    $("#DGCustomerFile").datagrid('reload');
                }
                else {
                    alert('此客戶檔案不存在!');
                    return false;
                }
            } 
        }
        function OnDeletFile() {
            var pre = confirm("確認刪除?");
            if (pre == true) {
                //callServerMethod
                DeleteCustFile();
                return false; //取消刪除的動作
            }
            else {
                return false;
            }

        }
        

        //------------------------合約紀錄-----------------------------------------------        
        function ContractLink(value, row, index) {
            var at = row.ActiveContract;
            if (value == "0") {
                return "";
            }
            else if (at == "0") {
                return "無效";
            } else  {
                return "有效";
            }
        }        

        //焦點欄位變顏色
        $(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "yellow");
            });

            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });
        });
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox'  onclick='return false;'  />";
        }
      
       
        function MasterGridReload() {
            //再打開一次網頁---------------------------------------------------------------------------------------
            //if (getEditMode($("#dataFormMaster")) == 'updated') {
            //    openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
            //} else {
                //reload
                $("#dataGridView").datagrid('reload');
            //}
        }
       //檢查客戶記錄是否可刪除
        function CheckDelCustomer() {
            var row = $('#dataGridView').datagrid('getSelected'); //取得當前主檔中選中的那個Data
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCustomersJobs.HUT_Customer', //連接的Server端，command
                data: "mode=method&method=" + "CheckDelCustomer" + "&parameters=" + row.CustID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
                alert('此客戶已有合約使用,無法刪除!!');
                return false;
            }
        }

        //---------------呼叫開啟Job Tab--------------------------------------------------------------------------------
        function OpenJobTab(value, row, index) {
            return "<a href='javascript: void(0)' onclick='LinkJobTab(" + index + ");' style='color:blue;'>" + value + "</a>";
        }        
        function LinkJobTab(index) {
            $("#dataGridView").datagrid('selectRow', index);
            var rows = $("#dataGridView").datagrid('getSelected');
            var CustID = rows.CustID;
            parent.addTab('職缺資料維護', './JB_ADMIN/JBHunter_Jobs.aspx?CustID=' + CustID);
        }
       
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 1134px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td colspan="2" class="auto-style1">
                        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
                        <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCustomersJobs.HUT_Customer" runat="server" AutoApply="True" 
                            DataMember="HUT_Customer" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                            Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="15,30,45,60,65" PageSize="15" QueryAutoColumn="False" QueryLeft="10px" QueryMode="Panel" QueryTop="10px" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnDelete="CheckDelCustomer" ColumnsHibeable="False" RecordLockMode="None" Width="1050px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="CustID" MaxLength="20" Width="60" EditorOptions="" Visible="True" Sortable="False" />
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" MaxLength="30" Width="223" Sortable="True" />
                                 <JQTools:JQGridColumn Alignment="center" Caption="最後聯繫日" Editor="datebox" FieldName="ContactDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85" FormatScript="ContactDateLink" Format="yyyy/mm/dd" />
                                <JQTools:JQGridColumn Alignment="left" Caption="聯繫人員" Editor="text" EditorOptions="" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75" />
                                <JQTools:JQGridColumn Alignment="center" Caption="統一編號" Editor="text" FieldName="CustTaxNo" MaxLength="0" Width="92" />
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶電話" Editor="text" FieldName="CustTel" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="118" />
                                <JQTools:JQGridColumn Alignment="center" Caption="有效職缺" Editor="text" FieldName="JobCount" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75" FormatScript="OpenJobTab" />
                                <JQTools:JQGridColumn Alignment="center" Caption="合約狀態" Editor="text" EditorOptions="" FieldName="bContract" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="75" Format="" FormatScript="ContractLink" />
                                <JQTools:JQGridColumn Alignment="center" Caption="最後修改人員" Editor="text" EditorOptions="" FieldName="LastUpdateBy" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="80" />
                                <JQTools:JQGridColumn Alignment="center" Caption="修改日期" Editor="datebox" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="87">
                                </JQTools:JQGridColumn>
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增客戶" />  
                                <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢條件" />                                                                                                                            
                            </TooItems>
                            <QueryColumns>
                                <JQTools:JQQueryColumn Caption="客戶名稱" Condition="%%" DataType="string" Editor="text" FieldName="CustName" NewLine="False" RemoteMethod="False" Width="80" AndOr="" />
                                <JQTools:JQQueryColumn Caption="統一編號" Condition="%%" DataType="string" Editor="text" FieldName="CustTaxNo" NewLine="False" RemoteMethod="False" Width="80" AndOr="and" />
                                <JQTools:JQQueryColumn AndOr="" Caption="客戶電話" Condition="%%" DataType="string" Editor="text" FieldName="CustomerTel" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="獵才顧問" Condition="=" DataType="string" DefaultValue="" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="115" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="客戶編號" Condition="=" DataType="string" Editor="text" FieldName="CustID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="55" />
                            </QueryColumns>
                        </JQTools:JQDataGrid>
                        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="客戶資料" DialogLeft="50px" DialogTop="5px" Width="1100px" Wrap="False" EditMode="Switch">
                            <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HUT_Customer" HorizontalColumnsCount="6" RemoteName="sCustomersJobs.HUT_Customer" Closed="False" ContinueAdd="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="MasterGridReload" disapply="False" IsRejectON="False"  IsAutoPause="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" ParentObjectID="" ChainDataFormID="dataFormMaster1" OnLoadSuccess="OnLoadDF">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" MaxLength="128" Span="2" Width="200" RowSpan="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="CustomerTelArea" MaxLength="0" Width="46" Span="1" NewRow="False" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CustomerTel" MaxLength="20" Width="80" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="地址" Editor="text" FieldName="CustomerAddress" MaxLength="128" Width="372" Span="1" NewRow="False" ReadOnly="False" Visible="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶編號" Editor="text" FieldName="CustID" MaxLength="20" NewRow="False" ReadOnly="True" Span="1" Visible="True" Width="70" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="CustTaxNo" MaxLength="10" Width="90" Span="1" NewRow="True" Visible="True" ReadOnly="False" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="業務單位" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" MaxLength="0" Span="1" Visible="True" Width="110" ReadOnly="False" NewRow="False" RowSpan="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="獵才業務" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" MaxLength="0" ReadOnly="False" Span="3" Visible="True" Width="120" NewRow="False" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" MaxLength="0" Width="120" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CustomerUrl" MaxLength="256" Span="1" Width="220" NewRow="False" Visible="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" MaxLength="20" Width="180" Span="1" Visible="False"/>
                                    <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" MaxLength="0" Width="180" Span="1" Visible="False" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" NewRow="False" ReadOnly="False" RowSpan="1" Format=""/>
                                    <JQTools:JQFormColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy"  Width="180" MaxLength="20" Span="1" Visible="False" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" MaxLength="0" Visible="False" Width="180" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" Format="" />                                                                     
                                </Columns>

                            </JQTools:JQDataForm>

                            <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQDefaultColumn DefaultValue="自動編號" FieldName="CustID" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="http://" FieldName="CustomerUrl" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                                </Columns>
                            </JQTools:JQDefault>
                            <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CustName" RemoteMethod="True" ValidateMessage="" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CustomerTel" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="ContacteMail1" RemoteMethod="True" ValidateType="EMail" />
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="ContacteMail2" RemoteMethod="True" ValidateType="EMail" />
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="ContacteMail3" RemoteMethod="True" ValidateType="EMail" />
                                </Columns>
                            </JQTools:JQValidate>
                          
                                
                                     <JQTools:JQDataForm ID="dataFormMaster1" runat="server" AlwaysReadOnly="False" ChainDataFormID="" Closed="False" ContinueAdd="False" DataMember="HUT_Customer" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="True" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="" RemoteName="sCustomersJobs.HUT_Customer" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                                         <Columns>
                                             <JQTools:JQFormColumn Alignment="left" Caption="客戶編號" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                             <JQTools:JQFormColumn Alignment=" " Caption="公告欄" Editor="textarea" EditorOptions="height:140" FieldName="RecruitNotes" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="550" />
                                             <JQTools:JQFormColumn Alignment="left" Caption="福利要項" Editor="textarea" FieldName="Benefits" MaxLength="0" Width="426" EditorOptions="height:140" Span="1" />

                                         </Columns>
                                     </JQTools:JQDataForm>                                                          
                                                                
                                     <JQTools:JQDataGrid ID="DGContactPerson" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_CustomerContactPerson" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogContactPerson" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnUpdate="" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.HUT_Customer" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                                         <Columns>
                                             <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="ContactName" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="105">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="職稱" Editor="text" FieldName="ContactTitle" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="138">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="ContactDept" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="區碼" Editor="text" FieldName="ContactTelArea" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="ContactTel" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="分機" Editor="text" FieldName="ContactTelExt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="right" Caption="國碼" Editor="text" FieldName="ContactMobile1Area" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="手機1" Editor="text" FieldName="ContactMobile1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="right" Caption="國碼" Editor="text" FieldName="ContactMobile1Area2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContactMobile2Area" Editor="text" FieldName="ContactMobile2Area" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="手機2" Editor="text" FieldName="ContactMobile2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="center" Caption="狀態" Editor="text" FieldName="ContactStatusName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="center" Caption="修改時間" Editor="text" FieldName="UpdateDate" Format="yyyy/mm/dd HH:MM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="text" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContactStatus" Editor="text" FieldName="ContactStatus" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContIMType1" Editor="text" FieldName="ContIMType1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContIMNO1" Editor="text" FieldName="ContIMNO1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContIMType2" Editor="text" FieldName="ContIMType2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContIMNO2" Editor="text" FieldName="ContIMNO2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContacteMail" Editor="text" FieldName="ContacteMail" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContacteNotes" Editor="text" FieldName="ContacteNotes" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                         </Columns>
                                         <RelationColumns>
                                             <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                         </RelationColumns>
                                         <TooItems>
                                             <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增聯絡人" />
                                         </TooItems>
                            </JQTools:JQDataGrid>
                            <JQTools:JQDialog ID="JQDialogContactPerson" runat="server" BindingObjectID="DFContactPerson" DialogLeft="80px" DialogTop="80px" Title="聯絡人維護" Width="850px">
                                <JQTools:JQDataForm ID="DFContactPerson" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_CustomerContactPerson" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedContactPerson" OnLoadSuccess="OnLoadSuccessContactPerson" ParentObjectID="dataFormMaster" RemoteName="sCustomersJobs.HUT_Customer" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="ContactName" MaxLength="20" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="110" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="ContactTitle" MaxLength="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="部門" Editor="text" FieldName="ContactDept" MaxLength="0" NewRow="False" Span="1" Visible="True" Width="150" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="狀態" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ContactStatusName',remoteName:'sCustomersJobs.HUT_CustomerContactStatus',tableName:'HUT_CustomerContactStatus',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="ContactStatus" MaxLength="0" NewRow="False" Span="1" Visible="True" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="ContactTelArea" MaxLength="0" NewRow="True" RowSpan="1" Span="4" Visible="True" Width="39" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTel" MaxLength="20" NewRow="False" Span="1" Visible="True" Width="78" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTelExt" MaxLength="10" NewRow="False" Visible="True" Width="50" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContacteMail" MaxLength="128" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="330" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="手機1" Editor="infocombobox" EditorOptions="valueField:'ContactMobile1Area',textField:'ContactMobile1Area',remoteName:'sCustomersJobs.infoContactMobile1Area',tableName:'infoContactMobile1Area',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactMobile1Area" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="90" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactMobile1" MaxLength="20" NewRow="True" OnBlur="" Span="1" Width="130" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'ContactMobile1Area',textField:'ContactMobile1Area',remoteName:'sCustomersJobs.infoContactMobile1Area',tableName:'infoContactMobile1Area',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactMobile2Area" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactMobile2" MaxLength="0" NewRow="False" Span="1" Width="130" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="即時通1" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'IMNAME',remoteName:'sCustomersJobs.HUT_ZIMType',tableName:'HUT_ZIMType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="ContIMType1" MaxLength="20" NewRow="True" Span="4" Width="90" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" EditorOptions="height:80" FieldName="ContacteNotes" MaxLength="256" NewRow="True" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="700" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContIMNO1" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'ID',textField:'IMNAME',remoteName:'sCustomersJobs.HUT_ZIMType',tableName:'HUT_ZIMType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="ContIMType2" MaxLength="20" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContIMNO2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="UpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateDate" Editor="text" FieldName="UpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    </Columns>
                                    <RelationColumns>
                                        <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                    </RelationColumns>
                                </JQTools:JQDataForm>
                            </JQTools:JQDialog>
                            <JQTools:JQDefault ID="JQDefault3" runat="server" BindingObjectID="DFContactPerson" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCODE" FieldName="UserID" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="UpdateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="UpdateDate" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="ContactStatus" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="ContactTelAreaDefault" FieldName="ContactTelArea" RemoteMethod="False" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="ContactTelDefault" FieldName="ContactTel" RemoteMethod="False" />
                                </Columns>
                            </JQTools:JQDefault>
                            <JQTools:JQValidate ID="JQValidate3" runat="server" BindingObjectID="DFContactPerson" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactName" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactTitle" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckMethod="CheckContactMobile1" CheckNull="True" FieldName="ContactMobile1" RemoteMethod="False" ValidateMessage="格式不對！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckMethod="CheckContactMobile2" CheckNull="False" FieldName="ContactMobile2" RemoteMethod="False" ValidateMessage="格式不對！" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>
                                                                
                                     <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="DFContactPerson" FieldName="AutoKey" NumDig="1" />                                

                                     <JQTools:JQDataGrid ID="DGCustomerFile" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_CustomerFile" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogCustomerFile" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnUpdate="" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.HUT_Customer" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="500px" OnDelete="OnDeletFile">
                                         <Columns>
                                             <JQTools:JQGridColumn Alignment="left" Caption="檔案下載" Editor="text" FieldName="CustFile" Format="download,folder:Files/Hunter/Customer" MaxLength="150" ReadOnly="False" Width="260" EditorOptions="">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="center" Caption="建立時間" Editor="text" FieldName="UpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="95" Format="yyyy/mm/dd HH:MM">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="center" Caption="建立者" Editor="text" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                         </Columns>
                                         <RelationColumns>
                                             <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                         </RelationColumns>
                                         <TooItems>
                                             <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增檔案(1000k)" />
                                         </TooItems>
                                     </JQTools:JQDataGrid>
                                     <JQTools:JQDialog ID="JQDialogCustomerFile" runat="server" BindingObjectID="DFCustomerFile" DialogLeft="240px" DialogTop="120px" Title="客戶檔案維護" Width="550px">
                                         <JQTools:JQDataForm ID="DFCustomerFile" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_CustomerFile" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedCustomerFile" ParentObjectID="dataFormMaster" RemoteName="sCustomersJobs.HUT_Customer" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="OnApplyCustomerFile">
                                             <Columns>
                                                 <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" Visible="False" Width="80" Span="1" NewRow="False" ReadOnly="False" RowSpan="1" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" Visible="False" Width="80" NewRow="False" Span="1" ReadOnly="False" RowSpan="1" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="檔案上傳" Editor="infofileupload" FieldName="CustFile" MaxLength="0" Width="400" Span="1" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" EditorOptions="filter:'docx|doc|xlsx|xls|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|image|tif|txt',isAutoNum:true,upLoadFolder:'Files/Hunter/Customer',showButton:true,showLocalFile:true,fileSizeLimited:'1000'" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="UpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="UpdateDate" Editor="text" FieldName="UpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                             </Columns>
                                             <RelationColumns>
                                                 <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                             </RelationColumns>
                                         </JQTools:JQDataForm>
                                     </JQTools:JQDialog>

                                     <JQTools:JQDefault ID="JQDefault4" runat="server" BindingObjectID="DFCustomerFile" EnableTheming="True">
                                         <Columns>
                                             <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCODE" FieldName="UserID" RemoteMethod="True" />
                                             <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="UpdateBy" RemoteMethod="True" />
                                             <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="UpdateDate" RemoteMethod="True" />
                                         </Columns>
                                     </JQTools:JQDefault>

                                      <JQTools:JQAutoSeq ID="JQAutoSeq4" runat="server" BindingObjectID="DFCustomerFile" FieldName="AutoKey" NumDig="1" />
                                 
                           

                            <br />
                                 
                           

                        </JQTools:JQDialog>
                        <JQTools:JQDialog ID="Dialog_ContactRecord" runat="server" BindingObjectID="dataFormMaster2" Title="客戶聯繫紀錄" DialogLeft="40px" DialogTop="30px" Width="1100px" Wrap="False" EditMode="Dialog" ShowSubmitDiv="False" ShowModal="True">
                            <JQTools:JQDataForm ID="dataFormMaster2" runat="server" AlwaysReadOnly="False" ChainDataFormID="" Closed="False" ContinueAdd="False" DataMember="HUT_Customer" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="6" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sCustomersJobs.HUT_Customer" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶編號" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" MaxLength="128" RowSpan="1" Span="2" Width="200" ReadOnly="True" />
                                </Columns>
                            </JQTools:JQDataForm>
                            <JQTools:JQDataGrid ID="DGContactRecord" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_CustomerContactRecord" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogContactRecord" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnUpdate="" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster2" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.HUT_Customer" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="100%" OnDeleted="OnDeletedContactRecord">
                                <Columns>
                                    <JQTools:JQGridColumn Alignment="center" Caption="聯繫日期" Editor="datebox" FieldName="ContactDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="聯繫內容" Editor="text" FieldName="Notes" FormatScript="ShowAllGrid" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="750">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="修改時間" Editor="text" FieldName="UpdateDate" Format="yyyy/mm/dd HH:MM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="text" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                </RelationColumns>
                                <TooItems>
                                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增聯繫紀錄" />
                                </TooItems>
                            </JQTools:JQDataGrid>
                            <JQTools:JQDialog ID="JQDialogContactRecord" runat="server" BindingObjectID="DFContactRecord" DialogLeft="120px" DialogTop="110px" Title="聯繫紀錄維護" Width="750px">
                                <JQTools:JQDataForm ID="DFContactRecord" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_CustomerContactRecord" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedContactRecord" ParentObjectID="dataFormMaster2" RemoteName="sCustomersJobs.HUT_Customer" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="聯繫日期" Editor="datebox" FieldName="ContactDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="聯繫內容" Editor="textarea" EditorOptions="height:90" FieldName="Notes" MaxLength="256" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="620" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="UpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateDate" Editor="text" FieldName="UpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    </Columns>
                                    <RelationColumns>
                                        <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                    </RelationColumns>
                                </JQTools:JQDataForm>
                                <JQTools:JQDefault ID="JQDefault2" runat="server" BindingObjectID="DFContactRecord" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ContactDate" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCODE" FieldName="UserID" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="UpdateBy" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="UpdateDate" RemoteMethod="True" />
                                    </Columns>
                                </JQTools:JQDefault>
                                <JQTools:JQValidate ID="JQValidate2" runat="server" BindingObjectID="DFContactRecord" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactDate" RemoteMethod="True" ValidateMessage="請選擇聯繫日期！" ValidateType="None" />
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Notes" RemoteMethod="True" ValidateMessage="聯繫內容不可空白！" ValidateType="None" />
                                    </Columns>
                                </JQTools:JQValidate>
                                <JQTools:JQAutoSeq ID="JQAutoSeq2" runat="server" BindingObjectID="DFContactRecord" FieldName="AutoKey" NumDig="1" />
                            </JQTools:JQDialog>
                        </JQTools:JQDialog>
                    </td>

                </tr>
            </table>
        </div>
       
       

    </form>
</body>
</html>
