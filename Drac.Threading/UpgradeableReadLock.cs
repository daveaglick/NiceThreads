using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace Drac.Threading
{
    public class UpgradeableReadLock : DisposableLock
    {
        public UpgradeableReadLock(ReaderWriterLockSlim lockSlim) : base(lockSlim)
        {
            LockSlim.EnterUpgradeableReadLock();
        }

        public override void Dispose()
        {
            LockSlim.ExitUpgradeableReadLock();
        }
    }
}
