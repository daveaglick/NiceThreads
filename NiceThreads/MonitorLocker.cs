using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NiceThreads
{
    /// <summary>
    /// Provides an ILocker implementation for the Monitor class.
    /// </summary>
    public class MonitorLocker : ILocker
    {
        public void EnterReadLock()
        {
            Monitor.Enter(this);
        }

        public void EnterWriteLock()
        {
            Monitor.Enter(this);
        }

        public void EnterUpgradeableReadLock()
        {
            Monitor.Enter(this);
        }

        public bool TryEnterReadLock(TimeSpan timeout)
        {
            return Monitor.TryEnter(this, timeout);
        }

        public bool TryEnterWriteLock(TimeSpan timeout)
        {
            return Monitor.TryEnter(this, timeout);
        }

        public bool TryEnterUpgradeableReadLock(TimeSpan timeout)
        {
            return Monitor.TryEnter(this, timeout);
        }

        public void ExitReadLock()
        {
            Monitor.Exit(this);
        }

        public void ExitWriteLock()
        {
            Monitor.Exit(this);
        }

        public void ExitUpgradeableReadLock()
        {
            Monitor.Exit(this);
        }
    }
}
