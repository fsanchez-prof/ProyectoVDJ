using UnityEngine;

public class FPShoot : MonoBehaviour
{
    //Cadencia de disparo: cuantas balas se disparan por segundo
    [SerializeField] private float fireRate = 10;
    //Alcance de los disparos
    [SerializeField] private float weaponRange = 100;

    //Timer para la cadencia de disparo automatico
    private float timeToShoot = 0;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //Incluir el timer con Time.time para que el disparo sea automático
        if (Input.GetMouseButton(0) && Time.time >= timeToShoot)
        {
            //Para usar una cadencia real de balas por segundo, se utiliza este cálculo
            timeToShoot = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        //Esta funcion calcula un rayo que va desde la camara hacia una posicion exacta de la pantalla
        //En este caso, para que vaya al centro la posicion es 0.5 en X y en Y
        //El valor del eje Z siempre se pone ese para que no de fallos (o eso dice Unity...)
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, cam.nearClipPlane));

        //Dibujar el rayo del disparo
        Debug.DrawRay(ray.origin, ray.direction * weaponRange, Color.indianRed, 1);

        //Para saber que ha golpeado el raycast, hay que usar una variable de tipo Raycasthit que guarda mucha infos
        if (Physics.Raycast(ray, out RaycastHit hit, weaponRange))
        {
            Debug.Log($"He disparado a: {hit.collider}");
        }
    }
}
