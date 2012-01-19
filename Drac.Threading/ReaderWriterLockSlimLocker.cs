using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    public class ReaderWriterLockSlimLocker : ILocker
    {
        private readonly ReaderWriterLockSlim _readerWriterLockSlim;

        public ReaderWriterLockSlimLocker()
        {
            _readerWriterLockSlim = new ReaderWriterLockSlim();
        }

        public ReaderWriterLockSlimLocker(ReaderWriterLockSlim readerWriterLockSlim)
        {
            _readerWriterLockSlim = readerWriterLockSlim;
        }

        public void EnterReadLock()
        {
            _readerWriterLockSlim.EnterReadLock();
        }

        public void EnterWriteLock()
        {
            _readerWriterLockSlim.EnterWriteLock();
        }

        public void EnterUpgradeableReadLock()
        {
            _readerWriterLockSlim.EnterUpgradeableReadLock();
        }

        public bool TryEnterReadLock(TimeSpan timeout)
        {
            return _readerWriterLockSlim.TryEnterReadLock(timeout);
        }

        public bool TryEnterWriteLock(TimeSpan timeout)
        {
            return _readerWriterLockSlim.TryEnterWriteLock(timeout);
        }

        public bool TryEnterUpgradeableReadLock(TimeSpan timeout)
        {
            return _readerWriterLockSlim.TryEnterUpgradeableReadLock(timeout);
        }

        public void ExitReadLock()
        {
            _readerWriterLockSlim.ExitReadLock();
        }

        public void ExitWriteLock()
        {
            _readerWriterLockSlim.ExitWriteLock();
        }

        public void ExitUpgradeableReadLock()
        {
            _readerWriterLockSlim.ExitUpgradeableReadLock();
        }
    }
}
