using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DumpAnalysisTool.Src.Page;
using DumpAnalysisTool.Src.Define;

namespace DumpAnalysisTool.Src.StaticManager
{
    static class PageManager
    {
        static IMainView m_mainView;

        static PageManager()
        {
            m_mainView = null;
        }

        public static void setPageListener(IMainView listener)
        {
            m_mainView = listener;
        }

        public static void setPageChange(ePAGE page)
        {
            m_mainView.setChangePageRequest(page);
        }

        public static void setPageSeq(LinkedList<ePAGE> pageList)
        {
            m_mainView.setPageSeq(pageList);
        }

        public static void Clear()
        {
            m_mainView = null;
        }

        public static void setPrevPage()
        {
            m_mainView.setPrevPage();
        }
 
        public static void setNextPage()
        {
            m_mainView.setNextPage();
        }
    }
}
