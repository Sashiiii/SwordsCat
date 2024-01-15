using UnityEngine;

namespace NK.Singleton
{
    public class Singleton : MonoBehaviour
    {
        public SingletonList singletonList;

        protected virtual void OnEnable()
        {
            if (singletonList == null)
                singletonList = SingletonList.GetSingletonList();
            singletonList.Add(this);
        }

        protected virtual void OnDisable()
        {
            singletonList.Remove(this);
        }
    }
}