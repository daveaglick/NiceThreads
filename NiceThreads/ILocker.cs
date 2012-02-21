/*
 * Copyright 2012 WildCard, LLC
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 */

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
        /// <summary>
        /// Enters a read lock.
        /// </summary>
        void EnterReadLock();

        /// <summary>
        /// Enters a write lock.
        /// </summary>
        void EnterWriteLock();

        /// <summary>
        /// Enters an upgradeable read lock.
        /// </summary>
        void EnterUpgradeableReadLock();

        /// <summary>
        /// Tries to enter a read lock.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns>True if the lock was succesfully entered before the timeout.</returns>
        bool TryEnterReadLock(TimeSpan timeout);

        /// <summary>
        /// Tries to enter a write lock.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns>True if the lock was succesfully entered before the timeout.</returns>
        bool TryEnterWriteLock(TimeSpan timeout);

        /// <summary>
        /// Tries to enter an upgradeable read lock.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns>True if the lock was succesfully entered before the timeout.</returns>
        bool TryEnterUpgradeableReadLock(TimeSpan timeout);

        /// <summary>
        /// Exits a read lock.
        /// </summary>
        void ExitReadLock();

        /// <summary>
        /// Exits a write lock.
        /// </summary>
        void ExitWriteLock();

        /// <summary>
        /// Exits an upgradeable read lock.
        /// </summary>
        void ExitUpgradeableReadLock();
    }
}
