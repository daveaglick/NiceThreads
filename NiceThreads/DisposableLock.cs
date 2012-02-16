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
    /// Base class for ILocker wrappers that implement the disposable pattern.
    /// </summary>
    public abstract class DisposableLock : IDisposable
    {
        private readonly ILocker _locker;

        protected DisposableLock(ILocker locker)
        {
            _locker = locker;
        }

        /// <summary>
        /// Gets the ILocker that this DisposableLock wraps.
        /// </summary>
        public ILocker Locker
        {
            get { return _locker; }
        }

        /// <summary>
        /// Unlocks the ILocker.
        /// </summary>
        public abstract void Dispose();
    }
}
