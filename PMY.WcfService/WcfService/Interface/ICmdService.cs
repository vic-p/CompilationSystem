using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfService.Interface
{
    [ServiceContract(CallbackContract =typeof(ICmdServiceCallback))]
    public interface ICmdService
    {
        [OperationContract(IsOneWay =true)]
        void ExecuteCMD(string command);
    }
}
