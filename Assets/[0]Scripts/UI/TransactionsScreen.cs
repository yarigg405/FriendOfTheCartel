using Game.Data;
using UnityEngine;
using Yrr.UI;
using Yrr.Utils;

namespace Game.UI
{
    internal sealed class TransactionsScreen : UIScreen
    {
        [SerializeField] private RectTransform viewsContainer;
        [SerializeField] private TransactionView viewPrefab;


        private void Start()
        {
            PlayerData.CurrentData.OnTransactionAdded += AddNewTransaction;
            PlayerData.CurrentData.OnTransactionRemoved += RemoveTransaction;
        }

        protected override void OnShow(object args)
        {
            var models = PlayerData.CurrentData.GetTransactions();

            viewsContainer.ClearChildren();
            foreach (var model in models)
            {
                var card = Instantiate(viewPrefab, viewsContainer.transform);
                card.SetTransaction(model);
            }
        }


        private void AddNewTransaction(TransactionModel model)
        {
            var view = Instantiate(viewPrefab, viewsContainer);
            view.SetTransaction(model);
        }

        private void RemoveTransaction(TransactionModel model)
        {
            OnShow(null);
        }
    }
}
