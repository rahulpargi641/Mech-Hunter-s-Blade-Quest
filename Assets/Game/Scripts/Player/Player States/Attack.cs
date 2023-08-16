using UnityEngine;

public class Attack : PlayerState
{
    // Player Slides forwards while attacking
    private float attackStartTime;
    private float attackSlideDuration = 0.1f;
    private float attackSlideSpeed = 0.5f;

    // Combo attack
    private float attackAnimationDuration;
    private int comboStep = 1;

    public Attack(PlayerView playerView, Animator animator) : base(playerView, animator)
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

        //PerformAttackSlide();

        //if(CanPerformAttackCombo())
        //{
        //}
        if(CanAttack())
        {
            PerformAttackCombo();
        }

        if (CanRoll())
        {
            nextState = new Roll(playerView, animator);
            stage = EStage.Exit;
        }


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
    private bool CanPerformAttackCombo()
    {
        if (playerView.CharacterController.isGrounded)
            return true;
        else
            return false;
    }

    private void PerformAttackCombo()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            string currentClipName = clipInfo[0].clip.name;
            attackAnimationDuration = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (currentClipName != "LittleAdventurerAndie_ATTACK_03" && attackAnimationDuration > 0.35f && attackAnimationDuration < 0.7f)
            {
                comboStep++;
                if (comboStep > 3)
                {
                    comboStep = 2;
                }

                string animationName = "Attack" + comboStep.ToString();
                animator.SetTrigger(animationName);
                Debug.Log("Attack trigger name" + animationName);
            }
        }
        
    }
}
