using TheKiwiCoder;

public class MoveToPosition : ActionNode
{
    protected override void OnStart() {
        context.absController.AbsMovement.ResetMovement();
        context.absController.AbsMovement.Move(blackboard.moveToPosition, context.absController.AbsStat.GetMoveSpeed());
        context.absController.AbsAnimator.SetBool("Running", true);
    }

    protected override void OnStop() {
        context.absController.AbsAnimator.SetBool("Running", false);
    }

    protected override State OnUpdate() {
        if (context.agent.pathPending) return State.Running;
        if (context.agent.remainingDistance > context.agent.stoppingDistance) return State.Running;
        if (!context.agent.hasPath || context.agent.velocity.sqrMagnitude == 0f) return State.Success;
        return State.Running;
    }
}
