using UnityEngine;

public class PlayerHealthView : HealthView
{
    public override void ApplyDamage(int damage, Vector3 attackerPos = default)
    {
        base.ApplyDamage(damage, attackerPos);

        PlayerVFXView playerVFXView = GetComponent<PlayerVFXView>();
        if (playerVFXView)
        {

        }
    }

}
