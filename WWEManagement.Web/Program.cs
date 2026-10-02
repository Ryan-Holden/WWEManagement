using WWEManagement.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using WWEManagement.Data.Data;
using WWEManagement.Web.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString =
    builder.Configuration.GetConnectionString("WWEManagement")
    ?? throw new InvalidOperationException(
        "Connection string 'WWEManagement' was not found.");

builder.Services.AddDbContext<WWEManagementDbContext>(
    options =>
        options.UseSqlServer(connectionString));

builder.Services.AddScoped<
    IEventRepository,
    EfEventRepository>();

builder.Services.AddScoped<
    IEmployeeRepository,
    EfEmployeeRepository>();

builder.Services.AddScoped<
    IWrestlerRepository,
    EfWrestlerRepository>();

builder.Services.AddScoped<
    IWrestlerService,
    WrestlerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
