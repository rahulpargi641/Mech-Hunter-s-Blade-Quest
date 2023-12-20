using UnityEngine;

public class PlayerRoll : PlayerState
{
    public PlayerRoll(PlayerController controller) : base(controller)
    {
        state = EPlayerState.Run;
    }

    protected override void Enter()
    {
        base.Enter();
        animator.SetTrigger(controller.RollAnimName);

        controller.RollAnimationEnded = false; // set it false then roll the player over few frames, it will be true when roll animation ends
    }

    protected override void Update()
    {
        base.Update();

        PerformRolling();

        SwitchStateToIdleIf();
    }

    protected override void Exit()
    {
        animator.ResetTrigger(controller.RollAnimName); // always good practice to animation to prevent animation gliches
        base.Exit();
    }

    private void PerformRolling()
    {
        float rollSlideSpeed = controller.RollSlideSpeed; 
        Vector3 moveVelocity = controller.PlayerForwardVector * rollSlideSpeed * Time.deltaTime;
        controller.MovePlayer(moveVelocity);
    }

    protected void SwitchStateToIdleIf()
    {
        if (controller.RollAnimationEnded) // RollAnimationEnded will be set to true via Animation event in the PlayerView when animation ends
        {
            nextState = new PlayerIdle(controller);
            stage = EStage.Exit;
        }
    }
}
