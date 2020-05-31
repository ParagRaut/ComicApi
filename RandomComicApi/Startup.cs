using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RandomComicApi.ComicsService;
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
            services.AddSingleton<IComicUrlService, ComicUrlService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/ComicsApiLog-{Date}.log");

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