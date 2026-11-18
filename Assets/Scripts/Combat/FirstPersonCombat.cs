using UnityEngine;

public class FirstPersonCombat : MonoBehaviour
{
    //Tamaño y posición del overlap que funciona como Hitbox
    [SerializeField] private Transform hitboxOrigin;
    [SerializeField] private Vector3 hitboxSize = Vector3.one;
    //Todas las capas a las que afecta el Overlap
    [SerializeField] private LayerMask affectedLayers;
    [SerializeField] private int damage = 50;

    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("MeleeAttackFPS"))
        {
            anim.SetTrigger("MeleeAttack");
        }
    }

    //Evento de animación en las animaciones de ataque cuerpo a cuerpo
    void MeleeAttack()
    {
        //Crear un OverlapBox que actúa como hitbox y detecta distintos colliders
        Collider[] targets = Physics.OverlapBox(hitboxOrigin.position, hitboxSize * 0.5f, 
            transform.rotation, affectedLayers);

        foreach (Collider target in targets)
        {
            Debug.Log($"Melee attack hits: {target}");
            target.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        if(hitboxOrigin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(hitboxOrigin.position, hitboxSize);
        }
    }
}
