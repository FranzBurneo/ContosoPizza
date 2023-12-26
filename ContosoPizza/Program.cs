using ContosoPizza.Data;
using ContosoPizza.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ContosoPizzaContextConnection") ?? throw new InvalidOperationException("Connection string 'ContosoPizzaContextConnection' not found.");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ContosoPizzaContext>(options =>
                options.UseSqlServer(connectionString));
builder.Services.AddDbContext<PizzaContext>(options =>
    options.UseSqlite("Data Source=ContosoPizza.db"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultUI()
                .AddEntityFrameworkStores<ContosoPizzaContext>()
                .AddDefaultTokenProviders();
builder.Services.AddScoped<PizzaService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
