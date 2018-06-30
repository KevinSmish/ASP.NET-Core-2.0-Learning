using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_11_06_CreateBinder.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string Name { get; set; }            // название событие
        public DateTime EventDate { get; set; }     // дата и время событие
    }
}
