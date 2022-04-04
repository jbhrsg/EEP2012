using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// ProcedureActivity�ӿ�
    /// </summary>
    public interface IFLProcedureActivity : IFLBaseActivity, IAddToFLPathList
    {
        string ModuleName { get;set;}

        string MethodName { get;set;}

        bool ErrorLog { get;set;}

        string ErrorToRole { get;set;}
    }
}
