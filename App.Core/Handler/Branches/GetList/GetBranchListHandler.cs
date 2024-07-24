using App.Domain.Models.Response;
using App.Domain.Models.shared;
using App.Infrastructure.Interfaces.Repository;
using Azure.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Handler.Branches.GetList
{
    public class GetBranchListHandler : IRequestHandler<GetBranchListRequest, ResponseResult>
    {
        private readonly IRepositoryQuery<App.Domain.Entities.Branches> _BranchesQuery;

        public GetBranchListHandler(IRepositoryQuery<Domain.Entities.Branches> branchesQuery)
        {
            _BranchesQuery = branchesQuery;
        }

        public async Task<ResponseResult> Handle(GetBranchListRequest request, CancellationToken cancellationToken)
        {
            var data = _BranchesQuery.TableNoTracking
                .Where(c=> request.searchCriteria != null ? (c.Title.Contains(request.searchCriteria) || c.ManagerName.Contains(request.searchCriteria)) :true )
                .OrderByDescending(c=> c.Id);
            int totalData = data.Count();
            var res = data
                .Skip((request.pageNumber - 1) * request.pageSize)
                .Take(request.pageSize)
                .Select(c => new GetBranchResponseDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    ManagerName = c.ManagerName,
                    OpenningHour = c.OpenningHour,
                    ClosingHour = c.ClosingHour,
                });
            return new ResponseResult
            {
                result = totalData > 0 ? enums.Result.success : enums.Result.noDataFound,
                data = res,
                totalData = totalData
            };
        }
    }
}
