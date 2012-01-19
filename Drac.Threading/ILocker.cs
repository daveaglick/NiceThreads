using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drac.Threading
{
    public interface ILocker
    {
        void EnterReadLock();
        void EnterWriteLock();
        void EnterUpgradeableReadLock();
        bool TryEnterReadLock(TimeSpan timeout);
        bool TryEnterWriteLock(TimeSpan timeout);
        bool TryEnterUpgradeableReadLock(TimeSpan timeout);
        void ExitReadLock();
        void ExitWriteLock();
        void ExitUpgradeableReadLock();
    }
}
