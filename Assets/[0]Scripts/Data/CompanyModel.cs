using System;
using Yrr.Utils;


namespace Game.Data
{
    [Serializable]
    public sealed class CompanyModel
    {
        public ReactiveValue<string> CompanyName = new ReactiveValue<string>("");
        public ReactiveValue<string> CompanyDescription = new ReactiveValue<string>("");

        public ReactiveFloat CompanyCost = new ReactiveFloat(0);
        public ReactiveFloat Income = new ReactiveFloat(0);
        public ReactiveFloat Expense = new ReactiveFloat(0);
        public ReactiveFloat Cleaning = new ReactiveFloat(0);

        public int AccountNum;
    }
}
