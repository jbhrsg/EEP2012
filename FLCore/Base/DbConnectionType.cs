using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore.Base
{
    [Serializable]
    public enum DbConnectionType
    {
        SqlClient = 0,

        OleDb = 1,

        Odbc = 2,

        OracleClient = 3,

        MySQL = 4,

        Informix = 5
    }
}
