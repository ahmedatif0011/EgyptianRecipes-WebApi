using App.Domain.Models.Request;
using App.Domain.Models.shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Handler.Branches.GetList
{
    public class GetBranchListRequest : GetBranches,IRequest<ResponseResult>
    {
    }
}
