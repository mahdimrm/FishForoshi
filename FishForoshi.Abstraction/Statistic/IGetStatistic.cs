using FishForoshi.ViewModel.Statistic;

namespace FishForoshi.Abstraction.Statistic
{
    public interface IGetStatistic
    {
        Task<IEnumerable<CadreHallStatisticViewModel>> GenerateCadreHallStatistics(List<Guid> foodIds,List<int> counts);
    }
}
