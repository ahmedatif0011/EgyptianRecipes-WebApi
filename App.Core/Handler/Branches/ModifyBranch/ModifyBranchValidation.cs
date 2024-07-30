using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Handler.Branches.UpdateBranch
{
    public class UpdateBranchValidation : AbstractValidator<ModifyBrancheRequest>
    {
        public UpdateBranchValidation()
        {
            //Title
            RuleFor(c => c.Title).NotEmpty().WithMessage("The 'Title' field is required.");
            RuleFor(c => c.Title).MaximumLength(200).WithMessage("'Title' can not be more than 200 chars");

            //Manager Name
            RuleFor(c => c.ManagerName).NotEmpty().WithMessage("The 'ManagerName field' is required.");
            RuleFor(c => c.ManagerName).MaximumLength(250).WithMessage("'Manager Name' can not be more than 250 chars");

            //Opening Hour
            RuleFor(c => c.OpenningHour).LessThan(new TimeSpan(23, 30, 0)).WithMessage("The 'Opening Hour' field can not be more than '23:30'");

            //Closing Hour
            RuleFor(c => c.ClosingHour).LessThan(new TimeSpan(23, 30, 0)).WithMessage("The 'Closing Hour' field can not be more than '23:30'");
            RuleFor(c => c.ClosingHour).GreaterThan(c => c.OpenningHour).WithMessage("'Closing Hour' must be later than the Opening Hour.");
            RuleFor(c => c.ClosingHour).Must((c, closingHour) => (closingHour - c.OpenningHour).TotalMinutes >= 30).WithMessage("The 'Closing hour' must be at least 30 minutes after the opening hour.");

        }
    }
}
