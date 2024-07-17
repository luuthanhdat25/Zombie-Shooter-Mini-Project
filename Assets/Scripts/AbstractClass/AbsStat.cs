using RepeatUtil;
using System;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbsStat : RepeatMonoBehaviour
    {
        public Action OnDead;

        [SerializeField] 
        protected int hpMax = 5;

        protected bool isDead = false;
        protected int hpCurrent;

        public int CurrentHp => this.hpCurrent;
        public int HpMax => this.hpMax;

        private void OnEnable() => Reborn();

        public virtual void Reborn()
        {
            this.hpCurrent = this.hpMax;
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
            OnDead?.Invoke();
        }
    }
}
