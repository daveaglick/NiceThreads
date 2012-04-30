using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public abstract class Locker
    {
        /// <summary>
        /// Enters a read lock.
        /// </summary>
        public abstract void EnterReadLock();

        /// <summary>
        /// Enters a write lock.
        /// </summary>
        public abstract void EnterWriteLock();

        /// <summary>
        /// Enters an upgradeable read lock.
        /// </summary>
        public abstract void EnterUpgradeableReadLock();

        /// <summary>
        /// Tries to enter a read lock.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns>True if the lock was succesfully entered before the timeout.</returns>
        public abstract bool TryEnterReadLock(TimeSpan timeout);

        /// <summary>
        /// Tries to enter a write lock.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns>True if the lock was succesfully entered before the timeout.</returns>
        public abstract bool TryEnterWriteLock(TimeSpan timeout);

        /// <summary>
        /// Tries to enter an upgradeable read lock.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns>True if the lock was succesfully entered before the timeout.</returns>
        public abstract bool TryEnterUpgradeableReadLock(TimeSpan timeout);

        /// <summary>
        /// Exits a read lock.
        /// </summary>
        public abstract void ExitReadLock();

        /// <summary>
        /// Exits a write lock.
        /// </summary>
        public abstract void ExitWriteLock();

        /// <summary>
        /// Exits an upgradeable read lock.
        /// </summary>
        public abstract void ExitUpgradeableReadLock();

        /// <summary>
        /// Enters a read lock.
        /// </summary>
        /// <param name="timeout">The timeout after which, if a lock hasn't been
        /// acquired, a TimeoutException will be thrown.</param>
        public void EnterReadLock(TimeSpan timeout)
        {
            if (!TryEnterReadLock(timeout))
            {
                ThrowTimeoutException("read");
            }
        }

        /// <summary>
        /// Enters a write lock.
        /// </summary>
        /// <param name="timeout">The timeout after which, if a lock hasn't been
        /// acquired, a TimeoutException will be thrown.</param>
        public void EnterWriteLock(TimeSpan timeout)
        {
            if (!TryEnterWriteLock(timeout))
            {
                ThrowTimeoutException("write");
            }
        }

        /// <summary>
        /// Enters an upgradeable read lock.
        /// </summary>
        /// <param name="timeout">The timeout after which, if a lock hasn't been
        /// acquired, a TimeoutException will be thrown.</param>
        public void EnterUpgradeableReadLock(TimeSpan timeout)
        {
            if (!TryEnterUpgradeableReadLock(timeout))
            {
                ThrowTimeoutException("upgradeable read");
            }
        }

        private void ThrowTimeoutException(string lockType)
        {
            StringBuilder builder = new StringBuilder("Timeout while attempting to enter " + lockType + " lock.");
            #if DEBUG
                builder.AppendLine();
                DisposableLock.AppendLog(builder);
            #endif
            throw new TimeoutException(builder.ToString());
        }
    }
}
