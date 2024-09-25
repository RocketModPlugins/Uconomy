using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace fr34kyn01535.Uconomy.Utils
{
    public static class ChatManagerUtils
    {
        public static void SendMessage(string message)
        {
            ChatManager.serverSendMessage(message.Replace('(', '<').Replace(')', '>'), Color.white, null, null, EChatMode.GLOBAL, Uconomy.Instance.Configuration.Instance.Image, true);
        }


        public static void SendMessageToPlayer(IRocketPlayer caller, string message)
        {

            if (caller is ConsolePlayer)
            {
                UnturnedChat.Say(caller, message,
                   UnturnedChat.GetColorFromName(Uconomy.MessageColor, Color.green));
            }
            else
            {
                UnturnedPlayer player = caller as UnturnedPlayer;
                ChatManager.serverSendMessage(message.Replace('(', '<').Replace(')', '>'), Color.white, null, player.SteamPlayer(), EChatMode.GLOBAL, Uconomy.Instance.Configuration.Instance.Image, true);
            }


        }



        public static void SendMessageToPlayer(UnturnedPlayer player, string message)
        {
            ChatManager.serverSendMessage(message.Replace('(', '<').Replace(')', '>'), Color.white, null, player.SteamPlayer(), EChatMode.GLOBAL, Uconomy.Instance.Configuration.Instance.Image, true);
        }
    }
}
