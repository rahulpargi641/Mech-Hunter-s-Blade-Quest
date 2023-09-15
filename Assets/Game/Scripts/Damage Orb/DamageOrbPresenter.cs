using UnityEngine;

public class DamageOrbPresenter : MonoBehaviour
{
    [SerializeField] ParticleSystem hitVFX;
    private Rigidbody rigidBody;

    private DamageOrbModel model;

    DamageOrbPresenter()
    {
        model = new DamageOrbModel();
    }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(transform.position + transform.forward * model.Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerView playerView = other.GetComponent<PlayerView>();
        if(playerView)
        {
            PlayerService.Instance.AddHitImpact(transform.position, 10f);
            EventService.Instance.InvokePlayerHitAction();
            PlayerHealthService.Instance.ApplyDamage(model.Damage);
            Instantiate(hitVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
