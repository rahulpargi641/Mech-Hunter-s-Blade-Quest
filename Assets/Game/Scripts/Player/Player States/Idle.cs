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

    protected override void Exit()
    {
        animator.ResetTrigger("Idle");
        base.Exit();
    }
}
