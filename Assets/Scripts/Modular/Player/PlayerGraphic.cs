using AbstractClass;
using UnityEngine;

namespace Player
{
    public class PlayerGraphic : AbsGraphic
    {
        [SerializeField]
        private GameObject hurtModel;

        [SerializeField]
        private float hurtTime = 0.2f;

        private float timer;
        private bool isHurt;

        private void Start()
        {
            isHurt = false;
            hurtModel.SetActive(false);
        }

        private void FixedUpdate()
        {
            if (!isHurt) return;
            timer += Time.fixedDeltaTime;
            if (timer > hurtTime)
            {
                isHurt = false;
                hurtModel.SetActive(false);
                timer = 0;
            }
        }

        public override void ActiveHurtEffect()
        {
            isHurt = true;
            hurtModel.SetActive(true);
            timer = 0;
        }
    }

}
