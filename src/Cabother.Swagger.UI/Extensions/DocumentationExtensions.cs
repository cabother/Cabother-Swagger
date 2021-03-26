using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Cabother.Swagger.UI.Infra.Options;

namespace Cabother.Swagger.UI.Extensions
{
    /// <summary>
    /// Extensões para configuração da documentação das APIs
    /// </summary>
    public static class DocumentationExtensions
    {
        /// <summary>
        /// Adiciona a documentação no container de injeção de dependência
        /// </summary>
        /// <param name="services">Container de injeção de dependência</param>
        public static void AddDocumentations(this IServiceCollection services, IConfiguration configuration)
        {
            var apiOptions = configuration.GetSection("ApiDocumentation");

            services.Configure<ApiDocumentationOptions>(apiOptions);

            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }

        /// <summary>
        /// Adiciona o middleware para exibição da documentação das APIs
        /// </summary>
        /// <param name="app">Configurações do app para sua execução</param>
        public static void UseDocumentations(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
                }

                options.DocExpansion(DocExpansion.List);
            });
        }
    }
}
