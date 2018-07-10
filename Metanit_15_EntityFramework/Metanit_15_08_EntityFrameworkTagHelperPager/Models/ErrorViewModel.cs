using System;

namespace Metanit_15_08_EntityFrameworkTagHelperPager.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}