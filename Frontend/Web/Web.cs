using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Olympus.Frontend.Interface;
using Olympus.Frontend.Interface.Services;
using Olympus.Frontend.Web.Services;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Routes>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddRadzenComponents();

// builder.Services.AddSingleton<ThemingService>();

builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
