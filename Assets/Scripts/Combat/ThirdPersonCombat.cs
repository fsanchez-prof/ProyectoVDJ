using UnityEngine;

public class ThirdPersonCombat : MonoBehaviour
{
    //El ataque de la cadena que esté haciendo actualmente
    [SerializeField] private int currentCombo = 0;
    //Cuántos ataques tiene la cadena de golpes
    [SerializeField] private int attackCount = 3;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        //Cada vez que se ataca se aumenta en 1 el contador de ataque actual
        currentCombo++;

        //Activar la animación del ataque actual de la cadena
        anim.SetInteger("AttackID", currentCombo);

        //Si llega al último ataque de la cadena, se tiene que empezar desde el principio
        if(currentCombo >= attackCount)
        {
            currentCombo = 0;
        }
    }
}
