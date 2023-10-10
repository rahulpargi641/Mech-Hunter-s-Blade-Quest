using UnityEngine;

public class EnemyIdle : EnemyState
{
    private int patrolChance;

    public EnemyIdle(EnemyView enemyAIView, EnemySO enemy) : base(enemyAIView, enemy)
    {
        state = EState.Idle;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        patrolChance = enemy.patrolChance;

        animator.SetTrigger(enemy.idleAnimName);
    }

    protected override void Update()
    {
        base.Update();

        if (CanSeePlayer())
        {
            nextState = new EnemyPursue(enemyAIView, enemy);
            stage = EStage.Exit;
        }
        else if (Random.Range(0, 100) < patrolChance)
        {
            nextState = new EnemyPatrol(enemyAIView, enemy);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger(enemy.idleAnimName);
        base.Exit();
    }
}
