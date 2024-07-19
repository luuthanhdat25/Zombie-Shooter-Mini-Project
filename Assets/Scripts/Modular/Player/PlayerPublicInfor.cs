using AbstractClass;
using Key;
using RepeatUtil.DesignPattern.SingletonPattern;
using UnityEngine;

namespace Player
{
    public class PlayerPublicInfor : Singleton<PlayerPublicInfor>
    {
        [SerializeField]
        private AbsController controller;

        [SerializeField]
        private string playerLayerMark = "Player";

        [SerializeField]
        private KeyCollector keyCollector;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponent(ref controller, gameObject);
            LoadComponent(ref keyCollector, gameObject);
        }

        public AbsController Controller => controller;

        public Vector3 Position => transform.position;

        public int PlayerLayerMarkIndex => 1 << LayerMask.NameToLayer(playerLayerMark);

        public KeyCollector KeyCollector => keyCollector;
    }
}
