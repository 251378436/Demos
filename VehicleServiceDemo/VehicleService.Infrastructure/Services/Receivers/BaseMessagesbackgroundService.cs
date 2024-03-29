using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VehicleService.Domain.Services.UseCases;

namespace VehicleService.Infrastructure.Services.Receivers;

public abstract class BaseMessagesbackgroundService<TMessage, TRequest> : BackgroundService
{
    protected readonly IMessageReceiver<TMessage> messageReceiver;
    private readonly IHostApplicationLifetime applicationLifetime;
    protected readonly ILogger logger;
    protected readonly IUseCase<TRequest> useCase;

    public BaseMessagesbackgroundService(IMessageReceiver<TMessage> messageReceiver,
        IHostApplicationLifetime applicationLifetime,
        ILogger logger,
        IUseCase<TRequest> useCase)
    {
        this.messageReceiver = messageReceiver;
        this.applicationLifetime = applicationLifetime;
        this.logger = logger;
        this.useCase = useCase;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!applicationLifetime.ApplicationStopping.IsCancellationRequested)
        {
            try
            {
                var messages = (await messageReceiver.ReceiveMessagesAsync(stoppingToken)).ToList();

                if (messages != null && messages.Count > 0)
                {
                    using var batchScope = logger.BeginScope(new Dictionary<string, string>()
                    {
                        { "BatchId", Guid.NewGuid().ToString() }
                    });

                    var tasks = messages.Select(m => ProcessMessage(m));
                    await Task.WhenAll(tasks);
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }
    }

    protected virtual async Task ProcessMessage(TMessage message)
    {
        try
        {
            using var individualScope = logger.BeginScope(new Dictionary<string, string>()
            {
                { "IndividualScope", Guid.NewGuid().ToString() }
            });

            var messageBody = await GetMessageBody(message);

            LogMessageBody(messageBody);

            var request = await GetRequest(message, messageBody);

            await ProcessRequest(request);

            await messageReceiver.CompleteMessageAsync(message);

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
        }
    }

    protected abstract Task<string> GetMessageBody(TMessage message);

    protected virtual void LogMessageBody(string messageBody) => logger.LogInformation($"Received message: {messageBody}");

    protected abstract Task<TRequest> GetRequest(TMessage message, string messageBody);

    protected virtual async Task ProcessRequest(TRequest sensorRequest, CancellationToken cancellationToken = default, IDictionary<string, object>? properties = null)
    {
        await useCase.Propcess(sensorRequest, cancellationToken, properties);
    }
}
