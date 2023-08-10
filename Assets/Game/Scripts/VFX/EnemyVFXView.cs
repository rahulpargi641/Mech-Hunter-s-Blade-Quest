using UnityEngine;
using UnityEngine.VFX;

public class EnemyVFXView : MonoBehaviour
{
    [SerializeField] VisualEffect footStep;
    [SerializeField] VisualEffect attackSmashVFX;

    public void BurstFootStep()
    {
        footStep.SendEvent("OnPlay");
    }

    public void PlayAttackSmashVFX()
    {
        attackSmashVFX.SendEvent("OnPlay");
    }
}
