using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using MicroMvvm;

using DumpAnalysisTool.Src.Define;
using DumpAnalysisTool.Src.Page;

namespace DumpAnalysisTool.Src.Model
{
    public class PageInfo : BasePageModel
    {
        BasePage m_curPage;

        public BasePage CurPage
        {
            get { return m_curPage; }
            set {

                if (m_curPage != null)
                {
                    m_curPage.onPageClose();
                }

                m_curPage = value;
                RaisePropertyChanged("CurPage");

                if (m_curPage != null)
                {
                    m_curPage.onPageOpen();
                }
            }
        }

        public PageInfo()
        {
            m_pageList = new Dictionary<ePAGE, BasePage>();

            m_pageSeq = new LinkedList<ePAGE>();
        }

        public void setPage(ePAGE page)
        {
            CurPage = m_pageList[page];
        }

        public void setPrePage()
        {
            try
            {
                if (CurPage != null)
                {
                    LinkedListNode<ePAGE> node = m_pageSeq.Find(CurPage.getPageEnum());

                    if (node != null || node.Previous != null)
                    {
                        setPage(node.Previous.Value);
                        return;
                    }
                    throw new Exception();
                }
                throw new Exception();
            }
            catch(Exception e)
            {
                e.ToString();
                setPage(ePAGE.ePage_StartPage);
            }
        }

        public void setNextPage()
        {
            try
            {
                if (CurPage != null)
                {
                    LinkedListNode<ePAGE> node = m_pageSeq.FindLast(CurPage.getPageEnum());

                    if (node != null || node.Next != null)
                    {
                        setPage(node.Next.Value);
                        return;
                    }
                    throw new Exception();
                }
                throw new Exception();
            }
            catch (Exception e)
            {
                e.ToString();
                setPage(ePAGE.ePage_StartPage);
            }
        }

        public void clearRelativePage()
        {
            m_pageSeq.Clear();
        }

        public void setPageSeq(LinkedList<ePAGE> pageList)
        {
            m_pageSeq.Clear();
            m_pageSeq = pageList;
        }

        public Dictionary<ePAGE, BasePage> m_pageList;
        
        LinkedList<ePAGE> m_pageSeq;
    }
}
