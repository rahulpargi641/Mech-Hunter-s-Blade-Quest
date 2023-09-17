using UnityEngine;

public class Attack : PlayerState
{
    // Combo attack
    private float attackStartTime;
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

        if(CanAttack())
        {
            PerformAttackCombo();
        }

        if (playerView.AttackAnimationEnded)
        {
            nextState = new Idle(playerView, animator);
            stage = EStage.Exit;
        }
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
                //Debug.Log("Attack trigger name" + animationName);
            }
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Attack");
        base.Exit();
    }
}
