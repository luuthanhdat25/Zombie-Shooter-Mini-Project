using AbstractClass;
using ScriptableObjects;
using Sound;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerDamageReciever: AbsDamageReciver 
    {
        [SerializeField]
        private SoundSO playerHurtSound;

        public override void GotHit()
        {
            absController.AbsGraphic.ActiveHurtEffect();
            SoundPooling.Instance.CreateSound(playerHurtSound, transform.position, -0.05f, 0.05f);
        }
    }
}
