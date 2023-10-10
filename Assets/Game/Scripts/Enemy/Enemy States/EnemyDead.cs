
public class EnemyDead : EnemyState
{
    public EnemyDead(EnemyView enemyAIView, EnemySO enemy) : base(enemyAIView, enemy)
    {
        state = EState.Dead;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger(enemy.deadAnimName);
    }

    protected override void Exit()
    {
        base.Exit();
    }
}
