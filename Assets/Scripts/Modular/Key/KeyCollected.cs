using ScriptableObjects;
using UnityEngine;

namespace Key
{
    public class KeyCollected : MonoBehaviour
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
