using System.Collections;
using UnityEngine;

public class BombPickup : ItemPickup
{
    [SerializeField] private float explosionRange = 4;
    [SerializeField] private LayerMask affectedLayers;
    [SerializeField] private float explosionDelay = 3;

    IEnumerator ExplodeCrt()
    {
        yield return new WaitForSeconds(explosionDelay);
        Explode();
        Destroy(this.gameObject);
    }

    void Explode()
    {
        Debug.Log("BOOOOOOOOOOOOOOM!!!!!");
        //Genera un OverlapSphere alrededor de la bomba para detectar todos los objetos afectados
        Collider[] targets = Physics.OverlapSphere(transform.position, explosionRange, affectedLayers);
        //Si no detecta nada, en el array no habrá ningún objeto
        if(targets.Length > 0)
        {
            //Recorre el array para hacer algo con todos los objetos que haya detectado
            foreach (Collider target in targets)
            {
                //Destruiiiiiir!!!!!
                Destroy(target.gameObject);
            }
        }
    }

    public override void PickUp(Transform point)
    {
        //Esto ejecuta todo el código que haya en la función PickUp de la clase base (ItemPickup)
        //En este caso, si no se añade esta línea, no se podría coger este objeto
        base.PickUp(point);
        //Cuando cojamos este objeto, activa la cuenta atrás para explotar
        StartCoroutine(ExplodeCrt());
    }

    private void OnDrawGizmos()
    {
        //Dibujar el alcance de la explosión
        Gizmos.color = Color.indianRed;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

}
