using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TwitterAPIClone.Data;
using TwitterAPIClone.Models;
using TwitterAPIClone.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("localDb"), ServiceLifetime.Singleton);
builder.Services.AddScoped<IBaseService<User>, BaseService<User>>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();
var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

// adding admin
var user = new User()
{
    Username = "Miras",
    Role = Constants.ADMIN_ROLE,
    Tweets = new List<Tweet>()
    {
        new Tweet() {Text = "First Tweet"},
        new Tweet() {Text = "2 Tweet"},
        new Tweet() {Text = "lol Tweet"},
    }
};
var userSecret = new UserSecrets()
{
    Username = user.Username,
    Password = "miras123",
    User = user
};
dbContext.Users.Add(user);
dbContext.UserSecrets.Add(userSecret);
dbContext.SaveChanges();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=WeatherForecast}/{action=Get}/{id?}");

app.Run();
