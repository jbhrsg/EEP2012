using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFLBaseActivity
    {
        string Description { get; set;}

        string Name { get;set;}

        bool Enabled { get; set; }
    }
}
