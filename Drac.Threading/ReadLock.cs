using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    public class ReadLock : IDisposable
    {
        private readonly ReaderWriterLockSlim _lockSlim;

        public ReadLock(ReaderWriterLockSlim lockSlim)
        {
            _lockSlim = lockSlim;
            _lockSlim.EnterReadLock();
        }

        public void Dispose()
        {
            _lockSlim.ExitReadLock();
        }
    }
}
