using UnityEngine;

public class PlayerDashAttack : PlayerState
{
    private float attackStartTime;
    private float attackSlideDuration; 
    private float attackSlideSpeed; 

    public PlayerDashAttack(PlayerController controller) : base(controller)
    {
        state = EPlayerState.Attack;

        attackSlideDuration = controller.DashAttackSlideDuration;
        attackSlideSpeed = controller.DashAttackSlideSpeed;
    }

    protected override void Enter()
    {
        base.Enter();
        animator.SetTrigger(controller.AttackAnimName);

        controller.AttackAnimationEnded = false;
        controller.PlayerDamageCaster?.DisableDamageCaster(); // Disabling player collider if not already disabled when performing attack
        
        attackStartTime = Time.time;
    }

    protected override void Update()
    {
        base.Update();

        PerformDashAttack();

        SwitchStateToIdleIf();
    }

    protected override void Exit()
    {
        animator.ResetTrigger(controller.AttackAnimName);
        base.Exit();
    }

    // Player slides forward while performing dash attack
    private void PerformDashAttack()
    {
        if (Time.time < attackStartTime + attackSlideDuration)
        {
            float timePassed = Time.time - attackStartTime;
            float lerpTime = timePassed / attackSlideDuration;
            Vector3 MoveVelocity = Vector3.Lerp(controller.PlayerForwardVector * attackSlideSpeed, Vector3.zero, lerpTime);
            controller.MovePlayer(MoveVelocity);
        }
    }

    private void SwitchStateToIdleIf() // Attack Animation ends
    {
        if (controller.AttackAnimationEnded) // AttackAnimationEnded will be set to true via Animation event in the PlayerView when animation ends
    {
            nextState = new PlayerIdle(controller);
            stage = EStage.Exit;
        }
    }
}
