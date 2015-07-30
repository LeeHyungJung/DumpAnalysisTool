using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoongUtils.Worker
{
    interface ITask
    {
        public bool IsBusy();
        public void Run();
        public void Cancle();
    }
}
