using UnityEngine;

public class Idle : PlayerState
{
    public Idle(PlayerView playerView, Animator animator) : base (playerView, animator)
    {
        state = EPlayerState.Idle;

        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        animator.SetTrigger("Idle");
        base.Enter();
    }

    protected override void Update()
    {
        base.Update();

        ProcessMovement();
        ProcessAttacking();
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Idle");
        base.Exit();
    }
}
