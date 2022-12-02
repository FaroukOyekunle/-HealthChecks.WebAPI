using HealthChecks.WebApi.Data;
using HealthChecks.WebApi.HealthChecks;
using HealthChecks.WebApi.Models;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDataContext>()
           .AddUrlGroup(new Uri("https://www.codewithmukesh.com"), name: "CodewithMukesh")
           .AddCheck<CustomHealthCheck>(name: "New Custom Check");

builder.Services.AddDbContext<ApplicationDataContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDataContext).Assembly.FullName)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Here we are adding an overload to the UseHealthChecks method, HealthCheckOptions.
// Line #42 – This accepts a HttpContext and the HealthReport.
// We use these variables to write to our HealthCheckReponse classes.
// Line #42 – Here we define that the type of response should be JSON
// Line #48 – Creating a new HealthCheckReponse object
// Line #52 - Filling out the Individual component class from the entire Health Check Report.
// Line #62 - Serializes the class to jSON and saves it to the output.
// PS: The Newtonsoft Package has already been installed.
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var response = new HealthCheckResponse
        {
            Status = report.Status.ToString(),
            HealthChecks = report.Entries.Select(x => new IndividualHealthCheckResponse
            {
                Components = x.Key,
                Status = x.Value.Status.ToString(),
                Description = x.Value.Description

            }),
            HealthCheckDuration = report.TotalDuration
        };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
});
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
