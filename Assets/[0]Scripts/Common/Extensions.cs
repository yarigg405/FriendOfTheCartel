using System.Globalization;
using UnityEngine;
using UnityEngine.UI;


namespace Game.Staff
{
    public static class Extensions
    {
        public static string ToMoneyString(this float value)
        {
            return value.ToString("N0", CultureInfo.CreateSpecificCulture("sv-SE")) + "$";
        }

        public static string ToSignedColorString(this float value)
        {
            if (value < 0)
            {
                return $"<color=#F35236>{value.ToMoneyString()}";
            }

            else
            {
                return $"<color=#46FF2A>+{value.ToMoneyString()}";
            }
        }

    }
}
