using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// StandActivity½Ó¿Ú
    /// </summary>
    public interface IFLStandActivity : IFLBaseActivity, IFLPlusApprove
    {
        bool DelayAutoApprove { get; set; }
    }
}
