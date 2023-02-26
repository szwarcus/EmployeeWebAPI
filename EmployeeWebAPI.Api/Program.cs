using EmployeeWebApi.Infrastracture;
using EmployeeWebApi.Infrastracture.Repository;
using EmployeeWebAPI.Application.Contracts.Persistence;
using EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
    //                  Enter 'Bearer' [space] and then your token in the text input below.
    //                  \r\n\r\nExample: 'Bearer 12345abcdef'",
    //    Name = "Authorization",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.ApiKey,
    //    Scheme = "Bearer"
    //});

    //c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    //              {
    //                {
    //                  new OpenApiSecurityScheme
    //                  {
    //                    Reference = new OpenApiReference
    //                      {
    //                        Type = ReferenceType.SecurityScheme,
    //                        Id = "Bearer"
    //                      },
    //                      Scheme = "oauth2",
    //                      Name = "Bearer",
    //                      In = ParameterLocation.Header,

    //                    },
    //                    new List<string>()
    //                  }
    //                });
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "EmployeeWebAPI",
    });
});
var connString = builder.Configuration.GetConnectionString("EmployeesContext");

builder.Services.AddDbContext<EmployeesDbContext>(options =>
{
    options.UseSqlServer(connString, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Open",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});


builder.Services.AddMediatR(configuration =>
    {
        configuration.Lifetime = ServiceLifetime.Transient; configuration.RegisterServicesFromAssemblyContaining(typeof(CreateEmployeeCommandHandler));
    });
builder.Services.AddAutoMapper(typeof(CreateEmployeeCommandHandler).Assembly);
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<EmployeesDbContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeWebAPI");
});

app.UseCors("Open");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();