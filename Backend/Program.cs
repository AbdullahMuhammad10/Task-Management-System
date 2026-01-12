namespace Backend;


// Changed Structure To Old Program Style From Old Project 😅.
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add Services To The Container.
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        // Register The Repository To Allow Di As A Singleton To Live Throughout The Application Lifetime.
        builder.Services.AddSingleton<ITaskRepository,InMemoryTaskRepository>();


        // Customizing The Validation Error Response.
        builder.Services.Configure<ApiBehaviorOptions>(cfg =>
        {
            cfg.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState.Where(P => P.Value!.Errors.Any())
                                               .SelectMany(P => P.Value!.Errors)
                                               .Select(E => E.ErrorMessage)
                                               .ToArray();
                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(errorResponse);
            };
        }
        );
        builder.Services.AddTransient<ExceptionMiddleware>();

        var app = builder.Build();

        // To Add Exception Our Custom Middleware To Handle Server Errors
        app.UseMiddleware<ExceptionMiddleware>();

        // Configure The HTTP Request Pipeline.
        if(app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(Options =>
            {
                Options.SwaggerEndpoint("/openapi/v1.json","Tasks Api");
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}