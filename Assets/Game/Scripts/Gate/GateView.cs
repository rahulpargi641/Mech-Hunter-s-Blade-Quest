using System.Collections;
using UnityEngine;

public class GateView : MonoBehaviour
{
    [SerializeField] GameObject gateVisual;

    private BoxCollider gateCollider;

    private GateModel model;
    private bool isOpened;

    private void Awake()
    {
        gateCollider = GetComponent<BoxCollider>();
        model = new GateModel();
    }

    private void Start()
    {
        EventService.Instance.onCurrentEnemyGroupDead += OpenGate;
    }

    private void OnDestroy()
    {
        EventService.Instance.onCurrentEnemyGroupDead -= OpenGate;
    }

    private void OpenGate()
    {
        if (isOpened) return;

        StartCoroutine(OpenGateAnimation());
    }

    IEnumerator OpenGateAnimation()
    {
        float currentOpenDuration = 0;
        Vector3 startPos = gateVisual.transform.position;
        Vector3 targetPos = startPos + Vector3.up * model.OpenTargetY;

        while (currentOpenDuration < model.OpenDuration)
        {
            UpdateGatePosition(startPos, targetPos, ref currentOpenDuration);
            yield return null;
        }

        FinalizeGateOpening();
    }

    private void UpdateGatePosition(Vector3 startPos, Vector3 targetPos, ref float currentOpenDuration)
    {
        currentOpenDuration += Time.deltaTime;
        gateVisual.transform.position = Vector3.Lerp(startPos, targetPos, currentOpenDuration / model.OpenDuration);
    }

    private void FinalizeGateOpening()
    {
        gateCollider.gameObject.SetActive(false);
        isOpened = true;
    }
}
