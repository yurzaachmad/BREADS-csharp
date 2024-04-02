using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using myFirstWeb.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Its use to connect our db context
builder.Services.AddDbContext<EmployeeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeContext") ?? throw new InvalidOperationException("Connection string 'EmployeeContext' not found.")));

builder.Services.AddSession();
// Add services to the container.
builder.Services.AddRazorPages();

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
  //  app.UseMigrationsEndPoint();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");

app.Run();
