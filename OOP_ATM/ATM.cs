using System;

namespace OOP_ATM
{
    [Serializable]
    public class ATM
    {
        private int _moneyAmount;
        private string _guid;

        public string Guid
        {
            get { return _guid; }
        }

        public int MoneyAmount
        {
            get { return _moneyAmount; }
        }

        public ATM()
        {
            _guid = System.Guid.NewGuid().ToString();
            _moneyAmount = 0;
        }

        public ATM(int moneyAmount) : this()
        {
            this._moneyAmount = moneyAmount;
        }

        public override string ToString()
        {
            return $"class: {GetType().ToString()} guid: {_guid} moneyAmount: {_moneyAmount.ToString()}";
        }

        public bool CanWithdraw(int withdrawAmount)
        {
            var moneyAfterWithdraw = _moneyAmount - withdrawAmount;

            return moneyAfterWithdraw >= 0;
        }

        public void WithdrawMoney(BankAccount account, int withdrawAmount)
        {
            if (!CanWithdraw(withdrawAmount))
            {
                Console.WriteLine("Insufficient money in ATM!");
                return;
            }

            _moneyAmount -= withdrawAmount;
        }
    }
}
