using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.ComponentModel;


namespace JoongUtils.Worker
{
    class TaskGroup
    {
        public delegate void DelegateEndTaskGroup(String taskName);

        #region Members
        DelegateEndTaskGroup m_DelegateEndTaskGroup;

        public DelegateEndTaskGroup EndTaskGroup
        {
            get { return m_DelegateEndTaskGroup; }
            set { m_DelegateEndTaskGroup = value; }
        }

        private BackgroundWorker m_backgroundWorker;

        private TaskGroup m_next;

        public JoongUtils.Worker.TaskGroup Next
        {
            get { return m_next; }
            set { m_next = value; }
        }

        private ConcurrentBag<ITask> m_TaskBag;

        private String m_name;
        #endregion

        #region Constructor
        public TaskGroup(String name)
        {
            m_TaskBag = new ConcurrentBag<ITask>();

            m_backgroundWorker = new BackgroundWorker();

            m_backgroundWorker.WorkerReportsProgress = true;
            m_backgroundWorker.WorkerSupportsCancellation = true;
            m_backgroundWorker.DoWork += DoWork;
            m_backgroundWorker.RunWorkerCompleted += RunWorkerCompleted;
        }
        #endregion

        protected virtual void DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (ITask task in m_TaskBag)
            {
                if (task.IsBusy() == false)
                {
                    task.Run();
                }
            }

            while (RunningCount() > 0)
            {
                
            }
        }

        protected void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(m_next != null)
            {
                Next.Run();
            }

            if(EndTaskGroup != null)
            {
                EndTaskGroup(m_name);
            }
        }

        public void putTask(ITask task)
        {
            if(m_backgroundWorker.IsBusy == false)
            {
                m_TaskBag.Add(task);
            }
        }

        public bool IsEmpty()
        {
            return m_TaskBag.IsEmpty;
        }

        public void Run()
        {
            m_backgroundWorker.RunWorkerAsync();
        }

        public int RunningCount()
        {
            int count = 0;

            foreach (ITask task in m_TaskBag)
            {
                if (task.IsBusy() == true)
                {
                    count++;
                }
            }

            return count;
        }

        public void ClearTask()
        {
            CancleTask();

            ITask task;
            while (!m_TaskBag.IsEmpty)
            {
                m_TaskBag.TryTake(out task);
            }
        }

        public bool IsAllSleepLinkedTaskGroup()
        {
            if (RunningCount() != 0) return false;

            if(m_next != null)
            {
                return m_next.IsAllSleepLinkedTaskGroup();
            }

            return true;
        }

        public void CancleTask()
        {
            foreach (ITask task in m_TaskBag)
            {
                if (task.IsBusy() == true)
                {
                    task.Cancle();
                }
            }
        }
    }
}
