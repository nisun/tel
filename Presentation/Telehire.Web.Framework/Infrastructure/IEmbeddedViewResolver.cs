using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telehire.Web.Framework.Infrastructure
{
    public interface IEmbeddedViewResolver
    {
        EmbeddedViewTable GetEmbeddedViews();
    }
}
