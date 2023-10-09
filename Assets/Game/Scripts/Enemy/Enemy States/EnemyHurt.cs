
public class EnemyHurt : EnemyState
{
    public EnemyHurt(EnemyView enemyAIView, EnemySO enemy) : base(enemyAIView, enemy)
    {
        state = EState.Idle;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger(enemy.hurtAnimName);
        enemyAIView.BeingHitAnimationEnded = false;
    }

    protected override void Update()
    {
        base.Update();

        if (enemyAIView.BeingHitAnimationEnded)
        {
            nextState = new EnemyIdle(enemyAIView, enemy);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger(enemy.hurtAnimName);
        base.Exit();
    }
}
