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
        builder.Services.AddSingleton<ITaskRepository,SqliteTaskRepository>();

        // Registering The Database Initializer As A SingletonTo Live Throughout The Application Lifetime..
        builder.Services.AddSingleton<IDatabaseInitializer,DatabaseInitializer>();

        // Registering CORS To Allow Requests From Angular Application.
        builder.Services.AddCors(Options =>
        {
            Options.AddPolicy("AngularPolicy",Policy =>
            {
                Policy.WithOrigins("http://localhost:4200") // Angular URL
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

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

        // To Register Our Custom Middleware For Handling Exceptions Globally.
        builder.Services.AddTransient<ExceptionMiddleware>();

        var app = builder.Build();

        // Initialize The Database On Application Startup.
        // Create A Scope To Get The Scoped Services.
        using(var scope = app.Services.CreateScope())
        {
            // Get The Database Initializer Service From Scope.
            var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
            // Call The InitializeDatabase Method To Seed The Database.
            databaseInitializer.InitializeDatabase();
        }

        // To Add Our Custom Middleware To Handle Server Errors
        app.UseMiddleware<ExceptionMiddleware>();

        // Middleware To Handle Missing Routes Using StatusCodePages
        app.UseStatusCodePagesWithReExecute("/error/{0}");

        // Configure The HTTP Request Pipeline.
        if(app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(Options =>
            {
                Options.SwaggerEndpoint("/openapi/v1.json","Tasks Api");
            });
        }

        // Enabling CORS Middleware
        app.UseCors("AngularPolicy");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}