using RepeatUtil;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbsStat : RepeatMonoBehaviour
    {
        [SerializeField] 
        protected int hpMax = 5;

        [SerializeField]
        protected int damageStart = 5;
        
        protected bool isDead = false;
        protected int hpCurrent;
        protected int damageCurrent;

        public int CurrentHp => this.hpCurrent;
        public int CurrentDamge => this.damageCurrent;
        public int HpMax => this.hpMax;

        private void OnEnable() => Reborn();

        public virtual void Reborn()
        {
            this.hpCurrent = this.hpMax;
            damageCurrent = damageStart;
            this.isDead = false;
        }

        public virtual void Add(int hpAdd)
        {
            if (this.isDead) return;
            if (this.hpCurrent >= this.hpMax) return;

            this.hpCurrent += hpAdd;
        }

        public virtual void Deduct(int hpDeduct)
        {
            if (IsDead()) return;

            this.hpCurrent -= hpDeduct;
            this.CheckIsDead();
        }

        protected virtual bool IsDead() => this.hpCurrent <= 0;

        protected virtual void CheckIsDead()
        {
            if (!this.IsDead()) return;
            this.isDead = true;
            this.OnDead();
        }

        protected abstract void OnDead();

        public virtual void AddDamage(int amount)
        {
            damageCurrent += amount;
        }

        public virtual void DeductDamage(int amount)
        {
            damageCurrent = damageCurrent > amount? damageCurrent - amount: 0;
        }
    }
}
