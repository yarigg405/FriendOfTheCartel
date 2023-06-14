using Game.Data;
using Game.Staff;
using System;
using TMPro;
using UnityEngine;


namespace Game.UI
{
    internal sealed class CompanyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI companyNameTmp;
        [SerializeField] private TextMeshProUGUI companyDescriptionTmp;
        [SerializeField] private TextMeshProUGUI incomeValueTmp;

        private CompanyModel _company;


        internal void SetCompany(CompanyModel company)
        {
            _company = company;

            _company.CompanyName.OnChange += UpdateName;
            _company.CompanyDescription.OnChange += UpdateDescription;
            _company.Income.OnChange += UpdateIncome;
            _company.Expense.OnChange += UpdateIncome;

            InitUpdate();
        }

        private void InitUpdate()
        {
            UpdateName(_company.CompanyName.Value);
            UpdateDescription(_company.CompanyDescription.Value);
            UpdateIncome(0);
        }


        private void OnDestroy()
        {
            _company.CompanyName.OnChange -= UpdateName;
            _company.CompanyDescription.OnChange -= UpdateDescription;
            _company.Income.OnChange -= UpdateIncome;
            _company.Expense.OnChange -= UpdateIncome;
        }
       
        private void UpdateName(string obj)
        {
            companyNameTmp.text = obj;
        }

        private void UpdateDescription(string obj)
        {
            companyDescriptionTmp.text = obj;
        }

        private void UpdateIncome(float obj)
        {
            var delta = _company.Income.Value - _company.Expense.Value;

            if (delta >= 0)
            {
                incomeValueTmp.text = $"<color=#46FF2A>+{delta.ToMoneyString()}";
            }

            else
            {
                incomeValueTmp.text = $"<color=#F35236>{delta.ToMoneyString()}";
            }
        }

        public void ClickOnOpen()
        {
            UIProvider.UIManager.OpenModal<CompanyEditModal>(_company);
        }
    }
}
