using Game.Data;
using System.Linq;
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

        [SerializeField] private TMP_InputField incomeIf;
        [SerializeField] private TMP_InputField expensesIf;

        [SerializeField] private TMP_Dropdown incomeAccountDd;
        [SerializeField] private TMP_Dropdown expensesAccoutDd;

        [SerializeField] private Button deleteBtn;
        private CompanyModel _company;


        protected override void OnShow(object args)
        {
            incomeAccountDd.ClearOptions();
            expensesAccoutDd.ClearOptions();
            foreach (var acc in PlayerData.CurrentData.GetAccounts())
            {
                incomeAccountDd.options.Add(new TMP_Dropdown.OptionData(acc.AccountName.Value));
                expensesAccoutDd.options.Add(new TMP_Dropdown.OptionData(acc.AccountName.Value));
            }
            var emptyOption = new TMP_Dropdown.OptionData("");
            incomeAccountDd.options.Add(emptyOption);
            expensesAccoutDd.options.Add(emptyOption);


            if (args != null)
            {
                if (args is CompanyModel company)
                {
                    _company = company;
                    deleteBtn.interactable = true;

                    companyNameIf.text = _company.CompanyName.Value;
                    companyDescriptionIf.text = _company.CompanyDescription.Value;

                    incomeIf.text = _company.Income.Value.ToIntString();
                    expensesIf.text = _company.Expense.Value.ToIntString();

                    incomeAccountDd.value = _company.IncomeAccountNum;
                    expensesAccoutDd.value = _company.ExpenseAccountNum;
                }
            }
            else
            {
                deleteBtn.interactable = false;

                companyNameIf.text = string.Empty;
                companyDescriptionIf.text = string.Empty;

                incomeIf.text = string.Empty;
                expensesIf.text = string.Empty;
            }
        }

        public void ClickOnSave()
        {
            var newName = companyNameIf.text;
            var newDescr = companyDescriptionIf.text;
            var newIncome = float.Parse(incomeIf.text);
            var newExpenses = float.Parse(expensesIf.text);

            var incAccValue = incomeAccountDd.value;
            var expAccValue = expensesAccoutDd.value;


            if (_company == null)
            {
                _company = new CompanyModel();

                _company.CompanyName.Value = newName;
                _company.CompanyDescription.Value = newDescr;
                _company.Income.Value = newIncome;
                _company.Expense.Value = newExpenses;
                if (incAccValue < incomeAccountDd.options.Count)
                {
                    _company.IncomeAccountNum = incAccValue;
                }
                if (expAccValue < expensesAccoutDd.options.Count)
                {
                    _company.ExpenseAccountNum = expAccValue;
                }

                PlayerData.CurrentData.AddCompany(_company);
            }
            else
            {
                _company.CompanyName.Value = newName;
                _company.CompanyDescription.Value = newDescr;
                _company.Income.Value = newIncome;
                _company.Expense.Value = newExpenses;
                if (incAccValue < incomeAccountDd.options.Count)
                {
                    _company.IncomeAccountNum = incAccValue;
                }
                if (expAccValue < expensesAccoutDd.options.Count)
                {
                    _company.ExpenseAccountNum = expAccValue;
                }
            }

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
