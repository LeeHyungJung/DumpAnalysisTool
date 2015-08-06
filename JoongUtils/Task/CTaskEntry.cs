using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Concurrent;

namespace JoongUtils.Task
{
    public class CTaskEntry : ITaskEntry
    {
        #region member
        List<ITask> m_TaskList;

        ITaskEntry m_NextEntry;

        ConcurrentQueue<object> m_contentsQue;

        string m_name;
        #endregion
        #region construrct
        public CTaskEntry(string name)
        {
            m_TaskList = new List<ITask>();

            m_contentsQue = new ConcurrentQueue<object>();

            m_NextEntry = null;

            m_name = name;

            getInputDesc = null;
        }
        #endregion
        #region event

        object objectLock = new Object();

        event DelegateTaskStart taskStart;

        event DelegateTaskStart ITask.TaskStart
        {
            add
            {
                lock (objectLock)
                {
                    taskStart += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    taskStart -= value;
                }
            }
        }

        event DelegateTaskEnd taskEnd;

        event DelegateTaskEnd ITask.TaskEnd
        {
            add
            {
                lock (objectLock)
                {
                    taskEnd += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    taskEnd -= value;
                }
            }
        }

        event DelegateTaskProgressChanged taskProgressChange;

        event DelegateTaskProgressChanged ITask.TaskProgressChange
        {
            add
            {
                lock (objectLock)
                {
                    taskProgressChange += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    taskProgressChange -= value;
                }
            }
        }

        event DelegateTaskOutputPush taskPushOutput;

        event DelegateTaskOutputPush ITask.TaskOutputPush
        {
            add
            {
                lock (objectLock)
                {
                    taskPushOutput += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    taskPushOutput -= value;
                }
            }
        }

        event DelegateGetInputDesc getInputDesc;

        event DelegateGetInputDesc ITask.GetInputDesc
        {
            add
            {
                lock (objectLock)
                {
                    getInputDesc += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    getInputDesc -= value;
                }
            }
        }
        #endregion

        #region function
        public void Push(object obj)
        {
            m_contentsQue.Enqueue(obj);
        }

        public object Pop()
        {
            object obj = null;

            if(m_contentsQue.TryDequeue(out obj) == false)
            {

            }

            return obj;
        }

        public bool IsBusy()
        {
            bool isBusy = false;

            foreach (ITask task in m_TaskList)
            {
                isBusy = isBusy || task.IsBusy();
            }

            return isBusy;
        }

        public ITaskEntry Next()
        {
            return m_NextEntry;
        }

        public void SetNextEntry(ITaskEntry taskEntry)
        {
            m_NextEntry = taskEntry;
        }

        public void AddTask(ITask task)
        {
            m_TaskList.Add(task);
            task.GetInputDesc       += Pop;
            task.TaskEnd            += OnTaskEnd;
            task.TaskProgressChange += OnTaskProgressChange;
            task.TaskOutputPush     += OnTaskOutputPush;
        }

        protected void OnTaskProgressChange(object sender, ProgressChangedEventArgs e)
        {
            if(taskProgressChange != null)
            {
                taskProgressChange(sender, e);
            }
        }

        protected void OnTaskEnd(object sender, RunWorkerCompletedEventArgs e)
        {
            if(m_contentsQue.Count > 0)
            {
                Run();
            }
            else
            {
                if (IsBusy() == false)
                {
                    if (taskEnd != null)
                    {
                        taskEnd(sender, e);
                    }

                    if(m_NextEntry != null)
                    {
                        m_NextEntry.Run();
                    }
                }
            }
        }

        protected void OnTaskOutputPush(object obj)
        {
            if(m_NextEntry != null)
            {
                m_NextEntry.Push(obj);
            }

            if (taskPushOutput != null)
            {
                taskPushOutput(obj);
            }
        }

        public void Run()
        {
            foreach(ITask task in m_TaskList)
            {
                if(task.IsBusy() == false)
                {
                    task.Run();
                }
            }

            if(taskStart != null)
            {
                taskStart(m_name);
            }
        }

        public void Cancle()
        {
            foreach (ITask task in m_TaskList)
            {
                task.Cancle();
            }
        }
        #endregion
    }
}
