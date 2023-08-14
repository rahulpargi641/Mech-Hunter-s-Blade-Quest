using UnityEngine;
using UnityEngine.VFX;

public class EnemyVFXView : MonoBehaviour
{
    [SerializeField] VisualEffect footStep;
    [SerializeField] VisualEffect attackSmashVFX;
    [SerializeField] VisualEffect beingHitSplashVFX;
    [SerializeField] ParticleSystem beingHitVFX;

    public void BurstFootStep()
    {
        footStep.SendEvent("OnPlay");
    }

    public void PlayAttackSmashVFX()
    {
        attackSmashVFX.SendEvent("OnPlay");
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
        Vector3 splashPos = transform.position;
        splashPos.y += 2;
        VisualEffect splashVFX = Instantiate(beingHitSplashVFX, splashPos, Quaternion.identity);
        splashVFX.SendEvent("OnPlay");
        Destroy(splashVFX.gameObject, 10f);
    }
}
