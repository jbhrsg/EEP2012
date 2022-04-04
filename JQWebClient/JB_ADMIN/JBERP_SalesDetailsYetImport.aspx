<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesDetailsYetImport.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var sDate = new Date();
            var date2 = $.jbjob.Date.DateFormat(sDate, 'yyyy/MM/dd').substring(0, 7);
            $("#InvoiceYM_Query").val(date2);//發票年月
            $("#SalesTypeID_Query").combobox('setValue', "");
        });
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridMaster') {
                var result = [];
                result.push("IsInvoice=0");
                var InvoiceYM = $('#InvoiceYM_Query').val();//發票年月
                if (InvoiceYM != '') result.push("d.InvoiceYM = '" + InvoiceYM + "'");

                var SalesID = $('#SalesID_Query').combobox('getValue');//業務
                if (SalesID != '') result.push("d.SalesID = '" + SalesID + "'");

                var SalesTypeID = $('#SalesTypeID_Query').combobox('getValue');//交易別
                if (SalesTypeID != '') result.push("d.SalesTypeID = '" + SalesTypeID + "'");

                var CustNO = $('#CustNO_Query').combobox('getValue');//客戶代號
                if (CustNO != '') result.push("d.CustNO = '" + CustNO + "'");

                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }
        //
        function OnUpdatedReload() {
            $('#dataGridMaster').datagrid('reload');
        }
        //檢查字串是否符合發票年月
        function CheckStrWildWord(str) {
            var r = str.match(/^(\d{4})(\/)(0[1-9]|1[0-2])$/);
            if (r == null) return false;
            var d = new Date(r[1], (r[3] - 1), 1);
            return (d.getFullYear() == r[1] && d.getMonth() == (r[3] - 1) && d.getDate() == 1);
        }
        function MasterOnLoadSuccess() {
            //Grid選取單選,checkbox多選
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                $(this).datagrid({
                    singleSelect: true,
                    selectOnCheck: false,
                    checkOnSelect: false
                });                
            }
        }
        //是否開發票
        function genCheckBox(val) {
            if (val != "0")
                return "<input  type='checkbox' checked='true' onclick='return false;'/>";
            else
                return "<input  type='checkbox' onclick='return false;'/>";
        }
        function DefaultYM() {            
            var sDate = new Date();
            return $.jbjob.Date.DateFormat(sDate, 'yyyy/MM/dd').substring(0, 7);
        }
        //呼叫視窗新增
        function OpenUpdateInvoiceYM() {
            if ($("#dataGridMaster").datagrid('getChecked').length == 0) {
                alert('請勾選修改項目。');
            } else {
                openForm('#Dialog_UpdateYM', {}, "inserted", 'dialog');
            }
        }
        //銷貨明細Grid勾選失效
        function UpdateInvoiceYM() {
            //發票年月	
            var NewYM = $("#dataFormSalesDetailInvoiceYM").val();
            if (NewYM == "") {
                alert('發票年月不可以空白!');
            } else {
                var pre = confirm("確定修改?");
                if (pre == true) {
                    var rows = $('#dataGridMaster').datagrid('getChecked');
                    var aSalesDetails = [];
                    var SalesMasterNO = "";
                    for (var i = 0; i < rows.length; i++) {
                        if (i == 0) {
                            SalesMasterNO = rows[0].SalesMasterNO;
                        }
                        aSalesDetails.push(rows[i].ItemSeq);
                    }
                    var sItemSeq = aSalesDetails.join('*');
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sSalesInvoiceYMAdjust.ERPSalesDetails', //連接的Server端，command
                        data: "mode=method&method=" + "UpdateInvoiceYM" + "&parameters=" + SalesMasterNO + "," + NewYM + "," + sItemSeq, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            alert('修改成功！');
                            $('#dataGridMaster').datagrid("reload");
                            closeForm('#Dialog_UpdateYM');
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            alert(xhr.status);
                            alert(thrownError);
                        }
                    });
                }
            }

        }
     </script>        
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sSalesInvoiceYMAdjust.ERPSalesDetails" runat="server" AutoApply="True"
                DataMember="ERPSalesDetails" Pagination="False" QueryTitle="銷貨明細條件"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" PageList="20,30,40,50" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdated="OnUpdatedReload" OnLoadSuccess="MasterOnLoadSuccess" Height="520px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesMasterNO" Editor="numberbox" FieldName="SalesMasterNO" Format="" Width="120" ReadOnly="True" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" Format="" MaxLength="0" Width="120" ReadOnly="True" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="90" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="100" />
                    <JQTools:JQGridColumn Alignment="center" Caption="業務人員" Editor="text" FieldName="SalesName" Format="" MaxLength="0" Width="60" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="交易別" Editor="text" FieldName="SalesTypeName" Format="" MaxLength="0" Width="60" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="" Width="70" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="發票年月" Editor="text" FieldName="InvoiceYM" Format="" MaxLength="0" Width="70" />
                    <JQTools:JQGridColumn Alignment="center" Caption="開發票" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsInvoice" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="50" />
                    <JQTools:JQGridColumn Alignment="center" Caption="贈期" Editor="text" FieldName="GrantTypeID" Format="" MaxLength="0" Width="38" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="客戶行數" Editor="text" FieldName="CustLines" Format="" Width="55" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="客戶單價" Editor="text" FieldName="CustPrice" Format="" Width="55" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="客戶總價" Editor="text" FieldName="CustAmt" Format="" Width="70" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="發稿行數" Editor="text" FieldName="OfficeLines" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="55" />
                    <JQTools:JQGridColumn Alignment="right" Caption="繳社單價" Editor="text" FieldName="OfficePrice" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="55" />
                    <JQTools:JQGridColumn Alignment="right" Caption="繳社總價" Editor="text" FieldName="OfficeAmt" MaxLength="0" Width="70" ReadOnly="True" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                    <JQTools:JQToolItem Enabled="True" ItemType="easyui-linkbutton" OnClick="OpenUpdateInvoiceYM" Text="整批修改" Visible="True" Icon="icon-back"/>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="發票年月" Condition="=" DataType="string" Editor="text" FieldName="InvoiceYM" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="60" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sSalesInvoiceYMAdjust.infoSalesManAll',tableName:'infoSalesManAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="交易別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sSalesInvoiceYMAdjust.infoSalesTypeAll',tableName:'infoSalesTypeAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶代號" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sSalesInvoiceYMAdjust.infoCustNO',tableName:'infoCustNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="190" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" EnableTheming="True" ID="validateMaster" >
    <Columns>
        <JQTools:JQValidateColumn CheckMethod="CheckStrWildWord" CheckNull="True" FieldName="InvoiceYM" RemoteMethod="False" ValidateMessage="發票年月格式錯誤!" ValidateType="None" />
    </Columns>
</JQTools:JQValidate>
                    <JQTools:JQDialog ID="Dialog_UpdateYM" runat="server" BindingObjectID="dataFormSalesDetail" EditMode="Dialog" Title="發票年月修正" DialogLeft="150px" DialogTop="150px" Width="400px" ShowSubmitDiv="False">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <JQTools:JQDataForm ID="dataFormSalesDetail" runat="server" Closed="False" ContinueAdd="False" DataMember="ERPSalesDetails" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="4" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sSalesInvoiceYMAdjust.ERPSalesDetails" ShowApplyButton="False" ValidateStyle="Hint" ParentObjectID="">
                                                <Columns>
                                                    <JQTools:JQFormColumn Alignment="left" Caption="發票年月" Editor="text" EditorOptions="" FieldName="InvoiceYM" NewRow="True" ReadOnly="False" Span="1" Visible="True" Width="90" MaxLength="0" RowSpan="1" />
                                                </Columns>
                                            </JQTools:JQDataForm>
                                            <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormSalesDetail" EnableTheming="True">
                                                <Columns>
                                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="DefaultYM" FieldName="InvoiceYM" RemoteMethod="False" />
                                                </Columns>
                                            </JQTools:JQDefault>
                                            <JQTools:JQValidate ID="validatedataFormSalesDetail" runat="server" BindingObjectID="dataFormSalesDetail" EnableTheming="True">
                                                <Columns>
                                                    <JQTools:JQValidateColumn CheckMethod="CheckStrWildWord" CheckNull="True" FieldName="InvoiceYM" RemoteMethod="False" ValidateMessage="發票年月格式錯誤!" ValidateType="None" />
                                                </Columns>
                                            </JQTools:JQValidate>
                                        </td>
                                        <td style="vertical-align: bottom">
                                            <input id="bnAssign" type="button" value="修改發票年月" onclick="UpdateInvoiceYM()"/>
                                        </td>
                                    </tr>
                                </table>
                             </JQTools:JQDialog>                           
</form>
</body>
</html>
