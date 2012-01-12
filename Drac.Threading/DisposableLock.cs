using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    public abstract class DisposableLock : IDisposable
    {
        private readonly ReaderWriterLockSlim _lockSlim;

        protected DisposableLock(ReaderWriterLockSlim lockSlim)
        {
            _lockSlim = lockSlim;
        }

        public ReaderWriterLockSlim LockSlim
        {
            get { return _lockSlim; }
        }

        public abstract void Dispose();
    }
}
