using App.Domain.Models.Request;
using App.Domain.Models.shared;
using MediatR;

namespace App.Core.Handler.Branches.UpdateBranch
{
    public class ModifyBrancheRequest : ModifyBranchDTOs, IRequest<ResponseResult>
    {
    }
}
