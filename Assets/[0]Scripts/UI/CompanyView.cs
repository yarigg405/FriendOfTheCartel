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
        [SerializeField] private TextMeshProUGUI expensesValueTmp;

        private CompanyModel _company;


        internal void SetCompany(CompanyModel company)
        {
            _company = company;

            _company.CompanyName.OnChange += UpdateName;
            _company.CompanyDescription.OnChange += UpdateDescription;
            _company.Income.OnChange += UpdateIncome;
            _company.Expense.OnChange += UpdateExpenses;

            InitUpdate();
        }



        private void InitUpdate()
        {
            UpdateName(_company.CompanyName.Value);
            UpdateDescription(_company.CompanyDescription.Value);
            UpdateIncome(_company.Income.Value);
            UpdateExpenses(_company.Expense.Value);
        }


        private void OnDestroy()
        {
            _company.CompanyName.OnChange -= UpdateName;
            _company.CompanyDescription.OnChange -= UpdateDescription;
            _company.Income.OnChange -= UpdateIncome;
            _company.Expense.OnChange -= UpdateExpenses;
        }

        private void UpdateName(string obj)
        {
            companyNameTmp.text = obj;
        }

        private void UpdateDescription(string obj)
        {
            companyDescriptionTmp.text = obj;
        }

        private void UpdateIncome(float delta)
        {
            if (delta >= 0)
            {
                incomeValueTmp.text = $"<color=#46FF2A>+{delta.ToMoneyString()}";
            }

            else
            {
                incomeValueTmp.text = $"<color=#F35236>{delta.ToMoneyString()}";
            }
        }

        private void UpdateExpenses(float delta)
        {
            delta *= -1;
            if (delta >= 0)
            {
                expensesValueTmp.text = $"<color=#46FF2A>+{delta.ToMoneyString()}";
            }

            else
            {
                expensesValueTmp.text = $"<color=#F35236>{delta.ToMoneyString()}";
            }
        }

        public void ClickOnOpen()
        {
            UIProvider.UIManager.OpenModal<CompanyEditModal>(_company);
        }
    }
}
