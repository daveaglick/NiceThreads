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
    /// Provides an ILocker implementation for the Monitor class.
    /// Uses the static Monitor methods with this instance as the object.
    /// </summary>
    public class MonitorLocker : ILocker
    {
        /// <inheritdoc />
        public void EnterReadLock()
        {
            Monitor.Enter(this);
        }

        /// <inheritdoc />
        public void EnterWriteLock()
        {
            Monitor.Enter(this);
        }

        /// <inheritdoc />
        public void EnterUpgradeableReadLock()
        {
            Monitor.Enter(this);
        }

        /// <inheritdoc />
        public bool TryEnterReadLock(TimeSpan timeout)
        {
            return Monitor.TryEnter(this, timeout);
        }

        /// <inheritdoc />
        public bool TryEnterWriteLock(TimeSpan timeout)
        {
            return Monitor.TryEnter(this, timeout);
        }

        /// <inheritdoc />
        public bool TryEnterUpgradeableReadLock(TimeSpan timeout)
        {
            return Monitor.TryEnter(this, timeout);
        }

        /// <inheritdoc />
        public void ExitReadLock()
        {
            Monitor.Exit(this);
        }

        /// <inheritdoc />
        public void ExitWriteLock()
        {
            Monitor.Exit(this);
        }

        /// <inheritdoc />
        public void ExitUpgradeableReadLock()
        {
            Monitor.Exit(this);
        }
    }
}
