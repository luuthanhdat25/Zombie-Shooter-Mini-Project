using RepeatUtil;
using UnityEngine;

namespace AbstractClass
{
    /// <summary>
    /// Abstract base class for objects that can receive damage.
    /// </summary>
    public abstract class AbsDamageReciver : RepeatMonoBehaviour
    {
        [SerializeField]
        protected AbsController absController;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponent<AbsController>(ref absController, gameObject);
        }

        /// <summary>
        /// Handles collision with a damage sender.
        /// </summary>
        /// <param name="damageSender">The object that caused the collision.</param>
        public virtual void Collision(AbsDamageSender damageSender)
        {
            if (damageSender == null) return;
            absController.AbsStat.Deduct(damageSender.GetDamage());
            damageSender.GotHit();
        }
    }
}