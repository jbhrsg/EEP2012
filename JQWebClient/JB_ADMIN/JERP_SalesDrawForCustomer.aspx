<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JERP_SalesDrawForCustomer.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jcanvas.min.js"></script>
    <script src="../js/jbCanvasDrawRect.js"></script>
    <script type="text/javascript">
        //alert(Request.getQueryStringByName("type"));
        //alert(Request.getQueryStringByName("pd"));

        var SalesDate = Request.getQueryStringByName("pd");
        var SalesTypeID = Request.getQueryStringByName("type");
        var xy1 = Request.getQueryStringByName("xy1");
        var xy2 = Request.getQueryStringByName("xy2");
        var xy3 = Request.getQueryStringByName("xy3");
        $(function () {
            for (i = 1; i < 4; i++) {
                $('#Canvas' + i).jbCanvasDrawRect({ strokeStyle: 'red', strokeWidth: 5, drawGroupName: 'showGroupName', showGroupName: 'showGroupName' });
                $('#Canvas' + i).jbCanvasDrawRect('drawEnable', true);
            }

            //var SalesDate = $("#dataGridView").datagrid('getSelected').SalesDate.substr(0, 10);
            //var SalesTypeID = $("#dataGridView").datagrid('getSelected').SalesTypeID;
            //畫報紙,畫框
            if (SalesTypeID == '1') {//求才
                $('#tt').tabs('enableTab', 2);
                DrawImage("../Files/JBERP_SalesPaper/SalesType1_A/" + SalesDate + ".png", "#Canvas1", xy1);
                DrawImage("../Files/JBERP_SalesPaper/SalesType1_B/" + SalesDate + ".png", "#Canvas2", xy2);
                DrawImage("../Files/JBERP_SalesPaper/SalesType1_C/" + SalesDate + ".png", "#Canvas3", xy3);
            } else if (SalesTypeID == '31') {//便利報
                DrawImage("../Files/JBERP_SalesPaper/SalesType31_A/" + SalesDate + ".png", "#Canvas1", xy1);
                DrawImage("../Files/JBERP_SalesPaper/SalesType31_B/" + SalesDate + ".png", "#Canvas2", xy2);
                $('#tt').tabs('disableTab', 2);
            }

            function DrawImage(FilePath, Canvas, Coordinate) {
                $.ajax({
                    url: FilePath,
                    async: false,
                    type: "HEAD",//get status code ex:404(not found)
                    success: function () {
                        $(Canvas).jbCanvasDrawRect('loadImage', FilePath);//畫報
                        setTimeout(function () {
                            if (Coordinate != "") { $(Canvas).jbCanvasDrawRect('setLocationList', $.parseJSON(Coordinate)); }//畫框
                        }, 100);
                    },
                    error: function () { }
                });
            }

            //function DrawImage(FilePath, Canvas) {
            //    $.ajax({
            //        url: FilePath,
            //        async: false,
            //        type: "HEAD",//get status code ex:404(not found)
            //        success: function () {
            //            $(Canvas).jbCanvasDrawRect('loadImage', FilePath);//畫報
            //            //setTimeout(function () {
            //            //if (Coordinate != "") { $(Canvas).jbCanvasDrawRect('setLocationList', $.parseJSON(Coordinate)); }//畫框
            //            //}, 100);
            //        },
            //        error: function () { }
            //    });
            //}
        });

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" AgentDatabase="JBADMIN" AgentSolution="JBADMIN" AgentUser="A0123" AgentDeveloper="" />
            <%--<JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERPSalesDraw.ERPSalesDetails" runat="server" AutoApply="True"
                DataMember="ERPSalesDetails" Pagination="True" QueryTitle="Query"
                Title="JERP_SalesDrawForCustomer">
                <Columns>
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
            </JQTools:JQDataGrid>--%>
            <div id="tt" class="easyui-tabs" style="width: 800px; height: 440px;">
                    <div title="正面" style="overflow: auto;padding: 10px;">
                        <canvas id='Canvas1' style="width: 760px; height: 360px"></canvas>
                        <div id="MenuDiv1"></div>
                    </div>
                    <div title="反面" style="overflow: auto;padding: 10px">
                        <canvas id='Canvas2' style="width: 760px; height: 360px"></canvas>
                        <div id="MenuDiv2"></div>
                    </div>
                    <div  title="共面" style="overflow: auto;padding: 10px">
                        <canvas id='Canvas3' style="width: 760px; height: 360px"></canvas>
                        <div id="MenuDiv3"></div>
                    </div>
                </div>
        </div>
    <%--<JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="defaultMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="validateMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQValidate>--%>
</form>
</body>
</html>
