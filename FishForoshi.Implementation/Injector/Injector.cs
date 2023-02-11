using FishForoshi.Abstraction;
using FishForoshi.Database;
using FishForoshi.Implementation;
using Infrastructure.Implementation.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenStorage.Services.Implementation.Injection;

public static class Injector
{
    public static void AddKitchenServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FishForoshiDb") ?? "";
        FishForoshiContext.ConnectionString = connectionString;

        services.AddDbContext<FishForoshiContext>(options => options.UseSqlServer(connectionString));

        services.AddBaseServices();
        services.AddActionServices();
    }

    static void AddBaseServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
        services.AddScoped(typeof(ICudRepository<>), typeof(CudRepository<>));
    }

    static void AddActionServices(this IServiceCollection services)
    {
        #region Food
        services.AddTransient<IGetFood, GetFood>();
        services.AddTransient<IFoodAction, FoodAction>();
        #endregion

        #region Norm
        services.AddTransient<IGetNorm, GetNorm>();
        services.AddTransient<INormAction, NormAction>();
        #endregion
    }
}
