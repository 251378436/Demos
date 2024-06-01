using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1;

internal class TestService
{
    public async Task Process(int key)
    {
        Console.WriteLine($"Starting {key} with thread {Thread.CurrentThread.ManagedThreadId} *******");
        await Task.Delay(1000);
        Console.WriteLine($"Finished {key} with thread {Thread.CurrentThread.ManagedThreadId} *******");
    }
}
