using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyVFX : MonoBehaviour
{
    [SerializeField] VisualEffect footStep;
    [SerializeField] VisualEffect attackSmashVFX;
    [SerializeField] ParticleSystem beingHitVFX;
    //[SerializeField] VisualEffect beingHitSplashVFX;

    [SerializeField] string onPlay = "OnPlay";

    private float waitDuration = 3f; // VFX returns to the pool after waitDuration
    private float yOffset = 2; // splash vfx offset

    public void BurstFootStep()
    {
        footStep.SendEvent(onPlay);
    }

    public void PlayAttackSmashVFX()
    {
        attackSmashVFX.SendEvent(onPlay);
    }

    public void PlayHitVFX(Vector3 attackerPos)
    {
        Vector3 forceForward = transform.position - attackerPos;
        forceForward.Normalize();
        forceForward.y = 0;
        beingHitVFX.transform.rotation = Quaternion.LookRotation(forceForward);
        beingHitVFX.Play();
    }

    public void PlayHitSplashVFX()
    {

        Vector3 splashPos = transform.position;
        splashPos.y += yOffset;

        //VisualEffect splashVFX = Instantiate(beingHitSplashVFX, splashPos, Quaternion.identity);
        VisualEffect enemyOilSplashVFX = VFXService.Instance.SpawnOilSplashVFX(splashPos);

        enemyOilSplashVFX.SendEvent(onPlay);

        StartCoroutine(ReturnVFX(enemyOilSplashVFX));
    }

    IEnumerator ReturnVFX(VisualEffect vfx)
    {
        yield return new WaitForSeconds(waitDuration);
        VFXService.Instance.ReturnVFXToPool(vfx);
    }
}
