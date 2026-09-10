using System.Net.Http.Headers;
using System.Net.Mime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Toxic.EId.Sdk.Features.BankId;

namespace Toxic.EId.Sdk;

public static class Extensions
{
    extension(IServiceCollection serviceCollection)
    {
        /// <summary>
        /// Adds the required services to use <see cref="BankIdClient"/>.
        /// Adds the ability to configure BankID options through <code>builder.Services.Configure{EIdOptions}()</code>.
        /// </summary>
        public IServiceCollection AddBankIdClient()
        {
            serviceCollection
                .AddOptions<EIdOptions>()
                .Configure<IConfiguration>((opts, config) => config.GetSection("eId").Bind(opts));

            serviceCollection.AddHttpClient<BankIdClient>((services, client) =>
            {
                var options = services.GetRequiredService<IOptions<EIdOptions>>();

                if (string.IsNullOrWhiteSpace(options.Value.ApiKey))
                {
                    throw new InvalidOperationException("eId:ApiKey must be specified");
                }

                if (string.IsNullOrWhiteSpace(options.Value.ServiceKey))
                {
                    throw new InvalidOperationException("eId:ServiceKey must be specified");
                }

                client.BaseAddress = new Uri(options.Value.IsTest ? "https://client-test.grandid.com/json1.1/" : "https://client.grandid.com/json1.1/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            });

            return serviceCollection;
        }
    }
}
