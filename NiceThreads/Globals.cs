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
    /// Contains global methods and properties.
    /// </summary>
    public static class Globals
    {
        private static TimeSpan? _timeout = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Gets or sets a global timeout value. This is used in debug builds for disposable locks as a failsafe.
        /// Even if no timeout is specified, if this is not null (default is 5 minutes) a TimeoutException
        /// will be thrown if the lock cannot be acquired.
        /// </summary>
        /// <value>
        /// The global timeout or null for no global timeout.
        /// </value>
        public static TimeSpan? Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the call stack and initiator thread should be logged
        /// for disposable locks when running in debug mode . This helps with debugging by allowing a
        /// view into all held locks and their points of origination.
        /// </summary>
        /// <value>
        ///   <c>true</c> if logging should be enabled; otherwise, <c>false</c>.
        /// </value>
        public static bool EnableLogging { get; set; }

        private static Func<Locker> _defaultLockerFunc = () => new ReaderWriterLockSlimLocker();

        /// <summary>
        /// Sets the default locker function that is used when a new lock is created (such as for a SyncObject)
        /// an a default Locker instance is needed. By default, this is set to create a new ReaderWriteLockSlimLocker.
        /// </summary>
        /// <value>
        /// The default locker function.
        /// </value>
        public static Func<Locker> DefaultLockerFunc
        {
            set { if (value != null) _defaultLockerFunc = value; }
        }

        // Returns a default Locker type (currently ReaderWriterLockSlim based)
        internal static Locker GetDefaultLocker()
        {
            return _defaultLockerFunc();
        }
    }
}
