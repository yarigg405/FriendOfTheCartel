using Game.Data;
using UnityEngine;


namespace Game
{
    internal sealed class DropData : MonoBehaviour
    {
        public void ClickOnDropData()
        {
            PlayerPrefs.DeleteAll();
            PlayerData.LoadData();
        }
    }
}
