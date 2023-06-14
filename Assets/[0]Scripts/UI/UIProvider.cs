using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yrr.UI;
using Game.Common;

namespace Game.UI
{
    public sealed class UIProvider : Singleton<UIProvider>
    {
        [SerializeField] private UIManager uiManager;

        public static UIManager UIManager => Instance.uiManager;
    }
}
