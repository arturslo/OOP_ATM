using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace OOP_ATM
{
    [Serializable]
    public class AppState
    {
        private static string SERIALIZED_FILE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "appState.txt");
        public ATM atm;
        public List<BankAccount> accounts;

        public AppState()
        {
            var appState = LoadState();
            if (appState != null)
            {
                atm = appState.atm;
                accounts = appState.accounts;
            }
            else
            {
                atm = new ATM(5000);
                accounts = new List<BankAccount>
                {
                    new BankAccount(0),
                    new BankAccount(4000),
                    new BankAccount(1500)
                };
            }


            foreach (var account in accounts)
            {
                account.MoneyWithdrawnEvent += OnMoneyWithdrawn;
            }
        }

        public AppState LoadState()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(SERIALIZED_FILE_PATH, FileMode.Open, FileAccess.Read))
                {
                    var appState = (AppState)formatter.Deserialize(stream);

                    return appState;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Can not read file");
                // AppState? not allowed, why can I return null?
                return null;
            }
        }

        public void SaveState()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(SERIALIZED_FILE_PATH, FileMode.Create, FileAccess.Write))
                {
                    formatter.Serialize(stream, this);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Can not save file");
            }
        }

        public void OnMoneyWithdrawn(object source, EventArgs eventArgs)
        {
            Console.WriteLine("Saving app state");
            SaveState();
        }
    }
}
