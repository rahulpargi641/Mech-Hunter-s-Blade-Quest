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
        base.Enter();

        animator.SetTrigger("Idle");
    }

    protected override void Update()
    {
        base.Update();

        if (CanRoll())
        {
            nextState = new Roll(playerView, animator);
            stage = EStage.Exit;
        }

        if (CanRun())
        {
            nextState = new Run(playerView, animator);
            stage = EStage.Exit;
        }

        if(CanAttack())
        {
            nextState = new Attack(playerView, animator);
            stage = EStage.Exit;
        }

        if(CanAttack2())
        {
            nextState = new SlideAttack(playerView, animator);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Idle");
        base.Exit();
    }
}
