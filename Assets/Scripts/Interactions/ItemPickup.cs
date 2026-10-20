using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    //Referencia al collider para poder activarlo y desactivarlo
    private Collider col;

    void Start()
    {
        col = GetComponent<Collider>();
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
    }
}
