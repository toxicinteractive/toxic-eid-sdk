using System.Net.Http.Headers;
using System.Net.Mime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Toxic.GrandId.Sdk;

public static class Extensions
{
    extension(IServiceCollection serviceCollection)
    {
        public IServiceCollection AddBankIdClient()
        {
            serviceCollection
                .AddOptions<GrandIdOptions>()
                .Configure<IConfiguration>((opts, config) => config.GetSection("GrandId").Bind(opts));

            serviceCollection.AddHttpClient<BankIdClient>((services, client) =>
            {
                var options = services.GetRequiredService<IOptions<GrandIdOptions>>();

                if (string.IsNullOrWhiteSpace(options.Value.ApiKey))
                {
                    throw new InvalidOperationException("GrandId:ApiKey must be specified");
                }

                if (string.IsNullOrWhiteSpace(options.Value.ServiceKey))
                {
                    throw new InvalidOperationException("GrandId:ServiceKey must be specified");
                }

                client.BaseAddress = new Uri(options.Value.IsTest ? "https://client-test.grandid.com/json1.1/" : "https://client.grandid.com/json1.1/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            });

            return serviceCollection;
        }
    }
}
