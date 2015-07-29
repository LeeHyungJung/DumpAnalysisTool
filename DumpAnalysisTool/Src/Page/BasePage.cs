using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DumpAnalysisTool.Src.Define;
using DumpAnalysisTool.Src.ViewModel;

namespace DumpAnalysisTool.Src.Page
{
    public partial class BasePage : System.Windows.Controls.Page, IPage
    {
        ePAGE m_curPage;

        public BasePage(ePAGE page)
        {
            m_curPage = page;
        }

        public ePAGE getPageEnum()
        {
            return m_curPage;
        }

        public virtual void onPageOpen()
        {
      
        }

        public virtual void onPageClose()
        {

        }

    }
}
