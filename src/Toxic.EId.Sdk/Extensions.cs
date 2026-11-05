using System.Buffers.Text;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Toxic.EId.Sdk.Features.BankId;
using Toxic.EId.Sdk.Features.FrejaEId;

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

                client.BaseAddress = new Uri($"{options.Value.Endpoint}/json1.1/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            });

            return serviceCollection;
        }

        /// <summary>
        /// Adds the required services to use <see cref="FrejaEIdClient"/>.
        /// Adds the ability to configure Freja eID options through <code>builder.Services.Configure{EIdOptions}()</code>.
        /// </summary>
        public IServiceCollection AddFrejaEIdClient()
        {
            serviceCollection
                .AddOptions<EIdOptions>()
                .Configure<IConfiguration>((opts, config) => config.GetSection("eId").Bind(opts));

            serviceCollection.AddHttpClient<FrejaEIdClient>((services, client) =>
            {
                var options = services.GetRequiredService<IOptions<EIdOptions>>();

                if (string.IsNullOrWhiteSpace(options.Value.ApiKey))
                {
                    throw new InvalidOperationException("eId:ApiKey must be specified");
                }

                client.BaseAddress = new Uri($"{options.Value.Endpoint}/json1.1/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            });

            return serviceCollection;
        }
    }

    extension (string str)
    {
        public string ToBase64() => 
            Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
    }
}
