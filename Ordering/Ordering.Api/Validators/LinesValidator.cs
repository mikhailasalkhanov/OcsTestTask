using System.ComponentModel.DataAnnotations;
using Ordering.Api.Dto;

namespace Ordering.Api.Validators
{
    public class UniqueProductIdsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IEnumerable<OrderLineDto> lines)
            {
                var productIds = new HashSet<Guid?>();
                if (lines.All(l => productIds.Add(l.ProductId)))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Two or more lines contain the same ProductIds");
                }
            }

            return new ValidationResult("Value is not assignable");
        }
    }
}