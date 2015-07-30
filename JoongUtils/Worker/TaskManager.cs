using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoongUtils.Worker
{
    class TaskManager
    {
        private static object lockObj;
        private static List<TaskGroup> m_TaskGroupBag;

        public static void AddTaskGroup(TaskGroup task)
        {
            lock(lockObj)
            {
                m_TaskGroupBag = new List<TaskGroup>();
            }
        }

        public static void RemoveTaskGroup(TaskGroup task)
        {
            lock (lockObj)
            {
                m_TaskGroupBag.Remove(task);
            }
        }
    }
}
