using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baisse.Model.Models.ApiModel
{
    public class ApiPageInfo
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public int PageCount
        {
            get
            {
                if (TotalCount <= 0 || PageSize <= 0) return 0;

                return (TotalCount % PageSize) == 0 ? (TotalCount / PageSize) : (TotalCount / PageSize) + 1;
            }
        }
    }
}
