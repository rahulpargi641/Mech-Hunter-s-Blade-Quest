using UnityEngine;

public class PlayerAttack : PlayerState
{
    // Combo attack
    private float attackStartTime;
    private float attackAnimDuration;
    private float minAnimWindow;
    private float maxAnimDuration;
    private int comboStep;

    public PlayerAttack(PlayerView playerView, PlayerSO player) : base(playerView, player)
    {
        state = EPlayerState.Attack;
        stage = EStage.Enter;

        minAnimWindow = player.minAnimWindow;
        maxAnimDuration = player.maxAnimWindow;
        comboStep = 1;
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

        if(CanAttack())
        {
            PerformAttackCombo();
        }

        if (playerView.AttackAnimationEnded)
        {
            nextState = new PlayerIdle(playerView, player);
            stage = EStage.Exit;
        }
    }

    private void PerformAttackCombo()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            string currentClipName = clipInfo[0].clip.name;
            attackAnimDuration = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (currentClipName != player.lastAttackComboClipName && attackAnimDuration > minAnimWindow && attackAnimDuration < maxAnimDuration)
            {
                comboStep++;
                if (comboStep > 3) comboStep = 2;

                string animationName = player.attackAnimName + comboStep.ToString();
                animator.SetTrigger(animationName);
            }
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger(player.attackAnimName);
        base.Exit();
    }
}
