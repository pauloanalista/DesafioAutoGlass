using AutoGlass.Api.Swagger;
using AutoGlass.Domain.Commands.Produto.SalvarProduto;
using AutoGlass.Domain.Interfaces.Repositories;
using AutoGlass.Domain.Interfaces.Services;
using AutoGlass.Infra.Repositories;
using AutoGlass.Infra.Repositories.Base;
using AutoGlass.Infra.Repositories.Transactions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Qsti.ManutencaoGps.Infra.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutoGlass.Api
{
    public static class Setup
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddTransient<IConfigureOptions<SwaggerUIOptions>, ConfigureSwaggerUIOptions>();
            services.AddSwaggerGen();
        }
        public static void ConfigureUploadLimitToMaximum(this IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue; //not recommended value
                options.MultipartBodyLengthLimit = long.MaxValue; //not recommended value
            });
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IServiceEmail, ServiceEmail>();
            //services.AddTransient<IServicePushNotification, ServicePushNotification>();
            //services.AddTransient<IServiceGlobalBus, ServiceGlobalBus>();
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            //services.AddDbContext<Context>();
            services.AddScoped<Context, Context>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IRepositoryProduto, RepositoryProduto>();
            services.AddTransient<IRepositoryFornecedor, RepositoryFornecedor>();
            

        }
        public static void ConfigureCompression(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });
        }
        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
        public static void ConfigureContextEF(this IServiceCollection services, string connection)
        {
            services.AddDbContext<Context>(options =>
            {
                connection = "Server=127.0.0.1;Database=autoglass;Uid=root;Pwd=root; Connection Timeout=120";
                options.UseMySql(connection, ServerVersion.AutoDetect(connection));
            });

        }

        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly, typeof(SalvarProdutoRequest).GetTypeInfo().Assembly);


        }
    }
}
