using Microsoft.OpenApi.Models;
using RandomComicApi.ComicsService;
using RandomComicApi.ComicsService.CalvinAndHobbes;
using RandomComicApi.ComicsService.Dilbert;
using RandomComicApi.ComicsService.Garfield;
using RandomComicApi.ComicsService.XKCD;
using RandomComicApi.ComicsService.XKCD.Generated;

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

builder.Services.AddScoped<IXKCD, XKCD>(p => new XKCD(new HttpClient(), true));

builder.Services.AddScoped<XKCDService>();

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

Random random = new(6);

ComicEnum ChooseRandomComicSource() => (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);

app.MapGet("/dilbert", async () => new ComicModel(await DilbertService.GetComicUri())).Produces<ComicModel>(200);

app.MapGet("/garfield", async () => new ComicModel(await GarfieldService.GetComicUri())).Produces<string>(200);

app.MapGet("/xkcd", async (XKCDService service) => new ComicModel(await service.GetComicUri())).Produces<string>(200);

app.MapGet("/calvinandhobbes", async () => new ComicModel(await CalvinAndHobbesService.GetComicUri())).Produces<string>(200);

app.MapGet("/random", async (XKCDService service) => 
{ 
    var comicName = ChooseRandomComicSource(); 
    return Results.Ok(comicName switch 
    { 
        ComicEnum.XKCD => new ComicModel(await service.GetComicUri()),
        ComicEnum.Garfield => new ComicModel(await GarfieldService.GetComicUri()),
        ComicEnum.Dilbert => new ComicModel(await DilbertService.GetComicUri()),
        ComicEnum.CalvinAndHobbes => new ComicModel(await CalvinAndHobbesService.GetComicUri()),
        _ => throw new ArgumentOutOfRangeException() 
        }); 
})
.Produces<string>(200);

app.Run();

record ComicModel(string Url);
