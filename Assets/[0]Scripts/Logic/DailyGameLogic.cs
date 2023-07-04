using Game.Data;
using System.Linq;
using UnityEngine;


namespace Yarigg
{
    internal sealed class DailyGameLogic : MonoBehaviour
    {
        private PlayerData _playerData;

        private void Start()
        {
            _playerData = PlayerData.CurrentData;

            var daysDelta = (System.DateTime.Today - System.DateTime.Parse(_playerData.lastCalculateDate)).TotalDays;
            while (daysDelta > 0)
            {
                NextDay();
                daysDelta--;
            }
        }

        private void OnDestroy()
        {
            _playerData.SaveData();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                NextDay();
            }
        }

        private void NextDay()
        {
            HandleDailyIncome();
            HandleDailyCleaning();
            HandleDailyTransactions();
        }

        private void HandleDailyIncome()
        {
            var accounts = _playerData.GetAccounts();

            foreach (var company in _playerData.GetCompanies())
            {
                var account = accounts.ElementAt(company.AccountNum);
                var incomeValue = company.Income.Value - company.Expense.Value;
                account.AccountBalance += incomeValue;
            }
        }

        private void HandleDailyCleaning()
        {
            var accounts = _playerData.GetAccounts();
            var cashAccount = accounts.ElementAt(1);

            foreach (var company in _playerData.GetCompanies())
            {
                var companyAccount = accounts.ElementAt(company.AccountNum);

                var cleaningMoneyAmount = company.Cleaning.Value;
                cashAccount.AccountBalance -= cleaningMoneyAmount;
                companyAccount.AccountBalance += cleaningMoneyAmount;
            }
        }

        private void HandleDailyTransactions()
        {
            foreach (var transaction in _playerData.GetTransactions())
            {
                var fromAccount = _playerData._accounts[transaction.ExpenseAccountNum.Value];
                var toAccount = _playerData._accounts[transaction.IncomeAccountNum.Value];

                fromAccount.AccountBalance.Value -= transaction.TransactionAmount.Value;
                toAccount.AccountBalance.Value += transaction.TransactionAmount.Value;
            }
        }
    }
}
