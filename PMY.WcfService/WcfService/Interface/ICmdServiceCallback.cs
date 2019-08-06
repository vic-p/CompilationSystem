using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfService.Interface
{
    interface ICmdServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void ShowCmdMessage(string message);
    }
}
