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
    }

    protected override void Update()
    {
        base.Update();

        if (playerView.BeingHitAnimationEnded)
        {
            Debug.Log("Value of Being Hit inside" + playerView.BeingHitAnimationEnded);
            ProcessMovement();
            ProcessAttacking();
            playerView.BeingHitAnimationEnded = false;
        }
        else
            PlayerService.Instance.ProcessHitImpact();
    }


    protected override void Exit()
    {
        animator.ResetTrigger("BeingHit");
        base.Exit();
    }
}
