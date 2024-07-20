using AbstractClass;
using Key;
using NPBehave;
using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    public class ZombieController : AbsController
    {
        [SerializeField]
        protected KeyCollector keyCollector;

        [SerializeField]
        protected NavMeshAgent agent;

        protected Root behaviorTree;

        public KeyCollector KeyCollector => keyCollector;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponent(ref keyCollector, gameObject);
            LoadComponent(ref agent, gameObject);
        }

        protected virtual void Start()
        {
            absStat.OnDead += SpawnKey;
            behaviorTree = CreateBehaviourTree();
#if UNITY_EDITOR
            if(behaviorTree != null )
            {
                Debugger debugger = (Debugger)this.gameObject.AddComponent(typeof(Debugger));
                debugger.BehaviorTree = behaviorTree;
            }
#endif
        }

        protected virtual Root CreateBehaviourTree() => null;

        protected virtual void OnDestroy()
        {
            StopBehaviorTree();
        }

        protected virtual void StopBehaviorTree()
        {
            if (behaviorTree != null && behaviorTree.CurrentState == Node.State.ACTIVE)
            {
                behaviorTree.Stop();
            }
        }

        private void SpawnKey()
        {
            if (keyCollector.KeySOList == null || keyCollector.KeySOList.Count <= 0) return;
            foreach (var keySO in keyCollector.KeySOList)
            {
                Instantiate(keySO.Prefab, transform.position, keySO.Prefab.transform.rotation);
            }
        }

        public virtual void ActiveBehaviourTree() => behaviorTree.Start();
    }
}
