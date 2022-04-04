<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBREQ_CostCenter.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
  <script type="text/javascript">
      function CheckDelCostCenter() { //檢查支付憑據是否可刪除
          var row = $('#dataGridView').datagrid('getSelected'); //取得當前主檔中選中的那個Data
          var cnt;
          $.ajax({
              type: "POST",
              url: '../handler/jqDataHandle.ashx?RemoteName=sCostCenter.CostCenter',    
              data: "mode=method&method=" + "CheckDelCostCenter" + "&parameters=" + row.CostCenterID, 
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
              alert('此成本中心已有請款單使用,無法刪除!!');
              return false;
          }
        

      }
      function genCheckBox(val) {
          if (val)
              return "<input  type='checkbox' checked='true' />";
          else
              return "";
          //  return "<input  type='checkbox' />";
      }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCostCenter.CostCenter" runat="server" AutoApply="True"
                DataMember="CostCenter" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="成本中心" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="15,30,45,60" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="720px" OnDelete="CheckDelCostCenter" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="代號" Editor="text" FieldName="CostCenterID" Format="" MaxLength="0" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="名稱" Editor="text" FieldName="CostCenterName" Format="" MaxLength="0" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="生效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" EditorOptions="" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton"   OnClick="exportXlsx" Text="匯出Excel" Visible="True" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="成本中心" DialogLeft="50px" DialogTop="50px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CostCenter" HorizontalColumnsCount="1" RemoteName="sCostCenter.CostCenter" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" IsAutoPause="False" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="電腦序號" Editor="text" FieldName="AutoKey" Format="" Width="180" Span="2" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="text" FieldName="CostCenterID" Format="" Width="180" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="名稱" Editor="text" FieldName="CostCenterName" Format="" maxlength="0" Width="180" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="生效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" EditorOptions="" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="CreateBy" RemoteMethod="True" DefaultMethod="" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CostCenterID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CostCenterName" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
