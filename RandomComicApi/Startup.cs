using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RandomComicApi.ComicServices;
using RandomComicApi.ComicServices.ComicSources.DilbertComics;
using RandomComicApi.ComicServices.ComicSources.GarfieldComics;
using RandomComicApi.ComicServices.ComicSources.XKCD;

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
            services.AddSingleton<IComicService, ComicService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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