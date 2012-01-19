using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    public class ReadLock : DisposableLock
    {
        public ReadLock(ILocker locker, TimeSpan timeout) : base(locker)
        {
            if(!Locker.TryEnterReadLock(timeout))
            {
                throw new TimeoutException();
            }
        }

        public ReadLock(ILocker locker) : this(locker, Globals.Timeout) { }

        public override void Dispose()
        {
            Locker.ExitReadLock();
        }
    }
}
