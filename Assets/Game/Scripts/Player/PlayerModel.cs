using UnityEngine;

public class PlayerModel
{
    // Health Related
    public int MaxHealth { get; }
    public int CurrentHealth { get; set; }
    public float CurrentHealthPercent { get { return CurrentHealth / (float)MaxHealth; } }

    // Movement Related
    public float MoveSpeed { get; }
    public float RollSlideSpeed { get; }
    public float FallGravity { get; }
    public Vector3 CurrentPushVelocity { get; set; } = new Vector3();

    // State related
    public bool IsHit { get; set; } = false;
    public bool IsDead { get; set; } = false;

    // Attack Combo related
    public float MinComboWindow { get; } // Min window to perform attack combo
    public float MaxComboWindow { get; } // Max window to perform attack combo
    public float DashAttackSlideDuration { get; }
    public float DashAttackSlideSpeed { get; }

    // Animation names
    public string IdleAnimName { get; }
    public string RunAnimName { get; }
    public string RollAnimName { get; }
    public string HurtAnimName { get; }
    public string AttackAnimName { get; }
    public string LastAttackInComboAnimName { get; }
    public string DeadAnimName { get; }

    public PlayerController Controller { private get; set; }

    public PlayerModel(PlayerSO playerSO)
    {
        MaxHealth = playerSO.MaxHealth;
        CurrentHealth = MaxHealth;

        MoveSpeed = playerSO.moveSpeed;
        RollSlideSpeed = playerSO.rollSlideSpeed;
        FallGravity = playerSO.fallGravity;

        MinComboWindow = playerSO.minComboWindow;
        MaxComboWindow = playerSO.maxComboWindow;
        DashAttackSlideDuration = playerSO.attackSlideDuration;
        DashAttackSlideSpeed = playerSO.attackSlideSpeed;

        IdleAnimName = playerSO.idleAnimName;
        RunAnimName = playerSO.runAnimName;
        RollAnimName = playerSO.rollAnimName;
        HurtAnimName = playerSO.hurtAnimName;
        AttackAnimName = playerSO.attackAnimName;
        LastAttackInComboAnimName = playerSO.lastAttackComboAnimName;
        DeadAnimName = playerSO.deadAnimName;
    }
}
