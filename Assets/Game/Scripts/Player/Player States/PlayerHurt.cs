using UnityEngine;

public class PlayerHurt : PlayerState
{
    public PlayerHurt(PlayerView playerView, PlayerSO player) : base(playerView, player)
    {
        state = EPlayerState.BeingHit;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger(player.hurtAnimName);
        playerView.BeingHitAnimationEnded = false;
    }

    protected override void Update()
    {
        base.Update();

        PlayerService.Instance.ApplyHitImpactForce();

        if (playerView.BeingHitAnimationEnded)
        {
            if(CanRun())
            {
                nextState = new PlayerRun(playerView, player);
                stage = EStage.Exit;
            }
            if(CanAttack())
            {
                nextState = new PlayerAttack(playerView, player);
                stage = EStage.Exit;
            }

            if (CanAttack2())
            {
                nextState = new SlideAttack(playerView, player);
                stage = EStage.Exit;
            }
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger(player.hurtAnimName);
        base.Exit();
    }
}
