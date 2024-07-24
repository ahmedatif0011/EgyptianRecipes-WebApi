using App.Domain.Models.shared;
using MediatR;

namespace App.Core.Handler.Branches.DeleteBranch
{
    public class DeleteBranchRequest :App.Domain.Models.Request.DeleteBranch ,IRequest<ResponseResult>
    {
    }
}
