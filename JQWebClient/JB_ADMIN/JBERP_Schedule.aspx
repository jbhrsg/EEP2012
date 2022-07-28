<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_Schedule.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="js/schedule/jquery.schedule.js" type="text/javascript"></script>
    <script type="text/javascript">
   //     <link href="js/schedule/jquery.schedule.css" rel="stylesheet" />;
 
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
            var Org = GetEmpOrg();
            $("#JQComboBox1").combobox('setValue', 1);
            $("#JQComboBox2").combobox('setValue', Org);
            $('#JQSchedule1').data('theQueryKey1', 1).data('theQueryKey2', Org).schedule('load');
        });
        function cbOnSelect(rowData) {
            $('#JQSchedule1').data('theQueryKey1', rowData.ScheTypeID).schedule('load');
        }
        function cbOrgOnSelect(rowData) {
            $('#JQSchedule1').data('theQueryKey2', rowData.ORG_NO).schedule('load');
        }
        function GetNewScheduleByType() {
            var schedule = $(this);
            var theQueryKey1 = schedule.data('theQueryKey1');                     //行事曆篩選條件載入
            theQueryKey1 = theQueryKey1 ? theQueryKey1 : '';
            var theQueryKey2 = schedule.data('theQueryKey2');                     //行事曆篩選條件載入
            theQueryKey2 = theQueryKey2 ? theQueryKey2 : '';
            var options = schedule.data('options');                             //加入QueryString
            options.queryWord.whereString += String.format(" and [ScheType]='{0}' ", theQueryKey1);
            if (theQueryKey2 == '00000') {
                options.queryWord.whereString += String.format('AND ApplyOrg_NO IS NOT NULL');
            }
            else {
                options.queryWord.whereString += String.format(" and [ApplyOrg_NO]='{0}' ", theQueryKey2);
            }
            schedule.data('options', options);
        }
        function GetEmpOrg() {
            var UserID = getClientInfo("UserID");
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sSchedule.ScheduleType', 
                data: "mode=method&method=" + "GetEmpOrg" + "&parameters=" + UserID , 
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        ReturnStr = data;
                    }
                }
            });
            return ReturnStr;
        }
        function GGGG(e) {
            e.item.FieldName = '';
            e.itemCss = { 'background-color': 'blue' };
        }
 
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
        <br />
        <div>行事曆類別
        <JQTools:JQComboBox ID="JQComboBox1" runat="server" DisplayMember="ScheTypeName" RemoteName="sSchedule.ScheduleType" ValueMember="ScheTypeID" Width="50px" OnSelect="cbOnSelect" >
        </JQTools:JQComboBox>
            部門
             <JQTools:JQComboBox ID="JQComboBox2" runat="server" DisplayMember="ORG_DESC" RemoteName="sSchedule.Organization" ValueMember="ORG_NO" Width="50px" OnSelect="cbOrgOnSelect" >
             </JQTools:JQComboBox>

        </div>
        <JQTools:JQSchedule ID="JQSchedule1" runat="server" AllowUpdate="False" DateField="OutDate" RemoteName="sSchedule.Schedule" TimeFormat="hhmm" TimeFromField="ScheStaTime" TimeToField="ScheEndTime" TipField="" TitleField="ScheTitle" OnBeforeLoad="GetNewScheduleByType" />
        <br />
</form>
</body>
</html>
