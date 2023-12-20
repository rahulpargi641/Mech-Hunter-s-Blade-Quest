using UnityEngine;

public class EnemyIdle : EnemyState
{
    private readonly int patrolChance;

    public EnemyIdle(EnemyController controller) : base(controller)
    {
        state = EEnemyState.Idle;

        patrolChance = controller.PatrolChance;
    }

    protected override void Enter()
    {
        base.Enter();
        animator.SetTrigger(controller.IdleAnimName);
    }

    protected override void Update()
    {
        base.Update();

        SwitchStateIf();
    }

    protected override void Exit()
    {
        animator.ResetTrigger(controller.IdleAnimName);
        base.Exit();
    }

    private void SwitchStateIf()
    {
        if (CanSeePlayer())
        {
            nextState = new EnemyPursue(controller);
            stage = EStage.Exit;
        }
        else if (Random.Range(0, 100) < patrolChance)
        {
            nextState = new EnemyPatrol(controller);
            stage = EStage.Exit;
        }
    }
}
