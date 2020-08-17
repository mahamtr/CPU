using DeepEqual.Syntax;
using prioridad;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CPUTesting
{
    public class UnitTest1
    {
        [Fact]
        public void PriorityCPU_1()
        {
            //Arrange
            var responseAssert = new List<Response>
            {
                new Response{Execution="E",Ready=new string[9]{ "A", "B", "C", "D", "F", "G", "H", "I", "J" },Block=new string[0] },
                new Response{Execution="C",Ready=new string[8]{"A","B","D","F","G","H","I","J"},Block=new string[1]{"E"} },
                new Response{Execution="A",Ready=new string[7]{ "B","D","F","G","H","I","J"},Block=new string[1]{"E"} },
                new Response{Execution="H",Ready=new string[6]{"B","D","F","G","I","J" },Block=new string[2]{"E","A"} },
                new Response{Execution="D",Ready=new string[5]{"B","F","G","I","J"},Block=new string[3] {"E","A","H"}},
                new Response{Execution="G",Ready=new string[4]{"B","F","I","J"  },Block=new string[3] {"E","A","H"}},
                new Response{Execution="F",Ready=new string[3]{"B","I","J" },Block=new string[4] {"E","A","H","G"}},
                new Response{Execution="B",Ready=new string[2]{"I","J" },Block=new string[4] {"E","A","H","G"}},
                new Response{Execution="I",Ready=new string[1]{"J" },Block=new string[4] {"E","A","H","G"}},
                new Response{Execution="J",Ready=new string[1]{"E" },Block=new string[3] {"A","H","G"}},
                new Response{Execution="E",Ready=new string[1]{"A"  },Block=new string[2]{"H","G"} },
                new Response{Execution="A",Ready=new string[1]{ "H"},Block=new string[1]{"G"} },
                new Response{Execution="H",Ready=new string[1]{ "G" },Block=new string[0] },
                new Response{Execution="G",Ready=new string[0],Block=new string[0] },

            };
            var inputProcesses = new List<Process>
            {
new Process{ProcessId="A",ArrivalTime=1,Priority=4,CPUTime=8},
new Process{ProcessId="B",ArrivalTime=2,Priority=7,CPUTime=4},
new Process{ProcessId="C",ArrivalTime=3,Priority=3,CPUTime=2},
new Process{ProcessId="D",ArrivalTime=4,Priority=5,CPUTime=3},
new Process{ProcessId="E",ArrivalTime=5,Priority=1,CPUTime=6},
new Process{ProcessId="F",ArrivalTime=6,Priority=6,CPUTime=4},
new Process{ProcessId="G",ArrivalTime=7,Priority=5,CPUTime=5},
new Process{ProcessId="H",ArrivalTime=8,Priority=4,CPUTime=7},
new Process{ProcessId="I",ArrivalTime=9,Priority=8,CPUTime=2},
new Process{ProcessId="J",ArrivalTime=10,Priority=9,CPUTime=4},
            };

            //Act
            var cpu = new CPU(4, inputProcesses);
            var response = cpu.StartPriorityProcessing();

            //Assert
            Assert.Equal(responseAssert.Count, response.Count);
            responseAssert.ShouldDeepEqual(response);
        }


        [Fact]
        public void PriorityCPU_2()
        {
            //Arrange
            var responseAssert = new List<Response>
            {
                new Response { Execution = "P2", Ready = new string[4] { "P1", "P3", "P4", "P5" }, Block = new string[0] },
                new Response { Execution = "P4", Ready = new string[3] { "P1", "P3", "P5" }, Block = new string[1] { "P2" } },
                new Response { Execution = "P1", Ready = new string[2] { "P3", "P5" }, Block = new string[2] { "P2", "P4" } },
                new Response { Execution = "P3", Ready = new string[1] { "P5" }, Block = new string[3] { "P2", "P4", "P1" } },
                new Response { Execution = "P5", Ready = new string[1] { "P2" }, Block = new string[2] { "P4", "P1" } },
                new Response { Execution = "P2", Ready = new string[1] { "P4" }, Block = new string[1] { "P1" } },
                new Response { Execution = "P4", Ready = new string[1] { "P1" }, Block = new string[0] { } },
                new Response { Execution = "P1", Ready = new string[0] { }, Block = new string[0] { } },
            };
            var inputProcesses = new List<Process>
            {
new Process{ProcessId="P1",ArrivalTime=0,Priority=2,CPUTime=7},
new Process{ProcessId="P2",ArrivalTime=5,Priority=0,CPUTime=8},
new Process{ProcessId="P3",ArrivalTime=12,Priority=2,CPUTime=2},
new Process{ProcessId="P4",ArrivalTime=2,Priority=1,CPUTime=6},
new Process{ProcessId="P5",ArrivalTime=9,Priority=4,CPUTime=2},
            };

            //Act
            var cpu = new CPU(4, inputProcesses);
            var response = cpu.StartPriorityProcessing();

            //Assert
            Assert.Equal(responseAssert.Count, response.Count);
            responseAssert.ShouldDeepEqual(response);

        }

        [Fact]
        public void PriorityCPU_3_moreThan2Cycles()
        {
            //Arrange
            var responseAssert = new List<Response>
            {
new Response{Execution="P2",Ready=new string[4]{"P1","P3","P4","P5"},Block=new string[0] },
new Response{Execution="P4",Ready=new string[3]{"P1","P3","P5"   },Block=new string[1]{"P2"} },
new Response{Execution="P1",Ready=new string[2]{ "P3","P5"  },Block=new string[2]{"P2","P4"} },
new Response{Execution="P3",Ready=new string[1]{"P5"   },Block=new string[3]{"P2","P4","P1"} },
new Response{Execution="P5",Ready=new string[1]{"P2"  },Block=new string[2] {"P4","P1"}},
new Response{Execution="P2",Ready=new string[1]{"P4"  },Block=new string[1] {"P1"}},
new Response{Execution="P4",Ready=new string[1]{"P1" },Block=new string[0] {}},
new Response{Execution="P1",Ready=new string[1]{"P4" },Block=new string[0] {}},
new Response{Execution="P4",Ready=new string[1]{"P4" },Block=new string[0] {}},
new Response{Execution="P4",Ready=new string[0]{},Block=new string[0] {}},
            };
            var inputProcesses = new List<Process>
            {
                new Process{ProcessId="P1",ArrivalTime=0,Priority=2,CPUTime=7},
                new Process{ProcessId="P2",ArrivalTime=5,Priority=0,CPUTime=8},
                new Process{ProcessId="P3",ArrivalTime=12,Priority=2,CPUTime=2},
                new Process{ProcessId="P4",ArrivalTime=2,Priority=1,CPUTime=13},
                new Process{ProcessId="P5",ArrivalTime=9,Priority=4,CPUTime=2},
            };

            //Act
            var cpu = new CPU(4, inputProcesses);
            var response = cpu.StartPriorityProcessing();

            //Assert
            Assert.Equal(responseAssert.Count, response.Count);
            responseAssert.ShouldDeepEqual(response);

        }
    }
}
