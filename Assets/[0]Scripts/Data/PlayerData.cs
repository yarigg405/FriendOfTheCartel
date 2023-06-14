using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


namespace Game.Data
{
    [Serializable]
    public sealed partial class PlayerData
    {
        public List<AccountModel> _accounts = new();
        public event Action<AccountModel> OnAccountAdded;
        public event Action<AccountModel> OnAccountRemoved;
        public string lastCalculateDate;

        public void AddAccount(AccountModel account)
        {
            _accounts.Add(account);
            OnAccountAdded?.Invoke(account);
            SaveData();
        }

        public void RemoveAccount(AccountModel account)
        {
            if (_accounts.Remove(account))
            {
                OnAccountRemoved?.Invoke(account);
                SaveData();
            }
        }

        public IEnumerable<AccountModel> GetAccounts()
        {
            return _accounts;
        }



        public List<CompanyModel> _companies = new();
        public event Action<CompanyModel> OnCompanyAdded;
        public event Action<CompanyModel> OnCompanyRemoved;


        public void AddCompany(CompanyModel company)
        {
            _companies.Add(company);
            OnCompanyAdded?.Invoke(company);
            SaveData();
        }

        public void RemoveCompany(CompanyModel company)
        {
            if (_companies.Remove(company))
            {
                OnCompanyRemoved?.Invoke(company);
                SaveData();
            }
        }

        public IEnumerable<CompanyModel> GetCompanies()
        {
            return _companies;
        }


        private void Init()
        {
        }
    }


    public sealed partial class PlayerData
    {
        public static PlayerData CurrentData
        {
            get
            {
                if (_currData == null)
                {
                    LoadData();
                }
                return _currData;
            }
        }
        private static PlayerData _currData;


        private const string key = "playerdata";
        private static bool _canSave = true;



        public PlayerData()
        {
            Init();
        }


        public static void LoadData()
        {
            var str = PlayerPrefs.GetString(key);
            if (str != null && str.Length > 0)
            {
                var json = JsonConvert.DeserializeObject<PlayerData>(str);

                if (json == null)
                {

                    _currData = new PlayerData();
                    _currData.SaveData();
                }
                else
                {
                    _currData = (json);
                }
            }
            else
            {
                _currData = new PlayerData();
                _currData.SaveData();
            }
            _canSave = true;
        }


        public void SaveData()
        {
            if (!_canSave) return;

            if (_currData != null)
            {
                _currData.lastCalculateDate = DateTime.Now.ToString();
                string json = JsonConvert.SerializeObject(_currData);
                PlayerPrefs.SetString(key, json);
                PlayerPrefs.Save();
            }
        }
    }
}
