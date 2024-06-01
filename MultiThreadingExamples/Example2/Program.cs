﻿using Example2;
using System.Diagnostics;

var list = Enumerable.Range(1, 5).ToList();

Console.WriteLine($"Main thread {Thread.CurrentThread.ManagedThreadId}");

var stopWatch = Stopwatch.StartNew();
var testService = new TestService();
var tasks = list.Select(x => Task.Run(() => testService.Process(x)));
await Task.WhenAll(tasks);

Console.WriteLine($"Stop Main thread {Thread.CurrentThread.ManagedThreadId} total time {stopWatch.Elapsed.TotalMilliseconds}");

// Output:
//Main thread 1
//Starting 1 with thread 5 *******
//Starting 2 with thread 8 *******
//Starting 3 with thread 10 *******
//Starting 4 with thread 11 *******
//Starting 5 with thread 12 *******
//Finished 1 with thread 12 *******
//Finished 4 with thread 8 *******
//Finished 2 with thread 11 *******
//Finished 3 with thread 10 *******
//Finished 5 with thread 7 *******
//Stop Main thread 7 total time 1007.4829