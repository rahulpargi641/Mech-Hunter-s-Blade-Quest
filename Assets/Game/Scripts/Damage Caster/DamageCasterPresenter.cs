using UnityEngine;

public class DamageCasterPresenter : MonoBehaviour
{
    private Collider damageCasterCollider; 

    protected DamageCasterModel model;

    private void Awake()
    {
        model = new DamageCasterModel();

        damageCasterCollider = GetComponent<Collider>();
        damageCasterCollider.enabled = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        model.damagedTargets.Add(other);
    }

    // called via animation event
    public void EnableDamageCaster()
    {
        model.damagedTargets.Clear();
        damageCasterCollider.enabled = true;
    }

    // called via animation event
    public void DisableDamageCaster()
    {
        model.damagedTargets.Clear();
        damageCasterCollider.enabled = false;
    }

    // for visualization in editor mode, if damageCasterCollider is touching other or not 
    private void OnDrawGizmos()
    {
        if (damageCasterCollider == null)
            damageCasterCollider = GetComponent<Collider>();

        RaycastHit hit;
        bool isHit;
        DrawBox(out hit, out isHit);

        if (isHit)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(hit.point, 0.3f);
        }
    }

    // Draws box same size as damageCasterCollider
    protected void DrawBox(out RaycastHit hit, out bool isHit)
    {
        Vector3 originalPos = transform.position + (-damageCasterCollider.bounds.extents.z) * transform.forward;

        isHit = Physics.BoxCast(originalPos, damageCasterCollider.bounds.extents / 2, transform.forward, out hit, transform.rotation, damageCasterCollider.bounds.extents.z, 1 << 6);
    }
}
