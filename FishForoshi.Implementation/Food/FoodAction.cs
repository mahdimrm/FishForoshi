using FishForoshi.Abstraction;
using FishForoshi.Abstraction.Common;
using FishForoshi.Entities;
using FishForoshi.Implementation.Upload;
using FishForoshi.ViewModel.Common;
using Microsoft.AspNetCore.Http;

namespace FishForoshi.Implementation
{
    public class FoodAction : IFoodAction
    {
        private readonly IQueryRepository<Food> _query;
        private readonly ICudRepository<Food> _action;
        private readonly IUploadFileService _upload;

        public FoodAction(IQueryRepository<Food> query, ICudRepository<Food> action, IUploadFileService upload)
        {
            _query = query;
            _action = action;
            _upload = upload;
        }

        public async Task<FoodActionStatus> CreateAsync(Food food, IFormFile file)
        {
            if (file != null)
            {
                var uploadLogoResult = await _upload.Upload(file, @"Upload\Food");
                if (uploadLogoResult != null)
                {
                    food.ImageName = uploadLogoResult.FileName;
                }
            }

            return await _action.AddAsync(food) ?
             FoodActionStatus.Success : FoodActionStatus.Failed;
        }



        public async Task<FoodActionStatus> DeleteAsync(Guid id)
        {
            var food = await _query.GetAsync(id);
            if (food == null)
            {
                return FoodActionStatus.NotFound;
            }

            var uploadResult = await _upload.DeleteFile(food.ImageName, @"Upload\Food");
            if (uploadResult == DeleteFileResultStatus.Failed)
            {
                return FoodActionStatus.Failed;
            }

            return await _action.DeleteByIdAsync(id)
                                 ? FoodActionStatus.Success
                                 : FoodActionStatus.Failed;
        }

        public async Task<FoodActionStatus> UpdateAsync(Food food, IFormFile file)
        {
            var model = await _query.GetAsync(food.Id);
            if (model == null)
            {
                return FoodActionStatus.NotFound;
            }

            if (model.ImageName != null)
            {
                await _upload.DeleteFile(model.ImageName, @"Upload\Food");
            }

            if (file != null)
            {
                var uploadLogoResult = await _upload.Upload(file, @"Upload\Food");
                if (uploadLogoResult != null)
                {
                    model.ImageName = uploadLogoResult.FileName;
                }
            }

            model.Name = food.Name;
            model.Price = food.Price;
            model.FoodType = food.FoodType;
            model.MealType = food.MealType;
            model.Description = food.Description;

            return await _action.UpdateAsync(model)
                ? FoodActionStatus.Success : FoodActionStatus.Failed;
        }
    }
}
