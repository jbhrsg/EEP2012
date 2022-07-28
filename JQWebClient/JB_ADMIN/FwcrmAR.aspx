<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FwcrmAR.aspx.cs" Inherits="Template_JQuerySingle1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="EFClientTools" namespace="EFClientTools" tagprefix="cc1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>                                
         $(document).ready(function () {
             $("input, select, textarea").focus(function () {
                 $(this).css("background-color", "#FFFFB5");
             });

             $("input, select, textarea").blur(function () {
                 $(this).css("background-color", "white");
             });                          
             $("#txtRC").css('font-size', 20);
             //$("#bnRC").css('background-color', '#008CBA');
         });
                
         function OnLoadSuccessGV() {
             ////panel寬度調整
             //var dgid = $('#dataGridMaster');
             //var queryPanel = getInfolightOption(dgid).queryDialog;
             //if (queryPanel)
             //    $(queryPanel).panel('resize', { width: 400 });
             ////Grid隱藏
             //$('#dataGridMaster').datagrid('getPanel').hide();            

         }

         function bnAR() {//查詢後添加固定條件
           
             var ResidenceID = $("#txtRC").val();

             if (ResidenceID == "") {
                 alert('請輸入居留證號！');
                 return false;

             } else {

                 var url = "../JB_ADMIN/REPORT/FWCRM/ARMasterReport.aspx?ResidenceID=" + ResidenceID;

                 var height = $(window).height() - 80;
                 var height2 = $(window).height() + 150;
                 var width = "830";//$(window).width() - 20;
                 var dialog = $('<div/>')
                 .dialog({
                     draggable: false,
                     modal: true,
                     height: height,
                     //top:0,
                     width: width,
                     title: "收據明細",
                     //maximizable: true                              
                 });
                 $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="' + height2 + '"></iframe>').appendTo(dialog.find('.panel-body'));
                 dialog.dialog('open');
             }
         }
         
    </script> 
    <style type="text/css">
     
        .button1 {
          background-color: white;
          color: black;
          border: 2px solid #b2b2ff; /* Green */
        }
      
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" AgentDatabase="JBADMIN" AgentSolution="JBADMIN" AgentUser="A0123" />
        </div>           
        <div class="button1" style="width: 530px">
                <table>
                    <tr>
                        <td  style="width: 180px; text-align: right; ">Số thẻ cư trú<br />
                            Masukan no arc<br />
                            ENTER ARC NUMBER<br />
                            กรุณาใส่หมายเลขกาม่า</td>
                        <td>
                            <JQTools:JQTextBox ID="txtRC" runat="server" />
                            <JQTools:JQButton ID="bnRC" runat="server" Text="查詢" onclick="bnAR()" CssClass="button1" />
                        </td>
                    </tr>                   
                </table>
         </div>
        
</form>
</body>
</html>
