using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DumpAnalysisTool.Src.Define;

namespace DumpAnalysisTool.Src.Page
{
    interface IPage
    {
        ePAGE getPageEnum();
        void onPageOpen();
        void onPageClose();
    }
}
