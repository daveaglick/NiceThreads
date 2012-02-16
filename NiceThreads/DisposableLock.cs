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
