﻿using System;
using System.IO;
using System.Windows.Controls;
using NLog;
using RestSharp.Serializers;
using Sandbox.Game.GameSystems.BankingAndCurrency;
using Sandbox.Game.Multiplayer;
using Sandbox.Game.World;
using Torch;
using Torch.API;
using Torch.API.Managers;
using Torch.API.Plugins;
using Torch.API.Session;
using Torch.Session;

namespace Referrals_project
{
    public class ReferralCore : TorchPluginBase, IWpfPlugin 
    {
        public static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public static TorchSessionManager SessionManager;
        public static ReferralCore Instance { get; private set; }
        private ReferralControls _control;
        private Persistent<ReferralConfig> _config;
        public ReferralConfig Config => _config?.Data;
        
        public UserControl GetControl()
        {
            return _control ?? (_control = new ReferralControls(this));
        }
        
        public override void Init(ITorchBase torch)
        {
            base.Init(torch);
            Instance = this;
            SessionManager = Torch.Managers.GetManager<TorchSessionManager>();
            SessionManager.SessionStateChanged += SessionManagerOnSessionStateChanged;
            
        }
        
        private static void SessionManagerOnSessionStateChanged(ITorchSession session, TorchSessionState newstate)
        {
            switch (newstate)
            {
                case TorchSessionState.Loading:
                    break;
                case TorchSessionState.Loaded:
                    ;
                    break;
                case TorchSessionState.Unloading:
                    break;
                case TorchSessionState.Unloaded:
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newstate), newstate, null);
            }
        }
        
        private void SetupConfig()
        {
            var configFile = Path.Combine(StoragePath, "ReferralConfig.cfg");
            try
            {
                _config = Persistent<ReferralConfig>.Load(configFile);
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }

            if (_config?.Data != null) return;
            Log.Info("Creating Default Config");
            _config = new Persistent<ReferralConfig>(configFile, new ReferralConfig());
            _config.Save();
        }
        
        public static UserXML.User ReferralDataFromStorage()
        {
            var serializer = new XmlSerializer(typeof(UserXML.User));
            using (var reader = new StreamReader(Instance.StoragePath))
            {
                return (UserXML.User) serializer.Deserialize(reader);
            }
        }
        
        public static UserXML.User WriteReferralDataToStorage()
        {
            var serializer = new XmlSerializer(typeof(UserXML));
            using (var writer = new StreamWriter(Instance.StoragePath))
            {
                return (UserXML.User) serializer.Serialize(writer);
            }
        }

        public void Save()
        {
            _config.Save();
        }
    }
}