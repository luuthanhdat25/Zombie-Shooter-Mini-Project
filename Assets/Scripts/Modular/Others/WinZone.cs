using Manager;
using Player;
using ScriptableObjects;
using UI;
using UnityEngine;

namespace Other
{
    public class WinZone : MonoBehaviour
    {
        [SerializeField]
        private KeySO winKeySO;

        private void OnTriggerEnter(Collider other)
        {
            if (other == null) return;
            if (other.GetComponent<PlayerPublicInfor>() == null) return;

            if (!PlayerPublicInfor.Instance.KeyCollector.IsHasKey(winKeySO))
            {
                UIManager.Instance.InPlayingUI.MessageUI.ShowMessage("Player doesn't has " + winKeySO.Name, Color.red);
            }
            else
            {
                PlayerPublicInfor.Instance.KeyCollector.RemoveKey(winKeySO);
                GameManager.Instance.GameOver(true);
            }
        }
    }
}

