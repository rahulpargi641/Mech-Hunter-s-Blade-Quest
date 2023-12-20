using UnityEngine;

public class PlayerAttack : PlayerState
{
    private float minComboWindow;
    private float maxComboWindow;

    private int comboStepNo;
    private const int MaxComboSteps = 3;
    private const int ResetComboStep = 2;

    public PlayerAttack(PlayerController controller) : base(controller)
    {
        state = EPlayerState.Attack;
    }

    protected override void Enter()
    {
        base.Enter();
        animator.SetTrigger(controller.AttackAnimName);

        InitializeCombo();
    }

    protected override void Update()
    {
        base.Update();

        PerformComboIfValid();

        SwitchStateToIdleIf();
    }

    protected override void Exit()
    {
        animator.ResetTrigger(controller.AttackAnimName);
        base.Exit();
    }

    private void InitializeCombo()
    {
        comboStepNo = 1;
        minComboWindow = controller.MinComboWindow;
        maxComboWindow = controller.MaxAnimWindow;

        controller.AttackAnimationEnded = false;
        controller.PlayerDamageCaster?.DisableDamageCaster();
    }

    private void PerformComboIfValid()
    {
        if (AttackButtonDown && IsComboWindowValid())
        {
            UpdateComboStep();
            TriggerComboAnimation();
        }
    }

    private bool IsComboWindowValid()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            string currentClipName = clipInfo[0].clip.name;
            float attackAnimDuration = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            return currentClipName != controller.LastAttackInComboAnimName &&
                   attackAnimDuration > minComboWindow &&
                   attackAnimDuration < maxComboWindow;
        }

        return false;
    }

    private void UpdateComboStep()
    {
        comboStepNo++;
        if (comboStepNo > MaxComboSteps) comboStepNo = ResetComboStep; // Can only perform 3 step combo
    }

    private void TriggerComboAnimation()
    {
        string animationName = controller.AttackAnimName + comboStepNo.ToString();
        animator.SetTrigger(animationName);
    }

    private void SwitchStateToIdleIf() // if Attack animation ends
    {
        if (controller.AttackAnimationEnded) // AttackAnimationEnded will be set to true via Animation event in the PlayerView when animation ends
        {
            nextState = new PlayerIdle(controller);
            stage = EStage.Exit;
        }
    }
}
