using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// 流程动作类型
    /// </summary>
    [Serializable]
    public enum FLActionType
    {
        Submit,

        Approve,

        Return,

        Pause,

        Reject
    }
}
