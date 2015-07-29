using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

namespace DumpAnalysisTool.Src.Model
{
    class DownloadPageModel : BasePageModel
    {
        DateTime downloadDate;

        public System.DateTime DownloadDate
        {
            get { return downloadDate; }
            set { 
                downloadDate = value;
                RaisePropertyChanged("DownloadDate");
            }
        }

        List<ComboData> countryList;

        public List<ComboData> CountryList
        {
            get { return countryList; }
            set { 
                countryList = value;
                RaisePropertyChanged("CountryList");            
            }
        }

        string selectedCountry;

        public string SelectedCountry
        {
            get { 
                return selectedCountry;
            }
            set { 
                selectedCountry = value;
                RaisePropertyChanged("SelectedCountry");            
            }
        }

        string server_address;

        public string SERVER_ADDRESS
        {
            get { return server_address; }
            set {
                server_address = value;
                RaisePropertyChanged("SERVER_ADDRESS");            
            }
        }

        string server_id;

        public string SERVER_ID
        {
            get { return server_id; }
            set { 
                server_id = value;
                RaisePropertyChanged("SERVER_ID");            
            }
        }

        string server_password;

        public string SERVER_PASSWORD
        {
            get { return server_password; }
            set
            {
                server_password = value;
                RaisePropertyChanged("SERVER_PASSWORD");
            }
        }

        string download_path;

        public string DOWNLOAD_PATH
        {
            get { return download_path; }
            set
            {
                download_path = value;
                RaisePropertyChanged("DOWNLOAD_PATH");
            }
        }

        public DownloadPageModel()
        {
            CountryList = new List<ComboData>();
            SelectedCountry = "ErrorsPosted";
            SERVER_ADDRESS = "127.0.0.1";
            SERVER_ID = "";
            SERVER_PASSWORD = "";
            DOWNLOAD_PATH = "";
        }

    }
}
