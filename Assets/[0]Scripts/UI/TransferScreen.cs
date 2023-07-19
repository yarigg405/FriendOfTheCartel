using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yrr.UI;
using Game.Data;
using Yrr.Utils;

namespace Game.UI
{
    internal sealed class TransferScreen : UIScreen
    {
        [SerializeField] private TMP_Dropdown accountFromDd;
        [SerializeField] private TMP_Dropdown accountToDd;
        [SerializeField] private TMP_InputField summIf;

        private PlayerData _playerData => PlayerData.CurrentData;


        protected override void OnShow(object args)
        {
            accountFromDd.ClearOptions();
            accountToDd.ClearOptions();
            foreach (var acc in PlayerData.CurrentData.GetAccounts())
            {
                accountFromDd.options.Add(new TMP_Dropdown.OptionData(acc.AccountName.Value));
                accountToDd.options.Add(new TMP_Dropdown.OptionData(acc.AccountName.Value));
            }
        }

        public void ClickOnAllMoney()
        {
            var accountFrom = _playerData._accounts[accountFromDd.value];
            summIf.text = accountFrom.AccountBalance.Value.ToIntString();
        }


        public void ClickOnConfirm()
        {
            var accountFrom = _playerData._accounts[accountFromDd.value];
            var accountTo = _playerData._accounts[accountToDd.value];
            var summ = summIf.text.Length > 0 ? float.Parse(summIf.text) : 0f;

            accountFrom.AccountBalance.Value -= summ;
            accountTo.AccountBalance.Value += summ;
        }
    }
}
