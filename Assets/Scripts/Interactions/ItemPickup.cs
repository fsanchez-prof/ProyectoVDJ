using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    //Referencia al collider para poder activarlo y desactivarlo
    private Collider col;
    private Rigidbody rb;

    void Start()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    //Esta funcion se llama por el PickupDetector. Tiene que ser pública o no dará acceso
    public void PickUp(Transform point)
    {
        //Emparentar el objeto a la mano del jugador
        transform.SetParent(point);
        //Para moverlo a la posicion exacta del padre hay que modificar su LOCAL POSITION
        transform.localPosition = Vector3.zero;
        //Desactivar la colision del objeto
        col.enabled = false;
        //Cambiar el rigidbody a Kinematic para que se esté quieto
        rb.isKinematic = true;
        //Cambiar la capa del objeto para que se renderice por encima de todo lo demás (como el brazo)
        gameObject.layer = LayerMask.NameToLayer("FPSArm");        
    }

    public void Drop()
    {
        //Desemparentar el objeto
        transform.SetParent(null);
        //Reactivar el collider
        col.enabled = true;
        //Quitar el Kinematic al rigidbody para que vuelva a tener físicas
        rb.isKinematic = false;
        //Devolver la capa a su valor original (Pikup) para que se pueda coger el objeto otra vez
        gameObject.layer = LayerMask.NameToLayer("Pickup");
    }

    public void Throw(Vector3 force)
    {
        //Soltar el objeto antes de lanzarlo
        Drop();
        //Aplicar la fuerza que mande el jugador
        rb.AddForce(force, ForceMode.VelocityChange);
    }
}
