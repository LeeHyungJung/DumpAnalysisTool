using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MicroMvvm;

namespace DumpAnalysisTool.Src.Model
{
    class StartPageModel : BasePageModel
    {
        bool m_bCheckedDownload;

        public bool IsCheckedDownload
        {
            get { return m_bCheckedDownload; }
            set {
                m_bCheckedDownload = value;
                RaisePropertyChanged("IsCheckedDownload");
            }
        }

        bool m_bCheckedAnalysis;

        public bool IsCheckedAnalysis
        {
            get { return m_bCheckedAnalysis; }
            set {

                m_bCheckedAnalysis = value;
                RaisePropertyChanged("IsCheckedAnalysis");
            }
        }

        bool m_bCheckedStatistic;

        public bool IsCheckedStatistic
        {
            get { return m_bCheckedStatistic; }
            set { 
                m_bCheckedStatistic = value;
                RaisePropertyChanged("IsCheckedStatistic");
            }
        }


    }
}
