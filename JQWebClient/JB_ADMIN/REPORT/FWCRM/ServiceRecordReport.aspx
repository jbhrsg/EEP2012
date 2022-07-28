<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceRecordReport.aspx.cs" Inherits="HtmlPages_eva_customers" %>

<%@ Register Assembly="EFClientTools" Namespace="EFClientTools" TagPrefix="cc1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="Srvtools, Version=5.0.0.0, Culture=neutral, PublicKeyToken=8763076c188bfb12" namespace="Srvtools" tagprefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-size: small">
            <asp:ScriptManager ID="ScriptManager" runat="server" />
            <cc1:WebDataSource ID="DataSource" runat="server" RemoteName="" PreviewSolution="SOLUTION1"
                PreviewDatabase="SDERPS" PacketRecords="-1" />

            簽到名冊排序條件：<asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Size="Small" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Selected="True" Value="1">依入境日期</asp:ListItem>
                <asp:ListItem Value="2">依中文姓名</asp:ListItem>
                <asp:ListItem Value="3">依英文姓名</asp:ListItem>
            </asp:RadioButtonList>
&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="報表預覽" />
            <br />

            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="412px"
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
