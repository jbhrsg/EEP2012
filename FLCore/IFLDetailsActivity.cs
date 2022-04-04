using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// DetailsActivity½Ó¿Ú
    /// </summary>
    public interface IFLDetailsActivity : IFLBaseActivity, IFLPlusApprove
    {
        string DetailsTableName { get;set;}

        string RelationKeys { get;set;}

        string ParallelField { get;set;}

        string SendToField { get;set;}

        ParallelMode ParallelMode { get; set; }

        string SendToMasterField { get; set; }

        decimal ParallelRate { get; set; }


        string FormName { get; set; }

        string WebFormName { get; set; }

        FLNavigatorMode FLNavigatorMode { get; set; }

        NavigatorMode NavigatorMode { get; set; }

        string FLNavigatorField { get; set; }

        string ExtApproveID { get; set;}
        string ExtGroupField { get; set;}
        string ExtValueField { get; set;}

        SendToKind SendToKind { get; set; }

        string SendToRole { get; set; }

        string SendToUser { get;set;}

        string Parameters { get; set; }

        decimal ExpTime { get; set; }

        decimal UrgentTime { get; set; }

        TimeUnit TimeUnit { get; set; }

        DateTime ExecutedTime { get; }

        bool IsUrgent { get; }

        string SendToId { get; set; }

        bool SendEmail { get; set; }

        bool AllowSendBack { get; set; }
    }
}
