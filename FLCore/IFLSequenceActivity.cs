using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// SequenceActivity�ӿ�
    /// </summary>
    public interface IFLSequenceActivity : IFLBaseActivity
    {
        FLDirection FLDirection { get;}

        void SetFLDirection(FLDirection flDirection);
    }
}
