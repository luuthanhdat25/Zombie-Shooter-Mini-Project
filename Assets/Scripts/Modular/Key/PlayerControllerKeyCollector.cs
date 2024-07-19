using ScriptableObjects;
using UI;
using UnityEngine;

namespace Key
{
    public class PlayerControllerKeyCollector : KeyCollector
    {
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!isCollectKey) return;
            if (hit.collider == null) return;

            KeyController keyController = hit.collider.GetComponent<KeyController>();
            if (keyController == null) return;
            KeySO keySO = keyController.CollectKey();
            Add(keySO);
            UIManager.Instance.KeyCollectedUI.UpdateVisual(keySOList);
            MessageUI.Instance.ShowMessage("Player get " + keySO.Name, Color.green);
        }
    }
}

