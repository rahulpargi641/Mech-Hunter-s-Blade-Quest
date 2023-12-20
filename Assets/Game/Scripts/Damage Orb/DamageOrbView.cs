using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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
        PlayerView playerView = other.GetComponent<PlayerView>();
        if(playerView)
        {
            Controller.ProcessHit();
            PlayDamageOrbHitVFX();
        }
    }

    private void PlayDamageOrbHitVFX()
    {
        ParticleSystem damageOrbHitVFX = VFXService.Instance.SpawnDamageOrbHitVFX(transform.position);
        damageOrbHitVFX.Play();
    }
}
