<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_Employee_Management.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jquery.blockUI.js"></script>
    <title>員工資料維護</title>
</head>
<body>
    <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
    <script type="text/javascript">
        var DataGrid_Base_ID = "#DataGrid_Base";
        var Dialog_Base_ID = '#Dialog_Base';
        var DataForm_Base_ID = '#DataForm_Base';
        var Tab_Management_ID = '#Tab_Management';
        var Dialog_BaseLog_ID = '#Dialog_BaseLog';
        var DataForm_BaseLog_ID = '#DataForm_BaseLog';
        var DataGrid_BaseLog_ID = '#DataGrid_BaseLog';
        var TreeView_BaseIO_ID = '#TreeView_BaseIO';
        var TreeView_Dept_ID = '#TreeView_Dept';
        //=======================================【Ready】=========================================
        $(function () {
            if ($(Dialog_Base_ID).hasClass('easyui-dialog')) {
                $(Dialog_Base_ID).dialog({ onClose: function () { $(DataGrid_Base_ID).datagrid('reload'); } });
            }
            //-----------------------------------LogDialog整形------------------------------------
            $(Dialog_Base_ID).jbDialogPlugin();
            $(Dialog_BaseLog_ID).jbDialogPlugin();
            //-----------------------------------篩選資料-----------------------------------------            
            (function () {
                //=====在職篩選=====
                var KeyWord_BaseIO = 'baseIO';
                $(TreeView_BaseIO_ID).tree({
                    onSelect: function (node) {
                        var filterWhereStr = String.format("BIO.ACTION_TYPE='{0}'", node.id);
                        $(DataGrid_Base_ID).datagrid('jbSetFilter', { KeyWord_BaseIO: filterWhereStr }).datagrid('jbRunFilter');
                    },
                    onBeforeSelect: function (node) {
                        var selectedNode = $(this).tree('getSelected');
                        if (selectedNode != null && selectedNode.id == node.id) {
                            $(node.target).removeClass("tree-node-selected");
                            $(DataGrid_Base_ID).datagrid('jbSetFilter', { KeyWord_BaseIO: '' }).datagrid('jbRunFilter');
                            return false;
                        }
                        return true;
                    }
                });
                //=====部門篩選=====
                var KeyWord_Dept = 'dept';
                $(TreeView_Dept_ID).tree({
                    onSelect: function (node) {
                        var filterWhereStr = String.format("BTS.DEPT_ID='{0}'", node.id);
                        $(DataGrid_Base_ID).datagrid('jbSetFilter', { KeyWord_Dept: filterWhereStr }).datagrid('jbRunFilter');
                    },
                    onBeforeSelect: function (node) {
                        var selectedNode = $(this).tree('getSelected');
                        if (selectedNode != null && selectedNode.id == node.id) {
                            $(node.target).removeClass("tree-node-selected");
                            $(DataGrid_Base_ID).datagrid('jbSetFilter', { KeyWord_Dept: '' }).datagrid('jbRunFilter');
                            return false;
                        }
                        return true;
                    },
                    onLoadSuccess: function () {
                        $(this).tree('collapseAll');
                    }
                });
            })();
            //-------------------------------------------------------------------------------------
        });
        //=========================================================================================
        //-----------------------------------Grid載入完成-----------------------------------------
        var DataGrid_Base_OnLoadSuccess = function () {
            var dgid = $(this);
            //第一次載入Grid用
            if (!dgid.data('alreadyFirstLoad') && dgid.data('alreadyFirstLoad', true)) {
                //首先判斷頁面
                var ID = Request.getQueryStringByName("ID");
                if (ID) {
                    var defaultWhereStr = String.format("BAS.EMPLOYEE_ID='{0}'", ID);
                    $(DataGrid_Base_ID).datagrid('jbSetFilter', { 'EmployeeID': defaultWhereStr }).datagrid('jbRunFilter');
                }
                else {
                    //選定在職人員
                    $(TreeView_BaseIO_ID).tree('select', $(TreeView_BaseIO_ID).tree('getRoot').target);
                }

            }
        }
        //---------------------------------------資料刪除前---------------------------------------
        var DataGrid_Base_OnDelete = function (rowData) {
            var Ans = false;
            $.ajaxSetup({ async: false });
            $.post('../handler/JqDataHandle.ashx?RemoteName=_HRM_Employee_Management', { mode: 'method', method: 'DeleteValidate', parameters: rowData.EMPLOYEE_ID }
                  ).done(function (data) {
                      var Json = $.parseJSON(data);
                      if (Json.IsOK) Ans = true;
                      else alert("此筆資料已有相關聯的使用資料, 無法刪除");
                  }).fail(function (xhr, textStatus, errorThrown) {
                      alert('error');
                  });
            $.ajaxSetup({ async: true });
            return Ans;
        }
        //---------------------------------------Grid欄位FormatScript-----------------------------
        var DataGrid_Base_FormatScript = function (value, row, index) {
            var fieldName = this.field;
            switch (fieldName) {
                case 'TRANSLOG':
                    return $('<a>', { href: 'javascript: void(0)', onclick: "DataGrid_Base_CommandTrigger.call(this,'" + fieldName + "')" }).
                            html('異動資料記錄')[0].outerHTML;
                    break;
                default:
                    return '';
                    break;
            }
        }
        //---------------------------------------Grid欄位CommandTrigger---------------------------
        var DataGrid_Base_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
            var RowData = $(DataGrid_Base_ID).datagrid('selectRow', rowIndex).datagrid('getSelected');
            switch (command) {
                case 'TRANSLOG':
                    openForm(Dialog_BaseLog_ID, RowData, 'viewed', 'Dialog');
                    break;
                default:
                    break;
            }
        }
        //---------------------------------------Form載入之後-------------------------------------
        var DataForm_OnLoadSuccess = function (RowData) {
            var defaultWhereStr = '';
            var theGrid = '';

            var thisDataForm = $(this);
            var form_ID = '#' + thisDataForm.attr('id');
            switch (form_ID) {
                case DataForm_BaseLog_ID:
                    defaultWhereStr = String.format("BAS.EMPLOYEE_ID='{0}'", RowData.EMPLOYEE_ID);
                    theGrid = DataGrid_BaseLog_ID;
                    break;
                case DataForm_Base_ID:
                    var TabIndexList = [];
                    if (RowData.EMPLOYEE_ID) {
                        //Tab方法
                        $(Tab_Management_ID).tabs({
                            onSelect: function (title, index) {
                                if ($.inArray(index, TabIndexList) == -1) {
                                    if (TabSelectedLoad.call($(this).tabs('getSelected'), RowData.EMPLOYEE_ID)) TabIndexList.push(index);
                                }
                            }
                        }).tabs('select', 0);
                    }
                    break;
                default:
                    break;
            }
            if (theGrid && defaultWhereStr) $(theGrid).data('defaultWhereStr', defaultWhereStr).datagrid("setWhere", defaultWhereStr);
        }
        //---------------------------------------Tab頁面載入方法----------------------------------
        var TabSelectedLoad = function (ID) {
            var theTab = $(this);
            var Iframe = theTab.find('iframe');
            if (Iframe) {
                var url = Iframe.data('src');
                if (url) {
                    url = url + '?' + $.param({ 'ID': ID });
                    theTab.block({ message: 'Loading.....', css: { border: 'none', padding: '15px', backgroundColor: '#fff', '-webkit-border-radius': '10px', '-moz-border-radius': '10px', opacity: .3, color: '#000' } });
                    Iframe.attr('src', url).load(function () { theTab.unblock(); });
                    return true;
                } else Iframe.attr('src', 'about:blank');
            }
            return false;
        }
        //---------------------------------------改寫查詢-----------------------------------------
        function queryGrid(dg) {
            var where = $(dg).datagrid('getWhere');
            $(dg).datagrid('jbRunFilter', where);
        }
        //---------------------------------------擴充Grid-----------------------------------------
        ; (function ($) {
            var filterObjName = 'jbFilterObj';

            var jbGetFilter = function (target) {
                var theObj = target.data(filterObjName);
                if (theObj == undefined || theObj == null) return {};
                else return theObj;
            }

            var jbSetFilter = function (target, newObj) {
                target.data(filterObjName, $.extend(jbGetFilter(target), newObj));
            }

            var jbRunFilter = function (target, setWhere) {
                var setWhereArray = [];

                var filterSetWhere = $.map(jbGetFilter(target), function (value, index) {
                    return ($.type(value) == "string" && value.length > 0) ? value : null;
                }).join(' and ');

                if (filterSetWhere.length > 0) setWhereArray.push(filterSetWhere);

                if ($.type(setWhere) == "string" && setWhere.length > 0) setWhereArray.push(setWhere);

                target.datagrid('setWhere', setWhereArray.join(' and '));
            }

            $.extend($.fn.datagrid.methods, {
                jbSetFilter: function (target, newObj) {
                    target.each(function () {
                        jbSetFilter($(this), newObj);
                    });
                    return target;
                },
                jbGetFilter: function (target) {
                    return jbGetFilter($(target[0]));
                },
                jbRunFilter: function (target, setWhere) {
                    target.each(function () {
                        jbRunFilter($(this), setWhere);
                    });
                    return target;
                }
            });
        })(jQuery);
        //-----------------------------------------------------------------------------------------
    </script>
    <form id="form1" runat="server">
        <JQTools:JQMultiLanguage ID="JQMultiLanguage1" runat="server" />
        <JQTools:JQImageContainer ID="JQImageContainer1" runat="server" Width="150"></JQTools:JQImageContainer>

        <JQTools:JQDialog ID="Dialog_Base" runat="server" BindingObjectID="DataForm_Base" Title="個人資料管理" ShowModal="True" EditMode="Dialog" DialogLeft="" DialogTop="0" Width="1000" ShowSubmitDiv="False">
            <JQTools:JQDataForm ID="DataForm_Base" runat="server" DataMember="HRM_BASE_BASE" HorizontalColumnsCount="2" RemoteName="_HRM_Employee_Management.HRM_BASE_BASE" OnLoadSuccess="DataForm_OnLoadSuccess">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="員工編碼" Editor="text" FieldName="EMPLOYEE_ID" Visible="False" Width="80" />
                    <JQTools:JQFormColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EMPLOYEE_CODE" Width="140" ReadOnly="true" />
                    <JQTools:JQFormColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" Width="140" ReadOnly="true" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQTab ID="Tab_Management" runat="server" Height="400">
                <JQTools:JQTabItem ID="JQTabItem1" runat="server" Title="基本資料">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="HRM_Employee_Edit.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem2" runat="server" Title="通訊資料">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="HRM_Employee_Base_EmployeeAddr_Single.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem3" runat="server" Title="職務異動">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="HRM_Employee_Transfer_Basetts.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem4" runat="server" Title="學歷資料">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="HRM_Employee_Base_EmployeeEducational_Single.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem5" runat="server" Title="經歷資料">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="HRM_Employee_Base_EmployeeExperience_Single.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem6" runat="server" Title="眷屬資料">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="HRM_Employee_Base_EmployeeFamily_Single.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem7" runat="server" Title="證照資料">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="HRM_Employee_Base_EmployeeLicence_Single.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem8" runat="server" Title="獎懲資料">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="HRM_Employee_Base_EmployeeReward_Single.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem9" runat="server" Title="專長資料">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="HRM_Employee_Base_EmployeeSkill_Single.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem10" runat="server" Title="合約資料">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="HRM_Employee_Base_EmployeeContract_Single.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem11" runat="server" Title="外籍員工居留資料">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="HRM_Employee_Base_EmployeeResident_Single.aspx"></iframe>
                </JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem12" runat="server" Title="帳戶資料">
                    <iframe style="width: 100%; height: 98%; border: 0" data-src="HRM_Employee_Base_EmployeeAccount.aspx"></iframe>
                </JQTools:JQTabItem>
            </JQTools:JQTab>
        </JQTools:JQDialog>

        <div id="Layout_Base" class="easyui-layout" style="height: 385px;">
            <div data-options="region:'west',split:true,border:false" title="篩選" style="width: 195px;">
                <div class="easyui-layout" style="height: 350px;">
                    <div data-options="region:'north',split:true,border:false" title="現職狀態" style="height: 86px;">
                        <JQTools:JQTreeView ID="TreeView_BaseIO" runat="server" DataMember="cb_S_ACTION_TYPE" idField="CODE" parentField="" RemoteName="_HRM_Employee_Management.cb_S_ACTION_TYPE" textField="NAME"></JQTools:JQTreeView>
                    </div>
                    <div data-options="region:'center',title:'',border:false" title="編制部門">
                        <JQTools:JQTreeView ID="TreeView_Dept" runat="server" DataMember="HRM_DEPT" idField="DEPT_ID" parentField="UPPER_DEPT_ID" RemoteName="_HRM_Employee_Management.HRM_DEPT" textField="DEPT_CNAME"></JQTools:JQTreeView>
                    </div>
                </div>
            </div>
            <div data-options="region:'center',title:'',border:false" title="">
                <JQTools:JQDataGrid ID="DataGrid_Base" data-options="pagination:true,view:commandview" RemoteName="_HRM_Employee_Management.HRM_BASE_BASE" runat="server" AutoApply="True"
                    DataMember="HRM_BASE_BASE" Pagination="True" QueryTitle="查詢" EditDialogID="Dialog_Base"
                    Title="員工資料管理" OnDelete="DataGrid_Base_OnDelete" OnLoadSuccess="DataGrid_Base_OnLoadSuccess" AllowAdd="False" AlwaysClose="True" AllowUpdate="False" QueryLeft="300" QueryTop="100" Height="381px">
                    <Columns>
                        <%--<JQTools:JQGridColumn Alignment="left" Caption="照片" Editor="text" FieldName="PHOTO" Width="50" Format="image,folder:Files/UploadFile/JBHRIS/HRM_BASE_BASE/PHOTO,height:30,width:30" Frozen="True" />--%>
                        <JQTools:JQGridColumn Alignment="left" Caption="到職日期" Editor="datebox" FieldName="ARRIVE_DATE" Sortable="True" Visible="True" Width="60" Format="yyyy/mm/dd" Frozen="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="狀態" Editor="text" FieldName="ACTION_TYPE_NAME" Width="60" Sortable="True" Frozen="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EMPLOYEE_CODE" MaxLength="50" Width="60" Sortable="True" Frozen="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" MaxLength="50" Width="60" Sortable="True" Frozen="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="英文名字" Editor="text" FieldName="NAME_E" MaxLength="50" Width="100" Sortable="True" Frozen="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="性別" Editor="text" FieldName="SEX_NAME" Frozen="True" Sortable="True" Visible="True" Width="40" />

                        <JQTools:JQGridColumn Alignment="left" Caption="Mail" Editor="text" FieldName="COMPANY_MAIL" MaxLength="50" Width="150" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="身分證" Editor="text" FieldName="IDNO" Sortable="True" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="出生日期" Editor="datebox" FieldName="BIRTHDAY" Sortable="True" Visible="True" Width="60" Format="yyyy/mm/dd" />
                        <JQTools:JQGridColumn Alignment="left" Caption="婚姻" Editor="text" FieldName="MARRIAGE_NAME" Sortable="True" Visible="True" Width="50" />
                        <JQTools:JQGridColumn Alignment="left" Caption="血型" Editor="text" FieldName="BLOOD_NAME" Sortable="True" Visible="True" Width="40" />
                        <JQTools:JQGridColumn Alignment="left" Caption="國別" Editor="text" FieldName="COUNTRY_ID_NAME" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="籍貫" Editor="text" FieldName="PROVINCE_CNAME" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="身高" Editor="text" FieldName="HEIGHT" Sortable="True" Visible="True" Width="50" />
                        <JQTools:JQGridColumn Alignment="left" Caption="體重" Editor="text" FieldName="WEIGHT" Sortable="True" Visible="True" Width="50" />
                        <JQTools:JQGridColumn Alignment="left" Caption="兵役" Editor="text" FieldName="ARMY" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="兵種" Editor="text" FieldName="ARMY_TYPE" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="護照號碼" Editor="text" FieldName="PASSPORT_NUMBER" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="護照名稱" Editor="text" FieldName="PASSPORT_NAME" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="居留證號" Editor="text" FieldName="RESIDENT_CERTIFICATE" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="外籍類別" Editor="text" FieldName="ALIEN_RESIDENT_TYPE" Sortable="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="錄取管道" Editor="text" FieldName="HIRE_WAY_ID_NAME" Sortable="True" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="集團到職日" Editor="datebox" FieldName="GROUP_EFFECT_DATE" Sortable="True" Visible="True" Width="60" Format="yyyy/mm/dd" />
                        <JQTools:JQGridColumn Alignment="left" Caption="外部年資" Editor="text" FieldName="EXTERNAL_SENIORITY" Sortable="True" Visible="True" Width="50" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Sortable="True" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Sortable="True" Visible="True" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Sortable="True" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Sortable="True" Visible="True" Width="120" />

                        <JQTools:JQGridColumn Alignment="left" Caption="異動資料記錄" Editor="text" FieldName="TRANSLOG" FormatScript="DataGrid_Base_FormatScript" ReadOnly="True" Sortable="False" Visible="True" Width="80" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                        <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                        <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                        <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                        <JQTools:JQToolItem ID="DataGrid_Base_ImportExcel" Icon="icon-importExcel" ItemType="easyui-linkbutton" OnClick="openImportExcel" Text="匯入EXCEL" />
                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" FieldName="EMPLOYEE_CODE" Caption="員工編號" Condition="%%" DataType="string" Editor="text" NewLine="True" RemoteMethod="False" Width="125" />
                        <JQTools:JQQueryColumn AndOr="and" FieldName="NAME_C" Caption="員工姓名" Condition="%%" DataType="string" Editor="text" NewLine="False" RemoteMethod="False" Width="125" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
            </div>
        </div>

        

        <JQTools:JQDialog ID="Dialog_BaseLog" runat="server" BindingObjectID="DataForm_BaseLog" Title="員工基本資料異動資料記錄" ShowModal="True" EditMode="Dialog" Width="650px" DialogTop="100" DialogLeft="">
            <div style="display: none;">
                <JQTools:JQDataForm ID="DataForm_BaseLog" runat="server" DataMember="HRM_BASE_BASE" RemoteName="_HRM_Employee_Management.HRM_BASE_BASE" OnLoadSuccess="DataForm_OnLoadSuccess">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="員工編碼" Editor="text" FieldName="EMPLOYEE_ID" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
            </div>
            <JQTools:JQDataGrid ID="DataGrid_BaseLog" data-options="pagination:true,view:commandview" RemoteName="_HRM_Employee_Management.HRM_BASE_BASE_LOG" runat="server" AutoApply="False"
                DataMember="HRM_BASE_BASE_LOG" Pagination="True" QueryTitle="查詢"
                Title="" ColumnsHibeable="False" QueryMode="Window" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="300px" QueryTop="100px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ReportFileName="">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="異動日期" Editor="text" FieldName="LOG_DATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="True" Sortable="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動人員" Editor="text" FieldName="LOG_USER_NAME" Frozen="True" Sortable="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="text" FieldName="LOG_STATE_NAME" Frozen="True" Sortable="True" Width="60" />

                    <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EMPLOYEE_CODE" MaxLength="50" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" MaxLength="50" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="英文名字" Editor="text" FieldName="NAME_E" MaxLength="50" Width="100" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="性別" Editor="text" FieldName="SEX_NAME" Sortable="True" Visible="True" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Mail" Editor="text" FieldName="COMPANY_MAIL" MaxLength="50" Width="150" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="身分證" Editor="text" FieldName="IDNO" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="出生日期" Editor="datebox" FieldName="BIRTHDAY" Sortable="True" Visible="True" Width="60" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="婚姻" Editor="text" FieldName="MARRIAGE_NAME" Sortable="True" Visible="True" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="血型" Editor="text" FieldName="BLOOD_NAME" Sortable="True" Visible="True" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="國別" Editor="text" FieldName="COUNTRY_ID_NAME" Sortable="True" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="籍貫" Editor="text" FieldName="PROVINCE_CNAME" Sortable="True" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="身高" Editor="text" FieldName="HEIGHT" Sortable="True" Visible="True" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="體重" Editor="text" FieldName="WEIGHT" Sortable="True" Visible="True" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="兵役" Editor="text" FieldName="ARMY" Sortable="True" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="兵種" Editor="text" FieldName="ARMY_TYPE" Sortable="True" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="護照號碼" Editor="text" FieldName="PASSPORT_NUMBER" Sortable="True" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="護照名稱" Editor="text" FieldName="PASSPORT_NAME" Sortable="True" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="居留證號" Editor="text" FieldName="RESIDENT_CERTIFICATE" Sortable="True" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="外籍類別" Editor="text" FieldName="ALIEN_RESIDENT_TYPE" Sortable="True" Visible="True" Width="60" />

                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CREATE_MAN" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CREATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Sortable="True" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="UPDATE_MAN" Sortable="True" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="UPDATE_DATE" Format="yyyy/mm/dd HH:MM:SS" Sortable="True" Visible="True" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" />
                </TooItems>
            </JQTools:JQDataGrid>
        </JQTools:JQDialog>

        <!-- 匯入對話框內容的 DIV -->
        <div id="Dialog_Import"></div>
        <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog">
            <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" RemoteName="_HRM_Employee_Management.HRM_BASE_BASE" DataMember="HRM_BASE_BASE" HorizontalColumnsCount="3" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="檔案名稱" Editor="text" FieldName="FilePathName" Width="80" ReadOnly="true" Visible="false" />
                    <JQTools:JQFormColumn Alignment="left" Caption="員工編號" Editor="infocombobox" FieldName="EMPLOYEE_CODE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="員工姓名" Editor="infocombobox" FieldName="NAME_C" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="英文名字" Editor="infocombobox" FieldName="NAME_E" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infocombobox" FieldName="SEX" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="Mail" Editor="infocombobox" FieldName="COMPANY_MAIL" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="身分證" Editor="infocombobox" FieldName="IDNO" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="出生日期" Editor="infocombobox" FieldName="BIRTHDAY" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="婚姻" Editor="infocombobox" FieldName="MARRIAGE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="血型" Editor="infocombobox" FieldName="BLOOD" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="國別" Editor="infocombobox" FieldName="COUNTRY_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="籍貫" Editor="infocombobox" FieldName="BIRTHPLACE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="身高" Editor="infocombobox" FieldName="HEIGHT" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="體重" Editor="infocombobox" FieldName="WEIGHT" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="兵役" Editor="infocombobox" FieldName="ARMY" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="兵種" Editor="infocombobox" FieldName="ARMY_TYPE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="護照號碼" Editor="infocombobox" FieldName="PASSPORT_NUMBER" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="護照名稱" Editor="infocombobox" FieldName="PASSPORT_NAME" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="居留證號" Editor="infocombobox" FieldName="RESIDENT_CERTIFICATE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="外籍類別" Editor="infocombobox" FieldName="ALIEN_RESIDENT_TYPE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="到職日期" Editor="infocombobox" FieldName="EFFECT_DATE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="錄取管道" Editor="infocombobox" FieldName="HIRE_WAY_ID" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="lefzt" Caption="集團到職日" Editor="infocombobox" FieldName="GROUP_EFFECT_DATE" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQFormColumn Alignment="left" Caption="外部年資" Editor="infocombobox" FieldName="EXTERNAL_SENIORITY" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EMPLOYEE_CODE" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="COUNTRY_ID" RemoteMethod="True" ValidateType="None" />

                </Columns>
            </JQTools:JQValidate>
        </JQTools:JQDialog>
        <script>
            $(document).ready(function () {
                //-------------------------------欄位配對視窗送出按鈕------------------------------
                $('#DialogSubmit', '#Dialog_ImportMain').removeAttr('onclick').on('click', function () {
                    if (!$('#DataForm_ImportMain').form('validateForm')) return;    //驗證                    
                    var data = $('#DataForm_ImportMain').jbDataFormGetAFormData();  //取資料
                    $.messager.progress({ msg: 'Loading...' });                 //進度條開始
                    //送出上傳動作
                    $.ajaxSetup({ async: false });
                    $.post('../handler/JqDataHandle.ashx?RemoteName=_HRM_Employee_Management', {
                        mode: 'method', method: 'FileUpload', parameters: $.toJSONString(data)
                    }).done(function (data) {
                        var Json = $.parseJSON(data);
                        if (Json.IsOK) {
                            $.messager.alert(' ', "匯入成功");
                            $('#Dialog_Import').dialog('close');
                            $('#Dialog_ImportMain').dialog('close');
                            $('#dataGridMaster').datagrid('reload');
                        }
                        else {
                            var html = Json.ErrorMsg;
                            if (Json.Result) {
                                var url = '../handler/JBHRISHandler.ashx?';
                                var querystrig = $.param({ mode: 'FileDownload', FilePathName: encodeURIComponent(Json.Result), DownloadName: encodeURIComponent('修正檔案') });
                                html = html + $('<a>', { href: url + querystrig, target: '_blank' }).html('檔案下載')[0].outerHTML;
                            }
                            $.messager.alert(' ', html);
                            $('#Dialog_ImportMain').dialog('close');
                        }
                    }).fail(function (xhr, textStatus, errorThrown) {
                        alert('error');
                    }).always(function () {
                        $.messager.progress('close');                           //進度條結束
                    });
                });
                //-------------------------------讀取ExcelJquery----------------------------------
                $('#Dialog_Import').jbImportExcel({
                    OnGetTitleSuccess: function (ArrayData, FilePathName) {
                        //開啟配對視窗
                        openForm('#Dialog_ImportMain', { FilePathName: FilePathName }, 'inserted', 'Dialog');
                        //載入選項以及預設
                        $('#DataForm_ImportMain').find('.info-combobox').each(function () {
                            $(this).combobox('loadData', ArrayData).combobox('clear');
                            $(this).combobox('selectExistsForText', $(this).closest('td').prev('td').html());
                        });
                    }
                });
            })
            function openImportExcel() {
                $("#Dialog_Import").dialog("open");
            }
        </script>
    </form>
</body>
</html>
