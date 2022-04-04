using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// RootActivity½Ó¿Ú
    /// </summary>
    public interface IFLRootActivity : IFLBaseActivity
    {
        string EEPAlias { get; set; }

        string TableName { get; set; }

        string Keys { get; set; }

        string PresentFields { get; set; }

        string OrgKind { get; set; }

        string FormName { get; set; }

        string WebFormName { get; set; }

        decimal ExpTime { get; set; }

        decimal UrgentTime { get; set; }

        string ExpTimeField { get; set;}

        TimeUnit TimeUnit { get; set; }

        bool NotifySendMail { get; set; }

        bool SkipForSameUser { get; set;}

        string RejectProcedure { get; set; }

        string BodyField { get; set; }

        string MailApproveLevel { get; set; }
    }
}
