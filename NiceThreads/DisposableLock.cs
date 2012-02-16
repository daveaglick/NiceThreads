using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NiceThreads
{
    public abstract class DisposableLock : IDisposable
    {
        private readonly ILocker _locker;

        protected DisposableLock(ILocker locker)
        {
            _locker = locker;
        }

        public ILocker Locker
        {
            get { return _locker; }
        }

        public abstract void Dispose();
    }
}
