namespace OOP_ATM
{
    class App
    {
        private AppState _appState;

        public App()
        {
            _appState = new AppState();
        }

        public void Run()
        {
            _appState.accounts[0].WithdrawMoney(_appState.atm, 500);
            _appState.accounts[1].WithdrawMoney(_appState.atm, 500);
            _appState.accounts[2].WithdrawMoney(_appState.atm, 500);
        }
    }
}
