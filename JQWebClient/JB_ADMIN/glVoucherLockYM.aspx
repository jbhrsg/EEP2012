<%@ Page Language="C#" AutoEventWireup="true" CodeFile="glVoucherLockYM.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/JBGL/label.css" rel="stylesheet" />
    <title></title>
        <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var flag = true; //定義一個全域變數，只有第一次執行
       
        //=========================================控制2個Grid1=0都做完才顯示Grid=============================================================
        var waitA = false;
        var waitB = false;

        var myVar = setInterval(function () { myTimer(); }, 100);
        setTimeout(function () { clearTimeout(myVar); }, 6000);//最多6秒結束

        function myTimer() {
            if (waitA == true && waitB == true) {
                OnSelectCompanyID();
                clearTimeout(myVar);
            }
        }
        //========================================= ready ====================================================================================
        $(document).ready(function () {
            var sDate = new Date();
            var vDate = new Date($.jbDateAdd('months', 1, sDate));
            var date1 = $.jbjob.Date.DateFormat(sDate, 'yyyyMMdd').substring(0, 6);
            $("#JQTextBox1").val(date1);//重算年月
            //寬度調整
            //$("#JQTextBox1").text('resize', '105');

        });
        
        function OnSelectCompanyID() {
            RefreshGrid2();
            RefreshGrid3();
        }
        //========================================= 傳回登入者目前設定的公司別 ====================================================================================

        var sCompanyID = "";

        $(document).ready(function () {
            //傳回登入者目前設定的公司別
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherMaster.glVoucherMaster', //連接的Server端，command
                data: "mode=method&method=" + "getglVoucherSet" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        //設定公司別
                        $('#cbCompanyID').combobox('setValue', rows[0].CompanyID);
                    }
                }
            });
        });
        //=================================================================鎖檔==============================================================

        //---------------------------------------OnLoadSuccessGrid---------------------------------------
        function OnLoadSuccessDG2() {
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                //1=0做完
                waitA = true;
            }
        }

        //---------------------------------------更新Grid---------------------------------------
        function RefreshGrid2() {

            $('#dataGridView2').datagrid('loadData', []); //明細清空資料            
            var result = [];

            //公司別
            var CompanyID = $("#cbCompanyID").combobox('getValue');
            if (CompanyID != '') result.push("CompanyID= " + CompanyID);

            $("#dataGridView2").datagrid('setWhere', result.join(' and '));

        }
         //---------------------------------------呼叫Method---------------------------------------
         var GetDataFromMethod = function (methodName, data) {
             var returnValue = "";
             $.ajax({
                 url: '../handler/JqDataHandle.ashx?RemoteName=sglVoucherSet',
                 data: { mode: 'method', method: methodName, parameters: $.toJSONString(data) },
                 type: 'POST',
                 async: false,
                 success: function (data) { returnValue = $.parseJSON(data); },
                 error: function (xhr, ajaxOptions, thrownError) { returnValue = null; }
             });
             return returnValue;
         };

        //得到新增時的預設值
        //TypeID 1=> 鎖檔年月 ,2 => 年轉
         function GetLockYM() {
             //公司別
             var CompanyID = $("#cbCompanyID").combobox('getValue');
             return GetDataFromMethod('GetLockYM', { Company_ID: CompanyID, TypeID: 1 });
         }

         function GetCompanyID() {
             return $("#cbCompanyID").combobox('getValue');
         }
        
        //Grid checkbox顯示         
         function genCheckBox(val) {
             if (val != "0")
                 return "<input  type='checkbox' checked='true' onclick='return false;'/>";
             else
                 return "<input  type='checkbox' onclick='return false;'/>";
         }
         function OpeninsertLockYM() {
             //檢查之前資料不可為失效
             var CompanyID = $("#cbCompanyID").combobox('getValue');
             var cnt = "";
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherSet.glVoucherLockYM', //連接的Server端，command
                 data: "mode=method&method=" + "CheckVoucherLockIsActive" + "&parameters=" + CompanyID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     cnt = $.parseJSON(data);
                 }
             });
             if ((cnt == "0")) {
                 openForm('#JQDialog1', {}, "inserted", 'dialog');
             } else {
                 alert("有失效的鎖檔年月！");
                 return false;
             }
         }

        //刪除按鈕控制=>1.檢查最後一筆,2.修改失效
         function OnUpdateDG2(datarow) {
             //false則不檢查
             if (datarow.IsActive == true) {
                 //公司別
                 var CompanyID = $("#cbCompanyID").combobox('getValue');
                 var LockYM = $("#dataGridView2").datagrid('getSelected').LockYM;;//取得當前主檔中選中的那個Data
                 var MaxLockYM;
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherSet.glVoucherLockYM', //連接的Server端，command
                     data: "mode=method&method=" + "CheckVoucherLockYM" + "&parameters=" + CompanyID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                     cache: false,
                     async: false,
                     success: function (data) {
                         MaxLockYM = data;
                     }
                 });

                 if ((MaxLockYM == LockYM)) {
                     var pre = confirm("確定失效?");
                     if (pre == true) {

                         //失效
                         $.ajax({
                             type: "POST",
                             url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherSet.glVoucherLockYM', //連接的Server端，command
                             data: "mode=method&method=" + "UpdateVoucherLockYM" + "&parameters=" + CompanyID + "," + LockYM, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                             cache: false,
                             async: false,
                             success: function (data) {
                                 RefreshGrid2();
                             }
                         });
                     }
                     return false;
                 }
                 else {

                     alert('最後一筆資料才可失效!!');
                     return false;
                 }

             }
         }
         function OnInsertedDG2() {             
             $("#dataGridView2").datagrid("reload");            
         }
        //存檔後
         function OnAppliedDF2() {
             $("#dataGridView2").datagrid("reload");
         }

        //存檔前檢查
         function OnApplyDF2() {
            //傳票日期需是鎖檔年月之 下月  
            var VoucherDate = $("#dataFormMaster2VoucherDate").datebox('getValue');
            var aDate = new Date(VoucherDate);//傳票日期 月份-1
            var MM = (aDate.getMonth() + 1 <= 10 ? '0' : '') + (aDate.getMonth());
            var YM = "";

            if (MM == "00") {//12月
                YM = (aDate.getFullYear()-1).toString() + "12";
            } else {
                YM = aDate.getFullYear().toString() + MM;
            }
            var LockYM = $("#dataFormMaster2LockYM").val();
           
            if (LockYM != YM) {
                alert('傳票日期有誤!');
                return false;
            }  


             //鎖檔年月,月為01時=>檢查去年度是否已年轉
             var sM = $('#dataFormMaster2LockYM').val().substr(4,2);
             if (sM == "01") {
                 var sYM = $('#dataFormMaster2LockYM').val();
                 //公司別
                 var CompanyID = $("#cbCompanyID").combobox('getValue');

                 var cnt="";
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherSet.glVoucherLockYM', //連接的Server端，command
                     data: "mode=method&method=" + "CheckAddglVoucherLockYM" + "&parameters=" + sYM + "," + CompanyID,
                     cache: false,
                     async: false,
                     success: function (data) {
                             cnt = $.parseJSON(data);
                     }
                 });
                 if ((cnt == "0")) {
                    alert("去年度年轉尚未成功。");
                    return false;
                 }
             }

         }


        //=================================================================年轉==============================================================

         //---------------------------------------OnLoadSuccess---------------------------------------
         function OnLoadSuccessDG3() {
             if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                 //1=0做完
                 waitB = true;
             }
         }
         //---------------------------------------更新Grid---------------------------------------
         function RefreshGrid3() {

             //$('#dataGridView3').datagrid('loadData', []); //明細清空資料            
             var result = [];

             //公司別
             var CompanyID = $("#cbCompanyID").combobox('getValue');
             if (CompanyID != '') result.push("CompanyID= " + CompanyID);

             $("#dataGridView3").datagrid('setWhere', result.join(' and '));

         }
        //得到預設值
         //TypeID 1=> 鎖檔年月 ,2 => 年轉
         function GetConvertYear() {
             //年轉年度  =>   抓取鎖檔年月中最大的月份 (需為12)
             //公司別
             var CompanyID = $("#cbCompanyID").combobox('getValue');
             return GetDataFromMethod('GetLockYM', { Company_ID: CompanyID, TypeID: 2 });
         }

         function OnApplyDF3() {
             var pre = confirm("確認年轉?");
             if (pre != true) {
                 return false;
             }
         }
         
        //重算glBanalce        
         function ComputeBanalce() {
             var CompanyID = $("#cbCompanyID").combobox('getValue');
             var YearMonth = $("#JQTextBox1").val();

             var pre = confirm("確認重算?");
             if (pre == true) {
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherSet.glVoucherLockYM', //連接的Server端，command
                     data: "mode=method&method=" + "procInsertglBalanceRepeat" + "&parameters=" + CompanyID + "," + YearMonth, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                     cache: false,
                     async: false,
                     success: function (data) {
                         alert("重算完成。");
                     }
                 });
             }
         }

     </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <h5 id="CompanyID" class="h3_Caption">公司別</h5>
                    </td>
                    <td>
                <JQTools:JQComboBox ID="cbCompanyID" runat="server" DisplayMember="CompanyName" Font-Size="Small" RemoteName="sglVoucherSet.glCompany" ValueMember="CompanyID" OnSelect="OnSelectCompanyID" SelectOnly="True">
                </JQTools:JQComboBox>
                        </td>
                </tr>
             </table>
        </div>
        <div>
           <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />            


            <div id="tt" class="easyui-tabs" style="width:auto;height:auto;">
                <div title="鎖檔" style="padding:20px;">
                        重算年月<JQTools:JQTextBox ID="JQTextBox1" runat="server" Width="100px" />
                        <JQTools:JQButton ID="JQButton1" runat="server" Text="重算" OnClick="ComputeBanalce()"/>
                        <br />
                        <br />
                        <JQTools:JQDataGrid ID="dataGridView2" data-options="pagination:true,view:commandview" RemoteName="sglVoucherSet.glVoucherLockYM" runat="server" AutoApply="True"
                        DataMember="glVoucherLockYM" Pagination="False" QueryTitle="" EditDialogID=""
                        Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="False" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="515px" Height="410px" OnLoadSuccess="OnLoadSuccessDG2" OnInserted="OnInsertedDG2" OnUpdate="OnUpdateDG2">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="center" Caption="鎖檔年月" Editor="text" FieldName="LockYM" Format="" Visible="True" Width="110" EditorOptions="" ReadOnly="True" FormatScript="" />
                            <JQTools:JQGridColumn Alignment="center" Caption="傳票日期" Editor="datebox" FieldName="VoucherDate" Format="yyyy-mm-dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="100">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="失效?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="設定人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="100" ReadOnly="True" />
                            <JQTools:JQGridColumn Alignment="center" Caption="設定日期" Editor="datebox" FieldName="CreateDate" Format="yyyy-mm-dd HH:MM" Visible="true" Width="130" ReadOnly="True" />
                            <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <TooItems>
                             <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                                        OnClick="OpeninsertLockYM" Text="新增鎖檔年月" />  
                            <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                            <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />  
                        </TooItems>
                    </JQTools:JQDataGrid>
       <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster2" Title="鎖檔年月維護" EditMode="Dialog" DialogLeft="100px" DialogTop="80px" Width="330px">

                <JQTools:JQDataForm ID="dataFormMaster2" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="glVoucherLockYM" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sglVoucherSet.glVoucherLockYM" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApply="OnApplyDF2" OnApplied="OnAppliedDF2" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="鎖檔年月" Editor="text" FieldName="LockYM" Format="" Width="100" ReadOnly="True" NewRow="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票日期" Editor="datebox" FieldName="VoucherDate" MaxLength="0" NewRow="True" ReadOnly="False" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="設定日期" Editor="datebox" EditorOptions="" FieldName="CreateDate" Format="" Width="130" Visible="False" NewRow="False" ReadOnly="False" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="設定人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="False" Width="80" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" NewRow="False" ReadOnly="False" Visible="False" Width="80" MaxLength="0" RowSpan="1" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>

                        <JQTools:JQDefault ID="defaultMaster2" runat="server" BindingObjectID="dataFormMaster2" EnableTheming="True">
                        <Columns>
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="VoucherDate" RemoteMethod="True" />
                           <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" RemoteMethod="True" DefaultValue="_usercode" FieldName="UserID" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetLockYM" FieldName="LockYM" RemoteMethod="False" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCompanyID" FieldName="CompanyID" RemoteMethod="False" />
                        </Columns>
                    </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster2" runat="server" BindingObjectID="dataFormMaster2" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="LockYM" RemoteMethod="True" ValidateMessage="鎖檔年月有誤 ！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="VoucherDate" RemoteMethod="True" ValidateMessage="傳票日期不可空白 ！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
           </JQTools:JQDialog>
                </div>
                <div title="年轉" data-options="closable:true" style="overflow:auto;padding:20px;">
                        <JQTools:JQDataGrid ID="dataGridView3" data-options="pagination:true,view:commandview" RemoteName="sglVoucherSet.glVoucherConvertYear" runat="server" AutoApply="True"
                        DataMember="glVoucherConvertYear" Pagination="False" QueryTitle="Query" EditDialogID="JQDialog2"
                        Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="False" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="450px" OnLoadSuccess="OnLoadSuccessDG3" Height="410px">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="center" Caption="年轉年份" Editor="text" FieldName="ConvertYear" Format="" Visible="True" Width="160" EditorOptions="" ReadOnly="True" />
                            <JQTools:JQGridColumn Alignment="center" Caption="設定人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" ReadOnly="True" />
                            <JQTools:JQGridColumn Alignment="center" Caption="設定日期" Editor="datebox" FieldName="CreateDate" Format="yyyy-mm-dd HH:MM" Visible="true" Width="130" ReadOnly="True" />
                            <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <TooItems>
                             <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                                        OnClick="insertItem" Text="新增年轉年份 " />                              
                        </TooItems>
                        <QueryColumns>
                        </QueryColumns>
                    </JQTools:JQDataGrid>
                    <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormMaster3" Title="年轉年份維護" EditMode="Dialog" DialogLeft="100px" DialogTop="80px" Width="330px">
                                        <JQTools:JQDataForm ID="dataFormMaster3" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="glVoucherConvertYear" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sglVoucherSet.glVoucherConvertYear" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApply="OnApplyDF3" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="年轉年份" Editor="text" FieldName="ConvertYear" Format="" Width="100" ReadOnly="True" MaxLength="0" Visible="True" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="設定日期" Editor="datebox" EditorOptions="" FieldName="CreateDate" Format="" Width="130" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="設定人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" NewRow="False" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" MaxLength="0" ReadOnly="False" Width="80" Visible="False" />
                    </Columns>
                        </JQTools:JQDataForm>
                                <JQTools:JQDefault ID="defaultMaster3" runat="server" BindingObjectID="dataFormMaster3" EnableTheming="True">
                                <Columns>
                                   <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" RemoteMethod="True" DefaultValue="_usercode" FieldName="UserID" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetConvertYear" FieldName="ConvertYear" RemoteMethod="False" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCompanyID" FieldName="CompanyID" RemoteMethod="False" />
                                </Columns>
                            </JQTools:JQDefault>
                        <JQTools:JQValidate ID="validateMaster3" runat="server" BindingObjectID="dataFormMaster3" EnableTheming="True">
                            <Columns>
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="ConvertYear" RemoteMethod="True" ValidateMessage="年轉年份有誤 ！" ValidateType="None" />
                            </Columns>
                        </JQTools:JQValidate>
                    </JQTools:JQDialog>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
