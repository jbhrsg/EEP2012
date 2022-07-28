<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesPaper.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(function () {
            //增加"選擇檔案"按鈕
            $('#dataFormMasterSalesType1_A').after($('<input>', { type: 'file', id: 'SalesType1_A_html', name: 'SalesType1_A_html' })).after("  ");
            $('#dataFormMasterSalesType1_B').after($('<input>', { type: 'file', id: 'SalesType1_B_html', name: 'SalesType1_B_html' })).after("  ");
            $('#dataFormMasterSalesType1_C').after($('<input>', { type: 'file', id: 'SalesType1_C_html', name: 'SalesType1_C_html' })).after("  ");
            $('#dataFormMasterSalesType31_A').after($('<input>', { type: 'file', id: 'SalesType31_A_html', name: 'SalesType31_A_html' })).after("  ");
            $('#dataFormMasterSalesType31_B').after($('<input>', { type: 'file', id: 'SalesType31_B_html', name: 'SalesType31_B_html' })).after("  ");
        });

        //檢查已"選擇檔案"卻未"上傳"而直接"存檔"
        function OnApplydataFormMaster() {
            if (($('#SalesType1_A_html').val() && $('#dataFormMasterSalesType1_A').val() == "") ||
                ($('#SalesType1_B_html').val() && $('#dataFormMasterSalesType1_B').val() == "") ||
                ($('#SalesType1_C_html').val() && $('#dataFormMasterSalesType1_C').val() == "") ||
                ($('#SalesType31_A_html').val() && $('#dataFormMasterSalesType31_A').val() == "") ||
                ($('#SalesType31_B_html').val() && $('#dataFormMasterSalesType31_B').val() == "")) {
                alert('您已選擇檔案，請先「上傳」，再「存檔」');
                return false;
            }
        }
        //檢查已"上傳"檔案卻未"存檔"(第一次新增時)
        function OnCanceldataFormMaster() {
            if (getEditMode($('#dataFormMaster')) == 'inserted' && ($('#dataFormMasterSalesType1_A').val() != "" || $('#dataFormMasterSalesType1_B').val() != "" ||
                $('#dataFormMasterSalesType1_C').val() != "" || $('#dataFormMasterSalesType31_A').val() != "" || $('#dataFormMasterSalesType31_B').val() != "")) {
                alert('您已「上傳」檔案，請「存檔」');
                return false;
            }
        }
        function OnLoadSuccessdataFormMaster() {//新增後，就不得修改"夾報日期"(為了檔名與日期一致)
            if (getEditMode($('#dataFormMaster')) == 'updated' && ($('#dataFormMasterSalesType1_A').val() != "" || $('#dataFormMasterSalesType1_B').val() != "" ||
                $('#dataFormMasterSalesType1_C').val() != "" || $('#dataFormMasterSalesType31_A').val() != "" || $('#dataFormMasterSalesType31_B').val() != "")) {
                $('#dataFormMasterPaperDate').combo('textbox').attr({ 'disabled': 'disabled' });//$('#dataFormMasterPaperDate').datebox({ 'disabled': true });
            } else {
                $('#dataFormMasterPaperDate').combo('textbox').removeAttr('disabled');//$('#dataFormMasterPaperDate').datebox({ 'disabled': false });
            }
        }
        //onclick"上傳"
        function onclickButton1() {
            if ($('#SalesType1_A_html').val()) UploadFile('SalesType1_A');//有值就上傳檔案
            if ($('#SalesType1_B_html').val()) UploadFile('SalesType1_B');
            if ($('#SalesType1_C_html').val()) UploadFile('SalesType1_C');
            if ($('#SalesType31_A_html').val()) UploadFile('SalesType31_A');
            if ($('#SalesType31_B_html').val()) UploadFile('SalesType31_B');
        }
        function UploadFile(SalesType) {
            $.ajaxFileUpload({
                url: '../handler/JbUploadPaperHandler.ashx?mode=SaveFileAndName', //需要链接到服务器地址   
                type: 'post',
                secureuri: false,
                data: {
                    dir: SalesType,
                    paperdate: $('#dataFormMasterPaperDate').datebox('getValue')
                },
                fileElementId: SalesType + '_html', //文件选择框的id属性   
                dataType: 'json', //服务器返回的格式，可以是json
                success: function (result) {
                    if (result.IsOK) {//有上傳檔案，才把該檔名塞回對應的textbox
                        $('#dataFormMaster' + SalesType).val(result.Result);
                    }
                    else alert(result.ErrorMsg);
                },
                error: function (result) {
                    alert(result.ErrorMsg);
                }
            });
        }
        function OnDeletedataGridView() {
            $.ajax({
                type: "POST",
                url: '../handler/JbUploadPaperHandler.ashx?mode=DeleteFile',
                secureuri: false,
                data: {
                    paperdate: $("#dataGridView").datagrid('getSelected').PaperDate
                },
                dataType: 'json', //服务器返回的格式，可以是json
                success: function (result) {
                    if (result.IsOK) {
                        alert("檔案與資料刪除成功");
                    } else { alert("資料刪除成功"); }
                },
                error: function (result) {
                    alert(result.ErrorMsg);
                }
            });
        }
        function GetDefaultStartDate (){
            var dt = new Date();
            var aDate = new Date($.jbDateAdd('days', 0, dt));//開始日期今天
            var StartaDate = $.jbGetFirstDate(aDate);
            return $.jbjob.Date.DateFormat(StartaDate, 'yyyy/MM/dd');
        }
        function GetDefaultLastDate() {
            var dt = new Date();
            var aDate = new Date($.jbDateAdd('days', 0, dt));//開始日期今天
            var LastaDate = $.jbGetLastDate(aDate);
            return $.jbjob.Date.DateFormat(LastaDate, 'yyyy/MM/dd')
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPSalesPaper.ERPSalesPaper" runat="server" AutoApply="True"
                DataMember="ERPSalesPaper" Pagination="True" QueryTitle="查詢夾報" EditDialogID="JQDialog1"
                Title="上傳夾報" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="200px" QueryMode="Window" QueryTop="5px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDelete="OnDeletedataGridView">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="夾報日期" Editor="datebox" FieldName="PaperDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="求才A面" Editor="text" FieldName="SalesType1_A" Format="download,folder:Files/JBERP_SalesPaper/SalesType1_A,Height:10" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="求才B面" Editor="text" FieldName="SalesType1_B" Format="download,folder:Files/JBERP_SalesPaper/SalesType1_B,Height:10" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="求才共版" Editor="text" FieldName="SalesType1_C" Format="download,folder:Files/JBERP_SalesPaper/SalesType1_C,Height:10" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="便利A面" Editor="text" FieldName="SalesType31_A" Format="download,folder:Files/JBERP_SalesPaper/SalesType31_A,Height:10" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="便利B面" Editor="text" FieldName="SalesType31_B" Format="download,folder:Files/JBERP_SalesPaper/SalesType31_B,Height:10" MaxLength="0" Visible="true" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="開始日期" Condition="&gt;=" DataType="string" Editor="datebox" FieldName="PaperDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" DefaultMethod="GetDefaultStartDate" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="結束日期" Condition="&lt;=" DataType="string" Editor="datebox" FieldName="PaperDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" DefaultMethod="GetDefaultLastDate" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="上傳夾報" Width="600px" DialogTop="50px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPSalesPaper" HorizontalColumnsCount="1" RemoteName="sERPSalesPaper.ERPSalesPaper" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="True" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="OnApplydataFormMaster" OnCancel="OnCanceldataFormMaster" OnLoadSuccess="OnLoadSuccessdataFormMaster">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="夾報日期" Editor="datebox" FieldName="PaperDate" Format="" Width="255" PlaceHolder="" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="求才A面" Editor="text" FieldName="SalesType1_A" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="求才B面" Editor="text" FieldName="SalesType1_B" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="求才共版" Editor="text" FieldName="SalesType1_C" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="便利A面" Editor="text" FieldName="SalesType31_A" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="便利B面" Editor="text" FieldName="SalesType31_B" Format="" maxlength="0" Width="180" ReadOnly="True" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="123" FieldName="AutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="PaperDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<input type="button" id="Button1" value="上傳" onclick="onclickButton1()" style="width:67px" ><label style='color:red'>※選擇檔案時，請選擇jpeg檔</label><br />
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
