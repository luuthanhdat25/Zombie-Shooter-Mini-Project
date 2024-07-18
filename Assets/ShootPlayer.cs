using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using Gun;
using Player;

public class ShootPlayer : ActionNode
{
    public float idleShootTime = 3f;

    private GunSelector gunSelector;
    private float timer;

    protected override void OnStart() {
        if(gunSelector == null) 
        {
            gunSelector = context.absController.GetComponent<GunSelector>();
            if (gunSelector == null) Debug.Log($"{context.absController.gameObject.name} doesn't have {typeof(GunSelector).Name}");
        }
        gunSelector.SetInfiniteBullet(true);
        timer = 0;

        context.absController.AbsAnimator.SetBool("Attack", true);
    }

    protected override void OnStop() {
        context.absController.AbsAnimator.SetBool("Attack", false);
    }

    protected override State OnUpdate() {
        Vector3 playerPosition = PlayerPublicInfor.Instance.Position;
        Vector3 directionNormalize = (playerPosition - context.absController.transform.position).normalized;
        gunSelector.UsingGun(directionNormalize, true);
        context.absController.AbsMovement.Rotate(directionNormalize);

        timer += Time.deltaTime;
        if(timer >= idleShootTime) 
        {
            timer = 0;
            return State.Success;
        }
        return State.Running;
    }
}
