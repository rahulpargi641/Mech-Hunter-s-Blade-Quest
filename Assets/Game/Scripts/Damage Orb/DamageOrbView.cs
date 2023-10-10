using UnityEngine;

public class DamageOrbView : MonoBehaviour
{
    [SerializeField] ParticleSystem hitVFX;
    public Rigidbody RigidBody { get; private set; }
    public DamageOrbController Controller { private get; set; }

    void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Controller.MoveOrbForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealthPresenter playerHealth = other.GetComponent<PlayerHealthPresenter>();
        if(playerHealth)
        {
            Controller.ProcessHit(playerHealth);
            PlayHitVFX();
        }
    }

    // Play from Particle Pool
    private void PlayHitVFX()
    {
        Instantiate(hitVFX, transform.position, Quaternion.identity);
        hitVFX.Play();
        Destroy(gameObject);
    }
}
