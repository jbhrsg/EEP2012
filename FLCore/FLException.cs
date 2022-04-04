using System;
using System.Collections.Generic;
using System.Text;

namespace FLCore
{
    /// <summary>
    /// 流程流转中的异常
    /// </summary>
    public class FLException : Exception
    {
        private int _type;

        public int Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public FLException(string message) : this(1, message)
        {
        }

        public FLException(int type, string message) : base(message)
        {
            _type = type;
        }
    }
}
