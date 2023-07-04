using Game.Data;
using System.Collections.Generic;
using UnityEngine;
using Yrr.Utils;


namespace Game.UI
{
    internal sealed class CompanyViewAdapter : MonoBehaviour
    {
        [SerializeField] private CompanyCard prefab;
        [SerializeField] private RectTransform container;

        private void Start()
        {
            PlayerData.CurrentData.OnCompanyAdded += AddNewCompany;
            PlayerData.CurrentData.OnCompanyRemoved += RemoveCompany;
        }

        internal void Show(IEnumerable<CompanyModel> models)
        {
            container.ClearChildren();
            foreach (var model in models)
            {
                AddNewCompany(model);
            }
        }

        private void AddNewCompany(CompanyModel model)
        {
            var view = Instantiate(prefab, container);
            view.SetCompany(model);
        }

        private void RemoveCompany(CompanyModel model)
        {
            Show(PlayerData.CurrentData.GetCompanies());
        }
    }
}
