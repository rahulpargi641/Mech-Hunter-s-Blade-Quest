using UnityEngine;

public class Roll : PlayerState
{
    private float slideSpeed = 7f;
    public Roll(PlayerView playerView, Animator animator) : base(playerView, animator)
    {
        state = EPlayerState.Run;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger("Roll");

        playerView.RollAnimationEnded = false;
    }

    protected override void Update()
    {
        base.Update();

        RollMovement();

        if(playerView.RollAnimationEnded)
        {
            nextState = new Idle(playerView, animator);
            stage = EStage.Exit;
        }

        //if (CanRun())
        //{
        //    nextState = new Run(playerView, animator);
        //    stage = EStage.Exit;
        //}

        //if (CanAttack())
        //{
        //    nextState = new Attack(playerView, animator);
        //    stage = EStage.Exit;
        //}
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Roll");
        base.Exit();
    }

    private void RollMovement()
    {
        Vector3 movementVelocity = playerView.transform.forward * slideSpeed * Time.deltaTime;
        playerView.CharacterController.Move(movementVelocity);
    }

}
