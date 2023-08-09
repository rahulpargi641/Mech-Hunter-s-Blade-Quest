using UnityEngine;

public class Idle : State
{
    public Idle(PlayerView playerView, Animator animator) : base (playerView, animator)
    {
        state = EPlayerState.Idle;
    }

    public override void Enter()
    {
        animator.SetTrigger("Idle");
        base.Enter();
    }

    public override void Update()
    {
        if (playerView.HorizontalInput != 0 || playerView.VerticalInput != 0)
        {
            nextState = new Run(playerView, animator);
            stage = EStage.Exit;
        }

        if (playerView.MouseButtonDown)
        {
            nextState = new Attack(playerView, animator);
            stage = EStage.Exit;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("Idle");
        base.Exit();
    }
}
