using Microsoft.AspNetCore.Mvc;

namespace Example5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {

        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<int>> Get()
        {
            _logger.LogInformation($"Current thread {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszz")}");
            await Task.Delay(1);
            return Ok(11);
        }

        [HttpGet("delay")]
        public async Task<ActionResult<int>> GetDelay()
        {
            _logger.LogInformation($"Current thread {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszz")}");
            await Task.Delay(3000);
            return Ok(12);
            // Output:
            //info: Example5.Controllers.ValuesController[0]
            //        Current thread 4 2024 - 06 - 01T22: 02:31 + 12
            //info: Example5.Controllers.ValuesController[0]
            //        Current thread 4 2024 - 06 - 01T22: 02:31 + 12
            //info: Example5.Controllers.ValuesController[0]
            //        Current thread 7 2024 - 06 - 01T22: 02:31 + 12
            //info: Example5.Controllers.ValuesController[0]
            //        Current thread 7 2024 - 06 - 01T22: 02:31 + 12
            //info: Example5.Controllers.ValuesController[0]
            //        Current thread 12 2024 - 06 - 01T22: 02:31 + 12
            //info: Example5.Controllers.ValuesController[0]
            //        Current thread 12 2024 - 06 - 01T22: 02:31 + 12
            //info: Example5.Controllers.ValuesController[0]
            //        Current thread 12 2024 - 06 - 01T22: 02:32 + 12
            //info: Example5.Controllers.ValuesController[0]
            //        Current thread 7 2024 - 06 - 01T22: 02:32 + 12
            //info: Example5.Controllers.ValuesController[0]
            //        Current thread 12 2024 - 06 - 01T22: 02:32 + 12
            //info: Example5.Controllers.ValuesController[0]
            //        Current thread 12 2024 - 06 - 01T22: 02:32 + 12

        }
    }
}
