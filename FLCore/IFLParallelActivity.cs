using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FLCore
{
    /// <summary>
    /// ParallelActivity�ӿ�
    /// </summary>
    public interface IFLParallelActivity : IFLBaseActivity
    {
        List<string> ExecutedBranches { get;}
    }
}
