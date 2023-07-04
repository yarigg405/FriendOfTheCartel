using Game.Data;
using Game.Staff;
using TMPro;
using UnityEngine;


namespace Game.UI
{
    internal sealed class CompanyCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI companyNameTmp;
        [SerializeField] private TextMeshProUGUI companyCostTmp;
        [SerializeField] private TextMeshProUGUI incomeTmp;
        [SerializeField] private TextMeshProUGUI cleaningTmp;

        private CompanyModel _company;


        internal void SetCompany(CompanyModel company)
        {
            _company = company;

            _company.CompanyName.OnChange += UpdateName;
            _company.CompanyCost.OnChange += UpdateCost;

            _company.Income.OnChange += UpdateIncome;
            _company.Expense.OnChange += UpdateIncome;
            _company.Cleaning.OnChange += UpdateCleaning;

            InitUpdate();
        }

        private void OnDestroy()
        {
            _company.CompanyName.OnChange -= UpdateName;
            _company.CompanyCost.OnChange -= UpdateCost;

            _company.Income.OnChange -= UpdateIncome;
            _company.Expense.OnChange -= UpdateIncome;
            _company.Cleaning.OnChange -= UpdateCleaning;
        }


        private void InitUpdate()
        {
            UpdateName(_company.CompanyName.Value);
            UpdateCost(_company.CompanyCost.Value);
            UpdateIncome(_company.Income.Value);
            UpdateCleaning(_company.Cleaning.Value);
        }



        private void UpdateName(string obj)
        {
            companyNameTmp.text = obj;
        }

        private void UpdateCost(float obj)
        {
            companyCostTmp.text = obj.ToMoneyString();
        }

        private void UpdateIncome(float obj)
        {
            var delta = _company.Income.Value - _company.Expense.Value;
            incomeTmp.text = delta.ToSignedColorString();
        }

        private void UpdateCleaning(float obj)
        {
            cleaningTmp.text = obj.ToMoneyString();
        }



        public void ClickOnOpen()
        {
            UIProvider.UIManager.OpenModal<CompanyEditModal>(_company);
        }
    }
}
