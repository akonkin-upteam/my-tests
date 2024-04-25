using CommonClasses;
using CountriesService.Db;
using CountriesService.DTO;
using CountriesService.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme, o =>
{
    o.BearerTokenProtector = new TicketDataFormat(
                                    new MyDataProtector()
                                        .CreateProtector(""));
    o.RefreshTokenProtector = new TicketDataFormat(
                                    new MyDataProtector()
                                        .CreateProtector(""));
});
builder.Services.AddAuthorizationBuilder();

builder.Services.AddDbContext<CountryDB>(o => o.UseSqlite("Data Source=Countries.db"));

builder.Services.AddScoped<CountryService>();

var app = builder.Build();

app.UseCors(
    b =>
 b.AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader()
 );

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/countries", async (CountryService service) =>
{
    return Results.Ok(await service.GetCuntryList());
})
.WithName("countries")
.RequireAuthorization()
.WithOpenApi()
.WithDescription("get list of countries")
.CacheOutput();

app.MapGet("/provinces", async (string countryId, CountryService service) =>
{
    return Results.Ok(await service.GetProvinceList(countryId));
})
.WithName("provinces")
.RequireAuthorization()
.WithOpenApi()
.WithDescription("get list of provinces")
.CacheOutput();

app.MapPost("/savelocation", async (UserData userdata, CountryService service) =>
{
    return Results.Ok(await service.SaveLocationOptions(userdata.UserId, userdata.ProvinceId));
}).WithName("savelocation")
.RequireAuthorization()
.WithOpenApi()
.WithDescription("get list of provinces")
.CacheOutput(); ;

app.Run();