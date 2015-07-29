using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoongUtils.Worker;
using DumpAnalysisTool.Src.Model;
using DumpAnalysisTool.Src.Data;

namespace DumpAnalysisTool.Src.Worker
{
    class FTPDirectoryCrawler : MsgBaseBackgroundWorker<string, DownloadTargetData>
    {
        int m_iRetryCount;
        int m_iTimeOut;

        #region Constructor
        public FTPDirectoryCrawler(int iRetryCount,int iTimeOut)
        {
            m_iRetryCount = iRetryCount;
            m_iTimeOut = iTimeOut;
        }
        #endregion

        protected override void _Work(string msg)
        {
            if (msg == null) return;

            DownloadTargetData info = new DownloadTargetData();

            int counter = 0;
            string line;


        }
    }
}
