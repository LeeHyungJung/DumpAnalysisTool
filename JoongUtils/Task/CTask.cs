using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace JoongUtils.Task
{
    class CTask : ITask
    {
        #region Members
        BackgroundWorker m_backgroundWorker;

        string m_name;
        #endregion

        #region Constructor
        public CTask(string name)
        {
            m_name = name;

            m_backgroundWorker = new BackgroundWorker();
            m_backgroundWorker.WorkerSupportsCancellation = true;
            m_backgroundWorker.DoWork += Work;
            m_backgroundWorker.RunWorkerCompleted += runWorkerCompleted;
            m_backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(progressChanged);
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

        public bool IsBusy()
        {
            return m_backgroundWorker.IsBusy;
        }

        public void Run()
        {
            m_backgroundWorker.RunWorkerAsync();
        }

        public void Cancle()
        {
            m_backgroundWorker.CancelAsync(); 
        }

        public virtual void Work(object sender, DoWorkEventArgs e)
        {

        }

        private void runWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           if(taskEnd != null)
           {
               taskEnd(sender, e);
           }
        }

        private void progressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (taskProgressChange != null)
            {
                taskProgressChange(sender, e);
            }
        }

        protected void ReportProgress(object desc, int progress)
        {
            m_backgroundWorker.ReportProgress(progress, (object)desc);
        }
    }
}
