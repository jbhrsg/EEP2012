<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBCON_ACTIVITY.aspx.cs" Inherits="Template_JQuerySingle1" %>
<%@ Register assembly="Srvtools, Version=5.0.0.0, Culture=neutral, PublicKeyToken=8763076c188bfb12" namespace="Srvtools" tagprefix="cc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
      <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var UserID = getClientInfo("UserID");
        $(document).ready(function () {
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
        });
        var HEADCOUNT_FormatScript = function (value, row, index) {
            if (value > 1) {
                var fieldName = this.field;
                return $('<a>', { href: 'javascript: void(0)', title: '', onclick: "HEADCOUNT_CommandTrigger.call(this,'')" }).linkbutton({ iconCls: '', plain: false, text: value })[0].outerHTML;
            }
        }
        var HEADCOUNT_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
            var rowData = $("#JQDataGrid1").datagrid('selectRow', rowIndex).datagrid('getSelected');
            var FiltStr = "CONTACT_ID = " + "'" + rowData.CONTACT_ID + "'";
            $("#JQDataGrid2").datagrid('setWhere', FiltStr);
            openForm('#JQDialog3', {}, "", 'dialog');
            return true;
        }
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        function dataGridViewOnDelete() {
            var flag = 0;
            var rowData = $("#dataGridView").datagrid('getSelected');
            var AUTOKEY = rowData.AUTOKEY;
            var ConfirmYN = confirm("注意!!確定要刪除參加人員:(" + rowData.CONTACT_NAME + ')?');
            if (ConfirmYN == false) {
                return false;
            }
        }
        function DeleteActivityContact() {
            var flag = 0;
            var rowData = $("#JQDataGrid1").datagrid('getSelected');
            var AUTOKEY = rowData.AUTOKEY;
            var ConfirmYN = confirm("注意!!確定要刪除參加人員:(" + rowData.CONTACT_NAME + ')?');
            if (ConfirmYN == false) {
                return false;
            }
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCON_ACTIVITYMASTER.CON_ACTIVITYMASTER', //連接的Server端，command
                data: "mode=method&method=" + "DeleteActivityContact" + " &parameters=" + AUTOKEY + "," + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data == 'True') {
                        flag = 1;
                    }
                    else {
                        alert("注意!! 刪除參加人員失敗");
                    }
                }
            });
            $('#JQDataGrid1').datagrid("reload");
            return flag;
        }
        function dataGridViewOnSelect(rowindex, rowdata) {
            var ACTIVITY_NAME = rowdata.ACTIVITY_NAME + '  活動名單';
            $("#JQDataGrid1").datagrid('getPanel').panel('setTitle', ACTIVITY_NAME);
            $("#JQDataGrid1").datagrid('options').title = ACTIVITY_NAME;
            if (rowdata != null && rowdata != undefined) {
                var ACTIVITY_ID = rowdata.ACTIVITY_ID;
                var FiltStr = "ACTIVITY_ID = " + "'" + ACTIVITY_ID + "'";
                $("#JQDataGrid1").datagrid('setWhere', FiltStr);
            }
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCON_ACTIVITYMASTER.CON_ACTIVITYMASTER" runat="server" AutoApply="True"
                DataMember="CON_ACTIVITYMASTER" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="人脈活動維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="1080px" OnDelete="dataGridViewOnDelete" OnSelect="dataGridViewOnSelect">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="代號" Editor="numberbox" FieldName="ACTIVITY_ID" Format="" Visible="True" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="活動名稱" Editor="text" FieldName="ACTIVITY_NAME" Format="" MaxLength="0" Visible="true" Width="380" />
                    <JQTools:JQGridColumn Alignment="center" Caption="推薦人數" Editor="text" FieldName="HEADCOUNT" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="活動日期" Editor="datebox" FieldName="ACTIVITY_DATE" Format="yyyy/mm/dd" Visible="true" Width="75" />
                    <JQTools:JQGridColumn Alignment="center" Caption="開放報名" Editor="checkbox" FieldName="ISOPENREGISTER" Format="" Visible="true" Width="80" EditorOptions="on:1,off:0" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CREATE_MAN" Format="" MaxLength="0" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Visible="True" Width="150" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                   <%-- <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />--%>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="人脈活動維護">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CON_ACTIVITYMASTER" HorizontalColumnsCount="1" RemoteName="sCON_ACTIVITYMASTER.CON_ACTIVITYMASTER" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="numberbox" FieldName="ACTIVITY_ID" Format="" Width="40" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動名稱" Editor="text" FieldName="ACTIVITY_NAME" Format="" maxlength="0" Width="360" />
                        <JQTools:JQFormColumn Alignment="left" Caption="活動日期" Editor="datebox" FieldName="ACTIVITY_DATE" Format="yyyy/mm/dd" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開放報名" Editor="checkbox" FieldName="ISOPENREGISTER" Format="" Width="90" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CREATE_MAN" Editor="text" FieldName="CREATE_MAN" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CREATE_DATE" Editor="datebox" FieldName="CREATE_DATE" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ACTIVITY_ID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ACTIVITY_DATE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="ISOPENREGISTER" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ACIIVITY_NAME" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
              <br />
            <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="CON_ACTIVITYDETAILS" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="20" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" EditDialogID="JQDialog2" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sCON_ACTIVITYMASTER.CON_ACTIVITYDETAILS" RowNumbers="True" Title="活動名單" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="1080px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="ACTIVITY_ID" Editor="text" FieldName="ACTIVITY_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="聯聯人代號" Editor="text" FieldName="CONTACT_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="CONTACT_COMPANY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="重複" Editor="text" FieldName="HEADCOUNT" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="48" FormatScript="HEADCOUNT_FormatScript">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="參加人員" Editor="text" FieldName="CONTACT_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="CONTACT_DEPT" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="職稱" Editor="text" FieldName="CONTACT_JOB" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="CONTACT_TEL" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="手機" Editor="text" FieldName="CONTACT_CELLPHONE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="信箱" Editor="text" FieldName="CONTACT_EMAIL" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="160">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="資料來源" Editor="text" FieldName="FROMCENTER" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                    </JQTools:JQGridColumn>
                </Columns>
                  <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="DeleteActivityContact" Text="刪除" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出EXCEL" />
                </TooItems>
            </JQTools:JQDataGrid>
          
            <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="JQDataForm1" Title="活動名單維護">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="CON_ACTIVITYDETAILS" HorizontalColumnsCount="1" RemoteName="sCON_ACTIVITYMASTER.CON_ACTIVITYDETAILS" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AUTOKEY" Editor="text" FieldName="AUTOKEY" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ACTIVITY_ID" Editor="text" FieldName="ACTIVITY_ID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CONTACT_ID" Editor="text" FieldName="CONTACT_ID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="CONTACT_COMPANY" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="參加人員" Editor="text" FieldName="CONTACT_NAME" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門" Editor="text" FieldName="CONTACT_DEPT" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="CONTACT_JOB" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="CONTACT_TEL" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="CONTACT_CELLPHONE" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="信箱" Editor="text" FieldName="CONTACT_EMAIL" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯入群組" Editor="text" FieldName="FROMCENTER" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯入人員" Editor="text" FieldName="CREATE_MAN" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯入日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ACTIVITY_ID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ACTIVITY_DATE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="ISOPENREGISTER" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
              <JQTools:JQDialog ID="JQDialog3" runat="server" DialogLeft="15px" DialogTop="60px" Title="推薦重複群組列表" Width="530px" Closed="True" ShowSubmitDiv="False">
                 <JQTools:JQDataGrid ID="JQDataGrid2" runat="server" AlwaysClose="True" DataMember="CON_ACTIVITYDETAILSTEMP" RemoteName="sCON_ACTIVITYMASTER.CON_ACTIVITYDETAILSTEMP" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Window" QueryTitle="設備查詢" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdate="" Height="450px" Width="450px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="AUTOKEY" Editor="text" FieldName="AUTOKEY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="ACTIVITY_ID" Editor="text" FieldName="ACTIVITY_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="推薦群組" Editor="text" FieldName="CENTER_CNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                        </JQTools:JQGridColumn>
                     </Columns>
                     <TooItems>
                    <%-- <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
            <%--         <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                     <JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton"   OnClick="ConcordContact" Text="整合聯絡人" />--%>
                </TooItems>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
