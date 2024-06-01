using Example6;
using System.Diagnostics;

var list = Enumerable.Range(1, 5).ToList();

Console.WriteLine($"Main thread {Thread.CurrentThread.ManagedThreadId}");

var stopWatch = Stopwatch.StartNew();
var testService = new TestService();
var tasks = list.Select(x => testService.Process(x));
await Task.WhenAll(tasks);

Console.WriteLine($"Stop Main thread {Thread.CurrentThread.ManagedThreadId} total time {stopWatch.Elapsed.TotalMilliseconds}");

// Output:
//Main thread 1
//Starting 1 with thread 1 *******
//Starting 2 with thread 1 *******
//Starting 3 with thread 1 *******
//Starting 4 with thread 1 *******
//Starting 5 with thread 1 *******
//Finished 2 with thread 7 *******
//Finished 3 with thread 12 *******
//Finished 5 with thread 11 *******
//Finished 1 with thread 4 *******
//Finished 4 with thread 6 *******
//Stop Main thread 6 total time 104.3974