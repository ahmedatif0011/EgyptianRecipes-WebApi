using App.Domain.Models.shared;
using App.Infrastructure.Interfaces.Repository;
using MediatR;

namespace App.Core.Handler.Branches.UpdateBranch
{
    public class UpdateBranchHandler : IRequestHandler<ModifyBrancheRequest, ResponseResult>
    {
        private readonly IRepositoryQuery<App.Domain.Entities.Branches> _BranchesQuery;
        private readonly IRepositoryCommand<App.Domain.Entities.Branches> _BranchesCommand;
        public UpdateBranchHandler(IRepositoryQuery<Domain.Entities.Branches> branchesQuery, IRepositoryCommand<Domain.Entities.Branches> branchesCommand)
        {
            _BranchesQuery = branchesQuery;
            _BranchesCommand = branchesCommand;
        }

        public async Task<ResponseResult> Handle(ModifyBrancheRequest request, CancellationToken cancellationToken)
        {
            var branch = _BranchesQuery.Find(c => c.Id == request.Id);
            if (branch == null && request.Id != 0)
                return new ResponseResult
                {
                    result = enums.Result.failed,
                    data = "Element is not exist"
                };
            if (request.Id == 0)
                branch = new Domain.Entities.Branches();

            branch.Id = request.Id;
            branch.Title = request.Title;
            branch.ManagerName = request.ManagerName;
            branch.OpenningHour = request.OpenningHour;
            branch.ClosingHour = request.ClosingHour;
            var updated = await _BranchesCommand.UpdateAsyn(branch);
            return new ResponseResult
            {
                result = updated ? enums.Result.success : enums.Result.failed
            };
        }
    }
}
