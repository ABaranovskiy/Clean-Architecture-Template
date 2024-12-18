using ShishByzh.Application;
using ShishByzh.Persistence;
using ShishByzh.Server;
using ShishByzh.Server.Middlewares;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    //.AddJsonFile("appsettings.Server.Development.json", optional: true)
    .AddJsonFile("appsettings.Server.json", optional: true)
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
    options.ListenAnyIP(8080);
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
builder.Services.AddServer(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<AuthExceptionHandlingMiddleware>();

/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();