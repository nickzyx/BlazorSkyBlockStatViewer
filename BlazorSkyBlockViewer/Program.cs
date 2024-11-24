using BlazorSkyBlockViewer.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Load appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.RootComponents.Add<App>("#app");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register HypixelService with IConfiguration
builder.Services.AddScoped<HypixelService>();
builder.Services.AddScoped(sp => new HypixelService(
    sp.GetRequiredService<HttpClient>(),
    builder.Configuration.GetSection("HypixelAPI:ApiKey").Value
));

await builder.Build().RunAsync();
