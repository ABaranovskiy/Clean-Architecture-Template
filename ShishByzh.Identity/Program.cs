using ShishByzh.Application;
using ShishByzh.Identity;
using ShishByzh.Identity.Middlewares;
using ShishByzh.Persistence;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    //.AddJsonFile("appsettings.Identity.Development.json", optional: true)
    .AddJsonFile("appsettings.Identity.json", optional: true)
    .AddEnvironmentVariables()
    .AddSecrets("/run/secrets/Database", "Database")
    .AddSecrets("/run/secrets/Jwt", "Jwt")
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConfiguration>(configuration);

builder.WebHost.UseKestrel(options =>
{   
    // Удаляем настройки, которые могут прийти из конфигурации или переменных окружения
    options.ConfigureEndpointDefaults(o => { }); 
    
    // Настраиваем явный порт
    options.ListenAnyIP(8081);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddApplication();

var connectionString = configuration["DbConnection"] 
                       + $";User Id={configuration["Database:login"]};Password={configuration["Database:password"]}";
builder.Services.AddPersistence(connectionString);
builder.Services.AddIdentity(configuration);
builder.Services.AddPolicies();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    try
    {
        var context = serviceProvider.GetRequiredService<ShishByzhDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "An error occurred while identity app initialization");
    }
}

// Configure the HTTP request pipeline.
app.UseMiddleware<AuthExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();