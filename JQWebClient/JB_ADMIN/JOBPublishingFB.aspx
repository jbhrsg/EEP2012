<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JOBPublishingFB.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        ///=============================================  ready  ===============================================================================================
        $(document).ready(function () {            
            $('#IsMark_Query').options('setValue', 0);
        });

        //標示顯示       
        function ScriptIsMark(val, rowData) {
            if (val == "1") {
                return "<div style='font-weight:bold;color:red;'>" + rowData.sMarkDate + "</div>";
            } else {
                return "";
            }
        }
     

        function queryGrid(dg) {//查詢後添加固定條件 
            if ($(dg).attr('id') == 'dataGridMaster') {
                //查詢條件
                var result = [];
              
                var IsMark = $('#IsMark_Query').options('getCheckedValue');
                var MarkDate = $('#MarkDate_Query').datebox('getValue');
                var cCode = $('#cCode_Query').val();
                var siAutoKey = "";
                var dataGrid = $('#dataGridMaster');
                var Type = 1;

                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sJOB0800.PublishingList', //連接的Server端，command
                    data: "mode=method&method=" + "ProcessPublishingJobFB" + "&parameters=" + IsMark + "," + MarkDate + "," + cCode + "," + siAutoKey + "," + Type, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
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
                     
        function AddMark() {
            var bIsMark = $('#IsMark_Query').options('getCheckedValue');
            if (bIsMark=="") {
                alert('不拘無法標示。');
                return true;
            }
            if ($("#dataGridMaster").datagrid('getChecked').length == 0) {
                alert('請勾選項目。');
                return true;
            }else {
                var ConfirmMark = confirm("確定要標記?");
                if (ConfirmMark == false) {
                    return true;
                }
                var IsMark = bIsMark;
                var MarkDate = $('#MarkDate_Query').datebox('getValue');
                var cCode = "";
                var rows = $('#dataGridMaster').datagrid("getChecked");
                var iAutoKeyStr = [];//JobID群
                for (var i = 0; i < rows.length; i++) {
                    iAutoKeyStr.push(rows[i].iAutoKey);
                }
                var siAutoKey = iAutoKeyStr.join('*');
                var Type = 3;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sJOB0800.PublishingList', //連接的Server端，command
                    data: "mode=method&method=" + "ProcessPublishingJobFB" + "&parameters=" + IsMark + "," + MarkDate + "," + cCode + "," + siAutoKey + "," + Type, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        queryGrid($('#dataGridMaster'));
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }

                });
            }
        }
        
        //開啟自訂Excel 匯出
        function AutoExcel() {
            if ($("#dataGridMaster").datagrid('getChecked').length == 0) {
                alert('請勾選項目。');
            } else {
                //得出grid data
                var IsMark = $('#IsMark_Query').options('getCheckedValue');
                var rows = $('#dataGridMaster').datagrid("getChecked");
                var iAutoKeyStr = [];//JobID群
                for (var i = 0; i < rows.length; i++) {
                    iAutoKeyStr.push(rows[i].iAutoKey);
                }
                var siAutoKey = iAutoKeyStr.join('*');

                var dt = new Date();
                var sDate = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')


                $.ajax({
                    url: '../handler/JBHRISUseCaseHandler.ashx?' + $.param({ mode: 'CallServerMethodReturnFile', remoteName: 'sJOB0800', method: 'PublishingJobFBExcel' }),
                    //data: { parameters: $.toJSONString(data) },

                    data: "&parameters=" + IsMark + "," + siAutoKey,

                    type: 'POST',
                    async: true,
                    success: function (data) {
                        //Json.FileName
                        var Json = $.parseJSON(data);
                        if (Json.IsOK) {
                            var Url = $('<a>', {
                                href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: sDate + 'fb資訊.xls', FilePathName: Json.FileStreamOrFileName }),
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
        }
        //報表---1查詢,2匯出Excel,3標記
        function OpenPublishing() {
            if ($("#dataGridMaster").datagrid('getChecked').length == 0) {
                alert('請勾選項目。');
            } else {
                var IsMark = $('#IsMark_Query').options('getCheckedValue');
                var rows = $('#dataGridMaster').datagrid("getChecked");
                var iAutoKeyStr = [];//JobID群
                for (var i = 0; i < rows.length; i++) {
                    iAutoKeyStr.push(rows[i].iAutoKey);
                }
                var siAutoKey = iAutoKeyStr.join('*');

                var dt = new Date();
                var sDate = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')

                var url = "../JB_ADMIN/REPORT/0800/JobFBReport.aspx?IsMark=" + IsMark + "&siAutoKey=" + siAutoKey + "&sDate=" + sDate;

                var height = $(window).height() - 20;
                var height2 = $(window).height() - 90;
                var width = $(window).width() - 230;
                var dialog = $('<div/>')
                .dialog({
                    draggable: false,
                    modal: true,
                    height: height,
                    //top:0,
                    width: width,
                    title: "fb職缺",
                    //maximizable: true                              
                });
                $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="98%"></iframe>').appendTo(dialog.find('.panel-body'));
                dialog.dialog('open');
            }
        }




     </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />

            <div title="資訊分析" style="padding:10px">
                <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sJOB0800.cmdPublishingJob" runat="server" AutoApply="False"
                DataMember="cmdPublishingJob" Pagination="True" QueryTitle=""
                Title="" QueryMode="Panel" UpdateCommandVisible="False" ViewCommandVisible="False" DeleteCommandVisible="False" AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="50,100,150,200,250" PageSize="50" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" Width="1100px" Height="400px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="iAutoKey" Editor="text" FieldName="iAutoKey" Format="" Width="100" Total="" FormatScript="" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="剩餘天數" Editor="numberbox" FieldName="iday" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代碼" Editor="text" FieldName="cCode" Format="" MaxLength="0" Width="110" Sortable="False" />
                     <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="cName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="95">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺資訊" Editor="text" FieldName="sJobName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="200" FormatScript="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="工作地址" Editor="text" FieldName="Address_1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="160">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="職務說明" Editor="textarea" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="330" FormatScript="">
                    </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="center" Caption="標記" Editor="text" FieldName="IsMark" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" FormatScript="ScriptIsMark" EditorOptions="" >
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
<%--                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="AutoExcel" Text="匯出Excel" Visible="True" />--%>
                    <JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton"   OnClick="AddMark" Text="標示" />
                    <JQTools:JQToolItem ID="JQToolItem2" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="OpenPublishing" Text="匯出"  />    
                </TooItems>  
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Condition="%" DataType="string" Editor="infooptions" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" Caption="狀態" FieldName="IsMark" EditorOptions="title:'JQOptions',panelWidth:180,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'未標記',value:'0'},{text:'已標記',value:'1'},{text:'不拘',value:''}]" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="標記日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="MarkDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="95" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="   代碼或地址" Condition="%" DataType="string" Editor="text" FieldName="cCode" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="150" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
