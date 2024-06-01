using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example6;

internal class TestService
{
    private readonly HttpClient httpClient;
    public TestService() 
    { 
        httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5022/"),
        };
    }

    public async Task Process(int key)
    {
        Console.WriteLine($"Starting {key} with thread {Thread.CurrentThread.ManagedThreadId} *******");
        await SendRequest();
        Console.WriteLine($"Finished {key} with thread {Thread.CurrentThread.ManagedThreadId} *******");
    }

    private async Task SendRequest()
    {
        var response = await httpClient.GetAsync("values");
        var jsonResponse = await response.Content.ReadAsStringAsync();
    }
}
