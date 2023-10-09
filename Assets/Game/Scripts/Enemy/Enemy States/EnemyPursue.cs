using UnityEngine;
using UnityEngine.AI;

public class EnemyPursue : EnemyState
{
    public EnemyPursue(EnemyView enemyAIView, NavMeshAgent navMeshAgent, Animator animator, Transform playerTransform)
     : base(enemyAIView, navMeshAgent, animator, playerTransform)
    {
        state = EState.Pursue;
        stage = EStage.Enter;

        navMeshAgent.speed = 1.5f;
        navMeshAgent.isStopped = false;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger("Run");
    }

    protected override void Update()
    {
        base.Update();

        UpdatePath(playerTransform.position);

        if (navMeshAgent.hasPath) // means following the player       
        {
            if (CanAttackPlayer())
            {
                nextState = new EnemyAttack(enemyAIView, navMeshAgent, animator, playerTransform);
                stage = EStage.Exit;
            }
            else if (! CanSeePlayer() || !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                nextState = new EnemyPatrol(enemyAIView, navMeshAgent, animator, playerTransform);
                stage = EStage.Exit;
            }
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Run");
        base.Exit();
    }
}
