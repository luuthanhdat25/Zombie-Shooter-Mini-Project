using AbstractClass;
using UnityEngine;

namespace Player
{
    public class PlayerAnimation : AbsAnimator
    {
        private Animator animator;

        protected override void Awake()
        {
            base.Awake();
            LoadComponentInParent(ref animator, gameObject);
        }
    }
}
