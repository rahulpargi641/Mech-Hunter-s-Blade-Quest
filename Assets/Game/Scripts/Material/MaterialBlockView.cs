using System.Collections;
using UnityEngine;

public class MaterialBlockView : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private MaterialPropertyBlock materialPropertyBlock;

    private float blinkDuration = 0.4f;

    private float dissolveTimeDuration = 2f;
    private float currentDissolveTime = 0f;
    private float dissolveHeightStart = 20f;
    private float dissolveHeightTarget = -10f;
    private float dissolveHeight;

    private void Awake()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
        skinnedMeshRenderer.GetPropertyBlock(materialPropertyBlock);
    }
    private void Start()
    {
        EventService.Instance.onEnemyDeathAction += CharacterDissolve;
    }

    private void OnDisable()
    {
        EventService.Instance.onEnemyDeathAction -= CharacterDissolve;
    }

    public void CharacterBlink()
    {
        StartCoroutine(MaterialBlink());
    }

    IEnumerator MaterialBlink()
    {
        materialPropertyBlock.SetFloat("_blink", blinkDuration);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);

        yield return new WaitForSeconds(0.2f);

        materialPropertyBlock.SetFloat("_blink", 0f);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    private void CharacterDissolve()
    {
        if(gameObject.GetComponent<EnemyView>())
        StartCoroutine(MaterialDissolve());
    }

    IEnumerator MaterialDissolve()
    {
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

        CollectibleService.Instance.DropItem(transform.position);
        gameObject.SetActive(false);
    }
}
