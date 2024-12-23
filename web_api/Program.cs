using infrastructure.configurations;
using web_api.configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);

// Add CORS services
builder.Services.AddCorsConfigurations(builder.Configuration);

builder.Services.AddControllers()
    .ConfigureJsonOptions(); 

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

app.UseCors(CorsConstants.AllowLocalReactApp);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.AddNotificationHub();

app.Run();