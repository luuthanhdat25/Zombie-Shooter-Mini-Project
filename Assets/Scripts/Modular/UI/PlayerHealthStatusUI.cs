using AbstractClass;
using Player;
using TMPro;
using UnityEngine;

namespace UI{
    public class PlayerHealthStatusUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text healthText;

        private void Awake()
        {
            PlayerPublicInfor.Instance.Controller.AbsStat.OnHealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(object sender, AbsStat.OnHealthChangedEventArgs e)
        {
            healthText.text = e.HealthUpdated.ToString();
        }
    }
}
