using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfService.Service
{
    public class CmdService : WcfService.Interface.ICmdService
    {
        public void ExecuteCMD(string command)
        {
            //获得由客户端传入的回调终结点的引用，使用这个引用可以调用客户端方法
            WcfService.Interface.ICmdServiceCallback callback = OperationContext.Current.GetCallbackChannel<WcfService.Interface.ICmdServiceCallback>();
            PMY.Common.ExecuteCMDHelper.ExecuteCMD(command,(s,e)=> { callback.ShowCmdMessage(e.Data); });
        }
    }
}
