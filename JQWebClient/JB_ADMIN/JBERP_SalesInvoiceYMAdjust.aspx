<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesInvoiceYMAdjust.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var sDate = new Date();
            var vDate = new Date($.jbDateAdd('months', 1, sDate));
            var date1 = $.jbjob.Date.DateFormat(sDate, 'yyyy/MM/dd').substring(0,7);
            var date2 = $.jbjob.Date.DateFormat(vDate, 'yyyy/MM/dd').substring(0, 7);
            $("#SalesDate_Query").val(date1);//銷貨年月
            $("#InvoiceYM_Query").val(date2);//發票年月
            var UserID = getClientInfo("UserID");
            setTimeout(function () {
                var data = $("#SalesID_Query").combobox('getData');
                for (var i = 0; i < data.length; i++) {
                    if (data[i].SalesEmployeeID == UserID) {
                        $("#SalesID_Query").combobox('setValue', data[i].SalesID);
                    }
                }
            }, 200);                       


        });
      
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridMaster') {
                var result = [];
                var SalesDateYM = $('#SalesDate_Query').val();//銷貨年月
                if (SalesDateYM != '') result.push("LEFT(convert(nvarchar(10),d.SalesDate,111),7) = '" + SalesDateYM + "'");

                var InvoiceYM = $('#InvoiceYM_Query').val();//發票年月
                if (InvoiceYM != '') result.push("d.InvoiceYM = '" + InvoiceYM + "'");

                var SalesMasterNO = $('#SalesMasterNO_Query').val();//序號
                if (SalesMasterNO != '') result.push("d.SalesMasterNO = '" + SalesMasterNO + "'");

                var SalesID = $('#SalesID_Query').combobox('getValue');//業務
                if (SalesID != '') result.push("d.SalesID = '" + SalesID + "'");

                var SalesTypeID = $('#SalesTypeID_Query').combobox('getValue');//交易別
                if (SalesTypeID != '') result.push("d.SalesTypeID = '" + SalesTypeID + "'");

                var CustNO = $('#CustNO_Query').combobox('getValue');//客戶代號
                if (CustNO != '') result.push("d.CustNO = '" + CustNO + "'");

                var IsTransSys = $('#IsTransSys_Query').combobox('getValue');//是否已匯入
                if (IsTransSys != '2') result.push("d.IsTransSys = '" + IsTransSys + "'");

                $(dg).datagrid('setWhere', result.join(' and '));

                ShowPatchButton(SalesMasterNO, IsTransSys);
            }
        }

        function ShowPatchButton(SalesMasterNO,IsTransSys) {                       
            var sUserID = getClientInfo("UserID");
            if (SalesMasterNO != '' && IsTransSys != '2' && (sUserID == "020" || sUserID == "060")) {
                $("#toolItemdataGridMaster整批修改").show();
            } else if (SalesMasterNO != '' && IsTransSys == '0') {//未匯入 0
                $("#toolItemdataGridMaster整批修改").show();
            } else $("#toolItemdataGridMaster整批修改").hide();

        }

        //是否開發票
        function genCheckBox(val) {
            if (val != "0")
                return "<input  type='checkbox' checked='true' onclick='return false;'/>";
            else
                return "<input  type='checkbox' onclick='return false;'/>";
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
        function DefaultYM() {            
            var sDate = new Date();
            return $.jbjob.Date.DateFormat(sDate, 'yyyy/MM/dd').substring(0, 7);
        }
        //呼叫視窗新增
        function OpenUpdateSalesDetails() {
            var IsTransSys = $('#IsTransSys_Query').combobox('getValue');//是否已匯入
            if (IsTransSys == 2) {//0未匯入,2不拘
                alert('查詢條件不拘狀態下無法批次修改。');
            }else if ($("#dataGridMaster").datagrid('getChecked').length == 0) {
                alert('請勾選修改項目。');
            } else {
                openForm('#Dialog_UpdateYM', {}, "inserted", 'dialog');
            }
        }
        //選取查詢選項是否已匯入=>0未匯入,1已匯入,2不拘(2不拘時=>隱藏整批修改)
        //function SelectIsTransSys(rowData) {
        //    var SalesMasterNO = $('#SalesMasterNO_Query').val();//序號不可空白
        //    if (SalesMasterNO != '') {
        //        if (rowData.value == '1' || rowData.value == '2') {
        //            $("#toolItemdataGridMaster整批修改").hide();
        //            var sUserID = getClientInfo("UserID");
        //            if (rowData.value == '1' && (sUserID == "020" || sUserID == "060")) {
        //                $("#toolItemdataGridMaster整批修改").show();
        //            }
        //        } else {
        //            $("#toolItemdataGridMaster整批修改").show();
        //        }
        //    } else $("#toolItemdataGridMaster整批修改").hide();
        //}

        //銷貨資料修正=>修改類別 選取控制
        function OnSelectClass(rowData) {
            //版別,區域,發票年月,單位數,見刊,出刊備註,PDF檔名     
            var FormName = '#dataFormSalesDetail';
            var HideFieldName = ['DMTypeID', 'ViewAreaID', 'InvoiceYM', 'SalesQty', 'SalesQtyView', 'SalesDescr', 'Remark1'];

            //隱藏
            $.each(HideFieldName, function (index, fieldName) {
                $(FormName + fieldName).closest('td').prev('td').hide();
                $(FormName + fieldName).closest('td').hide();
            });
            //顯示
            $(FormName + rowData).closest('td').prev('td').show();
            $(FormName + rowData).closest('td').show();

       }
      
        function bnUpdate(val, rowData) {
            if (val != null) {
                return '<a href="../handler/HunterFileHandler.ashx?File=' + val + '&UserID=' + rowData.UserID + '">' + val + '</a>';
            }
        }
        function OnLoadSuccessDF() {

            //修改類別 infooptions預設為發票年月	
            var iClass = $('#dataFormSalesDetailClass');
            iClass.options('setValue', 1);
            var onSelect = getInfolightOption(iClass).onSelect;
            if (onSelect) { onSelect.call(iClass, iClass.options('getCheckedValue')); };            

        }

        //銷貨明細Grid勾選
        function UpdateSalesDetails() {
            var NewData="";
            var sClass = $('#dataFormSalesDetailClass').options('getValue');            
            if (sClass == "") {
                alert('請選擇修改類別!');
            } else {
                var FormName = '#dataFormSalesDetail';
                //版別 & 區域
                if (sClass == "DMTypeID" || sClass == "ViewAreaID") {
                    NewData = $(FormName + sClass).combobox('getValue');
                } else NewData = $(FormName + sClass).val();

                //if (NewData != "") {
                    var pre = confirm("確定修改?");
                    if (pre == true) {
                        var rows = $('#dataGridMaster').datagrid('getChecked');
                        var aSalesDetails = [];
                        var SalesMasterNO = "";
                        var TransSys = 0;
                        for (var i = 0; i < rows.length; i++) {
                         
                                if (i == 0) {
                                    SalesMasterNO = rows[0].SalesMasterNO;
                                    TransSys = rows[0].IsTransSys;
                                }
                                aSalesDetails.push(rows[i].ItemSeq);

                        }
                        var sItemSeq = aSalesDetails.join('*');

                        $.ajax({
                            type: "POST",
                            url: '../handler/jqDataHandle.ashx?RemoteName=sSalesInvoiceYMAdjust.ERPSalesDetails', //連接的Server端，command
                            data: "mode=method&method=" + "UpdateSalesDetails" + "&parameters=" + encodeURIComponent(SalesMasterNO + "," + NewData + "," + sItemSeq + "," + sClass + "," + TransSys), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
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
                //} else {
                //    alert('請填入要修改資料！');
                //}
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
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" PageList="20,30,40,50" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdated="OnUpdatedReload" OnLoadSuccess="MasterOnLoadSuccess" Height="510px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="序號" Editor="numberbox" FieldName="SalesMasterNO" Format="" Width="50" ReadOnly="True" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" Format="" MaxLength="0" Width="120" ReadOnly="True" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="80" ReadOnly="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="100" />
                    <JQTools:JQGridColumn Alignment="center" Caption="交易別" Editor="text" FieldName="SalesTypeName" Format="" MaxLength="0" Width="54" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="版別" Editor="text" FieldName="DMTypeName" Format="" Width="52" EditorOptions="" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />                   
                    <JQTools:JQGridColumn Alignment="center" Caption="區域" Editor="text" FieldName="ViewAreaName" Format="" Width="40" EditorOptions="" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />                  
                    <JQTools:JQGridColumn Alignment="center" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="yyyy-mm-dd" Width="65" FormatScript="" Frozen="False" Sortable="True" ReadOnly="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Visible="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="星期" Editor="text" FieldName="dWeekday" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="35">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="需發票" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsInvoice" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" />
                    <JQTools:JQGridColumn Alignment="center" Caption="發票年月" Editor="text" FieldName="InvoiceYM" Format="" Width="50" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="客單價" Editor="numberbox" FieldName="CustPrice" Format="" Width="40" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" OnTotal="" />                        
                    <JQTools:JQGridColumn Alignment="right" Caption="客總額" Editor="numberbox" FieldName="CustAmt" Format="" Width="40" FormatScript="" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" />  
                    <JQTools:JQGridColumn Alignment="center" Caption="贈期" Editor="" FieldName="GrantTypeID" Format="" Width="30" EditorOptions="" Sortable="True" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Visible="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="單位數" Editor="text" FieldName="SalesQty" Format="" Width="40" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />                        
                    <JQTools:JQGridColumn Alignment="center" Caption="見刊" Editor="text" FieldName="SalesQtyView" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="30" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="出刊備註" Editor="text" FieldName="SalesDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PDF檔名" Editor="text" FieldName="Remark1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100"> </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="已匯入?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsTransSys" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="50" />
                    <JQTools:JQGridColumn Alignment="center" Caption="業務人員" Editor="text" FieldName="SalesName" Format="" MaxLength="0" Width="60" ReadOnly="True" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                    <JQTools:JQToolItem Enabled="True" ItemType="easyui-linkbutton" OnClick="OpenUpdateSalesDetails" Text="整批修改" Visible="True" Icon="icon-back"/>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨年月" Condition="=" DataType="string" Editor="text" FieldName="SalesDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="60" Format="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="發票年月" Condition="=" DataType="string" Editor="text" FieldName="InvoiceYM" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="60" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="序號" Condition="=" DataType="number" Editor="text" FieldName="SalesMasterNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="60" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sSalesInvoiceYMAdjust.infoSalesMan',tableName:'infoSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="交易別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sR_SalesDetails.infoSalesType',tableName:'infoSalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶代號" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sSalesInvoiceYMAdjust.infoCustNO',tableName:'infoCustNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="150" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="是否已匯入" Condition="=" DataType="number" Editor="infocombobox" FieldName="IsTransSys" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" EditorOptions="items:[{value:'0',text:'未匯入',selected:'true'},{value:'1',text:'已匯入',selected:'false'},{value:'2',text:'不拘',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Format="" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" EnableTheming="True" ID="validateMaster" >
    <Columns>
        <JQTools:JQValidateColumn CheckMethod="CheckStrWildWord" CheckNull="True" FieldName="InvoiceYM" RemoteMethod="False" ValidateMessage="發票年月格式錯誤!" ValidateType="None" />
    </Columns>
</JQTools:JQValidate>
                    <JQTools:JQDialog ID="Dialog_UpdateYM" runat="server" BindingObjectID="dataFormSalesDetail" EditMode="Dialog" Title="銷貨資料修正" DialogLeft="150px" DialogTop="70px" Width="600px" ShowSubmitDiv="False">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <JQTools:JQDataForm ID="dataFormSalesDetail" runat="server" Closed="False" ContinueAdd="False" DataMember="ERPSalesDetails" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="1" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sSalesInvoiceYMAdjust.ERPSalesDetails" ShowApplyButton="False" ValidateStyle="Hint" ParentObjectID="" OnLoadSuccess="OnLoadSuccessDF" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0">
                                                <Columns>
                                                    <JQTools:JQFormColumn Alignment="left" Caption="修改類別" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:400,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:4,multiSelect:false,openDialog:false,selectAll:false,onSelect:OnSelectClass,selectOnly:false,items:[{text:'版別',value:'DMTypeID'},{text:'區域',value:'ViewAreaID'},{text:'發票年月',value:'InvoiceYM'},{text:'單位數',value:'SalesQty'},{text:'見刊',value:'SalesQtyView'},{text:'出刊備註',value:'SalesDescr'},{text:'PDF檔名',value:'Remark1'}]" FieldName="Class" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="版別" Editor="infocombobox" EditorOptions="valueField:'DMTypeID',textField:'DMTypeName',remoteName:'sERPSalseDetails.infoERPDMType',tableName:'infoERPDMType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="DMTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="區域" Editor="infocombobox" EditorOptions="valueField:'ViewAreaID',textField:'ViewAreaName',remoteName:'sERPSalseDetails.infoERPViewArea',tableName:'infoERPViewArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ViewAreaID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="發票年月" Editor="text" FieldName="InvoiceYM" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" EditorOptions="" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="單位數" Editor="numberbox" FieldName="SalesQty" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="70" MaxLength="0" RowSpan="1" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="見刊" Editor="numberbox" FieldName="SalesQtyView" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="70" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="出刊備註" Editor="textarea" FieldName="SalesDescr" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" EditorOptions="height:50" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="PDF檔名" Editor="text" FieldName="Remark1" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="280" />
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
                                    </tr>
                                    <tr>
                                        <td align="center"><a href="#" id='UpdateLink' onclick='UpdateSalesDetails.call(this)' class="easyui-linkbutton" data-options="plain:false">修改</a></td>
                                    </tr>
                                </table>
                             </JQTools:JQDialog>                           
</form>
</body>
</html>
