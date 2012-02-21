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
    /// A disposable locking object for read locks.
    /// </summary>
    public class ReadLock : DisposableLock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadLock"/> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <param name="timeout">The timeout.</param>
        public ReadLock(ILocker locker, TimeSpan timeout) : base(locker)
        {
            if(!Locker.TryEnterReadLock(timeout))
            {
                throw new TimeoutException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadLock"/> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        public ReadLock(ILocker locker) : this(locker, Globals.Timeout) { }

        /// <inheritdoc />
        public override void Dispose()
        {
            Locker.ExitReadLock();
        }
    }
}
