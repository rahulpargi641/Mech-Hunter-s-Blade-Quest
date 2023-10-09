using System.Collections;
using UnityEngine;

public class GateView : MonoBehaviour
{
    [SerializeField] GameObject gateVisual;

    private BoxCollider gateCollider;

    private GateModel model;

    private void Awake()
    {
        gateCollider = GetComponent<BoxCollider>();
        model = new GateModel();
    }


    private void Start()
    {
        EventService.Instance.onCurrentEnemyGroupDeadAction += OpenGate;
    }

    private void OnDestroy()
    {
        EventService.Instance.onCurrentEnemyGroupDeadAction -= OpenGate;
    }

    private void OpenGate()
    {
        StartCoroutine(OpenGateAnimation());
    }

    IEnumerator OpenGateAnimation()
    {
        float currentOpen_Duration = 0;
        Vector3 startPos = gateVisual.transform.position;
        Vector3 targetPos = startPos + Vector3.up * model.OpenTargetY;

        while(currentOpen_Duration < model.OpenDuration)
        {
            currentOpen_Duration += Time.deltaTime;
            gateVisual.transform.position = Vector3.Lerp(startPos, targetPos, currentOpen_Duration / model.OpenDuration);
            yield return null;
        }
        gateCollider.gameObject.SetActive(false);
    }
}
