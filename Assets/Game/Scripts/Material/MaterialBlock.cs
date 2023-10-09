using System.Collections;
using UnityEngine;

public class MaterialBlock : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private MaterialPropertyBlock materialPropertyBlock;

    private float blinkResetDuration = 0.2f; 

    private void Awake()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
        skinnedMeshRenderer.GetPropertyBlock(materialPropertyBlock);
    }

    private void OnEnable()
    {
        EventService.Instance.onEnemyDeathAction += CharacterDissolveEffect;
    }

    private void OnDisable()
    {
        EventService.Instance.onEnemyDeathAction -= CharacterDissolveEffect;
    }

    public void CharacterBlinkEffect()
    {
        StartCoroutine(MaterialBlink());
    }

    IEnumerator MaterialBlink()
    {
        float blinkDuration = 0.4f;

        materialPropertyBlock.SetFloat("_blink", blinkDuration);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);

        yield return new WaitForSeconds(blinkResetDuration);

        materialPropertyBlock.SetFloat("_blink", 0f);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    public void CharacterDissolveEffect(EnemyView enemyView)
    {
        if(GetComponent<EnemyView>() == enemyView)
            StartCoroutine(MaterialDissolve(enemyView));
    }

    IEnumerator MaterialDissolve(EnemyView enemyView)
    {
        float dissolveTimeDuration = 2f;
        float currentDissolveTime = 0f;
        float dissolveHeightStart = 20f;
        float dissolveHeightTarget = -10f;
        float dissolveHeight;

        yield return new WaitForSeconds(2f); 

        materialPropertyBlock.SetFloat("_enableDissolve", 1f);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);

        while(currentDissolveTime < dissolveTimeDuration)
        {
            currentDissolveTime += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(dissolveHeightStart, dissolveHeightTarget, currentDissolveTime / dissolveTimeDuration);
            
            materialPropertyBlock.SetFloat("_dissolve_height", dissolveHeight);
            skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);

            yield return null;
        }

        PickupsService.Instance.SpawnPickup(transform.position);
        EnemyService.Instance.EnemyDissolved(enemyView);
        //gameObject.SetActive(false);
    }

    public void CharacterAppearEffect()
    {
        StartCoroutine(MaterialAppear());
    }

    IEnumerator MaterialAppear()
    {
        float dissolveTime_duration = 3f;
        float currentDissolve_time = 0f;
        float dissolveHeight_start = -10f;
        float dissolveHeight_target = 20f;
        float dissolveHeight;

        materialPropertyBlock.SetFloat("_enableDissolve", 1f);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);

        while(currentDissolve_time < dissolveTime_duration)
        {
            currentDissolve_time += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(dissolveHeight_start, dissolveHeight_target, currentDissolve_time / dissolveTime_duration);
            materialPropertyBlock.SetFloat("_dissolve_height", dissolveHeight);
            skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
            yield return null;
        }

        materialPropertyBlock.SetFloat("_enableDissolve", 0f);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
