## Introduction
This demo only shows how tasks / threads work in .Net Console app and Asp.Net Web api as examples

Xamarin, UWP, WPF or other client side .Net applications has main UI thread, it might be a little bit different.

The output of these examples has been put in the comments in program.cs /controller / background service class.

### Example 1 - Console app:
multi tasks, they starts with the same thread, when it meets Task.Delay, it uses another thread

### Example 2 - Console app:
multi tasks with Task.Run, they starts with the different thread, when it meets Task.Delay, it uses another thread

### Example 3 - Asp.Net Web API:
multi tasks in background service, they starts with the same thread, when it meets Task.Delay, it uses another thread

### Example 4 - Console app:
multi tasks with Task.Run, they starts with the same thread, when it meets await, it still uses the same thread

### Example 5 - Asp.Net Web API:
This example shows how controller use the threads. When there are multiple requests coming to API. It may use the same thread or different threads.
https://medium.com/geekculture/performance-testing-with-jmeter-af94a397cd0b

### Example 6 - Console app:
multi tasks with Task.Run, they starts with the different thread, when it meets await http request for API (Example 5), it uses another thread

## Other concerns
Exception in multi tasks, we can only get one exception when using Task.WhenAll. If we need all the exceptions, see this blog.
https://jeremybytes.blogspot.com/2020/09/await-taskwhenall-shows-one-exception.html