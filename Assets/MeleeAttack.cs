using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MeleeAttack : ActionNode
{
    public float delayBeforeAttack = 0.5f;

    private float delayTimer;

    protected override void OnStart() {
        delayTimer = 0;
    }

    protected override void OnStop() {
        
    }

    protected override State OnUpdate() {
        delayTimer += Time.deltaTime;
        if(delayTimer >= delayBeforeAttack)
        {
            Debug.Log("Attack");
            return State.Success;
        }
        return State.Running;
    }
}
