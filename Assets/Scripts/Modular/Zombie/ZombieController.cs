using AbstractClass;
using Key;
using TheKiwiCoder;
using UnityEngine;

namespace Zombie
{
    public class ZombieController : AbsController
    {
        [SerializeField]
        private BehaviourTreeRunner behaviourTreeRunner;

        [SerializeField]
        private KeyCollector keyCollector;

        public KeyCollector KeyCollector => keyCollector;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponent(ref behaviourTreeRunner, gameObject);
            ActiveBehaviourTree(false);
            LoadComponent(ref keyCollector, gameObject);
        }

        private void Start()
        {
            absStat.OnDead += SpawnKey;
        }

        private void SpawnKey()
        {
            if (keyCollector.KeySOList == null || keyCollector.KeySOList.Count <= 0) return;
            foreach (var keySO in keyCollector.KeySOList)
            {
                Instantiate(keySO.Prefab, transform.position, keySO.Prefab.transform.rotation);
            }
        }

        public void ActiveBehaviourTree(bool isOn) => behaviourTreeRunner.enabled = isOn;
    }
}
