using UnityEngine;
using TheKiwiCoder;
using Player;
using UnityEngine.AI;

public class FindShootingPosition : ActionNode
{
    public float safeRangeShoot;

    private NavMeshHit hit;

    protected override void OnStart() {
        return;
    }

    protected override void OnStop() {
        return;
    }

    protected override State OnUpdate()
    {
        Vector3 playerPosition = PlayerPublicInfor.Instance.Position;
        Vector3 direction = (playerPosition - context.transform.position).normalized;
        Vector3 targetPosition = playerPosition - direction * safeRangeShoot;

        if (NavMesh.SamplePosition(targetPosition, out hit, safeRangeShoot, NavMesh.AllAreas))
        {
            Vector3 navHitPosition = hit.position;
            Vector3 raycastDirection = (playerPosition - navHitPosition).normalized;

            if (!Physics.Raycast(navHitPosition, raycastDirection, Vector3.Distance(navHitPosition, playerPosition), 1 << LayerMask.NameToLayer("Wall")))
            {
                blackboard.moveToPosition = navHitPosition;
                return State.Success;
            }
        }

        return State.Running;
    }
}
