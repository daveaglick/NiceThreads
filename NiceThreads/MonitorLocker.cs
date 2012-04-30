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
    /// Provides an Locker implementation for the Monitor class.
    /// Uses the static Monitor methods with this instance as the object.
    /// </summary>
    public class MonitorLocker : Locker
    {
        private readonly object _lock = new object();

        /// <inheritdoc />
        public override void EnterReadLock()
        {
            Monitor.Enter(_lock);
        }

        /// <inheritdoc />
        public override void EnterWriteLock()
        {
            Monitor.Enter(_lock);
        }

        /// <inheritdoc />
        public override void EnterUpgradeableReadLock()
        {
            Monitor.Enter(_lock);
        }

        /// <inheritdoc />
        public override bool TryEnterReadLock(TimeSpan timeout)
        {
            return Monitor.TryEnter(_lock, timeout);
        }

        /// <inheritdoc />
        public override bool TryEnterWriteLock(TimeSpan timeout)
        {
            return Monitor.TryEnter(_lock, timeout);
        }

        /// <inheritdoc />
        public override bool TryEnterUpgradeableReadLock(TimeSpan timeout)
        {
            return Monitor.TryEnter(_lock, timeout);
        }

        /// <inheritdoc />
        public override void ExitReadLock()
        {
            Monitor.Exit(_lock);
        }

        /// <inheritdoc />
        public override void ExitWriteLock()
        {
            Monitor.Exit(_lock);
        }

        /// <inheritdoc />
        public override void ExitUpgradeableReadLock()
        {
            Monitor.Exit(_lock);
        }
    }
}
