using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyVFX : MonoBehaviour
{
    [SerializeField] VisualEffect footStep;
    [SerializeField] VisualEffect attackSmashVFX;
    [SerializeField] ParticleSystem beingHitVFX;
    [SerializeField] VisualEffect slash;

    [SerializeField] string onPlay = "OnPlay";

    private float waitDuration = 3f; // VFX returns to the pool after waitDuration
    private float splashVFXOffset = 2f; // splash vfx offset

    // called via animation event, when enemy walks
    public void BurstFootStep()
    {
        footStep.SendEvent(onPlay);
    }

    // called via animation event, when enemy attacks
    public void PlayAttackSmashVFX()
    {
        attackSmashVFX.SendEvent(onPlay);
    }

    // called when enemy is hit, enemy sliced vfx
    public void PlayEnemyHitVFX(Vector3 attackerPos)
    {
        Vector3 forceForwardDir = transform.position - attackerPos;
        forceForwardDir.Normalize();
        forceForwardDir.y = 0;

        beingHitVFX.transform.rotation = Quaternion.LookRotation(forceForwardDir);
        beingHitVFX.Play();
    }

    // called when enemy is hit, splashes oil on the ground
    public void PlayEnemyOilSplashVFX()
    {
        Vector3 splashPos = transform.position;
        splashPos.y += splashVFXOffset;

        VisualEffect enemyOilSplashVFX = VFXService.Instance.SpawnHitOilSplashVFX(splashPos);
        enemyOilSplashVFX.SendEvent(onPlay);

        StartCoroutine(ReturnVFX(enemyOilSplashVFX));
    }

    // called enemy is attacked by the player
    public void PlaySlashVFX(Vector3 pos)
    {
        slash.transform.position = pos;
        slash.Play();
    }

    IEnumerator ReturnVFX(VisualEffect enemyOilSplashVFX)
    {
        yield return new WaitForSeconds(waitDuration);
        VFXService.Instance.ReturnVFXToPool(enemyOilSplashVFX);
    }
}
