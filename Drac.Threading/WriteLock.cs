using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    public class WriteLock : IDisposable
    {
        private readonly ReaderWriterLockSlim _lockSlim;

        public WriteLock(ReaderWriterLockSlim lockSlim)
        {
            _lockSlim = lockSlim;
            _lockSlim.EnterWriteLock();
        }

        public void Dispose()
        {
            _lockSlim.ExitWriteLock();
        }
    }
}
