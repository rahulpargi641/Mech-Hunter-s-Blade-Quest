using UnityEngine;

public class Hurt : PlayerState
{
    public Hurt(PlayerView playerView, Animator animator) : base(playerView, animator)
    {
        state = EPlayerState.BeingHit;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger("Hurt");
        playerView.BeingHitAnimationEnded = false;
        AudioService.Instance.PlaySound(SoundType.Hurt);
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
        animator.ResetTrigger("Hurt");
        base.Exit();
    }
}
