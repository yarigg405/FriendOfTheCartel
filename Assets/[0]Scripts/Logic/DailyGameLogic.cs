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
            var accounts = _playerData.GetAccounts();

            foreach (var company in _playerData.GetCompanies())
            {
                var incomeAcc = accounts.ElementAt(company.IncomeAccountNum);
                incomeAcc.AccountBalance += company.Income;

                var expensesAcc = accounts.ElementAt(company.ExpenseAccountNum);
                expensesAcc.AccountBalance -= company.Expense;
            }
        }
    }
}
