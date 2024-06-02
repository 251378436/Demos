namespace FirstAPI.Outbound;

public static class OutboundExtentions
{
    public static IServiceCollection RegisterOutbound(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<OutboundAuth>(configuration.GetSection("outboundAuth:auth0_group_tenant"));
        
        services.AddTransient<Auth0HttpHandler>();

        services.AddHttpClient("secondapi", (provider, client) =>
        {
            client.BaseAddress = new Uri("http://localhost:5196/");
        }).AddHttpMessageHandler<Auth0HttpHandler>();

        return services;
    }
}
