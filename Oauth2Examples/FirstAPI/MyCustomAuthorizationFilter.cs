using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstAPI;

public class MyCustomAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
{
    public MyCustomAuthorizationFilter()
    {
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        await Task.Delay(1);
    }

}
