using BugTracker.Models.viewModels;

namespace BugTracker.Models
{
    public class DevSelectedIfReassignedChecked : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var activity = (ActivityFormViewModel)validationContext.ObjectInstance;
            if (activity.ReassignedIssue == true)
            {
                if (activity.ReassignToId == 0)
                {
                    return new ValidationResult("Please select a developer to reassign this");
                }
            }
            return ValidationResult.Success;
        }
    }
}
