using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// Activity½Ó¿Ú
    /// </summary>
    public interface IFLActivity : IFLBaseActivity
    {
        string UpperParallelBranch { get;set;}

        bool IsUpperParallelAnd { get;set;}

        string UpperParallel { get;set;}

        void AddFLActivity(IFLActivity flActivity);

        void Execute();

        void Return();

        void InitExecStatus();

        //IFLActivity Parent { get;set;}

        //List<IFLActivity> Children { get;set;}
    }
}
