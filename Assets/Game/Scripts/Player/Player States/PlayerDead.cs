using UnityEngine;

public class PlayerDead : PlayerState
{
    public PlayerDead(PlayerView playerView, PlayerSO player) : base(playerView, player)
    {
        state = EPlayerState.Dead;
        stage = EStage.Enter;
    }
    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger(player.deadAnimName);
    }

    protected override void Exit()
    {
        animator.ResetTrigger(player.deadAnimName);
        base.Exit();
    }
}
