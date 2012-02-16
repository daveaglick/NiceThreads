using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NiceThreads
{
    //Mimics the semantics of a readonly field in that the underlying object cannot be changed once constructed
    public class ReadOnlySyncObject<T> : ISyncObject<T>
    {
        private readonly ILocker _locker;

        public ILocker Locker
        {
            get { return _locker; }
        }

        public readonly T UnsyncField;    //Expose this directly so it can be used anywhere the variable is expected (such as in ref or out parameters)

        public T Unsync
        {
            get { return UnsyncField; }
        }

        public T Sync
        {
            get { using (ReadLock()) { return UnsyncField; } }
        }

        public ReadOnlySyncObject(T value)
            : this(value, Globals.GetDefaultLocker())
        {
        }

        public ReadOnlySyncObject(T value, ILocker locker)
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
