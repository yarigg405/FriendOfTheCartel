using System;
using System.Linq;
using Yrr.Utils;

namespace Game.Data
{
    [Serializable]
    public sealed class AccountModel
    {
        public ReactiveValue<string> AccountName = new ReactiveValue<string>("");
        public ReactiveFloat AccountBalance = new ReactiveFloat(0);
        public ReactiveFloat AccountIncome = new ReactiveFloat(0);

        public void RecalculateIncome()
        {
            var companies = PlayerData.CurrentData.GetCompanies();
            foreach (var company in companies)
            {
                var account = PlayerData.CurrentData._accounts[company.AccountNum];
                if (account == null) continue;

                if (account == this)
                {
                    AccountIncome.Value = company.Income.Value + company.Cleaning.Value - company.Expense.Value;
                    break;
                }
            }
        }
    }
}
