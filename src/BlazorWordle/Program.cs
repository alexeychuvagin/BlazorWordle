using BlazorWordle;
using BlazorWordle.Clients;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<WordsClient>(cfg =>
    cfg.BaseAddress = builder.HostEnvironment.IsProduction()
        ? new Uri(builder.HostEnvironment.BaseAddress + "/BlazorWordle")
        : new Uri(builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();