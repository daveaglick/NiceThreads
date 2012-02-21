﻿/*
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
    /// Provides an ILocker implementation for the ReaderWriterLockSlim class.
    /// </summary>
    public class ReaderWriterLockSlimLocker : ILocker
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
        public void EnterReadLock()
        {
            _readerWriterLockSlim.EnterReadLock();
        }

        /// <inheritdoc />
        public void EnterWriteLock()
        {
            _readerWriterLockSlim.EnterWriteLock();
        }

        /// <inheritdoc />
        public void EnterUpgradeableReadLock()
        {
            _readerWriterLockSlim.EnterUpgradeableReadLock();
        }

        /// <inheritdoc />
        public bool TryEnterReadLock(TimeSpan timeout)
        {
            return _readerWriterLockSlim.TryEnterReadLock(timeout);
        }

        /// <inheritdoc />
        public bool TryEnterWriteLock(TimeSpan timeout)
        {
            return _readerWriterLockSlim.TryEnterWriteLock(timeout);
        }

        /// <inheritdoc />
        public bool TryEnterUpgradeableReadLock(TimeSpan timeout)
        {
            return _readerWriterLockSlim.TryEnterUpgradeableReadLock(timeout);
        }

        /// <inheritdoc />
        public void ExitReadLock()
        {
            _readerWriterLockSlim.ExitReadLock();
        }

        /// <inheritdoc />
        public void ExitWriteLock()
        {
            _readerWriterLockSlim.ExitWriteLock();
        }

        /// <inheritdoc />
        public void ExitUpgradeableReadLock()
        {
            _readerWriterLockSlim.ExitUpgradeableReadLock();
        }
    }
}
