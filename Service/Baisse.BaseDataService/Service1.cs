using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Baisse.BaseDataService
{
    public partial class Service1 : ServiceBase
    {
        private BaseDataServiceController _baseDataServiceController;
        BaseDataServiceController BaseDataServiceController
        {
            get
            {
                if (_baseDataServiceController == null)
                    _baseDataServiceController = new BaseDataServiceController();
                return _baseDataServiceController;
            }
            set { _baseDataServiceController = value; }
        }

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            BaseDataServiceController.Start();
        }

        protected override void OnStop()
        {
            BaseDataServiceController.Stop();
        }
    }
}
