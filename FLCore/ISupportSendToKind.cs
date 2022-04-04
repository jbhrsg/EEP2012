using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// 支持SendToKind的接口
    /// </summary>
    public interface ISupportSendToKind
    {
        SendToKind SendToKind { get;set;}
    }
}
