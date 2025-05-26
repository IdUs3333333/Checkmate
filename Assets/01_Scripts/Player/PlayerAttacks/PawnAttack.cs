using System.Collections;
using UnityEngine;

public class PawnAttack : IAttackStrategy
{
    public IEnumerator ExecuteAttack(Player player, Transform attackTransform, Camera maincam)
    {
        float attackRange = player.stats.attackRange[(int)player.type];
        float angleThreshold = 45f;

        Vector2 attackDir = (maincam.ScreenToWorldPoint(Input.mousePosition) - player.transform.position);
        Vector2 attackPos = (Vector2)player.transform.position + attackDir.normalized * attackRange * 2f;

        GameObject particle = GameObject.Instantiate(player.attack.pawnAttackParticle, attackPos, Quaternion.identity);
        GameObject.Destroy(particle, 1f);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos, attackRange);
        foreach(Collider2D enemy in hitEnemies)
        {
            Vector2 towardEnemy = ((Vector2)enemy.transform.position - (Vector2)player.transform.position).normalized;
            float angle = Vector2.Angle(attackDir, towardEnemy);
            
            if(angle <= angleThreshold)
            {
                enemy.GetComponent<Enemy>()?.Damage(player.stats.attackDamage);
            }
        }

        yield return null;
    }
}
