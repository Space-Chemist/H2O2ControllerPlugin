﻿using NLog;
using Sandbox.Game.Multiplayer;
using Torch.Commands;
using Torch.Commands.Permissions;
using VRage.Game.ModAPI;

namespace Referrals_project
{
    [Category("referral")]
    public class ReferralCommands : CommandModule
    {
        private static readonly Logger Log = LogManager.GetLogger("koth");

        public Referrals_project.ReferralCore Plugin
        {
            get
            {
                return (Referrals_project.ReferralCore)this.Context.Plugin;
            }
        }

        [Command("Examlpe command", "Example Description", "help text")]
        [Permission(MyPromoteLevel.Admin)]
        public void Example()
        {
            this.Context.Respond("response string");
                
        }


        [Command("Test", "Adds money to player. Needed SteamID.", null)]
        [Permission(MyPromoteLevel.None)]
        public void GiveCredits(ulong steamId, long amount)
        {
            long identityId = Sync.Players.TryGetIdentityId(steamId, 0);
            if (identityId == 0)
                this.Context.Respond("Fuck", (string) null, (string) null);
            else if (FinancialService.GivePlayerCredits(identityId, amount))
                this.Context.Respond("test worked money added fuck yeah");
           
        }
        
        [Command("yes", "yes", "yes")]
        [Permission(MyPromoteLevel.None)]
        public void yes()
        {
            Context.Respond("response string");
            Context.Respond(Context.Player.Identity.IdentityId.ToString() + FinancialService.GivePlayerCredits(Context.Player.Identity.IdentityId, 500000L));
        }
    }
}