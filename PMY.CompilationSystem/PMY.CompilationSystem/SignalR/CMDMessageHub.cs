using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace PMY.CompilationSystem.SignalR
{
    public class CMDMessageHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string message)
        {
            //声明一个addNewMessageToPage的接口，可以在客户端（前端页面）实现该接口，在需要接收信息的地方把message内容显示出来
            Clients.All.addNewMessageToPage(message);
        }
    }
}