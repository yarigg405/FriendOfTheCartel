using Game.Data;
using Game.Staff;
using TMPro;
using UnityEngine;


namespace Game.UI
{
    internal sealed class AccountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI balanceTmp;
        [SerializeField] private TextMeshProUGUI accountNameTmp;

        private AccountModel _account;

        internal void SetAccount(AccountModel account)
        {
            _account = account;
            UpdateAccountName(_account.AccountName.Value);
            UpdateBalance(_account.AccountBalance.Value);            
            
            _account.AccountBalance.OnChange += UpdateBalance;
            _account.AccountName.OnChange += UpdateAccountName;
        }

        private void OnDestroy()
        {
            _account.AccountBalance.OnChange -= UpdateBalance;
            _account.AccountName.OnChange -= UpdateAccountName;
        }

        private void UpdateAccountName(string obj)
        {
            accountNameTmp.text = obj;
        }

        private void UpdateBalance(float obj)
        {
            balanceTmp.text = obj.ToMoneyString();
                
        }

        public void ClickOnOpen()
        {
            UIProvider.UIManager.OpenModal<AccountEditModal>(_account);
        }
    }
}
