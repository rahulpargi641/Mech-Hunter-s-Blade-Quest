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

    private VFXPool VFXPool;

    private float waitDuration = 3f; // VFX returns to the pool after waitDuration
    private float yOffset = 2; // splash vfx offset

    private void Start()
    {
        VFXPool = GetComponent<VFXPool>();
    }

    public void BurstFootStep()
    {
        footStep.SendEvent(onPlay);
    }

    public void PlayAttackSmashVFX()
    {
        attackSmashVFX.SendEvent(onPlay);
    }

    public void PlayBeingHitVFX(Vector3 attackerPos)
    {
        Vector3 forceForward = transform.position - attackerPos;
        forceForward.Normalize();
        forceForward.y = 0;
        beingHitVFX.transform.rotation = Quaternion.LookRotation(forceForward);
        beingHitVFX.Play();
    }

    public void PlayBeingHitSplashVFX()
    {
        //VisualEffect splashVFX = Instantiate(beingHitSplashVFX, splashPos, Quaternion.identity);
        VisualEffect enemyOilSplashVFX = VFXPool.GetHitSplashVFX();
        enemyOilSplashVFX.gameObject.SetActive(true);

        Vector3 splashPos = transform.position;
        splashPos.y += yOffset;
        enemyOilSplashVFX.transform.position = splashPos;
        enemyOilSplashVFX.transform.rotation = Quaternion.identity;
        enemyOilSplashVFX.SendEvent(onPlay);

        StartCoroutine(ReturnVFX(enemyOilSplashVFX));
    }

     IEnumerator ReturnVFX(VisualEffect vfx)
    {
        yield return new WaitForSeconds(waitDuration);
        ReturnPickupToPool(vfx);

    }
    public void ReturnPickupToPool(VisualEffect vfx)
    {
        vfx.gameObject.SetActive(false);
        VFXPool.ReturnItem(vfx);
    }
}
