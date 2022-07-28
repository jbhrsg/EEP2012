<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_CompetitionOutput1.aspx.cs" Inherits="Template_JQuerySingle1" %>
<%@ Register assembly="JQChartTools" namespace="JQChartTools" tagprefix="cc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%--<style type="text/css">
        #divJQBarChart1 {
        /*padding:0px 0px 0px 200px*/   
        }
    </style>--%>
    <style>
        table {
        border:thin;
        border-color:black;
        }

        .auto-style1 {
            height: 20px;
        }

    </style>
    <script type="text/javascript">
        var viewport = {
            width: $(window).width(),
            height: $(window).height()
        };
        //$(function () {
        //    alert(viewport.width + ';' + viewport.height);
        //});
        $(function () {
            //if (viewport.width < 700) {
            //    $("#tb1td1").attr({ "text-align": "right" });
            //    $("#tb2td1").attr({ "text-align": "right" });
            //    $("#tb2td2").attr({ "text-align": "right" });
            //    var options = $("#JQBarChart1").plotbarchart('options');
            //    options.width = '680';
            //    $("#JQBarChart1").plotbarchart('options', options);
            //    $("#JQBarChart1").plotbarchart('resize', true);
            //} else {


                $("#tb1td1").css({ "text-align": "left" });
                $("#tb2td1").css({ "text-align": "left" });
                $("#tb2td2").css({ "text-align": "left" });
                var options = $("#JQBarChart1").plotbarchart('options');
                options.width = '1000';
                $("#JQBarChart1").plotbarchart('options', options);
                $("#JQBarChart1").plotbarchart('resize', true);

                //setTimeout(function () {
                //    $("#JQBarChart1").plotbarchart("setWhere", "");//因有時候目標達成率圖不會顯示
                //}, 5000);
                //setTimeout(function () {
                //    $("#JQComboBox1").combobox({ panelHeight: 'auto', panelMinHeight: 180 });
                //    $("#JQComboBox1").combobox("setWhere", "");
                //}, 6000);

                
            //}
        });
        //function dataGridMaster_OnLoadSuccess() {
            //OnClickButton1();
        //}

        function OnSelectJQComboBox1(rows) {
            //if (viewport.width < 700) {
            //    var options = $("#JQLineChart1").plotlinechart('options');
            //    options.width = '680';
            //    $("#JQLineChart1").plotlinechart('options', options);
            //    var where = "t1.Department='" + rows.Department + "'";
            //    $("#JQLineChart1").plotlinechart("setWhere", where);
            //    setTimeout(function () {
            //        $("#JQLineChart1").plotlinechart('resize', true);
            //    }, 700);
            //} else {
                var where = "t1.Department='" + rows.Department + "'";
                $("#JQLineChart1").plotlinechart("setWhere", where);
            //}
        }
        function OnClickButton1() {
            $("#JQBarChart1").plotbarchart("setWhere", "");
            $("#JQComboBox1").combobox({ panelHeight: 'auto', panelMinHeight: 180 });
            $("#JQComboBox1").combobox("setWhere", "");
        }

    </script>
</head>
<body style="background-image: url(../img/main.jpg);">
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" UseChartJS="True" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sCompetitionOutput1.RealTargetRatio" runat="server" AutoApply="True"
                DataMember="RealTargetRatio" Pagination="True" QueryTitle="Query"
                Title="JBERP_CompetitionOutput1" Visible="False" >
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="Year" Editor="numberbox" FieldName="Year" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="Month" Editor="numberbox" FieldName="Month" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Department" Editor="text" FieldName="Department" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="RealSum" Editor="numberbox" FieldName="RealSum" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="QuarterTarget" Editor="numberbox" FieldName="QuarterTarget" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="RealTargetRatio" Editor="numberbox" FieldName="RealTargetRatio" Format="" Width="120" />
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
        <br/>
        <JQTools:JQButton ID="JQButton1" runat="server" OnClick="OnClickButton1" Text="刷新" />
                <table style="width:100%">
                    <tr>
                        <td style="text-align:center"><asp:Label ID="Label2" runat="server" Text="2022第三季業績競賽" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align:center"><asp:Label ID="Label4" runat="server" Text="TOP Gun一飛衝天" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                        </td>
                    </tr>
                </table>
            <table style="width:100%">
                <tr><td id="tb1td1" style="text-align:left">
                <cc1:JQBarChart ID="JQBarChart1" runat="server" DataMember="RealTargetRatio" KeyField="Department" RemoteName="sCompetitionOutput1.RealTargetRatio" Stack="True" Title="各單位目標達成率(單位:%)" Height="410px" labelShow="True" LegendLocation="ne" LegendPlacement="insideGrid" LegendShow="False" pointLabels="True" RenderObjectID="" Top="" DecimalDigit="1">
            <DataFields>
                <cc1:JQChartDataField Caption="目標達成率" FieldName="RealTargetRatio" />
            </DataFields>
        </cc1:JQBarChart>
        </td></tr>
            </table>

            <br/>

        <table  style="width:100%">
        <tr><td id="tb2td1" style="text-align:left" class="auto-style1"><asp:Label ID="Label1" runat="server" Font-Size="Smaller" Text="累積目標達成率曲線→請選擇部門"></asp:Label>
        <%--</td></tr>
<tr><td id="tb2td2" align="left">--%>
<JQTools:JQComboBox ID="JQComboBox1" runat="server" DisplayMember="Department" OnSelect="OnSelectJQComboBox1" RemoteName="sCompetitionOutput1.RealTargetRatio" ValueMember="Department" Width="180px" Height="50px" PageSize="0">
        </JQTools:JQComboBox>
        </td></tr>
        <tr><td id="tb2td2">
            <%--<JQTools:JQPanel ID="JQPanel1" runat="server" Title=" " Width="1100px">--%>
        <cc1:JQChartBase ID="JQChartBase1" runat="server">
        </cc1:JQChartBase>
        <cc1:JQLineChart ID="JQLineChart1" runat="server" DataMember="AccumRealTargetRatio" KeyField="Date1" KeyShowField="" LegendShow="False" RemoteName="sCompetitionOutput1.AccumRealTargetRatio" AlwaysClose="True" LegendLocation="ne" LegendPlacement="outsideGrid" Title="累積目標達成率曲線(單位:%)" Width="100%" Height="480px">
            <DataFields>
                <cc1:JQLineChartDataField Caption="累進目標達成率" FieldName="AccumRatio" LineWidth="2" MarkerStyle="filledCircle" ShowPointLabels="True" />
            </DataFields>
        </cc1:JQLineChart>
        <cc1:JQChartBase ID="JQChartBase2" runat="server">
        </cc1:JQChartBase>
            <%--</JQTools:JQPanel>--%>
        </td></tr>
        </table>
</form>
        
</body>
</html>
