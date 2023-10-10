using UnityEngine;
using UnityEngine.AI;

public class EnemyPursue : EnemyState
{
    public EnemyPursue(EnemyView enemyAIView, EnemySO enemy) : base(enemyAIView, enemy)
    {
        state = EState.Pursue;
        stage = EStage.Enter;

        navMeshAgent.speed = 1.5f;
        navMeshAgent.isStopped = false;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger(enemy.runAnimName);
    }

    protected override void Update()
    {
        base.Update();

        UpdatePath(playerTransform.position);

        if (navMeshAgent.hasPath) // means following the player       
        {
            if (CanAttackPlayer())
            {
                nextState = new EnemyAttack(enemyAIView, enemy);
                stage = EStage.Exit;
            }
            else if (! CanSeePlayer() || !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                nextState = new EnemyPatrol(enemyAIView, enemy);
                stage = EStage.Exit;
            }
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger(enemy.runAnimName);
        base.Exit();
    }
}
