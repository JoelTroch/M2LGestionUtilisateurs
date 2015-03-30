using System;
using System.ServiceProcess;
using System.Text;

namespace ServiceSynchronisation
{
    public partial class ServiceMain : ServiceBase
    {
        public ServiceMain()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            base.Stop();
        }
    }
}
