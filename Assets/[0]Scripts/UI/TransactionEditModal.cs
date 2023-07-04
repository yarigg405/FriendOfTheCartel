using Game.Data;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yrr.UI;
using Yrr.Utils;


namespace Game.UI
{
    internal sealed class TransactionEditModal : UIScreen
    {
        [SerializeField] private TMP_Dropdown accountFromDd;
        [SerializeField] private TMP_Dropdown accountToDd;
        [SerializeField] private TMP_InputField transactionSumIf;


        [SerializeField] private Button deleteBtn;
        private TransactionModel _transaction;

        protected override void OnShow(object args)
        {
            accountFromDd.ClearOptions();
            accountToDd.ClearOptions();
            foreach (var acc in PlayerData.CurrentData.GetAccounts())
            {
                accountFromDd.options.Add(new TMP_Dropdown.OptionData(acc.AccountName.Value));
                accountToDd.options.Add(new TMP_Dropdown.OptionData(acc.AccountName.Value));
            }


            if (args != null)
            {
                if (args is TransactionModel transaction)
                {
                    _transaction = transaction;
                    deleteBtn.interactable = true;

                    accountFromDd.value = _transaction.ExpenseAccountNum.Value;
                    accountToDd.value = _transaction.IncomeAccountNum.Value;
                    transactionSumIf.text = _transaction.TransactionAmount.Value.ToIntString();
                }
            }

            else
            {
                _transaction = null;
                deleteBtn.interactable = false;

                accountFromDd.value = 1;
                accountToDd.value = 1;
                transactionSumIf.text = "";
            }
        }


        public void ClickOnSave()
        {
            var fromAccountNum = accountFromDd.value;
            var toAccountNum = accountToDd.value;
            var sum = transactionSumIf.text.Length > 0 ? float.Parse(transactionSumIf.text) : 0;

            if (_transaction == null)
            {
                _transaction = new TransactionModel();
                PlayerData.CurrentData.AddTransaction(_transaction);
            }

            _transaction.ExpenseAccountNum.Value = fromAccountNum;
            _transaction.IncomeAccountNum.Value = toAccountNum;
            _transaction.TransactionAmount.Value = sum;

            PlayerData.CurrentData.SaveData();
            OnShow(_transaction);
        }

        public void ClickOnRemove()
        {
            PlayerData.CurrentData.RemoveTransaction(_transaction);
            OnShow(_transaction);
        }
    }
}
