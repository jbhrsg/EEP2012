using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// StandActivity�ӿ�
    /// </summary>
    public interface IFLStandActivity : IFLBaseActivity, IFLPlusApprove
    {
        bool DelayAutoApprove { get; set; }
    }
}
