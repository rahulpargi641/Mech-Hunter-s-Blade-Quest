using System.Collections;
using UnityEngine;

public class CharacterMaterial : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private MaterialPropertyBlock materialPropertyBlock;

    private float blinkResetDuration = 0.2f;

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

    public void PlayHurtBlinkEffect()
    {
        StartCoroutine(BlinkMaterialProperty("_blink", 0.4f));

        IEnumerator BlinkMaterialProperty(string propertyName, float value)
        {
            SetMaterialProperty(propertyName, value);
            yield return new WaitForSeconds(blinkResetDuration);
            SetMaterialProperty(propertyName, 0f);
        }
    }

    private void SetMaterialProperty(string propertyName, float value)
    {
        materialPropertyBlock.SetFloat(propertyName, value);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    public void StartCharacterDissolvingEffect()
    {
        StartCoroutine(DissolveMaterial(2f, 20f, -10f));
    }

    IEnumerator DissolveMaterial(float duration, float startHeight, float targetHeight)
    {
        yield return new WaitForSeconds(2f);

        SetMaterialProperty("_enableDissolve", 1f);

        float currentTime = 0f;
        float dissolveHeight;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(startHeight, targetHeight, currentTime / duration);

            SetMaterialProperty("_dissolve_height", dissolveHeight);

            yield return null;
        }

        HandleDissolvingCompletion();
    }

    private void HandleDissolvingCompletion()
    {
        PickupsService.Instance.SpawnHealOrb(transform.position);
        EventService.Instance.InvokeOnEnemyDissolved(GetComponent<EnemyView>());
    }

    public void StartCharacterAppearingEffect()
    {
        StartCoroutine(AppearMaterial(3f, -10f, 20f));
    }

    IEnumerator AppearMaterial(float duration, float startHeight, float targetHeight)
    {
        SetMaterialProperty("_enableDissolve", 1f);

        float currentTime = 0f;
        float dissolveHeight;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(startHeight, targetHeight, currentTime / duration);

            SetMaterialProperty("_dissolve_height", dissolveHeight);

            yield return null;
        }

        SetMaterialProperty("_enableDissolve", 0f);
    }
}
