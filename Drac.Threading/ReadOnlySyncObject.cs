using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    //Mimics the semantics of a readonly field in that the underlying object cannot be changed once constructed
    public class ReadOnlySyncObject<T> : ISyncObject<T>
    {
        private readonly ReaderWriterLockSlim _lockSlim = new ReaderWriterLockSlim();

        public ReaderWriterLockSlim LockSlim
        {
            get { return _lockSlim; }
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
        {
            UnsyncField = value;
        }

        public IDisposable ReadLock()
        {
            return new ReadLock(_lockSlim);
        }

        public IDisposable UpgradeableReadLock()
        {
            return new UpgradeableReadLock(_lockSlim);
        }

        public IDisposable WriteLock()
        {
            return new WriteLock(_lockSlim);
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
