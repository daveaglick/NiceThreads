using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    public class ReadLock : DisposableLock
    {
        public ReadLock(ReaderWriterLockSlim lockSlim, TimeSpan timeout) : base(lockSlim)
        {
            if(!LockSlim.TryEnterReadLock(timeout))
            {
                throw new TimeoutException();
            }
        }

        public ReadLock(ReaderWriterLockSlim lockSlim) : this(lockSlim, Timeout) { }

        public override void Dispose()
        {
            LockSlim.ExitReadLock();
        }
    }
}
