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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace NiceThreads
{
    /// <summary>
    /// Base class for Locker wrappers that implements the disposable pattern.
    /// </summary>
    public abstract class DisposableLock : IDisposable
    {
        private readonly Locker _locker;
        private bool _disposed = false;

        #if DEBUG

        private static readonly DisposableLockList WaitingLocks = new DisposableLockList();
        private static readonly DisposableLockList HeldLocks = new DisposableLockList();

        private LinkedListNode<DisposableLock> _node;
        private DateTime? _requested = null;
        private DateTime? _acquired = null;
        private StackTrace _stackTrace = null;
        private int? _threadId = null;

        internal int? ThreadId
        {
            get { return _threadId; }
        }

        internal StackTrace StackTrace
        {
            get { return _stackTrace; }
        }

        internal DateTime? Acquired
        {
            get { return _acquired; }
        }

        internal DateTime? Requested
        {
            get { return _requested; }
        }

        private void Waiting()
        {
            if (!Globals.EnableLogging) return;
            _stackTrace = new StackTrace(2, true);
            _threadId = Thread.CurrentThread.ManagedThreadId;
            _requested = DateTime.UtcNow;
            _node = WaitingLocks.AddLock(this);
        }

        private void Held()
        {
            if (!Globals.EnableLogging) return;
            WaitingLocks.RemoveLock(_node);
            _acquired = DateTime.UtcNow;
            _node = HeldLocks.AddLock(this);
        }

        /// <summary>
        /// Appends the log information (if logging is enabled) to the StringBuilder.
        /// </summary>
        /// <param name="builder">The builder to append log information to.</param>
        public static void AppendLog(StringBuilder builder)
        {
            if (!Globals.EnableLogging) return;
            builder.AppendLine("Waiting");
            builder.AppendLine("-------");
            WaitingLocks.AppendLog(builder);
            builder.AppendLine("Held");
            builder.AppendLine("-------");
            HeldLocks.AppendLog(builder);
        }

        /// <summary>
        /// Outputs log information (if logging is enabled).
        /// </summary>
        public static string Log()
        {
            StringBuilder builder = new StringBuilder();
            AppendLog(builder);
            return builder.ToString();
        }

        #endif

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <param name="timeout">The timeout.</param>
        protected DisposableLock(Locker locker, TimeSpan timeout)
        {
            _locker = locker;

            #if DEBUG
                Waiting();
            #endif

            try
            {
                EnterLock(timeout);
            }
            catch (Exception)
            {
                #if DEBUG
                    if(_node != null) WaitingLocks.RemoveLock(_node);
                #endif
                throw;
            }

            #if DEBUG
                Held();
            #endif
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="locker">The locker.</param>
        protected DisposableLock(Locker locker)
        {
            _locker = locker;

            #if DEBUG
                Waiting();

                if(Globals.Timeout.HasValue)
                {
                    try
                    {
                        EnterLock(Globals.Timeout.Value);
                    }
                    catch (Exception)
                    {
                        #if DEBUG
                            if(_node != null) WaitingLocks.RemoveLock(_node);
                        #endif
                        throw;
                    }
                    Held();
                    return;
                }
            #endif

            EnterLock();

            #if DEBUG   
                Held();
            #endif
        }

        /// <summary>
        /// Gets the Locker that this lock wraps.
        /// </summary>
        public Locker Locker
        {
            get { return _locker; }
        }

        /// <summary>
        /// Unlocks the Locker.
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;
            #if DEBUG
                if(_node != null) HeldLocks.RemoveLock(_node);
            #endif
            ExitLock();
            _disposed = true;
        }

        protected abstract void EnterLock();
        protected abstract void EnterLock(TimeSpan timeout);
        protected abstract void ExitLock();
    }
}
