using UnityEngine;

public class PlayerDead : PlayerState
{
    public PlayerDead(PlayerController controller) : base(controller)
    {
        state = EPlayerState.Dead;
    }
    protected override void Enter()
    {
        base.Enter();
        animator.SetTrigger(controller.DeadAnimName);

        controller.CanEnterDeadState = false;
    }

    protected override void Update()
    {
        base.Update();

        //SwitchStateToIdleIf(); // Player Respawns
    }

    protected override void Exit()
    {
        animator.ResetTrigger(controller.DeadAnimName);
        base.Exit();
    }

    private void SwitchStateToIdleIf()
    {
        // if Respawn conditions met
        nextState = new PlayerIdle(controller);
        stage = EStage.Exit;
    }
}
