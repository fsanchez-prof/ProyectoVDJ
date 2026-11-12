using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    [SerializeField] private EnemyProjectile projectilePrefab;
    [SerializeField] private float projectileSpeed = 10;
    [SerializeField] private Transform projectileOrigin;

    protected override void Attack()
    {
        EnemyProjectile projectile = Instantiate(projectilePrefab, projectileOrigin.position,
            projectileOrigin.rotation);
        //Para que pueda disparar bien aunque el jugador esté en mayor o menor altura
        directionToTarget.y = chaseTarget.position.y - 1;
        projectile.Shoot(directionToTarget.normalized * projectileSpeed);

    }
}
