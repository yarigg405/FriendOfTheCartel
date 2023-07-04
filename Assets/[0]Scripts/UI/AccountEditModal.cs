using Game.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yrr.UI;
using Yrr.Utils;

namespace Game.UI
{
    internal sealed class AccountEditModal : UIScreen
    {
        [SerializeField] private TMP_InputField nameIf;
        [SerializeField] private TMP_InputField balanceIf;
        [SerializeField] private Button deleteButton;

        private AccountModel _account;

        protected override void OnShow(object args)
        {
            if (args != null)
            {
                if (args is AccountModel account)
                {
                    _account = account;
                    deleteButton.interactable = true;

                    nameIf.text = _account.AccountName.Value.ToString();
                    balanceIf.text = _account.AccountBalance.Value.ToIntString();
                }
            }
            else
            {
                _account = null;
                deleteButton.interactable = false;
                nameIf.text = string.Empty;
                balanceIf.text = string.Empty;
            }
        }

        public void ClickOnSave()
        {
            var newName = nameIf.text;
            var newBalance = balanceIf.text.Length > 0 ? float.Parse(balanceIf.text) : 0;

            if (_account != null)
            {
                _account.AccountName.Value = newName;
                _account.AccountBalance.Value = newBalance;
            }

            else
            {
                _account = new AccountModel();
                _account.AccountName.Value = newName;
                _account.AccountBalance.Value = newBalance;
                PlayerData.CurrentData.AddAccount(_account);
            }

            PlayerData.CurrentData.SaveData();
            OnShow(_account);
        }

        public void ClickOnDelete()
        {
            PlayerData.CurrentData.RemoveAccount(_account);
            OnShow(null);
        }
    }
}
