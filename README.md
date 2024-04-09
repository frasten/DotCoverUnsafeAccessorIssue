# DotCoverUnsafeAccessorIssue

DotCover v2023.3 seems to have issues with [UnsafeAccessor](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.unsafeaccessorattribute?view=net-8.0), a new feature introduced in .NET 8 in order to avoid Reflection to access private fields/methods/properties with better performances.

In order to reproduce the bug, just run the tests inside Rider (I'm using NUnit, I don't know if this is related) and Cover the tests.

You should get 0% coverage, but DotCover is internally crashing:

open Tests => Session Options => Diagnostics => Show Last Launch Log.

Log from the issue:

```
15:14:48.640 |I| Profiler core message: Error The Method has no associated IL. (0x80131354)
Details: Can't get function body: CORPROF_E_FUNCTION_NOT_IL The Method has no associated IL. (hresult_error:80131354)
[location] = Z:\BuildAgent\work\c8e645325bb83aab\Profiler\Native\Solution\core\src\IL\Var\var_instrumenter.cpp(1430)
[function] = bool __cdecl jbprof::var_instrumenter::inject(unsigned __int64)
[method name] = ReadPrivateValue
[module name] = D:\Develop\prove\DotCoverUnsafeAccessorIssue\bin\Debug\net8.0\DotCoverUnsafeAccessorIssue.dll
[token] = 06000004, MethodDef
[type name] = DotCoverUnsafeAccessorIssue.TestableClass (DateTime=2024-04-09T15:14:48.6386523+02:00, HResult=CORPROF_E_FUNCTION_NOT_IL, Pid=2720, Executable=C:\Program Files\dotnet\dotnet.exe)
15:14:48.643 |E| Profiler: CoreAlert event received: run=ba9a41b8-36dd-4595-a903-93e842c95bfe, The Method has no associated IL. (0x80131354) Details: Can't get function body: CORPROF_E_FUNCTION_NOT_IL The Method has no associated IL. (hresult_error:80131354) [location] = Z:\BuildAgent\work\c8e645325bb83aab\Profiler\Native\Solution\core\src\IL\Var\var_instrumenter.cpp(1430) [function] = bool __cdecl jbprof::var_instrumenter::inject(unsigned __int64) [method name] = ReadPrivateValue [module name] = D:\Develop\prove\DotCoverUnsafeAccessorIssue\bin\Debug\net8.0\DotCoverUnsafeAccessorIssue.dll [token] = 06000004, MethodDef [type name] = DotCoverUnsafeAccessorIssue.TestableClass (DateTime=2024-04-09T15:14:48.6386523+02:00, HResult=CORPROF_E_FUNCTION_NOT_IL, pid=2720)

--- EXCEPTION #1/1 [LoggerException]
Message = “
  Profiler: CoreAlert event received: run=ba9a41b8-36dd-4595-a903-93e842c95bfe, The Method has no associated IL. (0x80131354)
  Details: Can't get function body: CORPROF_E_FUNCTION_NOT_IL The Method has no associated IL. (hresult_error:80131354)
  [location] = Z:\BuildAgent\work\c8e645325bb83aab\Profiler\Native\Solution\core\src\IL\Var\var_instrumenter.cpp(1430)
  [function] = bool __cdecl jbprof::var_instrumenter::inject(unsigned __int64)
  [method name] = ReadPrivateValue
  [module name] = D:\Develop\prove\DotCoverUnsafeAccessorIssue\bin\Debug\net8.0\DotCoverUnsafeAccessorIssue.dll
  [token] = 06000004, MethodDef
  [type name] = DotCoverUnsafeAccessorIssue.TestableClass (DateTime=2024-04-09T15:14:48.6386523+02:00, HResult=CORPROF_E_FUNCTION_NOT_IL, pid=2720)
”
ExceptionPath = Root
ClassName = JetBrains.Util.LoggerException
HResult = COR_E_APPLICATION=80131600
StackTraceString = “
  at JetBrains.ReSharper.UnitTestFramework.Execution.Launch.UnitTestLaunchOutput.Log(LoggingLevel level, String message, Exception exception) in Log.il:line IL_0003 mvid 13A4
     at JetBrains.Common.UnitTesting.HostController.ProfilerTaskRunnerHostController.ProfilerResultsHandlerWithLogging.OnCoreAlert(AlertEventArgs args, Int32 processId, String executable) in OnCoreAlert.il:line IL_0000 mvid 9661
     at JetBrains.ProfilingInstance.ProfilerResultsHandlersCollection.<>c.<OnCoreAlert>b__19_0(IProfilerResultsHandler handler, CoreAlertEventParams ep, ProfileeProcessInfo _) in ProfilerResultsHandlersCollection.il:line IL_0000 mvid E68E
     at JetBrains.ProfilingInstance.ProfilerResultsHandlersCollection.ProcessProfilerEvent[TEventParams](TEventParams eventParams, ICollection`1 eventsCollection, Action`3 action, Func`2 getCoreIdentity) in ProcessProfilerEvent.il:line IL_0053 mvid E68E
     at JetBrains.ProfilingInstance.ProfilerResultsHandlersCollection.OnCoreAlert(AlertEventArgs args, Int32 processId, String executable) in OnCoreAlert.il:line IL_0044 mvid E68E
     at JetBrains.ProfilingInstance.ProfilerResultsHandlersCollection.<>c__DisplayClass15_0.<BeforeProfilingStarted>b__1(AlertEventArgs args) in ProfilerResultsHandlersCollection.il:line IL_0000 mvid E68E
     at JetBrains.DataFlow.Signal`1.Fire(TValue value, Object cookie) in Fire.il:line IL_0054 mvid 3379
     at JetBrains.Profiler.Profile.Bridge.Impl.CommandProcessor.FireAlert(AlertEventArgs args) in FireAlert.il:line IL_000A mvid F566
     at JetBrains.Profiler.Profile.Bridge.Impl.CommandProcessor.Process(Byte answer, BinaryReader reader) in Process.il:line IL_05F2 mvid F566
     at JetBrains.Profiler.Profile.Bridge.Impl.MeasureCommandProcessor.Process(Byte answer, BinaryReader reader) in Process.il:line IL_027C mvid F566
     at JetBrains.Profiler.Windows.Impl.Bridge.BridgeThread.<>c__DisplayClass10_1.<DoAccept>b__3(Byte answer, BinaryReader reader) in BridgeThread.il:line IL_0000 mvid 8140 or BridgeThread.il:line IL_0027 mvid 8140 or BridgeThread.il:line IL_0100 mvid 8140
     at JetBrains.Profiler.Windows.Impl.PacketReader.Receive[TResult](Func`3 dataReader) in Receive.il:line IL_0034 mvid 8140
     at JetBrains.Profiler.Windows.Impl.Bridge.BridgeThread.<>c__DisplayClass10_0.<DoAccept>b__0(Lifetime lifetime) in BridgeThread.il:line IL_0000 mvid 8140 or BridgeThread.il:line IL_0027 mvid 8140 or BridgeThread.il:line IL_0100 mvid 8140
     at JetBrains.Lifetimes.Lifetime.Using(Action`1 action) in Using.il:line IL_0014 mvid CC54
     at JetBrains.Profiler.Windows.Impl.Bridge.BridgeThread.DoAccept(Guid streamId, Func`1 hasStopRequest) in DoAccept.il:line IL_0064 mvid 8140
     at JetBrains.Profiler.Windows.Impl.Bridge.BridgeThread.<>c__DisplayClass8_1.<DoWork>b__3() in BridgeThread.il:line IL_0000 mvid 8140 or BridgeThread.il:line IL_0027 mvid 8140 or BridgeThread.il:line IL_0100 mvid 8140
     at JetBrains.Util.ILoggerEx.Catch(ILogger thіs, Action F, ExceptionOrigin origin, LoggingLevel loggingLevel) in Catch.il:line IL_0060 mvid 3379
     at JetBrains.Profiler.Windows.Impl.Bridge.BridgeThread.<>c__DisplayClass8_1.<DoWork>b__2() in BridgeThread.il:line IL_0000 mvid 8140 or BridgeThread.il:line IL_0027 mvid 8140 or BridgeThread.il:line IL_0100 mvid 8140
”
```
