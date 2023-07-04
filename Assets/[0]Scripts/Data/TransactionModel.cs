using System;
using System.Collections.Generic;
using UnityEngine;
using Yrr.Utils;

namespace Game.Data
{
    [Serializable]
    public sealed class TransactionModel
    {
        public ReactiveInt IncomeAccountNum = new ReactiveInt(-1);
        public ReactiveInt ExpenseAccountNum = new ReactiveInt(-1);

        public ReactiveFloat TransactionAmount = new ReactiveFloat(0);
    }
}
