using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DumpAnalysisTool.Src.StaticManager;
using DumpAnalysisTool.Src.Define;
using DumpAnalysisTool.Src.Page;
using DumpAnalysisTool.Src.Model;

namespace DumpAnalysisTool.Src.ViewModel
{
    /// <summary>
    /// MainPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StartPage : BasePage
    {
        StartPageModel m_model;

        public StartPage()
            : base(ePAGE.ePage_StartPage)
        {
            m_model = new StartPageModel();

            InitializeComponent();

            DataContext = m_model;
        }

        private void One_Button_Click(object sender, RoutedEventArgs e)
        {
            LinkedList<ePAGE> pageList = new LinkedList<ePAGE>();

            pageList.AddLast(ePAGE.ePage_StartPage);

            if(m_model.IsCheckedDownload)
            {
                pageList.AddLast(ePAGE.ePage_DownloadPage);
            }
            if(m_model.IsCheckedAnalysis)
            {
                pageList.AddLast(ePAGE.ePage_AnalysisPage);
            }
            if(m_model.IsCheckedStatistic)
            {
                pageList.AddLast(ePAGE.ePage_StatisticPage);
            }

            pageList.AddLast(ePAGE.ePage_ProgressPage);

            PageManager.setPageSeq(pageList);
            PageManager.setPageChange(pageList.First.Next != null ? pageList.First.Next.Value :pageList.First.Value);
        }
    }
}
