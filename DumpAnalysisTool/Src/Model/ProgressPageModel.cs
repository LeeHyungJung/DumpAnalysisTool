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
using System.Collections.ObjectModel;

namespace DumpAnalysisTool.Src.Model
{
    class ProgressPageModel : BasePageModel
    {
        ObservableCollection<CLogEntry> logEntry;

        public ObservableCollection<CLogEntry> LogEntry
        {
            get { return logEntry; }
            set { 
                logEntry = value;
                RaisePropertyChanged("LogEntry");
            }
        }

        public ProgressPageModel()
        {
            logEntry = new ObservableCollection<CLogEntry>();
        }

        public void AddLogEntry(CLogEntry log)
        {
            LogEntry.Insert(0,log);
            RaisePropertyChanged("LogEntry");
        }

        double minimum;

        public double Minimum
        {
            get { return minimum; }
            set { 
                minimum = value;
                RaisePropertyChanged("Minimum");
            }
        }

        double maximum;

        public double Maximum
        {
            get { return maximum; }
            set { 
                maximum = value;
                RaisePropertyChanged("Maximum");
            }
        }

        double progress;

        public double Progress
        {
            get { return progress; }
            set { 
                progress = value;
                RaisePropertyChanged("Progress");
            }
        }
        
    }

    public class CLogEntry
    {
        public string strDateTime { get; set; }

        public string Message { get; set; }

        public CLogEntry(string msg)
        {
            strDateTime = DateTime.Now.ToString("h:mm:ss:ff");

            Message = msg; 
        }
    }
}
