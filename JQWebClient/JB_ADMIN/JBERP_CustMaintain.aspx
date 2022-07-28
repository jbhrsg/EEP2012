<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_CustMaintain.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        ///=============================================  ready  ===============================================================================================
        $(document).ready(function () {
            //日期寬度調整
            $('#JQLastUpdateDate1').datebox({
                width: 90
            });
            $('#JQLastUpdateDate2').datebox({
                width: 90
            });
            $('#JQLatelyDayD1').datebox({
                width: 90
            });
            $('#JQLatelyDayD2').datebox({
                width: 90
            });

            //維護日期預設
            var dt = new Date();
            var aDate = new Date($.jbDateAdd('days', -7, dt));//開始日期
            var aDate2 = new Date($.jbDateAdd('days', 0, dt));//結束日期

            $("#JQLastUpdateDate1").datebox('setValue', $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd'));
            $("#JQLastUpdateDate2").datebox('setValue', $.jbjob.Date.DateFormat(aDate2, 'yyyy/MM/dd'));

            $('<a>', { id: 'BT_FIELD', class: 'easyui-linkbutton', 'data-options': "iconCls:'icon-search',plain:true" }).appendTo($('#FIELD_NAME_Query').closest("td")).linkbutton();

            // 建立 dialog
            initQueryDialog();

            //open 顯示欄位 dialog --使用 jQuery 绑定 easyui-linkbutton
            $(function () {
                $('#BT_FIELD').bind('click', function () {
                    $("#Dialog_Query").dialog("open");
                });
            });
            
            //Grid隱藏


            $("#btnReport").click(function () {               
                flag = true;

                if (flag) {
                    var WhereString = "";
                    var SalesID = $("#JQSalesID").combobox('getValue');//業務人員
                    var LastUpdateDate1 = $("#JQLastUpdateDate1").datebox("getValue"); //維護日期
                    var LastUpdateDate2 = $("#JQLastUpdateDate2").datebox("getValue"); 
                    var CustAddr = $("#JQCustAddr").val();//地址關鍵字
                    var LatelyDayD1 = $("#JQLatelyDayD1").datebox("getValue"); //交易日期
                    var LatelyDayD2 = $("#JQLatelyDayD2").datebox("getValue");
                    var SalesID2 = $("#JQSalesID2").combobox('getValue');//最後登入者
                    var PostType = $("#JQPostType").combobox('getValue');//客戶等級

                    //顯示欄位                   
                    var data = $("#DG_Query").datagrid("getData");
                    var sFIELD = "";
                    for (var i = 0; i < data.rows.length; i++) {
                        if (data.rows[i].IS_SELECTED == "Y")
                        {
                            sFIELD = sFIELD + "1";
                        }else sFIELD = sFIELD + "0";                        
                    }
                                    
                    var url = "../JB_ADMIN/REPORT/Media/ERPCustMaintainView.aspx?SalesID=" + SalesID + "&LastUpdateDate1=" + LastUpdateDate1 + "&LastUpdateDate2=" + LastUpdateDate2 +
                        "&CustAddr=" + CustAddr + "&LatelyDayD1=" + LatelyDayD1 + "&LatelyDayD2=" + LatelyDayD2 + "&SalesID2=" + SalesID2 + "&PostType=" + PostType + "&sFIELD=" + sFIELD;

                    var height = $(window).height() - 40;
                    var height2 = $(window).height() - 80;
                    var width = $(window).width() - 60;
                    var dialog = $('<div/>')
                    .dialog({
                        draggable: false,
                        modal: true,
                        height: height,
                        //top:0,
                        width: width,
                        title: "客戶資料列表",
                        //maximizable: true                              
                    });
                    $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="' + height2 + '"></iframe>').appendTo(dialog.find('.panel-body'));
                    dialog.dialog('open');
                }
            });



        });
        function OnLoadSuccessGV() {
            $('#dataGridMaster').datagrid('getPanel').hide();

        }



        // 建立 dialog
        function initQueryDialog() {
            $("#Dialog_Query").dialog(
            {
                height: 400,
                width: 400,
                resizable: false,
                modal: true,
                title: "選項",
                closed: true,
                buttons: [{
                    text: '取消',
                    handler: function () { $("#Dialog_Query").dialog("close") }
                },
                {
                    text: "確認",
                    handler: function () {
                        var deptName = "";
                        var deptRows = $("#DG_Query").datagrid("getRows");
                        var checkedItems = $('#DG_Query').datagrid('getChecked');
                        var flag;

                        for (var k = 0; k < deptRows.length; k++) {
                            //判斷有勾選的 update 為 "Y"
                            flag = "N"
                            $.each(checkedItems, function (index, item) {
                                if (deptRows[k].FIELD_NAME == item.FIELD_NAME) {
                                    deptRows[k].IS_SELECTED = "Y";
                                    flag = "Y";
                                    deptName = deptName + deptRows[k].CAPTION + ",";
                                }
                            });
                            if (flag != "Y")
                                deptRows[k].IS_SELECTED = "N";
                        }

                        $("#FIELD_NAME_Query").val(deptName);
                        $("#Dialog_Query").dialog("close");
                    }
                }]
            });
        };


     </script> 

    <style type="text/css">
.h3_Caption
{
    width: 80px;
    height: 20px;
    text-align: right;
    display: table-cell;
    vertical-align: middle;
    padding: 0px;
    margin: 0px;
}

    .div_RelativeLayout
{
    position:relative;
    margin:0px;
    padding:0px;
    top: 0px;
    left: 0px;
    font-size: 16px;
}
.Btn_Decide
{
width: 70px;
height: 20px;
margin: 0px;
padding: 0px;
display: block;
text-align: center;
border: 1px solid #444;
cursor: pointer;
border-radius: 5px;
background: #fcf883;
background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iI2ZjZjg4MyIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjM4JSIgc3RvcC1jb2xvcj0iI2YyZWJiMyIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjY0JSIgc3RvcC1jb2xvcj0iI2YyZWJhYiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiNmN2YwOGMiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
background: -moz-linear-gradient(top, #fcf883 0%, #f2ebb3 38%, #f2ebab 64%, #f7f08c 100%);
background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#fcf883), color-stop(38%,#f2ebb3), color-stop(64%,#f2ebab), color-stop(100%,#f7f08c));
background: -webkit-linear-gradient(top, #fcf883 0%,#f2ebb3 38%,#f2ebab 64%,#f7f08c 100%);
background: -o-linear-gradient(top, #fcf883 0%,#f2ebb3 38%,#f2ebab 64%,#f7f08c 100%);
background: -ms-linear-gradient(top, #fcf883 0%,#f2ebb3 38%,#f2ebab 64%,#f7f08c 100%);
background: linear-gradient(to bottom, #fcf883 0%,#f2ebb3 38%,#f2ebab 64%,#f7f08c 100%);
filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#fcf883', endColorstr='#f7f08c',GradientType=0 );
}
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <div id="Div_Area">
                <table>
                    <tr>
                        <td>
                            <h5 id="companyCode" class="h3_Caption">業務人員</h5>
                        </td>
                        <td>
                            <JQTools:JQComboBox ID="JQSalesID" runat="server" CheckData="True" DisplayMember="SalesName" PanelHeight="150" RemoteName="sERPCustMaintain.infoSalesMan" ValueMember="SalesID" >
                            </JQTools:JQComboBox>
                        </td>
                        <td>
                            <h5 id="companyCode0" class="h3_Caption">維護日期</h5>
                        </td>
                        <td>
                            <JQTools:JQDateBox ID="JQLastUpdateDate1" runat="server" Format="DateTime" ShowSeconds="False" Width="120px" />
            〜 <JQTools:JQDateBox ID="JQLastUpdateDate2" runat="server" Format="DateTime" ShowSeconds="False" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h5 id="employeeCode" class="h3_Caption">地址關鍵字</h5>
                        </td>
                        <td>
                            <input id="JQCustAddr" type="text" /> </td>
                        <td>
                            <h5 id="companyCode1" class="h3_Caption">交易日期</h5>
                        </td>
                        <td>
                            <JQTools:JQDateBox ID="JQLatelyDayD1" runat="server" Format="DateTime" ShowSeconds="False" Width="200px" />
            〜 <JQTools:JQDateBox ID="JQLatelyDayD2" runat="server" Format="DateTime" ShowSeconds="False" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h5 id="employeeName" class="h3_Caption">最後登入者</h5>
                        </td>
                        <td>
                            <JQTools:JQComboBox ID="JQSalesID2" runat="server" CheckData="True" DisplayMember="USERNAME" PanelHeight="150" RemoteName="sERPCustMaintain.infoUserID" ValueMember="USERID">
                            </JQTools:JQComboBox>
                        </td>
                        <td>
                            <h5 id="employeeName0" class="h3_Caption">客戶等級</h5>
                        </td>
                        <td>
                            <JQTools:JQComboBox ID="JQPostType" runat="server" CheckData="True" DisplayMember="ListContent" PanelHeight="150" RemoteName="sERPCustMaintain.infoPostType" ValueMember="ListID">
                            </JQTools:JQComboBox>
                        </td>
                    </tr>
                    <%--</table>
                    <table>--%>
                    <tr>
                        <td>
                            <h5 id="employeeName1" class="h3_Caption">顯示內容</h5>
                        </td>
                        <td colspan="3">
                            <JQTools:JQTextArea ID="FIELD_NAME_Query" runat="server" Height="60px" Width="400px" />
                        </td>
                        <td>
            <h5 id="btnReport" class="Btn_Decide">報表列印</h5>

                        </td>
                    </tr>
                    </table>
            </div>

        <!-- Column dialog對話框內容的 DIV -->
        <div id="Dialog_Query">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="DG_Query" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="infoCustomerColumn" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" NotInitGrid="False" OnLoadSuccess="OnLoadSuccessGV" PageList="10,20,30,40,50" PageSize="50" Pagination="False" ParentObjectID="" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPCustMaintain.infoCustomerColumn" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="360px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="欄位代碼" Editor="text" FieldName="FIELD_NAME" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90" />
                        <JQTools:JQGridColumn Alignment="left" Caption="欄位名稱" Editor="text" FieldName="CAPTION" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="150" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERPCustMaintain.ERPCustMaintain" runat="server" AutoApply="True"
                DataMember="ERPCustMaintain" Pagination="True" QueryTitle="Query"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="CustNO" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustName" Editor="text" FieldName="CustName" Format="" MaxLength="0" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                    <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
    <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="defaultMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="validateMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQValidate>
</form>
</body>
</html>
