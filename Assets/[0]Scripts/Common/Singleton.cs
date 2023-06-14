using UnityEngine;


namespace Game.Common
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                if (!_instance)
                    _instance = GameObject.FindObjectOfType<T>();

                return _instance;
            }
        }
        private static T _instance;
    }
}