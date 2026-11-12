using System.Collections;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    //Trigger que hace daño para el ataque
    [SerializeField] private GameObject attackHitbox;
    //Cuánto dura el trigger del ataque activado
    [SerializeField] private float hitboxDuration = 0.2f;

    protected override void Attack()
    {
        StartCoroutine(AttackHitboxCrt());
    }

    IEnumerator AttackHitboxCrt()
    {
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(hitboxDuration);
        attackHitbox.SetActive(false);
    }
}
