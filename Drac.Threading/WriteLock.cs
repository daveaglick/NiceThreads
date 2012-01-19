using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    public class WriteLock : DisposableLock
    {
        public WriteLock(ILocker locker, TimeSpan timeout) : base(locker)
        {
            if(!Locker.TryEnterWriteLock(timeout))
            {
                throw new TimeoutException();
            }
        }

        public WriteLock(ILocker locker) : this(locker, Globals.Timeout) { }

        public override void Dispose()
        {
            Locker.ExitWriteLock();
        }
    }
}
