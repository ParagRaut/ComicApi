using ComicsProvider;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

builder.Services.AddComicsService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

Random random = new(6);

app.MapGet("/garfield", async (IComicsService service) => new ComicModel(await service.GetGarfieldComics()));

app.MapGet("/xkcd", async (IComicsService service) => new ComicModel(await service.GetXkcdComics()));

app.MapGet("/calvinandhobbes", async (IComicsService service) => new ComicModel(await service.GetCalvinAndHobbesComics()));

app.MapGet("/random", async (IComicsService service) =>
    {
        var comicName = (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
        
        return Results.Ok(comicName switch 
        { 
            ComicEnum.Xkcd => new ComicModel(await service.GetXkcdComics()),
            ComicEnum.Garfield => new ComicModel(await service.GetGarfieldComics()),
            ComicEnum.CalvinAndHobbes => new ComicModel(await service.GetCalvinAndHobbesComics()),
            _ => throw new ArgumentOutOfRangeException() 
        }); 
    });

app.Run();

internal record ComicModel(string Url);