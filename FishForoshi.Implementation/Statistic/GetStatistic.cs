using FishForoshi.Abstraction;
using FishForoshi.Abstraction.Statistic;
using FishForoshi.Entities;
using FishForoshi.ViewModel.Statistic;
using System.Diagnostics;
using System.Security.Principal;

namespace FishForoshi.Implementation.Statistic
{
    public class GetStatistic : IGetStatistic
    {
        private readonly IGetFood _foodQuery;
        private readonly IGetNorm _normQuery;

        public GetStatistic(IGetFood foodQuery, IGetNorm normQuery)
        {
            _foodQuery = foodQuery;
            _normQuery = normQuery;
        }

        public async Task<IEnumerable<CadreHallStatisticViewModel>> GenerateCadreHallStatistics(List<Guid> foodIds, List<int> counts)
        {
            var result = new List<CadreHallStatisticViewModel>();
            List<Food> foods = new();
            foreach (var foodId in foodIds)
            {
                foods.Add(await _foodQuery.GetByIdAsync(foodId));
            }
            int index = 0;
            foreach (var item in foods)
            {
                int count = counts[index];
                index++;
                var norms = await _normQuery.Norms(item.Id);
                var foodNorms = new List<Norm>();
                foreach (var norm in norms)
                {
                    foodNorms.Add(new Norm { Id = norm.Id, Name = norm.Name, Value = norm.Value * count });
                }
                result.Add(new CadreHallStatisticViewModel
                {
                    FoodName = item.Name,
                    MealType = GenerateTypes((MealType)item.MealType, (FoodType)item.FoodType),
                    Norms = foodNorms
                });

            }

            return result;
        }

        public string GenerateTypes(MealType meal, FoodType foodType)
        {
            string mealName = "";
            switch (meal)
            {
                case MealType.BreakFast:
                    mealName = "صبحانه";
                    break;
                case MealType.Launch:
                    if (foodType == FoodType.Soldier)
                    {
                        mealName = "ناهار سازمانی";
                    }
                    if (foodType == FoodType.Employee)
                    {
                        mealName = "ناهار کادر";
                    }
                    break;
                case MealType.Snack:
                    mealName = "میان وعده";
                    break;
                case MealType.Dinnder:
                    mealName = "شام";
                    break;
                default:
                    break;
            }
            return mealName;
        }
    }
}

