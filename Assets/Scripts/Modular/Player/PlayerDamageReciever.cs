using AbstractClass;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerDamageReciever: AbsDamageReciver 
    {
        public override void GotHit()
        {
            // Play Effect
        }
    }
}
