using UnityEngine;

public class DamageCasterView : MonoBehaviour
{
    [SerializeField] int damage;
    protected DamageCasterModel model;
    private Collider damageCasterCollider;

    private void Awake()
    {
        model = new DamageCasterModel(damage);
        damageCasterCollider = GetComponent<Collider>();
        damageCasterCollider.enabled = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        model.damagedTargets.Add(other);
    }

    public void EnableDamageCaster()
    {
        model.damagedTargets.Clear();
        damageCasterCollider.enabled = true;
    }

    public void DisableDamageCaster()
    {
        model.damagedTargets.Clear();
        damageCasterCollider.enabled = false;
    }

    private void OnDrawGizmos()
    {
        if (damageCasterCollider == null)
            damageCasterCollider = GetComponent<Collider>();

        RaycastHit hit;
        bool isHit;
        DrawBox(out hit, out isHit);

        //if (isHit)
        //{
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawSphere(hit.point, 0.3f);
        //}
    }

    protected void DrawBox(out RaycastHit hit, out bool isHit)
    {
        Vector3 originalPos = transform.position + (-damageCasterCollider.bounds.extents.z) * transform.forward;

        isHit = Physics.BoxCast(originalPos, damageCasterCollider.bounds.extents / 2, transform.forward, out hit, transform.rotation, damageCasterCollider.bounds.extents.z, 1 << 6);
    }
}
