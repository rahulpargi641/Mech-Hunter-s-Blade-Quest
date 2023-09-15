using UnityEngine;

public class SlideAttack : PlayerState
{
    // Player Slides forwards while attacking
    private float attackStartTime;
    private float attackSlideDuration = 0.1f;
    private float attackSlideSpeed = 0.5f;
    public SlideAttack(PlayerView playerView, Animator animator) : base(playerView, animator)
    {
        state = EPlayerState.Attack;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger("Attack");
        playerView.AttackAnimationEnded = false;

        attackStartTime = Time.time;

        // Disabling player collider if not already disabled when performing attack
        DamageCasterView damageCaster = playerView.GetComponentInChildren<DamageCasterView>();
        if (damageCaster) damageCaster.DisableDamageCaster();

        //AudioService.Instance.PlayAttackSound
    }

    protected override void Update()
    {
        base.Update();

        PerformAttackSlide();

        if (playerView.AttackAnimationEnded)
        {
            nextState = new Idle(playerView, animator);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Attack");
        base.Exit();
    }

    // Player Slides forwards while performing attack
    private void PerformAttackSlide()
    {
        //model.MovementVelocity = Vector3.zero; // might have value from last frame
        playerView.CharacterController.Move(Vector3.zero);

        if (Time.time < attackStartTime + attackSlideDuration)
        {
            float timePassed = Time.time - attackStartTime;
            float lerpTime = timePassed / attackSlideDuration;
            Vector3 MovementVelocity = Vector3.Lerp(playerView.transform.forward * attackSlideSpeed, Vector3.zero, lerpTime);
            playerView.CharacterController.Move(MovementVelocity);
        }
    }
}
