﻿using System;
using NLog;
using Sandbox.Game.GameSystems.BankingAndCurrency;
using Sandbox.Game.Multiplayer;
using Sandbox.Game.World;

namespace Referrals_project
{
    public class FinancialService
    {
        public static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private static MyBankingSystem bank = MyBankingSystem.Static;
        
        public static long PlayerAccountBalance(long accountNumber)
        {
            MyAccountInfo account;
            return bank.TryGetAccountInfo(accountNumber, out account) ? account.Balance : 0;
        }
        

        public static bool GivePlayerCredits(long accountNumber, long playerCredit)
        {
            
            try
            {
                MyAccountInfo playerAccount;
                if (bank.TryGetAccountInfo(accountNumber, out playerAccount))
                {
                    if (playerCredit > 0)
                    {
                        if (MyBankingSystem.ChangeBalance(accountNumber, playerCredit))
                            return true;
                    }
                    else
                    {
                        if (playerAccount.Balance < playerCredit)
                        {
                            MyBankingSystem.ChangeBalance(accountNumber,  playerAccount.Balance);
                            playerAccount.Balance = 0;
                            return true;
                        }
                        if (MyBankingSystem.ChangeBalance(accountNumber, playerCredit))
                            return true;
                    }
                }
                
            }
            catch (Exception error)
            {
                Log.Error("failed to pay");
            }
            
            return false;
        }
    }

}