using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace JoongUtils.Task
{
    public delegate void DelegateTaskStart(string taskName);
    public delegate void DelegateTaskEnd(object sender, RunWorkerCompletedEventArgs e);
    public delegate void DelegateTaskProgressChanged(object sender, ProgressChangedEventArgs e);
    public delegate object DelegateGetInputDesc();
    public delegate void DelegateTaskOutputPush(object output);

    public interface ITask
    {
        event DelegateTaskStart                 TaskStart;
        event DelegateTaskEnd                   TaskEnd;
        event DelegateTaskProgressChanged       TaskProgressChange;
        event DelegateGetInputDesc              GetInputDesc;
        event DelegateTaskOutputPush            TaskOutputPush;
        
        bool IsBusy();
        void Run();
        void Cancle();
    }
}
