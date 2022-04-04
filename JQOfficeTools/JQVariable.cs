using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace JQOfficeTools
{
    public class JQControl
    {
        public const string OfficePlate = "info-officeplate";
    }

    public class JQProperty
    {
        public const string DataOptions = "data-options";
        public const string InfolightOptions = "infolight-options";
    }

    public class JQCondtion
    {
        public const string Equal = "=";
        public const string NotEqual = "!=";
        public const string GreaterThan = ">";
        public const string EqualOrGreaterThan = ">=";
        public const string LessThan = "<";
        public const string EqualOrLessThan = "<=";
        public const string BeginWith = "%";
        public const string Contain = "%%";
    }

    public class JQDataType
    {
        public const string Number = "number";
        public const string String = "string";
        public const string DateTime = "datetime";
        public const string Guid = "guid";
        //public const string OracleDateTime = "oracledatetime";
    }

    public class JQTotalType
    {
        public const string Sum = "sum";
        public const string Max = "max";
        public const string Min = "min";
        public const string Average = "avg";
        public const string Count = "count";
    }

    public class JQLocale
    {
        public const string Afrikaans = "af";
        public const string Arabic = "ar";
        public const string Bulgarian = "bg";
        public const string Catalan = "ca";
        public const string Czech = "cs";
        //public const string Czech = "cz";
        public const string Danish = "da";
        public const string German = "de";
        public const string Greek = "el";
        public const string English = "en";
        public const string Spanish = "es";
        public const string French = "fr";
        public const string Italian = "it";
        public const string Japanese = "jp";
        public const string Dutch = "nl";
        public const string Portuguese = "pt_BR";
        public const string Russian = "ru";
        public const string Turkish = "tr";
        public const string Chinese_China = "zh_CN";
        public const string Chinese_Taiwan = "zh_TW";
    }

    public class JQAlignment
    {
        public const string Left = "left";
        public const string Center = "center";
        public const string Right = "right";
    }
}
