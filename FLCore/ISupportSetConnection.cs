using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FLCore.Base;

namespace FLCore
{
    public interface ISupportSetConnectionString
    {
        DbConnectionType ConnectionType { get;set;}

        string EEPAlias { get;set;}

        string ConnectionString { get;}
    }
}
