using AbstractClass;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerDamageReciever: AbsDamageReciver 
    {
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.collider == null) return;
            AbsController absControllerHit = hit.gameObject.GetComponent<AbsController>();
            if(absControllerHit == null) return;
            
            AbsDamageSender damageSender = absControllerHit.AbsDamageSender;
            Collision(damageSender);

            Debug.Log("Hit player");
        }
    }
}
