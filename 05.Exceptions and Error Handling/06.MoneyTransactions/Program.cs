using System;
using System.Collections.Generic;

namespace _06.MoneyTransactions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, double> accounts = new Dictionary<int, double>();

            ReadAccounts(accounts);

            string command;

            while ((command = Console.ReadLine()) != "End")
            {
                ProcessCommand(accounts, command);
            }
        }

        private static void ReadAccounts(Dictionary<int, double> accounts)
        {
            string[] accInput = Console.ReadLine().Split(",");

            foreach (var item in accInput)
            {
                string[] accInfo = item.Split("-");
                int accountNumber = int.Parse(accInfo[0]);
                double balance = double.Parse(accInfo[1]);

                accounts.Add(accountNumber, balance);
            }
        }

        private static void ProcessCommand(Dictionary<int, double> accounts, string command)
        {
            string[] cmd = command.Split();

            try
            {
                CheckInputData(cmd, accounts);

                string action = cmd[0];
                int account = int.Parse(cmd[1]);
                double amount = double.Parse(cmd[2]);

                if (action == "Deposit")
                {
                    accounts[account] += amount;
                }
                else
                {
                    accounts[account] -= amount;
                }

                Console.WriteLine($"Account {account} has new balance: {accounts[account]:f2}");
            }
            catch (InvalidOperationException ioe)
            {
                Console.WriteLine(ioe.Message);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            finally
            {
                Console.WriteLine("Enter another command");
            }
        }

        private static void CheckInputData(string[] cmd, Dictionary<int, double> accounts)
        {
            if (cmd[0] != "Deposit" && cmd[0] != "Withdraw"
                || !int.TryParse(cmd[1], out int accountNumber)
                || !double.TryParse(cmd[2], out double amount))
            {
                throw new InvalidOperationException("Invalid command!");
            }

            if (!accounts.ContainsKey(accountNumber))
            {
                throw new ArgumentException("Invalid account!");
            }

            if (cmd[0] == "Withdraw" && amount > accounts[accountNumber])
            {
                throw new InvalidOperationException("Insufficient balance!");
            }
        }
    }
}
