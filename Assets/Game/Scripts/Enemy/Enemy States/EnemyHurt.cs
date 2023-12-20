using UnityEngine;

public class EnemyHurt : EnemyState
{
    public EnemyHurt(EnemyController controller) : base(controller)
    {
        state = EEnemyState.Idle;
    }

    protected override void Enter()
    {
        base.Enter();
        animator.SetTrigger(controller.HurtAnimName);

        controller.HurtAnimationEnded = false;
        controller.CharacterMaterial?.PlayHurtBlinkEffect();
    }

    protected override void Update()
    {
        base.Update();

        SwitchStateToIdleIf();
    }

    protected override void Exit()
    {
        animator.ResetTrigger(controller.HurtAnimName); 
        base.Exit();
    }

    private void SwitchStateToIdleIf()
    {
        if (controller.HurtAnimationEnded) // HurtAnimationEnded will be set to true in the EnemyView when Hurt animation ends via animation event
        {
            nextState = new EnemyIdle(controller);
            stage = EStage.Exit;
        }
    }
}
