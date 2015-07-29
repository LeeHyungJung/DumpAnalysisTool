using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace JoongUtils.Worker
{
    public class MsgBaseBackgroundWorker<INPUT_MSG, OUTPUT_DATA> 
    {
        public delegate void DelegateProgressChanged(object sender, ProgressChangedEventArgs e);
        public delegate void DelegateRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e);

        #region Members
        Queue<INPUT_MSG> m_QueInputMsg;
        List<OUTPUT_DATA> m_ListOutputData;
        BackgroundWorker m_backgroundWorker;

        DelegateProgressChanged m_DelegateProgressChanged;
        public DelegateProgressChanged ProgressChanged
        {
            get { return m_DelegateProgressChanged; }
            set { m_DelegateProgressChanged = value; }
        }
        DelegateRunWorkerCompleted m_DelegateRunWorkerCompleted;
        public DelegateRunWorkerCompleted RunWorkerCompleted
        {
            get { return m_DelegateRunWorkerCompleted; }
            set { m_DelegateRunWorkerCompleted = value; }
        }

        int m_iCurDataIndex;
        public int CurDataIndex
        {
            get { return m_iCurDataIndex; }
        }
        int m_iTotalInputDataSize;
        public int TotalInputDataSize
        {
            get { return m_iTotalInputDataSize; }
        }
        #endregion

        #region Constructor
        public MsgBaseBackgroundWorker()
        {
            m_iCurDataIndex = 0;
            m_iTotalInputDataSize = 0;

            m_QueInputMsg       = new Queue<INPUT_MSG>();
            m_ListOutputData    = new List<OUTPUT_DATA>();

            m_backgroundWorker = new BackgroundWorker();
            m_backgroundWorker.WorkerReportsProgress = true;
            m_backgroundWorker.WorkerSupportsCancellation = true;
            m_backgroundWorker.DoWork += doWork;
            m_backgroundWorker.RunWorkerCompleted += runWorkerCompleted;
            m_backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(progressChanged);
        }
        #endregion

        public void RunWorkerAsync()
        {
           m_backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Thread의 Main Loop
        /// </summary>
        protected virtual void doWork(object sender, DoWorkEventArgs e)
        {
            while ((m_QueInputMsg.Count > 0) && m_backgroundWorker.CancellationPending == false)
            {
                Console.WriteLine("worker thread: working...");

                INPUT_MSG obj = m_QueInputMsg.Peek();

                _Work(obj);

                m_QueInputMsg.Dequeue();

                m_iCurDataIndex++;
            }
            Console.WriteLine("worker thread: terminating gracefully.");

            e.Result = m_ListOutputData;
        }

        protected virtual void _Work(INPUT_MSG obj){}

        public void Push(INPUT_MSG msg)
        {
            m_QueInputMsg.Enqueue(msg);

            m_iTotalInputDataSize++;
        }

        protected void ReportProgress(INPUT_MSG msg, int progress)
        {
            if (ProgressChanged != null)
            {
                m_backgroundWorker.ReportProgress(progress, (object)msg);
            }
        }

        /// <summary>
        /// doWork의 작업을 비동기로 취소하는 함수
        /// </summary> 
        public void CancelWorkerAsync()
        {
            if (m_backgroundWorker != null)
            {
                m_backgroundWorker.CancelAsync();
            }
        }

        /// <summary>
        /// doWork 함수의 DoWorkEventArgs 의 Report 함수를 사용할 때 호출되는 CallBack 함수
        /// </summary>
        protected void progressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(sender, e);
            }
        }
        /// <summary>
        /// doWork의 작업이 완료되었을 때 호출되는 함수
        /// </summary>
        protected void runWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (RunWorkerCompleted != null)
            {
                RunWorkerCompleted(sender, e);
            }

            m_ListOutputData.Clear();
            m_iCurDataIndex = 0;
            m_iTotalInputDataSize = 0;
        }

        public int CompleteMsgCount()
        {
            return m_ListOutputData.Count;
        }

        public bool IsBusy()
        {
            return m_backgroundWorker.IsBusy;
        }
    }
}
