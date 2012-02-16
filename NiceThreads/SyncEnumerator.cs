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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NiceThreads
{
    /// <summary>
    /// Provides thread-safe enumeration capabilities.
    /// Based on code from http://www.codeproject.com/KB/cs/safe_enumerable.aspx.
    /// </summary>
    /// <typeparam name="T">The type of the objects being enumerated.</typeparam>
    public class SyncEnumerator<T> : IEnumerator<T>, IEnumerable<T>
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly ILocker _locker;

        // locked => indicates if the LockSlim has already been locked (MUST be a read lock if so), if not, this will get a read lock
        public SyncEnumerator(IEnumerable<T> enumerable, ILocker locker, bool locked = false)
            : this(enumerable, locker, Globals.Timeout, locked)
        {
        }

        public SyncEnumerator(IEnumerable<T> enumerable, ILocker locker, TimeSpan timeout, bool locked = false)
        {
            _locker = locker;
            if (!locked)
            {
                if(!_locker.TryEnterReadLock(timeout))
                {
                    throw new TimeoutException();
                }
            }
            _enumerator = enumerable.GetEnumerator();
        }

        public void Dispose()
        {
            _locker.ExitReadLock();
        }

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            _enumerator.Reset();
        }

        public T Current
        {
            get { return _enumerator.Current; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}
