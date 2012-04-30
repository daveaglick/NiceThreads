NiceThreads is a simple threading utility library designed to make multiple .NET threading classes easier to use through a more consistent API and various convenience classes. It started out of frustration with the different options (and more specifically, the different APIs) for enabling thread safety and locks in the .NET framework and how much code was required to use some of them. NiceThreads provides a consistent interface for standard [Monitor](http://msdn.microsoft.com/en-us/library/system.threading.monitor.aspx) locks and the [ReaderWriterLockSlim](http://msdn.microsoft.com/en-us/library/system.threading.readerwriterlockslim.aspx) class (and possibly others in the future). It also provides support for activating and deactivating these locking primitives through the [disposable pattern](http://msdn.microsoft.com/en-us/library/system.idisposable.aspx). Finally, it provides wrappers that can easily provide thread-safety to unsafe objects.

View the API documentation here: http://somedave.github.com/NiceThreads/

Common API
===

Both Monitor (and the "lock" statement which is syntactic sugar for Monitor) and ReaderWriterLockSlim attempt to solve the same problem: preventing conflicting concurrent access to objects that might need to be read or written to by multiple threads. They both do this by limiting access to the object to one thread at a time (or in the case of read locks provided by ReaderWriterLockSlim, only to threads that signal they want read-only access) while making other threads wait their turn. However, even though both classes provide similar functionality they are intended for different uses and have [different tradeoffs](http://blogs.msdn.com/b/pedram/archive/2007/10/07/a-performance-comparison-of-readerwriterlockslim-with-readerwriterlock.aspx). Further, they use similar but different APIs making switching between them difficult.

To solve this problem, NiceThreads provides a consistent ILocker interface that has implementations wrapping both classes and provides a consistent API. The rest of NiceThreads is designed to interact with ILocker allowing interchangeable use of the different types of locking primitives. In addition, the ILocker interface can be used directly to provide a consistent wrapper around either locking class for your own code.

For example:
```
ILocker locker = new ReaderWriterLockSlimLocker();
locker.EnterReadLock();
// Do work...
locker.ExitReadLock();
locker = new MonitorLocker();
locker.EnterReadLock();
// Do work...
locker.ExitReadLock();
```

Disposable Pattern
===

Both locking primitives require explicitly activating the lock and subsiquently manually removing the lock when finished. This can lead to problems if the developer forgets to release the lock or ends up exiting the normal program flow (for example, because an exception was thrown). The "lock" keyword in C# attempts to make this design easier to use for the Monitor class by abstracting Monitor instantiation and surrounding it's use in a control block, however, no such keyword exists for other locking classes such as ReaderWriterLockSlim. In addition, using the "lock" keyword means some control is lost over the lifecycle and usage of the underlying Monitor class.

NiceThreads attempts to solve this problem by providing a set of classes that implement IDisposable and wraps an underlying ILocker (which in turn provides consistent access to alternate framework locking classes). They activate the requested lock type on instantiation and free it on disposal. This allows the developer to use the built-in support for the disposable pattern in .NET to automatically free a lock when finished with it by using the "using" statement.

For example:
```
ILocker locker = new ReaderWriterLockSlimLocker();
using(new ReadLock(locker))
{
  // Do work...
}
```

Thread-Safe Wrappers
===

Even with the added convenience of a consistent API and disposable pattern support, implementing thread-safety for non-thread-safe objects can still require a fair amount of code. For every object that needs to be protected, a new locking object potentially needs to be created and maintained. NiceThreads helps implement thread safety for objects by providing wrapper classes that encapsulate generic locking logic and provide thread-safe access to their underlying object. SyncObject<T> wraps an arbitrary type and ReadOnlySyncObject<T> wraps an arbitrary type while providing "readonly" semantics (I.e., once the ReadOnlySyncObject<T> has been constructed, it's underlying object cannot be changed). These classes provide a variety of methods to expose their wrapped object in thread-safe ways including thread-safe getting and setting, disposable pattern access, and action/function providers (I.e., lambdas or anonymous methods).

For example:
```
SyncObject<int> num = new SyncObject<int>(10);
num.Sync = 20;  // Access as a property with a thread-safe setter
int value = num.Sync;   // Access as a property with a thread-safe getter
using(num.WriteLock())
{
  // We can now access using unsafe code
  num.UnsyncField++;  // Provides direct field access
  value = num.Unsync; // Access as a property with an unsafe getter
}
num.DoWrite(n => n + 10);   // Thread-safe write with an Action
value = num.DoRead(n => n + 100);   // Thread-safe read with a Func
```

License
===

Copyright 2012 WildCard, LLC

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.