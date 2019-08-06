using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WcfHostOfWindowsService
{
    partial class WcfServiceHost : ServiceBase
    {
        private ServiceHost svrHost = null; //寄宿服务对象 
        PMY.Common.Logger logger = PMY.Common.Logger.CreateLogger(typeof(WcfServiceHost));
        public WcfServiceHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            try
            {
                svrHost = new ServiceHost(typeof(WcfService.Service.CmdService));
                if (svrHost.State != CommunicationState.Opened)
                {
                    svrHost.Open();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            logger.Info(DateTime.Now.ToShortTimeString() + "已成功调用了服务一次。");
            logger.Info("已成功启动。");
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            if (svrHost != null)
            {
                svrHost.Close();
                svrHost = null;

            }
            logger.Info(DateTime.Now.ToShortTimeString() + "已关闭服务。");
        }
    }
}
