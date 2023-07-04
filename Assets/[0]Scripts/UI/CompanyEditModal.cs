using Game.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yrr.UI;
using Yrr.Utils;


namespace Game.UI
{
    internal sealed class CompanyEditModal : UIScreen
    {
        [SerializeField] private TMP_InputField companyNameIf;
        [SerializeField] private TMP_InputField companyDescriptionIf;

        [SerializeField] private TMP_InputField companyCostIf;
        [SerializeField] private TMP_Dropdown accountDd;

        [SerializeField] private TMP_InputField incomeIf;
        [SerializeField] private TMP_InputField expensesIf;
        [SerializeField] private TMP_InputField cleaningIf;

        [SerializeField] private Button deleteBtn;
        private CompanyModel _company;


        protected override void OnShow(object args)
        {
            accountDd.ClearOptions();
            foreach (var acc in PlayerData.CurrentData.GetAccounts())
            {
                accountDd.options.Add(new TMP_Dropdown.OptionData(acc.AccountName.Value));
            }
            var emptyOption = new TMP_Dropdown.OptionData("New account");
            accountDd.options.Add(emptyOption);


            if (args != null)
            {
                if (args is CompanyModel company)
                {
                    _company = company;
                    deleteBtn.interactable = true;

                    companyNameIf.text = _company.CompanyName.Value;
                    companyDescriptionIf.text = _company.CompanyDescription.Value;

                    companyCostIf.text = _company.CompanyCost.Value.ToIntString();
                    accountDd.value = _company.AccountNum;

                    incomeIf.text = _company.Income.Value.ToIntString();
                    expensesIf.text = _company.Expense.Value.ToIntString();
                    cleaningIf.text = _company.Cleaning.Value.ToIntString();
                }
            }

            else
            {
                _company = null;
                deleteBtn.interactable = false;

                companyNameIf.text = "";
                companyDescriptionIf.text = "";

                companyCostIf.text = "";
                accountDd.value = accountDd.options.Count;

                incomeIf.text = "";
                expensesIf.text = "";
                cleaningIf.text = "";
            }
        }

        public void ClickOnSave()
        {
            var newName = companyNameIf.text;
            var newDescr = companyDescriptionIf.text;

            var newCost = companyCostIf.text.Length > 0 ? float.Parse(companyCostIf.text) : 0;
            var accountNum = accountDd.value;

            var newIncome = incomeIf.text.Length > 0 ? float.Parse(incomeIf.text) : 0;
            var newExpenses = expensesIf.text.Length > 0 ? float.Parse(expensesIf.text) : 0;
            var newCleaning = cleaningIf.text.Length > 0 ? float.Parse(cleaningIf.text) : 0;


            if (_company == null)
            {
                _company = new CompanyModel();
                PlayerData.CurrentData.AddCompany(_company);
            }

            _company.CompanyName.Value = newName;
            _company.CompanyDescription.Value = newDescr;

            _company.CompanyCost.Value = newCost;
            if (accountNum < accountDd.options.Count - 1)
                _company.AccountNum = accountNum;
            else
            {
                var newAcc = new AccountModel();
                newAcc.AccountName.Value = companyNameIf.text;
                PlayerData.CurrentData.AddAccount(newAcc);
                _company.AccountNum = accountNum;
            }

            _company.Income.Value = newIncome;
            _company.Expense.Value = newExpenses;
            _company.Cleaning.Value = newCleaning;

            PlayerData.CurrentData.SaveData();
            OnShow(_company);
        }

        public void ClickOnRemove()
        {
            PlayerData.CurrentData.RemoveCompany(_company);
            OnShow(_company);
        }
    }
}
