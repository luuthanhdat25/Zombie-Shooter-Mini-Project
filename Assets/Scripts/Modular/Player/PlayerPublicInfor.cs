using AbstractClass;
using RepeatUtil.DesignPattern.SingletonPattern;
using UnityEngine;

namespace Player
{
    public class PlayerPublicInfor : Singleton<PlayerPublicInfor>
    {
        [SerializeField]
        private AbsController controller;

        [SerializeField]
        private int playerLayerMarkIndex;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponent(ref controller, gameObject);
        }

        public AbsController Controller => controller;

        public Vector3 Position => transform.position;

        public int PlayerLayerMarkIndex => 1 << LayerMask.NameToLayer("Player");
    }
}
