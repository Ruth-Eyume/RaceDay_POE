using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using RaceDayAPI.Data;
using RaceDay.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Register controllers
builder.Services.AddControllers();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RaceDay API",
        Version = "v1",
        Description = "API for managing RaceDay events, categories, routes, participants and results."
    });
});

// Configure the database connection
builder.Services.AddDbContext<RaceDayDBContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<EnrolmentService>();
builder.Services.AddScoped<EventCategoryService>();
builder.Services.AddScoped<OrganiserService>();
builder.Services.AddScoped<ParticipantService>();
builder.Services.AddScoped<ResultService>();
builder.Services.AddScoped<RouteService>();
builder.Services.AddScoped<UserAccountService>();

var app = builder.Build();

// Enable Swagger UI in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "RaceDay API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
