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

        /// <summary>
        /// Called when hit the target.
        /// </summary>
        public abstract void GotHit();

        public void SetDamage(int newValue) => damage = newValue;
    }
}