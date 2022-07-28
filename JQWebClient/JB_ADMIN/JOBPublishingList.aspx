<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JOBPublishingList.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        ///=============================================  ready  ===============================================================================================
        $(document).ready(function () {
            //初始化查詢條件
            var dt = new Date();
            var FirstDate = new Date($.jbGetFirstDate(dt));                          
            var LastDate = new Date($.jbGetLastDate(dt));          
            $("#SDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd'));//開始日期
            $("#EDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd'));//結束日期
           
        });
        
        function queryGrid(dg) {//查詢後添加固定條件 
            //出刊資訊          
            if ($(dg).attr('id') == 'dataGridMaster') {
                //查詢條件
                var result = [];
              
                var SDate = $("#SDate_Query").datebox('getValue');              
                var EDate = $("#EDate_Query").datebox('getValue');

                //if (SDate != '') result.push("Convert(nvarchar(10),StartDate,111) <= '" + EDate + "'");
                //if (EDate != '') result.push("Convert(nvarchar(10),EndTime,111) >= '" + SDate + "'");
                //$(dg).datagrid('setWhere', result.join(' and '));

                var dataGrid = $('#dataGridMaster');
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sJOB0800.PublishingList', //連接的Server端，command
                    data: "mode=method&method=" + "GePublishingList" + "&parameters=" + SDate + "," + EDate , //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示                            

                        if (rows.length > 10) {
                            //通過loadData方法清除掉原有Grid中的舊有資料並填補分頁資料
                            $('#dataGridMaster').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                        } else {
                            $('#dataGridMaster').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                        }
                    }
                });
            }
           
        }
       
        //Grid開啟連結       
        function OpenPublishingUrl(val, row) {            
            return $('<a>', { href: '#', onclick: 'window.open("' + val + '","aa");', theData: row.UserID }).linkbutton({ text: "<b><div style=\"color:Blue\">" + val + "</div></b>", plain: true })[0].outerHTML;
        }

        //開啟自訂Excel 匯出
        function AutoExcel() {
            //得出grid data
            var SDate = $("#SDate_Query").datebox('getValue');
            var EDate = $("#EDate_Query").datebox('getValue');         

            $.ajax({
                url: '../handler/JBHRISUseCaseHandler.ashx?' + $.param({ mode: 'CallServerMethodReturnFile', remoteName: 'sJOB0800', method: 'PublishingListAutoExcel' }),
                //data: { parameters: $.toJSONString(data) },

                data: "&parameters=" + SDate + "," + EDate ,

                type: 'POST',
                async: true,
                success: function (data) {
                    //Json.FileName
                    var Json = $.parseJSON(data);
                    if (Json.IsOK) {
                        var Url = $('<a>', {
                            href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: '刊登資訊.xls', FilePathName: Json.FileStreamOrFileName }),
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

            <div title="點擊統計" style="padding:10px">
                <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sJOB0800.infoPublishingList" runat="server" AutoApply="False"
                DataMember="infoPublishingList" Pagination="True" QueryTitle=""
                Title="" QueryMode="Panel" UpdateCommandVisible="False" ViewCommandVisible="False" DeleteCommandVisible="False" AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" Width="90%">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="廣告名稱" Editor="text" FieldName="DisplayName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="Tel_1" Format="" Width="120" Total="" FormatScript="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Address_1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="區域" Editor="text" FieldName="sTown" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="sJobTitle" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="230">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="網址" Editor="text" FieldName="PublishingUrl" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="300" FormatScript="OpenPublishingUrl">
                    </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="center" Caption="起始日期" Editor="text" FieldName="StartDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="center" Caption="狀態" Editor="text" FieldName="sStatus" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" >
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="AutoExcel" Text="匯出Excel" Visible="True" />
                </TooItems>  
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Condition="=" DataType="datetime" Editor="datebox" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" Caption="起始日期" FieldName="SDate" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="結束日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="EDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
