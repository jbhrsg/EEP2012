<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_ContractGroup.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
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
        function OnLoad_dataGridView() {
                $("#dataGridView").datagrid("selectRow", -1);
                $("#dataGridView").datagrid("selectRow", 0);
        }
        function dataGridViewOnSelect(rowindex, rowdata) {
            if (rowdata != null && rowdata != undefined) {
                var CENTER_ID = rowdata.CENTER_ID;
                $("#JQDataGrid1").datagrid('setWhere', "CENTER_ID=" + CENTER_ID);
            }
        }
        function GetCENTER_ID() {
            var id = $("#dataGridView").datagrid('getSelected').CENTER_ID;
            return id;
        }
        function SetUserName() {
            var rowData = $("#JQDataForm1USERID").combobox('getSelectItem');
            $("#JQDataForm1USERNAME").val(rowData.USERNAME);
        }
        //function genCheckBox(val) {
        //    if (val)
        //        return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
        //    else
        //        return "<input  type='checkbox'  onclick='return false;'  />";
        //}
        function OnSelectCENTER_CNAME() {
            $("#dataFormMasterCENTER_ENAME").val($("#dataFormMasterCENTER_CNAME").combobox("getText"));
        }
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPContractGroup.ERPContractGroup" runat="server" AutoApply="True"
                DataMember="ERPContractGroup" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="權責部門" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="840px" OnSelect="dataGridViewOnSelect" OnLoadSuccess="OnLoad_dataGridView">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="代號" Editor="numberbox" FieldName="CENTER_ID" Format="" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="權責部門名稱" Editor="text" FieldName="CENTER_CNAME" Format="" MaxLength="0" Visible="False" Width="180" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="權責部門名稱" Editor="text" FieldName="CENTER_ENAME" Format="" MaxLength="0" Visible="True" Width="180" />
                    <JQTools:JQGridColumn Alignment="right" Caption="排序" Editor="numberbox" FieldName="CENTER_SEQ" Format="" Visible="False" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="UPDATE_MAN" Editor="text" FieldName="UPDATE_MAN" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="UPDATE_DATE" Editor="datebox" FieldName="UPDATE_DATE" Format="" Visible="False" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                  <%--  <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />--%>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="False" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="群組設定" DialogLeft="30px" DialogTop="50px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPContractGroup" HorizontalColumnsCount="2" RemoteName="sERPContractGroup.ERPContractGroup" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="部門代號" Editor="numberbox" FieldName="CENTER_ID" Format="" Width="60" ReadOnly="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="中文名稱" Editor="infocombobox" FieldName="CENTER_CNAME" Format="" maxlength="0" Width="180" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sERPContractGroup.SYS_ORG',tableName:'SYS_ORG',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:true,onSelect:OnSelectCENTER_CNAME,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="英文名稱" Editor="text" FieldName="CENTER_ENAME" Format="" maxlength="0" Width="180" Visible="False" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="排序" Editor="numberbox" FieldName="CENTER_SEQ" Format="" Width="60" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CREATE_MAN" Editor="text" FieldName="CREATE_MAN" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CREATE_DATE" Editor="datebox" FieldName="CREATE_DATE" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="英文名稱" Editor="text" FieldName="UPDATE_MAN" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UPDATE_DATE" Editor="datebox" FieldName="UPDATE_DATE" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="CENTER_ID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <br />
            <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="ERPContractGroupUser" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPContractGroup.ERPContractGroupUser" RowNumbers="True" Title="權責部門使用者" TotalCaption="Total:"  UpdateCommandVisible="True" ViewCommandVisible="True" Width="840px" EditDialogID="JQDialog2">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="CENTER_ID" Editor="text" FieldName="CENTER_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="使用者代號" Editor="text" FieldName="USERID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" EditorOptions="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="使用者姓名" Editor="text" FieldName="USERNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="業務人員" Editor="checkbox" FieldName="ISSALES" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CREATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="UPDATE_MAN" Editor="text" FieldName="UPDATE_MAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="UPDATE_DATE" Editor="text" FieldName="UPDATE_DATE" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                         OnClick="insertItem" Text="新增" />
               </TooItems>
              </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="JQDataForm1" Title="群組使用者" Width="600px">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="ERPContractGroupUser" HorizontalColumnsCount="1" RemoteName="sERPContractGroup.ERPContractGroupUser" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="CENTER_ID" Editor="text" FieldName="CENTER_ID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="使用者" Editor="infocombobox" FieldName="USERID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERPContractGroup.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:SetUserName,panelHeight:200" OnBlur="GetUserName" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務人員" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="ISSALES" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CREATE_MAN" Editor="text" FieldName="CREATE_MAN" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CREATE_DATE" Editor="text" FieldName="CREATE_DATE" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UPDATE_MAN" Editor="text" FieldName="UPDATE_MAN" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UPDATE_DATE" Editor="text" FieldName="UPDATE_DATE" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="USERNAME" Editor="text" FieldName="USERNAME" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCENTER_ID" FieldName="CENTER_ID" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                </JQTools:JQValidate>

              
            </JQTools:JQDialog>
        </div>
    
              

    </form>
</body>
</html>
