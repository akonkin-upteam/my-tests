using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using CommonClasses;

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

builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite("Data Source=app.db"));

builder.Services.AddIdentityCore<PromoMashUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

var app = builder.Build();

//app.UseAuthentication();
//app.UseAuthorization();

app.UseCors(
    b => 
 b.AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader()
 );


app.UseSwagger();
app.UseSwaggerUI();

app.MapIdentityApi<PromoMashUser>();


app.MapGet("/", async (ClaimsPrincipal u, AppDbContext db) => 
    {
        var userName = u.Identity?.Name;
        return await db.Users.Where(usr => usr.UserName == userName).ToListAsync();        
    }
).RequireAuthorization().WithOpenApi();

app.Run();

public class PromoMashUser : IdentityUser
{}

public class AppDbContext : IdentityDbContext<PromoMashUser> 
{ 
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) 
    { }
}



