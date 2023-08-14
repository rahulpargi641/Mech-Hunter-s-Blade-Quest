using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : EnemyState
{
    private int patrolChance = 100;
    public EnemyIdle(EnemyView enemyAIView, NavMeshAgent navMeshAgent, Animator animator, Transform playerTransform)
           : base(enemyAIView, navMeshAgent, animator, playerTransform)
    {
        state = EState.Idle;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger("Idle");
    }

    protected override void Update()
    {
        base.Update();

        if (CanSeePlayer())
        {
            nextState = new EnemyPursue(enemyAIView, navMeshAgent, animator, playerTransform);
            stage = EStage.Exit;
        }
        else //if (Random.Range(0, 100) < patrolChance)
        {
            nextState = new EnemyPatrol(enemyAIView, navMeshAgent, animator, playerTransform);
            stage = EStage.Exit;
        }

        //if(isPlayerDead)
        //{
        //    nextState = new EnemyPatrol(enemyAIView, navMeshAgent, animator, playerTransform);
        //    stage = EStage.Exit;
        //}

    }

    protected override void Exit()
    {
        animator.ResetTrigger("Idle");
        base.Exit();
    }
}
