using UnityEngine;
using UnityEngine.AI;

public class EnemyPursue : EnemyState
{
    private readonly float pathUpdateDelay;
    private float pathUpdateDeadline;

    public EnemyPursue(EnemyController controller) : base(controller)
    {
        state = EEnemyState.Pursue;

        pathUpdateDelay = controller.PathUpdateDelay;

        navMeshAgent.speed = 1.5f;
        navMeshAgent.isStopped = false;
    }

    protected override void Enter()
    {
        base.Enter();
        animator.SetTrigger(controller.RunAnimName);
    }

    protected override void Update()
    {
        base.Update();

        if (playerTransform.position == null) return;

        SetDestination(playerTransform.position);
        FollowPlayerIfPathExists();
    }

    protected override void Exit()
    {
        animator.ResetTrigger(controller.RunAnimName);
        base.Exit();
    }

    private void SetDestination(Vector3 targetPoint)
    {
        if (Time.time >= pathUpdateDeadline)
        {
            pathUpdateDeadline = Time.time + pathUpdateDelay;  // Update the deadline for the next path update
            navMeshAgent.SetDestination(targetPoint);
        }
    }

    private void FollowPlayerIfPathExists()
    {
        if (navMeshAgent.hasPath) // means currently pursuing the player by following a path      
        {
            SwitchStateIf();
        }
    }

    private void SwitchStateIf()
    {
        if (CanAttackPlayer())
        {
            nextState = new EnemyAttack(controller);
            stage = EStage.Exit;
        }
        else if (!CanSeePlayer() || HasReachedDestination())
        {
            nextState = new EnemyPatrol(controller);
            stage = EStage.Exit;
        }
    }

    private bool HasReachedDestination()
    {
        return !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
    }
}
