using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.IRemoteService
{
    public interface ICmdService
    {
        void ExecuteCMD(string command, Action<string> showCmdMessageAction);
    }
}
