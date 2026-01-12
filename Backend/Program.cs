using Backend.Repositories;
using Backend.Repositories.Interfaces;

namespace Backend;


// Changed Structure To Old Program Style From Old Project 😅.
public class Program
{
    public static void Main(string[] args)
    {
        var Builder = WebApplication.CreateBuilder(args);

        // Add Services To The Container.
        Builder.Services.AddControllers();
        Builder.Services.AddOpenApi();

        // Register The Repository To Allow Di As A Singleton To Live Throughout The Application Lifetime.
        Builder.Services.AddSingleton<ITaskRepository,InMemoryTaskRepository>();

        var App = Builder.Build();

        // Configure The HTTP Request Pipeline.
        if(App.Environment.IsDevelopment())
        {
            App.MapOpenApi();
        }

        App.UseHttpsRedirection();
        App.UseAuthorization();

        App.MapControllers();

        App.Run();
    }
}