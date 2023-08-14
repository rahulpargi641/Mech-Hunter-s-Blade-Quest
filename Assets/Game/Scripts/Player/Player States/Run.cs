using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : PlayerState
{
    public Run(PlayerView playerView, Animator animator) : base(playerView, animator)
    {
        state = PlayerState.EPlayerState.Run;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        animator.SetTrigger("Run");
        base.Enter();
    }

    protected override void Update()
    {
        base.Update();

        playerView.Run();

        if (playerView.HorizontalInput == 0 && playerView.VerticalInput == 0)
        {
            nextState = new Idle(playerView, animator);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Run");
        base.Exit();
    }
}
