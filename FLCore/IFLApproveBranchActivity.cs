using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// ApproveBranchActivity�ӿ�
    /// </summary>
	public interface IFLApproveBranchActivity
	{
        string Grade { get;set;}

        string Expression { get;set;}

        string Name { get;set;}

        string ParentActivity { get;set;}
	}
}
