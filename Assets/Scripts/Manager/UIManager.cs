using RepeatUtil;
using RepeatUtil.DesignPattern.SingletonPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField]
        private KeyCollectedUI keyCollectedUI;

        public KeyCollectedUI KeyCollectedUI => keyCollectedUI;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponentInChild(ref keyCollectedUI, gameObject);
        }
    }
}
