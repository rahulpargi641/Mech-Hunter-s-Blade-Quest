using UnityEngine;

public class PlayerRoll : PlayerState
{
    private float rollSlideSpeed = 7f;
    public PlayerRoll(PlayerView playerView, PlayerSO player) : base(playerView, player)
    {
        state = EPlayerState.Run;
        stage = EStage.Enter;

        rollSlideSpeed = player.rollSlideSpeed;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger(player.rollAnimName);

        playerView.RollAnimationEnded = false;
    }

    protected override void Update()
    {
        base.Update();

        RollMovement();

        if(playerView.RollAnimationEnded)
        {
            nextState = new PlayerIdle(playerView, player);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger(player.rollAnimName);
        base.Exit();
    }

    private void RollMovement()
    {
        Vector3 movementVelocity = playerView.transform.forward * rollSlideSpeed * Time.deltaTime;
        playerView.CharacterController.Move(movementVelocity);
    }
}
