using UnityEngine;

public class PlayerHurt : PlayerState
{
    private Vector3 currentPushVelocity; // CurrentPushVelocity gets set when enemy hits the player in the PlayerController.
    public PlayerHurt(PlayerController controller) : base(controller)
    {
        state = EPlayerState.Hurt;

        currentPushVelocity = controller.CurrentPushVelocity; 
    }

    protected override void Enter()
    {
        base.Enter();
        animator.SetTrigger(controller.HurtAnimName);

        controller.HurtAnimationEnded = false;
        controller.IsHit = false;
    }

    protected override void Update()
    {
        base.Update();

        ApplyHitImpactForce();

        SwitchStateToIdleIf();
    }

    protected override void Exit()
    {
        animator.ResetTrigger(controller.HurtAnimName);
        base.Exit();
    }

    public void ApplyHitImpactForce() // Enemy Hit force pushes the player back
    {
        if (currentPushVelocity.magnitude > 0)
        {
            Vector3 moveVelocity = currentPushVelocity * Time.deltaTime;
            controller.MovePlayer(moveVelocity);
        }
        currentPushVelocity = Vector3.Lerp(currentPushVelocity, Vector3.zero, Time.deltaTime * 5);
    }

    private void SwitchStateToIdleIf()
    {
        if (controller.HurtAnimationEnded) // HurtAnimationEnded will be set to true via Animation event in the PlayerView when animation ends
        {
            nextState = new PlayerIdle(controller);
            stage = EStage.Exit;
        }
    }
}
