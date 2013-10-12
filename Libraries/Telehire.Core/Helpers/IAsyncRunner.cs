using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telehire.Core.Helpers
{
    public interface IAsyncRunner
    {
        void Run<T>(Action<T> action);
    }
}
