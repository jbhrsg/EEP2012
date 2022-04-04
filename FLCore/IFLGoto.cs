using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FLCore
{
    public interface IFLGotoActivity: IFLBaseActivity
    {
        string ActivityName { get; set; }
    }
}
