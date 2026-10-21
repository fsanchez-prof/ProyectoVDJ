using UnityEngine;

public class PickupDetector : MonoBehaviour
{
    //Cuanto alcance tiene el rayo
    [SerializeField] private float detectionRange = 1;
    //Radio de la esfera que se lanza a traves del rayo
    [SerializeField] private float sphereRadius = 1;
    //La capa en la que estaran los objetos que pueden recogerse
    [SerializeField] private LayerMask pickupLayers;
    //Guarda una referencia al objeto que lleva en la mano
    [SerializeField] private ItemPickup itemInHand;
    //Referencia al objeto que esté detectando en ese momento
    [SerializeField] private ItemPickup detectedItem;
    //Referencia al objeto vacio al que se emparentan los objetos recogidos
    [SerializeField] private Transform pickupPoint;
    //Fuerza con la que se lanzan los objetos
    [SerializeField] private float throwForce = 20;

    //Rayo calculado por la camara hacia el centro de la pantalla
    private Ray ray;

    void Start()
    {
        
    }

    void Update()
    {
        //Si se pulsa la tecla de interactuar Y LLEVAMOS UN OBJETO EN LA MANO, se suelta
        if (Input.GetButtonDown("Interact") && itemInHand != null)
        {
            //Soltar el objeto y cambiar la variable a NULL
            itemInHand.Drop();
            itemInHand = null;
        }

        //Si se pulsa la tecla de lanzar y llevamos un objeto en la mano
        //Usamos este eje para que funcione con el gatillo R2 de un mando
        if (Input.GetAxisRaw("Shoot") > 0.1f && itemInHand != null)
        {
            //Lanzar el objeto y cambiar la variable a NULL
            itemInHand.Throw(transform.forward * throwForce);
            itemInHand = null;
        }

        Debug.Log(Input.GetAxisRaw("Shoot"));

        //Si se pulsa la tecla de interactuar Y hay un objeto detectado delante
        if (Input.GetButtonDown("Interact") && detectedItem != null && itemInHand == null)
        {
            //Guardar referencia del objeto que hemos cogido
            itemInHand = detectedItem;
            //Volvemos a cambiar el objeto detectado a NULL
            detectedItem = null;
            //Llamar a la funcion del objeto para cogerlo
            itemInHand.PickUp(pickupPoint);
        }

        //Detectar objetos siempre y cuando no tengamos ya uno en la mano
        if(itemInHand == null)
        {
            Detect();
        }
    }

    void Detect()
    {
        //Calcular un rayo que va hacia el centro de la pantalla
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));

        //Usamos SphereCast porque el raycast normal es muy fino y puede dar problemas con objetos pequeños
        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hit, detectionRange, pickupLayers))
        {
            Debug.Log($"Estoy viendo el objeto {hit.collider}");
            //Si no tenemos guardado el objeto detectado, hacemos un GetComponent y lo guardamos
            //Asi nos aseguramos de que solo haga GetComponent una vez
            if (detectedItem == null)
            {
                detectedItem = hit.collider.GetComponent<ItemPickup>();
            }
        }
        //Si no detecta nada PERO tenia un objeto detectado guardado, tiene que quitarlo
        else if(detectedItem != null)
        {
            detectedItem = null;
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
