using UnityEngine;

public class PlayerVFXService : MonoSingletonGeneric<PlayerVFXService>
{
    [SerializeField] PlayerVFXView playerVFXView;

    public void PlayHealVFX()
    {
        playerVFXView.PlayHealVFX();
    }
}
