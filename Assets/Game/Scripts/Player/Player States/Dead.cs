using UnityEngine;

public class Dead : PlayerState
{
    public Dead(PlayerView playerView, Animator animator) : base(playerView, animator)
    {
        state = EPlayerState.Dead;
        stage = EStage.Enter;
    }
    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger("Dead");
    }

    protected override void Exit()
    {
        //animator.ResetTrigger("Dead");
        //base.Exit();
    }
}
