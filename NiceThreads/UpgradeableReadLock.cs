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
    public class UpgradeableReadLock : DisposableLock
    {
        public UpgradeableReadLock(ILocker locker, TimeSpan timeout) : base(locker)
        {
            if(!Locker.TryEnterUpgradeableReadLock(timeout))
            {
                throw new TimeoutException();
            }
        }

        public UpgradeableReadLock(ILocker locker) : this(locker, Globals.Timeout) { }

        public override void Dispose()
        {
            Locker.ExitUpgradeableReadLock();
        }
    }
}
