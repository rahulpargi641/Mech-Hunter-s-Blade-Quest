using UnityEngine;

public class Attack : State
{
    public Attack(PlayerView playerView, Animator animator) : base(playerView, animator)
    {
        state = EPlayerState.Attack;
    }

    public override void Enter()
    {
        animator.SetTrigger("Attack");
        playerView.AttackSlide();
        //AudioService.Instance.PlayAttackSound
        base.Enter();
    }

    public override void Update()
    {
        if(playerView.AttackAnimationEnded)
        {
            playerView.AttackAnimationEnded = false;
            nextState = new Idle(playerView, animator);
            stage = EStage.Exit;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("Attack");
        base.Exit();
    }
}
