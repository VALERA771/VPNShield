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
        
        internal const string lastModifed = "2023/04/02 14:47 UTC";


        public override void OnEnabled()
        {
            Log.Info($"{Name} v{Version} by {Author}. Last modified: {lastModifed}.");

            Log.Info("Loading base scripts.");
            Account = new Account(this);
            VPN = new VPN(this);
            WebhookHandler = new WebhookHandler(this);

            if (Config.CheckForUpdates)
                _ = UpdateCheck.CheckForUpdate();

            Log.Info("Checking file system.");

            if (!Directory.Exists(Path.Combine(Paths.Exiled, "VPNShield")))
            {
                Log.Warn($"{Paths.Exiled}/VPNShield directory does not exist. Creating.");
                Directory.CreateDirectory(Path.Combine(Paths.Exiled, "VPNShield"));
            }

            Log.Info($"File system check complete.\nWorking directory is: {Path.Combine(Paths.Exiled, "VPNShield")}.\nDatabase path is: {DbManager.databaseLocation}.");

            Log.Info("Registering Event Handlers.");

            EventHandlers = new EventHandlers(this);
            PlayerEvents.PreAuthenticating += EventHandlers.PreAuthenticating;
            PlayerEvents.Verified += EventHandlers.Verified;
            ServerEvents.WaitingForPlayers += EventHandlers.WaitingForPlayers;

            Log.Info("Done.");
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
