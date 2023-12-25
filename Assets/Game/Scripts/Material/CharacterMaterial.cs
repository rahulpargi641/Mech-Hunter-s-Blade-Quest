using System.Collections;
using UnityEngine;

public class CharacterMaterial : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private MaterialPropertyBlock materialPropertyBlock;

    private float blinkResetDuration = 0.2f;
    private float dissolveStartHeight = 20f;
    private float dissolveTargetHeight = -10f;
    private float dissolveDuration = 2f;
    private float appearDuration = 3f;
    private float appearStartHeight = -10f;
    private float appearTargetHeight = 20f;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
        skinnedMeshRenderer.GetPropertyBlock(materialPropertyBlock);
    }

    private void SetMaterialProperty(string propertyName, float value)
    {
        materialPropertyBlock.SetFloat(propertyName, value);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    private IEnumerator ChangeMaterialHeightOverTime(float duration, float startHeight, float targetHeight)
    {
        float currentTime = 0f;
        float dissolveHeight;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(startHeight, targetHeight, currentTime / duration);

            SetMaterialProperty("_dissolve_height", dissolveHeight);

            yield return null;
        }
    }

    public void PlayHurtBlinkEffect()
    {
        StartCoroutine(BlinkMaterialProperty("_blink", 0.4f));
    }

    private IEnumerator BlinkMaterialProperty(string propertyName, float value)
    {
        SetMaterialProperty(propertyName, value);
        yield return new WaitForSeconds(blinkResetDuration);
        SetMaterialProperty(propertyName, 0f);
    }

    public void StartCharacterDissolvingEffect()
    {
        StartCoroutine(DissolveMaterial());
    }

    private IEnumerator DissolveMaterial()
    {
        yield return new WaitForSeconds(blinkResetDuration);

        SetMaterialProperty("_enableDissolve", 1f);

        yield return ChangeMaterialHeightOverTime(dissolveDuration, dissolveStartHeight, dissolveTargetHeight);

        HandleDissolvingCompletion();
    }

    private void HandleDissolvingCompletion()
    {
        PickupsService.Instance.SpawnHealOrb(transform.position);
        EventService.Instance.InvokeOnEnemyDissolved(GetComponent<EnemyView>());
    }

    public void StartCharacterAppearingEffect()
    {
        StartCoroutine(AppearMaterial());
    }

    private IEnumerator AppearMaterial()
    {
        SetMaterialProperty("_enableDissolve", 1f);

        yield return ChangeMaterialHeightOverTime(appearDuration, appearStartHeight, appearTargetHeight);

        SetMaterialProperty("_enableDissolve", 0f);
    }
}
