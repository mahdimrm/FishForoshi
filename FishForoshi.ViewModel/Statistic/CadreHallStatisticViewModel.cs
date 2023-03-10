using FishForoshi.Entities;

namespace FishForoshi.ViewModel.Statistic
{
    public class CadreHallStatisticViewModel
    {
        public string FoodName { get; set; }
        public double FoodCount { get; set; }
        public string MealType { get; set; }
        public List<Norm> CalculatedNorms { get; set; }
        public List<Norm> Norms { get; set; }
    }
}
