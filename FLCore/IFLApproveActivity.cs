using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// ApproveActivity½Ó¿Ú
    /// </summary>
    public interface IFLApproveActivity : IFLBaseActivity, IFLPlusApprove
    {
        List<IFLApproveBranchActivity> GetApproveRights();

        bool DelayAutoApprove { get; set; }
    }
}
