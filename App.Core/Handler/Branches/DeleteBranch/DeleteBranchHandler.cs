using App.Domain.Models.shared;
using App.Infrastructure.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Handler.Branches.DeleteBranch
{
    public class DeleteBranchHandler : IRequestHandler<DeleteBranchRequest, ResponseResult>
    {
        private IRepositoryCommand<App.Domain.Entities.Branches> _BranchesCommand;

        public DeleteBranchHandler(IRepositoryCommand<Domain.Entities.Branches> branchesCommand)
        {
            _BranchesCommand = branchesCommand;
        }

        public async Task<ResponseResult> Handle(DeleteBranchRequest request, CancellationToken cancellationToken)
        {
            var deleted = await _BranchesCommand.DeleteAsync(c => c.Id == request.Id);
            return new ResponseResult
            {
                result = deleted ? enums.Result.success : enums.Result.failed,
            };
        }
    }
}
