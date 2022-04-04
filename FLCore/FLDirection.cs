using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// 流程的流转方向
    /// </summary>
    public enum FLDirection
    {
        Waiting = 0,

        GoToNext = 1,

        GoToBack = 2
    }
}
