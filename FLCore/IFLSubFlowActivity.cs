using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// SubFlowActivity½Ó¿Ú
    /// </summary>
    public interface IFLSubFlowActivity : IFLBaseActivity
	{
        string XomlName { get;set;}

        bool IncludeFirstActivity {get;set;}

        string XomlField { get; set;}
	}
}
