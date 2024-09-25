using System.Collections.Generic;
using fr34kyn01535.Uconomy.Utils;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace fr34kyn01535.Uconomy
{
    public class CommandPay : IRocketCommand
    {
        public string Help => "Pays a specific player money from your account";

        public string Name => "pay";

        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Syntax => "<player> <amount>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> {"uconomy.pay"};

        public void Execute(IRocketPlayer caller, params string[] command)
        {
            if (command.Length != 2)
            {

                ChatManagerUtils.SendMessageToPlayer(caller, Uconomy.Instance.Translations.Instance.Translate("command_pay_invalid"));


                return;
            }

            var otherPlayer = command.GetCSteamIDParameter(0)?.ToString();
            var otherPlayerOnline = UnturnedPlayer.FromName(command[0]);
            if (otherPlayerOnline != null) otherPlayer = otherPlayerOnline.Id;

            if (otherPlayer != null)
            {
                if (caller.Id == otherPlayer)
                {
                    ChatManagerUtils.SendMessageToPlayer(caller,
                       Uconomy.Instance.Translations.Instance.Translate("command_pay_error_pay_self"));
                    return;
                }

                if (!decimal.TryParse(command[1], out var amount) || amount <= 0)
                {
                    ChatManagerUtils.SendMessageToPlayer(caller,
                       Uconomy.Instance.Translations.Instance.Translate("command_pay_error_invalid_amount"));
                    return;
                }

                if (caller is ConsolePlayer)
                {
                    Uconomy.Instance.Database.IncreaseBalance(otherPlayer, amount);
                    if (otherPlayerOnline != null)
                        ChatManagerUtils.SendMessageToPlayer(caller,
                           Uconomy.Instance.Translations.Instance.Translate("command_pay_console", amount,
                                Uconomy.Instance.Configuration.Instance.MoneyName));

                }
                else
                {
                    var myBalance = Uconomy.Instance.Database.GetBalance(caller.Id);
                    if (myBalance - amount <= 0)
                    {
                        ChatManagerUtils.SendMessageToPlayer(caller,
                           Uconomy.Instance.Translations.Instance.Translate("command_pay_error_cant_afford"));
                    }
                    else
                    {
                        Uconomy.Instance.Database.IncreaseBalance(caller.Id, -amount);
                        if (otherPlayerOnline != null)
                            ChatManagerUtils.SendMessageToPlayer(caller,
                                Uconomy.Instance.Translations.Instance.Translate("command_pay_private",
                                    otherPlayerOnline.CharacterName, amount,
                                    Uconomy.Instance.Configuration.Instance.MoneyName));
                        else
                            ChatManagerUtils.SendMessageToPlayer(caller,
                                Uconomy.Instance.Translations.Instance.Translate("command_pay_private", otherPlayer,
                                    amount, Uconomy.Instance.Configuration.Instance.MoneyName));

                        Uconomy.Instance.Database.IncreaseBalance(otherPlayer, amount);
                        if (otherPlayerOnline != null)
                            ChatManagerUtils.SendMessageToPlayer(caller,
                                Uconomy.Instance.Translations.Instance.Translate("command_pay_other_private", amount,
                                    Uconomy.Instance.Configuration.Instance.MoneyName, caller.DisplayName));

                        Uconomy.Instance.HasBeenPayed((UnturnedPlayer) caller, otherPlayer, amount);
                    }
                }
            }
            else
            {
                ChatManagerUtils.SendMessageToPlayer(caller,
                    Uconomy.Instance.Translations.Instance.Translate("command_pay_error_player_not_found"));
            }
        }
    }
}