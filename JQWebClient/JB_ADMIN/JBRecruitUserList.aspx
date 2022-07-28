<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBRecruitUserList.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        $(document).ready(function () {
            //panel寬度調整
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 580 });           

            //查詢條件預設值-上個月
            var dt = new Date();
            var vDate = new Date($.jbDateAdd('months', -1, dt));

            var FirstDate = new Date($.jbGetFirstDate(vDate));
            $("#sDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));
            var LastDate = new Date($.jbGetLastDate(vDate));
            $("#eDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));       

        });

        function queryGrid(dg) {//查詢後添加固定條件
           
            if ($(dg).attr('id') == 'dataGridView') {
                //查詢條件
                var result = [];
                var JQDate1 = $("#sDate_Query").datebox("getValue");//datebox("getBindingValue");//datebox("getValue");                
                var JQDate2 = $("#eDate_Query").datebox("getValue");
                
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sJBRecruit.UserList',  //連接的Server端，command
                    data: "mode=method&method=" + "UserListEEP" + "&parameters=" + JQDate1 + "," + JQDate2,  //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入搜尋字串
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
            }

        }
     
        //---------------------------------------查詢結果匯出Excel---------------------------------------
        function AutoExcel() {
            //查詢條件
            var result = [];
            var JQDate1 = $("#sDate_Query").datebox("getValue");//datebox("getBindingValue");//datebox("getValue");                
            var JQDate2 = $("#eDate_Query").datebox("getValue");

            $.ajax({
                url: '../handler/JBHRISUseCaseHandler.ashx?' + $.param({ mode: 'CallServerMethodReturnFile', remoteName: 'sJBRecruit', method: 'UserListExcel' }),
                //data: { parameters: $.toJSONString(data) },

                data: "&parameters=" + JQDate1 + "," + JQDate2 ,

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

       
        
   </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sJBRecruit.UserList" runat="server" AutoApply="True"
                DataMember="UserList" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="履歷列表" AllowAdd="False" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="580px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="姓名	" Editor="text" FieldName="NameC" Format="" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="center" Caption="性別" Editor="text" FieldName="SexText" Format="" Visible="true" Width="75" />
                    <JQTools:JQGridColumn Alignment="center" Caption="出生年" Editor="text" FieldName="iBirthYear" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="MobileNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="103">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="大縣市" Editor="text" FieldName="Address_B1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="居住區域" Editor="text" FieldName="Address_B2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                       <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="AutoExcel" Text="匯出Excel" Visible="True" />
<%--                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />--%>
                   <%-- <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="履歷修改日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="sDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="～" Condition="=" DataType="datetime" Editor="datebox" FieldName="eDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="100" />
                </QueryColumns>
            </JQTools:JQDataGrid>

        </div>
    </form>
</body>
</html>
