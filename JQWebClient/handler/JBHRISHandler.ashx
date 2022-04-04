<%@ WebHandler Language="C#" Class="JBHRISHandler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text;
using System.Linq;
using JBTool;

public class JBHRISHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        SetTheTempFileSavePath();//從Config設定檔案儲存路徑
        
        var mode = context.Request["mode"];
        if (mode == "FileDownload")
        {
            FileDownload(context);
        }
        else if (mode == "ExportExcel")
        {
            ExportExcel(context);
        }
        else if (mode == "ImportFile")
        {
            ImportFile(context);
        }
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

    public class TheJsonResult
    {
        public bool IsOK { get; set; }

        public string ErrorMsg { get; set; }

        public object Result { get; set; }

        public TheJsonResult()
        {
            IsOK = false;
            ErrorMsg = "";
            Result = null;
        }

        public string ToJsonString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }
    }

    private void ImportFile(HttpContext context)
    {
        //Filter
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";
        if (context.Request.Files.AllKeys.Length == 0) context.Response.Write(new TheJsonResult { ErrorMsg = "找不到檔案" }.ToJsonString());
        else
        {
            var virtualDirectoryPath = TempFileSavePath + "/";

            var filter = context.Request.Form["Filter"] ?? "";
            var filterlist = filter.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            for (int i = 0; i < context.Request.Files.AllKeys.Length; i++)
            {
                HttpPostedFile theFile = context.Request.Files[i];

                try
                {
                    //副檔名檢查
                    var ext = System.IO.Path.GetExtension(theFile.FileName);
                    if (!string.IsNullOrEmpty(filter) && !filterlist.Any(m => "." + m == ext)) context.Response.Write(new TheJsonResult { ErrorMsg = "副檔名不符合" }.ToJsonString());
                    else
                    {
                        //確認路徑目錄
                        var directoryPath = context.Server.MapPath(virtualDirectoryPath);
                        if (!System.IO.Directory.Exists(directoryPath)) System.IO.Directory.CreateDirectory(directoryPath);

                        //檔名重複問題
                        var filename = DateTime.Now.ToString("yyyyMMdd") + System.IO.Path.GetExtension(theFile.FileName);
                        var virtualpath = virtualDirectoryPath + filename;
                        var path = context.Server.MapPath(virtualpath);
                        for (int Index = 0; System.IO.File.Exists(path); Index++)
                        {
                            filename = string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMdd"), Index) + System.IO.Path.GetExtension(theFile.FileName);
                            virtualpath = virtualDirectoryPath + filename;
                            path = context.Server.MapPath(virtualpath);
                        }

                        //儲存準備回傳
                        //var size = theFile.InputStream.Length;
                        theFile.SaveAs(path);
                        context.Response.Write(new TheJsonResult { IsOK = true, Result = path }.ToJsonString());
                    }
                }
                catch (Exception e)
                {
                    context.Response.Write(new TheJsonResult { ErrorMsg = "儲存失敗" }.ToJsonString());
                }
            }
        }

    }

    private void FileDownload(HttpContext context)
    {
        //FilePathName
        //DownloadName        
        var FilePathName = HttpUtility.UrlDecode(context.Request.QueryString["FilePathName"] ?? "");
        //var FilePath = string.Format("~/{0}", FilePathName);
        var FilePath = FilePathName;

        //System.IO.FileInfo aFile = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(FilePath));
        System.IO.FileInfo aFile = new System.IO.FileInfo(FilePathName);
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

    private void ExportExcel(HttpContext context)
    {
        var exportTable = new System.Data.DataTable();
        //flag == "0" 轉 Excel ; 
        //flag == "1" 轉 txt ; 
        //flag == "2" 轉 csv;
       
        string flag = context.Request.Form["flag"] ?? "0";
        string userid = context.Request.Form["userid"] ?? "";
        userid = userid + "_";
        string reportName = context.Request.Form["reportName"] ?? "";
        reportName = reportName + "_";
        
        var fileName = "";
        var path = "";

        var fields = (Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(context.Request.Form["fields"]);
        var rows = (Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(context.Request.Form["rows"]);


        foreach (Newtonsoft.Json.Linq.JObject field in fields)
        {
            var columnType = typeof(string);
            if (rows.Count > 0)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    var obj = rows[i] as Newtonsoft.Json.Linq.JObject;
                    var value = obj[(string)field["field"]];
                    //if (value.Type.ToString() != "Null")
                    if (value != null)
                    {
                        if (value.Type == Newtonsoft.Json.Linq.JTokenType.Integer)
                        {
                            columnType = typeof(int);
                        }
                        else if (value.Type == Newtonsoft.Json.Linq.JTokenType.Float)
                        {
                            columnType = typeof(double);
                        }
                        else if (value.Type == Newtonsoft.Json.Linq.JTokenType.Date)
                        {
                            columnType = typeof(DateTime);
                        }
                        break;
                    }
                }
            }
            exportTable.Columns.Add(new System.Data.DataColumn((string)field["field"], columnType) { Caption = (string)field["title"] });
        }

        foreach (Newtonsoft.Json.Linq.JObject row in rows)
        {
            var dataRow = exportTable.NewRow();
            foreach (var column in row)
            {
                if (exportTable.Columns.Contains(column.Key))
                {
                    if (column.Value.Type.ToString() != "Null")
                        dataRow[column.Key] = column.Value.ToString();
                }
            }
            exportTable.Rows.Add(dataRow);
        }

        string txtFile = string.Format("{0}*.txt", userid + reportName);
        string excelFile = string.Format("{0}*.xls", userid + reportName);
        string csvFile = string.Format("{0}*.csv", userid + reportName);
        string filePath = HttpContext.Current.Server.MapPath("../Files/");

        if (flag == "0")
        {
            foreach (FileInfo file in new DirectoryInfo(filePath).GetFiles(excelFile))
            {
                file.Delete();
            }
            fileName = string.Format("{0}.xls", userid + reportName + DateTime.Now.ToString("yyyyMMddHHmmss"));
            path = string.Format("../Files/{0}", fileName);
            //JQUtility.ExportToExcel(exportTable, HttpContext.Current.Server.MapPath(path), "", new List<string>());
            NPOIHelper.JBExportToExcel(exportTable, HttpContext.Current.Server.MapPath(path), "", new List<string>());
            
        }
        else if (flag == "1")
        {
            foreach (FileInfo file in new DirectoryInfo(filePath).GetFiles(txtFile))
            {
                file.Delete();
            }
            fileName = string.Format("{0}.txt", userid + reportName + DateTime.Now.ToString("yyyyMMddHHmmss"));
            path = string.Format("../Files/{0}", fileName);
            
            StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath(path), false, Encoding.Unicode);
            String DataRow = "";
            string isBlank = context.Request.Form["isBlank"] ?? "Y";
            string isColumnName = context.Request.Form["isColumnName"] ?? "N";
            //isBlank == "Y" 欄位與欄位間需要加一個空白字串 ; 
            //isBlank == "N" 欄位與欄位間不需要加一個空白字串; 
            //isColumnName == "Y" 需要加欄位表頭名稱 ; 
            //isColumnName == "N" 不需要加欄位表頭名稱; 
            if (isColumnName == "Y")
            {
                for (int i = 0; i < exportTable.Columns.Count; i++) //取得欄位名稱 
                {
                    DataRow += exportTable.Columns[i].ColumnName;
                    if (i < exportTable.Columns.Count - 1) DataRow += " ";
                }
                sw.WriteLine(DataRow);
            }

            for (int i = 0; i < exportTable.Rows.Count; i++) // 取得資料
            {
                DataRow = "";
                for (int j = 0; j < exportTable.Columns.Count; j++)
                {
                    DataRow += exportTable.Rows[i][j].ToString();
                    if (j < exportTable.Columns.Count - 1 && isBlank == "Y") DataRow += " ";
                }
                sw.WriteLine(DataRow);
            }
            sw.Close();
        }
        else
        {
            foreach (FileInfo file in new DirectoryInfo(filePath).GetFiles(csvFile))
            {
                file.Delete();
            }
            fileName = string.Format("{0}.csv", userid + reportName + DateTime.Now.ToString("yyyyMMddHHmmss"));
            path = string.Format("../Files/{0}", fileName);
            CreateCSVFile(exportTable, path);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(fileName);
    }

    public static void CreateCSVFile(DataTable exportTable, string path)
    {
        StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath(path), false, System.Text.Encoding.Unicode);
        //sw. = System.Text.Encoding.Unicode;// System.Text.UTF8Encoding;
        int colCount = exportTable.Columns.Count;

        if (exportTable.Columns.Count > 0)
            sw.Write(exportTable.Columns[0]);
        for (int i = 1; i < exportTable.Columns.Count; i++)
            sw.Write("," + exportTable.Columns[i]);

        sw.Write(sw.NewLine);
        foreach (DataRow dr in exportTable.Rows)
        {
            if (exportTable.Columns.Count > 0 && !Convert.IsDBNull(dr[0]))
                sw.Write(Convert.ToString(dr[0]));
            for (int i = 1; i < colCount; i++)
                sw.Write("," + Convert.ToString(dr[i]));
            sw.Write(sw.NewLine);
        }
        sw.Close();
    }

  
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}