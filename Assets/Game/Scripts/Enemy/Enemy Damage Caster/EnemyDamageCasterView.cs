using UnityEngine;

public class EnemyDamageCasterView : DamageCasterView
{
    [SerializeField] EnemySO enemySO;
    protected override void Awake()
    {
        base.Awake();

        model = new DamageCasterModel(enemySO.hitForce, enemySO.damage);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>())
            EventService.Instance.InvokeOnPlayerHit(transform.parent.position, model.HitForce, model.Damage);
    }
}
