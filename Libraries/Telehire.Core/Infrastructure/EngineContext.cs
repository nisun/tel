using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Telehire.Core.Infrastructure
{
    public class EngineContext
    {
        public static ITelehireEngine Current
        {
            get
            {
                if (SingletonActivator<ITelehireEngine>.Instance == null)
                {
                    Initialize(false);
                }
                return SingletonActivator<ITelehireEngine>.Instance;
            }
        }

        public static void Replace(ITelehireEngine engine)
        {
            SingletonActivator<ITelehireEngine>.Instance = engine;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ITelehireEngine Initialize(bool forceRecreate)
        {
            if (SingletonActivator<ITelehireEngine>.Instance == null || forceRecreate)
            {

                Debug.WriteLine("Constructing engine " + DateTime.Now);
                SingletonActivator<ITelehireEngine>.Instance = new TelehireEngine();
                Debug.WriteLine("Initializing engine " + DateTime.Now);
                SingletonActivator<ITelehireEngine>.Instance.Initialize();
            }
            return SingletonActivator<ITelehireEngine>.Instance;
        }
    }
}
