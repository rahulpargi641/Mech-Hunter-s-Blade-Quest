using UnityEngine;

public class Run : PlayerState
{
    public Run(PlayerView playerView, Animator animator) : base(playerView, animator)
    {
        state = EPlayerState.Run;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger("Run");
    }

    protected override void Update()
    {
        base.Update();

        playerView.Run();

        if (! CanRun())
        {
            nextState = new Idle(playerView, animator);
            stage = EStage.Exit;
        }

        if (CanRoll())
        {
            nextState = new Roll(playerView, animator);
            stage = EStage.Exit;
        }

        if (CanAttack())
        {
            nextState = new Attack(playerView, animator);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Run");
        base.Exit();
    }
}
