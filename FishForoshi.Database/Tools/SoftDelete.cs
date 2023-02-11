using FishForoshi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace FishForoshi.Database
{
    public static class SoftDelete
    {
        private const string _isDeletedProperty = "IsDeleted";
        private static readonly MethodInfo _propertyMethod = typeof(EF)
            .GetMethod(nameof(EF.Property), BindingFlags.Static | BindingFlags.Public)
            .MakeGenericMethod(typeof(bool));

        public static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            var parm = Expression.Parameter(type, "it");
            var prop = Expression.Call(_propertyMethod, parm, Expression.Constant(_isDeletedProperty));
            var condition = Expression.MakeBinary(ExpressionType.Equal, prop, Expression.Constant(false));
            var lambda = Expression.Lambda(condition, parm);
            return lambda;
        }

        public static ModelBuilder ApplySoftDelete(this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entity.ClrType) == true)
                {
                    builder
                        .Entity(entity.ClrType)
                        .HasQueryFilter(SoftDelete.GetIsDeletedRestriction(entity.ClrType));
                }
            }

            return builder;
        }
    }

}
