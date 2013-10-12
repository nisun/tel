using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telehire.Services.Abstract
{
    public partial interface IUtilityService
    {
        string RemoteIP { get; }
        string HostIP { get; }
        T XMLDeserialize<T>(string data);
        string SerializeToXML<T>(T obj);
        string GenerateRandomDigitCode(int length);
        string ServiceUrl();

    }
}
