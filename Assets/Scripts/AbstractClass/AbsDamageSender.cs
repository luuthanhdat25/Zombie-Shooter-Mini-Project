using RepeatUtil;
using UnityEngine;

namespace AbstractClass
{
    /// <summary>
    /// Abstract base class for objects that can send damage.
    /// </summary>
    public abstract class AbsDamageSender : RepeatMonoBehaviour
    {
        [SerializeField]
        protected int damage;

        public virtual int GetDamage() => damage;

        public void SetDamage(int newValue) => damage = newValue;

        /// <summary>
        /// Handles collision with a damage sender.
        /// </summary>
        /// <param name="damageSender">The object that caused the collision.</param>
        public virtual void CollisionWithController(AbsController absController)
        {
            if (absController == null) return;
            absController.AbsStat.Deduct(GetDamage());
            absController.AbsDamageReciver.GotHit();
        }
    }
}