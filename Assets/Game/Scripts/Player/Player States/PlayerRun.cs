using UnityEngine;

public class PlayerRun : PlayerState
{
    public PlayerRun(PlayerView playerView, PlayerSO player) : base(playerView, player)
    {
        base.state = PlayerState.EPlayerState.Run;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger(player.runAnimName);
    }

    protected override void Update()
    {
        base.Update();

        playerView.Run();

        if (!CanRun())
        {
            nextState = new PlayerIdle(playerView, player);
            stage = EStage.Exit;
        }

        if (CanRoll())
        {
            nextState = new PlayerRoll(playerView, player);
            stage = EStage.Exit;
        }

        if (CanAttack())
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

    protected override void Exit()
    {
        animator.ResetTrigger(player.runAnimName);
        base.Exit();
    }
}
