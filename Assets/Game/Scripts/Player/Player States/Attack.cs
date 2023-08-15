using UnityEngine;

public class Attack : PlayerState
{
    public Attack(PlayerView playerView, Animator animator) : base(playerView, animator)
    {
        state = EPlayerState.Attack;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger("Attack");
        playerView.AttackSlide();

        DamageCasterView damageCaster = playerView.GetComponentInChildren<DamageCasterView>();
        if (damageCaster)
            damageCaster.DisableDamageCaster();

        //AudioService.Instance.PlayAttackSound
    }

    protected override void Update()
    {
        base.Update();

        if(playerView.AttackAnimationEnded)
        {
            playerView.AttackAnimationEnded = false;
            nextState = new Idle(playerView, animator);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Attack");
        base.Exit();
    }
}
