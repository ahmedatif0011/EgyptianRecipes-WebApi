using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models.Request
{
    public class ModifyBranchDTOs
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ManagerName { get; set; }
        public TimeSpan OpenningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }
    }
    public class DeleteBranch 
    {
        [Required]
        public int Id { get; set; }
    }
    public class GetBranches
    {
        public string? searchCriteria { get; set; }
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 3;
    }


}
