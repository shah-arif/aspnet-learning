var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("Application"));
builder.Services.Configure<RestaurantSettings>(builder.Configuration.GetSection("Restaurant"));

builder.Configuration.GetConnectionString("DefaultConnection");

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
