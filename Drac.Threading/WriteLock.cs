using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    public class WriteLock : DisposableLock
    {
        public WriteLock(ReaderWriterLockSlim lockSlim, TimeSpan timeout) : base(lockSlim)
        {
            if(!LockSlim.TryEnterWriteLock(timeout))
            {
                throw new TimeoutException();
            }
        }

        public WriteLock(ReaderWriterLockSlim lockSlim) : this(lockSlim, Timeout) {}

        public override void Dispose()
        {
            LockSlim.ExitWriteLock();
        }
    }
}
