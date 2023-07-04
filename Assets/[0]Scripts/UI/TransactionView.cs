using Game.Data;
using Game.Staff;
using System;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    internal sealed class TransactionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI transactionIncomeAccountNameTmp;
        [SerializeField] private TextMeshProUGUI transactionExpenseAccountNameTmp;
        [SerializeField] private TextMeshProUGUI transactionValueTmp;

        private TransactionModel _transactionModel;

        internal void SetTransaction(TransactionModel transaction)
        {
            _transactionModel = transaction;

            _transactionModel.TransactionAmount.OnChange += UpdateTransactionSum;
            _transactionModel.ExpenseAccountNum.OnChange += UpdateTransactionFromAccount;
            _transactionModel.IncomeAccountNum.OnChange += UpdateTransactionToAccount;

            InitUpdate();
        }

        private void OnDestroy()
        {
            _transactionModel.TransactionAmount.OnChange -= UpdateTransactionSum;
            _transactionModel.ExpenseAccountNum.OnChange -= UpdateTransactionFromAccount;
            _transactionModel.IncomeAccountNum.OnChange -= UpdateTransactionToAccount;
        }

        private void InitUpdate()
        {
            UpdateTransactionFromAccount(_transactionModel.ExpenseAccountNum.Value);
            UpdateTransactionToAccount(_transactionModel.IncomeAccountNum.Value);
            UpdateTransactionSum(_transactionModel.TransactionAmount.Value);
        }

        private void UpdateTransactionSum(float obj)
        {
            transactionValueTmp.text = obj.ToMoneyString();
        }

        private void UpdateTransactionFromAccount(int obj)
        {
            var acc = PlayerData.CurrentData._accounts[obj];
            transactionExpenseAccountNameTmp.text = acc.AccountName.Value;
        }

        private void UpdateTransactionToAccount(int obj)
        {
            var acc = PlayerData.CurrentData._accounts[obj];
            transactionIncomeAccountNameTmp.text = acc.AccountName.Value;
        }


        public void ClickOnOpen()
        {
            UIProvider.UIManager.OpenModal<TransactionEditModal>(_transactionModel);
        }
    }
}
