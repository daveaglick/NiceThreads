using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    public class ReadLock : DisposableLock
    {
        public ReadLock(ReaderWriterLockSlim lockSlim) : base(lockSlim)
        {
            LockSlim.EnterReadLock();
        }

        public override void Dispose()
        {
            LockSlim.ExitReadLock();
        }
    }
}
