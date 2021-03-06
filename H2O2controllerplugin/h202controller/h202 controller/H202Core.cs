using System;
using System.IO;
using System.Windows.Controls;
using NLog;
using Torch;
using Torch.API;
using Torch.API.Plugins;
using Torch.Session;

namespace h202_controller
{
    public class H202Core : TorchPluginBase, IWpfPlugin
    {
        public static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private Persistent<H2O2Config> _config;
        private H2O2UIControl _control;
        public static H202Core Instance { get; private set; }
        public H2O2Config Config => _config?.Data;
        
        public override void Init(ITorchBase torch)
        {
            base.Init(torch);
            SetupConfig();
            Instance = this;
        }

        public UserControl GetControl()
        {
            return _control ?? (_control = new H2O2UIControl(this));
        }
        
        private void SetupConfig()
        {
            var configFile = Path.Combine(StoragePath, "H2O2ControllerConfig.cfg");
            try
            {
                _config = Persistent<H2O2Config>.Load(configFile);
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }

            if (_config?.Data != null) return;
            Log.Info("Creating Default Config");
            _config = new Persistent<H2O2Config>(configFile, new H2O2Config());
            _config.Save();
        }
    }
}