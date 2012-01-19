using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Drac.Threading
{
    public interface ISyncObject<T>
    {
        ReaderWriterLockSlim LockSlim { get; }
        T Sync { get; }
        T Unsync { get; }
        IDisposable ReadLock();
        IDisposable UpgradeableReadLock();
        IDisposable WriteLock();
        IDisposable ReadLock(TimeSpan timeout);
        IDisposable UpgradeableReadLock(TimeSpan timeout);
        IDisposable WriteLock(TimeSpan timeout);
        void DoRead(Action<T> action);
        void DoWrite(Action<T> action);
        TR DoRead<TR>(Func<T, TR> func);
        TR DoWrite<TR>(Func<T, TR> func);
    }
}
