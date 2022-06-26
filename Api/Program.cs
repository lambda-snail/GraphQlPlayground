using Api.Graph;
using Api.Models;
using Api.Options;
using Api.Repositories;
using HotChocolate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var config = BuildConfiguration(builder);
        AddServices(builder, config);

        var app = builder.Build();

        AddMiddleware(app);

        app.Run();
    }

    private static void AddMiddleware(WebApplication app)
    {
        app.UseCors();

        app.UseHttpsRedirection();

        app.UseWebSockets();
        app
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

        app.UseAuthorization();

        app.MapControllers();
    }

    private static void AddServices(WebApplicationBuilder builder, IConfiguration config)
    {
        builder.Services.AddControllers();
        var connectionString = config.GetSection("NorthWindOptions").GetValue<string>("ConnectionString");
        builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddScoped<IOrderRepository, OrderRepository>();

        builder.Services.AddInMemorySubscriptions();

        builder.Services
            .AddGraphQLServer()
            .AddQueryType<Query>();
            //.AddMutationType<Mutation>()
            //.AddSubscriptionType<Subscription>();
    }

    private static IConfiguration BuildConfiguration(WebApplicationBuilder builder)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true).Build();

        builder.Services.Configure<NorthWindOptions>(
            config.GetSection("NorthWindOptions"));

        return config;
    }
}
