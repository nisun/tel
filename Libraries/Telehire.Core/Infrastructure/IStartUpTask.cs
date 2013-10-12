using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telehire.Core.Infrastructure
{
    public interface IStartupTask
    {
        void Execute();
    }
}
