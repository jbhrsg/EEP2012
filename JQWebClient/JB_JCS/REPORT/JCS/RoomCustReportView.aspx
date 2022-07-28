<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoomCustReportView.aspx.cs" Inherits="HtmlPages_eva_customers" %>

<%@ Register Assembly="EFClientTools" Namespace="EFClientTools" TagPrefix="cc1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>¦í±J¦W¥U</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager" runat="server" />
            <cc1:WebDataSource ID="DataSource" runat="server" RemoteName="" PreviewSolution="SOLUTION1"
                PreviewDatabase="SDERPS" PacketRecords="-1" />

            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="500px"
                OnLoad="ReportViewer_Load" Font-Names="Verdana" Font-Size="8pt"
                InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                WaitMessageFont-Size="14pt">
                <LocalReport EnableExternalImages="True" OnSubreportProcessing="SubreportProcessing" ReportPath="">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="DataSource" Name="TestDataSet" />
                    </DataSources> 
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
