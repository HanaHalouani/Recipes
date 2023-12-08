using System.ComponentModel.DataAnnotations;

namespace Recipes.Models.ViewModels
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredForCreateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (RecipeViewModel)validationContext.ObjectInstance;

            // Check if it's a create operation (Id is 0 or null)
            if (model.Id == 0 || model.Id == null)
            {
                // If it's a create operation, enforce the required validation
                if (value == null)
                {
                    return new ValidationResult(ErrorMessage ?? "The field is required.", new[] { validationContext.MemberName });
                }
            }

            return ValidationResult.Success;
        }
    }
}
