using System.Globalization;
using UnityEngine;
using UnityEngine.UI;


namespace Game.Staff
{
    public static class Extensions
    {
        public static string ToMoneyString(this float value)
        {
            return value.ToString("N1", CultureInfo.CreateSpecificCulture("sv-SE")) + "$";
        }

    }
}
