using UnityEngine;
using UnityEngine.VFX;

public class EnemyVFXView : MonoBehaviour
{
    [SerializeField] VisualEffect footStep;

    public void BurstFootStep()
    {
        footStep.SendEvent("OnPlay");
    }
}
