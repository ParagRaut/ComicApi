using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RandomComicApi.ComicsService;
using RandomComicApi.ComicsService.ComicSources.CalvinAndHobbes;
using RandomComicApi.ComicsService.ComicSources.DilbertComics;
using RandomComicApi.ComicsService.ComicSources.GarfieldComics;
using RandomComicApi.ComicsService.ComicSources.XKCD;

namespace RandomComicApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Random Comic API",
                    Version = "v1",
                    Description = "This api will fetch random comic strips from XKCD and Garfield and Dilbert comic sources.",
                    Contact = new OpenApiContact
                    {
                        Name = "Parag Raut",
                        Email = "parag.raut@outlook.com",
                        Url = new Uri("https://paragraut.me/"),
                    },
                });
            });

            services.AddHttpClient<IXKCD, XKCD>();
            services.AddSingleton<IXKCD, XKCD>(p =>
            {
                HttpClient httpClient = p.GetRequiredService<IHttpClientFactory>()
                    .CreateClient(nameof(IXKCD));

                return new XKCD(httpClient, true);
            });
            services.AddSingleton<IXkcdComic, XkcdComic>();
            services.AddSingleton<IGarfieldComics, GarfieldComics>();
            services.AddSingleton<IDilbertComics, DilbertComics>();
            services.AddSingleton<ICalvinAndHobbesComics, CalvinAndHobbesComics>();
            services.AddSingleton<IComicUrlService, ComicUrlService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/ComicsApiLog-{Date}.log");

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Random Comic API");

                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}