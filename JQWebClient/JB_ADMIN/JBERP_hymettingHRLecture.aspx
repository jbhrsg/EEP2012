<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_hymettingHRLecture.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
    function dataGridView_OnLoadSucess() {
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                $(this).datagrid({
                    singleSelect: true,//單選
                    selectOnCheck: false,//check不select
                    checkOnSelect: false//select不check
                });
            }
    }

    //選擇轉入銷貨
    function SelectCreateSales() {
        var rows = $('#dataGridView').datagrid("getChecked");
        if (rows.length <= 0) {
            alert('注意!!未選取報名資料,請選取');
            return false;
        }
        var rows = $('#dataGridView').datagrid("getChecked");
        var amount = 0;
        var before1 = '';
        var before2 = '';
        var after = '';
        var scount = 0;
        var samt = 0;
        var fcount = 0;
        var famt = 0;

        var AmountIsZero = 0;
        var arrZeroAmount = [];
        var IsEmpty = 0;
        var arrIsEmpty = [];
        var TaxNONot8 = 0;
        var arrTaxNONot8 = [];
        var ConvertToSalesIsTrue = 0;//已轉入
        var arrConvertToSalesIsTrue = [];

        for (var i = 0; i <= rows.length - 1; i++) {
            //amount = amount + rows[i].SalesAmount;
            //擋金額為0  
            if ($.trim(rows[i].Amount) == 0) {
                AmountIsZero = 1;
                arrZeroAmount.push(rows[i].AutoKey);
            }
            //擋發票類別沒填   //公司別，銷貨類別一定有，因報名就寫常數
            if ($.trim(rows[i].QInvoiceTypeID) == '') {
                IsEmpty = 1;
                arrIsEmpty.push(rows[i].AutoKey);
            }
            //擋三聯，統編沒8碼
            if ($.trim(rows[i].QInvoiceTypeID) == "98" && $.trim(rows[i].TaxNO).length != 8) {
                TaxNONot8 = 1;
                arrTaxNONot8.push(rows[i].AutoKey);
            }
            //已轉入
            if ($.trim(rows[i].IsConvertToSales) == 'true' ) {
                ConvertToSalesIsTrue = 1;
                arrConvertToSalesIsTrue.push(rows[i].AutoKey);
            }
        }

        if (AmountIsZero == 1) {
            alert("無法轉入銷貨" + '\n' + "報名序號:" + arrZeroAmount.join('，') + "的金額為0，不須開銷貨");
            return false;
        }
            //三聯的銷貨，統編一定要八碼
        else if (IsEmpty == 1) {
            alert("無法轉入銷貨" + '\n' + "報名序號:" + arrIsEmpty.join('，') + "的發票類別沒選");
            return false;
        }
            //單據類別只能是2,3聯
        else if (TaxNONot8 == 1) {
            alert("無法轉入銷貨" + '\n' + "報名序號:" + arrTaxNONot8.join('，') + "的統編不為8碼");
            return false;
        }

        //before1 = '注意!!您已選取要轉入收據的資訊如下:';
        //before2 = '筆數:' + i.toString() + '\n' + '銷貨金額:' + toCurrency(amount);
        var yn;
        if (arrConvertToSalesIsTrue.length > 0) {
            yn = confirm("報名序號:" + arrConvertToSalesIsTrue.join('，') + "已轉入銷貨過，按下「確定」就會再次寫入銷貨");
        }
        else {
            yn = confirm("按下「確定」就會寫入銷貨");
        }
        if (yn == false) {
            return
        }
        //CreateSales
        for (var i = 0; i <= rows.length - 1; i++) {
            var returnValue = CreateSales(rows[i].InsGroupID, rows[i].AutoKey);
            if (returnValue == "True") {
                scount = scount + 1;
                //samt = samt + rows[i].SalesAmount;
            }
            else {
                fcount = fcount + 1;
                //famt = famt + rows[i].SalesAmount;
            }
        }

        after = '寫入銷貨資訊如下:' + '\n'  + '成功筆數:' + scount.toString() + '\n' + '失敗筆數:' + fcount.toString();
        alert(after);
        $('#dataGridView').datagrid('reload');
        //openForm('#JQDialog3', $('#dataGridView').datagrid('getSelected'), "update", 'dialog');;
    }

    //產生銷貨
    function CreateSales(InsGroupID, AutoKey) {
        if (InsGroupID != '' && AutoKey != '' && InsGroupID !== undefined && AutoKey !== undefined)
        var ReturnStr = '';
        var UserID = getClientInfo("_username");
        $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sERP_hymeetingHRLecture.hymeetingHRLecture',
            data: "mode=method&method=" + "procHRLectureCreateSales" + " &parameters=" + InsGroupID + "," + AutoKey + "," + UserID,
            cache: false,
            async: false,
            success: function (data) {
                //if (data != false) {
                    ReturnStr = data;
                //}
            }
        });
        return ReturnStr;
    }

    function IsConvertToSales_FormatScript(val) {
        if (val == true)
            return "<input  type='checkbox' checked='true' onclick='return  false;'/>";
        else
            return "<input  type='checkbox' onclick='return false;'/>";
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERP_hymeetingHRLecture.hymeetingHRLecture" runat="server" AutoApply="True"
                DataMember="hymeetingHRLecture" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
                Title="人資講堂報名資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="20,40,60,80,100" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnLoadSuccess="dataGridView_OnLoadSucess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" Visible="True" Width="50" Sortable="False" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_hymeetingHRLecture.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="已轉過" Editor="text" FieldName="IsConvertToSales" MaxLength="0" Visible="true" Width="40" Sortable="True" FormatScript="IsConvertToSales_FormatScript" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="課程代號" Editor="text" FieldName="Coscode" MaxLength="0" Visible="true" Width="60" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="課程日期" Editor="text" FieldName="Cosdate" MaxLength="0" Visible="true" Width="60" Sortable="False" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="Descr" Visible="true" Width="120" ReadOnly="False" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司" Editor="text" FieldName="Company" MaxLength="0" Visible="true" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="Name" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動費用" Editor="text" FieldName="TuitionFee" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="性別" Editor="text" FieldName="Sex" MaxLength="0" Visible="true" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="統編" Editor="text" FieldName="TaxNO" MaxLength="0" Visible="true" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="訊息來源" Editor="text" FieldName="MessageSource" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="Telephone" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="手機" Editor="text" FieldName="Cellphone" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="付款方式" Editor="infocombobox" FieldName="PayWay" MaxLength="0" Visible="true" Width="60" EditorOptions="valueField:'PayWayID',textField:'PayWayName',remoteName:'sERP_hymeetingHRLecture.PayWay',tableName:'PayWay',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電子信箱" Editor="text" FieldName="Mail" MaxLength="0" Visible="true" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="text" FieldName="CreateBy" MaxLength="0" Visible="true" Width="90" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="傳真" Editor="text" FieldName="Fax" MaxLength="0" Visible="True" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="Department" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職稱" Editor="text" FieldName="Occupation" MaxLength="0" Visible="True" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="邀約人" Editor="text" FieldName="Inviter" MaxLength="0" Visible="True" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" Visible="true" Width="110" ReadOnly="True" Format="yyyy-mm-dd HH:MM:SS" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="text" FieldName="LastUpdateDate" MaxLength="0" Visible="true" Width="110" ReadOnly="True" Format="yyyy-mm-dd HH:MM:SS" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="InsGroupID" Visible="true" Width="80" ReadOnly="False" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_hymeetingHRLecture.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Education" Editor="text" FieldName="Education" MaxLength="0" Visible="False" Width="90" ReadOnly="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="序號" Editor="text" FieldName="AutoKey" MaxLength="0" Visible="true" Width="30" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="單據類別" Editor="infocombobox" FieldName="QInvoiceTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" EditorOptions="valueField:'INVOICETYPEID',textField:'INVOICETYPENAME',remoteName:'sERP_hymeetingHRLecture.QInvoiceType',tableName:'QInvoiceType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="單價" Editor="text" FieldName="Amount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="數量" Editor="text" FieldName="Quantity" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="Insert" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem"
                        Text="Update" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem"
                        Text="Delete" Visible="False"  />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton"
                        OnClick="apply" Text="Apply" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-upload" ItemType="easyui-linkbutton" OnClick="SelectCreateSales" Text="轉入銷貨" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="課程代號" Condition="%%" DataType="string" Editor="infocombobox" FieldName="Coscode" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'coscode',textField:'coscode',remoteName:'sERP_hymeetingHRLecture.descrQ',tableName:'descrQ',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="課程名稱" Condition="%%" DataType="string" Editor="infocombobox" FieldName="Descr" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'descr',textField:'descr',remoteName:'sERP_hymeetingHRLecture.descrQ',tableName:'descrQ',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="轉銷貨狀態" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'IsConvertToSales',textField:'IsConvertToSalesName',remoteName:'sERP_hymeetingHRLecture.IsConvertToSales',tableName:'IsConvertToSales',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsConvertToSales" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="人資講堂報名資料">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="hymeetingHRLecture" HorizontalColumnsCount="2" RemoteName="sERP_hymeetingHRLecture.hymeetingHRLecture" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="序號" Editor="text" FieldName="AutoKey" Width="160" Visible="True" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="課程代號" Editor="text" FieldName="Coscode" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="Descr" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="課程日期" Editor="datebox" FieldName="Cosdate" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動費用" Editor="text" FieldName="TuitionFee" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單價" Editor="numberbox" FieldName="Amount" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="數量" Editor="numberbox" FieldName="Quantity" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="text" FieldName="Name" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="text" FieldName="Sex" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電子信箱" Editor="text" FieldName="Mail" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="Telephone" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="Cellphone" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳真" Editor="text" FieldName="Fax" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司" Editor="text" FieldName="Company" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門" Editor="text" FieldName="Department" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="Occupation" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Education" Editor="text" FieldName="Education" maxlength="0" Width="160" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款方式" Editor="infocombobox" FieldName="PayWay" maxlength="0" Width="160" EditorOptions="valueField:'PayWayID',textField:'PayWayName',remoteName:'sERP_hymeetingHRLecture.PayWay',tableName:'PayWay',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訊息來源" Editor="text" FieldName="MessageSource" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統編" Editor="text" FieldName="TaxNO" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="邀約人" Editor="text" FieldName="Inviter" maxlength="0" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立者" Editor="text" FieldName="CreateBy" maxlength="0" Width="160" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Width="160" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改者" Editor="text" FieldName="LastUpdateBy" maxlength="0" Width="160" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Width="160" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="轉銷貨狀態" Editor="text" FieldName="IsConvertToSales" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_hymeetingHRLecture.SalesType',tableName:'SalesType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票類別" Editor="infocombobox" EditorOptions="valueField:'INVOICETYPEID',textField:'INVOICETYPENAME',remoteName:'sERP_hymeetingHRLecture.QInvoiceType',tableName:'QInvoiceType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="QInvoiceTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="InsGroupID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="160" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_hymeetingHRLecture.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
