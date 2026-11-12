using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Te surro {other.name} !! >:(");
    }
}
