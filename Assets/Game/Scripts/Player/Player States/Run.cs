using UnityEngine;

public class Run : State
{
    public Run(PlayerView playerView, Animator animator) : base(playerView, animator)
    {
        state = EPlayerState.Run;
    }

    public override void Enter()
    {
        animator.SetTrigger("Run");
        base.Enter();
    }

    public override void Update()
    {
        playerView.Run();

        if(playerView.HorizontalInput == 0 && playerView.VerticalInput == 0)
        {
            nextState = new Idle(playerView, animator);
            stage = EStage.Exit;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("Run");
        base.Exit();
    }
}
