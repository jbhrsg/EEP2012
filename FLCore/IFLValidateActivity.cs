using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// ValidateActivity½Ó¿Ú
    /// </summary>
    public interface IFLValidateActivity : IFLBaseActivity
    {
        string Expression { get;set;}

        string Message { get; set; }
    }
}
