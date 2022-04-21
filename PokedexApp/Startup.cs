using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokedex.Libs.Services;
using Pokedex.Libs.Services.Interfaces;

namespace PokedexApp
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
            services.AddControllers();
            services.AddHttpClient();
            services.AddHttpClient("PokemonApi", c => c.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon-species/"));
            services.AddHttpClient("YodaApi", c => c.BaseAddress = new Uri("https://api.funtranslations.com/translate/yoda.json"));
            services.AddHttpClient("ShakespeareApi", c => c.BaseAddress = new Uri("https://api.funtranslations.com/translate/shakespeare.json"));
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
