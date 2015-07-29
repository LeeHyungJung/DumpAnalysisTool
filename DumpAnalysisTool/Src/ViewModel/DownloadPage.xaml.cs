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

using DumpAnalysisTool.Src.Page;
using DumpAnalysisTool.Src.Model;
using DumpAnalysisTool.Src.Define;

namespace DumpAnalysisTool.Src.ViewModel
{
    /// <summary>
    /// DownloadPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DownloadPage : BasePage
    {
        DownloadPageModel m_model;

        public DownloadPage()
            : base(ePAGE.ePage_DownloadPage)
        {
            InitializeComponent();

            _init();

        }

        private void _init()
        {
            m_model = new DownloadPageModel();

            m_model.CountryList.Add(new ComboData("ErrorsPosted", "한국"));
            m_model.CountryList.Add(new ComboData("ErrorsPosted_cn", "중국"));
            m_model.CountryList.Add(new ComboData("ErrorsPosted_th", "태국"));
            m_model.CountryList.Add(new ComboData("ErrorsPosted_hk", "홍콩"));
            m_model.CountryList.Add(new ComboData("ErrorsPosted_sgp", "싱가폴"));
            m_model.CountryList.Add(new ComboData("ErrorsPosted_ina", "인도네시아"));

            m_model.DownloadDate = DateTime.Today.AddDays(-1);

            DataContext = m_model;

        }

        private void DownloadPath_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result.ToString() == "OK")
            {
                m_model.DOWNLOAD_PATH = dialog.SelectedPath;
            }
        }

        public override void onPageOpen()
        {
            base.onPageOpen();
        }
    }
}
