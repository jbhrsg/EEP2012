using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    [Serializable]
	public enum FLNavigatorMode
	{
        Submit = 0,
        Approve = 1,
        Return = 2,
        Notify = 3,
        Inquery = 4,
        Continue = 5
	}
}
