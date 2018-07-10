using System;

namespace Metanit_15_09_EntityFramework_SortFilterPager.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}