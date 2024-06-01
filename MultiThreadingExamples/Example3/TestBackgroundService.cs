
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Example3;

public class TestBackgroundService : BackgroundService
{
    private readonly ILogger<TestBackgroundService> logger;
    public TestBackgroundService(ILogger<TestBackgroundService> logger)
    {
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var list = Enumerable.Range(1, 5).ToList();

        this.logger.LogInformation($"Main thread {Thread.CurrentThread.ManagedThreadId}");

        var stopWatch = Stopwatch.StartNew();
        var testService = new TestService();
        var tasks = list.Select(x => testService.Process(x));
        await Task.WhenAll(tasks);

        this.logger.LogInformation($"Stop Main thread {Thread.CurrentThread.ManagedThreadId} total time {stopWatch.Elapsed.TotalMilliseconds}");
        
        // Output:
//    info: Example3.TestBackgroundService[0]
//      Main thread 1
//Starting 1 with thread 1 * ******
//Starting 2 with thread 1 * ******
//Starting 3 with thread 1 * ******
//Starting 4 with thread 1 * ******
//Starting 5 with thread 1 * ******
//info: Microsoft.Hosting.Lifetime[14]
//      Now listening on: http://localhost:5129
//    info: Microsoft.Hosting.Lifetime[0]
//      Application started. Press Ctrl+C to shut down.
//info: Microsoft.Hosting.Lifetime[0]
//      Hosting environment: Development
//info: Microsoft.Hosting.Lifetime[0]
//      Content root path: C:\Workspace\Work\14Demo\Demos\MultiThreadingExamples\Example3
//Finished 5 with thread 5 * ******
//Finished 3 with thread 10 * ******
//Finished 4 with thread 7 * ******
//Finished 1 with thread 5 * ******
//Finished 2 with thread 8 * ******
//info: Example3.TestBackgroundService[0]
//      Stop Main thread 8 total time 1017.355
    }
}
