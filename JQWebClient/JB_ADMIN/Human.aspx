<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Human.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        var sLabel = "";
        var industry = "";

        var Dialog_Import_ID = '#Dialog_Import';
        var Dialog_ImportMain_ID = '#Dialog_ImportMain';
        var DataForm_ImportMain_ID = '#DataForm_ImportMain';

        //========================================= ready ====================================================================================

        $(document).ready(function () {
            //---------帶入標籤查詢條件 , 匯入紀錄-----------
            queryGrid($('#dataGridQuery'));
            var fullText = $('#dataFormQueryfullText').closest('td');
            //var bnQ = $('#bnQuery').closest('td').children();
            fullText.append($('#bnQuery')).append('&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;').append($('#bnHumanImportInfo'));
                      
            //$('#dataFormQueryfullText').attr('title', '請輸入關鍵字:以 , 區隔');
            //var fullText = $('#dataFormQueryfullText').closest('td');
            //fullText.append('  ( ★請輸入關鍵字:以 , 區隔 )');           

            var UserID = getClientInfo("UserName");          
            sLabel = "";
            industry = "";
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
                        $.messager.alert('匯入失敗', showMessage, 'error');
                    }
                    else {
                        $.messager.alert(' ', "匯入成功");
                        $('#Dialog_Import').dialog('close');
                        $('#Dialog_ImportMain').dialog('close');
                        $('#dataGridView').datagrid('reload');
                    }
                }
            });

            //-----------------------------------欄位配對視窗送出按鈕----------------------------
            $('#DialogSubmit', '#Dialog_ImportMain').removeAttr('onclick').on('click', function () {
                if (!$('#DataForm_ImportMain').form('validateForm')) return;            //驗證
                //標籤群
                var sLabel = $("#DataForm_HumanMastersLabel").val();
                //產業別
                var industry = $("#DataForm_HumanMasterNameC").combobox('getValue');

                if (sLabel != "") {
                    var pre = confirm("確定匯入?");
                    if (pre == true) {

                        var voucherObject = $('#DataForm_HumanMaster').jbDataFormGetAFormData();   //取資料
                        var titleObject = $('#DataForm_ImportMain').jbDataFormGetAFormData();   //取資料

                        $('#Dialog_Import').jbExcelFileImport('importFile', {
                            remoteName: 'sHumanImport',
                            method: 'ExcelFileImport',
                            sheetIndex: $('#DataForm_SheetImportMainSHEET').combobox('getValue'),
                            titleObject: titleObject,
                            parameters: $.toJSONString(voucherObject)
                        });
                    }

                }

            });



        });

        //========================================匯入Excel=================================================================================================================
        var openImportExcel = function () {
            $(Dialog_Import_ID).dialog('open');
        }

        //---------------------------------------匯入Excel Sheet切換------------------------------
        var DataForm_SheetImportMainSHEET_OnSelect = function (rowData) {
            $('#Dialog_Import').jbExcelFileImport('changeSheetByIndex', rowData.value);
        }

        //一載入時=>標籤過濾已選擇部分
        function OnLoadSuccessDetail() {
            var HumanID = $("#dataFormMasterHumanID").val();
            GetrHumanClass(HumanID);
            //$("#dataFormDetailHumanClassID").combobox('setWhere', " AutoKey not in ( select HumanClassID from HumanClass where HumanID='" + HumanID + "')");

        }

        //========================================新增人才=================================================================================================================
        //得到可選擇標籤資料
        //---------------------------------------呼叫Method---------------------------------------
        var GetDataFromMethod = function (methodName, data) {
            var returnValue = null;
            $.ajax({
                url: '../handler/JqDataHandle.ashx?RemoteName=sHumanImport',
                data: { mode: 'method', method: methodName, parameters: $.toJSONString(data) },
                type: 'POST',
                async: false,
                success: function (data) { returnValue = $.parseJSON(data); },
                error: function (xhr, ajaxOptions, thrownError) { returnValue = null; }
            });
            return returnValue;
        };
        var GetrHumanClass = function (HumanID) {
            var HumanID = $("#dataFormMasterHumanID").val();
            var CodeList = GetDataFromMethod('GetrHumanClass', { Human_ID: HumanID});
            if (CodeList != null) $("#dataFormDetailHumanClassID").combobox('loadData', CodeList);
        }
        //人才DataForm修改完成後
        function DFOnApplied() {
            queryG();
        }

        //========================================人才搜尋功能=================================================================================================================
        function queryG() {//查詢後添加固定條件   
          
                //查詢條件
                var dg = $('#dataGridView');
                var result = [];
                var BirthYear1 = $('#dataFormQueryBirthYear').val();//年齡1
                var BirthYear2 = $('#dataFormQueryBirthYear2').val();//年齡2
                //	全文搜尋
                var fullText = $('#dataFormQueryfullText').val();
                var Type = 1;//Type int---1=>查詢 ,2=>匯出                  

                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sHumanImport.infoHuman',  //連接的Server端，command
                    data: "mode=method&method=" + "HumanSelect" + "&parameters=" + BirthYear1 + "*" + BirthYear2 + "*" + fullText + "*" + Type,  //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入搜尋字串
                    cache: false,
                    async: true,
                    success: function (data) {
                        var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示
                        if (rows.length > 10) {
                            //通過loadData方法清除掉原有Grid中的舊有資料並填補分頁資料
                            $(dg).datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                        } else {
                            $(dg).datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                        }
                        //if (rows.length == 0) {
                        //    alert("目前無符合人才！");
                        //} else {
                        //    alert("搜尋完成！");
                        //}
                    }
                });
                //回到第一頁
            //dg.datagrid({ pageNumber: 1 });
            //獲得目前匯入Execel的數量
                //var cnt = "";
                //$.ajax({
                //    type: "POST",
                //    url: '../handler/jqDataHandle.ashx?RemoteName=sHumanImport.infoHumanClassSetNew', //連接的Server端，command
                //    data: "mode=method&method=" + "GetHumanImportInfo"+ "&parameters=" + 1 ,
                //    cache: false,
                //    async: false,
                //    success: function (data) {
                //        if (data != false) {
                //            cnt = data;
                //        }
                //    },
                //    error: function (xhr, ajaxOptions, thrownError) {
                //        alert(xhr.status);
                //        alert(thrownError);
                //    }
                //});
                //$(dg).datagrid('getColumnOption', columnName).title = "人才清單 (Execel匯入總筆數：" + cnt+"";


                            
        }
        //reload
        function OnUpdatedGrid() {
            $("#dataGridView").datagrid("reload");
        }
        
        //---------------------------------------人才Grid顯示資料不完整=> 年齡,電話---------------------------------------
        function FormatScriptbCheck(val, rowData) {
            if (rowData.bCheck == 1) {
                return "<div title='資料有誤' style='font-weight:bold;color:red;'> " + val + "</div>";
            } else {
                return "<div style='color:black;'> " + val + "</div>";
            }
        }

        //---------------------------------------查詢結果匯出Excel---------------------------------------
        function AutoExcel() {
            //查詢條件
            var result = [];
            var BirthYear1 = $('#dataFormQueryBirthYear').val();//年齡1
            var BirthYear2 = $('#dataFormQueryBirthYear2').val();//年齡2
            //	全文搜尋
            var fullText = $('#dataFormQueryfullText').val();
            var Type = 2;//Type int---1=>查詢 ,2=>匯出                 

            $.ajax({
                url: '../handler/JBHRISUseCaseHandler.ashx?' + $.param({ mode: 'CallServerMethodReturnFile', remoteName: 'sHumanImport', method: 'HumanSelectExcel' }),
                //data: { parameters: $.toJSONString(data) },

                data: "&parameters=" + BirthYear1 + "*" + BirthYear2 + "*" + fullText + "*" + Type,

                type: 'POST',
                async: true,
                success: function (data) {
                    //Json.FileName
                    var Json = $.parseJSON(data);
                    if (Json.IsOK) {
                        var Url = $('<a>', {
                            href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: '人才資訊.xls', FilePathName: Json.FileStreamOrFileName }),
                            target: '_blank'

                        }).html('檔案下載')[0].outerHTML;

                        $.messager.alert('下載', Url, '');

                    }

                    else $.messager.alert('錯誤', Json.Msg, 'error');

                },

                beforeSend: function () { $.messager.progress({ msg: '執行中...' }); },

                complete: function () { $.messager.progress('close'); },

                error: function (xhr, ajaxOptions, thrownError) { alert('error'); }

            });
        }
        //---------------------------------------將人才群 新增標籤---------------------------------------
        function AddLabel() {
            if ($("#dataGridView").datagrid('getChecked').length == 0) {
                alert('請勾選人才。');
            } else {
                openForm('#JQDialog3', $('#dataGridView').datagrid('getSelected'), "update", 'dialog');
            }
        }
        function DFLabelOnApply() {
            
                var rows = $('#dataGridView').datagrid("getChecked");
                var HumanIDStr = [];//人才ID群
                for (var i = 0; i < rows.length; i++) {                   
                    HumanIDStr.push(rows[i].HumanID);
                }
                var sHumanID = HumanIDStr.join('*');
                var sClassID = $('#JQDFLabelHumanClassID').combobox('getValue');
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sHumanImport.infoHuman',
                    data: "mode=method&method=" + "AddHumanLabel" + " &parameters=" + sClassID + "," + sHumanID,
                    cache: false,
                    async: false,
                    success: function (data) {
                        queryG();
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
                        
        }                
        //--------------------查詢條件的標籤Grid--------------------------
        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridQuery') {
                //查詢條件
                var result = [];
                var UserID = getClientInfo("UserID");                          
                if (UserID != '') result.push("UserID = '" + UserID + "'");                
                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }
        //新增標籤項目後
        function GQOnInserted() {
            $('#dataGridQuery').datagrid('reload');
        }
        //清空登入者的所有標籤
        function ClearQuery() {
            var UserID = getClientInfo("UserID");
            var ConfirmYN = confirm("確定要刪除所有標籤?");
            if (ConfirmYN == false) {
                return true;
            }
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sHumanImport.infoHuman',
                data: "mode=method&method=" + "ClearQueryLabel" + " &parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    queryGrid($('#dataGridQuery'));
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }

            });
        }

        //人才刪除
        //=============================================取得登入者工號(是否有刪除權限)=========================================================================================
        function GetLoginID() {
            var bAdmin = false;          
            var sUserID = getClientInfo("UserID");
            if (sUserID == "060" || sUserID == "430") {//莊子平
                var bAdmin = true;
            }
            return bAdmin;
        }
        function HumanDetele() {
            if (GetLoginID()) { //有權限才可以刪除
                if ($("#dataGridView").datagrid('getChecked').length == 0) {
                    alert('請勾選需刪除人才。');
                } else {
                    var pre = confirm("確定刪除?");
                    if (pre == true) {
                        var rows = $('#dataGridView').datagrid('getChecked');
                        var aHumanID = [];
                        var sHumanID = "";
                        for (var i = 0; i < rows.length; i++) {
                            aHumanID.push(rows[i].HumanID);
                        }
                        var sHumanID = aHumanID.join('*');

                        $.ajax({
                            type: "POST",
                            url: '../handler/jqDataHandle.ashx?RemoteName=sHumanImport.infoHuman',
                            data: "mode=method&method=" + "DeleteHumanID" + "&parameters=" + sHumanID,
                            cache: false,
                            async: false,
                            success: function (data) {
                                alert('人才刪除成功！');
                                queryG();
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                alert(xhr.status);
                                alert(thrownError);
                            }
                        });

                    }

                }
            } else {
                alert('無刪除權限！');
            }
        
        }
        //呼叫匯入紀錄
        function HumanImportInfo() {
            openForm('#JQDialogImportInfo', {}, 'viewed', 'dialog');
        }
        


    </script>



</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="easyui-accordion">
            <div title="查詢條件">

            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
                <JQTools:JQDataForm ID="dataFormQuery" runat="server" DataMember="infoHuman" HorizontalColumnsCount="3" RemoteName="sHumanImport.infoHuman" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" ParentObjectID=""  >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="年齡" Editor="numberbox" FieldName="BirthYear" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="43" />
                        <JQTools:JQFormColumn Alignment="left" Caption="～" Editor="numberbox" FieldName="BirthYear2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="43" />
                        <JQTools:JQFormColumn Alignment="left" Caption="全文搜尋" Editor="text" FieldName="fullText" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="200" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="AutoKey" ParentFieldName="AutoKey" />
                    </RelationColumns>
                </JQTools:JQDataForm>
            <JQTools:JQDataGrid ID="dataGridQuery" data-options="pagination:true,view:commandview" RemoteName="sHumanImport.infoHumanClassQuery" runat="server" AutoApply="True"
                DataMember="infoHumanClassQuery" Pagination="False" QueryTitle="" EditDialogID=""
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="20,40,60,80,100" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Height="150px" Width="330px" OnInserted="GQOnInserted">
                <Columns>
<%--                    <JQTools:JQGridColumn Alignment="center" Caption="出生年" Editor="numberbox" FieldName="BirthYear" MaxLength="0" Visible="true" Width="60" Sortable="False" />--%>
                    <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="標籤" Editor="infocombobox" FieldName="HumanClassID" Format="" MaxLength="0" Visible="true" Width="220" Sortable="False" EditorOptions="valueField:'AutoKey',textField:'ClassText',remoteName:'sHumanImport.infoHumanClassSet',tableName:'infoHumanClassSet',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="center" Caption="維護人員" Editor="text" FieldName="UserID" Format="" MaxLength="0" Visible="False" Width="78" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="維護日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM" MaxLength="0" Visible="False" Width="105" Sortable="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增查詢標簽" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" Enabled="True" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" Enabled="True" Visible="True"  />
                    <JQTools:JQToolItem Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="ClearQuery" Text="清空" />
                </TooItems>
            </JQTools:JQDataGrid>
                <JQTools:JQDefault ID="defaultQGrid" runat="server" BindingObjectID="dataGridQuery" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="UserID" RemoteMethod="True" DefaultValue="_usercode"/>
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="CreateDate" RemoteMethod="True" DefaultValue="_today"/>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateGQuery" runat="server" BindingObjectID="dataGridQuery" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="HumanClassID" RemoteMethod="True" ValidateMessage="請選擇標籤" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            <a class="easyui-linkbutton" href="#" onclick="queryG()" id="bnQuery">查詢</a>            
            <a class="easyui-linkbutton" href="#" onclick="HumanImportInfo()" id="bnHumanImportInfo">匯入紀錄</a>

            </div>
            </div>
            
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sHumanImport.infoHuman" runat="server" AutoApply="True"
                DataMember="infoHuman" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="人才清單" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="20,40,60,80,100" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnUpdated="OnUpdatedGrid" Height="90%">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="center" Caption="姓名" Editor="text" FieldName="NameC" Format="" MaxLength="0" Visible="true" Width="75" Sortable="False" FormatScript="FormatScriptbCheck" />
                    <JQTools:JQGridColumn Alignment="center" Caption="性別" Editor="text" FieldName="SexText" Format="" MaxLength="0" Visible="true" Width="40" Sortable="False" />
<%--                    <JQTools:JQGridColumn Alignment="center" Caption="出生年" Editor="numberbox" FieldName="BirthYear" MaxLength="0" Visible="true" Width="60" Sortable="False" />--%>
                    <JQTools:JQGridColumn Alignment="center" Caption="年齡" Editor="text" FieldName="iAge" Format="" MaxLength="0" Visible="true" Width="45" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="PhoneNo" Format="" MaxLength="0" Visible="true" Width="80" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="居住區域" Editor="text" FieldName="Address" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="標籤" Editor="text" FieldName="HumanClassName" Format="" MaxLength="0" Visible="true" Width="400" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Note" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="center" Caption="維護人員" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Visible="true" Width="65" />
                    <JQTools:JQGridColumn Alignment="center" Caption="維護日期" Editor="datebox" FieldName="LastUpdateDate" Format="yyyy/mm/dd HH:MM" Visible="true" Width="100" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增人才" />
                    <JQTools:JQToolItem ID="Import" Icon="icon-importExcel" ItemType="easyui-linkbutton" OnClick="openImportExcel" Text="匯入人才"  />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="AutoExcel" Text="匯出人才" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton"   OnClick="AddLabel" Text="新增標簽" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="HumanDetele" Text="刪除人才" Visible="True" />
                </TooItems>
            </JQTools:JQDataGrid>
          
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="人才資料維護" DialogTop="50px" DialogLeft="200px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="infoHuman" HorizontalColumnsCount="3" RemoteName="sHumanImport.infoHuman" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" ParentObjectID="dataGridView" OnApplied="DFOnApplied"  >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="text" FieldName="NameC" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="70" />
                        <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infocombobox" FieldName="SexText" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" EditorOptions="items:[{value:'女',text:'女',selected:'false'},{value:'男',text:'男',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出生年" Editor="numberbox" FieldName="BirthYear" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="65" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="PhoneNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="居住區域" Editor="text" FieldName="Address" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="Note" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="480" EditorOptions="height:60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="HumanID" Editor="text" FieldName="HumanID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="AutoKey" ParentFieldName="AutoKey" />
                    </RelationColumns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="CreateBy" RemoteMethod="True" DefaultValue="_username"/>
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="CreateDate" RemoteMethod="True" DefaultValue="_today"/>
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="LastUpdateBy" RemoteMethod="True" DefaultValue="_username"/>
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="LastUpdateDate" RemoteMethod="True" DefaultValue="_today"/>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="HumanID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="NameC" RemoteMethod="True" ValidateMessage="姓名必填" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PhoneNo" RemoteMethod="True" ValidateMessage="電話必填" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SexText" RemoteMethod="True" ValidateMessage="性別必填" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="BirthYear" RemoteMethod="True" ValidateMessage="年齡必填" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Address" RemoteMethod="True" ValidateMessage="居住區域必填" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="infoHumanClass" DeleteCommandVisible="True" DuplicateCheck="True" EditDialogID="" EditMode="Dialog" EditOnEnter="True" Height="230px" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sHumanImport.infoHuman" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="HumanID" Editor="text" FieldName="HumanID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="標籤" Editor="infocombobox" FieldName="HumanClassID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="200" EditorOptions="valueField:'AutoKey',textField:'ClassText',remoteName:'sHumanImport.infoHumanClassSet',tableName:'infoHumanClassSet',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="197">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="維護人員" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="維護日期" Editor="datebox" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="HumanID" ParentFieldName="HumanID" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增標籤" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <JQTools:JQAutoSeq ID="JQAutoSeq2" runat="server" BindingObjectID="dataGridDetail" FieldName="AutoKey" NumDig="3" />
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Closed="True" EditMode="Dialog" Title="標籤維護" Width="420px" DialogLeft="130px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="True" DataMember="infoHumanClass" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="5" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sHumanImport.infoHuman" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnLoadSuccess="OnLoadSuccessDetail">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="序號" Editor="text" FieldName="HumanID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="40" />
                            <JQTools:JQFormColumn Alignment="left" Caption="請選擇" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="HumanClassID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="230" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="HumanID" ParentFieldName="HumanID" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataGridDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="AutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataGridDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="HumanClassID" RemoteMethod="True" ValidateMessage="請選擇標籤" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>

                  <JQTools:JQDialog ID="JQDialog3"  BindingObjectID="JQDFLabel" runat="server" DialogLeft="160px" DialogTop="110px" Title="新增標簽" Width="400px" Closed="False" ShowSubmitDiv="True">
                  <JQTools:JQDataForm ID="JQDFLabel" runat="server" DataMember="infoHumanClass" HorizontalColumnsCount="2" RemoteName="sHumanImport.infoHumanClass" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnApply="DFLabelOnApply" ParentObjectID="dataGridView">
                        <Columns>
                             <JQTools:JQFormColumn Alignment="left" Caption=" 標籤" Editor="infocombobox" FieldName="HumanClassID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="250" EditorOptions="valueField:'AutoKey',textField:'ClassText',remoteName:'sHumanImport.infoHumanClassSet',tableName:'infoHumanClassSet',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        </Columns>
                    </JQTools:JQDataForm>
                      <JQTools:JQValidate ID="Validate_Label" runat="server" BindingObjectID="JQDFLabel" EnableTheming="True">
                          <Columns>
                              <JQTools:JQValidateColumn CheckNull="True" FieldName="HumanClassID" RemoteMethod="True" ValidateMessage="請選擇標籤" ValidateType="None" />
                          </Columns>
                      </JQTools:JQValidate>
            </JQTools:JQDialog>

        <div id="Dialog_Import"></div>

        <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog" DialogLeft="" DialogTop="" Width="750px">
            <JQTools:JQDataForm ID="DataForm_HumanMaster" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HumanImport" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="3" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sHumanImport.HumanImport" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="標籤" Editor="text" EditorOptions="" FieldName="sLabel" Format="" MaxLength="1000" Width="430" NewRow="False" />
                    <JQTools:JQFormColumn Alignment="left" Caption="產業別" Editor="infocombobox" EditorOptions="items:[{value:'製造業',text:'製造業',selected:'false'},{value:'服務業',text:'服務業',selected:'false'},{value:'物流業',text:'物流業',selected:'false'},{value:'保全業',text:'保全業',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="NameC" MaxLength="0" NewRow="False" Width="100" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQValidate ID="Validate_HumanMaster" runat="server" BindingObjectID="DataForm_HumanMaster" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="sLabel" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="NameC" RemoteMethod="True" ValidateMessage="請選擇產業別" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>
            <JQTools:JQDataForm ID="DataForm_SheetImportMain" runat="server" DataMember=" " HorizontalColumnsCount="3" RemoteName=" " Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="工作表" Editor="infocombobox" FieldName="SHEET" Width="120" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,onSelect:DataForm_SheetImportMainSHEET_OnSelect,panelHeight:200" />

                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HumanImport" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sHumanImport.HumanImport" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="NameC" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="SexText" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="PhoneNo" Width="100" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="出生年" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="BirthYear" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="居住區域" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Address" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
            </JQTools:JQValidate>
        </JQTools:JQDialog>


            <JQTools:JQDialog ID="JQDialogImportInfo" runat="server" DialogLeft="200px" DialogTop="100px" Title="匯入紀錄查詢" ShowSubmitDiv="False" Width="400px">
                <JQTools:JQDataGrid ID="JQGridImportInfo" runat="server" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="infoImportInfo" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sHumanImport.infoImportInfo" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="320px" BufferView="False" NotInitGrid="False" RowNumbers="True" Height="300px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="匯入日期" Editor="datebox" FieldName="cDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="95" Format="yyyy/mm/dd HH:MM" />
                        <JQTools:JQGridColumn Alignment="center" Caption="匯入人員" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90" />
                        <JQTools:JQGridColumn Alignment="right" Caption="匯入筆數" Editor="numberbox" FieldName="iCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Total="sum" Visible="True" Width="77" />
                    </Columns>
                    
                </JQTools:JQDataGrid>
        </JQTools:JQDialog>


        </div>
    </form>
</body>
</html>
