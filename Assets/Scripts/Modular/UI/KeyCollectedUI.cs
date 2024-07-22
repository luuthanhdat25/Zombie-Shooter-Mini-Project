using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class KeyCollectedUI : MonoBehaviour
    {
        [SerializeField]
        private Image tempalteIcon;

        private void Start()
        {
            tempalteIcon.gameObject.SetActive(false);
        }

        public void UpdateVisual(List<KeySO> keySOList)
        {
            foreach (Transform childTransform in this.transform)
            {
                if (childTransform != tempalteIcon.transform)
                    Destroy(childTransform.gameObject);
            }

            foreach (KeySO keySO in keySOList)
            {
                Transform iconTransfom = Instantiate(tempalteIcon.transform, this.transform);
                iconTransfom.gameObject.SetActive(true);
                iconTransfom.GetComponent<Image>().sprite = keySO.KeyIconSprite;
            }
        }
    }
}
