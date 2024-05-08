using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace AutoGlass.Api.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            options.CustomSchemaIds(type => type.ToString()); //Resolve erro de Enum no Swagger
            options.SwaggerDoc("v1", CreateInfoForApiVersion());

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"<b>JWT Autorização</b> <br/> 
                      Digite 'Bearer' [espaço] e em seguida informe o seu token na caixa de texto abaixo.
                      <br/> <br/>
                      <b>Exemplo:</b> 'Bearer 123456abcdefg...'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        }

        private static OpenApiInfo CreateInfoForApiVersion()
        {
            var info = new OpenApiInfo()
            {
                Title = "AutoGlass",
                Version = "v1",
                Description = "Documentação e Versionamento da API AutoGlass",
                Contact = new OpenApiContact() { Name = "AutoGlass", Email = "paulo.analista@outlook.com" },
                License = new OpenApiLicense() { Name = "Software as a Service (Software como Serviço)", Url = new Uri("https://ilovecode.com.br") }
            };

            return info;
        }
    }
}