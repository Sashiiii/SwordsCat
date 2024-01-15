using UnityEngine;
using UnityEngine.Rendering;

namespace NK.Singleton
{
    [CreateAssetMenu(menuName = "Scriptables/Singleton List", fileName = "SingletonList")]
    public class SingletonList : ScriptableObject
    {
        public SerializedDictionary<string, Singleton> singletonList = new();

        public void Add(Singleton singleton)
        {
            string key = singleton.GetType().Name;
            if (!singletonList.ContainsKey(key))
                singletonList.Add(key, singleton);
        }

        public bool Remove(Singleton singleton)
        {
            return singletonList.Remove(singleton.GetType().Name);
        }

        public bool GetSingleton<T>(out T set) where T : Singleton
        {
            bool retVal = singletonList.TryGetValue(typeof(T).Name, out Singleton value);
            set = (T)value;
            return retVal;
        }

        public static SingletonList GetSingletonList()
        {
            return Resources.Load<SingletonList>("SingletonList");
        }
    }
}