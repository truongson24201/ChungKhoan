using BangGiaTrucTuyen.Hubs;
using BangGiaTrucTuyen.MiddlewareExtentions;
using BangGiaTrucTuyen.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

// add class through DI
builder.Services.AddSingleton<DashboardHub>();
builder.Services.AddSingleton<SubscribeBGTTtableDependency>();
builder.Services.AddRazorPages();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://example.com")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials();
        });
});

var app = builder.Build();
var connectionString = app.Configuration.GetConnectionString("DefaultConnection");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapHub<DashboardHub>("/dashboardHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

/*
 * we must call SubscribeTableDependency() here
 * We create on middleware and call SubscribeTableDependency() method in the middleware
 */

app.UseSqlTableDependency<SubscribeBGTTtableDependency>(connectionString);
app.Run();
