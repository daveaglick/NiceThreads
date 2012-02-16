using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace NiceThreads
{
    public class UpgradeableReadLock : DisposableLock
    {
        public UpgradeableReadLock(ILocker locker, TimeSpan timeout) : base(locker)
        {
            if(!Locker.TryEnterUpgradeableReadLock(timeout))
            {
                throw new TimeoutException();
            }
        }

        public UpgradeableReadLock(ILocker locker) : this(locker, Globals.Timeout) { }

        public override void Dispose()
        {
            Locker.ExitUpgradeableReadLock();
        }
    }
}
