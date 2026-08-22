var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
/*
app.Use(async (context, next) =>
{
    Console.WriteLine($"Method: {context.Request.Method}");
    Console.WriteLine($"Path: {context.Request.Path}");
    Console.WriteLine($"Request: {context.Request.Headers}");
    Console.WriteLine($"Response: {context.Response.StatusCode}");
    context.Response.Headers["X-App-Version"] = "2.0";

    await next();
    Console.WriteLine("Request ended");
});

app.Use(async (context, next) =>
{
    Console.WriteLine("Middleware A");

    await next();

    Console.WriteLine("Middleware A End");
});

app.Use(async (context, next) =>
{
    Console.WriteLine("Middleware B");

    await next();

    Console.WriteLine("Middleware B End");
});

app.Map("/hello", helloApp => // curl http://localhost:5172/hello
{
    helloApp.Run(async context =>
    {
        Console.WriteLine("Hello App");
        await context.Response.WriteAsync(
            "Hello!");
    });
});
*/

app.Use(async (context, next) =>
{
    var requestId = Guid.NewGuid();
    context.Response.Headers["X-Request-Id"] = requestId.ToString();
    Console.WriteLine($"Request ID: {context.Response.Headers["X-Request-Id"]}");
    var startTime = DateTime.UtcNow;
    Console.WriteLine(
        $"Started: {context.Request.Method} " +
        $"{context.Request.Path}");
    await next();
    var duration = DateTime.UtcNow - startTime;
    Console.WriteLine(
        $"Finised: " +
        $"{context.Response.StatusCode} " +
        $"in {duration.TotalMilliseconds} ms");
});

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        context.Response.StatusCode = 500;

        await context.Response.WriteAsJsonAsync(new
        {
            message = "Internal Server Error"
        });
    }
});

app.MapControllers();
app.MapControllers();
app.Run();