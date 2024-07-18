using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using Player;
using System.Threading;

public class ChaseToPlayerPosition : ActionNode
{
    public float speed = 5;
    public float delayUpdatePlayerPosition = 1f;
    public float tolerance = 1.0f;

    private float delayTimer;

    protected override void OnStart()
    {
        context.absController.AbsMovement.Move(PlayerPublicInfor.Instance.Position, speed);
        delayTimer = 0;
        Debug.Log("Update Player Position");
    }


    protected override void OnStop() {
    }

    protected override State OnUpdate()
    {
        delayTimer += Time.deltaTime;
        if(delayTimer >= delayUpdatePlayerPosition)
        {
            context.absController.AbsMovement.Move(PlayerPublicInfor.Instance.Position, speed);
            Debug.Log("Update Player Position");
            delayTimer = 0;
        }

        if (context.agent.pathPending)
        {
            return State.Running;
        }

        if (context.agent.remainingDistance < tolerance)
        {
            return State.Success;
        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return State.Failure;
        }

        return State.Running;
    }
}
