using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cabother.Swagger.UI.Infra.Options
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IConfiguration _configuration;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration)
        {
            _provider = provider;
            _configuration = configuration;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, _configuration));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, IConfiguration config)
        {
            var apiOptions = config.GetSection("ApiDocumentation");

            var info = new OpenApiInfo()
            {
                Title = apiOptions.GetValue<string>("AppName"), 
                Description = apiOptions.GetValue<string>("Description"),
                Version = description.ApiVersion.ToString(),
                Contact = new OpenApiContact
                {
                    Name = apiOptions.GetValue<string>("Owner"),
                    Url = new System.Uri(apiOptions.GetValue<string>("Url")),
                    Email = apiOptions.GetValue<string>("Email")
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}