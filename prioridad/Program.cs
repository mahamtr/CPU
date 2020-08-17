using System;
using System.Collections.Generic;
using System.Linq;

namespace prioridad
{
    class Program
    {


        static void Main(string[] args)
        {
            //var inputProcesses = new List<Process>
            //            {
            //new Process{ProcessId="A",ArrivalTime=1,Priority=4,CPUTime=8},
            //new Process{ProcessId="B",ArrivalTime=2,Priority=7,CPUTime=4},
            //new Process{ProcessId="C",ArrivalTime=3,Priority=3,CPUTime=2},
            //new Process{ProcessId="D",ArrivalTime=4,Priority=5,CPUTime=3},
            //new Process{ProcessId="E",ArrivalTime=5,Priority=1,CPUTime=6},
            //new Process{ProcessId="F",ArrivalTime=6,Priority=6,CPUTime=4},
            //new Process{ProcessId="G",ArrivalTime=7,Priority=5,CPUTime=5},
            //new Process{ProcessId="H",ArrivalTime=8,Priority=4,CPUTime=7},
            //new Process{ProcessId="I",ArrivalTime=9,Priority=8,CPUTime=2},
            //new Process{ProcessId="J",ArrivalTime=10,Priority=9,CPUTime=4},
            //            };

            var inputProcesses = new List<Process>
            {
                new Process{ProcessId="P1",ArrivalTime=0,Priority=2,CPUTime=7},
                new Process{ProcessId="P2",ArrivalTime=5,Priority=0,CPUTime=8},
                new Process{ProcessId="P3",ArrivalTime=12,Priority=2,CPUTime=2},
                new Process{ProcessId="P4",ArrivalTime=2,Priority=1,CPUTime=13},
                new Process{ProcessId="P5",ArrivalTime=9,Priority=4,CPUTime=2},
            };
            //            var inputProcesses = new List<Process>
            //            {
            //new Process{ProcessId="P1",ArrivalTime=0,Priority=2,CPUTime=7},
            //new Process{ProcessId="P2",ArrivalTime=5,Priority=0,CPUTime=8},
            //new Process{ProcessId="P3",ArrivalTime=12,Priority=2,CPUTime=2},
            //new Process{ProcessId="P4",ArrivalTime=2,Priority=1,CPUTime=6},
            //new Process{ProcessId="P5",ArrivalTime=9,Priority=4,CPUTime=2},
            //            };

            var cpu = new CPU(4, inputProcesses);
            var response = cpu.StartPriorityProcessing();
            Console.WriteLine("Execution | Ready                     | Blocked");
            foreach (var item in response)
            {
                Console.WriteLine(item.Execution + "         |" + string.Join(",", item.Ready) + "       |" + string.Join(",", item.Block));
            }
        }

       
    }
}
