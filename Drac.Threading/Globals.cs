using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    internal class Globals
    {
        // This is a global timeout value - acts as a failsafe in case a lock doesn't get released
        internal readonly static TimeSpan Timeout = TimeSpan.FromMinutes(5);

        // Returns a default ILocker type (currently ReaderWriterLockSlim based)
        internal static ILocker GetDefaultLocker()
        {
            return  new ReaderWriterLockSlimLocker();
        }
    }
}
