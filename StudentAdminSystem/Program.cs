using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using StudentAdminSystem.Data;
using StudentAdminSystem.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// MySQL DbContext
builder.Services.AddDbContext<MySqlDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.Parse("8.0.0-mysql")
    )
);

// MongoDB Context
builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var mongoSettings = MongoClientSettings.FromConnectionString(
        builder.Configuration.GetConnectionString("MongoDbConnection"));
    var mongoClient = new MongoClient(mongoSettings);
    return new MongoDbContext(mongoClient, "StudentAdminSystem");
});

// Register Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
// FIX: Ensure you are registering the correct concrete class (PdfService) for the correct interface (IPdfService).
builder.Services.AddScoped<IPdfService, PdfService>();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapRazorPages();

// Initialize MongoDB indexes
using (var scope = app.Services.CreateScope())
{
    var mongoContext = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
    await mongoContext.InitializeIndexesAsync();
}

app.Run();