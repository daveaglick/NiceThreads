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
        private DisposableLock _lock = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncEnumerator&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="enumerable">The enumerable to protect.</param>
        /// <param name="locker">The locker to use.</param>
        public SyncEnumerator(IEnumerable<T> enumerable, Locker locker)
        {
            _lock = new ReadLock(locker);
            _enumerator = enumerable.GetEnumerator();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncEnumerator&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="enumerable">The enumerable to protect.</param>
        /// <param name="locker">The locker to use.</param>
        /// <param name="timeout">The timeout - if a lock can't be obtained a TimeoutException will be thrown.</param>
        public SyncEnumerator(IEnumerable<T> enumerable, Locker locker, TimeSpan timeout)
        {
            _lock = new ReadLock(locker, timeout);
            _enumerator = enumerable.GetEnumerator();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_lock == null) return;
            _lock.Dispose();
            _lock = null;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            if(_lock == null) throw new ObjectDisposedException("SyncEnumerator");
            return _enumerator.MoveNext();
        }

        /// <inheritdoc />
        public void Reset()
        {
            if (_lock == null) throw new ObjectDisposedException("SyncEnumerator");
            _enumerator.Reset();
        }

        /// <inheritdoc />
        public T Current
        {
            get
            {
                if (_lock == null) throw new ObjectDisposedException("SyncEnumerator");
                return _enumerator.Current;
            }
        }

        /// <inheritdoc />
        object IEnumerator.Current
        {
            get
            {
                if (_lock == null) throw new ObjectDisposedException("SyncEnumerator");
                return Current;
            }
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            if (_lock == null) throw new ObjectDisposedException("SyncEnumerator");
            return this;
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            if (_lock == null) throw new ObjectDisposedException("SyncEnumerator");
            return this;
        }
    }
}
