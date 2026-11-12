using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;

    [Header("DETECTION")]
    [Range(0, 360)]
    [SerializeField] private float visionAngle = 90; //Ángulo de visión completo
    [SerializeField] private float detectionRange = 5; //Rango para detectar al jugador cuando esté cerca
    [SerializeField] private LayerMask detectionLayers; //Capas que detecta (jugador, otras cosas...)
    [SerializeField] private Transform detectedTarget; //Objeto que haya detectado con el Overlap

    [Header("COMBAT")]
    [SerializeField] private float attackDistance = 2; //Distancia a la que empieza a atacar
    [SerializeField] private float timeBtnAttacks = 1; //Tiempo entre un ataque y el siguiente
    [SerializeField] private float lookAtSpeed = 10; //Velocidad de rotación para mirar al objetivo

    private bool canAttack = true;

    protected NavMeshAgent agent;
    //Objeto al que tiene que perseguir una vez detectado por visión
    protected Transform chaseTarget;
    //La direccion en la que esta el objetivo (se calcula en LookAtPlayer)
    protected Vector3 directionToTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //Busca y guarda todos los puntos posibles de movimiento
        waypoints = GameObject.FindGameObjectsWithTag("EnemyWaypoint");
        PickRandomPoint();
    }

    void Update()
    {
        Detect();
        CheckVisionRange();

        //Si tiene un objeto al que perseguir, lo persigue!
        if(chaseTarget != null)
        {
            ChaseTarget();
        }
        else //Cuando no tiene objetivo al que perseguir, se mueve por su cuenta
        {
            Wander();
        }
    }

    //Elige un punto de movimiento aleatorio
    void PickRandomPoint()
    {
        //Coger un punto aleatorio del array
        GameObject randomPoint = waypoints[Random.Range(0, waypoints.Length)];
        agent.SetDestination(randomPoint.transform.position);
    }

    void ChaseTarget()
    {
        agent.SetDestination(chaseTarget.position);
        //Si se encuentra en distancia de ataque Y puede atacar, ataca >:(
        if (agent.remainingDistance <= attackDistance)
        {
            //Mientras está en distancia de ataque, mira hacia el objetivo
            LookAtPlayer();
            //Al ataqueeeeer
            if(canAttack == true)
            {
                //Ataca y activa el cooldown para el siguiente ataque
                Attack();
                StartCoroutine(AttackCooldownCrt());
            }
            //Hacer que se quede quieto mientras está en distancia de ataque
            agent.isStopped = true;
        }
        //Cuando se aleja del rango de ataque, vuelve a moverse y perseguir
        else if(agent.isStopped == true)
        {
            agent.isStopped = false;
        }
    }

    void Wander()
    {
        //Comprobar si ha llegado a su destino actual
        //Si tiene algo de stoppingDistance, hay que tenerlo en cuenta o nunca llega a su destino :(
        if (agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            //Cuando llega a su punto de destino, elige uno nuevo
            PickRandomPoint();
        }
    }

    void Detect()
    {
        //Crea continuamente una esfera a su alrededor para detectar objetos cercanos
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, detectionRange, detectionLayers);
        //Si detecta algo, guarda el primer objeto que haya detectado en la variable
        if (detectedObjects.Length > 0)
        {
            //Solo guarda el objetivo si no ha detectado nada
            if (detectedTarget == null)
            {
                detectedTarget = detectedObjects[0].transform;
            }
        }
        //Si un objeto detectado se aleja del rango, lo quita de la variable
        else if (detectedTarget != null)
        {
            detectedTarget = null;
            //Cuando se aleja mucho del enemigo, deja de perseguirlo
            if(chaseTarget != null)
            {
                chaseTarget = null;
                //Busca un nuevo punto hacia el que moverse
                PickRandomPoint();
            }
        }
    }

    void CheckVisionRange()
    {
        //Si no tiene objetivo detectado O ya está persiguiendo algo, no hace nada
        if (detectedTarget == null || chaseTarget != null) return;

        //Calcular la dirección hacia el objeto detectado
        Vector3 direction = detectedTarget.position - transform.position;
        //Calcular el producto escalar entre la dirección en la que mira y la dirección del jugador
        float dot = Vector3.Dot(transform.forward, direction);
        //El cálculo de magia negra con el coseno sirve para transformar el valor del ángulo de visión
        //en número entre -1 y 1 que sirva para el producto escalar
        if (dot >= Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad))
        {
            //Asignar el objeto a perseguir
            chaseTarget = detectedTarget;
        }
    }

    //Todos los enemigos que hereden de este script modifican esta función
    //para hacer una cosa u otra al atacar
    protected virtual void Attack()
    {
        Debug.Log("Que te pego! >:V");
    }

    IEnumerator AttackCooldownCrt()
    {
        canAttack = false;
        yield return new WaitForSeconds(timeBtnAttacks);
        canAttack = true;
    }

    void LookAtPlayer()
    {
        //Calcular la dirección en la que se encuentra el objetivo
        directionToTarget = chaseTarget.position - transform.position;
        //Para que solo rote en el eje Y, da igual la diferencia de altura
        directionToTarget.y = 0;
        //Rotar el objeto para que mire en esa dirección
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(directionToTarget), Time.deltaTime * lookAtSpeed);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.moccasin;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.indianRed;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        if(agent != null)
        {
            Gizmos.color = Color.rebeccaPurple;
            Gizmos.DrawWireSphere(transform.position, agent.stoppingDistance);
        }

    }
}
