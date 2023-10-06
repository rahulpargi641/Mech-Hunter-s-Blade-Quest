using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class VFXService : MonoSingletonGeneric<VFXService>
{
    [SerializeField] VisualEffect oilSplashVFX;
    [SerializeField] VisualEffect healVFX;

    private VFXPool vfxPool;

    // Start is called before the first frame update
    void Start()
    {
        vfxPool = new VFXPool();
    }

    public VisualEffect SpawnOilSplashVFX(Vector3 spawnPoint)
    {
        VisualEffect oilSplashVFX = vfxPool.GetHitSplashVFX(this.oilSplashVFX);
        oilSplashVFX.gameObject.SetActive(true);

        oilSplashVFX.transform.position = spawnPoint;


        return oilSplashVFX;
    }
    public VisualEffect SpawnHealVFX(Vector3 spawnPoint)
    {
        VisualEffect healVFX = vfxPool.GetHealVFX(this.healVFX);
        healVFX.gameObject.SetActive(true);

        healVFX.transform.position = spawnPoint;

        return healVFX;
    }

    public IEnumerator ReturnVFXToPool(VisualEffect vfx)
    {
        yield return new WaitForSeconds(3f);

        vfx.gameObject.SetActive(false);
        vfxPool.ReturnItem(vfx);
    }
}
