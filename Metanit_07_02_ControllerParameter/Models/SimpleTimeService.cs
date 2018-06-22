using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_07_02_ControllerParameter.Models
{
    public class SimpleTimeService : ITimeService
    {
        public SimpleTimeService()
        {
            Time = DateTime.Now.ToString("hh:mm:ss");
        }
        public string Time { get; }
    }
}
