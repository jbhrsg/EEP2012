using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// QueryActivity�ӿ�
    /// </summary>
	public interface IFLQueryActivity : IFLBaseActivity
	{
        string Parameters { get; set; }
	}
}
