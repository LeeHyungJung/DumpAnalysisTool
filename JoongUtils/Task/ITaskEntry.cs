using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace JoongUtils.Task
{
    public interface ITaskEntry : ITask
    {
        void Push(object obj);
        object Pop();
        ITaskEntry Next();
    }
}
