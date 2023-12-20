using UnityEngine;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(PlayerController controller) : base (controller)
    {
        state = EPlayerState.Idle;
    }

    protected override void Enter()
    {
        base.Enter();
        animator.SetTrigger(controller.IdleAnimName);
    }

    protected override void Update()
    {
        base.Update();

        SwitchStateIfConditionsMet();
    }

    protected override void Exit()
    {
        animator.ResetTrigger(controller.IdleAnimName);
        base.Exit();
    }

    protected void SwitchStateIfConditionsMet()
    {
        SwitchStateToRollIf();
        SwitchStateToRunIf();
        SwitchStateToAttackIf();
        SwitchStateToDashAttackIf();
    }

    private void SwitchStateToRollIf()
    {
        if (RollButtonDown)
        {
            nextState = new PlayerRoll(controller);
            stage = EStage.Exit;
        }
    }

    private void SwitchStateToRunIf()
    {
        if (RunButtonDown)
        {
            nextState = new PlayerRun(controller);
            stage = EStage.Exit;
        }
    }

    private void SwitchStateToAttackIf()
    {
        if (AttackButtonDown)
        {
            nextState = new PlayerAttack(controller);
            stage = EStage.Exit;
        }
    }

    private void SwitchStateToDashAttackIf()
    {
        if (DashAttackButtonDown)
        {
            nextState = new PlayerDashAttack(controller);
            stage = EStage.Exit;
        }
    }
}
