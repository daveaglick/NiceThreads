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
using System.Threading;

namespace NiceThreads
{
    /// <summary>
    /// Provides an Locker implementation for the ReaderWriterLockSlim class.
    /// </summary>
    public class ReaderWriterLockSlimLocker : Locker
    {
        private readonly ReaderWriterLockSlim _readerWriterLockSlim;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterLockSlimLocker"/> class
        /// with a new ReaderWriterLockSlim.
        /// </summary>
        public ReaderWriterLockSlimLocker()
        {
            _readerWriterLockSlim = new ReaderWriterLockSlim();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterLockSlimLocker"/> class.
        /// </summary>
        /// <param name="readerWriterLockSlim">The ReaderWriterLockSlim to use.</param>
        public ReaderWriterLockSlimLocker(ReaderWriterLockSlim readerWriterLockSlim)
        {
            _readerWriterLockSlim = readerWriterLockSlim;
        }

        /// <inheritdoc />
        public override void EnterReadLock()
        {
            _readerWriterLockSlim.EnterReadLock();
        }

        /// <inheritdoc />
        public override void EnterWriteLock()
        {
            _readerWriterLockSlim.EnterWriteLock();
        }

        /// <inheritdoc />
        public override void EnterUpgradeableReadLock()
        {
            _readerWriterLockSlim.EnterUpgradeableReadLock();
        }

        /// <inheritdoc />
        public override bool TryEnterReadLock(TimeSpan timeout)
        {
            return _readerWriterLockSlim.TryEnterReadLock(timeout);
        }

        /// <inheritdoc />
        public override bool TryEnterWriteLock(TimeSpan timeout)
        {
            return _readerWriterLockSlim.TryEnterWriteLock(timeout);
        }

        /// <inheritdoc />
        public override bool TryEnterUpgradeableReadLock(TimeSpan timeout)
        {
            return _readerWriterLockSlim.TryEnterUpgradeableReadLock(timeout);
        }

        /// <inheritdoc />
        public override void ExitReadLock()
        {
            _readerWriterLockSlim.ExitReadLock();
        }

        /// <inheritdoc />
        public override void ExitWriteLock()
        {
            _readerWriterLockSlim.ExitWriteLock();
        }

        /// <inheritdoc />
        public override void ExitUpgradeableReadLock()
        {
            _readerWriterLockSlim.ExitUpgradeableReadLock();
        }
    }
}
