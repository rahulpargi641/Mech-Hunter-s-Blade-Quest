using UnityEngine;

public class BeingHit : PlayerState
{
    public BeingHit(PlayerView playerView, Animator animator) : base(playerView, animator)
    {
        state = EPlayerState.BeingHit;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger("BeingHit");
        playerView.BeingHitAnimationEnded = false;
    }

    protected override void Update()
    {
        base.Update();

        PlayerService.Instance.HitImpactOnPlayer();

        if (playerView.BeingHitAnimationEnded)
        {
            if(CanRun())
            {
                nextState = new Run(playerView, animator);
                stage = EStage.Exit;
            }
            if(CanAttack())
            {
                nextState = new Attack(playerView, animator);
                stage = EStage.Exit;
            }

            if (CanAttack2())
            {
                nextState = new SlideAttack(playerView, animator);
                stage = EStage.Exit;
            }
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger("BeingHit");
        base.Exit();
    }
}
