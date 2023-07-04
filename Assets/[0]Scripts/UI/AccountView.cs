using Game.Data;
using Game.Staff;
using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;


namespace Game.UI
{
    internal sealed class AccountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI balanceTmp;
        [SerializeField] private TextMeshProUGUI accountNameTmp;
        [SerializeField] private TextMeshProUGUI incomePerDayTmp;

        private AccountModel _account;

        internal void SetAccount(AccountModel account)
        {
            _account = account;

            _account.AccountBalance.OnChange += UpdateBalance;
            _account.AccountName.OnChange += UpdateAccountName;
            _account.AccountIncome.OnChange += UpdateIncome;


            UpdateBalance(_account.AccountBalance.Value);
            UpdateAccountName(_account.AccountName.Value);
            UpdateIncome(_account.AccountIncome.Value);
        }

        private void OnDestroy()
        {
            _account.AccountBalance.OnChange -= UpdateBalance;
            _account.AccountName.OnChange -= UpdateAccountName;
            _account.AccountIncome.OnChange -= UpdateIncome;
        }

        private void UpdateAccountName(string obj)
        {
            accountNameTmp.text = obj;
        }

        private void UpdateBalance(float obj)
        {
            balanceTmp.text = obj.ToMoneyString();
        }

        private void UpdateIncome(float obj)
        {
            incomePerDayTmp.text = $"{obj.ToSignedColorString()}/day";
        }


        public void ClickOnOpen()
        {
            UIProvider.UIManager.OpenModal<AccountEditModal>(_account);
        }
    }
}
