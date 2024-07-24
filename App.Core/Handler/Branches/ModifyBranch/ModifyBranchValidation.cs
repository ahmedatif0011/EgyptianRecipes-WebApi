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
            RuleFor(c => c.Title).MaximumLength(200).WithMessage("{PropertyName} : can not be more than 200 chars");
            RuleFor(c => c.ManagerName).MaximumLength(250).WithMessage("{PropertyName} : can not be more than 250 chars");

            RuleFor(c => c)
                .Must(c => c.ClosingHour > c.OpenningHour)
                .WithMessage("Closing Hour must be later than the Opening Hour.");

            RuleFor(c => c)
                .Must(CheckWorkingTime)
                .WithMessage("Working Hours must be a valid time span more than 30 minutes.");

            RuleFor(c => c.OpenningHour)
            .Must(BeValidTimeSpan)
            .WithMessage("Opening Hour must be a valid time span in 30 minutes interval between 00:00 and 23:30.");

            RuleFor(c => c.ClosingHour)
                .Must(BeValidTimeSpan)
                .WithMessage("Closing Hour must be a valid time span in 30 minutes interval between 00:00 and 23:30.");

            
        }
        private bool CheckWorkingTime(ModifyBrancheRequest request)
        {
            var workingTime = request.ClosingHour - request.OpenningHour;
            if (workingTime.TotalMinutes < 30)
                return false;

            return true;
        }
        private bool BeValidTimeSpan(TimeSpan time)
        {
            // Check if the time is between 00:00 and 23:30
            if (time < new TimeSpan(0, 0, 0) || time > new TimeSpan(23, 30, 0))
                return false;

            return true;
        }
    }
}
