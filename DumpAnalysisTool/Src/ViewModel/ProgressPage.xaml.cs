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
    /// ProgressPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProgressPage : BasePage
    {
        ProgressPageModel m_model;

        public ProgressPage()
            : base(ePAGE.ePage_ProgressPage)
        {
            m_model = new ProgressPageModel();

            InitializeComponent();

            DataContext = m_model;
        }

        public override void onPageClose()
        {
            base.onPageClose();

            m_model.LogEntry.Clear();
        }

        private void btStop_Click(object sender, RoutedEventArgs e)
        {
            m_model.AddLogEntry(new CLogEntry("으아아아\n으이이잉\nㅁㄴㅇㄹ"));
        }
    }
}
