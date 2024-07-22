using RepeatUtil.DesignPattern.SingletonPattern;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MessageUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text messageText;

        [SerializeField]
        private float timeShowMessage;

        private float timer;
        private bool isShow;

        private void Start() => messageText.gameObject.SetActive(false);

        private void FixedUpdate()
        {
            if (!isShow) return;
            timer += Time.fixedDeltaTime;
            if (timer > timeShowMessage)
            {
                isShow = false;
                messageText.gameObject.SetActive(false);
            }
        }

        public void ShowMessage(string message, Color color)
        {
            messageText.text = message;
            messageText.color = color;
            messageText.gameObject.SetActive(true);
            isShow = true;
            timer = 0;
        }
    }
}

