using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using RandomComicApi.ComicsService;
using RandomComicApi.ComicsService.ComicSources.CalvinAndHobbes;
using RandomComicApi.ComicsService.ComicSources.Dilbert;
using RandomComicApi.ComicsService.ComicSources.Garfield;
using RandomComicApi.ComicsService.ComicSources.Xkcd;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
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

builder.Services.AddHttpClient<IXKCD, XKCD>();
builder.Services.AddSingleton<IXKCD, XKCD>(p =>
{
    HttpClient httpClient = p.GetRequiredService<IHttpClientFactory>()
        .CreateClient(nameof(IXKCD));

    return new XKCD(httpClient, true);
});            

builder.Services.AddSingleton<IXkcdComic, XkcdComic>();
builder.Services.AddSingleton<IGarfield, Garfield>();
builder.Services.AddSingleton<IDilbert, Dilbert>();
builder.Services.AddSingleton<ICalvinAndHobbes, CalvinAndHobbes>();
builder.Services.AddSingleton<IComicUrlService, ComicUrlService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Random Comic API");

                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                c.RoutePrefix = string.Empty;
            });
}

app.UseHttpsRedirection();


app.MapGet("/random", async (IComicUrlService service, ILogger<ComicUrlService> _logger) =>
{
    try
    {
        _logger.LogInformation("Fetching random comic uri...");
        
        Results.Ok(new ComicModel {  ComicUrl = await service.GetRandomComic() });
    }
    catch (Exception exception)
    {
        _logger.LogError("Error while processing request.", exception);

        Results.Json(new ErrorModel { ErrorMessage = "Something went wrong" });
    }
});

app.MapGet("/dilbert", async (IComicUrlService service, ILogger<ComicUrlService> _logger) =>
{
    try
    {
        _logger.LogInformation("Fetching Dilbert comic uri...");

        Results.Ok(new ComicModel { ComicUrl = await service.GetDilbertComic() });
    }
    catch (Exception exception)
    {
        _logger.LogError("Error while processing request.", exception);

        Results.Json(new ErrorModel { ErrorMessage = "Something went wrong" });
    }
});


app.MapGet("/garfield", async (IComicUrlService service, ILogger<ComicUrlService> _logger) =>
{
    try
    {
        _logger.LogInformation("Fetching Garfield comic uri...");

        Results.Ok(new ComicModel { ComicUrl = await service.GetGarfieldComic() });
    }
    catch (Exception exception)
    {
        _logger.LogError("Error while processing request.", exception);

        Results.Json(new ErrorModel { ErrorMessage = "Something went wrong" });
    }
});


app.MapGet("/xkcd", async (IComicUrlService service, ILogger<ComicUrlService> _logger) =>
{
    try
    {
        _logger.LogInformation("Fetching XKCD comic uri...");

        Results.Ok(new ComicModel { ComicUrl = await service.GetXkcdComic() });
    }
    catch (Exception exception)
    {
        _logger.LogError("Error while processing request.", exception);

        Results.Json(new ErrorModel { ErrorMessage = "Something went wrong" });
    }
});


app.MapGet("/calvinandhobbes", async (IComicUrlService service, ILogger<ComicUrlService> _logger) =>
{
    try
    {
        _logger.LogInformation("Fetching Calvin and Hobbes comic uri...");

        Results.Ok(new ComicModel { ComicUrl = await service.GetCalvinAndHobbesComic() });
    }
    catch (Exception exception)
    {
        _logger.LogError("Error while processing request.", exception);

        Results.Json(new ErrorModel { ErrorMessage = "Something went wrong" });
    }
});


app.Run();
