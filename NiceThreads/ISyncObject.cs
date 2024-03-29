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
    /// Provides a thread-safe wrapper around objects that normally wouldn't be thread-safe.
    /// </summary>
    /// <typeparam name="T">The type of the object to be wrapped.</typeparam>
    public interface ISyncObject<T>
    {
        /// <summary>
        /// Gets the Locker used to manage access to the wrapped object.
        /// </summary>
        Locker Locker { get; }

        /// <summary>
        /// Gets a thread-safe synchronized reference to the wrapped object.
        /// </summary>
        T Sync { get; }

        /// <summary>
        /// Gets an unsafe/unsynchronized reference to the wrapped object.
        /// </summary>
        T Unsync { get; }

        /// <summary>
        /// Gets a disposable read lock for the wrapped object.
        /// After obtaining the lock, the wrapped object can safely be
        /// used with the Unsync property until the lock is disposed.
        /// </summary>
        /// <returns>A object that holds the lock until it is disposed.</returns>
        IDisposable ReadLock();

        /// <summary>
        /// Gets a disposable upgradeable read lock for the wrapped object.
        /// After obtaining the lock, the wrapped object can safely be
        /// used with the Unsync property until the lock is disposed.
        /// </summary>
        /// <returns>A object that holds the lock until it is disposed.</returns>
        IDisposable UpgradeableReadLock();

        /// <summary>
        /// Gets a disposable write lock for the wrapped object. 
        /// After obtaining the lock, the wrapped object can safely be
        /// used with the Unsync property until the lock is disposed.
        /// </summary>
        /// <returns>A object that holds the lock until it is disposed.</returns>
        IDisposable WriteLock();

        /// <summary>
        /// Gets a disposable read lock for the wrapped object.
        /// After obtaining the lock, the wrapped object can safely be
        /// used with the Unsync property until the lock is disposed.
        /// </summary>
        /// <param name="timeout">The amount of time to wait for the lock before an exception is thrown.</param>
        /// <returns>A object that holds the lock until it is disposed.</returns>
        IDisposable ReadLock(TimeSpan timeout);

        /// <summary>
        /// Gets a disposable upgradeable read lock for the wrapped object.
        /// After obtaining the lock, the wrapped object can safely be
        /// used with the Unsync property until the lock is disposed.
        /// </summary>
        /// <param name="timeout">The amount of time to wait for the lock before an exception is thrown.</param>
        /// <returns>A object that holds the lock until it is disposed.</returns>
        IDisposable UpgradeableReadLock(TimeSpan timeout);

        /// <summary>
        /// Gets a disposable write lock for the wrapped object. 
        /// After obtaining the lock, the wrapped object can safely be
        /// used with the Unsync property until the lock is disposed.
        /// </summary>
        /// <param name="timeout">The amount of time to wait for the lock before an exception is thrown.</param>
        /// <returns>A object that holds the lock until it is disposed.</returns>
        IDisposable WriteLock(TimeSpan timeout);

        /// <summary>
        /// Locks the wrapped object for reading for the duration of an action.
        /// </summary>
        /// <param name="action">The action.</param>
        void DoRead(Action<T> action);

        /// <summary>
        /// Locks the wrapped object for writing for the duration of an action.
        /// </summary>
        /// <param name="action">The action.</param>
        void DoWrite(Action<T> action);

        /// <summary>
        /// Locks the wrapped object for reading for the duration of a function.
        /// </summary>
        /// <typeparam name="TR">The return type.</typeparam>
        /// <param name="func">The function.</param>
        /// <returns>The result of the function.</returns>
        TR DoRead<TR>(Func<T, TR> func);

        /// <summary>
        /// Locks the wrapped object for writing for the duration of a function.
        /// </summary>
        /// <typeparam name="TR">The return type.</typeparam>
        /// <param name="func">The function.</param>
        /// <returns>The result of the function.</returns>
        TR DoWrite<TR>(Func<T, TR> func);
    }
}
