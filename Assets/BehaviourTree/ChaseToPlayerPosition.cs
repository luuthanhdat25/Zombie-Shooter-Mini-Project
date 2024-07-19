using UnityEngine;
using TheKiwiCoder;
using Player;

public class ChaseToPlayerPosition : ActionNode
{
    public float delayUpdatePlayerPosition = 1f;
    private float delayTimer;
    private float speed;

    protected override void OnStart()
    {
        speed = context.absController.AbsStat.GetMoveSpeed();
        context.absController.AbsMovement.Move(PlayerPublicInfor.Instance.Position, speed);
        delayTimer = 0;
        context.absController.AbsAnimator.SetBool("Running", true);
    }

    protected override void OnStop()
    {
        context.absController.AbsAnimator.SetBool("Running", false);
    }

    protected override State OnUpdate()
    {
        delayTimer += Time.deltaTime;
        if (delayTimer >= delayUpdatePlayerPosition)
        {
            context.absController.AbsMovement.Move(PlayerPublicInfor.Instance.Position, speed);
            delayTimer = 0;
        }


        if (context.agent.pathPending) return State.Running;
        if (context.agent.remainingDistance > context.agent.stoppingDistance) return State.Running;
        if (!context.agent.hasPath || context.agent.velocity.sqrMagnitude == 0f) return State.Success;

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return State.Failure;
        }

        return State.Running;
    }
}
