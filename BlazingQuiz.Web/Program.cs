using BlazingQuiz.Web;
using BlazingQuiz.Web.Apis;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
ConfigureRifit(builder.Services); 

await builder.Build().RunAsync();

static void ConfigureRifit(IServiceCollection services)
{
    const string apiBaseUrl = "https://localhost:7029";
    services.AddRefitClient<IAuthApi>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl));
}
