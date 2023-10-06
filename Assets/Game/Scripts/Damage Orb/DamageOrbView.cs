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
        Controller.MoveForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerView playerView = other.GetComponent<PlayerView>();
        if(playerView)
        {
            Controller.ProcessHit();
            PlayHitVFX();
        }
    }

    // Play from VFX Pool
    private void PlayHitVFX()
    {
        Instantiate(hitVFX, transform.position, Quaternion.identity);
        hitVFX.Play();
        Destroy(gameObject);
    }
}
