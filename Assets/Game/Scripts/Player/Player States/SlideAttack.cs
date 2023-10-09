using UnityEngine;

public class SlideAttack : PlayerState
{
    // Player Slides forwards while attacking
    private float attackStartTime;
    private float attackSlideDuration = 0.1f;
    private float attackSlideSpeed = 0.5f;
    public SlideAttack(PlayerView playerView, PlayerSO player) : base(playerView, player)
    {
        state = EPlayerState.Attack;
        stage = EStage.Enter;

        attackSlideDuration = player.attackSlideDuration;
        attackSlideSpeed = player.attackSlideSpeed;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger(player.attackAnimName);
        playerView.AttackAnimationEnded = false;

        attackStartTime = Time.time;

        // Disabling player collider if not already disabled when performing attack
        DamageCasterPresenter damageCaster = playerView.GetComponentInChildren<DamageCasterPresenter>();
        if (damageCaster) damageCaster.DisableDamageCaster();

        //AudioService.Instance.PlayAttackSound
    }

    protected override void Update()
    {
        base.Update();

        PerformSlideAttack();

        if (playerView.AttackAnimationEnded)
        {
            nextState = new PlayerIdle(playerView, player);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger(player.attackAnimName);
        base.Exit();
    }

    // Player Slides forwards while performing attack
    private void PerformSlideAttack()
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
