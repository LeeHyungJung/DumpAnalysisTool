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
using System.ComponentModel;
using System.Windows.Media.Effects;

using MahApps.Metro.Controls;

using DumpAnalysisTool.Src.Worker;
using DumpAnalysisTool.Src.Page;
using DumpAnalysisTool.Src.Define;
using DumpAnalysisTool.Src.Model;
using DumpAnalysisTool.Src.ViewModel;
using DumpAnalysisTool.Src.StaticManager;

namespace DumpAnalysisTool
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow, IMainView
    {

        PageInfo m_model;

        Effect m_effect;

        public MainWindow()
        {
            m_model = new PageInfo();

            InitializeComponent();

            DataContext = m_model;
           
            InitPage();
           
            PageManager.setPageListener(this);

            PageManager.setPageChange(ePAGE.ePage_StartPage);

            m_effect = gdBackgroundContainer.Effect;

            _updateEffect();
        }

        private void InitPage()
        { 
            m_model.m_pageList.Add(ePAGE.ePage_StartPage, new StartPage());
            m_model.m_pageList.Add(ePAGE.ePage_AnalysisPage, new AnalysisPage());
            m_model.m_pageList.Add(ePAGE.ePage_DownloadPage, new DownloadPage());
            m_model.m_pageList.Add(ePAGE.ePage_StatisticPage, new StatisticPage());
            m_model.m_pageList.Add(ePAGE.ePage_ProgressPage, new ProgressPage());
        }

        private void _updateEffect()
        {
            if (foFlyout.IsOpen)
            {
                gdBackgroundContainer.Effect = m_effect;
            }
            else
            {
                gdBackgroundContainer.Effect = null;
            }
        }

        public void setChangePageRequest(ePAGE page)
        {
           m_model.setPage(page);
        }

        public void setPrevPage()
        {
            m_model.setPrePage();
        }

        public void setNextPage()
        {
            m_model.setNextPage();
        }

        public void setPageSeq(LinkedList<ePAGE> pageList)
        {
            m_model.setPageSeq(pageList);
        }

        private void Start_Page_Button_Click(object sender, RoutedEventArgs e)
        {
            m_model.clearRelativePage();
            PageManager.setPageChange(ePAGE.ePage_StartPage);
            foFlyout.IsOpen = !foFlyout.IsOpen;
        }

        private void foFlyout_IsOpenChanged(object sender, RoutedEventArgs e)
        {
            _updateEffect();
        }

        private void Setting_Button_Click(object sender, RoutedEventArgs e)
        {
            foFlyout.IsOpen = !foFlyout.IsOpen;
        }
    }
}
