using MediatR;
using MediatR.Pipeline;
using RouteAPI.Interfaces;
using RouteAPI.Mapping;
using RouteAPI.Options;
using RouteAPI.Pipelines;
using RouteAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure
builder.Services.AddAutoMapper(typeof(IMapperAssemblyMarker));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped(typeof(IRequestPreProcessor<>), typeof(ValidationBehavior<>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CacheBehavior<,>));

// Services
builder.Services.AddTransient<IDateTimeProvider, DatetimeProvider>();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddTransient<ISearchService, SearchOneService>();
builder.Services.AddTransient<ISearchService, SearchTwoService>();

// Options
var myOptions = builder.Configuration.GetSection("Providers").Get<Providers>();
builder.Services.AddSingleton(myOptions);

var app = builder.Build();

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
