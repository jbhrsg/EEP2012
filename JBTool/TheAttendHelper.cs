using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TheAttendHelper
{
    /// <summary>人員日期區間資料管理</summary>
    public abstract class EmployeeAndDateRangeFilter<T>
    {
        /// <summary>【想要過濾的資料】</summary>
        public List<T> FilterData { get; set; }

        /// <summary>【人員時間資料】</summary>
        public List<EmployeeAndDateRange> EmployeeDateRange { get; set; }
        
        /// <summary>執行過濾作業</summary>
        public List<T> ExecuteFilter()
        {
            List<T> Ans = new List<T>();

            //字典檔，根據Employee
            var aDictionary = DictionaryTransfer(this.EmployeeDateRange);

            foreach (var aItem in this.FilterData)
            {
                Ans.AddRange(ExecuteFilter(aItem, aDictionary));
            }
            return Ans;
        }

        /// <summary>單筆資料過濾</summary>
        protected abstract List<T> ExecuteFilter(T aItem, Dictionary<string, List<DateRange>> aDictionary);

        /// <summary>轉換成人員日期區間字典檔</summary>
        private Dictionary<string, List<DateRange>> DictionaryTransfer(List<EmployeeAndDateRange> InputData)
        {
            //轉換資料
            return InputData.GroupBy(m => m.EmployeeID)
                .ToDictionary(
                m => m.Key,
                m => Reorganize(m.Select(n => new DateRange { DateFrom = n.DateFrom, DateTo = n.DateTo }).ToList()));
        }

        /// <summary>重整日期區間，把連在一起日期串起來</summary>
        private List<DateRange> Reorganize(List<DateRange> DateRangeList)
        {
            /*A~B,(B+1)~C,D~E
             *A~C,(B+1)~C,D~E
             */
            var LongerRange = DateRangeList.Select(m => new DateRange
            {
                DateFrom = m.DateFrom,
                DateTo = m.GetLongerDate(DateRangeList)
            }).ToList();

            //A~C,D~E
            return LongerRange.GroupBy(m => m.DateTo)
                                .Select(m => new DateRange
                                {
                                    DateFrom = m.Min(n => n.DateFrom),
                                    DateTo = m.Key
                                }).ToList();
        }
    }

    /// <summary>人員與日期區間資料</summary>
    public class EmployeeAndDateRange
    {
        /// <summary>【員工ID】</summary>
        public string EmployeeID { get; set; }

        /// <summary>【開始日期】</summary>
        public DateTime DateFrom { get; set; }

        /// <summary>【截止日期】</summary>
        public DateTime DateTo { get; set; }
    }

    /// <summary>日期區間資料</summary>
    public class DateRange
    {
        /// <summary>【開始日期】</summary>
        public DateTime DateFrom { get; set; }

        /// <summary>【截止日期】</summary>
        public DateTime DateTo { get; set; }

        /// <summary>取得最長的時間</summary>
        public DateTime GetLongerDate(List<DateRange> aDateRangeList)
        {
            return GetLongerDate(this.DateTo, aDateRangeList);
        }

        /// <summary>取得最長的時間</summary>
        private DateTime GetLongerDate(DateTime aDate, List<DateRange> aDateRangeList)
        {
            var aNew = aDateRangeList.FirstOrDefault(m => m.DateTo.Date > aDate.Date && m.DateFrom.AddDays(-1).Date <= aDate.Date);
            if (aNew == null) return aDate;

            return GetLongerDate(aNew.DateTo, aDateRangeList);
        }
    }

    /// <summary>時間區間</summary>
    public class RangeTime
    {
        /// <summary>【起始】</summary>
        public DateTime DateTimeFrom { get; set; }

        /// <summary>【截止】</summary>
        public DateTime DateTimeTo { get; set; }

        /// <summary>是否交集(不包含頭尾)</summary>
        public bool IsCross(RangeTime aRangeTime)
        {
            return (this.DateTimeFrom < aRangeTime.DateTimeTo && DateTimeTo > aRangeTime.DateTimeFrom);
        }

        /// <summary>區間分鐘數</summary>
        public int GetMinutes()
        {
            return Convert.ToInt32(new TimeSpan(DateTimeTo.Ticks - DateTimeFrom.Ticks).TotalMinutes);
        }
    }

    /// <summary>時間區間_方法</summary>
    public class RangeTimeFun
    {
        /// <summary>區間整合，會把重疊的部分合起來</summary>
        public static List<RangeTime> Merge(List<RangeTime> MergeRangeTimeList)
        {
            List<RangeTime> NewRangeTimeList = new List<RangeTime>();

            foreach (var aRangeTime in MergeRangeTimeList)
            {
                NewRangeTimeList.Add(new RangeTime { DateTimeFrom = aRangeTime.DateTimeFrom, DateTimeTo = RangeTimeFun.GetLastDateTime(aRangeTime.DateTimeTo, MergeRangeTimeList) });
            }
            return NewRangeTimeList.GroupBy(m => m.DateTimeTo).Select(m => new RangeTime { DateTimeFrom = m.Min(n => n.DateTimeFrom), DateTimeTo = m.Key }).ToList();
        }

        /// <summary>取得最晚時間(遞迴)</summary>
        private static DateTime GetLastDateTime(DateTime aDateTime, List<RangeTime> RangeTimeList)
        {
            var MaxTimeList = RangeTimeList.Where(m => m.DateTimeFrom <= aDateTime && aDateTime < m.DateTimeTo).ToList();
            if (MaxTimeList.Count == 0) return aDateTime;
            return RangeTimeFun.GetLastDateTime(MaxTimeList.Max(m => m.DateTimeTo), RangeTimeList);
        }

        /// <summary>區間切割，把區間做排除的動作</summary>
        /// <param name="aRangeTimeList">被切割</param>
        /// <param name="SplitRangeTimeList">切割</param>
        /// (注意，這裡的時間片段不可以重疊)
        public static List<RangeTime> Split(List<RangeTime> aRangeTimeList, List<RangeTime> SplitRangeTimeList)
        {
            //這裡已經假設各區間都已經整理好了(不重疊時間)

            //全部打直排序之後判斷交集去做
            List<DateTime> TimeList = aRangeTimeList.SelectMany(m => new List<DateTime> { m.DateTimeFrom, m.DateTimeTo }).ToList();
            TimeList = TimeList.Union(SplitRangeTimeList.SelectMany(m => new List<DateTime> { m.DateTimeFrom, m.DateTimeTo })).OrderBy(m => m).ToList();

            //TimeList.AddRange(aRangeTimeList.Select(m => m.DateTimeFrom));
            //TimeList.AddRange(aRangeTimeList.Select(m => m.DateTimeTo));
            //TimeList.AddRange(SplitRangeTimeList.Select(m => m.DateTimeFrom));
            //TimeList.AddRange(SplitRangeTimeList.Select(m => m.DateTimeTo));
            //TimeList = TimeList.OrderBy(m => m).ToList();

            List<RangeTime> theResult = new List<RangeTime>();
            for (int i = 0; i < TimeList.Count - 1; i++)
            {
                var aRangeTime = new RangeTime { DateTimeFrom = TimeList[i], DateTimeTo = TimeList[i + 1] };
                if (aRangeTimeList.Any(m => m.IsCross(aRangeTime)) && !SplitRangeTimeList.Any(m => m.IsCross(aRangeTime)))
                    theResult.Add(aRangeTime);
            }
            return theResult;
        }

        /// <summary>區間交集，兩區段組合的交集</summary>
        /// <param name="RangeTimeList1">區間1</param>
        /// <param name="RangeTimeList2">區間2</param>
        public static List<RangeTime> Union(List<RangeTime> RangeTimeList1, List<RangeTime> RangeTimeList2)
        {
            //這裡已經假設各區間都已經整理好了(不重疊時間)
            return RangeTimeList1.SelectMany(m =>
                RangeTimeList2.Where(n => m.IsCross(n)).Select(
                n => new RangeTime
                {
                    DateTimeFrom = GetMaxDateTime(n.DateTimeFrom, m.DateTimeFrom),
                    DateTimeTo = GetMinDateTime(n.DateTimeTo, m.DateTimeTo)
                })).ToList();
        }

        /// <summary>取得較小的時間</summary>
        private static DateTime GetMinDateTime(DateTime DateTime1, DateTime DateTime2)
        {
            return (DateTime1 <= DateTime2) ? DateTime1 : DateTime2;
        }

        /// <summary>取得較大的時間</summary>
        private static DateTime GetMaxDateTime(DateTime DateTime1, DateTime DateTime2)
        {
            return (DateTime1 >= DateTime2) ? DateTime1 : DateTime2;
        }

    }
       
}
