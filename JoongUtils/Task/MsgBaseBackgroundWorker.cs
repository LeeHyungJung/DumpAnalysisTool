using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Concurrent;

namespace JoongUtils.Task
{
    /// <summary>
    /// 시퀀스가 있는 일을 수행하기 위한 스레드 INPUT_MSG 가 처리되어 OUTPUT_DATA 로 나온다.
    /// </summary> 
    public class MsgBaseBackgroundWorker<INPUT_MSG, OUTPUT_DATA>
    {
        public delegate void DelegateProgressChanged(object sender, ProgressChangedEventArgs e);
        public delegate void DelegateRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e);
        public delegate void DelegateMsgProgressChanged(INPUT_MSG msg, float percent , int msgIndex);
        
        #region Members
        ConcurrentQueue<INPUT_MSG> m_QueInputMsg;
        ConcurrentBag<OUTPUT_DATA> m_ListOutputData;
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

        DelegateMsgProgressChanged m_DelegateMsgProgressChanged;

        public DelegateMsgProgressChanged MsgProgressChanged
        {
            get { return m_DelegateMsgProgressChanged; }
            set { m_DelegateMsgProgressChanged = value; }
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

            m_QueInputMsg = new ConcurrentQueue<INPUT_MSG>();
            m_ListOutputData = new ConcurrentBag<OUTPUT_DATA>();

            m_backgroundWorker = new BackgroundWorker();
            m_backgroundWorker.WorkerReportsProgress = true;
            m_backgroundWorker.WorkerSupportsCancellation = true;
            m_backgroundWorker.DoWork += doWork;
            m_backgroundWorker.RunWorkerCompleted += runWorkerCompleted;
            m_backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(progressChanged);
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

        /// <summary>
        /// doWork의 작업을 비동기로 취소하는 함수
        /// </summary> 
        public void Cancle()
        {
            ClearInputMsg();
            ClearOutputMsg();

            if (m_backgroundWorker != null)
            {
                m_backgroundWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Thread의 Main Loop
        /// </summary>
        protected virtual void doWork(object sender, DoWorkEventArgs e)
        {
            while ((m_QueInputMsg.Count > 0) && m_backgroundWorker.CancellationPending == false)
            {
                Console.WriteLine("worker thread: working...");

                INPUT_MSG obj;

                if(m_QueInputMsg.TryDequeue(out obj))
                {
                    _Work(obj);
                }

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

            ClearOutputMsg();
            m_iCurDataIndex = 0;
            m_iTotalInputDataSize = 0;
        }

        public int CompleteMsgCount()
        {
            return m_ListOutputData.Count;
        }

        public void ClearOutputMsg()
        {
            OUTPUT_DATA msg;

            while (!m_ListOutputData.IsEmpty)
            {
                m_ListOutputData.TryTake(out msg);
            }
        }

        public void ClearInputMsg()
        {
            INPUT_MSG msg;

            while (m_QueInputMsg.TryDequeue(out msg))
            {
            }
        }
    }
}
