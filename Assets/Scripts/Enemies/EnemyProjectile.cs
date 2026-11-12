using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public void Shoot(Vector3 force)
    {
        rb.AddForce(force, ForceMode.VelocityChange);
    }

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
