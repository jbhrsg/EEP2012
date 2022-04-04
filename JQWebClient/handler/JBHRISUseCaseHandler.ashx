<%@ WebHandler Language="C#" Class="JBHRISUseCaseHandler" %>

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

public class JBHRISUseCaseHandler : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        var mode = context.Request["mode"];
        SetTheTempFileSavePath();//從Config設定檔案儲存路徑

        if (mode == "FileDownload") FileDownload(context);
        else if (mode == "FileUpload") FileUpload(context);
        else if (mode == "CardCollect") CardCollect(context);
        else if (mode == "CardCollectForFile") CardCollectForFile(context);
        else if (mode == "BankMedia") BankMedia(context);
        else if (mode == "CallServerMethodReturnFile") CallServerMethodReturnFile(context);
            
    }

    //從Config設定檔案儲存路徑
    private void SetTheTempFileSavePath()
    {
        string AppSettingName = "JBTempFileSavePath";
        var AppSettings = System.Configuration.ConfigurationManager.AppSettings;
        if (!string.IsNullOrEmpty(AppSettings[AppSettingName])) this.TempFileSavePath = AppSettings[AppSettingName];
    }


    //存檔路徑
    public string TempFileSavePath = @"../Files/JBHRIS/Temp";

    /// <summary>進行呼叫Method</summary>
    private object CallMethod(HttpContext context)
    {
        var jdo = new JqDataObject(context.Request["remoteName"]);
        var methodName = context.Request["method"];

        var parameters = new List<object>();

        var aDictionary = new Dictionary<string, object>();
        if (context.Request.Form.Count > 0)
        {
            foreach (var aKey in context.Request.Form.AllKeys) aDictionary.Add(aKey, context.Request.Form[aKey]);
        }
        parameters.Add(JBTool.HandlerHelper.SerializeObject(aDictionary));

        MemoryStream aMemoryStream = new MemoryStream();
        if (context.Request.Files.AllKeys.Length > 0)
        {
            HttpPostedFile theFile = context.Request.Files[0];
            theFile.InputStream.CopyTo(aMemoryStream);
            aMemoryStream.Seek(0, SeekOrigin.Begin);
        }
        parameters.Add(JBTool.HandlerHelper.SerializeObject(aMemoryStream));

        return jdo.CallMethod(methodName, parameters);
    }    

    private void FileUpload(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Write(CallMethod(context).ToString());
    }

    private void CardCollect(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Write((string)CallMethod(context));
    }

    private void CardCollectForFile(HttpContext context)
    {
        var result = (Dictionary<string, object>)JBTool.HandlerHelper.DeserializeObject((string)CallMethod(context));

        var ResultString = "";
        if (!(bool)result["IsOK"]) ResultString = new JBTool.TheJsonResult { ErrorMsg = result["ErrorMsg"].ToString() }.ToJsonString();
        else
        {
            //錯誤資料列
            var ErrorStringList = (List<string>)result["ErrorStringList"];

            //把錯誤資料存起來
            string ErrorFilePath = "";

            //如果有錯就要寫檔
            if (ErrorStringList.Count > 0)
            {
                //把錯誤資料存起來
                ErrorFilePath = SaveTheFile(DateTime.Now.ToString("yyyyMMddHHmmss"), ErrorStringList);
            }

            ResultString = new JBTool.TheJsonResult
            {
                IsOK = true,
                Result = String.Format("成功執行{0}筆，錯誤{1}筆", result["DoNum"], ErrorStringList.Count),
                ErrorMsg = ErrorFilePath
            }.ToJsonString();

            //顯示正確與錯誤數量
            //顯示下載路徑
        }

        context.Response.ContentType = "text/plain";
        context.Response.Write(ResultString);
    }

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

    /// <summary>文字資料寫入</summary>
    /// <param name="FileName">檔案名稱</param>
    /// <param name="StringList">寫入資料</param>
    /// <returns>存檔後檔案路徑(名稱)</returns>
    private string SaveTheFile(string FileName, List<string> StringList)
    {
        byte[] dataAsBytes = Encoding.GetEncoding("big5").GetBytes(string.Join(Environment.NewLine, StringList));
        return SaveTheFile(FileName, new MemoryStream(dataAsBytes));
    }

    /// <summary>檔案資料寫入</summary>
    /// <param name="FileName">檔案名稱</param>
    /// <param name="aMemoryStream">寫入資料</param>
    /// <returns>存檔後檔案路徑(名稱)</returns>
    private string SaveTheFile(string FileName, MemoryStream aMemoryStream)
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
            aMemoryStream.WriteTo(file);
            aMemoryStream.Close();
        }

        //回傳可能變更後的檔案+路徑
        return Path.Combine(TempFileSavePath, FileName);
    }

    private void FileDownload(HttpContext context)
    {
        //FilePathName
        //DownloadName        
        var FilePathName = HttpUtility.UrlDecode(context.Request.QueryString["FilePathName"] ?? "");
        //var FilePath = string.Format("~/{0}", FilePathName);
        var FilePath = HttpContext.Current.Server.MapPath(FilePathName);

        //System.IO.FileInfo aFile = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(FilePath));
        System.IO.FileInfo aFile = new System.IO.FileInfo(FilePath);
        if (!aFile.Exists)
        {
            context.Response.ContentType = "text/html";
            context.Response.Write("<script type='text/javascript'>alert('檔案下載失敗');</script>");
        }
        else
        {
            var DownloadName = HttpUtility.UrlDecode(aFile.Name ?? "");
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

    private void BankMedia(HttpContext context)
    {
        var result = (Dictionary<string, object>)JBTool.HandlerHelper.DeserializeObject((string)CallMethod(context));

        var ResultString = "";
        if (!(bool)result["IsOK"]) ResultString = new JBTool.TheJsonResult { ErrorMsg = result["ErrorMsg"].ToString() }.ToJsonString();
        else
        {
            //媒體檔案
            var MediaFile = (string)result["FileString"];
            var FileExtension = (string)result["FileExtension"];
            byte[] dataAsBytes = Encoding.GetEncoding("big5").GetBytes(MediaFile);

            //把錯誤資料存起來
            //string MediaFilePath = SaveTheFile(FileExtension, new MemoryStream(dataAsBytes));

            string FileName=FileExtension;
            MemoryStream aMemoryStream = new MemoryStream(dataAsBytes);
            //確認路徑目錄
            var MapPath = HttpContext.Current.Server.MapPath(Path.Combine(TempFileSavePath, FileName));
            string DirectoryPath = Path.GetDirectoryName(MapPath);
            if (!System.IO.Directory.Exists(DirectoryPath)) System.IO.Directory.CreateDirectory(DirectoryPath);

            //確定儲存的檔案名稱
            //FileName = GetSaveFileNamePath(MapPath);
            MapPath = HttpContext.Current.Server.MapPath(Path.Combine(TempFileSavePath, FileName));

            //進行存檔
            using (FileStream file = new FileStream(MapPath, FileMode.Create, FileAccess.Write))
            {
                aMemoryStream.WriteTo(file);
                aMemoryStream.Close();
            }

            //回傳可能變更後的檔案+路徑
            string MediaFilePath = Path.Combine(TempFileSavePath, FileName);
            

            ResultString = new JBTool.TheJsonResult
            {
                IsOK = true,
                Result = "OK",
                ErrorMsg = MediaFilePath
            }.ToJsonString();
        }

        context.Response.ContentType = "text/plain";
        context.Response.Write(ResultString);
    }

    private void CallServerMethodReturnFile(HttpContext context)
    {
        var jdo = new JqDataObject(context.Request["remoteName"]);
        var methodName = context.Request["method"];

        var parameters = new List<object>();
        if (context.Request.Form["parameters"] != null) parameters.Add(context.Request["parameters"]);
          
        var ActionResult = (Dictionary<string, object>)JBTool.HandlerHelper.DeserializeObject(jdo.CallMethod(methodName, parameters).ToString());
        
        var KeyName = "FileStreamOrFileName";
        
        var ReturnResult = new Dictionary<string, object>();
        foreach (var aItem in ActionResult)
        {
            if (aItem.Key == KeyName)
            {
                var FileMemoryStream = (MemoryStream)aItem.Value;
                if (FileMemoryStream != null) ReturnResult.Add(aItem.Key, SaveTheFile(DateTime.Now.ToString("yyyyMMddHHmmss"), FileMemoryStream));
            }
            else ReturnResult.Add(aItem.Key, aItem.Value);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(JsonConvert.SerializeObject(ReturnResult, Formatting.Indented));
    }
    
    public bool IsReusable { get { return false; } }
}
