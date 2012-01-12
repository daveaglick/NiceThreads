using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    //Based on code from http://www.codeproject.com/KB/cs/safe_enumerable.aspx
    public class SyncEnumerator<T> : IEnumerator<T>, IEnumerable<T>
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly ReaderWriterLockSlim _lock;

        // locked => indicates if the LockSlim has already been locked (MUST be a read lock if so), if not, this will get a read lock
        public SyncEnumerator(IEnumerable<T> enumerable, ReaderWriterLockSlim lockSlim, bool locked = false)
        {
            _lock = lockSlim;
            if (!locked)
            {
                _lock.EnterReadLock();
            }
            _enumerator = enumerable.GetEnumerator();
        }

        public void Dispose()
        {
            _lock.ExitReadLock();
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
