using RepeatUtil;
using System;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbsStat : RepeatMonoBehaviour
    {
        public Action OnDead;
        public EventHandler<OnHealthChangedEventArgs> OnHealthChanged;

        public class OnHealthChangedEventArgs : EventArgs
        {
            public int HealthUpdated;
        }

        protected int hpMax;
        protected bool isDead = false;
        protected int hpCurrent;
        protected int damage;
        

        private void OnEnable() => Reborn();

        public virtual void Reborn()
        {
            this.hpCurrent = this.hpMax;
            this.isDead = false;
            CallOnHealthChangedEvent(hpCurrent);
        }

        public void CallOnHealthChangedEvent(int healthUpdated)
        {
            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { HealthUpdated = healthUpdated });
        }

        public virtual void Add(int hpAdd)
        {
            if (this.isDead) return;
            if (this.hpCurrent >= this.hpMax) return;

            this.hpCurrent += hpAdd;
            CallOnHealthChangedEvent(hpCurrent);
        }

        public virtual void Deduct(int hpDeduct)
        {
            if (IsDead()) return;

            this.hpCurrent -= hpDeduct;
            Debug.Log("Deduct: " + hpDeduct);
            CallOnHealthChangedEvent(hpCurrent);
            this.CheckIsDead();
        }

        protected virtual bool IsDead() => this.hpCurrent <= 0;

        protected virtual void CheckIsDead()
        {
            if (!this.IsDead()) return;
            this.isDead = true;
            OnDead?.Invoke();
        }

        public int GetCurrentHp() => this.hpCurrent;
        public int GetHpMax() => this.hpMax;

        public virtual float GetMoveSpeed() => 0f;

        public virtual int GetDamage() => damage;

        public virtual void SetDamage(int value) => damage = value;
    }
}
