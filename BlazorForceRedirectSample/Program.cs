using BlazorForceRedirectSample.Components;
using BlazorForceRedirectSample.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<BlazorForceRedirectSample.Middlewares.ForceRedirectMiddleware>();
//builder.Services.AddScoped<BlazorForceRedirectSample.Middlewares.ForceRedirectMiddleware2>();

// 本来であればScopedが適切だが、今回はCookie等を使わないのでSingletonで動作確認する
builder.Services.AddSingleton<SampleSettingService>();

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

//app.UseMiddleware<BlazorForceRedirectSample.Middlewares.ForceRedirectMiddleware>();
//app.UseMiddleware<BlazorForceRedirectSample.Middlewares.ForceRedirectMiddleware2>();

app.Run();
