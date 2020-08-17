using System;
using System.Collections.Generic;
using System.Text;

namespace prioridad
{
    public class Process
    {
        public string ProcessId { get; set; }
        public int ArrivalTime { get; set; }
        public int Priority { get; set; }
        public int CPUTime { get; set; }
    }
}
