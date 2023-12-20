using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageOrb", menuName = "ScriptableObjects/DamageOrb")]
public class DamageOrbSO : ScriptableObject
{
    public float speed = 9f;
    public int damage = 10;
    public float hitForce = 10;
}
