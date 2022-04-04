<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_CustomersJobs.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {
            //設定欄位Caption 變顏色
            var flagIDs = ['#dataFormDetailJobWorkContent', '#dataFormDetailJobName', '#dataFormDetailJobFunctionID', '#dataFormDetailJobRequirement', '#dataFormDetailJobNeedCount'
                , '#dataFormDetailGender', '#dataFormDetailEduLevelID', '#dataFormDetailEduSubject', '#dataFormDetailEduDepart', '#dataFormDetailOrderID', '#dataFormDetailJobArea'
                , '#dataFormDetailJobWorkArea', '#dataFormMasterIndustryType', '#dataFormMasterService', '#dataFormDetailJobLangID1', '#dataFormDetailJobLangID2',
            '#dataFormDetailJobLangID3', '#dataFormDetailJobLangID4', '#dataFormDetailJobLangID5', '#dataFormDetailJobLangID6'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: '#8A2BE2' });
            });
            　
            //新增時將客戶名稱帶給客戶簡稱
            $('#dataFormMasterCustName').blur(function () {
                var cc = $('#dataFormMasterCustShortName').val();
                if (cc == "") {
                    $('#dataFormMasterCustShortName').val($('#dataFormMasterCustName').val());
                }
              });
            //新增時將電話區域號碼帶給聯絡人1電話區碼
            $('#dataFormMasterCustomerTelArea').blur(function () {
                var cc = $('#dataFormMasterContactTelArea1').val();
                if (cc == "") {
                    $('#dataFormMasterContactTelArea1').val($('#dataFormMasterCustomerTelArea').val());
                }
            });
            //新增時將電話號碼帶給聯絡人1電話號碼
            $('#dataFormMasterCustomerTel').blur(function () {
                var cc = $('#dataFormMasterContactTel1').val();
                if (cc == "") {
                    $('#dataFormMasterContactTel1').val($('#dataFormMasterCustomerTel').val());
                }
            });
            //新增時將客戶名稱帶給客戶簡稱
            $('#dataFormMasterCustName').blur(function () {
                var cc = $('#dataFormMasterCustShortName').val();
                if (cc == "") {
                    $('#dataFormMasterCustShortName').val($('#dataFormMasterCustName').val());
                }
            });
            //新增時將職缺詳細地址帶給職缺工作地點
            $('#dataFormDetailJobWorkAreaLoc').blur(function () {
                var cc = $('#dataFormDetailJobWorkArea').val();
                if (cc == "") {
                    $('#dataFormDetailJobWorkArea').val($('#dataFormDetailJobWorkAreaLoc').val());
                }
            });
            //新增時將客戶簡稱+職務名稱帶給推薦履歷
            $('#dataFormDetailJobName').blur(function () {
                var cc = $('#dataFormDetailJobResumeFile').val();
                if (cc == "") {
                    $('#dataFormDetailJobResumeFile').val($('#dataFormMasterCustShortName').val() + '_' + $('#dataFormDetailJobName').val());
                }
            });
            //資本額加上(元)
            var Capital = $('#dataFormMasterCapital').closest('td');
            Capital.append('  元');
            //客戶電話 區號+電話 合併為同TD顯示
            var Custarea1 = $('#dataFormMasterCustomerTelArea').closest('td');
            var Custcode1 = $('#dataFormMasterCustomerTel').closest('td').children();
            Custarea1.append('-').append(Custcode1);
            //在客戶負責人加入經濟部商業司超連結
            var EconmicLink = $("<a>").attr({ 'href': 'http://www.gcis.nat.gov.tw/pub/cmpy/cmpyInfoListAction.do'}).attr({'target':'_blank'}).text("    經濟部商業司");
            var BuildYears = $('#dataFormMasterBuildYears').closest('td');
            BuildYears.append(EconmicLink);
            //即時通類型1 即時通類型+帳號合併縣顯示
            var imtype1 = $('#dataFormMasterContIMType1').closest('td');
            var imno1 = $('#dataFormMasterContIMNO1').closest('td').children();
            imtype1.append(' 帳號').append(imno1);
            //即時通類型2 即時通類型+帳號合併縣顯示
            var imtype2 = $('#dataFormMasterContIMType2').closest('td');
            var imno2 = $('#dataFormMasterContIMNO2').closest('td').children();
            imtype2.append(' 帳號').append(imno2);
            //即時通類型3 即時通類型+帳號合併縣顯示
            var imtype3 = $('#dataFormMasterContIMType3').closest('td');
            var imno3 = $('#dataFormMasterContIMNO3').closest('td').children();
            imtype3.append(' 帳號').append(imno3);
            //到職區域 到職區域與到職縣市合併顯示 
            //var jobarea = $('#dataFormDetailJobArea').closest('td');
            //var jobworkarea = $('#dataFormDetailJobWorkArea').closest('td').children();
            //var JobWorkAreaLoc = $('#dateFormDetailJobWorkAreaLoc').closest('td').children();
            //jobarea.append(jobworkarea).append(JobWorkAreaLoc);
            //客戶聯絡人1 區號+電話+分機 合併為同TD顯示
            var area1 = $('#dataFormMasterContactTelArea1').closest('td');
            var code1 = $('#dataFormMasterContactTel1').closest('td').children();
            var ext1  = $('#dataFormMasterContactTelExt1').closest('td').children();
            area1.append('-').append(code1).append(' 分機').append(ext1);
            //客戶聯絡人2 區號+電話+分機 合併為同TD顯示
            var area2 = $('#dataFormMasterContactTelArea2').closest('td');
            var code2 = $('#dataFormMasterContactTel2').closest('td').children();
            var ext2 = $('#dataFormMasterContactTelExt2').closest('td').children();
            area2.append('-').append(code2).append(' 分機').append(ext2);
            //客戶聯絡人3 區號+電話+分機 合併為同TD顯示
            var area3 = $('#dataFormMasterContactTelArea3').closest('td');
            var code3 = $('#dataFormMasterContactTel3').closest('td').children();
            var ext3  = $('#dataFormMasterContactTelExt3').closest('td').children();
            area3.append('-').append(code3).append(' 分機').append(ext3);
            //職務聯絡人 區號+電話+分機 合併為同TD顯示
            var Jobarea3 = $('#dataFormDetailJobContTelArea').closest('td');
            var Jobcode3 = $('#dataFormDetailJobContTel').closest('td').children();
            var Jobext3 = $('#dataFormDetailJobContExt').closest('td').children();
            Jobarea3.append('-').append(Jobcode3).append(Jobext3);
            //學類 學類+科系合併顯示
            var EduSubject = $('#dataFormDetailEduSubject').closest('td');
            var EduDepart = $('#dataFormDetailEduDepart').closest('td').children();
            EduSubject.append($('<lable>').css({ color: '#8A2BE2' }).html(' 科系')).append(EduDepart);
            //
            var DutyTitle = $('#dataFormDetailDutyTitle').closest('td');
            DutyTitle.append('  ＊職缺搜尋履歷專用')
            //
            var JobRequirement = $('#dataFormDetailJobRequirement').closest('td');
            JobRequirement.append('  ＊公告使用')
            var JobTerms = $('#dataFormDetailJobTerms').closest('td');
            JobTerms.append('  ＊內部使用')
            var JobTEMP = $('#dataFormDetailTEMP').closest('td');
            JobTEMP.append('  ＊此欄位來自客戶資料,供複製轉貼使用,請勿編輯')
            var JobResumeFile = $('#dataFormDetailJobResumeFile').closest('td');
            JobResumeFile.append('  ＊此欄位不可有字元 /\\*?,:"<>|')
            //設定 Grid QueryColunm Windows width=320px
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel) 
                $(queryPanel).panel('resize', { width: 320 });
            //建立產業別的Tree
            $("#Dialog_IndTree").dialog(
                {
                    height: 640,
                    width: 420,
                    resizable: false,
                    modal: true,
                    title: "產業別",
                    closed: true,
                    buttons: [{
                        text: '確定',
                        handler: function () {
                            var idField = $('#JQTreeView1').tree('getSelected');
                            var id = (idField.id);
                            $("#dataFormMasterIndustryType").combobox('setValue', id);
                            $("#Dialog_IndTree").dialog("close");
                        }
                    },
                    {
                        text: '結束',
                        handler: function () { $("#Dialog_IndTree").dialog("close"); }
                    }
                    ]
                });

            $('#JQTreeView1').tree('collapseAll');
            $("#dataFormMasterIndustryType").bind("click", function () {
                Dialog_IndTree();
            });
            //以Master欄位過濾Details Combox內容  
            $("#dataFormDetail").form({ //初始欄位控制設定
                onLoadSuccess: function (data) {
                    var combo = $("#dataFormDetailContractNO"); //查詢欄位combobox的名稱
                    var CustID = $("#dataGridView").datagrid('getSelected').CustID;
                    combo.combobox('setWhere', "CustID = '" + CustID + "'");
                }
            });
            $("#Dialog_JobLang").dialog(
               {
                   height: 480,
                   width: 420,
                   resizable: false,
                   modal: true,
                   title: "學門學類",
                   closed: true,
                   buttons: [{
                       text: '確定',
                       handler: function () {
                           var idField = $('#JQTreeView2').tree('getSelected');
                           var id = (idField.id);
                           $("#dataFormDetailEduSubject").combobox('setValue', id);
                           $("#Dialog_JobLang").dialog("close");
                       }
                   },
                   {
                       text: '結束',
                       handler: function () { $("#Dialog_JobLang").dialog("close"); }
                   }
                   ]
               });

            $('#JQTreeView2').tree('collapseAll');
            $("#dataFormDetailEduSubject").bind("click", function () {
                openDialog_JobLang;
            });
            $('#dataFormDetailEduSubject').jbCombobox2tree({
                parentField: 'ParentID'
            });
            $('#dataFormMasterIndustryType').jbCombobox2tree({
                parentField: 'ParentID'
            });
         

        });

        function Showtooltip() {
            var IndustryType = $("#dataFormMasterIndustryType+.combo :hidden[name=IndustryType]").val();
            if (IndustryType == 0) {
                $("#dataFormMasterIndustryType").combobox('setValue', 0);
             }
             $('#dataFormMasterCustomerArea').attr('title','請輸入縣市或地區如上海、香港、北京、加州等');
             $('#dataFormMasterCustomerAddress').attr('title','請輸入詳細地址如鄉鎮、里、路、鄰、巷、路等');
             $('#dataFormDetailDutyDept').attr('title', '請輸入「工作部門」關鍵字,並以「,」做間隔,例如「品保,研發,稽核....」');
             $('#dataFormDetailDutyTitle').attr('title', '請輸入「工作職稱」關鍵字,並以「,」做間隔,例如「消防,製程,業務副總....」');
             $('#dataFormMasterContactTelArea1').attr('title', '請輸入「電話區號」例02、03、039,05......');
             $('#dataFormMasterContactTelArea2').attr('title', '請輸入「電話區號」例02、03、039,05......');
             $('#dataFormMasterContactMobile1').attr('title', '請以下列格式輸入「9999-999999」');
             $('#dataFormMasterContactMobile2').attr('title', '請以下列格式輸入「9999-999999」');
             $('#dataFormMasterContactMobile3').attr('title', '請以下列格式輸入「9999-999999」');
             $('#dataFormMasterPhotoPath').attr('title', '限上傳檔案格式 「jpg、png、pdf、docx、ppt、xls」');
             $('#dataFormMasterOtherPath').attr('title', '限上傳檔案格式 「pdf、docx、ppt、xls」');
        };
        function dataFormLoadSucess() {
            var EduSubject = $("#dataFormMasterEduSubject+.combo :hidden[name=EduSubject]").val();
            if (EduSubject == 0) {
                $("#dataFormMasterEduSubject").combobox('setValue', 0);
            }
            var dd = $('#dataFormMasterContactMark').val();
            if (dd != "") {
               $('#dataFormDetailTEMP').val($('#dataFormMasterContactMark').val());
            }
        }
        //以上為combotree
        //開啟行業別Dialog,顯示樹狀結構
        function openDialog() {
            $("#Dialog_IndTree").dialog("open");
        };
        //開啟學門學類Dialog,顯示樹狀結構
        function openDialog_JobLang() {
            $("#Dialog_JobLang").dialog("open");
        };
        function formatdate(val) {
            if (val != undefined) {
                return val.toString().replace('T', ' ');
            }
        }
        //複制職缺
        function CopyJob() {
            var rowcount = $('#dataGridDetail').datagrid('getData').total;
            if (rowcount <= 0) {
                alert('注意!! 沒有可選取職缺資料,本功能無法使用');
                return false;
            }
            var row = $('#dataGridDetail').datagrid('getSelected');
            $('#dataGridDetail').datagrid('appendRow', row);
            row.JobID = 0;
            //新增且同時OPENDATAFORM一筆資料
            openForm('#JQDialog2', row, "inserted", 'dialog');
        }
        //焦點欄位變顏色
        $(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "yellow");
            });

            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });
        });
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox'  onclick='return false;'  />";
        }
        //異動資料欄位超連結
        function HyperlinkLog(value, row, index) {
            return "<a href='javascript: void(0)' onclick='LinkLog(" + index + ");'>" + value + "</a>";
        }
        function LinkLog(index) {
            $("#dataGridMaster").datagrid('selectRow', index);
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var ID = rows.RELATION_ID;
            $("#Dialog_TransLog").dialog("open");
            $("#DG_HRM_RELATION_LOG").datagrid('setWhere', "HRM_RELATION_LOG.RELATION_ID = '" + ID + "'");
        }
        function MasterGridReload() {
            $("#dataGridView").datagrid('reload');
        }
       //檢查客戶記錄是否可刪除
        function CheckDelCustomer() {
            var row = $('#dataGridView').datagrid('getSelected'); //取得當前主檔中選中的那個Data
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCustomersJobs.HUT_Customer', //連接的Server端，command
                data: "mode=method&method=" + "CheckDelCustomer" + "&parameters=" + row.CustID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
                alert('此客戶已有合約使用,無法刪除!!');
                return false;
            }
        }
        //取得6個月後日期
        function AfterMonths() {

            var dt = new Date();
            return dt.getFullYear() + '-' + (dt.getMonth() + 6) + '-' + dt.getDate()
        }
        //傳回客戶聯絡人1姓名
        function  GetCustContactName1() {
            var Contact = $('#dataFormMasterContactName1').val();
            return Contact;
        }
        //傳回客戶聯絡人1職稱姓名
        function GetCustContactTitle1() {
            var Title = $('#dataFormMasterContactTitle1').val();
            return Title;
        }
        //傳回客戶聯絡人1區域號碼
        function GetCustContactTelArea1() {
            var Tel = $('#dataFormMasterContactTelArea1').val();
            return Tel;
        }
        //傳回客戶聯絡人1電話
        function GetCustContactTel1() {
            var Tel = $('#dataFormMasterContactTel1').val();
            return Tel;
        }
        //傳回客戶聯絡人1分機
        function GetCustContactTelExt1() {
            var TelExt = $('#dataFormMasterContactTelExt1').val();
            return TelExt;
        }
        //傳回客戶聯絡人1eMail
        function GetCustContacteMail1() {
            var EMail = $('#dataFormMasterContacteMail1').val();
            return EMail;
        }
        //傳回客戶聯絡人1eMail
        function GetCustContactMobile1() {
            var ContactMobile = $('#dataFormMasterContactMobile1').val();
            return ContactMobile;
        }
        //傳回客戶工作地區
        function GetCustCustomerArea() {
            var CustomerArea = $('#dataFormMasterCustomerArea').val();
            return CustomerArea;
        }
        //傳回客戶詳細地址
        function GetCustCustomerAddress() {
            var CustomerAddress = $('#dataFormMasterCustomerAddress').val();
            return CustomerAddress;
        }
        //傳回客戶付款方式
        function GetCustCustomerPayDescr() {
            var PayDescr = $('#dataFormMasterPayDesc').val();
            return PayDescr;
        }
        //傳回客戶上班時間
        function GetCustWorkTime() {
            var WorkTime = $('#dataFormMasterWorkTime').val();
            return WorkTime;
        }
        //產生下載組織圖下載連結
        function OrgDLink(value) {
            if (value == "" || value == null) {
                return "NA";
            } else {
                value = "images/"+value 
                return "<a  href='" + value + "' target='_blank' >組織圖</a>";
            }
        }
        //產生下載招募重點下載連結
        function RecDLink(value) {
            if (value == "" || value == null) {
                return "NA";
            } else {
                value="images/"+value
                return "<a  href='" + value + "' target='_blank' >招募重點</a>";
            }
        }
        function FilterLangID1() {
            var val = $(this).combobox("getValue");
            var combo=$('#dataFormDetailJobLangLicence1');
            combo.combobox('setWhere', "HUT_LangLicence.LangID = '" + val + "'");
        }
        function FilterLangID2() {
            var val = $(this).combobox("getValue");
            var combo = $('#dataFormDetailJobLangLicence2');
            combo.combobox('setWhere', "HUT_LangLicence.LangID = '" + val + "'");
        }
        function FilterLangID3() {
            var val = $(this).combobox("getValue");
            var combo = $('#dataFormDetailJobLangLicence3');
            combo.combobox('setWhere', "HUT_LangLicence.LangID = '" + val + "'");
        }
        function FilterLangID4() {
            var val = $(this).combobox("getValue");
            var combo = $('#dataFormDetailJobLangLicence4');
            combo.combobox('setWhere', "HUT_LangLicence.LangID = '" + val + "'");
        }
        function FilterLangID5() {
            var val = $(this).combobox("getValue");
            var combo = $('#dataFormDetailJobLangLicence5');
            combo.combobox('setWhere', "HUT_LangLicence.LangID = '" + val + "'");
        }
        function FilterLangID6() {
            var val = $(this).combobox("getValue");
            var combo = $('#dataFormDetailJobLangLicence6');
            combo.combobox('setWhere', "HUT_LangLicence.LangID = '" + val + "'");
        }
        //檢查字串是否含有特殊字元
        function CheckStrWildWord(str) {
            var r = str.match(/^[^/\\*?,:"<>|]+$/);
            if (r == null) {
                alert('「推薦履歷」欄位,不可含有特殊字元!!');
                return false;
            }
            else {                
                return true;
            }
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 1134px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td colspan="2" class="auto-style1">
                        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
                        <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCustomersJobs.HUT_Customer" runat="server" AutoApply="True" 
                            DataMember="HUT_Customer" Pagination="True" QueryTitle="快速查詢" EditDialogID="JQDialog1"
                            Title="客戶資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="90px" QueryMode="Window" QueryTop="120px" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDelete="CheckDelCustomer" ColumnsHibeable="False" RecordLockMode="None" Width="1080px">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶編號" Editor="text" FieldName="CustID" MaxLength="20" Width="80" EditorOptions="" Visible="False" Sortable="False" />
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶電話" Editor="text" FieldName="CustTel" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" MaxLength="30" Width="90" Sortable="True" />
                                <JQTools:JQGridColumn Alignment="center" Caption="職缺數" Editor="text" FieldName="JobCount" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" />
                                <JQTools:JQGridColumn Alignment="center" Caption="合約" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsContact" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40" Format="" FormatScript="genCheckBox" />
                                <JQTools:JQGridColumn Alignment="left" Caption="產品服務項目" Editor="text" FieldName="Service" MaxLength="512" Width="350" />
                                <JQTools:JQGridColumn Alignment="left" Caption="產業別" Editor="text" FieldName="IndName" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140" />
                                <JQTools:JQGridColumn Alignment="left" Caption="產業別" Editor="text" FieldName="IndustryType" Width="90" EditorOptions="" Sortable="True" Visible="False" />
                                <JQTools:JQGridColumn Alignment="center" Caption="等級" Editor="text" FieldName="CustomerGrade" MaxLength="10" Width="30" EditorOptions="" />
                                <JQTools:JQGridColumn Alignment="left" Caption="組織圖" Editor="text" FieldName="PhotoPath" Format="Image,Folder:JB_Hunter/Images,Height:30" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="60" FormatScript="OrgDLink" />
                                <JQTools:JQGridColumn Alignment="left" Caption="業務人員" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',columns:[],columnMatches:[],whereItems:[],valueField:'ID',textField:'HunterName',valueFieldCaption:'ID',textFieldCaption:'HunterName',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="HunterID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                                <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" EditorOptions="" FieldName="CreateBy" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="60" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                                    OnClick="insertItem" Text="新增" />
                                <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                                    Text="存檔" />
                                <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                                    Text="取消" />
                                <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                                    OnClick="openQuery" Text="快速查詢" />
                            </TooItems>
                            <QueryColumns>
                                <JQTools:JQQueryColumn Caption="客戶電話" Condition="%%" DataType="string" Editor="text" FieldName="CustomerTel" NewLine="True" RemoteMethod="False" Width="180" AndOr="" />
                                <JQTools:JQQueryColumn Caption="客戶簡稱" Condition="%%" DataType="string" Editor="text" FieldName="CustShortName" NewLine="True" RemoteMethod="False" Width="180" AndOr="" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="業務人員" Condition="=" DataType="string" DefaultValue="" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="182" />
                            </QueryColumns>
                        </JQTools:JQDataGrid>
                        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="客戶資料" DialogLeft="10px" DialogTop="10px" Width="1080px" Wrap="False" EditMode="Dialog">
                            <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HUT_Customer" HorizontalColumnsCount="5" RemoteName="sCustomersJobs.HUT_Customer" Closed="False" ContinueAdd="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="MasterGridReload" disapply="False" IsRejectON="False" OnLoadSuccess="Showtooltip" IsAutoPause="False">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="產業別" Editor="infocombobox" FieldName="IndustryType" Width="250" EditorOptions="valueField:'ID',textField:'IndCategoryName',remoteName:'sCustomersJobs.HUT_IndCategoryL1',tableName:'HUT_IndCategoryL1',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="5" MaxLength="0" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="公司簡介" Editor="textarea" FieldName="Introduction" MaxLength="1024" Width="512" EditorOptions="height:75" Span="5" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="產品服務項目" Editor="textarea" FieldName="Service" MaxLength="1024" Width="512" Span="5" EditorOptions="height:75" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="組織圖上傳" Editor="infofileupload" FieldName="PhotoPath" MaxLength="100" Width="512" Span="5" EditorOptions="filter:'jpg|png|pdf|docx|xls|tif|doc|ppt',isAutoNum:true,upLoadFolder:'JB_Hunter/Images',showButton:true,showLocalFile:false" Format="" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人(1)" Editor="text" FieldName="ContactName1" MaxLength="20" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="ContactTitle1" MaxLength="100" Width="250" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="ContactTelArea1" MaxLength="0" Width="25" RowSpan="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTel1" MaxLength="20" Width="78" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTelExt1" MaxLength="10" Width="30" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="ContactMobile1" MaxLength="20" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="ContacteMail1" MaxLength="128" Width="250" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="即時通類型" Editor="infocombobox" FieldName="ContIMType1" MaxLength="20" Width="116" EditorOptions="valueField:'IMNAME',textField:'IMNAME',remoteName:'sCustomersJobs.HUT_ZIMType',tableName:'HUT_ZIMType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContIMNO1" MaxLength="0" Width="90" Span="2" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人(2)" Editor="text" FieldName="ContactName2" MaxLength="20" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="ContactTitle2" MaxLength="100" Width="250" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="ContactTelArea2" MaxLength="0" Width="25" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTel2" MaxLength="20" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTelExt2" MaxLength="10" Width="30" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="ContactMobile2" MaxLength="20" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="ContacteMail2" MaxLength="128" Width="250" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="即時通類型" Editor="infocombobox" FieldName="ContIMType2" MaxLength="20" Width="116" EditorOptions="valueField:'IMNAME',textField:'IMNAME',remoteName:'sCustomersJobs.HUT_ZIMType',tableName:'HUT_ZIMType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContIMNO2" MaxLength="0" Width="90" Span="2" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人(3)" Editor="text" FieldName="ContactName3" MaxLength="20" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="ContactTitle3" MaxLength="100" Width="250" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="ContactTelArea3" MaxLength="0" Width="25" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTel3" MaxLength="20" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTelExt3" MaxLength="10" Width="30" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="ContactMobile3" MaxLength="20" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="ContacteMail3" MaxLength="128" Width="250" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="即時通類型" Editor="infocombobox" FieldName="ContIMType3" MaxLength="20" Width="116" EditorOptions="valueField:'IMNAME',textField:'IMNAME',remoteName:'sCustomersJobs.HUT_ZIMType',tableName:'HUT_ZIMType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContIMNO3" MaxLength="0" Width="90" Span="2" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="聯絡人備註" Editor="textarea" FieldName="ContactMark" MaxLength="512" Width="386" Span="5" EditorOptions="height:80" ReadOnly="False" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="福利要項" Editor="textarea" FieldName="Benefits" MaxLength="1024" Width="512" Span="5" EditorOptions="height:120" ReadOnly="False"  />
                                    <JQTools:JQFormColumn Alignment="left" Caption="上班時間" Editor="textarea" FieldName="WorkTime" MaxLength="1024" Width="512" Span="5" EditorOptions="height:120" ReadOnly="False" Visible="True" />

                                    <JQTools:JQFormColumn Alignment="left" Caption="招募重點" Editor="textarea" EditorOptions="height:245" FieldName="RecruitNotes" MaxLength="0" Span="5" Width="540" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="複試重點" Editor="textarea" EditorOptions="height:245" FieldName="ReviewNotes" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="540" />

                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶編號" Editor="text" FieldName="CustID" MaxLength="20" Width="100" ReadOnly="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" MaxLength="128" Width="230" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" MaxLength="30" Width="130" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="網址" Editor="text" FieldName="CustomerUrl" MaxLength="256" Width="220" Span="2" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="負責人" Editor="text" FieldName="PersonName" MaxLength="20" Width="100" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="CustTaxNo" MaxLength="10" Width="100" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="資本額" Editor="text" FieldName="Capital" MaxLength="0" Width="130" Span="1" Format="" Visible="True" />
                                   
                                    <JQTools:JQFormColumn Alignment="left" Caption="成立日期" Editor="datebox" FieldName="BuildYears" Width="100" MaxLength="0" Format="yyyy/mm/dd" Span="2" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="地區" Editor="text" FieldName="CustomerArea" MaxLength="128" Width="180"/>
                                 
                                    <JQTools:JQFormColumn Alignment="left" Caption="詳細地址" Editor="text" FieldName="CustomerAddress" MaxLength="128" Width="230" Span="1" Visible="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="CustomerTelArea" MaxLength="0" Width="30"/>
                                    <JQTools:JQFormColumn Alignment="left" Caption="傳真" Editor="text" FieldName="CustomerFax" MaxLength="0" Width="97" Span="1" Visible="False"/>
                                    <JQTools:JQFormColumn Alignment="left" Caption="員工人數" Editor="numberbox" FieldName="EmployeeCount"  Width="97" MaxLength="0" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CustomerTel" MaxLength="20" Visible="True" Width="90" />
                                   
                                  
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶等級" Editor="infocombobox" FieldName="CustomerGrade" Width="100" MaxLength="10" Span="1" EditorOptions="valueField:'ID',textField:'Grade',remoteName:'sCustomersJobs.HUT_ZGRAType',tableName:'HUT_ZGRAType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True"/>
                                    <JQTools:JQFormColumn Alignment="left" Caption="業務單位" Editor="infocombobox" FieldName="SalesTeamID" MaxLength="0" Width="100" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="業務人員" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                                   
                                    <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" MaxLength="20" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" FieldName="CreateDate" Format="" MaxLength="0" Span="1" Visible="False" Width="180" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" MaxLength="20" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="修正日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" FieldName="LastUpdateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                                   
                                </Columns>

                            </JQTools:JQDataForm>

                            <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="True" DataMember="HUT_Job" EditDialogID="JQDialog2" Pagination="True" ParentObjectID="dataFormMaster" RemoteName="sCustomersJobs.HUT_Customer" Title="客戶職缺列表" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="05,10,15,20" PageSize="5" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" ColumnsHibeable="False" RecordLockMode="None">
                                <Columns>
                                    <JQTools:JQGridColumn Alignment="right" Caption="職缺代號" Editor="numberbox" FieldName="JobID" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="False" Width="60" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="客戶" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sCustomersJobs.HUT_Customer',tableName:'HUT_Customer',columns:[],columnMatches:[],whereItems:[],valueField:'CustID',textField:'CustShortName',valueFieldCaption:'CustID',textFieldCaption:'CustShortName',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="CustID" Width="120" Visible="False" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺類別" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sJobType.HUT_JobType',tableName:'HUT_JobType',columns:[],columnMatches:[],whereItems:[],valueField:'ID',textField:'JobTypeName',valueFieldCaption:'ID',textFieldCaption:'JobTypeName',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="JobTypeID" Width="80" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="合約編號" Editor="text" FieldName="ContractNO" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="120" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderID" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="180" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺類別" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sFunction.HUT_Function',tableName:'HUT_Function',columns:[],columnMatches:[],whereItems:[],valueField:'FunctionID',textField:'FunctionName',valueFieldCaption:'FunctionID',textFieldCaption:'FunctionName',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="JobFunctionID" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="急迫性" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sReferences.View_HUT_ZPRIType',tableName:'View_HUT_ZPRIType',columns:[],columnMatches:[],whereItems:[],valueField:'Priority',textField:'Priority',valueFieldCaption:'Priority',textFieldCaption:'Priority',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="JobPriority" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="70" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="機密程度" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sReferences.HUT_ZKeepType',tableName:'HUT_ZKeepType',columns:[],columnMatches:[],whereItems:[],valueField:'keepType',textField:'keepType',valueFieldCaption:'keepType',textFieldCaption:'keepType',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="JobKeepType" Visible="True" Width="70" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" EditorOptions="" FieldName="JobContName" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人職稱" Editor="text" FieldName="JobContTitle" Visible="False" Width="80" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobContTelArea" Editor="text" FieldName="JobContTelArea" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人電話" Editor="text" FieldName="JobContTel" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="eMail" Editor="text" FieldName="JobContMail" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人分機" Editor="text" FieldName="JobContExt" Visible="False" Width="80" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobContMobile" Editor="text" FieldName="JobContMobile" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobArea" Editor="text" FieldName="JobArea" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺工作地" Editor="text" FieldName="JobWorkArea" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺工作地" Editor="text" FieldName="JobWorkAreaLoc" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺面試地" Editor="text" FieldName="JobRecAreaLoc" Visible="False" Width="80" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="需求人數" Editor="text" FieldName="JobNeedCount" Visible="True" Width="60" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="需求起始日" Editor="datebox" FieldName="JobDateStd" Visible="False" Width="120" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="工作內容" Editor="text" FieldName="JobWorkContent" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="福利" Editor="text" FieldName="JobFare" Visible="False" Width="80" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="出勤規定" Editor="text" FieldName="JobAttendRule" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="需求原因" Editor="infocombobox" EditorOptions="valueField:'JobRequestID',textField:'JobRequestName',remoteName:'sCustomersJobs.HUT_JobRequest',tableName:'HUT_JobRequest',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobRequestID" Width="90" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobRequestName" Editor="text" FieldName="JobRequestName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="90" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobRequirement" Editor="text" FieldName="JobRequirement" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="面試人員" Editor="text" FieldName="JobAuditor" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" QueryCondition="" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="獵才顧問" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sHunter.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="獵才公司" Editor="text" FieldName="HuntGroupID" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="獵才業務" Editor="text" FieldName="SalesTeamID" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="性別需求" Editor="text" FieldName="JobGender" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="起始年齡" Editor="text" FieldName="JobAge1" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="終止年齡" Editor="text" FieldName="JobAge2" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="教育程度" Editor="text" FieldName="EduLevelID" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="學類" Editor="text" FieldName="EduSubject" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="科系" Editor="text" FieldName="EduDepart" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺證照" Editor="text" FieldName="JobLicense" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobSkill" Editor="text" FieldName="JobSkill" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺態度" Editor="text" FieldName="JobAttitude" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺備註" Editor="text" FieldName="JobNotes" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言需求程度" Editor="text" FieldName="JobLangNeedType" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言1" Editor="text" FieldName="JobLangID1" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言1聽" Editor="text" FieldName="JobLangListen1" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言1說" Editor="text" FieldName="JobLangSpeak1" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言1讀" Editor="text" FieldName="JobLangRead1" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言1寫" Editor="text" FieldName="JobLangWrite1" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言證照" Editor="text" FieldName="JobLangLicence1" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="證照分數" Editor="text" FieldName="JobLangScore1" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言2" Editor="text" FieldName="JobLangID2" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言2聽" Editor="text" FieldName="JobLangListen2" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言2說" Editor="text" FieldName="JobLangSpeak2" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言2讀" Editor="text" FieldName="JobLangRead2" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言2寫" Editor="text" FieldName="JobLangWrite2" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言證照" Editor="text" FieldName="JobLangLicence2" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="證照分數" Editor="text" FieldName="JobLangScore2" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言3" Editor="text" FieldName="JobLangID3" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言3聽" Editor="text" FieldName="JobLangListen3" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言3說" Editor="text" FieldName="JobLangSpeak3" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言3讀" Editor="text" FieldName="JobLangRead3" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言3寫" Editor="text" FieldName="JobLangWrite3" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言證照" Editor="text" FieldName="JobLangLicence3" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="證照分數" Editor="text" FieldName="JobLangScore3" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言4" Editor="text" FieldName="JobLangID4" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言4聽" Editor="text" FieldName="JobLangListen4" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言4說" Editor="text" FieldName="JobLangSpeak4" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言4讀" Editor="text" FieldName="JobLangRead4" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言4寫" Editor="text" FieldName="JobLangWrite4" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言證照" Editor="text" FieldName="JobLangLicence4" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="證照分數" Editor="text" FieldName="JobLangScore4" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言5" Editor="text" FieldName="JobLangID5" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言5聽" Editor="text" FieldName="JobLangListen5" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言5說" Editor="text" FieldName="JobLangSpeak5" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言5讀" Editor="text" FieldName="JobLangRead5" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言5寫" Editor="text" FieldName="JobLangWrite5" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言證照" Editor="text" FieldName="JobLangLicence5" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="證照分數" Editor="text" FieldName="JobLangScore5" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言6" Editor="text" FieldName="JobLangID6" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言6聽" Editor="text" FieldName="JobLangListen6" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言6說" Editor="text" FieldName="JobLangSpeak6" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言6讀" Editor="text" FieldName="JobLangRead6" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言6寫" Editor="text" FieldName="JobLangWrite6" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="語言證照" Editor="text" FieldName="JobLangLicence6" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="證照分數" Editor="text" FieldName="JobLangScore6" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="關閉日期" Editor="text" FieldName="JobCloseDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" Format="yyyy/mm/dd" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="DutyDept" Editor="text" FieldName="DutyDept" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="DutyTitle" Editor="text" FieldName="DutyTitle" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyName1" Editor="text" FieldName="JobNotifyName1" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyMail1" Editor="text" FieldName="JobNotifyMail1" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyType1" Editor="text" FieldName="JobNotifyType1" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyName2" Editor="text" FieldName="JobNotifyName2" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyMail2" Editor="text" FieldName="JobNotifyMail2" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyType2" Editor="text" FieldName="JobNotifyType2" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyName3" Editor="text" FieldName="JobNotifyName3" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyMail3" Editor="text" FieldName="JobNotifyMail3" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyType3" Editor="text" FieldName="JobNotifyType3" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyName4" Editor="text" FieldName="JobNotifyName4" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyMail4" Editor="text" FieldName="JobNotifyMail4" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyType4" Editor="text" FieldName="JobNotifyType4" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyName5" Editor="text" FieldName="JobNotifyName5" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyMail5" Editor="text" FieldName="JobNotifyMail5" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyType5" Editor="text" FieldName="JobNotifyType5" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyName6" Editor="text" FieldName="JobNotifyName6" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyMail6" Editor="text" FieldName="JobNotifyMail6" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobNotifyType6" Editor="text" FieldName="JobNotifyType6" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="110" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="修正日期" Editor="text" FieldName="LastUpdateDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobDeclareDate" Editor="text" FieldName="JobDeclareDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="JobTerms" Editor="text" FieldName="JobTerms" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                </RelationColumns>
                                <TooItems>
                                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                                    <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" Visible="False" />
                                    <JQTools:JQToolItem Icon="icon-cut" ItemType="easyui-linkbutton" OnClick="CopyJob" Text="複製" Visible="True" />
                                    <JQTools:JQToolItem ItemType="easyui-linkbutton" />
                                </TooItems>
                            </JQTools:JQDataGrid>

                            <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Title="職缺資料" Width="1080px" DialogLeft="50px" DialogTop="25px">
                                <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="HUT_Job" BackColor="Yellow" HorizontalColumnsCount="7" RemoteName="sCustomersJobs.HUT_Customer" Closed="False" ContinueAdd="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" disapply="False" IsRejectON="False" Width="1120px" OnLoadSuccess="dataFormLoadSucess" OnApplied="MasterGridReload" IsAutoPause="False">
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺條件" Editor="textarea" EditorOptions="height:125" FieldName="JobRequirement"  Width="450" Span="7" MaxLength="2000" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺需求" Editor="textarea" EditorOptions="height:125" FieldName="JobTerms" MaxLength="1024" Span="7" Width="450" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="需求起始日" Editor="datebox" FieldName="JobDateStd" Width="200" Format="yyyy/mm/dd" Span="1" MaxLength="0" ReadOnly="False" Visible="True" NewRow="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="關閉日期" Editor="datebox" FieldName="JobCloseDate" Width="200" Span="6" MaxLength="0" ReadOnly="False" Visible="True" NewRow="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="需求人數" Editor="text" FieldName="JobNeedCount" Span="1" Width="197" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Gender',remoteName:'sCustomersJobs.HUT_GenType',tableName:'HUT_GenType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobGender" Span="6" Width="200" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="起始年齡" Editor="text" FieldName="JobAge1" Span="1" Width="197" Visible="True" MaxLength="0" ReadOnly="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="終止年齡" Editor="text" FieldName="JobAge2" Span="6" Width="197" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="教育程度" Editor="infocombobox" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'sCustomersJobs.HUT_ZEduLevel',tableName:'HUT_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EduLevelID" Width="200" Span="1" MaxLength="0" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="學類" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SubjectName',remoteName:'sCustomersJobs.HUT_EduSubject',tableName:'HUT_EduSubject',panelHeight:200" FieldName="EduSubject" Width="200" Span="1" MaxLength="0" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="EduDepart" Span="5" Width="240" MaxLength="0" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="工作部門" Editor="text" FieldName="DutyDept" Width="197" Span="1" MaxLength="0" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="工作職稱" Editor="text" FieldName="DutyTitle" Width="197" Span="1" MaxLength="0" ReadOnly="False" Visible="True" NewRow="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="語言需求條件" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NeedTypeName',remoteName:'sCustomersJobs.HUT_ZLangNeedType',tableName:'HUT_ZLangNeedType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangNeedType" Span="7" Visible="True" Width="120" MaxLength="0" ReadOnly="False" NewRow="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="語言[一]" Editor="infocombobox" EditorOptions="valueField:'LangID',textField:'LangName',remoteName:'sCustomersJobs.HUT_ZLangType',tableName:'HUT_ZLangType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:FilterLangID1,panelHeight:200" FieldName="JobLangID1" Span="1" Width="80" MaxLength="0" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="聽" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangListen1" Span="1" Width="80" MaxLength="0" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="說" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangSpeak1" MaxLength="0" ReadOnly="False" Span="1" Visible="True" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="讀" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangRead1" MaxLength="0" ReadOnly="False" Span="1" Visible="True" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="寫" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangWrite1" MaxLength="0" ReadOnly="False" Span="1" Visible="True" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="證照" Editor="infocombobox" EditorOptions="valueField:'LangLicenceID',textField:'LangLicenceName',remoteName:'sLangLicence.HUT_LangLicence',tableName:'HUT_LangLicence',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangLicence1" MaxLength="0" ReadOnly="False" Span="1" Visible="True" Width="280" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="分數" Editor="numberbox" FieldName="JobLangScore1" MaxLength="0" ReadOnly="False" Span="1" Visible="True" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="語言[二]" Editor="infocombobox" EditorOptions="valueField:'LangID',textField:'LangName',remoteName:'sCustomersJobs.HUT_ZLangType',tableName:'HUT_ZLangType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:FilterLangID2,panelHeight:200" FieldName="JobLangID2" MaxLength="0" Span="1" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="聽" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangListen2" MaxLength="0" Span="1" Width="80" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="說" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangSpeak2" MaxLength="0" Span="1" Width="80" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="讀" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangRead2" MaxLength="0" Span="1" Width="80" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="寫" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangWrite2" MaxLength="0" Span="1" Width="80" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="證照" Editor="infocombobox" EditorOptions="valueField:'LangLicenceID',textField:'LangLicenceName',remoteName:'sLangLicence.HUT_LangLicence',tableName:'HUT_LangLicence',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangLicence2" MaxLength="0" Span="1" Width="280" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="分數" Editor="numberbox" FieldName="JobLangScore2" MaxLength="0" ReadOnly="False" Span="1" Visible="True" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="語言[三]" Editor="infocombobox" EditorOptions="valueField:'LangID',textField:'LangName',remoteName:'sCustomersJobs.HUT_ZLangType',tableName:'HUT_ZLangType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:FilterLangID3,panelHeight:200" FieldName="JobLangID3" MaxLength="0" Span="1" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="聽" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangListen3" Span="1" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="說" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangSpeak3" Span="1" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="讀" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangRead3" Span="1" Width="80" MaxLength="0" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="寫" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangWrite3" Span="1" Width="80" MaxLength="0" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="證照" Editor="infocombobox" EditorOptions="valueField:'LangLicenceID',textField:'LangLicenceName',remoteName:'sLangLicence.HUT_LangLicence',tableName:'HUT_LangLicence',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangLicence3" Span="1" Width="280" MaxLength="0" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="分數" Editor="numberbox" FieldName="JobLangScore3" Span="1" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="語言[四]" Editor="infocombobox" EditorOptions="valueField:'LangID',textField:'LangName',remoteName:'sCustomersJobs.HUT_ZLangType',tableName:'HUT_ZLangType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:FilterLangID4,panelHeight:200" FieldName="JobLangID4" Span="1" Width="80" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="聽" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangListen4" Span="1" Width="80" MaxLength="0" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="說" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangSpeak4" Span="1" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="寫" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangWrite4" Span="1" Width="80" MaxLength="0" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="讀" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangRead4" Span="1" Width="80" MaxLength="0" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="證照" Editor="infocombobox" EditorOptions="valueField:'LangLicenceID',textField:'LangLicenceName',remoteName:'sLangLicence.HUT_LangLicence',tableName:'HUT_LangLicence',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangLicence4" Span="1" Width="280" MaxLength="0" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="分數" Editor="numberbox" FieldName="JobLangScore4" Span="1" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="語言[五]" Editor="infocombobox" EditorOptions="valueField:'LangID',textField:'LangName',remoteName:'sCustomersJobs.HUT_ZLangType',tableName:'HUT_ZLangType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:FilterLangID5,panelHeight:200" FieldName="JobLangID5" Span="1" Width="80" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="聽" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangListen5" Span="1" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="說" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangSpeak5" Span="1" Width="80" MaxLength="0" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="寫" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangWrite5" Span="1" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="讀" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangRead5" Span="1" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="證照" Editor="infocombobox" EditorOptions="valueField:'LangLicenceID',textField:'LangLicenceName',remoteName:'sLangLicence.HUT_LangLicence',tableName:'HUT_LangLicence',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangLicence5" Span="1" Width="280" MaxLength="0" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="分數" Editor="numberbox" FieldName="JobLangScore5" Span="1" Width="80" MaxLength="0" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="語言[六]" Editor="infocombobox" EditorOptions="valueField:'LangID',textField:'LangName',remoteName:'sCustomersJobs.HUT_ZLangType',tableName:'HUT_ZLangType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:FilterLangID6,panelHeight:200" FieldName="JobLangID6" Span="1" Width="80" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="聽" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangListen6" Span="1" Width="80" MaxLength="0" ReadOnly="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="說" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangSpeak6" Span="1" Width="80" MaxLength="0" ReadOnly="False" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="寫" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangWrite6" Span="1" Width="80" MaxLength="0" ReadOnly="False" Visible="True" NewRow="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="讀" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sCustomersJobs.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangRead6" Span="1" Width="80" MaxLength="0" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="證照" Editor="infocombobox" EditorOptions="valueField:'LangLicenceID',textField:'LangLicenceName',remoteName:'sLangLicence.HUT_LangLicence',tableName:'HUT_LangLicence',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobLangLicence6" Span="1" Width="280" MaxLength="0" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="分數" Editor="numberbox" FieldName="JobLangScore6" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="技能" Editor="textarea" EditorOptions="height:182" FieldName="JobSkill"   Span="7" Width="450" MaxLength="2000" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="證照" Editor="textarea" EditorOptions="height:182" FieldName="JobLicense" Span="7" Width="450" MaxLength="2000" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="人格特質" Editor="textarea" EditorOptions="height:182" FieldName="JobAttitude" Span="7" Width="450" MaxLength="2000" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" EditorOptions="height:182" FieldName="JobNotes" Span="7" Width="450" MaxLength="2000" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="工作內容" Editor="textarea" EditorOptions="height:335" FieldName="JobWorkContent" MaxLength="2000" Span="7" Width="645" ReadOnly="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺主試者" Editor="text" FieldName="JobAuditor" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="220" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="面試詳細地址" Editor="text" FieldName="JobRecAreaLoc" Span="6" Width="342" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="工作詳細地址" Editor="text" FieldName="JobWorkAreaLoc" Width="220" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="工作地區" Editor="infocombobox" EditorOptions="valueField:'JobAreaName',textField:'JobAreaName',remoteName:'sCustomersJobs.HUT_ZJobArea',tableName:'HUT_ZJobArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobArea" Width="80" Span="1" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="工作地點" Editor="text" FieldName="JobWorkArea" Span="5" Width="204" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="薪資福利" Editor="textarea" EditorOptions="height:182" FieldName="JobFare" Span="7" Width="450" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="上班時間" Editor="textarea" EditorOptions="height:182" FieldName="JobAttendRule" Span="7" Width="450" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="履歷通知人1" Editor="text" FieldName="JobNotifyName1" Span="1" Width="120" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="收件類型" Editor="infocombobox" FieldName="JobNotifyType1" Span="1" Width="100" EditorOptions="valueField:'NotifyTypeID',textField:'NotifyTypeName',remoteName:'sCustomersJobs.HUT_ZNotifyType',tableName:'HUT_ZNotifyType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="JobNotifyMail1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" Width="220" Span="5" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="履歷通知人2" Editor="text" FieldName="JobNotifyName2" Span="1" Width="120" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="收件類型" Editor="infocombobox" FieldName="JobNotifyType2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" EditorOptions="valueField:'NotifyTypeID',textField:'NotifyTypeName',remoteName:'sCustomersJobs.HUT_ZNotifyType',tableName:'HUT_ZNotifyType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="JobNotifyMail2" Span="5" Width="220" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="履歷通知人3" Editor="text" FieldName="JobNotifyName3" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="收件類型" Editor="infocombobox" FieldName="JobNotifyType3" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" EditorOptions="valueField:'NotifyTypeID',textField:'NotifyTypeName',remoteName:'sCustomersJobs.HUT_ZNotifyType',tableName:'HUT_ZNotifyType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="JobNotifyMail3" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="220"/>
                                        <JQTools:JQFormColumn Alignment="left" Caption="履歷通知人4" Editor="text" FieldName="JobNotifyName4" ReadOnly="False" Span="1" Visible="True" Width="120" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="收件類型" Editor="infocombobox" FieldName="JobNotifyType4" ReadOnly="False" Span="1" Visible="True" Width="100" EditorOptions="valueField:'NotifyTypeID',textField:'NotifyTypeName',remoteName:'sCustomersJobs.HUT_ZNotifyType',tableName:'HUT_ZNotifyType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="JobNotifyMail4" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="220" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="履歷通知人5" Editor="text" FieldName="JobNotifyName5" ReadOnly="False" Span="1" Visible="True" Width="120" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="收件類型" Editor="infocombobox" FieldName="JobNotifyType5" ReadOnly="False" Span="1" Visible="True" Width="100" EditorOptions="valueField:'NotifyTypeID',textField:'NotifyTypeName',remoteName:'sCustomersJobs.HUT_ZNotifyType',tableName:'HUT_ZNotifyType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="JobNotifyMail5" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="220" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="履歷通知人6" Editor="text" FieldName="JobNotifyName6" Span="1" Width="120" MaxLength="0" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="收件類型" Editor="infocombobox" FieldName="JobNotifyType6" Span="1" Width="100" EditorOptions="valueField:'NotifyTypeID',textField:'NotifyTypeName',remoteName:'sCustomersJobs.HUT_ZNotifyType',tableName:'HUT_ZNotifyType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" MaxLength="0" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="JobNotifyMail6" Span="5" Width="220" MaxLength="0" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人備註" Editor="textarea" EditorOptions="height:120" FieldName="TEMP" MaxLength="0" Span="7" Width="360" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="召募重點" Editor="textarea" EditorOptions="height:372" FieldName="RecruitNotes" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="7" Visible="False" Width="600" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="複試重點" Editor="textarea" EditorOptions="height:372" FieldName="ReviewNotes" ReadOnly="False" Span="7" Visible="False" Width="600" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="客戶" Editor="infocombobox" EditorOptions="valueField:'CustID',textField:'CustShortName',remoteName:'sCustomersJobs.HUT_Customer',tableName:'HUT_Customer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustID" ReadOnly="True" Span="1" Width="105" Visible="True" OnBlur="" MaxLength="0" NewRow="False" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="合約性質" Editor="infocombobox" FieldName="ContractNO" ReadOnly="False" Visible="True" Width="180" Span="1" EditorOptions="valueField:'ContRACTNO',textField:'ContContent',remoteName:'sCustomersJobs.HUT_Contract',tableName:'HUT_Contract',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" OnBlur=" CustIDBlur()" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="訂單" Editor="text" FieldName="OrderID" ReadOnly="True" Span="1" Visible="True" Width="150" MaxLength="0" NewRow="False" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺發佈日" Editor="datebox" FieldName="JobDeclareDate" Span="4" Width="120" Format="yyyy/mm/dd" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺代號" Editor="numberbox" FieldName="JobID" ReadOnly="True" Span="1" Visible="False" Width="120" MaxLength="0" NewRow="False" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺" Editor="text" FieldName="JobName" Span="2" Width="337" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="推薦履歷" Editor="text" FieldName="JobResumeFile" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="340" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺性質" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'JobTypeName',remoteName:'sJobType.HUT_JobType',tableName:'HUT_JobType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobTypeID" Span="1" Width="103" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職缺類別" Editor="infocombobox" EditorOptions="valueField:'FunctionID',textField:'FunctionName',remoteName:'sFunction.HUT_Function',tableName:'HUT_Function',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobFunctionID" Span="1" Width="180" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="急迫性" Editor="infocombobox" EditorOptions="valueField:'Priority',textField:'Priority',remoteName:'sCustomersJobs.HUT_ZPRIType',tableName:'HUT_ZPRIType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobPriority" Width="150" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="機密性" Editor="infocombobox" FieldName="JobKeepType" Span="4" Width="180" EditorOptions="valueField:'keepType',textField:'keepType',remoteName:'sCustomersJobs.HUT_ZKeepType',tableName:'HUT_ZKeepType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="JobContName" Width="100" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="JobContTitle" Width="177" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="電話 " Editor="text" FieldName="JobContTelArea" Width="25" Span="1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="JobContMobile" Span="1" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="JobContMail" Span="1" Width="180" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="JobContTel" Width="80" Span="1" Visible="True" MaxLength="0" NewRow="False" ReadOnly="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="JobContExt" Span="1" Width="30" Visible="True" />
                                       
                                        <JQTools:JQFormColumn Alignment="left" Caption="獵才顧問" Editor="infocombobox" FieldName="HunterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="103" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sHunter.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="獵才公司" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HuntGroupName',remoteName:'sHunterGroup.HUT_HuntGroup',tableName:'HUT_HuntGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HuntGroupID" Width="180" Span="1" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="業務單位" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" Width="150" Span="1" Visible="True" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="需求原因" Editor="infocombobox" EditorOptions="valueField:'JobRequestID',textField:'JobRequestName',remoteName:'sCustomersJobs.HUT_JobRequest',tableName:'HUT_JobRequest',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="JobRequestID" Span="4" Visible="True" Width="265" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="False" Width="180" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Width="180" Visible="False" Span="1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" Width="180" Visible="False" Span="1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" Width="180" Visible="False" Span="1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" />
                                    </Columns>
                                    <RelationColumns>
                                        <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                    </RelationColumns>
                                </JQTools:JQDataForm>
                            </JQTools:JQDialog>
                            <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQDefaultColumn DefaultValue="自動編號" FieldName="CustID" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="0" FieldName="Capital" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="1900/01/01" FieldName="BuildYears" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="0" FieldName="EmployeeCount" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="http://" FieldName="CustomerUrl" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="2" FieldName="SalesTeamID" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" FieldName="ContactName1" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IndustryType" RemoteMethod="True" />
                                </Columns>
                            </JQTools:JQDefault>
                            <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CustName" RemoteMethod="True" ValidateMessage="" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CustShortName" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="IndustryType" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CustomerTel" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="ContacteMail1" RemoteMethod="True" ValidateType="EMail" />
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="ContacteMail2" RemoteMethod="True" ValidateType="EMail" />
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="ContacteMail3" RemoteMethod="True" ValidateType="EMail" />
                                </Columns>
                            </JQTools:JQValidate>
                            <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQDefaultColumn DefaultValue="0" FieldName="JobID" RemoteMethod="True" CarryOn="False" />
                                    <JQTools:JQDefaultColumn DefaultValue="一般" FieldName="JobPriority" RemoteMethod="True" CarryOn="False" />
                                    <JQTools:JQDefaultColumn DefaultValue="可公開" FieldName="JobKeepType" RemoteMethod="True" CarryOn="False" />
                                    <JQTools:JQDefaultColumn DefaultValue="1" FieldName="HunterID" RemoteMethod="True" CarryOn="False" />
                                    <JQTools:JQDefaultColumn DefaultValue="1" FieldName="HuntGroupID" RemoteMethod="True" CarryOn="False" />
                                    <JQTools:JQDefaultColumn DefaultValue="2" FieldName="SalesTeamID" RemoteMethod="True" CarryOn="False" />
                                    <JQTools:JQDefaultColumn DefaultValue="27" FieldName="JobAge1" RemoteMethod="True" CarryOn="False" />
                                    <JQTools:JQDefaultColumn DefaultValue="55" FieldName="JobAge2" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="0" FieldName="JobNeedCount" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="2" FieldName="JobGender" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="JobDateStd" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="自動編號" FieldName="OrderID" RemoteMethod="True" CarryOn="False" />
                                    <JQTools:JQDefaultColumn FieldName="JobContName" RemoteMethod="False" DefaultMethod="GetCustContactName1" />
                                    <JQTools:JQDefaultColumn DefaultMethod="GetCustContactTitle1" FieldName="JobContTitle" RemoteMethod="False" CarryOn="False" />
                                    <JQTools:JQDefaultColumn FieldName="JobContTel" RemoteMethod="False" DefaultMethod="GetCustContactTel1" CarryOn="False" />
                                    <JQTools:JQDefaultColumn DefaultMethod="GetCustContactTelExt1" FieldName="JobContExt" RemoteMethod="False" CarryOn="False" />
                                    <JQTools:JQDefaultColumn DefaultMethod="GetCustContacteMail1" FieldName="JobContMail" RemoteMethod="False" />
                                    <JQTools:JQDefaultColumn DefaultMethod="GetCustContactMobile1" FieldName="JobContMobile" RemoteMethod="False" />
                                    <JQTools:JQDefaultColumn DefaultMethod="GetCustCustomerArea" FieldName="JobWorkArea" RemoteMethod="False" />
                                    <JQTools:JQDefaultColumn DefaultMethod="GetCustCustomerAddress" FieldName="JobWorkAreaLoc" RemoteMethod="False" />
                                    <JQTools:JQDefaultColumn FieldName="JobRecAreaLoc" RemoteMethod="False" DefaultMethod="GetCustCustomerAddress" />
                                    <JQTools:JQDefaultColumn FieldName="JobArea" RemoteMethod="True" DefaultValue="台灣" CarryOn="False" />
                                    <JQTools:JQDefaultColumn FieldName="CreateBy" RemoteMethod="True" DefaultValue="_username" CarryOn="False" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultMethod="GetCustContactTelArea1" FieldName="JobContTelArea" RemoteMethod="False" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="JobLangNeedType" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="EduSubject" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="JobRequestID" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="鍾懷德" FieldName="JobNotifyName1" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="3" FieldName="JobNotifyType1" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="paul@jbjob.com.tw" FieldName="JobNotifyMail1" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="許月英" FieldName="JobNotifyName2" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="3" FieldName="JobNotifyType2" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="amy@jbjob.com.tw" FieldName="JobNotifyMail2" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultMethod="GetCustContactName1" FieldName="JobNotifyName3" RemoteMethod="False" />
                                    <JQTools:JQDefaultColumn DefaultValue="1" FieldName="JobNotifyType3" RemoteMethod="True" CarryOn="False" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCustContacteMail1" FieldName="JobNotifyMail3" RemoteMethod="False" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="JobDeclareDate" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCustWorkTime" DefaultValue="" FieldName="JobAttendRule" RemoteMethod="False" />
                                </Columns>
                            </JQTools:JQDefault>
                            <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="ContractNO" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="JobName" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="JobTypeID" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="JobFunctionID" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="JobDateStd" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="JobNeedCount" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EduLevelID" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="JobContMail" RemoteMethod="True" ValidateType="EMail" />
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="JobNotifyMail1" RemoteMethod="True" ValidateType="EMail" />
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="JobNotifyMail2" RemoteMethod="True" ValidateType="EMail" />
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="JobNotifyMail3" RemoteMethod="True" ValidateType="EMail" />
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="JobNotifyMail4" RemoteMethod="True" ValidateType="EMail" />
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="JobNotifyMail5" RemoteMethod="True" ValidateType="EMail" />
                                    <JQTools:JQValidateColumn CheckNull="False" FieldName="JobNotifyMail6" RemoteMethod="True" ValidateType="EMail" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="JobResumeFile" RemoteMethod="False" ValidateType="None" CheckMethod="CheckStrWildWord" />
                                </Columns>
                            </JQTools:JQValidate>
                        </JQTools:JQDialog>
                    </td>

                </tr>
            </table>
        </div>
        <div id="Dialog_IndTree">
            <div class="div_RelativeLayout">
                <JQTools:JQTreeView ID="JQTreeView1" runat="server" DataMember="HUT_IndCategory" idField="ID" parentField="ParentID" RemoteName="sCustomersJobs.HUT_IndCategory" RootValue="0" textField="IndCategory" Height="100px" checkbox="true"></JQTools:JQTreeView>

            </div>
        </div>
        <div id="Dialog_JobLang">
            <div class="div_RelativeLayout">
                <JQTools:JQTreeView ID="JQTreeView2" runat="server" DataMember="HUT_EduSubject" idField="ID" parentField="ParentID" RemoteName="sCustomersJobs.HUT_EduSubject" RootValue="0" textField="SubjectName" Height="100px" checkbox="true"></JQTools:JQTreeView>
            </div>
        </div>
        <div id="tt" class="easyui-tabs" data-options="plain:true" style="height: 300px;">
        </div>
        <div id="qq" class="easyui-tabs" data-options="plain:true" style="height: 450px;">
        </div>
        <script src="../js/jquery.jbjob.js"></script>
        <script>
            $(function () {
                var MyArray1 = { 客戶資料: 4, 聯絡方式: 7, 福利出勤: 2, 召募重點: 1,複試重點:1 };
                //var MyArray2 = { 客戶資料: [1, 3], 聯絡人: [4, ], 服務設定: [5, 1] };
                $('#dataFormMaster').jbFormUISet({ TitleList: MyArray1, ID: 'tt', Type: 'tab' });
            });
            $(function () {
                var MyArray2 = { 職缺需求: 7, 語文需求: 7, 技能證照: 2, 人格特質與備註: 2, 職缺資訊: 3, 職缺薪資福利: 2, 推薦履歷聯絡人: 7};
                //var MyArray2 = { 公司組織: [1, 3], 聯絡人: [4, ], 服務設定: [5, 1] };
                $('#dataFormDetail').jbFormUISet({ TitleList: MyArray2, ID: 'qq', Type: 'tab' });
            });

        </script>
       

    </form>
</body>
</html>
