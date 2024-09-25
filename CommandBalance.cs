using System.Collections.Generic;
using fr34kyn01535.Uconomy.Utils;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using UnityEngine;

namespace fr34kyn01535.Uconomy
{
    public class CommandBalance : IRocketCommand
    {
        public string Name => "balance";

        public string Help => "Shows the current balance";


        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Syntax => "(player)";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> {"uconomy.balance"};

        public void Execute(IRocketPlayer caller, params string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (command.Length == 1)
            {
                if (caller.HasPermission("balance.check"))
                {
                    var target = UnturnedPlayer.FromName(command[0]);
                    if (target != null)
                    {
                        var balance = Uconomy.Instance.Database.GetBalance(target.Id);

                        ChatManagerUtils.SendMessageToPlayer(player,
                            Uconomy.Instance.Translate("command_balance_show_otherPlayer",
                            Uconomy.Instance.Configuration.Instance.MoneySymbol, balance,
                                Uconomy.Instance.Configuration.Instance.MoneyName));

                    }
                    else
                    {
                        ChatManagerUtils.SendMessageToPlayer(player, Uconomy.Instance.Translate("command_balance_error_player_not_found"));
                    }
                }
                else
                {
                    ChatManagerUtils.SendMessageToPlayer(player, Uconomy.Instance.Translate("command_balance_check_noPermissions"));
                }
            }
            else
            {
                var balance = Uconomy.Instance.Database.GetBalance(caller.Id);

  

                ChatManagerUtils.SendMessageToPlayer(player, Uconomy.Instance.Translations.Instance.Translate("command_balance_show", Uconomy.Instance.Configuration.Instance.MoneySymbol, balance,
                        Uconomy.Instance.Configuration.Instance.MoneyName));

                ChatManagerUtils.SendMessageToPlayer(player,
                    Uconomy.Instance.Translations.Instance.Translate("command_balance_show",
                        Uconomy.Instance.Configuration.Instance.MoneySymbol, balance,
                        Uconomy.Instance.Configuration.Instance.MoneyName));
            }
        }
    }
}