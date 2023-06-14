using Game.Data;
using System.Collections.Generic;
using UnityEngine;
using Yrr.Utils;


namespace Game.UI
{
    internal sealed class AccountsViewAdapter : MonoBehaviour
    {
        [SerializeField] private AccountView prefab;
        [SerializeField] private RectTransform container;



        private void Start()
        {
            PlayerData.CurrentData.OnAccountAdded += AddNewAccount;
            PlayerData.CurrentData.OnAccountRemoved += RemoveAccount;
        }


        internal void Show(IEnumerable<AccountModel> accounts)
        {
            container.ClearChildren();
            foreach (var account in accounts)
            {
                AddNewAccount(account);
            }
        }



        private void AddNewAccount(AccountModel account)
        {
            var view = Instantiate(prefab, container);
            view.SetAccount(account);
        }

        private void RemoveAccount(AccountModel model)
        {
            Show(PlayerData.CurrentData.GetAccounts()); ;
        }
    }
}
