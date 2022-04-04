<%@ WebHandler Language="C#" Class="JbExcelHandler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.SessionState;
using Newtonsoft.Json;
using JBTool;

public class JbExcelHandler : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        SetTheTempFileSavePath();//從Config設定檔案儲存路徑

        var mode = context.Request["mode"];

        if (mode == "ExcelFileGetTitle") ExcelFileGetTitle(context);
        else if (mode == "ExcelFileImport") ExcelFileImport(context);
        else if (mode == "FileDownload") FileDownload(context);

        else if (mode == "ExcelFileSave") ExcelFileSave(context);
        else if (mode == "ExcelFileGetSheetTitle") ExcelFileGetSheetTitle(context);
        else if (mode == "ExcelFileSheetImport") ExcelFileSheetImport(context);
        else if (mode == "CallServerMethod") CallServerMethod(context);
    }

    //從Config設定檔案儲存路徑
    private void SetTheTempFileSavePath()
    {
        string AppSettingName = "JBTempFileSavePath";
        var AppSettings = System.Configuration.ConfigurationManager.AppSettings;
        if (!string.IsNullOrEmpty(AppSettings[AppSettingName])) this.TempFileSavePath = AppSettings[AppSettingName];
    }

    //存檔路徑
    private string TempFileSavePath = @"../Files/JBHRIS/Temp";

    /// <summary>取得儲存的檔案路徑
    /// <para>檢查員路徑下是否有重複檔案，如果有會自動更名</para>
    /// </summary>
    /// <param name="FilePath">原檔案路徑</param>        
    private string GetSaveFileNamePath(string FilePath)
    {
        string DirectoryName = Path.GetDirectoryName(FilePath);
        string FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FilePath);
        string Extension = Path.GetExtension(FilePath);

        string CheckFileNamePath = FilePath;
        for (int i = 0; File.Exists(CheckFileNamePath); i++)
            CheckFileNamePath = Path.Combine(DirectoryName, string.Format("{0}{1}{2}", FileNameWithoutExtension, i, Extension));

        return Path.GetFileName(CheckFileNamePath);
    }

    /// <summary>檔案資料寫入</summary>
    /// <param name="FileName">檔案名稱</param>
    /// <param name="aMemoryStream">寫入資料</param>
    /// <returns>存檔後檔案路徑(名稱)</returns>
    private string SaveTheFile(string FileName, Stream aStream)
    {
        //確認路徑目錄
        var MapPath = HttpContext.Current.Server.MapPath(Path.Combine(TempFileSavePath, FileName));
        string DirectoryPath = Path.GetDirectoryName(MapPath);
        if (!System.IO.Directory.Exists(DirectoryPath)) System.IO.Directory.CreateDirectory(DirectoryPath);

        //確定儲存的檔案名稱
        FileName = GetSaveFileNamePath(MapPath);
        MapPath = HttpContext.Current.Server.MapPath(Path.Combine(TempFileSavePath, FileName));

        //進行存檔
        using (FileStream file = new FileStream(MapPath, FileMode.Create, FileAccess.Write))
        {
            aStream.CopyTo(file);
            aStream.Close();
        }

        //回傳可能變更後的檔案+路徑
        return TempFileSavePath + "/" + FileName;
    }

    //讀取檔案
    private byte[] Read2Byte(Stream input)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            input.CopyTo(ms);
            ms.Flush();
            return ms.ToArray();
        }
    }

    //取得Excel表頭資料
    private void ExcelFileGetTitle(HttpContext context)
    {
        //回傳結果
        var theResult = new JBTool.TheJsonResult
        {
            IsOK = false,
            Result = "",
            ErrorMsg = ""
        };

        try
        {
            //檔案處理
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile theFile = context.Request.Files[0];
                var bytes = Read2Byte(theFile.InputStream);
                var HeadList = NPOIHelper.GetHeadRowList(new MemoryStream(bytes));
                if (HeadList != null)
                {
                    //檔案名稱
                    string FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    FileName = SaveTheFile(FileName, new MemoryStream(bytes));

                    //等待回傳
                    theResult.IsOK = true;
                    theResult.ErrorMsg = FileName;
                    theResult.Result = HeadList.Select(m => new { text = m.Key, value = m.Value }).OrderBy(m => m.value).ToList();
                }
                else theResult.ErrorMsg = "檔案處理錯誤";
            }
            else theResult.ErrorMsg = "找不到檔案";
        }
        catch (Exception) { theResult.ErrorMsg = "執行錯誤"; }

        context.Response.ContentType = "text/plain";
        context.Response.Write(theResult.ToJsonString());
    }

    /// <summary>檔案讀取</summary>
    private MemoryStream ReadTheFile(string FileName)
    {
        //確認路徑目錄
        var MapPath = HttpContext.Current.Server.MapPath(FileName);

        //進行存檔
        using (FileStream file = new FileStream(MapPath, FileMode.Open, FileAccess.ReadWrite))
        {
            MemoryStream aMemoryStream = new MemoryStream();
            file.CopyTo(aMemoryStream);
            aMemoryStream.Seek(0, SeekOrigin.Begin);
            return aMemoryStream;
        }

    }

    //讀檔案傳參數進DLL
    private void ExcelFileImport(HttpContext context)
    {
        var jdo = new JqDataObject(context.Request["remoteName"]);
        var methodName = context.Request["method"];
        var fileName = context.Request["fileName"];

        //方法參數
        var parameters = new List<object>();

        //字典檔
        var aDictionary = new Dictionary<string, object>();
        if (context.Request.Form.AllKeys.Contains("handler")) aDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(context.Request.Form["handler"]);
        parameters.Add(JBTool.HandlerHelper.SerializeObject(aDictionary));

        //讀取檔案
        parameters.Add(JBTool.HandlerHelper.SerializeObject(ReadTheFile(fileName)));

        //讀取檔案        
        string ParameterStr = "";
        if (context.Request.Form.AllKeys.Contains("parameters")) ParameterStr = context.Request.Form["parameters"].ToString();
        parameters.Add(ParameterStr);

        //執行方法與參數
        /* 1.IsOK
         * 2.Msg
         * 3.File
         */
        var ActionResult = (Dictionary<string, object>)JBTool.HandlerHelper.DeserializeObject(jdo.CallMethod(methodName, parameters).ToString());

        var theResult = new JBTool.TheJsonResult();
        if (!(bool)ActionResult["IsOK"])
        {
            theResult.IsOK = false;
            theResult.ErrorMsg = ActionResult["Msg"].ToString();

            if (ActionResult.ContainsKey("File"))
            {
                //錯誤資料
                var FileMemoryStream = (MemoryStream)ActionResult["File"];
                if (FileMemoryStream != null)
                {
                    string FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    FileName = SaveTheFile(FileName, FileMemoryStream);
                    theResult.Result = FileName;
                }
            }
        }
        else
        {
            theResult.IsOK = true;
            theResult.Result = ActionResult["Msg"].ToString();
        }

        context.Response.ContentType = "text/plain";
        context.Response.Write(theResult.ToJsonString());
    }

    //檔案下載
    private void FileDownload(HttpContext context)
    {
        var FilePathName = HttpUtility.UrlDecode(context.Request.QueryString["FilePathName"] ?? "");
        var FilePath = HttpContext.Current.Server.MapPath(FilePathName);
        System.IO.FileInfo aFile = new System.IO.FileInfo(FilePath);
        if (!aFile.Exists)
        {
            context.Response.ContentType = "text/html";
            context.Response.Write("<script type='text/javascript'>alert('檔案下載失敗');</script>");
        }
        else
        {
            var DownloadName = HttpUtility.UrlDecode(context.Request.QueryString["DownloadName"] ?? "");
            DownloadName = DownloadName == "" ? aFile.Name : DownloadName + aFile.Extension;
            var NewFileName = HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(DownloadName));

            context.Response.Clear();
            context.Response.Buffer = false;
            context.Response.ContentType = "application/octet-stream";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + NewFileName);
            context.Response.AddHeader("Content-Length", aFile.Length.ToString());
            context.Response.Filter.Close();
            context.Response.WriteFile(aFile.FullName);
            context.Response.End();
        }
    }


    //==========================================================================================================
    //==========================================================================================================
    //==========================================================================================================
    //==========================================================================================================
    //==========================================================================================================


    //取得Excel表頭資料
    private void ExcelFileSave(HttpContext context)
    {
        //回傳結果
        var theResult = new JBTool.TheJsonResult { IsOK = false, Result = "", ErrorMsg = "" };

        try
        {
            //檔案處理
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile theFile = context.Request.Files[0];
                if (Path.GetExtension(theFile.FileName) == ".xls")
                {
                    var bytes = Read2Byte(theFile.InputStream);
                    string FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    FileName = SaveTheFile(FileName, new MemoryStream(bytes));

                    //等待回傳
                    theResult.IsOK = true;
                    theResult.ErrorMsg = "";
                    theResult.Result = FileName;
                }
                else theResult.ErrorMsg = "檔案格式錯誤，請選擇Excel 97-2003 活頁簿(*.xls)格式";
            }
            else theResult.ErrorMsg = "找不到檔案";
        }
        catch (Exception) { theResult.ErrorMsg = "執行錯誤"; }

        context.Response.ContentType = "text/plain";
        context.Response.Write(theResult.ToJsonString());
    }

    //取得工作表標頭
    private void ExcelFileGetSheetTitle(HttpContext context)
    {
        //回傳結果
        var theResult = new JBTool.TheJsonResult { IsOK = false, Result = null, ErrorMsg = "" };

        //SheetIndex
        string SheetIndexStr = "0";
        if (context.Request.Form.AllKeys.Contains("sheetIndex")) SheetIndexStr = context.Request.Form["sheetIndex"].ToString();

        //SheetName
        string SheetName = "";
        if (context.Request.Form.AllKeys.Contains("sheetName")) SheetName = context.Request.Form["sheetName"].ToString();

        //檔案路徑
        string filePath = "";
        if (context.Request.Form.AllKeys.Contains("filePath")) filePath = context.Request.Form["filePath"].ToString();

        int headRowIndex = 0;
        if (context.Request.Form.AllKeys.Contains("headRowIndex")) headRowIndex = Convert.ToInt32(context.Request.Form["headRowIndex"].ToString());

        int SheetIndex = 0;
        if (Int32.TryParse(SheetIndexStr, out SheetIndex) && filePath.Length > 0)
        {
            var SheetArray = NPOIHelper.GetSheetList(ReadTheFile(filePath));

            if (SheetIndex < 0) SheetIndex = SheetArray.IndexOf(SheetName);
            if (SheetIndex < 0) SheetIndex = 0;

            var TitleArray = NPOIHelper.GetHeadRowListFromSheet(ReadTheFile(filePath), SheetIndex, headRowIndex);

            if (SheetArray == null) theResult.ErrorMsg = "找不到工作表";
            else if (TitleArray == null) theResult.ErrorMsg = "找不到標頭";
            else
            {
                var aResult = new Dictionary<string, object>();
                aResult.Add("FilePath", filePath);
                aResult.Add("SheetArray", SheetArray.Select(m => new { text = m, value = SheetArray.IndexOf(m), selected = SheetArray.IndexOf(m) == SheetIndex }).OrderBy(m => m.value).ToList());
                aResult.Add("TitleArray", TitleArray.Select(m => new { text = m.Key, value = m.Value }).OrderBy(m => m.value).ToList());

                //等待回傳
                theResult.IsOK = true;
                theResult.Result = aResult;
            }
        }

        context.Response.ContentType = "text/plain";
        context.Response.Write(theResult.ToJsonString());
    }

    private void ExcelFileSheetImport(HttpContext context)
    {
        string filePath = "";
        int sheetIndex = 0;
        var titleObject = new Dictionary<string, object>();
        string parameters = "";

        string remoteName = "";
        string method = "";

        //方法參數
        var methodParameters = new List<object>();

        //讀取檔案
        if (context.Request.Form.AllKeys.Contains("filePath")) filePath = context.Request.Form["filePath"].ToString();
        methodParameters.Add(JBTool.HandlerHelper.SerializeObject(ReadTheFile(filePath)));

        //SheetIndex
        if (!context.Request.Form.AllKeys.Contains("sheetIndex") || !int.TryParse(context.Request.Form["sheetIndex"].ToString(), out sheetIndex)) sheetIndex = 0;
        methodParameters.Add(sheetIndex);

        //字典檔
        if (context.Request.Form.AllKeys.Contains("titleObject")) titleObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(context.Request.Form["titleObject"]);
        methodParameters.Add(JBTool.HandlerHelper.SerializeObject(titleObject));

        //參數
        if (context.Request.Form.AllKeys.Contains("parameters")) parameters = context.Request.Form["parameters"].ToString();
        methodParameters.Add(parameters);

        //ServerPackage
        if (context.Request.Form.AllKeys.Contains("remoteName")) remoteName = context.Request.Form["remoteName"].ToString();

        //Method
        if (context.Request.Form.AllKeys.Contains("method")) method = context.Request.Form["method"].ToString();

        //執行方法與參數
        /* 1.IsOK
         * 2.Msg
         * 3.File
         */
        var ActionResult = (Dictionary<string, object>)JBTool.HandlerHelper.DeserializeObject(new JqDataObject(remoteName).CallMethod(method, methodParameters).ToString());

        var theResult = new JBTool.TheJsonResult();
        if (!(bool)ActionResult["IsOK"])
        {
            theResult.IsOK = false;
            theResult.ErrorMsg = ActionResult["Msg"].ToString();

            if (ActionResult.ContainsKey("File"))
            {
                //錯誤資料
                var FileMemoryStream = (MemoryStream)ActionResult["File"];
                if (FileMemoryStream != null)
                {
                    string FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    FileName = SaveTheFile(FileName, FileMemoryStream);
                    theResult.Result = FileName;
                }
            }
        }
        else
        {
            theResult.IsOK = true;
            theResult.Result = ActionResult["Msg"].ToString();
            if (ActionResult.ContainsKey("Result")) theResult.Result = ActionResult["Result"];
        }

        context.Response.ContentType = "text/plain";
        context.Response.Write(theResult.ToJsonString());
    }

    //傳參數進DLL
    private void CallServerMethod(HttpContext context)
    {
        var jdo = new JqDataObject(context.Request["remoteName"]);
        var methodName = context.Request["method"];

        //方法參數
        var parameters = new List<object>();

        //讀取檔案        
        string ParameterStr = "";
        if (context.Request.Form.AllKeys.Contains("parameters")) ParameterStr = context.Request.Form["parameters"].ToString();
        parameters.Add(ParameterStr);

        //執行方法與參數
        /* 1.IsOK
         * 2.Msg
         * 3.File
         */
        var ActionResult = (Dictionary<string, object>)JBTool.HandlerHelper.DeserializeObject(jdo.CallMethod(methodName, parameters).ToString());

        var theResult = new JBTool.TheJsonResult();

        theResult.IsOK = (bool)ActionResult["IsOK"];
        theResult.ErrorMsg = ActionResult["Msg"].ToString();
        if (ActionResult.ContainsKey("File"))
        {
            //錯誤資料
            var FileMemoryStream = (MemoryStream)ActionResult["File"];
            if (FileMemoryStream != null)
            {
                string FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                FileName = SaveTheFile(FileName, FileMemoryStream);
                theResult.Result = FileName;
            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(theResult.ToJsonString());
    }
    
    public bool IsReusable { get { return false; } }

}