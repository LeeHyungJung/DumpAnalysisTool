using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DumpAnalysisTool.Src.Define;

namespace DumpAnalysisTool.Src.Page
{
    public interface IMainView
    {
        void setChangePageRequest(ePAGE page);
        void setNextPage();
        void setPrevPage();
        void setPageSeq(LinkedList<ePAGE> pageList);
    }
}
