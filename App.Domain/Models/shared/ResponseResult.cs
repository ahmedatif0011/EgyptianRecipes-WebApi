using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Domain.Models.shared.enums;

namespace App.Domain.Models.shared
{
    public class ResponseResult
    {
        public Result result { get; set; }
        public object data { get; set; }
        public int totalData { get; set; }
        public string note { get; set; }
    }
}
