using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace Drac.Threading
{
    public class UpgradeableReadLock : IDisposable
    {
        private readonly ReaderWriterLockSlim _lockSlim;

        public UpgradeableReadLock(ReaderWriterLockSlim lockSlim)
        {
            _lockSlim = lockSlim;
            _lockSlim.EnterUpgradeableReadLock();
        }

        public void Dispose()
        {
            _lockSlim.ExitUpgradeableReadLock();
        }
    }
}
