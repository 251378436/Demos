namespace Example4;

internal class TestService
{
    public async Task Process(int key)
    {
        Console.WriteLine($"Starting {key} with thread {Thread.CurrentThread.ManagedThreadId} *******");
        var result = await GetValue(key);
        Console.WriteLine($"Finished {result} with thread {Thread.CurrentThread.ManagedThreadId} *******");
    }

    private Task<int> GetValue(int key) { return Task.FromResult(key);}
}
