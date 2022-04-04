<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBOUT_PettyCashYM.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //設定欄位Caption 變顏色
            var flagIDs = ['#dataFormMasterIsUrgentPay', '#dataFormMasterIsNotPayDate'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: '#8A2BE2' });
            });
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
            //
        })
        function dataGridViewLoadSucess() {
            var UserID = getClientInfo("UserID");
            if (UserID == '001' || UserID == '009') {
                setReadOnly($('#dataGridView'), false);
            }
            else {

                setReadOnly($('#dataGridView'), true);
            }
        }
        //檢查年月是否重複
        function CheckYearMonthDul() {
            var dataFormMasterYearMonth = $("#dataFormMasterYearMonth").val();
            if (dataFormMasterYearMonth == "" || dataFormMasterYearMonth == undefined) {
                alert('注意!!,未輸入年月,請選取');
                $("#dataFormMasterYearMonth").focus();
                return false;
            }
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var YearMonth = $('#dataFormMasterYearMonth').val();
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sPettyCashYM.YearMonth', //連接的Server端，command
                    data: "mode=method&method=" + "CheckYearMonthNODul" + "&parameters=" + YearMonth, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
                    alert('注意!!此年月已存在');
                    $('#dataFormMasterYearMonth').focus;
                    return false;
                }
            }
        }
       
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPettyCashYM.PettyCashYM" runat="server" AutoApply="True"
                DataMember="PettyCashYM" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="油資費率設定" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="780px" OnLoadSuccess="dataGridViewLoadSucess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="年月" Editor="text" FieldName="YearMonth" Format="" MaxLength="0" Width="50" />
                    <JQTools:JQGridColumn Alignment="center" Caption="起始日期" Editor="datebox" FieldName="StdDate" Format="yyyy/mm/dd" Width="65" />
                    <JQTools:JQGridColumn Alignment="center" Caption="終止日期" Editor="datebox" FieldName="EndDate" Format="yyyy/mm/dd" Width="65" />
                    <JQTools:JQGridColumn Alignment="right" Caption="汽車補助費率@每公里" Editor="numberbox" FieldName="CardOilRate" Format="N2" Width="130" EditorOptions="precision:2" />
                    <JQTools:JQGridColumn Alignment="right" Caption="機車補助費率@每公里" Editor="numberbox" FieldName="MotoOilRate" Format="N2" Width="130" EditorOptions="precision:2" />
                    <JQTools:JQGridColumn Alignment="right" Caption="Etag補助費率@每公里" Editor="numberbox" EditorOptions="precision:2" FieldName="EtagRate" Format="N2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="130" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="零用金年月設定" Width="520px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="PettyCashYM" HorizontalColumnsCount="2" RemoteName="sPettyCashYM.PettyCashYM" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApply="CheckYearMonthDul" IsAutoPause="False" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="年月" Editor="text" FieldName="YearMonth" Format="" maxlength="0" Width="116" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="StdDate" Format="yyyy/mm/dd" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止日期" Editor="datebox" FieldName="EndDate" Format="yyyy/mm/dd" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="汽車補助費率" Editor="numberbox" FieldName="CardOilRate" Format="" Width="116" EditorOptions="precision:2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="機車補助費率" Editor="numberbox" FieldName="MotoOilRate" Format="" Width="116" EditorOptions="precision:2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="控管天數" Editor="numberbox" FieldName="DaysBefore" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="116" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Etag補助費率" Editor="numberbox" EditorOptions="precision:2" FieldName="EtagRate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="116" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="StdDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="EndDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0.0" FieldName="CardOilRate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0.0" FieldName="MotoOilRate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1.20" FieldName="EtagRate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="4" FieldName="DaysBefore" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="False" FieldName="CardOilRate" RangeFrom="0.1" RangeTo="10" RemoteMethod="True" ValidateMessage="不得小於0" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="MotoOilRate" RangeFrom="0.1" RangeTo="10" RemoteMethod="True" ValidateMessage="不得小於0" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
