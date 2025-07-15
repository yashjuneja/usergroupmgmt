using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UserGroupManagement.Client.Services;

namespace UserGroupManagement.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7299/") });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5110/") });

            builder.Services.AddScoped<IUserApiService, UserApiService>();
            builder.Services.AddScoped<IGroupApiService, GroupApiService>();

            await builder.Build().RunAsync();
        }
    }
}
