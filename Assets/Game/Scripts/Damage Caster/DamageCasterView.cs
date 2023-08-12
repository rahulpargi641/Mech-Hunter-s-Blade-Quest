using UnityEngine;

public class DamageCasterView : MonoBehaviour
{
    private DamageCasterModel model;
    private Collider damageCasterCollider;

    private void Awake()
    {
        model = new DamageCasterModel();
        damageCasterCollider = GetComponent<Collider>();
        damageCasterCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthView enemyhealthView = other.GetComponent<HealthView>();

        if(enemyhealthView && ! model.damagedTargets.Contains(other))
        {
            enemyhealthView.ApplyDamage(model.Damage);
            PlayerVFXView playerVFXView = transform.parent.GetComponent<PlayerVFXView>();
            if(playerVFXView)
            {
                RaycastHit hit;
                bool isHit;
                DrawBox(out hit, out isHit);

                if (isHit)
                    playerVFXView.PlaySlash(hit.point + new Vector3(0f, 0.5f, 0f));
            }

        }

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

    private void DrawBox(out RaycastHit hit, out bool isHit)
    {
        Vector3 originalPos = transform.position + (-damageCasterCollider.bounds.extents.z) * transform.forward;

        isHit = Physics.BoxCast(originalPos, damageCasterCollider.bounds.extents / 2, transform.forward, out hit, transform.rotation, damageCasterCollider.bounds.extents.z, 1 << 6);
    }
}
