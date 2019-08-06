using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.RemoteService
{
    public class CmdServiceCallback:WcfServiceReference.ICmdServiceCallback
    {
        public Action<string> showCmdMessageAction = null;
        public void ShowCmdMessage(string message)
        {
            showCmdMessageAction?.Invoke(message);
        }

    }
}
