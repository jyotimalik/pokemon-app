using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokedex.Libs.Services;
using Pokedex.Libs.Services.Interfaces;
using PokedexApi.Middleware;

namespace PokedexApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string applicationEnv = Configuration["ApplicationEnvironment"];
            services.AddControllers();
            services.AddHttpClient();
            services.AddHttpClient("PokemonApi", c => c.BaseAddress = new Uri(Configuration[applicationEnv + ":pokemonApiUrl"])).ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            }); ;
            services.AddHttpClient("YodaApi", c => c.BaseAddress = new Uri(Configuration[applicationEnv + ":yodaApiUrl"])).ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            }); ;
            services.AddHttpClient("ShakespeareApi", c => c.BaseAddress = new Uri(Configuration[applicationEnv + ":shakespeareApiUrl"])).ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            }); ;
            services.AddSingleton<IPokemonService, PokemonService>();
            services.AddSingleton<ShakespeareTranslationService>();
            services.AddSingleton<YodaTranslationService>();
            services.AddSingleton<ITranslationService, ShakespeareTranslationService>();
            services.AddSingleton<ITranslationService, YodaTranslationService>();
            services.AddSingleton<ITranslationFactory, TranslationFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
           //custom exception middleware added
            app.UseCustomExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
