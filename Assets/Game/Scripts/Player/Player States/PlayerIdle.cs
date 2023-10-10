using UnityEngine;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(PlayerView playerView, PlayerSO player) : base (playerView, player)
    {
        state = EPlayerState.Idle;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger(player.idleAnimName);
    }

    protected override void Update()
    {
        base.Update();

        if (CanRoll())
        {
            nextState = new PlayerRoll(playerView, player);
            stage = EStage.Exit;
        }

        if (CanRun())
        {
            nextState = new PlayerRun(playerView, player);
            stage = EStage.Exit;
        }

        if(CanAttack())
        {
            nextState = new PlayerAttack(playerView, player);
            stage = EStage.Exit;
        }

        if(CanAttack2())
        {
            nextState = new SlideAttack(playerView, player);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Idle");
        base.Exit();
    }
}
