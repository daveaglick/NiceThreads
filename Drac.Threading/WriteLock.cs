using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    public class WriteLock : DisposableLock
    {
        public WriteLock(ReaderWriterLockSlim lockSlim) : base(lockSlim)
        {
            LockSlim.EnterWriteLock();
        }

        public override void Dispose()
        {
            LockSlim.ExitWriteLock();
        }
    }
}
