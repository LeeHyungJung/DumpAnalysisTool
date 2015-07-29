using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpAnalysisTool.Src.Model
{
    class ComboData
    {
        string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public ComboData(string id, string data)
        {
            Id = id;
            Value = data;
        }
    }
}
