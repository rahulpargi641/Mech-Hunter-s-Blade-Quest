using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "ScriptableObjects/Player")]
public class PlayerSO : ScriptableObject
{
    [Header("Player Info")]
    public new string name;
    public PlayerView playerView;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rollSlideSpeed = 7f;
    public float fallGravity = -20;

    [Header("Attack Combo")]
    public float minAnimWindow = 0.35f;
    public float maxAnimWindow = 0.7f;

    [Header("Slide Attack")]
    public float attackSlideDuration = 0.1f;
    public float attackSlideSpeed = 0.5f;

    [Header("Animation Names")]
    public string idleAnimName = "Idle";
    public string runAnimName = "Run";
    public string rollAnimName = "Roll";
    public string hurtAnimName = "Hurt";
    public string attackAnimName = "Attack";
    public string lastAttackComboClipName = "LittleAdventurerAndie_ATTACK_03";
    public string deadAnimName = "Dead";

    [Header("Properties")]
    public ItemType itemType; // Enum to specify the item type (e.g., Consumable, Weapon, Armor, etc.).
}


// Enum to specify different item types.
public enum ItemType
{
    Consumable,
    Damaging,
}
