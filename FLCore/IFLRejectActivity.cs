using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// RejectActivity½Ó¿Ú
    /// </summary>
    public interface IFLRejectActivity : IFLBaseActivity
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

        int ExpTime { get; set;}

        int UrgentTime { get; set;}

        TimeUnit TimeUnit { get; set; }

        bool SendEmail { get; set; }
    }
}
