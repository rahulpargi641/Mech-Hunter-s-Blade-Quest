using System;
using UnityEngine;

public class PlayerHealthView : HealthView
{
    public PlayerHealthController Controller { get; set; }

    public void DamageVisual(Vector3 attackerPos = new Vector3())
    {
        PlayerVFXView playerVFXView = GetComponent<PlayerVFXView>();
        if (playerVFXView)
        {
            //playerVFXView.PlayBeingHitVFX(attackerPos);
            //playerVFXView.PlayBeingHitSplashVFX();
        }
        DamageBlinkEffect();
    }
}
