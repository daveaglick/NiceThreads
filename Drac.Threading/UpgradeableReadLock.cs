using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace Drac.Threading
{
    public class UpgradeableReadLock : DisposableLock
    {
        public UpgradeableReadLock(ReaderWriterLockSlim lockSlim, TimeSpan timeout) : base(lockSlim)
        {
            if(!LockSlim.TryEnterUpgradeableReadLock(timeout))
            {
                throw new TimeoutException();
            }
        }

        public UpgradeableReadLock(ReaderWriterLockSlim lockSlim) : this(lockSlim, Timeout) { }

        public override void Dispose()
        {
            LockSlim.ExitUpgradeableReadLock();
        }
    }
}
