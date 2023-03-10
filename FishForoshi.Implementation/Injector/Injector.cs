using FishForoshi.Abstraction;
using FishForoshi.Abstraction.Common;
using FishForoshi.Abstraction.Statistic;
using FishForoshi.Database;
using FishForoshi.Implementation;
using FishForoshi.Implementation.Statistic;
using FishForoshi.Implementation.Upload;
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
        #region Upload
        services.AddTransient<IUploadFileService, UploadFileService>();

        #endregion

        #region Food
        services.AddTransient<IGetFood, GetFood>();
        services.AddTransient<IFoodAction, FoodAction>();
        #endregion

        #region Factor

        services.AddTransient<IGetFactor, GetFactor>();
        services.AddTransient<IFactorAction, FactorAction>();

        #endregion

        #region Norm
        services.AddTransient<IGetNorm, GetNorm>();
        services.AddTransient<INormAction, NormAction>();
        #endregion

        #region Barber

        services.AddTransient<IGetBarber, GetBarber>();
        services.AddTransient<IBarberAction, BarberAction>();
        #endregion

        #region CustomerTurn

        services.AddTransient<IGetCustomerTurn, GetCustomerTurn>();
        services.AddTransient<ICustomerTurnAction, CustomerTurnAction>();

        #endregion

        #region Statistic

        services.AddTransient<IGetStatistic, GetStatistic>();

        #endregion

        #region Day
        services.AddTransient<IGetDay, GetDay>();
        services.AddTransient<IDayAction, DayAction>();
        #endregion

        #region DailyFood

        services.AddTransient<IGetDailyFood, GetDailyFood>();
        services.AddTransient<IDailyFoodAction, DailyFoodAction>();

        #endregion
    }
}
