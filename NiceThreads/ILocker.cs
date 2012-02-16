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
