using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// HyperLinkActivity�ӿ�
    /// </summary>
	public interface IFLHyperLinkActivity : IFLBaseActivity
	{
        string Parameters { get; set; }
	}
}
