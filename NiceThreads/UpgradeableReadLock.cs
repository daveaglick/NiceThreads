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
    /// A disposable locking object for upgradeable read locks.
    /// </summary>
    public class UpgradeableReadLock : DisposableLock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeableReadLock"/> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <param name="timeout">The timeout.</param>
        public UpgradeableReadLock(Locker locker, TimeSpan timeout) : base(locker, timeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeableReadLock"/> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        public UpgradeableReadLock(Locker locker) : base(locker)
        {
        }

        protected override void EnterLock()
        {
            Locker.EnterUpgradeableReadLock();
        }

        protected override void EnterLock(TimeSpan timeout)
        {
            Locker.EnterUpgradeableReadLock(timeout);
        }

        protected override void ExitLock()
        {
            Locker.ExitUpgradeableReadLock();
        }
    }
}
