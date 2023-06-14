using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yrr.UI;
using Game.Data;

namespace Game.UI
{
    internal sealed class MainScreen : UIScreen
    {
        [SerializeField] private AccountsViewAdapter accountsAdapter;

        [SerializeField] private CompanyViewAdapter companiesAdapter;

        protected override void OnShow(object args)
        {
            accountsAdapter.Show(PlayerData.CurrentData.GetAccounts());
            companiesAdapter.Show(PlayerData.CurrentData.GetCompanies());
        }
    }
}
