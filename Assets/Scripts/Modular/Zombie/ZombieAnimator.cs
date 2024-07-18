using AbstractClass;
using UnityEngine;

namespace Zombie
{
    public class ZombieAnimator : AbsAnimator
    {
        [SerializeField]
        private Animator animator;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponentInParent(ref animator, gameObject);
        }

        public override void SetBool(string parraName, bool value)
        {
            animator.SetBool(parraName, value);
        }
    }
}
