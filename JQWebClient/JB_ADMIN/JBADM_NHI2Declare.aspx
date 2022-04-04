<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBADM_NHI2Declare.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script> 
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
    function OnSelectYearMonth(rowData) {
        var YearMonth = rowData.YearMonth;
        var FiltStr = " InsGroupID in (Select InsGroupID From dbo.View_2NHIInsGroup Where YearMonth = '" + YearMonth + "')"
        $("#InsGroupID_Query").combobox('setWhere',FiltStr);
    }
    function queryGrid(dg) {
        var UserID = getClientInfo("UserID");
        if ($(dg).attr('id') == 'dataGridView') {
            var YearMonth = $("#YearMonth_Query").combobox('getValue');
            if (YearMonth == '' || YearMonth == 'undefined') {
                alert('提示!!未選取申報年月,請選取');
                $("#YearMonth_Query").focus();
                return false;
            }
            var InsGroupID = $("#InsGroupID_Query").combobox('getValue');
            if (InsGroupID == '' || InsGroupID == 'undefined') {
                alert('提示!!未選取公司別,請選取');
                $("#InsGroupID_Query").focus();
                return false;
            }
            var FiltStr = "YearMonth = '" + YearMonth + "' and  InsGroupID= '" + InsGroupID + "'";
            $("#dataGridView").datagrid('setWhere', FiltStr);
            $("#JQDataGrid1").datagrid('setWhere', FiltStr);
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sNHI2Declare.NHI2DeclareBill" runat="server" AutoApply="True"
                DataMember="NHI2DeclareBill" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="統編_基本資料" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="850px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="YearMonth" Editor="text" FieldName="YearMonth" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SaryID" Editor="text" FieldName="SaryID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InsGroupID" Editor="text" FieldName="InsGroupID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNo" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="所得人身份證號" Editor="text" FieldName="IDNumber" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="所得人姓名" Editor="text" FieldName="NameC" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="所得人地址" Editor="text" FieldName="Addr" Format="" MaxLength="0" Width="320" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"  OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"  Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton"   OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                   <%-- <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportXlsx" Text="匯出Excel" Visible="True" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="申報年月" Condition="=" DataType="string" DefaultMethod="" Editor="infocombobox" EditorOptions="valueField:'YearMonth',textField:'YearMonth',remoteName:'sNHI2Declare.YearMonth',tableName:'YearMonth',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectYearMonth,panelHeight:200" FieldName="YearMonth" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="" DataType="string" DefaultMethod="" DefaultValue="" Editor="infocombobox" EditorOptions="valueField:'INSGROUPID',textField:'INSGROUPSHORTNAME',remoteName:'sNHI2Declare.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InsGroupID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="NHI2DeclareBill" HorizontalColumnsCount="2" RemoteName="sNHI2Declare.NHI2DeclareBill" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="YearMonth" Editor="text" FieldName="YearMonth" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SaryID" Editor="text" FieldName="SaryID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="InsGroupID" Editor="text" FieldName="InsGroupID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="TaxNo" Editor="text" FieldName="TaxNo" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IDNumber" Editor="text" FieldName="IDNumber" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="NameC" Editor="text" FieldName="NameC" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Addr" Editor="text" FieldName="Addr" Format="" maxlength="0" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
         <div>
      
             <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" Title="統編_明細資料" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="NHI2DeclareAmount" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sNHI2Declare.NHI2DeclareAmount" RowNumbers="True"  TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                 <Columns>
                     <JQTools:JQGridColumn Alignment="left" Caption="投保單位代號" Editor="text" FieldName="HealthInsNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="所得人身份證" Editor="text" FieldName="IDNumber" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="所得人姓名" Editor="text" FieldName="NameC" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="所得給付日期" Editor="text" FieldName="GetDay" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="所得類別" Editor="text" FieldName="InComeType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="給付當月投保金額" Editor="text" FieldName="Note1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="本次股利所屬期間,以雇主身分加保之投保總金額" Editor="text" FieldName="Note2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="股利註記" Editor="text" FieldName="Note3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="信託註記" Editor="text" FieldName="Note4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="扣取時可扣抵稅額" Editor="text" FieldName="Note5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="年度確定可扣抵稅額" Editor="text" FieldName="Note6" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="除權息基準日" Editor="text" FieldName="Note7" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="特殊註記" DrillObjectID="" Editor="text" FieldName="Note8" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="right" Caption="所得收入給付金額" Editor="text" FieldName="Income" Format="N0" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="right" Caption="扣繳補充保險費金額" Editor="text" FieldName="NHI2Amount" Format="N0" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                     </JQTools:JQGridColumn>
                 </Columns>
                 <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"  OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"  Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton"   OnClick="exportGrid" Text="匯出Excel" Visible="True" />
               <%--     <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportXlsx" Text="匯出Excel" Visible="True" />--%>
                 </TooItems>
             </JQTools:JQDataGrid>
      
        </div>
    </form>
</body>
</html>
