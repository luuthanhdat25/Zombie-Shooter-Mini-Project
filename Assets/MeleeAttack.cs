using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using Player;

public class MeleeAttack : ActionNode
{
    public float delayBeforeAttack = 0.5f;

    private float delayTimer;

    protected override void OnStart() {
        delayTimer = 0;
        Vector3 playerPosition = PlayerPublicInfor.Instance.Position;
        Vector3 rotateDirection = (playerPosition - context.absController.transform.position).normalized;
        context.absController.AbsMovement.Rotate(rotateDirection);
        context.absController.AbsAnimator.SetBool("Attack", true);
    }

    protected override void OnStop() {
        context.absController.AbsAnimator.SetBool("Attack", false);
    }

    protected override State OnUpdate()
    {
        delayTimer += Time.deltaTime;

        if (delayTimer >= delayBeforeAttack)
        {
            var controllerAttack = context.absController.AbsDamageSender.CheckCollision();
            if(controllerAttack != null && controllerAttack.Count != 0)
            {
                foreach (var controller in controllerAttack)
                {
                    context.absController.AbsDamageSender.CollisionWithController(controller);
                }
                Debug.Log("Found Character Controller");
            }
            else
            {
                Debug.Log("Not Found Character Controller");
            }

            return State.Success;
        }
        return State.Running;
    }

}
