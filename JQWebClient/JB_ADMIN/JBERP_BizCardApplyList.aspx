<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_BizCardApplyList.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#MarkButton").linkbutton({ 'plain': false });
            $('#IsPrint_Query').closest('td').append($('.infosysbutton-q', '#querydataGridMaster').closest('td').contents());
        });
        function OnLoad_dataGridMaster() {
            $("#dataGridMaster").datagrid("unselectAll");
            if (!$(this).data('firstLoadv') && $(this).data('firstLoadv', true)) { //只執行第一次
                var where = $("#dataGridMaster").datagrid("getWhere");
                $("#dataGridMaster").datagrid("setWhere", where);
            }
        }
        function OnClick_MarkButton() {
            if ($("#dataGridMaster").datagrid('getChecked').length == 0) {
                alert('請勾選項目。');
            } else {
                    var rows = $('#dataGridMaster').datagrid('getChecked');
                    var aCardNO = [];
                    for (var i = 0; i < rows.length; i++) {
                        aCardNO.push(rows[i].BizCardNO);
                    }
                    var sCardNO = aCardNO.join('*');
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sERPBizCardApply.ERPBizCardApply',
                        data: "mode=method&method=" + "Update_ERPBizCardApply" + "&parameters=" + sCardNO,
                        cache: false,
                        async: false,
                        success: function (data) {
                            if (data != false) {
                                alert("勾選成功");
                            } else { alert("勾選失敗"); }
                            $('#dataGridMaster').datagrid("reload");
                        }
                    });
            }
        }
        //function FormatScript_IsPrint(val, row, index) {
        //    if (val == true || val == 1) { return 'V' }
        //    else { return ''}
        //}
        function FormScript_FlowFlag(val, row, index) {
            if (val == 'Z') { return '結案' }
            else if (val == 'P') { return '流程中' }
            else if (val == 'N') { return '新申請' }
            else if (val == 'X') { return '作廢' }
            else { return val }
        }
        function OnLoad_JQDataForm1(row) {
            if (row.FlowFlag == 'Z') { $("#JQDataForm1FlowFlag").val('結案') }
            else if (row.FlowFlag == 'P') { $("#JQDataForm1FlowFlag").val('流程中') }
            else if (row.FlowFlag == 'N') { $("#JQDataForm1FlowFlag").val('新申請') }
            else if (row.FlowFlag == 'X') { $("#JQDataForm1FlowFlag").val('作廢') }
            else { return $("#JQDataForm1").val(row.FlowFlag) }

            //if (row.IsPrint == true || row.IsPrint == 1) { $("#JQDataForm1IsPrint").val('V') }
            //else { $("#JQDataForm1IsPrint").val(' ') }

            //if (row.IsUrgent == true) { $("#JQDataForm1IsUrgent").val('V') }
            //else { $("#JQDataForm1IsUrgent").val('') }
        }
        function queryGrid(dg) {
            var where=$(dg).datagrid("getWhere");
            $(dg).datagrid("setWhere",where);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERPBizCardApply.ERPBizCardApplyList" runat="server" AutoApply="True"
                DataMember="ERPBizCardApplyList" Pagination="True" QueryTitle="Query"
                Title="名片申請待印清單" AllowDelete="True" AllowInsert="False" AllowUpdate="True" QueryMode="Panel" AlwaysClose="True" AllowAdd="True" ViewCommandVisible="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" OnLoadSuccess="OnLoad_dataGridMaster" EditDialogID="JQDialog1">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="申請單號" Editor="text" FieldName="BizCardNO" Format="" MaxLength="50" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="已送印" Editor="text" FieldName="IsPrint" Format="" Width="40" EditorOptions="" FormatScript="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="速別" Editor="text" FieldName="IsUrgent" Width="30" FormatScript="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請狀態" Editor="text" FieldName="FlowFlag" FormatScript="FormScript_FlowFlag" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者" Editor="infocombobox" FieldName="ApplyEmpID" Format="" MaxLength="10" Width="60" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERPBizCardApply.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="right" Caption="盒數" Editor="numberbox" FieldName="Quantity" Format="" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="名片檔" Editor="text" FieldName="FilePath" Format="download,folder:JB_ADMIN/JBERP_BizCardApply/origin" MaxLength="100" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Remark" Format="" MaxLength="2147483647" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請時間" Editor="text" FieldName="CreateDate" Format="" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="Cname" Format="" MaxLength="50" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="英文姓名" Editor="text" FieldName="Ename" Format="" MaxLength="50" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門/職稱" Editor="text" FieldName="Title" Format="" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="任職地點" Editor="text" FieldName="Workplace" Format="" MaxLength="50" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="任職地址" Editor="text" FieldName="WorkplaceA" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司電話" Editor="text" FieldName="WorkplaceTEL" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="分機" Editor="text" FieldName="ExtNum" Format="" MaxLength="50" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="傳真" Editor="text" FieldName="FaxNum" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="手機" Editor="text" FieldName="PhoneNum" Format="" MaxLength="100" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Skype" Editor="text" FieldName="Skype" Format="" MaxLength="100" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LineID" Editor="text" FieldName="LineID" Format="" MaxLength="100" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Email" Editor="text" FieldName="Email" Format="" MaxLength="100" Width="60" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" ItemType="easyui-linkbutton" Text="送印製" Visible="True" ID="MarkButton" Icon="icon-next" OnClick="OnClick_MarkButton" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="已送印" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'否',text:'否',selected:'true'},{value:'是',text:'是',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsPrint" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" DefaultValue="" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="JQDataForm1" Closed="True" DataFormTabID="JQDataForm1" DialogTop="10px" Width="580px" Title="名片申請單">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="ERPBizCardApplyList" RemoteName="sERPBizCardApply.ERPBizCardApplyList" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoad_JQDataForm1">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="申請單號" Editor="text" FieldName="BizCardNO" MaxLength="50" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請時間" Editor="text" FieldName="CreateDate" MaxLength="4" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="infocombobox" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERPBizCardApply.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyEmpID" MaxLength="10" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="304" />
                        <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="text" FieldName="Cname" MaxLength="50" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="英文姓名" Editor="text" FieldName="Ename" MaxLength="50" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="任職地點" Editor="text" FieldName="Workplace" MaxLength="50" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="任職地址" Editor="text" FieldName="WorkplaceA" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門/職稱" Editor="text" FieldName="Title" MaxLength="50" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司電話" Editor="text" FieldName="WorkplaceTEL" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分機" Editor="text" FieldName="ExtNum" MaxLength="50" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳真" Editor="text" FieldName="FaxNum" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="PhoneNum" MaxLength="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Skype" Editor="text" FieldName="Skype" MaxLength="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LineID" Editor="text" FieldName="LineID" MaxLength="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Email" Editor="text" FieldName="Email" MaxLength="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="盒數" Editor="text" FieldName="Quantity" MaxLength="4" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="Remark" MaxLength="2147483647" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" EditorOptions="height:30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="名片瀏覽檔" Editor="text" FieldName="FilePath" MaxLength="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" Format="download,folder:JB_ADMIN/JBERP_BizCardApply/origin" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請狀態" Editor="text" FieldName="FlowFlag" MaxLength="1" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="速別" Editor="text" FieldName="IsUrgent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="已送印" Editor="text" FieldName="IsPrint" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                    </Columns>
                </JQTools:JQDataForm>
            </JQTools:JQDialog>
        </div>

    </form>
</body>
</html>
