using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Key
{
    public class KeyCollector : MonoBehaviour
    {
        [SerializeField]
        protected List<KeySO> keySOList;

        [SerializeField]
        protected bool isCollectKey;

        public void Add(KeySO keySO) => keySOList.Add(keySO);

        public virtual void RemoveKey(KeySO keySO)
        {
            if (keySOList.Count <= 0) return;
            if (!keySOList.Contains(keySO)) return;
            keySOList.Remove(keySO);
        }

        public bool IsHasKey(KeySO keySO)
        {
            if (keySOList.Count <= 0) return false;
            return keySOList.Contains(keySO);
        }

        public List<KeySO> KeySOList => keySOList;
    }
}

