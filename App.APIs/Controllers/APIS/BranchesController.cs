using App.Api.Controllers.BaseController;
using App.Core.Handler.Branches.DeleteBranch;
using App.Core.Handler.Branches.GetList;
using App.Core.Handler.Branches.UpdateBranch;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.APIs.Controllers.APIS
{
    public class BranchesController : ApiControllerBase
    {
        public BranchesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet(nameof(GetListOfBranches))]
        public async Task<IActionResult> GetListOfBranches([FromQuery] GetBranchListRequest request)
        {
            var res = await QueryAsync(request);
            return Ok(res);
        }
        [HttpPost(nameof(AddBranch))]
        public async Task<IActionResult> AddBranch([FromBody] ModifyBrancheRequest request)
        {
            request.Id = 0;
            var res = await CommandAsync(request);
            return Ok(res);
        }
        [HttpPut(nameof(updateBranch))]
        public async Task<IActionResult> updateBranch([FromBody] ModifyBrancheRequest request)
        {
            var res = await CommandAsync(request);
            return Ok(res);
        }
        [HttpDelete(nameof(deleteBranch))]
        public async Task<IActionResult> deleteBranch([FromBody] DeleteBranchRequest request)
        {
            var res = await CommandAsync(request);
            return Ok(res);
        }
    }
}
