using BlazorWordle;
using BlazorWordle.Clients;
using BlazorWordle.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpClient<WordsClient>(cfg => cfg.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddTransient<GameState>();

await builder.Build().RunAsync();