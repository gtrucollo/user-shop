using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using MudBlazor.Services;
using UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Settings settings = builder.Configuration.GetSection("BackEndSetting").Get<Settings>();

builder.Services.AddHttpClient<FactoryService>(service => service.BaseAddress = new Uri("https://localhost:7127/"));

builder.Services.AddMudServices();

await builder.Build().RunAsync();

internal class Settings
{
    public string Url { get; set; }
}