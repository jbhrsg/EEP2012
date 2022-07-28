<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JCS1_CustomerUpdate.aspx.cs" Inherits="Template_JQuerySingle1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="EFClientTools" namespace="EFClientTools" tagprefix="cc1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>        
         
         var Dialog_Import_ID = '#Dialog_Import';
         var Dialog_ImportMain_ID = '#Dialog_ImportMain';
         var DataForm_ImportMain_ID = '#DataForm_ImportMain';

         //========================================= ready ====================================================================================


         $(document).ready(function () {
             $("input, select, textarea").focus(function () {
                 $(this).css("background-color", "#FFFFB5");
             });

             $("input, select, textarea").blur(function () {
                 $(this).css("background-color", "white");
             });

             //-----------------------------------讀取ExcelJquery----------------------------------
             $('#Dialog_Import').jbExcelFileImport({
                 OnFileUploadSuccess: function () {
                     //開啟配對視窗
                     openForm('#Dialog_ImportMain', {}, 'inserted', 'Dialog');
                     $(this).jbExcelFileImport('changeSheetByName', 'Sheet1');
                 },
                 OnGetTitleSuccess: function (SheetArray, TitleArray) {

                     //SheetChangge
                     $('#DataForm_SheetImportMainSHEET').combobox('clear').combobox('loadData', SheetArray);

                     //載入選項以及預設
                     $('#DataForm_ImportMain').find('.info-combobox').each(function () {
                         $(this).combobox('loadData', TitleArray).combobox('clear');
                         $(this).combobox('selectExistsForText', $(this).closest('td').prev('td').html());
                     });
                 },
                 OnImportSuccess: function (jsonStr) {
                     var json = $.parseJSON(jsonStr);
                     if (!json.IsOK) {
                         var showMessage = json.ErrorMsg;
                         if (json.Result) {
                             showMessage += $('<a>', { href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: '錯誤資料.xls', FilePathName: json.Result }), target: '_blank' })
                                             .html('檔案下載')[0].outerHTML;
                         }
                         $.messager.alert('匯入修改失敗', showMessage, 'error');
                     }
                     else {
                         $.messager.alert(' ', "匯入修改成功");
                         $('#Dialog_Import').dialog('close');
                         $('#Dialog_ImportMain').dialog('close');
                         $('#dataGridView').datagrid('reload');
                         queryGrid('#dataGridMaster');
                     }
                 }
             });
             //-----------------------------------欄位配對視窗送出按鈕----------------------------
             $('#DialogSubmit', '#Dialog_ImportMain').removeAttr('onclick').on('click', function () {
                
                     var pre = confirm("確定匯入修改資料?");
                     if (pre == true) {

                         var voucherObject = $('#DataForm_VoucherMaster').jbDataFormGetAFormData();   //取資料
                         var titleObject = $('#DataForm_ImportMain').jbDataFormGetAFormData();   //取資料

                         $('#Dialog_Import').jbExcelFileImport('importFile', {
                             remoteName: 'sJCS1Import',
                             method: 'ExcelFileImport',
                             sheetIndex: $('#DataForm_SheetImportMainSHEET').combobox('getValue'),
                             titleObject: titleObject,
                             parameters: $.toJSONString(voucherObject)
                         });
                     }

             });
             //-------------------------------------------------------------------------------------


            
         });
                
         function OnLoadSuccessGV() {
             //panel寬度調整
             var dgid = $('#dataGridMaster');
             var queryPanel = getInfolightOption(dgid).queryDialog;
             if (queryPanel)
                 $(queryPanel).panel('resize', { width: 500 });
          

         }

         function queryGrid(dg) {//查詢後添加固定條件
             if ($(dg).attr('id') == 'dataGridMaster') {
                 //查詢條件
                 var result = [];                 
                 var CustomerID = $('#CustomerID_Query').combobox('getValue');//客戶代號
                 //if (CustomerID == "" || CustomerID == undefined) {
                 //    alert('請選擇客戶！');
                 //    return false;
                 //}
                 if (CustomerID != '') result.push("c.CustomerID like '%" + CustomerID + "%'");
                 var RoomerNameC = $('#RoomerNameC_Query').val();//中文姓名
                 if (RoomerNameC != '') result.push("RoomerNameC like '%" + RoomerNameC + "%'");
                 var CustNo = $('#CustNo_Query').val();//員工號碼                        
                 if (CustNo != '') result.push("CustNo like '%" + CustNo + "%'");                              
                 $(dg).datagrid('setWhere', result.join(' and '));
             }
         }

         //========================================匯入Excel========================================
         var openImportExcel = function () {
            
             $(Dialog_Import_ID).dialog('open');
         }
         //==========================================================================================

         //---------------------------------------匯入Excel Sheet切換------------------------------
         var DataForm_SheetImportMainSHEET_OnSelect = function (rowData) {
             $('#Dialog_Import').jbExcelFileImport('changeSheetByIndex', rowData.value);
         }


    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />


&nbsp;<div id="Dialog_dataGridMasterData">
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sJCS1.infoCust" runat="server" AutoApply="True"
                DataMember="infoCust" Pagination="True" QueryTitle="查詢條件"
                Title="" ReportFileName="~/JB_ADMIN/JBERP_R_SalesDetails.rdlc" QueryMode="Panel" DeleteCommandVisible="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="True" OnLoadSuccess="OnLoadSuccessGV" AllowAdd="True" AllowDelete="True" AllowUpdate="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="50px" QueryTop="50px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" Width="770px" >
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="員工號碼" Editor="text" FieldName="CustNo" Format="" Width="90" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="英文姓名" Editor="text" FieldName="RoomerNameC" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="英文姓名" Editor="text" FieldName="RoomerNameE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="190">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="性別" Editor="text" FieldName="GenderText" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="出生日期" Editor="datebox" FieldName="Birthdate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="國籍" Editor="text" FieldName="NationalityName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenter" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="班別" Editor="text" FieldName="CustClass" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                </Columns>      
                 <TooItems>
                    <JQTools:JQToolItem ID="Import" Icon="icon-importExcel" ItemType="easyui-linkbutton" OnClick="openImportExcel" Text="匯入檔案修改資料"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>           
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="選擇客戶" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustomerID',textField:'CustomerShortName',remoteName:'sJCS1.infoCustomers',tableName:'infoCustomers',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustomerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="中文姓名" Condition="%" DataType="string" Editor="text" FieldName="RoomerNameC" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工號碼" Condition="%%" DataType="string" Editor="text" FieldName="CustNo" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="80" EditorOptions="" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
        <div id="Dialog_Import"></div>

        <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog" DialogLeft="" DialogTop="" Width="750px">
            <JQTools:JQDataForm ID="DataForm_VoucherMaster" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="Roomer" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="3" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sJCS1Import.Roomer" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="客戶" Editor="infocombobox" EditorOptions="valueField:'CustomerID',textField:'CustomerShortName',remoteName:'sJCS1.infoCustomers',tableName:'infoCustomers',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustomerID" Format="" MaxLength="50" Width="180" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQValidate ID="Validate_VoucherMaster" runat="server" BindingObjectID="DataForm_VoucherMaster" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CustomerID" RemoteMethod="True" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>
            <JQTools:JQDefault ID="Default_Schedule" runat="server" BindingObjectID="DataForm_VoucherMaster">
            </JQTools:JQDefault>
            <JQTools:JQDataForm ID="DataForm_SheetImportMain" runat="server" DataMember=" " HorizontalColumnsCount="3" RemoteName=" " Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="工作表" Editor="infocombobox" FieldName="SHEET" Width="120" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,onSelect:DataForm_SheetImportMainSHEET_OnSelect,panelHeight:200" />

                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="Roomer" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="3" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sJCS1Import.Roomer" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="員工號碼" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="CustNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="CostCenter" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="班別" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="CustClass" Width="100" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
            </JQTools:JQValidate>
        </JQTools:JQDialog>


</form>
</body>
</html>
