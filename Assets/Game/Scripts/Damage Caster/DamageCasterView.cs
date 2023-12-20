using UnityEngine;

public abstract class DamageCasterView : MonoBehaviour // Parent class for Player and Enemies
{
    protected abstract void OnTriggerEnter(Collider other);

    protected DamageCasterModel model;

    private Collider damageCasterCollider;  // gets enabled over few attack animation frames and applies damage to other when enabled.

    protected virtual void Awake()
    {
        damageCasterCollider = GetComponent<Collider>();
        damageCasterCollider.enabled = false;
    }

    // called via animation event, called at the mid of attack animation
    public void EnableDamageCaster() 
    {
        damageCasterCollider.enabled = true; 
    }

    // called via animation event, called at the end of attack animation
    public void DisableDamageCaster()
    {
        damageCasterCollider.enabled = false; 
    }

    // for visualization in editor mode, if damageCasterCollider is touching other(enemy) or not during attack animation
    private void OnDrawGizmos()
    {
        InitializeDamageCasterCollider();

        DrawHitSphereOnCollision();
    }

    private void InitializeDamageCasterCollider()
    {
        if (damageCasterCollider == null)
            damageCasterCollider = GetComponent<Collider>();
    }

    private void DrawHitSphereOnCollision()
    {
        RaycastHit raycastHit;
        bool isHit;
        DrawCollisionBox(out raycastHit, out isHit);

        if (isHit)
            DrawSphere(raycastHit); // Draws the sphere if BoxCast is touching other
    }

    // Draws box same size as damageCasterCollider
    protected void DrawCollisionBox(out RaycastHit raycastHit, out bool isHit)
    {
        Vector3 originalPos = GetBoxCastOrigin();
        isHit = Physics.BoxCast(originalPos, damageCasterCollider.bounds.extents / 2, transform.forward, out raycastHit, transform.rotation, damageCasterCollider.bounds.extents.z, 1 << 6);
    }

    private static void DrawSphere(RaycastHit raycastHit) 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(raycastHit.point, 0.3f);
    }

    private Vector3 GetBoxCastOrigin()
    {
        return transform.position + (-damageCasterCollider.bounds.extents.z) * transform.forward;
    }
}
