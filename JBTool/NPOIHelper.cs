using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;

namespace JBTool
{
    public class NPOIHelper
    {
        /// <summary>取得欄位值</summary>
        /// <param name="aWorkbook">活頁簿</param>
        /// <param name="aCell">欄位</param>        
        protected static string aCellValue(IWorkbook aWorkbook, ICell aCell)
        {
            if (aCell == null) return "";
            switch (aCell.CellType)
            {
                case CellType.Blank: return "";
                case CellType.String: return aCell.StringCellValue;
                case CellType.Boolean: return aCell.BooleanCellValue.ToString();
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(aCell) && DateUtil.IsValidExcelDate(aCell.NumericCellValue))
                        return aCell.DateCellValue.ToString("s");
                    else
                        return aCell.NumericCellValue.ToString();
                case CellType.Formula:
                    IFormulaEvaluator e = new HSSFFormulaEvaluator(aWorkbook);
                    return aCellValue(aWorkbook, e.EvaluateInCell(aCell));
                default: return "";
            }
        }

        #region ---取得工作表---

        /// <summary>取得工作表</summary>
        /// <param name="FilePathName">檔案路徑</param>        
        public static List<string> GetSheetList(string FilePathName)
        {
            try
            {
                using (FileStream fileStream = new FileStream(FilePathName, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook aIWorkbook = WorkbookFactory.Create(fileStream);
                    return GetSheetList(aIWorkbook);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>取得工作表</summary>
        /// <param name="aFileStream">檔案流</param>
        public static List<string> GetSheetList(Stream aFileStream)
        {
            try
            {
                IWorkbook aIWorkbook = WorkbookFactory.Create(aFileStream);
                return GetSheetList(aIWorkbook);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>取得工作表</summary>
        /// <param name="aWorkbook">活頁簿</param>
        protected static List<string> GetSheetList(IWorkbook aIWorkbook)
        {
            List<string> Result = new List<string>();
            for (int SheetIndex = 0; SheetIndex < aIWorkbook.NumberOfSheets; SheetIndex++)
            {
                Result.Add(aIWorkbook.GetSheetAt(SheetIndex).SheetName);
            }
            return Result;
        }

        #endregion

        #region ---取得標頭---

        /// <summary>取得標頭</summary>
        /// <param name="FilePathName">檔案路徑</param>
        /// <param name="HeadRowIndex">標頭Index</param>   
        public static Dictionary<string, int> GetHeadRowList(string FilePathName, int HeadRowIndex = 0)
        {
            return GetHeadRowListFromSheet(FilePathName, 0, HeadRowIndex);
        }

        /// <summary>取得標頭</summary>
        /// <param name="aFileStream">檔案流</param>
        /// <param name="HeadRowIndex">標頭Index</param>
        public static Dictionary<string, int> GetHeadRowList(Stream aFileStream, int HeadRowIndex = 0)
        {
            return GetHeadRowListFromSheet(aFileStream, 0, HeadRowIndex);
        }

        /// <summary>取得標頭</summary>
        /// <param name="FilePathName">檔案路徑</param>
        /// <param name="SheetIndex">工作表Index</param>
        /// <param name="HeadRowIndex">標頭Index</param>
        public static Dictionary<string, int> GetHeadRowListFromSheet(string FilePathName, int SheetIndex, int HeadRowIndex = 0)
        {
            try
            {
                using (FileStream fileStream = new FileStream(FilePathName, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook aIWorkbook = WorkbookFactory.Create(fileStream);
                    return GetHeadRowListFromSheet(aIWorkbook, SheetIndex, HeadRowIndex);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>取得標頭</summary>
        /// <param name="aFileStream">檔案流</param>
        /// <param name="SheetIndex">工作表Index</param>
        /// <param name="HeadRowIndex">標頭Index</param>        
        public static Dictionary<string, int> GetHeadRowListFromSheet(Stream aFileStream, int SheetIndex, int HeadRowIndex = 0)
        {
            try
            {
                IWorkbook aIWorkbook = WorkbookFactory.Create(aFileStream);
                return GetHeadRowListFromSheet(aIWorkbook, SheetIndex, HeadRowIndex);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>取得標頭</summary>
        /// <param name="aIWorkbook">活頁簿</param>
        /// <param name="SheetIndex">工作表Index</param>
        /// <param name="HeadRowIndex">標頭Index</param>        
        protected static Dictionary<string, int> GetHeadRowListFromSheet(IWorkbook aIWorkbook, int SheetIndex, int HeadRowIndex)
        {
            try
            {
                Dictionary<string, int> HeadList = new Dictionary<string, int>();

                ISheet aSheet = aIWorkbook.GetSheetAt(SheetIndex);
                if (aSheet == null) return null;

                IRow aRow = aSheet.GetRow(HeadRowIndex);
                if (aRow == null) return null;

                for (int i = aRow.FirstCellNum; i < aRow.LastCellNum; i++)
                {
                    ICell aCell = aRow.GetCell(i);
                    if (aCell == null) continue;
                    string aValue = aCellValue(aIWorkbook, aCell);
                    if (!HeadList.ContainsKey(aValue)) HeadList.Add(aValue, i);
                }
                return HeadList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region ---取得資料---

        /// <summary>取得資料</summary>
        /// <param name="FilePathName">檔案路徑</param>
        /// <param name="SheetIndex">工作表Index</param>
        /// <param name="HeadRowIndex">標頭Index</param>        
        public static DataTable GetDataTable(string FilePathName, int HeadRowIndex = 0)
        {
            return GetDataTableFromSheet(FilePathName, 0, HeadRowIndex);
        }

        /// <summary>取得資料</summary>
        /// <param name="aFileStream">檔案流</param>
        /// /// <param name="HeadRowIndex">標頭Index</param>
        public static DataTable GetDataTable(Stream aFileStream, int HeadRowIndex = 0)
        {
            return GetDataTableFromSheet(aFileStream, 0, HeadRowIndex);
        }

        /// <summary>取得資料</summary>
        /// <param name="FilePathName">檔案路徑</param>        
        /// <param name="HeadList">標頭</param>
        /// <param name="HeadRowIndex">標頭Index</param>
        public static DataTable GetDataTable(string FilePathName, Dictionary<string, int> HeadList, int HeadRowIndex = 0)
        {
            return GetDataTableFromSheet(FilePathName, 0, HeadList, HeadRowIndex);
        }

        /// <summary>取得資料</summary>
        /// <param name="aFileStream">檔案流</param>
        /// <param name="HeadList">標頭</param>
        /// <param name="HeadRowIndex">標頭Index</param>
        public static DataTable GetDataTable(Stream aFileStream, Dictionary<string, int> HeadList, int HeadRowIndex = 0)
        {
            return GetDataTableFromSheet(aFileStream, 0, HeadList, HeadRowIndex);
        }

        /// <summary>取得資料</summary>
        /// <param name="FilePathName">檔案路徑</param>
        /// <param name="SheetIndex">工作表Index</param>
        /// <param name="HeadRowIndex">標頭Index</param>        
        public static DataTable GetDataTableFromSheet(string FilePathName, int SheetIndex, int HeadRowIndex = 0)
        {
            try
            {
                using (FileStream aFileStream = new FileStream(FilePathName, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook aIWorkbook = WorkbookFactory.Create(aFileStream);
                    Dictionary<string, int> HeadList = GetHeadRowListFromSheet(aIWorkbook, SheetIndex, HeadRowIndex);
                    return GetDataTableFromSheet(aIWorkbook, SheetIndex, HeadRowIndex, HeadList);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>取得資料</summary>
        /// <param name="aFileStream">檔案流</param>
        /// <param name="SheetIndex">工作表Index</param>
        /// <param name="HeadRowIndex">標頭Index</param>
        public static DataTable GetDataTableFromSheet(Stream aFileStream, int SheetIndex, int HeadRowIndex = 0)
        {
            try
            {
                IWorkbook aIWorkbook = WorkbookFactory.Create(aFileStream);
                Dictionary<string, int> HeadList = GetHeadRowListFromSheet(aIWorkbook, SheetIndex, HeadRowIndex);
                return GetDataTableFromSheet(aIWorkbook, SheetIndex, HeadRowIndex, HeadList);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>取得資料</summary>
        /// <param name="FilePathName">檔案路徑</param>
        /// <param name="SheetIndex">工作表Index</param>
        /// <param name="HeadRowIndex">標頭Index</param>
        /// <param name="HeadList">標頭</param>
        public static DataTable GetDataTableFromSheet(string FilePathName, int SheetIndex, Dictionary<string, int> HeadList, int HeadRowIndex = 0)
        {
            try
            {
                using (FileStream aFileStream = new FileStream(FilePathName, FileMode.Open))
                {
                    IWorkbook aIWorkbook = WorkbookFactory.Create(aFileStream);
                    return GetDataTableFromSheet(aIWorkbook, SheetIndex, HeadRowIndex, HeadList);
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>取得資料</summary>
        /// <param name="aFileStream">檔案流</param>
        /// <param name="SheetIndex">工作表Index</param>
        /// <param name="HeadRowIndex">標頭Index</param>
        /// <param name="HeadList">標頭</param>
        public static DataTable GetDataTableFromSheet(Stream aFileStream, int SheetIndex, Dictionary<string, int> HeadList, int HeadRowIndex = 0)
        {
            try
            {
                IWorkbook aIWorkbook = WorkbookFactory.Create(aFileStream);
                return GetDataTableFromSheet(aIWorkbook, SheetIndex, HeadRowIndex, HeadList);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>取得資料</summary>
        /// <param name="aIWorkbook">工作簿</param>
        /// <param name="SheetIndex">工作表Index</param>
        /// <param name="HeadRowIndex">標頭Index</param>
        /// <param name="HeadList">標頭</param>
        protected static DataTable GetDataTableFromSheet(IWorkbook aIWorkbook, int SheetIndex, int HeadRowIndex, Dictionary<string, int> HeadList)
        {
            try
            {
                DataTable AllData = new DataTable();
                Dictionary<int, int> HashList = new Dictionary<int, int>();

                foreach (var aHead in HeadList)
                {
                    int Index = AllData.Columns.Count;
                    HashList.Add(Index, aHead.Value);
                    AllData.Columns.Add(new DataColumn(aHead.Key));
                }

                ISheet aSheet = aIWorkbook.GetSheetAt(SheetIndex);

                for (int RowIndex = HeadRowIndex + 1; RowIndex <= aSheet.LastRowNum; RowIndex++)
                {
                    IRow aRow = aSheet.GetRow(RowIndex);
                    DataRow aNewDataRow = AllData.NewRow();
                    if (aRow != null)
                    {
                        foreach (var aHash in HashList)
                        {
                            ICell aCell = aRow.GetCell(aHash.Value);
                            aNewDataRow[aHash.Key] = aCellValue(aIWorkbook, aCell);
                        }
                    }
                    AllData.Rows.Add(aNewDataRow);
                }

                return AllData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region ---設定錯誤訊息---

        /// <summary>設定錯誤訊息</summary>
        /// <param name="aTable">資料表</param>
        /// <param name="FilePathName">檔案路徑</param>
        /// <param name="HeadList">標頭</param>
        /// <param name="HeadRowIndex">標頭Index</param>        
        public static bool SetErrorMemo(DataTable aTable, string FilePathName, Dictionary<string, int> HeadList, int HeadRowIndex = 0)
        {
            return SetErrorMemoToSheet(aTable, FilePathName, 0, HeadList, HeadRowIndex);
        }

        /// <summary>設定錯誤訊息</summary>
        /// <param name="aTable">資料表</param>
        /// <param name="aFileStream">檔案流</param>
        /// <param name="HeadList">標頭</param>
        /// <param name="HeadRowIndex">標頭Index</param>
        public static MemoryStream SetErrorMemo(DataTable aTable, Stream aFileStream, Dictionary<string, int> HeadList, int HeadRowIndex = 0)
        {
            return SetErrorMemoToSheet(aTable, aFileStream, 0, HeadList, HeadRowIndex);
        }

        /// <summary>設定錯誤訊息</summary>
        /// <param name="aTable">資料表</param>
        /// <param name="FilePathName">檔案路徑</param>
        /// <param name="HeadList">標頭</param>
        /// <param name="HeadRowIndex">標頭Index</param>        
        public static bool SetErrorMemoToSheet(DataTable aTable, string FilePathName, int SheetIndex, Dictionary<string, int> HeadList, int HeadRowIndex = 0)
        {
            try
            {
                using (FileStream InputFileStream = new FileStream(FilePathName, FileMode.Open, FileAccess.Read))
                {
                    var aWorkbook = SetErrorMemoToSheet(aTable, InputFileStream, SheetIndex, HeadRowIndex, HeadList);

                    using (FileStream OutputFileStream = new FileStream(FilePathName, FileMode.Open, FileAccess.Write))
                    {
                        aWorkbook.Write(OutputFileStream);
                    }
                }
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>設定錯誤訊息</summary>
        /// <param name="aTable">資料表</param>
        /// <param name="aFileStream">檔案流</param>
        /// <param name="SheetIndex">工作表Index</param>        
        /// <param name="HeadList">標頭</param>
        /// <param name="HeadRowIndex">標頭Index</param>
        public static MemoryStream SetErrorMemoToSheet(DataTable aTable, Stream aFileStream, int SheetIndex, Dictionary<string, int> HeadList, int HeadRowIndex = 0)
        {
            MemoryStream outMemoryStream = new MemoryStream();
            SetErrorMemoToSheet(aTable, aFileStream, SheetIndex, HeadRowIndex, HeadList).Write(outMemoryStream);
            outMemoryStream.Flush();
            outMemoryStream.Position = 0;
            return outMemoryStream;
        }

        /// <summary>設定錯誤訊息</summary>
        /// <param name="aTable">資料表</param>
        /// <param name="aFileStream">檔案流</param>
        /// <param name="SheetIndex">工作表Index</param>
        /// <param name="HeadRowIndex">標頭Index</param>
        /// <param name="HeadList">標頭</param>
        protected static IWorkbook SetErrorMemoToSheet(DataTable aTable, Stream aFileStream, int SheetIndex, int HeadRowIndex, Dictionary<string, int> HeadList)
        {
            IWorkbook aIWorkbook = WorkbookFactory.Create(aFileStream);
            ISheet aSheet = aIWorkbook.GetSheetAt(SheetIndex);

            //黃色樣式
            ICellStyle aMemoStyle = aIWorkbook.CreateCellStyle();
            aMemoStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
            aMemoStyle.FillPattern = FillPattern.SolidForeground;

            //黃色樣式
            ICellStyle aErrorStyle = aIWorkbook.CreateCellStyle();
            aErrorStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
            aErrorStyle.FillPattern = FillPattern.SolidForeground;

            foreach (var aErrorRow in aTable.AsEnumerable().Where(m => m.HasErrors))
            {
                IRow aRow = aSheet.GetRow((HeadRowIndex + 1) + aTable.Rows.IndexOf(aErrorRow));
                if (aRow == null) continue;

                List<string> ErrorList = new List<string>();
                ICell aCell = null;
                foreach (var aErrorCell in aErrorRow.GetColumnsInError())
                {
                    var aCellIndex = HeadList.ContainsKey(aErrorCell.Caption) ? HeadList[aErrorCell.Caption] : -1;
                    if (aCellIndex == -1) continue;

                    aCell = aRow.GetCell(aCellIndex);
                    if (aCell == null) aCell = aRow.CreateCell(aCellIndex);

                    aErrorStyle.SetFont(aCell.CellStyle.GetFont(aIWorkbook));
                    aErrorStyle.DataFormat = aCell.CellStyle.DataFormat;
                    aErrorStyle.Alignment = aCell.CellStyle.Alignment;
                    aCell.CellStyle = aErrorStyle;

                    ErrorList.Add(aErrorRow.GetColumnError(aErrorCell));
                }

                aCell = aRow.CreateCell(aRow.LastCellNum);
                aCell.CellStyle = aMemoStyle;
                aCell.SetCellValue(string.Join(";", ErrorList));
            }

            return aIWorkbook;
        }

        #endregion

        #region ---產生Excel資料表---

        public static MemoryStream DataTableToExcel(DataTable aTable)
        {
            MemoryStream outMemoryStream = new MemoryStream();
            DataTableToWorkbook(aTable).Write(outMemoryStream);
            outMemoryStream.Flush();
            outMemoryStream.Position = 0;
            return outMemoryStream;
        }

        protected static IWorkbook DataTableToWorkbook(DataTable aTable)
        {
            //2003.xls
            IWorkbook aIWorkbook = new HSSFWorkbook(); 
            ISheet aSheet = aIWorkbook.CreateSheet();

            //黃色樣式
            ICellStyle aErrorStyle = aIWorkbook.CreateCellStyle();
            aErrorStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
            aErrorStyle.FillPattern = FillPattern.SolidForeground;

            var ExcelRowIndex = 0;
            var aHeadRow = aSheet.CreateRow(ExcelRowIndex);
            ExcelRowIndex++;
            for (int ColumnIndex = 0; ColumnIndex < aTable.Columns.Count; ColumnIndex++)
            {
                var aCell = aHeadRow.CreateCell(ColumnIndex);
                aCell.SetCellValue(aTable.Columns[ColumnIndex].ColumnName);                
            }

            for (int RowIndex = 0; RowIndex < aTable.Rows.Count; RowIndex++)
            {
                var aRow = aSheet.CreateRow(ExcelRowIndex);
                ExcelRowIndex++;

                var ErrorList = new List<string>();
                for (int ColumnIndex = 0; ColumnIndex < aTable.Columns.Count; ColumnIndex++)
                {
                    var aCell = aRow.CreateCell(ColumnIndex);                                        
                    if (aTable.Rows[RowIndex][ColumnIndex] != null) aCell.SetCellValue(aTable.Rows[RowIndex][ColumnIndex].ToString());
                    var ErrorMsg = aTable.Rows[RowIndex].GetColumnError(ColumnIndex);
                    if (ErrorMsg.Length > 0)
                    {
                        aCell.CellStyle = aErrorStyle;
                        ErrorList.Add(ErrorMsg);
                    }
                }
                if (ErrorList.Count > 0)
                {
                    var AllErrorCell = aRow.CreateCell(aTable.Columns.Count);
                    AllErrorCell.CellStyle = aErrorStyle;
                    AllErrorCell.SetCellValue(string.Join(";", ErrorList));
                }
            }
            return aIWorkbook;
        }

        #endregion

        //DataTable轉成Excel檔案的方法
        public static void JBExportToExcel(DataTable table, string filePath, string title, List<string> columns)
        {
            JBExportToExcel(table, filePath, title, columns, null);
        }

        public static void JBExportToExcel(DataTable table, string filePath, string title, List<string> columns, List<string> totals)
        {
            //建立Excel 2003檔案
            IWorkbook wb = new HSSFWorkbook();
            ISheet ws;

            ////建立Excel 2007檔案
            //IWorkbook wb = new XSSFWorkbook();
            //ISheet ws;

            if (table.TableName != string.Empty)
            {
                if (title.Length > 0)
                    ws = wb.CreateSheet(title);
                else
                    ws = wb.CreateSheet("Sheet1");

            }
            else
            {
                ws = wb.CreateSheet("Sheet1");
            }

            ICellStyle timeStyle = wb.CreateCellStyle();
            timeStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("yyyy/MM/dd");
            timeStyle.Alignment = HorizontalAlignment.Left;

            ICellStyle decimalStyle = wb.CreateCellStyle();
            decimalStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0.00");
            decimalStyle.Alignment = HorizontalAlignment.Right;

            ICellStyle intStyle = wb.CreateCellStyle();
            intStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0");
            intStyle.Alignment = HorizontalAlignment.Right;

            ws.CreateRow(0);//第一行為欄位名稱
            if (columns.Count == 0)
            {
                foreach (DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);
                }
            }

            int i = 0;
            foreach (var column in columns)
            {
                DataColumn dc = table.Columns[column];
                ws.GetRow(0).CreateCell(i).SetCellValue(dc.Caption);
                i++;
            }

            for (i = 0; i < table.Rows.Count; i++)
            {
                int j = 0;
                ws.CreateRow(i + 1);
                foreach (var column in columns)
                {
                    DataColumn dc = table.Columns[column];
                    var aCell = ws.GetRow(i + 1).CreateCell(j);
                    if (dc.DataType == typeof(decimal) || dc.DataType == typeof(double))
                    {
                        aCell.CellStyle = decimalStyle;
                        if (table.Rows[i][dc] == DBNull.Value)
                            aCell.SetCellValue("");
                        else
                            aCell.SetCellValue(Double.Parse(table.Rows[i][dc].ToString()));
                    }
                    else if (dc.DataType == typeof(int) || dc.DataType == typeof(Int16)
                        || dc.DataType == typeof(Int32) || dc.DataType == typeof(Int64))
                    {
                        aCell.CellStyle = intStyle;
                        if (table.Rows[i][dc] == DBNull.Value)
                            aCell.SetCellValue("");
                        else
                            aCell.SetCellValue(int.Parse(table.Rows[i][dc].ToString()));
                    }
                    else if (dc.DataType == typeof(DateTime))
                    {
                        if (table.Rows[i][dc] == DBNull.Value)
                            aCell.SetCellValue("");
                        else
                        {
                            aCell.CellStyle = timeStyle;
                            if (((DateTime)table.Rows[i][dc]).ToString("HH:mm:ss") == "00:00:00")
                                aCell.SetCellValue(((DateTime)table.Rows[i][dc]).ToString("yyyy/MM/dd"));
                            else
                                aCell.SetCellValue(((DateTime)table.Rows[i][dc]).ToString("yyyy/MM/dd HH:mm:ss"));
                        }
                    }
                    else
                        ws.GetRow(i + 1).CreateCell(j).SetCellValue(table.Rows[i][dc].ToString().Trim());
                    j++;
                }
            }

            FileStream file = new FileStream(filePath, FileMode.Create);//產生檔案
            wb.Write(file);
            file.Close();
        }
    }
}
