using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telehire.Core
{
    public class JavaScriptException : Exception
    {
        public JavaScriptException(string message)
            : base(message)
        {
        }
    }
}
