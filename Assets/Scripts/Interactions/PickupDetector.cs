using UnityEngine;

public class PickupDetector : MonoBehaviour
{
    //Cuanto alcance tiene el rayo
    [SerializeField] private float detectionRange = 1;
    //Radio de la esfera que se lanza a traves del rayo
    [SerializeField] private float sphereRadius = 1;
    //La capa en la que estaran los objetos que pueden recogerse
    [SerializeField] private LayerMask pickupLayers;

    //Rayo calculado por la camara hacia el centro de la pantalla
    private Ray ray;

    void Start()
    {
        
    }

    void Update()
    {
        Detect();
    }

    void Detect()
    {
        //Calcular un rayo que va hacia el centro de la pantalla
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));

        //Usamos SphereCast porque el raycast normal es muy fino y puede dar problemas con objetos pequeños
        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hit, detectionRange, pickupLayers))
        {
            Debug.Log($"Estoy viendo el objeto {hit.collider}");
        }
    }

    private void OnDrawGizmos()
    {
        //Representacion visual del SphereCast (aproximada)
        Gizmos.color = Color.cornflowerBlue;
        Gizmos.DrawWireSphere(ray.origin, sphereRadius);
        Gizmos.DrawRay(ray);
    }
}
