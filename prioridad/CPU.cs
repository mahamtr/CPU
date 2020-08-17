using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace prioridad
{
    public class CPU
    {
        public int CPUTime { get; set; }
        public List<Process> InputProcesses { get; set; }

        public CPU(int _cpuTime, List<Process> _inputProcesses)
        {
            CPUTime = _cpuTime;
            InputProcesses = _inputProcesses;
        }

        public List<Response> StartPriorityProcessing()
        {
            var readyList = InputProcesses.Select(i => i.ProcessId).ToList();
            var responseList = new List<Response>();
            var blockedList = new List<string>();
            var blockedListOfCycle = new List<string>();
            var wasLastExecutionBlocked = false;
            String lastExecutionId = "";
            var actualPriority = 0;
            
            while (InputProcesses.Count != 0 )
            {
                List<Process> notFinishedProcesses = InputProcesses;
                Process processToServe;
                int highestPriority = InputProcesses.Max(i => i.Priority);
                if (wasLastExecutionBlocked)
                {
                    blockedList.Add(lastExecutionId);
                }
                processToServe = GetProcessToServe(notFinishedProcesses, ref actualPriority, highestPriority, blockedListOfCycle);

                lastExecutionId = processToServe.ProcessId;

                if (processToServe.CPUTime > CPUTime)
                {
                    processToServe.CPUTime -= CPUTime;
                    wasLastExecutionBlocked = true;
                    readyList.Remove(processToServe.ProcessId);
                    if (processToServe.CPUTime <= 0)
                    {
                        notFinishedProcesses.Remove(processToServe);
                        responseList.Add(new Response
                        {
                            Execution = processToServe.ProcessId,
                            Ready = readyList.ToArray(),
                            Block = blockedList.ToArray(),
                        });
                        continue;
                    }
                    MoveBlockedProcessToReady(readyList, blockedList, blockedListOfCycle, actualPriority, notFinishedProcesses);
                    responseList.Add(new Response
                    {
                        Execution = processToServe.ProcessId,
                        Ready = readyList.ToArray(),
                        Block = blockedList.ToArray(),
                    });

                    continue;
                }
                notFinishedProcesses.Remove(processToServe);
                readyList.Remove(processToServe.ProcessId);
                MoveBlockedProcessToReady(readyList, blockedList, blockedListOfCycle, actualPriority, notFinishedProcesses);
                if (notFinishedProcesses.Count == 0) blockedList.Clear();
                responseList.Add(new Response
                {
                    Execution = processToServe.ProcessId,
                    Ready = readyList.ToArray(),
                    Block = blockedList.ToArray(),
                });
                wasLastExecutionBlocked = false;

            }
            return responseList;
        }

        private static void MoveBlockedProcessToReady(List<string> readyList, List<string> blockedList, List<string> blockedListOfCycle, int actualPriority, List<Process> notFinishedProcesses)
        {
            if (readyList.Count() == 0 && notFinishedProcesses.Count > 0)
            {
                var processToUnlock = GetProcessToUnblock(notFinishedProcesses, actualPriority, notFinishedProcesses.Max(i => i.Priority), blockedListOfCycle);
                readyList.Add(processToUnlock.ProcessId);
                blockedList.Remove(processToUnlock.ProcessId);
            }
        }

        private static Process GetProcessToServe(List<Process> notFinishedProcesses,ref int actualPriority,int highestPriority,List<string> blockedList)
        {
            Process processToServe;
            var priority = actualPriority;
            var lowestPriority = notFinishedProcesses.OrderBy(i => i.Priority).FirstOrDefault(i => i.Priority >= priority).Priority;
            var processesWithLowestPriority = notFinishedProcesses.Where(i => i.Priority == lowestPriority && !blockedList.Contains(i.ProcessId));
            if (processesWithLowestPriority.Count() > 1)
            {
                processToServe = processesWithLowestPriority.OrderBy(i => i.ArrivalTime).FirstOrDefault();
                blockedList.Add(processToServe.ProcessId);
                if (processToServe.Priority == highestPriority)
                {
                    blockedList.Clear();
                }
                return processToServe;
            }
            processToServe = processesWithLowestPriority.FirstOrDefault();
            actualPriority = UpdateActualPriority(highestPriority, blockedList, processToServe);
            return processToServe;

        }

        private static int UpdateActualPriority(int highestPriority, List<string> blockedList, Process processToServe)
        {
            if (processToServe.Priority == highestPriority)
            {
                blockedList.Clear();
                return 0;
            }
         return processToServe.Priority + 1;
        }

        private static Process GetProcessToUnblock(List<Process> notFinishedProcesses, int actualPriority, int highestPriority, List<string> blockedList)
        {
            var lowestPriority = notFinishedProcesses.OrderBy(i => i.Priority).FirstOrDefault(i => i.Priority >= actualPriority).Priority;
            var processesWithLowestPriority = notFinishedProcesses.Where(i => i.Priority == lowestPriority && !blockedList.Contains(i.ProcessId));
            if (processesWithLowestPriority.Count() > 1)
            {
                return processesWithLowestPriority.OrderBy(i => i.ArrivalTime).FirstOrDefault();

            }
          

                return processesWithLowestPriority.FirstOrDefault();
           

        }

    }
}
