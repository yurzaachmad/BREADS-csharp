using HealthChecks.UI.Client;
using IEM.Core;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using myFirstWeb;
using myFirstWeb.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseLoggingService("Serilog");

// Its use to connect our db context
builder.Services.AddDbContext<EmployeeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeContext") ?? throw new InvalidOperationException("Connection string 'EmployeeContext' not found.")));

builder.Services.AddSession();
// Add services to the container.
builder.Services.AddRazorPages();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddHealthChecks()
     .AddCheck(
        "OrderingDB-check",
        new SqlConnectionHealthCheck(builder.Configuration["ConnectionString"]),
        HealthStatus.Unhealthy,
        new string[] { "DBemployee" }).AddCheck(
        "Test-MasterDB",
        new SqlConnectionHealthCheck(builder.Configuration["TestUnhealthyDatabase"]),
        HealthStatus.Unhealthy,
        new string[] { "DB" });

builder.Services.AddHealthChecksUI(setupSettings: setup =>
{
    setup.SetEvaluationTimeInSeconds(60);
    setup.MaximumHistoryEntriesPerEndpoint(50);
}).AddSqlServerStorage("Server=localhost\\sqlexpress;Database=Employee;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false");

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

app.UseHealthChecksUI(config => config.UIPath = "/hc-ui");

app.UseRouting()
     .UseEndpoints(config =>
     {
       config.MapHealthChecks("/healthz", new HealthCheckOptions
        {
          Predicate = _ => true,
          ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
     });

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");

app.Run();
