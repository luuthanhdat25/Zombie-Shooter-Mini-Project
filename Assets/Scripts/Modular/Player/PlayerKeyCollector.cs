using Key;
using Player;
using ScriptableObjects;
using Sound;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerKeyCollector : KeyCollector
    {
        [SerializeField]
        private SoundSO pickupKeySoundSO;

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!isCollectKey) return;
            if (hit.collider == null) return;

            KeyCollected key = hit.collider.GetComponent<KeyCollected>();
            if (key == null) return;
            KeySO keySO = key.CollectKey();
            Add(keySO);

            SoundPooling.Instance.CreateSound(pickupKeySoundSO, PlayerPublicInfor.Instance.Position, 0, 0);
            UIManager.Instance.InPlayingUI.KeyCollectedUI.UpdateVisual(keySOList);
            UIManager.Instance.InPlayingUI.MessageUI.ShowMessage("Player get " + keySO.Name, Color.green);
        }

        public override void RemoveKey(KeySO keySO)
        {
            base.RemoveKey(keySO);
            UIManager.Instance.InPlayingUI.KeyCollectedUI.UpdateVisual(keySOList);
        }
    }
}

