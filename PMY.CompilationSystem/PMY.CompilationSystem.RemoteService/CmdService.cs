using PMY.CompilationSystem.IRemoteService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.RemoteService
{
    public class CmdService : ICmdService
    {
        public void ExecuteCMD(string command, Action<string> showCmdMessageAction)
        {
            WcfServiceReference.CmdServiceClient client = null;
            try
            {
                WcfServiceReference.ICmdServiceCallback callback = new CmdServiceCallback() { showCmdMessageAction = showCmdMessageAction };
                System.ServiceModel.InstanceContext context = new System.ServiceModel.InstanceContext(callback);
                client = new WcfServiceReference.CmdServiceClient(context);
                client.ExecuteCMD(command);
                //client.Close();//会关闭链接，但是如果网络异常了，会抛出异常而且关闭不了
            }
            catch (Exception ex)
            {
                if (client != null)
                    client.Abort();
                throw ex;
            }
        }
    }
}
