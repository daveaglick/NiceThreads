using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NiceThreads
{
    /// <summary>
    /// A consistent wrapper for different types of locking primitives. Not all
    /// methods have appropriate analogs for all lock types (for example, the standard
    /// Monitor class does not distinguish between read and write locks, so all methods
    /// in this class just lock the Monitor).
    /// </summary>
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
