using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// 等待事件执行的Activity接口
    /// </summary>
    public interface IEventWaiting : IFLBaseActivity, IAddToFLPathList
    {
        string FormName { get; set;}

        string WebFormName { get; set;}

        FLNavigatorMode FLNavigatorMode { get; set;}

        NavigatorMode NavigatorMode { get; set;}

        SendToKind SendToKind { get; set;}

        string SendToField { get; set;}

        string SendToRole { get; set;}

        string SendToUser { get;set;}

        string Parameters { get; set;}

        decimal ExpTime { get; set;}

        decimal UrgentTime { get; set;}

        TimeUnit TimeUnit { get; set;}

        DateTime ExecutedTime { get; }

        bool IsUrgent { get;}

        string SendToId { get; set;}

        bool SendEmail { get;set; }

        bool AllowSendBack { get; set; }
    }
}
