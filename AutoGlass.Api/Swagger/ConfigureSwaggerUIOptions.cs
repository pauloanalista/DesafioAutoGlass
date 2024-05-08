using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
namespace AutoGlass.Api.Swagger
{
    public class ConfigureSwaggerUIOptions : IConfigureOptions<SwaggerUIOptions>
    {
        public void Configure(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint(
                $"/swagger/v1/swagger.json",
                $"Meu Dinheiro API - v1.0");

            options.DocExpansion(DocExpansion.List);
        }
    }
}