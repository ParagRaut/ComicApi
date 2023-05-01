using ComicsProvider;
using Microsoft.OpenApi.Models;

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
                    Description = "This api will fetch random comic strips from XKCD and Garfield and CalvinAndHobbes comic sources.",
                    Contact = new OpenApiContact
                    {
                        Name = "Parag Raut",
                        Email = "parag.raut@outlook.com",
                        Url = new Uri("https://paragraut.me/"),
                    },
                });
            });

builder.Services.AddComicsService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

Random random = new(6);

ComicEnum ChooseRandomComicSource() => (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);

app.MapGet("/garfield", async (IComicsService service) => new ComicModel(await service.GetGarfieldComics())).Produces<ComicModel>(200);

app.MapGet("/xkcd", async (IComicsService service) => new ComicModel(await service.GetXkcdComics())).Produces<ComicModel>(200);

app.MapGet("/calvinandhobbes", async (IComicsService service) => new ComicModel(await service.GetCalvinAndHobbesComics())).Produces<ComicModel>(200);

app.MapGet("/random", async (IComicsService service) => 
{ 
    var comicName = ChooseRandomComicSource(); 
    return Results.Ok(comicName switch 
    { 
        ComicEnum.Xkcd => new ComicModel(await service.GetXkcdComics()),
        ComicEnum.Garfield => new ComicModel(await service.GetGarfieldComics()),
        ComicEnum.CalvinAndHobbes => new ComicModel(await service.GetCalvinAndHobbesComics()),
        _ => throw new ArgumentOutOfRangeException() 
        }); 
})
.Produces<ComicModel>();

app.Run();

internal record ComicModel(string Url);
