<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_CustomerImport.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>    
</head>
<body>
    <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
    <script type="text/javascript">
        var Dialog_Import_ID = '#Dialog_Import';//匯入分析視窗
        var Dialog_ImportMain_ID = '#Dialog_ImportMain';//匯入配對視窗
        var DataForm_ImportMain_ID = '#DataForm_ImportMain';
        var DataForm_SheetImportMainSHEET_ID = '#DataForm_SheetImportMainSHEET';
        //=======================================【Ready】=========================================
        //html完全載入時開啟"Excel欄位分析視窗"，呼叫(jquery.jbjob.js)jbExcelFileImport 其初始化路徑是/InnerPages/ImportExcel.aspx，選擇xls檔按"欄位分析"，呼叫JbExcelHandler.ashx的ExcelFileSave()上傳xls，
        //xls檔上傳成功後，開啟"欄位配對視窗"，
        //按下'儲存"，呼叫(jquery.jbjob.js)jbExcelFileImport('importFile')，呼叫(handler)ExcelFileSheetImport()，呼叫(dll)ExcelFileImport()，呼叫ExcelFileImport類別匯入並傳回結果
        $(function () {
            $(Dialog_Import_ID).jbExcelFileImport({
                OnFileUploadSuccess: function () {//上傳檔案成功
                    openForm(Dialog_ImportMain_ID, {}, 'inserted', 'Dialog');
                    $(this).jbExcelFileImport('changeSheetByName', 'Sheet1');
                },
                OnGetTitleSuccess: function (SheetArray, TitleArray) {//取得標題成功
                    $(DataForm_SheetImportMainSHEET_ID).combobox('clear').combobox('loadData', SheetArray);
                    $(DataForm_ImportMain_ID).find('.info-combobox').each(function () {
                        $(this).combobox('loadData', TitleArray).combobox('clear');
                        $(this).combobox('selectExistsForText', $(this).closest('td').prev('td').html());
                    });
                },
                OnImportSuccess: function (jsonStr) {//匯入成功
                    var json = $.parseJSON(jsonStr);
                    if (!json.IsOK) {
                        var showMessage = json.ErrorMsg;
                        if (json.Result) {
                            showMessage += $('<a>', { href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: '錯誤資料.xls', FilePathName: json.Result }), target: '_blank' })
                                            .html('檔案下載')[0].outerHTML;
                        }
                        $.messager.alert('匯入失敗', showMessage, 'error');
                    }else {
                        closeForm(Dialog_ImportMain_ID);
                        $.messager.alert('匯入完成', json.Result);
                        //$(DataGrid_Basetts_ID).datagrid('reload');
                    }
                }
            });

            $('#DialogSubmit', Dialog_ImportMain_ID).removeAttr('onclick').on('click', function () {//欄位配對視窗按下"儲存"按鈕------
                if (!$(DataForm_ImportMain_ID).form('validateForm')) return;            //驗證
                var titleObject = $(DataForm_ImportMain_ID).jbDataFormGetAFormData();   //取資料
                $(Dialog_Import_ID).jbExcelFileImport('importFile', {////匯入作業
                    remoteName: 'sERPCustomerImport',
                    method: 'ExcelFileImport',
                    sheetIndex: $(DataForm_SheetImportMainSHEET_ID).combobox('getValue'),
                    titleObject: titleObject,
                    parameters: ''
                });
            });
            
            openImportExcel();//欄位分析視窗

            $("#DataForm_SheetImportMainSHEET").closest('td').append(" ※必填欄位：客戶代號、公司名稱、電話1");
        });
        //=========================================================================================        
        //---------------------------------------匯入Excel----------------------------------------
        var openImportExcel = function () {
            $(Dialog_Import_ID).dialog('open');
        };       
        //---------------------------------------匯入Excel Sheet切換------------------------------
        var DataForm_SheetImportMainSHEET_OnSelect = function (rowData) {
            $(Dialog_Import_ID).jbExcelFileImport('changeSheetByIndex', rowData.value);
        }
        //-----------------------------------------------------------------------------------------
    </script>
    <form id="form1" runat="server">
        <JQTools:JQMultiLanguage ID="JQMultiLanguage1" runat="server" />

        <!-- 匯入對話框內容的 DIV -->
        <div id="Dialog_Import"></div>

        <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog" DialogLeft="" DialogTop="">
            <JQTools:JQDataForm ID="DataForm_SheetImportMain" runat="server" DataMember=" " HorizontalColumnsCount="3" RemoteName=" " Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="工作表" Editor="infocombobox" FieldName="SHEET" Width="140" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,onSelect:DataForm_SheetImportMainSHEET_OnSelect,panelHeight:200" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" DataMember=" " HorizontalColumnsCount="3" RemoteName=" " Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0">
                <Columns>
                    <%--<JQTools:JQFormColumn Alignment="left" Caption="項次" Editor="infocombobox" FieldName="iAutoKey" Width="120" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />--%>
                    <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustNO" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="公司名稱" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustName" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="公司簡稱" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustShortName" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="電話1" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustTelNO" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="電話2" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustTelNO1" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="傳真" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustFaxNO" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人A" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactA" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人A行動" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactATel" Width="100" />
                    <%--<JQTools:JQFormColumn Alignment="left" Caption="職稱A" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactAJobID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />--%>
                    <JQTools:JQFormColumn Alignment="left" Caption="分機A" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactASubTel" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人A Email" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactAMail" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人B" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactB" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人B行動" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactBTel" Width="100" />
                    <%--<JQTools:JQFormColumn Alignment="left" Caption="職稱B" Editor="infocombobox" FieldName="ContactBJobID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />--%>
                    <JQTools:JQFormColumn Alignment="left" Caption="分機B" Editor="infocombobox" FieldName="ContactBSubTel" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人B Email" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactBMail" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustNotes" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="地址" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustAddr" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="業務代號" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="人力銀行連結網址" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HrBankURL" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="開發說明" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="DevelopDescr" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="客戶級別" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PostType" Width="100" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="False" FieldName="iAutoKey" />
                </Columns>
            </JQTools:JQValidate>
        </JQTools:JQDialog>        

    </form>
</body>
</html>
