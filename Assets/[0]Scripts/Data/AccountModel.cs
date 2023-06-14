using System;
using Yrr.Utils;

namespace Game.Data
{
    [Serializable]
    public sealed class AccountModel
    {
        public ReactiveValue<string> AccountName;
        public ReactiveFloat AccountBalance = new ReactiveFloat(0);

        public AccountModel()
        {
            AccountName = new ReactiveValue<string>("");
            AccountBalance = new ReactiveFloat(0);
        }
    }
}
