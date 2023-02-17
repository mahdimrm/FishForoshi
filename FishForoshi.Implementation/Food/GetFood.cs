using FishForoshi.Abstraction;
using FishForoshi.Abstraction.Tools;
using FishForoshi.Entities;
using FishForoshi.Implementation.Tools;
using FishForoshi.ViewModel.Common;

namespace FishForoshi.Implementation
{
    public class GetFood : IGetFood
    {
        private readonly IQueryRepository<Food> _query;

        public GetFood(IQueryRepository<Food> query)
        {
            _query = query;
        }

        public async Task<Food> GetByIdAsync(Guid id)
            => await _query.GetAsync(id);

        public async Task<IPagedList<Food>> GetFoodsAsync(int pageNumber, string name)
        {
            var foods = await _query.GetAllAsync();

            if (name != null)
            {
                foods = foods.Where(x => x.Name.Contains(name));
            }
            return new PagedList<Food>(foods, pageNumber, 10);
        }

        public async Task<IEnumerable<SelectListDto>> GetBreakFastNames()
        {
            var foods = await _query.GetAllAsync();

            var result = foods
                .Where(x => (FoodType)x.FoodType == FoodType.Soldier 
                && 
                (MealType)x.MealType == MealType.BreakFast)
                .Select(x => new SelectListDto
            {
                Id = x.Id,
                Name = x.Name
            });

            return result;
        }
        public async Task<IEnumerable<SelectListDto>> GetEmployeeLaunchNames()
        {
            var foods = await _query.GetAllAsync();

            var result = foods
                .Where(x => (FoodType)x.FoodType == FoodType.Employee
                &&
                (MealType)x.MealType == MealType.Launch)
                .Select(x => new SelectListDto
                {
                    Id = x.Id,
                    Name = x.Name
                });

            return result;
        }
        public async Task<IEnumerable<SelectListDto>> GetSnackNames()
        {
            var foods = await _query.GetAllAsync();

            var result = foods
                .Where(x => (FoodType)x.FoodType == FoodType.Employee
                &&
                (MealType)x.MealType == MealType.Snack)
                .Select(x => new SelectListDto
                {
                    Id = x.Id,
                    Name = x.Name
                });

            return result;
        }

        public async Task<IEnumerable<SelectListDto>> GetSoldierLaunchNames()
        {
            var foods = await _query.GetAllAsync();

            var result = foods
                .Where(x => (FoodType)x.FoodType == FoodType.Soldier
                &&
                (MealType)x.MealType == MealType.Launch)
                .Select(x => new SelectListDto
                {
                    Id = x.Id,
                    Name = x.Name
                });

            return result;
        }

        public async Task<IEnumerable<SelectListDto>> GetSoldierDinndersNames()
        {
            var foods = await _query.GetAllAsync();

            var result = foods
                .Where(x => (FoodType)x.FoodType == FoodType.Soldier
                &&
                (MealType)x.MealType == MealType.Dinnder)
                .Select(x => new SelectListDto
                {
                    Id = x.Id,
                    Name = x.Name
                });

            return result;
        }
    }
}
