using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FavoritesProductsAPI.Data;
using System;
using Microsoft.AspNetCore.ResponseCompression;
using System.Linq;
using Microsoft.Extensions.Logging;
using FavoritesProductsAPI.Services;
using FavoritesProductsAPI.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using IdentityServer4.AccessTokenValidation;
using IdentityModel;

namespace FavoritesProductsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IFavoriteProductService, FavoriteProductService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddDbContext<FavoritesProductsContext>(opt => opt.UseMySQL(Configuration.GetConnectionString("FavoritesProductsConnection")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FavoritesProductsAPI", Version = "v1" });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddResponseCompression(opt =>
            {
                opt.EnableForHttps = true;
                opt.Providers.Add<GzipCompressionProvider>();
                opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://jpproject-sso:5000";
                    options.RequireHttpsMetadata = false;
                    options.ApiSecret = "api-secret";
                    options.ApiName = "report-api";
                    options.RoleClaimType = JwtClaimTypes.Role;
                    options.NameClaimType = JwtClaimTypes.Name;
                });
        }

        public void Configure(IApplicationBuilder app, 
            ILoggerFactory loggerFactory, 
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FavoritesProductsAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseResponseCompression();
            loggerFactory.AddFile($"Logs/Api.Log.txt");
        }
    }
}
