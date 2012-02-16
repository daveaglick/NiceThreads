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
    /// Implements a thread-safe wrapper around objects that normally wouldn't be thread-safe.
    /// Uses a ReaderWriterLockSlim locking object if an alternate ILocker isn't specified.
    /// </summary>
    /// <typeparam name="T">The type of the object to be wrapped.</typeparam>
    public class SyncObject<T> : ISyncObject<T>
    {
        private readonly ILocker _locker;

        public ILocker Locker
        {
            get { return _locker; }
        }

        public T UnsyncField;    //Expose this directly so it can be used anywhere the variable is expected (such as in ref or out parameters)

        public T Unsync
        {
            get { return UnsyncField; }
            set { UnsyncField = value; }
        }

        public T Sync
        {
            get { using (ReadLock()) { return UnsyncField; } }
            set { using (WriteLock()) { UnsyncField = value; } }
        }

        public SyncObject(T value)
            : this(value, Globals.GetDefaultLocker())
        {
        }

        public SyncObject(T value, ILocker locker)
        {
            UnsyncField = value;
            _locker = locker;
        }

        public IDisposable ReadLock()
        {
            return new ReadLock(_locker);
        }

        public IDisposable UpgradeableReadLock()
        {
            return new UpgradeableReadLock(_locker);
        }

        public IDisposable WriteLock()
        {
            return new WriteLock(_locker);
        }

        public IDisposable ReadLock(TimeSpan timeout)
        {
            return new ReadLock(_locker, timeout);
        }

        public IDisposable UpgradeableReadLock(TimeSpan timeout)
        {
            return new UpgradeableReadLock(_locker, timeout);
        }

        public IDisposable WriteLock(TimeSpan timeout)
        {
            return new WriteLock(_locker, timeout);
        }
        
        public void DoRead(Action<T> action)
        {
            using(ReadLock())
            {
                action(UnsyncField);
            }
        }

        public void DoWrite(Action<T> action)
        {
            using(WriteLock())
            {
                action(UnsyncField);
            }
        }

        public TR DoRead<TR>(Func<T, TR> func)
        {
            using(ReadLock())
            {
                return func(UnsyncField);
            }
        }

        public TR DoWrite<TR>(Func<T, TR> func)
        {
            using(WriteLock())
            {
                return func(UnsyncField);
            }
        }
    }
}
