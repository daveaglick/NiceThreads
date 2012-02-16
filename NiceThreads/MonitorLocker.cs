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
