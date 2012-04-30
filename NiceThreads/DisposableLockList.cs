using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NiceThreads
{
#if DEBUG
    internal class DisposableLockList
    {
        private readonly LinkedList<DisposableLock> _locks = new LinkedList<DisposableLock>();
 
        public LinkedListNode<DisposableLock> AddLock(DisposableLock disposableLock)
        {
            lock(_locks)
            {
                return _locks.AddLast(disposableLock);
            }
        }

        public void RemoveLock(LinkedListNode<DisposableLock> node)
        {
            lock(_locks)
            {
                _locks.Remove(node);
            }
        }

        public void AppendLog(StringBuilder builder)
        {
            DateTime now = DateTime.UtcNow;
            string ns = GetType().Namespace;
            builder.AppendLine("Lock Type\tThread Id\tWait Time\tHeld Time");
            builder.AppendLine("\tMethod, File, Line (If Available)");
            lock(_locks)
            {
                LinkedListNode<DisposableLock> node = _locks.First;
                while(node != null)
                {
                    DisposableLock disposableLock = node.Value;
                    builder.Append(disposableLock.GetType().Name + "\t"
                        + (disposableLock.ThreadId.HasValue ? (disposableLock.ThreadId.Value.ToString() + "\t") : "???\t"));
                    builder.Append(disposableLock.Requested.HasValue
                        ? (now.Subtract(disposableLock.Requested.Value).TotalMilliseconds + "\t") : "???\t");
                    builder.AppendLine(disposableLock.Acquired.HasValue
                        ? now.Subtract(disposableLock.Acquired.Value).TotalMilliseconds.ToString() : "???");
                    if (disposableLock.StackTrace != null)
                    {
                        for (int c = 0; c < disposableLock.StackTrace.FrameCount; c++)
                        {
                            StackFrame frame = disposableLock.StackTrace.GetFrame(c);
                            MethodBase method = frame.GetMethod();
                            if(method.DeclaringType.Namespace != ns)
                            {
                                builder.AppendLine("\t" + method + ", "
                                    + frame.GetFileName() + ", " + frame.GetFileLineNumber());
                            }
                        }
                    }
                    node = node.Next;
                }
            }
        }
    }
#endif
}
