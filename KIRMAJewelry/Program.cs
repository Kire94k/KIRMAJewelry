using KIRMAJewelry;
using Microsoft.AspNetCore.Components.Web;
using KIRMAJewelry.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");


builder.Services.AddScoped<IBraceletService, BraceletService>();
builder.Services.AddScoped<INecklaceService, NecklaceService>();
builder.Services.AddSingleton<IMessagingService, MessagingService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5231/") });

await builder.Build().RunAsync();