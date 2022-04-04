using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// ProcedureActivity½Ó¿Ú
    /// </summary>
    public interface IFLProcedureActivity : IFLBaseActivity, IAddToFLPathList
    {
        string ModuleName { get;set;}

        string MethodName { get;set;}

        bool ErrorLog { get;set;}

        string ErrorToRole { get;set;}
    }
}
