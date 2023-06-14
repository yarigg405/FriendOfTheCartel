using System;
using Yrr.Utils;


namespace Game.Data
{
    [Serializable]
    public sealed class CompanyModel
    {
        public ReactiveValue<string> CompanyName;
        public ReactiveValue<string> CompanyDescription;
        public ReactiveFloat Income;
        public ReactiveFloat Expense;

        public int IncomeAccountNum;
        public int ExpenseAccountNum;

        public CompanyModel()
        {
            CompanyName = new ReactiveValue<string>("");
            CompanyDescription = new ReactiveValue<string>("");
            Income = new ReactiveFloat(0);
            Expense = new ReactiveFloat(0);
        }
    }
}
