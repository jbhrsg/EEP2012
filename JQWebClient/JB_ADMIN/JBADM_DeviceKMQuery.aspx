<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBADM_DeviceKMQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jquery.jbjob.js"></script>
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {

                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });

            });
        });
       function queryGrid(dg) {
            var UserID = getClientInfo("UserID");
            if ($(dg).attr('id') == 'dataGridView') {
                var StdDate = $("#StdDate_Query").datebox('getValue');
                var EndDate = $("#EndDate_Query").datebox('getValue');
                var DeviceItemsID = $("#DeviceItemsID_Query").combobox('getValue');
                GetDeviceKMRate(UserID,StdDate, EndDate, DeviceItemsID);
            }
       }
       function GetDeviceKMRate(UserID, StdDate, EndDate, DeviceItemsID) {
           $.ajax({
               type: "POST",
               url: '../handler/jqDataHandle.ashx?RemoteName=sDeviceKMQuery.DeviceKMQuery',
               data: "mode=method&method=" + "GetDeviceKMRate" + "&parameters="+ UserID + "," + StdDate + "," + EndDate + "," + DeviceItemsID,
               cache: false,
               async: false,
               success: function (data) {
                   $.messager.progress({ msg: '資料下載中...', interval: '100' });//進度條開始
                   if (data == "false") {
                       $("#dataGridView").datagrid('setWhere', "1=0");
                   }
                   else {
                       $("#dataGridView").datagrid('setWhere', "UserID='" + UserID + "'");
                   }
                   setTimeout(function () {
                       $.messager.progress('close'); //進度條結束
                   }, 1000);

               }
           }
        );
       }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sDeviceKMQuery.DeviceKMQuery" runat="server" AutoApply="True"
                DataMember="DeviceKMQuery" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="公務車里程分攤" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="650px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="公務車" Editor="numberbox" FieldName="DeviceItemsID" Format="" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公務車名稱" Editor="text" FieldName="DeviceName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenterID" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心名稱" Editor="text" FieldName="CostCenterName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="公里數" Editor="numberbox" FieldName="KMs" Format="N0" Width="80" Total="sum" />
                    <JQTools:JQGridColumn Alignment="right" Caption="里程分擔比率%" Editor="numberbox" FieldName="KMRate" Format="N2" Width="100" Total="sum" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportXlsx" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起迄日期" Condition="=" DataType="datetime" DefaultValue="_today" Editor="datebox" FieldName="StdDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="-" Condition="=" DataType="datetime" DefaultValue="_today" Editor="datebox" FieldName="EndDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公務車號" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'DeviceItemsID',textField:'DeviceItemsName',remoteName:'sDeviceKMQuery.DeviceItem',tableName:'DeviceItem',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="DeviceItemsID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="公務車里程分攤">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="DeviceKMQuery" HorizontalColumnsCount="2" RemoteName="sDeviceKMQuery.DeviceKMQuery" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公務車代號" Editor="numberbox" FieldName="DeviceItemsID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公務車名稱" Editor="text" FieldName="DeviceName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenterID" Format="" Width="180" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心名稱" Editor="text" FieldName="CostCenterName" Format="" Width="180" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公里數" Editor="numberbox" FieldName="KMs" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="里程分擔比率" Editor="text" FieldName="KMRate" Format="N2" Width="180" />
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
