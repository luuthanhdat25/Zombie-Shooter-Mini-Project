using AbstractClass;
using Manager;
using UnityEngine;

namespace Player
{
    public class PlayerStat : AbsStat
    {
        [SerializeField]
        private PlayerSO playerSO;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            if (playerSO == null) Debug.Log("Player Stat does't have PlayerSO");
            hpMax = playerSO.Health;
        }

        private void Start()
        {
            OnDead += () => GameManager.Instance.GameOver(false);
        }

        public override float GetMoveSpeed() => playerSO.MoveSpeed;
    }
}

