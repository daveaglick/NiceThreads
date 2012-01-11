using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    //Based on code from http://www.codeproject.com/KB/cs/safe_enumerable.aspx
    public class SyncEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly ReaderWriterLockSlim _lock;

        public SyncEnumerator(IEnumerable<T> enumerable, ReaderWriterLockSlim lockSlim)
        {
            _lock = lockSlim;
            _lock.EnterReadLock();
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
    }
}
