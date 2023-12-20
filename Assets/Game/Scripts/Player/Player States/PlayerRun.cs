using UnityEngine;

public class PlayerRun : PlayerState
{
    private Vector3 moveVelocity = new Vector3();

    public PlayerRun(PlayerController controller) : base(controller)
    { 
        state = EPlayerState.Run;
    }

    protected override void Enter()
    {
        base.Enter();
        animator.SetTrigger(controller.RunAnimName);
    }

    protected override void Update()
    {
        base.Update();

        ProcessMovement();

        SwitchStateIfConditionsMet();
    }

    protected override void Exit()
    {
        animator.ResetTrigger(controller.RunAnimName);
        base.Exit();
    }

    public void ProcessMovement()
    {
        Run();
        Rotate(); // Rotate player in the direction of velocity i.e current player movement input
        DescendStairs();
        //InAir();
        controller.MovePlayer(moveVelocity);
    }

    private void Run() // XZ plane movement
    {
        moveVelocity.Set(controller.HorizontalInput, 0f, controller.VerticalInput);
        moveVelocity.Normalize();
        moveVelocity = Quaternion.Euler(0f, -45f, 0f) * moveVelocity;

        moveVelocity *= controller.MoveSpeed * Time.deltaTime;
    }

    private void Rotate() 
    {
        if (moveVelocity != Vector3.zero)
            controller.RotatePlayer(moveVelocity);
    }

    // Adjusts the vertical movement of the player when descending stairs to simulate realistic gravity effects.
    private void DescendStairs() 
    {
        float speedY = (controller.IsGrounded) ? controller.FallGravity * 0.3f : controller.FallGravity; // Determine the vertical speed based on the player's grounded state

        moveVelocity += speedY * Vector3.up * Time.deltaTime;
    }

    protected void SwitchStateIfConditionsMet()
    {
        SwitchStateToIdleIf();
        SwitchStateToRollIf();
        SwitchStateToAttackIf();
        SwitchStateToDashAttackIf();
    }

    private void SwitchStateToIdleIf()
    {
        if (!RunButtonDown)
        {
            nextState = new PlayerIdle(controller);
            stage = EStage.Exit;
        }
    }

    private void SwitchStateToRollIf()
    {
        if (RollButtonDown)
        {
            nextState = new PlayerRoll(controller);
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
