using Exiled.API.Features;
using System;
using System.IO;
using System.Reflection;
using VPNShield.API;
using VPNShield.Handlers;
using PlayerEvents = Exiled.Events.Handlers.Player;
using ServerEvents = Exiled.Events.Handlers.Server;

namespace VPNShield
{
    public class Plugin : Plugin<Config>
    {
        public EventHandlers EventHandlers;
        public Account Account;
        public VPN VPN;
        public WebhookHandler WebhookHandler;

        public override string Name { get; } = "VPNShield EXILED Edition";
        public override string Author { get; } = "SomewhatSane & VALERA771";
        public override string Prefix { get; } = "vs";
        public override Version Version { get; } = version;
        public override Version RequiredExiledVersion { get; } = new("6.0.0");

        public static Version version => Assembly.GetExecutingAssembly().GetName().Version;
        
        internal const string lastModifed = "2023/05/08 09:43 UTC";


        public override void OnEnabled()
        {
            Log.Debug($"Last modified: {lastModifed}.");

            Log.Debug("Loading base scripts.");
            Account = new Account(this);
            VPN = new VPN(this);
            WebhookHandler = new WebhookHandler(this);

            Log.Debug("Checking file system.");

            if (!Directory.Exists(Path.Combine(Paths.Exiled, "VPNShield")))
            {
                Log.Debug($"{Paths.Exiled}/VPNShield directory does not exist. Creating.");
                Directory.CreateDirectory(Path.Combine(Paths.Exiled, "VPNShield"));
            }

            if (!File.Exists(DbManager.databaseLocation))
            {
                Log.Debug($"{DbManager.databaseLocation} file does not exist.Creating.");
                File.Create(DbManager.databaseLocation).Close();
            }

            Log.Debug($"File system check complete.\nWorking directory is: {Path.Combine(Paths.Exiled, "VPNShield")}.\nDatabase path is: {DbManager.databaseLocation}.");

            Log.Debug("Registering Event Handlers.");

            EventHandlers = new EventHandlers(this);
            PlayerEvents.PreAuthenticating += EventHandlers.PreAuthenticating;
            PlayerEvents.Verified += EventHandlers.Verified;
            ServerEvents.WaitingForPlayers += EventHandlers.WaitingForPlayers;

            Log.Debug("");
        }

        public override void OnDisabled()
        {
            if (!Config.IsEnabled) return;

            PlayerEvents.PreAuthenticating -= EventHandlers.PreAuthenticating;
            PlayerEvents.Verified -= EventHandlers.Verified;
            ServerEvents.WaitingForPlayers -= EventHandlers.WaitingForPlayers;

            EventHandlers = null;
            Account = null;
            VPN = null;

            Log.Info("Disabled.");
        }
    }
}
