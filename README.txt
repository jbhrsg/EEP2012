EEP2012/JQuery Module (5.0.0.0.-SP4版本 2014/03/13)

更新方法 (*** 注意, 本更新版, 一定需事先安裝 SP3 的版本才能進行更新)
-------------------------------------------------------------------------
1. 將rar解壓后，覆蓋到EEP安裝的目錄(如C:\Program Files\Infolight\EEP2012)
2. 執行InitEEP.exe中的InstallGAC(一定要做,有更新InfoRemoteModule.dll)
3. 進入VS2012, 將Solution1重新建置所有Project。


2014/03/13版本README(SP4版本)
--------------------------------------------
* 'JQWebClient\Image\MenuTree\css'之下, 如果By userid沒有權限產生menu.css時,
  更正不會報錯,不顯示Icon就好。
* DataGrid開放CheckOnSelect屬性,預設為True,設定為False即可將選擇資料與CheckBox
  打勾分開控制。
* JQuery正式提供 EEPNetServer 自動啟動的機制, 使用時須配合EEPServerLoader的
  服務註冊。
* ReportViewer的Preview時, 如果希望直接以PDF來預覽, 請自行將
  ReportViewerTemplate.aspx.cs 中 line 41的Export() 這行恢復並重新編譯即可。
* InfoCombobox的EditorOptions增加一個屬性Checkdata=True/False,用來控制是否檢
  查Combobox所輸入的內容來與資料核對是否存在。
* RefVal的資料來源有用LeftJoin欄位且在RefVal查詢窗中按下OK時會出現錯誤訊息,
  必須在Refval的Columns中, 對LeftJoin的欄位設定Join的TableName解決此問題。
* 使用JQOptions元件，當Options使用Dialog模式時, onSelect事件沒有作用,已更正。
* DataGrid輸出Excel如果有Refval,如果grid欄位名稱與refval的Valuemember欄位名稱
  不同會報錯，已更正。
* 所有JQDataForm的Validate欄位Title預設將會以紅色來呈現, 可以用css來設定。
* InfoCommand的SQL編輯器打開時, 如果資料量很大需要等很久, 已經改善只抓取結構
  即可。
* ReportViewer(RDLC)中, 上面的"ReportParameter1"之WhereTextString內容不對,
  已經更正。
* ReportViewer輸出時, 資料只有100筆的PacketRecord, 已經更正為-1全部資料。
* infooptions新增selectonly屬性(True/False), True只能用選的, 無法Keyin資料
* InofOptions提供OnWhere事件, 讓Options的資料來源可以動態過濾.
* DefaultValue元件的DefaultValue屬性中帶有逗號[,] 則會出現錯誤。已更正。
* 多國語言元件按RefreshControl時彈出選擇的TableName視窗，此無法面對
  Master/Detail多檔的頁面上, 已經更正成自動取得TableName。
* DataGrid與DataForm中的RemoteName增加Solution Name的功能,如使用
  'Solution1@SBASIC.Customers'來代表指定Solution,此可用來整合多個Solution整合
  在同一個網頁上同時執行(不同頁籤)。
* RefVal如果是ReadOnly=True,會將右邊的Icon自動隱藏。
* Refval增加屬性ShowValueAndText=True/False,False為原來顯示DisplayMember,True
  時可同時顯示兩個值(中間以':'隔開), 如：ValueMember:DisplayMember。
* DataForm增加OnApplyed屬性, 在Apply成功之後可以執行該事件. (可以用
  getEditMode($(this)) == ʺupdatedʺ來判斷是否為更改的狀態下)
* Wizard在挑選ViewField後, 產生的DataGrid欄位還是會全部, 已經更正了。
* DataGrid增加ColumnHideable=True/False屬性,可以讓User控制欄位是否要隱藏或顯示
  (但此模式DataGrid欄位不宜太多, 此功能會自動取消水平捲軸, 會將欄位固定擠滿
   在DataGrid中)
* JQuery的DataForm增加一個屬性disapply=True/False(預設為False), True的時候,
   DataForm的Apply就不會去執行, 這樣可以自己設計DataForm的存檔機制。
* DataGrid與DataForm的Columns的Format功能, 如果遇到DateTime或Time的型態, 增加
  'HH:MM:SS'格式, 如'yyyy.mm.dd HH:MM:SS','YYY.mm.dd HH:MM'等等。 
------------------ (以下為 2014/01/28版本)
* JQuery的DataGrid中提供Record Copy的功能, 將目前筆資料Copy到另一筆新的一筆
  資料中,動作與"新增"一樣。此功能需自行在ToolItems中自行貼入一個Icon,並以
  "copyItem"這個方法來執行Copy功能。
* JQuery中, 提供了EEPServerLoader的服務機制, 可以自動啟動EEPNetServer.exe。
* JQScriptManager中, 會自動Render js\jquery.userdefine.js 這個js,讓開發者可
  以自由定義自己的外掛js程式。
* JQuery的FileUpload要能以OnBeforeUpload來改變Folder屬性, 已處理。
* JQDataForm增加ValidateStyle: Hint(預設),Dialog.Hint兩種,Hint使用原來的
  EasyUI的Validate,而Dialog改用Dialog報告方式來呈現Validate。
------------------ (以下為 2014/01/08版本)
* JQDataGrid如果使用Format:L,YES,NO顯示邏輯資料時,NO的部分不會顯示出來,
  已更正。
* 遇到RefVal欄位，如果欄位為key的話，ColumnMatch會無效, 已更正。
* JQuery在Chrome瀏覽器時，點選任何一張表單會出現錯誤訊息：Sysmsg.XML File
   Not Found；已更正。
* Wizard產生JQuery表單時，原本不支援資料來自不同種的資料庫(SQL、Oracle)，
  已經更正。
* Wizard產生JQuery表單時,DataGrid的欄位如果是數值型欄位,會自動設定為靠右
  對齊。
* 在JQuery的DataForm中如果User有輸入&符號時，刪除會發生錯誤,已更正。
* DataGrid增加OnInserted屬性,可以在新增後自行以這個事件來處理其他動作，
  如希望能再次更新資料等動作。
* Options欄位新增一個OpenDialog(True/False)屬性,如果為False會將選項Render
  在DataForm表格裡面。
* JQDataForm Column的屬性Alignment靠左、靠右無效，已更正。
* ComboGrid及Combox若是要手動keyin來查詢時，會查詢Display Member的值，已經
  更正為查詢DisplayMember與ValueMember值都會找。
* 在ClientInfo的變數中, 已將UserPara1與UserPara2兩個功用的自訂變數整合進去,
  讓Client與Server Method可以互通傳遞變數。
* Server端Wizard, Column後面的Description沒抓到D.D.來顯示,已更正。
* ComboGird與RefVal的Width改成預設為350，好讓下面的分頁Footer不要折行。
* JQValidate增加了3種常用的ValidateType，如IdCard(台灣身分證)/URL/EMail等。
* JQMultiLanguage在設計時按下Refresh, 會讓開發者輸入TableName,如果沒有輸入,
  D.D.就不以TableName來抓取(以ColumnName抓), 有TableName則按Table抓取D.D。
* JQDataForm欄位如果為Key且為Refval時，編輯狀態下沒有Disable掉，已更正。
* Validate的提示訊息，會將RefVal欄位的按鈕遮住，不容易操作，已經改善了。
* 新增一個JQSchedule元件, 規格如下:
  RemoteName:連到後端的服務名稱.
  TableName:連到後端行程表的資料表名稱.
  TitleField:行程表的標題欄位.
  DateField: 行程表日期欄位名稱,可以使用Date,DateTime,Varchar8:YYYYMMDD的欄位。
  DateToField: 行程表終止日期欄位名稱,有設定時代表DateField與DateToField為一個
    範圍區間的日期行程。
  TimeFromField: 行程表起始時間欄位名稱, 格式為HH:MM或HHMM兩種.
  TimeToField: 行程表終止時間欄位名稱, 格式為HH:MM或HHMM兩種.
  TipField: 提示的欄位名稱.
  開放OnClick,OnWhere事件,讓User自行開發.
  (注意: 本元件如果是IE瀏覽器只能用再IE 9.0以上的版本)
* JQuery的DateTimeBox的時間，更改資料時會被統一去除，可以在EditOptions時設定      ShowTimeSpinner=True,這樣就可以將時間資料保留下來。
* JQuery的Sysmsg.xml的下載方式優化, 已經可以提升所有需要Sysmsg.xml的傳輸速度。
* JQDataGrid的Pagination=False時,Column的Total功能將自動改用Grid的即時資料來
  進行Sum/Count/Average/Max/Min等動作。
* 提供一個JQuery的共用JS,如getClientInfo(variableName),只要傳入系統變數名稱,
  即可取得內容。如 getClientInfo('_usercode');
* ComboxBox內容如果為空時, 現在會出現 "--請選擇--" 的訊息。(僅DataForm有效)
* JQDataGrid的Column Format增加了"L,Checkbox"格式,會以Checkbox打勾來顯示該欄位
  的值,如Bit為True,數值為1,文字為'Y'或'y'時打勾。
* JQDataGrid增加RecordLock(自動鎖定)(bool)的功能, 當某個User先編輯時, 會在
  A/P Server上記錄該Table哪一筆已經進入編輯，另一個User在前一個User尚未存檔前
  是無法編輯的(會告知該資料正在編輯當中...)
* JQWebClient提供SingleSignOn的功能(類似EEPWebClient)。讓別的系統可以SignOn到
   EEP的JQuery頁面中。
* JQuery提供Windows Client SSOLogon的單一AD認證的登入方式, 使用時必須配合,
  EEPNetServer上的Login Mananger/AD Domain的設定, 已經登入到此AD的Client, 可以
  使用 JQWebClient/SSOLogon.aspx?Database=ERPS&Solution=Solution1 方式自動登入

JQuery Workflow的部分
--------------------------------------------
* 使用FLApprove時, 如果FLNavigatorMode設為"Modify",已經簽核過的User透過
  "經辦事項"打開時可以更改資料, 已經更正只有在待辦才可以更改。
* 表單以"退回到第一關"的方式退回後, 在重新審核會出錯, 也不應該可以再次執行
  暫停才對,已更正。
* 若待簽的關卡設定是Modify ,可以修改表單的內容, 如果由EMail通知所連結進來的
  表單是不能修改的, 已更正。
* 流程被退回到第一關或暫停之後無法使用'預覽'的功能, 已經更正。
* WF的XOML中增加EEPAlias屬性,來設定資料表的資料庫, 以解決WF的系統資料庫必須
  與單據的資料庫一致的問題。
* JQuery WF的加簽功能中,增加一個'使用者'頁籤, 可以加簽給使用者。
------------------ (以下為2014/01/08版本)
* '一起審核'功能執行後，主畫面"首頁"的Tab，會變成"批示意見",已更正。
* JQuery中, FLApprove的屬性PlusApproveReturn設為True，沒有等加簽回來就可以往下
  呈送，已更正。
* JQuery Approve的畫面中, 針對"簽核意見"已經按照時間排序(小到大)。
* JQuery的經辦中,'催單'的畫面有簡體字，應支援多國語言，已更正。
* JQuery的 上呈/審核/退回/暫停/加簽/通知/作廢 等功能,如果DataForm在編輯狀態下,
  會提出警告"資料尚未存檔, 不要去執行流程動作.."
  sysmsg.xml喔)
* "整批退回"中退回單據，如果有狀態為Continue的單據，會警告該單據無法退回。
* JQuery按下"作廢"按鈕後,出現"作廢成功"後，會將那個DataForm關掉才不會讓User有
  機會按"確定"存檔。
* 經辦事項畫面中，點選已結案後，下拉用來選擇流程的ComboBox會沒有資料，已更正。
* DataForm的EditMode如果為Contiue和Expand, 在Workflow中執行"Submit"時會警告
  "無法使用Continue或Expand模式來呈送Flow單據"。
* JQuery在待辦事項中打開Web表單時, 都是當作JQuery表單打開,現在可在FLDesigner
  中設定流程為"WEB.xxxxx"開頭的表單時代表示Asp.net的Web表單，而非JQuery表單,
  這樣就可以在JQuery的待辦中,同時使用JQuery表單與Asp的Web表單。


JQuery Mobile的部分
--------------------------------------------
* Wizard中(Single與Master都一樣), 已將ViewField那頁獨立選擇DataGrid上的欄位,  
  正式與DataForm個別獨立設定。
* DataGrid增加AlwaysClose屬性來控制是否有資料。
* CheckBox多選存檔後,重新Edit無法定位到原來多選的勾選項目,已經更正。
* DataGrid的QueryColumns中, 請開放Editor與EditorOptions屬性,
* DataGrid增加一個GridViewStyle,原有為"Grid"的Style,多了一個"List"的Style, 
  前者在寬螢幕為Grid,窄螢幕為FormList; 後者寬螢幕為ListColumnCount所控制, 
  窄螢幕會自動將ListColumnCount自動設為1。
* DataGrid/QueryColumn當中, 如果Editor為Selects, 一定要選，已更正。
* DataGrid的Columns中如果使用到RelationOptions的關聯設定時, 會多一個Select
  Count(*)的指令, 影響效能, 已更正。
* 呈送/審核中的'預覽'沒有作用, 已經加上去了。
* 工作流程送出時, 如果後端報錯不會反映Error Message, 已經更正了。
* 主畫面的'個人事項'中, 待辦/經辦/通知/逾時頁籤上增加了數量的顯示。
* 呈送/審核中上傳文件的窗體Caption不正確, 應該為"上傳",非審核。
* 經辦中按下'催單'會報錯, 已經更正。
* 通知中按下'刪除通知'會確認兩次, 已經更正。

