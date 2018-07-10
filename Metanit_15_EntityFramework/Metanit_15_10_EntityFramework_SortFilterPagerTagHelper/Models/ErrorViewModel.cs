using System;

namespace Metanit_15_10_EntityFramework_SortFilterPagerTagHelper.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}