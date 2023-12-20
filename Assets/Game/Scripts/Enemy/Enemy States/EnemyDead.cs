using UnityEngine;

public class EnemyDead : EnemyState
{
    public EnemyDead(EnemyController controller) : base(controller)
    {
        state = EEnemyState.Dead;
    }

    protected override void Enter()
    {
        base.Enter();
        animator.SetTrigger(controller.DeadAnimName);

        controller.CharacterMaterial?.StartCharacterDissolvingEffect();
    }

    protected override void Exit()
    {
        animator.ResetTrigger(controller.DeadAnimName);
        base.Exit();
    }
}
