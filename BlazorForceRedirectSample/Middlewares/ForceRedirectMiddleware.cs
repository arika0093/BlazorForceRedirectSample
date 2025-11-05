using BlazorForceRedirectSample.Services;

namespace BlazorForceRedirectSample.Middlewares;

// 素直に実装(NG)
public class ForceRedirectMiddleware : IMiddleware
{
    private const string RedirectPath = "/setting";

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var service = context.RequestServices.GetRequiredService<SampleSettingService>();
        var isRedirectNeeds = await service.Check();
        var alreadyEntered = context.Request.Path.Equals(
            RedirectPath,
            StringComparison.OrdinalIgnoreCase
        );
        if (isRedirectNeeds && !alreadyEntered)
        {
            context.Response.Redirect(RedirectPath);
        }
        await next(context);
    }
}

// Sec-Fetch-Dest ヘッダーを考慮した実装(Reload時のみOK)
public class ForceRedirectMiddleware2 : IMiddleware
{
    private const string RedirectPath = "/setting";

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var service = context.RequestServices.GetRequiredService<SampleSettingService>();
        var isRedirectNeeds = await service.Check();
        var isDestIsDocument = context.Request.Headers["Sec-Fetch-Dest"] == "document";
        var alreadyEntered = context.Request.Path.Equals(
            RedirectPath,
            StringComparison.OrdinalIgnoreCase
        );
        if (isRedirectNeeds && isDestIsDocument && !alreadyEntered)
        {
            context.Response.Redirect(RedirectPath);
        }
        await next(context);
    }
}
