using System;

namespace OOP_ATM
{
    [Serializable]
    public class BankAccount
    {
        private int _balance;
        private string _guid;

        public delegate void MoneyWithdrawnEventHandler(object source, EventArgs eventArgs);
        [field:NonSerialized]
        public event MoneyWithdrawnEventHandler MoneyWithdrawnEvent;

        public string Guid
        {
            get { return _guid; }
        }

        public int Balance
        {
            get { return _balance; }
        }

        public BankAccount()
        {
            _guid = System.Guid.NewGuid().ToString();
            _balance = 0;
        }

        public BankAccount(int balance) : this()
        {
            this._balance = balance;
        }

        public override string ToString()
        {
            return $"class: {GetType().ToString()} guid: {_guid} balance: {_balance.ToString()}";
        }

        private bool CanWithdraw(int withdrawAmount)
        {
            var moneyAfterWithdraw = _balance - withdrawAmount;

            return moneyAfterWithdraw >= 0;
        }

        public void WithdrawMoney(ATM atm, int withdrawAmount)
        {
            Console.WriteLine($"acc: {this} atm: {atm} Trying to withdraw {withdrawAmount.ToString()}");

            if (!CanWithdraw(withdrawAmount))
            {
                Console.WriteLine("Insufficient money in account!");
                return;
            }
            if (!atm.CanWithdraw(withdrawAmount))
            {
                Console.WriteLine("Insufficient money in ATM!");
                return;
            }

            _balance -= withdrawAmount;
            atm.WithdrawMoney(this, withdrawAmount);
            Console.WriteLine($"acc: {this} atm: {atm} Withdrawn {withdrawAmount.ToString()}");

            OnMoneyWithdrawn();
        }

        protected virtual void OnMoneyWithdrawn()
        {
            if (MoneyWithdrawnEvent != null)
            {
                MoneyWithdrawnEvent(this, EventArgs.Empty);
            }
        }
    }
}
