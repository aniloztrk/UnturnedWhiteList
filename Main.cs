using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core.Commands;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace MixyWhitelister
{
    public class Main : RocketPlugin<Config>
    {
        protected override void Load()
        {
            U.Events.OnPlayerConnected += Connect;
        }
        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= Connect;
        }
        public void Connect(UnturnedPlayer player)
        {
            if (!Configuration.Instance.SteamIDList.Contains(player.CSteamID.ToString()))
            {
                player.Player.enablePluginWidgetFlag(EPluginWidgetFlags.Modal);
            }
            else
            {
                return;
            }
        }
        [RocketCommand("whitelistadd", "")]
        [RocketCommandPermission("mixy.whitelist")]
        public void WhiteListAdd(IRocketPlayer caller, string[] command)
        {
            var player = caller as UnturnedPlayer;
            var target = UnturnedPlayer.FromName(command[0]);
            if (target != null)
            {
                if (!Configuration.Instance.SteamIDList.Contains(target.CSteamID.ToString()))
                {
                    Configuration.Instance.SteamIDList.Add(target.CSteamID.ToString());
                    Configuration.Save();
                    target.Player.disablePluginWidgetFlag(EPluginWidgetFlags.Modal);
                    UnturnedChat.Say(target, Translate("YouAddWhitelist"), Color.green);
                }
                else
                {
                    UnturnedChat.Say(player, "Player is already on the whitelist.", Color.red);
                }
            }
            else
            {
                UnturnedChat.Say(player, "Player not found", Color.red);
            }
        }
        [RocketCommand("whitelistremove", "")]
        [RocketCommandPermission("mixy.whitelist")]
        public void WhiteListRemove(IRocketPlayer caller, string[] command)
        {
            var player = caller as UnturnedPlayer;
            var target = UnturnedPlayer.FromName(command[0]);
            if (target != null)
            {
                if (Configuration.Instance.SteamIDList.Contains(target.CSteamID.ToString()))
                {
                    Configuration.Instance.SteamIDList.Remove(target.CSteamID.ToString());
                     Configuration.Save();
                    target.Kick(Translate("YouRemoveWhitelist"));
                }
                else
                {
                    UnturnedChat.Say(player, "Player is bot already on the whitelist.", Color.red);
                }
            }
            else
            {
                UnturnedChat.Say(player, "Player not found", Color.red);
            }
        }
        public override TranslationList DefaultTranslations => new TranslationList
        {
            { "YouAddWhitelist", "Whiteliste eklendin." },
            { "YouRemoveWhitelist", "Whitelistten atıldın." }
        };
    }
}
