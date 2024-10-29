using ApiDevBP.Configurations;
using ApiDevBP.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar DatabaseOptions
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("DatabaseOptions"));

// Registrar DatabaseHandler y UserService
builder.Services.AddSingleton<DatabaseHandler>();
builder.Services.AddScoped<IUserService, UserService>();

// Resto de la configuración
builder.Services.AddControllers();
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
app.UseAuthorization();
app.MapControllers();
app.Run();