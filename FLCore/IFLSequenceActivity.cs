using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// SequenceActivity½Ó¿Ú
    /// </summary>
    public interface IFLSequenceActivity : IFLBaseActivity
    {
        FLDirection FLDirection { get;}

        void SetFLDirection(FLDirection flDirection);
    }
}
