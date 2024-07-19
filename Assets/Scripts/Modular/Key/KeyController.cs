using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Key
{
    public class KeyController : MonoBehaviour
    {
        [SerializeField]
        private KeySO keySO;

        public KeySO CollectKey()
        {
            Destroy(gameObject);
            return keySO;
        }
    }
}
