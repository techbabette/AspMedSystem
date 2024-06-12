using AspMedSystem.API;
using AspMedSystem.API.Core;
using AspMedSystem.API.Extensions;
using AspMedSystem.Application;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

var settings = new AppSettings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings.JWTSettings);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(x => new MedSystemContext(settings.ConnectionString));
builder.Services.AddScoped<IDbConnection>(x => new SqlConnection(settings.ConnectionString));

builder.Services.AddUseCases();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IExceptionLogger, DbExceptionLogger>();


builder.Services.AddTransient<IApplicationActorProvider>(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();

    var request = accessor.HttpContext.Request;

    var authHeader = request.Headers.Authorization.ToString();

    var context = x.GetService<MedSystemContext>();

    return new JwtApplicationActorProvider(authHeader);
});
builder.Services.AddTransient(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();
    if (accessor.HttpContext == null)
    {
        return new UnauthorizedActor();
    }

    return x.GetService<IApplicationActorProvider>().GetActor();
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
